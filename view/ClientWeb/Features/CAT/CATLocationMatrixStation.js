/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATLocationMatrixStation = {
    URL: {
        Read: 'CATLocationMatrixStation_List',
        Get: 'CATLocationMatrixStation_Get',
        Save: 'CATLocationMatrixStation_Save',
        Delete: 'CATLocationMatrixStation_Delete',

        LocationMatrixList: 'CATLocationMatrixStation_LocationMatrixList',
        LocationList: 'CATLocationMatrixStation_LocationList',
    },
}

angular.module('myapp').controller('CATLocationMatrixStationCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATLocationMatrixStationCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.data = {};
    $scope.CATLocationMatrixStation_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrixStation.URL.Read,
            readparam: function () { },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    KM: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row', visible: false },
        columns: [
            {
                title: ' ', width: '85px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CATLocationMatrixStation_EditClick($event,CATLocationMatrixStation_EditWin,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATLocationMatrixStation_DestroyClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { field: 'LocationMatrixFromCode', title: 'Mã trạm đi', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationMatrixFromName', title: 'Tên', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationMatrixFromAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationMatrixToCode', title: 'Mã trạm đến', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationMatrixToName', title: 'Tên', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationMatrixToAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationCode', title: 'Mã điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'IsChecked', title: '', width: 150, template: "<input type='checkbox' ng-model='dataItem.IsChecked' class='checkbox' disabled/>", filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'KM', title: 'km', filterable: { cell: { operator: 'lte', showOperators: false } } },
        ]
    };
    
    $scope.LocationMatrixList_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrixStation.URL.LocationMatrixList,
            readparam: function () { },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, selectable: "row", filterable: { mode: 'row', visible: false },
        columns: [
            { field: 'LocationFromCode', title: 'Mã trạm đi', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', title: 'Tên', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', title: 'Mã trạm đến', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', title: 'Tên', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.LocationList_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrixStation.URL.LocationList,
            readparam: function () { },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, selectable: "row", filterable: { mode: 'row', visible: false },
        columns: [
            { field: 'LocationCode', title: 'Mã điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerCode', title: 'PartnerCode', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', title: 'PartnerName', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.CATLocationMatrixStation_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.data = {};
        $scope.data.KM = 0;
        win.center().open();
    }

    $scope.CATLocationMatrixStation_Save = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrixStation.URL.Save,
                data: { item: $scope.item},
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.CATLocationMatrixStation_Grid_Options.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }


    $scope.CATLocationMatrixStation_DestroyClick = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrixStation.URL.Delete,
            data: { item: data },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.CATLocationMatrixStation_Grid_Options.dataSource.read();
            }
        });
    };


    $scope.CATLocationMatrixStation_EditClick = function ($event, win, data) {
        $event.preventDefault();
        $scope.data = data;
        win.center().open();
    };

    $scope.LocationMatrixList_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LocationMatrixList_Grid.dataSource.read();
        $timeout(function () {
            $scope.LocationMatrixList_Grid.resize();
        });
        win.center().open();
    };

    $scope.LocationList_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LocationList_Grid.dataSource.read();
        $timeout(function () {
            $scope.LocationList_Grid.resize();
        });
        win.center().open();
    };

    $scope.LocationMatrixList_Save = function ($event, grid, win) {
        $event.preventDefault();
        var itemSelect = grid.dataItem(grid.select());
        if (Common.HasValue(itemSelect)) {
            $scope.data.LocationMatrixID = itemSelect.ID;
            $scope.data.LocationMatrixFromID = itemSelect.LocationFromID;
            $scope.data.LocationMatrixFromCode = itemSelect.LocationFromCode;
            $scope.data.LocationMatrixFromName = itemSelect.LocationFromName;
            $scope.data.LocationMatrixFromAddress = itemSelect.LocationFromAddress;
            $scope.data.LocationMatrixToID = itemSelect.LocationToID;
            $scope.data.LocationMatrixToCode = itemSelect.LocationToCode;
            $scope.data.LocationMatrixToName = itemSelect.LocationToName;
            $scope.data.LocationMatrixToAddress = itemSelect.LocationToAddress;
        }
        win.close();
    };

    $scope.LocationList_Save = function ($event, grid, win) {
        $event.preventDefault();
        var itemSelect = grid.dataItem(grid.select());
        if (Common.HasValue(itemSelect))
        {
            $scope.data.LocationID = itemSelect.ID;
            $scope.data.LocationCode = itemSelect.LocationCode;
            $scope.data.LocationName = itemSelect.LocationName;
            $scope.data.LocationAddress = itemSelect.LocationAddress;
        }
        win.close();
    };

    $scope.CATLocationMatrixStation_Save = function($event, win)
    {
        $event.preventDefault();
        if ($scope.data.LocationMatrixID > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrixStation.URL.Save,
                data: { item: $scope.data },
                success: function (res) {
                    $scope.CATLocationMatrixStation_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                }
            });
        }
        else {
            $rootScope.Message({
                Msg: 'Chưa chọn trạm.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
        
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.CATLocationMatrix,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    };
}]);