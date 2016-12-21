
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />

//#region URL
var _CATLocation_test = {
    URL: {
        Read: 'CATLocationTest_Read',
        Resource: 'CATLocationTest_Resource'
    },
    Data: {
        FileName: ''
    }
}

angular.module('myapp').controller('CATLocation_TestCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATLocation_TestCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.new_timeline_v2Options = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: true, resize: true, update: false },
        //editable: { create: false, destroy: false, move: false, resize: false, update: false },
        views: [
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 40,
                selectedDateFormat: "{0:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 120
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 40,
                selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 360
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 60,
                selectedDateFormat: "{0:MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 360
            }
        ],
        dataSource: {
        },
        eventTemplate: $("#new-timeline-v2-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            
        },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '', text: 'Không có DL' }], multiple: true
            }
        ],
        navigate: function (e) {
            $timeout(function () {
                $scope.LoadData();
            },1)
        }
    }
    
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATLocation_test.URL.Resource,
        data: {  },
        success: function (res) {
            $rootScope.IsLoading = false;
            var data = [];
            Common.Data.Each(res, function (o) {
                data.push({
                    value: o.VendorID + "_" + o.VehicleID, text: o.Text, VendorID: o.VendorID, VendorCode: o.VendorCode, VendorName: o.VendorName,
                    RomoocID: o.RomoocID, RomoocNo: o.RomoocNo, VehicleID: o.VehicleID, VehicleNo: o.VehicleNo, IsChoose: false
                });
            })
            if (data.length == 0) {
                data.push({
                    value: '-2_-1', text: "DL trống", VendorID: -1, VendorCode: "", VendorName: "", VehicleID: -1, VehicleNo: "", IsChoose: false
                });
            }
            Common.Log("load resource: ")
            Common.Log(data);
            $scope.new_timeline_v2.resources[0].dataSource.data(data);

            $scope.LoadData();
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    $scope.LoadData = function () {
        var view = $scope.new_timeline_v2.view();
        var viewName = $scope.new_timeline_v2.viewName();
        var date = $scope.new_timeline_v2.date();

        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocation_test.URL.Read,
            data: { dtStart:view.startDate(),dtEnd: view.endDate()},
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Data.Each(res, function (o) {
                    o.field = o.VendorID + "_" + o.VehicleID;
                    o.StartDate = Common.Date.FromJson(o.StartDate);
                    o.EndDate = Common.Date.FromJson(o.EndDate);
                })
                Common.Log("load data: ")
                Common.Log(res);
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: res,
                    schema: {
                        model: {
                            id: "id",
                            fields: {
                                id: { from: "ID", type: "number" },
                                title: { from: "Title" },
                                start: { type: "date", from: "StartDate" },
                                end: { type: "date", from: "EndDate" },
                                field: { from: "field" }
                            }
                        }
                    }
                });
                $scope.new_timeline_v2.setDataSource(dataSource);

                $timeout(function () {
                    $scope.new_timeline_v2.date(date);
                    $scope.new_timeline_v2.view(viewName);
                    //$scope.main_scheduler.refresh();
                }, 10)
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

}]);