/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSettingProduct = {
    URL: {
        GOPRead: 'FLMSetting_GroupOfProduct_Read',
        GOPUpdate: 'FLMSetting_GroupOfProduct_Update',
        GOPDestroy: 'FLMSetting_GroupOfProduct_Destroy',
        GOPAllRead: 'FLMSetting_GroupOfProductAll_Read',
        GOPReset: 'FLMSetting_GroupOfProduct_ResetPrice',
        GOPMappingList: 'FLMSetting_GroupOfProductMapping_List',
        GOPMappingNotinList: 'FLMSetting_GroupOfProductMappingNotIn_List',
        GOPMappingSave: 'FLMSetting_GroupOfProductMapping_SaveList',
        GOPMappingDelete: 'FLMSetting_GroupOfProductMapping_Delete',
        GOPGetByCode: 'FLMSetting_GroupOfProduct_GetByCode',
    },
    ExcelKey: {
      
    }
}

angular.module('myapp').controller('FLMSetting_ProductCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FLMSetting_ProductCtrl');
    $rootScope.IsLoading = false;

    $scope.PriceGopID = 0;
    $scope.gopID = 0;
    $scope.DetailItem = {};
    $scope.Auth = $rootScope.GetAuth();

    //#region GOP
    $scope.GOP_TreeOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingProduct.URL.GOPRead,
            readparam: function () { return {  } },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number', nullable: true },
                    PriceOfGOPName: { type: 'string' },
                    ParentName: { type: 'string' },
                    ParentID: { type: 'number' }
                },
                expanded: false
            }
        }),
        height: '99%', filterable: false, columnMenu: false, sortable: false, selectable: true, editable: false,
        change: function (e) {
            var tree = this;
            var dataItem = tree.dataItem(this.select());
            if (dataItem != null) {
                $scope.gopID = dataItem.ID;
            }
            $scope.Product_Grid.dataSource.read();
        },
        columns: [
            { title: '', width: 20, filterable: false },
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="GOP_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    ' <a href="/" ng-click="GOP_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã nhóm SP', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Tên nhóm SP', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PriceOfGOPName', title: 'Tính theo', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.PriceGopID = 0;
    $scope.GOP_Add = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingProduct.URL.GOPGetByCode,
            data: { code: ""},
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSettingProduct.URL.GOPAllRead,
                    data: { request: "", gopID: $scope.gopID },
                    success: function (res) {
                        $scope.Parent_CbbOptions.dataSource.data(res.Data);
                    }
                });
                $scope.DetailItem = res;
                $scope.DetailItem.PriceOfGOPID = $scope.PriceGopID;
                $scope.GOP_Win.center().open();
            }
        })
    }

    $scope.GOP_Edit = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        var grid = $scope.GOP_Tree;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingProduct.URL.GOPGetByCode,
            data: { code: item.Code },
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSettingProduct.URL.GOPAllRead,
                    data: { request: "", gopID: $scope.gopID },
                    success: function (res) {
                        $scope.Parent_CbbOptions.dataSource.data(res.Data);
                    }
                });
                $scope.DetailItem = res;
                $scope.GOP_Win.center().open();
            }
        })

    }

    $scope.GOP_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSettingProduct.URL.GOPUpdate,
                data: { item: $scope.DetailItem },
                success: function (res) {
                    $scope.GOP_Tree.dataSource.read();
                    $scope.GOP_Win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.GOP_Delete = function ($event) {
        $event.preventDefault();

        var grid = $scope.GOP_Tree;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSettingProduct.URL.GOPDestroy,
                    data: { item: item },
                    success: function (res) {
                        $scope.GOP_Tree.dataSource.read();
                        $rootScope.IsLoading = false;
                    }
                })
            }
        });
    }

    $scope.GOP_Reset = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingProduct.URL.GOPReset,
            data: {  },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $rootScope.IsLoading = false;
            }
        })
    }
    //#endregion

    //#region Product
    $scope.Product_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingProduct.URL.GOPMappingList,
            readparam: function () { return { groupOfProductID: $scope.gopID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ProductName: { type: 'string' },
                    PackingName: { type: 'string' },
                    IsKg: { type: 'bool' }
                }
            },
        }),
        height: '99%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        toolbar: kendo.template($('#product-grid-toolbar').html()),
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Product_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false, locked: true
            },
            { field: 'GroupOfProductName', title: 'Hàng hóa', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Khách hàng', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.PriceOfGOP_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'ValueOfVar',
        dataValueField: 'ID',
        index: 0,
        dataSource: Common.DataSource.Local({
            data: []
        })
    }

    $scope.Parent_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: false,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        index: 0,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' }
                }
            }
        }),
        dataBound: function (e) {
            //if (me._gopItemBind != null) {
            //    this.select(function (dataItem) {
            //        return dataItem.ID == me._gopItemBind.ParentID;
            //    })
            //}
        }
    }

    $scope.Product_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingProduct.URL.GOPMappingNotinList,
            readparam: function () { return { groupOfProductID: $scope.gopID } },
            model: {
                id: 'ID',
                fields: {
                    IsChoose: { type: 'boolean' },
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', filterable: { mode: 'row' }, sortable: false,

        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Product_NotinGrid,Product_NotinGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Product_NotinGrid,Product_NotinGridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfProductName', title: 'Hàng hóa', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Khách hàng', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }

    $scope.Product_Add = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.Product_NotinGrid.dataSource.read();
    }

    $scope.Product_SaveList = function ($event) {
        $event.preventDefault();

        var grid = $scope.Product_NotinGrid;
        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSettingProduct.URL.GOPMappingSave,
                data: { lst: data, groupOfProductID: $scope.gopID },
                success: function (res) {
                    $scope.Product_Grid.dataSource.read();
                    $scope.Product_NotinGrid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.Product_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Product_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSettingProduct.URL.GOPMappingDelete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Product_Grid.dataSource.read();
                    }
                })
            }
        });
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SLI_SYSVarPriceOfGOP,
        success: function (res) {
            if (Common.HasValue(res))
                $scope.PriceGopID = res[0].ID;
            $scope.PriceOfGOP_CbbOptions.dataSource.data(res)
        }
    });

    //#endregion
    //#region Action
    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FLMSetting,
            event: $event,
            current: $state.current
        });
    };
    //#endregion
    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);