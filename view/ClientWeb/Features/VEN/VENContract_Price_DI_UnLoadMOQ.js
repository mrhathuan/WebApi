/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _VENContract_Price_DI_UnLoadMOQ = {
    URL: {
        Price_Get: 'VENContract_Price_Get',
        Price_Data: 'VENContract_Price_Data',

        List: 'VENPrice_DI_PriceMOQUnLoad_List',
        Get: 'VENPrice_DI_PriceMOQUnLoad_Get',
        Delete: 'VENPrice_DI_PriceMOQUnLoad_Delete',
        Save: 'VENPrice_DI_PriceMOQUnLoad_Save',

        GroupLocation_List: 'VENPrice_DI_PriceMOQLoad_GroupLocation_List',
        GroupLocation_Delete: 'VENPrice_DI_PriceMOQLoad_GroupLocation_DeleteList',
        GroupLocation_Save: 'VENPrice_DI_PriceMOQLoad_GroupLocation_SaveList',
        GroupLocation_GrouNotInList: 'VENPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList',

        GroupProduct_List: 'VENPrice_DI_PriceMOQLoad_GroupProduct_List',
        GroupProduct_Delete: 'VENPrice_DI_PriceMOQLoad_GroupProduct_DeleteList',
        GroupProduct_Save: 'VENPrice_DI_PriceMOQLoad_GroupProduct_Save',
        GroupProduct_Get: 'VENPrice_DI_PriceMOQLoad_GroupProduct_Get',
        GroupProduct_GOPList: 'VENPrice_DI_PriceMOQLoad_GroupProduct_GOPList',

        Location_List: 'VENPrice_DI_PriceMOQLoad_Location_List',
        Location_Delete: 'VENPrice_DI_PriceMOQLoad_Location_DeleteList',
        Location_Save: 'VENPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList',
        Location_NotInList: 'VENPrice_DI_PriceMOQLoad_Location_LocationNotInList',
        LoadMOQ_Location_Save: 'VENPrice_DI_PriceMOQLoad_Location_Save',
        LoadMOQ_Location_Get: 'VENPrice_DI_PriceMOQLoad_Location_Get',

        Route_List: 'VENPrice_DI_PriceMOQLoad_Route_List',
        Route_Delete: 'VENPrice_DI_PriceMOQLoad_Route_DeleteList',
        Route_Save: 'VENPrice_DI_PriceMOQLoad_Route_SaveList',
        Route_GrouNotInList: 'VENPrice_DI_PriceMOQLoad_Route_RouteNotInList',

        ParentRoute_List: 'VENPrice_DI_PriceMOQLoad_ParentRoute_List',
        ParentRoute_Delete: 'VENPrice_DI_PriceMOQLoad_ParentRoute_DeleteList',
        ParentRoute_Save: 'VENPrice_DI_PriceMOQLoad_ParentRoute_SaveList',
        ParentRoute_GrouNotInList: 'VENPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList',

        PriceMOQ_Province_List: 'VENPrice_DI_PriceMOQLoad_Province_List',
        PriceMOQ_Province_Delete: 'VENPrice_DI_PriceMOQLoad_Province_DeleteList',
        PriceMOQ_Province_Save: 'VENPrice_DI_PriceMOQLoad_Province_SaveList',
        PriceMOQ_Province_NotInList: 'VENPrice_DI_PriceMOQLoad_Province_NotInList',

    },
    Data: {

    },
    Params: {
        PriceID: -1,
        TermID: -1,
        CustomerID: -1,
    }
}

angular.module('myapp').controller('VENContract_Price_DI_UnLoadMOQCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('VENContract_Price_DI_UnLoadMOQCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = { ID: 0 };
    $scope.TabIndex = 0;

    _VENContract_Price_DI_UnLoadMOQ.Params = $.extend({}, true, $state.params);

    $scope.GroupLocationHasChoose = false;
    $scope.GroupLocationNotInHasChoose = false;
    $scope.LocationHasChoose = false;
    $scope.LocationNotInHasChoose = false;
    $scope.RouteHasChoose = false;
    $scope.RouteNotInHasChoose = false;
    $scope.ParentRouteHasChoose = false;
    $scope.ParentRouteNotInHasChoose = false;

    //Common.Services.Call($http, {
    //    url: Common.Services.url.VEN,
    //    method: _VENContract_Price_DI_UnLoadMOQ.URL.Get,
    //    data: { priceID: _VENContract_Price_DI_UnLoadMOQ.Params.ID },
    //    success: function (res) {
    //        $scope.Item = res;
    //        if (res.ID > 0) {
    //            $scope.unloadMOQ_GroupLocation_GridOptions.dataSource.read();
    //            $scope.unloadMOQ_GroupProduct_GridOptions.dataSource.read();
    //            $scope.unloadMOQ_Location_GridOptions.dataSource.read();
    //            $scope.unloadMOQ_Route_GridOptions.dataSource.read();
    //            $scope.unloadMOQ_ParentRoute_GridOptions.dataSource.read();
    //        }
    //    }
    //});
    $scope.unload_MOQ_grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.List,
            readparam: function () { return { priceID: _VENContract_Price_DI_UnLoadMOQ.Params.PriceID } },
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
                template: '<a href="/" ng-click="unloadMOQ_GridEdit_Click($event,unload_MOQ_Edit_win,dataItem,unloadMOQ_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="unloadMOQ_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'MOQName', title: 'Tên MOQ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DIMOQLoadSumName', width: 250, title: 'Loại tính tổng', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.unload_MOQ_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.unLoadMOQLoadItem(0, win, vform)
    }

    $scope.unloadMOQ_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.unLoadMOQLoadItem(data.ID, win, vform)
    }

    $scope.unLoadMOQLoadItem = function (id, win, vform) {
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.Get,
            data: { priceID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                win.center().open();
                vform({ clear: true })
                $scope.unloadMOQ_tabstrip.select(0);
                $scope.unloadMOQ_GroupLocation_GridOptions.dataSource.read();
                $scope.unloadMOQ_GroupProduct_GridOptions.dataSource.read();
                $scope.unloadMOQ_Location_GridOptions.dataSource.read();
                $scope.unloadMOQ_Route_GridOptions.dataSource.read();
                $scope.unloadMOQ_ParentRoute_GridOptions.dataSource.read();
                $scope.priceEx_Province_GridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.unloadMOQ_GridDestroy_Click = function ($event, data) {
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
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_DI_UnLoadMOQ.URL.Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.unload_MOQ_grid_Options.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.VENContract.PriceDI", _VENContract_Price_DI_UnLoadMOQ.Params)
    }

    $scope.unloadMOQ_tabstripOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }

    //#region info
    $scope.cboTypeDIMOQLoadSum_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                TypeName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarDIMOQLoadSum,
        success: function (data) {
            $scope.cboTypeDIMOQLoadSum_Options.dataSource.data(data)
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

    $scope.unloadMOQ_SaveInfo_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.Item.DIMOQLoadSumID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_DI_UnLoadMOQ.URL.Save,
                    data: { priceID: _VENContract_Price_DI_UnLoadMOQ.Params.PriceID, item: $scope.Item },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.VEN,
                            method: _VENContract_Price_DI_UnLoadMOQ.URL.Get,
                            data: { priceID: res },
                            success: function (res) {
                                $scope.Item = res;
                                $scope.unloadMOQ_GroupLocation_GridOptions.dataSource.read();
                                $scope.unloadMOQ_GroupProduct_GridOptions.dataSource.read();
                                $scope.unloadMOQ_Location_GridOptions.dataSource.read();
                                $scope.unloadMOQ_Route_GridOptions.dataSource.read();
                                $scope.unloadMOQ_ParentRoute_GridOptions.dataSource.read();
                                $rootScope.IsLoading = false;
                                $scope.unload_MOQ_grid_Options.dataSource.read();
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

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region group location
    $scope.unloadMOQ_GroupLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupLocation_List,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_GroupLocation_Grid,unloadMOQ_GroupLocation_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_GroupLocation_Grid,unloadMOQ_GroupLocation_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'GroupOfLocationCode', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfLocationName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.unloadMOQ_GroupLocation_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupLocationHasChoose = hasChoose;
    }
    $scope.unloadMOQ_GroupLocation_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.unloadMOQ_GroupNotIn_GridOptions.dataSource.read();
    }

    $scope.unloadMOQ_GroupLocation_Delete = function ($event, grid) {
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupLocation_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_GroupLocation_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    $scope.unloadMOQ_GroupNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupLocation_GrouNotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_GroupNotIn_Grid,unloadMOQ_GroupNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_GroupNotIn_Grid,unloadMOQ_GroupNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.unloadMOQ_GroupNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupLocationNotInHasChoose = hasChoose;
    }

    $scope.unloadMOQ_GroupNotIn_Save_Click = function ($event, win, grid) {
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupLocation_Save,
                data: { PriceMOQLoadID: $scope.Item.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_GroupLocation_GridOptions.dataSource.read();
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

    //#region group product
    $scope.unloadMOQ_GroupProduct_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupProduct_List,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID } },
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
                template: '<a href="/" ng-click="unloadMOQ_GroupProduct_GridEdit_Click($event,unloadMOQ_GroupProduct_win,dataItem,priceMOQGOP_vform)" ng-show="!Item.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="unloadMOQ_GroupProduct_GridDestroy_Click($event,dataItem)" ng-show="!Item.TermClosed" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'GroupOfProductCode', title: "Mã nhóm hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', title: "Tên nhóm hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprPrice', title: "Công thức giá", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprQuantity', title: "Công thức số lượng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.unloadMOQ_GroupProduct_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.GOPLoadItem(0, win, vform)
    }

    $scope.unloadMOQ_GroupProduct_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.GOPLoadItem(data.ID, win, vform)
    }

    $scope.GOPLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupProduct_Get,
            data: { id: id, cusID: _VENContract_Price_DI_UnLoadMOQ.Params.CustomerID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemGOP = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.unloadMOQ_GroupProduct_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupProduct_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_GroupProduct_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.unloadMOQ_GOP_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemGOP.GroupOfProductID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupProduct_Save,
                    data: { PriceMOQLoadID: $scope.Item.ID, item: $scope.ItemGOP },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.unloadMOQ_GroupProduct_GridOptions.dataSource.read();
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
        method: _VENContract_Price_DI_UnLoadMOQ.URL.GroupProduct_GOPList,
        data: { cusID: _VENContract_Price_DI_UnLoadMOQ.Params.CustomerID },
        success: function (res) {
            $scope.cboGroupOfProduct_Options.dataSource.data(res.Data)
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });
    //#endregion

    //#region location
    $scope.unloadMOQ_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.Location_List,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_Location_Grid,unloadMOQ_Location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_Location_Grid,unloadMOQ_Location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Location_GridEdit_Click($event,Location_win,dataItem)" ng-show="!ItemPriceEx.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'LocationCode', title: "Mã địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: "Tên địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
		    { field: 'ProvinceName', title: "Tỉnh thành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: "Quận huyện", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfTOLocationName', title: "Loại", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.unloadMOQ_Location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.unloadMOQ_Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.unloadMOQ_LocationNotIn_GridOptions.dataSource.read();
    }

    $scope.unloadMOQ_Location_Delete = function ($event, grid) {
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.Location_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.unloadMOQ_LocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.Location_NotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID, customerID: _VENContract_Price_DI_UnLoadMOQ.Params.CustomerID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_LocationNotIn_Grid,unloadMOQ_LocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_LocationNotIn_Grid,unloadMOQ_LocationNotIn_GridChoose_Change)" />',
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
    $scope.unloadMOQ_LocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationNotInHasChoose = hasChoose;
    }

    $scope.unloadMOQ_LocationNotIn_Save_Click = function ($event, win, grid) {
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.Location_Save,
                data: { PriceMOQLoadID: $scope.Item.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_Location_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.ItemLocation = null;

    $scope.Location_GridEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.LoadMOQ_Location_Get,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemLocation = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.Location_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.LoadMOQ_Location_Save,
            data: { item: $scope.ItemLocation },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.unloadMOQ_Location_GridOptions.dataSource.read();
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

    //#region route
    $scope.unloadMOQ_Route_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.Route_List,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_Route_Grid,unloadMOQ_Route_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_Route_Grid,unloadMOQ_Route_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.unloadMOQ_Route_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteHasChoose = hasChoose;
    }

    $scope.unloadMOQ_Route_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.unloadMOQ_RouteNotIn_GridOptions.dataSource.read();
    }

    $scope.unloadMOQ_RouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.Route_GrouNotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID, contractTermID: _VENContract_Price_DI_UnLoadMOQ.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_RouteNotIn_Grid,unloadMOQ_RouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_RouteNotIn_Grid,unloadMOQ_RouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.unloadMOQ_RouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteNotInHasChoose = hasChoose;
    }

    $scope.unloadMOQ_RouteNotIn_Save_Click = function ($event, win, grid) {
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.Route_Save,
                data: { PriceMOQLoadID: $scope.Item.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_Route_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.unloadMOQ_Route_Delete = function ($event, grid) {
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.Route_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_Route_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region parent route
    $scope.unloadMOQ_ParentRoute_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.ParentRoute_List,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_ParentRoute_Grid,unloadMOQ_ParentRoute_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_ParentRoute_Grid,unloadMOQ_ParentRoute_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ParentRoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ParentRoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.unloadMOQ_ParentRoute_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteHasChoose = hasChoose;
    }

    $scope.unloadMOQ_ParentRoute_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.unloadMOQ_ParentRouteNotIn_GridOptions.dataSource.read();
    }

    $scope.unloadMOQ_ParentRouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.ParentRoute_GrouNotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.Item.ID, contractTermID: _VENContract_Price_DI_UnLoadMOQ.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,unloadMOQ_ParentRouteNotIn_Grid,unloadMOQ_ParentRouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,unloadMOQ_ParentRouteNotIn_Grid,unloadMOQ_ParentRouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.unloadMOQ_ParentRouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteNotInHasChoose = hasChoose;
    }

    $scope.unloadMOQ_ParentRouteNotIn_Save_Click = function ($event, win, grid) {

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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.ParentRoute_Save,
                data: { PriceMOQLoadID: $scope.Item.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_ParentRoute_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.unloadMOQ_ParentRoute_Delete = function ($event, grid) {
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.Route_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.unloadMOQ_RouteNotIn_GridOptions.dataSource.read();
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
                $scope.EXpr_Content = $scope.Item.ExprInput; break;
            case "ExprCBM":
                $scope.EXpr_Content = $scope.Item.ExprCBM; break;
            case "ExprTon":
                $scope.EXpr_Content = $scope.Item.ExprTon; break;
            case "ExprQuan":
                $scope.EXpr_Content = $scope.Item.ExprQuan; break;
            case "ExprPrice":
                $scope.EXpr_Content = $scope.Item.ExprPrice; break;
            case "ExprPriceFix":
                $scope.EXpr_Content = $scope.Item.ExprPriceFix; break;
        }
        $scope.LoadNote($scope.Item.DIMOQLoadSumID);
        win.open().center();
    }

    $scope.Btn_Expr_Save_Click = function ($event, win) {
        $event.preventDefault();
        switch ($scope.Expr_current) {
            case "ExprInput":
                $scope.Item.ExprInput = $scope.EXpr_Content; break;
            case "ExprCBM":
                $scope.Item.ExprCBM = $scope.EXpr_Content; break;
            case "ExprTon":
                $scope.Item.ExprTon = $scope.EXpr_Content; break;
            case "ExprQuan":
                $scope.Item.ExprQuan = $scope.EXpr_Content; break;
            case "ExprPrice":
                $scope.Item.ExprPrice = $scope.EXpr_Content; break;
            case "ExprPriceFix":
                $scope.Item.ExprPriceFix = $scope.EXpr_Content; break;
        }
        win.close();
    }

    $scope.show_Note = "";
    $scope.LoadNote = function (key) {
        //var DIMOQLoadSumOrderInDay = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [TotalSchedule - Tổng chuyến] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận]";
        //var DIMOQLoadSumScheduleInDay = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận]";
        //var DIMOQLoadSumOrder = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [GOVCodeOrder - Mã loại xe đơn hàng] [UnitPriceMin Đơn giá nhỏ nhất] [UnitPriceMax - Đơn giá lớn nhất] [UnitPrice - Đơn giá]";
        //var DIMOQLoadSumSchedule = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [GOVCodeSchedule - Mã loại xe chuyến] [UnitPriceMin Đơn giá nhỏ nhất] [UnitPriceMax - Đơn giá lớn nhất] [UnitPrice - Đơn giá]";
        //var DIMOQLoadSumReturnOrder = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [UnitPriceMin Đơn giá nhỏ nhất] [UnitPriceMax - Đơn giá lớn nhất] [UnitPrice - Đơn giá]";
        //var DIMOQLoadSumOrderRoute = "[TonTransfer - Tấn chở] [CBMTransfer - Khối chở] [QuantityTransfer - SL chở] [TonReturn - Tấn trả về] [CBMReturn - Khối trả về][QuantityReturn - SL trả về] [Credit - Doanh thu] [DropPoint - Số điểm giao] [GetPoint - Số điểm nhận] [UnitPriceMin Đơn giá nhỏ nhất] [UnitPriceMax - Đơn giá lớn nhất] [UnitPrice - Đơn giá] [GOVCodeSchedule - Mã loại xe chuyến]";
        //switch (key + "") {
        //    case "314": $scope.Content_Note = DIMOQLoadSumOrder; break;
        //    case "315": $scope.Content_Note = ""; break;
        //    case "316": $scope.Content_Note = DIMOQLoadSumOrderRoute; break;
        //    case "317": $scope.Content_Note = DIMOQLoadSumOrderInDay; break;
        //    case "318": $scope.Content_Note = DIMOQLoadSumSchedule; break;
        //    case "319": $scope.Content_Note = DIMOQLoadSumScheduleInDay; break;
        //    case "320": $scope.Content_Note = DIMOQLoadSumReturnOrder; break;
        //    case "321": $scope.Content_Note = ""; break;
        //}
        $scope.show_Note = key;
    }

    //#region Province
    $scope.priceEx_Province_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_DI_UnLoadMOQ.URL.PriceMOQ_Province_List,
            readparam: function () { return { PriceDIMOQLoadID: $scope.Item.ID } },
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
            method: _VENContract_Price_DI_UnLoadMOQ.URL.PriceMOQ_Province_NotInList,
            readparam: function () { return { PriceDIMOQLoadID: $scope.Item.ID, contractTermID: _VENContract_Price_DI_UnLoadMOQ.Params.TermID, CustomerID: _VENContract_Price_DI_UnLoadMOQ.Params.CustomerID } },
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.PriceMOQ_Province_Save,
                data: { PriceDIMOQLoadID: $scope.Item.ID, lst: datasend },
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
                method: _VENContract_Price_DI_UnLoadMOQ.URL.PriceMOQ_Province_Delete,
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