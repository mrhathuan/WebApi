/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/views.js" />

//#region Data
var _ORDOrder = {
    URL: {
        Read: 'ORDOrder_List',
        OPS: 'ORDOrder_ToOPS',
        Copy: 'ORDOrder_Copy',
        OPS_Check: 'ORDOrder_ToOPSCheck',
        Delete_List: 'ORDOrder_DeleteList',
        OPS_Refresh_Route: 'ORDOrder_RoutingArea_Refresh',
        Container_OPS_Check: 'ORDOrderContainer_ToOPSCheck',
        Container_OPS_Update: 'ORDOrderContainer_ToOPSUpdate',
        Update_Warning: 'ORDOrder_UpdateWarning',
        Tender: 'ORDOrder_ToTender'
    },
    Data: {
        OPS: []
    }
};
//#endregion

angular.module('myapp').controller('ORDOrder_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    //QuickButton
    $rootScope.QuickAdd({
        Show: true,
        Call: function ($event) {
            $event.preventDefault();
            $state.go('main.ORDOrder.New');
            $scope.IsDetail = true;
        }
    });

    $scope.HasChoose = false;
    $scope.OrderID = -1;

    //#region View
    $scope.ShowDate = false;
    $scope.ParamRequest = {
        sStatus: [0],
        fDate: new Date(),
        tDate: new Date().addDays(1),
        aDate: false,
        typeDate: 0
    }

    try {
        var objCookie = JSON.parse(Common.Cookie.Get("ORDOrder_Index"));
        if (Common.HasValue(objCookie)) {
            $scope.ParamRequest.sStatus = objCookie.sStatus;
            $scope.ParamRequest.aDate = objCookie.aDate;
            $scope.ParamRequest.typeDate = objCookie.typeDate;
            switch (objCookie.typeDate) {
                case 0:
                    $scope.ParamRequest.fDate = new Date();
                    $scope.ParamRequest.tDate = new Date().addDays(1);
                    break;
                case 1:
                    $scope.ParamRequest.fDate = $scope.GetMonday(new Date());
                    $scope.ParamRequest.tDate = $scope.GetMonday(new Date().addDays(7));;
                    break;
                case 2:
                    var toDate = new Date();
                    $scope.ParamRequest.fDate = new Date(toDate.getFullYear(), toDate.getMonth(), 1);
                    $scope.ParamRequest.tDate = new Date(toDate.getFullYear(), toDate.getMonth() + 1, 0);
                    break;
                case 3:
                    break;
                case 4:
                    $scope.ParamRequest.fDate = Common.Date.FromJson(objCookie.fDate);
                    $scope.ParamRequest.tDate = Common.Date.FromJson(objCookie.tDate);
                    break;
                default:
                    break;
            }
        }
    }
    catch (e) { }

    $scope.OrderStatusBar = function (id, cusID, data) {
        if (!Common.HasValue(data) || data.length == 0)
            return '<div class="status-bar"><div class="status-bar-item status new" style="width:100%;"></div></div>';

        var total = data.length,
            perc = Math.round(100 / total),
            html = '<a class="status-bar" href="/" ui-sref="main.ORDOrder.TrackingView({ID:\'' + id + '&' + cusID + '\'})">',
            dataClasses = ['new', 'planning', 'approved', 'tendered', 'delivery', 'received', 'podreceived'];

        for (var i = 0; i < data.length; i++) {
            var item = data[i], border = "";

            if (i + 1 < total && item.Status == data[i + 1].Status && i != total - 1) {
                border = "with-border ";
            }
            var zIndex = total - (i + 1),
                position = "left: 0;",
                range = 0;
            if (i != 0 && i != total - 1 && total < 12)
                position = "left: calc(" + perc * i + "% - 6px);";
            else if (i != 0 && i != total - 1)
                position = "left: calc(" + perc * i + "% - 7px);";
            else if (i == total - 1)
                position = "right: 0;";

            if (i == 0 && total == 1)
                range = 0;
            else if (i == 0 && total < 11)
                range = 6;
            else if (i == 0)
                range = 4;
            else if (total > 13)
                range = 8
            else if (total != 1)
                range = 10;

            var c = item.Status == -1 ? "cancel" : dataClasses[item.Status];
            var cssToolTip = "";
            if (Common.HasValue(item.TOMasterCode) && item.TOMasterCode != "") {
                cssToolTip = "status-tooltip";
            }

            html += '<div data-value="' + item.TOMasterCode + '" class="status-bar-item status ' + c + ' ' + border
                + cssToolTip + '" style="width:calc(' + perc + '% + ' + range + 'px); ' + position + ' z-index:' + zIndex + '"></div>';
        }
        html += '</a>';
        return html;
    }

    $scope.order_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder.URL.Read,
            sort: [{ field: 'CreatedDate', dir: 'desc' }],
            pageSize: 20,
            readparam: function () {
                return $scope.ParamRequest
            },
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
                    TotalContainer40: { type: 'number' },
                    WarningTime: { type: 'number' },
                    TotalComment: { type: 'number' },
                }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.OrderID = obj.ID;
                var title = '<b>ĐH ' + obj.TransportModeName + '</b>: ' + obj.Code + ' - ' + obj.CustomerName;
                $rootScope.Comment_Show(obj.id, title);
            }
        },
        dataBound: function (e) {
            $scope.HasChoose = false;
        },
        columns: [
            {
                field: "Command", title: ' ', width: '35px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,order_grid,order_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,order_grid,order_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'StatusOfOrderName', width: 120, title: '{{RS.ORDOrder.StatusOfOrderName}}',
                template: function (e) { return $scope.OrderStatusBar(e.ID, e.CustomerID, e.ListShipmentStatus) },
                attributes: { style: "text-align: center; " },
                filterable: false, sortable: false, sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'IsWarning', width: 120, title: '{{RS.ORDOrder_Index.IsWarning}}', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" ng-click="warningtooltip.show()" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>',
                filterable: false, sortable: false, sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Code', width: 120, title: '{{RS.ORDOrder.Code}}', template: "<a class='action-text' href='\' ng-click='Detail_Click($event,dataItem)'>#=Code#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ServiceOfOrderName', width: 120, title: '{{RS.ORDOrder.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ShortName', width: 120, title: '{{RS.CUSCustomer.ShortName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CreatedBy', width: 120, title: '{{RS.ORDOrder.CreatedBy}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TransportModeName', width: 120, title: '{{RS.ORDOrder.TransportModeName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ExternalCode', width: 120, title: '{{RS.ORDOrder.ExternalCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ExternalDate', width: 120, title: '{{RS.ORDOrder.ExternalDate}}', template: "#=ExternalDate==null?' ':kendo.toString(ExternalDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: 120, title: '{{RS.ORDOrder.ETD}}', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: 120, title: '{{RS.ORDOrder.ETA}}', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TotalTon', width: 120, title: '{{RS.ORDOrder.TotalTon}}', template: "#=TotalTon > 0 ? TotalTon : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TotalCBM', width: 120, title: '{{RS.ORDOrder.TotalCBM}}', template: "#=TotalCBM > 0 ? TotalCBM : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TotalQuantity', width: 120, title: '{{RS.ORDOrder.TotalQuantity}}', template: "#=TotalQuantity > 0 ? TotalQuantity : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TotalContainer20', width: 120, title: '{{RS.ORDOrder.TotalContainer20}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TotalContainer40', width: 120, title: '{{RS.ORDOrder.TotalContainer40}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFrom', width: 120, title: '{{RS.ORDOrder.LocationFrom}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationTo', width: 120, title: '{{RS.ORDOrder.LocationTo}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromAddress', width: 120, title: '{{RS.ORDOrder.LocationFromAddress}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToAddress', width: 120, title: '{{RS.ORDOrder.LocationToAddress}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerName', width: 120, title: '{{RS.ORDOrder.CustomerName}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDate', width: 120, title: '{{RS.ORDOrder.RequestDate}}', template: "#=RequestDate==null?' ':kendo.toString(RequestDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfContractName', width: 120, title: '{{RS.ORDOrder.TypeOfContractName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 23, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TripNo', width: 120, title: '{{RS.ORDOrder.TripNo}}', template: "#=TripNo==null?' ':TripNo#",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 24, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'VesselName', width: 120, title: '{{RS.ORDOrder.VesselName}}', template: "#=VesselName==null?' ':VesselName#",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 25, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CutOffTime', width: 120, title: '{{RS.ORDOrder.CutOffTime}}', template: "#=CutOffTime==null?' ':kendo.toString(CutOffTime, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 26, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CreatedDate', width: 120, title: '{{RS.ORDOrder.CreatedDate}}', template: "#=CreatedDate==null?' ':kendo.toString(CreatedDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 27, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefine1', width: 120, title: '{{RS.ORDOrder.UserDefine1}}', template: "#=UserDefine1==null?' ':UserDefine1#",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 28, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefine2', width: 120, title: '{{RS.ORDOrder.UserDefine2}}', template: "#=UserDefine2==null?' ':UserDefine2#",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 29, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefine3', width: 120, title: '{{RS.ORDOrder.UserDefine3}}', template: "#=UserDefine3==null?' ':UserDefine3#",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 30, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfDocName', width: 120, title: '{{RS.ORDOrder.TypeOfDocName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 31, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfWAInspectionName', width: 120, title: '{{RS.ORDOrder.TypeOfWAInspectionName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 32, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfWAInspectionStatusName', width: 120, title: '{{RS.ORDOrder.TypeOfWAInspectionStatusName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 33, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'WarningTime', width: 120, title: '{{RS.ORDOrder.WarningTime}}', template: "#=WarningTime>0?Common.Number.ToNumber1(WarningTime):' '#",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 34, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'WarningText', width: 120, title: '{{RS.ORDOrder.WarningTimeText}}', template: "#=WarningText==null?' ':WarningText#",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 35, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TotalComment', width: 120, title: '{{RS.ORDOrder.TotalComment}}', attributes: { style: 'text-align: center;' },
                template: "<div class='displayComment' ng-show='dataItem.TotalComment>0' >{{dataItem.TotalComment}}</div>",
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 36, configurable: true, isfunctionalHidden: false
            },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.order_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.tooltipOptions = {
        filter: ".status-tooltip,.img-warning", position: "top",
        content: function (e) {
            return $(e.target).data('value');
        }
    }

    //WinCopy
    $scope.copy_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: false,
        columns: [
            { field: 'Code', title: '{{RS.ORDOrder.Code}}', },
            { field: 'NewCode', title: '{{RS.ORDOrder_Index.NewCode}}', template: '<input type="text" class="k-input k-textbox" ng-model="dataItem.NewCode">' }
        ]
    }

    $scope.routing_area_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        dataBound: function () {
            var grid = this;
            Common.Data.Each(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (item.ListArea.length == 0) {
                    $(tr).addClass('k-state-selected');
                }
            })
        },
        columns: [
            { field: 'Error', title: '{{RS.ORDOrder_Index.Error}}', width: 200 },
            {
                title: ' ', width: '90px', filterable: false, sortable: false,
                template: '<a ng-show="dataItem.ListArea.length > 0" href="/" ng-click="RoutingArea_Refresh($event,dataItem)" class="k-button k-i-refresh">Làm mới</a>'
            },
            { field: 'LocationFromName', title: '{{RS.ORDOrder_Index.LocationFromName}}', width: 100 },
            { field: 'LocationFromAddress', title: '{{RS.ORDOrder_Index.LocationFromAddress}}', width: 150 },
            { field: 'ProvinceFromName', title: '{{RS.ORDOrder_Index.ProvinceFromName}}', width: 100 },
            { field: 'DistrictFromName', title: '{{RS.ORDOrder_Index.DistrictFromName}}', width: 100 },
            { field: 'LocationToName', title: '{{RS.ORDOrder_Index.LocationToName}}', width: 100 },
            { field: 'LocationToAddress', title: '{{RS.ORDOrder_Index.LocationToAddress}}', width: 150 },
            { field: 'ProvinceToName', title: '{{RS.ORDOrder_Index.ProvinceToName}}', width: 100 },
            { field: 'DistrictToName', title: '{{RS.ORDOrder_Index.DistrictToName}}', width: 100 }
        ]
    }

    $scope.container_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        dataBound: function () {
            var grid = this;
            Common.Data.Each(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (item.DataPartnerLocation.length == 0) {
                    $(tr).addClass('k-state-selected');
                }
            })
        },
        columns: [
            { field: 'OrderCode', title: '{{RS.ORDOrder.Code}}', width: 150 },
            { field: 'CustomerCode', title: '{{RS.CUSCustomer.ShortName}}', width: 150 },
            { field: 'ServiceOfOrderName', title: '{{RS.ORDOrder.ServiceOfOrderName}}', width: 100 },
            {
                field: 'PartnerID', width: 120, title: 'Hãng tàu',
                template: function (e) {
                    if (e.PartnerID > 0) {
                        return e.PartnerCode;
                    } else {
                        return ' <select ng-model="dataItem.PartnerID" kendo-combo-box k-data-text-field="\'PartnerCode\'" '
                        + 'k-data-value-field="\'CUSPartnerID\'"  k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" '
                        + 'k-on-data-bound="cboPartnerDataBound(kendoEvent, dataItem)" k-data-source="dataItem.DataPartner"'
                        + 'k-on-change="cboPartnerChange(kendoEvent, dataItem)" style="width: 100%"></select>';
                    }
                }
            },
            {
                field: 'LocationDepotID', width: 260, title: 'Bãi container',
                template: '<select ng-model="dataItem.LocationDepotID" kendo-combo-box k-data-text-field="\'LocationName\'" '
                    + 'k-data-value-field="\'CUSLocationID\'"  k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" '
                    + 'k-on-data-bound="cboLocationDataBound(kendoEvent, dataItem)" k-data-source="dataItem.DataPartnerLocation" style="width: 100%"></select>'
            },
            { title: ' ', sortable: false }
        ]
    }

    $scope.cboPartnerDataBound = function ($kendoEvent, item) {
        if (item && !item.dirty) {
            $kendoEvent.sender.select(function (o) { return o.ID == item.PartnerID });
            item.dirty = true;
        }
    }

    $scope.cboPartnerChange = function ($kendoEvent, item) {
        var obj = $kendoEvent.sender.dataItem($kendoEvent.sender.select());
        if (Common.HasValue(obj)) {
            $timeout(function () {
                var data = $.grep(item.DataLocation, function (o) { return o.CusPartID == obj.CUSPartnerID });
                item.LocationPartnerData = $.extend(true, [], data);
            })
        }
    }

    $scope.cboLocationDataBound = function ($kendoEvent, item) {
        if (item && !item.dirty) {
            $kendoEvent.sender.select(function (o) { return o.CUSLocationID == item.LocationDepotID });
            item.dirty = true;
        }
    }
    //#endregion

    //#region Tender
    $scope.tender_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ETDRequest: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateReturnEmpty: { type: 'date' },
                }
            },
            pageSize: 20,
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                field: "Command", title: ' ', width: '35px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,tender_grid,tender_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,tender_grid,tender_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'OrderCode', width: 120, title: '{{RS.ORDOrder.Code}}', sortable: true, },
            { field: 'CustomerCode', width: 120, title: '{{RS.CUSCustomer.Code}}', sortable: true, },
            { field: 'PackingID', width: 120, title: '{{RS.ORDContainer.PackingID}}', template: '#=PackingName#', sortable: true, },
            { field: 'LocationFromID', width: 170, title: '{{RS.ORDContainer.LocationFromID}}', template: '#=LocationFromName#', sortable: true, },
            { field: 'LocationToID', width: 170, title: '{{RS.ORDContainer.LocationToID}}', template: '#=LocationToName#', sortable: true, },
            { field: 'DateGetEmpty', width: 170, title: '{{RS.ORDContainer.DateGetEmpty}}', template: '#=Common.Date.FromJsonDMYHM(DateGetEmpty)#', sortable: true, },
            { field: 'DateReturnEmpty', width: 170, title: '{{RS.ORDContainer.DateReturnEmpty}}', template: '#=Common.Date.FromJsonDMYHM(DateReturnEmpty)#', sortable: true, },
            { field: 'LoadingTime', width: 100, title: '{{RS.ORDContainer.LoadingTime}}', },
            { field: 'UnLoadingTime', width: 100, title: '{{RS.ORDContainer.UnLoadingTime}}', },
            { field: 'ETD', width: 170, title: '{{RS.ORDContainer.ETD}}', template: '#=Common.Date.FromJsonDMYHM(ETD)#', sortable: true, },
            { field: 'ETA', width: 170, title: '{{RS.ORDContainer.ETA}}', template: '#=Common.Date.FromJsonDMYHM(ETA)#', sortable: true, },
            { field: 'ContainerNo', width: 100, title: '{{RS.ORDContainer.ContainerNo}}', },
            { field: 'Ton', width: 100, title: '{{RS.ORDContainer.Ton}}', },
            { field: 'SealNo1', width: 100, title: '{{RS.ORDContainer.SealNo1}}', },
            { field: 'SealNo2', width: 100, title: '{{RS.ORDContainer.SealNo2}}' },
            { field: 'Note', title: '{{RS.ORDContainer.Note}}', width: 300 },
            { field: 'Note1', title: '{{RS.ORDContainer.Note1}}', width: 300 },
            { field: 'Note2', title: '{{RS.ORDContainer.Note2}}', width: 300 },
        ]
    }

    $scope.tender_gridChoose_Change = function ($event, grid, hasChoose) {

    }
    //#endregion

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }

    $scope.Comment_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = grid.select();
        var id = -1;
        var tx = null;
        if (Common.HasValue(tr) && tr.length > 0) {
            var item = grid.dataItem(tr[0]);
            tx = '<b>ĐH ' + item.TransportModeName + '</b>: ' + item.Code + ' - ' + item.CustomerName;
            id = item.ID;
        }
        try {
            $rootScope.Comment({ Type: Common.Comment.ORD, ReferID: id, Title: tx });
        } catch (e) {
        }
    };

    $scope.OPS_Click = function ($event, grid, win) {
        $event.preventDefault();

        _ORDOrder.Data.OPS = [];
        var IsEmpty = true;
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true && v.IsOPS == true) _ORDOrder.Data.OPS.push(v.ID);
            if (v.IsChoose == true) IsEmpty = false;
        });
        if (_ORDOrder.Data.OPS.length > 0) {
            // $rootScope.IsLoading = true;
            $rootScope.Loading.Show();
            $rootScope.Loading.Change("Đang kiểm tra dữ liệu...", 20);
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder.URL.Container_OPS_Check,
                data: { data: _ORDOrder.Data.OPS },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.IsLoading = false;
                        if (res.length > 0) {
                            $rootScope.Loading.Hide()
                            $scope.container_win.center().open();
                            $scope.container_GridOptions.dataSource.data(res);
                            $rootScope.Message({ Msg: "Thiếu dữ liệu container!" });
                        } else {
                            // $rootScope.IsLoading = true;
                            $rootScope.Loading.Change("Đang kiểm tra dữ liệu...", 50);
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDOrder.URL.OPS_Check,
                                data: { lst: _ORDOrder.Data.OPS },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.Loading.Hide()
                                        $rootScope.IsLoading = false;
                                        if (res.length > 0) {

                                            $.each(res, function (i, v) {
                                                if (v.ListArea.length == 0)
                                                    v.Error = "Chưa thiết lập cung đường";
                                                else
                                                    v.Error = "Chưa làm mới cung đường";
                                            });
                                            win.center().open();
                                            $scope.routing_area_gridOptions.dataSource.data(res);
                                            $rootScope.Message({ Msg: "Thiếu dữ liệu thiết lập cung đường!" });
                                        } else {
                                            $rootScope.Message({
                                                Type: Common.Message.Type.Confirm,
                                                Msg: "Bạn muốn gửi điều phối các dữ liệu đã chọn?",
                                                Ok: function () {
                                                    //  $rootScope.IsLoading = true;

                                                    $rootScope.Loading.Show();
                                                    $rootScope.Loading.Change("Đang gửi điều phối...", 50);

                                                    Common.Services.Call($http, {
                                                        url: Common.Services.url.ORD,
                                                        method: _ORDOrder.URL.OPS,
                                                        data: { lst: _ORDOrder.Data.OPS },
                                                        success: function (res) {
                                                            Common.Services.Error(res, function (res) {
                                                                if (res.length > 0) {
                                                                    Common.Data.Each(res, function (o) {
                                                                        o.IsChoose = false;
                                                                    })
                                                                    $scope.tender_gridOptions.dataSource.data(res);
                                                                    $scope.tender_win.center();
                                                                    $scope.tender_win.open();
                                                                }
                                                                $rootScope.Loading.Change("Đang gửi điều phối...", 80);
                                                                $rootScope.Loading.Hide();
                                                                grid.dataSource.read();
                                                                $rootScope.IsLoading = false;
                                                            })
                                                        }
                                                    })
                                                }
                                            });
                                        }
                                    })
                                }
                            })
                        }
                    })
                }
            })
        }
        else {
            if (!IsEmpty) {
                $rootScope.Message({ Msg: 'Đơn hàng không được gửi điều phối', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn đơn hàng', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.Del_Click = function ($event, grid) {
        $event.preventDefault();

        var lstID = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                lstID.push(v.ID);
        });
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn xóa các đơn hàng đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder.URL.Delete_List,
                        data: { lstID: lstID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã xóa!" });
                                grid.dataSource.read();
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.Copy_Click = function ($event, grid, win, gridcopy) {
        $event.preventDefault();

        var lstObj = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstObj.push({ ID: v.ID, Code: v.Code, NewCode: v.Code + "_Copy" });
        });
        if (lstObj.length > 0) {
            gridcopy.dataSource.data(lstObj);
            win.center().open();
            gridcopy.refresh();
        }
    }

    $scope.Copy_Save_Click = function ($event, grid, win, gridmain) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var flag = true;
        var lstCode = [];
        $.each(data, function (i, v) {
            v.Value = v.NewCode;
            if (v.NewCode == "" || v.NewCode == null) {
                flag = false;
                lstCode.push(v.Code);
            }
        })
        if (!flag) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: "Nhập mã đơn hàng mới cho các đơn hàng: " + lstCode.join(", ")
            });
        } else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn sao chép các đơn hàng đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder.URL.Copy,
                        data: { lst: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                gridmain.dataSource.read();
                                win.close();
                            })
                        }
                    })
                }
            });
        }
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
                flag = true;
                view = "main.ORDOrder.FCLLOEmpty"
                break;
            case 6:
                flag = true;
                view = "main.ORDOrder.FCLLOLaden"
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
                ServiceID: item.ServiceOfOrderID || -1,
                TransportID: item.TransportModeID || -1,
                ContractID: item.ContractID > 0 ? item.ContractID : -1,
                TermID: item.ContractTermID > 0 ? item.ContractTermID : -1
            });
        }
    }

    $scope.ViewStatus_Click = function ($event, grid) {
        $event.preventDefault();
        try {
            var flag = true;
            var value = $($event.currentTarget).data('tabindex');
            var index = $scope.ParamRequest.sStatus.indexOf(value);
            if (index > -1) {
                if ($scope.ParamRequest.sStatus.length == 1) {
                    flag = false;
                }
                else {
                    $scope.ParamRequest.sStatus.splice(index, 1);
                }
            } else {
                $scope.ParamRequest.sStatus.push(value);
            }
            if (flag) {
                Common.Cookie.Set("ORDOrder_Index", JSON.stringify({
                    typeDate: $scope.ParamRequest.typeDate,
                    sStatus: $scope.ParamRequest.sStatus,
                    aDate: $scope.ParamRequest.aDate,
                    fDate: $scope.ParamRequest.fDate,
                    tDate: $scope.ParamRequest.tDate
                }));
                grid.dataSource.page(1);
                grid.dataSource.read();
            }
        }
        catch (e) { }
    }

    $scope.ViewDate_Click = function ($event, grid) {
        $event.preventDefault();
        try {
            var flag = true;
            var tabIndex = $($event.target).data('tabindex');
            if (Common.HasValue(tabIndex)) {
                $scope.ParamRequest.typeDate = tabIndex;
                switch (tabIndex) {
                    case 0:
                        $scope.ParamRequest.fDate = new Date();
                        $scope.ParamRequest.tDate = new Date().addDays(1);
                        $scope.ParamRequest.aDate = false;
                        $scope.ShowDate = false;
                        break;
                    case 1:
                        $scope.ParamRequest.fDate = $scope.GetMonday(new Date());
                        $scope.ParamRequest.tDate = $scope.GetMonday(new Date().addDays(7));;
                        $scope.ParamRequest.aDate = false;
                        $scope.ShowDate = false;
                        break;
                    case 2:
                        var toDate = new Date();
                        $scope.ParamRequest.fDate = new Date(toDate.getFullYear(), toDate.getMonth(), 1);
                        $scope.ParamRequest.tDate = new Date(toDate.getFullYear(), toDate.getMonth() + 1, 0);
                        $scope.ParamRequest.aDate = false;
                        $scope.ShowDate = false;
                        break;
                    case 3:
                        $scope.ParamRequest.aDate = true;
                        $scope.ShowDate = false;
                        break;
                    case 4:
                        $scope.ShowDate = !$scope.ShowDate;
                        $scope.ParamRequest.aDate = false;
                        flag = false;
                    default:
                        break;
                }

                Common.Cookie.Set("ORDOrder_Index", JSON.stringify({
                    typeDate: $scope.ParamRequest.typeDate,
                    sStatus: $scope.ParamRequest.sStatus,
                    aDate: $scope.ParamRequest.aDate,
                    fDate: $scope.ParamRequest.fDate,
                    tDate: $scope.ParamRequest.tDate
                }));
                if (flag) {
                    grid.dataSource.page(1);
                    grid.dataSource.read();
                }

            }
        }
        catch (e) { }
    }

    $scope.ViewDate_Options_Click = function ($event, grid) {
        $event.preventDefault();

        Common.Cookie.Set("ORDOrder_Index", JSON.stringify({
            typeDate: $scope.ParamRequest.typeDate,
            sStatus: $scope.ParamRequest.sStatus,
            aDate: $scope.ParamRequest.aDate,
            fDate: $scope.ParamRequest.fDate,
            tDate: $scope.ParamRequest.tDate
        }));
        $scope.ShowDate = false;
        grid.dataSource.page(1);
        grid.dataSource.read();
    }

    $scope.RoutingArea_Refresh = function ($event, item) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder.URL.OPS_Refresh_Route,
            data: { lst: item.ListArea },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.IsLoading = false;
                    item.ListArea = [];
                    $scope.routing_area_gridOptions.dataSource.sync();
                })
            }
        })
    }

    $scope.RoutingArea_Refresh_All = function ($event, grid) {
        $event.preventDefault();

        var datasend = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.ListArea.length > 0) {
                $.each(v.ListArea, function (j, areaItem) {
                    if (datasend.length == 0) {
                        datasend.push(areaItem);
                    }
                    else {
                        var hasId = false;
                        $.each(datasend, function (i, lstItem) {
                            if (areaItem == lstItem) {
                                hasId = true;
                            }
                        });
                        if (!hasId) {
                            datasend.push(areaItem);
                        }
                    }
                });
            }
        });

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder.URL.OPS_Refresh_Route,
            data: { lst: datasend },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $.each(data, function (i, v) {
                        if (v.ListArea.length > 0) {
                            v.ListArea = [];
                        }
                    });
                    $rootScope.IsLoading = false;
                    $scope.routing_area_gridOptions.dataSource.sync();
                })
            }
        })
    }

    $scope.Container_ToOPS_Update = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder.URL.Container_OPS_Update,
            data: { data: grid.dataSource.data() },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    win.close();
                    $rootScope.Message({ Msg: "Thành công! Đang kiểm tra dữ liệu trước khi gửi điều phối..." });
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder.URL.OPS_Check,
                        data: { lst: _ORDOrder.Data.OPS },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                if (res.length > 0) {
                                    $rootScope.Message({ Msg: "Thiếu dữ liệu thiết lập cung đường!" });
                                    $.each(res, function (i, v) {
                                        if (v.ListArea.length == 0)
                                            v.Error = "Chưa thiết lập cung đường";
                                        else
                                            v.Error = "Chưa làm mới cung đường";
                                    });
                                    $scope.routing_area_win.center().open();
                                    $scope.routing_area_gridOptions.dataSource.data(res);
                                } else {
                                    $rootScope.Message({
                                        Type: Common.Message.Type.Confirm,
                                        Msg: "Bạn muốn gửi điều phối các dữ liệu đã chọn?",
                                        Ok: function () {
                                            $rootScope.IsLoading = true;
                                            Common.Services.Call($http, {
                                                url: Common.Services.url.ORD,
                                                method: _ORDOrder.URL.OPS,
                                                data: { lst: _ORDOrder.Data.OPS },
                                                success: function (res) {
                                                    Common.Services.Error(res, function (res) {
                                                        $scope.order_grid.dataSource.read();
                                                        $rootScope.IsLoading = false;
                                                    })
                                                }
                                            })
                                        }
                                    });
                                }
                            })
                        }
                    })
                })
            }
        })
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.UpdateWarning_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn kiểm tra thời gian đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder.URL.Update_Warning,
                    data: {},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        grid.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.OrderToTender_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true });

        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn gửi tender dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.Loading.Show();
                    $rootScope.Loading.Change("Đang gửi tender...", 50);
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder.URL.Tender,
                        data: { lst: data },
                        success: function (res) {
                            win.close();
                            $rootScope.Loading.Hide();
                            $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        } else {
            win.close();
        }
    };

    //#endregion

    $scope.GetMonday = function (d) {
        d = new Date(d);
        var day = d.getDay(),
            diff = d.getDate() - day + (day == 0 ? -6 : 1);
        return new Date(d.setDate(diff));
    }
}]);