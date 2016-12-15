/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('list_vendorController', function ($rootScope, $scope, $state, $location, $http) {
    console.log('list_vendorController');
    $scope.VendorList = [];
    Common.Services.Call($http, {
        url: Common.Services.url.MOBI,
        method: "FLMMobileVendor_ListVendor",
        data: {},
        success: function (res) {
            $scope.VendorList = res;
            if ($scope.VendorList.length == 1) {
                $rootScope.VendorID = $scope.VendorList[0].ID;
                $state.go('vendor.home');
            }
        }
    });

    $scope.ClickVendor = function (item) {
        $rootScope.VendorID = item.ID;
        $state.go('vendor.home');
    }
});