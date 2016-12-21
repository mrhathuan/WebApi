
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMDriverRouteFee = {
    URL: {
        Read: 'FLMDriverRouteFee_Read',
        Update: 'FLMDriverRouteFee_Update',
        Delete: 'FLMDriverRouteFee_Destroy',
        Get: 'FLMDriverRouteFee_Get',

        Get_Routing: 'FLMDriverRouteFee_GetRouting',
        Get_Customer: 'FLMDriverRouteFee_GetCustomer',
        Get_TypeOfDriverRouteFee: 'FLMDriverRouteFee_GetTypeOfDriverRouteFee',
        Get_TypeOfVehicle: 'ALL_TypeOfVehicle',

        Excel_Export: 'FLMDriver_Excel_Export',
        Excel_Save: 'FLMDriver_Excel_Save',
        Excel_Check: 'FLMDriver_Excel_Check',
    },
}

angular.module('myapp').controller('FLMDriverRouteFee_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMDriverRouteFee_IndexCtrl');

    $rootScope.IsLoading = false;
    $scope.ParamEdit = { ID: -1 }
    $scope.DriverRouteFeeItem = null;
    $scope.FLMDriverRouteFee_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverRouteFee.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    EffectDate: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="FLMDriverRouteFeeEdit_Click($event,FLMDriverRouteFee_Detail_Win,FLMDriverRouteFee_Grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="FLMDriverRouteFeeDestroy_Click($event,FLMDriverRouteFee_Grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'CustomerName', title: 'Tên K.H', width: 150 },
            { field: 'RoutingName', title: 'Cung đường', width: 200 },
             { field: 'TypeOfDriverRouteFeeName', title: 'Loại phí', width: 100 },
             { field: 'TypeOfVehicleName', title: 'Loại xe', width: 100 },
            { field: 'EffectDate', title: 'Ngày hiệu lực', template: "#=EffectDate==null?' ':Common.Date.FromJsonDMY(EffectDate)#", width: 120, },
        ]
    }

    $scope.FLMDriverRouteFeeEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, id);
    }

    $scope.FLMDriverRouteFeeDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverRouteFee.URL.Delete,
                    data: { 'item': item },
                    success: function (res) {
                        $scope.FLMDriverRouteFee_Grid_Options.dataSource.read();
                    }
                });
            }
        }
    }

    $scope.FLMDriverRouteFee_AddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverRouteFee.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.DriverRouteFeeItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.FLMDriverRouteFee_Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'EffectDate', title: 'Ngày hiệu lực', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: "TypeOfVehicleName", title: "Loại xe", width: "150px", filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: "CustomerName", title: "Khách hàng", width: "150px", filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: "RoutingName", title: "Cung đường", width: "150px", filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: "PriceRoute", title: "Giá chuyến", width: "150px", filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: "PriceRouteTimes", title: "Phụ thu nhiều lần", width: "150px", filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: "PercentIncome", title: "Tỉ lệ doanh thu", width: "150px", filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverRouteFee.URL.Excel_Export,
                    data: {},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverRouteFee.URL.Excel_Check,
                    data: { file: e },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverRouteFee.URL.Excel_Save,
                    data: { lst: data },
                    success: function (res) {
                        $scope.FLMDriverRouteFee_Grid_Options.dataSource.read()
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.FLMDriverRouteFee_Detail_Win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMDriverRouteFee.URL.Update,
                data: { 'item': $scope.DriverRouteFeeItem },
                success: function (res) {
                    win.close();
                    $scope.FLMDriverRouteFee_Grid_Options.dataSource.read();
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.FLMDriverRouteFee_Detail_Win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.cboTypeOfDriverRouteFee_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                Text: { type: 'string' },
                Value: { type: 'number' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                var selectItem = this.dataItem();
                $timeout(function () {
                    $scope.DriverRouteFeeItem.TypeOfView = selectItem.TypeOfView;
                }, 1)
            }
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDriverRouteFee.URL.Get_TypeOfDriverRouteFee,
        data: {},
        success: function (res) {
            $scope.cboTypeOfDriverRouteFee_options.dataSource.data(res.Data)
        }
    });

    $scope.cboTypeOfVehicle_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMDriverRouteFee.URL.Get_TypeOfVehicle,
        data: {},
        success: function (res) {
            $scope.cboTypeOfVehicle_options.dataSource.data(res.Data)
        }
    });

    $scope.cboCustomer_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                CustomerName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDriverRouteFee.URL.Get_Customer,
        data: {},
        success: function (res) {
            $scope.cboCustomer_options.dataSource.data(res.Data)
        }
    });

    $scope.cboRouting_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'RoutingName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                RoutingName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDriverRouteFee.URL.Get_Routing,
        data: {},
        success: function (res) {
            $scope.cboRouting_options.dataSource.data(res.Data)
        }
    });

    $scope.numPriceRoute_options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.numPriceRouteTimes_options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.numPercentIncome_options = { format: '#,##0,00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numPlusTotal_options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numPlusPrice_options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
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

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion
}])

