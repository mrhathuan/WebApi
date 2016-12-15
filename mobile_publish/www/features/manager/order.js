/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('manage_orderController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, charting, openMap) {
    console.log('manage_orderController');
    
    var skip = 0;
    var pageSize = 5;
    $scope.OrderData = [];
    $scope.OrderSumary = [];
    $scope.Filter =
    {
        DateFrom: new Date(),
        DateTo: new Date().addDays(1),
    };
    $scope.ShowDetail = false;
    $scope.ShowSearch = false;
    $scope.IsSum = true;
    $scope.IsEnd = true;
    $scope.OrderItem = {};
    $scope.ViewTittle = 'Đơn hàng';
    $scope.StatusID = 0;

    $scope.LoadSummary = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "MobiManage_OrderSummary",
            data: {
                dtfrom: $scope.Filter.DateFrom,
                dtto: $scope.Filter.DateTo,
            },
            success: function (res) {
                $scope.OrderSumary = res;
                $ionicLoading.hide();
            }
        })
    }
    $scope.LoadSummary();

    $scope.OrderSummaryClick = function (orderStatus) {
        $scope.IsSum = false;
        $scope.StatusID = orderStatus;
        $scope.LoadData(orderStatus);
        $scope.OrderData = [];
    }

    $scope.LoadData = function (orderStatus) {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "MobiManage_Order",
            data: {
                dtfrom: $scope.Filter.DateFrom,
                dtto: $scope.Filter.DateTo,
                skip: skip,
                pageSize: pageSize,
                orderStatus: $scope.StatusID
            },
            success: function (res) {
                if (res.length == 0) {
                    $scope.IsEnd = true;
                }
                $ionicLoading.hide();
                skip += 5;
                $scope.OrderData = res;
            }
        })
    }

    $scope.BackTo = function () {
        if (!$scope.ShowDetail && $scope.IsSum)
            $state.go('manage.dashboard');
        else if (!$scope.ShowDetail && !$scope.IsSum) {
            $scope.IsSum = true;
            $scope.ViewTittle = 'Đơn hàng';
        }
        else {
            $scope.ShowDetail = false;
            $scope.ViewTittle = 'Đơn hàng';
        }
    }

    $scope.OrderClick = function (e, item) {
        e.preventDefault();

        $scope.ShowDetail = true;
        $scope.ViewTittle = item.OrderCode;
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "MobiManage_OrderDetail",
            data: {
                orderID: item.OrderID,
            },
            success: function (res) {
                $ionicLoading.hide();
                $scope.OrderItem=res;
            }
        })
    }

    $scope.ColorClass = function (code) {
        var str = '';
        switch (code) {
            case 'new':
                str = 'color-grey';
                break;
            case 'trans':
                str = 'color-orange';
                break;
            case 'receive':
                str = 'color-green';
                break;
            case 'invoice':
                str = 'color-orchid';
                break;
            default:
                str = '';
                break;
        }
        return str;
    }

    //comobobox
    $scope.PickDate = function () {
        var data = [
            { text: '1 ngày', value: 1 },
            { text: '2 ngày', value: 2 },
            { text: '1 tuần', value: 3 }
        ]
        $scope.CreateDataCBB(data, 'text', $scope.CbbModel)
    }

    $scope.CreateDataCBB = function (data, textField, model) {
        $scope.ShowSelectPane = true;
        $scope.DataCombobox = [];
        $scope.ComoboboxModel = model;

        angular.forEach(data, function (o, i) {
            $scope.DataCombobox.push({ text: o[textField],obj:o });
        })
    }

    $scope.ComboboxSelect = function (item) {
        switch (item.value) {
            case 1:
                $scope.Filter.DateFrom = new Date();
                $scope.Filter.DateTo = new Date().addDays(1);
                break;
            case 2:
                $scope.Filter.DateFrom = new Date().addDays(-1);;
                $scope.Filter.DateTo = new Date().addDays(1);
                break;
            case 3:
                $scope.Filter.DateFrom = new Date().addDays(-16);;
                $scope.Filter.DateTo = new Date().addDays(1);
                break;
        }
        $scope.ShowSelectPane = false;
        $scope.LoadSummary();
    }

    $scope.CloseSelectPane = function () {
        $scope.ShowSelectPane = false;
    }

    //
});