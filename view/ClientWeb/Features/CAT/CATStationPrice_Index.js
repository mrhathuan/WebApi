/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATStationPrice = {
    URL: {
        Read: 'CATStationPrice_List',
        Get: 'CATStationPrice_Get',
        Save: 'CATStationPrice_Save',
        Delete: 'CATStationPrice_Delete',

        LocationList: 'CATStationPrice_LocationList',
        AssetList: 'CATStationPrice_AssetList',

        Excel_Export: 'CATStationPrice_Excel_Export',
        Excel_Import: 'CATStationPrice_Excel_Save',
        Excel_Check: 'CATStationPrice_Excel_Check',
    },
}

angular.module('myapp').controller('CATStationPrice_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATStationPrice_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.item = {};
    $scope.CATStationPrice_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATStationPrice.URL.Read,
            readparam: function () {},
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Price: { type: 'number' },
                    PriceKM: { type: 'number' },
                }
            },
            group: [{ field: "LocationCode", aggregates: [{ field: "LocationAddress", aggregate: "count" }] }],
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '85px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CATStationPrice_EditClick($event,CATStationPrice_EditWin,dataItem,CATStationPriceForm)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATStationPrice_DestroyClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { field: 'LocationID', title: 'Mã CAT', width: 150, sortable: false, ilterable: { cell: { operator: 'contains', showOperators: false } } ,hidden:true},
            { field: 'LocationAddress', title: 'Địa chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, hidden:true},
            { field: 'LocationCode', title: 'Mã trạm', width: 150, groupHeaderTemplate: function (data) { return 'Mã trạm: ' + data.value + ', Mã địa chỉ :' + data.aggregates.parent().items[0].LocationAddress }, hidden: true },
          
            { field: 'LocationName', title: 'Tên trạm', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, hidden: true },
           { field: 'Ton', title: 'Trọng tải', width: 150, filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'PriceKM', title: 'Giá theo km', width: 150, filterable: { cell: { operator: 'lte', showOperators: false } } },
             { field: 'Price', title: 'Giá', width: 150, filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'Note', title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.CATStationPrice_AddClick = function ($event, win,vform) {
        $event.preventDefault();
       $scope.LoadItem(0,win,vform)
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
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATStationPrice.URL.LocationList,
        success: function (res) {
            $scope.cboLocation_Options.dataSource.data(res);
        }
    });
  
    $scope.numPrice_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numPriceKM_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numTon_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }


    $scope.CATStationPrice_Save = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.item.LocationID) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStationPrice.URL.Save,
                    data: { item: $scope.item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.CATStationPrice_Grid_Options.dataSource.read();
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
            else $rootScope.Message({ Msg: 'Trạm không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }


    $scope.CATStationPrice_DestroyClick = function ($event, data) {
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
                    method: _CATStationPrice.URL.Delete,
                    data: { item: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.CATStationPrice_Grid_Options.dataSource.read();
                    }
                });
            }
        })
        
    };


    $scope.CATStationPrice_EditClick = function ($event, win, data,vform) {
        $event.preventDefault();
       $scope.LoadItem(data.ID,win,vform)
    }

    $scope.LoadItem = function (id,win,vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATStationPrice.URL.Get,
            data: {id:id},
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.item = res;
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
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
                { field: 'LocationName', width: 100, title: 'Trạm', filterable: { cell: { showOperators: false, operator: "contains" } } },

            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStationPrice.URL.Excel_Export,
                    data: {},
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStationPrice.URL.Excel_Check,
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
                        method: _CATStationPrice.URL.Excel_Import,
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
                            $scope.CATStationPrice_Grid_Options.dataSource.read();
                        }
                    
                    })
                }
        })
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CATStationPrice,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);