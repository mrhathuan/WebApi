/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerDriverRole = {
    URL: {
        REPList: 'FLMDriver_List',
        REPActualData: 'REPFLMDriverRole_ActualData',
        REPPlanData: 'REPFLMDriverRole_PlanData',
    },

    Data: {
        toDay: null,
        DriverResources: [],
    },
    Timeline: null
}


angular.module('myapp').controller('REPOwnerDriverRole_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerDriverRole_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.LstTimelineResource = [];
    $scope.Item = null;
    _REPOwnerDriverRole.Data.toDay = new Date();

    $scope.ItemSearch = {
        isPlan: true,
        dateFrom: new Date(_REPOwnerDriverRole.Data.toDay.getFullYear(), _REPOwnerDriverRole.Data.toDay.getMonth(), _REPOwnerDriverRole.Data.toDay.getDate() - _REPOwnerDriverRole.Data.toDay.getDay()),
        dateTo: new Date(_REPOwnerDriverRole.Data.toDay.getFullYear(), _REPOwnerDriverRole.Data.toDay.getMonth(), _REPOwnerDriverRole.Data.toDay.getDate() + -_REPOwnerDriverRole.Data.toDay.getDay() + 6)
    };


    $scope.main_schedulerOptions = {
        date: new Date().addDays(1),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false,
        eventHeight: 20, majorTick: 60,
        height: '99%',
        messages: {
            today: "Hôm nay"
        },
        editable: {
            destroy: false, create: false, update: false
        },
        views: [
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 50,
                selectedDateFormat: "{0:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 120,
                group: {
                    orientation: "vertical"
                }
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 50,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 720,
                group: {
                    orientation: "vertical"
                },
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 50,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440,
                group: {
                    orientation: "vertical"
                }
            }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "ID",
                    fields: {
                        ID: { from: "ID", type: "number" },
                        start: { type: "date", from: "DateFrom" },
                        end: { type: "date", from: "DateTo" },
                        attendees: { from: "DriverID" },
                    }
                }
            }
        },


        eventTemplate: $("#task-template").html(),
        group: {
            resources: ["DriverID"],
            orientation: "vertical"
        },
        dataBound: function (e) {

        },
        resources: [
          {
              field: "attendees",
              name: "DriverID",
              dataTextField: 'DriverName',
              dataValueField: 'DriverID',
              dataSource: [],
          }
        ],

        navigate: function (e) {
            var schedule = this;
            $rootScope.IsLoading = true;
            $timeout(function () {
                var view = schedule.view();
                var viewName = schedule.viewName();
                var date = $scope.main_scheduler.date();
                $scope.ItemSearch.dateFrom = view.startDate();
                $scope.ItemSearch.dateTo = view.endDate();
                $scope.Loaddata();
            }, 10)

        },
        save: function (e) {

        }

    }

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPOwnerDriverRole.URL.REPList,
        success: function (res) {

            $scope.main_schedulerOptions.resources[0].dataSource = res.Data;
        }
    });

    $scope.Loaddata = function () {
        Common.Log("Load data isPlan:" + $scope.ItemSearch .isPlan+ " From:" + $scope.ItemSearch.dateFrom + " To:" + $scope.ItemSearch.dateTo)
        if ($scope.ItemSearch.isPlan)//ke hoach
        {
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPOwnerDriverRole.URL.REPPlanData,
                data: { dateFrom: $scope.ItemSearch.dateFrom, dateTo: $scope.ItemSearch.dateTo },
                success: function (res) {
                    
                    var dataSource = new kendo.data.SchedulerDataSource({
                        data: res,
                        schema: {
                            model: {
                                id: "ID",
                                fields: {
                                    ID: { from: "ID", type: "number" },
                                    start: { type: "date", from: "DateFrom" },
                                    end: { type: "date", from: "DateTo" },
                                    attendees: { from: "DriverID" },
                                }
                            }
                        }
                    });

                    $scope.main_scheduler.setDataSource(dataSource);
                    $rootScope.IsLoading = false;
                }
            });
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPOwnerDriverRole.URL.REPActualData,
                data: { dateFrom: $scope.ItemSearch.dateFrom, dateTo: $scope.ItemSearch.dateTo, },
                success: function (res) {

                    var dataSource = new kendo.data.SchedulerDataSource({
                        data: res,
                        schema: {
                            model: {
                                id: "ID",
                                fields: {
                                    ID: { from: "ID", type: "number" },
                                    start: { type: "date", from: "DateFrom" },
                                    end: { type: "date", from: "DateTo" },
                                    attendees: { from: "DriverID" },
                                }
                            }
                        }
                    });

                    $scope.main_scheduler.setDataSource(dataSource);
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Loaddata();

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.Loaddata();
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPOwnerDriverRole,
            event: $event,
            current: $state.current
        });
    };
}]);