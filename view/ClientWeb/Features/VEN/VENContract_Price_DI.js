﻿/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _VENContract_Price_DIV2 = {
    URL: {
        Get: 'VENContract_Price_Get',
        Delete_PriceNormal: 'VENContract_Price_DeletePriceNormal',
        Delete_PriceLevel: 'VENContract_Price_DeletePriceLevel',

        //#region price ex
        PriceEx_List: 'VENPrice_DI_Ex_List',
        PriceEx_Get: 'VENPrice_DI_Ex_Get',
        PriceEx_Save: 'VENPrice_DI_Ex_Save',
        PriceEx_Delete: 'VENPrice_DI_Ex_Delete',


        //#endregion

        //#region price moq
        PriceMOQ_DeleteList: 'VENPrice_DI_PriceMOQ_DeleteList',
        PriceLoad_Location_DeleteList: 'VENPrice_DI_LoadLocation_DeleteList',
        PriceLoad_Route_DeleteList: 'VENPrice_DI_LoadRoute_DeleteList',
        PriceLoad_Partner_DeleteList: 'VENPrice_DI_LoadPartner_DeleteList',
        PriceLoad_MOQ_DeleteList: 'VENPrice_DI_PriceMOQLoad_DeleteList',

        PriceUnLoad_Location_DeleteList: 'VENPrice_DI_UnLoadLocation_DeleteList',
        PriceUnLoad_Route_DeleteList: 'VENPrice_DI_UnLoadRoute_DeleteList',
        PriceUnLoad_Partner_DeleteList: 'VENPrice_DI_UnLoadPartner_DeleteList',
        PriceUnLoad_MOQ_DeleteList: 'VENPrice_DI_PriceMOQUnLoad_DeleteList',
        //#endregion

        Price_DI_Load_DeleteAll: 'VENPrice_DI_Load_DeleteAllList',
        Price_DI_UnLoad_DeleteAll: 'VENPrice_DI_UnLoad_DeleteAllList',
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
        ListTypeOfPriceDIEx: [],
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
        TermID: -1,
    }
}

angular.module('myapp').controller('VENContract_PriceDICtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('VENContract_PriceDICtrl');
    $rootScope.IsLoading = false;
    _VENContract_Price_DIV2.Params = $.extend({}, true, $state.params);
    $scope.ItemPrice = null;
    $scope.ItemCheck = {
        Primary: 1,
        Load: true,
        UnLoad: true,
        PEx: false,
        HasLoad: false,
        HasUnload: false,
        HasPEx: true,
        HasPriceEx: false,
    }
    $scope.splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, size: "110px", resizable: false },
            { collapsible: false, size: "60px", resizable: false },
            { collapsible: false, resizable: false }
        ]
    }

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENContract_Price_DIV2.URL.Get,
        data: { priceID: _VENContract_Price_DIV2.Params.PriceID },
        success: function (res) {
            $scope.ItemPrice = res;
            if (res.CheckPrice.HasNormal)
                $scope.ItemCheck.Primary = 1;
            else if (res.CheckPrice.HasLevel)
                $scope.ItemCheck.Primary = 2;
            else if (!res.CheckPrice.HasNormal && !res.CheckPrice.HasLevel)
                $scope.ItemCheck.Primary = 1;

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
            method: _VENContract_Price_DIV2.URL.Get,
            data: { priceID: _VENContract_Price_DIV2.Params.PriceID },
            success: function (res) {
                $scope.ItemPrice = res;
                if (res.CheckPrice.HasNormal)
                    $scope.ItemCheck.Primary = 1;
                else if (res.CheckPrice.HasLevel)
                    $scope.ItemCheck.Primary = 2;
                else if (!res.CheckPrice.HasNormal && !res.CheckPrice.HasLevel)
                    $scope.ItemCheck.Primary = 1;

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
            method: _VENContract_Price_DIV2.URL.PriceEx_List,
            readparam: function () { return { priceID: _VENContract_Price_DIV2.Params.PriceID } },
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
            { field: 'TypeOfPriceDIExName', width: 250, title: 'Loại phụ thu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DIExSumName', width: 150, title: 'Loại tính tổng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', title: 'Tên phụ thu', filterable: { cell: { operator: 'contains', showOperators: false } } },
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
        $state.go("main.VENContract.PriceDIEx", {
            PriceID: _VENContract_Price_DIV2.Params.PriceID,
            PriceIDEx: data.ID,
            TermID: $scope.ItemPrice.ContractTermID,
            CustomerID: $scope.ItemPrice.CustomerID,
        })
    }
    $scope.PriceDI_Ex_AddClick = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIEx", {
            PriceID: _VENContract_Price_DIV2.Params.PriceID,
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
                    method: _VENContract_Price_DIV2.URL.PriceEx_Delete,
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
    $scope.PriceDI_AddNormal_Click = function ($event) {
        $event.preventDefault();
        if ((!$scope.ItemPrice.HasNormal && !$scope.ItemPrice.HasLevel) || $scope.ItemPrice.HasNormal) {
            $state.go("main.VENContract.PriceDINormal", _VENContract_Price_DIV2.Params)
        }
    }

    $scope.PriceDI_AddLevel_Click = function ($event) {
        $event.preventDefault();
        if ((!$scope.ItemPrice.HasNormal && !$scope.ItemPrice.HasLevel) || $scope.ItemPrice.HasLevel) {
            $state.go("main.VENContract.PriceDILevel", _VENContract_Price_DIV2.Params)
        }
    }

    $scope.PriceDI_AddMOQ_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIMOQ", { PriceID: _VENContract_Price_DIV2.Params.PriceID, TermID: _VENContract_Price_DIV2.Params.TermID, CustomerID: $scope.ItemPrice.CustomerID })
    }

    $scope.PriceDI_DeleteNormal_Click = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.Delete_PriceNormal,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }
    $scope.PriceDI_DeleteLevel_Click = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.Delete_PriceLevel,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }
    $scope.PriceDI_DeleteMOQ_Click = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa MOQ bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceMOQ_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }
    //#endregion

    //#region boc xep len
    $scope.PriceDI_LoadLocation_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDILoadLocation", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_LoadLocation_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceLoad_Location_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_Load_DeleteAllClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa tất cả bốc xếp lên?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.Price_DI_Load_DeleteAll,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //                $rootScope.IsLoading = false;
        //                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_LoadRoute_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDILoadRoute", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_LoadRoute_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceLoad_Route_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_LoadPartner_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDILoadPartner", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_LoadTypeOfPartner_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDILoadTypeOfPartner", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_LoadPartner_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceLoad_Partner_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_LoadMOQ_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDILoadMOQ", { PriceID: _VENContract_Price_DIV2.Params.PriceID, TermID: _VENContract_Price_DIV2.Params.TermID, CustomerID: $scope.ItemPrice.CustomerID })
    }
    $scope.PriceDI_LoadMOQ_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceLoad_MOQ_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }
    //#endregion

    //#region boc xep xuống
    $scope.PriceDI_UnLoadLocation_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIUnLoadLocation", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_UnLoadLocation_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceUnLoad_Partner_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_UnLoadRoute_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIUnLoadRoute", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_UnLoadRoute_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceUnLoad_Partner_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_UnLoadPartner_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIUnLoadPartner", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_UnLoadTypeOfPartner_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIUnLoadTypeOfPartner", _VENContract_Price_DIV2.Params)
    }
    $scope.PriceDI_UnLoadPartner_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceUnLoad_Partner_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_UnLoadMOQ_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIUnLoadMOQ", { PriceID: _VENContract_Price_DIV2.Params.PriceID, TermID: _VENContract_Price_DIV2.Params.TermID, CustomerID: $scope.ItemPrice.CustomerID })
    }
    $scope.PriceDI_UnLoadMOQ_DeleteClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa bảng giá đã chọn?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.PriceUnLoad_MOQ_DeleteList,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }

    $scope.PriceDI_UnLoad_DeleteAllClick = function ($event) {
        $event.preventDefault();
        //$rootScope.Message({
        //    Type: Common.Message.Type.Confirm,
        //    NotifyType: Common.Message.NotifyType.SUCCESS,
        //    Title: 'Thông báo',
        //    Msg: 'Bạn muốn xóa tất cả bốc xếp xuống?',
        //    Close: null,
        //    Ok: function () {
        //        $rootScope.IsLoading = true;
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.VEN,
        //            method: _VENContract_Price_DIV2.URL.Price_DI_UnLoad_DeleteAll,
        //            data: { priceID: _VENContract_Price_DIV2.Params.ID },
        //            success: function (res) {
        //                $scope.LoadItemPrice();
        //                $rootScope.IsLoading = false;
        //                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
        //            },
        //            error: function (res) {
        //                $rootScope.IsLoading = false;
        //            }
        //        });

        //    }
        //})
    }
    //#endregion

    $scope.PriceDI_Ex_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.PriceDIEx", _VENContract_Price_DIV2.Params)
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }



    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.Term", { TermID: _VENContract_Price_DIV2.Params.TermID, ContractID: $scope.ItemPrice.ContractID })
    }
}]);