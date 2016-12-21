/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('manageController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading) {
    console.log('manageController');
    $timeout(function () {
        $('ion-nav-bar.bar-calm').find('.button.button-icon.button-clear.ion-navicon').css({ display: 'none' });
    }, 100);
    $state.go('manage.dashboard');
});