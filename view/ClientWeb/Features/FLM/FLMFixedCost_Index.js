/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMFixedCost = {
    URL: {
        Read: "FLMFixedCost_List",
        SaveList: 'FLMFixedCost_SaveList',
        DeleteList: 'FLMFixedCost_DeleteList',
        //mới 15/7
        Detail_Receipt: "FLMFixedCost_ReceiptList",
    },
    Data: {
    }
}

angular.module('myapp').controller('FLMFixedCost_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMFixedCost_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.FixedCostHasChoose = false;

    $scope.FixedCostHasChoose = false;
    $scope.ItemSearch = {
        Month: (new Date().getMonth() + 1),
        Year: (new Date().getFullYear()),
    }

    $scope.numYear_options = { format: '#', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numMonth_options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, max: 12, step: 1, decimals: 0, }

    $scope.FLMFixedCost_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMFixedCost.URL.Read,
            readparam: function () { return { 'month': $scope.ItemSearch.Month, 'year': $scope.ItemSearch.Year, }; },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    IsClosed: { type: 'string', editable: false },
                    IsChoose: { type: 'boolean' },
                    FixValue: { type: 'number' },
                    TotalReceiptValue: { type: 'number' },
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMFixedCost_Grid,FLMFixedCost_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMFixedCost_Grid,FLMFixedCost_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'AssetCode', title: 'Mã xe',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px', editable: false, }
            },
            {
                field: 'FixValue', title: 'Khấu hao cố định', template:'#= Common.Number.ToMoney(FixValue)#',
                filterable: { cell: { operator: 'equal', showOperators: false }, width: '150px', editable: false, }
            },
            {
                field: 'TotalReceiptValue', title: 'Khấu hao phiếu',
                template: '<a href="/" ng-click="Detail_ReceiptClick($event,dataItem,Receipt_win,Receipt_Grid)" >#=Common.Number.ToMoney(TotalReceiptValue)#</a>',
                filterable: { cell: { operator: 'equal', showOperators: false }, width: '150px', editable: false, }
            },
            {
                field: 'IsClosed', title: 'Đã đóng', width: 120,
                template: '<input type="checkbox" #= IsClosed=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Tất cả', Value: '' }, { Text: 'Đã đóng', Value: true }, { Text: 'Chưa đóng', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FLMFixedCost_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.FixedCostHasChoose = hasChoose;
    }

    $scope.FLMFixedCost_SearchClick = function ($event) {
        $event.preventDefault();
        $scope.FLMFixedCost_GridOptions.dataSource.read();
    }

    $scope.Detail_ReceiptClick = function ($event, data, win, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMFixedCost.URL.Detail_Receipt,
            data: { assetID: data.AssetID, 'month': $scope.ItemSearch.Month, 'year': $scope.ItemSearch.Year, },
            success: function (res) {
                $rootScope.IsLoading = false;
                win.center().open();
                $scope.Receipt_GridOptions.dataSource.data(res);
                
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.FLMFixedCost_Save_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.AssetID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMFixedCost.URL.SaveList,
                data: { lst: datasend, 'month': $scope.ItemSearch.Month, 'year': $scope.ItemSearch.Year },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.FLMFixedCost_GridOptions.dataSource.read();
                    $scope.FixedCostHasChoose = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    };

    $scope.FLMFixedCost_Refresh_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.AssetID);
        })
        if (datasend.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận cập nhật?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMFixedCost.URL.DeleteList,
                        data: { lst: datasend, 'month': $scope.ItemSearch.Month, 'year': $scope.ItemSearch.Year },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.FLMFixedCost_GridOptions.dataSource.read();
                            $scope.FixedCostHasChoose = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    };

    $scope.Receipt_GridOptions = {
        dataSource: Common.DataSource.Local([], {
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    IsClosed: { type: 'string', editable: false },
                    Value: { type: 'number' },
                }
            },
            pageSize:0
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                field: 'ReceiptNo', title: 'Mã phiếu',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px', editable: false, }
            },
            {
                field: 'Value', title: 'Giá trị',
                filterable: { cell: { operator: 'equal', showOperators: false }, width: '150px', editable: false, }
            },
            {
                field: 'IsClosed', title: 'Đã đóng', width: 120,
                template: '<input type="checkbox" #= IsClosed=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Tất cả', Value: '' }, { Text: 'Đã đóng', Value: true }, { Text: 'Chưa đóng', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    //#region Action

    $scope.Win_CloseClick = function ($event,win) {
        $event.preventDefault();
        win.close();
    }

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