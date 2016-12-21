/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATLocationMatrix = {
    URL: {
        List: 'CATLocationMatrix_List',
        Save: 'CATLocationMatrix_Save',
        Get: 'CATLocationMatrix_Get',
        Delete: 'CATLocationMatrix_Delete',
        Generate: 'CATLocationMatrix_Generate',
        SaveList: 'CATLocationMatrix_SaveList',
        GenerateByList: 'CATLocationMatrix_GenerateByList',
        GenerateLimit: 'CATLocationMatrix_GenerateLimit',

        LocationList: 'CATLocation_Read',

        Detail_List: 'CATLocationMatrixDetail_List',
        Detail_Save: 'CATLocationMatrixDetail_Save',
        Detail_Get: 'CATLocationMatrixDetail_Get',
        Detail_Detele: 'CATLocationMatrixDetail_Detele',

        DetailStation_List: 'CATLocationMatrixDetailStation_List',
        DetailStationNotIn_SaveList: 'CATLocationMatrixDetailStationNotIn_SaveList',
        DetailStation_SaveList: 'CATLocationMatrixDetailStation_SaveList',
        DetailStation_DeleteList: 'CATLocationMatrixDetailStation_DeleteList',
        DetailStation_LocationList: 'CATLocationMatrixDetailStation_LocationList',

        ExcelInit: 'CATLocationMatrix_ExcelInit',
        ExcelChange: 'CATLocationMatrix_ExcelChange',
        ExcelImport: 'CATLocationMatrix_ExcelImport',
        ExcelApprove: 'CATLocationMatrix_ExcelApprove',

        GenerateFromOPS: 'CATLocationMatrix_CreateFromOPS',
    },
    Data: {
        ExcelKey: {
            CATLocationMatrix: "CATLocationMatrix",
            CATLocationMatrixRes: "CATLocationMatrix",
        }
    }
}

//#endregion

angular.module('myapp').controller('CATLocationMatrix_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', 'openMap', '$timeout', function ($rootScope, $scope, $http, $location, $state, openMap, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATLocationMatrix_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.LocationMatrix = {ID: 0};
    $scope.HasChoose = false;
    $scope.LocationMatrixGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.List,
            readparam: function () { return {} },
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '80px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,LocationMatrixGrid,LocationMatrix_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,LocationMatrixGrid,LocationMatrix_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: 100,
                template: "<a  href='/' class='k-button' ng-click='LocationMatrix_Edit_Click($event,dataItem,LocationMatrix_win)' ><i class='fa fa-pencil'></i></a>"+
                    '<a href="/" ng-click="LocationMatrix_Delete_Click($event,dataItem,LocationMatrixGrid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'LocationFromCode', width: '200px', title: 'Mã địa chỉ lấy',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '200px', title: 'Tên địa chỉ lấy',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: '200px', title: 'Địa chỉ lấy',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromLat', width: '200px', title: 'Vĩ độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromLng', width: '200px', title: 'Kinh độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', width: '200px', title: 'Mã địa chỉ giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '200px', title: 'Tên địa chỉ giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToLat', width: '200px', title: 'Vĩ độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToLng', width: '200px', title: 'Kinh độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', width: '200px', title: 'km',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Hour', width: '200px', title: 'giờ',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.LocationGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', resizable: true, pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false,
        columns: [
            {
                field: 'LocationFromCode', width: '200px', title: 'Mã địa chỉ giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '200px', title: 'Tên địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromLat', width: '200px', title: 'Vĩ độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromLng', width: '200px', title: 'Kinh độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             {
                 field: 'LocationToCode', width: '200px', title: 'Mã địa chỉ nhận',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
            {
                field: 'LocationToAddress', width: '200px', title: 'Tên địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToLat', width: '200px', title: 'Vĩ độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToLng', width: '200px', title: 'Kinh độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', width: '200px', title: 'km',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Hour', width: '200px', title: 'giờ',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    //map
    openMap.Create({
        Element: 'map',
        Tooltip_Show: true,
        Tooltip_Element: 'map_tooltip',
        InfoWin_Show: false,
        ClickMarker: null,
        ClickMap: null
    });
    //

    $scope.LocationMatrix_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.CATLocationMatrix_CalReturn_Click = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true) {
                lstid.push(v);
            }
        });
        $scope.LocationGrid_Options.dataSource.data(lstid);
        $timeout(function () {
            $rootScope.IsLoading = false;
            $scope.LocationGrid.resize();
        }, 1);
        win.center().open();
    };

    $scope.LocationMatrix_Edit_Click = function ($event, data, win) {
        $rootScope.IsLoading = true;
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.Get,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.LocationMatrix = res;
                win.center().open();
                $scope.Detail_GridOptions.dataSource.read();
            }
        });
    };

    $scope.LocationMatrix_Delete_Click = function ($event, data,grid) {
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
                    url: Common.Services.url.CAT,
                    method: _CATLocationMatrix.URL.Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        grid.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.CATLocationMatrix_CreateRoute_Click = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn có muốn tạo tất cả cung đường không?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATLocationMatrix.URL.Generate,
                    data: {},
                    success: function (res) {
                        $scope.LocationMatrixGrid_Options.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                    }
                });
            }
        })
    };

    $scope.LocationMatrix_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrix.URL.Save,
                data: { item: $scope.LocationMatrix },
                success: function (res) {
                    $scope.LocationMatrixGrid_Options.dataSource.read();
                    win.close();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                }
            });
        }
    };

    $scope.LocationMatrix_Location_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.SaveList,
            data: { item: grid.dataSource.data() },
            success: function (res) {
                $scope.LocationMatrixGrid.dataSource.read();
                win.close();
                $rootScope.IsLoading = false;
                $scope.HasChoose = false;
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
            }
        });
    };

    //#region Matrix
    $scope.Matrix_TabIndex = 0;
    $scope.Matrix_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.Matrix_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.Matrix_TabIndex);
            }, 1)
        }
    }

    $scope.Detail_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.Detail_List,
            readparam: function () { return { locationMatrixID: $scope.LocationMatrix.ID } },
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="DetailEdit_Click($event,detail_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="DetailDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Name', width: '200px', title: 'Tên',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'KM', width: '200px', title: 'km',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Hour', width: '200px', title: 'giờ',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.Detail_AddNew = function ($event, win) {
        $event.preventDefault();
        $scope.LoadDetailItem(win, 0);
    }

    $scope.DetailEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadDetailItem(win, data.ID);
    }
    $scope.Detail = { ID: 0 };

    $scope.LoadDetailItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.Detail_Get,
            data: { 'id': id },
            success: function (res) {
                $scope.Detail = res;
                win.center();
                win.open();
                $scope.Station_GridOptions.dataSource.read();
            }
        });
    }

    $scope.DetailDestroy_Click = function ($event, dataItem) {
        $event.preventDefault();
        if (Common.HasValue(dataItem)) {
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
                        method: _CATLocationMatrix.URL.Detail_Detele,
                        data: { 'id': dataItem.ID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.Detail_GridOptions.dataSource.read();
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.Detail_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $scope.Detail.LocationMatrixID = $scope.LocationMatrix.ID;
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrix.URL.Detail_Save,
                data: { item: $scope.Detail },
                success: function (res) {
                    $scope.Detail_GridOptions.dataSource.read();
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATLocationMatrix.URL.Detail_Get,
                        data: { 'id': res },
                        success: function (res) {
                            $scope.Detail = res;
                            win.center();
                            win.open();
                            $scope.Station_GridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                        }
                    });
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                }
            });
        }
    }
    //#region Station
    $scope.num_Options = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.Station_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.DetailStation_List,
            readparam: function () { return { locationMaxtrixDetailID: $scope.Detail.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    KM: { type: 'number' },
                    LocationCode: { type: 'string', editable: false },
                    LocationName: { type: 'string', editable: false },
                    LocationAddress: { type: 'string', editable: false },
                    IsChoose: { type: 'boolean', },
                    IsChecked: { type: 'string', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row', visible: false }, editable: 'incell',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Station_Grid,station_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Station_Grid,station_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'LocationCode', title: 'Mã điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'IsChecked', title: '', width: 80,
                template: '<input type="checkbox" #= IsChecked=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: false
            },
            {
                field: 'KM', title: 'KM', filterable: { cell: { operator: 'lte', showOperators: false } },
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box style="width:100%" k-options="num_Options"/>').appendTo(container)
                }
            },
        ]
    };

    $scope.station_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.StationHasChoose = hasChoose;
    }

    $scope.Station_SaveList = function ($event) {
        $event.preventDefault();

        var data = $scope.Station_GridOptions.dataSource.data();
        if (data != null && data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrix.URL.DetailStation_SaveList,
                data: { lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Station_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Station_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.stationNotIn_GridOptions.dataSource.read();
    }

    $scope.stationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.DetailStation_LocationList,
            readparam: function () { return { locationMaxtrixDetailID: $scope.Detail.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, selectable: "row", filterable: { mode: 'row', visible: false },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,stationNotIn_Grid,stationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,stationNotIn_Grid,stationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'LocationCode', title: 'Mã điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên điểm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.stationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        
    }

    $scope.stationNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrix.URL.DetailStationNotIn_SaveList,
                data: { LocationMaxtrixDetailID: $scope.Detail.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Station_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.Station_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrix.URL.DetailStation_DeleteList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Station_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#endregion

    //#region Matrix Detail
    $scope.Detail_TabIndex = 0;
    $scope.Detail_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.Detail_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.Detail_TabIndex);
            }, 1)
        }
    }
    //#endregion

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

    $scope.LocationMatrix_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    //#region LocationList
    $scope.LocationHasChoose = false;
    $scope.location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.LocationList,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,location_Grid,location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,location_Grid,location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã địa điểm", width: '120px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: "Tên địa điểm", width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: '250px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.GenerateByList = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn có muốn tạo từ các điểm đã chọn không?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATLocationMatrix.URL.GenerateByList,
                        data: { lst: datasend },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.LocationMatrixGrid_Options.dataSource.read();
                            win.close();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        } else {
            $rootScope.Message({ Msg: 'Chưa chọn địa điểm', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.LocationList_Click = function ($event, win) {
        $event.preventDefault();
        $scope.location_GridOptions.dataSource.read();
        win.center().open();
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region gennerate by 2 list
    $scope.hor_splitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: true, resizable: true, size: "50%" },
            { collapsible: true, resizable: true }
        ]
    };

    $scope.LocationFromHasChoose = false;
    $scope.locationFrom_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocationMatrix.URL.LocationList,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,locationFrom_Grid,locationFrom_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,locationFrom_Grid,locationFrom_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã địa điểm", width: '120px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: "Tên địa điểm", width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: '250px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.locationFrom_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationFromHasChoose = hasChoose;
    }

    $scope.LocationToHasChoose = false;
    $scope.locationTo_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 0,
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            //{
            //    title: ' ', width: '35px',
            //    headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,locationTo_Grid,locationTo_GridChoose_Change)" />',
            //    headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
            //    template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,locationTo_Grid,locationTo_GridChoose_Change)" />',
            //    filterable: false, sortable: false
            //},
             {
                 title: ' ', width: 45,
                 template: "<a  href='/' class='k-button' ng-click='locationTo_Grid_Delete($event,dataItem,locationTo_Grid)' title='Ràng buộc'><i class='fa fa-trash'></i></a>",
                 filterable: false, sortable: false
             },
            { field: 'Code', title: "Mã địa điểm", width: '120px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: "Tên địa điểm", width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: '250px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.locationTo_Grid_Delete = function ($event,data,grid) {
        $event.preventDefault();
        grid.dataSource.remove(data);
    }
    $scope.GenerateLimit_AddLocation_Click = function ($event,gridFrom,gridTo) {
        $event.preventDefault();
        var data = gridTo.dataSource.data();
        Common.Data.Each(gridFrom.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o);
        })
        var dataUnique = [];
        var dataUniqueID = [];
        Common.Data.Each(data, function (o) {
            if (dataUniqueID.indexOf(o.ID) === -1) {
                dataUnique.push(o);
                dataUniqueID.push(o.ID);
            }
        })
        gridTo.dataSource.data(dataUnique);
    }
    $scope.locationTo_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationToHasChoose = hasChoose;
    }
    $scope.Location2List_Click = function ($event,win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.hor_splitter.resize();
    }

    $scope.GenerateLimit_Generate = function ($event, win, gridTo) {
        $event.preventDefault();
        var lst = [];
        Common.Data.Each(gridTo.dataSource.data(), function (o) {
            lst.push(o.ID);
        })
        if (lst.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocationMatrix.URL.GenerateLimit,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.LocationMatrixGrid_Options.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
        else $rootScope.Message({ Msg: 'Chưa chọn điểm', NotifyType: Common.Message.NotifyType.ERROR });
    }

    $scope.LocationMatrix_Location_MathClick = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        var lstid = [];
        var data = grid.dataSource.data();
        var flag = 0;
        var length =  data.length;
        $.each(data, function (i, v) {
            var obj1 = { Lat: v.LocationFromLat, Lng: v.LocationFromLng }
            var obj2 = { Lat: v.LocationToLat, Lng: v.LocationToLng }
            if ((obj1.Lat != null && obj1.Lng != null && obj1.Lat != "" && obj1.Lng != "")
                && (obj2.Lat != null && obj2.Lng != null && obj2.Lat != "" && obj2.Lng != ""))
            {
                openMap.Distance(obj1, obj2, function (p1, p2, e) {
                    v.KM = Common.Number.ToNumber2(e.Distance / 1000);
                    v.Hour = Common.Number.ToNumber2(e.Time / 3600);
                    flag++;
                    Common.Log(v.KM + " " + flag);
                    if (flag == length) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Tính lại thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
            else {
                v.KM = 0;
                v.Hour = 0;
                flag++;
                if (flag == length) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Tính lại thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            }
            lstid.push(v);
        });
        grid.dataSource.data(lstid);
    };
    //#endregion

    $scope.CATLocationMatrix_CreateDI_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn tạo dữ liệu từ dữ liệu chuyến phân phối?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATLocationMatrix.URL.GenerateFromOPS,
                    data: { isDI:true },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        grid.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };
    $scope.CATLocationMatrix_CreateCO_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn tạo dữ liệu từ dữ liệu chuyến container?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATLocationMatrix.URL.GenerateFromOPS,
                    data: { isDI: false },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        grid.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.CATLocationMatrix_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_CATLocationMatrix.Data.ExcelKey.CATLocationMatrixRes + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã điểm đi] không được trống',
                '[Mã điểm đi] không tồn tại',
                '[Mã điểm đến] không được trống',
                '[Mã điểm đến]-[Mã điểm đến] trùng nhau',
                '[Mã điểm đến]-[Mã điểm đến] trùng trên file',
                '[KM] không được trống hoặc <0',
                 '[Thời gian(giờ)] không được trống hoặc <0',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _CATLocationMatrix.Data.ExcelKey.CATLocationMatrix,
            params: {},
            rowStart: 1,
            colCheckChange: 8,
            url: Common.Services.url.CAT,
            methodInit: _CATLocationMatrix.URL.ExcelInit,
            methodChange: _CATLocationMatrix.URL.ExcelChange,
            methodImport: _CATLocationMatrix.URL.ExcelImport,
            methodApprove: _CATLocationMatrix.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.LocationMatrixGrid_Options.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);