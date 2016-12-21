using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWeb
{
    public class ExcelHelper
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
        public const short SizeNormal = 10;
        public const string FormatString = "@";
        public const string FormatMMYY = "MM/yy";
        public const string FormatDDMM = "dd/MM";
        public const string FormatDDMMYYYY = "dd/MM/yyyy";
        public const string FormatDMYHM = "dd/MM/yyyy HH:mm";
        public const string FormatHHMM = "HH:mm";
        public const string FormatMoney = "#,##0";
        public const string FormatMoneySpecial = "_(* #,##0_);_(* (#,##0);_(* \" - \"??_);_(@_)";
        public const string FormatNumber = "#,##0.000";
        public const string FontArial = "Arial";
        public const string FontTahoma = "Tahoma";

        private static string[] _excelColumn = new string[] { 
            "","A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
            "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
            "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ"};

        public static void CreateCellStyle(OfficeOpenXml.ExcelWorksheet worksheet, int row, int col, bool isMerge = false, bool isBold = false, string backgroundColor = "", string forceColor = "", float fontSize = 0, string format = "")
        {
            CreateCellStyle(worksheet.Cells[row, col], isMerge, isBold, backgroundColor, forceColor, fontSize, format);
        }

        public static void CreateCellStyle(OfficeOpenXml.ExcelWorksheet worksheet, int fromRow, int fromCol, int toRow, int toCol, bool isMerge = false, bool isBold = false, string backgroundColor = "", string forceColor = "", float fontSize = 0, string format = "")
        {
            CreateCellStyle(worksheet.Cells[fromRow, fromCol, toRow, toCol], isMerge, isBold, backgroundColor, forceColor, fontSize, format);
        }

        public static void CreateFormat(OfficeOpenXml.ExcelWorksheet worksheet, int row, int col, string format, string backgroundColor = "", string forceColor = "")
        {
            CreateFormat(worksheet.Cells[row, col], format, backgroundColor, forceColor);
        }

        public static void CreateFormat(OfficeOpenXml.ExcelWorksheet worksheet, int fromRow, int fromCol, int toRow, int toCol, string format, string backgroundColor = "", string forceColor = "")
        {
            CreateFormat(worksheet.Cells[fromRow, fromCol, toRow, toCol], format, backgroundColor, forceColor);
        }

        private static void CreateFormat(OfficeOpenXml.ExcelRange cell, string format, string backgroundColor = "", string forceColor = "")
        {
            if (format != string.Empty)
                cell.Style.Numberformat.Format = format;
            if (backgroundColor != string.Empty)
            {
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(backgroundColor));
            }
            if (forceColor != string.Empty)
                cell.Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml(forceColor));
        }

        public static void CopyStyle(OfficeOpenXml.ExcelWorksheet worksheet, int sourceRow, int sourceCol, int targetRow, int targetCol)
        {
            OfficeOpenXml.ExcelRange cellTarget = worksheet.Cells[targetRow, targetCol];
            OfficeOpenXml.ExcelRange cellSource = worksheet.Cells[sourceRow, sourceCol];

            if (cellSource.Style.Border.Top != null && cellSource.Style.Border.Top.Style != OfficeOpenXml.Style.ExcelBorderStyle.None)
            {
                if (cellSource.Style.Border.Top.Style != null)
                    cellTarget.Style.Border.Top.Style = cellSource.Style.Border.Top.Style;
                if (cellSource.Style.Border.Top.Color.Indexed > 0)
                    cellTarget.Style.Border.Top.Color.Indexed = cellSource.Style.Border.Top.Color.Indexed;
            }
            if (cellSource.Style.Border.Bottom != null && cellSource.Style.Border.Bottom.Style != OfficeOpenXml.Style.ExcelBorderStyle.None)
            {
                if (cellSource.Style.Border.Bottom.Style != null)
                    cellTarget.Style.Border.Bottom.Style = cellSource.Style.Border.Bottom.Style;
                if (cellSource.Style.Border.Bottom.Color.Indexed > 0)
                    cellTarget.Style.Border.Bottom.Color.Indexed = cellSource.Style.Border.Bottom.Color.Indexed;
            }
            if (cellSource.Style.Border.Left != null && cellSource.Style.Border.Left.Style != OfficeOpenXml.Style.ExcelBorderStyle.None)
            {
                if (cellSource.Style.Border.Left.Style != null)
                    cellTarget.Style.Border.Left.Style = cellSource.Style.Border.Left.Style;
                if (cellSource.Style.Border.Left.Color.Indexed > 0)
                    cellTarget.Style.Border.Left.Color.Indexed = cellSource.Style.Border.Left.Color.Indexed;
            }
            if (cellSource.Style.Border.Right != null && cellSource.Style.Border.Right.Style != OfficeOpenXml.Style.ExcelBorderStyle.None)
            {
                if (cellSource.Style.Border.Right.Style != null)
                    cellTarget.Style.Border.Right.Style = cellSource.Style.Border.Right.Style;
                if (cellSource.Style.Border.Right.Color.Indexed > 0)
                    cellTarget.Style.Border.Right.Color.Indexed = cellSource.Style.Border.Right.Color.Indexed;
            }

            string htmlColor = string.Empty;
            if (cellSource.Style.Fill.PatternType != null && cellSource.Style.Fill.PatternType != OfficeOpenXml.Style.ExcelFillStyle.None)
            {
                cellTarget.Style.Fill.PatternType = cellSource.Style.Fill.PatternType;
                if (cellSource.Style.Fill.PatternColor.Indexed > 0)
                {
                    htmlColor = cellSource.Style.Fill.PatternColor.LookupColor(cellSource.Style.Fill.PatternColor);
                    if (!string.IsNullOrEmpty(htmlColor))
                    {
                        if (htmlColor.Length > 7)
                            cellTarget.Style.Fill.PatternColor.SetColor(HexColor(htmlColor));
                        else
                            cellTarget.Style.Fill.PatternColor.SetColor(System.Drawing.ColorTranslator.FromHtml(htmlColor));
                    }
                }

                if (cellSource.Style.Fill.BackgroundColor.Indexed > 0)
                {
                    htmlColor = cellSource.Style.Fill.BackgroundColor.LookupColor(cellSource.Style.Fill.BackgroundColor);
                    if (!string.IsNullOrEmpty(htmlColor))
                    {
                        if (htmlColor.Length > 7)
                            cellTarget.Style.Fill.BackgroundColor.SetColor(HexColor(htmlColor));
                        else
                            cellTarget.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(htmlColor));
                    }
                }
            }

            cellTarget.Style.Font = cellSource.Style.Font;
            if (cellSource.Style.Font.Color.Indexed > 0)
            {
                htmlColor = cellSource.Style.Font.Color.LookupColor(cellSource.Style.Font.Color);
                if (!string.IsNullOrEmpty(htmlColor))
                {
                    if (htmlColor.Length > 7)
                        cellTarget.Style.Font.Color.SetColor(HexColor(htmlColor));
                    else
                        cellTarget.Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml(htmlColor));
                }
            }

            cellTarget.Style.HorizontalAlignment = cellSource.Style.HorizontalAlignment;
            cellTarget.Style.Locked = cellSource.Style.Locked;
            cellTarget.Style.Numberformat.Format = cellSource.Style.Numberformat.Format;
            cellTarget.Style.ShrinkToFit = cellSource.Style.ShrinkToFit;
            cellTarget.Style.TextRotation = cellSource.Style.TextRotation;
            cellTarget.Style.VerticalAlignment = cellSource.Style.VerticalAlignment;
            cellTarget.Style.WrapText = cellSource.Style.WrapText;
        }

        //public static void CopyStyle(OfficeOpenXml.ExcelWorksheet worksheet, int sourceFromRow, int sourceFromCol, int targetFromRow, int targetFromCol, int targetToRow, int targetToCol)
        //{
        //    OfficeOpenXml.ExcelRange cellTarget = worksheet.Cells[targetFromRow, targetFromCol, targetToRow, targetToCol];
        //    OfficeOpenXml.ExcelRange cellSource = worksheet.Cells[sourceFromRow, sourceFromCol];


        //    cellTarget.Style.Border.Top.Color.Indexed = cellSource.Style.Border.Top.Color.Indexed;
        //    //cellTarget.Style.Border.Top.Color.Rgb = cellSource.Style.Border.Top.Color.Rgb;
        //    cellTarget.Style.Border.Top.Style = cellSource.Style.Border.Top.Style;
        //    cellTarget.Style.Border.Bottom.Color.Indexed = cellSource.Style.Border.Bottom.Color.Indexed;
        //    //cellTarget.Style.Border.Bottom.Color.Rgb = cellSource.Style.Border.Bottom.Color.Rgb;
        //    cellTarget.Style.Border.Bottom.Style = cellSource.Style.Border.Bottom.Style;
        //    cellTarget.Style.Border.Left.Color.Indexed = cellSource.Style.Border.Left.Color.Indexed;
        //    //cellTarget.Style.Border.Left.Color.Rgb = cellSource.Style.Border.Left.Color.Rgb;
        //    cellTarget.Style.Border.Left.Style = cellSource.Style.Border.Left.Style;
        //    cellTarget.Style.Border.Right.Color.Indexed = cellSource.Style.Border.Right.Color.Indexed;
        //    //cellTarget.Style.Border.Right.Color.Rgb = cellSource.Style.Border.Right.Color.Rgb;
        //    cellTarget.Style.Border.Right.Style = cellSource.Style.Border.Right.Style;

        //    cellTarget.Style.Fill.BackgroundColor.Indexed = cellSource.Style.Fill.BackgroundColor.Indexed;
        //    //cellTarget.Style.Fill.BackgroundColor.Rgb = cellSource.Style.Fill.BackgroundColor.Rgb;
        //    cellTarget.Style.Fill.Gradient.Top = cellSource.Style.Fill.Gradient.Top;
        //    cellTarget.Style.Fill.Gradient.Bottom = cellSource.Style.Fill.Gradient.Bottom;
        //    cellTarget.Style.Fill.Gradient.Left = cellSource.Style.Fill.Gradient.Left;
        //    cellTarget.Style.Fill.Gradient.Right = cellSource.Style.Fill.Gradient.Right;

        //    cellTarget.Style.Font.Bold = cellSource.Style.Font.Bold;
        //    cellTarget.Style.Font.Color.Indexed = cellSource.Style.Font.Color.Indexed;
        //    //cellTarget.Style.Font.Color.Rgb = cellSource.Style.Font.Color.Rgb;
        //    cellTarget.Style.Font.Family = cellSource.Style.Font.Family;
        //    cellTarget.Style.Font.Italic = cellSource.Style.Font.Italic;
        //    cellTarget.Style.Font.Name = cellSource.Style.Font.Name;
        //    cellTarget.Style.Font.Size = cellSource.Style.Font.Size;
        //    cellTarget.Style.Font.Strike = cellSource.Style.Font.Strike;
        //    cellTarget.Style.Font.UnderLine = cellSource.Style.Font.UnderLine;
        //    cellTarget.Style.Font.UnderLineType = cellSource.Style.Font.UnderLineType;
        //    cellTarget.Style.Font.VerticalAlign = cellSource.Style.Font.VerticalAlign;

        //    cellTarget.Style.HorizontalAlignment = cellSource.Style.HorizontalAlignment;
        //    cellTarget.Style.Locked = cellSource.Style.Locked;
        //    cellTarget.Style.Numberformat.Format = cellSource.Style.Numberformat.Format;
        //    cellTarget.Style.ShrinkToFit = cellSource.Style.ShrinkToFit;
        //    cellTarget.Style.TextRotation = cellSource.Style.TextRotation;
        //    cellTarget.Style.VerticalAlignment = cellSource.Style.VerticalAlignment;
        //    cellTarget.Style.WrapText = cellSource.Style.WrapText;
        //}

        public static DateTime ValueToDate(string value)
        {
            return DateTime.FromOADate(Convert.ToDouble(value));
        }

        public static DateTime ValueToDate(double value)
        {
            return DateTime.FromOADate(Convert.ToDouble(value));
        }

        private static void CreateCellStyle(OfficeOpenXml.ExcelRange cell, bool isMerge = false, bool isBold = false, string backgroundColor = "", string forceColor = "", float fontSize = 0, string format = "")
        {
            if (isBold)
                cell.Style.Font.Bold = true;
            if (backgroundColor != string.Empty)
            {
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(backgroundColor));
            }
            if (forceColor != string.Empty)
                cell.Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml(forceColor));
            if (fontSize > 0)
                cell.Style.Font.Size = fontSize;
            if (isMerge)
                cell.Merge = isMerge;
            if (format != string.Empty)
                cell.Style.Numberformat.Format = format;
        }

        public static OfficeOpenXml.ExcelWorksheet GetWorksheetByIndex(OfficeOpenXml.ExcelPackage package, int index)
        {
            try
            {
                OfficeOpenXml.ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                    if (workBook.Worksheets.Count > 0)
                        return workBook.Worksheets[index];
            }
            catch { }
            return null;
        }

        public static string GetValue(OfficeOpenXml.ExcelWorksheet worksheet, int row, int col)
        {
            if (worksheet.Cells[row, col].Value != null)
                return worksheet.Cells[row, col].Value.ToString().Trim();
            return string.Empty;
        }

        public static string GetColumnName(int col)
        {
            return _excelColumn[col];
        }

        private static System.Drawing.Color HexColor(String hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return System.Drawing.Color.FromArgb(a, r, g, b);
        }
    }

    public class SpreadHelper
    {

    }
}