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
using System.Globalization;

namespace ClientWeb
{
    public class CUSController : BaseController
    {
        #region Common
        [HttpPost]
        public DTOResult Customer_AllList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Customer_AllList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Customer

        [HttpPost]
        public List<CATRoutingArea> Customer_Location_RoutingAreaList(dynamic d)
        {
            try
            {
                int locationID = d.locationID;
                var result = default(List<CATRoutingArea>);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Customer_Location_RoutingAreaList(locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public DTOResult Customer_Location_RoutingAreaNotInList(dynamic d)
        {
            try
            {
                int locationID = d.locationID;
                string request = d.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Customer_Location_RoutingAreaNotInList(locationID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Customer_Location_RoutingAreaNotInSave(dynamic d)
        {
            try
            {
                int locationID = d.locationID;
                List<int> lstAreaID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lstAreaID.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Customer_Location_RoutingAreaNotInSave(lstAreaID, locationID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Customer_Location_RoutingAreaNotInDeleteList(dynamic d)
        {
            try
            {
                int locationID = d.locationID;
                List<int> lstAreaID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lstAreaID.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Customer_Location_RoutingAreaNotInDeleteList(lstAreaID, locationID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Customer_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Customer_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CUSCustomer_ApprovedList(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSCustomer_ApprovedList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CUSCustomer_UnApprovedList(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSCustomer_UnApprovedList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CUSCustomer Customer_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(CUSCustomer);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CustomerGetByID(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CUSCustomer Customer_Update(dynamic d)
        {
            try
            {
                CUSCustomer item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSCustomer>(d.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    item.ID = sv.Customer_Save(item);
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Customer_Destroy(dynamic d)
        {
            try
            {
                CUSCustomer item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSCustomer>(d.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Customer_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #region Contact
        public CUSContact Contact_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(CUSContact);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Contact_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Contact_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Contact_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CUSContact Contact_Update(dynamic d)
        {
            try
            {
                CUSContact item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSContact>(d.item.ToString());
                int id = d.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Contact_Save(item, id);
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Contact_Destroy(dynamic d)
        {
            try
            {
                int id = Convert.ToInt32(d.id.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Contact_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Product
        public DTOCUSGroupOfProduct GroupOfProduct_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(DTOCUSGroupOfProduct);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.GroupOfProduct_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult GroupOfProductAll_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                int gopID = d.gopID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.GroupOfProduct_List(request, id);
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

        public DTOResult GroupOfProduct_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.GroupOfProduct_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool GroupOfProduct_Update(dynamic d)
        {
            try
            {
                DTOCUSGroupOfProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSGroupOfProduct>(d.item.ToString());
                int id = d.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.GroupOfProduct_Save(item, id);
                });
                return true;
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
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
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

        private CUSGroupOfProduct GroupOfProduct_GetByCode(string code, int customerID)
        {
            try
            {
                CUSGroupOfProduct result = new CUSGroupOfProduct();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
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
                int id = d.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.GroupOfProduct_ResetPrice(id);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOCUSProduct Product_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(DTOCUSProduct);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Product_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Product_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int gopID = d.gopID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Product_List(request, gopID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Product_Update(dynamic d)
        {
            try
            {
                DTOCUSProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSProduct>(d.item.ToString());
                int gopID = d.gopID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Product_Save(item, gopID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Product_Destroy(dynamic d)
        {
            try
            {
                DTOCUSProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSProduct>(d.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Product_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string CUS_Product_Export(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                List<DTOCUSProduct_Export> resBody = new List<DTOCUSProduct_Export>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    resBody = sv.CUS_Product_Export(customerID);
                });
                string file = "/Uploads/temp/" + "SanPham" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(f))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã nhóm"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên nhóm"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "UOM"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Chiều dài"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Rộng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Cao"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Thể tích(cm3)"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Cân nặng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "isKG ?"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Hàng trả về"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mô tả"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Nhiệt độ tối thiểu"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Nhiệt độ tối đa"; worksheet.Column(col).Width = 15;

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
                    foreach (DTOCUSProduct_Export item in resBody)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.GroupOfProductCode;
                        col++; worksheet.Cells[row, col].Value = item.GroupOfProductName;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = item.ProductName;
                        col++; worksheet.Cells[row, col].Value = item.PackingCode;
                        col++; worksheet.Cells[row, col].Value = item.Length;
                        col++; worksheet.Cells[row, col].Value = item.Width;
                        col++; worksheet.Cells[row, col].Value = item.Height;
                        col++; worksheet.Cells[row, col].Value = item.CBM;
                        col++; worksheet.Cells[row, col].Value = item.Weight;
                        col++; worksheet.Cells[row, col].Value = item.IsKg == true ? "x" : string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.HasReturn == true ? "x" : string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.Description;
                        col++; worksheet.Cells[row, col].Value = item.TempMin;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.TempMax;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
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
        public List<DTOCUSGroupProduct_Import> CUS_Product_Check(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                string file = "/" + dynParam.file.ToString();

                List<DTOCUSGroupProduct_Import> result = new List<DTOCUSGroupProduct_Import>();
                DTOCUSProduct_Data dataCheck = new DTOCUSProduct_Data();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    dataCheck = sv.CUS_Product_Check(customerID);
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

                                int col = 1;

                                col++; string strGroupCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strGroupName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strProductCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strProductName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strPacking = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strLength = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strWidth = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strHeight = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strCBM = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strWeight = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strKg = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strReturn = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strDescription = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strTempMin = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strTempMax = ExcelHelper.GetValue(worksheet, row, col);

                                #region Group
                                //res chua co thi add res, res co roi thi bo qua
                                var objGroup = result.FirstOrDefault(c => c.Code == strGroupCode);
                                if (objGroup == null)
                                {
                                    objGroup = new DTOCUSGroupProduct_Import();
                                    objGroup.ListProduct = new List<DTOCUSProduct_Import>();
                                    objGroup.ExcelRow = row;
                                    objGroup.ExcelSuccess = true;
                                    objGroup.PriceOfGOPID = -1;
                                    result.Add(objGroup);
                                    var checkGroup = dataCheck.lstGOP.FirstOrDefault(c => c.Code == strGroupCode);
                                    if (checkGroup == null)
                                        objGroup.ID = 0;
                                    else
                                        objGroup.ID = checkGroup.ID;
                                    objGroup.Code = strGroupCode;
                                    objGroup.Name = strGroupName;
                                }
                                #endregion

                                #region product
                                List<string> lstError = new List<string>();
                                DTOCUSProduct_Import objProduct = new DTOCUSProduct_Import();
                                objProduct.ExcelRow = row;
                                objProduct.ExcelSuccess = true;
                                var checkPro = dataCheck.lstProduct.FirstOrDefault(c => c.GroupOfProductID == objGroup.ID && c.Code == strProductCode);
                                if (checkPro == null)
                                    objProduct.ID = 0;
                                else
                                    objProduct.ID = checkPro.ID;
                                objProduct.Code = strProductCode;
                                objProduct.ProductName = strProductName;

                                if (objGroup.ListProduct.Count(c => c.Code == strProductCode) > 0)// có sp cung mã thi báo lỗi
                                    lstError.Add("Mã hàng[" + strProductCode + "] đã tồn tại trong nhóm hàng");

                                objGroup.ListProduct.Add(objProduct);

                                //check UOM
                                objProduct.PackingCode = strPacking;

                                var packing = dataCheck.lstPackingGOP.FirstOrDefault(c => c.Code == strPacking);
                                if (packing == null)
                                    lstError.Add("Mã UOM[" + strPacking + "]Không tồn tại");
                                else
                                {
                                    objProduct.PackingID = packing.ID;
                                    if (objGroup.PriceOfGOPID < 0)
                                    {
                                        switch (packing.TypeOfVar)
                                        {
                                            case -(int)SYSVarType.TypeOfPackingGOPTon:
                                                objGroup.PriceOfGOPID = -(int)SYSVarType.PriceOfGOPTon;
                                                break;
                                            case -(int)SYSVarType.TypeOfPackingGOPCBM:
                                                objGroup.PriceOfGOPID = -(int)SYSVarType.PriceOfGOPCBM;
                                                break;
                                            default:
                                                objGroup.PriceOfGOPID = -(int)SYSVarType.PriceOfGOPTU;
                                                break;
                                        }
                                    }
                                }
                                //chieu dai
                                if (string.IsNullOrEmpty(strLength))
                                    objProduct.Length = null;
                                else
                                {
                                    try
                                    {
                                        objProduct.Length = Convert.ToDouble(strLength);
                                    }
                                    catch
                                    {
                                        lstError.Add("Chiều dài[" + strLength + "] không chính xác");
                                    }
                                }
                                //chieu rong
                                if (string.IsNullOrEmpty(strWidth))
                                    objProduct.Width = null;
                                else
                                {
                                    try
                                    {
                                        objProduct.Width = Convert.ToDouble(strWidth);
                                    }
                                    catch
                                    {
                                        lstError.Add("Chiều rộng[" + strWidth + "] không chính xác");
                                    }
                                }
                                //chieu cao
                                if (string.IsNullOrEmpty(strHeight))
                                    objProduct.Height = null;
                                else
                                {
                                    try
                                    {
                                        objProduct.Height = Convert.ToDouble(strHeight);
                                    }
                                    catch
                                    {
                                        lstError.Add("Chiều cao[" + strHeight + "] không chính xác");
                                    }
                                }
                                //the tich
                                if (string.IsNullOrEmpty(strCBM))
                                    objProduct.CBM = null;
                                else
                                {
                                    try
                                    {
                                        objProduct.CBM = Convert.ToDouble(strCBM);
                                    }
                                    catch
                                    {
                                        lstError.Add("Thể tích[" + strCBM + "] không chính xác");
                                    }
                                }
                                //can nang
                                if (string.IsNullOrEmpty(strWeight))
                                    objProduct.Weight = null;
                                else
                                {
                                    try
                                    {
                                        objProduct.Weight = Convert.ToDouble(strWeight);
                                    }
                                    catch
                                    {
                                        lstError.Add("Cân nặng[" + strWeight + "] không chính xác");
                                    }
                                }
                                //nhiệt độ tối thiểu
                                if (string.IsNullOrEmpty(strTempMin))
                                    objProduct.TempMin = null;
                                else
                                {
                                    try
                                    {
                                        objProduct.TempMin = Convert.ToDouble(strTempMin);
                                    }
                                    catch
                                    {
                                        lstError.Add("nhiệt độ tối thiểu[" + strTempMin + "] không chính xác");
                                    }
                                }

                                objProduct.Description = strDescription;

                                //nhiệt độ tối đa
                                if (string.IsNullOrEmpty(strTempMax))
                                    objProduct.TempMax = null;
                                else
                                {
                                    try
                                    {
                                        objProduct.TempMax = Convert.ToDouble(strTempMax);
                                    }
                                    catch
                                    {
                                        lstError.Add("nhiệt độ tối đa[" + strTempMax + "] không chính xác");
                                    }
                                }
                                //isKg
                                objProduct.IsKg = strKg == "x" ? true : false;

                                //check loi
                                objProduct.ExcelSuccess = lstError.Count > 0 ? false : true;
                                objProduct.ExcelError = string.Join(" ,", lstError);

                                // hàng trả về
                                if (strReturn.ToLower() == "x" && objGroup.HasReturn != null && objProduct.ExcelSuccess)
                                {
                                    objGroup.HasReturn = true;
                                }
                                #endregion
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
        public void CUS_Product_Import(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                List<DTOCUSGroupProduct_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSGroupProduct_Import>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUS_Product_Import(data, customerID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATPackingGOPTU()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATPacking(SYSVarType.TypeOfPackingGOPTU);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CUS_Product_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                int customerid = (int)dynParam.customerid;
                var isreload = (bool)dynParam.isreload;
                var result = default(SYSExcel);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Product_ExcelInit(functionid, functionkey,isreload ,customerid);
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
        public Row CUS_Product_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row; 
                int customerid = (int)dynParam.customerid;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 )
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUS_Product_ExcelChange(id, row, cells, lstMessageError, customerid);
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
        public SYSExcel CUS_Product_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                int customerid = (int)dynParam.customerid;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUS_Product_ExcelImport(id, lst[0].Rows, lstMessageError, customerid);
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
        public bool CUS_Product_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                int customerid = (int)dynParam.customerid;
                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUS_Product_ExcelApprove(id, customerid);
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

        #region Stock
        public DTOResult Stock_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Stock_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult StockNotIn_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.StockNotIn_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSStock Stock_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                int stockID = d.stockID;
                var result = default(DTOCUSStock);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Stock_GetByID(stockID, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSStock Stock_Update(dynamic d)
        {
            try
            {
                int id = d.id;
                DTOCUSStock item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSStock>(d.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    item = sv.Stock_Save(item, id);
                    AddressSearchHelper.Update(sv.AddressSearch_List(item.ID));
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Stock_SaveList(dynamic d)
        {
            try
            {
                int id = d.id;
                List<DTOCUSLocation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSLocation>>(d.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Stock_SaveList(lst, id);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int Stock_Destroy(dynamic d)
        {
            try
            {
                DTOCUSStock item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSStock>(d.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Stock_Delete(item);
                    AddressSearchHelper.Delete(sv.AddressSearch_List(item.LocationID));
                });
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Stock_DeleteList(dynamic d)
        {
            try
            {

                List<DTOCUSStock> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSStock>>(d.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Stock_DeleteList(lst);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult GroupOfProductInStock_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int stockID = d.stockID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.GroupOfProductInStock_List(request, stockID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GroupOfProductInStock_DeleteList(dynamic d)
        {
            try
            {
                List<int> lstGroupID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lstGroupID.ToString());
                int stockID = d.stockID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.GroupOfProductInStock_DeleteList(lstGroupID, stockID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult GroupOfProductNotInStock_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int stockID = d.stockID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.GroupOfProductNotInStock_List(request, stockID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool GroupOfProductNotInStock_SaveList(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int stockID = d.stockID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.GroupOfProductNotInStock_SaveList(lst, stockID);
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

        public DTOResult Routing_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Routing_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Routing_Delete(dynamic d)
        {
            try
            {
                DTOCUSRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSRouting>(d.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
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

        public DTOResult RoutingNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.RoutingNotIn_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RoutingNotIn_SaveList(dynamic d)
        {
            try
            {
                List<DTOCATRouting> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATRouting>>(d.lst.ToString());
                int id = d.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.RoutingNotIn_SaveList(lst, id);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Routing_Reset(dynamic d)
        {
            try
            {
                int id = d.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Routing_Reset(id);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Đối tác
        public DTOResult Partner_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                int partnerType = d.partnerType;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Partner_List(request, id, partnerType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult PartnerNotIn_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int id = dynParam.id;
                int typePartner = (int)dynParam.typePartner;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.PartnerNotIn_List(request, id, typePartner);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public DTOCUSPartnerAllCustom Partner_Get(dynamic d)
        //{
        //    try
        //    {
        //        int partnerID = d.partnerID;
        //        var result = default(DTOCUSPartnerAllCustom);
        //        ServiceFactory.SVCustomer((ISVCustomer sv) =>
        //        {
        //            result = sv.Partner_GetByID(partnerID);
        //        });
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public int Partner_Save(dynamic dynParam)
        {
            try
            {
                DTOCUSPartnerAllCustom item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPartnerAllCustom>(dynParam.item.ToString());
                int customerid = (int)dynParam.customerid;
                int typePartner = (int)dynParam.typePartner;
                if (typePartner != 1 && typePartner != 2 && typePartner != 3)
                    throw new Exception("Loại đối tác không đúng");

                int result = -1;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Partner_Save(item, customerid, typePartner);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Partner_SaveList(dynamic d)
        {
            try
            {
                List<DTOCUSPartnerAll> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSPartnerAll>>(d.lst.ToString());
                int id = d.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Partner_SaveList(lst, id);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Partner_Delete(dynamic dynParam)
        {
            try
            {
                int cuspartnerid = (int)dynParam.cuspartnerid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Partner_Delete(cuspartnerid);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PartnerLocation_List(dynamic dynParam)
        {
            try
            {
                int partnerID = dynParam.partnerID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.PartnerLocation_List(request,partnerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult PartnerLocation_NotInList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int partnerID = d.partnerID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.PartnerLocation_NotInList(request, partnerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PartnerLocation_SaveNotinList(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int partnerID = d.partnerID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.PartnerLocation_SaveNotinList(lst, partnerID);
                });

                var lstAddress = new List<AddressSearchItem>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    lstAddress = sv.AddressSearch_List();
                });
                lstAddress = lstAddress.Where(c => c.Address != null && c.PartnerCode != null).ToList();
                AddressSearchHelper.Create(lstAddress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOCUSPartnerLocation PartnerLocation_Get(dynamic dynParam)
        {
            try
            {
                int locationID = dynParam.locationID;
                var result = default(DTOCUSPartnerLocation);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.PartnerLocation_Get(locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void PartnerLocation_SaveList(dynamic d)
        {
            try
            {
                List<DTOCUSLocation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSLocation>>(d.lst.ToString());
                int cuspartnerID = d.cuspartnerID;
                List<DTOCUSLocation> result = new List<DTOCUSLocation>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.PartnerLocation_SaveList(lst, cuspartnerID);
                    foreach (var item in result)
                    {
                        switch (item.StatusAddressSearch)
                        {
                            default:
                                break;
                            case 1: AddressSearchHelper.Update(sv.AddressSearch_List(item.ID)); break;
                            case 2: AddressSearchHelper.Delete(new AddressSearchItem { CUSLocationID = item.ID }); break;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int PartnerLocation_Save(dynamic dynParam)
        {
            try
            {
                DTOCUSPartnerLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPartnerLocation>(dynParam.item.ToString());
                int cuspartnerID = dynParam.cuspartnerID;
                DTOCUSPartnerLocation result = default(DTOCUSPartnerLocation);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.PartnerLocation_Save(item, cuspartnerID);

                    AddressSearchHelper.Update(sv.AddressSearch_List(result.ID));
                });
                return result.LocationID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool PartnerLocation_Destroy(dynamic d)
        {
            try
            {
                DTOCUSLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSLocation>(d.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.PartnerLocation_Delete(item);
                    AddressSearchHelper.Delete(sv.AddressSearch_List(item.ID));
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string CUS_PartnerLocation_Export(dynamic dynParam)
        {
            try
            {

                int customerID = (int)dynParam.customerID;
                bool isCarrier = dynParam.isCarrier;
                bool isSeaport = dynParam.isSeaport;
                bool isDistributor = dynParam.isDistributor;
                List<DTOPartnerLocation_Excel> resBody = new List<DTOPartnerLocation_Excel>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    resBody = sv.PartnerLocation_Export(customerID, isCarrier, isSeaport, isDistributor);
                });
                string file = "/Uploads/temp/" + "DiaDiem_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(f))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên NPP"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Loại NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ NPP"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã địa chỉ giao hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên địa chỉ giao hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ giao hàng"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Quận/Huyện"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Khu công nghiệp"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "SĐT"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Fax"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Email"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Kinh độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Vĩ độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã khu vực"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên khu vực"; worksheet.Column(col).Width = 15;

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
                    foreach (DTOPartnerLocation_Excel item in resBody)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.PartnerCode;
                        col++; worksheet.Cells[row, col].Value = item.PartnerName;
                        col++; worksheet.Cells[row, col].Value = item.PartnerGroupName;
                        col++; worksheet.Cells[row, col].Value = item.PartnerAddress;
                        col++; worksheet.Cells[row, col].Value = item.LocationCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationName;
                        col++; worksheet.Cells[row, col].Value = item.LocationAddress;
                        col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                        col++; worksheet.Cells[row, col].Value = item.DistrictName;
                        col++; worksheet.Cells[row, col].Value = item.Economiczone;
                        col++; worksheet.Cells[row, col].Value = item.TelNo;
                        col++; worksheet.Cells[row, col].Value = item.Fax;
                        col++; worksheet.Cells[row, col].Value = item.Email;
                        col++; worksheet.Cells[row, col].Value = item.Lng;
                        col++; worksheet.Cells[row, col].Value = item.Lat;
                        col++; worksheet.Cells[row, col].Value = item.RoutingAreaCode;
                        col++; worksheet.Cells[row, col].Value = item.RoutingAreaName;
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
        public List<DTOPartnerImport> CUS_PartnerLocation_Check(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                bool isCarrier = dynParam.isCarrier;
                bool isSeaport = dynParam.isSeaport;
                bool isDistributor = dynParam.isDistributor;
                string file = "/" + dynParam.file.ToString();
                int iTypeOfPartner = 0;
                if (isCarrier)
                    iTypeOfPartner = -(int)SYSVarType.TypeOfPartnerCarrier;
                else if (isSeaport)
                    iTypeOfPartner = -(int)SYSVarType.TypeOfPartnerSeaPort;
                else iTypeOfPartner = -(int)SYSVarType.TypeOfPartnerDistributor;

                if (iTypeOfPartner == 0) throw new Exception("Loại đối tác không chính xác");

                List<DTOPartnerImport> result = new List<DTOPartnerImport>();
                DTOPartnerLocation_Check lstCheck = new DTOPartnerLocation_Check();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    lstCheck = sv.PartnerLocation_Check(customerID);
                });
                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {
                            string CusPartCode, CusPartName, GOP, CusPartAddress, CusLoCode, CusLoName, CusLoAddress, District, Province, EZone, TellNo, Fax, Email, Lat, Lng;
                            int row = 1, col = 1;
                            Dictionary<string, int> dicPartnerCode_Row = new Dictionary<string, int>();
                            for (row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                CusPartCode = ExcelHelper.GetValue(worksheet, row, 2);
                                CusPartName = ExcelHelper.GetValue(worksheet, row, 3);
                                GOP = ExcelHelper.GetValue(worksheet, row, 4);
                                CusPartAddress = ExcelHelper.GetValue(worksheet, row, 5);
                                CusLoCode = ExcelHelper.GetValue(worksheet, row, 6);
                                CusLoName = ExcelHelper.GetValue(worksheet, row, 7);
                                CusLoAddress = ExcelHelper.GetValue(worksheet, row, 8);
                                District = ExcelHelper.GetValue(worksheet, row, 10);
                                Province = ExcelHelper.GetValue(worksheet, row, 9);
                                EZone = ExcelHelper.GetValue(worksheet, row, 11);
                                TellNo = ExcelHelper.GetValue(worksheet, row, 12);
                                Fax = ExcelHelper.GetValue(worksheet, row, 13);
                                Email = ExcelHelper.GetValue(worksheet, row, 14);
                                Lat = ExcelHelper.GetValue(worksheet, row, 15);
                                Lng = ExcelHelper.GetValue(worksheet, row, 16);

                                if (string.IsNullOrEmpty(CusPartCode) && string.IsNullOrEmpty(CusPartName))
                                    break;

                                List<string> lstError = new List<string>();
                                List<string> lstErrorLo = new List<string>();
                                #region check partner

                                var objPartner = result.FirstOrDefault(c => c.CUSPartnerCode == CusPartCode);
                                if (objPartner == null)
                                {
                                    objPartner = new DTOPartnerImport();
                                    objPartner.CUSPartnerCode = CusPartCode;
                                    objPartner.CUSPartnerName = CusPartName;
                                    objPartner.CUSPartnerAddress = CusPartAddress;
                                    objPartner.ExcelRow = row;
                                    objPartner.ListLocation = new List<DTOCUSParterLocationImport>();
                                    objPartner.TellNo = TellNo;
                                    objPartner.Fax = Fax;
                                    objPartner.Email = Email;
                                    objPartner.ExcelSuccess = true;

                                    if (string.IsNullOrEmpty(CusPartCode))
                                        lstError.Add("Mã NPP không được trống");
                                    if (string.IsNullOrEmpty(CusPartName))
                                        lstError.Add("Tên NPP không được trống");

                                    var checkCUSPart = lstCheck.lstPartnerOfCustomer.FirstOrDefault(c => c.PartnerCode == CusPartCode);
                                    if (checkCUSPart == null)
                                    {
                                        objPartner.CUSPartnerID = -1;
                                        var checkCATPart = lstCheck.lstCatPartner.FirstOrDefault(c => c.Code == CusPartCode && c.TypeOfPartnerID == iTypeOfPartner);
                                        if (checkCATPart == null) objPartner.CATParterID = -1;
                                        else objPartner.CATParterID = checkCATPart.ID;
                                    }
                                    else
                                    {
                                        objPartner.CUSPartnerID = checkCUSPart.ID;
                                        objPartner.CATParterID = checkCUSPart.PartnerID;
                                    }

                                    if (string.IsNullOrEmpty(Province))
                                    {
                                        objPartner.CountryID = null; objPartner.ProvinceID = null;
                                    }
                                    else
                                    {
                                        var checkPro = lstCheck.lstProvince.FirstOrDefault(c => c.ProvinceName == Province);
                                        if (checkPro != null)
                                        {
                                            objPartner.CountryID = checkPro.CountryID;
                                            objPartner.ProvinceID = checkPro.ID;
                                        }
                                        else lstError.Add("Tỉnh thành [" + Province + "]không chính xác");
                                    }
                                    if (string.IsNullOrEmpty(District))
                                    {
                                        objPartner.ProvinceID = null;
                                    }
                                    else
                                    {
                                        var checkPro = lstCheck.lstDistrict.FirstOrDefault(c => c.DistrictName == District);
                                        if (checkPro != null)
                                        {
                                            if (objPartner.ProvinceID == null)
                                                lstError.Add("Phải chọn tỉnh thành trước");
                                            else objPartner.DistrictID = checkPro.ID;
                                        }
                                        else lstError.Add("Quận huyện [" + District + "]không chính xác");
                                    }

                                    if (!string.IsNullOrEmpty(GOP))
                                    {
                                        var checkGOP = lstCheck.lstGroupOfPartner.FirstOrDefault(c => c.Code == GOP);
                                        if (checkGOP == null)
                                            lstError.Add("Loại NPP[" + GOP + "] không tồn tại");
                                        else
                                        {
                                            objPartner.GroupOfPartnerID = checkGOP.ID;
                                        }
                                    }
                                    else
                                    {
                                        objPartner.GroupOfPartnerID = null;
                                    }

                                    objPartner.ExcelError = string.Join(", ", lstError);
                                    objPartner.ExcelSuccess = (lstError.Count == 0) ? true : false;
                                    result.Add(objPartner);
                                }



                                #endregion

                                #region check location
                                //rong code Location thi chi có NPP
                                if (string.IsNullOrEmpty(CusLoCode))
                                {
                                    DTOCUSParterLocationImport objLo = new DTOCUSParterLocationImport();
                                    objLo.CATLocationID = -1;
                                    objLo.CUSLocationID = -1;
                                    objLo.CUSLocationCode = string.Empty;
                                    objLo.ExcelRow = row;
                                    objLo.ExcelSuccess = false;
                                    objLo.ExcelError = string.Join(" ,", lstError);
                                    objPartner.ListLocation.Add(objLo);
                                }
                                else
                                {
                                    DTOCUSParterLocationImport objLo = new DTOCUSParterLocationImport();
                                    objLo.ExcelRow = row;
                                    var checkCUSLo = lstCheck.lstLocationOfCustomer.FirstOrDefault(c => c.Code == CusLoCode);
                                    if (checkCUSLo == null)
                                    {
                                        objLo.CUSLocationID = -1;
                                        var checkCATLo = lstCheck.lstCatLocation.FirstOrDefault(c => c.Code == CusLoCode);
                                        if (checkCATLo == null) objLo.CATLocationID = -1;
                                        else objLo.CATLocationID = checkCATLo.ID;
                                    }
                                    else
                                    {
                                        if (checkCUSLo.CusPartID == null)
                                        {
                                            lstError.Add("Mã địa chỉ giao hàng trùng với mã kho của khách hàng này");
                                        }
                                        else
                                        {
                                            if (checkCUSLo.CusPartID != objPartner.CUSPartnerID)
                                                objLo.CUSLocationID = checkCUSLo.ID;
                                            else objLo.CUSLocationID = checkCUSLo.ID;
                                            objLo.CATLocationID = checkCUSLo.LocationID;
                                        }
                                    }

                                    var checkOnFile = objPartner.ListLocation.FirstOrDefault(c => c.CUSLocationCode == CusLoCode);
                                    if (checkOnFile != null && !string.IsNullOrEmpty(objPartner.CUSPartnerCode))
                                        lstError.Add("Mã địa chỉ giao hàng [" + CusLoCode + "] bị trùng cho partner [" + objPartner.CUSPartnerCode + "]");

                                    objLo.CUSLocationCode = CusLoCode;
                                    objLo.CUSLocationName = CusLoName;
                                    objLo.CUSLocationAddress = CusLoAddress;
                                    objLo.EconomicZone = EZone;

                                    if (string.IsNullOrEmpty(Province))
                                        lstError.Add("Tỉnh thành không được trống");
                                    else
                                    {
                                        var checkPro = lstCheck.lstProvince.FirstOrDefault(c => c.ProvinceName == Province);
                                        if (checkPro != null)
                                        {
                                            objLo.CountryID = checkPro.CountryID;
                                            objLo.ProvinceID = checkPro.ID;
                                        }
                                        else lstError.Add("Tỉnh thành [" + Province + "]không chính xác");
                                    }
                                    if (string.IsNullOrEmpty(District))
                                        lstError.Add("Quận huyện không được trống");
                                    else
                                    {
                                        var checkPro = lstCheck.lstDistrict.FirstOrDefault(c => c.DistrictName == District);
                                        if (checkPro != null)
                                        {
                                            if (objLo.ProvinceID == null)
                                                lstError.Add("Phải chọn tỉnh thành trước");
                                            else objLo.DistrictID = checkPro.ID;
                                        }
                                        else lstError.Add("Quận huyện [" + District + "]không chính xác");
                                    }

                                    if (string.IsNullOrEmpty(Lat))
                                        objLo.Lat = null;
                                    else
                                    {
                                        try
                                        {
                                            objLo.Lat = Convert.ToDouble(Lat);
                                        }
                                        catch
                                        {
                                            lstError.Add("Kinh độ [" + Lat + "] không chính xác");
                                        }
                                    }
                                    if (string.IsNullOrEmpty(Lng))
                                        objLo.Lng = null;
                                    else
                                    {
                                        try
                                        {
                                            objLo.Lng = Convert.ToDouble(Lng);
                                        }
                                        catch
                                        {
                                            lstError.Add("Vĩ độ [" + Lng + "] không chính xác");
                                        }
                                    }
                                    objLo.ExcelSuccess = lstError.Count == 0;
                                    objLo.ExcelError = string.Join(" ,", lstError);
                                    objPartner.ListLocation.Add(objLo);
                                }
                                #endregion
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
        public void CUS_PartnerLocation_Import(dynamic dynParam)
        {
            try
            {
                int customerID = (int)dynParam.customerID;
                bool isCarrier = dynParam.isCarrier;
                bool isSeaport = dynParam.isSeaport;
                bool isDistributor = dynParam.isDistributor;
                List<DTOPartnerImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPartnerImport>>(dynParam.data.ToString());
                var addressdata = new List<AddressSearchItem>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    addressdata = sv.PartnerLocation_Import(data, customerID, isCarrier, isSeaport, isDistributor);
                });
                foreach (var item in addressdata)
                {
                    AddressSearchHelper.Update(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region excel mới
        [HttpPost]
        public SYSExcel CUS_Partner_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int customerid = (int)dynParam.customerid;

                var result = default(SYSExcel);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_ExcelInit(functionid, functionkey, isreload, customerid);
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
        public Row CUS_Partner_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int customerid = (int)dynParam.customerid;

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = result = sv.CUS_Partner_ExcelChange(id, row, cells, lstMessageError, customerid);
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
        public SYSExcel CUS_Partner_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                int customerid = (int)dynParam.customerid;

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUS_Partner_ExcelImport(id, lst[0].Rows, lstMessageError, customerid);
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
        public bool CUS_Partner_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                int customerid = (int)dynParam.customerid;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUS_Partner_ExcelApprove(id, customerid);
                        if (result)
                            AddressSearchHelper.AddListByCustomerID(customerid, sv.AddressSearch_ByCustomerList(customerid));
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

        [HttpPost]
        public List<DTOCUSPartnerLocationAll> CUS_Partner_List(dynamic dynParam)
        {
            try
            {
                int customerid = (int)dynParam.customerid;
                List<int> lstPartner = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstPartner.ToString());
                List<int> lstLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstLocation.ToString());
                bool isUseLocation = Convert.ToBoolean(dynParam.isUseLocation.ToString());

                List<DTOCUSPartnerLocationAll> result = new List<DTOCUSPartnerLocationAll>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_List(customerid, lstPartner, lstLocation, isUseLocation);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSPartnerAllCustom CUS_Partner_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int typepartnerid = (int)dynParam.typepartnerid;
                DTOCUSPartnerAllCustom result = new DTOCUSPartnerAllCustom();

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_Get(id, typepartnerid);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CUS_Partner_CUSLocationSaveCode(dynamic dynParam)
        {
            try
            {
                DTOCUSPartnerLocationAll item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPartnerLocationAll>(dynParam.item.ToString());
                int result = -1;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_CUSLocationSaveCode(item);
                    if (string.IsNullOrEmpty(item.CUSCode))
                    {
                        AddressSearchHelper.Delete(new AddressSearchItem { CUSLocationID = result });
                    }
                    else
                    {
                        AddressSearchHelper.Update(sv.AddressSearch_List(result));
                    }
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region filter by partner
        [HttpPost]
        public DTOResult CUS_Partner_FilterByPartner_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int customerid = dynParam.customerid;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_FilterByPartner_List(request, customerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUS_Partner_FilterByLocation_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int customerid = dynParam.customerid;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_FilterByLocation_List(request, customerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<int> CUS_Partner_FilterByPartner_GetNumOfCusLocation(dynamic dynParam)
        {
            try
            {
                int customerid = (int)dynParam.customerid;
                List<int> result = new List<int>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_FilterByPartner_GetNumOfCusLocation(customerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region partner routing
        [HttpPost]
        public DTOResult CUS_Partner_RoutingContract_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int customerid = (int)dynParam.customerid;
                int locationid = (int)dynParam.locationid;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_RoutingContract_List(request, customerid, locationid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUS_Partner_RoutingContract_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> lstClear = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstClear.ToString());
                List<int> lstAdd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstAdd.ToString());
                int locationid = (int)dynParam.locationid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                     sv.CUS_Partner_RoutingContract_SaveList(lstClear, lstAdd, locationid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUS_Partner_RoutingContract_NewRoutingSave(dynamic dynParam)
        {
            try
            {
                DTOCUSPartnerNewRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPartnerNewRouting>(dynParam.item.ToString());
                int customerid = (int)dynParam.customerid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUS_Partner_RoutingContract_NewRoutingSave(item,customerid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUS_Partner_RoutingContract_NewAreaSave(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                int locationid = (int)dynParam.locationid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUS_Partner_RoutingContract_NewAreaSave(item, locationid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUS_Partner_RoutingContract_AreaList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_RoutingContract_AreaList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSPartnerNewRouting CUS_Partner_RoutingContract_NewRoutingGet(dynamic dynParam)
        {
            try
            {
                int customerid = (int)dynParam.customerid;
                var result = default(DTOCUSPartnerNewRouting);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_RoutingContract_NewRoutingGet(customerid);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOCATContract> CUS_Partner_RoutingContract_ContractData(dynamic dynParam)
        {
            try
            {
                int customerid = (int)dynParam.customerid;
                var result = default(List<DTOCATContract>);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_RoutingContract_ContractData(customerid);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATDock
        [HttpPost]
        public DTOResult CUS_Partner_StockDock_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int locationid = (int)dynParam.locationid;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_StockDock_List(request,locationid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void CUS_Partner_StockDock_Save(dynamic dynParam)
        {
            try
            {
                DTOCATDock item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATDock>(dynParam.item.ToString());
                int locationid = (int)dynParam.locationid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUS_Partner_StockDock_Save(item, locationid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUS_Partner_StockDock_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATDock item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATDock>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUS_Partner_StockDock_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATDock CUS_Partner_StockDock_Get(dynamic dynParam)
        {
            try
            {
                var result = default(DTOCATDock);
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_StockDock_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region partner location routing
        [HttpPost]
        public DTOResult CUS_Partner_RoutingLocationContract_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int customerid = (int)dynParam.customerid;
                int locationid = (int)dynParam.locationid;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_RoutingLocationContract_List(request, customerid, locationid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        [HttpPost]
        public void CUS_Partner_RoutingLocationContract_NewRoutingSave(dynamic dynParam)
        {
            try
            {
                DTOCUSPartnerNewRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPartnerNewRouting>(dynParam.item.ToString());
                int customerid = (int)dynParam.customerid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUS_Partner_RoutingLocationContract_NewRoutingSave(item, customerid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        [HttpPost]
        public DTOResult CUS_Partner_RoutingContract_LocationList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int customerid = (int)dynParam.customerid;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_RoutingContract_LocationList(request, customerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSPartnerNewRouting CUS_Partner_RoutingLocationContract_NewRoutingGet(dynamic dynParam)
        {
            try
            {
                int customerid = (int)dynParam.customerid;
                var result = default(DTOCUSPartnerNewRouting);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUS_Partner_RoutingLocationContract_NewRoutingGet(customerid);
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


        #region Thiết lập
        [HttpPost]
        public CUSSettingsCutOffTimeSuggest Customer_Setting_Get(dynamic dynParam)
        {
            try
            {
                int customerid = dynParam.customerid;
                var result = default(CUSSettingsCutOffTimeSuggest);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.Customer_Setting_Get(customerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Customer_Setting_Save(dynamic dynParam)
        {
            try
            {

                int customerid = (int)dynParam.customerid;
                CUSCustomer item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSCustomer>(dynParam.item.ToString());
                CUSSettingsCutOffTimeSuggest setting = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsCutOffTimeSuggest>(dynParam.setting.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Customer_Setting_Save(customerid, item, setting);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Customer_Generate_LocationArea(dynamic dynParam)
        {
            try
            {
                int customerid = (int)dynParam.customerid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Customer_Generate_LocationArea(customerid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void DropdownList_Read_Create(List<DTOCUSGroupOfProduct> lstTarget, IEnumerable<DTOCUSGroupOfProduct> lstSource, int? parentid, int level)
        {
            foreach (var item in lstSource.Where(c => c.ParentID == parentid))
            {
                item.GroupName = new string('.', 3 * level) + item.GroupName;
                lstTarget.Add(item);
                DropdownList_Read_Create(lstTarget, lstSource, item.ID, level + 1);
            }
        }

        #region SettingInfo

        [HttpPost]
        public DTOCUSSettingInfo CUSSettingInfo_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOCUSSettingInfo();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingInfo_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingInfo_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingInfo item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingInfo>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingInfo_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSSettingInfo_LocationList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingInfo_LocationList(request);
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

        #region CUSContract
        [HttpPost]
        public DTOResult CUSContract_ByCustomerList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                int customerID = (int)dynParam.customerID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ByCustomerList(request, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_ByCompanyList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                int customerID = (int)dynParam.customerID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ByCompanyList(request, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CUSAddRouting

        [HttpPost]
        public DTOResult CUSContract_NewRouting_LocationList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_LocationList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATRouting CUSContract_NewRouting_Get(dynamic dynParam)
        {
            try
            {
                DTOCATRouting result = new DTOCATRouting();
                int ID = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_Get(ID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_NewRouting_AreaList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATRoutingArea CUSContract_NewRouting_AreaGet(dynamic dynParam)
        {
            try
            {
                CATRoutingArea result = new CATRoutingArea();
                int ID = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaGet(ID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_NewRouting_AreaDetailList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                int areaID = (int)dynParam.areaID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaDetailList(request, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CUSContract_NewRouting_Save(dynamic dynParam)
        {
            try
            {
                int result = 0;
                int contractID = (int)dynParam.contractID;
                DTOCATRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRouting>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_Save(item, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATRoutingAreaDetail CUSContract_NewRouting_AreaDetailGet(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail result = new DTOCATRoutingAreaDetail();
                int ID = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaDetailGet(ID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CUSContract_NewRouting_AreaSave(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                int result = 0;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CUSContract_NewRouting_AreaDetailSave(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                int areaID = (int)dynParam.areaID;
                int result = 0;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaDetailSave(item, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_NewRouting_AreaDelete(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_NewRouting_AreaDelete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_NewRouting_AreaDetailDelete(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_NewRouting_AreaDetailDelete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void CUSContract_NewRouting_AreaRefresh(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_NewRouting_AreaRefresh(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CUSSSsettingOrder
        #region Code
        [HttpPost]
        public DTOResult CUSSettingOrderCode_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingOrderCode_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingOrderCode CUSSettingOrderCode_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingOrderCode result = new DTOCUSSettingOrderCode();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingOrderCode_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingOrderCode_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingOrderCode item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingOrderCode>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingOrderCode_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingOrderCode_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingOrderCode_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region new
        [HttpPost]
        public DTOResult CUSSettingOrder_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingOrder_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingOrder CUSSettingOrder_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingOrder result = new DTOCUSSettingOrder();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingOrder_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingOrder_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingOrder>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingOrder_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingOrder_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingOrder>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingOrder_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingStockData CUSSettingORD_StockByCus_List(dynamic dynParam)
        {
            try
            {
                int cusID = dynParam.CusID;
                var result = default(DTOCUSSettingStockData);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingORD_StockByCus_List(cusID);
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

        #region CUSContract

        #region CUSContract_Detail

        [HttpPost]
        public DTOCATContract CUSContract_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATContract);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CUSContract_Save(dynamic dynParam)
        {
            try
            {
                DTOCATContract item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContract>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    item.ID = sv.CUSContract_Save(item);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSContract_Data CUSContract_Data(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                DTOCUSContract_Data result = new DTOCUSContract_Data();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Data(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Common

        [HttpPost]
        public DTOResult CUSContract_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_List(request);
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
        public DTOResult CUSContract_CODefault_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_CODefault_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_CODefault_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_CODefault_NotInList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_CODefault_NotIn_SaveList(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATPacking> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATPacking>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_CODefault_NotIn_SaveList(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_CODefault_Delete(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_CODefault_Delete(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_CODefault_Update(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATContractCODefault> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATContractCODefault>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_CODefault_Update(data, contractID);
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
        public DTOResult CUSContract_Routing_List(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Routing_List(contractID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_Routing_NotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Routing_NotIn_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_Routing_CATNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Routing_CATNotIn_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Routing_CATNotIn_Save(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATRouting>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Routing_CATNotIn_Save(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Routing_NotIn_Delete(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                int contractID = (int)dynParam.contractID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Routing_NotIn_Delete(ID, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_Routing_Save(dynamic dynParam)
        {
            try
            {
                DTOCATContractRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractRouting>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Routing_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Routing_Insert(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCATRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATRouting>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Routing_Insert(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string CUSContract_Routing_LeadTime_Check(dynamic dynParam)
        {
            try
            {
                string expression = dynParam.Expression.ToString();
                DateTime requestDate = (DateTime)dynParam.RequestDate;
                string result = string.Empty;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Routing_LeadTime_Check(expression, requestDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Routing_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Routing_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string CUSContract_Routing_Export(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCUSContractRouting_Import> resBody = new List<DTOCUSContractRouting_Import>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    resBody = sv.CUSContract_Routing_Export(contractID);
                });

                var lstContractRoutingType = new List<SYSVar>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    lstContractRoutingType = sv.ALL_SysVar(SYSVarType.ContractRoutingType).Data.Cast<SYSVar>().ToList(); ;
                });
                string file = FolderUpload.Export + "CungDuong" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/" + file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/" + file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath("~/" + file));
                using (ExcelPackage pk = new ExcelPackage(f))
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
                    foreach (DTOCUSContractRouting_Import item in resBody)
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
        public List<DTOCUSContractRouting_Import> CUSContract_Routing_Excel_Check(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                int customerID = (int)dynParam.customerID;
                string file = "/" + dynParam.file.ToString();

                List<DTOCUSContractRouting_Import> result = new List<DTOCUSContractRouting_Import>();
                DTOCUSContractRoutingData resRouting = new DTOCUSContractRoutingData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    resRouting = sv.CUSContract_RoutingByCus_List(customerID, contractID);
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
                                DTOCUSContractRouting_Import obj = new DTOCUSContractRouting_Import();
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
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        public void CUSContract_Routing_Import(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOCUSContractRouting_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSContractRouting_Import>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Routing_Import(data, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOContractTerm> CUSContract_Routing_ContractTermList(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                List<DTOContractTerm> result = new List<DTOContractTerm>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Routing_ContractTermList(contractID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CUSContract_Routing_ExcelOnline_Init(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var contractID = (int)dynParam.contractID;
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Routing_ExcelOnline_Init(contractID, functionid, functionkey, isreload);
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
        public Row CUSContract_Routing_ExcelOnline_Change(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSContract_Routing_ExcelOnline_Change(contractID, customerID, id, row, cells, lstMessageError);
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
        public SYSExcel CUSContract_Routing_ExcelOnline_Import(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSContract_Routing_ExcelOnline_Import(contractID, customerID, id, lst[0].Rows, lstMessageError);
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
        public bool CUSContract_Routing_ExcelOnline_Approve(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var contractID = (int)dynParam.contractID;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSContract_Routing_ExcelOnline_Approve(id, contractID);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CUSPrice_LoadRoute_Export(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadRoute_Export(contractTermID, priceID);
                });

                string file = "/" + FolderUpload.Export + "ExportCUSContractPrice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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

                    foreach (var itemGroup in result.ListGroupProduct)
                    {
                        col++; worksheet.Cells[row, col].Value = itemGroup.GroupName; worksheet.Column(col).Width = 40;
                        worksheet.Cells[row + 1, col].Value = "Đơn vị tính"; worksheet.Column(col).Width = 20;
                        worksheet.Cells[row + 1, col + 1].Value = "Giá"; worksheet.Column(col).Width = 20;
                        col++;
                    }
                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
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
                                        col++; worksheet.Cells[row, col].Value = itemTruck.PriceOfGOPName;
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
        public DTOResult CUSContract_NewRouting_AreaLocation_List(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaLocation_List(request, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_NewRouting_AreaLocationNotIn_List(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_NewRouting_AreaLocationNotIn_List(request, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_NewRouting_AreaLocationNotIn_Save(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_NewRouting_AreaLocationNotIn_Save(areaID, lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_NewRouting_AreaLocation_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_NewRouting_AreaLocation_Delete(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_NewRouting_AreaLocation_Copy(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_NewRouting_AreaLocation_Copy(areaID, lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region KPI
        [HttpPost]
        public void CUSContract_KPI_Save(dynamic dynParam)
        {
            try
            {
                List<DTOContractKPITime> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractKPITime>>(dynParam.data.ToString());
                int routingID = (int)dynParam.routingID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_KPI_Save(data, routingID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DateTime? CUSContract_KPI_Check_Expression(dynamic dynParam)
        {
            try
            {
                string expression = dynParam.Expression.ToString();
                KPIKPITime item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIKPITime>(dynParam.item.ToString());
                List<DTOContractKPITime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractKPITime>>(dynParam.lst.ToString());
                double leadTime = (double)dynParam.leadTime;
                double zone = (double)dynParam.zone;
                DateTime? result = null;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPI_Check_Expression(expression, item, zone, leadTime, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool? CUSContract_KPI_Check_Hit(dynamic dynParam)
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
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPI_Check_Hit(expression, field, item, zone, leadTime, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_KPI_Routing_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                int routingID = (int)dynParam.routingID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPI_Routing_List(request, contractID, routingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_KPI_Routing_Apply(dynamic dynParam)
        {
            try
            {
                List<DTOCATContractRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATContractRouting>>(dynParam.data.ToString());
                int routingID = (int)dynParam.routingID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_KPI_Routing_Apply(data, routingID);
                });
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
        public DTOResult CUSContract_Price_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int contractID = (int)dynParam.contractID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Price_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOPrice CUSContract_Price_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPrice);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Price_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Price_Copy(dynamic dynParam)
        {
            try
            {
                List<DTOPriceCopyItem> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceCopyItem>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Price_Copy(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CUSContract_Price_Save(dynamic dynParam)
        {
            try
            {
                DTOPrice item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPrice>(dynParam.item.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    item.ID = sv.CUSContract_Price_Save(item, contractTermID);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Price_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Price_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSPrice_Data CUSContract_Price_Data(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                DTOCUSPrice_Data result = new DTOCUSPrice_Data();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Price_Data(contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_Price_DeletePriceNormal(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Price_DeletePriceNormal(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_Price_DeletePriceLevel(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Price_DeletePriceLevel(priceID);
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
        public void CUSPrice_DI_GroupVehicle_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceGroupVehicle> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGroupVehicle>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_GroupVehicle_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public string CUSPrice_DI_GroupVehicle_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                DTOPriceGroupVehicleData data = new DTOPriceGroupVehicleData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    data = sv.CUSPrice_DI_GroupVehicle_ExcelData(priceID, contractTermID);
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
        public List<DTOPriceGroupVehicleImport> CUSPrice_DI_GroupVehicle_ExcelCheck(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSPrice_DI_GroupVehicle_ExcelData(priceID, contractTermID);
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
        public void CUSPrice_DI_GroupVehicle_ExcelImport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceGroupVehicleImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGroupVehicleImport>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_GroupVehicle_ExcelImport(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CUSPrice_DI_GroupVehicle_ExcelInit(dynamic dynParam)
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

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_GroupVehicle_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_GroupVehicle_ExcelChange(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_GroupVehicle_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
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
        public SYSExcel CUSPrice_DI_GroupVehicle_ExcelOnImport(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_GroupVehicle_ExcelOnImport(isFrame, priceID, contractTermID, id, lst[0].Rows, lstMessageError);
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
        public bool CUSPrice_DI_GroupVehicle_ExcelApprove(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_GroupVehicle_ExcelApprove(isFrame, priceID, contractTermID, id);
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
        public List<DTOPriceGVLevelGroupVehicle> CUSPrice_DI_PriceGVLevel_DetailData(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.id;
                List<DTOPriceGVLevelGroupVehicle> result = new List<DTOPriceGVLevelGroupVehicle>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceGVLevel_DetailData(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceGVLevel_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceGVLevelGroupVehicle> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGVLevelGroupVehicle>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceGVLevel_Save(lst, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public string CUSPrice_DI_PriceGVLevel_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                DTOPriceGVLevelData data = new DTOPriceGVLevelData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    data = sv.CUSPrice_DI_PriceGVLevel_ExcelData(priceID, contractTermID);
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
        //[HttpPost]
        //public List<DTOPriceGVLevelImport> CUSPrice_DI_PriceGVLevel_ExcelCheck(dynamic dynParam)
        //{
        //    try
        //    {
        //        CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
        //        int priceID = (int)dynParam.priceID;
        //        int contractTermID = (int)dynParam.contractTermID;
        //        bool isFrame = (bool)dynParam.isFrame;
        //        List<DTOPriceGVLevelImport> result = new List<DTOPriceGVLevelImport>();

        //        if (file != null && !string.IsNullOrEmpty(file.FilePath))
        //        {
        //            DTOPriceGVLevelData data = new DTOPriceGVLevelData();
        //            ServiceFactory.SVCustomer((ISVCustomer sv) =>
        //            {
        //                data = sv.CUSPrice_DI_PriceGVLevel_ExcelData(priceID, contractTermID);
        //            });

        //            using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
        //            {
        //                using (var package = new ExcelPackage(fs))
        //                {
        //                    ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

        //                    int col = 4, row = 1;
        //                    string levelCode = "", Input = "";
        //                    Dictionary<int, int> dictColLevel = new Dictionary<int, int>();
        //                    Dictionary<int, string> dictColLevelCode = new Dictionary<int, string>();
        //                    if (worksheet != null)
        //                    {
        //                        if (isFrame)
        //                        {
        //                            while (col <= worksheet.Dimension.End.Column)
        //                            {
        //                                levelCode = ExcelHelper.GetValue(worksheet, row, col);
        //                                if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
        //                                if (!string.IsNullOrEmpty(levelCode))
        //                                {
        //                                    var checkLeveCode = data.ListLevel.FirstOrDefault(c => c.Code == levelCode);
        //                                    if (checkLeveCode == null) throw new Exception("Bậc giá [" + levelCode + "] không tồn tại");
        //                                    else
        //                                    {
        //                                        dictColLevel.Add(col, checkLeveCode.ID);
        //                                        dictColLevelCode.Add(col, checkLeveCode.Code);
        //                                    }
        //                                }
        //                                else break;
        //                                col += 2;
        //                            }

        //                            row = 3;
        //                            while (row <= worksheet.Dimension.End.Row)
        //                            {
        //                                List<string> lstError = new List<string>();
        //                                DTOPriceGVLevelImport obj = new DTOPriceGVLevelImport();
        //                                obj.ListDetail = new List<DTOPriceGVLevelGroupVehicleExcel>();
        //                                obj.ExcelRow = row;
        //                                obj.ExcelSuccess = true;
        //                                obj.ExcelError = string.Empty;
        //                                col = 2;
        //                                string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

        //                                Input = ExcelHelper.GetValue(worksheet, row, col);
        //                                //neu 2 cot dau rong thì thoat
        //                                if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

        //                                var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
        //                                if (checkRoute == null)
        //                                {
        //                                    lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
        //                                    obj.ExcelSuccess = false;
        //                                }
        //                                else
        //                                {
        //                                    obj.RouteID = checkRoute.ID;
        //                                    obj.RouteCode = checkRoute.Code;
        //                                    obj.RouteName = checkRoute.RoutingName;
        //                                }

        //                                if (dictColLevel.Keys.Count > 0)
        //                                {
        //                                    foreach (var word in dictColLevel)
        //                                    {
        //                                        int pCol = word.Key;
        //                                        int pLevel = word.Value;
        //                                        string pLevelCode = "";
        //                                        dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

        //                                        string priceMin = ExcelHelper.GetValue(worksheet, row, pCol);
        //                                        string priceMax = ExcelHelper.GetValue(worksheet, row, pCol + 1);


        //                                        if (!string.IsNullOrEmpty(priceMin) || !string.IsNullOrEmpty(priceMax))
        //                                        {
        //                                            DTOPriceGVLevelGroupVehicleExcel objDetail = new DTOPriceGVLevelGroupVehicleExcel();
        //                                            objDetail.IsSuccess = true;
        //                                            objDetail.RouteID = obj.RouteID;
        //                                            objDetail.LevelID = pLevel;
        //                                            objDetail.Price = 0;
        //                                            if (string.IsNullOrEmpty(priceMin)) objDetail.PriceMin = null;
        //                                            else
        //                                            {
        //                                                try
        //                                                {
        //                                                    objDetail.PriceMin = Convert.ToDecimal(priceMin);
        //                                                }
        //                                                catch
        //                                                {
        //                                                    objDetail.IsSuccess = false;
        //                                                    lstError.Add("Giá từ của bậc [" + pLevelCode + "] không chính xác");
        //                                                }
        //                                            }
        //                                            if (string.IsNullOrEmpty(priceMax)) objDetail.PriceMax = null;
        //                                            else
        //                                            {
        //                                                try
        //                                                {
        //                                                    objDetail.PriceMax = Convert.ToDecimal(priceMax);
        //                                                }
        //                                                catch
        //                                                {
        //                                                    objDetail.IsSuccess = false;
        //                                                    lstError.Add("Giá từ của bậc [" + pLevelCode + "] không chính xác");
        //                                                }
        //                                            }
        //                                            obj.ListDetail.Add(objDetail);
        //                                        }

        //                                    }
        //                                }

        //                                if (lstError.Count > 0)
        //                                    obj.ExcelError = string.Join(" ,", lstError);
        //                                result.Add(obj);
        //                                row++;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            while (col <= worksheet.Dimension.End.Column)
        //                            {
        //                                levelCode = ExcelHelper.GetValue(worksheet, row, col);
        //                                if (col == 4 && String.IsNullOrEmpty(levelCode)) break;
        //                                if (!string.IsNullOrEmpty(levelCode))
        //                                {
        //                                    var checkLeveCode = data.ListLevel.FirstOrDefault(c => c.Code == levelCode);
        //                                    if (checkLeveCode == null) throw new Exception("Bậc giá [" + levelCode + "] không tồn tại");
        //                                    else
        //                                    {
        //                                        dictColLevel.Add(col, checkLeveCode.ID);
        //                                        dictColLevelCode.Add(col, checkLeveCode.Code);
        //                                    }
        //                                }
        //                                else break;
        //                                col += 1;
        //                            }

        //                            row = 2;
        //                            while (row <= worksheet.Dimension.End.Row)
        //                            {
        //                                List<string> lstError = new List<string>();
        //                                DTOPriceGVLevelImport obj = new DTOPriceGVLevelImport();
        //                                obj.ListDetail = new List<DTOPriceGVLevelGroupVehicleExcel>();
        //                                obj.ExcelRow = row;
        //                                obj.ExcelSuccess = true;
        //                                obj.ExcelError = string.Empty;
        //                                col = 2;
        //                                string strSTT = ExcelHelper.GetValue(worksheet, row, 1);

        //                                Input = ExcelHelper.GetValue(worksheet, row, col);
        //                                //neu 2 cot dau rong thì thoat
        //                                if (string.IsNullOrEmpty(strSTT) && string.IsNullOrEmpty(Input)) break;

        //                                var checkRoute = data.ListRoute.FirstOrDefault(c => c.Code == Input);
        //                                if (checkRoute == null)
        //                                {
        //                                    lstError.Add("Mã cung đường [" + Input + "]không tồn tại");
        //                                    obj.ExcelSuccess = false;
        //                                }
        //                                else
        //                                {
        //                                    obj.RouteID = checkRoute.ID;
        //                                    obj.RouteCode = checkRoute.Code;
        //                                    obj.RouteName = checkRoute.RoutingName;
        //                                }

        //                                if (dictColLevel.Keys.Count > 0)
        //                                {
        //                                    foreach (var word in dictColLevel)
        //                                    {
        //                                        int pCol = word.Key;
        //                                        int pLevel = word.Value;
        //                                        string pLevelCode = "";
        //                                        dictColLevelCode.TryGetValue(pCol, out  pLevelCode);

        //                                        string price = ExcelHelper.GetValue(worksheet, row, pCol);


        //                                        if (!string.IsNullOrEmpty(price))
        //                                        {
        //                                            DTOPriceGVLevelGroupVehicleExcel objDetail = new DTOPriceGVLevelGroupVehicleExcel();
        //                                            objDetail.IsSuccess = true;
        //                                            objDetail.RouteID = obj.RouteID;
        //                                            objDetail.LevelID = pLevel;
        //                                            objDetail.PriceMax = null;
        //                                            objDetail.PriceMin = null;
        //                                            try
        //                                            {
        //                                                objDetail.Price = Convert.ToDecimal(price);
        //                                            }
        //                                            catch
        //                                            {
        //                                                objDetail.IsSuccess = false;
        //                                                lstError.Add("Giá của bậc[" + pLevelCode + "] không chính xác");
        //                                            }
        //                                            obj.ListDetail.Add(objDetail);
        //                                        }

        //                                    }
        //                                }

        //                                if (lstError.Count > 0)
        //                                    obj.ExcelError = string.Join(" ,", lstError);
        //                                result.Add(obj);
        //                                row++;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //[HttpPost]
        //public void CUSPrice_DI_PriceGVLevel_ExcelImport(dynamic dynParam)
        //{
        //    try
        //    {
        //        int priceID = (int)dynParam.priceID;
        //        List<DTOPriceGVLevelImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceGVLevelImport>>(dynParam.lst.ToString());
        //        ServiceFactory.SVCustomer((ISVCustomer sv) =>
        //        {
        //            sv.CUSPrice_DI_PriceGVLevel_ExcelImport(lst, priceID);
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public List<DTOPriceGroupVehicle> CUSPrice_DI_GroupVehicle_GetData(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceGroupVehicle> result = new List<DTOPriceGroupVehicle>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_GroupVehicle_GetData(priceID);
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

        #region GroupProduct

        [HttpPost]
        public List<DTOPriceDIGroupOfProduct> CUSPrice_DI_GroupProduct_Data(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(List<DTOPriceDIGroupOfProduct>);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_GroupProduct_Data(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_GroupProduct_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceDIGroupOfProduct> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDIGroupOfProduct>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_GroupProduct_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string CUSPrice_DI_GroupProduct_Export(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                bool isFrame = (bool)dynParam.isFrame;
                DTOPriceDIGroupOfProductData data = new DTOPriceDIGroupOfProductData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    data = sv.CUSPrice_DI_GroupProduct_Export(priceID);
                });
                string file = "/Uploads/temp/" + "BangGiaLTLNormal_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(f))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 1, row = 1;
                    int stt = 1;
                    #region header
                    if (isFrame)
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 20;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row + 1, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        foreach (var level in data.ListGOP)
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
                        foreach (var level in data.ListGOP)
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
                            foreach (var level in data.ListGOP)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.GroupOfProductID == level.ID).FirstOrDefault();
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
                            foreach (var level in data.ListGOP)
                            {
                                col++;
                                var check = data.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.GroupOfProductID == level.ID).FirstOrDefault();
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
        public List<DTOPriceDIGroupOfProductImport> CUSPrice_DI_GroupProduct_Check(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSPrice_DI_GroupProduct_Export(priceID);
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
        public void CUSPrice_DI_GroupProduct_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceDIGroupOfProductImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDIGroupOfProductImport>>(dynParam.data.ToString());

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_GroupProduct_Import(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CUSPrice_DI_GroupProduct_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;

                var result = default(SYSExcel);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_GroupProduct_ExcelInit(isFrame, priceID, functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_GroupProduct_ExcelChange(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_GroupProduct_ExcelChange(isFrame, priceID, id, row, cells, lstMessageError);
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
        public SYSExcel CUSPrice_DI_GroupProduct_ExcelOnImport(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_GroupProduct_ExcelOnImport(isFrame, priceID, id, lst[0].Rows, lstMessageError);
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
        public bool CUSPrice_DI_GroupProduct_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var priceID = (int)dynParam.priceID;
                var isFrame = (bool)dynParam.isFrame;
                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_GroupProduct_ExcelApprove(isFrame, priceID, id);
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

        #region Loading new

        #region common
        [HttpPost]
        public void CUSPrice_DI_Load_Delete(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Load_Delete(ID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Load_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Load_DeleteList(data);
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
        public List<DTOPriceTruckDILoad> CUSPrice_DI_LoadLocation_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadLocation_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_LoadLocation_LocationNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadLocation_LocationNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadLocation_LocationNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadLocation_LocationNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadLocation_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadLocation_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_LoadLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadLocation_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CUSPrice_DI_LoadLocation_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadLocation_Export(contractTermID, priceID);
                    data = sv.CUSContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportCUSPrice_LoadLocation_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
        public List<DTOPriceTruckDILoad_Import> CUSPrice_DI_LoadLocation_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSContract_Price_ExcelData(contractTermID);
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
        public void CUSPrice_DI_LoadLocation_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadLocation_Import(lst, priceID);
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
        public List<DTOPriceTruckDILoad> CUSPrice_DI_LoadRoute_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadRoute_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_LoadRoute_RouteNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadRoute_RouteNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadRoute_RouteNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadRoute_RouteNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadRoute_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadRoute_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_LoadRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadRoute_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CUSPrice_DI_LoadRoute_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadRoute_Export(contractTermID, priceID);
                    data = sv.CUSContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportCUSPrice_LoadRoute_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
        public List<DTOPriceTruckDILoad_Import> CUSPrice_DI_LoadRoute_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSContract_Price_ExcelData(contractTermID);
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
        public void CUSPrice_DI_LoadRoute_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadRoute_Import(lst, priceID);
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
        public List<DTOPriceTruckDILoad> CUSPrice_DI_LoadPartner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadPartner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_LoadPartner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadPartner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadPartner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadPartner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadPartner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadPartner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_LoadPartner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadPartner_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CUSPrice_DI_LoadPartner_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadPartner_Export(contractTermID, priceID);
                    data = sv.CUSContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportCUSPrice_LoadPartner_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
        public List<DTOPriceTruckDILoad_Import> CUSPrice_DI_LoadPartner_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSContract_Price_ExcelData(contractTermID);
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
        public void CUSPrice_DI_LoadPartner_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadPartner_Import(lst, priceID);
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
        public List<DTOPriceDILoadPartner> CUSPrice_DI_LoadPartner_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceDILoadPartner>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadPartner_Partner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_LoadPartner_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceDILoadPartner> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILoadPartner>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadPartner_Partner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_LoadPartner_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_LoadPartner_Partner_DeleteList(priceID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLoad CUSPrice_DI_PriceMOQLoad_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPriceDIMOQLoad);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CUSPrice_DI_PriceMOQLoad_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceDIMOQLoad item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoad>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_DeleteList(priceID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupLocation_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_GroupLocation_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_GroupLocation_SaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(request, PriceMOQLoadID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_GroupProduct_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_Save(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                DTOPriceDIMOQLoadGroupProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoadGroupProduct>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_GroupProduct_Save(item, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLoadGroupProduct CUSPrice_DI_PriceMOQLoad_GroupProduct_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int cusID = (int)dynParam.cusID;
                DTOPriceDIMOQLoadGroupProduct result = new DTOPriceDIMOQLoadGroupProduct();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_GroupProduct_Get(id, cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupProduct_GOPList(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_GroupProduct_GOPList(cusID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_Location_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Location_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQLoad_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Location_LocationNotInList(request, PriceMOQLoadID, customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOPriceDIMOQLoadLocation CUSPrice_DI_PriceMOQLoad_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceDIMOQLoadLocation result = new DTOPriceDIMOQLoadLocation();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceDIMOQLoadLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoadLocation>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Location_Save(item);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_Route_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Route_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Route_SaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQLoad_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Route_RouteNotInList(request, PriceMOQLoadID, contractTermID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_ParentRoute_List(request, PriceMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_ParentRoute_SaveList(lst, PriceMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int PriceMOQLoadID = (int)dynParam.PriceMOQLoadID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(request, PriceMOQLoadID, contractTermID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_Province_List(dynamic dynParam)
        {
            try
            {
                int PriceDIMOQLoadID = (int)dynParam.PriceDIMOQLoadID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Province_List(request, PriceDIMOQLoadID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Province_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Province_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQLoad_Province_SaveList(dynamic dynParam)
        {
            try
            {
                int PriceDIMOQLoadID = (int)dynParam.PriceDIMOQLoadID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQLoad_Province_SaveList(lst, PriceDIMOQLoadID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQLoad_Province_NotInList(dynamic dynParam)
        {
            try
            {
                int PriceDIMOQLoadID = (int)dynParam.PriceDIMOQLoadID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQLoad_Province_NotInList(request, PriceDIMOQLoadID, contractTermID, CustomerID);
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
        public void CUSPrice_DI_UnLoad_Delete(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoad_Delete(ID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_UnLoad_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoad_DeleteList(data);
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
        public List<DTOPriceTruckDILoad> CUSPrice_DI_UnLoadLocation_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadLocation_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_UnLoadLocation_LocationNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadLocation_LocationNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadLocation_LocationNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadLocation_LocationNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadLocation_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadLocation_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_UnLoadLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadLocation_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CUSPrice_DI_UnLoadLocation_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadLocation_Export(contractTermID, priceID);
                    data = sv.CUSContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportCUSPrice_UnLoadLocation_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
        public List<DTOPriceTruckDILoad_Import> CUSPrice_DI_UnLoadLocation_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSContract_Price_ExcelData(contractTermID);
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
        public void CUSPrice_DI_UnLoadLocation_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadLocation_Import(lst, priceID);
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
        public List<DTOPriceTruckDILoad> CUSPrice_DI_UnLoadRoute_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadRoute_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_UnLoadRoute_RouteNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadRoute_RouteNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadRoute_RouteNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadRoute_RouteNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadRoute_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadRoute_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_UnLoadRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadRoute_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CUSPrice_DI_UnLoadRoute_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadRoute_Export(contractTermID, priceID);
                    data = sv.CUSContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportCUSPrice_UnloadRoute_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
        public List<DTOPriceTruckDILoad_Import> CUSPrice_DI_UnLoadRoute_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSContract_Price_ExcelData(contractTermID);
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
        public void CUSPrice_DI_UnLoadRoute_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadRoute_Import(lst, priceID);
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
        public List<DTOPriceTruckDILoad> CUSPrice_DI_UnLoadPartner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceTruckDILoad>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadPartner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_UnLoadPartner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadPartner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadPartner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceTruckDILoad> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadPartner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_UnLoadPartner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadPartner_DeleteList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CUSPrice_DI_UnLoadPartner_ExcelExport(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                int priceID = (int)dynParam.priceID;
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadPartner_Export(contractTermID, priceID);
                    data = sv.CUSContract_Price_ExcelData(contractTermID);
                });

                string file = "/" + FolderUpload.Export + "ExportCUSPrice_UnLoadPartner_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
        public List<DTOPriceTruckDILoad_Import> CUSPrice_DI_UnLoadPartner_ExcelCheck(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                List<DTOPriceTruckDILoad_Import> sData = new List<DTOPriceTruckDILoad_Import>();
                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPrice_ExcelData data = new DTOCUSPrice_ExcelData();
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSContract_Price_ExcelData(contractTermID);
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
        public void CUSPrice_DI_UnLoadPartner_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceTruckDILoad_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceTruckDILoad_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadPartner_Import(lst, priceID);
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
        public List<DTOPriceDILoadPartner> CUSPrice_DI_UnLoadPartner_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceDILoadPartner>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadPartner_Partner_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int priceID = (int)dynParam.priceID;
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoadPartner_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceDILoadPartner> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILoadPartner>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadPartner_Partner_SaveList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_UnLoadPartner_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoadPartner_Partner_DeleteList(priceID);
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
        public DTOResult CUSPrice_DI_PriceMOQUnLoad_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQUnLoad_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLoad CUSPrice_DI_PriceMOQUnLoad_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPriceDIMOQLoad);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQUnLoad_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CUSPrice_DI_PriceMOQUnLoad_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceDIMOQLoad item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLoad>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQUnLoad_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_PriceMOQUnLoad_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQUnLoad_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQUnLoad_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQUnLoad_DeleteList(priceID);
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

        #region PricePackingCO

        [HttpPost]
        public List<DTOPriceRouting> CUSPrice_CO_COPackingPrice_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = new List<DTOPriceRouting>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_COPackingPrice_List(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_CO_COPackingPrice_SaveList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceRouting> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceRouting>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_COPackingPrice_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string CUSPrice_CO_COPackingPrice_Export(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                DTOPriceContainer_Export resBody = new DTOPriceContainer_Export();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    resBody = sv.CUSPrice_CO_COPackingPrice_Export(priceID);
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
        public List<DTOPrice_COPackingPrice_Import> CUSPrice_CO_COPackingPrice_Check(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string file = dynParam.file.ToString();
                int contractID = (int)dynParam.contractID;
                List<DTOPrice_COPackingPrice_Import> sData = new List<DTOPrice_COPackingPrice_Import>();
                if (!string.IsNullOrEmpty(file))
                {
                    DTOCUSPrice_Data data = new DTOCUSPrice_Data();
                    List<CATPacking> resPacking = new List<CATPacking>();

                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSContract_Price_Data(contractID);
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
                                            if (!dicTU.ContainsKey(objTU.ID))
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
        public void CUSPrice_CO_COPackingPrice_Import(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPrice_COPackingPrice_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPrice_COPackingPrice_Import>>(dynParam.data.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_COPackingPrice_Import(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PriceServiceCO

        [HttpPost]
        public DTOResult CUSPrice_CO_Service_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Service_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_CO_ServicePacking_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_ServicePacking_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATPriceCOService CUSPrice_CO_Service_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOCATPriceCOService();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Service_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATPriceCOService CUSPrice_CO_ServicePacking_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOCATPriceCOService();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_ServicePacking_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CUSPrice_CO_Service_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                DTOCATPriceCOService item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPriceCOService>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    item.ID = sv.CUSPrice_CO_Service_Save(item, priceID);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_CO_Service_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Service_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_CO_CATService_List()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_CATService_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_CO_CATServicePacking_List()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_CATServicePacking_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSPrice_CO_CATCODefault_List(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                var result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_CATCODefault_List(contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Price level
        [HttpPost]
        public string CUSPrice_DI_PriceLevel_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                DTOPriceDILevelData data = new DTOPriceDILevelData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    data = sv.CUSPrice_DI_PriceLevel_ExcelData(priceID, contractTermID);
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
        public List<DTOPriceDILevelImport> CUSPrice_DI_PriceLevel_ExcelCheck(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSPrice_DI_PriceLevel_ExcelData(priceID, contractTermID);
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
        //[HttpPost]
        //public void CUSPrice_DI_PriceLevel_ExcelImport(dynamic dynParam)
        //{
        //    try
        //    {
        //        int priceID = (int)dynParam.priceID;
        //        List<DTOPriceDILevelImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILevelImport>>(dynParam.lst.ToString());
        //        ServiceFactory.SVCustomer((ISVCustomer sv) =>
        //        {
        //            sv.CUSPrice_DI_PriceLevel_ExcelImport(lst, priceID);
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public SYSExcel CUSPrice_DI_PriceGVLevel_ExcelInit(dynamic dynParam)
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

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceGVLevel_ExcelInit(isFrame,priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_PriceGVLevel_ExcelChange(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_PriceGVLevel_ExcelChange(isFrame,priceID, contractTermID, id, row, cells, lstMessageError);
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
        public SYSExcel CUSPrice_DI_PriceGVLevel_ExcelOnImport(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_PriceGVLevel_ExcelOnImport(isFrame, priceID, contractTermID, id, lst[0].Rows, lstMessageError);
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
        public bool CUSPrice_DI_PriceGVLevel_ExcelApprove(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_PriceGVLevel_ExcelApprove(isFrame, priceID, contractTermID, id);
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
        public List<DTOPriceDILevelGroupProduct> CUSPrice_DI_PriceLevel_DetailData(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceDILevelGroupProduct> result = new List<DTOPriceDILevelGroupProduct>();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceLevel_DetailData(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_PriceLevel_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceDILevelGroupProduct> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceDILevelGroupProduct>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceLevel_Save(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CUSPrice_DI_PriceLevel_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;

                var result = default(SYSExcel);

               ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceLevel_ExcelInit(priceID,contractTermID,functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_PriceLevel_ExcelChange(dynamic dynParam)
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
                   ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_PriceLevel_ExcelChange(priceID, contractTermID,id, row, cells, lstMessageError);
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
        public SYSExcel CUSPrice_DI_PriceLevel_ExcelImport(dynamic dynParam)
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
                   ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_PriceLevel_OnExcelImport(priceID, contractTermID,id, lst[0].Rows, lstMessageError);
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
        public bool CUSPrice_DI_PriceLevel_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var priceID = (int)dynParam.priceID;
                var contractTermID = (int)dynParam.contractTermID;
                var result = false;
                if (id > 0)
                {
                   ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_DI_PriceLevel_ExcelApprove(priceID,contractTermID,id);
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

        #region price Ex new

        #region info
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIEx CUSPrice_DI_Ex_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOPriceDIEx);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CUSPrice_DI_Ex_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceDIEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIEx>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_Ex_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_Load_DeleteAllList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Load_DeleteAllList(priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_UnLoad_DeleteAllList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_UnLoad_DeleteAllList(priceID);
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
        public DTOResult CUSPrice_DI_Ex_GroupLocation_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_GroupLocation_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_GroupLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_GroupLocation_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_GroupLocation_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_GroupLocation_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_GroupLocation_GroupNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_GroupLocation_GroupNotInList(request, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_GroupProduct_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_GroupProduct_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_GroupProduct_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_GroupProduct_Save(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                DTOPriceDIExGroupProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIExGroupProduct>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_GroupProduct_Save(item, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIExGroupProduct CUSPrice_DI_Ex_GroupProduct_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int cusID = (int)dynParam.cusID;
                DTOPriceDIExGroupProduct result = new DTOPriceDIExGroupProduct();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_GroupProduct_Get(id, cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_GroupProduct_GOPList(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_GroupProduct_GOPList(cusID);
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
        public DTOResult CUSPrice_DI_Ex_Location_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Location_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIExLocation CUSPrice_DI_Ex_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceDIExLocation result = new DTOPriceDIExLocation();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceDIExLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIExLocation>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Location_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Location_LocationNotInSaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Location_LocationNotInList(request, priceExID, customerID);
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
        public DTOResult CUSPrice_DI_Ex_Route_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Route_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Route_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Route_RouteNotInList(request, priceExID, contractTermID);
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
        public DTOResult CUSPrice_DI_Ex_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_ParentRoute_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_ParentRoute_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_ParentRoute_RouteNotInList(request, priceExID, contractTermID);
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
        public DTOResult CUSPrice_DI_Ex_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Partner_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Partner_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Partner_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_Partner_PartnerNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Partner_PartnerNotInList(request, priceExID, contractTermID, CustomerID);
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
        public DTOResult CUSPrice_DI_Ex_Province_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Province_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Province_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Province_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_Ex_Province_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_Ex_Province_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_Ex_Province_NotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_Ex_Province_NotInList(request, priceExID, contractTermID, CustomerID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATPriceDIMOQ CUSPrice_DI_PriceMOQ_Get(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOCATPriceDIMOQ);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Get(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CUSPrice_DI_PriceMOQ_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOCATPriceDIMOQ item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPriceDIMOQ>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_DeleteList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_DeleteList(priceID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_GroupLocation_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_GroupLocation_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_GroupLocation_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_GroupLocation_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_GroupLocation_SaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_GroupLocation_SaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(request, priceMOQID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_GroupProduct_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_GroupProduct_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_GroupProduct_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_GroupProduct_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_GroupProduct_Save(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                DTOPriceDIMOQGroupProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQGroupProduct>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_GroupProduct_Save(item, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQGroupProduct CUSPrice_DI_PriceMOQ_GroupProduct_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int cusID = (int)dynParam.cusID;
                DTOPriceDIMOQGroupProduct result = new DTOPriceDIMOQGroupProduct();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_GroupProduct_Get(id, cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQ_GroupProduct_GOPList(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_GroupProduct_GOPList(cusID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Location_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Location_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceDIMOQLocation CUSPrice_DI_PriceMOQ_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceDIMOQLocation result = new DTOPriceDIMOQLocation();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceDIMOQLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceDIMOQLocation>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Location_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Location_LocationNotInSaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQ_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Location_LocationNotInList(request, priceMOQID, customerID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Route_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Route_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Route_SaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQ_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Route_RouteNotInList(request, priceMOQID, contractTermID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_ParentRoute_List(request, priceMOQID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_ParentRoute_SaveList(lst, priceMOQID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceMOQID = (int)dynParam.priceMOQID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(request, priceMOQID, contractTermID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Partner_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Partner_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Partner_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQ_Partner_PartnerNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Partner_PartnerNotInList(request, priceExID, contractTermID, CustomerID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Province_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Province_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Province_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Province_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_DI_PriceMOQ_Province_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_DI_PriceMOQ_Province_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_DI_PriceMOQ_Province_NotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_DI_PriceMOQ_Province_NotInList(request, priceExID, contractTermID, CustomerID);
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

        #region CusContract Price CO
        [HttpPost]
        public DTOCUSPriceCO_Data CUSContract_PriceCO_Data(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                var result = default(DTOCUSPriceCO_Data);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_PriceCO_Data(contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceCOContainerData CUSPrice_CO_COContainer_Data(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                var result = default(DTOPriceCOContainerData);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_COContainer_Data(priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_COContainer_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOPriceCOContainer> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceCOContainer>>(dynParam.data.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_COContainer_SaveList(data, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CUSPrice_CO_COContainer_ContainerList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = (string)dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_COContainer_ContainerList(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_CO_COContainer_ContainerNotInSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_COContainer_ContainerNotInSave(lst, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CUSPrice_CO_COContainer_ContainerNotInList(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = (string)dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_COContainer_ContainerNotInList(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_CO_COContainer_ContainerDelete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int priceID = (int)dynParam.priceID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_COContainer_ContainerDelete(lst, priceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string CUSPrice_CO_GroupContainer_ExcelExport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int contractTermID = (int)dynParam.contractTermID;
                bool isFrame = (bool)dynParam.isFrame;
                DTOPriceCOContainerExcelData data = new DTOPriceCOContainerExcelData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    data = sv.CUSPrice_CO_COContainer_ExcelData(priceID, contractTermID);
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
        public List<DTOPriceCOContainerImport> CUSPrice_CO_GroupContainer_ExcelCheck(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        data = sv.CUSPrice_CO_COContainer_ExcelData(priceID, contractTermID);
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
        public void CUSPrice_CO_GroupContainer_ExcelImport(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                List<DTOPriceCOContainerImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOPriceCOContainerImport>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_COContainer_ExcelImport(lst, priceID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CUSPrice_CO_GroupContainer_ExcelInit(dynamic dynParam)
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

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_GroupContainer_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row CUSPrice_CO_GroupContainer_ExcelChange(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_CO_GroupContainer_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
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
        public SYSExcel CUSPrice_CO_GroupContainer_ExcelOnImport(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_CO_GroupContainer_ExcelOnImport(isFrame, priceID, contractTermID, id, lst[0].Rows, lstMessageError);
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
        public bool CUSPrice_CO_GroupContainer_ExcelApprove(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSPrice_CO_GroupContainer_ExcelApprove(isFrame, priceID, contractTermID, id);
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

        #endregion

        #region GroupOfProduct
        [HttpPost]
        public DTOResult CUSContract_GroupOfProduct_List(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_GroupOfProduct_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATContractGroupOfProduct CUSContract_GroupOfProduct_Get(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                int id = (int)dynParam.id;
                DTOCATContractGroupOfProduct result = new DTOCATContractGroupOfProduct();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_GroupOfProduct_Get(id, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_GroupOfProduct_Save(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.contractID;
                DTOCATContractGroupOfProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractGroupOfProduct>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_GroupOfProduct_Save(item, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_GroupOfProduct_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_GroupOfProduct_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public double? CUSContract_GroupOfProduct_Check(dynamic dynParam)
        {
            try
            {
                double? result = null;
                DTOCATContractGroupOfProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractGroupOfProduct>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_GroupOfProduct_Check(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CUSContract_GroupOfProduct_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                int contractID = (int)dynParam.contractID;

                var result = default(SYSExcel);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_GroupOfProduct_ExcelInit(contractID, functionid, functionkey, isreload);
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
        public Row CUSContract_GroupOfProduct_ExcelChange(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSContract_GroupOfProduct_ExcelChange(contractID,id, row, cells, lstMessageError);
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
        public SYSExcel CUSContract_GroupOfProduct_ExcelImport(dynamic dynParam)
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
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSContract_GroupOfProduct_ExcelImport(contractID,id, lst[0].Rows, lstMessageError);
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
        public bool CUSContract_GroupOfProduct_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;
                int contractID = (int)dynParam.contractID;

                if (id > 0)
                {
                    ServiceFactory.SVCustomer((ISVCustomer sv) =>
                    {
                        result = sv.CUSContract_GroupOfProduct_ExcelApprove(contractID, id);
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

        #region thay doi bang gia the vat tu
        [HttpPost]
        public DTOCUSPrice_MaterialData CUSContract_MaterialChange_Data(dynamic dynParam)
        {
            try
            {
                int contractTermID = (int)dynParam.contractTermID;
                DTOCUSPrice_MaterialData result = new DTOCUSPrice_MaterialData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_MaterialChange_Data(contractTermID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_MaterialChange_Save(dynamic dynParam)
        {
            try
            {
                int contractMaterialID = (int)dynParam.contractMaterialID;
                DTOCUSPrice_MaterialData item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPrice_MaterialData>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_MaterialChange_Save(item, contractMaterialID);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CusContract Setting
        [HttpPost]
        public void CUSContract_Setting_TypeOfRunLevelSave(dynamic dynParam)
        {
            try
            {
                int typeID = (int)dynParam.typeID;
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Setting_TypeOfRunLevelSave(typeID, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Setting_TypeOfSGroupProductChangeSave(dynamic dynParam)
        {
            try
            {
                int typeID = (int)dynParam.typeID;
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Setting_TypeOfSGroupProductChangeSave(typeID, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATContractSetting_Save(dynamic dynParam)
        {
            try
            {
                string setting = dynParam.lstsetting.ToString();
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CATContractSetting_Save(setting, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_Setting_GOVList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Setting_GOVList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATGroupOfVehicle> CUSContract_Setting_Level_GOVList(dynamic dynParam)
        {
            try
            {
                var result = default(List<CATGroupOfVehicle>);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Setting_Level_GOVList(contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATContractGroupVehicle CUSContract_Setting_GOVGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATContractGroupVehicle);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Setting_GOVGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_Setting_GOVSave(dynamic dynParam)
        {
            try
            {
                DTOCATContractGroupVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATContractGroupVehicle>(dynParam.item.ToString());

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Setting_GOVSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_Setting_GOVDeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Setting_GOVDeleteList(lst, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSContract_Setting_GOVNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Setting_GOVNotInList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_Setting_GOVNotInSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Setting_GOVNotInSave(lst, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSContract_Setting_LevelList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Setting_LevelList(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATContractLevel CUSContract_Setting_LevelGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATContractLevel);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_Setting_LevelGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_Setting_LevelSave(dynamic dynParam)
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
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Setting_LevelSave(item, contractID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_Setting_LevelDeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_Setting_LevelDeleteList(lst);
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
        public DTOResult CUSContract_ContractTerm_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractID = (int)dynParam.contractID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_List(request, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOContractTerm CUSContract_ContractTerm_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int contractID = (int)dynParam.contractID;
                var result = default(DTOContractTerm);

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_Get(id, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CUSContract_ContractTerm_Save(dynamic dynParam)
        {
            try
            {
                DTOContractTerm item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOContractTerm>(dynParam.item.ToString());
                int contractID = (int)dynParam.contractID;
                int result = -1;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_Save(item, contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_ContractTerm_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_ContractTerm_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSContract_ContractTerm_Price_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_Price_List(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_ContractTerm_Open(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_ContractTerm_Open(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_ContractTerm_Close(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_ContractTerm_Close(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSTerm_Change_RemoveWarning(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSTerm_Change_RemoveWarning(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region KPI Term
        [HttpPost]
        public DTOResult CUSContract_ContractTerm_KPITime_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_KPITime_List(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_ContractTerm_KPITime_SaveExpr(dynamic dynParam)
        {
            try
            {
                DTOContractTerm_KPITime item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOContractTerm_KPITime>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_ContractTerm_KPITime_SaveExpr(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_ContractTerm_KPITime_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_KPITime_NotInList(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_ContractTerm_KPITime_SaveNotInList(dynamic dynParam)
        {
            try
            {
                List<DTOContractTerm_TypeOfKPI> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractTerm_TypeOfKPI>>(dynParam.lst.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_ContractTerm_KPITime_SaveNotInList(lst, contractTermID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_ContractTerm_KPIQuantity_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_KPIQuantity_List(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSContract_ContractTerm_KPIQuantity_SaveExpr(dynamic dynParam)
        {
            try
            {
                DTOContractTerm_KPIQuantity item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOContractTerm_KPIQuantity>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_ContractTerm_KPIQuantity_SaveExpr(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSContract_ContractTerm_KPIQuantity_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                int contractTermID = (int)dynParam.contractTermID;

                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_ContractTerm_KPIQuantity_NotInList(request, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSContract_ContractTerm_KPIQuantity_SaveNotInList(dynamic dynParam)
        {
            try
            {
                List<DTOContractTerm_TypeOfKPI> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOContractTerm_TypeOfKPI>>(dynParam.lst.ToString());
                int contractTermID = (int)dynParam.contractTermID;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSContract_ContractTerm_KPIQuantity_SaveNotInList(lst, contractTermID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DateTime? CUSContract_KPITime_Check_Expression(dynamic dynParam)
        {
            try
            {
                KPITimeDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPITimeDate>(dynParam.item.ToString());
                List<KPITimeDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPITimeDate>>(dynParam.lst.ToString());
                DateTime? result = null;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPITime_Check_Expression(item, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool? CUSContract_KPITime_Check_Hit(dynamic dynParam)
        {
            try
            {
                KPITimeDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPITimeDate>(dynParam.item.ToString());
                List<KPITimeDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPITimeDate>>(dynParam.lst.ToString());
                bool? result = null;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPITime_Check_Hit(item, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIQuantityDate> CUSContract_KPIQuantity_Get(dynamic dynParam)
        {
            try
            {
                List<KPIQuantityDate> result = new List<KPIQuantityDate>(); ;
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPIQuantity_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public KPIQuantityDate CUSContract_KPIQuantity_Check_Expression(dynamic dynParam)
        {
            try
            {
                KPIQuantityDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIQuantityDate>(dynParam.item.ToString());
                List<KPIQuantityDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPIQuantityDate>>(dynParam.lst.ToString());
                KPIQuantityDate result = new KPIQuantityDate(); ;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPIQuantity_Check_Expression(item, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool? CUSContract_KPIQuantity_Check_Hit(dynamic dynParam)
        {
            try
            {
                KPIQuantityDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIQuantityDate>(dynParam.item.ToString());
                List<KPIQuantityDate> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPIQuantityDate>>(dynParam.lst.ToString());
                bool? result = null;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSContract_KPIQuantity_Check_Hit(item, lst);
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
        public DTOResult CUSPrice_CO_Ex_List(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_List(request, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceCOEx CUSPrice_CO_Ex_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOPriceCOEx);
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CUSPrice_CO_Ex_Save(dynamic dynParam)
        {
            try
            {
                int priceID = (int)dynParam.priceID;
                int result = 0;
                DTOPriceCOEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceCOEx>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Save(item, priceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CUSPrice_CO_Ex_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Delete(id);
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
        public DTOResult CUSPrice_CO_Ex_Location_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Location_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_Location_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Location_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOPriceCOExLocation CUSPrice_CO_Ex_Location_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOPriceCOExLocation result = new DTOPriceCOExLocation();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_Location_Save(dynamic dynParam)
        {
            try
            {
                DTOPriceCOExLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPriceCOExLocation>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Location_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_Location_LocationNotInSaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Location_LocationNotInSaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_CO_Ex_Location_LocationNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int customerID = (int)dynParam.customerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Location_LocationNotInList(request, priceExID, customerID);
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
        public DTOResult CUSPrice_CO_Ex_Route_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Route_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_Route_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Route_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_Route_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Route_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_CO_Ex_Route_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Route_RouteNotInList(request, priceExID, contractTermID);
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
        public DTOResult CUSPrice_CO_Ex_ParentRoute_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_ParentRoute_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_ParentRoute_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_ParentRoute_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_ParentRoute_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_ParentRoute_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_CO_Ex_ParentRoute_RouteNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_ParentRoute_RouteNotInList(request, priceExID, contractTermID);
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
        public DTOResult CUSPrice_CO_Ex_Partner_List(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Partner_List(request, priceExID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_Partner_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Partner_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSPrice_CO_Ex_Partner_SaveList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSPrice_CO_Ex_Partner_SaveList(lst, priceExID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSPrice_CO_Ex_Partner_PartnerNotInList(dynamic dynParam)
        {
            try
            {
                int priceExID = (int)dynParam.priceExID;
                int contractTermID = (int)dynParam.contractTermID;
                int CustomerID = (int)dynParam.CustomerID;
                string request = (string)dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSPrice_CO_Ex_Partner_PartnerNotInList(request, priceExID, contractTermID,CustomerID);
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

        [HttpPost]
        public void Location_Check(dynamic dynParam)
        {
            try
            {
                int customerid = (int)dynParam.customerid;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.Location_Check(customerid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CUSSettingPlan
        [HttpPost]
        public DTOResult CUSSettingPlan_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingPlan_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSSettingPlan CUSSettingPlan_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingPlan result = new DTOCUSSettingPlan();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingPlan_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingPlan_Save(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingPlan item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPlan>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingPlan_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingPlan_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingPlan item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPlan>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingPlan_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSSettingPlan_SettingOrderList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingPlan_SettingOrderList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CUSSettingPOD
        [HttpPost]
        public DTOResult CUSSettingPOD_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingPOD_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingPOD CUSSettingPOD_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingPOD result = new DTOCUSSettingPOD();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingPOD_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingPOD_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingPOD item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPOD>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingPOD_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingPOD_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingPOD item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPOD>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingPOD_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSSettingMON_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingMON_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingMON CUSSettingMON_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingMON result = new DTOCUSSettingMON();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingMON_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingMON_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingMON item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingMON>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingMON_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingMON_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingMON item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingMON>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingMON_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSSettingPODMap_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingPODMap_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingPODMap CUSSettingPODMap_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingPODMap result = new DTOCUSSettingPODMap();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingPODMap_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingPODMap_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingPODMap item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPODMap>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingPODMap_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingPODMap_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingPODMap item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPODMap>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingPODMap_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CUSSettingMONImport
        [HttpPost]
        public DTOResult CUSSettingMONImport_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingMONImport_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSSettingMONImport CUSSettingMONImport_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingMONImport result = new DTOCUSSettingMONImport();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingMONImport_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingMONImport_Save(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingMONImport item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingMONImport>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingMONImport_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingMONImport_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingMONImport item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingMONImport>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingMONImport_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CUSSettingMONImport_SettingOrderList(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingMONImport_SettingOrderList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CUSSettingMONExt_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingMONExt_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingMONExt CUSSettingMONExt_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingMONExt result = new DTOCUSSettingMONExt();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingMONExt_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingMONExt_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingMONExt item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingMONExt>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingMONExt_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingMONExt_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingMONExt item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingMONExt>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingMONExt_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CUSSettingORDPlan
        [HttpPost]
        public DTOResult CUSSettingORDPlan_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingORDPlan_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCUSSettingORDPlan CUSSettingORDPlan_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingORDPlan result = new DTOCUSSettingORDPlan();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingORDPlan_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingORDPlan_Save(dynamic dynParam)
        {
            try
            {

                int id = (int)dynParam.id;
                DTOCUSSettingORDPlan item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingORDPlan>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingORDPlan_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingORDPlan_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingORDPlan item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingORDPlan>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingORDPlan_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Price History
        //public int PriceHistory_CheckPrice(dynamic dynParam)
        //{
        //    try
        //    {
        //        int transportModeID = (int)dynParam.transportModeID;
        //        List<int> lstCusId = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCusId.ToString());
        //        DTOCUSPrice_HistoryData historyData = new DTOCUSPrice_HistoryData();
        //        int result = 0;
        //        int iFCL = -(int)SYSVarType.TransportModeFCL;
        //        if (transportModeID == iFCL)
        //        {
        //            return result = 1;
        //        }
        //        ServiceFactory.SVCustomer((ISVCustomer sv) =>
        //        {
        //            historyData = sv.PriceHistory_CheckPrice(lstCusId, transportModeID);
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
        //        List<int> lstCusId = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCusId.ToString());
        //        string result = "";
        //        if (lstCusId.Count == 1)
        //        {
        //            //result = PriceHistory_ExportOneUser(lstCusId[0], 0, transportModeID, typePrice);
        //        }
        //        else
        //        {
        //            //result = PriceHistory_ExportMulUser(lstCusId, transportModeID, typePrice);
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public string PriceHistory_ExportOneUser(dynamic dynParam)
        {
            try
            {
                int cusId = (int)dynParam.cusId;
                int contractID = (int)dynParam.contractID;
                DTOCUSPrice_HistoryData result = new DTOCUSPrice_HistoryData();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.PriceHistory_GetDataOneUser(cusId, contractID);
                });

                string[] colorCol = new string[] { ExcelHelper.ColorGreen, ExcelHelper.ColorBlue, ExcelHelper.ColorOrange };

                int iFCL = 1;
                int iFTL = 2;
                int iLTL = 3;

                string file = "/" + FolderUpload.Export + "ExportPriceHistory_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1;
                    if (result.ContractPriceData != null)
                    {
                        var data = result.ContractPriceData;
                        row = 1;
                        #region cung đường
                        int colRte = col;
                        row = 3;
                        //worksheet.Cells[row, colRte].Value = "Mã CĐ Hệ thống";
                        //colRte++; worksheet.Cells[row, colRte].Value = "Tên CĐ Hệ thống";
                        //colRte++; worksheet.Cells[row - 2, colRte].Value = item.CustomerName;
                        worksheet.Cells[row, colRte].Value = "Mã cung đường";
                        colRte++; worksheet.Cells[row, colRte].Value = "Tên cung đường";
                        ExcelHelper.CreateCellStyle(worksheet, row - 2, colRte - 1, row - 1, colRte, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        ExcelHelper.CreateCellStyle(worksheet, row, col, row, colRte, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        worksheet.Cells[row - 2, col, row, colRte].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row - 2, col, row, colRte].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        row = 4;
                        colRte = col;
                        if (data.ListRouting != null)
                        {
                            foreach (var route in data.ListRouting)
                            {
                                colRte = col;
                                //worksheet.Cells[row, colRte].Value = route.CATCode; worksheet.Column(colRte).Width = 20;
                                //colRte++; worksheet.Cells[row, colRte].Value = route.CATName; worksheet.Column(colRte).Width = 20;
                                worksheet.Cells[row, colRte].Value = route.Code; worksheet.Column(colRte).Width = 20;
                                colRte++; worksheet.Cells[row, colRte].Value = route.RoutingName; worksheet.Column(colRte).Width = 20;
                                row++;
                            }
                        }
                        #endregion

                        #region bảng giá
                        col = colRte;
                        int colPrice = colRte + 1;
                        int index = 0;
                        foreach (var price in result.ListPrice)
                        {
                            row = 1;
                            int colR = col;
                            colPrice = col;
                            if ((price.ItemPrice.CheckPrice.HasNormal == true) || (price.ItemPrice.CheckPrice.HasLevel == true) || ((price.FCLData != null) && (price.FCLData.ListPacking.Count > 0)))
                            {
                                colPrice++; worksheet.Cells[row, colPrice].Value = price.ItemPrice.Code + " - " + price.ItemPrice.EffectDate.ToString("dd/MM/yyyy");
                            }
                            row = 3;
                            #region FTL
                            if (price.ItemPrice.TypeOfMode == iFTL)
                            {
                                #region FTL Normal
                                if (price.ItemPrice.CheckPrice.HasNormal == true)
                                {
                                    if (price.ItemPrice.TypeOfContract == 1)
                                    {
                                        colPrice = col;
                                        foreach (var gov in data.ListGroupOfVehicle)
                                        {
                                            colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        }
                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        row = 4;
                                        if (data.ListRouting != null)
                                        {
                                            foreach (var route in data.ListRouting)
                                            {
                                                colPrice = col;
                                                foreach (var gov in data.ListGroupOfVehicle)
                                                {
                                                    var value = price.FTLNormalDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == gov.ID).FirstOrDefault();
                                                    if (value != null)
                                                    {
                                                        colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                    else
                                                    {
                                                        colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                }
                                                row++;
                                            }
                                        }
                                    }
                                    else if (price.ItemPrice.TypeOfContract == 2)
                                    {
                                        row = 2;
                                        colPrice = col;
                                        foreach (var gov in data.ListGroupOfVehicle)
                                        {
                                            colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
                                            worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                            ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        }
                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        row = 4;
                                        if (data.ListRouting != null)
                                        {
                                            foreach (var route in data.ListRouting)
                                            {
                                                colPrice = col;
                                                foreach (var gov in data.ListGroupOfVehicle)
                                                {
                                                    colPrice++;
                                                    var value = price.FTLNormalDetail.Where(c => c.RouteID == route.ID && c.GroupOfVehicleID == gov.ID).FirstOrDefault();
                                                    if (value != null)
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                        worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                        worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                    }
                                                    colPrice++;
                                                }
                                                row++;
                                            }
                                        }
                                    }

                                    col = colPrice;
                                }
                                #endregion

                                #region FTL Level
                                if (price.ItemPrice.CheckPrice.HasLevel == true)
                                {
                                    if (price.ItemPrice.TypeOfContract == 1)
                                    {
                                        colPrice = col;
                                        foreach (var level in data.ListLevel)
                                        {
                                            colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        }
                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        row = 4;
                                        if (data.ListRouting != null)
                                        {
                                            foreach (var route in data.ListRouting)
                                            {
                                                colPrice = col;
                                                foreach (var level in data.ListLevel)
                                                {
                                                    var value = price.FTLLevelDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
                                                    if (value != null)
                                                    {
                                                        colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                    else
                                                    {
                                                        colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                }
                                                row++;
                                            }
                                        }
                                    }
                                    else if (price.ItemPrice.TypeOfContract == 2)
                                    {
                                        row = 2;
                                        colPrice = col;
                                        foreach (var level in data.ListLevel)
                                        {
                                            colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
                                            worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                            ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        }
                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        row = 4;
                                        if (data.ListRouting != null)
                                        {
                                            foreach (var route in data.ListRouting)
                                            {
                                                colPrice = col;
                                                foreach (var level in data.ListLevel)
                                                {
                                                    colPrice++;
                                                    var value = price.FTLLevelDetail.Where(c => c.RoutingID == route.ID && c.ContractLevelID == level.ID).FirstOrDefault();
                                                    if (value != null)
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                        worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                        worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                    }
                                                    colPrice++;
                                                }
                                                row++;
                                            }
                                        }
                                    }
                                    col = colPrice;
                                }
                                #endregion
                            }
                            #endregion

                            #region LTL
                            if (price.ItemPrice.TypeOfMode == iLTL)
                            {
                                #region LTL Normal
                                if (price.ItemPrice.CheckPrice.HasNormal == true)
                                {
                                    if (price.ItemPrice.TypeOfContract == 1)
                                    {
                                        colPrice = col;
                                        foreach (var gop in data.ListGroupOfProduct)
                                        {
                                            colPrice++; worksheet.Cells[row, colPrice].Value = gop.GroupName;
                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        }
                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        row = 4;
                                        if (data.ListRouting != null)
                                        {
                                            foreach (var route in data.ListRouting)
                                            {
                                                colPrice = col;
                                                foreach (var gop in data.ListGroupOfProduct)
                                                {
                                                    var value = price.LTLNormalDetail.Where(c => gop.ID == c.GroupOfProductID).FirstOrDefault();
                                                    if (value != null)
                                                    {
                                                        colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                    else
                                                    {
                                                        colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                }
                                                row++;
                                            }
                                        }

                                    }
                                    else if (price.ItemPrice.TypeOfContract == 2)
                                    {
                                        row = 2;
                                        colPrice = col;
                                        foreach (var gov in data.ListGroupOfProduct)
                                        {
                                            colPrice++; worksheet.Cells[row, colPrice].Value = gov.Code;
                                            worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
                                            ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                            ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        }
                                        worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        row = 4;
                                        if (data.ListRouting != null)
                                        {
                                            foreach (var route in data.ListRouting)
                                            {
                                                colPrice = col;
                                                foreach (var gop in data.ListGroupOfProduct)
                                                {
                                                    colPrice++;
                                                    var value = price.LTLNormalDetail.Where(c => gop.ID == c.GroupOfProductID).FirstOrDefault();
                                                    if (value != null)
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                        worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                        worksheet.Cells[row, colPrice + 1].Value = 0;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                    }
                                                    colPrice++;
                                                }
                                                row++;
                                            }
                                        }
                                    }

                                    col = colPrice;
                                }
                                #endregion

                                #region LTL Level
                                if (price.ItemPrice.CheckPrice.HasLevel == true)
                                {
                                    row = 2;
                                    colPrice = col;
                                    foreach (var level in data.ListLevel)
                                    {
                                        colPrice++; worksheet.Cells[row, colPrice].Value = level.Code;
                                        colPrice--;
                                        foreach (var gop in data.ListGroupOfProduct)
                                        {
                                            colPrice++; worksheet.Cells[row + 1, colPrice].Value = gop.GroupName;
                                        }
                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice - data.ListGroupOfProduct.Count + 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - data.ListGroupOfProduct.Count + 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                    }
                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    row = 4;
                                    colPrice = col;
                                    if (data.ListRouting != null)
                                    {
                                        foreach (var route in data.ListRouting)
                                        {
                                            foreach (var level in data.ListLevel)
                                            {
                                                foreach (var gop in data.ListGroupOfProduct)
                                                {
                                                    colPrice++;
                                                    var value = price.LTLLevelDetail.Where(c => c.RoutingID == route.ID && c.LevelID == level.ID && c.GroupProductID == gop.ID).FirstOrDefault();
                                                    if (value != null)
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                        ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    }
                                                }
                                            }
                                            colPrice = colPrice - data.ListGroupOfProduct.Count * data.ListLevel.Count;
                                            row++;
                                        }
                                    }

                                    col = colPrice + data.ListGroupOfProduct.Count * data.ListLevel.Count;
                                }
                                #endregion
                            }
                            #endregion

                            #region FCL
                            if (price.ItemPrice.TypeOfMode == iFCL)
                            {
                                if (price.ItemPrice.TypeOfContract == 1)
                                {
                                    colPrice = col;
                                    foreach (var pack in price.FCLData.ListPacking)
                                    {
                                        colPrice++; worksheet.Cells[row, colPrice].Value = pack.Code;
                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                    }
                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    row = 4;
                                    if (data.ListRouting != null)
                                    {
                                        foreach (var route in data.ListRouting)
                                        {
                                            colPrice = col;
                                            foreach (var pack in price.FCLData.ListPacking)
                                            {
                                                var value = price.FCLData.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == pack.ID).FirstOrDefault();
                                                if (value != null)
                                                {
                                                    colPrice++; worksheet.Cells[row, colPrice].Value = value.Price; worksheet.Column(colPrice).Width = 20;
                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                }
                                                else
                                                {
                                                    colPrice++; worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                }
                                            }
                                            row++;
                                        }
                                    }
                                }
                                else if (price.ItemPrice.TypeOfContract == 2)
                                {
                                    row = 2;
                                    colPrice = col;
                                    foreach (var pack in price.FCLData.ListPacking)
                                    {
                                        colPrice++; worksheet.Cells[row, colPrice].Value = pack.Code;
                                        worksheet.Cells[row + 1, colPrice].Value = "Giá từ";
                                        colPrice++; worksheet.Cells[row + 1, colPrice].Value = "Đến giá";
                                        ExcelHelper.CreateCellStyle(worksheet, row, colPrice - 1, row, colPrice, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        ExcelHelper.CreateCellStyle(worksheet, row + 1, colPrice - 1, row + 1, colPrice, false, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                    }
                                    worksheet.Cells[row, col, row, colPrice].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[row, col, row, colPrice].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    row = 4;
                                    if (data.ListRouting != null)
                                    {
                                        foreach (var route in data.ListRouting)
                                        {
                                            colPrice = col;
                                            foreach (var pack in price.FCLData.ListPacking)
                                            {
                                                colPrice++;
                                                var value = price.FCLData.ListDetail.Where(c => c.ContractRoutingID == route.ID && c.PackingID == pack.ID).FirstOrDefault();
                                                if (value != null)
                                                {
                                                    worksheet.Cells[row, colPrice].Value = value.PriceMin; worksheet.Column(colPrice).Width = 20;
                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    worksheet.Cells[row, colPrice + 1].Value = value.PriceMax; worksheet.Column(colPrice).Width = 20;
                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                }
                                                else
                                                {
                                                    worksheet.Cells[row, colPrice].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice, ExcelHelper.FormatMoney);
                                                    worksheet.Cells[row, colPrice + 1].Value = 0; worksheet.Column(colPrice).Width = 20;
                                                    ExcelHelper.CreateFormat(worksheet, row, colPrice + 1, ExcelHelper.FormatMoney);
                                                }
                                                colPrice++;
                                            }
                                            row++;
                                        }
                                    }
                                }

                                col = colPrice;
                            }
                            #endregion

                            if (col >= colR + 1)
                            {
                                var hasMerge = false;
                                if (price.ItemPrice.TypeOfMode == iFTL)
                                {
                                    if (price.ItemPrice.CheckPrice.HasNormal == true)
                                    {
                                        if (price.ItemPrice.TypeOfContract == 1)
                                        {
                                            ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                            hasMerge = true;
                                        }
                                    }
                                    if (price.ItemPrice.CheckPrice.HasLevel == true)
                                    {
                                        if (price.ItemPrice.TypeOfContract == 1)
                                        {
                                            ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                            hasMerge = true;
                                        }
                                    }
                                }
                                if (price.ItemPrice.TypeOfMode == iLTL)
                                {
                                    if (price.ItemPrice.CheckPrice.HasNormal == true)
                                    {
                                        if (price.ItemPrice.TypeOfContract == 1)
                                        {
                                            ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                            hasMerge = true;
                                        }
                                    }
                                }
                                if (price.ItemPrice.TypeOfMode == iFCL)
                                {
                                    if (price.ItemPrice.TypeOfContract == 1)
                                    {
                                        ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 2, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                        hasMerge = true;
                                    }
                                }
                                if (hasMerge == false)
                                {
                                    ExcelHelper.CreateCellStyle(worksheet, 1, colR + 1, 1, col, true, true, colorCol[index % 3], ExcelHelper.ColorWhite, 0, "");
                                }
                                worksheet.Cells[1, colR + 1, 2, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[1, colR + 1, 2, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                index++;
                            }
                        }
                        #endregion
                        col++;
                    }


                    if (worksheet.Dimension != null)
                    {
                        for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                        {
                            for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                            {
                                worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
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

        //private string PriceHistory_ExportMulUser(List<int> listCusId, int transportModeID, int typePrice)
        //{
        //    try
        //    {
        //        DTOCUSPrice_HistoryData result = new DTOCUSPrice_HistoryData();
        //        ServiceFactory.SVCustomer((ISVCustomer sv) =>
        //        {
        //            result = sv.PriceHistory_GetDataMulUser(listCusId, transportModeID, typePrice);
        //        });

        //        string[] colorCol = new string[] { ExcelHelper.ColorGreen, ExcelHelper.ColorBlue, ExcelHelper.ColorOrange };

        //        int iFCL = -(int)SYSVarType.TransportModeFCL;
        //        int iFTL = -(int)SYSVarType.TransportModeFTL;
        //        int iLTL = -(int)SYSVarType.TransportModeLTL;

        //        string file = "/" + FolderUpload.Export + "ExportPriceHistoryCustomer_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

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
        #endregion

        #region  CUSSettingExtReturn
        [HttpPost]
        public DTOResult CUSSettingExtReturn_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingExtReturn_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSSettingExtReturn CUSSettingExtReturn_Get(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingExtReturn result = new DTOCUSSettingExtReturn();
                int id = (int)dynParam.id;
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    result = sv.CUSSettingExtReturn_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingExtReturn_Save(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOCUSSettingExtReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingExtReturn>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingExtReturn_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CUSSettingExtReturn_Delete(dynamic dynParam)
        {
            try
            {
                DTOCUSSettingExtReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingExtReturn>(dynParam.item.ToString());
                ServiceFactory.SVCustomer((ISVCustomer sv) =>
                {
                    sv.CUSSettingExtReturn_Delete(item);
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