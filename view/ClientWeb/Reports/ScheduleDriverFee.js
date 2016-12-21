/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _ScheduleDriverFee = {

}

//#endregion

angular.module('myapp').controller('ScheduleDriverFeeCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('ScheduleDriverFeeCtrl');
    
    $timeout(function () {
        var pars = $state.params
        $('#report').telerik_ReportViewer({
            serviceUrl: Common.Report.ServiceUrl,
            templateUrl: Common.Report.TemplateUrl,
            reportSource: Common.Report.ReportSource('rpScheduleDriverFee', { headerkey: pars.headerkey, lstid: pars.lstid, month: pars.month, year: pars.year }),
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