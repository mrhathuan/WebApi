/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATStationMonth = {
    URL: {
        Read: 'CATStationMonth_List',
        Get: 'CATStationMonth_Get',
        Save: 'CATStationMonth_Save',
        Delete: 'CATStationMonth_Delete',

        LocationList: 'CATStationMonth_LocationList',
        AssetList: 'CATStationMonth_AssetList',

        Excel_Export: 'CATStationMonth_Excel_Export',
        Excel_Import: 'CATStationMonth_Excel_Save',
        Excel_Check: 'CATStationMonth_Excel_Check',
    },
}

angular.module('myapp').controller('CATStationMonth_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATStationMonth_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.item = {};
    $scope.CATStationMonth_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATStationMonth.URL.Read,
            readparam: function () {},
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Price: { type: 'number' },
                    PriceKM: { type: 'number' },
                    DateFrom: { type: 'date' },
                    DateTo: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '85px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CATStationMonth_EditClick($event,CATStationMonth_EditWin,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATStationMonth_DestroyClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { field: 'LocationCode', title: 'Mã trạm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên trạm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AssetNo', title: 'Số xe', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Price', title: 'Giá', width: 150, filterable: { cell: { operator: 'lte', showOperators: false } } },
            {
                field: 'DateFrom', width: '130px', title: 'Từ ngày', template: "#=DateFrom==null?' ':Common.Date.FromJsonDMY(DateFrom)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'DateTo', width: '130px', title: 'Đến ngày', template: "#=DateTo==null?' ':Common.Date.FromJsonDMY(DateTo)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { field: 'Note', title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },

        ]
    }

    $scope.CATStationMonth_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.item = {};
        $scope.item.Price = 0;
        $scope.LoadData();
        win.center().open();
    }

    $scope.cboLocation_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        })
    };

    $scope.cboAsset_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'RegNo', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RegNo: { type: 'string' },
                }
            }
        })
    };

    $scope.cboExprInputDay_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data: [
                { Text: ' ', Value: ' ' },
                 { Text: 'Theo ngày của vé', Value: 'true' },
                 { Text: 'Theo ngày hoạt động', Value: '[IsDayOn]' }
            ],
            model: {
                //id: 'ID',
                fields: {
                    Text: { type: 'string' },
                    Value: { type: 'string' },
                }
            }
        })
    };

    $scope.cboExprDay_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data: [
                { Text: ' ', Value: ' ' },
                { Text: 'Từng ngày của vé', Value: '[Price]/[TotalDayMonth]' },
                { Text: 'Từng ngày hoạt động', Value: '[Price]/[TotalDayOn]' }
            ],
            model: {
                //id: 'ID',
                fields: {
                    Text: { type: 'string' },
                    Value: { type: 'string' },
                }
            }
        })
    };

    $scope.CATStationMonth_Save = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.item.DateFrom < $scope.item.DateTo) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStationMonth.URL.Save,
                    data: { item: $scope.item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.CATStationMonth_Grid_Options.dataSource.read();
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
            else {
                $rootScope.Message({
                    Msg: 'Thiết lập từ ngày đến ngày sai.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
            }
        }
    }


    $scope.CATStationMonth_DestroyClick = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATStationMonth.URL.Delete,
            data: { item: data },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.CATStationMonth_Grid_Options.dataSource.read();
            }
        });
    };


    $scope.CATStationMonth_EditClick = function ($event, win, data) {
        $event.preventDefault();
        $scope.item = data;
        $scope.LoadData();
        win.center().open();
    }

    $scope.LoadData = function()
    {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATStationMonth.URL.LocationList,
            success: function (res) {
                $scope.cboLocation_Options.dataSource.data(res);
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATStationMonth.URL.AssetList,
            success: function (res) {
                $scope.cboAsset_Options.dataSource.data(res);
            }
        });
    }
    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'LocationCode', width: 100, title: 'Mã trạm', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'LocationName', width: 100, title: 'Trạm', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStationMonth.URL.Excel_Export,
                    data: {},
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStationMonth.URL.Excel_Check,
                    data: { file: e },
                    success: function (data) {
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStationMonth.URL.Excel_Import,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })
                        $scope.CATStationMonth_Grid_Options.dataSource.read();
                    }
                })
            }
        })
    }
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CATStationMonth,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);