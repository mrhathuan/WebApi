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
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using System.Globalization;
using ServicesExtend;

namespace ClientWeb
{
    public class OPSController : BaseController
    {
        #region Const

        const int iLO = -(int)SYSVarType.ServiceOfOrderLocal;
        const int iIM = -(int)SYSVarType.ServiceOfOrderImport;
        const int iEx = -(int)SYSVarType.ServiceOfOrderExport;
        const int iFCL = -(int)SYSVarType.TransportModeFCL;
        const int iFTL = -(int)SYSVarType.TransportModeFTL;
        const int iLTL = -(int)SYSVarType.TransportModeLTL;

        #endregion

        #region COAppointment
        [HttpPost]
        public DTOResult OPS_COAppointment_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.COAppointment_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void COAppointment_Cancel(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.COAppointment_Cancel(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DIAppointment
        [HttpPost]
        public DTOResult OPS_DIAppointment_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Cancel(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Cancel(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Common

        [HttpPost]
        public List<DTOOPS_FLMPlaning> OPS_DIAppointment_FLMPlaning(dynamic dynParam)
        {
            try
            {
                List<DTOOPS_FLMPlaning> result = new List<DTOOPS_FLMPlaning>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Appointment_Route_FLMPlaning();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DIAppointment_Route
        [HttpPost]
        public List<DTOFLMVehicle> OPS_DIAppointment_Route_VehicleList()
        {
            try
            {
                var result = new List<DTOFLMVehicle>();
                string request = string.Empty;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentVENVehicle> OPS_DIAppointment_Route_VehicleTOVENList()
        {
            try
            {
                var result = new List<DTODIAppointmentVENVehicle>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleTOVENList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMAssetTimeSheet> OPS_DIAppointment_Route_VehicleTimeList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOFLMAssetTimeSheet>();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleTimeList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMAssetTimeSheet> OPS_DIAppointment_Route_VehicleTOVEN(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOFLMAssetTimeSheet>();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleTOVEN(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTODIAppointmentOrder OPS_DIAppointment_Route_VehicleTimeGet(dynamic dynParam)
        {
            try
            {
                var result = default(DTODIAppointmentOrder);
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleTimeGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOAppointmentRouteActivity OPS_DIAppointment_Route_ActivityGet(dynamic dynParam)
        {
            try
            {
                var result = default(DTOAppointmentRouteActivity);
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Appointment_Route_ActivityGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_ActivitySave(dynamic dynParam)
        {
            try
            {
                DTOAppointmentRouteActivity item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOAppointmentRouteActivity>(dynParam.item.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Appointment_Route_ActivitySave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDIShiptmentRoute OPS_DIAppointment_Route_VehicleDetail(dynamic dynParam)
        {
            try
            {
                var result = default(DTOOPSDIShiptmentRoute);
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleDetail(lstid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<FLMDriver> OPS_DIAppointment_Route_VehicleListDriver()
        {
            try
            {
                var result = new List<FLMDriver>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Appointment_Route_ListDriver();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool COAppointment_2View_Master_CheckDriver(dynamic dynParam)
        {
            try
            {
                bool result = false;
                int vehicleID = (int)dynParam.vehicleID;
                int driverID = (int)dynParam.driverID;
                DateTime etd = Convert.ToDateTime(dynParam.etd.ToString());
                DateTime eta = Convert.ToDateTime(dynParam.eta.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.COAppointment_2View_Master_CheckDriver(vehicleID, driverID, etd, eta);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_VehicleTOVENInDate(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                int id = Convert.ToInt32(dynParam.id.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleTOVENInDate(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATGroupOfVehicle> OPS_DIAppointment_Route_VehicleListGroupVehicle()
        {
            try
            {
                var result = new List<CATGroupOfVehicle>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleListGroupVehicle();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentGroupProduct> OPS_DIAppointment_Route_VehicleGet(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentGroupProduct>();
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleGet(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_VehicleAdd(dynamic dynParam)
        {
            try
            {
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_VehicleAdd(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string OPS_DIAppointment_Route_VehicleRemoveMonitor(dynamic dynParam)
        {
            try
            {
                string result = string.Empty;
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleRemoveMonitor(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_VehicleSave(dynamic dynParam)
        {
            try
            {
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_VehicleSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_VehicleRemove(dynamic dynParam)
        {
            try
            {
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_VehicleRemove(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_VehicleMonitor(dynamic dynParam)
        {
            try
            {
                int id = Convert.ToInt32(dynParam.id.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_VehicleMonitor(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_VehicleAddRate(dynamic dynParam)
        {
            try
            {
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_VehicleAddRate(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTODIAppointmentOrder OPS_DIAppointment_Route_VehicleTOVENGet(dynamic dynParam)
        {
            try
            {
                var result = default(DTODIAppointmentOrder);
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleTOVENGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentPOD> OPS_DIAppointment_Route_PODList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentPOD>();
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_PODList(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_PODDiv(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentPOD> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentPOD>>(dynParam.lst.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_PODDiv(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_PODExcelSave(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentPOD> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentPOD>>(dynParam.lst.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_PODExcelSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string OPS_DIAppointment_Route_PODExcelDownload(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                var lst = new List<DTODIAppointmentPOD>();
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    lst = sv.DIAppointment_Route_PODList(dtfrom, dtto);
                });
                string filePath = "/" + FolderUpload.Export + "file" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    col++; worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "CustomerID";
                    col++; worksheet.Cells[row, col].Value = "NPP";
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ";
                    col++; worksheet.Cells[row, col].Value = "Route ID";
                    col++; worksheet.Cells[row, col].Value = "Route Description";
                    col++; worksheet.Cells[row, col].Value = "Order No";
                    col++; worksheet.Cells[row, col].Value = "Lít";
                    col++; worksheet.Cells[row, col].Value = "";
                    col++; worksheet.Cells[row, col].Value = "KG";
                    col++; worksheet.Cells[row, col].Value = "Số xe";
                    col++; worksheet.Cells[row, col].Value = "Tên tài xế";
                    col++; worksheet.Cells[row, col].Value = "THỜI GIAN DỰ KIẾN XE CÓ MẶT TẠI KHO";
                    col++; worksheet.Cells[row, col].Value = "Số DN";
                    col++; worksheet.Cells[row, col].Value = "Ngày nhận đơn";
                    col++; worksheet.Cells[row, col].Value = "Buổi";
                    col++; worksheet.Cells[row, col].Value = "Load list";
                    col++; worksheet.Cells[row, col].Value = "Ghi chú";

                    row++;
                    int stt = 1;
                    foreach (var item in lst)
                    {
                        col = 1; worksheet.Cells[row, col].Value = item.TOMasterCode;
                        col++; worksheet.Cells[row, col].Value = "";
                        col++; worksheet.Cells[row, col].Value = item.PartnerCode;
                        col++; worksheet.Cells[row, col].Value = item.PartnerName;
                        col++; worksheet.Cells[row, col].Value = item.Address;
                        col++; worksheet.Cells[row, col].Value = item.RouteCode;
                        col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.Note2;
                        col++; worksheet.Cells[row, col].Value = item.Note1;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.Ton;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.VehicleCode;
                        col++; worksheet.Cells[row, col].Value = item.DriverName;
                        col++; worksheet.Cells[row, col].Value = item.ETD;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DNCode;
                        col++; worksheet.Cells[row, col].Value = item.RequestDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.RequestDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatHHMM);
                        col++; worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.Description;

                        stt++;
                        row++;
                    }

                    row++;

                    package.Save();
                }
                result = filePath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentPOD> OPS_DIAppointment_Route_PODExcelCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTODIAppointmentPOD>();

                var lst = new List<DTODIAppointmentPOD>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    lst = sv.DIAppointment_Route_PODList(dtfrom, dtto);
                });

                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {
                            int col = 0;
                            int row = 0;
                            for (row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                col = 1; string strTOMasterCode = ExcelHelper.GetValue(worksheet, row, col);
                                col = 8; string strSOCode = ExcelHelper.GetValue(worksheet, row, col);
                                col = 9; string strNote2 = ExcelHelper.GetValue(worksheet, row, col);
                                col = 12; string strVehicleCode = ExcelHelper.GetValue(worksheet, row, col);
                                col = 10; string strNote1 = ExcelHelper.GetValue(worksheet, row, col);
                                col = 15; string strDNCode = ExcelHelper.GetValue(worksheet, row, col);
                                col = 18; string strOrderCode = ExcelHelper.GetValue(worksheet, row, col);

                                var queryOrder = lst.Where(c => c.TOMasterCode == strTOMasterCode && c.OrderCode == strOrderCode && c.SOCode == strSOCode);
                                var querySO = lst.Where(c => c.TOMasterCode == strTOMasterCode && c.SOCode != string.Empty && c.SOCode == strSOCode);
                                var queryTO = lst.Where(c => c.TOMasterCode == strTOMasterCode && c.OrderCode == strOrderCode && c.SOCode != string.Empty);

                                if (queryOrder.Count() > 0 || querySO.Count() > 0)
                                {
                                    if (queryOrder.Count() > 0)
                                    {
                                        foreach (var obj in queryOrder)
                                        {
                                            obj.Note1 = strNote1;
                                            obj.Note2 = strNote2;
                                            obj.DNCode = strDNCode;
                                            obj.ExcelSuccess = true;
                                            obj.ExcelError = "";
                                            obj.ExcelRow = row;
                                            result.Add(obj);
                                        }
                                    }
                                    else
                                    {
                                        foreach (var obj in querySO)
                                        {
                                            obj.Note1 = strNote1;
                                            obj.Note2 = strNote2;
                                            obj.DNCode = strDNCode;
                                            obj.ExcelSuccess = true;
                                            obj.ExcelError = "";
                                            obj.ExcelRow = row;
                                            result.Add(obj);
                                        }
                                    }
                                }
                                else
                                {
                                    var obj = new DTODIAppointmentPOD();
                                    obj.ID = -1;
                                    obj.TOMasterCode = strTOMasterCode;
                                    obj.OrderCode = strOrderCode;
                                    obj.Note1 = strNote1;
                                    obj.Note2 = strNote2;
                                    obj.DNCode = strDNCode;
                                    obj.ExcelSuccess = false;
                                    obj.ExcelError = "Không có dữ liệu này trong file";
                                    obj.ExcelRow = row;
                                    result.Add(obj);
                                }
                            }

                            result = result.Distinct().ToList();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentPOD> OPS_DIAppointment_Route_QuickSearch(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentPOD>();
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_QuickSearch(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTODIAppointmentOrder OPS_DIAppointment_Route_QuickSearchGet(dynamic dynParam)
        {
            try
            {
                var result = default(DTODIAppointmentOrder);
                int id = Convert.ToInt32(dynParam.id.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_QuickSearchGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_QuickSearchApproved(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_QuickSearchApproved(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_QuickSearchUnApproved(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_QuickSearchUnApproved(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_WinVehicleSave(dynamic dynParam)
        {
            try
            {
                List<DTOFLMVehicle> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOFLMVehicle>>(dynParam.lst.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_WinVehicleSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTODIAppointmentOrder OPS_DIAppointment_Route_VehicleTimelineChange(dynamic dynParam)
        {
            try
            {
                DTOFLMAssetTimeSheet source = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMAssetTimeSheet>(dynParam.source.ToString());
                DTODIAppointmentOrder target = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.target.ToString());
                var result = default(DTODIAppointmentOrder);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleTimelineChange(source, target);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_OrderList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new List<DTODIAppointmentOrder>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_OrderList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentGroupProduct> OPS_DIAppointment_Route_OrderDetail(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentOrder> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentOrder>>(dynParam.lst.ToString());
                var result = new List<DTODIAppointmentGroupProduct>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_OrderDetail(lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_OrderDiv(dynamic dynParam)
        {
            try
            {
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                int div = Convert.ToInt32(dynParam.div.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_OrderDiv(item, div);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_OrderGroup(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentOrder> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentOrder>>(dynParam.lst.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_OrderGroup(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentGroupProduct> OPS_DIAppointment_Route_OrderDivCustomGet(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentGroupProduct>();
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_OrderDivCustomGet(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSCustomer> OPS_DIAppointment_Route_CustomerList()
        {
            try
            {
                var result = new List<CUSCustomer>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Appointment_Route_ListCustomer();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_OrderDNCodeChange(dynamic dynParam)
        {
            try
            {
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_OrderDNCodeChange(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_HasDNOrderListDN()
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_HasDNOrderListDN();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_HasDNOrderListByGroupID(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_HasDNOrderListByGroupID(lstid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_HasDNListGroupID(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_HasDNListGroupID(lstid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_HasDNOrderList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_HasDNOrderList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSVehicle> OPS_DIAppointment_Route_VehicleListVehicle()
        {
            try
            {
                var result = new List<DTOOPSVehicle>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_VehicleListVehicle();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSCustomer> OPS_DIAppointment_Route_VehicleListVendor()
        {
            try
            {
                var result = new List<CUSCustomer>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Appointment_Route_ListVendor();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_HasDNDelete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_HasDNDelete(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_HasDNSave(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentOrder> lstOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentOrder>>(dynParam.lstOrder.ToString());
                List<DTODIAppointmentRate> lstVehicle = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRate>>(dynParam.lstVehicle.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_HasDNSave(lstOrder, lstVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_HasDNApproved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_HasDNApproved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_HasDNUnApproved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_HasDNUnApproved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_HasDNList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_HasDNList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_NoDNList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_NoDNList(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_NoDNOrderList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_NoDNOrderList(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_NoDNSave(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentOrder> lstOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentOrder>>(dynParam.lstOrder.ToString());
                List<DTODIAppointmentRate> lstVehicle = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRate>>(dynParam.lstVehicle.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_NoDNSave(lstOrder, lstVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_FTL_NoDNList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_FTL_NoDNList(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_FTL_NoDNOrderList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_FTL_NoDNOrderList(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_FTL_NoDNSave(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentRate> lstVehicle = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRate>>(dynParam.lstVehicle.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_FTL_NoDNSave(lstVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_FTL_NoDNCancel(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentRate> lstVehicle = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRate>>(dynParam.lstVehicle.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_FTL_NoDNCancel(lstVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_FTL_NoDNSplit(dynamic dynParam)
        {
            try
            {
                int toMasterID = (int)dynParam.toMasterID;
                List<DTODIAppointmentOrder> dataGop = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentOrder>>(dynParam.dataGop.ToString());
                List<DTODIAppointmentRate> dataVehicle = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRate>>(dynParam.dataVehicle.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_FTL_NoDNSplit(toMasterID, dataGop, dataVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_RouteDN_OrderDNCodeChange(dynamic dynParam)
        {
            try
            {
                DTODIAppointmentOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentOrder>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_RouteDN_OrderDNCodeChange(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DIAppointment_RouteDN_OrderList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                int statusID = Convert.ToInt32(dynParam.statusID.ToString());
                DTOResult result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_RouteDN_OrderList(request, DateFrom, DateTo, lstCustomerID, statusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_RouteDN_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());

                DTOResult result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_RouteDN_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_RouteDN_Revert(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());

                DTOResult result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_RouteDN_Revert(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string DIAppointment_Route_ExcelConfirm(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                dtfrom = dtfrom.Date;
                dtto = dtto.Date.AddDays(1);

                string filePath = "/" + FolderUpload.Export + "file" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0, row = 0, stt = 0;

                    var lst = new List<DTOORDImportABA>();
                    ServiceFactory.SVOperation((ISVOperation sv) =>
                    {
                        lst = sv.DIAppointment_Route_ExcelConfirm(dtfrom, dtto);
                    });

                    row = 1;
                    col = 1; worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã KH";
                    col++; worksheet.Cells[row, col].Value = "Tên Khách hàng";
                    col++; worksheet.Cells[row, col].Value = "Ngày vận hành";
                    col++; worksheet.Cells[row, col].Value = "Mã chuyến hàng";
                    col++; worksheet.Cells[row, col].Value = "Điểm bốc hàng";
                    int colStockCode = col;
                    col++; worksheet.Cells[row, col].Value = "Loại hàng";
                    int colGroupOfProductCode = col;
                    col++; worksheet.Cells[row, col].Value = "Nhiệt độ bảo quản";
                    col++; worksheet.Cells[row, col].Value = "Tên Hàng";
                    int colProductCode = col;
                    col++; worksheet.Cells[row, col].Value = "Loại xe";
                    col++; worksheet.Cells[row, col].Value = "Thời gian nhận hàng dự kiến(ngày giờ)";
                    col++; worksheet.Cells[row, col].Value = "Số SO";
                    col++; worksheet.Cells[row, col].Value = "Mã NPP";
                    col++; worksheet.Cells[row, col].Value = "Tên NPP";
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ Nhận hàng NPP";
                    col++; worksheet.Cells[row, col].Value = "Thứ tự Trả Hàng";
                    col++; worksheet.Cells[row, col].Value = "Thùng/Khay";
                    col++; worksheet.Cells[row, col].Value = "KL(Tấn)";
                    col++; worksheet.Cells[row, col].Value = "TT(M3)";
                    col++; worksheet.Cells[row, col].Value = "Ghi chú";
                    col++; worksheet.Cells[row, col].Value = "Số xe";
                    col++; worksheet.Cells[row, col].Value = "Tài xế";
                    col++; worksheet.Cells[row, col].Value = "Điện thoại liên lạc";
                    col++; worksheet.Cells[row, col].Value = "Ghi chú ABA";
                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite);

                    row++;
                    stt = 1;
                    foreach (var item in lst)
                    {
                        col = 1; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                        col++; worksheet.Cells[row, col].Value = item.CustomerName;
                        col++; worksheet.Cells[row, col].Value = item.ETDDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.OrderCode;
                        col++;
                        col++;
                        col++; worksheet.Cells[row, col].Value = item.UserDefine1;
                        col++;
                        col++; worksheet.Cells[row, col].Value = item.GroupOfVehicleCode;
                        col++; worksheet.Cells[row, col].Value = item.ETDTime;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatHHMM);

                        int colDetail = col;

                        col += 9; worksheet.Cells[row, col].Value = item.Note;
                        col++; worksheet.Cells[row, col].Value = item.VehicleCode;
                        col++; worksheet.Cells[row, col].Value = item.DriverName1;
                        col++; worksheet.Cells[row, col].Value = item.DriverTel1;

                        if (item.Details != null)
                        {
                            foreach (var detail in item.Details)
                            {
                                col = colDetail;

                                worksheet.Cells[row, colStockCode].Value = detail.StockCode;
                                worksheet.Cells[row, colGroupOfProductCode].Value = detail.GroupOfProductCode;
                                worksheet.Cells[row, colProductCode].Value = detail.ProductCode;

                                col++; worksheet.Cells[row, col].Value = detail.SOCode;
                                col++; worksheet.Cells[row, col].Value = detail.DistributorCode;
                                col++; worksheet.Cells[row, col].Value = detail.DistributorName;
                                col++; worksheet.Cells[row, col].Value = detail.Address;
                                col++;
                                col++; worksheet.Cells[row, col].Value = detail.Quantity;
                                ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                col++; worksheet.Cells[row, col].Value = detail.Ton;
                                ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                                col++; worksheet.Cells[row, col].Value = detail.CBM;
                                ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);

                                row++;
                            }
                        }

                        stt++;
                    }

                    package.Save();
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMailVendor> OPS_DIAppointment_Route_SendToTender(dynamic dynParam)
        {
            try
            {
                List<DTOMailVendor> result = new List<DTOMailVendor>();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                List<DTODIAppointmentRouteTender> lstTender = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRouteTender>>(dynParam.lstTender.ToString());
                double RateTime = Convert.ToDouble(dynParam.RateTime.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_SendToTender(lst, lstTender, RateTime);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMailVendor> OPS_DIAppointment_Route_TenderReject(dynamic dynParam)
        {
            try
            {
                List<DTOMailVendor> result = new List<DTOMailVendor>();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                DTODIAppointmentRouteTenderReject item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentRouteTenderReject>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_TenderReject(lst, item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_TenderApproved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_TenderApproved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_TenderSave(dynamic dynParam)
        {
            try
            {
                List<DTODIAppointmentRate> lstVehicle = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRate>>(dynParam.lstVehicle.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_TenderSave(lstVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_TenderRequestList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_TenderRequestList(request, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_TenderAcceptList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_TenderAcceptList(request, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_TenderRejectList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_TenderRejectList(request, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_TenderRequestOrderList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_TenderRequestOrderList(request, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_TenderAcceptOrderList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_TenderAcceptOrderList(request, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTODIAppointmentOrder> OPS_DIAppointment_Route_TenderRejectOrderList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentOrder>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_TenderRejectOrderList(request, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DIAppointment_Route_ReasonList()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.RejectReason_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_Route_SendMailToTender(dynamic dynParam)
        {
            try
            {
                List<DTOMailVendor> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMailVendor>>(dynParam.lst.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_SendMailToTender(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTODIAppointmentRate> OPS_DIAppointment_Route_MasterList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTODIAppointmentRate>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_MasterList(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTODIAppointmentRate OPS_DIAppointment_Route_Master_OfferPL(dynamic dynParam)
        {
            try
            {
                var result = new DTODIAppointmentRate();
                DTODIAppointmentRate master = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentRate>(dynParam.master.ToString());
                List<DTODIAppointmentOrder> lstOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentOrder>>(dynParam.lstOrder.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_Master_OfferPL(master, lstOrder);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string OPS_DIAppointment_BookingConfirmation_Excel(dynamic dynParam)
        {
            try
            {
                CATFile itemfile = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.itemfile.ToString());
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                List<int> lstid = new List<int>();
                int customerID = Convert.ToInt32(dynParam.customerID.ToString());
                lstid.Add(customerID);

                dtfrom = dtfrom.Date;
                dtto = dtto.Date.AddDays(1);
                var data = new List<DTOOPSBookingConfirm>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    data = sv.DIAppointment_Route_BookingConfirmation(lstid, dtfrom, dtto);
                });

                var newfile = "/" + FolderUpload.Export + itemfile.FileName.Replace(itemfile.FileExt, "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + itemfile.FilePath)))
                    System.IO.File.Copy(HttpContext.Current.Server.MapPath("/" + itemfile.FilePath), HttpContext.Current.Server.MapPath(newfile), true);
                else
                    throw new Exception("File không tồn tại!");

                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(newfile));
                using (var package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                    if (worksheet != null)
                    {
                        Dictionary<int, PropertyInfo> dicProp = new Dictionary<int, PropertyInfo>();
                        List<int> lstCopy = new List<int>();
                        var typeProp = typeof(DTOOPSBookingConfirm);

                        int row = 0, col = 0, stt = 0, rowStart = 0, colStart = 0;
                        for (row = 1; row <= worksheet.Dimension.End.Row; row++)
                        {
                            for (col = 1; col <= worksheet.Dimension.End.Column; col++)
                            {
                                var str = ExcelHelper.GetValue(worksheet, row, col);
                                if (str == "[STT]")
                                {
                                    rowStart = row;
                                    colStart = col;
                                }
                                else if (rowStart > 0 && colStart > 0)
                                {
                                    if (!string.IsNullOrEmpty(str) && str.StartsWith("[") && str.EndsWith("]"))
                                    {
                                        if (str == "[]")
                                        {
                                            lstCopy.Add(col);
                                        }
                                        else
                                        {
                                            str = str.Substring(1, str.Length - 2);
                                            try
                                            {
                                                var prop = typeProp.GetProperty(str);
                                                if (prop != null)
                                                    dicProp.Add(col, prop);
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }

                            if (rowStart > 0) break;
                        }

                        stt = 1;
                        row = rowStart + 1;
                        col = colStart;
                        if (data.Count > 0)
                        {
                            foreach (var item in data)
                            {
                                worksheet.Cells[row, col].Value = stt;
                                ExcelHelper.CopyStyle(worksheet, rowStart, col, row, col);

                                foreach (var prop in dicProp)
                                {
                                    var val = prop.Value.GetValue(item);
                                    if (val != null)
                                        worksheet.Cells[row, prop.Key].Value = val;
                                    else
                                        worksheet.Cells[row, prop.Key].Value = null;
                                    ExcelHelper.CopyStyle(worksheet, rowStart, prop.Key, row, prop.Key);
                                }
                                foreach (var colCopy in lstCopy)
                                {
                                    worksheet.Cells[row, colCopy].Value = worksheet.Cells[rowStart + 1, colCopy].Value;
                                    ExcelHelper.CopyStyle(worksheet, rowStart, colCopy, row, colCopy);
                                }

                                row++;
                                stt++;
                            }
                        }
                    }
                    package.Save();
                }
                return newfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DIAppointment_BookingConfirmation_Read(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                if (dynParam.dtfrom != null && dynParam.dtto != null)
                {
                    DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                    DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                    int customerID = Convert.ToInt32(dynParam.customerID.ToString());
                    string request = Convert.ToString(dynParam.request.ToString());
                    ServiceFactory.SVOperation((ISVOperation sv) =>
                    {
                        result = sv.DIAppointment_Route_BookingConfirmation_Read(request, customerID, dtfrom, dtto);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region DIPacket

        [HttpPost]
        public DTOResult OPS_DI_Tendering_Packet_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_Packet_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_VEN_Tendering_Rate_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                bool? isAccept = null;
                if (dynParam.isAccept.ToString() != "")
                    isAccept = (bool?)dynParam.isAccept;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_VEN_Tendering_Rate_List(request, isAccept);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_VEN_Tendering_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int packetDetailID = (int)dynParam.packetDetailID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_VEN_Tendering_GroupProduct_List(request, packetDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_PacketDetail_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int packetID = (int)dynParam.packetID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_PacketDetail_List(request, packetID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDIPacketDetailRate OPS_DI_VEN_2View_Get(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDIPacketDetailRate();
                int packetDetailRateID = (int)dynParam.ID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_VEN_2View_Get(packetDetailRateID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_Packet_Save(dynamic dynParam)
        {
            try
            {
                DTOOPSDIPacket item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDIPacket>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_Packet_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_Packet_CreateViaSetting(dynamic dynParam)
        {
            try
            {
                int sID = (int)dynParam.sID;
                string name = dynParam.name.ToString();
                List<int> dataGop = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataGop.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_Packet_CreateViaSetting(sID, name, dataGop);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_PacketGroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int packetID = (int)dynParam.packetID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_PacketGroupProduct_List(request, packetID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDIPacket OPS_DI_Tendering_Packet_Get(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDIPacket();
                int packetID = (int)dynParam.packetID;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_Packet_Get(packetID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_PacketGroupProduct_Save(dynamic dynParam)
        {
            try
            {
                int packetID = (int)dynParam.packetID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_PacketGroupProduct_Save(packetID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_VEN_2View_TOMaster_Delete(dynamic dynParam)
        {
            try
            {
                int packetID = (int)dynParam.packetID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_VEN_2View_TOMaster_Delete(packetID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_PacketGroupProduct_Remove(dynamic dynParam)
        {
            try
            {
                int packetID = (int)dynParam.packetID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_PacketGroupProduct_Remove(packetID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_PacketRate_Remove(dynamic dynParam)
        {
            try
            {
                int packetID = (int)dynParam.packetID;
                DTOOPSDIPacketRate data = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDIPacketRate>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_PacketRate_Remove(packetID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_Tendering_Vendor_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_Tendering_Vendor_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_PacketRate_Save(dynamic dynParam)
        {
            try
            {
                int packetID = (int)dynParam.packetID;
                DTOOPSDIPacketRate data = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDIPacketRate>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_PacketRate_Save(packetID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_VEN_Tendering_Rate_Accept(dynamic dynParam)
        {
            try
            {
                DTOOPSDIPacketDetailRate data = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDIPacketDetailRate>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_VEN_Tendering_Rate_Accept(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_VEN_Tendering_Rate_Reject(dynamic dynParam)
        {
            try
            {
                DTOOPSDIPacketDetailRate data = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDIPacketDetailRate>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_VEN_Tendering_Rate_Reject(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_VEN_Tendering_Rate_AcceptPart(dynamic dynParam)
        {
            try
            {
                int packetDetailRateID = (int)dynParam.packetDetailRateID;
                List<int> dataGop = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_VEN_Tendering_Rate_AcceptPart(packetDetailRateID, dataGop);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_Setting_Packet_Save(dynamic dynParam)
        {
            try
            {
                int fID = (int)dynParam.fID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_Setting_Packet_Save(fID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_Setting_Packet_Order_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int sID = (int)dynParam.sID;
                var result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_Setting_Packet_Order_List(request, sID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_Packet_Send(dynamic dynParam)
        {
            try
            {
                int packetID = (int)dynParam.packetID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_Packet_Send(packetID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_Packet_Delete(dynamic dynParam)
        {
            try
            {
                int packetID = (int)dynParam.packetID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_Packet_Delete(packetID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_PacketGroupProduct_NotIn_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int packetID = (int)dynParam.packetID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_PacketGroupProduct_NotIn_List(request, packetID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_PacketRate_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int packetID = (int)dynParam.packetID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_PacketRate_List(request, packetID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult TruckByVendorID_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int vendorID = (int)dynParam.vendorID;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.TruckByVendorID_List(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_VEN_2View_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int packetDetailID = (int)dynParam.packetID;
                bool hasMaster = (bool)dynParam.hasMaster;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_VEN_2View_GroupProduct_List(request, packetDetailID, hasMaster);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_VEN_2View_TOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int packetDetailID = (int)dynParam.packetID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_VEN_2View_TOMaster_List(request, packetDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSSettingsTenderLTL> OPS_DI_Tendering_Setting_Packet_List(dynamic dynParam)
        {
            try
            {
                var result = new List<CUSSettingsTenderLTL>();
                int fID = (int)dynParam.fID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_Setting_Packet_List(fID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_VEN_2View_TOMaster_Save(dynamic dynParam)
        {
            try
            {
                int packetDetailID = (int)dynParam.packetID;
                List<DTOOPSDIPacketTOMaster> dataMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSDIPacketTOMaster>>(dynParam.dataMaster.ToString());
                List<DTOOPSDIPacketTOGroupProduct> dataGop = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSDIPacketTOGroupProduct>>(dynParam.dataGop.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_VEN_2View_TOMaster_Save(packetDetailID, dataMaster, dataGop);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_VEN_Tendering_Packet_Confirm(dynamic dynParam)
        {
            try
            {
                int packetDetailID = (int)dynParam.packetID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_VEN_Tendering_Packet_Confirm(packetDetailID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region COAppointment

        [HttpPost]
        public List<DTOOPSCOTOContainer> OPS_COAppointment_ContainerHasMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSCOTOContainer>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.COAppointment_2View_ContainerHasMaster_List(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSCustomer> OPS_COAppointment_Route_CustomerList()
        {
            try
            {
                var result = new List<CUSCustomer>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Appointment_Route_ListCustomer();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCOTOContainer> OPS_COAppointment_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSCOTOContainer>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.COAppointment_2View_Container_List(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCOTOMaster> OPS_COAppointment_Master_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSCOTOMaster>();
                string request = dynParam.request.ToString();
                DateTime DateFrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime DateTo = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.COAppointment_2View_Master_List(request, lstCustomerID, DateFrom, DateTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSCustomer> OPS_COAppointment_Route_VehicleListVendor()
        {
            try
            {
                List<CUSCustomer> result = new List<CUSCustomer>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Appointment_Route_ListVendor();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_COAppointment_Route_VehicleListTractor(dynamic dynParam)
        {
            try
            {
                int? vendorid = (int?)dynParam.vendorid;
                var result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.TractorByVendorID_List(vendorid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_COAppointment_Route_VehicleListRomooc(dynamic dynParam)
        {
            try
            {
                int? vendorid = (int?)dynParam.vendorid;
                var result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.RomoocByVendorID_List(vendorid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void COAppointment_2View_Master_Update(dynamic dynParam)
        {
            try
            {
                List<DTOOPSCOTOMaster> dataMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCOTOMaster>>(dynParam.dataMaster.ToString());
                List<DTOOPSCOTOContainer> dataContainer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCOTOContainer>>(dynParam.dataContainer.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.COAppointment_2View_Master_Update(dataMaster, dataContainer);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void COAppointment_2View_Master_Delete(dynamic dynParam)
        {
            try
            {
                foreach (var x in dynParam.lst)
                {
                    try
                    {
                        int masterID = int.Parse(x.ToString());
                        ServiceFactory.SVOperation((ISVOperation sv) =>
                        {
                            sv.COAppointment_2View_Master_Delete(masterID);
                        });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void COAppointment_2View_Master_Tender(dynamic dynParam)
        {
            try
            {
                foreach (var x in dynParam.lst)
                {
                    try
                    {
                        int masterID = int.Parse(x.ToString());
                        ServiceFactory.SVOperation((ISVOperation sv) =>
                        {
                            sv.COAppointment_2View_Master_ToMON(masterID);
                        });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void COAppointment_2View_Master_Revert(dynamic dynParam)
        {
            try
            {
                foreach (var x in dynParam.lst)
                {
                    try
                    {
                        int masterID = int.Parse(x.ToString());
                        ServiceFactory.SVOperation((ISVOperation sv) =>
                        {
                            sv.COAppointment_2View_Master_ToOPS(masterID);
                        });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMailVendor> COAppointment_2View_Master_ToVendor(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOMailVendor>();
                List<int> dataMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataMaster.ToString());
                List<DTODIAppointmentRouteTender> dataRate = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRouteTender>>(dynParam.dataRate.ToString());
                double rTime = Convert.ToDouble(dynParam.rTime.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.COAppointment_2View_Master_ToVendor(dataMaster, dataRate, rTime);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void COAppointment_2View_Master_ToVendor_Email(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOMailVendor>();
                List<DTOMailVendor> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMailVendor>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.COAppointment_2View_Master_ToVendor_Email(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Barcode
        [HttpPost]
        public DTOBarcode OPS_DIAppointment_RouteBarcode_SOList(dynamic dynParam)
        {
            try
            {
                DTOBarcode result = new DTOBarcode();
                string Barcode = dynParam.Barcode.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_RouteBarcode_SOList(Barcode);
                });
                return result;
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public void OPS_DIAppointment_RouteBarcode_SOSave(dynamic dynParam)
        {
            try
            {
                List<DTOBarcodeGroup> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOBarcodeGroup>>(dynParam.lst.ToString());
                bool IsNote = Convert.ToBoolean(dynParam.IsNote.ToString());
                DTOResult result = new DTOResult();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_RouteBarcode_SOSave(lst, IsNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Optimizer

        #region Optimizer

        [HttpPost]
        public DTOResult Opt_Optimizer_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                bool isCo = (bool)dynParam.isCo;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optimizer_List(request, isCo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Optimizer_Save(dynamic dynParam)
        {
            try
            {
                DTOOPTOptimizer item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTOptimizer>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Optimizer_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPTOptimizer Opt_Optimizer_Get(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                DTOOPTOptimizer result = new DTOOPTOptimizer();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optimizer_Get(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Optimizer_Delete(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Optimizer_Delete(optimizerID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Optimizer_Run(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Optimizer_Run(optimizerID, 0);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Optimizer_Cal(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Optimizer_Cal(optimizerID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Optimizer_Out(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Optimizer_Out(optimizerID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string Opt_Optimizer_Run_Check_Setting()
        {
            try
            {
                string result = string.Empty;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optimizer_Run_Check_Setting();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string Opt_Optimizer_Run_Check_Vehicle(dynamic dynParam)
        {
            try
            {
                string result = string.Empty;
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optimizer_Run_Check_Vehicle(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string Opt_Optimizer_Run_Check_Location(dynamic dynParam)
        {
            try
            {
                string result = string.Empty;
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optimizer_Run_Check_Location(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPTLocationToLocation> Opt_Optimizer_Run_Get_LocationMatrix(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPTLocationToLocation>();
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optimizer_Run_Get_LocationMatrix(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Optimizer_Run_Update_LocationMatrix(dynamic dynParam)
        {
            try
            {
                DTOOPTLocationToLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTLocationToLocation>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Optimizer_Run_Update_LocationMatrix(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Container

        [HttpPost]
        public DTOResult Opt_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Container_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_Container_NotIn_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Container_NotIn_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Container_SaveList(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Container_SaveList(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Container_Update(dynamic dynParam)
        {
            try
            {
                DTOOPSCOTOContainer item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCOTOContainer>(dynParam.item.ToString());
                if (item.ETD == null || item.ETA == null || item.ETAStart == null || item.ETDStart == null)
                {
                    throw new Exception("Thời gian không đúng.");
                }
                if (item.ETD < item.ETDStart || item.ETA < item.ETAStart)
                {
                    throw new Exception("Thời gian không hợp lệ.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Container_Update(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Container_Remove(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Container_Remove(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GroupOfProduct

        [HttpPost]
        public DTOResult Opt_GroupOfProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_GroupOfProduct_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_GroupOfProduct_NotIn_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_GroupOfProduct_NotIn_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_GroupOfProduct_SaveList(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_GroupOfProduct_SaveList(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_GroupOfProduct_Remove(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_GroupOfProduct_Remove(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_GroupOfProduct_Update(dynamic dynParam)
        {
            try
            {
                DTOOPTOPSGroupProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTOPSGroupProduct>(dynParam.item.ToString());
                if (item.ETD == null || item.ETA == null || item.ETAStart == null || item.ETDStart == null)
                {
                    throw new Exception("Thời gian không đúng.");
                }
                if (item.ETD < item.ETDStart || item.ETA < item.ETAStart)
                {
                    throw new Exception("Thời gian không hợp lệ.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_GroupOfProduct_Update(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Vehicle

        [HttpPost]
        public DTOResult Opt_Vehicle_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Vehicle_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_Vehicle_NotIn_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Vehicle_NotIn_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Vehicle_SaveList(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Vehicle_SaveList(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Vehicle_Remove(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Vehicle_Remove(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Vehicle_Update(dynamic dynParam)
        {
            try
            {
                int vehicleID = (int)dynParam.vehicleID;
                int? romoocID = (int?)dynParam.romoocID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Vehicle_Update(vehicleID, romoocID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Vehicle_UpdateWeight(dynamic dynParam)
        {
            try
            {
                int vehicleID = (int)dynParam.vehicleID;
                double maxWeight = (double)dynParam.maxWeight;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Vehicle_UpdateWeight(vehicleID, maxWeight);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Romooc

        [HttpPost]
        public DTOResult Opt_Romooc_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Romooc_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_Romooc_NotIn_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Romooc_NotIn_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Romooc_SaveList(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Romooc_SaveList(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Romooc_Remove(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.OptimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Romooc_Remove(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Location

        [HttpPost]
        public DTOResult Opt_Location_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Location_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_Location_Require_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optLocationID = (int)dynParam.optLocationID;
                bool isSize = (bool)dynParam.isSize;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Location_Require_List(optLocationID, isSize);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Location_Require_Save(dynamic dynParam)
        {
            try
            {
                int optLocationID = (int)dynParam.optLocationID;
                DTOOPTLocationRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTLocationRequire>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Location_Require_Save(optLocationID, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Location_Require_Reset(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Location_Require_Reset(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Location_Require_Remove(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Location_Require_Remove(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region LocationToLocation

        [HttpPost]
        public DTOResult Opt_LocationMatrix_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_LocationMatrix_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_LocationMatrix_Refresh(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_LocationMatrix_Refresh(optimizerID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_LocationMatrix_Update(dynamic dynParam)
        {
            try
            {
                DTOOPTLocationToLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTLocationToLocation>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_LocationMatrix_Update(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Route

        [HttpPost]
        public DTOResult Opt_Routing_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Routing_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_Routing_Require_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optRouteID = (int)dynParam.optRouteID;
                bool isSize = (bool)dynParam.isSize;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Routing_Require_List(optRouteID, isSize);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Routing_Save(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                double time = (double)dynParam.Time;
                double distance = (double)dynParam.Distance;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Routing_Save(ID, time, distance);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Routing_Require_Save(dynamic dynParam)
        {
            try
            {
                int optRoutingID = (int)dynParam.optRoutingID;
                DTOOPTLocationRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTLocationRequire>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Routing_Require_Save(optRoutingID, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Routing_Require_Reset(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Routing_Require_Reset(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_Routing_Require_Remove(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_Routing_Require_Remove(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Master

        [HttpPost]
        public bool Opt_Optmizer_HasRun(dynamic dynParam)
        {
            try
            {
                var result = false;
                int optimizerID = (int)dynParam.optimizerID;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optmizer_HasRun(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Opt_Optmizer_HasSave(dynamic dynParam)
        {
            try
            {
                var result = false;
                int optimizerID = (int)dynParam.optimizerID;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optmizer_HasSave(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_COTOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_COTOMaster_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_COTOMaster_Change(dynamic dynParam)
        {
            try
            {
                DTOOPTCOTOMaster item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTCOTOMaster>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_COTOMaster_Change(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_COTOMaster_Delete(dynamic dynParam)
        {
            try
            {
                int optMasterID = (int)dynParam.optMasterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_COTOMaster_Delete(optMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_COTOMaster_Save(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_COTOMaster_Save(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_COTOMaster_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optMasterID = (int)dynParam.optMasterID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_COTOMaster_Container_List(request, optMasterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_COTOLocation_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_COTOLocation_List(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_DITOLocation_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_DITOLocation_List(optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_Optimizer_VehicleSchedule(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_Optimizer_VehicleSchedule(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_COTOContainer_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_COTOContainer_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_DITOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_DITOMaster_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_DITOMaster_Delete(dynamic dynParam)
        {
            try
            {
                int optMasterID = (int)dynParam.optMasterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_DITOMaster_Delete(optMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_DITOMaster_Save(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_DITOMaster_Save(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_DITOGroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_DITOGroupProduct_List(request, optimizerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_DITOMaster_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optMasterID = (int)dynParam.optMasterID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_DITOMaster_GroupProduct_List(request, optMasterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string Opt_Optimizer_GetJsonSet(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;

                var data = new DTOOPTJsonData();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    data = sv.Opt_Optimizer_GetDataRun(optimizerID);
                });

                string folderPath = HttpContext.Current.Server.MapPath("/Uploads/temp/Optimize_" + optimizerID.ToString());
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var file0 = "/Uploads/temp/Optimize_" + optimizerID.ToString() + ".zip"; ;

                var file1 = folderPath + "/Locations.json";
                var file2 = folderPath + "/LocationToLocationMatrix.json";
                var file3 = folderPath + "/orders.json";
                var file4 = folderPath + "/Rates.json";
                var file5 = folderPath + "/vehicles.json";
                var file6 = folderPath + "/VehicleTypes.json";

                System.IO.File.WriteAllText(file1, data.Locations);
                System.IO.File.WriteAllText(file2, data.LocationToLocationMatrix);
                System.IO.File.WriteAllText(file3, data.Orders);
                System.IO.File.WriteAllText(file4, data.Rates);
                System.IO.File.WriteAllText(file5, data.Vehicles);
                System.IO.File.WriteAllText(file6, data.VehicleTypes);

                FastZip zip = new FastZip();
                zip.CreateZip(HttpContext.Current.Server.MapPath(file0), folderPath, false, null);

                return file0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 2ViewDI

        [HttpPost]
        public DTOResult Opt_2ViewDI_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();
                bool hasMaster = (bool)dynParam.hasMaster;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_2ViewDI_GroupProduct_List(request, optimizerID, hasMaster);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_2ViewDI_Master_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_2ViewDI_Master_List(request, optimizerID);
                });
                foreach (DTOOPTDITOMaster x in result.Data)
                {
                    try
                    {
                        string[] words = x.Note.Split(' ');
                        x.CreateSortOrder = int.Parse(words[1]);
                    }
                    catch (Exception ex)
                    { }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool DIAppointment_Route_Master_CheckDriver(dynamic dynParam)
        {
            try
            {
                bool result = false;
                int vehicleID = (int)dynParam.vehicleID;
                int driverID = (int)dynParam.driverID;
                DateTime etd = Convert.ToDateTime(dynParam.etd.ToString());
                DateTime eta = Convert.ToDateTime(dynParam.eta.ToString());

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.DIAppointment_Route_Master_CheckDriver(vehicleID, driverID, etd, eta);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_2ViewDI_SaveList(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<DTOOPTDITOMaster> dataMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPTDITOMaster>>(dynParam.dataMaster.ToString());
                List<DTOOPTDITOGroupOfProduct> dataGroupProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPTDITOGroupOfProduct>>(dynParam.dataContainer.ToString());
                var sMaster = dataMaster.Where(c => (c.ID > 0 && c.HasChanged == true) || (c.ID < 1 && dataGroupProduct.Count(o => o.CreateSortOrder == c.CreateSortOrder) > 0)).ToList();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_2ViewDI_SaveList(optimizerID, sMaster, dataGroupProduct);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_2ViewDI_Delete(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_2ViewDI_Delete(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void DIAppointment_Route_Master_ChangeMode(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                bool fromFTL = (bool)dynParam.fromFTL;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.DIAppointment_Route_Master_ChangeMode(data, fromFTL);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 2ViewCO

        [HttpPost]
        public DTOResult Opt_2ViewCO_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();
                bool hasMaster = (bool)dynParam.hasMaster;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_2ViewCO_Container_List(request, optimizerID, hasMaster);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Opt_2View_COTOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                int optimizerID = (int)dynParam.optimizerID;
                string request = dynParam.request.ToString();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.Opt_COTOMaster_List(request, optimizerID);
                });
                foreach (DTOOPTCOTOMaster x in result.Data)
                {
                    try
                    {
                        string[] words = x.Note.Split(' ');
                        x.CreateSortOrder = int.Parse(words[1]);
                    }
                    catch (Exception ex)
                    { }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void Opt_2ViewCO_SaveList(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<DTOOPTCOTOMaster> dataMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPTCOTOMaster>>(dynParam.dataMaster.ToString());
                List<DTOOPTCOTOContainer> dataContainer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPTCOTOContainer>>(dynParam.dataContainer.ToString());
                var sMaster = dataMaster.Where(c => (c.ID > 0 && c.HasChanged == true) || (c.ID < 1 && dataContainer.Count(o => o.CreateSortOrder == c.CreateSortOrder) > 0)).ToList();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_2ViewCO_SaveList(optimizerID, sMaster, dataContainer);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Opt_2ViewCO_Delete(dynamic dynParam)
        {
            try
            {
                int optimizerID = (int)dynParam.optimizerID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.Opt_2ViewCO_Delete(optimizerID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region SettingTenderLTL

        [HttpPost]
        public List<CUSSettingsTenderLTL> OPS_DI_Tendering_Setting_List(dynamic dynParam)
        {
            try
            {
                var result = new List<CUSSettingsTenderLTL>();
                int fID = (int)dynParam.fID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_Setting_List(fID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CUSSettingsTenderLTL OPS_DI_Tendering_Setting_Get(dynamic dynParam)
        {
            try
            {
                var result = new CUSSettingsTenderLTL();
                int sID = (int)dynParam.sID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Tendering_Setting_Get(sID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_Setting_Save(dynamic dynParam)
        {
            try
            {
                CUSSettingsTenderLTL item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsTenderLTL>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_Setting_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Tendering_Setting_Delete(dynamic dynParam)
        {
            try
            {
                int sID = (int)dynParam.sID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Tendering_Setting_Delete(sID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_Setting_Location_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_DI_Tendering_Setting_Location_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_Setting_Routing_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_DI_Tendering_Setting_Routing_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_Setting_GroupLocation_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_DI_Tendering_Setting_GroupLocation_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_Setting_Customer_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_DI_Tendering_Setting_Customer_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Tendering_Setting_Vendor_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_DI_Tendering_Setting_Vendor_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #region SettingTenderFCL
        [HttpPost]
        public DTOResult OPS_CO_Tendering_Setting_Service_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Tendering_Setting_Service_List(request, dataExist);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPS_CO_Tendering_Setting_Routing_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_CO_Tendering_Setting_Routing_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPS_CO_Tendering_Setting_Customer_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_CO_Tendering_Setting_Customer_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPS_CO_Tendering_Setting_Vendor_List(dynamic dynParam)
        {
            try
            {
                var Result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> dataExist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataExist.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    Result = sv.OPS_CO_Tendering_Setting_Vendor_List(request, dataExist);
                });
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPS_CO_Tendering_Setting_FCL_Packet_Save(dynamic dynParam)
        {
            try
            {
                int fID = (int)dynParam.fID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Tendering_Setting_FCL_Packet_Save(fID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public List<CUSSettingsTenderFCL> OPS_CO_Tendering_Setting_FCL_List(dynamic dynParam)
        {
            try
            {
                var result = new List<CUSSettingsTenderFCL>();
                int fID = (int)dynParam.fID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Tendering_Setting_FCL_List(fID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Tendering_Setting_FCL_Save(dynamic dynParam)
        {
            try
            {
                CUSSettingsTenderFCL item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsTenderFCL>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Tendering_Setting_FCL_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Tendering_Setting_FCL_Delete(dynamic dynParam)
        {
            try
            {
                int sID = (int)dynParam.sID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Tendering_Setting_FCL_Delete(sID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CUSSettingsTenderFCL OPS_CO_Tendering_Setting_FCL_Get(dynamic dynParam)
        {
            try
            {
                var result = new CUSSettingsTenderFCL();
                int sID = (int)dynParam.sID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Tendering_Setting_FCL_Get(sID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NewDI - Import
        const int digitsOfRound = 6;

        [HttpPost]
        public DTOResult OPS_DI_Import_Packet_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                bool isCreated = (bool)dynParam.IsCreated;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_List(request, isCreated);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Import_Packet_Setting_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Import_Packet_TOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_TOMaster_List(request, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Import_Packet_GroupProduct_ByMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int masterID = (int)dynParam.masterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_GroupProduct_ByMaster_List(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Import_Packet_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_GroupProduct_List(request, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSSettingPlan> OPS_DI_Import_Packet_SettingPlan(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCUSSettingPlan>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_SettingPlan();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDIImportPacket OPS_DI_Import_Packet_Get(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDIImportPacket();
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_Get(pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Import_Packet_ORDGroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = default(DTOResult);
                int pID = (int)dynParam.pID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_ORDGroupProduct_List(request, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_DI_Import_Packet_ORDGroupProduct_NotIn_List(dynamic dynParam)
        {
            try
            {
                var result = default(DTOResult);
                int sID = (int)dynParam.sID;
                int pID = (int)dynParam.pID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_ORDGroupProduct_NotIn_List(request, sID, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string OPS_DI_Import_Packet_DownLoad(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                var sID = (int)dynParam.sID;
                var pID = (int)dynParam.pID;
                var objSetting = new DTOCUSSettingPlan();
                var objSettingOrder = new DTOCUSSettingOrder();
                var dataOrder = new DTOORDOrder_ImportCheck();
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                var dataExport = new List<DTOOPSDIImportPacket_GroupProductExport>();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    objSetting = sv.OPS_DI_Import_Packet_Setting_Get(sID);
                    dataExport = sv.OPS_DI_Import_Packet_ORDGroupProductExport_List(pID, data);
                });

                if (objSetting != null && objSetting.CUSSettingOrderID > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        objSettingOrder = sv.ORDOrder_Excel_Setting_Get(objSetting.CUSSettingOrderID);
                    });
                    if (objSettingOrder == null)
                        throw new Exception("Không tìm thấy thiết lập đơn hàng.");
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        dataOrder = sv.ORDOrder_Excel_Import_Data(objSettingOrder.CustomerID);
                    });
                    if (objSetting.FileID > 0)
                    {
                        string[] name = objSetting.FileName.Split('.').Reverse().Skip(1).Reverse().ToArray();
                        result = "/" + FolderUpload.Export + string.Join(".", name) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        result = result.Replace("+", "");
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + objSetting.FilePath)))
                        {
                            System.IO.File.Copy(HttpContext.Current.Server.MapPath("/" + objSetting.FilePath), HttpContext.Current.Server.MapPath(result), true);
                        }
                        else
                        {
                            throw new Exception("Không tìm thấy file mẫu!");
                        }
                    }
                    else
                    {
                        throw new Exception("Chưa thiết lập file mẫu!");
                    }

                    FileInfo exportFile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                    using (var package = new ExcelPackage(exportFile))
                    {
                        ExcelWorksheet ws = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (ws != null)
                        {
                            var sValue = new List<string>(new string[]{ "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart",
                                                  "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID" });

                            //Empty WS
                            var iRow = ws.Dimension.End.Row;
                            if (iRow > objSettingOrder.RowStart)
                            {
                                for (var row = iRow; row >= objSettingOrder.RowStart; row--)
                                {
                                    ws.DeleteRow(row);
                                }
                            }

                            var cRow = objSettingOrder.RowStart;
                            List<string> timeProps = new List<string>(new string[] { "RequestTime", "ETARequestTime", "TimeGetEmpty", "TimeReturnEmpty" });
                            if (objSettingOrder.HasStock)
                            {
                                var dataGop = dataExport.GroupBy(c => new { c.OrderCode, c.GroupProductCode, c.Packing, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                                foreach (var gop in dataGop)
                                {
                                    int max = 1;
                                    var item = gop.FirstOrDefault();
                                    foreach (var sto in objSettingOrder.ListStock)
                                    {
                                        var o = gop.Count(c => c.StockID == sto.StockID);
                                        if (o > max)
                                            max = o;
                                    }
                                    var dataContains = new List<int>();
                                    for (var i = 0; i < max; i++)
                                    {
                                        foreach (var prop in objSettingOrder.GetType().GetProperties())
                                        {
                                            try
                                            {
                                                var p = prop.Name;
                                                if (!sValue.Contains(p))
                                                {
                                                    var v = (int)prop.GetValue(objSettingOrder, null);
                                                    var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                    var txt = string.Empty;
                                                    if (val != null)
                                                    {
                                                        if (val.GetType() == typeof(DateTime))
                                                        {
                                                            if (timeProps.Contains(p))
                                                            {
                                                                txt = String.Format("{0:HH:mm}", val);
                                                            }
                                                            else
                                                            {
                                                                txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                            }
                                                        }
                                                        else if (val.GetType() == typeof(TimeSpan))
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                        else
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                    }
                                                    ws.Cells[cRow, v].Value = txt;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        if (objSetting.LocationToProvince > 0)
                                        {
                                            ws.Cells[cRow, objSetting.LocationToProvince].Value = item.LocationToProvince;
                                        }
                                        if (objSetting.LocationToDistrict > 0)
                                        {
                                            ws.Cells[cRow, objSetting.LocationToDistrict].Value = item.LocationToDistrict;
                                        }
                                        foreach (var stock in objSettingOrder.ListStock)
                                        {
                                            var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && !dataContains.Contains(c.ID));
                                            if (objGopInStock != null)
                                            {
                                                dataContains.Add(objGopInStock.ID);
                                                foreach (var prop in stock.GetType().GetProperties())
                                                {
                                                    try
                                                    {
                                                        var p = prop.Name;
                                                        if (p != "StockID")
                                                        {
                                                            var v = (int)prop.GetValue(stock, null);
                                                            var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                            if (val != null)
                                                            {
                                                                ws.Cells[cRow, v].Value = val.ToString();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                        cRow++;
                                    }
                                }
                            }
                            else if (objSettingOrder.HasStockProduct)
                            {
                                var dataGop = dataExport.GroupBy(c => new { c.OrderCode, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                                foreach (var gop in dataGop)
                                {
                                    int max = 1;
                                    var item = gop.FirstOrDefault();
                                    foreach (var sto in objSettingOrder.ListStockWithProduct)
                                    {
                                        var o = gop.Count(c => c.StockID == sto.StockID && c.GroupProductID == sto.GroupOfProductID && c.PackingID == sto.ProductID);
                                        if (o > max)
                                            max = o;
                                    }
                                    var dataContains = new List<int>();
                                    for (var i = 0; i < max; i++)
                                    {
                                        foreach (var prop in objSettingOrder.GetType().GetProperties())
                                        {
                                            try
                                            {
                                                var p = prop.Name;
                                                if (!sValue.Contains(p))
                                                {
                                                    var v = (int)prop.GetValue(objSettingOrder, null);
                                                    var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                    var txt = string.Empty;
                                                    if (val != null)
                                                    {
                                                        if (val.GetType() == typeof(DateTime))
                                                        {
                                                            if (timeProps.Contains(p))
                                                            {
                                                                txt = String.Format("{0:HH:mm}", val);
                                                            }
                                                            else
                                                            {
                                                                txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                            }
                                                        }
                                                        else if (val.GetType() == typeof(TimeSpan))
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                        else
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                    }
                                                    ws.Cells[cRow, v].Value = txt;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        if (objSetting.LocationToProvince > 0)
                                        {
                                            ws.Cells[cRow, objSetting.LocationToProvince].Value = item.LocationToProvince;
                                        }
                                        if (objSetting.LocationToDistrict > 0)
                                        {
                                            ws.Cells[cRow, objSetting.LocationToDistrict].Value = item.LocationToDistrict;
                                        }
                                        foreach (var stock in objSettingOrder.ListStockWithProduct)
                                        {
                                            var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && c.GroupProductID == stock.GroupOfProductID && c.PackingID == stock.ProductID && !dataContains.Contains(c.ID));
                                            if (objGopInStock != null)
                                            {
                                                dataContains.Add(objGopInStock.ID);
                                                foreach (var prop in stock.GetType().GetProperties())
                                                {
                                                    try
                                                    {
                                                        var p = prop.Name;
                                                        if (p != "StockID" && p != "GroupOfProductID" && p != "ProductID")
                                                        {
                                                            var v = (int)prop.GetValue(stock, null);
                                                            var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                            if (val != null)
                                                            {
                                                                ws.Cells[cRow, v].Value = val.ToString();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                        cRow++;
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in dataExport)
                                {
                                    item.Quantity_SKU = item.Quantity;
                                    item.Ton_SKU = item.Ton;
                                    item.CBM_SKU = item.CBM;
                                    foreach (var prop in objSettingOrder.GetType().GetProperties())
                                    {
                                        try
                                        {
                                            var p = prop.Name;
                                            if (!sValue.Contains(p))
                                            {
                                                var v = (int)prop.GetValue(objSettingOrder, null);
                                                var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                var txt = string.Empty;
                                                if (val != null)
                                                {
                                                    if (val.GetType() == typeof(DateTime))
                                                    {
                                                        if (timeProps.Contains(p))
                                                        {
                                                            txt = String.Format("{0:HH:mm}", val);
                                                        }
                                                        else
                                                        {
                                                            txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                        }
                                                    }
                                                    else if (val.GetType() == typeof(TimeSpan))
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                    else
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                }
                                                ws.Cells[cRow, v].Value = txt;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                    if (objSetting.LocationToProvince > 0)
                                    {
                                        ws.Cells[cRow, objSetting.LocationToProvince].Value = item.LocationToProvince;
                                    }
                                    if (objSetting.LocationToDistrict > 0)
                                    {
                                        ws.Cells[cRow, objSetting.LocationToDistrict].Value = item.LocationToDistrict;
                                    }
                                    cRow++;
                                }
                            }
                            package.Save();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDIImportPacket_Data OPS_DI_Import_Packet_Data(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                var result = new DTOOPSDIImportPacket_Data();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_Data(pID, new List<string>());
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int OPS_DI_Import_Packet_Save(dynamic dynParam)
        {
            try
            {
                int result = 0;
                DTOOPSDIImportPacket item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDIImportPacket>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_Import(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                int templateID = (int)dynParam.templateID;
                List<DTOOPSDIImportPacketTOMaster_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSDIImportPacketTOMaster_Import>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_Import(pID, templateID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_2View_Save(dynamic dynParam)
        {
            try
            {
                List<DTOOPSDIImportPacketTOMaster> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSDIImportPacketTOMaster>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_2View_Save(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_ToOPS(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_ToOPS(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_ToMON(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_ToMON(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_Delete(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_Delete(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_Reset(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_Reset(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_Vehicle_Update(dynamic dynParam)
        {
            try
            {
                List<DTOOPSDIImportPacket_Vehicle> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSDIImportPacket_Vehicle>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_Vehicle_Update(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_ORDGroupProduct_SaveList(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_ORDGroupProduct_SaveList(pID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_DI_Import_Packet_ORDGroupProduct_DeleteList(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_DI_Import_Packet_ORDGroupProduct_DeleteList(pID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSDIImportPacketTOMaster_Import> OPS_DI_Import_Packet_Check(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                int templateID = (int)dynParam.templateID;
                string file = "/" + dynParam.file.ToString();
                var dataTOMaster = new List<DTOOPSDIImportPacketTOMaster_Import>();
                DTOCUSSettingPlan objSetting = new DTOCUSSettingPlan();
                DTOCUSSettingOrder objSettingOrder = new DTOCUSSettingOrder();
                DTOOPSDIImportPacket_Data data = new DTOOPSDIImportPacket_Data();
                DTOORDOrder_ImportCheck dataOrder = new DTOORDOrder_ImportCheck();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    objSetting = sv.OPS_DI_Import_Packet_Setting_Get(templateID);
                });

                if (objSetting != null)
                {
                    string[] aValue = { "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart", "HasStockProduct",
                                        "StockID", "GroupOfProductID", "ProductID", "ListStockWithProduct", "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID" };
                    var sValue = new List<string>(aValue);
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        objSettingOrder = sv.ORDOrder_Excel_Setting_Get(objSetting.CUSSettingOrderID);
                    });
                    if (objSettingOrder == null)
                        throw new Exception("Không tìm thấy thiết lập đơn hàng.");
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        dataOrder = sv.ORDOrder_Excel_Import_Data(objSettingOrder.CustomerID);
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                List<string> dataOrders = new List<string>();
                                for (row = objSettingOrder.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var excelInput = GetDataValue(worksheet, objSettingOrder, row, sValue);
                                    if (!string.IsNullOrEmpty(excelInput["OrderCode"]))
                                        dataOrders.Add(excelInput["OrderCode"].Trim().ToLower());
                                }
                                if (dataOrders.Count == 0)
                                    throw new Exception("Không có thông tin đơn hàng.");
                                ServiceFactory.SVOperation((ISVOperation sv) =>
                                {
                                    data = sv.OPS_DI_Import_Packet_Data(pID, dataOrders);
                                });

                                int idx = 1;
                                Dictionary<int, int> dicTOMaster = new Dictionary<int, int>();
                                for (row = objSettingOrder.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var excelError = new List<string>();
                                    var excelInput = GetDataValue(worksheet, objSettingOrder, row, sValue);

                                    var excelPlanInput = GetDataValue(worksheet, objSetting, row, sValue);
                                    if (!string.IsNullOrEmpty(excelPlanInput["VehicleNo"]))
                                    {
                                        DateTime? ETD = null;
                                        if (objSetting.MasterETDDate < 1 && objSetting.MasterETDDate_Time < 1)
                                            throw new Exception("Chưa thiết lập cột ETD.");
                                        try
                                        {
                                            ETD = ExcelHelper.ValueToDate(excelPlanInput["MasterETDDate"]);
                                        }
                                        catch (Exception)
                                        {
                                            try
                                            {
                                                ETD = Convert.ToDateTime(excelPlanInput["MasterETDDate"], new CultureInfo("vi-VN"));
                                            }
                                            catch { }
                                        }
                                        if (objSetting.MasterETDTime > 0 && ETD != null)
                                        {
                                            if (!string.IsNullOrEmpty(excelPlanInput["MasterETDTime"]))
                                            {
                                                try
                                                {
                                                    ETD = ETD.Value.Date.Add(TimeSpan.Parse(excelPlanInput["MasterETDTime"]));
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai giờ ETD.");
                                                }
                                            }
                                        }
                                        if (objSetting.MasterETDDate_Time > 0)
                                        {
                                            try
                                            {
                                                ETD = ExcelHelper.ValueToDate(excelPlanInput["MasterETDDate_Time"]);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    ETD = Convert.ToDateTime(excelPlanInput["MasterETDDate_Time"], new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai ETD.");
                                                }
                                            }
                                        }

                                        DateTime? ETA = null;
                                        if (objSetting.MasterHours > 0)
                                        {
                                            if (ETD != null)
                                            {
                                                try
                                                {
                                                    ETA = ETD.Value.AddHours(Convert.ToDouble(excelPlanInput["MasterHours"]));
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai thời gian chuyến.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.MasterETADate < 1 && objSetting.MasterETADate_Time < 1)
                                                throw new Exception("Chưa thiết lập cột ETA.");
                                            try
                                            {
                                                ETA = ExcelHelper.ValueToDate(excelPlanInput["MasterETADate"]);
                                            }
                                            catch (Exception)
                                            {
                                                try
                                                {
                                                    ETA = Convert.ToDateTime(excelPlanInput["MasterETADate"], new CultureInfo("vi-VN"));
                                                }
                                                catch { }
                                            }
                                            if (objSetting.MasterETATime > 0 && ETA != null)
                                            {
                                                if (!string.IsNullOrEmpty(excelPlanInput["MasterETATime"]))
                                                {
                                                    try
                                                    {
                                                        ETA = ETA.Value.Date.Add(TimeSpan.Parse(excelPlanInput["MasterETATime"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ ETA.");
                                                    }
                                                }
                                            }
                                            if (objSetting.MasterETADate_Time > 0)
                                            {
                                                try
                                                {
                                                    ETA = ExcelHelper.ValueToDate(excelPlanInput["MasterETADate_Time"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        ETA = Convert.ToDateTime(excelPlanInput["MasterETADate_Time"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai ETA.");
                                                    }
                                                }
                                            }
                                        }

                                        if (ETA != null && ETD != null && ETD >= ETA)
                                            excelError.Add("Sai ràng buộc thời gian ETD-ETA");


                                        var isNewVehicle = false;
                                        int venID = -1, vehID = -1;
                                        string venCode = excelPlanInput["VendorCode"].Trim(), vehCode = excelPlanInput["VehicleNo"].Trim();
                                        string driverName = excelPlanInput["DriverName"], driverTel = excelPlanInput["DriverTel"];

                                        #region Kiểm tra chuyến
                                        if (!string.IsNullOrEmpty(excelPlanInput["VendorCode"]))
                                        {
                                            var objVendor = data.ListVendor.FirstOrDefault(c => c.VendorCode.Trim().ToLower() == excelPlanInput["VendorCode"].Trim().ToLower());
                                            if (objVendor != null)
                                            {
                                                venID = objVendor.ID;
                                                venCode = objVendor.VendorCode;
                                            }
                                            else
                                            {
                                                excelError.Add("Nhà xe [" + excelPlanInput["VendorCode"] + "] không tồn tại.");
                                            }
                                        }
                                        else
                                        {
                                            var objVendor = data.ListVendor.FirstOrDefault(c => c.IsVendor == false);
                                            if (objVendor != null)
                                            {
                                                venID = objVendor.ID;
                                                venCode = "Xe nhà";
                                            }
                                            else
                                            {
                                                excelError.Add("Nhà xe không xác định.");
                                            }
                                        }

                                        var dataCode = new List<string>();
                                        var dataChar = new char[] { '-', ' ', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', '/', '?', '<', ',', '>', '~', '`' };
                                        var sCode = vehCode.Split(dataChar, StringSplitOptions.RemoveEmptyEntries).ToArray();
                                        foreach (var c in dataChar)
                                        {
                                            dataCode.Add(string.Join(c.ToString(), sCode).ToLower());
                                        }

                                        var objVehicle = data.ListVehicle.FirstOrDefault(c => c.VendorID == venID && dataCode.Contains(c.VehicleNo.Trim().ToLower()));
                                        if (objVehicle != null)
                                        {
                                            vehID = objVehicle.ID;
                                            vehCode = objVehicle.VehicleNo;
                                            if (venID > 0 && venCode == "Xe nhà")
                                            {
                                                var objPlanning = data.ListVehiclePlan.FirstOrDefault(c => c.VehicleID == vehID && !((c.DateFrom < ETD && c.DateTo < ETD) || (c.DateFrom > ETA && c.DateTo > ETA)));
                                                if (objPlanning != null)
                                                {
                                                    var objDriver = data.ListDriver.FirstOrDefault(c => c.ID == objPlanning.DriverID);
                                                    if (objDriver != null)
                                                    {
                                                        driverName = objDriver.DriverName;
                                                        driverTel = objDriver.DriverTel;
                                                    }
                                                }
                                                else
                                                {
                                                    driverName = objVehicle.DriverName;
                                                    driverTel = objVehicle.DriverTel;
                                                }
                                                if (string.IsNullOrEmpty(driverName))
                                                    excelError.Add("Không có thông tin tài xế.");
                                            }
                                        }
                                        else
                                        {
                                            if (venID > 0)
                                            {
                                                if (venCode != "Xe nhà")
                                                {
                                                    isNewVehicle = true;
                                                }
                                                excelError.Add("Xe [" + excelPlanInput["VehicleNo"] + "] không tồn tại.");
                                            }
                                        }

                                        var govID = -1;
                                        if (!string.IsNullOrEmpty(excelInput["GroupVehicle"]))
                                        {
                                            var objCheck = dataOrder.ListGroupOfVehicle.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["GroupVehicle"].Trim().ToLower());
                                            if (objCheck != null)
                                            {
                                                govID = objCheck.ID;
                                            }
                                            else
                                            {
                                                excelError.Add("Không tìm thấy loại xe [" + excelInput["GroupVehicle"] + "]");
                                            }
                                        }
                                        #endregion

                                        #region Lưu thông tin chuyến
                                        var obj = new DTOOPSDIImportPacketTOMaster_Import();
                                        if (venID > 0 && vehID > 0 && ETD != null && ETA != null)
                                        {
                                            obj = dataTOMaster.FirstOrDefault(c => c.VendorID == venID && (c.VehicleID == vehID || c.VehicleNo == vehCode) && c.ETD == ETD);
                                            if (obj == null)
                                            {
                                                obj = new DTOOPSDIImportPacketTOMaster_Import();
                                                obj.ETD = ETD;
                                                obj.ETA = ETA;
                                                obj.IsNewVendorVehicle = isNewVehicle;
                                                obj.SortOrder = idx++;
                                                obj.VehicleID = vehID;
                                                obj.VendorID = venID;
                                                obj.VehicleNo = vehCode;
                                                obj.VendorCode = venCode;
                                                obj.GroupOfVehicleID = govID;
                                                obj.DriverTel = driverTel;
                                                obj.DriverName = driverName;
                                                obj.Note = excelPlanInput["MasterNote"];
                                                obj.Error = new List<string>();
                                                obj.ListDITOGroupProduct = new List<DTOOPSDIImportPacket_GroupProduct>();
                                                foreach (var item in excelError)
                                                {
                                                    obj.Error.Add(item);
                                                }
                                                dataTOMaster.Add(obj);
                                                dicTOMaster.Add(row, obj.SortOrder);
                                            }
                                            else
                                            {
                                                if (obj.IsFTL && obj.GroupOfVehicleID != govID)
                                                {
                                                    obj = dataTOMaster.FirstOrDefault(c => c.VendorID == venID && (c.VehicleID == vehID || c.VehicleNo == vehCode) && c.ETD == ETD && c.GroupOfVehicleID == govID);
                                                    if (obj == null)
                                                    {
                                                        obj = new DTOOPSDIImportPacketTOMaster_Import();
                                                        obj.ETD = ETD;
                                                        obj.ETA = ETA;
                                                        obj.IsNewVendorVehicle = isNewVehicle;
                                                        obj.SortOrder = idx++;
                                                        obj.VehicleID = vehID;
                                                        obj.VendorID = venID;
                                                        obj.VehicleNo = vehCode;
                                                        obj.VendorCode = venCode;
                                                        obj.GroupOfVehicleID = govID;
                                                        obj.DriverTel = driverTel;
                                                        obj.DriverName = driverName;
                                                        obj.Note = excelPlanInput["MasterNote"];
                                                        obj.Error = new List<string>();
                                                        obj.ListDITOGroupProduct = new List<DTOOPSDIImportPacket_GroupProduct>();
                                                        foreach (var item in excelError)
                                                        {
                                                            obj.Error.Add(item);
                                                        }
                                                        dataTOMaster.Add(obj);
                                                        dicTOMaster.Add(row, obj.SortOrder);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.ETD = ETD;
                                            obj.ETA = ETA;
                                            obj.IsNewVendorVehicle = isNewVehicle;
                                            obj.SortOrder = idx++;
                                            obj.VehicleID = vehID;
                                            obj.VendorID = venID;
                                            obj.VehicleNo = vehCode;
                                            obj.VendorCode = venCode;
                                            obj.GroupOfVehicleID = govID;
                                            obj.DriverTel = driverTel;
                                            obj.DriverName = driverName;
                                            obj.Note = excelPlanInput["MasterNote"];
                                            obj.Error = new List<string>();
                                            obj.ListDITOGroupProduct = new List<DTOOPSDIImportPacket_GroupProduct>();
                                            foreach (var item in excelError)
                                            {
                                                obj.Error.Add(item);
                                            }
                                            dataTOMaster.Add(obj);
                                            dicTOMaster.Add(row, obj.SortOrder);
                                        }
                                        #endregion

                                        #region Kiểm tra nhóm sản phẩm
                                        var objCopy = new CopyHelper();
                                        var oData = GetDataGroupProduct(worksheet, row, excelInput, dataOrder, objSettingOrder, sValue);
                                        var isSettingETD = objSettingOrder.ETD > 0 || objSettingOrder.ETDTime_RequestDate > 0;
                                        foreach (var o in oData)
                                        {
                                            if (o.IsFTL)
                                            {
                                                if (obj.ListDITOGroupProduct.Count == 0)
                                                {
                                                    obj.IsFTL = true;
                                                }
                                                if (!obj.IsFTL)
                                                {
                                                    obj.Error.Add("Chuyến LTL không thể chạy đơn FTL. Nhóm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                                }
                                                var objDITO = data.ListDITOGroupProduct.FirstOrDefault(c => c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                        && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                                        && (isSettingETD ? c.ETD == o.ETD : true) && c.GroupID == o.GroupID && c.ProductID == o.ProductID
                                                        && (c.TypeOfPacking == 1 ? c.Ton == o.Ton : c.TypeOfPacking == 2 ? c.CBM == o.CBM : c.Quantity == o.Quantity));
                                                if (objDITO == null)
                                                    objDITO = data.ListDITOGroupProduct.FirstOrDefault(c => c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                        && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                                        && (isSettingETD ? c.ETD == o.ETD : true) && c.GroupID == o.GroupID && c.ProductID == o.ProductID);
                                                if (objDITO != null)
                                                {
                                                    if (obj.FTLOrderID < 1)
                                                        obj.FTLOrderID = objDITO.OrderID;
                                                    if (obj.FTLOrderID != objDITO.OrderID)
                                                        obj.Error.Add("Không thể chạy 2 đơn FTL. Nhóm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");

                                                    o.ID = objDITO.ID;
                                                    o.ETA = objDITO.ETA;
                                                    o.ETARequest = objDITO.ETARequest;
                                                    o.ETD = objDITO.ETD;
                                                    o.LocationFromName = objDITO.LocationFromName;
                                                    o.LocationToName = objDITO.LocationToName;
                                                    switch (objDITO.TypeOfPacking)
                                                    {
                                                        case 1:
                                                            if (objDITO.Ton > o.Ton)
                                                            {
                                                                var objTmp = new DTOOPSDIImportPacket_GroupProduct();
                                                                objCopy.Copy(objDITO, objTmp);
                                                                objTmp.Ton = Math.Round(objDITO.Ton - o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                if (objDITO.ExchangeQuantity > 0)
                                                                {
                                                                    o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                    objTmp.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * objTmp.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                if (objDITO.ExchangeCBM > 0)
                                                                {
                                                                    o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                    objTmp.CBM = Math.Round(objDITO.ExchangeCBM.Value * objTmp.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                data.ListDITOGroupProduct.Remove(objDITO);
                                                                data.ListDITOGroupProduct.Add(objTmp);
                                                                obj.ListDITOGroupProduct.Add(o);
                                                            }
                                                            else if (objDITO.Ton == o.Ton)
                                                            {
                                                                if (objDITO.ExchangeQuantity > 0)
                                                                {
                                                                    o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                if (objDITO.ExchangeCBM > 0)
                                                                {
                                                                    o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                data.ListDITOGroupProduct.Remove(objDITO);
                                                                obj.ListDITOGroupProduct.Add(o);
                                                            }
                                                            else
                                                            {
                                                                obj.Error.Add("ĐH [" + objDITO.OrderCode + "] Quá số tấn. Dòng [" + row + "].");
                                                            }
                                                            break;
                                                        case 2:
                                                            if (objDITO.CBM > o.CBM)
                                                            {
                                                                var objTmp = new DTOOPSDIImportPacket_GroupProduct();
                                                                objCopy.Copy(objDITO, objTmp);
                                                                objTmp.CBM = Math.Round(objDITO.CBM - o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                if (objDITO.ExchangeTon > 0)
                                                                {
                                                                    o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                    objTmp.Ton = Math.Round(objDITO.ExchangeTon.Value * objTmp.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                if (objDITO.ExchangeQuantity > 0)
                                                                {
                                                                    o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                    objTmp.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * objTmp.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                data.ListDITOGroupProduct.Remove(objDITO);
                                                                data.ListDITOGroupProduct.Add(objTmp);
                                                                obj.ListDITOGroupProduct.Add(o);
                                                            }
                                                            else if (objDITO.CBM == o.CBM)
                                                            {
                                                                if (objDITO.ExchangeTon > 0)
                                                                {
                                                                    o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                if (objDITO.ExchangeQuantity > 0)
                                                                {
                                                                    o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                data.ListDITOGroupProduct.Remove(objDITO);
                                                                obj.ListDITOGroupProduct.Add(o);
                                                            }
                                                            else
                                                            {
                                                                obj.Error.Add("ĐH [" + objDITO.OrderCode + "] Quá số khối. Dòng [" + row + "].");
                                                            }
                                                            break;
                                                        case 3:
                                                            if (objDITO.Quantity > o.Quantity)
                                                            {
                                                                var objTmp = new DTOOPSDIImportPacket_GroupProduct();
                                                                objCopy.Copy(objDITO, objTmp);
                                                                objTmp.Quantity = Math.Round(objDITO.Quantity - o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                if (objDITO.ExchangeTon > 0)
                                                                {
                                                                    o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                    objTmp.Ton = Math.Round(objDITO.ExchangeTon.Value * objTmp.Quantity, digitsOfRound);
                                                                }
                                                                if (objDITO.ExchangeCBM > 0)
                                                                {
                                                                    o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                    objTmp.CBM = Math.Round(objDITO.ExchangeCBM.Value * objTmp.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                data.ListDITOGroupProduct.Remove(objDITO);
                                                                data.ListDITOGroupProduct.Add(objTmp);
                                                                obj.ListDITOGroupProduct.Add(o);
                                                            }
                                                            else if (objDITO.Quantity == o.Quantity)
                                                            {
                                                                if (objDITO.ExchangeTon > 0)
                                                                {
                                                                    o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                if (objDITO.ExchangeCBM > 0)
                                                                {
                                                                    o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                }
                                                                data.ListDITOGroupProduct.Remove(objDITO);
                                                                obj.ListDITOGroupProduct.Add(o);
                                                            }
                                                            else
                                                            {
                                                                obj.Error.Add("ĐH [" + objDITO.OrderCode + "] Quá SL. Dòng [" + row + "].");
                                                            }
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    obj.Error.Add("Không tìm thấy sản phẩm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                                }
                                            }
                                            else
                                            {
                                                if (obj.IsFTL)
                                                {
                                                    obj.Error.Add("Chuyến FTL không thể chạy đơn LTL. Nhóm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                                }
                                                else
                                                {
                                                    if (o.Ton == 0 && o.CBM == 0 && o.Quantity == 0 && o.ProductID > 0 && o.GroupID > 0)
                                                    {
                                                        obj.Error.Add("Không có thông tin sản lượng. Nhóm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                                    }
                                                    else
                                                    {
                                                        //var lst = data.ListDITOGroupProduct.Where(c => c.TOMasterID == null && c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()).ToList();
                                                        var objDITO = data.ListDITOGroupProduct.FirstOrDefault(c => c.TOMasterID == null && c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                               && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                                               && c.GroupID == o.GroupID && c.ProductID == o.ProductID && (c.TypeOfPacking == 1 ? c.Ton == o.Ton : (c.TypeOfPacking == 2 ? c.CBM == o.CBM : c.Quantity == o.Quantity)));
                                                        if (objDITO == null)
                                                            objDITO = data.ListDITOGroupProduct.FirstOrDefault(c => c.TOMasterID == null && c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                               && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                                               && c.GroupID == o.GroupID && c.ProductID == o.ProductID && (c.TypeOfPacking == 1 ? c.Ton > o.Ton : (c.TypeOfPacking == 2 ? c.CBM > o.CBM : c.Quantity > o.Quantity)));
                                                        if (objDITO == null)
                                                            objDITO = data.ListDITOGroupProduct.FirstOrDefault(c => c.TOMasterID == null && c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                               && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                                               && c.GroupID == o.GroupID && c.ProductID == o.ProductID);
                                                        if (objDITO == null)
                                                            objDITO = data.ListDITOGroupProduct.FirstOrDefault(c => c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                               && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                                               && c.GroupID == o.GroupID && c.ProductID == o.ProductID);
                                                        if (objDITO != null)
                                                        {
                                                            o.ID = objDITO.ID;
                                                            o.ETA = objDITO.ETA;
                                                            o.ETARequest = objDITO.ETARequest;
                                                            o.ETD = objDITO.ETD;
                                                            o.LocationFromName = objDITO.LocationFromName;
                                                            o.LocationToName = objDITO.LocationToName;
                                                            switch (objDITO.TypeOfPacking)
                                                            {
                                                                case 1:
                                                                    if (objDITO.Ton > o.Ton)
                                                                    {
                                                                        var objTmp = new DTOOPSDIImportPacket_GroupProduct();
                                                                        objCopy.Copy(objDITO, objTmp);
                                                                        objTmp.Ton = Math.Round(objDITO.Ton - o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        if (objDITO.ExchangeQuantity > 0)
                                                                        {
                                                                            o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                            objTmp.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * objTmp.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        if (objDITO.ExchangeCBM > 0)
                                                                        {
                                                                            o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                            objTmp.CBM = Math.Round(objDITO.ExchangeCBM.Value * objTmp.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        data.ListDITOGroupProduct.Remove(objDITO);
                                                                        data.ListDITOGroupProduct.Add(objTmp);
                                                                        obj.ListDITOGroupProduct.Add(o);
                                                                    }
                                                                    else if (objDITO.Ton == o.Ton)
                                                                    {
                                                                        if (objDITO.ExchangeQuantity > 0)
                                                                        {
                                                                            o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        if (objDITO.ExchangeCBM > 0)
                                                                        {
                                                                            o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        data.ListDITOGroupProduct.Remove(objDITO);
                                                                        obj.ListDITOGroupProduct.Add(o);
                                                                    }
                                                                    else
                                                                    {
                                                                        obj.Error.Add("ĐH [" + objDITO.OrderCode + "] Quá số tấn. Dòng [" + row + "].");
                                                                    }
                                                                    break;
                                                                case 2:
                                                                    if (objDITO.CBM > o.CBM)
                                                                    {
                                                                        var objTmp = new DTOOPSDIImportPacket_GroupProduct();
                                                                        objCopy.Copy(objDITO, objTmp);
                                                                        objTmp.CBM = Math.Round(objDITO.CBM - o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        if (objDITO.ExchangeTon > 0)
                                                                        {
                                                                            o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                            objTmp.Ton = Math.Round(objDITO.ExchangeTon.Value * objTmp.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        if (objDITO.ExchangeQuantity > 0)
                                                                        {
                                                                            o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                            objTmp.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * objTmp.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        data.ListDITOGroupProduct.Remove(objDITO);
                                                                        data.ListDITOGroupProduct.Add(objTmp);
                                                                        obj.ListDITOGroupProduct.Add(o);
                                                                    }
                                                                    else if (objDITO.CBM == o.CBM)
                                                                    {
                                                                        if (objDITO.ExchangeTon > 0)
                                                                        {
                                                                            o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        if (objDITO.ExchangeQuantity > 0)
                                                                        {
                                                                            o.Quantity = Math.Round(objDITO.ExchangeQuantity.Value * o.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        data.ListDITOGroupProduct.Remove(objDITO);
                                                                        obj.ListDITOGroupProduct.Add(o);
                                                                    }
                                                                    else
                                                                    {
                                                                        obj.Error.Add("ĐH [" + objDITO.OrderCode + "] Quá số khối. Dòng [" + row + "].");
                                                                    }
                                                                    break;
                                                                case 3:
                                                                    if (objDITO.Quantity > o.Quantity)
                                                                    {
                                                                        var objTmp = new DTOOPSDIImportPacket_GroupProduct();
                                                                        objCopy.Copy(objDITO, objTmp);
                                                                        objTmp.Quantity = Math.Round(objDITO.Quantity - o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        if (objDITO.ExchangeTon > 0)
                                                                        {
                                                                            o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                            objTmp.Ton = Math.Round(objDITO.ExchangeTon.Value * objTmp.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        if (objDITO.ExchangeCBM > 0)
                                                                        {
                                                                            o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                            objTmp.CBM = Math.Round(objDITO.ExchangeCBM.Value * objTmp.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        data.ListDITOGroupProduct.Remove(objDITO);
                                                                        data.ListDITOGroupProduct.Add(objTmp);
                                                                        obj.ListDITOGroupProduct.Add(o);
                                                                    }
                                                                    else if (objDITO.Quantity == o.Quantity)
                                                                    {
                                                                        if (objDITO.ExchangeTon > 0)
                                                                        {
                                                                            o.Ton = Math.Round(objDITO.ExchangeTon.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        if (objDITO.ExchangeCBM > 0)
                                                                        {
                                                                            o.CBM = Math.Round(objDITO.ExchangeCBM.Value * o.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                                                                        }
                                                                        data.ListDITOGroupProduct.Remove(objDITO);
                                                                        obj.ListDITOGroupProduct.Add(o);
                                                                    }
                                                                    else
                                                                    {
                                                                        obj.Error.Add("ĐH [" + objDITO.OrderCode + "] Quá SL. Dòng [" + row + "].");
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            obj.Error.Add("Không tìm thấy sản phẩm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else if (string.IsNullOrEmpty(excelInput["OrderCode"]))
                                    {
                                        break;
                                    }
                                }

                                foreach (var item in dataTOMaster.Where(c => c.IsFTL == true && c.FTLOrderID > 0).ToList())
                                {
                                    var objCheck = data.ListDITOGroupProduct.Where(c => c.OrderID == item.FTLOrderID && c.IsFTL == true).ToList();
                                    if (objCheck.Count > 0)
                                    {
                                        item.Error.Add("Chưa phân chuyến hết đơn FTL. ĐH [" + objCheck.FirstOrDefault().OrderCode + "]");
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var item in dataTOMaster)
                {
                    if (item.Error.Count == 0)
                    {
                        item.ExcelSuccess = true;
                        var objV = data.ListVehicle.FirstOrDefault(c => c.VendorID == item.VendorID && c.ID == item.VehicleID);
                        if (objV != null && item.ListDITOGroupProduct.Sum(c => c.Ton) > objV.MaxWeight)
                        {
                            item.ExcelError = "Quá trọng tải.";
                        }
                    }
                    else
                    {
                        item.ExcelSuccess = false;
                        item.ExcelError = string.Join(", ", item.Error);
                    }
                }

                return dataTOMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Dictionary<string, string> GetDataValue(ExcelWorksheet ws, object obj, int row, List<string> sValue)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    var p = prop.Name;
                    if (!sValue.Contains(p))
                    {
                        var v = (int)prop.GetValue(obj, null);
                        result.Add(p, v > 0 ? ExcelHelper.GetValue(ws, row, v) : string.Empty);
                    }
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        private List<DTOOPSDIImportPacket_GroupProduct> GetDataGroupProduct(ExcelWorksheet worksheet, int row, Dictionary<string, string> excelInput, DTOORDOrder_ImportCheck data, DTOCUSSettingOrder objSetting, List<string> sValue)
        {
            var dataRes = new List<DTOOPSDIImportPacket_GroupProduct>();

            int svID = -1, serviceID = -1, tmID = -1, transportID = -1;

            if (objSetting.TypeOfTransportModeID > 0)
            {
                tmID = objSetting.TypeOfTransportModeID;
                var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                if (objTM != null)
                    transportID = objTM.TransportModeID;
            }
            else
            {
                var str = excelInput["TypeOfTransportMode"];
                if (!string.IsNullOrEmpty(str))
                {
                    var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                    if (objTM != null)
                    {
                        tmID = objTM.ID;
                        transportID = objTM.TransportModeID;
                    }
                }
            }

            if (objSetting.ServiceOfOrderID > 0)
            {
                svID = objSetting.ServiceOfOrderID;
                var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.ID == svID);
                if (objSV != null)
                    serviceID = objSV.ServiceOfOrderID;
            }
            else
            {
                var str = excelInput["ServiceOfOrder"].Trim().ToLower();
                if (!string.IsNullOrEmpty(str))
                {
                    var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                    if (objSV != null)
                    {
                        svID = objSV.ID;
                        serviceID = objSV.ServiceOfOrderID;
                    }
                }
            }

            //Xe tải
            if ((transportID == iFTL || transportID == iLTL) && (serviceID == iLO || serviceID == -1))
            {
                #region ĐH xe tải
                var eItem = new DTOOPSDIImportPacket_GroupProduct();
                var cusID = -1;

                #region Check tgian

                if (objSetting.RequestDate > 0)
                {
                    try
                    {
                        eItem.RequestDate = ExcelHelper.ValueToDate(excelInput["RequestDate"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.RequestDate = Convert.ToDateTime(excelInput["RequestDate"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                    if (objSetting.RequestTime > 0 && eItem.RequestDate != null)
                    {
                        if (!string.IsNullOrEmpty(excelInput["RequestTime"]))
                        {
                            try
                            {
                                eItem.RequestDate = eItem.RequestDate.Date.Add(TimeSpan.Parse(excelInput["RequestTime"]));
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                if (objSetting.RequestDate_Time > 0)
                {
                    try
                    {
                        eItem.RequestDate = ExcelHelper.ValueToDate(excelInput["RequestDate_Time"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.RequestDate = Convert.ToDateTime(excelInput["RequestDate_Time"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETD"]))
                {
                    try
                    {
                        eItem.ETD = ExcelHelper.ValueToDate(excelInput["ETD"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.ETD = Convert.ToDateTime(excelInput["ETD"], new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                        }
                    }
                }
                else if (objSetting.ETDTime_RequestDate > 0 && eItem.RequestDate != null)
                {
                    if (!string.IsNullOrEmpty(excelInput["ETDTime_RequestDate"]))
                    {
                        try
                        {
                            eItem.ETD = eItem.RequestDate.Date.Add(TimeSpan.Parse(excelInput["ETDTime_RequestDate"]));
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        eItem.ETD = eItem.RequestDate;
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETA"]))
                {
                    try
                    {
                        eItem.ETA = ExcelHelper.ValueToDate(excelInput["ETA"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.ETA = Convert.ToDateTime(excelInput["ETA"], new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                        }
                    }
                }
                else if (objSetting.ETATime_RequestDate > 0 && eItem.RequestDate != null)
                {
                    if (!string.IsNullOrEmpty(excelInput["ETATime_RequestDate"]))
                    {
                        try
                        {
                            eItem.ETA = eItem.RequestDate.Date.Add(TimeSpan.Parse(excelInput["ETATime_RequestDate"]));
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        eItem.ETA = eItem.RequestDate;
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETARequest"]))
                {
                    try
                    {
                        eItem.ETARequest = ExcelHelper.ValueToDate(excelInput["ETARequest"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.ETARequest = Convert.ToDateTime(excelInput["ETARequest"], new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                        }
                    }
                    if (eItem.ETARequest != null && objSetting.ETARequestTime > 0)
                    {
                        if (!string.IsNullOrEmpty(excelInput["ETARequestTime"]))
                        {
                            try
                            {
                                eItem.ETARequest = eItem.ETARequest.Value.Date.Add(TimeSpan.Parse(excelInput["ETARequestTime"]));
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                #endregion

                #region Check Customer, Contract và Code

                if (objSetting.CustomerID == objSetting.SYSCustomerID)
                {
                    if (!string.IsNullOrEmpty(excelInput["CustomerCode"]))
                    {
                        var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["CustomerCode"].Trim().ToLower());
                        if (objCheck != null)
                        {
                            cusID = objCheck.ID;
                        }
                    }
                }
                else
                {
                    cusID = objSetting.CustomerID;
                }

                eItem.OrderCode = excelInput["OrderCode"];

                #endregion

                #region Check nhà phân phối

                var pID = -1;
                string toCode = string.Empty;
                string toName = string.Empty;
                string dName = excelInput["DistributorName"];
                string dCode = excelInput["DistributorCode"];

                if (!string.IsNullOrEmpty(excelInput["DistributorCodeName"]))
                {
                    string[] s = excelInput["DistributorCodeName"].Split('-');
                    dCode = s[0];
                    if (s.Length > 1)
                    {
                        dName = excelInput["DistributorCodeName"].Substring(dCode.Length + 1);
                    }
                }

                if (!string.IsNullOrEmpty(dCode))
                {
                    var objCheck = data.ListDistributor.FirstOrDefault(c => c.PartnerCode.Trim().ToLower() == dCode.Trim().ToLower() && c.CustomerID == cusID);
                    if (objCheck != null)
                    {
                        pID = objCheck.CUSPartnerID;
                        toCode = excelInput["LocationToCode"];
                        toName = excelInput["LocationToName"];
                        if (objSetting.LocationToCodeName > 0)
                        {
                            if (!string.IsNullOrEmpty(excelInput["LocationToCodeName"]))
                            {
                                toCode = excelInput["LocationToCodeName"].Split('-').FirstOrDefault();
                                toName = excelInput["LocationToCodeName"].Split('-').Skip(1).FirstOrDefault();
                            }
                            else
                            {
                                toCode = string.Empty;
                                toName = string.Empty;
                            }
                        }

                        //Tìm theo code
                        var objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                        if (objTo != null)
                        {
                            eItem.LocationToID = objTo.CUSLocationID;
                        }
                        else
                        {
                            objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.Address.Trim().ToLower() == excelInput["LocationToAddress"].Trim().ToLower());
                            if (objTo != null)
                            {
                                eItem.LocationToID = objTo.CUSLocationID;
                            }
                        }
                    }
                }

                #endregion

                #region Check sản lượng, kho, nhóm sản phẩm và đơn vị tính

                //Dictionary quantity theo kho. [Q = Quantity]
                Dictionary<int, Dictionary<int, double>> dicQ = new Dictionary<int, Dictionary<int, double>>();
                //Dictionary chi tiết kho. [L = Location]
                Dictionary<int, DTOORDData_Location> dicL = new Dictionary<int, DTOORDData_Location>();
                //Dictionary chi tiết nhóm sản phẩm đầu tiên/chỉ định trong kho. [GS = GroupProductInStock]
                Dictionary<int, DTOORDData_GroupProduct> dicGS = new Dictionary<int, DTOORDData_GroupProduct>();

                //Dictionary quantity theo kho-nhóm hàng-hàng hóa. [QP = QuantityProduct]
                Dictionary<string, Dictionary<int, double>> dicQP = new Dictionary<string, Dictionary<int, double>>();

                //Nếu thiết lập kho theo cột, check kho, lấy sản lượng theo excel.
                if (objSetting.HasStock && objSetting.ListStock != null)
                {
                    foreach (var stock in objSetting.ListStock)
                    {
                        int sID = stock.StockID;
                        var objCheck = data.ListStock.FirstOrDefault(c => c.CUSLocationID == sID && c.CustomerID == cusID);
                        if (objCheck != null)
                        {
                            dicL.Add(sID, objCheck);
                        }

                        Dictionary<int, double> dicV = new Dictionary<int, double>();
                        var dicS = GetDataValue(worksheet, stock, row, sValue);
                        if (!dicS.Values.All(c => string.IsNullOrEmpty(c)))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(dicS["Ton"]))
                                    dicV.Add(1, Convert.ToDouble(dicS["Ton"]));
                                else
                                    dicV.Add(1, 0);
                            }
                            catch
                            {
                                dicV.Add(1, 0);
                            }
                            try
                            {
                                if (!string.IsNullOrEmpty(dicS["CBM"]))
                                    dicV.Add(2, Convert.ToDouble(dicS["CBM"]));
                                else
                                    dicV.Add(2, 0);
                            }
                            catch
                            {
                                dicV.Add(2, 0);
                            }
                            try
                            {
                                if (!string.IsNullOrEmpty(dicS["Quantity"]))
                                    dicV.Add(3, Convert.ToDouble(dicS["Quantity"]));
                                else
                                    dicV.Add(3, 0);
                            }
                            catch
                            {
                                dicV.Add(3, 0);
                            }
                            dicQ.Add(sID, dicV);
                        }
                    }
                }
                else if (objSetting.HasStockProduct)
                {
                    foreach (var stock in objSetting.ListStockWithProduct)
                    {
                        var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID == stock.StockID && c.CustomerID == cusID);
                        var cusGroup = data.ListGroupOfProduct.FirstOrDefault(c => c.CUSStockID == stock.StockID && c.ID == stock.GroupOfProductID);
                        var cusProduct = data.ListProduct.FirstOrDefault(c => c.GroupOfProductID == stock.GroupOfProductID && stock.ProductID == c.ID);
                        if (cusStock != null && cusGroup != null && cusProduct != null)
                        {
                            Dictionary<int, double> dicV = new Dictionary<int, double>();
                            var dicS = GetDataValue(worksheet, stock, row, sValue);
                            if (!dicS.Values.All(c => string.IsNullOrEmpty(c)))
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(dicS["Ton"]))
                                        dicV.Add(1, Convert.ToDouble(dicS["Ton"]));
                                    else
                                        dicV.Add(1, 0);
                                }
                                catch
                                {
                                    dicV.Add(1, 0);
                                }
                                try
                                {
                                    if (!string.IsNullOrEmpty(dicS["CBM"]))
                                        dicV.Add(2, Convert.ToDouble(dicS["CBM"]));
                                    else
                                        dicV.Add(2, 0);
                                }
                                catch
                                {
                                    dicV.Add(2, 0);
                                }
                                try
                                {
                                    if (!string.IsNullOrEmpty(dicS["Quantity"]))
                                        dicV.Add(3, Convert.ToDouble(dicS["Quantity"]));
                                    else
                                        dicV.Add(3, 0);
                                }
                                catch
                                {
                                    dicV.Add(3, 0);
                                }
                                var key = stock.StockID + "-" + stock.GroupOfProductID + "-" + stock.ProductID;
                                dicQP.Add(key, dicV);
                            }
                        }
                    }
                }
                //Mỗi dòng 1 kho, check kho, lấy sản lượng theo excel.
                else
                {
                    int sID = -1;
                    if (objSetting.LocationFromCode < 1 && objSetting.LocationFromCodeName < 1)
                    {
                        if (data.ListStock.Count(c => c.CustomerID == cusID) == 1)
                        {
                            var objCheck = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID);

                            sID = objCheck.CUSLocationID;
                            dicL.Add(sID, objCheck);
                        }
                    }
                    else
                    {
                        var sCode = excelInput["LocationFromCode"];
                        if (objSetting.LocationFromCodeName > 0)
                        {
                            if (!string.IsNullOrEmpty(excelInput["LocationFromCodeName"]))
                            {
                                sCode = excelInput["LocationFromCodeName"].Split('-').FirstOrDefault();
                            }
                            else
                            {
                                sCode = string.Empty;
                            }
                        }
                        var objCheck = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.ToLower().Trim() == sCode.ToLower().Trim());
                        if (objCheck != null)
                        {
                            sID = objCheck.CUSLocationID;
                            dicL.Add(sID, objCheck);
                        }
                        else
                        {
                            dicL.Add(-1, new DTOORDData_Location());
                        }
                    }


                    if (!string.IsNullOrEmpty(excelInput["Ton_SKU"]) || !string.IsNullOrEmpty(excelInput["CBM_SKU"]) || !string.IsNullOrEmpty(excelInput["Quantity_SKU"]))
                    {
                        Dictionary<int, double> dicV = new Dictionary<int, double>();
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Ton_SKU"]))
                                dicV.Add(1, Convert.ToDouble(excelInput["Ton_SKU"]));
                            else
                                dicV.Add(1, 0);
                        }
                        catch
                        {
                            dicV.Add(1, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["CBM_SKU"]))
                                dicV.Add(2, Convert.ToDouble(excelInput["CBM_SKU"]));
                            else
                                dicV.Add(2, 0);
                        }
                        catch
                        {
                            dicV.Add(2, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Quantity_SKU"]))
                                dicV.Add(3, Convert.ToDouble(excelInput["Quantity_SKU"]));
                            else
                                dicV.Add(3, 0);
                        }
                        catch
                        {
                            dicV.Add(3, 0);
                        }
                        dicQ.Add(sID, dicV);
                    }
                    else
                    {
                        Dictionary<int, double> dicV = new Dictionary<int, double>();
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Ton"]))
                                dicV.Add(1, Convert.ToDouble(excelInput["Ton"]));
                            else
                                dicV.Add(1, 0);
                        }
                        catch
                        {
                            dicV.Add(1, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["CBM"]))
                                dicV.Add(2, Convert.ToDouble(excelInput["CBM"]));
                            else
                                dicV.Add(2, 0);
                        }
                        catch
                        {
                            dicV.Add(2, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Quantity"]))
                                dicV.Add(3, Convert.ToDouble(excelInput["Quantity"]));
                            else
                                dicV.Add(3, 0);
                        }
                        catch
                        {
                            dicV.Add(3, 0);
                        }
                        dicQ.Add(sID, dicV);
                    }
                }

                string strGopCode = string.Empty;
                //Dictionary Product theo GroupProduct. [P = Product] - Key: GroupOfProductID
                Dictionary<int, int> dicP = new Dictionary<int, int>();
                //Dictionary ProductCode theo GroupProduct. [PCode = ProductCode] - Key: GroupOfProductID
                Dictionary<int, string> dicPCode = new Dictionary<int, string>();
                Dictionary<int, bool> dicPIsKG = new Dictionary<int, bool>();

                //Nếu không có cột nhóm SP, check sản phẩm ko nhóm (ProductCodeWithoutGroup)
                //Nếu không có cột nhóm SP, kiểm tra kho có duy nhất nhóm SP => Lấy
                if (objSetting.GroupProductCode == 0 && objSetting.GroupProductCodeNotUnicode == 0)
                {
                    if (objSetting.ProductCodeWithoutGroup > 0)
                    {
                        if (!string.IsNullOrEmpty(excelInput["ProductCodeWithoutGroup"]))
                        {
                            var objP = data.ListProduct.FirstOrDefault(c => c.Code == excelInput["ProductCodeWithoutGroup"] && c.CustomerID == cusID);
                            if (objP != null)
                            {
                                foreach (var st in dicQ)
                                {
                                    var objGS = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == objP.GroupOfProductID && c.CUSStockID == st.Key);
                                    if (objGS != null)
                                    {
                                        strGopCode = objGS.Code;
                                        dicGS.Add(st.Key, objGS);
                                        dicP.Add(objGS.ID, objP.ID);
                                        dicPCode.Add(objGS.ID, objP.Code);
                                        dicPIsKG.Add(objGS.ID, objP.IsKg);
                                    }
                                    else
                                    {
                                        dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var st in dicQ)
                        {
                            var dataGS = data.ListGroupOfProduct.Where(c => c.CustomerID == cusID && c.CUSStockID == st.Key).ToList();
                            if (dataGS.Count == 0)
                            {
                                dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                            }
                            else if (dataGS.Count == 1)
                            {
                                var objCheck = dataGS.FirstOrDefault();
                                strGopCode = objCheck.Code;
                                dicGS.Add(st.Key, objCheck);
                            }
                            else
                            {
                                var objCheck = dataGS.FirstOrDefault(c => c.IsDefault == true);
                                if (objCheck != null)
                                {
                                    strGopCode = objCheck.Code;
                                    dicGS.Add(st.Key, objCheck);
                                }
                                else
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                            }
                        }
                    }
                }
                //Kiểm tra nhóm SP có tồn tại + có trong kho.
                else if(objSetting.ProductCodeWithoutGroup == 0)
                {
                    if (objSetting.GroupProductCode > 0)
                        strGopCode = excelInput["GroupProductCode"];
                    else
                        strGopCode = StringHelper.RemoveSign4VietnameseString(excelInput["GroupProductCodeNotUnicode"]);

                    if (!string.IsNullOrEmpty(strGopCode))
                    {
                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.CustomerID == cusID && c.Code.Trim().ToLower() == strGopCode.Trim().ToLower());
                        if (objGop != null)
                        {
                            foreach (var st in dicQ)
                            {
                                if (data.ListGroupOfProduct.Count(c => c.CustomerID == cusID && c.ID == objGop.ID && c.CUSStockID == st.Key) == 0)
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                                else
                                {
                                    var objCheck = data.ListGroupOfProduct.FirstOrDefault(c => c.CustomerID == cusID && c.ID == objGop.ID && c.CUSStockID == st.Key);
                                    dicGS.Add(st.Key, objCheck);
                                }
                            }
                        }
                        else
                        {
                            dicGS.Add(-1, new DTOORDData_GroupProduct());
                        }
                    }
                    else
                    {
                        foreach (var st in dicQ)
                        {
                            var dataGS = data.ListGroupOfProduct.Where(c => c.CustomerID == cusID && c.CUSStockID == st.Key).ToList();
                            if (dataGS.Count == 0)
                            {
                                dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                            }
                            else if (dataGS.Count == 1)
                            {
                                var objCheck = dataGS.FirstOrDefault();
                                strGopCode = objCheck.Code;
                                dicGS.Add(st.Key, objCheck);
                            }
                            else
                            {
                                var objCheck = dataGS.FirstOrDefault(c => c.IsDefault == true);
                                if (objCheck != null)
                                {
                                    strGopCode = objCheck.Code;
                                    dicGS.Add(st.Key, objCheck);
                                }
                                else
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(excelInput["ProductCodeWithoutGroup"]))
                    {
                        var objP = data.ListProduct.FirstOrDefault(c => c.Code == excelInput["ProductCodeWithoutGroup"] && c.CustomerID == cusID);
                        if (objP != null)
                        {
                            foreach (var st in dicQ)
                            {
                                var objGS = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == objP.GroupOfProductID && c.CUSStockID == st.Key);
                                if (objGS != null)
                                {
                                    strGopCode = objGS.Code;
                                    dicGS.Add(st.Key, objGS);
                                    dicP.Add(objGS.ID, objP.ID);
                                    dicPCode.Add(objGS.ID, objP.Code);
                                    dicPIsKG.Add(objGS.ID, objP.IsKg);
                                }
                                else
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                            }
                        }
                    }
                }

                if (objSetting.ProductCodeWithoutGroup == 0)
                {
                    if (objSetting.Packing == 0 && objSetting.PackingNotUnicode == 0)
                    {
                        foreach (var gop in dicGS)
                        {
                            if (!dicP.ContainsKey(gop.Value.ID))
                            {
                                var dataProduct = data.ListProduct.Where(c => c.GroupOfProductID == gop.Value.ID && c.CustomerID == cusID).ToList();
                                if (dataProduct.Count == 1)
                                {
                                    dicP.Add(gop.Value.ID, dataProduct[0].ID);
                                    dicPCode.Add(gop.Value.ID, dataProduct[0].Code);
                                    dicPIsKG.Add(gop.Value.ID, dataProduct[0].IsKg);
                                }
                                else
                                {
                                    var objDefault = dataProduct.FirstOrDefault(c => c.IsDefault == true);
                                    if (objDefault != null)
                                    {
                                        dicP.Add(gop.Value.ID, objDefault.ID);
                                        dicPCode.Add(gop.Value.ID, objDefault.Code);
                                        dicPIsKG.Add(gop.Value.ID, objDefault.IsKg);
                                    }
                                    else
                                    {
                                        dicP.Add(gop.Value.ID, -1);
                                        dicPCode.Add(gop.Value.ID, "");
                                        dicPIsKG.Add(gop.Value.ID, false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var gop in dicGS)
                        {
                            if (!dicP.ContainsKey(gop.Value.ID))
                            {
                                var dataProduct = data.ListProduct.Where(c => c.GroupOfProductID == gop.Value.ID && c.CustomerID == cusID).ToList();
                                if (dataProduct.Count > 0)
                                {
                                    var str = string.Empty;
                                    if (objSetting.Packing > 0)
                                        str = excelInput["Packing"];
                                    else if (objSetting.PackingNotUnicode > 0)
                                        str = StringHelper.RemoveSign4VietnameseString(excelInput["PackingNotUnicode"]);

                                    if (string.IsNullOrEmpty(str))
                                    {
                                        var objDefault = dataProduct.FirstOrDefault(c => c.IsDefault == true);
                                        if (objDefault != null)
                                        {
                                            dicP.Add(gop.Value.ID, objDefault.ID);
                                            dicPCode.Add(gop.Value.ID, objDefault.Code);
                                            dicPIsKG.Add(gop.Value.ID, objDefault.IsKg);
                                        }
                                        else
                                        {
                                            dicP.Add(gop.Value.ID, -1);
                                            dicPCode.Add(gop.Value.ID, "");
                                            dicPIsKG.Add(gop.Value.ID, false);
                                        }
                                    }
                                    else
                                    {
                                        var product = dataProduct.FirstOrDefault(c => c.Code.ToLower().Trim() == str.ToLower().Trim());
                                        if (product != null)
                                        {
                                            dicP.Add(gop.Value.ID, product.ID);
                                            dicPCode.Add(gop.Value.ID, product.Code);
                                            dicPIsKG.Add(gop.Value.ID, product.IsKg);
                                        }
                                        else
                                        {
                                            dicP.Add(gop.Value.ID, -1);
                                            dicPCode.Add(gop.Value.ID, "");
                                            dicPIsKG.Add(gop.Value.ID, false);
                                        }
                                    }
                                }
                                else
                                {
                                    dicP.Add(gop.Value.ID, -1);
                                    dicPCode.Add(gop.Value.ID, "");
                                    dicPIsKG.Add(gop.Value.ID, false);
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Lưu dữ liệu

                foreach (var dic in dicQ)
                {
                    var gop = new DTOORDData_GroupProduct();
                    try
                    {
                        gop = dicGS[dic.Key];
                    }
                    catch
                    {
                        gop.Code = excelInput["GroupProductCode"];
                    }

                    var item = new DTOOPSDIImportPacket_GroupProduct();
                    item.IsFTL = transportID == iFTL;
                    item.SOCode = excelInput["SOCode"];
                    item.DNCode = excelInput["DNCode"];
                    item.OrderCode = excelInput["OrderCode"];
                    item.GroupID = gop.ID;
                    item.GroupCode = gop.Code;
                    item.GroupName = gop.GroupName;
                    try
                    {
                        item.ProductID = dicP[gop.ID];
                        item.ProductCode = dicPCode[gop.ID];
                    }
                    catch
                    { }
                    item.LocationToID = eItem.LocationToID;
                    item.LocationFromID = dic.Key;

                    item.ETD = eItem.ETD;
                    item.ETA = eItem.ETA;
                    item.ETARequest = eItem.ETARequest;

                    var objProduct = data.ListProduct.FirstOrDefault(c => c.ID == item.ProductID && c.CustomerID == cusID);
                    if (objProduct != null)
                    {
                        item.Ton = dic.Value[1];
                        item.CBM = Math.Round(dic.Value[2], digitsOfRound, MidpointRounding.AwayFromZero);
                        item.Quantity = Math.Round(dic.Value[3], digitsOfRound, MidpointRounding.AwayFromZero);
                        if (objProduct.IsKg)
                            item.Ton = item.Ton / 1000;
                        item.Ton = Math.Round(item.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                    }
                    dataRes.Add(item);
                }
                foreach (var dic in dicQP)
                {
                    var tmp = dic.Key.ToString().Split('-').ToList();
                    var item = new DTOOPSDIImportPacket_GroupProduct();
                    item.IsFTL = transportID == iFTL;
                    item.SOCode = excelInput["SOCode"];
                    item.DNCode = excelInput["DNCode"];
                    item.OrderCode = excelInput["OrderCode"];
                    var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID.ToString() == tmp[0] && c.CustomerID == cusID);
                    var cusGroup = data.ListGroupOfProduct.FirstOrDefault(c => c.CUSStockID.ToString() == tmp[0] && c.ID.ToString() == tmp[1]);
                    var cusProduct = data.ListProduct.FirstOrDefault(c => c.GroupOfProductID.ToString() == tmp[1] && c.ID.ToString() == tmp[2]);
                    if (cusStock != null && cusGroup != null && cusProduct != null)
                    {
                        item.GroupID = cusGroup.ID;
                        item.GroupCode = cusGroup.Code;
                        item.GroupName = cusGroup.GroupName;
                        item.ProductID = cusProduct.ID;
                        item.ProductCode = cusProduct.Code;
                        item.LocationToID = eItem.LocationToID;
                        item.LocationFromID = cusStock.CUSLocationID;
                        item.ETD = eItem.ETD;
                        item.ETA = eItem.ETA;
                        item.ETARequest = eItem.ETARequest;
                        item.Ton = dic.Value[1];
                        item.CBM = Math.Round(dic.Value[2], digitsOfRound, MidpointRounding.AwayFromZero);
                        item.Quantity = Math.Round(dic.Value[3], digitsOfRound, MidpointRounding.AwayFromZero);
                        if (cusProduct.IsKg)
                            item.Ton = item.Ton / 1000;
                        item.Ton = Math.Round(item.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                        dataRes.Add(item);
                    }
                }

                #endregion

                #endregion
            }

            return dataRes;
        }

        #endregion

        #region NewCO - Import

        [HttpPost]
        public DTOResult OPS_CO_Import_Packet_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                bool isCreated = (bool)dynParam.IsCreated;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_List(request, isCreated);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_CO_Import_Packet_Setting_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_CO_Import_Packet_TOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_TOMaster_List(request, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_CO_Import_Packet_Container_ByMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int masterID = (int)dynParam.masterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_Container_ByMaster_List(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_CO_Import_Packet_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_Container_List(request, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSSettingPlan> OPS_CO_Import_Packet_SettingPlan(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCUSSettingPlan>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_SettingPlan();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDIImportPacket OPS_CO_Import_Packet_Get(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDIImportPacket();
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_Get(pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_CO_Import_Packet_ORDContainer_List(dynamic dynParam)
        {
            try
            {
                var result = default(DTOResult);
                int pID = (int)dynParam.pID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_ORDContainer_List(request, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPS_CO_Import_Packet_ORDContainer_NotIn_List(dynamic dynParam)
        {
            try
            {
                var result = default(DTOResult);
                int sID = (int)dynParam.sID;
                int pID = (int)dynParam.pID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_ORDContainer_NotIn_List(request, sID, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string OPS_CO_Import_Packet_DownLoad(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                var sID = (int)dynParam.sID;
                var pID = (int)dynParam.pID;
                var objSetting = new DTOCUSSettingPlan();
                var objSettingOrder = new DTOCUSSettingOrder();
                var dataOrder = new DTOORDOrder_ImportCheck();
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                var dataExport = new List<DTOOPSCOImportPacket_ContainerExport>();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    objSetting = sv.OPS_CO_Import_Packet_Setting_Get(sID);
                    dataExport = sv.OPS_CO_Import_Packet_ORDContainerExport_List(pID, data);
                });

                if (objSetting != null && objSetting.CUSSettingOrderID > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        objSettingOrder = sv.ORDOrder_Excel_Setting_Get(objSetting.CUSSettingOrderID);
                    });
                    if (objSettingOrder == null)
                        throw new Exception("Không tìm thấy thiết lập đơn hàng.");
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        dataOrder = sv.ORDOrder_Excel_Import_Data(objSettingOrder.CustomerID);
                    });
                    if (objSetting.FileID > 0)
                    {
                        string[] name = objSetting.FileName.Split('.').Reverse().Skip(1).Reverse().ToArray();
                        result = "/" + FolderUpload.Export + string.Join(".", name) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + objSetting.FilePath)))
                        {
                            System.IO.File.Copy(HttpContext.Current.Server.MapPath("/" + objSetting.FilePath), HttpContext.Current.Server.MapPath(result), true);
                        }
                        else
                        {
                            throw new Exception("Không tìm thấy file mẫu!");
                        }
                    }
                    else
                    {
                        throw new Exception("Chưa thiết lập file mẫu!");
                    }

                    FileInfo exportFile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                    using (var package = new ExcelPackage(exportFile))
                    {
                        ExcelWorksheet ws = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (ws != null)
                        {
                            var sValue = new List<string>(new string[]{ "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart",
                                                  "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID" });

                            //Empty WS
                            var iRow = ws.Dimension.End.Row;
                            if (iRow > objSettingOrder.RowStart)
                            {
                                for (var row = iRow; row >= objSettingOrder.RowStart; row--)
                                {
                                    ws.DeleteRow(row);
                                }
                            }

                            var cRow = objSettingOrder.RowStart;
                            List<string> timeProps = new List<string>(new string[] { "RequestTime", "ETARequestTime", "TimeGetEmpty", "TimeReturnEmpty" });

                            foreach (var item in dataExport)
                            {
                                foreach (var prop in objSettingOrder.GetType().GetProperties())
                                {
                                    try
                                    {
                                        var p = prop.Name;
                                        if (!sValue.Contains(p))
                                        {
                                            var v = (int)prop.GetValue(objSettingOrder, null);
                                            var val = item.GetType().GetProperty(p).GetValue(item, null);
                                            var txt = string.Empty;
                                            if (val != null)
                                            {
                                                if (val.GetType() == typeof(DateTime))
                                                {
                                                    if (timeProps.Contains(p))
                                                    {
                                                        txt = String.Format("{0:HH:mm}", val);
                                                    }
                                                    else
                                                    {
                                                        txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                    }
                                                }
                                                else if (val.GetType() == typeof(TimeSpan))
                                                {
                                                    txt = val.ToString();
                                                }
                                                else
                                                {
                                                    txt = val.ToString();
                                                }
                                            }
                                            ws.Cells[cRow, v].Value = txt;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                cRow++;
                            }
                            package.Save();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCOImportPacket_Data OPS_CO_Import_Packet_Data(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                var result = new DTOOPSCOImportPacket_Data();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_Data(pID, new List<string>());
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int OPS_CO_Import_Packet_Save(dynamic dynParam)
        {
            try
            {
                int result = 0;
                DTOOPSCOImportPacket item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCOImportPacket>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_Import(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                int templateID = (int)dynParam.templateID;
                List<DTOOPSCOImportPacketTOMaster_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCOImportPacketTOMaster_Import>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_Import(pID, templateID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_2View_Save(dynamic dynParam)
        {
            try
            {
                List<DTOOPSCOImportPacketTOMaster> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCOImportPacketTOMaster>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_2View_Save(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_ToOPS(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_ToOPS(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_ToMON(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_ToMON(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_Delete(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_Delete(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_Reset(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_Reset(pID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_Vehicle_Update(dynamic dynParam)
        {
            try
            {
                List<DTOOPSDIImportPacket_Vehicle> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSDIImportPacket_Vehicle>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_Vehicle_Update(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_ORDContainer_SaveList(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_ORDContainer_SaveList(pID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPS_CO_Import_Packet_ORDContainer_DeleteList(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPS_CO_Import_Packet_ORDContainer_DeleteList(pID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCOImportPacketTOMaster_Import> OPS_CO_Import_Packet_Check(dynamic dynParam)
        {
            try
            {
                int pID = (int)dynParam.pID;
                int templateID = (int)dynParam.templateID;
                string file = "/" + dynParam.file.ToString();
                var dataTOMaster = new List<DTOOPSCOImportPacketTOMaster_Import>();
                DTOCUSSettingPlan objSetting = new DTOCUSSettingPlan();
                DTOCUSSettingOrder objSettingOrder = new DTOCUSSettingOrder();
                DTOOPSCOImportPacket_Data data = new DTOOPSCOImportPacket_Data();
                DTOORDOrder_ImportCheck dataOrder = new DTOORDOrder_ImportCheck();

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    objSetting = sv.OPS_DI_Import_Packet_Setting_Get(templateID);
                });

                if (objSetting != null)
                {
                    string[] aValue = { "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart", "HasStockProduct",
                                        "StockID", "GroupOfProductID", "ProductID", "ListStockWithProduct", "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID" };
                    var sValue = new List<string>(aValue);
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        objSettingOrder = sv.ORDOrder_Excel_Setting_Get(objSetting.CUSSettingOrderID);
                    });
                    if (objSettingOrder == null)
                        throw new Exception("Không tìm thấy thiết lập đơn hàng.");
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        dataOrder = sv.ORDOrder_Excel_Import_Data(objSettingOrder.CustomerID);
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                List<string> dataOrders = new List<string>();
                                for (row = objSettingOrder.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var excelInput = GetDataValue(worksheet, objSettingOrder, row, sValue);
                                    if (!string.IsNullOrEmpty(excelInput["OrderCode"]))
                                        dataOrders.Add(excelInput["OrderCode"].Trim().ToLower());
                                }
                                if (dataOrders.Count == 0)
                                    throw new Exception("Không có thông tin đơn hàng.");
                                ServiceFactory.SVOperation((ISVOperation sv) =>
                                {
                                    data = sv.OPS_CO_Import_Packet_Data(pID, dataOrders);
                                });

                                int idx = 1;
                                Dictionary<int, int> dicTOMaster = new Dictionary<int, int>();
                                for (row = objSettingOrder.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var excelError = new List<string>();
                                    var excelInput = GetDataValue(worksheet, objSettingOrder, row, sValue);

                                    var excelPlanInput = GetDataValue(worksheet, objSetting, row, sValue);
                                    if (!string.IsNullOrEmpty(excelPlanInput["VehicleNo"]))
                                    {
                                        DateTime? ETD = null;
                                        if (objSetting.MasterETDDate < 1 && objSetting.MasterETDDate_Time < 1)
                                            throw new Exception("Chưa thiết lập cột ETD.");
                                        try
                                        {
                                            ETD = ExcelHelper.ValueToDate(excelPlanInput["MasterETDDate"]);
                                        }
                                        catch (Exception)
                                        {
                                            try
                                            {
                                                ETD = Convert.ToDateTime(excelPlanInput["MasterETDDate"], new CultureInfo("vi-VN"));
                                            }
                                            catch { }
                                        }
                                        if (objSetting.MasterETDTime > 0 && ETD != null)
                                        {
                                            if (!string.IsNullOrEmpty(excelPlanInput["MasterETDTime"]))
                                            {
                                                try
                                                {
                                                    ETD = ETD.Value.Date.Add(TimeSpan.Parse(excelPlanInput["MasterETDTime"]));
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai giờ ETD.");
                                                }
                                            }
                                        }
                                        if (objSetting.MasterETDDate_Time > 0)
                                        {
                                            try
                                            {
                                                ETD = ExcelHelper.ValueToDate(excelPlanInput["MasterETDDate_Time"]);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    ETD = Convert.ToDateTime(excelPlanInput["MasterETDDate_Time"], new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai ETD.");
                                                }
                                            }
                                        }

                                        DateTime? ETA = null;
                                        if (objSetting.MasterHours > 0)
                                        {
                                            if (ETD != null)
                                            {
                                                try
                                                {
                                                    ETA = ETD.Value.AddHours(Convert.ToDouble(excelPlanInput["MasterHours"]));
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai thời gian chuyến.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.MasterETADate < 1 && objSetting.MasterETADate_Time < 1)
                                                throw new Exception("Chưa thiết lập cột ETA.");
                                            try
                                            {
                                                ETA = ExcelHelper.ValueToDate(excelPlanInput["MasterETADate"]);
                                            }
                                            catch (Exception)
                                            {
                                                try
                                                {
                                                    ETA = Convert.ToDateTime(excelPlanInput["MasterETADate"], new CultureInfo("vi-VN"));
                                                }
                                                catch { }
                                            }
                                            if (objSetting.MasterETATime > 0 && ETA != null)
                                            {
                                                if (!string.IsNullOrEmpty(excelPlanInput["MasterETATime"]))
                                                {
                                                    try
                                                    {
                                                        ETA = ETD.Value.Date.Add(TimeSpan.Parse(excelPlanInput["MasterETATime"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ ETA.");
                                                    }
                                                }
                                            }
                                            if (objSetting.MasterETADate_Time > 0)
                                            {
                                                try
                                                {
                                                    ETA = ExcelHelper.ValueToDate(excelPlanInput["MasterETADate_Time"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        ETA = Convert.ToDateTime(excelPlanInput["MasterETADate_Time"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai ETA.");
                                                    }
                                                }
                                            }
                                        }

                                        if (ETA != null && ETD != null && ETD >= ETA)
                                            excelError.Add("Sai ràng buộc thời gian ETD-ETA");


                                        var isNewVehicle = false; var isNewRomooc = false;
                                        int venID = -1, vehID = -1, romID = -1;
                                        string venCode = excelPlanInput["VendorCode"].Trim(), vehCode = excelPlanInput["VehicleNo"].Trim(), romCode = excelPlanInput["RomoocNo"].Trim();
                                        string driverName = excelPlanInput["DriverName"], driverTel = excelPlanInput["DriverTel"];

                                        #region Kiểm tra chuyến
                                        if (!string.IsNullOrEmpty(excelPlanInput["VendorCode"]))
                                        {
                                            var objVendor = data.ListVendor.FirstOrDefault(c => c.VendorCode.Trim().ToLower() == excelPlanInput["VendorCode"].Trim().ToLower());
                                            if (objVendor != null)
                                            {
                                                venID = objVendor.ID;
                                                venCode = objVendor.VendorCode;
                                            }
                                            else
                                            {
                                                excelError.Add("Nhà xe [" + excelPlanInput["VendorCode"] + "] không tồn tại.");
                                            }
                                        }
                                        else
                                        {
                                            var objVendor = data.ListVendor.FirstOrDefault(c => c.IsVendor == false);
                                            if (objVendor != null)
                                            {
                                                venID = objVendor.ID;
                                                venCode = "Xe nhà";
                                            }
                                            else
                                            {
                                                excelError.Add("Nhà xe không xác định.");
                                            }
                                        }

                                        var dataCode = new List<string>();
                                        var dataChar = new char[] { '-', ' ', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', '/', '?', '<', ',', '>', '~', '`' };
                                        var sCode = vehCode.Split(dataChar, StringSplitOptions.RemoveEmptyEntries).ToArray();
                                        foreach (var c in dataChar)
                                        {
                                            dataCode.Add(string.Join(c.ToString(), sCode).ToLower());
                                        }

                                        var objVehicle = data.ListVehicle.FirstOrDefault(c => c.VendorID == venID && dataCode.Contains(c.VehicleNo.Trim().ToLower()));
                                        if (objVehicle != null)
                                        {
                                            vehID = objVehicle.ID;
                                            vehCode = objVehicle.VehicleNo;
                                            if (venID > 0 && venCode == "Xe nhà")
                                            {
                                                var objPlanning = data.ListVehiclePlan.FirstOrDefault(c => c.VehicleID == vehID && !((c.DateFrom < ETD && c.DateTo < ETD) || (c.DateFrom > ETA && c.DateTo > ETA)));
                                                if (objPlanning != null)
                                                {
                                                    var objDriver = data.ListDriver.FirstOrDefault(c => c.ID == objPlanning.DriverID);
                                                    if (objDriver != null)
                                                    {
                                                        driverName = objDriver.DriverName;
                                                        driverTel = objDriver.DriverTel;
                                                    }
                                                }
                                                else
                                                {
                                                    driverName = objVehicle.DriverName;
                                                    driverTel = objVehicle.DriverTel;
                                                }
                                                if (string.IsNullOrEmpty(driverName))
                                                    excelError.Add("Không có thông tin tài xế.");
                                            }
                                        }
                                        else
                                        {
                                            if (venID > 0)
                                            {
                                                if (venCode != "Xe nhà")
                                                {
                                                    isNewVehicle = true;
                                                }
                                                excelError.Add("Xe [" + excelPlanInput["VehicleNo"] + "] không tồn tại.");
                                            }
                                        }

                                        dataCode = new List<string>();
                                        sCode = romCode.Split(dataChar, StringSplitOptions.RemoveEmptyEntries).ToArray();
                                        foreach (var c in dataChar)
                                        {
                                            dataCode.Add(string.Join(c.ToString(), sCode).ToLower());
                                        }

                                        // Nếu ko có romooc và vendor là xe nhà => ktra lấy romooc mặc định
                                        if (string.IsNullOrEmpty(romCode.Trim()) && venCode == "Xe nhà")
                                        {
                                            if (objVehicle != null && objVehicle.CurrentRomoocID > 0)
                                            {
                                                romID = objVehicle.CurrentRomoocID;
                                                romCode = objVehicle.CurrentRomoocNo;
                                            }
                                            else
                                                excelError.Add("Không có romooc.");
                                        }
                                        else
                                        {
                                            var objRomooc = data.ListRomooc.FirstOrDefault(c => c.VendorID == venID && dataCode.Contains(c.VehicleNo.Trim().ToLower()));
                                            if (objRomooc != null)
                                            {
                                                romID = objRomooc.ID;
                                                romCode = objRomooc.VehicleNo;
                                            }
                                            else
                                            {
                                                if (venID > 0)
                                                {
                                                    if (venCode != "Xe nhà")
                                                    {
                                                        isNewRomooc = true;
                                                    }
                                                    excelError.Add("Romooc [" + excelPlanInput["RomoocNo"] + "] không tồn tại.");
                                                }
                                            }
                                        }
                                        

                                        #endregion

                                        #region Lưu thông tin chuyến
                                        var obj = new DTOOPSCOImportPacketTOMaster_Import();
                                        if (venID > 0 && vehID > 0 && ETD != null && ETA != null)
                                        {
                                            obj = dataTOMaster.FirstOrDefault(c => c.VendorID == venID && (c.VehicleID == vehID || c.VehicleNo == vehCode) && c.ETD == ETD);
                                            if (obj == null)
                                            {
                                                obj = new DTOOPSCOImportPacketTOMaster_Import();
                                                obj.ETD = ETD;
                                                obj.ETA = ETA;
                                                obj.IsNewVendorVehicle = isNewVehicle;
                                                obj.IsNewVendorRomooc = isNewRomooc;
                                                obj.SortOrder = idx++;
                                                obj.VehicleID = vehID;
                                                obj.RomoocID = romID;
                                                obj.VehicleNo = vehCode;
                                                obj.RomoocNo = romCode;
                                                obj.VendorID = venID;
                                                obj.VendorCode = venCode;
                                                obj.DriverTel = driverTel;
                                                obj.DriverName = driverName;
                                                obj.Note = excelPlanInput["MasterNote"];
                                                obj.Error = new List<string>();
                                                obj.ListContainer = new List<DTOOPSCOImportPacket_Container>();
                                                foreach (var item in excelError)
                                                {
                                                    obj.Error.Add(item);
                                                }
                                                dataTOMaster.Add(obj);
                                                dicTOMaster.Add(row, obj.SortOrder);
                                            }
                                        }
                                        else
                                        {
                                            obj.ETD = ETD;
                                            obj.ETA = ETA;
                                            obj.IsNewVendorVehicle = isNewVehicle;
                                            obj.IsNewVendorRomooc = isNewRomooc;
                                            obj.SortOrder = idx++;
                                            obj.VehicleID = vehID;
                                            obj.RomoocID = romID;
                                            obj.VehicleNo = vehCode;
                                            obj.RomoocNo = romCode;
                                            obj.VendorID = venID;
                                            obj.VendorCode = venCode;
                                            obj.DriverTel = driverTel;
                                            obj.DriverName = driverName;
                                            obj.Note = excelPlanInput["MasterNote"];
                                            obj.Error = new List<string>();
                                            obj.ListContainer = new List<DTOOPSCOImportPacket_Container>();
                                            foreach (var item in excelError)
                                            {
                                                obj.Error.Add(item);
                                            }
                                            dataTOMaster.Add(obj);
                                            dicTOMaster.Add(row, obj.SortOrder);
                                        }
                                        #endregion

                                        #region Kiểm tra container
                                        var objCopy = new CopyHelper();
                                        var oData = GetDataContainer(worksheet, row, excelInput, dataOrder, objSettingOrder, sValue);
                                        var isSettingETD = objSettingOrder.ETD > 0 || objSettingOrder.ETDTime_RequestDate > 0;
                                        foreach (var o in oData)
                                        {
                                            var objCon = data.ListORDContainer.FirstOrDefault(c => c.TOMasterID == null && c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                               && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.LocationDepotID == o.LocationDepotID && c.LocationReturnID == o.LocationReturnID
                                                               && c.ContainerNo == o.ContainerNo && c.TypeOfWAInspectionStatus == o.TypeOfWAInspectionStatus && c.DateGetEmpty == o.DateGetEmpty && c.DateReturnEmpty == o.DateReturnEmpty
                                                               && c.ETA == o.ETA && c.ETD == o.ETD && c.CutOffTime == o.CutOffTime && c.PackingID == o.PackingID && c.PartnerID == o.PartnerID && c.CustomerID == o.CustomerID
                                                               && c.SealNo1 == o.SealNo1 && c.SealNo2 == o.SealNo2 && c.InspectionDate == o.InspectionDate && c.TripNo == o.TripNo && c.VesselName == o.VesselName && c.VesselNo == o.VesselNo);
                                            if (objCon != null)
                                            {
                                                foreach (var objCOTO in data.ListOPSContainer.Where(c => c.ORDContainerID == objCon.ID))
                                                {
                                                    obj.ListContainer.Add(objCOTO);
                                                }
                                            }
                                            else
                                            {
                                                obj.Error.Add("Không tìm thấy container. ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var item in dataTOMaster)
                {
                    if (item.Error.Count == 0)
                    {
                        item.ExcelSuccess = true;
                        var objV = data.ListVehicle.FirstOrDefault(c => c.VendorID == item.VendorID && c.VendorID == item.VendorID);
                        if (objV != null && item.ListContainer.Sum(c => c.Ton) > objV.MaxWeight)
                        {
                            item.ExcelError = "Quá trọng tải.";
                        }
                    }
                    else
                    {
                        item.ExcelSuccess = false;
                        item.ExcelError = string.Join(", ", item.Error);
                    }
                }

                return dataTOMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DTOOPSCOImportPacket_Container> GetDataContainer(ExcelWorksheet worksheet, int row, Dictionary<string, string> excelInput, DTOORDOrder_ImportCheck data, DTOCUSSettingOrder objSetting, List<string> sValue)
        {
            var dataRes = new List<DTOOPSCOImportPacket_Container>();

            int svID = -1, serviceID = -1, tmID = -1, transportID = -1;

            if (objSetting.TypeOfTransportModeID > 0)
            {
                tmID = objSetting.TypeOfTransportModeID;
                var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                if (objTM != null)
                    transportID = objTM.TransportModeID;
            }
            else
            {
                var str = excelInput["TypeOfTransportMode"];
                if (!string.IsNullOrEmpty(str))
                {
                    var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                    if (objTM != null)
                    {
                        tmID = objTM.ID;
                        transportID = objTM.TransportModeID;
                    }
                }
            }

            if (objSetting.ServiceOfOrderID > 0)
            {
                svID = objSetting.ServiceOfOrderID;
                var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.ID == svID);
                if (objSV != null)
                    serviceID = objSV.ServiceOfOrderID;
            }
            else
            {
                var str = excelInput["ServiceOfOrder"].Trim().ToLower();
                if (!string.IsNullOrEmpty(str))
                {
                    var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                    if (objSV != null)
                    {
                        svID = objSV.ID;
                        serviceID = objSV.ServiceOfOrderID;
                    }
                }
            }

            //Xe container
            if (transportID == iFCL)
            {
                int cusID = -1, fID = -1, tID = -1, toCoID = -1;
                int? crID = null, dpID = null, dprID = null;
                string cusCode = string.Empty, toCoName = string.Empty;
                string frCode = string.Empty, frName = string.Empty, toCode = string.Empty, toName = string.Empty;
                string dpCode = string.Empty, dpName = string.Empty, dprCode = string.Empty, dprName = string.Empty;
                string crCode = string.Empty, crName = string.Empty;
                double ton = 0; bool? isInspect = null;

                #region Check Customer và Code

                if (objSetting.CustomerID == objSetting.SYSCustomerID)
                {
                    if (!string.IsNullOrEmpty(excelInput["CustomerCode"]))
                    {
                        var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["CustomerCode"].Trim().ToLower());
                        if (objCheck != null)
                        {
                            cusID = objCheck.ID;
                            cusCode = objCheck.Code;
                        }
                    }
                }
                else
                {
                    cusID = objSetting.CustomerID;
                }
                #endregion

                #region Check tgian

                DateTime? requestDate = null;
                DateTime? eTD = null;
                DateTime? eTA = null;
                DateTime? eTARequest = null;
                DateTime? cutOffTime = null;
                DateTime? getDate = null;
                DateTime? returnDate = null;
                DateTime? inspectDate = null;

                if (objSetting.RequestDate > 0)
                {
                    try
                    {
                        requestDate = ExcelHelper.ValueToDate(excelInput["RequestDate"]);
                    }
                    catch
                    {
                        try
                        {
                            requestDate = Convert.ToDateTime(excelInput["RequestDate"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                    if (objSetting.RequestTime > 0 && requestDate != null)
                    {
                        if (!string.IsNullOrEmpty(excelInput["RequestTime"]))
                        {
                            try
                            {
                                requestDate = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["RequestTime"]));
                            }
                            catch { }
                        }
                    }
                }

                if (objSetting.RequestDate_Time > 0)
                {
                    try
                    {
                        requestDate = ExcelHelper.ValueToDate(excelInput["RequestDate_Time"]);
                    }
                    catch
                    {
                        try
                        {
                            requestDate = Convert.ToDateTime(excelInput["RequestDate_Time"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETD"]))
                {
                    try
                    {
                        eTD = ExcelHelper.ValueToDate(excelInput["ETD"]);
                    }
                    catch
                    {
                        try
                        {
                            eTD = Convert.ToDateTime(excelInput["ETD"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }
                else if (objSetting.ETDTime_RequestDate > 0 && requestDate != null)
                {
                    if (!string.IsNullOrEmpty(excelInput["ETDTime_RequestDate"]))
                    {
                        try
                        {
                            eTD = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["ETDTime_RequestDate"]));
                        }
                        catch { }
                    }
                    else
                    {
                        eTD = requestDate;
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETA"]))
                {
                    try
                    {
                        eTA = ExcelHelper.ValueToDate(excelInput["ETA"]);
                    }
                    catch
                    {
                        try
                        {
                            eTA = Convert.ToDateTime(excelInput["ETA"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }
                else if (objSetting.ETATime_RequestDate > 0 && requestDate != null)
                {
                    if (!string.IsNullOrEmpty(excelInput["ETATime_RequestDate"]))
                    {
                        try
                        {
                            eTA = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["ETATime_RequestDate"]));
                        }
                        catch { }
                    }
                    else
                    {
                        eTA = requestDate;
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETARequest"]))
                {
                    try
                    {
                        eTARequest = ExcelHelper.ValueToDate(excelInput["ETARequest"]);
                    }
                    catch
                    {
                        try
                        {
                            eTARequest = Convert.ToDateTime(excelInput["ETARequest"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                    if (eTARequest != null && objSetting.ETARequestTime > 0)
                    {
                        if (!string.IsNullOrEmpty(excelInput["ETARequestTime"]))
                        {
                            try
                            {
                                eTARequest = eTARequest.Value.Date.Add(TimeSpan.Parse(excelInput["ETARequestTime"]));
                            }
                            catch { }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["CutOffTime"]))
                {
                    try
                    {
                        cutOffTime = ExcelHelper.ValueToDate(excelInput["CutOffTime"]);
                    }
                    catch
                    {
                        try
                        {
                            cutOffTime = Convert.ToDateTime(excelInput["CutOffTime"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["Date_TimeGetEmpty"]))
                {
                    try
                    {
                        getDate = ExcelHelper.ValueToDate(excelInput["Date_TimeGetEmpty"]);
                    }
                    catch
                    {
                        try
                        {
                            getDate = Convert.ToDateTime(excelInput["Date_TimeGetEmpty"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(excelInput["DateGetEmpty"]))
                    {
                        try
                        {
                            getDate = ExcelHelper.ValueToDate(excelInput["DateGetEmpty"]);
                        }
                        catch
                        {
                            try
                            {
                                getDate = Convert.ToDateTime(excelInput["DateGetEmpty"], new CultureInfo("vi-VN"));
                            }
                            catch { }
                        }
                    }
                    if (getDate != null && !string.IsNullOrEmpty(excelInput["TimeGetEmpty"]))
                    {
                        try
                        {
                            getDate = getDate.Value.Date.Add(TimeSpan.Parse(excelInput["TimeGetEmpty"]));
                        }
                        catch { }
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["Date_TimeReturnEmpty"]))
                {
                    try
                    {
                        returnDate = ExcelHelper.ValueToDate(excelInput["Date_TimeReturnEmpty"]);
                    }
                    catch
                    {
                        try
                        {
                            returnDate = Convert.ToDateTime(excelInput["Date_TimeReturnEmpty"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(excelInput["DateReturnEmpty"]))
                    {
                        try
                        {
                            returnDate = ExcelHelper.ValueToDate(excelInput["DateReturnEmpty"]);
                        }
                        catch
                        {
                            try
                            {
                                returnDate = Convert.ToDateTime(excelInput["DateReturnEmpty"], new CultureInfo("vi-VN"));
                            }
                            catch { }
                        }
                    }
                    if (getDate != null && !string.IsNullOrEmpty(excelInput["TimeReturnEmpty"]))
                    {
                        try
                        {
                            returnDate = returnDate.Value.Date.Add(TimeSpan.Parse(excelInput["TimeReturnEmpty"]));
                        }
                        catch { }
                    }
                }

                #endregion

                #region Check Carrier, Depot, From, To

                frCode = excelInput["LocationFromCode"];
                frName = excelInput["LocationFromName"];
                if (objSetting.LocationFromCodeName > 0)
                {
                    if (!string.IsNullOrEmpty(excelInput["LocationFromCodeName"]))
                    {
                        frCode = excelInput["LocationFromCodeName"].Split('-').FirstOrDefault();
                        frCode = excelInput["LocationFromCodeName"].Split('-').Skip(1).FirstOrDefault();
                    }
                    else
                    {
                        frCode = string.Empty;
                        frCode = string.Empty;
                    }
                }

                toCode = excelInput["LocationToCode"];
                toName = excelInput["LocationToName"];
                if (objSetting.LocationToCodeName > 0)
                {
                    if (!string.IsNullOrEmpty(excelInput["LocationToCodeName"]))
                    {
                        toCode = excelInput["LocationToCodeName"].Split('-').FirstOrDefault();
                        toName = excelInput["LocationToCodeName"].Split('-').Skip(1).FirstOrDefault();
                    }
                    else
                    {
                        toCode = string.Empty;
                        toName = string.Empty;
                    }
                }

                //Nội địa
                if (serviceID == iLO)
                {
                    //Nếu không có Depot và DepotReturn => Mặc định depot đầu tiên của hãng tàu.
                    var dataDepot = data.ListDepot.Where(c => c.CustomerID == cusID).ToList();
                    if (objSetting.LocationDepotCode > 0 && !string.IsNullOrEmpty(excelInput["LocationDepotCode"]))
                    {
                        dpCode = excelInput["LocationDepotCode"];
                        dpName = excelInput["LocationDepotName"];
                        var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == excelInput["LocationDepotCode"].Trim().ToLower());
                        if (objCheck != null)
                        {
                            dpID = objCheck.CUSLocationID;
                            if (string.IsNullOrEmpty(dpName))
                                dpName = objCheck.LocationName;
                        }
                    }
                    if (objSetting.LocationReturnCode > 0 && !string.IsNullOrEmpty(excelInput["LocationReturnCode"]))
                    {
                        dprCode = excelInput["LocationReturnCode"];
                        dprName = excelInput["LocationReturnName"];
                        var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == excelInput["LocationReturnCode"].Trim().ToLower());
                        if (objCheck != null)
                        {
                            dprID = objCheck.CUSLocationID;
                            if (string.IsNullOrEmpty(dprName))
                                dprName = objCheck.LocationName;
                        }
                    }

                    var objF = data.ListCUSLocation.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == frCode.Trim().ToLower());
                    if (objF != null)
                    {
                        fID = objF.CUSLocationID;
                        if (string.IsNullOrEmpty(frName))
                            frName = objF.LocationName;
                    }

                    var objT = data.ListCUSLocation.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                    if (objT != null)
                    {
                        tID = objT.CUSLocationID;
                        if (string.IsNullOrEmpty(toName))
                            toName = objT.LocationName;
                    }
                }
                //Xuất nhập khẩu.
                else
                {
                    if (objSetting.TypeOfWAInspectionStatus > 0 && !string.IsNullOrEmpty(excelInput["TypeOfWAInspectionStatus"]))
                    {
                        if (excelInput["TypeOfWAInspectionStatus"].Trim().ToLower() == "x")
                        {
                            isInspect = true;
                        }
                        else
                        {
                            isInspect = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(excelInput["InspectionDate"]))
                    {
                        try
                        {
                            inspectDate = ExcelHelper.ValueToDate(excelInput["InspectionDate"]);
                        }
                        catch
                        {
                            try
                            {
                                inspectDate = Convert.ToDateTime(excelInput["InspectionDate"], new CultureInfo("vi-VN"));
                            }
                            catch
                            {
                            }
                        }
                    }
                    crCode = excelInput["CarrierCode"];
                    crName = excelInput["CarrierName"];
                    if (!string.IsNullOrEmpty(excelInput["CarrierCodeName"]))
                    {
                        string[] s = excelInput["CarrierCodeName"].Split('-');
                        crCode = s[0];
                        if (s.Length > 1)
                        {
                            crName = excelInput["CarrierCodeName"].Substring(crCode.Length + 1);
                        }
                    }

                    if (!string.IsNullOrEmpty(crCode))
                    {
                        var objCarrier = data.ListCarrier.FirstOrDefault(c => c.CustomerID == cusID && c.PartnerCode.Trim().ToLower() == crCode.Trim().ToLower());
                        if (objCarrier != null)
                        {
                            crID = objCarrier.CUSPartnerID;
                            if (string.IsNullOrEmpty(crName))
                                crName = objCarrier.PartnerName;

                            var dataDepot = data.ListDepot.Where(c => c.CusPartID == crID && c.CustomerID == cusID).ToList();
                            //Nhập khẩu (Cảng-Kho-Depot)
                            if (serviceID == iIM)
                            {
                                if (objSetting.LocationReturnCode > 0 && !string.IsNullOrEmpty(excelInput["LocationReturnCode"]))
                                {
                                    dprCode = excelInput["LocationReturnCode"];
                                    dprName = excelInput["LocationReturnName"];
                                    var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == excelInput["LocationReturnCode"].Trim().ToLower());
                                    if (objCheck != null)
                                    {
                                        dprID = objCheck.CUSLocationID;
                                        if (string.IsNullOrEmpty(dprName))
                                            dprName = objCheck.LocationName;
                                    }
                                }

                                var objF = data.ListSeaPort.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == frCode.Trim().ToLower());
                                if (objF != null)
                                {
                                    fID = objF.CUSLocationID;
                                    if (string.IsNullOrEmpty(frName))
                                        frName = objF.LocationName;
                                }

                                var objT = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                if (objT != null)
                                {
                                    tID = objT.CUSLocationID;
                                    if (string.IsNullOrEmpty(toName))
                                        toName = objT.LocationName;
                                }
                            }
                            else
                            {
                                if (objSetting.LocationDepotCode > 0 && !string.IsNullOrEmpty(excelInput["LocationDepotCode"]))
                                {
                                    dpCode = excelInput["LocationDepotCode"];
                                    dpName = excelInput["LocationDepotName"];
                                    var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == excelInput["LocationDepotCode"].Trim().ToLower());
                                    if (objCheck != null)
                                    {
                                        dpID = objCheck.CUSLocationID;
                                        if (string.IsNullOrEmpty(dpName))
                                            dpName = objCheck.LocationName;
                                    }
                                }
                                var objF = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == frCode.Trim().ToLower());
                                if (objF != null)
                                {
                                    fID = objF.CUSLocationID;
                                    if (string.IsNullOrEmpty(frName))
                                        frName = objF.LocationName;
                                }

                                var objT = data.ListSeaPort.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                if (objT != null)
                                {
                                    tID = objT.CUSLocationID;
                                    if (string.IsNullOrEmpty(toName))
                                        toName = objT.LocationName;
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Check Container

                if (objSetting.TypeOfContainerName < 1)
                {
                    var objCheck = data.ListPackingCO.FirstOrDefault();
                    if (objCheck != null)
                    {
                        toCoID = objCheck.ID;
                        toCoName = objCheck.PackingName;
                    }
                }
                else
                {
                    var objCheck = data.ListPackingCO.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["TypeOfContainerName"].Trim().ToLower());
                    if (objCheck != null)
                    {
                        toCoID = objCheck.ID;
                        toCoName = objCheck.Code;
                    }
                }

                if (objSetting.Ton > 0 && !string.IsNullOrEmpty(excelInput["Ton"]))
                {
                    try
                    {
                        ton = Convert.ToDouble(excelInput["Ton"]);
                    }
                    catch { }
                }

                #endregion

                #region Lưu dữ liệu

                var obj = new DTOOPSCOImportPacket_Container();
                obj.CustomerID = cusID;
                obj.CustomerCode = cusCode;
                obj.ContainerNo = excelInput["Note"];
                obj.VesselName = excelInput["VesselName"];
                obj.VesselNo = excelInput["VesselNo"];
                obj.TripNo = excelInput["TripNo"];
                obj.LocationToID = tID;
                obj.TypeOfWAInspectionStatus = isInspect;
                obj.InspectionDate = inspectDate;
                obj.PartnerID = crID;
                obj.PartnerCode = crCode;
                obj.PartnerName = crName;
                if (requestDate != null)
                    obj.RequestDate = requestDate.Value;
                obj.ETARequest = eTARequest;
                obj.ETD = eTD;
                obj.ETA = eTA;
                obj.CutOffTime = cutOffTime;
                obj.OrderCode = excelInput["OrderCode"];
                obj.UserDefine1 = excelInput["UserDefine1"];
                obj.UserDefine2 = excelInput["UserDefine2"];
                obj.ContainerNo = excelInput["ContainerNo"];
                obj.SealNo1 = excelInput["SealNo1"];
                obj.SealNo2 = excelInput["SealNo2"];
                obj.PackingID = toCoID;
                obj.PackingName = toCoName;
                obj.ETA = eTA;
                obj.ETD = eTD;
                obj.Ton = ton;
                obj.LocationDepotID = dpID > 0 ? dpID : null;
                obj.LocationDepotCode = dpCode;
                obj.LocationDepotName = dpName;
                obj.LocationReturnID = dprID > 0 ? dprID : null;
                obj.LocationReturnCode = dprCode;
                obj.LocationReturnName = dprName;
                obj.LocationFromID = fID;
                obj.LocationFromCode = frCode;
                obj.LocationFromName = frName;
                obj.LocationToID = tID;
                obj.LocationToCode = toCode;
                obj.LocationToName = toName;
                obj.DateGetEmpty = getDate;
                obj.DateReturnEmpty = returnDate;

                dataRes.Add(obj);
                #endregion
            }

            return dataRes;
        }

        [HttpPost]
        public string OPS_CO_Import_Packet_CheckLocation()
        {
            try
            {
                string result = string.Empty;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_CO_Import_Packet_CheckLocation();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DI Import ExcelOnline
        [HttpPost]
        public DTOCUSSettingPlan OPS_DI_Import_Packet_Setting_Get(dynamic dynParam)
        {
            try
            {
                int templateID = (int)dynParam.templateID;
                var result = default(DTOCUSSettingPlan);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_Packet_Setting_Get(templateID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel OPS_DI_Import_ExcelOnline_Init(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var templateID = (int)dynParam.templateID;
                var pID = (int)dynParam.pID;
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPS_DI_Import_ExcelOnline_Init(templateID, pID, functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region NewCO - Map

        [HttpPost]
        public DTOResult OPSCO_MAP_Order_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int typeOfOrder = (int)dynParam.typeOfOrder;
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> dataCus = new List<int>();
                List<int> dataService = new List<int>();
                List<int> dataCarrier = new List<int>();
                List<int> dataSeaport = new List<int>();
                if (dynParam.dataCus != null)
                    dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                if (dynParam.dataService != null)
                    dataService = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataService.ToString());
                if (dynParam.dataCarrier != null)
                    dataCarrier = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCarrier.ToString());
                if (dynParam.dataSeaport != null)
                    dataSeaport = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataSeaport.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Order_List(request, typeOfOrder, fDate, tDate, dataCus, dataService, dataCarrier, dataSeaport);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_MAP_Tractor_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime requestDate = Convert.ToDateTime(dynParam.requestDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Tractor_List(request, requestDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_MAP_Romooc_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime requestDate = Convert.ToDateTime(dynParam.requestDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Romooc_List(request, requestDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                    
        [HttpPost]
        public DTOResult OPSCO_MAP_VehicleVendor_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                var request = dynParam.request.ToString();
                var vendorID = (int)dynParam.vendorID;
                var typeofvehicle = (int)dynParam.typeofvehicle;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_VehicleVendor_List(request, vendorID, typeofvehicle);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_MAP_TOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                bool isApproved = (bool)dynParam.isApproved;
                bool isTendered = (bool)dynParam.isTendered;
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_TOMaster_List(request, isApproved, isTendered, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Dùng chung
        [HttpPost]
        public DTOResult OPSCO_MAP_Location_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Location_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Dùng chung
        [HttpPost]
        public List<CUSCustomer> OPSCO_MAP_Vendor_List()
        {
            try
            {
                var result = new List<CUSCustomer>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Vendor_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<CUSCustomer> OPSCO_MAP_Customer_List()
        {
            try
            {
                var result = new List<CUSCustomer>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Customer_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CATServiceOfOrder> OPSCO_MAP_ServiceOfOrder_List()
        {
            try
            {
                var result = new List<CATServiceOfOrder>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_ServiceOfOrder_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Dùng chung
        [HttpPost]
        public List<FLMDriver> OPSCO_MAP_Driver_List()
        {
            try
            {
                var result = new List<FLMDriver>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Driver_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Dùng chung
        [HttpPost]
        public List<FLMDriver> OPSCO_MAP_DriverVendor_List(dynamic dynParam)
        {
            try
            {
                int vendorID = (int)dynParam.vendorID;
                var result = new List<FLMDriver>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_DriverVendor_List(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Dùng chung
        [HttpPost]
        public DTOOPSCO_MAP_Setting OPSCO_MAP_Setting(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Setting();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Setting();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public List<DTOOPSCO_MAP_Seaport> OPSCO_MAP_Seaport_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSCO_MAP_Seaport>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Seaport_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_MAP_Carrier> OPSCO_MAP_Carrier_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSCO_MAP_Carrier>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Carrier_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_Trip OPSCO_MAP_TripByID(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Trip();

                int masterID = (int)dynParam.masterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_TripByID(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_MAP_Trip> OPSCO_MAP_TripByVehicle_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSCO_MAP_Trip>();

                DateTime now = Convert.ToDateTime(dynParam.Date);
                int vehicleID = (int)dynParam.vehicleID;
                int romoocID = (int)dynParam.romoocID;
                int total = (int)dynParam.total;
                bool isApproved = (bool)dynParam.isApproved;
                bool isTendered = (bool)dynParam.isTendered;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_TripByVehicle_List(now, vehicleID, romoocID, total);
                });
                if (!isApproved)
                {
                    result = result.Where(c => c.Status != 1).ToList();
                }
                if (!isTendered)
                {
                    result = result.Where(c => c.Status != 2).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_VehicleAvailable OPSCO_MAP_CheckVehicleAvailable(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_VehicleAvailable();
                double Ton = (double)dynParam.Ton;
                int masterID = (int)dynParam.masterID;
                int romoocID = (int)dynParam.romoocID;
                int vehicleID = (int)dynParam.vehicleID;
                DateTime ETD = Convert.ToDateTime(dynParam.ETD);
                DateTime ETA = Convert.ToDateTime(dynParam.ETA);
                List<int> dataCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCon.ToString());
                List<int> dataOPSCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataOPSCon.ToString());
                List<int> dataORDCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataORDCon.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_CheckVehicleAvailable(masterID, vehicleID, romoocID, ETD, ETA, Ton, dataORDCon, dataOPSCon, dataCon);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Schedule_Data();
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Schedule_Data(data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_Save(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCO_MAP_Trip>(dynParam.item.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_ToMON(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_ToMON(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_ToOPS(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_ToOPS(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_Cancel(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Cancel(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_UpdateAndToMON(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.TOMasterID;
                string DriverTel = dynParam.DriverTel.ToString();
                string DriverName = dynParam.DriverName.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_UpdateAndToMON(ID, DriverName, DriverTel);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_Delete(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Delete(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_ToVendor(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCO_MAP_Trip>(dynParam.item.ToString());
                List<DTODIAppointmentRouteTender> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRouteTender>>(dynParam.data.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_ToVendor(item, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_ToVendorKPI(dynamic dynParam)
        {
            try
            {
                List<int> dataCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCon.ToString());
                List<DTOOPSCO_MAP_Vendor_KPI> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCO_MAP_Vendor_KPI>>(dynParam.data.ToString());
                double rateTime = (double)dynParam.rateTime;

                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_ToVendorKPI(dataCon, data, rateTime);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_COTOContainer_Split(dynamic dynParam)
        {
            try
            {
                int hubID = (int)dynParam.hubID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_COTOContainer_Split(data, hubID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_COTOContainer_Split_Cancel(dynamic dynParam)
        {
            try
            {
                int conID = (int)dynParam.conID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_COTOContainer_Split_Cancel(conID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //View xem chuyến mới.
        [HttpPost]
        public DTOResult OPSCO_MAP_COTOContainer_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                bool isApproved = (bool)dynParam.isApproved;
                bool isTendered = (bool)dynParam.isTendered;
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_COTOContainer_List(request, isApproved, isTendered, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        [HttpPost]
        public DTOResult OPSCO_MAP_COTOContainer_ByTrip_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                var mID = (int)dynParam.mID;
                var opsConID = (int)dynParam.conID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_COTOContainer_ByTrip_List(request, mID, opsConID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_MAP_2View_Container_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_2View_Container_List(request, data, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool OPSCO_MAP_2View_Master_Update_Check4Delete(dynamic dynParam)
        {
            try
            {
                var result = false;
                var mID = (int)dynParam.mID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_2View_Master_Update_Check4Delete(mID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_2View_Master_Update_TimeLine(dynamic dynParam)
        {
            try
            {
                var mID = (int)dynParam.mID;
                var vehicleID = (int)dynParam.vehicleID;
                var vendorID = -1;
                if (dynParam.vendorID != null)
                {
                    vendorID = (int)dynParam.vendorID;
                }
                var isTractor = (bool)dynParam.isTractor;
                var ETD = Convert.ToDateTime(dynParam.ETD);
                var ETA = Convert.ToDateTime(dynParam.ETA);
                List<DTOOPSCOTOContainer> dataOffer = new List<DTOOPSCOTOContainer>();
                if (dynParam.dataOffer != null)
                    dataOffer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCOTOContainer>>(dynParam.dataOffer.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_2View_Master_Update_TimeLine(mID, vendorID, vehicleID, isTractor, ETD, ETA, dataOffer);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_2View_Master_Update_Container(dynamic dynParam)
        {
            try
            {
                var mID = (int)dynParam.mID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                var isRemove = (bool)dynParam.isRemove;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_2View_Master_Update_Container(mID, data, isRemove);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_2View_Master_Update(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCO_MAP_Trip>(dynParam.item.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_2View_Master_Update(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_2View_Master_ChangeVehicle(dynamic dynParam)
        {
            try
            {                
                int mID = (int)dynParam.mID;
                int vID = (int)dynParam.vehID;
                int type = (int)dynParam.type;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_2View_Master_ChangeVehicle(mID, vID, type);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_VehicleAvailable OPSCO_MAP_2View_Master_ChangeDriver(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_MAP_VehicleAvailable result = new DTOOPSCO_MAP_VehicleAvailable();
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_2View_Master_ChangeDriver(data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_MAP_TimeLine_Create_Master(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCO_MAP_Trip>(dynParam.item.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_TimeLine_Create_Master(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_MAP_TimeLine_Update_Container(dynamic dynParam)
        {
            try
            {
                var mID = (int)dynParam.mID;
                var isRemove = (bool)dynParam.isRemove;
                List<int> dataCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCon.ToString());
                List<int> dataOPSCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataOPSCon.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_TimeLine_Update_Container(mID, dataOPSCon, dataCon, isRemove);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_Tractor OPSCO_MAP_TimeLine_Vehicle_Info(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_MAP_Tractor result = new DTOOPSCO_MAP_Tractor();
                var vehID = (int)dynParam.vehID;
                var venID = (int)dynParam.venID;
                var romID = (int)dynParam.romID;
                DateTime now = Convert.ToDateTime(dynParam.now);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_TimeLine_Vehicle_Info(venID, vehID, romID, now);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_MAP_TimeLine_Master_Container_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                int typeOfOrder = (int)dynParam.typeOfOrder;
                int mID = (int)dynParam.mID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_TimeLine_Master_Container_List(request, mID, typeOfOrder);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_MAP_Vendor_KPI> OPSCO_MAP_Vendor_KPI_List(dynamic dynParam)
        {
            try
            {
                List<DTOOPSCO_MAP_Vendor_KPI> result = new List<DTOOPSCO_MAP_Vendor_KPI>();
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Vendor_KPI_List(data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_MAP_Vendor_KPI> OPSCO_MAP_Vendor_With_KPI_List(dynamic dynParam)
        {
            try
            {
                List<DTOOPSCO_MAP_Vendor_KPI> result = new List<DTOOPSCO_MAP_Vendor_KPI>();
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Vendor_With_KPI_List(data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_MAP_Vendor_KPI_Save(dynamic dynParam)
        {
            try
            {
                int vendorID = (int)dynParam.vendorID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Vendor_KPI_Save(vendorID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_New_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Schedule_Data();
                bool isShowVehicle = (bool)dynParam.isShowVehicle;
                string strVehicle = dynParam.strVehicle.ToString();
                int typeOfResource = 1;
                if (dynParam.typeOfResource != null)
                    typeOfResource = (int)dynParam.typeOfResource;
                List<int> dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                List<int> dataSer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataSer.ToString());
                List<int> dataSea = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataSea.ToString());
                List<int> dataCar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCar.ToString());
                List<int> dataStt = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataStt.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_New_Schedule_Data(isShowVehicle, strVehicle, typeOfResource, dataCus, dataSer, dataCar, dataSea, dataStt);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_MAP_New_Schedule_COTOContainer_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int vendorID = (int)dynParam.vendorID;
                var fDate = Convert.ToDateTime(dynParam.fDate);
                var tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> dataCus = new List<int>();
                List<int> dataStatus = new List<int>();
                List<int> dataService = new List<int>();
                List<int> dataCarrier = new List<int>();
                List<int> dataSeaport = new List<int>();
                if (dynParam.dataCus != null)
                    dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                if (dynParam.dataService != null)
                    dataService = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataService.ToString());
                if (dynParam.dataCarrier != null)
                    dataCarrier = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCarrier.ToString());
                if (dynParam.dataSeaport != null)
                    dataSeaport = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataSeaport.ToString());
                if (dynParam.dataStatus != null)
                    dataStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataStatus.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_New_Schedule_COTOContainer_List(request, vendorID, fDate, tDate, dataCus, dataService, dataCarrier, dataSeaport, dataStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_MAP_Vehicle_New(dynamic dynParam)
        {
            try
            {
                int vendorID = (int)dynParam.vendorID;
                double maxWeight = (double)dynParam.maxWeight;
                string regNo = dynParam.regNo.ToString();
                int typeofVehicle = (int)dynParam.typeofVehicle;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Vehicle_New(vendorID, regNo, maxWeight, typeofVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_MAP_TimeLine_Create_Item(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCO_MAP_Trip>(dynParam.item.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                List<DTOOPSCOTOContainer> dataOffer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCOTOContainer>>(dynParam.dataOffer.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_TimeLine_Create_Item(item, dataOffer);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_Info_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Schedule_Data();
                int m1ID = (int)dynParam.m1ID;
                int m2ID = (int)dynParam.m2ID;
                int venID = (int)dynParam.venID;
                int vehID = (int)dynParam.vehID;
                int romID = (int)dynParam.romID;
                int typeOfResource = 1;
                if (dynParam.typeOfResource != null)
                    typeOfResource = (int)dynParam.typeOfResource;
                DateTime ETD = Convert.ToDateTime(dynParam.ETD);
                DateTime ETA = Convert.ToDateTime(dynParam.ETA);
                List<int> dataCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCon.ToString());
                List<int> dataOPSCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataOPSCon.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Info_Schedule_Data(m1ID, m2ID, venID, vehID, romID, typeOfResource, ETD, ETA, dataOPSCon, dataCon);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public string OPSCO_MAP_Info_Schedule_DragDrop_Save_Check(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                int mID = (int)dynParam.mID;
                List<DTOOPSCO_Map_Schedule_Event> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCO_Map_Schedule_Event>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Info_Schedule_DragDrop_Save_Check(mID, data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_Info_Schedule_DragDrop_Save(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                int mID = (int)dynParam.mID;
                List<DTOOPSCO_Map_Schedule_Event> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCO_Map_Schedule_Event>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Info_Schedule_DragDrop_Save(mID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_Vehicle_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Schedule_Data();
                int mID = (int)dynParam.mID;
                int vehID = (int)dynParam.vehID;
                int romID = (int)dynParam.romID;
                int typeOfResource = (int)dynParam.typeOfResource;                
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Vehicle_Schedule_Data(mID, typeOfResource, vehID, romID, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string OPSCO_MAP_Schedule_Check(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                int vehID = (int)dynParam.vehID;
                int romID = (int)dynParam.romID;
                List<int> dataContainer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataContainer.ToString());
                List<int> dataOPSContainer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataOPSContainer.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Schedule_Check(vehID, romID, dataContainer, dataOPSContainer);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_NewTime_Offer(dynamic dynParam)
        {
            try
            {
                DTOOPSCOTOMaster result = new DTOOPSCOTOMaster();
                DTOOPSCO_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCO_MAP_Trip>(dynParam.item.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Schedule_NewTime_Offer(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_TOMaster_Vehicle_Offer(dynamic dynParam)
        {
            try
            {
                DTOOPSCOTOMaster result = new DTOOPSCOTOMaster();
                int mID = (int)dynParam.mID;
                int vehID = (int)dynParam.vehID;
                int? venID = (int)dynParam.venID;
                bool isTractor = (bool)dynParam.isTractor;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Schedule_TOMaster_Vehicle_Offer(mID, vehID, venID, isTractor);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_Schedule_TOMaster_Change_Vehicle(dynamic dynParam)
        {
            try
            {
                int mID = (int)dynParam.mID;
                int vehID = (int)dynParam.vehID;
                int? venID = (int)dynParam.venID;
                bool isTractor = (bool)dynParam.isTractor;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Schedule_TOMaster_Change_Vehicle(mID, vehID, venID, isTractor);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_MAP_Schedule_TOMaster_Change_Time(dynamic dynParam)
        {
            try
            {
                int mID = (int)dynParam.mID;
                DateTime ETD = Convert.ToDateTime(dynParam.ETD);
                DateTime ETA = Convert.ToDateTime(dynParam.ETA);
                List<DTOOPSCOTOContainer> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCOTOContainer>>(dynParam.dataContainer.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Schedule_TOMaster_Change_Time(mID, ETD, ETA, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_LeadTime_Offer(dynamic dynParam)
        {
            try
            {
                DTOOPSCOTOMaster result = new DTOOPSCOTOMaster();
                List<int> dataCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCon.ToString());
                List<int> dataOPSCon = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataOPSCon.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Schedule_LeadTime_Offer(dataOPSCon, dataCon);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_AddTOContainer_Offer(dynamic dynParam)
        {
            try
            {
                DTOOPSCOTOMaster result = new DTOOPSCOTOMaster();
                int mID = (int)dynParam.mID;
                int typeOfData = (int)dynParam.typeOfData;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_MAP_Schedule_AddTOContainer_Offer(mID, data, typeOfData);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_MAP_Schedule_AddTOContainer(dynamic dynParam)
        {
            try
            {
                int mID = (int)dynParam.mID;
                DateTime ETD = Convert.ToDateTime(dynParam.ETD.ToString());
                DateTime ETA = Convert.ToDateTime(dynParam.ETA.ToString());
                List<DTOOPSCO_Map_Schedule_Event> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCO_Map_Schedule_Event>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_MAP_Schedule_AddTOContainer(mID, ETD, ETA, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TimeLine

        [HttpPost]
        public DTOResult OPSCO_TimeLine_Order_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int typeOfOrder = (int)dynParam.typeOfOrder;
                bool isOwnerPlanning = (bool)dynParam.isOwnerPlanning;
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> dataCus = new List<int>();
                List<int> dataService = new List<int>();
                List<int> dataCarrier = new List<int>();
                List<int> dataSeaport = new List<int>();
                if (dynParam.dataCus != null)
                    dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                if (dynParam.dataService != null)
                    dataService = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataService.ToString());
                if (dynParam.dataCarrier != null)
                    dataCarrier = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCarrier.ToString());
                if (dynParam.dataSeaport != null)
                    dataSeaport = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataSeaport.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_Order_List(request, typeOfOrder, isOwnerPlanning, fDate, tDate, dataCus, dataService, dataCarrier, dataSeaport);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_TimeLine_Vehicle_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                int typeOfView = (int)dynParam.typeOfView;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_Vehicle_List(request, fDate, tDate, typeOfView);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPSCO_TimeLine_RomoocWait_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_RomoocWait_List(request, data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOOPSCO_MAP_Schedule_Data OPSCO_TimeLine_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Schedule_Data();
                int typeOfView = (int)dynParam.typeOfView;
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                List<string> dataRes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.dataRes.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_Schedule_Data(fDate, tDate, dataRes, typeOfView);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPSCO_TimeLine_COTOContainer_ByTrip_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                var mID = (int)dynParam.mID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_COTOContainer_ByTrip_List(request, mID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_TimeLine_COTOContainer_Remove(dynamic dynParam)
        {
            try
            {
                var mID = (int)dynParam.mID;
                var conID = (int)dynParam.conID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_TimeLine_COTOContainer_Remove(mID, conID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOOPSCOTOMaster OPSCO_TimeLine_Event_ChangeTime_Offer(dynamic dynParam)
        {
            try
            {
                DTOOPSCOTOMaster result = new DTOOPSCOTOMaster();
                int mID = (int)dynParam.mID;
                var conID = (int)dynParam.conID;
                DateTime ETD = Convert.ToDateTime(dynParam.ETD);
                DateTime ETA = Convert.ToDateTime(dynParam.ETA);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_Event_ChangeTime_Offer(mID, conID, ETD, ETA);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOOPSCO_MAP_Trip OPSCO_TimeLine_TOMaster_ByID(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Trip();
                int masterID = (int)dynParam.masterID;
                int containerID = (int)dynParam.containerID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_TOMaster_ByID(masterID, containerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_Trip OPSCO_TimeLine_DataContainerLocal_ByTOMasterID(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Trip();
                int masterID = (int)dynParam.masterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_DataContainerLocal_ByTOMasterID(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSCO_TimeLine_OrderToTOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int typeofserviceorder = (int)dynParam.typeofserviceorder;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_OrderToTOMaster_List(request, typeofserviceorder);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_TimeLine_Order_OwnerPlanning_Update(dynamic dynParam)
        {
            try
            {
                bool value = (bool)dynParam.value;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_TimeLine_Order_OwnerPlanning_Update(data, value);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPSCO_TimeLine_OrderFilter_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_OrderFilter_List(request, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public List<DTOOPSCO_TimeLine_Vehicle> OPSCO_TimeLine_Vehicle_OnMap_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSCO_TimeLine_Vehicle>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_Vehicle_OnMap_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_TimeLine_Vehicle> OPSCO_TimeLine_Vehicle_OnMap_GPSUpdate(dynamic dynParam)
        {
            try
            {
                List<DTOOPSCO_TimeLine_Vehicle> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCO_TimeLine_Vehicle>>(dynParam.data.ToString());
                foreach (var item in data)
                {
                    var res = new DTOOtherVehiclePosition();
                    ServiceFactory.SVOther((ISVOther sv) =>
                    {
                        res = sv.VehiclePosition_GetLast(item.GPSCode, DateTime.Now);
                    });
                    item.Lat = res.Lat;
                    item.Lng = res.Lng;
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSLocation> OPSCO_TimeLine_ORDLocation_OnMap_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCUSLocation>();
                int conID = (int)dynParam.conID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_ORDLocation_OnMap_List(conID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_Schedule_Data OPSCO_TimeLine_Vehicle_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_Schedule_Data();
                int typeofvehicle = (int)dynParam.typeofvehicle;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_TimeLine_Vehicle_Schedule_Data(typeofvehicle);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NewDI - Map

        [HttpPost]
        public DTOResult OPSDI_MAP_Order_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int typeOfOrder = (int)dynParam.typeOfOrder;
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> dataCus = new List<int>();
                if (dynParam.dataCus != null)
                    dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_Order_List(request, typeOfOrder, fDate, tDate, dataCus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSDI_MAP_Vehicle_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime requestDate = Convert.ToDateTime(dynParam.requestDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_Vehicle_List(request, requestDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSDI_MAP_VehicleVendor_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int vendorID = Convert.ToInt32(dynParam.vendorID);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_VehicleVendor_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSDI_MAP_TOMaster_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                bool isApproved = (bool)dynParam.isApproved;
                bool isTendered = (bool)dynParam.isTendered;
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_TOMaster_List(request, isApproved, isTendered, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSDI_MAP_TOMaster_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int mID = (int)dynParam.mID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_TOMaster_GroupProduct_List(request, mID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDI_MAP_Trip OPSDI_MAP_TripByID(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDI_MAP_Trip();

                int masterID = (int)dynParam.masterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_TripByID(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSDI_MAP_Trip> OPSDI_MAP_TripByVehicle_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSDI_MAP_Trip>();

                DateTime now = Convert.ToDateTime(dynParam.Date);
                int vehicleID = (int)dynParam.vehicleID;
                int total = (int)dynParam.total;
                bool isApproved = (bool)dynParam.isApproved;
                bool isTendered = (bool)dynParam.isTendered;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_TripByVehicle_List(now, vehicleID, total);
                });
                if (!isApproved)
                {
                    result = result.Where(c => c.Status != 1).ToList();
                }
                if (!isTendered)
                {
                    result = result.Where(c => c.Status != 2).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSCO_MAP_VehicleAvailable OPSDI_MAP_CheckVehicleAvailable(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSCO_MAP_VehicleAvailable();
                double Ton = (double)dynParam.Ton;
                int masterID = (int)dynParam.masterID;
                int vehicleID = (int)dynParam.vehicleID;
                DateTime ETD = Convert.ToDateTime(dynParam.ETD);
                DateTime ETA = Convert.ToDateTime(dynParam.ETA);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_CheckVehicleAvailable(masterID, vehicleID, ETD, ETA, Ton);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDI_MAP_Schedule_Data OPSDI_MAP_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDI_MAP_Schedule_Data();
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_Schedule_Data(data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSDI_MAP_GroupProduct> OPSDI_MAP_GroupByTrip_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOOPSDI_MAP_GroupProduct>();
                int tripID = (int)dynParam.tripID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_GroupByTrip_List(tripID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int OPSDI_MAP_Save(dynamic dynParam)
        {
            try
            {
                DTOOPSDI_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDI_MAP_Trip>(dynParam.item.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    item.ID = sv.OPSDI_MAP_Save(item);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_Map_Update(dynamic dynParam)
        {
            try
            {
                DTOOPSDI_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDI_MAP_Trip>(dynParam.item.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                if (item.VendorOfVehicleID == null && (item.LocationEndID < 1 || item.LocationStartID < 1))
                {
                    throw new Exception("Thiếu thông tin điểm bắt đầu/kết thúc chuyến.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_Map_Update(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_ToMON(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_ToMON(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_ToOPS(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_ToOPS(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_Cancel(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_Cancel(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_UpdateAndToMON(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.TOMasterID;
                string DriverTel = dynParam.DriverTel.ToString();
                string DriverName = dynParam.DriverName.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_UpdateAndToMON(ID, DriverName, DriverTel);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_Delete(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_Delete(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_ToVendor(dynamic dynParam)
        {
            try
            {
                DTOOPSDI_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDI_MAP_Trip>(dynParam.item.ToString());
                List<DTODIAppointmentRouteTender> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTODIAppointmentRouteTender>>(dynParam.data.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_ToVendor(item, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_GroupProduct_Split(dynamic dynParam)
        {
            try
            {
                var gopID = (int)dynParam.gopID;
                var total = (int)dynParam.total;
                var value = (double)dynParam.value;
                var packingType = (int)dynParam.packingType;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_GroupProduct_Split(gopID, total, value, packingType);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_GroupProduct_Split_Cancel(dynamic dynParam)
        {
            try
            {
                int orderGopID = (int)dynParam.orderGopID;
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_GroupProduct_Split_Cancel(orderGopID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool OPSDI_MAP_FTL_Split_Check(dynamic dynParam)
        {
            try
            {
                bool result = false;
                int toMasterID = (int)dynParam.toMasterID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_FTL_Split_Check(toMasterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_FTL_Split(dynamic dynParam)
        {
            try
            {
                int toMasterID = (int)dynParam.toMasterID;
                List<DTOOPSDI_MAP_GroupProduct> dataGop = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSDI_MAP_GroupProduct>>(dynParam.dataGop.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_FTL_Split(toMasterID, dataGop);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_FTL_Merge(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_FTL_Merge(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_Vehicle_New(dynamic dynParam)
        {
            try
            {
                int vendorID = (int)dynParam.vendorID;
                double maxWeight = (double)dynParam.maxWeight;
                string regNo = dynParam.regNo.ToString();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_Vehicle_New(vendorID, regNo, maxWeight);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //View xem chuyến mới
        [HttpPost]
        public DTOResult OPSDI_MAP_DITOGroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                bool isApproved = (bool)dynParam.isApproved;
                bool isTendered = (bool)dynParam.isTendered;
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_DITOGroupProduct_List(request, isApproved, isTendered, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult OPSDI_MAP_2View_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime? fDate = null;
                if (dynParam.fDate != null)
                    fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime? tDate = null;
                if (dynParam.tDate != null)
                    tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_2View_GroupProduct_List(request, data, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool OPSDI_MAP_2View_Master_Update_Check4Delete(dynamic dynParam)
        {
            try
            {
                var result = false;
                var mID = (int)dynParam.mID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_2View_Master_Update_Check4Delete(mID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool OPSDI_MAP_2View_Master_Update_Check4Update(dynamic dynParam)
        {
            try
            {
                var result = false;
                var mID = (int)dynParam.mID;
                var gopID = (int)dynParam.gopID;
                var value = (double)dynParam.value;
                var packingType = (int)dynParam.packingType;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_2View_Master_Update_Check4Update(mID, gopID, value, packingType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool OPSDI_MAP_2View_Master_Update_Check4Consolidate(dynamic dynParam)
        {
            try
            {
                var result = false;
                var mID = (int)dynParam.mID;
                var gopID = (int)dynParam.gopID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_2View_Master_Update_Check4Consolidate(mID, gopID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_2View_Master_Update_TimeLine(dynamic dynParam)
        {
            try
            {
                var mID = (int)dynParam.mID;
                var vehicleID = (int)dynParam.vehicleID;
                var ETD = Convert.ToDateTime(dynParam.ETD);
                var ETA = Convert.ToDateTime(dynParam.ETA);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_2View_Master_Update_TimeLine(mID, vehicleID, ETD, ETA);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_2View_Master_Update_Group(dynamic dynParam)
        {
            try
            {
                var mID = (int)dynParam.mID;
                var gopID = (int)dynParam.gopID;
                var isRemove = (bool)dynParam.isRemove;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_2View_Master_Update_Group(mID, gopID, isRemove);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_2View_Master_Update_Group_Quantity(dynamic dynParam)
        {
            try
            {
                var mID = (int)dynParam.mID;
                var gopID = (int)dynParam.gopID;
                var value = (double)dynParam.value;
                var packingType = (int)dynParam.packingType;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_2View_Master_Update_Group_Quantity(mID, gopID, value, packingType);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSDI_MAP_2View_Master_Update(dynamic dynParam)
        {
            try
            {
                DTOOPSDI_MAP_Trip item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDI_MAP_Trip>(dynParam.item.ToString());
                if (item == null || item.ETA == null || item.ETD == null)
                {
                    throw new Exception("Thiếu thông tin ngày bắt đầu và kết thúc chuyến.");
                }
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_2View_Master_Update(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDI_MAP_Schedule_Data OPSDI_MAP_New_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDI_MAP_Schedule_Data();
                bool isShowVehicle = (bool)dynParam.isShowVehicle;
                string strVehicle = dynParam.strVehicle.ToString();
                int typeOfResource = 1;
                if (dynParam.typeOfResource != null)
                    typeOfResource = (int)dynParam.typeOfResource;
                List<int> dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                List<int> dataStt = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataStt.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_New_Schedule_Data(isShowVehicle, strVehicle, typeOfResource, dataCus, dataStt);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPSDI_MAP_New_Schedule_DITOGroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int vendorID = -1;
                if (dynParam.vendorID != null)
                    vendorID = (int)dynParam.vendorID;
                var fDate = Convert.ToDateTime(dynParam.fDate);
                var tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                List<int> dataStt = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataStt.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_New_Schedule_DITOGroupProduct_List(request, vendorID, fDate, tDate, dataCus, dataStt);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSDI_MAP_Vehicle OPSDI_MAP_TimeLine_Vehicle_Info(dynamic dynParam)
        {
            try
            {
                var result = new DTOOPSDI_MAP_Vehicle();
                int venID = (int)dynParam.venID;
                int vehID = (int)dynParam.vehID;
                var now = Convert.ToDateTime(dynParam.now);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_MAP_TimeLine_Vehicle_Info(venID, vehID, now);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OPSDI_MAP_TimeLine_Master_Update_Group(dynamic dynParam)
        {
            try
            {
                int mID = (int)dynParam.mID;
                List<int> dataGroupProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataGop.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSDI_MAP_TimeLine_Master_Update_Group(mID, dataGroupProduct);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NewCO - Vendor

        [HttpPost]
        public DTOResult OPSCO_VEN_COTOContainer_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int typeOfView = (int)dynParam.typeOfView;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_VEN_COTOContainer_List(request, typeOfView);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_VEN_COTOContainer_Add_No(dynamic dynParam)
        {
            try
            {
                DTOOPSCO_VEN_Container item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSCO_VEN_Container>(dynParam.item.ToString());                
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_VEN_COTOContainer_Add_No(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_VEN_Driver> OPSCO_VEN_Driver_List()
        {
            try
            {
                var result = new List<DTOOPSCO_VEN_Driver>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_VEN_Driver_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_VEN_Vehicle> OPSCO_VEN_Tractor_List()
        {
            try
            {
                var result = new List<DTOOPSCO_VEN_Vehicle>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_VEN_Tractor_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_VEN_Vehicle> OPSCO_VEN_Romooc_List()
        {
            try
            {
                var result = new List<DTOOPSCO_VEN_Vehicle>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_VEN_Romooc_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSCO_VEN_Reason> OPSCO_VEN_Reason_List()
        {
            try
            {
                var result = new List<DTOOPSCO_VEN_Reason>();
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_VEN_Reason_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_VEN_Accept(dynamic dynParam)
        {
            try
            {
                List<DTOOPSCO_VEN_TOMaster> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCO_VEN_TOMaster>>(dynParam.data.ToString());
                List<string> dataError = new List<string>();
                //foreach (var item in data)
                //{
                //    if (item.VehicleID <= 2)
                //        dataError.Add(item.TOMasterCode);
                //}
                if (dataError.Count > 0)
                    throw new Exception("Vui lòng nhập xe cho các chuyến: " + string.Join(",", dataError));
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_VEN_Accept(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_VEN_Reject(dynamic dynParam)
        {
            try
            {
                List<DTOOPSCO_VEN_TOMaster> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOOPSCO_VEN_TOMaster>>(dynParam.data.ToString());
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_VEN_Reject(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult OPSCO_VEN_Vehicle_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int venID = (int)dynParam.venID;
                int typeOfVehicle = (int)dynParam.typeOfVehicle;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSCO_VEN_Vehicle_List(request, venID, typeOfVehicle);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void OPSCO_VEN_Change_Time(dynamic dynParam)
        {
            try
            {
                int mID = (int)dynParam.mID;
                DateTime ETD = Convert.ToDateTime(dynParam.ETD);
                DateTime ETA = Convert.ToDateTime(dynParam.ETA);
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_VEN_Change_Time(mID, ETD, ETA);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_VEN_Change_Vehicle(dynamic dynParam)
        {
            try
            {
                int mID = (int)dynParam.mID;
                int vehID = (int)dynParam.vehID;
                int romID = (int)dynParam.romID;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_VEN_Change_Vehicle(mID, vehID, romID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_VEN_Change_Driver(dynamic dynParam)
        {
            try
            {
                int mID = (int)dynParam.mID;
                string tel = (string)dynParam.tel;
                string name = (string)dynParam.name;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_VEN_Change_Driver(mID, name, tel);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSCO_VEN_Vehicle_New(dynamic dynParam)
        {
            try
            {
                int venID = (int)dynParam.venID;
                string regNo = (string)dynParam.regNo;
                double? maxWeight = (double?)dynParam.maxWeight;
                int typeOfVehicle = (int)dynParam.typeOfVehicle;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    sv.OPSCO_VEN_Vehicle_New(venID, regNo, maxWeight, typeOfVehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region NewDI - Vendor
        
        [HttpPost]
        public DTOResult OPSDI_VEN_DITOGroupProduct_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                int typeOfView = (int)dynParam.typeOfView;
                ServiceFactory.SVOperation((ISVOperation sv) =>
                {
                    result = sv.OPSDI_VEN_DITOGroupProduct_List(request, typeOfView);
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