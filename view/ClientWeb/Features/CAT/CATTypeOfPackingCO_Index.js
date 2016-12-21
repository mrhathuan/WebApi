/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATTypeOfPackingCO = {
    URL: {
        Read: 'ContainerPacking_List',
        Delete: 'ContainerPacking_Delete',
        Save: 'ContainerPacking_Save',
        Get: 'ContainerPacking_Get',
        List_CO: 'CATPackingCO_List',
       
    },
}

//#endregion

angular.module('myapp').controller('CATTypeOfPackingCO_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATTypeOfPackingCO_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATPackingCOItem = null
    $scope.IsEdit = false;

    $scope.CATPackingCO_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfPackingCO.URL.Read,
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
                title: ' ', width: '100px',
                template: '<a href="/" ng-click="CATPackingCO_Click($event,CATPackingCO_win,CATPackingCO_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATPackingCODestroy_Click($event,CATPackingCO_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATPackingCO.Code}}', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PackingName', title: '{{RS.CATPackingCO.PackingName}}', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPackageName', title: '{{RS.CATPackingCO.TypeOfPackageName}}', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    };


    $scope.CATPackingCO_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminOperation.Index");
    }

    $scope.CATPackingCO_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.IsEdit = false;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfPackingCO.URL.Get,
            data: { id: 0 },
            success: function (res) {
                $scope.CATPackingCOItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATPackingCO_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATTypeOfPackingCO.URL.Save,
                data: { item: $scope.CATPackingCOItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATPackingCO_grid.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATPackingCO_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.IsEdit = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfPackingCO.URL.Get,
            data: { id: id },
            success: function (res) {
                $scope.CATPackingCOItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATPackingCODestroy_Click = function ($event, grid) {
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
                        method: _CATTypeOfPackingCO.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATPackingCO_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATPackingCO_cboList_Options = {
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

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATTypeOfPackingCO.URL.List_CO,

        success: function (data) {

            $scope.CATPackingCO_cboList_Options.dataSource.data(data.Data);
        }
    });
   
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
}]);