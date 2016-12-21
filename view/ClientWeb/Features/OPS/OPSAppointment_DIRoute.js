/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _OPSAppointment_DIRoute = {
    URL: {
        CustomerList: 'OPS_DIAppointment_Route_CustomerList',
        OrderList: 'OPS_DIAppointment_Route_OrderList',
        OrderDNCodeChange: 'OPS_DIAppointment_Route_OrderDNCodeChange',
        OrderDetailList: 'OPS_DIAppointment_Route_OrderDetail',
        OrderGroup: 'OPS_DIAppointment_Route_OrderGroup',
        OrderDiv: 'OPS_DIAppointment_Route_OrderDiv',
        OrderDivCustomGet: 'OPS_DIAppointment_Route_OrderDivCustomGet',
        VehicleList: 'OPS_DIAppointment_Route_VehicleList',
        VehicleTOVENList: 'OPS_DIAppointment_Route_VehicleTOVENList',
        VehicleTimeList: 'OPS_DIAppointment_Route_VehicleTimeList',
        VehicleTOVEN: 'OPS_DIAppointment_Route_VehicleTOVEN',
        VehicleTimeGet: 'OPS_DIAppointment_Route_VehicleTimeGet',
        ActivityGet: 'OPS_DIAppointment_Route_ActivityGet',
        VehicleGet: 'OPS_DIAppointment_Route_VehicleGet',
        VehicleDetail: 'OPS_DIAppointment_Route_VehicleDetail',
        VehicleListDriver: 'OPS_DIAppointment_Route_VehicleListDriver',
        VehicleTOVENInDate: 'OPS_DIAppointment_Route_VehicleTOVENInDate',
        VehicleListGroupVehicle: 'OPS_DIAppointment_Route_VehicleListGroupVehicle',
        VehicleAdd: 'OPS_DIAppointment_Route_VehicleAdd',
        VehicleRemoveMonitor: 'OPS_DIAppointment_Route_VehicleRemoveMonitor',
        VehicleSave: 'OPS_DIAppointment_Route_VehicleSave',
        VehicleRemove: 'OPS_DIAppointment_Route_VehicleRemove',
        VehicleMonitor: 'OPS_DIAppointment_Route_VehicleMonitor',
        ActivitySave: 'OPS_DIAppointment_Route_ActivitySave',
        ActivityGet: 'OPS_DIAppointment_Route_ActivityGet',
        ActivityDel: 'OPS_DIAppointment_Route_ActivityDel',
        VehicleAddRate: 'OPS_DIAppointment_Route_VehicleAddRate',
        VehicleListVehicle: 'OPS_DIAppointment_Route_VehicleListVehicle',
        VehicleListVendor: 'OPS_DIAppointment_Route_VehicleListVendor',
        VehicleTOVENGet: 'OPS_DIAppointment_Route_VehicleTOVENGet',
        PODList: 'OPS_DIAppointment_Route_PODList',
        PODExcelSave: 'OPS_DIAppointment_Route_PODExcelSave',
        PODDiv: 'OPS_DIAppointment_Route_PODDiv',
        PODExcelDownload: 'OPS_DIAppointment_Route_PODExcelDownload',
        PODExcelCheck: 'OPS_DIAppointment_Route_PODExcelCheck',
        PODExcelSave: 'OPS_DIAppointment_Route_PODExcelSave',
        QuickSearch: 'OPS_DIAppointment_Route_QuickSearch',
        QuickSearchGet: 'OPS_DIAppointment_Route_QuickSearchGet',
        QuickSearchApproved: 'OPS_DIAppointment_Route_QuickSearchApproved',
        QuickSearchUnApproved: 'OPS_DIAppointment_Route_QuickSearchUnApproved',
        WinVehicleSave: 'OPS_DIAppointment_Route_WinVehicleSave',
        VehicleTimelineChange: 'OPS_DIAppointment_Route_VehicleTimelineChange',

        VehicleExcelConfirm: 'DIAppointment_Route_ExcelConfirm',
    },
    Data: {
        CookieVehicle: 'OPS_Appointment_DIRoute_Vehicle',
        CookieOrder: 'OPS_Appointment_DIRoute_Order',
        CookieTabStrip: 'OPS_Appointment_DIRoute_TabStrip',
        _dataVehicle: [],
        _vehicleListSelect: [],
        _currentTimeline: null,
        _currentOrder: null,
        _vehicleid: -1,
        _vendorid: -1,
        _dataDriver: [],
        _winVendorTOID: -1,
        _id: -1,
        _dataGroupOfVehicle: [],
        _dataVehicleVEN: [],
        _dataVehicleByVEN: [],
        _dataVehicleHome: [],
        _dataVendor: [],
        _winPODItem: null,
    },
    Timeline: null
};
//endregion URL
angular.module('myapp').controller('OPSAppointment_DIRouteCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIRouteCtrl');
    $rootScope.IsLoading = false;

    $scope.IsHasChooseQuickSearch = false;

    $scope.IsShowVehicleVendor = false;
    $scope.IsShowWinToBtnMonitor = false;
    $scope.IsShowWinToBtnRemoveMonitor = false;
    $scope.IsShowWinToBtnSave = false;
    $scope.IsShowWinToBtnDel = false;
    $scope.IsShowWinToBtnAdd = false;

    $scope.IsShowWinToRateBtnMonitor = false;
    $scope.IsShowWinToRateBtnRemoveMonitor = false;
    $scope.IsShowWinToRateBtnSave = false;
    $scope.IsShowWinToRateBtnDel = false;
    $scope.IsShowWinToRateBtnAdd = false;

    $scope.IsShowWinActivityDel = false;

    $scope.Vehicle = {};
    $scope.Vehicle.IsShowConfig = false;

    $scope.Order = {};
    $scope.Order.IsShowConfig = false;
    $scope.Order.ListCustomerID = [];

    $scope.WinTO = {};
    $scope.WinTORate = {};
    $scope.WinActivity = {};
    $scope.WinPODDiv = {};

    $scope.OPSAppointment_DIRoute_splitterOptions = {
        orientation: "horizontal",
        panes: [
                { collapsible: true, resizable: true, size: '50%' },
                { collapsible: true, resizable: true, size: '50%' }
        ],
        collapse: function (e) {
            var objCookie = { Status: 0 };
            if ($(e.pane).hasClass("k-pane-vehicle")) {
                objCookie.Status = 2;
            }
            if ($(e.pane).hasClass("k-pane-order")) {
                objCookie.Status = 1;
            }
            Common.Cookie.Set(_OPSAppointment_DIRoute.Data.CookieTabStrip, JSON.stringify(objCookie));
        },
        expand: function (e) {
            var objCookie = { Status: 0 };
            Common.Cookie.Set(_OPSAppointment_DIRoute.Data.CookieTabStrip, JSON.stringify(objCookie));
        },
        resize: function (e) {
            $timeout(function () {
                $scope.OPSAppointment_DIRoute_Vehicle_grid.resize();
                $scope.OPSAppointment_DIRoute_Order_grid.resize();
            }, 100);
        }
    };

    $scope.OPSAppointment_DIRoute_Vehicle_splitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: '25px' },
            { collapsible: false, resizable: true },
            { collapsible: true, resizable: true, collapsed: true, size: '200px' },
        ],
    };

    $scope.OPSAppointment_DIRoute_Order_splitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: '25px' },
            { collapsible: false, resizable: true },
            { collapsible: true, resizable: true, collapsed: true, size: '200px' }
        ]
    };

    $scope.OPSAppointment_DIRoute_TORate_tabstripOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    }

    $scope.OPSAppointment_DIRoute_OrderDetail_TabstripOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    }

    $scope.OPSAppointment_DIRoute_TO_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        TOMasterCode: { type: 'string' },
                        GroupCustom: { type: 'string' },
                        OrderCode: { type: 'string' },
                        Ton: { type: 'number' },
                        CBM: { type: 'number' },
                        Quantity: { type: 'number' },
                        TonExtra: { type: 'number' },
                        CBMExtra: { type: 'number' }
                    }
            },
            group: [{ field: 'GroupCustom' }]
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: 'GroupCustom', title: '', hidden: true, groupHeaderTemplate: '#= value #',
                sortorder: 0, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupOfProductName', width: '120px', title: 'Nhóm hàng hóa',
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProductCode', width: '100px', title: 'H.hóa / ĐVT',
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: '70px', template: '<input class="txtTon cus-number" style="width:100%"/><span class="lblTon"></span>', filterable: false, menu: false, sortable: true, title: '{{RS.OPSDITOGroupProduct.Ton}}', headerTemplate: 'Tấn<br/><span id="WinToGridTon">&nbsp;</span>',
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: '70px', template: '<input class="txtCBM cus-number" style="width:100%"/><span class="lblCBM"></span>', filterable: false, menu: false, sortable: true, title: '{{RS.OPSDITOGroupProduct.CBM}}', headerTemplate: 'Khối<br/><span id="WinToGridCBM">&nbsp;</span>',
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: '70px', template: '<input class="txtQuantity cus-number"/><span class="lblQuantity"></span>', filterable: false, menu: false, sortable: true, title: '{{RS.OPSDITOGroupProduct.Quantity}}',
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TonExtra', width: '70px', title: 'Tấn (c.l)', template: '<span class="lblTonExtra">#=Common.Number.ToNumber3(TonExtra)#</span>',
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBMExtra', width: '70px', title: 'Khối (c.l)', template: '<span class="lblCBMExtra">#=Common.Number.ToNumber3(CBMExtra)#</span>',
                
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SOCode', width: '70px', title: 'SO',
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DNCode', width: '70px', title: 'DN',
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            { title: ' ', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRoute_TO_grid) && Common.HasValue($scope.OPSAppointment_DIRoute_TO_grid.tbody)) {
                Common.Log('WinGridDataBound');

                var lst = $scope.OPSAppointment_DIRoute_TO_grid.tbody.find('tr');

                $.each(lst, function (i, tr) {
                    var item = $scope.OPSAppointment_DIRoute_TO_grid.dataItem(tr);
                    var lblTon = $(tr).find('.lblTon');
                    var lblCBM = $(tr).find('.lblCBM');
                    var lblQuantity = $(tr).find('.lblQuantity');
                    var txtTon = $(tr).find('.txtTon');
                    var txtCBM = $(tr).find('.txtCBM');
                    var txtQuantity = $(tr).find('.txtQuantity');

                    lblTon.html(Common.Number.ToNumber3(item.Ton));
                    lblCBM.html(Common.Number.ToNumber3(item.CBM));
                    lblQuantity.html(Common.Number.ToNumber3(item.Quantity));
                    txtTon.hide();
                    txtCBM.hide();
                    txtQuantity.hide();

                    if (!item.IsFTL) {
                        switch (item.TypeEditID) {
                            case 1:
                                txtTon.val(Common.Number.ToNumber3(item.Ton));
                                txtTon.show();
                                lblTon.html('');
                                if (!txtTon.hasClass('allow'))
                                    txtTon.addClass('allow');
                                break;
                            case 2:
                                txtCBM.val(Common.Number.ToNumber3(item.CBM));
                                txtCBM.show();
                                lblCBM.html('');
                                if (!txtCBM.hasClass('allow'))
                                    txtCBM.addClass('allow');
                                break;
                            case 3:
                                txtQuantity.val(Common.Number.ToNumber3(item.Quantity));
                                txtQuantity.show();
                                lblQuantity.html('');
                                if (!txtQuantity.hasClass('allow'))
                                    txtQuantity.addClass('allow');
                                break;
                        }
                    }
                });
                var lstNum = $($scope.OPSAppointment_DIRoute_TO_grid.element).find('tbody > tr > td > input.cus-number.allow').kendoNumericTextBox({
                    format: '#,##0.0##', spinners: false, culture: 'en-US', min: 0, step: 0.001, decimals: Common.Number.DI_Decimals,
                    change: function () {
                        var tr = $(this.element).closest('tr');
                        var item = $scope.OPSAppointment_DIRoute_TO_grid.dataItem(tr);

                        var lblTon = $(tr).find('.lblTon');
                        var lblCBM = $(tr).find('.lblCBM');
                        var lblQuantity = $(tr).find('.lblQuantity');
                        var lblTonExtra = $(tr).find('.lblTonExtra');
                        var lblCBMExtra = $(tr).find('.lblCBMExtra');

                        var val = this.value();
                        switch (item.TypeEditID) {
                            case 1:
                                lblCBM.html(Common.Number.ToNumber3(val * item.ExchangeCBM));
                                item.CBM = val * item.ExchangeCBM;
                                item.Ton = val;
                                break;
                            case 2:
                                lblTon.html(Common.Number.ToNumber3(val * item.ExchangeTon));
                                item.Ton = val * item.ExchangeTon;
                                item.CBM = val;
                                break;
                            case 3:
                                lblTon.html(Common.Number.ToNumber3(val * item.ExchangeTon));
                                lblCBM.html(Common.Number.ToNumber3(val * item.ExchangeCBM));
                                item.Ton = val * item.ExchangeTon;
                                item.CBM = val * item.ExchangeCBM;
                                item.Quantity = val;
                                break;
                        }
                        lblTonExtra.html(Common.Number.ToNumber3(item.TonTotal - item.Ton));
                        lblCBMExtra.html(Common.Number.ToNumber3(item.CBMTotal - item.CBM));

                        $scope.WinToLoadTotal();
                    }
                });
                $.each(lstNum, function (i, v) {
                    $(v).data('kendoNumericTextBox').max($(v).val());
                });
            }
        }
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.OPSAppointment_DIRoute_VehicleDetail_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        Code: { type: 'string' },
                        ETD: { type: 'datetime' },
                        ETA: { type: 'datetime' },
                        Ton: { type: 'number' }
                    }
            },
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'Code', title: 'Số chuyến' },
            { field: 'ETD', width: '100px', title: 'ETD', template: "#=Common.Date.FromJsonDDMMYY(ETD)#" },
            { field: 'ETA', width: '100px', title: 'ETA', template: "#=Common.Date.FromJsonDDMMYY(ETA)#" },
            { title: ' ', filterable: false, sortable: false }
        ],
    }

    $scope.OPSAppointment_DIRoute_TORate_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        TOMasterCode: { type: 'string' },
                        GroupCustom: { type: 'string' },
                        OrderCode: { type: 'string' },
                        Ton: { type: 'number' },
                        CBM: { type: 'number' },
                        Quantity: { type: 'number' },
                        TonExtra: { type: 'number' },
                        CBMExtra: { type: 'number' }
                    }
            },
            group: [{ field: 'GroupCustom' }]
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        toolbar: kendo.template($('#OPSAppointment_DIRoute_TORate_gridtoolbar').html()),
        columns: [
            { field: 'GroupCustom', title: '', hidden: true, groupHeaderTemplate: '#= value #' },
            { field: 'GroupOfProductName', width: '110px', title: 'Nhóm hàng hóa' },
            { field: 'ProductCode', width: '100px', title: 'H.hóa / ĐVT' },
            { field: 'Ton', width: '70px', template: '<input class="txtTon cus-number" style="width:100%"/><span class="lblTon"></span>', filterable: false, menu: false, sortable: true, title: 'Tấn', headerTemplate: 'Tấn<br/><span id="WinToRateGridTon">&nbsp;</span>' },
            { field: 'CBM', width: '70px', template: '<input class="txtCBM cus-number" style="width:100%"/><span class="lblCBM"></span>', filterable: false, menu: false, sortable: true, title: 'Khối', headerTemplate: 'Khối<br/><span id="WinToRateGridCBM">&nbsp;</span>' },
            { field: 'Quantity', width: '70px', template: '<input class="txtQuantity cus-number"/><span class="lblQuantity"></span>', filterable: false, menu: false, sortable: true, title: 'S.l' },
            { field: 'TonExtra', width: '70px', title: 'Tấn (c.l)', template: '<span class="lblTonExtra">#=Common.Number.ToNumber3(TonExtra)#</span>' },
            { field: 'CBMExtra', width: '70px', title: 'Khối (c.l)', template: '<span class="lblCBMExtra">#=Common.Number.ToNumber3(CBMExtra)#</span>' },
            { field: 'SOCode', width: '80px', title: 'SO' },
            { field: 'DNCode', width: '80px', title: 'DN' },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRoute_TORate_grid) && Common.HasValue($scope.OPSAppointment_DIRoute_TORate_grid.tbody)) {
                Common.Log('WinRateGridDataBound');

                var lst = $scope.OPSAppointment_DIRoute_TORate_grid.tbody.find('tr');

                $.each(lst, function (i, tr) {
                    var item = $scope.OPSAppointment_DIRoute_TORate_grid.dataItem(tr);
                    var lblTon = $(tr).find('.lblTon');
                    var lblCBM = $(tr).find('.lblCBM');
                    var lblQuantity = $(tr).find('.lblQuantity');
                    var txtTon = $(tr).find('.txtTon');
                    var txtCBM = $(tr).find('.txtCBM');
                    var txtQuantity = $(tr).find('.txtQuantity');

                    lblTon.html(Common.Number.ToNumber3(item.Ton));
                    lblCBM.html(Common.Number.ToNumber3(item.CBM));
                    lblQuantity.html(Common.Number.ToNumber3(item.Quantity));
                    txtTon.hide();
                    txtCBM.hide();
                    txtQuantity.hide();

                    if (!item.IsFTL) {
                        switch (item.TypeEditID) {
                            case 1:
                                txtTon.val(Common.Number.ToNumber3(item.Ton));
                                txtTon.show();
                                lblTon.html('');
                                if (!txtTon.hasClass('allow'))
                                    txtTon.addClass('allow');
                                break;
                            case 2:
                                txtCBM.val(Common.Number.ToNumber3(item.CBM));
                                txtCBM.show();
                                lblCBM.html('');
                                if (!txtCBM.hasClass('allow'))
                                    txtCBM.addClass('allow');
                                break;
                            case 3:
                                txtQuantity.val(Common.Number.ToNumber3(item.Quantity));
                                txtQuantity.show();
                                lblQuantity.html('');
                                if (!txtQuantity.hasClass('allow'))
                                    txtQuantity.addClass('allow');
                                break;
                        }
                    }
                });
                var lstNum = $($scope.OPSAppointment_DIRoute_TORate_grid.element).find('tbody > tr > td > input.cus-number.allow').kendoNumericTextBox({
                    format: '#,##0.0##', spinners: false, culture: 'en-US', min: 0, step: 0.001, decimals: Common.Number.DI_Decimals,
                    change: function () {
                        var tr = $(this.element).closest('tr');
                        var item = $scope.OPSAppointment_DIRoute_TORate_grid.dataItem(tr);

                        var lblTon = $(tr).find('.lblTon');
                        var lblCBM = $(tr).find('.lblCBM');
                        var lblQuantity = $(tr).find('.lblQuantity');
                        var lblTonExtra = $(tr).find('.lblTonExtra');
                        var lblCBMExtra = $(tr).find('.lblCBMExtra');

                        var val = this.value();
                        switch (item.TypeEditID) {
                            case 1:
                                lblCBM.html(Common.Number.ToNumber3(val * item.ExchangeCBM));
                                item.CBM = val * item.ExchangeCBM;
                                item.Ton = val;
                                break;
                            case 2:
                                lblTon.html(Common.Number.ToNumber3(val * item.ExchangeTon));
                                item.Ton = val * item.ExchangeTon;
                                item.CBM = val;
                                break;
                            case 3:
                                lblTon.html(Common.Number.ToNumber3(val * item.ExchangeTon));
                                lblCBM.html(Common.Number.ToNumber3(val * item.ExchangeCBM));
                                item.Ton = val * item.ExchangeTon;
                                item.CBM = val * item.ExchangeCBM;
                                item.Quantity = val;
                                break;
                        }
                        lblTonExtra.html(Common.Number.ToNumber3(item.TonTotal - item.Ton));
                        lblCBMExtra.html(Common.Number.ToNumber3(item.CBMTotal - item.CBM));

                        $scope.WinRateLoadTotal();
                    }
                });
                $.each(lstNum, function (i, v) {
                    $(v).data('kendoNumericTextBox').max($(v).val());
                });
            }
        }
    }

    $scope.OPSAppointment_DIRoute_TORateVendor_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'SortOrder',
                fields:
                    {
                        SortOrder: { type: 'number', editable: false },
                        VendorID: { type: 'number' },
                        IsManual: { type: 'boolean' },
                        Debit: { type: 'number' },
                        VehicleCode: { type: 'string' }
                    }
            },
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'SortOrder', width: '50px', title: 'T.tự' },
            { field: 'VendorID', width: '300px', title: 'Nhà vận tải', template: '<input class="cus-combobox cboVendor" kendo-combo-box k-options="cboVendorOptions" data-bind="value:VendorID" ng-model="dataItem.VendorID" k-data-source="dataItem.ListVendor"/>' },
            { field: 'IsManual', width: '70px', title: 'Nhập giá', template: '<input class="chkIsManual" type="checkbox" #= IsManual ? checked="checked" : "" #/>', filterable: false, menu: false, sortable: true },
            { field: 'Debit', width: '120px', title: 'Giá', template: '<input class="txtDebit cus-number" value="#=Debit#" style="width:100%"/>', filterable: false, menu: false, sortable: true },
            { field: 'VehicleCode', width: '120px', title: 'Xe', template: '<input class="cus-combobox cboVehicle" kendo-combo-box k-options="cboVehicleOptions" data-bind="value:VehicleCode" ng-model="dataItem.VehicleCode" k-data-source="dataItem.lstVehicle"/>', filterable: false, menu: false, sortable: true },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRoute_TORateVendor_grid) && Common.HasValue($scope.OPSAppointment_DIRoute_TORateVendor_grid.tbody)) {
                Common.Log('WinRateGridVENDataBound');

                $($scope.OPSAppointment_DIRoute_TORateVendor_grid.element).find('.txtDebit').kendoNumericTextBox({
                    format: '#,##0', spinners: false, culture: 'en-US', min: 0, max: 100000000, step: 1000, decimals: 0,
                    value: 0
                });

                $($scope.OPSAppointment_DIRoute_TORateVendor_grid.element).find('.chkIsManual').change(function () {
                    var tr = $(this).closest('tr');
                    var txtDebit = $($(tr).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                    txtDebit.enable($(this).prop('checked'));
                });

                var lst = $scope.OPSAppointment_DIRoute_TORateVendor_grid.tbody.find('tr');
                $.each(lst, function (itr, tr) {
                    var item = $scope.OPSAppointment_DIRoute_TORateVendor_grid.dataItem(tr);
                    
                    var txtDebit = $($(tr).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                    var chkIsManual = $($(this).find('.chkIsManual')[0]);
                    if (item.IsManual == true)
                        txtDebit.enable(true);
                    else
                        txtDebit.enable(false);
                    chkIsManual.prop('checked', item.IsManual);

                    var cbbVehicle = $($(tr).find('input.cboVehicle.cus-combobox')[1]).data('kendoComboBox');
                    cbbVehicle.value(item.VehicleCode);
                    cbbVehicle.trigger('change');
                });
            }
        }
    }

    $scope.OPSAppointment_DIRoute_VendorTO_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'TOMasterID',
                fields:
                    {
                        TOMasterID: { type: 'number', editable: false },
                        TOMasterCode: { type: 'string' },
                        VehicleCode: { type: 'string' },
                        ETD: { type: 'date' },
                        ETA: { type: 'date' },
                        DriverName: { type: 'string' },
                        DriverTelNo: { type: 'string' }
                    }
            },
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'TOMasterCode', width: '80px', template: '<a class="todetail" href="/">#=TOMasterCode#</a>', title: 'Lệnh' },
            { field: 'VehicleCode', width: '80px', template: '<a class="todetail" href="/">#=VehicleCode#</a>', title: 'Xe' },
            { field: 'ETD', width: '100px', template: '#=Common.Date.FromJsonDDMMHM(ETD)#', title: 'Ngày lấy hàng' },
            { field: 'ETA', width: '100px', template: '#=Common.Date.FromJsonDDMMHM(ETA)#', title: 'Ngày giao hàng' },
            { field: 'DriverName', width: '90px', title: 'Tài xế' },
            { field: 'DriverTelNo', width: '90px', title: 'Điện thoại' },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRoute_VendorTO_grid) && Common.HasValue($scope.OPSAppointment_DIRoute_VendorTO_grid.tbody)) {
                Common.Log('WinVendorTOGridDataBound');

                $.each($scope.OPSAppointment_DIRoute_VendorTO_grid.tbody.find('tr'), function (i, tr) {
                    var item = $scope.OPSAppointment_DIRoute_VendorTO_grid.dataItem(tr);

                    switch (item.TypeID) {
                        case 1: if (!$(tr).hasClass('approved')) $(tr).addClass('approved'); break;
                        case 2: if (!$(tr).hasClass('tendered')) $(tr).addClass('tendered'); break;
                        case 3: if (!$(tr).hasClass('received')) $(tr).addClass('received'); break;
                    }
                });

                $($scope.OPSAppointment_DIRoute_VendorTO_grid.element).find('a.todetail').click(function (e) {
                    e.preventDefault();
                    var tr = $(this).closest('tr');
                    var item = $scope.OPSAppointment_DIRoute_VendorTO_grid.dataItem(tr);

                    $scope.WinToRateLoad(item.TOMasterID, item.TypeID, item.VehicleID);
                });
            }
        }
    }

    $scope.OPSAppointment_DIRoute_POD_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        VehicleCode: { type: 'string' },
                        SOCode: { type: 'string' },
                        RequestDateEmpty: { type: 'date' },
                        RequestDate: { type: 'date' },
                        RouteCode: { type: 'string' },
                        Ton: { type: 'number' },
                        Note1: { type: 'string' },
                        Note2: { type: 'string' },
                        DNCode: { type: 'string' },
                        IsOrigin: { type: 'boolean' }
                    }
            },
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, filterable: { mode: 'row' },
        columns: [
            { field: 'IsOrigin', width: '60px', title: '{{RS.OPSDITOGroupProduct.IsOrigin}}', template: '<input type="checkbox" class="chkIsOrigin" #= IsOrigin ? checked="checked" : ""  # />', sortable: false, filterable: false },
            { field: 'VehicleCode', width: '80px', title: 'Xe', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '80px', title: '{{RS.ORDOrder.Code}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: '80px', title: 'Order No', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDateEmpty', width: '120px', title: 'Ngày nhận đơn', template: '#=Common.Date.FromJsonDDMMHM(RequestDate)#', sortable: true, format: "{0: dd/MM/yyyy}", filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'RouteCode', width: '100px', title: 'Route Description', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Kg', width: '70px', title: '', template: '#=Common.Number.ToNumber3(Ton)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Note2', width: '70px', title: '{{RS.OPSDITOGroupProduct.Note2}}', template: '<input class="txtInput txtNote2 k-textbox" value="#=Note2==null?\'\':Note2#" style="width:100%" />', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: '90px', title: '{{RS.OPSDITOGroupProduct.Note1}}', template: '<input class="txtInput txtNote1 k-textbox" value="#=Note1==null?\'\':Note1#" style="width:100%" />', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', template: '<input class="txtInput txtDNCode k-textbox" value="#=DNCode==null?\'\':DNCode#" style="width:100%" />', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { width: '90px', title: '', template: '<a href="" class="k-button btnDiv">Chia DN</a>' },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRoute_POD_grid) && Common.HasValue($scope.OPSAppointment_DIRoute_POD_grid.tbody)) {
                Common.Log('PODGridDataBound');

                $scope.OPSAppointment_DIRoute_POD_grid.tbody.find('tr .btnDiv').click(function (e) {
                    e.preventDefault();

                    var tr = $(this).closest('tr');
                    _OPSAppointment_DIRoute.Data._winPODItem = $scope.OPSAppointment_DIRoute_POD_grid.dataItem(tr);
                    $scope.WinPODDivLoad();
                    $scope.OPSAppointment_DIRoute_PODDiv_win.center().open();
                });

                $scope.OPSAppointment_DIRoute_POD_grid.tbody.find('tr .txtInput').change(function () {
                    var tr = $(this).closest('tr');
                    var item = $scope.OPSAppointment_DIRoute_POD_grid.dataItem(tr);
                    var txtNote1 = $(tr).find('input.txtInput.txtNote1');
                    var txtNote2 = $(tr).find('input.txtInput.txtNote2');
                    var txtDNCode = $(tr).find('input.txtInput.txtDNCode');
                    item.ExcelSuccess = true;

                    if (Common.HasValue(txtNote1))
                        item.Note1 = $(txtNote1).val();
                    if (Common.HasValue(txtNote2))
                        item.Note2 = $(txtNote2).val();
                    if (Common.HasValue(txtDNCode))
                        item.DNCode = $(txtDNCode).val();
                });

                $scope.OPSAppointment_DIRoute_POD_grid.tbody.find('tr .chkIsOrigin').change(function () {
                    var tr = $(this).closest('tr');
                    var item = $scope.OPSAppointment_DIRoute_POD_grid.dataItem(tr);
                    item.IsOrigin = $(this).prop('checked');
                    item.ExcelSuccess = true;
                });
            }
        }
    }

    $scope.OPSAppointment_DIRoute_QuickSearch_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        IsChoose: { type: 'boolean' },
                        VehicleCode: { type: 'string' },
                        SOCode: { type: 'string' },
                        RequestDateEmpty: { type: 'date' },
                        RequestDate: { type: 'date' },
                        RouteCode: { type: 'string' },
                        Ton: { type: 'number' },
                        Note1: { type: 'string' },
                        Note2: { type: 'string' },
                        DNCode: { type: 'string' }
                    }
            },
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSAppointment_DIRoute_QuickSearch_grid,OPSAppointment_DIRoute_QuickSearch_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSAppointment_DIRoute_QuickSearch_grid,OPSAppointment_DIRoute_QuickSearch_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'VehicleCode', width: '80px', title: 'Xe', template: "<a href=\"\" class=\"openAction\">#=VehicleCode#</a>", sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '80px', title: '{{RS.ORDOrder.Code}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: '80px', title: 'Order No', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDateEmpty', width: '120px', title: '{{RS.ORDOrder.RequestDate}}', template: '#=Common.Date.FromJsonDDMMHM(RequestDate)#', sortable: true, format: "{0: dd/MM/yyyy}", filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'RouteCode', width: '100px', title: 'Route Description', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '70px', title: '{{RS.OPSDITOGroupProduct.Ton}}', template: '#=Common.Number.ToNumber3(Ton)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Note2', width: '70px', title: '{{RS.OPSDITOGroupProduct.Note2}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: '90px', title: '{{RS.OPSDITOGroupProduct.Note2}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRoute_QuickSearch_grid) && Common.HasValue($scope.OPSAppointment_DIRoute_QuickSearch_grid.tbody)) {
                Common.Log('_winQuickSearchGridDataBound');

                $scope.OPSAppointment_DIRoute_QuickSearch_grid.tbody.find('tr .openAction').click(function (e) {
                    e.preventDefault();

                    var tr = $(this).closest('tr');
                    var dataItem = $scope.OPSAppointment_DIRoute_QuickSearch_grid.dataItem(tr);
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.QuickSearchGet,
                        data: { id: dataItem.ID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.WinQuickSearchLoadItem(res);
                            });
                        }
                    });
                });

                $scope.OPSAppointment_DIRoute_QuickSearch_grid.tbody.find('tr .chkChoose').change(function (e) {
                    var me = Common.View;

                    var tr = $(this).closest('tr');
                    var dataItem = $scope.OPSAppointment_DIRoute_QuickSearch_grid.dataItem(tr);
                    dataItem.IsChoose = $(this).prop('checked');
                });

                $scope.OPSAppointment_DIRoute_QuickSearch_grid.tbody.find('tr').each(function () {
                    var dataItem = $scope.OPSAppointment_DIRoute_QuickSearch_grid.dataItem(this);
                    switch (dataItem.TypeID) {
                        case 1: $(this).addClass('approved'); break;
                        case 2: $(this).addClass('tendered'); break;
                        case 3: $(this).addClass('received'); break;
                    }
                });
            }
        }
    }

    $scope.OPSAppointment_DIRoute_VehicleLimit_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        RegNo: { type: 'string' },
                        MaxWeight: { type: 'number' },
                        MaxCapacity: { type: 'number' }
                    }
            },
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, filterable: { mode: 'row' },
        columns: [
            { field: 'RegNo', width: '120px', title: 'Xe tải', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxWeight', width: '100px', title: 'T.tải', template: '<input class="txtMaxWeight cus-number" value="#=MaxWeight#" style="width:100%" />', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxCapacity', width: '100px', title: 'Khối', template: '<input class="txtMaxCapacity cus-number" value="#=MaxCapacity#" style="width:100%" />', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRoute_VehicleLimit_grid) && Common.HasValue($scope.OPSAppointment_DIRoute_VehicleLimit_grid.tbody)) {
                Common.Log('WinVehicleGridDataBound');

                $($scope.OPSAppointment_DIRoute_VehicleLimit_grid.element).find('tbody > tr > td > input.txtMaxWeight.cus-number').kendoNumericTextBox({
                    format: '#0.0', spinners: false, culture: 'en-US', min: 0, max: 100, step: 1, decimals: 1
                });

                $($scope.OPSAppointment_DIRoute_VehicleLimit_grid.element).find('tbody > tr > td > input.txtMaxCapacity.cus-number').kendoNumericTextBox({
                    format: '#0.0', spinners: false, culture: 'en-US', min: 0, max: 1000, step: 1, decimals: 1
                });
            }
        }
    }

    $scope.OPSAppointment_DIRoute_QuickSearch_gridChooseChange = function ($event, grid, haschoose) {
        $scope.IsHasChooseQuickSearch = haschoose;
    };

    $scope.OPSAppointment_DIRoute_TO_win_cbbDriverOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data:[], 
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Text: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var driveritem = e.sender.dataItem();
            $scope.WinTO.DriverTelNo = driveritem.DriverTelNo;
        }
    };

    $scope.cboVendorOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID',
        change: function (e) {
            var tr = $(this.element).closest('tr');
            var item = $scope.OPSAppointment_DIRoute_TORateVendor_grid.dataItem(tr);
            item.lstVehicle = _OPSAppointment_DIRoute.Data._dataVehicleByVEN[this.value()];
            $timeout(function () {
                if (item.lstVehicle.length > 0) {
                    item.VehicleCode = item.lstVehicle[0].RegNo;
                    item.RegNo = item.VehicleCode;
                }
                    
               
            }, 10);
        }
    };

    $scope.cboVehicleOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'RegNo', dataValueField: 'RegNo',
        change: function (e) {

        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRoute.URL.VehicleListDriver,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRoute.Data._dataDriver = [];
                _OPSAppointment_DIRoute.Data._dataDriver.push({ 'ID': -1, 'Text': '', 'DriverName': '', 'DriverTelNo': '' });
                $.each(res, function (i, v) {
                    _OPSAppointment_DIRoute.Data._dataDriver.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTelNo': v.Cellphone });
                });
                $scope.OPSAppointment_DIRoute_TO_win_cbbDriverOptions.dataSource.data(_OPSAppointment_DIRoute.Data._dataDriver);
            });
        }
    });

    $scope.OPSAppointment_DIRoute_TORate_win_cbbTransportOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data:[], 
            model: {
                id: 'Value',
                fields: {
                    Value: { type: 'number' },
                    Text: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex == 0)
                $(".divWinToRateGroupVehicle").show();
            else
                $("._divWinToRateGroupVehicle").hide();
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.SLI_SYSVarTransportModeOPSDI,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                $scope.OPSAppointment_DIRoute_TORate_win_cbbTransportOptions.dataSource.data(res.Data);
                $scope.WinTORate.TransportID = res.Data[0].Value;
            });
        }
    });

    $scope.OPSAppointment_DIRoute_TORate_win_cbbGroupVehicleOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' }
                }
            }
        })
    };

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRoute.URL.VehicleListGroupVehicle,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRoute.Data._dataGroupOfVehicle = $.extend(true, [], res);
                $scope.OPSAppointment_DIRoute_TORate_win_cbbGroupVehicleOptions.dataSource.data(res);
                $scope.WinTORate.GroupVehicleID = res[0].ID;
            });
        }
    });

    $scope.OPSAppointment_DIRoute_Activity_win_cbbActivityTypeOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'Value',
                fields: {
                    Value: { type: 'number' },
                    Text: { type: 'string' }
                }
            }
        })
    };

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.SLI_SYSVarTypeOfActivity,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                $scope.OPSAppointment_DIRoute_Activity_win_cbbActivityTypeOptions.dataSource.data(res.Data);
                $scope.WinActivity.TypeOfActivityID = res.Data[0].Value;
            });
        }
    });

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRoute.URL.VehicleListVehicle,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRoute.Data._dataVehicleVEN = res;
                $scope.InitWinToRate_LoadComplete();
                if (_OPSAppointment_DIRoute.Data._dataVehicleVEN.length > 0 && _OPSAppointment_DIRoute.Data._dataVendor.length > 0) 
                    $scope.InitDataVendorVehicle();
            });
        }
    });

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRoute.URL.VehicleListVendor,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRoute.Data._dataVendor = res;
                $scope.InitWinToRate_LoadComplete();
                if (_OPSAppointment_DIRoute.Data._dataVehicleVEN.length > 0 && _OPSAppointment_DIRoute.Data._dataVendor.length > 0) 
                    $scope.InitDataVendorVehicle();
            });
        }
    });

    $scope.InitDataVendorVehicle = function () {
        var lstVehicle = _OPSAppointment_DIRoute.Data._dataVehicleVEN;
        var lstVendor = _OPSAppointment_DIRoute.Data._dataVendor;
        _OPSAppointment_DIRoute.Data._dataVehicleByVEN = [];
        _OPSAppointment_DIRoute.Data._dataVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue(_OPSAppointment_DIRoute.Data._dataVehicleByVEN[vendorid]))
                        _OPSAppointment_DIRoute.Data._dataVehicleByVEN[vendorid] = [];
                    _OPSAppointment_DIRoute.Data._dataVehicleByVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _OPSAppointment_DIRoute.Data._dataVehicleHome.push(vehicle);
                }
            } else {
                _OPSAppointment_DIRoute.Data._dataVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_OPSAppointment_DIRoute.Data._dataVehicleByVEN[vendorid]))
                        _OPSAppointment_DIRoute.Data._dataVehicleByVEN[vendorid] = [];
                    _OPSAppointment_DIRoute.Data._dataVehicleByVEN[vendorid].push(vehicle);
                }
            }
        });

        var data = [];
        data.push({ SortOrder: 1, VendorID: -1, IsManual: false, Debit: 0, VehicleCode: '', lstVehicle: _OPSAppointment_DIRoute.Data._dataVehicleHome, lstVendor: _OPSAppointment_DIRoute.Data._dataVendor });
        data.push({ SortOrder: 2, VendorID: -1, IsManual: false, Debit: 0, VehicleCode: '', lstVehicle: _OPSAppointment_DIRoute.Data._dataVehicleHome, lstVendor: _OPSAppointment_DIRoute.Data._dataVendor });
        data.push({ SortOrder: 3, VendorID: -1, IsManual: false, Debit: 0, VehicleCode: '', lstVehicle: _OPSAppointment_DIRoute.Data._dataVehicleHome, lstVendor: _OPSAppointment_DIRoute.Data._dataVendor });
        
        $scope.OPSAppointment_DIRoute_TORateVendor_grid.dataSource.data(data);
    };

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');

        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRoute.Data.CookieVehicle);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Vehicle.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Vehicle.DateTo = Common.Date.FromJson(objCookie.DateTo);
                $scope.Vehicle.HourFrom = Common.Date.FromJson(objCookie.HourFrom);
                $scope.Vehicle.HourTo = Common.Date.FromJson(objCookie.HourTo);
                $scope.Vehicle.RouteInDay = objCookie.RouteInDay;
            } catch (e) { }
        }
        if (!Common.HasValue($scope.Vehicle.DateFrom) || !Common.HasValue($scope.Vehicle.DateTo)) {
            $scope.Vehicle.DateFrom = new Date().addDays(-2);
            $scope.Vehicle.DateTo = new Date().addDays(2);
            $scope.Vehicle.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Vehicle.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Vehicle.RouteInDay = 3;
        }

        strCookie = Common.Cookie.Get(_OPSAppointment_DIRoute.Data.CookieOrder);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Order.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Order.DateTo = Common.Date.FromJson(objCookie.DateTo);
            } catch (e) { }
        }
        if (!Common.HasValue($scope.Order.DateFrom) || !Common.HasValue($scope.Order.DateTo)) {
            $scope.Order.DateFrom = new Date().addDays(-2);
            $scope.Order.DateTo = new Date().addDays(2);
        }

        var currentDate = Common.Date.AddDay(new Date(), 1);
        if ($scope.Vehicle.DateTo.getTime() < currentDate.getTime()) {
            if ($scope.Vehicle.DateFrom.getTime() == $scope.Vehicle.DateTo.getTime()) {
                $scope.Vehicle.DateFrom = Common.Date.Date(new Date());
                $scope.Vehicle.DateTo = Common.Date.Date(new Date());
            }
            else {
                $scope.Vehicle.DateFrom = Common.Date.AddDay(new Date(), -2);
                $scope.Vehicle.DateTo = Common.Date.AddDay(new Date(), 2);
            }
        }
        if ($scope.Order.DateTo.getTime() < currentDate.getTime()) {
            if ($scope.Order.DateFrom.getTime() == $scope.Order.DateTo.getTime()) {
                $scope.Order.DateFrom = Common.Date.Date(new Date());
                $scope.Order.DateTo = Common.Date.Date(new Date());
            }
            else {
                $scope.Order.DateFrom = Common.Date.AddDay(new Date(), -2);
                $scope.Order.DateTo = Common.Date.AddDay(new Date(), 2);
            }
        }
    };

    $scope.Init_LoadCookie();

    $scope.OPSAppointment_DIRoute_Vehicle_LoadLabel = function () {
        $scope.Vehicle.ConfigString = '';
        var lst = _OPSAppointment_DIRoute.Timeline.GetListRouteInDay();
        var str = '';
        $.each(lst, function (i, v) {
            str += '[' + v.Name + ']:' + Common.Date.ToString(v.HourFrom, Common.Date.Format.HM) + '-' + Common.Date.ToString(v.HourTo, Common.Date.Format.HM) + '&nbsp;&nbsp;';
        });

        if (str != '')
            str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
                Common.Date.ToString($scope.Vehicle.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' + str;
        else
            str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' +
                Common.Date.ToString($scope.Vehicle.HourFrom, Common.Date.Format.HM) + ' - ' + Common.Date.ToString($scope.Vehicle.HourTo, Common.Date.Format.HM);
        $scope.Vehicle.ConfigString = str;
    }

    $scope.OPSAppointment_DIRoute_VehicleMenuOptions = {
        orientation: 'vertical',
        target: '#OPSAppointment_DIRoute_Vehicle_grid',
        filter: 'tr',
        animation: { open: { effects: 'fadeIn' }, duration: 300 },
        open: function (e) {
            var pn = $($scope.OPSAppointment_DIRoute_Vehicle_splitter.element.find('.k-pane')[2]).data('pane');
            if (pn.collapsed)
                $($scope.OPSAppointment_DIRoute_VehicleMenu.element.find(".lblVehicleDetail")).html('Hiện chi tiết xe');
            else
                $($scope.OPSAppointment_DIRoute_VehicleMenu.element.find(".lblVehicleDetail")).html('Ẩn chi tiết xe');

            if ($scope.IsShowVehicleVendor)
                $($scope.OPSAppointment_DIRoute_VehicleMenu.element.find(".lblVehicleList")).html('Hiện danh sách xe nhà');

            else
                $($scope.OPSAppointment_DIRoute_VehicleMenu.element.find(".lblVehicleList")).html('Hiện danh sách xe ngoài');
        },
        select: function (e) {
            var index = $(e.item).index();
            switch (index) {
                case 0:
                    var pane = $scope.OPSAppointment_DIRoute_Vehicle_splitter.element.find('.k-pane')[2];
                    var pn = $(pane).data('pane');
                    if (pn.collapsed)
                        $scope.OPSAppointment_DIRoute_Vehicle_splitter.expand(pane);
                    else
                        $scope.OPSAppointment_DIRoute_Vehicle_splitter.collapse(pane);
                    break;
                case 1:
                    $scope.IsShowVehicleVendor = !$scope.IsShowVehicleVendor;
                    _OPSAppointment_DIRoute.Timeline.RefreshMain();
                    break;
                case 3:
                    $scope.WinVehicleOpen();
                    break;
                case 4:
                    $scope.WinMaterialOpen();
                    break;
                case 5:
                    $scope.WinActivityOpen();
                    break;
            }
        },
    };

    $timeout(function () {
        _OPSAppointment_DIRoute.Timeline = Common.Timeline({
            grid: $scope.OPSAppointment_DIRoute_Vehicle_grid,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        RegNo: { type: 'string' },
                        VehicleID: { type: 'number' },
                        VendorName: { type: 'string' },
                        MaxWeightCal: { type: 'number' }
                    }
            },
            modelGroup: [{ field: 'VendorName' }],
            modelSort: { field: 'VehicleID', dir: 'asc' },
            columns: [
                { field: 'VendorName', hidden: true, groupHeaderTemplate: '#= value #' },
                { field: 'RegNo', width: '90px', title: 'Xe', template: '<div class="bgtruck allowdrop"><span class="fa fa-truck"></span>&nbsp;#=RegNo#</div>', sortable: true, locked: true },
                { field: 'MaxWeightCal', width: '50px', template: '<div class="allowdrop">#=MaxWeightCal==null?"":MaxWeightCal#</div>', title: 'T.tải', sortable: true, locked: true }
            ],
            search: $scope.Vehicle,
            eventMainData: function () {
                Common.Log('VehicleMainData');
                if (!$scope.IsShowVehicleVendor) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleList,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Data._dataVehicle = [];
                                $.each(res, function (i, v) {
                                    v.VendorName = 'Xe nhà';
                                    v.VehicleID = v.ID;
                                    _OPSAppointment_DIRoute.Data._dataVehicle.push(v);
                                });

                                res.splice(0, 0, { 'ID': -1, 'RegNo': ' Xe ngoài', 'VehicleID': -1, 'MaxWeightCal': null, 'GroupOfVehicleID': null, 'GroupOfVehicleName': '', 'VendorName': '' });
                                _OPSAppointment_DIRoute.Timeline.SetMainData(res);
                            });
                        }
                    });
                } else {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleTOVENList,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                var data = res;
                                $.each(data, function (i, v) {
                                    v.MaxWeightCal = null;
                                    v.GroupOfVehicleID = null;
                                    v.GroupOfVehicleName = '';
                                });

                                res.splice(0, 0, { 'ID': -1, 'RegNo': '[ Chờ xác nhận ]', 'MaxWeightCal': null, 'GroupOfVehicleID': null, 'GroupOfVehicleName': '', 'VendorName': '' });
                                _OPSAppointment_DIRoute.Timeline.SetMainData(res);
                            });
                        }
                    });
                }
            },
            eventDetailData: function (dtFrom, dtTo) {
                Common.Log('VehicleDetailData');
                if (!$scope.IsShowVehicleVendor) {
                    var lst = _OPSAppointment_DIRoute.Timeline.GetListRouteInDay();
                    var str = '';
                    $.each(lst, function (i, v) {
                        str += '[' + v.Name + ']:' + Common.Date.ToString($scope.Vehicle.HourFrom, Common.Date.Format.HM) + '-' + Common.Date.ToString($scope.Vehicle.HourTo, Common.Date.Format.HM) + '&nbsp;&nbsp;';
                    });

                    if (str != '')
                        str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
                            Common.Date.ToString($scope.Vehicle.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' + str;
                    else
                        str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' +
                            Common.Date.ToString($scope.Vehicle.HourFrom, Common.Date.Format.HM) + ' - ' + Common.Date.ToString($scope.Vehicle.HourTo, Common.Date.Format.HM);
                    $scope.Vehicle.ConfigString = str;

                    var param = Common.Request.Create({
                        Sorts: [], Filters: [
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.GreaterThanOrEqual, dtFrom),
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.LessThanOrEqual, dtTo)
                        ]
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleTimeList,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $.each(res, function (i, v) {
                                    v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                    v.DateTo = Common.Date.FromJson(v.DateTo);
                                });
                                
                                _OPSAppointment_DIRoute.Timeline.SetDetailData(res);
                            });
                        }
                    });
                } else {
                    var param = Common.Request.Create({
                        Sorts: [], Filters: [
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.GreaterThanOrEqual, dtFrom),
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.LessThanOrEqual, dtTo)
                        ]
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleTOVEN,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $.each(res, function (i, v) {
                                    v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                    v.DateTo = Common.Date.FromJson(v.DateTo);
                                });
                                _OPSAppointment_DIRoute.Timeline.SetDetailData(res);
                            });
                        }
                    });
                }
            },
            eventClickTime: function (id, item, typeid) {
                Common.Log('VehicleClickTime');
                _OPSAppointment_DIRoute.Data._currentTimeline = item;

                if (!$scope.IsShowVehicleVendor) {
                    if (typeid > 0) {
                        $scope.IsShowWinToBtnMonitor = false;
                        $scope.IsShowWinToBtnRemoveMonitor = false;
                        $scope.IsShowWinToBtnSave = false;
                        $scope.IsShowWinToBtnDel = false;
                        $scope.IsShowWinToBtnAdd = false;

                        switch (typeid) {
                            case 1:
                                $scope.IsShowWinToBtnSave = true;
                                $scope.IsShowWinToBtnMonitor = true;
                                $scope.IsShowWinToBtnDel = true;
                                break;
                            case 2:
                                $scope.IsShowWinToBtnRemoveMonitor = true;
                                break;
                        }

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRoute.URL.VehicleTimeGet,
                            data: { id: id },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    _OPSAppointment_DIRoute.Data._currentOrder = res;
                                    _OPSAppointment_DIRoute.Data._currentOrder.ETD = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETD);
                                    _OPSAppointment_DIRoute.Data._currentOrder.ETA = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETA);

                                    $scope.WinToLoadVehicleInfo(item);
                                    $scope.OPSAppointment_DIRoute_TO_win.center().open();
                                    $scope.WinTO.DriverID = -1;

                                    if (_OPSAppointment_DIRoute.Data._currentOrder.DriverID > 0)
                                        $scope.WinTO.DriverID = _OPSAppointment_DIRoute.Data._currentOrder.DriverID;
                                    $scope.WinTO.DriverTelNo = _OPSAppointment_DIRoute.Data._currentOrder.DriverTelNo;

                                    $.each(res.ListGroupProduct, function (i, v) {
                                        var groupcustom = 'Đơn hàng: ' + v.OrderCode;
                                        var strSOCode = v.SOCode == null ? '' : v.SOCode;
                                        if (v.GroupOfVehicleID > 0)
                                            groupcustom += '&nbsp;&nbsp;&nbsp;Loại xe: ' + v.GroupOfVehicleName;
                                        else
                                            groupcustom += '&nbsp;&nbsp;&nbsp;SO: ' + strSOCode + '&nbsp;&nbsp;&nbsp;KH: ' + v.PartnerName + ', ' + v.ProvinceName + ', ' + v.DistrictName;
                                        v.GroupCustom = groupcustom;

                                        v.TonTotal = v.Ton;
                                        v.CBMTotal = v.CBM;
                                        v.TonExtra = 0;
                                        v.CBMExtra = 0;
                                    });

                                    $scope.OPSAppointment_DIRoute_TO_grid.dataSource.data(_OPSAppointment_DIRoute.Data._currentOrder.ListGroupProduct);

                                    $scope.WinTO.DateFrom = _OPSAppointment_DIRoute.Data._currentOrder.ETD;
                                    $scope.WinTO.DateTo = _OPSAppointment_DIRoute.Data._currentOrder.ETA;
                                    $scope.WinTO.HourFrom = _OPSAppointment_DIRoute.Data._currentOrder.ETD;
                                    $scope.WinTO.HourTo = _OPSAppointment_DIRoute.Data._currentOrder.ETA;

                                    $scope.WinToLoadTotal();
                                });
                            }
                        });
                    }
                    else {
                        switch (typeid) {
                            case -1:
                            case -2:
                                $scope.WinActivityItem(id);
                                break;
                        }
                    }
                } else {
                    _OPSAppointment_DIRoute.Data._winVendorTOID = id;
                    $scope.WinVendorTOLoad();
                }
            },
            eventDrop: function (e, tdtime, item) {
                Common.Log('VehicleDrop');
                
                var tr = $(e.draggable.currentTarget).closest('tr');
                _OPSAppointment_DIRoute.Data._currentOrder = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(tr);
                _OPSAppointment_DIRoute.Data._currentTimeline = item;
                $scope.Vehicle.TDTime = tdtime;

                if (_OPSAppointment_DIRoute.Data._currentTimeline.ID > 0 && !$scope.IsShowVehicleVendor) {
                    $scope.WinToLoadVehicleInfo(item);
                    $scope.OPSAppointment_DIRoute_TO_win.center().open();
                    $scope.WinTO.RegNo = _OPSAppointment_DIRoute.Data._currentTimeline.RegNo;

                    $scope.IsShowWinToBtnAdd = true;
                    $scope.IsShowWinToBtnSave = false;
                    $scope.IsShowWinToBtnRemoveMonitor = false;
                    $scope.IsShowWinToBtnMonitor = false;
                    $scope.IsShowWinToBtnDel = false;
                    
                    if (_OPSAppointment_DIRoute.Data._currentTimeline.DriverID > 0) {
                        $timeout(function () {
                            $scope.WinTO.DriverID = _OPSAppointment_DIRoute.Data._currentTimeline.DriverID;
                            $scope.WinTO.DriverTelNo = _OPSAppointment_DIRoute.Data._currentTimeline.DriverTelNo;
                        }, 10);
                    }
                    else {
                        $timeout(function () {
                            $scope.WinTO.DriverID = -1;
                            $scope.WinTO.DriverTelNo = '';
                        }, 10);
                    }

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleGet,
                        data: { item: _OPSAppointment_DIRoute.Data._currentOrder },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                var data = [];
                                var f = $scope.Vehicle.DateFrom;
                                var t = $scope.Vehicle.DateTo;

                                $scope.WinTO.DateFrom = _OPSAppointment_DIRoute.Data._currentOrder.ETD;
                                $scope.WinTO.DateTo = _OPSAppointment_DIRoute.Data._currentOrder.ETA;
                                $scope.WinTO.HourFrom = _OPSAppointment_DIRoute.Data._currentOrder.ETD;
                                $scope.WinTO.HourTo = Common.Date.AddHour(_OPSAppointment_DIRoute.Data._currentOrder.ETD, 2);

                                $.each(res, function (i, v) {
                                    var groupcustom = 'Đơn hàng: ' + v.OrderCode;
                                    var strSOCode = v.SOCode == null ? '' : v.SOCode;
                                    if (v.GroupOfVehicleID > 0)
                                        groupcustom += '&nbsp;&nbsp;&nbsp;Loại xe: ' + v.GroupOfVehicleName;
                                    else
                                        groupcustom += '&nbsp;&nbsp;&nbsp;SO: ' + strSOCode + '&nbsp;&nbsp;&nbsp;KH: ' + v.PartnerName + ', ' + v.ProvinceName + ', ' + v.DistrictName;
                                    v.GroupCustom = groupcustom;

                                    v.TonTotal = v.Ton;
                                    v.CBMTotal = v.CBM;
                                    v.TonExtra = 0;
                                    v.CBMExtra = 0;
                                });
                                $scope.OPSAppointment_DIRoute_TO_grid.dataSource.data(res);

                                if (Common.HasValue($scope.Vehicle.TDTime)) {
                                    $scope.WinTO.DateFrom = $scope.Vehicle.TDTime.Date;
                                    $scope.WinTO.DateTo = $scope.Vehicle.TDTime.Date;
                                    $scope.WinTO.HourFrom = $scope.Vehicle.TDTime.HourFrom;
                                    $scope.WinTO.HourTo = Common.Date.AddHour($scope.Vehicle.TDTime.HourFrom, 2);
                                }
                                $scope.Vehicle.TDTime = null;

                                $scope.WinToLoadTotal();
                            });
                        }
                    });
                }
                else {
                    var str = item.RegNo + ' - ' + item.VendorName;
                    _OPSAppointment_DIRoute.Data._vehicleid = item.VehicleID;
                    _OPSAppointment_DIRoute.Data._vendorid = item.VendorID;

                    $scope.WinTORate.Title = str;

                    $scope.IsShowWinToRateBtnAdd = true;
                    $scope.IsShowWinToRateBtnSave = false;
                    $scope.IsShowWinToRateBtnRemoveMonitor = false;
                    $scope.IsShowWinToRateBtnMonitor = false;
                    $scope.IsShowWinToRateBtnDel = false;

                    var data = $scope.OPSAppointment_DIRoute_TORateVendor_grid.dataSource.data();
                    $.each(data, function (i, v) {
                        if (i == 0) {
                            v.VendorID = item.VendorID;
                            v.VehicleCode = item.RegNo;
                            v.Debit = 0;
                            v.RegNo = item.RegNo;
                            v.lstVehicle = _OPSAppointment_DIRoute.Data._dataVehicleByVEN[item.VendorID];
                        }
                        else {
                            v.VendorID = -1;
                            v.VehicleCode = '';
                            v.RegNo = '';
                            v.Debit = 0;
                            v.lstVehicle = _OPSAppointment_DIRoute.Data._dataVehicleHome;
                        }
                    });
                    $timeout(function () {
                        data[0].VendorID = item.VendorID;
                        data[0].VehicleCode = item.RegNo;
                        data[0].RegNo = item.RegNo;
                        data[0].Debit = 0;
                        data[0].lstVehicle = _OPSAppointment_DIRoute.Data._dataVehicleByVEN[item.VendorID];
                        
                        $scope.OPSAppointment_DIRoute_TORateVendor_gridOptions.dataSource.data(data);
                        $scope.OPSAppointment_DIRoute_TORateVendor_grid.resize();
                    }, 10);
                    
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleGet,
                        data: { item: _OPSAppointment_DIRoute.Data._currentOrder },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.WinTORate.DateFrom = $scope.Vehicle.DateFrom;
                                $scope.WinTORate.DateTo = $scope.Vehicle.DateTo;
                                $scope.WinTORate.HourFrom = $scope.Vehicle.HourFrom;
                                $scope.WinTORate.HourTo = Common.Date.AddHour($scope.Vehicle.HourFrom, 2);
                                $scope.WinTORate.RateTime = 2;

                                $.each(res, function (i, v) {
                                    var groupcustom = 'Đơn hàng: ' + v.OrderCode;
                                    var strSOCode = v.SOCode == null ? '' : v.SOCode;
                                    if (v.GroupOfVehicleID > 0)
                                        groupcustom += '&nbsp;&nbsp;&nbsp;Loại xe: ' + v.GroupOfVehicleName;
                                    else
                                        groupcustom += '&nbsp;&nbsp;&nbsp;SO: ' + strSOCode + '&nbsp;&nbsp;&nbsp;KH: ' + v.PartnerName + ', ' + v.ProvinceName + ', ' + v.DistrictName;
                                    v.GroupCustom = groupcustom;

                                    v.TonTotal = v.Ton;
                                    v.CBMTotal = v.CBM;
                                    v.TonExtra = 0;
                                    v.CBMExtra = 0;
                                });
                                $scope.OPSAppointment_DIRoute_TORate_grid.dataSource.data(res);
                                $timeout(function () {
                                    $scope.OPSAppointment_DIRoute_TORate_grid.resize();
                                }, 10);
                                if (Common.HasValue($scope.Vehicle.TDTime)) {
                                    $scope.WinTORate.DateFrom = $scope.Vehicle.TDTime.Date;
                                    $scope.WinTORate.DateTo = $scope.Vehicle.TDTime.Date;
                                    $scope.WinTORate.HourFrom = $scope.Vehicle.TDTime.HourFrom;
                                    $scope.WinTORate.HourTo = Common.Date.AddHour($scope.Vehicle.TDTime.HourFrom, 2);
                                    $scope.WinTORate.RateTime = 2;
                                }
                                $scope.Vehicle.TDTime = null;

                                $scope.WinToLoadTotal();
                                
                                $scope.OPSAppointment_DIRoute_TORate_win.center().open();
                            });
                        }
                    });
                }
            },
            eventDropInTimeline: function (e, data) {
                Common.Log("VehicleDropInTimeline");

                var source = { ID: data.id };
                var target = { VehicleID: data.target.ID, ETD: data.tdtime.HourFrom };
                _OPSAppointment_DIRoute.Data._currentTimeline = data.target;

                $scope.IsShowWinToBtnMonitor = false;
                $scope.IsShowWinToBtnRemoveMonitor = false;
                $scope.IsShowWinToBtnSave = false;
                $scope.IsShowWinToBtnDel = false;
                $scope.IsShowWinToBtnAdd = false;

                switch (data.typeid) {
                    case 1:
                        $scope.IsShowWinToBtnSave = true;
                        break;
                }

                if (data.typeid < 3) {
                    if (data.source.ID != data.target.ID) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Confirm,
                            Msg: 'Bạn muốn chuyển từ xe ' + data.source.RegNo + ' sang xe ' + data.target.RegNo + ' ?',
                            Ok: function () {
                                $scope.VehicleTimelineChange(source, target);
                            }
                        });
                    }
                    else
                        $scope.VehicleTimelineChange(source, target);
                }
            },
            eventSelect: function (lst) {
                Common.Log('VehicleSelect');

                _OPSAppointment_DIRoute.Data._vehicleListSelect = lst;
                var lstid = [];
                $.each(lst, function (i, v) {
                    lstid.push(v.ID);
                });

                var pn = $($scope.OPSAppointment_DIRoute_Vehicle_splitter.element.find(".k-pane")[2]).data('pane');
                if (!pn.collapsed) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleDetail,
                        data: { lstid: lstid },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.OPSAppointment_DIRoute_VehicleDetail_grid.dataSource.data(res.ListTOMaster);
                            });
                        }
                    });
                }
            }
        });
        _OPSAppointment_DIRoute.Timeline.Init();
    }, 500);

    //#region Vehicle
    $scope.VehicleConfig_Click = function ($event) {
        Common.Log('VehicleConfig_Click');
        $event.preventDefault();

        $scope.Vehicle.IsShowConfig = !$scope.Vehicle.IsShowConfig;
        // Nếu đóng config thì load lại grid + lưu cookie
        if (!$scope.Vehicle.IsShowConfig) {
            // Load lại data
            _OPSAppointment_DIRoute.Timeline.ChangeTime({
                search: {
                    DateFrom: $scope.Vehicle.DateFrom,
                    DateTo: $scope.Vehicle.DateTo,
                    HourFrom: $scope.Vehicle.HourFrom,
                    HourTo: $scope.Vehicle.HourTo
                 }
            });
            // Set cookie
            Common.Cookie.Set(_OPSAppointment_DIRoute.Data.CookieVehicle, JSON.stringify($scope.Vehicle));
            // Resize
            $scope.OPSAppointment_DIRoute_Vehicle_splitter.size(".k-pane:first", "25px");
        } else {
            $scope.OPSAppointment_DIRoute_Vehicle_splitter.size(".k-pane:first", "70px");
        }
        $scope.OPSAppointment_DIRoute_Vehicle_LoadLabel();
    };

    $scope.VehiclePOD_Click = function ($event) {
        Common.Log('VehiclePOD_Click');
        $event.preventDefault();

        $scope.WinPODLoad();
    };

    $scope.VehicleQuickSearch_Click = function ($event) {
        Common.Log('VehicleQuickSearch_Click');
        $event.preventDefault();

        $scope.WinQuickSearchLoad();
    };

    $scope.WinToLoadVehicleInfo = function (item) {
        $scope.WinTO.RegNo = item.RegNo;
        $scope.WinTO.VehicleTon = item.MaxWeightCal;
        $scope.WinTO.VehicleCBM = item.MaxCapacity;
    };

    $scope.WinToLoadTotal = function () {
        Common.Log('WinToLoadTotal');

        var data = $scope.OPSAppointment_DIRoute_TO_grid.dataSource.data();
        var ton = 0;
        var cbm = 0;
        $.each(data, function (i, v) {
            ton += v.Ton;
            cbm += v.CBM;
        });

        $scope.WinTO.OrderTon = Common.Number.ToNumber3(ton);
        $scope.WinTO.OrderCBM = Common.Number.ToNumber3(cbm);

        var str = '';
        if (_OPSAppointment_DIRoute.Data._currentTimeline.MaxWeightCal > 0) {
            if (ton > _OPSAppointment_DIRoute.Data._currentTimeline.MaxWeightCal)
                str = 'quá trọng tải của xe';
        }
        if (str == '' && _OPSAppointment_DIRoute.Data._currentTimeline.MaxCapacity > 0) {
            if (cbm > _OPSAppointment_DIRoute.Data._currentTimeline.MaxCapacity)
                str = 'quá khối của xe';
        }
        $scope.WinTO.VehicleNote = str;
    };

    $scope.WinVendorTOLoad = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.VehicleTOVENInDate,
            data: { id: _OPSAppointment_DIRoute.Data._winVendorTOID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    if (res.length > 0) {
                        $scope.OPSAppointment_DIRoute_VendorTO_grid.dataSource.data(res);
                        $scope.OPSAppointment_DIRoute_VendorTO_win.center().open();
                        $timeout(function () {
                            $scope.OPSAppointment_DIRoute_VendorTO_grid.resize();
                        }, 100);
                    }
                    else
                        $scope.OPSAppointment_DIRoute_VendorTO_win.close();
                });
            }
        });
    };

    $scope.VehicleTimelineChange = function (source, target) {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.VehicleTimelineChange,
            data: { source: source, target: target },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    if (Common.HasValue(res) && !res.IsOrigin) {
                        _OPSAppointment_DIRoute.Data._currentOrder = res;

                        $scope.WinToLoadVehicleInfo(_OPSAppointment_DIRoute.Data._currentTimeline);
                        $scope.OPSAppointment_DIRoute_TO_win.open();

                        $.each(res.ListGroupProduct, function (i, v) {
                            var groupcustom = 'Đơn hàng: ' + v.OrderCode;
                            if (v.GroupOfVehicleID > 0)
                                groupcustom += '&nbsp;&nbsp;&nbsp;Loại xe: ' + v.GroupOfVehicleName;
                            v.GroupCustom = groupcustom;
                        });
                        $scope.OPSAppointment_DIRoute_TO_grid.dataSource.data(_OPSAppointment_DIRoute.Data._currentOrder.ListGroupProduct);

                        $scope.WinTO.DateFrom = Common.Date.Date(Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETD));
                        $scope.WinTO.DateTo = Common.Date.Date(Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETA));

                        $scope.WinTO.HourFrom = Common.Date.Date(Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETD));
                        $scope.WinTO.HourTo = Common.Date.Date(Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETA));

                        $scope.WinToLoadTotal();
                    } else {
                        _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        });
    };

    $scope.ExcelConfirm_Click = function ($event) {
        Common.Log('ExcelConfirm_Click');
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.VehicleExcelConfirm,
            data: { dtfrom: $scope.Vehicle.DateFrom, dtto: $scope.Vehicle.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    
                    $rootScope.DownloadFile(res);
                });
            }
        });
    };

    $scope.CreateCustom_Click = function ($event) {
        Common.Log("CreateCustom_Click");
        $event.preventDefault();

        $state.go("main.OPSAppointment.DIRouteMasterCustom");
    };

    // Win Search
    $scope.WinQuickSearchLoad = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.QuickSearch,
            data: { dtfrom: $scope.Vehicle.DateFrom, dtto: $scope.Vehicle.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $.each(res, function (i, v) {
                        if (Common.HasValue(v.RequestDate)) {
                            v.RequestDate = Common.Date.FromJson(v.RequestDate);
                            v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                        }
                    });

                    $scope.OPSAppointment_DIRoute_QuickSearch_grid.dataSource.data(res);
                    $timeout(function () {
                        $scope.OPSAppointment_DIRoute_QuickSearch_grid.resize();
                    }, 10);
                    $scope.OPSAppointment_DIRoute_QuickSearch_win.center().open();
                });
            }
        });

    };

    $scope.WinQuickSearchLoadItem = function (item) {
        if (item.TypeID > 0) {
            $scope.IsShowWinToBtnAdd = false;
            $scope.IsShowWinToBtnSave = false;
            $scope.IsShowWinToBtnRemoveMonitor = false;
            $scope.IsShowWinToBtnMonitor = false;
            $scope.IsShowWinToBtnDel = false;

            switch (item.TypeID) {
                case 1:
                    $scope.IsShowWinToBtnDel = true;
                    $scope.IsShowWinToBtnSave = true;
                    $scope.IsShowWinToBtnMonitor = true;
                    break;
                case 2:
                    $scope.IsShowWinToBtnRemoveMonitor = true;
                    break;
            }

            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRoute.URL.VehicleTimeGet,
                data: { id: item.TOMasterID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRoute.Data._currentOrder = res;
                        _OPSAppointment_DIRoute.Data._currentOrder.ETD = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETD);
                        _OPSAppointment_DIRoute.Data._currentOrder.ETA = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETA);
                        //me._lblWinToTitle.html('Lệnh vận chuyển: ' + me._currentOrder.TOMasterCode);
                        
                        $scope.WinToLoadVehicleInfo(item);
                        $scope.OPSAppointment_DIRoute_TO_win.center().open();

                        if (_OPSAppointment_DIRoute.Data._currentOrder.DriverID > 0)
                            $scope.WinTO.DriverID = _OPSAppointment_DIRoute.Data._currentOrder.DriverID;
                        $scope.WinTO.DriverTelNo = _OPSAppointment_DIRoute.Data._currentOrder.DriverTelNo;

                        $.each(res.ListGroupProduct, function (i, v) {
                            var groupcustom = 'Đơn hàng: ' + v.OrderCode;
                            var strSOCode = v.SOCode == null ? '' : v.SOCode;
                            if (v.GroupOfVehicleID > 0)
                                groupcustom += '&nbsp;&nbsp;&nbsp;Loại xe: ' + v.GroupOfVehicleName;
                            else
                                groupcustom += '&nbsp;&nbsp;&nbsp;SO: ' + strSOCode + '&nbsp;&nbsp;&nbsp;KH: ' + v.PartnerName + ', ' + v.ProvinceName + ', ' + v.DistrictName;
                            v.GroupCustom = groupcustom;

                            v.TonTotal = v.Ton;
                            v.CBMTotal = v.CBM;
                            v.TonExtra = 0;
                            v.CBMExtra = 0;
                        });

                        $scope.OPSAppointment_DIRoute_TO_grid.dataSource.data(_OPSAppointment_DIRoute.Data._currentOrder.ListGroupProduct);

                        $scope.WinTO.DateFrom = _OPSAppointment_DIRoute.Data._currentOrder.ETD;
                        $scope.WinTO.DateTo = _OPSAppointment_DIRoute.Data._currentOrder.ETA;
                        $scope.WinTO.HourFrom = _OPSAppointment_DIRoute.Data._currentOrder.ETD;
                        $scope.WinTO.HourTo = _OPSAppointment_DIRoute.Data._currentOrder.ETA;

                        $scope.WinToLoadTotal();
                    });
                }
            });
        }
        else {
            $scope.WinToRateLoad(item.TOMasterID, -item.TypeID, item.VehicleID);
        }
    };

    $scope.QuickSearchApproved_Click = function ($event) {
        Common.Log('QuickSearchApproved_Click');
        $event.preventDefault();

        var data = $scope.OPSAppointment_DIRoute_QuickSearch_grid.dataSource.data();
        var lstid = [];
        $.each(data, function (i, v) {
            if (lstid.indexOf(v.TOMasterID) < 0 && v.IsChoose == true)
                lstid.push(v.TOMasterID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn hủy duyệt các dòng đã chọn ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.QuickSearchApproved,
                        data: { lstid: lstid },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.WinQuickSearchLoad();
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.QuickSearchUnApproved_Click = function ($event) {
        Common.Log('QuickSearchUnApproved_Click');
        $event.preventDefault();

        var data = $scope.OPSAppointment_DIRoute_QuickSearch_grid.dataSource.data();
        var lstid = [];
        $.each(data, function (i, v) {
            if (lstid.indexOf(v.TOMasterID) < 0 && v.IsChoose == true)
                lstid.push(v.TOMasterID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn hủy duyệt các dòng đã chọn ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.QuickSearchUnApproved,
                        data: { lstid: lstid },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.WinQuickSearchLoad();
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.QuickSearchClose_Click = function ($event, win) {
        Common.Log('QuickSearchClose_Click');
        $event.preventDefault();

        win.close();
    };

    // Win POD
    $scope.WinPODLoad = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.PODList,
            data: { dtfrom: $scope.Vehicle.DateFrom, dtto: $scope.Vehicle.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $.each(res, function (i, v) {
                        if (Common.HasValue(v.RequestDate)) {
                            v.RequestDate = Common.Date.FromJson(v.RequestDate);
                            v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                        }
                    });
                    $scope.OPSAppointment_DIRoute_POD_grid.dataSource.data(res);
                    $timeout(function () {
                        $scope.OPSAppointment_DIRoute_POD_grid.resize();
                    }, 10);
                    $scope.OPSAppointment_DIRoute_POD_win.center().open();
                });
            }
        });
    };

    $scope.WinPODDivLoad = function () {
        var dncode = _OPSAppointment_DIRoute.Data._winPODItem.DNCode;
        if (dncode == null)
            dncode = '';
        $timeout(function () {
            $scope.WinPODDiv.Title = 'SO:' + _OPSAppointment_DIRoute.Data._winPODItem.SOCode + '&nbsp;&nbsp;&nbsp;&nbsp;DN:' + dncode + '&nbsp;&nbsp;&nbsp;&nbsp;KG:' + Common.Number.ToNumber1(_OPSAppointment_DIRoute.Data._winPODItem.Ton);

            $scope.WinPODDiv.DN1 = dncode;
            $scope.WinPODDiv.KG1 = _OPSAppointment_DIRoute.Data._winPODItem.Ton;
            $scope.WinPODDiv.DN2 = '';
            $scope.WinPODDiv.KG2 = 0;
        }, 10);
    };

    $scope.PODUpload_Click = function ($event) {
        Common.Log("PODUpload_Click");
        $event.preventDefault();

        $rootScope.UploadExcel({
            width: '900px',
            height: '500px',
            columns: [
                { field: 'ExcelSuccess', width: '50px', title: ' ', template: '<input type="checkbox" #= ExcelSuccess ? checked="checked" : "" #/>', sortable: true, filterable: false},
                { field: 'ExcelError', width: '50px', title: 'Lỗi', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'VehicleCode', width: '80px', title: 'Xe', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'OrderCode', width: '100px', title: '{{RS.ORDOrder.Code}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'SOCode', width: '100px', title: 'Order No', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'RouteCode', width: '100px', title: 'Route Description', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'DNCode', width: '100px', title: 'Số DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                { title: ' ', filterable: false, sortable: false }
            ],
            Download: function ($event) {
                $rootScope.IsLoading = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIRoute.URL.PODExcelDownload,
                    data: { dtfrom: $scope.Vehicle.DateFrom, dtto: $scope.Vehicle.DateTo },
                    success: function (res) {
                        $rootScope.IsLoading = false;

                        $rootScope.DownloadFile(res);
                    }
                });
            },
            Upload: function (file, success) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIRoute.URL.PODExcelCheck,
                    data: { file: file, dtfrom: $scope.Vehicle.DateFrom, dtto: $scope.Vehicle.DateTo },
                    success: function (res) {
                        success(res);
                    }
                });
            },
            Complete: function (file, data) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIRoute.URL.PODExcelSave,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.WinPODLoad();
                    }
                });
            }
        });
    };

    $scope.PODSave_Click = function ($event) {
        Common.Log("PODSave_Click");
        $event.preventDefault();

        var data = $.extend(true, [], $scope.OPSAppointment_DIRoute_POD_grid.dataSource.data());
        var lst = [];
        $.each(data, function (i, v) {
            if (v.ExcelSuccess) {
                v.PartnerCode = v.PartnerName = v.Address = v.RoutingName = v.DriverName = '';
                v.ETARequest = null;
                lst.push(v);
            }
        });

        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRoute.URL.PODExcelSave,
                data: { lst: lst },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        if (res == '') {
                            $scope.WinPODLoad();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                        else
                            $rootScope.Message({ Msg: res, NotifyType: Common.Message.NotifyType.ERROR });
                    });
                }
            });
        }
    };

    $scope.PODClose_Click = function ($event, win) {
        Common.Log("PODClose_Click");
        $event.preventDefault();

        win.close();
    };

    $scope.PODDivSave_Click = function ($event, win) {
        Common.Log("PODDivSave_Click");
        $event.preventDefault();
        var DN1 = $scope.WinPODDiv.DN1;
        var DN2 = $scope.WinPODDiv.DN2;
        var KG1 = $scope.WinPODDiv.KG1;
        var KG2 = $scope.WinPODDiv.KG2;

        if (KG1 > 0) {
            if ((DN1 + '').trim() != '') {
                if (KG2 > 0) {
                    if ((DN1 + '').trim() != '' && (DN1 + '').trim() != (DN2 + '').trim()) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Confirm,
                            Msg: 'Bạn muốn chia DN này ?',
                            Ok: function () {
                                var lst = [];
                                lst.push({ 'ID': _OPSAppointment_DIRoute.Data._winPODItem.ID, 'DNCode': (DN1 + '').trim(), 'Ton': KG1 });
                                lst.push({ 'ID': _OPSAppointment_DIRoute.Data._winPODItem.ID, 'DNCode': (DN2 + '').trim(), 'Ton': KG2 });

                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIRoute.URL.PODDiv,
                                    data: { lst: lst },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $scope.WinPODLoad();
                                            win.close();
                                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                        });
                                    }
                                });
                            }
                        });
                    }
                    else
                        $rootScope.Message({ Msg: 'Chưa nhập số DN2 hoặc DN1 phải khác DN2', NotifyType: Common.Message.NotifyType.ERROR });
                }
                else
                    win.close();
            }
            else
                $rootScope.Message({ Msg: 'Chưa nhập số DN1', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else
            $rootScope.Message({ Msg: 'KG của DN1 phải lớn 0', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.PODDivClose_Click = function ($event, win) {
        Common.Log("PODDivClose_Click");
        $event.preventDefault();

        win.close();
    };

    // Win TO
    $scope.WinToMonitor_Click = function ($event) {
        Common.Log("WinToMonitor_Click");
        $event.preventDefault();

        if (Common.HasValue(_OPSAppointment_DIRoute.Data._currentOrder)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn duyệt chuyến này ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleMonitor,
                        data: { id: _OPSAppointment_DIRoute.Data._currentOrder.TOMasterID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();

                                $scope.OPSAppointment_DIRoute_TO_win.close();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.WinToRemoveMonitor_Click = function ($event) {
        Common.Log("WinToRemoveMonitor_Click");
        $event.preventDefault();

        if (Common.HasValue(_OPSAppointment_DIRoute.Data._currentOrder)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn hủy duyệt ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleRemoveMonitor,
                        data: { id: _OPSAppointment_DIRoute.Data._currentOrder.TOMasterID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();

                                $scope.OPSAppointment_DIRoute_TO_win.close();
                                if (res == "")
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                else
                                    $rootScope.Message({ Msg: res, NotifyType: Common.Message.NotifyType.ERROR });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.WinToSave_Click = function ($event) {
        Common.Log("WinToSave_Click");
        $event.preventDefault();

        var item = _OPSAppointment_DIRoute.Data._currentOrder;
        item.VehicleID = _OPSAppointment_DIRoute.Data._currentTimeline.ID;
        item.DriverID = $scope.WinTO.DriverID;
        item.DriverName = $scope.WinTO.DriverName;
        item.DriverTelNo = $scope.WinTO.DriverTelNo;

        var fdate = $scope.WinTO.DateFrom;
        var fhour = $scope.WinTO.HourFrom;
        var etd = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());

        fdate = $scope.WinTO.DateTo;
        fhour = $scope.WinTO.HourTo;
        var eta = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());
        item.ETD = etd;
        item.ETA = eta;

        var flag = false;
        var data = $scope.OPSAppointment_DIRoute_TO_grid.dataSource.data();
        $.each(data, function (ig, vg) {
            switch (vg.TypeEditID) {
                case 1:
                    vg.CBM = vg.Ton * vg.ExchangeCBM;
                    break;
                case 2:
                    vg.Ton = vg.CBM * vg.ExchangeTon;
                    break;
                case 3:
                    vg.Ton = vg.Quantity * vg.ExchangeTon;
                    vg.CBM = vg.Quantity * vg.ExchangeCBM;
                    break;
            }
            if (vg.Ton > 0 || vg.CBM > 0 || vg.IsFTL) {
                flag = true;
            }
        });
        item.ListGroupProduct = data;

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.VehicleSave,
            data: { item: item },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                    $scope.OPSAppointment_DIRoute_Order_LoadData();

                    $scope.OPSAppointment_DIRoute_TO_win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                });
            }
        });
    };

    $scope.WinToDel_Click = function ($event) {
        Common.Log("WinToDel_Click");
        $event.preventDefault();

        if (Common.HasValue(_OPSAppointment_DIRoute.Data._currentOrder)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa chuyến này ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleRemove,
                        data: { id: _OPSAppointment_DIRoute.Data._currentOrder.TOMasterID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                                $scope.OPSAppointment_DIRoute_Order_LoadData();
                                $scope.OPSAppointment_DIRoute_TO_win.close();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.WinToAdd_Click = function ($event) {
        Common.Log("WinToAdd_Click");
        $event.preventDefault();

        var item = _OPSAppointment_DIRoute.Data._currentOrder;
        item.VehicleID = _OPSAppointment_DIRoute.Data._currentTimeline.ID;
        item.DriverID = $scope.WinTO.DriverID;
        item.DriverName = $scope.WinTO.DriverName;
        item.DriverTelNo = $scope.WinTO.DriverTelNo;

        var fdate = $scope.WinTO.DateFrom;
        var fhour = $scope.WinTO.HourFrom;
        var etd = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());

        fdate = $scope.WinTO.DateTo;
        fhour = $scope.WinTO.HourTo;
        var eta = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());

        item.ETD = etd;
        item.ETA = eta;

        var flag = false;
        var data = $scope.OPSAppointment_DIRoute_TO_grid.dataSource.data();
        var lstGroupProduct = [];
        $.each(data, function (ig, vg) {
            switch (vg.TypeEditID) {
                case 1:
                    vg.CBM = vg.Ton * vg.ExchangeCBM;
                    break;
                case 2:
                    vg.Ton = vg.CBM * vg.ExchangeTon;
                    break;
                case 3:
                    vg.Ton = vg.Quantity * vg.ExchangeTon;
                    vg.CBM = vg.Quantity * vg.ExchangeCBM;
                    break;
            }
            if (vg.Ton > 0 || vg.CBM > 0 || vg.IsFTL) {
                flag = true;
                lstGroupProduct.push(vg);
            }
        });
        item.ListGroupProduct = lstGroupProduct;

        if (data.length > 0 && flag) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRoute.URL.VehicleAdd,
                data: { item: item },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                        $scope.OPSAppointment_DIRoute_Order_LoadData();

                        $scope.OPSAppointment_DIRoute_TO_win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    });
                }
            });
        }
        else
            $scope.OPSAppointment_DIRoute_TO_win.close();
    };

    $scope.WinToClose_Click = function ($event, win) {
        Common.Log("WinToClose_Click");
        $event.preventDefault();
        
        win.close();
    };

    $scope.WinVendorToClose_Click = function ($event, win) {
        Common.Log("WinVendorToClose_Click");
        $event.preventDefault();

        win.close();
    };

    // Win TO Rate
    $scope.InitWinToRate_LoadComplete = function () {
        if (Common.HasValue(_OPSAppointment_DIRoute.Data._dataVendor) && Common.HasValue(_OPSAppointment_DIRoute.Data._dataVehicleVEN) && Common.HasValue(_OPSAppointment_DIRoute.Data._dataVehicleByVEN)) {
            Common.Log('InitWinToRate_LoadComplete');
        }
    };

    $scope.WinToRateLoad = function (id, typeid, vehicleid) {
        $scope.IsShowWinToRateBtnAdd = false;
        $scope.IsShowWinToRateBtnSave = false;
        $scope.IsShowWinToRateBtnRemoveMonitor = false;
        $scope.IsShowWinToRateBtnMonitor = false;
        $scope.IsShowWinToRateBtnDel = false;

        switch (typeid) {
            case 1:
                if (vehicleid > 0)
                    $scope.IsShowWinToRateBtnMonitor = true;
                $scope.IsShowWinToRateBtnDel = true;
                break;
            case 2:
                $scope.IsShowWinToRateBtnRemoveMonitor = true;
                break;
        }
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.VehicleTOVENGet,
            data: { id: id },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _OPSAppointment_DIRoute.Data._currentOrder = res;
                    var str = res.VendorOfVehicleName;
                    $scope.WinTORate.Title = str;
                    $scope.OPSAppointment_DIRoute_TORate_win.center().open();

                    $.each(_OPSAppointment_DIRoute.Data._currentOrder.ListGroupProduct, function (i, v) {
                        var groupcustom = 'Đơn hàng: ' + v.OrderCode;
                        var strSOCode = v.SOCode == null ? '' : v.SOCode;
                        if (v.GroupOfVehicleID > 0)
                            groupcustom += '&nbsp;&nbsp;&nbsp;Loại xe: ' + v.GroupOfVehicleName;
                        else
                            groupcustom += '&nbsp;&nbsp;&nbsp;SO: ' + strSOCode + '&nbsp;&nbsp;&nbsp;KH: ' + v.PartnerName + ', ' + v.ProvinceName + ', ' + v.DistrictName;
                        v.GroupCustom = groupcustom;

                        v.TonTotal = v.Ton;
                        v.CBMTotal = v.CBM;
                        v.TonExtra = 0;
                        v.CBMExtra = 0;
                    });

                    $scope.OPSAppointment_DIRoute_TORate_grid.dataSource.data(_OPSAppointment_DIRoute.Data._currentOrder.ListGroupProduct);
                    $timeout(function () {
                        $scope.OPSAppointment_DIRoute_TORate_grid.resize();
                    }, 10);

                    $scope.OPSAppointment_DIRoute_TORateVendor_grid.dataSource.data([]);

                    $.each(_OPSAppointment_DIRoute.Data._currentOrder.ListRate, function (i, v) {
                        v.ListVendor = _OPSAppointment_DIRoute.Data._dataVendor;
                        if (v.VendorID == null) {
                            v.lstVehicle = _OPSAppointment_DIRoute.Data._dataVehicleHome;
                        } else {
                            v.lstVehicle = _OPSAppointment_DIRoute.Data._dataVehicleByVEN[v.VendorID];
                        }
                    });
                    $timeout(function () {
                        $scope.OPSAppointment_DIRoute_TORateVendor_grid.dataSource.data(_OPSAppointment_DIRoute.Data._currentOrder.ListRate);
                    }, 10);
                    $scope.WinTORate.DriverName = _OPSAppointment_DIRoute.Data._currentOrder.DriverName;
                    $scope.WinTORate.DriverTelNo = _OPSAppointment_DIRoute.Data._currentOrder.DriverTelNo;
                    $scope.WinTORate.RateTime = _OPSAppointment_DIRoute.Data._currentOrder.RateTime;
                    $scope.WinTORate.TransportModeID = _OPSAppointment_DIRoute.Data._currentOrder.TransportModeID + '';
                    $scope.WinTORate.GroupVehicleID = _OPSAppointment_DIRoute.Data._currentOrder.GroupOfVehicle;
                    $scope.WinTORate.DateFrom = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETD);
                    $scope.WinTORate.DateTo = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETA);
                    $scope.WinTORate.HourFrom = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETD);
                    $scope.WinTORate.HourTo = Common.Date.FromJson(_OPSAppointment_DIRoute.Data._currentOrder.ETA);
                });
            }
        });
    };

    $scope.WinRateLoadTotal = function () {
        Common.Log('WinRateLoadTotal');

        var data = $scope.OPSAppointment_DIRoute_TORate_grid.dataSource.data();
        var ton = 0;
        var cbm = 0;
        $.each(data, function (i, v) {
            ton += v.Ton;
            cbm += v.CBM;
        });
        $("#WinToRateGridTon").html(Common.Number.ToNumber3(ton));
        $("#WinToRateGridCBM").html(Common.Number.ToNumber3(cbm));
    };

    $scope.WinToRateMonitor_Click = function ($event) {
        Common.Log("WinToRateMonitor_Click");
        $event.preventDefault();

        if (Common.HasValue(_OPSAppointment_DIRoute.Data._currentOrder)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn duyệt chuyến này ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleMonitor,
                        data: { id: _OPSAppointment_DIRoute.Data._currentOrder.TOMasterID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                                $scope.WinVendorTOLoad();

                                $scope.OPSAppointment_DIRoute_TORate_win.close();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.WinToRateRemoveMonitor_Click = function ($event) {
        Common.Log("WinToRateRemoveMonitor_Click");
        $event.preventDefault();

        if (Common.HasValue(_OPSAppointment_DIRoute.Data._currentOrder)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn hủy duyệt ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleRemoveMonitor,
                        data: { id: _OPSAppointment_DIRoute.Data._currentOrder.TOMasterID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                                $scope.WinVendorTOLoad();

                                $scope.OPSAppointment_DIRoute_TORate_win.close();
                                if (res == "")
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                else
                                    $rootScope.Message({ Msg: res, NotifyType: Common.Message.NotifyType.ERROR });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.WinToRateSave_Click = function ($event) {
        Common.Log("WinToRateSave_Click");
        $event.preventDefault();

        //var lst = [];
        //var vehicleid = -1;
        ////var etd = new Date(Common.Date.ToString(me._txtWinToDateFrom.value(), Common.Date.Format.YYMMDD) + ' ' + Common.Date.ToString(me._txtWinToHourFrom.value(), Common.Date.Format.HM));
        ////var eta = new Date(Common.Date.ToString(me._txtWinToDateTo.value(), Common.Date.Format.YYMMDD) + ' ' + Common.Date.ToString(me._txtWinToHourTo.value(), Common.Date.Format.HM));

        //var fdate = me._txtWinToDateFrom.value();
        //var fhour = me._txtWinToHourFrom.value();
        //var etd = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());
        //fdate = me._txtWinToDateTo.value();
        //fhour = me._txtWinToHourTo.value();
        //var eta = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());

        //me._currentOrder.ETD = etd;
        //me._currentOrder.ETA = eta;

        //var data = me._winToGrid.dataSource.data();
        //$.each(me._currentOrder.ListGroupProduct, function (ig, vg) {
        //    switch (vg.TypeEditID) {
        //        case 1:
        //            vg.CBM = vg.Ton * vg.ExchangeCBM;
        //            break;
        //        case 2:
        //            vg.Ton = vg.CBM * vg.ExchangeTon;
        //            break;
        //        case 3:
        //            vg.Ton = vg.Quantity * vg.ExchangeTon;
        //            vg.CBM = vg.Quantity * vg.ExchangeCBM;
        //            break;
        //    }
        //});
        ////me._currentTimeline.ListGroupProduct = data;
        //Common.Services.Call(me._urlSRVehicleSave, Common.Services.Type.POST, { 'item': me._currentOrder }, function (res) {
        //    Common.Services.Error(res, function (res) {
        //        var me = Common.View;

        //        me._gridSRVehicle.RefreshDetail();
        //        me.OrderLoad();

        //        me._winTo.close();
        //        Common.Message.Show('Đã cập nhật');
        //    });
        //});
    };

    $scope.WinToRateDel_Click = function ($event) {
        Common.Log("WinToRateDel_Click");
        $event.preventDefault();

        if (Common.HasValue(_OPSAppointment_DIRoute.Data._currentOrder)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa chuyến này ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleRemove,
                        data: { id: _OPSAppointment_DIRoute.Data._currentOrder.TOMasterID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                                $scope.WinVendorTOLoad();
                                $scope.OPSAppointment_DIRoute_Order_LoadData();

                                $scope.OPSAppointment_DIRoute_TORate_win.close();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.WinToRateAdd_Click = function ($event) {
        Common.Log("WinToRateAdd_Click");
        $event.preventDefault();
        
        var fdate = $scope.WinTORate.DateFrom;
        var fhour = $scope.WinTORate.HourFrom;
        var etd = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());

        fdate = $scope.WinTORate.DateTo;
        fhour = $scope.WinTORate.HourTo;
        var eta = new Date(fdate.getFullYear(), fdate.getMonth(), fdate.getDate(), fhour.getHours(), fhour.getMinutes());
        
        var item = _OPSAppointment_DIRoute.Data._currentOrder;
        item.VehicleID = _OPSAppointment_DIRoute.Data._currentTimeline.ID;
        item.ETD = etd;
        item.ETA = eta;
        item.DriverName = $scope.WinTORate.DriverName;
        item.DriverTelNo = $scope.WinTORate.DriverTelNo;
        item.TransportModeID = $scope.WinTORate.TransportID;
        item.GroupOfVehicleID = $scope.WinTORate.GroupVehicleID;
        item.RateTime = $scope.WinTORate.RateTime;

        var flagData = false;
        var data = $scope.OPSAppointment_DIRoute_TORate_grid.dataSource.data();
        $.each(data, function (ig, vg) {
            switch (vg.TypeEditID) {
                case 1:
                    vg.CBM = vg.Ton * vg.ExchangeCBM;
                    break;
                case 2:
                    vg.Ton = vg.CBM * vg.ExchangeTon;
                    break;
                case 3:
                    vg.Ton = vg.Quantity * vg.ExchangeTon;
                    vg.CBM = vg.Quantity * vg.ExchangeCBM;
                    break;
            }
            if (vg.Ton > 0 || vg.IsFTL)
                flagData = true;
        });
        item.ListGroupProduct = data;

        var flagVEN = false;
        var ven = [];
        $($scope.OPSAppointment_DIRoute_TORateVendor_grid.element).find('tbody tr').each(function () {
            var cboVendor = $($(this).find('input.cboVendor.cus-combobox')[1]).data('kendoComboBox');
            var chkIsManual = $($(this).find('.chkIsManual')[0]);
            var txtDebit = $($(this).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
            var cboVehicle = $($(this).find('input.cboVehicle.cus-combobox')[1]).data('kendoComboBox');
            
            var item = $scope.OPSAppointment_DIRoute_TORateVendor_grid.dataItem(this);
            item.VendorID = cboVendor.value();
            item.IsManual = chkIsManual.prop('checked');
            item.Debit = txtDebit.value();
            item.VehicleCode = cboVehicle.text();

            if (item.VendorID > 0) {
                ven.push(item);
                flagVEN = true;
            }
        });
        item.ListRate = ven;

        if (data.length > 0 && flagData && flagVEN) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRoute.URL.VehicleAddRate,
                data: { item: item },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRoute.Timeline.RefreshDetail();
                        $scope.OPSAppointment_DIRoute_Order_LoadData();

                        $scope.OPSAppointment_DIRoute_TORate_win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    });
                }
            });
        }
        else {
            if (!flagVEN)
                $rootScope.Message({ Msg: 'Chưa chọn nhà vận tải', NotifyType: Common.Message.NotifyType.SUCCESS });
            else
                $scope.OPSAppointment_DIRoute_TORate_win.close();
        }

        _OPSAppointment_DIRoute.Data._vendorid = -1;
    };

    $scope.WinToRateClose_Click = function ($event, win) {
        Common.Log("WinToRateClose_Click");
        $event.preventDefault();

        win.close();
    };

    // Win Vehicle Limit
    $scope.WinVehicleOpen = function () {
        $scope.OPSAppointment_DIRoute_VehicleLimit_grid.dataSource.data(_OPSAppointment_DIRoute.Data._dataVehicle);
        $scope.OPSAppointment_DIRoute_VehicleLimit_win.center().open();
        $timeout(function () {
            $scope.OPSAppointment_DIRoute_VehicleLimit_grid.resize();
        }, 10);
    };

    $scope.VehicleLimitSave_Click = function ($event, win) {
        Common.Log("VehicleLimitSave_Click");
        $event.preventDefault();

        var data = [];
        $($scope.OPSAppointment_DIRoute_VehicleLimit_grid.element).find('tbody > tr[role="row"]').each(function () {
            var item = $scope.OPSAppointment_DIRoute_VehicleLimit_grid.dataItem(this);

            var txtMaxWeight = ($(this).find('input.txtMaxWeight.cus-number').length > 0) ?
                $($(this).find('input.txtMaxWeight.cus-number')[1]).data('kendoNumericTextBox') : null;
            var txtMaxCapacity = ($(this).find('input.txtMaxCapacity.cus-number').length > 0) ?
                $($(this).find('input.txtMaxCapacity.cus-number')[1]).data('kendoNumericTextBox') : null;

            item.MaxWeight = txtMaxWeight.value();
            item.MaxCapacity = txtMaxCapacity.value();

            data.push(item);
        });

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRoute.URL.WinVehicleSave,
                data: { lst: data },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRoute.Timeline.RefreshMain();

                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    });
                }
            });
        }
        win.close();
    };

    $scope.VehicleLimitClose_Click = function ($event, win) {
        Common.Log("VehicleLimitClose_Click");
        $event.preventDefault();

        win.close();
    };

    // Win Activity
    $scope.WinActivityItem = function (id) {
        Common.Log('WinActivityItem');

        $scope.IsShowWinActivityDel = true;
        $scope.OPSAppointment_DIRoute_Activity_win_cbbActivityType.enable(false);
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.ActivityGet,
            data: { id: id },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _OPSAppointment_DIRoute.Data._id = res.ID;

                    $timeout(function () {
                        $scope.WinActivity.Title = 'Lịch trình của xe: ' + res.VehicleCode;

                        $scope.WinActivity.TypeOfActivityID = res.TypeOfActivityID;
                        $scope.WinActivity.DateFrom = res.DateFrom;
                        $scope.WinActivity.DateTo = res.DateTo;
                        $scope.WinActivity.Note = res.Note;
                    }, 10);

                    $scope.OPSAppointment_DIRoute_Activity_win.center().open();
                });
            }
        });
    };

    $scope.WinActivityOpen = function () {
        Common.Log('WinActivityOpen');
        if (_OPSAppointment_DIRoute.Data._vehicleListSelect.length > 0) {
            $scope.OPSAppointment_DIRoute_Activity_win.center().open();
            $timeout(function () {
                $scope.IsShowWinActivityDel = false;
                $scope.OPSAppointment_DIRoute_Activity_win_cbbActivityType.enable(true);

                var item = _OPSAppointment_DIRoute.Data._vehicleListSelect[0];
                $scope.WinActivity.Title = 'Lịch trình của xe: ' + item.RegNo;

                $scope.WinActivity.DateFrom = new Date();
                $scope.WinActivity.DateTo = Common.Date.AddHour(new Date(), 3);
            }, 10);

            _OPSAppointment_DIRoute.Data._id = -1;
        }
        else
            $rootScope.Message({ Msg: 'Chưa chọn xe', NotifyType: Common.Message.NotifyType.ERROR });
    }

    $scope.WinActivitySave_Click = function ($event) {
        Common.Log("WinActivitySave_Click");
        $event.preventDefault();

        var item = {
            ID: _OPSAppointment_DIRoute.Data._id,
            VehicleID: _OPSAppointment_DIRoute.Data._id < 1 ? _OPSAppointment_DIRoute.Data._vehicleListSelect[0].ID : -1,
            TypeOfActivityID: $scope.WinActivity.TypeOfActivityID,
            DateFrom: $scope.WinActivity.DateFrom,
            DateTo: $scope.WinActivity.DateTo,
            Note: $scope.WinActivity.Note
        };

        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRoute.URL.ActivitySave,
                data: { item: item },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRoute.Timeline.RefreshMain();

                        $scope.OPSAppointment_DIRoute_Activity_win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    });
                }
            });
        }
        else
            $scope.OPSAppointment_DIRoute_Activity_win.close();
    };

    $scope.WinActivityDel_Click = function ($event) {
        Common.Log("WinActivityDel_Click");
        $event.preventDefault();

        if (_OPSAppointment_DIRoute.Data._id > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa lịch trình này ?',
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.ActivityDel,
                        data: { id: _OPSAppointment_DIRoute.Data._id },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRoute.Timeline.RefreshMain();

                                $scope.OPSAppointment_DIRoute_Activity_win.close();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.WinActivityClose_Click = function ($event, win) {
        Common.Log("WinActivityClose_Click");
        $event.preventDefault();

        win.close();
    };

    // Win Material
    $scope.WinMaterialOpen = function () {

        //var data = $.extend(true, [], me._dataVehicle);
        //data.splice(0, 1);
        //me._winMaterialGrid.dataSource.data(data);
        //me._winMaterial.open();
    };

    //#endregion

    //#region Order
    $scope.OrderCreate_Click = function ($event) {
        Common.Log("OrderCreate_Click");
        $event.preventDefault();

        $state.go("main.OPSAppointment.DIRouteMaster");
    };

    $scope.OrderCreateHasDN_Click = function ($event, win) {
        $event.preventDefault();

        $state.go("main.OPSAppointment.DIRouteMasterHasDN");
    };

    $scope.OrderClose_Click = function ($event) {
        $event.preventDefault();

        $state.go('main.OPSAppointment.DI');
    };

    $scope.OrderConfig_Click = function ($event) {
        Common.Log('OrderConfig_Click');
        $event.preventDefault();

        $scope.Order.IsShowConfig = !$scope.Order.IsShowConfig;
        // Nếu đóng config thì load lại grid + lưu cookie
        if (!$scope.Order.IsShowConfig) {
            // Load lại data + set cookie
            $scope.OPSAppointment_DIRoute_Order_LoadData();
            Common.Cookie.Set(_OPSAppointment_DIRoute.Data.CookieOrder, JSON.stringify($scope.Order));
            // Resize
            $scope.OPSAppointment_DIRoute_Order_splitter.size(".k-pane:first", "25px");
        } else {
            $scope.OPSAppointment_DIRoute_Order_splitter.size(".k-pane:first", "70px");
        }
    };

    $scope.OrderInputDN_Click = function ($event) {
        $event.preventDefault();

        $state.go('main.OPSAppointment.DIRouteDN');
    };

    $scope.OrderDNEnter_Click = function ($event) {
        Common.Log('OrderDNEnter_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        if (Common.HasValue(this.dataItem)) {
            var item = this.dataItem;
            if (item.Kg >= 0) {
                item.Ton = item.Kg / 1000;

                var data = $scope.OPSAppointment_DIRoute_Order_gridOptions.dataSource.data();
                var str = '';

                if (item.DNCode != '' && item.DNCode != null) {
                    $.each(data, function (i, v) {
                        if (v.ID > 0 && v.ID != item.ID && v.DNCode == item.DNCode)
                            str += ', ' + v.SOCode;
                    });
                }
                if (str != '') {
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Các SO: ' + str.substr(1) + ' đã có số DN này. Bạn có muốn tiếp tục lưu ?',
                        pars: { item: item },
                        Ok: function (pars) {
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIRoute.URL.OrderDNCodeChange,
                                data: pars,
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $scope.OPSAppointment_DIRoute_Order_LoadData();
                                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    });
                                }
                            });
                        }
                    });
                }
                else {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.OrderDNCodeChange,
                        data: { item: item },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.OPSAppointment_DIRoute_Order_LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            } else {
                $rootScope.Message({ Msg: 'Số Kg không được âm', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    };

    $scope.OrderChangeRoute_Click = function ($event) {
        Common.Log('OrderChangeRoute_Click');
        $event.preventDefault();
    };

    $scope.OPSAppointment_DIRoute_Order_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'GenID',
                fields:
                    {
                        GenID: { type: 'string', editable: false },
                        CustomerCode: { type: 'string' },
                        CustomerName: { type: 'string' },
                        OrderCode: { type: 'string' },
                        SOCode: { type: 'string' },
                        DNCode: { type: 'string' },
                        TranferItem: { type: 'string' },
                        ProvinceName: { type: 'string' },
                        DistrictName: { type: 'string' },
                        Address: { type: 'string' },
                        Ton: { type: 'number' },
                        Kg: { type: 'number' },
                        CBM: { type: 'number' },
                        RequestDate: { type: 'date' },
                        ETD: { type: 'date' },

                        PartnerCode: { type: 'string' },
                        PartnerName: { type: 'string' },
                        FromCode: { type: 'string' },
                        FromAddress: { type: 'string' },
                        FromProvince: { type: 'string' },
                        FromDistrict: { type: 'string' },
                        Description: { type: 'string' },
                        CUSRoutingCode: { type: 'string' },
                        TOMasterCode: { type: 'string' },

                        GroupSort: { type: 'string' },
                        GroupSortText: { type: 'string' }
                    }
            },
            sort: { field: 'GroupSortText', dir: 'desc' }
        }),
        height: '99%', pageable: false, sortable: { mode: 'multiple' }, groupable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' }, selectable: 'multiple',
        columns: [
            {
                width: '35px', template: '<div class="icon pointer tomaster fa fa-archive">&nbsp;</div>', headerAttributes: { 'colname': 'tomastericon' }, filterable: false, menu: false, sortable: false, title: ' ',
                sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'CustomerCode', width: '80px', title: '{{RS.CUSCustomer.Code}}', filterable: false,
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerName', width: '100px', title: '{{RS.CUSCustomer.CustomerName}}', filterable: false,
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: '80px', title: '{{RS.ORDOrder.Code}}', filterable: false,
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SOCode', width: '80px', title: 'SO', filterable: false,
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DNCode', width: '170px', template: '<form class="cus-form-enter-btn" ng-submit="OrderDNEnter_Click($event)"><input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.DNCode" /></form><a href="/" ng-click="OrderDNEnter_Click($event)" class="btnDNCode k-button right30"><span class="k-icon k-i-tick"></span></a>', title: 'DN No', filterable: false,
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TranferItem', width: '80px', title: 'Vận chuyển', filterable: false,
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProvinceName', width: '100px', title: 'Tỉnh / Thành', filterable: false,
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DistrictName', width: '100px', title: 'Quận / Huyện', filterable: false,
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
             {
                 field: 'Address', width: '150px', title: 'Địa chỉ', filterable: false,
                 sortorder: 9, configurable: true, isfunctionalHidden: false
             },
            {
                field: 'Ton', width: '80px', title: '{{RS.OPSDITOGroupProduct.Ton}}', template: "#=Common.Number.ToNumber3(Ton)#", filterable: false,
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Kg', width: '80px', title: 'KG', template: '<form class="cus-form-enter" ng-submit="OrderDNEnter_Click($event)"><input type="number" step="0.01" min="0" class="k-textbox" data-k-max="dataItem.Kg" ng-model="dataItem.Kg" style="width:100%"/></form>', filterable: false,
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: '80px', title: '{{RS.OPSDITOGroupProduct.CBM}}', template: "#=Common.Number.ToNumber1(CBM)#", filterable: false,
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDate', width: '100px', title: '{{RS.ORDOrder.RequestDate}}', template: "#=Common.Date.FromJsonDDMMHM(RequestDate)#", filterable: false,
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: '100px', title: 'ETD', template: "#=Common.Date.FromJsonDDMMHM(ETD)#", filterable: false,
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerCode', width: '100px', title: 'Mã NPP', filterable: false,
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerName', width: '100px', title: 'NPP', filterable: false,
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromCode', width: '100px', title: 'Mã l.hàng', filterable: false,
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromAddress', width: '150px', title: 'Điểm l.hàng', filterable: false,
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromProvince', width: '100px', title: 'Tỉnh thành l.hàng', filterable: false,
                sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromDistrict', width: '100px', title: 'Quận huyện l.hàng', filterable: false,
                sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Description', width: '100px', title: 'Ghi chú', filterable: false,
                sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CUSRoutingCode', width: '180px', title: 'Cung đường', filterable: false,
                sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                width: '90px', title: ' ', template: '<a href="/" ng-click="OrderChangeRoute_Click($event)" class="btnAddress k-button"><span class="k-icon k-i-tick"></span>Đổi đ.c</a>', filterable: false,
                sortorder: 23, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TOMasterCode', width: '60px', title: ' ', filterable: false,
                sortorder: 24, configurable: true, isfunctionalHidden: false
            },
                { title: ' ', filterable: false, menu: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ],
        change: function (e) {
            var sel = e.sender.select();
            var pn = $($scope.OPSAppointment_DIRoute_Order_splitter.element.find(".k-pane")[2]).data('pane');
            if (Common.HasValue(sel) && !pn.collapsed) {
                if (sel.length > 0) {
                    var lstid = [];
                    var lst = [];
                    $.each(sel, function (itr, tr) {
                        var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(tr);
                        if (lstid.indexOf(item.ID) < 0) {
                            lstid.push(item.ID);
                            lst.push(item);
                        }
                    });
                    $scope.OPSAppointment_DIRoute_OrderDetail_LoadData(lst);
                }
            }
            else if (sel.length == 1) {
                var currentitem = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(sel[0]);
                var tr = $(sel[0]).next();
                var lst = [];
                while (Common.HasValue(tr)) {
                    var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(tr);
                    if (Common.HasValue(item) && item.GroupSort == currentitem.GroupSort) {
                        lst.push(tr);
                        tr = $(tr).next();
                    }
                    else break;
                }
                tr = $(sel[0]).prev();
                while (Common.HasValue(tr)) {
                    var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(tr);
                    if (Common.HasValue(item) && item.GroupSort == currentitem.GroupSort) {
                        lst.push(tr);
                        tr = $(tr).prev();
                    }
                    else break;
                }
                lst.push($(sel[0]));
                if (lst.length > 1) {
                    $scope.OPSAppointment_DIRoute_Order_grid.select(lst);
                }
            }
        },
        dataBound: function (e) {
            Common.Controls.Grid.MergeRow(this, {
                Columns: ['tomastericon'],
                Compare: function (lvl1item, dataitem) {
                    return lvl1item.GroupSort != null && dataitem.GroupSort != null && lvl1item.GroupSort == dataitem.GroupSort;
                }
            });

            this.table.kendoDraggable({
                filter: 'tbody > tr > td > .icon.tomaster',
                group: 'gridGroup',
                hint: function (e) {
                    return '<div><span class="fa fa-truck" style="font-size:16px;"></span></div>';
                }
            });
        }
    };

    $scope.OPSAppointment_DIRoute_OrderDetail_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        Code: { type: 'string' },
                        OrderCode: { type: 'string' },
                        ETD: { type: 'datetime' },
                        ETA: { type: 'datetime' },
                        Ton: { type: 'number' },
                        CBM: { type: 'number' },
                        Quantity: { type: 'number' }
                    }
            },
        }),
        height: '99%', groupable: true, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns:
            [
                { field: 'OrderCode', width: '80px', title: '{{RS.ORDOrder.Code}}' },
                { field: 'GroupOfProductName', width: '120px', title: 'Nhóm hàng hóa' },
                { field: 'Ton', width: '70px', template: '#=Common.Number.ToNumber1(Ton)#', filterable: false, menu: false, sortable: true, title: 'Tấn' },
                { field: 'CBM', width: '70px', template: '#=Common.Number.ToNumber1(CBM)#', filterable: false, menu: false, sortable: true, title: 'Khối' },
                { field: 'LocationFromName', width: '180px', title: 'Kho lấy hàng' },
                { field: 'LocationFromAddress', width: '180px', title: 'Đ.chỉ lấy hàng' },
                { field: 'LocationToName', width: '180px', title: 'Điểm giao' },
                { field: 'LocationToAddress', width: '180px', title: 'Đ.chỉ giao' },
                { field: 'LocationToProvince', width: '180px', title: 'Tỉnh giao' },
                { field: 'LocationToDistrict', width: '180px', title: 'Quận giao' },
                { field: 'Quantity', width: '70px', template: '#=Common.Number.ToNumber1(Quantity)#', filterable: false, menu: false, sortable: true, title: 'S.l' },
                { title: ' ', filterable: false, sortable: false }
            ],
        dataBound: function (e) { }
    };

    $scope.OPSAppointment_DIRoute_OrderRoute_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {},
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        dataBound: function (e) { }
    };

    // Reorder columns
    $timeout(function () {
        Common.Log("ReorderColumns");
        Common.Controls.Grid.ReorderColumns({ Grid: $scope.OPSAppointment_DIRoute_Order_grid, CookieName: 'OPS_DIAppointment_Route_Order' });
        // Hide tabstrip
        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRoute.Data.CookieTabStrip);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                var panes = $scope.OPSAppointment_DIRoute_splitter.wrapper.find(".k-pane-main");
                if (objCookie.Status == 1) {
                    $scope.OPSAppointment_DIRoute_splitter.collapse(panes[1]);
                    $scope.OPSAppointment_DIRoute_splitter.expand(panes[0]);
                } else {
                    if (objCookie.Status == 2) {
                        $scope.OPSAppointment_DIRoute_splitter.collapse(panes[0]);
                        $scope.OPSAppointment_DIRoute_splitter.expand(panes[1]);
                    } else {
                        $scope.OPSAppointment_DIRoute_splitter.expand(".k-pane-main");
                    }
                }
            } catch (e) { }
        } else {
            $scope.OPSAppointment_DIRoute_splitter.expand(".k-pane-main");
        }
    }, 100);

    $scope.OPSAppointment_DIRoute_Order_mltCustomerOptions = {
        autoBind: true,
        valuePrimitive: true,
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
    };

    $scope.OPSAppointment_DIRoute_OrderMenuOptions = {
        orientation: 'vertical',
        target: '#OPSAppointment_DIRoute_Order_grid',
        filter: 'tr',
        animation: { open: { effects: 'fadeIn' }, duration: 300 },
        open: function (e) {
            var pn = $($scope.OPSAppointment_DIRoute_Order_splitter.element.find('.k-pane')[2]).data('pane');

            if (pn.collapsed)
                $($scope.OPSAppointment_DIRoute_OrderMenu.element.find(".lblShowHide")).html('Hiện chi tiết đơn hàng');
            else
                $($scope.OPSAppointment_DIRoute_OrderMenu.element.find(".lblShowHide")).html('Ẩn chi tiết đơn hàng');
        },
        select: function (e) {
            var index = $(e.item).index();
            switch (index) {
                case 0:
                    var pane = $scope.OPSAppointment_DIRoute_Order_splitter.element.find('.k-pane')[2];
                    var pn = $(pane).data('pane');
                    if (pn.collapsed)
                        $scope.OPSAppointment_DIRoute_Order_splitter.expand(pane);
                    else
                        $scope.OPSAppointment_DIRoute_Order_splitter.collapse(pane);
                    break;
                case 1:
                    var sel = $scope.OPSAppointment_DIRoute_Order_grid.select();
                    if (!Common.HasValue(sel) || sel.length > 1) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Confirm,
                            Msg: 'Bạn muốn gom các đơn hàng đã chọn ?',
                            Ok: function () {
                                var lst = [];
                                $.each(sel, function (i, tr) {
                                    var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(tr);
                                    lst.push({ 'TOMasterID': item.TOMasterID, 'OrderID': item.OrderID, 'CUSRoutingID': item.CUSRoutingID, 'ID': item.ID, 'OrderGroupProductID': item.OrderGroupProductID, 'GroupSort': item.GroupSort, 'DNCode': item.DNCode });
                                });

                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIRoute.URL.OrderGroup,
                                    data: { lst: lst },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $scope.OPSAppointment_DIRoute_Order_LoadData();
                                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                        });
                                    }
                                });
                            }
                        });
                    }
                    break;
                case 2:
                    $scope.OPSAppointment_DIRoute_Order_Div(-1);
                    break;
                case 3:
                    $scope.OPSAppointment_DIRoute_Order_Div(2);
                    break;
                case 4:
                    $scope.OPSAppointment_DIRoute_Order_Div(3);
                    break;
            }
        },
    };

    // Load danh sách khách hàng
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRoute.URL.CustomerList,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.OPSAppointment_DIRoute_Order_mltCustomerOptions.dataSource.data(res);
                }, 1);
            }
        }
    });

    // Load Order Label
    $scope.OPSAppointment_DIRoute_Order_LoadLabel = function () {
        $scope.Order.ConfigString = '';
        var str = 'Ngày: ' + Common.Date.ToString($scope.Order.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
        Common.Date.ToString($scope.Order.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;';
        if (Common.HasValue($scope.OPSAppointment_DIRoute_Order_mltCustomer)) {
            var lstCUS = $scope.OPSAppointment_DIRoute_Order_mltCustomer.dataItems();
            if (lstCUS.length > 0) {
                str += 'Khách hàng: ';
                var strCus = '';
                $.each(lstCUS, function (i, v) {
                    strCus += ',' + v.Code;
                });
                str += strCus.substr(1);
            }
        }
        $scope.Order.ConfigString = str;
    }

    $scope.OPSAppointment_DIRoute_Order_LoadData = function () {
        Common.Log('OrderLoad');

        var lstFilters = [
            Common.Request.FilterParamWithAnd('RequestDate', Common.Request.FilterType.GreaterThanOrEqual, $scope.Order.DateFrom),
            Common.Request.FilterParamWithAnd('RequestDate', Common.Request.FilterType.LessThanOrEqual, Common.Date.AddDay(Common.Date.Date($scope.Order.DateTo), 1))
        ];

        if ($scope.Order.ListCustomerID.length > 0) {
            var lstAdd = [];
            $.each($scope.Order.ListCustomerID, function (i, v) {
                lstAdd.push(Common.Request.FilterParamWithOr('CustomerID', Common.Request.FilterType.Equal, v));
            });
            lstAdd[0] = '(' + lstAdd[0];
            if (lstAdd.length == 1)
                lstAdd[0] = lstAdd[0] + ')';
            else
                lstAdd[lstAdd.length - 1] = lstAdd[lstAdd.length - 1] + ')';
            $.each(lstAdd, function (i, v) {
                lstFilters.push(v);
            });
        }
        var param = Common.Request.Create({
            Sorts: [], Filters: lstFilters
        });

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.OrderList,
            data: { request: param },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    var lstCheck = [];
                    $.each(res, function (i, v) {
                        v.ETD = Common.Date.FromJson(v.ETD);
                        if (Common.HasValue(v.ETA))
                            v.ETA = Common.Date.FromJson(v.ETA);
                        v.Kg = v.Ton * 1000;

                        var index = lstCheck.indexOf(v.GroupSort);
                        lstCheck.push(v.GroupSort);
                        if (index >= 0) {
                            res[index].GroupSortText = v.GroupSort;
                            v.GroupSortText = v.GroupSort;
                        }
                        else
                            v.GroupSortText = null;
                    });
                    $scope.OPSAppointment_DIRoute_Order_gridOptions.dataSource.data(res);
                });
            }
        });

        $scope.OPSAppointment_DIRoute_Order_LoadLabel();
    };

    // Load Order Grid
    $scope.OPSAppointment_DIRoute_Order_LoadData();

    // Load Order Detail
    $scope.OPSAppointment_DIRoute_OrderDetail_LoadData = function (lst) {
        Common.Log("OrderDetailLoad");

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRoute.URL.OrderDetailList,
            data: { lst: lst },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $scope.OPSAppointment_DIRoute_OrderDetail_gridOptions.dataSource.data(res);
                });
            }
        });
    };

    // Chia đơn hàng
    $scope.OPSAppointment_DIRoute_Order_Div = function (i) {
        Common.Log("OrderDiv");
        var sel = $scope.OPSAppointment_DIRoute_Order_grid.select();
        if (!Common.HasValue(sel) || sel.length > 0) {
            if (i > 0) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn chia đơn hàng làm ' + i + ' ?',
                    Ok: function () {
                        var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(sel[0]);
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRoute.URL.OrderDiv,
                            data: { item: item, div: i },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.OPSAppointment_DIRoute_Order_LoadData();
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                });
                            }
                        });
                    }
                });
            }
            else {
                var data = $scope.OPSAppointment_DIRoute_Order_grid.dataSource.data();
                var flag = false;
                var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(sel[0]);
                for (var j = 0; j < data.length; j++) {
                    if (item.ID != data[j].ID && item.GroupSort == data[j].GroupSort) {
                        flag = true;
                        break;
                    }
                }
                if (flag) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Bạn muốn tách đơn hàng đã chọn ?',
                        Ok: function () {
                            var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(sel[0]);
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIRoute.URL.OrderDiv,
                                data: { item: item, div: -1 },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $scope.OPSAppointment_DIRoute_Order_LoadData();
                                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    });
                                }
                            });
                        }
                    });
                }
            }
        }
    };

    $scope.OPSAppointment_DIRoute_Order_DivLoad = function () {
        Common.Log('OrderDivLoad');
        var sel = $scope.OPSAppointment_DIRoute_Order_grid.select();
        if (!Common.HasValue(sel) || sel.length > 0) {
            var item = $scope.OPSAppointment_DIRoute_Order_grid.dataItem(sel[0]);
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRoute.URL.OrderDivCustomGet,
                data: { item: item },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $.each(res, function (i, v) {
                            var groupcustom = 'Đơn hàng: ' + v.OrderCode;
                            v.GroupCustom = groupcustom;

                            v.TonTotal = v.Ton;
                            v.CBMTotal = v.CBM;
                            v.TonExtra = 0;
                            v.CBMExtra = 0;
                        });

                        //me._winOrderDivGrid.dataSource.data(res);

                        //me._winOrderDiv.open();
                    });
                }
            });
        }
    };
    //#endregion Order
}]);