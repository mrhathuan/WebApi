/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfService = {
    URL: {
        Read: 'CATGroupOfService_Read',
        Delete: 'CATGroupOfService_Destroy',
        Save: 'CATGroupOfService_Update',
        Get: 'CATGroupOfService_Get',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfService_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfService_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATGroupOfServiceItem = null

    $scope.CATGroupOfService_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfService.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            { title: ' ', width: 20, filterable: false, sortable: false },
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="CATGroupOfServiceEdit_Click($event,CATGroupOfService_win,CATGroupOfService_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfServiceDestroy_Click($event,CATGroupOfService_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATGroupOfService.Code}}', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfService', title: '{{RS.CATGroupOfService.GroupOfService}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
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
    $scope.CATGroupOfServiceEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;

        $scope.LoadItem(win, id);
    }

    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfService.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATGroupOfServiceItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfServiceDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATGroupOfService.URL.Delete,
                    data: { 'item': item },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.CATGroupOfService_gridOptions.dataSource.read();
                        })
                    }
                });
            }
        }
    }

    $scope.CATGroupOfService_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATGroupOfService.URL.Save,
                data: { item: $scope.CATGroupOfServiceItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATGroupOfService_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATGroupOfService_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfService_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

}]);