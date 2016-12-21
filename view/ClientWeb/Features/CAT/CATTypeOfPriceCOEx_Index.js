/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATTypeOfPriceCOEx = {
    URL: {
        Read: 'CATTypeOfPriceCOEx_List',
        Delete: 'CATTypeOfPriceCOEx_Delete',
        Save: 'CATTypeOfPriceCOEx_Save',
        Get: 'CATTypeOfPriceCOEx_Get',

    },
}

//#endregion

angular.module('myapp').controller('CATTypeOfPriceCOEx_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATTypeOfPriceCOEx_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATTypeOfPriceCOEx_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfPriceCOEx.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#CATTypeOfPriceCOEx_grid_toolbar').html()),
        columns: [
            {
                field: 'Command', title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATTypeOfPriceCOExEdit_Click($event,CATTypeOfPriceCOEx_win,CATTypeOfPriceCOEx_grid,CATTypeOfPriceCOEx_win_form)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATTypeOfPriceCOExDestroy_Click($event,CATTypeOfPriceCOEx_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeName', title: 'Tên', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.CATTypeOfPriceCOExEdit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID, vform);
    }

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfPriceCOEx.URL.Get,
            data: { 'id': id },
            success: function (res) {
                $scope.Item = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATTypeOfPriceCOExDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
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
                        method: _CATTypeOfPriceCOEx.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATTypeOfPriceCOEx_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATTypeOfPriceCOEx_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATTypeOfPriceCOEx.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $scope.CATTypeOfPriceCOEx_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.CATTypeOfPriceCOEx_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };




    $scope.CATTypeOfPriceCOExAddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0, vform);
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
   
}]);
