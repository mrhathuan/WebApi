/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATCountry = {
    URL: {
        Read: 'CATCountry_Read',
        Delete: 'CATCountry_Destroy',
        Save: 'CATCountry_Update',
        Get: 'CATCountry_Get',

        ExcelInit: 'CATCountry_ExcelInit',
        ExcelChange: 'CATCountry_ExcelChange',
        ExcelImport: 'CATCountry_ExcelImport',
        ExcelApprove: 'CATCountry_ExcelApprove',
    }
}

//#endregion

angular.module('myapp').controller('CATCountry_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATCountry_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.country_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATCountry.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode:'row'},
        toolbar: kendo.template($('#country_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="CATCountryEdit_Click($event,country_winPopup,country_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATCountryDestroy_Click($event,country_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATCountry.Code}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: '{{RS.CATCountry.CountryName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.CATCountryEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID);
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

    //#endregion
    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATCountry.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CountryItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATCountryDestroy_Click = function ($event, grid) {
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
                        method: _CATCountry.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.country_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATCountrySave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATCountry.URL.Save,
                data: { item: $scope.CountryItem },
                success: function (res) {
                    win.close();
                    $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.country_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.CATCountryClose_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATCountryAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminOther.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.CATcountry_Excel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.excelShare.Init({
            functionkey: 'CATCountry_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATCountry.URL.ExcelInit,
            methodChange: _CATCountry.URL.ExcelChange,
            methodImport: _CATCountry.URL.ExcelImport,
            methodApprove: _CATCountry.URL.ExcelApprove,

            Changed: function () {

            },
            Approved: function () {
                $scope.country_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);