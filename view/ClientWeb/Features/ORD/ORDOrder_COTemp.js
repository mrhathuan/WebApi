/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _ORDOrder_COTemp = {
    URL: {
        List: "ORD_COTemp_List",
        Data: "ORD_COTemp_Data",
        ToORD: "ORD_COTemp_ToORD",
        Update: "ORD_COTemp_Update",
        Delete: "ORD_COTemp_DeleteList",
        Save_List: "ORD_COTemp_SaveList",
        Carrier_List: "ORD_COTemp_Carrier_List",
        Location_List: "ORD_COTemp_Location_List",
    },
    Data: {
        ListPacking: [],
    }
}

//#endregion

angular.module('myapp').controller('ORDOrder_COTempCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('ORDOrder_COTempCtrl');
    $rootScope.IsLoading = false;

    $scope.HasChoose = false;
    $scope.IsNewContainer = false;
    $scope.CarrierID = -1;
    $scope.CustomerID = -1;
    $scope.LocationType = -1;
    $scope.TransportModeID = -1;
    $scope.ServiceOfOrderID = -1;
    $scope.TypeOfService = -1;
    $scope.Service = {
        iImport: 26,
        iExport: 27,
        iLocal: 28,
        iEmpty: 29,
        iLaden: 30,
    };

    $scope.NumOfGroup = 0;

    $scope.EditItem = null;
    $scope.NewItem = {
        ID: -1, OrderCode: "", PackingID: "", PackingName: "", CustomerID: "", CustomerCode: "", ServiceOfOrderID: "", ServiceOfOrderName: "",
        TransportModeID: "", TransportModeName: "", RequestDate: new Date(), CreatedDate: new Date(), CreatedBy: "", ModifiedDate: "", ModifiedBy: "",
        PartnerID: "", ContainerNo: "", SealNo1: "", SealNo2: "", LocationToID: "", LocationFromID: "", ETD: new Date(), ETA: new Date().addDays(2 / 24),
        LocationDepotID: "", LocationDepotReturnID: "", DateGetEmpty: null, DateReturnEmpty: null, Quantity: 1, CutOffTime: new Date().addDays(5)
    }

    $scope.IsGenCode=false;

    $scope.con_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_COTemp.URL.List,
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false, editable: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    CustomerCode: { type: 'string', editable: false },
                    TransportModeName: { type: 'string', editable: false },
                    ServiceOfOrderName: { type: 'string', editable: false },
                    PackingID: { type: 'number', editable: false },
                    PackingName: { type: 'string' },
                    LocationFromCode: { type: 'string', editable: false },
                    LocationToCode: { type: 'string', editable: false },
                    PartnerCode: { type: 'string', editable: false },
                    LocationDepotCode: { type: 'string', editable: false },
                    LocationDepotReturnCode: { type: 'string', editable: false },
                    RequestDate: { type: 'date', editable: false },
                    CutOffTime: { type: 'date' },
                    CreatedDate: { type: 'date' },
                    ModifiedDate: { type: 'date' },
                    DateShipCome: { type: 'date' },
                    DateDocument: { type: 'date' },
                    DateInspect: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateLoading: { type: 'date' },
                    DateReturnEmpty: { type: 'date' },
                    DateUnloading: { type: 'date' },
                    Note: { type: 'string' },
                    Note1: { type: 'string' },
                    Note2: { type: 'string' },
                    IsClosed: { type: 'bool' }
                }
            }
        }),
        height: '99%', groupable: true, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: false, editable: 'incell',
        dataBound: function () {
            $scope.HasChoose = false;
            $scope.NumOfGroup = this.dataSource.group().length;
        },
        save: function (e) {
            var grid = this, myfield = '', myValue = '';
            for (f in e.values) { myfield = f; } myValue = e.values[myfield];
            if (myfield == "OrderCode" && myValue == "") {
                e.preventDefault(); return;
            }
            else if (myfield == "PackingName") {
                var flag = false;
                Common.Data.Each(_ORDOrder_COTemp.Data.ListPacking, function (o) {
                    if (o.PackingName == myValue)
                        flag = true;
                })
                if (!flag) {
                    e.preventDefault(); return;
                }
            }
            $timeout(function () {
                var item = e.model;
                if (myfield == "PackingName") {
                    Common.Data.Each(_ORDOrder_COTemp.Data.ListPacking, function (o) {
                        if (o.PackingName == myValue) { item.PackingID = o.ID; }
                    })
                }
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_COTemp.URL.Update,
                    data: { item: item },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            grid.dataSource.read();
                        })
                    }
                })
            }, 1)
        },
        columns: [
            {
                field: "IsChoose", title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,con_Grid,con_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,con_Grid,con_GridChoose_Change)" />',
                filterable: false, sortable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'RequestDate', width: 120, title: '{{RS.ORDContainerTemp.RequestDate}}', template: "#=RequestDate==null?' ':kendo.toString(RequestDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.RequestDate}}: #=value#',
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                sortorder: 2, configurable: true, isfunctionalHidden: false,
                field: 'CustomerCode', width: 150, title: '{{RS.ORDContainerTemp.CustomerCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.CustomerCode}}: #=value#'
            },
            {
                sortorder: 3, configurable: true, isfunctionalHidden: false,
                field: 'TransportModeName', width: 100, title: '{{RS.ORDContainerTemp.TransportModeName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.TransportModeName}}: #=value#'
            },
            {
                sortorder: 4, configurable: true, isfunctionalHidden: false,
                field: 'ServiceOfOrderName', width: 80, title: '{{RS.ORDContainerTemp.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.ServiceOfOrderName}}: #=value#'
            },
            {
                sortorder: 5, configurable: true, isfunctionalHidden: false,
                field: 'OrderCode', width: 150, title: '{{RS.ORDContainerTemp.OrderCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.OrderCode}}: #=value#'
            },
            {
                sortorder: 6, configurable: true, isfunctionalHidden: false,
                field: 'LocationFromCode', width: 150, title: '{{RS.ORDContainerTemp.LocationFromCode}}',
                template: '<a href="/" ng-click="Location_Select($event,dataItem,1,location_win)" class="my-button">{{dataItem.LocationFromCode}}</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.LocationFromCode}}: #=value#'
            },
            {
                sortorder: 7, configurable: true, isfunctionalHidden: false,
                field: 'LocationToCode', width: 150, title: '{{RS.ORDContainerTemp.LocationToCode}}',
                template: '<a href="/" ng-click="Location_Select($event,dataItem,2,location_win)" class="my-button">{{dataItem.LocationToCode}}</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.LocationToCode}}: #=value#'
            },
            {
                sortorder: 8, configurable: true, isfunctionalHidden: false,
                field: 'PackingName', width: 80, title: '{{RS.ORDContainerTemp.PackingName}}', template: "#=PackingName#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoComboBox({
                        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
                        dataTextField: 'PackingName', dataValueField: 'PackingName', minLength: 3,
                        dataSource: Common.DataSource.Local({
                            data: _ORDOrder_COTemp.Data.ListPacking, model: { id: 'ID', fields: { ID: { type: 'number' }, PackingName: { type: 'string' } } }
                        })
                    });
                },
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.PackingName}}: #=value#'
            },
            {
                sortorder: 9, configurable: true, isfunctionalHidden: false,
                field: 'ContainerNo', width: 80, title: '{{RS.ORDContainerTemp.ContainerNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.ContainerNo}}: #=value#'
            },
            {
                sortorder: 10, configurable: true, isfunctionalHidden: false,
                field: 'SealNo1', width: 80, title: '{{RS.ORDContainerTemp.SealNo1}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.SealNo1}}: #=value#'
            },
            {
                sortorder: 11, configurable: true, isfunctionalHidden: false,
                field: 'ETD', width: 165, title: '{{RS.ORDContainerTemp.ETD}}', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.ETD}}: #=value#'
            },
            {
                sortorder: 12, configurable: true, isfunctionalHidden: false,
                field: 'ETA', width: 165, title: '{{RS.ORDContainerTemp.ETA}}', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.ETA}}: #=value#'
            },
            {
                sortorder: 13, configurable: true, isfunctionalHidden: false,
                field: 'PartnerCode', width: 120, title: '{{RS.ORDContainerTemp.PartnerCode}}',
                template: '<a href="/" ng-click="Carrier_Select($event,dataItem,carrier_win)" class="my-button">{{dataItem.PartnerCode}}</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.PartnerCode}}: #=value#'
            },
            {
                sortorder: 14, configurable: true, isfunctionalHidden: false,
                field: 'LocationDepotCode', width: 150, title: '{{RS.ORDContainerTemp.LocationDepotCode}}',
                template: '<a href="/" ng-click="Location_Select($event,dataItem,3,location_win)" class="my-button">{{dataItem.LocationDepotCode}}</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.LocationDepotCode}}: #=value#'
            },
            {
                sortorder: 15, configurable: true, isfunctionalHidden: false,
                field: 'LocationDepotReturnCode', width: 150, title: '{{RS.ORDContainerTemp.LocationDepotReturnCode}}',
                template: '<a href="/" ng-click="Location_Select($event,dataItem,4,location_win)" class="my-button">{{dataItem.LocationDepotReturnCode}}</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.LocationDepotReturnCode}}: #=value#'
            },
            {
                sortorder: 16, configurable: true, isfunctionalHidden: false,
                field: 'DateGetEmpty', width: 165, title: '{{RS.ORDContainerTemp.DateGetEmpty}}', template: "#=DateGetEmpty==null?' ':kendo.toString(DateGetEmpty, '" + Common.Date.Format.DMYHM + "')#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.DateGetEmpty}}: #=value#'
            },
            {
                sortorder: 17, configurable: true, isfunctionalHidden: false,
                field: 'DateReturnEmpty', width: 165, title: '{{RS.ORDContainerTemp.DateReturnEmpty}}', template: "#=DateReturnEmpty==null?' ':kendo.toString(DateReturnEmpty, '" + Common.Date.Format.DMYHM + "')#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.DateReturnEmpty}}: #=value#'
            },
            {
                sortorder: 18, configurable: true, isfunctionalHidden: false,
                field: 'CutOffTime', width: 165, title: '{{RS.ORDContainerTemp.CutOffTime}}', template: "#=CutOffTime==null?' ':kendo.toString(CutOffTime, '" + Common.Date.Format.DMYHM + "')#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.CutOffTime}}: #=value#'
            },
            {
                sortorder: 19, configurable: true, isfunctionalHidden: false,
                field: 'DateDocument', width: 165, title: '{{RS.ORDContainerTemp.DateDocument}}', template: "#=DateDocument==null?' ':kendo.toString(DateDocument, '" + Common.Date.Format.DMYHM + "')#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.DateDocument}}: #=value#'
            },
            {
                sortorder: 20, configurable: true, isfunctionalHidden: false,
                field: 'Note', width: 165, title: '{{RS.ORDContainerTemp.Note}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.Note}}: #=value#'
            },
            {
                sortorder: 21, configurable: true, isfunctionalHidden: false,
                field: 'Note1', width: 165, title: '{{RS.ORDContainerTemp.Note1}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.Note1}}: #=value#'
            },
            {
                sortorder: 22, configurable: true, isfunctionalHidden: false,
                field: 'Note2', width: 165, title: '{{RS.ORDContainerTemp.Note2}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: '<input type="checkbox" ng-show="NumOfGroup==1" ng-click="selectGroup_Click($event,con_Grid)"> {{RS.ORDContainerTemp.Note2}}: #=value#'
            },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false, }
        ]
    }

    $scope.con_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.selectGroup_Click = function ($event, grid) {
        var val = $event.target.checked;
        var groupRow = ($event.currentTarget).closest('tr');
        var lstSelect = grid.dataItem(groupRow).parent();
        angular.forEach(lstSelect,function (o,i) {
            o.IsChoose=val;
        })
        //grid.dataSource.sync();
        
            var flag=false;
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose == true) {
                    flag = true;
                    $(tr).find('td input.chkChoose').prop('checked', true);
                    if (!$(tr).hasClass('IsChoose'))
                        $(tr).addClass('IsChoose');
                }
                else {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    if ($(tr).hasClass('IsChoose'))
                        $(tr).removeClass('IsChoose');
                }
            });
            $scope.HasChoose=flag
        debugger
    };

    $scope.location_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_COTemp.URL.Location_List,
            readparam: function () {
                return {
                    cusID: $scope.CustomerID, carID: $scope.CarrierID,
                    serID: $scope.ServiceOfOrderID, transID: $scope.TransportModeID,
                    nLocation: $scope.LocationType > 2 ? 0 : $scope.LocationType
                }
            },
            pageSize: 100,
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        columns: [
            {
                field: 'Choose', title: ' ', width: '45px', filterable: false, sortable: false,
                template: '<a class="k-button" title="Chọn" href="/" ng-click="Location_Select_OK($event,dataItem,location_win)"><i class="fa fa-check"></i></a>'
            },
            {
                field: 'LocationCode', width: 150, title: '{{RS.CUSLocation.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', width: 250, title: '{{RS.CUSLocation.LocationName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '{{RS.CATLocation.Address}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }

    $scope.carrier_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSPartnerID',
                fields: {
                    CUSPartnerID: { type: 'number' },
                    PartnerCode: { type: 'string' },
                    PartnerName: { type: 'string' }
                }
            },
            pageSize: 0
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        columns: [
            {
                field: 'Choose', title: ' ', width: '45px', filterable: false, sortable: false,
                template: '<a class="k-button" title="Chọn" href="/" ng-click="Carrier_Select_OK($event,dataItem,carrier_win)"><i class="fa fa-check"></i></a>'
            },
            {
                field: 'PartnerCode', width: 150, title: '{{RS.CUSPartner.PartnerCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: '{{RS.CATPartner.PartnerName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, CustomerName: { type: 'string' } } }
        })
    }

    $scope.cboService_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, ValueOfVar: { type: 'string' } } }
        }),
        change: function (e) {
            var cbx = this;
            if (e.sender.selectedIndex >= 0) {
                var object = cbx.dataItem(cbx.select());
                if (object != null) {
                    $scope.TypeOfService = object.TypeOfVar;
                }
            }
        }
    }

    $scope.cboPacking_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'PackingName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, PackingName: { type: 'string' } } }
        })
    }

    $scope.cboTransport_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, ValueOfVar: { type: 'string' } } }
        })
    }

    $scope.cboCarrier_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'PartnerCode', dataValueField: 'CUSPartnerID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'CUSPartnerID', fields: { CUSPartnerID: { type: 'number' }, PartnerCode: { type: 'string' } } }
        })
    }

    $scope.conQuantity_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n0", decimal: 0
    }

    $scope.New_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
        $scope.IsNewContainer = true;
        $scope.LoadDataCarrier($scope.NewItem.CustomerID);
        $timeout(function () {
            angular.element("#txtCode").focus();
        }, 500)
    }

    $scope.Delete_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose)
                data.push(o.ID);
        })
        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận xóa các dòng đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_COTemp.URL.Delete,
                        data: { data: data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                });
                                grid.dataSource.read();
                            })
                        }
                    })
                }
            })
        }
    }

    $scope.Save_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose)
                data.push(o.ID);
        })
        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận lưu các dòng đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $timeout(function () {
                        $rootScope.Message({
                            Type: Common.Message.Type.Confirm,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Bạn muốn tự tạo mã đơn hàng cho dữ liệu đã chọn?',
                            Close: function () {
                                $scope.IsGenCode = false;
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.ORD,
                                    method: _ORDOrder_COTemp.URL.ToORD,
                                    data: { data: data, isGenCode: $scope.IsGenCode },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $rootScope.IsLoading = false;
                                            $rootScope.Message({
                                                Msg: 'Thành công!'
                                            });
                                            grid.dataSource.read();
                                        })
                                    }
                                })
                            },
                            Ok: function () {
                                $scope.IsGenCode = true;
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.ORD,
                                    method: _ORDOrder_COTemp.URL.ToORD,
                                    data: { data: data, isGenCode: $scope.IsGenCode },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $rootScope.IsLoading = false;
                                            $rootScope.Message({
                                                Msg: 'Thành công!'
                                            });
                                            grid.dataSource.read();
                                        })
                                    }
                                })
                            }
                        })
                    },1)
                }
            })
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrder_COTemp.URL.Data,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _ORDOrder_COTemp.Data.ListPacking = res.ListPacking;
                $scope.cboPacking_Options.dataSource.data(res.ListPacking);
                if (res.ListPacking != null && res.ListPacking[0] != null) {
                    $scope.NewItem.PackingID = res.ListPacking[0].ID;
                }
                $scope.cboCustomer_Options.dataSource.data(res.ListCustomer);
                if (res.ListCustomer != null && res.ListCustomer[0] != null) {
                    $scope.NewItem.CustomerID = res.ListCustomer[0].ID;
                }
                $scope.cboService_Options.dataSource.data(res.ListServiceOfOrder);
                if (res.ListServiceOfOrder != null && res.ListServiceOfOrder[0] != null) {
                    $scope.NewItem.ServiceOfOrderID = res.ListServiceOfOrder[0].ID;
                    $scope.TypeOfService = res.ListServiceOfOrder[0].TypeOfVar;
                }
                $scope.cboTransport_Options.dataSource.data(res.ListTransportMode);
                if (res.ListTransportMode != null && res.ListTransportMode[0] != null) {
                    $scope.NewItem.TransportModeID = res.ListTransportMode[0].ID;
                }
            })
        }
    })

    $scope.LoadDataCarrier = function (cusID) {
        if (cusID == "" || cusID < 1)
            cusID = -1;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_COTemp.URL.Carrier_List,
            data: { cusID: cusID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $timeout(function () {
                        if ($scope.IsNewContainer) {
                            $scope.NewItem.PartnerID = "";
                            $scope.cboCarrier_Options.dataSource.data(res);
                        } else {
                            $scope.carrier_Grid_Options.dataSource.data(res);
                        }
                    }, 1)
                })
            }
        })
    }

    $scope.$watch("NewItem.CustomerID", function () {
        $scope.NewItem.LocationFromID = "";
        $scope.NewItem.LocationFromCode = "";
        $scope.NewItem.LocationToID = "";
        $scope.NewItem.LocationToCode = "";
        $scope.LoadDataCarrier($scope.NewItem.CustomerID);
    })
    $scope.$watch("NewItem.PartnerID", function () {
        $scope.NewItem.LocationDepotID = "";
        $scope.NewItem.LocationDepotReturnID = "";
        $scope.NewItem.LocationDepotCode = "";
        $scope.NewItem.LocationDepotReturnCode = "";
    })
    $scope.$watch("NewItem.ServiceOfOrderID", function () {
        $scope.NewItem.LocationFromID = "";
        $scope.NewItem.LocationFromCode = "";
        $scope.NewItem.LocationToID = "";
        $scope.NewItem.LocationToCode = "";
        $scope.NewItem.DateGetEmpty = null;
        $scope.NewItem.DateReturnEmpty = null;
    })

    $scope.Location_Select = function ($event, item, type, win) {
        $event.preventDefault();

        $scope.CustomerID = item.CustomerID;
        $scope.TransportModeID = item.TransportModeID;
        $scope.CarrierID = item.PartnerID;
        $scope.ServiceOfOrderID = item.ServiceOfOrderID;
        $scope.LocationType = type; $scope.IsNewContainer = item.ID < 1;

        if (!$scope.IsNewContainer) {
            $scope.EditItem = $.extend(true, {}, item);
        }

        $scope.location_Grid.dataSource.read();
        win.center().open();
    }

    $scope.Location_Select_OK = function ($event, item, win) {
        $event.preventDefault();

        if ($scope.IsNewContainer) {
            switch ($scope.LocationType) {
                case 1: //From
                    $scope.NewItem.LocationFromID = item.CUSLocationID;
                    $scope.NewItem.LocationFromCode = item.LocationCode;
                    break;
                case 2: //To
                    $scope.NewItem.LocationToID = item.CUSLocationID;
                    $scope.NewItem.LocationToCode = item.LocationCode;
                    break;
                case 3: //GetEmpty
                    $scope.NewItem.LocationDepotID = item.CUSLocationID;
                    $scope.NewItem.LocationDepotCode = item.LocationCode;
                    break;
                case 4://ReturnEmpty
                    $scope.NewItem.LocationDepotReturnID = item.CUSLocationID;
                    $scope.NewItem.LocationDepotReturnCode = item.LocationCode;
                    break;
            }
        } else {
            switch ($scope.LocationType) {
                case 1: //From
                    $scope.EditItem.LocationFromID = item.CUSLocationID;
                    $scope.EditItem.LocationFromCode = item.LocationCode;
                    break;
                case 2: //To
                    $scope.EditItem.LocationToID = item.CUSLocationID;
                    $scope.EditItem.LocationToCode = item.LocationCode;
                    break;
                case 3: //GetEmpty
                    $scope.EditItem.LocationDepotID = item.CUSLocationID;
                    $scope.EditItem.LocationDepotCode = item.LocationCode;
                    break;
                case 4://ReturnEmpty
                    $scope.EditItem.LocationDepotReturnID = item.CUSLocationID;
                    $scope.EditItem.LocationDepotReturnCode = item.LocationCode;
                    break;
            }

            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_COTemp.URL.Update,
                data: { item: $scope.EditItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.IsLoading = false;
                        $scope.con_Grid.dataSource.read();
                    })
                }
            })
        }
        win.close();
    }

    $scope.Carrier_Select = function ($event, item, win) {
        $event.preventDefault();

        win.center().open();
        $scope.IsNewContainer = false;
        $scope.EditItem = $.extend(true, {}, item);
        $timeout(function () { $scope.LoadDataCarrier(item.CustomerID); }, 100)
    }

    $scope.Carrier_Select_OK = function ($event, item, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;

        $scope.EditItem.PartnerID = item.CUSPartnerID;
        $scope.EditItem.LocationDepotID = null;
        $scope.EditItem.LocationDepotReturnID = null;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_COTemp.URL.Update,
            data: { item: $scope.EditItem },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.IsLoading = false;
                    $scope.con_Grid.dataSource.read();
                    win.close();
                })
            }
        })
    }

    $scope.New_OK_Click = function ($event, win, grid) {
        $event.preventDefault();

        if (!$scope.IsGTZero($scope.NewItem.CustomerID) || !$scope.IsGTZero($scope.NewItem.ServiceOfOrderID)
            || !$scope.IsGTZero($scope.NewItem.TransportModeID) || !$scope.IsGTZero($scope.NewItem.PackingID)
            || !$scope.IsGTZero($scope.NewItem.LocationFromID) || !$scope.IsGTZero($scope.NewItem.LocationToID)) {
            $rootScope.Message({ Msg: "Vui lòng nhập đầy đủ thông tin!", Type: Common.Message.Type.Alert });
            return;
        }

        $rootScope.Message({
            Msg: "Xác nhận tạo mới?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_COTemp.URL.Save_List,
                    data: { total: $scope.NewItem.Quantity, item: $scope.NewItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            });
                            win.close();
                            grid.dataSource.read();
                        })
                    }
                })
            }
        })
    }

    $scope.IsGTZero = function (val) {
        return parseInt(val) > 0;
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
            event: $event, grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }

    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.con_Grid);
    }, 500);

    //#endregion
}]);