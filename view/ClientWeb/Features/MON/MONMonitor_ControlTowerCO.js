/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _MONMonitor_ControlTowerCO = {
    URL: {
        TractorOwner_List: "TractorOwner_List",
        RomoocOwner_List: "RomoocOwner_List",
        TractorOwner_UpdateFromGPS: "TractorOwner_UpdateFromGPS",
        MONCO_Continue: "MONControlTowerCO_MONCO_Continue",
        MONCO_End: "MONControlTowerCO_MONCO_End",

        SRVehicleComplete: "DIMonitor_MsComplete",
        SRVehicleCompleteDN: "DIMonitor_MsCompleteDN",
        SRVehicleRevert: "DIMonitor_MsRevert",
        SRVehicleRevertDN: "DIMonitor_MsRevertDN",
        SRVehicleTimeVehicleLocation: "DIMonitor_MsVehicleLocation",
        SRMasterDNList: "DIMonitorDN_MasterDNList",
        SRMasterDNGet: "DIMonitor_VehicleTimeGet",
        MapLocationGetStatus: "DIMonitor_MasterStatusGet",
        MapLocationChangeStatus: "DIMonitor_MasterStatusUpdate",
        MSTroubleList: "DIMonitor_MsTroubleList",
        MSTroubleUpdate: "DIMonitor_MsTroubleUpdate",
        MSTroubleUpdateAll: "DIMonitor_MsTroubleUpdateAll",
        MSTroubleDelete: "DIMonitor_MsTroubleDelete",
        MSTroubleGet: "DIMonitor_MsTroubleGet",
        MSRoutingList: "DIMonitor_MsRoutingList",
        TroubleList: "DIMonitor_CATTroubleList",
        TroubleSaveList: "DIMonitorTrouble_SaveList",
        SRWinToUpdate: "DIMonitor_MsUpdate",
        SRWinToDetailList: "DIMonitor_MsDetailList",
        SRWinToDITOList: "DIMonitor_MsDITOList",
        SRWinToDITOUpdate: "DIMonitor_MsDITOUpdate",
        SRWinToDITOLocationList: "DIMonitor_MsDITOLocationList",
        SRWinToDITOLocationUpdate: "DIMonitor_MsDITOLocationUpdate",
        DriverList: "Monitor_DriverList",
        VendorList: "Monitor_VendorList",
        TruckList: "Monitor_TruckList",
        LocationList: "Monitor_LocationList",
        SRWinToGroupList: "DIMonitor_MsGroupProductList",
        SRWinToGroup_Change: "DIMonitor_MsGroupProductChange",
        SRWinToGroup_Merge: "DIMonitor_MsGroupProductMerge",
        GroupPOD_Change: "DIMonitor_GroupPODChange",
        Vehicle_GetOPS: "DIMonitorMaster_GetByVehicle",
        SRVehicleNoDNList: "DIMonitor_NoDNList",
        SRVehicleNoDNOrderList: "DIMonitor_NoDNOrderList",
        TroubleNotin_List: "DIMonitor_TroubleNotinList",
        GOPReturn_List: "DIMonitorMaster_GOPReturnList",
        GOPReturn_Save: "DIMonitorMaster_GOPReturnSave",
        CUSGOP_List: "DIMonitorMaster_CUSGOPList",


    },
    Data: {
        _dataVehicle: null,
        _dataHasDN: [],
        ListLocation: [],
        _mapMarkers: [],
        _mapMarkerTruck: null,
        _dataTruck: [],
        _dataLocation: [],
        _locationID: 0,
        _dataMsRouting: [],
        _dataVendor: [],
        _dataVehicleByVEN: [],
        _dataVehicleHome: [],
        _trouble1stStatus: 0,

        _scheduleContainer: []
    },
    Map: {
        Map: null,
        Marker: [],
        MapInfo: null
    },

    ScheduleContainer_GetItem: function (value) {
        return _MONMonitor_ControlTowerCO.Data._scheduleContainer[value + ''];
    },

    allowDrop: function (ev) {
        ev.preventDefault();
    },
    drag: function (ev) {
        ev.dataTransfer.setData("text", ev.target.id);
    },

    drop: function (ev) {
        ev.preventDefault();
        var data = ev.dataTransfer.getData("text");
        ev.target.appendChild(document.getElementById(data));
    }
}

angular.module('myapp').controller('MONMonitor_ControlTowerCtrlCO', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', '$interval', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile, $interval) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('MONMonitor_ControlTowerCtrlCO');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();


    //#region Common

    $scope.GetMonday = function (d) {
        d = new Date(d);
        var day = d.getDay(),
            diff = d.getDate() - day + (day == 0 ? -6 : 1);
        return new Date(d.setDate(diff));
    };

    $scope.GetLastWeek = function (dfrom, dto) {
        var last = dfrom;
        if (dfrom.getDay() != 0) {
            last = $scope.GetMonday(dfrom);
        }
        do {
            last = last.addDays(7);
        }
        while (last < dto)
        return last;
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

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

    $scope.TODetail_ReloadAllGrid = function (ignore) {
        $scope.ct_order_grid.dataSource.read();
        $scope.Trouble_Grid.dataSource.read();
        $scope.Station_Grid.dataSource.read();
        $scope.Location_Grid.dataSource.read();
        $scope.COContainer_Grid.dataSource.read();
        $scope.TOContainer_Grid.dataSource.read();
        if (ignore != 7)
            $scope.ShedularLoadData();
    };

    $scope.DayRelationCheck = function (from, to) {
        var SameWeek = false;
        var SameMonth = false;
        var DayCount = 0;
        from = new Date(from);
        to = new Date(to);

        DayCount = (to - from) / (24 * 60 * 60 * 1000);
        if (DayCount < 7) {
            if ($scope.GetMonday(from).getDate() == $scope.GetMonday(to).getDate()) {
                SameWeek = true;
            }
        }
        if (DayCount < 31 && from.getMonth() == to.getMonth()) {
            SameMonth = true;
        }
        return {
            SameWeek: SameWeek,
            SameMonth: SameMonth,
            DayCount: DayCount
        }
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
    //#endregion

    //#region location

    var _LocationData = {
        Data: {
            _lstVehicle: null,
            _lstRommoc: null,
            _lstVehicleGPS: null,
            _lstVehicleMap: null,
            _lstRommocMap: null,
            _lstRealRoute: null,
        }
    }

    $scope.ShowTractor = false;
    $scope.ShowRomooc = false;

    $scope.AutoRun = {
        Enabled: false,
        Interval: 10,
        IsFitBound: false,
    };

    $scope.AutoRun_Start = function () {
        if (_LocationData.Data._lstVehicleGPS == null)
            throw ('lst gps null');
        if ($scope.AutoRun.Enabled == true && $scope.AutoRun.Interval > 0 && _LocationData.Data._lstVehicleGPS.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_ControlTowerCO.URL.TractorOwner_UpdateFromGPS,
                data: { lst: _LocationData.Data._lstVehicleGPS, dt: new Date() },
                success: function (res) {

                    _LocationData.Data._lstRommoc = null;
                    _LocationData.Data._lstVehicle = null;

                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_ControlTowerCO.URL.TractorOwner_List,
                        data: { dt: new Date() },
                        success: function (res) {
                            _LocationData.Data._lstVehicle = [];
                            angular.forEach(res, function (v, i) {
                                _LocationData.Data._lstVehicle[v.ID] = v;
                            });
                            $scope.AutoRun_StartReload();
                        }
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_ControlTowerCO.URL.RomoocOwner_List,
                        data: { dt: new Date() },
                        success: function (res) {
                            _LocationData.Data._lstRommoc = [];
                            angular.forEach(res, function (v, i) {
                                v.IsDraw = false;
                                _LocationData.Data._lstRommoc[v.ID] = v;
                            });
                            $scope.AutoRun_StartReload();
                        }
                    });

                }
            });

            _LocationData.Data._lstRealRoute = null;
            openMapV2.ClearVector("VectorRealRoute");
            if ($scope.masterID > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTowerCO_OrderLogList",
                    data: { masterID: $scope.masterID },
                    success: function (res) {

                        var vehicleCode = res.GPSCode;
                        var dtfrom = res.ATD;
                        var dtto = res.ATA;
                        if (Common.HasValue(dtfrom)) {
                            var isNewDate = false;
                            if (!Common.HasValue(dtto)) {
                                isNewDate = true;
                                dtto = new Date();
                            }
                            Common.Services.Call($http, {
                                url: Common.Services.url.MON,
                                method: "Extend_VehiclePosition_Get",
                                data: { vehicleCode: vehicleCode, dtfrom: dtfrom, dtto: dtto },
                                success: function (res) {
                                    _LocationData.Data._lstRealRoute = res;
                                    if (MapNo == 1) {
                                        $scope.RealRoute_Draw();
                                    }
                                }
                            });
                        }

                    }
                })
            }
        }
    };

    $scope.AutoRun_StartReload = function () {
        var reload = false;
        if (_LocationData.Data._lstVehicle != null && _LocationData.Data._lstRommoc != null) {
            if (MapNo == 1) {
                $scope.Owner_Draw();
            }
            reload = true;
        }

        if (reload) {
            $timeout(function () {
                $scope.AutoRun_Start();
            }, $scope.AutoRun.Interval * 1000);
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_ControlTowerCO.URL.TractorOwner_List,
        data: { dt: new Date() },
        success: function (res) {
            _LocationData.Data._lstVehicle = [];
            _LocationData.Data._lstVehicleGPS = [];
            angular.forEach(res, function (v, i) {
                _LocationData.Data._lstVehicle[v.ID] = v;
                _LocationData.Data._lstVehicleGPS.push({ 'ID': v.ID, 'GPSCode': v.GPSCode });
            });
            $scope.Owner_Init();
        }
    });

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_ControlTowerCO.URL.RomoocOwner_List,
        data: { dt: new Date() },
        success: function (res) {
            _LocationData.Data._lstRommoc = [];
            angular.forEach(res, function (v, i) {
                v.IsDraw = false;
                _LocationData.Data._lstRommoc[v.ID] = v;
            });
            $scope.Owner_Init();
        }
    });

    $scope.Owner_Init = function () {
        if (_LocationData.Data._lstVehicle != null && _LocationData.Data._lstRommoc != null) {
            $scope.Owner_Draw();
            if (_LocationData.Data._lstVehicle.length > 0)
                $scope.AutoRun_Start();
        }
    };

    $scope.Owner_Draw = function () {
        if (_LocationData.Data._lstVehicle == null || _LocationData.Data._lstRommoc == null)
            throw ('lst data null');

        openMapV2.ClearVector('VectorVehicle');
        _LocationData.Data._lstVehicleMap = [];
        if (_LocationData.Data._lstVehicle.length > 0) {
            angular.forEach(_LocationData.Data._lstVehicle, function (v, i) {
                if (Common.HasValue(v)) {
                    if (v.Lat > 0 && v.Lng > 0) {
                        var icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tractor.png', 1);
                        if (v.RomoocID > 0) {
                            icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_40.png', 1);
                            var romooc = _LocationData.Data._lstRommoc[v.RomoocID];
                            if (Common.HasValue(romooc)) {
                                //
                                if (romooc.MaxWeight < v.MaxWeight)
                                    v.MaxWeight = romooc.MaxWeight;
                                v.ListOrder = romooc.ListOrder;
                                //
                                romooc.IsDraw = true;
                                if (romooc.HasContainer) {
                                    if (romooc.IsLaden) {
                                        if (romooc.NoOfDelivery == 1)
                                            icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_co_20f.png', 1);
                                        else if (romooc.NoOfDelivery == 2)
                                            icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_co_40f.png', 1);
                                    }
                                    else {
                                        if (romooc.NoOfDelivery == 1)
                                            icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_co_20e.png', 1);
                                        else if (romooc.NoOfDelivery == 2)
                                            icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_co_40e.png', 1);
                                    }
                                }
                                else {
                                    if (romooc.NoOfDelivery == 1)
                                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_20.png', 1);
                                    else if (romooc.NoOfDelivery == 2)
                                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_40.png', 1);
                                }

                            }
                            //
                            if ($scope.ShowRomooc || $scope.ShowTractor) {
                                _LocationData.Data._lstVehicleMap.push(openMapV2.NewMarker(v.Lat, v.Lng, 'ID' + v.ID, v.RegNo, icon, v, 'VectorVehicle'));
                            }
                        }
                        else if ($scope.ShowTractor)
                            _LocationData.Data._lstVehicleMap.push(openMapV2.NewMarker(v.Lat, v.Lng, 'ID' + v.ID, v.RegNo, icon, v, 'VectorVehicle'));
                    }
                }
            });
        }

        _LocationData.Data._lstRommocMap = [];
        if (_LocationData.Data._lstRommoc.length > 0 && $scope.ShowRomooc) {
            angular.forEach(_LocationData.Data._lstRommoc, function (v, i) {
                if (Common.HasValue(v)) {
                    if (v.IsDraw != true && v.Lat > 0 && v.Lng > 0) {
                        var icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_romooc_40.png', 1);

                        var romooc = _LocationData.Data._lstRommoc[v.RomoocID];
                        if (Common.HasValue(romooc)) {
                            if (romooc.HasContainer) {
                                if (romooc.IsLaden) {
                                    if (romooc.NoOfDelivery == 1)
                                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_ro_co_20f.png', 1);
                                    else if (romooc.NoOfDelivery == 2)
                                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_ro_co_40f.png', 1);
                                }
                                else {
                                    if (romooc.NoOfDelivery == 1)
                                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_ro_co_20e.png', 1);
                                    else if (romooc.NoOfDelivery == 2)
                                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_ro_co_40e.png', 1);
                                }
                            }
                            else {
                                if (romooc.NoOfDelivery == 1)
                                    icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_romooc_20.png', 1);
                                else if (romooc.NoOfDelivery == 2)
                                    icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_romooc_40.png', 1);
                            }
                        }
                        _LocationData.Data._lstRommocMap.push(openMapV2.NewMarker(v.Lat, v.Lng, 'ID' + v.ID, v.RegNo, icon, v, 'VectorVehicle'));
                    }
                }
            });
        }


        if ($scope.AutoRun.IsFitBound == true) {
            $scope.AutoRun.IsFitBound = false;
            openMapV2.FitBound('VectorVehicle');
        }
    };

    $scope.RealRoute_Draw = function () {

        var res = _LocationData.Data._lstRealRoute;
        if (Common.HasValue(res)) {
            for (var i = 0; i < res.length - 1; i++) {
                var l1 = res[i];
                var l2 = res[i + 1];

                var style = openMapV2.NewStyle.Line(4, 'rgba(150,50,50, 0.7)', [15, 10], "", '#fff');
                if (Common.HasValue(l1) && Common.HasValue(l2) && l1.Lat > 0 && l1.Lng > 0 && l2.Lat > 0 && l2.Lng > 0) {
                    openMapV2.NewPolyLine([openMapV2.NewPoint(l1.Lat, l1.Lng), openMapV2.NewPoint(l2.Lat, l2.Lng)], 1, "", style, {}, "VectorRealRoute")

                }
            }
        }
    };

    //#endregion location

    //#region Model
    $scope.ShowOrderStatusPane = false;
    $scope.Search = {
        DateFrom: Common.Date.AddDay(new Date(), -2),
        DateTo: Common.Date.AddDay(new Date(), 2),
        HourFrom: Common.Date.AddDay(new Date(), -2),
        HourTo: Common.Date.AddHour(new Date(), 8),
        RouteInDay: 3,
    };
    $scope.masterID = 0;
    $scope.Show_BtnWinTO_Update = true;
    $scope.CurrentMaster = {
        VehicleID: -1,
    };
    $scope._dataVendor = [];
    $scope._dataTruck = [];
    $scope._mapMarkers = [];
    $scope.Marker = [];
    $scope.Show_Gantt_Info = false;
    $scope.VehicleLogItem = {};
    $scope.Color_Series = ["#3f51b5", "#03a9f4", "#41aa45", "#ff9800"];
    $scope.Item = {};
    $scope.MONCT_Filter = {
        DateFrom: new Date().addDays(-30),
        DateTo: new Date().addDays(10),
        Going: true,
        Complete: true,
        IsLoadAll: false,
        IsRecieved: true
    };
    $scope.MONCT_OPSSummary = [{}];
    $scope.FilterLoading = false;
    $scope.Show_Filter_Status = false;
    $scope.ShowTimelineActual = false;
    $scope.Filter_Status_Position = {
        Left: 0,
        Top: 0
    }
    $scope.Filter_Html = '<div ng-click="OrderCheckbox(1)" ng-class="{filteractived:MONCT_Filter.Going}" class="filter-status-row"><div class="filter-status-cell"><div class="dot orange"></div></div><div class="filter-status-cell" style="width:80%">Xe đang chạy</div></div>' +
                         '<div ng-click="OrderCheckbox(2)" ng-class="{filteractived:MONCT_Filter.Complete}" class="filter-status-row"><div class="filter-status-cell"><div class="dot green"></div></div><div class="filter-status-cell" style="width:80%"> Xe đã hoàn thành</div> </div>';

    $scope._dataVehicleByVEN = [];
    $scope._dataVehicleHome = [];
    $scope.LocalObj = {};
    $scope.FilterVehicle = {
        IsVehicleFree: true,
        IsVehiclePlan: true,
        IsVehicleGoing: true,
        lstVendorID: [],
        Time: 4,
        ShowDate: false
    };
    $scope.NotifyData = [];
    $scope.Show_Chart_Menu = false;
    $scope.IsFullMap = false;
    $scope.ChartMenuSelect = 1;
    $scope.FitlerWidth = false;
    $scope.ShowFilterSum = 0;
    $scope.LoadingCount = 0;
    $scope.LstTimelineResource = [];
    $scope.DataCUSProduct = [];
    $scope.DataDITOGroupProduct = [];
    $scope.FilterXe = {
        Run: true,
        Free: true
    }

    $scope.Init_CK = function () {

        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = Common.Date.AddDay(new Date(), -4);
            $scope.Search.DateTo = Common.Date.AddDay(new Date(), 2);
            $scope.Search.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Search.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Search.RouteInDay = 3;
        }
        else {
            $scope.Search.DateFrom = Common.Date.AddDay(new Date(), -4);
            $scope.Search.DateTo = Common.Date.AddDay(new Date(), 2);
            $scope.Search.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Search.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Search.RouteInDay = 3;
        }
    }

    var LoadingStep = 20;
    var isChangePlan = false;
    //$scope.Init_CK();

    //#endregion

    //#region Options
    $scope.orien = "vertical";
    $scope.MainSplitterClass = "ver-splitter";
    $scope.ButtonViewClass = "view2";
    if (Common.HasValue($state.params.orien)) {
        $scope.orien = "horizontal";
        $scope.MainSplitterClass = "hor-splitter";
        $scope.ButtonViewClass = "view3";
    }
    $scope.MONDI_SpitterOptions = {
        orientation: $scope.orien,
        panes: [
                { collapsible: true, resizable: true, size: '40%', scrollable: false },
                { collapsible: true, resizable: true, size: '60%', collapsed: false }
        ],
        resize: function (e) {
            if (Common.HasValue(openMapV2))
                openMapV2.Resize();
            $timeout(function () {
                $scope.ct_vehicle_scheduler.refresh();
            }, 100)
        }
    };

    $scope.ct_tabstripOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        },
        select: function (e) {
            $scope.ct_OrderSplitter.collapse(".k-pane:last");
            $scope.ShowOrderStatusPane = false;
        },
        show: function (e) {
            $timeout(function () {
                $scope.ct_order_grid.refresh();
            }, 100)
        }
    };

    $scope.toolTipOptions = {
        filter: ".ct-warning,.img-warning",
        position: "top",
        content: function (e) {
            if ($(e.target).attr('class') == '.ct-warning') {
                var grid = e.target.closest(".k-grid").getKendoGrid();
                var dataItem = grid.dataItem(e.target.closest("tr"));
                return "Chuyến có " + dataItem.WarningCount + " chi phí phát sinh";
            }
            else {
                return $(e.target).data('value');
            }
        },
        show: function (e) {
            $(this.popup.element[0]).find('.k-tooltip-content')[0].style.color = "red"
            $(this.popup.element[0]).find('.k-callout')[0].style.background = "none";
        }
    }

    $scope.toolTipTimelineOptions = {
        filter: ".cus-task-actual , .schedule-dot",
        position: "top",
        content: function (e) {
            if (e.target.attr('class') == 'cus-task-actual')
                return "Chuyến " + $(e.target.find('.tooltip-text')[0]).text();
            else if (e.target.attr('class') == 'schedule-dot') {
                return $(e.target.find('span')[0]).text();
            }
            return "";

        },
        show: function (e) {
            $(this.popup.element[0]).find('.k-tooltip-content')[0].style.color = "red"
            $(this.popup.element[0]).find('.k-callout')[0].style.background = "none";
        }
    }

    $scope.TODetailMaxMap_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_ControlTowerCO.URL.SRWinToDetailList,
            model: { id: 'ID', fields: { ID: { type: 'number' } } },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false, editable: true,
        columns: [
            {
                title: 'STT',
                field: 'STT', width: "50px",
            },
            {
                title: '{{RS.ORDOrder.Code}}',
                field: 'OrderCode', width: "100px",
            },
            {
                title: '{{RS.ORDGroupProduct.SOCode}}',
                field: 'SOCode', width: "80px",
            },
            {
                title: '{{RS.ORDGroupProduct.DNCode}}',
                field: 'DNCode', width: "80px",
            },
            {
                title: '{{RS.CUSGroupOfProduct.GroupName}}',
                field: 'GroupProductName', width: "150px",
            },
            {
                field: 'TonTranfer', width: 75,
                title: '{{RS.TonTranfer}}',
                //template: '#=WinToDetailTemplate(1, "TonTranfer", TonTranfer, UpdateTypeID) #'
            },
            {
                field: 'CBMTranfer', width: 75,
                title: '{{RS.CBMTranfer}}',
                //template: '#=WinToDetailTemplate(2, "CBMTranfer", CBMTranfer, UpdateTypeID) #'
            },
            {
                field: 'QuantityTranfer', width: 75,
                title: '{{RS.QuantityTranfer}}',
                //: '#=WinToDetailTemplate(3, "QuantityTranfer", QuantityTranfer, UpdateTypeID) #'
            },
            {
                title: '{{RS.CUSLocation.LocationName}}',
                field: 'LocationToName', width: "125px",
            },
            {
                title: '{{RS.CATProvince.ProvinceName}}',
                field: 'LocationToProvince', width: "125px",
            },
            {
                title: '{{RS.CATDistrict.DistrictName}}',
                field: 'LocationToDistrict', width: "125px",
            },
            {
                title: '{{RS.CATLocation.Address}}',
                field: 'LocationToAddress', width: "200px",
            },
            { title: ' ', filterable: false, sortable: false }
        ], dataBound: function (e) {
            Common.Log("me.WinToGridDetailDataBound missing");
        },
        reorderable: true
    };

    $scope.filterWindowOptions = {
        title: "Tìm theo cung đường",
        position: { top: 120, left: "20%" },
        actions: ["Minimize", "close"],
        height: 200, width: 320,
    };

    $scope.Trouble_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TroubleNotinList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    COTOID: { type: 'string', editable: false },
                    Cost: { type: 'number', defaultValue: 0 },
                    IsChoose: { type: 'bool' },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        edit: function (e) {
            e.model.COTOMasterID = $scope.masterID;
        },
        //toolbar: kendo.template($('#win-trouble-notin-grid-toolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Trouble_NotinGrid,Trouble_NotinGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Trouble_NotinGrid,Trouble_NotinGridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
           {
               field: 'Name', width: 200, title: 'Nhóm',
           },
           { field: 'CostValue', width: 130, title: '{{RS.CATCost.CostName}}' },
           { title: ' ', filterable: false, sortable: false }
        ],

    }

    $scope.numPrice_Options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.MaximumView = function (e) {
        e.preventDefault();
        $('#2view').kendoWindow({
            title: false,
            close: function (e) {
                var win = this;
                $timeout(function () {
                    $('#2view-container').append(win.element);
                    $('#2view').resize();
                    win.destroy();
                    $scope.IsFullScreen = false;
                }, 1)
            }
        });
        $('#2view').data('kendoWindow').maximize();
        $scope.IsFullScreen = true;
    }

    $scope.MinimumView = function (e) {
        e.preventDefault();

        var win = $('#2view').data('kendoWindow');
        $('#2view-container').append(win.element);
        $('#2view').resize();
        win.destroy();
        $scope.MONDI_Splitter.expand('#pane2');
        $scope.IsFullScreen = false;
    }
    //#endregion

    //#region TODtail

    //#region Change Depot

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
            openMapV2.Active(MainMap);
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

    //#region ORD Container Detail

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
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: 'inline',
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
            $rootScope.Loading.Change("Thông tin đơn hàng...", $rootScope.Loading.Progress + LoadingStep);
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

    //#region COTOContainer

    var dataRow = null;
    $scope.TOContainerID = 0;
    $scope.LocationDepotName = "";
    $scope.DateComeEstimate = null;
    $scope.IsAvailable = false;

    $scope.SLMenuOptions = function (sort) {
        //var dir = 'bottom';
        //if (sort > 7)
        dir = 'top';
        return {
            openOnClick: true,
            direction: dir,
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
                    STT: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true,
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
                            return $('#to-container-tpl').html();
                        }
                        else
                            return "";
                    }
                },
                //template: kendo.template($('#to-container-tpl').html()),
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
            $rootScope.Loading.Change("Thông tin chặng...", $rootScope.Loading.Progress + LoadingStep);
        }
    };

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
        $scope.ContLocaitonType = type;
        dataRow = $scope.TOContainer_Grid.dataItem(e.target.closest('tr'));
        $scope.DateComeEstimate = dataRow.DateComeEstimate;
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
            $scope.COTODateReturnDTPicker.bind("change", function () {
                $scope.COTODateShip = $scope.COTODateReturn.addDays(1 / 48);
            });
            //
            if (Common.HasValue($scope.ItemSave.FirstReason))
                $scope.ReasonChangeID = $scope.ItemSave.FirstReason.ValueInt;

            $scope.COTODateReturn = dataRow.ETA.addDays(1 / 48);
            $scope.COTODateShip = dataRow.ETA.addDays(1 / 24);
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

    $scope.COTOContainer_Start = function (e, item) {
        e.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONCO_TOContainer_StartOffer",
            data: {
                opsTOContainer: item.ID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
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

    $scope.ReasonChangeNote = "";
    $scope.COTOStopHour = 0;
    $scope.COTOLoadHour = 0;
    $scope.COTODateReturn = new Date();
    $scope.COTODateShip = new Date();
    $scope.COTOFail2Container = false;
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
                            opsTOContainer: dataRow.ID,
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
                            loadHour: $scope.COTOLoadHour,
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
                            dateReturnEmpty: $scope.COTODateReturn,
                            dateShipEmpty: $scope.COTODateShip,
                            fail2container: $scope.COTOFail2Container,
                            locationID: $scope.LocationDepotID,
                            vehicleID: vehicleID,
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
                        stopHour: $scope.COTOStopHour,
                        vehicleID: vehicleID
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

        var item = $scope.TOContainer_Grid.dataItem(e.target.closest('tr'));
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

    $scope.COTOContainer_ChangeStatusAccept_Click = function (e, win, vform) {
        var item = $scope.TOContainer_Grid.dataItem(e.target.closest('tr'));
        var vehicleID = null;
        if ($scope.ItemSave.isContinue)
            vehicleID = $scope.CurrentMaster.VehicleID;
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
                        isCutRomooc: $scope.ItemSave.isCutRomooc,
                        vehicleID: vehicleID,
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
        win.close();
    };

    $scope.COTOContainer_FastCompleteCO = function (e) {
        Common.Log("COTOContainer_FastCompleteCO");
        dataRow = $scope.TOContainer_Grid.dataItem(e.target.closest('tr'));
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

    //Muon Cont

    var _itemCOTOCO = {ID:0};
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

    $scope.COTOContainer_BorrowContOpen = function (e) {
        _itemCOTOCO = $scope.TOContainer_Grid.dataItem(e.target.closest('tr'));
        _packingID = _itemCOTOCO.PackingID;
        $scope.CountMax = _itemCOTOCO.PackingQuantity;
        e.preventDefault();
        $scope.TONonMasterWin.center().open();
        $scope.TOContainerNonMaster_Grid.dataSource.read();
        $scope.UnCompleteMaster_Grid.dataSource.read();
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

    //Swap cont

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
        _itemCOTOCO = $scope.TOContainer_Grid.dataItem(e.target.closest('tr'));
        _packingID = _itemCOTOCO.PackingID;
        $scope.CountMax = _itemCOTOCO.PackingQuantity;
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
    //#endregion

    //#region Station

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
        toolbar: kendo.template($('#station-grid-toolbar').html()),
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
            $rootScope.Loading.Change("Thông tin trạm...", $rootScope.Loading.Progress + LoadingStep);
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
                    $scope.ct_order_grid.dataSource.read();
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

    //#region Trouble

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
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
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
            $rootScope.Loading.Change("Thông tin cung đường...", $rootScope.Loading.Progress + LoadingStep);
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
                    $scope.ct_order_grid.dataSource.read();
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

    //#region Change Xe

    var CurrentTractorID = 0;
    var CurrentRomoocID = 0;
    $scope.VendorOfVehicleID = 0;
    $scope.VendorRomoocID = 0;

    $scope.ShowType = 1;

    $scope.tractorVendor_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TractorRead",
            readparam: function () {
                return {
                    DateFrom: null,
                    DateTo: null,
                    vendorID: $scope.VendorOfVehicleID,
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
        filterable: { mode: 'row' }, resizable: true, reorderable: true, selectable: true,
        columns: [
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
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            var data = grid.dataSource.data();
            for (var i = 0; i < data.length; i++) {
                if (data[i].ID == CurrentTractorID) {
                    grid.select("tr[data-uid='" + data[i].uid + "']");
                    break;
                }
            }
        }
    };

    $scope.romoocVendor_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_RomoocRead",
            readparam: function () {
                return {
                    DateFrom: null,
                    DateTo: null,
                    vendorID: $scope.VendorRomoocID,
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
        filterable: { mode: 'row' }, resizable: true, reorderable: true, selectable: true,
        columns: [
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
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            var data = grid.dataSource.data();
            for (var i = 0; i < data.length; i++) {
                if (data[i].ID == CurrentRomoocID) {
                    grid.select("tr[data-uid='" + data[i].uid + "']");
                    break;
                }
            }
        }
    };

    $scope.ListVehicle_Open = function (type) {
        $scope.ShowType = type;
        switch (type) {
            case 1:
                CurrentTractorID = $scope.CurrentMaster.VehicleID;
                $scope.VendorOfVehicleID = $scope.CurrentMaster.VendorID;
                $scope.tractorVendor_Grid.dataSource.read();
                break;
            case 2:
                CurrentRomoocID = $scope.CurrentMaster.RomoocID;
                $scope.VendorRomoocID = $scope.CurrentMaster.VendorRomoocID;
                $scope.romoocVendor_Grid.dataSource.read();
                break;
        }
        $scope.VendorVehicle_Win.center().open();
    };

    $scope.ChangeVehicle_Accept = function (e) {
        e.preventDefault();
        switch ($scope.ShowType) {
            case 1:
                var selectedRows = $scope.tractorVendor_Grid.select();
                if (selectedRows.length > 0) {
                    var dataItem = $scope.tractorVendor_Grid.dataItem(selectedRows[0]);
                    if ($scope.CurrentMaster.VehicleID != dataItem.ID) {
                        $scope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: "MONControlTower_ChangeTractor",
                            data: {
                                masterID: $scope.masterID,
                                vehicleID: dataItem.ID
                            },
                            success: function (res) {
                                $scope.IsLoading = false;
                                $scope.CurrentMaster.VehicleNo = dataItem.RegNo;
                                $scope.CurrentMaster.VehicleID = dataItem.ID;
                                $scope.VendorVehicle_Win.close();
                                $scope.ct_order_grid.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            }
                        })
                    }
                }
                break;
            case 2:
                var selectedRows = $scope.romoocVendor_Grid.select();
                if (selectedRows.length > 0) {
                    var dataItem = $scope.romoocVendor_Grid.dataItem(selectedRows[0]);
                    if ($scope.CurrentMaster.RomoocID != dataItem.ID) {
                        $scope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: "MONControlTower_ChangeRomooc",
                            data: {
                                masterID: $scope.masterID,
                                vehicleID: dataItem.ID
                            },
                            success: function (res) {
                                $scope.CurrentMaster.RomoocNo = dataItem.RegNo;
                                $scope.CurrentMaster.RomoocID = dataItem.ID;
                                $scope.IsLoading = false;
                                $scope.VendorVehicle_Win.close();
                                $scope.ct_order_grid.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            }
                        })
                    }
                }
                break;
        }
    };

    //#endregion

    //
    $scope.TOChangeShowType = 1;

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

    //location
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
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: true,
        toolbar: kendo.template($('#location-toolbar').html()),
        dataBound: function (e) {
            $scope.LocationData = this.dataSource.data();
            $rootScope.Loading.Change("Thông tin địa điểm...", $rootScope.Loading.Progress + LoadingStep);
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
               field: 'DateCome', width: 170, title: '{{RS.FLMAssetTimeSheet.DateTo}}', template: '#=Common.Date.FromJsonDMYHM(DateCome)#',
               editor: function (container, options) {
                   var input = $("<input kendo-date-time-picker k-options='DateDMYHM'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DateLeave', width: 170, title: '{{RS.FLMAssetTimeSheet.DateLeave}}', template: '#=Common.Date.FromJsonDMYHM(DateLeave)#',
               editor: function (container, options) {
                   var input = $("<input kendo-date-time-picker k-options='DateDMYHM'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DateComeEstimate', width: 120, title: '{{RS.FLMAssetTimeSheet.DateComeEstimate}}', template: '#=Common.Date.FromJsonDMYHM(DateComeEstimate)#',
           },
           {
               field: 'DateLeaveEstimate', width: 120, title: '{{RS.FLMAssetTimeSheet.DateLeaveEstimate}}', template: '#=Common.Date.FromJsonDMYHM(DateLeaveEstimate)#',
           },
           {
               field: 'Comment', width: 300, title: '{{RS.OPSDITOLocation.Comment}}',
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.Location_SaveAll = function ($event, grid) {
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
                method: "MONControlTowerCO_MasterLocationUpdate",
                data: { masterID: $scope.masterID, lst: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.TO_WinOptions = {
        width: '1025px', height: '100%',
        draggable: true, modal: true, resizable: false, title: false,
        open: function () {
            $timeout(function () {
                $rootScope.Loading.Show();
                $scope.TODetail_ReloadAllGrid(7);
                $scope.TO_Splitter.resize();
            }, 100)
        },
        close: function () {
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
            if (e.item.id == 'tab-coto') {
                $scope.Show_COTOKM_Update = true;
            }
            else {
                $scope.Show_COTOKM_Update = false;
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

    $scope.CUSGOP_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        change: function (e) {
            var grid = $scope.Return_Grid;
            grid.dataItem($(e.sender.wrapper).closest('tr')).GroupProductName = this.text();
            grid.dataItem($(e.sender.wrapper).closest('tr')).GroupProductID = this.value();
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

    //cbb

    $scope.CUSProduct_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'ProductName',
        dataValueField: 'ID',
        open: function (e) {
            var id = $scope.CUSGroupofProduct_Cbb.value();
            if (id > 0) {
                var lst = []
                angular.forEach($scope.DataCUSProduct, function (o, i) {
                    if (o.GroupOfProductID == id)
                        lst.push(o);
                })
                $scope.CUSProduct_CbbOptions.dataSource.data(lst);
            }
            else {
                $scope.CUSProduct_CbbOptions.dataSource.data([]);
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    $scope.SL_Product_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'ProductName',
        dataValueField: 'ID',
        change: function (e) {
            var item = this.dataItem(e.item);
            if (Common.HasValue(item)) {
                $scope.SL_Old.BBGN = item.QuantityBBGN_Old;
                $scope.SL_Old.Return = item.QuantityReturn_Old;
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

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

    $scope.Vehicle_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'RegNo', dataValueField: 'ID', enable: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { RegNo: { type: 'string' }, ID: { type: 'string' } } }
        }),
        change: function (e) {
            var a = this.dataItem(e.item);
            if (Common.HasValue(a)) {
                var id = a.DriverID;
                if (id > 0) {
                    $scope.Driver1_Cbb.value(id);
                }
            }

        }
    };

    $scope.Romooc_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'RegNo', dataValueField: 'ID', enable: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { RegNo: { type: 'string' }, ID: { type: 'string' } } }
        }),
        change: function (e) {
            var a = this.dataItem(e.item);
            if (Common.HasValue(a)) {
                var id = a.DriverID;
                if (id > 0) {
                    $scope.Driver1_Cbb.value(id);
                }
            }

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

    //event

    $scope.To_WinUpdate = function ($event) {
        Common.Log("WinToUpdate");
        var isSuccess = [];
        //$scope.CurrentMaster.VehicleNo = $scope.Vehicle_Cbb.text();
        //$scope.CurrentMaster.RomoocNo = $scope.Romooc_Cbb.text();

        if ($scope.CurrentMaster.IsVehicleVendor) {
            var dataDriver = $scope.Driver_GridOptions.dataSource.data();
            $scope.CurrentMaster.Driver1 = dataDriver[0];
            $scope.CurrentMaster.Driver2 = dataDriver[1];
            $scope.CurrentMaster.Assistant1 = dataDriver[2];
            $scope.CurrentMaster.Assistant2 = dataDriver[3];
        } else {
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
                    $scope.TO_Win.close();
                    $scope.ShedularLoadData();
                    $scope.ct_order_grid.dataSource.read();
                    $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
        else {
            $rootScope.Message({ Msg: isSuccess.join("; "), NotifyType: Common.Message.NotifyType.ERROR });
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
                $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
    }

    $scope.SL_Save = function ($event, grid) {
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
                method: "DIMonitorMaster_SL_Save",
                data: { lst: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })

    }

    $scope.Return_DeleteList = function ($event, grid) {
        $event.preventDefault();
        var lst = [];
        angular.forEach(grid.dataSource.data(), function (o, i) {
            if (o.IsChoose == true)
                lst.push(o.ID);
        });

        if (lst.length > 0)
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_GOPReturnDeleteList",
                data: { lst: lst },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.Return_Add = function ($event, win) {
        $event.preventDefault();
        for (var i = 0; i < $scope.DataDITOGroupProduct.length; i++) {
            $scope.ItemGOPRturn.OrderGroupID = $scope.DataDITOGroupProduct[i].ValueInt;
            break;
        }
        for (var i = 0; i < $scope.DataCUSProduct.length; i++) {
            $scope.ItemGOPRturn.ProductID = $scope.DataCUSProduct[i].ID;
            $scope.ItemGOPRturn.GroupProductID = $scope.DataCUSProduct[i].GroupOfProductID;
            break;
        }
        win.center().open();
    }

    $scope.GOPReturn_Save = function ($event, vform, win) {
        $event.preventDefault();
        $scope.ItemGOPRturn.MasterID = $scope.masterID;
        $scope.ItemGOPRturn.ProductID = $scope.ItemGOPRturn.ProductID > 0 ? $scope.ItemGOPRturn.ProductID : 0;
        if (vform()) {
            $scope.LoadingCount++;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_GOPReturnAdd",
                data: { item: $scope.ItemGOPRturn },
                success: function (res) {
                    $scope.LoadingCount--;
                    $scope.Return_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.MasterActualItem = {};
    $scope.TOMasterActual_Open = function (e, win) {
        e.preventDefault();
        win.center().open();
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MasterActual",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $scope.MasterActualItem = res;
            }
        })
    }

    $scope.TOMasterActual_Save = function (e, win) {
        e.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MasterActualChange",
            data: { item: $scope.MasterActualItem },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
        win.close();
    }

    //#endregion

    //#region Take other

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
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        });
    }

    //#region ket thuc tai bai

    $scope.normalTabStripOptions = {
        height: "100%", animation: false,
    }

    $scope.TOContainer_EndStation = function (e, dataitem) {
        e.preventDefault();
        _itemCOTOCO = dataitem;
        $scope.MasterEndStationWin.center().open();
        $scope.StandDetailGridOptions.dataSource.read();
    }

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
               template: '<a ng-click="PickEndStation($event,dataItem.ID)" class="btn-grid-pick" >Chọn</a>',
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
            readparam: function () { return { opsTOContainer: _itemCOTOCO.ID, } }
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: false, editable: false, selectable: true,
        columns: [
           {
               field: '', width: 70,
               template: '<a ng-click="PickEndStation($event,dataItem.ID)" class="btn-grid-pick" >Chọn</a>',
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

    $scope.PickEndStation = function (e, id) {
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
                        locationid: id,
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

    //#region Popup update TODetail
    $scope.IsWinTOUpdateMax = true;
    $scope.Time_From = new Date();
    $scope.Time_To = null;
    var MapNo = 0;
    var curUIDItem = {};
    var orgUIDItem = {};
    var curRomoocUIDItem = {};
    var orRomoocgUIDItem = {};
    $scope.TripItem = {};
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
            openMapV2.Active(MainMap);
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

    $scope.Vendor_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'VendorName', dataValueField: 'VendorID', enable: true, value: -1,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'VendorID', fields: { VendorName: { type: 'string' }, VendorID: { type: 'number' } } }
        }),
        change: function (e) {
            $scope.VendorOfVehicleID = this.value();
            $scope.vehicle_grid.dataSource.read();
        }
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
               title: '',
               filterable: { cell: { operator: 'contains', showOperators: false } }
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

    $scope.Location_Select = function ($event, item, win) {
        $event.preventDefault();

        switch ($scope.LocationType) {
            case 1: //Start
                $scope.TripItem.LocationStartID = item.ID;
                $scope.TripItem.LocationStartName = item.Location;
                $scope.TripItem.LocationStartLat = item.Lat;
                $scope.TripItem.LocationStartLng = item.Lng;
                break;
            case 2: //End
                $scope.TripItem.LocationEndID = item.ID;
                $scope.TripItem.LocationEndName = item.Location;
                $scope.TripItem.LocationEndLat = item.Lat;
                $scope.TripItem.LocationEndLng = item.Lng;
                break;
        }

        win.close();
    }

    $scope.SearchVehicle = function (e, grid) {
        e.preventDefault();
        grid.dataSource.read();
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
                            $scope.ct_order_grid.dataSource.read();
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
                            $scope.ct_order_grid.dataSource.read();
                            $scope.LoadMasterDetail($scope.masterID);
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
    };

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

    //#region Main Filter

    $scope.Vendor_multiselectOptions = {
        placeholder: "Chọn nhà vận tải...",
        dataTextField: "VendorName", dataValueField: "VendorID",
        valuePrimitive: true, autoBind: false, autoClose: false,
        dataSource: Common.DataSource.Local({
            data: [],
        }),
    };

    $scope.FromProvinceCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ProvinceName', dataValueField: 'ID', enable: true, placeholder: "Tỉnh thành...",
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { ProvinceName: { type: 'string' }, ID: { type: 'number' } } }
        }),
        change: function (e) {
            $scope.FromDistrictCbb.text("");
        },
    };

    $scope.FromDistrictCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'DistrictName', dataValueField: 'ID', enable: true, placeholder: "Quận huyện...",
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { ProvinceName: { type: 'string' }, ID: { type: 'number' } } }
        }),
        open: function (e) {
            var result = $.grep($scope.LocalObj.District, function (o) { return o.ProvinceID == $scope.FromProvinceCbb.value(); });
            $scope.FromDistrictCbb.dataSource.data(result);
        },
        close: function (e) {
            $scope.FromDistrictCbb.dataSource.data($scope.LocalObj.District);
        }
    };

    $scope.ToProvinceCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ProvinceName', dataValueField: 'ID', enable: true, placeholder: "Tỉnh thành...",
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { ProvinceName: { type: 'string' }, ID: { type: 'number' } } }
        }),
        change: function (e) {
            $scope.ToDistrictCbb.text("");
        },
    };

    $scope.ToDistrictCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'DistrictName', dataValueField: 'ID', enable: true, placeholder: "Quận huyện...",
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { ProvinceName: { type: 'string' }, ID: { type: 'number' } } }
        }),
        open: function (e) {
            var result = $.grep($scope.LocalObj.District, function (o) { return o.ProvinceID == $scope.ToProvinceCbb.value(); });
            $scope.ToDistrictCbb.dataSource.data(result);
        },
        close: function (e) {
            $scope.ToDistrictCbb.dataSource.data($scope.LocalObj.District);
        }
    };

    $scope.FilterButtonClick = function (e) {
        e.preventDefault();
        $scope.MaximumView(e);
        $scope.MONDI_Splitter.collapse('#pane2');
        $scope.stopInterval(interval_real_route);
        if ($scope.FromProvinceCbb.selectedIndex < 0)
            $scope.FilterVehicle.FromProvinceID = -1;

        if ($scope.FromDistrictCbb.selectedIndex < 0)
            $scope.FilterVehicle.FromDistrictID = -1;

        if ($scope.ToProvinceCbb.selectedIndex < 0)
            $scope.FilterVehicle.ToProvinceID = -1;

        if ($scope.ToDistrictCbb.selectedIndex < 0)
            $scope.FilterVehicle.ToDistrictID = -1;
        $scope.LoadingCount++;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MainFilter",
            data: { item: $scope.FilterVehicle },
            success: function (res) {
                // tranh loi combobox
                if ($scope.FromProvinceCbb.selectedIndex < 0)
                    $scope.FilterVehicle.FromProvinceID = "";
                if ($scope.FromDistrictCbb.selectedIndex < 0)
                    $scope.FilterVehicle.FromDistrictID = "";
                if ($scope.ToProvinceCbb.selectedIndex < 0)
                    $scope.FilterVehicle.ToProvinceID = "";
                if ($scope.ToDistrictCbb.selectedIndex < 0)
                    $scope.FilterVehicle.ToDistrictID = "";

                // bieu do 
                $scope.MONCT_OPSSummary = res.COSummary;
                var t = 0;
                angular.forEach(res.COSummary, function (o, i) {
                    t += o.Total;
                })
                res.COSummary.push({ Total: t, TypeOfContainer: "Tổng" })
                //MAP
                openMapV2.ClearMap();
                var style = openMapV2.NewStyle.Polygon(1);
                if ($scope.FilterVehicle.ToProvinceID != $scope.FilterVehicle.FromProvinceID && $scope.FilterVehicle.ToProvinceID > 0) {
                    var str1 = '';
                    if ($scope.FilterVehicle.ToProvinceID < 10)
                        str1 = '0';
                    var areaB = openMapV2.NewProvincePolygon(str1 + $scope.FilterVehicle.ToProvinceID, $scope.ToProvinceCbb.text(), "VectorProvince");
                    areaB.Init(style);
                }
                var str = '';
                if ($scope.FilterVehicle.FromProvinceID < 10)
                    str = '0';
                var areaA = openMapV2.NewProvincePolygon(str + $scope.FilterVehicle.FromProvinceID, $scope.FromProvinceCbb.text(), "VectorProvince");
                areaA.Init(style);

                //ve location
                $scope.Marker = [];
                $scope._mapMarkers = [];
                if (Common.HasValue(res.lstLocation)) {
                    $scope.DrawNewMarker(res.lstLocation, function (item) {
                        return openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Stock), 1);
                    }, "VectorMarker", true, 'LocationID', 'LocationName.');
                }

                $timeout(function () {
                    areaA.Center();
                }, 1000)


                if ($scope.Marker.length > 0)
                    openMapV2.FitBound("VectorMarker", 15);

                $scope.ShowFilterSum = 1;
                $scope.LoadingCount--;
            }
        })
    };

    $scope.ShowHideFilter = function (win) {
        if ($scope.FitlerWidth == '0px')
            $scope.FitlerWidth = '170px';
        else {
            $scope.FitlerWidth = '0px';
        }
    }

    $scope.ShowHideFilterSum = function () {
        $scope.ShowFilterSum = $scope.ShowFilterSum == 0 ? 1 : 0;
    }

    $scope.Filter_VehicleStatus = function (e, model) {
        e.preventDefault();
        switch (model) {
            case 1:
                $scope.FilterVehicle.IsVehicleFree = !$scope.FilterVehicle.IsVehicleFree;
                break;
            case 2:
                $scope.FilterVehicle.IsVehiclePlan = !$scope.FilterVehicle.IsVehiclePlan;
                break;
            case 3:
                $scope.FilterVehicle.IsVehicleGoing = !$scope.FilterVehicle.IsVehicleGoing;
                break;
        }
    }

    $scope.Filter_Time = function (e, model) {
        e.preventDefault();
        if ($(e.target.closest('ul')).attr('class') != 'filtet-timepicker') {
            switch (model) {
                case 1:
                    $scope.FilterVehicle.Time = 1;
                    $scope.MONCT_Filter.DateFrom = new Date();
                    $scope.MONCT_Filter.DateTo = new Date().addDays(1);
                    break;
                case 2:
                    $scope.FilterVehicle.Time = 2;
                    $scope.MONCT_Filter.DateFrom = $scope.GetMonday(new Date());
                    $scope.MONCT_Filter.DateTo = $scope.GetMonday(new Date().addDays(7));
                    break;
                case 3:
                    $scope.FilterVehicle.Time = 3;
                    var toDate = new Date();
                    $scope.MONCT_Filter.DateFrom = new Date(toDate.getFullYear(), toDate.getMonth(), 1);
                    $scope.MONCT_Filter.DateTo = new Date(toDate.getFullYear(), toDate.getMonth() + 1, 0);
                    break;
                case 4:
                    $scope.FilterVehicle.Time = 4;
                    $scope.FilterVehicle.ShowDate = !$scope.FilterVehicle.ShowDate;
                    break;
            }
            if (model != 4) {
                $scope.ct_order_grid.dataSource.read();
            }
        }
    }

    $scope.ViewDate_Options_Click = function (e) {
        e.preventDefault();
        $scope.ct_order_grid.dataSource.read();
    }

    //#endregion

    //#region Tab order

    $scope.ShowAllOPS_COTOContainer = function (e, type) {
        e.preventDefault();
        $scope.MONCT_Filter.IsLoadAll = !$scope.MONCT_Filter.IsLoadAll;
        $scope.ct_order_grid.dataSource.read();
    }

    $scope.ct_OrderSplitterOptions = {
        panes: [
                { collapsible: true, resizable: true, size: '50%' },
                { collapsible: true, resizable: true, size: '50%', collapsed: false }
        ],
    }

    $scope.ct_order_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OrderList",
            readparam: function () {
                return {
                    DateFrom: Common.Date.Date($scope.MONCT_Filter.DateFrom),
                    DateTo: Common.Date.Date($scope.MONCT_Filter.DateTo),
                    isRunning: $scope.MONCT_Filter.Going,
                    isLoadAll: $scope.MONCT_Filter.IsLoadAll,
                    isComplete: $scope.MONCT_Filter.Complete
                };
            },
            sort: [{ field: "MasterID", dir: "asc" }],
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
                    IsWarning: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        change: function (e) {
        },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ct_order_grid,ct_order_gridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ct_order_grid,ct_order_gridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: '', width: 60, title: '',
                template: '<a ng-click="BtnEditOrder($event,dataItem.MasterID)" class="k-button"><i class="fa fa-pencil"></i></a>',
                //headerTemplate: '<div class="filter-bar"><div class="filter-bar-row"><div ng-click="OrderCheckbox(1)" ng-class="{none:!MONCT_Filter.Going,going:MONCT_Filter.Going}" class="filter-bar-cell"></div><div ng-click="OrderCheckbox(2)" ng-class="{none:!MONCT_Filter.Complete,complete:MONCT_Filter.Complete}" class="filter-bar-cell"></div></div></div>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsWarning', width: 50, title: '{{RS.ORDOrder_Index.IsWarning}}', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" ng-click="warningtooltip.show()" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false
            },
           {
               field: 'ContainerNo', width: 100,
               title: '{{RS.ORDOrder.ContainerNo}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'OrderCode', width: 100,
               title: '{{RS.ORDOrder.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'VehicleNo', width: 150, sortable: false,
               title: '',
               //headerTemplate: '<div>Đầu kéo<img class="img-filter" ng-click="Filter_Status_Click($event)" src="images/function/ico_filter_down.png" />  <br/><div class="filter-status" ng-mouseleave="Filter_Status_Hide($event)" ng-style="{left: Filter_Status_Position.Left, top: Filter_Status_Position.Top}" ng-show="Show_Filter_Status">' + $scope.Filter_Html + '</div></div>',
               template: '<img ng-click="VehicleHistory(dataItem.MasterID)" ng-show="dataItem.IsCompleteCO" class="order-grid-img" src="images/function/ico_xe_done.png" /><img ng-click="VehicleHistory(dataItem.MasterID)" ng-show="!dataItem.IsCompleteCO" class="order-grid-img" src="images/function/ico_xe_going.png" />#=VehicleNo != null ? VehicleNo : ""#',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'RomoocNo', width: 100,
               title: '{{RS.OPSCOTOMaster.RomoocNo}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'MasterCode', width: 100,
               title: '{{RS.OPSDITOMaster.Code}}',
               template: '<a ng-click="DN_Click($event,dataItem)" title="Xem chi tiết" style="cursor:pointer;font-weight: bold;">#=MasterCode != null ? MasterCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'WarningCount', width: 100,
               title: '{{RS.MONMonitorIndex.WarningCount}}', attributes: { 'style': 'text-align: center;' },
               template: '<img class="ct-warning" ng-click="DN_Click($event,dataItem,true)"  ng-show="dataItem.WarningCount>0" src="images/function/ico_warning_active.png" /><img ng-click="DN_Click($event,dataItem,true)" ng-show="dataItem.WarningCount<=0" style="vertical-align:middle;cursor:pointer;" src="images/function/ico_warning.png" />',
               filterable: false
           },
           {
               field: 'ServiceOfOrder', width: 100,
               title: '{{RS.ORDOrder.ServiceOfOrderName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'StatusOfCOContainer', width: 100,
               title: '{{RS.StatusOfCOContainer}}.',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'TypeOfContainer', width: 100,
               title: '{{RS.CATPacking.Code}}.',
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

        }
    };

    $scope.MasterActualTime_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    MasterCode: { editable: false },
                    ID: { type: 'number' },
                    ATA: { type: 'date' },
                    ATD: { type: 'date' },
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: true,
        columns: [
            { field: 'MasterCode', title: '{{RS.OPSDITOMaster.Code}}', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'ATD', title: '{{RS.OPSDITOMaster.ATD}}', width: 150,
                template: '<input style="width:99%;" class="cus-datepicker" focus-k-datetimepicker kendo-date-time-picker k-ng-model="dataItem.ATD" name="ATD" k-options="DateDMYHM" />',
            },
            {
                field: 'ATA', title: '{{RS.OPSDITOMaster.ATA}}', width: 150,
                template: '<input style="width:99%;" class="cus-datepicker" focus-k-datetimepicker kendo-date-time-picker k-ng-model="dataItem.ATA" name="ATA" k-options="DateDMYHM" />',
            },
        ]
    };

    $scope.Return_RomoocList = function (e, grid) {
        var lstID = []
        angular.forEach(grid.dataSource.data(), function (o, i) {
            if (o.IsChoose) {
                lstID.push(o.ID);
            }
        })
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận tháo romooc?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ReturnRomooc",
                        data: { lst: lstID },
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

    $scope.BtnEditOrder = function (e, masterID) {
        e.preventDefault();
        isChangePlan = false;
        $scope.LoadMasterDetail(masterID);
    }

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
            $rootScope.Loading.Change("Thông tin vận chuyển...", 0);
            $rootScope.Loading.Show();
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_MasterGet",
                data: { id: masterID },
                success: function (res) {
                    $rootScope.Loading.Change("Thông tin vận chuyển...", 100);
                    $scope.TO_Win.center().open();
                    $scope.CurrentMaster = res;
                    $scope.masterID = res.ID;
                    $scope.TO_Win_Title = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentMaster.Code + ' - ETD: ' + $scope.CurrentMaster.ETD + ' ETA: ' + $scope.CurrentMaster.ETA;
                    if ($scope.CurrentMaster.IsVehicleVendor) {
                        $scope.Show_DriverGrid = true;
                    } else {
                        $scope.Show_DriverGrid = false;
                    }

                    //load data grid
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
                    //load regno
                    var temp = $scope.CurrentMaster.VehicleID;
                    var temp1 = $scope.CurrentMaster.RomoocID;
                    var vendorID = -1;
                    if ($scope.CurrentMaster.IsVehicleVendor)
                        vendorID = $scope.CurrentMaster.VendorID;
                    //$scope.Romooc_Cbb.dataSource.data($scope._romooc[vendorID]);
                    if (vendorID > 0) {
                        //$scope.Vehicle_Cbb.dataSource.data($scope._dataVehicleByVEN[vendorID]);
                    } else {
                        //$scope.Vehicle_Cbb.dataSource.data($scope._dataVehicleHome);
                    }

                    // load tai xe
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_MasterDriverList",
                        data: { masterID: $scope.masterID },
                        success: function (res) {
                            $scope.TroubleDriverCbbOptions.dataSource.data(res.Data);
                        }
                    })

                    $timeout(function () {
                        $scope.CurrentMaster.VehicleID = temp;
                        $scope.CurrentMaster.RomoocID = temp1;
                        //$scope.Vehicle_Cbb.value(temp);
                        //$scope.Romooc_Cbb.value(temp1);
                    }, 200)
                }
            });
        }
    }

    $rootScope.$watch('Loading.Progress', function (n, o) {
        if (n >= 100) {
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    })

    var interval_real_route = null;
    $scope.IntervalDelay = 10;
    $scope.stopInterval = function (stop) {
        if (Common.HasValue(stop)) {
            $interval.cancel(stop);
            stop = null;
        }
    };

    $scope.DN_Click = function ($event, dataItem, showRight) {
        $event.preventDefault();
        $scope.ShowOrderStatusPane = true;
        $scope.ct_order_grid.select($event.target.closest('tr'));

        $scope.stopInterval(interval_real_route);
        $scope.masterID = dataItem.MasterID;
        openMapV2.ClearVector("VectorMarker");
        openMapV2.ClearVector("VectorRoute");
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OrderLogList",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $scope.OrderLogData = res.LstTroubleLog;
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_GetLocationByMaster",
            data: { masterID: $scope.masterID },
            success: function (res) {

                $scope.CurrentMasterLocation = res;
                $scope.DrawNewMarker($scope.CurrentMasterLocation, function (item) {

                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Empty), 1);

                    if (item.LocationType == "romooc")
                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/blue/ico_end.png', 1);
                    if (item.LocationType == "seaport")
                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/blue/ico_seaport.png', 1);
                    if (item.LocationType == "depot")
                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/blue/ico_depot.png', 1);
                    if (item.LocationType == "stock")
                        icon = openMapV2.NewStyle.Icon('/Images/map/icon/blue/ico_stock.png', 1);
                    return icon;
                }, "VectorMarker", true, 'ID', 'LocationName');

                for (var i = 0; i < $scope.CurrentMasterLocation.length - 1; i++) {
                    var l1 = $scope.CurrentMasterLocation[i];
                    var l2 = $scope.CurrentMasterLocation[i + 1];
                    $scope.DrawRoute([l1.Lng, l1.Lat], [l2.Lng, l2.Lat]);
                }
                openMapV2.FitBound("VectorMarker");

            }
        })

        if (showRight == true)
            $scope.ct_OrderSplitter.expand(".k-pane:last");
    }

    $scope.RomoocMaster_Click = function ($event, dataItem) {
        $event.preventDefault();
        $scope.ShowOrderStatusPane = true;

        $scope.stopInterval(interval_real_route);
        $scope.masterID = dataItem.MasterID;
        openMapV2.ClearMap();
        openMapV2.ClearVector("VectorMarker");
        openMapV2.ClearVector("VectorRoute");
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_OrderLogList",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $scope.OrderLogData = res.LstTroubleLog;
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_GetLocationByMaster",
            data: { masterID: $scope.masterID },
            success: function (res) {

                $scope.CurrentMasterLocation = res;
                $scope.DrawNewMarker($scope.CurrentMasterLocation, function (item) {

                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Empty), 1);

                    if (item.LocationType == "delivery")
                        icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Delivery), 1);
                    if (item.LocationType == "get")
                        icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Get), 1);
                    if (item.LocationType == "getromooc")
                        icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.RomoocGet), 1);
                    if (item.LocationType == "returnromooc")
                        icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.RomoocReturn), 1);
                    return icon;
                }, "VectorMarker", true, 'ID', 'LocationName');

                for (var i = 0; i < $scope.CurrentMasterLocation.length - 1; i++) {
                    var l1 = $scope.CurrentMasterLocation[i];
                    var l2 = $scope.CurrentMasterLocation[i + 1];
                    $scope.DrawRoute([l1.Lng, l1.Lat], [l2.Lng, l2.Lat]);
                }
                openMapV2.FitBound("VectorMarker");

            }
        })
    }

    $scope.ToggleSplitter = function (e, sender) {
        e.preventDefault();
        sender.toggle("#order-pane-left", false);

    }

    $scope.OpenMasterCompleteConfirm_Click = function (e) {
        e.preventDefault();

        var data = [];

        var lstCheck = [];
        angular.forEach($scope.ct_order_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && lstCheck[o.MasterID] != true) {
                var atd = new Date();
                var ata = new Date().addDays(2 / 24);
                if (Common.HasValue(o.ATA)) {
                    ata = o.ATA;
                }
                else if (Common.HasValue(o.ETA)) {
                    ata = o.ETA;
                }
                if (Common.HasValue(o.ATD)) {
                    atd = o.ATD;
                }
                else if (Common.HasValue(o.ETD)) {
                    atd = o.ETD;
                }

                data.push({
                    MasterCode: o.MasterCode,
                    MasterID: o.MasterID,
                    ATD: atd,
                    ATA: ata,
                })
                lstCheck[o.MasterID] = true;
            }
        })

        $scope.MasterActualTime_Grid.dataSource.data(data);
        $timeout(function () {
            $scope.MasterActualTime_Grid.refresh();
        }, 300)

        $scope.MasterCompleteConfirm_Win.center().open();
    }

    $scope.CompleteMaster = function () {
        console.log("CompleteMaster")


        var data = $scope.MasterActualTime_Grid.dataSource.data();

        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh chuyến?',
                pars: {},
                Ok: function (pars) {
                    $scope.LoadingCount++;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_Complete",
                        data: {
                            lst: data,
                        },
                        success: function (res) {
                            $scope.LoadingCount--;
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.TractorShedularLoadData();
                            $scope.ct_order_grid.dataSource.read();
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Hãy chọn chuyến chưa hoàn thành!', NotifyType: Common.Message.NotifyType.ERROR });
        }
        $scope.MasterCompleteConfirm_Win.close();
    }

    $scope.CompleteMasterClick = function (e) {
        e.preventDefault();
        $scope.CompleteMaster()
    }

    $scope.RevertMaster = function ($event) {
        console.log("RevertMaster")
        $event.preventDefault();

        var lstMasterID = [];
        angular.forEach($scope.ct_order_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && o.IsComplete)
                lstMasterID.push(o.MasterID);
        })
        if (lstMasterID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hủy hoàn thành nhanh chuyến?',
                pars: {},
                Ok: function (pars) {
                    $scope.LoadingCount++;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_RevertMaster",
                        data: { lst: lstMasterID },
                        success: function (res) {
                            $scope.LoadingCount--;
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.ct_order_grid.dataSource.read();
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Hãy chọn hoàn thành!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    //#endregion

    //#region Gantt / Timeline

    var tractorSchedulerData = [];
    $scope.TractorSchedulerFilter = {
        DateFrom: Common.Date.Date($scope.GetMonday(new Date())),
        DateTo: $scope.GetMonday(new Date()).addDays(7),
        ListORDContainerID: [],
        ListRomoocID: [],
        ListTractorID: [],
        IsFilterMasterStatus: false,
    }

    $scope.ct_vehicle_schedulerOptions = {
        date: new Date().addDays(-1),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false, eventHeight: 20, majorTick: 60, height: '99%', editable: false,
        groupHeaderTemplate: $("#tractor-scheduler-header-template").html(),
        eventTemplate: $("#tractor-scheduler-template").html(),
        messages: {
            today: "Hôm nay"
        },
        views: [
            {
                type: MyCustomTimelistView,
                title: "Thiết lập",
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440
            },
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 50,
                //selectedDateFormat: "{0:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 120
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                selected: true,
                columnWidth: 50,
                editable: false,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 720
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 80,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440
            }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "meetingID",
                    fields: {
                        meetingID: { from: "MasterID", type: "number" },
                        title: { from: "title", defaultValue: "No title", validation: { required: true } },
                        start: { type: "date", from: "ETD" },
                        end: { type: "date", from: "ETA" },
                        attendees: { from: "VehicleID" },
                    }
                }
            }
        },
        group: {
            resources: ["VehicleID"],
            orientation: "vertical"
        },
        dataBound: function (e) {
            var timeline = this;
            var left = this.element.find('.k-scheduler-layout').children().children();
            var a = left.last();

            //fix group header template khong su dung dc $scope 
            angular.forEach(a.find('.k-scheduler-times th'), function (o, i) {
                $compile(o)($scope);
            });
        },
        resources: [
            {
                field: "attendees",
                name: "VehicleID",
                dataSource: [
                    { text: "", value: -1 }
                ],
                multiple: true,
            }
        ],
        navigate: function (e) {
            var time = this;
            $timeout(function () {
                time.refresh();
            }, 100)
        },
        save: function (e) {
            var item = {};
            var _e = e;
            var timeline = this;
            if (Common.HasValue(e.event.attendees[0]))
                item.VehicleID = e.event.attendees[0];
            else if (Common.HasValue(e.event.attendees))
                item.VehicleID = e.event.attendees;
            item.ETD = e.event.start;
            item.ETA = e.event.end;
            item.ID = e.event.id;

            $scope.LoadingCount++;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_SchedulerSaveChance",
                data: { item: item },
                success: function (res) {

                    $scope.LoadingCount--;
                    $rootScope.Message({ Msg: 'Lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                }
            })


        },
    }

    $scope.TractorShedularLoadData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TractorShedulerResource",
            data: { filter: $scope.TractorSchedulerFilter },
            success: function (res) {
                // load resource
                var lst = [];
                tractorSchedulerData = [];
                angular.forEach(res, function (o, i) {
                    lst.push(o.value);
                    tractorSchedulerData[o.value + ''] = o;
                });
                $scope.ct_vehicle_schedulerOptions.resources[0].dataSource = res;
                //load task event
                if (lst.length > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_TractorShedulerTask",
                        data: {
                            filter: $scope.TractorSchedulerFilter,
                            lst: lst
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

                                //chang empty
                                angular.forEach(o.ListContEmpty, function (empty, i) {
                                    empty.EE_Line_Left = 0;
                                    empty.EE_Line_Width = 0;
                                    if (empty.From != null && empty.To != null) {
                                        empty.From = Common.Date.FromJson(empty.From);
                                        empty.To = Common.Date.FromJson(empty.To);
                                        var length = o.DTo - o.DFrom;
                                        empty.EE_Line_Left = (empty.From - o.DFrom) * 100 / length;
                                        empty.EE_Line_Width = (empty.To - empty.From) * 100 / length;
                                    }
                                });
                            });
                            $scope.ct_vehicle_schedulerOptions.dataSource.data = res1;
                            $timeout(function () {
                                $scope.ct_vehicle_scheduler.date(new Date());

                                var rday = $scope.DayRelationCheck($scope.TractorSchedulerFilter.DateFrom, $scope.TractorSchedulerFilter.DateTo);

                                if (rday.DayCount < 7 && rday.SameWeek) {
                                    //load theo tuan
                                    $scope.ct_vehicle_scheduler.view('timelineWeek');
                                }
                                else if (rday.DayCount < 15 && rday.SameMonth) {
                                    $scope.ct_vehicle_scheduler.setOptions({
                                        numberOfDays: rday.DayCount
                                    });
                                    $scope.ct_vehicle_scheduler.view('MyCustomTimelistView');
                                }
                                else {
                                    //load theo thang
                                    $scope.ct_vehicle_scheduler.view('timelineMonth');
                                }
                                $scope.ct_vehicle_scheduler.refresh();
                            }, 100)
                        }
                    })
                }
            }
        })

    };
    $scope.TractorShedularLoadData();

    $scope.TractorSchedularFilter_Click = function (e, type) {
        e.preventDefault();
        switch (type) {
            case 1:
                break;
            case 4:
                $scope.TractorSchedulerFilter.ShowDate = !$scope.TractorSchedulerFilter.ShowDate;
                break;
            case 41:
                $scope.TractorShedularLoadData();
                $timeout(function () {
                    $scope.TractorSchedulerFilter.ShowDate = false;
                }, 200)
                break;
        }

    }

    $scope.TractorScheduleContainer_GetItem = function (value, field) {
        return Common.HasValue(tractorSchedulerData[value + '']) ? tractorSchedulerData[value + ''][field] : "";
    }

    $scope.Event_Click = function (masterID) {
        $scope.ShowOrderStatusPane = true;


        $scope.LoadMasterDetail(masterID);
    };

    //#endregion

    //#region Romooc Timeline

    var romoocSchedulerData = [];
    $scope.RomoocSchedulerFilter = {
        DateFrom: Common.Date.Date($scope.GetMonday(new Date())),
        DateTo: $scope.GetMonday(new Date()).addDays(7),
        ListORDContainerID: [],
        ListRomoocID: [],
        ListTractorID: [],
        IsFilterMasterStatus: false,
    }

    $scope.ct_romooc_schedulerOptions = {
        date: new Date().addDays(-1),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false, eventHeight: 20, majorTick: 60, height: '99%', editable: false,
        groupHeaderTemplate: $("#romooc-scheduler-header-template").html(),
        eventTemplate: $("#romooc-scheduler-template").html(),
        messages: {
            today: "Hôm nay"
        },
        views: [
            {
                type: MyCustomTimelistView,
                title: "Thiết lập",
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440
            },
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 50,
                //selectedDateFormat: "{0:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 120
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                selected: true,
                columnWidth: 50,
                editable: false,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 720
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 80,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440
            }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "meetingID",
                    fields: {
                        meetingID: { from: "MasterID", type: "number" },
                        title: { from: "title", defaultValue: "No title", validation: { required: true } },
                        start: { type: "date", from: "ETD" },
                        end: { type: "date", from: "ETA" },
                        attendees: { from: "RomoocID" },
                    }
                }
            }
        },
        group: {
            resources: ["RomoocID"],
            orientation: "vertical"
        },
        dataBound: function (e) {
            var timeline = this;
            var left = this.element.find('.k-scheduler-layout').children().children();
            var a = left.last();

            //fix group header template khong su dung dc $scope 
            angular.forEach(a.find('.k-scheduler-times th'), function (o, i) {
                $compile(o)($scope);
            });
        },
        resources: [
            {
                field: "attendees",
                name: "RomoocID",
                dataSource: [
                    { text: "", value: -1 }
                ],
                multiple: true,
            }
        ],
        navigate: function (e) {
            var time = this;
            $timeout(function () {
                time.refresh();
            }, 100)
        },
    }

    $scope.RomoocShedularLoadData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_RomoocShedulerResource",
            data: { filter: $scope.RomoocSchedulerFilter },
            success: function (res) {
                // load resource
                var lst = [];
                romoocSchedulerData = [];
                angular.forEach(res, function (o, i) {
                    lst.push(o.value);
                    romoocSchedulerData[o.value + ''] = o;
                });
                $scope.ct_romooc_schedulerOptions.resources[0].dataSource = res;
                //load task event
                if (lst.length > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_RomoocShedulerTask",
                        data: {
                            filter: $scope.RomoocSchedulerFilter,
                            lst: lst
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

                                //chang empty
                                angular.forEach(o.ListContEmpty, function (empty, i) {
                                    empty.EE_Line_Left = 0;
                                    empty.EE_Line_Width = 0;
                                    if (empty.From != null && empty.To != null) {
                                        empty.From = Common.Date.FromJson(empty.From);
                                        empty.To = Common.Date.FromJson(empty.To);
                                        var length = o.DTo - o.DFrom;
                                        empty.EE_Line_Left = (empty.From - o.DFrom) * 100 / length;
                                        empty.EE_Line_Width = (empty.To - empty.From) * 100 / length;
                                    }
                                });
                            });
                            $scope.ct_romooc_schedulerOptions.dataSource.data = res1;
                            $timeout(function () {
                                $scope.ct_romooc_scheduler.date($scope.RomoocSchedulerFilter.DateFrom);

                                var rday = $scope.DayRelationCheck($scope.RomoocSchedulerFilter.DateFrom, $scope.RomoocSchedulerFilter.DateTo);

                                if (rday.DayCount < 7 && rday.SameWeek) {
                                    //load theo tuan
                                    $scope.ct_romooc_scheduler.view('timelineWeek');
                                }
                                else if (rday.DayCount < 15 && rday.SameMonth) {
                                    $scope.ct_vehicle_scheduler.setOptions({
                                        numberOfDays: rday.DayCount
                                    });
                                    $scope.ct_romooc_scheduler.view('MyCustomTimelistView');
                                }
                                else {
                                    //load theo thang
                                    $scope.ct_romooc_scheduler.view('timelineMonth');
                                }
                                $scope.ct_romooc_scheduler.refresh();
                            }, 100)
                        }
                    })
                }
            }
        })

    };
    $scope.RomoocShedularLoadData();

    $scope.RomoocSchedularFilter_Click = function (e, type) {
        e.preventDefault();
        switch (type) {
            case 1:
                break;
            case 4:
                $scope.RomoocSchedulerFilter.ShowDate = !$scope.RomoocSchedulerFilter.ShowDate;
                break;
            case 41:
                $scope.RomoocShedularLoadData();
                $timeout(function () {
                    $scope.RomoocSchedulerFilter.ShowDate = false;
                }, 200)
                break;
        }

    }

    $scope.RomoocScheduleContainer_GetItem = function (value, field) {
        return Common.HasValue(romoocSchedulerData[value + '']) ? romoocSchedulerData[value + ''][field] : "";
    }

    //#endregion

    //#region Khac

    $scope.CloseWinInfo = function (e) {
        e.preventDefault();
        $scope.Show_Gantt_Info = false;
    }

    $scope.VehicleHistory = function (e) {
        $scope.VehicleLogItem.MasterID = e;

        if (Common.HasValue($scope.VehicleLogItem.MasterID))
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTowerCO_OrderLogList",
                data: { masterID: e },
                success: function (res) {
                    $scope.Show_Gantt_Info = true;
                    $scope.VehicleLogItem.RegNo = "";
                    $scope.VehicleLogData = res;
                }
            })
    }

    $scope.Complete_Master = function ($event, isComplete) {
        Common.Log("Complete_Master");
        $event.preventDefault();
        if (!isComplete) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh chuyến?',
                pars: {},
                Ok: function (pars) {
                    $scope.LoadingCount++;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_ControlTowerCO.URL.SRVehicleComplete,
                        data: { masterID: $scope.VehicleLogItem.MasterID },
                        success: function (res) {
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.ct_order_grid.dataSource.read();
                            Common.Services.Call($http, {
                                url: Common.Services.url.MON,
                                method: "MONControlTower_OrderLogList",
                                data: { masterID: $scope.VehicleLogItem.MasterID },
                                success: function (res) {
                                    $scope.LoadingCount--;
                                    $scope.VehicleLogData = res;
                                }
                            })
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: 'Chuyến đã hoàn thành',
            });
        }
    }

    $scope.UpdateCurrentLocal_Click = function (regno) {
        $scope.UpdateCurrentLocal(regno, function () {
            openMapV2.FitBound("VectorXe", 15);
            $rootScope.Message({ Msg: 'Cập nhật tọa độ thành công.', NotifyType: Common.Message.NotifyType.SUCCESS });
        })
    }

    $scope.UpdateCurrentLocal = function (regno, callback) {
        $scope.LoadingCount++;
        openMapV2.ClearVector("VectorXe");
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "Extend_VehiclePosition_GetLast",
            data: { vehicleCode: regno, dtfrom: new Date() },
            success: function (res) {
                $scope.LoadingCount--;
                if (Common.HasValue(res)) {


                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_GetVehicleStatus",
                        data: { vehicleNo: res.VehicleCode },
                        success: function (res1) {
                            res.Data = res1;
                            if (res.Lat != 0 && res.Lng != 0 && res.Lat != null && res.Lng != null) {
                                $scope.DrawNewMarker([res], function (item) {
                                    //acc on
                                    item.StatusName = "";
                                    if (item.Status == 1) {
                                        var minutesOff = 0;
                                        if (Common.HasValue(res.GPSDate)) {
                                            minutesOff = new Date(new Date() - new Date(res.GPSDate)).getMinutes();
                                        }
                                        if (minutesOff < 60) {
                                            if (item.Speed < 3) {
                                                item.StatusName = "Đang dừng";
                                            }
                                            else {
                                                item.StatusName = "Đang chạy";
                                            }
                                        }
                                        else {
                                            item.StatusName = "Mất tín hiệu";
                                        }
                                    }
                                    else {
                                        item.StatusName = "Không có tín hiệu";
                                    }


                                    var icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tractor.png', 1);
                                    if (res1.VehicleStatus == 'run') {
                                        if (res1.HasRomooc) {
                                            icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_40.png', 1);
                                            if (res1.IsRomooc20)
                                                icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_20.png', 1);
                                            if (res1.HasContainer) {
                                                icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_co_40f.png', 1);
                                                if (res1.ContainerType == 20)
                                                    icon = openMapV2.NewStyle.Icon('/Images/map/icon/orange/ico_tr_ro_co_20f.png', 1);
                                            }
                                        }
                                    }
                                    return icon;
                                }, "VectorXe", true, 'ID', 'VehicleCode');
                                if (Common.HasValue(callback)) {
                                    callback();
                                }
                            }
                            else {
                                $rootScope.Message({ Msg: 'Không thấy xe.', NotifyType: Common.Message.NotifyType.ERROR });
                            }
                        }

                    })

                }

            }

        })
    }

    $scope.TimelineFilterByRegNo = function (e) {
        if (e.which === 13) {

            var lst = [];
            angular.forEach($scope.LstTimelineResource, function (o, i) {
                if (o.text.toUpperCase().includes(e.target.value.toUpperCase()))
                    lst.push(o);
            })
            if (lst.length == 0) {
                if (e.target.value.trim() == "") {
                    $scope.ct_vehicle_schedulerOptions.resources[0].dataSource = $scope.LstTimelineResource;
                    $timeout(function () {
                        $scope.ct_vehicle_scheduler.refresh();
                    }, 100)
                }
                else
                    $rootScope.Message({ Msg: "Không tìm thấy xe", NotifyType: Common.Message.NotifyType.ERROR });
            }
            else {
                $scope.ct_vehicle_schedulerOptions.resources[0].dataSource = lst;
                $timeout(function () {
                    $scope.ct_vehicle_scheduler.refresh();
                }, 100)
            }
        }
    };

    $scope.Filter_xe = function (e, model) {
        e.preventDefault();
        switch (model) {
            case 1:
                $scope.FilterXe.Run = !$scope.FilterXe.Run;
                break;
            case 2:
                $scope.FilterXe.Free = !$scope.FilterXe.Free;
                break;
        }
        var lst = [];
        angular.forEach($scope.LstTimelineResource, function (o, i) {
            if ($scope.FilterXe.Run && o.text.split('|').length > 1)
                lst.push(o);
            else if ($scope.FilterXe.Free && o.text.split('|').length == 1)
                lst.push(o);
        })
        if (lst.length > 0) {
            $scope.ct_vehicle_schedulerOptions.resources[0].dataSource = lst;
            $timeout(function () {
                $scope.ct_vehicle_scheduler.refresh();
            }, 100)
        }
        else {
            switch (model) {
                case 1:
                    $scope.FilterXe.Run = !$scope.FilterXe.Run;
                    break;
                case 2:
                    $scope.FilterXe.Free = !$scope.FilterXe.Free;
                    break;
            }
            $rootScope.Message({ Msg: "Không tìm thấy xe", NotifyType: Common.Message.NotifyType.ERROR });
        }

    }

    $scope.MaxMap = function (e) {
        e.preventDefault();
        $scope.IsFullMap = !$scope.IsFullMap;
        if ($scope.IsFullMap) {
            $rootScope.IsShowMenu = false;
            $scope.MONDI_Splitter.toggle("#pane2", false);
        }
        else {
            $scope.MONDI_Splitter.toggle("#pane2", true);
        }
        $timeout(function () {
            $scope.MONDI_Splitter.resize();
        }, 200);
    }

    //#endregion

    //#region Container Scheduler

    $scope.SchedulerConfig = [
        {
            Lable: "Mã ĐH",
            Width: 100,
            Show: true,
            SortOrder: 1,
            Field: "OrderCode",
        },
        {
            Lable: "Loại Cont",
            Width: 100,
            Show: true,
            SortOrder: 2,
            Field: "TypeOfContainer",
        },
        {
            Lable: "Loại DV",
            Width: 100,
            Show: false,
            SortOrder: 3,
            Field: "ServiceOfOrder",
        },
        {
            Lable: "Mã Cont",
            Width: 100,
            Show: false,
            SortOrder: 4,
            Field: "ContainerNo",
        },
        {
            Lable: "Tấn",
            Width: 100,
            Show: false,
            SortOrder: 5,
            Field: "Ton",
        },
    ]
    $scope.ContainerRSColumnsClass = 'column2layout';

    //#region cookie
    var ck = Common.Cookie.Get('MON_ContainerSchedularConfig');
    if (Common.HasValue(ck)) {
        try {
            var obj = JSON.parse(ck);
            $scope.SchedulerConfig = obj;
            var count = 0;
            for (var m = 0; m < $scope.SchedulerConfig.length; m++) {
                if ($scope.SchedulerConfig[m].Show == true)
                    count++;
            }
            if (count == 3) {
                $scope.ContainerRSColumnsClass = 'column3layout';
            }
            else if (count == 2) {
                $scope.ContainerRSColumnsClass = 'column2layout';
            }
            else if (count == 1 || count == 0) {
                $scope.ContainerRSColumnsClass = 'column1layout';
            }
        }
        catch (e) { }
    }
    //#endregion

    $scope.ScheduleContainer_GetItem = function (value, field) {
        return Common.HasValue(_MONMonitor_ControlTowerCO.Data._scheduleContainer[value + '']) ? _MONMonitor_ControlTowerCO.Data._scheduleContainer[value + ''][field] : "";
    }

    $scope.GetData = function (e, a, s, t) {
        debugger
    }

    var MyCustomTimelistView = kendo.ui.TimelineMonthView.extend({
        options: {
            name: "MyCustomTimelistView",
            title: "Timeline Week",
            selectedDateFormat: "{0:D} - {1:D}",
            selectedShortDateFormat: "{0:d} - {1:d}",
            majorTick: 1440,
            numberOfDays: 7
        },
        name: "MyCustomTimelistView",
        calculateDateRange: function () {
            //create the required number of days

            var start = this.options.date,
                //  start = kendo.date.dayOfWeek(selectedDate, this.calendarInfo().firstDay, -1),
                idx, length,
                dates = [];

            for (idx = 0, length = this.options.numberOfDays; idx < length; idx++) {
                dates.push(start);
                start = kendo.date.nextDay(start);
            }
            this._render(dates);
        },
        previousDate: function () {
            var date = new Date(this.startDate());

            date.setDate(date.getDate() - this.options.numberOfDays);

            return date
        }
    });

    $scope.container_schedulerOptions = {
        date: new Date().addDays(-1),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false, editable: false, eventHeight: 25, majorTick: 60, height: '99%', width: '99.7%',
        eventTemplate: $("#container-scheduler-template").html(),
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
                type: MyCustomTimelistView,
                selected: true,
                title: "Thiết lập",
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440
            },
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 50,
                //selectedDateFormat: "{0:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 120
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 50,
                editable: false,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 720
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 80,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440
            }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "meetingID",
                    fields: {
                        meetingID: { from: "MasterID", type: "number" },
                        title: { from: "VehicleNo", defaultValue: "No title", validation: { required: true } },
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
                dataTextField: 'WarningMsg',
                dataValueField: 'ID',
                dataSource: [
                    { ID: -1, WarningMsg: '', OrderCode: '', TypeOfContainer: '' },
                ],
                multiple: true
            },
        ],
        dataBound: function (e) {
            var timeline = this;
            var left = this.element.find('.k-scheduler-layout').children().children();
            var a = left.last();

            //fix group header template khong su dung dc $scope 
            angular.forEach(a.find('.k-scheduler-times th'), function (o, i) {
                $compile(o)($scope);
            });

            //lst cac event co chung ordcontainerID
            var lstConect = [];
            angular.forEach(this.wrapper.find('.k-event'), function (ev, i) {
                var item = timeline.occurrenceByUid($(ev).data('uid'));
                if (!Common.HasValue(lstConect[item.attendees]))
                    lstConect[item.attendees] = [];
                lstConect[item.attendees].push(ev);

            });
            //noi 2 event cung ordcontainerID
            var addhtml = "";
            angular.forEach(lstConect, function (cn, i) {
                if (cn.length > 1) {
                    cn.sort(function (a, b) {
                        var itemA = timeline.occurrenceByUid($(a).data('uid'));
                        var itemB = timeline.occurrenceByUid($(b).data('uid'));
                        return itemA.ETD > itemB.ETD
                    })
                    for (var n = 0; n < cn.length - 1 ; n++) {
                        addhtml += EventConect(cn[n], cn[n + 1], "red", 2);
                    }
                }
            });
            $(timeline.wrapper.find('.k-scheduler-content')[0]).append(addhtml);
        },
    };

    $scope.SelectRowScheduler = function (e) {
        e.preventDefault();
        var tr = e.target.closest('tr');
        var body = e.target.closest('tbody');
        angular.forEach($(body).find('.highlight-tr'), function (o, i) {
            $(o).removeClass('highlight-tr');
        });
        $(tr).addClass('highlight-tr');
        //
        var index = $(body).find('tr').index(tr);
        var content = this.container_scheduler.element.find('.k-scheduler-content');
        var trs = $(content).find('tr')[index];
        angular.forEach($(content).find('.highlight-tr'), function (o, i) {
            $(o).removeClass('highlight-tr');
        });
        $(trs).addClass('highlight-tr');
    };

    $scope.SchedulerConfig_Accept = function (e, win) {
        e.preventDefault();
        $scope.SchedulerConfig.sort(function (a, b) { return a.SortOrder > b.SortOrder });
    };

    _MONMonitor_ControlTowerCO.Data._scheduleContainer['-1'] = { ID: -1, WarningMsg: '', OrderCode: '', TypeOfContainer: '' };

    $scope.orderFilter_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true, selectable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,orderFilter_Grid,orderFilter_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,orderFilter_Grid,orderFilter_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'OrderCode', width: 150,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', width: 100,
                title: '{{RS.ORDContainer.ContainerNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContainer', width: 100,
                title: '{{RS.CATPacking.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', width: 100,
                title: '{{RS.CUSCustomer.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
        }
    };

    $scope.tractorFilter_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TractorRead",
            readparam: function () {
                return {
                    DateFrom: null,
                    DateTo: null,
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
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true, selectable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,tractorFilter_Grid,tractorFilter_GridChange($event,this,true,tractorFilter_Grid))" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,tractorFilter_Grid,tractorFilter_GridChange($event,this))" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
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
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var o = grid.dataItem($(tr));
                if (lstTractorIsChoose.indexOf(o.ID) >= 0) {
                    o.IsChoose = true;
                    $(tr).find('.chkChoose').prop('checked', 'checked');
                }
            })
        }
    };

    $scope.romoocFilter_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_RomoocRead",
            readparam: function () {
                return {
                    DateFrom: null,
                    DateTo: null,
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
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true, selectable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,romoocFilter_Grid,romoocFilter_GridChange($event,this,true,romoocFilter_Grid))" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,romoocFilter_Grid,romoocFilter_GridChange($event,this))" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
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
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var o = grid.dataItem($(tr));
                if (lstRomoocIsChoose.indexOf(o.ID) >= 0) {
                    o.IsChoose = true;
                    $(tr).find('.chkChoose').prop('checked', 'checked');
                }
            })
        },
    };

    var lstTractorIsChoose = [];
    $scope.tractorFilter_GridChange = function (e, t, a, grid) {
        var val = $(e.target).prop('checked');
        if (a) {
            angular.forEach(grid.dataSource.data(), function (o, i) {
                if (val) {
                    if (lstTractorIsChoose.indexOf(o.ID) < 0) {
                        lstTractorIsChoose.push(o.ID);
                    }
                }
                else {
                    var idx = lstTractorIsChoose.indexOf(o.ID);
                    if (idx >= 0) {
                        lstTractorIsChoose.splice(idx, 1);
                    }
                }
            })
        }
        else {
            if (val) {
                if (lstTractorIsChoose.indexOf(t.dataItem.ID) < 0) {
                    lstTractorIsChoose.push(t.dataItem.ID);
                }
            }
            else {
                lstTractorIsChoose.splice(lstTractorIsChoose.indexOf(t.dataItem.ID), 1);
            }
        }
    }

    var lstRomoocIsChoose = [];
    $scope.romoocFilter_GridChange = function (e, t, a, grid) {
        var val = $(e.target).prop('checked');
        if (a) {
            angular.forEach(grid.dataSource.data(), function (o, i) {
                if (val) {
                    if (lstRomoocIsChoose.indexOf(o.ID) < 0) {
                        lstRomoocIsChoose.push(o.ID);
                    }
                }
                else {
                    var idx = lstRomoocIsChoose.indexOf(o.ID);
                    if (idx >= 0) {
                        lstRomoocIsChoose.splice(idx, 1);
                    }
                }
            })
        }
        else {
            if (val) {
                if (lstRomoocIsChoose.indexOf(t.dataItem.ID) < 0) {
                    lstRomoocIsChoose.push(t.dataItem.ID);
                }
            }
            else {
                lstRomoocIsChoose.splice(lstRomoocIsChoose.indexOf(t.dataItem.ID), 1);
            }
        }
    }

    $scope.ShedularLoadData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_ContainerShedulerResource",
            data: { filter: $scope.SchedulerFilter },
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
                $scope.container_schedulerOptions.resources[0].dataSource = res;

                //$scope.container_schedulerOptions.resources[0].dataSource = lstOrder;
                $scope.orderFilter_Grid.dataSource.data(data);
                if (lst.length > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTowerCO_ContainerShedulerTask",
                        data: {
                            filter: $scope.SchedulerFilter,
                            lst: lst
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

                                //chang empty
                                angular.forEach(o.ListContEmpty, function (empty, i) {
                                    empty.EE_Line_Left = 0;
                                    empty.EE_Line_Width = 0;
                                    if (empty.From != null && empty.To != null) {
                                        empty.From = Common.Date.FromJson(empty.From);
                                        empty.To = Common.Date.FromJson(empty.To);
                                        var length = o.ETA - o.ETD;
                                        empty.EE_Line_Left = (empty.From - o.ETD) * 100 / length;
                                        empty.EE_Line_Width = (empty.To - empty.From) * 100 / length;
                                    }
                                });


                            });
                            $scope.container_schedulerOptions.dataSource.data = res1;
                            $timeout(function () {
                                $scope.container_scheduler.date($scope.SchedulerFilter.DateFrom);
                                var days = ($scope.SchedulerFilter.DateTo - $scope.SchedulerFilter.DateFrom) / (24 * 60 * 60 * 1000);
                                if (days < 8) {
                                    //load theo tuan
                                    $scope.container_scheduler.view('timelineWeek');
                                }
                                else if (days < 15) {
                                    //load theo tuan
                                    $scope.container_scheduler.setOptions({
                                        numberOfDays: days
                                    });
                                    $scope.container_scheduler.view('MyCustomTimelistView');
                                }
                                else {
                                    //load theo thang
                                    $scope.container_scheduler.view('timelineMonth');
                                }
                                $timeout(function () {
                                    $scope.container_scheduler.refresh();
                                }, 200)
                            }, 100)
                        }
                    })
                }
            }
        })

    };
    $scope.ShedularLoadData();

    var ContainerSchedulerFilter_WinOpen1st = [];
    $scope.ContainerSchedularFilter_Click = function (e, type) {
        e.preventDefault();
        switch (type) {
            case 1:
                $scope.SchedulerFilter.IsFilterORD = !$scope.SchedulerFilter.IsFilterORD;
                $scope.SchedulerFilter.ShowType = 11;
                $scope.SchedulerFilter.ListORDContainerID = [];
                if ($scope.SchedulerFilter.IsFilterORD)
                    $scope.ContainerSchedulerFilter_Win.center().open();
                else
                    $scope.ShedularLoadData();
                break;
            case 11:
                var data = $scope.orderFilter_Grid.dataSource.data();
                angular.forEach(data, function (o, i) {
                    if (o.IsChoose)
                        $scope.SchedulerFilter.ListORDContainerID.push(o.ID);
                })
                $scope.ShedularLoadData();
                $scope.ContainerSchedulerFilter_Win.close();
                break;
            case 2:
                $scope.SchedulerFilter.IsFilterTractor = !$scope.SchedulerFilter.IsFilterTractor;
                $scope.SchedulerFilter.ShowType = 21;
                if ($scope.SchedulerFilter.IsFilterTractor)
                    $scope.ContainerSchedulerFilter_Win.center().open();
                else {
                    $scope.SchedulerFilter.ListTractorID = [];
                    $scope.ShedularLoadData();
                }
                break;
            case 21:
                var data = $scope.tractorFilter_Grid.dataSource.data();
                $scope.SchedulerFilter.ListTractorID = lstTractorIsChoose;
                $scope.ShedularLoadData();
                $scope.ContainerSchedulerFilter_Win.close();
                break;
            case 3:
                $scope.SchedulerFilter.IsFilterRomooc = !$scope.SchedulerFilter.IsFilterRomooc;
                $scope.SchedulerFilter.ShowType = 31;
                if ($scope.SchedulerFilter.IsFilterRomooc)
                    $scope.ContainerSchedulerFilter_Win.center().open();
                else {
                    $scope.SchedulerFilter.ListRomoocID = [];
                    $scope.ShedularLoadData();
                }
                break;
            case 31:
                var data = $scope.romoocFilter_Grid.dataSource.data();
                $scope.SchedulerFilter.ListRomoocID = lstRomoocIsChoose;
                $scope.ShedularLoadData();
                $scope.ContainerSchedulerFilter_Win.close();
                break;
            case 4:
                if ($scope.SchedulerFilter.IsFilterDate) {
                    if (!$scope.SchedulerFilter.ShowDate) {
                        $scope.SchedulerFilter.DateFrom = Common.Date.Date($scope.GetMonday(new Date()));
                        $scope.SchedulerFilter.DateTo = $scope.GetMonday(new Date()).addDays(7);
                        $scope.ShedularLoadData();
                    }
                    else {
                        $scope.SchedulerFilter.ShowDate = false;
                    }
                }
                else {
                    $scope.SchedulerFilter.ShowDate = true;
                }
                $scope.SchedulerFilter.IsFilterDate = !$scope.SchedulerFilter.IsFilterDate;
                break;
            case 41:
                $scope.SchedulerFilter.DateFrom = $scope.GetMonday($scope.SchedulerFilter.DateFrom);
                $scope.SchedulerFilter.DateTo = $scope.GetLastWeek($scope.SchedulerFilter.DateFrom, $scope.SchedulerFilter.DateTo);
                $scope.SchedulerFilter.IsFilterDate = true;
                $scope.ShedularLoadData();
                $timeout(function () {
                    $scope.SchedulerFilter.ShowDate = false;
                }, 100)
                break;
        }

        //resize lại grid
        if (ContainerSchedulerFilter_WinOpen1st[type] != true && type < 10) {
            ContainerSchedulerFilter_WinOpen1st[type] = true;
            $timeout(function () {
                $scope.orderFilter_Grid.refresh();
                $scope.romoocFilter_Grid.refresh();
                $scope.tractorFilter_Grid.refresh();
            }, 500)
        }
    }



    //#region config

    var dragItem = null;

    $scope.schedulerConfigGridOptions = {
        dataSource: Common.DataSource.Local({
            data: $scope.SchedulerConfig
        }),
        height: '100%', groupable: false, pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: 'Lable', title: 'Tên cột', width: '75px',
                headerAttributes: { style: 'text-align: left;' }, attributes: { style: 'text-align: left;' }
            },
            {
                field: 'Show', title: 'Hiện', width: '40px',
                template: '<input ng-click="CheckShowRS_Click($event,dataItem)" type="checkbox" ng-model="dataItem.Show" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }
            },
            {
                field: 'Width', title: 'Kích thước cột', width: '350px',
                template: '<input type="nunber" class="k-textbox" style="width: 50px;float:left;" ng-model="dataItem.Width" /><input kendo-slider ng-model="dataItem.Width" k-tooltip="{ enabled: true }" k-max="300" k-min="0" style="width: 300px;" />',
                headerAttributes: { style: 'text-align: left;' }, attributes: { style: 'text-align: left;' }
            },
            {
                title: ' ', filterable: false
            }
        ],
        columnResize: function (e) {
            if (e.column.field == 'Width')
                e.preventDefault();
        },
    }

    $scope.SchedulerConfig_Open = function (e, win) {
        e.preventDefault();
        win.center().open();
    }

    $scope.SchedularConfigDragOptions = {
        hint: function (el) {
            var obj = $(el[0]).clone();
            obj.addClass('dragging');
            obj.css('width', '55px');
            obj.css('height', '40px');
            obj.css('line-height', '30px');
            obj.css('font-size', '10px');
            return obj;
        },
        dragstart: function (e) {
            var sid = $(e.target).attr('sid');
            for (var i = 0; i < $scope.SchedulerConfig.length; i++) {
                if ($scope.SchedulerConfig[i].SortOrder == sid) {
                    dragItem = $scope.SchedulerConfig[i];
                    break;
                }
            }
        },
        dragend: function (e) {
            e.preventDefault();
            angular.forEach($('.ct-table').find('.highlight'), function (td, i) {
                $(td).removeClass('highlight');
            })
        }
    }

    $scope.SchedularConfigDropOptions = {
        dragenter: function (e) {
            $($(e.dropTarget[0]).find('.ct-drag')).addClass('highlight');
        },
        dragleave: function (e) {
            $($(e.dropTarget[0]).find('.ct-drag')).removeClass('highlight');
        },
        drop: function (e) {
            e.preventDefault();
            var dropSID = $(e.dropTarget[0]).find('div').attr('sid');
            var dropItem = null;
            if (dragItem.SortOrder != dropSID) {
                for (var i = 0; i < $scope.SchedulerConfig.length; i++) {
                    if ($scope.SchedulerConfig[i].SortOrder == dropSID) {
                        dropItem = $scope.SchedulerConfig[i];
                    }
                }
                var temp = dragItem.SortOrder;
                dragItem.SortOrder = dropItem.SortOrder;
                dropItem.SortOrder = temp;
                $scope.SchedulerConfig.sort(function (a, b) { return a.SortOrder > b.SortOrder });
            }
        }
    }

    $scope.CheckShowRS_Click = function (e, item) {
        var count = 0;
        for (var m = 0; m < $scope.SchedulerConfig.length; m++) {
            if ($scope.SchedulerConfig[m].Show == true)
                count++;
        }

        if (count > 3 && item.Show) {
            e.preventDefault();
            item.Show = false;
            $rootScope.Message({ Msg: 'Không được chọn quá 4 cột', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (count < 1 && !item.Show) {
            e.preventDefault();
            item.Show = true;
            $rootScope.Message({ Msg: 'Không được chọn ít hơn 1 cột', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (count == 3) {
            $scope.ContainerRSColumnsClass = 'column3layout';
        }
        else if (count == 2) {
            $scope.ContainerRSColumnsClass = 'column2layout';
        }
        else if (count == 1) {
            $scope.ContainerRSColumnsClass = 'column1layout';
        }

    }

    $scope.SchedularConfig_WinClose = function () {
        Common.Cookie.Set('MON_ContainerSchedularConfig', JSON.stringify($scope.SchedulerConfig));
    }

    $scope.lstTest = [];
    $scope.SchedularConfig_WinOpen = function () {
        $scope.SchedulerConfig = $scope.schedulerConfigGrid.dataSource.data();
        $timeout(function () {
            $scope.schedulerConfigGrid.refresh();
        }, 100)

    }
    //#endregion

    //#region helper draw conect
    var EventConect = function (div1, div2, color, thickness) {
        var off1 = getOffset(div1);
        var off2 = getOffset(div2);
        // bottom right
        var x1 = off1.left + off1.width;
        var y1 = off1.top + off1.height / 2;
        // top right
        var x2 = off2.left;
        var y2 = off2.top + off1.height / 2;
        // distance
        var length = Math.sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
        // center
        var cx = ((x1 + x2) / 2) - (length / 2);
        var cy = ((y1 + y2) / 2) - (thickness / 2);
        // angle
        var angle = Math.atan2((y1 - y2), (x1 - x2)) * (180 / Math.PI);
        // make hr
        var htmlLine = "<div style='padding:0px; margin:0px; height:" + thickness + "px; background-color:" + color + "; line-height:1px; position:absolute; left:" + cx + "px; top:" + cy + "px; width:" + length + "px; -moz-transform:rotate(" + angle + "deg); -webkit-transform:rotate(" + angle + "deg); -o-transform:rotate(" + angle + "deg); -ms-transform:rotate(" + angle + "deg); transform:rotate(" + angle + "deg);'></div>";
        //
        return htmlLine;
    }

    var getOffset = function (el) {
        var rect = el.getBoundingClientRect();
        return {
            left: el.offsetLeft,
            top: el.offsetTop,
            width: el.offsetWidth,
            height: el.offsetHeight
        };
    }

    //#endregion

    //#endregion
    
    //#region Event

    $scope.IsShowProblem = false;
    $scope.ShowMapProblem = function (e) {
        e.preventDefault();
        $scope.IsShowProblem = !$scope.IsShowProblem;
        if ($scope.IsShowProblem) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_ProblemList",
                data: {},
                success: function (res) {
                    openMapV2.ClearVector("VectorProblem");
                    $scope.DrawNewMarker(res, function (item) {
                        return openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Truck), 1);
                    }, "VectorProblem", true, 'ID', 'TypeOfRouteProblemName');
                    openMapV2.FitBound("VectorProblem");
                    if (res.length == 0) {
                        $rootScope.Message({ Msg: 'Không có sự cố phát sinh', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                }
            });
        }
        else {
            openMapV2.ClearVector("VectorProblem");
        }

        $rootScope.Message({
            Type: Common.Message.Type.Alert,
            Msg: 'Chưa thiết lập bãi mooc',
        });
    }

    $scope.Xoay = function (e) {
        e.preventDefault();
        if (Common.HasValue($state.params.orien))
            $state.go("main.MONMonitor.ControlTowerCO", { orien: null }, { reload: true });
        else
            $state.go("main.MONMonitor.ControlTowerCO", { orien: "ver" });
    }

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }

    $scope.Return_Save = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        var lstErr = [];
        for (var i = 0; i < grid.dataSource.data().length; i++) {
            var item = grid.dataSource.data()[i];
            if (item.dirty) {
                if (item.GroupProductID > 0)
                    data.push(item);
                else
                    lstErr.push(item.STT);
            }
        }
        if (data.length > 0)
            $scope.LoadingCount++;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_ControlTowerCO.URL.GOPReturn_Save,
            data: { data: data, masterID: $scope.masterID },
            success: function (res) {
                $scope.LoadingCount--;
                grid.dataSource.read();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })

        if (lstErr.length > 0) {
            var lst = lstErr.join(', ');
            $rootScope.Message({ Msg: 'Dòng ' + lst + ' chưa chọn nhóm SP', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Grid_Cancel = function ($event, grid) {
        $event.preventDefault();
        grid.cancelChanges();
        $scope.isEditting = false;
    }

    $scope.Trouble_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Trouble_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTowerCO_TroubleDelete",
                    data: { item: item },
                    success: function (res) {
                        $scope.Trouble_Grid.dataSource.read();
                    }
                })
            }
        });
    }

    $scope.ApprovedTrouble = function (troubleID, $index, data) {
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Duyệt chi phí',
            pars: {},
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTower_ApprovedTrouble",
                    data: { troubleID: troubleID },
                    success: function (res) {
                        data[$index].CostStatus = 1;
                    }
                })
            }
        });
    }

    $scope.RejectTrouble = function (troubleID, $index, data) {
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Từ chối chi phí',
            pars: {},
            Ok: function (pars) {
                data[$index].CostStatus = 2;
            }
        });
    }

    $scope.FilterByRouting = function () {
        var a = $scope.filterWindow;

        a.setOptions({
            height: 225
        })

        if ($scope.MONCT_Filter.RouteID > 0) {
            $scope.FilterLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_FilterByRoute",
                data: {
                    routeID: $scope.MONCT_Filter.RouteID,
                    dfrom: $scope.MONCT_Filter.DateFrom,
                    dto: $scope.MONCT_Filter.DateTo
                },
                success: function (res) {
                    $scope.FilterLoading = false;
                    $scope.MONCT_OPSSummary = res;
                }
            })
        }

        $scope.ct_order_grid.dataSource.read();
    }

    //#endregion

    //#region Init

    $scope.Init = function () {
        //load data
        $scope.LoadingCount += 6;

        //VendorList
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_ControlTowerCO.URL.VendorList,
            data: {},
            success: function (res) {
                $scope.LoadingCount--;
                $scope.Vendor_multiselectOptions.dataSource.data(res.Data);
                res.Data.push({
                    VendorID: -1,
                    VendorName: 'Xe nhà'
                });
                $scope._dataVendor = res.Data;
                $scope.Vendor_CbbOptions.dataSource.data(res.Data);
                if ($scope._dataTruck.length > 0 && $scope._dataVendor.length > 0)
                    $scope.InitDataVendorVehicle();
            }
        })

        //TruckList
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "Tractor_List",
            data: {},
            success: function (res) {
                $scope.LoadingCount--;
                $scope._dataTruck = res.Data;
                if ($scope._dataTruck.length > 0 && $scope._dataVendor.length > 0)
                    $scope.InitDataVendorVehicle();
            }
        })

        //TruckList
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "Romooc_List",
            data: {},
            success: function (res) {
                $scope.LoadingCount--;
                $scope._romooc = [];
                angular.forEach(res.Data, function (o, i) {
                    if (!Common.HasValue($scope._romooc[o.VendorID])) {
                        $scope._romooc[o.VendorID] = [];
                    }
                    $scope._romooc[o.VendorID].push(o);
                })
            }
        })

        //DIMonitor_ListTypeOfDriver
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_ListTypeOfDriver",
            data: {},
            success: function (res) {
                $scope.TypeOfDriver_CbbOptions.dataSource.data(res);
            }
        })

        //TroubleList
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitorDN_Index.URL.TroubleList,
            data: { isCo: true },
            success: function (res) {
                $scope.GroupOfTrouble_CbbOptions.dataSource.data(res.Data);
            }
        })

        //DriverList
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_ControlTowerCO.URL.DriverList,
            data: {},
            success: function (res) {
                $scope.LoadingCount--;
                $scope._dataDriver = res.Data;
                $scope.Driver1_CbbOptions.dataSource.data(res.Data);
                $scope.Driver2_CbbOptions.dataSource.data(res.Data);
            }
        })

        //province
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: Common.ALL.URL.Province,
            data: {},
            success: function (res) {
                $scope.LoadingCount--;
                $scope.LocalObj.Province = res.Data;
                $scope.FromProvinceCbbOptions.dataSource.data(res.Data);
                $scope.ToProvinceCbbOptions.dataSource.data(res.Data);
            }
        });

        //district
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: Common.ALL.URL.District,
            data: {},
            success: function (res) {
                $scope.LoadingCount--;
                $scope.LocalObj.District = res.Data;
                $scope.FromDistrictCbbOptions.dataSource.data(res.Data);
                $scope.ToDistrictCbbOptions.dataSource.data(res.Data);
            }
        });

        $scope.$watch('LoadingCount', function (newValue, oldValue) {
            if (newValue < 0)
                $scope.LoadingCount = 0;
            if (newValue == 0) {
                $rootScope.IsLoading = false;
            }
            else
                $rootScope.IsLoading = true;
        })
        //tabstrip
        $timeout(function () {
            if (!$scope.ShowOrderStatusPane && Common.HasValue($scope.ct_OrderSplitter)) {
                $scope.ct_OrderSplitter.collapse(".k-pane:last");
            }
        }, 100);
    }
    $scope.Init();


    $timeout(function () {
        $scope.CreateMap();
        openMapV2.ClearMap();
        openMapV2.Active(MainMap);
        MapNo = 1;
    }, 500);

    $scope.LoadGOPReturnData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_DITOGroupProductList",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $scope.DataDITOGroupProduct = res;
                $scope.OPSGroupCbbOptions.dataSource.data(res);
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_CUSProductList",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $scope.DataCUSProduct = res;
                $scope.CUSProduct_CbbOptions.dataSource.data(res);
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.CUSGOP_List,
            data: { customerid: $scope.masterID },
            success: function (res) {
                $scope.CUSGroupofProduct_CbbOptions.dataSource.data(res);
            }
        })
    }

    $scope.OpenFile_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.UploadFile({
            IsImage: true,
            ID: data.ID,
            AllowChange: !data.IsReceived,
            ShowChoose: false,
            Type: Common.CATTypeOfFileCode.TROUBLECO,
            Window: win,
            Complete: function (image) {
                $scope.Item.Image = image.FilePath;
            }
        });
    }

    $scope.InitDataVendorVehicle = function () {
        var lstVehicle = $scope._dataTruck;
        var lstVendor = $scope._dataVendor;
        $scope._dataVehicleByVEN = [];
        $scope._dataVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue($scope._dataVehicleByVEN[vendorid]))
                        $scope._dataVehicleByVEN[vendorid] = [];
                    $scope._dataVehicleByVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    $scope._dataVehicleHome.push(vehicle);
                }
            } else {
                $scope._dataVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue($scope._dataVehicleByVEN[vendorid]))
                        $scope._dataVehicleByVEN[vendorid] = [];
                    $scope._dataVehicleByVEN[vendorid].push(vehicle);
                }
            }
        });
    };

    //#endregion

    //#region new map
    var MainMap;

    $scope.CreateMap = function () {
        Common.Log("CreateMap");
        Common.Log(openMapV2.hasMap);
        openMapV2.hasMap = true;
        MainMap = openMapV2.Init({
            Element: 'MON_Map',
            Tooltip_Show: true,
            Tooltip_Element: 'MON_Map_tooltip',
            InfoWin_Show: true,
            InfoWin_Element: 'Map_Info_Win',
            DefinedLayer: [{
                Name: 'VectorMarker',
                zIndex: 100
            }, {
                Name: 'VectorXe',
                zIndex: 100
            }, {
                Name: 'VectorVehicle',
                zIndex: 100
            }, {
                Name: 'VectorRomooc',
                zIndex: 100
            }, {
                Name: 'VectorProblem',
                zIndex: 100
            }, {
                Name: 'VectorRoute',
                zIndex: 90
            }, {
                Name: 'VectorRealRoute',
                zIndex: 90
            }, {
                Name: 'VectorProvince',
                zIndex: 80
            }],
            ClickMarker: function (o, l) {
                if (Common.HasValue(o.RomoocRegNo)) {
                    $scope.MarkerType = "vehicleTractor";
                    $scope.MarkerItemBind = o;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "Extend_VehiclePosition_GetLast",
                        data: { vehicleCode: $scope.MarkerItemBind.GPSCode, dtfrom: new Date() },
                        success: function (res) {
                            if (Common.HasValue(res)) {
                                $scope.MarkerItemBind.StatusName = "";
                                if (res.Status == 1) {
                                    var minutesOff = 0;
                                    if (Common.HasValue(res.GPSDate)) {
                                        minutesOff = new Date(new Date() - new Date(res.GPSDate)).getMinutes();
                                    }
                                    if (minutesOff < 60) {
                                        if (res.Speed < 3) {
                                            $scope.MarkerItemBind.StatusName = "Đang dừng";
                                        }
                                        else {
                                            $scope.MarkerItemBind.StatusName = "Đang chạy";
                                        }
                                    }
                                    else {
                                        $scope.MarkerItemBind.StatusName = "Mất tín hiệu";
                                    }
                                }
                                else {
                                    $scope.MarkerItemBind.StatusName = "Không có tín hiệu";
                                }

                            }

                        }

                    })
                }
                else if (Common.HasValue(o.IsLaden)) {
                    $scope.MarkerType = "vehicleRomooc";
                    $scope.MarkerItemBind = o;
                }
                else if (Common.HasValue(o.TypeOfRouteProblemID)) {
                    $scope.MarkerType = "problem";
                    $scope.MarkerItemBind = o;
                }
                else if (Common.HasValue(o.LocationID)) {
                    $scope.MarkerType = "route";
                    $scope.MarkerItemBind = o;
                }
                else if (Common.HasValue(o.VehicleCode)) {
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

        openMapV2.ClearVector("VectorMarker");

        openMapV2.NewControl(
            '<i style="font-size:20px" class="fa fa-clock-o"></i>',
            'Thiết lập',
            'map-view-buttonleft1',
            function () {
                $timeout(function () {
                    $scope.MapConfig_Win.center().open();
                }, 1)
            }
        );

        openMapV2.NewControl('V', 'Chỉ hiện đầu kéo', 'map-view-buttonleft', function (e, o, a) {
            switch (o.textContent) {
                case ' ':
                    o.textContent = 'V';
                    o.setAttribute('title', 'Chỉ hiện đầu kéo');
                    $scope.ShowTractor = false;
                    $scope.ShowRomooc = false;
                    break;
                case 'V':
                    o.textContent = 'R';
                    o.setAttribute('title', 'Chỉ hiện romooc');
                    $scope.ShowTractor = true;
                    $scope.ShowRomooc = false;
                    break;
                case 'R':
                    o.textContent = 'A';
                    o.setAttribute('title', 'Hiện tất cả');
                    $scope.ShowTractor = false;
                    $scope.ShowRomooc = true;
                    break;
                case 'A':
                    o.textContent = ' ';
                    o.setAttribute('title', 'Ẩn tất cả');
                    $scope.ShowTractor = true;
                    $scope.ShowRomooc = true;
                    break;
            }
            if ($scope.ShowRomooc || $scope.ShowTractor)
                $scope.AutoRun.IsFitBound = true;

            $scope.Owner_Draw();
        })
    }

    $scope.DrawRoute = function (from, to) {
        openMapV2.NewRoute(from, to, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
            openMapV2.FitBound("VectorRoute", 15);
        });
    }

    $scope.CloseMapInfo = function (e) {
        e.preventDefault();
        openMapV2.Close();
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

    $scope.MapConfig_SaveChanges = function (e, win) {
        e.preventDefault();
        win.close();
        if (!$scope.AutoRun.Enabled) {
            $scope.masterID = 0;
        }
        $scope.AutoRun_Start();
    }

    //#endregion

    //#region location map

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
            openMapV2.Active(MainMap);
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

    $scope.AddNewLocation_Open = function (e, win) {
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

    //#region Map info
    $scope.MapLeft = '-214px';

    $scope.CloseMapPanel_Click = function (e) {
        if ($scope.MapLeft != '0px') {
            $scope.MapLeft = '0px';
        }
        else {
            $scope.MapLeft = '-214px';
        }
    }

    //#endregion

    //#region Create Vehicle
    var CreateType = 1;

    $scope.VehicleItem = {
        RegNo: "",
        MaxWeight: 0,
        VendorID: 0,
        vehicleType: 1,
    }

    $scope.OpenCreateVehicle = function (e, win, type) {
        e.preventDefault();
        CreateType = type;
        win.center().open();
    }

    $scope.CreateVehicle_Accept = function (e, win, vf) {
        e.preventDefault();
        var method = "Vehicle_Create";
        var vtype = 1;
        var venid = $scope.VendorOfVehicleID;
        if (CreateType == 2) {
            method = "Romooc_Create";
            vtype = 2;
            venid = $scope.VendorRomoocID;
        }
        if (vf()) {
            $scope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: method,
                data: {
                    vehicleNo: $scope.VehicleItem.RegNo,
                    maxWeight: $scope.VehicleItem.MaxWeight,
                    vendorID: venid,
                    vehicleType: vtype,
                },
                success: function (res) {
                    $scope.IsLoading = false;
                    $scope.tractorVendor_Grid.dataSource.read();
                    $scope.romoocVendor_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    //#endregion

}])