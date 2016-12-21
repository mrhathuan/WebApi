/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _MONMonitor_ScheduleCO = {
    URL: {
        List: "DTOMONMonitorScheduleDI_List",
        SRVehicleList: "DIMonitor_VehicleList",
        SRVehicleTimeList: "DIMonitor_VehicleTimeList",
        SRVehicleTimeGet: "DIMonitor_VehicleTimeGet",
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
        MS_Create: "DIMonitor_MsCreate",
        GroupPOD_Change: "DIMonitor_GroupPODChange",
        Vehicle_GetOPS: "DIMonitorMaster_GetByVehicle",
        SRVehicleNoDNList: "DIMonitor_NoDNList",
        SRVehicleNoDNOrderList: "DIMonitor_NoDNOrderList",
        TroubleNotin_List: "DIMonitor_TroubleNotinList",
        GOPReturn_List: "DIMonitorMaster_GOPReturnList",
        GOPReturn_Save: "DIMonitorMaster_GOPReturnSave",
        CUSGOP_List: "DIMonitorMaster_CUSGOPList"
    },
    Data: {
        _dataVehicle: null,
        _currentTimeline: null,
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

    },
}

angular.module('myapp').controller('MONMonitor_ScheduleCOCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    Common.Log('MONMonitor_ScheduleCOCtrl');
    $rootScope.IsLoading = false;

    $scope.gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRMasterDNList,
            readparam: function () {
                return {
                    DateFrom: Common.Date.Date($scope.Search.DateFrom),
                    DateTo: Common.Date.Date($scope.Search.DateTo),
                    IsRecieved: $scope.DataRequest.IsRecieved
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
                    ColorClass: { type: 'string' }
                }
            },
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: _MONMonitor_Index.Columns.gridSRMasterDNColumns,
        dataBound: function () {
            var grid = this;
            var rows = grid.tbody.find('tr');
            Common.Data.Each(rows, function (tr) {
                var obj = grid.dataItem(tr);
                if (Common.HasValue(obj)) {
                    if ($(tr).is('.k-alt'))
                        $(tr).removeClass('k-alt');
                    $(tr).addClass(obj.ColorClass);
                }
            })

        }
    };

    //#region Model
    $scope.Map_OnRight = true;
    $scope.View_Vehicle = true;
    $scope.Show_SearchTools = false;
    $scope.SRMasterDNSearchStatusRecieved = true;
    $scope.RouteIn = 3;
    $scope._id = 0;
    $scope.Show_SRVehicleMasterVehicleLocation = false;
    $scope.Show_SRVehicleMasterRevertDN = false;
    $scope.Show_SRVehicleMasterInfo = false;
    $scope.Show_SRVehicleMasterComplete = false;
    $scope.Show_SRVehicleCompleteDN = false;
    $scope.Display_Tab = {
        Trans: true,
        Group: false,
        Route: false,
        Return: true,
        Location: true,
        Trouble: true,
    }
    $scope._dataMsRouting = null;
    $scope.TO_Win_Title = "";
    $scope._dataDriver = null;
    $scope.CurrentMaster = null;
    $scope.DataRequest = {
        DateFrom: Common.Date.Date(Common.Date.AddDay(new Date(new Date()), -3)),
        DateTo: Common.Date.Date(Common.Date.AddDay(new Date(new Date()), 3)),
        IsRecieved: false
    };
    $scope.DataRequestParam = {
        DateFrom: Common.Date.ToString($scope.DataRequest.DateFrom),
        DateTo: Common.Date.ToString($scope.DataRequest.DateTo),
        IsRecieved: $scope.DataRequest.IsRecieved
    };
    $scope._dataDistrict = [];
    $scope._dataDistrict = [];
    $scope._typeID = 0;
    $scope.IsShowVehicleVendor = false;
    $scope.Search = {
        DateFrom: Common.Date.AddDay(new Date(), -2),
        DateTo: Common.Date.AddDay(new Date(), 2),
        HourFrom: Common.Date.AddDay(new Date(), -2),
        HourTo: Common.Date.AddHour(new Date(), 8),
        RouteInDay: 3,
    };
    $scope.LocationData = [];
    $scope._locationID = 0;
    $scope._locationStatusID = -1;
    $scope._dataGroupMaster = [];
    $scope._dataOPSNo = [];
    $scope.isEditting = false;
    //#endregion

    //#region Options

    $scope.MONDI_SpitterOptions = {
        panes: [
                { collapsible: true, resizable: true, size: '60%' },
                { collapsible: true, resizable: true, size: '40%', collapsed: false }
        ],
    };

    $scope.MONDI_SRVehicle_SpitterOptions = {
        orientation: 'vertical',
        panes: [
            { collapsible: false, resizable: false, size: '25px' },
            { collapsible: false, resizable: false, size: '28px' },
            { collapsible: false, resizable: false },
        ],
        resize: function (e) {
            $timeout(function () {
                openMap.Resize();
            },1)
            
        }
    };

    $scope.SRVehicleSearch_DateFrom_Options = {
        format: Common.Date.Format.DDMMYY,
        value: $scope.DataRequest.DateFrom
    };

    $scope.SRVehicleSearch_DateTo_Options = {
        format: Common.Date.Format.DDMMYY,
        value: $scope.DataRequest.DateTo
    }

    $scope.SRVehicleSearch_HourFrom_Options = {
        format: Common.Date.Format.HM,
        value: new Date(2011, 0, 1, 6, 00)
    }

    $scope.SRVehicleSearch_HourTo_Options = {
        format: Common.Date.Format.HM,
        value: new Date(2011, 0, 1, 18, 00)
    }

    $scope.SRVehicleDNSearch_DateFrom_Options = {
        format: Common.Date.Format.DDMMYY,
        value: $scope.DataRequest.DateFrom
    }

    $scope.SRVehicleDNSearch_DateTo_Options = {
        format: Common.Date.Format.DDMMYY,
        value: $scope.DataRequest.DateTo
    }

    $scope.SRMasterDN_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRMasterDNList,
            readparam: function () {
                return {
                    DateFrom: Common.Date.Date($scope.Search.DateFrom),
                    DateTo: Common.Date.Date($scope.Search.DateTo),
                    IsRecieved: $scope.DataRequest.IsRecieved
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
                    ColorClass: { type: 'string' }
                }
            },
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: _MONMonitor_Index.Columns.gridSRMasterDNColumns,
        dataBound: function () {
            var grid = this;
            var rows = grid.tbody.find('tr');
            Common.Data.Each(rows, function (tr) {
                var obj = grid.dataItem(tr);
                if (Common.HasValue(obj)) {
                    if ($(tr).is('.k-alt'))
                        $(tr).removeClass('k-alt');
                    $(tr).addClass(obj.ColorClass);
                }
            })

        }
    };

    if (!$scope.DataRequest.IsRecieved) {
        var f = { 'field': 'ColorClass', 'operator': 'neq', 'value': "green" };
        $scope.SRMasterDN_GridOptions.dataSource.filter(f);
    }

    $scope.TO_WinOptions = {
        width: '1025px', height: '550px',
        draggable: true, modal: true, resizable: false, title: false,
        open: function () {
            $timeout(function () {
                $scope.TO_Splitter.resize();
            }, 100)
        },
        close: function () {
            $scope.Display_Tab.Group = false;
            $scope.Display_Tab.Trans = true;
            $scope.TO_Tabstrip.select(0);
        }
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

    $scope.TO_SplitterOptions = {
        orientation: 'vertical',
        panes: [
            { collapsible: false, resizable: false, size: '90px' },
            { collapsible: false, resizable: false, size: '170px' },
            { collapsible: false, resizable: false },
            { collapsible: false, resizable: false, collapsed: false, size: '150px' }
        ]
    };

    $scope.TO_TabstripOptions = {
        height: "100%", animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            if (e.item.id == 'tab-trans' && $scope.CurrentMaster.IsEditable) {
                $scope.Show_BtnWinTO_Update = true;
            } else {
                $scope.Show_BtnWinTO_Update = false;
            }
        }
    };

    $scope.Driver_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { ID: { type: 'number' }, Name: { type: 'string' } } }
        }),
        height: '160px', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        columns: _MONMonitor_Index.Columns.winToGridVendorDriverColumns, editable: 'incell', dataBound: function (e) {
            Common.Log("Driver_GridOptions Bound missing");
        }
    }

    $scope.TODetail_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRWinToDetailList,
            model: { id: 'ID', fields: { ID: { type: 'number' } } },
            readparam: function () { return { masterID: $scope._id } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        columns: _MONMonitor_Index.Columns.winToGridDetailColumns, dataBound: function (e) {
            Common.Log("me.WinToGridDetailDataBound missing");
        },
        reorderable: true
    }

    $scope.Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRWinToDITOLocationList,
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
            readparam: function () { return { masterID: $scope._id } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        //toolbar: kendo.template($('#win-to-location-grid-toolbar').html()),
        dataBound: function (e) {
            $scope.LocationData = this.dataSource.data();
            Common.Log("me.WinToGridDITOLocationDataBound missing")
        },
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Location_Edit($event,Location_Grid)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
           { title: 'STT', field: 'SortOrder', width: "40px", },
           {
               field: 'SortOrderReal', width: 80, title: 'STT thực',
           },
           {
               field: 'LocationAddress', width: 200, title: 'Địa chỉ',
           },
           { title: 'Tỉnh thành', field: 'LocationProvince', width: "100px", },
           { title: 'Quận huyện', field: 'LocationDistrict', width: "100px", },
           { title: 'Trạng thái', field: 'DITOLocationStatusName', width: "100px", },
           {
               field: 'DateCome', width: 170, title: 'Tgian đến', template: '#=Common.Date.FromJsonDMYHM(DateCome)#'
           },
           {
               field: 'DateLeave', width: 170, title: 'Tgian đi', template: '#=Common.Date.FromJsonDMYHM(DateLeave)#'
           },
           {
               field: 'LoadingStart', width: 105, title: 'T.gian vào máng', template: '#=Common.Date.FromJsonHM(LoadingStart)#'
           },
           {
               field: 'LoadingEnd', width: 100, title: 'T.gian rời máng', template: '#=Common.Date.FromJsonHM(LoadingEnd)#'
           },
           {
               field: 'Comment', width: 300, title: 'Góp ý',
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    }

    $scope.Trouble_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.MSTroubleList,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    DITOID: { type: 'number', editable: true },
                    RoutingName: { type: 'string' },
                    Cost: { type: 'number', defaultValue: 0 },
                    CostOfCustomer: { type: 'number', defaultValue: 0 },
                    CostOfVendor: { type: 'number', defaultValue: 0 },
                    TroubleCostStatusName: { type: 'string' },

                }
            },
            readparam: function () { return { masterID: $scope._id } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        edit: function (e) {
            e.model.DITOMasterID = $scope._id;
        },
        toolbar: kendo.template($('#win-to-trouble-grid-toolbar').html()), editable: 'incell', columns: [
           {
               title: ' ', width: '155px',
               template: '<a href="/" ng-show="!true" ng-click="Trouble_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                   '<a href="/" ng-show="true" ng-click="Trouble_LineSave($event,Trouble_Grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                   '<a href="/" ng-show="true" ng-click="Trouble_Delete($event)" class="k-button"><i class="fa fa-trash"></i></a>' +
                   '<a href="/" ng-show="!true" ng-click="Grid_Cancel($event,Trouble_Grid)" class="k-button"><i class="fa fa-ban"></i></a>' +
                   '<a href="/" ng-click="OpenFile_Click($event,winfile,dataItem)" class="k-button"><i class="fa fa-paperclip"></i>Đính kèm</a>',
               filterable: false, sortable: false
           },
           {
               field: 'GroupOfTroubleName', width: 150, title: 'Nhóm',
           },
           { title: 'Mô tả', field: 'Description', width: "150px", },
           {
               field: 'Cost', width: 130, title: 'Chi phí', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'CostOfCustomer', width: 130, title: 'Chi cho CUS', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'CostOfVendor', width: 130, title: 'Chi cho VEN', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DITOID', width: 130, title: 'Cung đường', template: '#=RoutingName#', editor: function (container, options) {
                   var input = $("<input kendo-combobox k-options='Routing_CbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'TroubleCostStatusID', width: 130, title: 'Trạng thái', template: '#=TroubleCostStatusName#', editor: function (container, options) {
                   if (options.model.TroubleCostStatusID == 0)
                       options.model.TroubleCostStatusID = _MONMonitor_Index.Data._trouble1stStatus;
                   var input = $("<input kendo-combobox k-options='TroubleCostStatus_CbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            Common.Log("me.WinToGridTroubleDataBound missing")
        },

    }

    $scope.TroubleMap_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.MSTroubleList,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    DITOID: { type: 'number', editable: true },
                    RoutingName: { type: 'string' },
                    Cost: { type: 'number', defaultValue: 0 },
                    CostOfCustomer: { type: 'number', defaultValue: 0 },
                    CostOfVendor: { type: 'number', defaultValue: 0 },
                    TroubleCostStatusName: { type: 'string' },

                }
            },
            readparam: function () { return { masterID: $scope._id } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        edit: function (e) {
            e.model.DITOMasterID = $scope._id;
        },
        //toolbar: kendo.template($('#win-trouble-grid-toolbar').html()),
        editable: 'incell',
        columns: [
           {
               title: ' ', width: '155px',
               template: '<a href="/" ng-show="!true" ng-click="Trouble_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                   '<a href="/" ng-show="true" ng-click="Trouble_LineSave($event,Trouble_Grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                   '<a href="/" ng-show="true" ng-click="Trouble_Delete($event)" class="k-button"><i class="fa fa-trash"></i></a>' +
                   '<a href="/" ng-show="!true" ng-click="Grid_Cancel($event,Trouble_Grid)" class="k-button"><i class="fa fa-ban"></i></a>' +
                   '<a href="/" ng-click="OpenFile_Click($event,winfile,dataItem)" class="k-button"><i class="fa fa-paperclip"></i>Đính kèm</a>',
               filterable: false, sortable: false
           },
           {
               field: 'GroupOfTroubleName', width: 150, title: 'Nhóm',
           },
           { title: 'Mô tả', field: 'Description', width: "150px", },
           {
               field: 'Cost', width: 130, title: 'Chi phí', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'CostOfCustomer', width: 130, title: 'Chi cho CUS', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'CostOfVendor', width: 130, title: 'Chi cho VEN', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DITOID', width: 130, title: 'Cung đường', template: '#=RoutingName#', editor: function (container, options) {
                   var input = $("<input kendo-combobox k-options='Routing_CbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'TroubleCostStatusID', width: 130, title: 'Trạng thái', template: '#=TroubleCostStatusName#', editor: function (container, options) {
                   if (options.model.TroubleCostStatusID == 0)
                       options.model.TroubleCostStatusID = _MONMonitor_Index.Data._trouble1stStatus;
                   var input = $("<input kendo-combobox k-options='TroubleCostStatus_CbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            
        },

    }

    $scope.Trouble_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.TroubleNotin_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    DITOID: { type: 'string', editable: false },
                    Cost: { type: 'number', defaultValue: 0 },
                    IsChoose: { type: 'bool' },
                }
            },
            readparam: function () { return { masterID: $scope._id } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        edit: function (e) {
            e.model.DITOMasterID = $scope._id;
        },
        dataBound: function (e) {
            Common.Log("me.WinToGridTroubleDataBound missing")
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
           { field: 'CostValue', width: 130, title: 'Chi phí' },
           { title: ' ', filterable: false, sortable: false }
        ],

    }

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
                if ($scope.CurrentMaster.Driver2.ID != val && $scope.CurrentMaster.Assistant1.ID != val && $scope.CurrentMaster.Assistant2.ID != val) {
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
                if ($scope.CurrentMaster.Driver1.ID != val && $scope.CurrentMaster.Assistant1.ID != val && $scope.CurrentMaster.Assistant2.ID != val) {
                    res.push(elem);
                }
            })
            res = res.sort(function (a, b) { return a.ID - b.ID; });
            this.dataSource.data(res);
        }
    };

    $scope.Assistant1_CbbOptions = {
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
                $scope.CurrentMaster.Assistant1.ID = obj.ID;
                $scope.CurrentMaster.Assistant1.Name = obj.DriverName;
                $scope.CurrentMaster.Assistant1.Tel = obj.Cellphone;
                $scope.CurrentMaster.Assistant1.Card = obj.CardNo;
            }
        },
        open: function (e) {
            var res = [{ 'ID': null, 'DriverName': ' ', 'Cellphone': '', 'CardNo': '' }];
            $.each($scope._dataDriver, function (i, elem) {
                var val = parseInt(elem.ID);
                if ($scope.CurrentMaster.Driver1.ID != val && $scope.CurrentMaster.Driver2.ID != val && $scope.CurrentMaster.Assistant2.ID != val) {
                    res.push(elem);
                }
            })
            res = res.sort(function (a, b) { return a.ID - b.ID; });
            this.dataSource.data(res);
        }
    };

    $scope.Assistant2_CbbOptions = {
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
                $scope.CurrentMaster.Assistant2.ID = obj.ID;
                $scope.CurrentMaster.Assistant2.Name = obj.DriverName;
                $scope.CurrentMaster.Assistant2.Tel = obj.Cellphone;
                $scope.CurrentMaster.Assistant2.Card = obj.CardNo;
            }
        },
        open: function (e) {
            var res = [{ 'ID': null, 'DriverName': ' ', 'Cellphone': '', 'CardNo': '' }];
            $.each($scope._dataDriver, function (i, elem) {
                var val = parseInt(elem.ID);
                if ($scope.CurrentMaster.Driver1.ID != val && $scope.CurrentMaster.Driver2.ID != val && $scope.CurrentMaster.Assistant1.ID != val) {
                    res.push(elem);
                }
            })
            res = res.sort(function (a, b) { return a.ID - b.ID; });
            this.dataSource.data(res);
        }
    };

    $scope.NODN_WinOptions = {
        title: 'Lập lệnh',
        width: '900px', height: '500px',
        modal: true, draggable: true, pinned: false, resizable: false,
        open: function () {
            $timeout(function () {
                $scope.NODN_Grid.resize();
            }, 100)
        },
        actions: ['Maximize', 'Close']
    };

    $scope.NODN_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'GenID',
                fields:
                    {
                        GenID: { type: 'string', editable: false },
                        ID: { type: 'number' },
                        CreateSortOrder: { type: 'number' },
                        VehicleCode: { type: 'string' },
                        SOCode: { type: 'string' },
                        RequestDateEmpty: { type: 'date' },
                        RequestDate: { type: 'date' },
                        RouteCode: { type: 'string' },
                        Ton: { type: 'number' },
                        Kg: { type: 'number' },
                        Note1: { type: 'string' },
                        Note2: { type: 'string' },
                        DNCode: { type: 'string' }
                    }
            },
            sort: [{ field: 'CreateSortOrder', dir: 'desc' }]
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, resizable: true, reorderable: true,
        sortable: { mode: 'multiple' },
        columns: _MONMonitor_Index.Columns.winNoDNGridColumns,
        //toolbar: kendo.template($('#win-nodn-grid-toolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log('me.WinNoDNGridDataBound missing');
        }
    };

    $scope.NODN_Province_CbbOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "ProvinceName",
        dataValueField: "ID",
        placeholder: "Chọn tỉnh thành...",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        change: function (e) {
            e.sender.dataItems();
            var data = [];
            $.each(e.sender.dataItems(), function (i, v) {
                $.each($scope._dataDistrict[v.ID], function (id, vd) {
                    data.push(vd);
                });
            });
            $scope.NODN_District_CbbOptions.dataSource.data(data);
            //$scope.WinNoDNGridFilter();

        }
    };

    $scope.NODN_District_CbbOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' }
                }
            }
        }),
        dataTextField: 'DistrictName', dataValueField: 'ID', placeholder: 'Chọn quận huyện...',
        filter: 'contains', autoClose: false, ignoreCase: true,
        change: function (e) {
            //me.WinNoDNGridFilter();
        }
    };

    $scope.LocationTemplateOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Address', dataValueField: 'ID', placeholder: "Nhập địa chỉ",
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { Address: { type: 'string' }, ID: { type: 'string' } }
            }
        }),
        select: function (e) {
            var cbb = this;
            $timeout(function () {
                var tr = $(cbb.wrapper).closest('tr');
                var gridObj = $scope.Location_grid.dataItem(tr);
                var selItem = cbb.dataItem(cbb.select());
                if (Common.HasValue(gridObj)) {
                    if (!Common.HasValue(selItem)) {
                        selItem = _MONMonitor_Index.Data._dataLocation[0];
                    }
                    gridObj.LocationID = selItem.ID;
                    gridObj.LocationAddress = selItem.Address;
                    gridObj.LocationProvince = selItem.ProvinceName;
                    gridObj.LocationDistrict = selItem.DistrictName;
                    $scope.Location_grid.dataSource.sync();
                }
            }, 100)
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

    }

    $scope.TOGroup_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRWinToGroupList,
            readparam: function () { return { masterID: $scope._id } },
            model: { id: 'ID', fields: { ID: { type: 'number' } } },
            group: [{ field: 'OPSNo' }]
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: true, filterable: false, resizable: true, reorderable: true,
        toolbar: kendo.template($('#win-to-group-grid-toolbar').html()),
        columns: [
            {
                field: 'OPSNo', title: ' ', width: 65, filterable: false, sortable: false, menu: false,
                groupHeaderTemplate: "#= value != null ? 'STT '+ value : 'Chưa phân chuyến' #: Tấn: {{GetSumTon(#=value#)}} - Khối: {{GetSumCBM(#=value#)}} - SL: {{GetSumQuantity(#=value#)}} <input kendo-combobox='GroupVender_Cbb#=value#' k-options='GroupVender_CbbOptions' index='#=value#' class='cboGroup_Vendor'/> <input kendo-combobox k-options='GroupVehicle_CbbOptions' class='cboGroup_Vehicle'/> <input kendo-combobox k-options='GroupDriver_CbbOptions' class='cboGroup_DriverName'/> <input class='txtGroup_DriverTel' placeholder='SĐT*'/>",
                template: '<input kendo-numerictextbox k-options="opsNO_NtOptions"  class="cus-number opsNo"></input>'
            },
            {
                field: 'OrderCode', title: 'Mã đơn hàng', width: 125,
                template: function (o) {
                    var chk = '<input class="chkChoose" style="display:none" type="checkbox" />';
                    var style = 'style = "display: none"';
                    if (o.IsMergeable)
                        style = '';
                    return '<button ' + style + ' class="my-button groupMerge" title="Gộp">M</button>' + chk + '  ' + o.OrderCode;
                },
            },
            { title: 'Nhóm', field: 'GroupProductName', title: 'Hàng hóa', width: "150px", },
            {
                field: 'TonTranfer', width: 75,
                title: 'Tấn',
                //template: '#=WinToGroupTemplate(1, "TonTranfer", TonTranfer, UpdateTypeID) #'
            },
            {
                field: 'CBMTranfer', width: 75,
                title: 'Khối',
                //template: '#=WinToGroupTemplate(2, "CBMTranfer", CBMTranfer, UpdateTypeID) #'
            },
            {
                field: 'QuantityTranfer', width: 75,
                title: 'SL',
                //template: '#=WinToGroupTemplate(3, "QuantityTranfer", QuantityTranfer, UpdateTypeID) #'
            },
            { title: 'Số SO', field: 'SOCode', width: "80px", },
            { title: 'Số DN', field: 'DNCode', width: "80px", },
            {
                field: 'IsOrigin', width: 75,
                title: 'Gốc',
                template: '<input type="checkbox" ng-click="PODChange($event)" #=IsOrigin ? checked="checked" : ""# />'
            },
            { title: 'Điểm đến', field: 'LocationToName', width: "125px", },
            { title: 'Tỉnh thành', field: 'LocationToProvince', width: "125px", },
            { title: 'Quận huyện', field: 'LocationToDistrict', width: "125px", },
            { title: 'Địa chỉ', field: 'LocationToAddress', width: "200px", },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.GroupVender_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'VendorName', dataValueField: 'VendorID', enable: true,
        select: function () {
            var cbb = this;
            $timeout(function () {
                var obj = cbb.dataItem(cbb.select());
                if (Common.HasValue(obj)) {
                    var index = cbb.element.attr('index');
                    $scope._dataGroupMaster[index].VendorID = obj.VendorID;
                    $scope._dataGroupMaster[index].VendorName = obj.VendorName;
                }
            }, 100)
        },
        placeholder: "Nhà vận tải*",
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'VendorID', fields: { VendorName: { type: 'string' }, VendorID: { type: 'string' } }
            }
        }),
    }

    $scope.GroupVehicle_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'RegNo', dataValueField: 'ID', enable: true,
        placeholder: "Số xe*",
        change: function (e) {
            var cbb = this;
            var me = Common.View;
            var index = cbb.element.attr('index');

            var obj = cbb.dataItem(cbb.select());
            if (Common.HasValue(obj)) {
                me._dataGroupMaster[index].VehicleID = obj.ID;
                me._dataGroupMaster[index].VehicleNo = obj.RegNo;
            } else {
                me._dataGroupMaster[index].VehicleID = -1;
                me._dataGroupMaster[index].VehicleNo = cbb.text();
            }
        },
        open: function () {
            var tr = this.wrapper.closest('tr');
            var idx = $(tr).find('.cboGroup_Vendor [data-role="combobox"]').attr('index');
            var vendorID = $scope["GroupVender_CbbOptions" + idx].value();
            if (vendorID == "")
                vendorID = -2;
            if (vendorID == -1 || vendorID == 0) {
                this.dataSource.data($scope._dataTruck);
            } else {
                var data = $.grep($scope._dataTruck, function (e) {
                    return e.VendorID == vendorID || e.VedorID == vendorID.toString();
                })
                this.dataSource.data(data);
            }
        },
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { RegNo: { type: 'string' }, ID: { type: 'string' } }
            }
        })
    }

    $scope.GroupDriver_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'DriverName', dataValueField: 'ID', enable: true,
        placeholder: "Tài xế*",
        change: function (e) {
            var cbb = this;
            var index = cbb.element.attr('index');

            var obj = cbb.dataItem(cbb.select());
            if (Common.HasValue(obj)) {
                $scope._dataGroupMaster[index].DriverID = obj.ID;
                $scope._dataGroupMaster[index].DriverName = obj.DriverName;
                txtDriverTel.data('kendoMaskedTextBox').value(obj.Cellphone);
            } else {
                $scope._dataGroupMaster[index].DriverID = -1;
                $scope._dataGroupMaster[index].DriverName = cbb.text();
                txtDriverTel.data('kendoMaskedTextBox').value("");
            }
        },
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { DriverName: { type: 'string' }, ID: { type: 'string' } }
            }
        })
    }

    $scope.opsNO_NtOptions = {
        format: '0', spinners: false, culture: 'en-US', min: 1, max: 100, step: 1, decimals: 0,
        change: function () {

            var value = this.value();
            if (!(value > 0))
                value = null;

            var obj = $scope.TOGroup_Grid.dataItem($(this.wrapper).closest('tr'));
            if (Common.HasValue(obj)) {
                var tmp = obj.OPSNo;
                obj.OPSNo = value;

                var isAdd = false;
                var data = $scope.TOGroup_Grid.dataSource.data();
                for (var i = 0; i < data.length; i++) {
                    if (data[i].value == value) {
                        data[i].items.push(obj);
                        isAdd = true;
                    }
                }
                if (!isAdd) {
                    $scope.TOGroup_Grid.dataSource.insert(obj);
                }
                for (var i = 0; i < data.length; i++) {
                    if (data[i].value == tmp) {
                        if (data[i].items.length > 1)
                            data[i].items.remove(obj);
                        else
                            $scope.TOGroup_Grid.dataSource.data().splice(i, 1);
                    }
                }
                $scope.TOGroup_Grid.dataSource.sync();

                $scope._dataOPSNo = [];
                $.each(data, function (idx, items) {
                    Common.Data.Each(items.items, function (o) {
                        if (o.OPSNo > 0) {
                            if ($scope._dataOPSNo[o.OPSNo])
                                $scope._dataOPSNo[o.OPSNo].push(o.ID);
                            else {
                                $scope._dataOPSNo[o.OPSNo] = [o.ID];
                            }
                        }
                    })
                })
                for (var i = 0; i < $scope._dataOPSNo.length; i++) {
                    if (Common.HasValue($scope._dataGroupMaster[i]) && !Common.HasValue($scope._dataOPSNo[i]))
                        $scope._dataGroupMaster[i] = undefined;
                }
            }
        }
    }

    $scope.TroubleCostStatus_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'Code',
        dataValueField: 'ID',
        change: function (e) {
            var grid = $scope.Trouble_Grid;
            grid.dataItem($(e.sender.wrapper).closest('tr')).TroubleCostStatusName = this.text();
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

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

    $scope.Return_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.GOPReturn_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    STT: { type: 'string' },
                    OrderCode: { type: 'string', editable: false },
                    SOCode: { type: 'string', editable: false },
                    DNCode: { type: 'string', editable: false },
                    GroupProductName: { type: 'string', editable: false },
                    LocationToName: { type: 'string', editable: false },
                    LocationToProvince: { type: 'string', editable: false },
                    LocationToDistrict: { type: 'string', editable: false },
                    LocationToAddress: { type: 'string', editable: false },
                    OrderCode: { type: 'string', editable: false },
                }
            },
            readparam: function () { return { masterID: $scope._id } }
        }),
        toolbar: kendo.template($('#return-grid-toolbar').html()),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: true,
        columns: [
            {
                title: 'STT',
                field: 'STT', width: "50px",
            },
            {
                title: 'Mã ĐH',
                field: 'OrderCode', width: "100px",
            },
            {
                title: 'Số SO',
                field: 'SOCode', width: "80px",
            },
            {
                title: 'Số DN',
                field: 'DNCode', width: "80px",
            },
            {
                title: 'Nhóm SP', field: 'GroupProductID', template: '#=GroupProductName#', width: "150px", editor: function (container, options) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.CUSGOP_List,
                        data: { customerid: options.model.CustomerID },
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
                field: 'TonTranfer', width: 75, title: 'Tân',
                //template: '#=WinToDetailTemplate(1, "TonTranfer", TonTranfer, UpdateTypeID) #'
            },
            {
                field: 'CBMTranfer', width: 75, title: 'Khối',
                //template: '#=WinToDetailTemplate(2, "CBMTranfer", CBMTranfer, UpdateTypeID) #'
            },
            {
                field: 'QuantityTranfer', width: 75, title: 'SL',
                //: '#=WinToDetailTemplate(3, "QuantityTranfer", QuantityTranfer, UpdateTypeID) #'
            },
            {
                title: 'Điểm Đến',
                field: 'LocationToName', width: "125px",
            },
            {
                title: 'Tỉnh thành',
                field: 'LocationToProvince', width: "125px",
            },
            {
                title: 'Quận huyện',
                field: 'LocationToDistrict', width: "125px",
            },
            {
                title: 'Địa chỉ',
                field: 'LocationToAddress', width: "200px",
            },
            { title: ' ', filterable: false, sortable: false }
        ],
        reorderable: true
    }

    $scope.numPrice_Options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    //#endregion

    //#region Event

    $scope.SRVehicleDNSET = function ($event) {
        $event.preventDefault();
        $scope.Show_SearchTools = !$scope.Show_SearchTools;
        if (!$scope.Show_SearchTools) {
            $scope.DataRequestParam.DateFrom = Common.Date.Date($scope.SRVehicleDNSearch_DateFrom.value());
            $scope.DataRequestParam.DateTo = Common.Date.Date($scope.SRVehicleDNSearch_DateTo.value());
            if (!$scope.DataRequest.IsRecieved) {
                var f = { 'field': 'ColorClass', 'operator': 'neq', 'value': "green" };
                $scope.SRMasterDN_GridOptions.dataSource.filter(f);
            }
            else {
                $scope.SRMasterDN_GridOptions.dataSource.filter({});
            }
            $scope.MONDI_SRVehicle_Spitter.size(".k-pane:first", "25px");
        } else {
            $scope.MONDI_SRVehicle_Spitter.size(".k-pane:first", "35px");
        }


    }

    $scope.SRVehicleSET = function ($event) {
        $event.preventDefault();
        $scope.Show_SearchTools = !$scope.Show_SearchTools;
        if (!$scope.Show_SearchTools) {
            $scope.MONDI_SRVehicle_Spitter.size(".k-pane:first", "25px");
            _MONMonitor_Index.Timeline.ChangeTime({
                search: {
                    DateFrom: $scope.Search.DateFrom,
                    DateTo: $scope.Search.DateTo,
                    HourFrom: $scope.Search.HourFrom,
                    HourTo: $scope.Search.HourTo
                }
            });
        } else {
            $scope.MONDI_SRVehicle_Spitter.size(".k-pane:first", "65px");
        }

    }

    $scope.DN_Click = function ($event) {
        $event.preventDefault();
        var grid = $scope.SRMasterDN_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));

        $scope._id = item.DITOMasterID;
        $scope._typeID = item.TypeID;
        if ($scope._typeID == 1)
            $scope.Show_SRVehicleMasterVehicleLocation = true;
        else
            $scope.Show_SRVehicleMasterVehicleLocation = false;

        $scope._ditoGroupID = item.ID;
        if (item.IsDNComplete) {
            $scope.Show_SRVehicleMasterRevertDN = true;
        }
        else {
            $scope.Show_SRVehicleMasterRevertDN = false;
        }
        if (item.IsComplete) {
            $scope.Show_SRVehicleMasterRevert = true;
            $scope.Show_SRVehicleMasterRevertDN = false;
        }
        else {
            $scope.Show_SRVehicleMasterRevert = false;
        }

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRMasterDNGet,
            data: { id: item.DITOMasterID },
            success: function (res) {
                $scope.CurrentMaster = res;
                $scope._id = res.ID;

                $scope.Show_SRVehicleMasterInfo = true;

                if (res.IsComplete) {
                    $scope.Show_SRVehicleMasterComplete = false;
                    $scope.Show_SRVehicleCompleteDN = false;
                }
                else {
                    $scope.Show_SRVehicleMasterComplete = true;
                    $scope.Show_SRVehicleCompleteDN = true;
                }
                $scope.ReloadMap();
                //$scope.MapRefresh();
                $scope.MapVehicleLocation();
            }
        })
    }

    $scope.SRVehicleMasterInfo = function ($event) {
        $event.preventDefault();
        if ($scope._id > 0 && Common.HasValue($scope.CurrentMaster)) {
            $scope.WinToOpen();
        }
    }

    $scope.WinToOpen = function () {
        Common.Log("WinToOpen");
        if ($scope.CurrentMaster.IsEditable) {
            $scope.Show_BtnWinToGroup_View = true;
            $scope.Show_BtnWinTO_Update = true;
        }
        else {
            $scope.Show_BtnWinToGroup_View = false;
            $scope.Show_BtnWinTO_Update = false;
        }

        $scope.Vehicle_Cbb.enable($scope.CurrentMaster.IsEditable);

        var vendorID = $scope.CurrentMaster.VendorID > 0 ? $scope.CurrentMaster.VendorID : -1;
        if (vendorID > 0) {
            $scope.Vehicle_CbbOptions.dataSource.data(_MONMonitor_Index.Data._dataVehicleByVEN[vendorID]);
        } else {
            $scope.Vehicle_CbbOptions.dataSource.data(_MONMonitor_Index.Data._dataVehicleHome);
        }
        $timeout(function () {
            $scope.Vehicle_Cbb.value($scope.CurrentMaster.VehicleID);
        }, 200)

        $scope.TO_Win_Title = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentMaster.Code + ' - ETD: ' + $scope.CurrentMaster.ETD + ' ETA: ' + $scope.CurrentMaster.ETA;
        $scope.TO_Win.center().open();
        $scope.TODetail_Grid.resize();

        $('.cus-div-header').css('padding', '0px 0px 0px 0px');

        var data = [
            $scope.CurrentMaster.Driver1,
            $scope.CurrentMaster.Driver2,
            $scope.CurrentMaster.Assistant1,
            $scope.CurrentMaster.Assistant2
        ]
        $scope.Driver_GridOptions.dataSource.data(data);

        if ($scope.CurrentMaster.IsVehicleVendor) {
            $scope.Show_DriverGrid = true;

            //$scope.TO_Splitter.size(".k-pane:eq(1)", "200px");
        } else {
            $scope.Show_DriverGrid = false;
            //$scope.TO_Splitter.size(".k-pane:eq(1)", "125px");
        }

        $scope.TODetail_Grid.dataSource.read();
        $scope.Location_Grid.dataSource.read();
        if (!$scope.CurrentMaster.IsEditable) {
            $scope.Show_BtnTrouble_New = true;
        } else {
            $scope.Show_BtnTrouble_New = true;
        }
        $timeout(function () {
            $scope.Return_GridOptions.dataSource.read();
            $scope.Trouble_GridOptions.dataSource.read();
            $('.cus-div-header').css('padding', '5px 5px 5px 5px');
        }, 1000)

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.MSRoutingList,
            data: { masterID: $scope._id, locationID: null },
            success: function (res) {
                $scope._dataMsRouting = res.Data;
                $scope.Routing_CbbOptions.dataSource.data(res.Data);
            }
        })

    }

    $scope.TO_Win_Close = function ($event) {
        $event.preventDefault();
        $scope.TO_Win.close();
    }

    $scope.To_WinUpdate = function ($event) {
        Common.Log("WinToUpdate");
        var isSuccess = [];
        $scope.CurrentMaster.VehicleNo = $scope.Vehicle_Cbb.text();

        if ($scope.CurrentMaster.IsVehicleVendor) {
            var dataDriver = $scope.Driver_GridOptions.dataSource.data();
            $scope.CurrentMaster.Driver1 = dataDriver[0];
            $scope.CurrentMaster.Driver2 = dataDriver[1];
            $scope.CurrentMaster.Assistant1 = dataDriver[2];
            $scope.CurrentMaster.Assistant2 = dataDriver[3];
        } else {
        }

        if (!Common.HasValue($scope.CurrentMaster.Driver1.Tel) || $scope.CurrentMaster.Driver1.Tel == "") {
            //isSuccess = false;
        }
        if ($scope.CurrentMaster.KMStart > $scope.CurrentMaster.KMEnd) {
            isSuccess.push("Số KM kết thúc phải lớn hơn KM bắt đầu");
        }
        if (isSuccess.length == 0) {
            var dataDetail = $scope.TODetail_Grid.dataSource.data();
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Index.URL.SRWinToUpdate,
                data: { item: $scope.CurrentMaster, lstDetail: dataDetail },
                success: function (res) {
                    $scope.TO_Win.close();
                    if ($scope.View_Vehicle) {
                        _MONMonitor_Index.Timeline.RefreshDetail();
                    }
                    else {
                        $scope.SRMasterDN_Grid.dataSource.read();
                    }
                    $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
        else {
            $rootScope.Message({ Msg: isSuccess.join("; "), NotifyType: Common.Message.NotifyType.ERROR });
        }

    }

    $scope.Complete_Master = function ($event) {
        Common.Log("Complete_Master");
        $event.preventDefault();
        if (Common.HasValue($scope.CurrentMaster) && $scope._id > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh chuyến?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.SRVehicleComplete,
                        data: { masterID: $scope._id },
                        success: function (res) {
                            $scope.Show_SRVehicleMasterComplete = false;
                            $scope.Show_SRVehicleCompleteDN = false;
                            $scope.SRMasterDN_Grid.dataSource.read();
                            _MONMonitor_Index.Timeline.RefreshDetail();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });

        }
    }

    $scope.Complete_DN = function ($event) {
        Common.Log("Complete_DN");
        if (Common.HasValue($scope.CurrentMaster) && $scope._id > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh DN?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.SRVehicleCompleteDN,
                        data: { ditogropID: $scope._ditoGroupID },
                        success: function (res) {
                            $scope.Show_SRVehicleCompleteDN = false;
                            $scope.SRMasterDN_Grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }
    }

    $scope.Revert_Master = function ($event) {
        Common.Log("Revert_Master");
        if (Common.HasValue($scope.CurrentMaster) && $scope._id > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận trả về trạng thái kế hoạch?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.SRVehicleRevert,
                        data: { masterID: $scope._id },
                        success: function (res) {
                            $scope.Show_SRVehicleMasterRevert = false;
                            $scope.Show_SRVehicleMasterRevertDN = false;
                            $scope.SRMasterDN_Grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        };
    }

    $scope.Revert_MasterDN = function ($event) {
        Common.Log("Revert_MasterDN");

        if (Common.HasValue($scope.CurrentMaster) && $scope._id > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận trả về trạng thái kế hoạch?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.SRVehicleRevertDN,
                        data: { ditoGroupID: $scope._ditoGroupID },
                        success: function (res) {
                            $scope.Show_SRVehicleMasterRevert = false;
                            $scope.Show_SRVehicleMasterRevertDN = false;
                            $scope.SRMasterDN_Grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            });
        }

    }

    $scope.MasterNoDN = function ($event) {
        Common.Log("MasterNoDN");

        $scope.WinNoDNGridFilter();

        $scope._dataHasDNSort = [];

        var param = Common.Request.Create({
            Sorts: [], Filters: [
                Common.Request.FilterParamWithAnd('CreateDateTime', Common.Request.FilterType.GreaterThanOrEqual, $scope.Search.DateFrom),
                Common.Request.FilterParamWithAnd('CreateDateTime', Common.Request.FilterType.LessThanOrEqual, $scope.Search.DateTo)
            ]
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRVehicleNoDNList,
            data: { request: param },
            success: function (res) {
                $scope._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0, 'CBM': 0, 'Quantity': 0 };
                var datafix = [];
                var param1 = Common.Request.Create({
                    Sorts: [], Filters: [
                        Common.Request.FilterParamWithAnd('ETD', Common.Request.FilterType.GreaterThanOrEqual, $scope.DataRequest.DateFrom),
                        Common.Request.FilterParamWithAnd('ETD', Common.Request.FilterType.LessThanOrEqual, $scope.DataRequest.DateTo)
                    ]
                });

                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_Index.URL.SRVehicleNoDNOrderList,
                    data: param1,
                    success: function (res) {

                        _MONMonitor_Index.Data._dataHasDN = [];
                        $.each(res, function (i, v) {
                            if (v.TypeID < 2) {
                                if (Common.HasValue(v.RequestDate)) {
                                    v.RequestDate = Common.Date.FromJson(v.RequestDate);
                                    v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                                }
                                v.Kg = v.Ton * 1000;

                                if (v.CreateSortOrder > 0)
                                    v.CreateSortOrder = datafix[v.CreateSortOrder];

                                _MONMonitor_Index.Data._dataHasDN.push(v);
                            }
                        });
                        $scope.NODN_Win.center().open();
                        $scope.NODN_Grid.dataSource.data(_MONMonitor_Index.Data._dataHasDN);
                    }
                })

                var index = 1;
                $.each(res, function (i, v) {
                    if (v.TypeID < 2) {
                        v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                        v.Kg = 0;
                        _MONMonitor_Index.Data._dataHasDNSort[index] = v;
                        datafix[v.CreateSortOrder] = index;
                        v.CreateSortOrder = index;
                        v.IsChange = false;

                        index++;
                    }
                });
            }
        })


        $scope.NODN_Win.center().open();
    }

    $scope.WinNoDNGridFilter = function () {
        Common.Log('WinNoDNGridFilter');

        var dataProvince = $scope.NODN_Province_Cbb.dataItems();
        var dataDistrict = $scope.NODN_District_Cbb.dataItems();

        var fProvince = { 'filters': [], 'logic': 'or' };
        var fDistrict = { 'filters': [], 'logic': 'or' };
        var fCustomer = { 'filters': [], 'logic': 'or' };
        if (dataProvince.length > 0) {
            angular.forEach(dataProvince, function (v, i) {
                fProvince.filters.push({ 'field': 'ProvinceName', 'operator': 'contains', 'value': v.ProvinceName });
            });
        }
        if (dataDistrict.length > 0) {
            angular.forEach(dataDistrict, function (v, i) {
                fDistrict.filters.push({ 'field': 'DistrictName', 'operator': 'contains', 'value': v.DistrictName });
            });
        }

        var f = { 'filters': [], 'logic': 'and' };
        if (fDistrict.filters.length > 0)
            f.filters.push(fDistrict);
        else if (fProvince.filters.length > 0)
            f.filters.push(fProvince);

        if (f.filters.length > 1)
            $scope.NODN_Grid.dataSource.filter(f);
        else if (f.filters.length > 0)
            $scope.NODN_Grid.dataSource.filter(f.filters[0]);
        else
            $scope.NODN_Grid.dataSource.filter(null);
    }

    $scope.OnVehicleClick = function (e) {
        $scope.Vehicle_Win.center().open();
    }

    $scope.Location_Update = function ($event) {
        $event.preventDefault();
        var data = $scope.Location_Grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.SRWinToDITOLocationUpdate,
            data: { lstDITOLocation: data },
            success: function (res) {
                $rootScope.Message({ Msg: 'Đã cập nhật!', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.Location_Grid.dataSource.read();
            }
        })
    }

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Map_Save = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.MapLocationChangeStatus,
            data: {
                masterID: $scope._id,
                locationID: $scope._locationID,
                statusID: $scope._locationStatusID,
                signDate: $scope.dtpSignature,
                comment: $scope.txtComment
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                google.maps.event.trigger(_MONMonitor_Index.Data._mapMarkers[$scope._locationID], 'click');
                _MONMonitor_Index.Timeline.RefreshDetail();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.Map_Trouble = function ($event) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.MSRoutingList,
            data: { masterID: $scope._id, locationID: $scope._locationID },
            success: function (res) {
                _MONMonitor_Index.Data._dataMsRouting = res.Data;
                $scope.Routing_CbbOptions.dataSource.data(res.Data);
                $scope.TroubleMap_Grid.dataSource.read();
                $scope.Trouble_Win.center().open();
            }
        })
    }

    $scope.Map_Close = function ($event) {
        $event.preventDefault();
        openMap.Close();
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
                method: _MONMonitor_Index.URL.MSTroubleUpdateAll,
                data: { data: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.Trouble_Edit = function ($event) {
        $event.preventDefault();
        var grid = $scope.Trouble_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        grid.editRow($($event.currentTarget).closest("tr"));
        $scope.isEditting = true;
        //Common.Services.Call($http, {
        //    url: Common.Services.url.MON,
        //    method: _MONMonitor_Index.URL.MSTroubleGet,
        //    data: { troubleID: item.ID },
        //    success: function (res) {
        //        $scope.ItemDetail = res;
        //        $scope.Trouble_EditWin.center().open();
        //    }
        //})
    }

    $scope.Trouble_Save = function ($event, vform, win) {
        $event.preventDefault();
        $scope.ItemDetail.DITOMasterID = $scope._id;
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Index.URL.MSTroubleUpdate,
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
            method: _MONMonitor_Index.URL.MSTroubleUpdate,
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
                method: _MONMonitor_Index.URL.TroubleSaveList,
                data: { lst: data, masterID: $scope._id },
                success: function (res) {
                    grid1.dataSource.read();
                    grid2.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
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
                    method: _MONMonitor_Index.URL.MSTroubleDelete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Trouble_Grid.dataSource.read();
                    }
                })
            }
        });
    }

    $scope.GroupView_Click = function ($event) {
        $scope.Display_Tab.Group = true;
        $scope.Display_Tab.Trans = false;
        $scope.TOGroup_Grid.dataSource.read();
        $scope._dataGroupMaster = [];
        $scope.TO_Tabstrip.select(1);
        $scope._dataOPSNo = [];
    }

    $scope.PODChange = function ($event) {
        var obj = $scope.TOGroup_Grid.dataItem($($event.currentTarget).closest('tr'));
        if (Common.HasValue(obj)) {
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Index.URL.GroupPOD_Change,
                data: { groupID: obj.ID },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Đã cập nhật.', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.TOGroup_Cancel = function ($event) {
        $scope.Display_Tab.Trans = true;
        $scope.Display_Tab.Group = false;
        $scope.TO_Tabstrip.select(0);
    }

    $scope.Location_Edit = function ($event, grid) {
        $event.preventDefault();
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $scope.DetailItem = item;
        $timeout(function () {
            $scope.Location_DateCome.value(item.DateCome);
            $scope.Location_DateLeave.value(item.DateLeave);
            $scope.Location_LoadingStart.value(item.LoadingStart);
            $scope.Location_LoadingEnd.value(item.LoadingEnd);
        }, 100)
        $scope.Location_Win.center().open();
    }

    $scope.Location_Save = function ($event, vform, win) {
        $event.preventDefault();
        if (vform) {
            var data = [];
            data.push($scope.DetailItem);
            data[0].DateCome = $scope.Location_DateCome.value();
            data[0].DateLeave = $scope.Location_DateLeave.value();
            data[0].LoadingStart = $scope.Location_LoadingStart.value();
            data[0].LoadingEnd = $scope.Location_LoadingEnd.value();
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Index.URL.SRWinToDITOLocationUpdate,
                data: { lstDITOLocation: data },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Đã cập nhật!', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Location_Grid.dataSource.read();
                    win.close();
                }
            })
        }
    }

    $scope.Change_View = function ($event) {
        $scope.View_Vehicle = !$scope.View_Vehicle;
        if (!$scope.View_Vehicle) {
            $timeout(function () {
                $scope.SRMasterDN_Grid.dataSource.read();
                $scope.SRMasterDN_Grid.resize();
            }, 10)
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
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Index.URL.GOPReturn_Save,
                data: { data: data, masterID: $scope._id },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })

        if (lstErr.length > 0) {
            var lst = lstErr.join(', ');
            $rootScope.Message({ Msg: 'Dòng ' + lst + ' chưa chọn nhóm SP', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }
    //#endregion

    //#region Init
    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_Index.URL.TruckList,
        data: {},
        success: function (res) {
            _MONMonitor_Index.Data._dataTruck = res.Data;
            if (_MONMonitor_Index.Data._dataTruck.length > 0 && _MONMonitor_Index.Data._dataVendor.length > 0)
                $scope.InitDataVendorVehicle();
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_Index.URL.DriverList,
        data: {},
        success: function (res) {
            $scope._dataDriver = res.Data;
            $scope.GroupDriver_CbbOptions.dataSource.data(res.Data);
            $scope.Driver1_CbbOptions.dataSource.data(res.Data);
            $scope.Driver2_CbbOptions.dataSource.data(res.Data);
            $scope.Assistant1_CbbOptions.dataSource.data(res.Data);
            $scope.Assistant2_CbbOptions.dataSource.data(res.Data);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.Province,
        data: {},
        success: function (pres) {
            $scope.NODN_Province_CbbOptions.dataSource.data(pres.Data);

            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: Common.ALL.URL.District,
                data: {},
                success: function (dres) {
                    $scope._dataDistrict = [];
                    angular.forEach(dres.Data, function (v, i) {
                        if (!Common.HasValue($scope._dataDistrict[v.ProvinceID]))
                            $scope._dataDistrict[v.ProvinceID] = [];
                        $scope._dataDistrict[v.ProvinceID].push(v);
                    });
                }
            })
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_Index.URL.LocationList,
        data: {},
        success: function (res) {
            _MONMonitor_Index.Data._dataLocation = res.Data;
            $scope.LocationTemplateOptions.dataSource.data(res.Data);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_Index.URL.TroubleList,
        data: {},
        success: function (res) {
            $scope.GroupOfTrouble_CbbOptions.dataSource.data(res.Data);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_Index.URL.VendorList,
        data: {},
        success: function (res) {
            res.Data.push({
                VendorID: -1,
                VendorName: 'Xe nhà'
            });
            _MONMonitor_Index.Data._dataVendor = res.Data;
            if (_MONMonitor_Index.Data._dataTruck.length > 0 && _MONMonitor_Index.Data._dataVendor.length > 0)
                $scope.InitDataVendorVehicle();
            $scope.GroupVender_CbbOptions.dataSource.data(res.Data);
        }
    })

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_TroubleCostStatus,
        success: function (res) {
            $scope.TroubleCostStatus_CbbOptions.dataSource.data(res);
            if (Common.HasValue(res[0])) {
                _MONMonitor_Index.Data._trouble1stStatus = res[0].ID;
            }
            $scope.IsLoading = false;
        }
    });

    $scope.Init_CK = function () {

        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = Common.Date.AddDay(new Date(), -2);
            $scope.Search.DateTo = Common.Date.AddDay(new Date(), 2);
            $scope.Search.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Search.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Search.RouteInDay = 3;
        }
        else {
            $scope.Search.DateFrom = Common.Date.AddDay(new Date(), -2);
            $scope.Search.DateTo = Common.Date.AddDay(new Date(), 2);
            $scope.Search.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Search.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Search.RouteInDay = 3;
        }
    }
    $scope.Init_CK();

    $timeout(function () {
        _MONMonitor_Index.Timeline = Common.Timeline({
            grid: $scope.SRVehicle_Grid,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        RegNo: { type: 'string' },
                        MaxWeightCal: { type: 'number' },
                        VendorName: { type: 'string' }
                    }
            },
            modelGroup: [{ field: 'VendorName' }],
            modelSort: { field: 'VendorName', dir: 'asc' },
            columns: [
                { field: 'VendorName', hidden: true, groupHeaderTemplate: '#= value #' },
                { field: 'RegNo', width: '90px', title: 'Xe', template: '<div class="bgtruck allowdrop"><i ng-click="OnVehicleClick($event)" class="fa fa-truck"></i>&nbsp;#=RegNo#</div>', sortable: true, locked: true },
                { field: 'MaxWeightCal', width: '50px', template: '<div class="allowdrop">#=MaxWeightCal==null?"":MaxWeightCal#</div>', title: 'T.tải', sortable: true, locked: true }
            ],
            search: $scope.Search,
            eventMainData: function () {
                if (!$scope.IsShowVehicleVendor) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.SRVehicleList,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _MONMonitor_Index.Data._dataVehicle = [];
                                _MONMonitor_Index.Timeline.SetMainData(res);
                            });
                        }
                    });
                } else {
                }
            },
            eventDetailData: function (dtFrom, dtTo) {
                Common.Log('VehicleDetailData');
                if (!$scope.IsShowVehicleVendor) {
                    var lst = _MONMonitor_Index.Timeline.GetListRouteInDay();
                    var str = '';
                    $.each(lst, function (i, v) {
                        str += '[' + v.Name + ']:' + Common.Date.ToString($scope.Search.HourFrom, Common.Date.Format.HM) + '-' + Common.Date.ToString($scope.Search.HourTo, Common.Date.Format.HM) + '&nbsp;&nbsp;';
                    });

                    if (str != '')
                        str = 'Ngày: ' + Common.Date.ToString($scope.Search.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
                            Common.Date.ToString($scope.Search.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' + str;
                    else
                        str = 'Ngày: ' + Common.Date.ToString($scope.Search.DateFrom, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' +
                            Common.Date.ToString($scope.Search.HourFrom, Common.Date.Format.HM) + ' - ' + Common.Date.ToString($scope.Search.HourTo, Common.Date.Format.HM);
                    $scope.Search.ConfigString = str;

                    var param = Common.Request.Create({
                        Sorts: [], Filters: [
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.GreaterThanOrEqual, dtFrom),
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.LessThanOrEqual, dtTo)
                        ]
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.SRVehicleTimeList,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                angular.forEach(res, function (o, i) {
                                    o.DateFrom = Common.Date.FromJson(o.DateFrom);
                                    o.DateTo = Common.Date.FromJson(o.DateTo);
                                })
                                _MONMonitor_Index.Timeline.SetDetailData(res);
                            });
                        }
                    });
                } else {
                    var param = Common.Request.Create({
                        Sorts: [], Filters: [
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.GreaterThanOrEqual, dtFrom),
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.LessThanOrEqual, dtTo)
                        ]
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRoute.URL.VehicleTOVEN,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $.each(res, function (i, v) {
                                    v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                    v.DateTo = Common.Date.FromJson(v.DateTo);
                                });
                                _OPSAppointment_DIRoute.Timeline.SetDetailData(res);
                            });
                        }
                    });
                }
            },
            eventClickTime: function (id, item, typeid) {
                Common.Log('VehicleClickTime');
                _MONMonitor_Index.Data._currentTimeline = item;

                $scope.Show_SRVehicleCompleteDN = false;
                $scope._typeID = typeid;
                if ($scope._typeID == 2)
                    $scope.Show_SRVehicleMasterVehicleLocation = true;
                else
                    $scope.Show_SRVehicleMasterVehicleLocation = false;

                if (typeid > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.SRVehicleTimeGet,
                        data: { id: id },
                        success: function (res) {
                            $scope.CurrentMaster = res;
                            $scope._id = res.ID;
                            $scope.Show_SRVehicleMasterInfo = true;
                            if (res.IsComplete) {
                                $scope.Show_SRVehicleMasterComplete = false;
                            }
                            else {
                                $scope.Show_SRVehicleMasterComplete = true;
                            }

                            if (res.IsComplete) {
                                $scope.Show_SRVehicleMasterRevert = true;
                            }
                            else {
                                $scope.Show_SRVehicleMasterRevert = false;
                            }
                            $scope.ReloadMap();
                            //$scope.MapRefresh();
                            $scope.MapVehicleLocation();
                        }
                    })
                }
            },
            eventSelect: function (lst) {
                if (lst[0].VendorName == " Xe nhà")
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.Vehicle_GetOPS,
                        data: { vehicleID: lst[0].ID },
                        success: function (res) {
                            $scope.VehicleData = res;
                        }
                    })
            }
        });
        _MONMonitor_Index.Timeline.Init();

        $scope.CreateMap();
    }, 500);

    $scope.MapRefresh = function () {
        Common.Log("MapRefresh");

        if (Map.HasMap) {
            if (Common.HasValue(_MONMonitor_Index.Map.MapInfo))
                $(_MONMonitor_Index.Map.MapInfo).trigger('close');
            $scope.MapDrawMarkers();
            //$scope.MapDrawLines();
            $timeout(function () {
                var showMarkers = [];
                Common.Data.Each(_MONMonitor_Index.Data._mapMarkers, function (item) {
                    if (item.getMap() == _MONMonitor_Index.Map.Map)
                        showMarkers.push(item);
                })
                Map.FitBounds(_MONMonitor_Index.Map.Map, showMarkers);
            }, 500);
        }
    }

    $scope.MapDrawMarkers = function () {
        Common.Log("MapDrawMarkers");

        if (Map.HasMap) {
            Common.Data.Each(_MONMonitor_Index.Data._mapMarkers, function (o) {
                o.setMap(null);
                o = null;
            });
            _MONMonitor_Index.Data._mapMarkers = [];
            if (Common.HasValue($scope.CurrentMaster)) {
                Common.Data.Each($scope.CurrentMaster.ListLocation, function (item) {
                    if (!Common.HasValue(_MONMonitor_Index.Data._mapMarkers[item.ID])) {
                        if (item.Lat > 0 && item.Lng > 0) {
                            var img = Common.String.Format(Map.ImageURL.NumericPoint_Green, item.SortOrder);
                            _MONMonitor_Index.Data._mapMarkers[item.ID] = Map.Marker(_MONMonitor_Index.Map.Map, item.Location, item.Lat, item.Lng, img, 1, function (sender, event, param) {
                                $scope.MapShowInfo(sender, param);
                            }, item);
                        }
                    }
                })
            }
        }
    }

    $scope.MapShowInfo = function (sender, param) {
        Common.Log("MapDrawLines");
        //if (Common.HasValue(_MONMonitor_Index.Map.MapInfo))
        //    $(_MONMonitor_Index.Map.MapInfo).trigger('close');

        //_MONMonitor_Index.Map.MapInfo.open(_MONMonitor_Index.Map.Map, sender);
        $scope.Map_Info_Win.center().open();
        $scope._locationID = param.ID;

        $scope.txtMapLocation = param.Location + " - " + param.DistrictName + ", " + param.ProvinceName;
        $scope.txtMapAddress = param.Address;
        $scope.lblMapDate = "";
        $scope.dtpSignature = new Date();

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.MapLocationGetStatus,
            data: { masterID: $scope._id, locationID: $scope._locationID },
            success: function (res) {
                Common.Log("GetStatus");
                $scope._locationStatusID = res.StatusID;
                if ($scope._locationStatusID > 0 && $scope._locationStatusID < 5) {
                    $scope.ShowBtnMapSave = true;
                    switch ($scope._locationStatusID) {
                        case 1:
                            $scope.lblMapDate = "T.g đến";
                            break;
                        case 2:
                            $scope.lblMapDate = "T.g vào máng";
                            break;
                        case 3:
                            $scope.lblMapDate = "T.g cắt máng";
                            break;
                        case 4:
                            $scope.lblMapDate = "T.g đi";
                            break;
                    }
                } else {
                    $scope.lblMapDate = "Thời gian";
                    $scope.ShowBtnMapSave = false;
                }
            }
        })
    }

    $scope.MapVehicleLocation = function () {
        Common.Log("MapVehicleLocation");
        if (Map.HasMap) {
            if (Common.HasValue(_MONMonitor_Index.Data._mapMarkerTruck)) {
                _MONMonitor_Index.Data._mapMarkerTruck.setMap(null);
                _MONMonitor_Index.Data._mapMarkerTruck = null;
            }
            if ($scope._typeID == 2) {
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_Index.URL.SRVehicleTimeVehicleLocation,
                    data: { masterID: $scope._id },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Cập nhật tọa độ thành công.', NotifyType: Common.Message.NotifyType.SUCCESS });
                        var img = Common.String.Format(Map.ImageURL.Truck_Orange);
                        Common.Data.Each(res.Data, function (o) {
                            if (o.Lat > 0 && o.Lng > 0) {
                                _MONMonitor_Index.Data._mapMarkerTruck = Map.Marker(_MONMonitor_Index.Map.Map, o.RegNo, o.Lat, o.Lng, img, 1, function (sender, event, param) {
                                    //Chưa có yêu cầu
                                }, o);
                            }
                        })
                    }

                })
            }
        }
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
        var lstVehicle = _MONMonitor_Index.Data._dataTruck;
        var lstVendor = _MONMonitor_Index.Data._dataVendor;
        _MONMonitor_Index.Data._dataVehicleByVEN = [];
        _MONMonitor_Index.Data._dataVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.lstVendorID.length; i++) {
                    var vendorid = vehicle.lstVendorID[i];
                    if (!Common.HasValue(_MONMonitor_Index.Data._dataVehicleByVEN[vendorid]))
                        _MONMonitor_Index.Data._dataVehicleByVEN[vendorid] = [];
                    _MONMonitor_Index.Data._dataVehicleByVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _MONMonitor_Index.Data._dataVehicleHome.push(vehicle);
                }
            } else {
                _MONMonitor_Index.Data._dataVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_MONMonitor_Index.Data._dataVehicleByVEN[vendorid]))
                        _MONMonitor_Index.Data._dataVehicleByVEN[vendorid] = [];
                    _MONMonitor_Index.Data._dataVehicleByVEN[vendorid].push(vehicle);
                }
            }
        });
    };

    //#endregion
    //#region new map

    $scope.CreateMap = function () {
        Common.Log("CreateMap");

        Common.Log(openMap.hasMap);

        openMap.Create({
            Element: 'MON_Map',
            Tooltip_Show: true,
            Tooltip_Element: 'MON_Map_tooltip',
            InfoWin_Show: true,
            InfoWin_Element: 'Map_Info_Win',
            InfoWin_Width: 400,
            InfoWin_Height: 200,
            ClickMarker: function (o, l) {
                //$scope.Map_Info_Win.center().open();
                $scope._locationID = o.ID;

                $scope.txtMapLocation = o.Location + " - " + o.DistrictName + ", " + o.ProvinceName;
                $scope.txtMapAddress = o.Address;
                $scope.lblMapDate = "";
                $scope.SignatureDtp.value(new Date());

                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_Index.URL.MapLocationGetStatus,
                    data: { masterID: $scope._id, locationID: $scope._locationID },
                    success: function (res) {
                        Common.Log("GetStatus");
                        $scope._locationStatusID = res.StatusID;
                        if ($scope._locationStatusID > 0 && $scope._locationStatusID < 5) {
                            $scope.ShowBtnMapSave = true;
                            switch ($scope._locationStatusID) {
                                case 1:
                                    $scope.lblMapDate = "T.g đến";
                                    break;
                                case 2:
                                    $scope.lblMapDate = "T.g vào máng";
                                    break;
                                case 3:
                                    $scope.lblMapDate = "T.g cắt máng";
                                    break;
                                case 4:
                                    $scope.lblMapDate = "T.g đi";
                                    break;
                            }
                        } else {
                            $scope.lblMapDate = "Thời gian";
                            $scope.ShowBtnMapSave = false;
                        }
                    }
                })
            },
            ClickMap: null
        });
        openMap.SetVisible(null, false); //set all vectorlayers invisible from map
        $scope.ReloadMap();
    }

    $scope.ReloadMap = function () {
        Common.Log("ReloadMap");

        openMap.Close(); //Hide info window
        openMap.ClearRoute(); //Clear route data, make them invisible from map
        openMap.SetVisible(null, false); //set all vectorlayers invisible from map

        var imgStockGreen = Common.String.Format(openMap.mapImage.Stock_Green);
        var imgDepotGreen = Common.String.Format(openMap.mapImage.Depot_Green);

        var data = [];
        _MONMonitor_Index.Data._mapMarkers = [];
        if (Common.HasValue($scope.CurrentMaster)) {
            Common.Data.Each($scope.CurrentMaster.ListLocation, function (item) {
                if (!Common.HasValue(_MONMonitor_Index.Data._mapMarkers[item.ID])) {
                    var img = Common.String.Format(openMap.mapImage.NumericPoint_Green, item.SortOrder);
                    var icon = openMap.mapStyle.Icon(img, 1);
                    _MONMonitor_Index.Data._mapMarkers[item.ID] = openMap.Marker(item.Lat, item.Lng, "", icon, item);
                    data.push(_MONMonitor_Index.Data._mapMarkers[item.ID]);
                }
            })
        }
        if (data.length > 0)
            openMap.FitBound(data);
    }

    //#endregion
    $scope.GetSumTon = function (value) {
        if (Common.HasValue($scope.TOGroup_Grid) && Common.HasValue($scope.TOGroup_Grid.tbody)) {
            var datasource = $scope.TOGroup_Grid.dataSource;
            var result = 0;
            $(datasource.view()).each(function (index, element) {
                if (element.value === value) {
                    var items = element.items;
                    $.each(items, function (idx, e) {
                        result += e.TonTranfer;
                    });
                }
            });

            return Math.round(result * 1000) / 1000;
        }
    }

    $scope.GetSumCBM = function (value) {

        if (Common.HasValue($scope.TOGroup_Grid) && Common.HasValue($scope.TOGroup_Grid.tbody)) {
            var datasource = $scope.TOGroup_Grid.dataSource;
            var result = 0;
            $(datasource.view()).each(function (index, element) {
                if (element.value === value) {
                    var items = element.items;
                    $.each(items, function (idx, e) {
                        result += e.CBMTranfer;
                    });
                }
            });

            return Math.round(result * 1000) / 1000;
        }
    }

    $scope.GetSumQuantity = function (value) {

        if (Common.HasValue($scope.TOGroup_Grid) && Common.HasValue($scope.TOGroup_Grid.tbody)) {
            var datasource = $scope.TOGroup_Grid.dataSource;
            var result = 0;
            $(datasource.view()).each(function (index, element) {
                if (element.value === value) {
                    var items = element.items;
                    $.each(items, function (idx, e) {
                        result += e.QuantityTranfer;
                    });
                }
            });

            return Math.round(result * 1000) / 1000;
        }
    }
}])