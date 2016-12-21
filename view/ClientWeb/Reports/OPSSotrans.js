/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const



//#endregion

angular.module('myapp').controller('OPSSotransCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSSotransCtrl');
    $timeout(function () {
        var pars = $state.params;
        $('#report').telerik_ReportViewer({
            serviceUrl: Common.Report.ServiceUrl,
            templateUrl: Common.Report.TemplateUrl,
            reportSource: Common.Report.ReportSource('rpOPSSotrans', { headerkey: pars.headerkey, masterid: pars.masterid }),
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