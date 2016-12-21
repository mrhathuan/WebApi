/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _VENContract_Price_DI_Ex = {
    URL: {
        //#region price ex
        PriceEx_Get: 'VENPrice_DI_Ex_Get',
        PriceEx_Save: 'VENPrice_DI_Ex_Save',

        PriceEx_GroupLocation_List: 'VENPrice_DI_Ex_GroupLocation_List',
        PriceEx_GroupLocation_Delete: 'VENPrice_DI_Ex_GroupLocation_DeleteList',
        PriceEx_GroupLocation_Save: 'VENPrice_DI_Ex_GroupLocation_SaveList',
        PriceEx_GroupLocation_NotInList: 'VENPrice_DI_Ex_GroupLocation_GroupNotInList',

        PriceEx_GroupProduct_List: 'VENPrice_DI_Ex_GroupProduct_List',
        PriceEx_GroupProduct_Delete: 'VENPrice_DI_Ex_GroupProduct_DeleteList',
        PriceEx_GroupProduct_Save: 'VENPrice_DI_Ex_GroupProduct_Save',
        PriceEx_GroupProduct_Get: 'VENPrice_DI_Ex_GroupProduct_Get',
        PriceEx_GroupProduct_GOPList: 'VENPrice_DI_Ex_GroupProduct_GOPList',

        PriceEx_Location_List: 'VENPrice_DI_Ex_Location_List',
        PriceEx_Location_Delete: 'VENPrice_DI_Ex_Location_DeleteList',
        PriceEx_Location_Save: 'VENPrice_DI_Ex_Location_Save',
        PriceEx_Location_Get: 'VENPrice_DI_Ex_Location_Get',
        PriceEx_Location_NotInSave: 'VENPrice_DI_Ex_Location_LocationNotInSaveList',
        PriceEx_Location_NotInList: 'VENPrice_DI_Ex_Location_LocationNotInList',

        PriceEx_Route_List: 'VENPrice_DI_Ex_Route_List',
        PriceEx_Route_Delete: 'VENPrice_DI_Ex_Route_DeleteList',
        PriceEx_Route_Save: 'VENPrice_DI_Ex_Route_SaveList',
        PriceEx_Route_NotInList: 'VENPrice_DI_Ex_Route_RouteNotInList',

        PriceEx_ParentRoute_List: 'VENPrice_DI_Ex_ParentRoute_List',
        PriceEx_ParentRoute_Delete: 'VENPrice_DI_Ex_ParentRoute_DeleteList',
        PriceEx_ParentRoute_Save: 'VENPrice_DI_Ex_ParentRoute_SaveList',
        PriceEx_ParentRoute_NotInList: 'VENPrice_DI_Ex_ParentRoute_RouteNotInList',

        PriceEx_Parent_List: 'VENPrice_DI_Ex_Partner_List',
        PriceEx_Parent_Delete: 'VENPrice_DI_Ex_Partner_DeleteList',
        PriceEx_Parent_Save: 'VENPrice_DI_Ex_Partner_SaveList',
        PriceEx_Parent_NotInList: 'VENPrice_DI_Ex_Partner_PartnerNotInList',

        PriceMOQ_Province_List: 'VENPrice_DI_Ex_Province_List',
        PriceMOQ_Province_Delete: 'VENPrice_DI_Ex_Province_DeleteList',
        PriceMOQ_Province_Save: 'VENPrice_DI_Ex_Province_SaveList',
        PriceMOQ_Province_NotInList: 'VENPrice_DI_Ex_Province_NotInList',
        //#endregion
    },
    Data: {
    },
    Params: {
        //ID: -1,
        //CustomerID: -1,
        //ContractID: -1,
        //IDEx:0
        PriceID: -1,
        PriceIDEx: 0,
        TermID: -1,
        CustomerID: -1,
    }
}

angular.module('myapp').controller('VENContract_Price_DI_ExCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('VENContract_Price_DI_ExCtrl');
    $rootScope.IsLoading = false;

    _VENContract_Price_DI_Ex.Params = $.extend({}, true, $state.params);

    //ex
    $scope.TabIndex = 0;
    $scope.ItemPriceEx = { ID: 0 };
    $scope.GroupLocationHasChoose = false;
    $scope.GroupLocationNotInHasChoose = false;
    $scope.LocationHasChoose = false;
    $scope.LocationNotInHasChoose = false;
    $scope.RouteHasChoose = false;
    $scope.RouteNotInHasChoose = false;
    $scope.ParentRouteHasChoose = false;
    $scope.ParentRouteNotInHasChoose = false;

    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENContract_Price_DI_Ex.URL.PriceEx_Get,
        data: { id: _VENContract_Price_DI_Ex.Params.PriceIDEx },
        success: function (res) {
            $rootScope.IsLoading = false;
            $scope.ItemPriceEx = res;
            $scope.price_ex_tabstrip.select(0);
            if (res.ID > 0) {
                $scope.priceEx_GroupLocation_GridOptions.dataSource.read();
                $scope.priceEx_GroupProduct_GridOptions.dataSource.read();
                $scope.priceEx_Location_GridOptions.dataSource.read();
                $scope.priceEx_Route_GridOptions.dataSource.read();
                $scope.priceEx_ParentRoute_GridOptions.dataSource.read();
                $scope.priceEx_Parent_GridOptions.dataSource.read();
                $scope.priceEx_Province_GridOptions.dataSource.read();
            }
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    })

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //#region preice ex
    $scope.price_ex_tabstripOptions = {
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


    $scope.cboTypeDIExSum_Options = {
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
        url: Common.ALL.URL.SYSVarDIExSum,
        success: function (data) {
            $scope.cboTypeDIExSum_Options.dataSource.data(data)
        }
    })

    $scope.priceEX_SaveInfo_Click = function ($event, vform) {
        $event.preventDefault();

        if (vform()) {
            if ($scope.ItemPriceEx.TypeOfPriceDIExID > 0 && $scope.ItemPriceEx.DIExSumID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_DI_Ex.URL.PriceEx_Save,
                    data: { priceID: _VENContract_Price_DI_Ex.Params.PriceID, item: $scope.ItemPriceEx },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.VEN,
                            method: _VENContract_Price_DI_Ex.URL.PriceEx_Get,
                            data: { id: res },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.ItemPriceEx = res;
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



    //#region group location
    $scope.priceEx_GroupLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupLocation_List,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceEx_GroupLocation_Grid,priceEx_GroupLocation_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceEx_GroupLocation_Grid,priceEx_GroupLocation_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'GroupOfLocationCode', title: "Mã loại địa điểm", width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfLocationName', title: "Tên loại địa điểm", filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }
    $scope.priceEx_GroupLocation_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupLocationHasChoose = hasChoose;
    }

    $scope.priceEx_GroupLocation_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_ex_GroupNotIn_GridOptions.dataSource.read();
    }

    $scope.priceEx_GroupLocation_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupLocation_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_GroupLocation_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_ex_GroupNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupLocation_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ex_GroupNotIn_Grid,price_ex_GroupNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ex_GroupNotIn_Grid,price_ex_GroupNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_ex_GroupNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupLocationNotInHasChoose = hasChoose;
    }

    $scope.price_ex_GroupNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupLocation_Save,
                data: { priceExID: $scope.ItemPriceEx.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_GroupLocation_GridOptions.dataSource.read();
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
    $scope.priceEx_Route_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Route_List,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceEx_Route_Grid,priceEx_Route_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceEx_Route_Grid,priceEx_Route_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RoutingCode', title: "Mã cung đường", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.priceEx_Route_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteHasChoose = hasChoose;
    }

    $scope.priceEx_Route_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_ex_RouteNotIn_GridOptions.dataSource.read();
    }

    $scope.price_ex_RouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Route_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID, contractTermID: _VENContract_Price_DI_Ex.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ex_RouteNotIn_Grid,price_ex_RouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ex_RouteNotIn_Grid,price_ex_RouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_ex_RouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteNotInHasChoose = hasChoose;
    }

    $scope.price_ex_RouteNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_Route_Save,
                data: { priceExID: $scope.ItemPriceEx.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Route_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceEx_Route_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_Route_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Route_GridOptions.dataSource.read();
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
    $scope.priceEx_GroupProduct_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupProduct_List,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
                template: '<a href="/" ng-click="priceEx_GroupProduct_GridEdit_Click($event,price_ex_GroupProduct_win,dataItem,priceExGOP_vform)" ng-show="!ItemPriceEx.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="priceEx_GroupProduct_GridDestroy_Click($event,dataItem)" ng-show="!ItemPriceEx.TermClosed" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'GroupOfProductCode', title: "Mã nhóm hàng", width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', title: "Tên nhóm hàng", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprPrice', title: "Công thức giá", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprQuantity', title: "Công thức số lượng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.priceEx_GroupProduct_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.GOPLoadItem(0, win, vform)
    }

    $scope.priceEx_GroupProduct_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.GOPLoadItem(data.ID, win, vform)
    }

    $scope.GOPLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupProduct_Get,
            data: { id: id, cusID: _VENContract_Price_DI_Ex.Params.CustomerID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPriceExGOP = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.priceEx_GroupProduct_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupProduct_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_GroupProduct_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.price_ex_GOP_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemPriceExGOP.GroupOfProductID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupProduct_Save,
                    data: { priceExID: $scope.ItemPriceEx.ID, item: $scope.ItemPriceExGOP },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.priceEx_GroupProduct_GridOptions.dataSource.read();
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


    $scope.cboGroupOfProduct_Options = {
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
        url: Common.Services.url.VEN,
        method: _VENContract_Price_DI_Ex.URL.PriceEx_GroupProduct_GOPList,
        data: { cusID: _VENContract_Price_DI_Ex.Params.CustomerID },
        success: function (res) {
            $scope.cboGroupOfProduct_Options.dataSource.data(res.Data)
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    //#endregion

    //#region group location

    $scope.priceEx_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Location_List,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceEx_Location_Grid,priceEx_Location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceEx_Location_Grid,priceEx_Location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
             {
                 title: ' ', width: '90px',
                 template: '<a href="/" ng-click="priceEx_Location_GridEdit_Click($event,price_ex_Location_win,dataItem)" ng-show="!ItemPriceEx.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>' +
                     '<a href="/" ng-click="priceEx_Location_GridDestroy_Click($event,dataItem,price_ex_Location_win)" ng-show="!ItemPriceEx.TermClosed" class="k-button"><i class="fa fa-trash"></i></a>',
                 filterable: false, sortable: false
             },
            { field: 'LocationCode', title: "Mã địa điểm", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: "Tên địa điểm", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfTOLocationName', title: "Loại", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }
    $scope.priceEx_Location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.priceEx_Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_ex_LocationNotIn_GridOptions.dataSource.read();
    }

    $scope.priceEx_Location_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_Location_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_ex_LocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Location_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID, customerID: _VENContract_Price_DI_Ex.Params.CustomerID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ex_LocationNotIn_Grid,price_ex_LocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ex_LocationNotIn_Grid,price_ex_LocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: "Tên địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_ex_LocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationNotInHasChoose = hasChoose;
    }

    $scope.price_ex_LocationNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_Location_NotInSave,
                data: { priceExID: $scope.ItemPriceEx.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Location_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceEx_Location_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_Location_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.priceEx_Location_GridEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Location_Get,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPriceExLocation = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.price_ex_Location_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Location_Save,
            data: { item: $scope.ItemPriceExLocation },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.priceEx_Location_GridOptions.dataSource.read();
                win.close();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }


    $scope.cboTypeOfTOLocation_Options = {
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
            $scope.cboTypeOfTOLocation_Options.dataSource.data(newdata)
        }
    })


    //#endregion

    //#region parent route
    $scope.priceEx_ParentRoute_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_ParentRoute_List,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,priceEx_ParentRoute_Grid,priceEx_ParentRoute_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,priceEx_ParentRoute_Grid,priceEx_ParentRoute_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ParentRoutingCode', title: "Mã cung đường", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ParentRoutingName', title: "Tên cung đường", filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.priceEx_ParentRoute_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteHasChoose = hasChoose;
    }

    $scope.priceEx_ParentRoute_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_ex_ParentRouteNotIn_GridOptions.dataSource.read();
    }

    $scope.price_ex_ParentRouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_ParentRoute_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID, contractTermID: _VENContract_Price_DI_Ex.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ex_ParentRouteNotIn_Grid,price_ex_ParentRouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ex_ParentRouteNotIn_Grid,price_ex_ParentRouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_ex_ParentRouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteNotInHasChoose = hasChoose;
    }

    $scope.price_exParent_RouteNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_ParentRoute_Save,
                data: { priceExID: $scope.ItemPriceEx.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_ParentRoute_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.priceEx_ParentRoute_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_ParentRoute_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.priceEx_ParentRoute_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.VENContract.PriceDI", _VENContract_Price_DI_Ex.Params)
    }

    $scope.EXpr_Content = "";
    $scope.Expr_current = "";
    $scope.Btn_Expr_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.Expr_current = data;
        switch (data) {
            case "ExprInput":
                $scope.EXpr_Content = $scope.ItemPriceEx.ExprInput; break;
            case "ExprCBM":
                $scope.EXpr_Content = $scope.ItemPriceEx.ExprCBM; break;
            case "ExprTon":
                $scope.EXpr_Content = $scope.ItemPriceEx.ExprTon; break;
            case "ExprQuan":
                $scope.EXpr_Content = $scope.ItemPriceEx.ExprQuan; break;
            case "ExprPrice":
                $scope.EXpr_Content = $scope.ItemPriceEx.ExprPrice; break;
            case "ExprPriceFix":
                $scope.EXpr_Content = $scope.ItemPriceEx.ExprPriceFix; break;
        }
        $scope.LoadNote($scope.ItemPriceEx.DIExSumID);
        win.open().center();
    }

    $scope.Btn_Expr_Save_Click = function ($event, win) {
        $event.preventDefault();
        switch ($scope.Expr_current) {
            case "ExprInput":
                $scope.ItemPriceEx.ExprInput = $scope.EXpr_Content; break;
            case "ExprCBM":
                $scope.ItemPriceEx.ExprCBM = $scope.EXpr_Content; break;
            case "ExprTon":
                $scope.ItemPriceEx.ExprTon = $scope.EXpr_Content; break;
            case "ExprQuan":
                $scope.ItemPriceEx.ExprQuan = $scope.EXpr_Content; break;
            case "ExprPrice":
                $scope.ItemPriceEx.ExprPrice = $scope.EXpr_Content; break;
            case "ExprPriceFix":
                $scope.ItemPriceEx.ExprPriceFix = $scope.EXpr_Content; break;
        }
        win.close();
    }

    $scope.show_Note = "";
    $scope.LoadNote = function (key) {
        //var DIExSumOrderInDay = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] ";
        //var DIExScheduleInDay = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] ";
        //var DIExSchedule = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [DropPointCurrent - Số điểm giao hiện tại] [GetPointCurrent - Số điểm nhận hiện tại] [GOVCodeSchedule - Mã loại xe chuyến] [HasCashCollect - Có phụ thu] [VehicleCode - Số xe]";
        //var DIExSumOrder = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [DropPointCurrent - Số điểm giao hiện tại] [GetPointCurrent - Số điểm nhận hiện tại] [GOVCodeSchedule - Mã loại xe chuyến] [HasCashCollect - Có phụ thu] ";
        //var DIExSumOrderRoute = "TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [GOVCodeSchedule - Mã loại xe chuyến] [HasCashCollect - Có phụ thu] ";
        //var DIExSumOrderLocation = "TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [GOVCodeSchedule - Mã loại xe chuyến] [HasCashCollect - Có phụ thu] ";
        //switch (key + "") {
        //    case "323": $scope.Content_Note = DIExSumOrder; break;
        //    case "324": $scope.Content_Note = DIExSumOrderLocation; break;
        //    case "325": $scope.Content_Note = DIExSumOrderRoute; break;
        //    case "326": $scope.Content_Note = DIExSumOrderInDay; break;
        //    case "327": $scope.Content_Note = DIExSchedule; break;
        //    case "328": $scope.Content_Note = DIExScheduleInDay; break;
        //}
        $scope.show_Note = key;
    }

    //#region parent
    $scope.priceEx_Parent_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Parent_List,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
            { field: 'PartnerCode', title: "Mã đối tác", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', title: "Tên đối tác", filterable: { cell: { operator: 'contains', showOperators: false } } },
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
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceEx_Parent_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID, contractTermID: _VENContract_Price_DI_Ex.Params.TermID, CustomerID: _VENContract_Price_DI_Ex.Params.CustomerID } },
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
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_Parent_Save,
                data: { priceExID: $scope.ItemPriceEx.ID, lst: datasend },
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
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceEx_Parent_Delete,
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
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceMOQ_Province_List,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID } },
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
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_Ex.URL.PriceMOQ_Province_NotInList,
            readparam: function () { return { priceExID: $scope.ItemPriceEx.ID, contractTermID: _VENContract_Price_DI_Ex.Params.TermID, CustomerID: _VENContract_Price_DI_Ex.Params.CustomerID } },
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
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceMOQ_Province_Save,
                data: { priceExID: $scope.ItemPriceEx.ID, lst: datasend },
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
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_Ex.URL.PriceMOQ_Province_Delete,
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