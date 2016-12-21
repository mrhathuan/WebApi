/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/views.js" />

//#region Data
var _ORDOrderRouteDetail = {
    URL: {
        Get: 'ORDOrderRoute_Get',
        Save: 'ORDOrderRoute_Save',
        Delete: 'ORDOrderRoute_Delete',

        Order_List: 'ORDOrderRoute_OrderList',
        Order_SaveList: 'ORDOrderRoute_OrderSaveList',
        Order_Delete: 'ORDOrderRoute_OrderDelete',
        Order_NotInList: 'ORDOrderRoute_OrderNotInList',
        Order_Approved: 'ORDOrderRoute_OrderApproved',
        Order_UnApproved: 'ORDOrderRoute_OrderUnApproved',

        Route_List: 'ORDOrderRoute_RouteDetailList',
        Route_Save: 'ORDOrderRoute_RouteDetailSave',
        Route_Get: 'ORDOrderRoute_RouteDetailGet',
        Route_Delete: 'ORDOrderRoute_RouteDetailDelete',
        Route_AddVessel: 'ORDOrderRoute_RouteDetail_AddVessel',

        Location_data: 'ORDOrderRoute_LocationData',
        Vessel_List: 'ORDOrderRoute_VesselList',

        Route_Complete: 'ORDOrderRoute_RouteDetailComplete',
        Route_Run: "ORDOrderRoute_RouteDetailRun",
        CreateOrderChilds: 'ORDOrderRoute_CreateOrderChilds',
        ClearOrderChilds: 'ORDOrderRoute_ClearOrderChilds',

        Route_ContainerList: 'ORDOrderRoute_RouteDetail_RouteContainerList',
        Route_ContainerGet: 'ORDOrderRoute_RouteDetail_RouteContainerGet',
        Route_ContainerSave: 'ORDOrderRoute_RouteDetail_RouteContainerSave',
        Route_ContainerDelete: 'ORDOrderRoute_RouteDetail_RouteContainerDetele',

        Route_ProductList: 'ORDOrderRoute_RouteDetail_RouteProductList',
        Route_ProductNotinList: 'ORDOrderRoute_RouteDetail_RouteProductNotInList',
        Route_ProductSaveList: 'ORDOrderRoute_RouteDetail_RouteProductNotInSaveList',
        Route_ProductUpdateCont: 'ORDOrderRoute_RouteDetail_RouteProduct_UpdateContainer',
        Route_ProductContList: 'ORDOrderRoute_RouteDetail_RouteProduct_ContainerList',
        //Route_ProductDelete: 'ORDOrderRoute_RouteDetail_RouteContainerDetele',
    },
    Data: {
        Partner: null,
        Location: null,
        Vessel: null
    }
};
//#endregion

angular.module('myapp').controller('ORDOrder_RouteDetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_RouteDetailCtrl');
    $rootScope.IsLoading = false;

    $scope.Order_NotinHasChoose = false;
    $scope.TabIndex = 1;
    $scope.TabIndexDetailLTLtoFCL = 1;
    $scope.RouteDetailID = -1;
    $scope.RouteGOPID = -1;

    $scope.LocationFromHasChoose = false;
    $scope.ProductHasChoose = false;

    $scope.ParamID = $stateParams.OrderRouteID;
    $scope.Auth = $rootScope.GetAuth();

    $scope.ItemRoute = { ID: -1, PartnerID: -1 }


    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrderRouteDetail.URL.Get,
        data: { id: $scope.ParamID },
        success: function (res) {
            $rootScope.IsLoading = false;
            $scope.ItemOrder = res;

        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    $scope.main_tabstripOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
                Common.Log($scope.TabIndex)
            }, 1);
        }
    };

    $scope.info_Delete = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đa phương thức đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRouteDetail.URL.Delete,
                    data: { id: $scope.ParamID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        $state.go("main.ORDOrder.Route");
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.info_Update = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Save,
            data: { item: $scope.ItemOrder },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.info_Approved = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn duyệt dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRouteDetail.URL.Order_Approved,
                    data: { id: $scope.ParamID },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDOrderRouteDetail.URL.Get,
                            data: { id: $scope.ParamID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.ItemOrder = res;
                                $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
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
        })
    };

    $scope.info_UnApproved = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn hủy duyệt dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRouteDetail.URL.Order_UnApproved,
                    data: { id: $scope.ParamID },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDOrderRouteDetail.URL.Get,
                            data: { id: $scope.ParamID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.ItemOrder = res;
                                $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
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
        })
    };
    //#region order

    $scope.order_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Order_List,
            readparam: function () { return { ordRouteId: $scope.ParamID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="order_Delete($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'TransportModeName', width: 100, title: 'Loại v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: 100, title: 'Loại dịch vụ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 100, title: 'Mã đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'RequestDate', width: 120, title: 'Ngày gửi y/c', template: "#=RequestDate==null?' ': Common.Date.FromJsonDMYHM(RequestDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'LocationFromCode', width: 100, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 150, title: 'Tên điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 150, title: 'Đ/c điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 100, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 150, title: 'Tên điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 150, title: 'Đ/c điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.order_Delete = function ($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data.ID) || data.ID != '') {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrderRouteDetail.URL.Order_Delete,
                        data: { id: data.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.order_grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    };

    $scope.orderNotin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Order_NotInList,
            readparam: function () { return { ordRouteId: $scope.ParamID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,orderNotin_grid,orderNotin_gridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,orderNotin_grid,orderNotin_gridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'OrderCode', width: 100, title: 'Mã đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'RequestDate', width: 120, title: 'Ngày gửi y/c', template: "#=RequestDate==null?' ': Common.Date.FromJsonDMYHM(RequestDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'LocationFromCode', width: 100, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 150, title: 'Tên điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 150, title: 'Đ/c điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 100, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 150, title: 'Tên điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 150, title: 'Đ/c điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.orderNotin_gridChange = function ($event, grid, haschoose) {
        $scope.Order_NotinHasChoose = haschoose;
    };

    $scope.order_Search = function ($event, win, grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.orderNotIn_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        var data = [];
        angular.forEach(grid.dataSource.data(), function (o, i) {
            if (o.IsChoose == true)
                data.push(o.OrderID);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrderRouteDetail.URL.Order_SaveList,
                data: { lst: data, ordRouteId: $scope.ParamID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.order_grid.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };


    //#endregion

    //#region route detail(cac chang)
    $scope.route_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Route_List,
            readparam: function () { return { ordRouteId: $scope.ParamID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SortOrder: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: 145,
                template: '<a href="/" ng-click="route_Edit($event,dataItem,route_win)" class="k-button" ng-show="Auth.ActEdit"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="route_Delete($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsLTLtoFCL" ng-click="detailLTLToFCL_click($event,dataItem,LTLtoFCLDetail_win)" class="k-button" ><i class="fa fa-cog"></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', width: 100, title: 'Mã', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DetailName', width: 100, title: 'Tên', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SortOrder', width: 100, title: 'Thứ tự', filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'RouteDetailStatusName', width: 100, title: 'Trạng thái', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RouteDetailStatusModeName', width: 150, title: 'Loại', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RouteDetailStatusOPSIDName', width: 150, title: 'Tình trạng v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: 100, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 150, title: 'Tên điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 100, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 150, title: 'Tên điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', width: 100, title: 'Loại v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: 100, title: 'Loại dịch vụ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: 120, title: 'ETD', template: "#=ETD==null?' ': Common.Date.FromJsonDMYHM(ETD)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: 120, title: 'ETA', template: "#=ETA==null?' ': Common.Date.FromJsonDMYHM(ETA)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETDRequest', width: 120, title: 'ETD Ngày gửi y/c', template: "#=ETDRequest==null?' ': Common.Date.FromJsonDMYHM(ETDRequest)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETARequest', width: 120, title: 'ETA Ngày gửi y/c', template: "#=ETARequest==null?' ': Common.Date.FromJsonDMYHM(ETARequest)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'PartnerCode', width: 100, title: 'Mã đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', width: 100, title: 'Tên đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.routeDetail_Add = function ($event, win) {
        $event.preventDefault();
        $scope.LoadRouteDetail(0, win, "");
    };

    $scope.route_Delete = function ($event, data) {
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
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRouteDetail.URL.Route_Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.route_grid.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.route_Edit = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadRouteDetail(data.ID, win, "");
    };

    $scope.LoadRouteDetail = function (data, win, mess) {
        Common.Log("LoadRouteDetail");
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Route_Get,
            data: { id: data },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemRoute = res;
                $scope.LoadDataLocation($scope.ItemRoute)
                win.center();
                win.open();
                if (Common.HasValue(mess) && mess != "")
                    $rootScope.Message({ Msg: mess, Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.numSortOrder_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0, }

    $scope.cboTransportMode_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (data) {
            $scope.cboTransportMode_Options.dataSource.data(data);
        }
    })

    $scope.cboServiceOfOrder_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (data) {
            data.splice(0, 0, { ID: -1, Code: " ", ValueOfVar: " " })
            $scope.cboServiceOfOrder_Options.dataSource.data(data);
        }
    })

    $scope.cboRouteDetailStatusMode_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarRouteDetailStatusMode,
        success: function (res) {
            $scope.cboRouteDetailStatusMode_Options.dataSource.data(res);
        }
    })

    $scope.detailLTLToFCL_click = function ($event, data, win) {
        $event.preventDefault();
        $scope.LTLtoFCLDetail_tabstrip.select(0);
        $scope.TabIndexDetailLTLtoFCL = 1;
        $scope.RouteDetailID = data.ID;
        win.center();
        win.open();
        $scope.LTLtoFCLDetail_Cont_grid.dataSource.read();
        $scope.LTLtoFCLDetail_product_grid.dataSource.read();
    };

    $scope.cboPartnerFrom_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'PartnerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PartnerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.ItemRoute.LocationFromID = "";
            $scope.LoadDataLocation($scope.ItemRoute)
        }
    }

    $scope.cboPartnerTo_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'PartnerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PartnerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.ItemRoute.LocationToID = "";
            $scope.LoadDataLocation($scope.ItemRoute)
        }
    }

    $scope.cboPartnerDepot_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'PartnerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PartnerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.ItemRoute.LocationDepotID = "";
            $scope.ItemRoute.VesselID = "";
            $scope.LoadDataLocation($scope.ItemRoute)
        }
    }

    $scope.cboLocationFrom_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
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
    }

    $scope.cboLocationTo_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
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
    }

    $scope.cboLocationDepot_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
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
    }

    $scope.cboVessel_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'VesselName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    VesselName: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrderRouteDetail.URL.Location_data,
        data: {},
        success: function (res) {
            _ORDOrderRouteDetail.Data.Partner = res.ListPartner;
            _ORDOrderRouteDetail.Data.Location = {};

            Common.Data.Each(res.ListLocation, function (o) {
                if (!Common.HasValue(_ORDOrderRouteDetail.Data.Location[o.CusPartID])) {
                    _ORDOrderRouteDetail.Data.Location[o.CusPartID] = [{ ID: -1, Code: " ", LocationName: " " }, o];
                }
                else {
                    _ORDOrderRouteDetail.Data.Location[o.CusPartID].push(o);
                }
            })

            var objPartVessel = {};

            Common.Data.Each(res.ListVessel, function (o) {
                if (Common.HasValue(objPartVessel[o.PartnerID])) {
                    objPartVessel[o.PartnerID].push(o);
                }
                else {
                    objPartVessel[o.PartnerID] = [o];
                }
            })

            _ORDOrderRouteDetail.Data.Vessel = {};
            angular.forEach(res.ListPartner, function (o, index) {
                if (Common.HasValue(_ORDOrderRouteDetail.Data.Vessel[o.ID])) {
                    if (Common.HasValue(objPartVessel[o.CATPartnerID])) {
                        _ORDOrderRouteDetail.Data.Vessel[o.ID].push(objPartVessel[o.CATPartnerID]);
                    }
                }
                else {
                    _ORDOrderRouteDetail.Data.Vessel[o.ID] = [{ ID: -1, Code: ' ', VesselName: ' ' }]
                    if (Common.HasValue(objPartVessel[o.CATPartnerID])) {
                        _ORDOrderRouteDetail.Data.Vessel[o.ID] = _ORDOrderRouteDetail.Data.Vessel[o.ID].concat(objPartVessel[o.CATPartnerID]);
                    }
                }
            })

            $scope.cboPartnerFrom_Options.dataSource.data(_ORDOrderRouteDetail.Data.Partner);
            $scope.cboPartnerTo_Options.dataSource.data(_ORDOrderRouteDetail.Data.Partner);
            $scope.cboPartnerDepot_Options.dataSource.data(_ORDOrderRouteDetail.Data.Partner);

        }
    });

    $scope.LoadDataLocation = function (item) {
        Common.Log("LoadDataLocation")

        var partnerFrom = item.PartnerFromID;
        var partnerTo = item.PartnerToID;
        var partnerDepot = item.PartnerDepotID;

        var locationFrom = item.LocationFromID;
        var locationTo = item.LocationToID;
        var locationDepot = item.LocationDepotID;
        var vesselid = item.VesselID;

        var datafrom = _ORDOrderRouteDetail.Data.Location[partnerFrom];
        $scope.cboLocationFrom_Options.dataSource.data(datafrom);
        if (Common.HasValue(datafrom) && datafrom.length > 0 && !(locationFrom > 0)) {
            locationFrom = datafrom[0].ID;
        }
        $timeout(function () {
            item.LocationFromID = locationFrom;
        }, 1)

        var datato = _ORDOrderRouteDetail.Data.Location[partnerTo];
        $scope.cboLocationTo_Options.dataSource.data(datato);
        if (Common.HasValue(datato) && datato.length > 0 && !(locationTo > 0)) {
            locationTo = datato[0].ID;
        }
        $timeout(function () {
            item.LocationToID = locationTo;
        }, 1)

        var datadepot = _ORDOrderRouteDetail.Data.Location[partnerDepot];
        $scope.cboLocationDepot_Options.dataSource.data(datadepot);
        if (Common.HasValue(datadepot) && datadepot.length > 0 && !(locationDepot > 0)) {
            locationDepot = datadepot[0].ID;
        }
        $timeout(function () {
            item.LocationDepotID = locationDepot;
        }, 1)

        var dataVessel = _ORDOrderRouteDetail.Data.Vessel[partnerDepot];
        $scope.cboVessel_Options.dataSource.data(dataVessel);
        if (Common.HasValue(dataVessel) && dataVessel.length > 0 && !(vesselid > 0)) {
            vesselid = dataVessel[0].ID;
        }
        $timeout(function () {
            item.VesselID = vesselid;
        }, 1)
    }

    $scope.routeDetail_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrderRouteDetail.URL.Route_Save,
                data: { ordRouteId: $scope.ParamID, item: $scope.ItemRoute },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.route_grid.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };


    $scope.createOrderChilds_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.CreateOrderChilds,
            data: { id: $scope.ParamID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });

            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    };

    $scope.clearOrderChilds_Click = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa đơn hàng các chặng?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRouteDetail.URL.ClearOrderChilds,
                    data: { id: $scope.ParamID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.nextStatus_Click = function ($event, win) {
        $event.preventDefault();
        //run
        if ($scope.ItemRoute.TypeNextStatus == 1) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn chuyển trạng thái chặng sang "vận chuyển"?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrderRouteDetail.URL.Route_Run,
                        data: { id: $scope.ItemRoute.ID },
                        success: function (res) {
                            $scope.LoadRouteDetail($scope.ItemRoute.ID, win, "Đã cập nhật")
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
        else if ($scope.ItemRoute.TypeNextStatus == 2)//complete
        {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn chuyển trạng thái chặng sang "Hoàn thành"?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrderRouteDetail.URL.Route_Complete,
                        data: { id: $scope.ItemRoute.ID },
                        success: function (res) {
                            $scope.LoadRouteDetail($scope.ItemRoute.ID, win, "Đã cập nhật")
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    };

    $scope.vessel_addNew = function ($event, win) {
        $event.preventDefault();
        $scope.ItemVessel = { ID: -1, Code: "", VesselName: "" };
        win.center();
        win.open();
    };

    $scope.vessel_saveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrderRouteDetail.URL.Route_AddVessel,
                data: { item: $scope.ItemVessel, cuspartnerid: $scope.ItemRoute.PartnerDepotID },
                success: function (res) {
                    _ORDOrderRouteDetail.Data.Vessel[$scope.ItemRoute.PartnerDepotID].push(res);
                    $scope.LoadDataLocation();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }

    };
    //#endregion

    //#region ltl to fcl detail

    $scope.LTLtoFCLDetail_tabstripOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndexDetailLTLtoFCL = angular.element(e.item).data('tabindex'); //or
                Common.Log($scope.TabIndexDetailLTLtoFCL)
            }, 1);
        }
    };

    $scope.LTLtoFCLDetail_Cont_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Route_ContainerList,
            readparam: function () { return { routeDetailID: $scope.RouteDetailID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, autoBind: false,
        columns: [
            {
                title: ' ', width: 90,
                template: '<a href="/" ng-click="LTLtoFCLDetail_Cont_Edit($event,dataItem,containerDetail_win)" class="k-button" ng-show="Auth.ActEdit"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="LTLtoFCLDetail_Cont_Delete($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'PackingName', width: 100, title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: 100, title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: 100, title: 'Số Seal1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: 100, title: 'Số Seal2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 100, title: 'Trong tải', template: '#=Common.Number.ToNumber6(Ton)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'Note', width: 100, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 100, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 100, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.cboPacking_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'PackingName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATPackingCO,
        success: function (res) {
            $scope.cboPacking_Options.dataSource.data(res);
        }
    });

    $scope.numContTon_Options = { format: 'n6', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 6, }

    $scope.LTLtoFCLDetail_Cont_addNew = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItemCont(0, win);
    };

    $scope.LTLtoFCLDetail_Cont_Edit = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadItemCont(data.ID, win);
    };

    $scope.LoadItemCont = function (id, win) {

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Route_ContainerGet,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemCont = res;
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.containerSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrderRouteDetail.URL.Route_ContainerSave,
                data: { item: $scope.ItemCont, routeDetailID: $scope.RouteDetailID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.LTLtoFCLDetail_Cont_grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.LTLtoFCLDetail_Cont_Delete = function ($event, data) {
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
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRouteDetail.URL.Route_ContainerDelete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.LTLtoFCLDetail_Cont_grid.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };


    $scope.LTLtoFCLDetail_product_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Route_ProductList,
            readparam: function () { return { routeDetailID: $scope.RouteDetailID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, autoBind: false,
        columns: [
            {
                title: ' ', width: 90,
                template: '<a href="/" ng-click="LTLtoFCLDetail_Product_Edit($event,dataItem,container_win)" class="k-button" ng-show="Auth.ActEdit"><i class="fa fa-truck"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'GroupProductCode', width: 100, title: 'Mã nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: 100, title: 'Mã hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: 100, title: 'Số Seal2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 100, title: 'Tấn', template: '#=Common.Number.ToNumber6(Ton)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             {
                 field: 'CBM', width: 100, title: 'Khối', template: '#=Common.Number.ToNumber6(CBM)#',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
              {
                  field: 'Quantity', width: 100, title: 'Số lượng', template: '#=Common.Number.ToNumber6(Quantity)#',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
            { field: 'Note1', width: 100, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 100, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.LTLtoFCLDetail_product_addNew = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.productNotin_grid.dataSource.read()
    };

    $scope.productNotin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Route_ProductNotinList,
            readparam: function () { return { ordRouteId: $scope.ParamID, routeDetailID: $scope.RouteDetailID } },
            model: {
                id: 'ORDProductID',
                fields: {
                    ORDProductID: { type: 'number' },
                    IsChoose: { type: 'boolean' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
             {
                 title: ' ', width: '85px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,productNotin_grid,productNotin_gridChooseChange)" />',
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,productNotin_grid,productNotin_gridChooseChange)" />',
                 filterable: false, sortable: false
             },
            { field: 'GroupProductCode', width: 100, title: 'Mã nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: 100, title: 'Mã hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 100, title: 'Tấn', template: '#=Common.Number.ToNumber6(Ton)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             {
                 field: 'CBM', width: 100, title: 'Khối', template: '#=Common.Number.ToNumber6(CBM)#',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
              {
                  field: 'Quantity', width: 100, title: 'Số lượng', template: '#=Common.Number.ToNumber6(Quantity)#',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
            { field: 'Note1', width: 100, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 100, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.productNotin_gridChooseChange = function ($event, grid, haschoose) {
        $scope.ProductHasChoose = haschoose;
    };

    $scope.productNotinSave_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true)
                data.push(o.ORDProductID)
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrderRouteDetail.URL.Route_ProductSaveList,
                data: { lst: data, routeDetailID: $scope.RouteDetailID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.LTLtoFCLDetail_product_grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });

                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.LTLtoFCLDetail_Product_Edit = function ($event, data, win) {
        $event.preventDefault();
        $scope.RouteGOPID = data.ID;
        win.center();
        win.open();
        $scope.container_grid.dataSource.read();
    };

    $scope.container_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderRouteDetail.URL.Route_ProductContList,
            readparam: function () { return { routeDetailID: $scope.RouteDetailID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, autoBind: false,
        columns: [
            {
                title: ' ', width: 45,
                template: '<a href="/" ng-click="productAddContainer_Click($event,dataItem,container_win)" class="k-button" ng-show="Auth.ActEdit"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'PackingName', width: 100, title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: 100, title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: 100, title: 'Số Seal1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: 100, title: 'Số Seal2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 100, title: 'Trong tải', template: '#=Common.Number.ToNumber6(Ton)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'Note', width: 100, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 100, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 100, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.productAddContainer_Click = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn chọn container này cho hàng hóa đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderRouteDetail.URL.Route_ProductUpdateCont,
                    data: { routeGOPID: $scope.RouteGOPID, routeContID: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        win.close();
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    //#endregion

}]);