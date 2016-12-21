/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _MONMonitorDN_Index = {
    URL: {
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
        CUSGOP_List: "DIMonitorMaster_CUSGOPList"
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

    },
    Map: {
        Map: null,
        Marker: [],
        MapInfo: null
    },
}

angular.module('myapp').controller('MONMonitor_IndexDNCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout','openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    Common.Log('MONMonitor_IndexDNCtrl');
    $rootScope.IsLoading = false;

    //#region Model
    $scope.Map_OnRight = true;
    $scope.Show_SearchTools = true;
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
        SL:true
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
    $scope.SL_Return_Item = {};
    $scope.SL_BBGN_Item = {};
    $scope.SL_Old = {
        BBGN: 0,
        Return: 0
    }
    $scope.CusLocaitonID = 0;
    $scope.LocationData = {};
    $scope.ObjLocation= {
        Country: [],
        Province: [],
        District: []
    };
    $scope.SOItem = {
        CustomerID: 0,
    }
    $scope.DataCUSProduct = [];
    $scope.DataDITOGroupProduct = [];
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
            { collapsible: false, resizable: false, size: '35px' },
            { collapsible: false, resizable: false, size: '28px' },
            { collapsible: false, resizable: false },
        ],
        resize: function (e) {
            $timeout(function () {
                openMap.Resize();
            },1)
            
        }
    };

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
            method: "DIMonitorDN_MasterDNList",
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
                    ColorClass: { type: 'string' },
                    IsChoose: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SRMasterDN_Grid,SRMasterDN_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SRMasterDN_Grid,SRMasterDN_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
           {
               field: 'OrderCode', width: 100,
               title: '{{RS.ORDOrder.Code}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=OrderCode != null ? OrderCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DNCode', width: 100,
               title: '{{RS.OPSDITOGroupProduct.DNCode}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=DNCode != null ? DNCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'SOCode', width: 100,
               title: '{{RS.ORDGroupProduct.SOCode}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=SOCode != null ? SOCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'VehicleNo', width: 100,
               title: '{{RS.CATVehicle.RegNo}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=VehicleNo != null ? VehicleNo : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'ETD', width: 120,
               title: '{{RS.OPSDITOMaster.ETD}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=ETD==null?"":Common.Date.FromJsonDMYHM(ETD)#</a>',
               filterable: { cell: { operator: 'gte', showOperators: false } }
           },
           {
               field: 'ETA', width: 120,
               title: '{{RS.OPSDITOMaster.ETA}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=ETA==null?"":Common.Date.FromJsonDMYHM(ETA)#</a>',
               filterable: { cell: { operator: 'gte', showOperators: false } }
           },
           {
               field: 'GroupOfProductName', width: 100,
               title: '{{RS.CUSGroupOfProduct.GroupName}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=GroupOfProductName != null ? GroupOfProductName : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToProvince', width: 100,
               title: '{{RS.CATProvince.ProvinceName}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=LocationToProvince != null ? LocationToProvince : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToDistrict', width: 100,
               title: '{{RS.CATDistrict.DistrictName}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=LocationToDistrict != null ? LocationToDistrict : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToAddress', width: 100,
               title: '{{RS.CATLocation.Address}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=LocationToAddress != null ? LocationToAddress : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'RequestDate', width: 120,
               title: '{{RS.ORDOrder.RequestDate}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=RequestDate==null?"":Common.Date.FromJsonDMYHM(RequestDate) #</a>',
               filterable: { cell: { operator: 'gte', showOperators: false } }
           },
           {
               field: 'LocationToCode', width: 100,
               title: '{{RS.CUSLocation.Code}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=LocationToCode != null ? LocationToCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToName', width: 150,
               title: '{{RS.CATLocation.Location}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=LocationToName != null ? LocationToName : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Description', width: 150,
               title: '{{RS.ORDGroupProduct.Description}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=Description != null ? Description : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DITOMasterCode', width: 100,
               title: '{{RS.OPSDITOMaster.Code}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=DITOMasterCode != null ? DITOMasterCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'VendorCode', width: 100,
               title: '{{RS.CUSCustomer.Code}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=VendorCode != null ? VendorCode : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'CustomerName', width: 100,
               title: '{{RS.CUSCustomer.CustomerName}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=CustomerName != null ? CustomerName : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'VendorName', width: 100,
               title: 'Nhà vận tải',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=VendorName != null ? VendorName : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DriverName', width: 100,
               title: '{{RS.OPSDITOMaster.DriverName1}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=DriverName != null ? DriverName : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DriverTel', width: 100,
               title: '{{RS.OPSDITOMaster.DriverTel1}}',
               template: '<a ng-click="DN_Click($event)" title="Xem chi tiết" style="cursor:pointer">#=DriverTel != null ? DriverTel : ""#</a>',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
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

            if (Common.HasValue($state.params.masterid) && $state.params.masterid > 0) {
                $scope._id = $state.params.masterid;

                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitorDN_Index.URL.SRMasterDNGet,
                    data: { id: $scope._id },
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
                        $scope.LoadGOPReturnData();
                        //$scope.MapRefresh();
                        $scope.MapVehicleLocation();

                        $scope.WinToOpen();
                    }
                })
            }
        }
    };

    if (!$scope.DataRequest.IsRecieved) {
        var f = { 'field': 'ColorClass', 'operator': 'neq', 'value': "green" };
        $scope.SRMasterDN_GridOptions.dataSource.filter(f);
    }

    $scope.TO_WinOptions = {
        width: '900px', height: '550px',
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
            { collapsible: false, resizable: false, size: '140px' },
            { collapsible: false, resizable: false, size: '170px' },
            { collapsible: false, resizable: false },
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
        columns: [
            {
                title: "Họ tên", field: 'Name', width: 250,
                template: '<input class="k-textbox txt" name="Name" placeholder="#=Role#" value="#=Name!=null?Name:\"\"#" style="width: 100%;"></input>'
            },
            {
                title: "Số điện thoại", field: 'Tel', width: 250,
                template: '<input class="k-textbox txt" name="Tel" value="#=Tel!=null?Tel:\"\"#" style="width: 100%;"></input>'
            },
            {
                title: "CMND", field: 'Card', width: 465,
                template: '<input class="k-textbox txt" name="Card" value="#=Card!=null?Card:\"\"#" style="width: 100%;"></input>'
            },
            { title: ' ', filterable: false, sortable: false }
        ], editable: 'incell', dataBound: function (e) {
            Common.Log("Driver_GridOptions Bound missing");
        }
    }

    //#region thong tin hang hoa
    $scope.TODetail_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitorDN_Index.URL.SRWinToDetailList,
            model: { id: 'ID', fields: { ID: { type: 'number' } } },
            readparam: function () { return { masterID: $scope._id } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        columns: [
            {
                field: '', width: 50, title: '',
                template: '<a ng-click="EditSOLocation($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false
            },
            {
                field: '', width: 50, title: 'TH',
                template: '<input type="checkbox" ng-model="dataItem.HasCashCollect" ng-click="HasCashCollectChange($event,dataItem)" />',
                filterable: false
            },
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
                title: 'Nhóm SP',
                field: 'GroupProductName', width: "150px",
            },
            {
                field: 'TonTranfer', width: 75,
                title: 'Tân',
                //template: '#=WinToDetailTemplate(1, "TonTranfer", TonTranfer, UpdateTypeID) #'
            },
            {
                field: 'CBMTranfer', width: 75,
                title: 'Khối',
                //template: '#=WinToDetailTemplate(2, "CBMTranfer", CBMTranfer, UpdateTypeID) #'
            },
            {
                field: 'QuantityTranfer', width: 75,
                title: 'SL',
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
        ], dataBound: function (e) {
            Common.Log("me.WinToGridDetailDataBound missing");
        },
        reorderable: true
    };

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
            pars: {e:e},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                $($scope.curTarget).prop('checked', item.HasCashCollect);
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "DIMonitorMaster_UpdateCashCollection",
                    data: { ordGroupID: item.OrderGroupProductID, value: item.HasCashCollect },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            },
            Close: function () {
                
            }
        });
        
    }

    $scope.EditSOLocation = function (e, item) {
        e.preventDefault();

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
                field: 'CusPartrCode', title: 'Mã đối tác', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ', width: '150px',
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
            data: { cuslocationID: $scope.SOItem.LocationToIDNew, OrderGroupProductID: $scope.SOItem.OrderGroupProductID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.TODetail_Grid.dataSource.read();
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

    //#endregion
    $scope.Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitorDN_Index.URL.SRWinToDITOLocationList,
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
            method: _MONMonitorDN_Index.URL.MSTroubleList,
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
                       options.model.TroubleCostStatusID = _MONMonitorDN_Index.Data._trouble1stStatus;
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
            method: _MONMonitorDN_Index.URL.MSTroubleList,
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
                       options.model.TroubleCostStatusID = _MONMonitorDN_Index.Data._trouble1stStatus;
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
            method: _MONMonitorDN_Index.URL.TroubleNotin_List,
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
        columns: [
              { field: 'CreateSortOrder', width: '50px', template: '<input type="text" class="txtCreateSortOrder cus-number" value="#=CreateSortOrder==0?\'\':CreateSortOrder#" />', headerTemplate: '<a id="btnWinHasDNGridExpand" class="k-button" href="/">+</a> ', groupHeaderTemplate: '<i class="HasDNGridGroup" tabid="#=value#"></i>', sortable: true, filterable: false, menu: false },
              { field: 'SOCode', width: '80px', title: 'SO', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'IsOrigin', width: '30px', title: 'g', template: '<input type="checkbox" disabled="disabled" #= IsOrigin ? checked="checked" : ""  # />', sortable: false, filterable: false },
              { field: 'DNCode', width: '90px', title: 'DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { title: ' ', width: '45px', template: '<a href="" class="k-button btnAdd" style="display:none">M</a><a href="" class="k-button btnAddSave" style="display:none">S</a><input type="checkbox" class="chkAdd" style="display:none"/>', filterable: false, menu: false, sortable: false },
              { field: 'Kg', width: '80px', title: 'KG', template: '<input type="text" class="txtKg cus-number #=TypeEditID==1?\'allow\':\'notallow\'#" max="#= Kg#" value="#= Kg#" />', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
              { field: 'Ton', width: '60px', title: 'Tấn', template: '<input type="text" class="txtTon cus-number #=TypeEditID==1?\'allow\':\'notallow\'#" max="#= Ton#" value="#= Ton#" />', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
              { field: 'CBM', width: '60px', title: 'Khối', template: '<input type="text" class="txtCBM cus-number #=TypeEditID==2?\'allow\':\'notallow\'#" max="#= CBM#" value="#= CBM#" />', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
              { field: 'Quantity', width: '60px', title: 'S.l', template: '<input type="text" class="txtQuantity cus-number #=TypeEditID==3?\'allow\':\'notallow\'#" max="#= Quantity#" value="#= Quantity#" />', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
              { field: 'DistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: false },
              { field: 'Address', width: '200px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'ProvinceName', width: '100px', title: 'Tỉnh / Thành', sortable: true, filterable: false },
              { field: 'TranferItem', width: '80px', title: 'Vận chuyển', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'RequestDateEmpty', width: '100px', title: 'Ngày gửi', template: "#=Common.Date.FromJsonDDMMHM(RequestDate)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
              { field: 'OrderCode', width: '80px', title: 'List', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'ETD', width: '100px', title: 'ETD', template: "#=Common.Date.FromJsonDDMMHM(ETD)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
              { field: 'PartnerCode', width: '100px', title: 'Mã NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'PartnerName', width: '100px', title: 'NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'FromCode', width: '100px', title: 'Mã l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'FromAddress', width: '150px', title: 'Điểm l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'FromProvince', width: '100px', title: 'Tỉnh thành l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'FromDistrict', width: '100px', title: 'Quận huyện l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'Description', width: '100px', title: 'Ghi chú', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { field: 'CUSRoutingCode', width: '180px', title: 'Cung đường', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
              { title: ' ', filterable: false, menu: false, sortable: false }
        ],
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
                        selItem = _MONMonitorDN_Index.Data._dataLocation[0];
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
                }
            },
            readparam: function () { return { masterID: $scope._id } }
        }),
        toolbar: kendo.template($('#return-grid-toolbar').html()),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: true,
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
                title: 'Mã ĐH',
                field: 'OrderCode', width: "100px",
            },
            {
                title: 'Nhóm SP', field: 'GroupProductID', template: '#=GroupProductName#', width: "150px", editor: function (container, options) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitor_Index.URL.CUSGOP_List,
                        data: { customerid: $scope._id },
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
                title: 'SP', field: 'ProductName', width: "150px",
            },
            {
                field: 'Quantity', width: 75, title: 'SL',
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
            readparam: function () { return { masterID: $scope._id } }
        }),
        toolbar: kendo.template($('#sl-grid-toolbar').html()),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: true,
        columns: [
            {
                title: 'Mã ĐH',field: 'OrderCode', width: "100px",
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
                title: 'SL giao', field: 'QuantityBBGN', width: 75, editor: function (container, options) {
                    var input = $('<input type="number" class="k-input k-textbox" name="QuantityBBGN">');
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
                title: 'Tỉnh thành',field: 'LocationToProvince', width: "125px",
            },
            {
                title: 'Quận huyện',field: 'LocationToDistrict', width: "125px",
            },
            {
                title: 'Địa chỉ',field: 'LocationToAddress', width: "200px",
            },
            { title: ' ', filterable: false, sortable: false }
        ],
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

    $scope.CUSGroupofProduct_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        change: function (e) {
            $scope.CUSProduct_Cbb.text("");
            $scope.CUSProduct_Cbb.open();
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

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
    }

    $scope.SL_Product_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'ProductName',
        dataValueField: 'ID',
        change: function (e) {
            var item=this.dataItem(e.item);
            if(Common.HasValue(item)){
                $scope.SL_Old.BBGN = item.QuantityBBGN_Old;
                $scope.SL_Old.Return = item.QuantityReturn_Old;
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

    $scope.numPrice_Options = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    //#endregion

    //#region Event

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

    $scope.order_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
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
        $scope.ItemGOPRturn.MasterID = $scope._id;
        $scope.ItemGOPRturn.ProductID = $scope.ItemGOPRturn.ProductID > 0 ? $scope.ItemGOPRturn.ProductID : 0;
        if ($scope.ItemGOPRturn.Quantity > 0) {
            if (vform()) {
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "DIMonitorMaster_GOPReturnAdd",
                    data: { item: $scope.ItemGOPRturn },
                    success: function (res) {
                        $scope.Return_Grid.dataSource.read();
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
        }
        else {
            $rootScope.Message({ Msg: "SL phải lớn hơn 0", NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

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

    $scope.DN_Click = function ($event) {
        $event.preventDefault();
        var grid = $scope.SRMasterDN_Grid;
        var tr = $($event.currentTarget).closest("tr");
        var item = grid.dataItem(tr);
        
        $.each($scope.SRMasterDN_Grid.tbody.find(".k-state-selected"), function (i, v) {
            $(v).removeClass("k-state-selected")
        });

        tr.addClass("k-state-selected");

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
            method: _MONMonitorDN_Index.URL.SRMasterDNGet,
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
                $scope.LoadGOPReturnData();
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
            $scope.Show_BtnWinTO_Update = true;
        }
        else {
            $scope.Show_BtnWinTO_Update = false;
        }

        $scope.Vehicle_Cbb.enable($scope.CurrentMaster.IsEditable);

        var vendorID = $scope.CurrentMaster.VendorID > 0 ? $scope.CurrentMaster.VendorID : -1;
        if (vendorID > 0) {
            $scope.Vehicle_CbbOptions.dataSource.data(_MONMonitorDN_Index.Data._dataVehicleByVEN[vendorID]);
        } else {
            $scope.Vehicle_CbbOptions.dataSource.data(_MONMonitorDN_Index.Data._dataVehicleHome);
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
            $scope.CurrentMaster.Driver3,
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
        $scope.SL_Grid.dataSource.read();
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
            method: _MONMonitorDN_Index.URL.MSRoutingList,
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
        $scope.CurrentMaster.VehicleID = 0;
        if ($scope.CurrentMaster.IsVehicleVendor) {
            var dataDriver = $scope.Driver_GridOptions.dataSource.data();
            $scope.CurrentMaster.Driver1 = dataDriver[0];
            $scope.CurrentMaster.Driver2 = dataDriver[1];
            $scope.CurrentMaster.Driver3 = dataDriver[2];
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
                method: _MONMonitorDN_Index.URL.SRWinToUpdate,
                data: { item: $scope.CurrentMaster, lstDetail: dataDetail },
                success: function (res) {
                    $scope.TO_Win.close();
                    $scope.SRMasterDN_Grid.dataSource.read();
                    $rootScope.Message({ Msg: "Thành công", NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitorDN_Index.URL.TruckList,
                        data: {},
                        success: function (res) {
                            _MONMonitorDN_Index.Data._dataTruck = res.Data;
                            if (_MONMonitorDN_Index.Data._dataTruck.length > 0 && _MONMonitorDN_Index.Data._dataVendor.length > 0)
                                $scope.InitDataVendorVehicle();
                        }
                    })
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

        var lstMasterID = [];
        angular.forEach($scope.SRMasterDN_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && !o.IsComplete)
                lstMasterID.push(o.DITOMasterID);
        })
        if (Common.HasValue($scope.CurrentMaster) && lstMasterID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh chuyến?',
                pars: {},
                Ok: function (pars) {

                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitorDN_Index.URL.SRVehicleComplete,
                        data: { lstMasterID: lstMasterID },
                        success: function (res) {
                            $scope.Show_SRVehicleMasterComplete = false;
                            $scope.Show_SRVehicleCompleteDN = false;
                            $scope.SRMasterDN_Grid.dataSource.read();
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
        angular.forEach($scope.SRMasterDN_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && !o.IsDNComplete)
                lstID.push(o.ID);
        })
        if (Common.HasValue($scope.CurrentMaster) && lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành nhanh DN?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitorDN_Index.URL.SRVehicleCompleteDN,
                        data: { lstID: lstID },
                        success: function (res) {
                            $scope.Show_SRVehicleCompleteDN = false;
                            $scope.SRMasterDN_Grid.dataSource.read();
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
        angular.forEach($scope.SRMasterDN_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && o.IsComplete)
                lstMasterID.push(o.DITOMasterID);
        })
        if (Common.HasValue($scope.CurrentMaster) && lstMasterID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận trả về trạng thái kế hoạch?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitorDN_Index.URL.SRVehicleRevert,
                        data: { lstMasterID: lstMasterID },
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
        else {
            $rootScope.Message({ Msg: 'Không có chuyến để trả về!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Revert_MasterDN = function ($event) {
        Common.Log("Revert_MasterDN");
        var lstID = [];
        angular.forEach($scope.SRMasterDN_Grid.dataSource.data(), function (o, i) {
            if (o.IsChoose && o.IsDNComplete)
                lstID.push(o.ID);
        })
        if (Common.HasValue($scope.CurrentMaster) && lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận trả về trạng thái kế hoạch?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONMonitorDN_Index.URL.SRVehicleRevertDN,
                        data: { lstID: lstID },
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
        else {
            $rootScope.Message({ Msg: 'Không có chuyến để trả về!', NotifyType: Common.Message.NotifyType.ERROR });
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
            method: _MONMonitorDN_Index.URL.SRVehicleNoDNList,
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
                    method: _MONMonitorDN_Index.URL.SRVehicleNoDNOrderList,
                    data: param1,
                    success: function (res) {

                        _MONMonitorDN_Index.Data._dataHasDN = [];
                        $.each(res, function (i, v) {
                            if (v.TypeID < 2) {
                                if (Common.HasValue(v.RequestDate)) {
                                    v.RequestDate = Common.Date.FromJson(v.RequestDate);
                                    v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                                }
                                v.Kg = v.Ton * 1000;

                                if (v.CreateSortOrder > 0)
                                    v.CreateSortOrder = datafix[v.CreateSortOrder];

                                _MONMonitorDN_Index.Data._dataHasDN.push(v);
                            }
                        });
                        $scope.NODN_Win.center().open();
                        $scope.NODN_Grid.dataSource.data(_MONMonitorDN_Index.Data._dataHasDN);
                    }
                })

                var index = 1;
                $.each(res, function (i, v) {
                    if (v.TypeID < 2) {
                        v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                        v.Kg = 0;
                        _MONMonitorDN_Index.Data._dataHasDNSort[index] = v;
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
            method: _MONMonitorDN_Index.URL.SRWinToDITOLocationUpdate,
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
            method: _MONMonitorDN_Index.URL.MapLocationChangeStatus,
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
                google.maps.event.trigger(_MONMonitorDN_Index.Data._mapMarkers[$scope._locationID], 'click');
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
            method: _MONMonitorDN_Index.URL.MSRoutingList,
            data: { masterID: $scope._id, locationID: $scope._locationID },
            success: function (res) {
                _MONMonitorDN_Index.Data._dataMsRouting = res.Data;
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
                method: _MONMonitorDN_Index.URL.MSTroubleUpdateAll,
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
        //    method: _MONMonitorDN_Index.URL.MSTroubleGet,
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
                method: _MONMonitorDN_Index.URL.MSTroubleUpdate,
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
            method: _MONMonitorDN_Index.URL.MSTroubleUpdate,
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
                method: _MONMonitorDN_Index.URL.TroubleSaveList,
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
                    method: _MONMonitorDN_Index.URL.MSTroubleDelete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Trouble_Grid.dataSource.read();
                    }
                })
            }
        });
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
                method: _MONMonitorDN_Index.URL.SRWinToDITOLocationUpdate,
                data: { lstDITOLocation: data },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Đã cập nhật!', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Location_Grid.dataSource.read();
                    win.close();
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
                if (item.GroupProductID > 0 && (item.Quantity > 0))
                    data.push(item);
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
    }
    //#endregion

    //#region Init
    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitorDN_Index.URL.TruckList,
        data: {},
        success: function (res) {
            _MONMonitorDN_Index.Data._dataTruck = res.Data;
            if (_MONMonitorDN_Index.Data._dataTruck.length > 0 && _MONMonitorDN_Index.Data._dataVendor.length > 0)
                $scope.InitDataVendorVehicle();
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitorDN_Index.URL.DriverList,
        data: {},
        success: function (res) {
            $scope._dataDriver = res.Data;
            $scope.GroupDriver_CbbOptions.dataSource.data(res.Data);
            $scope.Driver1_CbbOptions.dataSource.data(res.Data);
            $scope.Driver2_CbbOptions.dataSource.data(res.Data);
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
        method: _MONMonitorDN_Index.URL.LocationList,
        data: {},
        success: function (res) {
            _MONMonitorDN_Index.Data._dataLocation = res.Data;
            $scope.LocationTemplateOptions.dataSource.data(res.Data);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitorDN_Index.URL.TroubleList,
        data: {isCo:false},
        success: function (res) {
            $scope.GroupOfTrouble_CbbOptions.dataSource.data(res.Data);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitorDN_Index.URL.VendorList,
        data: {},
        success: function (res) {
            res.Data.push({
                VendorID: -1,
                VendorName: 'Xe nhà'
            });
            _MONMonitorDN_Index.Data._dataVendor = res.Data;
            if (_MONMonitorDN_Index.Data._dataTruck.length > 0 && _MONMonitorDN_Index.Data._dataVendor.length > 0)
                $scope.InitDataVendorVehicle();
            $scope.GroupVender_CbbOptions.dataSource.data(res.Data);
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "DIMonitor_ListTypeOfDriver",
        data: {},
        success: function (res) {
            $scope.TypeOfDriver_CbbOptions.dataSource.data(res);
        }
    })

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_TroubleCostStatus,
        success: function (res) {
            $scope.TroubleCostStatus_CbbOptions.dataSource.data(res);
            if (Common.HasValue(res[0])) {
                _MONMonitorDN_Index.Data._trouble1stStatus = res[0].ID;
            }
            $scope.IsLoading = false;
        }
    });

    $scope.Init_CK = function () {

        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = Common.Date.AddDay(new Date(), -10);
            $scope.Search.DateTo = Common.Date.AddDay(new Date(), 2);
            $scope.Search.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Search.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Search.RouteInDay = 3;
        }
        else {
            $scope.Search.DateFrom = Common.Date.AddDay(new Date(), -10);
            $scope.Search.DateTo = Common.Date.AddDay(new Date(), 2);
            $scope.Search.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Search.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Search.RouteInDay = 3;
        }
    }
    $scope.Init_CK();

    $timeout(function () {

        $scope.CreateMap();
    }, 500);

    $scope.LoadGOPReturnData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_DITOGroupProductList",
            data: { masterID: $scope._id },
            success: function (res) {
                $scope.DataDITOGroupProduct = res;
                $scope.OPSGroupCbbOptions.dataSource.data(res);
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "DIMonitor_CUSProductList",
            data: { masterID: $scope._id },
            success: function (res) {
                $scope.DataCUSProduct = res;
                $scope.CUSProduct_CbbOptions.dataSource.data(res);
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Index.URL.CUSGOP_List,
            data: { customerid: $scope._id },
            success: function (res) {
                $scope.CUSGroupofProduct_CbbOptions.dataSource.data(res);
            }
        })
    }

    $scope.MapRefresh = function () {
        Common.Log("MapRefresh");

        if (Map.HasMap) {
            if (Common.HasValue(_MONMonitorDN_Index.Map.MapInfo))
                $(_MONMonitorDN_Index.Map.MapInfo).trigger('close');
            $scope.MapDrawMarkers();
            //$scope.MapDrawLines();
            $timeout(function () {
                var showMarkers = [];
                Common.Data.Each(_MONMonitorDN_Index.Data._mapMarkers, function (item) {
                    if (item.getMap() == _MONMonitorDN_Index.Map.Map)
                        showMarkers.push(item);
                })
                Map.FitBounds(_MONMonitorDN_Index.Map.Map, showMarkers);
            }, 500);
        }
    }

    $scope.MapDrawMarkers = function () {
        Common.Log("MapDrawMarkers");

        if (Map.HasMap) {
            Common.Data.Each(_MONMonitorDN_Index.Data._mapMarkers, function (o) {
                o.setMap(null);
                o = null;
            });
            _MONMonitorDN_Index.Data._mapMarkers = [];
            if (Common.HasValue($scope.CurrentMaster)) {
                Common.Data.Each($scope.CurrentMaster.ListLocation, function (item) {
                    if (!Common.HasValue(_MONMonitorDN_Index.Data._mapMarkers[item.ID])) {
                        if (item.Lat > 0 && item.Lng > 0) {
                            var img = Common.String.Format(Map.ImageURL.NumericPoint_Green, item.SortOrder);
                            _MONMonitorDN_Index.Data._mapMarkers[item.ID] = Map.Marker(_MONMonitorDN_Index.Map.Map, item.Location, item.Lat, item.Lng, img, 1, function (sender, event, param) {
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
        //if (Common.HasValue(_MONMonitorDN_Index.Map.MapInfo))
        //    $(_MONMonitorDN_Index.Map.MapInfo).trigger('close');

        //_MONMonitorDN_Index.Map.MapInfo.open(_MONMonitorDN_Index.Map.Map, sender);
        $scope.Map_Info_Win.center().open();
        $scope._locationID = param.ID;

        $scope.txtMapLocation = param.Location + " - " + param.DistrictName + ", " + param.ProvinceName;
        $scope.txtMapAddress = param.Address;
        $scope.lblMapDate = "";
        $scope.dtpSignature = new Date();

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitorDN_Index.URL.MapLocationGetStatus,
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
            if (Common.HasValue(_MONMonitorDN_Index.Data._mapMarkerTruck)) {
                _MONMonitorDN_Index.Data._mapMarkerTruck.setMap(null);
                _MONMonitorDN_Index.Data._mapMarkerTruck = null;
            }
            if ($scope._typeID == 2) {
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitorDN_Index.URL.SRVehicleTimeVehicleLocation,
                    data: { masterID: $scope._id },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Cập nhật tọa độ thành công.', NotifyType: Common.Message.NotifyType.SUCCESS });
                        var img = Common.String.Format(Map.ImageURL.Truck_Orange);
                        Common.Data.Each(res.Data, function (o) {
                            if (o.Lat > 0 && o.Lng > 0) {
                                _MONMonitorDN_Index.Data._mapMarkerTruck = Map.Marker(_MONMonitorDN_Index.Map.Map, o.RegNo, o.Lat, o.Lng, img, 1, function (sender, event, param) {
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
        var lstVehicle = _MONMonitorDN_Index.Data._dataTruck;
        var lstVendor = _MONMonitorDN_Index.Data._dataVendor;
        _MONMonitorDN_Index.Data._dataVehicleByVEN = [];
        _MONMonitorDN_Index.Data._dataVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue(_MONMonitorDN_Index.Data._dataVehicleByVEN[vendorid]))
                        _MONMonitorDN_Index.Data._dataVehicleByVEN[vendorid] = [];
                    _MONMonitorDN_Index.Data._dataVehicleByVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _MONMonitorDN_Index.Data._dataVehicleHome.push(vehicle);
                }
            } else {
                _MONMonitorDN_Index.Data._dataVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_MONMonitorDN_Index.Data._dataVehicleByVEN[vendorid]))
                        _MONMonitorDN_Index.Data._dataVehicleByVEN[vendorid] = [];
                    _MONMonitorDN_Index.Data._dataVehicleByVEN[vendorid].push(vehicle);
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
                    method: _MONMonitorDN_Index.URL.MapLocationGetStatus,
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
        _MONMonitorDN_Index.Data._mapMarkers = [];
        if (Common.HasValue($scope.CurrentMaster)) {
            Common.Data.Each($scope.CurrentMaster.ListLocation, function (item) {
                if (!Common.HasValue(_MONMonitorDN_Index.Data._mapMarkers[item.ID])) {
                    var img = Common.String.Format(openMap.mapImage.NumericPoint_Green, item.SortOrder);
                    var icon = openMap.mapStyle.Icon(img, 1);
                    _MONMonitorDN_Index.Data._mapMarkers[item.ID] = openMap.Marker(item.Lat, item.Lng, "", icon, item);
                    data.push(_MONMonitorDN_Index.Data._mapMarkers[item.ID]);
                }
            })
        }
        if (data.length > 0)
            openMap.FitBound(data);
    }

    //#endregion

    //
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
}])