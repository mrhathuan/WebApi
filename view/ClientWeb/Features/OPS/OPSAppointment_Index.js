/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSApointment_Index = {
    URL: {
        DI: {
            Order_List: 'OPSDI_MAP_Order_List',
            ToOPS: 'OPSDI_MAP_ToOPS',
            Delete: 'OPSDI_MAP_Delete',
            ToMon: 'OPSDI_MAP_ToMON',
            Save: 'OPSDI_MAP_Save',
            Cancel: 'OPSDI_MAP_Cancel',
            Update: 'OPSDI_MAP_Update',
            TripByID: 'OPSDI_MAP_TripByID',
            VehicleVendor_List: 'OPSDI_MAP_VehicleVendor_List',
            Setting: 'OPSCO_MAP_Setting',

            Vendor_List: 'OPSCO_MAP_Vendor_List',
            Driver_List: 'OPSCO_MAP_Driver_List',
            DriverVendor_List: 'OPSCO_MAP_DriverVendor_List',

            Schedule_Data: 'OPSDI_MAP_Schedule_Data',
            CheckVehicleAvailable: 'OPSDI_MAP_CheckVehicleAvailable',

            Location_List: 'OPSCO_MAP_Location_List',

            Split: 'OPSDI_MAP_GroupProduct_Split',
            Split_Cancel: 'OPSDI_MAP_GroupProduct_Split_Cancel',

            DITOGroupProduct_List: 'OPSDI_MAP_DITOGroupProduct_List',
            DI2View_GroupProduct_List: 'OPSDI_MAP_2View_GroupProduct_List',
            DI2View_Master_Update_Check4Delete: 'OPSDI_MAP_2View_Master_Update_Check4Delete',
            DI2View_Master_Update_Check4Consolidate: 'OPSDI_MAP_2View_Master_Update_Check4Consolidate',
            DI2View_Master_Update_Group_Quantity: 'OPSDI_MAP_2View_Master_Update_Group_Quantity',
            DI2View_Master_Update_Check4Update: 'OPSDI_MAP_2View_Master_Update_Check4Update',
            DI2View_Master_Update_TimeLine: 'OPSDI_MAP_2View_Master_Update_TimeLine',
            DI2View_Master_Update_Group: 'OPSDI_MAP_2View_Master_Update_Group',
            DI2View_Master_Update: 'OPSDI_MAP_2View_Master_Update',
        },
        CO: {
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
        }
    },
    Data: {
        Location: {
            LocationStartID: -1,
            LocationStartName: '',
            LocationEndID: -1,
            LocationEndName: '',
            LocationStartLat: null,
            LocationStartLng: null,
            LocationEndLat: null,
            LocationEndLng: null,
            LocationRomoocID: -1,
            LocationRomoocName: '',
            LocationRomoocLat: null,
            LocationRomoocLng: null,
        },
        VendorList: [],
        VehicleInfo: []
    }
}

angular.module('myapp').controller('OPSAppointment_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_IndexCtrl');
    $rootScope.IsLoading = false;

    angular.element('.mainform .mainmenu').removeClass('fullscreen');
    $scope.Auth = $rootScope.GetAuth();
    Common.Log($scope.Auth)
    $scope.opsDiagram_Options = {
        layout: { type: "tree", subtype: "down", horizontalSeparation: 30, verticalSeparation: 20 },
        editable: false, pannable: false, zoomMax: 1, zoomMin: 1, selectable: false,
        click: function (e) {
            if (e.item instanceof kendo.dataviz.diagram.Shape) {
                var item = e.item.dataItem;
                if (item.Code != "#") {
                    if (item.View != null) {
                        var views = item.View.split(',');
                        var flag = false;
                        angular.forEach(views, function (i, o) {
                            if ($scope.Auth[i] == true)
                                flag = true;
                        })
                        if (flag) {
                            if (item.Code == "main.OPSAppointment.DIPacket") {
                                if ($scope.Auth.ViewAdmin)
                                    $state.go(item.Code);
                                else
                                    $state.go("main.OPSAppointment.DIPacket_Ven");
                            } else {
                                $state.go(item.Code);
                            }
                        } else {
                            $rootScope.Message({ Msg: "Tài khoản không có quyền chức năng này!", NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    }
                    else if (item.Action != null) {
                        var actions = item.Action.split(',');
                        var flag = false;
                        angular.forEach(actions, function (i, o) {
                            if ($scope.Auth[i] == true)
                                flag = true;
                        })
                        if (flag) {
                            if (item.Code != "main.OPSAppointment.COViewOnMapV2" && item.Code != "main.OPSAppointment.DIViewOnMapV2") {
                                $state.go(item.Code);
                            }
                            else if (item.Code == "main.OPSAppointment.COViewOnMapV2") {
                                var objCookie = Common.Cookie.Get("view.OPSAppointment.COViewOnMap");
                                if (objCookie == null || objCookie == "" || objCookie == item.Code) {
                                    objCookie = Common.Cookie.Get("COViewOnMapV2_View");
                                    $state.go(item.Code, { view: objCookie });
                                } else {
                                    $state.go(objCookie);
                                }
                            } else if (item.Code == "main.OPSAppointment.DIViewOnMapV2") {
                                var objCookie = Common.Cookie.Get("view.OPSAppointment.DIViewOnMap");
                                if (objCookie == null || objCookie == "" || objCookie == item.Code) {
                                    objCookie = Common.Cookie.Get("DIViewOnMapV2_View");
                                    $state.go(item.Code, { view: objCookie });
                                } else {
                                    $state.go(objCookie);
                                }
                            }
                        } else {
                            $rootScope.Message({ Msg: "Tài khoản không có quyền chức năng này!", NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    }
                }
            }
        }
    };

    $timeout(function () {
        var dataShapes = [], dataConnections = [];
        var pLeft = 0;

        if ($scope.Auth.ActContainer && $scope.Auth.ActTruck) {
            dataShapes = [
                { ID: 1, Title: "VT phân phối", Code: 'main.OPSAppointment.DI', X: 200, Y: 30, W: 80, Image: "ico_di.png", Action: 'ActReturn' },
                { ID: 4, Title: "Auto", Code: '#', X: 100, Y: 160, W: 70, Image: "ico_auto.png" },
                { ID: 5, Title: "Manual", Code: '#', X: 310, Y: 160, W: 70, Image: "ico_manual.png" },
                { ID: 21, Title: "Import", Code: 'main.OPSAppointment.DIImportPacket', X: 40, Y: 280, W: 70, Image: "ico_import.png", Action: 'ActAdd,ActDel' },
                { ID: 22, Title: "Optimize", Code: 'main.OPSAppointment.DIOptimizer', X: 160, Y: 280, W: 70, Image: "ico_otimize.png", Action: 'ActAdd,ActDel' },
                { ID: 8, Title: "Non-tender", Code: 'main.OPSAppointment.DIViewOnMapV2', X: 250, Y: 280, W: 70, Image: "ico_no-tender.png", Action: 'ActAdd,ActDel' },
                { ID: 9, Title: "Tender", Code: 'main.OPSAppointment.DIRouteMasterVEN', X: 370, Y: 280, W: 70, Image: "ico_tender.png", View: 'ViewAdmin,ViewVendor' },
                { ID: 12, Title: "FTL", Code: '#', X: 310, Y: 400, W: 70, Image: "ico_ftl.png", View: 'ViewAdmin,ViewVendor' },
                { ID: 13, Title: "LTL", Code: 'main.OPSAppointment.DIPacket', X: 430, Y: 400, W: 70, Image: "ico_ltl.png", View: 'ViewAdmin,ViewVendor' },
                { ID: 3, Title: "VT container", Code: 'main.OPSAppointment.CO', X: 680, Y: 30, W: 80, Image: "ico_co.png", Action: 'ActReturn' },
                { ID: 6, Title: "Auto", Code: '#', X: 580, Y: 160, W: 70, Image: "ico_auto.png" },
                { ID: 7, Title: "Manual", Code: '#', X: 790, Y: 160, W: 70, Image: "ico_manual.png" },
                { ID: 23, Title: "Import", Code: 'main.OPSAppointment.COImportPacket', X: 520, Y: 280, W: 70, Image: "ico_import.png", Action: 'ActAdd,ActDel' },
                { ID: 24, Title: "Optimize", Code: 'main.OPSAppointment.COOptimizer', X: 640, Y: 280, W: 70, Image: "ico_otimize.png", Action: 'ActAdd,ActDel' },
                { ID: 10, Title: "Non-tender", Code: 'main.OPSAppointment.COViewOnMapV2', X: 730, Y: 280, W: 70, Image: "ico_no-tender.png", Action: 'ActAdd,ActDel' },
                { ID: 11, Title: "Tender", Code: 'main.OPSAppointment.COViewVendor', X: 850, Y: 280, W: 70, Image: "ico_tender.png", View: 'ViewAdmin,ViewVendor' },
                { ID: 14, Title: "FCL", Code: 'main.OPSAppointment.SettingTenderFCL', X: 780, Y: 400, W: 70, Image: "ico_fcl.png", View: 'ViewAdmin,ViewVendor' },
                { ID: 15, Title: "LCL", Code: '#', X: 910, Y: 400, W: 70, Image: "ico_lcl.png", View: 'ViewAdmin,ViewVendor' },
            ]
            dataConnections = [
                { ID: 1, FShape: 1, TShape: 4 },
                { ID: 2, FShape: 1, TShape: 5 },
                { ID: 3, FShape: 3, TShape: 6 },
                { ID: 4, FShape: 3, TShape: 7 },
                { ID: 5, FShape: 5, TShape: 8 },
                { ID: 6, FShape: 5, TShape: 9 },
                { ID: 7, FShape: 7, TShape: 10 },
                { ID: 8, FShape: 7, TShape: 11 },
                { ID: 9, FShape: 9, TShape: 12 },
                { ID: 10, FShape: 9, TShape: 13 },
                { ID: 11, FShape: 11, TShape: 14 },
                { ID: 12, FShape: 11, TShape: 15 },
                { ID: 13, FShape: 4, TShape: 21 },
                { ID: 14, FShape: 4, TShape: 22 },
                { ID: 15, FShape: 6, TShape: 23 },
                { ID: 16, FShape: 6, TShape: 24 }
            ]
        } else if ($scope.Auth.ActContainer) {
            pLeft = 200;
            dataShapes = [
                { ID: 1, Title: "VT container", Code: 'main.OPSAppointment.CO', X: 200, Y: 30, W: 80, Image: "ico_co.png", Action: 'ActReturn' },
                { ID: 4, Title: "Auto", Code: '#', X: 100, Y: 160, W: 70, Image: "ico_auto.png" },
                { ID: 5, Title: "Manual", Code: '#', X: 310, Y: 160, W: 70, Image: "ico_manual.png" },
                { ID: 21, Title: "Import", Code: 'main.OPSAppointment.COImportPacket', X: 40, Y: 280, W: 70, Image: "ico_import.png", Action: 'ActAdd,ActDel' },
                { ID: 22, Title: "Optimize", Code: 'main.OPSAppointment.COOptimizer', X: 160, Y: 280, W: 70, Image: "ico_otimize.png", Action: 'ActAdd,ActDel' },
                { ID: 8, Title: "Non-tender", Code: 'main.OPSAppointment.COViewOnMapV2', X: 250, Y: 280, W: 70, Image: "ico_no-tender.png", Action: 'ActAdd,ActDel' },
                { ID: 9, Title: "Tender", Code: 'main.OPSAppointment.COViewVendor', X: 370, Y: 280, W: 70, Image: "ico_tender.png", View: 'ViewAdmin,ViewVendor' },
                { ID: 12, Title: "FCL", Code: '#', X: 310, Y: 400, W: 70, Image: "ico_fcl.png", View: 'ViewAdmin,ViewVendor' },
                { ID: 13, Title: "LCL", Code: '#', X: 430, Y: 400, W: 70, Image: "ico_lcl.png", View: 'ViewAdmin,ViewVendor' }
            ]
            dataConnections = [
                { ID: 1, FShape: 1, TShape: 4 },
                { ID: 2, FShape: 1, TShape: 5 },
                { ID: 3, FShape: 4, TShape: 21 },
                { ID: 4, FShape: 4, TShape: 22 },
                { ID: 5, FShape: 5, TShape: 8 },
                { ID: 6, FShape: 5, TShape: 9 },
                { ID: 7, FShape: 9, TShape: 12 },
                { ID: 8, FShape: 9, TShape: 13 },
                { ID: 9, FShape: 11, TShape: 14 }
            ]
        } else if ($scope.Auth.ActTruck) {
            pLeft = 200;
            dataShapes = [
               { ID: 1, Title: "VT phân phối", Code: 'main.OPSAppointment.DI', X: 200, Y: 30, W: 80, Image: "ico_di.png", Action: 'ActReturn' },
               { ID: 4, Title: "Auto", Code: '#', X: 100, Y: 160, W: 70, Image: "ico_auto.png" },
               { ID: 5, Title: "Manual", Code: '#', X: 310, Y: 160, W: 70, Image: "ico_manual.png" },
               { ID: 21, Title: "Import", Code: 'main.OPSAppointment.DIImportPacket', X: 40, Y: 280, W: 70, Image: "ico_import.png", Action: 'ActAdd,ActDel' },
               { ID: 22, Title: "Optimize", Code: 'main.OPSAppointment.DIOptimizer', X: 160, Y: 280, W: 70, Image: "ico_otimize.png", Action: 'ActAdd,ActDel' },
               { ID: 8, Title: "Non-tender", Code: 'main.OPSAppointment.DIViewOnMapV2', X: 250, Y: 280, W: 70, Image: "ico_no-tender.png", Action: 'ActAdd,ActDel' },
               { ID: 9, Title: "Tender", Code: 'main.OPSAppointment.DIRouteMasterVEN', X: 370, Y: 280, W: 70, Image: "ico_tender.png", View: 'ViewAdmin,ViewVendor' },
               { ID: 12, Title: "FTL", Code: '#', X: 310, Y: 400, W: 70, Image: "ico_ftl.png", View: 'ViewAdmin,ViewVendor' },
               { ID: 13, Title: "LTL", Code: 'main.OPSAppointment.DIPacket', X: 430, Y: 400, W: 70, Image: "ico_ltl.png", View: 'ViewAdmin,ViewVendor' }
            ]
            dataConnections = [
                { ID: 1, FShape: 1, TShape: 4 },
                { ID: 2, FShape: 1, TShape: 5 },
                { ID: 3, FShape: 4, TShape: 21 },
                { ID: 4, FShape: 4, TShape: 22 },
                { ID: 5, FShape: 5, TShape: 8 },
                { ID: 6, FShape: 5, TShape: 9 },
                { ID: 7, FShape: 9, TShape: 12 },
                { ID: 8, FShape: 9, TShape: 13 },
                { ID: 9, FShape: 11, TShape: 14 }
            ]
        }

        angular.forEach(dataShapes, function (value, key) {
            var dataviz = kendo.dataviz;
            var shape = new dataviz.diagram.Shape({
                type: 'circle', id: value.ID, stroke: { width: 1, color: value.Code == "#" ? "#acb8c4" : "#31B6FC" }, editable: false,
                x: value.X + pLeft, y: value.Y, fill: "transparent", width: value.W, height: value.W, dataItem: value,
                visual: function (e) {
                    var g = new dataviz.diagram.Group();
                    var dataItem = e.dataItem;
                    g.append(new dataviz.diagram.Image({
                        source: "../Images/ops/" + dataItem.Image,
                        x: 0, y: 0, width: dataItem.W, height: dataItem.W
                    }));
                    return g;
                }
            })
            $scope.opsDiagram.addShape(shape);
        });
        angular.forEach(dataConnections, function (value, key) {
            var fShape = $scope.opsDiagram.getShapeById(value.FShape);
            var tShape = $scope.opsDiagram.getShapeById(value.TShape);
            var connection = new kendo.dataviz.diagram.Connection(fShape, tShape, {
                type: "cascading", stroke: { color: "#acb8c4" }
            });
            $scope.opsDiagram.addConnection(connection);
        });
    }, 300)

    //#region DIView
    $scope.Color = {
        None: '#f6fafe',
        Error: '#fc0000',
        Warning: '#ffaa00',
        Success: '#31B6FC'
    }
    $scope.ChangeData = false;
    $scope.IsShowNewTrip = false;
    $scope.NewTripChoose = false;
    $scope.NewTripViewAction = 0; //0: Grid, 1: Edit, 2: TimeLine
    $scope.NewViewTripDate = false;
    $scope.NewViewTripApproved = true;
    $scope.NewViewTripTendered = false;
    $scope.ShowNewTripDate = false;
    $scope.NewTripDateRequest = { fDate: null, tDate: null };
    $scope.NewViewTripLoading = false;
    $scope.NewViewDataSelect = [];

    //#region Xem chuyến
    $scope.DI_Click = function ($event, grid, win) {
        $event.preventDefault();

        $scope.NewTripChoose = false;
        $scope.NewTripViewAction = 0;
        $scope.NewViewTripDate = false;
        $scope.NewViewTripApproved = true;
        $scope.NewViewTripTendered = false;
        $scope.ShowNewTripDate = false;
        $scope.NewTripDateRequest = { fDate: null, tDate: null };
        $scope.NewViewTripLoading = true;

        grid.dataSource.read();
        win.center().open();
        $scope.IsShowNewTrip = true;
        $timeout(function () {
            $scope.NewViewTripLoading = false;
        }, 1000)
    }

    $scope.$watch("IsShowNewTrip", function () {
        if ($scope.IsShowNewTrip == false && $scope.ChangeData == true) {
            //Reset TripItem và grid đơn hàng.
            $scope.ResetTrip(true);
            //Refresh grid phương tiện
            if ($scope.IsVehicleVendor == false) {
                $scope.vehicle_Grid.dataSource.read();
            } else {
                $scope.vehicleByVendor_Grid.dataSource.read();
            }
        }
    })

    $scope.new_trip_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.DITOGroupProduct_List,
            pageSize: 100,
            group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    isApproved: $scope.NewViewTripApproved,
                    isTendered: $scope.NewViewTripTendered,
                    fDate: $scope.NewTripDateRequest.fDate, tDate: $scope.NewTripDateRequest.tDate
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
                    TOCreatedDate: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, columnMenu: false, resizable: true, reorderable: true, sortable: true, filterable: { mode: 'row' }, autoBind: false,
        dataBound: function () {
            $scope.NewTripChoose = false;
            $scope.ToOpsAvailable = false;
            $scope.ToMonAvailable = false;
        },
        columns: [
            {
                field: 'Check', title: ' ', width: '35px',
                headerTemplate: '<input class="chkGroupChooseAll" type="checkbox" ng-click="onGroupChooseAll($event,new_trip_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }, filterable: false, sortable: false, groupable: false
            },
            {
                field: 'TOMasterCode', width: '120px', title: 'Mã chuyến',
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
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            var strTitle = "Kế hoạch";
                            var sty = "width:20px;background:red;display:inline-block;height:16px;position:relative;top:3px;margin-right:7px;"
                            if (obj.TOStatus == 2) {
                                strTitle = "Đã duyệt";
                                sty = "width:20px;background:blue;display:inline-block;height:16px;position:relative;top:3px;margin-right:7px;"
                            }
                            return "<input style='position:relative;top:2px;' ng-click='onGroupChoose($event,new_trip_Grid)' "
                                + "type='checkbox' class='chkGroupChoose' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'></input>"
                                + "<span style='" + sty + "' title='" + strTitle + "'></span>"
                                + "<span>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + strVeh + " - " + strName + " - " + strTel
                                + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + " - Tấn: " + Math.round(sumTon * 100000) / 100000 + " - Khối: " + Math.round(sumCBM * 100000) / 100000 + " </span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', width: '80px', title: 'Loại v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistributorCode', width: '120px', title: 'Nhà phân phối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: '150px', title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: '100px', title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: '80px', title: 'Khối', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: '80px', title: 'SL', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TempMin', width: '100px', title: 'N.Độ tối thiểu', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TempMax', width: '100px', title: 'N.Độ tối đa', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine1', width: '100px', title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine2', width: '100px', title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVehicleNo', width: '120px', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
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

    $scope.onGroupChooseAll = function ($event, grid) {
        var value = $($event.currentTarget).prop('checked'), toops = false, tomon = false;
        grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
            var obj = $(this).data('item');
            if (Common.HasValue(obj)) {
                if (obj.TOStatus == 1 && value) tomon = true;
                if (obj.TOStatus == 2 && value) toops = true;
                $(this).prop('checked', value);
            }
        })
        $timeout(function () {
            $scope.NewTripChoose = value;
            $scope.ToOpsAvailable = toops;
            $scope.ToMonAvailable = tomon;
        }, 100)
    }

    $scope.onGroupChoose = function ($event, grid) {
        var chk = $($event.currentTarget), val = chk.prop('checked'),
            obj = chk.data('item'), toops = false, tomon = false;
        if (Common.HasValue(obj)) {
            if (val && obj.TOStatus == 1) tomon = true;
            if (val && obj.TOStatus == 2) toops = true;

            grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
                var i = $(this).prop('checked'), o = $(this).data('item');
                if (i == true) {
                    val = true;
                    if (Common.HasValue(o) && o.TOStatus == 1) tomon = true;
                    if (Common.HasValue(o) && o.TOStatus == 2) toops = true;
                }
            })

            $timeout(function () {
                $scope.NewTripChoose = val;
                $scope.ToOpsAvailable = toops;
                $scope.ToMonAvailable = tomon;
            }, 100)
        }
    }

    $timeout(function () {
        try {
            $scope.new_trip_Grid.resizable.bind("start", function (e) {
                if ($(e.currentTarget).data("th").data("field") == "Check") {
                    e.preventDefault();
                    setTimeout(function () {
                        $scope.new_trip_Grid.wrapper.removeClass("k-grid-column-resizing");
                        $(document.body).add(".k-grid th").css("cursor", "");
                    });
                }
            });
        } catch (e) { }
    }, 1000)

    $scope.NewToMON_Click = function ($event) {
        $event.preventDefault();

        if ($scope.NewTripViewAction == 0) {
            var data = [], dataWithoutDriver = [], dataTendered = [],
            isApproved = false, isTendered = false, grid = $scope.new_trip_Grid;
            Common.Data.Each(grid.tbody.find('.k-grouping-row .chkGroupChoose'), function (o) {
                var val = $(o).prop('checked'), obj = $(o).data('item');
                if (Common.HasValue(obj) && val) {
                    data.push(obj.TOMasterID);
                    if (obj.TOStatus == 1) {
                        isApproved = true;
                        if (obj.TOVendorID == null && (obj.TODriverName == null || obj.TODriverName == ''))
                            dataWithoutDriver.push(obj.TOMasterCode);
                    }
                    if (obj.TOStatus == 2) {
                        isTendered = true;
                        dataTendered.push(obj.TOMasterCode);
                    }
                }
            })

            if (!isApproved) {
                $rootScope.Message({ Msg: "Vui lòng chọn chuyến đang kế hoạch.", Type: Common.Message.Type.Alert });
                return;
            }
            if (isTendered) {
                $rootScope.Message({ Msg: "Vui lòng bỏ chọn chuyến đã duyệt: " + dataTendered.join(', '), Type: Common.Message.Type.Alert });
                return;
            }
            if (dataWithoutDriver.length > 0) {
                $rootScope.Message({ Msg: "Chưa chọn tài xế. Không thể duyệt. Chuyến: " + dataWithoutDriver.join(', '), Type: Common.Message.Type.Alert });
                return;
            }
            $rootScope.Message({
                Msg: "Xác nhận duyệt các chuyến đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.ToMon,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            });
        }
        else if ($scope.NewTripViewAction == 2) {

        }
    }

    $scope.NewToOPS_Click = function ($event) {
        $event.preventDefault();

        if ($scope.NewTripViewAction == 0) {
            var data = [], dataApproved = [],
                isApproved = false, isTendered = false, grid = $scope.new_trip_Grid;
            Common.Data.Each(grid.tbody.find('.k-grouping-row .chkGroupChoose'), function (o) {
                var val = $(o).prop('checked'), obj = $(o).data('item');
                if (Common.HasValue(obj) && val) {
                    data.push(obj.TOMasterID);
                    if (obj.TOStatus == 1) {
                        isApproved = true;
                        dataApproved.push(obj.TOMasterCode);
                    }
                    if (obj.TOStatus == 2) {
                        isTendered = true;
                    }
                }
            })

            if (!isTendered) {
                $rootScope.Message({ Msg: "Vui lòng chọn chuyến đã duyệt.", Type: Common.Message.Type.Alert });
                return;
            }
            if (isApproved) {
                $rootScope.Message({ Msg: "Vui lòng bỏ chọn chuyến đang kế hoạch: " + dataApproved.join(', '), Type: Common.Message.Type.Alert });
                return;
            }
            $rootScope.Message({
                Msg: "Xác nhận hủy duyệt các chuyến đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.ToOPS,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.NewDelete_Click = function ($event) {
        $event.preventDefault();
        if ($scope.NewTripViewAction == 0) {
            var data = [], dataTendered = [],
                isApproved = false, isTendered = false, grid = $scope.new_trip_Grid;

            Common.Data.Each(grid.tbody.find('.k-grouping-row .chkGroupChoose'), function (o) {
                var val = $(o).prop('checked'), obj = $(o).data('item');
                if (Common.HasValue(obj) && val) {
                    data.push(obj.TOMasterID);
                    if (obj.TOStatus == 1) {
                        isApproved = true;
                    }
                    if (obj.TOStatus == 2) {
                        isTendered = true;
                        dataTendered.push(obj.TOMasterCode);
                    }
                }
            })

            if (!isApproved) {
                $rootScope.Message({ Msg: "Vui lòng chọn chuyến đang kế hoạch.", Type: Common.Message.Type.Alert });
                return;
            }
            if (isTendered) {
                $rootScope.Message({ Msg: "Vui lòng bỏ chọn chuyến đã duyệt: " + dataTendered.join(', '), Type: Common.Message.Type.Alert });
                return;
            }
            $rootScope.Message({
                Msg: "Xác nhận xóa các chuyến đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.Delete,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.tripSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true, min: '400px' },
            { collapsible: true, resizable: true, size: '50%', min: '400px' }
        ]
    }

    //#endregion

    //#region 2View
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSApointment_Index.URL.DI.Vendor_List,
        success: function (res) {
            _OPSApointment_Index.Data.VendorList = res;
            $scope.cboNewTripVendor_Options.dataSource.data(res);
        }
    });

    $scope.NewTripDetail = false;

    $scope.gopTon_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var txt = this;
            var val = this.value();
            var tr = $(e.sender.element).closest('tr');
            var grid = $(e.sender.element).closest('.cus-grid.k-grid').data('kendoGrid');
            var dataItem = grid.dataItem(tr);
            if (val > 0.001 && dataItem.Ton - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 1
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.Ton);
                    }
                })
            }
        }
    }

    $scope.gopCBM_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var txt = this;
            var val = this.value();
            var tr = $(e.sender.element).closest('tr');
            var grid = $(e.sender.element).closest('.cus-grid.k-grid').data('kendoGrid');
            var dataItem = grid.dataItem(tr);
            if (val > 0.001 && dataItem.CBM - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 2
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.CBM);
                    }
                })
            }
        }
    }

    $scope.gopQuantity_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var val = this.value();
            var tr = $(e.sender.element).closest('tr');
            var grid = $(e.sender.element).closest('.cus-grid.k-grid').data('kendoGrid');
            var dataItem = grid.dataItem(tr);
            if (val > 0.001 && dataItem.Quantity - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 3
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.CBM);
                    }
                })
            }
        }
    }

    $scope.gopTOTon_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var txt = this, val = txt.value(), tr = $(txt.element).closest('tr'),
                grid = $(tr).closest('.cus-grid.k-grid').data('kendoGrid'),
                dataItem = grid.dataItem(tr);
            if (val > dataItem.Ton + 0.001) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Check4Update,
                    data: {
                        gopID: dataItem.ID, mID: dataItem.TOMasterID, value: val, packingType: 1
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            if (res == true) {
                                $rootScope.Message({
                                    Msg: "Xác nhận bổ sung đơn hàng?",
                                    Type: Common.Message.Type.Confirm,
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Group_Quantity,
                                            data: {
                                                gopID: dataItem.ID, mID: dataItem.TOMasterID, value: val, packingType: 1
                                            },
                                            success: function (res) {
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: 'Thành công!' });
                                                grid.dataSource.read();
                                                $scope.new2view_gop_Grid.dataSource.read();
                                                $scope.ChangeData = true;
                                            }
                                        });
                                    },
                                    Close: function () {
                                        txt.value(dataItem.Ton);
                                    }
                                })
                            } else {
                                $rootScope.Message({ Msg: "Sản lượng còn lại không đủ. Vui lòng kiểm tra lại!", Type: Common.Message.Type.Alert });
                                txt.value(dataItem.Ton);
                            }
                        }, function () {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Có lỗi xảy ra. Vui lòng thử lại sau!' });
                            txt.value(dataItem.Ton);
                        })
                    }
                });
            } else if (val > 0.001 && dataItem.Ton - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 1
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                grid.dataSource.read();
                                $scope.new2view_gop_Grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.Ton);
                    }
                })
            }
        }
    }

    $scope.gopTOCBM_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var txt = this, val = txt.value(), tr = $(txt.element).closest('tr'),
                grid = $(tr).closest('.cus-grid.k-grid').data('kendoGrid'),
                dataItem = grid.dataItem(tr);
            if (val > dataItem.CBM + 0.001) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Check4Update,
                    data: {
                        gopID: dataItem.ID, mID: dataItem.TOMasterID, value: val, packingType: 2
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            if (res == true) {
                                $rootScope.Message({
                                    Msg: "Xác nhận bổ sung đơn hàng?",
                                    Type: Common.Message.Type.Confirm,
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Group_Quantity,
                                            data: {
                                                gopID: dataItem.ID, mID: dataItem.TOMasterID, value: val, packingType: 2
                                            },
                                            success: function (res) {
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: 'Thành công!' });
                                                grid.dataSource.read();
                                                $scope.new2view_gop_Grid.dataSource.read();
                                                $scope.ChangeData = true;
                                            }
                                        });
                                    },
                                    Close: function () {
                                        txt.value(dataItem.CBM);
                                    }
                                })
                            } else {
                                $rootScope.Message({ Msg: "Sản lượng còn lại không đủ. Vui lòng kiểm tra lại!", Type: Common.Message.Type.Alert });
                                txt.value(dataItem.CBM);
                            }
                        }, function () {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Có lỗi xảy ra. Vui lòng thử lại sau!' });
                            txt.value(dataItem.CBM);
                        })
                    }
                });
            } else if (val > 0.001 && dataItem.CBM - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 2
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                grid.dataSource.read();
                                $scope.new2view_gop_Grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.CBM);
                    }
                })
            }
        }
    }

    $scope.gopTOQuantity_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var txt = this, val = txt.value(), tr = $(txt.element).closest('tr'),
                grid = $(tr).closest('.cus-grid.k-grid').data('kendoGrid'),
                dataItem = grid.dataItem(tr);
            if (val > dataItem.Quantity + 0.001) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Check4Update,
                    data: {
                        gopID: dataItem.ID, mID: dataItem.TOMasterID, value: val, packingType: 3
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            if (res == true) {
                                $rootScope.Message({
                                    Msg: "Xác nhận bổ sung đơn hàng?",
                                    Type: Common.Message.Type.Confirm,
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Group_Quantity,
                                            data: {
                                                gopID: dataItem.ID, mID: dataItem.TOMasterID, value: val, packingType: 3
                                            },
                                            success: function (res) {
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: 'Thành công!' });
                                                grid.dataSource.read();
                                                $scope.new2view_gop_Grid.dataSource.read();
                                                $scope.ChangeData = true;
                                            }
                                        });
                                    },
                                    Close: function () {
                                        txt.value(dataItem.Quantity);
                                    }
                                })
                            } else {
                                $rootScope.Message({ Msg: "Sản lượng còn lại không đủ. Vui lòng kiểm tra lại!", Type: Common.Message.Type.Alert });
                                txt.value(dataItem.Quantity);
                            }
                        }, function () {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Có lỗi xảy ra. Vui lòng thử lại sau!' });
                            txt.value(dataItem.Quantity);
                        })
                    }
                });
            } else if (val > 0.001 && dataItem.Quantity - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 3
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                grid.dataSource.read();
                                $scope.new2view_gop_Grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.Quantity);
                    }
                })
            }
        }
    }

    $scope.new2view_gop_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.Order_List,
            pageSize: 20,
            readparam: function () {
                return {
                    typeOfOrder: 1,
                    fDate: $scope.NewTripDateRequest.fDate,
                    tDate: $scope.NewTripDateRequest.tDate
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' }
                }
            }
        }),
        pageable: Common.PageSize, height: '99%', groupable: false, sortable: true, columnMenu: false, autoBind: false,
        filterable: { mode: 'row', visible: false }, reorderable: true, selectable: true, resizable: true,
        columns: [
            {
                template: '<form class="cus-form-enter" ng-submit="NewTOMasterEnter_Click($event)"><input kendo-numeric-text-box value="0" data-k-min="0" k-options="txtGroupEnter2View_Options" style="width:100%;text-align:center;" /></form>',
                sortable: false, filterable: false, menu: false, width: '75px', title: 'S.Chuyến'
            },
            {
                title: ' ', width: '35px', attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button btn-merge" ng-show="dataItem.IsSplit" title="Gộp" href="/" ng-click="GroupProduct_Merge($event,dataItem,new2view_gop_Grid)">M</a>' +
                    '<a class="k-button small-button btn-merge-ok" style="display:none;" title="Xác nhận" href="/" ng-click="GroupProduct_Merge_OK($event,dataItem,new2view_gop_Grid)">S</a>' +
                    '<input type="checkbox" style="display:none;" class="chk-select-to-merge" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 100, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', width: '80px', title: 'Loại v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistributorCode', width: 100, title: 'Nhà phân phối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: 100, title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: 80, title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 80, title: 'Tấn',
                template: '#if(Ton>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Ton#" k-options="gopTon_Options" data-k-max="#:Ton#" style="width:100%"/></form>#}else{##:Ton##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: 80, title: 'Khối',
                template: '#if(CBM>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:CBM#" k-options="gopCBM_Options" data-k-max="#:CBM#" style="width:100%"/></form>#}else{##:CBM##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: 80, title: 'S.Lượng',
                template: '#if(Quantity>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Quantity#" k-options="gopQuantity_Options" data-k-max="#:Quantity#" style="width:100%"/></form>#}else{##:Quantity##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETD', width: 120, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: 120, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'TempMin', width: 100, title: 'N.Độ tối thiểu', template: '#=TempMin!=null?TempMin:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TempMax', width: 100, title: 'N.Độ tối đa', template: '#=TempMax!=null?TempMax:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine1', width: 250, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine2', width: 250, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: 150, title: 'Mã điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 150, title: 'Tên điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 150, title: 'Mã điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 150, title: 'Tên điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupSort', width: 200, title: ' ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.new2view_trip_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.DI2View_GroupProduct_List,
            pageSize: 0, group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    fDate: $scope.NewTripDateRequest.fDate, tDate: $scope.NewTripDateRequest.tDate, data: $scope.NewViewDataSelect
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
                    TOCreatedDate: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, autoBind: false,
        reorderable: true, sortable: true, filterable: { mode: 'row' }, selectable: true,
        change: function (e) {
            var grid = this;
            var obj = grid.dataItem(grid.select());
            if (Common.HasValue(obj) && obj.OrderGroupProductID > 0) {
                $timeout(function () {
                    $scope.new2view_gop_Grid.clearSelection();
                    $scope.notification.hide();
                    var flag = false, ton = 0, cbm = 0, qty = 0;
                    Common.Data.Each($scope.new2view_gop_Grid.items(), function (tr) {
                        var item = $scope.new2view_gop_Grid.dataItem(tr);
                        if (Common.HasValue(item) && item.OrderGroupProductID == obj.OrderGroupProductID) {
                            flag = true;
                            ton += item.Ton;
                            cbm += item.CBM;
                            qty += item.Quantity;
                            $(tr).addClass('k-state-selected');
                        }
                    })
                    if (flag) {
                        $timeout(function () {
                            $scope.notification.show("Sản lượng chưa phân chuyến: Số lượng:" + Math.round(qty * 1000) / 1000 + ", Tấn: " + Math.round(ton * 1000) / 1000 + ", Khối: " + Math.round(cbm * 1000) / 1000 + "  ", "info");
                        }, 100)
                    }
                })
            } else {
                $timeout(function () {
                    $scope.new2view_gop_Grid.clearSelection();
                    $scope.notification.hide();
                })
            }
        },
        columns: [
            {
                title: ' ', width: '35px', attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button" ng-show="#=!IsFTL&&OrderGroupProductID>0#" title="Xóa đơn hàng khỏi chuyến" href="/" ng-click="NewTOMasterGroupDelete_Click($event,dataItem,new2view_trip_Grid)"><i class="fa fa-minus"></i></a>',
                filterable: false, sortable: false, groupable: false
            },
            {
                field: 'TOMasterCode', width: '120px', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        if (Common.HasValue(obj)) {
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;

                            return "<a href='#' class='btn2ViewTrip' ng-click='NewTOMasterInfo_Click($event,new2view_trip_Grid,new_trip_info_win)' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'>Chuyến " + obj.TOMasterIndex + "</a>"
                                + "<span class='txtGroup2View' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'> - " + obj.TOVendorCode + " - " + strVeh + " - " + strName + " - " + strTel
                                + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', width: '80px', title: 'Loại v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistributorCode', width: '120px', title: 'Nhà phân phối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: '150px', title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: '100px', title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 80, title: 'Tấn',
                template: '#if(!IsFTL&&Ton>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Ton#" k-options="gopTOTon_Options" style="width:100%"/></form>#}else{##:Ton##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: 80, title: 'Khối',
                template: '#if(!IsFTL&&CBM>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:CBM#" k-options="gopTOCBM_Options" style="width:100%"/></form>#}else{##:CBM##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: 80, title: 'S.Lượng',
                template: '#if(!IsFTL&&Quantity>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Quantity#" k-options="gopTOQuantity_Options" style="width:100%"/></form>#}else{##:Quantity##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TempMin', width: 100, title: 'N.Độ tối thiểu', template: '#=TempMin!=null?TempMin:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TempMax', width: 100, title: 'N.Độ tối đa', template: '#=TempMax!=null?TempMax:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine1', width: 250, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine2', width: 250, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: 150, title: 'Mã điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 150, title: 'Tên điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 150, title: 'Mã điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 150, title: 'Tên điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVehicleNo', width: '120px', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
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

    $scope.txtGroupEnter2View_Options = {
        min: 0, spinners: false, culture: "en-US", decimals: 0, format: '#',
        change: function (e) {
            var txt = this;
            var val = txt.value();
            var tr = $(e.sender.element).closest('tr');
            var grid = $(e.sender.element).closest('.cus-grid.k-grid').data('kendoGrid');
            var dataItem = grid.dataItem(tr);
            if (val > 0) {
                var opsGrid = $scope.new2view_trip_Grid;
                var flag = false, toMasterID = -1;
                Common.Data.Each(opsGrid.tbody.find('.k-grouping-row .txtGroup2View'), function (o) {
                    var obj = $(o).data('item');
                    if (Common.HasValue(obj) && obj.TOMasterIndex == val) {
                        flag = true; toMasterID = obj.TOMasterID;
                    }
                })

                if (!flag || toMasterID < 1) {
                    $rootScope.Message({
                        Msg: "Không tìm thấy chuyến số " + val + "! Bạn có muốn tạo mới?",
                        Type: Common.Message.Type.Confirm,
                        Ok: function () {
                            txt.value(0);
                            $scope.NewTripItem = {
                                ID: -1,
                                Code: "mới tạo",
                                VehicleNo: "",
                                DriverName: "",
                                DriverTel: "",
                                StatusCode: 'Chưa chọn xe',
                                StatusColor: $scope.Color.Error,
                                VehicleID: "",
                                VendorOfVehicleID: -1,
                                Ton: dataItem.Ton,
                                CBM: dataItem.CBM,
                                Status: 1,
                                ETA: dataItem.ETA,
                                ETD: dataItem.ETD,
                                ListOPSGOP: [dataItem.ID],
                                ListOPSGOPName: [],
                                LocationStartID: _OPSApointment_Index.Data.Location.LocationStartID,
                                LocationStartName: _OPSApointment_Index.Data.Location.LocationStartName,
                                LocationEndID: _OPSApointment_Index.Data.Location.LocationEndID,
                                LocationEndName: _OPSApointment_Index.Data.Location.LocationEndName,
                                LocationStartLat: _OPSApointment_Index.Data.Location.LocationStartLat,
                                LocationStartLng: _OPSApointment_Index.Data.Location.LocationStartLng,
                                LocationEndLat: _OPSApointment_Index.Data.Location.LocationEndLat,
                                LocationEndLng: _OPSApointment_Index.Data.Location.LocationEndLng,
                                ListLocation: dataItem.ListLocation
                            }
                            $scope.NewTripDetail = true;
                            if ($scope.NewTripItem.VendorOfVehicleID == null)
                                $scope.NewTripItem.VendorOfVehicleID = -1;
                            $scope.new_trip_info_win.setOptions({ height: 350 });
                            $scope.LoadDataNewTrip(true);
                            $timeout(function () {
                                $scope.new_trip_info_win.center().open();
                            }, 100)
                        },
                        Close: function () {
                            txt.value(0);
                        }
                    });
                }
                else {
                    var txt = this;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Check4Consolidate,
                        data: { mID: toMasterID, gopID: dataItem.ID },
                        success: function (res) {
                            if (res == true) {
                                $rootScope.Message({
                                    Msg: "Xác nhận thêm đơn hàng vào chuyến " + val + "?",
                                    Type: Common.Message.Type.Confirm,
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Group,
                                            data: {
                                                mID: toMasterID, gopID: dataItem.ID, isRemove: false
                                            },
                                            success: function (res) {
                                                $scope.ChangeData = true;
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: 'Thành công!' });
                                                $scope.new2view_gop_Grid.dataSource.read();
                                                $scope.new2view_trip_Grid.dataSource.read();
                                            }
                                        });
                                    },
                                    Close: function () {
                                        txt.value(0);
                                    }
                                })
                            } else {
                                $rootScope.Message({
                                    Msg: "Chuyến " + val + " không cho phép ghép hàng!", Type: Common.Message.Type.Alert
                                })
                            }
                        }
                    });
                }
            }
        }
    }

    $scope.GroupProduct_Cancel = function ($event, item, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận hủy đơn hàng?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSApointment_Index.URL.DI.Cancel,
                    data: {
                        data: [item.ID]
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            })
                            $scope.ResetTrip(true);
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    $scope.GroupProduct_Merge = function ($event, item, grid) {
        $event.preventDefault();

        $($event.target).closest('td').find('.btn-merge').hide();
        $($event.target).closest('td').find('.btn-merge-ok').show();
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            $(tr).find('.btn-split').hide();
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.ID != item.ID) {
                $(tr).find('.btn-merge').hide();
                var chk = $(tr).find('.chk-select-to-merge');
                chk.prop('checked', '');
                chk.show();
            }
        })
    }

    $scope.GroupProduct_Merge_OK = function ($event, item, grid) {
        $event.preventDefault();

        var flag = false;
        var data = [item.ID];
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.ID != item.ID) {
                var chk = $(tr).find('.chk-select-to-merge');
                if (chk.prop('checked')) {
                    data.push(o.ID);
                    flag = true;
                }
            }
        })

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận gom đơn hàng đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.Split_Cancel,
                        data: {
                            orderGopID: item.OrderGroupProductID, data: data
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            })
                            grid.dataSource.read();
                        }
                    });
                }
            })
        } else {
            $($event.target).closest('td').find('.btn-merge').show();
            $($event.target).closest('td').find('.btn-merge-ok').hide();
            Common.Data.Each(grid.items(), function (tr) {
                var o = grid.dataItem(tr);
                if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.ID != item.ID) {
                    var chk = $(tr).find('.chk-select-to-merge');
                    chk.hide();
                    $(tr).find('.btn-merge').show();
                }
            })
        }
    }

    $scope.notificationOptions = {
        appendTo: '#newtripwin', button: true, hideOnClick: true, autoHideAfter: 30000, width: 400
    }

    $scope.NewTOMasterGroupDelete_Click = function ($event, item, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa đơn hàng khỏi chuyến?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_Group,
                    data: { mID: item.TOMasterID, gopID: item.ID, isRemove: true },
                    success: function (res) {
                        $scope.ChangeData = true;
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công!' });
                        $scope.new2view_gop_Grid.dataSource.read();
                        $scope.new2view_trip_Grid.dataSource.read();
                    }
                });
            }
        });
    }

    $scope.NewTOMasterInfo_Click = function ($event, grid, win) {
        $event.preventDefault();

        var obj = $($event.currentTarget).data('item');
        if (Common.HasValue(obj)) {
            Common.Data.Each(grid.tbody.find('.k-grouping-row .btn2ViewTrip'), function (btn) {
                $(btn).css('color', '#5a6877');
            })
            $($event.currentTarget).css('color', 'red');
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSApointment_Index.URL.DI.TripByID,
                data: { masterID: obj.TOMasterID },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $scope.NewTripItem = {
                            ID: res.ID,
                            Code: res.Code,
                            VehicleNo: res.VehicleNo,
                            DriverName: res.DriverName,
                            DriverTel: res.DriverTel,
                            StatusCode: 'Có thể cập nhật',
                            StatusColor: $scope.Color.None,
                            VehicleID: res.VehicleID,
                            VendorOfVehicleID: res.VendorOfVehicleID,
                            Ton: res.TotalTon,
                            CBM: res.TotalCBM,
                            Status: 1,
                            ETA: Common.Date.FromJson(res.ETA),
                            ETD: Common.Date.FromJson(res.ETD),
                            ListOPSGOP: [],
                            ListOPSGOPName: [],
                            LocationStartID: res.LocationStartID,
                            LocationStartName: res.LocationStartName,
                            LocationEndID: res.LocationEndID,
                            LocationEndName: res.LocationEndName,
                            LocationStartLat: res.LocationStartLat,
                            LocationStartLng: res.LocationStartLng,
                            LocationEndLat: res.LocationEndLat,
                            LocationEndLng: res.LocationEndLng,
                            ListLocation: res.ListLocation
                        }
                        if ($scope.NewTripItem.VendorOfVehicleID == null) {
                            $scope.NewTripItem.VendorOfVehicleID = -1;
                        }
                        $scope.LoadDataNewTrip();
                        $timeout(function () {
                            $scope.NewTripDetail = true;
                            $scope.CheckNewTrip();
                            win.setOptions({ height: 275 });
                            win.center().open();
                        }, 300)
                    }
                }
            });
        }
    }

    $scope.cboNewTripVendor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { ID: { type: 'number' }, CustomerName: { type: 'string' } } }
        })
    }

    $scope.cboNewTripVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'Regno', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, Regno: { type: 'string' } } }
        })
    }

    $scope.atcNewTripDriverNameOptions = {
        dataSource: Common.DataSource.Local({ data: [] }),
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Họ tên", dataTextField: "DriverName",
        change: function (e) {
            var cbo = this;
            $timeout(function () {
                var obj = cbo.dataItem(cbo.select());
                if (Common.HasValue(obj)) {
                    $scope.NewTripItem.DriverTel = obj.DriverTel;
                }
            }, 10)
        }
    }

    $scope.$watch('NewTripItem.ETD', function () {
        $scope.CheckNewTrip();
    });
    $scope.$watch('NewTripItem.ETA', function () {
        $scope.CheckNewTrip();
    });
    $scope.$watch('NewTripItem.VehicleID', function () {
        $scope.CheckNewTrip();
    });
    $scope.$watch('NewTripItem.VendorOfVehicleID', function (nval, oval) {
        Common.Log("NewTripItem.VendorOfVehicleID")
        if ((nval == -1 || nval == null) && Common.HasValue($scope.NewTripItem) && $scope.NewTripItem.ID < 1) {
            $scope.new_trip_info_win.setOptions({ height: 350 })
        } else {
            $scope.new_trip_info_win.setOptions({ height: 275 })
        }
        if ($scope.NewTripDetail) {
            $scope.NewTripItem.DriverTel = '';
            $scope.NewTripItem.DriverName = '';
            $scope.LoadDataNewTrip(true);
        }
    });

    $scope.CheckNewTrip = function () {
        Common.Log("CheckNewTrip")
        if ($scope.NewTripDetail && $scope.NewTripItem != null && $scope.NewTripItem.Status == 1) {
            $scope.NewTripItem.StatusCode = "";
            $scope.NewTripItem.StatusColor = $scope.Color.None;
            //Trường hợp xe vendor không cần kiểm tra.
            if ($scope.NewTripItem.VendorOfVehicleID == -1 && $scope.NewTripItem.VehicleID > 0 && $scope.NewTripItem.ETD != null && $scope.NewTripItem.ETA != null) {
                Common.Log('Trip checing...');

                if ($scope.NewTripItem.ETD >= $scope.NewTripItem.ETA) {
                    $scope.NewTripItem.StatusCode = "Tg không hợp lệ.";
                    $scope.NewTripItem.StatusColor = $scope.Color.Error;
                }
                else {
                    $scope.NewTripItem.IsCheching = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.CheckVehicleAvailable,
                        data: {
                            vehicleID: $scope.NewTripItem.VehicleID,
                            masterID: $scope.NewTripItem.ID,
                            ETD: $scope.NewTripItem.ETD,
                            ETA: $scope.NewTripItem.ETA,
                            Ton: $scope.NewTripItem.TotalTon || 0
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.NewTripItem.IsCheching = false;
                                if (res.IsOverWeight) {
                                    $scope.NewTripItem.StatusCode = "Quá trọng tải";
                                    $scope.NewTripItem.StatusColor = $scope.Color.Error;
                                } else {
                                    if (res.IsVehicleAvailable) {
                                        $scope.NewTripItem.DriverTel = res.DriverTel;
                                        $scope.NewTripItem.DriverName = res.DriverName;
                                        $scope.NewTripItem.StatusCode = "Có thể cập nhật";
                                        $scope.NewTripItem.StatusColor = $scope.Color.Success;
                                    } else {
                                        $scope.NewTripItem.StatusCode = "Xe bận.";
                                        $scope.NewTripItem.StatusColor = $scope.Color.Error;
                                    }
                                }
                            })
                        }
                    })
                }
            }
        }
    }

    $scope.LoadDataNewTrip = function (isNew) {
        Common.Log("LoadDataNewTrip")
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.VehicleVendor_List,
            data: {
                vendorID: $scope.NewTripItem.VendorOfVehicleID, request: '',
            },
            success: function (res) {
                $scope.cboNewTripVehicle_Options.dataSource.data(res.Data);
                $timeout(function () {
                    if (isNew) {
                        if (Common.HasValue(res.Data[0]))
                            $scope.NewTripItem.VehicleID = res.Data[0].ID;
                        else
                            $scope.NewTripItem.VehicleID = null;
                    }
                    $rootScope.IsLoading = false;
                }, 100)
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.DriverVendor_List,
            data: {
                vendorID: $scope.NewTripItem.VendorOfVehicleID
            },
            success: function (res) {
                var data = [];
                $.each(res, function (i, v) {
                    data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
                });
                $scope.atcNewTripDriverNameOptions.dataSource.data(data);
            }
        });
    }

    $scope.NewTrip_Update_OK_Click = function ($event, win) {
        $event.preventDefault();

        var flag = true;
        if ($scope.NewTripItem.ETA == null || $scope.NewTripItem.ETD == null) {
            flag = false;
            $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        }

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận lưu?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    if ($scope.NewTripItem.ID < 1) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.Save,
                            data: {
                                item: $scope.NewTripItem
                            },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    if ($scope.NewTripItem.ID < 1 && $scope.NewViewDataSelect.length > 0) {
                                        $scope.NewViewDataSelect.push(res);
                                    }
                                    $scope.new2view_gop_Grid.dataSource.read();
                                    $scope.new2view_trip_Grid.dataSource.read();
                                    $scope.NewTripDetail = false;
                                    win.close();
                                }, function () {
                                    $rootScope.IsLoading = false;
                                })
                            }
                        })
                    }
                    else {
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSApointment_Index.URL.DI.DI2View_Master_Update,
                            data: {
                                item: $scope.NewTripItem
                            },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    if ($scope.NewTripViewAction == 1) {
                                        if ($scope.NewTripItem.ID < 1 && $scope.NewViewDataSelect.length > 0) {
                                            $scope.NewViewDataSelect.push(res);
                                        }
                                        $scope.new2view_gop_Grid.dataSource.read();
                                        $scope.new2view_trip_Grid.dataSource.read();
                                    } else {
                                        $scope.LoadNewTimeLineData();
                                    }
                                    $scope.NewTripDetail = false;
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

    $scope.NewTrip_Delete_OK_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa chuyến?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSApointment_Index.URL.DI.Delete,
                    data: {
                        data: [$scope.NewTripItem.ID]
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $scope.ChangeData = true;
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!' });
                            if ($scope.NewTripViewAction == 1) {
                                $scope.new2view_gop_Grid.dataSource.read();
                                $scope.new2view_trip_Grid.dataSource.read();
                            } else {
                                $scope.LoadNewTimeLineData();
                            }
                            $scope.NewTripDetail = false;
                            win.close();
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    //Bổ sung chuyến.
    $scope.NewTripViewDataSelect_Click = function ($event, grid, win) {
        $event.preventDefault();

        win.center().open();
        grid.dataSource.read();
    }

    $scope.new2view_trip_select_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.DI2View_GroupProduct_List,
            pageSize: 0, group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    fDate: $scope.NewTripDateRequest.fDate, tDate: $scope.NewTripDateRequest.tDate, data: []
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
                    TOCreatedDate: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, autoBind: false,
        reorderable: true, sortable: true, filterable: { mode: 'row' }, selectable: true,
        dataBound: function () {
            var grid = this;
            grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
                var obj = $(this).data('item');
                if (Common.HasValue(obj)) {
                    if ($scope.NewViewDataSelect.indexOf(obj.TOMasterID) > -1)
                        $(this).prop('checked', true);
                }
            })
        },
        columns: [
            {
                field: 'Check', title: ' ', width: '35px',
                headerTemplate: '<input class="chkGroupChooseAll" type="checkbox" ng-click="onGroupChooseAll($event,new2view_trip_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }, filterable: false, sortable: false, groupable: false
            },
            {
                field: 'TOMasterCode', width: '120px', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        if (Common.HasValue(obj)) {
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            return "<input style='position:relative;top:2px;' ng-click='onGroupChoose($event,new2view_trip_select_Grid)' "
                                + "type='checkbox' class='chkGroupChoose' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'></input>"
                                + "<span>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + strVeh + " - " + strName + " - " + strTel
                                + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + " </span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
            },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistributorCode', width: '120px', title: 'Nhà phân phối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: '150px', title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: '100px', title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: 80, title: 'Tấn', sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: 80, title: 'Khối', sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: 80, title: 'S.Lượng', sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TempMin', width: 100, title: 'N.Độ tối thiểu', template: '#=TempMin!=null?TempMin:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TempMax', width: 100, title: 'N.Độ tối đa', template: '#=TempMax!=null?TempMax:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine1', width: 250, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine2', width: 250, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: 150, title: 'Mã điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 150, title: 'Tên điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 150, title: 'Mã điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 150, title: 'Tên điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVehicleNo', width: '120px', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
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

    $scope.NewTripSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
            var obj = $(this).data('item');
            if (Common.HasValue(obj) && $(this).prop('checked')) {
                data.push(obj.TOMasterID);
            }
        })
        if (data.sort().toString() != $scope.NewViewDataSelect.sort().toString()) {
            $scope.NewViewDataSelect = data;
            $scope.new2view_trip_Grid.dataSource.read();
        }
        win.close();
    }

    $scope.TripLocation_Change = function ($event, type, win) {
        $event.preventDefault();

        $scope.LocationType = type;
        win.center().open();
        $timeout(function () {
            $scope.location_Grid.resize();
        }, 100)
    }

    $scope.location_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.Location_List,
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, autoBind: false,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: true,
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

    $scope.Location_Select = function ($event, item, win) {
        $event.preventDefault();

        if ($scope.IsShowNewTrip) {
            switch ($scope.LocationType) {
                case 1: //Start
                    $scope.NewTripItem.LocationStartID = item.ID;
                    $scope.NewTripItem.LocationStartName = item.Location;
                    $scope.NewTripItem.LocationStartLat = item.Lat;
                    $scope.NewTripItem.LocationStartLng = item.Lng;
                    break;
                case 2: //End
                    $scope.NewTripItem.LocationEndID = item.ID;
                    $scope.NewTripItem.LocationEndName = item.Location;
                    $scope.NewTripItem.LocationEndLat = item.Lat;
                    $scope.NewTripItem.LocationEndLng = item.Lng;
                    break;
            }
        }
        win.close();
    }

    $scope.atcLocationOptions = {
        dataSource: Common.DataSource.Local({ data: [] }),
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Tìm theo mã | tên | địa chỉ", dataTextField: "Text",
        change: function (e) {
            var cbo = this;
            $timeout(function () {
                try {
                    var obj = cbo.dataItem(cbo.select());
                    if (Common.HasValue(obj)) {
                        openMapV2.Center(obj.Lat, obj.Lng, 18);
                    }
                } catch (e) { }
            }, 10)
        }
    }

    $scope.LocationViewOnMap_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
        $timeout(function () {
            $scope.IsHub = false;
            if ($scope.popupMap == null) {
                $rootScope.IsLoading = true;
                $scope.popupMap = openMapV2({
                    Zoom: 14,
                    Element: 'popupmap',
                    Tooltip_Show: true,
                    Tooltip_Element: 'popupmap_tooltip',
                    InfoWin_Show: true,
                    InfoWin_Element: 'popupmap_info_win',
                    ClickMarker: function (i, o) {
                        $scope.LocationItem = i;
                    }
                });

                var fData = [];
                fData.push(Common.Request.FilterParamWithAnd('Lat', Common.Request.FilterType.GreaterThan, 0));
                fData.push(Common.Request.FilterParamWithAnd('Lng', Common.Request.FilterType.GreaterThan, 0));
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSApointment_Index.URL.DI.Location_List,
                    data: {
                        request: Common.Request.Create({
                            Sorts: [], Filters: fData
                        })
                    },
                    success: function (res) {
                        var dataSuggestions = [];
                        var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Location), 1, "", '#73C95F', true);
                        Common.Data.Each(res.Data, function (o) {
                            dataSuggestions.push({
                                Value: o.ID, Text: o.Code,
                                Lat: o.Lat, Lng: o.Lng
                            })
                            dataSuggestions.push({
                                Value: o.ID, Text: o.Location,
                                Lat: o.Lat, Lng: o.Lng
                            })
                            dataSuggestions.push({
                                Value: o.ID, Text: o.Address,
                                Lat: o.Lat, Lng: o.Lng
                            })
                            if (o.Lat > 0 && o.Lng > 0)
                                openMapV2.NewMarker(o.Lat, o.Lng, "" + o.ID, o.Location, icon, o, "VectorMarkerLocation");
                        })
                        $scope.atcLocationOptions.dataSource.data(dataSuggestions);
                        $rootScope.IsLoading = false;
                    }
                });
            } else {
                openMapV2.Active($scope.popupMap);
            }
        }, 100);
    }

    $scope.OnMap_LocationChoose_Click = function ($event, winmap, win) {
        $event.preventDefault();

        var item = $scope.LocationItem;

        if ($scope.IsShowNewTrip) {
            switch ($scope.LocationType) {
                case 1: //Start
                    $scope.NewTripItem.LocationStartID = item.ID;
                    $scope.NewTripItem.LocationStartName = item.Location;
                    $scope.NewTripItem.LocationStartLat = item.Lat;
                    $scope.NewTripItem.LocationStartLng = item.Lng;
                    break;
                case 2: //End
                    $scope.NewTripItem.LocationEndID = item.ID;
                    $scope.NewTripItem.LocationEndName = item.Location;
                    $scope.NewTripItem.LocationEndLat = item.Lat;
                    $scope.NewTripItem.LocationEndLng = item.Lng;
                    break;
            }
        }

        //Active indexMap
        openMapV2.Active($scope.indexMap);
        $scope.LocationItem = null;
        openMapV2.Close();
        winmap.close();
        win.close();
    }

    //#endregion

    //#region NewTimeLine
    $scope.IsNewTimeLineBound = false;
    $scope.NewTimeLineToMonAvailable = false;
    $scope.NewTimeLineToOpsAvailable = false;

    $scope.NewTripTimeLineEdit = {};
    $scope.NewTimeLineTripItem = {};
    $scope.NewTimeLineEvent_HasClick = false;
    $scope.NewDITimeLineVehicleData = [];
    $scope.NewDITimeLineVehicleDataTemp = [];

    $scope.new_timeline_TripOptions = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 25, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
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
            }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { from: "ID", type: "number" },
                        title: { from: "Code" },
                        start: { type: "date", from: "ETD" },
                        end: { type: "date", from: "ETA" },
                        field: { from: "VehicleID" }
                    }
                }
            }
        },
        groupHeaderTemplate: kendo.template("<input style='position:relative;top:2px;' type='checkbox' data-uid='#=value#' class='chk_vehicle_timeline' /><strong>#=text#</strong>"),
        eventTemplate: $("#new-timeline-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            $scope.TimeLineToMonAvailable = false;
            $scope.TimeLineToOpsAvailable = false;

            $timeout(function () {
                if ($scope.IsNewTimeLineBound == false && $scope.IsShowNewTrip == true) {
                    $scope.IsNewTimeLineBound = true;
                    scheduler.view(scheduler.view().name);
                    //scheduler.element.find('.k-nav-today a').trigger('click');
                } else if ($scope.IsNewTimeLineBound == true && $scope.IsShowNewTrip == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                if (i.Status == 1) {
                                    $(o).addClass('approved');
                                } else {
                                    $(o).addClass('tendered');
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

                    var thGroup = angular.element('.timeline-trip .k-scheduler-layout>tbody>tr>td>.k-scheduler-times .k-scheduler-table:eq(0) tr:eq(0)>th');
                    thGroup.empty();
                    thGroup.append($compile("<a href='/' style='width:100%;' title='Nhấn để chọn xe muốn hiển thị' class='k-button' ng-click='NewTimeLineVehicleSelect_Click($event,new_timeline_vehicle_select_Grid,new_timeline_vehicle_select_win)'>Chọn xe</a>")($scope));
                    var thCombobox = angular.element('.timeline-trip .k-scheduler-layout>tbody>tr>td>.k-scheduler-times .k-scheduler-table:eq(0) tr:eq(1)>th');
                    thCombobox.empty();
                    thCombobox.append($compile("<input class='cbo-new-timeline-vehicle-select' style='width:100%;text-align:center;font-weight: bold;color:#46bdfc;' focus-k-complete />")($scope));
                    $scope.$apply();

                    var dataVeh = [];
                    Common.Data.Each(scheduler.resources[0].dataSource.data(), function (o) {
                        dataVeh.push({ ID: o.value, VehicleNo: o.text });
                    })
                    thCombobox.find('.cbo-new-timeline-vehicle-select').kendoAutoComplete({
                        dataSource: Common.DataSource.Local({ data: dataVeh, model: { id: 'ID', fields: { ID: { type: 'number' }, VehicleNo: { type: 'string' } } } }),
                        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Số xe", dataTextField: "VehicleNo",
                        change: function (e) {
                            var cbo = this;
                            $timeout(function () {
                                var tbody1 = $('.timeline-trip .k-scheduler-layout > tbody > tr:eq(1) > td:eq(0) div.k-scheduler-times');
                                var tbody2 = $('.timeline-trip .k-scheduler-layout > tbody > tr:eq(1) > td:eq(1) div.k-scheduler-content');
                                var item = cbo.dataItem(cbo.select());
                                if (Common.HasValue(item)) {
                                    var valScrollTop = 0;
                                    scheduler.element.find('.chk_vehicle_timeline').each(function (chk) {
                                        var tr = $(this).closest('tr');
                                        tr.css('background', '#ffffff');
                                        if ($(this).data('uid') == item.ID) {
                                            valScrollTop = $(this).closest('tr').offset().top;
                                            tr.css('background', '#c5c5c5');
                                        }
                                    })
                                    try {
                                        var valScrollTo = valScrollTop - tbody1.offset().top + tbody1.scrollTop();
                                        tbody1.animate({ scrollTop: valScrollTo });
                                        tbody2.animate({ scrollTop: valScrollTo });
                                    } catch (e) { }
                                } else {
                                    $rootScope.Message({ Msg: "Không tìm thấy dữ liệu số xe." });
                                }
                            }, 1)
                        }
                    })

                    $timeout(function () {
                        $scope.NewViewTripLoading = false;
                    }, 1000)
                }
            }, 10)
        },
        resources: [
            {
                field: "field",
                name: "Group",
                dataSource: [{
                    value: '-1', text: 'Data Empty'
                }],
                multiple: true
            }
        ],
        moveStart: function (e) {
            if (e.event.Status == 2) {
                e.preventDefault();
            } else {
                $scope.NewTripTimeLineEdit = $.extend(true, {}, e.event);
            }
        },
        resizeStart: function (e) {
            if (e.event.Status == 2) {
                e.preventDefault();
            } else {
                $scope.NewTripTimeLineEdit = $.extend(true, {}, e.event);
            }
        },
        save: function (e) {
            e.preventDefault();
            var scheduler = this, obj = $.extend(true, {}, e.event);
            $rootScope.Message({
                Msg: "Xác nhận lưu thay đổi?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.DI2View_Master_Update_TimeLine,
                        data: { mID: obj.id, vehicleID: parseInt(obj.field), ETD: obj.start, ETA: obj.end },
                        success: function (res) {
                            Common.Services.Error(res, function () {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' })
                                scheduler.refresh();
                            }, function () {
                                var idx = 0;
                                $.each(scheduler.dataSource.data(), function (i, o) {
                                    if (o.id == obj.id) {
                                        idx = i;
                                    }
                                })
                                var item = scheduler.dataSource.at(idx);
                                if (Common.HasValue(item)) {
                                    item.end = $scope.NewTripTimeLineEdit.end;
                                    item.start = $scope.NewTripTimeLineEdit.start;
                                    item.field = $scope.NewTripTimeLineEdit.field;
                                }
                                scheduler.refresh();
                                $rootScope.IsLoading = false;
                            })
                        }
                    });
                },
                Close: function () {
                    $rootScope.IsLoading = true;
                    var idx = 0;
                    $.each(scheduler.dataSource.data(), function (i, o) {
                        if (o.id == obj.id) {
                            idx = i;
                        }
                    })
                    var item = scheduler.dataSource.at(idx);
                    if (Common.HasValue(item)) {
                        item.end = $scope.NewTripTimeLineEdit.end;
                        item.start = $scope.NewTripTimeLineEdit.start;
                        item.field = $scope.NewTripTimeLineEdit.field;
                    }
                    scheduler.refresh();
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.cboNewTimeLineVehicleSelect_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'VehicleNo', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, VehicleNo: { type: 'string' } } }
        })
    }

    $scope.new_timeline_vehicle_select_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.VehicleVendor_List,
            readparam: function () { return { vendorID: -1 } },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            }
        }),
        pageable: false, height: '99%', groupable: false, sortable: true, columnMenu: false, resizable: true, autoBind: false,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: true,
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && $scope.NewDITimeLineVehicleData.indexOf(item.ID) > -1) {
                    item.IsChoose = true;
                    $(tr).find('td input.chkChoose').prop('checked', true);
                    if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
                }
            })
        },
        columns: [
            {
                title: ' ', width: '50px',
                headerTemplate: '<input  type="checkbox" ng-click="NewTimeLineVehicleSelect_ChooseAll($event,new_timeline_vehicle_select_Grid)" />',
                template: '<input class="chkChoose" type="checkbox" #=IsChoose?"checked=checked":""# ng-click="NewTimeLineVehicleSelect_Choose($event,new_timeline_vehicle_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }, filterable: false, sortable: false, groupable: false
            },
            { field: 'Regno', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxWeight', width: 150, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } }
        ]
    }

    $scope.NewTimeLineVehicleSelect_ChooseAll = function ($event, grid) {
        $scope.NewDITimeLineVehicleData = [];
        if ($event.target.checked == true) {
            grid.items().each(function () {
                var tr = this, item = grid.dataItem(tr);
                $scope.NewDITimeLineVehicleData.push(item.ID);
                if (item.IsChoose != true) {
                    $(tr).find('td input.chkChoose').prop('checked', true);
                    item.IsChoose = true;
                    if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
                }
            });
        }
        else {
            grid.items().each(function () {
                var tr = this, item = grid.dataItem(tr);
                if (item.IsChoose == true) {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                    if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
                }
            });
        }
    }

    $scope.NewTimeLineVehicleSelect_Choose = function ($event, grid) {
        var tr = $($event.target).closest('tr'), item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
            $scope.NewDITimeLineVehicleData.push(item.ID);
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
            if ($scope.NewDITimeLineVehicleData.indexOf(item.ID) > -1) {
                $scope.NewDITimeLineVehicleData.splice($scope.NewDITimeLineVehicleData.indexOf(item.ID), 1)
            }
        }
    }

    $scope.NewTimeLineVehicleSelect_Click = function ($event, grid, win) {
        $event.preventDefault();
        grid.dataSource.read();
        win.center().open();
        $scope.NewDITimeLineVehicleDataTemp = $.extend(true, [], $scope.NewDITimeLineVehicleData);
    }

    $scope.NewTimeLineVehicleSelect_OK_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.NewDITimeLineVehicleData.sort().toString() != $scope.NewDITimeLineVehicleDataTemp.sort().toString()) {
            $scope.NewDITimeLineVehicleDataTemp = $.extend(true, [], $scope.NewDITimeLineVehicleData);
            $scope.LoadNewTimeLineData();
        }
        win.close();
    }

    $scope.LoadNewTimeLineData = function () {
        $scope.IsNewTimeLineBound = false;
        $scope.NewViewTripLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSApointment_Index.URL.DI.Schedule_Data,
            data: { data: $scope.NewDITimeLineVehicleData },
            success: function (res) {
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: res.ListTrip,
                    schema: {
                        model: {
                            id: "id",
                            fields: {
                                id: { from: "ID", type: "number" },
                                title: { from: "Code" },
                                start: { type: "date", from: "ETD" },
                                end: { type: "date", from: "ETA" },
                                field: { from: "VehicleID" }
                            }
                        }
                    }
                });
                $scope.new_timeline_Trip.setDataSource(dataSource);

                var data = [];
                Common.Data.Each(res.ListVehicle, function (o) {
                    var obj = {
                        value: o.ID,
                        text: o.Regno,
                        IsChoose: false
                    }
                    data.push(obj);
                })
                if (data.length == 0) {
                    data.push({
                        value: -1,
                        text: "DL trống"
                    });
                }
                $scope.new_timeline_Trip.resources[0].dataSource.data(data);
            }
        })
    }

    $scope.NewTimeLineEvent_Click = function ($event, uid, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineEvent_HasClick) {
            $rootScope.Message({ Msg: "Thao tác quá nhanh.", NotifyType: Common.Message.NotifyType.WARNING });
            return;
        }
        $scope.NewTimeLineEvent_HasClick = true;

        var obj = null;
        Common.Data.Each($scope.new_timeline_Trip.dataSource.data(), function (i) {
            if (i.id == uid) {
                obj = $.extend(true, {}, i);
            }
        })
        if (Common.HasValue(obj)) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSApointment_Index.URL.DI.TripByID,
                data: { masterID: uid },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $scope.NewTripItem = {
                            ID: res.ID,
                            Code: res.Code,
                            VehicleNo: res.VehicleNo,
                            DriverName: res.DriverName,
                            DriverTel: res.DriverTel,
                            StatusCode: 'Có thể cập nhật',
                            StatusColor: $scope.Color.None,
                            VehicleID: res.VehicleID,
                            VendorOfVehicleID: res.VendorOfVehicleID,
                            Ton: res.TotalTon,
                            CBM: res.TotalCBM,
                            Status: obj.Status,
                            ETA: Common.Date.FromJson(res.ETA),
                            ETD: Common.Date.FromJson(res.ETD),
                            ListOPSGOP: [],
                            ListOPSGOPName: [],
                            LocationStartID: res.LocationStartID,
                            LocationStartName: res.LocationStartName,
                            LocationEndID: res.LocationEndID,
                            LocationEndName: res.LocationEndName,
                            LocationStartLat: res.LocationStartLat,
                            LocationStartLng: res.LocationStartLng,
                            LocationEndLat: res.LocationEndLat,
                            LocationEndLng: res.LocationEndLng,
                            ListLocation: res.ListLocation
                        }
                        if ($scope.NewTripItem.VendorOfVehicleID == null) {
                            $scope.NewTripItem.VendorOfVehicleID = -1;
                        }
                        $scope.LoadDataNewTrip();
                        $timeout(function () {
                            $scope.NewTripDetail = true;
                            $scope.CheckNewTrip();
                            win.setOptions({ height: 275 });
                            win.center().open();
                        }, 300)
                    }
                }
            });
        }
        $timeout(function () {
            $scope.NewTimeLineEvent_HasClick = false;
        }, 2000)
    }

    $scope.NewTimeLine_GroupDelete_Click = function ($event, scheduler) {
        $event.preventDefault();

        var temp = [];
        var data = [];
        var dataCode = [];
        Common.Data.Each(scheduler.resources[0].dataSource.data(), function (o) {
            if (o.IsChoose == true) {
                temp[o.value] = true;
            }
        })

        var view = scheduler.view();
        var fDate = view.startDate();
        var tDate = view.endDate();

        Common.Data.Each(scheduler.dataSource.data(), function (o) {
            if (temp[o.field] && ((o.start > fDate && o.start < tDate) || (o.end > fDate && o.end < tDate))) {
                if (o.Status == 1) {
                    data.push(o.id);
                    dataCode.push(o.title);
                }
            }
        })
        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận xóa chuyến: " + dataCode.join(', ') + "?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.Delete,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.LoadNewTimeLineData();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.NewTimeLine_GroupToMON_Click = function ($event, scheduler) {
        $event.preventDefault();

        var temp = [];
        var data = [];
        var dataCode = [];
        var dataCodeEmpty = [];
        Common.Data.Each(scheduler.resources[0].dataSource.data(), function (o) {
            if (o.IsChoose == true) {
                temp[o.value] = true;
            }
        })

        var view = scheduler.view();
        var fDate = view.startDate();
        var tDate = view.endDate();

        Common.Data.Each(scheduler.dataSource.data(), function (o) {
            if (temp[o.field] && ((o.start > fDate && o.start < tDate) || (o.end > fDate && o.end < tDate))) {
                if (o.Status == 1) {
                    data.push(o.id);
                    dataCode.push(o.title);
                    if (o.VendorOfVehicleID == null && (o.DriverName == null || o.DriverName == '')) {
                        dataCodeEmpty.push(o.title);
                    }
                }
            }
        })

        if (dataCodeEmpty.length > 0) {
            $rootScope.Message({
                Msg: "Vui lòng chọn tài xế chuyến: " + dataCodeEmpty.join(', ') + "?",
                Type: Common.Message.Type.Alert,
            });
            return;
        }
        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận duyệt chuyến: " + dataCode.join(', ') + "?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.ToMon,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.LoadNewTimeLineData();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.NewTimeLine_GroupToOPS_Click = function ($event, scheduler) {
        $event.preventDefault();

        var temp = [];
        var data = [];
        var dataCode = [];
        Common.Data.Each(scheduler.resources[0].dataSource.data(), function (o) {
            if (o.IsChoose == true) {
                temp[o.value] = true;
            }
        })

        var view = scheduler.view();
        var fDate = view.startDate();
        var tDate = view.endDate();

        Common.Data.Each(scheduler.dataSource.data(), function (o) {
            if (temp[o.field] && ((o.start > fDate && o.start < tDate) || (o.end > fDate && o.end < tDate))) {
                if (o.Status == 2) {
                    data.push(o.id);
                    dataCode.push(o.title);
                }
            }
        })
        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận hủy duyệt chuyến: " + dataCode.join(', ') + "?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSApointment_Index.URL.DI.ToOPS,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.LoadNewTimeLineData();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            });
        }
    }

    //#endregion

    //#region Button
    $scope.NewTripViewStatus_Click = function ($event) {
        $event.preventDefault();

        if ($scope.NewTripViewAction == 0) {
            try {
                var flag = true;
                var value = $($event.currentTarget).data('tabindex');
                if (value == 1) {
                    $scope.NewViewTripApproved = !$scope.NewViewTripApproved;
                    if ($scope.NewViewTripApproved == false && $scope.NewViewTripTendered == false) {
                        $scope.NewViewTripApproved = !$scope.NewViewTripApproved;
                        flag = false;
                    }
                } else {
                    $scope.NewViewTripTendered = !$scope.NewViewTripTendered;
                    if ($scope.NewViewTripApproved == false && $scope.NewViewTripTendered == false) {
                        $scope.NewViewTripTendered = !$scope.NewViewTripTendered;
                        flag = false;
                    }
                }
                if (flag) {
                    $scope.new_trip_Grid.dataSource.read();
                }
            }
            catch (e) { }
        } else if ($scope.NewTripViewAction == 2) {

        }
    }

    $scope.NewTripViewDate_Click = function ($event) {
        $event.preventDefault();

        if ($event.currentTarget == $event.target)
            $scope.ShowNewTripDate = !$scope.ShowNewTripDate;

    }

    $scope.NewTripViewDate_OK_Click = function ($event) {
        $event.preventDefault();

        $scope.NewViewTripDate = true;
        $scope.ShowNewTripDate = false;
        if ($scope.NewTripViewAction == 0) {
            $scope.new_trip_Grid.dataSource.read();
        } else if ($scope.NewTripViewAction == 1) {
            $scope.new2view_gop_Grid.dataSource.read();
            $scope.new2view_trip_Grid.dataSource.read();
        }
    }

    $scope.NewTripViewDate_Cancel_Click = function ($event) {
        $event.preventDefault();

        $scope.NewViewTripDate = false;
        $scope.ShowNewTripDate = false;
        $scope.TripDateRequest = { fDate: null, tDate: null };
        if ($scope.NewTripViewAction == 0) {
            $scope.new_trip_Grid.dataSource.read();
        } else if ($scope.NewTripViewAction == 1) {
            $scope.new2view_gop_Grid.dataSource.read();
            $scope.new2view_trip_Grid.dataSource.read();
        }
    }

    $scope.NewTripView2View_Click = function ($event) {
        $event.preventDefault();

        $scope.notification.hide();
        $scope.NewViewTripLoading = true;
        if ($scope.NewTripViewAction != 1) {
            if ($scope.NewTripViewAction == 0) {
                var data = [];
                var grid = $scope.new_trip_Grid;
                Common.Data.Each(grid.tbody.find('.k-grouping-row .chkGroupChoose'), function (o) {
                    var val = $(o).prop('checked'), obj = $(o).data('item');
                    if (Common.HasValue(obj) && val) {
                        data.push(obj.TOMasterID);
                    }
                })
                $scope.NewTripViewAction = 1;
                if (data.sort().toString() != $scope.NewViewDataSelect.sort().toString()) {
                    $scope.NewViewDataSelect = data;
                    $scope.new2view_gop_Grid.dataSource.read();
                    $scope.new2view_trip_Grid.dataSource.read();
                } else {
                    $scope.new2view_trip_Grid.dataSource.read();
                }
            }
            else {
                $scope.NewViewDataSelect = [];
                $scope.NewTripViewAction = 1;
                $scope.new2view_gop_Grid.dataSource.read();
                $scope.new2view_trip_Grid.dataSource.read();
            }
        }
        else {
            $scope.NewTripViewAction = 0;
            $scope.new_trip_Grid.dataSource.read();
        }
        $timeout(function () {
            $scope.tripSplitter.resize();
            $scope.NewViewTripLoading = false;
        }, 2000)
    }

    $scope.NewTripViewTimeLine_Click = function ($event) {
        $event.preventDefault();

        $scope.notification.hide();
        $scope.NewViewTripLoading = true;
        if ($scope.NewTripViewAction != 2) {
            $scope.NewTripViewAction = 2;
            $scope.LoadNewTimeLineData();
        }
        else {
            $scope.NewTripViewAction = 0;
            $scope.new_trip_Grid.dataSource.read();
            $timeout(function () {
                $scope.NewViewTripLoading = false;
            }, 2000)
        }
    }

    $scope.ViewMax_Click = function ($event, win) {
        $event.preventDefault();
        $scope.notification.hide();
        $timeout(function () {
            win.toggleMaximization();
        }, 100)
    }
    //#endregion

    //#endregion

    $scope.Close_Click = function ($event, win, code) {
        $event.preventDefault();

        try {
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
                case 'TimeLine':
                    $scope.TripActived = false;
                    $scope.IsShowTimeLine = false;
                    $scope.IsShowTimeLineTrip = false;
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
                case 'DITimeLineVehicleSelect':
                    $scope.NewDITimeLineVehicleData = $.extend(true, [], $scope.NewDITimeLineVehicleDataTemp);
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

    $scope.On_Close = function (code) {
        switch (code) {
            case 'NewTO':
                $scope.notification.hide();
                $scope.IsShowNewTrip = false;
                break;
            case 'NewTODetail':
                $scope.NewTripDetail = false;
                break;
            case 'TimeLineTODetail':
                $scope.TimeLineTripDetail = false;
                break;
            case 'DITimeLineVehicleSelect':
                $scope.NewDITimeLineVehicleData = $.extend(true, [], $scope.NewDITimeLineVehicleDataTemp);
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

    $rootScope.IsLoading = false;
    $scope.InitComplete = true;
}]);