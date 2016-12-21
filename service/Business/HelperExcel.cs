using Data;
using DTO;
using ExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Business
{
    public class HelperExcel
    {
        public const string ColorBlack = "#000000";
        public const string ColorWhite = "#FFFFFF";
        public const string ColorRed = "#FF0000";
        public const string ColorYellow = "#FFFF00";
        public const string ColorGreen = "#008000";
        public const string ColorLightGreen = "#90EE90";
        public const string ColorBlue = "#0000FF";
        public const string ColorLightSkyBlue = "#87CEFA";
        public const string ColorCrimson = "#DC143C";
        public const string ColorCyan = "#00FFFF";
        public const string ColorOrange = "#FFA500";
        public const string ColorGray = "#808080";
        public const string ColorPlatinum = "#E5E4E2";
        public const double FontSize10 = 10;
        public const double FontSize11 = 11;
        public const double FontSize12 = 12;
        public const double FontSize16 = 16;
        public const string TextAlignLeft = "left";
        public const string TextAlignCenter = "center";
        public const string TextAlignRight = "right";
        public const string VerticalAlignTop = "top";
        public const string VerticalAlignCenter = "center";
        public const string FontFamilyArial = "Arial";
        public const string FontFamilyTahoma = "Tahoma";
        public const string FormatMMYY = "MM/yy";
        public const string FormatDDMM = "dd/MM";
        public const string FormatDDMMYYYY = "dd/mm/yyyy";
        public const string FormatDMYHM = "dd/MM/yyyy HH:mm";
        public const string FormatHHMM = "HH:mm";
        public const string FormatMoney = "#,##0";
        public const string FormatMoneySpecial = "_(* #,##0_);_(* (#,##0);_(* \" - \"??_);_(@_)";
        public const string FormatNumber = "#";
        public const string FormatNumber1 = "#,##0.0";
        public const string FormatNumber2 = "#,##0.00";
        public const string FormatNumber3 = "#,##0.000";
        public const string FormatNumber4 = "#,##0.0000";
        public const string FormatNumber5 = "#,##0.00000";
        public const string FormatNumber6 = "#,##0.000000";

        private static string[] _excelColumn = new string[] { 
            "","A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
            "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
            "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ"};

        public static string AddressColumns(int row, int colFrom, int colTo)
        {
            return _excelColumn[colFrom + 1] + (row + 1) + ":" + _excelColumn[colTo + 1] + (row + 1);
        }

        public static string AddressRows(int col, int rowFrom, int rowTo)
        {
            return _excelColumn[col + 1] + (rowFrom + 1) + ":" + _excelColumn[col + 1] + (rowTo + 1);
        }

        public static string AddressRange(int rowFrom, int rowTo, int colFrom, int colTo)
        {
            return _excelColumn[colFrom + 1] + (rowFrom + 1) + ":" + _excelColumn[colTo + 1] + (rowTo + 1);
        }

        public static long GetLastID(DataEntities model, int functionid, string functionkey)
        {
            var obj = model.SYS_Excel.Where(c => c.FunctionID == functionid && c.FunctionKey == functionkey).Select(c => new
            {
                c.ID,
                c.ModifiedDate
            }).FirstOrDefault();
            if (obj != null)
            {
                if (obj.ModifiedDate.Value.CompareTo(DateTime.Now.AddMinutes(-10)) > 0)
                    return obj.ID;
            }
            return -1;
        }

        public static void RemoveByKey(DataEntities model, int functionid, string functionkey)
        {
            if (!string.IsNullOrEmpty(functionkey))
            {
                var obj = model.SYS_Excel.FirstOrDefault(c => c.FunctionID == functionid && c.FunctionKey == functionkey);
                if (obj != null)
                {
                    model.SYS_Excel.Remove(obj);
                }
            }
        }

        public static SYSExcel GetByID(DataEntities model, long id)
        {
            return model.SYS_Excel.Where(c => c.ID == id).Select(c => new SYSExcel
            {
                ID = c.ID,
                FunctionID = c.FunctionID,
                FunctionKey = c.FunctionKey,
                Data = c.Data
            }).FirstOrDefault();
        }

        public static List<Worksheet> GetWorksheetByID(DataEntities model, long id)
        {
            var obj = model.SYS_Excel.Where(c => c.ID == id).Select(c => new
            {
                Data = c.Data
            }).FirstOrDefault();
            if (obj != null)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(obj.Data);
            else
            {
                List<Worksheet> result = new List<Worksheet>();
                var ws = new Worksheet();
                ws.Rows = new List<Row>();
                ws.Columns = NewColumns(new double[] { 80, 80, 80, 80 });
                ws.Selection = "A1:A1";
                ws.ActiveCell = "A1:A1";
                ws.MergedCells = new List<string>();
                ws.Name = "Sheet1";
                result.Add(ws);
                ws = new Worksheet();
                ws.Rows = new List<Row>();
                ws.Columns = NewColumns(new double[] { 80, 80, 80, 80 });
                ws.Selection = "A1:A1";
                ws.ActiveCell = "A1:A1";
                ws.MergedCells = new List<string>();
                ws.Name = "Sheet2";
                result.Add(ws);
                ws = new Worksheet();
                ws.Rows = new List<Row>();
                ws.Columns = NewColumns(new double[] { 80, 80, 80, 80 });
                ws.Selection = "A1:A1";
                ws.ActiveCell = "A1:A1";
                ws.MergedCells = new List<string>();
                ws.Name = "Sheet3";
                result.Add(ws);
                return result;
            }
        }

        public static List<Worksheet> ChangeText(DataEntities model, long id, int row, int col, string val)
        {
            var obj = model.SYS_Excel.FirstOrDefault(c => c.ID == id);
            if (obj != null && !string.IsNullOrEmpty(obj.Data))
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(obj.Data);
                if (lst.Count > 0)
                {
                    var excelRow = lst[0].Rows.FirstOrDefault(c => c.Index == row);
                    if (excelRow == null)
                    {
                        excelRow = NewRow(row, null);
                        lst[0].Rows.Add(excelRow);
                    }
                    var excelCell = excelRow.Cells.FirstOrDefault(c => c.Index == col);
                    if (excelCell == null)
                    {
                        excelCell = NewCell(col, val);
                        lst[0].Rows[row].Cells.Add(excelCell);
                    }
                    excelCell.Value = val;
                    obj.Data = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
                    model.SaveChanges();

                    return lst;
                }
            }
            return new List<Worksheet>();
        }

        public static List<Row> GetSuccess(DataEntities model, long id, int rowStart, int idxcheck, int idxchange)
        {
            var result = new List<Row>();
            var obj = model.SYS_Excel.FirstOrDefault(c => c.ID == id);
            if (obj != null && !string.IsNullOrEmpty(obj.Data))
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(obj.Data);
                if (lst.Count > 0)
                {
                    foreach (var excelRow in lst[0].Rows)
                    {
                        if (excelRow.Index >= rowStart)
                        {
                            var cell = excelRow.Cells.FirstOrDefault(c => c.Index == idxcheck);
                            if (cell != null && cell.Value != null && cell.Value.ToString() == "x")
                            {
                                cell = excelRow.Cells.FirstOrDefault(c => c.Index == idxchange);
                                if (cell != null && cell.Value != null && string.IsNullOrEmpty(cell.Value.ToString()))
                                    result.Add(excelRow);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static SYSExcel GetByKey(DataEntities model, int functionid, string functionkey)
        {
            var result = new SYSExcel();

            result = model.SYS_Excel.Where(c => c.FunctionID == functionid && c.FunctionKey == functionkey).Select(c => new SYSExcel
            {
                ID = c.ID,
                FunctionID = c.FunctionID,
                FunctionKey = c.FunctionKey,
                Data = c.Data
            }).FirstOrDefault();
            if (result == null)
            {
                result = new SYSExcel();
                result.FunctionID = functionid;
                result.FunctionKey = functionkey;
            }
            return result;
        }

        public static SYSExcel Save(DataEntities model, AccountItem Account, SYSExcel item)
        {
            if (item != null)
            {
                var obj = model.SYS_Excel.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new SYS_Excel();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                }
                obj.ModifiedBy = Account.UserName;
                obj.ModifiedDate = DateTime.Now;
                obj.FunctionID = item.FunctionID;
                obj.FunctionKey = item.FunctionKey;
                obj.Data = item.Data;
                if (obj.ID < 1)
                    model.SYS_Excel.Add(obj);
                model.SaveChanges();

                item.ID = obj.ID;
            }
            return item;
        }

        public static Row NewRow(int index, List<Cell> cells)
        {
            var result = new Row();
            result.Index = index;
            if (cells != null && cells.Count > 0)
            {
                for (int indexCell = 0; indexCell < cells.Count; indexCell++)
                {
                    if (cells[indexCell].Index < 0)
                        cells[indexCell].Index = indexCell;
                }
                result.Cells = cells.ToList();
            }
            else
                result.Cells = new List<Cell>();
            return result;
        }

        public static List<Column> NewColumns(double[] widths)
        {
            var result = new List<Column>();
            if (widths != null)
            {
                for (int index = 0; index < widths.Length; index++)
                {
                    result.Add(NewColum(index, widths[index]));
                }
            }
            return result;
        }

        public static Column NewColum(int index, double? width)
        {
            var result = new Column();
            result.Index = index;
            result.Width = width;
            return result;
        }

        public static Cell NewCell(object val)
        {
            return NewCell(-1, val);
        }

        public static Cell NewCell(object val, string color, string background)
        {
            return NewCell(-1, val, color, background);
        }

        public static Cell NewCell(int col, object val)
        {
            return NewCell(col, val, ColorBlack, ColorWhite);
        }

        public static Cell NewCell(int col, object val, string color, string background)
        {
            return NewCell(col, val, color, background, "", null);
        }

        public static Cell NewCell(int col, object val, string color, string background, string format)
        {
            return NewCell(col, val, color, background, format, null);
        }

        public static Cell NewCell(int col, object val, string color, string background, string format, bool? enable)
        {
            return NewCell(col, val, FontSize10, color, null, null, background, format, "", enable);
        }

        public static Cell NewCell(int col, object val, double fontsize, string color, bool? bold, bool? italic, string background,
            string format, string textalign, bool? enable)
        {
            return NewCell(col, val, fontsize, color, bold, italic, background, format, textalign, null, null, enable);
        }

        public static Cell NewCell(int col, object val, double fontsize, string color, bool? bold, bool? italic, string background,
              string format, string textalign, bool? wrap, bool? underline, bool? enable)
        {
            return NewCell(col, val, FontFamilyArial, fontsize, color, bold, italic, background, format, textalign, "", wrap, underline, enable);
        }

        public static Cell NewCell(int col, object val, string fontfamily, double fontsize, string color, bool? bold, bool? italic, string background,
             string format, string textalign, string verticalalign, bool? wrap, bool? underline, bool? enable)
        {
            var result = new Cell();
            result.Index = col;
            result.Value = val;
            result.Background = background;
            result.FontFamily = fontfamily;
            result.FontSize = fontsize;
            result.Color = color;
            if (bold != null)
                result.Bold = bold.Value;
            if (enable != null)
                result.Enable = enable.Value;
            if (!string.IsNullOrEmpty(format))
                result.Format = format;
            if (italic != null)
                result.Italic = italic.Value;
            if (!string.IsNullOrEmpty(textalign))
                result.TextAlign = textalign;
            if (!string.IsNullOrEmpty(verticalalign))
                result.VerticalAlign = verticalalign;
            if (wrap != null)
                result.Wrap = wrap.Value;
            if (underline != null)
                result.Underline = underline.Value;
            /// result.Validation = new Validation();
            return result;
        }

        public static string GetString(Row row, int index)
        {
            var cell = row.Cells.FirstOrDefault(c => c.Index == index);
            if (cell != null && cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                return cell.Value.ToString().Trim();
            else
                return "";
        }

        public static int GetInt(Row row, int index)
        {
            return Convert.ToInt32(GetString(row, index));
        }

        public static void SetString(Row row, int index, string value)
        {
            var cell = row.Cells.FirstOrDefault(c => c.Index == index);
            if (cell == null)
            {
                cell = NewCell(index, value);
                row.Cells.Add(cell);
            }
            cell.Value = value;
        }

        public static void CheckErrorFail(Row row, int idxcheck, int idxnote, string value)
        {
            var cell = row.Cells.FirstOrDefault(c => c.Index == idxcheck);
            if (cell == null)
            {
                cell = NewCell(idxcheck, "x");
                row.Cells.Add(cell);
            }
            cell.Value = "x";
            cell.Color = ColorRed;
            cell.Background = ColorWhite;

            cell = row.Cells.FirstOrDefault(c => c.Index == idxnote);
            if (cell == null)
            {
                cell = NewCell(idxnote, "");
                row.Cells.Add(cell);
            }
            cell.Value = value;
            cell.Color = ColorWhite;
            cell.Background = ColorRed;
        }

        public static void CheckErrorSuccess(Row row, int idxcheck, int idxnote, int idxid, params string[] lstvalid)
        {
            var cell = row.Cells.FirstOrDefault(c => c.Index == idxcheck);
            if (cell == null)
            {
                cell = NewCell(idxcheck, "x");
                row.Cells.Add(cell);
            }
            cell.Value = "x";
            cell.Color = ColorRed;
            cell.Background = ColorWhite;

            cell = row.Cells.FirstOrDefault(c => c.Index == idxnote);
            if (cell == null)
            {
                cell = NewCell(idxnote, "");
                row.Cells.Add(cell);
            }
            cell.Value = "";
            cell.Color = ColorWhite;
            cell.Background = ColorGreen;

            cell = row.Cells.FirstOrDefault(c => c.Index == idxid);
            if (cell == null)
            {
                cell = NewCell(idxid, "");
                row.Cells.Add(cell);
            }
            if (lstvalid != null)
            {
                foreach (var valid in lstvalid)
                {
                    cell = row.Cells.FirstOrDefault(c => c.Index == idxid);
                    if (cell == null)
                    {
                        cell = NewCell(idxid, "");
                        row.Cells.Add(cell);
                    }
                    cell.Value = valid;
                    idxid++;
                }
            }
        }
        public static void CheckErrorSuccessNotLog(Row row, int idxcheck, int idxnote)
        {
            var cell = row.Cells.FirstOrDefault(c => c.Index == idxcheck);
            if (cell == null)
            {
                cell = NewCell(idxcheck, "x");
                row.Cells.Add(cell);
            }
            cell.Value = "x";
            cell.Color = ColorRed;
            cell.Background = ColorWhite;

            cell = row.Cells.FirstOrDefault(c => c.Index == idxnote);
            if (cell == null)
            {
                cell = NewCell(idxnote, "");
                row.Cells.Add(cell);
            }
            cell.Value = "";
            cell.Color = ColorWhite;
            cell.Background = ColorGreen;
        }

        public static void SaveData(DataEntities model, long id, List<Worksheet> lst)
        {
            var obj = model.SYS_Excel.FirstOrDefault(c => c.ID == id);
            if (obj != null)
            {
                obj.Data = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
                model.SaveChanges();
            }
        }

        public static void ClearData(List<Row> lst, int idxcheck)
        {
            foreach (var item in lst)
            {
                if (item.Cells.Where(c => c.Index < idxcheck).Count() > 0)
                    item.Cells = item.Cells.Where(c => c.Index >= idxcheck).ToList();
            }
        }

        public static Row ClearData(Row row, int idxcheck)
        {
            if (row != null && idxcheck > 0)
            {
                row.Cells = row.Cells.Where(c => c.Index >= idxcheck).ToList();
            }
            return row;
        }

        public static bool IsValidCode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;
            return text.All(c => c >= 32 && c < 127);
        }

        public static bool IsValidRegNo(string RegNo)
        {
            RegNo = RegNo.Trim();
            string p4Num = @"\d{2}[a-zA-Z]-\d{4}$";//29c-1234
            string p5Num = @"\d{2}[a-zA-Z]-\d{5}$";//29c-12345
            string p4Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{4}$";//29LD-1234
            string p5Num2C = @"\d{2}[a-zA-Z][a-zA-Z]-\d{5}$";//29LD-12345
            bool passed = false;
            if (Regex.Match(RegNo, p4Num).Success) { passed = true; }
            if (Regex.Match(RegNo, p5Num).Success) { passed = true; }
            if (Regex.Match(RegNo, p4Num2C).Success) { passed = true; }
            if (Regex.Match(RegNo, p5Num2C).Success) { passed = true; }
            return passed;
        }

        public static DateTime? ValueToDate(string value)
        {
            try
            {
                return DateTime.FromOADate(Convert.ToDouble(value));
            }
            catch
            {
                try
                {
                    return Convert.ToDateTime(value, new CultureInfo("vi-VN"));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool Valid(string strData, ValidType validType, Row checkRow, int colCheckChange, int colCheckNote, int indexMessage, List<string> lstMessageError,
            bool isRequire = false, int maxStringLength = 0, object minValue = null, object maxValue = null)
        {
            bool flag = true;
            if (isRequire == true && string.IsNullOrEmpty(strData))
            {
                flag = false;
                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexMessage, lstMessageError));
            }
            if (flag && !string.IsNullOrEmpty(strData))
            {
                switch (validType)
                {
                    case ValidType.String:
                        if (maxStringLength > 0 && strData.Length > maxStringLength)
                        {
                            flag = false;
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexMessage, lstMessageError));
                        }
                        break;
                    case ValidType.Int32:
                        try
                        {
                            Convert.ToInt32(strData);
                        }
                        catch
                        {
                            flag = false;
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexMessage, lstMessageError));
                        }
                        break;
                    case ValidType.Int64:
                        try
                        {
                            Convert.ToInt64(strData);
                        }
                        catch
                        {
                            flag = false;
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexMessage, lstMessageError));
                        }
                        break;
                    case ValidType.Double:
                        try
                        {
                            Convert.ToDouble(strData);
                        }
                        catch
                        {
                            flag = false;
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexMessage, lstMessageError));
                        }
                        break;
                    case ValidType.Decimal:
                        try
                        {
                            Convert.ToDecimal(strData);
                        }
                        catch
                        {
                            flag = false;
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexMessage, lstMessageError));
                        }
                        break;
                    case ValidType.DateTime:
                        try
                        {
                            HelperExcel.ValueToDate(strData);
                        }
                        catch (Exception ex)
                        {
                            flag = false;
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexMessage, lstMessageError, strData, ex.Message));
                        }
                        break;
                    default:
                        throw new Exception("Unknow");
                }
            }
            return flag;
        }

        public enum ValidType
        {
            String,
            Int32,
            Int64,
            Double,
            Decimal,
            DateTime,

        }

        public static string MessageError(int index, List<string> lstMessageError, params object[] pars)
        {
            string result = string.Empty;
            if (lstMessageError.Count > index)
            {
                result = lstMessageError[index];
                if (!string.IsNullOrEmpty(result))
                {
                    if (pars != null)
                        result = string.Format(result, pars);
                }
                else
                    result = string.Empty;
            }
            return result;
        }


        public static DateTime ValueToDateVN(string value)
        {
            try
            {
                return DateTime.FromOADate(Convert.ToDouble(value));
            }
            catch
            {
                try
                {
                    return Convert.ToDateTime(value, new CultureInfo("vi-VN"));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
