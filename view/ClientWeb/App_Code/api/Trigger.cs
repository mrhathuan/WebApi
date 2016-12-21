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
using ServicesExtend;
using System.Reflection;

namespace ClientWeb
{
    public class TriggerController : BaseController
    {
        [HttpPost]
        public string MessageCall(dynamic dynParam)
        {
            try
            {
                string code = dynParam.code;
                var lst = new List<DTOTriggerMessage>();
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    lst = sv.MessageCall();
                });
                var hub = new ClientHub();
                foreach (var item in lst)
                {
                    hub.MessageCall(item.UserID, item.Total);
                }
                return code;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string WFL_MessageCall(dynamic dynParam)
        {
            try
            {
                string code = dynParam.code;
                var lst = new List<DTOTriggerMessage>();
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    lst = sv.WFL_MessageCall();
                });
                var hub = new ClientHub();
                foreach (var item in lst)
                {
                    hub.MessageCall(item.UserID, item.Total);
                }
                return code;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOTriggerMessage> MessageList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOTriggerMessage>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Packet_Check(dynamic dynParam)
        {
            try
            {
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    DTOTriggerWFLPacket data = sv.Packet_Check();
                    if (data != null)
                    {
                        Packet_Update(data);
                    }
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Packet_Update(DTOTriggerWFLPacket data)
        {
            if (data.ListPacket.Count > 0)
            {
                List<DTOTriggerPacketAction> lstTriggerPacketAction = new List<DTOTriggerPacketAction>();
                List<DTOTriggerPacketDriver> lstTriggerPacketDriver = new List<DTOTriggerPacketDriver>();
                foreach (var item in data.ListPacket)
                {
                    #region Lấy setting
                    CUSSettingsReport setting = new CUSSettingsReport();
                    if (!string.IsNullOrEmpty(item.Setting))
                        setting = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsReport>(item.Setting);
                    else
                        setting.TypeExport = 1;
                    #endregion

                    #region Đơn hàng
                    var lstPacketAction = data.ListPacketAction.Where(c => c.PacketID == item.ID).ToList();
                    if (lstPacketAction.Count > 0)
                    {
                        var lstFile = lstPacketAction.Select(c => new CATFile
                        {
                            ID = c.FileID,
                            FileName = c.FileName,
                            FilePath = c.FilePath,
                            FileExt = c.FileExt
                        }).Distinct().ToList();

                        foreach (var file in lstFile)
                        {
                            string filePath = Packet_ORD_GenFile(file, data.ListOPSGroup.Where(c => c.PacketID == item.ID).ToList(), setting);
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                lstTriggerPacketAction.AddRange(lstPacketAction.Where(c => c.FileID == file.ID).Select(c => new DTOTriggerPacketAction
                                    {
                                        PacketID = c.PacketID,
                                        PacketSettingActionID = c.PacketSettingActionID,
                                        FilePathResult = filePath,
                                    }).ToList());
                            }
                        }
                    }
                    #endregion

                    #region Chuyến
                    var lstPacketDriver = data.ListPacketDriver.Where(c => c.PacketID == item.ID).ToList();
                    if (item.DriverMailFileID > 0 && lstPacketDriver.Count > 0)
                    {
                        CATFile file = new CATFile();
                        file.FileName = item.DriverMailFileName;
                        file.FilePath = item.DriverMailFilePath;
                        file.FileExt = item.DriverMailFileExt;
                        string filePath = Packet_Master_GenFile(file, data.ListMaster.Where(c => c.PacketID == item.ID).ToList(), setting);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            lstTriggerPacketDriver.AddRange(lstPacketDriver.Where(c => c.TypeOfActionID == (int)WFLTypeOfAction.SendMail).Select(c => new DTOTriggerPacketDriver
                            {
                                PacketID = c.PacketID,
                                DriverID = c.DriverID,
                                TypeOfActionID = c.TypeOfActionID,
                                FilePathResult = filePath,
                            }).ToList());
                        }
                    }

                    if (item.DriverSMSFileID > 0 && lstPacketDriver.Count > 0)
                    {
                        CATFile file = new CATFile();
                        file.FileName = item.DriverMailFileName;
                        file.FilePath = item.DriverMailFilePath;
                        file.FileExt = item.DriverMailFileExt;
                        string filePath = Packet_Master_GenFile(file, data.ListMaster.Where(c => c.PacketID == item.ID).ToList(), setting);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            lstTriggerPacketDriver.AddRange(lstPacketDriver.Where(c => c.TypeOfActionID == (int)WFLTypeOfAction.SMS).Select(c => new DTOTriggerPacketDriver
                            {
                                PacketID = c.PacketID,
                                DriverID = c.DriverID,
                                TypeOfActionID = c.TypeOfActionID,
                                FilePathResult = filePath,
                            }).ToList());
                        }
                    }
                    #endregion
                }

                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    sv.Packet_Update(lstTriggerPacketAction, lstTriggerPacketDriver);
                });
            }
        }

        private string Packet_ORD_GenFile(CATFile file, List<DTOTriggerOPSGroup> ListData, CUSSettingsReport setting)
        {
            string result = string.Empty;
            switch (setting.TypeExport)
            {
                case 1: result = Packet_ORD_GenFile_Detail(file, ListData); break;
                case 2: result = Packet_ORD_GenFile_DetailColumn(file, ListData); break;
                case 3: result = Packet_ORD_GenFile_DetailGroupStock(file, ListData); break;
                case 4: result = Packet_ORD_GenFile_Order(file, ListData); break;
                case 5: result = Packet_ORD_GenFile_OrderColumn(file, ListData); break;
                case 6: result = Packet_ORD_GenFile_OrderGroupStock(file, ListData); break;
            }
            return result;
        }

        private string Packet_Master_GenFile(CATFile file, List<DTOTriggerMaster> ListData, CUSSettingsReport setting)
        {
            string result = string.Empty;

            return result;
        }

        private string Packet_ORD_GenFile_Detail(CATFile itemfile, List<DTOTriggerOPSGroup> ListData)
        {
            #region Tạo data
            var data = new List<DTOREPOPSPlan_Detail>();
            data = ListData.Select(c => new DTOREPOPSPlan_Detail
                {
                    TOMasterID = c.TOMasterID,
                    TOMasterCode = c.TOMasterCode,
                    TotalLocation = c.TotalLocation,
                    DITOGroupProductID = c.DITOGroupProductID,
                    OrderID = c.OrderID,
                    OrderCode = c.OrderCode,
                    DNCode = c.DNCode,
                    SOCode = c.SOCode,
                    DateConfig = c.DateConfig,
                    RequestDate = c.RequestDate,
                    StockID = c.StockID,
                    StockCode = c.StockCode,
                    StockName = c.StockName,
                    StockAddress = c.StockAddress,
                    PartnerID = c.PartnerID,
                    PartnerCode = c.PartnerCode,
                    PartnerName = c.PartnerName,
                    PartnerCodeName = c.PartnerCodeName,
                    Address = c.Address,
                    LocationToProvince = c.LocationToProvince,
                    LocationToDistrict = c.LocationToDistrict,

                    CUSRoutingID = c.CUSRoutingID,
                    CUSRoutingCode = c.CUSRoutingCode,
                    CUSRoutingName = c.CUSRoutingName,
                    OrderGroupProductID = c.OrderGroupProductID,
                    GroupOfProductID = c.GroupOfProductID,
                    GroupOfProductCode = c.GroupOfProductCode,
                    GroupOfProductName = c.GroupOfProductName,
                    Description = c.Description,

                    DriverName = c.DriverName,
                    TelNo = c.TelNo,
                    DrivingLicense = c.DrivingLicense,

                    VehicleID = c.VehicleID,
                    VehicleCode = c.VehicleCode,
                    VendorID = c.VendorID,
                    VendorCode = c.VendorCode,
                    VendorName = c.VendorName,
                    CustomerID = c.CustomerID,
                    CustomerCode = c.CustomerCode,
                    CustomerName = c.CustomerName,

                    TonTranfer = c.TonTranfer,
                    CBMTranfer = c.CBMTranfer,
                    QuantityTranfer = c.QuantityTranfer,
                    TonBBGN = c.TonBBGN,
                    CBMBBGN = c.CBMBBGN,
                    QuantityBBGN = c.QuantityBBGN,

                    KM = c.KM,
                    KMStart = c.KMStart,
                    KMEnd = c.KMEnd,
                    ETD = c.ETD,
                    ETA = c.ETA,

                    TonReturn = c.TonReturn,
                    CBMReturn = c.CBMReturn,
                    QuantityReturn = c.QuantityReturn,

                    KgTranfer = c.TonTranfer * 1000,
                    KgBBGN = c.TonBBGN * 1000,
                    KgReturn = c.TonReturn * 1000,

                    Note1 = c.Note1,
                    Note2 = c.Note2,

                    ProductCode = c.ProductCode,
                    ProductName = c.ProductName,
                    ProductDescription = c.ProductDescription,
                }).ToList();
            #endregion

            #region Tạo file
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
                    var typeProp = typeof(DTOREPOPSPlan_Detail);

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

                    if (rowStart < 1) throw new Exception("Kiểm tra [STT] có tồn tại và nằm ở sheet đầu tiên hay không");

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

                            foreach (var prop in dicProp)
                            {
                                var val = prop.Value.GetValue(item);
                                if (val != null)
                                    worksheet.Cells[row, prop.Key].Value = val;
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
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
            #endregion

            return newfile;
        }

        private string Packet_ORD_GenFile_DetailColumn(CATFile itemfile, List<DTOTriggerOPSGroup> ListData)
        {
            #region Tạo data
            var data = new DTOREPOPSPlan_ColumnDetail();
            data.ListColumn = new List<DTOREPOPSPlan_ColumnDetail_Group>();
            data.ListData = new List<DTOREPOPSPlan_Detail>();

            var lstData = ListData.Select(c => new
            {
                TOMasterID = c.TOMasterID,
                TOMasterCode = c.TOMasterCode,
                OrderID = c.OrderID,
                OrderCode = c.OrderCode,
                DateConfig = c.DateConfig,
                RequestDate = c.RequestDate,
                StockID = c.StockID,
                StockCode = c.StockCode,
                StockName = c.StockName,
                StockAddress = c.StockAddress,
                PartnerID = c.PartnerID,
                PartnerCode = c.PartnerCode,
                PartnerName = c.PartnerName,
                PartnerCodeName = c.PartnerCodeName,

                LocationToID = c.LocationToID,
                LocationToProvince = c.LocationToProvince,
                LocationToDistrict = c.LocationToDistrict,
                Address = c.Address,
                GroupOfProductID = c.GroupOfProductID,
                GroupOfProductCode = c.GroupOfProductCode,
                GroupOfProductName = c.GroupOfProductName,
                GroupOfLocationToCode = c.GroupOfLocationToCode,
                GroupOfLocationToName = c.GroupOfLocationToName,

                GroupOfVehicleCode = c.GroupOfVehicleCode,
                GroupOfVehicleName = c.GroupOfVehicleName,

                DriverName = c.DriverName,
                TelNo = c.TelNo,
                DrivingLicense = c.DrivingLicense,

                VehicleID = c.VehicleID,
                VehicleCode = c.VehicleCode,
                VendorID = c.VendorID,
                VendorCode = c.VendorCode,
                VendorName = c.VendorName,
                VendorShortName = c.VendorShortName,

                CustomerID = c.CustomerID,
                CustomerCode = c.CustomerCode,
                CustomerName = c.CustomerName,
                CustomerShortName = c.CustomerShortName,

                TotalLocation = c.TotalLocation,

                KM = c.KM,
                KMStart = c.KMStart,
                KMEnd = c.KMEnd,
                ETD = c.ETD,
                ETA = c.ETA,
            }).Distinct().ToList();

            var lstGroup = ListData.Select(c => new { c.GroupOfProductID, c.GroupOfProductName, c.GroupOfProductCode }).Distinct().ToList();

            foreach (var itemData in lstData)
            {
                DTOREPOPSPlan_Detail item = new DTOREPOPSPlan_Detail
                {
                    TOMasterID = itemData.TOMasterID,
                    TOMasterCode = itemData.TOMasterCode,

                    OrderID = itemData.OrderID,
                    OrderCode = itemData.OrderCode,

                    DateConfig = itemData.DateConfig,
                    RequestDate = itemData.RequestDate,

                    StockID = itemData.StockID,
                    StockCode = itemData.StockCode,
                    StockName = itemData.StockName,
                    StockAddress = itemData.StockAddress,
                    PartnerID = itemData.PartnerID,
                    PartnerCode = itemData.PartnerCode,
                    PartnerName = itemData.PartnerName,
                    PartnerCodeName = itemData.PartnerCodeName,

                    LocationToID = itemData.LocationToID,
                    LocationToProvince = itemData.LocationToProvince,
                    LocationToDistrict = itemData.LocationToDistrict,
                    Address = itemData.Address,
                    GroupOfLocationToCode = itemData.GroupOfLocationToCode,
                    GroupOfLocationToName = itemData.GroupOfLocationToName,

                    GroupOfVehicleCode = itemData.GroupOfVehicleCode,
                    GroupOfVehicleName = itemData.GroupOfVehicleName,

                    VehicleID = itemData.VehicleID,
                    VehicleCode = itemData.VehicleCode,
                    VendorID = itemData.VendorID,
                    VendorCode = itemData.VendorCode,
                    VendorName = itemData.VendorName,
                    VendorShortName = itemData.VendorShortName,

                    CustomerID = itemData.CustomerID,
                    CustomerCode = itemData.CustomerCode,
                    CustomerName = itemData.CustomerName,
                    CustomerShortName = itemData.CustomerShortName,

                    TotalLocation = itemData.TotalLocation,

                    KM = itemData.KM,
                    KMStart = itemData.KMStart,
                    KMEnd = itemData.KMEnd,
                    ETD = itemData.ETD,
                    ETA = itemData.ETA,

                    DriverName = itemData.DriverName,
                    TelNo = itemData.TelNo,
                    DrivingLicense = itemData.DrivingLicense,
                };

                data.ListData.Add(item);

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;
                item.TonTranfer = item.CBMTranfer = item.QuantityTranfer = item.TonBBGN = item.CBMBBGN = item.QuantityBBGN =
                    item.TonReturn = item.CBMReturn = item.QuantityReturn = item.KgTranfer = item.KgBBGN = item.KgReturn = 0;

                if (item.TOMasterID > 0)
                {
                    item.ScheduleTonTranfer = ListData.Where(c => c.TOMasterID == item.TOMasterID).Sum(c => c.TonTranfer);
                    item.ScheduleCBMTranfer = ListData.Where(c => c.TOMasterID == item.TOMasterID).Sum(c => c.CBMTranfer);
                    item.ScheduleQuantityTranfer = ListData.Where(c => c.TOMasterID == item.TOMasterID).Sum(c => c.QuantityTranfer);
                }

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;

                foreach (var itemGroup in lstGroup)
                {
                    var queryDITOGroup = ListData.Where(c => c.TOMasterID == item.TOMasterID && c.OrderID == item.OrderID && c.StockID == item.StockID && c.LocationToID == item.LocationToID && c.GroupOfProductID == itemGroup.GroupOfProductID);
                    if (queryDITOGroup.Count() > 0)
                    {
                        var first = queryDITOGroup.FirstOrDefault();
                        if (first != null)
                        {
                            item.CUSRoutingCode = first.CUSRoutingCode;
                            item.CUSRoutingName = first.CUSRoutingName;
                        }
                        var lstDITOGroupProductID = queryDITOGroup.Select(c => c.DITOGroupProductID).ToList();

                        var col = new DTOREPOPSPlan_ColumnDetail_Group();
                        col.TOMasterID = item.TOMasterID.Value;
                        col.OrderID = item.OrderID > 0 ? item.OrderID.Value : -1;
                        col.GroupOfProductID = itemGroup.GroupOfProductID != null ? itemGroup.GroupOfProductID.Value : 0;
                        col.GroupOfProductCode = itemGroup.GroupOfProductCode;
                        col.StockID = item.StockID.Value;
                        col.LocationToID = item.LocationToID;
                        col.LocationToProvince = item.LocationToProvince;
                        col.LocationToDistrict = item.LocationToDistrict;
                        col.KeyCode = itemGroup.GroupOfProductCode;

                        col.TonTranfer = queryDITOGroup.Sum(c => c.TonTranfer);
                        col.CBMTranfer = queryDITOGroup.Sum(c => c.CBMTranfer);
                        col.QuantityTranfer = queryDITOGroup.Sum(c => c.QuantityTranfer);
                        col.TonBBGN = queryDITOGroup.Sum(c => c.TonBBGN);
                        col.CBMBBGN = queryDITOGroup.Sum(c => c.CBMBBGN);
                        col.QuantityBBGN = queryDITOGroup.Sum(c => c.QuantityBBGN);
                        col.TonReturn = queryDITOGroup.Sum(c => c.TonReturn);
                        col.CBMReturn = queryDITOGroup.Sum(c => c.CBMReturn);
                        col.QuantityReturn = queryDITOGroup.Sum(c => c.QuantityReturn);
                        col.KgTranfer = queryDITOGroup.Sum(c => c.KgTranfer);
                        col.KgBBGN = queryDITOGroup.Sum(c => c.KgBBGN);
                        col.KgReturn = queryDITOGroup.Sum(c => c.KgReturn);

                        col.InvoiceNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceNote).Distinct().ToList());
                        col.InvoiceReturnNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceReturnNote).Distinct().ToList());

                        var queryProduct = ListData.Where(c => lstDITOGroupProductID.Contains(c.DITOGroupProductID));
                        if (queryProduct.Count() > 0)
                        {
                            col.ProductCode = string.Join(",", queryProduct.Select(c => c.ProductCode).Distinct().ToList());
                            col.ProductName = string.Join(",", queryProduct.Select(c => c.ProductName).Distinct().ToList());
                        }

                        data.ListColumn.Add(col);

                        item.TonTranfer += col.TonTranfer;
                        item.CBMTranfer += col.CBMTranfer;
                        item.QuantityTranfer += col.QuantityTranfer;
                        item.TonBBGN += col.TonBBGN;
                        item.CBMBBGN += col.CBMBBGN;
                        item.QuantityBBGN += col.QuantityBBGN;
                        item.TonReturn += col.TonReturn;
                        item.CBMReturn += col.CBMReturn;
                        item.QuantityReturn += col.QuantityReturn;
                        item.KgTranfer += col.KgTranfer;
                        item.KgBBGN += col.KgBBGN;
                        item.KgReturn += col.KgReturn;
                    }
                }
            }
            #endregion

            #region Tạo file
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
                    Dictionary<int, PropertyInfo> dicPropGroup = new Dictionary<int, PropertyInfo>();
                    Dictionary<int, string> dicPropGroupKey = new Dictionary<int, string>();
                    Dictionary<string, int> dicColumn = new Dictionary<string, int>();
                    List<ExcelRange> lstCheckFormula = new List<ExcelRange>();
                    Dictionary<int, string> dicCopy = new Dictionary<int, string>();
                    var typeProp = typeof(DTOREPOPSPlan_Detail);
                    var typePropGroup = typeof(DTOREPOPSPlan_ColumnDetail_Group);

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
                                        if (str.Split('-').Length == 1)
                                        {
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
                                        else
                                        {
                                            string strFirst = str.Substring(0, str.LastIndexOf('-'));
                                            string strLast = str.Substring(str.LastIndexOf('-') + 1);
                                            try
                                            {
                                                var prop = typePropGroup.GetProperty(strLast);
                                                if (prop != null)
                                                {
                                                    dicPropGroup.Add(col, prop);
                                                    dicPropGroupKey.Add(col, strFirst);
                                                    if (!dicColumn.ContainsKey(str))
                                                        dicColumn.Add(str, col);
                                                    flag = false;
                                                }
                                            }
                                            catch { }
                                        }
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

                    if (rowStart < 1) throw new Exception("Kiểm tra [STT] có tồn tại và nằm ở sheet đầu tiên hay không");

                    if (data.ListData.Count > 0)
                    {
                        int copyrowend = worksheet.Dimension.End.Row;
                        if (copyrowend > rowStart + 100)
                            copyrowend = rowStart + 100;
                        for (int copyrow = copyrowend; copyrow > rowStart; copyrow--)
                        {
                            worksheet.Cells[copyrow, 1, copyrow, worksheet.Dimension.End.Column].Copy(worksheet.Cells[copyrow + data.ListData.Count - 1, 1, copyrow + data.ListData.Count - 1, worksheet.Dimension.End.Column]);

                            for (col = 1; col <= worksheet.Dimension.End.Column && col < 200; col++)
                            {
                                var str = ExcelHelper.GetValue(worksheet, copyrow + data.ListData.Count - 1, col);
                                if (!string.IsNullOrEmpty(str) && str.StartsWith("="))
                                {
                                    if (str.IndexOf("[") > 0 && str.IndexOf("]") > 0)
                                        lstCheckFormula.Add(worksheet.Cells[copyrow + data.ListData.Count - 1, col]);
                                }
                            }
                        }

                        rowEnd = rowStart + data.ListData.Count - 1;
                        stt = 1;
                        row = rowStart;
                        col = colStart;

                        foreach (var item in data.ListData)
                        {
                            if (row != rowStart)
                                worksheet.Cells[rowStart, 1, rowStart, worksheet.Dimension.End.Column].Copy(worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column]);

                            worksheet.Cells[row, col].Value = stt;

                            foreach (var prop in dicProp)
                            {
                                var val = prop.Value.GetValue(item);
                                if (val != null)
                                    worksheet.Cells[row, prop.Key].Value = val;
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
                            }
                            foreach (var prop in dicPropGroup)
                            {
                                var groupKey = dicPropGroupKey[prop.Key];
                                var group = data.ListColumn.Where(c => c.TOMasterID == item.TOMasterID && c.OrderID == item.OrderID && c.StockID == item.StockID && c.LocationToID == item.LocationToID && c.KeyCode == groupKey).FirstOrDefault();
                                if (group != null)
                                {
                                    var val = prop.Value.GetValue(group);
                                    if (val != null)
                                        worksheet.Cells[row, prop.Key].Value = val;
                                    else
                                        worksheet.Cells[row, prop.Key].Value = null;
                                }
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
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
                                }
                            }

                            row++;
                            stt++;
                        }

                        Dictionary<string, string> dicTemp = new Dictionary<string, string>();
                        foreach (var item in dicColumn)
                        {
                            dicTemp.Add("[" + item.Key + "]", worksheet.Cells[rowStart, item.Value, rowStart + data.ListData.Count - 1, item.Value].Address);
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
            #endregion

            return newfile;
        }

        private string Packet_ORD_GenFile_DetailGroupStock(CATFile itemfile, List<DTOTriggerOPSGroup> ListData)
        {
            #region Tạo data
            var data = new DTOREPOPSPlan_ColumnDetail();
            data.ListColumn = new List<DTOREPOPSPlan_ColumnDetail_Group>();
            data.ListData = new List<DTOREPOPSPlan_Detail>();

            var lstData = ListData.Select(c => new
            {
                TOMasterID = c.TOMasterID,
                TOMasterCode = c.TOMasterCode,
                OrderID = c.OrderID,
                OrderCode = c.OrderCode,
                DateConfig = c.DateConfig,
                RequestDate = c.RequestDate,

                PartnerID = c.PartnerID,
                PartnerCode = c.PartnerCode,
                PartnerName = c.PartnerName,
                PartnerCodeName = c.PartnerCodeName,

                LocationToID = c.LocationToID,
                LocationToProvince = c.LocationToProvince,
                LocationToDistrict = c.LocationToDistrict,
                Address = c.Address,
                GroupOfProductID = c.GroupOfProductID,
                GroupOfProductCode = c.GroupOfProductCode,
                GroupOfProductName = c.GroupOfProductName,
                GroupOfLocationToCode = c.GroupOfLocationToCode,
                GroupOfLocationToName = c.GroupOfLocationToName,

                GroupOfVehicleCode = c.GroupOfVehicleCode,
                GroupOfVehicleName = c.GroupOfVehicleName,

                DriverName = c.DriverName,
                TelNo = c.TelNo,
                DrivingLicense = c.DrivingLicense,

                VehicleID = c.VehicleID,
                VehicleCode = c.VehicleCode,
                VendorID = c.VendorID,
                VendorCode = c.VendorCode,
                VendorName = c.VendorName,
                VendorShortName = c.VendorShortName,

                CustomerID = c.CustomerID,
                CustomerCode = c.CustomerCode,
                CustomerName = c.CustomerName,
                CustomerShortName = c.CustomerShortName,

                TotalLocation = c.TotalLocation,

                KM = c.KM,
                KMStart = c.KMStart,
                KMEnd = c.KMEnd,
                ETD = c.ETD,
                ETA = c.ETA,
            }).Distinct().ToList();

            var lstGroup = ListData.Select(c => new { c.GroupOfProductID, c.GroupOfProductName, c.GroupOfProductCode, c.StockID, c.StockCode }).Distinct().ToList();

            foreach (var itemData in lstData)
            {
                DTOREPOPSPlan_Detail item = new DTOREPOPSPlan_Detail
                {
                    TOMasterID = itemData.TOMasterID,
                    TOMasterCode = itemData.TOMasterCode,

                    OrderID = itemData.OrderID,
                    OrderCode = itemData.OrderCode,

                    DateConfig = itemData.DateConfig,
                    RequestDate = itemData.RequestDate,

                    PartnerID = itemData.PartnerID,
                    PartnerCode = itemData.PartnerCode,
                    PartnerName = itemData.PartnerName,
                    PartnerCodeName = itemData.PartnerCodeName,

                    LocationToID = itemData.LocationToID,
                    LocationToProvince = itemData.LocationToProvince,
                    LocationToDistrict = itemData.LocationToDistrict,
                    Address = itemData.Address,
                    GroupOfLocationToCode = itemData.GroupOfLocationToCode,
                    GroupOfLocationToName = itemData.GroupOfLocationToName,

                    GroupOfVehicleCode = itemData.GroupOfVehicleCode,
                    GroupOfVehicleName = itemData.GroupOfVehicleName,

                    VehicleID = itemData.VehicleID,
                    VehicleCode = itemData.VehicleCode,
                    VendorID = itemData.VendorID,
                    VendorCode = itemData.VendorCode,
                    VendorName = itemData.VendorName,
                    VendorShortName = itemData.VendorShortName,

                    CustomerID = itemData.CustomerID,
                    CustomerCode = itemData.CustomerCode,
                    CustomerName = itemData.CustomerName,
                    CustomerShortName = itemData.CustomerShortName,

                    TotalLocation = itemData.TotalLocation,

                    KM = itemData.KM,
                    KMStart = itemData.KMStart,
                    KMEnd = itemData.KMEnd,
                    ETD = itemData.ETD,
                    ETA = itemData.ETA,

                    DriverName = itemData.DriverName,
                    TelNo = itemData.TelNo,
                    DrivingLicense = itemData.DrivingLicense,
                };

                data.ListData.Add(item);

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;
                item.TonTranfer = item.CBMTranfer = item.QuantityTranfer = item.TonBBGN = item.CBMBBGN = item.QuantityBBGN =
                    item.TonReturn = item.CBMReturn = item.QuantityReturn = item.KgTranfer = item.KgBBGN = item.KgReturn = 0;

                if (item.TOMasterID > 0)
                {
                    item.ScheduleTonTranfer = ListData.Where(c => c.TOMasterID == item.TOMasterID).Sum(c => c.TonTranfer);
                    item.ScheduleCBMTranfer = ListData.Where(c => c.TOMasterID == item.TOMasterID).Sum(c => c.CBMTranfer);
                    item.ScheduleQuantityTranfer = ListData.Where(c => c.TOMasterID == item.TOMasterID).Sum(c => c.QuantityTranfer);
                }

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;

                foreach (var itemGroup in lstGroup)
                {
                    var queryDITOGroup = ListData.Where(c => c.TOMasterID == item.TOMasterID && c.OrderID == item.OrderID && c.StockID == item.StockID && c.LocationToID == item.LocationToID && c.GroupOfProductID == itemGroup.GroupOfProductID);
                    if (queryDITOGroup.Count() > 0)
                    {
                        var first = queryDITOGroup.FirstOrDefault();
                        if (first != null)
                        {
                            item.CUSRoutingCode = first.CUSRoutingCode;
                            item.CUSRoutingName = first.CUSRoutingName;
                        }
                        var lstDITOGroupProductID = queryDITOGroup.Select(c => c.DITOGroupProductID).ToList();

                        var col = new DTOREPOPSPlan_ColumnDetail_Group();
                        col.TOMasterID = item.TOMasterID.Value;
                        col.OrderID = item.OrderID > 0 ? item.OrderID.Value : -1;
                        col.GroupOfProductID = itemGroup.GroupOfProductID != null ? itemGroup.GroupOfProductID.Value : 0;
                        col.GroupOfProductCode = itemGroup.GroupOfProductCode;
                        col.StockID = itemGroup.StockID.Value;
                        col.LocationToID = item.LocationToID;
                        col.LocationToProvince = item.LocationToProvince;
                        col.LocationToDistrict = item.LocationToDistrict;
                        col.KeyCode = itemGroup.StockCode + "-" + itemGroup.GroupOfProductCode;

                        col.TonTranfer = queryDITOGroup.Sum(c => c.TonTranfer);
                        col.CBMTranfer = queryDITOGroup.Sum(c => c.CBMTranfer);
                        col.QuantityTranfer = queryDITOGroup.Sum(c => c.QuantityTranfer);
                        col.TonBBGN = queryDITOGroup.Sum(c => c.TonBBGN);
                        col.CBMBBGN = queryDITOGroup.Sum(c => c.CBMBBGN);
                        col.QuantityBBGN = queryDITOGroup.Sum(c => c.QuantityBBGN);
                        col.TonReturn = queryDITOGroup.Sum(c => c.TonReturn);
                        col.CBMReturn = queryDITOGroup.Sum(c => c.CBMReturn);
                        col.QuantityReturn = queryDITOGroup.Sum(c => c.QuantityReturn);
                        col.KgTranfer = queryDITOGroup.Sum(c => c.KgTranfer);
                        col.KgBBGN = queryDITOGroup.Sum(c => c.KgBBGN);
                        col.KgReturn = queryDITOGroup.Sum(c => c.KgReturn);

                        col.InvoiceNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceNote).Distinct().ToList());
                        col.InvoiceReturnNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceReturnNote).Distinct().ToList());

                        var queryProduct = ListData.Where(c => lstDITOGroupProductID.Contains(c.DITOGroupProductID));
                        if (queryProduct.Count() > 0)
                        {
                            col.ProductCode = string.Join(",", queryProduct.Select(c => c.ProductCode).Distinct().ToList());
                            col.ProductName = string.Join(",", queryProduct.Select(c => c.ProductName).Distinct().ToList());
                        }

                        data.ListColumn.Add(col);

                        item.TonTranfer += col.TonTranfer;
                        item.CBMTranfer += col.CBMTranfer;
                        item.QuantityTranfer += col.QuantityTranfer;
                        item.TonBBGN += col.TonBBGN;
                        item.CBMBBGN += col.CBMBBGN;
                        item.QuantityBBGN += col.QuantityBBGN;
                        item.TonReturn += col.TonReturn;
                        item.CBMReturn += col.CBMReturn;
                        item.QuantityReturn += col.QuantityReturn;
                        item.KgTranfer += col.KgTranfer;
                        item.KgBBGN += col.KgBBGN;
                        item.KgReturn += col.KgReturn;
                    }
                }
            }
            #endregion

            #region Tạo file
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
                    Dictionary<int, PropertyInfo> dicPropGroup = new Dictionary<int, PropertyInfo>();
                    Dictionary<int, string> dicPropGroupKey = new Dictionary<int, string>();
                    Dictionary<string, int> dicColumn = new Dictionary<string, int>();
                    List<ExcelRange> lstCheckFormula = new List<ExcelRange>();
                    Dictionary<int, string> dicCopy = new Dictionary<int, string>();
                    var typeProp = typeof(DTOREPOPSPlan_Detail);
                    var typePropGroup = typeof(DTOREPOPSPlan_ColumnDetail_Group);

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
                                        if (str.Split('-').Length == 1)
                                        {
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
                                        else
                                        {
                                            string strFirst = str.Substring(0, str.LastIndexOf('-'));
                                            string strLast = str.Substring(str.LastIndexOf('-') + 1);
                                            try
                                            {
                                                var prop = typePropGroup.GetProperty(strLast);
                                                if (prop != null)
                                                {
                                                    dicPropGroup.Add(col, prop);
                                                    dicPropGroupKey.Add(col, strFirst);
                                                    if (!dicColumn.ContainsKey(str))
                                                        dicColumn.Add(str, col);
                                                    flag = false;
                                                }
                                            }
                                            catch { }
                                        }
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

                    if (rowStart < 1) throw new Exception("Kiểm tra [STT] có tồn tại và nằm ở sheet đầu tiên hay không");

                    if (data.ListData.Count > 0)
                    {
                        int copyrowend = worksheet.Dimension.End.Row;
                        if (copyrowend > rowStart + 100)
                            copyrowend = rowStart + 100;
                        for (int copyrow = copyrowend; copyrow > rowStart; copyrow--)
                        {
                            worksheet.Cells[copyrow, 1, copyrow, worksheet.Dimension.End.Column].Copy(worksheet.Cells[copyrow + data.ListData.Count - 1, 1, copyrow + data.ListData.Count - 1, worksheet.Dimension.End.Column]);

                            for (col = 1; col <= worksheet.Dimension.End.Column && col < 200; col++)
                            {
                                var str = ExcelHelper.GetValue(worksheet, copyrow + data.ListData.Count - 1, col);
                                if (!string.IsNullOrEmpty(str) && str.StartsWith("="))
                                {
                                    if (str.IndexOf("[") > 0 && str.IndexOf("]") > 0)
                                        lstCheckFormula.Add(worksheet.Cells[copyrow + data.ListData.Count - 1, col]);
                                }
                            }
                        }

                        rowEnd = rowStart + data.ListData.Count - 1;
                        stt = 1;
                        row = rowStart;
                        col = colStart;

                        foreach (var item in data.ListData)
                        {
                            if (row != rowStart)
                                worksheet.Cells[rowStart, 1, rowStart, worksheet.Dimension.End.Column].Copy(worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column]);

                            worksheet.Cells[row, col].Value = stt;

                            foreach (var prop in dicProp)
                            {
                                var val = prop.Value.GetValue(item);
                                if (val != null)
                                    worksheet.Cells[row, prop.Key].Value = val;
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
                            }
                            foreach (var prop in dicPropGroup)
                            {
                                var groupKey = dicPropGroupKey[prop.Key];
                                var group = data.ListColumn.Where(c => c.TOMasterID == item.TOMasterID && c.OrderID == item.OrderID && c.LocationToID == item.LocationToID && c.KeyCode == groupKey).FirstOrDefault();
                                if (group != null)
                                {
                                    var val = prop.Value.GetValue(group);
                                    if (val != null)
                                        worksheet.Cells[row, prop.Key].Value = val;
                                    else
                                        worksheet.Cells[row, prop.Key].Value = null;
                                }
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
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
                                }
                            }

                            row++;
                            stt++;
                        }

                        Dictionary<string, string> dicTemp = new Dictionary<string, string>();
                        foreach (var item in dicColumn)
                        {
                            dicTemp.Add("[" + item.Key + "]", worksheet.Cells[rowStart, item.Value, rowStart + data.ListData.Count - 1, item.Value].Address);
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
            #endregion

            return newfile;
        }

        private string Packet_ORD_GenFile_Order(CATFile itemfile, List<DTOTriggerOPSGroup> ListData)
        {
            #region Tạo data
            var data = new List<DTOREPOPSPlan_Order>();
            var lstGroup = ListData.GroupBy(c => c.OrderGroupProductID);
            foreach (var itemGroup in lstGroup)
            {
                var itemDefault = itemGroup.FirstOrDefault();
                DTOREPOPSPlan_Order item = new DTOREPOPSPlan_Order();
                item.TOMasterCode = string.Join(",", itemGroup.Select(c => c.TOMasterCode).Distinct().ToList());
                item.OrderID = itemDefault.OrderID;
                item.OrderCode = itemDefault.OrderCode;
                item.DNCode = string.Join(",", itemGroup.Select(c => c.DNCode).Distinct().ToList());
                item.SOCode = itemDefault.SOCode;
                item.DateConfig = item.DateConfig;
                item.RequestDate = item.RequestDate;
                item.StockID = item.StockID;
                item.StockCode = item.StockCode;
                item.StockName = item.StockName;
                item.StockAddress = item.StockAddress;
                item.PartnerID = item.PartnerID;
                item.PartnerCode = item.PartnerCode;
                item.PartnerName = item.PartnerName;
                item.PartnerCodeName = item.PartnerCodeName;
                item.Address = item.Address;
                item.LocationToProvince = item.LocationToProvince;
                item.LocationToDistrict = item.LocationToDistrict;
                item.CUSRoutingID = item.CUSRoutingID;
                item.CUSRoutingCode = item.CUSRoutingCode;
                item.CUSRoutingName = item.CUSRoutingName;
                item.OrderGroupProductID = item.OrderGroupProductID;
                item.GroupOfProductID = item.GroupOfProductID;
                item.GroupOfProductCode = item.GroupOfProductCode;
                item.GroupOfProductName = item.GroupOfProductName;
                item.Description = item.Description;

                item.ProductCode = item.ProductCode;
                item.ProductName = item.ProductName;
                item.ProductDescription = item.ProductDescription;

                item.DriverName = string.Join(",", itemGroup.Select(c => c.DriverName).Distinct().ToList());
                item.TelNo = string.Join(",", itemGroup.Select(c => c.TelNo).Distinct().ToList());
                item.DrivingLicense = string.Join(",", itemGroup.Select(c => c.DrivingLicense).Distinct().ToList());

                item.VehicleCode = string.Join(",", itemGroup.Select(c => c.VehicleCode).Distinct().ToList());
                item.VendorCode = string.Join(",", itemGroup.Select(c => c.VendorCode).Distinct().ToList());
                item.VendorName = string.Join(",", itemGroup.Select(c => c.VendorName).Distinct().ToList());
                item.CustomerID = item.CustomerID;
                item.CustomerCode = item.CustomerCode;
                item.CustomerName = item.CustomerName;

                item.TonTranfer = itemGroup.Sum(c => c.TonTranfer);
                item.CBMTranfer = itemGroup.Sum(c => c.CBMTranfer);
                item.QuantityTranfer = itemGroup.Sum(c => c.QuantityTranfer);
                item.TonBBGN = itemGroup.Sum(c => c.TonBBGN);
                item.CBMBBGN = itemGroup.Sum(c => c.CBMBBGN);
                item.QuantityBBGN = itemGroup.Sum(c => c.QuantityBBGN);
                item.TonReturn = itemGroup.Sum(c => c.TonReturn);
                item.CBMReturn = itemGroup.Sum(c => c.CBMReturn);
                item.QuantityReturn = itemGroup.Sum(c => c.QuantityReturn);
                item.KgTranfer = itemGroup.Sum(c => c.KgTranfer);
                item.KgBBGN = itemGroup.Sum(c => c.KgBBGN);
                item.KgReturn = itemGroup.Sum(c => c.KgReturn);

                item.ETD = itemGroup.OrderBy(c => c.ETD).FirstOrDefault().ETD;
                item.ETA = itemGroup.OrderBy(c => c.ETA).FirstOrDefault().ETA;

                item.Note1 = string.Join(",", itemGroup.Select(c => c.Note1).Distinct().ToList());
                item.Note2 = string.Join(",", itemGroup.Select(c => c.Note2).Distinct().ToList());

                data.Add(item);
            }
            #endregion

            #region Tạo file
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
                    var typeProp = typeof(DTOREPOPSPlan_Order);

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

                    if (rowStart < 1) throw new Exception("Kiểm tra [STT] có tồn tại và nằm ở sheet đầu tiên hay không");

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

                            foreach (var prop in dicProp)
                            {
                                var val = prop.Value.GetValue(item);
                                if (val != null)
                                    worksheet.Cells[row, prop.Key].Value = val;
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
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
            #endregion

            return newfile;
        }

        private string Packet_ORD_GenFile_OrderColumn(CATFile itemfile, List<DTOTriggerOPSGroup> ListData)
        {
            #region Tạo data
            var data = new DTOREPOPSPlan_ColumnOrder();
            data.ListColumn = new List<DTOREPOPSPlan_ColumnOrder_Group>();
            data.ListData = new List<DTOREPOPSPlan_Order>();

            var lstData = ListData.Select(c => new
            {
                OrderID = c.OrderID,
                OrderCode = c.OrderCode,

                DateConfig = c.DateConfig,
                RequestDate = c.RequestDate,
                StockID = c.StockID,
                StockCode = c.StockCode,
                StockName = c.StockName,
                StockAddress = c.StockAddress,
                PartnerID = c.PartnerID,
                PartnerCode = c.PartnerCode,
                PartnerName = c.PartnerName,
                PartnerCodeName = c.PartnerCodeName,

                Address = c.Address,
                LocationToID = c.LocationToID,
                LocationToProvince = c.LocationToProvince,
                LocationToDistrict = c.LocationToDistrict,

                GroupOfProductID = c.GroupOfProductID,
                GroupOfProductCode = c.GroupOfProductCode,
                GroupOfProductName = c.GroupOfProductName,

                CustomerID = c.CustomerID,
                CustomerCode = c.CustomerCode,
                CustomerName = c.CustomerName,
                CustomerShortName = c.CustomerShortName,

                GroupOfLocationToCode = c.GroupOfLocationToCode,
                GroupOfLocationToName = c.GroupOfLocationToName,

                GroupOfVehicleCode = c.GroupOfVehicleCode,
                GroupOfVehicleName = c.GroupOfVehicleName,
            }).ToList();

            var lstGroup = ListData.Select(c => new { c.GroupOfProductID, c.GroupOfProductName, c.GroupOfProductCode }).Distinct().ToList();
            foreach (var itemData in lstData)
            {
                DTOREPOPSPlan_Order item = new DTOREPOPSPlan_Order
                {
                    OrderID = itemData.OrderID,
                    OrderCode = itemData.OrderCode,

                    DateConfig = itemData.DateConfig,
                    RequestDate = itemData.RequestDate,

                    StockID = itemData.StockID,
                    StockCode = itemData.StockCode,
                    StockName = itemData.StockName,
                    StockAddress = itemData.StockAddress,
                    PartnerID = itemData.PartnerID,
                    PartnerCode = itemData.PartnerCode,
                    PartnerName = itemData.PartnerName,
                    PartnerCodeName = itemData.PartnerCodeName,

                    LocationToID = itemData.LocationToID,
                    LocationToProvince = itemData.LocationToProvince,
                    LocationToDistrict = itemData.LocationToDistrict,
                    Address = itemData.Address,
                    GroupOfLocationToCode = itemData.GroupOfLocationToCode,
                    GroupOfLocationToName = itemData.GroupOfLocationToName,

                    GroupOfVehicleCode = itemData.GroupOfVehicleCode,
                    GroupOfVehicleName = itemData.GroupOfVehicleName,

                    CustomerID = itemData.CustomerID,
                    CustomerCode = itemData.CustomerCode,
                    CustomerName = itemData.CustomerName,
                    CustomerShortName = itemData.CustomerShortName,
                };

                data.ListData.Add(item);

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;
                item.TonTranfer = item.CBMTranfer = item.QuantityTranfer = item.TonBBGN = item.CBMBBGN = item.QuantityBBGN =
                    item.TonReturn = item.CBMReturn = item.QuantityReturn = item.KgTranfer = item.KgBBGN = item.KgReturn = 0;

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;

                foreach (var itemGroup in lstGroup)
                {
                    var queryDITOGroup = ListData.Where(c => c.OrderID == item.OrderID && c.StockID == item.StockID && c.LocationToID == item.LocationToID && c.GroupOfProductID == itemGroup.GroupOfProductID);
                    if (queryDITOGroup.Count() > 0)
                    {
                        var first = queryDITOGroup.FirstOrDefault();
                        if (first != null)
                        {
                            item.CUSRoutingCode = first.CUSRoutingCode;
                            item.CUSRoutingName = first.CUSRoutingName;
                        }
                        var lstOrderGroupProductID = queryDITOGroup.Select(c => c.OrderGroupProductID).ToList();

                        var col = new DTOREPOPSPlan_ColumnOrder_Group();
                        col.OrderID = item.OrderID > 0 ? item.OrderID.Value : -1;
                        col.GroupOfProductID = itemGroup.GroupOfProductID != null ? itemGroup.GroupOfProductID.Value : 0;
                        col.GroupOfProductCode = itemGroup.GroupOfProductCode;
                        col.StockID = item.StockID.Value;
                        col.LocationToID = item.LocationToID;
                        col.LocationToProvince = item.LocationToProvince;
                        col.LocationToDistrict = item.LocationToDistrict;
                        col.KeyCode = itemGroup.GroupOfProductCode;

                        col.TonTranfer = queryDITOGroup.Sum(c => c.TonTranfer);
                        col.CBMTranfer = queryDITOGroup.Sum(c => c.CBMTranfer);
                        col.QuantityTranfer = queryDITOGroup.Sum(c => c.QuantityTranfer);
                        col.TonBBGN = queryDITOGroup.Sum(c => c.TonBBGN);
                        col.CBMBBGN = queryDITOGroup.Sum(c => c.CBMBBGN);
                        col.QuantityBBGN = queryDITOGroup.Sum(c => c.QuantityBBGN);
                        col.TonReturn = queryDITOGroup.Sum(c => c.TonReturn);
                        col.CBMReturn = queryDITOGroup.Sum(c => c.CBMReturn);
                        col.QuantityReturn = queryDITOGroup.Sum(c => c.QuantityReturn);
                        col.KgTranfer = queryDITOGroup.Sum(c => c.KgTranfer);
                        col.KgBBGN = queryDITOGroup.Sum(c => c.KgBBGN);
                        col.KgReturn = queryDITOGroup.Sum(c => c.KgReturn);

                        col.InvoiceNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceNote).Distinct().ToList());
                        col.InvoiceReturnNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceReturnNote).Distinct().ToList());
                        
                        var queryProduct = ListData.Where(c => lstOrderGroupProductID.Contains(c.OrderGroupProductID));
                        if (queryProduct.Count() > 0)
                        {
                            col.ProductCode = string.Join(",", queryProduct.Select(c => c.ProductCode).Distinct().ToList());
                            col.ProductName = string.Join(",", queryProduct.Select(c => c.ProductName).Distinct().ToList());
                        }

                        data.ListColumn.Add(col);

                        item.TonTranfer += col.TonTranfer;
                        item.CBMTranfer += col.CBMTranfer;
                        item.QuantityTranfer += col.QuantityTranfer;
                        item.TonBBGN += col.TonBBGN;
                        item.CBMBBGN += col.CBMBBGN;
                        item.QuantityBBGN += col.QuantityBBGN;
                        item.TonReturn += col.TonReturn;
                        item.CBMReturn += col.CBMReturn;
                        item.QuantityReturn += col.QuantityReturn;
                        item.KgTranfer += col.KgTranfer;
                        item.KgBBGN += col.KgBBGN;
                        item.KgReturn += col.KgReturn;
                    }
                }
            }

            #endregion

            #region Tạo file
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
                    Dictionary<int, PropertyInfo> dicPropGroup = new Dictionary<int, PropertyInfo>();
                    Dictionary<int, string> dicPropGroupKey = new Dictionary<int, string>();
                    Dictionary<string, int> dicColumn = new Dictionary<string, int>();
                    List<ExcelRange> lstCheckFormula = new List<ExcelRange>();
                    Dictionary<int, string> dicCopy = new Dictionary<int, string>();
                    var typeProp = typeof(DTOREPOPSPlan_Order);
                    var typePropGroup = typeof(DTOREPOPSPlan_ColumnOrder_Group);

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
                                        if (str.Split('-').Length == 1)
                                        {
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
                                        else
                                        {
                                            string strFirst = str.Substring(0, str.LastIndexOf('-'));
                                            string strLast = str.Substring(str.LastIndexOf('-') + 1);
                                            try
                                            {
                                                var prop = typePropGroup.GetProperty(strLast);
                                                if (prop != null)
                                                {
                                                    dicPropGroup.Add(col, prop);
                                                    dicPropGroupKey.Add(col, strFirst);
                                                    if (!dicColumn.ContainsKey(str))
                                                        dicColumn.Add(str, col);
                                                    flag = false;
                                                }
                                            }
                                            catch { }
                                        }
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

                    if (rowStart < 1) throw new Exception("Kiểm tra [STT] có tồn tại và nằm ở sheet đầu tiên hay không");

                    if (data.ListData.Count > 0)
                    {
                        int copyrowend = worksheet.Dimension.End.Row;
                        if (copyrowend > rowStart + 100)
                            copyrowend = rowStart + 100;
                        for (int copyrow = copyrowend; copyrow > rowStart; copyrow--)
                        {
                            worksheet.Cells[copyrow, 1, copyrow, worksheet.Dimension.End.Column].Copy(worksheet.Cells[copyrow + data.ListData.Count - 1, 1, copyrow + data.ListData.Count - 1, worksheet.Dimension.End.Column]);

                            for (col = 1; col <= worksheet.Dimension.End.Column && col < 200; col++)
                            {
                                var str = ExcelHelper.GetValue(worksheet, copyrow + data.ListData.Count - 1, col);
                                if (!string.IsNullOrEmpty(str) && str.StartsWith("="))
                                {
                                    if (str.IndexOf("[") > 0 && str.IndexOf("]") > 0)
                                        lstCheckFormula.Add(worksheet.Cells[copyrow + data.ListData.Count - 1, col]);
                                }
                            }
                        }

                        rowEnd = rowStart + data.ListData.Count - 1;
                        stt = 1;
                        row = rowStart;
                        col = colStart;

                        foreach (var item in data.ListData)
                        {
                            if (row != rowStart)
                                worksheet.Cells[rowStart, 1, rowStart, worksheet.Dimension.End.Column].Copy(worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column]);

                            worksheet.Cells[row, col].Value = stt;

                            foreach (var prop in dicProp)
                            {
                                var val = prop.Value.GetValue(item);
                                if (val != null)
                                    worksheet.Cells[row, prop.Key].Value = val;
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
                            }
                            foreach (var prop in dicPropGroup)
                            {
                                var groupKey = dicPropGroupKey[prop.Key];
                                var group = data.ListColumn.Where(c => c.OrderID == item.OrderID && c.StockID == item.StockID && c.LocationToID == item.LocationToID && c.KeyCode == groupKey).FirstOrDefault();
                                if (group != null)
                                {
                                    var val = prop.Value.GetValue(group);
                                    if (val != null)
                                        worksheet.Cells[row, prop.Key].Value = val;
                                    else
                                        worksheet.Cells[row, prop.Key].Value = null;
                                }
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
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
                                }
                            }

                            row++;
                            stt++;
                        }

                        Dictionary<string, string> dicTemp = new Dictionary<string, string>();
                        foreach (var item in dicColumn)
                        {
                            dicTemp.Add("[" + item.Key + "]", worksheet.Cells[rowStart, item.Value, rowStart + data.ListData.Count - 1, item.Value].Address);
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
            #endregion

            return newfile;
        }

        private string Packet_ORD_GenFile_OrderGroupStock(CATFile itemfile, List<DTOTriggerOPSGroup> ListData)
        {
            #region Tạo data
            var data = new DTOREPOPSPlan_ColumnOrder();
            data.ListColumn = new List<DTOREPOPSPlan_ColumnOrder_Group>();
            data.ListData = new List<DTOREPOPSPlan_Order>();

            var lstData = ListData.Select(c => new
            {
                OrderID = c.OrderID,
                OrderCode = c.OrderCode,

                DateConfig = c.DateConfig,
                RequestDate = c.RequestDate,

                PartnerID = c.PartnerID,
                PartnerCode = c.PartnerCode,
                PartnerName = c.PartnerName,
                PartnerCodeName = c.PartnerCodeName,

                Address = c.Address,
                LocationToID = c.LocationToID,
                LocationToProvince = c.LocationToProvince,
                LocationToDistrict = c.LocationToDistrict,

                GroupOfProductID = c.GroupOfProductID,
                GroupOfProductCode = c.GroupOfProductCode,
                GroupOfProductName = c.GroupOfProductName,

                CustomerID = c.CustomerID,
                CustomerCode = c.CustomerCode,
                CustomerName = c.CustomerName,
                CustomerShortName = c.CustomerShortName,

                GroupOfLocationToCode = c.GroupOfLocationToCode,
                GroupOfLocationToName = c.GroupOfLocationToName,

                GroupOfVehicleCode = c.GroupOfVehicleCode,
                GroupOfVehicleName = c.GroupOfVehicleName,
            }).ToList();

            var lstGroup = ListData.Select(c => new { c.GroupOfProductID, c.GroupOfProductName, c.GroupOfProductCode, c.StockID, c.StockCode }).Distinct().ToList();
            foreach (var itemData in lstData)
            {
                DTOREPOPSPlan_Order item = new DTOREPOPSPlan_Order
                {
                    OrderID = itemData.OrderID,
                    OrderCode = itemData.OrderCode,

                    DateConfig = itemData.DateConfig,
                    RequestDate = itemData.RequestDate,

                    PartnerID = itemData.PartnerID,
                    PartnerCode = itemData.PartnerCode,
                    PartnerName = itemData.PartnerName,
                    PartnerCodeName = itemData.PartnerCodeName,

                    LocationToID = itemData.LocationToID,
                    LocationToProvince = itemData.LocationToProvince,
                    LocationToDistrict = itemData.LocationToDistrict,
                    Address = itemData.Address,
                    GroupOfLocationToCode = itemData.GroupOfLocationToCode,
                    GroupOfLocationToName = itemData.GroupOfLocationToName,

                    GroupOfVehicleCode = itemData.GroupOfVehicleCode,
                    GroupOfVehicleName = itemData.GroupOfVehicleName,

                    CustomerID = itemData.CustomerID,
                    CustomerCode = itemData.CustomerCode,
                    CustomerName = itemData.CustomerName,
                    CustomerShortName = itemData.CustomerShortName,
                };

                data.ListData.Add(item);

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;
                item.TonTranfer = item.CBMTranfer = item.QuantityTranfer = item.TonBBGN = item.CBMBBGN = item.QuantityBBGN =
                    item.TonReturn = item.CBMReturn = item.QuantityReturn = item.KgTranfer = item.KgBBGN = item.KgReturn = 0;

                item.PartnerCodeName = item.PartnerCode + "-" + item.PartnerName;

                foreach (var itemGroup in lstGroup)
                {
                    var queryDITOGroup = ListData.Where(c => c.OrderID == item.OrderID && c.StockID == item.StockID && c.LocationToID == item.LocationToID && c.GroupOfProductID == itemGroup.GroupOfProductID);
                    if (queryDITOGroup.Count() > 0)
                    {
                        var first = queryDITOGroup.FirstOrDefault();
                        if (first != null)
                        {
                            item.CUSRoutingCode = first.CUSRoutingCode;
                            item.CUSRoutingName = first.CUSRoutingName;
                        }
                        var lstOrderGroupProductID = queryDITOGroup.Select(c => c.OrderGroupProductID).ToList();

                        var col = new DTOREPOPSPlan_ColumnOrder_Group();
                        col.OrderID = item.OrderID > 0 ? item.OrderID.Value : -1;
                        col.GroupOfProductID = itemGroup.GroupOfProductID != null ? itemGroup.GroupOfProductID.Value : 0;
                        col.GroupOfProductCode = itemGroup.GroupOfProductCode;
                        col.StockID = itemGroup.StockID.Value;
                        col.LocationToID = item.LocationToID;
                        col.LocationToProvince = item.LocationToProvince;
                        col.LocationToDistrict = item.LocationToDistrict;
                        col.KeyCode = itemGroup.StockCode + "-" + itemGroup.GroupOfProductCode;

                        col.TonTranfer = queryDITOGroup.Sum(c => c.TonTranfer);
                        col.CBMTranfer = queryDITOGroup.Sum(c => c.CBMTranfer);
                        col.QuantityTranfer = queryDITOGroup.Sum(c => c.QuantityTranfer);
                        col.TonBBGN = queryDITOGroup.Sum(c => c.TonBBGN);
                        col.CBMBBGN = queryDITOGroup.Sum(c => c.CBMBBGN);
                        col.QuantityBBGN = queryDITOGroup.Sum(c => c.QuantityBBGN);
                        col.TonReturn = queryDITOGroup.Sum(c => c.TonReturn);
                        col.CBMReturn = queryDITOGroup.Sum(c => c.CBMReturn);
                        col.QuantityReturn = queryDITOGroup.Sum(c => c.QuantityReturn);
                        col.KgTranfer = queryDITOGroup.Sum(c => c.KgTranfer);
                        col.KgBBGN = queryDITOGroup.Sum(c => c.KgBBGN);
                        col.KgReturn = queryDITOGroup.Sum(c => c.KgReturn);

                        col.InvoiceNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceNote).Distinct().ToList());
                        col.InvoiceReturnNote = string.Join(",", queryDITOGroup.Select(c => c.InvoiceReturnNote).Distinct().ToList());

                        var queryProduct = ListData.Where(c => lstOrderGroupProductID.Contains(c.OrderGroupProductID));
                        if (queryProduct.Count() > 0)
                        {
                            col.ProductCode = string.Join(",", queryProduct.Select(c => c.ProductCode).Distinct().ToList());
                            col.ProductName = string.Join(",", queryProduct.Select(c => c.ProductName).Distinct().ToList());
                        }

                        data.ListColumn.Add(col);

                        item.TonTranfer += col.TonTranfer;
                        item.CBMTranfer += col.CBMTranfer;
                        item.QuantityTranfer += col.QuantityTranfer;
                        item.TonBBGN += col.TonBBGN;
                        item.CBMBBGN += col.CBMBBGN;
                        item.QuantityBBGN += col.QuantityBBGN;
                        item.TonReturn += col.TonReturn;
                        item.CBMReturn += col.CBMReturn;
                        item.QuantityReturn += col.QuantityReturn;
                        item.KgTranfer += col.KgTranfer;
                        item.KgBBGN += col.KgBBGN;
                        item.KgReturn += col.KgReturn;
                    }
                }
            }

            #endregion

            #region Tạo file
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
                    Dictionary<int, PropertyInfo> dicPropGroup = new Dictionary<int, PropertyInfo>();
                    Dictionary<int, string> dicPropGroupKey = new Dictionary<int, string>();
                    Dictionary<string, int> dicColumn = new Dictionary<string, int>();
                    List<ExcelRange> lstCheckFormula = new List<ExcelRange>();
                    Dictionary<int, string> dicCopy = new Dictionary<int, string>();
                    var typeProp = typeof(DTOREPOPSPlan_Order);
                    var typePropGroup = typeof(DTOREPOPSPlan_ColumnOrder_Group);

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
                                        if (str.Split('-').Length == 1)
                                        {
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
                                        else
                                        {
                                            string strFirst = str.Substring(0, str.LastIndexOf('-'));
                                            string strLast = str.Substring(str.LastIndexOf('-') + 1);
                                            try
                                            {
                                                var prop = typePropGroup.GetProperty(strLast);
                                                if (prop != null)
                                                {
                                                    dicPropGroup.Add(col, prop);
                                                    dicPropGroupKey.Add(col, strFirst);
                                                    if (!dicColumn.ContainsKey(str))
                                                        dicColumn.Add(str, col);
                                                    flag = false;
                                                }
                                            }
                                            catch { }
                                        }
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

                    if (rowStart < 1) throw new Exception("Kiểm tra [STT] có tồn tại và nằm ở sheet đầu tiên hay không");

                    if (data.ListData.Count > 0)
                    {
                        int copyrowend = worksheet.Dimension.End.Row;
                        if (copyrowend > rowStart + 100)
                            copyrowend = rowStart + 100;
                        for (int copyrow = copyrowend; copyrow > rowStart; copyrow--)
                        {
                            worksheet.Cells[copyrow, 1, copyrow, worksheet.Dimension.End.Column].Copy(worksheet.Cells[copyrow + data.ListData.Count - 1, 1, copyrow + data.ListData.Count - 1, worksheet.Dimension.End.Column]);

                            for (col = 1; col <= worksheet.Dimension.End.Column && col < 200; col++)
                            {
                                var str = ExcelHelper.GetValue(worksheet, copyrow + data.ListData.Count - 1, col);
                                if (!string.IsNullOrEmpty(str) && str.StartsWith("="))
                                {
                                    if (str.IndexOf("[") > 0 && str.IndexOf("]") > 0)
                                        lstCheckFormula.Add(worksheet.Cells[copyrow + data.ListData.Count - 1, col]);
                                }
                            }
                        }

                        rowEnd = rowStart + data.ListData.Count - 1;
                        stt = 1;
                        row = rowStart;
                        col = colStart;

                        foreach (var item in data.ListData)
                        {
                            if (row != rowStart)
                                worksheet.Cells[rowStart, 1, rowStart, worksheet.Dimension.End.Column].Copy(worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column]);

                            worksheet.Cells[row, col].Value = stt;

                            foreach (var prop in dicProp)
                            {
                                var val = prop.Value.GetValue(item);
                                if (val != null)
                                    worksheet.Cells[row, prop.Key].Value = val;
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
                            }
                            foreach (var prop in dicPropGroup)
                            {
                                var groupKey = dicPropGroupKey[prop.Key];
                                var group = data.ListColumn.Where(c => c.OrderID == item.OrderID && c.StockID == item.StockID && c.LocationToID == item.LocationToID && c.KeyCode == groupKey).FirstOrDefault();
                                if (group != null)
                                {
                                    var val = prop.Value.GetValue(group);
                                    if (val != null)
                                        worksheet.Cells[row, prop.Key].Value = val;
                                    else
                                        worksheet.Cells[row, prop.Key].Value = null;
                                }
                                else
                                    worksheet.Cells[row, prop.Key].Value = null;
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
                                }
                            }

                            row++;
                            stt++;
                        }

                        Dictionary<string, string> dicTemp = new Dictionary<string, string>();
                        foreach (var item in dicColumn)
                        {
                            dicTemp.Add("[" + item.Key + "]", worksheet.Cells[rowStart, item.Value, rowStart + data.ListData.Count - 1, item.Value].Address);
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
            #endregion

            return newfile;
        }


        [HttpPost]
        public void PriceMaterial_Save(dynamic dynParam)
        {
            try
            {
                DTOOtherPriceMaterial item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOtherPriceMaterial>(dynParam.item.ToString());

                var lst = new List<DTOTriggerMaterial>();
                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    lst = sv.PriceMaterial_ListMaterial();
                    if (lst != null && lst.Count > 0)
                    {
                        foreach (var itemMaterial in lst.Where(c => c.DieselArea1_MaterialID > 0 || c.DieselArea2_MaterialID > 0 ||
                            c.DO05Area1_MaterialID > 0 || c.DO05Area2_MaterialID > 0 || c.DO25Area1_MaterialID > 0 ||
                            c.DO25Area2_MaterialID > 0 || c.E5RON92Area1_MaterialID > 0 || c.E5RON92Area2_MaterialID > 0 ||
                            c.RON92Area1_MaterialID > 0 || c.RON92Area2_MaterialID > 0 || c.RON95Area1_MaterialID > 0 ||
                            c.RON95Area2_MaterialID > 0))
                        {
                            itemMaterial.DieselArea1 = item.DieselArea1;
                            itemMaterial.DieselArea2 = item.DieselArea2;
                            itemMaterial.DO05Area1 = item.DO05Area1;
                            itemMaterial.DO05Area2 = item.DO05Area2;
                            itemMaterial.DO25Area1 = item.DO25Area1;
                            itemMaterial.DO25Area2 = item.DO25Area2;
                            itemMaterial.E5RON92Area1 = item.E5RON92Area1;
                            itemMaterial.E5RON92Area2 = item.E5RON92Area2;
                            itemMaterial.RON92Area1 = item.RON92Area1;
                            itemMaterial.RON92Area2 = item.RON92Area2;
                            itemMaterial.RON95Area1 = item.RON95Area1;
                            itemMaterial.RON95Area2 = item.RON95Area2;

                            sv.PriceMaterial_Save(itemMaterial);
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
        public List<DTOCATLocation> Location_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATLocation>();
                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    result = sv.Location_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Location_Save(dynamic dynParam)
        {
            try
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATLocation>>(dynParam.lst.ToString());

                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    sv.Location_Save(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATLocationMatrix> LocationMatrix_List(dynamic dynParam)
        {
            try
            {
                var result = new List<CATLocationMatrix>();
                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    result = sv.LocationMatrix_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void LocationMatrix_Save(dynamic dynParam)
        {
            try
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CATLocationMatrix>>(dynParam.lst.ToString());

                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    sv.LocationMatrix_Save(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOTriggerEmail> TriggerEmail_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOTriggerEmail>();
                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    result = sv.TriggerEmail_List();
                });

                foreach (var itemSetting in result)
                {
                    foreach (var item in itemSetting.Items.Where(c => !string.IsNullOrEmpty(c.FilePath)))
                    {
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + item.FilePath)))
                        {
                            try
                            {
                                item.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                                item.FileByte = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("/" + item.FilePath));
                            }
                            catch { }
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
        public void TriggerEmail_Send(dynamic dynParam)
        {
            try
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOTriggerEmail>>(dynParam.lst.ToString());

                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    sv.TriggerEmail_Send(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTOTriggerEmail> WFL_TriggerEmail_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOTriggerEmail>();
                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    result = sv.WFL_TriggerEmail_List();
                });

                foreach (var itemSetting in result)
                {
                    foreach (var item in itemSetting.Items.Where(c => !string.IsNullOrEmpty(c.FilePath)))
                    {
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + item.FilePath)))
                        {
                            try
                            {
                                item.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                                item.FileByte = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("/" + item.FilePath));
                            }
                            catch { }
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
        public void WFL_TriggerEmail_Send(dynamic dynParam)
        {
            try
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOTriggerEmail>>(dynParam.lst.ToString());

                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    sv.WFL_TriggerEmail_Send(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public List<DTOTriggerSMS> TriggerSMS_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOTriggerSMS>();
                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    result = sv.TriggerSMS_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void TriggerSMS_Send(dynamic dynParam)
        {
            try
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOTriggerSMS>>(dynParam.lst.ToString());

                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    sv.TriggerSMS_Send(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOTriggerSMS> WFL_TriggerSMS_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOTriggerSMS>();
                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    result = sv.WFL_TriggerSMS_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFL_TriggerSMS_Send(dynamic dynParam)
        {
            try
            {
                var lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOTriggerSMS>>(dynParam.lst.ToString());

                ServiceFactory.SVTrigger((ISVTrigger sv) =>
                {
                    sv.WFL_TriggerSMS_Send(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}