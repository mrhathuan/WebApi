/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _VENContract_Price_CO = {
    URL: {
        Get: 'VENContract_Price_Get',
        //Delete_PriceNormal: 'VENContract_Price_DeletePriceNormal',
        //Delete_PriceLevel: 'VENContract_Price_DeletePriceLevel',

        //#region price ex
        PriceEx_List: 'VENPrice_CO_Ex_List',
        PriceEx_Get: 'VENPrice_CO_Ex_Get',
        PriceEx_Save: 'VENPrice_CO_Ex_Save',
        PriceEx_Delete: 'VENPrice_CO_Ex_Delete',
    },
    Data: {
        ListPackingTU: null,
        ListPriceOfGOP: null,
        ListGroupVehicle: null,
        ListGroupProduct: null,
        ListGroupPartner: null,
        ListTypeOfPriceEX: null,
        ListRoutingParent: [],
        ListLocation: [],
        LisAllRoute: [],
        ListTypeOfPriceCOEx: [],
        objModel: {},
        ListLevel: [],
        ListFieldLevel: [],
        ItemPriceBackup: null,
        ItemGOVBackup: null,
        objModelGov: {},
        ListGovLevel: [],
        //8/5
        ListGroupLocation: null,
    },
    Params: {
        //ID: -1,
        //CustomerID: -1,
        //ContractID: -1
        PriceID: -1,
        TermID:-1,
    }
}

angular.module('myapp').controller('VENContract_PriceCOCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('VENContract_PriceCOCtrl');
    $rootScope.IsLoading = false;
    _VENContract_Price_CO.Params = $.extend({}, true, $state.params);
    $scope.ItemPrice = null;

    $scope.ItemCheck = {
        Load: false,
        UnLoad: false,
        PEx: false,
        HasLoad: false,
        HasUnload: false,
        HasPEx: true,
        HasPriceEx: true,
    }


    $scope.splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, size: "70px", resizable: false },
            { collapsible: false, size: "60px", resizable: false },
            { collapsible: false, resizable: false }
        ]
    }
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

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENContract_Price_CO.URL.Get,
        data: { priceID: _VENContract_Price_CO.Params.PriceID },
        success: function (res) {
            $scope.ItemPrice = res;

            if (res.CheckPrice.HasLoadLocation || res.CheckPrice.HasLoadRoute || res.CheckPrice.HasLoadGOL)
                $scope.ItemCheck.HasLoad = true;
            else $scope.ItemCheck.HasLoad = false;
            if (res.CheckPrice.HasUnLoadLocation || res.CheckPrice.HasUnLoadRoute || res.CheckPrice.HasUnLoadGOL)
                $scope.ItemCheck.HasUnLoad = true;
            else $scope.ItemCheck.HasUnLoad = false;

            $scope.ItemCheck.HasPriceEx = res.CheckPrice.HasPriceEx;
            $rootScope.IsLoading = false;
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    })

    $scope.LoadItemPrice = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_CO.URL.Get,
            data: { priceID: _VENContract_Price_CO.Params.PriceID },
            success: function (res) {
                $scope.ItemPrice = res;

                if (res.CheckPrice.HasLoadLocation || res.CheckPrice.HasLoadRoute || res.CheckPrice.HasLoadGOL)
                    $scope.ItemCheck.HasLoad = true;
                else $scope.ItemCheck.HasLoad = false;
                if (res.CheckPrice.HasUnLoadLocation || res.CheckPrice.HasUnLoadRoute || res.CheckPrice.HasUnLoadGOL)
                    $scope.ItemCheck.HasUnLoad = true;
                else $scope.ItemCheck.HasUnLoad = false;


                $scope.ItemCheck.HasPriceEx = res.CheckPrice.HasPriceEx;
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.ChangeStatus = function ($event, status) {
        $event.preventDefault();
        switch (status) {
            default:
                break;
            case 'Load': $scope.ItemCheck.Load = !$scope.ItemCheck.Load; break;
            case 'UnLoad': $scope.ItemCheck.UnLoad = !$scope.ItemCheck.UnLoad; break;
            case 'PEx': $scope.ItemCheck.HasPEx = !$scope.ItemCheck.HasPEx; break;
        }
    }

    $scope.price_ex_grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Price_CO.URL.PriceEx_List,
            readparam: function () { return { priceID: _VENContract_Price_CO.Params.PriceID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '300px', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="priceEx_GridEdit_Click($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="priceEx_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'TypeOfPriceCOExName', width: 100, title: 'Loại phụ thu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'COExSumName', width: 100, title: 'Loại tính tổng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', width: 100, title: 'Tên phụ thu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            var grid = this;
            $timeout(function () {
                if (grid.dataSource.data().length < 1)
                    $scope.ItemCheck.HasPriceEx = false;
                else $scope.ItemCheck.HasPriceEx = true;
            }, 10)
        }
    }

    $scope.priceEx_GridEdit_Click = function ($event, data) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOEx", {
            PriceID: _VENContract_Price_CO.Params.PriceID,
            PriceIDEx: data.ID,
            TermID: $scope.ItemPrice.ContractTermID,
            CustomerID: $scope.ItemPrice.CustomerID,
        })
    }
    $scope.PriceCO_Ex_AddClick = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOEx", {
            PriceID: _VENContract_Price_CO.Params.PriceID,
            PriceIDEx: 0,
            TermID: $scope.ItemPrice.ContractTermID,
            CustomerID: $scope.ItemPrice.CustomerID,
        })
    }
    $scope.priceEx_GridDestroy_Click = function ($event, data) {
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
                    method: _VENContract_Price_CO.URL.PriceEx_Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.price_ex_grid_Options.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    //#region gia chính
    $scope.PriceCO_AddNormal_Click = function ($event) {
        $event.preventDefault();
        if ((!$scope.ItemPrice.HasNormal && !$scope.ItemPrice.HasLevel) || $scope.ItemPrice.HasNormal) {
            $state.go("main.VENContract.PriceCONormal", _VENContract_Price_CO.Params)
        }
    }

    $scope.PriceCO_AddMOQ_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOMOQ", _VENContract_Price_CO.Params)
    }

    $scope.PriceCO_DeleteNormal_Click = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.Delete_PriceNormal,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }
    $scope.PriceCO_DeleteMOQ_Click = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa MOQ bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceMOQ_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }
    //#endregion

    //#region boc xep len
    $scope.PriceCO_LoadLocation_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOLoadLocation", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_LoadLocation_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceLoad_Location_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_Load_DeleteAllClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa tất cả bốc xếp lên?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.Price_CO_Load_DeleteAll,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_LoadRoute_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOLoadRoute", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_LoadRoute_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceLoad_Route_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_LoadPartner_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOLoadPartner", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_LoadPartner_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceLoad_Partner_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_LoadMOQ_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOLoadMOQ", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_LoadMOQ_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceLoad_MOQ_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }
    //#endregion

    //#region boc xep xuống
    $scope.PriceCO_UnLoadLocation_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOUnLoadLocation", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_UnLoadLocation_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceUnLoad_Partner_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_UnLoadRoute_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOUnLoadRoute", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_UnLoadRoute_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceUnLoad_Partner_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_UnLoadPartner_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOUnLoadPartner", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_UnLoadPartner_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceUnLoad_Partner_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_UnLoadMOQ_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOUnLoadMOQ", _VENContract_Price_CO.Params)
    }
    $scope.PriceCO_UnLoadMOQ_DeleteClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.PriceUnLoad_MOQ_DeleteList,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }

    $scope.PriceCO_UnLoad_DeleteAllClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa tất cả bốc xếp xuống?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Price_CO.URL.Price_CO_UnLoad_DeleteAll,
                    data: { priceID: _VENContract_Price_CO.Params.ID },
                    success: function (res) {
                        $scope.LoadItemPrice();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }
    //#endregion

    $scope.PriceCO_Ex_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOEx", _VENContract_Price_CO.Params)
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }



    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.Term", { TermID: _VENContract_Price_CO.Params.TermID, ContractID: $scope.ItemPrice.ContractID })
    }

    $scope.AddService_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOService", _VENContract_Price_CO.Params )
    }

    $scope.AddServicePacking_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceCOServicePacking", _VENContract_Price_CO.Params)
    }
}]);