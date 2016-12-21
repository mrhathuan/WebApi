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
using System.Web;
using System.IO;

namespace Business
{
    public class HelperKPI
    {
        #region KPITime
        public static DateTime? KPITime_CheckDate(string strExpr, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                DateTime? result = null;
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (item.KPICode != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest;
                strExp.Replace("[DateRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value.Date;
                strExp.Replace("[DateRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateRequest.Value.Hour + "," + item.DateRequest.Value.Minute + "," + item.DateRequest.Value.Second + ")";
                strExp.Replace("[DateRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN;
                strExp.Replace("[DateDN]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value.Date;
                strExp.Replace("[DateDN.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateDN.Value.Hour + "," + item.DateDN.Value.Minute + "," + item.DateDN.Value.Second + ")";
                strExp.Replace("[DateDN.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome;
                strExp.Replace("[DateFromCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value.Date;
                strExp.Replace("[DateFromCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromCome.Value.Hour + "," + item.DateFromCome.Value.Minute + "," + item.DateFromCome.Value.Second + ")";
                strExp.Replace("[DateFromCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave;
                strExp.Replace("[DateFromLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value.Date;
                strExp.Replace("[DateFromLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLeave.Value.Hour + "," + item.DateFromLeave.Value.Minute + "," + item.DateFromLeave.Value.Second + ")";
                strExp.Replace("[DateFromLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                strExp.Replace("[DateFromLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value.Date;
                strExp.Replace("[DateFromLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadStart.Value.Hour + "," + item.DateFromLoadStart.Value.Minute + "," + item.DateFromLoadStart.Value.Second + ")";
                strExp.Replace("[DateFromLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                strExp.Replace("[DateFromLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value.Date;
                strExp.Replace("[DateFromLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadEnd.Value.Hour + "," + item.DateFromLoadEnd.Value.Minute + "," + item.DateFromLoadEnd.Value.Second + ")";
                strExp.Replace("[DateFromLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome;
                strExp.Replace("[DateToCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value.Date;
                strExp.Replace("[DateToCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToCome.Value.Hour + "," + item.DateToCome.Value.Minute + "," + item.DateToCome.Value.Second + ")";
                strExp.Replace("[DateToCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave;
                strExp.Replace("[DateToLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value.Date;
                strExp.Replace("[DateToLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLeave.Value.Hour + "," + item.DateToLeave.Value.Minute + "," + item.DateToLeave.Value.Second + ")";
                strExp.Replace("[DateToLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart;
                strExp.Replace("[DateToLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value.Date;
                strExp.Replace("[DateToLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadStart.Value.Hour + "," + item.DateToLoadStart.Value.Minute + "," + item.DateToLoadStart.Value.Second + ")";
                strExp.Replace("[DateToLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                strExp.Replace("[DateToLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value.Date;
                strExp.Replace("[DateToLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadEnd.Value.Hour + "," + item.DateToLoadEnd.Value.Minute + "," + item.DateToLoadEnd.Value.Second + ")";
                strExp.Replace("[DateToLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice;
                strExp.Replace("[DateInvoice]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value.Date;
                strExp.Replace("[DateInvoice.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateInvoice.Value.Hour + "," + item.DateInvoice.Value.Minute + "," + item.DateInvoice.Value.Second + ")";
                strExp.Replace("[DateInvoice.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest;
                strExp.Replace("[ETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                strExp.Replace("[ETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                strExp.Replace("[ETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                strExp.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                strExp.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                strExp.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                strExp.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                strExp.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                strExp.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                strExp.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                strExp.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                strExp.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                strExp.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                strExp.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                strExp.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD;
                strExp.Replace("[DateOrderETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value.Date;
                strExp.Replace("[DateOrderETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETD.Value.Hour + "," + item.DateOrderETD.Value.Minute + "," + item.DateOrderETD.Value.Second + ")";
                strExp.Replace("[DateOrderETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA;
                strExp.Replace("[DateOrderETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value.Date;
                strExp.Replace("[DateOrderETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETA.Value.Hour + "," + item.DateOrderETA.Value.Minute + "," + item.DateOrderETA.Value.Second + ")";
                strExp.Replace("[DateOrderETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest;
                strExp.Replace("[DateOrderETDRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value.Date;
                strExp.Replace("[DateOrderETADateOrderETDRequestDate]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETDRequest.Value.Hour + "," + item.DateOrderETDRequest.Value.Minute + "," + item.DateOrderETDRequest.Value.Second + ")";
                strExp.Replace("[DateOrderETDRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest;
                strExp.Replace("[DateOrderETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                strExp.Replace("[DateOrderETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                strExp.Replace("[DateOrderETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime;
                strExp.Replace("[DateOrderCutOfTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value.Date;
                strExp.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderCutOfTime.Value.Hour + "," + item.DateOrderCutOfTime.Value.Minute + "," + item.DateOrderCutOfTime.Value.Second + ")";
                strExp.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = zone;
                strExp.Replace("[Zone]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[Zone]", strCol + row);
                if (zone % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }
                row++;

                worksheet.Cells[row, col].Value = leadTime;
                strExp.Replace("[LeadTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[LeadTime]", strCol + row);
                if (leadTime % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }

                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExp.Replace("[" + valEx.Key + "]", strCol + row);
                }

                row++;
                worksheet.Cells[row, col].Formula = strExp.ToString();
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

                if (result != null)
                {
                    if (result.Value.Year == 1900)
                        result = null;
                }
                //package.Save();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool? KPITime_CheckBool(string strExpr, string strFieldr, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                bool? result = null;

                //string file = "/MailTemplate/" + "KPI_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                //    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                //FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                //ExcelPackage package = new ExcelPackage(exportfile);

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);
                StringBuilder strField = new StringBuilder(strFieldr);
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (item.KPICode != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest;
                strExp.Replace("[DateRequest]", strCol + row);
                strField.Replace("[DateRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value.Date;
                strExp.Replace("[DateRequest.Date]", strCol + row);
                strField.Replace("[DateRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateRequest.Value.Hour + "," + item.DateRequest.Value.Minute + "," + item.DateRequest.Value.Second + ")";
                strExp.Replace("[DateRequest.Time]", strCol + row);
                strField.Replace("[DateRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN;
                strExp.Replace("[DateDN]", strCol + row);
                strField.Replace("[DateDN]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value.Date;
                strExp.Replace("[DateDN.Date]", strCol + row);
                strField.Replace("[DateDN.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateDN.Value.Hour + "," + item.DateDN.Value.Minute + "," + item.DateDN.Value.Second + ")";
                strExp.Replace("[DateDN.Time]", strCol + row);
                strField.Replace("[DateDN.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome;
                strExp.Replace("[DateFromCome]", strCol + row);
                strField.Replace("[DateFromCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value.Date;
                strExp.Replace("[DateFromCome.Date]", strCol + row);
                strField.Replace("[DateFromCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromCome.Value.Hour + "," + item.DateFromCome.Value.Minute + "," + item.DateFromCome.Value.Second + ")";
                strExp.Replace("[DateFromCome.Time]", strCol + row);
                strField.Replace("[DateFromCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave;
                strExp.Replace("[DateFromLeave]", strCol + row);
                strField.Replace("[DateFromLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value.Date;
                strExp.Replace("[DateFromLeave.Date]", strCol + row);
                strField.Replace("[DateFromLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLeave.Value.Hour + "," + item.DateFromLeave.Value.Minute + "," + item.DateFromLeave.Value.Second + ")";
                strExp.Replace("[DateFromLeave.Time]", strCol + row);
                strField.Replace("[DateFromLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                strExp.Replace("[DateFromLoadStart]", strCol + row);
                strField.Replace("[DateFromLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value.Date;
                strExp.Replace("[DateFromLoadStart.Date]", strCol + row);
                strField.Replace("[DateFromLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadStart.Value.Hour + "," + item.DateFromLoadStart.Value.Minute + "," + item.DateFromLoadStart.Value.Second + ")";
                strExp.Replace("[DateFromLoadStart.Time]", strCol + row);
                strField.Replace("[DateFromLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                strExp.Replace("[DateFromLoadEnd]", strCol + row);
                strField.Replace("[DateFromLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value.Date;
                strExp.Replace("[DateFromLoadEnd.Date]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadEnd.Value.Hour + "," + item.DateFromLoadEnd.Value.Minute + "," + item.DateFromLoadEnd.Value.Second + ")";
                strExp.Replace("[DateFromLoadEnd.Time]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome;
                strExp.Replace("[DateToCome]", strCol + row);
                strField.Replace("[DateToCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value.Date;
                strExp.Replace("[DateToCome.Date]", strCol + row);
                strField.Replace("[DateToCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToCome.Value.Hour + "," + item.DateToCome.Value.Minute + "," + item.DateToCome.Value.Second + ")";
                strExp.Replace("[DateToCome.Time]", strCol + row);
                strField.Replace("[DateToCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave;
                strExp.Replace("[DateToLeave]", strCol + row);
                strField.Replace("[DateToLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value.Date;
                strExp.Replace("[DateToLeave.Date]", strCol + row);
                strField.Replace("[DateToLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLeave.Value.Hour + "," + item.DateToLeave.Value.Minute + "," + item.DateToLeave.Value.Second + ")";
                strExp.Replace("[DateToLeave.Time]", strCol + row);
                strField.Replace("[DateToLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart;
                strExp.Replace("[DateToLoadStart]", strCol + row);
                strField.Replace("[DateToLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value.Date;
                strExp.Replace("[DateToLoadStart.Date]", strCol + row);
                strField.Replace("[DateToLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadStart.Value.Hour + "," + item.DateToLoadStart.Value.Minute + "," + item.DateToLoadStart.Value.Second + ")";
                strExp.Replace("[DateToLoadStart.Time]", strCol + row);
                strField.Replace("[DateToLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                strExp.Replace("[DateToLoadEnd]", strCol + row);
                strField.Replace("[DateToLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value.Date;
                strExp.Replace("[DateToLoadEnd.Date]", strCol + row);
                strField.Replace("[DateToLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadEnd.Value.Hour + "," + item.DateToLoadEnd.Value.Minute + "," + item.DateToLoadEnd.Value.Second + ")";
                strExp.Replace("[DateToLoadEnd.Time]", strCol + row);
                strField.Replace("[DateToLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice;
                strExp.Replace("[DateInvoice]", strCol + row);
                strField.Replace("[DateInvoice]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value.Date;
                strExp.Replace("[DateInvoice.Date]", strCol + row);
                strField.Replace("[DateInvoice.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateInvoice.Value.Hour + "," + item.DateInvoice.Value.Minute + "," + item.DateInvoice.Value.Second + ")";
                strExp.Replace("[DateInvoice.Time]", strCol + row);
                strField.Replace("[DateInvoice.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest;
                strExp.Replace("[ETARequest]", strCol + row);
                strField.Replace("[ETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                strExp.Replace("[ETARequest.Date]", strCol + row);
                strField.Replace("[ETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                strExp.Replace("[ETARequest.Time]", strCol + row);
                strField.Replace("[ETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                strExp.Replace("[DateTOMasterETD]", strCol + row);
                strField.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                strExp.Replace("[DateTOMasterETD.Date]", strCol + row);
                strField.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                strExp.Replace("[DateTOMasterETD.Time]", strCol + row);
                strField.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                strExp.Replace("[DateTOMasterETA]", strCol + row);
                strField.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                strExp.Replace("[DateTOMasterETA.Date]", strCol + row);
                strField.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                strExp.Replace("[DateTOMasterETA.Time]", strCol + row);
                strField.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                strExp.Replace("[DateTOMasterATD]", strCol + row);
                strField.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                strExp.Replace("[DateTOMasterATD.Date]", strCol + row);
                strField.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                strExp.Replace("[DateTOMasterATD.Time]", strCol + row);
                strField.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                strExp.Replace("[DateTOMasterATA]", strCol + row);
                strField.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                strExp.Replace("[DateTOMasterATA.Date]", strCol + row);
                strField.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                strExp.Replace("[DateTOMasterATA.Time]", strCol + row);
                strField.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD;
                strExp.Replace("[DateOrderETD]", strCol + row);
                strField.Replace("[DateOrderETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value.Date;
                strExp.Replace("[DateOrderETD.Date]", strCol + row);
                strField.Replace("[DateOrderETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETD.Value.Hour + "," + item.DateOrderETD.Value.Minute + "," + item.DateOrderETD.Value.Second + ")";
                strExp.Replace("[DateOrderETD.Time]", strCol + row);
                strField.Replace("[DateOrderETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA;
                strExp.Replace("[DateOrderETA]", strCol + row);
                strField.Replace("[DateOrderETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value.Date;
                strExp.Replace("[DateOrderETA.Date]", strCol + row);
                strField.Replace("[DateOrderETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETA.Value.Hour + "," + item.DateOrderETA.Value.Minute + "," + item.DateOrderETA.Value.Second + ")";
                strExp.Replace("[DateOrderETA.Time]", strCol + row);
                strField.Replace("[DateOrderETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest;
                strExp.Replace("[DateOrderETDRequest]", strCol + row);
                strField.Replace("[DateOrderETDRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value.Date;
                strExp.Replace("[DateOrderETDRequest.Date]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETDRequest.Value.Hour + "," + item.DateOrderETDRequest.Value.Minute + "," + item.DateOrderETDRequest.Value.Second + ")";
                strExp.Replace("[DateOrderETDRequest.Time]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest;
                strExp.Replace("[DateOrderETARequest]", strCol + row);
                strField.Replace("[DateOrderETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                strExp.Replace("[DateOrderETARequest.Date]", strCol + row);
                strField.Replace("[DateOrderETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                strExp.Replace("[DateOrderETARequest.Time]", strCol + row);
                strField.Replace("[DateOrderETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime;
                strExp.Replace("[DateOrderCutOfTime]", strCol + row);
                strField.Replace("[DateOrderCutOfTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value.Date;
                strExp.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderCutOfTime.Value.Hour + "," + item.DateOrderCutOfTime.Value.Minute + "," + item.DateOrderCutOfTime.Value.Second + ")";
                strExp.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = zone;
                strExp.Replace("[Zone]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[Zone]", strCol + row);
                if (zone % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }
                row++;

                worksheet.Cells[row, col].Value = leadTime;
                strExp.Replace("[LeadTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[LeadTime]", strCol + row);
                if (leadTime % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }

                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExp.Replace("[" + valEx.Key + "]", strCol + row);
                    strField.Replace("[" + valEx.Key + "]", strCol + row);
                }

                row++;
                worksheet.Cells[row, col].Formula = strExp.ToString();
                row++;
                worksheet.Cells[row, col].Formula = strField.ToString();
                row++;
                // IF(OR(A20="",A39=""),"N",IF(AND(A20<>"",A39<>"",A20<=A39),"T","F"))
                worksheet.Cells[row, col].Formula = "IF(OR(" + strField.ToString() + "=\"\"," + worksheet.Cells[row - 2, col].Address + "=\"\"),\"N\",IF(AND(" + strField.ToString() + "<>\"\"," + worksheet.Cells[row - 2, col].Address + "<>\"\"," + strField.ToString() + "<=" + worksheet.Cells[row - 2, col].Address + ")" + ",\"T\",\"F\"))";

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                if (val == "T") result = true;
                else if (val == "F") result = false;
                    else if (val == "N") result = null;

                //package.Save();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static ExcelPackage KPITime_GetPackage(string strExpr, string strFieldr, string code, List<DTOContractKPITime> lst, double? zone, double? leadTime)
        {
            try
            {
                //string file = "/MailTemplate/" + "KPI_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                //    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                //FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                //ExcelPackage result = new ExcelPackage(exportfile);

                ExcelPackage result = new ExcelPackage();
                ExcelWorksheet worksheet = result.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);
                StringBuilder strField = new StringBuilder(strFieldr);

                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (code != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                strExp.Replace("[DateRequest]", strCol + row);
                strField.Replace("[DateRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateRequest.Date]", strCol + row);
                strField.Replace("[DateRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateRequest.Time]", strCol + row);
                strField.Replace("[DateRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateDN]", strCol + row);
                strField.Replace("[DateDN]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateDN.Date]", strCol + row);
                strField.Replace("[DateDN.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateDN.Time]", strCol + row);
                strField.Replace("[DateDN.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromCome]", strCol + row);
                strField.Replace("[DateFromCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromCome.Date]", strCol + row);
                strField.Replace("[DateFromCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromCome.Time]", strCol + row);
                strField.Replace("[DateFromCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLeave]", strCol + row);
                strField.Replace("[DateFromLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLeave.Date]", strCol + row);
                strField.Replace("[DateFromLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLeave.Time]", strCol + row);
                strField.Replace("[DateFromLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadStart]", strCol + row);
                strField.Replace("[DateFromLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadStart.Date]", strCol + row);
                strField.Replace("[DateFromLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadStart.Time]", strCol + row);
                strField.Replace("[DateFromLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadEnd]", strCol + row);
                strField.Replace("[DateFromLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadEnd.Date]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadEnd.Time]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToCome]", strCol + row);
                strField.Replace("[DateToCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToCome.Date]", strCol + row);
                strField.Replace("[DateToCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToCome.Time]", strCol + row);
                strField.Replace("[DateToCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLeave]", strCol + row);
                strField.Replace("[DateToLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLeave.Date]", strCol + row);
                strField.Replace("[DateToLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLeave.Time]", strCol + row);
                strField.Replace("[DateToLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadStart]", strCol + row);
                strField.Replace("[DateToLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadStart.Date]", strCol + row);
                strField.Replace("[DateToLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadStart.Time]", strCol + row);
                strField.Replace("[DateToLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadEnd]", strCol + row);
                strField.Replace("[DateToLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadEnd.Date]", strCol + row);
                strField.Replace("[DateToLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadEnd.Time]", strCol + row);
                strField.Replace("[DateToLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateInvoice]", strCol + row);
                strField.Replace("[DateInvoice]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateInvoice.Date]", strCol + row);
                strField.Replace("[DateInvoice.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateInvoice.Time]", strCol + row);
                strField.Replace("[DateInvoice.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ETARequest]", strCol + row);
                strField.Replace("[ETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ETARequest.Date]", strCol + row);
                strField.Replace("[ETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ETARequest.Time]", strCol + row);
                strField.Replace("[ETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETD]", strCol + row);
                strField.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETD.Date]", strCol + row);
                strField.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETD.Time]", strCol + row);
                strField.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateTOMasterETA]", strCol + row);
                strField.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETA.Date]", strCol + row);
                strField.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETA.Time]", strCol + row);
                strField.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateTOMasterATD]", strCol + row);
                strField.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATD.Date]", strCol + row);
                strField.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATD.Time]", strCol + row);
                strField.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateTOMasterATA]", strCol + row);
                strField.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATA.Date]", strCol + row);
                strField.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATA.Time]", strCol + row);
                strField.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETD]", strCol + row);
                strField.Replace("[DateOrderETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETD.Date]", strCol + row);
                strField.Replace("[DateOrderETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETD.Time]", strCol + row);
                strField.Replace("[DateOrderETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETA]", strCol + row);
                strField.Replace("[DateOrderETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETA.Date]", strCol + row);
                strField.Replace("[DateOrderETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETA.Time]", strCol + row);
                strField.Replace("[DateOrderETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETDRequest]", strCol + row);
                strField.Replace("[DateOrderETDRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETDRequest.Date]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETDRequest.Time]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETARequest]", strCol + row);
                strField.Replace("[DateOrderETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETARequest.Date]", strCol + row);
                strField.Replace("[DateOrderETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETARequest.Time]", strCol + row);
                strField.Replace("[DateOrderETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderCutOfTime]", strCol + row);
                strField.Replace("[DateOrderCutOfTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[Zone]", strCol + row);
                strField.Replace("[Zone]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[Zone]", strCol + row);
                if (zone % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                }
                row++;

                strExp.Replace("[LeadTime]", strCol + row);
                strField.Replace("[LeadTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[LeadTime]", strCol + row);
                if (leadTime % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                }

                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExp.Replace("[" + valEx.Key + "]", strCol + row);
                    strField.Replace("[" + valEx.Key + "]", strCol + row);
                }

                row++;
                worksheet.Cells[row, col].Formula = strExp.ToString();
                row++;
                worksheet.Cells[row, col].Formula = strField.ToString();
                row++;
                worksheet.Cells[row, col].Formula = "IF(OR(" + strField.ToString() + "=\"\"," + worksheet.Cells[row - 2, col].Address + "=\"\"),\"N\",IF(AND(" + strField.ToString() + "<>\"\"," + worksheet.Cells[row - 2, col].Address + "<>\"\"," + strField.ToString() + "<=" + worksheet.Cells[row - 2, col].Address + ")" + ",\"T\",\"F\"))";

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KPITime_GetDate(ExcelPackage package, string code, KPI_KPITime item, List<DTOContractKPITime> lst)
        {
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (code != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateRequest.Value.Hour + "," + item.DateRequest.Value.Minute + "," + item.DateRequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateDN != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateDN.Value.Hour + "," + item.DateDN.Value.Minute + "," + item.DateDN.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromCome.Value.Hour + "," + item.DateFromCome.Value.Minute + "," + item.DateFromCome.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLeave.Value.Hour + "," + item.DateFromLeave.Value.Minute + "," + item.DateFromLeave.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadStart.Value.Hour + "," + item.DateFromLoadStart.Value.Minute + "," + item.DateFromLoadStart.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadEnd.Value.Hour + "," + item.DateFromLoadEnd.Value.Minute + "," + item.DateFromLoadEnd.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToCome.Value.Hour + "," + item.DateToCome.Value.Minute + "," + item.DateToCome.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLeave.Value.Hour + "," + item.DateToLeave.Value.Minute + "," + item.DateToLeave.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadStart.Value.Hour + "," + item.DateToLoadStart.Value.Minute + "," + item.DateToLoadStart.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadEnd.Value.Hour + "," + item.DateToLoadEnd.Value.Minute + "," + item.DateToLoadEnd.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateInvoice != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateInvoice.Value.Hour + "," + item.DateInvoice.Value.Minute + "," + item.DateInvoice.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETD.Value.Hour + "," + item.DateOrderETD.Value.Minute + "," + item.DateOrderETD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETA.Value.Hour + "," + item.DateOrderETA.Value.Minute + "," + item.DateOrderETA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETDRequest.Value.Hour + "," + item.DateOrderETDRequest.Value.Minute + "," + item.DateOrderETDRequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderCutOfTime.Value.Hour + "," + item.DateOrderCutOfTime.Value.Minute + "," + item.DateOrderCutOfTime.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;


                worksheet.Cells[row, col].Value = item.Zone;
                if (item.Zone % 1 == 0.5)
                    worksheet.Cells[row, col].Value = item.Zone + 0.01;
                row++;
                worksheet.Cells[row, col].Value = item.LeadTime;
                if (item.LeadTime % 1 == 0.5)
                    worksheet.Cells[row, col].Value = item.LeadTime + 0.01;

                foreach (var valEx in dicEx)
                    row++;

                package.Workbook.Calculate();
                row++;
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                item.KPIDate = null;
                item.IsKPI = null;
                try
                { item.KPIDate = DateTime.FromOADate(Convert.ToDouble(val)); }
                catch { }
                if (item.KPIDate == null)
                {
                    try
                    { item.KPIDate = Convert.ToDateTime(val, new CultureInfo("en-US")).Date; }
                    catch { }
                }
                //if (item.DateRequest != null && item.KPIDate != null)
                //    if (item.DateRequest.Value.AddDays(-2).CompareTo(item.KPIDate.Value) > 0)
                //        item.KPIDate = null;

                row++;
                row++;
                if (item.KPIDate != null)
                {
                    if (item.KPIDate.Value.Year == 1900)
                        item.KPIDate = null;
                    else
                    {
                        val = worksheet.Cells[row, col].Value.ToString().Trim();
                        if (val == "T") item.IsKPI = true;
                        else if (val == "F") item.IsKPI = false;
                        else if (val == "N") item.IsKPI = null;
                    }
                }
                //if (item.OrderID == 6933)
                //    package.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void KPITime_SendOPS(DataEntities model, AccountItem account, List<int> lstOrderID)
        {
            KPITime_Create(model, account, null, lstOrderID, new List<int>());
        }

        public static void KPITime_DIMonitorComplete(DataEntities model, AccountItem account, int ditomasterID)
        {
            KPITime_Create(model, account, ditomasterID, new List<int>(), new List<int>());
            KPIVENTime_Create(model, account, ditomasterID, new List<int>(), new List<int>());
        }

        public static void KPITime_DIPODChange(DataEntities model, AccountItem account, List<int> lstDITOGroupProductID)
        {
            KPITime_Create(model, account, null, new List<int>(), lstDITOGroupProductID);
            KPIVENTime_Create(model, account, null, new List<int>(), lstDITOGroupProductID);
        }

        private static void KPITime_Create(DataEntities model, AccountItem account, int? ditomasterID, List<int> lstOrderID, List<int> lstDITOGroupProductID)
        {
            DateTime? dtNull = null;
            var lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 &&
                    c.DITOMasterID == ditomasterID &&
                    c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        ContractID = c.ORD_GroupProduct.ORD_Order.ContractID.Value,
                        CATRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,
                    }).ToList();
            if (lstOrderID != null && lstOrderID.Count > 0)
            {
                lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 &&
                    lstOrderID.Contains(c.ORD_GroupProduct.OrderID) &&
                    c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        ContractID = c.ORD_GroupProduct.ORD_Order.ContractID.Value,
                        CATRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,
                    }).ToList();
            }
            if (lstDITOGroupProductID != null && lstDITOGroupProductID.Count > 0)
            {
                lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 &&
                    lstDITOGroupProductID.Contains(c.ID) &&
                    c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        ContractID = c.ORD_GroupProduct.ORD_Order.ContractID.Value,
                        CATRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,
                    }).ToList();
            }

            if (lstDITOGroupProduct.Count > 0)
            {
                var lstContractID = lstDITOGroupProduct.Select(c => c.ContractID).Distinct().ToList();

                var lstKPI = model.CAT_ContractKPITime.Where(c => lstContractID.Contains(c.CAT_ContractRouting.ContractID)).Select(c => new DTOContractKPITime
                {
                    CustomerID = c.CAT_ContractRouting.CAT_Contract.CustomerID,
                    ContractID = c.CAT_ContractRouting.ContractID,
                    CATRoutingID = c.CAT_ContractRouting.RoutingID,
                    Expression = c.Expression,
                    CompareField = c.CompareField,
                    Zone = c.CAT_ContractRouting.Zone,
                    LeadTime = c.CAT_ContractRouting.LeadTime,

                    KPIID = c.KPIID,
                    KPICode = c.KPI_KPI.Code
                }).ToList();
                var lstRouting = model.CAT_ContractRouting.Where(c => lstContractID.Contains(c.ContractID)).Select(c => new
                {
                    c.ContractID,
                    CATRoutingID = c.RoutingID,
                    c.Zone,
                    c.LeadTime
                }).ToList();

                //Danh sach cong thuc
                Dictionary<string, OfficeOpenXml.ExcelPackage> dicKPICode = new Dictionary<string, OfficeOpenXml.ExcelPackage>();
                foreach (var item in lstKPI.Distinct())
                {
                    if (!string.IsNullOrEmpty(item.Expression) && !string.IsNullOrEmpty(item.CompareField))
                    {
                        var lst = lstKPI.Where(c => c.ContractID == item.ContractID && c.CATRoutingID == item.CATRoutingID).ToList();
                        dicKPICode.Add(item.ContractID + "_" + item.CATRoutingID + "_" + item.KPICode, KPITime_GetPackage(item.Expression, item.CompareField, item.KPICode, lst, item.Zone, item.LeadTime));
                    }
                }

                //Tinh KPI
                foreach (var itemGroupProduct in lstDITOGroupProduct)
                {
                    var itemRouting = lstRouting.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID);
                    var lstKPIGroup = lstKPI.Where(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID).ToList();

                    if (itemRouting != null && lstKPIGroup.Count > 0)
                    {
                        foreach (var itemKPIGroup in lstKPIGroup)
                        {
                            if (!string.IsNullOrEmpty(itemKPIGroup.Expression) && !string.IsNullOrEmpty(itemKPIGroup.CompareField))
                            {
                                OfficeOpenXml.ExcelPackage kpiCode = default(OfficeOpenXml.ExcelPackage);
                                if (dicKPICode.ContainsKey(itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode))
                                    kpiCode = dicKPICode[itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode];

                                if (kpiCode != null)
                                {
                                    var obj = model.KPI_KPITime.FirstOrDefault(c => c.CustomerID == itemGroupProduct.CustomerID && c.OrderID == itemGroupProduct.OrderID &&
                                        c.DITOGroupProductID == itemGroupProduct.ID);
                                    if (obj == null)
                                    {
                                        obj = new KPI_KPITime();
                                        obj.KPIID = itemKPIGroup.KPIID;
                                        obj.CustomerID = itemGroupProduct.CustomerID;
                                        obj.OrderID = itemGroupProduct.OrderID;
                                        obj.OrderGroupProductID = itemGroupProduct.OrderGroupProductID;
                                        obj.DITOGroupProductID = itemGroupProduct.ID;

                                        obj.CreatedBy = account.UserName;
                                        obj.CreatedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        obj.ModifiedBy = account.UserName;
                                        obj.ModifiedDate = DateTime.Now;
                                    }
                                    obj.DateData = itemGroupProduct.RequestDate;
                                    obj.DateRequest = itemGroupProduct.RequestDate;
                                    obj.DateDN = itemGroupProduct.DateDN;
                                    if (obj.DateDN == null)
                                        obj.DateDN = itemGroupProduct.RequestDate;
                                    obj.DateFromCome = itemGroupProduct.DateFromCome;
                                    obj.DateFromLeave = itemGroupProduct.DateFromLeave;
                                    obj.DateFromLoadStart = itemGroupProduct.DateFromLoadStart;
                                    obj.DateFromLoadEnd = itemGroupProduct.DateFromLoadEnd;
                                    obj.DateToCome = itemGroupProduct.DateToCome;
                                    obj.DateToLeave = itemGroupProduct.DateToLeave;
                                    obj.DateToLoadStart = itemGroupProduct.DateToLoadStart;
                                    obj.DateToLoadEnd = itemGroupProduct.DateToLoadEnd;
                                    obj.DateInvoice = itemGroupProduct.InvoiceDate;
                                    obj.ETARequest = itemGroupProduct.ETARequest;

                                    obj.DateTOMasterETD = itemGroupProduct.DateTOMasterETD;
                                    obj.DateTOMasterETA = itemGroupProduct.DateTOMasterETA;
                                    obj.DateTOMasterATD = itemGroupProduct.DateTOMasterATD;
                                    obj.DateTOMasterATA = itemGroupProduct.DateTOMasterATA;
                                    obj.DateOrderETD = itemGroupProduct.DateOrderETD;
                                    obj.DateOrderETA = itemGroupProduct.DateOrderETA;
                                    obj.DateOrderETDRequest = itemGroupProduct.DateOrderETDRequest;
                                    obj.DateOrderETARequest = itemGroupProduct.DateOrderETARequest;
                                    obj.DateOrderCutOfTime = itemGroupProduct.DateOrderCutOfTime;

                                    obj.Zone = itemRouting.Zone;
                                    if (obj.Zone == null) obj.Zone = 0;
                                    obj.LeadTime = itemRouting.LeadTime;
                                    if (obj.LeadTime == null) obj.LeadTime = 0;
                                    obj.Note = string.Empty;
                                    obj.IsKPI = null;
                                    try
                                    {
                                        KPITime_GetDate(kpiCode, itemKPIGroup.KPICode, obj, lstKPIGroup);

                                        if (itemKPIGroup.KPIID == (int)KPICode.POD && itemGroupProduct.DITOGroupProductStatusPODID != -(int)SYSVarType.DITOGroupProductStatusPODComplete)
                                            obj.IsKPI = null;
                                    }
                                    catch (Exception ex)
                                    {
                                        obj.Note = ex.Message;
                                    }

                                    if (obj.ID < 1)
                                        model.KPI_KPITime.Add(obj);
                                }
                            }
                        }
                    }
                }
                model.SaveChanges();
            }
        }


        public static void KPIVENTime_GetDate(ExcelPackage package, string code, KPI_VENTime item, List<DTOContractKPITime> lst)
        {
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (code != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateRequest.Value.Hour + "," + item.DateRequest.Value.Minute + "," + item.DateRequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateDN != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateDN.Value.Hour + "," + item.DateDN.Value.Minute + "," + item.DateDN.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromCome.Value.Hour + "," + item.DateFromCome.Value.Minute + "," + item.DateFromCome.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLeave.Value.Hour + "," + item.DateFromLeave.Value.Minute + "," + item.DateFromLeave.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadStart.Value.Hour + "," + item.DateFromLoadStart.Value.Minute + "," + item.DateFromLoadStart.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadEnd.Value.Hour + "," + item.DateFromLoadEnd.Value.Minute + "," + item.DateFromLoadEnd.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToCome.Value.Hour + "," + item.DateToCome.Value.Minute + "," + item.DateToCome.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLeave.Value.Hour + "," + item.DateToLeave.Value.Minute + "," + item.DateToLeave.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadStart.Value.Hour + "," + item.DateToLoadStart.Value.Minute + "," + item.DateToLoadStart.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadEnd.Value.Hour + "," + item.DateToLoadEnd.Value.Minute + "," + item.DateToLoadEnd.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateInvoice != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateInvoice.Value.Hour + "," + item.DateInvoice.Value.Minute + "," + item.DateInvoice.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                //if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETD.Value.Hour + "," + item.DateOrderETD.Value.Minute + "," + item.DateOrderETD.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETA.Value.Hour + "," + item.DateOrderETA.Value.Minute + "," + item.DateOrderETA.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETDRequest.Value.Hour + "," + item.DateOrderETDRequest.Value.Minute + "," + item.DateOrderETDRequest.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;

                //if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value.Date;
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;
                //if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderCutOfTime.Value.Hour + "," + item.DateOrderCutOfTime.Value.Minute + "," + item.DateOrderCutOfTime.Value.Second + ")";
                //else worksheet.Cells[row, col].Value = null;
                //strRow = strCol + row; row++;


                worksheet.Cells[row, col].Value = item.Zone;
                if (item.Zone % 1 == 0.5)
                    worksheet.Cells[row, col].Value = item.Zone + 0.01;
                row++;
                worksheet.Cells[row, col].Value = item.LeadTime;
                if (item.LeadTime % 1 == 0.5)
                    worksheet.Cells[row, col].Value = item.LeadTime + 0.01;

                foreach (var valEx in dicEx)
                    row++;

                package.Workbook.Calculate();
                row++;
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                { item.KPIDate = DateTime.FromOADate(Convert.ToDouble(val)); }
                catch { }
                if (item.KPIDate == null)
                {
                    try
                    { item.KPIDate = Convert.ToDateTime(val, new CultureInfo("en-US")).Date; }
                    catch { }
                }
                if (item.DateRequest != null && item.KPIDate != null)
                    if (item.DateRequest.Value.AddDays(-2).CompareTo(item.KPIDate.Value) > 0)
                        item.KPIDate = null;

                row++;
                row++;
                if (item.KPIDate != null)
                {
                    val = worksheet.Cells[row, col].Value.ToString().Trim();
                    if (val == "T") item.IsKPI = true;
                    else if (val == "F") item.IsKPI = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void KPIVENTime_Create(DataEntities model, AccountItem account, int? ditomasterID, List<int> lstOrderID, List<int> lstDITOGroupProductID)
        {
            DateTime? dtNull = null;
            var lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.CATRoutingID > 0 &&
                    c.DITOMasterID == ditomasterID &&
                    c.OPS_DITOMaster.ContractID > 0 && c.OPS_DITOMaster.SYSCustomerID == account.SYSCustomerID).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        ContractID = c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = c.CATRoutingID,
                        c.DITOGroupProductStatusPODID,

                        VendorID = c.OPS_DITOMaster.VendorOfVehicleID == null ? account.SYSCustomerID : c.OPS_DITOMaster.VendorOfVehicleID.Value,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,
                    }).ToList();

            if (lstDITOGroupProductID != null && lstDITOGroupProductID.Count > 0)
            {
                lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.CATRoutingID > 0 &&
                    lstDITOGroupProductID.Contains(c.ID) &&
                    c.OPS_DITOMaster.ContractID > 0 && c.OPS_DITOMaster.SYSCustomerID == account.SYSCustomerID).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        ContractID = c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = c.CATRoutingID,
                        c.DITOGroupProductStatusPODID,

                        VendorID = c.OPS_DITOMaster.VendorOfVehicleID == null ? account.SYSCustomerID : c.OPS_DITOMaster.VendorOfVehicleID.Value,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,
                    }).ToList();
            }

            if (lstDITOGroupProduct.Count > 0)
            {
                var lstContractID = lstDITOGroupProduct.Select(c => c.ContractID).Distinct().ToList();

                var lstKPI = model.CAT_ContractKPITime.Where(c => lstContractID.Contains(c.CAT_ContractRouting.ContractID)).Select(c => new DTOContractKPITime
                {
                    CustomerID = c.CAT_ContractRouting.CAT_Contract.CustomerID,
                    ContractID = c.CAT_ContractRouting.ContractID,
                    CATRoutingID = c.CAT_ContractRouting.RoutingID,
                    Expression = c.Expression,
                    CompareField = c.CompareField,
                    Zone = c.CAT_ContractRouting.Zone,
                    LeadTime = c.CAT_ContractRouting.LeadTime,

                    KPIID = c.KPIID,
                    KPICode = c.KPI_KPI.Code
                }).ToList();
                var lstRouting = model.CAT_ContractRouting.Where(c => lstContractID.Contains(c.ContractID)).Select(c => new
                {
                    c.ContractID,
                    CATRoutingID = c.RoutingID,
                    c.Zone,
                    c.LeadTime
                }).ToList();

                //Danh sach cong thuc
                Dictionary<string, OfficeOpenXml.ExcelPackage> dicKPICode = new Dictionary<string, OfficeOpenXml.ExcelPackage>();
                foreach (var item in lstKPI.Distinct())
                {
                    if (!string.IsNullOrEmpty(item.Expression) && !string.IsNullOrEmpty(item.CompareField))
                    {
                        var lst = lstKPI.Where(c => c.ContractID == item.ContractID && c.CATRoutingID == item.CATRoutingID).ToList();
                        dicKPICode.Add(item.ContractID + "_" + item.CATRoutingID + "_" + item.KPICode, KPITime_GetPackage(item.Expression, item.CompareField, item.KPICode, lst, item.Zone, item.LeadTime));
                    }
                }

                //Tinh KPI
                foreach (var itemGroupProduct in lstDITOGroupProduct)
                {
                    var itemRouting = lstRouting.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID);
                    var lstKPIGroup = lstKPI.Where(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID).ToList();

                    if (itemRouting != null && lstKPIGroup.Count > 0)
                    {
                        foreach (var itemKPIGroup in lstKPIGroup)
                        {
                            if (!string.IsNullOrEmpty(itemKPIGroup.Expression) && !string.IsNullOrEmpty(itemKPIGroup.CompareField))
                            {
                                OfficeOpenXml.ExcelPackage kpiCode = default(OfficeOpenXml.ExcelPackage);
                                if (dicKPICode.ContainsKey(itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode))
                                    kpiCode = dicKPICode[itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode];

                                if (kpiCode != null)
                                {
                                    var obj = model.KPI_VENTime.FirstOrDefault(c => c.VendorID == itemGroupProduct.VendorID && c.OrderID == itemGroupProduct.OrderID &&
                                        c.DITOGroupProductID == itemGroupProduct.ID);
                                    if (obj == null)
                                    {
                                        obj = new KPI_VENTime();
                                        obj.KPIID = itemKPIGroup.KPIID;
                                        obj.VendorID = itemGroupProduct.VendorID;
                                        obj.OrderID = itemGroupProduct.OrderID;
                                        obj.OrderGroupProductID = itemGroupProduct.OrderGroupProductID;
                                        obj.DITOGroupProductID = itemGroupProduct.ID;

                                        obj.CreatedBy = account.UserName;
                                        obj.CreatedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        obj.ModifiedBy = account.UserName;
                                        obj.ModifiedDate = DateTime.Now;
                                    }
                                    obj.DateData = itemGroupProduct.RequestDate;
                                    obj.DateRequest = itemGroupProduct.RequestDate;
                                    obj.DateDN = itemGroupProduct.DateDN;
                                    if (obj.DateDN == null)
                                        obj.DateDN = itemGroupProduct.RequestDate;
                                    obj.DateFromCome = itemGroupProduct.DateFromCome;
                                    obj.DateFromLeave = itemGroupProduct.DateFromLeave;
                                    obj.DateFromLoadStart = itemGroupProduct.DateFromLoadStart;
                                    obj.DateFromLoadEnd = itemGroupProduct.DateFromLoadEnd;
                                    obj.DateToCome = itemGroupProduct.DateToCome;
                                    obj.DateToLeave = itemGroupProduct.DateToLeave;
                                    obj.DateToLoadStart = itemGroupProduct.DateToLoadStart;
                                    obj.DateToLoadEnd = itemGroupProduct.DateToLoadEnd;
                                    obj.DateInvoice = itemGroupProduct.InvoiceDate;
                                    obj.ETARequest = itemGroupProduct.ETARequest;

                                    obj.Zone = itemRouting.Zone;
                                    if (obj.Zone == null) obj.Zone = 0;
                                    obj.LeadTime = itemRouting.LeadTime;
                                    if (obj.LeadTime == null) obj.LeadTime = 0;
                                    obj.Note = string.Empty;

                                    try
                                    {
                                        KPIVENTime_GetDate(kpiCode, itemKPIGroup.KPICode, obj, lstKPIGroup);

                                        if (itemKPIGroup.KPIID == (int)KPICode.POD && itemGroupProduct.DITOGroupProductStatusPODID != -(int)SYSVarType.DITOGroupProductStatusPODComplete)
                                            obj.IsKPI = null;
                                    }
                                    catch (Exception ex)
                                    {
                                        obj.Note = ex.Message;
                                    }

                                    if (obj.ID < 1)
                                        model.KPI_VENTime.Add(obj);
                                }
                            }
                        }
                    }
                }
                model.SaveChanges();
            }
        }


        public static void KPIVENTenderFTL_GetValue(KPI_VENTenderFTL item, string strExpr, string strFieldr)
        {
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);
                StringBuilder strField = new StringBuilder(strFieldr);

                row++;
                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strExp.Replace("[TotalSchedule]", strCol + row);
                strField.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalAccept;
                strExp.Replace("[TotalAccept]", strCol + row);
                strField.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalReject;
                strExp.Replace("[TotalReject]", strCol + row);
                strField.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();
                strField.Replace("[TotalKPI]", worksheet.Cells[row, col].Address);
                row++;

                worksheet.Cells[row, col].Formula = strField.ToString();

                package.Workbook.Calculate();

                var val = worksheet.Cells[row - 1, col].Value.ToString().Trim();

                try
                { item.TotalKPI = Convert.ToInt32(val); }
                catch { }

                if (item.TotalKPI != null)
                {
                    val = worksheet.Cells[row, col].Value.ToString().Trim();
                    if (val == "True") item.IsKPI = true;
                    else if (val == "False") item.IsKPI = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KPIVENTenderLTL_GetValue(KPI_VENTenderLTL item, string strExpr, string strFieldr)
        {
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);
                StringBuilder strField = new StringBuilder(strFieldr);

                row++;
                worksheet.Cells[row, col].Value = item.TonOrder;
                strExp.Replace("[TonOrder]", strCol + row);
                strField.Replace("[TonOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMOrder;
                strExp.Replace("[CBMOrder]", strCol + row);
                strField.Replace("[CBMOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityOrder;
                strExp.Replace("[QuantityOrder]", strCol + row);
                strField.Replace("[QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonAccept;
                strExp.Replace("[TonAccept]", strCol + row);
                strField.Replace("[TonAccept]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMAccept;
                strExp.Replace("[CBMAccept]", strCol + row);
                strField.Replace("[CBMAccept]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityAccept;
                strExp.Replace("[QuantityAccept]", strCol + row);
                strField.Replace("[QuantityAccept]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();
                strField.Replace("[ValueKPI]", worksheet.Cells[row, col].Address);
                row++;

                worksheet.Cells[row, col].Formula = strField.ToString();

                package.Workbook.Calculate();

                var val = worksheet.Cells[row - 1, col].Value.ToString().Trim();

                try
                { item.ValueKPI = Convert.ToDouble(val); }
                catch { }

                if (item.ValueKPI != null)
                {
                    val = worksheet.Cells[row, col].Value.ToString().Trim();
                    if (val == "True") item.IsKPI = true;
                    else if (val == "False") item.IsKPI = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void KPICollection_Order_Generate(DataEntities model, AccountItem Account, List<DTOKPICollection_Order> ListData, List<DTOKPICollection_Column> ListColumn)
        {
            var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);

            // Cần sửa
            const int i20DC = 5;
            const int i40DC = 6;
            const int i45DC = 7;

            foreach (var item in ListData)
            {
                #region Group
                if (item.ListGroup.Count > 0)
                {
                    var qOrder = item.ListGroup.Where(c => c.IsReturn == false);
                    if (qOrder.Count() > 0)
                    {
                        item.Order_TonOrder = qOrder.GroupBy(c => c.OrderGroupProductID).Sum(c => c.FirstOrDefault().TonOrder);
                        item.Order_CBMOrder = qOrder.GroupBy(c => c.OrderGroupProductID).Sum(c => c.FirstOrDefault().CBMOrder);
                        item.Order_QuantityOrder = qOrder.GroupBy(c => c.OrderGroupProductID).Sum(c => c.FirstOrDefault().QuantityOrder);
                    }
                    qOrder = item.ListGroup.Where(c => !c.IsCancel);
                    if (qOrder.Count() > 0)
                    {
                        item.Order_TonTranfer = qOrder.Sum(c => c.TonTranfer);
                        item.Order_CBMTranfer = qOrder.Sum(c => c.CBMTranfer);
                        item.Order_QuantityTranfer = qOrder.Sum(c => c.QuantityTranfer);
                        item.Order_TonReturn = qOrder.Sum(c => c.TonReturn);
                        item.Order_CBMReturn = qOrder.Sum(c => c.CBMReturn);
                        item.Order_QuantityReturn = qOrder.Sum(c => c.QuantityReturn);
                        item.Order_TonBBGN = qOrder.Sum(c => c.TonBBGN);
                        item.Order_CBMBBGN = qOrder.Sum(c => c.CBMBBGN);
                        item.Order_QuantityBBGN = qOrder.Sum(c => c.QuantityBBGN);
                    }
                    qOrder = item.ListGroup.Where(c => c.IsReturn == true);
                    if (qOrder.Count() > 0)
                    {
                        item.Order_TonPlus = qOrder.Sum(c => c.TonTranfer);
                        item.Order_CBMPlus = qOrder.Sum(c => c.CBMTranfer);
                        item.Order_QuantityPlus = qOrder.Sum(c => c.QuantityTranfer);
                    }
                    qOrder = item.ListGroup.Where(c => c.IsCancel == true);
                    if (qOrder.Count() > 0)
                    {
                        item.Order_TonCancel = qOrder.Sum(c => c.TonTranfer);
                        item.Order_CBMCancel = qOrder.Sum(c => c.CBMTranfer);
                        item.Order_QuantityCancel = qOrder.Sum(c => c.QuantityTranfer);
                    }

                    var date = item.ListGroup.FirstOrDefault(c => c.ETA.HasValue);
                    if (date != null)
                        item.Order_ETA = date.ETA;
                    date = item.ListGroup.FirstOrDefault(c => c.ETD.HasValue);
                    if (date != null)
                        item.Order_ETD = date.ETD;
                    date = item.ListGroup.FirstOrDefault(c => c.ATA.HasValue);
                    if (date != null)
                        item.Order_ATA = date.ATA;
                    date = item.ListGroup.FirstOrDefault(c => c.ATD.HasValue);
                    if (date != null)
                        item.Order_ATD = date.ATD;
                }
                #endregion

                #region Container
                if (item.ListContainer.Count > 0)
                {
                    item.Order_TonOrder = item.ListContainer.GroupBy(c => c.OrderContainerID).Sum(c => c.FirstOrDefault().TonOrder);

                    var lstTranfer = item.ListContainer.Where(c => c.PackingID == i20DC).GroupBy(c => c.OrderContainerID);
                    if (lstTranfer.Count() > 0)
                    {
                        item.Order_Container20Order = lstTranfer.Count();
                        foreach (var itemTranfer in lstTranfer)
                        {
                            if (itemTranfer.Count() == itemTranfer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                item.Order_Container20Tranfer += 1;
                        }
                    }

                    lstTranfer = item.ListContainer.Where(c => c.PackingID == i40DC).GroupBy(c => c.OrderContainerID);
                    if (lstTranfer.Count() > 0)
                    {
                        item.Order_Container40Order = lstTranfer.Count();
                        foreach (var itemTranfer in lstTranfer)
                        {
                            if (itemTranfer.Count() == itemTranfer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                item.Order_Container40Tranfer += 1;
                        }
                    }

                    lstTranfer = item.ListContainer.Where(c => c.PackingID == i45DC).GroupBy(c => c.OrderContainerID);
                    if (lstTranfer.Count() > 0)
                    {
                        item.Order_Container40HOrder = lstTranfer.Count();
                        foreach (var itemTranfer in lstTranfer)
                        {
                            if (itemTranfer.Count() == itemTranfer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                item.Order_Container40HTranfer += 1;
                        }
                    }

                    var date = item.ListContainer.FirstOrDefault(c => c.ETA.HasValue);
                    if (date != null)
                        item.Order_ETA = date.ETA;
                    date = item.ListContainer.FirstOrDefault(c => c.ETD.HasValue);
                    if (date != null)
                        item.Order_ETD = date.ETD;
                    date = item.ListContainer.FirstOrDefault(c => c.ATA.HasValue);
                    if (date != null)
                        item.Order_ATA = date.ATA;
                    date = item.ListContainer.FirstOrDefault(c => c.ATD.HasValue);
                    if (date != null)
                        item.Order_ATD = date.ATD;
                }
                #endregion

                #region DateData
                item.Order_TotalCredit = item.ListGroupFIN.Sum(c => c.Credit);
                item.Order_TotalDebit = item.ListGroupFIN.Sum(c => c.Debit);

                KPI_Collection obj = new KPI_Collection();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                obj.CustomerID = item.Order_CustomerID;
                obj.OrderID = item.Order_ID;
                bool flag = false;
                switch (columnDetail.Code)
                {
                    case "Order_DateConfig":
                        if (item.Order_DateConfig.HasValue)
                        {
                            obj.DateData = item.Order_DateConfig.Value.Date;
                            flag = true;
                        }
                        break;
                    case "Order_DateRequest":
                        if (item.Order_DateRequest.HasValue)
                        {
                            obj.DateData = item.Order_DateRequest.Value.Date;
                            flag = true;
                        }
                        break;
                    case "Order_ExternalDate":
                        if (item.Order_ExternalDate.HasValue)
                        {
                            obj.DateData = item.Order_ExternalDate.Value.Date;
                            flag = true;
                        }
                        break;
                    case "Order_ETD":
                        if (item.Order_ETD.HasValue)
                        {
                            obj.DateData = item.Order_ETD.Value.Date;
                            flag = true;
                        }
                        break;
                    case "Order_ETA":
                        if (item.Order_ETA.HasValue)
                        {
                            obj.DateData = item.Order_ETA.Value.Date;
                            flag = true;
                        }
                        break;
                    case "Order_ETARequest":
                        if (item.Order_ETARequest.HasValue)
                        {
                            obj.DateData = item.Order_ETARequest.Value.Date;
                            flag = true;
                        }
                        break;
                    case "Order_ATD":
                        if (item.Order_ATD.HasValue)
                        {
                            obj.DateData = item.Order_ATD.Value.Date;
                            flag = true;
                        }
                        break;
                    case "Order_ATA":
                        if (item.Order_ATA.HasValue)
                        {
                            obj.DateData = item.Order_ATA.Value.Date;
                            flag = true;
                        }
                        break;
                }
                #endregion

                #region Detail
                if (flag)
                {
                    #region Chi tiết
                    var lstDetail = ListColumn.Where(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDetail);
                    foreach (var itemDetail in lstDetail)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = itemDetail.ID;
                        KPICollection_Order_GetValue(itemDetail.Code, item, objDetail);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức KPI
                    var detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_Order_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức đạt KPI
                    detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeIsKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_Order_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                        {
                            obj.KPI_CollectionDetail.Add(objDetail);
                            try
                            {
                                obj.IsKPI = Convert.ToBoolean(objDetail.Value);
                            }
                            catch { }
                        }
                    }
                    #endregion

                    model.KPI_Collection.Add(obj);
                }
                #endregion
            }
        }

        private static void KPICollection_Order_GetValue(string field, DTOKPICollection_Order item, KPI_CollectionDetail objDetail)
        {
            switch (field)
            {
                case "Order_DateConfig":
                    if (item.Order_DateConfig.HasValue)
                        objDetail.Value = item.Order_DateConfig.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_DateRequest":
                    if (item.Order_DateRequest.HasValue)
                        objDetail.Value = item.Order_DateRequest.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_ExternalDate":
                    if (item.Order_ExternalDate.HasValue)
                        objDetail.Value = item.Order_ExternalDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_ETD":
                    if (item.Order_ETD.HasValue)
                        objDetail.Value = item.Order_ETD.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_ETA":
                    if (item.Order_ETA.HasValue)
                        objDetail.Value = item.Order_ETA.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_ETARequest":
                    if (item.Order_ETARequest.HasValue)
                        objDetail.Value = item.Order_ETARequest.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_ATD":
                    if (item.Order_ATD.HasValue)
                        objDetail.Value = item.Order_ATD.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_ATA":
                    if (item.Order_ATA.HasValue)
                        objDetail.Value = item.Order_ATA.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "Order_TonOrder":
                    objDetail.Value = item.Order_TonOrder.ToString();
                    break;
                case "Order_CBMOrder":
                    objDetail.Value = item.Order_CBMOrder.ToString();
                    break;
                case "Order_QuantityOrder":
                    objDetail.Value = item.Order_QuantityOrder.ToString();
                    break;
                case "Order_TonTranfer":
                    objDetail.Value = item.Order_TonTranfer.ToString();
                    break;
                case "Order_CBMTranfer":
                    objDetail.Value = item.Order_CBMTranfer.ToString();
                    break;
                case "Order_QuantityTranfer":
                    objDetail.Value = item.Order_QuantityTranfer.ToString();
                    break;
                case "Order_TonBBGN":
                    objDetail.Value = item.Order_TonBBGN.ToString();
                    break;
                case "Order_CBMBBGN":
                    objDetail.Value = item.Order_CBMBBGN.ToString();
                    break;
                case "Order_QuantityBBGN":
                    objDetail.Value = item.Order_QuantityBBGN.ToString();
                    break;
                case "Order_TonReturn":
                    objDetail.Value = item.Order_TonReturn.ToString();
                    break;
                case "Order_CBMReturn":
                    objDetail.Value = item.Order_CBMReturn.ToString();
                    break;
                case "Order_QuantityReturn":
                    objDetail.Value = item.Order_QuantityReturn.ToString();
                    break;
                case "Order_TonPlus":
                    objDetail.Value = item.Order_TonPlus.ToString();
                    break;
                case "Order_CBMPlus":
                    objDetail.Value = item.Order_CBMPlus.ToString();
                    break;
                case "Order_QuantityPlus":
                    objDetail.Value = item.Order_QuantityPlus.ToString();
                    break;
                case "Order_TonCancel":
                    objDetail.Value = item.Order_TonCancel.ToString();
                    break;
                case "Order_CBMCancel":
                    objDetail.Value = item.Order_CBMCancel.ToString();
                    break;
                case "Order_QuantityCancel":
                    objDetail.Value = item.Order_QuantityCancel.ToString();
                    break;
                case "Order_TotalCredit":
                    objDetail.Value = item.Order_TotalCredit.ToString();
                    break;
                case "Order_TotalDebit":
                    objDetail.Value = item.Order_TotalDebit.ToString();
                    break;
                case "Order_Container20Order":
                    objDetail.Value = item.Order_Container20Order.ToString();
                    break;
                case "Order_Container40Order":
                    objDetail.Value = item.Order_Container40Order.ToString();
                    break;
                case "Order_Container40HOrder":
                    objDetail.Value = item.Order_Container40HOrder.ToString();
                    break;
                case "Order_Container20Tranfer":
                    objDetail.Value = item.Order_Container20Tranfer.ToString();
                    break;
                case "Order_Container40Tranfer":
                    objDetail.Value = item.Order_Container40Tranfer.ToString();
                    break;
                case "Order_Container40HTranfer":
                    objDetail.Value = item.Order_Container40HTranfer.ToString();
                    break;

            }
        }

        public static string KPICollection_Order_GetKPI(DTOKPICollection_Order item, string strExpr)
        {
            string result = string.Empty;
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                worksheet.Cells[row, col].Value = item.Order_DateRequest;
                strExp.Replace("[Order_DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_ExternalDate;
                strExp.Replace("[Order_ExternalDate]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_ETD;
                strExp.Replace("[Order_ETD]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_ETA;
                strExp.Replace("[Order_ETA]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_ETARequest;
                strExp.Replace("[Order_ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_ATD;
                strExp.Replace("[Order_ATD]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_ATA;
                strExp.Replace("[Order_ATA]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_DateRequest;
                strExp.Replace("[Order_DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TonOrder;
                strExp.Replace("[Order_TonOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_CBMOrder;
                strExp.Replace("[Order_CBMOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_QuantityOrder;
                strExp.Replace("[Order_QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TonTranfer;
                strExp.Replace("[Order_TonTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_CBMTranfer;
                strExp.Replace("[Order_CBMTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_QuantityTranfer;
                strExp.Replace("[Order_QuantityTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TonBBGN;
                strExp.Replace("[Order_TonBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_CBMOrder;
                strExp.Replace("[Order_CBMOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_CBMBBGN;
                strExp.Replace("[Order_CBMBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_QuantityBBGN;
                strExp.Replace("[Order_QuantityBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TonReturn;
                strExp.Replace("[Order_TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_CBMReturn;
                strExp.Replace("[Order_CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_QuantityReturn;
                strExp.Replace("[Order_QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TonPlus;
                strExp.Replace("[Order_TonPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_CBMPlus;
                strExp.Replace("[Order_CBMPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_QuantityPlus;
                strExp.Replace("[Order_QuantityPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TonCancel;
                strExp.Replace("[Order_TonCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_CBMCancel;
                strExp.Replace("[Order_CBMCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_QuantityCancel;
                strExp.Replace("[Order_QuantityCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TotalCredit;
                strExp.Replace("[Order_TotalCredit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_TotalDebit;
                strExp.Replace("[Order_TotalDebit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_DateConfig;
                strExp.Replace("[Order_DateConfig]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_Container20Order;
                strExp.Replace("[Order_Container20Order]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_Container40Order;
                strExp.Replace("[Order_Container40Order]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_Container40HOrder;
                strExp.Replace("[Order_Container40HOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_Container20Tranfer;
                strExp.Replace("[Order_Container20Tranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_Container40Tranfer;
                strExp.Replace("[Order_Container40Tranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Order_Container40HTranfer;
                strExp.Replace("[Order_Container40HTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                package.Workbook.Calculate();
                result = worksheet.Cells[row, col].Value.ToString().Trim();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }


        public static void KPICollection_OrderGroupProduct_Generate(DataEntities model, AccountItem Account, List<DTOKPICollection_OrderGroupProduct> ListData, List<DTOKPICollection_Column> ListColumn)
        {
            var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);

            foreach (var item in ListData)
            {
                var qOrder = item.ListGroup.Where(c => !c.IsCancel);
                if (qOrder.Count() > 0)
                {
                    item.OrderGroupProduct_TonTranfer = qOrder.Sum(c => c.TonTranfer);
                    item.OrderGroupProduct_CBMTranfer = qOrder.Sum(c => c.CBMTranfer);
                    item.OrderGroupProduct_QuantityTranfer = qOrder.Sum(c => c.QuantityTranfer);
                    item.OrderGroupProduct_TonReturn = qOrder.Sum(c => c.TonReturn);
                    item.OrderGroupProduct_CBMReturn = qOrder.Sum(c => c.CBMReturn);
                    item.OrderGroupProduct_QuantityReturn = qOrder.Sum(c => c.QuantityReturn);
                    item.OrderGroupProduct_TonBBGN = qOrder.Sum(c => c.TonBBGN);
                    item.OrderGroupProduct_CBMBBGN = qOrder.Sum(c => c.CBMBBGN);
                    item.OrderGroupProduct_QuantityBBGN = qOrder.Sum(c => c.QuantityBBGN);
                }
                qOrder = item.ListGroup.Where(c => c.IsReturn == true);
                if (qOrder.Count() > 0)
                {
                    item.OrderGroupProduct_TonPlus = qOrder.Sum(c => c.TonTranfer);
                    item.OrderGroupProduct_CBMPlus = qOrder.Sum(c => c.CBMTranfer);
                    item.OrderGroupProduct_QuantityPlus = qOrder.Sum(c => c.QuantityTranfer);
                }
                qOrder = item.ListGroup.Where(c => c.IsCancel == true);
                if (qOrder.Count() > 0)
                {
                    item.OrderGroupProduct_TonCancel = qOrder.Sum(c => c.TonTranfer);
                    item.OrderGroupProduct_CBMCancel = qOrder.Sum(c => c.CBMTranfer);
                    item.OrderGroupProduct_QuantityCancel = qOrder.Sum(c => c.QuantityTranfer);
                }

                var date = item.ListGroup.FirstOrDefault(c => c.ETA.HasValue);
                if (date != null)
                    item.OrderGroupProduct_ETA = date.ETA;
                date = item.ListGroup.FirstOrDefault(c => c.ETD.HasValue);
                if (date != null)
                    item.OrderGroupProduct_ETD = date.ETD;
                date = item.ListGroup.FirstOrDefault(c => c.ATA.HasValue);
                if (date != null)
                    item.OrderGroupProduct_ATA = date.ATA;
                date = item.ListGroup.FirstOrDefault(c => c.ATD.HasValue);
                if (date != null)
                    item.OrderGroupProduct_ATD = date.ATD;

                KPI_Collection obj = new KPI_Collection();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                obj.CustomerID = item.OrderGroupProduct_CustomerID;
                obj.OrderID = item.OrderGroupProduct_OrderID;
                obj.OrderGroupProductID = item.OrderGroupProduct_ID;
                bool flag = false;
                switch (columnDetail.Code)
                {
                    case "OrderGroupProduct_DateConfig":
                        if (item.OrderGroupProduct_DateConfig.HasValue)
                        {
                            obj.DateData = item.OrderGroupProduct_DateConfig.Value.Date;
                            flag = true;
                        }
                        break;
                    case "OrderGroupProduct_DateRequest":
                        if (item.OrderGroupProduct_DateRequest.HasValue)
                        {
                            obj.DateData = item.OrderGroupProduct_DateRequest.Value.Date;
                            flag = true;
                        }
                        break;
                    case "OrderGroupProduct_ETD":
                        if (item.OrderGroupProduct_ETD.HasValue)
                        {
                            obj.DateData = item.OrderGroupProduct_ETD.Value.Date;
                            flag = true;
                        }
                        break;
                    case "OrderGroupProduct_ETA":
                        if (item.OrderGroupProduct_ETA.HasValue)
                        {
                            obj.DateData = item.OrderGroupProduct_ETA.Value.Date;
                            flag = true;
                        }
                        break;
                    case "OrderGroupProduct_ETARequest":
                        if (item.OrderGroupProduct_ETARequest.HasValue)
                        {
                            obj.DateData = item.OrderGroupProduct_ETARequest.Value.Date;
                            flag = true;
                        }
                        break;
                    case "OrderGroupProduct_ATD":
                        if (item.OrderGroupProduct_ATD.HasValue)
                        {
                            obj.DateData = item.OrderGroupProduct_ATD.Value.Date;
                            flag = true;
                        }
                        break;
                    case "OrderGroupProduct_ATA":
                        if (item.OrderGroupProduct_ATA.HasValue)
                        {
                            obj.DateData = item.OrderGroupProduct_ATA.Value.Date;
                            flag = true;
                        }
                        break;
                }

                // Chỉ thiết lập khi DateData có value
                if (flag)
                {
                    #region Chi tiết
                    var lstDetail = ListColumn.Where(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDetail);
                    foreach (var itemDetail in lstDetail)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = itemDetail.ID;
                        KPICollection_OrderGroupProduct_GetValue(itemDetail.Code, item, objDetail);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức KPI
                    var detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_OrderGroupProduct_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức đạt KPI
                    detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeIsKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_OrderGroupProduct_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                        {
                            obj.KPI_CollectionDetail.Add(objDetail);
                            try
                            {
                                obj.IsKPI = Convert.ToBoolean(objDetail.Value);
                            }
                            catch { }
                        }
                    }
                    #endregion

                    model.KPI_Collection.Add(obj);
                }
            }
        }

        private static void KPICollection_OrderGroupProduct_GetValue(string field, DTOKPICollection_OrderGroupProduct item, KPI_CollectionDetail objDetail)
        {
            switch (field)
            {
                case "OrderGroupProduct_DateConfig":
                    if (item.OrderGroupProduct_DateConfig.HasValue)
                        objDetail.Value = item.OrderGroupProduct_DateConfig.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "OrderGroupProduct_DateRequest":
                    if (item.OrderGroupProduct_DateRequest.HasValue)
                        objDetail.Value = item.OrderGroupProduct_DateRequest.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "OrderGroupProduct_ETD":
                    if (item.OrderGroupProduct_ETD.HasValue)
                        objDetail.Value = item.OrderGroupProduct_ETD.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "OrderGroupProduct_ETA":
                    if (item.OrderGroupProduct_ETA.HasValue)
                        objDetail.Value = item.OrderGroupProduct_ETA.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "OrderGroupProduct_ETARequest":
                    if (item.OrderGroupProduct_ETARequest.HasValue)
                        objDetail.Value = item.OrderGroupProduct_ETARequest.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "OrderGroupProduct_ATD":
                    if (item.OrderGroupProduct_ATD.HasValue)
                        objDetail.Value = item.OrderGroupProduct_ATD.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "OrderGroupProduct_ATA":
                    if (item.OrderGroupProduct_ATA.HasValue)
                        objDetail.Value = item.OrderGroupProduct_ATA.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "OrderGroupProduct_TonOrder":
                    objDetail.Value = item.OrderGroupProduct_TonOrder.ToString();
                    break;
                case "OrderGroupProduct_CBMOrder":
                    objDetail.Value = item.OrderGroupProduct_CBMOrder.ToString();
                    break;
                case "OrderGroupProduct_QuantityOrder":
                    objDetail.Value = item.OrderGroupProduct_QuantityOrder.ToString();
                    break;
                case "OrderGroupProduct_TonTranfer":
                    objDetail.Value = item.OrderGroupProduct_TonTranfer.ToString();
                    break;
                case "OrderGroupProduct_CBMTranfer":
                    objDetail.Value = item.OrderGroupProduct_CBMTranfer.ToString();
                    break;
                case "OrderGroupProduct_QuantityTranfer":
                    objDetail.Value = item.OrderGroupProduct_QuantityTranfer.ToString();
                    break;
                case "OrderGroupProduct_TonBBGN":
                    objDetail.Value = item.OrderGroupProduct_TonBBGN.ToString();
                    break;
                case "OrderGroupProduct_CBMBBGN":
                    objDetail.Value = item.OrderGroupProduct_CBMBBGN.ToString();
                    break;
                case "OrderGroupProduct_QuantityBBGN":
                    objDetail.Value = item.OrderGroupProduct_QuantityBBGN.ToString();
                    break;
                case "OrderGroupProduct_TonReturn":
                    objDetail.Value = item.OrderGroupProduct_TonReturn.ToString();
                    break;
                case "OrderGroupProduct_CBMReturn":
                    objDetail.Value = item.OrderGroupProduct_CBMReturn.ToString();
                    break;
                case "OrderGroupProduct_QuantityReturn":
                    objDetail.Value = item.OrderGroupProduct_QuantityReturn.ToString();
                    break;
                case "OrderGroupProduct_TonPlus":
                    objDetail.Value = item.OrderGroupProduct_TonPlus.ToString();
                    break;
                case "OrderGroupProduct_CBMPlus":
                    objDetail.Value = item.OrderGroupProduct_CBMPlus.ToString();
                    break;
                case "OrderGroupProduct_QuantityPlus":
                    objDetail.Value = item.OrderGroupProduct_QuantityPlus.ToString();
                    break;
                case "OrderGroupProduct_TonCancel":
                    objDetail.Value = item.OrderGroupProduct_TonCancel.ToString();
                    break;
                case "OrderGroupProduct_CBMCancel":
                    objDetail.Value = item.OrderGroupProduct_CBMCancel.ToString();
                    break;
                case "OrderGroupProduct_QuantityCancel":
                    objDetail.Value = item.OrderGroupProduct_QuantityCancel.ToString();
                    break;
                case "OrderGroupProduct_TotalCredit":
                    objDetail.Value = item.OrderGroupProduct_TotalCredit.ToString();
                    break;
                case "OrderGroupProduct_TotalDebit":
                    objDetail.Value = item.OrderGroupProduct_TotalDebit.ToString();
                    break;
            }
        }

        public static string KPICollection_OrderGroupProduct_GetKPI(DTOKPICollection_OrderGroupProduct item, string strExpr)
        {
            string result = string.Empty;
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                worksheet.Cells[row, col].Value = item.OrderGroupProduct_DateRequest;
                strExp.Replace("[OrderGroupProduct_DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_ETD;
                strExp.Replace("[OrderGroupProduct_ETD]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_ETA;
                strExp.Replace("[OrderGroupProduct_ETA]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_ETARequest;
                strExp.Replace("[OrderGroupProduct_ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_ATD;
                strExp.Replace("[OrderGroupProduct_ATD]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_ATA;
                strExp.Replace("[OrderGroupProduct_ATA]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_DateRequest;
                strExp.Replace("[OrderGroupProduct_DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TonOrder;
                strExp.Replace("[OrderGroupProduct_TonOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_CBMOrder;
                strExp.Replace("[OrderGroupProduct_CBMOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_QuantityOrder;
                strExp.Replace("[OrderGroupProduct_QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TonTranfer;
                strExp.Replace("[OrderGroupProduct_TonTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_CBMTranfer;
                strExp.Replace("[OrderGroupProduct_CBMTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_QuantityTranfer;
                strExp.Replace("[OrderGroupProduct_QuantityTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TonBBGN;
                strExp.Replace("[OrderGroupProduct_TonBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_CBMOrder;
                strExp.Replace("[OrderGroupProduct_CBMOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_CBMBBGN;
                strExp.Replace("[OrderGroupProduct_CBMBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_QuantityBBGN;
                strExp.Replace("[OrderGroupProduct_QuantityBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TonReturn;
                strExp.Replace("[OrderGroupProduct_TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_CBMReturn;
                strExp.Replace("[OrderGroupProduct_CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_QuantityReturn;
                strExp.Replace("[OrderGroupProduct_QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TonPlus;
                strExp.Replace("[OrderGroupProduct_TonPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_CBMPlus;
                strExp.Replace("[OrderGroupProduct_CBMPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_QuantityPlus;
                strExp.Replace("[OrderGroupProduct_QuantityPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TonCancel;
                strExp.Replace("[OrderGroupProduct_TonCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_CBMCancel;
                strExp.Replace("[OrderGroupProduct_CBMCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_QuantityCancel;
                strExp.Replace("[OrderGroupProduct_QuantityCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TotalCredit;
                strExp.Replace("[OrderGroupProduct_TotalCredit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_TotalDebit;
                strExp.Replace("[OrderGroupProduct_TotalDebit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderGroupProduct_DateConfig;
                strExp.Replace("[OrderGroupProduct_DateConfig]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                package.Workbook.Calculate();
                result = worksheet.Cells[row, col].Value.ToString().Trim();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }


        public static void KPICollection_TOMaster_Generate(DataEntities model, AccountItem Account, List<DTOKPICollection_TOMaster> ListData, List<DTOKPICollection_Column> ListColumn)
        {
            var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);

            // Cần sửa
            const int i20DC = 5;
            const int i40DC = 6;
            const int i45DC = 7;

            foreach (var item in ListData)
            {
                #region Group
                if (item.ListGroup.Count > 0)
                {
                    var qOrder = item.ListGroup.Where(c => !c.IsCancel);
                    if (qOrder.Count() > 0)
                    {
                        item.TOMaster_Ton = qOrder.Sum(c => c.Ton);
                        item.TOMaster_CBM = qOrder.Sum(c => c.CBM);
                        item.TOMaster_Quantity = qOrder.Sum(c => c.Quantity);
                        item.TOMaster_TonTranfer = qOrder.Sum(c => c.TonTranfer);
                        item.TOMaster_CBMTranfer = qOrder.Sum(c => c.CBMTranfer);
                        item.TOMaster_QuantityTranfer = qOrder.Sum(c => c.QuantityTranfer);
                        item.TOMaster_TonReturn = qOrder.Sum(c => c.TonReturn);
                        item.TOMaster_CBMReturn = qOrder.Sum(c => c.CBMReturn);
                        item.TOMaster_QuantityReturn = qOrder.Sum(c => c.QuantityReturn);
                        item.TOMaster_TonBBGN = qOrder.Sum(c => c.TonBBGN);
                        item.TOMaster_CBMBBGN = qOrder.Sum(c => c.CBMBBGN);
                        item.TOMaster_QuantityBBGN = qOrder.Sum(c => c.QuantityBBGN);
                        item.TOMaster_InvoiceDate = qOrder.OrderBy(c => c.InvoiceDate).FirstOrDefault().InvoiceDate;
                    }
                }
                #endregion

                #region Container
                if (item.ListContainer.Count > 0)
                {
                    item.TOMaster_Ton = item.TOMaster_TonTranfer = item.TOMaster_TonBBGN = item.ListContainer.GroupBy(c => c.OrderContainerID).Sum(c => c.FirstOrDefault().TonOrder);

                    var lstTranfer = item.ListContainer.Where(c => c.PackingID == i20DC).GroupBy(c => c.OrderContainerID);
                    if (lstTranfer.Count() > 0)
                    {
                        foreach (var itemTranfer in lstTranfer)
                        {
                            if (itemTranfer.Count() == itemTranfer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                item.TOMaster_Container20Tranfer += 1;
                            else
                                item.TOMaster_Container20Plan += 1;
                        }
                    }

                    lstTranfer = item.ListContainer.Where(c => c.PackingID == i40DC).GroupBy(c => c.OrderContainerID);
                    if (lstTranfer.Count() > 0)
                    {
                        foreach (var itemTranfer in lstTranfer)
                        {
                            if (itemTranfer.Count() == itemTranfer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                item.TOMaster_Container40Tranfer += 1;
                            else
                                item.TOMaster_Container40Plan += 1;
                        }
                    }

                    lstTranfer = item.ListContainer.Where(c => c.PackingID == i45DC).GroupBy(c => c.OrderContainerID);
                    if (lstTranfer.Count() > 0)
                    {
                        foreach (var itemTranfer in lstTranfer)
                        {
                            if (itemTranfer.Count() == itemTranfer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                item.TOMaster_Container40HTranfer += 1;
                            else
                                item.TOMaster_Container40HPlan += 1;
                        }
                    }

                    item.TOMaster_InvoiceDate = item.ListContainer.OrderBy(c => c.InvoiceDate).FirstOrDefault().InvoiceDate;
                }
                #endregion

                #region DateData
                item.TOMaster_TotalCredit = item.ListGroupFIN.Sum(c => c.Credit);
                item.TOMaster_TotalDebit = item.ListGroupFIN.Sum(c => c.Debit);

                KPI_Collection obj = new KPI_Collection();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                obj.DITOMasterID = item.TOMaster_ID;
                bool flag = false;
                switch (columnDetail.Code)
                {
                    case "TOMaster_DateConfig":
                        if (item.TOMaster_DateConfig.HasValue)
                        {
                            obj.DateData = item.TOMaster_DateConfig.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOMaster_ETD":
                        if (item.TOMaster_ETD.HasValue)
                        {
                            obj.DateData = item.TOMaster_ETD.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOMaster_ETA":
                        if (item.TOMaster_ETA.HasValue)
                        {
                            obj.DateData = item.TOMaster_ETA.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOMaster_ATD":
                        if (item.TOMaster_ATD.HasValue)
                        {
                            obj.DateData = item.TOMaster_ATD.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOMaster_ATA":
                        if (item.TOMaster_ATA.HasValue)
                        {
                            obj.DateData = item.TOMaster_ATA.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOMaster_InvoiceDate":
                        if (item.TOMaster_InvoiceDate.HasValue)
                        {
                            obj.DateData = item.TOMaster_InvoiceDate.Value.Date;
                            flag = true;
                        }
                        break;
                }
                #endregion

                #region Detail
                if (flag)
                {
                    #region Chi tiết
                    var lstDetail = ListColumn.Where(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDetail);
                    foreach (var itemDetail in lstDetail)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = itemDetail.ID;
                        KPICollection_TOMaster_GetValue(itemDetail.Code, item, objDetail);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức KPI
                    var detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_TOMaster_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức đạt KPI
                    detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeIsKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_TOMaster_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                        {
                            obj.KPI_CollectionDetail.Add(objDetail);
                            try
                            {
                                obj.IsKPI = Convert.ToBoolean(objDetail.Value);
                            }
                            catch { }
                        }
                    }
                    #endregion

                    model.KPI_Collection.Add(obj);
                }
                #endregion
            }
        }

        private static void KPICollection_TOMaster_GetValue(string field, DTOKPICollection_TOMaster item, KPI_CollectionDetail objDetail)
        {
            switch (field)
            {
                case "TOMaster_ETD":
                    if (item.TOMaster_ETD.HasValue)
                        objDetail.Value = item.TOMaster_ETD.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOMaster_ETA":
                    if (item.TOMaster_ETA.HasValue)
                        objDetail.Value = item.TOMaster_ETA.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOMaster_ATD":
                    if (item.TOMaster_ATD.HasValue)
                        objDetail.Value = item.TOMaster_ATD.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOMaster_ATA":
                    if (item.TOMaster_ATA.HasValue)
                        objDetail.Value = item.TOMaster_ATA.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOMaster_DateConfig":
                    if (item.TOMaster_DateConfig.HasValue)
                        objDetail.Value = item.TOMaster_DateConfig.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOMaster_InvoiceDate":
                    if (item.TOMaster_InvoiceDate.HasValue)
                        objDetail.Value = item.TOMaster_InvoiceDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOMaster_KMStart":
                    objDetail.Value = item.TOMaster_KMStart.ToString();
                    break;
                case "TOMaster_KMEnd":
                    objDetail.Value = item.TOMaster_KMEnd.ToString();
                    break;
                case "TOMaster_KM":
                    objDetail.Value = item.TOMaster_KM.ToString();
                    break;
                case "TOMaster_TonAllow":
                    objDetail.Value = item.TOMaster_TonAllow.ToString();
                    break;
                case "TOMaster_CBMAllow":
                    objDetail.Value = item.TOMaster_CBMAllow.ToString();
                    break;
                case "TOMaster_Ton":
                    objDetail.Value = item.TOMaster_Ton.ToString();
                    break;
                case "TOMaster_CBM":
                    objDetail.Value = item.TOMaster_CBM.ToString();
                    break;
                case "TOMaster_Quantity":
                    objDetail.Value = item.TOMaster_Quantity.ToString();
                    break;
                case "TOMaster_TonTranfer":
                    objDetail.Value = item.TOMaster_TonTranfer.ToString();
                    break;
                case "TOMaster_CBMTranfer":
                    objDetail.Value = item.TOMaster_CBMTranfer.ToString();
                    break;
                case "TOMaster_QuantityTranfer":
                    objDetail.Value = item.TOMaster_QuantityTranfer.ToString();
                    break;
                case "TOMaster_TonBBGN":
                    objDetail.Value = item.TOMaster_TonBBGN.ToString();
                    break;
                case "TOMaster_CBMBBGN":
                    objDetail.Value = item.TOMaster_CBMBBGN.ToString();
                    break;
                case "TOMaster_QuantityBBGN":
                    objDetail.Value = item.TOMaster_QuantityBBGN.ToString();
                    break;
                case "TOMaster_TonReturn":
                    objDetail.Value = item.TOMaster_TonReturn.ToString();
                    break;
                case "TOMaster_CBMReturn":
                    objDetail.Value = item.TOMaster_CBMReturn.ToString();
                    break;
                case "TOMaster_QuantityReturn":
                    objDetail.Value = item.TOMaster_QuantityReturn.ToString();
                    break;
                case "TOMaster_TotalCredit":
                    objDetail.Value = item.TOMaster_TotalCredit.ToString();
                    break;
                case "TOMaster_TotalDebit":
                    objDetail.Value = item.TOMaster_TotalDebit.ToString();
                    break;
                case "TOMaster_Container20Plan":
                    objDetail.Value = item.TOMaster_Container20Plan.ToString();
                    break;
                case "TOMaster_Container40Plan":
                    objDetail.Value = item.TOMaster_Container40Plan.ToString();
                    break;
                case "TOMaster_Container40HPlan":
                    objDetail.Value = item.TOMaster_Container40HPlan.ToString();
                    break;
                case "TOMaster_Container20Tranfer":
                    objDetail.Value = item.TOMaster_Container20Tranfer.ToString();
                    break;
                case "TOMaster_Container40Tranfer":
                    objDetail.Value = item.TOMaster_Container40Tranfer.ToString();
                    break;
                case "TOMaster_Container40HTranfer":
                    objDetail.Value = item.TOMaster_Container40HTranfer.ToString();
                    break;

            }
        }

        public static string KPICollection_TOMaster_GetKPI(DTOKPICollection_TOMaster item, string strExpr)
        {
            string result = string.Empty;
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                worksheet.Cells[row, col].Value = item.TOMaster_ETD;
                strExp.Replace("[TOMaster_ETD]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_ETA;
                strExp.Replace("[TOMaster_ETA]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_ATD;
                strExp.Replace("[TOMaster_ATD]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_ATA;
                strExp.Replace("[TOMaster_ATA]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_DateConfig;
                strExp.Replace("[TOMaster_DateConfig]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_InvoiceDate;
                strExp.Replace("[TOMaster_InvoiceDate]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_KMStart;
                strExp.Replace("[TOMaster_KMStart]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_KMEnd;
                strExp.Replace("[TOMaster_KMEnd]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_KM;
                strExp.Replace("[TOMaster_KM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_TonAllow;
                strExp.Replace("[TOMaster_TonAllow]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_CBMAllow;
                strExp.Replace("[TOMaster_CBMAllow]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Ton;
                strExp.Replace("[TOMaster_Ton]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_CBM;
                strExp.Replace("[TOMaster_CBM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Quantity;
                strExp.Replace("[TOMaster_Quantity]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_TonTranfer;
                strExp.Replace("[TOMaster_TonTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_CBMTranfer;
                strExp.Replace("[TOMaster_CBMTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_QuantityTranfer;
                strExp.Replace("[TOMaster_QuantityTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_TonBBGN;
                strExp.Replace("[TOMaster_TonBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_CBMBBGN;
                strExp.Replace("[TOMaster_CBMBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_QuantityBBGN;
                strExp.Replace("[TOMaster_QuantityBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_TonReturn;
                strExp.Replace("[TOMaster_TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_CBMReturn;
                strExp.Replace("[TOMaster_CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_QuantityReturn;
                strExp.Replace("[TOMaster_QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_TotalCredit;
                strExp.Replace("[TOMaster_TotalCredit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_TotalDebit;
                strExp.Replace("[TOMaster_TotalDebit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Container20Plan;
                strExp.Replace("[TOMaster_Container20Plan]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Container40Plan;
                strExp.Replace("[TOMaster_Container40Plan]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Container40HPlan;
                strExp.Replace("[TOMaster_Container40HPlan]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Container20Tranfer;
                strExp.Replace("[TOMaster_Container20Tranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Container40Tranfer;
                strExp.Replace("[TOMaster_Container40Tranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOMaster_Container40HTranfer;
                strExp.Replace("[TOMaster_Container40HTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                package.Workbook.Calculate();
                result = worksheet.Cells[row, col].Value.ToString().Trim();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }


        public static void KPICollection_TOGroupProduct_Generate(DataEntities model, AccountItem Account, List<DTOKPICollection_TOGroupProduct> ListData, List<DTOKPICollection_Column> ListColumn)
        {
            var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);

            foreach (var item in ListData)
            {
                item.TOGroupProduct_TotalCredit = item.ListGroupFIN.Sum(c => c.Credit);
                item.TOGroupProduct_TotalDebit = item.ListGroupFIN.Sum(c => c.Debit);

                KPI_Collection obj = new KPI_Collection();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                obj.CustomerID = item.TOGroupProduct_CustomerID;
                obj.OrderID = item.TOGroupProduct_OrderID;
                obj.OrderGroupProductID = item.TOGroupProduct_OrderGroupProductID;
                obj.DITOGroupProductID = item.TOGroupProduct_ID;
                bool flag = false;
                switch (columnDetail.Code)
                {
                    case "TOGroupProduct_DateRequest":
                        if (item.TOGroupProduct_DateRequest.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateRequest.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_ExternalDate":
                        if (item.TOGroupProduct_ExternalDate.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_ExternalDate.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_ETD":
                        if (item.TOGroupProduct_ETD.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_ETD.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_ETA":
                        if (item.TOGroupProduct_ETA.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_ETA.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_ETARequest":
                        if (item.TOGroupProduct_ETARequest.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_ETARequest.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateFromCome":
                        if (item.TOGroupProduct_DateFromCome.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateFromCome.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateFromLeave":
                        if (item.TOGroupProduct_DateFromLeave.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateFromLeave.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateFromLoadStart":
                        if (item.TOGroupProduct_DateFromLoadStart.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateFromLoadStart.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateFromLoadEnd":
                        if (item.TOGroupProduct_DateFromLoadEnd.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateFromLoadEnd.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateToCome":
                        if (item.TOGroupProduct_DateToCome.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateToCome.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateToLeave":
                        if (item.TOGroupProduct_DateToLeave.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateToLeave.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateToLoadStart":
                        if (item.TOGroupProduct_DateToLoadStart.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateToLoadStart.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateToLoadEnd":
                        if (item.TOGroupProduct_DateToLoadEnd.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateToLoadEnd.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_InvoiceDate":
                        if (item.TOGroupProduct_InvoiceDate.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_InvoiceDate.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_InvoiceReturnDate":
                        if (item.TOGroupProduct_InvoiceReturnDate.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_InvoiceReturnDate.Value.Date;
                            flag = true;
                        }
                        break;
                    case "TOGroupProduct_DateConfig":
                        if (item.TOGroupProduct_DateConfig.HasValue)
                        {
                            obj.DateData = item.TOGroupProduct_DateConfig.Value.Date;
                            flag = true;
                        }
                        break;
                }

                // Chỉ thiết lập khi DateData có value
                if (flag)
                {
                    #region Chi tiết
                    var lstDetail = ListColumn.Where(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDetail);
                    foreach (var itemDetail in lstDetail)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = itemDetail.ID;
                        KPICollection_TOGroupProduct_GetValue(itemDetail.Code, item, objDetail);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức KPI
                    var detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_TOGroupProduct_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                            obj.KPI_CollectionDetail.Add(objDetail);
                    }
                    #endregion

                    #region Công thức đạt KPI
                    detailKPI = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeIsKPI && !string.IsNullOrEmpty(c.ExprData));
                    if (detailKPI != null)
                    {
                        KPI_CollectionDetail objDetail = new KPI_CollectionDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.ColumnID = detailKPI.ID;
                        objDetail.Value = KPICollection_TOGroupProduct_GetKPI(item, detailKPI.ExprData);
                        if (!string.IsNullOrEmpty(objDetail.Value))
                        {
                            obj.KPI_CollectionDetail.Add(objDetail);
                            try
                            {
                                obj.IsKPI = Convert.ToBoolean(objDetail.Value);
                            }
                            catch { }
                        }
                    }
                    #endregion

                    model.KPI_Collection.Add(obj);
                }
            }
        }

        private static void KPICollection_TOGroupProduct_GetValue(string field, DTOKPICollection_TOGroupProduct item, KPI_CollectionDetail objDetail)
        {
            switch (field)
            {
                case "TOGroupProduct_DateRequest":
                    if (item.TOGroupProduct_DateRequest.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateRequest.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_ExternalDate":
                    if (item.TOGroupProduct_ExternalDate.HasValue)
                        objDetail.Value = item.TOGroupProduct_ExternalDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_ETD":
                    if (item.TOGroupProduct_ETD.HasValue)
                        objDetail.Value = item.TOGroupProduct_ETD.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_ETA":
                    if (item.TOGroupProduct_ETA.HasValue)
                        objDetail.Value = item.TOGroupProduct_ETA.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_ETARequest":
                    if (item.TOGroupProduct_ETARequest.HasValue)
                        objDetail.Value = item.TOGroupProduct_ETARequest.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateFromCome":
                    if (item.TOGroupProduct_DateFromCome.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateFromCome.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateFromLeave":
                    if (item.TOGroupProduct_DateFromLeave.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateFromLeave.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateFromLoadStart":
                    if (item.TOGroupProduct_DateFromLoadStart.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateFromLoadStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateFromLoadEnd":
                    if (item.TOGroupProduct_DateFromLoadEnd.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateFromLoadEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateToCome":
                    if (item.TOGroupProduct_DateToCome.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateToCome.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateToLeave":
                    if (item.TOGroupProduct_DateToLeave.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateToLeave.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateToLoadStart":
                    if (item.TOGroupProduct_DateToLoadStart.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateToLoadStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateToLoadEnd":
                    if (item.TOGroupProduct_DateToLoadEnd.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateToLoadEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_InvoiceDate":
                    if (item.TOGroupProduct_InvoiceDate.HasValue)
                        objDetail.Value = item.TOGroupProduct_InvoiceDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_InvoiceReturnDate":
                    if (item.TOGroupProduct_InvoiceReturnDate.HasValue)
                        objDetail.Value = item.TOGroupProduct_InvoiceReturnDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_DateConfig":
                    if (item.TOGroupProduct_DateConfig.HasValue)
                        objDetail.Value = item.TOGroupProduct_DateConfig.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "TOGroupProduct_Ton":
                    objDetail.Value = item.TOGroupProduct_Ton.ToString();
                    break;
                case "TOGroupProduct_CBM":
                    objDetail.Value = item.TOGroupProduct_CBM.ToString();
                    break;
                case "TOGroupProduct_Quantity":
                    objDetail.Value = item.TOGroupProduct_Quantity.ToString();
                    break;
                case "TOGroupProduct_TonTranfer":
                    objDetail.Value = item.TOGroupProduct_TonTranfer.ToString();
                    break;
                case "TOGroupProduct_CBMTranfer":
                    objDetail.Value = item.TOGroupProduct_CBMTranfer.ToString();
                    break;
                case "TOGroupProduct_QuantityTranfer":
                    objDetail.Value = item.TOGroupProduct_QuantityTranfer.ToString();
                    break;
                case "TOGroupProduct_TonBBGN":
                    objDetail.Value = item.TOGroupProduct_TonBBGN.ToString();
                    break;
                case "TOGroupProduct_CBMBBGN":
                    objDetail.Value = item.TOGroupProduct_CBMBBGN.ToString();
                    break;
                case "TOGroupProduct_QuantityBBGN":
                    objDetail.Value = item.TOGroupProduct_QuantityBBGN.ToString();
                    break;
                case "TOGroupProduct_TonReturn":
                    objDetail.Value = item.TOGroupProduct_TonReturn.ToString();
                    break;
                case "TOGroupProduct_CBMReturn":
                    objDetail.Value = item.TOGroupProduct_CBMReturn.ToString();
                    break;
                case "TOGroupProduct_QuantityReturn":
                    objDetail.Value = item.TOGroupProduct_QuantityReturn.ToString();
                    break;
                case "TOGroupProduct_TonPlus":
                    objDetail.Value = item.TOGroupProduct_TonPlus.ToString();
                    break;
                case "TOGroupProduct_CBMPlus":
                    objDetail.Value = item.TOGroupProduct_CBMPlus.ToString();
                    break;
                case "TOGroupProduct_QuantityPlus":
                    objDetail.Value = item.TOGroupProduct_QuantityPlus.ToString();
                    break;
                case "TOGroupProduct_TonCancel":
                    objDetail.Value = item.TOGroupProduct_TonCancel.ToString();
                    break;
                case "TOGroupProduct_CBMCancel":
                    objDetail.Value = item.TOGroupProduct_CBMCancel.ToString();
                    break;
                case "TOGroupProduct_QuantityCancel":
                    objDetail.Value = item.TOGroupProduct_QuantityCancel.ToString();
                    break;
                case "TOGroupProduct_TotalCredit":
                    objDetail.Value = item.TOGroupProduct_TotalCredit.ToString();
                    break;
                case "TOGroupProduct_TotalDebit":
                    objDetail.Value = item.TOGroupProduct_TotalDebit.ToString();
                    break;
            }
        }

        public static string KPICollection_TOGroupProduct_GetKPI(DTOKPICollection_TOGroupProduct item, string strExpr)
        {
            string result = string.Empty;
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateRequest;
                strExp.Replace("[TOGroupProduct_DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_ExternalDate;
                strExp.Replace("[TOGroupProduct_ExternalDate]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_ETD;
                strExp.Replace("[TOGroupProduct_ETD]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_ETA;
                strExp.Replace("[TOGroupProduct_ETA]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_ETARequest;
                strExp.Replace("[TOGroupProduct_ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateFromCome;
                strExp.Replace("[TOGroupProduct_DateFromCome]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateFromLeave;
                strExp.Replace("[TOGroupProduct_DateFromLeave]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateFromLoadStart;
                strExp.Replace("[TOGroupProduct_DateFromLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateFromLoadEnd;
                strExp.Replace("[TOGroupProduct_DateFromLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateToCome;
                strExp.Replace("[TOGroupProduct_DateToCome]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateToLeave;
                strExp.Replace("[TOGroupProduct_DateToLeave]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateToLoadStart;
                strExp.Replace("[TOGroupProduct_DateToLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateToLoadEnd;
                strExp.Replace("[TOGroupProduct_DateToLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_InvoiceDate;
                strExp.Replace("[TOGroupProduct_InvoiceDate]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_InvoiceReturnDate;
                strExp.Replace("[TOGroupProduct_InvoiceReturnDate]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_DateConfig;
                strExp.Replace("[TOGroupProduct_DateConfig]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_Ton;
                strExp.Replace("[TOGroupProduct_Ton]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_CBM;
                strExp.Replace("[TOGroupProduct_CBM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_Quantity;
                strExp.Replace("[TOGroupProduct_Quantity]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_TonTranfer;
                strExp.Replace("[TOGroupProduct_TonTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_CBMTranfer;
                strExp.Replace("[TOGroupProduct_CBMTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_QuantityTranfer;
                strExp.Replace("[TOGroupProduct_QuantityTranfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_TonBBGN;
                strExp.Replace("[TOGroupProduct_TonBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_CBMBBGN;
                strExp.Replace("[TOGroupProduct_CBMBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_QuantityBBGN;
                strExp.Replace("[TOGroupProduct_QuantityBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_TonReturn;
                strExp.Replace("[TOGroupProduct_TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_CBMReturn;
                strExp.Replace("[TOGroupProduct_CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_QuantityReturn;
                strExp.Replace("[TOGroupProduct_QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_TonPlus;
                strExp.Replace("[TOGroupProduct_TonPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_CBMPlus;
                strExp.Replace("[TOGroupProduct_CBMPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_QuantityPlus;
                strExp.Replace("[TOGroupProduct_QuantityPlus]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_TonCancel;
                strExp.Replace("[TOGroupProduct_TonCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_CBMCancel;
                strExp.Replace("[TOGroupProduct_CBMCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_QuantityCancel;
                strExp.Replace("[TOGroupProduct_QuantityCancel]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_TotalCredit;
                strExp.Replace("[TOGroupProduct_TotalCredit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TOGroupProduct_TotalDebit;
                strExp.Replace("[TOGroupProduct_TotalDebit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                package.Workbook.Calculate();
                result = worksheet.Cells[row, col].Value.ToString().Trim();
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }
        #endregion

        #region KPIQuantity
        public static KPIQuantityDate KPIQuantity_CheckQuantity(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                KPIQuantityDate result = new KPIQuantityDate();
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExpTon = new StringBuilder(item.ExpressionTon);
                StringBuilder strExpCBM = new StringBuilder(item.ExpressionCBM);
                StringBuilder strExpQuantity = new StringBuilder(item.ExpressionQuantity);

                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (item.TypeOfKPICode != itemKPI.TypeOfKPICode && !string.IsNullOrEmpty(itemKPI.TypeOfKPICode))
                    {
                        dicEx[itemKPI.TypeOfKPICode + ".KPITon"] = itemKPI.KPITon.HasValue ? item.KPITon.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPICBM"] = itemKPI.KPICBM.HasValue ? item.KPICBM.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPIQuantity"] = itemKPI.KPIQuantity.HasValue ? item.KPIQuantity.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".IsKPI"] = itemKPI.IsKPI.HasValue ? item.IsKPI.ToString() : "";
                    }
                }
                var lstExKey = dicEx.Keys.ToArray();
                row++;

                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest;
                strExpTon.Replace("[DateOrderRequest]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest.Value.Date;
                strExpTon.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderRequest.Value.Hour + "," + item.DateOrderRequest.Value.Minute + "," + item.DateOrderRequest.Value.Second + ")";
                strExpTon.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                strExpTon.Replace("[DateTOMasterETD]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                strExpTon.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                strExpTon.Replace("[DateTOMasterETA]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                strExpTon.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                strExpTon.Replace("[DateTOMasterATD]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                strExpTon.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                strExpTon.Replace("[DateTOMasterATA]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                strExpTon.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonOrder != null) worksheet.Cells[row, col].Value = item.TonOrder;
                strExpTon.Replace("[TonOrder]", strCol + row);
                strExpCBM.Replace("[TonOrder]", strCol + row);
                strExpQuantity.Replace("[TonOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonOrder]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMOrder != null) worksheet.Cells[row, col].Value = item.CBMOrder;
                strExpTon.Replace("[CBMOrder]", strCol + row);
                strExpCBM.Replace("[CBMOrder]", strCol + row);
                strExpQuantity.Replace("[CBMOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMOrder]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantityOrder != null) worksheet.Cells[row, col].Value = item.QuantityOrder;
                strExpTon.Replace("[QuantityOrder]", strCol + row);
                strExpCBM.Replace("[QuantityOrder]", strCol + row);
                strExpQuantity.Replace("[QuantityOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonTranfer != null) worksheet.Cells[row, col].Value = item.TonTranfer;
                strExpTon.Replace("[TonTranfer]", strCol + row);
                strExpCBM.Replace("[TonTranfer]", strCol + row);
                strExpQuantity.Replace("[TonTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonTranfer]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMTranfer != null) worksheet.Cells[row, col].Value = item.CBMTranfer;
                strExpTon.Replace("[CBMTranfer]", strCol + row);
                strExpCBM.Replace("[CBMTranfer]", strCol + row);
                strExpQuantity.Replace("[CBMTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMTranfer]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantityTranfer != null) worksheet.Cells[row, col].Value = item.QuantityTranfer;
                strExpTon.Replace("[QuantityTranfer]", strCol + row);
                strExpCBM.Replace("[QuantityTranfer]", strCol + row);
                strExpQuantity.Replace("[QuantityTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityTranfer]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonReceive != null) worksheet.Cells[row, col].Value = item.TonReceive;
                strExpTon.Replace("[TonReceive]", strCol + row);
                strExpCBM.Replace("[TonReceive]", strCol + row);
                strExpQuantity.Replace("[TonReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonReceive]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMReceive != null) worksheet.Cells[row, col].Value = item.CBMReceive;
                strExpTon.Replace("[CBMReceive]", strCol + row);
                strExpCBM.Replace("[CBMReceive]", strCol + row);
                strExpQuantity.Replace("[CBMReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMReceive]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantityReceive != null) worksheet.Cells[row, col].Value = item.QuantityReceive;
                strExpTon.Replace("[QuantityReceive]", strCol + row);
                strExpCBM.Replace("[QuantityReceive]", strCol + row);
                strExpQuantity.Replace("[QuantityReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityReceive]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonReturn != null) worksheet.Cells[row, col].Value = item.TonReturn;
                strExpTon.Replace("[TonReturn]", strCol + row);
                strExpCBM.Replace("[TonReturn]", strCol + row);
                strExpQuantity.Replace("[TonReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonReturn]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMReturn != null) worksheet.Cells[row, col].Value = item.CBMReturn;
                strExpTon.Replace("[CBMReturn]", strCol + row);
                strExpCBM.Replace("[CBMReturn]", strCol + row);
                strExpQuantity.Replace("[CBMReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMReturn]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantiyReturn != null) worksheet.Cells[row, col].Value = item.QuantiyReturn;
                strExpTon.Replace("[QuantiyReturn]", strCol + row);
                strExpCBM.Replace("[QuantiyReturn]", strCol + row);
                strExpQuantity.Replace("[QuantiyReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantiyReturn]", strCol + row);
                strRow = strCol + row; row++;


                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExpTon.Replace("[" + valEx.Key + "]", strCol + row);
                    strExpCBM.Replace("[" + valEx.Key + "]", strCol + row);
                    strExpQuantity.Replace("[" + valEx.Key + "]", strCol + row);
                }

                row++;
                worksheet.Cells[row, col].Formula = strExpTon.ToString();
                row++;
                worksheet.Cells[row, col].Formula = strExpCBM.ToString();
                row++;
                worksheet.Cells[row, col].Formula = strExpQuantity.ToString();

                package.Workbook.Calculate();
                var valTon = worksheet.Cells[row - 2, col].Value.ToString().Trim();
                var valCBM = worksheet.Cells[row - 1, col].Value.ToString().Trim();
                var valQuantity = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                { result.KPITon = Convert.ToDouble(valTon); }
                catch { }
                try
                { result.KPICBM = Convert.ToDouble(valCBM); }
                catch { }
                try
                { result.KPIQuantity = Convert.ToDouble(valQuantity); }
                catch { }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool? KPIQuantity_CheckBool(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                bool? result = null;

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExpTon = new StringBuilder(item.ExpressionTon);
                StringBuilder strExpCBM = new StringBuilder(item.ExpressionCBM);
                StringBuilder strExpQuantity = new StringBuilder(item.ExpressionQuantity);

                StringBuilder strExpCompareFieldTon = new StringBuilder(item.CompareFieldTon);
                StringBuilder strExpCompareFieldCBM = new StringBuilder(item.CompareFieldCBM);
                StringBuilder strExpCompareFieldQuantity = new StringBuilder(item.CompareFieldQuantity);

                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (item.TypeOfKPICode != itemKPI.TypeOfKPICode && !string.IsNullOrEmpty(itemKPI.TypeOfKPICode))
                    {
                        dicEx[itemKPI.TypeOfKPICode + ".KPITon"] = itemKPI.KPITon.HasValue ? itemKPI.KPITon.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPICBM"] = itemKPI.KPICBM.HasValue ? itemKPI.KPICBM.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPIQuantity"] = itemKPI.KPIQuantity.HasValue ? itemKPI.KPIQuantity.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".IsKPI"] = itemKPI.IsKPI.HasValue ? item.IsKPI.ToString() : "";
                    }
                }
                var lstExKey = dicEx.Keys.ToArray();
                row++;

                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest;
                strExpTon.Replace("[DateOrderRequest]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest]", strCol + row);
                strExpCompareFieldTon.Replace("[DateOrderRequest]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateOrderRequest]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateOrderRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest.Value.Date;
                strExpTon.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateOrderRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderRequest.Value.Hour + "," + item.DateOrderRequest.Value.Minute + "," + item.DateOrderRequest.Value.Second + ")";
                strExpTon.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateOrderRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                strExpTon.Replace("[DateTOMasterETD]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETD]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETD]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                strExpTon.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                strExpTon.Replace("[DateTOMasterETA]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETA]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETA]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                strExpTon.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                strExpTon.Replace("[DateTOMasterATD]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATD]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATD]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                strExpTon.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                strExpTon.Replace("[DateTOMasterATA]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATA]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATA]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                strExpTon.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                strExpTon.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonOrder != null) worksheet.Cells[row, col].Value = item.TonOrder;
                strExpTon.Replace("[TonOrder]", strCol + row);
                strExpCBM.Replace("[TonOrder]", strCol + row);
                strExpQuantity.Replace("[TonOrder]", strCol + row);
                strExpCompareFieldTon.Replace("[TonOrder]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonOrder]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonOrder]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMOrder != null) worksheet.Cells[row, col].Value = item.CBMOrder;
                strExpTon.Replace("[CBMOrder]", strCol + row);
                strExpCBM.Replace("[CBMOrder]", strCol + row);
                strExpQuantity.Replace("[CBMOrder]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMOrder]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMOrder]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMOrder]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantityOrder != null) worksheet.Cells[row, col].Value = item.QuantityOrder;
                strExpTon.Replace("[QuantityOrder]", strCol + row);
                strExpCBM.Replace("[QuantityOrder]", strCol + row);
                strExpQuantity.Replace("[QuantityOrder]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantityOrder]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantityOrder]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantityOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonTranfer != null) worksheet.Cells[row, col].Value = item.TonTranfer;
                strExpTon.Replace("[TonTranfer]", strCol + row);
                strExpCBM.Replace("[TonTranfer]", strCol + row);
                strExpQuantity.Replace("[TonTranfer]", strCol + row);
                strExpCompareFieldTon.Replace("[TonTranfer]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonTranfer]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonTranfer]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMTranfer != null) worksheet.Cells[row, col].Value = item.CBMTranfer;
                strExpTon.Replace("[CBMTranfer]", strCol + row);
                strExpCBM.Replace("[CBMTranfer]", strCol + row);
                strExpQuantity.Replace("[CBMTranfer]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMTranfer]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMTranfer]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMTranfer]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantityTranfer != null) worksheet.Cells[row, col].Value = item.QuantityTranfer;
                strExpTon.Replace("[QuantityTranfer]", strCol + row);
                strExpCBM.Replace("[QuantityTranfer]", strCol + row);
                strExpQuantity.Replace("[QuantityTranfer]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantityTranfer]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantityTranfer]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantityTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityTranfer]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonReceive != null) worksheet.Cells[row, col].Value = item.TonReceive;
                strExpTon.Replace("[TonReceive]", strCol + row);
                strExpCBM.Replace("[TonReceive]", strCol + row);
                strExpQuantity.Replace("[TonReceive]", strCol + row);
                strExpCompareFieldTon.Replace("[TonReceive]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonReceive]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonReceive]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMReceive != null) worksheet.Cells[row, col].Value = item.CBMReceive;
                strExpTon.Replace("[CBMReceive]", strCol + row);
                strExpCBM.Replace("[CBMReceive]", strCol + row);
                strExpQuantity.Replace("[CBMReceive]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMReceive]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMReceive]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMReceive]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantityReceive != null) worksheet.Cells[row, col].Value = item.QuantityReceive;
                strExpTon.Replace("[QuantityReceive]", strCol + row);
                strExpCBM.Replace("[QuantityReceive]", strCol + row);
                strExpQuantity.Replace("[QuantityReceive]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantityReceive]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantityReceive]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantityReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityReceive]", strCol + row);
                strRow = strCol + row; row++;


                if (item.TonReturn != null) worksheet.Cells[row, col].Value = item.TonReturn;
                strExpTon.Replace("[TonReturn]", strCol + row);
                strExpCBM.Replace("[TonReturn]", strCol + row);
                strExpQuantity.Replace("[TonReturn]", strCol + row);
                strExpCompareFieldTon.Replace("[TonReturn]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonReturn]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonReturn]", strCol + row);
                strRow = strCol + row; row++;


                if (item.CBMReturn != null) worksheet.Cells[row, col].Value = item.CBMReturn;
                strExpTon.Replace("[CBMReturn]", strCol + row);
                strExpCBM.Replace("[CBMReturn]", strCol + row);
                strExpQuantity.Replace("[CBMReturn]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMReturn]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMReturn]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMReturn]", strCol + row);
                strRow = strCol + row; row++;


                if (item.QuantiyReturn != null) worksheet.Cells[row, col].Value = item.QuantiyReturn;
                strExpTon.Replace("[QuantiyReturn]", strCol + row);
                strExpCBM.Replace("[QuantiyReturn]", strCol + row);
                strExpQuantity.Replace("[QuantiyReturn]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantiyReturn]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantiyReturn]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantiyReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantiyReturn]", strCol + row);
                strRow = strCol + row; row++;


                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExpTon.Replace("[" + valEx.Key + "]", strCol + row);
                    strExpCBM.Replace("[" + valEx.Key + "]", strCol + row);
                    strExpQuantity.Replace("[" + valEx.Key + "]", strCol + row);
                }
                row++;

                worksheet.Cells[row, col].Formula = strExpTon.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCBM.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpQuantity.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCompareFieldTon.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCompareFieldCBM.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCompareFieldQuantity.ToString();
                row++;

                var rowTon = worksheet.Cells[row - 6, col].Address;
                var rowCBM = worksheet.Cells[row - 5, col].Address;
                var rowQuantity = worksheet.Cells[row - 4, col].Address;
                var rowCompareTon = worksheet.Cells[row - 3, col].Address;
                var rowCompareCBM = worksheet.Cells[row - 2, col].Address;
                var rowCompareQuantity = worksheet.Cells[row - 1, col].Address;

                // IF(OR(A1="",A2="",A3="",A4="",A5="",A6=""),"N",IF(AND(A1>=A4,A2>=A5,A3>=A6),"T","F"))
                worksheet.Cells[row, col].Formula = "IF(OR(" + rowTon + "=\"\"," + rowCBM + "=\"\"," + rowQuantity + "=\"\"," + rowCompareTon + "=\"\"," + rowCompareCBM + "=\"\"," + rowCompareQuantity + "=\"\"),\"N\",IF(AND(" + rowTon + ">=" + rowCompareTon + "," + rowCBM + ">=" + rowCompareCBM + "," + rowQuantity + ">=" + rowCompareQuantity + ")" + ",\"T\",\"F\"))";

                package.Workbook.Calculate();

                var valTon = worksheet.Cells[row - 6, col].Value.ToString().Trim();
                var valCBM = worksheet.Cells[row - 5, col].Value.ToString().Trim();
                var valQuantity = worksheet.Cells[row - 4, col].Value.ToString().Trim();
                var calCompareTon = worksheet.Cells[row - 3, col].Value.ToString().Trim();
                var calCompareCBM = worksheet.Cells[row - 2, col].Value.ToString().Trim();
                var calCompareQuantity = worksheet.Cells[row - 1, col].Value.ToString().Trim();
                var valKPI = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                { item.KPITon = Convert.ToDouble(valTon); }
                catch { }
                try
                { item.KPICBM = Convert.ToDouble(valCBM); }
                catch { }
                try
                { item.KPIQuantity = Convert.ToDouble(valQuantity); }
                catch { }

                if (valKPI == "T") result = true;
                else if (valKPI == "F") result = false;
                else if (valKPI == "N") result = null;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ExcelPackage KPIQuantity_GetPackage(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                //string file = "/MailTemplate/" + "KPI_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                //    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                //FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                //ExcelPackage result = new ExcelPackage(exportfile);

                ExcelPackage result = new ExcelPackage();
                ExcelWorksheet worksheet = result.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExpTon = new StringBuilder(item.ExpressionTon);
                StringBuilder strExpCBM = new StringBuilder(item.ExpressionCBM);
                StringBuilder strExpQuantity = new StringBuilder(item.ExpressionQuantity);

                StringBuilder strExpCompareFieldTon = new StringBuilder(item.CompareFieldTon);
                StringBuilder strExpCompareFieldCBM = new StringBuilder(item.CompareFieldCBM);
                StringBuilder strExpCompareFieldQuantity = new StringBuilder(item.CompareFieldQuantity);

                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (item.TypeOfKPICode != itemKPI.TypeOfKPICode && !string.IsNullOrEmpty(itemKPI.TypeOfKPICode))
                    {
                        dicEx[itemKPI.TypeOfKPICode + ".KPITon"] = itemKPI.KPITon.HasValue ? itemKPI.KPITon.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPICBM"] = itemKPI.KPICBM.HasValue ? itemKPI.KPICBM.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPIQuantity"] = itemKPI.KPIQuantity.HasValue ? itemKPI.KPIQuantity.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".IsKPI"] = itemKPI.IsKPI.HasValue ? item.IsKPI.ToString() : "";
                    }
                }
                var lstExKey = dicEx.Keys.ToArray();
                row++;

                strExpTon.Replace("[DateOrderRequest]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest]", strCol + row);
                strExpCompareFieldTon.Replace("[DateOrderRequest]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateOrderRequest]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateOrderRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateOrderRequest.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateOrderRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCBM.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpQuantity.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateOrderRequest.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateOrderRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[DateTOMasterETD]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETD]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETD]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETD.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETD.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[DateTOMasterETA]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETA]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETA]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETA.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterETA.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[DateTOMasterATD]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATD]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATD]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATD.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATD.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[DateTOMasterATA]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATA]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATA]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATA.Date]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExpTon.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCBM.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpQuantity.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCompareFieldTon.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCompareFieldCBM.Replace("[DateTOMasterATA.Time]", strCol + row);
                strExpCompareFieldQuantity.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[TonOrder]", strCol + row);
                strExpCBM.Replace("[TonOrder]", strCol + row);
                strExpQuantity.Replace("[TonOrder]", strCol + row);
                strExpCompareFieldTon.Replace("[TonOrder]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonOrder]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonOrder]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[CBMOrder]", strCol + row);
                strExpCBM.Replace("[CBMOrder]", strCol + row);
                strExpQuantity.Replace("[CBMOrder]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMOrder]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMOrder]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMOrder]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[QuantityOrder]", strCol + row);
                strExpCBM.Replace("[QuantityOrder]", strCol + row);
                strExpQuantity.Replace("[QuantityOrder]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantityOrder]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantityOrder]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantityOrder]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[TonTranfer]", strCol + row);
                strExpCBM.Replace("[TonTranfer]", strCol + row);
                strExpQuantity.Replace("[TonTranfer]", strCol + row);
                strExpCompareFieldTon.Replace("[TonTranfer]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonTranfer]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonTranfer]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[CBMTranfer]", strCol + row);
                strExpCBM.Replace("[CBMTranfer]", strCol + row);
                strExpQuantity.Replace("[CBMTranfer]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMTranfer]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMTranfer]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMTranfer]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[QuantityTranfer]", strCol + row);
                strExpCBM.Replace("[QuantityTranfer]", strCol + row);
                strExpQuantity.Replace("[QuantityTranfer]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantityTranfer]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantityTranfer]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantityTranfer]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityTranfer]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[TonReceive]", strCol + row);
                strExpCBM.Replace("[TonReceive]", strCol + row);
                strExpQuantity.Replace("[TonReceive]", strCol + row);
                strExpCompareFieldTon.Replace("[TonReceive]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonReceive]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonReceive]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[CBMReceive]", strCol + row);
                strExpCBM.Replace("[CBMReceive]", strCol + row);
                strExpQuantity.Replace("[CBMReceive]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMReceive]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMReceive]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMReceive]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[QuantityReceive]", strCol + row);
                strExpCBM.Replace("[QuantityReceive]", strCol + row);
                strExpQuantity.Replace("[QuantityReceive]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantityReceive]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantityReceive]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantityReceive]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantityReceive]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[TonReturn]", strCol + row);
                strExpCBM.Replace("[TonReturn]", strCol + row);
                strExpQuantity.Replace("[TonReturn]", strCol + row);
                strExpCompareFieldTon.Replace("[TonReturn]", strCol + row);
                strExpCompareFieldCBM.Replace("[TonReturn]", strCol + row);
                strExpCompareFieldQuantity.Replace("[TonReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[TonReturn]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[CBMReturn]", strCol + row);
                strExpCBM.Replace("[CBMReturn]", strCol + row);
                strExpQuantity.Replace("[CBMReturn]", strCol + row);
                strExpCompareFieldTon.Replace("[CBMReturn]", strCol + row);
                strExpCompareFieldCBM.Replace("[CBMReturn]", strCol + row);
                strExpCompareFieldQuantity.Replace("[CBMReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[CBMReturn]", strCol + row);
                strRow = strCol + row; row++;


                strExpTon.Replace("[QuantiyReturn]", strCol + row);
                strExpCBM.Replace("[QuantiyReturn]", strCol + row);
                strExpQuantity.Replace("[QuantiyReturn]", strCol + row);
                strExpCompareFieldTon.Replace("[QuantiyReturn]", strCol + row);
                strExpCompareFieldCBM.Replace("[QuantiyReturn]", strCol + row);
                strExpCompareFieldQuantity.Replace("[QuantiyReturn]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[QuantiyReturn]", strCol + row);
                strRow = strCol + row; row++;


                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExpTon.Replace("[" + valEx.Key + "]", strCol + row);
                    strExpCBM.Replace("[" + valEx.Key + "]", strCol + row);
                    strExpQuantity.Replace("[" + valEx.Key + "]", strCol + row);
                }
                row++;

                worksheet.Cells[row, col].Formula = strExpTon.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCBM.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpQuantity.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCompareFieldTon.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCompareFieldCBM.ToString();
                row++;

                worksheet.Cells[row, col].Formula = strExpCompareFieldQuantity.ToString();
                row++;

                var rowTon = worksheet.Cells[row - 6, col].Address;
                var rowCBM = worksheet.Cells[row - 5, col].Address;
                var rowQuantity = worksheet.Cells[row - 4, col].Address;
                var rowCompareTon = worksheet.Cells[row - 3, col].Address;
                var rowCompareCBM = worksheet.Cells[row - 2, col].Address;
                var rowCompareQuantity = worksheet.Cells[row - 1, col].Address;

                // IF(OR(A1="",A2="",A3="",A4="",A5="",A6=""),"N",IF(AND(A1>=A4,A2>=A5,A3>=A6),"T","F"))
                worksheet.Cells[row, col].Formula = "IF(OR(" + rowTon + "=\"\"," + rowCBM + "=\"\"," + rowQuantity + "=\"\"," + rowCompareTon + "=\"\"," + rowCompareCBM + "=\"\"," + rowCompareQuantity + "=\"\"),\"N\",IF(AND(" + rowTon + ">=" + rowCompareTon + "," + rowCBM + ">=" + rowCompareCBM + "," + rowQuantity + ">=" + rowCompareQuantity + ")" + ",\"T\",\"F\"))";

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KPIQuantity_GetQuantity(ExcelPackage package, string code, KPI_QuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (code != itemKPI.TypeOfKPICode)
                    {
                        dicEx[itemKPI.TypeOfKPICode + ".KPITon"] = itemKPI.KPITon.HasValue ? itemKPI.KPITon.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPICBM"] = itemKPI.KPICBM.HasValue ? itemKPI.KPICBM.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".KPIQuantity"] = itemKPI.KPIQuantity.HasValue ? itemKPI.KPIQuantity.Value.ToString() : "";
                        dicEx[itemKPI.TypeOfKPICode + ".IsKPI"] = itemKPI.IsKPI.HasValue ? item.IsKPI.ToString() : "";
                    }
                }
                var lstExKey = dicEx.Keys.ToArray();
                row++;

                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderRequest.Value.Hour + "," + item.DateOrderRequest.Value.Minute + "," + item.DateOrderRequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;


                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.TonOrder != null) worksheet.Cells[row, col].Value = item.TonOrder.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.CBMOrder != null) worksheet.Cells[row, col].Value = item.CBMOrder.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.QuantityOrder != null) worksheet.Cells[row, col].Value = item.QuantityOrder.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.TonTranfer != null) worksheet.Cells[row, col].Value = item.TonTranfer.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.CBMTranfer != null) worksheet.Cells[row, col].Value = item.CBMTranfer.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.QuantityTranfer != null) worksheet.Cells[row, col].Value = item.QuantityTranfer.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.TonReceive != null) worksheet.Cells[row, col].Value = item.TonReceive.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.CBMReceive != null) worksheet.Cells[row, col].Value = item.CBMReceive.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.QuantityReceive != null) worksheet.Cells[row, col].Value = item.QuantityReceive.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.TonReturn != null) worksheet.Cells[row, col].Value = item.TonReturn.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.CBMReturn != null) worksheet.Cells[row, col].Value = item.CBMReturn.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.QuantityReturn != null) worksheet.Cells[row, col].Value = item.QuantityReturn.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                foreach (var valEx in dicEx)
                    row++;

                row++;

                //worksheet.Cells[row, col].Formula = strExpTon.ToString();
                row++;

                //worksheet.Cells[row, col].Formula = strExpCBM.ToString();
                row++;

                //worksheet.Cells[row, col].Formula = strExpQuantity.ToString();
                row++;

                //worksheet.Cells[row, col].Formula = strExpCompareFieldTon.ToString();
                row++;

                //worksheet.Cells[row, col].Formula = strExpCompareFieldCBM.ToString();
                row++;

                //worksheet.Cells[row, col].Formula = strExpCompareFieldQuantity.ToString();
                row++;

                // IF(OR(A1="",A2="",A3="",A4="",A5="",A6=""),"N",IF(AND(A1>=A4,A2>=A5,A3>=A6),"T","F"))
                //worksheet.Cells[row, col].Formula = "IF(OR(" + rowTon + "=\"\"," + rowCBM + "=\"\"," + rowQuantity + "=\"\"," + rowCompareTon + "=\"\"," + rowCompareCBM + "=\"\"," + rowCompareQuantity + "=\"\"),\"N\",IF(AND(" + rowTon + ">=" + rowCompareTon + "," + rowCBM + ">=" + rowCompareCBM + "," + rowQuantity + ">=" + rowCompareQuantity + ")" + ",\"T\",\"F\"))";

                package.Workbook.Calculate();

                var valTon = worksheet.Cells[row - 6, col].Value.ToString().Trim();
                var valCBM = worksheet.Cells[row - 5, col].Value.ToString().Trim();
                var valQuantity = worksheet.Cells[row - 4, col].Value.ToString().Trim();
                var calCompareTon = worksheet.Cells[row - 3, col].Value.ToString().Trim();
                var calCompareCBM = worksheet.Cells[row - 2, col].Value.ToString().Trim();
                var calCompareQuantity = worksheet.Cells[row - 1, col].Value.ToString().Trim();
                var valKPI = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                { item.KPITon = Convert.ToDouble(valTon); }
                catch { }
                try
                { item.KPICBM = Convert.ToDouble(valCBM); }
                catch { }
                try
                { item.KPIQuantity = Convert.ToDouble(valQuantity); }
                catch { }

                if (valKPI == "T") item.IsKPI = true;
                else if (valKPI == "F") item.IsKPI = false;
                else if (valKPI == "N") item.IsKPI = null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KPIQuantity_Generate(DataEntities model, AccountItem account, int? ditomasterID, List<int> lstOrderID, List<int> lstDITOGroupProductID, bool IsCustomer)
        {
            #region Data
            DateTime? dtNull = null;
            var lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOMasterID == ditomasterID && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID &&
                    (IsCustomer ? c.ORD_GroupProduct.CUSRoutingID > 0 && c.ORD_GroupProduct.ORD_Order.ContractID > 0 : c.CATRoutingID > 0 && c.OPS_DITOMaster.ContractID > 0)).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        c.DITOMasterID,

                        ContractID = IsCustomer ? c.ORD_GroupProduct.ORD_Order.ContractID.Value : c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.RoutingID : c.CATRoutingID.Value,
                        ParentCATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID : c.CAT_Routing.ParentID,

                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,

                        DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                        OPSDateConfig = c.DateConfig.HasValue ? c.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,

                        TonOrder = c.ORD_GroupProduct.Ton,
                        CBMOrder = c.ORD_GroupProduct.CBM,
                        QuantityOrder = c.ORD_GroupProduct.Quantity,

                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,

                        TonReceive = c.TonBBGN,
                        CBMReceive = c.CBMBBGN,
                        QuantityReceive = c.QuantityBBGN,

                        TonReturn = c.TonReturn,
                        CBMReturn = c.CBMReturn,
                        QuantityReturn = c.QuantityReturn,
                    }).ToList();
            if (lstOrderID != null && lstOrderID.Count > 0)
            {
                lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && lstOrderID.Contains(c.ORD_GroupProduct.OrderID) && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID &&
                    (IsCustomer ? c.ORD_GroupProduct.CUSRoutingID > 0 && c.ORD_GroupProduct.ORD_Order.ContractID > 0 : c.CATRoutingID > 0 && c.DITOMasterID > 0 && c.OPS_DITOMaster.ContractID > 0)).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        c.DITOMasterID,

                        ContractID = IsCustomer ? c.ORD_GroupProduct.ORD_Order.ContractID.Value : c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.RoutingID : c.CATRoutingID.Value,
                        ParentCATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID : c.CAT_Routing.ParentID,

                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,

                        DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                        OPSDateConfig = c.DateConfig.HasValue ? c.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,

                        TonOrder = c.ORD_GroupProduct.Ton,
                        CBMOrder = c.ORD_GroupProduct.CBM,
                        QuantityOrder = c.ORD_GroupProduct.Quantity,

                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,

                        TonReceive = c.TonBBGN,
                        CBMReceive = c.CBMBBGN,
                        QuantityReceive = c.QuantityBBGN,

                        TonReturn = c.TonReturn,
                        CBMReturn = c.CBMReturn,
                        QuantityReturn = c.QuantityReturn,
                    }).ToList();
            }
            if (lstDITOGroupProductID != null && lstDITOGroupProductID.Count > 0)
            {
                lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 &&
                    lstDITOGroupProductID.Contains(c.ID) &&
                    c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        c.DITOMasterID,

                        ContractID = IsCustomer ? c.ORD_GroupProduct.ORD_Order.ContractID.Value : c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.RoutingID : c.CATRoutingID.Value,
                        ParentCATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID : c.CAT_Routing.ParentID,

                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,

                        DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                        OPSDateConfig = c.DateConfig.HasValue ? c.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,

                        TonOrder = c.ORD_GroupProduct.Ton,
                        CBMOrder = c.ORD_GroupProduct.CBM,
                        QuantityOrder = c.ORD_GroupProduct.Quantity,

                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,

                        TonReceive = c.TonBBGN,
                        CBMReceive = c.CBMBBGN,
                        QuantityReceive = c.QuantityBBGN,

                        TonReturn = c.TonReturn,
                        CBMReturn = c.CBMReturn,
                        QuantityReturn = c.QuantityReturn,
                    }).ToList();
            }
            #endregion

            if (lstDITOGroupProduct.Count > 0)
            {
                #region Thiết lập KPI
                var lstContractID = lstDITOGroupProduct.Select(c => c.ContractID).Distinct().ToList();

                var lstKPI = model.CAT_ContractTermKPIQuantityDate.Where(c => lstContractID.Contains(c.CAT_ContractTerm.ContractID)).Select(c => new
                {
                    CustomerID = c.CAT_ContractTerm.CAT_Contract.CustomerID,
                    ContractID = c.CAT_ContractTerm.ContractID,
                    ContractTermID = c.ContractTermID,
                    DateEffect = c.CAT_ContractTerm.DateEffect,
                    DateExpire = c.CAT_ContractTerm.DateExpire,
                    ExpressionTon = c.ExpressionTon,
                    ExpressionCBM = c.ExpressionCBM,
                    ExpressionQuantity = c.ExpressionQuantity,
                    CompareFieldTon = c.CompareFieldTon,
                    CompareFieldCBM = c.CompareFieldCBM,
                    CompareFieldQuantity = c.CompareFieldQuantity,
                    TypeOfKPIID = c.TypeOfKPIID,
                    TypeOfKPICode = c.KPI_TypeOfKPI.Code,
                    Level = c.KPI_TypeOfKPI.Level,
                    KPITypeOfKPIID = c.KPI_TypeOfKPI.KPITypeOfKPIID
                }).OrderBy(c => c.Level).ToList();
                var lstRoutingPrice = model.CAT_ContractRouting.Where(c => lstContractID.Contains(c.ContractID) && c.ContractTermID > 0 && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice).Select(c => new
                {
                    ContractID = c.ContractID,
                    ContractRoutingID = c.ID,
                    CATRoutingID = c.RoutingID,
                    c.Zone,
                    c.LeadTime,
                    c.ContractRoutingTypeID,
                    ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1,
                }).ToList();
                var lstRoutingKPI = model.CAT_ContractRouting.Where(c => lstContractID.Contains(c.ContractID) && c.ContractTermID > 0 && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypeKPI).Select(c => new
                {
                    ContractID = c.ContractID,
                    ContractRoutingID = c.ID,
                    CATRoutingID = c.RoutingID,
                    c.Zone,
                    c.LeadTime,
                    c.ContractRoutingTypeID,
                    ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1,
                }).ToList();
                #endregion

                #region Tính KPI
                foreach (var itemGroupProduct in lstDITOGroupProduct)
                {
                    var itemRouting = lstRoutingPrice.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID);
                    // Xét xem có thiết lập routing cho KPI riêng hay ko
                    if (lstRoutingKPI.Count > 0 && lstRoutingKPI.Count(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.ParentCATRoutingID) > 0)
                        itemRouting = lstRoutingKPI.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.ParentCATRoutingID);

                    if (itemRouting != null)
                    {
                        // Danh sách các KPI cần tính
                        var lstKPIGroup = lstKPI.Where(c => c.ContractID == itemGroupProduct.ContractID && c.ContractTermID == itemRouting.ContractTermID).OrderBy(c => c.Level).ToList();
                        // Chạy theo level từ thấp => cao
                        foreach (var itemKPIGroup in lstKPIGroup)
                        {
                            if ((!string.IsNullOrEmpty(itemKPIGroup.ExpressionTon) && !string.IsNullOrEmpty(itemKPIGroup.CompareFieldTon)) || (!string.IsNullOrEmpty(itemKPIGroup.ExpressionCBM) && !string.IsNullOrEmpty(itemKPIGroup.CompareFieldCBM)) || (!string.IsNullOrEmpty(itemKPIGroup.ExpressionQuantity) && !string.IsNullOrEmpty(itemKPIGroup.CompareFieldQuantity)))
                            {
                                OfficeOpenXml.ExcelPackage kpiPackage = default(OfficeOpenXml.ExcelPackage);
                                
                                if (kpiPackage != null)
                                {
                                    var obj = model.KPI_QuantityDate.FirstOrDefault(c => c.DITOGroupProductID == itemGroupProduct.ID && c.TypeOfKPIID == itemKPIGroup.TypeOfKPIID);
                                    if (obj == null)
                                    {
                                        obj = new KPI_QuantityDate();
                                        obj.TypeOfKPIID = itemKPIGroup.TypeOfKPIID;
                                        obj.OrderID = itemGroupProduct.OrderID;
                                        obj.OrderGroupProductID = itemGroupProduct.OrderGroupProductID;
                                        obj.DITOGroupProductID = itemGroupProduct.ID;
                                        obj.CreatedBy = account.UserName;
                                        obj.CreatedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        obj.ModifiedBy = account.UserName;
                                        obj.ModifiedDate = DateTime.Now;
                                    }
                                    obj.DITOMasterID = itemGroupProduct.DITOMasterID;
                                    obj.ContractRoutingID = itemRouting.ContractRoutingID;
                                    obj.DateData = itemGroupProduct.DateConfig;
                                    obj.DateOrderRequest = itemGroupProduct.RequestDate;
                                    
                                    obj.DateTOMasterETD = itemGroupProduct.DateTOMasterETD;
                                    obj.DateTOMasterETA = itemGroupProduct.DateTOMasterETA;
                                    obj.DateTOMasterATD = itemGroupProduct.DateTOMasterATD;
                                    obj.DateTOMasterATA = itemGroupProduct.DateTOMasterATA;

                                    obj.TonOrder = itemGroupProduct.TonOrder;
                                    obj.CBMOrder = itemGroupProduct.CBMOrder;
                                    obj.QuantityOrder = itemGroupProduct.QuantityOrder;

                                    obj.TonTranfer = itemGroupProduct.TonTranfer;
                                    obj.CBMTranfer = itemGroupProduct.CBMTranfer;
                                    obj.QuantityTranfer = itemGroupProduct.QuantityTranfer;

                                    obj.TonReceive = itemGroupProduct.TonReceive;
                                    obj.CBMReceive = itemGroupProduct.CBMReceive;
                                    obj.QuantityReceive = itemGroupProduct.QuantityReceive;

                                    obj.TonReturn = itemGroupProduct.TonReturn;
                                    obj.CBMReturn = itemGroupProduct.CBMReturn;
                                    obj.QuantityReturn = itemGroupProduct.QuantityReturn;

                                    obj.Note = string.Empty;
                                    obj.IsKPI = null;
                                    try
                                    {
                                        var lstKPIRefer = model.KPI_QuantityDate.Where(c => c.KPI_TypeOfKPI.Level < itemKPIGroup.Level && c.DITOGroupProductID == obj.DITOGroupProductID && c.KPI_TypeOfKPI.KPITypeOfKPIID == itemKPIGroup.KPITypeOfKPIID).Select(c => new KPIQuantityDate
                                        {
                                            KPITon = c.KPITon,
                                            KPICBM = c.KPICBM,
                                            KPIQuantity = c.KPIQuantity,
                                            IsKPI = c.IsKPI
                                        }).ToList();
                                        var itemKPI = new KPIQuantityDate();
                                        itemKPI.ExpressionTon = itemKPIGroup.ExpressionTon;
                                        itemKPI.ExpressionCBM = itemKPIGroup.ExpressionCBM;
                                        itemKPI.ExpressionQuantity = itemKPIGroup.ExpressionQuantity;
                                        itemKPI.CompareFieldTon = itemKPIGroup.CompareFieldTon;
                                        itemKPI.CompareFieldCBM = itemKPIGroup.CompareFieldCBM;
                                        itemKPI.CompareFieldQuantity = itemKPIGroup.CompareFieldQuantity;
                                        KPIQuantity_GetQuantity(KPIQuantity_GetPackage(itemKPI, lstKPIRefer), itemKPIGroup.TypeOfKPICode, obj, lstKPIRefer);
                                    }
                                    catch (Exception ex)
                                    {
                                        obj.Note = ex.Message;
                                    }

                                    if (obj.ID < 1)
                                        model.KPI_QuantityDate.Add(obj);
                                }
                            }
                        }
                    }
                }
                #endregion

                model.SaveChanges();
            }
        }


        #endregion

        #region KPITimeNew
        public static DateTime? KPITime_CheckDate(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                DateTime? result = null;
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(item.Expression);
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (item.KPICode != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest;
                strExp.Replace("[DateRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value.Date;
                strExp.Replace("[DateRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateRequest.Value.Hour + "," + item.DateRequest.Value.Minute + "," + item.DateRequest.Value.Second + ")";
                strExp.Replace("[DateRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN;
                strExp.Replace("[DateDN]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value.Date;
                strExp.Replace("[DateDN.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateDN.Value.Hour + "," + item.DateDN.Value.Minute + "," + item.DateDN.Value.Second + ")";
                strExp.Replace("[DateDN.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome;
                strExp.Replace("[DateFromCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value.Date;
                strExp.Replace("[DateFromCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromCome.Value.Hour + "," + item.DateFromCome.Value.Minute + "," + item.DateFromCome.Value.Second + ")";
                strExp.Replace("[DateFromCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave;
                strExp.Replace("[DateFromLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value.Date;
                strExp.Replace("[DateFromLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLeave.Value.Hour + "," + item.DateFromLeave.Value.Minute + "," + item.DateFromLeave.Value.Second + ")";
                strExp.Replace("[DateFromLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                strExp.Replace("[DateFromLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value.Date;
                strExp.Replace("[DateFromLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadStart.Value.Hour + "," + item.DateFromLoadStart.Value.Minute + "," + item.DateFromLoadStart.Value.Second + ")";
                strExp.Replace("[DateFromLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                strExp.Replace("[DateFromLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value.Date;
                strExp.Replace("[DateFromLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadEnd.Value.Hour + "," + item.DateFromLoadEnd.Value.Minute + "," + item.DateFromLoadEnd.Value.Second + ")";
                strExp.Replace("[DateFromLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome;
                strExp.Replace("[DateToCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value.Date;
                strExp.Replace("[DateToCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToCome.Value.Hour + "," + item.DateToCome.Value.Minute + "," + item.DateToCome.Value.Second + ")";
                strExp.Replace("[DateToCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave;
                strExp.Replace("[DateToLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value.Date;
                strExp.Replace("[DateToLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLeave.Value.Hour + "," + item.DateToLeave.Value.Minute + "," + item.DateToLeave.Value.Second + ")";
                strExp.Replace("[DateToLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart;
                strExp.Replace("[DateToLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value.Date;
                strExp.Replace("[DateToLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadStart.Value.Hour + "," + item.DateToLoadStart.Value.Minute + "," + item.DateToLoadStart.Value.Second + ")";
                strExp.Replace("[DateToLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                strExp.Replace("[DateToLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value.Date;
                strExp.Replace("[DateToLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadEnd.Value.Hour + "," + item.DateToLoadEnd.Value.Minute + "," + item.DateToLoadEnd.Value.Second + ")";
                strExp.Replace("[DateToLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice;
                strExp.Replace("[DateInvoice]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value.Date;
                strExp.Replace("[DateInvoice.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateInvoice.Value.Hour + "," + item.DateInvoice.Value.Minute + "," + item.DateInvoice.Value.Second + ")";
                strExp.Replace("[DateInvoice.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest;
                strExp.Replace("[ETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                strExp.Replace("[ETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                strExp.Replace("[ETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                strExp.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                strExp.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                strExp.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                strExp.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                strExp.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                strExp.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                strExp.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                strExp.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                strExp.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                strExp.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                strExp.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                strExp.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD;
                strExp.Replace("[DateOrderETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value.Date;
                strExp.Replace("[DateOrderETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETD.Value.Hour + "," + item.DateOrderETD.Value.Minute + "," + item.DateOrderETD.Value.Second + ")";
                strExp.Replace("[DateOrderETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA;
                strExp.Replace("[DateOrderETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value.Date;
                strExp.Replace("[DateOrderETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETA.Value.Hour + "," + item.DateOrderETA.Value.Minute + "," + item.DateOrderETA.Value.Second + ")";
                strExp.Replace("[DateOrderETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest;
                strExp.Replace("[DateOrderETDRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value.Date;
                strExp.Replace("[DateOrderETADateOrderETDRequestDate]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETDRequest.Value.Hour + "," + item.DateOrderETDRequest.Value.Minute + "," + item.DateOrderETDRequest.Value.Second + ")";
                strExp.Replace("[DateOrderETDRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest;
                strExp.Replace("[DateOrderETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                strExp.Replace("[DateOrderETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                strExp.Replace("[DateOrderETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime;
                strExp.Replace("[DateOrderCutOfTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value.Date;
                strExp.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderCutOfTime.Value.Hour + "," + item.DateOrderCutOfTime.Value.Minute + "," + item.DateOrderCutOfTime.Value.Second + ")";
                strExp.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Zone;
                strExp.Replace("[Zone]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[Zone]", strCol + row);
                if (item.Zone % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }
                row++;

                worksheet.Cells[row, col].Value = item.LeadTime;
                strExp.Replace("[LeadTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[LeadTime]", strCol + row);
                if (item.LeadTime % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }

                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExp.Replace("[" + valEx.Key + "]", strCol + row);
                }

                row++;
                worksheet.Cells[row, col].Formula = strExp.ToString();
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

                if (result != null)
                {
                    if (result.Value.Year == 1900)
                        result = null;
                }
                //package.Save();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool? KPITime_CheckBool(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                bool? result = null;

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(item.Expression);
                StringBuilder strField = new StringBuilder(item.CompareField);
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (item.KPICode != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest;
                strExp.Replace("[DateRequest]", strCol + row);
                strField.Replace("[DateRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Value = item.DateRequest.Value.Date;
                strExp.Replace("[DateRequest.Date]", strCol + row);
                strField.Replace("[DateRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateRequest.Value.Hour + "," + item.DateRequest.Value.Minute + "," + item.DateRequest.Value.Second + ")";
                strExp.Replace("[DateRequest.Time]", strCol + row);
                strField.Replace("[DateRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN;
                strExp.Replace("[DateDN]", strCol + row);
                strField.Replace("[DateDN]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value.Date;
                strExp.Replace("[DateDN.Date]", strCol + row);
                strField.Replace("[DateDN.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateDN.Value.Hour + "," + item.DateDN.Value.Minute + "," + item.DateDN.Value.Second + ")";
                strExp.Replace("[DateDN.Time]", strCol + row);
                strField.Replace("[DateDN.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome;
                strExp.Replace("[DateFromCome]", strCol + row);
                strField.Replace("[DateFromCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value.Date;
                strExp.Replace("[DateFromCome.Date]", strCol + row);
                strField.Replace("[DateFromCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromCome.Value.Hour + "," + item.DateFromCome.Value.Minute + "," + item.DateFromCome.Value.Second + ")";
                strExp.Replace("[DateFromCome.Time]", strCol + row);
                strField.Replace("[DateFromCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave;
                strExp.Replace("[DateFromLeave]", strCol + row);
                strField.Replace("[DateFromLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value.Date;
                strExp.Replace("[DateFromLeave.Date]", strCol + row);
                strField.Replace("[DateFromLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLeave.Value.Hour + "," + item.DateFromLeave.Value.Minute + "," + item.DateFromLeave.Value.Second + ")";
                strExp.Replace("[DateFromLeave.Time]", strCol + row);
                strField.Replace("[DateFromLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                strExp.Replace("[DateFromLoadStart]", strCol + row);
                strField.Replace("[DateFromLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value.Date;
                strExp.Replace("[DateFromLoadStart.Date]", strCol + row);
                strField.Replace("[DateFromLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadStart.Value.Hour + "," + item.DateFromLoadStart.Value.Minute + "," + item.DateFromLoadStart.Value.Second + ")";
                strExp.Replace("[DateFromLoadStart.Time]", strCol + row);
                strField.Replace("[DateFromLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                strExp.Replace("[DateFromLoadEnd]", strCol + row);
                strField.Replace("[DateFromLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value.Date;
                strExp.Replace("[DateFromLoadEnd.Date]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadEnd.Value.Hour + "," + item.DateFromLoadEnd.Value.Minute + "," + item.DateFromLoadEnd.Value.Second + ")";
                strExp.Replace("[DateFromLoadEnd.Time]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome;
                strExp.Replace("[DateToCome]", strCol + row);
                strField.Replace("[DateToCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value.Date;
                strExp.Replace("[DateToCome.Date]", strCol + row);
                strField.Replace("[DateToCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToCome.Value.Hour + "," + item.DateToCome.Value.Minute + "," + item.DateToCome.Value.Second + ")";
                strExp.Replace("[DateToCome.Time]", strCol + row);
                strField.Replace("[DateToCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave;
                strExp.Replace("[DateToLeave]", strCol + row);
                strField.Replace("[DateToLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value.Date;
                strExp.Replace("[DateToLeave.Date]", strCol + row);
                strField.Replace("[DateToLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLeave.Value.Hour + "," + item.DateToLeave.Value.Minute + "," + item.DateToLeave.Value.Second + ")";
                strExp.Replace("[DateToLeave.Time]", strCol + row);
                strField.Replace("[DateToLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart;
                strExp.Replace("[DateToLoadStart]", strCol + row);
                strField.Replace("[DateToLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value.Date;
                strExp.Replace("[DateToLoadStart.Date]", strCol + row);
                strField.Replace("[DateToLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadStart.Value.Hour + "," + item.DateToLoadStart.Value.Minute + "," + item.DateToLoadStart.Value.Second + ")";
                strExp.Replace("[DateToLoadStart.Time]", strCol + row);
                strField.Replace("[DateToLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                strExp.Replace("[DateToLoadEnd]", strCol + row);
                strField.Replace("[DateToLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value.Date;
                strExp.Replace("[DateToLoadEnd.Date]", strCol + row);
                strField.Replace("[DateToLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadEnd.Value.Hour + "," + item.DateToLoadEnd.Value.Minute + "," + item.DateToLoadEnd.Value.Second + ")";
                strExp.Replace("[DateToLoadEnd.Time]", strCol + row);
                strField.Replace("[DateToLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice;
                strExp.Replace("[DateInvoice]", strCol + row);
                strField.Replace("[DateInvoice]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value.Date;
                strExp.Replace("[DateInvoice.Date]", strCol + row);
                strField.Replace("[DateInvoice.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateInvoice.Value.Hour + "," + item.DateInvoice.Value.Minute + "," + item.DateInvoice.Value.Second + ")";
                strExp.Replace("[DateInvoice.Time]", strCol + row);
                strField.Replace("[DateInvoice.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest;
                strExp.Replace("[ETARequest]", strCol + row);
                strField.Replace("[ETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Value = item.ETARequest.Value.Date;
                strExp.Replace("[ETARequest.Date]", strCol + row);
                strField.Replace("[ETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.ETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.ETARequest.Value.Hour + "," + item.ETARequest.Value.Minute + "," + item.ETARequest.Value.Second + ")";
                strExp.Replace("[ETARequest.Time]", strCol + row);
                strField.Replace("[ETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                strExp.Replace("[DateTOMasterETD]", strCol + row);
                strField.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                strExp.Replace("[DateTOMasterETD.Date]", strCol + row);
                strField.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                strExp.Replace("[DateTOMasterETD.Time]", strCol + row);
                strField.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                strExp.Replace("[DateTOMasterETA]", strCol + row);
                strField.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                strExp.Replace("[DateTOMasterETA.Date]", strCol + row);
                strField.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                strExp.Replace("[DateTOMasterETA.Time]", strCol + row);
                strField.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                strExp.Replace("[DateTOMasterATD]", strCol + row);
                strField.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                strExp.Replace("[DateTOMasterATD.Date]", strCol + row);
                strField.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                strExp.Replace("[DateTOMasterATD.Time]", strCol + row);
                strField.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                strExp.Replace("[DateTOMasterATA]", strCol + row);
                strField.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                strExp.Replace("[DateTOMasterATA.Date]", strCol + row);
                strField.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                strExp.Replace("[DateTOMasterATA.Time]", strCol + row);
                strField.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD;
                strExp.Replace("[DateOrderETD]", strCol + row);
                strField.Replace("[DateOrderETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value.Date;
                strExp.Replace("[DateOrderETD.Date]", strCol + row);
                strField.Replace("[DateOrderETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETD.Value.Hour + "," + item.DateOrderETD.Value.Minute + "," + item.DateOrderETD.Value.Second + ")";
                strExp.Replace("[DateOrderETD.Time]", strCol + row);
                strField.Replace("[DateOrderETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA;
                strExp.Replace("[DateOrderETA]", strCol + row);
                strField.Replace("[DateOrderETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value.Date;
                strExp.Replace("[DateOrderETA.Date]", strCol + row);
                strField.Replace("[DateOrderETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETA.Value.Hour + "," + item.DateOrderETA.Value.Minute + "," + item.DateOrderETA.Value.Second + ")";
                strExp.Replace("[DateOrderETA.Time]", strCol + row);
                strField.Replace("[DateOrderETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest;
                strExp.Replace("[DateOrderETDRequest]", strCol + row);
                strField.Replace("[DateOrderETDRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value.Date;
                strExp.Replace("[DateOrderETDRequest.Date]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETDRequest.Value.Hour + "," + item.DateOrderETDRequest.Value.Minute + "," + item.DateOrderETDRequest.Value.Second + ")";
                strExp.Replace("[DateOrderETDRequest.Time]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest;
                strExp.Replace("[DateOrderETARequest]", strCol + row);
                strField.Replace("[DateOrderETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                strExp.Replace("[DateOrderETARequest.Date]", strCol + row);
                strField.Replace("[DateOrderETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                strExp.Replace("[DateOrderETARequest.Time]", strCol + row);
                strField.Replace("[DateOrderETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime;
                strExp.Replace("[DateOrderCutOfTime]", strCol + row);
                strField.Replace("[DateOrderCutOfTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value.Date;
                strExp.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderCutOfTime.Value.Hour + "," + item.DateOrderCutOfTime.Value.Minute + "," + item.DateOrderCutOfTime.Value.Second + ")";
                strExp.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Zone;
                strExp.Replace("[Zone]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[Zone]", strCol + row);
                if (item.Zone % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }
                row++;

                worksheet.Cells[row, col].Value = item.LeadTime;
                strExp.Replace("[LeadTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[LeadTime]", strCol + row);
                if (item.LeadTime % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.01,");
                }

                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExp.Replace("[" + valEx.Key + "]", strCol + row);
                    strField.Replace("[" + valEx.Key + "]", strCol + row);
                }

                row++;
                worksheet.Cells[row, col].Formula = strExp.ToString();
                row++;
                worksheet.Cells[row, col].Formula = strField.ToString();
                row++;
                // IF(OR(A20="",A39=""),"N",IF(AND(A20<>"",A39<>"",A20<=A39),"T","F"))
                worksheet.Cells[row, col].Formula = "IF(OR(" + strField.ToString() + "=\"\"," + worksheet.Cells[row - 2, col].Address + "=\"\"),\"N\",IF(AND(" + strField.ToString() + "<>\"\"," + worksheet.Cells[row - 2, col].Address + "<>\"\"," + strField.ToString() + "<=" + worksheet.Cells[row - 2, col].Address + ")" + ",\"T\",\"F\"))";

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                if (val == "T") result = true;
                else if (val == "F") result = false;
                else if (val == "N") result = null;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ExcelPackage KPITime_GetPackage(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                //string file = "/MailTemplate/" + "KPI_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                //    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                //FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                //ExcelPackage result = new ExcelPackage(exportfile);

                ExcelPackage result = new ExcelPackage();
                ExcelWorksheet worksheet = result.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(item.Expression);
                StringBuilder strField = new StringBuilder(item.CompareField);

                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (!string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                strExp.Replace("[DateRequest]", strCol + row);
                strField.Replace("[DateRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateRequest.Date]", strCol + row);
                strField.Replace("[DateRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateRequest.Time]", strCol + row);
                strField.Replace("[DateRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateRequest.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateDN]", strCol + row);
                strField.Replace("[DateDN]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateDN.Date]", strCol + row);
                strField.Replace("[DateDN.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateDN.Time]", strCol + row);
                strField.Replace("[DateDN.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateDN.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromCome]", strCol + row);
                strField.Replace("[DateFromCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromCome.Date]", strCol + row);
                strField.Replace("[DateFromCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromCome.Time]", strCol + row);
                strField.Replace("[DateFromCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLeave]", strCol + row);
                strField.Replace("[DateFromLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLeave.Date]", strCol + row);
                strField.Replace("[DateFromLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLeave.Time]", strCol + row);
                strField.Replace("[DateFromLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadStart]", strCol + row);
                strField.Replace("[DateFromLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadStart.Date]", strCol + row);
                strField.Replace("[DateFromLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadStart.Time]", strCol + row);
                strField.Replace("[DateFromLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadEnd]", strCol + row);
                strField.Replace("[DateFromLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadEnd.Date]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateFromLoadEnd.Time]", strCol + row);
                strField.Replace("[DateFromLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateFromLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToCome]", strCol + row);
                strField.Replace("[DateToCome]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToCome.Date]", strCol + row);
                strField.Replace("[DateToCome.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToCome.Time]", strCol + row);
                strField.Replace("[DateToCome.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToCome.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLeave]", strCol + row);
                strField.Replace("[DateToLeave]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLeave.Date]", strCol + row);
                strField.Replace("[DateToLeave.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLeave.Time]", strCol + row);
                strField.Replace("[DateToLeave.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLeave.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadStart]", strCol + row);
                strField.Replace("[DateToLoadStart]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadStart.Date]", strCol + row);
                strField.Replace("[DateToLoadStart.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadStart.Time]", strCol + row);
                strField.Replace("[DateToLoadStart.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadStart.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadEnd]", strCol + row);
                strField.Replace("[DateToLoadEnd]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadEnd.Date]", strCol + row);
                strField.Replace("[DateToLoadEnd.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateToLoadEnd.Time]", strCol + row);
                strField.Replace("[DateToLoadEnd.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateToLoadEnd.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateInvoice]", strCol + row);
                strField.Replace("[DateInvoice]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateInvoice.Date]", strCol + row);
                strField.Replace("[DateInvoice.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateInvoice.Time]", strCol + row);
                strField.Replace("[DateInvoice.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateInvoice.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ETARequest]", strCol + row);
                strField.Replace("[ETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ETARequest.Date]", strCol + row);
                strField.Replace("[ETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ETARequest.Time]", strCol + row);
                strField.Replace("[ETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[ETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETD]", strCol + row);
                strField.Replace("[DateTOMasterETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETD.Date]", strCol + row);
                strField.Replace("[DateTOMasterETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETD.Time]", strCol + row);
                strField.Replace("[DateTOMasterETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateTOMasterETA]", strCol + row);
                strField.Replace("[DateTOMasterETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETA.Date]", strCol + row);
                strField.Replace("[DateTOMasterETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterETA.Time]", strCol + row);
                strField.Replace("[DateTOMasterETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateTOMasterATD]", strCol + row);
                strField.Replace("[DateTOMasterATD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATD.Date]", strCol + row);
                strField.Replace("[DateTOMasterATD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATD.Time]", strCol + row);
                strField.Replace("[DateTOMasterATD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateTOMasterATA]", strCol + row);
                strField.Replace("[DateTOMasterATA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATA.Date]", strCol + row);
                strField.Replace("[DateTOMasterATA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateTOMasterATA.Time]", strCol + row);
                strField.Replace("[DateTOMasterATA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateTOMasterATA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETD]", strCol + row);
                strField.Replace("[DateOrderETD]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETD.Date]", strCol + row);
                strField.Replace("[DateOrderETD.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETD.Time]", strCol + row);
                strField.Replace("[DateOrderETD.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETD.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETA]", strCol + row);
                strField.Replace("[DateOrderETA]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETA.Date]", strCol + row);
                strField.Replace("[DateOrderETA.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETA.Time]", strCol + row);
                strField.Replace("[DateOrderETA.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETA.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETDRequest]", strCol + row);
                strField.Replace("[DateOrderETDRequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETDRequest.Date]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETDRequest.Time]", strCol + row);
                strField.Replace("[DateOrderETDRequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETDRequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderETARequest]", strCol + row);
                strField.Replace("[DateOrderETARequest]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETARequest.Date]", strCol + row);
                strField.Replace("[DateOrderETARequest.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderETARequest.Time]", strCol + row);
                strField.Replace("[DateOrderETARequest.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderETARequest.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[DateOrderCutOfTime]", strCol + row);
                strField.Replace("[DateOrderCutOfTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Date]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Date]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strField.Replace("[DateOrderCutOfTime.Time]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[DateOrderCutOfTime.Time]", strCol + row);
                strRow = strCol + row; row++;


                strExp.Replace("[Zone]", strCol + row);
                strField.Replace("[Zone]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[Zone]", strCol + row);
                if (item.Zone % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                }
                row++;

                strExp.Replace("[LeadTime]", strCol + row);
                strField.Replace("[LeadTime]", strCol + row);
                foreach (var valExKey in lstExKey)
                    dicEx[valExKey] = dicEx[valExKey].Replace("[LeadTime]", strCol + row);
                if (item.LeadTime % 1 == 0.5)
                {
                    strExp.Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                    foreach (var valExKey in lstExKey)
                        dicEx[valExKey] = dicEx[valExKey].Replace("ROUND(" + strCol + row + ",", "ROUND(" + strCol + row + "+0.1,");
                }

                foreach (var valEx in dicEx)
                {
                    row++;
                    worksheet.Cells[row, col].Formula = valEx.Value;
                    strExp.Replace("[" + valEx.Key + "]", strCol + row);
                    strField.Replace("[" + valEx.Key + "]", strCol + row);
                }

                row++;
                worksheet.Cells[row, col].Formula = strExp.ToString();
                row++;
                worksheet.Cells[row, col].Formula = strField.ToString();
                row++;
                worksheet.Cells[row, col].Formula = "IF(OR(" + strField.ToString() + "=\"\"," + worksheet.Cells[row - 2, col].Address + "=\"\"),\"N\",IF(AND(" + strField.ToString() + "<>\"\"," + worksheet.Cells[row - 2, col].Address + "<>\"\"," + strField.ToString() + "<=" + worksheet.Cells[row - 2, col].Address + ")" + ",\"T\",\"F\"))";

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KPITime_GetDate(ExcelPackage package, string code, KPI_TimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                Dictionary<string, string> dicEx = new Dictionary<string, string>();
                foreach (var itemKPI in lst)
                {
                    if (code != itemKPI.KPICode && !string.IsNullOrEmpty(itemKPI.Expression))
                        dicEx[itemKPI.KPICode] = itemKPI.Expression;
                }
                var lstExKey = dicEx.Keys.ToArray();

                row++;
                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Value = item.DateOrderRequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderRequest.Value.Hour + "," + item.DateOrderRequest.Value.Minute + "," + item.DateOrderRequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateDN != null) worksheet.Cells[row, col].Value = item.DateDN.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateDN != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateDN.Value.Hour + "," + item.DateDN.Value.Minute + "," + item.DateDN.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromCome != null) worksheet.Cells[row, col].Value = item.DateFromCome.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromCome.Value.Hour + "," + item.DateFromCome.Value.Minute + "," + item.DateFromCome.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLeave != null) worksheet.Cells[row, col].Value = item.DateFromLeave.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLeave.Value.Hour + "," + item.DateFromLeave.Value.Minute + "," + item.DateFromLeave.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Value = item.DateFromLoadStart.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadStart.Value.Hour + "," + item.DateFromLoadStart.Value.Minute + "," + item.DateFromLoadStart.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Value = item.DateFromLoadEnd.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateFromLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateFromLoadEnd.Value.Hour + "," + item.DateFromLoadEnd.Value.Minute + "," + item.DateFromLoadEnd.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToCome != null) worksheet.Cells[row, col].Value = item.DateToCome.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToCome != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToCome.Value.Hour + "," + item.DateToCome.Value.Minute + "," + item.DateToCome.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLeave != null) worksheet.Cells[row, col].Value = item.DateToLeave.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLeave != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLeave.Value.Hour + "," + item.DateToLeave.Value.Minute + "," + item.DateToLeave.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Value = item.DateToLoadStart.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadStart != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadStart.Value.Hour + "," + item.DateToLoadStart.Value.Minute + "," + item.DateToLoadStart.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Value = item.DateToLoadEnd.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateToLoadEnd != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateToLoadEnd.Value.Hour + "," + item.DateToLoadEnd.Value.Minute + "," + item.DateToLoadEnd.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateInvoice != null) worksheet.Cells[row, col].Value = item.DateInvoice.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateInvoice != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateInvoice.Value.Hour + "," + item.DateInvoice.Value.Minute + "," + item.DateInvoice.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Value = item.DateTOMasterETD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETD.Value.Hour + "," + item.DateTOMasterETD.Value.Minute + "," + item.DateTOMasterETD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Value = item.DateTOMasterETA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterETA.Value.Hour + "," + item.DateTOMasterETA.Value.Minute + "," + item.DateTOMasterETA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Value = item.DateTOMasterATD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATD.Value.Hour + "," + item.DateTOMasterATD.Value.Minute + "," + item.DateTOMasterATD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Value = item.DateTOMasterATA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateTOMasterATA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateTOMasterATA.Value.Hour + "," + item.DateTOMasterATA.Value.Minute + "," + item.DateTOMasterATA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETD != null) worksheet.Cells[row, col].Value = item.DateOrderETD.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETD != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETD.Value.Hour + "," + item.DateOrderETD.Value.Minute + "," + item.DateOrderETD.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETA != null) worksheet.Cells[row, col].Value = item.DateOrderETA.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETA != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETA.Value.Hour + "," + item.DateOrderETA.Value.Minute + "," + item.DateOrderETA.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Value = item.DateOrderETDRequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETDRequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETDRequest.Value.Hour + "," + item.DateOrderETDRequest.Value.Minute + "," + item.DateOrderETDRequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Value = item.DateOrderETARequest.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderETARequest != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderETARequest.Value.Hour + "," + item.DateOrderETARequest.Value.Minute + "," + item.DateOrderETARequest.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;

                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Value = item.DateOrderCutOfTime.Value.Date;
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;
                if (item.DateOrderCutOfTime != null) worksheet.Cells[row, col].Formula = "TIME(" + item.DateOrderCutOfTime.Value.Hour + "," + item.DateOrderCutOfTime.Value.Minute + "," + item.DateOrderCutOfTime.Value.Second + ")";
                else worksheet.Cells[row, col].Value = null;
                strRow = strCol + row; row++;


                worksheet.Cells[row, col].Value = item.Zone;
                if (item.Zone % 1 == 0.5)
                    worksheet.Cells[row, col].Value = item.Zone + 0.01;
                row++;
                worksheet.Cells[row, col].Value = item.LeadTime;
                if (item.LeadTime % 1 == 0.5)
                    worksheet.Cells[row, col].Value = item.LeadTime + 0.01;

                foreach (var valEx in dicEx)
                    row++;

                package.Workbook.Calculate();
                row++;
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                item.KPIDate = null;
                item.IsKPI = null;
                try
                { item.KPIDate = DateTime.FromOADate(Convert.ToDouble(val)); }
                catch { }
                if (item.KPIDate == null)
                {
                    try
                    { item.KPIDate = Convert.ToDateTime(val, new CultureInfo("en-US")).Date; }
                    catch { }
                }
                //if (item.DateRequest != null && item.KPIDate != null)
                //    if (item.DateRequest.Value.AddDays(-2).CompareTo(item.KPIDate.Value) > 0)
                //        item.KPIDate = null;

                row++;
                row++;
                if (item.KPIDate != null)
                {
                    if (item.KPIDate.Value.Year == 1900)
                        item.KPIDate = null;
                    else
                    {
                        val = worksheet.Cells[row, col].Value.ToString().Trim();
                        if (val == "T") item.IsKPI = true;
                        else if (val == "F") item.IsKPI = false;
                        else if (val == "N") item.IsKPI = null;
                    }
                }
                //if (item.OrderID == 6933)
                //    package.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KPITime_Generate(DataEntities model, AccountItem account, int? ditomasterID, List<int> lstOrderID, List<int> lstDITOGroupProductID, bool IsCustomer)
        {
            #region Data
            DateTime? dtNull = null;
            var lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOMasterID == ditomasterID &&c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID &&
                    (IsCustomer ? c.ORD_GroupProduct.CUSRoutingID > 0 && c.ORD_GroupProduct.ORD_Order.ContractID > 0 : c.CATRoutingID > 0 && c.OPS_DITOMaster.ContractID > 0)).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        c.DITOMasterID,

                        ContractID = IsCustomer ? c.ORD_GroupProduct.ORD_Order.ContractID.Value : c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.RoutingID : c.CATRoutingID.Value,
                        ParentCATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID : c.CAT_Routing.ParentID,

                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,

                        DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                        OPSDateConfig = c.DateConfig.HasValue ? c.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                    }).ToList();
            if (lstOrderID != null && lstOrderID.Count > 0)
            {
                lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && lstOrderID.Contains(c.ORD_GroupProduct.OrderID) && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID &&
                    (IsCustomer ? c.ORD_GroupProduct.CUSRoutingID > 0 && c.ORD_GroupProduct.ORD_Order.ContractID > 0 : c.CATRoutingID > 0 && c.DITOMasterID > 0 && c.OPS_DITOMaster.ContractID > 0)).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        c.DITOMasterID,

                        ContractID = IsCustomer ? c.ORD_GroupProduct.ORD_Order.ContractID.Value : c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.RoutingID : c.CATRoutingID.Value,
                        ParentCATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID : c.CAT_Routing.ParentID,

                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,

                        DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                        OPSDateConfig = c.DateConfig.HasValue ? c.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                    }).ToList();
            }
            if (lstDITOGroupProductID != null && lstDITOGroupProductID.Count > 0)
            {
                lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 &&
                    lstDITOGroupProductID.Contains(c.ID) &&
                    c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == account.SYSCustomerID).Select(c => new
                    {
                        c.ID,
                        c.ORD_GroupProduct.ORD_Order.CustomerID,
                        c.ORD_GroupProduct.OrderID,
                        c.OrderGroupProductID,
                        c.DITOMasterID,

                        ContractID = IsCustomer ? c.ORD_GroupProduct.ORD_Order.ContractID.Value : c.OPS_DITOMaster.ContractID.Value,
                        CATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.RoutingID : c.CATRoutingID.Value,
                        ParentCATRoutingID = IsCustomer ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID : c.CAT_Routing.ParentID,

                        c.DITOGroupProductStatusPODID,

                        c.DateDN,
                        c.ORD_GroupProduct.ORD_Order.RequestDate,
                        c.DateFromCome,
                        c.DateFromLeave,
                        c.DateFromLoadStart,
                        c.DateFromLoadEnd,
                        c.DateToCome,
                        c.DateToLeave,
                        c.DateToLoadStart,
                        c.DateToLoadEnd,
                        c.InvoiceDate,
                        c.ORD_GroupProduct.ETARequest,

                        DateTOMasterETD = c.OPS_DITOMaster.ETD,
                        DateTOMasterETA = c.OPS_DITOMaster.ETA,
                        DateTOMasterATD = c.OPS_DITOMaster.ATD,
                        DateTOMasterATA = c.OPS_DITOMaster.ATA,

                        DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                        DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                        DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                        DateOrderCutOfTime = dtNull,

                        DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                        OPSDateConfig = c.DateConfig.HasValue ? c.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.RequestDate,
                    }).ToList();
            }
            #endregion

            if (lstDITOGroupProduct.Count > 0)
            {
                #region Thiết lập KPI
                var lstContractID = lstDITOGroupProduct.Select(c => c.ContractID).Distinct().ToList();

                var lstKPI = model.CAT_ContractTermKPITimeDate.Where(c => lstContractID.Contains(c.CAT_ContractTerm.ContractID)).Select(c => new
                {
                    CustomerID = c.CAT_ContractTerm.CAT_Contract.CustomerID,
                    ContractID = c.CAT_ContractTerm.ContractID,
                    ContractTermID = c.ContractTermID,
                    DateEffect = c.CAT_ContractTerm.DateEffect,
                    DateExpire = c.CAT_ContractTerm.DateExpire,
                    Expression = c.Expression,
                    CompareField = c.CompareField,
                    TypeOfKPIID = c.TypeOfKPIID,
                    TypeOfKPICode = c.KPI_TypeOfKPI.Code,
                    Level = c.KPI_TypeOfKPI.Level
                }).OrderBy(c => c.Level).ToList();
                var lstRoutingPrice = model.CAT_ContractRouting.Where(c => lstContractID.Contains(c.ContractID) && c.ContractTermID > 0 && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice).Select(c => new
                {
                    ContractID = c.ContractID,
                    ContractRoutingID = c.ID,
                    CATRoutingID = c.RoutingID,
                    c.Zone,
                    c.LeadTime,
                    c.ContractRoutingTypeID,
                    ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1,
                }).ToList();
                var lstRoutingKPI = model.CAT_ContractRouting.Where(c => lstContractID.Contains(c.ContractID) && c.ContractTermID > 0 && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypeKPI).Select(c => new
                {
                    ContractID = c.ContractID,
                    ContractRoutingID = c.ID,
                    CATRoutingID = c.RoutingID,
                    c.Zone,
                    c.LeadTime,
                    c.ContractRoutingTypeID,
                    ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1,
                }).ToList();

                //Danh sach cong thuc
                Dictionary<string, OfficeOpenXml.ExcelPackage> dicKPIPackage = new Dictionary<string, OfficeOpenXml.ExcelPackage>();
                foreach (var itemKPI in lstKPI.Distinct())
                {
                    if (!string.IsNullOrEmpty(itemKPI.Expression) && !string.IsNullOrEmpty(itemKPI.CompareField))
                    {
                        // Lấy các KPI liên quan (có Level < Level của KPI hiện tại)
                        var lst = lstKPI.Where(c => c.ContractID == itemKPI.ContractID && c.ContractTermID == itemKPI.ContractTermID && c.Level < itemKPI.Level).Select(c => new KPITimeDate
                            {
                                KPICode = c.TypeOfKPICode,
                                Expression = c.Expression,
                            }).ToList();
                        // Lấy các cung đường liên quan tới phụ lục
                        var lstRoutingPackage = lstRoutingPrice.Where(c => c.ContractTermID == itemKPI.ContractTermID).ToList();
                        lstRoutingPackage.AddRange(lstRoutingKPI.Where(c => c.ContractTermID == itemKPI.ContractTermID).ToList());
                        // Tạo excel package cho từng cung đường
                        foreach (var itemRoutingPackage in lstRoutingPackage)
                        {
                            var objKPI = new KPITimeDate
                            {
                                Expression = itemKPI.Expression,
                                CompareField = itemKPI.CompareField,
                                Zone = itemRoutingPackage.Zone > 0 ? itemRoutingPackage.Zone.Value : 0,
                                LeadTime = itemRoutingPackage.LeadTime > 0 ? itemRoutingPackage.LeadTime.Value : 0,
                            };
                            dicKPIPackage.Add(itemKPI.ContractID + "_" + itemKPI.ContractTermID + "_" + itemRoutingPackage.CATRoutingID + "_" + itemKPI.TypeOfKPICode, KPITime_GetPackage(objKPI, lst));
                        }
                    }
                }
                #endregion

                #region Tính KPI
                foreach (var itemGroupProduct in lstDITOGroupProduct)
                {
                    var itemRouting = lstRoutingPrice.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID);
                    // Xét xem có thiết lập routing cho KPI riêng hay ko
                    if (lstRoutingKPI.Count > 0 && lstRoutingKPI.Count(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.ParentCATRoutingID) > 0)
                        itemRouting = lstRoutingKPI.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.ParentCATRoutingID);

                    if (itemRouting != null)
                    {
                        // Danh sách các KPI cần tính
                        var lstKPIGroup = lstKPI.Where(c => c.ContractID == itemGroupProduct.ContractID && c.ContractTermID == itemRouting.ContractTermID).OrderBy(c => c.Level).ToList();
                        // Chạy theo level từ thấp => cao
                        foreach (var itemKPIGroup in lstKPIGroup)
                        {
                            if (!string.IsNullOrEmpty(itemKPIGroup.Expression) && !string.IsNullOrEmpty(itemKPIGroup.CompareField))
                            {
                                OfficeOpenXml.ExcelPackage kpiPackage = default(OfficeOpenXml.ExcelPackage);
                                if (dicKPIPackage.ContainsKey(itemGroupProduct.ContractID + "_" + itemRouting.ContractTermID + "_" + itemRouting.CATRoutingID + "_" + itemKPIGroup.TypeOfKPICode))
                                    kpiPackage = dicKPIPackage[itemGroupProduct.ContractID + "_" + itemRouting.ContractTermID + "_" + itemRouting.CATRoutingID + "_" + itemKPIGroup.TypeOfKPICode];

                                if (kpiPackage != null)
                                {
                                    var obj = model.KPI_TimeDate.FirstOrDefault(c => c.DITOGroupProductID == itemGroupProduct.ID && c.TypeOfKPIID == itemKPIGroup.TypeOfKPIID);
                                    if (obj == null)
                                    {
                                        obj = new KPI_TimeDate();
                                        obj.TypeOfKPIID = itemKPIGroup.TypeOfKPIID;
                                        obj.OrderID = itemGroupProduct.OrderID;
                                        obj.OrderGroupProductID = itemGroupProduct.OrderGroupProductID;
                                        obj.DITOGroupProductID = itemGroupProduct.ID;
                                        obj.CreatedBy = account.UserName;
                                        obj.CreatedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        obj.ModifiedBy = account.UserName;
                                        obj.ModifiedDate = DateTime.Now;
                                    }
                                    obj.DITOMasterID = itemGroupProduct.DITOMasterID;
                                    obj.ContractRoutingID = itemRouting.ContractRoutingID;
                                    obj.DateData = itemGroupProduct.DateConfig;
                                    obj.DateOrderRequest = itemGroupProduct.RequestDate;
                                    obj.DateDN = itemGroupProduct.DateDN;
                                    if (obj.DateDN == null)
                                        obj.DateDN = itemGroupProduct.RequestDate;
                                    obj.DateFromCome = itemGroupProduct.DateFromCome;
                                    obj.DateFromLeave = itemGroupProduct.DateFromLeave;
                                    obj.DateFromLoadStart = itemGroupProduct.DateFromLoadStart;
                                    obj.DateFromLoadEnd = itemGroupProduct.DateFromLoadEnd;
                                    obj.DateToCome = itemGroupProduct.DateToCome;
                                    obj.DateToLeave = itemGroupProduct.DateToLeave;
                                    obj.DateToLoadStart = itemGroupProduct.DateToLoadStart;
                                    obj.DateToLoadEnd = itemGroupProduct.DateToLoadEnd;
                                    obj.DateInvoice = itemGroupProduct.InvoiceDate;

                                    obj.DateTOMasterETD = itemGroupProduct.DateTOMasterETD;
                                    obj.DateTOMasterETA = itemGroupProduct.DateTOMasterETA;
                                    obj.DateTOMasterATD = itemGroupProduct.DateTOMasterATD;
                                    obj.DateTOMasterATA = itemGroupProduct.DateTOMasterATA;
                                    obj.DateOrderETD = itemGroupProduct.DateOrderETD;
                                    obj.DateOrderETA = itemGroupProduct.DateOrderETA;
                                    obj.DateOrderETDRequest = itemGroupProduct.DateOrderETDRequest;
                                    obj.DateOrderETARequest = itemGroupProduct.DateOrderETARequest;
                                    obj.DateOrderCutOfTime = itemGroupProduct.DateOrderCutOfTime;

                                    obj.Zone = itemRouting.Zone;
                                    if (obj.Zone == null) obj.Zone = 0;
                                    obj.LeadTime = itemRouting.LeadTime;
                                    if (obj.LeadTime == null) obj.LeadTime = 0;
                                    obj.Note = string.Empty;
                                    obj.IsKPI = null;
                                    try
                                    {
                                        var lstKPIRefer = lstKPIGroup.Where(c => c.Level < itemKPIGroup.Level).Select(c => new KPITimeDate
                                            {
                                                Expression = c.Expression,
                                                CompareField = c.CompareField
                                            }).ToList();
                                        KPITime_GetDate(kpiPackage, itemKPIGroup.TypeOfKPICode, obj, lstKPIRefer);
                                    }
                                    catch (Exception ex)
                                    {
                                        obj.Note = ex.Message;
                                    }

                                    if (obj.ID < 1)
                                        model.KPI_TimeDate.Add(obj);
                                }
                            }
                        }
                    }
                }
                #endregion

                model.SaveChanges();
            }
        }

        #endregion
    }
}
