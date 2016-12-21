using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.Data.Entity;
using System.ServiceModel;
using System.Net;
using System.Net.Mail;
using Kendo.Mvc;
using Opt_Plan = AdvancedPlanScheduling.TransportationPlanning;
using Opt_Data = AdvancedPlanScheduling.DataStructures;
using Opt_DataInput = AdvancedPlanScheduling.TransportationPlanning.InputData;
using Opt_DataOutput = AdvancedPlanScheduling.TransportationPlanning.OutputData;
using Opt_Tester = AdvancedPlanScheduling.TransportationPlanning.UnitTest;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;

namespace Business
{
    public class BLOptimize : Base, IBase
    {
        #region Variables

        const int NoDigit = 6;
        const string DICodeRrefix = "DI";
        const string DICodeNum = "0000000";
        const string COCodeRrefix = "CO";
        const string COCodeNum = "0000000";
        const string DIVehicleCode = "[Chờ nhập xe]";
        const string COVehicleCode = "[Chờ nhập xe]";
        const string CORomoocCode = "[Chờ nhập romooc]";

        const int iFCL = -(int)SYSVarType.TransportModeFCL;
        const int iPlanPlaning = -(int)SYSVarType.StatusOfPlanPlaning;
        const int iOrderPlaning = -(int)SYSVarType.StatusOfOrderPlaning;

        const int IMEmpty = -(int)SYSVarType.StatusOfCOContainerIMEmpty;
        const int IMLaden = -(int)SYSVarType.StatusOfCOContainerIMLaden;
        const int EXEmpty = -(int)SYSVarType.StatusOfCOContainerEXEmpty;
        const int EXLaden = -(int)SYSVarType.StatusOfCOContainerEXLaden;

        const int LOLaden = -(int)SYSVarType.StatusOfCOContainerLOLaden;
        const int LOGetEmpty = -(int)SYSVarType.StatusOfCOContainerLOGetEmpty;
        const int LOReturnEmpty = -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty;

        const int CTSizeDay = -(int)SYSVarType.ConstraintRequireTypeSizeDay;
        const int CTOpenDay = -(int)SYSVarType.ConstraintRequireTypeOpenDay;
        const int CTCloseDay = -(int)SYSVarType.ConstraintRequireTypeCloseDay;
        const int CTOpenWeek = -(int)SYSVarType.ConstraintRequireTypeOpenWeek;
        const int CTSizeWeek = -(int)SYSVarType.ConstraintRequireTypeSizeWeek;

        const string DataPath = @"C:\Hoang Hung\2016\TMSView\data\";
        const bool SaveJson = false;
                
        public class DTOOPTData
        {
            [JsonProperty]
            public int Status { get; set; }
            [JsonProperty]
            public Dictionary<string, Opt_DataInput.Location> Locations { get; set; }
            [JsonProperty]
            public Opt_DataInput.LocationToLocationMatrix LocationToLocationMatrix { get; set; }
            [JsonProperty]
            public List<Opt_DataInput.LocationVehicleTypeExclusion> LocationVehicleTypeExclusions { get; set; }
            [JsonProperty]
            public List<Opt_DataInput.Order> Orders { get; set; }
            [JsonProperty]
            public Dictionary<string, Opt_DataInput.Rate> Rates { get; set; }
            [JsonProperty]
            public List<Opt_DataInput.Vehicle> Vehicles { get; set; }
            [JsonProperty]
            public Dictionary<string, Opt_DataInput.VehicleType> VehicleTypes { get; set; }
            [JsonProperty]
            public Opt_DataInput.TransportationPlanInput Input { get; set; }
            [JsonProperty]
            public Opt_DataOutput.TransportationPlanOutput Output { get; set; }
        }

        public class DTOOPTDataJson
        {
            [JsonProperty]
            public int OptimizerID { get; set; }
            [JsonProperty]
            public string Locations { get; set; }
            [JsonProperty]
            public string LocationToLocationMatrix { get; set; }
            [JsonProperty]
            public string LocationVehicleTypeExclusions { get; set; }
            [JsonProperty]
            public string Orders { get; set; }
            [JsonProperty]
            public string Rates { get; set; }
            [JsonProperty]
            public string Vehicles { get; set; }
            [JsonProperty]
            public string VehicleTypes { get; set; }
            [JsonProperty]
            public string Output { get; set; }
        }

        private JsonSerializerSettings _jsonSetting = new JsonSerializerSettings
        {
            //TypeNameHandling = TypeNameHandling.All
        };

        #endregion

        public DTOResult Opt_Optimizer_List(string request, bool isCo)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_Optimizer.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.IsContainer == isCo).Select(c => new DTOOPTOptimizer
                    {
                        ID = c.ID,
                        OptimizerName = c.OptimizerName,
                        StatusOfOptimizer = c.IsContainer ? (model.OPS_OPTCOTOContainer.Count(o => o.OPS_OPTOPSContainer.OptimizerID == c.ID) == 0 ? 0 : c.IsSave == false ? 1 : 2) : (model.OPS_OPTDITOGroupProduct.Count(o => o.OPS_OPTOPSGroupProduct.OptimizerID == c.ID) == 0 ? 0 : c.IsSave == false ? 1 : 2),
                        StatusOfOptimizerName = c.IsContainer ? (model.OPS_OPTCOTOContainer.Count(o => o.OPS_OPTOPSContainer.OptimizerID == c.ID) == 0 ? "Khởi tạo" : c.IsSave == false ? "Đã tối ưu" : "Đã đóng") : (model.OPS_OPTDITOGroupProduct.Count(o => o.OPS_OPTOPSGroupProduct.OptimizerID == c.ID) == 0 ? "Khởi tạo" : c.IsSave == false ? "Đã tối ưu" : "Đã đóng"),
                        DateFrom = c.DateFrom,
                        DateTo = c.DateTo,
                        IsContainer = c.IsContainer,
                        CreatedBy = c.CreatedBy,
                        CreatedDate = c.CreatedDate,
                        IsBalanceCustomer = c.IsBalanceCustomer,
                        IsBalanceKMScore = c.IsBalanceKMScore
                    }).ToDataSourceResult(CreateRequest(request));

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTOptimizer>;
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

        public DTOResult Opt_Container_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTOPSContainer.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPSCOTOContainer
                    {
                        ID = c.ID,
                        COTOMasterID = c.OPS_COTOContainer.COTOMasterID,
                        PackingID = c.OPS_COTOContainer.OPS_Container.ORD_Container.PackingID,
                        PackingName = c.OPS_COTOContainer.OPS_Container.ORD_Container.CAT_Packing.PackingName,
                        ContainerID = c.OPS_COTOContainer.OPS_Container.ContainerID,
                        ContainerNo = c.OPS_COTOContainer.OPS_Container.ContainerNo,
                        SealNo1 = c.OPS_COTOContainer.OPS_Container.SealNo1,
                        SealNo2 = c.OPS_COTOContainer.OPS_Container.SealNo2,
                        StatusOfCOContainerID = c.OPS_COTOContainer.StatusOfCOContainerID,
                        StatusOfCOContainerName = c.OPS_COTOContainer.SYS_Var.ValueOfVar,
                        ETD = c.ETD,
                        ETA = c.ETA,
                        IsTimeRelax = c.ETA != c.OPS_COTOContainer.ETA || c.ETD != c.OPS_COTOContainer.ETD,
                        ETDStart = c.ETDStart,
                        ETAStart = c.ETAStart,
                        SortOrder = c.OPS_COTOContainer.SortOrder,
                        OrderID = c.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code,
                        CustomerID = c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                        CustomerCode = c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.CustomerName,
                        ServiceOfOrderName = c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.SYS_Var.ValueOfVar,
                        Note = c.OPS_COTOContainer.OPS_Container.Note,
                        LocationFromID = c.OPS_COTOContainer.LocationFromID,
                        LocationFromCode = c.OPS_COTOContainer.CAT_Location.Code,
                        LocationFromName = c.OPS_COTOContainer.CAT_Location.Location,
                        LocationFromAddress = c.OPS_COTOContainer.CAT_Location.Address,
                        LocationFromDistrictName = c.OPS_COTOContainer.CAT_Location.CAT_District.DistrictName,
                        LocationFromProvinceName = c.OPS_COTOContainer.CAT_Location.CAT_Province.ProvinceName,
                        LocationToID = c.OPS_COTOContainer.LocationToID,
                        LocationToCode = c.OPS_COTOContainer.CAT_Location1.Code,
                        LocationToName = c.OPS_COTOContainer.CAT_Location1.Location,
                        LocationToAddress = c.OPS_COTOContainer.CAT_Location1.Address,
                        LocationToDistrictName = c.OPS_COTOContainer.CAT_Location1.CAT_District.DistrictName,
                        LocationToProvinceName = c.OPS_COTOContainer.CAT_Location1.CAT_Province.ProvinceName,
                        TypeID = 0
                    }).ToDataSourceResult(CreateRequest(request));

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPSCOTOContainer>;
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

        public DTOResult Opt_Container_NotIn_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var isAdmin = Account.ListActionCode.Contains(SYSViewCode.ViewAdmin.ToString());
                    var obj = model.OPS_Optimizer.Where(c => c.ID == optimizerID).Select(c => new
                    {
                        c.DateFrom,
                        c.DateTo
                    }).FirstOrDefault();
                    if (obj != null)
                    {
                        var fDate = obj.DateFrom.Value.Date;
                        var tDate = obj.DateTo.Value.Date.AddDays(1);
                        var sExist = model.OPS_OPTOPSContainer.Where(c => c.OptimizerID == optimizerID).Select(c => c.COTOContainerID).ToList();
                        var query = model.OPS_COTOContainer.Where(c => c.COTOMasterID == null && DbFunctions.TruncateTime(c.ETD) >= fDate && DbFunctions.TruncateTime(c.ETD) <= tDate && !c.IsSplit
                            && !sExist.Contains(c.ID) && (isAdmin ? true : Account.ListCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID))).Select(c => new DTOOPSCOTOContainer
                            {
                                ID = c.ID,
                                IsTimeRelax = false,
                                COTOMasterID = c.COTOMasterID,
                                PackingID = c.OPS_Container.ORD_Container.PackingID,
                                PackingName = c.OPS_Container.ORD_Container.CAT_Packing.PackingName,
                                ContainerID = c.OPS_Container.ContainerID,
                                ContainerNo = c.OPS_Container.ContainerNo,
                                SealNo1 = c.OPS_Container.SealNo1,
                                SealNo2 = c.OPS_Container.SealNo2,
                                StatusOfCOContainerID = c.StatusOfCOContainerID,
                                StatusOfCOContainerName = c.SYS_Var.ValueOfVar,
                                ETD = c.ETD,
                                ETA = c.ETA,
                                ETDStart = c.ETDStart,
                                ETAStart = c.ETAStart,
                                SortOrder = c.SortOrder,
                                OrderID = c.OPS_Container.ORD_Container.OrderID,
                                OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                                CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                                CustomerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                                CustomerName = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.CustomerName,
                                ServiceOfOrderName = c.OPS_Container.ORD_Container.ORD_Order.SYS_Var.ValueOfVar,
                                Note = c.OPS_Container.Note,
                                LocationFromID = c.LocationFromID,
                                LocationFromCode = c.CAT_Location.Code,
                                LocationFromName = c.CAT_Location.Location,
                                LocationFromAddress = c.CAT_Location.Address,
                                LocationFromDistrictName = c.CAT_Location.CAT_District.DistrictName,
                                LocationFromProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
                                LocationToID = c.LocationToID,
                                LocationToCode = c.CAT_Location1.Code,
                                LocationToName = c.CAT_Location1.Location,
                                LocationToAddress = c.CAT_Location1.Address,
                                LocationToDistrictName = c.CAT_Location1.CAT_District.DistrictName,
                                LocationToProvinceName = c.CAT_Location1.CAT_Province.ProvinceName,
                                TypeID = 0
                            }).ToDataSourceResult(CreateRequest(request));

                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOOPSCOTOContainer>;
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

        public DTOResult Opt_GroupOfProduct_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTOPSGroupProduct.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTOPSGroupProduct
                    {
                        ID = c.ID,
                        GroupProductCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                        GroupProductName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                        OrderCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                        CustomerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.ShortName,
                        Ton = c.OPS_DITOGroupProduct.Ton,
                        CBM = c.OPS_DITOGroupProduct.CBM,
                        Quantity = c.OPS_DITOGroupProduct.Quantity,
                        ETA = c.ETA,
                        ETD = c.ETD,
                        ETAStart = c.ETAStart,
                        ETDStart = c.ETDStart,
                        Note = c.OPS_DITOGroupProduct.Note,
                        Note1 = c.OPS_DITOGroupProduct.Note1,
                        Note2 = c.OPS_DITOGroupProduct.Note2,
                        DNCode = c.OPS_DITOGroupProduct.DNCode,
                        PartnerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.PartnerCode,
                        PartnerName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName,
                        LocationFromName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationName,
                        LocationFromAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                        LocationFromDistrict = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_District.DistrictName,
                        LocationFromProvince = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_Province.ProvinceName,
                        LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationName,
                        LocationToAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        LocationToDistrict = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                        LocationToProvince = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                    }).ToDataSourceResult(CreateRequest(request));

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTOPSGroupProduct>;
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

        public DTOResult Opt_GroupOfProduct_NotIn_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var isAdmin = Account.ListActionCode.Contains(SYSViewCode.ViewAdmin.ToString());
                    var obj = model.OPS_Optimizer.Where(c => c.ID == optimizerID).Select(c => new
                    {
                        c.DateFrom,
                        c.DateTo
                    }).FirstOrDefault();
                    if (obj != null)
                    {
                        var fDate = obj.DateFrom.Value.Date;
                        var tDate = obj.DateTo.Value.Date.AddDays(1);
                        var sExist = model.OPS_OPTOPSGroupProduct.Where(c => c.OptimizerID == optimizerID).Select(c => c.DITOGroupProductID).ToList();
                        var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == null && c.OrderGroupProductID > 0
                            && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && !sExist.Contains(c.ID)
                            && DbFunctions.TruncateTime(c.ORD_GroupProduct.ETD) >= fDate && DbFunctions.TruncateTime(c.ORD_GroupProduct.ETD) <= tDate
                            && (isAdmin ? true : Account.ListCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new DTOOPTOPSGroupProduct
                        {
                            ID = c.ID,
                            GroupProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.ShortName,
                            Ton = c.Ton,
                            CBM = c.CBM,
                            Quantity = c.Quantity,
                            ETA = c.ORD_GroupProduct.ETA,
                            ETD = c.ORD_GroupProduct.ETD,
                            ETAStart = c.ORD_GroupProduct.ETAStart,
                            ETDStart = c.ORD_GroupProduct.ETDStart,
                            Note = c.Note,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            DNCode = c.DNCode,
                            PartnerCode = c.ORD_GroupProduct.CUS_Partner.PartnerCode,
                            PartnerName = c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName,
                            LocationFromName = c.ORD_GroupProduct.CUS_Location.LocationName,
                            LocationFromAddress = c.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                            LocationFromDistrict = c.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_District.DistrictName,
                            LocationFromProvince = c.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_Province.ProvinceName,
                            LocationToName = c.ORD_GroupProduct.CUS_Location1.LocationName,
                            LocationToAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            LocationToDistrict = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                            LocationToProvince = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOOPTOPSGroupProduct>;
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

        public DTOResult Opt_Vehicle_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTVehicle
                    {
                        ID = c.ID,
                        IsOverLoad = false,
                        VehicleID = c.VehicleID,
                        VehicleNo = c.CAT_Vehicle.RegNo,
                        RomoocID = c.RomoocID,
                        RomoocNo = c.RomoocID > 0 ? c.CAT_Romooc.RegNo : string.Empty,
                        MaxWeightCal = c.MaxWeightCal,
                        CBM = c.CAT_Vehicle.MaxCapacity > 0 ? c.CAT_Vehicle.MaxCapacity.Value : 0,
                        Lat = c.CAT_Vehicle.Lat,
                        Lng = c.CAT_Vehicle.Lng,
                        TotalKMScore = c.TotalKMScore.HasValue ? c.TotalKMScore.Value : 0
                    }).ToDataSourceResult(CreateRequest(request));                    
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTVehicle>;

                    foreach (DTOOPTVehicle item in result.Data)
                    {
                        var objCat = model.CAT_Vehicle.FirstOrDefault(c => c.ID == item.VehicleID);
                        if (item.RomoocID > 0)
                        {
                            if (item.MaxWeightCal > MaxWeight(model, item.VehicleID, item.RomoocID))
                                item.IsOverLoad = true;
                        }
                        else
                        {
                            if ( item.MaxWeightCal > objCat.MaxWeight)
                                item.IsOverLoad = true;
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

        public DTOResult Opt_Vehicle_NotIn_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var objOpt = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (objOpt != null)
                    {
                        int typeOfVehicle = -(int)SYSVarType.TypeOfVehicleTractor;
                        if (!objOpt.IsContainer)
                            typeOfVehicle = -(int)SYSVarType.TypeOfVehicleTruck;

                        var dataE = model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID).Select(c => c.VehicleID).ToList();
                        dataE.AddRange(new int[] { 1, 2 }.ToList());
                        var query = model.FLM_Asset.Where(c => c.VehicleID > 2 && !c.IsDisposal && c.SYSCustomerID == Account.SYSCustomerID && c.CAT_Vehicle.TypeOfVehicleID == typeOfVehicle && !dataE.Contains(c.VehicleID.Value)).Select(c => new DTOOPTVehicle
                        {
                            ID = c.ID,
                            VehicleID = c.VehicleID.Value,
                            VehicleNo = c.CAT_Vehicle.RegNo,
                            RomoocID = c.CAT_Vehicle.CurrentRomoocID,
                            RomoocNo = c.CAT_Vehicle.CurrentRomoocID > 0 ? c.CAT_Vehicle.CAT_Romooc.RegNo : string.Empty,
                            MaxWeightCal = c.CAT_Vehicle.MaxWeight ?? 0,
                            CBM = c.CAT_Vehicle.MaxCapacity ?? 0,
                            Lat = c.CAT_Vehicle.Lat,
                            Lng = c.CAT_Vehicle.Lng,
                            TotalKMScore = c.TotalKMScore.HasValue ? c.TotalKMScore.Value : 0
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOOPTVehicle>;
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

        public DTOResult Opt_Romooc_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTRomooc.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTRomooc
                    {
                        ID = c.ID,
                        RomoocID = c.RomoocID,
                        RomoocNo = c.CAT_Romooc.RegNo,
                        MaxWeightCal = c.CAT_Romooc.MaxWeight > 0 ? c.CAT_Romooc.MaxWeight.Value : 0,
                        Lat = c.CAT_Romooc.Lat,
                        Lng = c.CAT_Romooc.Lng,
                        Is20DC = c.CAT_Romooc.RegCapacity == 1 ? true : false
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTRomooc>;
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

        public DTOResult Opt_Romooc_NotIn_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var lstID = model.OPS_OPTRomooc.Where(c => c.OptimizerID == optimizerID).Select(c => c.RomoocID).ToList();
                    var query = model.CUS_Romooc.Where(c => c.CustomerID == Account.SYSCustomerID && !lstID.Contains(c.RomoocID)).Select(c => new DTOOPTRomooc
                    {
                        ID = c.ID,
                        RomoocID = c.RomoocID,
                        RomoocNo = c.CAT_Romooc.RegNo,
                        MaxWeightCal = c.CAT_Romooc.MaxWeight > 0 ? c.CAT_Romooc.MaxWeight.Value : 0,
                        Lat = c.CAT_Romooc.Lat,
                        Lng = c.CAT_Romooc.Lng
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTRomooc>;
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

        public DTOResult Opt_Location_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTLocation
                    {
                        ID = c.ID,
                        Code = c.CAT_Location.Code,
                        LocationName = c.CAT_Location.Location,
                        Address = c.CAT_Location.Address,
                        CountryName = c.CAT_Location.CAT_Country.CountryName,
                        ProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
                        DistrictName = c.CAT_Location.CAT_District.DistrictName,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTLocation>;
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

        public DTOResult Opt_Location_Require_List(int optLocationID, bool isSize)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTLocationRequire.Where(c => c.OPTLocationID == optLocationID).Select(c => new
                    {
                        c.ID,
                        c.TimeFrom,
                        c.TimeTo,
                        c.Weight,
                        c.Height,
                        c.Width,
                        c.Length,
                        c.ConstraintRequireTypeID
                    }).ToList();

                    var data = new List<DTOOPTLocationRequire>();
                    foreach (var item in query)
                    {
                        DTOOPTLocationRequire obj = new DTOOPTLocationRequire();
                        obj.ID = item.ID;
                        obj.TimeFrom = item.TimeFrom;
                        obj.TimeTo = item.TimeTo;
                        obj.Weight = item.Weight;
                        obj.Width = item.Width;
                        obj.Length = item.Length;
                        obj.Height = item.Height;
                        obj.IsSize = item.ConstraintRequireTypeID == CTSizeDay || item.ConstraintRequireTypeID == CTSizeWeek;
                        if (item.TimeFrom.HasValue)
                        {
                            obj.DayOfWeek = (int)obj.TimeFrom.Value.Date.DayOfWeek;
                            if (item.ConstraintRequireTypeID != CTOpenWeek && item.ConstraintRequireTypeID != CTSizeWeek)
                            {
                                obj.Date = obj.TimeFrom.Value.Date;
                            }
                        }
                        data.Add(obj);
                    }
                    result.Data = data.Where(c => c.IsSize == isSize).OrderBy(c => c.DayOfWeek).ToList();
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

        public DTOResult Opt_Routing_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTRouting.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTRouting
                    {
                        ID = c.ID,
                        Code = c.CAT_Routing.Code,
                        RoutingName = c.CAT_Routing.RoutingName,
                        RoutingDescription = string.Empty,
                        KM = c.CAT_Routing.EDistance > 0 ? c.CAT_Routing.EDistance.Value : 0,
                        Time = c.CAT_Routing.EHours > 0 ? c.CAT_Routing.EHours.Value : 0,
                        FromLat = c.CAT_Routing.LocationFromID > 0 ? c.CAT_Routing.CAT_Location.Lat : c.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.FirstOrDefault(o => o.CAT_Location.Lat > 0 && o.CAT_Location.Lng > 0 && o.CAT_Location.Lat != o.CAT_Location.Lng).CAT_Location.Lat,
                        FromLng = c.CAT_Routing.LocationFromID > 0 ? c.CAT_Routing.CAT_Location.Lng : c.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.FirstOrDefault(o => o.CAT_Location.Lng > 0 && o.CAT_Location.Lng > 0 && o.CAT_Location.Lat != o.CAT_Location.Lng).CAT_Location.Lng,
                        ToLat = c.CAT_Routing.LocationToID > 0 ? c.CAT_Routing.CAT_Location1.Lat : c.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.FirstOrDefault(o => o.CAT_Location.Lat > 0 && o.CAT_Location.Lng > 0 && o.CAT_Location.Lat != o.CAT_Location.Lng).CAT_Location.Lat,
                        ToLng = c.CAT_Routing.LocationToID > 0 ? c.CAT_Routing.CAT_Location1.Lng : c.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.FirstOrDefault(o => o.CAT_Location.Lng > 0 && o.CAT_Location.Lng > 0 && o.CAT_Location.Lat != o.CAT_Location.Lng).CAT_Location.Lng,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTRouting>;
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

        public DTOResult Opt_Routing_Require_List(int optRoutingID, bool isSize)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTRoutingRequire.Where(c => c.OPTRoutingID == optRoutingID).Select(c => new
                    {
                        c.ID,
                        c.TimeFrom,
                        c.TimeTo,
                        c.Weight,
                        c.Height,
                        c.Width,
                        c.Length,
                        c.ConstraintRequireTypeID
                    }).ToList();

                    var data = new List<DTOOPTLocationRequire>();
                    foreach (var item in query)
                    {
                        DTOOPTLocationRequire obj = new DTOOPTLocationRequire();
                        obj.ID = item.ID;
                        obj.TimeFrom = item.TimeFrom;
                        obj.TimeTo = item.TimeTo;
                        obj.Weight = item.Weight;
                        obj.Width = item.Width;
                        obj.Length = item.Length;
                        obj.Height = item.Height;
                        obj.IsSize = item.ConstraintRequireTypeID == CTSizeDay || item.ConstraintRequireTypeID == CTSizeWeek;
                        if (item.TimeFrom.HasValue)
                        {
                            obj.DayOfWeek = (int)obj.TimeFrom.Value.Date.DayOfWeek;
                            if (item.ConstraintRequireTypeID != CTOpenWeek && item.ConstraintRequireTypeID != CTSizeWeek)
                            {
                                obj.Date = obj.TimeFrom.Value.Date;
                            }
                        }
                        data.Add(obj);
                    }
                    result.Data = data.Where(c => c.IsSize == isSize).OrderBy(c => c.DayOfWeek).ToList();
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

        public DTOResult Opt_LocationMatrix_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTLocationMatrix.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTLocationToLocation
                    {
                        ID = c.ID,
                        LocationFromID = c.LocationFromID,
                        LocationFromLat = c.CAT_Location.Lat ?? 0,
                        LocationFromLng = c.CAT_Location.Lng ?? 0,
                        LocationFromName = c.CAT_Location.Location,
                        LocationToID = c.LocationToID,
                        LocationToLat = c.CAT_Location1.Lat ?? 0,
                        LocationToLng = c.CAT_Location1.Lng ?? 0,
                        LocationToName = c.CAT_Location1.Location,
                        EDistance = c.KM,
                        EHours = c.Hour,
                        LocationFromAddress = c.CAT_Location.Address,
                        LocationToAddress = c.CAT_Location1.Address
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTLocationToLocation>;
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

        public DTOResult Opt_COTOMaster_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTCOTOMaster.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTCOTOMaster
                    {
                        ID = c.ID,
                        HasChanged = c.ModifiedBy != null,
                        OptimizerID = c.OptimizerID,
                        VehicleID = c.VehicleID,
                        VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                        VendorOfVehicleID = c.VendorOfVehicleID,
                        VendorOfVehicleName = c.VendorOfVehicleID > 0 ? c.CUS_Customer1.ShortName : "Xe nhà",
                        RomoocID = c.RomoocID,
                        RomoocNo = c.RomoocID > 0 ? c.CAT_Romooc.RegNo : string.Empty,
                        VendorOfRomoocID = c.VendorOfRomoocID,
                        VendorOfRomoocName = c.VendorOfRomoocID > 0 ? c.CUS_Customer.ShortName : "Xe nhà",
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverCard1 = c.DriverCard1,
                        DriverCard2 = c.DriverCard2,
                        DriverName1 = c.DriverName1,
                        DriverName2 = c.DriverName2,
                        DriverTel1 = c.DriverTel1,
                        DriverTel2 = c.DriverTel2,
                        AssistantID1 = c.AssistantID1,
                        AssistantID2 = c.AssistantID2,
                        AssistantCard1 = c.AssistantCard1,
                        AssistantCard2 = c.AssistantCard2,
                        AssistantName1 = c.AssistantName1,
                        AssistantName2 = c.AssistantName2,
                        AssistantTel1 = c.AssistantTel1,
                        AssistantTel2 = c.AssistantTel2,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        ETA = c.ETA,
                        ETD = c.ETD,
                        Note = c.Note,
                        KM = c.KM > 0 ? c.KM.Value : 0,
                        TransportModeID = c.TransportModeID,
                        TypeOfOrderID = c.TypeOfOrderID,
                        ContractID = c.ContractID,
                        RoutingID = c.RoutingID,
                        Credit = c.Credit,
                        Debit = c.Debit,
                        DateConfig = c.DateConfig
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTCOTOMaster>;
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

        public DTOResult Opt_COTOMaster_Container_List(string request, int masterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == masterID).Select(c => new DTOOPTCOTOContainer
                    {
                        ID = c.ID,
                        OPTOPSContainerID = c.OPTOPSContainerID,
                        OrderID = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID,
                        ServiceOfOrderName = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.SYS_Var.ValueOfVar,
                        OrderCode = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code,
                        CustomerCode = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        ContainerNo = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ContainerNo,
                        SealNo1 = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.SealNo1,
                        SealNo2 = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.SealNo2,
                        ETD = c.ETD,
                        ETA = c.ETA,
                        ETAStart = c.ETAStart,
                        ETDStart = c.ETDStart,
                        SortOrder = c.SortOrder,
                        LocationFromID = c.OPS_OPTOPSContainer.OPS_COTOContainer.LocationFromID,
                        LocationToID = c.OPS_OPTOPSContainer.OPS_COTOContainer.LocationToID,
                        LocationFromName = c.OPS_OPTOPSContainer.OPS_COTOContainer.CAT_Location.Location,
                        LocationToName = c.OPS_OPTOPSContainer.OPS_COTOContainer.CAT_Location1.Location,
                        StatusOfCOContainerID = c.StatusOfCOContainerID,
                        StatusOfCOContainerName = c.SYS_Var.ValueOfVar,
                        PackingID = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.PackingID,
                        PackingName = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.CAT_Packing.PackingName,
                        Ton = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.Ton,
                        PlanNote = c.OPS_OPTOPSContainer.Note
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTCOTOContainer>;
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

        public DTOResult Opt_COTOContainer_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTCOTOContainer.Where(c => c.OPS_OPTOPSContainer.OptimizerID == optimizerID && c.IsLocked == false).Select(c => new DTOOPTCOTOContainer
                    {
                        ID = c.ID,
                        OPTOPSContainerID = c.OPTOPSContainerID,
                        ServiceOfOrderName = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.SYS_Var.ValueOfVar,
                        OrderID = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code,
                        CustomerCode = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        ContainerNo = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ContainerNo,
                        SealNo1 = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.SealNo1,
                        SealNo2 = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.SealNo2,
                        Note = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.Note,
                        ETD = c.ETD,
                        ETA = c.ETA,
                        ETAStart = c.ETAStart,
                        ETDStart = c.ETDStart,
                        SortOrder = c.SortOrder,
                        OPTCOTOMasterID = c.OPTCOTOMasterID,
                        OPTCOTOMasterCode = c.OPTCOTOMasterID.HasValue ? c.OPS_OPTCOTOMaster.Note : string.Empty,
                        LocationFromID = c.OPS_OPTOPSContainer.OPS_COTOContainer.LocationFromID,
                        LocationToID = c.OPS_OPTOPSContainer.OPS_COTOContainer.LocationToID,
                        LocationFromName = c.OPS_OPTOPSContainer.OPS_COTOContainer.CAT_Location.Location,
                        LocationToName = c.OPS_OPTOPSContainer.OPS_COTOContainer.CAT_Location1.Location,
                        StatusOfCOContainerID = c.StatusOfCOContainerID,
                        StatusOfCOContainerName = c.SYS_Var.ValueOfVar,
                        PackingID = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.PackingID,
                        PackingName = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.CAT_Packing.PackingName,
                        Ton = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.Ton,
                        VehicleNo = c.OPTCOTOMasterID.HasValue && c.OPS_OPTCOTOMaster.VehicleID > 0 ? c.OPS_OPTCOTOMaster.CAT_Vehicle.RegNo : string.Empty,
                        PlanNote = c.OPS_OPTOPSContainer.Note
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTCOTOContainer>;
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

        public DTOResult Opt_COTOLocation_List(int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTCOTOLocation.Where(c => c.OPS_OPTCOTOMaster.OptimizerID == optimizerID).GroupBy(c => c.OPTCOTOMasterID).Select(c => new DTOOPTTripDetail
                    {
                        ID = c.Key,
                        TripNo = c.FirstOrDefault().OPS_OPTCOTOMaster.Note,
                        VehicleNo = c.FirstOrDefault().OPS_OPTCOTOMaster.CAT_Vehicle.RegNo,
                        RomoocNo = c.FirstOrDefault().OPS_OPTCOTOMaster.CAT_Romooc.RegNo,
                        ListLocation = c.Where(o => o.SortOrder > 0).OrderBy(o => o.SortOrder).Select(o => new DTOOPTTripLocation
                        {
                            ID = o.LocationID.Value,
                            Location = o.CAT_Location.Location,
                            Address = o.CAT_Location.Address,
                            DateCome = o.DateCome,
                            DateLeave = o.DateLeave,
                            Lat = o.CAT_Location.Lat,
                            Lng = o.CAT_Location.Lng
                        }).ToList()
                    }).ToList();
                    result.Total = query.Count;
                    result.Data = query as IEnumerable<DTOOPTTripDetail>;
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

        public DTOResult Opt_Optimizer_VehicleSchedule(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                List<DTOOPTVehicleSchedule> data = new List<DTOOPTVehicleSchedule>();
                using (var model = new DataEntities())
                {
                    Opt_DataInput.TransportationPlanInput input = Opt_Tester.Tester.ReadJson(DataPath);
                    var optimizer = new Opt_Plan.TransportationPlanOptimizer(input);
                    var output = optimizer.Run();
                    if (output != null)
                    {
                        int sLocation = 1;
                        int eLocation = 1;
                        var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                        if (!string.IsNullOrEmpty(sSet))
                        {
                            var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                            if (oSet != null)
                            {
                                var obj1 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationFromID);
                                if (obj1 != null)
                                    sLocation = obj1.ID;
                                var obj2 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationToID);
                                if (obj2 != null)
                                    eLocation = obj2.ID;
                            }
                        }
                        foreach (var item in output.VehicleSchedules)
                        {
                            try
                            {
                                DTOOPTVehicleSchedule obj = new DTOOPTVehicleSchedule();
                                obj.ID = Convert.ToInt32(item.VehicleID);
                                obj.VehicleNo = item.Vehicle.Optionals["VehicleNo"];
                                obj.RomoocNo = item.Vehicle.Optionals["RomoocNo"];
                                var objSum = output.VehicleSummaries.FirstOrDefault(c => c.VehicleID == item.VehicleID);
                                if (objSum != null)
                                {
                                    obj.TotalCost = (decimal)objSum.Summary.TotalCost;
                                    obj.TotalDrivingTime = objSum.Summary.TotalDrivingTime;
                                    obj.TotalTravelDistance = objSum.Summary.TotalTravelDistance;
                                    obj.TotalWaitingTime = objSum.Summary.TotalWaitingTime;
                                }
                                obj.ListActivity = new List<DTOOPTVehicleActivity>();
                                obj.ListLocation = new List<DTOOPTTripLocation>();

                                Dictionary<int, List<DTOOPTLocationActivity>> dicActivity = new Dictionary<int, List<DTOOPTLocationActivity>>();
                                foreach (var o in item.ActivityList)
                                {
                                    var activity = new DTOOPTVehicleActivity();
                                    activity.BeginTime = o.ActivityTime.EarliestDateTime;
                                    activity.EndTime = o.ActivityTime.LatestDateTime;
                                    var type = o.GetType();
                                    if (type == typeof(Opt_DataOutput.AtLocationActivity))
                                    {
                                        var cObj = (Opt_DataOutput.AtLocationActivity)o;
                                        activity.AtLocationID = Convert.ToInt32(cObj.AtLocation.Optionals["CATLocationID"]);
                                        if (!dicActivity.ContainsKey(activity.AtLocationID.Value))
                                        {
                                            dicActivity.Add(activity.AtLocationID.Value, new List<DTOOPTLocationActivity>());
                                        }
                                    }
                                    else if (type == typeof(Opt_DataOutput.LoadActivity))
                                    {
                                        var cObj = (Opt_DataOutput.LoadActivity)o;
                                        activity.AtLocationID = Convert.ToInt32(cObj.AtLocation.Optionals["CATLocationID"]);
                                        if (!dicActivity.ContainsKey(activity.AtLocationID.Value))
                                        {
                                            dicActivity.Add(activity.AtLocationID.Value, new List<DTOOPTLocationActivity>());
                                        }

                                        activity.LoadOrderNames = new List<string>();
                                        foreach (var order in cObj.LoadOrders)
                                        {
                                            activity.LoadOrderNames.Add(order.Optionals["ContainerNo"] + ",," + order.Optionals["OrderCode"]);
                                            DTOOPTLocationActivity objLoad = new DTOOPTLocationActivity();
                                            objLoad.ID = Convert.ToInt32(order.ID);
                                            objLoad.IsLoad = true;
                                            objLoad.OrderCode = order.Optionals["OrderCode"];
                                            objLoad.ContainerNo = order.Optionals["ContainerNo"];
                                            objLoad.SealNo1 = order.Optionals["SealNo1"];
                                            objLoad.SealNo2 = order.Optionals["SealNo2"];
                                            objLoad.PackingName = order.Optionals["PackingName"];
                                            objLoad.StatusOfCOContainerName = order.Optionals["StatusOfCOContainerName"];
                                            objLoad.ETD = cObj.ActivityTime.EarliestDateTime;
                                            objLoad.ETA = cObj.ActivityTime.LatestDateTime;
                                            objLoad.Ton = order.OrderSize.Weight > 0 ? order.OrderSize.Weight.Value : 0;

                                            dicActivity[activity.AtLocationID.Value].Add(objLoad);
                                        }
                                    }
                                    else if (type == typeof(Opt_DataOutput.OnRoadActivity))
                                    {
                                        var cObj = (Opt_DataOutput.OnRoadActivity)o;
                                        activity.FromLocationID = Convert.ToInt32(cObj.FromLocation.Optionals["CATLocationID"]);
                                        activity.FromLocationName = cObj.FromLocation.Optionals["Code"];
                                        activity.ToLocationID = Convert.ToInt32(cObj.ToLocation.Optionals["CATLocationID"]);
                                        activity.ToLocationName = cObj.ToLocation.Optionals["Code"];
                                        activity.CarryingOrderNames = new List<string>();
                                        foreach (var order in cObj.CarryingOrders)
                                        {
                                            activity.CarryingOrderNames.Add(order.Optionals["ContainerNo"] + ",," + order.Optionals["OrderCode"]);
                                        }
                                    }
                                    else if (type == typeof(Opt_DataOutput.UnloadActivity))
                                    {
                                        var cObj = (Opt_DataOutput.UnloadActivity)o;
                                        activity.AtLocationID = Convert.ToInt32(cObj.AtLocation.Optionals["CATLocationID"]);
                                        if (!dicActivity.ContainsKey(activity.AtLocationID.Value))
                                        {
                                            dicActivity.Add(activity.AtLocationID.Value, new List<DTOOPTLocationActivity>());
                                        }
                                        activity.UnLoadOrderNames = new List<string>();
                                        foreach (var order in cObj.UnLoadOrders)
                                        {
                                            activity.UnLoadOrderNames.Add(order.Optionals["ContainerNo"] + ",," + order.Optionals["OrderCode"]);

                                            DTOOPTLocationActivity objLoad = new DTOOPTLocationActivity();
                                            objLoad.ID = Convert.ToInt32(order.ID);
                                            objLoad.IsLoad = false;
                                            objLoad.OrderCode = order.Optionals["OrderCode"];
                                            objLoad.ContainerNo = order.Optionals["ContainerNo"];
                                            objLoad.SealNo1 = order.Optionals["SealNo1"];
                                            objLoad.SealNo2 = order.Optionals["SealNo2"];
                                            objLoad.PackingName = order.Optionals["PackingName"];
                                            objLoad.StatusOfCOContainerName = order.Optionals["StatusOfCOContainerName"];
                                            objLoad.ETD = cObj.ActivityTime.EarliestDateTime;
                                            objLoad.ETA = cObj.ActivityTime.LatestDateTime;
                                            objLoad.Ton = order.OrderSize.Weight > 0 ? order.OrderSize.Weight.Value : 0;

                                            dicActivity[activity.AtLocationID.Value].Add(objLoad);
                                        }
                                    }
                                    else if (type == typeof(Opt_DataOutput.UnloadAndLoadActivity))
                                    {
                                        var cObj = (Opt_DataOutput.UnloadAndLoadActivity)o;
                                        activity.AtLocationID = Convert.ToInt32(cObj.AtLocation.Optionals["CATLocationID"]);
                                        if (!dicActivity.ContainsKey(activity.AtLocationID.Value))
                                        {
                                            dicActivity.Add(activity.AtLocationID.Value, new List<DTOOPTLocationActivity>());
                                        }
                                        activity.UnLoadOrderNames = new List<string>();
                                        activity.LoadOrderNames = new List<string>();
                                        foreach (var order in cObj.LoadOrders)
                                        {
                                            activity.LoadOrderNames.Add(order.Optionals["ContainerNo"] + ",," + order.Optionals["OrderCode"]);

                                            DTOOPTLocationActivity objLoad = new DTOOPTLocationActivity();
                                            objLoad.ID = Convert.ToInt32(order.ID);
                                            objLoad.IsLoad = true;
                                            objLoad.OrderCode = order.Optionals["OrderCode"];
                                            objLoad.ContainerNo = order.Optionals["ContainerNo"];
                                            objLoad.SealNo1 = order.Optionals["SealNo1"];
                                            objLoad.SealNo2 = order.Optionals["SealNo2"];
                                            objLoad.PackingName = order.Optionals["PackingName"];
                                            objLoad.StatusOfCOContainerName = order.Optionals["StatusOfCOContainerName"];
                                            objLoad.ETD = cObj.ActivityTime.EarliestDateTime;
                                            objLoad.ETA = cObj.ActivityTime.LatestDateTime;
                                            objLoad.Ton = order.OrderSize.Weight > 0 ? order.OrderSize.Weight.Value : 0;
                                        }
                                        foreach (var order in cObj.UnLoadOrders)
                                        {
                                            activity.UnLoadOrderNames.Add(order.Optionals["ContainerNo"] + ",," + order.Optionals["OrderCode"]);

                                            DTOOPTLocationActivity objLoad = new DTOOPTLocationActivity();
                                            objLoad.ID = Convert.ToInt32(order.ID);
                                            objLoad.IsLoad = false;
                                            objLoad.OrderCode = order.Optionals["OrderCode"];
                                            objLoad.ContainerNo = order.Optionals["ContainerNo"];
                                            objLoad.SealNo1 = order.Optionals["SealNo1"];
                                            objLoad.SealNo2 = order.Optionals["SealNo2"];
                                            objLoad.PackingName = order.Optionals["PackingName"];
                                            objLoad.StatusOfCOContainerName = order.Optionals["StatusOfCOContainerName"];
                                            objLoad.ETD = cObj.ActivityTime.EarliestDateTime;
                                            objLoad.ETA = cObj.ActivityTime.LatestDateTime;
                                            objLoad.Ton = order.OrderSize.Weight > 0 ? order.OrderSize.Weight.Value : 0;
                                        }
                                    }
                                    else if (type == typeof(Opt_DataOutput.WaitActivity))
                                    {

                                    }
                                    else
                                    {

                                    }

                                    obj.ListActivity.Add(activity);
                                }
                                foreach (var o in obj.ListActivity)
                                {
                                    if (o.AtLocationID > 0 && obj.ListLocation.Count(c => c.ID == o.AtLocationID) == 0)
                                    {
                                        var oLocation = input.Locations.FirstOrDefault(c => c.Value.Optionals["CATLocationID"] == o.AtLocationID.Value.ToString()).Value;
                                        if (oLocation != null)
                                        {
                                            DTOOPTTripLocation x = new DTOOPTTripLocation();
                                            x.ID = Convert.ToInt32(oLocation.Optionals["CATLocationID"]);
                                            x.Lat = oLocation.Latitude;
                                            x.Lng = oLocation.Longitude;
                                            x.Location = oLocation.LocationName;
                                            x.Address = oLocation.Optionals["Address"];
                                            x.Code = oLocation.Optionals["Code"];
                                            x.ListActivity = dicActivity.ContainsKey(x.ID) ? dicActivity[x.ID] : new List<DTOOPTLocationActivity>();
                                            obj.ListLocation.Add(x);
                                        }
                                    }

                                    if (o.FromLocationID > 0 && obj.ListLocation.Count(c => c.ID == o.FromLocationID) == 0)
                                    {
                                        var oLocation = input.Locations.FirstOrDefault(c => c.Value.Optionals["CATLocationID"] == o.FromLocationID.Value.ToString()).Value;
                                        if (oLocation != null)
                                        {
                                            DTOOPTTripLocation x = new DTOOPTTripLocation();
                                            x.ID = Convert.ToInt32(oLocation.Optionals["CATLocationID"]);
                                            x.Lat = oLocation.Latitude;
                                            x.Lng = oLocation.Longitude;
                                            x.Location = oLocation.LocationName;
                                            x.Address = oLocation.Optionals["Address"];
                                            x.Code = oLocation.Optionals["Code"];
                                            x.ListActivity = dicActivity.ContainsKey(x.ID) ? dicActivity[x.ID] : new List<DTOOPTLocationActivity>();
                                            obj.ListLocation.Add(x);
                                        }
                                    }

                                    if (o.ToLocationID > 0 && obj.ListLocation.Count(c => c.ID == o.ToLocationID) == 0)
                                    {
                                        var oLocation = input.Locations.FirstOrDefault(c => c.Value.Optionals["CATLocationID"] == o.ToLocationID.Value.ToString()).Value;
                                        if (oLocation != null)
                                        {
                                            DTOOPTTripLocation x = new DTOOPTTripLocation();
                                            x.ID = Convert.ToInt32(oLocation.Optionals["CATLocationID"]);
                                            x.Lat = oLocation.Latitude;
                                            x.Lng = oLocation.Longitude;
                                            x.Location = oLocation.LocationName;
                                            x.Address = oLocation.Optionals["Address"];
                                            x.Code = oLocation.Optionals["Code"];
                                            x.ListActivity = dicActivity.ContainsKey(x.ID) ? dicActivity[x.ID] : new List<DTOOPTLocationActivity>();
                                            obj.ListLocation.Add(x);
                                        }
                                    }
                                }
                                data.Add(obj);
                            }
                            catch (Exception) { }
                        }
                    }
                }

                var query = data.ToDataSourceResult(CreateRequest(request));
                result.Total = query.Total;
                result.Data = query.Data as IEnumerable<DTOOPTVehicleSchedule>;
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

        public DTOResult Opt_DITOMaster_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTDITOMaster.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTDITOMaster
                    {
                        ID = c.ID,
                        HasChanged = c.ModifiedBy != null,
                        OptimizerID = c.OptimizerID,
                        VehicleID = c.VehicleID,
                        VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                        VendorOfVehicleID = c.VendorOfVehicleID,
                        VendorOfVehicleName = c.VendorOfVehicleID > 0 ? c.CUS_Customer.ShortName : "Xe nhà",
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverCard1 = c.DriverCard1,
                        DriverCard2 = c.DriverCard2,
                        DriverName1 = c.DriverName1,
                        DriverName2 = c.DriverName2,
                        DriverTel1 = c.DriverTel1,
                        DriverTel2 = c.DriverTel2,
                        AssistantID1 = c.AssistantID1,
                        AssistantID2 = c.AssistantID2,
                        AssistantCard1 = c.AssistantCard1,
                        AssistantCard2 = c.AssistantCard2,
                        AssistantName1 = c.AssistantName1,
                        AssistantName2 = c.AssistantName2,
                        AssistantTel1 = c.AssistantTel1,
                        AssistantTel2 = c.AssistantTel2,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        ETA = c.ETA,
                        ETD = c.ETD,
                        Note = c.Note,
                        KM = c.KM > 0 ? c.KM.Value : 0,
                        Credit = c.Credit,
                        Debit = c.Debit,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTDITOMaster>;
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

        public DTOResult Opt_DITOMaster_GroupProduct_List(string request, int masterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTDITOGroupProduct.Where(c => c.OPTDITOMasterID == masterID).Select(c => new DTOOPTDITOGroupOfProduct
                    {
                        ID = c.ID,
                        ETA = c.ETA,
                        ETD = c.ETD,
                        IsGopSplit = model.OPS_OPTDITOGroupProduct.Count(o => o.OPTOPSGroupProductID == c.OPTOPSGroupProductID && o.ID != c.ID) > 0,
                        ETAStart = c.ETAStart,
                        ETDStart = c.ETDStart,
                        CreateSortOrder = 0,
                        GroupProductCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                        GroupProductName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                        OrderCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                        CustomerCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        CBM = c.CBM,
                        Quantity = c.Quantity,
                        Ton = c.Ton,
                        Note = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note,
                        Note1 = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note1,
                        Note2 = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note2,
                        DNCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.DNCode,
                        PartnerCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.PartnerCode,
                        PartnerName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName,
                        LocationFromName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationName,
                        LocationFromAddress = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                        LocationFromDistrict = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_District.DistrictName,
                        LocationFromProvince = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_Province.ProvinceName,
                        LocationToName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationName,
                        LocationToAddress = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        LocationToDistrict = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                        LocationToProvince = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                        PlanNote = c.OPS_OPTOPSGroupProduct.Note
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTDITOGroupOfProduct>;
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

        public DTOResult Opt_DITOGroupProduct_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTDITOGroupProduct.Where(c => c.OPS_OPTOPSGroupProduct.OptimizerID == optimizerID).Select(c => new DTOOPTDITOGroupOfProduct
                    {
                        ID = c.ID,
                        ETA = c.ETA,
                        OPTDITOMasterID = c.OPTDITOMasterID,
                        IsGopSplit = model.OPS_OPTDITOGroupProduct.Count(o => o.OPTOPSGroupProductID == c.OPTOPSGroupProductID && o.ID != c.ID) > 0,
                        OPTDITOMasterName = c.OPTDITOMasterID > 0 ? c.OPS_OPTDITOMaster.Note : string.Empty,
                        VehicleNo = c.OPTDITOMasterID > 0 ? c.OPS_OPTDITOMaster.CAT_Vehicle.RegNo : string.Empty,
                        ETD = c.ETD,
                        ETAStart = c.ETAStart,
                        ETDStart = c.ETDStart,
                        CreateSortOrder = 0,
                        GroupProductCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                        GroupProductName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                        OrderCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                        CustomerCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        CBM = c.CBM,
                        Quantity = c.Quantity,
                        Ton = c.Ton,
                        Note = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note,
                        Note1 = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note1,
                        Note2 = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note2,
                        DNCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.DNCode,
                        PartnerCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.PartnerCode,
                        PartnerName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName,
                        LocationFromName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationName,
                        LocationFromAddress = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                        LocationFromDistrict = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_District.DistrictName,
                        LocationFromProvince = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_Province.ProvinceName,
                        LocationToName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationName,
                        LocationToAddress = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        LocationToDistrict = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                        LocationToProvince = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                        PlanNote = c.OPS_OPTOPSGroupProduct.Note
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTDITOGroupOfProduct>;
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

        public DTOResult Opt_DITOLocation_List(int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTDITOLocation.Where(c => c.OPS_OPTDITOMaster.OptimizerID == optimizerID).GroupBy(c => c.OPTDITOMasterID).Select(c => new DTOOPTTripDetail
                    {
                        ID = c.Key,
                        TripNo = c.FirstOrDefault().OPS_OPTDITOMaster.Note,
                        VehicleNo = c.FirstOrDefault().OPS_OPTDITOMaster.CAT_Vehicle.RegNo,
                        ListLocation = c.Where(o => o.SortOrder > 0).OrderBy(o => o.SortOrder).Select(o => new DTOOPTTripLocation
                        {
                            ID = o.LocationID.Value,
                            Location = o.CAT_Location.Location,
                            Address = o.CAT_Location.Address,
                            DateCome = o.DateCome,
                            DateLeave = o.DateLeave,
                            Lat = o.CAT_Location.Lat,
                            Lng = o.CAT_Location.Lng
                        }).ToList()
                    }).ToList();
                    result.Total = query.Count;
                    result.Data = query as IEnumerable<DTOOPTTripDetail>;
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

        //2View
        public DTOResult Opt_2ViewCO_Master_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTCOTOMaster.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTCOTOMaster
                    {
                        ID = c.ID,
                        HasChanged = false,
                        OptimizerID = c.OptimizerID,
                        VehicleID = c.VehicleID,
                        VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                        VendorOfVehicleID = c.VendorOfVehicleID,
                        VendorOfVehicleName = c.VendorOfVehicleID > 0 ? c.CUS_Customer1.ShortName : "Xe nhà",
                        RomoocID = c.RomoocID,
                        RomoocNo = c.RomoocID > 0 ? c.CAT_Romooc.RegNo : string.Empty,
                        VendorOfRomoocID = c.VendorOfRomoocID,
                        VendorOfRomoocName = c.VendorOfRomoocID > 0 ? c.CUS_Customer.ShortName : "Xe nhà",
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverCard1 = c.DriverCard1,
                        DriverCard2 = c.DriverCard2,
                        DriverName1 = c.DriverName1,
                        DriverName2 = c.DriverName2,
                        DriverTel1 = c.DriverTel1,
                        DriverTel2 = c.DriverTel2,
                        AssistantID1 = c.AssistantID1,
                        AssistantID2 = c.AssistantID2,
                        AssistantCard1 = c.AssistantCard1,
                        AssistantCard2 = c.AssistantCard2,
                        AssistantName1 = c.AssistantName1,
                        AssistantName2 = c.AssistantName2,
                        AssistantTel1 = c.AssistantTel1,
                        AssistantTel2 = c.AssistantTel2,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        ETA = c.ETA,
                        ETD = c.ETD,
                        Note = c.Note,
                        KM = c.KM > 0 ? c.KM.Value : 0,
                        TransportModeID = c.TransportModeID,
                        TypeOfOrderID = c.TypeOfOrderID,
                        ContractID = c.ContractID,
                        RoutingID = c.RoutingID,
                        Credit = c.Credit,
                        Debit = c.Debit,
                        DateConfig = c.DateConfig
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTCOTOMaster>;
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

        public DTOResult Opt_2ViewCO_Container_List(string request, int optimizerID, bool? hasMaster)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTCOTOContainer.Where(c => c.OPS_OPTOPSContainer.OptimizerID == optimizerID && (hasMaster.HasValue ? hasMaster == true ? c.OPTCOTOMasterID > 0 : c.OPTCOTOMasterID == null : true )).Select(c => new DTOOPTCOTOContainer
                    {
                        ID = c.ID,
                        OPTOPSContainerID = c.OPTOPSContainerID,
                        OPTCOTOMasterID = c.OPTCOTOMasterID,
                        OrderID = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code,
                        CustomerCode = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        ContainerNo = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ContainerNo,
                        ServiceOfOrderName = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.SYS_Var.ValueOfVar,
                        SealNo1 = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.SealNo1,
                        SealNo2 = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.SealNo2,
                        ETD = c.ETD,
                        ETA = c.ETA,
                        ETAStart = c.ETAStart,
                        ETDStart = c.ETDStart,
                        SortOrder = c.SortOrder,
                        LocationFromID = c.OPS_OPTOPSContainer.OPS_COTOContainer.LocationFromID,
                        LocationToID = c.OPS_OPTOPSContainer.OPS_COTOContainer.LocationToID,
                        LocationFromName = c.OPS_OPTOPSContainer.OPS_COTOContainer.CAT_Location.Location,
                        LocationToName = c.OPS_OPTOPSContainer.OPS_COTOContainer.CAT_Location1.Location,
                        StatusOfCOContainerID = c.StatusOfCOContainerID,
                        StatusOfCOContainerName = c.SYS_Var.ValueOfVar,
                        PackingID = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.PackingID,
                        PackingName = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.CAT_Packing.PackingName,
                        Ton = c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.Ton
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTCOTOContainer>;
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

        public void Opt_2ViewCO_SaveList(int optimizerID, List<DTOOPTCOTOMaster> dataMaster, List<DTOOPTCOTOContainer> dataContainer)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (obj != null)
                    {
                        //Xóa dữ liệu chuyến không có container.
                        List<int> sValue = new List<int>();
                        foreach (var item in dataMaster)
                        {
                            var dataCo = dataContainer.Where(c => c.CreateSortOrder == item.CreateSortOrder).ToList();
                            if (dataCo.Count == 0)
                            {
                                var objMaster = model.OPS_OPTCOTOMaster.FirstOrDefault(c => c.ID == item.ID);
                                if (objMaster != null)
                                {
                                    sValue.Add(item.ID);
                                    foreach (var o in model.OPS_OPTCOTODetail.Where(c => c.OPS_OPTCOTOContainer.OPTCOTOMasterID == item.ID && c.OPS_OPTCOTOLocation.OPTCOTOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTCOTODetail.Remove(o);
                                    }
                                    foreach (var o in model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == item.ID).ToList())
                                    {
                                        o.ModifiedBy = Account.UserName;
                                        o.ModifiedDate = DateTime.Now;
                                        o.OPTCOTOMasterID = null;
                                    }
                                    foreach (var o in model.OPS_OPTCOTOLocation.Where(c => c.OPTCOTOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTCOTOLocation.Remove(o);
                                    }
                                    model.OPS_OPTCOTOMaster.Remove(objMaster);
                                }
                            }
                        }
                        model.SaveChanges();

                        int sLocation = 1;
                        int eLocation = 1;
                        var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                        if (!string.IsNullOrEmpty(sSet))
                        {
                            var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                            if (oSet != null)
                            {
                                var obj1 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationFromID);
                                if (obj1 != null)
                                    sLocation = obj1.ID;
                                var obj2 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationToID);
                                if (obj2 != null)
                                    eLocation = obj2.ID;
                            }
                        }

                        //Lưu thông tin các chuyến còn lại.
                        foreach (var item in dataMaster.Where(c=> !sValue.Contains(c.ID)).ToList())
                        {
                            var dataCo = dataContainer.Where(c => c.CreateSortOrder == item.CreateSortOrder).ToList();
                            if (dataCo.Count != 0)
                            {
                                var objMaster = model.OPS_OPTCOTOMaster.FirstOrDefault(c => c.ID == item.ID);
                                if (objMaster != null)
                                {
                                    foreach (var o in model.OPS_OPTCOTODetail.Where(c => c.OPS_OPTCOTOContainer.OPTCOTOMasterID == item.ID && c.OPS_OPTCOTOLocation.OPTCOTOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTCOTODetail.Remove(o);
                                    }
                                    foreach (var o in model.OPS_OPTCOTOLocation.Where(c => c.OPTCOTOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTCOTOLocation.Remove(o);
                                    }
                                    objMaster.ModifiedBy = Account.UserName;
                                    objMaster.ModifiedDate = DateTime.Now;
                                }
                                else
                                {
                                    objMaster = new OPS_OPTCOTOMaster();
                                    objMaster.CreatedBy = Account.UserName;
                                    objMaster.CreatedDate = DateTime.Now;
                                    objMaster.ModifiedBy = Account.UserName;
                                    objMaster.ModifiedDate = DateTime.Now;
                                    model.OPS_OPTCOTOMaster.Add(objMaster);

                                    objMaster.OptimizerID = optimizerID;
                                    objMaster.SortOrder = 1;
                                    objMaster.KM = 0;
                                    objMaster.Debit = 0;
                                    objMaster.Note = "Chuyến " + item.CreateSortOrder;
                                }
                                objMaster.ETD = item.ETD;
                                objMaster.ETA = item.ETA;
                                COTOMaster_Create(model, optimizerID, item.VehicleID, item.RomoocID, objMaster);
                                model.SaveChanges();

                                List<int> dataLocation = new List<int>();
                                dataLocation.Add(sLocation);
                                dataLocation.Add(eLocation);
                                foreach (var con in dataCo)
                                {
                                    var objCo = model.OPS_OPTCOTOContainer.FirstOrDefault(c => c.ID == con.ID);
                                    if (objCo != null)
                                    {
                                        objCo.ModifiedBy = Account.UserName;
                                        objCo.ModifiedDate = DateTime.Now;
                                        objCo.OPTCOTOMasterID = objMaster.ID;
                                        dataLocation.Add(objCo.LocationFromID);
                                        dataLocation.Add(objCo.LocationToID);
                                    }
                                }

                                int idx = 1;
                                foreach (var o in dataLocation.Distinct().ToList())
                                {
                                    OPS_OPTCOTOLocation oLocation = new OPS_OPTCOTOLocation();
                                    oLocation.LocationID = o;
                                    oLocation.SortOrder = idx++;
                                    oLocation.OPTCOTOMasterID = objMaster.ID;
                                    oLocation.CreatedBy = Account.UserName;
                                    oLocation.CreatedDate = DateTime.Now;

                                    model.OPS_OPTCOTOLocation.Add(oLocation);
                                }
                                model.SaveChanges();
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

        public void Opt_2ViewCO_Delete(int optimizerID, List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (obj != null)
                    {
                        foreach (var item in data)
                        {
                            var objMaster = model.OPS_OPTCOTOMaster.FirstOrDefault(c => c.ID == item);
                            if (objMaster != null)
                            {
                                foreach (var o in model.OPS_OPTCOTODetail.Where(c => c.OPS_OPTCOTOContainer.OPTCOTOMasterID == item && c.OPS_OPTCOTOLocation.OPTCOTOMasterID == item).ToList())
                                {
                                    model.OPS_OPTCOTODetail.Remove(o);
                                }
                                foreach (var o in model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == item).ToList())
                                {
                                    o.ModifiedBy = Account.UserName;
                                    o.ModifiedDate = DateTime.Now;
                                    o.OPTCOTOMasterID = null;
                                }
                                foreach (var o in model.OPS_OPTCOTOLocation.Where(c => c.OPTCOTOMasterID == item).ToList())
                                {
                                    model.OPS_OPTCOTOLocation.Remove(o);
                                }
                                model.OPS_OPTCOTOMaster.Remove(objMaster);
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

        public DTOResult Opt_2ViewDI_Master_List(string request, int optimizerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTDITOMaster.Where(c => c.OptimizerID == optimizerID).Select(c => new DTOOPTDITOMaster
                    {
                        ID = c.ID,
                        HasChanged = false,
                        OptimizerID = c.OptimizerID,
                        VehicleID = c.VehicleID,
                        VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                        VendorOfVehicleID = c.VendorOfVehicleID,
                        VendorOfVehicleName = c.VendorOfVehicleID > 0 ? c.CUS_Customer.ShortName : "Xe nhà",
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverCard1 = c.DriverCard1,
                        DriverCard2 = c.DriverCard2,
                        DriverName1 = c.DriverName1,
                        DriverName2 = c.DriverName2,
                        DriverTel1 = c.DriverTel1,
                        DriverTel2 = c.DriverTel2,
                        AssistantID1 = c.AssistantID1,
                        AssistantID2 = c.AssistantID2,
                        AssistantCard1 = c.AssistantCard1,
                        AssistantCard2 = c.AssistantCard2,
                        AssistantName1 = c.AssistantName1,
                        AssistantName2 = c.AssistantName2,
                        AssistantTel1 = c.AssistantTel1,
                        AssistantTel2 = c.AssistantTel2,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        ETA = c.ETA,
                        ETD = c.ETD,
                        Note = c.Note,
                        KM = c.KM > 0 ? c.KM.Value : 0,
                        Credit = c.Credit,
                        Debit = c.Debit,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTDITOMaster>;
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

        public DTOResult Opt_2ViewDI_GroupProduct_List(string request, int optimizerID, bool? hasMaster)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_OPTDITOGroupProduct.Where(c => c.OPS_OPTOPSGroupProduct.OptimizerID == optimizerID && (hasMaster.HasValue ? hasMaster == true ? c.OPTDITOMasterID > 0 : c.OPTDITOMasterID == null : true)).Select(c => new DTOOPTDITOGroupOfProduct
                    {
                        ID = c.ID,
                        ETA = c.ETA,
                        IsGopSplit = model.OPS_OPTDITOGroupProduct.Count(o => o.OPTOPSGroupProductID == c.OPTOPSGroupProductID && o.ID != c.ID) > 0,
                        OPTDITOMasterID = c.OPTDITOMasterID,
                        ORDGroupProductID = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.OrderGroupProductID.Value,
                        OPTOPSGroupProductID = c.OPTOPSGroupProductID,
                        OPTDITOMasterName = c.OPTDITOMasterID > 0 ? c.OPS_OPTDITOMaster.Note : string.Empty,
                        VehicleNo = c.OPTDITOMasterID > 0 ? c.OPS_OPTDITOMaster.CAT_Vehicle.RegNo : string.Empty,
                        ETD = c.ETD,
                        ETAStart = c.ETAStart,
                        ETDStart = c.ETDStart,
                        CreateSortOrder = 0,
                        GroupProductCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                        GroupProductName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                        OrderCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                        CustomerCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        CBM = c.CBM,
                        Quantity = c.Quantity,
                        Ton = c.Ton,                        
                        Note = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note,
                        Note1 = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note1,
                        Note2 = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.Note2,
                        InvoiceNote = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.InvoiceNote,
                        DNCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.DNCode,
                        PartnerCode = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.PartnerCode,
                        PartnerName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName,
                        LocationFromName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationName,
                        LocationFromAddress = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                        LocationFromDistrict = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_District.DistrictName,
                        LocationFromProvince = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_Province.ProvinceName,
                        LocationToName = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationName,
                        LocationToAddress = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        LocationToDistrict = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                        LocationToProvince = c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                    }).ToDataSourceResult(CreateRequest(request));

                    foreach (var group in query.Data as IEnumerable<DTOOPTDITOGroupOfProduct>)
                    {
                        var product = model.ORD_Product.Where(c => c.GroupProductID == group.ORDGroupProductID)
                            .Select(c => new { c.CUS_Product.Code, c.CAT_Packing.TypeOfPackageID, c.ExchangeTon, c.ExchangeCBM, c.ExchangeQuantity }).FirstOrDefault();
                        if (product != null)
                        {
                            if (product.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                group.TypeEditID = 1;
                            if (product.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                group.TypeEditID = 2;
                            group.ExchangeTon = (product.ExchangeTon > 0) ? product.ExchangeTon.Value : 0;
                            group.ExchangeCBM = (product.ExchangeCBM > 0) ? product.ExchangeCBM.Value : 0;
                            if (group.TypeEditID == 3)
                                group.ExchangeQuantity = 1;
                        }
                    }

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOOPTDITOGroupOfProduct>;
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

        public void Opt_2ViewDI_SaveList(int optimizerID, List<DTOOPTDITOMaster> dataMaster, List<DTOOPTDITOGroupOfProduct> dataGroupProduct)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (obj != null)
                    {
                        //Xóa dữ liệu chuyến không có nhóm sản phẩm.
                        List<int> sValue = new List<int>();
                        foreach (var item in dataMaster)
                        {
                            var dataDI = dataGroupProduct.Where(c => c.CreateSortOrder == item.CreateSortOrder).ToList();
                            if (dataDI.Count == 0)
                            {
                                var objMaster = model.OPS_OPTDITOMaster.FirstOrDefault(c => c.ID == item.ID);
                                if (objMaster != null)
                                {
                                    sValue.Add(item.ID);
                                    foreach (var o in model.OPS_OPTDITODetail.Where(c => c.OPS_OPTDITOGroupProduct.OPTDITOMasterID == item.ID && c.OPS_OPTDITOLocation.OPTDITOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTDITODetail.Remove(o);
                                    }
                                    foreach (var o in model.OPS_OPTDITOGroupProduct.Where(c => c.OPTDITOMasterID == item.ID).ToList())
                                    {
                                        o.ModifiedBy = Account.UserName;
                                        o.ModifiedDate = DateTime.Now;
                                        o.OPTDITOMasterID = null;
                                    }
                                    foreach (var o in model.OPS_OPTDITOLocation.Where(c => c.OPTDITOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTDITOLocation.Remove(o);
                                    }
                                    model.OPS_OPTDITOMaster.Remove(objMaster);
                                }
                            }
                        }
                        model.SaveChanges();

                        int sLocation = 1;
                        int eLocation = 1;
                        var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                        if (!string.IsNullOrEmpty(sSet))
                        {
                            var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                            if (oSet != null)
                            {
                                var obj1 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationFromID);
                                if (obj1 != null)
                                    sLocation = obj1.ID;
                                var obj2 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationToID);
                                if (obj2 != null)
                                    eLocation = obj2.ID;
                            }
                        }

                        //Lưu thông tin các chuyến còn lại.
                        foreach (var item in dataMaster.Where(c => !sValue.Contains(c.ID)).ToList())
                        {
                            var dataDI = dataGroupProduct.Where(c => c.CreateSortOrder == item.CreateSortOrder).ToList();
                            if (dataDI.Count != 0)
                            {
                                var objMaster = model.OPS_OPTDITOMaster.FirstOrDefault(c => c.ID == item.ID);
                                if (objMaster != null)
                                {
                                    foreach (var o in model.OPS_OPTDITODetail.Where(c => c.OPS_OPTDITOGroupProduct.OPTDITOMasterID == item.ID && c.OPS_OPTDITOLocation.OPTDITOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTDITODetail.Remove(o);
                                    }
                                    foreach (var o in model.OPS_OPTDITOLocation.Where(c => c.OPTDITOMasterID == item.ID).ToList())
                                    {
                                        model.OPS_OPTDITOLocation.Remove(o);
                                    }
                                    objMaster.ModifiedBy = Account.UserName;
                                    objMaster.ModifiedDate = DateTime.Now;
                                }
                                else
                                {
                                    objMaster = new OPS_OPTDITOMaster();
                                    objMaster.CreatedBy = Account.UserName;
                                    objMaster.CreatedDate = DateTime.Now;
                                    objMaster.ModifiedBy = Account.UserName;
                                    objMaster.ModifiedDate = DateTime.Now;
                                    model.OPS_OPTDITOMaster.Add(objMaster);

                                    objMaster.OptimizerID = optimizerID;
                                    objMaster.SortOrder = 1;
                                    objMaster.KM = 0;
                                    objMaster.Debit = 0;
                                    objMaster.Note = "Chuyến " + item.CreateSortOrder;
                                }
                                objMaster.ETD = item.ETD;
                                objMaster.ETA = item.ETA;
                                DITOMaster_Create(model, optimizerID, item.VehicleID, objMaster);
                                model.SaveChanges();

                                List<int> dataLocation = new List<int>();
                                dataLocation.Add(sLocation);
                                dataLocation.Add(eLocation);
                                foreach (var gop in dataDI)
                                {
                                    var objGop = model.OPS_OPTDITOGroupProduct.FirstOrDefault(c => c.ID == gop.ID);
                                    if (objGop != null)
                                    {
                                        if (gop.IsSplit && (gop.Ton != objGop.Ton || gop.CBM != objGop.CBM || gop.Quantity != objGop.Quantity))
                                        {
                                            var objNew = new OPS_OPTDITOGroupProduct();
                                            objNew.CreatedBy = Account.UserName;
                                            objNew.CreatedDate = DateTime.Now;
                                            objNew.OPTDITOMasterID = objMaster.ID;
                                            objNew.OPTOPSGroupProductID = objGop.OPTOPSGroupProductID;
                                            objNew.Ton = gop.Ton;
                                            objNew.CBM = gop.CBM;
                                            objNew.Quantity = gop.Quantity;
                                            model.OPS_OPTDITOGroupProduct.Add(objNew);

                                            objGop.ModifiedBy = Account.UserName;
                                            objGop.ModifiedDate = DateTime.Now;
                                            objGop.Ton = Math.Round(objGop.Ton - objNew.Ton, NoDigit, MidpointRounding.AwayFromZero);
                                            objGop.CBM = Math.Round(objGop.CBM - objNew.CBM, NoDigit, MidpointRounding.AwayFromZero);
                                            objGop.Quantity = Math.Round(objGop.Quantity - objNew.Quantity, NoDigit, MidpointRounding.AwayFromZero);
                                            model.SaveChanges();
                                        }
                                        else
                                        {
                                            objGop.ModifiedBy = Account.UserName;
                                            objGop.ModifiedDate = DateTime.Now;
                                            objGop.OPTDITOMasterID = objMaster.ID;
                                        }
                                        dataLocation.Add(objGop.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationID);
                                        dataLocation.Add(objGop.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationID);
                                    }
                                }

                                int idx = 1;
                                foreach (var o in dataLocation.Distinct().ToList())
                                {
                                    OPS_OPTDITOLocation oLocation = new OPS_OPTDITOLocation();
                                    oLocation.LocationID = o;
                                    oLocation.SortOrder = idx++;
                                    oLocation.OPTDITOMasterID = objMaster.ID;
                                    oLocation.CreatedBy = Account.UserName;
                                    oLocation.CreatedDate = DateTime.Now;

                                    model.OPS_OPTDITOLocation.Add(oLocation);
                                }
                                model.SaveChanges();
                            }
                        }
                        
                        DITOGroupOfProduct_Reset(model, optimizerID);
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

        public void Opt_2ViewDI_Delete(int optimizerID, List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (obj != null)
                    {
                        foreach (var item in data)
                        {
                            var objMaster = model.OPS_OPTDITOMaster.FirstOrDefault(c => c.ID == item);
                            if (objMaster != null)
                            {
                                foreach (var o in model.OPS_OPTDITODetail.Where(c => c.OPS_OPTDITOGroupProduct.OPTDITOMasterID == item && c.OPS_OPTDITOLocation.OPTDITOMasterID == item).ToList())
                                {
                                    model.OPS_OPTDITODetail.Remove(o);
                                }
                                foreach (var o in model.OPS_OPTDITOGroupProduct.Where(c => c.OPTDITOMasterID == item).ToList())
                                {
                                    o.ModifiedBy = Account.UserName;
                                    o.ModifiedDate = DateTime.Now;
                                    o.OPTDITOMasterID = null;
                                }
                                foreach (var o in model.OPS_OPTDITOLocation.Where(c => c.OPTDITOMasterID == item).ToList())
                                {
                                    model.OPS_OPTDITOLocation.Remove(o);
                                }
                                model.OPS_OPTDITOMaster.Remove(objMaster);
                            }
                        }
                        model.SaveChanges();

                        DITOGroupOfProduct_Reset(model, optimizerID);
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

        public DTOOPTJsonData Opt_Optimizer_GetDataRun(int optimizerID)
        {
            try
            {
                DTOOPTJsonData result = new DTOOPTJsonData();
                using (var model = new DataEntities())
                {
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (obj != null && !string.IsNullOrEmpty(obj.DataRun))
                    {
                        DTOOPTDataJson item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTDataJson>(obj.DataRun, _jsonSetting);
                        if (item != null)
                        {
                            result.ID = optimizerID;
                            result.Name = obj.OptimizerName;

                            result.Locations = item.Locations;
                            result.LocationToLocationMatrix = item.LocationToLocationMatrix;
                            result.Orders = item.Orders;
                            result.Rates = item.Rates;
                            result.Vehicles = item.Vehicles;
                            result.VehicleTypes = item.VehicleTypes;
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
        
        public string Opt_Optimizer_Run_Check_Setting()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    int sLocation = 1, eLocation = 1;
                    var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                    if (!string.IsNullOrEmpty(sSet))
                    {
                        var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                        if (oSet != null)
                        {
                            sLocation = oSet.LocationFromID;
                            eLocation = oSet.LocationToID;
                            if (sLocation < 1 || model.CAT_Location.Count(c=> c.ID == sLocation) == 0)
                            {
                                return "Chưa có dữ liệu điểm bắt đầu (Quản trị hệ thống).";
                            }
                            if (eLocation < 1 || model.CAT_Location.Count(c => c.ID == eLocation) == 0)
                            {
                                return "Chưa có dữ liệu điểm Kết thúc (Quản trị hệ thống).";
                            }
                            foreach (var item in model.CAT_Location.Where(c => c.ID == sLocation || c.ID == eLocation).ToList())
                            {
                                if (!item.Lat.HasValue || !item.Lng.HasValue)
                                {
                                    return "Điểm '" + item.Location + "' chưa có dữ liệu tọa độ (Quản trị hệ thống).";
                                }
                            }
                            return string.Empty;
                        }
                        else
                        {
                            return "Chưa thiết lập điểm bắt đầu, kết thúc (Quản trị hệ thống).";
                        }
                    }
                    else
                    {
                        return "Chưa thiết lập điểm bắt đầu, kết thúc (Quản trị hệ thống).";
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

        public string Opt_Optimizer_Run_Check_Vehicle(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID && c.SYSCustomerID == Account.SYSCustomerID);
                    if (obj != null)
                    {
                        if (model.OPS_OPTOPSContainer.Count(c => c.OptimizerID == optimizerID) == 0)
                            return "Không có dữ liệu container.";
                        if (model.OPS_OPTVehicle.Count(c => c.OptimizerID == optimizerID) == 0)
                            return "Không có dữ liệu đầu kéo.";
                        if (model.OPS_OPTRomooc.Count(c => c.OptimizerID == optimizerID) == 0)
                            return "Không có dữ liệu romooc.";
                        var vehicleEmpty = model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID && c.RomoocID == null).Select(c => c.CAT_Vehicle.RegNo).ToList();
                        if (vehicleEmpty.Count > 0)
                            return "Đầu kéo " + string.Join(", ", vehicleEmpty) + " chưa chọn romooc.";
                        var locationEmpty = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID && (!c.CAT_Location.Lat.HasValue || !c.CAT_Location.Lng.HasValue)).Select(c => c.CAT_Location.Location).ToList();
                        if (locationEmpty.Count > 0)
                            return "Điểm " + string.Join(", ", locationEmpty) + " chưa thiết lập tọa độ.";
                    }
                    else
                    {
                        return "Không có dữ liệu tối ưu.";
                    }
                    return string.Empty;
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

        public string Opt_Optimizer_Run_Check_Location(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID && c.SYSCustomerID == Account.SYSCustomerID);
                    if (obj != null)
                    {
                        var data = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID && (!c.CAT_Location.Lat.HasValue || !c.CAT_Location.Lng.HasValue)).Select(c => c.CAT_Location.Location).ToList();
                        if (data.Count > 0)
                            return "Điểm " + string.Join(", ", data) + " chưa thiết lập tọa độ.";
                    }
                    else
                    {
                        return "Không có dữ liệu tối ưu.";
                    }
                    return string.Empty;
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

        public List<DTOOPTLocationToLocation> Opt_Optimizer_Run_Get_LocationMatrix(int optimizerID)
        {
            try
            {
                var result = new List<DTOOPTLocationToLocation>();
                using (var model = new DataEntities())
                {
                    var objOpt = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (objOpt == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy optimizer!");

                    List<int> dataLocation = new List<int>();

                    int sLocation = 1, eLocation = 1;
                    var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                    if (!string.IsNullOrEmpty(sSet))
                    {
                        var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                        if (oSet != null)
                        {
                            sLocation = oSet.LocationFromID;
                            eLocation = oSet.LocationToID;
                            if (sLocation < 1 || model.CAT_Location.Count(c=> c.ID == sLocation) == 0)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Chưa có dữ liệu điểm bắt đầu (QT hệ thống).");
                            }
                            if (eLocation < 1 || model.CAT_Location.Count(c => c.ID == eLocation) == 0)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Chưa có dữ liệu điểm kết thúc (QT hệ thống).");
                            }
                            foreach (var item in model.CAT_Location.Where(c => c.ID == sLocation || c.ID == eLocation).ToList())
                            {
                                if (!item.Lat.HasValue || !item.Lng.HasValue)
                                {
                                    throw FaultHelper.BusinessFault(null, null, "Điểm '" + item.Location + "' chưa có dữ liệu tọa độ (QT hệ thống).");
                                }
                            }
                        }
                        else
                        {
                            throw FaultHelper.BusinessFault(null, null, "Chưa thiết lập điểm bắt đầu, kết thúc (QT hệ thống).");
                        }
                    }
                    else
                    {
                        throw FaultHelper.BusinessFault(null, null, "Chưa thiết lập điểm bắt đầu, kết thúc (QT hệ thống).");
                    }
                    dataLocation.Add(sLocation);
                    dataLocation.Add(eLocation);

                    var locationEmpty = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID && (!c.CAT_Location.Lat.HasValue || !c.CAT_Location.Lng.HasValue)).Select(c => c.CAT_Location.Location).ToList();
                    if (locationEmpty.Count > 0)
                        throw FaultHelper.BusinessFault(null, null, "Điểm " + string.Join(", ", locationEmpty) + " chưa thiết lập tọa độ.");

                    var data = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID).Select(c => c.LocationID).Distinct().ToList();
                    dataLocation.AddRange(data);
                    dataLocation = dataLocation.Distinct().ToList();
                    for (var i = 0; i < dataLocation.Count; i++)
                    {
                        var oneID = dataLocation[i];
                        var one = model.CAT_Location.FirstOrDefault(c => c.ID == oneID);
                        for (var j = 0; j < dataLocation.Count; j++)
                        {
                            if (i != j)
                            {
                                var twoID = dataLocation[j];
                                var two = model.CAT_Location.FirstOrDefault(c => c.ID == twoID);
                                var objDetail = model.OPS_OPTLocationMatrix.FirstOrDefault(c => c.LocationFromID == oneID && c.LocationToID == twoID);
                                if (objDetail == null)
                                {
                                    objDetail = new OPS_OPTLocationMatrix();
                                    objDetail.CreatedBy = Account.UserName;
                                    objDetail.CreatedDate = DateTime.Now;
                                    objDetail.OptimizerID = optimizerID;
                                    objDetail.LocationFromID = oneID;
                                    objDetail.LocationToID = twoID;
                                    var objCat = model.CAT_LocationMatrix.FirstOrDefault(c => c.LocationFromID == oneID && c.LocationToID == twoID);
                                    if (objCat != null)
                                    {
                                        if (objCat.Hour > 0 && objCat.KM > 0)
                                        {
                                            objDetail.KM = objCat.KM;
                                            objDetail.Hour = objCat.Hour;
                                        }
                                        else
                                        {
                                            double d = 0, h = 0;
                                            if (GetRoute_ViaOSM(model, oneID, twoID, ref h, ref d))
                                            {
                                                objDetail.KM = Math.Round(d / 1000, 2, MidpointRounding.AwayFromZero);
                                                objDetail.Hour = Math.Round(h / 3600, 2, MidpointRounding.AwayFromZero);
                                            }
                                            else
                                            {
                                                DTOOPTLocationToLocation objLocation = new DTOOPTLocationToLocation();
                                                objLocation.LocationFromID = oneID;
                                                objLocation.LocationToID = twoID;
                                                objLocation.LocationFromLat = one.Lat ?? 0;
                                                objLocation.LocationFromLng = one.Lng ?? 0;
                                                objLocation.LocationFromName = one.Location;
                                                objLocation.LocationFromAddress = one.Address;
                                                objLocation.LocationToLat = two.Lat ?? 0;
                                                objLocation.LocationToLng = two.Lng ?? 0;
                                                objLocation.LocationToName = two.Location;
                                                objLocation.LocationToAddress = two.Address;

                                                result.Add(objLocation);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        double d = 0, h = 0;
                                        if (GetRoute_ViaOSM(model, oneID, twoID, ref h, ref d))
                                        {
                                            objDetail.KM = Math.Round(d / 1000, 2, MidpointRounding.AwayFromZero);
                                            objDetail.Hour = Math.Round(h / 3600, 2, MidpointRounding.AwayFromZero);
                                        }
                                        else
                                        {
                                            DTOOPTLocationToLocation objLocation = new DTOOPTLocationToLocation();
                                            objLocation.LocationFromID = oneID;
                                            objLocation.LocationToID = twoID;
                                            objLocation.LocationFromLat = one.Lat ?? 0;
                                            objLocation.LocationFromLng = one.Lng ?? 0;
                                            objLocation.LocationFromName = one.Location;
                                            objLocation.LocationFromAddress = one.Address;
                                            objLocation.LocationToLat = two.Lat ?? 0;
                                            objLocation.LocationToLng = two.Lng ?? 0;
                                            objLocation.LocationToName = two.Location;
                                            objLocation.LocationToAddress = two.Address;

                                            result.Add(objLocation);
                                        }
                                    }
                                    model.OPS_OPTLocationMatrix.Add(objDetail);
                                    model.SaveChanges();
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
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void Opt_Optimizer_Run_Update_LocationMatrix(DTOOPTLocationToLocation item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.CAT_LocationMatrix.FirstOrDefault(c => c.LocationFromID == item.LocationFromID && c.LocationToID == item.LocationToID);
                    if (obj == null)
                    {
                        obj = new CAT_LocationMatrix();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.LocationFromID = item.LocationFromID;
                        obj.LocationToID = item.LocationToID;

                        model.CAT_LocationMatrix.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.KM = item.EDistance;
                    obj.Hour = item.EHours;

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


        public void Opt_Optimizer_Run(int optimizerID, int typeOfOptimizer)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID && c.SYSCustomerID == Account.SYSCustomerID);
                    if (obj != null)
                    {
                        if (obj.IsContainer)
                        {
                            COOptimize_Run(model, obj.ID);
                        }
                        else
                        {
                            DIOptimize_Run(model, obj.ID);
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

        public void Opt_Optimizer_Cal(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID && c.SYSCustomerID == Account.SYSCustomerID);
                    if (obj != null)
                    {
                        if (obj.IsContainer)
                        {
                            try
                            {
                                DTOOPTData objSave = GetJsonDataSet(model, optimizerID);
                                if (objSave.Input != null)
                                {
                                    var optimizer = new Opt_Plan.TransportationPlanOptimizer(objSave.Input);
                                    optimizer.SetDictionaryOfCoinMpDll(System.Configuration.ConfigurationSettings.AppSettings["CoinMpDll"]);
                                    var output = optimizer.Run();
                                    objSave.Status = 11;
                                    objSave.Output = output;
                                    SaveJsonDataSet(model, optimizerID, objSave);
                                }                                
                            }
                            catch (Exception ex)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Có lỗi trong quá trình xử lý tối ưu (2): " + ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                DTOOPTData objSave = GetJsonDataSet(model, optimizerID);
                                if (objSave.Input != null)
                                {
                                    var optimizer = new Opt_Plan.TransportationPlanOptimizer(objSave.Input);
                                    var output = optimizer.Run();
                                    objSave.Status = 11;
                                    objSave.Output = output;
                                    SaveJsonDataSet(model, optimizerID, objSave);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Có lỗi trong quá trình xử lý tối ưu (2): " + ex.Message);
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

        public void Opt_Optimizer_Out(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID && c.SYSCustomerID == Account.SYSCustomerID);
                    if (obj != null)
                    {
                        if (obj.IsContainer)
                        {
                            try
                            {
                                DTOOPTData objSave = GetJsonDataSet(model, optimizerID);
                                if (objSave.Input != null && objSave.Output != null)
                                {
                                    CO_ConvertData(model, optimizerID,  objSave.Input, objSave.Output);
                                    objSave.Status = 12;
                                    objSave.Output = null;
                                    SaveJsonDataSet(model, optimizerID, objSave);
                                }                                
                            }
                            catch (Exception ex)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Có lỗi trong quá trình xử lý dữ liệu đầu ra (3): " + ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                DTOOPTData objSave = GetJsonDataSet(model, optimizerID);
                                if (objSave.Input != null && objSave.Output != null)
                                {
                                    DI_ConvertData(model, optimizerID, objSave.Input, objSave.Output);
                                    objSave.Status = 12;
                                    objSave.Output = null;
                                    SaveJsonDataSet(model, optimizerID, objSave);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Có lỗi trong quá trình xử lý dữ liệu đầu ra (3): " + ex.Message);
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

        public DTOOPTOptimizer Opt_Optimizer_Get(int optimizerID)
        {
            try
            {
                DTOOPTOptimizer result = new DTOOPTOptimizer();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID && c.ID == optimizerID);
                    if (obj != null)
                    {
                        result.ID = obj.ID;
                        result.OptimizerName = obj.OptimizerName;
                        result.StatusOfOptimizer = obj.IsSave ? 2 : obj.OPS_OPTOPSContainer.Count > 0 ? 1 : 0;
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

        public void Opt_Optimizer_Reset(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    Optimizer_Reset(model, optimizerID);
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

        public void Opt_Optimizer_Save(DTOOPTOptimizer item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var objCheck = model.OPS_Optimizer.FirstOrDefault(c => c.ID != item.ID && c.OptimizerName.Trim().ToLower() == item.OptimizerName.Trim().ToLower());
                    if (objCheck != null)
                        throw FaultHelper.BusinessFault(null, null, "Trùng tên.");

                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new OPS_Optimizer();
                        obj.CreatedDate = DateTime.Now;
                        obj.CreatedBy = Account.UserName;
                        obj.SYSCustomerID = Account.SYSCustomerID;

                        model.OPS_Optimizer.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = Account.UserName;
                    }

                    obj.IsBalanceKMScore = item.IsBalanceKMScore;
                    obj.IsBalanceCustomer = item.IsBalanceCustomer;
                    obj.OptimizerName = item.OptimizerName;
                    obj.DateFrom = item.DateFrom.Value.Date;
                    obj.DateTo = item.DateTo.Value.Date;
                    obj.IsContainer = item.IsContainer;
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

        public void Opt_Optimizer_Delete(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID && c.ID == optimizerID);
                    if (obj != null)
                    {
                        foreach (var item in model.OPS_OPTRomooc.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            model.OPS_OPTRomooc.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            model.OPS_OPTVehicle.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTOrder.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            model.OPS_OPTOrder.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTOPSContainer.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            model.OPS_OPTOPSContainer.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTLocationMatrix.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            model.OPS_OPTLocationMatrix.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            foreach (var required in model.OPS_OPTLocationRequire.Where(c => c.OPTLocationID == item.ID).ToList())
                            {
                                model.OPS_OPTLocationRequire.Remove(required);
                            }
                            model.OPS_OPTLocation.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTRouting.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            foreach (var required in model.OPS_OPTRoutingRequire.Where(c => c.OPTRoutingID == item.ID).ToList())
                            {
                                model.OPS_OPTRoutingRequire.Remove(required);
                            }
                            model.OPS_OPTRouting.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTOPSGroupProduct.Where(c => c.OptimizerID == optimizerID).ToList())
                        {
                            model.OPS_OPTOPSGroupProduct.Remove(item);
                        }

                        Optimizer_Reset(model, optimizerID);
                        model.OPS_Optimizer.Remove(obj);
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

        public void Opt_Vehicle_SaveList(int optimizerID, List<int> dataVehicle)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var objOptimizer = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (objOptimizer != null)
                    {
                        foreach (var item in dataVehicle)
                        {
                            var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == item);
                            if (objVehicle != null)
                            {
                                OPS_OPTVehicle obj = new OPS_OPTVehicle();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;

                                obj.OptimizerID = optimizerID;
                                obj.VehicleID = objVehicle.ID;
                                obj.RomoocID = objVehicle.CurrentRomoocID;
                                obj.MaxWeightCal = MaxWeight(model, obj.VehicleID, obj.RomoocID);

                                if (obj.RomoocID > 0)
                                {
                                    OPS_OPTRomooc objRomooc = new OPS_OPTRomooc();
                                    objRomooc.CreatedBy = Account.UserName;
                                    objRomooc.CreatedDate = DateTime.Now;

                                    objRomooc.OptimizerID = optimizerID;
                                    objRomooc.RomoocID = obj.RomoocID.Value;

                                    model.OPS_OPTRomooc.Add(objRomooc);
                                }

                                model.OPS_OPTVehicle.Add(obj);

                                foreach (var o in model.FLM_AssetRequire.Where(c => c.FLM_Asset.VehicleID == objVehicle.ID).ToList())
                                {
                                    OPS_OPTVehicleRequire objReq = new OPS_OPTVehicleRequire();
                                    objReq.CreatedBy = Account.UserName;
                                    objReq.CreatedDate = DateTime.Now;

                                    objReq.KMTo = o.KMTo;
                                    objReq.TimeTo = o.TimeTo;
                                    objReq.KMFrom = o.KMFrom;
                                    objReq.KMScore = o.KMScore;
                                    objReq.TimeFrom = o.TimeFrom;
                                    objReq.Width = o.Width;
                                    objReq.Length = o.Length;
                                    objReq.Height = o.Height;
                                    objReq.Weight = o.Weight;
                                    objReq.ConstraintRequireTypeID = o.ConstraintRequireTypeID;
                                    obj.OPS_OPTVehicleRequire.Add(objReq);
                                }
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

        public void Opt_Romooc_SaveList(int optimizerID, List<int> dataRomooc)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var objOptimizer = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (objOptimizer != null)
                    {
                        foreach (var item in dataRomooc)
                        {
                            var objRomooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == item);
                            if (objRomooc != null)
                            {
                                OPS_OPTRomooc obj = new OPS_OPTRomooc();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;

                                obj.OptimizerID = optimizerID;
                                obj.RomoocID = objRomooc.ID;

                                model.OPS_OPTRomooc.Add(obj);
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

        public void Opt_Vehicle_Update(int optVehicleID, int? romoocID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTVehicle.FirstOrDefault(c => c.ID == optVehicleID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        obj.RomoocID = romoocID;
                        obj.MaxWeightCal = MaxWeight(model, obj.VehicleID, obj.RomoocID);

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

        public void Opt_Vehicle_UpdateWeight(int optVehicleID, double maxWeight)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTVehicle.FirstOrDefault(c => c.ID == optVehicleID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.MaxWeightCal = maxWeight;
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

        public void Opt_Container_SaveList(int optimizerID, List<int> dataCon)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var objOpt = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (objOpt != null)
                    {
                        foreach (var id in dataCon)
                        {
                            var objCO = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == id);
                            if (objCO != null)
                            {
                                OPS_OPTOPSContainer obj = new OPS_OPTOPSContainer();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;

                                obj.COTOContainerID = id;
                                obj.ETA = objCO.ETA;
                                obj.ETD = objCO.ETD;
                                obj.ETAStart = objCO.ETAStart;
                                obj.ETDStart = objCO.ETDStart;
                                obj.OptimizerID = optimizerID;

                                model.OPS_OPTOPSContainer.Add(obj);
                            }
                        }

                        Location_Create(model, optimizerID, true, dataCon);
                        Order_Create(model, optimizerID, true, dataCon);

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

        public void Opt_Container_Remove(int optimizerID, List<int> dataCon)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    List<int> dataOrder = new List<int>();
                    //Xóa container.
                    foreach (var id in dataCon)
                    {
                        var obj = model.OPS_OPTOPSContainer.FirstOrDefault(c => c.ID == id);
                        if (obj != null)
                        {
                            var dataCO = model.OPS_OPTCOTOContainer.Where(c => c.OPTOPSContainerID == obj.ID).ToList();
                            if(dataCO.Count(c=> c.OPTCOTOMasterID > 0) > 0)
                                throw FaultHelper.BusinessFault(null, null, "Container ['" + obj.OPS_COTOContainer.OPS_Container.ContainerNo + "'-" + obj.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code + "] đã lập chuyến. Vui lòng xóa chuyến trước.");
                            foreach (var item in dataCO)
                            {
                                model.OPS_OPTCOTOContainer.Remove(item);
                            }
                            dataOrder.Add(obj.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID);
                            model.OPS_OPTOPSContainer.Remove(obj);
                        }
                    }
                    model.SaveChanges();

                    //Xóa đơn hàng
                    foreach (var id in dataOrder.Distinct().ToList())
                    {
                        if (model.OPS_OPTOPSContainer.Count(c => c.OptimizerID == optimizerID && c.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID == id) == 0)
                        {
                            var obj = model.OPS_OPTOrder.FirstOrDefault(c => c.OrderID == id && c.OptimizerID == optimizerID);
                            if (obj != null)
                                model.OPS_OPTOrder.Remove(obj);
                        }
                    }
                    model.SaveChanges();

                    Location_Reset(model, optimizerID, true);
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

        public void Opt_Container_Update(DTOOPSCOTOContainer item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTOPSContainer.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ETA = item.ETA;
                        obj.ETD = item.ETD;
                        obj.ETAStart = item.ETAStart;
                        obj.ETDStart = item.ETDStart;
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
        
        public void Opt_GroupOfProduct_SaveList(int optimizerID, List<int> dataGop)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var objOpt = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (objOpt != null)
                    {
                        foreach (var id in dataGop)
                        {
                            var objDI = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == id);
                            if (objDI != null)
                            {
                                if (objDI.ORD_GroupProduct.LocationToID == null || objDI.ORD_GroupProduct.LocationToID == null)
                                    throw FaultHelper.BusinessFault(null, null, "Nhóm [" + objDI.ORD_GroupProduct.CUS_GroupOfProduct.GroupName + "-" + objDI.ORD_GroupProduct.ORD_Order.Code + "] thiếu thông tin điểm nhận, giao hàng.");

                                OPS_OPTOPSGroupProduct obj = new OPS_OPTOPSGroupProduct();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;

                                obj.DITOGroupProductID = id;
                                obj.ETA = objDI.ORD_GroupProduct.ETA;
                                obj.ETD = objDI.ORD_GroupProduct.ETD;
                                obj.ETDStart = objDI.ORD_GroupProduct.ETDStart;
                                obj.ETAStart = objDI.ORD_GroupProduct.ETAStart;
                                obj.OptimizerID = optimizerID;

                                model.OPS_OPTOPSGroupProduct.Add(obj);
                            }
                        }

                        Location_Create(model, optimizerID, false, dataGop);
                        Order_Create(model, optimizerID, false, dataGop);

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

        public void Opt_GroupOfProduct_Remove(int optimizerID, List<int> dataGop)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    List<int> dataOrder = new List<int>();
                    //Xóa nhóm sản phẩm.
                    foreach (var id in dataGop)
                    {
                        var obj = model.OPS_OPTOPSGroupProduct.FirstOrDefault(c => c.ID == id);
                        if (obj != null)
                        {
                            var dataDI = model.OPS_OPTDITOGroupProduct.Where(c => c.OPTOPSGroupProductID == obj.ID).ToList();
                            if (dataDI.Count(c => c.OPTDITOMasterID > 0) > 0)
                                throw FaultHelper.BusinessFault(null, null, "Nhóm ['" + obj.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code + "'-" + obj.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code + "] đã lập chuyến. Vui lòng xóa chuyến trước.");
                            foreach (var item in dataDI)
                            {
                                model.OPS_OPTDITOGroupProduct.Remove(item);
                            }
                            dataOrder.Add(obj.OPS_DITOGroupProduct.ORD_GroupProduct.OrderID);
                            model.OPS_OPTOPSGroupProduct.Remove(obj);
                        }
                    }
                    model.SaveChanges();

                    //Xóa đơn hàng
                    foreach (var id in dataOrder.Distinct().ToList())
                    {
                        if (model.OPS_OPTOPSContainer.Count(c => c.OptimizerID == optimizerID && c.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID == id) == 0)
                        {
                            var obj = model.OPS_OPTOrder.FirstOrDefault(c => c.OrderID == id && c.OptimizerID == optimizerID);
                            if (obj != null)
                                model.OPS_OPTOrder.Remove(obj);
                        }
                    }
                    model.SaveChanges();

                    Location_Reset(model, optimizerID, false);
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

        public void Opt_GroupOfProduct_Update(DTOOPTOPSGroupProduct item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTOPSGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ETA = item.ETA;
                        obj.ETD = item.ETD;
                        obj.ETAStart = item.ETAStart;
                        obj.ETDStart = item.ETDStart;
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

        public void Opt_Romooc_Remove(int optimizerID, List<int> dataRomooc)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var id in dataRomooc)
                    {
                        var obj = model.OPS_OPTRomooc.FirstOrDefault(c => c.ID == id && c.OptimizerID == optimizerID);
                        if (obj != null)
                        {
                            if (model.OPS_OPTVehicle.Count(c => c.RomoocID == obj.RomoocID && c.OptimizerID == optimizerID) > 0)
                                throw FaultHelper.BusinessFault(null, null, "Romooc " + obj.CAT_Romooc.RegNo + " đã sử dụng. Không thể xóa.");
                            model.OPS_OPTRomooc.Remove(obj);
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

        public void Opt_Vehicle_Remove(int optimizerID, List<int> dataVehicle)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var id in dataVehicle)
                    {
                        var obj = model.OPS_OPTVehicle.FirstOrDefault(c => c.ID == id && c.OptimizerID == optimizerID);
                        if (obj != null)
                        {
                            if (obj.CAT_Vehicle.CurrentRomoocID > 0)
                            {
                                if (model.OPS_OPTVehicle.Count(c => c.OptimizerID == optimizerID && c.ID != id && c.RomoocID == obj.CAT_Vehicle.CurrentRomoocID) == 0)
                                {
                                    var objRomooc = model.OPS_OPTRomooc.FirstOrDefault(c => c.ID == obj.CAT_Vehicle.CurrentRomoocID);
                                    if (objRomooc != null)
                                        model.OPS_OPTRomooc.Remove(objRomooc);
                                }
                            }
                            foreach (var o in model.OPS_OPTVehicleRequire.Where(c => c.OPTVehicleID == obj.ID).ToList())
                                model.OPS_OPTVehicleRequire.Remove(o);
                            model.OPS_OPTVehicle.Remove(obj);
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

        public void Opt_Routing_Save(int optRoutingID, double time, double distance)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTRouting.FirstOrDefault(c => c.ID == optRoutingID);
                    if (obj != null)
                    {
                        obj.CAT_Routing.EDistance = distance;
                        obj.CAT_Routing.EHours = time;
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

        public void Opt_Location_Require_Save(int optLocationID, DTOOPTLocationRequire item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTLocationRequire.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new OPS_OPTLocationRequire();

                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.OPTLocationID = optLocationID;

                        model.OPS_OPTLocationRequire.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    if (item.IsSize)
                    {
                        if (item.Date != null)
                        {
                            obj.ConstraintRequireTypeID = CTSizeDay;
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                        }
                        else
                        {
                            obj.ConstraintRequireTypeID = CTSizeWeek;
                            item.Date = DateTime.Now;
                            var dayofweek = (int)item.Date.Value.DayOfWeek;
                            item.Date = item.Date.Value.AddDays(item.DayOfWeek - dayofweek);
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                        }
                        obj.Weight = item.Weight;
                        obj.Width = item.Width;
                        obj.Height = item.Height;
                        obj.Length = item.Length;
                    }
                    else
                    {
                        if (item.Date != null)
                        {
                            obj.ConstraintRequireTypeID = CTOpenDay;
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                            if (obj.TimeFrom > obj.TimeTo)
                                obj.ConstraintRequireTypeID = CTCloseDay;
                        }
                        else
                        {
                            obj.ConstraintRequireTypeID = -(int)SYSVarType.ConstraintRequireTypeOpenWeek;
                            item.Date = DateTime.Now;
                            var dayofweek = (int)item.Date.Value.DayOfWeek;
                            item.Date = item.Date.Value.AddDays(item.DayOfWeek - dayofweek);
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                        }
                        obj.Weight = null;
                        obj.Width = null;
                        obj.Height = null;
                        obj.Length = null;
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

        public void Opt_Location_Require_Reset(List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var optLocationID in data)
                    {
                        var obj = model.OPS_OPTLocation.FirstOrDefault(c => c.ID == optLocationID);
                        if (obj != null)
                        {
                            foreach (var item in model.OPS_OPTLocationRequire.Where(c => c.OPTLocationID == obj.ID).ToList())
                            {
                                model.OPS_OPTLocationRequire.Remove(item);
                            }
                            model.SaveChanges();
                            foreach (var item in model.CAT_LocationRequire.Where(c => c.LocationID == obj.LocationID).ToList())
                            {
                                OPS_OPTLocationRequire objR = new OPS_OPTLocationRequire();
                                objR.CreatedBy = Account.UserName;
                                objR.CreatedDate = DateTime.Now;
                                objR.OPTLocationID = optLocationID;
                                objR.ConstraintRequireTypeID = item.ConstraintRequireTypeID;
                                objR.Height = item.Height;
                                objR.Weight = item.Weight;
                                objR.Width = item.Width;
                                objR.Length = item.Length;
                                objR.TimeFrom = item.TimeFrom;
                                objR.TimeTo = item.TimeTo;
                                model.OPS_OPTLocationRequire.Add(objR);
                            }
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

        public void Opt_Routing_Require_Save(int optRoutingID, DTOOPTLocationRequire item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTRoutingRequire.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new OPS_OPTRoutingRequire();

                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.OPTRoutingID = optRoutingID;

                        model.OPS_OPTRoutingRequire.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    if (item.IsSize)
                    {
                        if (item.Date != null)
                        {
                            obj.ConstraintRequireTypeID = CTSizeDay;
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                        }
                        else
                        {
                            obj.ConstraintRequireTypeID = CTSizeWeek;
                            item.Date = DateTime.Now;
                            var dayofweek = (int)item.Date.Value.DayOfWeek;
                            item.Date = item.Date.Value.AddDays(item.DayOfWeek - dayofweek);
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                        }
                        obj.Weight = item.Weight;
                        obj.Width = item.Width;
                        obj.Height = item.Height;
                        obj.Length = item.Length;
                    }
                    else
                    {
                        if (item.Date != null)
                        {
                            obj.ConstraintRequireTypeID = CTOpenDay;
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                            if (obj.TimeFrom > obj.TimeTo)
                                obj.ConstraintRequireTypeID = CTCloseDay;
                        }
                        else
                        {
                            obj.ConstraintRequireTypeID = CTOpenWeek;
                            item.Date = DateTime.Now;
                            var dayofweek = (int)item.Date.Value.DayOfWeek;
                            item.Date = item.Date.Value.AddDays(item.DayOfWeek - dayofweek);
                            obj.TimeFrom = item.Date.Value.Date.Add(item.TimeFrom.Value.TimeOfDay);
                            obj.TimeTo = item.Date.Value.Date.Add(item.TimeTo.Value.TimeOfDay);
                        }
                        obj.Weight = null;
                        obj.Width = null;
                        obj.Height = null;
                        obj.Length = null;
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

        public void Opt_Routing_Require_Reset(List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var optRoutingID in data)
                    {
                        var obj = model.OPS_OPTRouting.FirstOrDefault(c => c.ID == optRoutingID);
                        if (obj != null)
                        {
                            foreach (var item in model.OPS_OPTRoutingRequire.Where(c => c.OPTRoutingID == obj.ID).ToList())
                            {
                                model.OPS_OPTRoutingRequire.Remove(item);
                            }
                            model.SaveChanges();
                            foreach (var item in model.CAT_RoutingRequire.Where(c => c.RoutingID == obj.RoutingID).ToList())
                            {
                                OPS_OPTRoutingRequire objR = new OPS_OPTRoutingRequire();
                                objR.CreatedBy = Account.UserName;
                                objR.CreatedDate = DateTime.Now;
                                objR.OPTRoutingID = optRoutingID;
                                objR.ConstraintRequireTypeID = item.ConstraintRequireTypeID;
                                objR.Height = item.Height;
                                objR.Weight = item.Weight;
                                objR.Width = item.Width;
                                objR.Length = item.Length;
                                objR.TimeFrom = item.TimeFrom;
                                objR.TimeTo = item.TimeTo;
                                model.OPS_OPTRoutingRequire.Add(objR);
                            }
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

        public void Opt_Location_Require_Remove(List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in data)
                    {
                        var obj = model.OPS_OPTLocationRequire.FirstOrDefault(c => c.ID == item);
                        if (obj != null)
                        {
                            model.OPS_OPTLocationRequire.Remove(obj);
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

        public void Opt_Routing_Require_Remove(List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in data)
                    {
                        var obj = model.OPS_OPTRoutingRequire.FirstOrDefault(c => c.ID == item);
                        if (obj != null)
                        {
                            model.OPS_OPTRoutingRequire.Remove(obj);
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

        public void Opt_Location_Remove(List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in data)
                    {
                        var obj = model.OPS_OPTLocation.FirstOrDefault(c => c.ID == item);
                        if (obj != null)
                        {
                            model.OPS_OPTLocationRequire.RemoveRange(obj.OPS_OPTLocationRequire);
                            model.OPS_OPTLocation.Remove(obj);
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

        public void Opt_LocationMatrix_Refresh(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var dataExists = new List<int>();
                    var data = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID).Select(c => c.LocationID).Distinct().ToList();                    
                    var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                    if (!string.IsNullOrEmpty(sSet))
                    {
                        var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                        if (oSet != null)
                        {
                            foreach (var item in model.CAT_Location.Where(c => c.ID == oSet.LocationFromID || c.ID == oSet.LocationToID).ToList())
                            {
                                if (!data.Contains(item.ID))
                                    data.Add(item.ID);
                            }
                        }
                    }

                    for (var i = 0; i < data.Count; i++)
                    {
                        var oneID = data[i];
                        for (var j = 0; j < data.Count; j++)
                        {
                            if (i != j)
                            {
                                var twoID = data[j];
                                var obj = model.OPS_OPTLocationMatrix.FirstOrDefault(c => c.LocationFromID == oneID && c.LocationToID == twoID && c.OptimizerID == optimizerID);
                                if (obj == null)
                                {
                                    obj = new OPS_OPTLocationMatrix();
                                    obj.CreatedBy = Account.UserName;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.OptimizerID = optimizerID;
                                    obj.LocationFromID = oneID;
                                    obj.LocationToID = twoID;
                                    var objCat = model.CAT_LocationMatrix.FirstOrDefault(c => c.LocationFromID == oneID && c.LocationToID == twoID);
                                    if (objCat != null && objCat.Hour > 0 && objCat.KM > 0)
                                    {
                                        obj.KM = objCat.KM;
                                        obj.Hour = objCat.Hour;
                                    }
                                    else
                                    {
                                        double d = 0, h = 0;
                                        if (GetRoute_ViaOSM(model, oneID, twoID, ref h, ref d))
                                        {
                                            obj.KM = Math.Round(d / 1000, 2, MidpointRounding.AwayFromZero);
                                            obj.Hour = Math.Round(h / 3600, 2, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                    model.OPS_OPTLocationMatrix.Add(obj);
                                    model.SaveChanges();
                                }
                                dataExists.Add(obj.ID);
                            }
                        }
                    }

                    foreach (var item in model.OPS_OPTLocationMatrix.Where(c => !dataExists.Contains(c.ID) && c.OptimizerID == optimizerID))
                    {
                        model.OPS_OPTLocationMatrix.Remove(item);
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

        public void Opt_LocationMatrix_Update(DTOOPTLocationToLocation item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_OPTLocationMatrix.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.KM = item.EDistance;
                        obj.Hour = item.EHours;
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

        //Đổi thông tin ETD, ETA, Xe của chuyến
        public void Opt_COTOMaster_Change(DTOOPTCOTOMaster item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTCOTOMaster.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        //Check Tgian
                        if (model.OPS_OPTCOTOMaster.Count(c => c.ID != obj.ID && (c.VehicleID == item.VehicleID || c.RomoocID == item.RomoocID) && !((c.ETD <= item.ETD && c.ETA <= item.ETD) || (c.ETD >= item.ETA && c.ETA >= item.ETA))) > 0)
                            throw FaultHelper.BusinessFault(null, null, "Trùng thời gian! Không thể lưu!");
                        //Kiểm tra dữ liệu => Nếu có thay đổi => bổ sung ModifiedBy.
                        if (obj.VehicleID != item.VehicleID || obj.RomoocID != item.RomoocID || item.ETA != obj.ETA || item.ETD != obj.ETD)
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;

                            obj.VehicleID = item.VehicleID;
                            obj.RomoocID = item.RomoocID;
                            obj.ETA = item.ETA;
                            obj.ETD = item.ETD;
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
        
        public void Opt_COTOMaster_Delete(int optMasterID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTCOTOMaster.FirstOrDefault(c => c.ID == optMasterID);
                    if (obj != null)
                    {
                        foreach (var item in model.OPS_OPTCOTODetail.Where(c => c.OPS_OPTCOTOContainer.OPTCOTOMasterID == obj.ID && c.OPS_OPTCOTOLocation.OPTCOTOMasterID == obj.ID).ToList())
                        {
                            model.OPS_OPTCOTODetail.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == obj.ID).ToList())
                        {
                            item.ModifiedBy = Account.UserName;
                            item.ModifiedDate = DateTime.Now;
                            item.OPTCOTOMasterID = null;
                        }
                        foreach (var item in model.OPS_OPTCOTOLocation.Where(c => c.OPTCOTOMasterID == obj.ID).ToList())
                        {
                            model.OPS_OPTCOTOLocation.Remove(item);
                        }
                        model.OPS_OPTCOTOMaster.Remove(obj);
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

        public void Opt_DITOMaster_Change(DTOOPTDITOMaster item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTDITOMaster.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        //Kiểm tra dữ liệu => Nếu có thay đổi => bổ sung ModifiedBy.
                        if (obj.VehicleID != item.VehicleID ||item.ETA != obj.ETA || item.ETD != obj.ETD)
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;

                            obj.VehicleID = item.VehicleID;
                            obj.ETA = item.ETA;
                            obj.ETD = item.ETD;
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

        public void Opt_DITOMaster_Delete(int optMasterID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_OPTDITOMaster.FirstOrDefault(c => c.ID == optMasterID);
                    if (obj != null)
                    {
                        foreach (var item in model.OPS_OPTDITODetail.Where(c => c.OPS_OPTDITOGroupProduct.OPTDITOMasterID == obj.ID && c.OPS_OPTDITOLocation.OPTDITOMasterID == obj.ID).ToList())
                        {
                            model.OPS_OPTDITODetail.Remove(item);
                        }
                        foreach (var item in model.OPS_OPTDITOGroupProduct.Where(c => c.OPTDITOMasterID == obj.ID).ToList())
                        {
                            item.ModifiedBy = Account.UserName;
                            item.ModifiedDate = DateTime.Now;
                            item.OPTDITOMasterID = null;
                        }
                        foreach (var item in model.OPS_OPTDITOLocation.Where(c => c.OPTDITOMasterID == obj.ID).ToList())
                        {
                            model.OPS_OPTDITOLocation.Remove(item);
                        }
                        model.OPS_OPTDITOMaster.Remove(obj);
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
        
        //Kiểm tra Optimizer đã Run.
        public bool Opt_Optmizer_HasRun(int optimizerID)
        {
            try
            {
                var result = false;
                using (var model = new DataEntities())
                {
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (obj == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu tối ưu.");
                    if (model.OPS_OPTCOTOContainer.Count(c => c.OPS_OPTOPSContainer.OptimizerID == optimizerID) > 0)
                        result = true;
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

        //Kiểm tra Optimizer đã Save.
        public bool Opt_Optmizer_HasSave(int optimizerID)
        {
            try
            {
                var result = false;
                using (var model = new DataEntities())
                {
                    var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (obj == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu tối ưu.");
                    result = obj.IsSave;
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

        //Gửi sang giám sát.
        public void Opt_COTOMaster_Save(int optimizerID, List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    HelperTOMaster.OPSCO_CreateOptimize(model, Account, optimizerID, data);
                    //var objOpt = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    //if (objOpt != null)
                    //{
                    //    var dataMaster = model.OPS_OPTCOTOMaster.Where(c => c.OptimizerID == optimizerID).ToList();
                    //    foreach (var item in data)
                    //    {
                    //        var objMaster = dataMaster.FirstOrDefault(c => c.ID == item);
                    //        if (objMaster == null)
                    //            throw FaultHelper.BusinessFault(null, null, "Thiếu dữ liệu!");
                    //        var dataCheck = model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == item && c.OPS_OPTOPSContainer.OPS_COTOContainer.COTOMasterID > 0).Select(c => c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ContainerNo + "_(" + c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code + ")").ToList();
                    //        if (dataCheck.Count > 0)
                    //            throw FaultHelper.BusinessFault(null, null, objMaster.Note + "Có dữ liệu ops. Container: " + string.Join(", ", dataCheck));

                    //        int sLocation = 1;
                    //        int eLocation = 1;
                    //        var objSetting = OPS_SystemSetting_Get(model);
                    //        sLocation = objSetting.LocationFromID;
                    //        eLocation = objSetting.LocationToID;

                    //        OPS_COTOMaster obj = new OPS_COTOMaster();
                    //        obj.CreatedBy = Account.UserName;
                    //        obj.CreatedDate = DateTime.Now;

                    //        obj.SYSCustomerID = Account.SYSCustomerID;
                    //        obj.Code = CO_GetLastCode(model);
                    //        obj.VendorOfVehicleID = objMaster.VendorOfVehicleID;
                    //        obj.VendorOfRomoocID = objMaster.VendorOfRomoocID;
                    //        obj.VehicleID = objMaster.VehicleID;
                    //        obj.RomoocID = objMaster.RomoocID;
                    //        obj.DriverID1 = objMaster.DriverID1;
                    //        obj.DriverID2 = objMaster.DriverID2;
                    //        obj.DriverName1 = objMaster.DriverName1;
                    //        obj.DriverName2 = objMaster.DriverName2;
                    //        obj.DriverTel1 = objMaster.DriverTel1;
                    //        obj.DriverTel2 = objMaster.DriverTel2;
                    //        obj.DriverCard1 = objMaster.DriverCard1;
                    //        obj.DriverCard2 = objMaster.DriverCard2;
                    //        obj.ApprovedBy = Account.UserName;
                    //        obj.ApprovedDate = DateTime.Now;

                    //        obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;
                    //        obj.GroupOfVehicleID = objMaster.CAT_Vehicle.GroupOfVehicleID;
                    //        obj.SortOrder = 1;
                    //        obj.Note = "Create via optimizer";
                    //        obj.KM = objMaster.KM;
                    //        obj.ETA = objMaster.ETA;
                    //        obj.ETD = objMaster.ETD;
                    //        obj.RoutingID = objMaster.RoutingID;
                    //        obj.DateConfig = objMaster.DateConfig;
                    //        obj.TransportModeID = objMaster.TransportModeID;
                    //        obj.ContractID = objMaster.ContractID;
                    //        obj.TypeOfOrderID = objMaster.TypeOfOrderID;
                    //        model.OPS_COTOMaster.Add(obj);

                    //        var dataLocation = objMaster.OPS_OPTCOTOLocation.OrderBy(c => c.SortOrder).Select(c => new
                    //        {
                    //            c.ID,
                    //            c.LocationID,
                    //            c.DateCome,
                    //            c.DateLeave,                                
                    //            c.SortOrder
                    //        }).ToList();
                    //        var dataContainer = objMaster.OPS_OPTCOTOContainer.Select(c => new
                    //        {
                    //            c.LocationFromID,
                    //            c.LocationToID
                    //        }).ToList();
                    //        foreach (var o in dataLocation)
                    //        {
                    //            OPS_COTOLocation objLocation = new OPS_COTOLocation();
                    //            objLocation.CreatedBy = Account.UserName;
                    //            objLocation.CreatedDate = DateTime.Now;
                    //            obj.OPS_COTOLocation.Add(objLocation);

                    //            objLocation.LocationID = o.LocationID;
                    //            objLocation.SortOrder = o.SortOrder;
                    //            objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                    //            objLocation.DateCome = o.DateCome;
                    //            objLocation.DateLeave = o.DateLeave;
                    //            objLocation.DateComeEstimate = o.DateCome;
                    //            objLocation.DateLeaveEstimate = o.DateLeave;

                    //            if (dataContainer.Count(c => c.LocationFromID == o.LocationID) > 0 && dataContainer.Count(c => c.LocationToID == o.LocationID) > 0)
                    //                objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationGetDelivery;
                    //            else if (dataContainer.Count(c => c.LocationFromID == o.LocationID) > 0)
                    //                objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationGet;
                    //            else if (dataContainer.Count(c => c.LocationToID == o.LocationID) > 0)
                    //                objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationDelivery;
                    //            else
                    //                objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                    //        }

                    //        OPS_CheckingTime(model, objSetting, obj.ID, obj.VehicleID, obj.RomoocID, obj.ETD, obj.ETA, true);
                    //        HelperTimeSheet.Create(model, Account, obj.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, null);
                    //        model.SaveChanges();
                    //        foreach (var o in objMaster.OPS_OPTCOTOContainer.Select(c => c.OPS_OPTOPSContainer.COTOContainerID).ToList())
                    //        {
                    //            var objCo = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == o);
                    //            if (objCo != null)
                    //            {
                    //                objCo.ModifiedBy = Account.UserName;
                    //                objCo.ModifiedDate = DateTime.Now;
                    //                objCo.COTOMasterID = obj.ID;
                    //                objCo.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                    //            }
                    //        }
                    //        model.SaveChanges();
                    //        //Opt_COTOMaster_ToMon(model, obj.ID);
                    //    }

                    //    //Chuyển trạng thái optimizer: Đã lưu.
                    //    objOpt.ModifiedBy = Account.UserName;
                    //    objOpt.ModifiedDate = DateTime.Now;
                    //    objOpt.IsSave = true;
                    //    model.SaveChanges();
                    //}
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

        public void Opt_DITOMaster_Save(int optimizerID, List<int> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var objOpt = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
                    if (objOpt != null)
                    {
                        var dataMaster = model.OPS_OPTDITOMaster.Where(c => c.OptimizerID == optimizerID).ToList();
                        foreach (var item in data)
                        {
                            var objMaster = dataMaster.FirstOrDefault(c => c.ID == item);
                            if (objMaster == null)
                                throw FaultHelper.BusinessFault(null, null, "Thiếu dữ liệu!");
                            var dataCheck = model.OPS_OPTDITOGroupProduct.Where(c => c.OPTDITOMasterID == item && c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.DITOMasterID > 0).Select(c => c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName + "_(" + c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code + ")").ToList();
                            if (dataCheck.Count > 0)
                                throw FaultHelper.BusinessFault(null, null, objMaster.Note + "Có dữ liệu ops. Nhóm sp: " + string.Join(", ", dataCheck));

                            int sLocation = 1;
                            int eLocation = 1;
                            var objSetting = OPS_SystemSetting_Get(model);
                            sLocation = objSetting.LocationFromID;
                            eLocation = objSetting.LocationToID;

                            OPS_DITOMaster obj = new OPS_DITOMaster();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;

                            obj.SYSCustomerID = Account.SYSCustomerID;
                            obj.Code = DI_GetLastCode(model);
                            obj.VendorOfVehicleID = objMaster.VendorOfVehicleID;
                            obj.GroupOfVehicleID = objMaster.GroupOfVehicleID;
                            obj.VehicleID = objMaster.VehicleID;
                            obj.DriverID1 = objMaster.DriverID1;
                            obj.DriverID2 = objMaster.DriverID2;
                            obj.DriverName1 = objMaster.DriverName1;
                            obj.DriverName2 = objMaster.DriverName2;
                            obj.DriverTel1 = objMaster.DriverTel1;
                            obj.DriverTel2 = objMaster.DriverTel2;
                            obj.DriverCard1 = objMaster.DriverCard1;
                            obj.DriverCard2 = objMaster.DriverCard2;
                            obj.ApprovedBy = Account.UserName;
                            obj.ApprovedDate = DateTime.Now;

                            obj.TypeOfDITOMasterID = -(int)SYSVarType.TypeOfDITONormal;
                            obj.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterApproved;
                            obj.SortOrder = 1;
                            obj.Note = "Create via optimizer";
                            obj.KM = objMaster.KM;
                            obj.ETA = objMaster.ETA;
                            obj.ETD = objMaster.ETD;
                            model.OPS_DITOMaster.Add(obj);
                            
                            OPS_CheckingTime(model, objSetting, obj.ID, obj.VehicleID, null, obj.ETD, obj.ETA, false);

                            var dataLocation = objMaster.OPS_OPTDITOLocation.OrderBy(c => c.SortOrder).Select(c => new
                            {
                                c.ID,
                                c.LocationID,
                                c.DateCome,
                                c.DateLeave,
                                c.SortOrder
                            }).ToList();
                            var dataGop = objMaster.OPS_OPTDITOGroupProduct.Select(c => new
                            {
                                c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID,
                                c.OPS_OPTOPSGroupProduct.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID
                            }).ToList();
                            foreach (var o in dataLocation)
                            {
                                OPS_DITOLocation objLocation = new OPS_DITOLocation();
                                objLocation.CreatedBy = Account.UserName;
                                objLocation.CreatedDate = DateTime.Now;
                                obj.OPS_DITOLocation.Add(objLocation);

                                objLocation.LocationID = o.LocationID;
                                objLocation.SortOrder = o.SortOrder;
                                objLocation.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusPlan;
                                objLocation.DateCome = o.DateCome;
                                objLocation.DateLeave = o.DateLeave;
                                objLocation.DateComeEstimate = o.DateCome;
                                
                                if (dataGop.Count(c => c.LocationFromID == o.LocationID) > 0 && dataGop.Count(c => c.LocationToID == o.LocationID) > 0)
                                    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationGetDelivery;
                                else if (dataGop.Count(c => c.LocationFromID == o.LocationID) > 0)
                                    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationGet;
                                else if (dataGop.Count(c => c.LocationToID == o.LocationID) > 0)
                                    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationDelivery;
                                else
                                    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                            }

                            model.SaveChanges();
                            foreach (var o in objMaster.OPS_OPTDITOGroupProduct.Select(c => new
                            {
                                c.OPS_OPTOPSGroupProduct.DITOGroupProductID,
                                c.Ton,
                                c.CBM,
                                c.Quantity
                            }).ToList())
                            {
                                var objGop = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == o.DITOGroupProductID && c.DITOMasterID == null && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
                                if (objGop != null)
                                {
                                    if (objGop.Ton > o.Ton || objGop.Quantity > o.Quantity || objGop.CBM > o.CBM)
                                    {
                                        OPS_DITOGroupProduct objN = new OPS_DITOGroupProduct();
                                        objN.CreatedBy = Account.UserName;
                                        objN.CreatedDate = DateTime.Now;
                                        model.OPS_DITOGroupProduct.Add(objN);

                                        objN.IsSplit = true;
                                        objN.Ton = objGop.Ton - o.Ton;
                                        objN.TonBBGN = objN.Ton;
                                        objN.TonTranfer = objN.Ton;
                                        objN.CBM = objGop.CBM - o.CBM;
                                        objN.CBMBBGN = objN.CBM;
                                        objN.CBMTranfer = objN.CBM;
                                        objN.Quantity = objGop.Quantity - o.Quantity;
                                        objN.QuantityBBGN = objN.Quantity;
                                        objN.QuantityTranfer = objN.Quantity;
                                        objN.QuantityLoading = objN.Quantity;

                                        objN.OrderGroupProductID = objGop.OrderGroupProductID;
                                        objN.LockedBy = objGop.LockedBy;
                                        objN.Note = objGop.Note;
                                        objN.IsInput = objGop.IsInput;
                                        objN.GroupSort = objGop.GroupSort;
                                        objN.DNCode = objGop.DNCode;
                                        objN.DITOGroupProductStatusID = objGop.DITOGroupProductStatusID;
                                        objN.DateFromCome = objGop.DateFromCome;
                                        objN.DateFromLeave = objGop.DateFromLeave;
                                        objN.DateFromLoadStart = objGop.DateFromLoadStart;
                                        objN.DateFromLoadEnd = objGop.DateFromLoadEnd;
                                        objN.DateToCome = objGop.DateToCome;
                                        objN.DateToLeave = objGop.DateToLeave;
                                        objN.DateToLoadStart = objGop.DateToLoadStart;
                                        objN.DateToLoadEnd = objGop.DateToLoadEnd;
                                        objN.Note1 = objGop.Note1;
                                        objN.Note2 = objGop.Note2;
                                        objN.IsOrigin = objGop.IsOrigin;
                                        objN.InvoiceBy = objGop.InvoiceBy;
                                        objN.InvoiceDate = objGop.InvoiceDate;
                                        //objN.LocationToID = objGop.LocationToID;
                                        objN.InvoiceNote = objGop.InvoiceNote;
                                        objN.DateDN = objGop.DateDN;
                                        objN.DITOGroupProductStatusPODID = objGop.DITOGroupProductStatusPODID;
                                        objN.CUSRoutingID = objGop.CUSRoutingID;
                                        objN.TonReturn = objGop.TonReturn;
                                        objN.CBMReturn = objGop.CBMReturn;
                                        objN.QuantityReturn = objGop.QuantityReturn;
                                        objN.TypeOfDITOGroupProductReturnID = objGop.TypeOfDITOGroupProductReturnID;
                                        objN.DateConfig = objGop.DateConfig;
                                        objN.CATRoutingID = objGop.CATRoutingID;
                                        objN.InvoiceReturnBy = objGop.InvoiceReturnBy;
                                        objN.InvoiceReturnDate = objGop.InvoiceReturnDate;
                                        objN.InvoiceReturnNote = objGop.InvoiceReturnNote;
                                        
                                        objGop.IsSplit = true;
                                        objGop.DITOMasterID = obj.ID;
                                        objGop.ModifiedBy = Account.UserName;
                                        objGop.ModifiedDate = DateTime.Now;
                                        objGop.Ton = o.Ton;
                                        objGop.TonBBGN = o.Ton;
                                        objGop.TonTranfer = o.Ton;
                                        objGop.CBM = o.CBM;
                                        objGop.CBMBBGN = o.CBM;
                                        objGop.CBMTranfer = o.CBM;
                                        objGop.Quantity = o.Quantity;
                                        objGop.QuantityBBGN = o.Quantity;
                                        objGop.QuantityTranfer = o.Quantity;
                                        objGop.QuantityLoading = o.Quantity;

                                        foreach (var p in model.OPS_DITOProduct.Where(c => c.DITOGroupProductID == objGop.ID).ToList())
                                        {
                                            p.ModifiedBy = Account.UserName;
                                            p.ModifiedDate = DateTime.Now;

                                            OPS_DITOProduct nP = new OPS_DITOProduct();
                                            nP.CreatedBy = Account.UserName;
                                            nP.CreatedDate = DateTime.Now;
                                            nP.OrderProductID = p.OrderProductID;
                                            nP.Note = p.Note;
                                            objN.OPS_DITOProduct.Add(nP);

                                            switch (p.ORD_Product.CAT_Packing.TypeOfPackageID)
                                            {
                                                case -(int)SYSVarType.TypeOfPackingGOPTon:
                                                    p.Quantity = p.QuantityBBGN = p.QuantityTranfer = objGop.Ton;
                                                    nP.Quantity = nP.QuantityBBGN = nP.QuantityTranfer = objN.Ton;
                                                    break;
                                                case -(int)SYSVarType.TypeOfPackingGOPCBM:
                                                    p.Quantity = p.QuantityBBGN = p.QuantityTranfer = objGop.CBM;
                                                    nP.Quantity = nP.QuantityBBGN = nP.QuantityTranfer = objN.CBM;
                                                    break;
                                                case -(int)SYSVarType.TypeOfPackingGOPTU:
                                                    p.Quantity = p.QuantityBBGN = p.QuantityTranfer = objGop.Quantity;
                                                    nP.Quantity = nP.QuantityBBGN = nP.QuantityTranfer = objN.Quantity;
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objGop.ModifiedBy = Account.UserName;
                                        objGop.ModifiedDate = DateTime.Now;
                                        objGop.DITOMasterID = obj.ID;
                                    }
                                }
                            }
                            model.SaveChanges();

                            HelperTimeSheet.Create(model, Account, obj.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster, null);
                            //Opt_DITOMaster_ToMon(model, obj.ID);
                        }

                        //Chuyển trạng thái optimizer: Đã lưu.
                        objOpt.ModifiedBy = Account.UserName;
                        objOpt.ModifiedDate = DateTime.Now;
                        objOpt.IsSave = true;
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

        public void Opt_Optimizer_ToDataBase(int optimizerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    //model.EventAccount = Account; model.EventRunning = false;
                    //var output = Opt_Plan.UnitTest.Tester.RunJsonTestDataSet(DataPath);
                    //CO_ConvertData(model, optimizerID, output);
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

        private void Opt_COTOMaster_ToMon(DataEntities model, int cotomasterID)
        {
            var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == cotomasterID);
            if (obj != null)
            {
                obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;

                var dataLocation = obj.OPS_COTOLocation.OrderBy(c => c.SortOrder).Select(c => new
                {
                    c.LocationID,
                    c.DateCome,
                    c.DateLeave
                }).ToList();
                var dataContainer = obj.OPS_COTOContainer.Select(c => new
                {
                    c.ID,
                    c.LocationFromID,
                    c.LocationToID
                }).ToList();
                for (var i = 0; i < dataLocation.Count - 1; i++)
                {
                    var f = dataLocation[i];
                    var t = dataLocation[i + 1];

                    OPS_COTO objCo = new OPS_COTO();
                    objCo.CreatedBy = Account.UserName;
                    objCo.CreatedDate = DateTime.Now;
                    objCo.COTOMasterID = obj.ID;
                    FindRoute(model, f.LocationID.Value, t.LocationID.Value, objCo.RoutingID);
                    objCo.SortOrder = i + 1;
                    objCo.LocationFromID = f.LocationID;
                    objCo.LocationToID = t.LocationID;
                    objCo.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                    model.OPS_COTO.Add(objCo);

                    //foreach (var co in dataContainer.Where(c => c.LocationFromID == f.LocationID || c.LocationToID == t.LocationID).ToList())
                    //{
                    //    OPS_COTODetail objDetail = new OPS_COTODetail();
                    //    objDetail.CreatedBy = Account.UserName;
                    //    objDetail.CreatedDate = DateTime.Now;
                    //    objDetail.COTOContainerID = co.ID;
                    //    objCo.OPS_COTODetail.Add(objDetail);
                    //}
                }

                model.SaveChanges();
                HelperTimeSheet.Create(model, Account, obj.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, obj.DriverID1);
            }
        }
        
        private void Opt_DITOMaster_ToMon(DataEntities model, int ditomasterID)
        {
            var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == ditomasterID);
            if (obj != null)
            {
                obj.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterTendered;
                var dataLocation = obj.OPS_DITOLocation.OrderBy(c => c.SortOrder).Select(c => new
                {
                    c.LocationID,
                    c.DateCome,
                    c.DateLeave
                }).ToList();
                var dataGop = obj.OPS_DITOGroupProduct.Select(c => new
                {
                    c.ID,
                    c.ORD_GroupProduct.LocationFromID,
                    c.ORD_GroupProduct.LocationToID
                }).ToList();
                for (var i = 0; i < dataLocation.Count - 1; i++)
                {
                    var f = dataLocation[i];
                    var t = dataLocation[i + 1];

                    OPS_DITO objDi = new OPS_DITO();
                    objDi.CreatedBy = Account.UserName;
                    objDi.CreatedDate = DateTime.Now;
                    objDi.DITOMasterID = obj.ID;
                    FindRoute(model, f.LocationID.Value, t.LocationID.Value, objDi.RoutingID);
                    objDi.StatusOfDITOID = -(int)SYSVarType.StatusOfDITOStockPlan;
                    objDi.SortOrder = i + 1;
                    objDi.IsOPS = true;
                    model.OPS_DITO.Add(objDi);

                    foreach (var gop in dataGop.Where(c => c.LocationFromID == f.LocationID || c.LocationToID == t.LocationID).ToList())
                    {
                        OPS_DITODetail objDetail = new OPS_DITODetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.DITOGroupProductID = gop.ID;
                        objDi.OPS_DITODetail.Add(objDetail);
                    }
                }

                model.SaveChanges();
                HelperTimeSheet.Create(model, Account, obj.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster, obj.DriverID1);
            }
        }

        private DTOSYSSetting OPS_SystemSetting_Get(DataEntities model)
        {
            DTOSYSSetting objSetting = new DTOSYSSetting();
            var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
            if (!string.IsNullOrEmpty(sSet))
            {
                objSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                if (objSetting != null)
                {
                    var objCheck = model.CAT_Location.FirstOrDefault(c => c.ID == objSetting.LocationFromID);
                    if (objCheck == null)
                        objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
                    objCheck = model.CAT_Location.FirstOrDefault(c => c.ID == objSetting.LocationToID);
                    if (objCheck != null)
                        objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
                }
            }
            else
            {
                objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
                objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
            }
            return objSetting;
        }

        private List<DTOOPSLocationRequired> OPS_GetLocationRequired(DataEntities model, int cID)
        {
            List<DTOOPSLocationRequired> result = new List<DTOOPSLocationRequired>();

            foreach (var item in model.CAT_LocationRequire.Where(c => c.LocationID == cID).Select(c => new
            {
                c.ID,
                c.ConstraintRequireTypeID,
                c.Weight,
                c.TimeFrom,
                c.TimeTo
            }).ToList())
            {
                switch (item.ConstraintRequireTypeID)
                {
                    case -(int)SYSVarType.ConstraintRequireTypeSizeDay:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue && item.Weight.HasValue)
                        {
                            var obj = new DTOOPSLocationRequired(true, true, item.Weight.Value, -1, item.TimeFrom.Value, item.TimeTo.Value);
                            result.Add(obj);
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeSizeWeek:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue && item.Weight.HasValue)
                        {
                            var fDate = (int)item.TimeFrom.Value.Date.DayOfWeek;
                            var tDate = (int)item.TimeTo.Value.Date.DayOfWeek;
                            if (fDate <= tDate)
                            {
                                for (var i = fDate; i <= tDate; i++)
                                {
                                    var obj = new DTOOPSLocationRequired(true, true, item.Weight.Value, i, new DateTime(2000, 1, 1).Add(item.TimeFrom.Value.Subtract(item.TimeFrom.Value.Date)), new DateTime(2000, 1, 1).Add(item.TimeTo.Value.Subtract(item.TimeTo.Value.Date)));
                                    result.Add(obj);
                                }
                            }
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeOpenWeek:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                        {
                            var fDate = (int)item.TimeFrom.Value.Date.DayOfWeek;
                            var tDate = (int)item.TimeTo.Value.Date.DayOfWeek;
                            if (fDate <= tDate)
                            {
                                for (var i = fDate; i <= tDate; i++)
                                {
                                    var obj = new DTOOPSLocationRequired(false, true, 0, i, new DateTime(2000, 1, 1).Add(item.TimeFrom.Value.Subtract(item.TimeFrom.Value.Date)), new DateTime(2000, 1, 1).Add(item.TimeTo.Value.Subtract(item.TimeTo.Value.Date)));
                                    result.Add(obj);
                                }
                            }
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeOpenDay:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                        {
                            var obj = new DTOOPSLocationRequired(false, true, 0, -1, item.TimeFrom.Value, item.TimeTo.Value);
                            result.Add(obj);
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeCloseDay:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                        {
                            var obj = new DTOOPSLocationRequired(false, false, 0, -1, item.TimeFrom.Value, item.TimeTo.Value);
                            result.Add(obj);
                        }
                        break;
                }
            }

            return result;
        }

        private void OPS_CheckingTime(DataEntities model, DTOSYSSetting objSetting, int mID, int? vehicleID, int? romoocID, DateTime? ETD, DateTime? ETA, bool isContainer)
        {
            try
            {
                if (objSetting != null && ETA.HasValue && ETD.HasValue)
                {
                    var strETD = String.Format("{0:d/M HH:mm}", ETD.Value);
                    var strETA = String.Format("{0:d/M HH:mm}", ETA.Value);

                    if (objSetting.HasConstraintTimeOPS)
                    {
                        if (isContainer)
                        {
                            var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == vehicleID);
                            if (vehicleID > 0 && objVehicle == null)
                                throw FaultHelper.BusinessFault(null, null, "Đầu kéo không tồn tại.");
                            var objRomooc = model.CAT_Vehicle.FirstOrDefault(c => c.ID == romoocID);
                            if (romoocID > 0 && objVehicle == null)
                                throw FaultHelper.BusinessFault(null, null, "Romooc không tồn tại.");

                            var objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID && c.FLM_Asset.VehicleID == vehicleID
                                && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.ReferID != mID
                              && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                            if (objV != null)
                            {
                                var objTo = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objV.ReferID);
                                if (objTo != null)
                                    throw FaultHelper.BusinessFault(null, null, "Đầu kéo " + objVehicle.RegNo + " đã phân chuyến. Số: " + objTo.Code + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                            }
                            else
                            {
                                objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID
                                    && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.FLM_Asset.VehicleID == vehicleID
                                    && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (objV != null)
                                {
                                    var strCode = string.Empty;
                                    switch (objV.StatusOfAssetTimeSheetID)
                                    {
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetMaintence:
                                            strCode = "bảo trì";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRegistry:
                                            strCode = "đăng kiểm";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRepair:
                                            strCode = "sửa chữa";
                                            break;
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Đầu kéo " + objVehicle.RegNo + " bận " + strCode + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                                }
                            }

                            var objR = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID && c.FLM_Asset.RomoocID == romoocID
                                && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.ReferID != mID
                                && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                            if (objR != null)
                            {
                                var objTo = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objV.ReferID);
                                if (objTo != null)
                                    throw FaultHelper.BusinessFault(null, null, "Romooc " + objRomooc.RegNo + "đã phân chuyến. Số: " + objTo.Code + ", từ: " + String.Format("{0:d/M HH:mm}", objR.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objR.DateToActual));
                            }
                            else
                            {
                                objR = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID
                                    && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.FLM_Asset.RomoocID == romoocID
                                    && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (objR != null)
                                {
                                    var strCode = string.Empty;
                                    switch (objR.StatusOfAssetTimeSheetID)
                                    {
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetMaintence:
                                            strCode = "bảo trì";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRegistry:
                                            strCode = "đăng kiểm";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRepair:
                                            strCode = "sửa chữa";
                                            break;
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Romooc " + objRomooc.RegNo + " bận " + strCode + ", từ: " + String.Format("{0:d/M HH:mm}", objR.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objR.DateToActual));
                                }
                            }
                        }
                        else
                        {
                            var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == vehicleID);
                            if (vehicleID >= 0 && objVehicle == null)
                                throw FaultHelper.BusinessFault(null, null, "Xe không tồn tại.");
                            var objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID && c.FLM_Asset.VehicleID == vehicleID
                                && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster && c.ReferID != mID
                                && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                            if (objV != null)
                            {
                                var objTo = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == objV.ReferID);
                                if (objTo != null)
                                    throw FaultHelper.BusinessFault(null, null, "Xe " + objVehicle.RegNo + " đã phân chuyến. Số: " + objTo.Code + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                            }
                            else
                            {
                                objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID
                                    && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster && c.FLM_Asset.VehicleID == vehicleID
                                    && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (objV != null)
                                {
                                    var strCode = string.Empty;
                                    switch (objV.StatusOfAssetTimeSheetID)
                                    {
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetMaintence:
                                            strCode = "bảo trì";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRegistry:
                                            strCode = "đăng kiểm";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRepair:
                                            strCode = "sửa chữa";
                                            break;
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Xe " + objVehicle.RegNo + " bận " + strCode + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                                }
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

        public void OPSCO_CheckLocationRequired(DataEntities model, DTOSYSSetting objSetting, DTOOPSCOTOMaster obj)
        {
            try
            {
                double cWeight = 0;
                double eWeight = 0;
                if (obj.VehicleID > 0)
                {
                    var objV = model.CAT_Vehicle.FirstOrDefault(c => c.ID == obj.VehicleID);
                    if (objV == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy đầu kéo");
                    eWeight = Math.Min(objV.MaxWeight ?? 0, eWeight);
                }
                if (obj.RomoocID > 0)
                {
                    var objR = model.CAT_Romooc.FirstOrDefault(c => c.ID == obj.RomoocID);
                    if (objR == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy romooc");
                    eWeight = Math.Min(objR.MaxWeight ?? 0, eWeight);
                }

                for (var i = 0; i < obj.ListCOLocation.Count; i++)
                {
                    var item = obj.ListCOLocation[i];
                    var catObj = model.CAT_Location.FirstOrDefault(c => c.ID == item.LocationID);
                    if (catObj != null && item.DateComeEstimate.HasValue)
                    {
                        var dataRequired = OPS_GetLocationRequired(model, item.LocationID.Value);
                        if (objSetting.HasConstraintTimeOpenLocation)
                        {
                            if (dataRequired.Count(c => c.IsOpen == true && c.IsSize == false) > 0)
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));

                                var objCheck = dataRequired.FirstOrDefault(c => c.IsOpen == true && c.IsSize == false && ((c.DayOfWeek == dayOfWeek && esDate >= c.TimeFrom && esDate <= c.TimeTo) || (c.DayOfWeek == -1 && item.DateComeEstimate >= c.TimeFrom && item.DateComeEstimate <= c.TimeTo)));
                                if (objCheck == null)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không mở cửa vào tgian: " + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
                            }
                            foreach (var o in dataRequired.Where(c => c.IsOpen == false && c.IsSize == false))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));

                                if ((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo))
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không mở cửa vào tgian: từ " + String.Format("{0:d/M HH:mm}", o.TimeFrom) + " đến " + String.Format("{0:d/M HH:mm}", o.TimeTo));
                            }
                        }
                        //Check trọng tải trước khi vào kho/cảng/bãi
                        if (objSetting.HasConstraintWeight)
                        {
                            foreach (var o in dataRequired.Where(c => c.IsOpen == true && c.IsSize == true))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));
                                if (((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo)) && o.Weight < cWeight)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không đáp ứng trọng tải. Tgian:" + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
                            }
                        }
                        foreach (var o in obj.ListCOContainer.Where(c => c.LocationFromID == item.LocationID).ToList())
                        {
                            cWeight = cWeight + o.Ton;
                        }
                        foreach (var o in obj.ListCOContainer.Where(c => c.LocationFromID == item.LocationID).ToList())
                        {
                            cWeight = cWeight - o.Ton;
                        }
                        //Check trọng tải sau khi Load/UnLoad hàng
                        if (objSetting.HasConstraintTransport)
                        {
                            if (eWeight > 0 && eWeight < cWeight)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Phương tiện không đáp ứng trọng tải tại điểm " + catObj.Location);
                            }
                        }
                        if (objSetting.HasConstraintWeight)
                        {
                            foreach (var o in dataRequired.Where(c => c.IsOpen == true && c.IsSize == true))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));
                                if (((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo)) && o.Weight < cWeight)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không đáp ứng trọng tải. Tgian:" + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
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

        public void OPSDI_CheckLocationRequired(DataEntities model, DTOSYSSetting objSetting, List<DTOOPSDITOGroupProduct> dataGop, List<DTOOPSDITOLocation> dataLocation, int vehicleID)
        {
            try
            {
                double cWeight = 0;
                double eWeight = 0;
                if (vehicleID > 0)
                {
                    var objV = model.CAT_Vehicle.FirstOrDefault(c => c.ID == vehicleID);
                    if (objV == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy xe");
                    eWeight = objV.MaxWeight ?? 0;
                }
                for (var i = 0; i < dataLocation.Count; i++)
                {
                    var item = dataLocation[i];
                    var catObj = model.CAT_Location.FirstOrDefault(c => c.ID == item.LocationID);
                    if (catObj != null && item.DateComeEstimate.HasValue)
                    {
                        var dataRequired = OPS_GetLocationRequired(model, item.LocationID.Value);

                        //Check thời gian mở cửa
                        if (objSetting.HasConstraintTimeOpenLocation)
                        {
                            if (dataRequired.Count(c => c.IsOpen == true && c.IsSize == false) > 0)
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));

                                var objCheck = dataRequired.FirstOrDefault(c => c.IsOpen == true && c.IsSize == false && ((c.DayOfWeek == dayOfWeek && esDate >= c.TimeFrom && esDate <= c.TimeTo) || (c.DayOfWeek == -1 && item.DateComeEstimate >= c.TimeFrom && item.DateComeEstimate <= c.TimeTo)));
                                if (objCheck == null)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không mở cửa vào tgian: " + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
                            }
                            foreach (var o in dataRequired.Where(c => c.IsOpen == false && c.IsSize == false))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));

                                if ((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo))
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không mở cửa vào tgian: từ " + String.Format("{0:d/M HH:mm}", o.TimeFrom) + " đến " + String.Format("{0:d/M HH:mm}", o.TimeTo));
                            }
                        }

                        //Check trọng tải trước khi vảo điểm.
                        if (objSetting.HasConstraintWeight)
                        {
                            foreach (var o in dataRequired.Where(c => c.IsOpen == true && c.IsSize == true))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));

                                if (((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo)) && o.Weight < cWeight)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không đáp ứng trọng tải. Tgian:" + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
                            }
                        }

                        foreach (var o in dataGop.Where(c => c.CATLocationFromID == item.LocationID).ToList())
                        {
                            cWeight = cWeight + o.Ton;
                        }
                        foreach (var o in dataGop.Where(c => c.CATLocationToID == item.LocationID).ToList())
                        {
                            cWeight = cWeight - o.Ton;
                        }

                        //Check trọng tải xe sau khi Load/UnLoad hàng
                        if (objSetting.HasConstraintTransport)
                        {
                            if (eWeight > 0 && eWeight < cWeight)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Xe không đáp ứng trọng tải tại điểm " + catObj.Location);
                            }
                        }

                        //Check trọng tải sau khi Load/UnLoad hàng                        
                        if (objSetting.HasConstraintWeight)
                        {
                            foreach (var o in dataRequired.Where(c => c.IsOpen == true && c.IsSize == true))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));
                                if (((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo)) && o.Weight < cWeight)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không đáp ứng trọng tải. Tgian:" + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
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

        private void COOptimize_Run(DataEntities model, int optimizerID)
        {
            var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID && c.SYSCustomerID == Account.SYSCustomerID);
            if (obj != null && obj.IsContainer)
            {
                if (model.OPS_OPTOPSContainer.Count(c => c.OptimizerID == optimizerID) == 0)
                    throw FaultHelper.BusinessFault(null, null, "Không có dữ liệu container.");
                if (model.OPS_OPTVehicle.Count(c => c.OptimizerID == optimizerID) == 0)
                    throw FaultHelper.BusinessFault(null, null, "Không có dữ liệu đầu kéo.");
                if (model.OPS_OPTRomooc.Count(c => c.OptimizerID == optimizerID) == 0)
                    throw FaultHelper.BusinessFault(null, null, "Không có dữ liệu romooc.");
                var vehicleEmpty = model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID && c.RomoocID == null).Select(c => c.CAT_Vehicle.RegNo).ToList();
                if (vehicleEmpty.Count > 0)
                    throw FaultHelper.BusinessFault(null, null, "Đầu kéo " + string.Join(", ", vehicleEmpty) + " chưa chọn romooc.");
                var locationEmpty = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID && (!c.CAT_Location.Lat.HasValue || !c.CAT_Location.Lng.HasValue)).Select(c => c.CAT_Location.Location).ToList();
                if (locationEmpty.Count > 0)
                    throw FaultHelper.BusinessFault(null, null, "Điểm " + string.Join(", ", locationEmpty) + " chưa thiết lập tọa độ.");


                //GetData.
                var inputVehicle = new List<Opt_DataInput.Vehicle>();
                var inputContainer = new List<Opt_DataInput.Order>();
                var inputLocation = new List<Opt_DataInput.Location>();
                var inputRate = new Dictionary<string, Opt_DataInput.Rate>();
                var inputLocationMatrix = new Opt_DataInput.LocationToLocationMatrix();
                var inputVehicleType = new Dictionary<string, Opt_DataInput.VehicleType>();
                var inputPrice = HelperFinance.Container_OptimizePrice(optimizerID);

                try
                {
                    // Lấy điểm HomeLocation
                    var objSetting = OPS_SystemSetting_Get(model);
                    if (objSetting != null)
                    {
                        foreach (var item in model.CAT_Location.Where(c => c.ID == objSetting.LocationFromID || c.ID == objSetting.LocationToID).ToList())
                        {
                            var temp = new Dictionary<DayOfWeek, List<Opt_Data.TimeWindow>>();
                            if (item.Lat > 0 && item.Lng > 0)
                            {
                                var objL = new Opt_DataInput.Location(item.Location, new Opt_Data.WeeklyOpenHours(temp));
                                objL.Latitude = item.Lat.Value;
                                objL.Longitude = item.Lng.Value;
                                objL.ID = item.ID == objSetting.LocationFromID ? "-1" : "-2"; //-1: CurrentLocation //-2: HomeLocation
                                objL.Optionals = new Dictionary<string, string>();
                                objL.Optionals.Add("CATLocationID", item.ID.ToString());
                                objL.Optionals.Add("Code", item.Code);
                                objL.Optionals.Add("Address", item.Address);
                                objL.Optionals.Add("EconomicZone", item.EconomicZone);
                                objL.Optionals.Add("CountryID", item.CountryID.ToString());
                                objL.Optionals.Add("ProvinceID", item.ProvinceID.ToString());
                                objL.Optionals.Add("DistrictID", item.DistrictID.ToString());
                                inputLocation.Add(objL);

                                if (objSetting.LocationFromID == objSetting.LocationToID)
                                {
                                    var objLCopy = new Opt_DataInput.Location(item.Location, new Opt_Data.WeeklyOpenHours(temp));
                                    var copy = new CopyHelper();
                                    copy.Copy(objL, objLCopy);
                                    objLCopy.ID = "-2";
                                    inputLocation.Add(objLCopy);
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }

                foreach (var item in model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID).Select(c => c.ID).ToList())
                {
                    var objVehicle = CO_GetVehicle(model, item, ref inputVehicleType, ref inputRate);
                    if (objVehicle != null && objVehicle.VehicleAvailabilty != null && objVehicle.VehicleAvailabilty.Count > 0)
                    {
                        if (inputLocation.Count(c => c.ID == "-1") > 0)
                            objVehicle.CurrentLocationId = "-1";
                        if (inputLocation.Count(c => c.ID == "-2") > 0)
                            objVehicle.HomeLocationId = "-2";
                        inputVehicle.Add(objVehicle);
                    }
                }
                var dataExLocation = new List<int>();
                foreach (var item in model.OPS_OPTOPSContainer.Where(c => c.OptimizerID == optimizerID).Select(c => c.ID).ToList())
                {
                    var pLocation = new Opt_DataInput.Location();
                    var dLocation = new Opt_DataInput.Location();
                    var objCon = CO_GetContainer(model, item, inputPrice, dataExLocation, out pLocation, out dLocation, obj.IsBalanceCustomer);
                    if (objCon != null)
                    {
                        inputContainer.Add(objCon);
                        if (!string.IsNullOrEmpty(objCon.PickUpLocationId) && dataExLocation.Count(c => c.ToString() == objCon.PickUpLocationId) == 0 && pLocation.ID != null)
                        {
                            dataExLocation.Add(Convert.ToInt32(objCon.PickUpLocationId));
                            inputLocation.Add(pLocation);
                        }
                        if (!string.IsNullOrEmpty(objCon.DeliveryLocationId) && dataExLocation.Count(c => c.ToString() == objCon.DeliveryLocationId) == 0 && dLocation.ID != null)
                        {
                            dataExLocation.Add(Convert.ToInt32(objCon.DeliveryLocationId));
                            inputLocation.Add(dLocation);
                        }
                    }
                }

                var inputLocationToLocation = new Dictionary<string, Dictionary<string, Opt_DataInput.LocationToLocation>>();
                if (inputLocation.Count > 0)
                {
                    for (var i = 0; i < inputLocation.Count; i++)
                    {
                        var one = inputLocation[i];
                        if (one != null)
                        {
                            var oneID = Convert.ToInt32(one.Optionals["CATLocationID"]);
                            if (!inputLocationToLocation.ContainsKey(one.ID))
                                inputLocationToLocation[one.ID] = new Dictionary<string, Opt_DataInput.LocationToLocation>();

                            for (var j = i + 1; j < inputLocation.Count; j++)
                            {
                                var two = inputLocation[j];
                                if (two != null)
                                {
                                    var twoID = Convert.ToInt32(two.Optionals["CATLocationID"]);
                                    if (!inputLocationToLocation.ContainsKey(two.ID))
                                        inputLocationToLocation[two.ID] = new Dictionary<string, Opt_DataInput.LocationToLocation>();

                                    Opt_DataInput.LocationToLocation obj1 = GetRoute(model, optimizerID, oneID, twoID);
                                    if (obj1 != null && obj1.TravelDistance > 0 && obj1.TravelTime > 0 && !inputLocationToLocation[one.ID].ContainsKey(two.ID.ToString()))
                                    {
                                        inputLocationToLocation[one.ID].Add(two.ID, obj1);
                                    }
                                    Opt_DataInput.LocationToLocation obj2 = GetRoute(model, optimizerID, twoID, oneID);
                                    if (obj2 != null && obj2.TravelDistance > 0 && obj2.TravelTime > 0 && !inputLocationToLocation[two.ID].ContainsKey(one.ID.ToString()))
                                    {
                                        inputLocationToLocation[two.ID].Add(one.ID, obj2);
                                    }
                                }
                            }
                        }
                    }
                }

                inputLocationMatrix = new Opt_DataInput.LocationToLocationMatrix(inputLocationToLocation);
                var dicLocation = new Dictionary<string, Opt_DataInput.Location>();
                foreach (var key in inputLocation)
                {
                    dicLocation.Add(key.ID, key);
                }

                Opt_DataInput.TransportationPlanInput input = null;
                try
                {

                    input = new Opt_DataInput.TransportationPlanInput(inputContainer, inputVehicle, dicLocation, inputRate, inputVehicleType, inputLocationMatrix);
                    //ToJsonInput
                    SaveJsonFile(input, "TMS_CO", optimizerID);
                    DTOOPTData objSave = GetJsonDataSet(model, optimizerID);
                    objSave.Status = 10;
                    objSave.Input = input;
                    objSave.Locations = input.Locations;
                    objSave.LocationToLocationMatrix = input.LocationToLocationMatrix;
                    objSave.LocationVehicleTypeExclusions = input.LocationVehicleTypeExclusions;
                    objSave.Orders = input.Orders;
                    objSave.Rates = input.Rates;
                    objSave.Vehicles = input.Vehicles;
                    objSave.VehicleTypes = input.VehicleTypes;

                    objSave.Output = new Opt_DataOutput.TransportationPlanOutput();
                    SaveJsonDataSet(model, optimizerID, objSave);
                }
                catch (Exception ex)
                {
                    throw FaultHelper.BusinessFault(null, null, "Có lỗi trong quá trình khởi tạo dữ liệu đầu vào (1): " + ex.Message);
                }
            }
        }
        
        private void DIOptimize_Run(DataEntities model, int optimizerID)
        {
            if (model.OPS_OPTOPSGroupProduct.Count(c => c.OptimizerID == optimizerID) == 0)
                throw FaultHelper.BusinessFault(null, null, "Không có dữ liệu đơn hàng.");
            if (model.OPS_OPTVehicle.Count(c => c.OptimizerID == optimizerID) == 0)
                throw FaultHelper.BusinessFault(null, null, "Không có dữ liệu xe tải.");
            var locationEmpty = model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID && (!c.CAT_Location.Lat.HasValue || !c.CAT_Location.Lng.HasValue)).Select(c => c.CAT_Location.Location).ToList();
            if (locationEmpty.Count > 0)
                throw FaultHelper.BusinessFault(null, null, "Điểm " + string.Join(", ", locationEmpty) + " chưa thiết lập tọa độ.");

            //GetData.
            var inputVehicle = new List<Opt_DataInput.Vehicle>();
            var inputGroupProduct = new List<Opt_DataInput.Order>();
            var inputLocation = new List<Opt_DataInput.Location>();
            var inputRate = new Dictionary<string, Opt_DataInput.Rate>();
            var inputLocationMatrix = new Opt_DataInput.LocationToLocationMatrix();
            var inputVehicleType = new Dictionary<string, Opt_DataInput.VehicleType>();
            var inputPrice = HelperFinance.Container_OptimizePrice(optimizerID);

            try
            {
                // Lấy điểm HomeLocation
                var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                if (!string.IsNullOrEmpty(sSet))
                {
                    var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                    if (oSet != null)
                    {
                        foreach (var item in model.CAT_Location.Where(c => c.ID == oSet.LocationFromID || c.ID == oSet.LocationToID).ToList())
                        {
                            if (item.Lat > 0 && item.Lng > 0)
                            {
                                var objL = new Opt_DataInput.Location(item.Location, new Opt_Data.WeeklyOpenHours(null));
                                objL.Latitude = item.Lat.Value;
                                objL.Longitude = item.Lng.Value;
                                objL.ID = item.ID == oSet.LocationFromID ? "-1" : "-2";
                                //-1: CurrentLocation
                                //-2: HomeLocation
                                objL.Optionals = new Dictionary<string, string>();
                                objL.Optionals.Add("CATLocationID", item.ID.ToString());
                                objL.Optionals.Add("Code", item.Code);
                                objL.Optionals.Add("Address", item.Address);
                                objL.Optionals.Add("EconomicZone", item.EconomicZone);
                                objL.Optionals.Add("CountryID", item.CountryID.ToString());
                                objL.Optionals.Add("ProvinceID", item.ProvinceID.ToString());
                                objL.Optionals.Add("DistrictID", item.DistrictID.ToString());

                                inputLocation.Add(objL);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            foreach (var item in model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID).Select(c => c.ID).ToList())
            {
                var objVehicle = DI_GetVehicle(model, item, ref inputVehicleType, ref inputRate);
                if (objVehicle != null && objVehicle.VehicleAvailabilty != null && objVehicle.VehicleAvailabilty.Count > 0)
                {
                    if (inputLocation.Count(c => c.ID == "-1") > 0)
                        objVehicle.CurrentLocationId = "-1";
                    if (inputLocation.Count(c => c.ID == "-2") > 0)
                        objVehicle.HomeLocationId = "-2";
                    inputVehicle.Add(objVehicle);
                }
            }

            var dataExLocation = new List<int>();
            foreach (var item in model.OPS_OPTOPSGroupProduct.Where(c => c.OptimizerID == optimizerID).Select(c => c.ID).ToList())
            {
                var pLocation = new Opt_DataInput.Location();
                var dLocation = new Opt_DataInput.Location();
                var objGop = DI_GetGroupProduct(model, item, dataExLocation, inputPrice, out pLocation, out dLocation);
                if (objGop != null)
                {
                    inputGroupProduct.Add(objGop);
                    if (!string.IsNullOrEmpty(objGop.PickUpLocationId) && dataExLocation.Count(c => c.ToString() == objGop.PickUpLocationId) == 0 && pLocation.ID != null)
                    {
                        dataExLocation.Add(Convert.ToInt32(objGop.PickUpLocationId));
                        inputLocation.Add(pLocation);
                    }
                    if (!string.IsNullOrEmpty(objGop.DeliveryLocationId) && dataExLocation.Count(c => c.ToString() == objGop.DeliveryLocationId) == 0 && dLocation.ID != null)
                    {
                        dataExLocation.Add(Convert.ToInt32(objGop.DeliveryLocationId));
                        inputLocation.Add(dLocation);
                    }

                }
            }

            var inputLocationToLocation = new Dictionary<string, Dictionary<string, Opt_DataInput.LocationToLocation>>();
            if (inputLocation.Count > 0)
            {
                for (var i = 0; i < inputLocation.Count; i++)
                {
                    var one = inputLocation[i];
                    if (one != null)
                    {
                        var oneID = Convert.ToInt32(one.Optionals["CATLocationID"]);
                        if (!inputLocationToLocation.ContainsKey(one.ID))
                            inputLocationToLocation[one.ID] = new Dictionary<string, Opt_DataInput.LocationToLocation>();

                        for (var j = i + 1; j < inputLocation.Count; j++)
                        {
                            var two = inputLocation[j];
                            if (two != null)
                            {
                                var twoID = Convert.ToInt32(two.Optionals["CATLocationID"]);
                                if (!inputLocationToLocation.ContainsKey(two.ID))
                                    inputLocationToLocation[two.ID] = new Dictionary<string, Opt_DataInput.LocationToLocation>();

                                Opt_DataInput.LocationToLocation obj1 = GetRoute(model, optimizerID, oneID, twoID);
                                if (obj1 != null && !inputLocationToLocation[one.ID].ContainsKey(two.ID.ToString()))
                                {
                                    inputLocationToLocation[one.ID].Add(two.ID, obj1);
                                }
                                Opt_DataInput.LocationToLocation obj2 = GetRoute(model, optimizerID, twoID, oneID);
                                if (obj2 != null && !inputLocationToLocation[two.ID].ContainsKey(one.ID.ToString()))
                                {
                                    inputLocationToLocation[two.ID].Add(one.ID, obj2);
                                }
                            }
                        }
                    }
                }
            }

            inputLocationMatrix = new Opt_DataInput.LocationToLocationMatrix(inputLocationToLocation);
            var dicLocation = new Dictionary<string, Opt_DataInput.Location>();
            foreach (var key in inputLocation)
            {
                dicLocation.Add(key.ID, key);
            }

            Opt_DataInput.TransportationPlanInput input = null;
            try
            {
                input = new Opt_DataInput.TransportationPlanInput(inputGroupProduct, inputVehicle, dicLocation, inputRate, inputVehicleType, inputLocationMatrix);
                input.LocationVehicleTypeExclusions = new List<Opt_DataInput.LocationVehicleTypeExclusion>();
                foreach (var objL in inputLocation)
                {
                    var dataExclustion = GetLocationVehicleTypeExclusion(model, objL.ID, inputVehicleType);
                    input.LocationVehicleTypeExclusions.AddRange(dataExclustion);
                }
                //ToJsonInput
                SaveJsonFile(input, "TMS_DI", optimizerID);

                DTOOPTData objSave = GetJsonDataSet(model, optimizerID);
                objSave.Status = 10;
                objSave.Input = input;
                objSave.Locations = input.Locations;
                objSave.LocationToLocationMatrix = input.LocationToLocationMatrix;
                objSave.LocationVehicleTypeExclusions = input.LocationVehicleTypeExclusions;
                objSave.Orders = input.Orders;
                objSave.Rates = input.Rates;
                objSave.Vehicles = input.Vehicles;
                objSave.VehicleTypes = input.VehicleTypes;

                objSave.Output = new Opt_DataOutput.TransportationPlanOutput();
                SaveJsonDataSet(model, optimizerID, objSave);
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(null, null, "Có lỗi trong quá trình khởi tạo dữ liệu đầu vào (1): " + ex.Message);
            }
        }

        private void SaveJsonFile(Opt_DataInput.TransportationPlanInput input, string name, int optimizerID)
        {
            try
            {
                if (SaveJson)
                {
                    if (HttpContext.Current == null || HttpContext.Current.Server.MapPath("").Contains("Hoang Hung"))
                    {
                        string folderPath = DataPath + name + "_" + optimizerID;
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        input.ToJsonFiles(folderPath);
                    }
                }
            }
            catch (Exception) { }
        }

        private DTOOPTData GetJsonDataSet(DataEntities model, int optimizerID)
        {
            DTOOPTData result = new DTOOPTData();
            var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
            if (obj != null && !string.IsNullOrEmpty(obj.DataRun))
            {
                DTOOPTDataJson item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPTDataJson>(obj.DataRun, _jsonSetting);
                if (item != null)
                {
                    result.Locations = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Opt_DataInput.Location>>(item.Locations, _jsonSetting);
                    var matrix = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Opt_DataInput.LocationToLocation>>>(item.LocationToLocationMatrix, _jsonSetting);
                    result.LocationToLocationMatrix = new Opt_DataInput.LocationToLocationMatrix(matrix);
                    result.LocationVehicleTypeExclusions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Opt_DataInput.LocationVehicleTypeExclusion>>(item.LocationVehicleTypeExclusions, _jsonSetting);
                    result.Orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Opt_DataInput.Order>>(item.Orders, _jsonSetting);
                    result.Rates = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Opt_DataInput.Rate>>(item.Rates, _jsonSetting);
                    result.Vehicles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Opt_DataInput.Vehicle>>(item.Vehicles, _jsonSetting);
                    result.VehicleTypes = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Opt_DataInput.VehicleType>>(item.VehicleTypes, _jsonSetting);

                    result.Input = new Opt_DataInput.TransportationPlanInput(result.Orders, result.Vehicles, result.Locations, result.Rates, result.VehicleTypes, result.LocationToLocationMatrix);
                    result.Input.LocationVehicleTypeExclusions = result.LocationVehicleTypeExclusions;

                    if (!string.IsNullOrEmpty(item.Output))
                    {
                        result.Output = Newtonsoft.Json.JsonConvert.DeserializeObject<Opt_DataOutput.TransportationPlanOutput>(item.Output, _jsonSetting);
                    }
                }
            }
            return result;
        }

        private void SaveJsonDataSet(DataEntities model, int optimizerID, DTOOPTData item)
        {
            var obj = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerID);
            if (obj != null)
            {
                DTOOPTDataJson objSave = new DTOOPTDataJson();
                objSave.OptimizerID = optimizerID;

                objSave.Locations = Newtonsoft.Json.JsonConvert.SerializeObject(item.Locations, Formatting.Indented, _jsonSetting);
                var matrix = item.LocationToLocationMatrix.ToDict();
                objSave.LocationToLocationMatrix = Newtonsoft.Json.JsonConvert.SerializeObject(matrix, Formatting.Indented, _jsonSetting);
                objSave.LocationVehicleTypeExclusions = Newtonsoft.Json.JsonConvert.SerializeObject(item.LocationVehicleTypeExclusions, Formatting.Indented, _jsonSetting);
                objSave.Orders = Newtonsoft.Json.JsonConvert.SerializeObject(item.Orders, Formatting.Indented, _jsonSetting);
                objSave.Rates = Newtonsoft.Json.JsonConvert.SerializeObject(item.Rates, Formatting.Indented, _jsonSetting);
                objSave.Vehicles = Newtonsoft.Json.JsonConvert.SerializeObject(item.Vehicles, Formatting.Indented, _jsonSetting);
                objSave.VehicleTypes = Newtonsoft.Json.JsonConvert.SerializeObject(item.VehicleTypes, Formatting.Indented, _jsonSetting);
                objSave.Output = Newtonsoft.Json.JsonConvert.SerializeObject(item.Output, Formatting.Indented, _jsonSetting);

                obj.DataRun = Newtonsoft.Json.JsonConvert.SerializeObject(objSave, Formatting.Indented, _jsonSetting);
                model.SaveChanges();
            }
        }
        
        private Opt_DataInput.Vehicle CO_GetVehicle(DataEntities model, int vehicleID, ref Dictionary<string, Opt_DataInput.VehicleType> dicVehicleType, ref Dictionary<string, Opt_DataInput.Rate> dicRate)
        {
            Opt_DataInput.Vehicle result = null;
            var obj = model.OPS_OPTVehicle.FirstOrDefault(c => c.ID == vehicleID && c.RomoocID > 0);
            if (obj != null)
            {
                //GetRate
                var objRate = new Opt_DataInput.Rate();
                objRate.ID = vehicleID.ToString();
                objRate.Optionals = new Dictionary<string, string>();

                foreach (var item in model.FLM_MaterialQuota.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID && c.FLM_Asset.VehicleID == obj.VehicleID && c.MaterialID > 0).Select(c => new
                {
                    c.MaterialID,
                    c.FLM_Material.MaterialName,
                    c.QuantityPerKM
                }).ToList())
                {
                    var oPrice = model.FLM_MaterialPrice.Where(c => c.MaterialID == item.MaterialID).OrderBy(c => c.Price).Select(c => c.Price).FirstOrDefault();
                    if (oPrice > 0)
                    {
                        objRate.PerDistance += (double)(oPrice * (decimal)item.QuantityPerKM);
                        objRate.Optionals.Add(item.MaterialName, "QpU: " + item.QuantityPerKM.ToString() + "--C: " + oPrice.ToString());
                    }
                    else
                    {
                        objRate.Optionals.Add(item.MaterialName, "QpU: " + item.QuantityPerKM.ToString() + "--C: 0");
                    }
                }
                if (!dicRate.ContainsKey(objRate.ID))
                    dicRate.Add(objRate.ID, objRate);

                //GetType
                var strType = obj.CAT_Romooc.GroupOfRomoocID > 0 ? obj.CAT_Romooc.CAT_GroupOfRomooc.GroupName : string.Empty;
                var objType = new Opt_DataInput.VehicleType(objRate.ID, new Opt_Data.MultiDimSize(obj.CAT_Romooc.RegCapacity == 1 ? 20 : 40, obj.MaxWeightCal, obj.CAT_Romooc.RegCapacity == 1 ? 1 : 2));
                var dataReq = model.FLM_AssetRequire.Where(c => c.FLM_Asset.VehicleID == obj.VehicleID && c.ConstraintRequireTypeID == -(int)SYSVarType.ConstraintRequireTypeKM && c.KMTo > 0).ToList();
                if (dataReq.Count > 0)
                {
                    objType.MaxDistanceTwoStops = dataReq.Max(c => c.KMTo.Value);
                }
                objType.ID = vehicleID.ToString();
                objType.Optionals = new Dictionary<string, string>();
                objType.Optionals.Add("RegCapacity", obj.CAT_Romooc.RegCapacity.HasValue ? obj.CAT_Romooc.RegCapacity.Value.ToString() : string.Empty);
                objType.Optionals.Add("GroupOfRomoocID", obj.CAT_Romooc.GroupOfRomoocID.ToString());
                objType.Optionals.Add("GroupOfRomoocName", strType);
                if (!dicVehicleType.ContainsKey(objType.ID))
                    dicVehicleType.Add(objType.ID, objType);

                //GetVehicle
                result = new Opt_DataInput.Vehicle(null, null, objType.ID);
                result.ID = obj.ID.ToString();
                result.Optionals = new Dictionary<string, string>();
                result.Optionals.Add("CATVehicleID", obj.VehicleID.ToString());
                result.Optionals.Add("VehicleNo", obj.CAT_Vehicle.RegNo);
                result.Optionals.Add("RomoocID", obj.RomoocID.Value.ToString());
                result.Optionals.Add("RomoocNo", obj.CAT_Romooc.RegNo);
                result.UsagePriority = 0;

                var objSetting = OPS_SystemSetting_Get(model);
                //GetVehicleAvailabilty
                var objAsset = model.FLM_Asset.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID == obj.VehicleID).Select(c => new
                {
                    c.ID
                }).FirstOrDefault();
                if (objAsset != null)
                {
                    result.VehicleAvailabilty = new List<Opt_Data.TimeWindow>();
                    if (objSetting.HasConstraintTimeOPS)
                    {
                        var dataTimeSheet = model.FLM_AssetTimeSheet.Where(c => c.AssetID == objAsset.ID && c.DateToActual >= obj.OPS_Optimizer.DateFrom && c.DateFromActual <= obj.OPS_Optimizer.DateTo).Select(c => new
                        {
                            DateTo = c.DateToActual,
                            DateFrom = c.DateFromActual
                        }).OrderBy(c => c.DateFrom).ThenBy(c => c.DateTo).ToList();

                        DateTime cTime = obj.OPS_Optimizer.DateFrom.Value;
                        if (dataTimeSheet.Count > 0)
                        {
                            for (var i = 0; i < dataTimeSheet.Count; i++)
                            {
                                var item = dataTimeSheet[i];
                                if (item.DateFrom > cTime)
                                {
                                    result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(cTime, item.DateFrom));
                                }
                                cTime = item.DateTo;
                            }
                            if (cTime < obj.OPS_Optimizer.DateTo.Value)
                            {
                                result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(cTime, obj.OPS_Optimizer.DateTo.Value));
                            }
                        }
                        else
                        {
                            result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(obj.OPS_Optimizer.DateFrom.Value, obj.OPS_Optimizer.DateTo.Value));
                        }
                    }
                    else
                    {
                        result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(obj.OPS_Optimizer.DateFrom.Value, obj.OPS_Optimizer.DateTo.Value));
                    }
                }
            }
            return result;
        }

        private Opt_DataInput.Order CO_GetContainer(DataEntities model, int optCoID, DTOOPTPriceContainer dataPrice, List<int> dataLocation, out Opt_DataInput.Location pLocation, out Opt_DataInput.Location dLocation, bool isCusBalance)
        {
            Opt_DataInput.Order result = null;
            pLocation = new Opt_DataInput.Location();
            dLocation = new Opt_DataInput.Location();

            var obj = model.OPS_OPTOPSContainer.Where(c => c.ID == optCoID).Select(c => new
            {
                c.ID,
                c.OptimizerID,
                c.OPS_COTOContainer.OPS_Container.ContainerNo,
                c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code,
                c.OPS_COTOContainer.SortOrder,
                c.OPS_COTOContainer.OPS_Container.ContainerID,
                c.OPS_COTOContainer.LocationFromID,
                c.OPS_COTOContainer.LocationToID,
                c.ETA,
                c.OPS_COTOContainer.ETAStart,
                c.ETD,
                c.OPS_COTOContainer.ETDStart,
                c.OPS_COTOContainer.StatusOfCOContainerID,
                c.OPS_COTOContainer.CAT_Location.LoadTimeCO,
                c.OPS_COTOContainer.SYS_Var.ValueOfVar,
                c.OPS_COTOContainer.CAT_Location1.UnLoadTimeCO
            }).FirstOrDefault();
            if (obj != null)
            {
                var objCon = model.ORD_Container.Where(c => c.ID == obj.ContainerID).Select(c => new
                {
                    c.ID,
                    c.OrderID,
                    c.ORD_Order.IsHot,
                    c.ORD_Order.Code,
                    c.ORD_Order.CustomerID,
                    c.ORD_Order.PartnerID,
                    c.ORD_Order.ServiceOfOrderID,
                    c.ContainerNo,
                    c.SealNo1,
                    c.SealNo2,
                    c.PackingID,
                    c.CAT_Packing.PackingName,
                    c.Ton,
                    c.CUSRoutingID,
                    c.Note
                }).FirstOrDefault();

                //GetLocation
                pLocation = GetLocation(model, obj.OptimizerID, obj.LocationFromID);
                dLocation = GetLocation(model, obj.OptimizerID, obj.LocationToID);

                int[] sFull = { -(int)SYSVarType.StatusOfCOContainerEXLaden, -(int)SYSVarType.StatusOfCOContainerIMLaden, -(int)SYSVarType.StatusOfCOContainerLOLaden };
                var iFull = new List<int>(sFull);
                var iLaden = iFull.Contains(obj.StatusOfCOContainerID);
                var size = new Opt_Data.MultiDimSize(objCon.PackingID == 5 ? 20 : 40, objCon.Ton, iLaden ? 1 : 0);
                if (obj.ETA == null || obj.ETD == null)
                    throw FaultHelper.BusinessFault(null, null, "Container: " + obj.ContainerNo + "-" + obj.Code + " thiếu dữ liệu thời gian.");
                var pTime = new Opt_Data.TimeWindow(obj.ETDStart.HasValue ? obj.ETDStart.Value : obj.ETD.Value.AddHours(-2), obj.ETD.Value);
                var dTime = new Opt_Data.TimeWindow(obj.ETAStart.HasValue ? obj.ETAStart.Value : obj.ETA.Value.AddHours(-2), obj.ETA.Value);

                result = new Opt_DataInput.Order(pLocation.ID, pTime, dLocation.ID, dTime, size);
                result.ID = optCoID.ToString();
                result.UploadTime = obj.LoadTimeCO > 0 ? obj.LoadTimeCO.Value * 60 : 60;
                result.DownLoadTime = obj.UnLoadTimeCO > 0 ? obj.UnLoadTimeCO.Value * 60 : 60;

                var objCoPrice = dataPrice.ListOrderContainer.FirstOrDefault(c => c.ORDContainerID == obj.ContainerID);
                if (objCoPrice != null)
                {
                    result.Profit = (double)objCoPrice.Price;
                }
                else
                {
                    var objPakingPrice = dataPrice.ListOwner.FirstOrDefault(c => c.PackingID == objCon.PackingID && c.CATRoutingID == objCon.CUSRoutingID);
                    if (objPakingPrice != null)
                    {
                        result.Profit = (double)objPakingPrice.Price;
                    }
                }

                if (iLaden)
                    result.Profit = result.Profit * 60 / 100;
                else
                    result.Profit = result.Profit * 40 / 100;
                result.Priority = objCon.IsHot == true ? 0 : 1;

                result.Optionals = new Dictionary<string, string>();
                result.Optionals.Add("OrderID", objCon.OrderID.ToString());
                result.Optionals.Add("OrderCode", objCon.Code);
                if (isCusBalance)
                    result.Optionals.Add("CustomerID", objCon.CustomerID.ToString());
                else
                    result.Optionals.Add("CustomerID", string.Empty);
                result.Optionals.Add("PartnerID", objCon.PartnerID.HasValue ? objCon.PartnerID.Value.ToString() : "");
                result.Optionals.Add("ContainerID", obj.ContainerID.ToString());
                result.Optionals.Add("ContainerNo", objCon.ContainerNo);
                result.Optionals.Add("SealNo1", objCon.SealNo1);
                result.Optionals.Add("SealNo2", objCon.SealNo2);
                result.Optionals.Add("SortOrder", obj.SortOrder.ToString());
                result.Optionals.Add("ServiceOfOrder", objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport ? "1" : objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport ? "2" : "3");
                result.Optionals.Add("PackingID", objCon.PackingID.ToString());
                result.Optionals.Add("StatusOfCOContainerName", obj.ValueOfVar);
                result.Optionals.Add("PackingName", objCon.PackingName);
                result.Optionals.Add("Note", objCon.Note);
            }
            return result;
        }

        private Opt_DataInput.Vehicle DI_GetVehicle(DataEntities model, int vehicleID, ref Dictionary<string, Opt_DataInput.VehicleType> dicVehicleType, ref Dictionary<string, Opt_DataInput.Rate> dicRate)
        {
            Opt_DataInput.Vehicle result = null;

            var obj = model.OPS_OPTVehicle.FirstOrDefault(c => c.ID == vehicleID);
            if (obj != null)
            {
                //GetRate
                var objRate = new Opt_DataInput.Rate();
                objRate.ID = vehicleID.ToString();
                objRate.Optionals = new Dictionary<string, string>();

                foreach (var item in model.FLM_MaterialQuota.Where(c => c.FLM_Asset.SYSCustomerID == Account.SYSCustomerID && c.FLM_Asset.VehicleID == obj.VehicleID && c.MaterialID > 0).Select(c => new
                {
                    c.MaterialID,
                    c.FLM_Material.MaterialName,
                    c.QuantityPerKM
                }).ToList())
                {
                    var oPrice = model.FLM_MaterialPrice.Where(c => c.MaterialID == item.MaterialID).OrderBy(c => c.Price).Select(c => c.Price).FirstOrDefault();
                    if (oPrice > 0)
                    {
                        objRate.PerDistance += (double)(oPrice * (decimal)item.QuantityPerKM);
                        objRate.Optionals.Add(item.MaterialName, "QpU: " + item.QuantityPerKM.ToString() + "--C: " + oPrice.ToString());
                    }
                    else
                    {
                        objRate.Optionals.Add(item.MaterialName, "QpU: " + item.QuantityPerKM.ToString() + "--C: 0");
                    }
                }
                if (!dicRate.ContainsKey(objRate.ID))
                    dicRate.Add(objRate.ID, objRate);

                //GetType
                //Chưa đưa Cubic vào.
                //var cubic = obj.CAT_Vehicle.MaxCapacity.HasValue && obj.CAT_Vehicle.MaxCapacity > 0 ? obj.CAT_Vehicle.MaxCapacity : null;
                var objType = new Opt_DataInput.VehicleType(objRate.ID, new Opt_Data.MultiDimSize(null, obj.MaxWeightCal, null));
                objType.ID = vehicleID.ToString();
                objType.Optionals = new Dictionary<string, string>();
                objType.Optionals.Add("GroupOfVehicleID", obj.CAT_Vehicle.GroupOfVehicleID > 0 ? obj.CAT_Vehicle.GroupOfVehicleID.ToString() : string.Empty);
                objType.Optionals.Add("GroupOfVehicleName", obj.CAT_Vehicle.GroupOfVehicleID > 0 ? obj.CAT_Vehicle.CAT_GroupOfVehicle.GroupName : string.Empty);
                if (!dicVehicleType.ContainsKey(objType.ID))
                    dicVehicleType.Add(objType.ID, objType);

                //GetVehicle
                result = new Opt_DataInput.Vehicle(null, null, objType.ID);
                result.ID = obj.ID.ToString();
                result.Optionals = new Dictionary<string, string>();
                result.Optionals.Add("CATVehicleID", obj.VehicleID.ToString());
                result.Optionals.Add("VehicleNo", obj.CAT_Vehicle.RegNo);
                result.UsagePriority = 0;

                var objSetting = OPS_SystemSetting_Get(model);
                //GetVehicleAvailabilty
                var objAsset = model.FLM_Asset.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID == obj.VehicleID).Select(c => new
                {
                    c.ID
                }).FirstOrDefault();
                if (objAsset != null)
                {
                    result.VehicleAvailabilty = new List<Opt_Data.TimeWindow>();
                    if (objSetting.HasConstraintTimeOPS)
                    {
                        var dataTimeSheet = model.FLM_AssetTimeSheet.Where(c => c.AssetID == objAsset.ID && c.DateToActual >= obj.OPS_Optimizer.DateFrom && c.DateFromActual <= obj.OPS_Optimizer.DateTo).Select(c => new
                        {
                            DateTo = c.DateToActual,
                            DateFrom = c.DateFromActual
                        }).OrderBy(c => c.DateFrom).ThenBy(c => c.DateTo).ToList();

                        DateTime cTime = obj.OPS_Optimizer.DateFrom.Value;
                        if (dataTimeSheet.Count > 0)
                        {
                            for (var i = 0; i < dataTimeSheet.Count; i++)
                            {
                                var item = dataTimeSheet[i];
                                if (item.DateFrom > cTime)
                                {
                                    result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(cTime, item.DateFrom));
                                }
                                cTime = item.DateTo;
                            }
                            if (cTime < obj.OPS_Optimizer.DateTo.Value)
                            {
                                result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(cTime, obj.OPS_Optimizer.DateTo.Value));
                            }
                        }
                        else
                        {
                            result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(obj.OPS_Optimizer.DateFrom.Value, obj.OPS_Optimizer.DateTo.Value));
                        }
                    }
                    else
                    {
                        result.VehicleAvailabilty.Add(new Opt_Data.TimeWindow(obj.OPS_Optimizer.DateFrom.Value, obj.OPS_Optimizer.DateTo.Value));
                    }
                }
            }
            return result;
        }

        private Opt_DataInput.Order DI_GetGroupProduct(DataEntities model, int optGopID, List<int> dataLocation, DTOOPTPriceContainer dataPrice, out Opt_DataInput.Location pLocation, out Opt_DataInput.Location dLocation)
        {
            Opt_DataInput.Order result = null;
            pLocation = new Opt_DataInput.Location();
            dLocation = new Opt_DataInput.Location();

            var obj = model.OPS_OPTOPSGroupProduct.Where(c => c.ID == optGopID).Select(c => new
            {
                c.ID,
                c.OptimizerID,
                c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                c.OPS_DITOGroupProduct.OrderGroupProductID,
                LocationFromID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationID,
                LocationToID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationID,
                c.ETA,
                c.ETD,
                c.ETAStart,
                c.ETDStart,
                c.OPS_DITOGroupProduct.Ton,
                c.OPS_DITOGroupProduct.CBM,
                c.OPS_DITOGroupProduct.Quantity,
                c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.LoadTimeDI,
                c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.UnLoadTimeDI,
                c.OPS_DITOGroupProduct.Note
            }).FirstOrDefault();
            if (obj != null)
            {
                var objGop = model.ORD_GroupProduct.Where(c => c.ID == obj.OrderGroupProductID).Select(c => new
                {
                    c.ID,
                    c.OrderID,
                    c.ORD_Order.IsHot,
                    c.ORD_Order.Code,
                    c.ORD_Order.CustomerID,
                    c.ORD_Order.PartnerID,
                    c.GroupOfProductID,
                    c.CUS_GroupOfProduct.GroupName,
                    c.PackingID,
                    c.CAT_Packing.PackingName,
                    c.Ton,
                    c.CBM,
                    c.Quantity,
                    c.CUSRoutingID,
                    c.DNCode,
                    c.SOCode,
                    c.ORD_Order.TransportModeID,
                    c.ORD_Order.SYS_Var3.ValueOfVar
                }).FirstOrDefault();

                //GetLocation
                pLocation = GetLocation(model, obj.OptimizerID, obj.LocationFromID);
                dLocation = GetLocation(model, obj.OptimizerID, obj.LocationToID);

                var size = new Opt_Data.MultiDimSize(obj.CBM, obj.Ton, obj.Quantity);
                if (obj.ETA == null || obj.ETD == null)
                    throw FaultHelper.BusinessFault(null, null, "Nhóm: " + obj.GroupName + "-" + obj.Code + " thiếu dữ liệu thời gian.");
                var pTime = new Opt_Data.TimeWindow(obj.ETDStart.HasValue ? obj.ETDStart.Value : obj.ETD.Value.AddHours(-2), obj.ETD.Value);
                var dTime = new Opt_Data.TimeWindow(obj.ETAStart.HasValue ? obj.ETAStart.Value : obj.ETA.Value.AddHours(-2), obj.ETA.Value);

                result = new Opt_DataInput.Order(pLocation.ID, pTime, dLocation.ID, dTime, size);
                result.ID = optGopID.ToString();
                result.UploadTime = obj.LoadTimeDI > 0 ? obj.LoadTimeDI.Value * 60 : 60;
                result.DownLoadTime = obj.UnLoadTimeDI > 0 ? obj.UnLoadTimeDI.Value * 60 : 60;

                result.Priority = objGop.IsHot == true ? 0 : 1;

                result.Optionals = new Dictionary<string, string>();
                result.Optionals.Add("OrderID", objGop.OrderID.ToString());
                result.Optionals.Add("OrderCode", objGop.Code);
                result.Optionals.Add("CustomerID", objGop.CustomerID.ToString());
                result.Optionals.Add("PartnerID", objGop.PartnerID.HasValue ? objGop.PartnerID.Value.ToString() : "");
                result.Optionals.Add("OrderGroupProductID", obj.OrderGroupProductID.ToString());
                result.Optionals.Add("GroupOfProductID", objGop.GroupOfProductID.ToString());
                result.Optionals.Add("GroupOfProductName", objGop.GroupName);
                result.Optionals.Add("DNCode", objGop.DNCode);
                result.Optionals.Add("SOCode", objGop.SOCode);
                result.Optionals.Add("PackingID", objGop.PackingID.ToString());
                result.Optionals.Add("PackingName", objGop.PackingName.ToString());
                result.Optionals.Add("TransportModeID", objGop.TransportModeID.ToString());
                result.Optionals.Add("IsFTL", objGop.TransportModeID == -(int)SYSVarType.TransportModeFTL ? "True" : "False");
                result.Optionals.Add("FTLID", objGop.TransportModeID == -(int)SYSVarType.TransportModeFTL ? objGop.OrderID.ToString() : "");
                result.Optionals.Add("TransportModeName", objGop.ValueOfVar);
                result.Optionals.Add("Note", obj.Note);
            }

            return result;
        }

        private List<Opt_DataInput.LocationVehicleTypeExclusion> GetLocationVehicleTypeExclusion(DataEntities model, string optLocationID, Dictionary<string, Opt_DataInput.VehicleType> inputVehicleType)
        {
            List<Opt_DataInput.LocationVehicleTypeExclusion> result = new List<Opt_DataInput.LocationVehicleTypeExclusion>();
            var data = model.OPS_OPTLocationRequire.Where(c => c.OPTLocationID.ToString() == optLocationID && (c.ConstraintRequireTypeID == CTSizeWeek || c.ConstraintRequireTypeID == CTSizeDay)).ToList();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var obj = new Opt_DataInput.LocationVehicleTypeExclusion();
                    obj.LocationId = optLocationID.ToString();
                    obj.ExclusionHours = new Dictionary<DayOfWeek, Opt_Data.TimeWindow>();
                    obj.ExclusionHours.Add(item.TimeFrom.Value.DayOfWeek, new Opt_Data.TimeWindow(item.TimeFrom.Value, item.TimeTo.Value));
                    obj.VehicleTypeIds = inputVehicleType.Where(c => c.Value.VehicleCapacity.Weight > item.Weight).Select(c => c.Key).ToList();
                    if (obj.VehicleTypeIds.Count > 0)
                        result.Add(obj);
                }
            }
            return result;
        }

        private Opt_DataInput.Location GetLocation(DataEntities model, int optimizerID, int locationID)
        {
            Opt_DataInput.Location result = new Opt_DataInput.Location();

            var obj = model.OPS_OPTLocation.Where(c => c.LocationID == locationID && c.OptimizerID == optimizerID).Select(c => new
            {
                c.ID,
                c.LocationID,
                c.CAT_Location.Code,
                c.CAT_Location.Location,
                c.CAT_Location.Lat,
                c.CAT_Location.Lng,
                c.CAT_Location.CountryID,
                c.CAT_Location.ProvinceID,
                c.CAT_Location.DistrictID,
                c.CAT_Location.WardID,
                c.CAT_Location.Address,
                c.CAT_Location.EconomicZone
            }).FirstOrDefault();
            if (obj != null)
            {
                Dictionary<DayOfWeek, List<Opt_Data.TimeWindow>> dicOpenHours = new Dictionary<DayOfWeek, List<Opt_Data.TimeWindow>>();
                //Get OpenHours From Constraint Table
                var dataConst = model.OPS_OPTLocationRequire.Where(c => c.OPTLocationID == obj.ID).OrderBy(c => c.ConstraintRequireTypeID).Select(c => new
                {
                    c.TimeFrom,
                    c.TimeTo,
                    c.ConstraintRequireTypeID,
                    c.Weight,
                    c.Width,
                    c.Length,
                    c.Height
                }).ToList();
                foreach (var item in dataConst)
                {
                    switch (item.ConstraintRequireTypeID)
                    {
                        // Tgian làm việc theo tuần. Thiết lập hằng tuần
                        case CTOpenWeek:
                            if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                            {
                                var time1 = item.TimeFrom.HasValue ? (int)item.TimeFrom.Value.DayOfWeek : -1;
                                var time2 = item.TimeTo.HasValue ? (int)item.TimeTo.Value.DayOfWeek : -1;
                                if (time2 >= time1 && time1 > -1)
                                {
                                    for (var time = time1; time <= time2; time++)
                                    {
                                        if (dicOpenHours.ContainsKey((DayOfWeek)time))
                                        {
                                            dicOpenHours[(DayOfWeek)time].Add(new Opt_Data.TimeWindow(item.TimeFrom.Value, item.TimeTo.Value));
                                        }
                                        else
                                        {
                                            var o = new List<Opt_Data.TimeWindow>();
                                            o.Add(new Opt_Data.TimeWindow(item.TimeFrom.Value, item.TimeTo.Value));
                                            dicOpenHours.Add((DayOfWeek)time, o);
                                        }
                                    }
                                }
                            }
                            break;
                        // Tgian làm việc những ngày đặc biệt
                        case CTOpenDay:
                            if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                            {
                                var time1 = item.TimeFrom.HasValue ? (int)item.TimeFrom.Value.DayOfWeek : -1;
                                var time2 = item.TimeTo.HasValue ? (int)item.TimeTo.Value.DayOfWeek : -1;
                                if (time1 == time2 && time2 > -1)
                                {
                                    if (dicOpenHours.ContainsKey((DayOfWeek)time1))
                                    {
                                        dicOpenHours[(DayOfWeek)time1].Add(new Opt_Data.TimeWindow(item.TimeFrom.Value, item.TimeTo.Value));
                                    }
                                    else
                                    {
                                        var o = new List<Opt_Data.TimeWindow>();
                                        o.Add(new Opt_Data.TimeWindow(item.TimeFrom.Value, item.TimeTo.Value));
                                        dicOpenHours.Add((DayOfWeek)time1, o);
                                    }
                                }
                            }
                            break;
                        // Tgian nghỉ những ngày đặc biệt
                        case CTCloseDay:
                            if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                            {
                                var time1 = item.TimeFrom.HasValue ? (int)item.TimeFrom.Value.DayOfWeek : -1;
                                var time2 = item.TimeTo.HasValue ? (int)item.TimeTo.Value.DayOfWeek : -1;
                                if (time1 == time2 && time2 > -1)
                                {
                                    if (dicOpenHours.ContainsKey((DayOfWeek)time1))
                                    {
                                        var objDic = dicOpenHours[(DayOfWeek)time1];
                                        if (objDic != null)
                                        {
                                            var tmp = new List<Opt_Data.TimeWindow>();
                                            foreach (var time in objDic)
                                            {
                                                if (item.TimeFrom.Value > time.EarliestDateTime)
                                                {
                                                    if (item.TimeTo.Value < time.LatestDateTime)
                                                    {
                                                        //Tách thành 2 khung tgian
                                                        tmp.Add(new Opt_Data.TimeWindow(time.EarliestDateTime, item.TimeFrom.Value));
                                                        tmp.Add(new Opt_Data.TimeWindow(item.TimeTo.Value, time.LatestDateTime));
                                                    }
                                                    else
                                                    {
                                                        tmp.Add(new Opt_Data.TimeWindow(time.EarliestDateTime, item.TimeFrom.Value));
                                                    }
                                                }
                                                else
                                                {
                                                    if (item.TimeTo.Value < time.LatestDateTime)
                                                    {
                                                        tmp.Add(new Opt_Data.TimeWindow(item.TimeTo.Value, time.LatestDateTime));
                                                    }
                                                }
                                            }

                                            dicOpenHours[(DayOfWeek)time1] = tmp;
                                        }
                                    }
                                }
                            }
                            break;
                        // Giới hạn trọng tải, kích thước theo tuần - Với xe Container: không ảnh hưởng
                        case CTSizeWeek:

                            break;
                        // Giới hạn trọng tải, kích thước theo ngày - Với xe Container: không ảnh hưởng
                        case CTSizeDay:

                            break;
                        default:
                            break;
                    }
                }

                var openHours = new Opt_Data.WeeklyOpenHours(dicOpenHours);
                result = new Opt_DataInput.Location(obj.Location, openHours);
                result.ID = obj.ID.ToString();

                result.Optionals = new Dictionary<string, string>();
                result.Optionals.Add("Code", obj.Code);
                result.Optionals.Add("CATLocationID", obj.LocationID.ToString());
                result.Optionals.Add("Address", obj.Address);
                result.Optionals.Add("EconomicZone", obj.EconomicZone);
                result.Optionals.Add("CountryID", obj.CountryID.ToString());
                result.Optionals.Add("ProvinceID", obj.ProvinceID.ToString());
                result.Optionals.Add("DistrictID", obj.DistrictID.ToString());

                result.Latitude = obj.Lat.HasValue ? obj.Lat.Value : 0;
                result.Longitude = obj.Lng.HasValue ? obj.Lng.Value : 0;
            }
            return result;
        }

        private Opt_DataInput.LocationToLocation GetRoute(DataEntities model, int optID, int oneID, int twoID)
        {
            Opt_DataInput.LocationToLocation result = null;
            try
            {
                var objDetail = model.OPS_OPTLocationMatrix.Where(c => c.OptimizerID == optID && c.LocationFromID == oneID && c.LocationToID == twoID).Select(c => new
                {
                    c.ID,
                    c.Hour,
                    c.KM
                }).FirstOrDefault();
                if (objDetail != null && objDetail.KM > 0)
                {
                    result = new Opt_DataInput.LocationToLocation(Math.Round(objDetail.Hour * 60, 2, MidpointRounding.AwayFromZero), Math.Round(objDetail.KM, 2, MidpointRounding.AwayFromZero));
                }                
            }
            catch (Exception)
            {
            }
            return result;
        }

        private bool GetRoute_ViaOSM(DataEntities model, int oneID, int twoID, ref double time, ref double distance)
        {
            var obj1 = model.CAT_Location.Where(c => c.ID == oneID && c.Lat.HasValue && c.Lng.HasValue).Select(c => new { c.Lat, c.Lng }).FirstOrDefault();
            var obj2 = model.CAT_Location.Where(c => c.ID == twoID && c.Lat.HasValue && c.Lng.HasValue).Select(c => new { c.Lat, c.Lng }).FirstOrDefault();
            if (obj1 != null && obj2 != null)
            {
                try
                {
                    var request = WebRequest.Create(String.Format("http://router.project-osrm.org/route/v1/driving/{0},{1};{2},{3}", obj1.Lng, obj1.Lat, obj2.Lng, obj2.Lat));
                    var response = request.GetResponse();
                    var stream = new StreamReader(response.GetResponseStream());
                    var txt = stream.ReadToEnd();
                    txt.Remove(txt.Length - 1, 1).Remove(0, 1);

                    var item = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(txt);
                    distance = item.routes[0].distance;
                    time = item.routes[0].duration;

                    stream.Close();
                    response.Close();
                    return true;
                }
                catch (Exception)
                {
                    try
                    {
                        var request = WebRequest.Create(String.Format("https://api.mapbox.com/directions/v5/mapbox/driving/{0},{1};{2},{3}.json?geometries=geojson&overview=simplified&alternatives=false&steps=false&access_token=pk.eyJ1IjoiaHVuZ2hvYW5nc21sIiwiYSI6ImNpcWx5OWhoMTAwYXZmcG5oOWZvcHI3eTYifQ.hKLiBbe5enCLWitk9OsKtQ", obj1.Lng, obj1.Lat, obj2.Lng, obj2.Lat));
                        var response = request.GetResponse();
                        var stream = new StreamReader(response.GetResponseStream());
                        var txt = stream.ReadToEnd();
                        txt.Remove(txt.Length - 1, 1).Remove(0, 1);

                        var item = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(txt);
                        distance = item.routes[0].distance;
                        time = item.routes[0].duration;

                        stream.Close();
                        response.Close();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        private void FindRoute(DataEntities model, int oneID, int twoID, int? result)
        {
            var objRoute = model.CAT_Routing.FirstOrDefault(c => c.LocationFromID == oneID && c.LocationToID == twoID);
            if (objRoute != null)
            {
                result = objRoute.ID;
                return;
            }
            objRoute = model.CAT_Routing.FirstOrDefault(c => c.RoutingAreaFromID > 0 && c.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(o => o.LocationID == oneID)
                && c.RoutingAreaToID > 0 && c.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(o => o.LocationID == twoID));
            if (objRoute != null)
            {
                result = objRoute.ID;
                return;
            }
        }

        private void CO_ConvertData(DataEntities model, int optimizerID, Opt_DataInput.TransportationPlanInput input, Opt_DataOutput.TransportationPlanOutput output)
        {
            if (output != null)
            {
                //ResetData
                Optimizer_Reset(model, optimizerID);
                model.SaveChanges();

                int sLocation = 1;
                int eLocation = 1;
                var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                if (!string.IsNullOrEmpty(sSet))
                {
                    var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                    if (oSet != null)
                    {
                        var obj1 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationFromID);
                        if (obj1 != null)
                            sLocation = obj1.ID;
                        var obj2 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationToID);
                        if (obj2 != null)
                            eLocation = obj2.ID;
                    }
                }

                Dictionary<int, string> dicContainer = new Dictionary<int, string>();
                //Lưu container
                foreach (var con in output.OrderTransportationPlans)
                {
                    OPS_OPTCOTOContainer objCo = new OPS_OPTCOTOContainer();
                    objCo.CreatedBy = Account.UserName;
                    objCo.CreatedDate = DateTime.Now;
                    
                    COTOContainer_Create(model, optimizerID, con.OrderId, con.UnPlannedReasons, objCo);
                    var objO = input.Orders.FirstOrDefault(c => c.ID == con.OrderId);
                    COTOLocation_Create(model, optimizerID, objO.PickUpLocationId, objO.DeliveryLocationId, objCo);
                    model.OPS_OPTCOTOContainer.Add(objCo);
                    model.SaveChanges();
                    
                    if (con.TripIds.Count > 1)
                    {
                        objCo.IsLocked = true;
                        var idx = 0;
                        var dataTrips = output.Trips.Where(c => con.TripIds.Contains(c.TripID)).OrderBy(c => c.TripID).ToList();
                        foreach (var item in dataTrips)
                        {
                            idx++;
                            OPS_OPTCOTOContainer objC = new OPS_OPTCOTOContainer();
                            objC.CreatedBy = Account.UserName;
                            objC.CreatedDate = DateTime.Now;
                            objC.ParentID = objCo.ID;
                            COTOContainer_Create(model, optimizerID, con.OrderId, con.UnPlannedReasons, objC);
                            var objOT = input.Orders.FirstOrDefault(c => c.ID == con.OrderId);
                            COTOLocation_Create(model, optimizerID, objOT.PickUpLocationId, objOT.DeliveryLocationId, objC);
                            objC.SortOrder = idx;
                            model.OPS_OPTCOTOContainer.Add(objC);
                            model.SaveChanges();
                            dicContainer.Add(objC.ID, item.TripID);
                        }
                    }
                    else
                    {
                        dicContainer.Add(objCo.ID, con.TripIds.FirstOrDefault());
                    }
                }

                //Lưu chuyến.
                foreach (var group in output.Trips.GroupBy(c => c.VehicleID).ToList())
                {
                    foreach (var trip in group.OrderBy(c => c.DeliveryDateTime).ToList())
                    {
                        OPS_OPTCOTOMaster objCo = new OPS_OPTCOTOMaster();
                        objCo.CreatedBy = Account.UserName;
                        objCo.CreatedDate = DateTime.Now;

                        objCo.OptimizerID = optimizerID;
                        objCo.SortOrder = 1;
                        objCo.ETD = trip.PickupDateTime;
                        objCo.ETA = trip.DeliveryDateTime;
                        objCo.KM = trip.Summary.TotalTravelDistance;
                        objCo.Debit = (decimal)trip.Summary.TotalCost;
                        objCo.Note = "Chuyến " + trip.TripID;
                        var objV = input.Vehicles.FirstOrDefault(c => c.ID == trip.VehicleID);
                        COTOMaster_Create(model, optimizerID, Convert.ToInt32(objV.Optionals["CATVehicleID"]), null, objCo);
                        model.OPS_OPTCOTOMaster.Add(objCo);

                        int idx = 1;
                        //Nếu có trip trước đó => điểm bắt đầu là điểm giao hàng của trip trước đó.
                        var currLocation = group.FirstOrDefault(c => c.DeliveryDateTime < trip.DeliveryDateTime);
                        if (currLocation != null && input.Locations.ContainsKey(currLocation.ToLocationId))
                        {
                            sLocation = Convert.ToInt32(input.Locations[currLocation.ToLocationId].Optionals["CATLocationID"]);
                        }

                        //Nếu BĐ = điểm lấy hàng => Chỉ tạo điểm lấy hàng.
                        if (input.Locations.ContainsKey(trip.FromLocationId) && sLocation != Convert.ToInt32(input.Locations[trip.FromLocationId].Optionals["CATLocationID"]))
                        {
                            //OPS_OPTCOTOLocation oLocation0 = new OPS_OPTCOTOLocation();
                            //oLocation0.LocationID = sLocation;
                            //oLocation0.SortOrder = idx++;
                            //oLocation0.CreatedBy = Account.UserName;
                            //oLocation0.CreatedDate = DateTime.Now;
                            //GetLocation_TimeWindow(output, trip.VehicleID, trip.TripID, sLocation, oLocation0.DateCome, oLocation0.DateLeave);
                            //objCo.OPS_OPTCOTOLocation.Add(oLocation0);
                        }

                        //Điểm lấy hàng
                        OPS_OPTCOTOLocation oLocation1 = new OPS_OPTCOTOLocation();
                        oLocation1.LocationID = Convert.ToInt32(input.Locations[trip.FromLocationId].Optionals["CATLocationID"]);
                        oLocation1.SortOrder = idx++;
                        oLocation1.CreatedBy = Account.UserName;
                        oLocation1.CreatedDate = DateTime.Now;
                        GetLocation_TimeWindow(output, trip.VehicleID, trip.TripID, oLocation1.LocationID, oLocation1.DateCome, oLocation1.DateLeave);
                        objCo.OPS_OPTCOTOLocation.Add(oLocation1);

                        //Điểm giao hàng
                        OPS_OPTCOTOLocation oLocation2 = new OPS_OPTCOTOLocation();
                        oLocation2.LocationID = Convert.ToInt32(input.Locations[trip.ToLocationId].Optionals["CATLocationID"]);
                        oLocation2.SortOrder = idx++;
                        oLocation2.CreatedBy = Account.UserName;
                        oLocation2.CreatedDate = DateTime.Now;
                        GetLocation_TimeWindow(output, trip.VehicleID, trip.TripID, oLocation2.LocationID, oLocation2.DateCome, oLocation2.DateLeave);
                        objCo.OPS_OPTCOTOLocation.Add(oLocation2);

                        //Nếu có chuyến tiếp theo => điểm kết thúc = điểm lấy hàng của chuyến tiếp
                        var homeLocation = group.FirstOrDefault(c => c.DeliveryDateTime > trip.DeliveryDateTime);
                        var homeLocationID = eLocation;
                        if (homeLocation != null && input.Locations.ContainsKey(homeLocation.ToLocationId))
                        {
                            homeLocationID = Convert.ToInt32(input.Locations[homeLocation.ToLocationId].Optionals["CATLocationID"]);
                        }

                        //Điểm kết thúc trong ngày.
                        //Nếu != điểm giao hàng => tạo điểm kết thúc.
                        if (eLocation != oLocation2.LocationID)
                        {
                            //OPS_OPTCOTOLocation oLocation3 = new OPS_OPTCOTOLocation();
                            //oLocation3.LocationID = homeLocationID;
                            //oLocation3.SortOrder = idx++;
                            //oLocation3.CreatedBy = Account.UserName;
                            //oLocation3.CreatedDate = DateTime.Now;
                            //GetLocation_TimeWindow(output, trip.VehicleID, trip.TripID, oLocation3.LocationID, oLocation3.DateCome, oLocation3.DateLeave);
                            //objCo.OPS_OPTCOTOLocation.Add(oLocation3);
                        }

                        model.SaveChanges();
                        foreach (var item in dicContainer.Where(c => c.Value == trip.TripID).ToList())
                        {
                            var objCoTo = model.OPS_OPTCOTOContainer.FirstOrDefault(c => c.ID == item.Key);
                            if (objCoTo != null)
                                objCoTo.OPTCOTOMasterID = objCo.ID;
                        }
                        model.SaveChanges();
                    }
                }

                //Dữ liệu tổng hợp theo xe.
                foreach (var vehicle in output.VehicleSummaries)
                {
                    //var objVehicle = model.OPS_OPTVehicle.FirstOrDefault(c => c.ID.ToString() == vehicle.VehicleID);
                    //if (objVehicle != null)
                    //{
                    //    objVehicle.ModifiedBy = Account.UserName;
                    //    objVehicle.ModifiedDate = DateTime.Now;
                    //    objVehicle.TotalCost = (decimal)vehicle.Summary.TotalCost;
                    //    objVehicle.TotalDistanceWithNoLoad = vehicle.Summary.TotalDistanceWithNoLoad;
                    //    objVehicle.TotalDrivingTime = vehicle.Summary.TotalDrivingTime;
                    //    objVehicle.TotalTravelDistance = vehicle.Summary.TotalTravelDistance;
                    //    objVehicle.TotalWaitingTime = vehicle.Summary.TotalWaitingTime;
                    //}
                }
                model.SaveChanges();
            }
        }

        private void DI_ConvertData(DataEntities model, int optimizerID, Opt_DataInput.TransportationPlanInput input, Opt_DataOutput.TransportationPlanOutput output)
        {            
            if (output != null)
            {
                //ResetData
                Optimizer_Reset(model, optimizerID);
                model.SaveChanges();

                int sLocation = 1;
                int eLocation = 1;
                var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
                if (!string.IsNullOrEmpty(sSet))
                {
                    var oSet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                    if (oSet != null)
                    {
                        var obj1 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationFromID);
                        if (obj1 != null)
                            sLocation = obj1.ID;
                        var obj2 = model.CAT_Location.FirstOrDefault(c => c.ID == oSet.LocationToID);
                        if (obj2 != null)
                            eLocation = obj2.ID;
                    }
                }

                Dictionary<int, string> dicGroupProduct = new Dictionary<int, string>();
                Dictionary<int, string> dicFTLGroupProduct = new Dictionary<int, string>();
                Dictionary<int, double> dicGroupProductProfit = new Dictionary<int, double>();
                Dictionary<int, string> dicFTLMaster = new Dictionary<int, string>();
                Dictionary<int, string> dicLTLMaster = new Dictionary<int, string>();
                foreach (var gop in output.OrderTransportationPlans.GroupBy(c => c.FTLTripID).ToList())
                {
                    if (gop.Key == "-1")
                    {
                        foreach (var o in gop)
                        {
                            OPS_OPTDITOGroupProduct objGop = new OPS_OPTDITOGroupProduct();
                            objGop.CreatedBy = Account.UserName;
                            objGop.CreatedDate = DateTime.Now;
                            DITOGroupOfProduct_Create(model, optimizerID, o.OrderId, o.OrderSize, o.UnPlannedReasons, objGop);

                            model.OPS_OPTDITOGroupProduct.Add(objGop);
                            model.SaveChanges();

                            dicGroupProduct.Add(objGop.ID, o.TripIds.FirstOrDefault());
                            var objO = input.Orders.FirstOrDefault(c => c.ID == o.OrderId);
                            dicGroupProductProfit.Add(objGop.ID, objO.Profit);
                            if (!dicLTLMaster.ContainsKey(Convert.ToInt32(o.TripIds.FirstOrDefault())))
                                dicLTLMaster.Add(Convert.ToInt32(o.TripIds.FirstOrDefault()), gop.Key);
                        }
                    }
                    else
                    {
                        foreach (var o in gop)
                        {
                            OPS_OPTDITOGroupProduct objGop = new OPS_OPTDITOGroupProduct();
                            objGop.CreatedBy = Account.UserName;
                            objGop.CreatedDate = DateTime.Now;
                            DITOGroupOfProduct_Create(model, optimizerID, o.OrderId, o.OrderSize, o.UnPlannedReasons, objGop);

                            model.OPS_OPTDITOGroupProduct.Add(objGop);
                            model.SaveChanges();

                            dicGroupProduct.Add(objGop.ID, o.TripIds.FirstOrDefault());
                            dicFTLGroupProduct.Add(objGop.ID, gop.Key);
                            var objO = input.Orders.FirstOrDefault(c => c.ID == o.OrderId);
                            dicGroupProductProfit.Add(objGop.ID, objO.Profit);
                            if (!dicFTLMaster.ContainsKey(Convert.ToInt32(o.TripIds.FirstOrDefault())))
                                dicFTLMaster.Add(Convert.ToInt32(o.TripIds.FirstOrDefault()), gop.Key);
                        }
                    }
                }

                int idx = 1;
                //Lưu chuyến FTL.
                foreach (var group in dicFTLMaster.Where(c=> c.Key > 0).GroupBy(c => c.Value))
                {
                    OPS_OPTDITOMaster objDI = new OPS_OPTDITOMaster();
                    objDI.CreatedBy = Account.UserName;
                    objDI.CreatedDate = DateTime.Now;

                    objDI.OptimizerID = optimizerID;
                    objDI.SortOrder = 1;
                    objDI.Note = "Chuyến " + idx;
                    idx++;
                    var vehicleID = -1;
                    List<string> dataLocation = new List<string>();
                    foreach (var trip in group.OrderBy(c => c.Key).ToList())
                    {
                        var objT = output.Trips.FirstOrDefault(c => c.TripID == trip.Key.ToString());
                        if (objT != null)
                        {
                            if (objDI.ETD == null || objDI.ETD > objT.PickupDateTime)
                                objDI.ETD = objT.PickupDateTime;
                            if (objDI.ETA == null || objDI.ETA < objT.DeliveryDateTime)
                                objDI.ETA = objT.DeliveryDateTime;
                            objDI.KM += objT.Summary.TotalTravelDistance;
                            objDI.Debit += (decimal)objT.Summary.TotalCost;
                            var objV = input.Vehicles.FirstOrDefault(c => c.ID == objT.VehicleID);
                            vehicleID = Convert.ToInt32(objV.Optionals["CATVehicleID"]);
                            var fObj = input.Locations[objT.FromLocationId];
                            var tObj = input.Locations[objT.ToLocationId];
                            dataLocation.Add(fObj.Optionals["CATLocationID"]);
                            dataLocation.Add(tObj.Optionals["CATLocationID"]);
                        }
                    }
                    DITOMaster_Create(model, optimizerID, vehicleID, objDI);
                    model.OPS_OPTDITOMaster.Add(objDI);

                    dataLocation = dataLocation.Distinct().ToList();
                    int skip = 0;
                    int sOrder = 1;
                    int eID = 0;
                    //if (dataLocation.FirstOrDefault() == sLocation.ToString())
                    //    skip = 1;
                    //OPS_OPTDITOLocation oLocation0 = new OPS_OPTDITOLocation();
                    //oLocation0.LocationID = sLocation;
                    //oLocation0.SortOrder = sOrder++;
                    //oLocation0.CreatedBy = Account.UserName;
                    //oLocation0.CreatedDate = DateTime.Now;
                    //objDI.OPS_OPTDITOLocation.Add(oLocation0);

                    foreach (var id in dataLocation.Skip(skip).ToList())
                    {
                        OPS_OPTDITOLocation oLocation = new OPS_OPTDITOLocation();
                        oLocation.LocationID = Convert.ToInt32(id);
                        oLocation.SortOrder = sOrder++;
                        oLocation.CreatedBy = Account.UserName;
                        oLocation.CreatedDate = DateTime.Now;
                        objDI.OPS_OPTDITOLocation.Add(oLocation);
                        eID = Convert.ToInt32(id);
                    }
                    if (eID != eLocation)
                    {
                        //OPS_OPTDITOLocation oLocationE = new OPS_OPTDITOLocation();
                        //oLocationE.LocationID = sLocation;
                        //oLocationE.SortOrder = sOrder;
                        //oLocationE.CreatedBy = Account.UserName;
                        //oLocationE.CreatedDate = DateTime.Now;
                        //objDI.OPS_OPTDITOLocation.Add(oLocation0);
                    }

                    model.SaveChanges();
                    foreach (var item in dicFTLGroupProduct.Where(c => c.Value == group.Key).ToList())
                    {
                        var objDITO = model.OPS_OPTDITOGroupProduct.FirstOrDefault(c => c.ID == item.Key);
                        if (objDITO != null)
                            objDITO.OPTDITOMasterID = objDI.ID;
                    }
                    model.SaveChanges();
                }
                    
                //Lưu chuyến LTL.
                foreach (var group in dicLTLMaster.Where(c => c.Key > 0).ToList())
                {
                    var objT = output.Trips.FirstOrDefault(c => c.TripID == group.Key.ToString());
                    if (objT != null)
                    {
                        OPS_OPTDITOMaster objDI = new OPS_OPTDITOMaster();
                        objDI.CreatedBy = Account.UserName;
                        objDI.CreatedDate = DateTime.Now;

                        objDI.OptimizerID = optimizerID;
                        objDI.SortOrder = 1;
                        objDI.Note = "Chuyến " + idx;
                        idx++;
                        objDI.ETD = objT.PickupDateTime;
                        objDI.ETA = objT.DeliveryDateTime;
                        objDI.KM = objT.Summary.TotalTravelDistance;
                        objDI.Debit = (decimal)objT.Summary.TotalCost;
                        var objV = input.Vehicles.FirstOrDefault(c => c.ID == objT.VehicleID);
                        DITOMaster_Create(model, optimizerID, Convert.ToInt32(objV.Optionals["CATVehicleID"]), objDI);
                        model.OPS_OPTDITOMaster.Add(objDI);

                        int sOrder = 1;
                        //Nếu BĐ = điểm lấy hàng => Chỉ tạo điểm lấy hàng.
                        var fObj = input.Locations.FirstOrDefault(c => c.Key == objT.FromLocationId);
                        var tObj = input.Locations.FirstOrDefault(c => c.Key == objT.ToLocationId);
                        if (input.Locations.ContainsKey(objT.FromLocationId) && fObj.Value != null)
                        {
                            if (sLocation != Convert.ToInt32(fObj.Value.Optionals["CATLocationID"]))
                            {
                                //OPS_OPTDITOLocation oLocation0 = new OPS_OPTDITOLocation();
                                //oLocation0.LocationID = sLocation;
                                //oLocation0.SortOrder = sOrder++;
                                //oLocation0.CreatedBy = Account.UserName;
                                //oLocation0.CreatedDate = DateTime.Now;
                                //GetLocation_TimeWindow(output, objT.VehicleID, objT.TripID, sLocation, oLocation0.DateCome, oLocation0.DateLeave);
                                //objDI.OPS_OPTDITOLocation.Add(oLocation0);
                            }
                            //Điểm lấy hàng
                            OPS_OPTDITOLocation oLocation1 = new OPS_OPTDITOLocation();
                            oLocation1.LocationID = Convert.ToInt32(fObj.Value.Optionals["CATLocationID"]);
                            oLocation1.SortOrder = sOrder++;
                            oLocation1.CreatedBy = Account.UserName;
                            oLocation1.CreatedDate = DateTime.Now;
                            GetLocation_TimeWindow(output, objT.VehicleID, objT.TripID, oLocation1.LocationID, oLocation1.DateCome, oLocation1.DateLeave);
                            objDI.OPS_OPTDITOLocation.Add(oLocation1);
                        }
                        if (input.Locations.ContainsKey(objT.ToLocationId) && tObj.Value != null)
                        {
                            //Điểm giao hàng
                            OPS_OPTDITOLocation oLocation2 = new OPS_OPTDITOLocation();
                            oLocation2.LocationID = Convert.ToInt32(tObj.Value.Optionals["CATLocationID"]);
                            oLocation2.SortOrder = sOrder++;
                            oLocation2.CreatedBy = Account.UserName;
                            oLocation2.CreatedDate = DateTime.Now;
                            GetLocation_TimeWindow(output, objT.VehicleID, objT.TripID, oLocation2.LocationID, oLocation2.DateCome, oLocation2.DateLeave);
                            objDI.OPS_OPTDITOLocation.Add(oLocation2);

                            //Nếu != điểm giao hàng => tạo điểm kết thúc.
                            if (eLocation != oLocation2.LocationID)
                            {
                                //OPS_OPTDITOLocation oLocation3 = new OPS_OPTDITOLocation();
                                //oLocation3.LocationID = eLocation;
                                //oLocation3.SortOrder = sOrder++;
                                //oLocation3.CreatedBy = Account.UserName;
                                //oLocation3.CreatedDate = DateTime.Now;
                                //GetLocation_TimeWindow(output, objT.VehicleID, objT.TripID, oLocation3.LocationID, oLocation3.DateCome, oLocation3.DateLeave);
                                //objDI.OPS_OPTDITOLocation.Add(oLocation3);
                            }
                        }

                        model.SaveChanges();
                        foreach (var item in dicGroupProduct.Where(c => c.Value == group.Key.ToString()).ToList())
                        {
                            var objDITO = model.OPS_OPTDITOGroupProduct.FirstOrDefault(c => c.ID == item.Key);
                            if (objDITO != null)
                                objDITO.OPTDITOMasterID = objDI.ID;
                        }
                        model.SaveChanges();
                    }
                }

                //Dữ liệu tổng hợp theo xe.
                foreach (var vehicle in output.VehicleSummaries)
                {
                    //var objVehicle = model.OPS_OPTVehicle.FirstOrDefault(c => c.ID.ToString() == vehicle.VehicleID);
                    //if (objVehicle != null)
                    //{
                    //    objVehicle.ModifiedBy = Account.UserName;
                    //    objVehicle.ModifiedDate = DateTime.Now;
                    //    objVehicle.TotalCost = (decimal)vehicle.Summary.TotalCost;
                    //    objVehicle.TotalDistanceWithNoLoad = vehicle.Summary.TotalDistanceWithNoLoad;
                    //    objVehicle.TotalDrivingTime = vehicle.Summary.TotalDrivingTime;
                    //    objVehicle.TotalTravelDistance = vehicle.Summary.TotalTravelDistance;
                    //    objVehicle.TotalWaitingTime = vehicle.Summary.TotalWaitingTime;
                    //}
                }
                model.SaveChanges();
            }           
        }

        private void GetLocation_TimeWindow(Opt_DataOutput.TransportationPlanOutput output, string vehicleID, string tripID, int? catLocation, DateTime? from, DateTime? to)
        {
            try
            {
                //var objSchedule = output.VehicleSchedules.FirstOrDefault(c => c.VehicleID == vehicleID);
                //if (objSchedule != null)
                //{
                //    var dataCo = output.OrderTransportationPlans.Where(c => c.TripIds.Contains(tripID)).Select(c => c.OrderId);
                //    List<DTOOPTVehicleActivity> dataActivity = new List<DTOOPTVehicleActivity>();
                //    foreach (var o in objSchedule.ActivityList)
                //    {
                //        DTOOPTVehicleActivity activity = new DTOOPTVehicleActivity();
                //        var type = o.GetType();
                //        if (type == typeof(Opt_DataOutput.AtLocationActivity))
                //        {
                //            var cObj = (Opt_DataOutput.AtLocationActivity)o;

                //        }
                //        else if (type == typeof(Opt_DataOutput.LoadActivity))
                //        {
                //            var cObj = (Opt_DataOutput.LoadActivity)o;

                //        }
                //        else if (type == typeof(Opt_DataOutput.OnRoadActivity))
                //        {
                //            var cObj = (Opt_DataOutput.OnRoadActivity)o;

                //        }
                //        else if (type == typeof(Opt_DataOutput.UnloadActivity))
                //        {
                //            var cObj = (Opt_DataOutput.UnloadActivity)o;

                //        }
                //        else if (type == typeof(Opt_DataOutput.UnloadAndLoadActivity))
                //        {
                //            var cObj = (Opt_DataOutput.UnloadAndLoadActivity)o;

                //            activity.AtLocationID = Convert.ToInt32(cObj.AtLocation.Optionals["CATLocationID"]);
                //            activity.UnLoadOrderNames = new List<string>();
                //            activity.LoadOrderNames = new List<string>();
                //            if (cObj.AtLocation.Optionals["CATLocationID"] == catLocation.ToString())
                //            {

                //            }
                //        }
                //        else if (type == typeof(Opt_DataOutput.WaitActivity))
                //        {

                //        }
                //        else
                //        {

                //        }
                //    }
                //}
            }
            catch (Exception)
            {
            }
        }

        private void Order_Create(DataEntities model, int optimizerID, bool isCo, List<int> data)
        {
            List<int> dataOrder = new List<int>();
            if (isCo)
                dataOrder = model.OPS_COTOContainer.Where(c => data.Contains(c.ID)).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
            else
                dataOrder = model.OPS_DITOGroupProduct.Where(c => data.Contains(c.ID)).Select(c => c.ORD_GroupProduct.OrderID).Distinct().ToList();
            foreach (var id in dataOrder)
            {
                var obj = model.OPS_OPTOrder.FirstOrDefault(c => c.OptimizerID == optimizerID && c.OrderID == id);
                if (obj == null)
                {
                    obj = new OPS_OPTOrder();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;

                    obj.OptimizerID = optimizerID;
                    obj.OrderID = id;

                    model.OPS_OPTOrder.Add(obj);
                }
            }
        }

        private void Location_Create(DataEntities model, int optimizerID, bool isCo, List<int> data)
        {
            List<int> dataRouting = new List<int>();
            List<int> dataLocation = new List<int>();

            //Get DataLocation && DataRouting
            if (isCo)
            {
                foreach (var item in model.OPS_COTOContainer.Where(c => data.Contains(c.ID)).Select(c => new
                {
                    c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                    c.LocationFromID,
                    c.LocationToID
                }).ToList())
                {
                    dataLocation.Add(item.LocationToID);
                    dataLocation.Add(item.LocationFromID);

                    var objRoute = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == item.CustomerID && c.CAT_Routing.LocationFromID == item.LocationFromID && c.CAT_Routing.LocationToID == item.LocationToID);
                    if (objRoute != null)
                    {
                        dataRouting.Add(objRoute.RoutingID);
                    }
                    else
                    {
                        var obj = model.CUS_Routing.Where(c => c.CustomerID == item.CustomerID && c.CAT_Routing.RoutingAreaFromID > 0 && c.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(o => o.LocationID == item.LocationFromID)
                            && c.CAT_Routing.RoutingAreaToID > 0 && c.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(o => o.LocationID == item.LocationToID)).FirstOrDefault();
                        if (obj != null)
                            dataRouting.Add(obj.RoutingID);
                    }
                }
            }
            else
            {
                foreach (var item in model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && data.Contains(c.ID)).Select(c => new
                {
                    c.ORD_GroupProduct.ORD_Order.CustomerID,
                    c.ORD_GroupProduct.LocationFromID,
                    c.ORD_GroupProduct.LocationToID
                }).ToList())
                {
                    int? fID = null, tID = null;
                    var obj1 = model.CUS_Location.FirstOrDefault(c => c.ID == item.LocationFromID);
                    if (obj1 != null)
                    {
                        fID = obj1.LocationID;
                        dataLocation.Add(fID.Value);
                    }
                    var obj2 = model.CUS_Location.FirstOrDefault(c => c.ID == item.LocationToID);
                    if (obj2 != null)
                    {
                        tID = obj2.LocationID;
                        dataLocation.Add(tID.Value);
                    }

                    var objRoute = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == item.CustomerID && c.CAT_Routing.LocationFromID == fID && c.CAT_Routing.LocationToID == tID);
                    if (objRoute != null)
                    {
                        dataRouting.Add(objRoute.RoutingID);
                    }
                    else
                    {
                        var obj = model.CUS_Routing.Where(c => c.CustomerID == item.CustomerID && c.CAT_Routing.RoutingAreaFromID > 0 && c.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(o => o.LocationID == fID)
                            && c.CAT_Routing.RoutingAreaToID > 0 && c.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(o => o.LocationID == tID)).FirstOrDefault();
                        if (obj != null)
                            dataRouting.Add(obj.RoutingID);
                    }
                }
            }
            //Save Route && RouteRequire
            foreach (var item in dataRouting.Distinct().ToList())
            {
                if (model.OPS_OPTRouting.Count(c => c.OptimizerID == optimizerID && c.RoutingID == item) == 0)
                {
                    OPS_OPTRouting obj = new OPS_OPTRouting();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.OptimizerID = optimizerID;
                    obj.RoutingID = item;

                    model.OPS_OPTRouting.Add(obj);

                    foreach (var o in model.CAT_RoutingRequire.Where(c => c.RoutingID == item).ToList())
                    {
                        OPS_OPTRoutingRequire e = new OPS_OPTRoutingRequire();
                        e.CreatedBy = Account.UserName;
                        e.CreatedDate = DateTime.Now;

                        e.Width = o.Width;
                        e.Weight = o.Weight;
                        e.Height = o.Height;
                        e.Length = o.Length;

                        e.TimeFrom = o.TimeFrom;
                        e.TimeTo = o.TimeTo;
                        e.ConstraintRequireTypeID = o.ConstraintRequireTypeID;

                        obj.OPS_OPTRoutingRequire.Add(e);
                    }
                }
            }
            //Save Location && LocationRequire
            foreach (var item in dataLocation.Distinct().ToList())
            {
                if (model.OPS_OPTLocation.Count(c => c.OptimizerID == optimizerID && c.LocationID == item) == 0)
                {
                    OPS_OPTLocation obj = new OPS_OPTLocation();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.OptimizerID = optimizerID;
                    obj.LocationID = item;

                    model.OPS_OPTLocation.Add(obj);

                    foreach (var o in model.CAT_LocationRequire.Where(c => c.LocationID == item).ToList())
                    {
                        OPS_OPTLocationRequire e = new OPS_OPTLocationRequire();
                        e.CreatedBy = Account.UserName;
                        e.CreatedDate = DateTime.Now;

                        e.Width = o.Width;
                        e.Weight = o.Weight;
                        e.Height = o.Height;
                        e.Length = o.Length;

                        e.TimeFrom = o.TimeFrom;
                        e.TimeTo = o.TimeTo;
                        e.ConstraintRequireTypeID = o.ConstraintRequireTypeID;

                        obj.OPS_OPTLocationRequire.Add(e);
                    }
                }
            }
        }

        private void Location_Reset(DataEntities model, int optimizerID, bool isCo)
        {
            List<int> dataRouting = new List<int>();
            List<int> dataLocation = new List<int>();
            if (isCo)
            {
                foreach (var item in model.OPS_OPTOPSContainer.Where(c => c.OptimizerID == optimizerID).Select(c => new
                {
                    c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                    c.OPS_COTOContainer.LocationFromID,
                    c.OPS_COTOContainer.LocationToID
                }).ToList())
                {
                    dataLocation.Add(item.LocationToID);
                    dataLocation.Add(item.LocationFromID);

                    var objRoute = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == item.CustomerID && c.CAT_Routing.LocationFromID == item.LocationFromID && c.CAT_Routing.LocationToID == item.LocationToID);
                    if (objRoute != null)
                    {
                        dataRouting.Add(objRoute.RoutingID);
                    }
                    else
                    {
                        var obj = model.CUS_Routing.Where(c => c.CustomerID == item.CustomerID && c.CAT_Routing.RoutingAreaFromID > 0 && c.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(o => o.LocationID == item.LocationFromID)
                            && c.CAT_Routing.RoutingAreaToID > 0 && c.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(o => o.LocationID == item.LocationToID)).FirstOrDefault();
                        if (obj != null)
                            dataRouting.Add(obj.RoutingID);
                    }
                }
            }
            else
            {
                foreach (var item in model.OPS_OPTOPSGroupProduct.Where(c => c.OptimizerID == optimizerID).Select(c => new
                {
                    c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID,
                    c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID,
                    c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID
                }).ToList())
                {
                    int? fID = null, tID = null;
                    var obj1 = model.CUS_Location.FirstOrDefault(c => c.ID == item.LocationFromID);
                    if (obj1 != null)
                    {
                        fID = obj1.LocationID;
                        dataLocation.Add(fID.Value);
                    }
                    var obj2 = model.CUS_Location.FirstOrDefault(c => c.ID == item.LocationToID);
                    if (obj2 != null)
                    {
                        tID = obj2.LocationID;
                        dataLocation.Add(tID.Value);
                    }

                    var objRoute = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == item.CustomerID && c.CAT_Routing.LocationFromID == fID && c.CAT_Routing.LocationToID == tID);
                    if (objRoute != null)
                    {
                        dataRouting.Add(objRoute.RoutingID);
                    }
                    else
                    {
                        var obj = model.CUS_Routing.Where(c => c.CustomerID == item.CustomerID && c.CAT_Routing.RoutingAreaFromID > 0 && c.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(o => o.LocationID == fID)
                            && c.CAT_Routing.RoutingAreaToID > 0 && c.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(o => o.LocationID == tID)).FirstOrDefault();
                        if (obj != null)
                            dataRouting.Add(obj.RoutingID);
                    }
                }
            }
            dataRouting = dataRouting.Distinct().ToList();
            dataLocation = dataLocation.Distinct().ToList();

            foreach (var item in model.OPS_OPTLocation.Where(c => c.OptimizerID == optimizerID && !dataLocation.Contains(c.LocationID)).ToList())
            {
                model.OPS_OPTLocationRequire.RemoveRange(item.OPS_OPTLocationRequire);
                model.OPS_OPTLocation.Remove(item);
            }

            foreach (var item in model.OPS_OPTRouting.Where(c => c.OptimizerID == optimizerID && !dataRouting.Contains(c.RoutingID)).ToList())
            {
                model.OPS_OPTRoutingRequire.RemoveRange(item.OPS_OPTRoutingRequire);
                model.OPS_OPTRouting.Remove(item);
            }

            model.SaveChanges();
        }

        private void Optimizer_Reset(DataEntities model, int optimizerID)
        {
            foreach (var item in model.OPS_OPTCOTOMaster.Where(c => c.OptimizerID == optimizerID).ToList())
            {
                foreach (var o in model.OPS_OPTCOTOLocation.Where(c => c.OPTCOTOMasterID == item.ID).ToList())
                {
                    model.OPS_OPTCOTOLocation.Remove(o);
                }
                foreach (var o in model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == item.ID).ToList())
                {
                    model.OPS_OPTCOTOContainer.Remove(o);
                }
                foreach (var o in model.OPS_OPTCOTODetail.Where(c => c.OPS_OPTCOTOContainer.OPTCOTOMasterID == item.ID || c.OPS_OPTCOTOLocation.OPTCOTOMasterID == item.ID).ToList())
                {
                    model.OPS_OPTCOTODetail.Remove(o);
                }
                model.OPS_OPTCOTOMaster.Remove(item);
                HelperTimeSheet.Remove(model, Account, item.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
            }
            foreach (var o in model.OPS_OPTCOTOContainer.Where(c => c.OPS_OPTOPSContainer.OptimizerID == optimizerID && c.OPTCOTOMasterID == null).ToList())
            {
                model.OPS_OPTCOTOContainer.Remove(o);
            }

            foreach (var item in model.OPS_OPTDITOMaster.Where(c => c.OptimizerID == optimizerID).ToList())
            {
                foreach (var o in model.OPS_OPTDITOLocation.Where(c => c.OPTDITOMasterID == item.ID).ToList())
                {
                    model.OPS_OPTDITOLocation.Remove(o);
                }
                foreach (var o in model.OPS_OPTDITOGroupProduct.Where(c => c.OPTDITOMasterID == item.ID).ToList())
                {
                    model.OPS_OPTDITOGroupProduct.Remove(o);
                }
                foreach (var o in model.OPS_OPTDITODetail.Where(c => c.OPS_OPTDITOGroupProduct.OPTDITOMasterID == item.ID || c.OPS_OPTDITOLocation.OPTDITOMasterID == item.ID).ToList())
                {
                    model.OPS_OPTDITODetail.Remove(o);
                }
                model.OPS_OPTDITOMaster.Remove(item);
                HelperTimeSheet.Remove(model, Account, item.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster);
            }
            foreach (var o in model.OPS_OPTDITOGroupProduct.Where(c => c.OPS_OPTOPSGroupProduct.OptimizerID == optimizerID && c.OPTDITOMasterID == null).ToList())
            {
                model.OPS_OPTDITOGroupProduct.Remove(o);
            }
            
            foreach (var item in model.OPS_OPTOPSContainer.Where(c => c.OptimizerID == optimizerID).ToList())
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                item.Note = string.Empty;
            }
            foreach (var item in model.OPS_OPTOPSGroupProduct.Where(c => c.OptimizerID == optimizerID).ToList())
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                item.Note = string.Empty;
            }
        }

        private void COTOMaster_Create(DataEntities model, int optimizerID, int? vehicleID, int? romoocID, OPS_OPTCOTOMaster item)
        {
            var obj = model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID && c.VehicleID == vehicleID).Select(c => new
            {
                c.VehicleID,
                c.RomoocID,
                c.CAT_Vehicle.DriverID,
                c.CAT_Vehicle.DriverName,
                c.CAT_Vehicle.Cellphone,
                c.CAT_Vehicle.AssistantID
            }).FirstOrDefault();
            if (obj != null)
            {
                item.VehicleID = obj.VehicleID;
                item.DriverID1 = obj.DriverID;
                item.DriverName1 = obj.DriverName;
                item.DriverTel1 = obj.Cellphone;
                item.AssistantID1 = obj.AssistantID;

                if (romoocID == null)
                {
                    item.RomoocID = obj.RomoocID;
                }
                else
                {
                    var objR = model.OPS_OPTRomooc.FirstOrDefault(c => c.OptimizerID == optimizerID && c.RomoocID == romoocID);
                    if (objR != null)
                        item.RomoocID = objR.RomoocID;
                }
            }
        }

        private void COTOContainer_Create(DataEntities model, int optimizerID, string optContainerID, List<string> sNote, OPS_OPTCOTOContainer item)
        {
            var obj = model.OPS_OPTOPSContainer.FirstOrDefault(c => c.OptimizerID == optimizerID && c.ID.ToString() == optContainerID);
            if (obj != null)
            {
                if (sNote != null && sNote.Count > 0)
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                    obj.Note = string.Join(", ", sNote.Distinct().ToList());
                }

                var objCO = obj.OPS_COTOContainer;
                item.OPTOPSContainerID = obj.ID;
                item.ETD = obj.ETD;
                item.ETDStart = obj.ETDStart;
                item.ETA = obj.ETA;
                item.ETAStart = obj.ETAStart;
                item.StatusOfCOContainerID = objCO.StatusOfCOContainerID;
                item.SortOrder = 1;
            }
        }

        private void COTOLocation_Create(DataEntities model, int optimizerID, string optLocation1, string optLocation2, OPS_OPTCOTOContainer item)
        {
            var fLocation = model.OPS_OPTLocation.FirstOrDefault(c => c.OptimizerID == optimizerID && c.ID.ToString() == optLocation1);
            if (fLocation != null)
                item.LocationFromID = fLocation.LocationID;
            var tLocation = model.OPS_OPTLocation.FirstOrDefault(c => c.OptimizerID == optimizerID && c.ID.ToString() == optLocation2);
            if (tLocation != null)
                item.LocationToID = tLocation.LocationID;
        }

        private void DITOGroupOfProduct_Create(DataEntities model, int optimizerID, string optGopID, Opt_Data.MultiDimSize orderSize, List<string> sNote, OPS_OPTDITOGroupProduct item)
        {
            var obj = model.OPS_OPTOPSGroupProduct.FirstOrDefault(c => c.OptimizerID == optimizerID && c.ID.ToString() == optGopID);
            if (obj != null)
            {
                if (sNote != null && sNote.Count > 0)
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                    obj.Note = string.Join(", ", sNote.Distinct().ToList());
                }

                var objDI = obj.OPS_DITOGroupProduct;
                item.OPTOPSGroupProductID = obj.ID;
                item.ETD = obj.ETD;
                //item.ETDStart = objDI.ORD_GroupProduct.ETDStart;
                item.ETA = obj.ETA;
                //item.ETAStart = objDI.ORD_GroupProduct.ETAStart;
                if (orderSize != null)
                {
                    item.Ton = orderSize.Weight.HasValue ? orderSize.Weight.Value : 0;
                    item.CBM = orderSize.Cubic.HasValue ? orderSize.Cubic.Value : 0;
                    item.Quantity = orderSize.Qty.HasValue ? orderSize.Qty.Value : 0;
                }
            }
        }

        private void DITOMaster_Create(DataEntities model, int optimizerID, int? vehicleID, OPS_OPTDITOMaster item)
        {
            var obj = model.OPS_OPTVehicle.Where(c => c.OptimizerID == optimizerID && c.VehicleID == vehicleID).Select(c => new
            {
                c.VehicleID,
                c.CAT_Vehicle.DriverID,
                c.CAT_Vehicle.DriverName,
                c.CAT_Vehicle.Cellphone,
                c.CAT_Vehicle.AssistantID,
                c.CAT_Vehicle.GroupOfVehicleID
            }).FirstOrDefault();
            if (obj != null)
            {
                item.VehicleID = obj.VehicleID;
                item.DriverID1 = obj.DriverID;
                item.DriverName1 = obj.DriverName;
                item.DriverTel1 = obj.Cellphone;
                item.AssistantID1 = obj.AssistantID;
                item.GroupOfVehicleID = obj.GroupOfVehicleID;
            }
        }
        
        private void DITOGroupOfProduct_Reset(DataEntities model, int optimizerID)
        {
            var data = model.OPS_OPTDITOGroupProduct.Where(c => c.OPTDITOMasterID == null && c.OPS_OPTOPSGroupProduct.OptimizerID == optimizerID).GroupBy(c=> c.OPTOPSGroupProductID).ToList();
            foreach (var group in data)
            {
                if (group.ToList().Count > 1)
                {
                    var first = group.FirstOrDefault();
                    first.ModifiedBy = Account.UserName;
                    first.ModifiedDate = DateTime.Now;
                    foreach (var item in group.Where(c => c.ID != first.ID).ToList())
                    {
                        first.Ton += item.Ton;
                        first.CBM += item.CBM;
                        first.Quantity += item.Quantity;
                        model.OPS_OPTDITOGroupProduct.Remove(item);
                    }
                    first.Ton = Math.Round(first.Ton, NoDigit, MidpointRounding.AwayFromZero);
                    first.CBM = Math.Round(first.CBM, NoDigit, MidpointRounding.AwayFromZero);
                    first.Quantity = Math.Round(first.Quantity, NoDigit, MidpointRounding.AwayFromZero);
                }
            }
            model.SaveChanges();
        }

        private double MaxWeight(DataEntities model, int vehicleID, int? romoocID)
        {
            var value = (double)0;
            var maxWeightVehicle = model.CAT_Vehicle.Where(c => c.ID == vehicleID).Select(c => c.MaxWeight).FirstOrDefault();
            var maxWeightRomooc = model.CAT_Romooc.Where(c => c.ID == romoocID).Select(c => c.MaxWeight).FirstOrDefault();
            value = maxWeightVehicle > 0 ? maxWeightVehicle.Value : 0;
            if (maxWeightVehicle > 0)
                value = Math.Min(maxWeightVehicle.Value, value);
            return value;
        }

        private string CO_GetLastCode(DataEntities model)
        {
            long idx = 1;
            var last = model.OPS_COTOMaster.OrderByDescending(c => c.ID).Select(c => new { c.ID }).FirstOrDefault();
            if (last != null)
                idx = Convert.ToInt64(last.ID) + 1;
            else
                idx = 1;
            return COCodeRrefix + idx.ToString(COCodeNum);
        }

        private string DI_GetLastCode(DataEntities model)
        {
            long idx = 1;
            var last = model.OPS_DITOMaster.OrderByDescending(c => c.ID).Select(c => new { c.ID }).FirstOrDefault();
            if (last != null)
                idx = Convert.ToInt64(last.ID) + 1;
            else
                idx = 1;
            return DICodeRrefix + idx.ToString(DICodeNum);
        }
    }
}