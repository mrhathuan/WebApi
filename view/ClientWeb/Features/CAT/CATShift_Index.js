/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATShift = {
    URL: {
        Read: 'CATShift_Read',
        Delete: 'CATShift_Destroy',
        Save: 'CATShift_Update',
        Get: 'CATShift_Get',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATShift_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATShift_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATShiftItem = null

    $scope.CATShift_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATShift.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Hour: { type: 'number' },
                    TimeTo: { type: 'date' },
                    TimeFrom: { type: 'date' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode:'row'},
        columns: [
            {
                field: 'Command', title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATShiftEdit_Click($event,CATShift_win,CATShift_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATShiftDestroy_Click($event,CATShift_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATShift.Code}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 1, configurable: true, isfunctionalHidden: false, },
            { field: 'ShiftName', title: '{{RS.CATShift.ShiftName}}', width: 150, sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'TimeFrom', title: '{{RS.CATShift.TimeFrom}}', width: 150, sortorder: 3, configurable: true, isfunctionalHidden: false, template: '#=Common.Date.FromJsonHM(TimeFrom)#', filterable: { cell: { operator: 'gte', showOperators: false } }, },
            { field: 'TimeTo', title: '{{RS.CATShift.TimeTo}}', width: 150, sortorder: 4, configurable: true, isfunctionalHidden: false, template: '#=Common.Date.FromJsonHM(TimeTo)#', filterable: { cell: { operator: 'gte', showOperators: false } }, },
            { field: 'Hour', title: '{{RS.CATShift.Hour}}', sortorder: 5, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'eq', showOperators: false } }, },
            { title: '', filterable: false, sortable: false, sortorder: 6, configurable: false, isfunctionalHidden: false }
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

    $scope.CATShiftEdit_Click = function ($event, win, grid) {
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
            method: _CATShift.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATShiftItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATShiftDestroy_Click = function ($event, grid) {
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
                        method: _CATShift.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATShift_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATShift_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATShiftItem.TimeTo) && Common.HasValue($scope.CATShiftItem.TimeFrom)) {
                //debugger
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATShift.URL.Save,
                    data: { item: $scope.CATShiftItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATShift_gridOptions.dataSource.read();
                        })
                    }
                });
            }
            else $rootScope.Message({ Msg: 'Chọn lại thời gian bắt đầu và kết thúc cho chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATShift_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATShift_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATShift_numHour_Options = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0,max:24, step: 1, decimals: 2, }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);