/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingMONImport = {
    URL: {
        Read: 'CUSSettingMONImport_List',
        Get: 'CUSSettingMONImport_Get',
        Save: 'CUSSettingMONImport_Save',
        Delete: 'CUSSettingMONImport_Delete',
        SettingOrderList: 'CUSSettingMONImport_SettingOrderList',
    }
}

angular.module('myapp').controller('CUSSettingMON_InputCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingMON_InputCtrl');
    $rootScope.IsLoading = false;

    $scope.setting_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingMONImport.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    CreateDate: { type: 'date' },
                },
            }
        }),
        height: '100%', filterable: true, sortable: true, menu: false,
        filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CUSSettingMONImport_EditClick($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CUSSettingMONImport_DeleteClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>'
            },
            { field: 'Name', width: "200px", title: 'Tên thiết lập', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CreateBy', width: "200px", title: 'Người tạo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'CreateDate', width: "200px", title: 'Ngày tạo',
                template: '#=Common.Date.FromJsonDMYHM(CreateDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.CUSSettingMONImport_EditClick = function ($event, data) {
        $event.preventDefault();
        //$scope.LoadSettingMONImport(win, data.id, vform);
        $state.go("main.CUSSettingMonitor.InputDetail", { 'ID': data.ID });
    }

    
    $scope.CUSSetting_AddClick = function ($event) {
        $event.preventDefault();
        //$scope.LoadSettingMONImport(win, 0, vform);
        $state.go("main.CUSSettingMonitor.InputDetail", { 'ID': 0 });
    }

    $scope.LoadSettingMONImport = function (win, id, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingMONImport.URL.Get,
            data: { id: id },
            success: function (res) {
                $scope.SettingItem = res;
                $rootScope.IsLoading = false;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.CUSSettingMONImport_DeleteClick = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingMONImport.URL.Delete,
            data: { item: data },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.setting_gridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.SettingMONImport_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSSettingMONImport.URL.Save,
                data: { item: $scope.SettingItem },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.setting_gridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.numVehicleNo_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numMasterETDDate_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numMasterETDTime_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numMasterETDDate_Time_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numVendorCode_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numMasterNote_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }


    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#region Action

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CUSSettingMON,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    };
    //#endregion

}]);