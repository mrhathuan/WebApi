/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATRomooc = {
    URL: {
        Read: 'CATRomooc_Read',
        Delete: 'CATRomooc_Destroy',
        Save: 'CATRomooc_Update',
        Get: 'CATRomooc_Get',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATRomooc_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATRomooc_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATRomoocItem = null

    $scope.CATRomooc_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATRomooc.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RegNo: { type: 'string' },
                    IsOwn: { type: 'string' },
                    NoOfDelivery: { type: 'number' },
                    Lat: { type: 'number' },
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '84px',
                template: '<a href="/" ng-click="CATRomoocEdit_Click($event,CATRomooc_win,CATRomooc_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATRomoocDestroy_Click($event,CATRomooc_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'RegNo', title: '{{RS.CATRomooc.RegNo}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'IsOwn', title: '{{RS.CATRomooc.IsOwn}}', width: 150, template: '<input type="checkbox" #=IsOwn!="true"?"":checked="checked"# disabled="disabled">',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Xe nhà', Value: true }, { Text: 'Xe ngoài', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            { field: 'NoOfDelivery', title: '{{RS.CATRomooc.NoOfDelivery}}', width: 150, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Lat', title: '{{RS.CATRomooc.Lat}}', width: 150, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Lng', title: '{{RS.CATRomooc.Lng}}', width: 150, filterable: { cell: { operator: 'gte', showOperators: false } } },
        ]
    };

    $scope.CATRomoocEdit_Click = function ($event, win, grid) {
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
            method: _CATRomooc.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATRomoocItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATRomoocDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có muốn xóa không',
                Close: null,
                Ok: function () {
                    $scope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATRomooc.URL.Delete,
                        data:{'item':item},
                        success: function (res) {
                            $scope.CATRomooc_gridOptions.dataSource.read();
                            $scope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATRomooc_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATRomooc.URL.Save,
                data: { item: $scope.CATRomoocItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATRomooc_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATRomooc_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    //$scope.CATRomooc_AddNew_Click = function ($event, win, vform) {
    //    $event.preventDefault();
    //    $scope.LoadItem(win, 0);
    //}

    $scope.CATRomooc_numNoOfDelivery_Options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, max: 2, decimals: 0, }
    $scope.CATRomooc_numLat_Options = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.CATRomooc_numLng_Options = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.TypeOfReasonName,
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
}]);