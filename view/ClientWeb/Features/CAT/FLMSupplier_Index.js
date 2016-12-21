/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _FLMSupplier = {
    URL: {
        Read: 'FLMSupplier_List',
        Delete: 'FLMSupplier_Delete',
        Save: 'FLMSupplier_Save',
        Get: 'FLMSupplier_Get',

        MaterialPrice_List: 'FLMMaterialPrice_List',
        MaterialPrice_Get: 'FLMMaterialPrice_Get',
        MaterialPrice_Save: 'FLMMaterialPrice_Save',
        MaterialPrice_Delete: 'FLMaterialPrice_Delete',

        ExcelInit: 'FLMSupplier_ExcelInit',
        ExcelChange: 'FLMSupplier_ExcelChange',
        ExcelImport: 'FLMSupplier_ExcelImport',
        ExcelApprove: 'FLMSupplier_ExcelApprove',

        Get_Material: 'MaterialAll_List',
    },
    Data: {
        Country: [],
        Province: [],
        District: [],
        Ward: [],
    }
}

//#endregion

angular.module('myapp').controller('FLMSupplier_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FLMSupplier_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.SupplierItem = { ID: 0 };
    $scope.MaterialPriceItem = { ID: 0 };

    $scope.FLMSupplier_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _FLMSupplier.URL.Read,
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
                field: "Command", title: ' ', width: '84px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="FLMSupplierEdit_Click($event,FLMSupplier_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="FLMSupplierDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã nhà phân phối', width: 150,sortorder: 1, configurable: true, isfunctionalHidden: false , filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SupplierName', title: 'Tên nhà phân phối', sortorder: 2, configurable: true, isfunctionalHidden: false, width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: 180, sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.FLMSupplier_AddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.TabIndex = 0;
        $scope.LoadItem(win, 0);
    }

    $scope.FLMSupplierEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    }

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _FLMSupplier.URL.Get,
            data: { id: id },
            success: function (res) {
                $scope.CreateDataSourceComboBox('Province', res.CountryID, status)
                $scope.CreateDataSourceComboBox('District', res.ProvinceID, status)
                $scope.SupplierItem = res;
                if (res.ID > 0) {
                    $scope.materialPrice_GridOptions.dataSource.read();
                }
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        })
    }

    $scope.Station_cboCountryOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CountryName', dataValueField: 'ID', minLength: 3,
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
                $scope.CreateDataSourceComboBox('Province', cbo.value(), 'change')
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {

            $scope.Station_cboCountryOptions.dataSource.data(data);
            _CATStation.Data.Country = data;
        }
    })
    $scope.Station_cboProvinceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ProvinceName', dataValueField: 'ID', index: 0,
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
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.CreateDataSourceComboBox('District', cbo.value(), 'change')
            }
        }
    }
    $scope.Station_cboDistrictOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DistrictName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                //$scope.CreateDataSourceComboBox('Ward', cbo.value(),'change')
            }
        }
    }
    $scope.Station_cboWardOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'WardName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    WardName: { type: 'string' },
                }
            }
        })
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _FLMSupplier.Data.Province = data;
        }
    })
    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _FLMSupplier.Data.District = data;
        }
    })
    Common.ALL.Get($http, {
        url: Common.ALL.URL.Ward,
        success: function (data) {
            _FLMSupplier.Data.Ward = data;
        }
    })
    $scope.CreateDataSourceComboBox = function (target, value, status) {
        switch (target) {
            default: break;
            case 'Province': var result = $.grep(_FLMSupplier.Data.Province, function (o) { return o.CountryID == value; });

                $scope.Station_cboProvinceOptions.dataSource.data(result);
                //$scope.LocationItem.ProvinceID = null;
                if (result.length > 0) {
                    $scope.SupplierItem.ProvinceID = result[0].ID;
                    $timeout(function () {
                        $scope.Station_cboProvince.select(0)
                    }, 1)
                }
                break;
            case 'District': var result = $.grep(_FLMSupplier.Data.District, function (o) { return o.ProvinceID == value; });

                $scope.Station_cboDistrictOptions.dataSource.data(result);
                //$scope.LocationItem.DistrictID = null;
                if (result.length > 0) {
                    $scope.SupplierItem.DistrictID = result[0].ID;
                    $timeout(function () {
                        $scope.Station_cboDistrict.select(0)
                    }, 1)
                }
                break;
            case 'Ward': var result = $.grep(_FLMSupplier.Data.Ward, function (o) { return o.DistrictID == value; });
                $scope.Station_cboWardOptions.dataSource.data(result);
                //$scope.LocationItem.DistrictID = null;
                if (result.length > 0) {
                    $scope.SupplierItem.WardID = result[0].ID;
                    $timeout(function () {
                        $scope.Station_cboWard.select(0)
                    }, 1)

                } break;
        }
    }

    $scope.FLMSupplier_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _FLMSupplier.URL.Save,
                data: { item: $scope.SupplierItem },
                success: function (res) {
                    
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Ok: null,
                        close: null,
                    });
                    $scope.FLMSupplier_gridOptions.dataSource.read();
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _FLMSupplier.URL.Get,
                        data: { id: res },
                        success: function (res) {
                            
                            $scope.CreateDataSourceComboBox('Province', res.CountryID, status)
                            $scope.CreateDataSourceComboBox('District', res.ProvinceID, status)
                            $scope.SupplierItem = res;
                            $rootScope.IsLoading = false;
                            $scope.materialPrice_GridOptions.dataSource.read();
                        }
                    })
                },
                error: function () {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.FLMSupplierDestroy_Click = function ($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _FLMSupplier.URL.Delete,
                        data: { 'item': data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.FLMSupplier_gridOptions.dataSource.read();
                                $rootScope.IsLoading = false;
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.FLMSupplier_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    $scope.TabIndex = 0;
    $scope.supplier_tabstripOptions = {
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

    //#region material price
    $scope.materialPrice_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _FLMSupplier.URL.MaterialPrice_List,
            readparam: function () { return { supplierID: $scope.SupplierItem.ID} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '85px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="MaterialPrice_EditClick($event,MaterialPrice_EditWin,dataItem,FLMSupplier_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="MaterialPrice_DestroyClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { field: 'MaterialCode', title: "Mã vật tư", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaterialName', title: "Vật tư", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Price', title: "Giá", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'EffectDate', title: "Ngày hiệu lực", width: 150, template: "#=EffectDate==null?' ':Common.Date.FromJsonDMY(EffectDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false
                    }
                },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.MaterialPrice_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadMaterialPriceItem(win, 0, vform);
    }

    $scope.MaterialPrice_EditClick = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadMaterialPriceItem(win, data.ID, vform);
    }

    $scope.LoadMaterialPriceItem = function (win, id, vform) {
        vform({ clear: true });

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _FLMSupplier.URL.MaterialPrice_Get,
            data: { id: id },
            success: function (res) {
                
                $scope.MaterialPriceItem = res;
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        })
    }

    $scope.MaterialPrice_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _FLMSupplier.URL.MaterialPrice_Save,
                data: { item: $scope.MaterialPriceItem, supplierID: $scope.SupplierItem.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Ok: null,
                        close: null,
                    });
                    $scope.materialPrice_GridOptions.dataSource.read();
                    win.close();
                }
            });
        }
    }

    $scope.MaterialPrice_DestroyClick = function ($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _FLMSupplier.URL.MaterialPrice_Delete,
                        data: { 'item': data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.materialPrice_GridOptions.dataSource.read();
                                $rootScope.IsLoading = false;
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.numPrice_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0 }

    $scope.cboMaterial_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'MaterialName', dataValueField: 'MaterialID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'MaterialID',
                fields: {
                    MaterialID: { type: 'number' },
                    MaterialName: { type: 'string' },
                }
            }
        }),
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMSupplier.URL.Get_Material,
        success: function (res) {
            $scope.cboMaterial_options.dataSource.data(res.Data);
        }
    });
    //#endregion
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

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.FLMSupplier_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                '[Tên] không được trống và > 50 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'FLMSupplier_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _FLMSupplier.URL.ExcelInit,
            methodChange: _FLMSupplier.URL.ExcelChange,
            methodImport: _FLMSupplier.URL.ExcelImport,
            methodApprove: _FLMSupplier.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.FLMSupplier_grid.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);