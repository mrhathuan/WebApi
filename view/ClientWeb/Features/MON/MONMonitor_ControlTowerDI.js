/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/openMapV2.js" />

var _MONCT_Index = {
    URL: {
        SRVehicleComplete: "DIMonitor_MsComplete",
        SRVehicleCompleteDN: "DIMonitor_MsCompleteDN",
        SRVehicleRevert: "DIMonitor_MsRevert",
        SRVehicleRevertDN: "DIMonitor_MsRevertDN",
        SRMasterDNGet: "DIMonitor_VehicleTimeGet",
        MSTroubleList: "DIMonitor_MsTroubleList",
        MSTroubleUpdate: "DIMonitorTrouble_Save",
        MSTroubleUpdateAll: "DIMonitorTrouble_SaveAll",
        MSTroubleDelete: "DIMonitorTrouble_Delete",
        MSTroubleGet: "DIMonitorTrouble_Get",
        MSRoutingList: "DIMonitor_MsRoutingList",
        TroubleList: "DIMonitor_CATTroubleList",
        TroubleSaveList: "DIMonitorTrouble_SaveList",
        SRWinToUpdate: "DIMonitor_MsUpdate",
        SRWinToDITOList: "DIMonitor_MsDITOList",
        SRWinToDITOUpdate: "DIMonitor_MsDITOUpdate",
        SRWinToDITOLocationList: "DIMonitor_MsDITOLocationList",
        DriverList: "Monitor_DriverList",
        VendorList: "Monitor_VendorList",
        TruckList: "Monitor_TruckList",
        LocationList: "Monitor_LocationList",
        TroubleNotin_List: "DIMonitorTrouble_NotinList",
        GOPReturn_List: "DIMonitorMaster_GOPReturnList",
        GOPReturn_Save: "DIMonitorMaster_GOPReturnSave",
        CUSGOP_List: "DIMonitorMaster_CUSGOPList"
    },
    Data: {
        _trouble1stStatus: 0,

    },
    Map: {
        Map: null,
        Marker: [],
        MapInfo: null
    },
}

angular.module('myapp').controller('MONMonitor_ControlTowerCtrlDI', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', '$interval', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile, $interval) {
    Common.Log('MONMonitor_ControlTowerCtrlDI');
    $rootScope.IsLoading = false;

    if ($rootScope.IsPageComplete != true) return;
    $scope.Auth = $rootScope.GetAuth();

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
    $scope.CurrentMaster = null;
    $scope._dataVendor = [];
    $scope._dataTruck = [];
    $scope.CurrentMaster = null;
    $scope.Display_Tab = {
        Trans: true,
        Return: true,
        Location: true,
        Trouble: true,
        SL: true,
        Station: true,
    }
    $scope._mapMarkers = [];
    $scope.Marker = [];
    $scope.Show_Gantt_Info = false;
    $scope.VehicleLogItem = {};
    $scope.Color_Series = ["#3f51b5", "#03a9f4", "#41aa45", "#ff9800"];
    $scope.Item = {};
    $scope.MONCT_OPSSummary = {
        TotalCBM: 0,
        TotalTon: 0,
        TotalTrip: 0,
        TenderTrip: 0,
        TenderCBM: 0,
        TenderTon: 0,
        ReceivedCBM: 0,
        ReceivedTon: 0,
        ReceivedTrip: 0,
        DeliveryCBM: 0,
        DeliveryTon: 0,
        DeliveryTrip: 0,
    };
    $scope.ObjLocation = {
        Country: [],
        Province: [],
        District: []
    };
    $scope.SOItem = {
        CustomerID: 0,
    }
    $scope.FilterLoading = false;
    $scope.Show_Filter_Status = false;
    $scope.ShowTimelineActual = false;
    $scope.Filter_Status_Position = {
        Left: 0,
        Top: 0
    }
    $scope.Filter_Html = '<div ng-click="OrderCheckbox(1)" ng-class="{filteractived:MONCT_Filter.Going}" class="filter-status-row"><div class="filter-status-cell"><div class="dot orange"></div></div><div class="filter-status-cell" style="width:80%">Xe đang chạy</div></div>' +
                         '<div ng-click="OrderCheckbox(2)" ng-class="{filteractived:MONCT_Filter.Complete}" class="filter-status-row"><div class="filter-status-cell"><div class="dot green"></div></div><div class="filter-status-cell" style="width:80%"> Xe đã hoàn thành</div> </div>';

    $scope.Gantt_Selected_View = 1;
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
    //$scope.Init_CK();
    //#endregion

    $scope.btnTest = function (e) {
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

    //#region Options
    $scope.orien = "horizontal";
    $scope.MainSplitterClass = "hor-splitter";
    $scope.ButtonViewClass = "view3";
    if (Common.HasValue($state.params.orien)) {
        $scope.orien = "vertical";
        $scope.MainSplitterClass = "ver-splitter";
        $scope.ButtonViewClass = "view2";
    }
    $scope.MONDI_SpitterOptions = {
        orientation: $scope.orien,
        panes: [
                { collapsible: true, resizable: true, size: '0%' },
                { collapsible: true, resizable: true, size: '100%' }
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
        filter: ".ct-warning",
        position: "top",
        content: function (e) {
            var grid = e.target.closest(".k-grid").getKendoGrid();
            var dataItem = grid.dataItem(e.target.closest("tr"));
            return "Chuyến có " + dataItem.WarningCount + " chi phí phát sinh";

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

    $scope.filterWindowOptions = {
        title: "Tìm theo cung đường",
        position: { top: 120, left: "20%" },
        actions: ["Minimize", "close"],
        height: 200, width: 320,
    };

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
        $scope.IsFullScreen = false;
    }
    //#endregion

    //#region Tab order

    $scope.MONCT_Filter = {
        DateFrom: new Date().addDays(-7),
        DateTo: new Date(),
        Going: true,
        Complete: true,
        IsRecieved: true
    };

    //#region Load Cookie

    var ck = Common.Cookie.Get('MON_OrderFilter');
    if (Common.HasValue(ck)) {
        try {
            var obj = JSON.parse(ck);
            $scope.MONCT_Filter.Going = obj.Going;
            $scope.MONCT_Filter.Complete = obj.Complete;
        }
        catch (e) { }
    }
    //#endregion

    $scope.ct_OrderSplitterOptions = {
        panes: [
                { collapsible: true, resizable: true, size: '50%' },
                { collapsible: true, resizable: true, size: '50%', collapsed: false }
        ],
    }

    $scope.ct_order_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_OrderList",
            readparam: function () {
                return {
                    DateFrom: Common.Date.Date($scope.MONCT_Filter.DateFrom.addDays(-1)),
                    DateTo: Common.Date.Date($scope.MONCT_Filter.DateTo.addDays(1)),
                    isRunning: $scope.MONCT_Filter.Going,
                    isComplete: $scope.MONCT_Filter.Complete
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ColorClass: { type: 'string' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
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
                template: '<a ng-click="BtnEditOrder($event,dataItem.DITOMasterID)" class="k-button"><i class="fa fa-pencil"></i></a>',
                //headerTemplate: '<div class="filter-bar"><div class="filter-bar-row"><div ng-click="OrderCheckbox(1)" ng-class="{none:!MONCT_Filter.Going,going:MONCT_Filter.Going}" class="filter-bar-cell"></div><div ng-click="OrderCheckbox(2)" ng-class="{none:!MONCT_Filter.Complete,complete:MONCT_Filter.Complete}" class="filter-bar-cell"></div></div></div>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
           {
               field: 'OrderCode', width: 100,
               title: '{{RS.ORDOrder.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'WarningCount', width: 100,
               title: '{{RS.MONMonitorIndex.WarningCount}}', attributes: { 'style': 'text-align: center;' },
               template: '<img class="ct-warning" ng-click="DN_Click($event,dataItem,true)"  ng-show="dataItem.WarningCount>0" src="images/function/ico_warning_active.png" /><img ng-show="dataItem.WarningCount<=0" ng-click="DN_Click($event,dataItem,true)" style="vertical-align:middle;cursor:pointer;" src="images/function/ico_warning.png" />',
               filterable: false
           },
           {
               field: 'VehicleNo', width: 150, sortable: false,
               title: '{{RS.CATVehicle.RegNo}}',
               headerTemplate: '<div>{{RS.CATVehicle.RegNo}} <img class="img-filter" ng-click="Filter_Status_Click($event)" src="images/function/ico_filter_down.png" />  <br/><div class="filter-status" ng-mouseleave="Filter_Status_Hide($event)" ng-style="{left: Filter_Status_Position.Left, top: Filter_Status_Position.Top}" ng-show="Show_Filter_Status">' + $scope.Filter_Html + '</div></div>',
               template: '<img ng-click="VehicleHistory(dataItem.DITOMasterID)" ng-show="dataItem.IsDNComplete" class="order-grid-img" src="images/function/ico_xe_done.png" /><img ng-click="VehicleHistory(dataItem.DITOMasterID)" ng-show="!dataItem.IsDNComplete" class="order-grid-img" src="images/function/ico_xe_going.png" />#=VehicleNo != null ? VehicleNo : ""#',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DITOMasterCode', width: 100,
               title: '{{RS.OPSDITOMaster.Code}}',
               template: '<a ng-click="DN_Click($event,dataItem)" title="Xem chi tiết" style="cursor:pointer;font-weight: bold;">#=DITOMasterCode != null ? DITOMasterCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'VendorCode', width: 100,
               title: '{{RS.CUSCustomer.VendorCode}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'VendorName', width: 100,
               title: '{{RS.CUSCustomer.VendorName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'CustomerCode', width: 100,
               title: '{{RS.CUSCustomer.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'CustomerName', width: 100,
               title: '{{RS.CUSCustomer.CustomerName}}',
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
               field: 'Ton', width: 70,
               title: '{{RS.OPSDITOGroupProduct.Ton}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'CBM', width: 70,
               title: '{{RS.OPSDITOGroupProduct.CBM}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Quantity', width: 70,
               title: '{{RS.OPSDITOGroupProduct.Quantity}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DNCode', width: 100,
               title: '{{RS.OPSDITOGroupProduct.DNCode}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'SOCode', width: 100,
               title: '{{RS.ORDGroupProduct.SOCode}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'GroupOfProductName', width: 100,
               title: '{{RS.CUSGroupOfProduct.GroupName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToProvince', width: 100,
               title: '{{RS.CATProvince.ProvinceName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToDistrict', width: 100,
               title: '{{RS.CATDistrict.DistrictName}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToAddress', width: 100,
               title: '{{RS.CATLocation.Address}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'RequestDate', width: 120,
               template: '#=Common.Date.FromJsonDMY(RequestDate)#',
               filterable: { cell: { operator: 'gte', showOperators: false } }
           },
           {
               field: 'LocationToCode', width: 100,
               title: '{{RS.CUSLocation.Code}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToName', width: 150,
               title: '{{RS.CATLocation.Location}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Description', width: 150,
               title: '{{RS.ORDGroupProduct.Description}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DriverName', width: 100,
               title: '{{RS.OPSDITOMaster.DriverName1}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DriverTel', width: 100,
               title: '{{RS.OPSDITOMaster.DriverTel1}}',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            //var grid = this;
            //var rows = grid.tbody.find('tr');

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
            { field: 'MasterCode', title: 'Mã chuyến', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'ATD', title: 'Ngày bắt đầu', width: 150,
                template: '<input style="width:99%;" class="cus-datepicker" focus-k-datetimepicker kendo-date-time-picker k-ng-model="dataItem.ATD" name="ATD" k-options="DateDMYHM" />',
            },
            {
                field: 'ATA', title: 'Ngày kết thúc', width: 150,
                template: '<input style="width:99%;" class="cus-datepicker" focus-k-datetimepicker kendo-date-time-picker k-ng-model="dataItem.ATA" name="ATA" k-options="DateDMYHM" />',
            },
        ]
    };

    $scope.ct_order_gridChange = function (e) {

    }

    $scope.OrderCheckbox = function (i) {
        $scope.MONCT_Filter.IsRecieved = !$scope.MONCT_Filter.IsRecieved;
        switch (i) {
            case 1:
                $scope.MONCT_Filter.Going = !$scope.MONCT_Filter.Going;
                break;
            case 2:
                $scope.MONCT_Filter.Complete = !$scope.MONCT_Filter.Complete;
                break;
        }
        $scope.ct_order_grid.dataSource.read();
        Common.Cookie.Set('MON_OrderFilter', JSON.stringify($scope.MONCT_Filter));
    }

    $scope.OpenMasterCompleteConfirm_Click = function (e) {
        e.preventDefault();
        var data = [];
        var lstCheck = [];
        angular.forEach($scope.ct_order_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && !o.IsComplete && lstCheck[o.DITOMasterID] != true) {
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
                    MasterCode: o.DITOMasterCode,
                    MasterID: o.DITOMasterID,
                    ATD: atd,
                    ATA: ata,
                })
                lstCheck[o.DITOMasterID] = true;
            }
        })

        $scope.MasterActualTime_Grid.dataSource.data(data);
        $timeout(function () {
            $scope.MasterActualTime_Grid.refresh();
        }, 300)

        $scope.MasterCompleteConfirm_Win.center().open();
    }

    $scope.Complete_Master = function ($event, win) {
        Common.Log("Complete_Master");
        $event.preventDefault();

        var data = $scope.MasterActualTime_Grid.dataSource.data();
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh chuyến?',
                pars: {},
                Ok: function (pars) {

                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONCT_Index.URL.SRVehicleComplete,
                        data: {
                            lst: data
                        },
                        success: function (res) {
                            win.close();
                            $scope.ct_order_grid.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });

        }
        else {
            $rootScope.Message({ Msg: 'Hãy chọn chuyến chưa hoàn thành!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Complete_DN = function ($event) {
        Common.Log("Complete_DN");
        var lstID = [];
        angular.forEach($scope.ct_order_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && !o.IsDNComplete)
                lstID.push(o.ID);
        })
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh DN?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONCT_Index.URL.SRVehicleCompleteDN,
                        data: { lstID: lstID },
                        success: function (res) {
                            $scope.ct_order_grid.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Hãy chọn chuyến chưa hoàn thành!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Revert_Master = function ($event) {
        Common.Log("Revert_Master");
        var lstMasterID = [];
        angular.forEach($scope.ct_order_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                lstMasterID.push(o.DITOMasterID);
        })
        if (lstMasterID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận trả về trạng thái kế hoạch?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONCT_Index.URL.SRVehicleRevert,
                        data: { lstMasterID: lstMasterID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.ct_order_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Không có chuyến để trả về!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Revert_MasterDN = function ($event) {
        Common.Log("Revert_MasterDN");
        var lstID = [];
        angular.forEach($scope.ct_order_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && o.IsDNComplete)
                lstID.push(o.ID);
        })
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận trả về trạng thái kế hoạch?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONCT_Index.URL.SRVehicleRevertDN,
                        data: { lstID: lstID },
                        success: function (res) {
                            $scope.ct_order_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Không có chuyến để trả về!', NotifyType: Common.Message.NotifyType.ERROR });
        }

    }

    $scope.BtnEditOrder = function (e, masterID) {
        e.preventDefault();

        $scope.LoadMasterDetail(masterID);
    }

    $scope.LoadMasterDetail = function (masterID) {
        $rootScope.Loading.Show("Thông tin vận chuyển...");
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONCT_Index.URL.SRMasterDNGet,
            data: { id: masterID },
            success: function (res) {
                $rootScope.Loading.Change("Thông tin vận chuyển...", 100);
                $scope.TO_Win.center().open();
                $scope.CurrentMaster = res;

                $scope.masterID = res.ID;
                $scope.TO_Win_Title = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentMaster.Code + ' - ETD: ' + $scope.CurrentMaster.ETD + ' ETA: ' + $scope.CurrentMaster.ETA;

                angular.forEach($scope.ListTypeOfDriver, function (o, i) {
                    var lst = [];
                    angular.forEach(res.ListDriver, function (o2, i2) {
                        if (o.ValueInt == o2.TypeOfDriverID) {
                            lst.push(o2.Name);
                        }
                    });
                    o.ListName = lst.join(', ');
                });

                if ($scope.CurrentMaster.IsVehicleVendor) {
                    $scope.Show_DriverGrid = true;
                } else {
                    $scope.Show_DriverGrid = false;
                }

                //load cung duong
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONCT_Index.URL.MSRoutingList,
                    data: { masterID: $scope.masterID, locationID: null },
                    success: function (res) {
                        $scope._dataMsRouting = res.Data;
                        $scope.Routing_CbbOptions.dataSource.data(res.Data);
                    }
                })
                //load regno
                var vendorID = $scope.CurrentMaster.VendorID > 0 ? $scope.CurrentMaster.VendorID : -1;

                $scope.LoadGOPReturnData();

            }
        })
    }

    $rootScope.$watch('Loading.Progress', function (n, o) {
        if ($rootScope.Loading.Progress >= 100) {
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    })

    var interval_real_route = null;
    $scope.IntervalDelay = 10;
    $scope.stopInterval = function (stop) {
        if (Common.HasValue(stop)) {
            $interval.cancel(stop);
            stop = undefined;
        }
    };

    $scope.DN_Click = function ($event, dataItem, showRight) {
        $event.preventDefault();
        $scope.ShowOrderStatusPane = true;
        $scope.MasterItem = dataItem;
        $scope.ct_order_grid.select($event.target.closest('tr'));
        $scope.stopInterval(interval_real_route);

        if (_isLoadMap) {
            openMapV2.ClearVector("VectorMarker");
            openMapV2.ClearVector("VectorRoute");

            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_OrderLogList",
                data: { masterID: $scope.MasterItem.DITOMasterID },
                success: function (res) {
                    $scope.OrderLogData = res.LstTroubleLog;
                    var vehicleCode = res.GPSCode;
                    var dtfrom = res.ATD;
                    var dtto = res.ATA;
                    if (Common.HasValue(dtfrom)) {
                        if (!Common.HasValue(dtto))
                            dtto = new Date();


                        $scope.IntervalFunction = function () {
                            openMapV2.ClearVector("VectorRealRoute");
                            openMapV2.ClearVector("VectorXe");
                            Common.Services.Call($http, {
                                url: Common.Services.url.MON,
                                method: "Extend_VehiclePosition_Get",
                                data: { vehicleCode: vehicleCode, dtfrom: dtfrom, dtto: dtto },
                                success: function (res) {
                                    for (var i = 0; i < res.length - 1; i++) {
                                        var l1 = res[i];
                                        var l2 = res[i + 1];

                                        var style = openMapV2.NewStyle.Line(4, 'rgba(150,50,50, 0.7)', [15, 10], "", '#fff');
                                        if (Common.HasValue(l1) && Common.HasValue(l2)) {
                                            openMapV2.NewPolyLine([openMapV2.NewPoint(l1.Lat, l1.Lng), openMapV2.NewPoint(l2.Lat, l2.Lng)], 1, "", style, {}, "VectorRealRoute")

                                        }
                                    }

                                    if (res.length > 0) {
                                        var item = res[res.length - 1];
                                        openMapV2.NewMarker(item.Lat, item.Lng, "con", vehicleCode, openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Truck), 1), {}, "VectorXe")
                                    }
                                }
                            });
                        }
                        $scope.IntervalFunction();
                        if (res.IsComplete) {
                            $scope.IntervalFunction = null;
                        }
                        $scope.SetInterval();
                    }
                }
            })

            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_GetLocationByMaster",
                data: { masterID: $scope.MasterItem.DITOMasterID },
                success: function (res) {
                    $scope.CurrentMasterLocation = res;
                    $scope.DrawNewMarker($scope.CurrentMasterLocation, function (item) {

                        var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Start), 1);

                        if (item.LocationType == "delivery")
                            icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Delivery), 1);
                        if (item.LocationType == "get")
                            icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Get), 1);

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
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_OrderLogList",
                data: { masterID: $scope.MasterItem.DITOMasterID },
                success: function (res) {
                    $scope.OrderLogData = res.LstTroubleLog;
                }
            })
        }

        if (showRight == true)
            $scope.ct_OrderSplitter.expand(".k-pane:last");
    }

    $scope.ToggleSplitter = function (e, sender) {
        e.preventDefault();
        sender.toggle("#order-pane-left", false);

    }

    $scope.ShowTroubleOnMap = function (e, id) {
        e.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_GetTroubleLocation",
            data: { troubleID: id },
            success: function (res) {
                openMapV2.ClearMap();
                if (res.Lat != 0 && res.Lng != 0 && res.Lat != null && res.Lng != null) {
                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewEmpty(openMapV2.NewImage.Color.Dark)), 1, '', '#cecece', true);
                    $timeout(function () {
                        openMapV2.NewMarker(res.Lat, res.Lng, 1, "", icon, res, "VectorMarker");
                        $timeout(function () {
                            openMapV2.FitBound("VectorMarker", 20);
                        }, 10)
                    }, 10)
                }
            }
        });

    }


    //#endregion

    //#region TODtail popup
    $scope.TO_WinOptions = {
        width: '1025px', height: '550px',
        draggable: true, modal: true, resizable: false, title: false,
        open: function () {
            $timeout(function () {
                $rootScope.Loading.Show();
                $scope.Return_Grid.dataSource.read();
                $scope.Trouble_Grid.dataSource.read();
                $scope.SL_Grid.dataSource.read();
                $scope.Location_Grid.dataSource.read();
                $scope.Station_Grid.dataSource.read();
                $scope.TO_Splitter.resize();
            }, 100)
        },
        close: function () {
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

    $scope.TO_SplitterOptions = {
        orientation: 'vertical',
        panes: [
            { collapsible: false, resizable: false, size: '130px' },
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
    };

    $scope.Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONCT_Index.URL.SRWinToDITOLocationList,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SortOrderReal: { type: 'number' },
                    SortOrder: { type: 'number', editable: false },
                    LocationAddress: { type: 'text' },
                    DateCome: { type: 'date' },
                    DateLeave: { type: 'date' },
                    LoadingStart: { type: 'date' },
                    LoadingEnd: { type: 'date' }
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: false, autoBind: false,
        //toolbar: kendo.template($('#win-to-location-grid-toolbar').html()),
        dataBound: function (e) {
            $scope.LocationData = this.dataSource.data();
            $rootScope.Loading.Change("Thông tin địa điểm...", $rootScope.Loading.Progress + LoadingStep);
        },
        columns: [
           { field: 'SortOrder', title: '{{RS.OPSDITOLocation.SortOrder}}', width: "40px", },
           { field: 'SortOrderReal', width: 80, title: '{{RS.OPSDITOLocation.SortOrderReal}}', },
           { field: 'TypeOfTOLocationName', width: 80, title: '{{RS.TypeOfTOLocationName}}', },
           { field: 'LocationAddress', width: 200, title: '{{RS.OPSDITOLocation.LocationAddress}}', },
           { field: 'LocationProvince', title: '{{RS.CATProvince.ProvinceName}}', width: "100px", },
           { field: 'Location', title: '{{RS.CATDistrict.DistrictName}}', width: "100px", },
           { field: 'DITOLocationStatusName', title: '{{RS.OPSDITOLocation.DITOLocationStatusName}}', width: "100px", },
           { field: 'DateCome', width: 170, title: '{{RS.OPSDITOLocation.DateCome}}', template: '#=Common.Date.FromJsonDMYHM(DateCome)#' },
           { field: 'DateLeave', width: 170, title: '{{RS.OPSDITOLocation.DateLeave}}', template: '#=Common.Date.FromJsonDMYHM(DateLeave)#' },
           { field: 'LoadingStart', width: 105, title: '{{RS.OPSDITOLocation.LoadingStart}}', template: '#=Common.Date.FromJsonHM(LoadingStart)#' },
           { field: 'LoadingEnd', width: 100, title: '{{RS.OPSDITOLocation.LoadingEnd}}', template: '#=Common.Date.FromJsonHM(LoadingEnd)#' },
           { field: 'Temp', width: 100, title: '{{RS.OPSDITOLocation.Temp}}', },
           { field: 'Comment', width: 300, title: '{{RS.OPSDITOLocation.Comment}}', },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    //#region trouble

    $scope.TroubleDriverCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'DriverName',
        dataValueField: 'ID',
        change: function (e) {
            var grid = $scope.Trouble_Grid;
            var item = grid.dataItem($(e.sender.wrapper).closest('tr'));
            if (Common.HasValue(item)) {
                item.DriverName = this.text();
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "Monitor_DriverList",
        data: {},
        success: function (res) {
            $scope.TroubleDriverCbbOptions.dataSource.data(res.Data);
        }
    })

    $scope.Trouble_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONCT_Index.URL.MSTroubleList,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    DITOID: { type: 'number', editable: true },
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
            e.model.DITOMasterID = $scope.masterID;
        },
        toolbar: kendo.template($('#win-to-trouble-grid-toolbar').html()), editable: 'incell', columns: [
           {
               title: ' ', width: '155px',
               template: ' ' +
                   '<a href="/" ng-show="Auth.ActEdit" ng-click="Trouble_Delete($event)" class="k-button"><i class="fa fa-trash"></i></a>' +
                   '<a href="/" ng-show="Auth.ActEdit" ng-click="OpenFile_Click($event,winfile,dataItem)" class="k-button"><i class="fa fa-paperclip"></i>Đính kèm</a>',
               filterable: false, sortable: false
           },
           {
               field: 'GroupOfTroubleName', width: 150, title: '{{RS.CATGroupOfTrouble.Name}}',
           },
           {
               field: 'Description', title: '{{RS.CATTrouble.Description}}', width: "150px",
           },
           {
               field: 'Cost', width: 100, title: '{{RS.CATTrouble.Cost}}', editor: function (container, options) {
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
               field: 'DITOID', width: 100, title: '{{RS.CATTrouble.DITOID}}', template: '#=RoutingName == null ? "" : RoutingName #', editor: function (container, options) {
                   var input = $("<input kendo-combobox k-options='Routing_CbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'TroubleCostStatusID', width: 130, title: '{{RS.CATTrouble.TroubleCostStatusName}}', template: '#=TroubleCostStatusName#', editor: function (container, options) {
                   if (options.model.TroubleCostStatusID == 0)
                       options.model.TroubleCostStatusID = _MONCT_Index.Data._trouble1stStatus;
                   var input = $("<input kendo-combobox k-options='TroubleCostStatus_CbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $rootScope.Loading.Change("Chi phí phát sinh...", $rootScope.Loading.Progress + LoadingStep);
        }

    };

    $scope.Trouble_AddNew = function ($event, win, grid) {
        $event.preventDefault();
        win.center().open();
        $scope.ItemDetail.GroupOfTroubleID = $scope.GroupOfTrouble_CbbOptions.dataSource.data()[0].ID;
        $scope.ItemDetail.Cost = 0;
        $scope.ItemDetail.CostOfCustomer = 0;
        $scope.ItemDetail.CostOfVendor = 0;
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
                method: _MONCT_Index.URL.MSTroubleUpdateAll,
                data: { data: data },
                success: function (res) {
                    grid.dataSource.read();
                    $scope.ct_order_grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.Trouble_Save = function ($event, vform, win) {
        $event.preventDefault();
        $scope.ItemDetail.DITOMasterID = $scope.masterID;
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONCT_Index.URL.MSTroubleUpdate,
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
            method: _MONCT_Index.URL.MSTroubleUpdate,
            data: { item: item },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.Trouble_Grid.dataSource.read();
            }
        })
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
                    method: _MONCT_Index.URL.MSTroubleDelete,
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

    $scope.ChangeProblemCost = function () {
        $scope.ItemDetail.CostOfVendor = $scope.ItemDetail.Cost;
    }

    $scope.ChangeProblemCostGrid = function (o) {
        o.CostOfVendor = o.Cost;
    }

    //#endregion

    //#region sl grid
    $scope.SLMenuOptions = {
        dataSource: [
            {
                text: '<i class="fa fa-cog" aria-hidden="true"></i>',
                encoded: false,
                cssClass: "ct-no-border",
                items: [
                    { text: "<span ng-click='EditSOLocation($event)'><i class='fa fa-map-marker'></i> Thay đổi địa điểm</span>", encoded: false, },
                    { text: "<span ng-click='NewSO($event,NewSO_Win)'><i class='fa fa-plus'></i> Thêm hàng tại kho</span>", encoded: false, },
                    { text: "<span ng-click='OpenSplitDN_Click($event,SplitDN_Win)'><i class='fa fa-columns'></i> Tách DN</span>", encoded: false, },
                    { text: "<span ng-click='CancelSO_Click($event,CancelSO_Win,1)'><i class='fa fa-trash'></i> Hủy DN</span>", encoded: false, },
                    { text: "<span ng-click='CancelSO_Click($event,CancelSO_Win,2)'><i class='fa fa-scissors'></i> Hủy 1 phần</span>", encoded: false, },
                    { text: "<span ng-click='CancelSO_Click($event,CancelSO_Win,3)'><i class='fa fa-recycle'></i> Trả về</span>", encoded: false, },
                    { text: "<span ng-click='Return_Add($event,GOPReturn_EditWin)'><i class='fa fa-plus'></i> Thêm hàng trả về</span>", encoded: false, },
                ]
            }
        ],
        openOnClick: true,
    }
    $scope.SL_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "DIMonitorMaster_SL_List",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    STT: { type: 'string' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    HasCashCollect: { type: 'bool', editable: false },
                    OrderCode: { type: 'string', editable: false },
                    SOCode: { type: 'string', editable: false },
                    DNCode: { type: 'string', editable: true },
                    Quantity: { type: 'number', editable: true },
                    ProductName: { type: 'string', editable: false },
                    CustomerName: { type: 'string', editable: false },
                    LocationToName: { type: 'string', editable: false },
                    LocationToProvince: { type: 'string', editable: false },
                    LocationToDistrict: { type: 'string', editable: false },
                    LocationToAddress: { type: 'string', editable: false },
                    GroupProductName: { type: 'string', editable: false },
                    GroupProductID: { type: 'number', editable: false },
                    ModifiedBy: { type: 'string', editable: false },
                    ModifiedDate: { type: 'date', editable: false },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        toolbar: kendo.template($('#sl-grid-toolbar').html()),
        height: '99%', pageable: Common.PageSize, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: true, autoBind: false,
        columns: [
            {
                field: '', width: 60, title: '',
                template: '<ul kendo-menu k-options="SLMenuOptions" style="border:none;"></ul>',
                attributes: {
                    style: "overflow: visible;border-right: none;"
                },
                filterable: false
            },
            {
                field: '{{RS.ORDGroupProduct.HasCashCollect}}', width: 50, title: 'TH',
                template: '<input ng-readonly="Auth.ActEdit" type="checkbox" #= HasCashCollect ? \'checked="checked"\' : "" # ng-model="dataItem.HasCashCollect" ng-click="HasCashCollectChange($event,dataItem)" />',
                filterable: false
            },
            {
                title: '{{RS.ORDOrder.Code}}', field: 'OrderCode', width: "100px",
            },
            {
                title: '{{RS.CUSGroupOfProduct.GroupName}}', field: 'GroupProductID', template: '#=GroupProductName#', width: "150px"
            },
            {
                field: 'ProductID', width: 100, title: '{{RS.CUSProduct.Code}}', template: '#=ProductCode == null ? "" : ProductCode #', editor: function (container, options) {

                    var input = $("<input kendo-combobox k-options='ProductGridLineCbbOptions'/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "DIMonitor_GroupProductOfTOGroup",
                        data: {
                            TOGroupID: options.model.OPSGroupProductID,
                            productID: -1,
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            var lst = [];
                            angular.forEach(res, function (o, i) {
                                if (o.GroupProductID == options.model.GroupProductID) {
                                    lst.push(o);
                                }
                            })
                            $scope.ProductGridLineCbbOptions.dataSource.data(lst);
                        }
                    })
                }
            },
            {
                title: '{{RS.OPSDITOProduct.Quantity}}', field: 'Quantity', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="Quantity">');
                    input.appendTo(container);
                },
            },
            {
                title: '{{RS.OPSDITOProduct.QuantityTransfer}}', field: 'QuantityTranfer', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="QuantityTranfer">');
                    input.appendTo(container);
                    input.change(function (e) {
                        var grid = $scope.SL_Grid;
                        var item = grid.dataItem(this.closest('tr'));
                        if (this.value < item.QuantityReturn + item.QuantityBBGN) {
                            item.QuantityReturn = 0; item.QuantityBBGN = this.value;
                            $rootScope.Message({ Msg: 'Lỗi [SL giao] + [SL Trả về] > [SL chở]', NotifyType: Common.Message.NotifyType.ERROR });

                        }
                    })
                },
            },
            {
                title: '{{RS.OPSDITOProduct.QuantityBBGN}}', field: 'QuantityBBGN', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="QuantityBBGN">');
                    input.appendTo(container);
                    var co = container;
                    input.change(function (e) {
                        var grid = $scope.SL_Grid;
                        var item = grid.dataItem(this.closest('tr'));
                        if (this.value <= item.QuantityTranfer) {
                            item.QuantityReturn = item.QuantityTranfer - this.value;
                        }
                        else {
                            this.value = item.QuantityTranfer;
                            item.QuantityReturn = 0;
                            $rootScope.Message({ Msg: 'SL giao không được lớn hơn SL chở', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    })
                },
            },
            {
                title: '{{RS.OPSDITOProduct.QuantityReturn}}', field: 'QuantityReturn', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="QuantityReturn">');
                    input.appendTo(container);
                },
            },
            {
                title: '{{RS.OPSDITOGroupProduct.DNCode}}', field: 'DNCode', width: "120px"
            },
            {
                title: '{{RS.OPSDITOGroupProduct.VendorLoadName}}', field: 'VendorLoadID', template: '#=VendorLoadName == null ? "" : VendorLoadName#', width: "150px", editor: function (container, options) {
                    var input = $("<input focus-k-combobox kendo-combobox k-options='Vendor_TOLoadCbbOptions'/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                }
            },
            {
                title: '{{RS.OPSDITOGroupProduct.VendorUnLoadName}}', field: 'VendorUnLoadID', template: '#=VendorUnLoadName == null ? "" : VendorUnLoadName#', width: "150px", editor: function (container, options) {
                    var input = $("<input focus-k-combobox kendo-combobox k-options='Vendor_TOUnLoadCbbOptions'/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                }
            },
            {
                title: '{{RS.CUSCustomer.Code}}', field: 'CustomerName', width: 100,
            },
            {
                title: '{{RS.CUSLocation.LocationName}}', field: 'LocationToName', width: "125px",
            },
            {
                title: '{{RS.CATProvince.ProvinceName}}', field: 'LocationToProvince', width: "125px",
            },
            {
                title: '{{RS.CATDistrict.DistrictName}}', field: 'LocationToDistrict', width: "125px",
            },
            {
                title: '{{RS.CATLocation.Address}}', field: 'LocationToAddress', width: "200px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.Note}}', field: 'Note', width: "150px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.Note1}}', field: 'Note1', width: "150px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.Note2}}', field: 'Note2', width: "150px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.Price}}', field: 'Price', width: "100px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.ModifiedBy}}', field: 'ModifiedBy', width: "100px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.ModifiedDate}}', field: 'ModifiedDate', width: "100px", template: '#=Common.Date.FromJsonDMYHM(ModifiedDate)#',
            },
            { title: ' ', filterable: false, sortable: false }
        ],
        reorderable: true,
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin đơn hàng...", $rootScope.Loading.Progress + LoadingStep);
        }
    };

    var tOPSGroupID = 0;
    $scope.PopupSOType = 1;
    $scope.CancelSONote = "";
    $scope.CancelSOID = "";
    $scope.CancelSOQuantity = "";

    $scope.ProductGridLineCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'ProductCode',
        dataValueField: 'ID',
        change: function (e) {
            var grid = $scope.SL_Grid;
            var item = grid.dataItem($(e.sender.wrapper).closest('tr'));
            if (Common.HasValue(item)) {
                item.ProductCode = this.text();
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    $scope.CancelSO_Click = function (e, win, type) {
        e.preventDefault();
        var item = $scope.SL_Grid.dataItem(e.target.closest('tr'));
        var groupid = item.OPSGroupProductID;

        switch (type) {
            case 1:
                $scope.CancelSO_Title = "Lý do hủy DN";
                break;
            case 2:
                $scope.CancelSO_Title = "Lý do hủy một phần DN";
                break;
            case 3:
                $scope.CancelSO_Title = "Trả một phần DN về điều phối";
                break;
        }

        $scope.CancelSONote = "";
        $scope.CancelSOID = "";
        $scope.CancelSOQuantity = "";
        tOPSGroupID = groupid;
        $scope.PopupSOType = type;
        win.center().open();
    }

    $scope.OpenSplitDN_Click = function (e, win) {
        e.preventDefault();
        var item = $scope.SL_Grid.dataItem(e.target.closest('tr'));
        var groupid = item.OPSGroupProductID;

        $scope.SplitDN_Quantity = item.Quantity;
        $scope.SplitDN_Quantity1 = item.Quantity;
        $scope.SplitDN_Quantity2 = 0;
        tOPSGroupID = groupid;
        if ($scope.SplitDN_Quantity1 > 0)
            win.center().open();
        else
            $rootScope.Message({ Msg: 'Sản lượng DN quá nhỏ để thực hiện thao tác này', NotifyType: Common.Message.NotifyType.ERROR });
    }

    $scope.SplitDNChange = function () {
        if ($scope.SplitDN_Quantity2 >= $scope.SplitDN_Quantity) {
            $scope.SplitDN_Quantity2 = 0;
            $scope.SplitDN_Quantity1 = $scope.SplitDN_Quantity;
            $rootScope.Message({ Msg: 'Số lượng vượt vượt quá mức cho phép', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $scope.SplitDN_Quantity1 = $scope.SplitDN_Quantity - $scope.SplitDN_Quantity2;
        }
    }

    $scope.CancelMissingSO_AcceptClick = function (e, win) {
        e.preventDefault();
        if ($scope.CancelSOID > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_CancelMissingProduct",
                data: {
                    opsGroupID: tOPSGroupID,
                    quantity: $scope.CancelSOQuantity,
                    reasonID: $scope.CancelSOID,
                    reasonNote: $scope.CancelSONote,
                },
                success: function (res) {
                    $scope.SL_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
            win.close();
        }
    }

    $scope.ReturnMissingSO_AcceptClick = function (e, win) {
        e.preventDefault();
        if ($scope.CancelSOQuantity > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_ReturnMissingProductToOPS",
                data: {
                    opsGroupID: tOPSGroupID,
                    quantity: $scope.CancelSOQuantity,
                },
                success: function (res) {
                    $scope.SL_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
            win.close();
        }
        win.close();
    }

    $scope.SplitDN_AcceptClick = function (e, win) {
        e.preventDefault();
        if ($scope.SplitDN_Quantity2 > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_SplitDN",
                data: {
                    opsGroupID: tOPSGroupID,
                    quantity: $scope.SplitDN_Quantity2,
                },
                success: function (res) {
                    $scope.ct_order_grid.dataSource.read();
                    $scope.SL_Grid.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
        win.close();
    }

    $scope.AcceptCancel_SO = function ($event, win) {
        $event.preventDefault();
        var reasonID = $scope.CancelSOReason_Cbb.value();
        if (reasonID > 0)
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_CancelTOGroup",
                data: { opsGroupID: tOPSGroupID, note: $scope.CancelSONote, reasonID: reasonID },
                success: function (res) {
                    $scope.TO_Win.close();
                    $scope.CancelSO_Win.close();
                    $scope.ct_order_grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.HasCashCollectChange = function (e, item) {
        e.preventDefault();
        $scope.curTarget = e.target;
        var str = 'Xác nhận thu hộ ?';
        if (!item.HasCashCollect) {
            str = 'Xác nhận ngừng thu hộ ?';
        }
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: str,
            pars: { e: e },
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                $($scope.curTarget).prop('checked', item.HasCashCollect);
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "DIMonitorMaster_UpdateCashCollection",
                    data: { ordGroupID: item.OrderGroupProductID, value: item.HasCashCollect },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.SL_Grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            },
            Close: function () {

            }
        });

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
                    method: "MONControlTower_DIStationApprove",
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

    $scope.EditSOLocation = function (e) {
        e.preventDefault();
        var item = $scope.SL_Grid.dataItem(e.target.closest('tr'));

        $rootScope.IsLoading = true;
        $scope.SOItem = item;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitorMaster_SOPartnerList",
            data: { customerID: $scope.SOItem.CustomerID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.PartnerOfLoationCbb.dataSource.data(res);
                $scope.PartnerOfLoationCbb.value($scope.SOItem.CusPartID);
            }
        })
        $scope.Location_NotinGrid.dataSource.read();
        $scope.Location_NotinWin.center().open();
    }

    $scope.Location_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "DIMonitorMaster_SOPartnerLocation",
            readparam: function () { return { cuspartID: $scope.SOItem.CustomerID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', editable: false },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, selectable: true,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var o = grid.dataItem(tr);
                if (o.ID == $scope.SOItem.LocationToID) {
                    grid.select(tr);
                }
            })
        },
        change: function (e) {
            var selectedRows = this.select();
            var dataItem = this.dataItem(selectedRows[0]);
            $scope.SOItem.LocationToIDNew = dataItem.ID;
        },
        columns: [
            {
                field: 'CusPartrCode', title: '{{RS.CUSPartner.PartnerCode}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Code', title: '{{RS.CUSPartner.PartnerCode}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: '{{RS.CUSLocation.LocationName}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '{{RS.CATLocation.Address}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },

        ]
    }

    $scope.LocationAddClick = function (e) {
        e.preventDefault();

        $scope.Location_EditWin.center().open();
        $scope.LocationData = {};
    }

    $scope.Location_NotinSaveChange = function (e) {
        e.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitorMaster_ChangeSOLocation",
            data: {
                cuslocationID: $scope.SOItem.LocationToIDNew,
                opsGroupID: $scope.SOItem.OPSGroupProductID
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Location_NotinWin.close();
                $scope.SL_Grid.dataSource.read();
                $scope.Location_Grid.dataSource.read();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
    }

    $scope.SOLocation_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_PartnerLocationSave",
                data: { item: $scope.LocationData, cuspartID: $scope.SOItem.CusPartID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Location_NotinGrid.dataSource.read();
                    $scope.Location_EditWin.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Vendor_TOLoadCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'VendorName', dataValueField: 'VendorID', enable: true, value: -1,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'VendorID', fields: { VendorName: { type: 'string' }, VendorID: { type: 'number' } } }
        }),
        change: function (e) {
            var grid = $scope.SL_Grid;
            var currentItem = grid.dataItem($(e.sender.wrapper).closest('tr'));
            currentItem.VendorLoadName = this.text();
            currentItem.VendorLoadID = this.value();

        },
    };

    $scope.Vendor_TOUnLoadCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'VendorName', dataValueField: 'VendorID', enable: true, value: -1,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'VendorID', fields: { VendorName: { type: 'string' }, VendorID: { type: 'number' } } }
        }),
        change: function (e) {
            var grid = $scope.SL_Grid;
            grid.dataItem($(e.sender.wrapper).closest('tr')).VendorUnLoadName = this.text();
            grid.dataItem($(e.sender.wrapper).closest('tr')).VendorUnLoadID = this.value();
        },
    };

    $scope.Location_CountryCbbOptions = {}; $scope.Location_ProvinceCbbOptions = {}; $scope.Location_DistrictCbbOptions = {};
    $scope.CreateCBB = function (CountryOpt, ProvinceOpt, DicstrictOpt, CountryCbb, ProvinceCbb, DistrictCbb, LocalObj) {
        //#region Option
        //  Country
        CountryOpt.autoBind = true;
        CountryOpt.valuePrimitive = true;
        CountryOpt.ignoreCase = true;
        CountryOpt.filter = 'contains';
        CountryOpt.suggest = true;
        CountryOpt.dataTextField = 'CountryName';
        CountryOpt.dataValueField = 'ID';
        CountryOpt.minLength = 2;
        CountryOpt.dataSource = Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        })
        CountryOpt.change = function (e) {
            $scope[ProvinceCbb].open();
            $scope[ProvinceCbb].select(0);
        }
        //  Province
        ProvinceOpt.autoBind = true;
        ProvinceOpt.valuePrimitive = true;
        ProvinceOpt.ignoreCase = true;
        ProvinceOpt.filter = 'contains';
        ProvinceOpt.suggest = true;
        ProvinceOpt.dataTextField = 'ProvinceName';
        ProvinceOpt.dataValueField = 'ID';
        ProvinceOpt.minLength = 2;
        ProvinceOpt.dataSource = Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        });
        ProvinceOpt.change = function (e) {
            var cbb = this;
            for (var i = 0; i < LocalObj.District.length; i++) {
                if (LocalObj.District[i].ProvinceID == cbb.value()) {
                    $scope[DistrictCbb].open();
                    $scope[DistrictCbb].select(0);
                }
            }
        }
        //  District
        DicstrictOpt.autoBind = true;
        DicstrictOpt.valuePrimitive = true;
        DicstrictOpt.ignoreCase = true;
        DicstrictOpt.filter = 'contains';
        DicstrictOpt.suggest = true;
        DicstrictOpt.dataTextField = 'DistrictName';
        DicstrictOpt.dataValueField = 'ID';
        DicstrictOpt.minLength = 2;
        DicstrictOpt.dataSource = Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        })
        DicstrictOpt.open = function (e) {
            var result = $.grep(LocalObj.District, function (o) { return o.ProvinceID == $scope[ProvinceCbb].value(); });
            $scope[DistrictCbb].dataSource.data(result);
        }
        DicstrictOpt.close = function (e) {
            $scope[DistrictCbb].dataSource.data(LocalObj.District);
        }
        //#endregion

        //#region Load data
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: Common.ALL.URL.Country,
            data: {},
            success: function (res) {
                LocalObj.Country = res.Data;
                CountryOpt.dataSource.data(res.Data);
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: Common.ALL.URL.Province,
            data: {},
            success: function (res) {
                LocalObj.Province = res.Data;
                ProvinceOpt.dataSource.data(res.Data);
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: Common.ALL.URL.District,
            data: {},
            success: function (res) {
                LocalObj.District = res.Data;
                DicstrictOpt.dataSource.data(res.Data);
            }
        });

        //#endregion
    }

    $scope.CreateCBB($scope.Location_CountryCbbOptions, $scope.Location_ProvinceCbbOptions, $scope.Location_DistrictCbbOptions, 'Location_CountryCbb', 'Location_ProvinceCbb', 'Location_DistrictCbb', $scope.ObjLocation);

    $scope.PartnerOfLoationCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Text', dataValueField: 'ValueInt',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                Text: { type: 'string' },
                ID: { type: 'number' },
            }
        }),

    }

    $scope.Location_GOLCbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfLocation,
        success: function (data) {
            var dataNew = [];
            dataNew.push({ GroupName: " ", ID: -1 });
            Common.Data.Each(data, function (o) {
                dataNew.push(o);
            })
            $scope.Location_GOLCbbOptions.dataSource.data(dataNew)
        }
    })


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

    //add new SO

    var ProductData = [];
    $scope.NewSOItem = {};

    $scope.GroupProductCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'GroupProductCode',
        dataValueField: 'GroupProductID',
        change: function (e) {
            var val = this.value();
            var lst = [];
            angular.forEach(ProductData, function (o, i) {
                if (o.GroupProductID == val) {
                    lst.push(o);
                }
            })

            $scope.ProductCbb.dataSource.data(lst);
            if (lst.length > 0) {
                $scope.NewSOItem.ID = lst[0].ID;
                //$scope.ProductCbb.value(lst[0].ID);
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    $scope.ProductCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'ProductCode',
        dataValueField: 'ID',
        change: function (e) {

        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    $scope.NewSO = function (e, win) {
        e.preventDefault();
        win.center().open();
        var item = $scope.SL_Grid.dataItem(e.target.closest('tr'));
        $scope.NewSOItem.GroupProductID = "";
        $scope.NewSOItem.ID = "";
        $scope.NewSOItem.OPSGroupProductID = item.OPSGroupProductID;
        $scope.NewSOItem.Quantity = "";

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_GroupProductOfTOGroup",
            data: {
                TOGroupID: item.OPSGroupProductID,
                productID: item.ProductID,
            },
            success: function (res) {
                ProductData = res;

                //tao gp datasource
                var lstGP = [];
                var lstTemp = [];
                var hasGroup = false;
                angular.forEach(res, function (o, i) {
                    if (!Common.HasValue(lstTemp[o.GroupProductID])) {
                        if (item.GroupProductID == o.GroupProductID) {
                            $scope.NewSOItem.GroupProductID = item.GroupProductID;
                            hasGroup = true;
                        }
                        lstGP.push(o);
                        lstTemp[o.GroupProductID] = true;
                    }
                })
                $scope.GroupProductCbb.dataSource.data(lstGP);
                if (!hasGroup) {
                    $scope.NewSOItem.GroupProductID = "";
                }

                //tao product datasource
                var lst = [];
                angular.forEach(ProductData, function (o, i) {
                    if (o.GroupProductID == item.GroupProductID) {
                        lst.push(o);
                    }
                })
                $scope.ProductCbb.dataSource.data(lst);
                if (lst.length > 0) {
                    $scope.ProductCbb.value(lst[0].ID);
                    $scope.NewSOItem.ID = lst[0].ID;
                }
            }
        })

    }

    $scope.AcceptAdd_SO = function (e, win) {
        e.preventDefault();
        if ($scope.NewSO_vForm()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_AddGroupProductFromDN",
                data: {
                    opsGroupID: $scope.NewSOItem.OPSGroupProductID,
                    item: $scope.NewSOItem,
                },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.SL_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }


    //add from non tender

    $scope.DateRequest = {
        fDate: null,
        tDate: null,
    }

    $scope.NonTender_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "DIMonitorMaster_NonTenderList",
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Quantity: { type: 'number', defaultValue: 0 },
                    IsChoose: { type: 'bool' },
                }
            },
            readparam: function () { return { fDate: $scope.DateRequest.fDate, tDate: $scope.DateRequest.tDate } }
        }),
        height: '99%', pageable: true, sortable: false, columnMenu: false, filterable: false, resizable: false, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,NonTender_Grid,NonTender_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,NonTender_Grid,NonTender_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
           {
               title: '{{RS.ORDGroupProduct.Ton}}', field: 'Ton', width: "100px", filterable: false, template: "<input ng-value='dataItem.Ton' ng-blur='DNSplit($event,1)' type='number' class='k-textbox' />",
           },
           {
               title: '{{RS.ORDGroupProduct.CBM}}', field: 'CBM', width: "100px", filterable: false, template: "<input ng-value='dataItem.CBM' ng-blur='DNSplit($event,2)' type='number' class='k-textbox' />",
           },
           {
               title: '{{RS.ORDGroupProduct.Quantity}}', field: 'Quantity', width: "100px", filterable: false, template: "<input ng-value='dataItem.Quantity' ng-blur='DNSplit($event,3)' type='number' class='k-textbox' />",
           },
           {
               title: '{{RS.ORDOrder.Code}}', field: 'OrderCode', width: "100px",
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
            {
                title: '{{RS.CUSGroupOfProduct.GroupName}}', field: 'GroupProductID', template: '#=GroupProductName#', width: "150px",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: '{{RS.CUSCustomer.Code}}', field: 'CustomerName', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: '{{RS.ORDOrder.LocationToName}}', field: 'LocationToName', width: "125px",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],

    }

    $scope.DNSplit = function (e, type) {
        var item = this.dataItem;
        var val = parseFloat(e.target.value);
        var target = e.target;
        var oldVal = 0;
        var isOver = false;
        switch (type) {
            case 1:
                oldVal = item.Ton;
                if (val > item.Ton)
                    isOver = true;
                break;
            case 2:
                oldVal = item.CBM;
                if (val > item.CBM)
                    isOver = true;
                break;
            case 3:
                oldVal = item.Quantity;
                if (val > item.Quantity)
                    isOver = true;
                break;
        }
        if (oldVal == val)
            isOver = null;
        if (isOver != null) {
            if (!isOver)
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Xác nhận tách đơn hàng ?',
                    pars: {},
                    Ok: function (pars) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: "DIMonitorMaster_GroupProduct_Split",
                            data: {
                                gopID: item.OPSGroupProductID,
                                value: val,
                                packingType: type,
                            },
                            success: function (res) {
                                $scope.NonTender_Grid.dataSource.read();
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            }
                        })
                    },
                    Close: function () {
                        $(target).val(oldVal);
                    },
                });
            else {
                $(target).val(oldVal);
                $rootScope.Message({ Msg: 'Vượt quá số lượng ban đầu!', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.SL_AddNonTender = function (e, grid, win) {
        e.preventDefault();
        win.center().open();
        grid.dataSource.read();
    }

    $scope.AddOrderToMaster_Click = function (e, grid, win) {
        e.preventDefault();

        var lst = []
        angular.forEach(grid.dataSource.data(), function (o, i) {
            if (o.IsChoose) {
                lst.push(o.OPSGroupProductID);
            }
        })

        if (lst.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_AddGroupProductFromNonTender",
                data: { lst: lst, masterID: $scope.masterID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.SL_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.NonTenderRefresh_Click = function (e, grid) {
        e.preventDefault();
        grid.dateSource.read();
    }

    //#endregion

    //#region station

    $scope.Station_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_DIStationList",
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
                    DITOLocationName: { editable: false },
                    IsApproved: { type: 'bool', editable: false },
                    DateCome: { type: 'date' }
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        toolbar: kendo.template($('#station-grid-toolbar').html()),
        height: '99%', pageable: false, sortable: false, columnMenu: false, editable: true, resizable: false, filterable: { mode: 'row' }, autoBind: false,
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
               field: 'DateCome', width: 150, title: '{{RS.OPSDITOLocation.DateCome}}', template: '#=Common.Date.FromJsonDMYHM(DateCome)#', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Note', width: 150, title: '{{RS.OPSDITOStation.Note}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DITOLocationName', width: 150, title: '{{RS.MONMonitorIndex.DITOLocationName}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
            {
                field: 'IsApproved', width: 100, title: '{{RS.OPSDITOStation.IsApproved}}',
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
            method: "MONControlTower_DIStationNotinList",
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

    $scope.ItemLocation = {};

    $scope.SearchLocation = function ($event, win, grid) {
        $event.preventDefault();
        win.center().open();
        grid.dataSource.read();
    }

    $scope.SearchStation = function ($event, win, grid, item) {
        $event.preventDefault();
        $scope.ItemLocation = item;
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
                method: "MONControlTower_DIStationAdd",
                data: { ListStationID: lstID, masterID: $scope.masterID, opsLocation: $scope.ItemLocation },
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
                        method: "MONControlTower_DIStationRemove",
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

    $scope.LocationForStation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_DILocation",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="SearchStation($event,Station_NotinWin,Station_NotinGrid,dataItem)" class="k-button"><i class="fa fa-plus"></i></a>',
                filterable: false, sortable: false
            },
           {
               field: 'SortOrder', width: 170, title: '{{RS.OPSDITOLocation.SortOrder}}'
           },
           {
               field: 'LocationAddress', width: 170, title: '{{RS.OPSDITOLocation.LocationAddress}}',
           },
           {
               field: 'LocationName', width: 105, title: '{{RS.CUSLocation.LocationName}}',
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

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
                method: "MONControlTower_DIStationSaveChanges",
                data: { lst: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })

    }

    //#endregion

    //#region Return product

    $scope.Return_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONCT_Index.URL.GOPReturn_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    STT: { type: 'string' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    OrderCode: { type: 'string', editable: false },
                    SOCode: { type: 'string', editable: false },
                    DNCode: { type: 'string', editable: false },
                    GroupProductName: { type: 'string', editable: false },
                    ProductName: { type: 'string', editable: false },
                    LocationToName: { type: 'string', editable: false },
                    LocationToProvince: { type: 'string', editable: false },
                    LocationToDistrict: { type: 'string', editable: false },
                    LocationToAddress: { type: 'string', editable: false },
                    OrderCode: { type: 'string', editable: false },
                    GroupProductID: { type: 'number', editable: false },
                    InvoiceReturnDate: { type: 'date' },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        toolbar: kendo.template($('#return-grid-toolbar').html()),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: true, autoBind: false,
        columns: [

            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Return_Grid,order_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Return_Grid,order_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: 'STT',
                field: 'STT', width: "50px",
            },
            {
                title: '{{RS.ORDOrder.Code}}',
                field: 'OrderCode', width: "100px",
            },
            {
                title: '{{RS.CUSGroupOfProduct.GroupName}}',
                field: 'GroupProductID', template: '#=GroupProductName#', width: "150px", editor: function (container, options) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONCT_Index.URL.CUSGOP_List,
                        data: { customerid: $scope.masterID },
                        success: function (res) {
                            $scope.CUSGOP_CbbOptions.dataSource.data(res);
                        }
                    })
                    var input = $("<input kendo-combobox k-options='CUSGOP_CbbOptions'/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                }
            },
            {
                title: '{{RS.CUSProduct.Code}}',
                field: 'ProductName', width: "150px",
            },
            {
                title: '{{RS.ORDGroupProduct.Quantity}}',
                field: 'QuantityReturn', width: 75,
            },
            {
                title: '{{RS.ORDOrder.Code}}Giá',
                field: 'Price', width: "150px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.InvoiceReturnBy}}',
                field: 'InvoiceReturnBy', width: "150px",
            },
            {
                title: '{{RS.OPSDITOGroupProduct.InvoiceReturnDate}}',
                field: 'InvoiceReturnDate', width: "150px", template: '#=Common.Date.FromJsonDMYHM(InvoiceReturnDate)#',
            },
            {
                title: '{{RS.OPSDITOGroupProduct.InvoiceReturnNote}}',
                field: 'InvoiceReturnNote', width: "150px",
            },
            {
                title: '{{RS.LocationToName}}',
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
        ],
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin hàng trả về...", $rootScope.Loading.Progress + LoadingStep);
        },
        reorderable: true
    }

    $scope.OPSGroupCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'Text',
        dataValueField: 'ValueInt',
        change: function (e) {
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

    $scope.CUSProduct_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'Code',
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

    $scope.CUSGroupofProduct_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'Code',
        dataValueField: 'ID',
        change: function (e) {
            $scope.CUSProduct_Cbb.text("");
            $scope.CUSProduct_Cbb.open();
        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    $scope.GOPReturn_Save = function ($event, vform, win) {
        $event.preventDefault();
        $scope.ItemGOPRturn.MasterID = $scope.masterID;
        $scope.ItemGOPRturn.ProductID = $scope.ItemGOPRturn.ProductID > 0 ? $scope.ItemGOPRturn.ProductID : 0;
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "DIMonitorMaster_GOPReturnAdd",
                data: { item: $scope.ItemGOPRturn },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Return_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
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
            method: _MONCT_Index.URL.GOPReturn_Save,
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

    $scope.Return_Add = function ($event, win) {
        $event.preventDefault();
        if ($scope.CUSGroupofProduct_CbbOptions.dataSource.data().length > 0) {
            for (var i = 0; i < $scope.DataDITOGroupProduct.length; i++) {
                $scope.ItemGOPRturn.OrderGroupID = $scope.DataDITOGroupProduct[i].ValueInt;
                break;
            }
            for (var i = 0; i < $scope.DataCUSProduct.length; i++) {
                if ($scope.DataCUSProduct[i].HasReturn) {
                    $scope.ItemGOPRturn.ProductID = $scope.DataCUSProduct[i].ID;
                    $scope.ItemGOPRturn.GroupProductID = $scope.DataCUSProduct[i].GroupOfProductID;
                    break;
                }
            }
            $scope.ItemGOPRturn.InvoiceReturnDate = new Date();
            $scope.ItemGOPRturn.InvoiceReturnBy = $rootScope.Default_UserName;
            $scope.ItemGOPRturn.InvoiceReturnNote = "";
            win.center().open();
        }
        else {
            $rootScope.Message({ Msg: 'Khách hàng chưa thiết lập hàng trả về', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Return_DeleteList = function ($event, grid) {
        $event.preventDefault();
        var lst = [];
        angular.forEach(grid.dataSource.data(), function (o, i) {
            if (o.IsChoose == true)
                lst.push(o.ID);
        });

        if (lst.length > 0)
            $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitorMaster_GOPReturnDeleteList",
            data: { lst: lst },
            success: function (res) {
                $rootScope.IsLoading = false;
                grid.dataSource.read();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
    }
    //#endregion

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

    $scope.GroupOfTrouble_CbbOptions = {
        autoBind: true, valuePrimitive: false, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'Name', dataValueField: 'ID', placeholder: "Chọn sự cố",
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { Name: { type: 'string' }, ID: { type: 'string' } }
            }
        })
    }

    $scope.CancelSOReason_CbbOptions = {
        autoBind: true, valuePrimitive: false, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'Text', dataValueField: 'ValueInt', placeholder: "Chọn lý do hủy",
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { Text: { type: 'string' }, ID: { ValueInt: 'string' } }
            }
        })
    }

    //event

    $scope.order_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    //#endregion

    //#region Popup update TODetail
    $scope.IsWinTOUpdateMax = true;
    $scope.Time_From = new Date();
    $scope.Time_To = null;
    var MapNo = 0;
    var curUIDItem = {};
    var orgUIDItem = {};
    $scope.TripItem = {};
    $scope.VendorOfVehicleID = -1;

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
            $scope.trip_info_win.toFront();

            $scope.Time_From = $scope.CurrentMaster.DateFrom;
            $scope.Time_To = $scope.CurrentMaster.DateTo;
            $scope.vehicle_grid.dataSource.read();
        },
        resize: function () {
            $scope.trip_info_win.toFront();
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

    $scope.ChangeTOMaster = function (win, win2) {
        if ($scope.Auth.ActEdit) {
            if ($scope.CurrentMaster.IsTransport) {
                win2.center().open();
                $scope.TOUpdateType = 2;
                $scope.MasterDetail_Grid.dataSource.read();
            }
            else if (!$scope.CurrentMaster.IsComplete) {
                win.open().maximize();
                $scope.TOUpdateType = 1;
                if ($scope.CurrentMaster.IsVehicleVendor) {
                    $scope.VendorOfVehicleID = $scope.CurrentMaster.VendorID;
                }
                else {
                    $scope.VendorOfVehicleID = -1;
                }
                $scope.vehicle_grid.dataSource.read();
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
            $rootScope.Message({
                Type: Common.Message.Type.ERROR,
                Msg: 'Tài khoản không có quyển đổi xe',
            });
        }
    };

    $scope.Vendor_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'VendorName', dataValueField: 'VendorID', enable: true, value: -1,
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'VendorID', fields: { VendorName: { type: 'string' }, VendorID: { type: 'number' } } }
        }),
        change: function (e) {
            $scope.vehicle_grid.dataSource.read();
        }
    };

    $scope.vehicle_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TruckRead",
            readparam: function () {
                return {
                    DateFrom: Common.Date.Date($scope.Time_From),
                    DateTo: Common.Date.Date($scope.Time_To),
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
               title: 'Số xe',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'MaxWeight', width: 100,
               title: 'Trọng tải',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Status', width: 100,
               title: 'Tình trạng',
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

    $scope.MasterDetail_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "DIMonitorMaster_SL_List",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    STT: { type: 'string' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsReceived: { type: 'bool', defaultValue: false },
                    HasCashCollect: { type: 'bool', editable: false },
                    OrderCode: { type: 'string', editable: false },
                    SOCode: { type: 'string', editable: false },
                    DNCode: { type: 'string', editable: false },
                    Quantity: { type: 'number', editable: true },
                    ProductName: { type: 'string', editable: false },
                    CustomerName: { type: 'string', editable: false },
                    LocationToName: { type: 'string', editable: false },
                    LocationToProvince: { type: 'string', editable: false },
                    LocationToDistrict: { type: 'string', editable: false },
                    LocationToAddress: { type: 'string', editable: false },
                    GroupProductName: { type: 'string', editable: false },
                    GroupProductID: { type: 'number', editable: false },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
            {
                field: 'Đã nhận', width: 100, title: '',
                template: '<input disabled class="chkChoose" type="checkbox" #= IsReceived ? "checked=checked" : "" #" />',
                filterable: false
            },
            {
                title: 'Mã ĐH', field: 'OrderCode', width: "100px",
            },
            {
                title: 'Nhóm SP', field: 'GroupProductID', template: '#=GroupProductName#', width: "150px"
            },
            {
                title: 'SP', field: 'ProductName', width: "120px"
            },
            {
                title: 'SL lấy', field: 'Quantity', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="Quantity">');
                    input.appendTo(container);
                },
            },
            {
                title: 'SL giao', field: 'QuantityTranfer', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="QuantityTranfer">');
                    input.appendTo(container);
                    var co = container;
                    input.change(function (e) {
                        var grid = $scope.SL_Grid;
                        var item = grid.dataItem(this.closest('tr'));
                        if (this.value <= item.Quantity) {
                            item.QuantityReturn = item.Quantity - this.value;
                        }
                        else {
                            this.value = item.Quantity;
                            item.QuantityReturn = 0;
                            $rootScope.Message({ Msg: 'SL giao không được lớn hơn SL lấy', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    })
                },
            },
            {
                title: 'SL trả về', field: 'QuantityReturn', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="QuantityReturn">');
                    input.appendTo(container);
                },
            },
            {
                title: 'KH', field: 'CustomerName', width: 100,
            },
            {
                title: 'Điểm Đến', field: 'LocationToName', width: "125px",
            },
            {
                title: 'Tỉnh thành', field: 'LocationToProvince', width: "125px",
            },
            {
                title: 'Quận huyện', field: 'LocationToDistrict', width: "125px",
            },
            {
                title: 'Địa chỉ', field: 'LocationToAddress', width: "200px",
            },
            { title: ' ', filterable: false, sortable: false }
        ],
        reorderable: true
    };

    $scope.LocationAll_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: "OPSCO_MAP_Location_List",
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a class="k-button" title="Chọn" href="/" ng-click="Location_Select($event,dataItem,location_win)"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', width: 150, title: 'Mã',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Location', width: 250, title: 'Tên',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.trip_info_win_options = {
        title: false,
        width: 550,
        height: 300,
        resizable: false,
        visible: false,
        modal: false,
        position: {
            top: '50%',
            left: 0
        },
        appendTo: "#winid"
    }

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

    $scope.MasterSplit_Click = function (e, win1, win2, win3) {
        e.preventDefault();
        win1.close();
        win2.open();
        $timeout(function () {
            win3.open();
            $scope.MONTOUpdate_Splitter.resize();
        }, 500)

        $scope.TripItem = {}
        $scope.TripItem.VendorID = null;
        $scope.TripItem.VehicleID = $scope.CurrentMaster.VehicleID;
        $scope.TripItem.VehicleNo = $scope.CurrentMaster.VehicleNo;
        $scope.TripItem.DriverName = $scope.CurrentMaster.DriverName;
        $scope.TripItem.DriverTel = $scope.CurrentMaster.DriverTel;
        $scope.TripItem.ETD = $scope.CurrentMaster.DateFrom;
        $scope.TripItem.ETA = $scope.CurrentMaster.DateTo;
        $scope.TripItem.LocationStartID = -1;
        $scope.TripItem.LocationEndID = -1;
        $scope.TripItem.LocationStartName = "";
        $scope.TripItem.LocationEndName = "";
    };

    $scope.TripLocation_Change = function ($event, type, win) {
        $event.preventDefault();

        $scope.LocationType = type;
        win.center().open();
        $timeout(function () {
            $scope.LocationAll_Grid.resize();
        }, 100)
    }

    $scope.CreateNewMaster_Click = function (e, win) {
        e.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận tách chuyến chuyến?',
            pars: {},
            Ok: function (pars) {

                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONControlTower_TOMaster_Split",
                    data: {
                        masterID: $scope.masterID,
                        vendorID: $scope.VendorOfVehicleID,
                        vehicleID: $scope.TripItem.VehicleID,
                        ETD: $scope.TripItem.ETD,
                        ETA: $scope.TripItem.ETD,
                        driverName: $scope.TripItem.DriverName,
                        driverTel: $scope.TripItem.DriverTel,
                        fLocation: $scope.TripItem.LocationStartID,
                        tLocation: $scope.TripItem.LocationEndID,
                    },
                    success: function (res) {
                        $scope.TO_Win.close();
                        win.close();
                        $scope.SplitTOMaster_Win.close();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
        });
    };

    $scope.PickVehicle = function (e, item) {
        e.preventDefault();
        if (curUIDItem.uid != item.uid && $scope.TOUpdateType == 1) {
            var t = $scope.vehicle_grid.dataItem("[data-uid='" + item.uid + "']");
            t.IsChoose = true;
            var tr = $scope.vehicle_grid.tbody.find("tr[data-uid='" + item.uid + "']");
            $(tr.find('.chkChoose')).prop('checked', 'checked');
            var o = $scope.vehicle_grid.dataItem("[data-uid='" + curUIDItem.uid + "']");
            if (Common.HasValue(o)) {
                o.IsChoose = false;
                tr = $scope.vehicle_grid.tbody.find("tr[data-uid='" + curUIDItem.uid + "']");
                $(tr.find('.chkChoose')).prop('checked', false);
            }
            curUIDItem = item;
        }
        if ($scope.TOUpdateType == 2) {
            var t = $scope.vehicle_grid.dataItem("[data-uid='" + item.uid + "']");
            t.IsChoose = true;
            var tr = $scope.vehicle_grid.tbody.find("tr[data-uid='" + item.uid + "']");
            $(tr.find('.chkChoose')).prop('checked', 'checked');
            var o = $scope.vehicle_grid.dataItem("[data-uid='" + curUIDItem.uid + "']");
            if (Common.HasValue(o)) {
                o.IsChoose = false;
                tr = $scope.vehicle_grid.tbody.find("tr[data-uid='" + curUIDItem.uid + "']");
                $(tr.find('.chkChoose')).prop('checked', false);
            }
            curUIDItem = item;
            if (Common.HasValue(item)) {
                $scope.TripItem.VehicleID = item.ID;
                $scope.TripItem.VehicleNo = item.RegNo;
            }
        }
    }

    $scope.SearchVehicle = function (e, grid) {
        e.preventDefault();
        grid.dataSource.read();
    };

    $scope.SaveVehicle = function (e, grid) {
        e.preventDefault();
        if (orgUIDItem.ID != curUIDItem.ID || (orgUIDItem.ID == curUIDItem.ID && $scope.VendorOfVehicleID != $scope.CurrentMaster.VendorOfVehicleID)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_ChangeVehicle",
                data: {
                    masterID: $scope.masterID,
                    vehicleID: curUIDItem.ID,
                    vendorID: $scope.VendorOfVehicleID,
                },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.TO_Win.close();
                    $scope.TOUpdate_win.close();
                    $scope.ct_order_grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
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

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: "OPSCO_MAP_Driver_List",
        success: function (res) {
            var data = [];
            $.each(res, function (i, v) {
                data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
            });
            $scope.atcDriverNameOptions.dataSource.data(data);
        }
    });

    $scope.atcDriverNameOptions = {
        dataSource: Common.DataSource.Local({ data: [] }),
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Họ tên", dataTextField: "DriverName",
        change: function (e) {
            var cbo = this;
            $timeout(function () {
                var obj = cbo.dataItem(cbo.select());
                if (Common.HasValue(obj)) {
                    $scope.TripItem.DriverTel = obj.DriverTel;
                }
            }, 10)
        }
    }

    //#endregion

    //#region Popup driver todetail

    $scope.TypeOfUpdateDriver = 1;//1 tai xe / 2 phu lai / 3 boc xep
    $scope.ListTypeOfDriver = [];
    $scope.ListSupDriver = [];
    $scope.DriverDateFrom = null;
    $scope.DriverDateTo = null;
    $scope.TypeOfDriverID = 0;
    $scope.TypeOfPickDriver = 'main';

    $scope.Driver_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_DriverRead",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
            readparam: function () {
                return {
                    DateFrom: $scope.DriverDateFrom,
                    DateTo: $scope.DriverDateTo,
                }
            }
        }),
        height: '98%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, selectable: "multiple",
        columns: [
            {
                field: 'Note', width: 100,
                title: 'Tình trạng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName', width: 100,
                title: 'Tên tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Cellphone', width: 100,
                title: 'SĐT',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CardNumber', width: 100,
                title: 'CMND',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.SupDriver_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '98%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, selectable: true,
        columns: [
            {
                field: '', width: 100, title: '',
                template: "<a class='k-button' ng-show='!CurrentMaster.IsComplete' ng-click='SupDriver_DestroyClick($event,SupDriver_Grid,dataItem)'><i class='fa fa-trash'></i></a>",
                filterable: false
            },
            {
                field: 'Name', width: 100,
                title: 'Tên tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Card', width: 100,
                title: 'CMND',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfDriverName', width: 100,
                title: 'Vai trò',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.DriverGrid_BtnSave = function (e, grid) {
        e.preventDefault();
        var data = grid.select();
        if (data.length > 0) {
            var item = grid.dataItem(data[0]);
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_ChangeDriver",
                data: {
                    masterID: $scope.masterID,
                    driverID: item.ID,
                    typeOfDriver: $scope.TypeOfUpdateDriver,
                    reason: ""

                },
                success: function (res) {
                    $scope.TO_Win.close();
                    $scope.Driver_Win.close();
                    $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }
    };

    $scope.ChangeTOMaster_Driver = function (win, type) {
        if ($scope.Auth.ActEdit) {
            if (!$scope.CurrentMaster.IsVehicleVendor && !$scope.CurrentMaster.IsComplete) {
                win.center().open();
                $scope.TypeOfPickDriver = 'main';
                $scope.DriverDateFrom = $scope.CurrentMaster.DateFrom;
                $scope.DriverDateTo = $scope.CurrentMaster.DateTo;
                $scope.Driver_Grid.dataSource.read();
                $scope.TypeOfUpdateDriver = type;
            }
            else {
                $rootScope.Message({ Msg: 'Không có quyền đổi tài xế của chuyến này', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.ERROR,
                Msg: 'Tài khoản không có quyển đổi tài xế',
            });
        }
    };

    $scope.ShowListSupDriver_Click = function (win, grid, o) {
        win.center().open();
        $scope.TypeOfDriverID = o.ValueInt;
        $scope.TypeOfPickDriver = 'sup';
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_SupDriverRead",
            data: {
                masterID: $scope.masterID,
            },
            success: function (res) {
                $scope.ListSupDriver = [];
                var lst = [];
                for (var i = 0; i < res.length; i++) {
                    if (res[i].TypeOfDriverID == $scope.TypeOfDriverID) {
                        lst.push(res[i]);

                    }
                    else {
                        $scope.ListSupDriver.push(res[i]);
                    }
                }
                $scope.SupDriver_Grid.dataSource.data(lst);
            }
        })
    };

    $scope.SupDriver_SaveClick = function (e, win, grid) {
        var lstname = [];
        angular.forEach(grid.dataSource.data(), function (o, i) {
            $scope.ListSupDriver.push(o);
            lstname.push(o.Name);
        })
        if ($scope.ListSupDriver.length > 4) {
            $rootScope.Message({ Msg: 'Không được quá 5 người trên xe!', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận lưu thông tin phụ xe?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;

                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTower_ChangeSupDriver",
                        data: {
                            masterID: $scope.masterID,
                            lst: $scope.ListSupDriver,
                        },
                        success: function (res) {
                            angular.forEach($scope.ListTypeOfDriver, function (o, i) {
                                if (o.ValueInt == $scope.TypeOfDriverID) {
                                    o.ListName = lstname.join(', ');
                                }
                            });
                            $rootScope.IsLoading = false;
                            win.close();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            });
        }
    };

    $scope.SupDriver_AddClick = function (e, win) {
        e.preventDefault();
        $scope.DriverDateFrom = $scope.CurrentMaster.DateFrom;
        $scope.DriverDateTo = $scope.CurrentMaster.DateTo;
        $scope.Driver_Grid.dataSource.read();
        win.center().open();
    };

    $scope.SupDriver_PickClick = function (e, win, grid) {
        e.preventDefault();
        var lst = $scope.SupDriver_Grid.dataSource.data();
        angular.forEach(grid.select(), function (o, i) {
            var item = grid.dataItem(o);
            var obj = {};
            obj.Name = item.DriverName;
            obj.ID = item.ID;
            obj.TypeOfDriverID = $scope.TypeOfDriverID;
            lst.push(obj);
        })

        $scope.SupDriver_GridOptions.dataSource.data(lst);
        win.close()
    };

    $scope.SupDriver_DestroyClick = function (e, grid, item) {
        e.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận xóa phụ xe?',
            pars: {},
            Ok: function (pars) {
                grid.removeRow('tr[data-uid="' + item.uid + '"]');
            }
        });
    };
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
            method: "MONControlTower_MainVehicleFilter",
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

                // bieu do donut chuyen
                $scope.MONCT_OPSSummary = res.Summary;

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

                // ve tat ca xe
                if (false) { // ko xai
                    if (Common.HasValue(res.lstVehicle)) {
                        $scope.DrawNewMarker(res.lstVehicle, function (item) {
                            return openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Truck), 1);
                        }, "VectorXe", true, 'ID', 'RegNo');
                    }
                }

                if ($scope.Marker.length > 0)
                    openMapV2.FitBound($scope.Marker);

                $scope.ShowFilterSum = 1;
                $scope.LoadingCount--;
            }
        })
    };

    $scope.ShowHideFilter = function (win) {
        if ($scope.FitlerWidth == false)
            $scope.FitlerWidth = true;
        else {
            $scope.FitlerWidth = false;
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
                    $scope.FilterVehicle.ShowDate = false;
                    break;
                case 2:
                    $scope.FilterVehicle.Time = 2;
                    $scope.MONCT_Filter.DateFrom = $scope.GetMonday(new Date());
                    $scope.MONCT_Filter.DateTo = $scope.GetMonday(new Date().addDays(7));
                    $scope.FilterVehicle.ShowDate = false;
                    break;
                case 3:
                    $scope.FilterVehicle.Time = 3;
                    var toDate = new Date();
                    $scope.MONCT_Filter.DateFrom = new Date(toDate.getFullYear(), toDate.getMonth(), 1);
                    $scope.MONCT_Filter.DateTo = new Date(toDate.getFullYear(), toDate.getMonth() + 1, 1);
                    $scope.FilterVehicle.ShowDate = false;
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
        $timeout(function () {
            $scope.FilterVehicle.ShowDate = false;
        }, 100)
        $scope.ct_order_grid.dataSource.read();
    }

    //#endregion

    //#region Tab Phan cong tai xe

    var _ToDay = new Date();
    $scope.ItemSearch = {
        dateFrom: new Date(_ToDay.getFullYear(), _ToDay.getMonth() - 1, 22),
        dateTo: new Date(_ToDay.getFullYear(), _ToDay.getMonth() + 1, 1),
        IsTruck: true,
        IsTractor: true
    };
    $scope.DriverSchedularItem = {};
    $scope.TabIndex = 0;
    $scope.TimeSheetItem = {};
    $scope.TimeSheetConflictItem = {};
    $scope.TimeSheetFilterParam = {
        Open: false,
        Accept: false,
        Reject: false,
        Complete: false,
        Get: false,
        Running: false,
    }

    $scope.FLMDriverTimeSheet_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
            }, 1)
        }
    };

    $scope.main_schedulerOptions = {
        date: new Date(),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false,
        eventHeight: 20, majorTick: 60,
        height: '99%',
        messages: {
            today: "Hôm nay"
        },
        editable: false,
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
                },
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 50,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 720, selected: true,
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
        eventTemplate: $("#driver-task-template").html(),
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
                dataSource: [{ RegNo: "61C-09204", AssetID: 1 }],
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
                $scope.LoadDriverTimeSheet();
            }, 10)

        },
        save: function (e) {

        }
    }


    //#region Load Cookie

    var ck1 = Common.Cookie.Get('MON_DriverSchedulerFilter');
    if (Common.HasValue(ck1)) {
        try {
            var obj = JSON.parse(ck1);
            $scope.TimeSheetFilterParam = obj;
        }
        catch (e) { }
    }
    //#endregion

    //load resource xe

    $scope.LoadDriverTimeSheet = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TimeSheet_VehicleTimeList",
            data: {
                dateFrom: $scope.ItemSearch.dateFrom,
                dateTo: $scope.ItemSearch.dateTo,
                isOpen: $scope.TimeSheetFilterParam.Open,
                isAccept: $scope.TimeSheetFilterParam.Accept,
                isReject: $scope.TimeSheetFilterParam.Reject,
                isGet: $scope.TimeSheetFilterParam.Get,
                isComplete: $scope.TimeSheetFilterParam.Complete,
                isRunning: $scope.TimeSheetFilterParam.Running,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.main_schedulerOptions.dataSource.data = res.ListEvent;
                var lst = [{ RegNo: "" }];
                if (res.ListReSource.length > 0) {
                    lst = res.ListReSource;
                }
                $scope.main_schedulerOptions.resources[0].dataSource = lst;
                var name = $scope.main_scheduler.viewName();
                var date = $scope.main_scheduler.date();
                $timeout(function () {
                    $scope.main_scheduler.view(name);
                    $scope.main_scheduler.date(date);
                }, 100);
            }
        });
    };

    $timeout(function () {
        var view = $scope.main_scheduler.view();
        $scope.ItemSearch.dateFrom = view.startDate();
        $scope.ItemSearch.dateTo = view.endDate();
        $scope.LoadDriverTimeSheet();
    }, 200)


    $scope.DriverEvent_Click = function (ID, win) {
        $rootScope.IsLoading = true;
        $scope.TimeSheetID = ID;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TimeSheet_VehicleTimeGet",
            data: { timeID: ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $timeout(function () {
                        $scope.FLMDriverTimeSheet_Driver_grid.dataSource.data(res.ListDriver)
                        $scope.TimeSheetItem = res;
                        $rootScope.IsLoading = false;
                        win.center().open();
                    }, 10);
                });
            }
        });
    };

    $scope.FLMDriverTimeSheet_Driver_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Birthday: { type: 'date' },
                    IsReject: { type: 'bool' }
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false, selectable: true,
        columns: [
             {
                 title: 'Từ chối ', width: '100px', field: "IsReject",
                 template: '<input disabled type="checkbox" #=IsReject ? "checked=checked" : "" #/>', sortable: true
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
    };

    $scope.TimeSheetConflict_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DateFrom: { type: 'date' },
                    DateTo: { type: 'date' },
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
            { field: "StatusOfAssetTimeSheetName", title: 'Kế hoạch', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'DriverName', title: 'Tên', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'DateFrom', title: 'Ngày bắt đầu', template: "#=DateFrom==null?' ':Common.Date.FromJsonDMY(DateFrom)#", width: 120,
                filterable: { cell: { showOperators: false, operator: "gte" } }
            },
            {
                field: 'DateFrom', title: 'Ngày kết thúc', template: "#=DateTo==null?' ':Common.Date.FromJsonDMY(DateTo)#", width: 120,
                filterable: { cell: { showOperators: false, operator: "gte" } }
            },
        ]
    };

    $scope.ChangeTypeTimeSheet_Click = function ($event, win, grid) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TimeSheetDriver_CheckComplete",
            data: { timeID: $scope.TimeSheetID },
            success: function (res) {

                var IsConflict = res.IsConflict;
                if (IsConflict) {
                    $scope.TimeSheetConflictItem.DateFrom = res.DateFrom;
                    $scope.TimeSheetConflictItem.DateTo = res.DateTo;
                    win.center().open();
                    grid.dataSource.data(res.ListTimeSheetConflict);
                }
                else {
                    var obj = {
                        DateFrom: res.DateFrom,
                        DateTo: res.DateTo,
                        IsConflict: false,
                        ListTimeSheetConflict: [],
                    }

                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTower_TimeSheetDriverComplete",
                        data: {
                            timeID: $scope.TimeSheetID,
                            item: obj
                        },
                        success: function (res) {
                            $scope.LoadDriverTimeSheet();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                    });
                }
            },
            error: function (res) {
                $rootScope.IsLoading = false;

            }
        });


    };

    $scope.TimeSheetComplete_AcceptClick = function (e, win) {
        win.close();
        var obj = {
            DateFrom: $scope.TimeSheetConflictItem.DateFrom,
            DateTo: $scope.TimeSheetConflictItem.DateTo,
            IsConflict: true,
            ListTimeSheetConflict: [],
        }
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TimeSheetDriverComplete",
            data: {
                timeID: $scope.TimeSheetID,
                item: obj,
            },
            success: function (res) {
                $scope.LoadDriverTimeSheet();
                $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
        });
    }

    $scope.TimeSheetChangeDriver_Click = function (e, win, grid) {
        e.preventDefault();
        win.center().open();
        $scope.TypeOfPickDriver = 'timesheet';
        $scope.DriverDateFrom = $scope.TimeSheetItem.DateFrom;
        $scope.DriverDateTo = $scope.TimeSheetItem.DateTo;
        $scope.Driver_Grid.dataSource.read();
    };

    $scope.TimeSheet_PickClick = function (e, win, grid) {
        e.preventDefault();
        var data = grid.select();
        win.close();
        if (data.length > 0) {

            $rootScope.IsLoading = true;
            var item = grid.dataItem(data[0]);
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_ChangeTimeSheetDriver",
                data: {
                    flmDriverID: item.FLMDriverID,
                    timeID: $scope.TimeSheetItem.TimeSheetID,
                },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONControlTower_TimeSheet_VehicleTimeGet",
                        data: { timeID: $scope.TimeSheetID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $timeout(function () {
                                    $scope.FLMDriverTimeSheet_Driver_grid.dataSource.data(res.ListDriver)
                                    $scope.TimeSheetItem = res;
                                    $rootScope.IsLoading = false;
                                    win.center().open();
                                }, 10);
                            });
                        }
                    });
                    $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }
        win.close();
    };

    $scope.TimeSheet_ApproveDriver = function (e, grid) {
        e.preventDefault();
        var data = grid.select();
        if (data.length > 0) {
            var item = grid.dataItem(data[0]);
            if (!item.IsReject) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Xác nhận duyệt tài xế?',
                    pars: {},
                    Ok: function (pars) {
                        $scope.LoadingCount++;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: "MONControlTower_TimeSheet_ApproveDriver",
                            data: {
                                timeDriverID: item.ID,
                            },
                            success: function (res) {
                                $scope.LoadingCount = 0;
                                $scope.FLMDriverTimeSheet_win.close();
                                $scope.LoadDriverTimeSheet();
                                $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            }
                        });
                    }
                });
            }
            else {
                $rootScope.Message({ Msg: 'Lệnh đã bị từ chối không thể duyệt!', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.TimeSheet_RejectDriver = function (e, grid) {
        e.preventDefault();
        var data = grid.select();
        if (data.length > 0) {
            var item = grid.dataItem(data[0]);
            if (!item.IsReject) {

                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Xác nhận từ chối tài xế?',
                    pars: {},
                    Ok: function (pars) {
                        $scope.LoadingCount++;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: "MONControlTower_TimeSheet_RejectDriver",
                            data: {
                                timeDriverID: item.ID,
                            },
                            success: function (res) {
                                $scope.LoadingCount = 0;
                                $scope.FLMDriverTimeSheet_win.close();
                                $scope.LoadDriverTimeSheet();
                                $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            }
                        });
                    }
                });
            }
            else {
                $rootScope.Message({ Msg: 'Lệnh đã bị từ chối!', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.ListTimeVehicle = [];
    $scope.VehicleMultiOptions = {
        autoBind: true, valuePrimitive: false, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'RegNo', dataValueField: 'AssetID', placeholder: "Chọn xe",
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { Name: { type: 'string' }, ID: { type: 'string' } }
            }
        }),
        change: function (e) {
            var lst = [{ RegNo: "" }];
            if (this.dataItems().length > 0)
                lst = this.dataItems();
            $scope.main_schedulerOptions.resources[0].dataSource = lst;
            var name = $scope.main_scheduler.viewName();
            $timeout(function () {
                $scope.main_scheduler.view(name);
            }, 100)

        }
    };
    $scope.ShowTimeSheetInfo = false;
    $scope.TimeSheetQuickInfo = {};

    $scope.TimeSheetDriver_MouseOver = function (e) {
        var item = this.dataItem;
        var rec = e.target.getBoundingClientRect();

        var left = e.clientX + "px";
        var top = rec.top + e.target.offsetHeight + 1 + "px";
        $scope.InfoPosition = { 'top': top, 'left': left };
        $scope.ShowTimeSheetInfo = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_TimeSheetDriverQuickInfo",
            data: {
                timeID: item.meetingID,
            },
            success: function (res) {
                if (res) {
                    $scope.TimeSheetQuickInfo = res;
                }
            }
        });
    }

    $scope.TimeSheetDriver_MouseLeave = function (e) {
        $scope.ShowTimeSheetInfo = false;
    }

    $scope.TimeSheetStatus_Click = function (e, type) {

        var view = $scope.main_scheduler.view();
        $scope.ItemSearch.dateFrom = view.startDate();
        $scope.ItemSearch.dateTo = view.endDate();
        switch (type) {
            case 'open':
                $scope.TimeSheetFilterParam.Open = !$scope.TimeSheetFilterParam.Open;
                break;
            case 'accept':
                $scope.TimeSheetFilterParam.Accept = !$scope.TimeSheetFilterParam.Accept;
                break;
            case 'reject':
                $scope.TimeSheetFilterParam.Reject = !$scope.TimeSheetFilterParam.Reject;
                break;
            case 'get':
                $scope.TimeSheetFilterParam.Get = !$scope.TimeSheetFilterParam.Get;
                break;
            case 'complete':
                $scope.TimeSheetFilterParam.Complete = !$scope.TimeSheetFilterParam.Complete;
                break;
            case 'running':
                $scope.TimeSheetFilterParam.Running = !$scope.TimeSheetFilterParam.Running;
                break;
        }
        $scope.LoadDriverTimeSheet();
        Common.Cookie.Set('MON_DriverSchedulerFilter', JSON.stringify($scope.TimeSheetFilterParam));
    }
    //#endregion

    //#region Gantt / Timeline

    $scope.ct_vehicle_schedulerOptions = {
        date: new Date().addDays(-1),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false,
        eventHeight: 25, majorTick: 60,
        height: '100%',
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
                majorTick: 120
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 50, selected: true,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 720
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 100,
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
        eventTemplate: $("#task-template").html(),
        group: {
            resources: ["VehicleID"],
            orientation: "vertical"
        },
        dataBound: function (e) {
            var left = this.element.find('.k-scheduler-layout').children().children();
            //custom header
            //custom row
            var a = left.last();
            angular.forEach(a.find('.k-scheduler-times th'), function (o, i) {
                var func = "";
                var text = $(o).text();
                if (text.split('|')[1])
                    func = "ng-click='VehicleHistory(" + text.split('|')[1].toString() + ")' style='cursor:pointer;'";
                if (text.split('|').length > 2) {
                    $(o).text(text.split('|')[0]);
                    $(o).parent().find(".schedule-dot-list").remove();
                    $(o).append($scope.HtmlScheduleDotStatus(text));



                }
                if ($(o).parent().find("img").length == 0)
                    var html = "<img " + func + " class='schedule-img' src='images/function/ico_xe_done.png'/>";
                $(o).prepend($compile(html)($scope));

            })
        },
        resources: [
            {
                field: "attendees",
                name: "VehicleID",
                dataSource: [],
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

        }
    }

    $scope.CloseWinInfo = function (e) {
        e.preventDefault();
        $scope.Show_Gantt_Info = false;
    }

    $scope.VehicleHistory = function (e) {
        $scope.VehicleLogItem.MasterID = e;

        if (Common.HasValue($scope.VehicleLogItem.MasterID))
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONControlTower_OrderLogList",
                data: { masterID: e },
                success: function (res) {
                    $scope.Show_Gantt_Info = true;
                    $scope.VehicleLogItem.RegNo = "";
                    $scope.VehicleLogData = res;
                }
            })
    }

    $scope.Complete_MasterTimeline = function ($event, isComplete) {
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
                        method: _MONCT_Index.URL.SRVehicleComplete,
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

    $scope.UpdateCurrentLocal = function () {
        $scope.LoadingCount++;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "Extend_VehiclePosition_GetLast",
            data: { vehicleCode: $scope.VehicleLogData.GPSCode, dtfrom: new Date() },
            success: function (res) {
                $scope.LoadingCount--;
                if (Common.HasValue(res)) {
                    var check = null;
                    if (res.Lat != 0 && res.Lng != 0 && res.Lat != null && res.Lng != null) {
                        check = true;
                        $scope.DrawNewMarker([res], function (item) {
                            return openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Truck), 1);
                        }, "VectorXe", true, 'ID', 'VehicleCode');
                    }
                    if (Common.HasValue(check)) {
                        openMapV2.FitBound("VectorXe", 15);
                        $rootScope.Message({ Msg: 'Cập nhật tọa độ thành công.', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                    else {
                        $rootScope.Message({ Msg: 'Không thấy xe.', NotifyType: Common.Message.NotifyType.ERROR });
                    }
                }
            }

        })
    }

    $scope.HtmlScheduleDotStatus = function (string) {
        var lst = string.split('|');
        var htmlTop = [];
        var htmlBot = [];

        for (var i = 2; i < lst.length; i++) {
            var sep = lst[i].split(';');
            if (sep[0] == 'come') {
                if (sep[2] == "")
                    htmlTop.push("<div class='schedule-dot' style='display:inline-block;margin-right:1px; width:6px;height:6px;border-radius:6px;border:2px solid grey'></div>");
                else
                    htmlTop.push("<div class='schedule-dot' style='display:inline-block;margin-right:1px; width:10px;height:10px;border-radius:10px;background-color:green'><span class='locationdate' style='display:none'>" + sep[2] + "</span></div>");
            }
            else {
                if (sep[2] == "")
                    htmlBot.push("<div class='schedule-dot' style='display:inline-block;margin-right:1px; width:6px;height:6px;border-radius:6px;border:2px solid grey'></div>");
                else
                    htmlBot.push("<div class='schedule-dot' style='display:inline-block;margin-right:1px; width:10px;height:10px;border-radius:10px;background-color:green'><span class='locationdate' style='display:none'>" + sep[2] + "</span></div>");

            }
        }
        return "<div class='schedule-dot-list'><div>" + htmlTop.join("") + "</div><div>" + htmlBot.join("") + "</div></div>";
    }

    $scope.ShowActualTimeline = function (e) {
        e.preventDefault();
        $scope.ShowTimelineActual = !$scope.ShowTimelineActual;
    }

    $scope.Event_Click = function (masterID) {
        $scope.ShowOrderStatusPane = true;

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_OrderLogList",
            data: { masterID: masterID },
            success: function (res) {
                $scope.OrderLogData = res.LstTroubleLog;
            }
        })

        $scope.VehicleHistory(masterID);

        $scope.LoadMasterDetail(masterID);
    };

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
            for (var idx = 0; idx < $scope.LstTimelineTask.length; idx++) {
                if ($scope.FilterXe.Run) {
                    if (o.value == $scope.LstTimelineTask[idx].VehicleID && $scope.LstTimelineTask[idx].Status == 2) {
                        lst.push(o);
                        break;
                    }
                }
                else if ($scope.FilterXe.Free) {
                    if (o.value == $scope.LstTimelineTask[idx].VehicleID && $scope.LstTimelineTask[idx].Status != 2) {
                        lst.push(o);
                        break;
                    }
                }
            }

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

    //#endregion

    //#region Event
    $scope.IsShowProblem = false;
    var _isLoadMap = false;

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

        if (!_isLoadMap) {
            _isLoadMap = true;

            $scope.MONDI_Splitter.size(".k-pane:first", "40%");
            $timeout(function () {
                $scope.CreateMap();
                openMapV2.ClearMap();
                openMapV2.Active(MainMap);
                MapNo = 1;
            }, 500);
        }
    }

    $scope.Xoay = function (e) {
        e.preventDefault();
        if (Common.HasValue($state.params.orien))
            $state.go("main.MONMonitor.Index", { orien: null }, { reload: true });
        else
            $state.go("main.MONMonitor.Index", { orien: "hor" });
    }

    $scope.Filter_Status_Click = function (e) {
        e.preventDefault();
        $scope.Filter_Status_Position.Left = e.clientX - 99;
        $scope.Filter_Status_Position.Top = e.clientY - 1;
        $scope.Show_Filter_Status = !$scope.Show_Filter_Status;
    }

    $scope.Filter_Status_Hide = function (e) {
        e.preventDefault();
        $scope.Show_Filter_Status = false;
    }

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }

    $scope.To_WinUpdate = function ($event) {
        Common.Log("WinToUpdate");
        var isSuccess = [];

        if ($scope.CurrentMaster.KMStart > $scope.CurrentMaster.KMEnd) {
            isSuccess.push("Số KM kết thúc phải lớn hơn KM bắt đầu");
        }
        if (isSuccess.length == 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONCT_Index.URL.SRWinToUpdate,
                data: { item: $scope.CurrentMaster, lstDetail: [] },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.TO_Win.close();
                    $scope.ct_order_grid.dataSource.read();
                    $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
        else {
            $rootScope.Message({ Msg: isSuccess.join("; "), NotifyType: Common.Message.NotifyType.ERROR });
        }

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
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONCT_Index.URL.VendorList,
            data: {},
            success: function (res) {
                $scope.Vendor_multiselectOptions.dataSource.data(res.Data);
                res.Data.unshift({
                    VendorID: -1,
                    VendorName: 'Xe nhà'
                });
                $scope._dataVendor = res.Data;
                $scope.Vendor_CbbOptions.dataSource.data(res.Data);
                $scope.Vendor_TOLoadCbbOptions.dataSource.data(res.Data);
                $scope.Vendor_TOUnLoadCbbOptions.dataSource.data(res.Data);
                $timeout(function () {
                    $scope.Vendor_Cbb.select(0);
                }, 100)
                if ($scope._dataTruck.length > 0 && $scope._dataVendor.length > 0)
                    $scope.InitDataVendorVehicle();
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONCT_Index.URL.TruckList,
            data: {},
            success: function (res) {
                $scope._dataTruck = res.Data;
                if ($scope._dataTruck.length > 0 && $scope._dataVendor.length > 0)
                    $scope.InitDataVendorVehicle();
            }
        })

        //DIMonitor_ListTypeOfDriver
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_ListTypeOfDriver",
            data: {},
            success: function (res) {
                $scope.ListTypeOfDriver = [];
                angular.forEach(res, function (o, i) {
                    if (o.ValueString != 'main') {
                        $scope.ListTypeOfDriver.push(o);
                    }
                });
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_TOGroupProductCancelReason",
            data: {},
            success: function (res) {
                $scope.CancelSOReason_Cbb.dataSource.data(res);
            }
        })

        //MONControlTower_Schedule
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_Schedule",
            data: { item: $scope.FilterVehicle },
            success: function (res) {
                $scope.ct_vehicle_schedulerOptions.dataSource.data = res.ListTask;
                $scope.LstTimelineTask = res.ListTask;
                $scope.LstTimelineResource = res.ListResource;
                $scope.ct_vehicle_schedulerOptions.resources[0].dataSource = res.ListResource;
                $rootScope.IsLoading = false;
                $timeout(function () {
                    $scope.ct_vehicle_scheduler.refresh();
                }, 100)
            }
        })

        //TroubleList
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONCT_Index.URL.TroubleList,
            data: { isCo: false },
            success: function (res) {
                $scope.GroupOfTrouble_CbbOptions.dataSource.data(res.Data);
            }
        })

        //province
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: Common.ALL.URL.Province,
            data: {},
            success: function (res) {
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
                $scope.LocalObj.District = res.Data;
                $scope.FromDistrictCbbOptions.dataSource.data(res.Data);
                $scope.ToDistrictCbbOptions.dataSource.data(res.Data);
            }
        });

        //notify
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_GetAllNotification",
            data: {},
            success: function (res) {
                $scope.dataloading = null;
                $scope.NotifyData = res;
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
            method: _MONCT_Index.URL.CUSGOP_List,
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
            Type: Common.CATTypeOfFileCode.TROUBLE,
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
                Name: 'VectorProblem',
                zIndex: 100
            }, {
                Name: 'VectorXe',
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

        openMapV2.NewControl(
            '<i style="font-size:20px" class="fa fa-filter"></i>',
            'Lọc',
            'map-view-buttonleft',
            function () {
                $timeout(function () {
                    if ($scope.FitlerWidth == false)
                        $scope.FitlerWidth = true;
                    else {
                        $scope.FitlerWidth = false;
                    }
                }, 1)
            }
        )

        openMapV2.NewControl(
            '<i style="font-size:20px" class="fa fa-bar-chart"></i>',
            'Thống kê',
            'map-view-buttonleft1',
            function () {
                $timeout(function () {
                    $scope.ShowFilterSum = $scope.ShowFilterSum == 0 ? 1 : 0;
                }, 1)
            }
        );

        openMapV2.NewControl(
            '<i style="font-size:20px" class="fa fa-clock-o"></i>',
            'Thiết lập',
            'map-view-buttonleft2',
            function () {
                $timeout(function () {
                    $scope.MapConfig_Win.center().open();
                }, 1)
            }
        );

    }
    $scope.DrawRoute = function (from, to) {
        openMapV2.NewRoute(from, to, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
            openMapV2.FitBound("VectorRoute", 15);
        });
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

    $scope.CloseMapInfo = function (e) {
        e.preventDefault();
        openMapV2.Close();
    }

    $scope.MapConfig_SaveChanges = function (e, win) {
        e.preventDefault();
        win.close();
        $scope.SetInterval();

    }
    //#endregion

    $scope.IsAutoRefesh = true;
    $scope.RefreshInterval = 10;
    $scope.IntervalFunction = null;
    $scope.SetInterval = function () {
        $scope.stopInterval(interval_real_route);
        if ($scope.IsAutoRefesh && Common.HasValue($scope.IntervalFunction)) {
            interval_real_route = $interval($scope.IntervalFunction, 1000 * $scope.RefreshInterval);
        }
    }

    //

    $scope.GetMonday = function (d) {
        d = new Date(d);
        var day = d.getDay(),
            diff = d.getDate() - day + (day == 0 ? -6 : 1);
        return new Date(d.setDate(diff));
    }

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
    };

    $scope.RandomColor = function () {
        var lst = ['#FF0000', '#00FFFF', '#0000FF', '#0000A0', '#ADD8E6', '#800080', '#FFFF00', '#00FF00', '#FF00FF', '#FFA500', '#A52A2A', '#800000', '#008000', '#808000'];
        return lst[Math.floor(Math.random() * lst.length)];
    }
}])