/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_COViewOnMapV5 = {
    URL: {
        Order_List: 'OPSCO_TimeLine_Order_List',
        Vehicle_List: 'OPSCO_TimeLine_Vehicle_List',
        Schedule_Data: 'OPSCO_TimeLine_Schedule_Data',

        ToOPS: 'OPSCO_MAP_ToOPS',
        Delete: 'OPSCO_MAP_Delete',
        ToMon: 'OPSCO_MAP_ToMON',

        TripByID: "OPSCO_MAP_TripByID",
        Driver_List: 'OPSCO_MAP_Driver_List',
        Vendor_List: 'OPSCO_MAP_Vendor_List',
        Romooc_List: 'OPSCO_MAP_Romooc_List',
        Tractor_List: 'OPSCO_MAP_Tractor_List',
        RomoocWaiting_List: 'OPSCO_TimeLine_RomoocWait_List',

        Seaport_List: 'OPSCO_MAP_Seaport_List',
        Carrier_List: 'OPSCO_MAP_Carrier_List',
        Customer_List: 'OPSCO_MAP_Customer_List',
        Service_List: 'OPSCO_MAP_ServiceOfOrder_List',
        OrderFilter_List: 'OPSCO_TimeLine_OrderFilter_List',
        ContainerToMaster_List: 'OPSCO_TimeLine_OrderToTOMaster_List',

        CheckVehicleAvailable: 'OPSCO_MAP_CheckVehicleAvailable',

        TimeLine_TOMaster_Save: "OPSCO_MAP_TimeLine_Create_Item",
        TimeLine_TOMaster_OfferTime: 'OPSCO_MAP_Schedule_NewTime_Offer',
        TimeLine_TOMaster_OfferVehicle: 'OPSCO_MAP_Schedule_TOMaster_Vehicle_Offer',
        TimeLine_TOMaster_Container_List: 'OPSCO_TimeLine_COTOContainer_ByTrip_List',
        TimeLine_TOMaster_Update_Time: 'OPSCO_MAP_Schedule_TOMaster_Change_Time',
        TimeLine_TOMaster_Update_Vehicle: 'OPSCO_MAP_Schedule_TOMaster_Change_Vehicle',

        TimeLine_TOMaster_Data: 'OPSCO_MAP_Info_Schedule_Data',
        TimeLine_TOMaster_Check: 'OPSCO_MAP_Schedule_AddTOContainer_Offer',
        TimeLine_TOMaster_AddContainer: 'OPSCO_MAP_Schedule_AddTOContainer',
        TimeLine_TOMaster_DelContainer: 'OPSCO_TimeLine_COTOContainer_Remove',
        TimeLine_TOMaster_Container_ChangeTimeOffer: 'OPSCO_TimeLine_Event_ChangeTime_Offer',
        TimeLine_TOMaster_Update_Driver: 'OPSCO_MAP_2View_Master_ChangeDriver',

        TripTooltipByID: "OPSCO_TimeLine_TOMaster_ByID",
        TripInfinityTooltipByID: "OPSCO_TimeLine_DataContainerLocal_ByTOMasterID",
        TimeLine_Order_OwnerPlanning_Update: 'OPSCO_TimeLine_Order_OwnerPlanning_Update',

        TimeLine_Vehicle_OnMap_List: 'OPSCO_TimeLine_Vehicle_OnMap_List',
        TimeLine_Vehicle_OnMap_GPSUpdate: 'OPSCO_TimeLine_Vehicle_OnMap_GPSUpdate',
        TimeLine_ORDLocation_OnMap_List: 'OPSCO_TimeLine_ORDLocation_OnMap_List',


        TimeLine_Vehicle_Schedule_Data: 'OPSCO_TimeLine_Vehicle_Schedule_Data'

    },
    DAYOFWEEK: [
        "Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy",
    ],
    DATATOMASTER: [],
    DATATOMASTERINFINITY: []
}

angular.module('myapp').controller('OPSAppointment_COViewOnMapV5Ctrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COViewOnMapV5Ctrl');

    angular.element('.mainform .mainmenu').removeClass('fullscreen');
    $rootScope.Loading.Change("Khởi tạo...", 20);
    $rootScope.Loading.Show();

    $scope.IsFullScreen = false;
    $rootScope.IsLoading = false;

    $scope.SameDate = true;
    $scope.OrderLoadType = 1;
    $scope.RomoocLoadType = 1;
    $scope.VehicleLoadType = 1;
    $scope.ShowOrder = false;
    $scope.TimeLineLoading = true;
    $scope.TimeLineConLoading = true;
    $scope.VehicleLoadTypeName = "Hiện xe chờ";
    $scope.OrderLoadTypeName = "Đơn đã phân";
    $scope.IsOwnerPlanning = true;

    $scope.MaxDate = 14;
    $scope.Color = { None: '#f6fafe', Error: '#fc0000', Success: '#31B6FC' }
    $scope.DateRequest = { fDate: new Date().addDays(-1), tDate: new Date().addDays(1) }
    $scope.DateTimeRequest = { fDate: $scope.DateRequest.fDate.setHours(0, 0, 0, 0, 0), tDate: $scope.DateRequest.tDate.setHours(0, 0, 0, 0, 0) }

    //#region Layout

    $scope.verSplitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: true, size: '50%', min: '200px' },
            { collapsible: true, resizable: true, min: '200px', collapsed: true }
        ],
        resize: function (e) {
        },
        collapse: function (e) {
            $scope.ShowOrder = false;
        },
        expand: function (e) {
            $scope.ShowOrder = true;
        }
    }

    $scope.vehSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: true, size: '450px', min: '300px' },
            { collapsible: false, resizable: true, min: '600px' }
        ],
        resize: function (e) {
            if ($scope.conSplitter != null && $scope.conSplitter.size($scope.conSplitter.element.children(":first")) != this.size(this.element.children(":first")))
                $scope.conSplitter.size($scope.conSplitter.element.children(":first"), this.size(this.element.children(":first")));
        }
    }

    $scope.conSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: true, size: '450px', min: '300px' },
            { collapsible: false, resizable: true, min: '600px' }
        ],
        resize: function (e) {
            if ($scope.vehSplitter != null && $scope.vehSplitter.size($scope.vehSplitter.element.children(":first")) != this.size(this.element.children(":first")))
                $scope.vehSplitter.size($scope.vehSplitter.element.children(":first"), this.size(this.element.children(":first")));
        }
    }

    $scope.ViewSize_Click = function ($event) {
        $event.preventDefault();
        if ($('#2view').is('.smsize')) {
            $('#2view').removeClass('smsize');
            $scope.LastTop = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop() / 24 * 39;
            $scope.LastLeft = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollLeft();
        }
        else {
            $('#2view').addClass('smsize');
            $scope.LastTop = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop() / 39 * 24;
            $scope.LastLeft = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollLeft();
        }
        $rootScope.Loading.Show();
        $rootScope.Loading.Change("Làm mới hiển thị...", 20);
        $scope.TimeLineLoading = true;
        $scope.TimeLineConLoading = true;

        $timeout(function () {
            $rootScope.Loading.Change("Làm mới hiển thị...", 30);
            $scope.timelineVeh.element.find('.k-nav-today').trigger('click');
            $scope.timelineCon.element.find('.k-nav-today').trigger('click');
            $timeout(function () {
                $rootScope.Loading.Change("Làm mới hiển thị...", 80);
                $rootScope.Loading.Hide();
            }, 1500)
        }, 10)
    }

    $scope.ShowOrder_Click = function ($event) {
        $event.preventDefault();
        $scope.ShowOrder = !$scope.ShowOrder;
        var pane = $($scope.verSplitter.element.children(':last'));
        if ($scope.ShowOrder != true) $scope.verSplitter.collapse(pane);
        else $scope.verSplitter.expand(pane);

    }

    $scope.TimeLineScrollType_Click = function ($event) {
        $event.preventDefault();
        $scope.SameDate = !$scope.SameDate;
        if ($scope.SameDate) {
            $scope.ContentLeft = 0;
            $scope.ContentConLeft = 0;
            $($event.currentTarget).addClass('active');
            $(".cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").animate({ scrollLeft: 0 });
            $(".cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").animate({ scrollLeft: 0 });
        } else {
            $($event.currentTarget).removeClass('active');
        }
    }

    $scope.TimeLineOrderType_Click = function ($event) {
        $event.preventDefault();
        if ($scope.OrderLoadType == 1) {
            $scope.OrderLoadType = 2;
            $scope.OrderLoadTypeName = "Đơn chưa phân";
            $($event.currentTarget).addClass('active');
            $scope.Loading.Show();
            $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
            $scope.con_Grid.dataSource.read();
        }
        else {
            $scope.OrderLoadType = 1;
            $scope.OrderLoadTypeName = "Đơn đã phân";
            $($event.currentTarget).removeClass('active');
            $scope.Loading.Show();
            $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
            $scope.con_Grid.dataSource.read();
        }
    }

    $scope.TimeLineRomoocType_Click = function ($event) {
        $event.preventDefault();

        if ($scope.RomoocLoadType == 1) {
            $scope.RomoocLoadType = 2;
            $($event.currentTarget).addClass('active');
            $scope.LoadDataTimeLine(function () {
                $timeout(function () {
                    $rootScope.Loading.Change("Dữ liệu chuyến...", 80);
                    $rootScope.Loading.Hide();
                }, 1000)
            });
        }
        else {
            $scope.RomoocLoadType = 1;
            $($event.currentTarget).removeClass('active');
            $scope.LoadDataTimeLine(function () {
                $timeout(function () {
                    $rootScope.Loading.Change("Dữ liệu chuyến...", 80);
                    $rootScope.Loading.Hide();
                }, 1000)
            });
        }
    }

    $scope.TimeLineVehicleType_Click = function ($event) {
        $event.preventDefault();
        if ($scope.VehicleLoadType == 1) {
            $scope.VehicleLoadType = 2;
            $scope.VehicleLoadTypeName = "Ẩn xe chờ";
            $($event.currentTarget).addClass('active');
            $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
            $rootScope.Loading.Show();
            $scope.veh_Grid.dataSource.read();
        }
        else {
            $scope.VehicleLoadType = 1;
            $scope.VehicleLoadTypeName = "Hiện xe chờ";
            $($event.currentTarget).removeClass('active');
            $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
            $rootScope.Loading.Show();
            $scope.veh_Grid.dataSource.read();
        }
    }

    var CUSTOMVIEW = kendo.ui.TimelineMonthView.extend({
        nextDate: function () {
            return kendo.date.nextDay(this.startDate());
        },
        options: {
            columnWidth: 60, majorTick: 60, daysOfView: 14, selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
            dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
            majorTimeHeaderTemplate: kendo.template("<strong>#=Math.round(kendo.toString(date, 'HH'))#:00</strong>")
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

    $scope.VehicleResource = [];

    $scope.veh_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.Vehicle_List,
            pageSize: 20,
            readparam: function () {
                $scope.RomoocTempItem = [];
                $scope.VehicleRomoocData = [];
                $scope.TimeLineLoading = true;
                return {
                    fDate: $scope.DateRequest.fDate,
                    tDate: $scope.DateRequest.tDate,
                    typeOfView: $scope.VehicleLoadType
                }
            },
            group: [{ field: 'VehicleNo' }],
            model: {
                id: 'Text',
                fields: {
                    Text: { type: 'string' },
                    MaxWeight: { type: 'number' },
                    Lat: { type: 'number' },
                    Lng: { type: 'number' },
                    MaxCapacity: { type: 'number' }
                }
            }
        }),
        pageable: { info: true, numeric: false, buttonCount: 0, input: true, messages: { of: "/{0}", page: "", display: "{0}-{1} / {2}" } },
        height: '100%', groupable: false, sortable: true, columnMenu: false, resizable: false, selectable: true, filterable: { mode: 'row' }, reorderable: false, autoBind: false,
        change: function (e) {
            var obj = this.dataItem(this.select()), data = $scope.timelineVeh.dataSource.data();
            if (Common.HasValue(obj)) {
                var field = obj.id;
                if (this.select().is('.k-grouping-row')) {
                    field = "1" + "-" + obj.VehicleID + "--1"; //xx
                }
                var ds = new kendo.data.DataSource({
                    data: data,
                    filter: [{
                        field: 'field', operator: 'eq', value: field
                    }, {
                        field: 'StatusOfEvent', operator: 'gte', value: 2
                    }],
                    sort: [{ field: 'end', dir: "desc" }]
                })
                ds.fetch(function () {
                    var view = ds.view();
                    if (view.length > 0) {
                        var o = view[0];
                        var div = $($scope.timelineVeh.element).find('.cus-event[data-eventid="' + o.id + '"]:eq(0)').closest('.k-event');
                        if (div.length > 0) {
                            var conLeft = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').width();
                            var valLeft = div.position().left;
                            if (valLeft < 0) {
                                conLeft = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content tbody').position().left;
                                valLeft = valLeft - conLeft - 200;
                                valLeft = valLeft > 0 ? valLeft : 0;
                                $scope.ContentLeft = valLeft;
                                $(".cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").animate({ scrollLeft: valLeft });
                            } else if (valLeft > conLeft) {
                                valLeft = valLeft - 200 + $(".cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").scrollLeft();
                                $scope.ContentLeft = valLeft;
                                $(".cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").animate({ scrollLeft: valLeft });
                            }
                        }
                    }
                });
            }
        },
        dataBound: function () {
            var grid = this;
            $(grid.tbody).find('tr.k-grouping-row').each(function () {
                var val = $(this).find('.myval').data('value');
                $(this).find('td:eq(0)').attr('colspan', 1);
                $(this).append("<td><a class='k-button small-button' href='#' ng-click='Vehicle_Filter_Click($event,veh_Grid,1)' style='position: relative;top: -2px;'><i class='fa fa-filter'></i></a>" + val.VehicleNo + "</td>");
                $(this).append("<td></td>");
                $(this).append("<td>" + val.VehicleMaxWeight + "</td>");
                $(this).append("<td></td>");
                $(this).append("<td></td>");
                $(this).append("<td>" + val.VehicleLocationName + "</td>");
                if (val.VehicleLat == null)
                    val.VehicleLat = "";
                if (val.VehicleLng == null)
                    val.VehicleLng = "";
                $(this).append("<td>" + val.VehicleLat + "</td>");
                $(this).append("<td>" + val.VehicleLng + "</td>");
                $compile(this)($scope);
            })

            var data = [];
            $scope.VehicleResource = [];
            angular.forEach(grid.dataSource.view(), function (g) {
                var o = g.items[0];
                $scope.VehicleResource.push(o.VehicleID + "_" + "-1");
                data.push({ value: "1" + "-" + o.VehicleID + "--1", text: o.Text, VehicleID: o.VehicleID, RomoocID: -1, VehicleNo: o.VehicleNo, RomoocNo: '[Chờ nhập]' })
                angular.forEach(g.items, function (p) {
                    $scope.VehicleResource.push(p.VehicleID + "_" + p.RomoocID);
                    data.push({ value: "2" + "-" + p.VehicleID + "-" + p.RomoocID, text: o.Text, VehicleID: p.VehicleID, RomoocID: p.RomoocID, VehicleNo: p.VehicleNo, RomoocNo: p.RomoocNo })
                })
            })
            if (data.length == 0) {
                data.push({ value: '1--1--1', text: "DL trống", VehicleID: -1, RomoocID: -1, VehicleNo: '[Chờ nhập]', RomoocNo: '[Chờ nhập]' });
            }
            $scope.timelineVeh.resources[0].dataSource.data(data);
            $timeout(function () {
                $scope.LoadDataTimeLine(function () {
                    $rootScope.Loading.Change("Dữ liệu đơn hàng...", 60);
                    $scope.con_Grid.dataSource.read();
                });
                $rootScope.Loading.Change("Dữ liệu chuyến...", 30);
            })
            $timeout(function () {
                $scope.Init_TimeLine_Veh_Times(1);
            }, 1000)
        },
        columns: [
            {
                field: 'VehicleNo', width: 100, title: 'Số xe',
                groupHeaderTemplate: function (e) { return "<span class='myval' data-value='" + JSON.stringify(e.aggregates.parent().items[0], Common.JSON.QuoteReplacer) + "'>" },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100, title: 'Số romooc',
                template: function (e) {
                    if (e.RomoocID == -1 || e.IsChange) return "<a href='#' ng-click='Vehicle_Grid_Change_Romooc($event,veh_Grid,timeline_romooc_win)'>" + e.RomoocNo + "</a>";
                    else return "<a class='k-button small-button' href='#' ng-click='Vehicle_Filter_Click($event,veh_Grid,2)' style='position: relative;top: -2px;'><i class='fa fa-filter'></i></a>" + e.RomoocNo;
                }, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'MaxWeight', width: 100, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'GroupOfRomoocCode', width: 150, title: 'Mã loại romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfRomoocName', width: 150, title: 'Tên loại romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', width: 150, title: 'Vị trí', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Lat', width: 120, title: 'Vĩ độ', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Lng', width: 120, title: 'Kinh độ', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.timelineVeh_Options = {
        date: $scope.DateRequest.fDate, footer: false, snap: false, autoBind: false,
        eventHeight: 20, majorTick: 60, height: '100%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: true, resize: true, update: false },
        views: [
            {
                type: CUSTOMVIEW, title: 'Custom',
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
                    Common.Data.Each(scheduler.items(), function (o) {
                        $(o).addClass('to-event');
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case 1:
                                        if (i.StatusOfTimeSheet <= 2) {
                                            if (i.StatusOfEvent == 1) {
                                                //$(o).addClass('approved');
                                            } else if (i.StatusOfEvent == 2) {
                                                $(o).find('.k-resize-handle').hide();
                                                //$(o).addClass('tendered');
                                            } else if (i.StatusOfEvent == 3) {
                                                $(o).find('.k-resize-handle').hide();
                                                //$(o).addClass('recieved');
                                            } else if (i.StatusOfEvent == 11) {
                                                //$(o).addClass('tenderable');
                                            } else {
                                                //$(o).addClass('undefined');
                                                $(o).find('.k-resize-handle').hide();
                                            }
                                        } else {
                                            $(o).addClass('empty');
                                            $(o).find('.k-resize-handle').hide();
                                        }
                                        break;
                                    case 2:
                                        //$(o).addClass('maintainance');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 3:
                                        //$(o).addClass('registry');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 4:
                                        //$(o).addClass('repair');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        })
                    })
                    $timeout(function () {
                        $scope.Init_TimeLine_Con_DragDrop();
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
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || e.event.StatusOfTimeSheet > 2 || e.event.TOMasterID < 1) {
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
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || e.event.StatusOfTimeSheet > 2 || e.event.TOMasterID < 1) {
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
                    if (o.id == obj.id && o.TypeOfGroupID == obj.TypeOfGroupID && o.StatusOfTimeSheet == obj.StatusOfTimeSheet && obj.VehicleID == o.VehicleID) {
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
                                                    $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                                    $rootScope.Loading.Show();
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
                                                    $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                                    $rootScope.Loading.Show();
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

    $scope.TimeLineEventEdit = {};
    $scope.TimeLineEventDragDrop = false;
    $scope.TimeLineVehicleEventItem = { IsLoaded: false, Item: { ListContainer: [{}] }, StatusOfTimeSheet: 0 };
    $scope.TimeLineVehicleEventStyle = { 'display': 'none', 'top': 0, 'left': 0 }
    $scope.TimeLineVehicleInfinityEventItem = { ID: -1, IsLoaded: false, Item: { ListContainer: [] } };
    $scope.TimeLineVehicleInfinityEventStyle = { 'display': 'none', 'top': 0, 'left': 0 }

    //Move tooltip => body
    $('body > #timeline_vehicle_tooltip').remove();
    $('#timeline_vehicle_tooltip').detach().appendTo('body');
    $('body > #timeline_vehicle_infinity_tooltip').remove();
    $('#timeline_vehicle_infinity_tooltip').detach().appendTo('body');

    $scope.ShowTimeLineTooltip = function ($event, cid, uid, type) {
        if ($scope.TimeLineEventDragDrop == false && type <= 6 && cid > 0) {
            var off = $($event.currentTarget).closest('.cus-event').offset();
            $scope.TimeLineVehicleEventItem = { IsLoaded: false, Item: { ListContainer: [{}] }, StatusOfTimeSheet: type };
            $scope.TimeLineVehicleEventStyle = { 'display': '', 'top': off.top - 183, 'left': $($event.currentTarget).closest('.cus-event').width() / 2 + off.left - 211 };

            if (Common.HasValue(_OPSAppointment_COViewOnMapV5.DATATOMASTER[cid + "_" + uid])) {
                $scope.TimeLineVehicleEventItem = {
                    IsLoaded: true,
                    StatusOfTimeSheet: type,
                    Item: _OPSAppointment_COViewOnMapV5.DATATOMASTER[cid + "_" + uid]
                };
                if ($scope.TimeLineVehicleEventItem.StatusOfTimeSheet <= 2) {
                    if ($scope.TimeLineVehicleEventItem.Item.ListContainer.length == 1) {
                        $('#timeline_vehicle_tooltip').height(165);
                    } else {
                        $('#timeline_vehicle_tooltip').height(185);
                        $scope.TimeLineVehicleEventStyle.top = off.top - 203;
                    }
                } else {
                    $('#timeline_vehicle_tooltip').height(145);
                    $scope.TimeLineVehicleEventStyle.top = off.top - 163;
                }
            } else {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV5.URL.TripTooltipByID,
                    data: { masterID: uid, containerID: cid },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            _OPSAppointment_COViewOnMapV5.DATATOMASTER[cid + "_" + uid] = res;
                            if (res.ListContainer != null && res.ListContainer == null)
                                _OPSAppointment_COViewOnMapV5.DATATOMASTER[cid + "_" + uid].ListContainer = [];
                            $scope.TimeLineVehicleEventItem = {
                                IsLoaded: true, StatusOfTimeSheet: type,
                                Item: _OPSAppointment_COViewOnMapV5.DATATOMASTER[cid + "_" + uid]
                            };
                            if ($scope.TimeLineVehicleEventItem.StatusOfTimeSheet <= 2) {
                                if ($scope.TimeLineVehicleEventItem.Item.ListContainer.length == 1) {
                                    $('#timeline_vehicle_tooltip').height(165);
                                } else {
                                    $('#timeline_vehicle_tooltip').height(185);
                                    $scope.TimeLineVehicleEventStyle.top = off.top - 203;
                                }
                            } else {
                                $('#timeline_vehicle_tooltip').height(145);
                                $scope.TimeLineVehicleEventStyle.top = off.top - 163;
                            }
                        }
                    }
                })
            }
        }
    }

    $scope.HideTimeLineTooltip = function ($event) {
        $scope.TimeLineVehicleEventItem = { IsLoaded: false, Item: { ListContainer: [{}] }, StatusOfTimeSheet: 0 };
        $scope.TimeLineVehicleEventStyle = { 'display': 'none', 'top': 0, 'left': 0 };
    }

    $scope.ShowTimeLineInfinityTooltip = function ($event, cid, uid) {
        var off = $($event.currentTarget).offset();
        $scope.TimeLineVehicleInfinityEventItem = { ID: cid, IsLoaded: false, Item: { ListContainer: [] } };
        $scope.TimeLineVehicleInfinityEventStyle = { 'display': '', 'top': -1000, 'left': $($event.currentTarget).width() / 2 + off.left - 236 };

        if (Common.HasValue(_OPSAppointment_COViewOnMapV5.DATATOMASTERINFINITY[uid])) {
            var l = _OPSAppointment_COViewOnMapV5.DATATOMASTERINFINITY[uid].ListContainer.length;
            if (l == 0) l = 1;
            var h = 50 + l * 20;
            $('#timeline_vehicle_infinity_tooltip').height(h);
            $scope.TimeLineVehicleInfinityEventStyle.top = off.top - h - 18;
            $scope.TimeLineVehicleInfinityEventItem = {
                ID: cid,
                IsLoaded: true,
                Item: _OPSAppointment_COViewOnMapV5.DATATOMASTERINFINITY[uid]
            };
        } else {
            $scope.TimeLineVehicleInfinityEventStyle.top = off.top - 168;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV5.URL.TripInfinityTooltipByID,
                data: { masterID: uid },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        _OPSAppointment_COViewOnMapV5.DATATOMASTERINFINITY[uid] = res;
                        if (res.ListContainer != null && res.ListContainer == null)
                            _OPSAppointment_COViewOnMapV5.DATATOMASTERINFINITY[uid].ListContainer = [];
                        var l = _OPSAppointment_COViewOnMapV5.DATATOMASTERINFINITY[uid].ListContainer.length;
                        if (l == 0) l = 1;
                        var h = 50 + l * 20;
                        $('#timeline_vehicle_infinity_tooltip').height(h);
                        $scope.TimeLineVehicleInfinityEventStyle.top = off.top - h - 18;
                        $scope.TimeLineVehicleInfinityEventItem = {
                            ID: cid,
                            IsLoaded: true,
                            Item: _OPSAppointment_COViewOnMapV5.DATATOMASTERINFINITY[uid]
                        };
                    }
                }
            })
        }
    }

    $scope.HideTimeLineInfinityTooltip = function ($event) {
        $scope.TimeLineVehicleInfinityEventItem = { ID: -1, IsLoaded: false, Item: { ListContainer: [] } };
        $scope.TimeLineVehicleInfinityEventStyle = { 'display': 'none', 'top': 0, 'left': 0 };
        $('#timeline_vehicle_infinity_tooltip').height(150);
    }

    $scope.LastTop = 0;
    $scope.LastLeft = 0;
    $scope.LoadDataTimeLine = function (callback) {
        $rootScope.Loading.Show();
        $rootScope.Loading.Change("Dữ liệu chuyến...", 30);
        Common.Log("LoadDataTimeLine");
        $scope.DateTimeRequest = { fDate: $scope.DateRequest.fDate.setHours(0, 0, 0, 0, 0), tDate: $scope.DateRequest.tDate.setHours(0, 0, 0, 0, 0) };
        var daysOfView = ($scope.DateTimeRequest.tDate - $scope.DateTimeRequest.fDate) / (24 * 60 * 60 * 1000) + 1;
        $scope.timelineVeh.setOptions({ daysOfView: daysOfView });
        $scope.timelineCon.setOptions({ daysOfView: daysOfView });
        $scope.timelineVeh.date($scope.DateRequest.fDate);
        $scope.timelineCon.date($scope.DateRequest.fDate);
        $scope.TimeLineLoading = true;

        $scope.LastTop = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop();
        $scope.LastLeft = $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollLeft();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.Schedule_Data,
            data: {
                fDate: $scope.DateRequest.fDate,
                tDate: $scope.DateRequest.tDate,
                dataRes: $scope.VehicleResource,
                typeOfView: $scope.RomoocLoadType
            },
            success: function (res) {
                Common.Data.Each(res.DataSources, function (o) {
                    var i = o.GroupID == o.VehicleID && o.TypeOfGroupID == 1 ? -1 : o.GroupID;
                    o.field = o.TypeOfGroupID + "-" + o.VehicleID + "-" + i;
                })
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: res.DataSources,
                    filter: $scope.TimeLineVehicleFilter,
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

                $rootScope.Loading.Change("Dữ liệu chuyến...", 50);
                if (callback)
                    callback();
            }
        })
    }

    $scope.$watch("TimeLineLoading", function () {
        if ($scope.TimeLineLoading == false) {
            if ($scope.LastLeft != $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollLeft())
                $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').animate({ scrollLeft: $scope.LastLeft }, 100);
            if ($scope.LastTop != $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop())
                $('.cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').animate({ scrollTop: $scope.LastTop }, 100);
        }
    })

    $scope.DateRequestChange_Click = function ($event) {
        $event.preventDefault();
        if ($scope.DateRequest.tDate < $scope.DateRequest.fDate || !angular.isDate($scope.DateRequest.fDate) || !angular.isDate($scope.DateRequest.tDate))
            $rootScope.Message({ Msg: "Thời gian không hợp lệ!", Type: Common.Message.Type.Alert });
        else if ($scope.DateRequest.tDate - $scope.DateRequest.fDate > $scope.MaxDate * 24 * 60 * 60 * 1000) {
            $rootScope.Message({ Msg: "Thời gian không hợp lệ! Vui lòng không chọn quá 2 tuần!", Type: Common.Message.Type.Alert });
        }
        else {
            $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
            $rootScope.Loading.Show();
            $scope.veh_Grid.dataSource.read();
        }
    }

    $scope.TimesLeft = 0;
    $scope.ContentLeft = 0;
    $scope.IsEventTrigger = false;
    $scope.TimeLineRefreshTrigger = true;
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

    $scope.con_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.Order_List,
            pageSize: 20,
            readparam: function () {
                $scope.TimeLineConLoading = true;
                $($scope.con_Grid.thead).find('input[type="checkbox"]').prop('checked', false);
                return {
                    typeOfOrder: $scope.OrderLoadType,
                    isOwnerPlanning: $scope.IsOwnerPlanning,
                    fDate: $scope.DateRequest.fDate,
                    tDate: $scope.DateRequest.tDate,
                    dataCus: $scope.TimeLineDataCustomer,
                    dataService: $scope.TimeLineDataService,
                    dataCarrier: $scope.TimeLineDataCarrier,
                    dataSeaport: $scope.TimeLineDataSeaport
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    WarningTime: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false }
                }
            }
        }),
        pageable: { info: true, numeric: false, buttonCount: 0, input: true, messages: { of: "/{0}", page: "", display: "{0}-{1} / {2}" } },
        height: '100%', groupable: false, sortable: true, columnMenu: false, resizable: false, filterable: { mode: 'row' }, reorderable: false, autoBind: false, selectable: true,
        dataBound: function () {
            var grid = this;
            $rootScope.Loading.Change("Dữ liệu đơn hàng...", 70);
            $rootScope.Loading.Change("Dữ liệu chi tiết đơn hàng...", 75);
            var dataResource = [], data = [];
            angular.forEach(grid.dataSource.view(), function (o) {
                if (o.ListCOTOContainer != null && o.ListCOTOContainer != []) {
                    angular.forEach(o.ListCOTOContainer, function (c) {
                        if (Common.Date.FromJson(c.ETD) <= Common.Date.FromJson(c.ETA))
                            data.push({
                                ID: c.ID, Title: c.StatusOfContainerName, SortOrder: c.SortOrder,
                                TOMasterID: c.TOMasterID, TOMasterCode: c.TOMasterCode, StatusOfEvent: c.TOMasterIndex,
                                StartDate: Common.Date.FromJson(c.ETD), EndDate: Common.Date.FromJson(c.ETA),
                                field: "1" + "-" + o.OPSContainerID, ContainerID: o.ID, IsEmpty: c.COTOStatus == 1
                            });
                    });
                }
                dataResource.push({ value: "1" + "-" + o.OPSContainerID, text: o.ContainerNo, TOETD: o.TOETD, TOETA: o.TOETA })
            })
            if (dataResource.length == 0) {
                flag = true;
                dataResource.push({ value: '1--1', text: "DL trống" });
            }
            var dataSource = new kendo.data.SchedulerDataSource({
                data: data,
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
            $scope.timelineCon.resources[0].dataSource.data(dataResource);
            try {
                $scope.timelineCon.setDataSource(dataSource);
                $timeout(function () {
                    $scope.timelineCon.element.find('.k-nav-today').trigger('click');
                }, 100)
            } catch (e) {
                $timeout(function () {
                    $scope.timelineCon.element.find('.k-nav-today').trigger('click');
                }, 100)
            }
        },
        change: function (e) {
            var idx = this.tbody.children().index(this.select());
            if (idx > -1) {
                var td = $scope.timelineCon.element.find('.k-scheduler-content .k-scheduler-table tbody > tr:eq(' + idx + ') > td.container-time:eq(0)');
                if (td != null && td != []) {
                    var conLeft = $('.cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').width();
                    var valLeft = td.position().left;
                    if (valLeft < 0) {
                        conLeft = $('.cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content tbody').position().left;
                        valLeft = valLeft - conLeft - 200;
                        valLeft = valLeft > 0 ? valLeft : 0;
                        $scope.ContentConLeft = valLeft;
                        $(".cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").animate({ scrollLeft: valLeft });
                    } else if (valLeft > conLeft) {
                        valLeft = valLeft - 200 + $(".cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").scrollLeft();
                        $scope.ContentConLeft = valLeft;
                        $(".cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").animate({ scrollLeft: valLeft });
                    }
                }
            }
        },
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px', filterable: false, sortable: false, headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                headerTemplate: '<input type="checkbox" ng-click="conGridCheckAll($event,con_Grid)" />', template: '<input class="chkChoose" ng-disabled="dataitem.IsAllowChangeOwnerPlan" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="conGridCheck($event,con_Grid)" />',
            },
            { field: 'CreateTO', width: 40, title: ' ', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' }, template: '<a href="/" class="k-button small-button" ng-click="Container_Map_Click($event,dataItem,timeline_map_win)" title="Bản đồ"><i style="font-size:14px;color:rgba(111, 139, 169, 1);" class="fa fa-map-marker"></i></a>', filterable: false, sortable: false },
            { field: 'CustomerCode', width: 120, title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 150, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: '100px', title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '80px', title: 'Số seal 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '80px', title: 'Số seal 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: '100px', title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotCode', width: '100px', title: 'Điểm lấy rỗng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotReturnCode', width: '100px', title: 'Điểm trả rỗng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotReturnAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TripNo', width: 100, title: 'Số chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ShipNo', width: 100, title: 'Số tàu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ShipName', width: 100, title: 'Tên tàu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note0', width: 150, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 150, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 150, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined1', width: 100, title: 'Định nghĩa 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined2', width: 100, title: 'Định nghĩa 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined3', width: 100, title: 'Định nghĩa 3', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined4', width: 100, title: 'Định nghĩa 4', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined5', width: 100, title: 'Định nghĩa 5', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined6', width: 100, title: 'Định nghĩa 6', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.conGridCheck = function ($event, grid) {
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose'))
                $(tr).addClass('IsChoose');
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose'))
                $(tr).removeClass('IsChoose');
        }
    }

    $scope.conGridCheckAll = function ($event, grid) {
        if ($event.target.checked == true) {
            grid.items().each(function (tr) {
                var item = grid.dataItem(this);
                if (item.IsChoose != true && item.IsAllowChangeOwnerPlan) {
                    $(this).find('td input.chkChoose').prop('checked', true);
                    item.IsChoose = true;
                    if (!$(this).hasClass('IsChoose'))
                        $(this).addClass('IsChoose');
                }
            });
        }
        else {
            grid.items().each(function (tr) {
                var item = grid.dataItem(this);
                if (item.IsChoose == true && item.IsAllowChangeOwnerPlan) {
                    $(this).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                    if ($(this).hasClass('IsChoose'))
                        $(this).removeClass('IsChoose');
                }
            });
        }
    }

    $scope.timelineCon_Options = {
        date: $scope.DateRequest.fDate, footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '100%', messages: { today: "Hôm nay" }, editable: false,
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
        groupHeaderTemplate: kendo.template("<span  data-uid='#=value#' class='txtTimeLineContainer' style='cursor:pointer;'><strong>#=text#</strong></span><i class='fa fa-spinner fa-spin' style='color:rgb(248, 248, 248);padding: 0 2px;'></i>"),
        eventTemplate: $("#timeline-con-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            if (scheduler.date().getTime() != $scope.DateTimeRequest.fDate) {
                scheduler.date($scope.DateRequest.fDate);
            } else {
                $scope.Init_TimeLine_Con_Times();
                $timeout(function () {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                if (i.TOMasterID > 0) {
                                    switch (i.StatusOfEvent) {
                                        case 1:
                                            $(o).addClass('tenderable');
                                            break;
                                        case 2:
                                            $(o).addClass('tendered');
                                            break;
                                        case 3:
                                            $(o).addClass('recieved');
                                            break;
                                    }
                                }
                                else {
                                    $(o).addClass('approved');
                                }
                            }
                        })
                    })
                    angular.forEach(scheduler.element.find('.k-scheduler-times:eq(1) .k-scheduler-table tbody > tr'), function (o, i) {
                        var g, cells = scheduler.element.find('.k-scheduler-content .k-scheduler-table tbody > tr:eq(' + i + ') > td'), uid = $(o).find('.txtTimeLineContainer').data('uid');
                        Common.Data.Each(scheduler.resources[0].dataSource.data(), function (u) {
                            if (u.value == uid) g = u;
                        })
                        if (Common.HasValue(g) && uid != undefined && g.TOETA != undefined && g.TOETD != undefined) {
                            var etd = Common.Date.FromJson(g.TOETD), eta = Common.Date.FromJson(g.TOETA);
                            angular.forEach(cells, function (td) {
                                var slot = scheduler.slotByElement(td);
                                if (Common.HasValue(slot) && slot.startDate <= eta && etd <= slot.endDate) {
                                    $(td).addClass('container-time');
                                }
                            })
                        }
                    })

                    $timeout(function () {
                        $scope.Init_TimeLine_Con_DragDrop();
                        $scope.TimeLineConLoading = false;
                        $rootScope.Loading.Change("Dữ liệu chi tiết đơn hàng...", 90);
                        $rootScope.Loading.Hide();
                    }, 1000)
                }, 10)
            }
        },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '', text: 'Data Empty' }], multiple: true
            }
        ],
        navigate: function () {
            $scope.TimesConLeft = 0;
        }
    }

    $scope.EventDrag = false;
    $scope.TimesConLeft = 0;
    $scope.ContentConLeft = 0;
    $scope.IsEventConTrigger = false;
    $scope.Init_TimeLine_Con_Times = function () {
        $('#timeline-con-grid .k-grid-content').off('scroll');
        $('#timeline-con-grid .k-grid-content').on('scroll', function (e, o) {
            var currentLeft = $(this).scrollLeft();
            if (currentLeft != $scope.TimesConLeft) {
                $scope.TimesConLeft = currentLeft;
                $("#timeline-con-grid .k-grid-header-wrap").scrollLeft(currentLeft);
            } else {
                if (o > 0 && e.isTrigger) {
                    $scope.IsEventConTrigger = false;
                    if ($(this).scrollTop() != o)
                        $(this).scrollTop(o);
                } else if ($scope.IsEventConTrigger) {
                    if ($('.cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').scrollTop() != $(this).scrollTop())
                        $('.cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').trigger('scroll', $(this).scrollTop());
                } else
                    $scope.IsEventConTrigger = true
            }
        });
        $('.cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').off('scroll');
        $('.cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content').on('scroll', function (e, o) {
            var currentLeft = $(this).scrollLeft();
            if (currentLeft != $scope.ContentConLeft) {
                $scope.ContentConLeft = currentLeft;
                $(".cus-scheduler.timeline-con .k-scheduler-layout > tbody > tr:eq(0) > td:eq(1) .k-scheduler-header .k-scheduler-header-wrap").scrollLeft($scope.ContentConLeft);
                if ($scope.SameDate) {
                    $scope.ContentLeft = currentLeft;
                    $(".cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content").scrollLeft($scope.ContentLeft);
                    $(".cus-scheduler.timeline-veh .k-scheduler-layout > tbody > tr:eq(0) > td:eq(1) .k-scheduler-header .k-scheduler-header-wrap").scrollLeft($scope.ContentLeft);
                }
            } else {
                if (o > 0 && e.isTrigger) {
                    $scope.IsEventConTrigger = false;
                    if ($(this).scrollTop() != o)
                        $(this).scrollTop(o);
                } else if ($scope.IsEventConTrigger) {
                    if ($('#timeline-con-grid .k-grid-content').scrollTop() != $(this).scrollTop())
                        $('#timeline-con-grid .k-grid-content').trigger('scroll', $(this).scrollTop());
                } else
                    $scope.IsEventConTrigger = true
            }
        });
    }

    $scope.Init_TimeLine_Con_DragDrop = function () {
        $('.cus-scheduler.timeline-con .k-scheduler-content .k-event.approved').kendoDraggable({
            group: "tocontainer_group", cursorOffset: { top: 0, left: 0 },
            hint: function (e) { return e.clone(); },
            drag: function (e) {
                var uid = e.currentTarget.data("uid"), item = $scope.timelineCon.dataSource.getByUid(uid), table = $($scope.timelineVeh.element).find('.k-scheduler-content');
                if (Common.HasValue(item)) {
                    if ($(e.elementUnderCursor).is('td[data-role="droptarget"]')) {
                        angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                            $(td).removeClass('hight-light');
                        })
                        $(e.elementUnderCursor).addClass("hight-light");
                        var ele = e.elementUnderCursor, slot = $scope.timelineVeh.slotByElement(ele);
                        if (Common.HasValue(slot)) {
                            var time = slot.endDate;
                            while (Common.HasValue(slot) && slot.endDate - time <= item.end - item.start) {
                                $(ele).addClass("hight-light"); ele = ele.nextSibling;
                                if (Common.HasValue(ele) && ele != []) slot = $scope.timelineVeh.slotByElement(ele)
                                else slot = null;
                            }
                        }
                    }
                }
            },
            dragend: function (e) {
                $timeout(function () {
                    var table = $($scope.timelineVeh.element).find('.k-scheduler-content');
                    angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                        $(td).removeClass('hight-light');
                    })
                }, 50)
            }
        });
        $('.cus-scheduler.timeline-veh .k-scheduler-content td').kendoDropTarget({
            group: "tocontainer_group",
            drop: function (e) {
                var itemDrag = $scope.timelineCon.dataSource.getByUid(e.draggable.currentTarget.data("uid")),
                    slot = $scope.timelineVeh.slotByElement(e.dropTarget), resource = $scope.timelineVeh.resources[0].dataSource.data();
                if (itemDrag != null && itemDrag.TOMasterID < 1 && Common.HasValue(resource[slot.groupIndex])) {
                    var obj = resource[slot.groupIndex];
                    var _createItem = function (rs, st, ed, sid) {
                        $scope.TimeLineItem = {
                            ID: -1,
                            Code: "mới tạo",
                            DriverName: "",
                            DriverTel: "",
                            StatusCode: rs.RomoocID < 1 ? 'Chưa chọn romooc' : 'Đang kiểm tra',
                            StatusColor: $scope.Color.Error,
                            VehicleID: rs.VehicleID,
                            VehicleNo: rs.VehicleNo,
                            RomoocID: rs.RomoocID,
                            RomoocNo: rs.RomoocNo,
                            VendorOfVehicleID: -1,
                            VendorOfVehicleCode: "Xe nhà",
                            Ton: 0,
                            Status: 1,
                            ETD: st, ETA: ed,
                            ListORDCon: [],
                            ListOPSCon: sid,
                            HasChange: false,
                            MinInterval: 0.5,
                            IsAllowChangeRomooc: true,
                            TimeMin: null, TimeMax: null,
                            DataContainerOffer: [],
                            OfferRomooc: true,
                            IsCheching: false,
                            LoadDataOffer: true,
                            HasTimeChange: true
                        }
                        $scope.TimeLineDetail = true;
                        $scope.TimeLine_TOMaster_OfferTime(function () {
                            $scope.timeline_info_win.center().open();
                        })
                    }
                    var ds = $.grep($scope.timelineCon.dataSource.data(), function (o) { return o.field == itemDrag.field && (o.SortOrder >= itemDrag.SortOrder || o.id == itemDrag.id) });
                    if (ds.length > 1) {
                        $rootScope.Message({
                            Msg: "Bạn muốn phân chuyến cho cả đơn hàng?",
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                _createItem(obj, slot.startDate, slot.startDate.addDays((itemDrag.end - itemDrag.start) / (24 * 60 * 60 * 1000)), ds.map(function (val, idx) { return val.id }));
                            },
                            Close: function () {
                                _createItem(obj, slot.startDate, slot.startDate.addDays((itemDrag.end - itemDrag.start) / (24 * 60 * 60 * 1000)), [itemDrag.id]);
                            }
                        })
                    } else {
                        _createItem(obj, slot.startDate, slot.startDate.addDays((itemDrag.end - itemDrag.start) / (24 * 60 * 60 * 1000)), [itemDrag.id]);
                    }
                }
            }
        });

        $('.cus-scheduler.timeline-veh .k-scheduler-content .k-event').kendoDropTarget({
            drop: function (e) {
                var itemDrag = $scope.timelineCon.dataSource.getByUid(e.draggable.currentTarget.data("uid")),
                    eventid = e.dropTarget.find('.cus-event').data('eventid'), itemDrop = $scope.timelineVeh.dataSource.get(eventid);

                if (Common.HasValue(itemDrop) && Common.HasValue(itemDrag)) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Check,
                        data: { mID: itemDrop.TOMasterID, data: [itemDrag.id], typeOfData: 1 },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                if (res != null && res.OfferTimeError != null && res.OfferTimeError != '') {
                                    $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: res.OfferTimeError });
                                } else {
                                    $rootScope.Message({
                                        Type: Common.Message.Type.Confirm,
                                        Msg: 'Xác nhận thêm đơn hàng vào chuyến?',
                                        Ok: function () {
                                            $rootScope.IsLoading = true;
                                            var data = [];
                                            Common.Data.Each(res.ListCOContainer, function (o) {
                                                o.StartDate = o.ETD;
                                                o.EndDate = o.ETA;
                                            })
                                            Common.Services.Call($http, {
                                                url: Common.Services.url.OPS,
                                                method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_AddContainer,
                                                data: {
                                                    mID: itemDrop.TOMasterID, data: res.ListCOContainer, ETD: res.ETD, ETA: res.ETA
                                                },
                                                success: function (res) {
                                                    Common.Services.Error(res, function (res) {
                                                        $rootScope.IsLoading = false;
                                                        $scope.Message({ Msg: 'Thành công!' });
                                                        $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                                        $rootScope.Loading.Show();
                                                        $scope.veh_Grid.dataSource.read();
                                                    })
                                                }
                                            })
                                        }
                                    })
                                }
                            })
                        }
                    })
                }
            },
            group: "tocontainer_group",
        });
    }

    $scope.ActionType = 0;
    $scope.TimeLineAction_Click = function ($event, val) {
        $event.preventDefault();

        if ($scope.ActionType == val)
            $scope.ActionType = 0;
        else
            $scope.ActionType = val;
    }

    $scope.TimeLineEvent_Click = function ($event, id, typeofevent, statusofevent, typeoftimesheet, win) {
        $event.preventDefault();
        if (typeofevent == 1 && typeoftimesheet <= 2) {
            if ($scope.ActionType > 0) {
                var uid = $($event.currentTarget).closest('.k-event').data('uid'), item = $scope.timelineVeh.dataSource.getByUid(uid);
                if (Common.HasValue(item)) {
                    var msg = "", flag = false, url = "", statusback = 1;
                    if ($scope.ActionType == 1) {
                        statusback = 2;
                        url = _OPSAppointment_COViewOnMapV5.URL.ToMon;
                        msg = " duyệt chuyến?";
                        if (statusofevent == 11)
                            flag = true;
                    } else if ($scope.ActionType == 2) {
                        statusback = 11;
                        url = _OPSAppointment_COViewOnMapV5.URL.ToOPS;
                        msg = " hủy duyệt chuyến?";
                        if (statusofevent == 2)
                            flag = true;
                    } else {
                        statusback = -1;
                        url = _OPSAppointment_COViewOnMapV5.URL.Delete;
                        msg = " xóa chuyến?";
                        if (statusofevent == 1 || statusofevent == 11)
                            flag = true;
                    }
                    if (flag) {
                        $rootScope.Message({
                            Msg: "Xác nhận" + msg,
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: url,
                                    data: { data: [id] },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $rootScope.IsLoading = false;
                                            if (statusback == -1) {
                                                Common.Data.Each($scope.timelineVeh.dataSource.data(), function (o) {
                                                    if (o.id == id) {
                                                        $scope.timelineVeh.removeEvent(o);
                                                    }
                                                })
                                            } else {
                                                Common.Data.Each($scope.timelineVeh.dataSource.data(), function (o) {
                                                    if (o.id == id) {
                                                        o.StatusOfEvent = statusback;
                                                    }
                                                })
                                                $scope.timelineVeh.refresh();
                                            }
                                            $rootScope.Message({ Msg: 'Thành công!' });
                                        }, function () {
                                            $rootScope.IsLoading = false;
                                        })
                                    }
                                })
                            }
                        })
                    } else {
                        $rootScope.Message({ Msg: "Thao tác không phù hợp!", Type: Common.Message.Type.Alert });
                    }
                }
            } else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV5.URL.TripByID,
                    data: { masterID: id },
                    success: function (res) {
                        if (Common.HasValue(res) && res.ID > 0) {
                            $scope.TimeLineItem = {
                                ID: res.ID,
                                Code: res.Code,
                                VehicleNo: res.VehicleNo,
                                DriverName: res.DriverName,
                                DriverTel: res.DriverTel,
                                StatusCode: 'Có thể cập nhật',
                                StatusColor: $scope.Color.None,
                                VehicleID: res.VehicleID,
                                RomoocID: res.RomoocID,
                                RomoocNo: res.RomoocNo,
                                VendorOfVehicleID: res.VendorOfVehicleID,
                                VendorOfVehicleCode: res.VendorCode,
                                Ton: res.TotalTon,
                                Status: res.Status,
                                ETA: Common.Date.FromJson(res.ETA),
                                ETD: Common.Date.FromJson(res.ETD),
                                ListOPSCon: [], ListORDCon: [],
                                IsAllowChangeRomooc: true,
                                TimeMin: null, TimeMax: null,
                                HasChange: false,
                                LoadDataOffer: true,
                                HasTimeChange: true
                            }
                            if ($scope.TimeLineItem.VendorOfVehicleID == null) {
                                $scope.TimeLineItem.VendorOfVehicleID = -1;
                            }
                            $scope.TimeLineDetail = true;
                            $scope.timeline_info_Grid.dataSource.read();
                            $timeout(function () {
                                $rootScope.IsLoading = false;
                                win.center().open();
                            }, 100)
                        }
                        else {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Không tìm thấy chuyến!" });
                        }
                    }
                });
            }
        }
    }

    $scope.timeline_info_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Container_List, pageSize: 0,
            readparam: function () { return { mID: $scope.TimeLineItem.ID, conID: -1 } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' }, autoBind: false,
        columns: [
            { field: 'DeleteTO', width: 40, title: ' ', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' }, template: '<a href="/" class="k-button small-button" ng-click="Container_Remove_Click($event,timeline_info_Grid,dataItem)" title="Xóa đơn hàng này"><i style="font-size:14px;color:rgba(111, 139, 169, 1);" class="fa fa-ban"></i></a>' },
            { field: 'CustomerCode', width: '120px', title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: '100px', title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '80px', title: 'Số seal 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '80px', title: 'Số seal 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: '100px', title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfContainerName', width: '100px', title: 'Chặng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note0', width: 150, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 150, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 150, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.Container_Remove_Click = function ($event, grid, item) {
        $event.preventDefault();

        if ($scope.TimeLineItem.Status > 2) {
            $rootScope.Message({ Msg: 'Không thể xóa đơn khỏi chuyến đã hoàn tất!', Type: Common.Message.Type.Alert });
        } else {
            $rootScope.Message({
                Msg: 'Xác nhận xóa đơn hàng khỏi chuyến?',
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_DelContainer,
                        data: { mID: $scope.TimeLineItem.ID, conID: item.ID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Thành công!" });
                                $rootScope.IsLoading = false;
                                grid.dataSource.read();
                                $scope.LoadDataTimeLine(function () {
                                    $scope.con_Grid.dataSource.read();
                                });
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.timeline_romooc_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.RomoocWaiting_List,
            readparam: function () {
                return {
                    data: $scope.VehicleRomoocData
                }
            }, pageSize: 0,
            model: {
                id: 'ID', fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    Lat: { type: 'number' },
                    Lng: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        columns: [
            { field: 'Select', width: 40, title: ' ', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' }, template: '<a href="/" class="k-button small-button" ng-click="Vehicle_Grid_Change_Romooc_OK_Click($event, dataItem, timeline_romooc_win)" title="Chọn romooc này"><i style="font-size:14px;color:rgba(111, 139, 169, 1);" class="fa fa-server"></i></a>', filterable: false, sortable: false },
            { field: 'RomoocNo', width: 100, title: 'Số romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxWeight', width: 80, title: 'Trọng tải', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxCapacity', width: 100, title: 'Chuyên chở', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'GroupOfRomoocName', width: 120, title: 'Loại romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', width: 150, title: 'Vị trí', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Lat', width: 100, title: 'Vĩ độ', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Lng', title: 'Kinh độ', filterable: { cell: { operator: 'gte', showOperators: false } } },
        ]
    }

    $scope.Vehicle_Filter_Click = function ($event, grid, type) {
        $event.preventDefault();

        var item = grid.dataItem(($event.currentTarget).closest('tr')), filter = grid.dataSource.filter();
        if (Common.HasValue(item)) {
            switch (type) {
                case 1:
                    if (!Common.HasValue(filter)) {
                        filter = [];
                        filter.push({ field: 'VehicleNo', operator: 'contains', value: item.VehicleNo });
                    } else if (Common.HasValue(filter.filters)) {
                        var idx = filter.filters.map(function (o, v) { return o.field }).indexOf("VehicleNo")
                        if (idx > -1) {
                            filter.filters.splice(idx, 1);
                        } else {
                            filter.filters.push({ field: 'VehicleNo', operator: 'contains', value: item.VehicleNo });
                        }
                    }
                    break;
                case 2:
                    if (!Common.HasValue(filter)) {
                        filter = [];
                        filter.push({ field: 'RomoocNo', operator: 'contains', value: item.RomoocNo });
                    } else {
                        var idx = filter.filters.map(function (o, v) { return o.field }).indexOf("RomoocNo")
                        if (idx > -1) {
                            filter.filters.splice(idx, 1);
                        } else {
                            filter.filters.push({ field: 'RomoocNo', operator: 'contains', value: item.RomoocNo });
                        }
                    }
                    break;
            }
            grid.dataSource.filter(filter);
        }
    }

    $scope.TypeOfServiceOrder = 1;

    $scope.timeline_con_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.ContainerToMaster_List,
            pageSize: 20,
            readparam: function () {
                return { typeofserviceorder: $scope.TypeOfServiceOrder }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' }
                }
            }
        }),
        pageable: Common.PageSize,
        height: '99%', groupable: false, sortable: true, columnMenu: false, resizable: true, filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_con_Grid,timeline_con_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_con_Grid,timeline_con_GridChoose_Change)" />',
                filterable: false, sortable: false, groupable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            { field: 'CustomerCode', width: 150, title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 150, title: 'Mã ĐH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: 80, title: 'Loại con.', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ETD', width: 160, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'ETA', width: 160, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'LocationFromCode', width: 150, title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 150, title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined1', width: 100, title: 'Định nghĩa 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined2', width: 100, title: 'Định nghĩa 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined3', width: 100, title: 'Định nghĩa 3', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined4', width: 100, title: 'Định nghĩa 4', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined5', width: 100, title: 'Định nghĩa 5', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined6', width: 100, title: 'Định nghĩa 6', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note0', width: 150, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 150, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 150, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.TimeLine_AddLocal_Click = function ($event, grid, win) {
        $event.preventDefault();
        $scope.TypeOfServiceOrder = 1;
        grid.dataSource.read();
        win.center().open();
    }

    $scope.TimeLine_AddIMEX_Click = function ($event, grid, win) {
        $event.preventDefault();
        $scope.TypeOfServiceOrder = 2;
        grid.dataSource.read();
        win.center().open();
    }

    $scope.TimeLine_AddLocal_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose }).map(function (o, v) { return o.ID });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Check,
                data: { mID: $scope.TimeLineItem.ID, data: data, typeOfData: 2 },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.IsLoading = false;
                        if (res != null && res.OfferTimeError != null && res.OfferTimeError != '') {
                            $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: res.OfferTimeError });
                        } else {
                            $rootScope.Message({
                                Type: Common.Message.Type.Confirm,
                                Msg: 'Xác nhận thêm đơn hàng vào chuyến?',
                                Ok: function () {
                                    $rootScope.IsLoading = true;
                                    var data = [];
                                    Common.Data.Each(res.ListCOContainer, function (o) {
                                        o.StartDate = o.ETD;
                                        o.EndDate = o.ETA;
                                    })
                                    Common.Services.Call($http, {
                                        url: Common.Services.url.OPS,
                                        method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_AddContainer,
                                        data: {
                                            mID: $scope.TimeLineItem.ID, data: res.ListCOContainer, ETD: res.ETD, ETA: res.ETA
                                        },
                                        success: function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $rootScope.IsLoading = false;
                                                $scope.Message({ Msg: 'Thành công!' });
                                                win.close();
                                                $scope.timeline_info_Grid.dataSource.read();
                                                $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                                $rootScope.Loading.Show();
                                                $scope.veh_Grid.dataSource.read();
                                            })
                                        }
                                    })
                                }
                            })
                        }
                    })
                }
            })
        } else {
            win.close();
        }
    }

    $scope.RomoocTempItem = [];
    $scope.RomoocTempVehicleID = -1;
    $scope.VehicleRomoocData = [];
    $scope.Vehicle_Grid_Change_Romooc = function ($event, grid, win) {
        $event.preventDefault();

        var tr = $($event.currentTarget).closest('tr'), item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            var data = [item.RomoocID];
            $scope.RomoocTempVehicleID = item.VehicleID;
            if (!Common.HasValue($scope.RomoocTempItem[item.VehicleID]))
                $scope.RomoocTempItem[item.VehicleID] = { ID: -1, RomoocNo: '', VehicleID: item.VehicleID, Target: tr };
            tr.prevUntil('tr.k-grouping-row').each(function (idx, row) { var o = grid.dataItem(row); if (Common.HasValue(o)) data.push(o.RomoocID); })
            tr.nextUntil('tr.k-grouping-row', 'tr').each(function (idx, row) { var o = grid.dataItem(row); if (Common.HasValue(o)) data.push(o.RomoocID); })
            $scope.VehicleRomoocData = data;

            $scope.timeline_romooc_Grid.dataSource.read();
            win.center().open();
        }
    }

    $scope.Vehicle_Grid_Change_Romooc_OK_Click = function ($event, item, win) {
        $event.preventDefault();

        var o = $scope.RomoocTempItem[$scope.RomoocTempVehicleID];
        if (Common.HasValue(o)) {
            if (o.ID != item.ID) {
                var obj = $scope.veh_Grid.dataItem(o.Target);
                if (Common.HasValue(obj)) {
                    $scope.RomoocTempItem[$scope.RomoocTempVehicleID].ID = item.ID;
                    $scope.RomoocTempItem[$scope.RomoocTempVehicleID].RomoocNo = "[" + item.RomoocNo + "]";
                    obj.IsChange = true;
                    obj.RomoocID = item.ID;
                    obj.RomoocNo = "[" + item.RomoocNo + "]";
                    obj.Lat = item.Lat;
                    obj.Lng = item.Lng;
                    obj.MaxWeight = item.MaxWeight;
                    obj.MaxCapacity = item.MaxCapacity;
                    obj.GroupOfRomoocName = item.GroupOfRomoocName;
                    obj.LocationName = item.LocationName;
                    $scope.veh_Grid.dataSource.sync();
                }
                win.close();
            } else {
                win.close();
            }
        } else {
            win.close();
        }
    }

    //AppenndView
    $timeout(function () {
        $($scope.con_Grid.element).find(".k-grid-pager").insertBefore($($scope.con_Grid.element).children(".k-grid-header"));
        $($scope.veh_Grid.element).find(".k-grid-pager").insertBefore($($scope.veh_Grid.element).children(".k-grid-header"));
        $($scope.con_Grid.element).find(".k-grid-header .k-grid-header-wrap").append("<div style='display:none;' class='grid-layer'></div>");
        $($scope.veh_Grid.element).find(".k-grid-pager").prepend("<span class='grid-title'>Phương tiện</span>");
        $($scope.con_Grid.element).find(".k-grid-pager").prepend("<span class='grid-title'>Đơn hàng</span>");

        $($scope.timelineVeh.element).find('.k-scheduler-toolbar').children().hide();
        $($scope.timelineVeh.element).find('.k-scheduler-toolbar').append('<div class="cus-form thin"><div class="form-header"><div class="left">'
            + '<a href="/" style="color:#fff;" class="k-button btn1" ng-click="TimeLine_Service_Click($event,timeline_service_win)"><i class="fa fa-server"></i><span class="tooltip is-left">Lọc dịch vụ</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button btn2" ng-click="TimeLine_Customer_Click($event,timeline_customer_win)"><i class="fa fa-group"></i><span class="tooltip">Lọc khách hàng</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button btn3" ng-click="TimeLine_Carrier_Click($event,timeline_carrier_win)"><i class="fa fa-ship"></i><span class="tooltip">Lọc hãng tàu</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button btn4" ng-click="TimeLine_Seaport_Click($event,timeline_seaport_win)"><i class="fa fa-anchor"></i><span class="tooltip">Lọc cảng biển</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button btn5" ng-click="TimeLine_OrderCo_Click($event,timeline_orderco_win)"><i class="fa fa-bars"></i><span class="tooltip">Lọc ĐH</span></a>'
            + '</div><div class="right">'
            + '<input class="cus-datepicker" style="width:100px;" focus-k-datepicker kendo-date-picker k-ng-model="DateRequest.fDate" k-options="DateDMY" /><span style="color:#fff"> - </span><input class="cus-datepicker" style="width:100px;" focus-k-datepicker kendo-date-picker k-ng-model="DateRequest.tDate" k-options="DateDMY" />'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="DateRequestChange_Click($event)"><i class="fa fa-refresh"></i><span class="tooltip">Làm mới</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="TimeLineRomoocType_Click($event)"><i class="fa fa-cubes"></i><span class="tooltip">Chặng phụ</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="TimeLineVehicleType_Click($event)"><i class="fa fa-filter"></i><span class="tooltip">{{VehicleLoadTypeName}}</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="ShowTooltipDescription($event)"><i class="fa fa-question" ng-mouseenter="ShowTooltipDescription($event)" ng-mouseleave="HideTooltipDescription($event)"></i></a>'
            + '</div></div></div>')
        $compile($($scope.timelineVeh.element).find('.k-scheduler-toolbar'))($scope);

        $($scope.timelineCon.element).find('.k-scheduler-toolbar').children().hide();
        $($scope.timelineCon.element).find('.k-scheduler-toolbar').append('<div class="cus-form thin"><div class="form-header"><div class="right">'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="TimeLineOrderPlan_OK_Click($event,con_Grid)"><i class="fa fa-check-circle-o"></i><span class="tooltip">Dành cho xe nhà</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="TimeLineOrderPlan_Click($event)"><i class="fa fa-stack-overflow"></i><span class="tooltip">Đơn hàng đang chờ</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="TimeLineOrderType_Click($event)"><i class="fa fa-share-alt"></i><span class="tooltip">{{OrderLoadTypeName}}</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button active" ng-click="TimeLineScrollType_Click($event)"><i class="fa fa-exchange"></i><span class="tooltip  is-right">Cùng thời gian</span></a>'
            + '</div></div></div>')
        $compile($($scope.timelineCon.element).find('.k-scheduler-toolbar'))($scope);

        $rootScope.Loading.Change("Khởi tạo...", 100);
        $rootScope.Loading.Hide();
        //$rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
        //$rootScope.Loading.Show();
        //$scope.veh_Grid.dataSource.read();
    }, 100)

    $scope.Tick = 60;
    $scope.TimeLine_Tick_Change = function ($event, val) {
        $event.preventDefault();

        if (val != $scope.Tick) {
            $scope.Tick = val;
            $($event.currentTarget).closest('div').find('a.btn-tick').removeClass('active');
            $($event.currentTarget).addClass('active');
            $scope.timelineVeh.views.Custom.majorTick = val;
            $scope.timelineCon.views.Custom.majorTick = val;
            $rootScope.Loading.Show();
            $rootScope.Loading.Change("Làm mới hiển thị", 20);
            $timeout(function () {
                $scope.timelineVeh.element.find('.k-nav-today').trigger('click');
                $scope.timelineCon.element.find('.k-nav-today').trigger('click');
            })
            $timeout(function () {
                $rootScope.Loading.Change("Làm mới hiển thị", 80);
                $timeout(function () {
                    $rootScope.Loading.Change("Làm mới hiển thị", 100);
                    $rootScope.Loading.Hide();
                }, 400)
            }, 1000)
        }
    }

    $scope.ShowTooltipDescription = function ($event) {
        $event.preventDefault();
        var off = $($event.currentTarget).closest('a').offset();
        $('#description_tooltip').show();
        $('#description_tooltip').css('top', off.top - 39);
        $('#description_tooltip').css('left', off.left - 485);
    }

    $scope.HideTooltipDescription = function ($event) {
        $event.preventDefault();
        $('#description_tooltip').hide();
    }

    $scope.TimeLineOrderPlan_Click = function ($event) {
        $event.preventDefault();
        if ($scope.IsOwnerPlanning == true) {
            $scope.IsOwnerPlanning = false;
            $($event.currentTarget).addClass('active');
            $scope.Loading.Show();
            $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
            $scope.con_Grid.dataSource.read();
        }
        else {
            $scope.IsOwnerPlanning = true;
            $($event.currentTarget).removeClass('active');
            $scope.Loading.Show();
            $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
            $scope.con_Grid.dataSource.read();
        }
    }

    $scope.TimeLineOrderPlan_OK_Click = function ($event, grid) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true }).map(function (o) { return o.OPSContainerID });
        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận chuyển trạng thái kế hoạch đơn hàng!", Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_Order_OwnerPlanning_Update,
                        data: { data: data, value: !$scope.IsOwnerPlanning },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: "Thành công!" });
                                $scope.Loading.Show();
                                $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
                                $scope.con_Grid.dataSource.read();
                            })
                        }
                    });
                }
            });
        } else {
            $rootScope.Message({ Msg: "Vui lòng chọn đơn hàng!", Type: Common.Message.Type.Alert });
        }
    }

    //#region Filter
    $scope.TimeLineDataOrderCO = [];
    $scope.TimeLineDataService = [];
    $scope.TimeLineDataCarrier = [];
    $scope.TimeLineDataSeaport = [];
    $scope.TimeLineDataCustomer = [];

    $scope.timeline_service_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: { ID: { type: 'number' }, Name: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false } }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.TimeLineDataService != null && $scope.TimeLineDataService.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).addClass('IsChoose');
                    } else {
                        item.IsChoose = false;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).removeClass('IsChoose');
                    }
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_service_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_service_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'Name', title: 'Tên dịch vụ', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.timeline_customer_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, Code: { type: 'string' },
                    CustomerName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false, filterable: { mode: 'row' },
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.TimeLineDataCustomer != null && $scope.TimeLineDataCustomer.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).addClass('IsChoose');
                    } else {
                        item.IsChoose = false;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).removeClass('IsChoose');
                    }
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_customer_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_customer_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'Code', width: '150px', title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.timeline_carrier_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: { ID: { type: 'number' }, Code: { type: 'string' }, CarrierName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false } }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false, filterable: { mode: 'row' },
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.TimeLineDataCarrier != null && $scope.TimeLineDataCarrier.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).addClass('IsChoose');
                    } else {
                        item.IsChoose = false;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).removeClass('IsChoose');
                    }
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_carrier_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_carrier_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'Code', title: 'Mã', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CarrierName', title: 'Tên hãng tàu', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.timeline_seaport_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: { ID: { type: 'number' }, SeaportCode: { type: 'string' }, SeaportName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false } }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false, filterable: { mode: 'row' },
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.TimeLineDataSeaport != null && $scope.TimeLineDataSeaport.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).addClass('IsChoose');
                    } else {
                        item.IsChoose = false;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).removeClass('IsChoose');
                    }
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_seaport_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_seaport_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'SeaportCode', title: 'Mã', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SeaportName', title: 'Tên cảng biển', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationCode', title: 'Mã địa chỉ', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên địa chỉ', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', title: 'Địa chỉ', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.timeline_orderco_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.OrderFilter_List,
            pageSize: 20,
            readparam: function () {
                $($scope.timeline_orderco_Grid.thead).find('input.chkAll[type="checkbox"]').prop('checked', false);
                return {
                    fDate: $scope.DateRequest.fDate,
                    tDate: $scope.DateRequest.tDate
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    WarningTime: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false, filterable: { mode: 'row' },
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.TimeLineDataOrderCO != null && $scope.TimeLineDataOrderCO.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).addClass('IsChoose');
                    } else {
                        item.IsChoose = false;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).removeClass('IsChoose');
                    }
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_orderco_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_orderco_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'CustomerCode', width: 120, title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 150, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: '100px', title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '80px', title: 'Số seal 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '80px', title: 'Số seal 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: '100px', title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#", filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } } },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotCode', width: '100px', title: 'Điểm lấy rỗng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotReturnCode', width: '100px', title: 'Điểm trả rỗng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationDepotReturnAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TripNo', width: 100, title: 'Số chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ShipNo', width: 100, title: 'Số tàu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ShipName', width: 100, title: 'Tên tàu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note0', width: 150, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 150, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 150, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined1', width: 100, title: 'Định nghĩa 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined2', width: 100, title: 'Định nghĩa 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined3', width: 100, title: 'Định nghĩa 3', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined4', width: 100, title: 'Định nghĩa 4', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined5', width: 100, title: 'Định nghĩa 5', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefined6', width: 100, title: 'Định nghĩa 6', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    }

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV5.URL.Customer_List,
        success: function (res) {
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.timeline_customer_Grid.dataSource.data(res);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV5.URL.Seaport_List,
        success: function (res) {
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.timeline_seaport_Grid.dataSource.data(res);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV5.URL.Carrier_List,
        success: function (res) {
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.timeline_carrier_Grid.dataSource.data(res);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV5.URL.Service_List,
        success: function (res) {
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.timeline_service_Grid.dataSource.data(res);
        }
    })

    $scope.TimeLine_Service_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TimeLineDataService.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.timeline_service_Grid.refresh();
            }, 100)
        } else {
            $scope.TimeLineDataService = [];
            $timeout(function () {
                $scope.timeline_service_Grid.refresh();
            }, 100)
            $($event.currentTarget).removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
    }

    $scope.TimeLine_Customer_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TimeLineDataCustomer.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.timeline_customer_Grid.refresh();
            }, 100)
        } else {
            $scope.TimeLineDataCustomer = [];
            $timeout(function () {
                $scope.timeline_customer_Grid.refresh();
            }, 100)
            $($event.currentTarget).removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
    }

    $scope.TimeLine_Carrier_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TimeLineDataCarrier.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.timeline_carrier_Grid.refresh();
            }, 100)
        } else {
            $scope.TimeLineDataCarrier = [];
            $timeout(function () {
                $scope.timeline_carrier_Grid.refresh();
            }, 100)
            $($event.currentTarget).removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
    }

    $scope.TimeLine_Seaport_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TimeLineDataSeaport.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.timeline_seaport_Grid.refresh();
            }, 100)
        } else {
            $scope.TimeLineDataSeaport = [];
            $timeout(function () {
                $scope.timeline_seaport_Grid.refresh();
            }, 100)
            $($event.currentTarget).removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
    }

    $scope.TimeLine_OrderCo_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TimeLineDataOrderCO.length == 0) {
            $scope.timeline_orderco_Grid.dataSource.read();
            win.center().open();
        } else {
            $scope.TimeLineDataOrderCO = [];
            $($event.currentTarget).removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
    }

    $scope.TimeLine_Service_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.TimeLineDataService.sort().toString()) {
            $scope.TimeLineDataService = data;
            if (data.length > 0) $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn1').addClass('active');
            else $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn1').removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
        win.close();
    }

    $scope.TimeLine_Customer_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.TimeLineDataCustomer.sort().toString()) {
            $scope.TimeLineDataCustomer = data;
            if (data.length > 0) $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn2').addClass('active');
            else $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn2').removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
        win.close();
    }

    $scope.TimeLine_Carrier_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.TimeLineDataCarrier.sort().toString()) {
            $scope.TimeLineDataCarrier = data;
            if (data.length > 0) $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn3').addClass('active');
            else $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn3').removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
        win.close();
    }

    $scope.TimeLine_Seaport_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.TimeLineDataSeaport.sort().toString()) {
            $scope.TimeLineDataSeaport = data;
            if (data.length > 0) $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn4').addClass('active');
            else $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn4').removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
        win.close();
    }

    $scope.TimeLine_OrderCO_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.TimeLineDataOrderCO.sort().toString()) {
            $scope.TimeLineDataOrderCO = data;
            if (data.length > 0) $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn5').addClass('active');
            else $($scope.timelineVeh.element).find('.k-scheduler-toolbar .k-button.btn5').removeClass('active');
            $scope.TimeLineVehicleFilterData()
        }
        win.close();
    }

    $scope.TimeLineVehicleFilter = [{ field: 'IsDuplicateHidden', operator: 'neq', value: true }];
    $scope.TimeLineVehicleFilterData = function () {
        var fcus = [], fser = [], fcar = [], fsea = [], ford = [];
        if ($scope.TimeLineDataService != null) {
            Common.Data.Each($scope.TimeLineDataService, function (o) {
                fser.push({ field: 'Option1', operator: 'eq', value: o })
            })
        }
        if ($scope.TimeLineDataCustomer != null) {
            Common.Data.Each($scope.TimeLineDataCustomer, function (o) {
                fcus.push({ field: 'Option2', operator: 'eq', value: o })
            })
        }
        if ($scope.TimeLineDataCarrier != null) {
            Common.Data.Each($scope.TimeLineDataCarrier, function (o) {
                fcar.push({ field: 'Option1', operator: 'eq', value: o })
            })
        }
        if ($scope.TimeLineDataSeaport != null) {
            Common.Data.Each($scope.TimeLineDataSeaport, function (o) {
                fsea.push({ field: 'Option4', operator: 'eq', value: o });
                fsea.push({ field: 'Option5', operator: 'eq', value: o });
            })
        }
        if ($scope.TimeLineDataOrderCO != null) {
            Common.Data.Each($scope.TimeLineDataOrderCO, function (o) {
                ford.push({ field: 'Option6', operator: 'eq', value: o });
            })
        }

        var f = [{ field: 'IsDuplicateHidden', operator: 'neq', value: true }];
        if (fser.length > 0) f.push({ logic: 'or', filters: fser });
        if (fcus.length > 0) f.push({ logic: 'or', filters: fcus });
        if (fcar.length > 0) f.push({ logic: 'or', filters: fcar });
        if (fsea.length > 0) f.push({ logic: 'or', filters: fsea });
        if (ford.length > 0) f.push({ logic: 'or', filters: ford });
        $scope.TimeLineVehicleFilter = f;
        $scope.TimeLineLoading = true;
        $scope.timelineVeh.dataSource.filter(f);
        $scope.Loading.Show();
        $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
        $scope.con_Grid.dataSource.read();
        $timeout(function () {
            $scope.TimeLineLoading = false;
        }, 500)
    }
    //#endregion

    //#endregion

    $scope.TimeLineDetail = false;
    $scope.VehicleRequestDate = new Date();
    $scope.TimeLineItem = {
        ID: -1,
        Code: "mới tạo",
        DriverName: "",
        DriverTel: "",
        StatusCode: 'Chưa chọn xe',
        StatusColor: $scope.Color.Error,
        VehicleID: -1,
        VehicleNo: "",
        RomoocID: -1,
        RomoocNo: "",
        VendorOfVehicleID: -1,
        VendorOfVehicleCode: "",
        Ton: 0,
        Status: 1,
        ETD: null, ETA: null,
        ListORDCon: [],
        ListOPSCon: [],
        HasChange: false,
        MinInterval: 0.5,
        IsAllowChangeRomooc: true,
        TimeMin: null, TimeMax: null,
        DataContainerOffer: [],
        IsCheching: false,
        HideUpdate: false,
        LoadDataOffer: true,
        HasTimeChange: true
    }
    $scope.TimeLineVehicleItem = { ID: -1, VendorID: -1, VehicleVendorCode: '', RegNo: '', MaxWeight: 0, RomoocNo: '', DriverName: '' }

    $scope.atcTimeLineDriverNameOptions = {
        dataSource: Common.DataSource.Local({ data: [] }),
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Họ tên", dataTextField: "DriverName",
        change: function (e) {
            var cbo = this;
            $timeout(function () {
                var obj = cbo.dataItem(cbo.select());
                if (Common.HasValue(obj)) {
                    $scope.TimeLineItem.DriverTel = obj.DriverTel;
                }
            }, 10)
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV5.URL.Driver_List,
        success: function (res) {
            var data = [];
            $.each(res, function (i, v) {
                data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
            });
            $scope.atcTimeLineDriverNameOptions.dataSource.data(data);
        }
    });

    $scope.$watch('TimeLineItem.ID', function () {
        if ($scope.TimeLineItem.ID > 0) {
            $('#btnSotransPrintReciept').prop('href', "/Report.aspx#/OPSSotrans/" + Common.Auth.HeaderKey + "/" + $scope.TimeLineItem.ID);
        }
    });
    $scope.$watch('TimeLineItem.ETD', function () {
        $scope.TimeLine_TOMaster_OfferTime(function () { $scope.CheckTimeLine("ETD") });
    });
    $scope.$watch('TimeLineItem.ETA', function () {
        $scope.TimeLine_TOMaster_OfferTime(function () { $scope.CheckTimeLine("ETA") });
    });
    $scope.$watch('TimeLineItem.VehicleID', function () {
        $scope.CheckTimeLine();
    });
    $scope.$watch('TimeLineItem.RomoocID', function () {
        $scope.CheckTimeLine();
    });

    $scope.CheckTimeLine = function (props) {
        Common.Log("CheckTimeLine")
        if ($scope.TimeLineDetail && $scope.TimeLineItem != null && $scope.TimeLineItem.Status == 1) {
            $scope.TimeLineItem.StatusCode = "";
            $scope.TimeLineItem.StatusColor = $scope.Color.None;
            var interval = $scope.TimeLineItem.MinInterval || 0.5;
            if (props == "ETD" && $scope.TimeLineItem.ETD != null && $scope.TimeLineItem.ETA != null) {
                if ($scope.TimeLineItem.ETD >= $scope.TimeLineItem.ETA || $scope.TimeLineItem.ETD.addDays(interval / 24) > $scope.TimeLineItem.ETA) {
                    $scope.TimeLineItem.ETA = $scope.TimeLineItem.ETD.addDays(interval / 24);
                }
            }
            if ($scope.TimeLineItem.ETAOffer == null)
                $scope.TimeLineItem.ETAOffer = $scope.TimeLineItem.ETA;
            if ($scope.TimeLineItem.ETDOffer == null)
                $scope.TimeLineItem.ETDOffer = $scope.TimeLineItem.ETD;

            if ($scope.TimeLineItem.VendorOfVehicleID == -1 && $scope.TimeLineItem.VehicleID > 0 && $scope.TimeLineItem.ETD != null && $scope.TimeLineItem.ETA != null) {
                Common.Log('TimeLine checking...');
                $scope.TimeLineItem.IsCheching = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV5.URL.CheckVehicleAvailable,
                    data: {
                        romoocID: $scope.TimeLineItem.RomoocID,
                        vehicleID: $scope.TimeLineItem.VehicleID,
                        masterID: $scope.TimeLineItem.ID,
                        ETD: $scope.TimeLineItem.ETD,
                        ETA: $scope.TimeLineItem.ETA,
                        Ton: $scope.TimeLineItem.TotalTon || 0,
                        dataCon: $scope.TimeLineItem.ListOPSCon,
                        dataOPSCon: [],
                        dataORDCon: $scope.TimeLineItem.ListORDCon
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $scope.TimeLineItem.IsCheching = false;
                            $scope.TimeLineItem.HideUpdate = false;
                            if ($scope.TimeLineItem.LoadDataOffer == true)
                                $scope.TimeLineItem.DataContainerOffer = res.ListCOContainer;
                            $scope.TimeLineItem.TimeMin = Common.Date.FromJson(res.DateMin);
                            $scope.TimeLineItem.TimeMax = Common.Date.FromJson(res.DateMax);
                            $scope.TimeLineItem.MinInterval = res.HourETAOffer || 0.5;
                            $scope.TimeLineItem.IsAllowChangeRomooc = res.AllowChangeRomooc;
                            $scope.TimeLineItem.IsAllowAddLocal = res.AllowAddLocal;

                            if (res.OfferNoteError != null && res.OfferNoteError != "") {
                                $scope.TimeLineItem.HideUpdate = true;
                                $scope.TimeLineItem.StatusCode = res.OfferNoteError;
                                $scope.TimeLineItem.StatusColor = $scope.Color.Error;
                            } else if (res.OfferNoteWarning != null && res.OfferNoteWarning != "") {
                                $scope.TimeLineItem.StatusCode = res.OfferNoteWarning;
                                $scope.TimeLineItem.StatusColor = $scope.Color.Warning;
                            } else if ($scope.TimeLineItem.DataContainerOffer == null || $scope.TimeLineItem.DataContainerOffer.length == 0) {
                                $scope.TimeLineItem.HideUpdate = true;
                                $scope.TimeLineItem.StatusCode = "Thời gian không phù hợp!";
                                $scope.TimeLineItem.StatusColor = $scope.Color.Error;
                            } else {
                                if ($scope.TimeLineItem.HasChange) {
                                    $scope.TimeLineItem.DriverTel = res.DriverTel;
                                    $scope.TimeLineItem.DriverName = res.DriverName;
                                }
                                $scope.TimeLineItem.StatusCode = "Có thể cập nhật";
                                $scope.TimeLineItem.StatusColor = $scope.Color.Success;
                            }
                        })
                    },
                    error: function (res) {
                        if (Common.HasValue(res)) {
                            $scope.TimeLineItem.StatusCode = res.ErrorMessage;
                        } else {
                            $scope.TimeLineItem.StatusCode = "Lỗi. Vui lòng thử lại!";
                        }
                        $scope.TimeLineItem.IsCheching = false;
                        $scope.TimeLineItem.HideUpdate = true;
                        $scope.TimeLineItem.StatusColor = $scope.Color.Error;
                    }
                })
            }

            try {
                if ($scope.TimeLineItem.VehicleID != $scope.TimeLineItem.VehicleOfferID || $scope.TimeLineItem.RomoocID != $scope.TimeLineItem.RomoocOfferID || $scope.TimeLineItem.ETA.getTime() != $scope.TimeLineItem.ETAOffer.getTime() || $scope.TimeLineItem.ETD.getTime() != $scope.TimeLineItem.ETDOffer.getTime()) {
                    $scope.TimeLineItem.HasTimeChange = true;
                    $scope.TimeLineItem.ETAOffer = $scope.TimeLineItem.ETA;
                    $scope.TimeLineItem.ETDOffer = $scope.TimeLineItem.ETD;
                    $scope.TimeLineItem.RomoocOfferID = $scope.TimeLineItem.RomoocID;
                    $scope.TimeLineItem.VehicleOfferID = $scope.TimeLineItem.VehicleID;
                }
            } catch (e) {
                $scope.TimeLineItem.HasTimeChange = true;
            }
        }
    }

    $scope.TimeLine_TOMaster_OfferTime = function (callback) {
        Common.Log("TimeLine_TOMaster_OfferTime")
        if ($scope.TimeLineDetail && ($scope.TimeLineItem.Status < 2 || $scope.TimeLineItem.Status == 11)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_OfferTime,
                data: { item: $scope.TimeLineItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $scope.TimeLineItem.HideUpdate = false;
                        if ($scope.TimeLineItem.LoadDataOffer == true)
                            $scope.TimeLineItem.DataContainerOffer = res.ListCOContainer;
                        $scope.TimeLineItem.TimeMin = Common.Date.FromJson(res.DateMin);
                        $scope.TimeLineItem.TimeMax = Common.Date.FromJson(res.DateMax);
                        $scope.TimeLineItem.MinInterval = res.HourETAOffer || 0.5;
                        $scope.TimeLineItem.IsAllowChangeRomooc = res.AllowChangeRomooc;
                        if ($scope.TimeLineItem.OfferRomooc) {
                            $scope.TimeLineItem.RomoocID = res.RomoocID > 0 ? res.RomoocID : -1;
                            $scope.TimeLineItem.RomoocNo = res.RomoocNo;
                        }
                        if ($scope.TimeLineItem.DataContainerOffer == null || $scope.TimeLineItem.DataContainerOffer.length == 0) {
                            $scope.TimeLineItem.HideUpdate = true;
                        }
                        $rootScope.IsLoading = false;
                        if (Common.HasValue(callback))
                            callback(res);
                    })
                }
            })
        }
    }

    $scope.indexMap = null;
    openMapV2.hasMap = true;
    $scope.Container_Map_Click = function ($event, item, win) {
        $event.preventDefault();

        var _fnLoadData = function () {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_ORDLocation_OnMap_List,
                data: { conID: item.ID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        var tmp = [];
                        for (var i = 0; i < res.length; i++) {
                            var o = res[i], p = res[i - 1];
                            if (Common.HasValue(o) && o.Lat > 0 && o.Lng > 0) {
                                var img = Common.String.Format(openMapV2.NewImage.Stock);
                                if (o.GroupOfLocationID == 1)
                                    img = Common.String.Format(openMapV2.NewImage.Depot);
                                if (o.GroupOfLocationID == 2)
                                    img = Common.String.Format(openMapV2.NewImage.Seaport);
                                var icon = openMapV2.NewStyle.Icon(img, 1);
                                tmp[i] = openMapV2.NewMarker(o.Lat, o.Lng, o.Code, o.Location, icon, {
                                    Item: o, Type: 'Location'
                                }, "VectorMarkerORD");
                                if (Common.HasValue(p)) {
                                    var strName = o.Location + " - " + p.Location;
                                    openMapV2.NewRoute(tmp[i - 1], tmp[i], "", strName, openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)', [20, 10]), null, "VectorRouteORD", $scope.indexMap, function () {
                                    }, true);
                                }
                            }
                        }
                        openMapV2.FitBound("VectorMarkerORD", 15);
                    })
                }
            })
        }

        win.center().open();
        openMapV2.ClearVector("VectorRouteORD");
        openMapV2.ClearVector("VectorMarkerORD");
        if ($scope.indexMap == null) {
            $scope.indexMap = openMapV2.Init({
                Element: 'map',
                Tooltip_Show: true,
                Tooltip_Element: 'map_tooltip',
                InfoWin_Show: false,
                DefinedLayer: [{
                    Name: 'VectorMarkerORD',
                    zIndex: 100
                }, {
                    Name: 'VectorMarkerROM',
                    zIndex: 99
                }, {
                    Name: 'VectorMarkerVEH',
                    zIndex: 99
                }, {
                    Name: 'VectorRouteORD',
                    zIndex: 90
                }]
            });
            openMapV2.NewControl('A', 'Chỉ hiện đầu kéo', 'map-view-vehicle-button', function (e, o, a) {
                switch (o.textContent) {
                    case 'A':
                        o.textContent = 'V';
                        o.setAttribute('title', 'Chỉ hiện romooc');
                        a.VisibleVector("VectorMarkerVEH", true);
                        a.VisibleVector("VectorMarkerROM", false);
                        break;
                    case 'V':
                        o.textContent = 'R';
                        o.setAttribute('title', 'Hiện tất cả');
                        a.VisibleVector("VectorMarkerROM", true);
                        a.VisibleVector("VectorMarkerVEH", false);
                        break;
                    case 'R':
                        o.textContent = 'A';
                        o.setAttribute('title', 'Chỉ hiện đầu kéo');
                        a.VisibleVector("VectorMarkerVEH", true);
                        a.VisibleVector("VectorMarkerROM", true);
                        break;
                }
            })
            _fnLoadData();
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_Vehicle_OnMap_List,
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        for (var i = 0; i < res.length; i++) {
                            var o = res[i];
                            if (Common.HasValue(o) && o.Lat > 0 && o.Lng > 0) {
                                var img = Common.String.Format(openMapV2.NewImage.Tractor);
                                if (o.TypeOfGroupID == 1) {
                                    var icon = openMapV2.NewStyle.Icon(img, 1);
                                    openMapV2.NewMarker(o.Lat, o.Lng, o.VehicleNo, o.VehicleNo, icon, {
                                        Item: o, Type: 'Tractor'
                                    }, "VectorMarkerVEH");
                                } else {
                                    img = Common.String.Format(openMapV2.NewImage.Romooc_20);
                                    var icon = openMapV2.NewStyle.Icon(img, 1);
                                    openMapV2.NewMarker(o.Lat, o.Lng, o.VehicleNo, o.VehicleNo, icon, {
                                        Item: o, Type: 'Romooc'
                                    }, "VectorMarkerROM");
                                }
                            }
                        }
                    })
                }
            })
        } else {
            _fnLoadData();
        }
    }

    $scope.TimeLineConEvent_Click = function ($event, id, tomasterid, sort, start, end, win, vwin) {
        $event.preventDefault();

        if (tomasterid > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV5.URL.TripByID,
                data: { masterID: tomasterid },
                success: function (res) {
                    if (Common.HasValue(res) && res.ID > 0) {
                        $scope.TimeLineItem = {
                            ID: res.ID,
                            Code: res.Code,
                            VehicleNo: res.VehicleNo,
                            DriverName: res.DriverName,
                            DriverTel: res.DriverTel,
                            StatusCode: 'Có thể cập nhật',
                            StatusColor: $scope.Color.None,
                            VehicleID: res.VehicleID,
                            RomoocID: res.RomoocID,
                            RomoocNo: res.RomoocNo,
                            VendorOfVehicleID: res.VendorOfVehicleID,
                            VendorOfVehicleCode: res.VendorCode,
                            Ton: res.TotalTon,
                            Status: res.Status,
                            ETA: Common.Date.FromJson(res.ETA),
                            ETD: Common.Date.FromJson(res.ETD),
                            ListOPSCon: [], ListORDCon: [],
                            IsAllowChangeRomooc: true,
                            TimeMin: null, TimeMax: null,
                            HasChange: false,
                            LoadDataOffer: true,
                            HasTimeChange: true
                        }
                        if ($scope.TimeLineItem.VendorOfVehicleID == null) {
                            $scope.TimeLineItem.VendorOfVehicleID = -1;
                        }
                        $scope.TimeLineDetail = true;
                        $scope.timeline_info_Grid.dataSource.read();
                        $timeout(function () {
                            $rootScope.IsLoading = false;
                            vwin.center().open();
                        }, 100)
                    }
                    else {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Không tìm thấy chuyến!" });
                    }
                }
            });
        }
        else {
            var _createItem = function (st, ed, sid) {
                $scope.TimeLineItem = {
                    ID: -1,
                    Code: "mới tạo",
                    DriverName: "",
                    DriverTel: "",
                    StatusCode: 'Chưa chọn xe',
                    StatusColor: $scope.Color.Error,
                    VehicleID: -1,
                    VehicleNo: "[Chờ nhập]",
                    RomoocID: -1,
                    RomoocNo: "[Chờ nhập]",
                    VendorOfVehicleID: -1,
                    VendorOfVehicleCode: "Xe nhà",
                    Ton: 0,
                    Status: 1,
                    ListORDCon: [],
                    ETD: st, ETA: ed,
                    ListOPSCon: sid,
                    HasChange: false,
                    MinInterval: 0.5,
                    IsAllowChangeRomooc: true,
                    TimeMin: null, TimeMax: null,
                    DataContainerOffer: [],
                    OfferRomooc: true,
                    IsCheching: false,
                    LoadDataOffer: true,
                    HasTimeChange: true
                }
                $scope.TimeLineDetail = true;
                $scope.TimeLine_TOMaster_OfferTime(function () {
                    win.center().open();
                })
            }
            var data = $scope.timelineCon.dataSource.data();
            var obj = $.grep(data, function (o) { return o.id == id && o.TOMasterID == tomasterid && o.SortOrder == sort })[0];
            if (Common.HasValue(obj)) {
                var ds = $.grep(data, function (o) { return o.field == obj.field && (o.SortOrder >= sort || o.id == id) });
                if (ds.length > 1) {
                    $rootScope.Message({
                        Msg: "Bạn muốn phân chuyến cho cả đơn hàng?",
                        Type: Common.Message.Type.Confirm,
                        Ok: function () {
                            _createItem(new Date(start), new Date(end), ds.map(function (val, idx) { return val.id }));
                        },
                        Close: function () {
                            _createItem(new Date(start), new Date(end), [id]);
                        }
                    })
                } else {
                    _createItem(new Date(start), new Date(end), [id]);
                }
            }
        }
    }

    $scope.TimeLineVehicle_Change_Click = function ($event, typeofVehicle, win) {
        $event.preventDefault();

        if (typeofVehicle == 2 && !$scope.TimeLineItem.IsAllowChangeRomooc) {
            $rootScope.Message({ Msg: "Không cho phép đổi romooc", Type: Common.Message.Type.Alert });
        } else {
            win.center().open();
            $scope.ChangeVehicleType = typeofVehicle;
            $scope.IsVehicleWinActived = true;
            $scope.VehicleRequestDate = $scope.TimeLineItem.ETD || new Date();
            $timeout(function () {
                switch ($scope.ChangeVehicleType) {
                    case 1:
                        $scope.TimeLineVehicleItem.ID = $scope.TimeLineItem.VehicleID;
                        $scope.vehWin_Grid.dataSource.read();
                        break;
                    case 2:
                        $scope.TimeLineVehicleItem.ID = $scope.TimeLineItem.RomoocID;
                        $scope.romWin_Grid.dataSource.read();
                        break;
                    default:
                        break;
                }
                $rootScope.IsLoading = false;
            }, 100);
        }
    }

    $scope.vehWin_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.Tractor_List,
            readparam: function () { return { requestDate: $scope.VehicleRequestDate } }, pageSize: 0,
            model: { id: 'ID', fields: { ID: { type: 'number' }, MaxWeight: { type: 'number' } } }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        columns: [
            { field: 'Regno', width: 120, title: 'Số xe', template: '<span>#=Regno# </span>' + '<a class="k-button select" ng-click="VehicleOnWin_Select($event,dataItem,1,vehicle_win)"><span>LC</span></a>', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxWeight', width: 70, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'RomoocNo', width: 120, title: 'Romooc hiện tại', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfTractorName', width: 80, title: 'T/trạng', filterable: false, sortable: false },
            { field: 'LocationName', width: 150, title: 'Điểm hiện tại', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.romWin_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.Romooc_List,
            readparam: function () { return { requestDate: $scope.VehicleRequestDate } }, pageSize: 0,
            model: { id: 'ID', fields: { ID: { type: 'number' }, MaxWeight: { type: 'number' } } }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        columns: [
            { field: 'Regno', width: 120, title: 'Romooc', template: '<span>#=Regno# </span>' + '<a class="k-button select" ng-click="VehicleOnWin_Select($event,dataItem,2,vehicle_win)"><span>LC</span></a>', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxWeight', width: 70, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'GroupOfRomoocName', width: 80, title: 'Loại romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfRomoocName', width: 80, title: 'T/trạng', filterable: false, sortable: false },
            { field: 'LocationName', width: 150, title: 'Điểm hiện tại', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.VehicleOnWin_Select = function ($event, item, type, win) {
        $event.preventDefault();

        if ($scope.TimeLineDetail) {
            if ($scope.TimeLineItem.ID > 0) {
                switch (type) {
                    case 1:
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_OfferVehicle,
                            data: { mID: $scope.TimeLineItem.ID, venID: $scope.TimeLineItem.VendorOfVehicleID, vehID: item.ID, isTractor: true },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $rootScope.IsLoading = false;
                                    if (res != null && res.OfferTimeError != null && res.OfferTimeError != "") {
                                        $rootScope.Message({ Msg: res.OfferTimeError, Type: Common.Message.Type.Alert })
                                    } else {
                                        var msg = "Bạn muốn đổi đầu kéo này?";
                                        if (res.OfferTimeWarning != null && res.OfferTimeWarning != "")
                                            msg = res.OfferTimeWarning + ", tiếp tục lưu thay đổi?";
                                        $rootScope.Message({
                                            Msg: msg,
                                            Type: Common.Message.Type.Confirm,
                                            Ok: function () {
                                                $rootScope.IsLoading = true;
                                                Common.Services.Call($http, {
                                                    url: Common.Services.url.OPS,
                                                    method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Update_Vehicle,
                                                    data: { mID: $scope.TimeLineItem.ID, venID: $scope.TimeLineItem.VendorOfVehicleID, vehID: item.ID, isTractor: true },
                                                    success: function (res) {
                                                        Common.Services.Error(res, function (res) {
                                                            $scope.ChangeData = true;
                                                            $rootScope.IsLoading = false;
                                                            $rootScope.Message({ Msg: 'Thành công!' });
                                                            $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                                            $rootScope.Loading.Show();
                                                            $scope.veh_Grid.dataSource.read();
                                                            $scope.TimeLineItem.HasChange = false;
                                                            $scope.TimeLineItem.VehicleID = item.ID;
                                                            $scope.TimeLineItem.VehicleNo = item.Regno;
                                                            win.close();
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
                        });
                        break;
                    case 2:
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_OfferVehicle,
                            data: { mID: $scope.TimeLineItem.ID, venID: $scope.TimeLineItem.VendorOfVehicleID, vehID: item.ID, isTractor: false },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $rootScope.IsLoading = false;
                                    if (res != null && res.OfferTimeError != null && res.OfferTimeError != "") {
                                        $rootScope.Message({ Msg: res.OfferTimeError, Type: Common.Message.Type.Alert })
                                    } else {
                                        var msg = "Bạn muốn đổi romooc này?";
                                        if (res.OfferTimeWarning != null && res.OfferTimeWarning != "")
                                            msg = res.OfferTimeWarning + ", tiếp tục lưu thay đổi?";
                                        $rootScope.Message({
                                            Msg: msg,
                                            Type: Common.Message.Type.Confirm,
                                            Ok: function () {
                                                $rootScope.IsLoading = true;
                                                Common.Services.Call($http, {
                                                    url: Common.Services.url.OPS,
                                                    method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Update_Vehicle,
                                                    data: { mID: $scope.TimeLineItem.ID, venID: $scope.TimeLineItem.VendorOfVehicleID, vehID: item.ID, isTractor: false },
                                                    success: function (res) {
                                                        Common.Services.Error(res, function (res) {
                                                            $scope.ChangeData = true;
                                                            $rootScope.IsLoading = false;
                                                            $rootScope.Message({ Msg: 'Thành công!' });
                                                            $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                                            $rootScope.Loading.Show();
                                                            $scope.veh_Grid.dataSource.read();
                                                            $scope.TimeLineItem.HasChange = false;
                                                            $scope.TimeLineItem.RomoocID = item.ID;
                                                            $scope.TimeLineItem.RomoocNo = item.Regno;
                                                            win.close();
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
                        });
                        break;
                    default: break;
                }
            } else {
                switch (type) {
                    case 1:
                        if (item.RomoocID > 0 && $scope.TimeLineItem.IsAllowChangeRomooc) {
                            $rootScope.Message({
                                Msg: "Bạn muốn sử dụng romooc theo xe?",
                                Type: Common.Message.Type.Confirm,
                                Ok: function () {
                                    $scope.TimeLineItem.HasChange = true;
                                    $scope.TimeLineItem.VehicleID = item.ID;
                                    $scope.TimeLineItem.VehicleNo = item.Regno;
                                    $scope.TimeLineItem.RomoocID = item.RomoocID;
                                    $scope.TimeLineItem.RomoocNo = item.RomoocNo;
                                    win.close();
                                },
                                Close: function () {
                                    $scope.TimeLineItem.HasChange = true;
                                    $scope.TimeLineItem.VehicleID = item.ID;
                                    $scope.TimeLineItem.VehicleNo = item.Regno;
                                    win.close();
                                }
                            })
                        } else {
                            $scope.TimeLineItem.HasChange = true;
                            $scope.TimeLineItem.VehicleID = item.ID;
                            $scope.TimeLineItem.VehicleNo = item.Regno;
                            win.close();
                        }
                        break;
                    case 2:
                        $scope.TimeLineItem.HasChange = true;
                        $scope.TimeLineItem.RomoocID = item.ID;
                        $scope.TimeLineItem.RomoocNo = item.Regno;
                        win.close();
                        break;
                    default: break;
                }
            }
        }
    }

    //Lưu
    $scope.TimeLine_Update_OK_Click = function ($event, win) {
        $event.preventDefault();
        var flag = true;
        if ($scope.TimeLineItem.RomoocID == null || $scope.TimeLineItem.RomoocID == "") $scope.TimeLineItem.RomoocID = -1;
        if ($scope.TimeLineItem.ETA == null || $scope.TimeLineItem.ETD == null) {
            flag = false; $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        }
        else if ($scope.TimeLineItem.ETA <= $scope.TimeLineItem.ETD) {
            flag = false; $rootScope.Message({ Msg: "Sai ràng buộc ETD và ETA.", Type: Common.Message.Type.Alert });
        }
        else if ($scope.TimeLineItem.VehicleID < 1) {
            flag = false; $rootScope.Message({ Msg: "Vui lòng chọn đầu kéo.", Type: Common.Message.Type.Alert });
        }
        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận lưu?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    if ($scope.TimeLineItem.ID < 1) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Save,
                            data: { item: $scope.TimeLineItem, dataOffer: $scope.TimeLineItem.DataContainerOffer || [] },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    $scope.TimeLineDetail = false;
                                    $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                    $rootScope.Loading.Show();
                                    $scope.veh_Grid.dataSource.read();
                                    win.close();
                                }, function () {
                                    $rootScope.IsLoading = false;
                                })
                            }
                        })
                    } else {
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Update_Time,
                            data: { mID: $scope.TimeLineItem.ID, ETD: $scope.TimeLineItem.ETD, ETA: $scope.TimeLineItem.ETA, dataContainer: $scope.TimeLineItem.DataContainerOffer },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    $scope.TimeLineDetail = false;
                                    $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                    $rootScope.Loading.Show();
                                    $scope.veh_Grid.dataSource.read();
                                    win.close();
                                }, function () {
                                    $rootScope.IsLoading = false;
                                })
                            }
                        })
                    }
                }
            })
        }
    }

    ///Xóa
    $scope.TimeLine_Delete_OK_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TimeLineItem.ID > 0) {
            $rootScope.Message({
                Msg: "Xác nhận xóa chuyến?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV5.URL.Delete,
                        data: { data: [$scope.TimeLineItem.ID] },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.TimeLineDetail = false;
                                $scope.TimeLineItem = {
                                    ID: -1,
                                    Code: "mới tạo",
                                    DriverName: "",
                                    DriverTel: "",
                                    StatusCode: 'Chưa chọn xe',
                                    StatusColor: $scope.Color.Error,
                                    VehicleID: -1,
                                    VehicleNo: "",
                                    RomoocID: -1,
                                    RomoocNo: "",
                                    VendorOfVehicleID: -1,
                                    VendorOfVehicleCode: "",
                                    Ton: 0,
                                    Status: 1,
                                    ETD: null, ETA: null,
                                    ListORDCon: [],
                                    ListOPSCon: [],
                                    HasChange: false,
                                    MinInterval: 0.5,
                                    IsAllowChangeRomooc: true,
                                    TimeMin: null, TimeMax: null,
                                    DataContainerOffer: [],
                                    IsCheching: false,
                                    HideUpdate: false,
                                    LoadDataOffer: true,
                                    HasTimeChange: true
                                }
                                $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                $rootScope.Loading.Show();
                                $scope.veh_Grid.dataSource.read();
                                win.close();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            })
        }
    }

    //Duyệt
    $scope.TimeLine_Tender_OK_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TimeLineItem.ID > 0) {
            $rootScope.Message({
                Msg: "Xác nhận duyệt chuyến?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV5.URL.ToMon,
                        data: { data: [$scope.TimeLineItem.ID] },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.TimeLineDetail = false;
                                $scope.TimeLineItem = {
                                    ID: -1,
                                    Code: "mới tạo",
                                    DriverName: "",
                                    DriverTel: "",
                                    StatusCode: 'Chưa chọn xe',
                                    StatusColor: $scope.Color.Error,
                                    VehicleID: -1,
                                    VehicleNo: "",
                                    RomoocID: -1,
                                    RomoocNo: "",
                                    VendorOfVehicleID: -1,
                                    VendorOfVehicleCode: "",
                                    Ton: 0,
                                    Status: 1,
                                    ETD: null, ETA: null,
                                    ListORDCon: [],
                                    ListOPSCon: [],
                                    HasChange: false,
                                    MinInterval: 0.5,
                                    IsAllowChangeRomooc: true,
                                    TimeMin: null, TimeMax: null,
                                    DataContainerOffer: [],
                                    IsCheching: false,
                                    HideUpdate: false,
                                    LoadDataOffer: true,
                                    HasTimeChange: true
                                }
                                $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                $rootScope.Loading.Show();
                                $scope.veh_Grid.dataSource.read();
                                win.close();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            })
        }
    }

    //Hủy duyệt
    $scope.TimeLine_Cancel_OK_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.TimeLineItem.ID > 0) {
            $rootScope.Message({
                Msg: "Xác nhận hủy duyệt chuyến?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV5.URL.ToOPS,
                        data: { data: [$scope.TimeLineItem.ID] },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.TimeLineDetail = false;
                                $scope.TimeLineItem = {
                                    ID: -1,
                                    Code: "mới tạo",
                                    DriverName: "",
                                    DriverTel: "",
                                    StatusCode: 'Chưa chọn xe',
                                    StatusColor: $scope.Color.Error,
                                    VehicleID: -1,
                                    VehicleNo: "",
                                    RomoocID: -1,
                                    RomoocNo: "",
                                    VendorOfVehicleID: -1,
                                    VendorOfVehicleCode: "",
                                    Ton: 0,
                                    Status: 1,
                                    ETD: null, ETA: null,
                                    ListORDCon: [],
                                    ListOPSCon: [],
                                    HasChange: false,
                                    MinInterval: 0.5,
                                    IsAllowChangeRomooc: true,
                                    TimeMin: null, TimeMax: null,
                                    DataContainerOffer: [],
                                    IsCheching: false,
                                    HideUpdate: false,
                                    LoadDataOffer: true,
                                    HasTimeChange: true
                                }
                                $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                $rootScope.Loading.Show();
                                $scope.veh_Grid.dataSource.read();
                                win.close();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            })
        }
    }

    $scope.TimeLine_Update_Driver_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận cập nhật lại tài xế cho chuyến hiện tại?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Update_Driver,
                    data: { data: [$scope.TimeLineItem.ID] },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!' });
                            $scope.TimeLineItem.DriverName = res.DriverName;
                            $scope.TimeLineItem.DriverTel = res.DriverTel;
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    //Điều chỉnh chặng

    $scope.TimeLine_Change_Time_DataTemp = [];
    $scope.TimeLine_Change_Time_Click = function ($event) {
        $event.preventDefault();
        if ($scope.TimeLineItem.HasTimeChange) {
            $scope.TimeLineItem.HasTimeChange = false;
            var obj = $scope.TimeLineItem;
            var vehID = obj.VehicleID > 0 ? obj.VehicleID : -1,
                romID = obj.RomoocID > 0 ? obj.RomoocID : -1,
                venID = obj.VendorOfVehicleID > 0 ? obj.VendorOfVehicleID : -1,
                etd = obj.ETD != null && angular.isDate(obj.ETD) ? obj.ETD : new Date(),
                eta = obj.ETA != null && angular.isDate(obj.ETA) ? obj.ETA : new Date()
            $scope.LoadDataDragDropInfo(obj.ID, -1, vehID, romID, etd, eta, [], $scope.TimeLineItem.ListOPSCon);
        }
        else {
            $rootScope.IsLoading = true;
            $scope.timeline_info_time_win.center().open();
            $scope.IsShowTimeLineInfo = true;
            $scope.TimeLine_Change_Time_DataTemp = {};
            angular.forEach($scope.timeline_info.dataSource.data(), function (o, i) {
                if ((o.TypeOfGroupID == 1 && o.Code == 'New') || o.TypeOfGroupID == 4) {
                    $scope.TimeLine_Change_Time_DataTemp[o.id + "_" + o.field] = {
                        id: o.id, start: o.start, end: o.end, field: o.field
                    }
                }
            })
            $timeout(function () {
                $scope.timeline_info.date($scope.TimeLineItem.ETD);
                $scope.timeline_info.view($scope.timeline_info.view().name);
                $rootScope.IsLoading = false;
            }, 300)
        }
    }

    $scope.TimeLineInfoBound = false;
    $scope.IsShowTimeLineInfo = false;
    $scope.TimeLineInfoTOMasterID = -1;
    $scope.TimeLineInfoVehicleType = 1;

    $scope.TimeLineInfoResource_Click = function ($event, scheduler, value) {
        $event.preventDefault();

        $scope.NewScheduleDragDropInfoVehicleType = value;
        var filter = {
            logic: "or",
            filters: [
                { field: "value", operator: "startswith", value: $scope.NewScheduleDragDropInfoVehicleType + "-" },
                { field: "value", operator: "startswith", value: "3-" }, { field: "value", operator: "startswith", value: "4-" }
            ]
        };
        scheduler.resources[0].dataSource.filter(filter);
        $timeout(function () {
            scheduler.view(scheduler.view().name);
        }, 50)
    }

    $scope.LoadDataDragDropInfo = function (m1ID, m2ID, vehID, romID, ETD, ETA, dataOPSCon, dataCon) {
        Common.Log("LoadDataDragDropInfo");

        $scope.TimeLineInfoBound = false;
        $scope.TimeLineInfoTOMasterID = m1ID;
        $scope.TimeLineInfoVehicleType = 1;
        $scope.IsShowTimeLineInfo = false;
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_Data,
            data: {
                m1ID: m1ID, m2ID: m2ID,
                venID: -1, vehID: vehID, romID: romID,
                ETA: ETA, ETD: ETD, typeOfResource: 1,
                dataOPSCon: dataOPSCon, dataCon: dataCon
            },
            success: function (res) {
                $scope.IsShowTimeLineInfo = true;
                Common.Data.Each(res.DataSources, function (o) {
                    o.field = o.TypeOfGroupID + "-" + o.GroupID;
                })
                var dataG4 = $.grep(res.DataSources, function (o) { return o.TypeOfGroupID == 4 });
                var dataG1 = $.grep(res.DataSources, function (o) { return o.TypeOfGroupID <= 2 && o.ID == $scope.TimeLineInfoTOMasterID });
                var min = new Date(Math.min.apply(Math, dataG4.map(function (o) { return Common.Date.FromJson(o.StartDate); })))
                var max = new Date(Math.max.apply(Math, dataG4.map(function (o) { return Common.Date.FromJson(o.EndDate); })))
                Common.Data.Each(dataG1, function (o) {
                    o.StartDate = min; o.EndDate = max;
                })
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: res.DataSources,
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
                $scope.timeline_info.setDataSource(dataSource);
                Common.Data.Each(res.Resources, function (o) {
                    o.text = o.Text; o.value = o.TypeOfGroupID + "-" + o.Value;
                })
                var filter = {
                    logic: "or",
                    filters: [
                        { field: "value", operator: "startswith", value: $scope.TimeLineInfoVehicleType + "-" },
                        { field: "value", operator: "startswith", value: "3-" }, { field: "value", operator: "startswith", value: "4-" }
                    ]
                };
                $scope.timeline_info.resources[0].dataSource.data(res.Resources);
                $scope.timeline_info.resources[0].dataSource.filter(filter);
                $scope.timeline_info_time_win.center().open();
                $timeout(function () {
                    angular.forEach($scope.timeline_info.dataSource.data(), function (o, i) {
                        if ((o.TypeOfGroupID == 1 && o.Code == 'New') || o.TypeOfGroupID == 4) {
                            $scope.TimeLine_Change_Time_DataTemp[o.id + "_" + o.field] = {
                                id: o.id, start: o.start, end: o.end, field: o.field
                            }
                        }
                    })
                    $scope.timeline_info.date(ETD);
                    $scope.timeline_info.view($scope.timeline_info.view().name);
                    $rootScope.IsLoading = false;
                }, 500)
            }
        })
    }

    $scope.TimeLineInfoEventItem = {};

    $scope.timeline_info_Options = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: true, resize: true, update: false },
        views: [
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 60,
                selectedDateFormat: "{0:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 120
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 60, selected: true,
                selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 360
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 60,
                selectedDateFormat: "{0:MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 360
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
        groupHeaderTemplate: kendo.template("<span class='txtGroup' data-uid='#=value#'><strong>#=text#</strong></span>"),
        eventTemplate: $("#timeline-info-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.TimeLineInfoBound == false && $scope.IsShowTimeLineInfo == true) {
                    $scope.TimeLineInfoBound = true;
                    scheduler.view(scheduler.view().name);
                } else if ($scope.TimeLineInfoBound == true && $scope.IsShowTimeLineInfo == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case 1:
                                        if (i.TypeOfGroupID == 1 || i.TypeOfGroupID == 2) {
                                            $(o).find('.k-resize-handle').hide();
                                            if (i.id == $scope.TimeLineInfoTOMasterID) {
                                                $(o).addClass('approved');
                                            } else {
                                                $(o).addClass('tendered');
                                            }
                                        } else if (i.TypeOfGroupID == 3) {
                                            $(o).find('.k-resize-handle').hide();
                                            if ($scope.TimeLineInfoTOMasterID > 0) {
                                                if (i.StatusOfEvent == 1) {
                                                    $(o).addClass('con-planning');
                                                } else if (i.StatusOfEvent == 2) {
                                                    $(o).addClass('con-tranfers');
                                                } else if (i.StatusOfEvent == -1) {
                                                    $(o).addClass('approved');
                                                } else if (i.StatusOfEvent == -11) {
                                                    $(o).addClass('tenderable');
                                                } else if (i.StatusOfEvent == -2) {
                                                    $(o).addClass('tendered');
                                                } else {
                                                    $(o).addClass('con-revieved');
                                                }
                                            }
                                            else {
                                                if (i.StatusOfEvent == -1) {
                                                    $(o).addClass('approved');
                                                } else if (i.StatusOfEvent == -11) {
                                                    $(o).addClass('tenderable');
                                                } else {
                                                    $(o).addClass('tendered');
                                                }
                                            }
                                        } else {
                                            $(o).addClass('con-planning');
                                        }
                                        break;
                                    case 2:
                                        $(o).addClass('maintainance');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 3:
                                        $(o).addClass('registry');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 4:
                                        $(o).addClass('repair');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    default:
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                }
                            }
                        })
                    })
                    $timeout(function () {
                        scheduler.element.find('.k-scheduler-content tr td').each(function (idx, td) {
                            var slot = scheduler.slotByElement(td), resource = scheduler.resources[0].dataSource.view();
                            if (Common.HasValue(slot) && Common.HasValue(resource[slot.groupIndex])) {
                                var uid = resource[slot.groupIndex].value;
                                if (uid != null && uid.toString().split('-')[0] != 3) {
                                    $(td).css('background', 'rgb(255, 249, 158)');
                                }
                            }
                        })
                        scheduler.element.find('.k-scheduler-times tr').each(function (idx, tr) {
                            var uid = $(tr).find('span.txtGroup').data('uid');
                            if (uid != null && uid.toString().split('-')[0] != 3) {
                                $(tr).css('background', 'rgb(255, 249, 158)');
                            }
                        })
                    }, 100)
                }
            }, 10)
        },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '-1', text: 'Data Empty' }], multiple: true
            }
        ],
        moveStart: function (e) {
            if (e.event.TypeOfGroupID != 4) {
                e.preventDefault();
            } else {
                $scope.TimeLineInfoEventItem = $.extend(true, {}, e.event);
            }
        },
        resizeStart: function (e) {
            if (e.event.TypeOfGroupID < 3 || (e.event.TypeOfGroupID == 3 && e.event.StatusOfEvent != 1)) {
                e.preventDefault();
            } else {
                $scope.TimeLineInfoEventItem = $.extend(true, {}, e.event);
            }
        },
        save: function (e) {
            var scheduler = this, obj = $.extend(true, {}, e.event), data = scheduler._data, field = "";
            if (typeof obj.field == "string") field = obj.field;
            else if (typeof obj.field == "object") field = obj.field[0];
            var _refreshScheduler = function (sch) {
                var idx = 0;
                $.each(sch.dataSource.data(), function (i, o) {
                    if (o.id == obj.id) {
                        idx = i;
                    }
                })
                var item = sch.dataSource.at(idx);
                if (Common.HasValue(item)) {
                    item.end = $scope.TimeLineInfoEventItem.end;
                    item.start = $scope.TimeLineInfoEventItem.start;
                    item.field = $scope.TimeLineInfoEventItem.field;
                }
                sch.refresh();
            }
            if (obj.TypeOfEvent == 1) {
                if (obj.TypeOfGroupID == 4) {
                    if (!field.startsWith(obj.TypeOfGroupID)) {
                        $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                        _refreshScheduler(scheduler);
                    } else {
                        if (obj.start >= obj.end) {
                            $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                            _refreshScheduler(scheduler);
                        } else {
                            if (obj.start >= $scope.TimeLineInfoEventItem.start) {
                                //-->
                                var ds = new kendo.data.DataSource({
                                    data: data,
                                    filter: [{
                                        field: 'start', operator: 'gte', value: $scope.TimeLineInfoEventItem.end
                                    }, {
                                        field: 'field', operator: 'eq', value: field
                                    }, {
                                        field: 'id', operator: 'neq', value: obj.id
                                    }],
                                    sort: [{ field: 'start', dir: "asc" }]
                                })
                                ds.fetch(function () {
                                    var view = ds.view();
                                    var _30MIN = 30 * 60 * 1000;
                                    if (view.length > 0) {
                                        var item = {}, time = obj.end;
                                        for (var i = 0; i < view.length; i++) {
                                            item = view[i];
                                            if (item.start < time) {
                                                item.start = new Date(time.getTime() + 1000);
                                                if (item.end - item.start < _30MIN) {
                                                    item.end = new Date(item.start.getTime() + _30MIN);
                                                    time = item.end;
                                                }
                                            }
                                        }
                                    }
                                    var dataG4 = $.grep(data, function (o) { return o.TypeOfGroupID == 4 });
                                    var dataG1 = $.grep(data, function (o) { return o.TypeOfGroupID <= 2 && o.id == $scope.TimeLineInfoTOMasterID });
                                    var min = new Date(Math.min.apply(Math, dataG4.map(function (o) { return o.start; })))
                                    var max = new Date(Math.max.apply(Math, dataG4.map(function (o) { return o.end; })))
                                    Common.Data.Each(dataG1, function (o) {
                                        $.each(scheduler.dataSource.data(), function (i, e) {
                                            if (e.id == o.id) {
                                                var u = scheduler.dataSource.at(i);
                                                if (Common.HasValue(u)) {
                                                    u.start = min; u.end = max;
                                                }
                                            }
                                        })
                                    })
                                    scheduler.refresh();
                                });
                            } else {
                                //<--
                                var ds = new kendo.data.DataSource({
                                    data: data,
                                    filter: [{
                                        field: 'end', operator: 'lte', value: $scope.TimeLineInfoEventItem.start
                                    }, {
                                        field: 'field', operator: 'eq', value: field
                                    }, {
                                        field: 'id', operator: 'neq', value: obj.id
                                    }],
                                    sort: [{ field: 'end', dir: "desc" }]
                                })
                                ds.fetch(function () {
                                    var view = ds.view();
                                    var _30MIN = 30 * 60 * 1000;
                                    if (view.length > 0) {
                                        var item = {}, time = obj.start;
                                        for (var i = 0; i < view.length; i++) {
                                            item = view[i];
                                            if (item.end > time) {
                                                item.end = new Date(time.getTime() - 1000);
                                                if (item.end - item.start < _30MIN) {
                                                    item.start = new Date(item.end.getTime() - _30MIN);
                                                    time = item.start;
                                                }
                                            }
                                        }
                                    }
                                    var dataG4 = $.grep(data, function (o) { return o.TypeOfGroupID == 4 });
                                    var dataG1 = $.grep(data, function (o) { return o.TypeOfGroupID <= 2 && o.id == $scope.TimeLineInfoTOMasterID });
                                    var min = new Date(Math.min.apply(Math, dataG4.map(function (o) { return o.start; })))
                                    var max = new Date(Math.max.apply(Math, dataG4.map(function (o) { return o.end; })))
                                    Common.Data.Each(dataG1, function (o) {
                                        $.each(scheduler.dataSource.data(), function (i, e) {
                                            if (e.id == o.id) {
                                                var u = scheduler.dataSource.at(i);
                                                if (Common.HasValue(u)) {
                                                    u.start = min; u.end = max;
                                                }
                                            }
                                        })
                                    })
                                    scheduler.refresh();
                                });
                            }
                        }
                    }
                } else if (obj.TypeOfGroupID == 3) {
                    if (!field.startsWith(obj.TypeOfGroupID)) {
                        $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                        _refreshScheduler(scheduler);
                    } else {
                        if (obj.start >= obj.end) {
                            $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                            _refreshScheduler(scheduler);
                        } else {
                            if (obj.start >= $scope.TimeLineInfoEventItem.start) {
                                //-->
                                var ds = new kendo.data.DataSource({
                                    data: data,
                                    filter: [{
                                        field: 'start', operator: 'gte', value: $scope.TimeLineInfoEventItem.end
                                    }, {
                                        field: 'field', operator: 'eq', value: field
                                    }, {
                                        field: 'id', operator: 'neq', value: obj.id
                                    }],
                                    sort: [{ field: 'start', dir: "asc" }]
                                })
                                ds.fetch(function () {
                                    var view = ds.view();
                                    var _30MIN = 30 * 60 * 1000;
                                    if (view.length > 0) {
                                        var item = {}, time = obj.end;
                                        for (var i = 0; i < view.length; i++) {
                                            item = view[i];
                                            if (item.start < time) {
                                                item.start = new Date(time.getTime() + 1000);
                                                if (item.end - item.start < _30MIN) {
                                                    item.end = new Date(item.start.getTime() + _30MIN);
                                                    time = item.end;
                                                }
                                            }
                                        }
                                    }
                                });
                            } else {
                                //<--
                                var ds = new kendo.data.DataSource({
                                    data: data,
                                    filter: [{
                                        field: 'end', operator: 'lte', value: $scope.TimeLineInfoEventItem.start
                                    }, {
                                        field: 'field', operator: 'eq', value: field
                                    }, {
                                        field: 'id', operator: 'neq', value: obj.id
                                    }],
                                    sort: [{ field: 'end', dir: "desc" }]
                                })
                                ds.fetch(function () {
                                    var view = ds.view();
                                    var _30MIN = 30 * 60 * 1000;
                                    if (view.length > 0) {
                                        var item = {}, time = obj.start;
                                        for (var i = 0; i < view.length; i++) {
                                            item = view[i];
                                            if (item.end > time) {
                                                item.end = new Date(time.getTime() - 1000);
                                                if (item.end - item.start < _30MIN) {
                                                    item.start = new Date(item.end.getTime() - _30MIN);
                                                    time = item.start;
                                                }
                                            }
                                        }
                                    }
                                });
                            }
                        }
                    }
                } else {
                    $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                    _refreshScheduler(scheduler);
                }
            }
        }
    }

    $('body > #timeline_info_tooltip').remove();
    $('#timeline_info_tooltip').detach().appendTo('body');

    $scope.TimeLineInfoItem = null;
    $scope.TimeLineInfoStyle = { 'display': 'none', 'top': 0, 'left': 0 };

    $scope.ShowTimeLineInfoTooltip = function ($event, uid) {
        var flag = false;
        Common.Data.Each($scope.timeline_info.dataSource.data(), function (i) {
            if (i.uid == uid) {
                if (i.TypeOfGroupID == 3) {
                    flag = true;
                    switch (i.Code) {
                        case 'ETD-ETA':
                            $scope.TimeLineInfoItem = { ETA: i.end, ETD: i.start };
                            $scope.TimeLineInfoItem.TypeDate = "Kế hoạch";
                            break;
                        case 'ETDRequest':
                            $scope.TimeLineInfoItem = { ETD: i.end, ETA: null };
                            $scope.TimeLineInfoItem.TypeDate = "Ngày yêu cầu lấy hàng";
                            break;
                        case 'ETARequest':
                            $scope.TimeLineInfoItem = { ETD: i.end, ETA: null };
                            $scope.TimeLineInfoItem.TypeDate = "Ngày yêu cầu giao hàng";
                            break;
                        case 'CutOffTime':
                            $scope.TimeLineInfoItem = { ETD: i.end, ETA: null };
                            $scope.TimeLineInfoItem.TypeDate = "Hạn chót cắt máng/trả rỗng";
                            break;
                        default: break;
                    }
                }
            }
        })
        if (flag) {
            var off = $($event.currentTarget).offset();
            $scope.TimeLineInfoStyle = {
                'display': '', 'top': off.top + 32, 'left': off.left > 100 ? off.left : 100
            }
        }
    }

    $scope.HideTimeLineInfoTooltip = function ($event) {
        $scope.TimeLineInfoItem = null;
        $scope.TimeLineInfoStyle = {
            'display': 'none', 'top': 0, 'left': 0
        }
    }

    $scope.TimeLineInfo_OK_Click = function ($event, scheduler, win) {
        $event.preventDefault();

        if ($scope.TimeLineInfoTOMasterID < 1) {
            //Cập nhật ETA, ETD của chuyến/ ===> XXX
            var to = $.grep(scheduler.dataSource.data(), function (o) { return o.id == $scope.TimeLineItem.ID && o.TypeOfGroupID == 1 });
            if (to.length > 0) {
                $scope.TimeLineItem.LoadDataOffer = false;
                $scope.TimeLineItem.ETA = to[0].end;
                $scope.TimeLineItem.ETD = to[0].start;
            }
            var ds = new kendo.data.DataSource({
                data: scheduler.dataSource.data(),
                filter: [{
                    field: 'TypeOfGroupID', operator: 'gte', value: 4
                }]
            })
            ds.fetch(function () {
                var view = ds.view();
                var data = [];
                Common.Data.Each(view, function (o) {
                    var obj = {
                        ID: o.id,
                        ETD: o.start,
                        ETA: o.end,
                        SortOrder: o.SortOrder
                    }
                    data.push(obj);
                })
                $scope.TimeLineItem.DataContainerOffer = data;
                $rootScope.IsLoading = true;
                $timeout(function () {
                    win.close();
                    $rootScope.IsLoading = false;
                    $scope.TimeLineItem.LoadDataOffer = true;
                }, 2000)
            })
        } else {
            //Điều chỉnh chuyến
            if ($scope.TimeLineDetail) {
                var to = $.grep(scheduler.dataSource.data(), function (o) { return o.id == $scope.TimeLineItem.ID && o.TypeOfGroupID == 1 });
                if (to.length > 0) {
                    $scope.TimeLineItem.LoadDataOffer = false;
                    $scope.TimeLineItem.ETA = to[0].end;
                    $scope.TimeLineItem.ETD = to[0].start;
                }
                var ds = new kendo.data.DataSource({
                    data: scheduler.dataSource.data(),
                    filter: [{
                        field: 'TypeOfGroupID', operator: 'gte', value: 4
                    }]
                })
                ds.fetch(function () {
                    var view = ds.view();
                    var data = [];
                    Common.Data.Each(view, function (o) {
                        var obj = {
                            ID: o.id,
                            ETD: o.start,
                            ETA: o.end,
                            SortOrder: o.SortOrder
                        }
                        data.push(obj);
                    })
                    $scope.TimeLineItem.DataContainerOffer = data;
                    $rootScope.IsLoading = true;
                    $timeout(function () {
                        win.close();
                        $rootScope.IsLoading = false;
                        $scope.TimeLineItem.LoadDataOffer = true;
                    }, 2000)
                })
            } else { //Thêm container vào chuyến 
                var etd = null, eta = null;
                var to = $.grep(scheduler.dataSource.data(), function (o) { return o.id == $scope.TimeLineInfoTOMasterID && o.TypeOfGroupID == 1 });
                if (to.length > 0) {
                    eta = to[0].end;
                    etd = to[0].start;
                }
                var ds = new kendo.data.DataSource({
                    data: scheduler.dataSource.data(),
                    filter: [{
                        field: 'TypeOfGroupID', operator: 'gte', value: 4
                    }],
                    sort: [{ field: 'TypeOfGroupID', dir: "asc" }]
                })
                ds.fetch(function () {
                    var view = ds.view();
                    var data = [];
                    Common.Data.Each(view, function (o) {
                        var obj = $.extend(true, {}, o);
                        obj.ID = o.id;
                        obj.Title = obj.title;
                        obj.StartDate = o.start;
                        obj.EndDate = o.end;
                        data.push(obj);
                    })
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Bạn muốn thay đổi chuyến theo thời gian đã điều chỉnh?',
                        Ok: function () {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_TOMaster_AddContainer,
                                data: {
                                    mID: $scope.TimeLineInfoTOMasterID, data: data, ETD: etd, ETA: eta
                                },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.IsLoading = false;
                                        $scope.Message({ Msg: 'Thành công!' });
                                        $rootScope.Loading.Change("Dữ liệu phương tiện...", 0);
                                        $rootScope.Loading.Show();
                                        $scope.veh_Grid.dataSource.read();
                                        win.close();
                                    })
                                }
                            })
                        }
                    })
                });
            }
        }
    }

    //#region VehTimeLine
    $scope.TimeLineVehicleType = 1;

    $scope.TimeLine_Click = function ($event, win) {
        $event.preventDefault();

        $scope.TimeLineVehicleType = 1;
        $scope.TimeLineVeh1Bound = false;
        $scope.TimeLineVeh2Bound = false;
        $scope.IsShowTimeLineVeh1 = false;
        $scope.IsShowTimeLineVeh2 = false;

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_Vehicle_Schedule_Data,
            data: { typeofvehicle: 1 },
            success: function (res) {
                $scope.IsShowTimeLineVeh1 = true;
                Common.Data.Each(res.DataSources, function (o) {
                    o.field = o.VehicleID
                })
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: res.DataSources,
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
                $scope.veh_timeline_1.setDataSource(dataSource);
                Common.Data.Each(res.Resources, function (o) {
                    o.text = o.Text; o.value = o.ID;
                })
                $scope.veh_timeline_1.resources[0].dataSource.data(res.Resources);
                win.center().open();
                $rootScope.IsLoading = false;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV5.URL.TimeLine_Vehicle_Schedule_Data,
                    data: { typeofvehicle: 2 },
                    success: function (res) {
                        $scope.IsShowTimeLineVeh2 = true;
                        Common.Data.Each(res.DataSources, function (o) {
                            o.field = o.VehicleID
                        })
                        var dataSource = new kendo.data.SchedulerDataSource({
                            data: res.DataSources,
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
                        $scope.veh_timeline_2.setDataSource(dataSource);
                        Common.Data.Each(res.Resources, function (o) {
                            o.text = o.Text; o.value = o.ID;
                        })
                        $scope.veh_timeline_2.resources[0].dataSource.data(res.Resources);
                    }
                })
            }
        })
    }

    $scope.TimeLineVehicle_Click = function ($event, val) {
        $event.preventDefault();

        $scope.TimeLineVehicleType = val;
        if (val == 2) {
            $scope.veh_timeline_2.view($scope.veh_timeline_2.view().name);
        }
    }

    $scope.TimeLineVeh1Bound = false;
    $scope.TimeLineVeh2Bound = false;
    $scope.IsShowTimeLineVeh1 = false;
    $scope.IsShowTimeLineVeh2 = false;

    $scope.veh_timeline_1_Options = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: false,
        views: [
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 60,
                selectedDateFormat: "{0:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 60
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 60, selected: true,
                selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 60
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 60,
                selectedDateFormat: "{0:MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 60
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
        groupHeaderTemplate: kendo.template("<span class='txtGroup' data-uid='#=value#'><strong>#=text#</strong></span>"),
        eventTemplate: $("#timeline-veh-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.TimeLineVeh1Bound == false && $scope.IsShowTimeLineVeh1 == true) {
                    $scope.TimeLineVeh1Bound = true;
                    scheduler.view(scheduler.view().name);
                } else if ($scope.TimeLineVeh1Bound == true && $scope.IsShowTimeLineVeh1 == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case 1:
                                        $(o).addClass('approved');
                                        break;
                                    case 2:
                                        $(o).addClass('repair');
                                        break;
                                    default:
                                        break;
                                }
                            }
                        })
                    })
                }
            }, 10)
        },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '-1', text: 'Data Empty' }], multiple: true
            }
        ]
    }

    $scope.veh_timeline_2_Options = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: false,
        views: [
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 60,
                selectedDateFormat: "{0:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 60
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 60, selected: true,
                selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 60
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 60,
                selectedDateFormat: "{0:MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong data-dayofweek='#=date.getDay()#'>#=_OPSAppointment_COViewOnMapV5.DAYOFWEEK[date.getDay()]# - #=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 60
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
        groupHeaderTemplate: kendo.template("<span class='txtGroup' data-uid='#=value#'><strong>#=text#</strong></span>"),
        eventTemplate: $("#timeline-veh-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.TimeLineVeh2Bound == false && $scope.IsShowTimeLineVeh2 == true) {
                    $scope.TimeLineVeh2Bound = true;
                    scheduler.view(scheduler.view().name);
                } else if ($scope.TimeLineVeh2Bound == true && $scope.IsShowTimeLineVeh2 == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case 1:
                                        $(o).addClass('approved');
                                        break;
                                    case 2:
                                        $(o).addClass('repair');
                                        break;
                                    default:
                                        break;
                                }
                            }
                        })
                    })
                }
            }, 10)
        },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '-1', text: 'Data Empty' }], multiple: true
            }
        ]
    }

    //#endregion

    //#region Action
    $scope.Close_Click = function ($event, win, code) {
        $event.preventDefault();

        try {
            $scope.On_Close(code, $event);
            switch (code) {
                case 'ORD':
                    break;
                case 'TimeLineDetail':
                    $scope.TimeLineDetail = false;
                    break;
                case 'TimeLineInfo':
                    $scope.IsShowTimeLineInfo = false;
                    if ($scope.TimeLineInfoTOMasterID < 1) {
                        angular.forEach($scope.timeline_info.dataSource.data(), function (o, i) {
                            if ((o.TypeOfGroupID == 4 || (o.TypeOfGroupID == 1 && o.Code == 'New')) && Common.HasValue($scope.TimeLine_Change_Time_DataTemp[o.id + "_" + o.field])) {
                                var v = $scope.TimeLine_Change_Time_DataTemp[o.id + "_" + o.field];
                                o.start = v.start; o.end = v.end;
                            }
                        })
                    }
                    break;
            }
            win.close();
        } catch (e) {
        }
    }

    $scope.On_Close = function (code, event) {
        switch (code) {
            case 'TimeLineDetail':
                $scope.TimeLineDetail = false;
                break;
            case 'TimeLineInfo':
                $scope.IsShowTimeLineInfo = false;
                if (event.userTriggered) {
                    if ($scope.TimeLineInfoTOMasterID < 1) {
                        angular.forEach($scope.timeline_info.dataSource.data(), function (o, i) {
                            if ((o.TypeOfGroupID == 4 || (o.TypeOfGroupID == 1 && o.Code == 'New')) && Common.HasValue($scope.TimeLine_Change_Time_DataTemp[o.id + "_" + o.field])) {
                                var v = $scope.TimeLine_Change_Time_DataTemp[o.id + "_" + o.field];
                                o.start = v.start; o.end = v.end;
                            }
                        })
                    }
                }
                break;
        }
    }

    $scope.On_Resize = function (code) {
        switch (code) {
            case 'TimeLineDetail':
                $scope.TimeLineDetail = false;
                break;
        }
    }

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.OPSAppointmentCO,
            event: $event, current: $state.current,
            customview: true, customcache: "view.OPSAppointment.COViewOnMap",
            callback: function (e) {
                if (e) {
                    Common.Cookie.Set("view.OPSAppointment.COViewOnMap", $state.current.name);
                } else {
                    Common.Cookie.Set("view.OPSAppointment.COViewOnMap", 'main.OPSAppointment.COViewOnMapV2');
                }
            }
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }

    $scope.ViewA_Click = function ($event) {
        $event.preventDefault();

        angular.element('#2view').addClass('fullscreen');
        angular.element('.mainform .mainmenu').addClass('fullscreen');
        angular.element('#2view').resize();
        $scope.IsFullScreen = true;
    }

    $scope.ViewB_Click = function ($event) {
        $event.preventDefault();

        $scope.IsFullScreen = false;
        angular.element('.mainform .mainmenu').removeClass('fullscreen');
        angular.element('#2view').removeClass('fullscreen');
        angular.element('#2view').resize();
        $scope.viewSplitter.resize();
    }
    //#endregion

    $scope.ToDateString = function (v) {
        return Common.Date.FromJsonDMYHM(v);
    }

    $scope.ToDateStringHM = function (v) {
        return Common.Date.FromJsonHM(v);
    }

    $scope.ToDateStringDMY = function (v) {
        return Common.Date.FromJsonDDMMYY(v);
    }

    $scope.ToDateStringDMHM = function (v) {
        return Common.Date.FromJsonDDMM(v) + " " + Common.Date.FromJsonHM(v);
    }

    $rootScope.Loading.Change("Khởi tạo...", 50);
    $rootScope.IsLoading = false;
    $timeout(function () {
        $scope.InitComplete = true;
        $rootScope.Loading.Change("Khởi tạo...", 80);
    }, 1000)
}]);