/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('vendor_infoCtrl', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading) {
    console.log('vendor_infoCtrl');

    $scope.DriverID = Common.Auth.Item.DriverID;
    $scope.Init = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileVendor_Get",
            data: {},
            success: function (res) {
                $scope.VendorItem = res;
            }
        });
    }
    $scope.Init();

    $scope.LogOut = function () {
        $rootScope.PopupConfirm({
            title: 'Bạn có muốn quay trở lại giao diện đăng nhập?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $state.go('login');
            }
        });
    }

    $scope.ChangeView = function () {
        $rootScope.PopupConfirm({
            title: 'Bạn có muốn chuyển sang giao diện tài xế?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $state.go('main.home');
            }
        });
    }
});