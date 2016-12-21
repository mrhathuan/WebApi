/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />


angular.module('myapp').controller('MONMonitor_ControlTowerCOTimelineCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', '$interval', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile, $interval) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('MONMonitor_ControlTowerCOTimelineCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    angular.element('.mainform .mainmenu').removeClass('fullscreen');

    //#region Common

    $scope.IsFullScreen = false;

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.MONMonitor,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }

    $scope.GetMonday = function (d) {
        d = new Date(d);
        var day = d.getDay(),
            diff = d.getDate() - day + (day == 0 ? -6 : 1);
        return new Date(d.setDate(diff));
    };

    $scope.CT_Confirm = {
        Lable: "",
        OK: function () { },
        Deny: function () { },
    }
    $scope.CTConfirm = function (opstions) {
        $scope.CT_Confirm = {
            Lable: "",
            OK: function () {
            },
            Deny: function () {
            },
        }
        angular.extend($scope.CT_Confirm, opstions);

        $scope.CT_Confirm.Action_OK = function () {
            $scope.CT_Confirm.OK();
            $scope.CT_Confirm_Win.close();
        }
        $scope.CT_Confirm.Action_Deny = function () {
            $scope.CT_Confirm.Deny();
            $scope.CT_Confirm_Win.close();
        }
        $scope.CT_Confirm_Win.center().open();
    }

    $scope.ToDateStringDMHM = function (v) {
        return Common.Date.FromJsonDDMM(v) + " " + Common.Date.FromJsonHM(v);
    }

    $scope.DrawNewMarker = function (lst, fnIcon, vname, isClear, code, title, codetype) {
        $scope.ListMarker = [];
        if (!Common.HasValue(codetype))
            codetype = "";
        Common.Data.Each(lst, function (o) {
            var icon = fnIcon(o);
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                $scope.ListMarker.push(openMapV2.NewMarker(o.Lat, o.Lng, codetype + o[code], o[title], icon, o, vname));
            }
        })
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
        //$scope.viewSplitter.resize();
    }

    $scope.Tick = 60;
    $scope.TimeLine_Tick_Change = function ($event, val) {
        $event.preventDefault();

        if (val != $scope.Tick) {
            $scope.Tick = val;
            $($event.currentTarget.closest('div')).find('a.btn-tick').removeClass('active');
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
    //#endregion

    //#region General

    $scope.ShowOrder = false;

    $scope.ShowOrder_Click = function ($event) {
        $event.preventDefault();
        $scope.ShowOrder = !$scope.ShowOrder;
        var pane = $($scope.verSplitter.element.children(':last'));
        if ($scope.ShowOrder != true) $scope.verSplitter.collapse(pane);
        else $scope.verSplitter.expand(pane);

    }

    //#endregion

    //#region Timeline vehicle

    $rootScope.Loading.Show("Khởi tạo...");
    $rootScope.Loading.Change("Khởi tạo...", 70);

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

    $scope.VehicleResource = [];
    $scope.VehicleLoadType = 1;
    $scope.FitlerVendorID = -1;
    $scope.TimeLineEventDragDrop = false;
    $scope.SameDate = true;
    $scope.TimeLineLoading = null;
    $scope.DateRequest = { fDate: new Date().addDays(-2), tDate: new Date().addDays(1) }
    //$scope.DateRequest.fDate = new Date("11/14/2016"); $scope.DateRequest.tDate = new Date("11/20/2016");
    $scope.DateTimeRequest = { fDate: $scope.DateRequest.fDate.setHours(0, 0, 0, 0, 0), tDate: $scope.DateRequest.tDate.setHours(0, 0, 0, 0, 0) }
    $scope.FilterTimeline = {
        ListService: [],
        ListCustomer: [],
        ListCarrier: [],
        ListSeaPort: [],
        IsShowMasterPlan: false,
        IsShowOrderNoPlan: false,
    }


    $scope.VehicleLoadTypeName = "Hiện chuyến KH";
    $scope.OrderLoadTypeName = "Đơn đã phân";
    $scope.IsOwnerPlanning = true;

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
            { collapsible: false, resizable: true, min: '500px' }
        ],
        resize: function (e) {
            if ($scope.conSplitter != null && $scope.conSplitter.size($scope.conSplitter.element.children(":first")) != this.size(this.element.children(":first")))
                $scope.conSplitter.size($scope.conSplitter.element.children(":first"), this.size(this.element.children(":first")));
        }
    };

    $scope.conSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: true, size: '450px', min: '300px' },
            { collapsible: false, resizable: true, min: '500px' }
        ],
        resize: function (e) {
            if ($scope.vehSplitter != null && $scope.vehSplitter.size($scope.vehSplitter.element.children(":first")) != this.size(this.element.children(":first")))
                $scope.vehSplitter.size($scope.vehSplitter.element.children(":first"), this.size(this.element.children(":first")));
        }
    };

    $scope.veh_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONCO_TimeLine_Vehicle_List",
            pageSize: 20,
            readparam: function () {
                //$scope.TimeLineLoading = true;
                return {
                    fDate: $scope.DateRequest.fDate,
                    tDate: $scope.DateRequest.tDate,
                    typeOfView: $scope.VehicleLoadType,
                    vendorID: $scope.FitlerVendorID,
                    filter: $scope.FilterTimeline,
                }
            },
            group: [{ field: 'VehicleNo' }],
            model: {
                id: 'Text',
                fields: {
                    Text: { type: 'string' }
                }
            }
        }),
        pageable: { info: true, numeric: false, buttonCount: 0, input: true, messages: { of: "/{0}", page: "", display: "{0}-{1} / {2}" } },
        height: '100%', groupable: false, sortable: true, columnMenu: false, selectable: true, resizable: false, filterable: { mode: 'row' }, reorderable: false, autoBind: false,
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
            $rootScope.Loading.Change("Thông tin chuyến...", 50);
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
                    $rootScope.Loading.Hide();
                });
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
            { field: 'MaxWeight', width: 100, title: 'Trọng tải', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxCapacity', width: 100, title: 'Chuyên chở', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'GroupOfRomoocName', width: 120, title: 'Loại romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', width: 150, title: 'Vị trí', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Lat', width: 120, title: 'Vĩ độ', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Lng', width: 120, title: 'Kinh độ', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    };

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

    $scope.LastTop = 0;
    $scope.LastLeft = 0;
    $scope.RomoocLoadType = 1;

    $scope.LoadDataTimeLine = function (callback) {
        Common.Log("LoadDataTimeLine");

        $rootScope.Loading.Show("Thông tin chuyến...");
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
            url: Common.Services.url.MON,
            method: "MONCO_TimeLine_Schedule_Data",
            data: {
                fDate: $scope.DateRequest.fDate,
                tDate: $scope.DateRequest.tDate,
                dataRes: $scope.VehicleResource,
                typeOfView: $scope.RomoocLoadType,
                filter: $scope.FilterTimeline,
            },
            success: function (res) {
                Common.Data.Each(res.DataSources, function (o) {
                    var i = o.GroupID == o.VehicleID && o.TypeOfGroupID == 1 ? -1 : o.GroupID;
                    o.field = o.TypeOfGroupID + "-" + o.VehicleID + "-" + i;
                    o.EmptyLeft = 0, o.EmptyWidth = 0;
                    if (o.StatusOfTimeSheet == 1) {
                        if (o.ETDEmpty != null && o.ETAEmpty != null) {
                            o.ETDEmpty = Common.Date.FromJson(o.ETDEmpty);
                            o.ETAEmpty = Common.Date.FromJson(o.ETAEmpty);
                            var e = Common.Date.FromJson(o.EndDate) - Common.Date.FromJson(o.StartDate);
                            o.EmptyLeft = (o.ETDEmpty - Common.Date.FromJson(o.StartDate)) * 100 / e;
                            if (o.EmptyLeft < 0)
                                o.EmptyLeft = 0;
                            else if (o.EmptyLeft > 100)
                                o.EmptyLeft = 100;
                            o.EmptyWidth = (o.ETAEmpty - o.ETDEmpty) * 100 / e;
                            if (o.EmptyWidth < 0)
                                o.EmptyWidth = 0;
                            else if (o.EmptyWidth > 100)
                                o.EmptyWidth = 100;
                        }
                    }
                    if (o.StatusOfTimeSheet == 4) {
                        o.EmptyLeft = 0;
                        o.EmptyWidth = 100;
                    }
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

                if (callback) {
                    callback();
                }
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

    $scope.TimeLineEvent_Click = function (e, item) {
        e.preventDefault();
        if (item.TOMasterID > 0 && item.StatusOfEvent > 1 && item.StatusOfEvent != 11) {

            $scope.LoadMasterDetail(item.TOMasterID);

        }
    }

    $scope.Vendor_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'VendorCode', dataValueField: 'VendorID', enable: true, value: -1,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'VendorID', fields: { VendorName: { type: 'string' }, VendorID: { type: 'number' } } }
        }),
        change: function (e) {
            $scope.veh_Grid.dataSource.read();
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "Monitor_VendorList",
        data: {},
        success: function (res) {
            res.Data.unshift({
                VendorID: -1,
                VendorName: 'Xe nhà',
                VendorCode: 'Xe nhà'
            });
            $scope.Vendor_CbbOptions.dataSource.data(res.Data);
        }
    });

    $scope.Vehicle_Filter_Click = function ($event, grid, type) {
        $event.preventDefault();

        var item = grid.dataItem($event.currentTarget.closest('tr')), filter = grid.dataSource.filter();
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

    //#endregion

    //#region Timline Container

    $scope.TimeLineDataService = [];
    $scope.TimeLineDataCarrier = [];
    $scope.TimeLineDataSeaport = [];
    $scope.TimeLineDataCustomer = [];
    $scope.OrderLoadType = 2;

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
                        //$scope.Init_TimeLine_Con_DragDrop();
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

    $scope.timelineCon_Options1 = {
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
                        //$scope.Init_TimeLine_Con_DragDrop();
                        $scope.TimeLineConLoading = false;
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

    $scope.TimeLineOrderPlan_Click = function ($event) {
        $event.preventDefault();
        if ($scope.IsOwnerPlanning == true) {
            $scope.IsOwnerPlanning = false;
            $($event.currentTarget).addClass('active');
            $rootScope.Loading.Show();
            $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
            $scope.con_Grid.dataSource.read();
        }
        else {
            $scope.IsOwnerPlanning = true;
            $($event.currentTarget).removeClass('active');
            $rootScope.Loading.Show();
            $rootScope.Loading.Change("Dữ liệu đơn hàng...", 40);
            $scope.con_Grid.dataSource.read();
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
    //#endregion

    //#region Tooltip
    var _DATATOMASTER = [];
    var _DATATOMASTERINFINITY = [];
    $scope.TimeLineVehicleEventItem = {};
    $scope.TimeLineVehicleInfinityEventItem = {};
    $scope.TimeLineVehicleEventItem = { IsLoaded: false, Item: { ListContainer: [{}] } };
    $scope.TimeLineVehicleEventStyle = { 'display': 'none', 'top': 0, 'left': 0 }
    $scope.TimeLineVehicleInfinityEventItem = { ID: -1, IsLoaded: false, Item: { ListContainer: [] } };
    $scope.TimeLineVehicleInfinityEventStyle = { 'display': 'none', 'top': 0, 'left': 0 }

    $('body > #timeline_vehicle_tooltip').remove();
    $('#timeline_vehicle_tooltip').detach().appendTo('body');
    $('body > #timeline_vehicle_infinity_tooltip').remove();
    $('#timeline_vehicle_infinity_tooltip').detach().appendTo('body');

    $scope.ShowTimeLineTooltip = function ($event, cid, uid, type) {
        if ($scope.TimeLineEventDragDrop == false && type <= 6 && cid > 0) {
            var off = $($event.currentTarget).closest('.cus-event').offset();
            $scope.TimeLineVehicleEventItem = { IsLoaded: false, Item: { ListContainer: [{}] }, StatusOfTimeSheet: type };
            $scope.TimeLineVehicleEventStyle = { 'display': '', 'top': off.top - 183, 'left': $($event.currentTarget).closest('.cus-event').width() / 2 + off.left - 211 };

            if (Common.HasValue(_DATATOMASTER[cid + "_" + uid])) {
                $scope.TimeLineVehicleEventItem = {
                    IsLoaded: true,
                    StatusOfTimeSheet: type,
                    Item: _DATATOMASTER[cid + "_" + uid]
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
                            _DATATOMASTER[cid + "_" + uid] = res;
                            if (res.ListContainer != null && res.ListContainer == null)
                                _DATATOMASTER[cid + "_" + uid].ListContainer = [];
                            $scope.TimeLineVehicleEventItem = {
                                IsLoaded: true, StatusOfTimeSheet: type,
                                Item: _DATATOMASTER[cid + "_" + uid]
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
        $scope.TimeLineVehicleEventItem = { IsLoaded: false, Item: { ListContainer: [{}] } };
        $scope.TimeLineVehicleEventStyle = { 'display': 'none', 'top': 0, 'left': 0 };
    }

    $scope.ShowTimeLineInfinityTooltip = function ($event, cid, uid) {
        var off = $($event.currentTarget).offset();
        $scope.TimeLineVehicleInfinityEventItem = { ID: cid, IsLoaded: false, Item: { ListContainer: [] } };
        $scope.TimeLineVehicleInfinityEventStyle = { 'display': '', 'top': -1000, 'left': $($event.currentTarget).width() / 2 + off.left - 236 };

        if (Common.HasValue(_DATATOMASTERINFINITY[uid])) {
            var l = _DATATOMASTERINFINITY[uid].ListContainer.length;
            if (l == 0) l = 1;
            var h = 50 + 1 * 20;
            $('#timeline_vehicle_infinity_tooltip').height(h);
            $scope.TimeLineVehicleInfinityEventStyle.top = off.top - h - 18;
            $scope.TimeLineVehicleInfinityEventItem = {
                ID: cid,
                IsLoaded: true,
                Item: _DATATOMASTERINFINITY[uid]
            };
        } else {
            $scope.TimeLineVehicleInfinityEventStyle.top = off.top - 168;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV5.URL.TripInfinityTooltipByID,
                data: { masterID: uid },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        _DATATOMASTERINFINITY[uid] = res;
                        if (res.ListContainer != null && res.ListContainer == null)
                            _DATATOMASTERINFINITY[uid].ListContainer = [];
                        var l = _DATATOMASTERINFINITY[uid].ListContainer.length;
                        if (l == 0) l = 1;
                        var h = 50 + 1 * 20;
                        $('#timeline_vehicle_infinity_tooltip').height(h);
                        $scope.TimeLineVehicleInfinityEventStyle.top = off.top - h - 18;
                        $scope.TimeLineVehicleInfinityEventItem = {
                            ID: cid,
                            IsLoaded: true,
                            Item: _DATATOMASTERINFINITY[uid]
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

    //#endregion

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
            + '</div><div class="right">'
            + '<input class="cus-datepicker" style="width:100px;" focus-k-datepicker kendo-date-picker k-ng-model="DateRequest.fDate" k-options="DateDMY" /><span style="color:#fff"> - </span><input class="cus-datepicker" style="width:100px;" focus-k-datepicker kendo-date-picker k-ng-model="DateRequest.tDate" k-options="DateDMY" />'
            + '<a style="color:#fff;" class="k-button" ng-click="DateRequestChange_Click($event)"><i class="fa fa-refresh"></i><span class="tooltip">Làm mới</span></a>'
            + '<a style="color:#fff;" class="k-button" ng-click="TimeLineRomoocType_Click($event)"><i class="fa fa-cubes"></i><span class="tooltip">Chặng phụ</span></a>'
            + '<a style="color:#fff;" class="k-button" ng-click="TimeLineVehicleType_Click($event)"><i class="fa fa-filter"></i><span class="tooltip is-right">{{VehicleLoadTypeName}}</span></a>'
            + '</div></div></div>')
        $compile($($scope.timelineVeh.element).find('.k-scheduler-toolbar'))($scope);

        $($scope.timelineCon.element).find('.k-scheduler-toolbar').children().hide();
        $($scope.timelineCon.element).find('.k-scheduler-toolbar').append('<div class="cus-form thin"><div class="form-header"><div class="right">'
            + '<a href="/" style="color:#fff;" class="k-button" ng-click="TimeLineOrderPlan_Click($event)"><i class="fa fa-stack-overflow"></i><span class="tooltip">Đơn hàng đang chờ</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button active" ng-click="TimeLineOrderType_Click($event)"><i class="fa fa-share-alt"></i><span class="tooltip">{{OrderLoadTypeName}}</span></a>'
            + '<a href="/" style="color:#fff;" class="k-button active" ng-click="TimeLineScrollType_Click($event)"><i class="fa fa-exchange"></i><span class="tooltip  is-right">Cùng thời gian</span></a>'
            + '</div></div></div>')
        $compile($($scope.timelineCon.element).find('.k-scheduler-toolbar'))($scope);

        //$scope.veh_Grid.dataSource.read();
        //$scope.con_Grid.dataSource.read();

        $rootScope.Loading.Hide();
    }, 100)

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

    //#region TOMaster Detail

    $scope.CurrentMaster = { VehicleID: -1, };
    $scope.masterID = 0;
    $scope.TOChangeShowType = 1;
    var LoadingStep = 20;

    //#region -Infomation

    var isChangePlan = false;

    $scope.ChangeTOMaster = function (win, type) {
        if ($scope.Auth.ActEdit) {
            $scope.TOChangeShowType = type;

            if (!$scope.CurrentMaster.IsVehicleVendor) {
                if (type == 1) {
                    if (!$scope.CurrentMaster.IsComplete) {
                        win.open().maximize();
                        $scope.change_romooc_grid.dataSource.read();
                        //thong tin tai xe
                        $timeout(function () {
                            $scope.MONTOUpdate_Splitter.resize();
                        }, 400)
                    }
                    else {
                        $rootScope.Message({
                            Type: Common.Message.Type.ERROR,
                            Msg: 'Không được sửa thông tin chuyến đã hoàn thành',
                        });
                    }
                }
                else {
                    if ($scope.CurrentMaster.IsComplete) {
                        $rootScope.Message({
                            Type: Common.Message.Type.ERROR,
                            Msg: 'Không được sửa thông tin chuyến đã hoàn thành',
                        });
                    }
                    else if (!$scope.CurrentMaster.IsRomoocChangable) {
                        $rootScope.Message({
                            Type: Common.Message.Type.ERROR,
                            Msg: 'Không thể đổi romooc được cắt từ chuyến trước',
                        });
                    }
                    else {

                        win.open().maximize();
                        //thong tin tai xe
                        $timeout(function () {
                            $scope.MONTOUpdate_Splitter.resize();
                        }, 400)
                    }
                }
            }
            else {
                $scope.ListVehicle_Open(type);
            }
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.ERROR,
                Msg: 'Tài khoản không có quyển đổi xe',
            });
        }
    };

    $scope.Driver1_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'DriverName', dataValueField: 'ID', enable: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { DriverName: { type: 'string' }, ID: { type: 'string' } } }
        }),
        select: function (e) {
            var cbb = this;
            var obj = cbb.dataItem(e.item);
            if (Common.HasValue(obj)) {
                $scope.CurrentMaster.Driver1.ID = obj.ID;
                $scope.CurrentMaster.Driver1.Name = obj.DriverName;
                $scope.CurrentMaster.Driver1.Card = obj.CardNo;
                $scope.CurrentMaster.DriverCard1 = obj.CardNo;
                $scope.CurrentMaster.DriverTel1 = obj.Cellphone;

            }
        },
        open: function (e) {
            var res = [{ 'ID': null, 'DriverName': ' ', 'Cellphone': '', 'CardNo': '' }];
            $.each($scope._dataDriver, function (i, elem) {
                var val = parseInt(elem.ID);
                if ($scope.CurrentMaster.Driver2.ID != val && $scope.CurrentMaster.Driver3.ID != val) {
                    res.push(elem);
                }
            })
            res = res.sort(function (a, b) { return a.ID - b.ID; });
            this.dataSource.data(res);
        }
    };

    $scope.Driver2_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'DriverName', dataValueField: 'ID', enable: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { DriverName: { type: 'string' }, ID: { type: 'string' } } }
        }),
        select: function (e) {
            var cbb = this;
            var obj = cbb.dataItem(e.item);
            if (Common.HasValue(obj)) {
                $scope.CurrentMaster.Driver2.ID = obj.ID;
                $scope.CurrentMaster.Driver2.Name = obj.DriverName;
                $scope.CurrentMaster.Driver2.Tel = obj.Cellphone;
                $scope.CurrentMaster.Driver2.Card = obj.CardNo;
            }
        },
        open: function (e) {
            var res = [{ 'ID': null, 'DriverName': ' ', 'Cellphone': '', 'CardNo': '' }];
            $.each($scope._dataDriver, function (i, elem) {
                var val = parseInt(elem.ID);
                if ($scope.CurrentMaster.Driver1.ID != val && $scope.CurrentMaster.Driver3.ID != val) {
                    res.push(elem);
                }
            })
            res = res.sort(function (a, b) { return a.ID - b.ID; });
            this.dataSource.data(res);
        }
    };

    $scope.TypeOfDriver_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Text', dataValueField: 'ValueInt', enable: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ValueInt', fields: { Text: { type: 'string' }, ValueInt: { type: 'string' } } }
        }),
    };

    $scope.To_WinUpdate = function ($event) {
        Common.Log("WinToUpdate");

        var isSuccess = [];

        if ($scope.CurrentMaster.IsVehicleVendor) {
            var dataDriver = $scope.Driver_GridOptions.dataSource.data();
            $scope.CurrentMaster.Driver1 = dataDriver[0];
            $scope.CurrentMaster.Driver2 = dataDriver[1];
            $scope.CurrentMaster.Assistant1 = dataDriver[2];
            $scope.CurrentMaster.Assistant2 = dataDriver[3];
        }

        if ($scope.CurrentMaster.KMStart > $scope.CurrentMaster.KMEnd) {
            isSuccess.push("Số KM kết thúc phải lớn hơn KM bắt đầu");
        }

        if (isSuccess.length == 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_MasterUpdate",
                data: { item: $scope.CurrentMaster },
                success: function (res) {
                    $scope.ShedularLoadData();
                    $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
        else {
            $rootScope.Message({ Msg: isSuccess.join("; "), NotifyType: Common.Message.NotifyType.ERROR });
        }

    }

    $scope.Check_VehicleMaster = function (e) {
        e.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_Check_VehicleMaster",
            data: {
                id: $scope.CurrentMaster.VehicleID,
                romoocID: $scope.CurrentMaster.RomoocID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                if (Common.HasValue(res) && res.ID > 0) {
                    // truong hop mooc khong con gan voi dau keo nua
                    if (res.VehicleID == $scope.CurrentMaster.VehicleID && res.RomoocID != $scope.CurrentMaster.RomoocID) {
                        $rootScope.Message({ Msg: 'Xe đang rảnh!', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                    else {
                        var mess = 'Xe (hoặc romooc) đang chạy cho chuyến ' + res.Code + '. Nhấn đồng ý để xem chi tiết chuyến. ';
                        $rootScope.Message({
                            Type: Common.Message.Type.Confirm,
                            Msg: mess,
                            pars: {},
                            Ok: function (pars) {
                                $scope.LoadMasterDetail(res.ID);
                            }
                        });
                    }
                }
                else {
                    $rootScope.Message({ Msg: 'Xe đang rảnh!', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            }
        })
    }

    //DIMonitor_ListTypeOfDriver
    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "DIMonitor_ListTypeOfDriver",
        data: {},
        success: function (res) {
            $scope.TypeOfDriver_CbbOptions.dataSource.data(res);
        }
    })
    //#endregion

    //#region --Change Vehicle
    $scope.IsWinTOUpdateMax = true;
    $scope.Time_From = new Date();
    $scope.Time_To = null;
    var MapNo = 0;
    var curUIDItem = {};
    var orgUIDItem = {};
    var curRomoocUIDItem = {};
    var orRomoocgUIDItem = {};
    $scope.VendorOfVehicleID = -1;
    $scope.VendorOfRomoocID = -1;

    $scope.MONTOUpdate_SplitterOptions = {
        panes: [
                { collapsible: true, resizable: true, size: '50%' },
                { collapsible: true, resizable: true, size: '50%' }
        ],
        resize: function (e) {
            if (Common.HasValue(openMapV2))
                openMapV2.Resize();
        }
    };

    $scope.TOUpdate_winOptions = {
        width: '1025px', height: '550px',
        draggable: true, modal: true, resizable: false, title: false,
        close: function () {
            //openMapV2.Active(MainMap);
            MapNo = 1;
        },
        open: function () {
            openMapV2.Active(TOMap);
            MapNo = 2;
            $scope.Time_From = $scope.CurrentMaster.DateFrom;
            $scope.Time_To = $scope.CurrentMaster.DateTo;
            $scope.vehicle_grid.dataSource.read();
            $scope.change_romooc_grid.dataSource.read();
        },
        resize: function () {
            $timeout(function () {
                $scope.MONTOUpdate_Splitter.resize();
            }, 400)
        }
    };

    $scope.Win_Max = function (e, win) {
        e.preventDefault();
        win.maximize();
        $scope.IsWinTOUpdateMax = true;
    };
    $scope.Win_Min = function (e, win) {
        e.preventDefault();
        win.restore();
        win.center();
        $scope.IsWinTOUpdateMax = false;
        $timeout(function () {
            $scope.MONTOUpdate_Splitter.resize();
        }, 300)
    };

    $scope.SearchVehicle = function (e, grid) {
        e.preventDefault();
        grid.dataSource.read();
    };

    $scope.vehicle_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TractorRead",
            readparam: function () {
                return {
                    DateFrom: Common.Date.Date($scope.Time_From),
                    DateTo: Common.Date.Date($scope.Time_To),
                    vendorID: null,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        toolbar: kendo.template($('#vehicle-grid-toolbar').html()),
        columns: [
           {
               title: ' ', width: '40px',
               template: '<input disabled class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" #" />',
               templateAttributes: { style: 'text-align: center;' },
               filterable: false, sortable: false
           },
           {
               field: '', width: 70,
               template: '<a ng-click="PickVehicle($event,dataItem)" class="k-button" >Chọn</a>',
               title: '',filterable: false
           },
           {
               field: 'RegNo', width: 150,
               title: '{{RS.CATVehicle.RegNo}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'MaxWeight', width: 100,
               title: '{{RS.CATGroupOfVehicle.Ton}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Status', width: 100,
               title: '{{RS.MONMonitorIndex.WarningCount}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            var data = grid.dataSource.data();
            if (MapNo == 2) {
                $scope.DrawNewMarker(grid.dataSource.data(), function (item) {
                    return openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Truck), 1);
                }, "VectorXe", true, 'ID', 'RegNo');
                for (var i = 0; i < data.length; i++) {
                    if ($scope.CurrentMaster.VehicleID == data[i].ID) {
                        var o = grid.dataItem("[data-uid='" + data[i].uid + "']");
                        o.IsChoose = true;
                        curUIDItem = data[i];
                        orgUIDItem = data[i];
                        var tr = grid.tbody.find("tr[data-uid='" + data[i].uid + "']");
                        $(tr.find('.chkChoose')).prop('checked', 'checked');
                    }
                }
            }
        }
    };

    $scope.SaveVehicle = function (e) {
        if (Common.HasValue(e))
            e.preventDefault();
        if (orgUIDItem.VehicleID != curUIDItem.ID) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận đổi đầu kéo?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTower_ChangeTractor",
                        data: {
                            masterID: $scope.masterID,
                            vehicleID: curUIDItem.ID
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.TOUpdate_win.close();
                            $scope.LoadMasterDetail($scope.masterID);
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
    };

    $scope.PickVehicle = function (e, item) {
        e.preventDefault();
        if (curUIDItem.uid != item.uid) {
            var t = $scope.vehicle_grid.dataItem("[data-uid='" + item.uid + "']");
            t.IsChoose = true;
            var tr = $scope.vehicle_grid.tbody.find("tr[data-uid='" + item.uid + "']");
            $(tr.find('.chkChoose')).prop('checked', 'checked');
            var o = $scope.vehicle_grid.dataItem("[data-uid='" + curUIDItem.uid + "']");
            if (Common.HasValue(o))
                o.IsChoose = false;
            tr = $scope.vehicle_grid.tbody.find("tr[data-uid='" + curUIDItem.uid + "']");
            $(tr.find('.chkChoose')).prop('checked', false);
            curUIDItem = item;
        }
        $scope.SaveVehicle();
    }

    $scope.change_romooc_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_RomoocRead",
            readparam: function () {
                return {
                    DateFrom: Common.Date.Date($scope.Time_From),
                    DateTo: Common.Date.Date($scope.Time_To),
                    vendorID: null,
                    vehicleid: $scope.CurrentMaster.VehicleID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        toolbar: kendo.template($('#change-romooc-grid-toolbar').html()),
        columns: [
           {
               title: ' ', width: '40px',
               template: '<input disabled class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" #" />',
               templateAttributes: { style: 'text-align: center;' },
               filterable: false, sortable: false
           },
           {
               field: '', width: 70,
               template: '<a ng-click="PickRomooc($event,dataItem)" class="btn-grid-pick">Chọn</a>',
               title: '',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'RegNo', width: 150,
               title: '{{RS.CATRomooc.RegNo}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'MaxWeight', width: 100,
               title: '{{RS.CATGroupOfVehicle.Ton}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Status', width: 100,
               title: '{{RS.MONMonitorIndex.WarningCount}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            var data = grid.dataSource.data();
            if (MapNo == 2) {
                $scope.DrawNewMarker(grid.dataSource.data(), function (item) {
                    return openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Truck), 1);
                }, "VectorXe", true, 'ID', 'RegNo');
                for (var i = 0; i < data.length; i++) {
                    if ($scope.CurrentMaster.RomoocID == data[i].ID) {
                        var o = grid.dataItem("[data-uid='" + data[i].uid + "']");
                        o.IsChoose = true;
                        curRomoocUIDItem = data[i];
                        orgRomoocUIDItem = data[i];
                        var tr = grid.tbody.find("tr[data-uid='" + data[i].uid + "']");
                        $(tr.find('.chkChoose')).prop('checked', 'checked');
                    }
                }
            }
            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var item = grid.dataItem($(tr));
                if (item.SortOrder >= 0) {
                    $(tr).css("background-color", "#0750f3");
                    angular.forEach($(tr).find('td'), function (td, i) {
                        $(td).css("color", "#fff");
                    })
                }
            });
        }
    };

    $scope.PickRomooc = function (e, item) {
        e.preventDefault();
        if (curRomoocUIDItem.uid != item.uid) {
            var t = $scope.change_romooc_grid.dataItem("[data-uid='" + item.uid + "']");
            t.IsChoose = true;
            var tr = $scope.change_romooc_grid.tbody.find("tr[data-uid='" + item.uid + "']");
            $(tr.find('.chkChoose')).prop('checked', 'checked');
            var o = $scope.change_romooc_grid.dataItem("[data-uid='" + curRomoocUIDItem.uid + "']");
            if (Common.HasValue(o))
                o.IsChoose = false;
            tr = $scope.change_romooc_grid.tbody.find("tr[data-uid='" + curRomoocUIDItem.uid + "']");
            $(tr.find('.chkChoose')).prop('checked', false);
            curRomoocUIDItem = item;
        }
        $scope.SaveChangeRomooc();
    }

    $scope.SaveChangeRomooc = function (e) {
        if (Common.HasValue(e))
            e.preventDefault();
        if ($scope.CurrentMaster.RomoocID != curRomoocUIDItem.ID) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận đổi romooc ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTower_ChangeRomooc",
                        data: {
                            masterID: $scope.masterID,
                            vehicleID: curRomoocUIDItem.ID
                        },
                        success: function (res) {
                            $scope.TOUpdate_win.close();
                            $rootScope.IsLoading = false;
                            $scope.LoadMasterDetail($scope.masterID);
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
    };


    openMapV2.hasMap = true;
    var TOMap = openMapV2.Init({
        Element: 'MON_UpdateMap',
        Tooltip_Show: true,
        Tooltip_Element: 'MON_Map_tooltip',
        InfoWin_Show: true,
        InfoWin_Element: 'Map_Info_Win',
        DefinedLayer: [{
            Name: 'VectorMarker',
            zIndex: 100
        }, {
            Name: 'VectorProblem',
            zIndex: 100
        }, {
            Name: 'VectorXe',
            zIndex: 100
        }, {
            Name: 'VectorRoute',
            zIndex: 90
        }, {
            Name: 'VectorProvince',
            zIndex: 80
        }],
        ClickMarker: function (o, l) {
            if (Common.HasValue(o.TypeOfRouteProblemID)) {
                $scope.MarkerType = "problem";
                $scope.MarkerItemBind = o;
            }
            else if (Common.HasValue(o.LocationID)) {
                $scope.MarkerType = "route";
                $scope.MarkerItemBind = o;
            }
            else if (Common.HasValue(o.RegNo)) {
                $scope.MarkerType = "xe";
                $scope.MarkerItemBind = o;
            }
            else
                openMapV2.Close();
        },
        ClickMap: function () {
            openMapV2.Close();
        }
    });

    //#endregion

    //#region -ORD Container Detail

    var ordContainerID = 0;
    var lstIgnore = [];

    $scope.COContainer_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_COList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                    OrderCode: { editable: false },
                    PackingCode: { editable: false },
                    LocationFromCode: { editable: false },
                    ReasonChangeName: { editable: false },
                    ReasonChangeNote: { editable: false },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: 'inline', autoBind: false,
        //toolbar: kendo.template($('#co-container-grid-toolbar').html()),
        save: function (e) {
            e.preventDefault();
            if (!$scope.CurrentMaster.IsComplete) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTowerCO_COEdit",
                    data: {
                        item: e.model,
                    },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.COContainer_Grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
            else {
                $rootScope.Message({ Msg: 'Chuyến đã hoàn thành không được chỉnh sửa', NotifyType: Common.Message.NotifyType.ERROR });
            }
        },
        columns: [
            {
                width: 73,
                command: [{
                    name: "edit", text: { edit: "", cancel: "", update: "" }
                }]
            },
            { field: '', width: 40, title: '', template: '<a ng-hide="CurrentMaster.IsComplete" ng-click="ChangeDepot_Open($event,DepotWin,dataItem.ID)" class="k-button"><i class="fa fa-map-marker"></i></a>', },
            { field: 'OrderCode', width: 150, title: '{{RS.ORDOrder.Code}}', },
            { field: 'PackingCode', width: 150, title: '{{RS.CATPacking.Code}}', },
            { field: 'ContainerNo', width: 150, title: '{{RS.ORDOrder.ContainerNo}}', },
            { field: 'Ton', width: 150, title: '{{RS.ORDContainer.Ton}}', },
            { field: 'SealNo1', width: 150, title: '{{RS.OPSContainer.SealNo1}}', },
            { field: 'SealNo2', width: 150, title: '{{RS.OPSContainer.SealNo2}}', },
            { field: 'DepotCode', width: 150, title: '{{RS.MONMonitorIndex.DepotCode}}', },
            { field: 'DepotAddress', width: 150, title: '{{RS.MONMonitorIndex.DepotAddress}}', },
            { field: 'DepotReturnCode', width: 150, title: '{{RS.MONMonitorIndex.DepotReturnCode}}', },
            { field: 'DepotReturnAddress', width: 150, title: '{{RS.MONMonitorIndex.DepotReturnAddress}}', },
            { field: 'Note1', width: 100, title: '{{RS.ORDContainer.Note1}}', },
            { field: 'Note2', width: 100, title: '{{RS.ORDContainer.Note2}}', },
            { field: 'ReasonChangeName', width: 150, title: '{{RS.CATReason1.ReasonName}}', },
            { field: 'ReasonChangeNote', width: 150, title: '{{RS.CATReason.ReasonChangeNote}}', },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            if (isShowDetail) {
                $rootScope.Loading.Show("Thông tin đơn hàng...");
                $rootScope.Loading.Change("Thông tin đơn hàng...", 20);
                $scope.TOContainer_Grid.dataSource.read();
            }
        }
    };

    $scope.COContainer_GridChange = function () {

    }

    $scope.CatLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "CATLocation_List",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
            readparam: function () { return { ignore: lstIgnore } }
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: false, editable: false, selectable: true,
        columns: [

           {
               field: 'Code', width: 150,
               title: '{{RS.CATLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Location', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        change: function () {
            var select = this.select();
            if (select.length > 0) {
                var item = this.dataItem(select[0]);
                $scope.COTOStopLocationID = item.ID;
                $scope.COTOStopLocationName = "[" + item.Code + "]" + item.Address;
            }
        }
    };

    var isFisrtTime = true;
    var lstORDCOID = [];

    $scope.ListCatLocation_Open = function (e, type) {
        e.preventDefault();
        $scope.ContLocaitonType = type;
        if (isFisrtTime) {
            isFisrtTime = false;
            $scope.CatLocation_Grid.dataSource.read();
        }

        lstORDCOID = [];
        angular.forEach($scope.COContainer_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose) {
                lstORDCOID.push(o.ID)
            }
        })
        if (lstORDCOID.length == 0)
            $rootScope.Message({ Msg: 'Chọn ít nhất một dòng', NotifyType: Common.Message.NotifyType.ERROR });
        else if (lstORDCOID.length > 1 && type == 2)
            $rootScope.Message({ Msg: 'Chỉ được chọn 1 container để trả rỗng', NotifyType: Common.Message.NotifyType.ERROR });
        else
            $scope.CatLocaiton_Win.center().open();

    };

    $scope.ChooseLocation_Accept = function (e, win) {
        e.preventDefault();
        if ($scope.ContLocaitonType == 7) {
            var selectedRows = $scope.CatLocation_Grid.select();
            if (selectedRows.length > 0) {
                var dataItem = $scope.CatLocation_Grid.dataItem(selectedRows[0]);
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_ControlTowerCO.URL.MONCO_End,
                    data: {
                        opscontainerid: dataRow.id,
                        locationID: dataItem.ID,
                    },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.CatLocaiton_Win.close();
                        $scope.TODetail_ReloadAllGrid();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        }
        else
            win.center().open();
    }

    $scope.COContainerRepair_Click = function (e, win, grid, vform) {
        var selectedRows = grid.select();
        var lstMooc = [];
        if (selectedRows.length > 0 && vform()) {
            var dataItem = grid.dataItem(selectedRows[0]);

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận đi sửa container ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_RepairContainer",
                        data: {
                            masterID: $scope.masterID,
                            lst: lstORDCOID,
                            locationID: dataItem.ID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            win.close();
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
    };

    $scope.COContainerCut_Click = function (e, win, grid, vform) {
        e.preventDefault();
        var selectedRows = grid.select();

        if (selectedRows.length > 0 && vform()) {
            var dataItem = grid.dataItem(selectedRows[0]);
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hủy chặng còn lại ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_CutContainer",
                        data: {
                            masterID: $scope.masterID,
                            locationID: dataItem.ID,
                            lst: lstORDCOID,
                            containerNo: $scope.ReasonChangeContainerNo,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
        win.close();
    };

    $scope.ReasonChangeCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'Text',
        dataValueField: 'ValueInt',
        dataSource: Common.DataSource.Local({ data: [] })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "MONControlTower_ReasonChange",
        data: {},
        success: function (res) {
            if (res.length > 0) {
                $scope.ItemSave.FirstReason = res[0];
                $scope.ReasonChangeID = res[0].ValueInt;
            }
            $scope.ReasonChangeCbb.dataSource.data(res);
        }
    })

    //#endregion

    //#region --Change Depot

    var DepotMap = openMapV2.Init({
        Element: 'DepotMap',
        Tooltip_Show: false,
        Tooltip_Element: 'MON_Map_tooltip',
        InfoWin_Show: false,
        InfoWin_Element: 'Map_Info_Win',
        DefinedLayer: [{
            Name: 'VectorMarker',
            zIndex: 100
        }],
        ClickMarker: function (o, l) {
            $scope.Depot_Grid.select("tr[data-uid='" + o.uid + "']");
        },
        ClickMap: function (res) {

        }
    });
    var PrevMarker = null;
    $scope.TOContainerID = 0;

    $scope.DepotWinOptions = {
        width: '800', height: '640',
        draggable: true, modal: true, resizable: false, title: false, visible: false,
        close: function () {
            //openMapV2.Active(MainMap);
            MapNo = 1;
        },
        open: function () {
            openMapV2.Active(DepotMap);
            MapNo = 4;
            $timeout(function () {
                $scope.DepotSplitter.resize();
            }, 400)
        },
        resize: function () {
            $timeout(function () {
                $scope.DepotSplitter.resize();
            }, 400)
        }
    };

    $scope.DepotSplitter_Options = {
        panes: [
                { collapsible: false, resizable: false, size: '50%' },
                { collapsible: false, resizable: false, size: '50%' }
        ],
        resize: function (e) {
            if (Common.HasValue(openMapV2))
                openMapV2.Resize();
        }
    };

    $scope.Depot_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_DepotList",
            readparam: function () {
                return {
                    opscontainerID: $scope.TOContainerID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        dataBound: function () {
            var grid = this;
            var data = grid.dataSource.data();
            if (MapNo == 4) {
                openMapV2.ClearVector("VectorMarker");
                angular.forEach(data, function (o, i) {
                    var icon = openMapV2.NewStyle.Icon('/Images/map/icon/blue/ico_empty.png', 1);
                    openMapV2.NewMarker(o.Lat, o.Lng, 'ID' + o.ID, o.Location, icon, o, 'VectorMarker');

                });
                openMapV2.FitBound('VectorMarker');
            }
        },
        change: function () {
            var grid = this;
            var selectedRows = grid.select();
            if (selectedRows.length > 0) {
                //tra lai mau cho diem cu
                if (PrevMarker != null) {
                    openMapV2.ClearFeature("VectorMarker", 'ID' + PrevMarker.ID);
                    var icon = openMapV2.NewStyle.Icon('/Images/map/icon/blue/ico_empty.png', 1);
                    openMapV2.NewMarker(PrevMarker.Lat, PrevMarker.Lng, 'ID' + PrevMarker.ID, PrevMarker.Location, icon, PrevMarker, 'VectorMarker');
                }
                //ve diem moi
                var dataItem = grid.dataItem(selectedRows[0]);
                PrevMarker = dataItem;
                openMapV2.ClearFeature("VectorMarker", 'ID' + dataItem.ID);
                var icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_empty.png', 1);
                openMapV2.NewMarker(dataItem.Lat, dataItem.Lng, 'ID' + dataItem.ID, dataItem.Location, icon, dataItem, 'VectorMarker');
                openMapV2.Center(dataItem.Lat, dataItem.Lng, 10);
            }
        },
        toolbar: kendo.template($('#depot-grid-toolbar').html()),
        columns: [
           {
               field: 'Code', width: 100, title: '{{RS.CATLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Location', width: 100, title: '{{RS.CATLocation.LocationName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 100, title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'ProvinceName', width: 100, title: '{{RS.CATProvince.ProvinceName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DistrictName', width: 100, title: '{{RS.CATDistrict.DistrictName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.ChangeDepot_Accept = function (e, win, grid) {
        e.preventDefault();
        var selectedRows = grid.select();
        if (selectedRows.length > 0) {
            var dataItem = grid.dataItem(selectedRows[0]);
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_ChangeDepot",
                data: {
                    masterID: $scope.masterID,
                    opscontainerID: $scope.TOContainerID,
                    cuslocationID: dataItem.ID,
                    reasionID: $scope.ReasonChangeID,
                    reasonNote: $scope.ReasonChangeNote
                },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.TODetail_ReloadAllGrid();
                    win.close();
                }
            });
        }
    }

    $scope.ChangeDepot_Open = function (e, win, id) {
        e.preventDefault();
        $scope.TOContainerID = id;
        win.center().open();
        $scope.Depot_Grid.dataSource.read();
    }
    //#endregion

    //#region -COTOContainer

    var dataRow = null;
    $scope.TOContainerID = 0;
    $scope.LocationDepotName = "";
    $scope.IsAvailable = false;
    $scope.MenuItem = {};

    $scope.SLMenuOptions = function (sort) {
        //var dir = 'bottom';
        //if (sort > 7)
        //dir = 'top';
        return {
            openOnClick: true,
            //direction: dir,
            animation: false
        }
    }

    $scope.TOContainer_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TOContainerList",
            readparam: function () {
                return {
                    masterID: $scope.masterID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ColorClass: { type: 'string' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                    IsAvailable: { type: 'bool' },
                    COTOSort: { type: 'number' },
                    Ton: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true, autoBind: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        change: function (e) {
        },
        columns: [
            {
                field: '', width: 60, title: '',
                template: function (dataItem) {
                    if (dataItem.IsStart && !dataItem.IsVendor) {
                        if (dataItem.IsFirstTO) {
                            return $('#to-start-container-tpl').html();
                        }
                        else {
                            return "";
                        }
                    }
                    else {
                        if (dataItem.ShowAction) {
                            return $('#config-cotocontainer-tpl').html();
                        }
                        else
                            return "";
                    }
                },
                attributes: {
                    style: "overflow: visible;border-right: none;"
                },
                filterable: false
            },
            {
                field: 'COTOSort', width: 60, sortable: false, title: 'STT',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'OrderCode', width: 100,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', width: 100,
                title: '{{RS.ORDOrder.ContainerNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100,
                title: '{{RS.OPSCOTOMaster.RomoocNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrder', width: 100,
                title: '{{RS.ORDOrder.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfCOContainer', width: 100,
                title: '{{RS.OPSCOTOContainer.StatusOfCOContainer}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContainer', width: 100,
                title: '{{RS.CATPacking.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfStatusContainerName', width: 100,
                title: '{{RS.SYSVar1.ValueOfVar}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', width: 100,
                title: '{{RS.ORDContainer.Ton}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'COTOKM', width: 80,
                title: '{{RS.OPSCOTO.KM}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: 150,
                title: '{{RS.ORDOrder.TextFrom}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 150,
                title: '{{RS.ORDOrder.TextTo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            $scope.IsAvailable = false;
            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var item = grid.dataItem($(tr));
                if (item.IsAvailable && item.ShowAction) {
                    $scope.IsAvailable = true;
                }
                if (item.IsRunning) {
                    $(tr).css("background-color", "#F28126");
                    angular.forEach($(tr).find('td'), function (td, i) {
                        $(td).css("color", "#000");
                    })
                }
                if (item.IsCompleteCO) {

                    $(tr).css("background-color", "#73C95F");
                    angular.forEach($(tr).find('td'), function (td, i) {
                        $(td).css("color", "#000");
                    })
                }
            });
            isCutRomooc = false;
            if (isShowDetail) {
                $rootScope.Loading.Change("Thông tin chặng...", 40);
                $scope.Station_Grid.dataSource.read();
            }
        }
    };

    $scope.COTOContainerMenuList_Open = function (e, item) {
        e.preventDefault();
        $scope.MenuItem = item;
        $scope.OPSCOTOContainerGridMenuList.center().open();
    }

    $scope.CatLocation_SeaportGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "CATLocation_SeaPortList",
            readparam: function () {
                return {
                    opsTOContainer: $scope.TOContainerID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: false, editable: false, selectable: true,
        columns: [

           {
               field: 'Code', width: 150,
               title: '{{RS.CATLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Location', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.COTOContainer_LocationOpen = function (e, type) {
        if (Common.HasValue(e))
            e.preventDefault();
        $scope.OPSCOTOContainerGridMenuList.close();
        $scope.ContLocaitonType = type;
        dataRow = $scope.MenuItem;
        lstIgnore = [];
        if (isFisrtTime) {
            isFisrtTime = false;
            $scope.CatLocation_Grid.dataSource.read();
        }
        if (type == 6) {
            if (Common.HasValue($scope.ItemSave.FirstReason))
                $scope.ReasonChangeID = $scope.ItemSave.FirstReason.ValueInt;
            $scope.COTOIsContinue = true;
            $scope.ReasonChange_Win.center().open();
        }
        else if (type == 5) {
            //
            if (Common.HasValue($scope.ItemSave.FirstReason))
                $scope.ReasonChangeID = $scope.ItemSave.FirstReason.ValueInt;

            $scope.LocationDepotID = dataRow.LocationDepotID;
            $scope.LocationDepotName = "[" + dataRow.LocationDepotCode + "] " + dataRow.LocationDepotAddress;
            $scope.COTOIsContinue = true;
            $scope.TOContainerID = dataRow.ID;
            $scope.COTOHas2Container = dataRow.Has2Container;
            $scope.ReasonChange_Win.center().open();
        }
        else {
            lstIgnore.push(dataRow.LocationFromID);
            lstIgnore.push(dataRow.LocationToID);
            $scope.CatLocation_Grid.dataSource.read();
            $scope.CatLocaiton_Win.center().open();
        }
    };

    $scope.LocationSeapot_Open = function (e) {
        e.preventDefault();
        $scope.CatLocation_SeaportGrid.dataSource.read();
        $scope.CatLocaiton_SeaportWin.center().open();
    };

    $scope.LocationSeapot_Acccept = function (e, grid, win) {
        e.preventDefault();
        var selectedRows = grid.select();
        if (selectedRows.length > 0) {
            var dataItem = grid.dataItem(selectedRows[0]);
            $scope.LocationDepotID = dataItem.ID;
            $scope.LocationDepotName = "[" + dataItem.Code + "]" + dataItem.Address;
            win.close();
        }
    };

    //#region Start offer

    $scope.COTOContainer_Start = function (e, item) {
        e.preventDefault();
        $rootScope.IsLoading = true;
        $scope.MenuItem = item;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONCO_TOContainer_StartOffer",
            data: {
                opsTOContainer: item.ID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                //res = 5;
                switch (res) {
                    case 1:
                        $rootScope.Message({
                            Type: Common.Message.Type.Alert,
                            Msg: 'Phải chạy chuyến trước của đầu hoặc đơn hàng',
                        });
                        break;
                    case 2:
                        $scope.CTConfirm({
                            Lable: "Bạn muốn dùng lại rờ mooc ?",
                            OK: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.MON,
                                    method: "MONCO_TOContainer_Start",
                                    data: {
                                        opsTOContainer: item.ID,
                                        isChangeRomooc: true,
                                    },
                                    success: function (res) {
                                        $rootScope.IsLoading = false;
                                        $scope.TODetail_ReloadAllGrid();
                                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    }
                                });
                            },
                            Deny: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.MON,
                                    method: "MONCO_TOContainer_Start",
                                    data: {
                                        opsTOContainer: item.ID,
                                        isChangeRomooc: false,
                                    },
                                    success: function (res) {
                                        $rootScope.IsLoading = false;
                                        $scope.TODetail_ReloadAllGrid();
                                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    }
                                });
                            }
                        })
                        break;
                    case 3:
                        $rootScope.Message({
                            Type: Common.Message.Type.Alert,
                            Msg: 'Chưa thiết lập bãi đầu kéo',
                        });
                        break;
                    case 4:
                        $rootScope.Message({
                            Type: Common.Message.Type.Alert,
                            Msg: 'Chưa thiết lập bãi mooc',
                        });
                        break;
                    case 5:
                        $scope.OrderEXIMGrid.dataSource.read();
                        $scope.OrderLocalOfferGrid.dataSource.read();
                        $scope.StartOfferWin.center().open();
                        break;
                    default:
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: "MONCO_TOContainer_Start",
                            data: {
                                opsTOContainer: item.ID,
                                isChangeRomooc: false,
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.TODetail_ReloadAllGrid();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            }
                        });
                        break;
                }
            }
        })
    };

    $scope.OrderEXIMGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OrderEXIM",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
            readparam: function () { return { opsTOContainer: $scope.MenuItem.ID, } }
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: false, editable: false, selectable: true, autoBind: false,
        columns: [
           {
               field: 'COTOSort', width: 150,
               title: 'STT',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'OrderCode', width: 150,
               title: '{{RS.ORDOrder.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationFromCode', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationFromAddress', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },

           {
               field: 'LocationToCode', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToAddress', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.OrderLocalOfferGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OrderLocalOffer",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    COTOSort: { type: 'string' },
                    OrderCode: { type: 'string', editable: false },
                    StatusOfCOContainer: { type: 'string', editable: false },
                    LocationFromCode: { type: 'string', editable: false },
                    LocationFromAddress: { type: 'string', editable: false },
                    LocationToCode: { type: 'string', editable: false },
                    LocationToAddress: { type: 'string', editable: false },
                }
            },
            readparam: function () { return { opsTOContainer: $scope.MenuItem.ID, } }
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: false, editable: true, selectable: true, autoBind: false,
        columns: [
           {
               field: 'COTOSort', width: 50,
               title: 'STT',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'OrderCode', width: 150,
               title: '{{RS.ORDOrder.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'StatusOfCOContainer', width: 150,
               title: 'Chặng',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationFromCode', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationFromAddress', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToCode', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToAddress', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.StartOfferData = function (e) {
        e.preventDefault();
        var ListLocal = [];
        angular.forEach($scope.OrderLocalOfferGrid.dataSource.data(), function (o, i) {
            ListLocal.push({ SortOrder: o.COTOSort, ID: o.ID });
        });

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận đồng ý chạy ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONCO_TOContainer_StartOfferData",
                    data: {
                        opsTOContainer: $scope.MenuItem.ID,
                        item: { ListLocal: ListLocal, ListDuplicate: [], },
                    },
                    success: function (res) {
                        $scope.TODetail_ReloadAllGrid();
                        $scope.StartOfferWin.close();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
        });
    }
    //#endregion


    $scope.ReasonChangeNote = "";
    $scope.COTOHas2Container = false;
    $scope.COTOStopLocationID = 0;
    $scope.COTOStopLocationName = "";

    $scope.COTOContainerStop_Click = function (e, win, grid, vform) {
        e.preventDefault();

        if ($scope.COTOStopLocationID > 0 && vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận tạo điêm dừng ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ContainerStop",
                        data: {
                            opsTOContainer: $scope.MenuItem.ID,
                            locationID: $scope.COTOStopLocationID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
        win.close();
    };

    $scope.COTOContainerRepair_Click = function (e, win, grid, vform) {
        e.preventDefault();
        var selectedRows = grid.select();

        if (selectedRows.length > 0 && vform()) {
            var dataItem = grid.dataItem(selectedRows[0]);
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận tạo điểm sửa container ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ContainerRepair",
                        data: {
                            opsTOContainer: dataRow.ID,
                            locationID: dataItem.ID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
        win.close();
    };

    $scope.COTOContainerDestroyEmpty_Click = function (e, win, grid, vform) {
        e.preventDefault();
        if ($scope.COTOIsContinue)
            vehicleID = $scope.CurrentMaster.VehicleID;
        if ($scope.LocationDepotID > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận trả container empty?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ContainerCorrupt",
                        data: {
                            opsTOContainer: dataRow.ID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                            locationID: $scope.LocationDepotID,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn cảng biển', NotifyType: Common.Message.NotifyType.ERROR });
        }

        win.close();
    };

    var isCutRomooc = false;
    $scope.COTOContainerCutRomooc_Click = function (e, win, grid, vform) {
        e.preventDefault();
        var selectedRows = grid.select();
        var vehicleID = null;
        if ($scope.COTOIsContinue)
            vehicleID = $scope.CurrentMaster.VehicleID;
        var dataItem = grid.dataItem(selectedRows[0]);
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận cắt romooc ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                isCutRomooc = false;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTowerCO_ContainerCutRomooc",
                    data: {
                        opsTOContainer: dataRow.ID,
                        reasonID: $scope.ReasonChangeID,
                        reasonNote: $scope.ReasonChangeNote,
                    },
                    success: function (res) {
                        isCutRomooc = true;
                        $scope.CatLocaiton_Win.close();
                        $scope.TODetail_ReloadAllGrid();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
        });

        win.close();
    };

    $scope.ItemSave = {
        isContinue: false,
        isPause: false,
        isCutRomooc: false,
        stopHour: 0,
        ReasonID: 0,
        ReasonNote: '',
        ContainerNO: "",
        SealNo1: "",
        SealNo2: "",
    };
    $scope.COTOContainer_ChangeStatusOpen_Click = function (e) {

        var item = $scope.MenuItem
        e.preventDefault();
        $scope.ItemSave.isCutRomooc = false;
        $scope.ItemSave.IsRunning = item.IsRunning;
        $scope.ItemSave.opsTOContainer = item.ID;
        $scope.ItemSave.vehicleID = null;
        if ($scope.ItemSave.isContinue)
            $scope.ItemSave.vehicleID = $scope.CurrentMaster.VehicleID;

        if (Common.HasValue($scope.ItemSave.FirstReason))
            $scope.ItemSave.ReasonID = $scope.ItemSave.FirstReason.ValueInt;

        if (item.IsRunning) {
            $scope.ItemSave.ContainerNo = item.ContainerNo;
            $scope.ItemSave.SealNo1 = item.SealNo1;
            $scope.ItemSave.SealNo2 = item.SealNo2;
            $scope.ItemSave.IsOnShip = item.IsOnShip;
            $scope.ItemSave.IsReturnEmpty = item.IsReturnEmpty;
            $scope.TOContStatusChange_Win.center().open();
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận đổi trạng thái ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ContainerStatusChange",
                        data: {
                            opsTOContainer: $scope.ItemSave.opsTOContainer,
                            reasonID: $scope.ItemSave.ReasonID,
                            reasonNote: $scope.ItemSave.ReasonNote,
                            vehicleID: $scope.ItemSave.vehicleID,
                            isCutRomooc: $scope.ItemSave.isCutRomooc,
                            isPause: $scope.ItemSave.isPause,
                            stopHour: $scope.ItemSave.stopHour,
                            containerNo: $scope.ItemSave.ContainerNo,
                            sealNo1: $scope.ItemSave.SealNo1,
                            sealNo2: $scope.ItemSave.SealNo2,
                        },
                        success: function (res) {
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }

    }

    //Hoàn thành
    $scope.COTOContainer_FastCompleteCO = function (e) {
        Common.Log("COTOContainer_FastCompleteCO");
        $scope.OPSCOTOContainerGridMenuList.close();
        dataRow = $scope.MenuItem
        if (dataRow.ID > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh đến chặng này ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ContainerCompleteCOList",
                        data: { id: dataRow.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Hãy chọn chặng chưa hoàn thành!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    //Hủy hoàn thành
    $scope.COTOContainer_UnCompleteCO = function (e) {
        Common.Log("COTOContainer_UnCompleteCO");
        $scope.OPSCOTOContainerGridMenuList.close();
        dataRow = $scope.MenuItem
        if (dataRow.ID > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hủy hoàn thành chặng này ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_UnComplete",
                        data: { id: dataRow.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
    }


    //Đổi kho bãi Open
    var _changeType = 0;
    $scope.ChangeCOTOLocation = function (e, type) {
        e.preventDefault();
        $scope.OPSCOTOContainerGridMenuList.close();
        _changeType = type;
        if (Common.HasValue($scope.ItemSave.FirstReason)) {
            $scope.ReasonChangeID = $scope.ItemSave.FirstReason.ValueInt;
            $scope.ReasonChangeNote = "";
            switch (_changeType) {
                case 1:
                case 2:
                case 3:
                case 4:
                    $scope.LocationDepotID = $scope.MenuItem.LocationDepotID;
                    $scope.LocationDepotName = "[" + $scope.MenuItem.LocationDepotCode + "] " + $scope.MenuItem.LocationDepotAddress;
                    break;
                case 5:
                    $scope.LocationDepotID = 0;
                    $scope.LocationDepotName = "";
                    break;
            }
            $scope.ReasonChangeLocation_Win.center().open();
        }
        else {
            $rootScope.Message({ Msg: 'Hệ thống chưa thiết lập lý do', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    // Accept
    $scope.ReasonAccept = function (e, vform) {
        $scope.ReasonChangeLocation_Win.close();
        switch (_changeType) {
            case 1:
                $scope.COTOContainer_ChangeDepotGet_Click(e, vform);
                break;
            case 2:
                $scope.COTOContainer_AddDepotGet_Click(e, vform);
                break;
            case 3:
                $scope.COTOContainer_ChangeDepotReturn_Click(e, vform);
                break;
            case 4:
                $scope.COTOContainer_AddDepotReturn_Click(e, vform);
                break;
            case 5:
                $scope.COTOContainer_AddStock_Click(e, vform);
                break;
        }
    }

    // Open list location
    $scope.ListLocation_Open = function (e) {
        e.preventDefault();
        switch (_changeType) {
            case 1:
            case 2:
            case 3:
            case 4:
                $scope.COTODepotWin.center().open();
                $scope.COTODepot_Grid.dataSource.read();
                break;
            case 5:
                $scope.COTOStockWin.center().open();
                $scope.COTOStock_Grid.dataSource.read();
                break;
        }
    }

    //Cắt chặng
    $scope.COTOContainer_StopContainer = function (e, type) {
        if (Common.HasValue(e))
            e.preventDefault();
        $scope.OPSCOTOContainerGridMenuList.close();
        $scope.ContLocaitonType = type;

        $scope.StandPopupType = 2;
        $scope.StandDetailGridOptions.dataSource.read();

        $scope.StandWin.center().open();
    };

    //Đổi bãi lấy container
    $scope.COTOContainer_ChangeDepotGet_Click = function (e, vform) {
        e.preventDefault();

        if ($scope.LocationDepotID > 0 && vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận Đổi bãi lấy container ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ChangeDepotGet",
                        data: {
                            opsTOContainer: $scope.MenuItem.ID,
                            locationID: $scope.LocationDepotID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
    };

    //Thêm bão lấy cont
    $scope.COTOContainer_AddDepotGet_Click = function (e, vform) {
        e.preventDefault();

        if ($scope.LocationDepotID > 0 && vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận Thêm bão lấy cont ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_AddDepotGet",
                        data: {
                            opsTOContainer: $scope.MenuItem.ID,
                            locationID: $scope.LocationDepotID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
    };

    //Đổi bãi trả container
    $scope.COTOContainer_ChangeDepotReturn_Click = function (e, vform) {
        e.preventDefault();

        if ($scope.LocationDepotID > 0 && vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận Đổi bãi trả container ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ChangeDepotReturn",
                        data: {
                            opsTOContainer: $scope.MenuItem.ID,
                            locationID: $scope.LocationDepotID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
    };

    //Thêm bãi trả container
    $scope.COTOContainer_AddDepotReturn_Click = function (e, vform) {
        e.preventDefault();

        if ($scope.LocationDepotID > 0 && vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận Thêm bãi trả container ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_AddDepotReturn",
                        data: {
                            opsTOContainer: $scope.MenuItem.ID,
                            locationID: $scope.LocationDepotID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
    };

    //Thêm kho khác
    $scope.COTOContainer_AddStock_Click = function (e, vform) {
        e.preventDefault();

        if ($scope.LocationDepotID > 0 && vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận Thêm kho khác ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_AddStock",
                        data: {
                            opsTOContainer: $scope.MenuItem.ID,
                            locationID: $scope.LocationDepotID,
                            reasonID: $scope.ReasonChangeID,
                            reasonNote: $scope.ReasonChangeNote,
                        },
                        success: function (res) {
                            $scope.CatLocaiton_Win.close();
                            $scope.TODetail_ReloadAllGrid();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả container', NotifyType: Common.Message.NotifyType.SUCCESS });
        }
    };

    //#region Muon Cont

    var _itemCOTOCO = { ID: 0 };
    var _packingID = 0;
    $scope.CheckCount = 0;
    $scope.CheckCountUM = 0;
    $scope.CountMax = 0;
    $scope.BorrowTabIndex = 1;

    $scope.borrowTabStripOptions = {
        height: "100%", animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            if (e.item.id == 'borrowtab1') {
                $scope.BorrowTabIndex = 1;
            } else {
                $scope.BorrowTabIndex = 2;
            }
        }
    }

    $scope.COTOContainer_BorrowContAccept = function (e, win) {
        var lstID = [];
        angular.forEach($scope.TOContainerNonMaster_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                lstID.push(o.ORDContainerID);
        })
        if (lstID.length > 0 && lstID.length <= _itemCOTOCO.PackingQuantity) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận mượn cont ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_OrderLocal",
                        data: {
                            lst: lstID,
                            opsContainerID: _itemCOTOCO.ID
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.TODetail_ReloadAllGrid();
                            win.close();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Số lượng cont không hợp lệ (tối đa: ' + _itemCOTOCO.PackingQuantity + ')', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.COTOContainer_BorrowMasterAccept = function (e, win) {
        var lstID = [];
        angular.forEach($scope.UnCompleteMaster_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                lstID.push(o.ID);
        })
        if (lstID.length > 0 && lstID.length <= _itemCOTOCO.PackingQuantity) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận mượn cont ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_OrderLocalByMaster",
                        data: {
                            lst: lstID,
                            opsContainerID: _itemCOTOCO.ID
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.TODetail_ReloadAllGrid();
                            win.close();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Số lượng cont không hợp lệ (tối đa: ' + _itemCOTOCO.PackingQuantity + ')', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.TOContainerNonMaster_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_COTONonMasterList",
            readparam: function () {
                return {
                    packingID: _packingID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                title: ' ', width: '40px',
                template: '<input ng-hide="CheckCount >= CountMax && !dataItem.IsChoose" class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,TOContainerNonMaster_Grid,TOContainerNonMaster_GridChange($event,dataItem))" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'OrderCode', width: 100,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContainer', width: 100,
                title: '{{RS.CATPacking.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 100,
                title: '{{RS.ORDOrder.RequestDate}}',
                template: '#=Common.Date.FromJsonDMYHM(RequestDate)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ServiceOfOrder', width: 100,
                title: '{{RS.ORDOrder.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromCode', width: 100,
                title: '{{RS.ORDOrder.LocationFromCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: 150,
                title: '{{RS.ORDOrder.TextFrom}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', width: 100,
                title: '{{RS.ORDOrder.LocationToCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 150,
                title: '{{RS.ORDOrder.TextTo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $scope.CheckCount = 0;
        }
    };

    $scope.TOContainerNonMaster_GridChange = function (e, o) {
        var c = $(e.target).prop('checked');
        if (c) {
            $scope.CheckCount++;
        }
        else {
            $scope.CheckCount--;
        }
    }

    $scope.UnCompleteMaster_GridChange = function (e, o) {
        var c = $(e.target).prop('checked');
        if (c) {
            $scope.CheckCountUM++;
        }
        else {
            $scope.CheckCountUM--;
        }
    }

    $scope.UnCompleteMaster_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_UnCompleteMasterList",
            readparam: function () {
                return {
                    packingID: _packingID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                title: ' ', width: '40px',
                template: '<input ng-hide="CheckCountUM >= CountMax && !dataItem.IsChoose" class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,UnCompleteMaster_Grid,UnCompleteMaster_GridChange($event,dataItem))" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'MasterCode', width: 100,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', width: 100,
                title: '{{RS.CATVehicle.RegNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100,
                title: '{{RS.OPSCOTOMaster.RomoocNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $scope.CheckCountUM = 0;
        }
    };
    //#endregion 

    //#region Swap cont

    $scope.SwapTabIndex = 1;

    $scope.swapTabStripOptions = {
        height: "100%", animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            if (e.item.id == 'swaptab1') {
                $scope.SwapTabIndex = 1;
            } else {
                $scope.SwapTabIndex = 2;
            }
        }
    }

    $scope.SwapCOTOContainer_ContOpen = function (e) {
        _itemCOTOCO = $scope.MenuItem
        _packingID = _itemCOTOCO.PackingID;
        $scope.CountMax = _itemCOTOCO.PackingQuantity;
        $scope.OPSCOTOContainerGridMenuList.close();
        e.preventDefault();
        $scope.SwapContainerWin.center().open();
        $scope.SwapTOContainerNonMaster_Grid.dataSource.read();
        $scope.SwapUnCompleteMaster_Grid.dataSource.read();
    }

    $scope.SwapCOTOContainer_ContAccept = function (e, win) {
        var lstID = [];
        angular.forEach($scope.SwapTOContainerNonMaster_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                lstID.push(o.ORDContainerID);
        })
        if (lstID.length > 0 && lstID.length <= _itemCOTOCO.PackingQuantity) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận mượn cont ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_OrderExport",
                        data: {
                            lst: lstID,
                            opsContainerID: _itemCOTOCO.ID
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.TODetail_ReloadAllGrid();
                            win.close();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Số lượng cont không hợp lệ (tối đa: ' + _itemCOTOCO.PackingQuantity + ')', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.SwapCOTOContainer_MasterAccept = function (e, win) {
        var lstID = [];
        angular.forEach($scope.SwapUnCompleteMaster_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                lstID.push(o.ID);
        })
        if (lstID.length > 0 && lstID.length <= _itemCOTOCO.PackingQuantity) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận mượn cont ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_OrderExportByMaster",
                        data: {
                            lst: lstID,
                            opsContainerID: _itemCOTOCO.ID
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.TODetail_ReloadAllGrid();
                            win.close();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Số lượng cont không hợp lệ (tối đa: ' + _itemCOTOCO.PackingQuantity + ')', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.SwapTOContainerNonMaster_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_SwapCOTONonMasterList",
            readparam: function () {
                return {
                    packingID: _packingID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                title: ' ', width: '40px',
                template: '<input ng-hide="CheckCount >= CountMax && !dataItem.IsChoose" class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SwapTOContainerNonMaster_Grid,SwapTOContainerNonMaster_GridChange($event,dataItem))" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'OrderCode', width: 100,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContainer', width: 100,
                title: '{{RS.CATPacking.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 100,
                title: '{{RS.ORDOrder.RequestDate}}',
                template: '#=Common.Date.FromJsonDMYHM(RequestDate)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ServiceOfOrder', width: 100,
                title: '{{RS.ORDOrder.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromCode', width: 100,
                title: '{{RS.ORDOrder.LocationFromCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: 150,
                title: '{{RS.ORDOrder.TextFrom}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', width: 100,
                title: '{{RS.ORDOrder.LocationToCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 150,
                title: '{{RS.ORDOrder.TextTo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $scope.CheckCount = 0;
        }
    };

    $scope.SwapTOContainerNonMaster_GridChange = function (e, o) {
        var c = $(e.target).prop('checked');
        if (c) {
            $scope.CheckCount++;
        }
        else {
            $scope.CheckCount--;
        }
    }

    $scope.SwapUnCompleteMaster_GridChange = function (e, o) {
        var c = $(e.target).prop('checked');
        if (c) {
            $scope.CheckCountUM++;
        }
        else {
            $scope.CheckCountUM--;
        }
    }

    $scope.SwapUnCompleteMaster_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_SwapUnCompleteMasterList",
            readparam: function () {
                return {
                    packingID: _packingID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                title: ' ', width: '40px',
                template: '<input ng-hide="CheckCountUM >= CountMax && !dataItem.IsChoose" class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SwapUnCompleteMaster_Grid,SwapUnCompleteMaster_GridChange($event,dataItem))" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'MasterCode', width: 100,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', width: 100,
                title: '{{RS.CATVehicle.RegNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100,
                title: '{{RS.OPSCOTOMaster.RomoocNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $scope.CheckCountUM = 0;
        }
    };

    //#endregion

    //#region grid

    $scope.COTODepot_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_DepotList",
            readparam: function () {
                return {
                    opscontainerID: $scope.MenuItem.OPSContainerID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, selectable: true, autoBind: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
           {
               field: '', width: 70,
               template: '<a ng-click="PickLocation($event,dataItem)" class="btn-grid-pick" >Chọn</a>',
               title: '',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Code', width: 100, title: '{{RS.CATLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Location', width: 100, title: '{{RS.CATLocation.LocationName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 100, title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'ProvinceName', width: 100, title: '{{RS.CATProvince.ProvinceName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DistrictName', width: 100, title: '{{RS.CATDistrict.DistrictName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.COTOStock_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_COTOStockRead",
            readparam: function () {
                return {
                    opscontainerID: $scope.MenuItem.ID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, selectable: true, autoBind: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
           {
               field: '', width: 70,
               template: '<a ng-click="PickLocation($event,dataItem)" class="btn-grid-pick" >Chọn</a>',
               title: '',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Code', width: 100, title: '{{RS.CATLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Location', width: 100, title: '{{RS.CATLocation.LocationName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 100, title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'ProvinceName', width: 100, title: '{{RS.CATProvince.ProvinceName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DistrictName', width: 100, title: '{{RS.CATDistrict.DistrictName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.PickLocation = function (e, item) {
        e.preventDefault();
        $scope.LocationDepotID = item.ID;
        $scope.LocationDepotName = "[" + item.Code + "] " + item.Address;
        $scope.COTODepotWin.close();
        $scope.COTOStockWin.close();
        $scope.ReasonChangeLocation_Win.center().open();
    }

    //#endregion

    //#endregion

    //#region --Take other

    $scope.SchedulerFilter = {
        DateFrom: Common.Date.Date($scope.GetMonday(new Date())),
        DateTo: $scope.GetMonday(new Date()).addDays(7),
        ListORDContainerID: [],
        ListRomoocID: [],
        ListTractorID: [],
        IsFilterORD: false,
        IsFilterTractor: false,
        IsFilterRomooc: false,
        IsFilterDate: false,
        ShowDate: false,
        ShowType: 1,
        IsFilterMasterStatus: false,
    }
    var MasterPlanID = 0;
    var MasterPlanETD = 0;
    var ShedulerChangePlanResource = [];

    $scope.NewTimeLineLoadingV2 = false;

    $scope.COContainerPlan_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_COList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                    OrderCode: { editable: false },
                    PackingCode: { editable: false },
                    LocationFromCode: { editable: false },
                    ReasonChangeName: { editable: false },
                    ReasonChangeNote: { editable: false },
                }
            },
            readparam: function () { return { masterID: MasterPlanID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'OrderCode', width: 150, title: '{{RS.ORDOrder.Code}}', },
            { field: 'PackingCode', width: 150, title: '{{RS.CATPacking.Code}}', },
            { field: 'ContainerNo', width: 150, title: '{{RS.ORDOrder.ContainerNo}}', },
            { field: 'Ton', width: 150, title: '{{RS.ORDContainer.Ton}}', },
            { field: 'SealNo1', width: 150, title: '{{RS.OPSContainer.SealNo1}}', },
            { field: 'SealNo2', width: 150, title: '{{RS.OPSContainer.SealNo2}}', },
            { field: 'DepotCode', width: 150, title: '{{RS.MONMonitorIndex.DepotCode}}', },
            { field: 'DepotAddress', width: 150, title: '{{RS.MONMonitorIndex.DepotAddress}}', },
            { field: 'DepotReturnCode', width: 150, title: '{{RS.MONMonitorIndex.DepotReturnCode}}', },
            { field: 'DepotReturnAddress', width: 150, title: '{{RS.MONMonitorIndex.DepotReturnAddress}}', },
            { field: 'ReasonChangeName', width: 150, title: '{{RS.CATReason1.ReasonName}}', },
            { field: 'ReasonChangeNote', width: 150, title: '{{RS.CATReason.ReasonChangeNote}}', },
            { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.TOContainerPlan_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TOContainerList",
            readparam: function () {
                return {
                    masterID: MasterPlanID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ColorClass: { type: 'string' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                    IsAvailable: { type: 'bool' },
                    STT: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                field: 'COTOSort', width: 60, sortable: false, title: 'STT',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'OrderCode', width: 100,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', width: 100,
                title: '{{RS.ORDOrder.ContainerNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100,
                title: '{{RS.OPSCOTOMaster.RomoocNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrder', width: 100,
                title: '{{RS.ORDOrder.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfCOContainer', width: 100,
                title: '{{RS.StatusOfCOContainer}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContainer', width: 100,
                title: '{{RS.CATPacking.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfStatusContainerName', width: 100,
                title: '{{RS.SYSVar1.ValueOfVar}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: 150,
                title: '{{RS.ORDOrder.TextFrom}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 150,
                title: '{{RS.ORDOrder.TextTo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    var lastSearch = "";
    $scope.SCPSearch = "";
    $scope.ChangePlanTractorFilter = function (e) {
        if (e.which === 13) {
            lastSearch = e.target.value;
            var lst = [];
            angular.forEach(ShedulerChangePlanResource, function (o, i) {
                if (o.Text.toUpperCase().includes(e.target.value.toUpperCase()))
                    lst.push(o);
            })
            if (lst.length == 0) {
                if (e.target.value.trim() == "") {
                    $scope.new_timeline_v2_TripOptions.resources[0].dataSource = ShedulerChangePlanResource;
                    $timeout(function () {
                        $scope.new_timeline_v2_Trip.refresh();
                    }, 100)
                }
                else
                    $rootScope.Message({ Msg: "Không tìm thấy xe", NotifyType: Common.Message.NotifyType.ERROR });
            }
            else {
                $scope.new_timeline_v2_TripOptions.resources[0].dataSource = lst;
                $timeout(function () {
                    $scope.new_timeline_v2_Trip.refresh();
                }, 100)
            }
        }
    };

    $scope.new_timeline_v2_TripOptions = {
        date: new Date().addDays(-1), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: true, resize: false, update: false },
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
                columnWidth: 40, selected: true,
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
        moveEnd: function (e) {
            var rowID = null;
            if (typeof (e.event.field) == "number") {
                rowID = e.event.field;
            }
            else if (typeof (e.event.field) == "object") {
                rowID = e.event.field[0];
            }
            if (rowID != null) {
                if ($scope.CurrentMaster.VehicleID == e.resources.field[0] && rowID != e.resources.field[0]) {
                    e.preventDefault();
                    MasterPlanID = e.event.TOMasterID;
                    //MasterPlanETD = e.start;
                    MasterPlanETD = Common.Date.FromJsonYYMMDDHM(e.start);
                    $scope.ChangePlanOpen(MasterPlanID);
                }
                else if ($scope.CurrentMaster.VehicleID == e.resources.field[0] && rowID == e.resources.field[0] && e.event.StatusOfEvent == 1) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_MasterChangeETD",
                        data: {
                            masterID: e.event.TOMasterID,
                            etd: e.start,
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Đã đổi thời gian chuyến', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
                else {
                    e.preventDefault();
                }
            }
            else {
                e.preventDefault();
            }
        },
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
                        field: { from: "VehicleID" },
                    }
                }
            }
        },
        eventTemplate: $("#new-timeline-v2-event-template").html(),
        group: { resources: ["VehicleID"], orientation: "vertical" },
        resources: [
            {
                field: "field",
                name: "VehicleID",
                dataTextField: "Text",
                dataValueField: 'VehicleID',
                dataSource: [{ value: '', text: 'Không có DL' }],
                multiple: true
            }
        ],
        dataBound: function () {
            var scheduler = this;

            //
            var rh = $(scheduler.element.find('.k-scheduler-times .k-scheduler-table')[0]).find('th')[1]
            if ($(rh).find('input').length == 0) {
                $(rh).css('background-color', '#cecece');
                var html = '<input ng-model="SCPSearch" style="width: 100%;border:none;" placeholder="Tìm theo xe..." class="k-textbox" ng-keypress="ChangePlanTractorFilter($event)"/>';
                $(rh).prepend($compile(html)($scope));
            }
            //to vang nha xe vs xe hien tai

            scheduler.element.find('.k-scheduler-content tr td').each(function (idx, td) {
                var slot = scheduler.slotByElement(td), resource = scheduler.resources[0].dataSource.data();
                if (Common.HasValue(slot) && Common.HasValue(resource[slot.groupIndex])) {
                    var uid = resource[slot.groupIndex].VehicleID;
                    var cur = resource[slot.groupIndex].Option1;
                    if (uid == -1 || cur == "CurrentVehicle") {
                        $(td).css('background', 'rgb(255, 249, 158)');
                    }
                }
            })
            $(scheduler.element.find('.k-scheduler-times .k-scheduler-table')[1]).find('tr').each(function (idx, tr) {
                var uid = scheduler.resources[0].dataSource.data()[idx];
                if (Common.HasValue(uid) && (uid.VehicleID == -1 || uid.Option1 == "CurrentVehicle")) {
                    $(tr).css('background', 'rgb(255, 249, 158)');
                    $(tr).find('i').css('color', 'rgb(255, 249, 158)');
                }
            })
        }
    }

    $scope.EmptyHour = 0;
    $scope.TakeOtherMaster = function (e, dataItem) {
        e.preventDefault();
        _itemCOTOCO = dataItem;
        $scope.EmptyHour = 0;
        $scope.ShedularChangePlanTractorLoadData();
        $scope.ChangePlanContainerLoadData();
        $scope.new_timeline_win.center().open();
        isChangePlan = true;

    }
    $scope.AddHourEmpty = function (e) {
        e.preventDefault
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận thêm thời gian trống ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTowerCO_AddHourEmpty",
                    data: {
                        hour: $scope.EmptyHour,
                        opscontainerid: _itemCOTOCO.ID
                    },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.ShedularChangePlanTractorLoadData();
                        $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
        });
    }

    var _isClickContinue = false;
    $scope.TOContainer_Continue = function (e, dataitem) {
        e.preventDefault();
        _isClickContinue = true;
        _itemCOTOCO = dataitem;
        $scope.MasterByTractor_Grid.dataSource.read();

    }

    $scope.MasterByTractor_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_ListMasterByTractor",
            readparam: function () {
                return {
                    masterID: $scope.masterID,
                    tractorID: $scope.CurrentMaster.VehicleID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a class="k-button" ng-click="TOContainer_ContinuePick($event,dataItem.MasterID)"><i class="fa fa-check"></i></a>',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'MasterCode', width: 100,
                title: '{{RS.OPSDITOMaster.Code}}',
                template: '<a ng-click="Tractor_GridClick_OpenChangePlan($event,dataItem.MasterID)" title="Xem chi tiết" style="cursor:pointer;font-weight: bold;">#=MasterCode != null ? MasterCode : ""#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', width: 100,
                title: '{{RS.CATVehicle.RegNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100,
                title: '{{RS.OPSCOTOMaster.RomoocNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var datalen = this.dataSource.data().length;
            if (_isClickContinue && datalen > 0) {
                if (datalen == 1) {
                    var opscontainerid = _itemCOTOCO.ID;
                    var masterid = this.dataSource.data()[0].MasterID;
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Bạn muốn tiếp tục chạy chuyến trước ?',
                        pars: {},
                        Ok: function (pars) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.MON,
                                method: _MONMonitor_ControlTowerCO.URL.MONCO_Continue,
                                data: { opscontainerid: opscontainerid, masterID: masterid },
                                success: function (res) {
                                    //$scope.TODetail_ReloadAllGrid();
                                    $rootScope.IsLoading = false;

                                    $scope.LoadMasterDetail(res);

                                    $scope.MasterByTractorWin.close();
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                }
                            });
                        }
                    });
                }
                else {
                    $scope.MasterByTractorWin.center().open();
                }
            }
            else if (_isClickContinue && datalen == 0) {
                $rootScope.Message({ Msg: 'Hết chuyến', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    };

    $scope.IsChangePlan = true;
    $scope.Tractor_GridClick_OpenChangePlan = function (e, id) {
        e.preventDefault();
        $scope.ChangePlanWin.center().open();
        $scope.IsChangePlan = false;
        MasterPlanID = id;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MasterGet",
            data: { id: MasterPlanID },
            success: function (res) {
                $scope.CurrentPlanMaster = res;
                $scope.TO_Win_PlanTitle = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentPlanMaster.Code + ' - ETD: ' + $scope.CurrentPlanMaster.ETD + ' ETA: ' + $scope.CurrentPlanMaster.ETA;

            }
        })
        $scope.COContainerPlan_Grid.dataSource.read();
        $scope.TOContainerPlan_Grid.dataSource.read();
    }

    $scope.TOContainer_ContinuePick = function (e, id) {
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn tiếp tục chạy chuyến trước ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_ControlTowerCO.URL.MONCO_Continue,
                    data: {
                        opscontainerid: _itemCOTOCO.ID,
                        masterID: id,
                    },
                    success: function (res) {
                        //$scope.TODetail_ReloadAllGrid();
                        $rootScope.IsLoading = false;
                        $scope.LoadMasterDetail(res);
                        $scope.MasterByTractorWin.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        });
    }

    $scope.TOContainer_End = function (e, dataitem) {
        e.preventDefault();

        dataRow = dataitem;

        $scope.CTConfirm({
            Lable: "Bạn chọn sử dụng bãi hiện tại để cắt mooc ?",
            OK: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_ControlTowerCO.URL.MONCO_End,
                    data: {
                        opscontainerid: dataRow.id,
                        locationID: null,
                    },
                    success: function (res) {
                        $rootScope.IsLoading = false;

                        $scope.TODetail_ReloadAllGrid();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            },
            Deny: function () {
                lstIgnore = [];
                $scope.CatLocation_Grid.dataSource.read();
                $scope.CatLocaiton_Win.center().open();
                $scope.ContLocaitonType = 7;
            }
        })

    }

    $scope.ChangeMasterWinOpen = function () {
        $scope.SchedulerFilter.IsFilterMasterStatus = true;
        $scope.ChangePlanContainerLoadData();
    }

    $scope.ChangeMasterWinClose = function () {
        $scope.SchedulerFilter.IsFilterMasterStatus = false;
        $scope.ShedularLoadData();
    }

    $scope.ShedularChangePlanTractorLoadData = function () {
        $scope.NewTimeLineLoadingV2 = true;

        //timeline tractor
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONCTCO_TimelineChangePlan_TractorResource",
            data: {
                masterID: $scope.masterID,
            },
            success: function (res) {
                ShedulerChangePlanResource = res;
                var lst = [];
                angular.forEach(res, function (o, i) {
                    lst.push(o.VehicleID);
                });
                $scope.new_timeline_v2_TripOptions.resources[0].dataSource = res;
                if (lst.length > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONCTCO_TimelineChangePlan_TractorEvent",
                        data: {
                            lst: lst,
                            masterID: $scope.masterID,
                        },
                        success: function (res1) {
                            angular.forEach(res1, function (o, i) {
                                o.StartDate = Common.Date.FromJson(o.StartDate);
                                o.EndDate = Common.Date.FromJson(o.EndDate);
                            });
                            $scope.new_timeline_v2_TripOptions.dataSource.data = res1;
                            $scope.NewTimeLineLoadingV2 = false;
                            $timeout(function () {
                                $scope.new_timeline_v2_Trip.refresh();
                            }, 200)
                        }
                    })
                }
                else {
                    $scope.NewTimeLineLoadingV2 = false;
                }
            }
        })

    };

    $scope.ChangePlanOpen = function (id) {
        $scope.ChangePlanWin.center().open();
        MasterPlanID = id;
        $scope.IsChangePlan = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MasterGet",
            data: { id: MasterPlanID },
            success: function (res) {
                $scope.CurrentPlanMaster = res;
                $scope.TO_Win_PlanTitle = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentPlanMaster.Code + ' - ETD: ' + $scope.CurrentPlanMaster.ETD + ' ETA: ' + $scope.CurrentPlanMaster.ETA;

            }
        })
        $scope.COContainerPlan_Grid.dataSource.read();
        $scope.TOContainerPlan_Grid.dataSource.read();
    }

    $scope.CT_ConfirmMess = 'Có muốn sử dụng mooc đang chạy hay không ?';

    $scope.ChangePlanAccept = function (e) {
        e.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_CheckMasterRomooc",
            data: {
                masterID: $scope.masterID,
                planMasterID: MasterPlanID,
                etd: MasterPlanETD,
            },
            success: function (res) {
                $rootScope.IsLoading = false;

                if (res == 1) {
                    $scope.CT_MessConfirm_Win.center().open();
                }
                else {
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Xác nhận đổi kế hoạch ?',
                        pars: {},
                        Ok: function (pars) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.MON,
                                method: "MONControlTowerCO_ChangeMasterPlan",
                                data: {
                                    masterID: $scope.masterID,
                                    planMasterID: MasterPlanID,
                                    isChangeMooc: true,
                                    etd: MasterPlanETD
                                },
                                success: function (res) {
                                    $rootScope.IsLoading = false;
                                    $scope.ChangePlanWin.close();
                                    $scope.new_timeline_win.close();
                                    isChangePlan = false;
                                    $scope.LoadMasterDetail(MasterPlanID);
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                }
                            })
                        }
                    });
                }
            }
        })

    }

    $scope.ChangePlanConfirmKeepRomooc = function (e, isAccept) {
        if (isAccept) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_ChangeMasterPlan",
                data: {
                    masterID: $scope.masterID,
                    planMasterID: MasterPlanID,
                    isChangeMooc: false,
                    etd: MasterPlanETD
                },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.ChangePlanWin.close();
                    $scope.new_timeline_win.close();
                    isChangePlan = false;
                    $scope.LoadMasterDetail(MasterPlanID);
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
        else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_ChangeMasterPlan",
                data: {
                    masterID: $scope.masterID,
                    planMasterID: MasterPlanID,
                    isChangeMooc: true,
                    etd: MasterPlanETD
                },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.ChangePlanWin.close();
                    $scope.new_timeline_win.close();
                    isChangePlan = false;
                    $scope.LoadMasterDetail(MasterPlanID);
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
        $scope.CT_MessConfirm_Win.close();
    }

    $scope.TOContainer_Continuous = function (e, dataitem) {
        e.preventDefault();
        $scope.OPSCOTOContainerGridMenuList.close();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn chạy chuyến tiếp theo ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTowerCO_Continuous",
                    data: {
                        id: dataitem.ID,
                    },
                    success: function (res) {

                        $scope.TODetail_ReloadAllGrid();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                        if (res > 0) {
                            $rootScope.Message({
                                Type: Common.Message.Type.Confirm,
                                Msg: "Bạn có muốn xem thông tin chuyến tiếp theo",
                                pars: {},
                                Ok: function (pars) {
                                    $scope.LoadMasterDetail(res);
                                }
                            });
                        }
                    }
                });
            }
        });
    }

    //#region ket thuc tai bai

    var standType = 1;
    $scope.StandPopupType = 0;
    $scope.StandItem = {
        Tractor: {},
        Romooc: {}
    };

    $scope.normalTabStripOptions = {
        height: "100%", animation: false,
    }

    $scope.TOContainer_EndStation = function (e, dataitem) {
        e.preventDefault();
        _itemCOTOCO = dataitem;
        $scope.StandPopupType = 1;
        $scope.StandDetailGridOptions.dataSource.read();
        $scope.OPSCOTOContainerGridMenuList.close();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_VehicleLocationDefault",
            data: {
                opsTOContainer: _itemCOTOCO.ID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.MasterEndStationWin.center().open();
                if (Common.HasValue(res)) {
                    if (Common.HasValue(res[0])) {
                        $scope.StandItem.Tractor = res[0];
                    }
                    if (Common.HasValue(res[1])) {
                        $scope.StandItem.Romooc = res[1];
                    }
                }
            }
        });
    }

    $scope.TOContainer_EndStation_Accept = function (e) {
        e.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn kết thúc chuyến tại bãi này ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTowerCO_EndStation",
                    data: {
                        id: _itemCOTOCO.ID,
                        locationRomoocID: $scope.StandItem.Romooc.ID,
                        locationVehicleID: $scope.StandItem.Tractor.ID,
                    },
                    success: function (res) {
                        $scope.TODetail_ReloadAllGrid();
                        $rootScope.IsLoading = false;
                        $scope.MasterEndStationWin.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        });
    };

    $scope.LocationEndMasterGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "CATLocation_List",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
            readparam: function () { return { ignore: [] } }
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: false, editable: false, selectable: true,
        columns: [
           {
               field: '', width: 70,
               template: '<a ng-click="PickEndStation($event,dataItem)" class="btn-grid-pick" >Chọn</a>',
               title: '',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Code', width: 150,
               title: '{{RS.CATLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Location', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.StandDetailGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_StandDetailList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
            readparam: function () { return { opsTOContainer: $scope.MenuItem.ID, } }
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: false, editable: false, selectable: true, autoBind: false,
        columns: [
           {
               field: '', width: 70,
               template: '<a ng-click="PickEndStation($event,dataItem)" class="btn-grid-pick" >Chọn</a>',
               title: '',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Code', width: 150,
               title: '{{RS.CATLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Location', width: 200,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 200,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.PickEndStation = function (e, item) {
        e.preventDefault();
        if ($scope.StandPopupType == 1) {
            if (standType == 1) {
                $scope.StandItem.Tractor = item;
            }
            else {
                $scope.StandItem.Romooc = item;
            }
        }
        else {

            $scope.COTOStopLocationID = item.ID;
            $scope.COTOStopLocationName = "[" + item.Code + "]" + item.Address;
            $scope.ReasonChange_Win.center().open();
        }
        $scope.StandWin.close();
    };

    $scope.CancelStand = function (e, type) {
        e.preventDefault();

        if (type == 1) {
            $scope.StandItem.Tractor.ID = null;
            $scope.StandItem.Tractor.Code = "";
            $scope.StandItem.Tractor.Location = "";
            $scope.StandItem.Tractor.Address = "";
        }
        else {
            $scope.StandItem.Romooc.ID = null;
            $scope.StandItem.Romooc.Code = "";
            $scope.StandItem.Romooc.Location = "";
            $scope.StandItem.Romooc.Address = "";
        }
    }

    $scope.ChangeStandDefault = function (e, type) {
        e.preventDefault();
        standType = type;
        $scope.StandWin.center().open();
    }
    //#endregion

    //theo dau container
    $scope.ChangePlanContainer_schedulerOptions = {
        date: new Date(),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        editable: { create: false, destroy: false, move: true, resize: false, update: false },
        footer: false, eventHeight: 20, majorTick: 60, height: '99%',
        eventTemplate: $("#change-plan-container-scheduler-template").html(),
        groupHeaderTemplate: $("#container-scheduler-header-template").html(),
        messages: {
            today: "Hôm nay"
        },
        navigate: function (e) {
            var time = this;
            $timeout(function () {
                time.refresh();
            }, 100)
        },
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
                columnWidth: 40, selected: true,
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
            data: [],
            schema: {
                model: {
                    id: "meetingID",
                    fields: {
                        meetingID: { from: "MasterID", type: "number" },
                        title: { from: "MasterCode", defaultValue: "No title", validation: { required: true } },
                        start: { from: "DFrom", type: "date" },
                        end: { from: "DTo", type: "date" },
                        attendees: { from: "ORDContainerID" },
                        roomId: { from: "OrderCode", nullable: true },
                    }
                }
            }
        },
        group: {
            resources: ["cont"],
            orientation: "vertical"
        },
        resources: [
            {
                field: "attendees",
                name: "cont",
                dataTextField: 'VehicleNo',
                dataValueField: 'ID',
                dataSource: [
                    { ID: -1, WarningMsg: '', OrderCode: '', TypeOfContainer: '' },
                ],
                multiple: true
            },
        ],
        moveEnd: function (e) {
            var rowID = null;
            if (typeof (e.event.attendees) == "number") {
                rowID = e.event.attendees;
            }
            else if (typeof (e.event.attendees) == "object") {
                rowID = e.event.attendees[0];
            }
            if (rowID != null) {
                if (e.resources.attendees[0] == -1 && rowID != e.resources.attendees[0]) {
                    e.preventDefault();
                    MasterPlanID = e.event.meetingID;
                    //MasterPlanETD = e.start;
                    MasterPlanETD = Common.Date.FromJsonYYMMDDHM(e.start);
                    $scope.ChangePlanOpen(MasterPlanID);
                }
                else if (e.resources.attendees[0] == -1 && rowID == e.resources.attendees[0] && e.event.IsComplete == false) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_MasterChangeETD",
                        data: {
                            masterID: e.event.meetingID,
                            etd: e.start,
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Đã đổi thời gian chuyến', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
                else {
                    e.preventDefault();
                }
            }
            else {
                e.preventDefault();
            }
        },
        dataBound: function (e) {
            var scheduler = this;
            var left = this.element.find('.k-scheduler-layout').children().children();
            var a = left.last();

            //fix group header template khong su dung dc $scope 
            angular.forEach(a.find('.k-scheduler-times th'), function (o, i) {
                $compile(o)($scope);
            });

            //to vang nha xe vs xe hien tai

            scheduler.element.find('.k-scheduler-content tr td').each(function (idx, td) {
                var slot = scheduler.slotByElement(td), resource = scheduler.resources[0].dataSource.data();
                if (Common.HasValue(slot) && Common.HasValue(resource[slot.groupIndex])) {
                    var uid = resource[slot.groupIndex].ID;
                    if (uid == -1) {
                        $(td).css('background', 'rgb(255, 249, 158)');
                    }
                }
            })
            $(scheduler.element.find('.k-scheduler-times .k-scheduler-table')[1]).find('tr').each(function (idx, tr) {
                var uid = scheduler.resources[0].dataSource.data()[idx];
                if (Common.HasValue(uid) && (uid.ID == -1)) {
                    $(tr).css('background', 'rgb(255, 249, 158)');
                    $(tr).find('i').css('color', 'rgb(255, 249, 158)');
                }
            })
        },
    };

    $scope.ChangePlanContainerLoadData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_ChangePlanByORDCont_Resource",
            data: { filter: $scope.SchedulerFilter, masterID: $scope.masterID },
            success: function (res) {
                var data = [];
                var lst = [];
                var lstOrder = []; var lstOrderCheck = [];
                _MONMonitor_ControlTowerCO.Data._scheduleContainer = [];
                angular.forEach(res, function (o, i) {
                    lst.push(o.ID);
                    //
                    o.IsChoose = false;
                    data.push(o);
                    //
                    if (lstOrderCheck[o.OrderCode] != true) {
                        lstOrder.push(o);
                        lstOrderCheck[o.OrderCode] = true;
                    }

                    _MONMonitor_ControlTowerCO.Data._scheduleContainer[o.ID + ''] = o;
                });
                $scope.ChangePlanContainer_schedulerOptions.resources[0].dataSource = res;

                //$scope.container_schedulerOptions.resources[0].dataSource = lstOrder;
                $scope.orderFilter_Grid.dataSource.data(data);
                if (lst.length > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ChangePlanByORDCont_Task",
                        data: {
                            filter: $scope.SchedulerFilter,
                            lst: lst,
                            masterID: $scope.masterID
                        },
                        success: function (res1) {
                            angular.forEach(res1, function (o, i) {
                                o.DFrom = o.ETD = Common.Date.FromJson(o.ETD);
                                o.DTo = o.ETA = Common.Date.FromJson(o.ETA);
                                o.E_Line_Left = 0;
                                o.E_Line_Width = 100;
                                o.A_Line_Left = 0;
                                o.A_Line_Width = 0;
                                if (o.ATD != null && o.ATA != null) {
                                    o.ATD = Common.Date.FromJson(o.ATD);
                                    o.ATA = Common.Date.FromJson(o.ATA);
                                    if (o.ATD < o.ETD)
                                        o.DFrom = o.ATD;
                                    if (o.ATA > o.ETA)
                                        o.DTo = o.ATA;

                                    var length = o.DTo - o.DFrom;
                                    // ETD ETA
                                    o.E_Line_Left = (o.ETD - o.DFrom) * 100 / length;
                                    o.E_Line_Width = (o.ETA - o.ETD) * 100 / length;
                                    // ATD ATD
                                    o.A_Line_Left = (o.ATD - o.DFrom) * 100 / length;
                                    o.A_Line_Width = (o.ATA - o.ATD) * 100 / length;
                                }

                            });
                            $scope.ChangePlanContainer_schedulerOptions.dataSource.data = res1;
                        }
                    })
                }
            }
        })

    };

    //#endregion

    //#region -OPS_COTO
    var _cotoid = 0;
    $scope.OPSCOTO_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OPSCOTORead",
            readparam: function () {
                return {
                    masterID: $scope.masterID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    ApprovedDate: { type: 'date', editable: false },
                    Ton: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                    MasterCode: { type: 'string', editable: false },
                    RegNo: { type: 'string', editable: false },
                    KMGPS: { type: 'string', editable: false },
                    Ton: { type: 'number', editable: false },
                    SortOrder: { type: 'number', editable: false },
                }
            },
        }),
        toolbar: kendo.template($('#opscoto-grid-toolbar').html()),
        selectable: true, reorderable: true, editable: true, pageable: Common.PageSize, autoBind: false,
        height: '100%', sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px', field: '',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSCOTO_grid,Main_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSCOTO_grid,Main_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                field: '', width: 70, title: '', filterable: false,
                template: '<a ng-click="OPSCOTO_DetailOpen($event,dataItem)" class="btn-grid-pick">ĐH</a>',
            },
            { field: 'SortOrder', width: 120, title: 'STT', filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'StatusName', width: 120, title: 'Trạng thái', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RegNo', width: 100, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: 100, title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'KM', width: 100, title: 'KM', filterable: false, },
            { field: 'KMGPS', width: 100, title: 'KMGPS', filterable: false, },
            { field: 'Ton', width: 150, title: '{{RS.CATRouting.ParentName}}', filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'LocationFromCode', width: 120, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 120, title: 'Điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 120, title: 'Địa chỉ đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 120, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 120, title: 'Điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 120, title: 'Địa chỉ đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ApprovedBy', width: 120, title: 'Người duyệt', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ApprovedDate', width: 120, title: 'Ngày duyệt', template: "#=ApprovedDate==null?' ':Common.Date.FromJsonDMYHM(ApprovedDate)#",
                filterable: false,
            },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function () {
            if (isShowDetail) {
                $rootScope.Loading.Change("Thông tin vận hành...", 100);
                $rootScope.Loading.Hide();
            }
        }
    }

    $scope.OPSCOTODetail_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OPSCOTODetailRead",
            readparam: function () {
                return {
                    cotoid: _cotoid
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
        }),
        selectable: true, reorderable: true, editable: false, pageable: Common.PageSize, autoBind: false,
        height: '100%', sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px', field: '',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSCOTODetail_grid,Main_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSCOTODetail_grid,Main_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'SortOrder', width: 120, title: 'STT', filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'CustomerCode', width: 120, title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 100, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: 100, title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: 120, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 120, title: 'Địa chỉ đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 120, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 120, title: 'Địa chỉ đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
    }

    $scope.OPSCOTOContainerNonParent_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OPSCOTOContainerRead",
            readparam: function () {
                return {
                    cotoid: _cotoid,
                    masterID: $scope.masterID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
        }),
        selectable: true, reorderable: true, editable: false, pageable: Common.PageSize, autoBind: false,
        height: '100%', sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px', field: '',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSCOTOContainerNonParent_grid,Main_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSCOTOContainerNonParent_grid,Main_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'STT', width: 120, title: 'STT', filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'CustomerCode', width: 120, title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 100, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: 100, title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrder', width: 100, title: '{{RS.ORDOrder.ServiceOfOrderName}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainer', width: 100, title: 'RS.CATPacking.Code', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfCOContainer', width: 100, title: '{{RS.OPSCOTOContainer.StatusOfCOContainer}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfStatusContainerName', width: 100, title: '{{RS.SYSVar1.ValueOfVar}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { field: 'LocationFromCode', width: 120, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 120, title: 'Địa chỉ đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 120, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 120, title: 'Địa chỉ đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
    }

    $scope.OPSCOTO_Save = function (e, type) {
        e.preventDefault();
        var data = [];
        angular.forEach($scope.OPSCOTO_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose || o.dirty)
                data.push(o);
        });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_OPSCOTOSave",
                data: { lst: data, type: type },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.OPSCOTO_grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.OPSCOTO_DetailOpen = function (e, item) {
        e.preventDefault();
        _cotoid = item.ID;
        $scope.OPSCOTODetail_grid.dataSource.read();
        $scope.COTODetailWin.center().open();

    }

    $scope.OPSCOTO_DetailAddOpen = function (e) {
        e.preventDefault();
        $scope.OPSCOTOContainerNonParent_grid.dataSource.read();
        $scope.COTOContainerWin.center().open();

    }

    $scope.OPSCOTO_DetailAddAccept = function (e) {
        e.preventDefault();
        var data = [];
        angular.forEach($scope.OPSCOTOContainerNonParent_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                data.push(o.ID);
        });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_COTODeatailAddOPSTOContainer",
                data: { lst: data, id: _cotoid },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.OPSCOTODetail_grid.dataSource.read();
                    $scope.COTOContainerWin.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }

    }

    $scope.COTOKM_Update = function (e) {
        e.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_COTOKMUpdate",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.TOContainer_Grid.dataSource.read();
                $scope.OPSCOTO_grid.dataSource.read();
                $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
    }
    //#endregion

    //#region -Station

    $scope.Station_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_COStationList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                    LocationName: { editable: false },
                    Address: { editable: false },
                    KM: { editable: false },
                    Price: { editable: false },
                    Note: { editable: false },
                    IsApproved: { type: 'bool', editable: false },
                    DateCome: { type: 'date' }
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        toolbar: kendo.template($('#station-grid-toolbar').html()), autoBind: false,
        height: '99%', pageable: false, sortable: false, columnMenu: false, editable: true, resizable: false, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Station_Grid,Station_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Station_Grid,Station_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
           {
               field: 'LocationName', width: 170, title: '{{RS.CATLocation.Location}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 170, title: '{{RS.CATLocation.Address}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'KM', width: 80, title: '{{RS.OPSDITOStation.KM}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Price', width: 100, title: '{{RS.OPSDITOStation.Price}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DateCome', width: 150, title: '{{RS.FLMAssetTimeSheet.DateTo}}', template: '#=Common.Date.FromJsonDMYHM(DateCome)#', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Note', width: 150, title: '{{RS.OPSDITOStation.Note}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
            {
                field: 'IsApproved', width: 100, title: '{{RS.OPSCOTOStation.IsApproved}}',
                template: '<input type="checkbox" #= IsApproved ? \'checked="checked"\' : "" # ng-model="dataItem.IsApproved"  ng-click="ApproveStation($event,dataItem)" />',
                filterable: false
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            if (isShowDetail) {
                $rootScope.Loading.Change("Chi phí phát sinh...", 60);
                $scope.Trouble_Grid.dataSource.read();
            }
        }
    };

    $scope.Station_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_COStationNotinList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, resizable: false, filterable: { mode: 'row' },
        columns: [
             {
                 title: ' ', width: '40px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Station_NotinGrid,Station_NotinGridChange)" />',
                 headerAttributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Station_NotinGrid,Station_NotinGridChange)" />',
                 templateAttributes: { style: 'text-align: center;' },
                 filterable: false, sortable: false
             },
           {
               field: 'LocationName', width: 170, title: '{{RS.CATLocation.Location}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 170, title: '{{RS.CATLocation.Address}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'KM', width: 80, title: '{{RS.OPSDITOStation.KM}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Price', width: 100, title: '{{RS.OPSDITOStation.Price}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Note', width: 150, title: '{{RS.OPSDITOStation.Note}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.SearchStation = function ($event, win, grid) {
        $event.preventDefault();
        win.center().open();
        grid.dataSource.read();
    }

    $scope.Station_SaveList = function ($event, grid1, grid2) {
        var lstID = []
        angular.forEach(grid2.dataSource.data(), function (o, i) {
            if (o.IsChoose) {
                lstID.push(o.LocationID);
            }
        })

        if (lstID.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_COStationAdd",
                data: { ListStationID: lstID, masterID: $scope.masterID },
                success: function (res) {
                    grid1.dataSource.read();
                    grid2.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Station_RemoveList = function ($event, grid) {
        var lstID = []
        angular.forEach(grid.dataSource.data(), function (o, i) {
            if (o.IsChoose) {
                lstID.push(o.ID);
            }
        })
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận xóa trạm?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTower_COStationRemove",
                        data: { ListID: lstID, masterID: $scope.masterID },
                        success: function (res) {
                            grid.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });

        }
    }

    $scope.ApproveStation = function (e, item) {
        e.preventDefault();
        $scope.curTarget = e.target;
        var str = 'Xác nhận duyệt chi phí cho trạm này ?';
        if (!item.IsApproved) {
            str = 'Từ chối duyệt chi phí cho trạm này ?';
        }
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: str,
            pars: { e: e },
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                $($scope.curTarget).prop('checked', item.IsApproved);
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTower_COStationApprove",
                    data: { id: item.ID, value: item.IsApproved },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.Station_Grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            },
            Close: function () {

            }
        });

    }

    $scope.Station_SaveChanges = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        for (var i = 0; i < grid.dataSource.data().length; i++) {
            var item = grid.dataSource.data()[i];
            if (item.dirty) {
                data.push(item);
            }
        }
        if (data.length > 0)
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_COStationSaveChanges",
                data: { lst: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })

    }

    //#endregion

    //#region -Trouble

    $scope.TroubleDriverCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'Name',
        dataValueField: 'ID',
        change: function (e) {
            var grid = $scope.Trouble_Grid;
            var item = grid.dataItem($(e.sender.wrapper).closest('tr'));
            if (Common.HasValue(item)) {
                item.Name = this.text();
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

    $scope.Trouble_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TroubleList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    COTOID: { type: 'number', editable: true },
                    TroubleCostStatusID: { type: 'number', editable: false },
                    RoutingName: { type: 'string' },
                    Cost: { type: 'number', defaultValue: 0 },
                    CostOfCustomer: { type: 'number', defaultValue: 0 },
                    CostOfVendor: { type: 'number', defaultValue: 0 },
                    TroubleCostStatusName: { type: 'string' },

                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false, autoBind: false,
        edit: function (e) {
            e.model.COTOMasterID = $scope.masterID;
        },
        toolbar: kendo.template($('#win-to-trouble-grid-toolbar').html()), editable: 'incell', columns: [
           {
               title: ' ', width: '155px',
               template: ' ' +
                   '<a href="/" ng-show="true" ng-click="Trouble_Delete($event)" class="k-button"><i class="fa fa-trash"></i></a>' +
                   '<a href="/" ng-show="!true" ng-click="Grid_Cancel($event,Trouble_Grid)" class="k-button"><i class="fa fa-ban"></i></a>' +
                   '<a href="/" ng-click="OpenFile_Click($event,winfile,dataItem)" class="k-button"><i class="fa fa-paperclip"></i>Đính kèm</a>',
               filterable: false, sortable: false
           },
           {
               field: 'GroupOfTroubleName', width: 150, title: '{{RS.CATRouting.ParentName}}',
           },
           { title: '{{RS.SYSGroup.Description}}', field: 'Description', width: "150px", },
           {
               field: 'Cost', width: 100, title: '{{RS.CATCost.CostValue}}', editor: function (container, options) {

                   var input = $("<input kendo-numerictextbox k-on-change='ChangeProblemCostGrid(dataItem)' k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'CostOfCustomer', width: 100, title: '{{RS.CATTrouble.CostOfCustomer}}', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'CostOfVendor', width: 100, title: '{{RS.CATTrouble.CostOfVendor}}', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DriverID', width: 100, title: 'Tài xế', template: '#=DriverName == null ? "" : DriverName #', editor: function (container, options) {
                   var input = $("<input kendo-combobox k-options='TroubleDriverCbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'TroubleCostStatusID', width: 130, title: '{{RS.SYSVar1.ValueOfVar}}', template: '#=TroubleCostStatusName#', editor: function (container, options) {
                   if (options.model.TroubleCostStatusID == 0)
                       options.model.TroubleCostStatusID = _MONMonitor_ControlTowerCO.Data._trouble1stStatus;
                   var input = $("<input kendo-combobox k-options='TroubleCostStatus_CbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            if (isShowDetail) {
                $rootScope.Loading.Change("Thông tin cung đường...", 80);
                $scope.Location_Grid.dataSource.read();
            }
        }
    };

    $scope.GroupOfTrouble_CbbOptions = {
        autoBind: true, valuePrimitive: false, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'Name', dataValueField: 'ID', placeholder: "Chọn sự cố",
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { Name: { type: 'string' }, ID: { type: 'string' } }
            }
        })
    }

    //TroubleList
    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "DIMonitor_CATTroubleList",
        data: { isCo: true },
        success: function (res) {
            $scope.GroupOfTrouble_CbbOptions.dataSource.data(res.Data);
        }
    })

    $scope.Trouble_Add = function ($event, win, grid) {
        $event.preventDefault();
        win.center().open();
        grid.dataSource.read();
    }

    $scope.Trouble_AddNew = function ($event, win, grid) {
        $event.preventDefault();
        win.center().open();
        $scope.ItemDetail.GroupOfTroubleID = $scope.GroupOfTrouble_CbbOptions.dataSource.data()[0].ID;
        $scope.ItemDetail.Cost = 0;
        $scope.ItemDetail.CostOfCustomer = 0;
        $scope.ItemDetail.CostOfVendor = 0;
        $scope.ItemDetail.Description = "";
    }

    $scope.Trouble_SaveAll = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        for (var i = 0; i < grid.dataSource.data().length; i++) {
            var item = grid.dataSource.data()[i];
            if (item.dirty) {
                if (!Number.isFinite(item.CostOfCustomer))
                    item.CostOfCustomer = 0;
                if (!Number.isFinite(item.CostOfVendor))
                    item.CostOfVendor = 0;
                if (!Number.isFinite(item.Cost))
                    item.Cost = 0;
                data.push(item);
            }
        }
        if (data.length > 0)
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_TroubleSaveAll",
                data: { data: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.Trouble_Save = function ($event, vform, win) {
        $event.preventDefault();
        $scope.ItemDetail.COTOMasterID = $scope.masterID;
        if (!Common.HasValue($scope.ItemDetail.Cost))
            $scope.ItemDetail.Cost = 0;
        if (!Common.HasValue($scope.ItemDetail.CostOfCustomer))
            $scope.ItemDetail.CostOfCustomer = 0;
        if (!Common.HasValue($scope.ItemDetail.CostOfVendor))
            $scope.ItemDetail.CostOfVendor = 0;
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_TroubleSave",
                data: { item: $scope.ItemDetail },
                success: function (res) {
                    $scope.Trouble_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Trouble_LineSave = function ($event, grid) {
        $event.preventDefault();
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        var str = [];
        if (!Number.isFinite(item.CostOfCustomer))
            item.CostOfCustomer = 0;
        if (!Number.isFinite(item.CostOfVendor))
            item.CostOfVendor = 0;
        if (!Number.isFinite(item.Cost))
            item.Cost = 0;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TroubleSave",
            data: { item: item },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.Trouble_Grid.dataSource.read();
            }
        })
    }

    $scope.Trouble_SaveList = function ($event, grid1, grid2) {
        var data = $.grep(grid2.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_TroubleSaveList",
                data: { lst: data, masterID: $scope.masterID },
                success: function (res) {
                    grid1.dataSource.read();
                    grid2.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.ChangeProblemCost = function () {
        $scope.ItemDetail.CostOfVendor = $scope.ItemDetail.Cost;
    }

    $scope.ChangeProblemCostGrid = function (o) {
        o.CostOfVendor = o.Cost;
    }
    //#endregion

    //#region -Location

    $scope.Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MasterLocation",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SortOrderReal: { type: 'number' },
                    SortOrder: { type: 'number', editable: false },
                    LocationAddress: { type: 'text', editable: false },
                    LocationProvince: { type: 'text', editable: false },
                    LocationDistrict: { type: 'text', editable: false },
                    DITOLocationStatusName: { type: 'text', editable: false },
                    Comment: { type: 'text', editable: false },
                    DateComeEstimate: { type: 'date', editable: false },
                    DateLeaveEstimate: { type: 'date', editable: false },
                    DateCome: { type: 'date' },
                    DateLeave: { type: 'date' },
                }
            },
            sort: [{ field: "SortOrder", dir: "asc" }],
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: true, autoBind: false,
        //toolbar: kendo.template($('#location-toolbar').html()),
        dataBound: function (e) {
            $scope.LocationData = this.dataSource.data();
            if (isShowDetail) {
                $rootScope.Loading.Change("Thông tin vận hành...", 90);
                $scope.OPSCOTO_grid.dataSource.read();
            }
        },
        columns: [
           { title: 'STT', field: 'SortOrder', width: "40px", },
           {
               field: 'SortOrderReal', width: 80, title: 'STT thực',
           },
           {
               field: 'TypeOfTOLocationName', width: 100, title: '{{RS.TypeOfTOLocationName}}',
           },
           {
               field: 'DITOLocationStatusName', width: 100, title: '{{RS.MONMonitorIndex.WarningCount}}',
           },
           {
               field: 'LocationCode', width: 120, title: '{{RS.CATLocation.Code}}',
           },
           {
               field: 'LocationName', width: 150, title: '{{RS.CATLocation.Location}}',
           },
           {
               field: 'LocationAddress', width: 200, title: '{{RS.CATLocation.Address}}',
           },
           {
               field: 'DateCome', width: 170, title: '{{RS.OPSCOTOLocation.DateCome}}', template: '#=Common.Date.FromJsonDMYHM(DateCome)#',
               editor: function (container, options) {
                   var input = $("<input kendo-date-time-picker k-options='DateDMYHM'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DateLeave', width: 170, title: '{{RS.OPSCOTOLocation.DateLeave}}', template: '#=Common.Date.FromJsonDMYHM(DateLeave)#',
               editor: function (container, options) {
                   var input = $("<input kendo-date-time-picker k-options='DateDMYHM'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DateComeEstimate', width: 120, title: '{{RS.OPSCOTOLocation.DateComeEstimate}}', template: '#=Common.Date.FromJsonDMYHM(DateComeEstimate)#',
           },
           {
               field: 'DateLeaveEstimate', width: 120, title: '{{RS.OPSCOTOLocation.DateLeaveEstimate}}', template: '#=Common.Date.FromJsonDMYHM(DateLeaveEstimate)#',
           },
           {
               field: 'Comment', width: 300, title: '{{RS.OPSDITOLocation.Comment}}',
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    //#endregion

    var isShowDetail = false;

    $scope.TO_WinOptions = {
        width: '1025px', height: '100%',
        draggable: true, modal: true, resizable: false, title: false,
        open: function () {
            $timeout(function () {
                $rootScope.Loading.Show();
                isShowDetail = true;
                $scope.TODetail_ReloadAllGrid(7);
                $scope.TO_Splitter.resize();
            }, 100)
        },
        close: function () {
            isShowDetail = false;
            isChangePlan = false;
        }
    };

    $scope.TO_TabstripOptions = {
        height: "100%", animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            if (e.item.id == 'tab-trans') {
                $scope.Show_BtnWinTO_Update = true;
            } else {
                $scope.Show_BtnWinTO_Update = false;
            }
        }
    };

    $scope.tabStripOptions = {
        height: "100%", animation: { open: { effects: "fadeIn" } },
    };

    $scope.TimelineTabStripOptions = {
        height: "100%", animation: false,
        select: function (e) {

            var btn = $scope.ChangePlanContainer_scheduler.element.find(".k-state-selected a");
            $timeout(function () {
                $(btn).trigger('click');// nếu thực hiện cách refresh giống timeline dưới thì event bị trật thời gian 
                $scope.new_timeline_v2_Trip.view($scope.new_timeline_v2_Trip.viewName());
            }, 200)
        }
    }

    $scope.TO_SplitterOptions = {
        orientation: 'vertical',
        panes: [
            { collapsible: false, resizable: false, size: '150px' },
            { collapsible: false, resizable: false, },
        ]
    };

    //cbb

    $scope.Routing_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'RoutingName', dataValueField: 'TOID', placeholder: "Chọn tuyến đường",
        change: function (e) {
            var grid = $scope.Trouble_Grid;
            grid.dataItem($(e.sender.wrapper).closest('tr')).RoutingName = this.text();
        },
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { RoutingName: { type: 'string' }, TOID: { type: 'number' } }
            }
        })

    };

    //event


    $scope.LoadMasterDetail = function (masterID) {
        if (isChangePlan == true) {
            $scope.ChangePlanWin.center().open();
            MasterPlanID = masterID;
            $scope.IsChangePlan = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_MasterGet",
                data: { id: MasterPlanID },
                success: function (res) {
                    $scope.CurrentPlanMaster = res;
                    $scope.TO_Win_PlanTitle = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentPlanMaster.Code + ' - ETD: ' + $scope.CurrentPlanMaster.ETD + ' ETA: ' + $scope.CurrentPlanMaster.ETA;

                }
            })
            $scope.COContainerPlan_Grid.dataSource.read();
            $scope.TOContainerPlan_Grid.dataSource.read();
        }
        else {
            $rootScope.Loading.Show();
            $rootScope.Loading.Change("Thông tin vận chuyển...", 10);
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_MasterGet",
                data: { id: masterID },
                success: function (res) {
                    $scope.TO_Win.center().open();
                    $scope.CurrentMaster = res;
                    $scope.masterID = res.ID;
                    $scope.TO_Win_Title = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentMaster.Code + ' - ETD: ' + $scope.CurrentMaster.ETD + ' ETA: ' + $scope.CurrentMaster.ETA;
                    if ($scope.CurrentMaster.IsVehicleVendor) {
                        $scope.Show_DriverGrid = true;
                    } else {
                        $scope.Show_DriverGrid = false;
                    }

                    //load cung duong
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_RoutingList",
                        data: { masterID: $scope.masterID, locationID: null },
                        success: function (res) {
                            $scope._dataMsRouting = res.Data;
                            $scope.Routing_CbbOptions.dataSource.data(res.Data);
                        }
                    })

                    // load tai xe
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_MasterDriverList",
                        data: { masterID: $scope.masterID },
                        success: function (res) {
                            $scope.TroubleDriverCbbOptions.dataSource.data(res.Data);
                        }
                    })
                }
            });

        }
    }

    $scope.TODetail_ReloadAllGrid = function (ignore) {
        $scope.COContainer_Grid.dataSource.read();
        if (ignore != 7) {
            $scope.LoadDataTimeLine(function () {
                $rootScope.Loading.Hide();
            });
        }
    };
    //#endregion

    //#region Location map

    var _CATLocation = {
        Data: {
            Country: [],
            Province: [],
            District: [],
            Tmp: {}
        },
    }

    $scope.LocationItem = {
        Code: "",
        Location: "",
    };

    var LocationMap = openMapV2.Init({
        Element: 'LocationMap',
        Tooltip_Show: false,
        Tooltip_Element: 'MON_Map_tooltip',
        InfoWin_Show: false,
        InfoWin_Element: 'Map_Info_Win',
        DefinedLayer: [{
            Name: 'VectorMarker',
            zIndex: 100
        }, {
            Name: 'VectorProblem',
            zIndex: 100
        }, {
            Name: 'VectorXe',
            zIndex: 100
        }, {
            Name: 'VectorRoute',
            zIndex: 90
        }, {
            Name: 'VectorProvince',
            zIndex: 80
        }],
        ClickMarker: function (o, l) {
            $scope.LocationItem.Lat = o.Lat;
            $scope.LocationItem.Lng = o.Lng;
        },
        ClickMap: function (res) {
            Common.Log("Map click");
            openMapV2.ClearVector("VectorMarker");
            var img = Common.String.Format(openMapV2.NewImage.Location);
            var icon = openMapV2.NewStyle.Icon(img, 1);
            var o = openMapV2._to4326(res);
            $scope.LocationItem.Lat = o[1];
            $scope.LocationItem.Lng = o[0];
            openMapV2.NewMarker(o[1], o[0], $scope.LocationItem.Code, $scope.LocationItem.Location, icon, $scope.LocationItem, "VectorMarker");
            openMapV2.Center(o[1], o[0])
        }
    });

    $scope.NewLocationWinOptions = {
        width: '800', height: '640',
        draggable: true, modal: true, resizable: false, title: false, visible: false,
        close: function () {
            //openMapV2.Active(MainMap);
            MapNo = 1;
        },
        open: function () {
            openMapV2.Active(LocationMap);
            MapNo = 3;
            $timeout(function () {
                $scope.LocationSplitter.resize();

                //load vi tri hien tai cua xe
                openMapV2.ClearVector("VectorXe");
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "Extend_VehiclePosition_GetLast",
                    data: { vehicleCode: $scope.CurrentMaster.GPSCode, dtfrom: new Date() },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            if (Common.HasValue(res.Lng) && Common.HasValue(res.Lat)) {
                                var icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tractor.png', 1);
                                openMapV2.NewMarker(res.Lat, res.Lng, "ID", "VehicleCode", icon, res, "VectorXe");
                                openMapV2.Center(res.Lat, res.Lng, 10);
                            }
                        }

                    }

                })
            }, 400)
        },
        resize: function () {
            $timeout(function () {
                $scope.LocationSplitter.resize();
            }, 400)
        }
    };

    $scope.LocationSplitter_Options = {
        panes: [
                { collapsible: false, resizable: false, size: '50%' },
                { collapsible: false, resizable: false, size: '50%' }
        ],
        resize: function (e) {
            if (Common.HasValue(openMapV2))
                openMapV2.Resize();
        }
    };

    $scope.AddNewLocation_Open = function (e, win, type) {
        e.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: "CATLocation_Get",
            data: { 'ID': 0 },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.LoadRegionData($scope.LocationItem);
                win.center().open();
            }
        });
    }
    $scope.AddNewLocation_Open2 = function (e) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: "CATLocation_Get",
            data: { 'ID': 0 },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.LoadRegionData($scope.LocationItem);
            }
        });
    }

    $scope.AddNewLocation_Accept = function (e, win, vform) {
        e.preventDefault();
        if (vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận tạo điểm ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    win.close();
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: "CATLocation_Update",
                        data: { item: $scope.LocationItem },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.CatLocation_Grid.dataSource.read();
                            $scope.ReasonChange_Win.center().open();
                            $scope.COTOStopLocationID = res;
                            $scope.COTOStopLocationName = "[" + $scope.LocationItem.Code + "] " + $scope.LocationItem.Address;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                        }
                    });
                }
            });
        }
    }

    $scope.AddNewLocation_Accept2 = function (e, win, vform) {
        e.preventDefault();
        if (vform()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận tạo điểm ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    win.close();
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: "CATLocation_Update",
                        data: { item: $scope.LocationItem },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.CatLocation_Grid.dataSource.read();
                            Common.Services.Call($http, {
                                url: Common.Services.url.CAT,
                                method: "CATLocation_Get",
                                data: { 'ID': res },
                                success: function (r) {
                                    if ($scope.StandPopupType == 1) {
                                        if (standType == 1) {
                                            $scope.StandItem.Tractor = r;
                                        }
                                        else {
                                            $scope.StandItem.Romooc = r;
                                        }
                                        $scope.StandWin.close();
                                        $scope.MasterEndStationWin.center().open();
                                    }
                                    else {

                                        $scope.COTOStopLocationID = r.ID;
                                        $scope.COTOStopLocationName = "[" + r.Code + "]" + r.Address;
                                        $scope.StandWin.close();
                                        $scope.ReasonChange_Win.center().open();
                                    }
                                }
                            });
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                        }
                    });
                }
            });
        }
    }

    $scope.CATLocationEdit_win_numLatOptions = { format: 'n5', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }

    $scope.CATLocationEdit_win_numLngOptions = { format: 'n5', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }

    $scope.CATLocationEdit_win_numLoadTimeCOOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATLocationEdit_win_numUnLoadTimeCOOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATLocationEdit_win_numLoadTimeDIOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATLocationEdit_win_numUnLoadTimeDIOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.CATLocationEdit_win_cboCountryOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CountryName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.LocationItem.ProvinceID = -1;
                $scope.LocationItem.DistrictID = -1;
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.CountryID = "";
                $scope.LocationItem.ProvinceID = "";
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CATLocation.Data.Country = data;
            $scope.CATLocationEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.CATLocationEdit_win_cboProvinceOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ProvinceName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.LocationItem.DistrictID = -1;
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.ProvinceID = "";
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATLocation.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATLocation.Data.Province[obj.CountryID]))
                    _CATLocation.Data.Province[obj.CountryID].push(obj);
                else _CATLocation.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.CATLocationEdit_win_cboDistrictOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'DistrictName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATLocation.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATLocation.Data.District[obj.ProvinceID]))
                    _CATLocation.Data.District[obj.ProvinceID].push(obj);
                else _CATLocation.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })


    $scope.CATLocationEdit_win_cboWardOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'WardName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                WardName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
            }
            else {
                $scope.LocationItem.WardID = "";
            }
        }
    }

    $scope.CATLocationEdit_win_cboGOLOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfLocation,
        success: function (data) {
            var item = { ID: -1, GroupName: '' };
            data.unshift(item);
            $scope.CATLocationEdit_win_cboGOLOptions.dataSource.data(data)
        }
    })

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CATLocation.Data.Province[countryID];
            $scope.CATLocationEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CATLocation.Data.District[provinceID];
            $scope.CATLocationEdit_win_cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            //data = _SYSCustomer_Index.Data.Ward[districtID];
            //$scope.cboWardOptions.dataSource.data(data);
            //if (wardID < 1 && data.length > 0)
            //    wardID = data[0].ID;
            //$timeout(function () {
            //    item.WardID = wardID;
            //}, 1)
        }
        catch (e) { }
    }
    //#endregion

    //#region Common

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }

    //#endregion
}])