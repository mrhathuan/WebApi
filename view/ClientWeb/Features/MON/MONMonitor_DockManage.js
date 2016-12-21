/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />


angular.module('myapp').controller('MONMonitor_DockManageCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('MONMonitor_DockManageCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.FullName = Common.Auth.Item.LastName + " " + Common.Auth.Item.FirstName;


    //actions
    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.PODInput.Index")
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.MONMonitor,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }

}]);