/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONInput_InputProduction = {
    URL: {
        Read: 'MONInput_InputProduction_List',
        Update: 'MONInput_InputProduction_Save',
        Complete: 'MONInput_InputProduction_ChangeComplete',
        DNGet: "MONInput_InputProduction_SplitDNGet",
        DNSave: "MONInput_InputProduction_SplitDNSave",
        AddReturnGet: 'MONInput_InputProduction_AddReturnGet',
        AddReturnSave: 'MONInput_InputProduction_AddReturnSave',
        AddReturnEditGet: 'MONInput_InputProduction_AddReturn_EditGet',
        AddReturnEditDelte: 'MONInput_InputProduction_AddReturnEditDelete',
        VendorList: 'MONInput_InputProduction_Vendor_List'
    },
    Data: {
        DIPODStatus: [],
        ProductReturn: null,
    }
}

//#endregion

angular.module('myapp').controller('MONMonitor_Input_InputProductionCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('MONMonitor_Input_InputProductionCtrl');
    $rootScope.IsLoading = false;


    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID: [],
        IsReturn: true,
    }

    $scope.Item = null;
    $scope.ItemReturn = null;
    $scope.DITOGroupProductID = 0;

    $scope.mts_CustomerOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            },
            filter: [{ field: 'ID', operator: 'gt', value: 0 }]
        }),
        valuePrimitive: true, dataTextField: "Text", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<strong>[#= Code #]</strong><span> #= CustomerName #</span>',
        tagTemplate:'#=Code#',
        //headerTemplate: '<strong>[ Mã khách hàng ] Tên khách hàng </strong>',
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "Customer_List",
        data: {},
        success: function (res) {

            angular.forEach(res.Data, function (o, i) {
                if (o.ID > 0)
                    o.Text = "[" + o.Code + "] " + o.CustomerName;
                else
                    o.Text = " ";
            })
            $scope.mts_CustomerOptions.dataSource.data(res.Data)
        },
    });

    $scope.MONInput_SearchClick = function ($event) {
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
            $scope.MONInput_Production_gridOptions.dataSource.read();
        }
    }


    $scope.DITOGroupProductStatusPOD_datasource = [];

    $scope.cboDITOGroupProductStatusPOD_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfDIPODStatus,
        success: function (data) {
            $scope.cboDITOGroupProductStatusPOD_Options.dataSource.data(data)
            _MONInput_InputProduction.Data.DIPODStatus = data;
        }
    })

    $scope.cboVendor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONInput_InputProduction.URL.VendorList,
        data: {},
        success: function (res) {
            $scope.cboVendor_Options.dataSource.data(res.Data);
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    var lastid = 0;
    $scope.MONInput_Production_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONInput_InputProduction.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.ItemSearch.DateFrom,
                    'dtTo': $scope.ItemSearch.DateTo,
                    'listCustomerID': $scope.ItemSearch.ListCustomerID,
                    'hasIsReturn': $scope.ItemSearch.IsReturn
                };
            },
            sort: [{ field: "RequestDate", dir: "desc" }, { field: "OrderCode", dir: "desc" }],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsOrigin: { type: 'boolean', },
                    IsInvoice: { type: 'boolean', },
                    IsComplete: { type: 'number', },
                    DITOGroupProductStatusPODID: { type: 'number' },
                    VendorLoadID: { type: 'number' },
                    VendorUnLoadID: { type: 'number' },
                    InvoiceDate: { type: 'date' },
                    TonBBGN: { type: 'number', },
                    TonTranfer: { type: 'number', },
                    CBMBBGN: { type: 'number', },
                    CBMTranfer: { type: 'number', },
                    QuantityBBGN: { type: 'number', },
                    QuantityTranfer: { type: 'number', },
                    InvoiceReturnDate: { type: 'date' },
                    InvoiceReturnDateString: { type: 'string' },
                    InvoiceDateString: { type: 'string' },
                    InvoiceReturnDate: { type: 'date' },
                    IsChoose: { type: 'boolean', },
                    RequestDate: { type: 'date' },

                    OrderCode: { editable: false },
                    RegNo: { editable: false },
                    GroupOfProductCode: { editable: false },
                    ProductCode: { editable: false },
                    PartnerCode: { editable: false },
                    PartnerName: { editable: false },
                    StatusSOPOD: { editable: false },
                    Description: { editable: false },
                    StatusOPSMaster: { editable: false },
                    LocationToCode: { editable: false },
                    LocationToName: { editable: false },
                    LocationToAddress: { editable: false },
                    LocationToProvince: { editable: false },
                    LocationToDistrict: { editable: false },
                    EconomicZone: { editable: false },
                    LocationFromCode: { editable: false },
                    DriverName: { editable: false },
                    DriverTel: { editable: false },
                    MasterCode: { editable: false },
                    Ton: { editable: false },
                    CBM: { editable: false },
                    Quantity: { editable: false },
                    CustomerCode: { editable: false },
                    CustomerName: { editable: false },
                    VendorName: { editable: false },
                    GroupOfProductName: { editable: false },
                    DNABA: { editable: false },
                    StatusOrder: { editable: false },
                    KPIOPSDate: { editable: false },
                    HasReturn: { editable: false },
                    TotalReturn: { editable: false },
                    InvoiceReturnBy: { editable: false },
                    ModifiedBy: { editable: false },
                    F_Check: { editable: false },
                    F_Command2: { editable: false },

                    OrderDateConfig: { type: 'date', editable: false },
                    OrderGroupProductDateConfig: { type: 'date', editable: false },
                    OPSDateConfig: { type: 'date', editable: false },
                    OPSGroupProductDateConfig: { type: 'date', editable: false },
                }
            },
            pageSize: 20
        }),
        selectable: true, reorderable: false, editable: 'inline',
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        change: function (e) {
            var grid = this;
            var dataItem = this.dataItem(grid.select()[0]);
            if (lastid == 0) {
                if (!dataItem.IsInvoice) {
                    grid.editRow($(grid.select()[0]));
                    lastid = dataItem.ID;
                }
            }
            if (lastid != dataItem.ID) {
                lastid = 0;
                grid.saveRow();
            }
        },
        save: function (e) {
            //e.preventDefault();
            if (e.model.dirty)
                $scope.LineSave(e.model);

        },
        columns: [
            {
                title: ' ', width: 35, field: "F_Check",
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,MONInput_Production_grid,MONInput_Production_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,MONInput_Production_grid,MONInput_Production_gridChoose_Change)" />',
                filterable: false, sortable: false, sortorder: 0, configurable: true, isfunctionalHidden: true,
            },
            { field: 'OrderCode', width: 110, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 1, configurable: true, isfunctionalHidden: false, },
            { field: 'RegNo', width: 110, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 2, configurable: true, isfunctionalHidden: false, },
            { field: 'GroupOfProductCode', width: 110, title: 'Mã nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: true, isfunctionalHidden: false, },
            { field: 'ProductCode', width: 110, title: 'Mã hàng', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 4, configurable: true, isfunctionalHidden: false, },
            {
                field: 'RequestDate', width: 120, title: 'Ngày gửi y/c', sortorder: 6, configurable: true, isfunctionalHidden: true,
                template: "#=RequestDate==null?\"\":Common.Date.FromJsonDMY(RequestDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'PartnerName', width: 100, title: 'Tên NPP', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: false, },
            { field: 'PartnerCode', width: 100, title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: false, },

            {
                field: 'VendorLoadID', width: 100, title: 'Vendor bốc hàng', filterable: false, sortorder: 7, configurable: true, isfunctionalHidden: false,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input class="cus-combobox" focus-k-combobox kendo-combo-box k-options="cboVendor_Options" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=VendorLoadCode#",
            },
            {
                field: 'VendorUnLoadID', width: 100, title: 'Vendor dỡ hàng', filterable: false, sortorder: 8, configurable: true, isfunctionalHidden: false,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input class="cus-combobox" focus-k-combobox kendo-combo-box k-options="cboVendor_Options" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=VendorUnLoadCode#",
            },
            {
                field: 'InvoiceDateString', width: 100, sortorder: 10, configurable: true, isfunctionalHidden: false,
                title: 'Ngày nhận c.từ',
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  type="text" class="k-textbox txtInvoiceDate"/>');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=InvoiceDateString == null ? '' : InvoiceDateString #",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'Ton', width: 100, sortorder: 11, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.Ton}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: 100, sortorder: 12, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.CBM}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: 100, sortorder: 13, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.Quantity}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'QuantityTranfer', width: 100, title: 'SL v/c', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 14, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="Quantity_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber1(QuantityTranfer) #",
            },
            {
                field: 'TonTranfer', width: 100, title: 'Tấn v/c', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 16, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="Ton_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber9(TonTranfer) #",
            },
            {
                field: 'CBMTranfer', width: 100, title: 'Khối v/c', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 17, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="CBM_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber9(CBMTranfer) #",
            },
            {
                field: 'QuantityBBGN', width: 100, title: 'Số lượng BBGN', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 18, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="Quantity_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber1(QuantityBBGN) #",
            },
            {
                field: 'TonBBGN', width: 100, title: 'Tấn BBGN', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 19, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="Ton_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber9(TonBBGN) #",
            },
            {
                field: 'CBMBBGN', width: 100, title: 'Khối BBGN', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 20, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="CBM_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber9(CBMBBGN) #",
            },
            {
                field: 'QuantityReturn', width: 100, title: 'SL trả về', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 21, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="Quantity_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber6(QuantityReturn) #",

            },
            {
                field: 'TonReturn', width: 100, title: 'Tấn trả về', filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    var input = $('<input  kendo-numeric-text-box k-options="Ton_Options" style="width: 100%" />');
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber6(TonReturn) #",
                sortorder: 22, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'CBMReturn', width: 100, title: 'Khối trả về', filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $('<input  kendo-numeric-text-box k-options="CBM_Options" style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Common.Number.ToNumber6(CBMReturn) #",
                sortorder: 23, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'IsInvoice', width: 50, title: 'Đã nhận', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsInvoice' ng-disabled='true' ng-click='ChangeStatusInvoice($event,dataItem)' />",
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
                },
                sortorder: 1, configurable: true, isfunctionalHidden: false,
            },
            {
                field: 'InvoiceBy', width: 100, sortorder: 2, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceBy}}',
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', width: 100, sortorder: 3, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceDate}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-picker focus-k-datepicker k-options="DateDMY"  />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=InvoiceDate==null?' ':Common.Date.FromJsonDMY(InvoiceDate)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'InvoiceNote', width: 100, sortorder: 4, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceNote}}',
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceReturnNote', width: 100, title: 'Số hóa đơn trả về',
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=InvoiceReturnNote == null ? '' : InvoiceReturnNote #",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 24, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'InvoiceReturnDateString', width: 100, sortorder: 25, configurable: true, isfunctionalHidden: true,
                title: 'Ngày nhận c.từ trả về',
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=InvoiceReturnDateString == null ? '' : InvoiceReturnDateString #",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfDITOGroupProductReturnID', width: 100, title: 'Lý do trả về', filterable: false, sortorder: 26, configurable: true, isfunctionalHidden: true,
                editor: function (container, options) {
                    var input = $('<input class="cus-combobox" kendo-combo-box k-options="TypeOfReturn_Options"   style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=TypeOfDITOGroupProductReturnName == null ? '' : TypeOfDITOGroupProductReturnName #",

            },
            {
                field: 'DNCode', width: 100, sortorder: 28, configurable: true, isfunctionalHidden: false, title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } },
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DNCode == null ? '' : DNCode #",
            },
            {
                field: 'SOCode', width: 100, sortorder: 28, configurable: true, isfunctionalHidden: false, title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } },
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=SOCode == null ? '' : SOCode #",
            },
            {
                field: 'Note', width: 100, sortorder: 29, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.Note}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Note == null ? '' : Note #",
            },
            {
                field: 'Note1', width: 100, sortorder: 30, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } },
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Note1 == null ? '' : Note1 #",
            },
            {
                field: 'Note2', width: 100, sortorder: 31, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } },

                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Note2 == null ? '' : Note2 #",
            },
            {
                field: 'ChipNo', width: 100, sortorder: 32, configurable: true, isfunctionalHidden: false, title: 'Số chíp', filterable: { cell: { operator: 'contains', showOperators: false } },

                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=ChipNo == null ? '' : ChipNo #",
            },
            {
                field: 'Temperature', width: 100, sortorder: 33, configurable: true, isfunctionalHidden: false, title: 'Nhiệt độ', filterable: { cell: { operator: 'contains', showOperators: false } },

                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=Temperature == null ? '' : Temperature #",
            },
            { field: 'StatusSOPOD', width: 100, sortorder: 34, configurable: true, isfunctionalHidden: false, title: 'Tình trạng c.từ theo SO', sortable: false, filterable: false, menu: true },
            { field: 'Description', width: 100, sortorder: 35, configurable: true, isfunctionalHidden: false, title: 'Ghi chú SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOPSMaster', width: 100, sortorder: 36, configurable: true, isfunctionalHidden: false, title: 'Trạng thái chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 100, sortorder: 37, configurable: true, isfunctionalHidden: false, title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 100, sortorder: 38, configurable: true, isfunctionalHidden: false, title: 'Tên NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 100, sortorder: 39, configurable: true, isfunctionalHidden: false, title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToProvince', width: 100, sortorder: 40, configurable: true, isfunctionalHidden: false, title: 'Tỉnh thành', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToDistrict', width: 100, sortorder: 41, configurable: true, isfunctionalHidden: false, title: 'Quận huyện', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EconomicZone', width: 100, sortorder: 42, configurable: true, isfunctionalHidden: false, title: 'RouteID', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: 100, sortorder: 43, configurable: true, isfunctionalHidden: false, title: 'Mã kho', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', width: 100, sortorder: 44, configurable: true, isfunctionalHidden: false, title: 'Tên tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverTel', width: 100, sortorder: 45, configurable: true, isfunctionalHidden: false, title: 'SĐT tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: 100, sortorder: 46, configurable: true, isfunctionalHidden: false, title: 'Lệnh v.c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'DateFromCome', width: 140, sortorder: 47, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateFromCome}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMY(DateFromCome)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateFromLoadStart', width: 140, sortorder: 48, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateFromLoadStart}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateFromLoadStart==null?' ':Common.Date.FromJsonDMY(DateFromLoadStart)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateFromLoadEnd', width: 140, sortorder: 49, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateFromLoadEnd}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateFromLoadEnd==null?' ':Common.Date.FromJsonDMY(DateFromLoadEnd)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateFromLeave', width: 140, sortorder: 50, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateFromLeave}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateFromLeave==null?' ':Common.Date.FromJsonDMY(DateFromLeave)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateToCome', width: 140, sortorder: 51, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateToCome}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateToCome==null?' ':Common.Date.FromJsonDMY(DateToCome)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateToLoadStart', width: 140, sortorder: 52, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateToLoadStart}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateToLoadStart==null?' ':Common.Date.FromJsonDMY(DateToLoadStart)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateToLoadEnd', width: 140, sortorder: 53, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateToLoadEnd}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM"  />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateToLoadEnd==null?' ':Common.Date.FromJsonDMY(DateToLoadEnd)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateToLeave', width: 140, sortorder: 54, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.DateToLeave}}',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=DateToLeave==null?' ':Common.Date.FromJsonDMY(DateToLeave)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { field: 'CustomerCode', width: 100, sortorder: 55, configurable: true, isfunctionalHidden: false, title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: 100, sortorder: 56, configurable: true, isfunctionalHidden: false, title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', width: 100, sortorder: 57, configurable: true, isfunctionalHidden: false, title: 'Nhà vận tải', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', width: 100, sortorder: 58, configurable: true, isfunctionalHidden: false, title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNABA', width: 100, sortorder: 59, configurable: true, isfunctionalHidden: false, title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOrder', width: 100, sortorder: 60, configurable: true, isfunctionalHidden: false, title: 'Tình trạng đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'KPIOPSDate', width: 100, sortorder: 61, configurable: true, isfunctionalHidden: false, title: 'Ngày v.c hợp đồng', template: "#=KPIOPSDate==null?' ':Common.Date.FromJsonDMY(KPIOPSDate)#", sortable: false, filterable: false, menu: true },
            { field: 'HasReturn', width: 100, sortorder: 62, configurable: true, isfunctionalHidden: false, title: 'Có trả về', sortable: false, filterable: false, menu: true, template: '<input ng-show="dataItem.HasReturn != null" disabled type="checkbox" #= HasReturn ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' }, },
            { field: 'TotalReturn', width: 100, sortorder: 63, configurable: true, isfunctionalHidden: false, title: 'S.l khay trả về', filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'InvoiceReturnBy', width: 100, sortorder: 64, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.InvoiceReturnBy}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ModifiedBy', width: 100, sortorder: 65, configurable: true, isfunctionalHidden: false, title: 'Người cập nhật cuối cùng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                title: ' ', width: 100, field: 'F_Command2', sortorder: 80, configurable: true, isfunctionalHidden: true,
                template: function (dataItem) {
                    if (dataItem.IsInvoice == 1)
                        return ' ';
                    else
                        return '<a href="/" ng-click="MONInput_Production_SaveClick($event,MONInput_Production_grid,dataItem)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>' +
                 '<a href="/" ng-click="MONInput_Production_UpLoadClick($event,winfile,dataItem)" class="k-button" data-title="Chứng từ"><i class="fa fa-file"></i></a>'
                },
                filterable: false, sortable: false, locked: false,
            },
            {
                field: 'ExIsOverNight', width: 100, sortorder: 70, configurable: true, isfunctionalHidden: false, title: 'Qua đêm', filterable: false,
                editor: function (container, options) {
                    var input = $('<input  type="checkbox"/>');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: '<input ng-show="dataItem.ExIsOverNight != null" disabled type="checkbox" #= ExIsOverNight ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            {
                field: 'ExIsOverWeight', width: 100, sortorder: 71, configurable: true, isfunctionalHidden: false, title: 'Quá tải', filterable: false,
                editor: function (container, options) {
                    var input = $('<input  type="checkbox"/>');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: '<input ng-show="dataItem.ExIsOverWeight != null" disabled type="checkbox" #= ExIsOverWeight ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            {
                field: 'ExTotalDayOut', width: 100, sortorder: 72, configurable: true, isfunctionalHidden: false, title: "{{RS.OPSDITOMaster.ExTotalDayOut}}", filterable: { cell: { operator: 'contains', showOperators: false } },
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=ExTotalDayOut == null ? '' : ExTotalDayOut #",
            },
            {
                field: 'ExTotalJoin', width: 100, sortorder: 73, configurable: true, isfunctionalHidden: false, title: "{{RS.OPSDITOMaster.ExTotalJoin}}", filterable: { cell: { operator: 'contains', showOperators: false } },
                editor: function (container, options) {
                    var input = $('<input  type="text" class="k-textbox " style="width: 100%" />');
                    var data = $scope.MONInput_Production_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
                template: "#=ExTotalJoin == null ? '' : ExTotalJoin #",
            },
            { field: 'OPSDateConfig', width: 100, sortorder: 85, configurable: true, isfunctionalHidden: false, title: '{{RS.MONMonitor_Input_InputProduction.OPSDateConfig}}', template: "#=OPSDateConfig==null?' ':Common.Date.FromJsonDMY(OPSDateConfig)#", sortable: false, filterable: { cell: { operator: 'gte', showOperators: false } }, menu: true },
            { field: 'OPSGroupProductDateConfig', width: 100, sortorder: 86, configurable: true, isfunctionalHidden: false, title: '{{RS.MONMonitor_Input_InputProduction.OPSGroupProductDateConfig}}', template: "#=OPSGroupProductDateConfig==null?' ':Common.Date.FromJsonDMY(OPSGroupProductDateConfig)#", sortable: false, filterable: { cell: { operator: 'gte', showOperators: false } }, menu: true },
            { field: 'OrderDateConfig', width: 100, sortorder: 87, configurable: true, isfunctionalHidden: false, title: '{{RS.MONMonitor_Input_InputProduction.OrderDateConfig}}', template: "#=OrderDateConfig==null?' ':Common.Date.FromJsonDMY(OrderDateConfig)#", sortable: false, filterable: { cell: { operator: 'gte', showOperators: false } }, menu: true },
            { field: 'OrderGroupProductDateConfig', width: 100, sortorder: 88, configurable: true, isfunctionalHidden: false, title: '{{RS.MONMonitor_Input_InputProduction.OrderGroupProductDateConfig}}', template: "#=OrderGroupProductDateConfig==null?' ':Common.Date.FromJsonDMY(OrderGroupProductDateConfig)#", sortable: false, filterable: { cell: { operator: 'gte', showOperators: false } }, menu: true },

            { title: ' ', sortable: false, filterable: false, menu: false, sortorder: 100, configurable: false, isfunctionalHidden: true, }
        ],
        dataBound: function (e) {
            var grid = this;
            if (Common.HasValue(grid.tbody) && Common.HasValue(grid)) {
                var data = grid.dataSource.data();
                Common.Data.Each(data, function (o) {
                    if (o.IsComplete == 1)
                        $('tr[data-uid="' + o.uid + '"] ').css({ "background-color": "#73c95f", "color": "#ffffff" });
                })
                var lst = grid.tbody.find('tr');
                if (lst.length > 0) {
                    lstTd = $(lst[0]).find('.txtInvoiceNote');
                    if (lstTd.length > 0)
                        $timeout(function () {
                            $(lstTd[0]).focus();
                        }, 100)

                }
            }

            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var data = grid.dataItem(tr);
                if (!data.IsInvoice) {
                    $(tr).keyup(function (e) {
                        if (e.which === 13) {
                            e.target.blur()
                            lastid = 0;
                            $scope.MONInput_Production_grid.saveRow();
                        }
                    })
                }
            })
        }
    }

    $scope.MONInput_Production_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MainHasChoose = hasChoose;
    }

    $scope.MONInput_CompleteClick = function ($event, grid) {
        $event.preventDefault();
        var dataSend = [];
        var error = false;
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                dataSend.push(o.DITOGroupID);
            }
        })

        if (dataSend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONInput_InputProduction.URL.Complete,
                data: { lst: dataSend, isComplete: true },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    grid.dataSource.read();
                    $scope.MainHasChoose = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }

    };
    $scope.MONInput_UnCompleteClick = function ($event, grid) {
        $event.preventDefault();
        var dataSend = [];
        var error = false;
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                dataSend.push(o.DITOGroupID);
            }
        })

        if (dataSend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONInput_InputProduction.URL.Complete,
                data: { lst: dataSend, isComplete: false },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    grid.dataSource.read();
                    $scope.MainHasChoose = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }

    };
    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.MONInput_Production_grid);
    }, 100);

    $scope.ChangeInvoiceDate = function ($event, data) {
        if ($event.which === 38) {
            //up arrow
            data.InvoiceDate = Common.Date.AddDay(data.InvoiceDate, 1);
            data.InvoiceDateString = Common.Date.ToString(data.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 40) {
            //down arrow
            data.InvoiceDate = Common.Date.AddDay(data.InvoiceDate, -1);
            data.InvoiceDateString = Common.Date.ToString(data.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 13) {
            $scope.QuickSave($event, data);
        }
    }

    $scope.ChangeInvoiceReturnDate = function ($event, data) {
        if ($event.which === 38) {
            //up arrow
            data.InvoiceReturnDate = Common.Date.AddDay(data.InvoiceReturnDate, 1);
            data.InvoiceReturnDateString = Common.Date.ToString(data.InvoiceReturnDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 40) {
            //down arrow
            data.InvoiceReturnDate = Common.Date.AddDay(data.InvoiceReturnDate, -1);
            data.InvoiceReturnDateString = Common.Date.ToString(data.InvoiceReturnDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 13) {
            $scope.QuickSave($event, data);
        }
    }


    $scope.Ton_Options = { format: 'n9', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 9 }
    $scope.CBM_Options = { format: 'n9', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 9 }
    $scope.Quantity_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0 }

    $scope.TypeOfReturn_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                TypeName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.OPSTypeOfDITOGroupProductReturn,
        success: function (data) {

            var xdata = [];
            xdata.push({ TypeName: ' ', ID: -1 });
            Common.Data.Each(data, function (o) {
                xdata.push(o)
            })
            $scope.TypeOfReturn_Options.dataSource.data(xdata)
        }
    })

    $scope.MONInput_Production_SaveClick = function ($event, grid, data) {
        $event.preventDefault();
        var error = false;
        var ispod = false;
        if (Common.HasValue(data.InvoiceNote) && data.InvoiceNote != '' && Common.HasValue(data.InvoiceDate)) {
            var str = data.InvoiceDateString.split("/");
            if (str.length == 2) {
                var day = parseInt(str[0]);
                var month = parseInt(str[1]);
                ispod = true;
                if (day > 0 && day <= 31 && month > 0 && month < 13) {
                    data.InvoiceDate = new Date(new Date().getFullYear, month - 1, day);
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                    error = true;
                }
            }
            else {
                $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                error = true;
            }
        }

        if (!error) {
            if (ispod) {
                $scope.CTConfirm({
                    Lable: "Bạn đã nhận chứng từ này ?",
                    TextOK: "Đã nhận",
                    TextDeny: "Chưa nhận",
                    OK: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: _MONInput_InputProduction.URL.Update,
                            data: { 'item': data, ispod: true },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.MONInput_Production_gridOptions.dataSource.read();
                            }
                        })
                    },
                    Deny: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: _MONInput_InputProduction.URL.Update,
                            data: { 'item': data, ispod: false },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.MONInput_Production_gridOptions.dataSource.read();
                            }
                        })
                    }
                });
            }
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONInput_InputProduction.URL.Update,
                    data: { 'item': data, ispod: false },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.MONInput_Production_gridOptions.dataSource.read();
                    }
                });
            }
        }
    }

    $scope.MONInput_Production_UpLoadClick = function ($event, win, data) {
        $event.preventDefault();
        //$rootScope.UploadFile({
        //    IsImage: false,
        //    ID: data.ID,
        //    AllowChange: true,
        //    ShowChoose: false,
        //    Type: Common.CATTypeOfFileCode.DIPOD,
        //    Window: win,
        //    Complete: function (image) {
        //        $scope.Item.Image = image.FilePath;
        //    }
        //});
    }


    $scope.QuickSave = function ($event, data) {
        if ($event.which === 13) {
            Common.Log("QuickSave");
            var error = false;
            var ispod = false;
            if (Common.HasValue(data.InvoiceNote) && data.InvoiceNote != '') {
                var str = data.InvoiceDateString.split("/");
                if (str.length == 2) {
                    var day = parseInt(str[0]);
                    var month = parseInt(str[1]);
                    ispod = true;
                    if (day > 0 && day <= 31 && month > 0 && month < 13) {
                        data.InvoiceDate = new Date(new Date().getFullYear, month - 1, day);
                    }
                    else {
                        $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                        error = true;
                    }
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                    error = true;
                }
            }

            if (!error) {
                if (ispod) {
                    $scope.CTConfirm({
                        Lable: "Bạn đã nhận chứng từ này ?",
                        TextOK: "Đã nhận",
                        TextDeny: "Chưa nhận",
                        OK: function () {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.MON,
                                method: _MONInput_InputProduction.URL.Update,
                                data: { 'item': data, ispod: true },
                                success: function (res) {
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    $scope.MONInput_Production_gridOptions.dataSource.read();
                                }
                            })
                        },
                        Deny: function () {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.MON,
                                method: _MONInput_InputProduction.URL.Update,
                                data: { 'item': data, ispod: false },
                                success: function (res) {
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    $scope.MONInput_Production_gridOptions.dataSource.read();
                                }
                            })
                        }
                    });
                }
                else {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONInput_InputProduction.URL.Update,
                        data: { 'item': data, ispod: false },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.MONInput_Production_gridOptions.dataSource.read();
                        }
                    });
                }
            }
        }
    }

    $scope.LineSave = function (data) {
        Common.Log("LineSave");
        var error = false;
        var ispod = false;
        if (Common.HasValue(data.InvoiceNote) && data.InvoiceNote != '') {
            var str = data.InvoiceDateString.split("/");
            if (str.length == 2) {
                var day = parseInt(str[0]);
                var month = parseInt(str[1]);
                ispod = true;
                if (day > 0 && day <= 31 && month > 0 && month < 13) {
                    data.InvoiceDate = new Date(new Date().getFullYear, month - 1, day);
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                    error = true;
                }
            }
            else {
                $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                error = true;
            }
        }

        if (!error) {
            if (ispod) {
                $scope.CTConfirm({
                    Lable: "Bạn đã nhận chứng từ này ?",
                    TextOK: "Đã nhận",
                    TextDeny: "Chưa nhận",
                    OK: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: _MONInput_InputProduction.URL.Update,
                            data: { 'item': data, ispod: true },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.MONInput_Production_gridOptions.dataSource.read();
                            }
                        })
                    },
                    Deny: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: _MONInput_InputProduction.URL.Update,
                            data: { 'item': data, ispod: false },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.MONInput_Production_gridOptions.dataSource.read();
                            }
                        })
                    }
                });
            }
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONInput_InputProduction.URL.Update,
                    data: { 'item': data, ispod: false },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.MONInput_Production_gridOptions.dataSource.read();
                    }
                });
            }
        }
    }

    $scope.MONInput_Production_DNClick = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONInput_InputProduction.URL.DNGet,
            data: { DITOGroupProductID: data.DITOGroupID },
            success: function (res) {
                $scope.Item = res;
                $scope.Item.InvoiceDate = Common.Date.FromJson($scope.Item.InvoiceDate)
                $scope
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
    $scope.numQuantityTranfer_options = { format: 'n3', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 3, }

    $scope.DNChangeInvoiceDate = function ($event) {
        if ($event.which === 38) {
            //up arrow

            $scope.Item.InvoiceDate = Common.Date.AddDay($scope.Item.InvoiceDate, 1);
            $scope.Item.InvoiceDateString = Common.Date.ToString($scope.Item.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 40) {
            //down arrow
            $scope.Item.InvoiceDate = Common.Date.AddDay($scope.Item.InvoiceDate, -1);
            $scope.Item.InvoiceDateString = Common.Date.ToString($scope.Item.InvoiceDate, Common.Date.Format.DDMM)
        }
    }

    $scope.DN_SaveClick = function ($event, win) {
        $event.preventDefault();
        var str = $scope.Item.InvoiceDateString.split("/");

        if (str.length == 2) {
            if (Common.HasValue($scope.Item.InvoiceNote) && $scope.Item.InvoiceNote != '') {
                var day = parseInt(str[0]);
                var month = parseInt(str[1]);
                if (day > 0 && day <= 31 && month > 0 && month < 13) {
                    $scope.Item.InvoiceDate = new Date(new Date().getFullYear, month - 1, day);
                    if ($scope.Item.QuantityTranfer <= $scope.Item.KMax) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: _MONInput_InputProduction.URL.DNSave,
                            data: { 'item': $scope.Item },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                win.close();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.MONInput_Production_gridOptions.dataSource.read();
                            }
                        })
                    }
                    else $rootScope.Message({ Msg: 'Số lượng không chính xác không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                }
            } else $rootScope.Message({ Msg: 'Số chứng từ không được trống', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    //#region add hàng trả về
    $scope.numQuantityReturn_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0 }
    $scope.cbbGroupProductReturn_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.ItemReturn.ProductID = "";
            $scope.LoadProductReturn($scope.ItemReturn);
        }
    }
    $scope.cbbProductReturn_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ProductName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProductName: { type: 'string' },
                }
            }
        })
    }

    $scope.LoadProductReturn = function (item) {
        var GOPID = item.GroupProductID;
        var ProID = item.ProductID;
        var data = _MONInput_InputProduction.Data.ProductReturn[GOPID];
        $scope.cbbProductReturn_Options.dataSource.data(data);
        if (ProID < 1) {
            item.ProductID = data[0].ID;
        }
    }

    $scope.MONInput_Production_AddReturnClick = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.DITOGroupProductID = data.DITOGroupID;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONInput_InputProduction.URL.AddReturnGet,
            data: { DITOGroupProductID: data.DITOGroupID },
            success: function (res) {
                $scope.ItemReturn = res.ItemReturn;
                _MONInput_InputProduction.Data.ProductReturn = {};
                Common.Data.Each(res.ListProduct, function (o) {
                    if (!Common.HasValue(_MONInput_InputProduction.Data.ProductReturn[o.GroupOfProductID])) {
                        _MONInput_InputProduction.Data.ProductReturn[o.GroupOfProductID] = [o]
                    }
                    else _MONInput_InputProduction.Data.ProductReturn[o.GroupOfProductID].push(o);
                })

                $scope.cbbGroupProductReturn_Options.dataSource.data(res.ListGroupProduct);
                $scope.LoadProductReturn($scope.ItemReturn);
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.MONInput_Production_EditReturnClick = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONInput_InputProduction.URL.AddReturnEditGet,
            data: { DITOProductID: data.ID },
            success: function (res) {
                $scope.ItemReturn = res.ItemReturn;
                $scope.DITOGroupProductID = res.ItemReturn.DITOGroupID;
                _MONInput_InputProduction.Data.ProductReturn = {};
                Common.Data.Each(res.ListProduct, function (o) {
                    if (!Common.HasValue(_MONInput_InputProduction.Data.ProductReturn[o.GroupOfProductID])) {
                        _MONInput_InputProduction.Data.ProductReturn[o.GroupOfProductID] = [o]
                    }
                    else _MONInput_InputProduction.Data.ProductReturn[o.GroupOfProductID].push(o);
                })

                $scope.cbbGroupProductReturn_Options.dataSource.data(res.ListGroupProduct);
                $scope.LoadProductReturn($scope.ItemReturn);
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };
    $scope.AddReturn_DeleteClick = function ($event, win) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONInput_InputProduction.URL.AddReturnEditDelte,
                    data: { DITOProductID: $scope.ItemReturn.ID },
                    success: function (res) {
                        $scope.MONInput_Production_grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã xóa', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };
    $scope.AddReturn_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn lưu dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONInput_InputProduction.URL.AddReturnSave,
                        data: { item: $scope.ItemReturn, DITOGroupProductID: $scope.DITOGroupProductID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.MONInput_Production_grid.dataSource.read();
                            win.close();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    };
    //#endregion

    //#region Confirm window

    $scope.WinConfirmObj = {
        Lable: "Đang tải thông báo...",
        OK: function () { },
        Deny: function () { },
    }
    $scope.CTConfirm = function (opstions) {
        $scope.WinConfirmObj = {
            Lable: "",
            OK: function () {
            },
            Deny: function () {
            },
            TextOK: "Đồng ý",
            TextDeny: "Không đồng ý",
        }
        angular.extend($scope.WinConfirmObj, opstions);

        $scope.WinConfirmObj.Action_OK = function () {
            $scope.WinConfirmObj.OK();
            $scope.Confirm_Win.close();
        }
        $scope.WinConfirmObj.Action_Deny = function () {
            $scope.WinConfirmObj.Deny();
            $scope.Confirm_Win.close();
        }
        $scope.Confirm_Win.center().open();
    }

    //#endregion

    //actions

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Index")
    };

    String.prototype.toDate = function (format) {
        var normalized = this.replace(/[^a-zA-Z0-9]/g, '-');
        var normalizedFormat = format.toLowerCase().replace(/[^a-zA-Z0-9]/g, '-');
        var formatItems = normalizedFormat.split('-');
        var dateItems = normalized.split('-');

        var monthIndex = formatItems.indexOf("mm");
        var dayIndex = formatItems.indexOf("dd");
        var yearIndex = formatItems.indexOf("yyyy");
        var hourIndex = formatItems.indexOf("hh");
        var minutesIndex = formatItems.indexOf("ii");
        var secondsIndex = formatItems.indexOf("ss");

        var today = new Date();

        var year = yearIndex > -1 ? dateItems[yearIndex] : today.getFullYear();
        var month = monthIndex > -1 ? dateItems[monthIndex] - 1 : today.getMonth() - 1;
        var day = dayIndex > -1 ? dateItems[dayIndex] : today.getDate();

        var hour = hourIndex > -1 ? dateItems[hourIndex] : today.getHours();
        var minute = minutesIndex > -1 ? dateItems[minutesIndex] : today.getMinutes();
        var second = secondsIndex > -1 ? dateItems[secondsIndex] : today.getSeconds();

        return new Date(year, month, day, hour, minute, second);
    };

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }
}]);