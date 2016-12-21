
angular.module('myapp').controller('FLMAsset_AddNewTruckCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMAsset_AddNewTruckCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.IsNewVehicle = true;
    $scope.IsEdited = false;

    if (_FLMAsset_TruckEdit.Param.AssetID > 0) { $scope.IsShowTab = true; $scope.IsNewVehicle = false; }

    $scope.FLMAsset_Truck_SaveClick = function ($event, vform) {
        $event.preventDefault();
        if ($scope.Is5Number()) {
            $scope.TruckItem.RegNo += $scope.TruckItem.IsWhite ? 'T' : 'X';
        }
        if (vform()) {
            $state.go("main.FLMAsset.EditTruck", { AssetID: -1, RegNo: $scope.TruckItem.RegNo });
        }
    }

    $scope.Is5Number = function () {
        if ($scope.TruckItem === undefined || $scope.TruckItem.RegNo === undefined) {
            return false;
        }
        var numberMatched = $scope.TruckItem.RegNo.trim().match(/(\d+)$/);
        if (numberMatched == null) {
            return false;
        }
        return numberMatched[0].length == 5;
    }

    $scope.FLMAsset_Truck_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMAsset.Index")
    }
}]);