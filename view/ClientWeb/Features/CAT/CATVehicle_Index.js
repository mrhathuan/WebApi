/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATVehicle = {
    URL: {
        Read: 'CATVehicle_Read',
        Delete: 'CATVehicle_Destroy',
        Save: 'CATVehicle_Update',
        Get: 'CATVehicle_Get',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATVehicle_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATVehicle_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATVehicleItem = null

    $scope.CATVehicle_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATVehicle.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                    IsFuel: { type: 'boolean' },
                    RegCapacity: { type: 'number' },
                    MinCapacity: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    ID: { type: 'number' },
                    ID: { type: 'number' },
                    ID: { type: 'number' },
                    ID: { type: 'number' },
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px',
                template: '<a href="/" ng-click="CATVehicleEdit_Click($event,CATVehicle_win,CATVehicle_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATVehicleDestroy_Click($event,CATVehicle_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'RegNo', title: '{{RS.CATVehicle.RegNo}}', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfVehicleName', title: '{{RS.CATVehicle.TypeOfVehicleName}}', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                title: 'Khả năng chở', headerAttributes: { style: "text-align: center" },
                columns: [
                    { field: 'RegCapacity', title: '{{RS.CATVehicle.RegCapacity}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                    { field: 'MinCapacity', title: '{{RS.CATVehicle.MinCapacity}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                    { field: 'MaxCapacity', title: '{{RS.CATVehicle.MaxCapacity}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                ]
            },
            {
                title: 'Trọng tải', headerAttributes: { style: "text-align: center" },
                columns: [
                    { field: 'RegWeight', title: '{{RS.CATVehicle.RegWeight}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                    { field: 'MinWeight', title: '{{RS.CATVehicle.MinWeight}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                    { field: 'MaxWeight', title: '{{RS.CATVehicle.MaxWeight}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                ]
            },
            { field: 'Lat', title: '{{RS.CATVehicle.Lat}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Lng', title: '{{RS.CATVehicle.Lng}}', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'GPSCode', title: '{{RS.CATVehicle.GPSCode}}', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorCode', title: '{{RS.CATVehicle.VendorCode}}', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', title: '{{RS.CATVehicle.Note}}', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.CATVehicleEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;

        $scope.LoadItem(win, id);
    }
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CATVehicle,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion
    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATVehicle.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATVehicleItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATVehicleDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có muốn xóa không',
                Close: null,
                Ok: function () {
                    $scope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATVehicle.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATVehicle_gridOptions.dataSource.read();
                            $scope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATVehicle_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATVehicleItem.TypeOfVehicleID) && $scope.CATVehicleItem.TypeOfVehicleID > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATVehicle.URL.Save,
                    data: { item: $scope.CATVehicleItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATVehicle_gridOptions.dataSource.read();
                        })
                    }
                });
            }
            else
                $rootScope.Message({ Msg: 'Chưa chọn loại xe', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATVehicle_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATVehicle_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATVehicle_cboTypeVehicle_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ValueOfVar',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfVehicle,
        success: function (data) {
            $scope.CATVehicle_cboTypeVehicle_Options.dataSource.data(data);
        }
    })

    $scope.CATVehicle_cboGroupVehicle_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
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
        }
    }
    Common.ALL.Get($http, {
        url: Common.ALL.URL.GroupOfVehicle,
        success: function (data) {
            $scope.CATVehicle_cboGroupVehicle_Options.dataSource.data(data);
        }
    })

    $scope.CATVehicle_numRegCapacity_Options = { format: '#,##0,00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.CATVehicle_numMinCapacity_Options = { format: '#,##0,00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.CATVehicle_numMaxCapacity_Options = { format: '#,##0,00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.CATVehicle_numRegWeight_Options = { format: '#,##0,00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.CATVehicle_numMinWeight_Options = { format: '#,##0,00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.CATVehicle_numMaxWeight_Options = { format: '#,##0,00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }

    $scope.CATVehicle_numLat_Options = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.CATVehicle_numLat_Options = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);