/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _FINRefresh_ManualFix = {
    URL: {
        Read: 'FINManualFix_List',
        Save: 'FINManualFix_Save',
        Delete:'FINManualFix_Delete',

        GetChooseList: 'FINManualFix_ChooseList',
        SaveChooseList: 'FINManualFix_SaveList',
    },
    Data: {
        ExtReturnDetailList: []
    }
}

//#endregion

angular.module('myapp').controller('FINRefresh_ManualFixCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FINRefresh_ManualFixCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.manualFixChooseHasChoose = false;

    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    };
    $scope.HasSearch = false;

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
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
            $scope.FINManualFix_GridOptions.dataSource.read();
        }
    };

    $scope.FINManualFix_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_ManualFix.URL.Read,
            readparam: function () {
                return {
                    'pDateFrom': $scope.ItemSearch.DateFrom,
                    'pDateTo': $scope.ItemSearch.DateTo,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    MasterCode: { type: 'string', editable: false },
                    VehicleNo: { type: 'string', editable: false },
                    VendorCode: { type: 'string', editable: false },
                    OrderCode: { type: 'string', editable: false },
                    GroupProductCode: { type: 'string', editable: false },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    UnitPrice: { type: 'number' },
                    Credit: { type: 'number' },
                    Debit: { type: 'number' },
                    Note: { type: 'string' },
                    DNCode: { type: 'string' },
                    SOCode: { type: 'string' },
                    LocationToCode: { type: 'string' },
                    LocationToName: { type: 'string' },
                    LocationFromCode: { type: 'string' },
                    LocationFromName: { type: 'string' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ATD: { type: 'date' },
                    ATA: { type: 'date' },
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="FINRefreshManualFix_SaveClick($event,FINManualFix_Grid)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>' +
                            '<a href="/" ng-click="FINRefreshManualFix_DeleteClick($event,FINManualFix_Grid)" class="k-button" data-title="Xóa"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false, locked: true,
            },
            { field: 'MasterCode', width: "110px", title: 'Chuyến', filterable: { cell: { operator: 'contains', showOperators: false } }, locked: true, },
            { field: 'VehicleNo', width: "110px", title: 'Xe', filterable: { cell: { operator: 'contains', showOperators: false } }, locked: true, },
            { field: 'CustomerCode', width: "110px", title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorCode', width: "110px", title: 'Vendor', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: "110px", title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: "110px", title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: "110px", title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: "110px", title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: "110px", title: 'Mã điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: "110px", title: 'Tên điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: "110px", title: 'Mã điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: "110px", title: 'Tên điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            {
                field: 'ATD', width: '130px', title: 'ATD', template: "#=ATD==null?' ':Common.Date.FromJsonDMYHM(ATD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            {
                field: 'ATA', width: '130px', title: 'ATA', template: "#=ATA==null?' ':Common.Date.FromJsonDMYHM(ATA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            {
                field: 'Ton', width: "120px", title: 'Số Tấn', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Ton_Options" ng-model="dataItem.Ton" ng-keydown="QuickSave($event,FINManualFix_Grid)" style="width: 100%"></input>',
            },
            {
                field: 'CBM', width: "120px", title: 'Số Khối', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="CBM_Options" ng-model="dataItem.CBM" ng-keydown="QuickSave($event,FINManualFix_Grid)" style="width: 100%"></input>',
            },
            {
                field: 'Quantity', width: "110px", title: 'Số lượng', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Quantity_Options"  ng-model="dataItem.Quantity"  ng-keydown="QuickSave($event,FINManualFix_Grid)" style="width: 100%"></input>',
            },
            {
                field: 'UnitPrice', width: "110px", title: 'Đơn giá', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Price_Options"  ng-model="dataItem.UnitPrice"  ng-keydown="QuickSave($event,FINManualFix_Grid)" style="width: 100%"></input>',
            },
            {
                field: 'Credit', width: "110px", title: 'Thu', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Credit_Options"  ng-model="dataItem.Credit"  ng-keydown="QuickSave($event,FINManualFix_Grid)" style="width: 100%"></input>',
            },
            {
                field: 'Debit', width: "110px", title: 'Chi', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Debit_Options"  ng-model="dataItem.Debit"  ng-keydown="QuickSave($event,FINManualFix_Grid)" style="width: 100%"></input>',
            },
            {
                field: 'Note', width: "150px",
                title: 'Ghi chú', template: '<input  class="k-textbox "  ng-model="dataItem.Note"  ng-keydown="QuickSave($event,FINManualFix_Grid)" style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
            
        }
    }

    $scope.Ton_Options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3 }
    $scope.CBM_Options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3 }
    $scope.Quantity_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0 }
    $scope.Price_Options = { format: 'n0', spinners: false, culture: 'en-US', step: 1, decimals: 0 }
    $scope.Credit_Options = { format: 'n0', spinners: false, culture: 'en-US', step: 1, decimals: 0 }
    $scope.Debit_Options = { format: 'n0', spinners: false, culture: 'en-US', step: 1, decimals: 0 }

    $scope.QuickSave = function ($event, grid) {

        if ($event.which === 13) {
            var tr = $($event.target).closest('tr');
            var item = grid.dataItem(tr);
            if (Common.HasValue(item)) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINRefresh_ManualFix.URL.Save,
                    data: { 'item': item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.FINManualFix_GridOptions.dataSource.read();
                    }
                })
            }
        }
    }

    $scope.FINRefreshManualFix_SaveClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FIN,
                method: _FINRefresh_ManualFix.URL.Save,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.FINManualFix_GridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            })
        }
    }
    $scope.FINRefreshManualFix_DeleteClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.IsLoading=true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINRefresh_ManualFix.URL.Delete,
                    data: { 'item': item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.FINManualFix_GridOptions.dataSource.read();
                    },
                    error:function(res){
                        $rootScope.IsLoading=false;
                    }
                });
            }
        

    }
    //#region Choose List
    $scope.manual_fix_Choose_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_ManualFix.URL.GetChooseList,
            readparam: function () {
                return {
                    'pDateFrom': $scope.ItemSearch.DateFrom,
                    'pDateTo': $scope.ItemSearch.DateTo,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    MasterCode: { type: 'string', editable: false },
                    VehicleNo: { type: 'string', editable: false },
                    VendorCode: { type: 'string', editable: false },
                    OrderCode: { type: 'string', editable: false },
                    GroupProductCode: { type: 'string', editable: false },
                    Ton: { type: 'number', editable: false },
                    CBM: { type: 'number', editable: false },
                    Quantity: { type: 'number', editable: false },
                    UnitPrice: { type: 'number' },
                    Credit: { type: 'number' },
                    Debit: { type: 'number' },
                    Note: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                    DNCode: { type: 'string' },
                    SOCode: { type: 'string' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ATD: { type: 'date' },
                    ATA: { type: 'date' },
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,manual_fix_Choose_Grid,manual_fix_Choose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,manual_fix_Choose_Grid,manual_fix_Choose_Change)" />',
                filterable: false, sortable: false, locked: true,
            },
            { field: 'MasterCode', width: "110px", title: 'Chuyến', filterable: { cell: { operator: 'contains', showOperators: false } }, locked: true, },
            { field: 'VehicleNo', width: "110px", title: 'Xe', filterable: { cell: { operator: 'contains', showOperators: false } }, locked: true, },
            { field: 'CustomerCode', width: "110px", title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorCode', width: "110px", title: 'Vendor', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: "110px", title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: "110px", title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: "110px", title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: "110px", title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: "110px", title: 'Mã điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: "110px", title: 'Tên điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: "110px", title: 'Mã điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: "110px", title: 'Tên điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            {
                field: 'ATD', width: '130px', title: 'ATD', template: "#=ATD==null?' ':Common.Date.FromJsonDMYHM(ATD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            {
                field: 'ATA', width: '130px', title: 'ATA', template: "#=ATA==null?' ':Common.Date.FromJsonDMYHM(ATA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'gte', showOperators: false,
                    }
                },
            },
            { field: 'Ton', width: "110px", title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: "110px", title: 'Khối', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: "110px", title: 'Số lượng', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'UnitPrice', width: "110px", title: 'Đơn giá', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Price_Options"  ng-model="dataItem.UnitPrice" style="width: 100%"></input>',
            },
            {
                field: 'Credit', width: "110px", title: 'Thu', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Credit_Options"  ng-model="dataItem.Credit" style="width: 100%"></input>',
            },
            {
                field: 'Debit', width: "110px", title: 'Chi', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Debit_Options"  ng-model="dataItem.Debit" style="width: 100%"></input>',
            },
            {
                field: 'Note', width: "150px",
                title: 'Ghi chú', template: '<input  class="k-textbox "  ng-model="dataItem.Note" style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {

        }
    }

    $scope.manual_fix_Choose_Change = function ($event, grid, hasChoose) {
        $scope.manualFixChooseHasChoose = hasChoose;
    }

    $scope.manualFix_Choose_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FIN,
                method: _FINRefresh_ManualFix.URL.SaveChooseList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.FINManualFix_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.FINRefreshMaunalFix_AddNewClick = function ($event, win) {
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
            win.center().open();
            $scope.manual_fix_Choose_GridOptions.dataSource.read();
        }
    }
    //#endregion
    $scope.IsFullScreen = false;
    $scope.Zoom_Click = function ($event) {
        $event.preventDefault();
        $scope.IsFullScreen = true;

        $scope.manual_fix_ChooseList_win.setOptions({ draggable: false });
        $scope.manual_fix_ChooseList_win.maximize();
        $scope.manual_fix_ChooseList_win.center().open();
    }

    $scope.Minimize_Click = function ($event) {
        $event.preventDefault();
        $scope.IsFullScreen = false;
        $scope.manual_fix_ChooseList_win.setOptions({ draggable: true });
        $scope.manual_fix_ChooseList_win.center().open();
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //#region Action

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FINManualFix,
            event: $event,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    };
    //#endregion

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FINManualFix.Excel");
    }
}]);