/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMContract_Price_DI_PriceMOQ = {
    URL: {
        //#region price moq
        PriceMOQ_List: 'FLMPrice_DI_PriceMOQ_List',
        PriceMOQ_Get: 'FLMPrice_DI_PriceMOQ_Get',
        PriceMOQ_Save: 'FLMPrice_DI_PriceMOQ_Save',
        PriceMOQ_Delete: 'FLMPrice_DI_PriceMOQ_Delete',

        PriceMOQ_GroupLocation_List: 'FLMPrice_DI_PriceMOQ_GroupLocation_List',
        PriceMOQ_GroupLocation_Delete: 'FLMPrice_DI_PriceMOQ_GroupLocation_DeleteList',
        PriceMOQ_GroupLocation_Save: 'FLMPrice_DI_PriceMOQ_GroupLocation_SaveList',
        PriceMOQ_GroupLocation_NotInList: 'FLMPrice_DI_PriceMOQ_GroupLocation_GroupNotInList',

        PriceMOQ_GroupProduct_List: 'FLMPrice_DI_PriceMOQ_GroupProduct_List',
        PriceMOQ_GroupProduct_Delete: 'FLMPrice_DI_PriceMOQ_GroupProduct_DeleteList',
        PriceMOQ_GroupProduct_Save: 'FLMPrice_DI_PriceMOQ_GroupProduct_Save',
        PriceMOQ_GroupProduct_Get: 'FLMPrice_DI_PriceMOQ_GroupProduct_Get',
        PriceMOQ_GroupProduct_GOPList: 'FLMPrice_DI_PriceMOQ_GroupProduct_GOPList',

        PriceMOQ_Location_List: 'FLMPrice_DI_PriceMOQ_Location_List',
        PriceMOQ_Location_Delete: 'FLMPrice_DI_PriceMOQ_Location_DeleteList',
        PriceMOQ_Location_Save: 'FLMPrice_DI_PriceMOQ_Location_Save',
        PriceMOQ_Location_Get: 'FLMPrice_DI_PriceMOQ_Location_Get',
        PriceMOQ_Location_NotInSave: 'FLMPrice_DI_PriceMOQ_Location_LocationNotInSaveList',
        PriceMOQ_Location_NotInList: 'FLMPrice_DI_PriceMOQ_Location_LocationNotInList',

        PriceMOQ_Route_List: 'FLMPrice_DI_PriceMOQ_Route_List',
        PriceMOQ_Route_Delete: 'FLMPrice_DI_PriceMOQ_Route_DeleteList',
        PriceMOQ_Route_Save: 'FLMPrice_DI_PriceMOQ_Route_SaveList',
        PriceMOQ_Route_NotInList: 'FLMPrice_DI_PriceMOQ_Route_RouteNotInList',

        PriceMOQ_ParentRoute_List: 'FLMPrice_DI_PriceMOQ_ParentRoute_List',
        PriceMOQ_ParentRoute_Delete: 'FLMPrice_DI_PriceMOQ_ParentRoute_DeleteList',
        PriceMOQ_ParentRoute_Save: 'FLMPrice_DI_PriceMOQ_ParentRoute_SaveList',
        PriceMOQ_ParentRoute_NotInList: 'FLMPrice_DI_PriceMOQ_ParentRoute_RouteNotInList',

        PriceMOQ_Parent_List: 'FLMPrice_DI_PriceMOQ_Partner_List',
        PriceMOQ_Parent_Delete: 'FLMPrice_DI_PriceMOQ_Partner_DeleteList',
        PriceMOQ_Parent_Save: 'FLMPrice_DI_PriceMOQ_Partner_SaveList',
        PriceMOQ_Parent_NotInList: 'FLMPrice_DI_PriceMOQ_Partner_PartnerNotInList',

        PriceMOQ_Province_List: 'FLMPrice_DI_PriceMOQ_Province_List',
        PriceMOQ_Province_Delete: 'FLMPrice_DI_PriceMOQ_Province_DeleteList',
        PriceMOQ_Province_Save: 'FLMPrice_DI_PriceMOQ_Province_SaveList',
        PriceMOQ_Province_NotInList: 'FLMPrice_DI_PriceMOQ_Province_NotInList',
        //#endregion
    },
    Data: {
    },
    Params: {
        PriceID: -1,
        TermID: -1,
        CustomerID: -1,
    }
}

angular.module('myapp').controller('FLMSetting_Price_DI_PriceMOQCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_Price_DI_PriceMOQCtrl');
    $rootScope.IsLoading = false;

    _FLMContract_Price_DI_PriceMOQ.Params = $.extend({}, true, $state.params);

    //moq
    $scope.TabIndex = 0;
    $scope.ItemPriceMOQ = { ID: 0 };
    $scope.GLMOQHasChoose = false;
    $scope.GLMOQNotInHasChoose = false;
    $scope.TabIndexMOQ = 0;
    $scope.MOQLocationHasChoose = false;
    $scope.MOQLocationNotInHasChoose = false
    $scope.MOQParentRouteHasChoose = false;
    $scope.MOQParentRouteNotInHasChoose = false;


    $scope.price_MOQ_grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_List,
            readparam: function () { return { priceID: _FLMContract_Price_DI_PriceMOQ.Params.PriceID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="priceMOQ_GridEdit_Click($event,price_MOQ_Edit_win,dataItem,priceMOQ_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="priceMOQ_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'MOQName',  title: 'Tên MOQ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DIMOQSumName', width: 200, title: 'Loại tính tổng', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.price_MOQ_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.PriceMOQLoadItem(0, win, vform)
    }

    $scope.priceMOQ_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.PriceMOQLoadItem(data.ID, win, vform)
    }

    $scope.PriceMOQLoadItem = function (id, win, vform) {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Get,
            data: { priceID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPriceMOQ = res;
                win.center().open();
                vform({ clear: true })
                $scope.price_MOQ_tabstrip.select(0);
                $scope.priceMOQ_GroupLocation_GridOptions.dataSource.read();
                $scope.priceMOQ_GroupProduct_GridOptions.dataSource.read();
                $scope.priceMOQ_Location_GridOptions.dataSource.read();
                $scope.priceMOQ_Route_GridOptions.dataSource.read();
                $scope.priceMOQ_ParentRoute_GridOptions.dataSource.read();
                $scope.priceEx_Province_GridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.priceMOQ_GridDestroy_Click = function ($event, data) {
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
                    url: Common.Services.url.FLM,
                    method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.price_MOQ_grid_Options.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }


    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.FLMSetting.PriceDI", _FLMContract_Price_DI_PriceMOQ.Params)
    }

    //#region moq
    $scope.price_MOQ_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }

    $scope.cboTypeDIMOQSum_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarDIMOQSum,
        success: function (data) {
            $scope.cboTypeDIMOQSum_Options.dataSource.data(data)
        }
    })

    $scope.cboTypePrice_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                TypeName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATTypeOfPriceDIEx,
        success: function (data) {
            $scope.cboTypePrice_Options.dataSource.data(data)
        }
    })

    $scope.priceMOQ_SaveInfo_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemPriceMOQ.DIMOQSumID > 0 && $scope.ItemPriceMOQ.TypeOfPriceDIExID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Save,
                    data: { priceID: _FLMContract_Price_DI_PriceMOQ.Params.PriceID, item: $scope.ItemPriceMOQ },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Get,
                            data: { priceID: res },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.ItemPriceMOQ = res;
                                $scope.price_MOQ_grid_Options.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        });

                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        }
    }

    $scope.priceMOQ_DeleteInfo_Click = function ($event, win) {
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
                    url: Common.Services.url.FLM,
                    method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Delete,
                    data: { id: $scope.ItemPriceMOQ.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    //#region group location
    $scope.priceMOQ_GroupLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupLocation_List,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceMOQ_GroupLocation_Grid,priceMOQ_GroupLocation_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceMOQ_GroupLocation_Grid,priceMOQ_GroupLocation_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'GroupOfLocationCode', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfLocationName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.priceMOQ_GroupLocation_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GLMOQHasChoose = hasChoose;
    }

    $scope.priceMOQ_GroupLocation_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_MOQ_GroupNotIn_GridOptions.dataSource.read();
    }

    $scope.priceMOQ_GroupLocation_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupLocation_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_GroupLocation_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_MOQ_GroupNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupLocation_NotInList,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_MOQ_GroupNotIn_Grid,price_MOQ_GroupNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_MOQ_GroupNotIn_Grid,price_MOQ_GroupNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_MOQ_GroupNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GLMOQNotInHasChoose = hasChoose;
    }

    $scope.price_MOQ_GroupNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupLocation_Save,
                data: { priceMOQID: $scope.ItemPriceMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_GroupLocation_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region route
    $scope.priceMOQ_Route_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Route_List,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceMOQ_Route_Grid,priceMOQ_Route_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceMOQ_Route_Grid,priceMOQ_Route_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.priceMOQ_Route_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MOQRouteHasChoose = hasChoose;
    }

    $scope.priceMOQ_Route_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_MOQ_RouteNotIn_GridOptions.dataSource.read();
    }

    $scope.price_MOQ_RouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Route_NotInList,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID, contractTermID: _FLMContract_Price_DI_PriceMOQ.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_MOQ_RouteNotIn_Grid,price_MOQ_RouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_MOQ_RouteNotIn_Grid,price_MOQ_RouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_MOQ_RouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MOQRouteNotInHasChoose = hasChoose;
    }

    $scope.price_MOQ_RouteNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Route_Save,
                data: { priceMOQID: $scope.ItemPriceMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_Route_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceMOQ_Route_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Route_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_Route_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region group product
    $scope.priceMOQ_GroupProduct_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupProduct_List,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    //IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="priceMOQ_GroupProduct_GridEdit_Click($event,price_MOQ_GroupProduct_win,dataItem,priceMOQGOP_vform)" ng-show="!ItemPriceMOQ.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="priceMOQ_GroupProduct_GridDestroy_Click($event,dataItem)" ng-show="!ItemPriceMOQ.TermClosed" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'GroupOfProductCode', title: "Mã nhóm hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', title: "Tên nhóm hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprPrice', title: "Công thức giá", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprQuantity', title: "Công thức số lượng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.priceMOQ_GroupProduct_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.MOQGOPLoadItem(0, win, vform)
    }

    $scope.priceMOQ_GroupProduct_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.MOQGOPLoadItem(data.ID, win, vform)
    }

    $scope.MOQGOPLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupProduct_Get,
            data: { id: id, cusID: _FLMContract_Price_DI_PriceMOQ.Params.CustomerID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPriceMOQGOP = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.priceMOQ_GroupProduct_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupProduct_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_GroupProduct_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.price_MOQ_GOP_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemPriceMOQGOP.GroupOfProductID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupProduct_Save,
                    data: { priceMOQID: $scope.ItemPriceMOQ.ID, item: $scope.ItemPriceMOQGOP },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.priceMOQ_GroupProduct_GridOptions.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        }
    }


    $scope.cboMOQGroupOfProduct_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_GroupProduct_GOPList,
        data: { cusID: _FLMContract_Price_DI_PriceMOQ.Params.CustomerID },
        success: function (res) {
            $scope.cboMOQGroupOfProduct_Options.dataSource.data(res.Data)
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    //#endregion

    //#region  location

    $scope.priceMOQ_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Location_List,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceMOQ_Location_Grid,priceMOQ_Location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,pricMOQ_Location_Grid,priceMOQ_Location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
             {
                 title: ' ', width: '90px',
                 template: '<a href="/" ng-click="priceMOQ_Location_GridEdit_Click($event,price_MOQ_Location_win,dataItem)" ng-show="!ItemPriceMOQ.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>' +
                     '<a href="/" ng-click="priceMOQ_Location_GridDestroy_Click($event,dataItem,price_MOQ_Location_win)" ng-show="!ItemPriceMOQ.TermClosed" class="k-button"><i class="fa fa-trash"></i></a>',
                 filterable: false, sortable: false
             },
            { field: 'LocationCode', title: "Mã địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: "Tên địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfTOLocationName', title: "Loại", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
		    { field: 'ProvinceName', title: "Tỉnh thành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: "Quận huyện", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.priceMOQ_Location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MOQLocationHasChoose = hasChoose;
    }

    $scope.priceMOQ_Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_MOQ_LocationNotIn_GridOptions.dataSource.read();
    }

    $scope.priceMOQ_Location_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Location_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_MOQ_LocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Location_NotInList,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID, customerID: _FLMContract_Price_DI_PriceMOQ.Params.CustomerID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_MOQ_LocationNotIn_Grid,price_MOQ_LocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_MOQ_LocationNotIn_Grid,price_MOQ_LocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: "Tên địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
		    { field: 'ProvinceName', title: "Tỉnh thành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: "Quận huyện", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_MOQ_LocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MOQLocationNotInHasChoose = hasChoose;
    }

    $scope.price_MOQ_LocationNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Location_NotInSave,
                data: { priceMOQID: $scope.ItemPriceMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_Location_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceMOQ_Location_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Location_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.priceMOQ_Location_GridEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Location_Get,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPriceMOQLocation = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.price_MOQ_Location_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Location_Save,
            data: { item: $scope.ItemPriceMOQLocation },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.priceMOQ_Location_GridOptions.dataSource.read();
                win.close();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }


    $scope.cboMOQTypeOfTOLocation_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfTOLocation,
        success: function (data) {
            var newdata = [];
            newdata.push({ ValueOfVar: " ", ID: -1 })
            Common.Data.Each(data, function (o) {
                newdata.push(o);
            })
            $scope.cboMOQTypeOfTOLocation_Options.dataSource.data(newdata)
        }
    })


    //#endregion

    //#region parent route
    $scope.priceMOQ_ParentRoute_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_ParentRoute_List,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceMOQ_ParentRoute_Grid,priceMOQ_ParentRoute_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceMOQ_ParentRoute_Grid,priceMOQ_ParentRoute_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ParentRoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ParentRoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.priceMOQ_ParentRoute_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MOQParentRouteHasChoose = hasChoose;
    }

    $scope.priceMOQ_ParentRoute_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_MOQ_ParentRouteNotIn_GridOptions.dataSource.read();
    }

    $scope.price_MOQ_ParentRouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_ParentRoute_NotInList,
            readparam: function () { return { priceMOQID: $scope.ItemPriceMOQ.ID, contractTermID: _FLMContract_Price_DI_PriceMOQ.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_MOQ_ParentRouteNotIn_Grid,price_MOQ_ParentRouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_MOQ_ParentRouteNotIn_Grid,price_MOQ_ParentRouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_MOQ_ParentRouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MOQParentRouteNotInHasChoose = hasChoose;
    }

    $scope.price_MOQ_ParentRouteNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_ParentRoute_Save,
                data: { priceMOQID: $scope.ItemPriceMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_ParentRoute_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceMOQ_ParentRoute_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_ParentRoute_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceMOQ_ParentRoute_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion
    $scope.EXpr_Content = "";
    $scope.Expr_current = "";
    $scope.Btn_Expr_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.Expr_current = data;
        switch (data) {
            case "ExprInput":
                $scope.EXpr_Content = $scope.ItemPriceMOQ.ExprInput; break;
            case "ExprCBM":
                $scope.EXpr_Content = $scope.ItemPriceMOQ.ExprCBM; break;
            case "ExprTon":
                $scope.EXpr_Content = $scope.ItemPriceMOQ.ExprTon; break;
            case "ExprQuan":
                $scope.EXpr_Content = $scope.ItemPriceMOQ.ExprQuan; break;
            case "ExprPrice":
                $scope.EXpr_Content = $scope.ItemPriceMOQ.ExprPrice; break;
            case "ExprPriceFix":
                $scope.EXpr_Content = $scope.ItemPriceMOQ.ExprPriceFix; break;
        }
        $scope.LoadNote($scope.ItemPriceMOQ.DIMOQSumID);
        win.open().center();
    }

    $scope.Btn_Expr_Save_Click = function ($event, win) {
        $event.preventDefault();
        switch ($scope.Expr_current) {
            case "ExprInput":
                $scope.ItemPriceMOQ.ExprInput = $scope.EXpr_Content; break;
            case "ExprCBM":
                $scope.ItemPriceMOQ.ExprCBM = $scope.EXpr_Content; break;
            case "ExprTon":
                $scope.ItemPriceMOQ.ExprTon = $scope.EXpr_Content; break;
            case "ExprQuan":
                $scope.ItemPriceMOQ.ExprQuan = $scope.EXpr_Content; break;
            case "ExprPrice":
                $scope.ItemPriceMOQ.ExprPrice = $scope.EXpr_Content; break;
            case "ExprPriceFix":
                $scope.ItemPriceMOQ.ExprPriceFix = $scope.EXpr_Content; break;
        }
        win.close();
    }

    $scope.show_Note = "";
    $scope.LoadNote = function (key) {
        //var OrderInDay_ScheduleInDay = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về] [QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [UnitPriceMax - Đơn giá max] [UnitPriceMin - Đơn giá min] [TonMOQ - Số tấn (hàng hóa tính theo tấn)] [CBMMOQ - Số khối (hàng hóa tính theo khối)]";
        //var DIMOQSumOrder = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về] [QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [GOVCodeOrder - Mã loại xe đơn hàng]";
        //var DIMOQSumSchedule = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về] [QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [VehicleCode - Số xe]";
        //var SumOrderLocation_SumOrderRoute = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về] [QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [UnitPrice - Đơn giá] [UnitPriceMax - Đơn giá max] [UnitPriceMin - Đơn giá min] [TotalOrder - Tổng số đơn hàng]";
        //var DIMOQSumReturnOrder = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về] [QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [UnitPrice - Đơn giá] [UnitPriceMax - Đơn giá max] [UnitPriceMin - Đơn giá min] [Credit - Doanh thu]";
        //switch (key + "") {
        //    case "305": $scope.Content_Note = DIMOQSumOrder; break;
        //    case "306": $scope.Content_Note = SumOrderLocation_SumOrderRoute; break;
        //    case "307": $scope.Content_Note = SumOrderLocation_SumOrderRoute; break;
        //    case "308": $scope.Content_Note = OrderInDay_ScheduleInDay; break;
        //    case "309": $scope.Content_Note = DIMOQSumSchedule; break;
        //    case "310": $scope.Content_Note = OrderInDay_ScheduleInDay; break;
        //    case "311": $scope.Content_Note = DIMOQSumReturnOrder; break;
        //    case "312": $scope.Content_Note = DIMOQSumReturnOrder; break;
        //}
        $scope.show_Note = key;
    }

    //#region parent
    $scope.priceEx_Parent_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Parent_List,
            readparam: function () { return { priceExID: $scope.ItemPriceMOQ.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceEx_Parent_Grid,priceEx_Parent_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceEx_Parent_Grid,priceEx_Parent_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'PartnerCode', title: "Mã đối tác", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', title: "Tên đối tác", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.ParentHasChoose = false;
    $scope.priceEx_Parent_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentHasChoose = hasChoose;
    }

    $scope.priceEx_Parent_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_ex_ParentNotIn_GridOptions.dataSource.read();
    }

    $scope.price_ex_ParentNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Parent_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceMOQ.ID, contractTermID: _FLMContract_Price_DI_PriceMOQ.Params.TermID, CustomerID: _FLMContract_Price_DI_PriceMOQ.Params.CustomerID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ex_ParentNotIn_Grid,price_ex_ParentNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ex_ParentNotIn_Grid,price_ex_ParentNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'PartnerCode', title: "Mã đối tác", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', title: "Tên đối tác", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.ParentNotInHasChoose = false;
    $scope.price_ex_ParentNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentNotInHasChoose = hasChoose;
    }

    $scope.price_exParent_NotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Parent_Save,
                data: { priceExID: $scope.ItemPriceMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Parent_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceEx_Parent_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Parent_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Parent_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region Province
    $scope.priceEx_Province_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Province_List,
            readparam: function () { return { priceExID: $scope.ItemPriceMOQ.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceEx_Province_Grid,priceEx_Province_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceEx_Province_Grid,priceEx_Province_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ProvinceCode', title: "Mã tỉnh", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: "Tên tỉnh", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.ProvinceHasChoose = false;
    $scope.priceEx_Province_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceHasChoose = hasChoose;
    }

    $scope.priceEx_Province_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_ex_ProvinceNotIn_GridOptions.dataSource.read();
    }

    $scope.price_ex_ProvinceNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Province_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceMOQ.ID, contractTermID: _FLMContract_Price_DI_PriceMOQ.Params.TermID, CustomerID: _FLMContract_Price_DI_PriceMOQ.Params.CustomerID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ex_ProvinceNotIn_Grid,price_ex_ProvinceNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ex_ProvinceNotIn_Grid,price_ex_ProvinceNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ProvinceCode', title: "Mã tỉnh", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: "Tên tỉnh", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.ProvinceNotInHasChoose = false;
    $scope.price_ex_ProvinceNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceNotInHasChoose = hasChoose;
    }

    $scope.price_exProvince_NotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Province_Save,
                data: { priceExID: $scope.ItemPriceMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Province_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceEx_Province_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceMOQ.URL.PriceMOQ_Province_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Province_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion
}]);