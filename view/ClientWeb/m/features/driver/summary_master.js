angular.module('myapp').controller('driver_summaryMasterController', function ($rootScope, $scope, $ionicLoading, $state, $http, dataService) {
    console.log('driver_summaryMasterController');

    var timeSheetDriverID = $state.params.timeSheetDriverID;
    dataService.FLMMobileSchedule_Get(timeSheetDriverID).then(function (res) {
        $scope.AcceptedItem = res;
    })
    

    $scope.BackToTruck = function () {
        $state.go('driver.summary');
    }

    $scope.ShowSOList = function (masterID, locationID, statusID, timedriverID, timesheetID) {
            $state.go('driver.truck_detail', { masterID: masterID, locationID: locationID, statusID: statusID, sheetDriverID: timedriverID, sheetID: timesheetID })
    }
});