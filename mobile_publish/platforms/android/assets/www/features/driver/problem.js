angular.module('myapp').controller('driver_problemController', function ($rootScope, $scope, $state, $stateParams, $location, $http, $timeout, $ionicLoading, $ionicModal, dataService) {
    console.log('driver_truckProblemController');

    $scope.ProblemItem = {};
    $scope.selectedTab = 1;

    $scope.SwicthTab = function (i) {
        $scope.selectedTab = i;
    }

    dataService.FLMMobileDriver_ProblemTypeList().then(function (res) {
        $scope.ProblemTypeData = res;
    })

    dataService.ProblemList().then(function (res) {
        $scope.ProblemList = res;
    })


    $scope.ProblemSave = function () {
        navigator.geolocation.getCurrentPosition(function (position) {
            var item = {};
            var now = new Date();
            item.Lat = position.coords.latitude;
            item.Lng = position.coords.longitude;
            item.DateStart = now;
            item.DateEnd = new Date().addHours(2);
            item.TypeOfRouteProblemID = $scope.ProblemItem.TypeOfRouteProblemID;
            item.DriverID = $rootScope.DriverID;
            item.VehicleID = $rootScope.VehicleID;
            dataService.FLMMobileDriver_ProblemSave(item).then(function (res) {
                $rootScope.PopupAlert({ title: 'Thành công' });
                dataService.ProblemList().then(function () {
                    $scope.ProblemList = res;
                })
            })       
        });
    }

    $scope.LoadMap = function (item) {
        $state.go('map', { p0: '4', p1: item.ID })
    }
});