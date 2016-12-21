/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _CATField = {
    URL: {
        Read: 'CATField_Read',
        Get: 'CATField_Get',
        Save: 'CATField_Update',
        Delete: 'CATField_Destroy',

        ExcelInit: 'CATField_ExcelInit',
        ExcelChange: 'CATField_ExcelChange',
        ExcelImport: 'CATField_ExcelImport',
        ExcelApprove: 'CATField_ExcelApprove',
    },
    Data: {
        _gridModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                Name: { type: 'string' },
                Code: { type: 'string' },
            }
        },
    }
};
//#endregion

angular.module('myapp').controller('CATField_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATField_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATFieldItem = null;

    $scope.CATField_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATField.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="CATFieldEdit_Click($event,CATField_win,CATField_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATFieldDestroy_Click($event,CATField_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATField.Code}}', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'FieldName', title: '{{RS.CATField.FieldName}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.CATField_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATField.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATFieldItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATField_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATField.URL.Save,
                data: { item: $scope.CATFieldItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATField_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATField_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATFieldEdit_Click = function ($event, win, grid) {
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
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.CATFieldDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có muốn xóa',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATField.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATField_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }


    $scope.CATField_Excel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.excelShare.Init({
            functionkey: 'CATField_Index',
            params: { },
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATField.URL.ExcelInit,
            methodChange: _CATField.URL.ExcelChange,
            methodImport: _CATField.URL.ExcelImport,
            methodApprove: _CATField.URL.ExcelApprove,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATField_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);