/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODInput_Check = {
    URL: {
        Read: 'PODDIInput_Check_List',
        Update: 'PODDIInput_Check_Save',
        Customer_List: "Customer_List",
        Vendor_List: "Vendor_List",
        Reset: 'PODDIInput_Check_Reset',
        UpdateHasUpload: 'PODDIInput_UpdateHasUpload',
    },
    Data: {
        DIPODStatus: [],
        ListProduct: [],
    }
}

//#endregion

angular.module('myapp').controller('PODInput_CheckCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODInput_CheckCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.FullName = Common.Auth.Item.LastName + " " + Common.Auth.Item.FirstName;

    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID: [],
        IsReturn: true
    }

    // Phần Customer_List
    $scope.mts_CustomerOptions = {
        dataSource: Common.DataSource.Local({
            url: Common.Services.url.POD,
            method: _PODInput_Check.URL.Customer_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        valuePrimitive: true, dataTextField: "CustomerName", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.POD,
        method: _PODInput_Check.URL.Customer_List,
        data: {},
        success: function (res) {
            $scope.mts_CustomerOptions.dataSource.data(res.Data)
        }
    });

    // Phần vendor_List
    $scope.mts_VendorOptions = {
        dataSource: Common.DataSource.Local({
            url: Common.Services.url.POD,
            method: _PODInput_Check.URL.Vendor_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "CustomerName", dataValueField: "ID", placeholder: "Chọn vendor...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.POD,
        method: _PODInput_Check.URL.Vendor_List,
        data: {},
        success: function (res) {
            $scope.mts_VendorOptions.dataSource.data(res.Data)
        }
    });

    $scope.PODCheck_SearchClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.ItemSearch.DateFrom) || !Common.HasValue($scope.ItemSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ItemSearch.DateFrom > $scope.ItemSearch.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $scope.PODCheck_gridOptions.dataSource.read();
        }
    }

    $scope.PODCheck_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_Check.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.ItemSearch.DateFrom,
                    'dtTo': $scope.ItemSearch.DateTo,
                    'listCustomerID': $scope.ItemSearch.ListCustomerID,
                    'hasIsReturn': $scope.ItemSearch.IsReturn
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsOrigin: { type: 'boolean', },
                    IsInvoice: { type: 'boolean', },
                    IsComplete: { type: 'number', },
                    IsClosed: { type: 'boolean', },
                    DITOGroupProductStatusPODID: { type: 'number', },
                    DateFromCome: { type: 'date' },
                    DateFromLeave: { type: 'date' },
                    ReceivedDate: { type: 'date' },
                    DateFromLoadEnd: { type: 'date' },
                    DateFromLoadStart: { type: 'date' },
                    DateToCome: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    DateToLoadEnd: { type: 'date' },
                    DateToLoadStart: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ETD: { type: 'date' },
                    InvoiceDate: { type: 'date' },
                    DatePODContract: { type: 'date' },
                    Ton: { type: 'number', },
                    TonBBGN: { type: 'number', },
                    TonTranfer: { type: 'number', },
                    CBM: { type: 'number', },
                    CBMBBGN: { type: 'number', },
                    CBMTranfer: { type: 'number', },
                    CustomerCode: { type: 'string' },
                    CustomerName: { type: 'string' },
                    Quantity: { type: 'number', },
                    QuantityBBGN: { type: 'number', },
                    QuantityTranfer: { type: 'number', },
                    KPIOPSDate: { type: 'date' },
                    KPIPODDate: { type: 'date' },
                    DateDB: { type: 'date' },
                    MasterID: { type: 'number', },
                    IsChoose: { type: "boolean" }
                }
            },
            pageSize: 20,
            sort: [{ field: 'MasterID', dir: "asc" }]
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [

            {
                title: ' ', width: '40px', field: 'F_Choose', sortorder: 0, configurable: false, isfunctionalHidden: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,PODCheck_grid,PODCheck_gridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PODCheck_grid,PODCheck_gridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: 80, field: 'F_Command1', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="PODInput_SaveClick($event,PODCheck_grid,dataItem)" ng-show="!dataItem.IsClosed" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>' +
                    '<a href="/" ng-click="PODInput_UpLoadClick($event,winfile,dataItem)" ng-show="!dataItem.IsClosed"  class="k-button" data-title="Chứng từ"><i ng-class="{hasupload:dataItem.HasUpload}"  class="fa fa-file"></i></a>' +
                    '<a href="/" ng-click="PODInput_ResetClick($event,PODCheck_grid,dataItem)" ng-show="dataItem.IsClosed&&Auth.ActApproved" class="k-button" data-title="Lưu"><i class="fa fa-backward"></i></a>',
                filterable: false, sortable: false,
            },
            {
                field: 'IsInvoice', width: 80, title: '', attributes: { style: "text-align: center; " }, sortable: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Invoice($event,PODCheck_grid)" /> Đã nhận',
                template: "<input class='chkInvoice' type='checkbox' ng-model='dataItem.IsInvoice' ng-disabled='dataItem.IsClosed' ng-click='ChangeStatusInvoice($event,dataItem)' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đã nhận', Value: true }, { Text: 'Chưa nhận', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }, sortorder: 1, configurable: true, isfunctionalHidden: false,
            },

            {
                field: 'InvoiceBy', width: 100, sortorder: 2, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceBy}}', template: '<input type="text" class="k-textbox "  ng-model="dataItem.InvoiceBy"  ng-keydown="QuickSave($event,PODCheck_grid,dataItem)" style="width: 100%" ng-disabled="dataItem.IsClosed"  ></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', width: 100, sortorder: 3, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceDate}}',
                template: '<input class="cus-datepicker" kendo-date-picker focus-k-datepicker k-options="DateDMY" k-ng-model="dataItem.InvoiceDate" ng-disabled="dataItem.IsClosed" />',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'InvoiceNote', width: 100, sortorder: 4, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceNote}}', template: '<input type="text"  class="k-textbox "  ng-model="dataItem.InvoiceNote"  ng-keydown="QuickSave($event,PODCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  ></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateDN', width: 100, sortorder: 4, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateDN}}',
                template: '<input class="cus-datepicker" kendo-date-picker focus-k-datepicker k-options="DateDMY" k-ng-model="dataItem.DateDN" ng-disabled="dataItem.IsClosed" />',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { field: 'OrderCode', width: 100, sortorder: 5, configurable: true, isfunctionalHidden: false, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: 100, sortorder: 6, configurable: true, isfunctionalHidden: false, title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusSOPOD', width: 100, sortorder: 7, configurable: true, isfunctionalHidden: false, title: 'Tình trạng c.từ theo SO', sortable: false, filterable: false, menu: true },
            { field: 'DNCode', width: 100, sortorder: 8, configurable: true, isfunctionalHidden: false, title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Note', width: 100, sortorder: 9, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.Note}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Note"  ng-keydown="QuickSave($event,PODCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  />'
            },
            {
                field: 'Note1', width: 100, sortorder: 10, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Note1"  ng-keydown="QuickSave($event,PODCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  />'
            },
            {
                field: 'Note2', width: 100, sortorder: 11, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Note2"  ng-keydown="QuickSave($event,PODCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  />'
            },
            {
                field: 'Description', width: 100, sortorder: 12, configurable: true, isfunctionalHidden: false, title: 'Ghi chú SO', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Description"  ng-keydown="QuickSave($event,PODCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  />'
            },
            { field: 'StatusOPSMaster', width: 100, sortorder: 13, configurable: true, isfunctionalHidden: false, title: 'Trạng thái chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 100, sortorder: 14, configurable: true, isfunctionalHidden: false, title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 100, sortorder: 15, configurable: true, isfunctionalHidden: false, title: 'Tên NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 100, sortorder: 16, configurable: true, isfunctionalHidden: false, title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToProvince', width: 100, sortorder: 17, configurable: true, isfunctionalHidden: false, title: 'Tỉnh thành', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToDistrict', width: 100, sortorder: 18, configurable: true, isfunctionalHidden: false, title: 'Quận huyện', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EconomicZone', width: 100, sortorder: 19, configurable: true, isfunctionalHidden: false, title: 'RouteID', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: 100, sortorder: 20, configurable: true, isfunctionalHidden: false, title: 'Mã kho', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RegNo', width: 100, sortorder: 21, configurable: true, isfunctionalHidden: false, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', width: 100, sortorder: 22, configurable: true, isfunctionalHidden: false, title: 'Tên tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverTel', width: 100, sortorder: 23, configurable: true, isfunctionalHidden: false, title: 'SĐT tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: 100, sortorder: 24, configurable: true, isfunctionalHidden: false, title: 'Lệnh v.c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDate', width: 100, sortorder: 25, configurable: true, isfunctionalHidden: false, title: 'Ngày gửi', template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHMS(RequestDate)#", filterable: false },
            { field: 'DateFromCome', width: 100, sortorder: 26, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateFromCome}}', template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMY(DateFromCome)#", filterable: false },
            { field: 'DateFromLoadStart', width: 100, sortorder: 27, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateFromLoadStart}}', template: "#=DateFromLoadStart==null?' ':Common.Date.FromJsonHM(DateFromLoadStart)#", filterable: false },
            { field: 'DateFromLoadEnd', width: 100, sortorder: 28, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateFromLoadEnd}}', template: "#=DateFromLoadEnd==null?' ':Common.Date.FromJsonHM(DateFromLoadEnd)#", filterable: false },
            { field: 'DateFromLeave', width: 100, sortorder: 29, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateFromLeave}}', template: "#=DateFromLeave==null?' ':Common.Date.FromJsonDMY(DateFromLeave)#", filterable: false },
            { field: 'DateToCome', width: 100, sortorder: 30, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateToCome}}', template: "#=DateToCome==null?' ':Common.Date.FromJsonDMY(DateToCome)#", filterable: false },
            { field: 'DateToLoadStart', width: 100, sortorder: 31, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateToLoadStart}}', template: "#=DateToLoadStart==null?' ':Common.Date.FromJsonHM(DateToLoadStart)#", filterable: false },
            { field: 'DateToLoadEnd', width: 100, sortorder: 32, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateToLoadEnd}}', template: "#=DateToLoadEnd==null?' ':Common.Date.FromJsonHM(DateToLoadEnd)#", filterable: false },
            { field: 'DateToLeave', width: 100, sortorder: 33, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateToLeave}}', template: "#=DateToLeave==null?' ':Common.Date.FromJsonDMY(DateToLeave)#", filterable: false },
            { field: 'CustomerCode', width: 100, sortorder: 34, configurable: true, isfunctionalHidden: false, title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: 100, sortorder: 35, configurable: true, isfunctionalHidden: false, title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', width: 100, sortorder: 36, configurable: true, isfunctionalHidden: false, title: 'Nhà vận tải', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductCode', width: 100, sortorder: 37, configurable: true, isfunctionalHidden: false, title: 'Mã hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', width: 100, sortorder: 38, configurable: true, isfunctionalHidden: false, title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNABA', width: 100, sortorder: 39, configurable: true, isfunctionalHidden: false, title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ChipNo', width: 100, sortorder: 40, configurable: true, isfunctionalHidden: false, title: 'Số chíp', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Temperature', width: 100, sortorder: 41, configurable: true, isfunctionalHidden: false, title: 'Nhiệt độ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: 100, sortorder: 42, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.Ton}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TonBBGN', width: 100, sortorder: 43, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.TonBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TonTransfer', width: 100, sortorder: 44, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.TonTransfer}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: 100, sortorder: 45, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.CBM}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBMBBGN', width: 100, sortorder: 46, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.CBMBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBMTransfer', width: 100, sortorder: 47, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.CBMTransfer}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: 100, sortorder: 48, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.Quantity}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'QuantityBBGN', width: 100, sortorder: 49, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.QuantityBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'QuantityTransfer', width: 100, sortorder: 50, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.QuantityTransfer}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOrder', width: 100, sortorder: 51, configurable: true, isfunctionalHidden: false, title: 'Tình trạng đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'KPIOPSDate', width: 100, sortorder: 52, configurable: true, isfunctionalHidden: false, title: 'Ngày v.c hợp đồng', template: "#=KPIOPSDate==null?' ':Common.Date.FromJsonDMY(KPIOPSDate)#", sortable: false, filterable: false, menu: true },
            {
                field: 'IsKPIOPS', width: 100, sortorder: 53, configurable: true, isfunctionalHidden: false, title: 'Đạt KPI v.c', sortable: false, filterable: false, menu: true,
                template: '<input ng-show="dataItem.IsKPIOPS != null" disabled type="checkbox" #= IsKPIOPS ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            { field: 'KPIPODDate', width: 100, sortorder: 54, configurable: true, isfunctionalHidden: false, title: 'Ngày c.t hợp đồng', template: "#=KPIPODDate==null?' ':Common.Date.FromJsonDMY(KPIPODDate)#", sortable: false, filterable: false, menu: true },
            {
                field: 'IsKPIPOD', width: 100, sortorder: 55, configurable: true, isfunctionalHidden: false, title: 'Đạt KPI c.t', sortable: false, filterable: false, menu: true,
                template: '<input ng-show="dataItem.IsKPIPOD != null" disabled type="checkbox" #= IsKPIPOD ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            { field: 'TonReturn', width: 100, sortorder: 56, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.TonReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } }, },
            { field: 'CBMReturn', width: 100, sortorder: 57, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.CBMReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'QuantityReturn', width: 100, sortorder: 58, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.QuantityReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TypeOfDITOGroupProductReturnName', width: 100, sortorder: 59, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSTypeOfDITOGroupProductReturn.TypeName}}', filterable: false, },
            { field: 'HasReturn', width: 100, sortorder: 60, configurable: true, isfunctionalHidden: false, title: 'Có trả về', sortable: false, filterable: false, menu: true, template: '<input ng-show="dataItem.HasReturn != null" disabled type="checkbox" #= HasReturn ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' }, },
            { field: 'TotalReturn', width: 100, sortorder: 61, configurable: true, isfunctionalHidden: false, title: 'S.l khay trả về', filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'InvoiceReturnBy', width: 100, sortorder: 62, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.InvoiceReturnBy}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'InvoiceReturnDate', width: 100, sortorder: 63, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'InvoiceReturnNote', width: 100, sortorder: 64, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.InvoiceReturnNote}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', sortable: false, filterable: false, menu: false, sortorder: 999, configurable: false, isfunctionalHidden: false, }
        ],
        dataBound: function (e) {

        }
    }

    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.PODCheck_grid);
    }, 100);

    $scope.ChangeStatusInvoice = function ($event, data) {
        var tr = $($event.target).closest('tr');
        var lsttd = tr.find('input[type=text]');
        if (data.IsInvoice) {
            if (!Common.HasValue(data.InvoiceBy) || data.InvoiceBy == '')
                data.InvoiceBy = $scope.FullName;
            if (!Common.HasValue(data.InvoiceDate) || data.InvoiceDate == '')
                data.InvoiceDate = new Date();
        }
        else {
            data.InvoiceBy = null;
            data.InvoiceDate = null;
        }
        if (lsttd.length > 0) {
            lsttd[0].focus();
        }
    };

    $scope.gridChooseAll_Invoice = function ($event, grid) {
        if ($event.target.checked == true) {
            grid.items().each(function () {

                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsInvoice != true) {
                    $(tr).find('td input.chkInvoice').prop('checked', true);
                    item.IsInvoice = true;

                    if (!Common.HasValue(item.InvoiceBy) || item.InvoiceBy == '')
                        item.InvoiceBy = $scope.FullName;
                    if (!Common.HasValue(item.InvoiceDate) || item.InvoiceDate == '')
                        item.InvoiceDate = new Date();
                }
            });
        }
        else {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsInvoice == true) {
                    $(tr).find('td input.chkInvoice').prop('checked', false);
                    item.IsInvoice = false;

                    item.InvoiceBy = null;
                    item.InvoiceDate = null;
                }
            });
        }

    }

    $scope.PODInput_SaveClick = function ($event, grid, data) {
        $event.preventDefault();
        if (data.IsInvoice) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Xác nhận chứng từ này đã nhận',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.POD,
                        method: _PODInput_Check.URL.Update,
                        data: { 'item': data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            })

        }
        else {
            $rootScope.Message({ Msg: 'Chưa tick nhận chứng từ', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        }
    }
    $scope.QuickSave = function ($event, grid, data) {
        if ($event.which === 13) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Xác nhận lưu thông tin chứng từ ?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.POD,
                        method: _PODInput_Check.URL.Update,
                        data: { 'item': data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            })
        }
    }
    $scope.PODInput_ResetClick = function ($event, grid, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn mở dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODInput_Check.URL.Reset,
                    data: { DITOGroupID: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.PODCheck_grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã mở dữ liệu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.PODInput_UpLoadClick = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.UploadFile({
            IsImage: false,
            ID: data.ID,
            AllowChange: true,
            ShowChoose: false,
            Type: Common.CATTypeOfFileCode.DIPOD,
            Window: win,
            UploadComplete: function (res) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODInput_Check.URL.UpdateHasUpload,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.PODCheck_gridOptions.dataSource.read()
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            },
            DeleteComplete: function (lst) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODInput_Check.URL.UpdateHasUpload,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.PODCheck_gridOptions.dataSource.read()
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    }

    $scope.PODInput_SaveAllClick = function ($event) {
        $event.preventDefault();
        var lst = [];
        angular.forEach($scope.PODCheck_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                lst.push(o);
        })
        if (lst.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận lưu tất cả?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.POD,
                        method: "PODDIInput_Check_SaveList",
                        data: { lst: lst },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.PODCheck_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
    }

    //actions
    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.PODInput.Index")
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

}]);