/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _WFLSetting_Define = {
    URL: {
        Read: 'WFLSetting_DefineRead',
        Get: 'WFLSetting_DefineGet',
        Save: 'WFLSetting_DefineSave',
        Delete: 'WFLSetting_DefineDelete',
    },
    Data: {
    }
};
//#endregion

angular.module('myapp').controller('WFLSetting_DefineCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLSetting_DefineCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = null;
    $scope.WFLSetting_Define_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Define.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-click="Edit_Click($event,WFLSetting_Define_win,dataItem,WFLSetting_Define_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' + 
                    '<a href="/" ng-click="Detail_Click($event,dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DefineName', title: 'Tên định nghĩa',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.Edit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID, vform);
    };

    $scope.Detail_Click = function ($event, data) {
        $event.preventDefault();
        $state.go('main.WFLSetting.DefineDetail', {
            ID: data.ID
        });
    };

    $scope.Delete_Click = function ($event, data) {
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
                    url: Common.Services.url.WFL,
                    method: _WFLSetting_Define.URL.Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.WFLSetting_Define_gridOptions.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        })
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Define.URL.Get,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.Item = res;
                    }, 1);
                }
            }
        });

        win.center();
        win.open();
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_Define.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.WFLSetting_Define_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }

    };

    $scope.Detail_Click = function ($event, data) {
        $event.preventDefault();debugger
        $state.go("main.WFLSetting.DefineDetail", { ID: data.ID });
    }
    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //#region action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.WFLSetting,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion
}]);