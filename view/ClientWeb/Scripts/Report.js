var featuresV = '?v=0.33';
var featuresA = '';
var features = [];

features.push({ name: 'ScheduleDriverFee', url: '^/ScheduleDriverFee/:headerkey/:lstid/:month/:year', templateUrl: featuresA + 'Reports/Reports.html' + featuresV, controller: 'ScheduleDriverFeeCtrl', cache: false });
features.push({ name: 'WorkOrderFuel', url: '^/WorkOrderFuel/:headerkey/:UserName/:lstid', templateUrl: featuresA + 'Reports/Reports.html' + featuresV, controller: 'WorkOrderFuelCtrl', cache: false });
features.push({ name: 'OPSSotrans', url: '^/OPSSotrans/:headerkey/:masterid', templateUrl: featuresA + 'Reports/Reports.html' + featuresV, controller: 'OPSSotransCtrl', cache: false });

angular.module('myapp').config(['$stateProvider', function ($stateProvider) {
    var states = [];

    //states.push({
    //    cache: false,
    //    name: 'main', url: "^/main",
    //    views: {
    //        'view': { templateUrl: featuresA + 'Reports/Reports.html' + featuresV }
    //    }
    //});

    $.each(features, function (i, v) {
        states.push({
            cache: v.cache,
            name: v.name, url: v.url,
            views: {
                'view': { templateUrl: v.templateUrl, controller: v.controller }
            }
        });
    });

    angular.forEach(states, function (state) { $stateProvider.state(state); });
}]);

//#region Data
var _default = {
    URL: {
        GetAuthorization: 'App_GetAuthorization',
        ListResource: 'App_ListResource',
        ListResourceEmpty: 'App_ListResourceEmpty',
        FileList: 'App_FileList',
        FileSave: 'App_FileSave',
        FileDelete: 'App_FileDelete',
        CommentList: 'App_CommentList',
        CommentSave: 'App_CommentSave',
        MessageCall_User: 'App_MessageCall_User',

        FileHandler: '/Handler/File.ashx',
        NoImage: '/Images/empty.jpg',
    }
};
//#endregion

angular.module('myapp').controller('defaultController', ['$rootScope', '$scope', '$http', '$sce', '$location', '$state', '$timeout', '$window', '$compile', function ($rootScope, $scope, $http, $sce, $location, $state, $timeout, $window, $compile) {
    Common.Log('defaultController');

}]);