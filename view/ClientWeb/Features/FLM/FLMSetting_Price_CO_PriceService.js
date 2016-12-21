﻿/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSetting_Price_CO_PriceService = {
    URL: {
        Read: 'FLMPrice_CO_Service_List',
        Get: 'FLMPrice_CO_Service_Get',
        Save: 'FLMPrice_CO_Service_Save',
        Delete: 'FLMPrice_CO_Service_Delete',

        ListCurrency: 'CATCurrency_AllList',
        ListService: 'FLMPrice_CO_CATService_List',
    },
    Data: {
    },
    Params: {
        PriceID: -1,
        TermID: -1,
    }
}

angular.module('myapp').controller('FLMSetting_Price_CO_PriceServiceCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_Price_CO_PriceServiceCtrl');
    $rootScope.IsLoading = false;
    $scope.Item = null;
    _FLMSetting_Price_CO_PriceService.Params = $.extend(true, _FLMSetting_Price_CO_PriceService.Params, $state.params);

    $scope.Service_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_CO_PriceService.URL.Read,
            readparam: function () {
                return {
                    priceID: _FLMSetting_Price_CO_PriceService.Params.PriceID,
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="Edit_Click($event,dataItem,Service_win,Service_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Destroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'ServiceName', width: 150, title: 'Dịch vụ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Price', width: 150, title: 'Giá',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CurrencyName', width: 150, title: 'Tiền tệ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    };


    $scope.numPrice_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.PriceService_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.ServiceLoadItem(0, win, vform)
    }

    $scope.Edit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.ServiceLoadItem(item.ID, win, vform)
    }

    $scope.ServiceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_CO_PriceService.URL.Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                vform({ clear: true })
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Destroy_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dịch vụ đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Price_CO_PriceService.URL.Delete,
                    data: { ID: item.ID },
                    success: function (res) {
                        $scope.Service_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.Service_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            $scope.Item.PackingID = null;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Price_CO_PriceService.URL.Save,
                data: { item: $scope.Item, priceID: _FLMSetting_Price_CO_PriceService.Params.PriceID},
                success: function (res) {
                    $scope.Service_gridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    //#region combobox
    $scope.cboServiceOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ServiceName',
        dataValueField: 'ID',

        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ServiceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMSetting_Price_CO_PriceService.URL.ListService,

        success: function (res) {
            $scope.cboServiceOptions.dataSource.data(res.Data);
        }
    });

    $scope.cboCurrencyOptions =
    {

        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CurrencyName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CurrencyName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }


    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMSetting_Price_CO_PriceService.URL.ListCurrency,

        success: function (data) {
            $scope.cboCurrencyOptions.dataSource.data(data);
        }
    });
    //#endregion
    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMSetting.PriceCO", _FLMSetting_Price_CO_PriceService.Params)
    }
}]);