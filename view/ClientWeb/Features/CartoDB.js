/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CartoDB = {
    URL: {

    },
    Data: {
    }
}

angular.module('myapp').controller('CartoDBController', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap, $compile) {
    Common.Log('CartoDBController');
    $rootScope.IsLoading = false;


}]);