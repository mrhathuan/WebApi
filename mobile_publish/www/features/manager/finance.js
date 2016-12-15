/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('manage_financeController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, charting, openMap) {
    console.log('manage_financeController');
    
    var skip = 0;
    var pageSize = 5;
    $scope.FinanceData = [];
    $scope.Sum = 0;
    $scope.LoadData = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "MobiManage_TruckProfit",
            data: {
                skip: skip,
                pageSize: pageSize
            },
            success: function (res) {
                $ionicLoading.hide();
                skip += 5;
                $scope.FinanceData = $scope.FinanceData.concat(res);
                $scope.Sum = 0;
                angular.forEach($scope.FinanceData, function (o, i) {
                    $scope.Sum += o.Credit;
                })
                $scope.$broadcast('scroll.infiniteScrollComplete');
            }
        })
    }
    $scope.LoadData();

    $scope.BackTo = function () {
        $state.go('manage.dashboard');
    }

    $scope.ColorClass = function (code) {
        var str = '';
        switch (code) {
            case 'plan':
                str = 'color-grey';
                break;
            case 'delivery':
                str = 'color-orange';
                break;
            case 'received':
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
});