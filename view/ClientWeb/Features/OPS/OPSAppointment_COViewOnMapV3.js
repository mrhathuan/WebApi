/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_COViewOnMapV3 = {
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
        TimeLine_ToEvent_Time_Offer: 'OPSCO_MAP_Schedule_TOMaster_Time_Offer'
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
        },
        VendorList: [],
        VehicleInfo: []
    },
}

angular.module('myapp').controller('OPSAppointment_COViewOnMapV3Ctrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COViewOnMapV3Ctrl');
    $rootScope.IsLoading = false;

    $scope.TripActived = false;
    $scope.HasChoose = false;
    $scope.ViewOrder = true; //Xem theo ĐH/Chặng
    $scope.ViewOrderDate = false; //Lọc theo ngày
    $scope.ShowOrderDate = false; //Chọn ngày
    $scope.ViewTractor = true; //Xem theo đầu kéo/romooc
    $scope.IsTractor = false; //Show Tractor InfoWin.
    $scope.TripDetail = false; //Hiển thông tin chuyến khi tạo mới
    $scope.IsFullScreen = false;
    $scope.LocationType = 1;

    $scope.RomoocIsNotFound = true; //Xác định điểm lấy romooc
    $scope.RomoocMustReturn = true; //Ràng buộc trả romooc
    $scope.VehicleRequestDate = new Date();

    $scope.Color = {
        None: '#f6fafe',
        Error: '#fc0000',
        Success: '#31B6FC'
    }
    $scope.DateRequest = {
        fDate: null,
        tDate: null
    }
    $scope.TripItem = {
        ID: -1,
        VendorOfVehicleID: -1,
        VehicleNo: '[Chờ nhập]',
        RomoocNo: '[Chờ nhập]',
        DriverName: '',
        DriverTel: '',
        StatusCode: 'Có thể tạo chuyến',
        StatusColor: $scope.Color.None,
        VehicleID: -1,
        RomoocID: -1,
        ETA: null,
        ETD: null,
        Ton: 0,
        DateGetRomooc: null,
        DateReturnRomooc: null,
        ListORDCon: [],
        ListORDConName: [],
        ListOPSCon: [],
        LocationStartID: -1,
        LocationStartName: '',
        LocationStartLat: null,
        LocationStartLng: null,
        LocationGetRomoocID: -1,
        LocationGetRomoocName: '',
        LocationGetRomoocLat: null,
        LocationGetRomoocLng: null,
        LocationReturnRomoocID: -1,
        LocationReturnRomoocName: '',
        LocationReturnRomoocLat: null,
        LocationReturnRomoocLng: null,
        LocationEndID: -1,
        LocationEndName: '',
        LocationEndLat: null,
        LocationEndLng: null,
        ListLocation: []
    }
    $scope.TripSearch = {
        DateFrom: new Date().addDays(-3),
        DateTo: new Date().addDays(3),
        ListCustomerID: []
    }

    $scope.DataSort = {};
    $scope.DataTrip = [];

    $scope.ViewVehicleTripApproved = true; //Lọc danh sách chuyến của xe theo trạng thái 'Đang lập'
    $scope.ViewVehicleTripTendered = false; //Lọc danh sách chuyến của xe theo trạng thái 'Đã duyệt'
    $scope.VehicleTripRequest = {
        Date: new Date(),
        VehicleID: -1, RomoocID: -1
    };
    $scope.VehicleTitle = '';

    $scope.WeightConstraint = false; //Hệ thống thiết lập ràng buộc trọng tải xe - hàng

    $scope.ConfigView = { showGrid: true, showMap: true }

    var objSetting = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
    if (objSetting != null && objSetting.Options != null) {
        if (objSetting.Options["OPSShowMap"] != null && (objSetting.Options["OPSShowMap"] == false || objSetting.Options["OPSShowMap"] == 'false')) {
            $scope.ConfigView.showMap = false;
        }
        if (objSetting.Options["OPSShowGrid"] != null && (objSetting.Options["OPSShowGrid"] == false || objSetting.Options["OPSShowGrid"] == 'false')) {
            $scope.ConfigView.showGrid = false;
        }
    }

    $scope.VehicleWinWidth = 300;
    $scope.ContainerWinWidth = 250;

    $scope.viewSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true },
            { collapsible: true, resizable: true, size: '600px', min: '500px' }
        ],
        resize: function (e) {
            try {
                $timeout(function () {
                    $scope.VehicleWinWidth = $('#pnVehicle').width();
                    $scope.ContainerWinWidth = $('#pnContainer').width();
                    $scope.conSplitter.resize();
                }, 1)
                openMapV2.Resize();
            }
            catch (e) { }
        }
    }

    $scope.conSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true },
            { collapsible: true, resizable: true, size: '300px' }
        ],
        resize: function (e) {
            $timeout(function () {
                $scope.VehicleWinWidth = $('#pnVehicle').width();
                $scope.ContainerWinWidth = $('#pnContainer').width();
            }, 1)
        }
    }

    $timeout(function () {
        var pane = $($scope.conSplitter.element).find(".k-pane:last");
        if (!$scope.ConfigView.showGrid) {
            $($scope.conSplitter).find('.k-splitbar:last').hide();
            $scope.conSplitter.collapse(pane);
        }
        pane = $($scope.viewSplitter.element).find(".k-pane:first");
        if (!$scope.ConfigView.showMap) {
            $($scope.viewSplitter.element).find('.k-splitbar:eq(0)').hide();
            $scope.viewSplitter.collapse(pane);
        } else {
            $scope.viewSplitter.resize();
            $scope.conSplitter.resize();
            $('2view').resize();
        }
    }, 100)

    //Lấy thông tin thiết lập hệ thống gồm: ràng buộc trả romooc, điểm bắt đầu, điểm kết thúc
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV3.URL.Setting,
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.RomoocMustReturn = res.RomoocReturn;
                $scope.TripItem.IsRomoocBreak = res.RomoocReturn;
                $scope.WeightConstraint = res.WeightConstraint;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartID = res.LocationStartID;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartName = res.LocationStartName;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndID = res.LocationEndID;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndName = res.LocationEndName;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndLat = res.LocationEndLat;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndLng = res.LocationEndLng;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartLat = res.LocationStartLat;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartLng = res.LocationStartLng;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocID = res.LocationRomoocGetID;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocName = res.LocationRomoocGetName;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLat = res.LocationRomoocGetLat;
                _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLng = res.LocationRomoocGetLng;
            }
        }
    });

    openMapV2.hasMap = $scope.ConfigView.showMap;
    $scope.popupMap = null;
    $scope.indexMap = openMapV2.Init({
        Element: 'map',
        Tooltip_Show: true,
        Tooltip_Element: 'map_tooltip',
        InfoWin_Show: true,
        InfoWin_Element: 'map_info_win',
        ClickMap: function () {
            openMapV2.Close();
        },
        ClickMarker: function (i, o) {
            switch (i.Type) {
                case 'Romooc':
                    $scope.LocationItem = null;
                    $scope.VehicleItem = i.Item;
                    $scope.IsTractor = false;
                    $scope.ViewTractor = false;

                    Common.Data.Each($scope.romooc_Grid.items(), function (tr) {
                        var item = $scope.romooc_Grid.dataItem(tr);
                        $(tr).removeClass('k-state-selected');
                        if (Common.HasValue(item) && item.ID == $scope.VehicleItem.ID) {
                            $(tr).addClass('k-state-selected');
                        }
                    })
                    break;
                case 'Tractor':
                    $scope.LocationItem = null;
                    $scope.VehicleItem = i.Item;
                    $scope.IsTractor = true;
                    $scope.ViewTractor = true;
                    $timeout(function () {
                        $scope.tractor_Grid.resize();
                    }, 200);
                    break;
                case 'Location':
                    $scope.VehicleItem = null;
                    $scope.LocationItem = i.Item;
                    break;
                default:
                    openMapV2.Close();
                    break;
            }
        },
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
            Name: 'VectorMarkerTO',
            zIndex: 100
        }, {
            Name: 'VectorRouteORD',
            zIndex: 90
        }, {
            Name: 'VectorRouteTO',
            zIndex: 90
        }]
    });

    $scope.VehicleStatus = 3;

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

    $scope.con_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.Order_List,
            pageSize: 20,
            readparam: function () {
                return {
                    typeOfOrder: $scope.ViewOrder ? 1 : 2,
                    fDate: $scope.DateRequest.fDate,
                    tDate: $scope.DateRequest.tDate
                }
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
        pageable: Common.PageSize, height: '99%', groupable: true, sortable: true, columnMenu: false, resizable: true,
        filterable: { mode: 'row', visible: false }, reorderable: false,
        dataBound: function (e) {
            $scope.con_GridChoose_Change(e, this, false);
        },
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,con_Grid,con_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,con_Grid,con_GridChoose_Change)" />',
                filterable: false, sortable: false, groupable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            //{
            //    field: 'Command', title: ' ', width: '55px', hidden: true,
            //    attributes: { style: 'text-align: center;' },
            //    template: '<a class="k-button small-button btn-split" title="Chia" href="/" ng-click="Container_Split($event,dataItem,map_win)"><i class="fa fa-minus"></i></a>' +
            //        '<a class="k-button small-button btn-merge" ng-show="dataItem.ParentID>0" title="Gộp" href="/" ng-click="Container_Merge($event,dataItem,con_Grid)">M</a>' +
            //        '<a class="k-button small-button btn-merge-ok" style="display:none;" title="Xác nhận" href="/" ng-click="Container_Merge_OK($event,dataItem,con_Grid)">S</a>' +
            //        '<input type="checkbox" style="display:none;margin:0px 11px;" class="chk-select-to-merge" />',
            //    filterable: false, sortable: false, groupable: false, sortorder: 1, configurable: false, isfunctionalHidden: false
            //},
            {
                field: 'IsWarning', width: 100, title: 'Cảnh báo', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false, sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerCode', width: 150, title: 'Khách hàng',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:CustomerCode#</a>#}else{##:CustomerCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: 100, title: 'Mã ĐH',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:OrderCode#</a>#}else{##:OrderCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfContainerName', width: 80, title: 'Loại con.',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:TypeOfContainerName#</a>#}else{##:TypeOfContainerName##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ServiceOfOrderName', width: 100, title: 'Loại v/c',
                template: '#if(ServiceOfOrderName>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ServiceOfOrderName#</a>#}else{##:ServiceOfOrderName##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'StatusOfContainerName', width: 120, title: 'T/trạng con.', hidden: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 7, configurable: true, isfunctionalHidden: true
            },
            {
                field: 'ContainerNo', width: 100, title: 'Số con.',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ContainerNo#</a>#}else{##:ContainerNo##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: 160, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: 160, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: 50, title: 'Trọng tải',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Ton#</a>#}else{##:Ton##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromCode', width: 150, title: 'Điểm nhận',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationFromCode#</a>#}else{##:LocationFromCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToCode', width: 150, title: 'Điểm giao',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationToCode#</a>#}else{##:LocationToCode##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationFromAddress#</a>#}else{##:LocationFromAddress##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:LocationToAddress#</a>#}else{##:LocationToAddress##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo1', width: 100, title: 'Số seal 1',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:SealNo1#</a>#}else{##:SealNo1##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo2', width: 100, title: 'Số seal 2',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:SealNo2#</a>#}else{##:SealNo2##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TripNo', width: 100, title: 'Số chuyến',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:TripNo#</a>#}else{##:TripNo##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ShipNo', width: 100, title: 'Số tàu',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ShipNo#</a>#}else{##:ShipNo##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ShipName', width: 100, title: 'Tên tàu',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:ShipName#</a>#}else{##:ShipName##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined1', width: 100, title: 'Định nghĩa 1',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined1#</a>#}else{##:UserDefined1##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined2', width: 100, title: 'Định nghĩa 2',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined2#</a>#}else{##:UserDefined2##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined3', width: 100, title: 'Định nghĩa 3',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined3#</a>#}else{##:UserDefined3##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined4', width: 100, title: 'Định nghĩa 4',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined4#</a>#}else{##:UserDefined4##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 23, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined5', width: 100, title: 'Định nghĩa 5',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined5#</a>#}else{##:UserDefined5##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 24, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefined6', width: 100, title: 'Định nghĩa 6',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:UserDefined6#</a>#}else{##:UserDefined6##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 25, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'WarningTime', width: 100, title: 'TG cảnh báo',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:WarningTime#</a>#}else{##:WarningTime##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 26, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'WarningMsg', width: 100, title: 'ND cảnh báo',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:WarningMsg#</a>#}else{##:WarningMsg##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 27, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note0', width: 150, title: 'Ghi chú',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Note0#</a>#}else{##:Note0##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 28, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note1', width: 150, title: 'Ghi chú 1',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Note1#</a>#}else{##:Note1##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 29, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note2', width: 150, title: 'Ghi chú 1',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:Note2#</a>#}else{##:Note2##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 30, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupProductCode', width: 150, title: 'Mã hàng hóa',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:GroupProductCode#</a>#}else{##:GroupProductCode##}#',
                filterable: false, sortable: false, sortorder: 31, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupProductName', width: 150, title: 'Tên hàng hóa',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:GroupProductName#</a>#}else{##:GroupProductName##}#',
                filterable: false, sortable: false, sortorder: 32, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupSort', width: 200, title: ' ',
                template: '#if(OPSContainerID>0){#<a href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)">#:GroupSort#</a>#}else{##:GroupSort##}#',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 33, configurable: true, isfunctionalHidden: false
            },
            { title: '', filterable: false, sortable: false, sortorder: 99, configurable: false, isfunctionalHidden: true }
        ]
    };

    $scope.romooc_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.Romooc_List,
            readparam: function () {
                return {
                    requestDate: $scope.VehicleRequestDate
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
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                if (obj.Lat > 0 && obj.Lng > 0) {
                    openMapV2.Center(obj.Lat, obj.Lng);
                }
            }
        },
        dataBound: function () {
            Common.Log("LoadRommoocFinished");
            var grid = this;

            openMapV2.ClearVector("VectorMarkerROM");
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && item.Lat > 0 && item.Lng > 0) {
                    var img = Common.String.Format(openMapV2.NewImage.Romooc_20);
                    if (item.Group == 2)
                        img = Common.String.Format(openMapV2.NewImage.Romooc_40);
                    var icon = openMapV2.NewStyle.Icon(img, 1);
                    openMapV2.NewMarker(item.Lat, item.Lng, item.Regno, item.Regno, icon, {
                        Item: item, Type: 'Romooc'
                    }, "VectorMarkerROM");
                }
            });
        },
        columns: [
            {
                field: 'Regno', width: 120, title: 'Số romooc',
                template: '<span style="cursor:pointer;" ng-mouseenter="ViewVehicleInfo($event,dataItem,2)" ' +
                    'ng-click="VehicleInfo_View($event,dataItem,2,vehicle_trip_win)" ' +
                    'ng-mouseleave="HideVehicleInfo($event,dataItem)">#=Regno# </span>' +
                    '<a class="k-button select" ng-class="TripItem.RomoocID==#=ID#?\'selected\':\'\'" ' +
                    'ng-click="Romooc_Select($event,dataItem,romooc_Grid)"><span>LC</span></a>',
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

    $scope.tractor_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.Tractor_List,
            readparam: function () {
                return {
                    requestDate: $scope.VehicleRequestDate
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
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                if (obj.Lat > 0 && obj.Lng > 0) {
                    openMapV2.Center(obj.Lat, obj.Lng);
                }
            }
        },
        dataBound: function () {
            Common.Log("LoadVehicleFinished");
            var grid = this;

            openMapV2.ClearVector("VectorMarkerVEH");
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && item.Lat > 0 && item.Lng > 0) {
                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Tractor), 1);
                    openMapV2.NewMarker(item.Lat, item.Lng, item.Regno, item.Regno, icon, {
                        Item: item, Type: 'Tractor'
                    }, "VectorMarkerVEH");
                }
            });
        },
        columns: [
            {
                field: 'Regno', width: 120, title: 'Số xe',
                template: '<span style="cursor:pointer;" ng-mouseenter="ViewVehicleInfo($event,dataItem,1)" ' +
                    'ng-click="VehicleInfo_View($event,dataItem,1,vehicle_trip_win)" ' +
                    'ng-mouseleave="HideVehicleInfo($event,dataItem)">#=Regno# </span>' +
                    '<a class="k-button select" ng-class="TripItem.VehicleID==#=ID#?\'selected\':\'\'" ' +
                    'ng-click="Tractor_Select($event,dataItem,tractor_Grid)"><span>LC</span></a>',
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

    $scope.$watch('VehicleRequestDate', function () {
        $scope.romooc_Grid.dataSource.read();
        $scope.tractor_Grid.dataSource.read();
    });

    $scope.vehicle_Trip_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' },
                        DateGetRomooc: { type: 'date' },
                        DateReturnRomooc: { type: 'date' },
                        ETA: { type: 'date' }
                    }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true,
        columns: [
            {
                field: 'Code', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateGetRomooc', width: '120px', title: 'Lấy romooc', template: "#=DateGetRomooc != null ? Common.Date.FromJsonDMYHM(DateGetRomooc) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'DateReturnRomooc', width: '120px', title: 'Trả romooc', template: "#=DateReturnRomooc != null ? Common.Date.FromJsonDMYHM(DateReturnRomooc) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETD', width: '120px', title: 'Bắt đầu (ETD)', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'Kết thúc (ETA)', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            }
        ]
    }

    $scope.location_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.Location_List,
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true,
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

    $scope.$watch('TripActived', function (n, o) {
        if ($scope.TripActived) {
            $scope.trip_info_win.close();
            $scope.TripDetail = false;
            openMapV2.VisibleVector("VectorRouteORD", false);
            openMapV2.VisibleVector("VectorMarkerORD", false);
        } else {
            openMapV2.VisibleVector("VectorRouteORD", true);
            openMapV2.VisibleVector("VectorMarkerORD", true);
            openMapV2.VisibleVector("VectorRouteTO", false);
            openMapV2.VisibleVector("VectorMarkerTO", false);
            openMapV2.FitBound("VectorMarkerORD", 15);
        }
        $timeout(function () {
            $scope.viewSplitter.resize();
            $('#2view').resize();
        }, 100)
    });
    //Xe Vendor
    $scope.IsVehicleVendor = false;
    $scope.VehicleVendorID = -1;
    $scope.NewVehicleVendor = {
        RegNo: '', MaxWeight: 0
    }
    $scope.cboVehicleVendor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: { id: 'ID', fields: { ID: { type: 'number' }, CustomerName: { type: 'string' } } }
        })
    }

    $scope.VehicleVendor_Click = function ($event) {
        $event.preventDefault();

        $scope.IsVehicleVendor = !$scope.IsVehicleVendor;
        if ($scope.IsVehicleVendor) {
            $($event.currentTarget).find('.tooltip').text("Xe nhà");
        } else {
            $($event.currentTarget).find('.tooltip').text("Xe đối tác");
        }
    }

    $scope.$watch('IsVehicleVendor', function () {
        $scope.TripItem.VehicleNo = '[Chờ nhập]';
        $scope.TripItem.VehicleID = -1;
        $scope.TripItem.RomoocNo = '[Chờ nhập]';
        $scope.TripItem.RomoocID = -1;
        $scope.TripItem.DriverName = '';
        $scope.TripItem.DriverTel = '';
        $scope.TripItem.StatusCode = 'Có thể tạo chuyến';
        $scope.TripItem.StatusColor = $scope.Color.None;
        $timeout(function () {
            if ($scope.IsVehicleVendor) {
                try {
                    if ($scope.ViewTractor)
                        $scope.tractorVendor_Grid_Options.dataSource.read();
                    else
                        $scope.romoocVendor_Grid_Options.dataSource.read();
                    openMapV2.VisibleVector("VectorMarkerVEH", false);
                    openMapV2.VisibleVector("VectorMarkerROM", false);
                } catch (e) {
                }
            } else {
                try {
                    openMapV2.VisibleVector("VectorMarkerVEH", true);
                    openMapV2.VisibleVector("VectorMarkerROM", true);
                } catch (e) {
                }
            }
        }, 1)
    });

    $scope.$watch('VehicleVendorID', function () {
        if ($scope.IsVehicleVendor) {
            if ($scope.ViewTractor)
                $scope.tractorVendor_Grid_Options.dataSource.read();
            else
                $scope.romoocVendor_Grid_Options.dataSource.read();
        }
    });

    $scope.$watch('ViewTractor', function () {
        if ($scope.IsVehicleVendor) {
            try {
                if ($scope.ViewTractor)
                    $scope.tractorVendor_Grid_Options.dataSource.read();
                else
                    $scope.romoocVendor_Grid_Options.dataSource.read();
            } catch (e) {
            }
        }
    })

    $scope.romoocVendor_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV2.URL.VehicleVendor_List,
            readparam: function () {
                return {
                    vendorID: $scope.VehicleVendorID, typeofvehicle: 2
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
        columns: [
            {
                field: 'Regno', width: 150, title: 'Romooc',
                template: '<span>#=Regno# </span>' + '<a class="k-button select" ng-class="TripItem.RomoocID==#=ID#?\'selected\':\'\'" ' + 'ng-click="Romooc_Select($event,dataItem,romoocVendor_Grid)"><span>LC</span></a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'MaxWeight', width: 100, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.tractorVendor_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV2.URL.VehicleVendor_List,
            readparam: function () {
                return {
                    vendorID: $scope.VehicleVendorID, typeofvehicle: 1
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
        columns: [
            {
                field: 'Regno', width: 150, title: 'Số xe',
                template: '<span>#=Regno# </span>' + '<a class="k-button select" ng-class="TripItem.VehicleID==#=ID#?\'selected\':\'\'" ' + 'ng-click="Tractor_Select($event,dataItem,tractorVendor_Grid)"><span>LC</span></a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'MaxWeight', width: 100, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.VehicleVendor_New_Click = function ($event, win) {
        $event.preventDefault();

        $scope.NewVehicleVendor = {
            RegNo: '', MaxWeight: 0
        }
        win.center().open();
    }

    $scope.VehicleVendor_New_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận lưu?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV2.URL.New_Vendor_Vehicle_Save,
                    data: {
                        vendorID: $scope.VehicleVendorID,
                        regNo: $scope.NewVehicleVendor.RegNo,
                        maxWeight: $scope.NewVehicleVendor.MaxWeight,
                        typeofVehicle: $scope.ViewTractor ? 1 : 2,
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            });
                            win.close();
                            if ($scope.ViewTractor)
                                $scope.tractorVendor_Grid_Options.dataSource.read();
                            else
                                $scope.romoocVendor_Grid_Options.dataSource.read();
                        })
                    }
                })
            }
        })
    }

    $scope.atcDriverVendorNameOptions = {
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

    //Hiện popup chuyến

    $scope.Text_Click = function ($event, size) {
        $event.preventDefault();

        switch (size) {
            case 1:
                break;
            case 2:
                $('.cus-splitter .cus-form, .cus-splitter .form-body, .cus-splitter .form-body .trip-table').removeClass('small-font');
                break;
            case 3:
                $('.cus-splitter .cus-form, .cus-splitter .form-body, .cus-splitter .form-body .trip-table').addClass('small-font');
                break;
        }
        $('.cus-splitter .cus-form .cus-grid.k-grid').each(function () {
            $(this).data('kendoGrid').refresh();
        })
    }

    $scope.Trip_Click = function ($event, grid, win) {
        $event.preventDefault();

        if ($scope.IsShowTrip == false) {
            win.center().open();
            grid.dataSource.read();

            $scope.IsShowTrip = true;
            $scope.TripActived = true;
        }
        else {
            win.close();

            $scope.IsShowTrip = false;
            $scope.TripActived = false;
        }
    }

    $scope.con_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;

        var Ton = 0;
        var ETD = null;
        var ETA = null;
        var data = [];
        var dataCode = [];
        var dataLocation = [];
        var tmpDataM = {};
        angular.forEach(grid.items(), function (tr) {
            var item = grid.dataItem(tr);
            if (Common.HasValue(item) && item.IsChoose == true) {
                data.push(item.ID);
                dataCode.push(item);
                if (ETD == null) ETD = Common.Date.FromJson(item.ETD);
                if (ETA == null) ETA = Common.Date.FromJson(item.ETA);
                if (ETD > Common.Date.FromJson(item.ETD)) ETD = Common.Date.FromJson(item.ETD);
                if (ETA < Common.Date.FromJson(item.ETA)) ETA = Common.Date.FromJson(item.ETA);
                Ton += item.Ton;
                if (item.ListLocation != null) {
                    for (var i = 0; i < item.ListLocation.length; i++) {
                        var o = item.ListLocation[i];
                        if (!Common.HasValue(tmpDataM[o.LocationID])) {
                            tmpDataM[o.LocationID] = 1;
                            dataLocation.push(o);
                        }
                    }
                }
            }
        });

        $scope.TripItem.ETD = ETD;
        $scope.TripItem.ETA = ETA;
        $scope.TripItem.Ton = Ton;
        if ($scope.ViewOrder) {
            $scope.TripItem.ListORDCon = data;
            $scope.TripItem.ListOPSCon = [];
        } else {
            $scope.TripItem.ListOPSCon = data;
            $scope.TripItem.ListORDCon = [];
        }
        $scope.TripItem.ListLocation = dataLocation;
        $scope.TripItem.ListORDConName = dataCode;
        if ($scope.TripDetail) {
            if ($scope.TripItem.ListORDConName == null || $scope.TripItem.ListORDConName.length == 0) {
                $timeout(function () {
                    $scope.TripItem.StatusColor = $scope.Color.Error;
                    $scope.TripItem.StatusCode = "Không có thông tin container";
                    $scope.IsTripChecking = false;
                }, 1)
            }
        }
        $scope.VehicleRequestDate = ETD || $scope.VehicleRequestDate;
        $scope.RefreshMap();
    }

    $scope.RefreshMap = function () {
        openMapV2.ClearVector("VectorRouteORD");
        openMapV2.ClearVector("VectorMarkerORD");
        var objS, objE, objG, objR, oS, oR, oG, oE;
        if ($scope.TripItem.LocationEndID > 0 && $scope.TripItem.LocationEndLat != null && $scope.TripItem.LocationEndLng != null) {
            var oE = {
                ID: $scope.TripItem.LocationEndID,
                LocationID: $scope.TripItem.LocationEndID,
                LocationName: $scope.TripItem.LocationEndName,
                Lat: $scope.TripItem.LocationEndLat,
                Lng: $scope.TripItem.LocationEndLng
            }
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.End), 1);
            objE = openMapV2.NewMarker(oE.Lat, oE.Lng, "", oE.LocationName, icon, {
                Item: oE, Type: 'TOLocation'
            }, "VectorMarkerORD")
        }
        if ($scope.TripItem.LocationStartID > 0 && $scope.TripItem.LocationStartLat != null && $scope.TripItem.LocationStartLng != null) {
            var oS = {
                ID: $scope.TripItem.LocationStartID,
                LocationID: $scope.TripItem.LocationStartID,
                LocationName: $scope.TripItem.LocationStartName,
                Lat: $scope.TripItem.LocationStartLat,
                Lng: $scope.TripItem.LocationStartLng
            }
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Start), 1);
            objS = openMapV2.NewMarker(oS.Lat, oS.Lng, "", oS.LocationName, icon, {
                Item: oS, Type: 'TOLocation'
            }, "VectorMarkerORD")
        }
        if ($scope.TripItem.LocationGetRomoocID > 0 && $scope.TripItem.LocationGetRomoocLat != null && $scope.TripItem.LocationGetRomoocLng != null) {
            var oG = {
                ID: $scope.TripItem.LocationGetRomoocID,
                LocationID: $scope.TripItem.LocationGetRomoocID,
                LocationName: $scope.TripItem.LocationGetRomoocName,
                Lat: $scope.TripItem.LocationGetRomoocLat,
                Lng: $scope.TripItem.LocationGetRomoocLng
            }
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.RomoocGet), 1);
            objG = openMapV2.NewMarker(oG.Lat, oG.Lng, "", oG.LocationName, icon, {
                Item: oG, Type: 'TOLocation'
            }, "VectorMarkerORD")
        }
        if ($scope.TripItem.LocationReturnRomoocID > 0 && $scope.TripItem.LocationReturnRomoocLat != null && $scope.TripItem.LocationReturnRomoocLng != null) {
            var oR = {
                ID: $scope.TripItem.LocationReturnRomoocID,
                LocationID: $scope.TripItem.LocationReturnRomoocID,
                LocationName: $scope.TripItem.LocationReturnRomoocName,
                Lat: $scope.TripItem.LocationReturnRomoocLat,
                Lng: $scope.TripItem.LocationReturnRomoocLng
            }
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.RomoocReturn), 1);
            objR = openMapV2.NewMarker(oR.Lat, oR.Lng, "LocationReturn", oR.LocationName, icon, {
                Item: oR, Type: 'TOLocation'
            }, "VectorMarkerORD")
        }

        var tmpDataM = [], tmpDataR = [];
        if (Common.HasValue(objS) && Common.HasValue(objG)) {
            tmpDataR[oS.ID + "-" + oG.ID] = true;
            var strName = oS.LocationName + " - " + oG.LocationName;
            openMapV2.NewRoute(objS, objG, "", strName, openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)', [20, 10]), null, "VectorRouteORD", $scope.indexMap, function () {
            }, true);
        }
        if (Common.HasValue(objR) && Common.HasValue(objE)) {
            tmpDataR[oR.ID + "-" + oE.ID] = true;
            var strName = oR.LocationName + " - " + oE.LocationName;
            openMapV2.NewRoute(objR, objE, "", strName, openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)', [20, 10]), null, "VectorRouteORD", $scope.indexMap, function () {
            }, true);
        }
        if ($scope.TripItem.ListLocation != null) {
            var sTotal = $scope.TripItem.ListLocation.length;
            for (var i = 0; i < sTotal; i++) {
                var curr = $scope.TripItem.ListLocation[i];
                if (!Common.HasValue(tmpDataM[curr.LocationID]) && curr.Lat > 0 && curr.Lng > 0) {
                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Get), 1);
                    if (curr.TypeOfTOLocationID == 2)
                        icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Delivery), 1);
                    tmpDataM[curr.LocationID] = openMapV2.NewMarker(curr.Lat, curr.Lng, curr.Code, curr.LocationName, icon, {
                        Item: curr, Type: 'TOLocation'
                    }, "VectorMarkerORD")
                }
                if (i != 0) {
                    var prev = $scope.TripItem.ListLocation[i - 1];
                    if (Common.HasValue(tmpDataM[curr.LocationID]) && Common.HasValue(tmpDataM[prev.LocationID]) && !Common.HasValue(tmpDataR[prev.LocationID + "-" + curr.LocationID])) {
                        tmpDataR[prev.LocationID + "-" + curr.LocationID] = true;
                        var strName = prev.LocationName + " - " + curr.LocationName;
                        openMapV2.NewRoute(tmpDataM[prev.LocationID], tmpDataM[curr.LocationID], "", strName, openMapV2.NewStyle.Line(4, 'rgba(3, 169, 244, 0.6)'), null, "VectorRouteORD", $scope.indexMap, function () {
                        }, true);
                    }
                    if (i == sTotal - 1) {
                        if (Common.HasValue(objR) && Common.HasValue(tmpDataM[curr.LocationID]) && !Common.HasValue(tmpDataR[curr.LocationID + "-" + oR.ID])) {
                            tmpDataR[curr.LocationID + "-" + oR.ID] = true;
                            var strName = curr.LocationName + " - " + oR.LocationName;
                            openMapV2.NewRoute(tmpDataM[curr.LocationID], objR, "", strName, openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)', [20, 10]), null, "VectorRouteORD", $scope.indexMap, function () {
                            }, true);
                        }
                    }
                } else {
                    if (Common.HasValue(objG) && Common.HasValue(tmpDataM[curr.LocationID]) && !Common.HasValue(tmpDataR[oG.ID + "-" + curr.LocationID])) {
                        tmpDataR[oG.ID + "-" + curr.LocationID] = true;
                        var strName = oG.LocationName + " - " + curr.LocationName;
                        openMapV2.NewRoute(objG, tmpDataM[curr.LocationID], "", strName, openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)', [20, 10]), null, "VectorRouteORD", $scope.indexMap, function () {
                        }, true);
                    }
                }
            }
        }
        openMapV2.FitBound("VectorMarkerORD", 15);
    }

    $scope.VehicleInfo = {
        IsLoaded: false,
        ListTrip: []
    }

    $scope.VehicleInfoStyle = {
        'display': 'none', 'top': 0, 'left': 0
    }

    //Move tooltip => body
    $('#vehicle_tooltip').detach().appendTo('body');

    $scope.ViewVehicleInfo = function ($event, item, type) {
        $scope.VehicleInfo = {
            IsLoaded: false,
            ListTrip: []
        }

        var off = $($event.currentTarget).offset();
        $scope.VehicleInfoStyle = {
            'display': '', 'top': off.top - 8, 'left': off.left - 395
        }

        if (!Common.HasValue(_OPSAppointment_COViewOnMapV3.Data.VehicleInfo[type + "_" + item.ID])) {
            _OPSAppointment_COViewOnMapV3.Data.VehicleInfo[type + "_" + item.ID] = {
                IsLoaded: false,
                ListTrip: []
            }

            //Load data.
            $scope.LoadVehicleTrip(new Date(), type == 1 ? item.ID : -1, type == 2 ? item.ID : -1, 3, true, true, function (res) {
                _OPSAppointment_COViewOnMapV3.Data.VehicleInfo[type + "_" + item.ID] = {
                    IsLoaded: true,
                    ListTrip: res
                }
                var obj = _OPSAppointment_COViewOnMapV3.Data.VehicleInfo[type + "_" + item.ID];
                $scope.VehicleInfo = {
                    IsLoaded: obj.IsLoaded,
                    ListTrip: obj.ListTrip
                }
            });
        } else {
            var obj = _OPSAppointment_COViewOnMapV3.Data.VehicleInfo[type + "_" + item.ID];
            $scope.VehicleInfo = {
                IsLoaded: obj.IsLoaded,
                ListTrip: obj.ListTrip
            }
        }
    }

    $scope.LoadVehicleTrip = function (requestDate, vehicleID, romoocID, total, isapproved, istendered, callback) {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.TripByVehicle_List,
            data: {
                Date: requestDate, vehicleID: vehicleID, romoocID: romoocID,
                total: total, isApproved: isapproved, isTendered: istendered
            },
            success: function (res) {
                if (Common.HasValue(res) && Common.HasValue(res)) {
                    callback(res);
                }
            }
        });
    }

    $scope.HideVehicleInfo = function ($event, item) {
        $scope.VehicleInfoStyle = {
            'display': 'none', 'top': 0, 'left': 0
        }
    }

    $scope.VehicleInfo_View = function ($event, item, type, win) {
        $event.preventDefault();

        $scope.VehicleTitle = item.Regno;

        $scope.VehicleTripRequest.RomoocID = type == 2 ? item.ID : -1;
        $scope.VehicleTripRequest.VehicleID = type == 1 ? item.ID : -1;
        $scope.ViewVehicleTripApproved = true;
        $scope.ViewVehicleTripTendered = false;
        $scope.LoadVehicleTrip($scope.VehicleTripRequest.Date, $scope.VehicleTripRequest.VehicleID, $scope.VehicleTripRequest.RomoocID, -1, $scope.ViewVehicleTripApproved, $scope.ViewVehicleTripTendered, function (res) {
            $scope.vehicle_Trip_Grid_Options.dataSource.data(res);
            win.center().open();
            $timeout(function () {
                $scope.vehicle_Trip_Grid.resize();
            }, 100)
        });
    }

    $scope.Tractor_Select = function ($event, item, grid) {
        $event.preventDefault();

        angular.forEach(grid.items(), function (tr) {
            $(tr).find('.k-button.select').removeClass('selected');
        });

        if (Common.HasValue(item)) {
            $($event.currentTarget).addClass('selected');
            $scope.TripItem.VehicleID = item.ID;
            $scope.TripItem.VehicleNo = item.Regno;
        }
    }

    $scope.Romooc_Select = function ($event, item, grid) {
        $event.preventDefault();

        angular.forEach(grid.items(), function (tr) {
            $(tr).find('.k-button.select').removeClass('selected');
        });

        if (Common.HasValue(item)) {
            $($event.currentTarget).addClass('selected');
            $scope.TripItem.RomoocID = item.ID;
            $scope.TripItem.RomoocNo = item.Regno;
        }
    }

    $scope.OrderViewStatus_Click = function ($event, grid) {
        $event.preventDefault();
        try {
            $scope.ShowOrderDate = false;
            var value = $($event.currentTarget).data('tabindex');
            if (value == 1) {
                $scope.ViewOrder = true;
                grid.hideColumn('Command');
                grid.hideColumn('StatusOfContainerName');
            } else {
                $scope.ViewOrder = false;
                grid.showColumn('Command');
                grid.showColumn('StatusOfContainerName');
            }
            grid.dataSource.read();
        }
        catch (e) { }
    }

    $scope.OrderViewDate_Click = function ($event, grid) {
        $event.preventDefault();
        if ($event.currentTarget == $event.target)
            $scope.ShowOrderDate = !$scope.ShowOrderDate;
    }

    $scope.OrderViewDate_Cancel_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.ViewOrderDate = false;
        $scope.ShowOrderDate = false;
        $scope.DateRequest = { fDate: null, tDate: null }
        grid.dataSource.read();
    }

    $scope.OrderViewDate_OK_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.ViewOrderDate = true;
        $scope.ShowOrderDate = false;
        grid.dataSource.read();
    }

    $scope.VehicleViewStatus_Click = function ($event, grid) {
        $event.preventDefault();

        try {
            var value = $($event.currentTarget).data('tabindex');
            if (value == 1) {
                $scope.ViewTractor = true;
            } else {
                $scope.ViewTractor = false;
            }
            //grid.dataSource.read();
            $timeout(function () {
                grid.resize();
            }, 100);
        }
        catch (e) { }
    }

    $scope.VehicleTripViewStatus_Click = function ($event, grid) {
        $event.preventDefault();
        try {
            var flag = true;
            var value = $($event.currentTarget).data('tabindex');
            if (value == 1) {
                $scope.ViewVehicleTripApproved = !$scope.ViewVehicleTripApproved;
                if ($scope.ViewVehicleTripApproved == false && $scope.ViewVehicleTripTendered == false) {
                    $scope.ViewVehicleTripApproved = !$scope.ViewVehicleTripApproved;
                    flag = false;
                }
            } else {
                $scope.ViewVehicleTripTendered = !$scope.ViewVehicleTripTendered;
                if ($scope.ViewVehicleTripApproved == false && $scope.ViewVehicleTripTendered == false) {
                    $scope.ViewVehicleTripTendered = !$scope.ViewVehicleTripTendered;
                    flag = false;
                }
            }
            if (flag) {
                $scope.LoadVehicleTrip($scope.VehicleTripRequest.Date, $scope.VehicleTripRequest.VehicleID, $scope.VehicleTripRequest.RomoocID, -1, $scope.ViewVehicleTripApproved, $scope.ViewVehicleTripTendered, function (res) {
                    grid.dataSource.data(res);
                })
            }
        }
        catch (e) { }
    }

    $scope.$watch('VehicleTripRequest.Date', function (n, o) {
        if (n != 0) {
            $timeout(function () {
                $scope.LoadVehicleTrip($scope.VehicleTripRequest.Date, $scope.VehicleTripRequest.VehicleID, $scope.VehicleTripRequest.RomoocID, -1, $scope.ViewVehicleTripApproved, $scope.ViewVehicleTripTendered, function (res) {
                    $scope.vehicle_Trip_Grid_Options.dataSource.data(res);
                })
            }, 1)
        }
    });

    $scope.Accept_Click = function ($event, grid, win, vwin) {
        $event.preventDefault();

        var data = [];
        var dataCode = [];
        angular.forEach(grid.dataSource.data(), function (item) {
            if (item.IsChoose) {
                data.push(item.ID);
                dataCode.push(item);
            }
        })
        if ($scope.TripItem.ListORDConName.length == 0 || $scope.TripItem.VehicleID < 1 || $scope.TripItem.RomoocID < 1 || $scope.TripItem.ETA == null || $scope.TripItem.ETD == null) {
            $rootScope.Message({ Msg: "Vui lòng điền đầy đủ thông tin.", Type: Common.Message.Type.Alert });
        } else {
            $rootScope.IsLoading = true;

            $scope.TripItem.ListORDConName = dataCode;
            if ($scope.ViewOrder) {
                $scope.TripItem.ListORDCon = data;
                $scope.TripItem.ListOPSCon = [];
            } else {
                $scope.TripItem.ListOPSCon = data;
                $scope.TripItem.ListORDCon = [];
            }
            if ($scope.IsVehicleVendor) {
                $scope.TripItem.VendorOfVehicleID = $scope.VehicleVendorID;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV3.URL.DriverVendor_List,
                    data: {
                        vendorID: $scope.VehicleVendorID
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            var data = [];
                            $.each(res, function (i, v) {
                                data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
                            });
                            $scope.atcDriverVendorNameOptions.dataSource.data(data);

                            $rootScope.IsLoading = false;
                            $scope.TripDetail = true;
                            $scope.TripItem.IsCheching = false;
                            vwin.center().open();
                            $timeout(function () {
                                $scope.IsTripChecking = true;
                            }, 100)
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                });
            } else {
                $scope.TripItem.VendorOfVehicleID = -1;
                $scope.TripItem.LocationStartID = _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartID;
                $scope.TripItem.LocationStartName = _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartName;
                $scope.TripItem.LocationEndID = _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndID;
                $scope.TripItem.LocationEndName = _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndName;

                $scope.TripItem.LocationEndLat = _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndLat;
                $scope.TripItem.LocationEndLng = _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndLng;
                $scope.TripItem.LocationStartLat = _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartLat;
                $scope.TripItem.LocationStartLng = _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartLng;

                $scope.TripItem.LocationGetRomoocID = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocID;
                $scope.TripItem.LocationGetRomoocName = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocName;
                $scope.TripItem.LocationGetRomoocLat = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLat;
                $scope.TripItem.LocationGetRomoocLng = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLng;

                if ($scope.RomoocMustReturn == true) {
                    $scope.TripItem.LocationReturnRomoocID = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocID;
                    $scope.TripItem.LocationReturnRomoocName = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocName;
                    $scope.TripItem.LocationReturnRomoocLat = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLat;
                    $scope.TripItem.LocationReturnRomoocLng = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLng;
                }
                //Kiểm tra đầu kéo, romooc vào khoảng ETD và ETA. Lấy thông tin điểm lấy romooc.
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV3.URL.CheckVehicleAvailable,
                    data: {
                        masterID: $scope.TripItem.ID,
                        vehicleID: $scope.TripItem.VehicleID,
                        romoocID: $scope.TripItem.RomoocID,
                        ETD: $scope.TripItem.ETD,
                        ETA: $scope.TripItem.ETA,
                        Ton: $scope.TripItem.Ton,
                        dataCon: $scope.TripItem.ListOPSCon,
                        dataOPSCon: [],
                        dataORDCon: $scope.TripItem.ListORDCon
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $scope.RomoocIsNotFound = !res.IsVehicleHasRomooc;
                            $scope.TripDetail = true;
                            $scope.TripItem.IsCheching = false;
                            $scope.TripItem.DataContainerOffer = res.ListCOContainer;
                            $scope.TripItem.TimeMin = Common.Date.FromJson(res.DateMin);
                            $scope.TripItem.TimeMax = Common.Date.FromJson(res.DateMax);
                            $scope.TripItem.MinInterval = res.HourETAOffer || 0.5;
                            $scope.TripItem.IsAllowChangeRomooc = res.AllowChangeRomooc;
                            if ($scope.TripItem.DataContainerOffer == null || $scope.TripItem.DataContainerOffer.length == 0) {
                                $scope.TripItem.StatusCode = "TG không phù hợp";
                                $scope.TripItem.StatusColor = $scope.Color.Error;
                            }
                            if (res.LocationGetRomoocID > 0) {
                                $scope.TripItem.LocationGetRomoocID = res.LocationGetRomoocID;
                                $scope.TripItem.LocationGetRomoocName = res.LocationGetRomoocName;
                            }
                            $scope.TripItem.DriverName = res.DriverName;
                            $scope.TripItem.DriverTel = res.DriverTel;

                            win.center().open();
                            $timeout(function () {
                                $scope.IsTripChecking = true;
                            }, 100)
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        }
    }

    $scope.Save_Click = function ($event, win) {
        $event.preventDefault();

        var flag = true;
        if ($scope.TripItem.ListOPSCon.length == 0 && $scope.TripItem.ListORDCon.length == 0) {
            flag = false;
            $rootScope.Message({ Msg: "Không có thông tin container.", Type: Common.Message.Type.Alert });
        }
        else if ($scope.TripItem.ETA == null || $scope.TripItem.ETD == null) {
            flag = false;
            $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        } else if ($scope.TripItem.LocationGetRomoocID < 1) {
            //flag = false;
            //$rootScope.Message({ Msg: "Chưa chọn điểm lấy romooc.", Type: Common.Message.Type.Alert });
        } else if ($scope.RomoocMustReturn && $scope.TripItem.LocationReturnRomoocID < 1) {
            //flag = false;
            //$rootScope.Message({ Msg: "Chưa chọn điểm trả romooc.", Type: Common.Message.Type.Alert });
        }

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận lưu?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.Save,
                        data: {
                            item: $scope.TripItem
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                });
                                $scope.ResetTrip(true);
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

    $scope.TripLocation_Change = function ($event, type, win) {
        $event.preventDefault();

        $scope.LocationType = type;
        win.center().open();
        $timeout(function () {
            $scope.location_Grid.resize();
        }, 100)
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
            case 2: //RomoocGet
                $scope.TripItem.LocationGetRomoocID = item.ID;
                $scope.TripItem.LocationGetRomoocName = item.Location;
                $scope.TripItem.LocationGetRomoocLat = item.Lat;
                $scope.TripItem.LocationGetRomoocLng = item.Lng;
                break;
            case 3: //RomoocReturn
                $scope.TripItem.LocationReturnRomoocID = item.ID;
                $scope.TripItem.LocationReturnRomoocName = item.Location;
                $scope.TripItem.LocationReturnRomoocLat = item.Lat;
                $scope.TripItem.LocationReturnRomoocLng = item.Lng;
                break;
            case 4: //End
                $scope.TripItem.LocationEndID = item.ID;
                $scope.TripItem.LocationEndName = item.Location;
                $scope.TripItem.LocationEndLat = item.Lat;
                $scope.TripItem.LocationEndLng = item.Lng;
                break;
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
                } catch (e) {
                }
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
                $scope.popupMap = openMapV2.Init({
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
                    method: _OPSAppointment_COViewOnMapV3.URL.Location_List,
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

    $scope.OnMap_LocationChoose_Click = function ($event, winmap, win) {
        $event.preventDefault();
        var item = $scope.LocationItem;

        if ($scope.IsHub) {
            $rootScope.Message({
                Msg: 'Xác nhận tách chặng?',
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.Split,
                        data: {
                            data: [$scope.SConID], hubID: $scope.LocationItem.ID
                        },
                        success: function (res) {
                            $scope.IsHub = false;
                            $rootScope.IsLoading = false;

                            $rootScope.Message({
                                Msg: 'Thành công!'
                            })
                            winmap.close();
                            openMapV2.Close();
                            openMapV2.Active($scope.indexMap);

                            $scope.SConID = -1;
                            $scope.LocationItem = null;
                            $scope.con_Grid.dataSource.read();
                        }
                    });

                }
            })
        } else {
            switch ($scope.LocationType) {
                case 1: //Start
                    $scope.TripItem.LocationStartID = item.ID;
                    $scope.TripItem.LocationStartName = item.Location;
                    $scope.TripItem.LocationStartLat = item.Lat;
                    $scope.TripItem.LocationStartLng = item.Lng;
                    break;
                case 2: //RomoocGet
                    $scope.TripItem.LocationGetRomoocID = item.ID;
                    $scope.TripItem.LocationGetRomoocName = item.Location;
                    $scope.TripItem.LocationGetRomoocLat = item.Lat;
                    $scope.TripItem.LocationGetRomoocLng = item.Lng;
                    break;
                case 3: //RomoocReturn
                    $scope.TripItem.LocationReturnRomoocID = item.ID;
                    $scope.TripItem.LocationReturnRomoocName = item.Location;
                    $scope.TripItem.LocationReturnRomoocLat = item.Lat;
                    $scope.TripItem.LocationReturnRomoocLng = item.Lng;
                    break;
                case 4: //End
                    $scope.TripItem.LocationEndID = item.ID;
                    $scope.TripItem.LocationEndName = item.Location;
                    $scope.TripItem.LocationEndLat = item.Lat;
                    $scope.TripItem.LocationEndLng = item.Lng;
                    break;
            }

            //Active indexMap
            openMapV2.Active($scope.indexMap);

            $scope.LocationItem = null;
            openMapV2.Close();
            winmap.close();
            win.close();
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV3.URL.Driver_List,
        success: function (res) {
            var data = [];
            $.each(res, function (i, v) {
                data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
            });
            $scope.atcDriverNameOptions.dataSource.data(data);
        }
    });

    $scope.$watch('TripItem.ETD', function () {
        $scope.TripItem.DateGetRomooc = $scope.TripItem.ETD;
        $scope.CheckTrip();
    });
    $scope.$watch('TripItem.ETA', function () {
        $scope.TripItem.DateReturnRomooc = $scope.TripItem.ETA;
        $scope.CheckTrip();
    });
    $scope.$watch('TripItem.RomoocID', function () {
        $scope.CheckTrip();
    });
    $scope.$watch('TripItem.VehicleID', function () {
        $scope.CheckTrip();
    });
    $scope.$watch('TripItem.LocationEndID', function () {
        $scope.RefreshMap();
    });
    $scope.$watch('TripItem.LocationStartID', function () {
        $scope.RefreshMap();
    });
    $scope.$watch('TripItem.LocationGetRomoocID', function () {
        $scope.RefreshMap();
    });
    $scope.$watch('TripItem.LocationReturnRomoocID', function () {
        $scope.RefreshMap();
    });

    $scope.CheckTrip = function () {
        $scope.TripItem.StatusCode = "";
        $scope.TripItem.StatusColor = $scope.Color.None;

        if ($scope.TripItem.VehicleID > 0 && $scope.TripItem.RomoocID > 0 && $scope.TripItem.ETD != null && $scope.TripItem.ETA != null) {
            Common.Log('Trip checing...');

            if ($scope.TripItem.ETD >= $scope.TripItem.ETA) {
                $scope.TripItem.StatusCode = "Thời gian không hợp lệ.";
                $scope.TripItem.StatusColor = $scope.Color.Error;
            }
            else {
                $scope.TripItem.IsCheching = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV3.URL.CheckVehicleAvailable,
                    data: {
                        masterID: $scope.TripItem.ID,
                        vehicleID: $scope.TripItem.VehicleID,
                        romoocID: $scope.TripItem.RomoocID,
                        ETD: $scope.TripItem.ETD,
                        ETA: $scope.TripItem.ETA,
                        Ton: $scope.TripItem.Ton,
                        dataCon: $scope.TripItem.ListOPSCon,
                        dataOPSCon: [],
                        dataORDCon: $scope.TripItem.ListORDCon
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $scope.TripItem.IsCheching = false;
                            $scope.TripItem.DataContainerOffer = res.ListCOContainer;
                            $scope.TripItem.TimeMin = Common.Date.FromJson(res.DateMin);
                            $scope.TripItem.TimeMax = Common.Date.FromJson(res.DateMax);
                            $scope.TripItem.MinInterval = res.HourETAOffer || 0.5;
                            $scope.TripItem.IsAllowChangeRomooc = res.AllowChangeRomooc;
                            if (res.OfferNote != null && res.OfferNote != "") {
                                $scope.TripItem.StatusCode = res.OfferNote;
                                $scope.TripItem.StatusColor = $scope.Color.Error;
                            } else {
                                if ($scope.TripItem.DataContainerOffer == null || $scope.TripItem.DataContainerOffer.length == 0) {
                                    $scope.TripItem.StatusCode = "TG không phù hợp";
                                    $scope.TripItem.StatusColor = $scope.Color.Error;
                                } else if (res.IsOverWeight) {
                                    $scope.TripItem.StatusCode = "Quá trọng tải";
                                    $scope.TripItem.StatusColor = $scope.Color.Error;
                                    $scope.TripItem.HideUpdate = $scope.WeightConstraint;
                                } else {
                                    var tmp = {};
                                    var total = $scope.TripItem.ListORDConName.reduce(function (prev, curr, idx, array) {
                                        if (tmp[curr.OPSContaineID] == null) {
                                            tmp[curr.OPSContaineID] = curr.OPSContaineID;
                                            return prev + curr.Qty;
                                        } else {
                                            return prev;
                                        }
                                    }, 0);
                                    if (total > res.MaxCapacity) {
                                        $scope.TripItem.StatusCode = "Quá SL container.";
                                        $scope.TripItem.StatusColor = $scope.Color.Error;
                                        $scope.TripItem.HideUpdate = true;
                                    } else {
                                        $scope.TripItem.HideUpdate = false;
                                        if (res.IsVehicleAvailable && res.IsRomoocAvailable) {
                                            $scope.TripItem.StatusCode = "Có thể tạo chuyến";
                                            $scope.TripItem.StatusColor = $scope.Color.Success;
                                        } else if (!res.IsVehicleAvailable) {
                                            $scope.TripItem.StatusCode = "Đầu kéo bận.";
                                            $scope.TripItem.StatusColor = $scope.Color.Error;
                                        } else if (!res.IsRomoocAvailable) {
                                            $scope.TripItem.StatusCode = "Romooc bận.";
                                            $scope.TripItem.StatusColor = $scope.Color.Error;
                                        }
                                    }
                                }
                            }
                        })
                    }
                })
            }
        }
    }

    $scope.ResetTrip = function (isLoad) {
        $scope.HasChoose = false;
        $scope.TripItem = {
            ID: -1,
            VendorOfVehicleID: -1,
            VehicleNo: '[Chờ nhập]',
            RomoocNo: '[Chờ nhập]',
            DriverName: '',
            DriverTel: '',
            StatusCode: 'Có thể tạo chuyến',
            StatusColor: $scope.Color.None,
            VehicleID: -1,
            RomoocID: -1,
            ETA: null,
            ETD: null,
            Ton: 0,
            DateGetRomooc: null,
            DateReturnRomooc: null,
            ListORDCon: [],
            ListORDConName: [],
            ListOPSCon: [],
            LocationStartID: -1,
            LocationStartName: '',
            LocationStartLat: null,
            LocationStartLng: null,
            LocationGetRomoocID: -1,
            LocationGetRomoocName: '',
            LocationGetRomoocLat: null,
            LocationGetRomoocLng: null,
            LocationReturnRomoocID: -1,
            LocationReturnRomoocName: '',
            LocationReturnRomoocLat: null,
            LocationReturnRomoocLng: null,
            LocationEndID: -1,
            LocationEndName: '',
            LocationEndLat: null,
            LocationEndLng: null,
            ListLocation: []
        }
        if (isLoad) {
            $scope.con_Grid.dataSource.read();
        }
    }

    //#region Chia chặng

    $scope.IsHub = false;
    $scope.SConID = -1;
    $scope.Container_Split = function ($event, item, win) {
        try {
            $event.preventDefault();
        } catch (e) {
        }

        $scope.SConID = item.ID;
        $scope.IsHub = true;
        win.center().open();

        $timeout(function () {
            if ($scope.popupMap == null) {
                $rootScope.IsLoading = true;
                $scope.popupMap = openMapV2.Init({
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
                fData.push(Common.Request.FilterParamWithAnd('Lat', Common.Request.FilterType.GreaterThan, 0))
                fData.push(Common.Request.FilterParamWithAnd('Lng', Common.Request.FilterType.GreaterThan, 0))
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV3.URL.Location_List,
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

    $scope.Container_Merge = function ($event, item, grid) {
        $event.preventDefault();

        $($event.target).closest('td').find('.btn-merge').hide();
        $($event.target).closest('td').find('.btn-merge-ok').show();
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            $(tr).find('.btn-split').hide();
            if (Common.HasValue(o) && o.ParentID == item.ParentID && o.ID != item.ID) {
                $(tr).find('.btn-merge').hide();
                var chk = $(tr).find('.chk-select-to-merge');
                chk.prop('checked', '');
                chk.show();
            }
        })
    }

    $scope.Container_Merge_OK = function ($event, item, grid) {
        $event.preventDefault();

        var flag = false;
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            if (Common.HasValue(o) && o.ParentID == item.ParentID && o.ID != item.ID) {
                var chk = $(tr).find('.chk-select-to-merge');
                if (chk.prop('checked')) {
                    flag = true;
                }
            }
        })

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận gộp chặng?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.Split_Cancel,
                        data: {
                            conID: item.ParentID
                        },
                        success: function (res) {
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
                $(tr).find('.btn-split').show();
                if (Common.HasValue(o) && o.ParentID == item.ParentID && o.ID != item.ID) {
                    var chk = $(tr).find('.chk-select-to-merge');
                    chk.hide();
                    $(tr).find('.btn-merge').show();
                }
            })
        }
    }

    //#endregion
    
    //#region Tender
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewOnMapV3.URL.Vendor_List,
        success: function (res) {
            _OPSAppointment_COViewOnMapV3.Data.VendorList = res;
            $scope.cboNewTripVendor_Options.dataSource.data(res);
            var data = [];
            Common.Data.Each(res, function (o) {
                if (o.ID > 0)
                    data.push(o);
                if ($scope.VehicleVendorID < 1)
                    $scope.VehicleVendorID = o.ID;
            })
            $scope.cboVehicleVendor_Options.dataSource.data(data);
        }
    });

    //Gủi đối tác
    $scope.Tender_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [], dataCode = [];
        angular.forEach(grid.dataSource.data(), function (item) {
            if (item.IsChoose) {
                data.push(item.OPSContainerID);
            }
        })
        if (data.length > 0 && $scope.ViewOrder) {
            $scope.TripItem.ListOPSCon = data;
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV3.URL.Vendor_WithKPI_List,
                data: { data: data },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $.each(res, function (i, o) {
                            if (i < 3) {
                                o.IsChoose = true;
                                o.RateTime = 2;
                            } else {
                                o.IsChoose = false;
                            }
                            o.Note = "";
                        })
                        $scope.vendor_rate_Grid_Options.dataSource.data(res);
                        $rootScope.IsLoading = false;
                        win.center().open();
                        $timeout(function () {
                            $scope.vendor_rate_Grid.refresh();
                        }, 100)
                    }, function () {
                        $rootScope.IsLoading = false;
                    })
                }
            })
        }
    }

    $scope.TenderRateTime = 0.5;

    $scope.Tender_Accept_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [], idx = 1;
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                o.STT = idx; idx++;
                if (!o.IsManual)
                    o.Debit = 0;
                data.push(o);
            }
        })
        if (Common.HasValue(data) && data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận gửi đối tác đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    var rate = $scope.TenderRateTime;
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.ToVendorKPI,
                        data: { dataCon: $scope.TripItem.ListOPSCon, data: data, rateTime: $scope.TenderRateTime },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' })
                                $scope.ResetTrip(true);
                                win.close();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            })
        } else {
            $rootScope.Message({ Msg: "Vui lòng chọn đối tác!", Type: Common.Message.Type.Alert });
            return;
        }
    }

    $scope.Cancel_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        angular.forEach(grid.dataSource.data(), function (item) {
            if (item.IsChoose) {
                data.push(item.ID);
            }
        })

        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận hủy đơn hàng?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.Cancel,
                        data: {
                            data: data
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
    }

    $scope.cboVendorOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID'
    }

    $scope.vendor_rate_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 20,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' }, IsManual: { type: 'bool' },
                        IsChoose: { type: 'bool', defaultValue: false }
                    }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, columnMenu: false, resizable: false, reorderable: false, sortable: false,
        dataBound: function (e) {
            var grid = this;
            grid.table.kendoSortable({
                filter: ">tbody >tr",
                hint: $.noop,
                ignore: 'input,a',
                cursor: "move",
                placeholder: function (element) {
                    return element.clone().addClass("k-state-hover").css("opacity", 0.65);
                },
                container: ".cus-grid.k-grid tbody",
                change: function (e) {
                    var skip = 1,
                        oldIndex = e.oldIndex + skip,
                        newIndex = e.newIndex + skip,
                        dataItem = grid.dataSource.getByUid(e.item.data("uid"));
                    grid.dataSource.remove(dataItem);
                    grid.dataSource.insert(newIndex - 1, dataItem);
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px', filterable: false, sortable: false, groupable: false, headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,vendor_rate_Grid,vendor_rate_GridChoose_Change)" />',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,vendor_rate_Grid,vendor_rate_GridChoose_Change)" />'
            },
            { field: 'VendorCode', title: 'Mã đối tác', width: '125px', filterable: false, menu: false },
            { field: 'VendorName', title: 'Tên đối tác', width: '200px', filterable: false, menu: false },
            { field: 'Price', title: 'Bảng giá', width: '80px', filterable: false, menu: false },
            { field: 'KPI', title: 'KPI', width: '60px', filterable: false, menu: false },
            { field: 'IsManual', width: '70px', title: 'Nhập giá', template: '<input style="text-align:center" class="chkIsManual" ng-model="dataItem.IsManual" type="checkbox" #= IsManual ? checked="checked" : "" #/>', filterable: false, menu: false },
            { field: 'Debit', width: '100px', title: 'Giá', template: '<input class="k-textbox cus-number txtDebit" ng-disabled="!dataItem.IsManual" value="#=Debit#" style="width:100%"/>', filterable: false, menu: false },
            { field: 'Note', width: '200px', title: 'Ghi chú', template: '<input class="k-textbox cus-number txtNote" value="#=Note#" ng-model="dataItem.Note" style="width:100%"/>', filterable: false, menu: false },
            { title: ' ', filterable: false, menu: false }
        ]
    }
    //#endregion

    //#region KPITender

    $scope.Tender_Suggest_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [], dataCode = [];
        angular.forEach(grid.dataSource.data(), function (item) {
            if (item.IsChoose) {
                data.push(item.OPSContainerID);
            }
        })
        if (data.length > 0 && $scope.ViewOrder) {
            $scope.TripItem.ListORDCon = data;
            $scope.TripItem.ListOPSCon = [];
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_COViewOnMapV3.URL.TenderKPI_List,
                data: { data: data },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $scope.vendor_finance_Grid_Options.dataSource.data(res);
                        $rootScope.IsLoading = false;
                        win.center().open();
                        $timeout(function () {
                            $scope.vendor_finance_Grid.refresh();
                        }, 100)
                    }, function () {
                        $rootScope.IsLoading = false;
                    })
                }
            })
        }
    }

    $scope.vendor_finance_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 20,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' }
                    }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, columnMenu: false, resizable: true, reorderable: false, sortable: true, selectable: true,
        columns: [
            { field: 'STT', width: '50px', title: 'STT', headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }, filterable: false, menu: false },
            { field: 'VendorCode', title: 'Mã đối tác', width: '125px', filterable: false, menu: false },
            { field: 'VendorName', title: 'Tên đối tác', filterable: false, menu: false },
            { field: 'Price', title: 'Bảng giá', width: '100px', filterable: false, menu: false },
            { field: 'KPI', title: 'KPI', width: '100px', filterable: false, menu: false }
        ]
    }

    $scope.Tender_Suggest_Accept_Click = function ($event, grid, win) {
        $event.preventDefault();

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Msg: "Xác nhận gửi đối tác đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.TenderKPI_Save,
                        data: { vendorID: item.VendorID, data: $scope.TripItem.ListORDCon },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })
                                $scope.ResetTrip(true);
                                win.close();
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            })
        } else {
            $rootScope.Message({ Msg: "Vui lòng chọn một đối tác!", Type: Common.Message.Type.Alert });
            return;
        }
    }

    //#endregion

    //#region NewView
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
    $scope.IsVehicleVendor = false;

    //#region Xem chuyến
    $scope.NewTrip_Click = function ($event, grid, win) {
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
                if ($scope.ViewTractor)
                    $scope.tractor_Grid.dataSource.read();
                else
                    $scope.romooc_Grid.dataSource.read();
            } else {
                if ($scope.ViewTractor)
                    $scope.tractorVendor_Grid_Options.dataSource.read();
                else
                    $scope.romoocVendor_Grid_Options.dataSource.read();
            }
        }
    })

    $scope.new_trip_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.COTOContainer_List,
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
                    TOLastUpdateTime: { type: 'date' },
                    TOCreatedDate: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' },
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
                            var btn = "<a href='/' ng-click='NewTripChangeVehicle($event,1,vehicle_map_win)' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'>" + strVeh + " - </a>"
                                + "<a href='/' ng-click='NewTripChangeVehicle($event,2,vehicle_map_win)' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'>" + strRom + "</a>";
                            if (obj.TOStatus == 2) {
                                strTitle = "Đã duyệt";
                                sty = "width:20px;background:blue;display:inline-block;height:16px;position:relative;top:3px;margin-right:7px;";
                                btn = "<span>" + strVeh + " - " + strRom + "</span>";
                            }
                            return "<input style='position:relative;top:2px;' ng-click='onGroupChoose($event,new_trip_Grid)' "
                                + "type='checkbox' class='chkGroupChoose' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'></input>"
                                + "<span style='" + sty + "' title='" + strTitle + "'></span>" + "<span>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + "</span>"
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

    $scope.tooltipTripOptions = {
        filter: ".img-warning", position: "top",
        content: function (e) {
            return $(e.target).data('value');
        }
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
            method: _OPSAppointment_COViewOnMapV3.URL.COTOContainerByTrip_List,
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

    $timeout(function () {
        $scope.new_trip_Grid.resizable.bind("start", function (e) {
            if ($(e.currentTarget).data("th").data("field") == "Check") {
                e.preventDefault();
                setTimeout(function () {
                    $scope.new_trip_Grid.wrapper.removeClass("k-grid-column-resizing");
                    $(document.body).add(".k-grid th").css("cursor", "");
                });
            }
        });
    }, 100)

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
                        method: _OPSAppointment_COViewOnMapV3.URL.ToMon,
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
                        method: _OPSAppointment_COViewOnMapV3.URL.ToOPS,
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
                        method: _OPSAppointment_COViewOnMapV3.URL.Delete,
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

    $scope.NewUpdateDriver_Click = function ($event) {
        $event.preventDefault();

        if ($scope.NewTripViewAction == 0) {
            var data = [], dataApproved = [], grid = $scope.new_trip_Grid;
            Common.Data.Each(grid.tbody.find('.k-grouping-row .chkGroupChoose'), function (o) {
                var val = $(o).prop('checked'), obj = $(o).data('item');
                if (Common.HasValue(obj) && val) {
                    data.push(obj.TOMasterID);
                }
            })
            $rootScope.Message({
                Msg: "Xác nhận cập nhật tài xế cho các chuyến đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update_Driver,
                        data: { data: data },
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

    $scope.vehicleMap = null;
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
            $timeout(function () {
                $scope.IsHub = false;
                if ($scope.vehicleMap == null) {
                    $rootScope.IsLoading = true;
                    $scope.vehicleMap = openMapV2.Init({
                        Zoom: 14,
                        Element: 'vehiclemap',
                        Tooltip_Show: true,
                        Tooltip_Element: 'vehiclemap_tooltip',
                        InfoWin_Show: false,
                        ClickMarker: function (i, e) {
                            if ($scope.ChangeVehicleType == 1) {
                                var grid = $scope.vehMap_Grid;
                                angular.forEach(grid.items(), function (tr) {
                                    var item = grid.dataItem(tr);
                                    if (Common.HasValue(item) && item.ID == i.Item.ID) {
                                        grid.select(tr);
                                    }
                                });
                            } else {
                                var grid = $scope.romMap_Grid;
                                angular.forEach(grid.items(), function (tr) {
                                    var item = grid.dataItem(tr);
                                    if (Common.HasValue(item) && item.ID == i.Item.ID) {
                                        grid.select(tr);
                                    }
                                });
                            }
                        },
                        DefinedLayer: [{
                            Name: 'VectorMarkerVEH',
                            zIndex: 100
                        }, {
                            Name: 'VectorMarkerROM',
                            zIndex: 99
                        }]
                    });
                    openMapV2.Active($scope.vehicleMap);
                } else {
                    openMapV2.Active($scope.vehicleMap);
                }
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
        }
    }

    $scope.vehMap_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.Tractor_List,
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
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                if (obj.Lat > 0 && obj.Lng > 0) {
                    openMapV2.Center(obj.Lat, obj.Lng);
                }
            }
        },
        dataBound: function () {
            var grid = this;
            if ($scope.IsVehicleMapActived && $scope.ChangeVehicleType == 1) {
                openMapV2.ClearVector("VectorMarkerROM");
                openMapV2.ClearVector("VectorMarkerVEH");
                angular.forEach(grid.items(), function (tr) {
                    if (!$scope.NewTimeLineDetail) $(tr).addClass('unselectable');
                    var item = grid.dataItem(tr);
                    if (Common.HasValue(item) && item.Lat > 0 && item.Lng > 0) {
                        var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Tractor), 1);
                        openMapV2.NewMarker(item.Lat, item.Lng, item.Regno, item.Regno, icon, {
                            Item: item, Type: 'Tractor'
                        }, "VectorMarkerVEH");
                        if ($scope.NewTimeLineVehicleItem.ID == item.ID) {
                            grid.select(tr);
                            openMapV2.Center(item.Lat, item.Lng);
                        }
                    }
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
            method: _OPSAppointment_COViewOnMapV3.URL.Romooc_List,
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
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                if (obj.Lat > 0 && obj.Lng > 0) {
                    openMapV2.Center(obj.Lat, obj.Lng);
                }
            }
        },
        dataBound: function () {
            var grid = this;
            if ($scope.IsVehicleMapActived && $scope.ChangeVehicleType == 2) {
                openMapV2.ClearVector("VectorMarkerROM");
                openMapV2.ClearVector("VectorMarkerVEH");
                angular.forEach(grid.items(), function (tr) {
                    if (!$scope.NewTimeLineDetail) $(tr).addClass('unselectable');
                    var item = grid.dataItem(tr);
                    if (Common.HasValue(item) && item.Lat > 0 && item.Lng > 0) {
                        var img = Common.String.Format(openMapV2.NewImage.Romooc_20);
                        if (item.Group == 2)
                            img = Common.String.Format(openMapV2.NewImage.Romooc_40);
                        var icon = openMapV2.NewStyle.Icon(img, 1);
                        openMapV2.NewMarker(item.Lat, item.Lng, item.Regno, item.Regno, icon, {
                            Item: item, Type: 'Romooc'
                        }, "VectorMarkerROM");
                        if ($scope.NewTimeLineVehicleItem.ID == item.ID) {
                            grid.select(tr);
                            openMapV2.Center(item.Lat, item.Lng);
                        }
                    }
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
                                    method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update_TimeLine,
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
                                    method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update_TimeLine,
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
                                method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_ChangeVehicle,
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
                                method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_ChangeVehicle,
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

    //#endregion

    //#region 2View
    $scope.NewTripDetail = false;

    $scope.new2view_con_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.Order_List,
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
                    Ton: { type: 'number' }
                }
            }
        }),
        pageable: Common.PageSize, height: '99%', groupable: false, sortable: true, columnMenu: false,
        filterable: { mode: 'row', visible: false }, reorderable: false, selectable: true, resizable: true,
        columns: [
            {
                template: '<form class="cus-form-enter" ng-submit="NewTOMasterEnter_Click($event)"><input kendo-numeric-text-box value="0" data-k-min="0" k-options="txtGroupEnter2View_Options" style="width:100%;text-align:center;" /></form>',
                sortable: false, filterable: false, menu: false, width: '75px', title: 'S.Chuyến'
            },
            {
                field: 'Command', title: ' ', width: '35px',
                attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button" title="Xem chặng" href="/" ng-click="ContainerByTrip_Click($event,dataItem,container_by_trip_win,1)"><i class="fa fa-th"></i></a>',
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
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.new2view_trip_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Container_List,
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
                    TOLastUpdateTime: { type: 'date' },
                    TOCreatedDate: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true,
        reorderable: false, sortable: true, filterable: { mode: 'row' }, selectable: true,
        change: function (e) {
            var grid = this;
            var obj = grid.dataItem(grid.select());
            if (Common.HasValue(obj) && obj.OrderGroupProductID > 0) {
                $timeout(function () {
                    $scope.new2view_con_Grid.clearSelection();
                    $scope.notification.hide();
                    var flag = false, ton = 0, cbm = 0, qty = 0;
                    Common.Data.Each($scope.new2view_con_Grid.items(), function (tr) {
                        var item = $scope.new2view_con_Grid.dataItem(tr);
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
                    $scope.new2view_con_Grid.clearSelection();
                    $scope.notification.hide();
                })
            }
        },
        columns: [
            {
                title: ' ', width: '35px', attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button" title="Xóa container khỏi chuyến" href="/" ng-click="NewTOMasterContainerDelete_Click($event,dataItem,new2view_trip_Grid)"><i class="fa fa-minus"></i></a>',
                filterable: false, sortable: false, groupable: false
            },
            {
                field: 'TOMasterCode', width: '100px', title: 'Mã chuyến',
                template: "<a href='/' ng-click='ContainerByTrip_Click($event,dataItem,container_by_trip_win)'>#=TOMasterCode#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        if (Common.HasValue(obj)) {
                            var strRom = obj.TORomoocNo == "" || obj.TORomoocNo == null ? "[Chưa nhập]" : obj.TORomoocNo;
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;

                            return "<a href='#' class='btn2ViewTrip' ng-click='NewTOMasterInfo_Click($event,new2view_trip_Grid,new_trip_info_win)' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'>Chuyến " + obj.TOMasterIndex + "</a>"
                                + "<span class='txtGroup2View' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'> - " + obj.TOVendorCode + " - " + strVeh + " - " + strRom + " - " + strName + " - " + strTel
                                + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
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
                                DriverName: "",
                                DriverTel: "",
                                StatusCode: 'Chưa chọn xe',
                                StatusColor: $scope.Color.Error,
                                VehicleID: "",
                                VehicleNo: "",
                                RomoocID: "",
                                RomoocNo: "",
                                VendorOfVehicleID: -1,
                                Ton: dataItem.Ton,
                                Status: 1,
                                IsRomoocBreak: $scope.RomoocMustReturn,
                                ETA: dataItem.ETA,
                                ETD: dataItem.ETD,
                                DateGetRomooc: null,
                                DateReturnRomooc: null,
                                ListORDCon: [dataItem.ID],
                                ListORDConName: [],
                                ListOPSCon: [],
                                LocationStartID: _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartID,
                                LocationStartName: _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartName,
                                LocationEndID: _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndID,
                                LocationEndName: _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndName,
                                LocationStartLat: _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartLat,
                                LocationStartLng: _OPSAppointment_COViewOnMapV3.Data.Location.LocationStartLng,
                                LocationEndLat: _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndLat,
                                LocationEndLng: _OPSAppointment_COViewOnMapV3.Data.Location.LocationEndLng,
                                DateGetRomooc: dataItem.ETD,
                                DateReturnRomooc: null,
                                LocationGetRomoocID: _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocID,
                                LocationGetRomoocName: _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocName,
                                LocationGetRomoocLat: _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLat,
                                LocationGetRomoocLng: _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLng,
                                LocationReturnRomoocID: -1,
                                LocationReturnRomoocName: "",
                                LocationReturnRomoocLat: null,
                                LocationReturnRomoocLng: null,
                                ListLocation: dataItem.ListLocation
                            }

                            if ($scope.RomoocMustReturn == true) {
                                $scope.NewTripItem.LocationReturnRomoocID = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocID;
                                $scope.NewTripItem.LocationReturnRomoocName = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocName;
                                $scope.NewTripItem.LocationReturnRomoocLat = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLat;
                                $scope.NewTripItem.LocationReturnRomoocLng = _OPSAppointment_COViewOnMapV3.Data.Location.LocationRomoocLng;
                            }

                            $scope.NewTripDetail = true;
                            if ($scope.NewTripItem.VendorOfVehicleID == null)
                                $scope.NewTripItem.VendorOfVehicleID = -1;
                            //$scope.new_trip_info_win.setOptions({ height: 550 });
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
                    $rootScope.Message({
                        Msg: "Xác nhận thêm container vào chuyến " + val + "?",
                        Type: Common.Message.Type.Confirm,
                        Ok: function () {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update_Container,
                                data: {
                                    mID: toMasterID, data: [dataItem.OPSContaineID], isRemove: false
                                },
                                success: function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    $scope.new2view_con_Grid.dataSource.read();
                                    $scope.new2view_trip_Grid.dataSource.read();
                                }
                            });
                        },
                        Close: function () {
                            txt.value(0);
                        }
                    })
                }
            }
        }
    }

    $scope.notificationOptions = {
        appendTo: '#newtripwin', button: true, hideOnClick: true, autoHideAfter: 30000, width: 400
    }

    $scope.NewTOMasterContainerDelete_Click = function ($event, item, grid) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update_Check4Delete,
            data: { mID: item.TOMasterID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.IsLoading = false;
                    if (res == true) {
                        $rootScope.Message({
                            Msg: "Xác nhận xóa chuyến?",
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_COViewOnMapV3.URL.Delete,
                                    data: { data: [item.TOMasterID] },
                                    success: function (res) {
                                        $scope.ChangeData = true;
                                        $rootScope.IsLoading = false;
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        $scope.new2view_con_Grid.dataSource.read();
                                        $scope.new2view_trip_Grid.dataSource.read();
                                    }
                                });
                            }
                        });
                    } else {
                        $rootScope.Message({
                            Msg: "Xác nhận xóa container khỏi chuyến?",
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update_Container,
                                    data: { mID: item.TOMasterID, data: [item.ID], isRemove: true },
                                    success: function (res) {
                                        $scope.ChangeData = true;
                                        $rootScope.IsLoading = false;
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        $scope.new2view_con_Grid.dataSource.read();
                                        $scope.new2view_trip_Grid.dataSource.read();
                                    }
                                });
                            }
                        });
                    }
                }, function () {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Có lỗi xảy ra. Vui lòng thử lại sau!' });
                    txt.value(dataItem.Ton);
                })
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
                method: _OPSAppointment_COViewOnMapV3.URL.TripByID,
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
                            RomoocID: res.RomoocID,
                            RomoocNo: res.RomoocNo,
                            VendorOfVehicleID: res.VendorOfVehicleID,
                            Ton: res.TotalTon,
                            Status: 1,
                            IsRomoocBreak: $scope.RomoocMustReturn,
                            ETA: Common.Date.FromJson(res.ETA),
                            ETD: Common.Date.FromJson(res.ETD),
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
                            win.setOptions({ height: 345 });
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

    $scope.cboNewTripRomooc_Options = {
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
        $scope.CheckNewTrip("ETD");
    });
    $scope.$watch('NewTripItem.ETA', function () {
        $scope.CheckNewTrip();
    });
    $scope.$watch('NewTripItem.VehicleID', function () {
        $scope.CheckNewTrip();
    });
    $scope.$watch('NewTripItem.RomoocID', function () {
        $scope.CheckNewTrip();
    });
    $scope.$watch('NewTripItem.VendorOfVehicleID', function (nval, oval) {
        Common.Log("NewTripItem.VendorOfVehicleID")
        if ((nval == -1 || nval == null) && Common.HasValue($scope.NewTripItem) && $scope.NewTripItem.ID < 1) {
            //$scope.new_trip_info_win.setOptions({ height: 550 })
        } else {
            //$scope.new_trip_info_win.setOptions({ height: 345 })
        }
        if ($scope.NewTripDetail) {
            $scope.NewTripItem.DriverTel = '';
            $scope.NewTripItem.DriverName = '';
            $scope.LoadDataNewTrip(true);
        }
    });

    $scope.CheckNewTrip = function (props) {
        Common.Log("CheckNewTrip")
        if ($scope.NewTripDetail && $scope.NewTripItem != null && $scope.NewTripItem.Status == 1) {
            $scope.NewTripItem.StatusCode = "";
            $scope.NewTripItem.StatusColor = $scope.Color.None;
            //Trường hợp xe vendor không cần kiểm tra.
            if (props == "ETD" && $scope.NewTripItem.ETD != null && $scope.NewTripItem.ETA != null) {
                if ($scope.NewTripItem.ETD >= $scope.NewTripItem.ETA || $scope.NewTripItem.ETD.addDays(1 / 48) > $scope.NewTripItem.ETA) {
                    $scope.NewTripItem.ETA = $scope.NewTripItem.ETD.addDays(1 / 48);
                }
            }
            if ($scope.NewTripItem.VendorOfVehicleID == -1 && $scope.NewTripItem.VehicleID > 0 && $scope.NewTripItem.RomoocID > 0 && $scope.NewTripItem.ETD != null && $scope.NewTripItem.ETA != null) {
                Common.Log('Trip checing...');

                if ($scope.NewTripItem.ETD >= $scope.NewTripItem.ETA) {
                    $scope.NewTripItem.StatusCode = "Tg không hợp lệ.";
                    $scope.NewTripItem.StatusColor = $scope.Color.Error;
                }
                else {
                    $scope.NewTripItem.IsCheching = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewOnMapV3.URL.CheckVehicleAvailable,
                        data: {
                            romoocID: $scope.NewTripItem.RomoocID,
                            vehicleID: $scope.NewTripItem.VehicleID,
                            masterID: $scope.NewTripItem.ID,
                            ETD: $scope.NewTripItem.ETD,
                            ETA: $scope.NewTripItem.ETA,
                            Ton: $scope.NewTripItem.TotalTon || 0,
                            dataCon: [],
                            dataOPSCon: [],
                            dataORDCon: []
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.NewTripItem.IsCheching = false;
                                $scope.NewTripItem.IsCheching = false;
                                $scope.NewTripItem.IsCheching = false;
                                $scope.NewTripItem.DataContainerOffer = res.ListCOContainer;
                                $scope.NewTripItem.TimeMin = Common.Date.FromJson(res.DateMin);
                                $scope.NewTripItem.TimeMax = Common.Date.FromJson(res.DateMax);
                                $scope.NewTripItem.MinInterval = res.HourETAOffer || 0.5;
                                $scope.NewTripItem.IsAllowChangeRomooc = res.AllowChangeRomooc;
                                if (res.OfferNote != null && res.OfferNote != "") {
                                    $scope.NewTripItem.StatusCode = res.OfferNote;
                                    $scope.NewTripItem.StatusColor = $scope.Color.Error;
                                } else {
                                    if ($scope.NewTripItem.DataContainerOffer == null || $scope.NewTimeLineItem.DataContainerOffer.length == 0) {
                                        $scope.NewTripItem.StatusCode = "TG không phù hợp";
                                        $scope.NewTripItem.StatusColor = $scope.Color.Error;
                                    } else if (res.IsOverWeight) {
                                        $scope.NewTripItem.StatusCode = "Quá trọng tải";
                                        $scope.NewTripItem.StatusColor = $scope.Color.Warning;
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
            method: _OPSAppointment_COViewOnMapV3.URL.VehicleVendor_List,
            data: {
                vendorID: $scope.NewTripItem.VendorOfVehicleID, request: '', typeofvehicle: 1
            },
            success: function (res) {
                $timeout(function () {
                    $scope.cboNewTripVehicle_Options.dataSource.data(res.Data);
                }, 10)
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_COViewOnMapV3.URL.VehicleVendor_List,
                    data: {
                        vendorID: $scope.NewTripItem.VendorOfVehicleID, request: '', typeofvehicle: 2
                    },
                    success: function (res) {
                        $timeout(function () {
                            $scope.cboNewTripRomooc_Options.dataSource.data(res.Data);
                        }, 10)
                        $timeout(function () {
                            if (isNew) {
                                if (Common.HasValue(res.Data[0]))
                                    $scope.NewTripItem.RomoocID = res.Data[0].ID;
                                else
                                    $scope.NewTripItem.RomoocID = null;
                            }
                            $rootScope.IsLoading = false;
                        }, 100)
                    }
                });
                $timeout(function () {
                    if (isNew) {
                        if (Common.HasValue(res.Data[0]))
                            $scope.NewTripItem.VehicleID = res.Data[0].ID;
                        else
                            $scope.NewTripItem.VehicleID = null;
                    }
                }, 100)
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.DriverVendor_List,
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
                            method: _OPSAppointment_COViewOnMapV3.URL.Save,
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
                                    $scope.new2view_con_Grid.dataSource.read();
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
                            method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update,
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
                                        $scope.new2view_con_Grid.dataSource.read();
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
                    method: _OPSAppointment_COViewOnMapV3.URL.Delete,
                    data: {
                        data: [$scope.NewTripItem.ID]
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $scope.ChangeData = true;
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!' });
                            if ($scope.NewTripViewAction == 1) {
                                $scope.new2view_con_Grid.dataSource.read();
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
            method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Container_List,
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
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true,
        reorderable: false, sortable: true, filterable: { mode: 'row' }, selectable: true,
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
                field: 'TOMasterCode', width: '100px', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        if (Common.HasValue(obj)) {
                            var strRom = obj.TORomoocNo == "" || obj.TORomoocNo == null ? "[Chưa nhập]" : obj.TORomoocNo;
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            return "<input style='position:relative;top:2px;' ng-click='onGroupChoose($event,new2view_trip_select_Grid)' "
                                + "type='checkbox' class='chkGroupChoose' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'></input>"
                                + "<span>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + strVeh + " - " + strRom + " - " + strName + " - " + strTel
                                + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + " </span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
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
    //#endregion

    //#region NewTimeLine
    $scope.IsNewTimeLineBound = false;
    $scope.NewTimeLineToMonAvailable = false;
    $scope.NewTimeLineToOpsAvailable = false;

    $scope.NewTripTimeLineEdit = {};
    $scope.NewTimeLineTripItem = {};
    $scope.NewTimeLineEvent_HasClick = false;
    $scope.NewTimeLineVehicleData = [];
    $scope.NewTimeLineVehicleDataTemp = [];

    $scope.new_timeline_TripOptions = {
        date: new Date().addDays(-1), footer: false, snap: false,
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
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
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
                    value: '', text: 'Không có DL'
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
                        method: _OPSAppointment_COViewOnMapV3.URL.CO2View_Master_Update_TimeLine,
                        data: { mID: obj.id, vehicleID: parseInt(obj.field) || parseInt(obj.field[0]), isTractor: true, ETD: obj.start, ETA: obj.end },
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
                        },
                        error: function () {
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
            method: _OPSAppointment_COViewOnMapV3.URL.VehicleVendor_List,
            readparam: function () { return { vendorID: -1, typeofvehicle: 1 } },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            }
        }),
        pageable: false, height: '99%', groupable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if ($scope.IsShowNewTrip) {
                    if (Common.HasValue(item) && $scope.NewTimeLineVehicleData.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('td input.chkChoose').prop('checked', true);
                        if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
                    }
                } else if ($scope.IsShowNewTimeLine) {
                    if (Common.HasValue(item) && $scope.NewTimeLineDragDropVehicleData.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('td input.chkChoose').prop('checked', true);
                        if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
                    }
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
        if ($scope.IsShowNewTrip) {
            $scope.NewTimeLineVehicleData = [];
            if ($event.target.checked == true) {
                grid.items().each(function () {
                    var tr = this, item = grid.dataItem(tr);
                    $scope.NewTimeLineVehicleData.push(item.ID);
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
        } else if ($scope.IsShowNewTimeLine) {
            $scope.NewTimeLineDragDropVehicleData = [];
            if ($event.target.checked == true) {
                grid.items().each(function () {
                    var tr = this, item = grid.dataItem(tr);
                    $scope.NewTimeLineDragDropVehicleData.push(item.ID);
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
    }

    $scope.NewTimeLineVehicleSelect_Choose = function ($event, grid) {
        var tr = $($event.target).closest('tr'), item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
            if ($scope.IsShowNewTrip) {
                $scope.NewTimeLineVehicleData.push(item.ID);
            } else if ($scope.IsShowNewTimeLine) {
                $scope.NewTimeLineDragDropVehicleData.push(item.ID);
            }
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
            if ($scope.IsShowNewTrip) {
                if ($scope.NewTimeLineVehicleData.indexOf(item.ID) > -1) {
                    $scope.NewTimeLineVehicleData.splice($scope.NewTimeLineVehicleData.indexOf(item.ID), 1)
                }
            } else if ($scope.IsShowNewTimeLine) {
                if ($scope.NewTimeLineDragDropVehicleData.indexOf(item.ID) > -1) {
                    $scope.NewTimeLineDragDropVehicleData.splice($scope.NewTimeLineDragDropVehicleData.indexOf(item.ID), 1)
                }
            }
        }
    }

    $scope.NewTimeLineVehicleSelect_Click = function ($event, grid, win) {
        $event.preventDefault();
        grid.dataSource.read();
        win.center().open();
        if ($scope.IsShowNewTrip) {
            $scope.NewTimeLineVehicleDataTemp = $.extend(true, [], $scope.NewTimeLineVehicleData);
        } else if ($scope.IsShowNewTimeLine) {
            $scope.NewTimeLineDragDropVehicleDataTemp = $.extend(true, [], $scope.NewTimeLineDragDropVehicleData);
        }
    }

    $scope.NewTimeLineVehicleSelect_OK_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.IsShowNewTrip) {
            if ($scope.NewTimeLineVehicleData.sort().toString() != $scope.NewTimeLineVehicleDataTemp.sort().toString()) {
                $scope.NewTimeLineVehicleDataTemp = $.extend(true, [], $scope.NewTimeLineVehicleData);
                $scope.LoadNewTimeLineData();
            }
        } else if ($scope.IsShowNewTimeLine) {
            if ($scope.NewTimeLineDragDropVehicleData.sort().toString() != $scope.NewTimeLineDragDropVehicleDataTemp.sort().toString()) {
                $scope.NewTimeLineDragDropVehicleDataTemp = $.extend(true, [], $scope.NewTimeLineDragDropVehicleData);
                $scope.LoadNewTimeLineDragDropData();
            }
        }
        win.close();
    }

    $scope.LoadNewTimeLineData = function () {
        $scope.IsNewTimeLineBound = false;
        $scope.NewViewTripLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewOnMapV3.URL.Schedule_Data,
            data: { data: $scope.NewTimeLineVehicleData },
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
                method: _OPSAppointment_COViewOnMapV3.URL.TripByID,
                data: { masterID: uid },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $scope.NewTripItem = {
                            ID: res.ID,
                            Code: res.Code,
                            VehicleNo: res.VehicleNo,
                            RomoocNo: res.RomoocNo,
                            DriverName: res.DriverName,
                            DriverTel: res.DriverTel,
                            StatusCode: 'Có thể cập nhật',
                            StatusColor: $scope.Color.None,
                            VehicleID: res.VehicleID,
                            RomoocID: res.RomoocID,
                            VendorOfVehicleID: res.VendorOfVehicleID,
                            Ton: res.TotalTon,
                            Status: obj.Status,
                            IsRomoocBreak: $scope.RomoocMustReturn,
                            ETA: Common.Date.FromJson(res.ETA),
                            ETD: Common.Date.FromJson(res.ETD),
                            DateGetRomooc: Common.Date.FromJson(res.DateGetRomooc),
                            DateReturnRomooc: Common.Date.FromJson(res.DateReturnRomooc),
                            ListORDCon: [],
                            ListORDConName: [],
                            ListOPSCon: [],
                            LocationStartID: res.LocationStartID,
                            LocationStartName: res.LocationStartName,
                            LocationStartLat: res.LocationStartLat,
                            LocationStartLng: res.LocationStartLng,
                            LocationEndID: res.LocationEndID,
                            LocationEndName: res.LocationEndName,
                            LocationEndLat: res.LocationEndLat,
                            LocationEndLng: res.LocationEndLng,
                            LocationGetRomoocID: res.LocationGetRomoocID,
                            LocationGetRomoocName: res.LocationGetRomoocName,
                            LocationGetRomoocLat: res.LocationGetRomoocLat,
                            LocationGetRomoocLng: res.LocationGetRomoocLng,
                            LocationReturnRomoocID: res.LocationReturnRomoocID,
                            LocationReturnRomoocName: res.LocationReturnRomoocName,
                            LocationReturnRomoocLat: res.LocationReturnRomoocLat,
                            LocationReturnRomoocLng: res.LocationReturnRomoocLng,
                            ListLocation: res.ListLocation
                        }
                        if ($scope.NewTripItem.VendorOfVehicleID == null) {
                            $scope.NewTripItem.VendorOfVehicleID = -1;
                        }
                        $scope.LoadDataNewTrip();
                        $timeout(function () {
                            $scope.NewTripDetail = true;
                            $scope.CheckNewTrip();
                            win.setOptions({ height: 345 });
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
                        method: _OPSAppointment_COViewOnMapV3.URL.Delete,
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
                        method: _OPSAppointment_COViewOnMapV3.URL.ToMon,
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
                        method: _OPSAppointment_COViewOnMapV3.URL.ToOPS,
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
            $scope.new2view_con_Grid.dataSource.read();
            $scope.new2view_trip_Grid.dataSource.read();
        }
    }

    $scope.NewTripViewDate_Cancel_Click = function ($event) {
        $event.preventDefault();

        $scope.NewViewTripDate = false;
        $scope.ShowNewTripDate = false;
        $scope.NewTripDateRequest = { fDate: null, tDate: null };
        if ($scope.NewTripViewAction == 0) {
            $scope.new_trip_Grid.dataSource.read();
        } else if ($scope.NewTripViewAction == 1) {
            $scope.new2view_con_Grid.dataSource.read();
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
                    $scope.new2view_con_Grid.dataSource.read();
                    $scope.new2view_trip_Grid.dataSource.read();
                } else {
                    $scope.new2view_trip_Grid.dataSource.read();
                }
            }
            else {
                $scope.NewViewDataSelect = [];
                $scope.NewTripViewAction = 1;
                $scope.new2view_con_Grid.dataSource.read();
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

    //Chi tiết chuyến hiện tại của phương tiện
    $scope.Vehicle_Trip_Click = function ($event, win) {
        $event.preventDefault();
    }

    //Chi tiết chuyến tiếp theo của phương tiện
    $scope.Vehicle_FindNextTrip_Click = function ($event, win) {
        $event.preventDefault();
    }

    //Đóng infowin.
    $scope.InfoClose_Click = function ($event) {
        $event.preventDefault();

        openMapV2.Close();
    }

    //Chọn phương tiện trên map
    $scope.VehicleChoose_Click = function ($event) {
        $event.preventDefault();

        if ($scope.IsTractor == false) {
            $scope.TripItem.RomoocID = $scope.VehicleItem.ID;
            $scope.TripItem.RomoocNo = $scope.VehicleItem.Regno;
        } else {
            $scope.TripItem.VehicleID = $scope.VehicleItem.ID;
            $scope.TripItem.VehicleNo = $scope.VehicleItem.Regno;
            if ($scope.VehicleItem.RomoocID > 0) {
                $scope.TripItem.VehicleID = $scope.VehicleItem.RomoocID;
                $scope.TripItem.VehicleNo = $scope.VehicleItem.RomoocNo;
            }
        }
        openMapV2.Close();
    }

    $scope.ConvertLatLng = function (value) {
        var str = parseInt(value, 10);
        var deg = value - str;
        deg = Math.round(deg * 60);

        return str + "*" + deg;
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
    
    //#region ConfigView
    $scope.Setting_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
    }

    $scope.ConfigView_ShowMap_Click = function ($event) {
        $event.preventDefault();

        $scope.ConfigView.showMap = !$scope.ConfigView.showMap;
        var panel = $($scope.viewSplitter.element).find(".k-pane:first");
        if ($scope.ConfigView.showMap) {
            $($scope.viewSplitter.element).find('.k-splitbar').show();
            $scope.viewSplitter.expand(panel);
        } else {
            $($scope.viewSplitter.element).find('.k-splitbar').hide();
            $scope.viewSplitter.collapse(panel);
        }
        $timeout(function () {
            openMapV2.hasMap = $scope.ConfigView.showMap;
            if ($scope.indexMap == null) {
                $scope.indexMap = openMapV2.Init({
                    Element: 'map',
                    Tooltip_Show: true,
                    Tooltip_Element: 'map_tooltip',
                    InfoWin_Show: true,
                    InfoWin_Element: 'map_info_win',
                    ClickMap: function () {
                        openMapV2.Close();
                    },
                    ClickMarker: function (i, o) {
                        switch (i.Type) {
                            case 'Romooc':
                                $scope.LocationItem = null;
                                $scope.VehicleItem = i.Item;
                                $scope.IsTractor = false;
                                $scope.ViewTractor = false;

                                Common.Data.Each($scope.romooc_Grid.items(), function (tr) {
                                    var item = $scope.romooc_Grid.dataItem(tr);
                                    $(tr).removeClass('k-state-selected');
                                    if (Common.HasValue(item) && item.ID == $scope.VehicleItem.ID) {
                                        $(tr).addClass('k-state-selected');
                                    }
                                })
                                break;
                            case 'Tractor':
                                $scope.LocationItem = null;
                                $scope.VehicleItem = i.Item;
                                $scope.IsTractor = true;
                                $scope.ViewTractor = true;
                                $timeout(function () {
                                    $scope.tractor_Grid.resize();
                                }, 200);
                                break;
                            case 'Location':
                                $scope.VehicleItem = null;
                                $scope.LocationItem = i.Item;
                                break;
                            default:
                                openMapV2.Close();
                                break;
                        }
                    },
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
                        Name: 'VectorMarkerTO',
                        zIndex: 100
                    }, {
                        Name: 'VectorRouteORD',
                        zIndex: 90
                    }, {
                        Name: 'VectorRouteTO',
                        zIndex: 90
                    }]
                });
            }
        }, 1000)
        var objSetting = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
        if (objSetting == null) {
            objSetting = {
                DefaultFunctionID: 0, DefaultKey: '', Grids: [],
                ReferID: $rootScope.FunctionItem.ID, ReferKey: $rootScope.FunctionItem.Code,
                Options: {
                    'OPSShowMap': $scope.ConfigView.showMap,
                    'OPSShowGrid': $scope.ConfigView.showGrid
                }
            }
        } else {
            if (objSetting.Options == null) {
                objSetting.Options = {
                    'OPSShowMap': $scope.ConfigView.showMap,
                    'OPSShowGrid': $scope.ConfigView.showGrid
                }
            } else {
                objSetting.Options["OPSShowMap"] = $scope.ConfigView.showMap;
                objSetting.Options["OPSShowGrid"] = $scope.ConfigView.showGrid;
            }
        }
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: 'App_UserOptionsSetting_Save',
            data: {
                referID: $rootScope.FunctionItem.ID,
                referKey: $rootScope.FunctionItem.Code,
                options: objSetting.Options
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    //Nếu có thiết lập => Replace thiết lập cũ.
                    if (Common.HasValue($rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code])) {
                        var objSetting = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
                        if (objSetting.Options == null) {
                            objSetting.Options = {
                                'OPSShowMap': $scope.ConfigView.showMap,
                                'OPSShowGrid': $scope.ConfigView.showGrid
                            }
                        }
                    }
                        //Thêm mới thiết lập.
                    else {
                        $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code] = {
                            DefaultFunctionID: 0,
                            DefaultKey: '',
                            Options: {
                                'OPSShowMap': $scope.ConfigView.showMap,
                                'OPSShowGrid': $scope.ConfigView.showGrid
                            },
                            ReferID: $rootScope.FunctionItem.ID,
                            ReferKey: $rootScope.FunctionItem.Code
                        }
                    }
                })
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.ConfigView_ShowGrid_Click = function ($event) {
        $event.preventDefault();
        $scope.ConfigView.showGrid = !$scope.ConfigView.showGrid;
        var panel = $($scope.conSplitter.element).find(".k-pane:last");
        if ($scope.ConfigView.showGrid) {
            $($scope.conSplitter).find('.k-splitbar:last').show();
            $scope.conSplitter.expand(panel);
        } else {
            $($scope.conSplitter).find('.k-splitbar:last').hide();
            $scope.conSplitter.collapse(panel);
        }
        var objSetting = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
        if (objSetting == null) {
            objSetting = {
                DefaultFunctionID: 0, DefaultKey: '', Grids: [],
                ReferID: $rootScope.FunctionItem.ID, ReferKey: $rootScope.FunctionItem.Code,
                Options: {
                    'OPSShowMap': $scope.ConfigView.showMap,
                    'OPSShowGrid': $scope.ConfigView.showGrid
                }
            }
        } else {
            if (objSetting.Options == null) {
                objSetting.Options = {
                    'OPSShowMap': $scope.ConfigView.showMap,
                    'OPSShowGrid': $scope.ConfigView.showGrid
                }
            } else {
                objSetting.Options["OPSShowMap"] = $scope.ConfigView.showMap;
                objSetting.Options["OPSShowGrid"] = $scope.ConfigView.showGrid;
            }
        }
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: 'App_UserOptionsSetting_Save',
            data: {
                referID: $rootScope.FunctionItem.ID,
                referKey: $rootScope.FunctionItem.Code,
                options: objSetting.Options
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    //Nếu có thiết lập => Replace thiết lập cũ.
                    if (Common.HasValue($rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code])) {
                        var objSetting = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
                        if (objSetting.Options == null) {
                            objSetting.Options = {
                                'OPSShowMap': $scope.ConfigView.showMap,
                                'OPSShowGrid': $scope.ConfigView.showGrid
                            }
                        }
                    }
                        //Thêm mới thiết lập.
                    else {
                        $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code] = {
                            DefaultFunctionID: 0,
                            DefaultKey: '',
                            Options: {
                                'OPSShowMap': $scope.ConfigView.showMap,
                                'OPSShowGrid': $scope.ConfigView.showGrid
                            },
                            ReferID: $rootScope.FunctionItem.ID,
                            ReferKey: $rootScope.FunctionItem.Code
                        }
                    }
                })
                $rootScope.IsLoading = false;
            }
        })
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
                    openMapV2.Active($scope.indexMap);
                    break;
                case 'VehicleMAP':
                    openMapV2.Active($scope.indexMap);
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
                openMapV2.Active($scope.indexMap);
                break;
            case 'VehicleMAP':
                openMapV2.Active($scope.indexMap);
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
        }
    }

    $scope.On_Resize = function (code) {
        switch (code) {
            case 'TimeLineVehicleMAP':
                openMapV2.Resize();
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
        openMapV2.Resize();
    }

    $scope.ViewB_Click = function ($event) {
        $event.preventDefault();

        $scope.IsFullScreen = false;
        angular.element('.mainform .mainmenu').removeClass('fullscreen');
        angular.element('#2view').removeClass('fullscreen');
        angular.element('#2view').resize();
        $scope.viewSplitter.resize();
        openMapV2.Resize();
    }
    //#endregion

    $rootScope.IsLoading = false;
    $scope.InitComplete = true;
}]);
