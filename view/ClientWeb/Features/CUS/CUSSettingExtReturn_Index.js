/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingExtReturn = {
    URL: {
        Read: 'CUSSettingExtReturn_List',
        Delete: 'CUSSettingExtReturn_Delete'
    }
}

angular.module('myapp').controller('CUSSettingExtReturn_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingExtReturn_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.setting_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingExtReturn.URL.Read,
            model: {
                id: 'SettingID',
                fields: {
                    SettingID: { type: 'number', editable: false, nullable: true },
                    CreateDate: { type: 'date' },
                },
            }
        }),
        height: '100%', filterable: true, sortable: true, menu: false,
        filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CUSSetting_EditClick($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CUSSetting_DeleteClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>'
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

    $scope.CUSSetting_EditClick = function ($event, data) {
        $event.preventDefault();
        $state.go("main.CUSSettingMonitor.ExtReturnDetail", { id: data.SettingID })
    }

    $scope.CUSSetting_DeleteClick = function ($event, data) {
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
                    url: Common.Services.url.CUS,
                    method: _CUSSettingExtReturn.URL.Delete,
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
        })


    }

    $scope.AddNew_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.CUSSettingMonitor.ExtReturnDetail", { id: 0 })
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