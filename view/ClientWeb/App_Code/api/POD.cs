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
using Microsoft.SqlServer;
using System.IO;
using OfficeOpenXml;
using IServices;
using System.ServiceModel;
using Presentation;
using System.Reflection;
using OfficeOpenXml.Style;

namespace ClientWeb
{
    public class PODController : BaseController
    {
        #region Common
        public DTOResult Customer_List()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
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

        public DTOResult Vendor_List()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
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
        #endregion

        #region PODFLMCO
        public DTOResult PODFLMCOInput_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMCOInput_List(request, dtFrom, dtTo);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOPODFLMCOInputDriver PODFLMCOInput_GetDrivers(dynamic dynParam)
        {
            try
            {
                int COTOMasterID = (int)dynParam.COTOMasterID;
                DTOPODFLMCOInputDriver result = default(DTOPODFLMCOInputDriver);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMCOInput_GetDrivers(COTOMasterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMCOInput_SaveDrivers(dynamic dynParam)
        {
            try
            {
                int COTOMasterID = (int)dynParam.COTOMasterID;
                DTOPODFLMCOInputDriver item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODFLMCOInputDriver>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMCOInput_SaveDrivers(item, COTOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult PODFLMCOInput_TroubleCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int COTOMasterID = (int)dynParam.COTOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMCOInput_TroubleCostList(request, COTOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PODFLMCOInput_TroubleCostNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int COTOMasterID = (int)dynParam.COTOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMCOInput_TroubleCostNotIn_List(request, COTOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMCOInput_TroubleCostNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int COTOMasterID = (int)dynParam.COTOMasterID;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMCOInput_TroubleCostNotIn_SaveList(lst, COTOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMCOInput_TroubleCost_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMCOInput_TroubleCost_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PODFLMCOInput_StationCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int COTOMasterID = (int)dynParam.COTOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMCOInput_StationCostList(request, COTOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMCOInput_Save(dynamic dynParam)
        {
            try
            {
                DTOPODFLMCOInput item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODFLMCOInput>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMCOInput_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMCOInput_StationCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOPODOPSCOTOStation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODOPSCOTOStation>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMCOInput_StationCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMCOInput_TroubleCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOPODCATTroubleCost> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODCATTroubleCost>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMCOInput_TroubleCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PODFLMCOInput_DriverList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMCOInput_DriverList();
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PODFLMCOInput_Approved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMCOInput_Approved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODFLMDI
        public DTOResult DTOPODFLMDIInput_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMDIInput_List(request, dtFrom, dtTo);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOPODFLMDIInputDriver PODFLMDIInput_GetDrivers(dynamic dynParam)
        {
            try
            {
                int DITOMasterID = (int)dynParam.DITOMasterID;
                DTOPODFLMDIInputDriver result = default(DTOPODFLMDIInputDriver);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMDIInput_GetDrivers(DITOMasterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMDIInput_SaveDrivers(dynamic dynParam)
        {
            try
            {
                int DITOMasterID = (int)dynParam.DITOMasterID;
                DTOPODFLMDIInputDriver item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODFLMDIInputDriver>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMDIInput_SaveDrivers(item, DITOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult PODFLMDIInput_TroubleCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int DITOMasterID = (int)dynParam.DITOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMDIInput_TroubleCostList(request, DITOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PODFLMDIInput_TroubleCostNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int DITOMasterID = (int)dynParam.DITOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMDIInput_TroubleCostNotIn_List(request, DITOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMDIInput_TroubleCostNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int DITOMasterID = (int)dynParam.DITOMasterID;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMDIInput_TroubleCostNotIn_SaveList(lst, DITOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMDIInput_TroubleCost_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMDIInput_TroubleCost_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PODFLMDIInput_StationCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int DITOMasterID = (int)dynParam.DITOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMDIInput_StationCostList(request, DITOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMDIInput_Save(dynamic dynParam)
        {
            try
            {
                DTOPODFLMDIInput item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODFLMDIInput>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMDIInput_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMDIInput_StationCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOPODOPSDITOStation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODOPSDITOStation>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMDIInput_StationCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODFLMDIInput_TroubleCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOPODCATTroubleCost> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODCATTroubleCost>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMDIInput_TroubleCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PODFLMDIInput_DriverList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODFLMDIInput_DriverList();
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PODFLMDIInput_Approved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODFLMDIInput_Approved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Container POD
        public DTOResult PODCOInput_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODCOInput_List(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODCOInput_Save(dynamic dynParam)
        {
            try
            {
                DTOPODCOInput item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODCOInput>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODCOInput_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODInputDI
        [HttpPost]
        public DTOResult PODDIInput_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                List<int> listVendorID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listVendorID.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_List(request, dtFrom, dtTo, listCustomerID, listVendorID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_Save(dynamic dynParam)
        {
            try
            {
                DTOPODDIInput item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODDIInput>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_Save(item);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public string PODInputDI_DN_PODDistributionDN_ExcelExport(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom);
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo);
                var result = string.Empty;
                var lst = new List<DTOPODDistributionDNExcel>();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    lst = sv.PODDistributionDN_ExcelExport(request, dtFrom, dtTo).Data.Cast<DTOPODDistributionDNExcel>().ToList();
                });
                string filepath = "/" + FolderUpload.Export + "export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
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
                    //col++; worksheet.Cells[row, col].Value = "Hóa đơn số";
                    //col++; worksheet.Cells[row, col].Value = "Vận tải";

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
                        //col++;
                        //if (item.ETARequest != null)
                        //{
                        //    worksheet.Cells[row, col].Value = item.ETARequest;
                        //    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        //}

                        stt++;
                        row++;
                    }

                    row++;

                    package.Save();
                }
                result = filepath;
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOPODDistributionDNExcel> PODInputDI_DN_PODDistributionDN_ExcelCheck(dynamic dynParam)
        {
            try
            {
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                var result = new List<DTOPODDistributionDNExcel>();
                if (item != null && !string.IsNullOrEmpty(item.FilePath))
                {
                    var lst = new List<DTOPODDistributionDNExcel>();
                    ServiceFactory.SVPOD((ISVPOD sv) =>
                    {
                        lst = sv.PODDistributionDN_GetDataCheck();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(item.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
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

                                    var queryOrder = lst.FirstOrDefault(c => c.TOMasterCode == strTOMasterCode && c.OrderCode == strOrderCode && c.SOCode == strSOCode && c.DNCode == strDNCode);

                                    DTOPODDistributionDNExcel obj = new DTOPODDistributionDNExcel();
                                    obj.ExcelRow = row;
                                    if (queryOrder != null)
                                    {
                                        obj.ID = queryOrder.ID;
                                        obj.Note1 = strNote1;
                                        obj.Note2 = strNote2;
                                        obj.ExcelSuccess = true;
                                        obj.TOMasterCode = queryOrder.TOMasterCode;
                                        obj.VehicleCode = queryOrder.VehicleCode;
                                        obj.SOCode = queryOrder.SOCode;
                                        obj.DNCode = queryOrder.DNCode;
                                        obj.OrderCode = queryOrder.OrderCode;
                                        obj.ExcelError = string.Empty;
                                    }
                                    else
                                    {
                                        obj.ID = -1;
                                        obj.Note1 = strNote1;
                                        obj.Note2 = strNote2;
                                        obj.ExcelSuccess = false;
                                        obj.TOMasterCode = strTOMasterCode;
                                        obj.VehicleCode = strVehicleCode;
                                        obj.OrderCode = strOrderCode;
                                        obj.SOCode = strSOCode;
                                        obj.DNCode = strDNCode;
                                        obj.ExcelError = "Dữ liệu không tồn tại";
                                    }
                                    result.Add(obj);
                                }
                            }
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
                throw ex;
            }
        }

        [HttpPost]
        public void PODInputDI_DN_PODDistributionDN_ExcelSave(dynamic dynParam)
        {
            try
            {
                List<DTOPODDistributionDNExcel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODDistributionDNExcel>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDistributionDN_ExcelSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult PODDIInput_CloseList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_CloseList(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_Approved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_Approved(lst);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_UnApproved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_UnApproved(lst);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_UpdateHasUpload(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_UpdateHasUpload(id);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOCUSProduct> PODDIInput_InsertProduct_ProductByGOPList(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                List<DTOCUSProduct> result = new List<DTOCUSProduct>();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_InsertProduct_ProductByGOPList(id);
                });
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_InsertProduct_SaveList(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                List<PODInsertProduct> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PODInsertProduct>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_InsertProduct_SaveList(lst, id);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<CUSCustomer> PODDIInput_VendorList(dynamic dynParam)
        {
            try
            {
                List<CUSCustomer> result = new List<CUSCustomer>();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_VendorList();
                });
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODInputCO
        //public ActionResult PODInputCO()
        //{
        //    try
        //    {
        //    }
        //    catch (Exception)
        //    { }
        //    return View();
        //}

        //public ActionResult PODInputCO_PODContainer_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    try
        //    {
        //        var result = default(DTOResult);
        //        ServiceFactory.SVPOD((ISVPOD sv) =>
        //        {
        //            result = sv.PODContainer_List(request.ToCustom());
        //        });

        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost]
        //public ActionResult PODInputCO_PODContainer_SaveList(List<DTOPODCOMaster> lst)
        //{
        //    try
        //    {
        //        ServiceFactory.SVPOD((ISVPOD sv) =>
        //        {
        //            sv.PODContainer_SaveList(lst);
        //        });
        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region Excel DI CO
        public string PODFLMInput_Export(dynamic dynParam)
        {
            try
            {
                string file = "/" + FolderUpload.Export + "ExportPODFLMInput_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                DateTime dtfrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtfrom = dtfrom.Date;
                dtto = dtto.Date.AddDays(1);
                int isCO = (int)dynParam.isCO;

                var result = new List<DTOPODFLMDIInputExcel>();
                List<DTOPODCATTrouble> lstTrouble = new List<DTOPODCATTrouble>();
                if (isCO == 0)
                {
                    ServiceFactory.SVPOD((ISVPOD sv) =>
                    {
                        result = sv.PODFLMDIInput_Export(dtfrom, dtto);
                        lstTrouble = sv.PODGroupOfTrouble_List();
                    });
                }
                else if (isCO == 1)
                {
                    ServiceFactory.SVPOD((ISVPOD sv) =>
                    {
                        result = sv.PODFLMCOInput_Export(dtfrom, dtto);
                        lstTrouble = sv.PODGroupOfTrouble_List();
                    });
                }
                var maxStation = 5;
                var maxTrouble = 0;
                if (lstTrouble != null)
                {
                    maxTrouble = lstTrouble.Count();
                }
                var maxLocation = 0;
                if (result != null && result.Count > 0)
                {
                    maxLocation = result.Max(c => c.CountOfLocation);
                }

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, stt = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã chuyến";
                    col++; worksheet.Cells[row, col].Value = "Xe";
                    col++; worksheet.Cells[row, col].Value = "Từ ngày";
                    col++; worksheet.Cells[row, col].Value = "Mã lái xe 1";
                    col++; worksheet.Cells[row, col].Value = "Lái xe 1";
                    col++; worksheet.Cells[row, col].Value = "Mã phụ xe";
                    col++; worksheet.Cells[row, col].Value = "Phụ xe";
                    col++; worksheet.Cells[row, col].Value = "Mã bốc xếp 1";
                    col++; worksheet.Cells[row, col].Value = "Bốc xếp 1";
                    col++; worksheet.Cells[row, col].Value = "Mã bốc xếp 2";
                    col++; worksheet.Cells[row, col].Value = "Bốc xếp 2";
                    col++; worksheet.Cells[row, col].Value = "Mã bốc xếp 3";
                    col++; worksheet.Cells[row, col].Value = "Bốc xếp 3";
                    col++; worksheet.Cells[row, col].Value = "Chuyến";
                    col++; worksheet.Cells[row, col].Value = "Khách hàng";
                    col++; worksheet.Cells[row, col].Value = "Kênh";
                    col++; worksheet.Cells[row, col].Value = "Container";
                    col++; worksheet.Cells[row, col].Value = "Kết hợp";
                    col++; worksheet.Cells[row, col].Value = "Quá tải";
                    col++; worksheet.Cells[row, col].Value = "Đi tỉnh";
                    col++; worksheet.Cells[row, col].Value = "Ôm hàng";
                    for (int i = 1; i <= col; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, 1, i, 2, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }

                    col++; worksheet.Cells[row, col].Value = "Hành trình";
                    var colBak = col;
                    col--;
                    for (int i = 0; i < maxLocation; i++)
                    {
                        col++; worksheet.Cells[row + 1, col].Value = "Điểm " + (i + 1);
                        ExcelHelper.CreateCellStyle(worksheet, 2, col, 2, col, false, false, ExcelHelper.ColorYellow, ExcelHelper.ColorBlack, 0, "");
                    }
                    if (col > colBak)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, 1, colBak, 1, col, true, true, ExcelHelper.ColorYellow, ExcelHelper.ColorBlack, 0, "");
                    }

                    col++; worksheet.Cells[row, col].Value = "KM";
                    colBak = col;
                    worksheet.Cells[row + 1, col].Value = "Bắt đầu";
                    col++; worksheet.Cells[row + 1, col].Value = "Kết thúc";
                    ExcelHelper.CreateCellStyle(worksheet, 1, colBak, 1, col, true, true, ExcelHelper.ColorOrange, ExcelHelper.ColorBlack, 0, "");
                    ExcelHelper.CreateCellStyle(worksheet, 2, colBak, 2, col, false, false, ExcelHelper.ColorOrange, ExcelHelper.ColorBlack, 0, "");

                    for (int i = 0; i < maxStation; i++)
                    {
                        col++; worksheet.Cells[row, col].Value = "Trạm " + (i + 1);
                        worksheet.Cells[row + 1, col].Value = "Trạm";
                        ExcelHelper.CreateCellStyle(worksheet, 2, col, 2, col, false, false, ExcelHelper.ColorLightGreen, ExcelHelper.ColorBlack, 0, "");
                        col++; worksheet.Cells[row + 1, col].Value = "Tiền";
                        ExcelHelper.CreateCellStyle(worksheet, 2, col, 2, col, false, false, ExcelHelper.ColorLightGreen, ExcelHelper.ColorBlack, 0, "");

                        ExcelHelper.CreateCellStyle(worksheet, 1, col - 1, 1, col, true, true, ExcelHelper.ColorLightGreen, ExcelHelper.ColorBlack, 0, "");
                    }

                    for (int i = 0; i < maxTrouble; i++)
                    {
                        var trouble = lstTrouble[i];
                        col++; worksheet.Cells[row, col].Value = trouble.GroupOfTroubleName;
                        worksheet.Cells[row + 1, col].Value = "Ghi chú";
                        ExcelHelper.CreateCellStyle(worksheet, 2, col, 2, col, false, false, ExcelHelper.ColorLightGreen, ExcelHelper.ColorBlack, 0, "");
                        col++; worksheet.Cells[row + 1, col].Value = "Tiền";
                        ExcelHelper.CreateCellStyle(worksheet, 2, col, 2, col, false, false, ExcelHelper.ColorLightGreen, ExcelHelper.ColorBlack, 0, "");

                        ExcelHelper.CreateCellStyle(worksheet, 1, col - 1, 1, col, true, true, ExcelHelper.ColorLightGreen, ExcelHelper.ColorBlack, 0, "");
                    }

                    col++; worksheet.Cells[row, col].Value = "Ghi chú 1";
                    col++; worksheet.Cells[row, col].Value = "Ghi chú 2";
                    //col++; worksheet.Cells[row, col].Value = "Đã c.trả";
                    col++; worksheet.Cells[row, col].Value = "Tổng tiền";
                    for (int i = col - 2; i <= col; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, 1, i, 2, i, true, true, ExcelHelper.ColorLightGreen, ExcelHelper.ColorBlack, 0, "");
                    }
                    worksheet.Cells[1, 1, 1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    row = 3; col = 1;
                    if (result != null && result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt; worksheet.Column(col).Width = 5;
                            col++; worksheet.Cells[row, col].Value = item.DITOMasterCode; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.VehicleNo; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.ETD; worksheet.Column(col).Width = 20;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                            if (item.Driver1 != null)
                            {
                                col++; worksheet.Cells[row, col].Value = item.Driver1.EmployeeCode; worksheet.Column(col).Width = 20;
                                col++; worksheet.Cells[row, col].Value = item.Driver1.DriverName; worksheet.Column(col).Width = 20;
                            }
                            else
                            {
                                col += 2;
                            }
                            if (item.Driver2 != null)
                            {
                                col++; worksheet.Cells[row, col].Value = item.Driver2.EmployeeCode; worksheet.Column(col).Width = 20;
                                col++; worksheet.Cells[row, col].Value = item.Driver2.DriverName; worksheet.Column(col).Width = 20;
                            }
                            else
                            {
                                col += 2;
                            }
                            if (item.Driver3 != null)
                            {
                                col++; worksheet.Cells[row, col].Value = item.Driver3.EmployeeCode; worksheet.Column(col).Width = 20;
                                col++; worksheet.Cells[row, col].Value = item.Driver3.DriverName; worksheet.Column(col).Width = 20;
                            }
                            else
                            {
                                col += 2;
                            }
                            if (item.Driver4 != null)
                            {
                                col++; worksheet.Cells[row, col].Value = item.Driver4.EmployeeCode; worksheet.Column(col).Width = 20;
                                col++; worksheet.Cells[row, col].Value = item.Driver4.DriverName; worksheet.Column(col).Width = 20;
                            }
                            else
                            {
                                col += 2;
                            }
                            if (item.Driver5 != null)
                            {
                                col++; worksheet.Cells[row, col].Value = item.Driver5.EmployeeCode; worksheet.Column(col).Width = 20;
                                col++; worksheet.Cells[row, col].Value = item.Driver5.DriverName; worksheet.Column(col).Width = 20;
                            }
                            else
                            {
                                col += 2;
                            }
                            col++; worksheet.Cells[row, col].Value = item.SortOrder; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.CustomerCode; worksheet.Column(col).Width = 20;

                            var strGroup = "";
                            for (int i = 0; i < item.ListLocation.Count; i++)
                            {
                                var location = item.ListLocation[i];
                                if (location.GroupOfLocationCode != null && location.GroupOfLocationCode != "")
                                {
                                    strGroup += location.GroupOfLocationCode;
                                    if (i != item.ListLocation.Count - 1)
                                    {
                                        strGroup += ", ";
                                    }
                                }
                            }
                            col++; worksheet.Cells[row, col].Value = strGroup; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = isCO == 1 ? "Y" : "N"; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.ExTotalJoin; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.ExIsOverWeight ? "Y" : "N"; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.ExTotalDayOut; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.ExIsOverNight ? "Y" : "N"; worksheet.Column(col).Width = 20;
                            colBak = col;
                            foreach (var location in item.ListLocation)
                            {
                                col++; worksheet.Cells[row, col].Value = location.Code; worksheet.Column(col).Width = 20;
                            }
                            if (maxLocation > 0)
                            {
                                col = colBak + maxLocation;
                            }
                            else
                            {
                                col++;
                            }

                            col++; worksheet.Cells[row, col].Value = item.KmStart; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.KmEnd; worksheet.Column(col).Width = 20;

                            colBak = col;
                            for (int i = 0; i < item.ListStationCost.Count; i++)
                            {
                                var station = item.ListStationCost[i];
                                col++; worksheet.Cells[row, col].Value = station.LocationCode; worksheet.Column(col).Width = 20;
                                col++; worksheet.Cells[row, col].Value = station.Price; worksheet.Column(col).Width = 20;
                                ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                if (i == 4)
                                    break;
                            }
                            if (maxStation > 0)
                            {
                                col = colBak + maxStation * 2;
                            }

                            colBak = col;
                            for (int i = 0; i < maxTrouble; i++)
                            {
                                col += 2;
                                var trouble = lstTrouble[i];
                                foreach (var objTrouble in item.ListTroubleCost)
                                {
                                    if (trouble.GroupOfTroubleCode == objTrouble.GroupOfTroubleCode)
                                    {
                                        worksheet.Cells[row, col - 1].Value = objTrouble.Note; worksheet.Column(col).Width = 20;
                                        worksheet.Cells[row, col].Value = objTrouble.Cost; worksheet.Column(col).Width = 20;
                                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                    }
                                }
                            }
                            if (maxTrouble > 0)
                            {
                                col = colBak + maxTrouble * 2;
                            }
                            col++; worksheet.Cells[row, col].Value = item.Note1; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.Note2; worksheet.Column(col).Width = 20;
                            //col++; worksheet.Cells[row, col].Value = item.IsBidding == true ? "Y" : "N"; worksheet.Column(col).Width = 20;
                            col++; worksheet.Cells[row, col].Value = item.TotalStationCost + item.TotalTroubleCost; worksheet.Column(col).Width = 20;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            row++;
                            stt++;
                        }
                    }
                    #endregion
                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
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
        public List<DTOPODFLMDIInputImport> PODFLMInput_Excel_Check(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                List<DTOPODFLMDIInputImport> result = new List<DTOPODFLMDIInputImport>();

                DateTime dtfrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtfrom = dtfrom.Date;
                dtto = dtto.Date.AddDays(1);
                int isCO = (int)dynParam.isCO;

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    List<DTOPODFLMDIInputExcel> data = new List<DTOPODFLMDIInputExcel>();
                    DTOResult driverData = new DTOResult();
                    List<DTOPODCATTrouble> lstTrouble = new List<DTOPODCATTrouble>();

                    if (isCO == 0)
                    {
                        ServiceFactory.SVPOD((ISVPOD sv) =>
                        {
                            data = sv.PODFLMDIInput_Export(dtfrom, dtto);
                            driverData = sv.PODFLMDIInput_DriverList();
                            lstTrouble = sv.PODGroupOfTrouble_List();
                        });
                    }
                    else if (isCO == 1)
                    {
                        ServiceFactory.SVPOD((ISVPOD sv) =>
                        {
                            data = sv.PODFLMCOInput_Export(dtfrom, dtto);
                            driverData = sv.PODFLMDIInput_DriverList();
                            lstTrouble = sv.PODGroupOfTrouble_List();
                        });
                    }
                    var listDriver = driverData.Data.Cast<DTOFLMDriver>().ToList();

                    var maxStation = 5;
                    var maxTrouble = 0;
                    if (lstTrouble != null)
                    {
                        maxTrouble = lstTrouble.Count();
                    }
                    var maxLocation = 0;
                    if (data != null && data.Count > 0)
                    {
                        maxLocation = data.Max(c => c.CountOfLocation);
                    }

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string MasterCode, VehicleNo;
                            if (worksheet != null)
                            {
                                row = 3;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();
                                    DTOPODFLMDIInputImport obj = new DTOPODFLMDIInputImport();
                                    obj.ExcelRow = row;
                                    col = 2;
                                    MasterCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; VehicleNo = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(MasterCode) && string.IsNullOrEmpty(VehicleNo))
                                    {
                                        break;
                                    }

                                    #region check mã chuyến và xe
                                    DTOPODFLMDIInputExcel podDetail = new DTOPODFLMDIInputExcel();
                                    if (string.IsNullOrEmpty(MasterCode))
                                    {
                                        lstError.Add("[Mã chuyến] không được trống");
                                    }
                                    else
                                    {
                                        if (result.Count(c => c.DITOMasterCode == MasterCode) > 0)
                                        {
                                            lstError.Add("Mã chuyến [" + MasterCode + "] đã sử dụng");
                                        }
                                        else
                                        {
                                            podDetail = data.FirstOrDefault(c => c.DITOMasterCode == MasterCode);
                                            if (podDetail == null)
                                            {
                                                lstError.Add("Mã chuyến [" + MasterCode + "] không được thiết lập từ " + dtfrom.ToShortDateString() + " đến " + dtto.ToShortDateString());
                                            }
                                            else
                                            {
                                                //Xe
                                                if (string.IsNullOrEmpty(VehicleNo))
                                                {
                                                    lstError.Add("[Số xe] không được trống");
                                                }
                                                else
                                                {
                                                    if (podDetail.VehicleNo.CompareTo(VehicleNo) == 0)
                                                    {
                                                        obj.ID = podDetail.ID;
                                                        obj.DITOMasterCode = podDetail.DITOMasterCode;
                                                        obj.DITOMasterID = podDetail.DITOMasterID;
                                                        obj.VehicleID = podDetail.VehicleID;
                                                        obj.VehicleNo = VehicleNo;
                                                    }
                                                    else
                                                    {
                                                        lstError.Add("Số xe [" + VehicleNo + "] không được thiết lập cho chuyến");
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    #region tài xế
                                    string DriverCode;
                                    col = 5;
                                    DTOFLMDriver driver = new DTOFLMDriver();
                                    //Tài xế 1
                                    DriverCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++;
                                    if (string.IsNullOrEmpty(DriverCode))
                                    {
                                        lstError.Add("[Mã lái xe 1] không được trống");
                                    }
                                    else
                                    {
                                        driver = listDriver.FirstOrDefault(c => c.EmployeeCode == DriverCode);
                                        if (driver != null)
                                        {
                                            obj.Driver1 = new DTOFLMDriver();
                                            obj.Driver1.EmployeeCode = driver.EmployeeCode;
                                            obj.Driver1.ID = driver.ID;
                                        }
                                        else
                                        {
                                            lstError.Add("Mã lái xe 1 [" + DriverCode + "] không tồn tại");
                                        }
                                    }

                                    //Tài xế 2
                                    col++;
                                    DriverCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++;
                                    if (string.IsNullOrEmpty(DriverCode))
                                    {
                                    }
                                    else
                                    {
                                        driver = listDriver.FirstOrDefault(c => c.EmployeeCode == DriverCode);
                                        if (driver != null)
                                        {
                                            obj.Driver2 = new DTOFLMDriver();
                                            obj.Driver2.EmployeeCode = driver.EmployeeCode;
                                            obj.Driver2.ID = driver.ID;
                                        }
                                        else
                                        {
                                            lstError.Add("Mã phụ xe [" + DriverCode + "] không tồn tại");
                                        }
                                    }
                                    //Tài xế 3
                                    col++;
                                    DriverCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++;
                                    if (string.IsNullOrEmpty(DriverCode))
                                    {
                                    }
                                    else
                                    {
                                        driver = listDriver.FirstOrDefault(c => c.EmployeeCode == DriverCode);
                                        if (driver != null)
                                        {
                                            obj.Driver3 = new DTOFLMDriver();
                                            obj.Driver3.EmployeeCode = driver.EmployeeCode;
                                            obj.Driver3.ID = driver.ID;
                                        }
                                        else
                                        {
                                            lstError.Add("Mã bốc xếp 1 [" + DriverCode + "] không tồn tại");
                                        }
                                    }

                                    //Tài xế 4
                                    col++;
                                    DriverCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++;
                                    if (string.IsNullOrEmpty(DriverCode))
                                    {
                                    }
                                    else
                                    {
                                        driver = listDriver.FirstOrDefault(c => c.EmployeeCode == DriverCode);
                                        if (driver != null)
                                        {
                                            obj.Driver4 = new DTOFLMDriver();
                                            obj.Driver4.EmployeeCode = driver.EmployeeCode;
                                            obj.Driver4.ID = driver.ID;
                                        }
                                        else
                                        {
                                            lstError.Add("Mã bốc xếp 2 [" + DriverCode + "] không tồn tại");
                                        }
                                    }

                                    //Tài xế 5
                                    col++;
                                    DriverCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++;
                                    if (string.IsNullOrEmpty(DriverCode))
                                    {
                                    }
                                    else
                                    {
                                        driver = listDriver.FirstOrDefault(c => c.EmployeeCode == DriverCode);
                                        if (driver != null)
                                        {
                                            obj.Driver5 = new DTOFLMDriver();
                                            obj.Driver5.EmployeeCode = driver.EmployeeCode;
                                            obj.Driver5.ID = driver.ID;
                                        }
                                        else
                                        {
                                            lstError.Add("Mã bốc xếp 3 [" + DriverCode + "] không tồn tại");
                                        }
                                    }

                                    #endregion

                                    #region station
                                    col = 22;
                                    if (maxLocation != 0)
                                        col = col + maxLocation + 2;
                                    else
                                        col = col + 3;

                                    string StationCode, StationPrice;
                                    obj.ListStationCost = new List<DTOPODOPSDITOStation>();
                                    for (int i = 0; i < maxStation; i++)
                                    {
                                        col++; StationCode = ExcelHelper.GetValue(worksheet, row, col);
                                        col++; StationPrice = ExcelHelper.GetValue(worksheet, row, col);
                                        if (!string.IsNullOrEmpty(StationCode))
                                        {
                                            var station = podDetail.ListStationCost.FirstOrDefault(c => c.LocationCode == StationCode);
                                            if (station != null)
                                            {
                                                if (string.IsNullOrEmpty(StationPrice))
                                                    lstError.Add("[Tiền] Trạm [" + StationCode + "] không được trống");
                                                else
                                                {
                                                    try
                                                    {
                                                        station.Price = Convert.ToDecimal(StationPrice);
                                                        obj.ListStationCost.Add(station);
                                                    }
                                                    catch
                                                    {
                                                        lstError.Add("Tiền [" + StationPrice + "] Trạm [" + StationCode + "] không đúng định dạng");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                lstError.Add("Mã trạm [" + StationCode + "] không được thiết lập trong chuyến");
                                            }
                                        }
                                    }
                                    #endregion

                                    #region trouble
                                    string TroubleName, TroubleCode, TroublePrice, TroubleNote;
                                    obj.ListTroubleCost = new List<DTOPODCATTroubleCost>();
                                    for (int i = 0; i < maxTrouble; i++)
                                    {
                                        TroubleName = lstTrouble[i].GroupOfTroubleName;
                                        TroubleCode = lstTrouble[i].GroupOfTroubleCode;
                                        col++; TroubleNote = ExcelHelper.GetValue(worksheet, row, col);
                                        col++; TroublePrice = ExcelHelper.GetValue(worksheet, row, col);
                                        if (!string.IsNullOrEmpty(TroublePrice))
                                        {
                                            var trouble = podDetail.ListTroubleCost.FirstOrDefault(c => c.GroupOfTroubleCode == TroubleCode);
                                            if (trouble != null)
                                            {

                                                trouble.Note = TroubleNote;
                                                try
                                                {
                                                    trouble.Cost = Convert.ToDecimal(TroublePrice);
                                                    obj.ListTroubleCost.Add(trouble);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Tiền [" + TroublePrice + "] chi phí [" + TroubleCode + "] không đúng định dạng");
                                                }
                                            }
                                            else
                                            {
                                                trouble = new DTOPODCATTroubleCost();
                                                trouble.ID = -1;
                                                trouble.GroupOfTroubleID = lstTrouble[i].ID;
                                                trouble.Note = TroubleNote;
                                                try
                                                {
                                                    trouble.Cost = Convert.ToDecimal(TroublePrice);
                                                    obj.ListTroubleCost.Add(trouble);
                                                }
                                                catch
                                                {
                                                    lstError.Add("Tiền [" + TroublePrice + "] chi phí [" + TroubleCode + "] không đúng định dạng");
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    string Input;
                                    col++; Input = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Note1 = Input;

                                    col++; Input = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Note2 = Input;

                                    //col++; Input = ExcelHelper.GetValue(worksheet, row, col);
                                    //obj. = Input;

                                    obj.ExcelSuccess = true; obj.ExcelError = string.Empty;
                                    if (lstError.Count > 0)
                                    {
                                        obj.ExcelSuccess = false; obj.ExcelError = string.Join(" ,", lstError);
                                    }
                                    result.Add(obj);
                                    row++;
                                }
                            }
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

        public void PODFLMInput_Excel_Import(dynamic dynParam)
        {
            try
            {
                List<DTOPODFLMDIInputImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODFLMDIInputImport>>(dynParam.lst.ToString());
                int isCO = (int)dynParam.isCO;

                if (isCO == 0)
                {
                    ServiceFactory.SVPOD((IServices.ISVPOD sv) =>
                    {
                        sv.PODFLMDIInput_Import(lst);
                    });
                }
                else if (isCO == 1)
                {
                    ServiceFactory.SVPOD((IServices.ISVPOD sv) =>
                    {
                        sv.PODFLMCOInput_Import(lst);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODInputDI_DN Barcode
        public DTOPODBarCode PODInputDI_DN_Barcode_List(dynamic dynParam)
        {
            try
            {
                DTOPODBarCode result = new DTOPODBarCode();
                string Barcode = dynParam.Barcode.ToString();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODBarcodeGroup_List(Barcode);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PODInputDI_DN_Barcode_Save(dynamic dynParam)
        {
            try
            {
                bool IsNote = Convert.ToBoolean(dynParam.IsNote.ToString());
                DTOPODBarCodeGroup item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODBarCodeGroup>(dynParam.item.ToString());

                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODBarcodeGroup_Save(item, IsNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODTime

        public DTOResult PODTime_PODTime_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODMONTime_List(request);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODTime_PODTime_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPODTime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODTime>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODMONTime_SaveList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODExtReturn
        public DTOResult PODOPSExtReturn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtTo = dtTo.Date.AddDays(1);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_List(request, dtFrom, dtTo);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOPODOPSExtReturn PODOPSExtReturn_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOPODOPSExtReturn);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_Get(id);
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODOPSExtReturn_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODOPSExtReturn_Delete(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODOPSExtReturn_Approved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODOPSExtReturn_Approved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult PODOPSExtReturn_CustomerList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_CustomerList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATVehicle> PODOPSExtReturn_VehicleList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATVehicle>();
                int vendorID = (int)dynParam.vendorID;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_VehicleList(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriver> PODOPSExtReturn_DriverList()
        {
            try
            {
                var result = new List<DTOFLMDriver>();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_DriverList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult PODOPSExtReturn_VendorList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_VendorList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOPODTOMaster> PODOPSExtReturn_DITOMasterList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOPODTOMaster>();
                int cusID = (int)dynParam.cusID;
                int vendorID = (int)dynParam.vendorID;
                int vehicleID = (int)dynParam.vehicleID;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_DITOMasterList(cusID, vendorID, vehicleID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult PODOPSExtReturn_GOPByCus(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_GOPByCus(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult PODOPSExtReturn_ProductByGOP(dynamic dynParam)
        {
            try
            {
                int gopID = (int)dynParam.gopID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_ProductByGOP(gopID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult PODOPSExtReturn_DetailList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int extReturnID = (int)dynParam.ExtReturnID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_DetailList(request, extReturnID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult PODOPSExtReturn_DetailNotIn(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int masterID = (int)dynParam.masterID;
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_DetailNotIn(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int PODOPSExtReturn_Save(dynamic dynParam)
        {
            try
            {
                int result = -1;
                DTOPODOPSExtReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODOPSExtReturn>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_Save(item);
                });
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODOPSExtReturn_DetailSave(dynamic dynParam)
        {
            try
            {
                List<DTOPODOPSExtReturnDetail> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODOPSExtReturnDetail>>(dynParam.lst.ToString());
                int ExtReturnID = (int)dynParam.ExtReturnID;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODOPSExtReturn_DetailSave(lst, ExtReturnID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult PODOPSExtReturn_FindList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_FindList(request);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult PODOPSExtReturn_QuickList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_QuickList(request);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODOPSExtReturn_QuickSave(dynamic dynParam)
        {
            try
            {
                DTOPODOPSDITOProductQuick item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODOPSDITOProductQuick>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODOPSExtReturn_QuickSave(item);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPODOPSExtReturnData PODOPSExtReturn_QuickData()
        {
            try
            {
                DTOPODOPSExtReturnData result = new DTOPODOPSExtReturnData();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_QuickData();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODExtReport
        public List<DTOPODOPSExtReturnExport> PODOPSExtReturn_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtto = dtto.Date.AddDays(1);
                var result = new List<DTOPODOPSExtReturnExport>();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODOPSExtReturn_Data(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PODOPSExtReturnTemplate(dynamic dynParam)
        {
            try
            {
                CATFile itemfile = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.itemfile.ToString());
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                dtfrom = dtfrom.Date;
                dtto = dtto.Date.AddDays(1);
                var data = new List<DTOPODOPSExtReturnExport>();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    data = sv.PODOPSExtReturn_Data(dtfrom, dtto);
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
                        Dictionary<string, int> dicColumn = new Dictionary<string, int>();
                        List<ExcelRange> lstCheckFormula = new List<ExcelRange>();
                        Dictionary<int, string> dicCopy = new Dictionary<int, string>();
                        var typeProp = typeof(DTOPODOPSExtReturnExport);

                        int row = 0, col = 0, stt = 0, rowStart = 0, rowEnd = 0, colStart = 0;
                        for (row = 1; row <= worksheet.Dimension.End.Row && row < 200; row++)
                        {
                            if (rowStart == 0)
                            {
                                for (col = 1; col <= worksheet.Dimension.End.Column && col < 200; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (str == "[STT]")
                                    {
                                        rowStart = row;
                                        colStart = col;
                                    }
                                    else if (rowStart > 0 && colStart > 0)
                                    {
                                        var flag = true;
                                        if (!string.IsNullOrEmpty(str) && str.StartsWith("[") && str.EndsWith("]") && str.Length > 2)
                                        {
                                            str = str.Substring(1, str.Length - 2);
                                            try
                                            {
                                                var prop = typeProp.GetProperty(str);
                                                if (prop != null)
                                                {
                                                    dicProp.Add(col, prop);
                                                    if (!dicColumn.ContainsKey(str))
                                                        dicColumn.Add(str, col);
                                                    flag = false;
                                                }
                                            }
                                            catch { }
                                        }

                                        if (flag)
                                        {
                                            if (str.StartsWith("="))
                                                dicCopy.Add(col, str);
                                            else
                                                dicCopy.Add(col, "");
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(str) && str.StartsWith("="))
                                    {
                                        if (str.IndexOf("[") > 0 && str.IndexOf("]") > 0)
                                            lstCheckFormula.Add(worksheet.Cells[row, col]);
                                    }
                                }
                            }

                            if (rowStart > 0) break;
                        }

                        if (rowStart < 1) throw new Exception("Kiem tra [STT] co ton tai hoac nam o sheet dau tien khong");

                        if (data.Count > 0)
                        {
                            int copyrowend = worksheet.Dimension.End.Row;
                            if (copyrowend > rowStart + 100)
                                copyrowend = rowStart + 100;
                            for (int copyrow = copyrowend; copyrow > rowStart; copyrow--)
                            {
                                worksheet.Cells[copyrow, 1, copyrow, worksheet.Dimension.End.Column].Copy(worksheet.Cells[copyrow + data.Count - 1, 1, copyrow + data.Count - 1, worksheet.Dimension.End.Column]);

                                for (col = 1; col <= worksheet.Dimension.End.Column && col < 200; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, copyrow + data.Count - 1, col);
                                    if (!string.IsNullOrEmpty(str) && str.StartsWith("="))
                                    {
                                        if (str.IndexOf("[") > 0 && str.IndexOf("]") > 0)
                                            lstCheckFormula.Add(worksheet.Cells[copyrow + data.Count - 1, col]);
                                    }
                                }
                            }

                            rowEnd = rowStart + data.Count - 1;
                            stt = 1;
                            row = rowStart;
                            col = colStart;

                            foreach (var item in data)
                            {
                                if (row != rowStart)
                                    worksheet.Cells[rowStart, 1, rowStart, worksheet.Dimension.End.Column].Copy(worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column]);

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
                                foreach (var colCopy in dicCopy)
                                {
                                    if (!string.IsNullOrEmpty(colCopy.Value))
                                    {
                                        worksheet.Cells[row, colCopy.Key].Value = null;
                                        worksheet.Cells[row, colCopy.Key].Formula = colCopy.Value.Replace("[ROW]", row.ToString()).Replace("[ROWSTART]", rowStart.ToString()).Replace("[ROWEND]", rowEnd.ToString());
                                    }
                                    else if (row != rowStart)
                                    {
                                        worksheet.Cells[row, colCopy.Key].Copy(worksheet.Cells[rowStart, colCopy.Key]);
                                        ExcelHelper.CopyStyle(worksheet, rowStart, colCopy.Key, row, colCopy.Key);
                                    }
                                }

                                row++;
                                stt++;
                            }

                            Dictionary<string, string> dicTemp = new Dictionary<string, string>();
                            foreach (var item in dicColumn)
                            {
                                dicTemp.Add("[" + item.Key + "]", worksheet.Cells[rowStart, item.Value, rowStart + data.Count - 1, item.Value].Address);
                            }
                            foreach (var item in lstCheckFormula)
                            {
                                var str = item.Value.ToString().Trim();
                                foreach (var itemCheck in dicTemp)
                                {
                                    str = str.Replace(itemCheck.Key, itemCheck.Value);
                                }
                                item.Value = null;
                                item.Formula = str.Replace("[ROWSTART]", rowStart.ToString()).Replace("[ROWEND]", rowEnd.ToString());
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
        #endregion

        #region pod quick
        [HttpPost]
        public DTOResult PODDIInput_Quick_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_Quick_List(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_Quick_Save(dynamic dynParam)
        {
            try
            {
                DTOPODDIInputQuick item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODDIInputQuick>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_Quick_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPODDIInputQuickDN PODDIInput_Quick_DNGet(dynamic dynParam)
        {
            try
            {
                DTOPODDIInputQuickDN result = new DTOPODDIInputQuickDN();
                int DITOGroupProductID = (int)dynParam.DITOGroupProductID;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_Quick_DNGet(DITOGroupProductID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_Quick_DNSave(dynamic dynParam)
        {
            try
            {
                DTOPODDIInputQuickDN item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODDIInputQuickDN>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_Quick_DNSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODImport
        [HttpPost]
        public DTOResult PODImport_Index_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODImport_Index_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string PODImport_Index_Setting_Download(dynamic dynParam)
        {
            try
            {
                string file = "";
                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingPOD objSetting = new DTOCUSSettingPOD();
                List<DTOPODImport> data = new List<DTOPODImport>();

                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    data = sv.PODImport_Data(dtfrom, dtto, customerID);
                    objSetting = sv.PODImport_Index_Setting_Get(templateID);
                });

                string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "DITOGroupProductStatusPODName", "DITOGroupProductStatusPODID" };
                List<string> sValue = new List<string>(aValue);

                Dictionary<string, string> dicName = GetDataName();

                if (objSetting != null)
                {
                    file = "/Uploads/temp/" + objSetting.Name.Replace(' ', '-') + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                    FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                    using (ExcelPackage package = new ExcelPackage(exportfile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(objSetting.Name);
                        if (objSetting.RowStart > 1)
                        {
                            int row = 1;
                            foreach (var prop in objSetting.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSetting, null);
                                        if (v > 0)
                                        {
                                            if (dicName.ContainsKey(p))
                                                worksheet.Cells[row, v].Value = dicName[p];
                                            else
                                                worksheet.Cells[row, v].Value = p;

                                            worksheet.Column(v).Width = 20;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            row = 2;
                            foreach (var item in data)
                            {
                                foreach (var prop in objSetting.GetType().GetProperties())
                                {
                                    try
                                    {
                                        var p = prop.Name;
                                        if (!sValue.Contains(p))
                                        {
                                            var v = (int)prop.GetValue(objSetting, null);
                                            if (v > 0)
                                            {
                                                foreach (var propitem in item.GetType().GetProperties())
                                                {
                                                    var pitem = propitem.Name;
                                                    if (p == pitem)
                                                    {
                                                        var valueitem = propitem.GetValue(item, null);
                                                        Type t = valueitem.GetType();

                                                        if (t.Equals(typeof(bool)))
                                                        {
                                                            bool value = (bool)valueitem;
                                                            if (value == true)
                                                            {
                                                                worksheet.Cells[row, v].Value = "x";
                                                            }
                                                        }
                                                        else if (t.Equals(typeof(DateTime)))
                                                        {
                                                            worksheet.Cells[row, v].Value = valueitem;
                                                            if (pitem == "DateFromLoadStart" || pitem == "DateFromLoadEnd" || pitem == "DateToLoadStart" || pitem == "DateToLoadEnd")
                                                            {
                                                                ExcelHelper.CreateFormat(worksheet, row, v, ExcelHelper.FormatHHMM);
                                                            }
                                                            else
                                                            {
                                                                ExcelHelper.CreateFormat(worksheet, row, v, ExcelHelper.FormatDDMMYYYY);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            worksheet.Cells[row, v].Value = valueitem;
                                                        }

                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                row++;
                            }
                        }

                        package.Save();
                    }
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODImport_Excel_Import(dynamic dynParam)
        {
            try
            {
                int templateID = (int)dynParam.TemplateID;

                List<DTOPODImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODImport>>(dynParam.Data.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODImport_Excel_Import(templateID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOPODImport> PODImport_Excel_Check(dynamic dynParam)
        {
            try
            {
                string file = "/" + dynParam.file.ToString();

                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingPOD objSetting = new DTOCUSSettingPOD();
                List<DTOPODImport> data = new List<DTOPODImport>();

                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    data = sv.PODImport_Data(dtfrom, dtto, customerID);
                    objSetting = sv.PODImport_Index_Setting_Get(templateID);
                });

                var dataRes = new List<DTOPODImport>();


                if (objSetting != null)
                {
                    //Check các required.

                    string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID" };

                    var sValue = new List<string>(aValue);

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                for (row = objSetting.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var obj = new DTOPODImport();
                                    var lstError = new List<string>();

                                    var excelInput = GetDataValue(worksheet, objSetting, row, sValue);
                                    var ID = excelInput["ID"];
                                    if (string.IsNullOrEmpty(ID))
                                    {
                                        throw new Exception("Dòng [" + row + "], Số thứ tự [" + ID + "]  không được trống.");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var id = Convert.ToInt32(ID);

                                            obj.ID = id;
                                            var checkInFile = dataRes.FirstOrDefault(c => c.ID == id);

                                            if (checkInFile == null)
                                            {
                                                var value = data.FirstOrDefault(c => c.ID == obj.ID);
                                                if (value == null)
                                                {
                                                    lstError.Add("[Số thứ tự] không được thiết lập");
                                                }
                                                else
                                                {
                                                    obj = value;
                                                }
                                            }
                                            else
                                            {
                                                lstError.Add("[Số thứ tự] trùng trên file");
                                            }
                                        }
                                        catch
                                        {
                                            lstError.Add("[Số thứ tự] không chính xác");
                                        }
                                    }

                                    obj.DNCode = excelInput["DNCode"];
                                    obj.SOCode = excelInput["SOCode"];

                                    var IsInvoiceStr = excelInput["IsInvoice"];
                                    if (IsInvoiceStr.ToLower().Trim() == "x")
                                    {
                                        obj.IsInvoice = true;
                                    }
                                    else
                                    {
                                        obj.IsInvoice = false;
                                    }

                                    var DateFromCome = excelInput["DateFromCome"];
                                    if (!string.IsNullOrEmpty(DateFromCome))
                                    {
                                        try
                                        {
                                            obj.DateFromCome = ExcelHelper.ValueToDate(DateFromCome);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateFromCome = Convert.ToDateTime(DateFromCome);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày đến kho [" + DateFromCome + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateFromCome = null;

                                    var DateFromLoadStart = excelInput["DateFromLoadStart"];
                                    if (!string.IsNullOrEmpty(DateFromLoadStart) && obj.DateFromCome != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian vào máng [" + DateFromLoadStart + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateFromCome.Value.Date + time;
                                            obj.DateFromLoadStart = temp;
                                        }
                                    }
                                    else obj.DateFromLoadStart = null;

                                    var DateFromLeave = excelInput["DateFromLeave"];

                                    if (!string.IsNullOrEmpty(DateFromLeave))
                                    {
                                        try
                                        {
                                            obj.DateFromLeave = ExcelHelper.ValueToDate(DateFromLeave);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateFromLeave = Convert.ToDateTime(DateFromLeave);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày rời kho [" + DateFromLeave + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateFromLeave = null;

                                    var DateFromLoadEnd = excelInput["DateFromLoadEnd"];
                                    if (!string.IsNullOrEmpty(DateFromLoadEnd) && obj.DateFromLeave != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian ra máng [" + DateFromLoadEnd + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateFromLeave.Value.Date + time;
                                            obj.DateFromLoadEnd = temp;
                                        }
                                    }
                                    else obj.DateFromLoadEnd = null;

                                    var DateToCome = excelInput["DateToCome"];
                                    if (!string.IsNullOrEmpty(DateToCome))
                                    {
                                        try
                                        {
                                            obj.DateToCome = ExcelHelper.ValueToDate(DateToCome);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateToCome = Convert.ToDateTime(DateToCome);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày đến NPP [" + DateToCome + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateToCome = null;

                                    var DateToLoadStart = excelInput["DateToLoadStart"];
                                    if (!string.IsNullOrEmpty(DateToLoadStart) && obj.DateToCome != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian ra máng [" + DateToLoadStart + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateToCome.Value.Date + time;
                                            obj.DateToLoadStart = temp;
                                        }
                                    }
                                    else obj.DateToLoadStart = null;

                                    var DateToLeave = excelInput["DateToLeave"];
                                    if (!string.IsNullOrEmpty(DateToLeave))
                                    {
                                        try
                                        {
                                            obj.DateToLeave = ExcelHelper.ValueToDate(DateToLeave);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateToLeave = Convert.ToDateTime(DateToLeave);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày rời NPP [" + DateToLeave + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateToLeave = null;

                                    var DateToLoadEnd = excelInput["DateToLoadEnd"];
                                    if (!string.IsNullOrEmpty(DateToLoadEnd) && obj.DateToLeave != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian ra máng [" + DateToLoadEnd + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateToLeave.Value.Date + time;
                                            obj.DateToLoadEnd = temp;
                                        }
                                    }
                                    else obj.DateToLoadEnd = null;

                                    //obj.InvoiceBy = excelInput["InvoiceBy"];

                                    var InvoiceDate = excelInput["InvoiceDate"];
                                    if (!string.IsNullOrEmpty(InvoiceDate))
                                    {
                                        try
                                        {
                                            obj.InvoiceDate = ExcelHelper.ValueToDate(InvoiceDate);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                                            }
                                            catch
                                            {
                                                lstError.Add("InvoiceDate [" + InvoiceDate + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.InvoiceDate = null;

                                    obj.InvoiceNote = excelInput["InvoiceNote"];
                                    obj.Note = excelInput["Note"];
                                    obj.OPSGroupNote1 = excelInput["OPSGroupNote1"];
                                    obj.OPSGroupNote1 = excelInput["OPSGroupNote1"];
                                    obj.ChipNo = excelInput["ChipNo"];
                                    obj.Temperature = excelInput["Temperature"];

                                    var TonTranfer = excelInput["TonTranfer"];
                                    var CBMTranfer = excelInput["CBMTranfer"];
                                    var QuantityTranfer = excelInput["QuantityTranfer"];

                                    if (string.IsNullOrEmpty(TonTranfer) && string.IsNullOrEmpty(CBMTranfer) && string.IsNullOrEmpty(QuantityTranfer))
                                    {
                                        lstError.Add("Tấn lấy, Khối lấy, Số lượng lấy phải có ít nhất 1 cột không được trống");
                                    }

                                    if (!string.IsNullOrEmpty(TonTranfer))
                                    {
                                        try
                                        {
                                            obj.TonTranfer = Convert.ToDouble(TonTranfer);
                                        }
                                        catch
                                        {
                                            lstError.Add("Tấn lấy [" + TonTranfer + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.TonTranfer > 0)
                                            obj.TonTranfer = 0;
                                    }


                                    if (!string.IsNullOrEmpty(CBMTranfer))
                                    {
                                        try
                                        {
                                            obj.CBMTranfer = Convert.ToDouble(CBMTranfer);
                                        }
                                        catch
                                        {
                                            lstError.Add("Khối lấy [" + CBMTranfer + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.CBMTranfer > 0)
                                            obj.CBMTranfer = 0;
                                    }

                                    if (!string.IsNullOrEmpty(QuantityTranfer))
                                    {
                                        try
                                        {
                                            obj.QuantityTranfer = Convert.ToDouble(QuantityTranfer);
                                        }
                                        catch
                                        {
                                            lstError.Add("Số lượng lấy [" + QuantityTranfer + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.QuantityTranfer > 0)
                                            obj.QuantityTranfer = 0;
                                    }

                                    if (obj.TonTranfer < 0 || obj.CBMTranfer < 0 || obj.QuantityTranfer < 0)
                                    {
                                        lstError.Add("Tấn lấy, Khối lấy, Số lượng lấy phải lớn hơn 0");
                                    }

                                    var TonBBGN = excelInput["TonBBGN"];
                                    var CBMBBGN = excelInput["CBMBBGN"];
                                    var QuantityBBGN = excelInput["QuantityBBGN"];

                                    if (!string.IsNullOrEmpty(TonBBGN))
                                    {
                                        try
                                        {
                                            obj.TonBBGN = Convert.ToDouble(TonBBGN);
                                        }
                                        catch
                                        {
                                            lstError.Add("Tấn giao [" + TonBBGN + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.TonBBGN > 0)
                                            obj.TonBBGN = 0;
                                    }
                                    if (obj.TonBBGN > obj.TonTranfer)
                                    {
                                        lstError.Add("Tấn giao phải bé hơn hoặc bằng Tấn lấy");
                                    }

                                    if (!string.IsNullOrEmpty(CBMBBGN))
                                    {
                                        try
                                        {
                                            obj.CBMBBGN = Convert.ToDouble(CBMBBGN);
                                        }
                                        catch
                                        {
                                            lstError.Add("Khối giao [" + CBMBBGN + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.CBMBBGN > 0)
                                            obj.CBMBBGN = 0;
                                    }
                                    if (obj.CBMBBGN > obj.CBMTranfer)
                                    {
                                        lstError.Add("Khối giao phải bé hơn hoặc bằng Khối lấy");
                                    }

                                    if (!string.IsNullOrEmpty(QuantityBBGN))
                                    {
                                        try
                                        {
                                            obj.QuantityBBGN = Convert.ToDouble(QuantityBBGN);
                                        }
                                        catch
                                        {
                                            lstError.Add("Số lượng giao [" + QuantityBBGN + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.QuantityBBGN > 0)
                                            obj.QuantityBBGN = 0;
                                    }
                                    if (obj.QuantityBBGN > obj.QuantityTranfer)
                                    {
                                        lstError.Add("Số lượng giao phải bé hơn hoặc bằng Số lượng lấy");
                                    }

                                    lstError.Distinct();
                                    obj.ExcelError = string.Join(" ", lstError);
                                    if (!string.IsNullOrEmpty(obj.ExcelError))
                                        obj.ExcelSuccess = false;
                                    else
                                    {
                                        obj.ExcelSuccess = true;
                                    }
                                    dataRes.Add(obj);
                                }
                            }
                        }
                    }
                }
                return dataRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Dictionary<string, string> GetDataName()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("ID", "Số thứ tự");
            result.Add("DNCode", "Số DN");
            result.Add("SOCode", "Số SO");
            result.Add("OrderCode", "Mã đơn hàng");
            result.Add("ETARequest", "Ngày y/c giao hàng");
            result.Add("ETD", "ETD");
            result.Add("CustomerCode", "Mã Khách hàng");
            result.Add("CustomerName", "Tên khách hàng");
            result.Add("CreatedDate", "Ngày tạo");
            result.Add("MasterCode", "Mã chuyến");
            result.Add("DriverName", "Tài xế");
            result.Add("DriverTel", "SĐT Tài xế");
            result.Add("DriverCard", "CMND");
            result.Add("RegNo", "Xe");
            result.Add("RequestDate", "Ngày gửi y/c");
            result.Add("LocationFromCode", "Mã kho");
            result.Add("LocationToCode", "Mã NPP");
            result.Add("LocationToName", "NPP");
            result.Add("LocationToAddress", "Địa chỉ");
            result.Add("LocationToProvince", "Tỉnh");
            result.Add("LocationToDistrict", "Quận huyện");
            result.Add("IsInvoice", "Nhận chứng từ");
            result.Add("DateFromCome", "Ngày đến kho");
            result.Add("DateFromLeave", "Ngày rời kho");
            result.Add("DateFromLoadStart", "Thời gian vào máng");
            result.Add("DateFromLoadEnd", "Thời gian ra máng");
            result.Add("DateToCome", "Ngày đến NPP");
            result.Add("DateToLeave", "Ngày rời NPP");
            result.Add("DateToLoadStart", "Thời gian b.đầu dỡ hàng");
            result.Add("DateToLoadEnd", "Thời gian k.thúc dỡ hàng");
            result.Add("InvoiceBy", "Người tạo chứng từ");
            result.Add("InvoiceDate", "Ngày tạo c/t");
            result.Add("InvoiceNote", "Ghi chú c/t");
            result.Add("Note", "Ghi chú");
            result.Add("Note1", "Ghi chú 1");
            result.Add("Note2", "Ghi chú 2");
            result.Add("VendorName", "Nhà vận tải");
            result.Add("VendorCode", "Mã nhà vận tải");
            result.Add("Description", "Description");
            result.Add("GroupOfProductCode", "Mã nhóm sản phẩm");
            result.Add("GroupOfProductName", "Nhóm sản phẩm");
            result.Add("ChipNo", "ChipNo");
            result.Add("Temperature", "Temperature");
            result.Add("Ton", "Ton");
            result.Add("CBM", "CBM");
            result.Add("Quantity", "Số lượng");
            result.Add("TonTranfer", "Tấn lấy");
            result.Add("CBMTranfer", "Khối lấy");
            result.Add("QuantityTranfer", "Số lượng lấy");
            result.Add("TonBBGN", "Tấn giao");
            result.Add("CBMBBGN", "Khối giao");
            result.Add("QuantityBBGN", "Số lượng giao");


            return result;
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
        #endregion

        #region pod check
        [HttpPost]
        public DTOResult PODDIInput_Check_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                bool hasIsReturn = Convert.ToBoolean(dynParam.hasIsReturn);
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_Check_List(request, dtFrom, dtTo, listCustomerID, hasIsReturn);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_Check_Save(dynamic dynParam)
        {
            try
            {
                DTOPODDIInput item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODDIInput>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_Check_Save(item);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODDIInput_Check_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPODDIInput> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODDIInput>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_Check_SaveList(lst);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODDIInput_Check_Reset(dynamic dynParam)
        {
            try
            {
                int DITOGroupID = dynParam.DITOGroupID;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDIInput_Check_Reset(DITOGroupID);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PODMap
        [HttpPost]
        public DTOResult PODMapImport_Index_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODMapImport_Index_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string PODMapImport_Index_Setting_Download(dynamic dynParam)
        {
            try
            {
                string file = "";
                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingPODMap objSetting = new DTOCUSSettingPODMap();
                List<DTOPODImport> data = new List<DTOPODImport>();

                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    data = sv.PODMapImport_Data(dtfrom, dtto, customerID);
                    objSetting = sv.PODMapImport_Index_Setting_Get(templateID);
                });

                string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "DITOGroupProductStatusPODName", "DITOGroupProductStatusPODID", "VehicleID", "IsNew" };
                List<string> sValue = new List<string>(aValue);

                Dictionary<string, string> dicName = PODMapGetDataName();

                if (objSetting != null)
                {
                    file = "/Uploads/temp/" + objSetting.Name.Replace(' ', '-') + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                    file = file.Replace("+", "");
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                    FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                    using (ExcelPackage package = new ExcelPackage(exportfile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(objSetting.Name);
                        if (objSetting.RowStart > 1)
                        {
                            int row = 1;
                            foreach (var prop in objSetting.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSetting, null);
                                        if (v > 0)
                                        {
                                            if (dicName.ContainsKey(p))
                                                worksheet.Cells[row, v].Value = dicName[p];
                                            else
                                                worksheet.Cells[row, v].Value = p;

                                            worksheet.Column(v).Width = 20;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            row = 2;
                            foreach (var item in data)
                            {
                                foreach (var prop in objSetting.GetType().GetProperties())
                                {
                                    try
                                    {
                                        var p = prop.Name;
                                        if (!sValue.Contains(p))
                                        {
                                            var v = (int)prop.GetValue(objSetting, null);
                                            if (v > 0)
                                            {
                                                foreach (var propitem in item.GetType().GetProperties())
                                                {
                                                    var pitem = propitem.Name;
                                                    if (p == pitem)
                                                    {
                                                        var valueitem = propitem.GetValue(item, null);
                                                        Type t = valueitem.GetType();

                                                        if (t.Equals(typeof(bool)))
                                                        {
                                                            bool value = (bool)valueitem;
                                                            if (value == true)
                                                            {
                                                                worksheet.Cells[row, v].Value = "x";
                                                            }
                                                        }
                                                        else if (t.Equals(typeof(DateTime)))
                                                        {
                                                            worksheet.Cells[row, v].Value = valueitem;
                                                            if (pitem == "DateFromLoadStart" || pitem == "DateFromLoadEnd" || pitem == "DateToLoadStart" || pitem == "DateToLoadEnd")
                                                            {
                                                                ExcelHelper.CreateFormat(worksheet, row, v, ExcelHelper.FormatHHMM);
                                                            }
                                                            else if (pitem == "MasterETDDatetime")
                                                            {
                                                                ExcelHelper.CreateFormat(worksheet, row, v, ExcelHelper.FormatDMYHM);
                                                            }
                                                            else
                                                            {
                                                                ExcelHelper.CreateFormat(worksheet, row, v, ExcelHelper.FormatDDMMYYYY);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            worksheet.Cells[row, v].Value = valueitem;
                                                        }

                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                row++;
                            }
                        }

                        package.Save();
                    }
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODMapImport_Excel_Import(dynamic dynParam)
        {
            try
            {
                int templateID = (int)dynParam.TemplateID;

                List<DTOPODImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODImport>>(dynParam.Data.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODMapImport_Excel_Import(templateID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOPODImport> PODMapImport_Excel_Check(dynamic dynParam)
        {
            try
            {
                string file = "/" + dynParam.file.ToString();

                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingPODMap objSetting = new DTOCUSSettingPODMap();
                List<DTOPODImport> data = new List<DTOPODImport>();

                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    data = sv.PODMapImport_Data(dtfrom, dtto, customerID);
                    objSetting = sv.PODMapImport_Index_Setting_Get(templateID);
                });

                var dataRes = new List<DTOPODImport>();


                if (objSetting != null)
                {
                    //Check các required.

                    string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "VehicleID", "IsNew", "VENLoadCodeID", "VENUnLoadCodeID" };

                    var sValue = new List<string>(aValue);

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                for (row = objSetting.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var obj = new DTOPODImport();
                                    var lstError = new List<string>();

                                    var excelInput = PODMapGetDataValue(worksheet, objSetting, row, sValue);
                                    var ShipmentNo = excelInput["ShipmentNo"];
                                    var InvoiceNo = excelInput["InvoiceNo"];
                                    var BillingNo = excelInput["BillingNo"];

                                    if (string.IsNullOrEmpty(ShipmentNo) && string.IsNullOrEmpty(InvoiceNo) && string.IsNullOrEmpty(BillingNo))
                                    {
                                        break;
                                    }

                                    if (objSetting.ShipmentNo > 0)
                                    {
                                        obj.ShipmentNo = ShipmentNo;
                                    }
                                    else
                                    {
                                        obj.ShipmentNo = "";
                                    }
                                    if (objSetting.InvoiceNo > 0)
                                    {
                                        obj.InvoiceNo = InvoiceNo;
                                    }
                                    else
                                    {
                                        obj.InvoiceNo = "";
                                    }
                                    if (objSetting.BillingNo > 0)
                                    {
                                        obj.BillingNo = BillingNo;
                                    }
                                    else
                                    {
                                        obj.BillingNo = "";
                                    }

                                    var InvoiceDate = excelInput["InvoiceDate"];
                                    if (!string.IsNullOrEmpty(InvoiceDate))
                                    {
                                        try
                                        {
                                            obj.InvoiceDate = ExcelHelper.ValueToDate(InvoiceDate);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                                            }
                                            catch
                                            {
                                                lstError.Add("InvoiceDate [" + InvoiceDate + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.InvoiceDate = null;

                                    List<DTOPODImport> lstDetail = null;
                                    lstDetail = PODImportGetDetail_List(excelInput, data, objSetting, ref lstError);
                                    if (lstDetail == null || lstDetail.Count == 0)
                                    {
                                        lstError.Add("Không tìm thấy dữ liệu");
                                    }
                                    else
                                    {
                                        var lstID = lstDetail.Select(c => new DTOPODImportID
                                        {
                                            ID = c.ID,
                                            Value = c.ID.ToString(),
                                        }).ToList();
                                        obj.ListID = lstID;
                                        obj.ID = lstDetail[0].ID;
                                        obj.DNCode = lstDetail[0].DNCode;
                                        obj.SOCode = lstDetail[0].SOCode;
                                        obj.OrderCode = lstDetail[0].OrderCode;
                                        obj.ETARequest = lstDetail[0].ETARequest;
                                        obj.ETD = lstDetail[0].ETD;
                                        obj.MasterETDDate = lstDetail[0].MasterETDDate;
                                        obj.MasterETDDatetime = lstDetail[0].MasterETDDatetime;
                                        obj.OrderGroupETDDate = lstDetail[0].OrderGroupETDDate;
                                        obj.OrderGroupETDDatetime = lstDetail[0].OrderGroupETDDatetime;
                                        obj.CustomerCode = lstDetail[0].CustomerCode;
                                        obj.CustomerName = lstDetail[0].CustomerName;
                                        obj.CreatedDate = lstDetail[0].CreatedDate;
                                        obj.MasterCode = lstDetail[0].MasterCode;
                                        obj.DriverName = lstDetail[0].DriverName;
                                        obj.DriverTel = lstDetail[0].DriverTel;
                                        obj.DriverCard = lstDetail[0].DriverCard;
                                        obj.RegNo = lstDetail[0].RegNo;
                                        obj.RequestDate = lstDetail[0].RequestDate;
                                        obj.LocationFromCode = lstDetail[0].LocationFromCode;
                                        obj.LocationToCode = lstDetail[0].LocationToCode;
                                        obj.LocationToName = lstDetail[0].LocationToName;
                                        obj.LocationToAddress = lstDetail[0].LocationToAddress;
                                        obj.LocationToProvince = lstDetail[0].LocationToProvince;
                                        obj.LocationToDistrict = lstDetail[0].LocationToDistrict;
                                        obj.DistributorCode = lstDetail[0].DistributorCode;
                                        obj.DistributorName = lstDetail[0].DistributorName;
                                        obj.DistributorCodeName = lstDetail[0].DistributorCodeName;
                                        obj.Packing = lstDetail[0].Packing;
                                        obj.IsInvoice = lstDetail[0].DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete ? true : false;
                                        obj.DateFromCome = lstDetail[0].DateFromCome;
                                        obj.DateFromLeave = lstDetail[0].DateFromLeave;
                                        obj.DateFromLoadEnd = lstDetail[0].DateFromLoadEnd;
                                        obj.DateFromLoadStart = lstDetail[0].DateFromLoadStart;
                                        obj.DateToCome = lstDetail[0].DateToCome;
                                        obj.DateToLeave = lstDetail[0].DateToLeave;
                                        obj.DateToLoadEnd = lstDetail[0].DateToLoadEnd;
                                        obj.DateToLoadStart = lstDetail[0].DateToLoadStart;
                                        obj.EconomicZone = lstDetail[0].EconomicZone;
                                        obj.IsOrigin = lstDetail[0].IsOrigin;
                                        obj.InvoiceBy = lstDetail[0].InvoiceBy;
                                        obj.InvoiceNote = lstDetail[0].InvoiceNote;
                                        obj.Note = lstDetail[0].Note;
                                        obj.OPSGroupNote1 = lstDetail[0].OPSGroupNote1;
                                        obj.OPSGroupNote2 = lstDetail[0].OPSGroupNote2;
                                        obj.ORDGroupNote1 = lstDetail[0].ORDGroupNote1;
                                        obj.ORDGroupNote2 = lstDetail[0].ORDGroupNote2;
                                        obj.TOMasterNote1 = lstDetail[0].TOMasterNote1;
                                        obj.TOMasterNote2 = lstDetail[0].TOMasterNote2;
                                        obj.VendorName = lstDetail[0].VendorName;
                                        obj.VendorCode = lstDetail[0].VendorCode;
                                        obj.Description = lstDetail[0].Description;
                                        obj.GroupOfProductCode = lstDetail[0].GroupOfProductCode;
                                        obj.GroupOfProductName = lstDetail[0].GroupOfProductName;
                                        obj.ChipNo = lstDetail[0].ChipNo;
                                        obj.Temperature = lstDetail[0].Temperature;
                                        obj.Ton = lstDetail[0].Ton;
                                        obj.TonBBGN = lstDetail[0].TonBBGN;
                                        obj.TonTranfer = lstDetail[0].TonTranfer;
                                        obj.CBM = lstDetail[0].CBM;
                                        obj.CBMBBGN = lstDetail[0].CBMBBGN;
                                        obj.CBMTranfer = lstDetail[0].CBMTranfer;
                                        obj.Quantity = lstDetail[0].Quantity;
                                        obj.QuantityBBGN = lstDetail[0].QuantityBBGN;
                                        obj.QuantityTranfer = lstDetail[0].QuantityTranfer;
                                        obj.VENLoadCodeID = lstDetail[0].VENLoadCodeID;
                                        obj.VENLoadCode = lstDetail[0].VENLoadCode;
                                        obj.VENUnLoadCodeID = lstDetail[0].VENUnLoadCodeID;
                                        obj.VENUnLoadCode = lstDetail[0].VENUnLoadCode;
                                        obj.ListDetail = lstDetail;
                                    }

                                    lstError.Distinct();
                                    obj.ExcelError = string.Join(", ", lstError);
                                    if (!string.IsNullOrEmpty(obj.ExcelError))
                                        obj.ExcelSuccess = false;
                                    else
                                    {
                                        obj.ExcelSuccess = true;
                                    }
                                    dataRes.Add(obj);
                                }
                            }
                        }
                    }
                }
                return dataRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DTOPODImport> PODImportGetDetail_List(Dictionary<string, string> excelInput, List<DTOPODImport> data, DTOCUSSettingPODMap objSetting, ref List<string> lstError)
        {
            #region Data
            var DNCode = excelInput["DNCode"];
            var SOCode = excelInput["SOCode"];
            var OrderCode = excelInput["OrderCode"];
            var ETARequest = excelInput["ETARequest"];
            var MasterETDDate = excelInput["MasterETDDate"];
            var MasterETDDatetime = excelInput["MasterETDDatetime"];
            var OrderGroupETDDate = excelInput["OrderGroupETDDate"];
            var OrderGroupETDDatetime = excelInput["OrderGroupETDDatetime"];
            var CustomerCode = excelInput["CustomerCode"];
            var CustomerName = excelInput["CustomerName"];
            var CreatedDate = excelInput["CreatedDate"];
            var MasterCode = excelInput["MasterCode"];
            var DriverName = excelInput["DriverName"];
            var DriverTel = excelInput["DriverTel"];
            var DriverCard = excelInput["DriverCard"];
            var RegNo = excelInput["RegNo"];
            var RequestDate = excelInput["RequestDate"];
            var LocationFromCode = excelInput["LocationFromCode"];
            var LocationToCode = excelInput["LocationToCode"];
            var LocationToName = excelInput["LocationToName"];
            var LocationToAddress = excelInput["LocationToAddress"];
            var LocationToProvince = excelInput["LocationToProvince"];
            var LocationToDistrict = excelInput["LocationToDistrict"];
            var DistributorCode = excelInput["DistributorCode"];
            var DistributorName = excelInput["DistributorName"];
            var DistributorCodeName = excelInput["DistributorCodeName"];
            var IsInvoice = excelInput["IsInvoice"];
            var DateFromCome = excelInput["DateFromCome"];
            var DateFromLeave = excelInput["DateFromLeave"];
            var DateFromLoadStart = excelInput["DateFromLoadStart"];
            var DateFromLoadEnd = excelInput["DateFromLoadEnd"];
            var DateToCome = excelInput["DateToCome"];
            var DateToLeave = excelInput["DateToLeave"];
            var DateToLoadStart = excelInput["DateToLoadStart"];
            var DateToLoadEnd = excelInput["DateToLoadEnd"];
            var EconomicZone = excelInput["EconomicZone"];
            var DITOGroupProductStatusPODName = excelInput["DITOGroupProductStatusPODName"];
            var IsOrigin = excelInput["IsOrigin"];
            var InvoiceBy = excelInput["InvoiceBy"];
            var InvoiceDate = excelInput["InvoiceDate"];
            var InvoiceNote = excelInput["InvoiceNote"];
            var Note = excelInput["Note"];
            var OPSGroupNote1 = excelInput["OPSGroupNote1"];
            var OPSGroupNote2 = excelInput["OPSGroupNote2"];
            var ORDGroupNote1 = excelInput["ORDGroupNote1"];
            var ORDGroupNote2 = excelInput["ORDGroupNote2"];
            var TOMasterNote1 = excelInput["TOMasterNote1"];
            var TOMasterNote2 = excelInput["TOMasterNote2"];
            var VendorName = excelInput["VendorName"];
            var VendorCode = excelInput["VendorCode"];
            var Description = excelInput["Description"];
            var GroupOfProductCode = excelInput["GroupOfProductCode"];
            var GroupOfProductName = excelInput["GroupOfProductName"];
            var ChipNo = excelInput["ChipNo"];
            var Temperature = excelInput["Temperature"];
            var Ton = excelInput["Ton"];
            var CBM = excelInput["CBM"];
            var Quantity = excelInput["Quantity"];
            var TonTranfer = excelInput["TonTranfer"];
            var CBMTranfer = excelInput["CBMTranfer"];
            var QuantityTranfer = excelInput["QuantityTranfer"];
            var TonBBGN = excelInput["TonBBGN"];
            var CBMBBGN = excelInput["CBMBBGN"];
            var QuantityBBGN = excelInput["QuantityBBGN"];
            var VENLoadCode = excelInput["VENLoadCode"];
            var VENUnLoadCode = excelInput["VENUnLoadCode"];
            var ShipmentNo = excelInput["ShipmentNo"];
            var InvoiceNo = excelInput["InvoiceNo"];
            var BillingNo = excelInput["BillingNo"];
            var Packing = excelInput["Packing"];
            #endregion
            DateTime _date = new DateTime();
            Double _double = -1;
            List<DTOPODImport> temp = null;
            temp = data;
            if (objSetting.RegNoKey && objSetting.RegNo > 0)
            {
                if (string.IsNullOrEmpty(RegNo))
                {
                    lstError.Add("Số xe không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.RegNo == RegNo).ToList();
                }
            }

            if (objSetting.MasterETDDateKey && objSetting.MasterETDDate > 0)
            {
                if (string.IsNullOrEmpty(MasterETDDate))
                {
                    lstError.Add("Ngày xuất kho không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(MasterETDDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(MasterETDDate);
                        }
                        catch
                        {
                            lstError.Add("Ngày xuất kho [" + MasterETDDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.ETD.HasValue && c.ETD.Value.Date == _date).ToList();
                }
            }

            if (objSetting.MasterETDDatetimeKey && objSetting.MasterETDDatetime > 0)
            {
                if (string.IsNullOrEmpty(MasterETDDatetime))
                {
                    lstError.Add("Ngày giờ xuất không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(MasterETDDatetime);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(MasterETDDatetime);
                        }
                        catch
                        {
                            lstError.Add("Ngày giờ xuất kho [" + MasterETDDatetime + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.ETD == _date).ToList();
                }
            }

            if (objSetting.OrderGroupETDDateKey && objSetting.OrderGroupETDDate > 0)
            {
                if (string.IsNullOrEmpty(OrderGroupETDDate))
                {
                    lstError.Add("Ngày ETD chi tiết đơn không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(OrderGroupETDDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(OrderGroupETDDate);
                        }
                        catch
                        {
                            lstError.Add("Ngày ETD chi tiết đơn [" + OrderGroupETDDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.OrderGroupETDDate.HasValue && c.OrderGroupETDDate.Value.Date == _date.Date).ToList();
                }
            }

            if (objSetting.OrderGroupETDDatetimeKey && objSetting.OrderGroupETDDatetime > 0)
            {
                if (string.IsNullOrEmpty(OrderGroupETDDatetime))
                {
                    lstError.Add("Ngày giờ ETD chi tiết đơn không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(OrderGroupETDDatetime);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(OrderGroupETDDatetime);
                        }
                        catch
                        {
                            lstError.Add("Ngày giờ ETD chi tiết đơn [" + OrderGroupETDDatetime + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.OrderGroupETDDatetime.HasValue && c.OrderGroupETDDatetime.Value == _date).ToList();
                }
            }

            if (string.IsNullOrEmpty(OrderCode))
            {
                lstError.Add("Mã đơn hàng không được trống");
            }
            else
            {
                temp = temp.Where(c => c.OrderCode == OrderCode).ToList();
            }

            if (objSetting.LocationToCodeKey && objSetting.LocationToCode > 0)
            {
                if (string.IsNullOrEmpty(LocationToCode))
                {
                    lstError.Add("Mã điểm giao không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.LocationToCode == LocationToCode).ToList();
                }
            }

            if (objSetting.DistributorCodeKey && objSetting.DistributorCode > 0)
            {
                if (string.IsNullOrEmpty(DistributorCode))
                {
                    lstError.Add("Mã NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorCode == DistributorCode).ToList();
                }
            }

            if (objSetting.DistributorNameKey && objSetting.DistributorName > 0)
            {
                if (string.IsNullOrEmpty(DistributorName))
                {
                    lstError.Add("NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorName == DistributorName).ToList();
                }
            }

            if (objSetting.DistributorCodeNameKey && objSetting.DistributorCodeName > 0)
            {
                if (string.IsNullOrEmpty(DistributorCodeName))
                {
                    lstError.Add("Mã tên NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorCodeName == DistributorCodeName).ToList();
                }
            }

            if (objSetting.PackingKey && objSetting.Packing > 0)
            {
                if (string.IsNullOrEmpty(Packing))
                {
                    lstError.Add("Packing không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.Packing == Packing).ToList();
                }
            }

            if (objSetting.DNCodeKey && objSetting.DNCode > 0)
            {
                if (string.IsNullOrEmpty(DNCode))
                    lstError.Add("Số DN không được trống");
                else 
                    temp = temp.Where(c => c.DNCode == DNCode).ToList();
            }


            if (objSetting.SOCodeKey && objSetting.SOCode > 0)
            {
                if (string.IsNullOrEmpty(SOCode))
                    lstError.Add("Số SO không được trống");
                else 
                    temp = temp.Where(c => c.SOCode == SOCode).ToList();
            }

            if (objSetting.ETARequestKey && objSetting.ETARequest > 0 )
            {
                if(string.IsNullOrEmpty(ETARequest))
                    lstError.Add("Ngày y/c giao hàng không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(ETARequest);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(ETARequest);
                        }
                        catch
                        {
                            lstError.Add("Ngày y/c giao hàng [" + ETARequest + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.ETARequest == _date).ToList();
                }
            }

            if (objSetting.CustomerCodeKey && objSetting.CustomerCode > 0)
            {
                if (string.IsNullOrEmpty(CustomerCode))
                    lstError.Add("Mã Khách hàng không được trống");
                else
                    temp = temp.Where(c => c.CustomerCode == CustomerCode).ToList();
            }

            if (objSetting.CustomerNameKey && objSetting.CustomerName > 0 )
            {
                if (string.IsNullOrEmpty(CustomerName))
                    lstError.Add("Tên khách hàng không được trống");
                else
                    temp = temp.Where(c => c.CustomerName == CustomerName).ToList();
            }

            if (objSetting.CreatedDateKey && objSetting.CreatedDate > 0 )
            {
                if (string.IsNullOrEmpty(CreatedDate))
                    lstError.Add("Ngày tạo không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(CreatedDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(CreatedDate);
                        }
                        catch
                        {
                            lstError.Add("Ngày tạo [" + CreatedDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.CreatedDate == _date).ToList();
                }
            }

            if (objSetting.MasterCodeKey && objSetting.MasterCode > 0)
            {
                if(string.IsNullOrEmpty(MasterCode))
                    lstError.Add("Mã chuyến không được trống");
                else
                    temp = temp.Where(c => c.MasterCode == MasterCode).ToList();
            }

            if (objSetting.DriverNameKey && objSetting.DriverName > 0)
            {
                if(string.IsNullOrEmpty(DriverName))
                    lstError.Add("Tài xế không được trống");
                else
                    temp = temp.Where(c => c.DriverName == DriverName).ToList();
            }

            if (objSetting.DriverTelKey && objSetting.DriverTel > 0)
            {
                if(string.IsNullOrEmpty(DriverTel))
                    lstError.Add("SĐT Tài xế không được trống");
                else
                 temp = temp.Where(c => c.DriverTel == DriverTel).ToList();
            }

            if (objSetting.DriverCardKey && objSetting.DriverCard > 0)
            {
                if(string.IsNullOrEmpty(DriverCard))
                    lstError.Add("CMND không được trống");
                else
                    temp = temp.Where(c => c.DriverCard == DriverCard).ToList();
            }

            if (objSetting.RequestDateKey && objSetting.RequestDate > 0)
            {
                if(string.IsNullOrEmpty(RequestDate))
                    lstError.Add("Ngày gửi y/c không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(RequestDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(RequestDate);
                        }
                        catch
                        {
                            lstError.Add("ETD [" + RequestDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.RequestDate == _date).ToList();
                }
            }

            if (objSetting.LocationFromCodeKey && objSetting.LocationFromCode > 0)
            {
                if(string.IsNullOrEmpty(LocationFromCode))
                    lstError.Add("Mã kho không được trống");
                else
                    temp = temp.Where(c => c.LocationFromCode == LocationFromCode).ToList();
            }

            if (objSetting.LocationToNameKey && objSetting.LocationToName > 0)
            {
                if(string.IsNullOrEmpty(LocationToName))
                    lstError.Add("NPP không được trống");
                else
                    temp = temp.Where(c => c.LocationToName == LocationToName).ToList();
            }

            if (objSetting.LocationToAddressKey && objSetting.LocationToAddress > 0)
            {
                if(string.IsNullOrEmpty(LocationToAddress))
                    lstError.Add("Địa chỉ không được trống");
                else
                    temp = temp.Where(c => c.LocationToAddress == LocationToAddress).ToList();
            }

            if (objSetting.LocationToProvinceKey && objSetting.LocationToProvince > 0)
            {
                if(string.IsNullOrEmpty(LocationToProvince))
                    lstError.Add("Tỉnh không được trống");
                else
                    temp = temp.Where(c => c.LocationToProvince == LocationToProvince).ToList();
            }

            if (objSetting.LocationToDistrictKey && objSetting.LocationToDistrict > 0)
            {
                if(string.IsNullOrEmpty(LocationToDistrict))
                    lstError.Add("Quận huyện không được trống");
                else
                    temp = temp.Where(c => c.LocationToDistrict == LocationToDistrict).ToList();
            }

            if (objSetting.IsInvoiceKey && objSetting.IsInvoice > 0)
            {
                if(string.IsNullOrEmpty(IsInvoice))
                    lstError.Add("Nhận chứng từ không được trống");
                else
                {
                    bool _isInvoice = false;
                    if (IsInvoice.ToLower() == "x")
                    {
                        _isInvoice = true;
                    }
                    temp = temp.Where(c => c.IsInvoice == _isInvoice).ToList();
                }
            }

            if (objSetting.DateFromComeKey && objSetting.DateFromCome > 0)
            {
                if(string.IsNullOrEmpty(DateFromCome))
                    lstError.Add("Ngày đến kho không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateFromCome);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateFromCome);
                        }
                        catch
                        {
                            lstError.Add("Ngày đến kho [" + DateFromCome + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateFromCome == _date).ToList();
                }
            }

            if (objSetting.DateFromLeaveKey && objSetting.DateFromLeave > 0)
            {
                if(string.IsNullOrEmpty(DateFromLeave))
                    lstError.Add("Ngày rời kho không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateFromLeave);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateFromLeave);
                        }
                        catch
                        {
                            lstError.Add("Ngày rời kho [" + DateFromLeave + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateFromLeave == _date).ToList();
                }
            }

            if (objSetting.DateFromLoadStartKey && objSetting.DateFromLoadStart > 0)
            {
                if(string.IsNullOrEmpty(DateFromLoadStart))
                    lstError.Add("Thời gian vào máng không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateFromLoadStart);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateFromLoadStart);
                        }
                        catch
                        {
                            lstError.Add("Thời gian vào máng [" + DateFromLoadStart + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateFromLoadStart == _date).ToList();
                }
            }

            if (objSetting.DateFromLoadEndKey && objSetting.DateFromLoadEnd > 0)
            {
                if(string.IsNullOrEmpty(DateFromLoadEnd))
                    lstError.Add("Thời gian ra máng không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateFromLoadEnd);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateFromLoadEnd);
                        }
                        catch
                        {
                            lstError.Add("Thời gian ra máng [" + DateFromLoadEnd + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateFromLoadEnd == _date).ToList();
                }
            }

            if (objSetting.DateToComeKey && objSetting.DateToCome > 0)
            {
                if(string.IsNullOrEmpty(DateToCome))
                    lstError.Add("Ngày đến NPP không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateToCome);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateToCome);
                        }
                        catch
                        {
                            lstError.Add("Ngày đến NPP [" + DateToCome + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateToCome == _date).ToList();
                }
            }

            if (objSetting.DateToLeaveKey && objSetting.DateToLeave > 0)
            {
                if(string.IsNullOrEmpty(DateToLeave))
                    lstError.Add("Ngày rời NPP không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateToLeave);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateToLeave);
                        }
                        catch
                        {
                            lstError.Add("Ngày rời NPP [" + DateToLeave + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateToLeave == _date).ToList();
                }
            }

            if (objSetting.DateToLoadStartKey && objSetting.DateToLoadStart > 0)
            {
                if(string.IsNullOrEmpty(DateToLoadStart))
                lstError.Add("Thời gian b.đầu dỡ hàng không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateToLoadStart);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateToLoadStart);
                        }
                        catch
                        {
                            lstError.Add("Thời gian b.đầu dỡ hàng [" + DateToLoadStart + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateToLoadStart == _date).ToList();
                }
            }

            if (objSetting.DateToLoadEndKey && objSetting.DateToLoadEnd > 0)
            {
                if(string.IsNullOrEmpty(DateToLoadEnd))
                    lstError.Add("Thời gian k.thúc dỡ hàng không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(DateToLoadEnd);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(DateToLoadEnd);
                        }
                        catch
                        {
                            lstError.Add("Thời gian k.thúc dỡ hàng [" + DateToLoadEnd + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.DateToLoadEnd == _date).ToList();
                }
            }

            if (objSetting.InvoiceByKey && objSetting.InvoiceBy > 0 )
            {
                if(string.IsNullOrEmpty(InvoiceBy))
                    lstError.Add("Người tạo chứng từ không được trống");
                else
                    temp = temp.Where(c => c.InvoiceBy == InvoiceBy).ToList();
            }

            if (objSetting.InvoiceDateKey && objSetting.InvoiceDate > 0)
            {
                if(string.IsNullOrEmpty(InvoiceDate))
                    lstError.Add("Ngày tạo c/t không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(InvoiceDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(InvoiceDate);
                        }
                        catch
                        {
                            lstError.Add("Ngày tạo c/t [" + InvoiceDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.InvoiceDate == _date).ToList();
                }
            }

            if (objSetting.InvoiceNoteKey && objSetting.InvoiceNote > 0 )
            {
                if(string.IsNullOrEmpty(InvoiceNote))
                    lstError.Add("Ghi chú c/t không được trống");
                else
                    temp = temp.Where(c => c.InvoiceNote == InvoiceNote).ToList();
            }

            if (objSetting.NoteKey && objSetting.Note > 0)
            {
                if( string.IsNullOrEmpty(Note))
                    lstError.Add("Ghi chú không được trống");
                else
                    temp = temp.Where(c => c.Note == Note).ToList();
            }

            if (objSetting.OPSGroupNote1Key && objSetting.OPSGroupNote1 > 0)
            {
                if (string.IsNullOrEmpty(OPSGroupNote1))
                    lstError.Add("Ghi chú 1 không được trống");
                else
                    temp = temp.Where(c => c.OPSGroupNote1 == OPSGroupNote1).ToList();
            }

            if (objSetting.OPSGroupNote2Key && objSetting.OPSGroupNote2 > 0)
            {
                if (string.IsNullOrEmpty(OPSGroupNote2))
                    lstError.Add("Ghi chú 2 không được trống");
                else
                    temp = temp.Where(c => c.OPSGroupNote2 == OPSGroupNote2).ToList();
            }

            if (objSetting.ORDGroupNote1Key && objSetting.ORDGroupNote1 > 0)
            {
                if (string.IsNullOrEmpty(ORDGroupNote1))
                    lstError.Add("Ghi chú Đ/h 1 không được trống");
                else
                    temp = temp.Where(c => c.ORDGroupNote1 == ORDGroupNote1).ToList();
            }

            if (objSetting.ORDGroupNote2Key && objSetting.ORDGroupNote2 > 0)
            {
                if (string.IsNullOrEmpty(ORDGroupNote2))
                    lstError.Add("Ghi chú Đ/h 2 không được trống");
                else
                    temp = temp.Where(c => c.ORDGroupNote2 == ORDGroupNote2).ToList();
            }

            if (objSetting.TOMasterNote1Key && objSetting.TOMasterNote1 > 0)
            {
                if (string.IsNullOrEmpty(TOMasterNote1))
                    lstError.Add("Ghi chú chuyến 1 không được trống");
                else
                    temp = temp.Where(c => c.TOMasterNote1 == TOMasterNote1).ToList();
            }

            if (objSetting.TOMasterNote2Key && objSetting.TOMasterNote2 > 0)
            {
                if (string.IsNullOrEmpty(TOMasterNote2))
                    lstError.Add("Ghi chú chuyến 2 không được trống");
                else
                    temp = temp.Where(c => c.TOMasterNote2 == TOMasterNote2).ToList();
            }

            if (objSetting.VendorNameKey && objSetting.VendorName > 0)
            {
                if( string.IsNullOrEmpty(VendorName))
                    lstError.Add("Nhà vận tải không được trống");
                else
                    temp = temp.Where(c => c.VendorName == VendorName).ToList();
            }

            if (objSetting.VendorCodeKey && objSetting.VendorCode > 0 )
            {
                if(string.IsNullOrEmpty(VendorCode))
                    lstError.Add("Mã nhà vận tải không được trống");
                else
                    temp = temp.Where(c => c.VendorCode == VendorCode).ToList();
            }

            if (objSetting.DescriptionKey && objSetting.Description > 0)
            {
                if(string.IsNullOrEmpty(Description))
                    lstError.Add("Mô tả không được trống");
                else
                    temp = temp.Where(c => c.Description == Description).ToList();
            }

            if (objSetting.GroupOfProductCodeKey && objSetting.GroupOfProductCode > 0 )
            {
                if(string.IsNullOrEmpty(GroupOfProductCode))
                    lstError.Add("Mã nhóm sản phẩm không được trống");
                else
                   temp = temp.Where(c => c.GroupOfProductCode == GroupOfProductCode).ToList();
            }

            if (objSetting.GroupOfProductNameKey && objSetting.GroupOfProductName > 0 )
            {
                if(string.IsNullOrEmpty(GroupOfProductName))
                    lstError.Add("Nhóm sản phẩm không được trống");
                else
                    temp = temp.Where(c => c.GroupOfProductName == GroupOfProductName).ToList();
            }

            if (objSetting.ChipNoKey && objSetting.ChipNo > 0)
            {
                if(string.IsNullOrEmpty(ChipNo))
                    lstError.Add("ChipNo không được trống");
                else
                    temp = temp.Where(c => c.ChipNo == ChipNo).ToList();
            }

            if (objSetting.TemperatureKey && objSetting.Temperature > 0)
            {
                if(string.IsNullOrEmpty(Temperature))
                    lstError.Add("Temperature không được trống");
                else
                    temp = temp.Where(c => c.Temperature == Temperature).ToList();
            }

            if (objSetting.TonKey && objSetting.Ton > 0)
            {
                if(string.IsNullOrEmpty(Ton))
                     lstError.Add("Số tấn kế hoạch không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(Ton);
                    }
                    catch
                    {
                        lstError.Add("Số tấn kế hoạch [" + Ton + "] không chính xác");
                    }
                    temp = temp.Where(c => c.Ton == _double).ToList();
                }
            }

            if (objSetting.CBMKey && objSetting.CBM > 0)
            {
                if(string.IsNullOrEmpty(CBM))
                    lstError.Add("Số khối kế hoạch không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(CBM);
                    }
                    catch
                    {
                        lstError.Add("Số khối kế hoạch [" + CBM + "] không chính xác");
                    }
                    temp = temp.Where(c => c.CBM == _double).ToList();
                }
            }

            if (objSetting.QuantityKey && objSetting.Quantity > 0)
            {
                if( string.IsNullOrEmpty(Quantity))
                    lstError.Add("Số lượng kế hoạch không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(Quantity);
                    }
                    catch
                    {
                        lstError.Add("Số lượng kế hoạch [" + Quantity + "] không chính xác");
                    }
                    temp = temp.Where(c => c.Quantity == _double).ToList();
                }
            }

            if (objSetting.TonTranferKey && objSetting.TonTranfer > 0 )
            {
                if (string.IsNullOrEmpty(TonTranfer))
                    lstError.Add("Tấn lấy không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(TonTranfer);
                    }
                    catch
                    {
                        lstError.Add("Tấn lấy  [" + TonTranfer + "] không chính xác");
                    }
                    temp = temp.Where(c => c.TonTranfer == _double).ToList();
                }
            }

            if (objSetting.CBMTranferKey && objSetting.CBMTranfer > 0)
            {
                if (string.IsNullOrEmpty(CBMTranfer))
                    lstError.Add("Khối lấy không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(CBMTranfer);
                    }
                    catch
                    {
                        lstError.Add("Khối lấy [" + CBMTranfer + "] không chính xác");
                    }
                    temp = temp.Where(c => c.CBMTranfer == _double).ToList();
                }
            }

            if (objSetting.QuantityTranferKey && objSetting.QuantityTranfer> 0 )
            {
                if (string.IsNullOrEmpty(QuantityTranfer))
                    lstError.Add("Số lượng lấy không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(QuantityTranfer);
                    }
                    catch
                    {
                        lstError.Add("Số lượng lấy [" + QuantityTranfer + "] không chính xác");
                    }
                    temp = temp.Where(c => c.QuantityTranfer == _double).ToList();
                }
            }

            if (objSetting.TonBBGNKey && objSetting.TonBBGN > 0)
            {
                if (string.IsNullOrEmpty(TonBBGN))
                    lstError.Add("Tấn giao không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(TonBBGN);
                    }
                    catch
                    {
                        lstError.Add("Tấn giao [" + TonBBGN + "] không chính xác");
                    }
                    temp = temp.Where(c => c.TonBBGN == _double).ToList();
                }
            }

            if (objSetting.CBMBBGNKey && objSetting.CBMBBGN > 0 )
            {
                if (string.IsNullOrEmpty(CBMBBGN))
                    lstError.Add("Khối giao không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(CBMBBGN);
                    }
                    catch
                    {
                        lstError.Add("Khối giao [" + CBMBBGN + "] không chính xác");
                    }
                    temp = temp.Where(c => c.CBMBBGN == _double).ToList();
                }
            }

            if (objSetting.QuantityBBGNKey && objSetting.QuantityBBGN > 0 )
            {
                if (string.IsNullOrEmpty(QuantityBBGN))
                    lstError.Add("Số lượng giao không được trống");
                else
                {
                    _double = 0;
                    try
                    {
                        _double = Convert.ToDouble(QuantityBBGN);
                    }
                    catch
                    {
                        lstError.Add("Số lượng giao [" + QuantityBBGN + "] không chính xác");
                    }
                    temp = temp.Where(c => c.QuantityBBGN == _double).ToList();
                }
            }

            if (objSetting.VENLoadCodeKey && objSetting.VENLoadCode > 0)
            {
                if (string.IsNullOrEmpty(VENLoadCode))
                    lstError.Add("Vendor bốc xếp lên không được trống");
                else
                    temp = temp.Where(c => c.VENLoadCode == VENLoadCode).ToList();
            }

            if (objSetting.VENUnLoadCodeKey && objSetting.VENUnLoadCode > 0)
            {
                if (string.IsNullOrEmpty(VENUnLoadCode))
                    lstError.Add("Vendor bốc xếp xuống không được trống");
                else
                    temp = temp.Where(c => c.VENUnLoadCode == VENUnLoadCode).ToList();
            }

            return temp;
        }
        private Dictionary<string, string> PODMapGetDataName()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("ID", "Số thứ tự");
            result.Add("DNCode", "Số DN");
            result.Add("SOCode", "Số SO");
            result.Add("OrderCode", "Mã đơn hàng");
            result.Add("ETARequest", "Ngày y/c giao hàng");
            result.Add("MasterETDDate", "Ngày xuất kho");
            result.Add("MasterETDDatetime", "Ngày giờ xuất kho");
            result.Add("OrderGroupETDDate", "Ngày ETD chi tiết đơn");
            result.Add("OrderGroupETDDatetime", "Ngày giờ ETD chi tiết đơn");
            result.Add("CustomerCode", "Mã Khách hàng");
            result.Add("CustomerName", "Tên khách hàng");
            result.Add("CreatedDate", "Ngày tạo");
            result.Add("MasterCode", "Mã chuyến");
            result.Add("DriverName", "Tài xế");
            result.Add("DriverTel", "SĐT Tài xế");
            result.Add("DriverCard", "CMND");
            result.Add("RegNo", "Xe");
            result.Add("RequestDate", "Ngày gửi y/c");
            result.Add("LocationFromCode", "Mã kho");
            result.Add("LocationToCode", "Mã NPP");
            result.Add("LocationToName", "NPP");
            result.Add("LocationToAddress", "Địa chỉ");
            result.Add("LocationToProvince", "Tỉnh");
            result.Add("LocationToDistrict", "Quận huyện");
            result.Add("DistributorCode", "Mã NPP");
            result.Add("DistributorName", "NPP");
            result.Add("DistributorCodeName", "Mã - Tên NPP");
            result.Add("IsInvoice", "Nhận chứng từ");
            result.Add("DateFromCome", "Ngày đến kho");
            result.Add("DateFromLeave", "Ngày rời kho");
            result.Add("DateFromLoadStart", "Thời gian vào máng");
            result.Add("DateFromLoadEnd", "Thời gian ra máng");
            result.Add("DateToCome", "Ngày đến NPP");
            result.Add("DateToLeave", "Ngày rời NPP");
            result.Add("DateToLoadStart", "Thời gian b.đầu dỡ hàng");
            result.Add("DateToLoadEnd", "Thời gian k.thúc dỡ hàng");
            result.Add("InvoiceBy", "Người nhận chứng từ");
            result.Add("InvoiceDate", "Ngày nhận c/t");
            result.Add("InvoiceNote", "Ghi chú c/t");
            result.Add("Note", "Ghi chú");
            result.Add("OPSGroupNote1", "Ghi chú 1");
            result.Add("OPSGroupNote2", "Ghi chú 2");
            result.Add("ORDGroupNote1", "Ghi chú Đ/h 1");
            result.Add("ORDGroupNote2", "Ghi chú Đ/h 2");
            result.Add("TOMasterNote1", "Ghi chú chuyến 1");
            result.Add("TOMasterNote2", "Ghi chú chuyến 2");
            result.Add("VendorName", "Nhà vận tải");
            result.Add("VendorCode", "Mã nhà vận tải");
            result.Add("Description", "Description");
            result.Add("GroupOfProductCode", "Mã nhóm sản phẩm");
            result.Add("GroupOfProductName", "Nhóm sản phẩm");
            result.Add("ChipNo", "ChipNo");
            result.Add("Temperature", "Temperature");
            result.Add("Ton", "Ton");
            result.Add("CBM", "CBM");
            result.Add("Quantity", "Số lượng");
            result.Add("TonTranfer", "Tấn lấy");
            result.Add("CBMTranfer", "Khối lấy");
            result.Add("QuantityTranfer", "Số lượng lấy");
            result.Add("TonBBGN", "Tấn giao");
            result.Add("CBMBBGN", "Khối giao");
            result.Add("QuantityBBGN", "Số lượng giao");
            result.Add("VENLoadCode", "Vendor bốc xếp lên");
            result.Add("VENUnLoadCode", "Vendor bốc xếp xuống");
            result.Add("ShipmentNo", "ShipmentNo");
            result.Add("InvoiceNo", "InvoiceNo");
            result.Add("BillingNo", "BillingNo");
            result.Add("Packing", "Packing");
            return result;
        }

        private Dictionary<string, string> PODMapGetDataValue(ExcelWorksheet ws, object obj, int row, List<string> sValue)
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

        public List<DTOPODInvoice> PODInvoice_Data(dynamic dynParam)
        {
            try
            {
                int cusId = (int)dynParam.cusId;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date;
                List<DTOPODInvoice> data = new List<DTOPODInvoice>();
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    data = sv.PODInvoice_Data(dtFrom, dtTo, cusId);
                });

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PODInvoice_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPODInvoice> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPODInvoice>>(dynParam.lst.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODInvoice_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region pod upload order
        [HttpPost]
        public DTOResult PODDIInput_UploadOrder_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                     result = sv.PODDIInput_UploadOrder_List(request, dtFrom, dtTo);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void PODDIInput_UploadOrder_Save(dynamic dynParam)
        {
            try
            {
                PODInputUpLoadOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<PODInputUpLoadOrder>(dynParam.item.ToString());
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                      sv.PODDIInput_UploadOrder_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public PODInputUpLoadOrder PODDIInput_UploadOrder_Get(dynamic dynParam)
        {
            try
            {
                int id =(int) dynParam.id;
                var result = default(PODInputUpLoadOrder);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_UploadOrder_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public PODInputUpLoadOrderData PODDIInput_UploadOrder_GetData(dynamic dynParam)
        {
            try
            {
                var result = default(PODInputUpLoadOrderData);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_UploadOrder_GetData();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public SYSExcel PODDIInput_UploadOrder_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());

                var result = default(SYSExcel);

                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDIInput_UploadOrder_ExcelInit(functionid, functionkey, isreload, dtFrom, dtTo);
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

        [HttpPost]
        public Row PODDIInput_UploadOrder_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;

                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVPOD((ISVPOD sv) =>
                    {
                        result  = sv.PODDIInput_UploadOrder_ExcelChange(id, row,cells,lstMessageError); 
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel PODDIInput_UploadOrder_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id; 

                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVPOD((ISVPOD sv) =>
                    { 
                        result = sv.PODDIInput_UploadOrder_ExcelImport(id, lst[0].Rows,lstMessageError );
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool PODDIInput_UploadOrder_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVPOD((ISVPOD sv) =>
                    {
                        result = sv.PODDIInput_UploadOrder_ExcelApprove(id);
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

        #region Close Data

        [HttpPost]
        public DTOResult PODDI_CloseDataList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODDI_CloseDataList(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODDI_CloseDataByDate(dynamic dynParam)
        {
            try
            {
                List<DateTime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DateTime>>(dynParam.lst.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                bool isOpen = dynParam.isOpen;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODDI_CloseDataByDate(listCustomerID, lst, isOpen);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult PODCO_CloseDataList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    result = sv.PODCO_CloseDataList(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PODCO_CloseDataByDate(dynamic dynParam)
        {
            try
            {
                List<DateTime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DateTime>>(dynParam.lst.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                bool isOpen = dynParam.isOpen;
                ServiceFactory.SVPOD((ISVPOD sv) =>
                {
                    sv.PODCO_CloseDataByDate(listCustomerID, lst, isOpen);
                });
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}