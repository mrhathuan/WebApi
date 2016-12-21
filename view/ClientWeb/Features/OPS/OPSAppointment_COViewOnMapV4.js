/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_COViewOnMapV4 = {
    URL: {
        Order_List: 'OPSCO_MAP_Order_List',
        Tractor_List: 'OPSCO_MAP_Tractor_List',
        Romooc_List: 'OPSCO_MAP_Romooc_List',
        Location_List: 'OPSCO_MAP_Location_List',
        Customer_List: 'OPSCO_MAP_Customer_List',
        Service_List: 'OPSCO_MAP_ServiceOfOrder_List',
        Seaport_List: 'OPSCO_MAP_Seaport_List',
        Carrier_List: 'OPSCO_MAP_Carrier_List',
        Vendor_List: 'OPSCO_MAP_Vendor_List',
        Driver_List: 'OPSCO_MAP_Driver_List',
        DriverVendor_List: 'OPSCO_MAP_DriverVendor_List',
        TOMaster_List: 'OPSCO_MAP_TOMaster_List',
        CheckVehicleAvailable: 'OPSCO_MAP_CheckVehicleAvailable',
        TripByVehicle_List: 'OPSCO_MAP_TripByVehicle_List',
        Schedule_Data: 'OPSCO_MAP_Schedule_Data',
        Save: 'OPSCO_MAP_Save',
        ToMon: 'OPSCO_MAP_ToMON',
        Cancel: 'OPSCO_MAP_Cancel',
        UpdateAndToMon: 'OPSCO_MAP_UpdateAndToMON',
        ToOPS: 'OPSCO_MAP_ToOPS',
        Delete: 'OPSCO_MAP_Delete',
        Setting: 'OPSCO_MAP_Setting',
        ToVendorKPI: 'OPSCO_MAP_ToVendorKPI',
        Vendor_WithKPI_List: 'OPSCO_MAP_Vendor_With_KPI_List',
        ToVendor: 'OPSCO_MAP_ToVendor',
        Split: 'OPSCO_MAP_COTOContainer_Split',
        Split_Cancel: 'OPSCO_MAP_COTOContainer_Split_Cancel',

        TripByID: "OPSCO_MAP_TripByID",
        VehicleVendor_List: 'OPSCO_MAP_VehicleVendor_List',
        COTOContainer_List: 'OPSCO_MAP_COTOContainer_List',
        COTOContainerByTrip_List: 'OPSCO_MAP_COTOContainer_ByTrip_List',
        CO2View_Container_List: 'OPSCO_MAP_2View_Container_List',
        CO2View_Master_Update_Check4Delete: 'OPSCO_MAP_2View_Master_Update_Check4Delete',
        CO2View_Master_Update_TimeLine: 'OPSCO_MAP_2View_Master_Update_TimeLine',
        CO2View_Master_Update_Container: 'OPSCO_MAP_2View_Master_Update_Container',
        CO2View_Master_Update: 'OPSCO_MAP_2View_Master_Update',
        CO2View_Master_ChangeVehicle: 'OPSCO_MAP_2View_Master_ChangeVehicle',
        CO2View_Master_Update_Driver: 'OPSCO_MAP_2View_Master_ChangeDriver',

        TimeLine_Create_Master: "OPSCO_MAP_TimeLine_Create_Master",
        TimeLine_UpdateContainer: "OPSCO_MAP_TimeLine_Update_Container",
        TimeLine_VehicleInfo: "OPSCO_MAP_TimeLine_Vehicle_Info",
        TimeLine_Master_Info: "OPSCO_MAP_TimeLine_Master_Container_List",

        TenderKPI_List: "OPSCO_MAP_Vendor_KPI_List",
        TenderKPI_Save: "OPSCO_MAP_Vendor_KPI_Save",
        New_Schedule_Data: "OPSCO_MAP_New_Schedule_Data",
        New_Schedule_TOMaster_List: "OPSCO_MAP_New_Schedule_COTOContainer_List",
        New_Vendor_Vehicle_Save: "OPSCO_MAP_Vehicle_New",
        New_Schedule_TOMaster_Save: "OPSCO_MAP_TimeLine_Create_Item",

        Info_Schedule_Data: 'OPSCO_MAP_Info_Schedule_Data',
        Info_Schedule_Save: 'OPSCO_MAP_Info_Schedule_DragDrop_Save',
        Info_Schedule_Save_Check: 'OPSCO_MAP_Info_Schedule_DragDrop_Save_Check',

        Vehicle_Schedule_Data: 'OPSCO_MAP_Vehicle_Schedule_Data',

        TimeLine_VehicleCheck: "OPSCO_MAP_Schedule_Check",
        TimeLine_ToTD_Time_Offer: 'OPSCO_MAP_Schedule_NewTime_Offer',
        TimeLine_ToTD_LeadTime_Offer: 'OPSCO_MAP_Schedule_LeadTime_Offer',
        TimeLine_TOMaster_Vehicle_Offer: 'OPSCO_MAP_Schedule_TOMaster_Vehicle_Offer',
        TimeLine_TOMaster_Vehicle_Update: 'OPSCO_MAP_Schedule_TOMaster_Change_Vehicle',
        TimeLine_TOMaster_Time_Update: 'OPSCO_MAP_Schedule_TOMaster_Change_Time'
    },
    Data: {
        Location: {
            LocationStartID: -1,
            LocationStartName: '',
            LocationEndID: -1,
            LocationEndName: '',
            LocationRomoocID: -1,
            LocationRomoocName: '',
            LocationStartLat: null,
            LocationStartLng: null,
            LocationEndLat: null,
            LocationEndLng: null,
            LocationRomoocLat: null,
            LocationRomoocLng: null,
        },
        VendorList: [],
        VehicleInfo: []
    }
}

angular.module('myapp').controller('OPSAppointment_COViewOnMapV4Ctrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COViewOnMapV4Ctrl');
    $rootScope.IsLoading = false;

    $scope.IsFullScreen = false;
    $scope.RomoocIsNotFound = true; //Xác định điểm lấy romooc
    $scope.RomoocMustReturn = true; //Ràng buộc trả romooc
    $scope.Color = { None: '#f6fafe', Error: '#fc0000', Warning: 'ffaa00', Success: '#31B6FC' };

    //Lấy thông tin thiết lập hệ thống gồm: ràng buộc trả romooc, điểm bắt đầu, điểm kết thúc
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV4.URL.Setting,
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.RomoocMustReturn = res.RomoocReturn;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartID = res.LocationStartID;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartName = res.LocationStartName;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndID = res.LocationEndID;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndName = res.LocationEndName;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLat = res.LocationEndLat;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLng = res.LocationEndLng;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLat = res.LocationStartLat;
                _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLng = res.LocationStartLng;
            }
        }
    });

    $scope.IsNewTimeLineV2Bound = false;
    $scope.IsShowNewTimeLineV2 = false;
    $scope.NewTimeLineLoadingV2 = true;
    $scope.TimeLineViewOrderDate = false;
    $scope.TimeLineViewOrderV2 = true;
    $scope.ShowTimeLineV2OrderDate = false;
    $scope.IsShowTimeLineWithVehicle = true;
    $scope.TimeLineV2DateRequest = { fData: null, tData: null }
    $scope.NewTimeLineDetail = false;
    $scope.NewTimeLineVehicleSearchString = "";
    $scope.NewTimeLineResourceType = 1;
    $scope.NewTimeLineVehicleDataStatus = [];
    $scope.NewTimeLineVehicleDataCustomer = [];
    $scope.NewTimeLineVehicleDataService = [];
    $scope.NewTimeLineVehicleDataCarrier = [];
    $scope.NewTimeLineVehicleDataSeaport = [];
    $scope.NewTimeLineVehicleItem = {
        ID: -1, VendorID: -1, VehicleVendorCode: '',
        RegNo: '', MaxWeight: 0, RomoocNo: '', DriverName: ''
    }
    $scope.TimeLineViewVehicleV2 = true;
    $scope.TimeLineEventDragDrop = false;
    $scope.TimeLineViewOrderV2WithTimeLineFilter = false;
    $scope.TimeLineToMonAvailable = false;
    $scope.TimeLineToOpsAvailable = false;

    $scope.LoadNewTimeLineV2Data = function (flag) {
        if (flag == null || flag == undefined)
            flag = true;
        $scope.NewTimeLineLoadingV2 = true;
        $scope.IsNewTimeLineV2Bound = false;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.New_Schedule_Data,
            data: {
                isShowVehicle: $scope.IsShowTimeLineWithVehicle,
                strVehicle: $scope.NewTimeLineVehicleSearchString,
                typeOfResource: $scope.NewTimeLineResourceType,
                dataCus: $scope.NewTimeLineVehicleDataCustomer,
                dataSer: $scope.NewTimeLineVehicleDataService,
                dataCar: $scope.NewTimeLineVehicleDataCarrier,
                dataSea: $scope.NewTimeLineVehicleDataSeaport,
                dataStt: $scope.NewTimeLineVehicleDataStatus
            },
            success: function (res) {
                $scope.IsShowNewTimeLineV2 = true;
                if ($scope.NewTimeLineResourceType == 1) {
                    Common.Data.Each(res.DataSources, function (o) {
                        o.field = o.VendorID + "_" + o.VehicleID;
                        o.EmptyLeft = 0, o.EmptyWidth = 0;
                        if (o.ETDEmpty != null && o.ETAEmpty != null) {
                            o.ETDEmpty = Common.Date.FromJson(o.ETDEmpty);
                            o.ETAEmpty = Common.Date.FromJson(o.ETAEmpty);
                            var e = Common.Date.FromJson(o.EndDate) - Common.Date.FromJson(o.StartDate);
                            o.EmptyLeft = (o.ETDEmpty - Common.Date.FromJson(o.StartDate)) * 100 / e;
                            o.EmptyWidth = (o.ETAEmpty - o.ETDEmpty) * 100 / e;
                        }
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
                    $scope.new_timeline_v2_Trip.setDataSource(dataSource);

                    var data = [];
                    Common.Data.Each(res.Resources, function (o) {
                        var obj = {
                            value: o.VendorID + "_" + o.VehicleID, text: o.Text, VendorID: o.VendorID, VendorCode: o.VendorCode, VendorName: o.VendorName,
                            RomoocID: o.RomoocID, RomoocNo: o.RomoocNo, VehicleID: o.VehicleID, VehicleNo: o.VehicleNo, IsChoose: false
                        }
                        data.push(obj);
                    })
                    if (data.length == 0) {
                        data.push({
                            value: '-2_-1', text: "DL trống", VendorID: -1, VendorCode: "", VendorName: "",
                            RomoocID: -1, RomoocNo: "", VehicleID: -1, VehicleNo: "", IsChoose: false
                        });
                    }
                    $scope.new_timeline_v2_Trip.resources[0].dataSource.data(data);
                }
                else if ($scope.NewTimeLineResourceType == 2) {
                    Common.Data.Each(res.DataSources, function (o) {
                        o.field = o.VendorID + "_" + o.VehicleID;
                        o.EmptyLeft = 0, o.EmptyWidth = 0;
                        if (o.ETDEmpty != null && o.ETAEmpty != null) {
                            o.ETDEmpty = Common.Date.FromJson(o.ETDEmpty);
                            o.ETAEmpty = Common.Date.FromJson(o.ETAEmpty);
                            var e = Common.Date.FromJson(o.EndDate) - Common.Date.FromJson(o.StartDate);
                            o.EmptyLeft = (o.ETDEmpty - Common.Date.FromJson(o.StartDate)) * 100 / e;
                            o.EmptyWidth = (o.ETAEmpty - o.ETDEmpty) * 100 / e;
                        }
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
                    $scope.new_timeline_v2_Trip.setDataSource(dataSource);

                    var data = [];
                    Common.Data.Each(res.Resources, function (o) {
                        var obj = {
                            value: o.VendorID + "_" + o.RomoocID, text: o.Text, VendorID: o.VendorID, VendorCode: o.VendorCode, VendorName: o.VendorName,
                            RomoocID: o.RomoocID, RomoocNo: o.RomoocNo, VehicleID: o.VehicleID, VehicleNo: o.VehicleNo, IsChoose: false
                        }
                        data.push(obj);
                    })
                    if (data.length == 0) {
                        data.push({
                            value: '-2_-1', text: "DL trống", VendorID: -1, VendorCode: "", VendorName: "",
                            RomoocID: -1, RomoocNo: "", VehicleID: -1, VehicleNo: "", IsChoose: false
                        });
                    }
                    $scope.new_timeline_v2_Trip.resources[0].dataSource.data(data);
                } else if ($scope.NewTimeLineResourceType == 3) {
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
                                    field: { from: "GroupID" }
                                }
                            }
                        }
                    });
                    $scope.new_timeline_v2_Trip.setDataSource(dataSource);

                    var data = [];
                    Common.Data.Each(res.Resources, function (o) {
                        var obj = {
                            value: o.Value, text: o.Text, IsChoose: false, ETA: o.TOMasterETA, ETD: o.TOMasterETD
                        }
                        data.push(obj);
                    })
                    if (data.length == 0) {
                        data.push({
                            value: '-1', text: "DL trống", IsChoose: false
                        });
                    }
                    $scope.new_timeline_v2_Trip.resources[0].dataSource.data(data);
                }
            }
        })
        if (flag) {
            $scope.NewTimeLineContainerSelected = [];
            $scope.timeline_conV2_Grid.dataSource.read();
        }
    }

    $timeout(function () {
        $scope.LoadNewTimeLineV2Data(false);
    }, 100)

    $scope.timelineV2Splitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true, min: '500px' },
            { collapsible: true, resizable: true, size: '500px', min: '330px' }
        ],
        resize: function (e) {
            try {
                $scope.new_timeline_v2_Trip.refresh();
            }
            catch (e) { }
        }
    }

    $scope.new_timeline_v2_TripOptions = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: true, resize: true, update: false },
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
        groupHeaderTemplate: kendo.template("<input style='position:relative;top:2px;display:none !important;' type='checkbox' data-uid='#=value#' class='chk_vehicle_timeline' /><span data-uid='#=value#' class='txtTimeLineVehicle' style='cursor: pointer;' title='Click xem chi tiết'><strong>#=text#</strong></span><i class='fa fa-spinner fa-spin' style='color:rgb(248, 248, 248);padding: 0 2px;'></i>"),
        eventTemplate: $("#new-timeline-v2-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            $scope.TimeLineToMonAvailable = false;
            $scope.TimeLineToOpsAvailable = false;
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.IsNewTimeLineV2Bound == false && $scope.IsShowNewTimeLineV2 == true) {
                    $scope.IsNewTimeLineV2Bound = true;
                    scheduler.view(scheduler.view().name);
                    //scheduler.element.find('.k-nav-today a').trigger('click');
                } else if ($scope.IsNewTimeLineV2Bound == true && $scope.IsShowNewTimeLineV2 == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case -1:
                                        $(o).addClass('vendor-trip');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 1:
                                        if (i.StatusOfEvent == 1) {
                                            $(o).addClass('approved');
                                        } else if (i.StatusOfEvent == 2) {
                                            $(o).addClass('tendered');
                                            $(o).find('.k-resize-handle').hide();
                                        } else if (i.StatusOfEvent == 3) {
                                            $(o).addClass('recieved');
                                            $(o).find('.k-resize-handle').hide();
                                        } else if (i.StatusOfEvent == 11) {
                                            $(o).addClass('tenderable');
                                        } else {
                                            $(o).addClass('undefined');
                                            $(o).find('.k-resize-handle').hide();
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

                    if ($scope.NewTimeLineResourceType < 3) {
                        scheduler.element.find('span.txtTimeLineVehicle').each(function () {
                            $(this).unbind('click');
                            $(this).click(function (e) {
                                var uid = $(e.currentTarget).data('uid');

                                $scope.NewTimeLineVehicleItem.ID = uid.split('_')[1];
                                $scope.NewTimeLineVehicleItem.VendorID = uid.split('_')[0];
                                if ($scope.NewTimeLineVehicleItem.ID > 0) {
                                    if ($scope.NewTimeLineVehicleItem.VendorID == -1) {
                                        $scope.vehicle_map_win.center().open();
                                        $scope.ChangeVehicleType = $scope.NewTimeLineResourceType;
                                        $scope.IsVehicleMapActived = true;
                                        $scope.VehicleMapRequestDate = new Date();
                                        $timeout(function () {
                                            switch ($scope.ChangeVehicleType) {
                                                case 1:
                                                    $scope.vehMap_Grid.dataSource.read();
                                                    break;
                                                case 2:
                                                    $scope.romMap_Grid.dataSource.read();
                                                    break;
                                                default:
                                                    break;
                                            }
                                            $rootScope.IsLoading = false;
                                        }, 100);
                                    } else {
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_VehicleInfo,
                                            data: { venID: $scope.NewTimeLineVehicleItem.VendorID, romID: -1, vehID: $scope.NewTimeLineVehicleItem.ID, now: new Date() },
                                            success: function (res) {
                                                $scope.NewTimeLineVehicleItem.VehicleVendorCode = res.VehicleVendorCode;
                                                $scope.NewTimeLineVehicleItem.MaxWeight = res.MaxWeight;
                                                $scope.NewTimeLineVehicleItem.RegNo = res.Regno;
                                                $scope.NewTimeLineVehicleItem.RomoocNo = res.RomoocNo;
                                                $scope.NewTimeLineVehicleItem.DriverName = res.DriverName;
                                                $timeout(function () {
                                                    $scope.new_timeline_vehicle_info_win.center().open();
                                                }, 100)
                                            }
                                        });
                                    }
                                }
                            })
                        })
                        scheduler.element.find('.chk_vehicle_timeline').each(function () {
                            $(this).change(function (e) {
                                var uid = $(e.target).data('uid');
                                var flag = $(e.target).prop('checked');
                                var data = scheduler.resources[0].dataSource.data();
                                Common.Data.Each(data, function (o) {
                                    if (o.value == uid) {
                                        o.IsChoose = flag;
                                    }
                                })
                                var tmp = [];
                                Common.Data.Each(data, function (o) {
                                    if (o.IsChoose == true) {
                                        tmp[o.value] = true;
                                    }
                                })

                                var view = scheduler.view();
                                var fDate = view.startDate();
                                var tDate = view.endDate();
                                $scope.NewTimeLineToMonAvailable = false;
                                $scope.NewTimeLineToOpsAvailable = false;
                                Common.Data.Each(scheduler.dataSource.data(), function (o) {
                                    if (tmp[o.field] && ((o.start > fDate && o.start < tDate) || (o.end > fDate && o.end < tDate))) {
                                        if (o.Status == 1) {
                                            $timeout(function () {
                                                $scope.NewTimeLineToMonAvailable = true;
                                            }, 1)
                                        } else {
                                            $timeout(function () {
                                                $scope.NewTimeLineToOpsAvailable = true;
                                            }, 1)
                                        }
                                    }
                                })
                            })
                        })
                        scheduler.element.find('.k-scheduler-content tr td').each(function (idx, td) {
                            var slot = scheduler.slotByElement(td), resource = scheduler.resources[0].dataSource.data();
                            if (Common.HasValue(slot) && Common.HasValue(resource[slot.groupIndex])) {
                                var uid = resource[slot.groupIndex].value;
                                if (uid != null && uid.split('_')[1] == -1 && uid.split('_')[0] >= -1) {
                                    $(td).css('background', 'rgb(255, 249, 158)');
                                }
                            }
                        })
                        scheduler.element.find('.k-scheduler-times tr').each(function (idx, tr) {
                            var uid = $(tr).find('input.chk_vehicle_timeline').data('uid');
                            if (uid != null && uid.split('_')[1] == -1 && uid.split('_')[0] >= -1) {
                                $(tr).css('background', 'rgb(255, 249, 158)');
                                $(tr).find('i').css('color', 'rgb(255, 249, 158)');
                            }
                        })
                    } else {
                        scheduler.element.find('span.txtTimeLineVehicle').each(function () {
                            $(this).unbind('click');
                            $(this).click(function (e) {
                                var obj, uid = $(e.currentTarget).data('uid'), data = scheduler.resources[0].dataSource.data();
                                Common.Data.Each(data, function (o) { if (o.value == uid) obj = o; })
                                if (obj != null) {
                                    var view = scheduler.view(), start = view.startDate(), end = view.endDate(), etd = Common.Date.FromJson(obj.ETD)
                                    tbody1 = scheduler.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                                    top = tbody1.scrollTop();
                                    if (start == end)
                                        end = end.addDays(1);
                                    if (start <= etd && etd <= end) {
                                        $scope.TimeLineToTD = true;
                                        $scope.MoveToTime(scheduler, uid, etd, top);
                                    } else {
                                        $scope.TimeLineToTD = true;
                                        $scope.TimeLineToUID = uid;
                                        $scope.TimeLineToTime = etd;
                                        $scope.TimeLineToTop = top;
                                        scheduler.date(etd);
                                    }
                                }
                            })
                        })
                    }

                    $scope.MoveToTime(scheduler, $scope.TimeLineToUID, $scope.TimeLineToTime, $scope.TimeLineToTop);

                    $scope.InitDragAndDropV2();
                    if ($scope.NewTimeLineResourceType == 1) {
                        var thGroup = angular.element('.new-timeline-trip-v2 .k-scheduler-layout>tbody>tr>td>.k-scheduler-times .k-scheduler-table:eq(0) tr:eq(0)>th');
                        thGroup.empty();
                        thGroup.append($compile("<a href='/' style='width:100%;' title='Nhấn để đổi cách hiển thị' class='k-button' ng-click='NewTimeLineVehicleVisible_Click($event)'>Ẩn/hiện xe</a>")($scope));
                    }
                    if ($scope.NewTimeLineResourceType < 3 && $scope.IsShowTimeLineWithVehicle) {
                        var thVehicle = angular.element('.new-timeline-trip-v2 .k-scheduler-layout>tbody>tr>td>.k-scheduler-times .k-scheduler-table:eq(0) tr:eq(1)>th');
                        thVehicle.empty();
                        thVehicle.append($compile("<input class='k-textbox my-textbox' ng-model='NewTimeLineVehicleSearchString' style='width:calc(100% - 26px);text-align:center;font-weight: bold;color:#46bdfc;'/><a href='/' class='k-button' ng-click='NewTimeLineVehicleFilter_Click($event)'><i class='fa fa-refresh'></i></a>")($scope));
                    }
                    $scope.$apply();
                    if ($scope.TimeLineViewTripAction) {
                        $scope.InitTimeLineViewActionDragDrop(scheduler);
                    }
                    $timeout(function () {
                        $scope.NewTimeLineLoadingV2 = false;
                    }, 1000)
                    if ($scope.TineLineEventCallBack != null)
                        $scope.TineLineEventCallBack(scheduler);
                }
            }, 10)
        },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '-1', text: 'Data Empty' }], multiple: true
            }
        ],
        moveStart: function (e) {
            if (!$scope.TimeLineViewTripAction) {
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || $scope.NewTimeLineResourceType != 1) {
                    e.preventDefault();
                } else {
                    $scope.HideTimeLineV2Tooltip();
                    $scope.TimeLineEventDragDrop = true;
                    $scope.NewTripTimeLineEdit = $.extend(true, {}, e.event);
                }
            } else {
                e.preventDefault();
            }
        },
        resizeStart: function (e) {
            if (!$scope.TimeLineViewTripAction) {
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || $scope.NewTimeLineResourceType != 1) {
                    e.preventDefault();
                } else {
                    $scope.TimeLineEventDragDrop = true;
                    $scope.HideTimeLineV2Tooltip();
                    $scope.NewTripTimeLineEdit = $.extend(true, {}, e.event);
                }
            } else {
                e.preventDefault();
            }
        },
        save: function (e) {
            e.preventDefault();
            $scope.TimeLineEventDragDrop = false;
            $scope.HideTimeLineV2Tooltip();
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
                    item.end = $scope.NewTripTimeLineEdit.end;
                    item.start = $scope.NewTripTimeLineEdit.start;
                    item.field = $scope.NewTripTimeLineEdit.field;
                }
                sch.refresh();
                $rootScope.IsLoading = false;
            }
            var ds = new kendo.data.DataSource({
                data: data,
                filter: [{
                    logic: 'or',
                    filters: [{
                        logic: 'and',
                        filters: [
                            { field: 'start', operator: 'gte', value: obj.start },
                            { field: 'end', operator: 'gte', value: obj.start },
                            { field: 'start', operator: 'lt', value: obj.end }
                        ]
                    }, {
                        logic: 'and',
                        filters: [
                            { field: 'start', operator: 'lte', value: obj.start },
                            { field: 'end', operator: 'gt', value: obj.start }
                        ]
                    }, {
                        logic: 'and',
                        filters: [
                            { field: 'start', operator: 'lte', value: obj.end },
                            { field: 'end', operator: 'gt', value: obj.end }
                        ]
                    }]
                }, {
                    field: 'field', operator: 'eq', value: field
                }, {
                    logic: 'or',
                    filters: [{
                        field: 'StatusOfEvent', operator: 'eq', value: 1
                    }, {
                        field: 'StatusOfEvent', operator: 'eq', value: 2
                    }, {
                        field: 'StatusOfEvent', operator: 'eq', value: 11
                    }]
                }]
            })
            ds.fetch(function () {
                var view = this.view();
                if (view.length > 0 && $.grep(view, function (o) { return o.id == obj.id }).length == 0) {
                    var objDrop = view[0];
                    if (objDrop.ListContainer.length == 1) {
                        if (obj.ListContainer.length == 1 && obj.ListContainer[0].Qty == 2) {
                            var flag = false;
                            if (obj.ListContainer[0].ServiceType == 1 && objDrop.ListContainer[0].ServiceType == 2) flag = true;
                            if (obj.ListContainer[0].ServiceType == 3 && objDrop.ListContainer[0].ServiceType == 1) flag = true;
                            if (obj.ListContainer[0].ServiceType == 3 && objDrop.ListContainer[0].ServiceType == 2) flag = true;
                            if (flag) {
                                $scope.TimeLineToTD = true;
                                $scope.LoadDataDragDropInfo(objDrop.id, obj.id, -1, -1, -1, new Date(), new Date(), [], []);
                                _refreshScheduler(scheduler);
                            } else {
                                $rootScope.Message({
                                    Type: Common.Message.Type.Alert,
                                    Msg: "Không thể thêm đơn hàng vào chuyến! Chặng không phù hợp!"
                                })
                                _refreshScheduler(scheduler);
                            }
                        }
                        else if (obj.ListContainer.length == 1 && obj.ListContainer[0].Qty == 1) {
                            var flag = false;
                            if (obj.ListContainer[0].ServiceType == 1 && objDrop.ListContainer[0].ServiceType == 1) flag = true;
                            if (obj.ListContainer[0].ServiceType == 2 && objDrop.ListContainer[0].ServiceType == 2) flag = true;
                            if (obj.ListContainer[0].ServiceType == 3 && objDrop.ListContainer[0].ServiceType == 3) flag = true;
                            if (flag) {
                                flag = obj.MaxQty = 2 && obj.MaxTon >= obj.Ton + objDrop.Ton;
                                if (flag) {
                                    $scope.TimeLineToTD = true;
                                    $scope.LoadDataDragDropInfo(objDrop.id, obj.id, -1, -1, -1, new Date(), new Date(), [], []);
                                    _refreshScheduler(scheduler);
                                } else {
                                    $rootScope.Message({
                                        Type: Common.Message.Type.Alert,
                                        Msg: "Không thể thêm đơn hàng vào chuyến! Quá trọng tải hoặc kích thước!"
                                    })
                                    _refreshScheduler(scheduler);
                                }
                            } else {
                                $rootScope.Message({
                                    Type: Common.Message.Type.Alert,
                                    Msg: "Không thể thêm đơn hàng vào chuyến! Chặng không phù hợp!"
                                })
                                _refreshScheduler(scheduler);
                            }
                        } else {
                            $rootScope.Message({
                                Type: Common.Message.Type.Alert,
                                Msg: "Không thể thêm đơn hàng vào chuyến! Quá số container cho phép!"
                            })
                            _refreshScheduler(scheduler);
                        }
                    } else {
                        $rootScope.Message({
                            Type: Common.Message.Type.Alert,
                            Msg: "Không thể gộp chuyến! Quá số container quy định!"
                        })
                        _refreshScheduler(scheduler);
                    }
                } else {
                    var venID = -1, vehID = -1;
                    if (typeof obj.field == "string") {
                        venID = obj.field.split('_')[0], vehID = obj.field.split('_')[1];
                    } else if (typeof obj.field == "object") {
                        venID = obj.field[0].split('_')[0], vehID = obj.field[0].split('_')[1];
                    }
                    if (vehID < 1) {
                        if ($scope.NewTimeLineResourceType == 1) {
                            $rootScope.Message({
                                Msg: "Không thể lưu. Vui lòng chọn xe.", Type: Common.Message.Type.Alert
                            })
                        } else if ($scope.NewTimeLineResourceType == 2) {
                            $rootScope.Message({
                                Msg: "Không thể lưu. Vui lòng chọn romooc.", Type: Common.Message.Type.Alert
                            })
                        }
                        _refreshScheduler(scheduler);
                    } else {
                        //Time
                        if (vehID == obj.VehicleID) {
                            if (venID > 0) {
                                var oj = $.extend(true, {}, obj);
                                oj.ID = obj.id; oj.ETD = obj.start; oj.ETA = obj.end;
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_ToTD_Time_Offer,
                                    data: { item: oj },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            if (res.ListCOContainer == null || res.ListCOContainer == []) {
                                                $rootScope.Message({ Msg: "Thời gian không hợp lệ!", Type: Common.Message.Type.Alert })
                                                _refreshScheduler(scheduler);
                                            } else {
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({
                                                    Msg: "Xác nhận lưu thay đổi?",
                                                    Type: Common.Message.Type.Confirm,
                                                    Ok: function () {
                                                        $rootScope.IsLoading = true;
                                                        Common.Services.Call($http, {
                                                            url: Common.Services.url.OPS,
                                                            method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Time_Update,
                                                            data: { mID: obj.id, ETD: obj.start, ETA: obj.end, dataContainer: res.ListCOContainer },
                                                            success: function (res) {
                                                                Common.Services.Error(res, function (res) {
                                                                    $scope.ChangeData = true;
                                                                    $rootScope.IsLoading = false;
                                                                    $rootScope.Message({ Msg: 'Thành công!' })
                                                                    scheduler.refresh();
                                                                }, function () {
                                                                    _refreshScheduler(scheduler);
                                                                })
                                                            },
                                                            error: function () {
                                                                _refreshScheduler(scheduler);
                                                            }
                                                        });
                                                    },
                                                    Close: function () {
                                                        $rootScope.IsLoading = true;
                                                        _refreshScheduler(scheduler);
                                                    }
                                                })
                                            }
                                        }, function () {
                                            _refreshScheduler(scheduler);
                                        })
                                    },
                                    error: function () {
                                        _refreshScheduler(scheduler);
                                    }
                                });
                            }
                            else {
                                //==X==
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_COViewOnMapV4.URL.TripByID,
                                    data: { masterID: obj.id },
                                    success: function (res) {
                                        if (Common.HasValue(res) && res.ID > 0) {
                                            $scope.NewTimeLineItem = {
                                                ID: res.ID,
                                                Code: res.Code,
                                                VehicleNo: res.VehicleNo,
                                                DriverName: res.DriverName,
                                                DriverTel: res.DriverTel,
                                                StatusCode: 'Có thể cập nhật',
                                                StatusColor: $scope.Color.None,
                                                VehicleID: $scope.NewTimeLineResourceType == 1 ? vehID : res.VehicleID,
                                                RomoocID: $scope.NewTimeLineResourceType == 2 ? vehID : res.RomoocID,
                                                RomoocNo: res.RomoocNo,
                                                VendorOfVehicleID: res.VendorOfVehicleID,
                                                VendorOfVehicleCode: res.VendorCode,
                                                Ton: res.TotalTon,
                                                Status: res.Status,
                                                IsRomoocBreak: $scope.RomoocMustReturn,
                                                ETA: obj.end,
                                                ETD: obj.start,
                                                ListOPSCon: [], ListORDCon: [],
                                                LocationStartID: res.LocationStartID,
                                                LocationStartName: res.LocationStartName,
                                                LocationEndID: res.LocationEndID,
                                                LocationEndName: res.LocationEndName,
                                                LocationStartLat: res.LocationStartLat,
                                                LocationStartLng: res.LocationStartLng,
                                                LocationEndLat: res.LocationEndLat,
                                                LocationEndLng: res.LocationEndLng,
                                                ListLocation: res.ListLocation,
                                                MinInterval: 0.5,
                                                IsAllowChangeRomooc: true,
                                                TimeMin: null, TimeMax: null,
                                                HasChange: false
                                            }
                                            if ($scope.NewTimeLineItem.VendorOfVehicleID == null) {
                                                $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                                            }
                                            $scope.NewTimeLineItemTemp = $.extend(true, {}, $scope.NewTimeLineItem);

                                            $scope.TimeLineInfoTabIndex = 1;
                                            $scope.NewTimeLineDetail = true;
                                            $scope.LoadDataNewTimeLineInfo(false);
                                            $scope.new_timeline_trip_info_Grid.dataSource.read();
                                            $timeout(function () {
                                                $scope.NewTimeLineItem.ETA = obj.end;
                                                $scope.NewTimeLineItem.ETD = obj.start;
                                                $rootScope.IsLoading = false;
                                                $scope.timeline_trip_info_win.center().open();
                                            }, 100)
                                            $scope.TimeLineToUID = field;
                                        }
                                    }
                                });
                                _refreshScheduler(scheduler);
                            }
                        } else {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Vehicle_Offer,
                                data: { mID: obj.id, venID: venID, vehID: vehID, isTractor: $scope.NewTimeLineResourceType == 1 },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        if (res != null && res.OfferTimeError != null && res.OfferTimeError != "") {
                                            $rootScope.Message({ Msg: res.OfferTimeError, Type: Common.Message.Type.Alert })
                                            _refreshScheduler(scheduler);
                                        } else {
                                            var msg = "Xác nhận lưu thay đổi?";
                                            if (res.OfferTimeWarning != null && res.OfferTimeWarning != "")
                                                msg = res.OfferTimeWarning + ", tiếp tục lưu thay đổi?";
                                            $rootScope.IsLoading = false;
                                            $rootScope.Message({
                                                Msg: msg,
                                                Type: Common.Message.Type.Confirm,
                                                Ok: function () {
                                                    $rootScope.IsLoading = true;
                                                    Common.Services.Call($http, {
                                                        url: Common.Services.url.OPS,
                                                        method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Vehicle_Update,
                                                        data: { mID: obj.id, venID: venID, vehID: vehID, isTractor: $scope.NewTimeLineResourceType == 1 },
                                                        success: function (res) {
                                                            Common.Services.Error(res, function (res) {
                                                                $scope.ChangeData = true;
                                                                $rootScope.IsLoading = false;
                                                                $rootScope.Message({ Msg: 'Thành công!' })
                                                                scheduler.refresh();
                                                            }, function () {
                                                                _refreshScheduler(scheduler);
                                                            })
                                                        },
                                                        error: function () {
                                                            _refreshScheduler(scheduler);
                                                        }
                                                    });
                                                },
                                                Close: function () {
                                                    $rootScope.IsLoading = true;
                                                    _refreshScheduler(scheduler);
                                                }
                                            })
                                        }
                                    }, function () {
                                        _refreshScheduler(scheduler);
                                    })
                                },
                                error: function () {
                                    _refreshScheduler(scheduler);
                                }
                            });
                        }
                    }
                }
            })
        }
    }

    $scope.TimeLineToTD = false;
    $scope.TimeLineToUID = -1;
    $scope.TimeLineToTime = null;
    $scope.TimeLineToTop = 0;
    $scope.MoveToTime = function (scheduler, uid, time, top) {
        if ($scope.TimeLineToTD) {
            $scope.TimeLineToTD = false;
            var tbody1 = scheduler.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times');
            var tbody2 = scheduler.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content');
            if (top == null) top = tbody1.scrollTop();
            var left = 0;
            if (uid != null) {
                scheduler.element.find('.chk_vehicle_timeline').each(function (i) {
                    if (angular.element(this).data('uid') == uid) {
                        var cells = tbody2.find('tr:eq(' + i + ') > td'), total = cells.length;
                        for (var idx = 0; idx < total; idx++) {
                            var slot = scheduler.slotByElement(cells[idx]);
                            if (Common.HasValue(slot) && slot.startDate <= time && time < slot.endDate) {
                                left = angular.element(cells[idx]).offset().left;
                            }
                        }
                    }
                })
            } else {
                var cells = tbody2.find('tr:eq(0) > td'), total = cells.length;
                for (var idx = 0; idx < total; idx++) {
                    var slot = scheduler.slotByElement(cells[idx]);
                    if (Common.HasValue(slot) && slot.startDate <= time && time < slot.endDate) {
                        left = angular.element(cells[idx]).offset().left;
                    }
                }
            }
            try {
                var valLeft = left - tbody2.offset().left + tbody2.scrollLeft() - 30;
                tbody1.animate({ scrollTop: top });
                tbody2.animate({ scrollTop: top });
                tbody2.animate({ scrollLeft: valLeft });
            } catch (e) { }
        }
    }

    $scope.TimelineOrderV2ViewResource_Click = function ($event, scheduler, value) {
        $event.preventDefault();

        $scope.NewTimeLineResourceType = value;
    }

    $scope.$watch("NewTimeLineResourceType", function () {
        if ($scope.IsShowNewTimeLineV2 && $scope.InitComplete)
            $scope.LoadNewTimeLineV2Data(false);
    })
    
    $scope.NewTimeLineContainerSelected = [];
    $scope.timeline_conV2_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.Order_List,
            pageSize: 20,
            readparam: function () {
                return {
                    typeOfOrder: $scope.TimeLineViewOrderV2 ? 1 : 2,
                    fDate: $scope.TimeLineV2DateRequest.fDate,
                    tDate: $scope.TimeLineV2DateRequest.tDate,
                    dataService: $scope.TimeLineViewOrderV2WithTimeLineFilter ? $scope.NewTimeLineVehicleDataService : [],
                    dataCarrier: $scope.TimeLineViewOrderV2WithTimeLineFilter ? $scope.NewTimeLineVehicleDataCarrier : [],
                    dataSeaport: $scope.TimeLineViewOrderV2WithTimeLineFilter ? $scope.NewTimeLineVehicleDataSeaport : [],
                    dataCus: $scope.TimeLineViewOrderV2WithTimeLineFilter ? $scope.NewTimeLineVehicleDataCustomer : []
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETDRequest: { type: 'date' },
                    ETARequest: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateReturnEmpty: { type: 'date' }
                }
            }
        }),
        pageable: Common.PageSize,
        height: '99%', groupable: true, sortable: true, columnMenu: false, resizable: true, filterable: { mode: 'row' }, reorderable: false,
        dataBound: function () {
            var grid = this;
            Common.Data.Each(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && Common.HasValue($scope.NewTimeLineContainerSelected[item.ID])) {
                    item.IsChoose = true;
                    $(tr).find('.chkChoose').prop('checked', true);
                }
            })
        },
        columns: [
            {
                field: "Choose", title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_conV2_Grid,timeline_conV2_Grid_Choose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_conV2_Grid,timeline_conV2_Grid_Choose_Change)" />',
                filterable: false, sortable: false, groupable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            //{
            //    field: 'Command', title: ' ', width: '30px', hidden: true,
            //    attributes: { style: 'text-align: center;' },
            //    template: '<a class="k-button small-button btn-split" title="Chia" href="/" ng-click="Container_Split($event,dataItem,map_win)"><i class="fa fa-minus"></i></a>' +
            //        '<a class="k-button small-button btn-merge" ng-show="dataItem.ParentID>0" title="Gộp" href="/" ng-click="Container_Merge($event,dataItem,con_Grid)">M</a>' +
            //        '<a class="k-button small-button btn-merge-ok" style="display:none;" title="Xác nhận" href="/" ng-click="Container_Merge_OK($event,dataItem,con_Grid)">S</a>' +
            //        '<input type="checkbox" style="display:none;margin:0px 11px;" class="chk-select-to-merge" />',
            //    filterable: false, sortable: false, groupable: false, sortorder: 1, configurable: false, isfunctionalHidden: true
            //},
            {
                field: 'IsWarning', width: 60, title: ' ', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<i style="font-size: 20px;color: rgb(249, 73, 73);padding: 0 5px;" ng-show="dataItem.IsChoose" class="fa fa-qrcode"></i><img class="img-warning" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>',
                filterable: false, sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerCode', width: 150, title: 'Khách hàng',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:CustomerCode#</a>#}else{##:CustomerCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerShortName', width: 150, title: 'Tên ngắn khách hàng',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:CustomerShortName#</a>#}else{##:CustomerShortName##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: 100, title: 'Mã ĐH',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:OrderCode#</a>#}else{##:OrderCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfContainerName', width: 80, title: 'Loại cont',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:TypeOfContainerName#</a>#}else{##:TypeOfContainerName##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ServiceOfOrderName', width: 100, title: 'Loại v/c',
                template: '#if(ServiceOfOrderName>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ServiceOfOrderName#</a>#}else{##:ServiceOfOrderName##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'StatusOfContainerName', width: 120, title: 'T/trạng con.', hidden: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 8, configurable: true, isfunctionalHidden: true
            },
            {
                field: 'ContainerNo', width: 100, title: 'Số con.',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ContainerNo#</a>#}else{##:ContainerNo##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: 100, title: 'Trọng tải',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Ton#</a>#}else{##:Ton##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: 160, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: 160, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETDRequest', width: 160, title: 'ETD yêu cầu', template: "#=ETDRequest==null?' ':kendo.toString(ETDRequest, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETARequest', width: 160, title: 'ETA yêu cầu', template: "#=ETARequest==null?' ':kendo.toString(ETARequest, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateGetEmpty', width: 160, title: 'Ngày lấy rỗng', template: "#=DateGetEmpty==null?' ':kendo.toString(DateGetEmpty, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateReturnEmpty', width: 160, title: 'Ngày trả rỗng', template: "#=DateReturnEmpty==null?' ':kendo.toString(DateReturnEmpty, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromCode', width: 150, title: 'Điểm nhận',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationFromCode#</a>#}else{##:LocationFromCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToCode', width: 150, title: 'Điểm giao',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationToCode#</a>#}else{##:LocationToCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationFromAddress#</a>#}else{##:LocationFromAddress##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationToAddress#</a>#}else{##:LocationToAddress##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo1', width: 100, title: 'Số seal 1',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:SealNo1#</a>#}else{##:SealNo1##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo2', width: 100, title: 'Số seal 2',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:SealNo2#</a>#}else{##:SealNo2##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TripNo', width: 100, title: 'Số chuyến',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:TripNo#</a>#}else{##:TripNo##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 23, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ShipNo', width: 100, title: 'Số tàu',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ShipNo#</a>#}else{##:ShipNo##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 24, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ShipName', width: 100, title: 'Tên tàu',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ShipName#</a>#}else{##:ShipName##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 25, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined1', width: 100, title: 'Định nghĩa 1',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined1#</a>#}else{##:UserDefined1##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 26, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined2', width: 100, title: 'Định nghĩa 2',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined2#</a>#}else{##:UserDefined2##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 27, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined3', width: 100, title: 'Định nghĩa 3',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined3#</a>#}else{##:UserDefined3##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 28, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined4', width: 100, title: 'Định nghĩa 4',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined4#</a>#}else{##:UserDefined4##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 29, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined5', width: 100, title: 'Định nghĩa 5',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined5#</a>#}else{##:UserDefined5##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 30, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined6', width: 100, title: 'Định nghĩa 6',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined6#</a>#}else{##:UserDefined6##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 31, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'WarningTime', width: 100, title: 'TG cảnh báo',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:WarningTime#</a>#}else{##:WarningTime##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 32, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'WarningMsg', width: 100, title: 'ND cảnh báo',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:WarningMsg#</a>#}else{##:WarningMsg##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 33, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note0', width: 150, title: 'Ghi chú',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Note0#</a>#}else{##:Note0##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 34, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note1', width: 150, title: 'Ghi chú',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Note1#</a>#}else{##:Note1##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 35, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note2', width: 150, title: 'Ghi chú 1',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Note2#</a>#}else{##:Note2##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 36, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupProductCode', width: 150, title: 'Mã hàng hóa',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:GroupProductCode#</a>#}else{##:GroupProductCode##}#',
                filterable: false, sortable: false, sortorder: 37, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupProductName', width: 150, title: 'Tên hàng hóa',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:GroupProductName#</a>#}else{##:GroupProductName##}#',
                filterable: false, sortable: false, sortorder: 38, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupSort', width: 200, title: ' ',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:GroupSort#</a>#}else{##:GroupSort##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 39, configurable: true, isfunctionalHidden: false
            },
            { title: '', filterable: false, sortable: false, sortorder: 999, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.timeline_conV2_Grid_Choose_Change = function ($event, grid, haschange) {
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                $scope.NewTimeLineContainerSelected[o.ID] = $.extend(true, {}, o);
            } else if (Common.HasValue($scope.NewTimeLineContainerSelected[o.ID])) {
                $scope.NewTimeLineContainerSelected[o.ID] = null;
            }
        })
    }

    $scope.NewTimeLineVehicleVisible_Click = function ($event) {
        $event.preventDefault();

        $scope.IsShowTimeLineWithVehicle = !$scope.IsShowTimeLineWithVehicle;
        $scope.LoadNewTimeLineV2Data(false);
    }

    $scope.NewTimeLineVehicleFilter_Click = function ($event) {
        $event.preventDefault();
        $scope.LoadNewTimeLineV2Data(false);
    }


    $scope.OPS_TOMasterID = -1, $scope.OPS_ContainerID = -1;
    $scope.ContainerByTrip_Click = function ($event, item, win, type) {
        $event.preventDefault();

        win.center().open();
        $scope.OPS_TOMasterID = item.TOMasterID || -1;
        if (type == 1)
            $scope.OPS_ContainerID = item.OPSContainerID;
        else
            $scope.OPS_ContainerID = item.ID;
        $scope.new_container_by_trip_Grid.dataSource.read();
    }

    $scope.new_container_by_trip_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.COTOContainerByTrip_List,
            pageSize: 0,
            readparam: function () {
                return { mID: $scope.OPS_TOMasterID, conID: $scope.OPS_ContainerID }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' },
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var obj = grid.dataItem(tr);
                if (Common.HasValue(obj)) {
                    if (obj.COTOStatus == 1) {
                        $(tr).css('background-color', '#F28126')
                    } else if (obj.COTOStatus == 2) {
                        $(tr).css('background-color', '#73C95F')
                    }
                }
            })
        },
        columns: [
            { field: 'CustomerCode', width: '120px', title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: '100px', title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '80px', title: 'Số seal 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '80px', title: 'Số seal 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: '100px', title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfContainerName', width: '100px', title: 'Chặng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
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


    $scope.NewTimeLineItem = {
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
        IsRomoocBreak: $scope.RomoocMustReturn,
        ETD: null, ETA: null,
        DateGetRomooc: null,
        DateReturnRomooc: null,
        ListORDCon: [],
        ListORDConName: [],
        ListOPSCon: [],
        LocationStartID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartID,
        LocationStartName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartName,
        LocationEndID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndID,
        LocationEndName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndName,
        LocationStartLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLat,
        LocationStartLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLng,
        LocationEndLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLat,
        LocationEndLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLng,
        DateGetRomooc: null,
        DateReturnRomooc: null,
        LocationGetRomoocID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocID,
        LocationGetRomoocName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocName,
        LocationGetRomoocLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLat,
        LocationGetRomoocLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLng,
        LocationReturnRomoocID: -1,
        LocationReturnRomoocName: "",
        LocationReturnRomoocLat: null,
        LocationReturnRomoocLng: null,
        ListLocation: [],
        HasChange: false,
        MinInterval: 0.5,
        IsAllowChangeRomooc: true,
        TimeMin: null, TimeMax: null,
        DataContainerOffer: []
    }

    $scope.TimeLineEventCanDrop = false;
    $scope.TimeLineEventDropItem = { ETA: null, ETD: null, Hour: -1 };
    $scope.TineLineEventCallBack = null;

    $scope.InitDragAndDropV2 = function () {
        $($scope.timeline_conV2_Grid.element).kendoDraggable({
            filter: "tbody>tr", group: "timelineGroup", cursorOffset: { top: 0, left: 0 },
            drag: function (e) {
                $scope.HideTimeLineV2Tooltip();
                if ($scope.NewTimeLineResourceType == 3 || (!$scope.TimeLineViewOrderV2 && $scope.NewTimeLineResourceType == 2))
                    e.preventDefault();
                else {
                    if ($scope.NewTimeLineResourceType < 3) {
                        var uid = e.currentTarget.data("uid"), grid = $scope.timeline_conV2_Grid, item = grid.dataSource.getByUid(uid),
                            scheduler = $scope.new_timeline_v2_Trip, table = $(scheduler.element).find('.k-scheduler-content');
                        var eta = item.ETA, etd = item.ETD;
                        var dataContainer = [], dataOPSContainer = [];
                        if (item.IsChoose) {
                            Common.Data.Each($scope.NewTimeLineContainerSelected, function (o) {
                                dataContainer.push(o.ID);
                                dataOPSContainer.push(o.OPSContainerID);
                                if (!Common.HasValue(eta)) eta = o.ETA;
                                if (!Common.HasValue(etd)) etd = o.ETD;
                                if (eta < o.ETA) eta = o.ETA;
                                if (etd > o.ETD) etd = o.ETD;
                            })
                        } else {
                            dataContainer.push(item.ID);
                            dataOPSContainer.push(item.OPSContainerID);
                            eta = item.ETA; etd = item.ETD;
                        }
                        if (!$scope.TimeLineEventDragDrop) {
                            $scope.TimeLineEventDropItem = { ETA: null, ETD: null, Hour: -1 };
                            $scope.TimeLineEventDragDrop = true;
                            $scope.TimeLineEventCanDrop = false;
                            if ($scope.TimeLineViewOrderV2)
                                dataContainer = [];
                            else
                                dataOPSContainer = [];
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_ToTD_LeadTime_Offer,
                                data: { dataCon: dataContainer, dataOPSCon: dataOPSContainer },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        if (res != null && res.DateMax != null && res.DateMin != null) {
                                            res.DateMin = Common.Date.FromJson(res.DateMin);
                                            res.DateMax = Common.Date.FromJson(res.DateMax);
                                            $scope.TimeLineEventDropItem = { ETA: res.DateMax, ETD: res.DateMin, Hour: res.HourETAOffer || -1 };
                                            $scope.TimeLineEventCanDrop = true;
                                            var view = scheduler.view(), start = view.startDate(), end = view.endDate(), etd = $scope.TimeLineEventDropItem.ETD,
                                            tbody1 = scheduler.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                                            top = tbody1.scrollTop();
                                            if (start <= etd && etd < end) {
                                                $scope.TimeLineToTD = true;
                                                $scope.MoveToTime(scheduler, null, $scope.TimeLineEventDropItem.ETD, top);
                                                angular.forEach(table.find('td[data-role="droptarget"]'), function (td) {
                                                    slot = scheduler.slotByElement(td);
                                                    if (Common.HasValue(slot) && slot.startDate < res.DateMax && slot.startDate > res.DateMin)
                                                        $(td).addClass('dropzone');
                                                    else
                                                        $(td).removeClass('dropzone');
                                                })
                                            } else {
                                                $scope.TimeLineToTD = true;
                                                $scope.TimeLineToTime = $scope.TimeLineEventDropItem.ETD;
                                                $scope.TimeLineToTop = top;
                                                $scope.TimeLineToUID = null;
                                                $scope.TineLineEventCallBack = function (scheduler) {
                                                    var table = $(scheduler.element).find('.k-scheduler-content');
                                                    angular.forEach(table.find('td[data-role="droptarget"]'), function (td) {
                                                        slot = scheduler.slotByElement(td);
                                                        if (Common.HasValue(slot) && slot.startDate < $scope.TimeLineEventDropItem.ETA && slot.startDate > $scope.TimeLineEventDropItem.ETD)
                                                            $(td).addClass('dropzone');
                                                        else
                                                            $(td).removeClass('dropzone');
                                                    })
                                                    $scope.TineLineEventCallBack = null;
                                                }
                                                scheduler.date($scope.TimeLineEventDropItem.ETD);
                                            }
                                        }
                                    })
                                }
                            })
                        }
                        if ($(e.elementUnderCursor).is('td[data-role="droptarget"].dropzone')) {
                            angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                                $(td).removeClass('hight-light');
                            })
                            $(e.elementUnderCursor).addClass("hight-light");
                            var ele = e.elementUnderCursor, slot = scheduler.slotByElement(ele);
                            if (Common.HasValue(slot)) {
                                var time = slot.endDate;
                                var interval = $scope.TimeLineEventDropItem.Hour * 60 * 1000;
                                while (Common.HasValue(slot) && slot.endDate - time <= interval) {
                                    $(ele).addClass("hight-light"); ele = ele.nextSibling;
                                    if (Common.HasValue(ele) && ele != []) slot = scheduler.slotByElement(ele)
                                    else slot = null;
                                }
                            }
                        }
                    }
                }
            },
            dragend: function (e) {
                $timeout(function () {
                    var table = $($scope.new_timeline_v2_Trip.element).find('.k-scheduler-content');
                    angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                        $(td).removeClass('hight-light');
                    })
                    angular.forEach(table.find('td[data-role="droptarget"].dropzone'), function (td) {
                        $(td).removeClass('dropzone');
                    })
                    $timeout(function () {
                        $scope.TimeLineEventCanDrop = false;
                        $scope.TimeLineEventDragDrop = false;
                    }, 100)
                }, 50)
            },
            hint: function (e) {
                var grid = $scope.timeline_conV2_Grid, item = grid.dataItem(e);
                if (item.IsChoose) {
                    var html = '';
                    Common.Data.Each($scope.NewTimeLineContainerSelected, function (item) {
                        html += '<tr><td>' + item.CustomerCode + '</td>' + '<td>' + item.OrderCode + '</td>' + '<td>' + item.TypeOfContainerName + '</td></tr>';
                    })
                    return $('<div class="k-grid k-widget" style="background-color: #3ab9fc !important; color: #fff !important;"><table><tbody>' + html + '</tbody></table></div>');
                } else {
                    return $('<div class="k-grid k-widget" style="background-color: #3ab9fc !important; color: #fff !important;"><table><tbody><tr>' +
                   '<td>' + item.CustomerCode + '</td>' + '<td>' + item.OrderCode + '</td>' + '<td>' + item.TypeOfContainerName + '</td>' + '</tr></tbody></table></div>');
                }
            }
        });

        if ($scope.NewTimeLineResourceType < 3) {
            $($scope.new_timeline_v2_Trip.element).find('.k-scheduler-times tr').kendoDropTarget({
                drop: function (e) {
                    if ($scope.NewTimeLineResourceType == 3 || (!$scope.TimeLineViewOrderV2 && $scope.NewTimeLineResourceType == 2))
                        e.preventDefault();
                    else if ($scope.TimeLineEventCanDrop) {
                        var uid = e.draggable.currentTarget.data("uid"), grid = $scope.timeline_conV2_Grid, item = grid.dataSource.getByUid(uid);
                        var str = e.dropTarget.html(), s1 = str.indexOf("data-uid="), s2 = str.indexOf("chk_vehicle_timeline"), s3 = s2 - s1 - 19, group = str.substr(s1 + 10, s3);
                        var flag = false, data = $scope.new_timeline_v2_Trip.resources[0].dataSource.data();
                        var dataContainer = [], dataOPSContainer = [], ton = 0, eta, etd;
                        if (item.IsChoose) {
                            Common.Data.Each($scope.NewTimeLineContainerSelected, function (o) {
                                dataContainer.push(o.ID); ton += o.Ton;
                                dataOPSContainer.push(o.OPSContainerID);
                                if (!Common.HasValue(eta)) eta = o.ETA;
                                if (!Common.HasValue(etd)) etd = o.ETD;
                                if (eta < o.ETA) eta = o.ETA;
                                if (etd > o.ETD) etd = o.ETD;
                            })
                        } else {
                            dataContainer.push(item.ID);
                            dataOPSContainer.push(item.OPSContainerID);
                            ton = item.Ton; eta = item.ETA; etd = item.ETD;
                        }
                        Common.Data.Each(data, function (o) { if (o.value == group) flag = true; })
                        var tbody1 = $scope.new_timeline_v2_Trip.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                            top = tbody1.scrollTop();
                        $scope.TimeLineToTop = top;
                        $scope.TimeLineToUID = group;
                        if (flag) {
                            var venID = group.split('_')[0], vehID = group.split('_')[1];
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_VehicleInfo,
                                data: { venID: venID, vehID: $scope.NewTimeLineResourceType == 1 ? vehID : -1, romID: $scope.NewTimeLineResourceType == 1 ? -1 : vehID, now: item.ETD },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $scope.NewTimeLineItem = {
                                            ID: -1,
                                            Code: "mới tạo",
                                            DriverName: "",
                                            DriverTel: "",
                                            StatusCode: 'Chưa chọn xe',
                                            StatusColor: $scope.Color.Error,
                                            VehicleID: res.ID > 0 ? res.ID : "",
                                            VehicleNo: res.Regno,
                                            RomoocID: res.RomoocID > 0 ? res.RomoocID : "",
                                            RomoocNo: res.RomoocNo,
                                            VendorOfVehicleID: venID,
                                            VendorOfVehicleCode: res.VehicleVendorCode,
                                            Ton: ton,
                                            Status: 1,
                                            IsRomoocBreak: $scope.RomoocMustReturn,
                                            ETD: $scope.TimeLineEventDropItem.ETD || etd, ETA: $scope.TimeLineEventDropItem.ETA || eta,
                                            DateGetRomooc: null,
                                            DateReturnRomooc: null,
                                            ListORDCon: $scope.TimeLineViewOrderV2 ? dataContainer : [], //ORDContainerID
                                            ListORDConName: [],
                                            ListOPSCon: $scope.TimeLineViewOrderV2 ? [] : dataContainer, //OPSCOTOContainerID
                                            ListOPS_Container: dataOPSContainer, //OPSContainerID
                                            LocationStartID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartID,
                                            LocationStartName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartName,
                                            LocationEndID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndID,
                                            LocationEndName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndName,
                                            LocationStartLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLat,
                                            LocationStartLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLng,
                                            LocationEndLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLat,
                                            LocationEndLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLng,
                                            DateGetRomooc: etd,
                                            DateReturnRomooc: null,
                                            LocationGetRomoocID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocID,
                                            LocationGetRomoocName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocName,
                                            LocationGetRomoocLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLat,
                                            LocationGetRomoocLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLng,
                                            LocationReturnRomoocID: -1,
                                            LocationReturnRomoocName: "",
                                            LocationReturnRomoocLat: null,
                                            LocationReturnRomoocLng: null,
                                            ListLocation: [],
                                            DataContainerOffer: [],
                                            MinInterval: 0.5,
                                            IsAllowChangeRomooc: true,
                                            TimeMin: null, TimeMax: null,
                                            HasTimeChange: true
                                        }
                                        if ($scope.RomoocMustReturn == true) {
                                            $scope.NewTimeLineItem.DateReturnRomooc = $scope.NewTimeLineItem.ETA;
                                            $scope.NewTimeLineItem.LocationReturnRomoocID = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocID;
                                            $scope.NewTimeLineItem.LocationReturnRomoocName = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocName;
                                            $scope.NewTimeLineItem.LocationReturnRomoocLat = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLat;
                                            $scope.NewTimeLineItem.LocationReturnRomoocLng = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLng;
                                        }
                                        $scope.NewTimeLineDetail = true;
                                        if ($scope.NewTimeLineItem.VendorOfVehicleID == null)
                                            $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                                        $scope.LoadDataNewTimeLineInfo(false);
                                        $timeout(function () {
                                            if ($scope.NewTimeLineItem.VendorOfVehicleID > 0) {
                                                $scope.new_timeline_vendor_info_win.center().open();
                                            } else {
                                                $scope.new_timeline_info_win.center().open();
                                            }
                                        }, 100)
                                        $scope.TimeLineToTime = $scope.NewTimeLineItem.ETD;
                                        $scope.TimeLineToTD = true;
                                    })
                                }
                            })
                        }
                    }
                },
                group: "timelineGroup",
            });

            $($scope.new_timeline_v2_Trip.element).find('.k-scheduler-content td').kendoDropTarget({
                drop: function (e) {
                    if ($scope.NewTimeLineResourceType == 3 || (!$scope.TimeLineViewOrderV2 && $scope.NewTimeLineResourceType == 2))
                        e.preventDefault();
                    else if ($(e.dropTarget).is('.dropzone') && $scope.TimeLineEventCanDrop) {
                        $timeout(function () {
                            var uid = e.draggable.currentTarget.data("uid"), grid = $scope.timeline_conV2_Grid, item = grid.dataSource.getByUid(uid);
                            var scheduler = $scope.new_timeline_v2_Trip, slot = scheduler.slotByElement(e.dropTarget), resource = scheduler.resources[0].dataSource.data();
                            var dataContainer = [], ton = 0, eta, etd, dataOPSContainer = [];
                            if (item.IsChoose) {
                                Common.Data.Each($scope.NewTimeLineContainerSelected, function (o) {
                                    dataContainer.push(o.ID); ton += o.Ton;
                                    dataOPSContainer.push(o.OPSContainerID);
                                    if (!Common.HasValue(eta)) eta = o.ETA;
                                    if (!Common.HasValue(etd)) etd = o.ETD;
                                    if (eta < o.ETA) eta = o.ETA;
                                    if (etd > o.ETD) etd = o.ETD;
                                })
                            } else {
                                dataContainer.push(item.ID); ton = item.Ton;
                                dataOPSContainer.push(item.OPSContainerID);
                                eta = item.ETA; etd = item.ETD;
                            }
                            if (Common.HasValue(resource[slot.groupIndex])) {
                                var obj = resource[slot.groupIndex];
                                var tbody1 = $scope.new_timeline_v2_Trip.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                                    top = tbody1.scrollTop();
                                $scope.TimeLineToTop = top;
                                $scope.TimeLineToUID = obj.value;
                                var venID = obj.value.split('_')[0], vehID = obj.value.split('_')[1];
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_VehicleInfo,
                                    data: { venID: venID, vehID: $scope.NewTimeLineResourceType == 1 ? vehID : -1, romID: $scope.NewTimeLineResourceType == 1 ? -1 : vehID, now: slot.startDate },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            Common.Services.Call($http, {
                                                url: Common.Services.url.OPS,
                                                method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_VehicleCheck,
                                                data: { vehID: res.ID, romID: res.RomoocID, dataContainer: $scope.TimeLineViewOrderV2 ? [] : dataContainer, dataOPSContainer: dataOPSContainer },
                                                success: function (str) {
                                                    if (str == "" || str == "OK" || str == null) {
                                                        var interval = $scope.TimeLineEventDropItem.Hour > 0 ? $scope.TimeLineEventDropItem.Hour / 24 : (eta - etd) / (24 * 60 * 60 * 1000),
                                                            etaTime = slot.startDate.addDays(interval) > $scope.TimeLineEventDropItem.DateMax ? $scope.TimeLineEventDropItem.DateMax : slot.startDate.addDays(interval);
                                                        $scope.NewTimeLineItem = {
                                                            ID: -1,
                                                            Code: "mới tạo",
                                                            DriverName: "",
                                                            DriverTel: "",
                                                            StatusCode: 'Chưa chọn xe',
                                                            StatusColor: $scope.Color.Error,
                                                            VehicleID: res.ID > 0 ? res.ID : "",
                                                            VehicleNo: res.Regno,
                                                            RomoocID: res.RomoocID > 0 ? res.RomoocID : "",
                                                            RomoocNo: res.RomoocNo,
                                                            VendorOfVehicleID: venID,
                                                            VendorOfVehicleCode: res.VehicleVendorCode,
                                                            Ton: ton,
                                                            Status: 1,
                                                            IsRomoocBreak: $scope.RomoocMustReturn,
                                                            ETD: slot.startDate, ETA: etaTime,
                                                            DateGetRomooc: null,
                                                            DateReturnRomooc: null,
                                                            ListORDCon: $scope.TimeLineViewOrderV2 ? dataContainer : [],
                                                            ListORDConName: [],
                                                            ListOPSCon: $scope.TimeLineViewOrderV2 ? [] : dataContainer,
                                                            ListOPS_Container: dataOPSContainer,
                                                            LocationStartID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartID,
                                                            LocationStartName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartName,
                                                            LocationEndID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndID,
                                                            LocationEndName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndName,
                                                            LocationStartLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLat,
                                                            LocationStartLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationStartLng,
                                                            LocationEndLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLat,
                                                            LocationEndLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationEndLng,
                                                            DateGetRomooc: slot.startDate,
                                                            DateReturnRomooc: null,
                                                            LocationGetRomoocID: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocID,
                                                            LocationGetRomoocName: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocName,
                                                            LocationGetRomoocLat: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLat,
                                                            LocationGetRomoocLng: _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLng,
                                                            LocationReturnRomoocID: -1,
                                                            LocationReturnRomoocName: "",
                                                            LocationReturnRomoocLat: null,
                                                            LocationReturnRomoocLng: null,
                                                            ListLocation: [],
                                                            MinInterval: 0.5,
                                                            IsAllowChangeRomooc: true,
                                                            TimeMin: null, TimeMax: null,
                                                            DataContainerOffer: [],
                                                            HasTimeChange: true
                                                        }
                                                        if ($scope.RomoocMustReturn == true) {
                                                            $scope.NewTimeLineItem.DateReturnRomooc = $scope.NewTimeLineItem.ETA;
                                                            $scope.NewTimeLineItem.LocationReturnRomoocID = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocID;
                                                            $scope.NewTimeLineItem.LocationReturnRomoocName = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocName;
                                                            $scope.NewTimeLineItem.LocationReturnRomoocLat = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLat;
                                                            $scope.NewTimeLineItem.LocationReturnRomoocLng = _OPSAppointment_COViewOnMapV4.Data.Location.LocationRomoocLng;
                                                        }
                                                        $scope.NewTimeLineDetail = true;
                                                        if ($scope.NewTimeLineItem.VendorOfVehicleID == null)
                                                            $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                                                        $scope.LoadDataNewTimeLineInfo(false);
                                                        $timeout(function () {
                                                            if ($scope.NewTimeLineItem.VendorOfVehicleID > 0) {
                                                                $scope.new_timeline_vendor_info_win.center().open();
                                                            } else {
                                                                $scope.new_timeline_info_win.center().open();
                                                            }
                                                        }, 100)
                                                        $scope.TimeLineToTime = $scope.NewTimeLineItem.ETD;
                                                        $scope.TimeLineToTD = true;
                                                    } else {
                                                        $rootScope.Message({ Msg: str, Type: Common.Message.Type.Alert });
                                                    }
                                                }
                                            })
                                        })
                                    }
                                })
                            }
                        }, 1)
                    }
                },
                group: "timelineGroup",
            });
        }
        $($scope.new_timeline_v2_Trip.element).find('.k-scheduler-content .k-event').kendoDropTarget({
            drop: function (e) {
                var uid = e.draggable.currentTarget.data("uid"), grid = $scope.timeline_conV2_Grid, item = grid.dataSource.getByUid(uid);
                var str = e.dropTarget.html(), s1 = str.lastIndexOf("$event"), s2 = str.indexOf("timeline_trip_info_win"), s3 = s2 - s1 - 8;
                var sid = str.substr(s1 + 7, s3), obj = $scope.new_timeline_v2_Trip.dataSource.get(sid.split(',')[0]);
                var noC = 1;

                var tbody1 = $scope.new_timeline_v2_Trip.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                    top = tbody1.scrollTop();
                $scope.TimeLineToTop = top;
                $scope.TimeLineToTime = obj.ETD;
                $scope.TimeLineToUID = obj.field;
                if (item.IsChoose) {
                    Common.Data.Each($scope.NewTimeLineContainerSelected, function (o) {
                        if (o.ID != item.ID) {
                            noC++;
                        }
                    })
                }
                if (noC == 1) {
                    if (Common.HasValue(obj)) {
                        if (obj.TypeOfEvent == 1 && (obj.StatusOfEvent == 1 || obj.StatusOfEvent == 2 || obj.StatusOfEvent == 11)) {
                            if (obj.ListContainer.length == 1 && obj.ListContainer[0].Qty == 2) {
                                if (item.Qty == 2) {
                                    var flag = false;
                                    if (obj.ListContainer[0].ServiceType == 1 && item.ServiceType == 2) flag = true;
                                    if (obj.ListContainer[0].ServiceType == 3 && item.ServiceType == 1) flag = true;
                                    if (obj.ListContainer[0].ServiceType == 3 && item.ServiceType == 2) flag = true;
                                    if (flag) {
                                        $scope.LoadDataDragDropInfo(obj.id, -1, -1, -1, -1, new Date(), new Date(), $scope.TimeLineViewOrderV2 ? [item.OPSContainerID] : [], $scope.TimeLineViewOrderV2 ? [] : [item.ID]);
                                    } else {
                                        $rootScope.Message({
                                            Type: Common.Message.Type.Alert,
                                            Msg: "Không thể thêm đơn hàng vào chuyến! Chặng không phù hợp!"
                                        })
                                    }
                                } else {
                                    $rootScope.Message({
                                        Type: Common.Message.Type.Alert,
                                        Msg: "Không thể thêm đơn hàng vào chuyến! Loại container phù hợp!"
                                    })
                                }
                            }
                            else if (obj.ListContainer.length == 1 && obj.ListContainer[0].Qty == 1) {
                                if (item.Qty == 1) {
                                    var flag = false;
                                    if (obj.ListContainer[0].ServiceType == 1 && item.ServiceType == 1) flag = true;
                                    if (obj.ListContainer[0].ServiceType == 2 && item.ServiceType == 2) flag = true;
                                    if (obj.ListContainer[0].ServiceType == 3 && item.ServiceType == 3) flag = true;
                                    if (obj.ListContainer[0].ServiceType == 1 && item.ServiceType == 3) flag = true;
                                    if (obj.ListContainer[0].ServiceType == 2 && item.ServiceType == 3) flag = true;
                                    if (flag) {
                                        flag = obj.MaxQty = 2 && obj.MaxTon >= obj.Ton + item.Ton;
                                        if (flag) {
                                            $scope.LoadDataDragDropInfo(obj.id, -1, -1, -1, -1, new Date(), new Date(), $scope.TimeLineViewOrderV2 ? [item.OPSContainerID] : [], $scope.TimeLineViewOrderV2 ? [] : [item.ID]);
                                        } else {
                                            $rootScope.Message({
                                                Type: Common.Message.Type.Alert,
                                                Msg: "Không thể thêm đơn hàng vào chuyến! Quá trọng tải hoặc kích thước!"
                                            })
                                        }
                                    } else {
                                        $rootScope.Message({
                                            Type: Common.Message.Type.Alert,
                                            Msg: "Không thể thêm đơn hàng vào chuyến! Chặng không phù hợp!"
                                        })
                                    }
                                } else {
                                    $rootScope.Message({
                                        Type: Common.Message.Type.Alert,
                                        Msg: "Không thể thêm đơn hàng vào chuyến! Loại container phù hợp!"
                                    })
                                }
                            } else {
                                $rootScope.Message({
                                    Type: Common.Message.Type.Alert,
                                    Msg: "Không thể thêm đơn hàng vào chuyến! Quá số container cho phép!"
                                })
                            }
                        }
                        else {
                            $rootScope.Message({
                                Msg: "Không thể thêm đơn hàng vào chuyến đã hoàn thành!", Type: Common.Message.Type.Alert
                            })
                        }
                    }
                } else {
                    $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Không thể thêm! Quá số lượng container có thể chở!" })
                }
            },
            group: "timelineGroup",
        });
    }

    $scope.cboNewTimeLineVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'Regno', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, Regno: { type: 'string' } } }
        })
    }

    $scope.cboNewTimeLineRomooc_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'Regno', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, Regno: { type: 'string' } } }
        })
    }

    $scope.atcNewTimeLineDriverNameOptions = {
        dataSource: Common.DataSource.Local({ data: [] }),
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Họ tên", dataTextField: "DriverName",
        change: function (e) {
            var cbo = this;
            $timeout(function () {
                var obj = cbo.dataItem(cbo.select());
                if (Common.HasValue(obj)) {
                    $scope.NewTimeLineItem.DriverTel = obj.DriverTel;
                }
            }, 10)
        }
    }
    
    $scope.$watch('NewTimeLineItem.ETD', function () {
        $scope.NewTimeLine_OfferTime(function (res, e) {
            $scope.CheckNewTimeLine("ETD", e)
        });
    });
    $scope.$watch('NewTimeLineItem.ETA', function () {
        $scope.NewTimeLine_OfferTime(function (res, e) {
            $scope.CheckNewTimeLine("ETA", e)
        });
    });
    $scope.$watch('NewTimeLineItem.VehicleID', function () {
        $scope.CheckNewTimeLine();
    });
    $scope.$watch('NewTimeLineItem.RomoocID', function () {
        $scope.CheckNewTimeLine();
    });

    $scope.CheckNewTimeLine = function (props, saveContainer) {
        Common.Log("CheckNewTimeLine")
        if ($scope.NewTimeLineDetail && $scope.NewTimeLineItem != null && $scope.NewTimeLineItem.Status == 1) {
            $scope.NewTimeLineItem.StatusCode = "";
            $scope.NewTimeLineItem.StatusColor = $scope.Color.None;
            var interval = $scope.NewTimeLineItem.MinInterval || 0.5;
            if (props == "ETD" && $scope.NewTimeLineItem.ETD != null && $scope.NewTimeLineItem.ETA != null) {
                if ($scope.NewTimeLineItem.ETD >= $scope.NewTimeLineItem.ETA || $scope.NewTimeLineItem.ETD.addDays(interval / 24) > $scope.NewTimeLineItem.ETA) {
                    $scope.NewTimeLineItem.ETA = $scope.NewTimeLineItem.ETD.addDays(interval / 24);
                }
            }

            //Trường hợp xe vendor không cần kiểm tra.
            if ($scope.NewTimeLineItem.VendorOfVehicleID == -1 && $scope.NewTimeLineItem.VehicleID > 0 && $scope.NewTimeLineItem.RomoocID > 0 && $scope.NewTimeLineItem.ETD != null && $scope.NewTimeLineItem.ETA != null) {
                Common.Log('Trip checking...');
                var minInterval = interval * 60 * 60 * 1000;
                if ($scope.NewTimeLineItem.ETD >= $scope.NewTimeLineItem.ETA) {
                    $scope.NewTimeLineItem.StatusCode = "Tg không hợp lệ.";
                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                }
                else if ($scope.NewTimeLineItem.ETA - $scope.NewTimeLineItem.ETD < minInterval) {
                    $scope.NewTimeLineItem.StatusCode = "Tg không phù hợp.";
                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                }
                else if ($scope.NewTimeLineItem.TimeMin != null && $scope.NewTimeLineItem.TimeMin > $scope.NewTimeLineItem.ETD) {
                    $scope.NewTimeLineItem.StatusCode = "Tg không phù hợp.";
                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                }
                else if ($scope.NewTimeLineItem.TimeMax != null && $scope.NewTimeLineItem.TimeMax < $scope.NewTimeLineItem.ETA) {
                    $scope.NewTimeLineItem.StatusCode = "Tg không phù hợp.";
                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                }
                else {
                    $scope.NewTimeLineItem.IsCheching = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV4.URL.CheckVehicleAvailable,
                        data: {
                            romoocID: $scope.NewTimeLineItem.RomoocID,
                            vehicleID: $scope.NewTimeLineItem.VehicleID,
                            masterID: $scope.NewTimeLineItem.ID,
                            ETD: $scope.NewTimeLineItem.ETD,
                            ETA: $scope.NewTimeLineItem.ETA,
                            Ton: $scope.NewTimeLineItem.TotalTon || 0,
                            dataCon: $scope.NewTimeLineItem.ListOPSCon,
                            dataOPSCon: [],
                            dataORDCon: $scope.NewTimeLineItem.ListORDCon
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.NewTimeLineItem.IsCheching = false;
                                if (saveContainer == false)
                                    $scope.NewTimeLineItem.DataContainerOffer = res.ListCOContainer;
                                $scope.NewTimeLineItem.TimeMin = Common.Date.FromJson(res.DateMin);
                                $scope.NewTimeLineItem.TimeMax = Common.Date.FromJson(res.DateMax);
                                $scope.NewTimeLineItem.MinInterval = res.HourETAOffer || 0.5;
                                $scope.NewTimeLineItem.IsAllowChangeRomooc = res.AllowChangeRomooc;
                                if (res.OfferNoteError != null && res.OfferNoteError != "") {
                                    $scope.NewTimeLineItem.StatusCode = res.OfferNoteError;
                                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                                } else if (res.OfferNoteWarning != null && res.OfferNoteWarning != "") {
                                    $scope.NewTimeLineItem.StatusCode = res.OfferNoteWarning;
                                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Warning;
                                } else {
                                    $scope.NewTimeLineItem.DriverTel = res.DriverTel;
                                    $scope.NewTimeLineItem.DriverName = res.DriverName;
                                    $scope.NewTimeLineItem.StatusCode = "Có thể cập nhật";
                                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Success;

                                    //if ($scope.NewTimeLineItem.DataContainerOffer == null || $scope.NewTimeLineItem.DataContainerOffer.length == 0) {
                                    //    $scope.NewTimeLineItem.StatusCode = "TG không phù hợp";
                                    //    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                                    //} else if (res.IsOverWeight) {
                                    //    $scope.NewTimeLineItem.StatusCode = "Quá trọng tải";
                                    //    $scope.NewTimeLineItem.StatusColor = $scope.Color.Warning;
                                    //} else {
                                    //    if (res.IsVehicleAvailable) {
                                    //        $scope.NewTimeLineItem.DriverTel = res.DriverTel;
                                    //        $scope.NewTimeLineItem.DriverName = res.DriverName;
                                    //        $scope.NewTimeLineItem.StatusCode = "Có thể cập nhật";
                                    //        $scope.NewTimeLineItem.StatusColor = $scope.Color.Success;
                                    //    } else {
                                    //        $scope.NewTimeLineItem.StatusCode = "Xe bận.";
                                    //        $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                                    //    }
                                    //}
                                }
                            })
                        }
                    })
                }
            }

            try {
                if ($scope.NewTimeLineItem.VehicleID != $scope.NewTimeLineItemTemp.VehicleID || $scope.NewTimeLineItem.RomoocID != $scope.NewTimeLineItemTemp.RomoocID || $scope.NewTimeLineItem.ETA.getTime() != $scope.NewTimeLineItemTemp.ETA.getTime() || $scope.NewTimeLineItem.ETD.getTime() != $scope.NewTimeLineItemTemp.ETD.getTime()) {
                    $scope.NewTimeLineItem.HasChange = true;
                }
                else {
                    $scope.NewTimeLineItem.HasChange = false;
                }
            } catch (e) {
                $scope.NewTimeLineItem.HasChange = true;
            }

            try {
                if ($scope.NewTimeLineItem.VehicleID != $scope.NewTimeLineItem.VehicleOfferID || $scope.NewTimeLineItem.RomoocID != $scope.NewTimeLineItem.RomoocOfferID || $scope.NewTimeLineItem.ETA.getTime() != $scope.NewTimeLineItem.ETAOffer.getTime() || $scope.NewTimeLineItem.ETD.getTime() != $scope.NewTimeLineItem.ETDOffer.getTime()) {
                    $scope.NewTimeLineItem.HasTimeChange = true;
                    $scope.NewTimeLineItem.ETAOffer = $scope.NewTimeLineItem.ETA;
                    $scope.NewTimeLineItem.ETDOffer = $scope.NewTimeLineItem.ETD;
                    $scope.NewTimeLineItem.RomoocOfferID = $scope.NewTimeLineItem.RomoocID;
                    $scope.NewTimeLineItem.VehicleOfferID = $scope.NewTimeLineItem.VehicleID;
                }
            } catch (e) {
                $scope.NewTimeLineItem.HasTimeChange = true;
            }
        }
    }

    $scope.NewTimeLine_OfferTime = function (callback) {
        if ($scope.IsShowNewTimeLineDragDropInfo == false && $scope.IsShowNewTimeLineV2) {
            Common.Log("NewTimeLine_OfferTime")
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_ToTD_Time_Offer,
                data: { item: $scope.NewTimeLineItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        if (!$scope.LoadDataOfferFromTimeLine) {
                            $scope.NewTimeLineItem.DataContainerOffer = res.ListCOContainer;
                        }
                        $scope.NewTimeLineItem.TimeMin = Common.Date.FromJson(res.DateMin);
                        $scope.NewTimeLineItem.TimeMax = Common.Date.FromJson(res.DateMax);
                        $scope.NewTimeLineItem.MinInterval = res.HourETAOffer || 0.5;
                        $scope.NewTimeLineItem.IsAllowChangeRomooc = res.AllowChangeRomooc;
                        $scope.NewTimeLineItem.RomoocID = res.RomoocID;
                        $scope.NewTimeLineItem.RomoocNo = res.RomoocNo;
                        if ($scope.NewTimeLineItem.DataContainerOffer == null || $scope.NewTimeLineItem.DataContainerOffer.length == 0) {
                            //$timeout(function () {
                            //    $rootScope.Message({ Msg: "Thời gian không phù hợp với chuyến", Type: Common.Message.Type.Alert });
                            //}, 1000)
                        }
                        if (Common.HasValue(callback))
                            callback(res, $scope.LoadDataOfferFromTimeLine);
                        $timeout(function () { $scope.LoadDataOfferFromTimeLine = false; }, 1000);
                    })
                }
            })
        }
    }

    $scope.LoadDataNewTimeLineInfo = function (isNew) {
        Common.Log("LoadDataNewTimeLineInfo")
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.VehicleVendor_List,
            data: {
                vendorID: $scope.NewTimeLineItem.VendorOfVehicleID, request: '', typeofvehicle: 1
            },
            success: function (res) {
                $timeout(function () {
                    $scope.cboNewTimeLineVehicle_Options.dataSource.data(res.Data);
                }, 10)
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV4.URL.VehicleVendor_List,
                    data: {
                        vendorID: $scope.NewTimeLineItem.VendorOfVehicleID, request: '', typeofvehicle: 2
                    },
                    success: function (res) {
                        $timeout(function () {
                            $scope.cboNewTimeLineRomooc_Options.dataSource.data(res.Data);
                        }, 10)
                        $timeout(function () {
                            if (isNew) {
                                if (Common.HasValue(res.Data[0]))
                                    $scope.NewTimeLineItem.RomoocID = res.Data[0].ID;
                                else
                                    $scope.NewTimeLineItem.RomoocID = null;
                            }
                            $rootScope.IsLoading = false;
                        }, 100)
                    }
                });
                $timeout(function () {
                    if (isNew) {
                        if (Common.HasValue(res.Data[0]))
                            $scope.NewTimeLineItem.VehicleID = res.Data[0].ID;
                        else
                            $scope.NewTimeLineItem.VehicleID = null;
                    }
                }, 100)
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.DriverVendor_List,
            data: {
                vendorID: $scope.NewTimeLineItem.VendorOfVehicleID
            },
            success: function (res) {
                var data = [];
                $.each(res, function (i, v) {
                    data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
                });
                $scope.atcNewTimeLineDriverNameOptions.dataSource.data(data);
            }
        });
    }

    $scope.TypeOfTimeLineVehicle = 1;
    $scope.NewVendorVehicle = { ID: -1, RegNo: '', MaxWeight: 0 };
    $scope.TimeLine_Vehicle_Vendor_Create = function ($event, typeofVehicle, win) {
        $event.preventDefault();

        $scope.TypeOfTimeLineVehicle = typeofVehicle;
        $scope.NewVendorVehicle = { ID: -1, RegNo: '', MaxWeight: 0 };
        win.center().open();
    }

    $scope.VehicleVendor_New_OK_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận lưu?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV4.URL.New_Vendor_Vehicle_Save,
                    data: {
                        vendorID: $scope.NewTimeLineItem.VendorOfVehicleID,
                        regNo: $scope.NewVendorVehicle.RegNo,
                        maxWeight: $scope.NewVendorVehicle.MaxWeight,
                        typeofVehicle: $scope.TypeOfTimeLineVehicle
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            });
                            win.close();
                            $scope.LoadDataNewTimeLineInfo(false);
                        })
                    }
                })
            }
        })
    }

    $scope.NewTimeLineVehicle_Change_Click = function ($event, typeofVehicle, win) {
        $event.preventDefault();

        if (typeofVehicle == 2 && !$scope.NewTimeLineItem.IsAllowChangeRomooc) {
            $rootScope.Message({ Msg: "Không cho phép đổi romooc", Type: Common.Message.Type.Alert });
        } else {
            win.center().open();
            $scope.ChangeVehicleType = typeofVehicle;
            $scope.IsVehicleMapActived = true;
            $scope.VehicleMapRequestDate = $scope.NewTimeLineItem.ETD || new Date();
            $timeout(function () {
                switch ($scope.ChangeVehicleType) {
                    case 1:
                        $scope.NewTimeLineVehicleItem.ID = $scope.NewTimeLineItem.VehicleID;
                        $scope.vehMap_Grid.dataSource.read();
                        break;
                    case 2:
                        $scope.NewTimeLineVehicleItem.ID = $scope.NewTimeLineItem.RomoocID;
                        $scope.romMap_Grid.dataSource.read();
                        break;
                    default:
                        break;
                }
                $rootScope.IsLoading = false;
            }, 100);
        }
    }

    $scope.NewTimeLineV2_Update_OK_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.NewTimeLineItem.RomoocID == null || $scope.NewTimeLineItem.RomoocID == "") {
            $scope.NewTimeLineItem.RomoocID = -1;
        }
        var flag = true;
        if ($scope.NewTimeLineItem.ETA == null || $scope.NewTimeLineItem.ETD == null) {
            flag = false;
            $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        }
        else if ($scope.NewTimeLineItem.ETA <= $scope.NewTimeLineItem.ETD) {
            flag = false;
            $rootScope.Message({ Msg: "Sai ràng buộc ETD và ETA.", Type: Common.Message.Type.Alert });
        }
        else if ($scope.NewTimeLineItem.VehicleID < 1) {
            flag = false;
            $rootScope.Message({ Msg: "Vui lòng chọn đầu kéo.", Type: Common.Message.Type.Alert });
        }

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận lưu?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    if ($scope.NewTimeLineItem.ID < 1) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_COViewOnMapV4.URL.New_Schedule_TOMaster_Save,
                            data: { item: $scope.NewTimeLineItem, dataOffer: $scope.NewTimeLineItem.DataContainerOffer || [] },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    var tbody1 = $scope.new_timeline_v2_Trip.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                                        top = tbody1.scrollTop();
                                    $scope.TimeLineToTop = top;
                                    $scope.TimeLineToTime = $scope.NewTimeLineItem.ETD;
                                    $scope.TimeLineToTD = true;
                                    $scope.LoadNewTimeLineV2Data(true);
                                    $scope.NewTimeLineDetail = false;
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

    $scope.timeline_vehicle_romooc_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.Romooc_List,
            readparam: function () {
                return {
                    requestDate: $scope.NewTimeLineItem.ETD || new Date()
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && item.ID == $scope.NewTimeLineItem.RomoocID) {
                    grid.select(tr);
                }
            });
        },
        columns: [
            {
                field: 'Regno', width: 120, title: 'Romooc',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeight', width: 70, title: 'Trọng tải',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'StatusOfRomoocName', title: 'T/trạng',
                filterable: false, sortable: false
            }
        ]
    };

    $scope.timeline_vehicle_tractor_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.Tractor_List,
            readparam: function () {
                return {
                    requestDate: $scope.NewTimeLineItem.ETD || new Date()
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && item.ID == $scope.NewTimeLineItem.VehicleID) {
                    grid.select(tr);
                }
            });
        },
        columns: [
            {
                field: 'Regno', width: 120, title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeight', width: 70, title: 'Trọng tải',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'StatusOfTractorName', title: 'T/trạng',
                filterable: false, sortable: false
            }
        ]
    };

    $scope.NewTimeLineVehicle_Change_OK_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.TypeOfTimeLineVehicle == 1) {
            var grid = $scope.timeline_vehicle_tractor_Grid;
            var item = grid.dataItem(grid.select());
            if (!Common.HasValue(item)) {
                $rootScope.Message({ Msg: "Chưa chọn đầu kéo!", Type: Common.Message.Type.Alert })
            } else {
                $scope.NewTimeLineItem.VehicleID = item.ID;
                $scope.NewTimeLineItem.VehicleNo = item.Regno;
                $scope.NewTimeLineItem.HasChange = true;
                win.close();
            }
        } else {
            var grid = $scope.timeline_vehicle_romooc_Grid;
            var item = grid.dataItem(grid.select());
            if (!Common.HasValue(item)) {
                $rootScope.Message({ Msg: "Chưa chọn đầu kéo!", Type: Common.Message.Type.Alert })
            } else {
                $scope.NewTimeLineItem.RomoocID = item.ID;
                $scope.NewTimeLineItem.RomoocNo = item.Regno;
                $scope.NewTimeLineItem.HasChange = true;
                win.close();
            }
        }
    }

    $scope.NewTimeLineV2Event_Click = function ($event, uid, type, start, end, win, wingroup) {
        $event.preventDefault();

        if (type == 1) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV4.URL.TripByID,
                data: { masterID: uid },
                success: function (res) {
                    if (Common.HasValue(res) && res.ID > 0) {
                        $scope.NewTimeLineItem = {
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
                            IsRomoocBreak: $scope.RomoocMustReturn,
                            ETA: Common.Date.FromJson(res.ETA),
                            ETD: Common.Date.FromJson(res.ETD),
                            ListOPSCon: [], ListORDCon: [],
                            LocationStartID: res.LocationStartID,
                            LocationStartName: res.LocationStartName,
                            LocationEndID: res.LocationEndID,
                            LocationEndName: res.LocationEndName,
                            LocationStartLat: res.LocationStartLat,
                            LocationStartLng: res.LocationStartLng,
                            LocationEndLat: res.LocationEndLat,
                            LocationEndLng: res.LocationEndLng,
                            ListLocation: res.ListLocation,
                            MinInterval: 0.5,
                            IsAllowChangeRomooc: true,
                            TimeMin: null, TimeMax: null,
                            HasChange: false
                        }
                        if ($scope.NewTimeLineItem.VendorOfVehicleID == null) {
                            $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                        }
                        $scope.NewTimeLineItemTemp = $.extend(true, {}, $scope.NewTimeLineItem);

                        var tbody1 = $scope.new_timeline_v2_Trip.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                            top = tbody1.scrollTop();
                        $scope.TimeLineToTop = top;
                        $scope.TimeLineToTime = $scope.NewTimeLineItem.ETD;
                        $scope.TimeLineToTD = true;
                        $scope.TimeLineToUID = -1;
                        var obj = $scope.new_timeline_v2_Trip.dataSource.get(uid);
                        if (Common.HasValue(obj)) {
                            $scope.TimeLineToUID = obj.field;
                        }

                        $scope.TimeLineInfoTabIndex = 1;
                        $scope.NewTimeLineDetail = true;
                        $scope.LoadDataNewTimeLineInfo(false);
                        $scope.new_timeline_trip_info_Grid.dataSource.read();
                        $timeout(function () {
                            $rootScope.IsLoading = false;
                            win.center().open();
                        }, 100)
                    }
                    else {
                        $rootScope.Message({ Msg: "Không tìm thấy chuyến! Xóa khỏi timeline!" });
                        var tbody1 = $scope.new_timeline_v2_Trip.element.find('.k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times'),
                            top = tbody1.scrollTop();
                        $scope.TimeLineToTop = top;
                        $scope.TimeLineToTime = start;
                        $scope.TimeLineToTD = true;
                        $scope.TimeLineToUID = -1;
                        var obj = $scope.new_timeline_v2_Trip.dataSource.get(uid);
                        if (Common.HasValue(obj)) {
                            $scope.TimeLineToUID = obj.field;
                        }
                        $scope.LoadNewTimeLineV2Data(false);
                        $rootScope.IsLoading = false;
                    }
                }
            });
        } else if (type == -1) {
            wingroup.center().open();
            $scope.TimeLineGroupEvent = {
                VendorID: uid, fDate: new Date(start), tDate: new Date(end)
            }
            $scope.timeline_tomaster_Grid.dataSource.read();
        }
    }

    $scope.timelinetooltipV2Options = {
        filter: ".img-warning", position: "top",
        content: function (e) {
            return $(e.target).data('value');
        }
    }

    $scope.TimelineOrderV2ViewStatus_Click = function ($event, grid) {
        $event.preventDefault();
        try {
            $scope.TimeLineViewTripAction = false;
            $scope.ShowTimeLineV2OrderDate = false;
            var value = $($event.currentTarget).data('tabindex');
            if (value == 1) {
                $scope.TimeLineViewOrderV2 = true;
                //grid.hideColumn('Command');
                grid.hideColumn('StatusOfContainerName');
                grid.hideColumn('DateGetEmpty');
                grid.hideColumn('DateReturnEmpty');
            } else {
                $scope.TimeLineViewOrderV2 = false;
                //grid.showColumn('Command');
                grid.showColumn('StatusOfContainerName');
                grid.showColumn('DateGetEmpty');
                grid.showColumn('DateReturnEmpty');
            }
            $scope.NewTimeLineContainerSelected = [];
            grid.dataSource.read();
        }
        catch (e) { }
    }

    $scope.TimelineOrderV2ViewDate_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.ShowTimeLineV2OrderDate = !$scope.ShowTimeLineV2OrderDate;
    }

    $scope.TimelineOrderV2ViewTimeLineWithFilter_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.TimeLineViewOrderV2WithTimeLineFilter = !$scope.TimeLineViewOrderV2WithTimeLineFilter;
        $scope.NewTimeLineContainerSelected = [];
        grid.dataSource.read();
    }

    $scope.TimeLineOrderV2ViewDate_OK_Click = function ($event, grid) {
        $event.preventDefault();
        $scope.TimeLineViewV2OrderDate = true;
        $scope.ShowTimeLineV2OrderDate = false;
        $scope.NewTimeLineContainerSelected = [];
        grid.dataSource.read();
    }

    $scope.TimeLineOrderV2ViewDate_Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        $scope.TimeLineViewV2OrderDate = false;
        $scope.ShowTimeLineV2OrderDate = false;
        $scope.TimeLineV2DateRequest = { fDate: null, tDate: null };
        $scope.NewTimeLineContainerSelected = [];
        grid.dataSource.read();
    }

    $scope.TimeLineGroupEvent = {
        VendorID: -1, fDate: new Date(), tDate: new Date()
    }
    $scope.timeline_tomaster_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.New_Schedule_TOMaster_List,
            pageSize: 0,
            group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    vendorID: $scope.TimeLineGroupEvent.VendorID,
                    fDate: $scope.TimeLineGroupEvent.fDate, tDate: $scope.TimeLineGroupEvent.tDate,
                    dataService: $scope.NewTimeLineVehicleDataService,
                    dataCarrier: $scope.NewTimeLineVehicleDataCarrier,
                    dataSeaport: $scope.NewTimeLineVehicleDataSeaport,
                    dataCus: $scope.NewTimeLineVehicleDataCustomer,
                    dataStatus: $scope.NewTimeLineVehicleDataStatus
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    TOETD: { type: 'date' },
                    TOETA: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    TOCreatedDate: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' }, autoBind: false,
        columns: [
            {
                field: 'TOMasterCode', width: '100px', title: 'Mã chuyến',
                template: "<a href='/' ng-click='ContainerByTrip_Click($event,dataItem,container_by_trip_win)'>#=TOMasterCode#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        if (Common.HasValue(obj)) {
                            var sumTon = 0, sumCBM = 0, sumQty = 0;
                            Common.Data.Each(e.aggregates.parent().items, function (o) {
                                sumTon += o.Ton; sumCBM += o.CBM; sumQty += o.Quantity;
                            })
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strRom = obj.TORomoocNo == "" || obj.TORomoocNo == null ? "[Chưa nhập]" : obj.TORomoocNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            var strTitle = "Kế hoạch";
                            var sty = "width:20px;background:red;display:inline-block;height:16px;position:relative;top:3px;margin-right:7px;";
                            var btn = "<span>" + strVeh + " - " + strRom + "</span>";
                            if (obj.TOStatus == 2) {
                                strTitle = "Đã duyệt";
                                sty = "width:20px;background:blue;display:inline-block;height:16px;position:relative;top:3px;margin-right:7px;";
                            }
                            return "<span style='" + sty + "' title='" + strTitle + "'></span>" + "<span>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + "</span>"
                                + btn + "<span> - " + strName + " - " + strTel + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
            },
            {
                field: 'IsWarning', width: 100, title: 'Cảnh báo', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false
            },
            { field: 'CustomerCode', width: '120px', title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: '100px', title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '80px', title: 'Số seal 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '80px', title: 'Số seal 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: '100px', title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note0', width: 150, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 150, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 150, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVehicleNo', width: '120px', title: 'Đầu kéo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TORomoocNo', width: '120px', title: 'Romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverName', width: '100px', title: 'Tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverTel', width: '100px', title: 'SĐT', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'TOCreatedDate', width: '160px', title: 'Ngày tạo chuyến', template: "#=TOCreatedDate != null ? Common.Date.FromJsonDMYHM(TOCreatedDate) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TOCreatedBy', width: '160px', title: 'Người tạo chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.NewTimeLineItemTemp = {};
    $scope.TimeLineInfoTabIndex = 1;
    $scope.TimeLineV2InfoItem = null;
    $scope.TimeLineV2InfoStyle = { 'display': 'none', 'top': 0, 'left': 0 }
    $scope.$watch("TimeLineInfoTabIndex", function () {
        if ($scope.TimeLineInfoTabIndex == 2) {
            $timeout(function () {
                $scope.new_timeline_trip_info_Grid.refresh();
            }, 10)
        }
    })

    $scope.NewTimeLineInfo_Update_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineItem.ETA == null || $scope.NewTimeLineItem.ETD == null) {
            flag = false;
            $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        } else if ($scope.NewTimeLineItem.ETA <= $scope.NewTimeLineItem.ETD) {
            flag = false;
            $rootScope.Message({ Msg: "Sai ràng buộc ETD và ETA.", Type: Common.Message.Type.Alert });
        } else if ($scope.NewTimeLineItem.DataContainerOffer == null || $scope.NewTimeLineItem.DataContainerOffer == []) {
            flag = false;
            $rootScope.Message({ Msg: "Thời gian không hợp lệ.", Type: Common.Message.Type.Alert });
        } else {
            $rootScope.IsLoading = false;
            $rootScope.Message({
                Msg: "Xác nhận lưu thay đổi?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Time_Update,
                        data: { mID: $scope.NewTimeLineItem.ID, ETD: $scope.NewTimeLineItem.ETD, ETA: $scope.NewTimeLineItem.ETA, dataContainer: $scope.NewTimeLineItem.DataContainerOffer },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.LoadNewTimeLineV2Data(false);
                                $scope.NewTimeLineItem.HasChange = false;
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    });
                },
                Close: function () {
                }
            })
        }
    }

    $scope.new_timeline_trip_info_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.COTOContainerByTrip_List,
            pageSize: 0,
            readparam: function () {
                return { mID: $scope.NewTimeLineItem.ID, conID: -1 }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' }, autoBind: false,
        columns: [
            { field: 'CustomerCode', width: '120px', title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: '100px', title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '80px', title: 'Số seal 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '80px', title: 'Số seal 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: '100px', title: 'Loại cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfContainerName', width: '100px', title: 'Chặng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
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

    //Move tooltip => body
    $('#timeline_v2_tooltip').detach().appendTo('body');

    $scope.ShowTimeLineV2Tooltip = function ($event, uid) {
        if ($scope.TimeLineEventDragDrop == false) {
            var off = $($event.currentTarget).offset();
            $scope.TimeLineV2InfoStyle = {
                'display': '', 'top': off.top + 32, 'left': off.left > 100 ? off.left : 100
            }

            Common.Data.Each($scope.new_timeline_v2_Trip.dataSource.data(), function (i) {
                if (i.id == uid) {
                    $scope.TimeLineV2InfoItem = i;
                }
            })
            try {
                var scheduler = $scope.new_timeline_v2_Trip;
                var ele = scheduler.element.find('.k-scheduler-content tbody');
                var mySVG = ele.connect(ele, 'mycanvas');
                var note = $($event.currentTarget).closest('.k-event');
                var connections = $($event.currentTarget).data('connection');
                if (connections != null && connections.length > 0) {
                    angular.forEach($scope.new_timeline_v2_Trip.items(), function (o) {
                        var evid = $(o).find('.cus-event').data('eventid');
                        if (evid != uid) {
                            var cnts = $(o).find('.cus-event').data('connection');
                            if (cnts != null && cnts.length > 0) {
                                if ($scope.HasItemInList(cnts, connections)) {
                                    Common.Log(cnts);
                                    Common.Log(connections);
                                    Common.Log(note[0]);
                                    Common.Log($('#event' + evid).closest('.k-event')[0]);
                                    mySVG.drawLine({
                                        left_node: note,
                                        right_node: $('#event' + evid).closest('.k-event'),
                                        horizantal_gap: 20,
                                        error: true,
                                        status: 'accepted',
                                        style: 'solid',
                                        width: 3,
                                        sub: 5
                                    });
                                }
                            }
                        }
                    })
                }
            } catch (e) {
            }
        }
    }

    $scope.HideTimeLineV2Tooltip = function ($event) {
        $scope.TimeLineV2InfoItem = null;
        $scope.TimeLineV2InfoStyle = {
            'display': 'none', 'top': 0, 'left': 0
        }
        var scheduler = $scope.new_timeline_v2_Trip;
        var ele = scheduler.element.find('.k-scheduler-content tbody');
        ele.disconnect(ele, 'mycanvas')
    }

    //#region TimeLine Filter
    $scope.new_timeline_v2_service_select_Grid_Options = {
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
                    if ($scope.NewTimeLineVehicleDataService != null && $scope.NewTimeLineVehicleDataService.indexOf(item.ID) > -1) {
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,new_timeline_v2_service_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,new_timeline_v2_service_select_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'Name', title: 'Tên dịch vụ', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.new_timeline_v2_customer_select_Grid_Options = {
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
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.NewTimeLineVehicleDataCustomer != null && $scope.NewTimeLineVehicleDataCustomer.indexOf(item.ID) > -1) {
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,new_timeline_v2_customer_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,new_timeline_v2_customer_select_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'Code', width: '150px', title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.new_timeline_v2_carrier_select_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: { ID: { type: 'number' }, Code: { type: 'string' }, CarrierName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false } }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.NewTimeLineVehicleDataCarrier != null && $scope.NewTimeLineVehicleDataCarrier.indexOf(item.ID) > -1) {
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,new_timeline_v2_carrier_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,new_timeline_v2_carrier_select_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'Code', title: 'Mã', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CarrierName', title: 'Tên hãng tàu', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.new_timeline_v2_seaport_select_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: { ID: { type: 'number' }, SeaportCode: { type: 'string' }, SeaportName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false } }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.NewTimeLineVehicleDataSeaport != null && $scope.NewTimeLineVehicleDataSeaport.indexOf(item.ID) > -1) {
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,new_timeline_v2_seaport_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,new_timeline_v2_seaport_select_Grid)" />',
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

    $scope.new_timeline_v2_status_select_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [{
                ID: 1, StatusName: 'Chuyến mới'
            }, {
                ID: 2, StatusName: 'Gửi giám sát'
            }, {
                ID: 3, StatusName: 'Hoàn thành'
            }, {
                ID: 4, StatusName: 'Khác'
            }],
            model: {
                id: 'ID',
                fields: { ID: { type: 'number' }, StatusName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false } }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, autoBind: false,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.NewTimeLineVehicleDataStatus != null && $scope.NewTimeLineVehicleDataStatus.indexOf(item.ID) > -1) {
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,new_timeline_v2_status_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,new_timeline_v2_status_select_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'StatusName', title: 'Trạng thái', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV4.URL.Customer_List,
        success: function (res) {
            //$scope.cbo_timeLineV2_Customer.dataSource.data(res);
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.new_timeline_v2_customer_select_Grid.dataSource.data(res);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV4.URL.Seaport_List,
        success: function (res) {
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.new_timeline_v2_seaport_select_Grid.dataSource.data(res);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV4.URL.Carrier_List,
        success: function (res) {
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.new_timeline_v2_carrier_select_Grid.dataSource.data(res);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV4.URL.Service_List,
        success: function (res) {
            //$scope.cbo_timeLineV2_Service.dataSource.data(res);
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.new_timeline_v2_service_select_Grid.dataSource.data(res);
        }
    })

    $scope.TimelineOrderV2ViewServiceSelect_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineVehicleDataService.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.new_timeline_v2_service_select_Grid.refresh();
            }, 100)
        } else {
            $scope.NewTimeLineVehicleDataService = [];
            $timeout(function () {
                $scope.new_timeline_v2_service_select_Grid.refresh();
            }, 100)
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
    }

    $scope.TimelineOrderV2ViewCustomerSelect_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineVehicleDataCustomer.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.new_timeline_v2_customer_select_Grid.refresh();
            }, 100)
        } else {
            $scope.NewTimeLineVehicleDataCustomer = [];
            $timeout(function () {
                $scope.new_timeline_v2_customer_select_Grid.refresh();
            }, 100)
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
    }

    $scope.NewTimeLineV2ServiceSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.NewTimeLineVehicleDataService.sort().toString()) {
            $scope.NewTimeLineVehicleDataService = data;
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
        win.close();
    }

    $scope.NewTimeLineV2CustomerSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.NewTimeLineVehicleDataCustomer.sort().toString()) {
            $scope.NewTimeLineVehicleDataCustomer = data;
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
        win.close();
    }

    $scope.TimelineOrderV2ViewCarrierSelect_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineVehicleDataCarrier.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.new_timeline_v2_carrier_select_Grid.refresh();
            }, 100)
        } else {
            $scope.NewTimeLineVehicleDataCarrier = [];
            $timeout(function () {
                $scope.new_timeline_v2_carrier_select_Grid.refresh();
            }, 100)
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
    }

    $scope.TimelineOrderV2ViewSeaportSelect_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineVehicleDataSeaport.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.new_timeline_v2_seaport_select_Grid.refresh();
            }, 100)
        } else {
            $scope.NewTimeLineVehicleDataSeaport = [];
            $timeout(function () {
                $scope.new_timeline_v2_seaport_select_Grid.refresh();
            }, 100)
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
    }

    $scope.NewTimeLineV2CarrierSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.NewTimeLineVehicleDataCarrier.sort().toString()) {
            $scope.NewTimeLineVehicleDataCarrier = data;
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
        win.close();
    }

    $scope.NewTimeLineV2SeaportSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.NewTimeLineVehicleDataSeaport.sort().toString()) {
            $scope.NewTimeLineVehicleDataSeaport = data;
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
        win.close();
    }

    $scope.TimelineOrderV2ViewStatusSelect_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineVehicleDataStatus.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.new_timeline_v2_status_select_Grid.refresh();
            }, 100)
        } else {
            $scope.NewTimeLineVehicleDataStatus = [];
            $timeout(function () {
                $scope.new_timeline_v2_status_select_Grid.refresh();
            }, 100)
            $scope.LoadNewTimeLineV2Data(false);
        }
    }

    $scope.NewTimeLineV2StatusSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.NewTimeLineVehicleDataStatus.sort().toString()) {
            $scope.NewTimeLineVehicleDataStatus = data;
            $scope.LoadNewTimeLineV2Data(false);
        }
        win.close();
    }

    //#endregion

    //#region DragToEvent
    $scope.IsNewTimeLineDragDropInfoBound = false;
    $scope.NewScheduleDragDropInfoVehicleType = 1;
    $scope.IsShowNewTimeLineDragDropInfo = false;
    $scope.NewScheduleDragDropInfoTOMasterID = -1;

    $scope.NewTimeLineV2_Change_Time_DataTemp = [];
    $scope.NewTimeLineV2_Change_Time_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.NewTimeLineItem.HasTimeChange) {
            $scope.NewTimeLineItem.HasTimeChange = false;
            var obj = $scope.NewTimeLineItem;
            var vehID = obj.VehicleID > 0 ? obj.VehicleID : -1,
                romID = obj.RomoocID > 0 ? obj.RomoocID : -1,
                venID = obj.VendorOfVehicleID > 0 ? obj.VendorOfVehicleID : -1,
                etd = obj.ETD != null && angular.isDate(obj.ETD) ? obj.ETD : new Date(),
                eta = obj.ETA != null && angular.isDate(obj.ETA) ? obj.ETA : new Date()
            $scope.LoadDataDragDropInfo(obj.ID, -1, venID, vehID, romID, etd, eta, $scope.NewTimeLineItem.ListOPS_Container || [], $scope.NewTimeLineItem.ListOPSCon || []);
        }
        else {
            $rootScope.IsLoading = true;
            $scope.new_timeline_drapdrop_info_win.center().open();
            $scope.IsShowNewTimeLineDragDropInfo = true;
            $scope.NewTimeLineV2_Change_Time_DataTemp = {};
            angular.forEach($scope.new_timeline_dragdrop_info.dataSource.data(), function (o, i) {
                if ((o.TypeOfGroupID == 1 && o.Code == 'New') || o.TypeOfGroupID == 4) {
                    $scope.NewTimeLineV2_Change_Time_DataTemp[o.id + "_" + o.field] = {
                        id: o.id, start: o.start, end: o.end, field: o.field
                    }
                }
            })
            $timeout(function () {
                $scope.new_timeline_dragdrop_info.date($scope.NewTimeLineItem.ETD);
                $scope.new_timeline_dragdrop_info.view($scope.new_timeline_dragdrop_info.view().name);
                $rootScope.IsLoading = false;
            }, 300)
        }
    }

    $scope.NewTimeLineV2_Change_Driver_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận cập nhật lại tài xế cho chuyến hiện tại?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV4.URL.CO2View_Master_Update_Driver,
                    data: { data: [$scope.NewTimeLineItem.ID] },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!' });
                            $scope.NewTimeLineItem.DriverName = res.DriverName;
                            $scope.NewTimeLineItem.DriverTel = res.DriverTel;
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    $scope.NewScheduleDragDropInfoVehicle_Click = function ($event, scheduler, value) {
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

    $scope.LoadDataDragDropInfo = function (m1ID, m2ID, venID, vehID, romID, ETD, ETA, dataOPSCon, dataCon) {
        Common.Log("LoadDataDragDropInfo");

        $scope.NewScheduleDragDropInfoTOMasterID = m1ID;
        $scope.IsNewTimeLineDragDropInfoBound = false;
        $scope.NewScheduleDragDropInfoVehicleType = 1;
        $scope.IsShowNewTimeLineDragDropInfo = false;
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.Info_Schedule_Data,
            data: {
                m1ID: m1ID, m2ID: m2ID,
                venID: venID, vehID: vehID, romID: romID,
                ETA: ETA, ETD: ETD, typeOfResource: 1,
                dataOPSCon: dataOPSCon, dataCon: dataCon
            },
            success: function (res) {
                $scope.IsShowNewTimeLineDragDropInfo = true;
                Common.Data.Each(res.DataSources, function (o) {
                    o.field = o.TypeOfGroupID + "-" + o.GroupID;
                })
                var dataG4 = $.grep(res.DataSources, function (o) { return o.TypeOfGroupID == 4 });
                var dataG1 = $.grep(res.DataSources, function (o) { return o.TypeOfGroupID <= 2 && o.ID == $scope.NewScheduleDragDropInfoTOMasterID });
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
                $scope.new_timeline_dragdrop_info.setDataSource(dataSource);
                Common.Data.Each(res.Resources, function (o) {
                    o.text = o.Text; o.value = o.TypeOfGroupID + "-" + o.Value;
                })
                var filter = {
                    logic: "or",
                    filters: [
                        { field: "value", operator: "startswith", value: $scope.NewScheduleDragDropInfoVehicleType + "-" },
                        { field: "value", operator: "startswith", value: "3-" }, { field: "value", operator: "startswith", value: "4-" }
                    ]
                };
                $scope.new_timeline_dragdrop_info.resources[0].dataSource.data(res.Resources);
                $scope.new_timeline_dragdrop_info.resources[0].dataSource.filter(filter);
                $scope.new_timeline_drapdrop_info_win.center().open();
                $timeout(function () {
                    angular.forEach($scope.new_timeline_dragdrop_info.dataSource.data(), function (o, i) {
                        if ((o.TypeOfGroupID == 1 && o.Code == 'New') || o.TypeOfGroupID == 4) {
                            $scope.NewTimeLineV2_Change_Time_DataTemp[o.id + "_" + o.field] = {
                                id: o.id, start: o.start, end: o.end, field: o.field
                            }
                        }
                    })
                    $scope.new_timeline_dragdrop_info.date(ETD);
                    $scope.new_timeline_dragdrop_info.view($scope.new_timeline_dragdrop_info.view().name);
                    $rootScope.IsLoading = false;
                }, 500)
            }
        })
    }

    $scope.NewInfoTimeLineEventItem = {};

    $scope.new_timeline_dragdrop_info_Options = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: true, resize: true, update: false },
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
        eventTemplate: $("#new-timeline-v2-dragdrop-info-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.IsNewTimeLineDragDropInfoBound == false && $scope.IsShowNewTimeLineDragDropInfo == true) {
                    $scope.IsNewTimeLineDragDropInfoBound = true;
                    scheduler.view(scheduler.view().name);
                    //scheduler.element.find('.k-nav-today a').trigger('click');
                } else if ($scope.IsNewTimeLineDragDropInfoBound == true && $scope.IsShowNewTimeLineDragDropInfo == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case 1:
                                        if (i.TypeOfGroupID == 1 || i.TypeOfGroupID == 2) {
                                            $(o).find('.k-resize-handle').hide();
                                            if (i.id == $scope.NewScheduleDragDropInfoTOMasterID) {
                                                $(o).addClass('approved');
                                            } else {
                                                $(o).addClass('tendered');
                                            }
                                        } else if (i.TypeOfGroupID == 3) {
                                            $(o).find('.k-resize-handle').hide();
                                            if ($scope.NewScheduleDragDropInfoTOMasterID > 0) {
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
                                if (uid != null && uid.split('-')[0] != 3) {
                                    $(td).css('background', 'rgb(255, 249, 158)');
                                }
                            }
                        })
                        scheduler.element.find('.k-scheduler-times tr').each(function (idx, tr) {
                            var uid = $(tr).find('span.txtGroup').data('uid');
                            if (uid != null && uid.split('-')[0] != 3) {
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
                $scope.NewInfoTimeLineEventItem = $.extend(true, {}, e.event);
            }
        },
        resizeStart: function (e) {
            if (e.event.TypeOfGroupID < 3 || (e.event.TypeOfGroupID == 3 && e.event.StatusOfEvent != 1)) {
                e.preventDefault();
            } else {
                $scope.NewInfoTimeLineEventItem = $.extend(true, {}, e.event);
            }
        },
        save: function (e) {
            var scheduler = this, obj = $.extend(true, {}, e.event), data = scheduler._data, field = "";
            if (typeof obj.field == "string") field = obj.field;
            else if (typeof obj.field == "object") field = obj.field[0];
            if (obj.TypeOfEvent == 1) {
                if (obj.TypeOfGroupID == 4) {
                    if (!field.startsWith(obj.TypeOfGroupID)) {
                        $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                        var idx = 0;
                        $.each(scheduler.dataSource.data(), function (i, o) {
                            if (o.id == obj.id) {
                                idx = i;
                            }
                        })
                        var item = scheduler.dataSource.at(idx);
                        if (Common.HasValue(item)) {
                            item.end = $scope.NewInfoTimeLineEventItem.end;
                            item.start = $scope.NewInfoTimeLineEventItem.start;
                            item.field = $scope.NewInfoTimeLineEventItem.field;
                        }
                        scheduler.refresh();
                    } else {
                        if (obj.start >= obj.end) {
                            $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                            var idx = 0;
                            $.each(scheduler.dataSource.data(), function (i, o) {
                                if (o.id == obj.id) {
                                    idx = i;
                                }
                            })
                            var item = scheduler.dataSource.at(idx);
                            if (Common.HasValue(item)) {
                                item.end = $scope.NewInfoTimeLineEventItem.end;
                                item.start = $scope.NewInfoTimeLineEventItem.start;
                                item.field = $scope.NewInfoTimeLineEventItem.field;
                            }
                            scheduler.refresh();
                        } else {
                            if (obj.start >= $scope.NewInfoTimeLineEventItem.start) {
                                //-->
                                var ds = new kendo.data.DataSource({
                                    data: data,
                                    filter: [{
                                        field: 'start', operator: 'gte', value: $scope.NewInfoTimeLineEventItem.end
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
                                    var dataG1 = $.grep(data, function (o) { return o.TypeOfGroupID <= 2 && o.id == $scope.NewScheduleDragDropInfoTOMasterID });
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
                                        field: 'end', operator: 'lte', value: $scope.NewInfoTimeLineEventItem.start
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
                                    if ($scope.NewScheduleDragDropInfoTOMasterID < 1) {
                                        var dataG4 = $.grep(data, function (o) { return o.TypeOfGroupID == 4 });
                                        var dataG1 = $.grep(data, function (o) { return o.TypeOfGroupID <= 2 && o.id == $scope.NewScheduleDragDropInfoTOMasterID });
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
                                    }
                                });
                            }
                        }
                    }
                } else if (obj.TypeOfGroupID == 3) {
                    if (!field.startsWith(obj.TypeOfGroupID)) {
                        $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                        var idx = 0;
                        $.each(scheduler.dataSource.data(), function (i, o) {
                            if (o.id == obj.id) {
                                idx = i;
                            }
                        })
                        var item = scheduler.dataSource.at(idx);
                        if (Common.HasValue(item)) {
                            item.end = $scope.NewInfoTimeLineEventItem.end;
                            item.start = $scope.NewInfoTimeLineEventItem.start;
                            item.field = $scope.NewInfoTimeLineEventItem.field;
                        }
                        scheduler.refresh();
                    } else {
                        if (obj.start >= obj.end) {
                            $rootScope.Message({ Type: Common.Message.Type.Alert, Msg: "Thao tác không hợp lệ!" })
                            var idx = 0;
                            $.each(scheduler.dataSource.data(), function (i, o) {
                                if (o.id == obj.id) {
                                    idx = i;
                                }
                            })
                            var item = scheduler.dataSource.at(idx);
                            if (Common.HasValue(item)) {
                                item.end = $scope.NewInfoTimeLineEventItem.end;
                                item.start = $scope.NewInfoTimeLineEventItem.start;
                                item.field = $scope.NewInfoTimeLineEventItem.field;
                            }
                            scheduler.refresh();
                        } else {
                            if (obj.start >= $scope.NewInfoTimeLineEventItem.start) {
                                //-->
                                var ds = new kendo.data.DataSource({
                                    data: data,
                                    filter: [{
                                        field: 'start', operator: 'gte', value: $scope.NewInfoTimeLineEventItem.end
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
                                        field: 'end', operator: 'lte', value: $scope.NewInfoTimeLineEventItem.start
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
                    var idx = 0;
                    $.each(scheduler.dataSource.data(), function (i, o) {
                        if (o.id == obj.id) {
                            idx = i;
                        }
                    })
                    var item = scheduler.dataSource.at(idx);
                    if (Common.HasValue(item)) {
                        item.end = $scope.NewInfoTimeLineEventItem.end;
                        item.start = $scope.NewInfoTimeLineEventItem.start;
                        item.field = $scope.NewInfoTimeLineEventItem.field;
                    }
                    scheduler.refresh();
                }
            }
        }
    }

    $scope.LoadDataOfferFromTimeLine = false;

    $scope.NewTimeLineDragDropInfo_OK_Click = function ($event, scheduler, win) {
        $event.preventDefault();

        if ($scope.NewScheduleDragDropInfoTOMasterID < 1) {
            //Cập nhật ETA, ETD của chuyến/ ===> XXX
            var to = $.grep(scheduler.dataSource.data(), function (o) { return o.id == $scope.NewTimeLineItem.ID && o.TypeOfGroupID == 1 });
            if (to.length > 0) {
                $scope.LoadDataOfferFromTimeLine = true;
                $scope.NewTimeLineItem.ETA = to[0].end;
                $scope.NewTimeLineItem.ETD = to[0].start;
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
                $scope.NewTimeLineItem.DataContainerOffer = data;
                win.close();
            })
        } else {
            //Điều chỉnh chuyến
            if ($scope.NewTimeLineDetail) {
                var to = $.grep(scheduler.dataSource.data(), function (o) { return o.id == $scope.NewTimeLineItem.ID && o.TypeOfGroupID == 1 });
                if (to.length > 0) {
                    $scope.LoadDataOfferFromTimeLine = true;
                    $scope.NewTimeLineItem.ETA = to[0].end;
                    $scope.NewTimeLineItem.ETD = to[0].start;
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
                    $scope.NewTimeLineItem.DataContainerOffer = data;
                    win.close();
                })
            } else { //Thêm container vào chuyến 
                var ds = new kendo.data.DataSource({
                    data: scheduler.dataSource.data(),
                    filter: [{
                        field: 'TypeOfGroupID', operator: 'gte', value: 3
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
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV4.URL.Info_Schedule_Save_Check,
                        data: {
                            mID: $scope.NewScheduleDragDropInfoTOMasterID, data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                if (res == null || res == '') {
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({
                                        Type: Common.Message.Type.Confirm,
                                        Msg: 'Bạn muốn thay đổi chuyến theo thời gian đã điều chỉnh?',
                                        Ok: function () {
                                            $rootScope.IsLoading = true;
                                            Common.Services.Call($http, {
                                                url: Common.Services.url.OPS,
                                                method: _OPSAppointment_COViewOnMapV4.URL.Info_Schedule_Save,
                                                data: {
                                                    mID: $scope.NewScheduleDragDropInfoTOMasterID, data: data
                                                },
                                                success: function (res) {
                                                    Common.Services.Error(res, function (res) {
                                                        $rootScope.IsLoading = false;
                                                        $scope.Message({ Msg: 'Thành công!' });
                                                        $scope.LoadNewTimeLineV2Data(true);
                                                        win.close();
                                                    })
                                                }
                                            })
                                        }
                                    })
                                } else {
                                    $rootScope.IsLoading = false;
                                    $scope.Message({ Msg: msg, Type: Common.Message.Type.Alert })
                                }
                            })
                        }
                    })
                });
            }
        }
    }

    $scope.NewTimeLineInfoItem = null;
    $scope.NewTimeLineInfoStyle = { 'display': 'none', 'top': 0, 'left': 0 };

    //Move tooltip => body
    $('#new_timeline_info_tooltip').detach().appendTo('body');

    $scope.ShowNewTimeLineInfoTooltip = function ($event, uid) {
        var flag = false;
        Common.Data.Each($scope.new_timeline_dragdrop_info.dataSource.data(), function (i) {
            if (i.uid == uid) {
                if (i.TypeOfGroupID == 3) {
                    flag = true;
                    switch (i.Code) {
                        case 'ETD-ETA':
                            $scope.NewTimeLineInfoItem = { ETA: i.end, ETD: i.start };
                            $scope.NewTimeLineInfoItem.TypeDate = "Kế hoạch";
                            break;
                        case 'ETDRequest':
                            $scope.NewTimeLineInfoItem = { ETD: i.end, ETA: null };
                            $scope.NewTimeLineInfoItem.TypeDate = "Ngày yêu cầu lấy hàng";
                            break;
                        case 'ETARequest':
                            $scope.NewTimeLineInfoItem = { ETD: i.end, ETA: null };
                            $scope.NewTimeLineInfoItem.TypeDate = "Ngày yêu cầu giao hàng";
                            break;
                        case 'CutOffTime':
                            $scope.NewTimeLineInfoItem = { ETD: i.end, ETA: null };
                            $scope.NewTimeLineInfoItem.TypeDate = "Hạn chót cắt máng/trả rỗng";
                            break;
                        default: break;
                    }
                }
            }
        })
        if (flag) {
            var off = $($event.currentTarget).offset();
            $scope.NewTimeLineInfoStyle = {
                'display': '', 'top': off.top + 32, 'left': off.left > 100 ? off.left : 100
            }
        }
    }

    $scope.HideNewTimeLineInfoTooltip = function ($event) {
        $scope.NewTimeLineInfoItem = null;
        $scope.NewTimeLineInfoStyle = {
            'display': 'none', 'top': 0, 'left': 0
        }
    }

    //#endregion

    $scope.NewTimeLineGroupString = '';
    $scope.NewTimeLineContainerString = "";
    $scope.$watch("$scope.NewTimeLineGroupString", function () {

    })

    //#region TimeLineAction
    $scope.TimeLineViewTripAction = false;
    $scope.TimelineOrderV2ViewTripAction_Click = function ($event, scheduler) {
        $event.preventDefault();

        $scope.TimeLineViewTripAction = !$scope.TimeLineViewTripAction;
        if ($scope.TimeLineViewTripAction) {
            $scope.InitTimeLineViewActionDragDrop(scheduler);
        } else {
            $(scheduler.element).find('.k-scheduler-content').each(function (i, o) {
                $(o).removeClass('action-timeline');
            })
        }
    }
    $('.panel.panel_action.panel_action_tomon,.panel.panel_action.panel_action_toord').kendoDropTarget({
        drop: function (e) {
            $scope.OnTimeLineViewActionDrop(e);
        },
        dragenter: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 1);
        },
        dragleave: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 0.5);
        },
        group: "timelineAction1"
    });
    $('.panel.panel_action.panel_action_toops').kendoDropTarget({
        drop: function (e) {
            $scope.OnTimeLineViewActionDrop(e);
        },
        dragenter: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 1);
        },
        dragleave: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 0.5);
        },
        group: "timelineAction2"
    });

    $scope.InitTimeLineViewActionDragDrop = function (scheduler) {
        $(scheduler.element).find('.k-scheduler-content').each(function (i, o) {
            $(o).addClass('action-timeline');
        })
        $(scheduler.element).kendoDraggable({
            filter: ".action-timeline .k-event.approved,.action-timeline .k-event.tenderable", group: "timelineAction1", cursorOffset: { top: 0, left: 0 },
            drag: function (e) {
                $scope.TimeLineEventDragDrop = true;
            },
            dragend: function (e) {
                $scope.TimeLineEventDragDrop = false;
            },
            hint: function (e) {
                return e.clone();
            }
        });
        $(scheduler.element).kendoDraggable({
            filter: ".action-timeline .k-event.tendered", group: "timelineAction2", cursorOffset: { top: 0, left: 0 },
            drag: function (e) {
                $scope.TimeLineEventDragDrop = true;
            },
            dragend: function (e) {
                $scope.TimeLineEventDragDrop = true;
            },
            hint: function (e) {
                return e.clone();
            }
        });
    }

    $scope.OnTimeLineViewActionDrop = function (e) {
        var scheduler = $scope.new_timeline_v2_Trip, element = e.draggable.hint,
            item = scheduler.dataSource.getByUid(element.data('uid')), action = e.dropTarget.data('action');
        if (Common.HasValue(item)) {
            switch (action) {
                case 1:
                    $rootScope.Message({
                        Msg: 'Xác nhận duyệt chuyến?',
                        Type: Common.Message.Type.Confirm,
                        Ok: function (o) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.ToMon,
                                data: { data: [item.id] },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.IsLoading = false;
                                        item.StatusOfEvent = 2;
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        scheduler.refresh();
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        }
                    })
                    break;
                case 2:
                    $rootScope.Message({
                        Msg: 'Xác nhận hủy duyệt chuyến?',
                        Type: Common.Message.Type.Confirm,
                        Ok: function (o) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.ToOPS,
                                data: { data: [item.id] },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.IsLoading = false;
                                        item.StatusOfEvent = 11;
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        scheduler.refresh();
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        }
                    })
                    break;
                case 3:
                    $rootScope.Message({
                        Msg: 'Xác nhận xóa chuyến?',
                        Type: Common.Message.Type.Confirm,
                        Ok: function (o) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.Delete,
                                data: { data: [item.id] },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.IsLoading = false;
                                        scheduler.dataSource.remove(item);
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        scheduler.refresh();
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        }
                    })
                    break;
            }
        }
    }
    //#endregion

    $scope.NewTimeLineChangeVehicleType = 1;
    $scope.IsShowNewTimeLineVehicleMap = false;
    $scope.NewTimeLineVehicleMapLoading = false;
    $scope.IsNewTimeLineVehicleMapBound = false;
    $scope.NewTimeLineVehicleMapParam = {
        Class: 'column2layout',
        Config: [
            { Label: "Số xe", Width: 100, hasShow: true, SortOrder: 1, Field: "VehicleNo" },
            { Label: "Trọng tải", Width: 100, hasShow: true, SortOrder: 2, Field: "MaxWeight" },
            { Label: "Loại xe", Width: 100, hasShow: false, SortOrder: 3, Field: "Option1" },
            { Label: "Romooc hiện tại", Width: 100, hasShow: false, SortOrder: 4, Field: "RomoocNo" }
        ]
    }
    $scope.NewTimeLineVehicleMapDataResource = {};

    $scope.timelineVehicleSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true },
            { collapsible: true, resizable: true, size: '50%', min: '450px' }
        ],
        resize: function (e) {
            try {
                if ($scope.NewTimeLineChangeVehicleType == 1) {
                    $scope.new_timeline_map_vehicle.refresh();
                } else {
                    $scope.new_timeline_map_romooc.refresh();
                }
            }
            catch (e) { }
        }
    }

    $scope.NewTimeLineTripVehicle_Change_Click = function ($event, typeofVehicle, win) {
        $event.preventDefault();

        if (!$scope.NewTimeLineItem.IsAllowChangeRomooc && typeofVehicle == 2) {
            $rootScope.Message({ Msg: "Không cho phép đổi romooc", Type: Common.Message.Type.Alert });
        } else {
            win.center().open();
            $scope.ChangeVehicleType = typeofVehicle;
            $scope.NewTimeLineChangeVehicleType = typeofVehicle;
            $timeout(function () {
                $scope.timelineVehicleSplitter.resize();
                $scope.LoadVehicleMapScheduleData();
                $rootScope.IsLoading = false;
            }, 100);
        }
    }

    $scope.LoadVehicleMapScheduleData = function () {
        $scope.NewTimeLineVehicleMapLoading = true;
        $scope.IsNewTimeLineVehicleMapBound = false;
        var min = new Date($scope.NewTimeLineItem.ETA.getTime() - ($scope.NewTimeLineItem.ETA - $scope.NewTimeLineItem.ETD)).addDays(-7);
        var max = min.addDays(14);
        $scope.new_timeline_map_vehicle_Options.min = min;
        $scope.new_timeline_map_vehicle_Options.max = max;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.Vehicle_Schedule_Data,
            data: {
                mID: $scope.NewTimeLineItem.ID,
                vehID: $scope.NewTimeLineItem.VehicleID,
                romID: $scope.NewTimeLineItem.RomoocID,
                fDate: min, tDate: max,
                typeOfResource: $scope.NewTimeLineChangeVehicleType
            },
            success: function (res) {
                if ($scope.NewTimeLineChangeVehicleType == 1) {
                    $scope.NewTimeLineVehicleMapDataResource = {};
                    Common.Data.Each(res.Resources, function (o) {
                        o.value = o.VehicleID; o.text = o.Text;
                        $scope.NewTimeLineVehicleMapDataResource[o.value] = o;
                    })
                    if (res.Resources.length == 0) {
                        res.Resources = [{
                            value: '-1', text: "DL trống", VendorID: -1, VendorCode: "", VendorName: "", RomoocID: -1, RomoocNo: "", VehicleID: -1, VehicleNo: ""
                        }];
                    }
                    Common.Data.Each(res.DataSources, function (o) {
                        o.field = o.VehicleID;
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
                    $timeout(function () {
                        $scope.new_timeline_map_vehicle.setDataSource(dataSource);
                        $scope.IsShowNewTimeLineVehicleMap = true;
                        $scope.new_timeline_map_vehicle.resources[0].dataSource.data(res.Resources);
                    }, 100)
                }
                else if ($scope.NewTimeLineChangeVehicleType == 2) {
                    $scope.NewTimeLineVehicleMapDataResource = {};
                    Common.Data.Each(res.Resources, function (o) {
                        o.value = o.RomoocID; o.text = o.Text;
                        $scope.NewTimeLineVehicleMapDataResource[o.value] = o;
                    })
                    if (res.Resources.length == 0) {
                        res.Resources = [{
                            value: '-1', text: "DL trống", VendorID: -1, VendorCode: "", VendorName: "", RomoocID: -1, RomoocNo: "", VehicleID: -1, VehicleNo: ""
                        }];
                    }
                    Common.Data.Each(res.DataSources, function (o) {
                        o.field = o.VehicleID;
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
                    $timeout(function () {
                        $scope.new_timeline_map_vehicle.setDataSource(dataSource);
                        $scope.IsShowNewTimeLineVehicleMap = true;
                        $scope.new_timeline_map_vehicle.resources[0].dataSource.data(res.Resources);
                    }, 100)
                }
            }
        })
    }

    var TWOWEEKVIEW = kendo.ui.TimelineMonthView.extend({
        nextDate: function () {
            return kendo.date.nextDay(this.startDate());
        },
        options: {
            columnWidth: 50, majorTick: 360, daysOfView: 14, selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
            dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
            majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>")
        },
        name: "TWOWEEKVIEW",
        calculateDateRange: function () {
            var start = this.options.min, idx, length, dates = [];
            for (idx = 0, length = this.options.daysOfView; idx < length; idx++) {
                dates.push(start); start = kendo.date.nextDay(start);
            }
            this._render(dates);
        }
    });

    var strCookie = Common.Cookie.Get('OPSCO_ViewOnMap_Config');
    if (Common.HasValue(strCookie)) {
        try {
            var objCookie = JSON.parse(strCookie);
            $scope.NewTimeLineVehicleMapParam = objCookie;
            var noShow = $.grep($scope.NewTimeLineVehicleMapParam.Config, function (o) { return o.hasShow == true }).length;
            if (noShow == 3) $scope.NewTimeLineVehicleMapParam.Class = 'column3layout';
            else if (noShow == 2) $scope.NewTimeLineVehicleMapParam.Class = 'column2layout';
            else if (noShow == 1 || noShow == 0) $scope.NewTimeLineVehicleMapParam.Class = 'column1layout';
        }
        catch (e) { }
    }

    $scope.NewTimeLineVehicleMapGroup_GetItem = function (value, field) {
        var obj = $scope.NewTimeLineVehicleMapDataResource[value];
        if (Common.HasValue(obj)) return obj[field];
        else return "--N/a--";
    }

    $scope.new_timeline_map_vehicle_Options = {
        date: new Date(), min: new Date().addDays(-7), max: new Date().addDays(7), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" }, editable: false,
        views: [
            {
                type: "timeline", title: "Ngày", columnWidth: 40, selectedDateFormat: "{0:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 120
            },
            {
                type: TWOWEEKVIEW, title: 'Custom', columnWidth: 50, selectedDateFormat: "{0:d-M} - {1:d-M}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 120
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
        groupHeaderTemplate: $("#new-timeline-vehicle-map-group-template").html(),
        eventTemplate: $("#new-timeline-vehicle-map-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '-1', text: 'Data Empty' }], multiple: true
            }
        ],
        dataBound: function (e) {
            var scheduler = this;
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.IsNewTimeLineVehicleMapBound == false && $scope.IsShowNewTimeLineVehicleMap == true) {
                    $scope.IsNewTimeLineVehicleMapBound = true;
                    scheduler.view(scheduler.view().name);
                } else if ($scope.IsNewTimeLineVehicleMapBound == true && $scope.IsShowNewTimeLineVehicleMap == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case 1:
                                        if (i.StatusOfEvent == 1) {
                                            $(o).addClass('approved');
                                        } else if (i.StatusOfEvent == 2) {
                                            $(o).addClass('tendered');
                                        } else if (i.StatusOfEvent == 3) {
                                            $(o).addClass('recieved');
                                        } else if (i.StatusOfEvent == 11) {
                                            $(o).addClass('tenderable');
                                        } else {
                                            $(o).addClass('undefined');
                                        }
                                        break;
                                    case 2:
                                        $(o).addClass('maintainance');
                                        break;
                                    case 3:
                                        $(o).addClass('registry');
                                        break;
                                    case 4:
                                        $(o).addClass('repair');
                                        break;
                                    default:
                                        break;
                                }
                            }
                        })
                    })
                    angular.forEach($(scheduler.element).find('.k-scheduler-times tr'), function (o, i) {
                        $compile(o)($scope);
                    });
                    scheduler.element.find('.k-scheduler-times tr').each(function () {
                        var uid = $(this).find('.txtGroup').data('uid');
                        if (uid > 0) {
                            var idx = $scope.NewTimeLineVehicleMapGroup_GetItem(uid, "SortOrder");
                            if (idx > 0) {
                                $(this).css('background', 'rgb(255, 249, 158)');
                            }
                            $(this).css('cursor', 'pointer');
                            $(this).on('click', false);
                            $(this).click(function (e) {
                                var txt = $(this).find('.txtGroup').data('text');
                                if ($scope.NewTimeLineChangeVehicleType == 1) {
                                    $rootScope.Message({
                                        Msg: "Xác nhận chọn đầu kéo?",
                                        Type: Common.Message.Type.Confirm,
                                        Ok: function () {
                                            $rootScope.IsLoading = true;
                                            Common.Services.Call($http, {
                                                url: Common.Services.url.OPS,
                                                method: _OPSAppointment_COViewOnMapV4.URL.CO2View_Master_Update_TimeLine,
                                                data: { mID: $scope.NewTimeLineItem.ID, vehicleID: uid, isTractor: true, ETD: $scope.NewTimeLineItem.ETD, ETA: $scope.NewTimeLineItem.ETA },
                                                success: function (res) {
                                                    Common.Services.Error(res, function (res) {
                                                        $scope.ChangeData = true;
                                                        $rootScope.IsLoading = false;
                                                        $rootScope.Message({ Msg: 'Thành công!' });
                                                        $scope.LoadNewTimeLineV2Data(false);
                                                        $scope.NewTimeLineItem.HasChange = false;
                                                        $scope.NewTimeLineItemTemp.VehicleID = uid;
                                                        $scope.NewTimeLineItemTemp.VehicleNo = txt;
                                                        $scope.NewTimeLineItem.VehicleID = uid;
                                                        $scope.NewTimeLineItem.VehicleNo = txt;
                                                        $scope.NewTimeLineItem.HasTimeChange = true;
                                                        $scope.timeline_vehicle_map_win.close();
                                                    }, function () {
                                                        $rootScope.IsLoading = false;
                                                    })
                                                }
                                            })
                                        }
                                    })
                                } else {
                                    $rootScope.Message({
                                        Msg: "Xác nhận chọn romooc?",
                                        Type: Common.Message.Type.Confirm,
                                        Ok: function () {
                                            $rootScope.IsLoading = true;
                                            Common.Services.Call($http, {
                                                url: Common.Services.url.OPS,
                                                method: _OPSAppointment_COViewOnMapV4.URL.CO2View_Master_Update_TimeLine,
                                                data: { mID: $scope.NewTimeLineItem.ID, vehicleID: uid, isTractor: false, ETD: $scope.NewTimeLineItem.ETD, ETA: $scope.NewTimeLineItem.ETA },
                                                success: function (res) {
                                                    Common.Services.Error(res, function (res) {
                                                        $scope.ChangeData = true;
                                                        $rootScope.IsLoading = false;
                                                        $rootScope.Message({ Msg: 'Thành công!' });
                                                        $scope.LoadNewTimeLineV2Data(false);
                                                        $scope.NewTimeLineItem.HasChange = false;
                                                        $scope.NewTimeLineItemTemp.RomoocID = uid;
                                                        $scope.NewTimeLineItemTemp.RomoocNo = txt;
                                                        $scope.NewTimeLineItem.RomoocID = uid;
                                                        $scope.NewTimeLineItem.RomoocNo = txt;
                                                        $scope.NewTimeLineItem.HasTimeChange = true;
                                                        $scope.timeline_vehicle_map_win.close();
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
                    scheduler.element.find('.k-scheduler-content tr td').each(function (idx, td) {
                        var slot = scheduler.slotByElement(td), resource = scheduler.resources[0].dataSource.data();
                        if (Common.HasValue(slot) && Common.HasValue(resource[slot.groupIndex])) {
                            var idx = resource[slot.groupIndex].SortOrder;
                            if (idx > 0) {
                                $(td).css('background', 'rgb(255, 249, 158)');
                            }
                        }
                    })
                    $timeout(function () {
                        $scope.NewTimeLineVehicleMapLoading = false;
                        scheduler.resize();
                    }, 2000)
                }
            }, 10)
        },
        navigate: function (e) {
            if (e.view == 'Custom' && (e.action == 'next' || e.action == 'previous'))
                e.preventDefault();
        }
    }

    $scope.NewTimeLineVehicleMapGroupClick = function ($event, uid, txt) {
        if ($scope.NewTimeLineChangeVehicleType == 1) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Vehicle_Offer,
                data: { mID: $scope.NewTimeLineItem.ID, venID: $scope.NewTimeLineItem.VendorOfVehicleID, vehID: uid, isTractor: true },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        if (res != null && res.OfferTimeError != null && res.OfferTimeError != "") {
                            $rootScope.Message({ Msg: res.OfferTimeError, Type: Common.Message.Type.Alert })
                            _refreshScheduler(scheduler);
                        } else {
                            var msg = "Xác nhận chọn đầu kéo?";
                            if (res != null && res.OfferTimeWarning != null && res.OfferTimeWarning != "")
                                msg = res.OfferTimeWarning + ", tiếp tục lưu đầu kéo?";
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: msg,
                                Type: Common.Message.Type.Confirm,
                                Ok: function () {
                                    $rootScope.IsLoading = true;
                                    Common.Services.Call($http, {
                                        url: Common.Services.url.OPS,
                                        method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Vehicle_Update,
                                        data: { mID: $scope.NewTimeLineItem.ID, venID: $scope.NewTimeLineItem.VendorOfVehicleID, vehID: uid, isTractor: true },
                                        success: function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $scope.ChangeData = true;
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: 'Thành công!' });
                                                $scope.LoadNewTimeLineV2Data(false);
                                                $scope.NewTimeLineItem.HasChange = false;
                                                $scope.NewTimeLineItemTemp.VehicleID = uid;
                                                $scope.NewTimeLineItemTemp.VehicleNo = txt;
                                                $scope.NewTimeLineItem.VehicleID = uid;
                                                $scope.NewTimeLineItem.VehicleNo = txt;
                                                $scope.NewTimeLineItem.HasTimeChange = true;
                                                $scope.timeline_vehicle_map_win.close();
                                            }, function () {
                                                $rootScope.IsLoading = false;
                                            })
                                        }
                                    });
                                },
                                Close: function () {
                                    $rootScope.IsLoading = false;
                                }
                            })
                        }
                    }, function () { })
                }
            });
        } else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Vehicle_Offer,
                data: { mID: $scope.NewTimeLineItem.ID, venID: $scope.NewTimeLineItem.VendorOfVehicleID, vehID: uid, isTractor: true },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        if (res != null && res.OfferTimeError != null && res.OfferTimeError != "") {
                            $rootScope.Message({ Msg: res.OfferTimeError, Type: Common.Message.Type.Alert })
                            _refreshScheduler(scheduler);
                        } else {
                            var msg = "Xác nhận chọn romooc?";
                            if (res != null && res.OfferTimeWarning != null && res.OfferTimeWarning != "")
                                msg = res.OfferTimeWarning + ", tiếp tục lưu romooc?";
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: msg,
                                Type: Common.Message.Type.Confirm,
                                Ok: function () {
                                    $rootScope.IsLoading = true;
                                    Common.Services.Call($http, {
                                        url: Common.Services.url.OPS,
                                        method: _OPSAppointment_COViewOnMapV4.URL.TimeLine_TOMaster_Vehicle_Update,
                                        data: { mID: $scope.NewTimeLineItem.ID, venID: $scope.NewTimeLineItem.VendorOfVehicleID, vehID: uid, isTractor: true },
                                        success: function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $scope.ChangeData = true;
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: 'Thành công!' });
                                                $scope.LoadNewTimeLineV2Data(false);
                                                $scope.NewTimeLineItem.HasChange = false;
                                                $scope.NewTimeLineItemTemp.RomoocID = uid;
                                                $scope.NewTimeLineItemTemp.RomoocNo = txt;
                                                $scope.NewTimeLineItem.RomoocID = uid;
                                                $scope.NewTimeLineItem.RomoocNo = txt;
                                                $scope.NewTimeLineItem.HasTimeChange = true;
                                                $scope.timeline_vehicle_map_win.close();
                                            }, function () {
                                                $rootScope.IsLoading = false;
                                            })
                                        }
                                    });
                                },
                                Close: function () {
                                    $rootScope.IsLoading = false;
                                }
                            })
                        }
                    }, function () { })
                }
            });
        }
    }

    $scope.NewTimeLineVehicleMap_Config = function ($event, win) {
        $event.preventDefault();

        $scope.NewTimeLineVehicleMapParam.Config = $scope.newtimeline_vehicle_map_config_Grid.dataSource.data();
        win.center().open();
        $timeout(function () {
            $scope.newtimeline_vehicle_map_config_Grid.refresh();
        }, 100)
    }

    $scope.newtimeline_vehicle_map_config_GridOptions = {
        dataSource: Common.DataSource.Local({ data: $scope.NewTimeLineVehicleMapParam.Config }),
        height: '100%', groupable: false, pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        columns: [
            { field: 'Label', title: 'Tên cột' },
            { field: 'hasShow', title: 'Hiện', width: '40px', template: '<input ng-click="NewTimeLineVehicleMap_Config_Check($event,dataItem)" type="checkbox" ng-model="dataItem.hasShow" />', headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' } },
            { field: 'Width', title: 'Kích thước', width: '320px', template: '<input type="nunber" class="k-textbox" style="width: 40px;float:left;" ng-model="dataItem.Width" /><input kendo-slider ng-model="dataItem.Width" k-tooltip="{ enabled: true }" k-max="280" k-min="0" style="width: 280px;" />' }
        ]
    }

    $scope.NewTimeLineVehicleMap_Config_Check = function ($event, item) {
        var noShow = $.grep($scope.NewTimeLineVehicleMapParam.Config, function (o) { return o.hasShow == true }).length;

        if (noShow > 3 && item.hasShow) {
            $event.preventDefault(); item.hasShow = false;
            $rootScope.Message({ Msg: 'Tối đa hiển thị 3 cột', Type: Common.Message.Type.Alert });
        } else if (noShow == 3) {
            $scope.NewTimeLineVehicleMapParam.Class = 'column3layout';
        }
        else if (noShow == 2) {
            $scope.NewTimeLineVehicleMapParam.Class = 'column2layout';
        }
        else if (noShow == 1 || noShow == 0) {
            $scope.NewTimeLineVehicleMapParam.Class = 'column1layout';
        }
    }

    $scope.newtimelinevehiclemapconfig_Drag_Options = {
        hint: function (e) { return $(e).clone().addClass('hint'); },
        dragend: function (e) {
            e.preventDefault();
            angular.forEach($('.config-table').find('.div-over'), function (td, i) {
                $(td).removeClass('div-over');
            })
        }
    }

    $scope.newtimelinevehiclemapconfig_Drop_Options = {
        dragenter: function (e) {
            $(e.dropTarget).find('.config-div').addClass('div-over');
        },
        dragleave: function (e) {
            $(e.dropTarget).find('.config-div').removeClass('div-over');
        },
        drop: function (e) {
            e.preventDefault();
            var idxDrop = $(e.dropTarget).find('div').data('index'),
                idxDrag = $(e.draggable.element).data('index'),
                totalItem = $scope.NewTimeLineVehicleMapParam.Config.length,
                itemDrop = null, itemDrag = null;
            for (var i = 0; i < totalItem; i++) {
                if ($scope.NewTimeLineVehicleMapParam.Config[i].SortOrder == idxDrop) {
                    itemDrop = $scope.NewTimeLineVehicleMapParam.Config[i];
                }
                if ($scope.NewTimeLineVehicleMapParam.Config[i].SortOrder == idxDrag) {
                    itemDrag = $scope.NewTimeLineVehicleMapParam.Config[i];
                }
            }
            if (Common.HasValue(itemDrag) && Common.HasValue(itemDrop)) {
                if (itemDrag.SortOrder != idxDrop) {
                    var val = itemDrag.SortOrder;
                    itemDrag.SortOrder = itemDrop.SortOrder;
                    itemDrop.SortOrder = val;
                    $scope.NewTimeLineVehicleMapParam.Config.sort(function (a, b) { return a.SortOrder > b.SortOrder });
                    $scope.newtimeline_vehicle_map_config_Grid.refresh();
                }
            }
        }
    }

    $scope.ToDateString = function (v) {
        return Common.Date.FromJsonDMYHM(v);
    }

    $scope.ToDateStringDMHM = function (v) {
        return Common.Date.FromJsonDDMM(v) + " " + Common.Date.FromJsonHM(v);
    }

    $scope.HasItemInList = function (v1, v2) {
        var flag = false;
        for (var i = 0; i < v1.length; i++) {
            flag = v2.indexOf(v1[i]) > -1;
            if (flag)
                break;
        }
        return flag;
    }

    $scope.ChangeVehicleType = 1;
    $scope.IsVehicleMapActived = false;
    $scope.VehicleMapRequestDate = new Date();
    $scope.TOMasterOnMapID = -1;

    $scope.NewTripChangeVehicle = function ($event, type, win) {
        $event.preventDefault();

        var objTO = $($event.target).data('item');
        if (Common.HasValue(objTO)) {
            $scope.TOMasterOnMapID = objTO.TOMasterID;
            win.center().open();
            $scope.ChangeVehicleType = type;
            $scope.IsVehicleMapActived = false;
            $scope.VehicleMapRequestDate = Common.Date.FromJson(objTO.TOETD);
            switch ($scope.ChangeVehicleType) {
                case 1:
                    $scope.vehMap_Grid.dataSource.read();
                    break;
                case 2:
                    $scope.romMap_Grid.dataSource.read();
                    break;
                default:
                    break;
            }
            $rootScope.IsLoading = false;
        }
    }

    $scope.vehMap_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.Tractor_List,
            readparam: function () {
                return {
                    requestDate: $scope.VehicleMapRequestDate
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        dataBound: function () {
            var grid = this;
            if ($scope.IsVehicleMapActived && $scope.ChangeVehicleType == 2) {
                angular.forEach(grid.items(), function (tr) {
                    if (!$scope.NewTimeLineDetail) $(tr).addClass('unselectable');
                });
            }
        },
        columns: [
            {
                field: 'Regno', width: 120, title: 'Số xe',
                template: '<span>#=Regno# </span>' + '<a class="k-button select" ng-click="VehicleOnMap_Select($event,dataItem,1,vehicle_map_win)"><span>LC</span></a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'MaxWeight', width: 70, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'RomoocNo', width: 120, title: 'Romooc hiện tại', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfTractorName', width: 80, title: 'T/trạng', filterable: false, sortable: false },
            { field: 'LocationName', width: 150, title: 'Điểm hiện tại', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.romMap_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV4.URL.Romooc_List,
            readparam: function () {
                return {
                    requestDate: $scope.VehicleMapRequestDate
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, autoBind: false,
        dataBound: function () {
            var grid = this;
            if ($scope.IsVehicleMapActived && $scope.ChangeVehicleType == 2) {
                angular.forEach(grid.items(), function (tr) {
                    if (!$scope.NewTimeLineDetail) $(tr).addClass('unselectable');
                });
            }
        },
        columns: [
            {
                field: 'Regno', width: 120, title: 'Romooc',
                template: '<span>#=Regno# </span>' + '<a class="k-button select" ng-click="VehicleOnMap_Select($event,dataItem,2,vehicle_map_win)"><span>LC</span></a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'MaxWeight', width: 70, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'GroupOfRomoocName', width: 80, title: 'Loại romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfRomoocName', width: 80, title: 'T/trạng', filterable: false, sortable: false },
            { field: 'LocationName', width: 150, title: 'Điểm hiện tại', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.VehicleOnMap_Select = function ($event, item, type, win) {
        $event.preventDefault();

        if ($scope.IsShowNewTimeLineV2 && $scope.NewTimeLineDetail) {
            if ($scope.NewTimeLineItem.ID > 0) {
                switch (type) {
                    case 1:
                        $rootScope.Message({
                            Msg: "Xác nhận lưu?",
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_COViewOnMapV4.URL.CO2View_Master_Update_TimeLine,
                                    data: { mID: $scope.NewTimeLineItem.ID, vehicleID: item.ID, isTractor: true, ETD: $scope.NewTimeLineItem.ETD, ETA: $scope.NewTimeLineItem.ETA },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $scope.ChangeData = true;
                                            $rootScope.IsLoading = false;
                                            $rootScope.Message({ Msg: 'Thành công!' });
                                            $scope.LoadNewTimeLineV2Data(false);
                                            $scope.NewTimeLineItem.HasChange = false;
                                            $scope.NewTimeLineItemTemp.VehicleID = item.ID;
                                            $scope.NewTimeLineItemTemp.VehicleNo = item.Regno;
                                            $scope.NewTimeLineItem.VehicleID = item.ID;
                                            $scope.NewTimeLineItem.VehicleNo = item.Regno;
                                            win.close();
                                        }, function () {
                                            $rootScope.IsLoading = false;
                                        })
                                    }
                                })
                            }
                        })
                        break;
                    case 2:
                        $rootScope.Message({
                            Msg: "Xác nhận lưu?",
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_COViewOnMapV4.URL.CO2View_Master_Update_TimeLine,
                                    data: { mID: $scope.NewTimeLineItem.ID, vehicleID: item.ID, isTractor: false, ETD: $scope.NewTimeLineItem.ETD, ETA: $scope.NewTimeLineItem.ETA },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $scope.ChangeData = true;
                                            $rootScope.IsLoading = false;
                                            $rootScope.Message({ Msg: 'Thành công!' });
                                            $scope.LoadNewTimeLineV2Data(false);
                                            $scope.NewTimeLineItem.HasChange = false;
                                            $scope.NewTimeLineItemTemp.RomoocID = item.ID;
                                            $scope.NewTimeLineItemTemp.RomoocNo = item.Regno;
                                            $scope.NewTimeLineItem.RomoocID = item.ID;
                                            $scope.NewTimeLineItem.RomoocNo = item.Regno;
                                            win.close();
                                        }, function () {
                                            $rootScope.IsLoading = false;
                                        })
                                    }
                                })
                            }
                        })
                        break;
                    default: break;
                }
            }
            else {
                switch (type) {
                    case 1:
                        if ($scope.NewTimeLineResourceType == 1) {
                            $scope.NewTimeLineItem.HasChange = true;
                            $scope.NewTimeLineItem.VehicleID = item.ID;
                            $scope.NewTimeLineItem.VehicleNo = item.Regno;
                            win.close();
                        } else if ($scope.NewTimeLineResourceType == 2) {
                            if (item.RomoocID > 0) {
                                $rootScope.Message({
                                    Msg: "Bạn muốn sử dụng romooc theo xe?",
                                    Type: Common.Message.Type.Confirm,
                                    Ok: function () {
                                        $scope.NewTimeLineItem.HasChange = true;
                                        $scope.NewTimeLineItem.VehicleID = item.ID;
                                        $scope.NewTimeLineItem.VehicleNo = item.Regno;
                                        $scope.NewTimeLineItem.RomoocID = item.RomoocID;
                                        $scope.NewTimeLineItem.RomoocNo = item.RomoocNo;
                                        win.close();
                                    },
                                    Close: function () {
                                        $scope.NewTimeLineItem.HasChange = true;
                                        $scope.NewTimeLineItem.VehicleID = item.ID;
                                        $scope.NewTimeLineItem.VehicleNo = item.Regno;
                                        win.close();
                                    }
                                })
                            } else {
                                $scope.NewTimeLineItem.HasChange = true;
                                $scope.NewTimeLineItem.VehicleID = item.ID;
                                $scope.NewTimeLineItem.VehicleNo = item.Regno;
                                win.close();
                            }
                        }
                        break;
                    case 2:
                        $scope.NewTimeLineItem.HasChange = true;
                        $scope.NewTimeLineItem.RomoocID = item.ID;
                        $scope.NewTimeLineItem.RomoocNo = item.Regno;
                        win.close();
                        break;
                    default: break;
                }
            }
        } else if (!$scope.IsShowNewTimeLineV2) {
            switch (type) {
                case 1:
                    $rootScope.Message({
                        Msg: "Xác nhận đổi đầu kéo!",
                        Type: Common.Message.Type.Confirm,
                        Ok: function () {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.CO2View_Master_ChangeVehicle,
                                data: { mID: $scope.TOMasterOnMapID, vehID: item.ID, type: 1 },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        $scope.new_trip_Grid.dataSource.read();
                                        $rootScope.IsLoading = false;
                                        win.close();
                                    })
                                }
                            })
                        }
                    })
                    break;
                case 2:
                    $rootScope.Message({
                        Msg: "Xác nhận đổi romooc!",
                        Type: Common.Message.Type.Confirm,
                        Ok: function () {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV4.URL.CO2View_Master_ChangeVehicle,
                                data: { mID: $scope.TOMasterOnMapID, vehID: item.ID, type: 2 },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        $scope.new_trip_Grid.dataSource.read();
                                        $rootScope.IsLoading = false;
                                        win.close();
                                    })
                                }
                            })
                        }
                    })
                    break;
                default:
                    win.close();
                    break;
            }
        }
    }

    //#region Action
    $scope.Close_Click = function ($event, win, code) {
        $event.preventDefault();

        try {
            $scope.On_Close(code, $event);
            switch (code) {
                case 'ORD':
                    break;
                case 'TO':
                    $scope.TripActived = false;
                    $scope.IsShowTrip = false;
                    $scope.IsShowTimeLineTrip = false;
                    break;
                case 'NewTO':
                    $scope.IsShowNewTrip = false;
                    break;
                case 'VEHICLE':
                    break;
                case 'MAP':
                    break;
                case 'VehicleMAP':
                    break;
                case 'TimeLine':
                    $scope.TripActived = false;
                    $scope.IsShowTimeLine = false;
                    $scope.IsShowTimeLineTrip = false;
                    break;
                case 'NewTimeLine':
                    $scope.IsShowNewTimeLine = false;
                    break;
                case 'NewTimeLineV2':
                    $scope.IsShowNewTimeLineV2 = false;
                    break;
                case 'NewTimeLineDetail':
                    $scope.NewTimeLineDetail = false;
                    break;
                case 'TOTimeLine':
                    $scope.TripActived = false;
                    $scope.IsShowTimeLineTrip = false;
                    break;
                case 'TODetail':
                    $scope.TripDetail = false;
                    break;
                case 'NewTODetail':
                    $scope.NewTripDetail = false;
                    break;
                case 'TimeLineTODetail':
                    $scope.TimeLineTripDetail = false;
                    break;
                case 'TimeLineVehicleSelect':
                    $scope.NewTimeLineVehicleData = $.extend(true, [], $scope.NewTimeLineVehicleDataTemp);
                    $scope.NewTimeLineDragDropVehicleData = $.extend(true, [], $scope.NewTimeLineDragDropVehicleDataTemp);
                    break;
                case 'TimeLineInfoDragDrop':
                    $scope.IsShowNewTimeLineDragDropInfo = false;
                    if ($scope.NewScheduleDragDropInfoTOMasterID < 1) {
                        angular.forEach($scope.new_timeline_dragdrop_info.dataSource.data(), function (o, i) {
                            if ((o.TypeOfGroupID == 4 || (o.TypeOfGroupID == 1 && o.Code == 'New')) && Common.HasValue($scope.NewTimeLineV2_Change_Time_DataTemp[o.id + "_" + o.field])) {
                                var v = $scope.NewTimeLineV2_Change_Time_DataTemp[o.id + "_" + o.field];
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
            case 'ORD':
                break;
            case 'TO':
                $scope.TripActived = false;
                $scope.IsShowTrip = false;
                $scope.IsShowTimeLineTrip = false;
                break;
            case 'NewTO':
                $scope.notification.hide();
                $scope.IsShowNewTrip = false;
                break;
            case 'VEHICLE':
                break;
            case 'MAP':
                break;
            case 'VehicleMAP':
                break;
            case 'TimeLine':
                $scope.TripActived = false;
                $scope.IsShowTimeLine = false;
                $scope.IsShowTimeLineTrip = false;
                break;
            case 'NewTimeLine':
                $scope.IsShowNewTimeLine = false;
                break;
            case 'NewTimeLineV2':
                $scope.IsShowNewTimeLineV2 = false;
                break;
            case 'NewTimeLineDetail':
                $scope.NewTimeLineDetail = false;
            case 'TOTimeLine':
                $scope.TripActived = false;
                $scope.IsShowTimeLineTrip = false;
                break;
            case 'TODetail':
                $scope.TripDetail = false;
                break;
            case 'NewTODetail':
                $scope.NewTripDetail = false;
                break;
            case 'TimeLineTODetail':
                $scope.TimeLineTripDetail = false;
                break;
            case 'TimeLineVehicleSelect':
                $scope.NewTimeLineVehicleData = $.extend(true, [], $scope.NewTimeLineVehicleDataTemp);
                $scope.NewTimeLineDragDropVehicleData = $.extend(true, [], $scope.NewTimeLineDragDropVehicleDataTemp);
                break;
            case 'TimeLineInfoDragDrop':
                $scope.IsShowNewTimeLineDragDropInfo = false;
                //Esc press
                if (event.userTriggered) {
                    if ($scope.NewScheduleDragDropInfoTOMasterID < 1) {
                        angular.forEach($scope.new_timeline_dragdrop_info.dataSource.data(), function (o, i) {
                            if ((o.TypeOfGroupID == 4 || (o.TypeOfGroupID == 1 && o.Code == 'NEW')) && Common.HasValue($scope.NewTimeLineV2_Change_Time_DataTemp[o.id + "_" + o.field])) {
                                var v = $scope.NewTimeLineV2_Change_Time_DataTemp[o.id + "_" + o.field];
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
            case 'TimeLineVehicleMAP':
                break;
        }
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.OPSAppointmentCO,
            event: $event, grid: grid,
            current: $state.current,
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
        $scope.timelineV2Splitter.resize();
    }
    //#endregion

    $rootScope.IsLoading = false;
    $scope.InitComplete = true;

    $timeout(function () {
        angular.element('#2view').resize();
        $scope.timelineV2Splitter.resize();
    }, 1000)
    //#endregion
}]);
