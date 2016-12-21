/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONMonitor_Input_Container = {
    URL: {
        Read: 'MONCOInput_List',
        Update: 'MONCOInput_Save',
        Customer_List: "Customer_List",
        Vendor_List: "Vendor_List",
        Reset: 'PODDIInput_Check_Reset',
    },
    Data: {
        DIPODStatus: [],
        ListProduct: [],
    }
}

//#endregion

angular.module('myapp').controller('MONMonitor_DockRegisterCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile) {

    Common.Log('MONMonitor_DockRegisterCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    //#region Filter

    $scope.FitlerVendorID = 0;

    $scope.DateRequest = { fDate: new Date().addDays(-2), tDate: new Date().addDays(1) }
    $scope.DateTimeRequest = { fDate: $scope.DateRequest.fDate.setHours(0, 0, 0, 0, 0), tDate: $scope.DateRequest.tDate.setHours(0, 0, 0, 0, 0) }

    $scope.DateRequestChange_Click = function ($event) {
        $event.preventDefault();
        if ($scope.DateRequest.tDate <= $scope.DateRequest.fDate)
            $rootScope.Message({ Msg: "Thời gian không hợp lệ!", Type: Common.Message.Type.Alert });
        else
            $scope.LoadDataTimeLine(function () {
                $scope.veh_Grid.dataSource.read();
                $scope.con_Grid.dataSource.read();
            });
    }

    
    //#endregion

    //#region Timeline

    $scope.vehSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: true, size: '250px', min: '200px' },
            { collapsible: false, resizable: true, min: '700px' }
        ],
    };

    $scope.veh_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONDock_VehicleList",
            pageSize: 50,
            readparam: function () {
                return {
                    locationID: -1,
                    fDate: $scope.DateRequest.fDate,
                    tDate: $scope.DateRequest.tDate,
                }
            },
            model: {
                id: 'Text',
                fields: {
                    Text: { type: 'string' }
                }
            }
        }),
        pageable: { info: false, numeric: true, buttonCount: 1 },
        height: '100%', groupable: false, sortable: true, columnMenu: false, selectable: true, resizable: false, filterable: { mode: 'row' }, reorderable: false, autoBind: true,
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin chuyến...", 50);
            var grid = this;
            var data = [];
            $scope.VehicleResource = [];
            angular.forEach(grid.dataSource.view(), function (g) {
                var o = g;
                $scope.VehicleResource.push(o.ID);
                data.push({ value: o.ID, text: o.RegNo })

            })
            if (data.length == 0) {
                data.push({ value: '1--1--1', text: "DL trống" });
            }
            $scope.timelineVeh.resources[0].dataSource.data(data);
            $timeout(function () {
                $scope.LoadDataTimeLine();
            })
            $timeout(function () {
                $scope.Init_TimeLine_Veh_Times(1);
            }, 1000)


        },
        columns: [
            { field: 'RegNo', width: 100, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', width: 100, title: 'Nhà xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    };

    var CUSTOMVIEW = kendo.ui.TimelineMonthView.extend({
        nextDate: function () {
            return kendo.date.nextDay(this.startDate());
        },
        options: {
            columnWidth: 50, majorTick: 120, daysOfView: 14, selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
            dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
            majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>")
        },
        name: "CUSTOMVIEW",
        calculateDateRange: function () {
            var start = this.options.date, idx, length, dates = [];
            for (idx = 0, length = this.options.daysOfView; idx < length; idx++) {
                dates.push(start); start = kendo.date.nextDay(start);
            }
            this._render(dates);
        }
    });
    $scope.timelineVeh_Options = {
        date: $scope.DateRequest.fDate, footer: false, snap: false, autoBind: false,
        eventHeight: 20, majorTick: 60, height: '100%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: false, resize: false, update: false },
        views: [
            {
                type: CUSTOMVIEW, title: 'Custom'
            }
        ],
        dataSource: {
            data: [],
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
        },
        groupHeaderTemplate: kendo.template("<span  data-uid='#=value#' class='txtTimeLineVehicle' style='cursor: pointer;'><strong>#=text#</strong></span><i class='fa fa-spinner fa-spin' style='color:rgb(248, 248, 248);padding: 0 2px;'></i>"),
        eventTemplate: $("#timeline-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            if (scheduler.date().getTime() != $scope.DateTimeRequest.fDate) {
                scheduler.date($scope.DateRequest.fDate);
            } else {
                $scope.Init_TimeLine_Veh_Times(2);
                $timeout(function () {
                    var data = scheduler.dataSource.data();
                    $timeout(function () {
                        $scope.TimeLineLoading = false;
                    }, 1000)
                }, 10)
            }
        },
        dataBind: function () {
            $scope.TimeLineRefreshTrigger = true;
        },
        resources: [
            {
                field: "field", name: "Group",
                dataTextField: 'text',
                dataValueField: 'value',
                dataSource: [{ value: '', text: 'Data Empty' }], multiple: true
            }
        ],
        navigate: function () {
            $scope.ContentLeft = 0;
            $scope.TimeLineRefreshTrigger = true;
        },
        moveStart: function (e) {
            if ($scope.ActionType == 0) {
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || e.event.StatusOfTimeSheet != 1 || e.event.TOMasterID < 1) {
                    e.preventDefault();
                } else {
                    $scope.HideTimeLineTooltip();
                    $scope.TimeLineEventDragDrop = true;
                    $scope.TimeLineEventEdit = $.extend(true, {}, e.event);
                }
            } else {
                e.preventDefault();
            }
        },
        resizeStart: function (e) {
            if ($scope.ActionType == 0) {
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || e.event.StatusOfTimeSheet != 1 || e.event.TOMasterID < 1) {
                    e.preventDefault();
                } else {
                    $scope.TimeLineEventDragDrop = true;
                    $scope.HideTimeLineTooltip();
                    $scope.TimeLineEventEdit = $.extend(true, {}, e.event);
                }
            } else {
                e.preventDefault();
            }
        },
        save: function (e) {
            e.preventDefault();
            $scope.TimeLineEventDragDrop = false;
            $scope.HideTimeLineTooltip();
            var scheduler = this, obj = $.extend(true, {}, e.event), data = scheduler._data, field = "", venID = -1, vehID = -1;;
            if (typeof obj.field == "string") field = obj.field;
            else if (typeof obj.field == "object") field = obj.field[0];
            vehID = field.split('-')[1], romID = field.split('-')[2];
            var _refreshScheduler = function (sch) {
                var idx = 0;
                $.each(sch.dataSource.data(), function (i, o) {
                    if (o.id == obj.id && o.TypeOfGroupID == obj.TypeOfGroupID && o.StatusOfTimeSheet == obj.StatusOfTimeSheet) {
                        o.end = $scope.TimeLineEventEdit.end;
                        o.start = $scope.TimeLineEventEdit.start;
                        o.field = $scope.TimeLineEventEdit.field;
                    }
                })
                sch.refresh();
            }
            //Change Time
            if (vehID == obj.VehicleID && (obj.TypeOfGroupID != 1 ? romID == obj.GroupID : romID == "-1" || romID == "")) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Container_ChangeTimeOffer,
                    data: { mID: obj.TOMasterID, conID: obj.id, ETD: obj.start, ETA: obj.end },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            if (res.OfferTimeError != null && res.OfferTimeError != '') {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: res.OfferTimeError, Type: Common.Message.Type.Alert })
                            } else if (res.OfferTimeWarning != null && res.OfferTimeWarning != '') {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: res.OfferTimeWarning + ", tiếp tục cập nhật?", Type: Common.Message.Type.Confirm,
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Update_Time,
                                            data: { mID: obj.TOMasterID, ETD: Common.Date.FromJson(res.ETD), ETA: Common.Date.FromJson(res.ETA), dataContainer: res.ListCOContainer },
                                            success: function (res) {
                                                Common.Services.Error(res, function (res) {
                                                    $scope.ChangeData = true;
                                                    $rootScope.IsLoading = false;
                                                    $rootScope.Message({ Msg: 'Thành công!' });
                                                    $scope.veh_Grid.dataSource.read();
                                                }, function () {
                                                    $rootScope.IsLoading = false;
                                                })
                                            }
                                        })
                                    }
                                })
                            } else {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: "Xác nhận cập nhật thời gian?", Type: Common.Message.Type.Confirm,
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Update_Time,
                                            data: { mID: obj.TOMasterID, ETD: Common.Date.FromJson(res.ETD), ETA: Common.Date.FromJson(res.ETA), dataContainer: res.ListCOContainer },
                                            success: function (res) {
                                                Common.Services.Error(res, function (res) {
                                                    $scope.ChangeData = true;
                                                    $rootScope.IsLoading = false;
                                                    $rootScope.Message({ Msg: 'Thành công!' });
                                                    $scope.veh_Grid.dataSource.read();
                                                }, function () {
                                                    $rootScope.IsLoading = false;
                                                })
                                            }
                                        })
                                    }
                                })
                            }
                        })
                    }
                })
                _refreshScheduler(scheduler);
            } else {
                $rootScope.Message({ Msg: "Thao tác không hợp lệ!", Type: Common.Message.Type.Alert });
                _refreshScheduler(scheduler);
                $rootScope.IsLoading = false;
            }
        }
    }

    $scope.LoadDataTimeLine = function (callback) {
        Common.Log("LoadDataTimeLine");
        $scope.DateTimeRequest = { fDate: $scope.DateRequest.fDate.setHours(0, 0, 0, 0, 0), tDate: $scope.DateRequest.tDate.setHours(0, 0, 0, 0, 0) };
        var daysOfView = ($scope.DateTimeRequest.tDate - $scope.DateTimeRequest.fDate) / (24 * 60 * 60 * 1000) + 1;
        $scope.timelineVeh.setOptions({ daysOfView: daysOfView });
        $scope.timelineVeh.date($scope.DateRequest.fDate);
        $scope.TimeLineLoading = true;

        $scope.LastTop = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop();
        $scope.LastLeft = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollLeft();

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONDock_RegisterTimeline",
            data: {
                fDate: $scope.DateRequest.fDate,
                tDate: $scope.DateRequest.tDate,
                lst: $scope.VehicleResource,
                vendorID: 0,
            },
            success: function (res) {
                Common.Data.Each(res, function (o) {
                    o.field = o.VehicleID;
                })
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: res,
                    filter: [],
                    schema: {
                        model: {
                            id: "id",
                            fields: {
                                id: { from: "ID", type: "number" },
                                title: { from: "Title" },
                                start: { type: "date", from: "DateComeModified" },
                                end: { type: "date", from: "DateComeModifiedEnd" },
                                field: { from: "field" }
                            }
                        }
                    }
                });
                try {
                    $scope.timelineVeh.setDataSource(dataSource);
                    $timeout(function () {
                        $scope.timelineVeh.element.find('.k-nav-today').trigger('click');
                    }, 1)
                } catch (e) {
                    $timeout(function () {
                        $scope.timelineVeh.element.find('.k-nav-today').trigger('click');
                    }, 1)
                }

                if (callback) {
                    callback();
                }
            }
        })
    }

    $scope.Init_TimeLine_Veh_Times = function (type) {
        if (type == 1) {
            $('#timeline-vehicle-grid .k-grid-content').off('scroll');
            $('#timeline-vehicle-grid .k-grid-content').on('scroll', function (e, o) {
                var currentLeft = $(this).scrollLeft();
                if (currentLeft != $scope.TimesLeft) {
                    $scope.TimesLeft = currentLeft;
                    $("#timeline-vehicle-grid .k-grid-header-wrap").scrollLeft(currentLeft);
                } else {
                    if (o > 0 && e.isTrigger) {
                        $scope.IsEventTrigger = false;
                        if ($(this).scrollTop() != o)
                            $(this).scrollTop(o);
                    } else if ($scope.IsEventTrigger) {
                        if ($('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop() != $(this).scrollTop())
                            $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').trigger('scroll', $(this).scrollTop());
                    } else
                        $scope.IsEventTrigger = true
                }
            });

            $('#timeline-vehicle-grid .k-grid-content .k-i-collapse').off('click');
            $('#timeline-vehicle-grid .k-grid-content .k-i-collapse').on('click', function (e) {
                var ele = e.currentTarget.closest('tr'), nex = ele.nextElementSibling, chr = $(ele).parent().children(),
                che = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content'),
                idx = chr.index(nex);
                if ($(e.target).is('.k-i-collapse')) {
                    while (nex != null && !$(nex).is('.k-grouping-row')) {
                        if (idx > 0) {
                            $(che).find('tr:eq(' + idx + ')').hide();
                        }
                        nex = nex.nextElementSibling;
                        idx++;
                    }
                } else {
                    while (nex != null && !$(nex).is('.k-grouping-row')) {
                        if (idx > 0) {
                            $(che).find('tr:eq(' + idx + ')').show();
                        }
                        nex = nex.nextElementSibling;
                        idx++;
                    }
                }
                $scope.TimeLineRefreshTrigger = false;
                $scope.timelineVeh.view($scope.timelineVeh.view().name);
            });
        } else {
            $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').off('scroll');
            $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').on('scroll', function (e, o) {
                var currentLeft = $(this).scrollLeft();
                if (currentLeft != $scope.ContentLeft) {
                    $scope.ContentLeft = currentLeft;
                    $(".cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(0) > td:eq(1) .k-scheduler-header .k-scheduler-header-wrap").scrollLeft(currentLeft);
                    if ($scope.SameDate) {
                        $scope.ContentConLeft = currentLeft;
                        $(".cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").scrollLeft($scope.ContentConLeft);
                        $(".cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(0) > td:eq(1) .k-scheduler-header .k-scheduler-header-wrap").scrollLeft($scope.ContentConLeft);
                    }
                } else {
                    if (o > 0 && e.isTrigger) {
                        $scope.IsEventTrigger = false;
                        if ($(this).scrollTop() != o)
                            $(this).scrollTop(o);
                    } else if ($scope.IsEventTrigger) {
                        if ($('#timeline-vehicle-grid .k-grid-content').scrollTop() != $(this).scrollTop())
                            $('#timeline-vehicle-grid .k-grid-content').trigger('scroll', $(this).scrollTop());
                    } else
                        $scope.IsEventTrigger = true
                }
            });

            if ($scope.TimeLineRefreshTrigger) {
                $scope.IsEventTrigger = false;
                $scope.TimeLineRefreshTrigger = false;
                if ($('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop() != $('#timeline-vehicle-grid .k-grid-content').scrollTop())
                    $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').trigger('scroll', $('#timeline-vehicle-grid .k-grid-content').scrollTop());
                var flag = false;
                angular.forEach($('#timeline-vehicle-grid .k-grid-content tr'), function (tr, idx) {
                    if (!$(tr).is(":visible")) {
                        flag = true;
                        $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').find('tr:eq(' + idx + ')').hide();
                    }
                })
                if (flag) {
                    $scope.timelineVeh.view($scope.timelineVeh.view().name);
                }
            }
        }
    }

    $timeout(function () {
        $($scope.veh_Grid.element).find(".k-grid-pager").insertBefore($($scope.veh_Grid.element).children(".k-grid-header"));
        $($scope.veh_Grid.element).find(".k-grid-pager").prepend("<span class='grid-title'>Phương tiện</span>");

        $($scope.timelineVeh.element).find('.k-scheduler-toolbar').children().hide();
        $($scope.timelineVeh.element).find('.k-scheduler-toolbar').append('<div class="cus-form thin"><div class="form-header"><div class="left">'
            + '</div><div class="right">'
            + '<input class="cus-datepicker" style="width:100px;" focus-k-datepicker kendo-date-picker k-ng-model="DateRequest.fDate" k-options="DateDMY" /><span style="color:#fff"> - </span><input class="cus-datepicker" style="width:100px;" focus-k-datepicker kendo-date-picker k-ng-model="DateRequest.tDate" k-options="DateDMY" />'
            + '<a style="color:#fff;" class="k-button" ng-click="DateRequestChange_Click($event)"><i class="fa fa-refresh"></i><span class="tooltip">Làm mới</span></a>'
            + '</div></div></div>')
        $compile($($scope.timelineVeh.element).find('.k-scheduler-toolbar'))($scope);

        $scope.veh_Grid.dataSource.read();
    }, 100)

    $scope.ItemT1 = {};
    $scope.TimelineEvent_Click = function (e, item) {
        e.preventDefault();
        
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONDock_GetInfo",
            data: {
                id:item.ID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemT1 = res;
                $scope.TimlinePopup.center().open();
            }
        })
    }

    $scope.TimelineAccept = function (e) {
        e.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONDock_DockTimelineAccept",
            data: {
                id: item.ID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.TimlinePopup.close();
                $scope.LoadDataTimeLine();
            }
        })
    }
    //#endregion


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