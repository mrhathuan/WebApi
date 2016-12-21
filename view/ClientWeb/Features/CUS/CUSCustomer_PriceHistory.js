

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSCustomer_PriceHistory = {
    URL: {
        Read_Customer: 'CATDistributor_Customer_Read',
        Export: 'PriceHistory_Export',
        CheckPrice: 'PriceHistory_CheckPrice',
    },
    Data: {
        DataCustomer: [],
    },
}

angular.module('myapp').controller('CUSCustomer_PriceHistoryCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('CUSCustomer_PriceHistoryCtrl');
    $rootScope.IsLoading = false;
    $scope.Item = {TypePrice:0};
    $scope.typePriceDisable = true;
    $scope.ListCustomer = [];

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
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã khách hàng </span><span style="float:right;"> Tên khách hàng </span></strong>',
        change: function (e) {
            $scope.ListCustomer = this.value();
            if (Common.HasValue($scope.Item.TransportModeID) && $scope.Item.TransportModeID > 0) {
                //$rootScope.IsLoading = true;
                //Common.Services.Call($http, {
                //    url: Common.Services.url.CUS,
                //    method: _CUSCustomer_PriceHistory.URL.CheckPrice,
                //    data: { lstCusId: $scope.ListCustomer, transportModeID: $scope.Item.TransportModeID },
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
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CUSCustomer_PriceHistory.URL.Read_Customer,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {

                _CUSCustomer_PriceHistory.Data.DataCustomer = res.Data;
                $scope.mts_CustomerOption.dataSource.data(res.Data);
            }
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
            //    url: Common.Services.url.CUS,
            //    method: _CUSCustomer_PriceHistory.URL.CheckPrice,
            //    data: { lstCusId: $scope.ListCustomer, transportModeID: $scope.Item.TransportModeID },
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
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 0, ValueName: '' },
            { ID: 1, ValueName: 'Thường' },
            { ID: 2, ValueName: 'Bậc thang' },
        ],
        change: function (e) {
        }
    }

    $scope.PriceHistory_Export = function ($event, vform) {
        //$event.preventDefault();
        //var error = false;
        //if ($scope.typePriceDisable == false) {
        //    if ($scope.Item.TypePrice == 0) {
        //        error = true;
        //        $rootScope.Message({ Msg: 'Chưa chọn loại bảng giá', NotifyType: Common.Message.NotifyType.ERROR });
        //    }
        //}
        //if ($scope.ListCustomer.length == 0) {
        //    error = true;
        //    if ($scope.Item.TypePrice == 0) {
        //        $rootScope.Message({ Msg: 'Chưa chọn khách hàng', NotifyType: Common.Message.NotifyType.ERROR });
        //    }
        //}
        //if (vform() && !error) {
        //    $rootScope.IsLoading = true;
        //    Common.Services.Call($http, {
        //        url: Common.Services.url.CUS,
        //        method: _CUSCustomer_PriceHistory.URL.Export,
        //        data: { lstCusId: $scope.ListCustomer, transportModeID: $scope.Item.TransportModeID, typePrice: $scope.Item.TypePrice },
        //        success: function (res) {
        //            $rootScope.IsLoading = false;
        //            $rootScope.DownloadFile(res);
        //        }
        //    });
        //}
    }

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CUSCustomer,
            event: $event,
            current: $state.current
        });
    };
}]);