/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _ORDOrder_Packet = {
    URL: {
        List: "ORD_PAK_List",
        OrderList: "ORD_PAK_Order_List"
    }
}

//#endregion

angular.module('myapp').controller('ORDOrder_PacketCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_PacketCtrl');

    $scope.PacketID = -1;

    $scope.schedulerOptions = {
        date: new Date().addDays(-1),
        footer: false, snap: false, height: '99%',
        messages: { today: "Hôm nay" }, editable: false,
        eventTemplate: $("#scheduler-event-template").html(),
        dataBound: function (e) {
            var schedule = this;
            var data = schedule.dataSource.data();
            Common.Data.Each(schedule.items(), function (o) {
                Common.Data.Each(data, function (i) {
                    if (i.uid == $(o).data('uid')) {
                        switch (i.Status) {
                            case 1:
                                $(o).addClass('status1');
                                break;
                            case 2:
                                $(o).addClass('status2');
                                break;
                            case 3:
                                $(o).addClass('status3');
                                break;
                            case 4:
                                $(o).addClass('status4');
                                break;
                        }
                    }
                })
            })
        },
        views: [
             {
                 type: "day", allDaySlot: false, title: "Ngày",
                 selectedDateFormat: "{0:dd-MM-yyyy}", majorTick: 60, minorTickCount: 1,
                 dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                 majorTimeHeaderTemplate: kendo.template("<strong>#=Math.round(kendo.toString(date, 'HH'))#:00</strong>")
             },
             {
                 type: "week", title: "Tuần", allDaySlot: false, selected: true,
                 selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}", majorTick: 60, minorTickCount: 1,
                 dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                 majorTimeHeaderTemplate: kendo.template("<strong>#=Math.round(kendo.toString(date, 'HH'))#:00</strong>")
             },
             {
                 type: "month", title: "Tháng", allDaySlot: false, selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}", majorTick: 120,
                 dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                 majorTimeHeaderTemplate: kendo.template("<strong>#=Math.round(kendo.toString(date, 'HH'))#:00</strong>")
             }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { from: "ID", type: "number" }, title: { from: "PacketName" },
                        start: { type: "date", from: "Start" }, end: { type: "date", from: "End" }
                    }
                }
            }
        }
    }

    $scope.tooltipOptions = {
        filter: ".cus-event", position: "top",
        content: function (e) {
            return $(e.target).data('value');
        }
    }

    $scope.LoadData = function () {
        Common.Log("LoadData");

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Packet.URL.List,
            success: function (res) {
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: res,
                    schema: {
                        model: {
                            id: "id",
                            fields: {
                                id: { from: "ID", type: "number" }, title: { from: "PacketName" },
                                start: { type: "date", from: "Start" }, end: { type: "date", from: "End" }
                            }
                        }
                    }
                });
                $scope.scheduler.setDataSource(dataSource);
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.LoadData();

    var CS = $.extend({
        StatusOfOrderName: 120, StatusOfPlanName: 120, Code: 120, ServiceOfOrderName: 120, TransportModeName: 120,
        ExternalCode: 120, ExternalDate: 120, ETD: 120, ETA: 120, TotalTon: 120, TotalCBM: 120, LocationFrom: 120, LocationTo: 120,
        LocationFromAddress: 120, LocationToAddress: 120, CustomerName: 120, RequestDate: 120, TypeOfContractName: 120,
        TripNo: 120, VesselName: 120, CutOffTime: 120, CreatedDate: 120, UserDefine1: 120, UserDefine2: 120, UserDefine3: 120, TotalQuantity: 120,
        IsWarning: 120, ShortName: 120, CreatedBy: 120, TotalContainer20: 120, TotalContainer40: 120, TypeOfWAInspectionName: 120, TypeOfWAInspectionStatusName: 120
    }, true, $rootScope.GetColumnSettings('order_Grid'));

    $scope.order_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Packet.URL.OrderList, pageSize: 20,
            readparam: function () { return { pID: $scope.PacketID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    RequestDate: { type: 'date' },
                    StatusOfOrderName: { type: 'string' },
                    CutOffTime: { type: 'date' },
                    CreatedDate: { type: 'date' },
                    ExternalDate: { type: 'date' },
                    UserDefine1: { type: 'string' },
                    UserDefine2: { type: 'string' },
                    UserDefine3: { type: 'string' },
                    TotalTon: { type: 'number' },
                    TotalCBM: { type: 'number' },
                    TotalQuantity: { type: 'number' },
                    Code: { type: 'string' },
                    F_Cmd: { type: 'string' },
                    F_Empty: { type: 'string' },
                    ShortName: { type: 'string' },
                    CreatedBy: { type: 'string' },
                    TotalContainer20: { type: 'number' },
                    TotalContainer40: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        dataBound: function (e) { $scope.HasChoose = false; },
        columns: [
            {
                field: "F_Cmd", title: '{{RS.ORDOrder.F_Cmd}}', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,order_Grid,order_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,order_Grid,order_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'StatusOfOrderName', title: '{{RS.ORDOrder.StatusOfOrderName}}', width: 160, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfPlanName', title: '{{RS.ORDOrder.StatusOfPlanName}}', width: 160, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            //{
            //    field: 'IsWarning', width: CS['IsWarning'], title: '{{RS.ORDOrder_Index.IsWarning}}', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
            //    template: '<img class="img-warning" ng-click="warningtooltip.show()" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false
            //},
            {
                field: 'Code', width: CS['Code'], title: '{{RS.ORDOrder.Code}}', template: "<a class='action-text' href='\' ng-click='Detail_Click($event,dataItem)'>#=Code#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', width: CS['ServiceOfOrderName'], title: '{{RS.ORDOrder.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ShortName', width: CS['ShortName'], title: '{{RS.CUSCustomer.ShortName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreatedBy', width: CS['CreatedBy'], title: '{{RS.ORDOrder.CreatedBy}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeName', width: CS['TransportModeName'], title: '{{RS.ORDOrder.TransportModeName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExternalCode', width: CS['ExternalCode'], title: '{{RS.ORDOrder.ExternalCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExternalDate', width: CS['ExternalDate'], title: '{{RS.ORDOrder.ExternalDate}}', template: "#=ExternalDate==null?' ':kendo.toString(ExternalDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETD', width: CS['ETD'], title: '{{RS.ORDOrder.ETD}}', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: CS['ETA'], title: '{{RS.ORDOrder.ETA}}', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'TotalTon', width: CS['TotalTon'], title: '{{RS.ORDOrder.TotalTon}}', template: "#=TotalTon > 0 ? TotalTon : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalCBM', width: CS['TotalCBM'], title: '{{RS.ORDOrder.TotalCBM}}', template: "#=TotalCBM > 0 ? TotalCBM : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalQuantity', width: CS['TotalQuantity'], title: '{{RS.ORDOrder.TotalQuantity}}', template: "#=TotalQuantity > 0 ? TotalQuantity : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalContainer20', width: CS['TotalContainer20'], title: '{{RS.ORDOrder.TotalContainer20}}', filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalContainer40', width: CS['TotalContainer40'], title: '{{RS.ORDOrder.TotalContainer40}}', filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'LocationFrom', width: CS['LocationFrom'], title: '{{RS.ORDOrder.LocationFrom}}', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationTo', width: CS['StatusOfOrderName'], title: '{{RS.ORDOrder.LocationTo}}', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: CS['LocationFromAddress'], title: '{{RS.ORDOrder.LocationFromAddress}}', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: CS['LocationToAddress'], title: '{{RS.ORDOrder.LocationToAddress}}', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: CS['CustomerName'], title: '{{RS.ORDOrder.CustomerName}}', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: CS['RequestDate'], title: '{{RS.ORDOrder.RequestDate}}', template: "#=RequestDate==null?' ':kendo.toString(RequestDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'TypeOfContractName', width: CS['TypeOfContractName'], title: '{{RS.ORDOrder.TypeOfContractName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TripNo', width: CS['TripNo'], title: '{{RS.ORDOrder.TripNo}}', template: "#=TripNo==null?' ':TripNo#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VesselName', width: CS['VesselName'], title: '{{RS.ORDOrder.VesselName}}', template: "#=VesselName==null?' ':VesselName#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CutOffTime', width: CS['CutOffTime'], title: '{{RS.ORDOrder.CutOffTime}}', template: "#=CutOffTime==null?' ':kendo.toString(CutOffTime, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'CreatedDate', width: CS['CreatedDate'], title: '{{RS.ORDOrder.CreatedDate}}', template: "#=CreatedDate==null?' ':kendo.toString(CreatedDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'UserDefine1', width: CS['UserDefine1'], title: '{{RS.ORDOrder.UserDefine1}}', template: "#=UserDefine1==null?' ':UserDefine1#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine2', width: CS['UserDefine2'], title: '{{RS.ORDOrder.UserDefine2}}', template: "#=UserDefine2==null?' ':UserDefine2#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine3', width: CS['UserDefine3'], title: '{{RS.ORDOrder.UserDefine3}}', template: "#=UserDefine3==null?' ':UserDefine3#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfDocName', width: CS['TypeOfDocName'], title: '{{RS.ORDOrder.TypeOfDocName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfWAInspectionName', width: CS['TypeOfWAInspectionName'], title: '{{RS.ORDOrder.TypeOfWAInspectionName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfWAInspectionStatusName', width: CS['TypeOfWAInspectionStatusName'], title: '{{RS.ORDOrder.TypeOfWAInspectionStatusName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: "F_Empty", title: '', filterable: false, sortable: false }
        ]
    }
    
    $scope.order_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }
    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.order_Grid);
    }, 100);

    $scope.SchedulerEvent_Click = function ($event, uid, win) {
        $event.preventDefault();

        $scope.PacketID = uid;
        $scope.order_Grid.dataSource.read();
        win.center().open();
    }

    $scope.Detail_Click = function ($event, item) {
        $event.preventDefault();
        var view = '';
        var flag = false;
        switch (item.TypeOfView) {
            case 0:
                break;
            case 1:
                flag = true;
                view = "main.ORDOrder.FCLIMEX"
                break;
            case 2:
                flag = true;
                view = "main.ORDOrder.FCLLO"
                break;
            case 3:
                flag = true;
                view = "main.ORDOrder.FTLLO"
                break;
            case 4:
                flag = true;
                view = "main.ORDOrder.LTLLO"
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            default:
                break;
        }
        if (flag) {
            $state.go(view, {
                OrderID: item.ID,
                CustomerID: item.CustomerID,
                ServiceID: item.ServiceOfOrderID,
                TransportID: item.TransportModeID,
                ContractID: item.ContractID > 0 ? item.ContractID : -1
            });
        }
    }
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

}]);