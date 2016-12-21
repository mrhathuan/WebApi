/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODInput_ExtReturn = {
    URL: {
        Read: 'PODOPSExtReturn_List',
        Get: 'PODOPSExtReturn_Get',
        Save: 'PODOPSExtReturn_Save',
        Delete: 'PODOPSExtReturn_Delete',
        Approved: 'PODOPSExtReturn_Approved',

        CustomerList: 'PODOPSExtReturn_CustomerList',
        VehicleList: 'PODOPSExtReturn_VehicleList',
        DriverList: 'PODOPSExtReturn_DriverList',
        VendorList: 'PODOPSExtReturn_VendorList',
        GOPList: 'PODOPSExtReturn_GOPByCus',
        ProductList: 'PODOPSExtReturn_ProductByGOP',
        DITOMasterList: 'PODOPSExtReturn_DITOMasterList',

        DetailList: 'PODOPSExtReturn_DetailList',
        DetailNotIn: 'PODOPSExtReturn_DetailNotIn',
        SaveDetail: 'PODOPSExtReturn_DetailSave',

        FindList: 'PODOPSExtReturn_FindList',
        ExtReturnStatus: 'ALL_ExtReturnStatus',

        QuickList: 'PODOPSExtReturn_QuickList',
        QuickSave: 'PODOPSExtReturn_QuickSave',
        QuickData: 'PODOPSExtReturn_QuickData'
    },
    Data: {
        ExtReturnDetailList: [],
        ListExtReturnStatus: null,
        LastInvoiceNo: { DITOProductID: -1, InvoiceNo: ' ' },
        ListVendor: null,
        ListVehicle:null,
    }
}

//#endregion

angular.module('myapp').controller('PODInput_ExtReturnCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('PODInput_ExtReturnCtrl');
    $rootScope.IsLoading = false;
    $scope.TabIndex = 0;
    $scope.Item = { ID: 0, DITOMasterID: 0 };
    $scope.vendorID = -1;
    $scope.cusID = -1;
    $scope.vehicleID = 0;
    $scope.DetailNotInHasChoose = false;
    $scope.DetailHasChoose = false;
    $scope.MainHasChoose = false;

    $scope.Search = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    }

    $scope.PODInputExtReturn_SearchClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.Search.DateFrom > $scope.Search.DateTo) {
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
            $scope.PODInputExtReturn_Options.dataSource.read();
        }
    }

    $scope.PODInputExtReturn_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.Search.DateFrom,
                    'dtTo': $scope.Search.DateTo,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, pagesize: 50,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,PODInputExtReturn_Grid,PODInputExtReturn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PODInputExtReturn_Grid,PODInputExtReturn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="EditExtReturn_Click($event,ext_return_Edit_win,dataItem, extReturn_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'InvoiceNo', title: 'Chứng từ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'InvoiceDate', title: 'Ngày nhập', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, template: '#=Common.Date.FromJsonDDMMYY(InvoiceDate)#' },
            { field: 'DITOMasterCode', title: 'Mã chuyến', width: 150, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'CustomerCode', title: 'Mã khách hàng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorCode', title: 'Mã nhà xe', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverCode', title: 'Mã tài xế', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleNo', title: 'Xe', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', title: 'Mã sản phẩm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Quantity', title: 'Số lượng', width: 150, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'ExtReturnStatusName', title: 'Loại trả về', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'IsApproved', width: "80px", title: 'Đã duyệt', sortable: false, filterable: false, menu: true,
                template: '<input ng-show="dataItem.IsApproved != null" disabled type="checkbox" #= IsApproved ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.PODInputExtReturn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MainHasChoose = hasChoose;
    }
    $scope.numQuantity_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.PODInputExtReturn_AddNewClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadExtReturn(0, win, vform);
    }

    $scope.PODInputExtReturn_QuickClick = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.extReturn_Quick_GridOptions.dataSource.read();
    }

    $scope.EditExtReturn_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadExtReturn(data.ID, win, vform)
    }
    $scope.PODInputExtReturn_ApprovedClick = function ($event, grid) {
        $event.preventDefault();
        var dataSend = [];
        var error = false;
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                dataSend.push(o.ID);
                if (o.IsApproved) error = true;
            }
        })
        if (error) $rootScope.Message({ Msg: 'Có dữ liệu đã được duyệt, không duyệt lại', NotifyType: Common.Message.NotifyType.ERROR });
        else {
            if (dataSend.length > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODInput_ExtReturn.URL.Approved,
                    data: { lst: dataSend },
                    success: function (res) {
                        $scope.PODInputExtReturn_Options.dataSource.read();
                        $rootScope.IsLoading = false;
                        $scope.MainHasChoose = false;
                        $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        }
    }

    $scope.PODInputExtReturn_DeleteClick = function ($event, grid) {
        $event.preventDefault();
        var dataSend = [];
        var error = false;
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                dataSend.push(o.ID);
                if (o.IsApproved) error = true;
            }
        })
        if (error) $rootScope.Message({ Msg: 'Có dữ liệu đã được duyệt, không được xóa', NotifyType: Common.Message.NotifyType.ERROR });
        else {
            if (dataSend.length > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODInput_ExtReturn.URL.Delete,
                    data: { lst: dataSend },
                    success: function (res) {
                        $scope.PODInputExtReturn_Options.dataSource.read();
                        $rootScope.IsLoading = false;
                        $scope.MainHasChoose = false;
                        $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        }
    }

    $scope.LoadExtReturn = function (id, win, vform) {
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.Get,
            data: { ID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                if ($scope.Item.VendorID == null) {
                    $scope.Item.VendorID = -1;
                }
                if ($scope.Item.DITOMasterID == null) {
                    $scope.Item.DITOMasterID = -1;
                }
                $scope.LoadCustomerList();
                $scope.LoadGOPList($scope.Item.CustomerID);
                if ($scope.Item.GroupProductID != null) {
                    $scope.LoadProductList($scope.Item.GroupProductID);
                }
                $scope.LoadDriverList();
                $scope.LoadVendorList();
                $scope.LoadVehicleList($scope.Item.VendorID);
                $scope.LoadDITOMasterList($scope.Item.CustomerID, $scope.Item.VendorID, $scope.Item.VehicleID);

                $scope.extReturn_Detail_GridOptions.dataSource.read();
                _PODInput_ExtReturn.Data.ExtReturnDetailList = $scope.extReturn_Detail_GridOptions.dataSource.data();
                if ($scope.Item.ID > 0)
                    $scope.extReturn_Detail_Grid.setOptions({ editable: false })
               else $scope.extReturn_Detail_Grid.setOptions({ editable: 'incell' })
                win.center().open();
                vform({ clear: true })
                $scope.ext_return_tabstrip.select(0);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.extReturn_win_Save_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            var error = false;
            if ($scope.Item.VehicleID <= 0) {
                error = true;
                $rootScope.Message({ Msg: 'Chưa chọn xe', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
            if ($scope.Item.GroupProductID <= 0) {
                error = true;
                $rootScope.Message({ Msg: 'Chưa chọn nhóm sản phẩm', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
            if ($scope.Item.ProductID <= 0) {
                error = true;
                $rootScope.Message({ Msg: 'Chưa chọn sản phẩm', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
            if (!error && $scope.Item.Quantity > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODInput_ExtReturn.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.POD,
                            method: _PODInput_ExtReturn.URL.Get,
                            data: { ID: res },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.Item = res;
                                $scope.PODInputExtReturn_Options.dataSource.read();

                                if ($scope.Item.VendorID == null) {
                                    $scope.Item.VendorID = -1;
                                }
                                if ($scope.Item.DITOMasterID == null) {
                                    $scope.Item.DITOMasterID = -1;
                                }
                                $scope.LoadCustomerList();
                                $scope.LoadGOPList($scope.Item.CustomerID);
                                if ($scope.Item.GroupProductID != null) {
                                    $scope.LoadProductList($scope.Item.GroupProductID);
                                }
                                $scope.LoadDriverList();
                                $scope.LoadVendorList();
                                $scope.LoadVehicleList($scope.Item.VendorID);
                                $scope.LoadDITOMasterList($scope.Item.CustomerID, $scope.Item.VendorID, $scope.Item.VehicleID);

                                $scope.extReturn_Detail_GridOptions.dataSource.read();
                                _PODInput_ExtReturn.Data.ExtReturnDetailList = $scope.extReturn_Detail_GridOptions.dataSource.data();

                                $scope.extReturn_DetailNotIn_GridOptions.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        });

                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            } else {
                $rootScope.Message({ Msg: 'Số lượng phải lớn hơn 0', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.suggest_click = function ($event, win) {
        $event.preventDefault();
        $scope.extReturn_FindList_GridOptions.dataSource.read();
        win.center();
        win.open();
    }

    $scope.ext_return_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }

    //#region cbx
    $scope.PODInput_ExtReturn_cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.Item.CustomerID = this.value();
            $scope.Item.DITOMasterID = -1;
            $scope.GroupProductID = -1;
            $scope.LoadDITOMasterList($scope.Item.CustomerID, $scope.Item.VendorID, $scope.Item.VehicleID);
            $scope.LoadGOPList($scope.Item.CustomerID);
        }
    }
    $scope.LoadCustomerList = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.CustomerList,
            data: {},
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.PODInput_ExtReturn_cboCustomer_Options.dataSource.data(res.Data);
                }
            }
        });
    }

    $scope.PODInput_ExtReturn_cboVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'RegNo', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RegNo: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.Item.VehicleID = this.value();
            $scope.Item.DITOMasterID = -1;
            $scope.LoadDITOMasterList($scope.Item.CustomerID, $scope.Item.VendorID, $scope.Item.VehicleID);
        }
    }

    $scope.LoadVehicleList = function (venID) {

        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.VehicleList,
            data: { vendorID: venID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    var item = { ID: -1, RegNo: 'Chọn xe' };
                    res.unshift(item);
                    $scope.PODInput_ExtReturn_cboVehicle_Options.dataSource.data(res);
                }
            }
        });
    }

    $scope.PODInput_ExtReturn_cboDriver_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DriverName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DriverName: { type: 'string' },
                }
            }
        }),
        change: function (e) {

        }
    }

    $scope.LoadDriverList = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.DriverList,
            data: {},
            success: function (res) {

                var item = { ID: -1, DriverName: '' };
                res.unshift(item);
                if (Common.HasValue(res)) {
                    $scope.PODInput_ExtReturn_cboDriver_Options.dataSource.data(res);
                }
                //if ($scope.Item.VendorID == -1) {
                //    $("#cboDriver").data("kendoComboBox").enable();
                //} else {
                //    $("#cboDriver").data("kendoComboBox").enable(false);
                //}
            }
        });
    }

    $scope.PODInput_ExtReturn_cboVendor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.Item.VendorID = this.value();
            $scope.Item.VehicleID = 0;
            $scope.Item.DITOMasterID = -1;
            $scope.Item.DriverID = -1;
            $scope.LoadVehicleList($scope.Item.VendorID);
            $scope.LoadDITOMasterList($scope.Item.CustomerID, $scope.Item.VendorID, $scope.Item.VehicleID);
            $scope.LoadDriverList();
        }
    }

    $scope.LoadVendorList = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.VendorList,
            data: {},
            success: function (res) {

                if (Common.HasValue(res)) {
                    var item = { ID: -1, CustomerName: 'Xe nhà' };
                    res.Data.unshift(item);
                    $scope.PODInput_ExtReturn_cboVendor_Options.dataSource.data(res.Data);
                }
            }
        });
    }

    $scope.PODInput_ExtReturn_cboDITOMaster_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TOMasterCode', dataValueField: 'TOMasterID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'TOMasterID',
                fields: {
                    TOMasterID: { type: 'number' },
                    TOMasterCode: { type: 'string' },
                }
            }
        }),
        change: function (e) {

        }
    }

    $scope.LoadDITOMasterList = function (cusID, vendorID, vehicleID) {
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.DITOMasterList,
            data: { cusID: cusID, vendorID: vendorID, vehicleID: vehicleID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    var item = { TOMasterID: -1, TOMasterCode: '' };
                    res.unshift(item);
                    $scope.PODInput_ExtReturn_cboDITOMaster_Options.dataSource.data(res);
                }
            }
        });
    }

    $scope.PODInput_ExtReturn_cboGOP_Options = {
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
        }),
        change: function (e) {
            $scope.Item.GroupProductID = this.value();
            $scope.Item.ProductID = -1;
            $scope.LoadProductList($scope.Item.GroupProductID);
        }
    }
    $scope.LoadGOPList = function (cusID) {

        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.GOPList,
            data: { cusID: cusID },
            success: function (res) {
                var item = { ID: -1, Code: 'Chọn nhóm sản phẩm' };
                res.Data.unshift(item);
                if (Common.HasValue(res)) {
                    $scope.PODInput_ExtReturn_cboGOP_Options.dataSource.data(res.Data);
                }
            }
        });
    }

    $scope.PODInput_ExtReturn_cboProduct_Options = {
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
        }),
        change: function (e) {
        }
    }
    $scope.LoadProductList = function (gopID) {

        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.ProductList,
            data: { gopID: gopID },
            success: function (res) {
                var item = { ID: -1, Code: 'Chọn sản phẩm' };
                res.Data.unshift(item);
                if (Common.HasValue(res)) {
                    $scope.PODInput_ExtReturn_cboProduct_Options.dataSource.data(res.Data);
                }
            }
        });
    }


    $scope.PODInput_ExtReturn_cboExtReturnStatus_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }


    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _PODInput_ExtReturn.URL.ExtReturnStatus,
        data: {},
        success: function (res) {
            $scope.PODInput_ExtReturn_cboExtReturnStatus_Options.dataSource.data(res.Data)
            $scope.cboExtReturnStatusOptions.dataSource.data(res.Data)
            _PODInput_ExtReturn.Data.ListExtReturnStatus = res.Data;
        }
    });

    //#endregion

    $scope.extReturn_Detail_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.DetailList,
            readparam: function () { return { ExtReturnID: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                    OrderCode: { type: 'string', editable: false },
                    LocationTo: { type: 'string', editable: false },
                    GroupProductCode: { type: 'string', editable: false },
                    ProductCode: { type: 'string', editable: false },
                    Quantity: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell', pagesize: 20,
        columns: [
            { field: 'OrderCode', title: "Đơn hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationTo', title: "Địa điểm đến", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', title: "Nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', title: "Sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Quantity', title: 'Số lượng', width: 150,
                editor: function (container, options) {
                    //$('<input  name="' + options.field + '" kendo-numeric-text-box style="width:100%" k-options="numQuantity_Options"/>').appendTo(container)
                    var kmax = options.model.KMax > 0 ? options.model.KMax : 0;
                    var input = $('<input name="' + options.field + '"  />');
                    input.appendTo(container);
                    input.kendoNumericTextBox({
                        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, max: kmax
                    });
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.detail_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.DetailHasChoose = hasChoose;
    }

    $scope.detail_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.extReturn_DetailNotIn_GridOptions.dataSource.read();
    }


    $scope.extReturn_DetailNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.DetailNotIn,
            readparam: function () { return { masterID: $scope.Item.DITOMasterID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                    OrderCode: { type: 'string', editable: false },
                    LocationTo: { type: 'string', editable: false },
                    GroupProductCode: { type: 'string', editable: false },
                    ProductCode: { type: 'string', editable: false },
                    Quantity: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell', pagesize: 20,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,extReturn_DetailNotIn_Grid,detailNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,extReturn_DetailNotIn_Grid,detailNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'OrderCode', title: "Đơn hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationTo', title: "Địa điểm đến", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', title: "Nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', title: "Sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Quantity', title: 'Số lượng', width: 150,
                editor: function (container, options) {
                    var kmax = options.model.KMax > 0 ? options.model.KMax : 0;
                    var input = $('<input name="' + options.field + '"  />');
                    input.appendTo(container);
                    input.kendoNumericTextBox({
                        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, max: kmax
                    });
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.detailNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.DetailNotInHasChoose = hasChoose;
    }

    $scope.detailNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose)
                datasend.push(o);
        })
        $rootScope.IsLoading = true;
        if (datasend.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODInput_ExtReturn.URL.SaveDetail,
                data: { ExtReturnID: $scope.Item.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.extReturn_Detail_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                    $scope.PODInputExtReturn_Options.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.extReturn_SaveDetail_Click = function ($event, grid) {

        $event.preventDefault();
        var data = grid.dataSource.data();
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODInput_ExtReturn.URL.SaveDetail,
                data: { ExtReturnID: $scope.Item.ID, lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.extReturn_Detail_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //suggest
    $scope.extReturn_FindList_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.FindList,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    Quantity: { type: 'number' }
                }
            }
        }),
        selectable: 'row', reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, pagesize: 50,
        columns: [
            { field: 'OrderCode', title: 'Mã ĐH', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DITOMasterCode', title: 'Mã chuyến', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: 'Mã khách hàng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorCode', title: 'Mã nhà xe', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleNo', title: 'Xe', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationTo', title: 'Nơi đến', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', title: 'Mã nhóm hàng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', title: 'Mã hàng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Quantity', title: 'Số lượng', width: 150, filterable: { cell: { operator: 'eq', showOperators: false } } },
            {
                field: 'ETD', title: 'ETD', width: 150, template: '#=Common.Date.FromJsonDDMMYY(ETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', title: 'ETA', width: 150, template: '#=Common.Date.FromJsonDDMMYY(ETA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FindList_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var dataitem = grid.dataItem(grid.select())
        if (Common.HasValue(dataitem)) {
            $scope.Item = dataitem;
            if ($scope.Item.VendorID == null) {
                $scope.Item.VendorID = -1;
            }
            if ($scope.Item.DITOMasterID == null) {
                $scope.Item.DITOMasterID = -1;
            }
            $scope.LoadCustomerList();
            $scope.LoadGOPList($scope.Item.CustomerID);
            if ($scope.Item.GroupProductID != null) {
                $scope.LoadProductList($scope.Item.GroupProductID);
            }
            $scope.LoadDriverList();
            $scope.LoadVendorList();
            $scope.LoadVehicleList($scope.Item.VendorID);
            $scope.LoadDITOMasterList($scope.Item.CustomerID, $scope.Item.VendorID, $scope.Item.VehicleID);

            $scope.extReturn_Detail_GridOptions.dataSource.read();
            _PODInput_ExtReturn.Data.ExtReturnDetailList = $scope.extReturn_Detail_GridOptions.dataSource.data();

            win.close()
            $scope.ext_return_tabstrip.select(0);
        }
    }

    //#region quick
    $scope.extReturn_Quick_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_ExtReturn.URL.QuickList,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    Quantity: { type: 'number' },
                    KMax: { type: 'number' },
                    InvoiceDate: { type: 'date' },
                    InvoiceNo: { type: 'string' }
                }
            }
        }),
        reorderable: false, editable: 'inline',
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, pagesize: 20,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Quick_SaveClick($event,dataItem)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false, locked: true,
            },
            { field: 'OrderCode', title: 'Mã ĐH', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DITOMasterCode', title: 'Mã chuyến', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: 'Mã khách hàng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorCode', title: 'Mã nhà xe', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'VendorID', title: 'Mã nhà xe', width: 120,
                template: '<input kendo-combo-box class="cus-combobox" focus-k-combobox  k-options="cboExtReturnVendorIDOptions"  k-ng-model="dataItem.VendorID" value="dataItem.VendorID" />',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            { field: 'VehicleNo', title: 'Xe', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'VehicleID', title: 'Xe', width: 120,
                template: '<input kendo-combo-box class="cus-combobox" focus-k-combobox k-options="cboExtReturnVehicleOptions" k-data-source="dataItem.ListVehicle"  ng-model="dataItem.VehicleID" value="dataItem.VehicleID" />',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            { field: 'LocationTo', title: 'Nơi đến', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', title: 'Mã nhóm hàng', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', title: 'Mã hàng', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Quantity', title: 'Số lượng', width: 120,
                template: '<input kendo-numeric-text-box k-options="numQuantyOptions" k-min="1" k-max="dataItem.KMax" k-ng-model="dataItem.Quantity" style="width: 100%;" />',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'ExtReturnStatusID', title: 'Loại trả về', width: 120,
                template: '<input kendo-combo-box focus-k-combobox class="cus-combobox" k-options="cboExtReturnStatusOptions"  ng-model="dataItem.ExtReturnStatusID" value="dataItem.ExtReturnStatusID" />',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'InvoiceNo', title: 'Số chứng từ', width: 150,
                template: '<input type="text" class="k-textbox"  ng-model="dataItem.InvoiceNo"  />',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', title: 'Ngày chứng từ', width: 150,
                template: '<input kendo-date-picker class="cus-datepicker" k-options="DateDMY"  k-ng-model="dataItem.InvoiceDate" value="dataItem.InvoiceDate" />',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', title: 'ETD', width: 150, template: '#=Common.Date.FromJsonDDMMYY(ETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', title: 'ETA', width: 150, template: '#=Common.Date.FromJsonDDMMYY(ETA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            var grid = this;
            var data = grid.dataSource.view();
            Common.Data.Each(data, function (o) {
                if (o.ID == _PODInput_ExtReturn.Data.LastInvoiceNo.DITOProductID)
                    o.InvoiceNo = _PODInput_ExtReturn.Data.LastInvoiceNo.InvoiceNo;
                o.ListVehicle = _PODInput_ExtReturn.Data.ListVehicle[o.VendorID];
            })
        }
    }

    $scope.numQuantyOptions = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.cboExtReturnStatusOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ValueOfVar: { type: 'string' },
                    ID: { type: 'number' },
                }
            }),
        
    }

    $scope.cboExtReturnVendorIDOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                Code: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            var cbb= this;
            var val=cbb.value();
            if(e.sender.selectedIndex>=0)
            {
                var tr = $(cbb.element).closest('tr');
                var grid = $scope.extReturn_Quick_Grid;
                var dataItem = grid.dataItem(tr);
                if (Common.HasValue(dataItem)) {
                    dataItem.ListVehicle = _PODInput_ExtReturn.Data.ListVehicle[val];

                    if (_PODInput_ExtReturn.Data.ListVehicle[val].length > 0)
                        dataItem.VehicleID = _PODInput_ExtReturn.Data.ListVehicle[val][0].VehicleID;
                }
                debugger
            }
        }
    }

    $scope.cboExtReturnVehicleOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'RegNo', dataValueField: 'VehicleID',
        //dataSource: Common.DataSource.Local([], {
        //    id: 'VehicleID',
        //    fields: {
        //        RegNo: { type: 'string' },
        //        VehicleID: { type: 'number' },
        //    }
        //})
    }

    Common.Services.Call($http, {
        url: Common.Services.url.POD,
        method: _PODInput_ExtReturn.URL.QuickData,
        data: {},
        success: function (res) {
            _PODInput_ExtReturn.Data.ListVendor = [];
            _PODInput_ExtReturn.Data.ListVendor.push({ ID: -1, Code: "Xe Nhà", CustomerName: "Xe Nhà" });
            Common.Data.Each(res.ListVendor, function (ven) {
                _PODInput_ExtReturn.Data.ListVendor.push(ven);
            })
            _PODInput_ExtReturn.Data.ListVehicle = {};
            Common.Data.Each(res.ListVehicle, function (vehicle) {
                if (!Common.HasValue(_PODInput_ExtReturn.Data.ListVehicle[vehicle.CustomerID])) {
                    _PODInput_ExtReturn.Data.ListVehicle[vehicle.CustomerID] = [];
                    _PODInput_ExtReturn.Data.ListVehicle[vehicle.CustomerID].push(vehicle);
                }
                else _PODInput_ExtReturn.Data.ListVehicle[vehicle.CustomerID].push(vehicle);
            })
            
            $scope.cboExtReturnVendorIDOptions.dataSource.data(_PODInput_ExtReturn.Data.ListVendor)
        },
        error: function (res) {
        }
    });


    $scope.Quick_SaveClick = function ($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data.InvoiceNo) && data.InvoiceNo != "") {
            if (Common.HasValue(data.InvoiceDate)) {
                if (Common.HasValue(data.VendorID)&&Common.HasValue(data.VehicleID)) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.POD,
                        method: _PODInput_ExtReturn.URL.QuickSave,
                        data: { item: data },
                        success: function (res) {
                            _PODInput_ExtReturn.Data.LastInvoiceNo.DITOProductID = data.ID;
                            _PODInput_ExtReturn.Data.LastInvoiceNo.InvoiceNo = data.InvoiceNo;
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.ERROR });
                            $scope.extReturn_Quick_GridOptions.dataSource.read();
                            $scope.PODInputExtReturn_Options.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                } else $rootScope.Message({ Msg: 'Nhà xe, Số xe không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                
            } else $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else $rootScope.Message({ Msg: 'Số chứng từ không được trống', NotifyType: Common.Message.NotifyType.ERROR });
    }
    //#endregion

    //actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput_ExtReturn,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);