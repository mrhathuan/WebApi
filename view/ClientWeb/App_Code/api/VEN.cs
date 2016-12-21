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
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ClientWeb
{
    public class VENController : BaseController
    {
        #region Vendor

        #region Vendor
        public DTOResult Vendor_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Vendor_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VENVendor_ApprovedList(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENVendor_ApprovedList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VENVendor_UnApprovedList(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENVendor_UnApprovedList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOVendor Vendor_Get(dynamic d)
        {
            try
            {
                int vendorID = d.id;
                var result = default(DTOVendor);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Vendor_Get(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int Vendor_Update(dynamic d)
        {
            try
            {
                DTOVendor item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOVendor>(d.item.ToString());
                int result = -1;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Vendor_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Vendor_Destroy(dynamic d)
        {
            try
            {
                DTOVendor item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOVendor>(d.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Vendor_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Vehicle
        public List<CATGroupOfVehicle> VENGroupOfVehicleList()
        {
            try
            {
                var result = default(List<CATGroupOfVehicle>);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENGroupOfVehicleList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Truck_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Truck_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSVehicle Truck_Get(dynamic d)
        {
            try
            {
                int vendorID = d.id;
                var result = new DTOCUSVehicle();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Truck_Get(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSVehicle Truck_Update(dynamic d)
        {
            try
            {
                DTOCUSVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSVehicle>(d.item.ToString());
                int vendorID = d.id;
                item.RegNo = item.RegNo.Trim();
                string p4Num = @"\d{2}[a-zA-Z]-\d{4}$";//29c-1234
                string p5Num = @"\d{2}[a-zA-Z]-\d{5}$";//29c-12345
                string p4Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{4}$";//29LD-1234
                string p5Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{5}$";//29LD-12345
                string p40Num2C = @"[a-zA-Z][a-zA-Z]-\d{4}$";//LD-1234
                string p41Num1C = @"[a-zA-Z]d{1}-\d{4}$";//L2-1234
                bool pass = false;
                if (Regex.Match(item.RegNo, p4Num).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p5Num).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p4Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p5Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p40Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p41Num1C).Success) { pass = true; }
                if (!pass)
                    throw new Exception("Số xe không đúng định dạng (ví dụ:[29c-1234] [29c-12345] [29LD-1234] [29LD-12345] [LD-1234] [L2-1234])");

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Truck_Save(item, vendorID);
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Truck_Destroy(dynamic d)
        {
            try
            {
                DTOCUSVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSVehicle>(d.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Truck_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSVehicle Tractor_Get(dynamic d)
        {
            try
            {
                int vendorID = d.id;
                var result = default(DTOCUSVehicle);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Tractor_Get(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Tractor_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Tractor_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSVehicle Tractor_Update(dynamic d)
        {
            try
            {
                DTOCUSVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSVehicle>(d.item.ToString());
                int vendorID = d.id;
                item.RegNo = item.RegNo.Trim();
                string p4Num = @"\d{2}[a-zA-Z]-\d{4}$";//29c-1234
                string p5Num = @"\d{2}[a-zA-Z]-\d{5}$";//29c-12345
                string p4Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{4}$";//29LD-1234
                string p5Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{5}$";//29LD-12345
                string p40Num2C = @"[a-zA-Z][a-zA-Z]-\d{4}$";//LD-1234
                string p41Num1C = @"[a-zA-Z]d{1}-\d{4}$";//L2-1234
                bool pass = false;
                if (Regex.Match(item.RegNo, p4Num).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p5Num).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p4Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p5Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p40Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p41Num1C).Success) { pass = true; }
                if (!pass)
                    throw new Exception("Số xe không đúng định dạng (ví dụ:[29c-1234] [29c-12345] [29LD-1234] [29LD-12345] [LD-1234] [L2-1234])");

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Tractor_Save(item, vendorID);
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Tractor_Destroy(dynamic d)
        {
            try
            {
                DTOCUSVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSVehicle>(d.item.ToString());

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Tractor_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DTOCUSRomooc Romooc_Get(dynamic d)
        {
            try
            {
                int vendorID = d.id;
                var result = default(DTOCUSRomooc);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Romooc_Get(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DTOResult Romooc_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Romooc_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSRomooc Romooc_Update(dynamic d)
        {
            try
            {
                DTOCUSRomooc item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSRomooc>(d.item.ToString());
                int vendorID = d.id;
                item.RegNo = item.RegNo.Trim();
                string p4Num = @"\d{2}[a-zA-Z]-\d{4}$";//29c-1234
                string p5Num = @"\d{2}[a-zA-Z]-\d{5}$";//29c-12345
                string p4Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{4}$";//29LD-1234
                string p5Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{5}$";//29LD-12345
                string p40Num2C = @"[a-zA-Z][a-zA-Z]-\d{4}$";//LD-1234
                string p41Num1C = @"[a-zA-Z]d{1}-\d{4}$";//L2-1234
                bool pass = false;
                if (Regex.Match(item.RegNo, p4Num).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p5Num).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p4Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p5Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p40Num2C).Success) { pass = true; }
                if (Regex.Match(item.RegNo, p41Num1C).Success) { pass = true; }
                if (!pass)
                    throw new Exception("Số xe không đúng định dạng (ví dụ:[29c-1234] [29c-12345] [29LD-1234] [29LD-12345] [LD-1234] [L2-1234])");

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Romooc_Save(item, vendorID);
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Romooc_Destroy(dynamic d)
        {
            try
            {
                DTOCUSRomooc item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSRomooc>(d.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Romooc_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATTruck_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.CATTruck_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATTractor_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.CATTractor_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRomooc_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.CATRomooc_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     

        [HttpPost]
        public SYSExcel Romooc_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int vendorID = (int)dynParam.RommocID;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Romooc_ExcelInit(vendorID, functionid, functionkey, isreload);
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
        public Row Romooc_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.Romooc_ExcelChange(id, row, cells, lstMessageError);
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
        public SYSExcel Romooc_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.Romooc_ExcelImport(id, lst[0].Rows, lstMessageError);
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
        public bool Romooc_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;
                int vendorID = dynParam.RommocID;

                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.Romooc_ExcelApprove(vendorID, id);
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
        public DTOResult Tractor_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int vendorID = dynParam.vendorID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Tractor_NotInList(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void Tractor_NotInSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int vendorID = dynParam.vendorID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Tractor_NotInSave(lst, vendorID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Truck_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int vendorID = dynParam.vendorID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Truck_NotInList(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void Truck_NotInSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int vendorID = dynParam.vendorID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Truck_NotInSave(lst, vendorID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult Romooc_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int vendorID = dynParam.vendorID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Romooc_NotInList(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void Romooc_NotInSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int vendorID = dynParam.vendorID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Romooc_NotInSave(lst, vendorID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region excel

        [HttpPost]
        public string VEN_Truck_Export(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                string type = dynParam.type;
                List<DTOCUSVehicle_Excel> resBody = new List<DTOCUSVehicle_Excel>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    resBody = sv.Truck_Export(customerID, type);
                });
                string file = "/Uploads/temp/" + type + "_KH" + customerID + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(f))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Số xe"; worksheet.Column(col).Width = 15;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row + 1, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    col++; worksheet.Cells[row, col].Value = "Trọng tải"; worksheet.Column(col).Width = 15;
                    ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row + 1, col].Value = "Đăng ký"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row + 1, col].Value = "Tối đa"; worksheet.Column(col).Width = 15;
                    ExcelHelper.CreateCellStyle(worksheet, row + 1, col - 1, row + 1, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    col++; worksheet.Cells[row, col].Value = "Số khối"; worksheet.Column(col).Width = 15;
                    ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row + 1, col].Value = "Đăng ký"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row + 1, col].Value = "Tối đa"; worksheet.Column(col).Width = 15;
                    ExcelHelper.CreateCellStyle(worksheet, row + 1, col - 1, row + 1, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet.Cells[1, 1, row + 1, col].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row + 1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row + 1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, row + 1, col].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row + 1, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, row + 1, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Top.Color.SetColor(Color.Black);
                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Bottom.Color.SetColor(Color.Black);
                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Left.Color.SetColor(Color.Black);
                    worksheet.Cells[1, 1, row + 1, col].Style.Border.Right.Color.SetColor(Color.Black);

                    #endregion

                    #region Body
                    int stt = 0;
                    row = 2;
                    foreach (var item in resBody)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.RegNo;
                        col++; worksheet.Cells[row, col].Value = item.RegWeight;
                        col++; worksheet.Cells[row, col].Value = item.MaxWeight;
                        col++; worksheet.Cells[row, col].Value = item.RegCapacity;
                        col++; worksheet.Cells[row, col].Value = item.MaxCapacity;
                    }
                    #endregion

                    pk.Save();
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSVehicle_Excel> VEN_Truck_Check(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                string type = dynParam.type;
                string file = "/" + dynParam.file.ToString();
                string p4Num = @"\d{2}[a-zA-Z]-\d{4}$";//29c-1234
                string p5Num = @"\d{2}[a-zA-Z]-\d{5}$";//29c-12345
                string p4Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{4}$";//29LD-1234
                string p5Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{5}$";//29LD-12345
                string p40Num2C = @"[a-zA-Z][a-zA-Z]-\d{4}$";//LD-1234
                string p41Num1C = @"[a-zA-Z]d{1}-\d{4}$";//L2-1234

                List<DTOCUSVehicle_Excel> result = new List<DTOCUSVehicle_Excel>();
                DTOCUSVehicle_Check lstCheck = new DTOCUSVehicle_Check();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    lstCheck = sv.Truck_Check(customerID);
                });
                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {

                            int row = 1;
                            int iTruckType = -(int)SYSVarType.TypeOfVehicleTruck;
                            int iTractorType = -(int)SYSVarType.TypeOfVehicleTractor;
                            for (row = 3; row <= worksheet.Dimension.End.Row; row++)
                            {
                                DTOCUSVehicle_Excel obj = new DTOCUSVehicle_Excel();
                                int col = 1;
                                obj.ExcelSuccess = true;
                                obj.ExcelRow = row;
                                col++; string strRegNo = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strRegWeight = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strMaxWeight = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strRegCapacity = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strMaxCapcity = ExcelHelper.GetValue(worksheet, row, col);

                                int catid = 0;
                                int id = 0;
                                if (string.IsNullOrEmpty(strRegNo))
                                    break;
                                var checkonfile = result.FirstOrDefault(c => c.RegNo == strRegNo);
                                if (checkonfile == null)
                                {
                                    if (!string.IsNullOrEmpty(strRegNo))
                                    {
                                        strRegNo = strRegNo.Trim();

                                        bool pass = false;
                                        if (Regex.Match(strRegNo, p4Num).Success) { pass = true; }
                                        if (Regex.Match(strRegNo, p5Num).Success) { pass = true; }
                                        if (Regex.Match(strRegNo, p4Num2C).Success) { pass = true; }
                                        if (Regex.Match(strRegNo, p5Num2C).Success) { pass = true; }
                                        if (Regex.Match(strRegNo, p40Num2C).Success) { pass = true; }
                                        if (Regex.Match(strRegNo, p41Num1C).Success) { pass = true; }
                                        if (!pass)
                                            obj.ExcelError += "Số xe không đúng định dạng (ví dụ:[29c-1234] [29c-12345] [29LD-1234] [29LD-12345] [LD-1234] [L2-1234])";

                                        var qrid = lstCheck.lstCUSVehicle.FirstOrDefault(c => c.RegNo == strRegNo.Trim());
                                        if (qrid != null)
                                        {
                                            id = qrid.ID;
                                            catid = qrid.VehicleID;
                                            if (type == "truck")
                                            {
                                                if (qrid.TypeOfVehicleID == iTruckType)
                                                {
                                                    id = qrid.ID;
                                                    catid = qrid.VehicleID;
                                                }
                                                else
                                                {
                                                    obj.ExcelError += "Số xe '" + strRegNo + "' đã tồn tại (thuộc đầu kéo)"; obj.ExcelSuccess = false;
                                                }
                                            }
                                            else
                                            {
                                                if (qrid.TypeOfVehicleID == iTractorType)
                                                {
                                                    id = qrid.ID;
                                                    catid = qrid.VehicleID;
                                                }
                                                else
                                                {
                                                    obj.ExcelError += "Số xe '" + strRegNo + "' đã tồn tại (thuộc xe tải)"; obj.ExcelSuccess = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var qrcat = lstCheck.lstCATVehicle.FirstOrDefault(c => c.RegNo == strRegNo.Trim());
                                            if (qrcat != null)
                                            {
                                                if (type == "truck")
                                                {
                                                    if (qrcat.TypeOfVehicleID == iTruckType)
                                                    {
                                                        catid = qrcat.ID;
                                                    }
                                                    else
                                                    {
                                                        obj.ExcelError += "Số xe '" + strRegNo + "' đã tồn tại (thuộc đầu kéo)"; obj.ExcelSuccess = false;
                                                    }
                                                }
                                                else
                                                {
                                                    if (qrcat.TypeOfVehicleID == iTractorType)
                                                    {
                                                        catid = qrcat.ID;
                                                    }
                                                    else
                                                    {
                                                        obj.ExcelError += "Số xe '" + strRegNo + "' đã tồn tại (thuộc xe tải)"; obj.ExcelSuccess = false;
                                                    }
                                                }


                                            }
                                        }
                                    }
                                    else
                                    {
                                        obj.ExcelError += "Số xe không được trống"; obj.ExcelSuccess = false;
                                    }
                                }
                                else
                                {
                                    obj.ExcelError += "Số xe đã trùng trên file"; obj.ExcelSuccess = false;
                                }
                                //kiem tra kieu so

                                double? maxcap = null;
                                double? maxwei = null;
                                double? regcap = null;
                                double? regwei = null;
                                try
                                {
                                    if (string.IsNullOrEmpty(strMaxCapcity))
                                        maxcap = null;
                                    else
                                    {
                                        maxcap = double.Parse(strMaxCapcity);
                                    }

                                    if (string.IsNullOrEmpty(strMaxWeight))
                                        maxwei = null;
                                    else
                                    {
                                        maxwei = double.Parse(strMaxWeight);
                                    }

                                    if (string.IsNullOrEmpty(strRegCapacity))
                                        regcap = null;
                                    else
                                    {
                                        regcap = double.Parse(strRegCapacity);
                                    }

                                    if (string.IsNullOrEmpty(strRegWeight))
                                        regwei = null;
                                    else
                                    {
                                        regwei = double.Parse(strRegWeight);
                                    }
                                }
                                catch
                                {
                                    obj.ExcelError += "Sai kiểu dữ liệu"; obj.ExcelSuccess = false;
                                }

                                obj.ID = id;
                                obj.RegNo = strRegNo;
                                obj.VehicleID = catid;
                                obj.MaxCapacity = maxcap;
                                obj.MaxWeight = maxwei;
                                obj.RegCapacity = regcap;
                                obj.RegWeight = regwei;
                                result.Add(obj);
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

        [HttpPost]
        public void VEN_Truck_Import(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                string type = dynParam.type;
                List<DTOCUSVehicle_Excel> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSVehicle_Excel>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Truck_Import(data, customerID, type);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public SYSExcel VEN_Truck_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int customerID = (int)dynParam.customerID;
                string type = dynParam.type;
                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VEN_Truck_ExcelInit(customerID, type,functionid, functionkey, isreload);
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
        public Row VEN_Truck_ExcelChange(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Truck_ExcelChange(customerID,id, row, cells, lstMessageError);
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
        public SYSExcel VEN_Truck_ExcelImport(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Truck_ExcelImport(customerID,id, lst[0].Rows, lstMessageError);
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
        public bool VEN_Truck_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;
                int customerID = (int)dynParam.customerID;
                string type = dynParam.type;

                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Truck_ExcelApprove(customerID,type,id);
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
        public SYSExcel VEN_Tractor_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int customerID = (int)dynParam.customerID;
                string type = dynParam.type;
                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VEN_Tractor_ExcelInit(customerID, type, functionid, functionkey, isreload);
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
        public Row VEN_Tractor_ExcelChange(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Tractor_ExcelChange(customerID, id, row, cells, lstMessageError);
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
        public SYSExcel VEN_Tractor_ExcelImport(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Tractor_ExcelImport(customerID, id, lst[0].Rows, lstMessageError);
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
        public bool VEN_Tractor_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;
                int customerID = (int)dynParam.customerID;
                string type = dynParam.type;

                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Tractor_ExcelApprove(customerID, type, id);
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

        #endregion

        #region Company
        public DTOResult Company_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Company_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Company_Delete(dynamic d)
        {
            try
            {
                DTOCUSCompany item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSCompany>(d.item.ToString());
                var lst = new List<DTOCUSCompany>();
                lst.Add(item);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Company_DeleteList(lst);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CompanyNotInList_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.CompanyNotIn_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CompanyNotInList_Save(dynamic d)
        {
            try
            {
                List<DTOCUSCompany> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSCompany>>(d.lst.ToString());
                int vendorID = d.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.CompanyNotIn_SaveList(lst, vendorID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Routing
        [HttpPost]
        public DTOResult Routing_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.Routing_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult RoutingNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.RoutingNotIn_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult RoutingCusNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.RoutingCusNotIn_List(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Routing_Delete(dynamic d)
        {
            try
            {
                DTOCUSRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSRouting>(d.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Routing_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool RoutingNotIn_SaveList(dynamic d)
        {
            try
            {
                List<DTOCATRouting> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATRouting>>(d.lst.ToString());
                int vendorID = d.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.RoutingNotIn_SaveList(lst, vendorID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Routing_Update(dynamic d)
        {
            try
            {
                int vendorid = d.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.Routing_Update(vendorid);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }

        #endregion

        #region Group of Product
        [HttpPost]
        public DTOResult GroupOfProductAll_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int customerID = d.id;
                int gopID = d.gopID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.GroupOfProduct_List(request, customerID);
                });
                var lst = new List<DTOCUSGroupOfProduct>();
                DropdownList_Read_Create(lst, result.Data.Cast<DTOCUSGroupOfProduct>(), null, 0);
                foreach (DTOCUSGroupOfProduct item in lst)
                {
                    if (item.ID == gopID)
                    {
                        lst.Remove(item);
                        break;
                    }
                }
                result.Data = lst;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult GroupOfProduct_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int customerID = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.GroupOfProduct_List(request, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSGroupOfProduct GroupOfProduct_Update(dynamic d)
        {
            try
            {
                DTOCUSGroupOfProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSGroupOfProduct>(d.item.ToString());
                int customerID = d.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    item.ID = sv.GroupOfProduct_Save(item, customerID);
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool GroupOfProduct_Destroy(dynamic d)
        {
            try
            {
                DTOCUSGroupOfProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSGroupOfProduct>(d.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.GroupOfProduct_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CUSGroupOfProduct GroupOfProduct_GetByCode(dynamic d)
        {
            try
            {
                string code = d.code;
                int customerID = d.id;
                CUSGroupOfProduct result = new CUSGroupOfProduct();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.GroupOfProduct_GetByCode(code, customerID);
                });
                return result;
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public bool GroupOfProduct_ResetPrice(dynamic d)
        {
            try
            {
                int customerID = d.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.GroupOfProduct_ResetPrice(customerID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult GroupOfProductMapping_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int groupOfProductID = d.groupOfProductID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.GroupOfProductMapping_List(request, groupOfProductID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult GroupOfProductMappingNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int groupOfProductID = d.groupOfProductID;
                int venderID = d.venID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.GroupOfProductMappingNotIn_List(request, groupOfProductID, venderID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool GroupOfProductMapping_SaveList(dynamic d)
        {
            try
            {
                List<DTOCUSGroupOfProductMapping> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSGroupOfProductMapping>>(d.lst.ToString());
                int groupOfProductID = d.groupOfProductID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.GroupOfProductMapping_SaveList(lst, groupOfProductID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GroupOfProductMapping_Delete(dynamic d)
        {
            try
            {
                DTOCUSGroupOfProductMapping item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSGroupOfProductMapping>(d.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.GroupOfProductMapping_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DropdownList_Read_Create(List<DTOCUSGroupOfProduct> lstTarget, IEnumerable<DTOCUSGroupOfProduct> lstSource, int? parentid, int level)
        {
            foreach (var item in lstSource.Where(c => c.ParentID == parentid))
            {
                item.GroupName = new string('.', 3 * level) + item.GroupName;
                lstTarget.Add(item);
                DropdownList_Read_Create(lstTarget, lstSource, item.ID, level + 1);
            }
        }

        #endregion

        #region driver
        public DTOResult VENDriver_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorId = (int)d.vendorId;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENDriver_List(request, vendorId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOVENDriver VENDriver_Get(dynamic d)
        {
            try
            {
                int id = (int)d.id;
                var result = default(DTOVENDriver);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENDriver_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int VENDriver_Save(dynamic d)
        {
            try
            {
                DTOVENDriver item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOVENDriver>(d.item.ToString());
                int vendorId = (int)d.vendorId;
                int result = 0;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENDriver_Save(item, vendorId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool VENDriver_Delete(dynamic d)
        {
            try
            {
                DTOVENDriver item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOVENDriver>(d.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENDriver_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult VENDriver_NotInList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int vendorId = (int)d.vendorId;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENDriver_NotInList(request, vendorId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENDriver_NotInSave(dynamic d)
        {
            try
            {
                List<DTOVENDriver> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOVENDriver>>(d.lst.ToString());
                int vendorId = (int)d.vendorId;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENDriver_NotInSave(lst, vendorId);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string VENDriver_Excel_Export(dynamic dynParam)
        {
            try
            {
                int vendorId = (int)dynParam.vendorId;
                List<DTOVENDriverImport> listDriver = new List<DTOVENDriverImport>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    listDriver = sv.VENDriver_ExportByVendor(vendorId);
                });
                string file = "/Uploads/temp/" + "VENTaixe" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Số CMND"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Họ"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Số điện thoại"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày sinh"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Loại bằng lái"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số bằng lái"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 25;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");


                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    #endregion

                    #region Body
                    int stt = 0;
                    foreach (DTOVENDriverImport driver in listDriver)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = driver.CardNumber;
                        col++; worksheet.Cells[row, col].Value = driver.LastName;
                        col++; worksheet.Cells[row, col].Value = driver.FirstName;
                        col++; worksheet.Cells[row, col].Value = driver.Cellphone;
                        col++; worksheet.Cells[row, col].Value = driver.Birthday;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        if (driver.ListDriverLicence != null && driver.ListDriverLicence.Count() > 0)
                        {
                            DTOCATDriverLicence driverLicence = driver.ListDriverLicence.FirstOrDefault();
                            col++; worksheet.Cells[row, col].Value = driverLicence.DrivingLicenceCode;
                            col++; worksheet.Cells[row, col].Value = driverLicence.DrivingLicenceNumber;
                        }
                        else
                        {
                            col++; worksheet.Cells[row, col].Value = "";
                            col++; worksheet.Cells[row, col].Value = "";
                        }
                        col++; worksheet.Cells[row, col].Value = driver.Note;
                    }
                    #endregion

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    pk.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VENDriver_Excel_Save(dynamic dynParam)
        {
            try
            {
                int vendorId = (int)dynParam.vendorId;
                List<DTOVENDriverImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOVENDriverImport>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((IServices.ISVVendor sv) =>
                {
                    sv.VENDriver_Import(lst, vendorId);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOVENDriverImport> VENDriver_Excel_Check(dynamic dynParam)
        {
            try
            {
                int vendorId = (int)dynParam.vendorId;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                List<DTOVENDriverImport> result = new List<DTOVENDriverImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOVENDriverData driver_data = new DTOVENDriverData();
                    List<DTOCATDrivingLicence> lstDriverLicence = new List<DTOCATDrivingLicence>();
                    ServiceFactory.SVVendor((IServices.ISVVendor sv) =>
                    {
                        driver_data = sv.VENDriver_Data(vendorId);
                    });

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        lstDriverLicence = sv.ALL_CATDrivingLicence().Data.Cast<DTOCATDrivingLicence>().ToList();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string CardNumber, LastName, FirstName, Cellphone, Birthday, Note, DrivingLicenceCode, DrivingLicenceNumber;
                            if (worksheet != null)
                            {
                                row = 2;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();
                                    DTOVENDriverImport obj = new DTOVENDriverImport();
                                    obj.ExcelRow = row;

                                    col = 2;
                                    CardNumber = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(CardNumber))
                                    {

                                    }
                                    else
                                    {
                                        if (result.Count(c => c.CardNumber == CardNumber) > 0)
                                        {
                                            lstError.Add("Số CMND [" + CardNumber + "] đã sử dụng");
                                        }
                                        else
                                        {
                                            var driverCAT = driver_data.ListCATDriver.FirstOrDefault(c => c.CardNumber == CardNumber);
                                            if (driverCAT != null && !string.IsNullOrEmpty(CardNumber))
                                            {
                                                obj.DriverID = driverCAT.DriverID;
                                            }
                                            else
                                            {
                                                obj.DriverID = 0;
                                            }
                                            var driverCUS = driver_data.ListCUSDriver.FirstOrDefault(c => c.CardNumber == CardNumber);
                                            if (driverCUS != null && !string.IsNullOrEmpty(CardNumber))
                                            {
                                                obj.ID = driverCUS.ID;
                                            }
                                            else
                                            {
                                                obj.ID = 0;
                                            }
                                            obj.CardNumber = CardNumber;
                                        }
                                    }

                                    col++;
                                    LastName = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(LastName))
                                        lstError.Add("[Họ] không được trống");
                                    else obj.LastName = LastName;

                                    col++;
                                    FirstName = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(FirstName))
                                        lstError.Add("[Tên] không được trống");
                                    else obj.FirstName = FirstName;


                                    if (string.IsNullOrEmpty(CardNumber) && string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(FirstName))
                                    {
                                        break;
                                    }

                                    col++;
                                    Cellphone = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Cellphone = Cellphone;

                                    col++;
                                    Birthday = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(Birthday))
                                        obj.Birthday = null;
                                    else
                                    {
                                        try
                                        {
                                            obj.Birthday = ExcelHelper.ValueToDate(Birthday);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.Birthday = Convert.ToDateTime(Birthday);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày sinh [" + Birthday + "] không không đúng định dạng");
                                            }
                                        }
                                    }

                                    col++;
                                    DrivingLicenceCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++;
                                    DrivingLicenceNumber = ExcelHelper.GetValue(worksheet, row, col);

                                    //Check
                                    if (!string.IsNullOrEmpty(DrivingLicenceCode) && string.IsNullOrEmpty(DrivingLicenceNumber))
                                    {
                                        lstError.Add("[Số bằng lái] không được trống");
                                    }
                                    if (!string.IsNullOrEmpty(DrivingLicenceNumber) && string.IsNullOrEmpty(DrivingLicenceCode))
                                    {
                                        lstError.Add("[Loại bằng lái] không được trống");
                                    }

                                    obj.ListDriverLicence = new List<DTOCATDriverLicence>();
                                    if (!string.IsNullOrEmpty(DrivingLicenceNumber) && !string.IsNullOrEmpty(DrivingLicenceCode))
                                    {
                                        var drivingLicence = driver_data.ListDrivingLicence.FirstOrDefault(c => c.Code == DrivingLicenceCode);
                                        DTOCATDriverLicence licence = new DTOCATDriverLicence();
                                        if (drivingLicence != null)
                                        {
                                            licence.DrivingLicenceCode = drivingLicence.Code;
                                            licence.DrivingLicenceID = drivingLicence.ID;

                                            var driverLicence = driver_data.ListDriverLicence.FirstOrDefault(c => c.DrivingLicenceID == drivingLicence.ID && c.DriverID == obj.DriverID);
                                            if (driverLicence != null)
                                            {
                                                licence.ID = driverLicence.ID;
                                            }
                                            else
                                            {
                                                licence.ID = 0;
                                            }

                                            licence.DrivingLicenceNumber = DrivingLicenceNumber;
                                            obj.ListDriverLicence.Add(licence);
                                        }
                                        else
                                        {
                                            lstError.Add("Loại bằng lái [" + DrivingLicenceCode + "] không tồn tại");
                                        }
                                    }

                                    col++;
                                    Note = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Note = Note;

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


        [HttpPost]
        public SYSExcel VEN_Driver_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int vendorID = (int)dynParam.VendorID;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VEN_Driver_ExcelInit(vendorID, functionid, functionkey, isreload);
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
        public Row VEN_Driver_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int vendorID = (int)dynParam.VendorID;

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Driver_ExcelChange(vendorID,id, row, cells, lstMessageError);
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
        public SYSExcel VEN_Driver_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int vendorID = (int)dynParam.VendorID;

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Driver_ExcelImport(vendorID,id, lst[0].Rows, lstMessageError);
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
        public bool VEN_Driver_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;
                int vendorID = (int)dynParam.VendorID;
                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VEN_Driver_ExcelApprove(vendorID, id);
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
        public DTOResult VENDriver_DrivingLicence_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int driverId = (int)dynParam.driverId;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENDriver_DrivingLicence_List(request, driverId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENDriver_DrivingLicence_Save(dynamic dynParam)
        {
            try
            {
                DTOCATDriverLicence item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATDriverLicence>(dynParam.item.ToString());
                int driverId = (int)dynParam.driverId;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENDriver_DrivingLicence_Save(item, driverId);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATDriverLicence VENDriver_DrivingLicence_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATDriverLicence);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENDriver_DrivingLicence_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENDriver_DrivingLicence_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATDriverLicence item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATDriverLicence>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENDriver_DrivingLicence_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region danh sách địa chỉ
        public DTOResult VENLocation_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int vendorId = (int)dynParam.vendorId;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENLocation_List(request, vendorId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VENLocationSaveLoad_List(dynamic dynParam)
        {
            try
            {
                List<DTOCUSLocationInVEN> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSLocationInVEN>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENLocationSaveLoad_List(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VENLocation_SaveList(dynamic dynParam)
        {
            try
            {
                List<CATLocation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CATLocation>>(dynParam.lst.ToString());
                int vendorId = (int)dynParam.vendorId;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENLocation_SaveList(vendorId, lst);
                    AddressSearchHelper.AddListByCustomerID(vendorId, sv.AddressSearch_ByCustomerList(vendorId));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void VENLocation_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENLocation_Delete(id);
                    AddressSearchHelper.Delete(sv.AddressSearch_List(id));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult VENLocation_HasRun(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int vendorId = (int)dynParam.vendorId;
                var result = default(DTOResult);
                //ServiceFactory.SVVendor((ISVVendor sv) =>
                //{
                //    result = sv.VENLocation_List(request, vendorId);
                //});
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult VENLocation_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int vendorId = (int)dynParam.vendorId;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENLocation_NotInList(request, vendorId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public SYSExcel VENLocation_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int vendorId = (int)dynParam.vendorId;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENLocation_ExcelInit(functionid, functionkey, isreload, vendorId);
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
        public Row VENLocation_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int vendorId = (int)dynParam.vendorId;

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = result = sv.VENLocation_ExcelChange(id, row, cells, lstMessageError, vendorId);
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
        public SYSExcel VENLocation_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int vendorId = (int)dynParam.vendorId;

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENLocation_ExcelImport(id, lst[0].Rows, lstMessageError, vendorId);
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
        public bool VENLocation_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                int vendorId = (int)dynParam.vendorId;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENLocation_ExcelApprove(id, vendorId);
                        if (result)
                            AddressSearchHelper.AddListByCustomerID(vendorId, sv.AddressSearch_ByCustomerList(vendorId));
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
        public DTOResult VENLocation_RoutingContract_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int locationid = (int)dynParam.locationid;
                int vendorId = (int)dynParam.vendorId;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENLocation_RoutingContract_List(request, vendorId, locationid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENLocation_RoutingContract_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> lstClear = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstClear.ToString());
                List<int> lstAdd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstAdd.ToString());
                int locationid = (int)dynParam.locationid;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENLocation_RoutingContract_SaveList(lstClear, lstAdd, locationid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENLocation_RoutingContract_NewRoutingSave(dynamic dynParam)
        {
            try
            {
                DTOCUSPartnerNewRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPartnerNewRouting>(dynParam.item.ToString());
                int vendorId = (int)dynParam.vendorId;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENLocation_RoutingContract_NewRoutingSave(item, vendorId);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENLocation_RoutingContract_NewAreaSave(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                int locationid = (int)dynParam.locationid;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENLocation_RoutingContract_NewAreaSave(item, locationid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENLocation_RoutingContract_AreaList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENLocation_RoutingContract_AreaList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSPartnerNewRouting VENLocation_RoutingContract_NewRoutingGet(dynamic dynParam)
        {
            try
            {
                int vendorId = (int)dynParam.vendorId;
                var result = default(DTOCUSPartnerNewRouting);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENLocation_RoutingContract_NewRoutingGet(vendorId);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOCATContract> VENLocation_RoutingContract_ContractData(dynamic dynParam)
        {
            try
            {
                int vendorId = (int)dynParam.vendorId;
                var result = default(List<DTOCATContract>);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENLocation_RoutingContract_ContractData(vendorId);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region VENContract

        #region Common

        [HttpPost]
        public DTOResult VENContract_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATContract VENContract_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATContract);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int VENContract_Save(dynamic dynParam)
        {
            try
            {
                DTOCATContract item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContract>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    item.ID = sv.VENContract_Save(item);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOVENContract_Data VENContract_Data(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                DTOVENContract_Data result = new DTOVENContract_Data();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Data(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CODefault

        [HttpPost]
        public DTOResult VENContract_CODefault_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_CODefault_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_CODefault_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_CODefault_NotInList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_CODefault_NotIn_SaveList(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATPacking> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATPacking>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_CODefault_NotIn_SaveList(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_CODefault_Delete(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_CODefault_Delete(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_CODefault_Update(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATContractCODefault> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATContractCODefault>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_CODefault_Update(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Routing

        [HttpPost]
        public DTOResult VENContract_Routing_List(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Routing_List(contractID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_Routing_CATNotIn_List(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Routing_CATNotIn_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Routing_CATNotIn_Save(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATRouting>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Routing_CATNotIn_Save(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_Routing_NotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Routing_NotIn_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Routing_NotIn_Delete(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                int contractID = (int)dynParam.contractID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Routing_NotIn_Delete(ID, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Routing_Save(dynamic dynParam)
        {
            try
            {
                DTOCATContractRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractRouting>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Routing_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Routing_Insert(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATRouting>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Routing_Insert(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Routing_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Routing_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string VENContract_Routing_Export(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOVENContractRouting_Import> resBody = new List<DTOVENContractRouting_Import>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    resBody = sv.VENContract_Routing_Export(contractID);
                });

                var lstContractRoutingType = new List<SYSVar>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    lstContractRoutingType = sv.ALL_SysVar(SYSVarType.ContractRoutingType).Data.Cast<SYSVar>().ToList(); ;
                });

                string file = "/Uploads/temp/" + "CungDuong" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "Thứ tự"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã hệ thống"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên hệ thống"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Zone"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Leadtime"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Phụ lục hợp đồng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã loại cung đường"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Loại cung đường"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Theo khu vực"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Khu vực đi"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Khu vực đến"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Điểm đi"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Điểm đến"; worksheet.Column(col).Width = 15;

                    worksheet.Cells[1, 1, row, col].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Color.SetColor(Color.White);

                    #endregion

                    #region Body
                    int stt = 0;
                    foreach (DTOVENContractRouting_Import item in resBody)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = item.SortOrder;
                        col++; worksheet.Cells[row, col].Value = item.CATRoutingCode;
                        col++; worksheet.Cells[row, col].Value = item.CATRoutingName;
                        col++; worksheet.Cells[row, col].Value = item.ContractRoutingCode;
                        col++; worksheet.Cells[row, col].Value = item.ContractRoutingName;
                        col++; worksheet.Cells[row, col].Value = item.Zone;
                        col++; worksheet.Cells[row, col].Value = item.LeadTime;

                        col++; worksheet.Cells[row, col].Value = item.ContractTermCode;
                        col++; worksheet.Cells[row, col].Value = item.ContractRoutingTypeCode;
                        col++; worksheet.Cells[row, col].Value = item.ContractRoutingType;
                        col++; worksheet.Cells[row, col].Value = item.IsArea ? "x" : string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.AreaFromCode;
                        col++; worksheet.Cells[row, col].Value = item.AreaToCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationFromCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToCode;
                    }
                    #endregion

                    ExcelWorksheet worksheet1 = pk.Workbook.Worksheets.Add("Danh sách loại CĐ");
                    col = 0; row = 1;

                    #region Header
                    col++; worksheet1.Cells[row, col].Value = "Thứ tự"; worksheet1.Column(col).Width = 15;
                    col++; worksheet1.Cells[row, col].Value = "Mã loại cung đường"; worksheet1.Column(col).Width = 15;
                    col++; worksheet1.Cells[row, col].Value = "Loại cung đường"; worksheet1.Column(col).Width = 15;
                    for (int i = 1; i <= col; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet1, 1, i, 1, i, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }
                    #endregion

                    #region Body
                    stt = 0;
                    foreach (SYSVar item in lstContractRoutingType)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet1.Cells[row, col].Value = stt;
                        col++; worksheet1.Cells[row, col].Value = item.Code;
                        col++; worksheet1.Cells[row, col].Value = item.ValueOfVar;
                    }
                    #endregion
                    pk.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOVENContractRouting_Import> VENContract_Routing_Excel_Check(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                int customerID = (int)dynParam.customerID;
                string file = "/" + dynParam.file.ToString();

                List<DTOVENContractRouting_Import> result = new List<DTOVENContractRouting_Import>();
                DTOVENContractRoutingData resRouting = new DTOVENContractRoutingData();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    resRouting = sv.VENContract_RoutingByCus_List(customerID, contractID);
                });

                var lstContractRoutingType = new List<SYSVar>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    lstContractRoutingType = sv.ALL_SysVar(SYSVarType.ContractRoutingType).Data.Cast<SYSVar>().ToList(); ;
                });

                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {

                            int row = 1;
                            for (row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                DTOVENContractRouting_Import obj = new DTOVENContractRouting_Import();
                                List<string> lstError = new List<string>();
                                int col = 0;
                                obj.ExcelSuccess = true;
                                obj.ExcelRow = row;
                                col++; string strSortOrder = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strCatCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strCatName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strConCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strConName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strZone = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strLeadtime = ExcelHelper.GetValue(worksheet, row, col);

                                col++; string strContractTerm = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strContractRoutingTypeCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++;
                                col++; string strIsArea = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strAreaFrom = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strAreaTo = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strLocationFrom = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strLocationTo = ExcelHelper.GetValue(worksheet, row, col);

                                if (string.IsNullOrEmpty(strCatCode) && string.IsNullOrEmpty(strCatName))
                                    break;

                                //kiem tra cat routing
                                obj.CATRoutingCode = strCatCode;
                                obj.CATRoutingName = strCatName;
                                obj.ContractRoutingCode = strConCode;
                                obj.ContractRoutingName = strConName;

                                if (!string.IsNullOrEmpty(strCatCode))
                                {
                                    var checkCAT = resRouting.ListCATRouting.FirstOrDefault(c => c.Code == strCatCode);
                                    if (checkCAT != null)
                                    {
                                        obj.CATRoutingID = checkCAT.ID;
                                        var checkOnFile = result.FirstOrDefault(c => c.CATRoutingCode == strCatCode);
                                        if (checkOnFile != null) lstError.Add("Mã hệ thống bị trùng trên file");
                                    }
                                    else
                                    {
                                        obj.CATRoutingID = 0;
                                        var checkOnFile = result.FirstOrDefault(c => c.CATRoutingCode == strCatCode);
                                        if (checkOnFile != null) lstError.Add("Mã hệ thống bị trùng trên file");
                                    }

                                    var checkCUS = resRouting.ListCUSRouting.FirstOrDefault(c => c.RoutingID == obj.CATRoutingID);
                                    if (checkCUS != null)
                                        obj.CUSRoutingID = checkCUS.ID;
                                    else
                                        obj.CUSRoutingID = 0;

                                    if (!string.IsNullOrEmpty(strConCode))
                                    {
                                        //kiem tra ma trong hop dong
                                        var checkContract = resRouting.ListContractRouting.FirstOrDefault(c => c.Code == strConCode);
                                        if (checkContract == null)
                                        {
                                            obj.ContractRoutingID = 0;
                                            //kiem tra tren file
                                            var checkfile = result.FirstOrDefault(c => c.ContractRoutingCode == strConCode);
                                            if (checkfile != null)
                                                lstError.Add("Mã cung đường [" + strConCode + "]bị trùng trên file");
                                        }
                                        else
                                        {
                                            if (checkContract.RoutingID != checkCAT.ID)
                                                lstError.Add("Mã cung đường [" + strConCode + "] đã sử dụng trong hợp đồng");
                                            else
                                            {
                                                obj.ContractRoutingID = checkContract.ID;
                                                var checkfile = result.FirstOrDefault(c => c.ContractRoutingCode == strConCode);
                                                if (checkfile != null)
                                                    lstError.Add("Mã cung đường [" + strConCode + "]bị trùng trên file");
                                            }
                                        }
                                    }
                                    else lstError.Add("Mã cung đường không được trống.");
                                }
                                else lstError.Add("Mã hệ thống không được trống.");

                                if (string.IsNullOrEmpty(strZone))
                                    obj.Zone = null;
                                else
                                {
                                    try { obj.Zone = Convert.ToDouble(strZone); }
                                    catch { lstError.Add("Zone[" + strZone + "] không chính xác"); }
                                }
                                if (string.IsNullOrEmpty(strLeadtime))
                                    obj.LeadTime = null;
                                else
                                {
                                    try { obj.LeadTime = Convert.ToDouble(strLeadtime); }
                                    catch { lstError.Add("LeadTime[" + strLeadtime + "] không chính xác"); }
                                }

                                if (string.IsNullOrEmpty(strSortOrder))
                                {
                                    obj.SortOrder = 0;
                                }
                                else
                                {
                                    try { obj.SortOrder = Convert.ToInt32(strSortOrder); }
                                    catch { lstError.Add("Thứ tự[" + strSortOrder + "] không chính xác"); }
                                }
                                //check phụ lục hợp đồng
                                if (string.IsNullOrEmpty(strContractTerm))
                                {
                                    obj.ContractTermID = -1;
                                }
                                else
                                {
                                    var checkTerm = resRouting.ListContractTerm.FirstOrDefault(c => c.Code == strContractTerm);
                                    if (checkTerm != null)
                                        obj.ContractTermID = checkTerm.ID;
                                    else
                                        lstError.Add("Phụ lục[" + strContractTerm + "] không tồn tại");
                                }

                                //check loai cung đường
                                if (string.IsNullOrEmpty(strContractRoutingTypeCode))
                                {
                                    lstError.Add("Mã loại cung đường [" + strContractRoutingTypeCode + "] không được trống");
                                }
                                else
                                {
                                    var checkType = lstContractRoutingType.FirstOrDefault(c => c.Code == strContractRoutingTypeCode);
                                    if (checkType != null)
                                        obj.ContractRoutingTypeID = checkType.ID;
                                    else
                                        lstError.Add("Mã loại cung đường [" + strContractRoutingTypeCode + "] không tồn tại");
                                }

                                //theo khu vực/ điểm
                                if (string.IsNullOrEmpty(strIsArea))
                                    obj.IsArea = false;
                                else
                                    obj.IsArea = true;

                                //check khu vuc
                                if (obj.IsArea)
                                {
                                    if (!string.IsNullOrEmpty(strAreaFrom))
                                    {
                                        var checkArea = resRouting.ListArea.FirstOrDefault(c => c.Code == strAreaFrom);
                                        if (checkArea != null)
                                            obj.AreaFromID = checkArea.ID;
                                        else lstError.Add("Khu vực đi" + strAreaFrom + " không tại trong hệ thống");
                                    }
                                    else lstError.Add("Khu vực đi không được trống");

                                    if (!string.IsNullOrEmpty(strAreaTo))
                                    {
                                        var checkArea = resRouting.ListArea.FirstOrDefault(c => c.Code == strAreaTo);
                                        if (checkArea != null)
                                            obj.AreaToID = checkArea.ID;
                                        else lstError.Add("Khu vực đến" + strAreaFrom + " không tại trong hệ thống");
                                    }
                                    else lstError.Add("Khu vực đến không được trống");

                                    obj.LocationFromID = obj.LocationToID = -1;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(strLocationFrom))
                                    {
                                        var checkLo = resRouting.ListLocation.FirstOrDefault(c => c.Code == strLocationFrom);
                                        if (checkLo != null)
                                            obj.LocationFromID = checkLo.ID;
                                        else lstError.Add("Điểm đi" + strLocationFrom + " không tại trong hệ thống");
                                    }
                                    else lstError.Add("Điểm đi không được trống");

                                    if (!string.IsNullOrEmpty(strLocationTo))
                                    {
                                        var checkLo = resRouting.ListLocation.FirstOrDefault(c => c.Code == strLocationTo);
                                        if (checkLo != null)
                                            obj.LocationToID = checkLo.ID;
                                        else lstError.Add("Điểm đến" + strLocationTo + " không tại trong hệ thống");
                                    }
                                    else lstError.Add("Điểm đến không được trống");

                                    obj.AreaFromID = obj.AreaToID = -1;
                                }

                                obj.ExcelSuccess = lstError.Count() > 0 ? false : true;
                                obj.ExcelError = string.Join(" ,", lstError);
                                result.Add(obj);
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

        [HttpPost]
        public void VENContract_Routing_Import(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOVENContractRouting_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOVENContractRouting_Import>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Routing_Import(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_KPI_Save(dynamic dynParam)
        {
            try
            {
                List<DTOContractKPITime> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractKPITime>>(dynParam.data.ToString());
                int routingID = (int)dynParam.routingID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_KPI_Save(data, routingID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DateTime? VENContract_KPI_Check_Expression(dynamic dynParam)
        {
            try
            {
                string expression = dynParam.Expression.ToString();
                KPIKPITime item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIKPITime>(dynParam.item.ToString());
                List<DTOContractKPITime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractKPITime>>(dynParam.lst.ToString());
                double leadTime = (double)dynParam.leadTime;
                double zone = (double)dynParam.zone;
                DateTime? result = null;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPI_Check_Expression(expression, item, zone, leadTime, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool? VENContract_KPI_Check_Hit(dynamic dynParam)
        {
            try
            {
                string expression = dynParam.Expression.ToString();
                string field = dynParam.Field.ToString();
                KPIKPITime item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIKPITime>(dynParam.item.ToString());
                List<DTOContractKPITime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractKPITime>>(dynParam.lst.ToString());
                double leadTime = (double)dynParam.leadTime;
                double zone = (double)dynParam.zone;
                bool? result = null;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPI_Check_Hit(expression, field, item, zone, leadTime, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_KPI_Routing_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                int routingID = (int)dynParam.routingID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPI_Routing_List(request, contractID, routingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_KPI_Routing_Apply(dynamic dynParam)
        {
            try
            {
                List<DTOCATContractRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATContractRouting>>(dynParam.data.ToString());
                int routingID = (int)dynParam.routingID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_KPI_Routing_Apply(data, routingID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOContractTerm> VENContract_Routing_ContractTermList(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOContractTerm> result = new List<DTOContractTerm>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Routing_ContractTermList(contractID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel VENContract_Routing_ExcelOnline_Init(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var contractID = (int)dynParam.contractID;
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Routing_ExcelOnline_Init(contractID, functionid, functionkey, isreload);
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
        public Row VENContract_Routing_ExcelOnline_Change(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var contractID = (int)dynParam.contractID;
                var customerID = (int)dynParam.customerID;
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENContract_Routing_ExcelOnline_Change(contractID, customerID, id, row, cells, lstMessageError);
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
        public SYSExcel VENContract_Routing_ExcelOnline_Import(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var contractID = (int)dynParam.contractID;
                var customerID = (int)dynParam.customerID;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENContract_Routing_ExcelOnline_Import(contractID, customerID, id, lst[0].Rows, lstMessageError);
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
        public bool VENContract_Routing_ExcelOnline_Approve(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var contractID = (int)dynParam.contractID;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENContract_Routing_ExcelOnline_Approve(id, contractID);
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

        #region Price

        #region Common

        [HttpPost]
        public DTOResult VENContract_Price_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractTermID = (int)dynParam.contractTermID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Price_List(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOPrice VENContract_Price_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPrice);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Price_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int VENContract_Price_Save(dynamic dynParam)
        {
            try
            {
                DTOPrice item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPrice>(dynParam.item.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    item.ID = sv.VENContract_Price_Save(item, contractTermID);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Price_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Price_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOVENPrice_Data VENContract_Price_Data(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                DTOVENPrice_Data result = new DTOVENPrice_Data();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Price_Data(contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Price_Copy(dynamic dynParam)
        {
            try
            {
                List<DTOVENPrice_ItemCopy> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOVENPrice_ItemCopy>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Price_Copy(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Price_DeletePriceNormal(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Price_DeletePriceNormal(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Price_DeletePriceLevel(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Price_DeletePriceLevel(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GroupVehicle

        #region old
        [HttpPost]
        public List<DTOPriceGroupVehicle> VENPrice_DI_GroupVehicle_GetData(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(List<DTOPriceGroupVehicle>);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_GroupVehicle_GetData(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_GroupVehicle_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceGroupVehicle> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGroupVehicle>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_GroupVehicle_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public string VENPrice_DI_GroupVehicle_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                DTOPriceGroupVehicleData data = new DTOPriceGroupVehicleData();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    data = sv.VENPrice_DI_GroupVehicle_ExcelData(priceID, contractTermID);
                });

                string result = "/" + FolderUpload.Export + "Bảng giá FTL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 1, row = 1, stt = 1;

                    #region header
                    if (isFrame)
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 20;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row + 1, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        foreach (var level in data.ListGOV)
                        {
                            col++; worksheet.Cells[row, col].Value = level.Code;
                            worksheet.Cells[row + 1, col].Value = "Giá từ";
                            col++; worksheet.Cells[row + 1, col].Value = "Đến giá";
                            ExcelHelper.CreateCellStyle(worksheet, row, col - 1, row, col, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                            ExcelHelper.CreateCellStyle(worksheet, row + 1, col - 1, row + 1, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        }
                        worksheet.Cells[row, 1, row + 1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1, row + 1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 20;
                        foreach (var level in data.ListGOV)
                        {
                            col++; worksheet.Cells[row, col].Value = level.Code;
                        }
                        ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    #endregion

                    #region data

                    if (isFrame)
                    {
                        col = 1;
                        row = 3;
                        foreach (var route in data.ListRoute)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = route.Code;
                            col++; worksheet.Cells[row, col].Value = route.RoutingName;
                            foreach (var level in data.ListGOV)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == level.ID).FirstOrDefault();
                                if (check != null)
                                {
                                    worksheet.Cells[row, col].Value = check.PriceMin;
                                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                    worksheet.Cells[row, col + 1].Value = check.PriceMax;
                                    ExcelHelper.CreateFormat(worksheet, row, col + 1, ExcelHelper.FormatMoney);
                                }
                                col++;
                            }
                            row++;
                            stt++;
                        }
                    }
                    else
                    {
                        col = 1;
                        row = 2;
                        foreach (var route in data.ListRoute)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = route.Code;
                            col++; worksheet.Cells[row, col].Value = route.RoutingName;
                            foreach (var level in data.ListGOV)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == level.ID).FirstOrDefault();
                                if (check != null)
                                {
                                    worksheet.Cells[row, col].Value = check.Price;
                                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                }
                            }
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    package.Save();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOPriceGroupVehicleImport> VENPrice_DI_GroupVehicle_ExcelCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                List<DTOPriceGroupVehicleImport> result = new List<DTOPriceGroupVehicleImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOPriceGroupVehicleData data = new DTOPriceGroupVehicleData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENPrice_DI_GroupVehicle_ExcelData(priceID, contractTermID);
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 4, row = 1;
                            string levelCode = "", Input = "";
                            Dictionary<int, int> dictColLevel = new Dictionary<int, int>();
                            Dictionary<int, string> dictColLevelCode = new Dictionary<int, string>();
                            if (worksheet != null)
                            {
                                if (isFrame)
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListGOV.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Loại xe [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 2;
                                    }

                                    row = 3;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceGroupVehicleImport obj = new DTOPriceGroupVehicleImport();
                                        obj.ListDetail = new List<DTOPriceGroupVehicleExcel>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string priceMin = ExcelHelper.GetValue(worksheet, row, pCol);
                                                string priceMax = ExcelHelper.GetValue(worksheet, row, pCol + 1);


                                                if (!string.IsNullOrEmpty(priceMin) || !string.IsNullOrEmpty(priceMax))
                                                {
                                                    DTOPriceGroupVehicleExcel objDetail = new DTOPriceGroupVehicleExcel();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.RouteID = obj.RouteID;
                                                    objDetail.GroupOfVehicleID = pLevel;
                                                    objDetail.Price = 0;
                                                    if (string.IsNullOrEmpty(priceMin)) objDetail.PriceMin = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMin = Convert.ToDecimal(priceMin);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của loại [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    if (string.IsNullOrEmpty(priceMax)) objDetail.PriceMax = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMax = Convert.ToDecimal(priceMax);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của loại [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
                                }
                                else
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListGOV.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Loại xe [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 1;
                                    }

                                    row = 2;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceGroupVehicleImport obj = new DTOPriceGroupVehicleImport();
                                        obj.ListDetail = new List<DTOPriceGroupVehicleExcel>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string price = ExcelHelper.GetValue(worksheet, row, pCol);


                                                if (!string.IsNullOrEmpty(price))
                                                {
                                                    DTOPriceGroupVehicleExcel objDetail = new DTOPriceGroupVehicleExcel();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.RouteID = obj.RouteID;
                                                    objDetail.GroupOfVehicleID = pLevel;
                                                    objDetail.PriceMax = null;
                                                    objDetail.PriceMin = null;
                                                    try
                                                    {
                                                        objDetail.Price = Convert.ToDecimal(price);
                                                    }
                                                    catch
                                                    {
                                                        objDetail.IsSuccess = false;
                                                        lstError.Add("Giá của loại[" + pLevelCode + "] không chính xác");
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
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
        [HttpPost]
        public void VENPrice_DI_GroupVehicle_ExcelImport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceGroupVehicleImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGroupVehicleImport>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_GroupVehicle_ExcelImport(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel VENPrice_DI_GroupVehicle_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;

                var result = default(SYSExcel);

               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_GroupVehicle_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row VENPrice_DI_GroupVehicle_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                   ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_GroupVehicle_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
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
        public SYSExcel VENPrice_DI_GroupVehicle_ExcelOnImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0)
                {
                   ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_GroupVehicle_ExcelOnImport(isFrame, priceID, contractTermID, id, lst[0].Rows, lstMessageError);
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
        public bool VENPrice_DI_GroupVehicle_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;
                var isFrame = (bool)dynParam.isFrame;
                var result = false;
                if (id > 0)
                {
                   ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_GroupVehicle_ExcelApprove(isFrame, priceID, contractTermID, id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region new
        [HttpPost]
        public List<DTOPriceGVLevelGroupVehicle> VENPrice_DI_PriceGVLevel_DetailData(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.id;
                List<DTOPriceGVLevelGroupVehicle> result = new List<DTOPriceGVLevelGroupVehicle>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceGVLevel_DetailData(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceGVLevel_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceGVLevelGroupVehicle> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGVLevelGroupVehicle>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceGVLevel_Save(lst, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public string VENPrice_DI_PriceGVLevel_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                DTOPriceGVLevelData data = new DTOPriceGVLevelData();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    data = sv.VENPrice_DI_PriceGVLevel_ExcelData(priceID, contractTermID);
                });

                string result = "/" + FolderUpload.Export + "Bảng giá FTL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 1, row = 1, stt = 1;

                    #region header
                    if (isFrame)
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 20;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row + 1, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        foreach (var level in data.ListLevel)
                        {
                            col++; worksheet.Cells[row, col].Value = level.Code;
                            worksheet.Cells[row + 1, col].Value = "Giá từ";
                            col++; worksheet.Cells[row + 1, col].Value = "Đến giá";
                            ExcelHelper.CreateCellStyle(worksheet, row, col - 2, row, col, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                            ExcelHelper.CreateCellStyle(worksheet, row + 2, col - 2, row + 1, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        }
                        worksheet.Cells[row, 1, row + 1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1, row + 1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 20;
                        foreach (var level in data.ListLevel)
                        {
                            col++; worksheet.Cells[row, col].Value = level.Code;
                        }
                        ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    #endregion

                    #region data

                    if (isFrame)
                    {
                        col = 1;
                        row = 3;
                        foreach (var route in data.ListRoute)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = route.Code;
                            col++; worksheet.Cells[row, col].Value = route.RoutingName;
                            foreach (var level in data.ListLevel)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
                                if (check != null)
                                {
                                    worksheet.Cells[row, col].Value = check.PriceMin;
                                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                    worksheet.Cells[row, col + 1].Value = check.PriceMax;
                                    ExcelHelper.CreateFormat(worksheet, row, col + 1, ExcelHelper.FormatMoney);
                                }
                                col += 2;
                            }
                            row++;
                            stt++;
                        }
                    }
                    else
                    {
                        col = 1;
                        row = 2;
                        foreach (var route in data.ListRoute)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = route.Code;
                            col++; worksheet.Cells[row, col].Value = route.RoutingName;
                            foreach (var level in data.ListLevel)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
                                if (check != null)
                                {
                                    worksheet.Cells[row, col].Value = check.Price;
                                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                }
                            }
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    package.Save();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOPriceGVLevelImport> VENPrice_DI_PriceGVLevel_ExcelCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                List<DTOPriceGVLevelImport> result = new List<DTOPriceGVLevelImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOPriceGVLevelData data = new DTOPriceGVLevelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENPrice_DI_PriceGVLevel_ExcelData(priceID, contractTermID);
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 4, row = 1;
                            string levelCode = "", Input = "";
                            Dictionary<int, int> dictColLevel = new Dictionary<int, int>();
                            Dictionary<int, string> dictColLevelCode = new Dictionary<int, string>();
                            if (worksheet != null)
                            {
                                if (isFrame)
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListLevel.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Bậc giá [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 2;
                                    }

                                    row = 3;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceGVLevelImport obj = new DTOPriceGVLevelImport();
                                        obj.ListDetail = new List<DTOPriceGVLevelGroupVehicleExcel>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string priceMin = ExcelHelper.GetValue(worksheet, row, pCol);
                                                string priceMax = ExcelHelper.GetValue(worksheet, row, pCol + 1);


                                                if (!string.IsNullOrEmpty(priceMin) || !string.IsNullOrEmpty(priceMax))
                                                {
                                                    DTOPriceGVLevelGroupVehicleExcel objDetail = new DTOPriceGVLevelGroupVehicleExcel();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.RouteID = obj.RouteID;
                                                    objDetail.LevelID = pLevel;
                                                    objDetail.Price = 0;
                                                    if (string.IsNullOrEmpty(priceMin)) objDetail.PriceMin = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMin = Convert.ToDecimal(priceMin);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của bậc [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    if (string.IsNullOrEmpty(priceMax)) objDetail.PriceMax = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMax = Convert.ToDecimal(priceMax);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của bậc [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
                                }
                                else
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListLevel.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Bậc giá [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 1;
                                    }

                                    row = 2;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceGVLevelImport obj = new DTOPriceGVLevelImport();
                                        obj.ListDetail = new List<DTOPriceGVLevelGroupVehicleExcel>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string price = ExcelHelper.GetValue(worksheet, row, pCol);


                                                if (!string.IsNullOrEmpty(price))
                                                {
                                                    DTOPriceGVLevelGroupVehicleExcel objDetail = new DTOPriceGVLevelGroupVehicleExcel();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.RouteID = obj.RouteID;
                                                    objDetail.LevelID = pLevel;
                                                    objDetail.PriceMax = null;
                                                    objDetail.PriceMin = null;
                                                    try
                                                    {
                                                        objDetail.Price = Convert.ToDecimal(price);
                                                    }
                                                    catch
                                                    {
                                                        objDetail.IsSuccess = false;
                                                        lstError.Add("Giá của bậc[" + pLevelCode + "] không chính xác");
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
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
        [HttpPost]
        public void VENPrice_DI_PriceGVLevel_ExcelImport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceGVLevelImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGVLevelImport>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceGVLevel_ExcelImport(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel VENPrice_DI_PriceGVLevel_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceGVLevel_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row VENPrice_DI_PriceGVLevel_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_PriceGVLevel_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
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
        public SYSExcel VENPrice_DI_PriceGVLevel_ExcelOnImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_PriceGVLevel_ExcelOnImport(isFrame, priceID, contractTermID, id, lst[0].Rows, lstMessageError);
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
        public bool VENPrice_DI_PriceGVLevel_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;
                var isFrame = (bool)dynParam.isFrame;
                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_PriceGVLevel_ExcelApprove(isFrame, priceID, contractTermID, id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_GroupVehicle_GOVList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_GroupVehicle_GOVList(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_GroupVehicle_GOVDelete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_GroupVehicle_GOVDelete(lst, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_GroupVehicle_GOVNotInList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_GroupVehicle_GOVNotInList(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_GroupVehicle_GOVNotInSave(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_GroupVehicle_GOVNotInSave(lst, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
        #endregion

        #region GroupProduct

        [HttpPost]
        public List<DTOPriceDIGroupOfProduct> VENPrice_DI_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(List<DTOPriceDIGroupOfProduct>);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_GroupProduct_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_GroupProduct_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceDIGroupOfProduct> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDIGroupOfProduct>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_GroupProduct_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string VENPrice_DI_GroupProduct_Export(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                DTOPriceDIGroupOfProductData resBody = new DTOPriceDIGroupOfProductData();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    resBody = sv.VENPrice_DI_GroupProduct_Export(priceID);
                });
                string file = "/Uploads/temp/" + "BangGiaLTL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(f))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 25;

                    foreach (CUSGroupOfProduct item in resBody.ListGOP)
                    {
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        worksheet.Column(col).Width = 10;
                    }

                    worksheet.Cells[1, 1, row, col].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Color.SetColor(Color.White);

                    #endregion

                    #region Body
                    int stt = 0;
                    foreach (DTOCATRouting item in resBody.ListRoute)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = item.RoutingName;

                        foreach (CUSGroupOfProduct o in resBody.ListGOP)
                        {
                            col++;
                            var e = resBody.ListDetail.Where(c => c.ContractRoutingID == item.ID && o.ID == c.GroupOfProductID).FirstOrDefault();
                            if (e != null)
                            {
                                worksheet.Cells[row, col].Value = e.Price;
                            }
                        }
                    }
                    #endregion

                    pk.Save();
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOPriceDIGroupOfProductImport> VENPrice_DI_GroupProduct_Check(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string file = dynParam.file.ToString();
                bool isFrame = (bool)dynParam.isFrame;
                List<DTOPriceDIGroupOfProductImport> result = new List<DTOPriceDIGroupOfProductImport>();
                if (!string.IsNullOrEmpty(file))
                {
                    DTOPriceDIGroupOfProductData data = new DTOPriceDIGroupOfProductData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENPrice_DI_GroupProduct_Export(priceID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int col = 4, row = 1;
                                string levelCode = "", Input = "";
                                Dictionary<int, int> dictColLevel = new Dictionary<int, int>();
                                Dictionary<int, string> dictColLevelCode = new Dictionary<int, string>();
                                if (isFrame)
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListGOP.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Mã nhóm sản phẩm [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 2;
                                    }

                                    row = 3;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceDIGroupOfProductImport obj = new DTOPriceDIGroupOfProductImport();
                                        obj.ListDetail = new List<DTOPriceDIGroupOfProductExcel>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string priceMin = ExcelHelper.GetValue(worksheet, row, pCol);
                                                string priceMax = ExcelHelper.GetValue(worksheet, row, pCol + 1);


                                                if (!string.IsNullOrEmpty(priceMin) || !string.IsNullOrEmpty(priceMax))
                                                {
                                                    DTOPriceDIGroupOfProductExcel objDetail = new DTOPriceDIGroupOfProductExcel();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.ContractRoutingID = obj.RouteID;
                                                    objDetail.GroupOfProductID = pLevel;
                                                    objDetail.Price = 0;
                                                    if (string.IsNullOrEmpty(priceMin)) objDetail.PriceMin = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMin = Convert.ToDecimal(priceMin);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của loại [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    if (string.IsNullOrEmpty(priceMax)) objDetail.PriceMax = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMax = Convert.ToDecimal(priceMax);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của loại [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
                                }
                                else
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListGOP.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Mã nhóm hàng [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 1;
                                    }

                                    row = 2;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceDIGroupOfProductImport obj = new DTOPriceDIGroupOfProductImport();
                                        obj.ListDetail = new List<DTOPriceDIGroupOfProductExcel>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string price = ExcelHelper.GetValue(worksheet, row, pCol);


                                                if (!string.IsNullOrEmpty(price))
                                                {
                                                    DTOPriceDIGroupOfProductExcel objDetail = new DTOPriceDIGroupOfProductExcel();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.ContractRoutingID = obj.RouteID;
                                                    objDetail.GroupOfProductID = pLevel;
                                                    objDetail.PriceMax = null;
                                                    objDetail.PriceMin = null;
                                                    try
                                                    {
                                                        objDetail.Price = Convert.ToDecimal(price);
                                                    }
                                                    catch
                                                    {
                                                        objDetail.IsSuccess = false;
                                                        lstError.Add("Giá của loại[" + pLevelCode + "] không chính xác");
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
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

        [HttpPost]
        public void VENPrice_DI_GroupProduct_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceDIGroupOfProductImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDIGroupOfProductImport>>(dynParam.data.ToString());

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_GroupProduct_Import(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel VENPrice_DI_GroupProduct_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;

                var result = default(SYSExcel);

               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_GroupProduct_ExcelInit(isFrame, priceID, functionid, functionkey, isreload);
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
        public Row VENPrice_DI_GroupProduct_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                   ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_GroupProduct_ExcelChange(isFrame, priceID, id, row, cells, lstMessageError);
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
        public SYSExcel VENPrice_DI_GroupProduct_ExcelOnImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0)
                {
                   ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_GroupProduct_ExcelOnImport(isFrame, priceID, id, lst[0].Rows, lstMessageError);
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
        public bool VENPrice_DI_GroupProduct_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var result = false;
                if (id > 0)
                {
                   ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_GroupProduct_ExcelApprove(isFrame, priceID, id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        //ko xài
        #region Loading

        [HttpPost]
        public List<DTOPriceTruckDILoad> VENPrice_DI_Loading_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Loading_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_Loading_Location_NotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Loading_Location_NotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_Loading_Location_NotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceLocation> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceLocation>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Loading_Location_NotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_Loading_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Loading_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_Loading_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Loading_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_Loading_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Loading_DeleteList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //public string VENPrice_DI_Loading_Export(dynamic dynParam)
        //{
        //    try
        //    {
        //        int priceID = (int)dynParam.priceID;
        //        DTOPriceTruckDILoad_Export resBody = new DTOPriceTruckDILoad_Export();
        //        ServiceFactory.SVVendor((ISVVendor sv) =>
        //        {
        //            resBody = sv.VENPrice_DI_Loading_Export(priceID);
        //        });
        //        string file = "/" + FolderUpload.Export + "BangGiaBocXep_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

        //        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
        //            System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
        //        FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
        //        using (ExcelPackage pk = new ExcelPackage(f))
        //        {
        //            ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
        //            int col = 0, row = 1;

        //            #region Header
        //            col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
        //            worksheet.Cells[row, col, row + 1, col].Merge = true;
        //            col++; worksheet.Cells[row, col].Value = "Mã địa điểm"; worksheet.Column(col).Width = 15;
        //            worksheet.Cells[row, col, row + 1, col].Merge = true;
        //            col++; worksheet.Cells[row, col].Value = "Tên địa điểm"; worksheet.Column(col).Width = 25;
        //            worksheet.Cells[row, col, row + 1, col].Merge = true;

        //            Dictionary<int, int> dicGroup = new Dictionary<int, int>();
        //            foreach (CUSGroupOfProduct item in resBody.ListGroupProduct)
        //            {
        //                col++; worksheet.Cells[row, col].Value = item.Code;
        //                worksheet.Column(col).Width = 30;
        //                dicGroup.Add(item.ID, col);
        //                worksheet.Cells[row + 1, col].Value = "Đơn vị tính";
        //                worksheet.Column(col).Width = 15;
        //                col++; worksheet.Cells[row, col].Value = "";
        //                worksheet.Cells[row, col - 1, row, col].Merge = true;
        //                worksheet.Cells[row + 1, col].Value = "Giá";
        //                worksheet.Column(col).Width = 15;
        //            }
        //            row++;
        //            worksheet.Cells[1, 1, row, col].Style.Font.Bold = true;
        //            worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //            worksheet.Cells[1, 1, row, col].Style.Font.Color.SetColor(Color.White);
        //            worksheet.Cells[1, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //            worksheet.Cells[1, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

        //            worksheet.Cells[1, 1, row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            worksheet.Cells[1, 1, row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            worksheet.Cells[1, 1, row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;

        //            worksheet.Cells[1, 1, row, col].Style.Border.Top.Color.SetColor(Color.White);
        //            worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Color.SetColor(Color.White);
        //            worksheet.Cells[1, 1, row, col].Style.Border.Left.Color.SetColor(Color.White);
        //            worksheet.Cells[1, 1, row, col].Style.Border.Right.Color.SetColor(Color.White);

        //            #endregion

        //            #region Body
        //            int stt = 0;
        //            foreach (DTOPriceTruckDILoad item in resBody.ListDILoad)
        //            {
        //                row++; col = 0; stt++;
        //                col++; worksheet.Cells[row, col].Value = stt;
        //                col++; worksheet.Cells[row, col].Value = item.LocationCode;
        //                col++; worksheet.Cells[row, col].Value = item.LocationName;

        //                foreach (var dict in dicGroup)
        //                {
        //                    var objGroup = item.ListPriceTruckLoadingDetail.FirstOrDefault(c => c.GroupOfProductID == dict.Key);
        //                    if (objGroup != null)
        //                    {
        //                        var val = dict.Value;
        //                        col++; worksheet.Cells[row, val].Value = objGroup.PriceOfGOPCode;
        //                        col++; worksheet.Cells[row, val + 1].Value = objGroup.Price;
        //                    }
        //                }
        //            }
        //            #endregion

        //            pk.Save();
        //        }

        //        return file;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public List<DTOPriceTruckDILoad_Import> VENPrice_DI_Loading_Check(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string file = dynParam.file.ToString();
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (!string.IsNullOrEmpty(file))
                {
                    DTOVENPrice_Data data = new DTOVENPrice_Data();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_Data(contractTermID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicGop = new Dictionary<int, int>();

                                for (int col = 4; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGop != null)
                                        {
                                            if (!dicGop.ContainsKey(objGop.ID))
                                            {
                                                dicGop.Add(objGop.ID, col);
                                            }
                                            else
                                            {
                                                throw new Exception("Nhóm sản phẩm " + objGop.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Nhóm sản phẩm " + str + " không tồn tại.");
                                        }
                                        col++;
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    int col = 1; col++;

                                    DTOPriceTruckDILoad_Import obj = new DTOPriceTruckDILoad_Import();
                                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                                    obj.ExcelSuccess = true;
                                    obj.ExcelRow = row;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        obj.ExcelSuccess = false;
                                        obj.ExcelError = "Thiếu mã địa điểm.";
                                    }
                                    else
                                    {
                                        obj.LocationCode = str;
                                        var objLocation = data.ListLocation.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objLocation != null)
                                        {
                                            obj.LocationID = objLocation.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.LocationID == obj.LocationID);
                                            if (objCheck != null)
                                            {
                                                obj.ExcelSuccess = false;
                                                obj.ExcelError = "Trùng địa điểm.";
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = "Địa điểm không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.LocationName = str;

                                    var strErrorGop = "Sai đơn vị tính:";
                                    var strErrorPrice = "Sai giá:";
                                    var isErrorGop = false;
                                    var isErrorPrice = false;
                                    foreach (var dict in dicGop)
                                    {
                                        col = dict.Value;
                                        str = ExcelHelper.GetValue(worksheet, row, col);
                                        var objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objPriceGop != null)
                                        {
                                            DTOPriceTruckDILoadDetail o = new DTOPriceTruckDILoadDetail();
                                            obj.ListPriceTruckLoadingDetail.Add(o);
                                            o.GroupOfProductID = dict.Key;
                                            o.PriceOfGOPID = objPriceGop.ID;
                                            o.Price = 0;

                                            col++;
                                            str = ExcelHelper.GetValue(worksheet, row, col);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                try
                                                {
                                                    o.Price = Convert.ToDecimal(str);
                                                }
                                                catch
                                                {
                                                    obj.ExcelSuccess = false;
                                                    strErrorPrice += " [" + row + "-" + col + "]"; isErrorPrice = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            strErrorGop += " [" + row + "-" + col + "]"; isErrorGop = true;
                                        }
                                    }
                                    if (obj.ExcelSuccess == false)
                                    {
                                        if (isErrorGop)
                                            obj.ExcelError += strErrorGop;
                                        if (isErrorPrice)
                                            obj.ExcelError += strErrorPrice;
                                    }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_Loading_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.data.ToString());

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Loading_Import(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        //ko xài
        #region PriceEx
        [HttpPost]
        public List<DTOCATRouting> VENPrice_DI_PriceEx_RoutingParentList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOCATRouting>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceEx_RoutingParentList(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATRouting> VENPrice_DI_PriceEx_RoutingList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOCATRouting>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceEx_RoutingList(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_PriceEx_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceEx_List(priceID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_PriceEx_Detail(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int typeid = (int)dynParam.typeid;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceEx_Detail(typeid, priceID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_PriceEx_SaveList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOCATPriceDIEx> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATPriceDIEx>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceEx_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATPriceDIEx VENPrice_DI_PriceEx_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int id = (int)dynParam.id;
                int typeid = (int)dynParam.typeid;
                var result = new DTOCATPriceDIEx();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceEx_Get(id, typeid, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_PriceEx_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int typeid = (int)dynParam.typeid;
                DTOCATPriceDIEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPriceDIEx>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceEx_Save(item, typeid, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_PriceEx_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceEx_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PricePackingCO

        [HttpPost]
        public List<DTOPriceRouting> VENPrice_CO_COPackingPrice_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceRouting>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_COPackingPrice_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_CO_COPackingPrice_SaveList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceRouting>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_COPackingPrice_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string VENPrice_CO_COPackingPrice_Export(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                DTOPriceContainer_Export resBody = new DTOPriceContainer_Export();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    resBody = sv.VENPrice_CO_COPackingPrice_Export(priceID);
                });
                string file = "/Uploads/temp/" + "BangGiaFCL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(f))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 25;

                    foreach (CATPacking item in resBody.ListPacking)
                    {
                        col++; worksheet.Cells[row, col].Value = item.PackingName;
                        worksheet.Column(col).Width = 10;
                    }

                    worksheet.Cells[1, 1, row, col].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Color.SetColor(Color.White);

                    #endregion

                    #region Body
                    int stt = 0;
                    foreach (DTOPrice_Routing item in resBody.ListRouting)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = item.RoutingName;

                        foreach (CATPacking o in resBody.ListPacking)
                        {
                            col++;
                            var e = resBody.ListRoutingPrice.Where(c => c.RoutingID == item.RoutingID && o.ID == c.PackingID).FirstOrDefault();
                            if (e != null)
                            {
                                worksheet.Cells[row, col].Value = e.Price;
                            }
                        }
                    }
                    #endregion

                    pk.Save();
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOPrice_COPackingPrice_Import> VENPrice_CO_COPackingPrice_Check(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string file = dynParam.file.ToString();
                int contractID = (int)dynParam.contractID;
                List<DTOPrice_COPackingPrice_Import> sData = new List<DTOPrice_COPackingPrice_Import>();
                if (!string.IsNullOrEmpty(file))
                {
                    DTOVENPrice_Data data = new DTOVENPrice_Data();
                    List<CATPacking> resPacking = new List<CATPacking>();

                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_Data(contractID);
                    });

                    resPacking = data.ListPackingCO;

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicTU = new Dictionary<int, int>();

                                for (int col = 4; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objTU = resPacking.FirstOrDefault(c => c.PackingName == str);
                                        if (objTU != null)
                                        {
                                            if (!dicTU.ContainsValue(objTU.ID))
                                            {
                                                dicTU.Add(col, objTU.ID);
                                            }
                                            else
                                            {
                                                throw new Exception("Loại container " + objTU.PackingName + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Loại container " + str + " không tồn tại.");
                                        }
                                    }
                                }
                                for (row = 2; row <= totalRow; row++)
                                {
                                    int col = 1; col++;
                                    DTOPrice_COPackingPrice_Import obj = new DTOPrice_COPackingPrice_Import();
                                    obj.ListContainerPrice = new List<DTOCATPriceCOContainer>();

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                        obj.ExcelError = "Mã cung đường không được để trống.";
                                    else
                                    {
                                        obj.Code = str;
                                        var objRoute = data.ListRouting.FirstOrDefault(c => c.Code == str);
                                        if (objRoute != null)
                                        {
                                            obj.ID = objRoute.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.Code == str);
                                            if (objCheck != null)
                                                obj.ExcelError = "Trùng mã cung đường.";
                                        }
                                        else
                                        {
                                            obj.ExcelError = "Cung đường không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.RoutingName = str;

                                    for (; col < totalCol; )
                                    {
                                        col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                        DTOCATPriceCOContainer o = new DTOCATPriceCOContainer();
                                        o.PackingID = dicTU[col];
                                        o.PriceID = priceID;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            try
                                            {
                                                o.Price = Convert.ToDecimal(str);
                                            }
                                            catch
                                            {
                                                if (obj.ExcelError.IndexOf("Nhập giá sai.") < 0)
                                                    obj.ExcelError += "Nhập giá sai.";
                                            }
                                        }
                                        obj.ListContainerPrice.Add(o);
                                    }

                                    if (!string.IsNullOrEmpty(obj.ExcelError))
                                        obj.ExcelSuccess = false;
                                    else
                                        obj.ExcelSuccess = true;

                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_CO_COPackingPrice_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPrice_COPackingPrice_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPrice_COPackingPrice_Import>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_COPackingPrice_Import(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        //#region PriceServiceCO

        ////[HttpPost]
        ////public DTOResult VENPrice_CO_Service_List(dynamic dynParam)
        ////{
        ////    try
        ////    {
        ////        int priceID = (int)dynParam.priceID;
        ////        string request = dynParam.request.ToString();
        ////        var result = new DTOResult();
        ////        ServiceFactory.SVVendor((ISVVendor sv) =>
        ////        {
        ////            result = sv.VENPrice_CO_Service_List(request, priceID);
        ////        });
        ////        return result;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        ////[HttpPost]
        ////public int VENPrice_CO_Service_Save(dynamic dynParam)
        ////{
        ////    try
        ////    {
        ////        int priceID = (int)dynParam.priceID;
        ////        DTOCATPriceCOService item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPriceCOService>(dynParam.item.ToString());
        ////        ServiceFactory.SVVendor((ISVVendor sv) =>
        ////        {
        ////            item.ID = sv.VENPrice_CO_Service_Save(item, priceID);
        ////        });
        ////        return item.ID;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}

        //[HttpPost]
        //public void VENPrice_CO_Service_Delete(dynamic dynParam)
        //{
        //    try
        //    {
        //        int id = (int)dynParam.ID;
        //        ServiceFactory.SVVendor((ISVVendor sv) =>
        //        {
        //            sv.VENPrice_CO_Service_Delete(id);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //#endregion

        #region Price level
        [HttpPost]
        public List<DTOPriceDILevelGroupProduct> VENPrice_DI_PriceLevel_DetailData(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceDILevelGroupProduct> result = new List<DTOPriceDILevelGroupProduct>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceLevel_DetailData(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceLevel_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceDILevelGroupProduct> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILevelGroupProduct>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceLevel_Save(lst, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string VENPrice_DI_PriceLevel_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                DTOPriceDILevelData data = new DTOPriceDILevelData();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    data = sv.VENPrice_DI_PriceLevel_ExcelData(priceID, contractTermID);
                });

                string result = "/" + FolderUpload.Export + "Bảng giá bậc thang_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 1, row = 1, stt = 1;

                    #region header

                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row + 1, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    int colProduct = 0;
                    foreach (var level in data.ListLevel)
                    {
                        col++; worksheet.Cells[row, col].Value = level.Code;
                        colProduct = col;
                        foreach (var pro in data.ListGroupProduct)
                        {
                            worksheet.Cells[row + 1, colProduct].Value = pro.Code;
                            colProduct++;
                        }
                        ExcelHelper.CreateCellStyle(worksheet, row + 1, col, row + 1, colProduct - 1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, colProduct - 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        col = col + data.ListGroupProduct.Count - 1;
                    }

                    #endregion

                    #region data
                    col = 1;
                    row = 3;
                    foreach (var route in data.ListRoute)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = route.Code;
                        col++; worksheet.Cells[row, col].Value = route.RoutingName;
                        foreach (var level in data.ListLevel)
                        {
                            foreach (var pro in data.ListGroupProduct)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.RoutingID == route.ID && c.LevelID == level.ID && c.GroupProductID == pro.ID).FirstOrDefault();
                                if (check != null)
                                    worksheet.Cells[row, col].Value = check.Price;
                            }
                        }
                        row++;
                        stt++;
                    }
                    #endregion

                    package.Save();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOPriceDILevelImport> VENPrice_DI_PriceLevel_ExcelCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceDILevelImport> result = new List<DTOPriceDILevelImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOPriceDILevelData data = new DTOPriceDILevelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENPrice_DI_PriceLevel_ExcelData(priceID, contractTermID);
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 4, row = 1;
                            string levelCode = "", proCode = "", Input = "";
                            Dictionary<int, int> dictColLevel = new Dictionary<int, int>();
                            Dictionary<int, string> dictColLevelCode = new Dictionary<int, string>();
                            Dictionary<int, int> dictColPro = new Dictionary<int, int>();
                            Dictionary<int, string> dictColProCode = new Dictionary<int, string>();
                            if (worksheet != null)
                            {
                                int preLevelID = 0;
                                string preLevelCode = "";
                                while (col <= worksheet.Dimension.End.Column)
                                {
                                    levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                    if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                    if (!string.IsNullOrEmpty(levelCode))
                                    {
                                        var checkLeveCode = data.ListLevel.FirstOrDefault(c => c.Code == levelCode);
                                        if (checkLeveCode == null) throw new Exception("Bậc giá [" + levelCode + "] không tồn tại");
                                        else { preLevelID = checkLeveCode.ID; preLevelCode = checkLeveCode.Code; }
                                    }

                                    proCode = ExcelHelper.GetValue(worksheet, row + 1, col);
                                    var checkProCode = data.ListGroupProduct.FirstOrDefault(c => c.Code == proCode);
                                    if (checkProCode == null) throw new Exception("Mã sản phẩm [" + proCode + "] không tồn tại");
                                    else
                                    {
                                        dictColLevel.Add(col, preLevelID);
                                        dictColLevelCode.Add(col, preLevelCode);
                                        dictColPro.Add(col, checkProCode.ID);
                                        dictColProCode.Add(col, checkProCode.Code);
                                    }
                                    col++;
                                }

                                row = 3;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();
                                    DTOPriceDILevelImport obj = new DTOPriceDILevelImport();
                                    obj.ListDetail = new List<DTOPriceDILevelGroupProductExcel>();
                                    obj.ExcelRow = row;
                                    obj.ExcelSuccess = true;
                                    obj.ExcelError = string.Empty;
                                    col = 2;

                                    Input = ExcelHelper.GetValue(worksheet, row, col);
                                    var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
                                    if (checkRoute == null)
                                    {
                                        lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                        obj.ExcelSuccess = false;
                                    }
                                    else
                                    {
                                        obj.RouteID = checkRoute.ID;
                                        obj.RouteCode = checkRoute.Code;
                                        obj.RouteName = checkRoute.RoutingName;
                                    }

                                    if (dictColPro.Keys.Count > 0)
                                    {
                                        foreach (var word in dictColPro)
                                        {
                                            int pCol = word.Key;
                                            int proID = word.Value;
                                            int pLevel = 0;
                                            string pProCode = "", pLevelCode = "";

                                            dictColLevel.TryGetValue(pCol, out  pLevel);
                                            dictColLevelCode.TryGetValue(pCol, out  pLevelCode);
                                            dictColProCode.TryGetValue(pCol, out  pProCode);

                                            Input = ExcelHelper.GetValue(worksheet, row, pCol);

                                            if (!string.IsNullOrEmpty(Input))
                                            {
                                                DTOPriceDILevelGroupProductExcel objDetail = new DTOPriceDILevelGroupProductExcel();
                                                objDetail.IsSuccess = true;
                                                objDetail.RouteID = obj.RouteID;
                                                objDetail.LevelID = pLevel;
                                                objDetail.GroupProductID = proID;
                                                try
                                                {
                                                    objDetail.Price = Convert.ToDecimal(Input);
                                                }
                                                catch
                                                {
                                                    objDetail.IsSuccess = false;
                                                    lstError.Add("Giá của bậc [" + pLevelCode + "]-sản phẩm [" + pProCode + "] không chính xác");
                                                }
                                                obj.ListDetail.Add(objDetail);
                                            }
                                        }
                                    }

                                    if (lstError.Count > 0)
                                        obj.ExcelError = string.Join(" ,", lstError);
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
        [HttpPost]
        public void VENPrice_DI_PriceLevel_ExcelImport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceDILevelImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILevelImport>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceLevel_ExcelImport(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel VENPrice_DI_PriceLevel_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceLevel_ExcelInit(priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row VENPrice_DI_PriceLevel_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_PriceLevel_ExcelChange(priceID, contractTermID, id, row, cells, lstMessageError);
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
        public SYSExcel VENPrice_DI_PriceLevel_OnExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_PriceLevel_OnExcelImport(priceID, contractTermID, id, lst[0].Rows, lstMessageError);
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
        public bool VENPrice_DI_PriceLevel_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;
                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_DI_PriceLevel_ExcelApprove(priceID, contractTermID, id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GroupOfProduct
        [HttpPost]
        public DTOResult VENContract_GroupOfProduct_List(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_GroupOfProduct_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATContractGroupOfProduct VENContract_GroupOfProduct_Get(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                int id = (int)dynParam.id;
                DTOCATContractGroupOfProduct result = new DTOCATContractGroupOfProduct();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_GroupOfProduct_Get(id, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_GroupOfProduct_Save(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                DTOCATContractGroupOfProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractGroupOfProduct>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_GroupOfProduct_Save(item, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_GroupOfProduct_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_GroupOfProduct_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public double? VENContract_GroupOfProduct_Check(dynamic dynParam)
        {
            try
            {
                double? result = null;
                DTOCATContractGroupOfProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractGroupOfProduct>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_GroupOfProduct_Check(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public SYSExcel VENContract_GroupOfProduct_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int contractID = (int)dynParam.contractID;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_GroupOfProduct_ExcelInit(contractID, functionid, functionkey, isreload);
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
        public Row VENContract_GroupOfProduct_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int contractID = (int)dynParam.contractID;

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENContract_GroupOfProduct_ExcelChange(contractID, id, row, cells, lstMessageError);
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
        public SYSExcel VENContract_GroupOfProduct_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int contractID = (int)dynParam.contractID;

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENContract_GroupOfProduct_ExcelImport(contractID, id, lst[0].Rows, lstMessageError);
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
        public bool VENContract_GroupOfProduct_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;
                int contractID = (int)dynParam.contractID;

                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENContract_GroupOfProduct_ExcelApprove(contractID, id);
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

        #region Loading new

        #region common
        [HttpPost]
        public void VENPrice_DI_Load_Delete(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Load_Delete(ID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Load_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Load_DeleteList(data);
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_LoadLocation_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadLocation_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_LoadLocation_LocationNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadLocation_LocationNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadLocation_LocationNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadLocation_LocationNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadLocation_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadLocation_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_LoadLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadLocation_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VENPrice_DI_LoadLocation_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadLocation_Export(contractTermID, priceID);
                    data = sv.VENContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportVENPrice_LoadLocation_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
                    col++; worksheet.Cells[row, col].Value = "Mã địa điểm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa điểm"; worksheet.Column(col).Width = 30;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 30;

                    for (int i = 1; i < 5; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, row, i, 2, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }

                    foreach (var itemGroup in result.ListGroupProduct)
                    {
                        col++; worksheet.Cells[row, col].Value = itemGroup.Code; worksheet.Column(col).Width = 40;
                        worksheet.Cells[row + 1, col].Value = "Đơn vị tính"; worksheet.Column(col).Width = 20;
                        worksheet.Cells[row + 1, col + 1].Value = "Giá"; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        ExcelHelper.CreateCellStyle(worksheet, row + 1, col, row + 1, col + 1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        col++;
                    }
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 3;
                    if (result.ListData.Count > 0)
                    {
                        foreach (var itemData in result.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = itemData.LocationCode;
                            col++; worksheet.Cells[row, col].Value = itemData.LocationName;
                            col++; worksheet.Cells[row, col].Value = itemData.Address;
                            foreach (var itemGroup in result.ListGroupProduct)
                            {
                                var flag = 0;
                                foreach (var itemTruck in itemData.ListPriceTruckLoadingDetail)
                                {
                                    if (itemGroup.ID == itemTruck.GroupOfProductID)
                                    {
                                        flag = 1;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.PriceOfGOPCode;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.Price;
                                    }
                                }
                                if (flag == 0)
                                {
                                    col++; worksheet.Cells[row, col].Value = "";
                                    col++; worksheet.Cells[row, col].Value = 0;
                                }
                            }
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

                    #region location
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Danh sách địa điểm");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet1.Cells[row, col].Value = "STT";
                    col++; worksheet1.Cells[row, col].Value = "Mã địa điểm"; worksheet1.Column(col).Width = 20;
                    col++; worksheet1.Cells[row, col].Value = "Địa điểm"; worksheet1.Column(col).Width = 30;
                    col++; worksheet1.Cells[row, col].Value = "Địa chỉ"; worksheet1.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet1, 1, 1, 1, 4, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet1.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListLocation != null && data.ListLocation.Count > 0)
                    {
                        foreach (var itemData in data.ListLocation)
                        {
                            col = 1;
                            worksheet1.Cells[row, col].Value = stt;
                            col++; worksheet1.Cells[row, col].Value = itemData.CATLocationCode;
                            col++; worksheet1.Cells[row, col].Value = itemData.LocationName;
                            col++; worksheet1.Cells[row, col].Value = itemData.Address;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet1.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet1.Dimension.End.Column; j++)
                        {
                            worksheet1.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

                    #region priceOfGOP
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Danh sách đơn vị tính");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet2.Cells[row, col].Value = "STT";
                    col++; worksheet2.Cells[row, col].Value = "Mã đơn vị"; worksheet2.Column(col).Width = 20;
                    col++; worksheet2.Cells[row, col].Value = "Đơn vị"; worksheet2.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet2, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet2.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListPriceOfGOP != null && data.ListPriceOfGOP.Count > 0)
                    {
                        foreach (var itemData in data.ListPriceOfGOP)
                        {
                            col = 1;
                            worksheet2.Cells[row, col].Value = stt;
                            col++; worksheet2.Cells[row, col].Value = itemData.Code;
                            col++; worksheet2.Cells[row, col].Value = itemData.ValueOfVar;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet2.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet2.Dimension.End.Column; j++)
                        {
                            worksheet2.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion
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
        public List<DTOPriceTruckDILoad_Import> VENPrice_DI_LoadLocation_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_ExcelData(contractTermID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicGop = new Dictionary<int, int>();

                                for (int col = 5; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGop != null)
                                        {
                                            if (!dicGop.ContainsKey(objGop.ID))
                                            {
                                                dicGop.Add(objGop.ID, col);
                                            }
                                            else
                                            {
                                                throw new Exception("Nhóm sản phẩm " + objGop.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Nhóm sản phẩm " + str + " không tồn tại.");
                                        }
                                        col++;
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    int col = 1; col++;

                                    DTOPriceTruckDILoad_Import obj = new DTOPriceTruckDILoad_Import();
                                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                                    obj.ExcelSuccess = true;
                                    obj.ExcelRow = row;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        obj.ExcelSuccess = false;
                                        obj.ExcelError = "Thiếu mã địa điểm.";
                                    }
                                    else
                                    {
                                        obj.LocationCode = str;
                                        var objLocation = data.ListLocation.FirstOrDefault(c => c.CATLocationCode.Trim().ToLower() == str.Trim().ToLower());
                                        if (objLocation != null)
                                        {
                                            obj.LocationID = objLocation.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.LocationID == obj.LocationID);
                                            if (objCheck != null)
                                            {
                                                obj.ExcelSuccess = false;
                                                obj.ExcelError = "Trùng địa điểm.";
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = "Địa điểm không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.LocationName = str;

                                    var strErrorGop = "Sai đơn vị tính:";
                                    var strErrorPrice = "Sai giá:";
                                    var isErrorGop = false;
                                    var isErrorPrice = false;
                                    foreach (var dict in dicGop)
                                    {
                                        col = dict.Value;
                                        str = ExcelHelper.GetValue(worksheet, row, col);
                                        SYSVar objPriceGop = null;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        }
                                        else
                                        {
                                            //trống
                                            var priceOfGOPID = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == dict.Key).PriceOfGOPID;
                                            if (priceOfGOPID != null)
                                                objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.ID == priceOfGOPID);
                                        }
                                        if (objPriceGop != null)
                                        {
                                            DTOPriceTruckDILoadDetail o = new DTOPriceTruckDILoadDetail();
                                            obj.ListPriceTruckLoadingDetail.Add(o);
                                            o.GroupOfProductID = dict.Key;
                                            o.PriceOfGOPID = objPriceGop.ID;
                                            o.Price = 0;

                                            col++;
                                            str = ExcelHelper.GetValue(worksheet, row, col);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                try
                                                {
                                                    o.Price = Convert.ToDecimal(str);
                                                }
                                                catch
                                                {
                                                    obj.ExcelSuccess = false;
                                                    strErrorPrice += " [" + row + "-" + col + "]"; isErrorPrice = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            strErrorGop += " [" + row + "-" + col + "]"; isErrorGop = true;
                                        }
                                    }
                                    if (obj.ExcelSuccess == false)
                                    {
                                        if (isErrorGop)
                                            obj.ExcelError += strErrorGop;
                                        if (isErrorPrice)
                                            obj.ExcelError += strErrorPrice;
                                    }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadLocation_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadLocation_Import(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region route
        [HttpPost]
        public List<DTOPriceTruckDILoad> VENPrice_DI_LoadRoute_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadRoute_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_LoadRoute_RouteNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadRoute_RouteNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadRoute_RouteNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadRoute_RouteNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadRoute_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadRoute_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_LoadRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadRoute_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VENPrice_DI_LoadRoute_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadRoute_Export(contractTermID, priceID);
                    data = sv.VENContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportVENPrice_LoadRoute_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Cung đường"; worksheet.Column(col).Width = 30;

                    for (int i = 1; i < 4; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, row, i, 2, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }

                    foreach (var itemGroup in result.ListGroupProduct)
                    {
                        col++; worksheet.Cells[row, col].Value = itemGroup.Code; worksheet.Column(col).Width = 40;
                        worksheet.Cells[row + 1, col].Value = "Đơn vị tính"; worksheet.Column(col).Width = 20;
                        worksheet.Cells[row + 1, col + 1].Value = "Giá"; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        ExcelHelper.CreateCellStyle(worksheet, row + 1, col, row + 1, col + 1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        col++;
                    }
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 3;
                    if (result.ListData.Count > 0)
                    {
                        foreach (var itemData in result.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = itemData.RoutingCode;
                            col++; worksheet.Cells[row, col].Value = itemData.RoutingName;
                            foreach (var itemGroup in result.ListGroupProduct)
                            {
                                var flag = 0;
                                foreach (var itemTruck in itemData.ListPriceTruckLoadingDetail)
                                {
                                    if (itemGroup.ID == itemTruck.GroupOfProductID)
                                    {
                                        flag = 1;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.PriceOfGOPCode;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.Price;
                                    }
                                }
                                if (flag == 0)
                                {
                                    col++; worksheet.Cells[row, col].Value = "";
                                    col++; worksheet.Cells[row, col].Value = 0;
                                }
                            }
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

                    #region route
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Danh sách cung đường");

                    col = 1; row = 1; stt = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet1.Cells[row, col].Value = "Mã cung đường"; worksheet1.Column(col).Width = 20;
                    col++; worksheet1.Cells[row, col].Value = "Cung đường"; worksheet1.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet1, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet1.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListRouting != null && data.ListRouting.Count > 0)
                    {
                        foreach (var itemData in data.ListRouting)
                        {
                            col = 1;
                            worksheet1.Cells[row, col].Value = stt;
                            col++; worksheet1.Cells[row, col].Value = itemData.CATCode;
                            col++; worksheet1.Cells[row, col].Value = itemData.RoutingName;
                            row++;
                            stt++;
                        }
                    }
                    #endregion
                    for (int i = 1; i <= worksheet1.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet1.Dimension.End.Column; j++)
                        {
                            worksheet1.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

                    #region priceOfGOP
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Danh sách đơn vị tính");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet2.Cells[row, col].Value = "STT";
                    col++; worksheet2.Cells[row, col].Value = "Mã đơn vị"; worksheet2.Column(col).Width = 20;
                    col++; worksheet2.Cells[row, col].Value = "Đơn vị"; worksheet2.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet2, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet2.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListPriceOfGOP != null && data.ListPriceOfGOP.Count > 0)
                    {
                        foreach (var itemData in data.ListPriceOfGOP)
                        {
                            col = 1;
                            worksheet2.Cells[row, col].Value = stt;
                            col++; worksheet2.Cells[row, col].Value = itemData.Code;
                            col++; worksheet2.Cells[row, col].Value = itemData.ValueOfVar;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet2.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet2.Dimension.End.Column; j++)
                        {
                            worksheet2.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion
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
        public List<DTOPriceTruckDILoad_Import> VENPrice_DI_LoadRoute_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_ExcelData(contractTermID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicGop = new Dictionary<int, int>();

                                for (int col = 4; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGop != null)
                                        {
                                            if (!dicGop.ContainsKey(objGop.ID))
                                            {
                                                dicGop.Add(objGop.ID, col);
                                            }
                                            else
                                            {
                                                throw new Exception("Nhóm sản phẩm " + objGop.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Nhóm sản phẩm " + str + " không tồn tại.");
                                        }
                                        col++;
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    int col = 1; col++;

                                    DTOPriceTruckDILoad_Import obj = new DTOPriceTruckDILoad_Import();
                                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                                    obj.ExcelSuccess = true;
                                    obj.ExcelRow = row;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        obj.ExcelSuccess = false;
                                        obj.ExcelError = "Thiếu mã cung đường.";
                                    }
                                    else
                                    {
                                        obj.RoutingCode = str;
                                        var objRoute = data.ListRouting.FirstOrDefault(c => c.CATCode.Trim().ToLower() == str.Trim().ToLower());
                                        if (objRoute != null)
                                        {
                                            obj.RoutingID = objRoute.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.RoutingID == obj.RoutingID);
                                            if (objCheck != null)
                                            {
                                                obj.ExcelSuccess = false;
                                                obj.ExcelError = "Trùng cung đường.";
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = "Cung đường không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.RoutingName = str;

                                    var strErrorGop = "Sai đơn vị tính:";
                                    var strErrorPrice = "Sai giá:";
                                    var isErrorGop = false;
                                    var isErrorPrice = false;
                                    foreach (var dict in dicGop)
                                    {
                                        col = dict.Value;
                                        str = ExcelHelper.GetValue(worksheet, row, col);
                                        SYSVar objPriceGop = null;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        }
                                        else
                                        {
                                            //trống
                                            var priceOfGOPID = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == dict.Key).PriceOfGOPID;
                                            if (priceOfGOPID != null)
                                                objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.ID == priceOfGOPID);
                                        }
                                        if (objPriceGop != null)
                                        {
                                            DTOPriceTruckDILoadDetail o = new DTOPriceTruckDILoadDetail();
                                            obj.ListPriceTruckLoadingDetail.Add(o);
                                            o.GroupOfProductID = dict.Key;
                                            o.PriceOfGOPID = objPriceGop.ID;
                                            o.Price = 0;

                                            col++;
                                            str = ExcelHelper.GetValue(worksheet, row, col);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                try
                                                {
                                                    o.Price = Convert.ToDecimal(str);
                                                }
                                                catch
                                                {
                                                    obj.ExcelSuccess = false;
                                                    strErrorPrice += " [" + row + "-" + col + "]"; isErrorPrice = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            strErrorGop += " [" + row + "-" + col + "]"; isErrorGop = true;
                                        }
                                    }
                                    if (obj.ExcelSuccess == false)
                                    {
                                        if (isErrorGop)
                                            obj.ExcelError += strErrorGop;
                                        if (isErrorPrice)
                                            obj.ExcelError += strErrorPrice;
                                    }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadRoute_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadRoute_Import(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TypeOfpartner
        [HttpPost]
        public List<DTOPriceTruckDILoad> VENPrice_DI_LoadPartner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadPartner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_LoadPartner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadPartner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadPartner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadPartner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadPartner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadPartner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_LoadPartner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadPartner_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VENPrice_DI_LoadPartner_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadPartner_Export(contractTermID, priceID);
                    data = sv.VENContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportVENPrice_LoadPartner_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
                    col++; worksheet.Cells[row, col].Value = "Mã loại địa điểm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên loại địa điểm"; worksheet.Column(col).Width = 30;

                    for (int i = 1; i < 4; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, row, i, 2, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }

                    foreach (var itemGroup in result.ListGroupProduct)
                    {
                        col++; worksheet.Cells[row, col].Value = itemGroup.Code; worksheet.Column(col).Width = 40;
                        worksheet.Cells[row + 1, col].Value = "Đơn vị tính"; worksheet.Column(col).Width = 20;
                        worksheet.Cells[row + 1, col + 1].Value = "Giá"; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        ExcelHelper.CreateCellStyle(worksheet, row + 1, col, row + 1, col + 1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        col++;
                    }
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 3;
                    if (result.ListData.Count > 0)
                    {
                        foreach (var itemData in result.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = itemData.GroupOfLocationCode;
                            col++; worksheet.Cells[row, col].Value = itemData.GroupOfLocationName;
                            foreach (var itemGroup in result.ListGroupProduct)
                            {
                                var flag = 0;
                                foreach (var itemTruck in itemData.ListPriceTruckLoadingDetail)
                                {
                                    if (itemGroup.ID == itemTruck.GroupOfProductID)
                                    {
                                        flag = 1;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.PriceOfGOPCode;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.Price;
                                    }
                                }
                                if (flag == 0)
                                {
                                    col++; worksheet.Cells[row, col].Value = "";
                                    col++; worksheet.Cells[row, col].Value = 0;
                                }
                            }
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
                    #region partner
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Danh sách loại địa điểm");

                    col = 1; row = 1; stt = 1;
                    #region header
                    worksheet1.Cells[row, col].Value = "STT";
                    col++; worksheet1.Cells[row, col].Value = "Mã loại địa điểm"; worksheet1.Column(col).Width = 20;
                    col++; worksheet1.Cells[row, col].Value = "Tên loại địa điểm"; worksheet1.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet1, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet1.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListGroupOfLocation != null && data.ListGroupOfLocation.Count > 0)
                    {
                        foreach (var itemData in data.ListGroupOfLocation)
                        {
                            col = 1;
                            worksheet1.Cells[row, col].Value = stt;
                            col++; worksheet1.Cells[row, col].Value = itemData.Code;
                            col++; worksheet1.Cells[row, col].Value = itemData.GroupName;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet1.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet1.Dimension.End.Column; j++)
                        {
                            worksheet1.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

                    #region priceOfGOP
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Danh sách đơn vị tính");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet2.Cells[row, col].Value = "STT";
                    col++; worksheet2.Cells[row, col].Value = "Mã đơn vị"; worksheet2.Column(col).Width = 20;
                    col++; worksheet2.Cells[row, col].Value = "Đơn vị"; worksheet2.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet2, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet2.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListPriceOfGOP != null && data.ListPriceOfGOP.Count > 0)
                    {
                        foreach (var itemData in data.ListPriceOfGOP)
                        {
                            col = 1;
                            worksheet2.Cells[row, col].Value = stt;
                            col++; worksheet2.Cells[row, col].Value = itemData.Code;
                            col++; worksheet2.Cells[row, col].Value = itemData.ValueOfVar;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet2.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet2.Dimension.End.Column; j++)
                        {
                            worksheet2.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

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
        public List<DTOPriceTruckDILoad_Import> VENPrice_DI_LoadPartner_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_ExcelData(contractTermID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicGop = new Dictionary<int, int>();

                                for (int col = 4; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGop != null)
                                        {
                                            if (!dicGop.ContainsKey(objGop.ID))
                                            {
                                                dicGop.Add(objGop.ID, col);
                                            }
                                            else
                                            {
                                                throw new Exception("Nhóm sản phẩm " + objGop.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Nhóm sản phẩm " + str + " không tồn tại.");
                                        }
                                        col++;
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    int col = 1; col++;

                                    DTOPriceTruckDILoad_Import obj = new DTOPriceTruckDILoad_Import();
                                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                                    obj.ExcelSuccess = true;
                                    obj.ExcelRow = row;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        obj.ExcelSuccess = false;
                                        obj.ExcelError = "Thiếu mã loại điểm.";
                                    }
                                    else
                                    {
                                        obj.GroupOfLocationCode = str;
                                        var objGOL = data.ListGroupOfLocation.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGOL != null)
                                        {
                                            obj.GroupOfLocationID = objGOL.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.GroupOfLocationID == obj.GroupOfLocationID);
                                            if (objCheck != null)
                                            {
                                                obj.ExcelSuccess = false;
                                                obj.ExcelError = "Trùng loại điểm.";
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = "Loại điểm không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.GroupOfLocationName = str;

                                    var strErrorGop = "Sai đơn vị tính:";
                                    var strErrorPrice = "Sai giá:";
                                    var isErrorGop = false;
                                    var isErrorPrice = false;
                                    foreach (var dict in dicGop)
                                    {
                                        col = dict.Value;
                                        str = ExcelHelper.GetValue(worksheet, row, col);
                                        SYSVar objPriceGop = null;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        }
                                        else
                                        {
                                            //trống
                                            var priceOfGOPID = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == dict.Key).PriceOfGOPID;
                                            if (priceOfGOPID != null)
                                                objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.ID == priceOfGOPID);
                                        }
                                        if (objPriceGop != null)
                                        {
                                            DTOPriceTruckDILoadDetail o = new DTOPriceTruckDILoadDetail();
                                            obj.ListPriceTruckLoadingDetail.Add(o);
                                            o.GroupOfProductID = dict.Key;
                                            o.PriceOfGOPID = objPriceGop.ID;
                                            o.Price = 0;

                                            col++;
                                            str = ExcelHelper.GetValue(worksheet, row, col);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                try
                                                {
                                                    o.Price = Convert.ToDecimal(str);
                                                }
                                                catch
                                                {
                                                    obj.ExcelSuccess = false;
                                                    strErrorPrice += " [" + row + "-" + col + "]"; isErrorPrice = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            strErrorGop += " [" + row + "-" + col + "]"; isErrorGop = true;
                                        }
                                    }
                                    if (obj.ExcelSuccess == false)
                                    {
                                        if (isErrorGop)
                                            obj.ExcelError += strErrorGop;
                                        if (isErrorPrice)
                                            obj.ExcelError += strErrorPrice;
                                    }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadPartner_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadPartner_Import(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region partner
        [HttpPost]
        public List<DTOPriceDILoadPartner> VENPrice_DI_LoadPartner_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceDILoadPartner>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadPartner_Partner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_LoadPartner_Partner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_LoadPartner_Partner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_LoadPartner_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceDILoadPartner> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILoadPartner>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadPartner_Partner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_LoadPartner_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_LoadPartner_Partner_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MOQ

        #region info
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLoad VENPrice_DI_PriceMOQLoad_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPriceDIMOQLoad);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int VENPrice_DI_PriceMOQLoad_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceDIMOQLoad item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoad>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Save(item, priceID);
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region group location
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupLocation_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_GroupLocation_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_GroupLocation_SaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GOP
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_GroupProduct_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_Save(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                DTOPriceDIMOQLoadGroupProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoadGroupProduct>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_GroupProduct_Save(item, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLoadGroupProduct VENPrice_DI_PriceMOQLoad_GroupProduct_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int cusID = (int)dynParam.cusID;
                DTOPriceDIMOQLoadGroupProduct result = new DTOPriceDIMOQLoadGroupProduct();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_GroupProduct_Get(id, cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupProduct_GOPList(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_GroupProduct_GOPList(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  location
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_Location_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Location_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Location_LocationNotInList(request, PriceMOQLoadID, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOPriceDIMOQLoadLocation VENPrice_DI_PriceMOQLoad_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceDIMOQLoadLocation result = new DTOPriceDIMOQLoadLocation();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceDIMOQLoadLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoadLocation>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Location_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  route
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_Route_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Route_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Route_SaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Route_RouteNotInList(request, PriceMOQLoadID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  parnet route
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_ParentRoute_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_ParentRoute_SaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(request, PriceMOQLoadID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  province
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_Province_List(dynamic dynParam)
        {
            try
            {
                int PriceDIMOQLoadID = (int)dynParam.PriceDIMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Province_List(request, PriceDIMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Province_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Province_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQLoad_Province_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceDIMOQLoadID = (int)dynParam.PriceDIMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQLoad_Province_SaveList(lst, PriceDIMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQLoad_Province_NotInList(dynamic dynParam)
        {
            try
            {
                int PriceDIMOQLoadID = (int)dynParam.PriceDIMOQLoadID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQLoad_Province_NotInList(request, PriceDIMOQLoadID, contractTermID, CustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region Area

        #endregion

        #endregion

        #region unload new

        #region common
        [HttpPost]
        public void VENPrice_DI_UnLoad_Delete(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoad_Delete(ID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_UnLoad_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoad_DeleteList(data);
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_UnLoadLocation_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadLocation_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_UnLoadLocation_LocationNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadLocation_LocationNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadLocation_LocationNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadLocation_LocationNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadLocation_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadLocation_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_UnLoadLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadLocation_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VENPrice_DI_UnLoadLocation_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadLocation_Export(contractTermID, priceID);
                    data = sv.VENContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportVENPrice_UnLoadLocation_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
                    col++; worksheet.Cells[row, col].Value = "Mã địa điểm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa điểm"; worksheet.Column(col).Width = 30;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 30;

                    for (int i = 1; i < 5; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, row, i, 2, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }

                    foreach (var itemGroup in result.ListGroupProduct)
                    {
                        col++; worksheet.Cells[row, col].Value = itemGroup.Code; worksheet.Column(col).Width = 40;
                        worksheet.Cells[row + 1, col].Value = "Đơn vị tính"; worksheet.Column(col).Width = 20;
                        worksheet.Cells[row + 1, col + 1].Value = "Giá"; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        ExcelHelper.CreateCellStyle(worksheet, row + 1, col, row + 1, col + 1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        col++;
                    }
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 3;
                    if (result.ListData.Count > 0)
                    {
                        foreach (var itemData in result.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = itemData.LocationCode;
                            col++; worksheet.Cells[row, col].Value = itemData.LocationName;
                            col++; worksheet.Cells[row, col].Value = itemData.Address;
                            foreach (var itemGroup in result.ListGroupProduct)
                            {
                                var flag = 0;
                                foreach (var itemTruck in itemData.ListPriceTruckLoadingDetail)
                                {
                                    if (itemGroup.ID == itemTruck.GroupOfProductID)
                                    {
                                        flag = 1;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.PriceOfGOPCode;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.Price;
                                    }
                                }
                                if (flag == 0)
                                {
                                    col++; worksheet.Cells[row, col].Value = "";
                                    col++; worksheet.Cells[row, col].Value = 0;
                                }
                            }
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

                    #region location
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Danh sách địa điểm");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet1.Cells[row, col].Value = "STT";
                    col++; worksheet1.Cells[row, col].Value = "Mã địa điểm"; worksheet1.Column(col).Width = 20;
                    col++; worksheet1.Cells[row, col].Value = "Địa điểm"; worksheet1.Column(col).Width = 30;
                    col++; worksheet1.Cells[row, col].Value = "Địa chỉ"; worksheet1.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet1, 1, 1, 1, 4, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet1.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListLocation != null && data.ListLocation.Count > 0)
                    {
                        foreach (var itemData in data.ListLocation)
                        {
                            col = 1;
                            worksheet1.Cells[row, col].Value = stt;
                            col++; worksheet1.Cells[row, col].Value = itemData.CATLocationCode;
                            col++; worksheet1.Cells[row, col].Value = itemData.LocationName;
                            col++; worksheet1.Cells[row, col].Value = itemData.Address;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet1.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet1.Dimension.End.Column; j++)
                        {
                            worksheet1.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

                    #region priceOfGOP
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Danh sách đơn vị tính");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet2.Cells[row, col].Value = "STT";
                    col++; worksheet2.Cells[row, col].Value = "Mã đơn vị"; worksheet2.Column(col).Width = 20;
                    col++; worksheet2.Cells[row, col].Value = "Đơn vị"; worksheet2.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet2, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet2.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListPriceOfGOP != null && data.ListPriceOfGOP.Count > 0)
                    {
                        foreach (var itemData in data.ListPriceOfGOP)
                        {
                            col = 1;
                            worksheet2.Cells[row, col].Value = stt;
                            col++; worksheet2.Cells[row, col].Value = itemData.Code;
                            col++; worksheet2.Cells[row, col].Value = itemData.ValueOfVar;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet2.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet2.Dimension.End.Column; j++)
                        {
                            worksheet2.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion
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
        public List<DTOPriceTruckDILoad_Import> VENPrice_DI_UnLoadLocation_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_ExcelData(contractTermID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicGop = new Dictionary<int, int>();

                                for (int col = 5; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGop != null)
                                        {
                                            if (!dicGop.ContainsKey(objGop.ID))
                                            {
                                                dicGop.Add(objGop.ID, col);
                                            }
                                            else
                                            {
                                                throw new Exception("Nhóm sản phẩm " + objGop.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Nhóm sản phẩm " + str + " không tồn tại.");
                                        }
                                        col++;
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    int col = 1; col++;

                                    DTOPriceTruckDILoad_Import obj = new DTOPriceTruckDILoad_Import();
                                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                                    obj.ExcelSuccess = true;
                                    obj.ExcelRow = row;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        obj.ExcelSuccess = false;
                                        obj.ExcelError = "Thiếu mã địa điểm.";
                                    }
                                    else
                                    {
                                        obj.LocationCode = str;
                                        var objLocation = data.ListLocation.FirstOrDefault(c => c.CATLocationCode.Trim().ToLower() == str.Trim().ToLower());
                                        if (objLocation != null)
                                        {
                                            obj.LocationID = objLocation.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.LocationID == obj.LocationID);
                                            if (objCheck != null)
                                            {
                                                obj.ExcelSuccess = false;
                                                obj.ExcelError = "Trùng địa điểm.";
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = "Địa điểm không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.LocationName = str;

                                    var strErrorGop = "Sai đơn vị tính:";
                                    var strErrorPrice = "Sai giá:";
                                    var isErrorGop = false;
                                    var isErrorPrice = false;
                                    foreach (var dict in dicGop)
                                    {
                                        col = dict.Value;
                                        str = ExcelHelper.GetValue(worksheet, row, col);
                                        SYSVar objPriceGop = null;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        }
                                        else
                                        {
                                            //trống
                                            var priceOfGOPID = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == dict.Key).PriceOfGOPID;
                                            if (priceOfGOPID != null)
                                                objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.ID == priceOfGOPID);
                                        }
                                        if (objPriceGop != null)
                                        {
                                            DTOPriceTruckDILoadDetail o = new DTOPriceTruckDILoadDetail();
                                            obj.ListPriceTruckLoadingDetail.Add(o);
                                            o.GroupOfProductID = dict.Key;
                                            o.PriceOfGOPID = objPriceGop.ID;
                                            o.Price = 0;

                                            col++;
                                            str = ExcelHelper.GetValue(worksheet, row, col);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                try
                                                {
                                                    o.Price = Convert.ToDecimal(str);
                                                }
                                                catch
                                                {
                                                    obj.ExcelSuccess = false;
                                                    strErrorPrice += " [" + row + "-" + col + "]"; isErrorPrice = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            strErrorGop += " [" + row + "-" + col + "]"; isErrorGop = true;
                                        }
                                    }
                                    if (obj.ExcelSuccess == false)
                                    {
                                        if (isErrorGop)
                                            obj.ExcelError += strErrorGop;
                                        if (isErrorPrice)
                                            obj.ExcelError += strErrorPrice;
                                    }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadLocation_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadLocation_Import(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region route
        [HttpPost]
        public List<DTOPriceTruckDILoad> VENPrice_DI_UnLoadRoute_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadRoute_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_UnLoadRoute_RouteNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadRoute_RouteNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadRoute_RouteNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadRoute_RouteNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadRoute_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadRoute_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_UnLoadRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadRoute_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string VENPrice_DI_UnLoadRoute_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadRoute_Export(contractTermID, priceID);
                    data = sv.VENContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportVENPrice_UnLoadRoute_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Cung đường"; worksheet.Column(col).Width = 30;

                    for (int i = 1; i < 4; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, row, i, 2, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }

                    foreach (var itemGroup in result.ListGroupProduct)
                    {
                        col++; worksheet.Cells[row, col].Value = itemGroup.Code; worksheet.Column(col).Width = 40;
                        worksheet.Cells[row + 1, col].Value = "Đơn vị tính"; worksheet.Column(col).Width = 20;
                        worksheet.Cells[row + 1, col + 1].Value = "Giá"; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        ExcelHelper.CreateCellStyle(worksheet, row + 1, col, row + 1, col + 1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        col++;
                    }
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 3;
                    if (result.ListData.Count > 0)
                    {
                        foreach (var itemData in result.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = itemData.RoutingCode;
                            col++; worksheet.Cells[row, col].Value = itemData.RoutingName;
                            foreach (var itemGroup in result.ListGroupProduct)
                            {
                                var flag = 0;
                                foreach (var itemTruck in itemData.ListPriceTruckLoadingDetail)
                                {
                                    if (itemGroup.ID == itemTruck.GroupOfProductID)
                                    {
                                        flag = 1;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.PriceOfGOPCode;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.Price;
                                    }
                                }
                                if (flag == 0)
                                {
                                    col++; worksheet.Cells[row, col].Value = "";
                                    col++; worksheet.Cells[row, col].Value = 0;
                                }
                            }
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

                    #region route
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Danh sách cung đường");

                    col = 1; row = 1; stt = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet1.Cells[row, col].Value = "Mã cung đường"; worksheet1.Column(col).Width = 20;
                    col++; worksheet1.Cells[row, col].Value = "Cung đường"; worksheet1.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet1, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet1.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListRouting != null && data.ListRouting.Count > 0)
                    {
                        foreach (var itemData in data.ListRouting)
                        {
                            col = 1;
                            worksheet1.Cells[row, col].Value = stt;
                            col++; worksheet1.Cells[row, col].Value = itemData.CATCode;
                            col++; worksheet1.Cells[row, col].Value = itemData.RoutingName;
                            row++;
                            stt++;
                        }
                    }
                    #endregion
                    for (int i = 1; i <= worksheet1.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet1.Dimension.End.Column; j++)
                        {
                            worksheet1.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

                    #region priceOfGOP
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Danh sách đơn vị tính");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet2.Cells[row, col].Value = "STT";
                    col++; worksheet2.Cells[row, col].Value = "Mã đơn vị"; worksheet2.Column(col).Width = 20;
                    col++; worksheet2.Cells[row, col].Value = "Đơn vị"; worksheet2.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet2, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet2.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListPriceOfGOP != null && data.ListPriceOfGOP.Count > 0)
                    {
                        foreach (var itemData in data.ListPriceOfGOP)
                        {
                            col = 1;
                            worksheet2.Cells[row, col].Value = stt;
                            col++; worksheet2.Cells[row, col].Value = itemData.Code;
                            col++; worksheet2.Cells[row, col].Value = itemData.ValueOfVar;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet2.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet2.Dimension.End.Column; j++)
                        {
                            worksheet2.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion
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
        public List<DTOPriceTruckDILoad_Import> VENPrice_DI_UnLoadRoute_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_ExcelData(contractTermID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicGop = new Dictionary<int, int>();

                                for (int col = 4; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGop != null)
                                        {
                                            if (!dicGop.ContainsKey(objGop.ID))
                                            {
                                                dicGop.Add(objGop.ID, col);
                                            }
                                            else
                                            {
                                                throw new Exception("Nhóm sản phẩm " + objGop.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Nhóm sản phẩm " + str + " không tồn tại.");
                                        }
                                        col++;
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    int col = 1; col++;

                                    DTOPriceTruckDILoad_Import obj = new DTOPriceTruckDILoad_Import();
                                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                                    obj.ExcelSuccess = true;
                                    obj.ExcelRow = row;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        obj.ExcelSuccess = false;
                                        obj.ExcelError = "Thiếu mã cung đường.";
                                    }
                                    else
                                    {
                                        obj.RoutingCode = str;
                                        var objRoute = data.ListRouting.FirstOrDefault(c => c.CATCode.Trim().ToLower() == str.Trim().ToLower());
                                        if (objRoute != null)
                                        {
                                            obj.RoutingID = objRoute.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.RoutingID == obj.RoutingID);
                                            if (objCheck != null)
                                            {
                                                obj.ExcelSuccess = false;
                                                obj.ExcelError = "Trùng cung đường.";
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = "Cung đường không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.RoutingName = str;

                                    var strErrorGop = "Sai đơn vị tính:";
                                    var strErrorPrice = "Sai giá:";
                                    var isErrorGop = false;
                                    var isErrorPrice = false;
                                    foreach (var dict in dicGop)
                                    {
                                        col = dict.Value;
                                        str = ExcelHelper.GetValue(worksheet, row, col);
                                        SYSVar objPriceGop = null;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        }
                                        else
                                        {
                                            //trống
                                            var priceOfGOPID = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == dict.Key).PriceOfGOPID;
                                            if (priceOfGOPID != null)
                                                objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.ID == priceOfGOPID);
                                        }
                                        if (objPriceGop != null)
                                        {
                                            DTOPriceTruckDILoadDetail o = new DTOPriceTruckDILoadDetail();
                                            obj.ListPriceTruckLoadingDetail.Add(o);
                                            o.GroupOfProductID = dict.Key;
                                            o.PriceOfGOPID = objPriceGop.ID;
                                            o.Price = 0;

                                            col++;
                                            str = ExcelHelper.GetValue(worksheet, row, col);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                try
                                                {
                                                    o.Price = Convert.ToDecimal(str);
                                                }
                                                catch
                                                {
                                                    obj.ExcelSuccess = false;
                                                    strErrorPrice += " [" + row + "-" + col + "]"; isErrorPrice = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            strErrorGop += " [" + row + "-" + col + "]"; isErrorGop = true;
                                        }
                                    }
                                    if (obj.ExcelSuccess == false)
                                    {
                                        if (isErrorGop)
                                            obj.ExcelError += strErrorGop;
                                        if (isErrorPrice)
                                            obj.ExcelError += strErrorPrice;
                                    }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadRoute_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadRoute_Import(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TypeOfpartner
        [HttpPost]
        public List<DTOPriceTruckDILoad> VENPrice_DI_UnLoadPartner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadPartner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_UnLoadPartner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadPartner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadPartner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadPartner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_UnLoadPartner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadPartner_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string VENPrice_DI_UnLoadPartner_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadPartner_Export(contractTermID, priceID);
                    data = sv.VENContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportVENPrice_UnloadPartner_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
                    col++; worksheet.Cells[row, col].Value = "Mã loại địa điểm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên loại địa điểm"; worksheet.Column(col).Width = 30;

                    for (int i = 1; i < 4; i++)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, row, i, 2, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    }

                    foreach (var itemGroup in result.ListGroupProduct)
                    {
                        col++; worksheet.Cells[row, col].Value = itemGroup.Code; worksheet.Column(col).Width = 40;
                        worksheet.Cells[row + 1, col].Value = "Đơn vị tính"; worksheet.Column(col).Width = 20;
                        worksheet.Cells[row + 1, col + 1].Value = "Giá"; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, col + 1, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        ExcelHelper.CreateCellStyle(worksheet, row + 1, col, row + 1, col + 1, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        col++;
                    }
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 3;
                    if (result.ListData.Count > 0)
                    {
                        foreach (var itemData in result.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = itemData.GroupOfLocationCode;
                            col++; worksheet.Cells[row, col].Value = itemData.GroupOfLocationName;
                            foreach (var itemGroup in result.ListGroupProduct)
                            {
                                var flag = 0;
                                foreach (var itemTruck in itemData.ListPriceTruckLoadingDetail)
                                {
                                    if (itemGroup.ID == itemTruck.GroupOfProductID)
                                    {
                                        flag = 1;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.PriceOfGOPCode;
                                        col++; worksheet.Cells[row, col].Value = itemTruck.Price;
                                    }
                                }
                                if (flag == 0)
                                {
                                    col++; worksheet.Cells[row, col].Value = "";
                                    col++; worksheet.Cells[row, col].Value = 0;
                                }
                            }
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

                    #region partner
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Danh sách loại địa điểm");

                    col = 1; row = 1; stt = 1;
                    #region header
                    worksheet1.Cells[row, col].Value = "STT";
                    col++; worksheet1.Cells[row, col].Value = "Mã loại địa điểm"; worksheet1.Column(col).Width = 20;
                    col++; worksheet1.Cells[row, col].Value = "Tên loại địa điểm"; worksheet1.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet1, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet1.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListGroupOfLocation != null && data.ListGroupOfLocation.Count > 0)
                    {
                        foreach (var itemData in data.ListGroupOfLocation)
                        {
                            col = 1;
                            worksheet1.Cells[row, col].Value = stt;
                            col++; worksheet1.Cells[row, col].Value = itemData.Code;
                            col++; worksheet1.Cells[row, col].Value = itemData.GroupName;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet1.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet1.Dimension.End.Column; j++)
                        {
                            worksheet1.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

                    #region priceOfGOP
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Danh sách đơn vị tính");

                    col = 1; row = 1; stt = 1;

                    #region header
                    worksheet2.Cells[row, col].Value = "STT";
                    col++; worksheet2.Cells[row, col].Value = "Mã đơn vị"; worksheet2.Column(col).Width = 20;
                    col++; worksheet2.Cells[row, col].Value = "Đơn vị"; worksheet2.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet2, 1, 1, 1, 3, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet2.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region body
                    col = 1;
                    row = 2;
                    if (data.ListPriceOfGOP != null && data.ListPriceOfGOP.Count > 0)
                    {
                        foreach (var itemData in data.ListPriceOfGOP)
                        {
                            col = 1;
                            worksheet2.Cells[row, col].Value = stt;
                            col++; worksheet2.Cells[row, col].Value = itemData.Code;
                            col++; worksheet2.Cells[row, col].Value = itemData.ValueOfVar;
                            row++;
                            stt++;
                        }
                    }
                    #endregion

                    for (int i = 1; i <= worksheet2.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet2.Dimension.End.Column; j++)
                        {
                            worksheet2.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion
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
        public List<DTOPriceTruckDILoad_Import> VENPrice_DI_UnLoadPartner_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOVENPrice_ExcelData data = new DTOVENPrice_ExcelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        data = sv.VENContract_Price_ExcelData(contractTermID);
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 1;

                                Dictionary<int, int> dicGop = new Dictionary<int, int>();

                                for (int col = 4; col <= totalCol; col++)
                                {
                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGop != null)
                                        {
                                            if (!dicGop.ContainsKey(objGop.ID))
                                            {
                                                dicGop.Add(objGop.ID, col);
                                            }
                                            else
                                            {
                                                throw new Exception("Nhóm sản phẩm " + objGop.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Nhóm sản phẩm " + str + " không tồn tại.");
                                        }
                                        col++;
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    int col = 1; col++;

                                    DTOPriceTruckDILoad_Import obj = new DTOPriceTruckDILoad_Import();
                                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                                    obj.ExcelSuccess = true;
                                    obj.ExcelRow = row;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        obj.ExcelSuccess = false;
                                        obj.ExcelError = "Thiếu mã loại điểm.";
                                    }
                                    else
                                    {
                                        obj.GroupOfLocationCode = str;
                                        var objGOL = data.ListGroupOfLocation.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        if (objGOL != null)
                                        {
                                            obj.GroupOfLocationID = objGOL.ID;
                                            var objCheck = sData.FirstOrDefault(c => c.GroupOfLocationID == obj.GroupOfLocationID);
                                            if (objCheck != null)
                                            {
                                                obj.ExcelSuccess = false;
                                                obj.ExcelError = "Trùng loại điểm.";
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = "Loại điểm không tồn tại.";
                                        }
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.GroupOfLocationName = str;

                                    var strErrorGop = "Sai đơn vị tính:";
                                    var strErrorPrice = "Sai giá:";
                                    var isErrorGop = false;
                                    var isErrorPrice = false;
                                    foreach (var dict in dicGop)
                                    {
                                        col = dict.Value;
                                        str = ExcelHelper.GetValue(worksheet, row, col);
                                        SYSVar objPriceGop = null;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.Code.Trim().ToLower() == str.Trim().ToLower());
                                        }
                                        else
                                        {
                                            //trống
                                            var priceOfGOPID = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == dict.Key).PriceOfGOPID;
                                            if (priceOfGOPID != null)
                                                objPriceGop = data.ListPriceOfGOP.FirstOrDefault(c => c.ID == priceOfGOPID);
                                        }
                                        if (objPriceGop != null)
                                        {
                                            DTOPriceTruckDILoadDetail o = new DTOPriceTruckDILoadDetail();
                                            obj.ListPriceTruckLoadingDetail.Add(o);
                                            o.GroupOfProductID = dict.Key;
                                            o.PriceOfGOPID = objPriceGop.ID;
                                            o.Price = 0;

                                            col++;
                                            str = ExcelHelper.GetValue(worksheet, row, col);
                                            try
                                            {
                                                o.Price = Convert.ToDecimal(str);
                                            }
                                            catch
                                            {
                                                obj.ExcelSuccess = false;
                                                strErrorPrice += " [" + row + "-" + col + "]"; isErrorPrice = true;
                                            }
                                        }
                                        else
                                        {
                                            obj.ExcelSuccess = false;
                                            strErrorGop += " [" + row + "-" + col + "]"; isErrorGop = true;
                                        }
                                    }
                                    if (obj.ExcelSuccess == false)
                                    {
                                        if (isErrorGop)
                                            obj.ExcelError += strErrorGop;
                                        if (isErrorPrice)
                                            obj.ExcelError += strErrorPrice;
                                    }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadPartner_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadPartner_Import(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region partner
        [HttpPost]
        public List<DTOPriceDILoadPartner> VENPrice_DI_UnLoadPartner_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceDILoadPartner>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadPartner_Partner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoadPartner_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceDILoadPartner> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILoadPartner>>(dynParam.data.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadPartner_Partner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_UnLoadPartner_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoadPartner_Partner_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MOQ
        #region info
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQUnLoad_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQUnLoad_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLoad VENPrice_DI_PriceMOQUnLoad_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPriceDIMOQLoad);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQUnLoad_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int VENPrice_DI_PriceMOQUnLoad_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceDIMOQLoad item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoad>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQUnLoad_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_PriceMOQUnLoad_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQUnLoad_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQUnLoad_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQUnLoad_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Area

        #endregion

        #endregion

        #region price Ex new

        #region info
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIEx VENPrice_DI_Ex_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOPriceDIEx);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int VENPrice_DI_Ex_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceDIEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIEx>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_Ex_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_Load_DeleteAllList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Load_DeleteAllList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_UnLoad_DeleteAllList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_UnLoad_DeleteAllList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region group location
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_GroupLocation_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_GroupLocation_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_GroupLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_GroupLocation_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_GroupLocation_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_GroupLocation_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_GroupLocation_GroupNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_GroupLocation_GroupNotInList(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GOP
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_GroupProduct_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_GroupProduct_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_GroupProduct_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_GroupProduct_Save(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                DTOPriceDIExGroupProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIExGroupProduct>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_GroupProduct_Save(item, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIExGroupProduct VENPrice_DI_Ex_GroupProduct_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int cusID = (int)dynParam.cusID;
                DTOPriceDIExGroupProduct result = new DTOPriceDIExGroupProduct();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_GroupProduct_Get(id, cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_GroupProduct_GOPList(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_GroupProduct_GOPList(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  location
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Location_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Location_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIExLocation VENPrice_DI_Ex_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceDIExLocation result = new DTOPriceDIExLocation();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceDIExLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIExLocation>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Location_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Location_LocationNotInSaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Location_LocationNotInList(request, priceExID, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  route
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Route_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Route_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Route_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Route_RouteNotInList(request, priceExID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  parnet route
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_ParentRoute_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_ParentRoute_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_ParentRoute_RouteNotInList(request, priceExID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  partner
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Partner_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Partner_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Partner_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Partner_PartnerNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Partner_PartnerNotInList(request, priceExID, contractTermID, CustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  province
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Province_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Province_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Province_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Province_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_Ex_Province_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_Ex_Province_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_Ex_Province_NotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_Ex_Province_NotInList(request, priceExID, contractTermID, CustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region moq new

        #region info
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATPriceDIMOQ VENPrice_DI_PriceMOQ_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOCATPriceDIMOQ);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int VENPrice_DI_PriceMOQ_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOCATPriceDIMOQ item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPriceDIMOQ>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Save(item, priceID);
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region group location
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_GroupLocation_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_GroupLocation_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_GroupLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_GroupLocation_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_GroupLocation_SaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_GroupLocation_SaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GOP
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_GroupProduct_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_GroupProduct_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_GroupProduct_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_GroupProduct_Save(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                DTOPriceDIMOQGroupProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQGroupProduct>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_GroupProduct_Save(item, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQGroupProduct VENPrice_DI_PriceMOQ_GroupProduct_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int cusID = (int)dynParam.cusID;
                DTOPriceDIMOQGroupProduct result = new DTOPriceDIMOQGroupProduct();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_GroupProduct_Get(id, cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_GroupProduct_GOPList(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_GroupProduct_GOPList(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  location
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Location_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Location_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLocation VENPrice_DI_PriceMOQ_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceDIMOQLocation result = new DTOPriceDIMOQLocation();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceDIMOQLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLocation>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Location_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Location_LocationNotInSaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Location_LocationNotInList(request, priceMOQID, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  route
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Route_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Route_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Route_SaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Route_RouteNotInList(request, priceMOQID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  parnet route
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_ParentRoute_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_ParentRoute_SaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(request, priceMOQID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  partner
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Partner_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Partner_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Partner_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Partner_PartnerNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Partner_PartnerNotInList(request, priceExID, contractTermID, CustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  province
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Province_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Province_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Province_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Province_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_DI_PriceMOQ_Province_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_DI_PriceMOQ_Province_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_DI_PriceMOQ_Province_NotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_DI_PriceMOQ_Province_NotInList(request, priceExID, contractTermID, CustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region thay doi bang gia the vat tu
        [HttpPost]
        public DTOCUSPrice_MaterialData VENContract_MaterialChange_Data(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                DTOCUSPrice_MaterialData result = new DTOCUSPrice_MaterialData();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_MaterialChange_Data(contractTermID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_MaterialChange_Save(dynamic dynParam)
        {
            try
            {
                int contractMaterialID = (int)dynParam.contractMaterialID;
                DTOCUSPrice_MaterialData item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPrice_MaterialData>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_MaterialChange_Save(item, contractMaterialID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region CustumerList
        [HttpPost]
        public DTOResult VENContract_ByCustomerList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int vendorID = (int)dynParam.vendorID;
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ByCustomerList(request, vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Price History
        [HttpPost]
        //public int PriceHistory_CheckPrice(dynamic dynParam)
        //{
        //    try
        //    {
        //        int transportModeID = (int)dynParam.transportModeID;
        //        List<int> lstVenId = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstVenId.ToString());
        //        DTOCUSPrice_HistoryData historyData = new DTOCUSPrice_HistoryData();
        //        int result = 0;
        //        int iFCL = -(int)SYSVarType.TransportModeFCL;
        //        if (transportModeID == iFCL)
        //        {
        //            return result = 1;
        //        }
        //        ServiceFactory.SVVendor((ISVVendor sv) =>
        //        {
        //            historyData = sv.PriceHistory_CheckPrice(lstVenId, transportModeID);
        //            if (historyData.hasLevel == true && historyData.hasNomal == true)
        //            {
        //                result = 2;
        //            }
        //            else if (historyData.hasLevel == true || historyData.hasNomal == true)
        //            {
        //                result = 1;
        //            }

        //        });
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string PriceHistory_Export(dynamic dynParam)
        //{
        //    try
        //    {
        //        int transportModeID = (int)dynParam.transportModeID;
        //        int typePrice = (int)dynParam.typePrice;
        //        int cusId = (int)dynParam.cusId;
        //        List<int> lstVenId = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstVenId.ToString());
        //        string result = "";
        //        if (lstVenId.Count == 1)
        //        {
        //            result = PriceHistory_ExportOneUser(cusId,lstVenId[0], transportModeID, typePrice);
        //        }
        //        else
        //        {
        //            result = PriceHistory_ExportMulUser(cusId,lstVenId, transportModeID, typePrice);
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private string PriceHistory_ExportOneUser(int cusId,int venId, int transportModeID, int typePrice)
        //{
        //    try
        //    {
        //        DTOCUSPrice_HistoryData result = new DTOCUSPrice_HistoryData();
        //        ServiceFactory.SVVendor((ISVVendor sv) =>
        //        {
        //            result = sv.PriceHistory_GetDataOneUser(cusId, venId, transportModeID, typePrice);
        //        });

        //        string[] colorCol = new string[] { ExcelHelper.ColorGreen, ExcelHelper.ColorBlue, ExcelHelper.ColorOrange };

        //        int iFCL = -(int)SYSVarType.TransportModeFCL;
        //        int iFTL = -(int)SYSVarType.TransportModeFTL;
        //        int iLTL = -(int)SYSVarType.TransportModeLTL;

        //        string file = "/" + FolderUpload.Export + "ExportPriceHistoryVendor_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

        //        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
        //            System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
        //        FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
        //        using (ExcelPackage package = new ExcelPackage(exportfile))
        //        {
        //            // Sheet 1
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

        //            int col = 1, row = 1;
        //            if (result.ListPriceOfCustomer != null)
        //            {
        //                foreach (var item in result.ListPriceOfCustomer)
        //                {
        //                    row = 1;
        //                    #region cung đường
        //                    int colRte = col;
        //                    row = 3;
        //                    worksheet.Cells[row, colRte].Value = "Mã CĐ Hệ thống";
        //                    colRte++; worksheet.Cells[row, colRte].Value = "Tên CĐ Hệ thống";
        //                    colRte++; worksheet.Cells[row - 2, colRte].Value = item.CustomerName;
        //                    worksheet.Cells[row, colRte].Value = "Mã cung đường";
        //                    colRte++; worksheet.Cells[row, colRte].Value = "Tên cung đường";
        //                    ExcelHelper.CreateCellStyle(worksheet, row - 2, colRte - 1, row - 1, colRte, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
        //                    ExcelHelper.CreateCellStyle(worksheet, row, col, row, colRte, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
        //                    worksheet.Cells[row - 2, col, row, colRte].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    worksheet.Cells[row - 2, col, row, colRte].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                    row = 4;
        //                    colRte = col;
        //                    if (item.ListRouting != null)
        //                    {
        //                        foreach (var route in item.ListRouting)
        //                        {
        //                            colRte = col;
        //                            worksheet.Cells[row, colRte].Value = route.CATCode; worksheet.Column(colRte).Width = 20;
        //                            colRte++; worksheet.Cells[row, colRte].Value = route.CATName; worksheet.Column(colRte).Width = 20;
        //                            colRte++; worksheet.Cells[row, colRte].Value = route.Code; worksheet.Column(colRte).Width = 20;
        //                            colRte++; worksheet.Cells[row, colRte].Value = route.RoutingName; worksheet.Column(colRte).Width = 20;
        //                            row++;
        //                        }
        //                    }
        //                    #endregion

        //                    #region bảng giá
        //                    col = colRte;
        //                    int colPrice = colRte + 1;
        //                    int index = 0;
        //                    foreach (var price in result.ListPrice)
        //                    {
        //                        var abc = index % 3;
        //                        row = 1;
        //                        int colR = col;
        //                        colPrice = col;
        //                        if ((price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1)) || (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2)) || (transportModeID == iFCL && price.FCLData.ListPacking.Count > 0))
        //                        {
        //                            colPrice++; worksheet.Cells[row, colPrice].Value = price.ItemPrice.Code + " - " + price.ItemPrice.EffectDate.ToString("dd/MM/yyyy"); 
        //                        }
        //                        row = 3;
        //                        #region FTL
        //                        if (transportModeID == iFTL)
        //                        {
        //                            #region FTL Normal
        //                            if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                            {
        //                                if (price.ItemPrice.TypeOfContract == 1)
        //                                {
        //                                    colPrice = col;
        //                                    foreach (var gov in price.FTLNormal.ListGOV)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code; 
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                    row = 4;
        //                                    if (item.ListRouting != null)
        //                                    {
        //                                        foreach (var route in item.ListRouting)
        //                                        {
        //                                            colPrice = col;
        //                                            foreach (var gov in price.FTLNormal.ListGOV)
        //                                            {
        //                                                var value = price.FTLNormal.ListDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == gov.ID).FirstOrDefault();
        //                                                if (value != null)
        //                                                {
        //                                                    colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                }
        //                                                else
        //                                                {
        //                                                    colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                }
        //                                            }
        //                                            row++;
        //                                        }
        //                                    }
        //                                }
        //                                else if (price.ItemPrice.TypeOfContract == 2)
        //                                {
        //                                    row = 2;
        //                                    colPrice = col;
        //                                    foreach (var gov in price.FTLNormal.ListGOV)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
        //                                        worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                        colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                    row = 4;
        //                                    if (item.ListRouting != null)
        //                                    {
        //                                        foreach (var route in item.ListRouting)
        //                                        {
        //                                            colPrice = col;
        //                                            foreach (var gov in price.FTLNormal.ListGOV)
        //                                            {
        //                                                colPrice++;
        //                                                var value = price.FTLNormal.ListDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == gov.ID).FirstOrDefault();
        //                                                if (value != null)
        //                                                {
        //                                                    worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                    worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                }
        //                                                else
        //                                                {
        //                                                    worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                    worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                }
        //                                                colPrice++;
        //                                            }
        //                                            row++;
        //                                        }
        //                                    }
        //                                }

        //                                col = colPrice;
        //                            }
        //                            #endregion

        //                            #region FTL Level
        //                            if (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2))
        //                            {
        //                                if (price.ItemPrice.TypeOfContract == 1)
        //                                {
        //                                    colPrice = col;
        //                                    foreach (var level in price.FTLLevel.ListLevel)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                    row = 4;
        //                                    if (item.ListRouting != null)
        //                                    {
        //                                        foreach (var route in item.ListRouting)
        //                                        {
        //                                            colPrice = col;
        //                                            foreach (var level in price.FTLLevel.ListLevel)
        //                                            {
        //                                                var value = price.FTLLevel.ListDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
        //                                                if (value != null)
        //                                                {
        //                                                    colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                }
        //                                                else
        //                                                {
        //                                                    colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                }
        //                                            }
        //                                            row++;
        //                                        }
        //                                    }
        //                                }
        //                                else if (price.ItemPrice.TypeOfContract == 2)
        //                                {
        //                                    row = 2;
        //                                    colPrice = col;
        //                                    foreach (var level in price.FTLLevel.ListLevel)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
        //                                        worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                        colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                    row = 4;
        //                                    if (item.ListRouting != null)
        //                                    {
        //                                        foreach (var route in item.ListRouting)
        //                                        {
        //                                            colPrice = col;
        //                                            foreach (var level in price.FTLLevel.ListLevel)
        //                                            {
        //                                                colPrice++;
        //                                                var value = price.FTLLevel.ListDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
        //                                                if (value != null)
        //                                                {
        //                                                    worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                    worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                }
        //                                                else
        //                                                {
        //                                                    worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                    worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                }
        //                                                colPrice++;
        //                                            }
        //                                            row++;
        //                                        }
        //                                    }
        //                                }
        //                                col = colPrice;
        //                            }
        //                            #endregion
        //                        }
        //                        #endregion

        //                        #region LTL
        //                        if (transportModeID == iLTL)
        //                        {
        //                            #region LTL Normal
        //                            if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                            {
        //                                if (price.ItemPrice.TypeOfContract == 1)
        //                                {
        //                                    colPrice = col;
        //                                    foreach (var gop in item.ListGroupOfProduct)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = gop.GroupName; 
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                    row = 4;
        //                                    foreach (var data in price.LTLNormal)
        //                                    {
        //                                        colPrice = col;
        //                                        foreach (var gop in item.ListGroupOfProduct)
        //                                        {
        //                                            var value = data.ListGroupProductPrice.Where(c => gop.ID == c.GroupOfProductID).FirstOrDefault();
        //                                            if (value != null)
        //                                            {
        //                                                colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                            }
        //                                            else
        //                                            {
        //                                                colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                            }
        //                                        }
        //                                        row++;
        //                                    }
        //                                }
        //                                else if (price.ItemPrice.TypeOfContract == 2)
        //                                {
        //                                    row = 2;
        //                                    colPrice = col;
        //                                    foreach (var gov in item.ListGroupOfProduct)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
        //                                        worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                        colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                    row = 4;
        //                                    foreach (var data in price.LTLNormal)
        //                                    {
        //                                        colPrice = col;
        //                                        foreach (var gop in item.ListGroupOfProduct)
        //                                        {
        //                                            colPrice++;
        //                                            var value = data.ListGroupProductPrice.Where(c => gop.ID == c.GroupOfProductID).FirstOrDefault();
        //                                            if (value != null)
        //                                            {
        //                                                worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                            }
        //                                            else
        //                                            {
        //                                                worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                worksheet.Cells[row, colPrice + 1].Value = 0;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                            }
        //                                            colPrice++;
        //                                        }
        //                                        row++;
        //                                    }
        //                                }

        //                                col = colPrice;
        //                            }
        //                            #endregion

        //                            #region LTL Level
        //                            if (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2))
        //                            {
        //                                row = 2;
        //                                colPrice = col;
        //                                foreach (var level in price.LTLLevel.ListLevel)
        //                                {
        //                                    colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
        //                                    colPrice--;
        //                                    foreach (var gop in item.ListGroupOfProduct)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row + 1, colPrice].Value = gop.GroupName;
        //                                    }
        //                                    ExcelHelper.CreateCellStyle(worksheet, row, colPrice - item.ListGroupOfProduct.Count + 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - item.ListGroupOfProduct.Count + 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                }
        //                                worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                row = 4;
        //                                colPrice = col;
        //                                if (item.ListRouting != null)
        //                                {
        //                                    foreach (var route in item.ListRouting)
        //                                    {
        //                                        foreach (var level in price.LTLLevel.ListLevel)
        //                                        {
        //                                            foreach (var gop in item.ListGroupOfProduct)
        //                                            {
        //                                                colPrice++;
        //                                                var value = price.LTLLevel.ListDetail.Where(c => c.RoutingID == route.ID && c.LevelID == level.ID && c.GroupProductID == gop.ID).FirstOrDefault();
        //                                                if (value != null)
        //                                                {
        //                                                    worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                }
        //                                                else
        //                                                {
        //                                                    worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                }
        //                                            }
        //                                        }
        //                                        colPrice = colPrice - item.ListGroupOfProduct.Count * price.LTLLevel.ListLevel.Count;
        //                                        row++;
        //                                    }
        //                                }

        //                                col = colPrice + item.ListGroupOfProduct.Count * price.LTLLevel.ListLevel.Count;
        //                            }
        //                            #endregion
        //                        }
        //                        #endregion

        //                        #region FCL
        //                        if (transportModeID == iFCL)
        //                        {
        //                            if (price.ItemPrice.TypeOfContract == 1)
        //                            {
        //                                colPrice = col;
        //                                foreach (var pack in price.FCLData.ListPacking)
        //                                {
        //                                    colPrice++; worksheet.Cells[row, colPrice].Value = pack.Code;
        //                                    ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                }
        //                                worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                row = 4;
        //                                if (item.ListRouting != null)
        //                                {
        //                                    foreach (var route in item.ListRouting)
        //                                    {
        //                                        colPrice = col;
        //                                        foreach (var pack in price.FCLData.ListPacking)
        //                                        {
        //                                            var value = price.FCLData.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == pack.ID).FirstOrDefault();
        //                                            if (value != null)
        //                                            {
        //                                                colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                            }
        //                                            else
        //                                            {
        //                                                colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                            }
        //                                        }
        //                                        row++;
        //                                    }
        //                                }
        //                            }
        //                            else if (price.ItemPrice.TypeOfContract == 2)
        //                            {
        //                                row = 2;
        //                                colPrice = col;
        //                                foreach (var pack in price.FCLData.ListPacking)
        //                                {
        //                                    colPrice++; worksheet.Cells[row, colPrice].Value = pack.Code;
        //                                    worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                    colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                    ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                }
        //                                worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                row = 4;
        //                                if (item.ListRouting != null)
        //                                {
        //                                    foreach (var route in item.ListRouting)
        //                                    {
        //                                        colPrice = col;
        //                                        foreach (var pack in price.FCLData.ListPacking)
        //                                        {
        //                                            colPrice++;
        //                                            var value = price.FCLData.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == pack.ID).FirstOrDefault();
        //                                            if (value != null)
        //                                            {
        //                                                worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                            }
        //                                            else
        //                                            {
        //                                                worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                            }
        //                                            colPrice++;
        //                                        }
        //                                        row++;
        //                                    }
        //                                }
        //                            }

        //                            col = colPrice;
        //                        }
        //                        #endregion

        //                        if (col >= colR + 1)
        //                        {
        //                            var hasMerge = false;
        //                            if (transportModeID == iFTL)
        //                            {
        //                                if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                                {
        //                                    if (price.ItemPrice.TypeOfContract == 1)
        //                                    {
        //                                        ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index%3], ExcelHelper.ColorWhite, 0, "");
        //                                        hasMerge = true;
        //                                    }
        //                                }
        //                                if (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2))
        //                                {
        //                                    if (price.ItemPrice.TypeOfContract == 1)
        //                                    {
        //                                        ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        hasMerge = true;
        //                                    }
        //                                }
        //                            }
        //                            if (transportModeID == iLTL)
        //                            {
        //                                if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                                {
        //                                    if (price.ItemPrice.TypeOfContract == 1)
        //                                    {
        //                                        ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        hasMerge = true;
        //                                    }
        //                                }
        //                            }
        //                            if (transportModeID == iFCL)
        //                            {
        //                                if (price.ItemPrice.TypeOfContract == 1)
        //                                {
        //                                    ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    hasMerge = true;
        //                                }
        //                            }
        //                            if (hasMerge == false)
        //                            {
        //                                ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 1, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                            }
        //                            worksheet.Cells[1, colR + 1, 2, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                            worksheet.Cells[1, colR + 1, 2, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                            index++;
        //                        }
        //                    }
        //                    #endregion
        //                    col++;
        //                }
        //            }


        //            if (worksheet.Dimension != null)
        //            {
        //                for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
        //                {
        //                    for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
        //                    {
        //                        worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    }
        //                }
        //            }
        //            package.Save();
        //        }
        //        return file;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private string PriceHistory_ExportMulUser(int cusId, List<int> listVenId, int transportModeID, int typePrice)
        //{
        //    try
        //    {
        //        DTOCUSPrice_HistoryData result = new DTOCUSPrice_HistoryData();
        //        ServiceFactory.SVVendor((ISVVendor sv) =>
        //        {
        //            result = sv.PriceHistory_GetDataMulUser(cusId, listVenId, transportModeID, typePrice);
        //        });

        //        string[] colorCol = new string[] { ExcelHelper.ColorGreen, ExcelHelper.ColorBlue, ExcelHelper.ColorOrange };

        //        int iFCL = -(int)SYSVarType.TransportModeFCL;
        //        int iFTL = -(int)SYSVarType.TransportModeFTL;
        //        int iLTL = -(int)SYSVarType.TransportModeLTL;

        //        string file = "/" + FolderUpload.Export + "ExportPriceHistoryVendor_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

        //        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
        //            System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
        //        FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
        //        using (ExcelPackage package = new ExcelPackage(exportfile))
        //        {
        //            // Sheet 1
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

        //            int col = 1, row = 1;
        //            if (result.ListPriceOfCustomer != null)
        //            {
        //                row = 3;
        //                int colRte = col;
        //                worksheet.Cells[row, colRte].Value = "Mã CĐ Hệ thống";
        //                colRte++; worksheet.Cells[row, colRte].Value = "Tên CĐ Hệ thống";
        //                ExcelHelper.CreateCellStyle(worksheet, row, col, row, colRte, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
        //                row = 4;
        //                if (result.ListRoute != null)
        //                {
        //                    foreach (var route in result.ListRoute)
        //                    {
        //                        colRte = col;
        //                        worksheet.Cells[row, colRte].Value = route.Code; worksheet.Column(colRte).Width = 20;
        //                        colRte++; worksheet.Cells[row, colRte].Value = route.RoutingName; worksheet.Column(colRte).Width = 20;
        //                        row++;
        //                    }
        //                }

        //                #region cung đường
        //                col = 2;
        //                int index = 0;
        //                if (result.ListRoute != null)
        //                {
        //                    foreach (var item in result.ListPriceOfCustomer)
        //                    {
        //                        row = 3;
        //                        colRte = col;
        //                        //row = 3;
        //                        //worksheet.Cells[row, colRte].Value = "Mã CĐ Hệ thống";
        //                        //colRte++; worksheet.Cells[row, colRte].Value = "Tên CĐ Hệ thống";
        //                        colRte++; worksheet.Cells[row - 2, colRte].Value = item.CustomerName;
        //                        worksheet.Cells[row, colRte].Value = "Mã cung đường"; worksheet.Column(colRte).Width = 20;
        //                        colRte++; worksheet.Cells[row, colRte].Value = "Tên cung đường"; worksheet.Column(colRte).Width = 20;
        //                        ExcelHelper.CreateCellStyle(worksheet, row - 2, colRte - 1, row - 1, colRte, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                        ExcelHelper.CreateCellStyle(worksheet, row, col + 1, row, colRte, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                        worksheet.Cells[row - 2, col, row, colRte].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        worksheet.Cells[row - 2, col, row, colRte].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                        row = 4;
        //                        colRte = col;
        //                        if (item.ListRouting != null && result.ListRoute != null)
        //                        {
        //                            foreach (var rSys in result.ListRoute)
        //                            {
        //                                if (item.ListRouting != null)
        //                                {
        //                                    foreach (var route in item.ListRouting)
        //                                    {
        //                                        if (route.CATID == rSys.RoutingID)
        //                                        {
        //                                            colRte = col;
        //                                            //worksheet.Cells[row, colRte].Value = route.CATCode; worksheet.Column(colRte).Width = 20;
        //                                            //colRte++; worksheet.Cells[row, colRte].Value = route.CATName; worksheet.Column(colRte).Width = 20;
        //                                            colRte++; worksheet.Cells[row, colRte].Value = route.Code; worksheet.Column(colRte).Width = 20;
        //                                            colRte++; worksheet.Cells[row, colRte].Value = route.RoutingName; worksheet.Column(colRte).Width = 20;
        //                                        }
        //                                    }
        //                                }

        //                                row++;
        //                            }
        //                        }
        //                        col = col + 2;
        //                        index++;
        //                    }
        //                }
        //                #endregion

        //                index = 0;
        //                foreach (var item in result.ListPriceOfCustomer)
        //                {
        //                    #region bảng giá
        //                    //col = colRte;
        //                    int colPrice;
        //                    foreach (var price in result.ListPrice)
        //                    {
        //                        if (price.ItemPrice.CustomerID == item.CustomerID)
        //                        {
        //                            row = 1;
        //                            int colR = col;
        //                            colPrice = col;
        //                            if ((price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1)) || (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2)) || (transportModeID == iFCL && price.FCLData.ListPacking.Count > 0))
        //                            {
        //                                colPrice++; worksheet.Cells[row, colPrice].Value = price.ItemPrice.CustomerName + " - " + price.ItemPrice.Code + " - " + price.ItemPrice.EffectDate.ToString("dd/MM/yyyy");
        //                            }
        //                            row = 3;
        //                            #region FTL
        //                            if (transportModeID == iFTL)
        //                            {
        //                                #region FTL Normal
        //                                if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                                {
        //                                    if (price.ItemPrice.TypeOfContract == 1)
        //                                    {
        //                                        colPrice = col;
        //                                        foreach (var gov in price.FTLNormal.ListGOV)
        //                                        {
        //                                            colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
        //                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        }
        //                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                        row = 4;
        //                                        if (item.ListRouting != null && result.ListRoute != null)
        //                                        {
        //                                            foreach (var rSys in result.ListRoute)
        //                                            {
        //                                                foreach (var route in item.ListRouting)
        //                                                {
        //                                                    if (route.CATID == rSys.RoutingID)
        //                                                    {
        //                                                        colPrice = col;
        //                                                        foreach (var gov in price.FTLNormal.ListGOV)
        //                                                        {
        //                                                            var value = price.FTLNormal.ListDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == gov.ID).FirstOrDefault();
        //                                                            if (value != null)
        //                                                            {
        //                                                                colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                                row++;
        //                                            }
        //                                        }
        //                                    }
        //                                    else if (price.ItemPrice.TypeOfContract == 2)
        //                                    {
        //                                        row = 2;
        //                                        colPrice = col;
        //                                        foreach (var gov in price.FTLNormal.ListGOV)
        //                                        {
        //                                            colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
        //                                            worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                            ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        }
        //                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                        row = 4;
        //                                        if (item.ListRouting != null && result.ListRoute != null)
        //                                        {
        //                                            foreach (var rSys in result.ListRoute)
        //                                            {
        //                                                foreach (var route in item.ListRouting)
        //                                                {
        //                                                    if (route.CATID == rSys.RoutingID)
        //                                                    {
        //                                                        colPrice = col;
        //                                                        foreach (var gov in price.FTLNormal.ListGOV)
        //                                                        {
        //                                                            colPrice++;
        //                                                            var value = price.FTLNormal.ListDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == gov.ID).FirstOrDefault();
        //                                                            if (value != null)
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                                worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                                worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            colPrice++;
        //                                                        }
        //                                                    }
        //                                                }
        //                                                row++;
        //                                            }
        //                                        }
        //                                    }

        //                                    col = colPrice;
        //                                }
        //                                #endregion

        //                                #region FTL Level
        //                                if (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2))
        //                                {
        //                                    if (price.ItemPrice.TypeOfContract == 1)
        //                                    {
        //                                        colPrice = col;
        //                                        foreach (var level in price.FTLLevel.ListLevel)
        //                                        {
        //                                            colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
        //                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        }
        //                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                        row = 4;
        //                                        if (item.ListRouting != null && result.ListRoute != null)
        //                                        {
        //                                            foreach (var rSys in result.ListRoute)
        //                                            {
        //                                                foreach (var route in item.ListRouting)
        //                                                {
        //                                                    if (route.CATID == rSys.RoutingID)
        //                                                    {
        //                                                        colPrice = col;
        //                                                        foreach (var level in price.FTLLevel.ListLevel)
        //                                                        {
        //                                                            var value = price.FTLLevel.ListDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
        //                                                            if (value != null)
        //                                                            {
        //                                                                colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                                row++;
        //                                            }
        //                                        }
        //                                    }
        //                                    else if (price.ItemPrice.TypeOfContract == 2)
        //                                    {
        //                                        row = 2;
        //                                        colPrice = col;
        //                                        foreach (var level in price.FTLLevel.ListLevel)
        //                                        {
        //                                            colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
        //                                            worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                            ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        }
        //                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                        row = 4;
        //                                        if (item.ListRouting != null && result.ListRoute != null)
        //                                        {
        //                                            foreach (var rSys in result.ListRoute)
        //                                            {
        //                                                foreach (var route in item.ListRouting)
        //                                                {
        //                                                    if (route.CATID == rSys.RoutingID)
        //                                                    {
        //                                                        colPrice = col;
        //                                                        foreach (var level in price.FTLLevel.ListLevel)
        //                                                        {
        //                                                            colPrice++;
        //                                                            var value = price.FTLLevel.ListDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
        //                                                            if (value != null)
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                                worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                                worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            colPrice++;
        //                                                        }
        //                                                    }
        //                                                }
        //                                                row++;
        //                                            }
        //                                        }
        //                                    }
        //                                    col = colPrice;
        //                                }
        //                                #endregion
        //                            }
        //                            #endregion

        //                            #region LTL
        //                            if (transportModeID == iLTL)
        //                            {
        //                                #region LTL Normal
        //                                if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                                {
        //                                    if (price.ItemPrice.TypeOfContract == 1)
        //                                    {
        //                                        colPrice = col;
        //                                        foreach (var gop in item.ListGroupOfProduct)
        //                                        {
        //                                            colPrice++; worksheet.Cells[row, colPrice].Value = gop.GroupName;
        //                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        }
        //                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                        row = 4;
        //                                        if (price.LTLNormal != null && result.ListRoute != null)
        //                                        {
        //                                            foreach (var rSys in result.ListRoute)
        //                                            {
        //                                                foreach (var data in price.LTLNormal)
        //                                                {
        //                                                    if (data.CATID == rSys.RoutingID)
        //                                                    {
        //                                                        colPrice = col;
        //                                                        foreach (var gop in item.ListGroupOfProduct)
        //                                                        {
        //                                                            var value = data.ListGroupProductPrice.Where(c => gop.ID == c.GroupOfProductID).FirstOrDefault();
        //                                                            if (value != null)
        //                                                            {
        //                                                                colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                                row++;
        //                                            }
        //                                        }
        //                                    }
        //                                    else if (price.ItemPrice.TypeOfContract == 2)
        //                                    {
        //                                        row = 2;
        //                                        colPrice = col;
        //                                        foreach (var gov in item.ListGroupOfProduct)
        //                                        {
        //                                            colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
        //                                            worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                            ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        }
        //                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                        row = 4;
        //                                        if (result.ListRoute != null && price.LTLNormal != null)
        //                                        {
        //                                            foreach (var rSys in result.ListRoute)
        //                                            {
        //                                                foreach (var data in price.LTLNormal)
        //                                                {
        //                                                    if (data.CATID == rSys.RoutingID)
        //                                                    {
        //                                                        colPrice = col;
        //                                                        foreach (var gop in item.ListGroupOfProduct)
        //                                                        {
        //                                                            colPrice++;
        //                                                            var value = data.ListGroupProductPrice.Where(c => gop.ID == c.GroupOfProductID).FirstOrDefault();
        //                                                            if (value != null)
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                                worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                                worksheet.Cells[row, colPrice + 1].Value = 0;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            colPrice++;
        //                                                        }
        //                                                    }
        //                                                }
        //                                                row++;
        //                                            }
        //                                        }
        //                                    }

        //                                    col = colPrice;
        //                                }
        //                                #endregion

        //                                #region LTL Level
        //                                if (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2))
        //                                {
        //                                    row = 2;
        //                                    colPrice = col;
        //                                    foreach (var level in price.LTLLevel.ListLevel)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
        //                                        colPrice--;
        //                                        foreach (var gop in item.ListGroupOfProduct)
        //                                        {
        //                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = gop.GroupName;
        //                                        }
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice - item.ListGroupOfProduct.Count + 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - item.ListGroupOfProduct.Count + 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                    row = 4;
        //                                    colPrice = col;
        //                                    if (item.ListRouting != null && result.ListRoute != null)
        //                                    {
        //                                        foreach (var rSys in result.ListRoute)
        //                                        {
        //                                            foreach (var route in item.ListRouting)
        //                                            {
        //                                                if (route.CATID == rSys.RoutingID)
        //                                                {
        //                                                    foreach (var level in price.LTLLevel.ListLevel)
        //                                                    {
        //                                                        foreach (var gop in item.ListGroupOfProduct)
        //                                                        {
        //                                                            colPrice++;
        //                                                            var value = price.LTLLevel.ListDetail.Where(c => c.RoutingID == route.ID && c.LevelID == level.ID && c.GroupProductID == gop.ID).FirstOrDefault();
        //                                                            if (value != null)
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                                ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            }
        //                                                        }
        //                                                    }

        //                                                    colPrice = colPrice - item.ListGroupOfProduct.Count * price.LTLLevel.ListLevel.Count;
        //                                                }
        //                                            }
        //                                            row++;
        //                                        }
        //                                    }
        //                                    col = colPrice + item.ListGroupOfProduct.Count * price.LTLLevel.ListLevel.Count;
        //                                }
        //                                #endregion
        //                            }
        //                            #endregion

        //                            #region FCL
        //                            if (transportModeID == iFCL)
        //                            {
        //                                if (price.ItemPrice.TypeOfContract == 1)
        //                                {
        //                                    colPrice = col;
        //                                    foreach (var pack in price.FCLData.ListPacking)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = pack.Code;
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                    row = 4;
        //                                    if (item.ListRouting != null && result.ListRoute != null)
        //                                    {
        //                                        foreach (var rSys in result.ListRoute)
        //                                        {
        //                                            foreach (var route in item.ListRouting)
        //                                            {
        //                                                if (route.CATID == rSys.RoutingID)
        //                                                {
        //                                                    colPrice = col;
        //                                                    foreach (var pack in price.FCLData.ListPacking)
        //                                                    {
        //                                                        var value = price.FCLData.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == pack.ID).FirstOrDefault();
        //                                                        if (value != null)
        //                                                        {
        //                                                            colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
        //                                                            ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                            ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                            row++;
        //                                        }
        //                                    }
        //                                }
        //                                else if (price.ItemPrice.TypeOfContract == 2)
        //                                {
        //                                    row = 2;
        //                                    colPrice = col;
        //                                    foreach (var pack in price.FCLData.ListPacking)
        //                                    {
        //                                        colPrice++; worksheet.Cells[row, colPrice].Value = pack.Code;
        //                                        worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
        //                                        colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
        //                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                    }
        //                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //                                    row = 4;
        //                                    if (item.ListRouting != null && result.ListRoute != null)
        //                                    {
        //                                        foreach (var rSys in result.ListRoute)
        //                                        {
        //                                            foreach (var route in item.ListRouting)
        //                                            {
        //                                                if (route.CATID == rSys.RoutingID)
        //                                                {
        //                                                    colPrice = col;
        //                                                    foreach (var pack in price.FCLData.ListPacking)
        //                                                    {
        //                                                        colPrice++;
        //                                                        var value = price.FCLData.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == pack.ID).FirstOrDefault();
        //                                                        if (value != null)
        //                                                        {
        //                                                            worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
        //                                                            ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
        //                                                            ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                            ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
        //                                                            worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
        //                                                            ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
        //                                                        }
        //                                                        colPrice++;
        //                                                    }
        //                                                }
        //                                            }
        //                                            row++;
        //                                        }
        //                                    }
        //                                }

        //                                col = colPrice;
        //                            }
        //                            #endregion

        //                            if (col >= colR + 1)
        //                            {
        //                                var hasMerge = false;
        //                                if (transportModeID == iFTL)
        //                                {
        //                                    if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                                    {
        //                                        if (price.ItemPrice.TypeOfContract == 1)
        //                                        {
        //                                            ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                            hasMerge = true;
        //                                        }
        //                                    }
        //                                    if (price.ItemPrice.CheckPrice.HasLevel == true && (typePrice == 0 || typePrice == 2))
        //                                    {
        //                                        if (price.ItemPrice.TypeOfContract == 1)
        //                                        {
        //                                            ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                            hasMerge = true;
        //                                        }
        //                                    }
        //                                }
        //                                if (transportModeID == iLTL)
        //                                {
        //                                    if (price.ItemPrice.CheckPrice.HasNormal == true && (typePrice == 0 || typePrice == 1))
        //                                    {
        //                                        if (price.ItemPrice.TypeOfContract == 1)
        //                                        {
        //                                            ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                            hasMerge = true;
        //                                        }
        //                                    }
        //                                }
        //                                if (transportModeID == iFCL)
        //                                {
        //                                    if (price.ItemPrice.TypeOfContract == 1)
        //                                    {
        //                                        ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                        hasMerge = true;
        //                                    }
        //                                }
        //                                if (hasMerge == false)
        //                                {
        //                                    ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 1, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
        //                                }
        //                                worksheet.Cells[1, colR + 1, 2, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                                worksheet.Cells[1, colR + 1, 2, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //                                index++;
        //                            }
        //                        }
        //                    }
        //                    #endregion
        //                }
        //            }


        //            if (worksheet.Dimension != null)
        //            {
        //                for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
        //                {
        //                    for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
        //                    {
        //                        worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    }
        //                }
        //            }
        //            package.Save();
        //        }
        //        return file;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<int> PriceHistory_GetListVendor(dynamic dynParam)
        {
            try
            {
                int cusId = (int)dynParam.cusId;
                List<int> result = new List<int>();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.PriceHistory_GetListVendor(cusId);

                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VenContract Setting

        [HttpPost]
        public void VenContractSetting_Save(dynamic dynParam)
        {
            try
            {
                string setting = dynParam.setting.ToString();
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VenContractSetting_Save(setting, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Setting_TypeOfSGroupProductChangeSave(dynamic dynParam)
        {
            try
            {
                int typeID = (int)dynParam.typeID;
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Setting_TypeOfSGroupProductChangeSave(typeID, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Setting_TypeOfRunLevelSave(dynamic dynParam)
        {
            try
            {
                int typeID = (int)dynParam.typeID;
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Setting_TypeOfRunLevelSave(typeID, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_Setting_GOVList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Setting_GOVList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATGroupOfVehicle> VENContract_Setting_Level_GOVList(dynamic dynParam)
        {
            try
            {
                var result = default(List<CATGroupOfVehicle>);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Setting_Level_GOVList(contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATContractGroupVehicle VENContract_Setting_GOVGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATContractGroupVehicle);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Setting_GOVGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Setting_GOVSave(dynamic dynParam)
        {
            try
            {
                DTOCATContractGroupVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractGroupVehicle>(dynParam.item.ToString());

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Setting_GOVSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Setting_GOVDeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Setting_GOVDeleteList(lst, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENContract_Setting_GOVNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Setting_GOVNotInList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_Setting_GOVNotInSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Setting_GOVNotInSave(lst, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENContract_Setting_LevelList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Setting_LevelList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATContractLevel VENContract_Setting_LevelGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATContractLevel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_Setting_LevelGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Setting_LevelSave(dynamic dynParam)
        {
            try
            {
                DTOCATContractLevel item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractLevel>(dynParam.item.ToString());
                int contractID = (int)dynParam.contractID;
                int typeMode = (int)dynParam.typeMode;
                if (typeMode == 2)
                {
                    item.Ton = null;
                    item.CBM = null;
                    item.Quantity = null;
                }
                else if (typeMode == 3)
                {
                    item.GroupOfVehicleID = null;
                    item.DateStart = null;
                    item.DateEnd = null;
                }
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Setting_LevelSave(item, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_Setting_LevelDeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_Setting_LevelDeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Contract term(phụ lục hợp đồng)
        [HttpPost]
        public DTOResult VENContract_ContractTerm_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOContractTerm VENContract_ContractTerm_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int contractID = (int)dynParam.contractID;
                var result = default(DTOContractTerm);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_Get(id, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int VENContract_ContractTerm_Save(dynamic dynParam)
        {
            try
            {
                DTOContractTerm item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOContractTerm>(dynParam.item.ToString());
                int contractID = (int)dynParam.contractID;
                int result = -1;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_Save(item, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_ContractTerm_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_ContractTerm_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENContract_ContractTerm_Price_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_Price_List(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_ContractTerm_Open(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_ContractTerm_Open(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_ContractTerm_Close(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_ContractTerm_Close(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENTerm_Change_RemoveWarning(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENTerm_Change_RemoveWarning(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region KPI Term
        [HttpPost]
        public DTOResult VENContract_ContractTerm_KPITime_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_KPITime_List(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_ContractTerm_KPITime_SaveExpr(dynamic dynParam)
        {
            try
            {
                DTOContractTerm_KPITime item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOContractTerm_KPITime>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_ContractTerm_KPITime_SaveExpr(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_ContractTerm_KPITime_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_KPITime_NotInList(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_ContractTerm_KPITime_SaveNotInList(dynamic dynParam)
        {
            try
            {
                List<DTOContractTerm_TypeOfKPI> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractTerm_TypeOfKPI>>(dynParam.lst.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_ContractTerm_KPITime_SaveNotInList(lst, contractTermID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_ContractTerm_KPIQuantity_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_KPIQuantity_List(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_ContractTerm_KPIQuantity_SaveExpr(dynamic dynParam)
        {
            try
            {
                DTOContractTerm_KPIQuantity item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOContractTerm_KPIQuantity>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_ContractTerm_KPIQuantity_SaveExpr(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_ContractTerm_KPIQuantity_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_ContractTerm_KPIQuantity_NotInList(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENContract_ContractTerm_KPIQuantity_SaveNotInList(dynamic dynParam)
        {
            try
            {
                List<DTOContractTerm_TypeOfKPI> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractTerm_TypeOfKPI>>(dynParam.lst.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_ContractTerm_KPIQuantity_SaveNotInList(lst, contractTermID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DateTime? VENContract_KPITime_Check_Expression(dynamic dynParam)
        {
            try
            {
                KPITimeDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPITimeDate>(dynParam.item.ToString());
                List<KPITimeDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPITimeDate>>(dynParam.lst.ToString());
                DateTime? result = null;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPITime_Check_Expression(item, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool? VENContract_KPITime_Check_Hit(dynamic dynParam)
        {
            try
            {
                KPITimeDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPITimeDate>(dynParam.item.ToString());
                List<KPITimeDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPITimeDate>>(dynParam.lst.ToString());
                bool? result = null;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPITime_Check_Hit(item, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIQuantityDate> VENContract_KPIQuantity_Get(dynamic dynParam)
        {
            try
            {
                List<KPIQuantityDate> result = new List<KPIQuantityDate>(); ;
                int id = (int)dynParam.id;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPIQuantity_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public KPIQuantityDate VENContract_KPIQuantity_Check_Expression(dynamic dynParam)
        {
            try
            {
                KPIQuantityDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIQuantityDate>(dynParam.item.ToString());
                List<KPIQuantityDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPIQuantityDate>>(dynParam.lst.ToString());
                KPIQuantityDate result = new KPIQuantityDate(); ;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPIQuantity_Check_Expression(item, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool? VENContract_KPIQuantity_Check_Hit(dynamic dynParam)
        {
            try
            {
                KPIQuantityDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIQuantityDate>(dynParam.item.ToString());
                List<KPIQuantityDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPIQuantityDate>>(dynParam.lst.ToString());
                bool? result = null;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_KPIQuantity_Check_Hit(item, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region price Ex CO new

        #region info
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceCOEx VENPrice_CO_Ex_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOPriceCOEx);
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int VENPrice_CO_Ex_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceCOEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceCOEx>(dynParam.item.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_CO_Ex_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  location
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_Location_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Location_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceCOExLocation VENPrice_CO_Ex_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceCOExLocation result = new DTOPriceCOExLocation();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceCOExLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceCOExLocation>(dynParam.item.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Location_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Location_LocationNotInSaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Location_LocationNotInList(request, priceExID, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  route
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_Route_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Route_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Route_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Route_RouteNotInList(request, priceExID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  parnet route
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_ParentRoute_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_ParentRoute_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_ParentRoute_RouteNotInList(request, priceExID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  partner
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Partner_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Partner_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_Ex_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Ex_Partner_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult VENPrice_CO_Ex_Partner_PartnerNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
               ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Ex_Partner_PartnerNotInList(request, priceExID, contractTermID, CustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
        #endregion

        #region VENContract Price CO
        [HttpPost]
        public DTOVENPriceCO_Data VENContract_PriceCO_Data(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                var result = default(DTOVENPriceCO_Data);
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   result = sv.VENContract_PriceCO_Data(contractTermID);
               });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceCOContainerData VENPrice_CO_COContainer_Data(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPriceCOContainerData);
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   result = sv.VENPrice_CO_COContainer_Data(priceID);
               });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void VENPrice_CO_COContainer_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceCOContainer> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceCOContainer>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   sv.VENPrice_CO_COContainer_SaveList(data, priceID);
               });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult VENPrice_CO_COContainer_ContainerList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = (string)dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   result = sv.VENPrice_CO_COContainer_ContainerList(request, priceID);
               });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_CO_COContainer_ContainerNotInSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   sv.VENPrice_CO_COContainer_ContainerNotInSave(lst, priceID);
               });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult VENPrice_CO_COContainer_ContainerNotInList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = (string)dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   result = sv.VENPrice_CO_COContainer_ContainerNotInList(request, priceID);
               });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_CO_COContainer_ContainerDelete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   sv.VENPrice_CO_COContainer_ContainerDelete(lst, priceID);
               });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string VENPrice_CO_GroupContainer_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                DTOPriceCOContainerExcelData data = new DTOPriceCOContainerExcelData();
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   data = sv.VENPrice_CO_COContainer_ExcelData(priceID, contractTermID);
               });

                string result = "/" + FolderUpload.Export + "Bảng giá FCL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 1, row = 1, stt = 1;

                    #region header
                    if (isFrame)
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 50;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row + 1, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        foreach (var level in data.ListPacking)
                        {
                            col++; worksheet.Cells[row, col].Value = level.Code;
                            worksheet.Cells[row + 1, col].Value = "Giá từ";
                            col++; worksheet.Cells[row + 1, col].Value = "Đến giá";
                            ExcelHelper.CreateCellStyle(worksheet, row, col - 1, row, col, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                            ExcelHelper.CreateCellStyle(worksheet, row + 1, col - 1, row + 1, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        }
                        worksheet.Cells[row, 1, row + 1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1, row + 1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 50;
                        foreach (var level in data.ListPacking)
                        {
                            col++; worksheet.Cells[row, col].Value = level.Code;
                        }
                        ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    #endregion

                    #region data

                    if (isFrame)
                    {
                        col = 1;
                        row = 3;
                        foreach (var route in data.ListRouting)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = route.Code;
                            col++; worksheet.Cells[row, col].Value = route.RoutingName;
                            foreach (var level in data.ListPacking)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == level.ID).FirstOrDefault();
                                if (check != null)
                                {
                                    worksheet.Cells[row, col].Value = check.PriceMin;
                                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                    worksheet.Cells[row, col + 1].Value = check.PriceMax;
                                    ExcelHelper.CreateFormat(worksheet, row, col + 1, ExcelHelper.FormatMoney);
                                }
                                col++;
                            }
                            row++;
                            stt++;
                        }
                    }
                    else
                    {
                        col = 1;
                        row = 2;
                        foreach (var route in data.ListRouting)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = route.Code;
                            col++; worksheet.Cells[row, col].Value = route.RoutingName;
                            foreach (var level in data.ListPacking)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == level.ID).FirstOrDefault();
                                if (check != null)
                                {
                                    worksheet.Cells[row, col].Value = check.Price;
                                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                                }
                            }
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
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOPriceCOContainerImport> VENPrice_CO_GroupContainer_ExcelCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                List<DTOPriceCOContainerImport> result = new List<DTOPriceCOContainerImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOPriceCOContainerExcelData data = new DTOPriceCOContainerExcelData();
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                   {
                       data = sv.VENPrice_CO_COContainer_ExcelData(priceID, contractTermID);
                   });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 4, row = 1;
                            string levelCode = "", Input = "";
                            Dictionary<int, int> dictColLevel = new Dictionary<int, int>();
                            Dictionary<int, string> dictColLevelCode = new Dictionary<int, string>();
                            if (worksheet != null)
                            {
                                if (isFrame)
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListPacking.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Container [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 2;
                                    }

                                    row = 3;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceCOContainerImport obj = new DTOPriceCOContainerImport();
                                        obj.ListDetail = new List<DTOPriceCOContainerDetail>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRouting.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string priceMin = ExcelHelper.GetValue(worksheet, row, pCol);
                                                string priceMax = ExcelHelper.GetValue(worksheet, row, pCol + 1);


                                                if (!string.IsNullOrEmpty(priceMin) || !string.IsNullOrEmpty(priceMax))
                                                {
                                                    DTOPriceCOContainerDetail objDetail = new DTOPriceCOContainerDetail();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.ContractRoutingID = obj.RouteID;
                                                    objDetail.PackingID = pLevel;
                                                    objDetail.Price = 0;
                                                    if (string.IsNullOrEmpty(priceMin)) objDetail.PriceMin = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMin = Convert.ToDecimal(priceMin);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của loại [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    if (string.IsNullOrEmpty(priceMax)) objDetail.PriceMax = null;
                                                    else
                                                    {
                                                        try
                                                        {
                                                            objDetail.PriceMax = Convert.ToDecimal(priceMax);
                                                        }
                                                        catch
                                                        {
                                                            objDetail.IsSuccess = false;
                                                            lstError.Add("Giá từ của loại [" + pLevelCode + "] không chính xác");
                                                        }
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
                                }
                                else
                                {
                                    while (col <= worksheet.Dimension.End.Column)
                                    {
                                        levelCode = ExcelHelper.GetValue(worksheet, row, col);
                                        if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
                                        if (!string.IsNullOrEmpty(levelCode))
                                        {
                                            var checkLeveCode = data.ListPacking.FirstOrDefault(c => c.Code == levelCode);
                                            if (checkLeveCode == null) throw new Exception("Container [" + levelCode + "] không tồn tại");
                                            else
                                            {
                                                dictColLevel.Add(col, checkLeveCode.ID);
                                                dictColLevelCode.Add(col, checkLeveCode.Code);
                                            }
                                        }
                                        else break;
                                        col += 1;
                                    }

                                    row = 2;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        DTOPriceCOContainerImport obj = new DTOPriceCOContainerImport();
                                        obj.ListDetail = new List<DTOPriceCOContainerDetail>();
                                        obj.ExcelRow = row;
                                        obj.ExcelSuccess = true;
                                        obj.ExcelError = string.Empty;
                                        col = 2;
                                        string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

                                        Input = ExcelHelper.GetValue(worksheet, row, col);
                                        //neu 2 cot dau rong thì thoat
                                        if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

                                        var checkRoute = data.ListRouting.FirstOrDefault(c => c.Code == Input);
                                        if (checkRoute == null)
                                        {
                                            lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
                                            obj.ExcelSuccess = false;
                                        }
                                        else
                                        {
                                            obj.RouteID = checkRoute.ID;
                                            obj.RouteCode = checkRoute.Code;
                                            obj.RouteName = checkRoute.RoutingName;
                                        }

                                        if (dictColLevel.Keys.Count > 0)
                                        {
                                            foreach (var word in dictColLevel)
                                            {
                                                int pCol = word.Key;
                                                int pLevel = word.Value;
                                                string pLevelCode = "";
                                                dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

                                                string price = ExcelHelper.GetValue(worksheet, row, pCol);


                                                if (!string.IsNullOrEmpty(price))
                                                {
                                                    DTOPriceCOContainerDetail objDetail = new DTOPriceCOContainerDetail();
                                                    objDetail.IsSuccess = true;
                                                    objDetail.ContractRoutingID = obj.RouteID;
                                                    objDetail.PackingID = pLevel;
                                                    objDetail.PriceMax = null;
                                                    objDetail.PriceMin = null;
                                                    try
                                                    {
                                                        objDetail.Price = Convert.ToDecimal(price);
                                                    }
                                                    catch
                                                    {
                                                        objDetail.IsSuccess = false;
                                                        lstError.Add("Giá của loại[" + pLevelCode + "] không chính xác");
                                                    }
                                                    obj.ListDetail.Add(objDetail);
                                                }

                                            }
                                        }

                                        if (lstError.Count > 0)
                                            obj.ExcelError = string.Join(" ,", lstError);
                                        result.Add(obj);
                                        row++;
                                    }
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

        [HttpPost]
        public void VENPrice_CO_GroupContainer_ExcelImport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceCOContainerImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceCOContainerImport>>(dynParam.lst.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
               {
                   sv.VENPrice_CO_COContainer_ExcelImport(lst, priceID);
               });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel VENPrice_CO_GroupContainer_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;

                var result = default(SYSExcel);

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_GroupContainer_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row VENPrice_CO_GroupContainer_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_CO_GroupContainer_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
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
        public SYSExcel VENPrice_CO_GroupContainer_ExcelOnImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var contractTermID = (int)dynParam.contractTermID;
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_CO_GroupContainer_ExcelOnImport(isFrame, priceID, contractTermID, id, lst[0].Rows, lstMessageError);
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
        public bool VENPrice_CO_GroupContainer_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;
                var isFrame = (bool)dynParam.isFrame;
                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVVendor((ISVVendor sv) =>
                    {
                        result = sv.VENPrice_CO_GroupContainer_ExcelApprove(isFrame, priceID, contractTermID, id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PriceServiceCO

        [HttpPost]
        public DTOResult VENPrice_CO_Service_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Service_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_CO_ServicePacking_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_ServicePacking_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATPriceCOService VENPrice_CO_Service_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOCATPriceCOService();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_Service_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATPriceCOService VENPrice_CO_ServicePacking_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOCATPriceCOService();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_ServicePacking_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int VENPrice_CO_Service_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                DTOCATPriceCOService item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPriceCOService>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    item.ID = sv.VENPrice_CO_Service_Save(item, priceID);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENPrice_CO_Service_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENPrice_CO_Service_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_CO_CATService_List()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_CATService_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_CO_CATServicePacking_List()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_CATServicePacking_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENPrice_CO_CATCODefault_List(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                var result = new DTOResult();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENPrice_CO_CATCODefault_List(contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region CUSAddRouting

        [HttpPost]
        public DTOResult VENContract_NewRouting_LocationList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_LocationList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATRouting VENContract_NewRouting_Get(dynamic dynParam)
        {
            try
            {
                DTOCATRouting result = new DTOCATRouting();
                int ID = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_Get(ID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_NewRouting_AreaList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATRoutingArea VENContract_NewRouting_AreaGet(dynamic dynParam)
        {
            try
            {
                CATRoutingArea result = new CATRoutingArea();
                int ID = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaGet(ID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_NewRouting_AreaDetailList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                int areaID = (int)dynParam.areaID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaDetailList(request, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int VENContract_NewRouting_Save(dynamic dynParam)
        {
            try
            {
                int result = 0;
                int contractID = (int)dynParam.contractID;
                DTOCATRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRouting>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_Save(item, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATRoutingAreaDetail VENContract_NewRouting_AreaDetailGet(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail result = new DTOCATRoutingAreaDetail();
                int ID = (int)dynParam.ID;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaDetailGet(ID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int VENContract_NewRouting_AreaSave(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                int result = 0;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int VENContract_NewRouting_AreaDetailSave(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                int areaID = (int)dynParam.areaID;
                int result = 0;
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaDetailSave(item, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_NewRouting_AreaDelete(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_NewRouting_AreaDelete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_NewRouting_AreaDetailDelete(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_NewRouting_AreaDetailDelete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void VENContract_NewRouting_AreaRefresh(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_NewRouting_AreaRefresh(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_NewRouting_AreaLocation_List(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaLocation_List(request, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult VENContract_NewRouting_AreaLocationNotIn_List(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    result = sv.VENContract_NewRouting_AreaLocationNotIn_List(request, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_NewRouting_AreaLocationNotIn_Save(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_NewRouting_AreaLocationNotIn_Save(areaID, lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_NewRouting_AreaLocation_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_NewRouting_AreaLocation_Delete(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void VENContract_NewRouting_AreaLocation_Copy(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());

                ServiceFactory.SVVendor((ISVVendor sv) =>
                {
                    sv.VENContract_NewRouting_AreaLocation_Copy(areaID, lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}