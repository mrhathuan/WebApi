/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATProvince = {
    URL: {
        Read: 'CATProvince_Read',
        Delete: 'CATProvince_Destroy',
        Save: 'CATProvince_Update',
        Get: 'CATProvince_Get',

        ExcelInit: 'CATProvince_ExcelInit',
        ExcelChange: 'CATProvince_ExcelChange',
        ExcelImport: 'CATProvince_ExcelImport',
        ExcelApprove: 'CATProvince_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATProvince_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATProvince_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATProvince_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATProvince.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="CATProvinceEdit_Click($event,CATProvince_win,CATProvince_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATProvinceDestroy_Click($event,CATProvince_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATProvince.Code}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: '{{RS.CATProvince.CountryName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };
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
    $scope.CATProvinceEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID);
    }

    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATProvince.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATProvinceItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATProvinceDestroy_Click = function ($event, grid) {
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
                        method: _CATProvince.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATProvince_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATProvince_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATProvinceItem.CountryID) && $scope.CATProvinceItem.CountryID > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATProvince.URL.Save,
                        data: { item: $scope.CATProvinceItem },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                win.close();
                                $scope.CATProvince_gridOptions.dataSource.read();
                            })
                        }
                    });
            }
            else
                $rootScope.Message({ Msg: 'Chưa chọn quốc gia', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATProvince_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATProvince_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATProvince_cboCountry_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CountryName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CATProvince.Data.Country = data;
            $scope.CATProvince_cboCountry_Options.dataSource.data(data);
        }
    })

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminOther.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.CATProvince_Excel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.excelShare.Init({
            functionkey: 'CATProvince_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATProvince.URL.ExcelInit,
            methodChange: _CATProvince.URL.ExcelChange,
            methodImport: _CATProvince.URL.ExcelImport,
            methodApprove: _CATProvince.URL.ExcelApprove,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATProvince_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);