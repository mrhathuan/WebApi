

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _VENVendor_PriceHistory = {
    URL: {
        Read_Vendor: 'ALL_Vendor',
        Read_Customer: 'CATDistributor_Customer_Read',
        Export: 'PriceHistory_Export',
        CheckPrice: 'PriceHistory_CheckPrice',
        VenList: 'PriceHistory_GetListVendor',
    },
    Data: {
        DataCustomer: [],
    },
}

angular.module('myapp').controller('VENVendor_PriceHistoryCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('VENVendor_PriceHistoryCtrl');
    $rootScope.IsLoading = false;
    $scope.Item = { TypePrice: 0 };
    $scope.typePriceDisable = true;
    $scope.ListVendor = [];

    $scope.mts_CustomerOption = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn nhà xe...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã nhà xe </span><span style="float:right;"> Tên nhà xe </span></strong>',
        change: function (e) {
            ////$scope.ListVendor = this.value();
            //if (Common.HasValue($scope.Item.TransportModeID) && $scope.Item.TransportModeID > 0) {
            //    $rootScope.IsLoading = true;
            //    Common.Services.Call($http, {
            //        url: Common.Services.url.VEN,
            //        method: _VENVendor_PriceHistory.URL.CheckPrice,
            //        data: { lstVenId: $scope.ListVendor, transportModeID: $scope.Item.TransportModeID },
            //        success: function (res) {
            //            $rootScope.IsLoading = false;
            //            if (res == 0) {

            //            } else if (res == 2) {
            //                $scope.typePriceDisable = false;
            //            } else if (res == 1) {
            //                $scope.typePriceDisable = true;
            //                $scope.Item.TypePrice = 0;
            //            }
            //        }
            //    });
            //}
            //if ($scope.ListVendor.length == 0) {
            //    $scope.CustomerID = -1;
            //}
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _VENVendor_PriceHistory.URL.Read_Vendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {

                _VENVendor_PriceHistory.Data.DataCustomer = res.Data;
                $scope.mts_CustomerOption.dataSource.data(res.Data);
            }
        }
    });

    $scope.cboCustomerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Chọn khách hàng...",
        dataTextField: 'CustomerName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function () {

            if ($scope.CustomerID == -1) {
                $scope.ListVendor = [];
            } else if (Common.HasValue($scope.CustomerID) && $scope.CustomerID > 0) {
                $timeout(function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.VEN,
                        method: _VENVendor_PriceHistory.URL.VenList,
                        data: { cusId: $scope.CustomerID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.ListVendor = res;
                        }
                    });
                }, 1)


            }
            //this.refresh();
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _VENVendor_PriceHistory.URL.Read_Customer,
        data: {},
        success: function (res) {
            var item = { ID: -1, CustomerName: '' };
            res.Data.unshift(item);
            $scope.cboCustomerOptions.dataSource.data(res.Data);
        }
    });

    $scope.cboTransportOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            //$rootScope.IsLoading = true;
            //Common.Services.Call($http, {
            //    url: Common.Services.url.VEN,
            //    method: _VENVendor_PriceHistory.URL.CheckPrice,
            //    data: { lstVenId: $scope.ListVendor, transportModeID: $scope.Item.TransportModeID },
            //    success: function (res) {
            //        $rootScope.IsLoading = false;
            //        if (res == 0) {

            //        } else if (res == 2) {
            //            $scope.typePriceDisable = false;
            //        } else if (res == 1) {
            //            $scope.typePriceDisable = true;
            //            $scope.Item.TypePrice = 0;
            //        }
            //    }
            //});
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (res) {
            $timeout(function () {
                $scope.cboTransportOptions.dataSource.data(res);
            }, 1);
        }
    });

    $scope.cboTypePriceOptions = {
        //autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        //suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        //dataSource: [
        //    { ID: 0, ValueName: '' },
        //    { ID: 1, ValueName: 'Thường' },
        //    { ID: 2, ValueName: 'Bậc thang' },
        //],
        //change: function (e) {
        //}
    }

    $scope.PriceHistory_Export = function ($event, vform) {
        //$event.preventDefault();
        //var error = false;
        ////if ($scope.ListVendor.length == 0 || !Common.HasValue($scope.CustomerID) || $scope.CustomerID <= 0) {
        ////    $rootScope.Message({ Msg: 'Chưa chọn khách hàng hoặc nhà xe', NotifyType: Common.Message.NotifyType.ERROR });
        ////}

        //if ($scope.typePriceDisable == false) {
        //    if ($scope.Item.TypePrice == 0) {
        //        error = true;
        //        $rootScope.Message({ Msg: 'Chưa chọn loại bảng giá', NotifyType: Common.Message.NotifyType.ERROR });
        //    }
        //}
        //if ($scope.ListVendor.length == 0) {
        //    error = true;
        //    if ($scope.Item.TypePrice == 0) {
        //        $rootScope.Message({ Msg: 'Chưa chọn nhà xe', NotifyType: Common.Message.NotifyType.ERROR });
        //    }
        //}
        //if (vform() && !error) {
        //    $rootScope.IsLoading = true;
        //    Common.Services.Call($http, {
        //        url: Common.Services.url.VEN,
        //        method: _VENVendor_PriceHistory.URL.Export,
        //        data: { cusId: $scope.CustomerID ,lstVenId: $scope.ListVendor, transportModeID: $scope.Item.TransportModeID, typePrice: $scope.Item.TypePrice },
        //        success: function (res) {
        //            $rootScope.IsLoading = false;
        //            $rootScope.DownloadFile(res);
        //        }
        //    });
        //}
    }

    //$scope.ShowSetting = function ($event) {
    //    $event.preventDefault();

    //    $rootScope.ShowSetting({
    //        ListView: views.VENVendor,
    //        event: $event,
    //        current: $state.current
    //    });
    //};
}]);
//Array.prototype.unique = function () {
//    var a = this.concat();
//    for (var i = 0; i < a.length; ++i) {
//        for (var j = i + 1; j < a.length; ++j) {
//            if (a[i] === a[j])
//                a.splice(j, 1);
//        }
//    }
//    return a;
//};