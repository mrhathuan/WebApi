/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('vendorController', function ($rootScope, $scope, $state, $location, $http, $ionicSideMenuDelegate) {
    console.log('vendorController');
    $ionicSideMenuDelegate.canDragContent(false)
    $rootScope.VendorID = 0;
    $state.go('vendor.list');
});