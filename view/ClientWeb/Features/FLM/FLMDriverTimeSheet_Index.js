
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMDriverTimeSheet = {
    URL: {
        VehicleList: 'FLMDriverTimeSheet_VehicleList',
        VehicleTimeList: 'FLMDriverTimeSheet_VehicleTimeList',
        VehicleTimeGet: 'FLMDriverTimeSheet_VehicleTimeGet',
        VehicleTimeSave: 'FLMDriverTimeSheet_VehicleTimeSave',
        VehicleTimeDelete: 'FLMDriverTimeSheet_VehicleTimeDelete',

        DriverTime_Save: 'FLMDriverTimeSheet_DriverSave',
        DriverTime_Delete: 'FLMDriverTimeSheet_DriverDelete',

        Get_ListDriver: 'FLMDriverTimeSheet_DriverList',
        TimeSheet_ChangeType: 'FLMDriverTimeSheet_ChangeType',
    },
    Data: {
        toDay: null,
        DriverResources: [],
        VehicleList: [],
        TruckList: [],
        TractorList: []
    }
}
angular.module('myapp').controller('FLMDriverTimeSheet_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMDriverTimeSheet_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.TimeSheetID = 0;
    $scope.TabIndex = 0;
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FLMDriverTimeSheet,
            event: $event,
            current: $state.current
        });
    };

    _FLMDriverTimeSheet.Data.toDay = new Date();
    $scope.ItemSearch = {
        dateFrom: new Date(_FLMDriverTimeSheet.Data.toDay.getFullYear(), _FLMDriverTimeSheet.Data.toDay.getMonth(), _FLMDriverTimeSheet.Data.toDay.getDate() - _FLMDriverTimeSheet.Data.toDay.getDay()),
        dateTo: new Date(_FLMDriverTimeSheet.Data.toDay.getFullYear(), _FLMDriverTimeSheet.Data.toDay.getMonth(), _FLMDriverTimeSheet.Data.toDay.getDate() + -_FLMDriverTimeSheet.Data.toDay.getDay() + 6),
        IsTruck: true,
        IsTractor: true
    };
    $scope.FLMDriverTimeSheet_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    };
    //#region scheduler
    $scope.main_schedulerOptions = {
        date: new Date(),
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
                    id: "meetingID",
                    fields: {
                        meetingID: { from: "ID", type: "number" },
                        title: { from: "Note", defaultValue: "No title", validation: { required: true } },
                        start: { type: "date", from: "DateFrom" },
                        end: { type: "date", from: "DateTo" },
                        attendees: { from: "AssetID" },
                    }
                }
            }
        },
        eventTemplate: $("#task-template").html(),
        group: {
            resources: ["AssetID"],
            orientation: "vertical"
        },
        dataBound: function (e) {

        },
        resources: [
            {
                field: "attendees",
                name: "AssetID",
                dataTextField: 'RegNo',
                dataValueField: 'AssetID',
                dataSource: [{ RegNo: "61C-09204" ,AssetID:1 }],
                multiple: true,
            }
        ],
        navigate: function (e) {
            var schedule = this;
            $rootScope.IsLoading = true;
            $timeout(function () {
                var view = schedule.view();
                var date = $scope.main_scheduler.date();
                var viewName = schedule.viewName();
                $scope.ItemSearch.dateFrom = view.startDate();
                $scope.ItemSearch.dateTo = view.endDate();
               // Common.Log("lay range:" + $scope.ItemSearch.dateFrom + "_" + $scope.ItemSearch.dateTo)
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverTimeSheet.URL.VehicleTimeList,
                    data: {
                        dateFrom: $scope.ItemSearch.dateFrom,
                        dateTo: $scope.ItemSearch.dateTo
                    },
                    success: function (res) {
                        var dataSource = new kendo.data.SchedulerDataSource({
                            data: res,
                            schema: {
                                model: {
                                    id: "meetingID",
                                    fields: {
                                        meetingID: { from: "ID", type: "number" },
                                        title: { from: "Note", defaultValue: "No title", validation: { required: true } },
                                        start: { type: "date", from: "DateFrom" },
                                        end: { type: "date", from: "DateTo" },
                                        attendees: { from: "AssetID" },
                                    }
                                }
                            }
                        });
                        $scope.main_scheduler.setDataSource(dataSource);
                        $rootScope.IsLoading = false;

                        $timeout(function () {
                            $scope.main_scheduler.date(date);
                            $scope.main_scheduler.view(viewName);
                        }, 1)
                    },
                    error: function (e) {
                        $rootScope.IsLoading = false;
                    }
                });
            }, 10)

        },
        save: function (e) {

        }
    }

    //load resource xe
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDriverTimeSheet.URL.VehicleList,
        success: function (res) {
            _FLMDriverTimeSheet.Data.VehicleList = res;

            _FLMDriverTimeSheet.Data.TractorList = [];
            _FLMDriverTimeSheet.Data.TruckList = [];

            Common.Data.Each(res, function (o) {
                if (o.TypeOfAssetID == 1)//xe tải
                    _FLMDriverTimeSheet.Data.TruckList.push(o);
                if (o.TypeOfAssetID == 2)//đầu kéo
                    _FLMDriverTimeSheet.Data.TractorList.push(o);
            })
            $scope.main_schedulerOptions.resources[0].dataSource = res;
        }
    });
    $scope.LoadDriverTimeSheet = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverTimeSheet.URL.VehicleTimeList,
            data: { dateFrom: $scope.ItemSearch.dateFrom, dateTo: $scope.ItemSearch.dateTo },
            success: function (res) {
                $scope.main_schedulerOptions.dataSource.data = res;
            }
        });
    };
    $scope.LoadDriverTimeSheet();

    $scope.DriverTimeSheet_Search_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        var view = $scope.main_scheduler.view();
        var viewName = $scope.main_scheduler.viewName();

        if ($scope.ItemSearch.IsTruck && $scope.ItemSearch.IsTractor)
            $scope.main_schedulerOptions.resources[0].dataSource = _FLMDriverTimeSheet.Data.VehicleList;
        else if ($scope.ItemSearch.IsTruck && !$scope.ItemSearch.IsTractor)
            $scope.main_schedulerOptions.resources[0].dataSource = _FLMDriverTimeSheet.Data.TruckList;
        else $scope.main_schedulerOptions.resources[0].dataSource = _FLMDriverTimeSheet.Data.TractorList;

        $timeout(function () {

            $scope.main_scheduler.view(viewName);
            $rootScope.IsLoading = false;
        }, 10)
    }

    $scope.Event_Click = function (ID, win) {
        $rootScope.IsLoading = true;
        $scope.TimeSheetID = ID;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverTimeSheet.URL.VehicleTimeGet,
            data: { timeID: ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $timeout(function () {
                        $scope.FLMDriverTimeSheet_Driver_grid.dataSource.data(res.ListDriver)
                        $scope.Item = res;
                        $rootScope.IsLoading = false;
                        win.center().open();
                    }, 10);
                });
            }
        });
    }

    $scope.FLMDriverTimeSheet_Driver_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Birthday: { type: 'date' }
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
             {
                 title: ' ', width: '35px',
                 template: '',
                 filterable: false, sortable: false
             },
            { field: "EmployeeCode", title: 'Mã tài xế', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'LastName', title: 'Họ', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'FirstName', title: 'Tên', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'Birthday', title: 'Ngày sinh', template: "#=Birthday==null?' ':Common.Date.FromJsonDMY(Birthday)#", width: 120,
                filterable: { cell: { showOperators: false, operator: "gte" } }
            },
            { field: 'Cellphone', title: 'Điện thoại', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'CardNo', title: 'CMND', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'Note', title: 'Ghi chú', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { title: '', sortable: false, menu: false, filterable: false }
        ]
    }

    $scope.ChangeTypeTimeSheet_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverTimeSheet.URL.TimeSheet_ChangeType,
            data: {timeID:$scope.TimeSheetID},
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverTimeSheet.URL.VehicleTimeGet,
                    data: { timeID: $scope.TimeSheetID },
                    success: function (res) {
                        $scope.Item = res;
                        $scope.FLMDriverTimeSheet_Driver_grid.dataSource.data(res.ListDriver)
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', NotifyType: Common.Message.NotifyType.ERROR });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    //#endregion


    $scope.cboDriver_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'DriverName', dataValueField: 'ID',index:0,
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                DriverName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDriverTimeSheet.URL.Get_ListDriver,
        data: {},
        success: function (res) {
            $scope.cboDriver_Options.dataSource.data(res)
        }
    });

    $scope.DriverSave_Click = function ($event, vform, win) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.DriverID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverTimeSheet.URL.DriverTime_Save,
                    data: { timeID: $scope.TimeSheetID, driverID: $scope.DriverID },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMDriverTimeSheet.URL.VehicleTimeGet,
                            data: { timeID: $scope.TimeSheetID },
                            success: function (res) {
                                $scope.Item = res;
                                $scope.FLMDriverTimeSheet_Driver_grid.dataSource.data(res.ListDriver)
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.ERROR });
                                win.close();
                            }
                        });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
            else $rootScope.Message({ Msg: 'Tài xế không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.addDriver_Click = function ($event,win) {
        $event.preventDefault();
        win.center().open();
    }

    $scope.Close_Click = function ($event, win) {
        Common.Log("Close_Click");
        $event.preventDefault();
        win.close();
    };

}]);

