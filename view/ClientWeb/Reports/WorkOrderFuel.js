/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

//var _WorkOrderFuel = {

//}

//#endregion

angular.module('myapp').controller('WorkOrderFuelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('WorkOrderFuelCtrl');

    $timeout(function () {
        var pars = $state.params
        $('#report').telerik_ReportViewer({
            serviceUrl: Common.Report.ServiceUrl,
            templateUrl: Common.Report.TemplateUrl,
            reportSource: Common.Report.ReportSource('rpWorkOrderFuel', { headerkey: pars.headerkey, UserName: pars.UserName, lstid: pars.lstid }),
            viewMode: Common.Report.ViewMode.PRINT_PREVIEW,
            scaleMode: Common.Report.ScaleMode.SPECIFIC,
            scale: 1,
            persistSession: false,
            printMode: Common.Report.PrintMode.AUTO_SELECT,
            disabledButtonClass: null,
            checkedButtonClass: null
        });
    }, 100);
}]);