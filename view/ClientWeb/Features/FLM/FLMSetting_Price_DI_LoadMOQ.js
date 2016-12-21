/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSetting_Price_DI_LoadMOQ = {
    URL: {
        //#region price ex
        loadMOQ_List: 'FLMPrice_DI_PriceMOQLoad_List',
        loadMOQ_Get: 'FLMPrice_DI_PriceMOQLoad_Get',
        loadMOQ_Save: 'FLMPrice_DI_PriceMOQLoad_Save',
        loadMOQ_Delete: 'FLMPrice_DI_PriceMOQLoad_Delete',

        LoadMOQ_GroupLocation_List: 'FLMPrice_DI_PriceMOQLoad_GroupLocation_List',
        LoadMOQ_GroupLocation_Delete: 'FLMPrice_DI_PriceMOQLoad_GroupLocation_DeleteList',
        LoadMOQ_GroupLocation_Save: 'FLMPrice_DI_PriceMOQLoad_GroupLocation_SaveList',
        LoadMOQ_GroupLocation_NotInList: 'FLMPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList',

        LoadMOQ_GroupProduct_List: 'FLMPrice_DI_PriceMOQLoad_GroupProduct_List',
        LoadMOQ_GroupProduct_Delete: 'FLMPrice_DI_PriceMOQLoad_GroupProduct_DeleteList',
        LoadMOQ_GroupProduct_Save: 'FLMPrice_DI_PriceMOQLoad_GroupProduct_Save',
        LoadMOQ_GroupProduct_Get: 'FLMPrice_DI_PriceMOQLoad_GroupProduct_Get',
        LoadMOQ_GroupProduct_GOPList: 'FLMPrice_DI_PriceMOQLoad_GroupProduct_GOPList',

        LoadMOQ_Location_List: 'FLMPrice_DI_PriceMOQLoad_Location_List',
        LoadMOQ_Location_Delete: 'FLMPrice_DI_PriceMOQLoad_Location_DeleteList',
        LoadMOQ_Location_NotInSave: 'FLMPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList',
        LoadMOQ_Location_NotInList: 'FLMPrice_DI_PriceMOQLoad_Location_LocationNotInList',
        LoadMOQ_Location_Save: 'FLMPrice_DI_PriceMOQLoad_Location_Save',
        LoadMOQ_Location_Get: 'FLMPrice_DI_PriceMOQLoad_Location_Get',

        LoadMOQ_Route_List: 'FLMPrice_DI_PriceMOQLoad_Route_List',
        LoadMOQ_Route_Delete: 'FLMPrice_DI_PriceMOQLoad_Route_DeleteList',
        LoadMOQ_Route_Save: 'FLMPrice_DI_PriceMOQLoad_Route_SaveList',
        LoadMOQ_Route_NotInList: 'FLMPrice_DI_PriceMOQLoad_Route_RouteNotInList',

        LoadMOQ_ParentRoute_List: 'FLMPrice_DI_PriceMOQLoad_ParentRoute_List',
        LoadMOQ_ParentRoute_Delete: 'FLMPrice_DI_PriceMOQLoad_ParentRoute_DeleteList',
        LoadMOQ_ParentRoute_Save: 'FLMPrice_DI_PriceMOQLoad_ParentRoute_SaveList',
        LoadMOQ_ParentRoute_NotInList: 'FLMPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList',

        PriceMOQ_Province_List: 'FLMPrice_DI_PriceMOQLoad_Province_List',
        PriceMOQ_Province_Delete: 'FLMPrice_DI_PriceMOQLoad_Province_DeleteList',
        PriceMOQ_Province_Save: 'FLMPrice_DI_PriceMOQLoad_Province_SaveList',
        PriceMOQ_Province_NotInList: 'FLMPrice_DI_PriceMOQLoad_Province_NotInList',
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

angular.module('myapp').controller('FLMSetting_Price_DI_LoadMOQCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_Price_DI_LoadMOQCtrl');
    $rootScope.IsLoading = false;
    _FLMSetting_Price_DI_LoadMOQ.Params = $.extend({}, true, $state.params);
    $scope.ItemLoadMOQ = { ID: 0 };

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
    //moq
    $scope.ItemPriceMOQ = { ID: 0 };
    $scope.GLMOQHasChoose = false;
    $scope.GLMOQNotInHasChoose = false;
    $scope.TabIndexMOQ = 0;
    $scope.MOQLocationHasChoose = false;
    $scope.MOQLocationNotInHasChoose = false
    $scope.MOQParentRouteHasChoose = false;
    $scope.MOQParentRouteNotInHasChoose = false;

    //$rootScope.IsLoading = true;
    //Common.Services.Call($http, {
    //    url: Common.Services.url.FLM,
    //    method: _FLMSetting_Price_DI_LoadMOQ.URL.loadMOQ_Get,
    //    data: { priceID: _FLMSetting_Price_DI_LoadMOQ.Params.ID },
    //    success: function (res) {
    //        $scope.ItemLoadMOQ = res;
    //        $rootScope.IsLoading = false;
    //        $scope.load_MOQ_tabstrip.select(0);
    //        $scope.loadMOQ_GroupLocation_GridOptions.dataSource.read();
    //        $scope.loadMOQ_GroupProduct_GridOptions.dataSource.read();
    //        $scope.loadMOQ_Location_GridOptions.dataSource.read();
    //        $scope.loadMOQ_Route_GridOptions.dataSource.read();
    //        $scope.loadMOQ_ParentRoute_GridOptions.dataSource.read();
    //    },
    //    error: function (res) {
    //        $rootScope.IsLoading = false;
    //    }
    //})

    $scope.load_MOQ_grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.loadMOQ_List,
            readparam: function () { return { priceID: _FLMSetting_Price_DI_LoadMOQ.Params.PriceID } },
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
                template: '<a href="/" ng-click="loadMOQ_GridEdit_Click($event,load_MOQ_Edit_win,dataItem,loadMOQ_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="loadMOQ_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'MOQName', title: 'Tên MOQ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DIMOQLoadSumName', width: 250, title: 'Loại tính tổng', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.load_MOQ_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadMOQLoadItem(0, win, vform)
    }

    $scope.loadMOQ_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadMOQLoadItem(data.ID, win, vform)
    }

    $scope.LoadMOQLoadItem = function (id, win, vform) {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.loadMOQ_Get,
            data: { priceID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemLoadMOQ = res;
                win.center().open();
                vform({ clear: true })
                $scope.load_MOQ_tabstrip.select(0);
                $scope.loadMOQ_GroupLocation_GridOptions.dataSource.read();
                $scope.loadMOQ_GroupProduct_GridOptions.dataSource.read();
                $scope.loadMOQ_Location_GridOptions.dataSource.read();
                $scope.loadMOQ_Route_GridOptions.dataSource.read();
                $scope.loadMOQ_ParentRoute_GridOptions.dataSource.read();
                $scope.priceEx_Province_GridOptions.dataSource.read()
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.loadMOQ_GridDestroy_Click = function ($event, data) {
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
                    method: _FLMSetting_Price_DI_LoadMOQ.URL.loadMOQ_Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.load_MOQ_grid_Options.dataSource.read();
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

        $state.go("main.FLMSetting.PriceDI", _FLMSetting_Price_DI_LoadMOQ.Params)
    }

    //#region
    $scope.load_MOQ_tabstripOptions = {
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
        url: Common.ALL.URL.SYSVarDIMOQLoadSum,
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

    $scope.loadMOQ_SaveInfo_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemLoadMOQ.DIMOQLoadSumID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Price_DI_LoadMOQ.URL.loadMOQ_Save,
                    data: { priceID: _FLMSetting_Price_DI_LoadMOQ.Params.PriceID, item: $scope.ItemLoadMOQ },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMSetting_Price_DI_LoadMOQ.URL.loadMOQ_Get,
                            data: { priceID: res },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.ItemLoadMOQ = res;
                                $scope.loadMOQ_GroupLocation_GridOptions.dataSource.read();
                                $scope.loadMOQ_GroupProduct_GridOptions.dataSource.read();
                                $scope.loadMOQ_Location_GridOptions.dataSource.read();
                                $scope.loadMOQ_Route_GridOptions.dataSource.read();
                                $scope.loadMOQ_ParentRoute_GridOptions.dataSource.read();
                                $scope.load_MOQ_grid_Options.dataSource.read();
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

    $scope.loadMOQ_DeleteInfo_Click = function ($event) {
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
                    method: _FLMSetting_Price_DI_LoadMOQ.URL.loadMOQ_Delete,
                    data: { id: $scope.ItemLoadMOQ.ID },
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
    $scope.loadMOQ_GroupLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupLocation_List,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,loadMOQ_GroupLocation_Grid,loadMOQ_GroupLocation_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,loadMOQ_GroupLocation_Grid,loadMOQ_GroupLocation_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'GroupOfLocationCode', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfLocationName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.loadMOQ_GroupLocation_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupLocationHasChoose = hasChoose;
    }

    $scope.loadMOQ_GroupLocation_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.load_MOQ_GroupNotIn_GridOptions.dataSource.read();
    }

    $scope.loadMOQ_GroupLocation_Delete = function ($event, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupLocation_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_GroupLocation_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.load_MOQ_GroupNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupLocation_NotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,load_MOQ_GroupNotIn_Grid,load_MOQ_GroupNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,load_MOQ_GroupNotIn_Grid,load_MOQ_GroupNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.load_MOQ_GroupNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GLMOQNotInHasChoose = hasChoose;
    }

    $scope.load_MOQ_GroupNotIn_Save_Click = function ($event, win, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupLocation_Save,
                data: { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_GroupLocation_GridOptions.dataSource.read();
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
    $scope.loadMOQ_GroupProduct_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupProduct_List,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID } },
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
                template: '<a href="/" ng-click="loadMOQ_GroupProduct_GridEdit_Click($event,load_MOQ_GroupProduct_win,dataItem,loadMOQGOP_vform)" ng-show="!ItemLoadMOQ.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="loadMOQ_GroupProduct_GridDestroy_Click($event,dataItem)" ng-show="!ItemLoadMOQ.TermClosed" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'GroupOfProductCode', title: "Mã nhóm hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', title: "Tên nhóm hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprPrice', title: "Công thức giá", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ExprQuantity', title: "Công thức số lượng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.loadMOQ_GroupProduct_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.GOPLoadItem(0, win, vform)
    }

    $scope.loadMOQ_GroupProduct_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.GOPLoadItem(data.ID, win, vform)
    }

    $scope.GOPLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupProduct_Get,
            data: { id: id, cusID: _FLMSetting_Price_DI_LoadMOQ.Params.CustomerID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemLoadMOQGOP = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.loadMOQ_GroupProduct_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupProduct_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_GroupProduct_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.load_MOQ_GOP_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemLoadMOQGOP.GroupOfProductID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupProduct_Save,
                    data: { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, item: $scope.ItemLoadMOQGOP },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.loadMOQ_GroupProduct_GridOptions.dataSource.read();
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
        url: Common.Services.url.FLM,
        method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_GroupProduct_GOPList,
        data: { cusID: _FLMSetting_Price_DI_LoadMOQ.Params.CustomerID },
        success: function (res) {
            $scope.cboGroupOfProduct_Options.dataSource.data(res.Data)
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    //#endregion

    //#region  location

    $scope.loadMOQ_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Location_List,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,loadMOQ_Location_Grid,loadMOQ_Location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,loadMOQ_Location_Grid,loadMOQ_Location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Location_GridEdit_Click($event,Location_win,dataItem)" ng-show="!ItemPriceEx.TermClosed" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'LocationCode', title: "Mã địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: "Tên địa điểm", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
		    { field: 'ProvinceName', title: "Tỉnh thành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: "Quận huyện", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfTOLocationName', title: "Loại", width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.loadMOQ_Location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.loadMOQ_Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.load_MOQ_LocationNotIn_GridOptions.dataSource.read();
    }

    $scope.loadMOQ_Location_Delete = function ($event, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Location_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.load_MOQ_LocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Location_NotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, customerID: _FLMSetting_Price_DI_LoadMOQ.Params.CustomerID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,load_MOQ_LocationNotIn_Grid,load_MOQ_LocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,load_MOQ_LocationNotIn_Grid,load_MOQ_LocationNotIn_GridChoose_Change)" />',
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
    $scope.load_MOQ_LocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.MOQLocationNotInHasChoose = hasChoose;
    }

    $scope.load_MOQ_LocationNotIn_Save_Click = function ($event, win, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Location_NotInSave,
                data: { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_Location_GridOptions.dataSource.read();
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
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Location_Get,
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
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Location_Save,
            data: { item: $scope.ItemLocation },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.loadMOQ_Location_GridOptions.dataSource.read();
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
    $scope.loadMOQ_Route_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Route_List,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,loadMOQ_Route_Grid,loadMOQ_Route_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,loadMOQ_Route_Grid,loadMOQ_Route_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.loadMOQ_Route_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteHasChoose = hasChoose;
    }

    $scope.loadMOQ_Route_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.load_MOQ_RouteNotIn_GridOptions.dataSource.read();
    }

    $scope.load_MOQ_RouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Route_NotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, contractTermID: _FLMSetting_Price_DI_LoadMOQ.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,load_MOQ_RouteNotIn_Grid,load_MOQ_RouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,load_MOQ_RouteNotIn_Grid,load_MOQ_RouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.load_MOQ_RouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteNotInHasChoose = hasChoose;
    }

    $scope.load_MOQ_RouteNotIn_Save_Click = function ($event, win, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Route_Save,
                data: { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_Route_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.loadMOQ_Route_Delete = function ($event, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_Route_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_Route_GridOptions.dataSource.read();
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
    $scope.loadMOQ_ParentRoute_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_ParentRoute_List,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,loadMOQ_ParentRoute_Grid,loadMOQ_ParentRoute_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,loadMOQ_ParentRoute_Grid,loadMOQ_ParentRoute_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ParentRoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ParentRoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.loadMOQ_ParentRoute_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteHasChoose = hasChoose;
    }

    $scope.loadMOQ_ParentRoute_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.load_MOQ_ParentRouteNotIn_GridOptions.dataSource.read();
    }

    $scope.load_MOQ_ParentRouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_ParentRoute_NotInList,
            readparam: function () { return { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, contractTermID: _FLMSetting_Price_DI_LoadMOQ.Params.TermID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,load_MOQ_ParentRouteNotIn_Grid,load_MOQ_ParentRouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,load_MOQ_ParentRouteNotIn_Grid,load_MOQ_ParentRouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.load_MOQ_ParentRouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteNotInHasChoose = hasChoose;
    }

    $scope.load_MOQ_ParentRouteNotIn_Save_Click = function ($event, win, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_ParentRoute_Save,
                data: { PriceMOQLoadID: $scope.ItemLoadMOQ.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_ParentRoute_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.loadMOQ_ParentRoute_Delete = function ($event, grid) {
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.LoadMOQ_ParentRoute_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.loadMOQ_ParentRoute_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.EXpr_Content = "";
    $scope.Expr_current = "";
    $scope.Btn_Expr_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.Expr_current = data;
        switch (data) {
            case "ExprInput":
                $scope.EXpr_Content = $scope.ItemLoadMOQ.ExprInput; break;
            case "ExprCBM":
                $scope.EXpr_Content = $scope.ItemLoadMOQ.ExprCBM; break;
            case "ExprTon":
                $scope.EXpr_Content = $scope.ItemLoadMOQ.ExprTon; break;
            case "ExprQuan":
                $scope.EXpr_Content = $scope.ItemLoadMOQ.ExprQuan; break;
            case "ExprPrice":
                $scope.EXpr_Content = $scope.ItemLoadMOQ.ExprPrice; break;
            case "ExprPriceFix":
                $scope.EXpr_Content = $scope.ItemLoadMOQ.ExprPriceFix; break;
        }
        $scope.LoadNote($scope.ItemLoadMOQ.DIMOQLoadSumID);
        win.open().center();
    }

    $scope.Btn_Expr_Save_Click = function ($event, win) {
        $event.preventDefault();
        switch ($scope.Expr_current) {
            case "ExprInput":
                $scope.ItemLoadMOQ.ExprInput = $scope.EXpr_Content; break;
            case "ExprCBM":
                $scope.ItemLoadMOQ.ExprCBM = $scope.EXpr_Content; break;
            case "ExprTon":
                $scope.ItemLoadMOQ.ExprTon = $scope.EXpr_Content; break;
            case "ExprQuan":
                $scope.ItemLoadMOQ.ExprQuan = $scope.EXpr_Content; break;
            case "ExprPrice":
                $scope.ItemLoadMOQ.ExprPrice = $scope.EXpr_Content; break;
            case "ExprPriceFix":
                $scope.ItemLoadMOQ.ExprPriceFix = $scope.EXpr_Content; break;
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
            url: Common.Services.url.FLM,
            method: _FLMSetting_Price_DI_LoadMOQ.URL.PriceMOQ_Province_List,
            readparam: function () { return { PriceDIMOQLoadID: $scope.ItemLoadMOQ.ID } },
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
            method: _FLMSetting_Price_DI_LoadMOQ.URL.PriceMOQ_Province_NotInList,
            readparam: function () { return { PriceDIMOQLoadID: $scope.ItemLoadMOQ.ID, contractTermID: _FLMSetting_Price_DI_LoadMOQ.Params.TermID, CustomerID: _FLMSetting_Price_DI_LoadMOQ.Params.CustomerID } },
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.PriceMOQ_Province_Save,
                data: { PriceDIMOQLoadID: $scope.ItemLoadMOQ.ID, lst: datasend },
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
                method: _FLMSetting_Price_DI_LoadMOQ.URL.PriceMOQ_Province_Delete,
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