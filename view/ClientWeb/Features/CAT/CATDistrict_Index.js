/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATDistrict = {
    URL: {
        Read: 'CATDistrict_Read',
        Delete: 'CATDistrict_Destroy',
        Save: 'CATDistrict_Update',
        Get: 'CATDistrict_Get',

        ExcelInit: 'CATDistrict_ExcelInit',
        ExcelChange: 'CATDistrict_ExcelChange',
        ExcelImport: 'CATDistrict_ExcelImport',
        ExcelApprove: 'CATDistrict_ExcelApprove',
    },
    Data: {
        Country: [],
        Province:[]
    }
}

//#endregion

angular.module('myapp').controller('CATDistrict_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATDistrict_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATDistrict_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATDistrict.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode:'row'},
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="CATDistrictEdit_Click($event,CATDistrict_win,CATDistrict_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATDistrictDestroy_Click($event,CATDistrict_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATDistric.Code}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: '{{RS.CATDistrict.ProvinceName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: '{{RS.CATCountry.CountryName}}',
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
    $scope.CATDistrictEdit_Click = function ($event, win, grid) {
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
            method: _CATDistrict.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CreateDataSourceComboBox('Province',res.CountryID)
                $scope.CATDistrictItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATDistrictDestroy_Click = function ($event, grid) {
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
                        method: _CATDistrict.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATDistrict_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATDistrict_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATDistrictItem.CountryID) && $scope.CATDistrictItem.CountryID > 0) {
                if (Common.HasValue($scope.CATDistrictItem.ProvinceID) && $scope.CATDistrictItem.ProvinceID > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATDistrict.URL.Save,
                        data: { item: $scope.CATDistrictItem },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                win.close();
                                $scope.CATDistrict_gridOptions.dataSource.read();
                            })
                        }
                    });
                }
                else $rootScope.Message({ Msg: 'Chưa chọn tỉnh thành', NotifyType: Common.Message.NotifyType.ERROR });
            }
            else 
                $rootScope.Message({ Msg: 'Chưa chọn quốc gia', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATDistrict_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATDistrict_AddNew_Click = function ($event, win,vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATDistrict_cboCountry_Options = {
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
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.CreateDataSourceComboBox('Province', cbo.value())
                $scope.CreateDataSourceComboBox('District', $scope.LocationItem.ProvinceID)
            }
        }
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CATDistrict.Data.Country = data;
            $scope.CATDistrict_cboCountry_Options.dataSource.data(data);
        }
    })

    $scope.CATDistrict_cboProvince_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ProvinceName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATDistrict.Data.Province = [];
            Common.Data.Each(data, function (o) {
                if (!Common.HasValue(_CATDistrict.Data.Province[o.CountryID]))
                    _CATDistrict.Data.Province[o.CountryID] = [];
                _CATDistrict.Data.Province[o.CountryID].push(o);
            })
        }
    })

    $scope.CreateDataSourceComboBox = function (target, value) {
        switch (target) {
            default: break;
            case 'Province': var result = _CATDistrict.Data.Province[value]
                $scope.CATDistrict_cboProvince_Options.dataSource.data(result);

                //$scope.LocationItem.ProvinceID = null;
                if (result.length > 0) {
                    $scope.CATDistrictItem.ProvinceID = result[0].ID;
                    $timeout(function () {

                        $scope.CATDistrict_cboProvince.select(0)
                    }, 1)

                }
                break;
            case 'District': break;
        }
    }
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminOther.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.CATDistrict_Excel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.excelShare.Init({
            functionkey: 'CATDistrict_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATDistrict.URL.ExcelInit,
            methodChange: _CATDistrict.URL.ExcelChange,
            methodImport: _CATDistrict.URL.ExcelImport,
            methodApprove: _CATDistrict.URL.ExcelApprove,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATDistrict_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);