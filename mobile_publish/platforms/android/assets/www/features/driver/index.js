/// <reference path="~/m/js/common.js" />

angular.module('myapp').controller('driverController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading) {
    console.log('driverController');
    
    $state.go('driver.truck');
});