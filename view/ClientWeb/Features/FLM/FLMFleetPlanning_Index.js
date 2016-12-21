
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMFleetPlanning = {
    URL: {
        DriverList: 'FLMVehiclePlan_DriverList',
        Data: 'FLMVehiclePlan_Data',
        ListVehicle: 'FLMVehiclePlan_VehicleList',
        VehiclePlan_Save: 'FLMVehiclePlan_Save',
        VehiclePlan_Get: 'FLMVehiclePlan_Get',
        VehiclePlan_Delete: 'FLMVehiclePlan_Delete',
    },
    Data: {
        toDay: null,
        nextDay: null
    },
}

angular.module('myapp').controller('FLMFleetPlanning_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMFleetPlanning_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.Show_Gantt_Info = false;
    $scope.HasChoose = false;
    $scope.IsEdit = false;
    $scope.ItemEdit = null;
    $scope.IsShowDelete = false;
    $scope.ItemVehicle = null;

    $scope.Item = null;
    _FLMFleetPlanning.Data.toDay = new Date();
    _FLMFleetPlanning.Data.nextDay = new Date().addDays(-1);

    $scope.ItemSearch = {
        ListFLMAssetID: [],
        dateFrom: new Date(_FLMFleetPlanning.Data.toDay.getFullYear(), _FLMFleetPlanning.Data.toDay.getMonth(), _FLMFleetPlanning.Data.toDay.getDate() - _FLMFleetPlanning.Data.toDay.getDay()),
        dateTo: new Date(_FLMFleetPlanning.Data.toDay.getFullYear(), _FLMFleetPlanning.Data.toDay.getMonth(), _FLMFleetPlanning.Data.toDay.getDate() + -_FLMFleetPlanning.Data.toDay.getDay() + 6)
    };

    $scope.mtsFLMAsset_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RegNo: { type: 'string' },
                    TypeOfAssetName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "RegNo", dataValueField: "ID", placeholder: "Chọn xe...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= RegNo #</span><span style="float:right;">#= TypeOfAssetName #</span>',
        headerTemplate: '<strong><span> Số xe </span><span style="float:right;"> Loại </span></strong>',
        change: function (e) {

        }
    }



    $scope.MoveTime = {};

    $scope.main_schedulerOptions = {
        date: new Date(),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false,
        eventHeight: 29, majorTick: 60,
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
        timezone: "Etc/UTC",
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "meetingID",
                    fields: {
                        meetingID: { from: "ID", type: "number" },
                        title: { from: "DriverName", defaultValue: "No title", validation: { required: true } },
                        start: { type: "date", from: "DateFrom" },
                        end: { type: "date", from: "DateTo" },
                        attendees: { from: "VehicleID" },
                        roomId: { from: "TypeOfDriverID" },
                    }
                }
            }
        },
        eventTemplate: $("#task-template").html(),
        group: {
            resources: ["VehicleID", "TypeOfDriverID"],
            orientation: "vertical"
        },
        resources: [
            {
                field: "attendees",
                name: "VehicleID",
                dataTextField: 'RegNo',
                dataValueField: 'VehicleID',
                dataSource: [
                    { RegNo: " ", VehicleID: -1 },
                ],

            },
            {
                field: "roomId",
                name: "TypeOfDriverID",
                dataTextField: 'TypeOfDriverName',
                dataValueField: 'TypeOfDriverID',
                dataSource: [
                    { TypeOfDriverID: -1, TypeOfDriverName: " " }
                ],
                multiple: true,
            }
        ],
        navigate: function (e) {
            var schedule = this;
            $rootScope.IsLoading = true;
            $timeout(function () {
                var view = schedule.view();
                var viewName = schedule.viewName();
                //var date = $scope.main_scheduler.date();
                $scope.ItemSearch.dateFrom = view.startDate();
                $scope.ItemSearch.dateTo = view.endDate();

                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMFleetPlanning.URL.Data,
                    data: {
                        lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                        dateFrom: $scope.ItemSearch.dateFrom,
                        dateTo: $scope.ItemSearch.dateTo
                    },
                    success: function (res) {
                        var dataTask = [];
                        Common.Data.Each(res.ListTask, function (o) {
                            o.DateFrom = Common.Date.FromJson(o.DateFrom);
                            o.DateTo = Common.Date.FromJson(o.DateTo);
                            if (o.DateFrom <= o.DateTo) {
                                dataTask.push(o)
                            }
                        });

                        _FLMFleetPlanning.Data.VehiclePlan_Data = dataTask;
                        $scope.main_schedulerOptions.dataSource.data = dataTask;
                        if (res.ListRSVehicle.length > 0)
                            $scope.main_schedulerOptions.resources[0].dataSource = res.ListRSVehicle;
                        else $scope.main_schedulerOptions.resources[0].dataSource = [{ VehicleID: -1, RegNo: " " }];

                        if (res.ListRSTypeDriver.length > 0)
                            $scope.main_schedulerOptions.resources[1].dataSource = res.ListRSTypeDriver;
                        else $scope.main_schedulerOptions.resources[1].dataSource = [{ TypeOfDriverID: -1, TypeOfDriverName: " " }];

                        $rootScope.IsLoading = false;

                        $timeout(function () {
                            //$scope.main_scheduler.date(date);
                            $scope.main_scheduler.view(viewName);
                            // $scope.main_scheduler.refresh();
                        }, 1)
                    },
                    error: function (e) {
                        $rootScope.IsLoading = false;
                    }
                });

            }, 10)

        },
        save: function (e) {
            if (Common.HasValue(e.event.attendees[0]))
                $scope.MoveTime.VehicleID = e.event.attendees[0];
            else if (Common.HasValue(e.event.attendees))
                $scope.MoveTime.VehicleID = e.event.attendees;
            $scope.MoveTime.DriverID = e.event.DriverID;
            $scope.MoveTime.TypeOfDriverID = e.event.TypeOfDriverID;
            $scope.MoveTime.DateFrom = e.event.start;
            $scope.MoveTime.DateTo = e.event.end;
            $scope.MoveTime.ID = e.event.id;

            var data = [];
            var flag = false;
            $.each(_FLMFleetPlanning.Data.VehiclePlan_Data, function (i, v) {
                if (v.DriverID == $scope.MoveTime.DriverID && v.ID != $scope.MoveTime.ID) {
                    data = v;
                    if (($scope.MoveTime.DateFrom.getTime() < data.DateFrom.getTime() && $scope.MoveTime.DateTo.getTime() < data.DateFrom.getTime()) ||
                   ($scope.MoveTime.DateFrom.getTime() > data.DateTo.getTime() && $scope.MoveTime.DateTo.getTime() > data.DateTo.getTime())) { }
                    else { flag = true; }
                }
            });

            if (Common.HasValue(data)) {
                if (!flag) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMFleetPlanning.URL.VehiclePlan_Save,
                        data: { item: $scope.MoveTime },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });

                } else {
                    $rootScope.Message({
                        Msg: 'Không thể phân công!',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                }
            } else {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMFleetPlanning.URL.VehiclePlan_Save,
                    data: { item: $scope.MoveTime },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        }
    }

    $scope.LoadData = function () {
        $rootScope.IsLoading = true;
        
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleetPlanning.URL.Data,
            data: {
                lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                dateFrom: $scope.ItemSearch.dateFrom,
                dateTo: $scope.ItemSearch.dateTo
            },
            success: function (res) {
                var dataTask = [];
                Common.Data.Each(res.ListTask, function (o) {
                    o.DateFrom = Common.Date.FromJson(o.DateFrom);
                    o.DateTo = Common.Date.FromJson(o.DateTo);
                    if (o.DateFrom <= o.DateTo) {
                        dataTask.push(o)
                    }
                });

                _FLMFleetPlanning.Data.VehiclePlan_Data = dataTask;
                $scope.main_schedulerOptions.dataSource.data = dataTask;
                if (res.ListRSVehicle.length > 0)
                    $scope.main_schedulerOptions.resources[0].dataSource = res.ListRSVehicle;
                else $scope.main_schedulerOptions.resources[0].dataSource = [{ VehicleID: -1, RegNo: " " }];

                if (res.ListRSTypeDriver.length > 0)
                    $scope.main_schedulerOptions.resources[1].dataSource = res.ListRSTypeDriver;
                else $scope.main_schedulerOptions.resources[1].dataSource = [{ TypeOfDriverID: -1, TypeOfDriverName: " " }];
               
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.LoadData();

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var date = $scope.main_scheduler.date();
        var view = $scope.main_scheduler.view();
        var viewName = $scope.main_scheduler.viewName();
        $scope.ItemSearch.dateFrom = view.startDate();
        $scope.ItemSearch.dateFrom = view.endDate();
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleetPlanning.URL.Data,
            data: {
                lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                dateFrom: view.startDate(),
                dateTo: view.endDate(),
            },
            success: function (res) {
                var dataTask = [];
                Common.Data.Each(res.ListTask, function (o) {
                    o.DateFrom = Common.Date.FromJson(o.DateFrom);
                    o.DateTo = Common.Date.FromJson(o.DateTo);
                    if (o.DateFrom <= o.DateTo) {
                        dataTask.push(o)
                    }
                });

                _FLMFleetPlanning.Data.VehiclePlan_Data = dataTask;
                $scope.main_schedulerOptions.dataSource.data = dataTask;
                debugger
                if (res.ListRSVehicle.length > 0)
                    $scope.main_schedulerOptions.resources[0].dataSource = res.ListRSVehicle;
                else $scope.main_schedulerOptions.resources[0].dataSource = [{ VehicleID: -1, RegNo: " " }];

                if (res.ListRSTypeDriver.length > 0)
                    $scope.main_schedulerOptions.resources[1].dataSource = res.ListRSTypeDriver;
                else $scope.main_schedulerOptions.resources[1].dataSource = [{ TypeOfDriverID: -1, TypeOfDriverName: " " }];

                $timeout(function () {
                    $scope.main_scheduler.date(date);
                    $scope.main_scheduler.view(viewName);
                    // $scope.main_scheduler.refresh();
                }, 1)

                $rootScope.IsLoading = false;
            },
            error: function (e) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.Driver_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleetPlanning.URL.DriverList,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            },
            pageSize: 20,
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, selectable: true,
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="driver_Add_Click($event,dataItem,driver_Add_win)" class="k-button"><i class="fa fa-plus"></i></a>',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'EmployeeCode', title: 'Mã nhân viên', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LastName', title: 'Họ', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FirstName', title: 'Tên', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.FLMFLeetPlanning_Add_Click = function ($event) {
        $event.preventDefault();
        $scope.Show_Gantt_Info = true;
    };

    $scope.Event_Click = function ($event, ID, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleetPlanning.URL.VehiclePlan_Get,
            data: { ID: ID },
            success: function (res) {
                $scope.ItemVehicle = res;
                $scope.ItemVehicle.DriverID = ID;
                win.center().open();
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.driver_Add_Click = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleetPlanning.URL.VehiclePlan_Get,
            data: { ID: 0 },
            success: function (res) {

                $scope.ItemVehicle = res;
                $scope.ItemVehicle.DriverID = data.ID;
                $scope.ItemVehicle.DateFrom = Common.Date.FromJson($scope.ItemVehicle.DateFrom)
                $scope.ItemVehicle.DateTo = Common.Date.FromJson($scope.ItemVehicle.DateTo)
                win.center().open();
                $rootScope.IsLoading = false;
            }
        })
    };

    $scope.cboVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'RegNo', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RegNo: { type: 'string' },
                }
            }
        })
    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMFleetPlanning.URL.ListVehicle,
        data: {},
        success: function (res) {
            $scope.mtsFLMAsset_Options.dataSource.data(res.Data)
            $scope.cboVehicle_Options.dataSource.data(res.Data)
        }
    });

    $scope.typeOfDriver_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    };

    $scope.Driver_Save_Click = function ($event, win, driver_vform) {
        $event.preventDefault();
        if (driver_vform() && $scope.ItemVehicle.VehicleID > 0 && $scope.ItemVehicle.TypeOfDriverID > 0) {
           
                if ($scope.ItemVehicle.DateFrom.getTime() >= $scope.ItemVehicle.DateTo.getTime()) 
                    $rootScope.Message({Msg: 'Thời gian không chính xác', NotifyType: Common.Message.NotifyType.ERROR});
                else {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMFleetPlanning.URL.VehiclePlan_Save,
                        data: { item: $scope.ItemVehicle },
                        success: function (res) {
                            $rootScope.Message({  Msg: 'Đã cập nhật.', NotifyType: Common.Message.NotifyType.SUCCESS  });
                            win.close();
                            var view = $scope.main_scheduler.view();
                            var viewName = $scope.main_scheduler.viewName();
                            //var date = $scope.main_scheduler.date();
                            $scope.ItemSearch.dateFrom = view.startDate();
                            $scope.ItemSearch.dateTo = view.endDate();

                            Common.Services.Call($http, {
                                url: Common.Services.url.FLM,
                                method: _FLMFleetPlanning.URL.Data,
                                data: {
                                    lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                                    dateFrom: $scope.ItemSearch.dateFrom,
                                    dateTo: $scope.ItemSearch.dateTo
                                },
                                success: function (res) {
                                    var dataTask = [];
                                    Common.Data.Each(res.ListTask, function (o) {
                                        o.DateFrom = Common.Date.FromJson(o.DateFrom);
                                        o.DateTo = Common.Date.FromJson(o.DateTo);
                                        if (o.DateFrom <= o.DateTo) {
                                            dataTask.push(o)
                                        }
                                    });

                                    _FLMFleetPlanning.Data.VehiclePlan_Data = dataTask;
                                    $scope.main_schedulerOptions.dataSource.data = dataTask;
                                    if (res.ListRSVehicle.length > 0)
                                        $scope.main_schedulerOptions.resources[0].dataSource = res.ListRSVehicle;
                                    else $scope.main_schedulerOptions.resources[0].dataSource = [{ VehicleID: -1, RegNo: " " }];

                                    if (res.ListRSTypeDriver.length > 0)
                                        $scope.main_schedulerOptions.resources[1].dataSource = res.ListRSTypeDriver;
                                    else $scope.main_schedulerOptions.resources[1].dataSource = [{ TypeOfDriverID: -1, TypeOfDriverName: " " }];
                                    $timeout(function () {
                                        //$scope.main_scheduler.date(date);
                                        $scope.main_scheduler.view(viewName);
                                        // $scope.main_scheduler.refresh();
                                    }, 1)
                                    $rootScope.IsLoading = false;
                                }
                            });
                        }
                    });
                }

        } else {
            $rootScope.Message({ Msg: 'Chưa nhập đủ thông tin.', NotifyType: Common.Message.NotifyType.ERROR });
        }
    };

    $scope.Driver_Delete_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMFleetPlanning.URL.VehiclePlan_Delete,
                    data: { id: $scope.ItemVehicle.ID },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Đã cập nhật.', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        var view = $scope.main_scheduler.view();
                        var viewName = $scope.main_scheduler.viewName();
                        //var date = $scope.main_scheduler.date();
                        $scope.ItemSearch.dateFrom = view.startDate();
                        $scope.ItemSearch.dateTo = view.endDate();

                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMFleetPlanning.URL.Data,
                            data: {
                                lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                                dateFrom: $scope.ItemSearch.dateFrom,
                                dateTo: $scope.ItemSearch.dateTo
                            },
                            success: function (res) {
                                var dataTask = [];
                                Common.Data.Each(res.ListTask, function (o) {
                                    o.DateFrom = Common.Date.FromJson(o.DateFrom);
                                    o.DateTo = Common.Date.FromJson(o.DateTo);
                                    if (o.DateFrom <= o.DateTo) {
                                        dataTask.push(o)
                                    }
                                });

                                _FLMFleetPlanning.Data.VehiclePlan_Data = dataTask;
                                $scope.main_schedulerOptions.dataSource.data = dataTask;
                                if (res.ListRSVehicle.length > 0)
                                    $scope.main_schedulerOptions.resources[0].dataSource = res.ListRSVehicle;
                                else $scope.main_schedulerOptions.resources[0].dataSource = [{ VehicleID: -1, RegNo: " " }];

                                if (res.ListRSTypeDriver.length > 0)
                                    $scope.main_schedulerOptions.resources[1].dataSource = res.ListRSTypeDriver;
                                else $scope.main_schedulerOptions.resources[1].dataSource = [{ TypeOfDriverID: -1, TypeOfDriverName: " " }];
                                $timeout(function () {
                                    //$scope.main_scheduler.date(date);
                                    $scope.main_scheduler.view(viewName);
                                    // $scope.main_scheduler.refresh();
                                }, 1)
                                $rootScope.IsLoading = false;
                            }
                        });
                    }
                });
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarTypeOfDriver,
        success: function (data) {
            $scope.typeOfDriver_Options.dataSource.data(data);
        }
    })

    $scope.CloseWinInfo = function (e) {
        e.preventDefault();
        $scope.Show_Gantt_Info = false;
    };

    $scope.win_close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.FLMFleetPlanning,
            event: $event,
            current: $state.current
        });
    };
}]);