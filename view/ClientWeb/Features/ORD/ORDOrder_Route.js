/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/views.js" />
/// <reference path="~/Scripts/angular.min.js" />


var _ORDOrderRoute = {
    URL: {
        Read: 'ORDOrderRoute_List',
        Get: 'ORDOrderRoute_Get',
        Save: 'ORDOrderRoute_Save',
        Delete: 'ORDOrderRoute_Delete'
    }
};

angular.module('myapp').controller('ORDOrder_RouteCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_RouteCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.ItemOrder = null;

    $scope.HasReload = false;

    $scope.HasChooose = false;

    $scope.IsStatusClosed = false;
    //var CS = $.extend({
    //    Code: 100, RouteName: 100, RouteStatusName: 100, RouteStatusOPSName: 100
    //}, true, $rootScope.GetColumnSettings('orderroute_grid'));

    $scope.orderroute_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRoute.URL.Read,
            readparam: function () { return { isClosed: $scope.IsStatusClosed } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: 90,
                template: '<a href="/" ng-click="orderRoute_DelClick($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false, sortorder: 0, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'Code', width: 150, title: 'Mã', template: "<a class='action-text' href='\' ng-click='Detail_Click($event,dataItem)'>#=Code#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 1, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'RouteName', width: 150, title: 'Tên', template: "<a class='action-text' href='\' ng-click='Detail_Click($event,dataItem)'>#=RouteName#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 2, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'RouteStatusName', width: 100, title: 'Trạng thái ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: true, isfunctionalHidden: true,
            },
            {
                field: 'RouteStatusOPSName', width: 100, title: 'Trạng thái v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 4, configurable: true, isfunctionalHidden: true,
            },
            { title: ' ', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false, }
        ],
        dataBound: function (e) {
            if ($scope.HasReload) {
                $rootScope.IsLoading = false;
                $scope.HasReload = false;
            }
        }
    }


    $scope.orderRoute_DelClick = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRoute.URL.Delete,
                    data: { id:data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.orderroute_grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.ViewStatus_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.HasReload = true;
        $scope.IsStatusClosed = !$scope.IsStatusClosed;
        grid.dataSource.read();
    }

    $scope.Detail_Click = function ($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data.ID) && data.ID != '')
            $state.go("main.ORDOrder.RouteDetail", { OrderRouteID: data.ID });
    };

    $scope.orderRoute_AddClick = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRoute.URL.Get,
            data: { id:0 },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemOrder = res;
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Save_Click = function ($event, win,vform,grid) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrderRoute.URL.Save,
                data: { item:$scope.ItemOrder },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                    grid.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }
}]);