/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATCurrency = {
    URL: {
        Read: 'CATCurrency_List',
        Delete: 'CATCurrency_Delete',
        Save: 'CATCurrency_Save',
        Get: 'CATCurrency_Get',

        ExcelInit: 'CATCurrency_ExcelInit',
        ExcelChange: 'CATCurrency_ExcelChange',
        ExcelImport: 'CATCurrency_ExcelImport',
        ExcelApprove: 'CATCurrency_ExcelApprove',
    }
}

//#endregion

angular.module('myapp').controller('CATCurrency_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATCurrency_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = null;

    $scope.currency_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATCurrency.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CurrencyName: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#currency_grid_toolbar').html()), editable: 'inline',
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Edit_Click($event,win,currency_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Destroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATCurrency.Code}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CurrencyName', title: '{{RS.CATCurrency.CurrencyName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView:[],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
    $scope.Edit_Click = function ($event, win, grid) {
        $event.preventDefault();
        Common.Log("Edit_Click");

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item))
            $scope.LoadItem(win, item.ID);
    }

    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATCurrency.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.Item = res;
                win.center().open();
            }
        });
    }

    $scope.Destroy_Click = function ($event, item) {
        $event.preventDefault();
        Common.Log("Destroy_Click");

        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATCurrency.URL.Delete,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.Message({ Msg: "Đã xóa!", NotifyType:Common.Message.NotifyType.INFO });
                    $scope.currency_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATCurrency.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!", NotifyType:Common.Message.NotifyType.SUCCESS });
                    $scope.currency_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.AddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminOther.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.CATcurrency_Excel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.excelShare.Init({
            functionkey: 'CATCurrency_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATCurrency.URL.ExcelInit,
            methodChange: _CATCurrency.URL.ExcelChange,
            methodImport: _CATCurrency.URL.ExcelImport,
            methodApprove: _CATCurrency.URL.ExcelApprove,

            Changed: function () {

            },
            Approved: function () {
                $scope.currency_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);