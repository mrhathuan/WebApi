/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
app.controller('sytemlogCtrl', function ($rootScope, $scope, $state, $location, $http, $ionicHistory) {
    console.log('sytemlogCtrl');

    $scope.logHistory = Common.LogHistory;


    $scope.myGoBack = function () {
       $state.go('main.truck')
    };
});