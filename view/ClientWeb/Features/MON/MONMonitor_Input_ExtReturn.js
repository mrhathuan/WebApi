/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONInput_ExtReturn = {
    URL: {
        Read: 'MONOPSExtReturn_List',
        Get: 'MONOPSExtReturn_Get',
        Save: 'MONOPSExtReturn_Save',
        Delete: 'MONOPSExtReturn_Delete',
        Approved: 'MONOPSExtReturn_Approved',
        UnApproved: 'MONOPSExtReturn_UnApproved',

        CustomerList: 'MONOPSExtReturn_CustomerList',
        VehicleList: 'MONOPSExtReturn_VehicleList',
        DriverList: 'MONOPSExtReturn_DriverList',
        VendorList: 'MONOPSExtReturn_VendorList',
        GOPList: 'MONOPSExtReturn_GOPByCus',
        ProductList: 'MONOPSExtReturn_ProductByGOP',
        DITOMasterList: 'MONOPSExtReturn_DITOMasterList',

        DetailList: 'MONOPSExtReturn_DetailList',
        DetailNotIn: 'MONOPSExtReturn_DetailNotIn',
        SaveDetail: 'MONOPSExtReturn_DetailSave',

        FindList: 'MONOPSExtReturn_FindList',
        ExtReturnStatus: 'ALL_ExtReturnStatus',

        QuickList: 'MONOPSExtReturn_QuickList',
        QuickSave: 'MONOPSExtReturn_QuickSave',
        QuickData: 'MONOPSExtReturn_QuickData'
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

angular.module('myapp').controller('MONMonitor_Input_ExtReturnCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('MONMonitor_Input_ExtReturnCtrl');
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
        ListCustomerID:[]
    }

    var cookie = Common.Cookie.Get("ExtReturn_Search");
    if (!Common.HasValue(cookie) || cookie == '') {
        var val = JSON.stringify($scope.Search)
        Common.Cookie.Set("ExtReturn_Search", val)
    }
    else {
        var val = JSON.parse(cookie);
        $scope.Search = val;
    }
    
    

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
            }
        }),
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã khách hàng </span><span style="float:right;"> Tên khách hàng </span></strong>',
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Customer,
        success: function (data) {
            $scope.mts_CustomerOptions.dataSource.data(data)
        }
    })

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
            var val = JSON.stringify($scope.Search)
            Common.Cookie.Set("ExtReturn_Search", val)
            $scope.MONInputExtReturn_GridOptions.dataSource.read();
        }
    }

    $scope.MONInputExtReturn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.Search.DateFrom,
                    'dtTo': $scope.Search.DateTo,
                    'listCustomerID': $scope.Search.ListCustomerID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    IsChoose: { type: 'boolean' },
                    InvoiceDate: { type: 'date' },
                    RequestDate: { type: 'date' },
                    ExtReturnID: { type: 'number', editable: false, },
                    IsApproved: { type: 'boolean' },
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, pagesize: 50,
        columns: [
            {
                title: ' ', width: 35, field: 'F_Command1', sortorder: 0, configurable: true, isfunctionalHidden: true,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,MONInputExtReturn_Grid,MONInputExtReturn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,MONInputExtReturn_Grid,MONInputExtReturn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: 35, field: 'F_Command2', sortorder: 1, configurable: true, isfunctionalHidden: true,
                template: '<a href="/" ng-click="EditExtReturn_SaveClick($event,dataItem,MONInputExtReturn_Grid)" class="k-button" ng-show="!IsApproved"><i class="fa fa-floppy-o"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'IsApproved', width: 100, title: 'Đã duyệt', sortable: false, filterable: false, menu: true, sortorder: 2, configurable: true, isfunctionalHidden: true,
                template: '<input ng-show="dataItem.IsApproved != null" disabled type="checkbox" #= IsApproved ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            {
                field: 'InvoiceNo', title: 'Chứng từ', width: 150, sortorder: 3, configurable: true, isfunctionalHidden: true,
                template: '<input type="text"  class="k-textbox"  ng-model="dataItem.InvoiceNo" ng-disabled="dataItem.IsApproved"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', title: 'Ngày nhập', width: 120, sortorder: 4, configurable: true, isfunctionalHidden: true,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '<input class="cus-datepicker" kendo-date-picker focus-k-datepicker k-options="DateDMY" k-ng-model="dataItem.InvoiceDate" style="width: 100px;"  ng-disabled="dataItem.IsApproved"/>'

            },
            {
                field: 'Quantity', title: 'Số lượng', width: 100, filterable: { cell: { operator: 'eq', showOperators: false } }, sortorder: 13, configurable: true, isfunctionalHidden: true,
                template: '<input kendo-numeric-text-box k-options="numQuantyOptions" k-min="1" k-max="dataItem.KMax" k-ng-model="dataItem.Quantity" style="width: 100%;" ng-disabled="dataItem.IsApproved" />',
            },
            {
                field: 'ExtReturnStatusName', title: 'Loại trả về', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 0, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'ORDGroupNote1', title: 'Ghi chú c/t đơn hàng 1', width: 150, sortorder: 3, configurable: true, isfunctionalHidden: true,
                template: '<input type="text"  class="k-textbox"  ng-model="dataItem.ORDGroupNote1"  ng-disabled="dataItem.IsApproved"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ORDGroupNote2', title: 'Ghi chú c/t đơn hàng 2', width: 150, sortorder: 3, configurable: true, isfunctionalHidden: true,
                template: '<input type="text"  class="k-textbox"  ng-model="dataItem.ORDGroupNote2"  ng-disabled="dataItem.IsApproved"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'Note', title: 'Ghi chú đ/h', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: true, },
            { field: 'DITOMasterCode', title: 'Mã chuyến', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: true, },
            { field: 'CustomerCode', title: 'Mã khách hàng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: true, },
            { field: 'VendorCode', title: 'Mã nhà xe', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 7, configurable: true, isfunctionalHidden: true, },
            { field: 'DriverCode', title: 'Mã tài xế', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 8, configurable: true, isfunctionalHidden: true, },
            { field: 'VehicleNo', title: 'Xe', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 9, configurable: true, isfunctionalHidden: true, },
            { field: 'ProductCode', title: 'Mã sản phẩm', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 10, configurable: true, isfunctionalHidden: true, },
            { field: 'OrderCode', title: 'Mã đơn hàng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 11, configurable: true, isfunctionalHidden: true, },
            {
                field: 'RequestDate', title: 'Ngày gửi yêu cầu', width: 120, sortorder: 12, configurable: true, isfunctionalHidden: true,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(RequestDate)#'
            },
            {
                field: 'ETD', title: 'ETD', width: 120, template: '#=Common.Date.FromJsonDDMMYY(ETD)#', sortorder: 14, configurable: true, isfunctionalHidden: true,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', title: 'ETA', width: 120, template: '#=Common.Date.FromJsonDDMMYY(ETA)#', sortorder: 16, configurable: true, isfunctionalHidden: true,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: ' ', filterable: false, sortable: false, sortorder: 100, configurable: true, isfunctionalHidden: true, }
        ]
    }

    $scope.numQuantyOptions = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.MONInputExtReturn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MainHasChoose = hasChoose;
    }
    $scope.numQuantity_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.PODInputExtReturn_AddNewClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadExtReturn(0, win, vform);
    }

    $scope.EditExtReturn_SaveClick = function ($event, data,grid) {
        $event.preventDefault();
        if(!Common.HasValue(data.InvoiceNo)||data.InvoiceNo=='')
            $rootScope.Message({ Msg: 'Số chứng từ không được trống', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        else if(!Common.HasValue(data.Quantity)||data.Quantity<0)
            $rootScope.Message({ Msg: 'Số lượng không chính xác', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONInput_ExtReturn.URL.Save,
                data: { item: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.PODInputExtReturn_QuickClick = function ($event, win) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Input_ExtReturnQuick")
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
                dataSend.push(o.ExtReturnID);
                if (o.IsApproved) error = true;
            }
        })
        if (error) $rootScope.Message({ Msg: 'Có dữ liệu đã được duyệt, không duyệt lại', NotifyType: Common.Message.NotifyType.ERROR });
        else {
            if (dataSend.length > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONInput_ExtReturn.URL.Approved,
                    data: { lst: dataSend },
                    success: function (res) {
                        $scope.MONInputExtReturn_GridOptions.dataSource.read();
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

    $scope.PODInputExtReturn_UnApprovedClick = function ($event, grid) {
        $event.preventDefault();
        var dataSend = [];
        var error = false;
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                dataSend.push(o.ExtReturnID);
            }
        })
        if (error) $rootScope.Message({ Msg: 'Có dữ liệu đã được duyệt, không duyệt lại', NotifyType: Common.Message.NotifyType.ERROR });
        else {
            if (dataSend.length > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONInput_ExtReturn.URL.UnApproved,
                    data: { lst: dataSend },
                    success: function (res) {
                        $scope.MONInputExtReturn_GridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $scope.MainHasChoose = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
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
                    url: Common.Services.url.MON,
                    method: _MONInput_ExtReturn.URL.Delete,
                    data: { lst: dataSend },
                    success: function (res) {
                        $scope.MONInputExtReturn_GridOptions.dataSource.read();
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.Get,
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
                _MONInput_ExtReturn.Data.ExtReturnDetailList = $scope.extReturn_Detail_GridOptions.dataSource.data();
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
                    url: Common.Services.url.MON,
                    method: _MONInput_ExtReturn.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: _MONInput_ExtReturn.URL.Get,
                            data: { ID: res },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.Item = res;
                                $scope.MONInputExtReturn_GridOptions.dataSource.read();

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
                                _MONInput_ExtReturn.Data.ExtReturnDetailList = $scope.extReturn_Detail_GridOptions.dataSource.data();

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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.CustomerList,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.VehicleList,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.DriverList,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.VendorList,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.DITOMasterList,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.GOPList,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.ProductList,
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
        method: _MONInput_ExtReturn.URL.ExtReturnStatus,
        data: {},
        success: function (res) {
            $scope.PODInput_ExtReturn_cboExtReturnStatus_Options.dataSource.data(res.Data)
            _MONInput_ExtReturn.Data.ListExtReturnStatus = res.Data;
        }
    });

    //#endregion

    $scope.extReturn_Detail_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.DetailList,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.DetailNotIn,
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
                url: Common.Services.url.MON,
                method: _MONInput_ExtReturn.URL.SaveDetail,
                data: { ExtReturnID: $scope.Item.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.extReturn_Detail_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                    $scope.MONInputExtReturn_GridOptions.dataSource.read();
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
                url: Common.Services.url.MON,
                method: _MONInput_ExtReturn.URL.SaveDetail,
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
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturn.URL.FindList,
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
            _MONInput_ExtReturn.Data.ExtReturnDetailList = $scope.extReturn_Detail_GridOptions.dataSource.data();

            win.close()
            $scope.ext_return_tabstrip.select(0);
        }
    }

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

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Index")
    };

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Input_ExtReturnExcel")
    }
}]);