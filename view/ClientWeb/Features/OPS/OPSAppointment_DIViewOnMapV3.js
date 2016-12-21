/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_DIViewOnMapV3 = {
    URL: {
        Order_List: 'OPSDI_MAP_Order_List',
        Vehicle_List: 'OPSDI_MAP_Vehicle_List',
        VehicleVendor_List: 'OPSDI_MAP_VehicleVendor_List',
        TOMaster_List: 'OPSDI_MAP_TOMaster_List',
        CheckVehicleAvailable: 'OPSDI_MAP_CheckVehicleAvailable',

        Setting: 'OPSCO_MAP_Setting',
        Vendor_List: 'OPSCO_MAP_Vendor_List',
        Driver_List: 'OPSCO_MAP_Driver_List',
        DriverVendor_List: 'OPSCO_MAP_DriverVendor_List',
        Location_List: 'OPSCO_MAP_Location_List',

        TripByID: 'OPSDI_MAP_TripByID',
        TripByVehicle_List: 'OPSDI_MAP_TripByVehicle_List',
        Schedule_Data: 'OPSDI_MAP_Schedule_Data',
        Save: 'OPSDI_MAP_Save',
        Update: 'OPSDI_MAP_Update',
        ToMon: 'OPSDI_MAP_ToMON',
        Cancel: 'OPSDI_MAP_Cancel',
        UpdateAndToMon: 'OPSDI_MAP_UpdateAndToMON',
        ToOPS: 'OPSDI_MAP_ToOPS',
        Delete: 'OPSDI_MAP_Delete',
        ToVendor: 'OPSDI_MAP_ToVendor',
        Split: 'OPSDI_MAP_GroupProduct_Split',
        Split_Cancel: 'OPSDI_MAP_GroupProduct_Split_Cancel',

        GroupByTrip_List: 'OPSDI_MAP_GroupByTrip_List',
        FTLSplit: 'OPSDI_MAP_FTL_Split',
        FTLMerge: 'OPSDI_MAP_FTL_Merge',

        Vehicle_New: 'OPSDI_MAP_Vehicle_New',

        DITOGroupProduct_List: 'OPSDI_MAP_DITOGroupProduct_List',
        DI2View_GroupProduct_List: 'OPSDI_MAP_2View_GroupProduct_List',
        DI2View_Master_Update_Check4Delete: 'OPSDI_MAP_2View_Master_Update_Check4Delete',
        DI2View_Master_Update_Check4Consolidate: 'OPSDI_MAP_2View_Master_Update_Check4Consolidate',
        DI2View_Master_Update_Group_Quantity: 'OPSDI_MAP_2View_Master_Update_Group_Quantity',
        DI2View_Master_Update_Check4Update: 'OPSDI_MAP_2View_Master_Update_Check4Update',
        DI2View_Master_Update_TimeLine: 'OPSDI_MAP_2View_Master_Update_TimeLine',
        DI2View_Master_Update_Group: 'OPSDI_MAP_2View_Master_Update_Group',
        DI2View_Master_Update: 'OPSDI_MAP_2View_Master_Update'
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
        VehicleInfo: [],
    },
}

angular.module('myapp').controller('OPSAppointment_DIViewOnMapV3Ctrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIViewOnMapV3Ctrl');
    $rootScope.IsLoading = false;

    $scope.HasChoose = false;
    $scope.TripActived = false;
    $scope.ViewOrderLTL = true; //Xem theo ĐH LTL/FTL
    $scope.ViewOrderDate = false; //Lọc theo ngày
    $scope.ShowOrderDate = false; //Chọn ngày
    $scope.IsFullScreen = false;
    $scope.LocationType = 1;

    $scope.TripDetail = false;
    $scope.IsShowTrip = false; //Hiển thị popup chuyến
    $scope.IsShowTimeLine = false; //Hiển thị popup timelime
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
        VehicleNo: '[Chờ nhập]',
        DriverName: '',
        DriverTel: '',
        StatusCode: 'Có thể tạo chuyến',
        StatusColor: $scope.Color.None,
        VehicleID: -1,
        ETA: null,
        ETD: null,
        Ton: 0,
        CBM: 0,
        ListOPSGOP: [],
        ListOPSGOPName: [],
        LocationStartID: -1,
        LocationStartName: '',
        LocationStartLat: null,
        LocationStartLng: null,
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
        Date: new Date(), VehicleID: -1
    };
    $scope.VehicleTitle = '';

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
    $scope.GroupProductWinWidth = 250;

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
                    $scope.GroupProductWinWidth = $('#pnGroupProduct').width();
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
            { collapsible: true, resizable: true, size: '250px' }
        ],
        resize: function (e) {
            $timeout(function () {
                $scope.VehicleWinWidth = $('#pnVehicle').width();
                $scope.GroupProductWinWidth = $('#pnGroupProduct').width();
            }, 1)
        }
    }

    $timeout(function () {
        var pane = $($scope.conSplitter.element).find(".k-pane:last");
        if (!$scope.ConfigView.showGrid) {
            $($scope.conSplitter.element).find('.k-splitbar:last').hide();
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

    //Lấy thông tin điểm bắt đầu, điểm kết thúc
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIViewOnMapV3.URL.Setting,
        success: function (res) {
            if (Common.HasValue(res)) {
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartID = res.LocationStartID;
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartName = res.LocationStartName;
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndID = res.LocationEndID;
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndName = res.LocationEndName;
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndLat = res.LocationEndLat;
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndLng = res.LocationEndLng;
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartLat = res.LocationStartLat;
                _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartLng = res.LocationStartLng;
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
                case 'Vehicle':
                    $scope.LocationItem = null;
                    $scope.VehicleItem = i.Item;

                    Common.Data.Each($scope.vehicle_Grid.items(), function (tr) {
                        var item = $scope.vehicle_Grid.dataItem(tr);
                        $(tr).removeClass('k-state-selected');
                        if (Common.HasValue(item) && item.ID == $scope.VehicleItem.ID) {
                            $(tr).addClass('k-state-selected');
                        }
                    })
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
    
    $scope.FTLGroupRequest = [{ field: 'TOMasterCode', dir: 'desc' }]

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
                            method: _OPSAppointment_DIViewOnMapV3.URL.Split,
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
                            method: _OPSAppointment_DIViewOnMapV3.URL.Split,
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
                            method: _OPSAppointment_DIViewOnMapV3.URL.Split,
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

    $scope.IsFTLMerge = false;

    $scope.gop_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV3.URL.Order_List,
            pageSize: 20,
            readparam: function () {
                return {
                    typeOfOrder: $scope.ViewOrderLTL ? 1 : 2,
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
                    ETA: { type: 'date' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' }
                }
            }
        }),
        pageable: Common.PageSize,
        height: '99%', groupable: true, sortable: true, columnMenu: false, resizable: true, filterable: { mode: 'row', visible: false }, reorderable: true,
        dataBound: function (e) {
            if ($scope.ViewOrderLTL) {
                this.showColumn('Command');
                this.hideColumn('CommandFTL');
            } else {
                this.hideColumn('Command');
                this.showColumn('CommandFTL');
            }

            var grid = this;//Nếu chọn FTL và gộp đơn hàng
            if (!$scope.ViewOrderLTL && $scope.IsFTLMerge) {
                angular.forEach(grid.element.find('.chkFTLChoose'), function (o) {
                    $(o).hide();
                    $(o).closest('td').find('input.chkFTLChooseMerge').show();
                    if ($scope.DataFTLMerge.indexOf($(o).data('uid'))) chk.prop('checked', true);
                })
            } else {
                Common.Data.Each(grid.items(), function (tr) {
                    var item = grid.dataItem(tr);
                    if (Common.HasValue(item) && Common.HasValue($scope.DataGroupProductSelected[item.ID])) {
                        item.IsChoose = true;
                        $(tr).find('.chkChoose').prop('checked', true);
                    }
                })
            }
        },
        columns: [
            {
                field: 'Choose', title: ' ', width: 35,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gop_Grid,gop_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gop_Grid,gop_GridChoose_Change)" />',
                filterable: false, sortable: false, groupable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'Command', title: ' ', width: 50,
                attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button btn-merge" ng-show="dataItem.IsSplit" title="Gộp" href="/" ng-click="GroupProduct_Merge($event,dataItem,gop_Grid)">M</a>' +
                    '<a class="k-button small-button btn-merge-ok" style="display:none;" title="Xác nhận" href="/" ng-click="GroupProduct_Merge_OK($event,dataItem,gop_Grid)">S</a>' +
                    '<input type="checkbox" style="display:none;" class="chk-select-to-merge" />',
                filterable: false, sortable: false, groupable: false, sortorder: 1, configurable: false, isfunctionalHidden: true
            },
            {
                field: 'CommandFTL', title: ' ', width: 40,
                attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button" title="Hủy đơn hàng" href="/" ng-click="GroupProduct_Cancel($event,dataItem,gop_Grid)">C</a>',
                filterable: false, sortable: false, groupable: false, sortorder: 2, configurable: false, isfunctionalHidden: true
            },
            {
                field: 'TOMasterCode', width: 0, title: ' ', hidden: true,
                groupHeaderTemplate: function (e) {
                    var uid = -1;
                    try {
                        var o = e.aggregates.parent();
                        while (o.hasSubgroups == true && o.items != null && o.items.length > 0) {
                            o = o.items[0].aggregates.parent();
                        }
                        uid = o.items[0].TOMasterID;
                    } catch (e) { }
                    return "<input type='checkbox' data-uid='" + uid + "' style='position:relative;top:1px;' class='chkFTLChoose' ng-click='gridChooseFTL_Change($event,gop_Grid)' />"
                        + "<input type='checkbox' data-uid='" + uid + "' style='position:relative;top:1px;display:none;' class='chkFTLChooseMerge' ng-click='gridChooseMergeFTL_Change($event,gop_Grid)' />Mã chuyến: " + e.value;
                },
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: false, isfunctionalHidden: true
            },
            {
                field: 'IsWarning', width: 80, title: 'Cảnh báo', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false, sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: false },
            { field: 'CustomerShortName', width: 100, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: false },
            { field: 'OrderCode', width: 100, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 7, configurable: true, isfunctionalHidden: false },
            { field: 'GroupProductCode', width: 100, title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 8, configurable: true, isfunctionalHidden: false },
            { field: 'ProductCode', width: 100, title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 9, configurable: true, isfunctionalHidden: false },
            {
                field: 'Ton', width: 80, title: 'Tấn',
                template: '#if(!IsFTL&&Ton>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Ton#" k-options="gopTon_Options" data-k-max="#:Ton#" style="width:100%"/></form>#}else{##:Ton##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: 80, title: 'Khối',
                template: '#if(!IsFTL&&CBM>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:CBM#" k-options="gopCBM_Options" data-k-max="#:CBM#" style="width:100%"/></form>#}else{##:CBM##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: 80, title: 'S.Lượng',
                template: '#if(!IsFTL&&Quantity>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Quantity#" k-options="gopQuantity_Options" data-k-max="#:Quantity#" style="width:100%"/></form>#}else{##:Quantity##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            { field: 'DNCode', width: 100, title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 14, configurable: true, isfunctionalHidden: false },
            { field: 'SOCode', width: 100, title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 15, configurable: true, isfunctionalHidden: false },
            {
                field: 'ETD', width: 110, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }, sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: 110, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }, sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            { field: 'TempMin', width: 100, title: 'N.Độ tối thiểu', template: '#=TempMin!=null?TempMin:""#', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 18, configurable: true, isfunctionalHidden: false },
            { field: 'TempMax', width: 100, title: 'N.Độ tối đa', template: '#=TempMax!=null?TempMax:""#', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 19, configurable: true, isfunctionalHidden: false },
            { field: 'DistributorCode', width: 120, title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 20, configurable: true, isfunctionalHidden: false },
            { field: 'DistributorName', width: 120, title: 'Tên NPP', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 21, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromCode', width: 120, title: 'Mã điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 22, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromName', width: 120, title: 'Tên điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 23, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToCode', width: 120, title: 'Mã điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 24, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToName', width: 120, title: 'Tên điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 25, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromAddress', width: 120, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 26, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromDistrict', width: 120, title: 'Quận huyên nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 27, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromProvince', width: 120, title: 'Tỉnh thành nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 28, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToAddress', width: 120, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 29, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToDistrict', width: 120, title: 'Quận huyên giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 30, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToProvince', width: 120, title: 'Tỉnh thành giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 31, configurable: true, isfunctionalHidden: false },
            { field: 'UserDefine1', width: 100, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 32, configurable: true, isfunctionalHidden: false },
            { field: 'UserDefine2', width: 100, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 33, configurable: true, isfunctionalHidden: false },
            { field: 'CUSRoutingCode', width: 100, title: 'Mã cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 34, configurable: true, isfunctionalHidden: false },
            { field: 'CUSRoutingName', width: 100, title: 'Tên cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 35, configurable: true, isfunctionalHidden: false },
            { field: 'CUSRoutingNote', width: 100, title: 'Ghi chú cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 36, configurable: true, isfunctionalHidden: false },
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 37, configurable: true, isfunctionalHidden: false },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 38, configurable: true, isfunctionalHidden: false },
            { field: 'GroupSort', width: 100, title: ' ', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 39, configurable: true, isfunctionalHidden: false },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: true }
        ]
    };

    $scope.tooltipOptions = {
        filter: ".img-warning", position: "top",
        content: function (e) {
            return $(e.target).data('value');
        }
    }

    $scope.vehicle_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV3.URL.Vehicle_List,
            readparam: function () {
                return {
                    requestDate: $scope.VehicleRequestDate
                }
            },
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' }
                }
            }
        }),
        pageable: Common.PageSize,
        height: '99%', groupable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: true,
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
                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Truck), 1);
                    openMapV2.NewMarker(item.Lat, item.Lng, item.Regno, item.Regno, icon, {
                        Item: item, Type: 'Vehicle'
                    }, "VectorMarkerVEH");
                }
            });
        },
        columns: [
            {
                field: 'Regno', width: 150, title: 'Số xe',
                template: '<span style="cursor:pointer;" ng-mouseenter="ViewVehicleInfo($event,dataItem)" ' +
                    'ng-click="VehicleInfo_View($event,dataItem,vehicle_trip_win)" ' +
                    'ng-mouseleave="HideVehicleInfo($event,dataItem)">#=Regno# </span>' +
                    '<a class="k-button select" ng-class="TripItem.VehicleID==#=ID#?\'selected\':\'\'" ' +
                    'ng-click="Vehicle_Select($event,dataItem,vehicle_Grid)"><span>LC</span></a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeight', width: 100, title: 'Trọng tải',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'StatusOfVehicleName', width: 100, title: 'T/trạng',
                filterable: false, sortable: false
            }
        ]
    };

    $scope.GroupProduct_Cancel = function ($event, item, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận hủy đơn hàng?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIViewOnMapV3.URL.Cancel,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.Split_Cancel,
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

    $scope.$watch('VehicleRequestDate', function () {
        $scope.vehicle_Grid.dataSource.read();
    });
    
    $scope.vehicle_Trip_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' },
                        ETD: { type: 'date' },
                        ETA: { type: 'date' }
                    }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, auboBind: false,
        columns: [
            {
                field: 'Code', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }, {
                field: 'DriverName', title: 'Tài xế', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
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
            method: _OPSAppointment_DIViewOnMapV3.URL.Location_List,
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, auboBind: false,
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

    $scope.$watch('TripActived', function (n, o) {
        if ($scope.TripActived) {
            $scope.trip_info_win.close();
            $scope.TripDetail = false;
            openMapV2.VisibleVector("VectorRouteORD", false);
            openMapV2.VisibleVector("VectorMarkerORD", false);
            openMapV2.VisibleVector("VectorMarkerVEH", true);
            try {
                $scope.viewSplitter.collapse("#pnBottom");
            } catch (e) { }
        } else {
            openMapV2.VisibleVector("VectorRouteORD", true);
            openMapV2.VisibleVector("VectorMarkerORD", true);
            openMapV2.VisibleVector("VectorRouteTO", false);
            openMapV2.VisibleVector("VectorMarkerTO", false);            
            openMapV2.FitBound("VectorMarkerORD", 15);
            try {
                if (!$scope.IsVehicleVendor) {
                    openMapV2.VisibleVector("VectorMarkerVEH", true); //TH xe nhà.
                }
                $scope.viewSplitter.expand("#pnBottom");
            } catch (e) { }            
        }
    });

    //Hiện popup chuyến
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
    
    $scope.DataGroupProductSelected = {};
    $scope.gop_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                $scope.DataGroupProductSelected[o.ID] = $.extend(true, {}, o);
            } else if (Common.HasValue($scope.DataGroupProductSelected[o.ID])) {
                $scope.DataGroupProductSelected[o.ID] = null;
            }
        })
        Common.Data.Each($scope.DataGroupProductSelected, function (o) {
            if (o != null)
                $scope.HasChoose = true;
        })
        var Ton = 0,
            CBM = 0,
            ETD = null,
            ETA = null,
            data = [],
            tmpDataM = {},
            tmpDataN = {},
            dataLocationM = [],
            dataLocationN = [];
        angular.forEach($scope.DataGroupProductSelected, function (item) {
            if (Common.HasValue(item)) {
                Ton += item.Ton;
                CBM += item.CBM;
                data.push(item.ID);
                if (ETD == null) ETD = Common.Date.FromJson(item.ETD);
                if (ETA == null) ETA = Common.Date.FromJson(item.ETA);
                if (ETD > Common.Date.FromJson(item.ETD)) ETD = Common.Date.FromJson(item.ETD);
                if (ETA < Common.Date.FromJson(item.ETA)) ETA = Common.Date.FromJson(item.ETA);
                if (item.ListLocation != null) {
                    for (var i = 0; i < item.ListLocation.length; i++) {
                        var o = item.ListLocation[i];
                        if (!Common.HasValue(tmpDataM[o.LocationID]) && !Common.HasValue(tmpDataN[o.LocationID])) {
                            if (o.ID == item.LocationFromID) {
                                o.TypeOfTOLocationID = 1;
                                tmpDataM[o.LocationID] = 1; dataLocationM.push(o);
                            } else {
                                o.TypeOfTOLocationID = 2;
                                tmpDataN[o.LocationID] = 1; dataLocationN.push(o);
                            }
                        }
                    }
                }
            }
        });
        var dataLocation = dataLocationM.concat(dataLocationN);
        $scope.TripItem.Ton = Ton;
        $scope.TripItem.CBM = CBM;
        $scope.TripItem.ETD = ETD;
        $scope.TripItem.ETA = ETA;
        $scope.TripItem.ListOPSGOP = data;
        $scope.TripItem.ListLocation = dataLocation;
        if ($scope.TripDetail) {
            if ($scope.TripItem.ListOPSGOP == null || $scope.TripItem.ListOPSGOP.length == 0) {
                $timeout(function () {
                    $scope.TripItem.StatusColor = $scope.Color.Error;
                    $scope.TripItem.StatusCode = "Không có thông tin hàng hóa";
                    $scope.IsTripChecking = false;
                }, 1)
            }
        }
        $scope.VehicleRequestDate = ETD || $scope.VehicleRequestDate;
        $scope.RefreshMap();
    }

    $scope.gridChooseFTL_Change = function ($event, grid) {
        var chk = $event.target;
        var uid = $(chk).data('uid');
        var tmp = $(chk).prop('checked');
        angular.forEach(grid.element.find('.chkFTLChoose'), function (o) {
            if (o != chk)
                $(o).prop('checked', '');
        })
        if (uid > 0 && tmp) {
            $scope.HasChoose = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIViewOnMapV3.URL.TripByID,
                data: {
                    masterID: uid
                },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $scope.TripItem = {
                            ID: res.ID,
                            Code: res.Code,
                            VehicleNo: res.VehicleNo,
                            DriverName: res.DriverName,
                            DriverTel: res.DriverTel,
                            StatusCode: 'Có thể tạo chuyến',
                            StatusColor: $scope.Color.None,
                            VehicleID: res.VehicleID,
                            Ton: res.TotalTon,
                            CBM: res.TotalCBM,
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
                        $scope.RefreshMap();
                    }
                }
            });
        } else {
            $scope.ResetTrip(false);
        }
    }
    
    $scope.RefreshMap = function () {
        openMapV2.ClearVector("VectorRouteORD");
        openMapV2.ClearVector("VectorMarkerORD");

        var objS, objE, oS, oE;
        if ($scope.TripItem.ListOPSGOP.length > 0 && $scope.TripItem.LocationEndID > 0 && $scope.TripItem.LocationEndLat != null && $scope.TripItem.LocationEndLng != null) {
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
        if ($scope.TripItem.ListOPSGOP.length > 0 && $scope.TripItem.LocationStartID > 0 && $scope.TripItem.LocationStartLat != null && $scope.TripItem.LocationStartLng != null) {
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

        var tmpDataM = [], tmpDataR = [];
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
                        openMapV2.NewRoute(tmpDataM[prev.LocationID], tmpDataM[curr.LocationID], "", "", openMapV2.NewStyle.Line(4, 'rgba(3, 169, 244, 0.6)'), null, "VectorRouteORD", $scope.indexMap, function () {
                        }, true);
                    }
                    if (i == sTotal - 1) {
                        if (Common.HasValue(objE) && Common.HasValue(tmpDataM[curr.LocationID]) && !Common.HasValue(tmpDataR[curr.LocationID + "-" + oE.ID])) {
                            tmpDataR[curr.LocationID + "-" + oE.ID] = true;
                            openMapV2.NewRoute(tmpDataM[curr.LocationID], objE, "", "", openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)', [20, 10]), null, "VectorRouteORD", $scope.indexMap, function () {
                            }, true);
                        }
                    }
                } else {
                    if (Common.HasValue(objS) && Common.HasValue(tmpDataM[curr.LocationID]) && !Common.HasValue(tmpDataR[oS.ID + "-" + curr.LocationID])) {
                        tmpDataR[oS.ID + "-" + curr.LocationID] = true;
                        openMapV2.NewRoute(objS, tmpDataM[curr.LocationID], "", "", openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)', [20, 10]), null, "VectorRouteORD", $scope.indexMap, function () {
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
            IsLoaded: false, ListTrip: []
        }

        var off = $($event.currentTarget).offset();
        $scope.VehicleInfoStyle = {
            'display': '', 'top': off.top - 8, 'left': off.left - 395
        }

        if (!Common.HasValue(_OPSAppointment_DIViewOnMapV3.Data.VehicleInfo[item.ID])) {
            _OPSAppointment_DIViewOnMapV3.Data.VehicleInfo[item.ID] = {
                IsLoaded: false, ListTrip: []
            }

            //Load data.
            $scope.LoadVehicleTrip(new Date(), item.ID,  3, true, true, function (res) {
                _OPSAppointment_DIViewOnMapV3.Data.VehicleInfo[item.ID] = {
                    IsLoaded: true,
                    ListTrip: res
                }
                var obj = _OPSAppointment_DIViewOnMapV3.Data.VehicleInfo[item.ID];
                $scope.VehicleInfo = {
                    IsLoaded: obj.IsLoaded,
                    ListTrip: obj.ListTrip
                }
            });
        } else {
            var obj = _OPSAppointment_DIViewOnMapV3.Data.VehicleInfo[item.ID];
            $scope.VehicleInfo = {
                IsLoaded: obj.IsLoaded,
                ListTrip: obj.ListTrip
            }
        }
    }
    
    $scope.LoadVehicleTrip = function (requestDate, vehicleID, total, isapproved, istendered, callback) {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV3.URL.TripByVehicle_List,
            data: {
                Date: requestDate, vehicleID: vehicleID,
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

    $scope.VehicleInfo_View = function ($event, item, win) {
        $event.preventDefault();

        $scope.VehicleTitle = item.Regno;
        $scope.VehicleTripRequest.VehicleID = item.ID;
        $scope.ViewVehicleTripApproved = true;
        $scope.ViewVehicleTripTendered = false;
        $scope.LoadVehicleTrip($scope.VehicleTripRequest.Date, $scope.VehicleTripRequest.VehicleID, -1, $scope.ViewVehicleTripApproved, $scope.ViewVehicleTripTendered, function (res) {
            $scope.vehicle_Trip_Grid_Options.dataSource.data(res);
            win.center().open();
            $timeout(function () {
                $scope.vehicle_Trip_Grid.resize();
            }, 100)
        });
    }

    $scope.Vehicle_Select = function ($event, item, grid) {
        $event.preventDefault();

        angular.forEach(grid.items(), function (tr) {
            $(tr).find('.k-button.select').removeClass('selected');
        });

        if (Common.HasValue(item)) {
            $($event.currentTarget).addClass('selected');
            $scope.TripItem.VehicleID = item.ID;
            $scope.TripItem.VehicleNo = item.Regno;
            $scope.TripItem.VehicleMaxWeight = item.MaxWeight;
        }
    }
    
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
        $scope.TripItem.DriverName = '';
        $scope.TripItem.DriverTel = '';
        $scope.TripItem.StatusCode = 'Có thể tạo chuyến';
        $scope.TripItem.StatusColor = $scope.Color.None;
        $timeout(function () {
            if ($scope.IsVehicleVendor) {
                try {
                    $scope.vehicleByVendor_Grid.dataSource.read();
                    openMapV2.VisibleVector("VectorMarkerVEH", false);
                } catch (e) {
                }
            } else {
                try {
                    openMapV2.VisibleVector("VectorMarkerVEH", true);
                } catch (e) {
                }
            }
        }, 1)
    });

    $scope.$watch('VehicleVendorID', function () {
        $scope.vehicleByVendor_Grid.dataSource.read();
    });

    $scope.vehicleByVendor_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV3.URL.VehicleVendor_List,
            readparam: function () {
                return {
                    vendorID: $scope.VehicleVendorID
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
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: true, auboBind: false,
        columns: [
            {
                field: 'Regno', width: 150, title: 'Số xe',
                template: '<span >#=Regno# </span><a class="k-button select" ng-class="TripItem.VehicleID==#=ID#?\'selected\':\'\'" ng-click="Vehicle_Select($event,dataItem,vehicleByVendor_Grid)"><span>LC</span></a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeight', width: 100, title: 'Trọng tải',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
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
                    method: _OPSAppointment_DIViewOnMapV3.URL.Vehicle_New,
                    data: {
                        vendorID: $scope.VehicleVendorID,
                        regNo: $scope.NewVehicleVendor.RegNo,
                        maxWeight: $scope.NewVehicleVendor.MaxWeight,
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            });
                            win.close();
                            $scope.vehicleByVendor_Grid.dataSource.read();
                        })
                    }
                })
            }
        })
    }

    $scope.OrderViewStatus_Click = function ($event, grid) {
        $event.preventDefault();

        try {
            $scope.ResetTrip(false);
            $scope.ShowOrderDate = false;
            var value = $($event.currentTarget).data('tabindex');
            var group = grid.dataSource.group();

            if (value == 1) {
                grid.showColumn('Choose');
                $scope.ViewOrderLTL = true;

                if (group != null && group.length > 0) {
                    var i = 0;
                    while (i < group.length) {
                        var o = group[i];
                        if (o.field == 'TOMasterCode') {
                            group.splice(i, 1);
                        } else {
                            i++;
                        }
                    }
                    grid.dataSource.group(group);
                } else {
                    grid.dataSource.read();
                }
            } else {
                grid.hideColumn('Choose');
                $scope.ViewOrderLTL = false;

                if (group == null || group.length == 0) {
                    group = [{ field: 'TOMasterCode', dir: 'desc' }];
                    grid.dataSource.group(group);
                } else {
                    var flag = false;
                    Common.Data.Each(group, function (o) {
                        if (o.field == 'TOMasterCode')
                            flag = true;
                    })
                    if (!flag) {
                        group.splice(0, 0, { field: 'TOMasterCode', dir: 'desc' });
                        grid.dataSource.group(group);
                    } else {
                        grid.dataSource.read();
                    }
                }
            }
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
        $scope.DateRequest = { fDate: null, tDate: null };
        grid.dataSource.read();
    }

    $scope.OrderViewDate_OK_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.ViewOrderDate = true;
        $scope.ShowOrderDate = false;
        grid.dataSource.read();
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
                $scope.LoadVehicleTrip($scope.VehicleTripRequest.Date, $scope.VehicleTripRequest.VehicleID, -1, $scope.ViewVehicleTripApproved, $scope.ViewVehicleTripTendered, function (res) {
                    grid.dataSource.data(res);
                })
            }
        }
        catch (e) { }
    }

    $scope.$watch('VehicleTripRequest.Date', function () {
        $timeout(function () {
            $scope.LoadVehicleTrip($scope.VehicleTripRequest.Date, $scope.VehicleTripRequest.VehicleID, -1, $scope.ViewVehicleTripApproved, $scope.ViewVehicleTripTendered, function (res) {
                $scope.vehicle_Trip_Grid_Options.dataSource.data(res);
            })
        }, 1)
    });

    $scope.Accept_Click = function ($event, grid, win, vwin) {
        $event.preventDefault();

        var data = [];
        var dataCode = [];

        if ($scope.ViewOrderLTL) {
            angular.forEach($scope.DataGroupProductSelected, function (item) {
                if (Common.HasValue(item) && item.IsChoose) {
                    data.push(item.ID);
                    dataCode.push(item);
                }
            })
            $scope.TripItem.ListOPSGOPName = dataCode;
            $scope.TripItem.ListOPSGOP = data;

            $scope.TripItem.LocationStartID = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartID;
            $scope.TripItem.LocationStartName = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartName;
            $scope.TripItem.LocationEndID = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndID;
            $scope.TripItem.LocationEndName = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndName;
            $scope.TripItem.LocationEndLat = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndLat;
            $scope.TripItem.LocationEndLng = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndLng;
            $scope.TripItem.LocationStartLat = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartLat;
            $scope.TripItem.LocationStartLng = _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartLng;
        }
        if ((data.length == 0 && $scope.TripItem.ID < 1) || $scope.TripItem.VehicleID < 1 || $scope.TripItem.ETA == null || $scope.TripItem.ETD == null) {
            $rootScope.Message({ Msg: "Vui lòng điền đầy đủ thông tin.", Type: Common.Message.Type.Alert });
        } else {
            if ($scope.IsVehicleVendor) {
                if ($scope.TripItem.ETD >= $scope.TripItem.ETA) {
                    $scope.TripItem.StatusCode = "Tg không hợp lệ.";
                    $scope.TripItem.StatusColor = $scope.Color.Error;
                    return;
                }

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIViewOnMapV3.URL.DriverVendor_List,
                    data: {
                        vendorID: $scope.VehicleVendorID
                    },
                    success: function (res) {
                        var data = [];
                        $.each(res, function (i, v) {
                            data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
                        });
                        $scope.atcVendorDriverNameOptions.dataSource.data(data);
                    }
                });

                $scope.TripDetail = true;
                vwin.center().open();
                $timeout(function () {
                    $scope.IsTripChecking = true;
                }, 100)
            }
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIViewOnMapV3.URL.CheckVehicleAvailable,
                    data: {
                        vehicleID: $scope.TripItem.VehicleID,
                        masterID: $scope.TripItem.ID || -1,
                        ETD: $scope.TripItem.ETD,
                        ETA: $scope.TripItem.ETA,
                        Ton: $scope.TripItem.Ton
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $scope.TripDetail = true;

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
        if ($scope.TripItem.ETA == null || $scope.TripItem.ETD == null) {
            flag = false;
            $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        }

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận lưu?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    if ($scope.IsVehicleVendor) {
                        $scope.TripItem.VendorOfVehicleID = $scope.VehicleVendorID;
                    }

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIViewOnMapV3.URL.Save,
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
        else {
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
                    method: _OPSAppointment_DIViewOnMapV3.URL.Location_List,
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

    $scope.atcVendorDriverNameOptions = {
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
        else {
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
        }

        //Active indexMap
        openMapV2.Active($scope.indexMap);
        $scope.LocationItem = null;
        openMapV2.Close();
        winmap.close();
        win.close();
    }

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIViewOnMapV3.URL.Driver_List,
        success: function (res) {
            var data = [];
            $.each(res, function (i, v) {
                data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
            });
            $scope.atcDriverNameOptions.dataSource.data(data);
        }
    });

    $scope.$watch('TripItem.ETD', function () {
        $scope.CheckTrip();
    });
    $scope.$watch('TripItem.ETA', function () {
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

    $scope.CheckTrip = function () {
        $scope.TripItem.StatusCode = "";
        $scope.TripItem.StatusColor = $scope.Color.None;

        if (!$scope.IsVehicleVendor && $scope.TripItem.VehicleID > 0 && $scope.TripItem.ETD != null && $scope.TripItem.ETA != null) {
            Common.Log('Trip checing...');

            if ($scope.TripItem.ETD >= $scope.TripItem.ETA) {
                $scope.TripItem.StatusCode = "Tg không hợp lệ.";
                $scope.TripItem.StatusColor = $scope.Color.Error;
            }
            else {
                $scope.TripItem.IsCheching = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIViewOnMapV3.URL.CheckVehicleAvailable,
                    data: {
                        vehicleID: $scope.TripItem.VehicleID,
                        masterID: $scope.TripItem.ID || -1,
                        ETD: $scope.TripItem.ETD,
                        ETA: $scope.TripItem.ETA,
                        Ton: $scope.TripItem.Ton
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $scope.TripItem.IsCheching = false;
                            if (res.IsOverWeight) {
                                $scope.TripItem.StatusCode = "Quá trọng tải";
                                $scope.TripItem.StatusColor = $scope.Color.Error;
                            } else {
                                if (res.IsVehicleAvailable) {
                                    $scope.TripItem.DriverTel = res.DriverTel;
                                    $scope.TripItem.DriverName = res.DriverName;
                                    if ($scope.TripItem.Ton == $scope.TripItem.VehicleMaxWeight) {
                                        $scope.TripItem.StatusCode = "Có thể tạo chuyến";
                                    } else {
                                        $scope.TripItem.StatusCode = "Có thể tạo chuyến (" + $scope.TripItem.Ton + "/" + $scope.TripItem.VehicleMaxWeight + "T)";
                                    }
                                    if ($scope.TripItem.ID > 0)
                                        $scope.TripItem.StatusCode = "Có thể cập nhật";
                                    $scope.TripItem.StatusColor = $scope.Color.Success;
                                } else {
                                    $scope.TripItem.StatusCode = "Xe bận.";
                                    $scope.TripItem.StatusColor = $scope.Color.Error;
                                }
                            }                            
                        })
                    }
                })
            }
        }
    }

    $scope.ResetTrip = function (isLoad) {
        $scope.DataGroupProductSelected = {};
        $scope.HasChoose = false;
        $scope.TripItem = {
            VehicleNo: '[Chờ nhập]',
            DriverName: '',
            DriverTel: '',
            StatusCode: 'Có thể tạo chuyến',
            StatusColor: $scope.Color.None,
            VehicleID: -1,
            ETA: null,
            ETD: null,
            Ton: 0,
            CBM: 0,
            ListOPSGOP: [],
            ListOPSGOPName: [],
            LocationStartID: -1,
            LocationStartName: '',
            LocationStartLat: null,
            LocationStartLng: null,
            LocationEndID: -1,
            LocationEndName: '',
            LocationEndLat: null,
            LocationEndLng: null,
            ListLocation: []
        }
        if (isLoad) {
            $scope.gop_Grid.dataSource.read();
        }
        if ($scope.TripDetail == true) {
            $scope.TripDetail = false;
            $scope.trip_info_win.close();
        }
    }

    //#region Tender
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIViewOnMapV3.URL.Vendor_List,
        success: function (res) {
            _OPSAppointment_DIViewOnMapV3.Data.VendorList = res;
            $scope.cboNewTripVendor_Options.dataSource.data(res);
            var data = $.extend(true, [], res);
            data.shift();
            if (data.length > 0) {
                $scope.VehicleVendorID = data[0].ID;
                $scope.cboVehicleVendor_Options.dataSource.data(data);
            }
        }
    });

    //Gủi đối tác
    $scope.Tender_Click = function ($event, grid, win) {
        $event.preventDefault();

        if ($scope.ViewOrderLTL) {
            var data = [];
            var dataCode = [];
            angular.forEach($scope.DataGroupProductSelected, function (item) {
                if (Common.HasValue(item) && item.IsChoose) {
                    data.push(item.ID);
                }
            })
            if (data.length > 0) {
                $scope.TripItem.ListOPSGOP = data;
            } else {
                return;
            }
        }
        else {
            if ($scope.TripItem.ID < 1) {
                return;
            }
        }

        var dataVen = [];
        var cboVenData = $.extend(true, [], _OPSAppointment_DIViewOnMapV3.Data.VendorList);
        var vID = '';
        if (cboVenData.length > 0) {
            vID = cboVenData[0].ID;
        }
        dataVen.push({ SortOrder: 1, VendorID: vID, RateTime: 2, IsManual: false, Debit: 0, ListVendor: cboVenData });
        dataVen.push({ SortOrder: 2, VendorID: vID, RateTime: 2, IsManual: false, Debit: 0, ListVendor: cboVenData });
        dataVen.push({ SortOrder: 3, VendorID: vID, RateTime: 2, IsManual: false, Debit: 0, ListVendor: cboVenData });

        win.center().open();

        $timeout(function () {
            $scope.vendor_rate_Grid_Options.dataSource.data(dataVen);
        }, 200)
    }

    $scope.Tender_Accept_Click = function ($event, grid, win) {
        $event.preventDefault();

        var flag = true;
        var data = $.extend(true, [], grid.dataSource.data());
        Common.Data.Each(data, function (o) {
            if (o.VendorID < 1)
                flag = false;
            if (!o.IsManual)
                o.Debit = 0;
        })

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận gửi đối tác?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIViewOnMapV3.URL.ToVendor,
                        data: {
                            item: $scope.TripItem, data: data
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
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                            if (res.ErrorMessage == 'Failure sending mail.') {
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })

                                win.close();
                                $scope.ResetTrip(true);
                                $rootScope.Message({ Msg: "Không thể gửi email cho đối tác!", Type: Common.Message.Type.Alert });
                            }
                        }
                    })                    
                }
            })
        } else {
            $rootScope.Message({ Msg: "Vui lòng chọn đối tác và thời gian.", Type: Common.Message.Type.Alert });
        }
    }
    
    $scope.Cancel_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        angular.forEach($scope.DataGroupProductSelected, function (item) {
            if (Common.HasValue(item) && item.IsChoose) {
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.Cancel,
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
            pageSize: 0,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' },
                        IsManual: { type: 'bool' }
                    }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, auboBind: false,
        columns: [
            {
                field: 'SortOrder', width: '50px', title: 'STT',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                filterable: false, menu: false
            },
            {
                field: 'VendorID', title: 'Đối tác',
                template: '<input focus-k-combobox class="cus-combobox cboVendor" kendo-combo-box k-options="cboVendorOptions" data-bind="value:VendorID" ng-model="dataItem.VendorID" k-data-source="dataItem.ListVendor"/>',
                filterable: false, menu: false
            },
            {
                field: 'RateTime', width: '60px', title: 'T.gian',
                template: '<input type="number" class="k-textbox cus-number" min="0" max="1000" style="width:100%"/>',
                filterable: false, menu: false
            },
            {
                field: 'IsManual', width: '80px', title: 'Nhập giá',
                template: '<input style="text-align:center" class="chkIsManual" ng-model="dataItem.IsManual" type="checkbox" #= IsManual ? checked="checked" : "" #/>',
                filterable: false, menu: false
            },
            {
                field: 'Debit', width: '120px', title: 'Giá',
                template: '<input class="k-textbox cus-number txtDebit" ng-disabled="!dataItem.IsManual" value="#=Debit#" style="width:100%"/>',
                filterable: false, menu: false
            }
        ]
    }

    //#endregion

    //#region FTL Split & Merge
    $scope.DataFTLMerge = [];
    $scope.FTLMerge_Click = function ($event, grid) {
        $event.preventDefault();

        if (!$scope.IsFTLMerge) {
            $scope.IsFTLMerge = true;
            $scope.DataFTLMerge = [];
            angular.forEach(grid.element.find('.chkFTLChoose'), function (o) {
                var chk = $(o).closest('td').find('input.chkFTLChooseMerge');
                chk.show(); $(o).hide();
                if ($(o).prop("checked")) {
                    chk.prop("checked", true);
                    var uid = $(chk).data('uid');
                    $scope.DataFTLMerge.push(uid);
                }
            })
        } else {
            if ($scope.DataFTLMerge.length < 2) {
                $rootScope.Message({
                    Msg: "Vui lòng chọn ít nhất 2 đơn hàng!", Type: Common.Message.Type.Alert
                })
            } else {
                $rootScope.Message({
                    Msg: "Xác nhận gộp các đơn hàng đã chọn?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV3.URL.FTLMerge,
                            data: { data: $scope.DataFTLMerge },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                        $scope.ResetTrip(true);
                                        $rootScope.IsLoading = false;
                                        $scope.IsFTLMerge = false;
                                        $scope.DataFTLMerge = [];
                                    });
                                });
                            }
                        });
                    }
                })
            }
        }
    }

    $scope.gridChooseMergeFTL_Change = function ($event, grid) {
        var chk = $(event.target), val = $(chk).prop('checked'), uid = $(chk).data('uid');
        if (val == true) {
            if ($scope.DataFTLMerge.indexOf(uid) == -1) $scope.DataFTLMerge.push(uid);
        } else {
            if ($scope.DataFTLMerge.indexOf(uid) > -1) $scope.DataFTLMerge.splice($scope.DataFTLMerge.indexOf(uid), 1);
        }
    }

    $scope.FTLMerge_Cancel_Click = function ($event, grid) {
        $event.preventDefault();

        if ($scope.DataFTLMerge.length > 1) {
            $rootScope.Message({
                Msg: "Xác nhận hủy gộp?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $scope.IsFTLMerge = false;
                    $scope.DataFTLMerge = [];
                    angular.forEach(grid.element.find('.chkFTLChooseMerge'), function (o) {
                        var chk = $(o).closest('td').find('input.chkFTLChoose');
                        chk.show(); $(o).hide(); $(o).prop("checked", false);
                    })
                }
            })
        } else {
            $scope.IsFTLMerge = false;
            $scope.DataFTLMerge = [];
            angular.forEach(grid.element.find('.chkFTLChooseMerge'), function (o) {
                var chk = $(o).closest('td').find('input.chkFTLChoose');
                chk.show(); $(o).hide(); $(o).prop("checked", false);
            })
        }
    }

    $scope.FTLSplit_Click = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV3.URL.GroupByTrip_List,
            data: {
                tripID: $scope.TripItem.ID
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.IsLoading = false;
                    $scope.splitGop_GridOptions.dataSource.data(res);
                    win.center().open();
                    $timeout(function () {
                        $scope.splitGop_GridOptions.dataSource.sync();
                    }, 101)
                })
            }
        })
    }

    $scope.splitGop_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'GenID',
                fields:
                    {
                        GenID: { type: 'string', editable: false },
                        ID: { type: 'number' },
                        TOMasterID: { type: 'number' },
                        ETD: { type: 'date' },
                        ETA: { type: 'date' },
                        Ton: { type: 'number' },
                        CBM: { type: 'number' },
                        Quantity: { type: 'number' },
                        TempMin: { type: 'number' },
                        TempMax: { type: 'number' }
                    }
            },
            group: [{ field: 'TOMasterID', dir: 'asc' }]
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, resizable: true, reorderable: true, sortable: { mode: 'multiple' }, auboBind: false,
        columns: [
            {
                field: 'TOMasterID', width: 50, title: ' ', filterable: false, sortable: false, groupable: false, groupHeaderTemplate: "Chuyến: #=value#",
                template: '<form class="cus-form-enter" ng-submit="TOMasterEnter_Click($event)"><input kendo-numeric-text-box class="txtTOMaster" value="#=TOMasterID>0?TOMasterID:0#" data-k-min="1" k-options="gopSplitTOMaster_Options" style="width:100%" /></form>',
            },
            {
                field: 'Command', title: ' ', width: '35px',
                attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button btn-merge" ng-show="dataItem.IsSplit" title="Gộp" href="/" ng-click="FTLGroupProduct_Merge($event,dataItem,splitGop_Grid)">M</a>' +
                    '<a class="k-button small-button btn-merge-ok" style="display:none;" title="Xác nhận" href="/" ng-click="FTLGroupProduct_Merge_OK($event,dataItem,splitGop_Grid)">S</a>' +
                    '<input type="checkbox" style="display:none;" class="chk-select-to-merge" />',
                filterable: false, sortable: false, groupable: false
            },
			{ field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 100, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: 100, title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: 80, title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 80, title: 'Tấn',
                template: '#if(PackingType==1&&IsFTL&&Ton>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Ton#" k-options="gopSplitTon_Options" data-k-max="#:Ton#" style="width:100%"/></form>#}else{##:Ton##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: 80, title: 'Khối',
                template: '#if(PackingType==2&&IsFTL&&CBM>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:CBM#" k-options="gopSplitCBM_Options" data-k-max="#:CBM#" style="width:100%"/></form>#}else{##:CBM##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: 80, title: 'S.Lượng',
                template: '#if(PackingType==3&&IsFTL&&Quantity>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Quantity#" k-options="gopSplitQuantity_Options" data-k-max="#:Quantity#" style="width:100%"/></form>#}else{##:Quantity##}#',
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
            { field: 'LocationFromCode', width: 150, title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 150, title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");
        }
    }

    $scope.ToFix3 = function (val) {
        return val > 0 ? Math.round(val * 1000) / 1000 : 0;
    }

    $scope.ToFix6 = function (val) {
        return val > 0 ? Math.round(val * 1000000) / 1000000 : 0;
    }

    $scope.gopSplitTon_Options = {
        min: 0, spinners: false, decimals: Common.Number.DI_Decimals, culture: "en-US", format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.IsSplit = true;
                var val = this.value();
                var objNew = angular.copy(obj);
                objNew.Ton = obj.Ton - val;
                obj.Ton = val;

                objNew.CBM = $scope.ToFix3(objNew.ExchangeCBM * objNew.Ton / objNew.ExchangeTon);
                objNew.Quantity = $scope.ToFix3(objNew.ExchangeQuantity * objNew.Ton / objNew.ExchangeTon);
                obj.CBM = $scope.ToFix3(obj.ExchangeCBM * obj.Ton / obj.ExchangeTon);
                obj.Quantity = $scope.ToFix3(obj.ExchangeQuantity * obj.Ton / obj.ExchangeTon);

                var index = $scope.splitGop_Grid.dataSource.data().indexOf(obj);
                $scope.splitGop_Grid.dataSource.data().splice(index, 0, objNew.toJSON());
            }
        }
    };

    $scope.gopSplitCBM_Options = {
        min: 0, spinners: false, decimals: Common.Number.DI_Decimals, culture: "en-US", format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.IsSplit = true;
                var val = this.value();
                var objNew = angular.copy(obj);
                objNew.CBM = obj.CBM - val;
                obj.CBM = val;

                objNew.Ton = $scope.ToFix3(objNew.ExchangeTon * objNew.CBM / objNew.ExchangeCBM);
                objNew.Quantity = $scope.ToFix3(objNew.ExchangeQuantity * objNew.CBM / objNew.ExchangeCBM);
                obj.Ton = $scope.ToFix3(obj.ExchangeTon * obj.CBM / obj.ExchangeCBM);
                obj.Quantity = $scope.ToFix3(obj.ExchangeQuantity * obj.CBM / obj.ExchangeCBM);

                var index = $scope.splitGop_Grid.dataSource.data().indexOf(obj);
                $scope.splitGop_Grid.dataSource.data().splice(index, 0, objNew.toJSON());
            }
        }
    };

    $scope.gopSplitQuantity_Options = {
        min: 0, spinners: false, decimals: Common.Number.DI_Decimals, culture: "en-US", format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.IsSplit = true;
                var val = this.value();
                var objNew = angular.copy(obj);
                objNew.Quantity = obj.Quantity - val;
                obj.Quantity = val;

                objNew.CBM = $scope.ToFix3(objNew.ExchangeCBM * objNew.Quantity / objNew.ExchangeQuantity);
                objNew.Ton = $scope.ToFix3(objNew.ExchangeTon * objNew.Quantity / objNew.ExchangeQuantity);
                obj.CBM = $scope.ToFix3(obj.ExchangeCBM * obj.Quantity / obj.ExchangeQuantity);
                obj.Ton = $scope.ToFix3(obj.ExchangeTon * obj.Quantity / obj.ExchangeQuantity);

                var index = $scope.splitGop_Grid.dataSource.data().indexOf(obj);
                $scope.splitGop_Grid.dataSource.data().splice(index, 0, objNew.toJSON());
            }
        }
    };

    $scope.gopSplitTOMaster_Options = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 1,
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.TOMasterID = this.value();
                $scope.splitGop_Grid.dataSource.sync();
            }
        }
    };

    $scope.FTLGroupProduct_Merge = function ($event, item, grid) {
        $event.preventDefault();

        $($event.target).closest('td').find('.btn-merge').hide();
        $($event.target).closest('td').find('.btn-merge-ok').show();
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                $(tr).find('.btn-merge').hide();
                var chk = $(tr).find('.chk-select-to-merge');
                chk.prop('checked', '');
                chk.show();
            }
        })
    }

    $scope.FTLGroupProduct_Merge_OK = function ($event, item, grid) {
        $event.preventDefault();

        var flag = false;
        var data = [item.ID];
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                var chk = $(tr).find('.chk-select-to-merge');
                if (chk.prop('checked')) {
                    data.push(o.uid);
                    flag = true;
                }
            }
        })

        if (flag) {
            var isAll = true; //Check All
            $($event.target).closest('td').find('.btn-merge').show();
            $($event.target).closest('td').find('.btn-merge-ok').hide();
            var dataSource = grid.dataSource;
            Common.Data.Each(grid.items(), function (tr) {
                var o = grid.dataItem(tr);
                if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                    if (data.indexOf(o.uid) > -1) {
                        item.Ton += o.Ton;
                        item.CBM += o.CBM;
                        item.Quantity += o.Quantity;
                        dataSource.remove(o);
                    } else {
                        isAll = false;
                    }
                }
            })
            if (isAll) {
                item.IsSplit = false;
            }
            dataSource.sync();
        } else {
            $($event.target).closest('td').find('.btn-merge').show();
            $($event.target).closest('td').find('.btn-merge-ok').hide();
            Common.Data.Each(grid.items(), function (tr) {
                var o = grid.dataItem(tr);
                if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                    var chk = $(tr).find('.chk-select-to-merge');
                    chk.hide();
                    $(tr).find('.btn-merge').show();
                }
            })
        }
    }

    $scope.FTLSplit_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var temp = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.TOMasterID > 0 && temp.indexOf(o.TOMasterID) == -1) {
                temp.push(o.TOMasterID);
            }
        })
        if (temp.length > 1) {
            $rootScope.Message({
                Msg: 'Xác nhận lưu?',
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIViewOnMapV3.URL.FTLSplit,
                        data: { toMasterID: $scope.TripItem.ID, dataGop: grid.dataSource.data() },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                Common.Services.Error(res, function (res) {
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                                    $scope.ResetTrip(true);
                                    win.close();
                                    $rootScope.IsLoading = false;
                                });
                            });
                        }
                    });
                }
            });
        } else {
            win.close();
        }
    }

    //#endregion
    
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

    $scope.SwitchView_Click = function ($event) {
        $event.preventDefault();

        try {
            var view = $state.params.view;
            if (view == 'ver') {
                Common.Cookie.Clear("DIViewOnMapV3_View");
                $state.go("main.OPSAppointment.DIViewOnMapV3", { view: undefined });
            } else {
                Common.Cookie.Set("DIViewOnMapV3_View", 'ver');
                $state.go("main.OPSAppointment.DIViewOnMapV3", { view: 'ver' });
            }
        } catch (e) {
        }
    }

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

        $scope.TripItem.VehicleID = $scope.VehicleItem.ID;
        $scope.TripItem.VehicleNo = $scope.VehicleItem.Regno;
        $scope.TripItem.VehicleMaxWeight = $scope.VehicleItem.MaxWeight;

        openMapV2.Close();
    }
    
    $scope.ConvertLatLng = function (value) {
        var str = parseInt(value, 10);
        var deg = value - str;
        deg = Math.round(deg * 60);

        return str + "*" + deg;
    }

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
                $scope.vehicle_Grid.dataSource.read();
            } else {
                $scope.vehicleByVendor_Grid.dataSource.read();
            }
        }
    })

    $scope.new_trip_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV3.URL.DITOGroupProduct_List,
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
        height: '99%', groupable: false, pageable: Common.PageSize, columnMenu: false, resizable: true, reorderable: true, sortable: true, filterable: { mode: 'row' }, auboBind: false,
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
            {
                field: 'IsWarning', width: 100, title: 'Cảnh báo', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false
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
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.ToMon,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.ToOPS,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.Delete,
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
    $scope.NewTripDetail = false;

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
                    method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Check4Update,
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
                                            method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Group_Quantity,
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
                            method: _OPSAppointment_DIViewOnMapV3.URL.Split,
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
                    method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Check4Update,
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
                                            method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Group_Quantity,
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
                            method: _OPSAppointment_DIViewOnMapV3.URL.Split,
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
                    method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Check4Update,
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
                                            method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Group_Quantity,
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
                            method: _OPSAppointment_DIViewOnMapV3.URL.Split,
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
            method: _OPSAppointment_DIViewOnMapV3.URL.Order_List,
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
        pageable: Common.PageSize, height: '99%', groupable: false, sortable: true, columnMenu: false, auboBind: false,
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
            method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_GroupProduct_List,
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
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, auboBind: false,
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
                                LocationStartID: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartID,
                                LocationStartName: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartName,
                                LocationEndID: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndID,
                                LocationEndName: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndName,
                                LocationStartLat: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartLat,
                                LocationStartLng: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationStartLng,
                                LocationEndLat: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndLat,
                                LocationEndLng: _OPSAppointment_DIViewOnMapV3.Data.Location.LocationEndLng,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Check4Consolidate,
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
                                            method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Group,
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
                    method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_Group,
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
                method: _OPSAppointment_DIViewOnMapV3.URL.TripByID,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.CheckVehicleAvailable,
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
            method: _OPSAppointment_DIViewOnMapV3.URL.VehicleVendor_List,
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
            method: _OPSAppointment_DIViewOnMapV3.URL.DriverVendor_List,
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
                            method: _OPSAppointment_DIViewOnMapV3.URL.Save,
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
                            method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update,
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
                    method: _OPSAppointment_DIViewOnMapV3.URL.Delete,
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
            method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_GroupProduct_List,
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
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true,
        reorderable: true, sortable: true, filterable: { mode: 'row' }, selectable: true, auboBind: false,
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
            var schedule = this;
            $scope.TimeLineToMonAvailable = false;
            $scope.TimeLineToOpsAvailable = false;
            $(schedule.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.IsNewTimeLineBound == false && $scope.IsShowNewTrip == true) {
                    $scope.IsNewTimeLineBound = true;
                    schedule.element.find('.k-nav-today a').trigger('click');
                } else if ($scope.IsNewTimeLineBound == true && $scope.IsShowNewTrip == true) {
                    var data = schedule.dataSource.data();
                    Common.Data.Each(schedule.items(), function (o) {
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

                    schedule.element.find('.chk_vehicle_timeline').each(function () {
                        $(this).change(function (e) {
                            var uid = $(e.target).data('uid');
                            var flag = $(e.target).prop('checked');
                            var data = schedule.resources[0].dataSource.data();
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

                            var view = schedule.view();
                            var fDate = view.startDate();
                            var tDate = view.endDate();
                            $scope.NewTimeLineToMonAvailable = false;
                            $scope.NewTimeLineToOpsAvailable = false;
                            Common.Data.Each(schedule.dataSource.data(), function (o) {
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
                    Common.Data.Each(schedule.resources[0].dataSource.data(), function (o) {
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
                                    schedule.element.find('.chk_vehicle_timeline').each(function (chk) {
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.DI2View_Master_Update_TimeLine,
                        data: { mID: obj.id, vehicleID: parseInt(obj.field) || parseInt(obj.field[0]), ETD: obj.start, ETA: obj.end },
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
                        error: function (res) {
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
            method: _OPSAppointment_DIViewOnMapV3.URL.VehicleVendor_List,
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
        pageable: false, height: '99%', groupable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: true, auboBind: false,
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && $scope.NewTimeLineVehicleData.indexOf(item.ID) > -1) {
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
    }

    $scope.NewTimeLineVehicleSelect_Choose = function ($event, grid) {
        var tr = $($event.target).closest('tr'), item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
            $scope.NewTimeLineVehicleData.push(item.ID);
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
            if ($scope.NewTimeLineVehicleData.indexOf(item.ID) > -1) {
                $scope.NewTimeLineVehicleData.splice($scope.NewTimeLineVehicleData.indexOf(item.ID), 1)
            }
        }
    }

    $scope.NewTimeLineVehicleSelect_Click = function ($event, grid, win) {
        $event.preventDefault();
        grid.dataSource.read();
        win.center().open();
        $scope.NewTimeLineVehicleDataTemp = $.extend(true, [], $scope.NewTimeLineVehicleData);
    }

    $scope.NewTimeLineVehicleSelect_OK_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.NewTimeLineVehicleData.sort().toString() != $scope.NewTimeLineVehicleDataTemp.sort().toString()) {
            $scope.NewTimeLineVehicleDataTemp = $.extend(true, [], $scope.NewTimeLineVehicleData);
            $scope.LoadNewTimeLineData();
        }
        win.close();
    }

    $scope.LoadNewTimeLineData = function () {
        $scope.IsNewTimeLineBound = false;
        $scope.NewViewTripLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV3.URL.Schedule_Data,
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
                method: _OPSAppointment_DIViewOnMapV3.URL.TripByID,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.Delete,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.ToMon,
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
                        method: _OPSAppointment_DIViewOnMapV3.URL.ToOPS,
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
        $scope.NewTripDateRequest = { fDate: null, tDate: null };
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
            if ($scope.indexMap == null) {
                openMapV2.hasMap = $scope.ConfigView.showMap;
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
                            case 'Vehicle':
                                $scope.LocationItem = null;
                                $scope.VehicleItem = i.Item;

                                Common.Data.Each($scope.vehicle_Grid.items(), function (tr) {
                                    var item = $scope.vehicle_Grid.dataItem(tr);
                                    $(tr).removeClass('k-state-selected');
                                    if (Common.HasValue(item) && item.ID == $scope.VehicleItem.ID) {
                                        $(tr).addClass('k-state-selected');
                                    }
                                })
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
            $($scope.conSplitter.element).find('.k-splitbar:last').show();
            $scope.conSplitter.expand(panel);
        } else {
            $($scope.conSplitter.element).find('.k-splitbar:last').hide();
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
                case 'TimeLineTODetail':
                    $scope.TimeLineTripDetail = false;
                    break;
                case 'NewTODetail':
                    $scope.NewTripDetail = false;
                    break;
                case 'TimeLineTODetail':
                    $scope.TimeLineTripDetail = false;
                    break;
                case 'TimeLineVehicleSelect':
                    $scope.NewTimeLineVehicleData = $.extend(true, [], $scope.NewTimeLineVehicleDataTemp);
                    break;
            }
            win.close();
        } catch (e) {
        }
    }

    $scope.On_Close = function (code) {
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
            case 'TimeLineVehicleSelect':
                $scope.NewTimeLineVehicleData = $.extend(true, [], $scope.NewTimeLineVehicleDataTemp);
                break;
        }
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.OPSAppointmentDI,
            event: $event, grid: grid,
            current: $state.current,
            customview: true, customcache: "view.OPSAppointment.DIViewOnMap",
            callback: function (e) {
                if (e) {
                    Common.Cookie.Set("view.OPSAppointment.DIViewOnMap", $state.current.name);
                } else {
                    Common.Cookie.Set("view.OPSAppointment.DIViewOnMap", 'main.OPSAppointment.DIViewOnMapV2');
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
    };

    //#endregion

    $rootScope.IsLoading = false;
    $scope.InitComplete = true;
    //#endregion
}]);