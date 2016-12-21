/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingPlanDetail = {
    URL: {
        Get: 'CUSSettingPlan_Get',
        Save: 'CUSSettingPlan_Save',
        Delete: 'CUSSettingPlan_Delete',
        SettingOrderList: 'CUSSettingPlan_SettingOrderList',

        GetSettingOrder: 'CUSSettingOrder_Get',
        Get_StockByCus: 'CUSSettingORD_StockByCus_List',
    },
    Data: {
        GridAllSetting: [
            { Code: "VendorCode", Name: "Mã nhà xe" },
            { Code: "VehicleNo", Name: "Số xe" },
            { Code: "MasterETDDate_Time", Name: "Ngày-giờ ETD" },
            { Code: "MasterETDDate", Name: "Ngày ETD" },
            { Code: "MasterETDTime", Name: "Giờ ETD" },
            { Code: "MasterETADate_Time", Name: "Ngày-giờ ETA" },
            { Code: "MasterETADate", Name: "Ngày ETA" },
            { Code: "MasterETATime", Name: "Giờ ETA" },
            { Code: "MasterNote", Name: "Ghi chú chuyến" },
            { Code: "MasterGroupVehicle", Name: "Loại xe" },
            { Code: "DriverName", Name: "Tên tài xế" },
            { Code: "DriverTel", Name: "SĐT tài xế" },
            { Code: "MasterSortOrder", Name: "Số chuyến" },
            { Code: "LocationFromProvince", Name: "Tỉnh thành điểm lấy" },
            { Code: "LocationFromDistrict", Name: "Quận huyện điểm lấy" },
            { Code: "LocationToProvince", Name: "Tỉnh thành điểm giao" },
            { Code: "LocationToDistrict", Name: "Quận huyện điểm giao" },
            { Code: "MasterHours", Name: "Thời gian chuyến" },
            { Code: "RomoocNo", Name: "Số Romooc" }
        ],
        GridSelected: [
            { Type: 0, Column: 'A', Code: "", Name: "", IndexColumn: 1, isOrder: 0 },
            { Type: 0, Column: 'B', Code: "", Name: "", IndexColumn: 2, isOrder: 0 },
            { Type: 0, Column: 'C', Code: "", Name: "", IndexColumn: 3, isOrder: 0 },
            { Type: 0, Column: 'D', Code: "", Name: "", IndexColumn: 4, isOrder: 0 },
            { Type: 0, Column: 'E', Code: "", Name: "", IndexColumn: 5, isOrder: 0 },
            { Type: 0, Column: 'F', Code: "", Name: "", IndexColumn: 6, isOrder: 0 },
            { Type: 0, Column: 'G', Code: "", Name: "", IndexColumn: 7, isOrder: 0 },
            { Type: 0, Column: 'H', Code: "", Name: "", IndexColumn: 8, isOrder: 0 },
            { Type: 0, Column: 'I', Code: "", Name: "", IndexColumn: 9, isOrder: 0 },
            { Type: 0, Column: 'J', Code: "", Name: "", IndexColumn: 10, isOrder: 0 },
            { Type: 0, Column: 'K', Code: "", Name: "", IndexColumn: 11, isOrder: 0 },
            { Type: 0, Column: 'L', Code: "", Name: "", IndexColumn: 12, isOrder: 0 },
            { Type: 0, Column: 'M', Code: "", Name: "", IndexColumn: 13, isOrder: 0 },
            { Type: 0, Column: 'N', Code: "", Name: "", IndexColumn: 14, isOrder: 0 },
            { Type: 0, Column: 'O', Code: "", Name: "", IndexColumn: 15, isOrder: 0 },
            { Type: 0, Column: 'P', Code: "", Name: "", IndexColumn: 16, isOrder: 0 },
            { Type: 0, Column: 'Q', Code: "", Name: "", IndexColumn: 17, isOrder: 0 },
            { Type: 0, Column: 'R', Code: "", Name: "", IndexColumn: 18, isOrder: 0 },
            { Type: 0, Column: 'S', Code: "", Name: "", IndexColumn: 19, isOrder: 0 },
            { Type: 0, Column: 'T', Code: "", Name: "", IndexColumn: 20, isOrder: 0 },
            { Type: 0, Column: 'U', Code: "", Name: "", IndexColumn: 21, isOrder: 0 },
            { Type: 0, Column: 'V', Code: "", Name: "", IndexColumn: 22, isOrder: 0 },
            { Type: 0, Column: 'W', Code: "", Name: "", IndexColumn: 23, isOrder: 0 },
            { Type: 0, Column: 'X', Code: "", Name: "", IndexColumn: 24, isOrder: 0 },
            { Type: 0, Column: 'Y', Code: "", Name: "", IndexColumn: 25, isOrder: 0 },
            { Type: 0, Column: 'Z', Code: "", Name: "", IndexColumn: 26, isOrder: 0 },
            { Type: 0, Column: 'AA', Code: "", Name: "", IndexColumn: 27, isOrder: 0 },
            { Type: 0, Column: 'AB', Code: "", Name: "", IndexColumn: 28, isOrder: 0 },
            { Type: 0, Column: 'AC', Code: "", Name: "", IndexColumn: 29, isOrder: 0 },
            { Type: 0, Column: 'AD', Code: "", Name: "", IndexColumn: 30, isOrder: 0 },
            { Type: 0, Column: 'AE', Code: "", Name: "", IndexColumn: 31, isOrder: 0 },
            { Type: 0, Column: 'AF', Code: "", Name: "", IndexColumn: 32, isOrder: 0 },
            { Type: 0, Column: 'AG', Code: "", Name: "", IndexColumn: 33, isOrder: 0 },
            { Type: 0, Column: 'AH', Code: "", Name: "", IndexColumn: 34, isOrder: 0 },
            { Type: 0, Column: 'AI', Code: "", Name: "", IndexColumn: 35, isOrder: 0 },
            { Type: 0, Column: 'AJ', Code: "", Name: "", IndexColumn: 36, isOrder: 0 },
            { Type: 0, Column: 'AK', Code: "", Name: "", IndexColumn: 37, isOrder: 0 },
            { Type: 0, Column: 'AL', Code: "", Name: "", IndexColumn: 38, isOrder: 0 },
            { Type: 0, Column: 'AM', Code: "", Name: "", IndexColumn: 39, isOrder: 0 },
            { Type: 0, Column: 'AN', Code: "", Name: "", IndexColumn: 40, isOrder: 0 },
            { Type: 0, Column: 'AO', Code: "", Name: "", IndexColumn: 41, isOrder: 0 },
            { Type: 0, Column: 'AP', Code: "", Name: "", IndexColumn: 42, isOrder: 0 },
            { Type: 0, Column: 'AQ', Code: "", Name: "", IndexColumn: 43, isOrder: 0 },
            { Type: 0, Column: 'AR', Code: "", Name: "", IndexColumn: 44, isOrder: 0 },
            { Type: 0, Column: 'AS', Code: "", Name: "", IndexColumn: 45, isOrder: 0 },
            { Type: 0, Column: 'AT', Code: "", Name: "", IndexColumn: 46, isOrder: 0 },
            { Type: 0, Column: 'AU', Code: "", Name: "", IndexColumn: 47, isOrder: 0 },
            { Type: 0, Column: 'AV', Code: "", Name: "", IndexColumn: 48, isOrder: 0 },
            { Type: 0, Column: 'AW', Code: "", Name: "", IndexColumn: 49, isOrder: 0 },
            { Type: 0, Column: 'AX', Code: "", Name: "", IndexColumn: 50, isOrder: 0 },
            { Type: 0, Column: 'AY', Code: "", Name: "", IndexColumn: 51, isOrder: 0 },
            { Type: 0, Column: 'AZ', Code: "", Name: "", IndexColumn: 52, isOrder: 0 },
        ],
        GridAllSettingOrder: [
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "ServiceOfOrder", Name: "Loại dịch vụ" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "TypeOfTransportMode", Name: "Loại vận chuyển" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "OrderCode", Name: "Mã đơn hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "SOCode", Name: "Số SO" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DNCode", Name: "Số DN" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "RequestDate", Name: "Ngày gửi y/c" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "RequestTime", Name: "Giờ gửi y/c" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "RequestDate_Time", Name: "Ngày-giờ y/c" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "ETD", Name: "ETD" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETDTime_RequestDate", Name: "ETD tính theo giờ" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "ETA", Name: "ETA" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETATime_RequestDate", Name: "ETA tính theo giờ" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETARequest", Name: "Ngày y/c giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ETARequestTime", Name: "Giờ y/c giao hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "CustomerCode", Name: "Mã Khách hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CustomerName", Name: "Tên khách hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "CustomerCodeName", Name: "Mã-Tên khách hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "DistributorCode", Name: "Mã Nhà phân phối" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DistributorName", Name: "Tên Nhà phân phối" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "DistributorCodeName", Name: "Mã-Tên Nhà phân phối" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationFromCode", Name: "Mã điểm lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationFromName", Name: "Tên điểm lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationFromCodeName", Name: "Mã-Tên điểm lấy hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationToCode", Name: "Mã điểm giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToName", Name: "Tên điểm giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToAddress", Name: "Địa chỉ" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "LocationToCodeName", Name: "Mã-Tên điểm giao hàng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToNote", Name: "Ghi chú điểm đến" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationToNote1", Name: "Ghi chú điểm đến 1" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "GroupProductCode", Name: "Mã nhóm hàng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "GroupProductCodeNotUnicode", Name: "Mã nhóm hàng không dấu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TemperatureMax", Name: "Nhiệt độ tối đa" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TemperatureMin", Name: "Nhiệt độ tối thiểu" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "Packing", Name: "Đơn vị tính" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "PackingNotUnicode", Name: "Đơn vị tính không dấu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Ton", Name: "Ton" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "IsHot", Name: "Đơn hàng gấp" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CBM", Name: "CBM" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Quantity", Name: "Số lượng" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "GroupVehicle", Name: "Loại xe" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "RoutingCode", Name: "Mã cung đường" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Note", Name: "Ghi chú" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "HasCashCollect", Name: "Thu hộ" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "EconomicZone", Name: "EconomicZone" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceTOMaster", Name: "Giá chuyến" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceTon", Name: "Giá Tấn" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceCBM", Name: "Giá khối" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "PriceQuantity", Name: "Giá số lượng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine1", Name: "UserDefine1" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine2", Name: "UserDefine2" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine3", Name: "UserDefine3" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine4", Name: "UserDefine4" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine5", Name: "UserDefine5" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine6", Name: "UserDefine6" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine7", Name: "UserDefine7" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine8", Name: "UserDefine8" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "UserDefine9", Name: "UserDefine9" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Note1", Name: "Ghi chú 1" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Note2", Name: "Ghi chú 2" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CutOffTime", Name: "CutOffTime" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "CarrierCode", Name: "Mã Hãng Tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CarrierName", Name: "Tên Hãng Tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CarrierCodeName", Name: "Mã - Tên Hãng Tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "VesselNo", Name: "Số hiệu tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "VesselName", Name: "Tên tàu" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TripNo", Name: "Số chuyến" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ContainerNo", Name: "Số container" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "SealNo1", Name: "Số Seal thứ nhất" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "SealNo2", Name: "Số Seal thứ hai" },
            { GOPID: -1, ProductID: -1, Required: true, Type: 0, StockID: 0, IsStock: false, Code: "TypeOfContainerName", Name: "Loại container" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationDepotCode", Name: "Mã điểm lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationDepotName", Name: "Tên điểm lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationReturnCode", Name: "Mã điểm trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "LocationReturnName", Name: "Tên điểm trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DateGetEmpty", Name: "Ngày lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Date_TimeGetEmpty", Name: "Ngày-giờ lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TimeGetEmpty", Name: "Giờ lấy rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "DateReturnEmpty", Name: "Ngày trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TimeReturnEmpty", Name: "Giờ trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Date_TimeReturnEmpty", Name: "Ngày-Giờ trả rỗng" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "TypeOfWAInspectionStatus", Name: "Miễn kiểm hoá" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "InspectionDate", Name: "Thời gian gửi tờ khai kiểm hoá" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "ProductCodeWithoutGroup", Name: "Mã hàng hóa không cần nhóm" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Ton_SKU", Name: "Tấn SKU" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "CBM_SKU", Name: "Khối SKU" },
            { GOPID: -1, ProductID: -1, Required: false, Type: 0, StockID: 0, IsStock: false, Code: "Quantity_SKU", Name: "Số lượng SKU" },
        ],
    },
    Param: { ID: 0 },
}

angular.module('myapp').controller('CUSSettingPlan_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingPlan_DetailCtrl');
    $rootScope.IsLoading = false;
    $scope.SettingItem = null;
    $scope.SettingOrder = '';
    _CUSSettingPlanDetail.Param = $.extend(true, _CUSSettingPlanDetail.Param, $state.params);

    $scope.splitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: false, size: "50%", min: '250px' },
            { collapsible: false, resizable: false, size: "50%", min: '250px' }
        ]
    }
    $scope.AllSettingGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsStock: { type: 'boolean' }
                }
            },
            pageSize: 0
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: false, selectable: 'row',
        columns: [
            { field: 'Code', width: "100px", title: 'Mã' },
            { field: 'Name', width: "150px", title: 'Tên' },
        ],
        change: function (e) {
            var selected = this.select();
            if (selected.length > 0) {
                $scope.TranferItem = this.dataItem(selected);
                $timeout(function () {
                    $scope.RevertItem = null;
                    $scope.SelectedSettingGrid.clearSelection();
                }, 1)
            }
        }
    }

    $scope.SelectedSettingGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'IndexColumn',
                fields: {
                    IndexColumn: { type: 'number' },
                    isOrder: { type: 'number' },
                    Column: { type: 'string' },
                    Code: { type: 'string' },
                    Name: { type: 'string' },
                }
            },
            pageSize: 0
        }),
        //rowTemplate: kendo.template($("#rowTemplate").html()),
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: false, selectable: 'row',
        columns: [
            {
                field: 'Column', width: "100px", title: 'Cột Excel',
            },
            { field: 'Code', width: "100px", title: 'Mã' },
            { field: 'Name', width: "150px", title: 'Tên' },
        ],
        change: function (e) {
            var selected = this.select();

            if (selected.length > 0) {
                var item = this.dataItem(selected);
                //co item chuuyen
                if (Common.HasValue($scope.TranferItem) && item.isOrder != 1) {
                    if (item.Code == "" && item.Name == "") {//chi chuyen vao item chua dc chọn
                        item.Code = $scope.TranferItem.Code;
                        item.Name = $scope.TranferItem.Name;
                        $timeout(function () {
                            $scope.AllSettingGrid_Options.dataSource.remove($scope.TranferItem);
                            $scope.TranferItem = null;
                            $scope.AllSettingGrid.clearSelection()
                            $scope.SelectedSettingGrid.clearSelection()
                        }, 1)
                    }
                }
                else {
                    //neu ko co item revert thi select row
                    if (!Common.HasValue($scope.RevertItem) && item.isOrder != 1) {
                        if (item.Code != "" && item.Name != "") {
                            $scope.RevertItem = item;
                            $scope.PreItem = item;
                        }
                        else $scope.RevertItem = null
                    }
                    else if (item.isOrder != 1) {
                        // co item revert => change loction
                        var itemBackup = $.extend(true, itemBackup, item)
                        item.Code = $scope.RevertItem.Code;
                        item.Name = $scope.RevertItem.Name;
                        item.StockID = $scope.RevertItem.StockID;
                        item.Type = $scope.RevertItem.Type;

                        $scope.PreItem.Code = itemBackup.Code;
                        $scope.PreItem.Name = itemBackup.Name;
                        $scope.PreItem.StockID = itemBackup.StockID;
                        $scope.PreItem.Type = itemBackup.Type;

                        $timeout(function () {
                            $scope.RevertItem = null;
                            $scope.PreItem = null;
                            $scope.AllSettingGrid.clearSelection()
                            $scope.SelectedSettingGrid.clearSelection()
                        }, 1)
                    }
                }
            }

        },
        dataBound: function (e) {

            var grid = this;
            if (Common.HasValue(grid) && Common.HasValue(grid.tbody)) {
                var list = grid.tbody.find('tr');
                Common.Data.Each(list, function (o) {
                    var dataItem = grid.dataItem(o);
                    if (Common.HasValue(dataItem)) {
                        if (dataItem.isOrder == 1) {
                            $(o).css("background-color", "#fda");
                        }
                    }
                })
            }
        }
    }

    //$scope.SelectedSettingGrid_Options.dataSource.data(_CUSSettingOrderDetail.Data.GridSelected)

    $scope.Revert_Click = function ($event) {
        if (Common.HasValue($scope.RevertItem)) {
            var item = { Code: $scope.RevertItem.Code, Name: $scope.RevertItem.Name };
            $scope.AllSettingGrid_Options.dataSource.insert(0, item);

            var itemrevert = $scope.SelectedSettingGrid.dataItem($scope.SelectedSettingGrid.select())

            if (Common.HasValue(itemrevert)) {
                itemrevert.Code = "";
                itemrevert.Name = "";
                itemrevert.StockID = 0;
                itemrevert.Type = 0;
            }
            $scope.RevertItem = null;
            $scope.SelectedSettingGrid.clearSelection()
        }
    }

    $scope.LoadSetting = function () {
        $rootScope.IsLoading = true;

        //$scope.SelectedSettingGrid_Options.dataSource.data(_CUSSettingPlanDetail.Data.GridSelected);
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingPlanDetail.URL.GetSettingOrder,
            data: { id: $scope.SettingItem.CUSSettingOrderID },
            success: function (res) {

                $scope.SettingOrder = res.Name;


                var dataOrder = $.extend(true, [], _CUSSettingPlanDetail.Data.GridSelected);// _CUSSettingPlanDetail.Data.GridSelected;

                var dataAllSet = $.extend(true, [], _CUSSettingPlanDetail.Data.GridAllSettingOrder);

                var SettingOrderFull = res;

                if (res.HasStock || res.HasStockProduct) {
                    var ListStock = res.ListStock;
                    var ListStockWithProduct = res.ListStockWithProduct;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSSettingPlanDetail.URL.Get_StockByCus,
                        data: { CusID: res.CustomerID },
                        success: function (res) {

                            var objStock = {};
                            Common.Data.Each(res.ListStock, function (o) {
                                objStock[o.ID] = o; 
                            })
                            var objStockProduct = {};
                            Common.Data.Each(res.ListDetail, function (o) {
                                objStockProduct[o.StockID + "_" + o.GroupProductID + "_" + o.ProductID] = o;
                            })
                            var objPacking = {};
                            Common.Data.Each(res.ListPacking, function (o) {
                                objPacking[o.PackingID] = o;
                            })

                            Common.Data.Each(dataAllSet, function (o) {
                                if (SettingOrderFull[o.Code] > 0) {
                                    dataOrder[SettingOrderFull[o.Code] - 1].Name = o.Name;
                                    dataOrder[SettingOrderFull[o.Code] - 1].Code = o.Code;
                                    dataOrder[SettingOrderFull[o.Code] - 1].isOrder = 1;
                                }
                            })

                            Common.Data.Each(ListStock, function (o) {
                                if (Common.HasValue(objStock[o.StockID])) {
                                    if (o.Ton > 0) {
                                        dataOrder[o.Ton - 1].Name = objStock[o.StockID].LocationName + "-Tấn";
                                        dataOrder[o.Ton - 1].Code = objStock[o.StockID].Code + "_Ton";
                                        dataOrder[o.Ton - 1].isOrder = 1;
                                    }
                                    if (o.CBM > 0) {
                                        dataOrder[o.CBM - 1].Name = objStock[o.StockID].LocationName + "-Khối";
                                        dataOrder[o.CBM - 1].Code = objStock[o.StockID].Code + "_CBM";
                                        dataOrder[o.CBM - 1].isOrder = 1;
                                    }
                                    if (o.Quantity > 0) {
                                        dataOrder[o.Quantity - 1].Name = objStock[o.StockID].LocationName + "-Số lượng";
                                        dataOrder[o.Quantity - 1].Code = objStock[o.StockID].Code + "_Quantity";
                                        dataOrder[o.Quantity - 1].isOrder = 1;
                                    }
                                }
                            })
                            Common.Data.Each(ListStockWithProduct, function (o) {
                                var  data = objStockProduct[o.StockID + "_" + o.GroupOfProductID + "_" + o.ProductID];
                                if (o.Ton > 0) {
                                    dataOrder[o.Ton - 1].Name = data.StockCode + "_" + data.GroupProductCode + "_" + data.ProductCode + "-Tấn";
                                    dataOrder[o.Ton - 1].Code = data.StockCode + "_" + data.GroupProductCode + "_" + data.ProductCode + "_Ton";
                                    dataOrder[o.Ton - 1].isOrder = 1;
                                }
                                if (o.CBM > 0) {
                                    dataOrder[o.CBM - 1].Name = data.StockCode + "_" + data.GroupProductCode + "_" + data.ProductCode + "-Khối";
                                    dataOrder[o.CBM - 1].Code = data.StockCode + "_" + data.GroupProductCode + "_" + data.ProductCode + "_CBM";
                                    dataOrder[o.CBM - 1].isOrder = 1;
                                }
                                if (o.Quantity > 0) {
                                    dataOrder[o.Quantity - 1].Name = data.StockCode + "_" + data.GroupProductCode + "_" + data.ProductCode + "-Số lượng";
                                    dataOrder[o.Quantity - 1].Code = data.StockCode + "_" + data.GroupProductCode + "_" + data.ProductCode + "_Quantity";
                                    dataOrder[o.Quantity - 1].isOrder = 1;
                                }
                                if (o.PackingCOQuantity > 0) {
                                    data = objPacking[o.PackingID];
                                    dataOrder[o.PackingCOQuantity - 1].Name = data.PackingCode + "-Loại cont";
                                    dataOrder[o.PackingCOQuantity - 1].Code = data.PackingCode + "_Cont";
                                    dataOrder[o.PackingCOQuantity - 1].isOrder = 1;
                                }
                            })

                            $scope.SelectedSettingGrid_Options.dataSource.data(dataOrder);

                            var dataSource = $scope.SelectedSettingGrid.dataSource;
                            var dataAll = [];

                            Common.Data.Each(_CUSSettingPlanDetail.Data.GridAllSetting, function (o) {
                                if (!$scope.SettingItem[o.Code] > 0) {
                                    dataAll.push(o);
                                }
                                else {
                                    var dataItem = dataSource.get($scope.SettingItem[o.Code]);
                                    dataItem.Code = o.Code; dataItem.Name = o.Name; dataItem.isOrder = 0;
                                }
                            })
                            $rootScope.IsLoading = false;
                            $scope.AllSettingGrid_Options.dataSource.data(dataAll);
                        }
                    });

                }
                else {
                    Common.Data.Each(_CUSSettingPlanDetail.Data.GridAllSettingOrder, function (o) {
                        if (res[o.Code] > 0) {
                            dataOrder[res[o.Code] - 1].Name = o.Name;
                            dataOrder[res[o.Code] - 1].Code = o.Code;
                            dataOrder[res[o.Code] - 1].isOrder = 1;
                        }
                    })

                    $scope.SelectedSettingGrid_Options.dataSource.data(dataOrder);

                    var dataSource = $scope.SelectedSettingGrid.dataSource;
                    var dataAll = [];

                    Common.Data.Each(_CUSSettingPlanDetail.Data.GridAllSetting, function (o) {
                        if (!$scope.SettingItem[o.Code] > 0) {
                            dataAll.push(o);
                        }
                        else {
                            var dataItem = dataSource.get($scope.SettingItem[o.Code]);
                            dataItem.Code = o.Code; dataItem.Name = o.Name; dataItem.isOrder = 0;
                        }
                    })
                    $rootScope.IsLoading = false;
                    $scope.AllSettingGrid_Options.dataSource.data(dataAll);
                }

            }
        });

    }
    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSSettingPlanDetail.URL.Get,
        data: { id: _CUSSettingPlanDetail.Param.ID },
        success: function (res) {
            $rootScope.IsLoading = false;
            $scope.SettingItem = res;
            $scope.LoadSetting();
        }
    });

    $scope.settingOrder_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingPlanDetail.URL.SettingOrderList,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: "row",
        columns: [
            { field: 'Name', title: "Tên thiết lập", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.SettingOrderList = function ($event, win) {
        $event.preventDefault();
        $scope.settingOrder_GridOptions.dataSource.read();
        win.center().open();
    }

    $scope.SettingOrder_SelectClick = function ($event, grid, win) {
        $event.preventDefault();

        var itemSelect = grid.dataItem(grid.select());
        if (!Common.HasValue(itemSelect)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Chưa chọn setting',
                Close: null,
                Ok: null
            })
        } else {

            $scope.SettingItem.CUSSettingOrderID = itemSelect.ID;
            $scope.SettingOrder = itemSelect.Name;
            $scope.SettingItem.VehicleNo = 0;
            $scope.SettingItem.MasterETDDate = 0;
            $scope.SettingItem.MasterETDTime = 0;
            $scope.SettingItem.MasterETDDate_Time = 0;
            $scope.SettingItem.VendorCode = 0;
            $scope.SettingItem.MasterNote = 0;
            $scope.LoadSetting();
            win.close();
        }
    }

    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        if (Common.HasValue($scope.SettingItem.Name) && $scope.SettingItem.Name != "") {
            if (Common.HasValue($scope.SettingItem.CUSSettingOrderID) && $scope.SettingItem.CUSSettingOrderID > 0) {
                $rootScope.IsLoading = true;
                var data = $scope.SelectedSettingGrid.dataSource.data();

                var ItemSend = {
                    ID: $scope.SettingItem.ID,
                    Name: $scope.SettingItem.Name,
                    CUSSettingOrderID: $scope.SettingItem.CUSSettingOrderID,
                    FileName: $scope.SettingItem.FileName,
                    FilePath:$scope.SettingItem.FilePath ,
                    FileID: $scope.SettingItem.FileID ,
                }
                var objStock = {};
                Common.Data.Each(data, function (o) {
                    if (o.Code != "" && o.Name != "" && o.isOrder != 1) {
                        ItemSend[o.Code] = o.IndexColumn;
                    }
                })

                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSSettingPlanDetail.URL.Save,
                    data: { item: ItemSend },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $state.go("main.CUSSettingPlan.Index")
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
            else $rootScope.Message({ Msg: 'Chưa chọn thiết lập', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else $rootScope.Message({ Msg: 'Thiếu tên', NotifyType: Common.Message.NotifyType.ERROR });

    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.CUSSettingPlan.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.ChooseFile_Click = function ($event, winfile) {
        $event.preventDefault();
        $rootScope.UploadFile({
            IsImage: false,
            ID: $rootScope.FunctionItem.ID,
            AllowChange: true,
            ShowChoose: true,
            Type: Common.CATTypeOfFileCode.ImportOPS,
            Window: winfile,
            Complete: function (file) {
                $scope.SettingItem.FileName = file.FileName;
                $scope.SettingItem.FilePath = file.FilePath;
                $scope.SettingItem.FileID = file.ID;
            }
        });
    }

    $scope.DowloadFileTemplate_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $rootScope.DownloadFile($scope.SettingItem.FilePath)
        $rootScope.IsLoading = false;
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);