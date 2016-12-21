/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _FLMAsset_TruckEdit = {
    URL: {
        Get_Truck: 'FLMAsset_Truck_Get',
        Save_Truck: 'FLMAsset_Truck_Update',
        Delete_Truck: 'FLMAsset_Truck_Destroy',

        Get_EQMList: 'FLMAsset_EQM_ReadByAsset',
        Get_EQMPastList: 'Equipment_PastListByVehicleID',
        Get_FuelHistory: 'FLMAsset_FuelHistoryRead',

        Read_Consumption: 'FLMAsset_Consumption_ReadByAsset',
        Save_Consumption: 'FLMAsset_Consumption_UpdateByAsset',
        Delete_Consumption: 'FLMAsset_Consumption_Destroy',
        Read_ConsumptionNotIn: 'FLMAsset_Consumption_NotChooseRead',
        Save_ConsumptionNotIn: 'FLMAsset_Consumption_NotChooseUpdate',

        History_Depreciation: 'FLMAsset_History_DepreciationList',
        History_OPS: 'FLMAsset_History_OPSList',
        History_Repair: 'FLMAsset_History_RepairList',

        Get_ALLDriverList: 'FLMAsset_AllDriverRead',
        Get_TruckDriverList: 'FLMAsset_Truck_Read',
        Get_RentList: 'FLMAsset_RentRead',

        FixedCost_ByAssetList: 'FLMFixedCost_ByAssetList',
        FixedCost_Generate: 'FLMFixedCost_Generate',
        FixedCost_Save: 'FLMFixedCost_Save',

        AssetDelete: 'FLMFixedCost_ByAssetDelete',
        Asset_CheckExpr: 'FLMFixedCost_ByAsset_CheckExpr',

        FeeDefault_List: 'FLMScheduleFeeDefault_List',
        FeeDefault_Get: 'FLMScheduleFeeDefault_Get',
        FeeDefault_Save: 'FLMScheduleFeeDefault_Save',
        FeeDefault_Delete: 'FLMScheduleFeeDefault_Delete',

        LocationAddress_List: 'FLMAsset_Location_List',
        Location_Get: 'FLMAsset_Location_Get',
        Location_Save: 'FLMAsset_Location_Save',
        Location_Delete: 'FLMAsset_Location_Delete',

        FLM_LocationNotIn_Save: 'FLMAsset_LocationNotIn_Save',
    },
    Param: {
        AssetID: -1
    },
    Data: {
        ItemBackup: null,
        Country: [],
        Province: [],
        District:[],
    },
    Map: {
        Map: null,
        Marker: null,
        IconMarker: '/Images/map/truck_green.png'
    }
}

angular.module('myapp').controller('FLMAsset_EditTruckCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    Common.Log('FLMAsset_EditTruckCtrl');

    $rootScope.IsLoading = false;

    $rootScope.Loading.Show();
    $scope.HasChoose = false;
    $scope.IsNewVehicle = true;
    $scope.IsEdited = false;
    $scope.IsShowRadio = true;
    $scope.ExprDay = {};
    $scope.LocationItem = {};
    $scope.locationMap = { Lat: 0, Lng: 0 };
    $scope.mapEdited = false;
    $scope.LocationHasChoose = false;
    var LoadingStep = 10;

    $scope.OPSSearchItem = {
        DateFrom: new Date().addDays(-7),
        DateTo:new Date()
    }

    $scope.RepairSearchItem = {
        DateFrom: new Date().addDays(-7),
        DateTo: new Date()
    }

    _FLMAsset_TruckEdit.Param = $.extend(true, _FLMAsset_TruckEdit.Param, $state.params);
    $scope.TruckItem = null;
    $scope.ConsumptionEdit = null;
    $scope.IsConsumptionEdit = false;
    $scope.IsShowTab = false;
    $scope.hasDelete = false;
    $scope.ItemAssetFee = {};    
    $scope.dataExprDay = [{ ID: 1, name: "", value: "" },
                          { ID: 2, name: "Theo tất cả ngày", value: "[Value]/[TotalDay]" },
                          { ID: 3, name: "Theo ngày hoạt động", value: "[Value]/[TotalDayOn]" }];

    $scope.dataExprInputDay = [{ ID: 1, name: "", value: "" },
                               { ID: 2, name: "Theo tất cả ngày", value: true },
                               { ID: 3, name: "Ngày hoạt động", value: "[IsDayOn]" },
                               { ID: 4, name: "Không phân bổ", value: null },
                               { ID: 5, name: "Khác", value: "" }];

    if (_FLMAsset_TruckEdit.Param.AssetID > 0) { $scope.IsShowTab = true; $scope.IsNewVehicle = false; }

    $rootScope.$watch('Loading.Progress', function (v, n) {
        if ($rootScope.Loading.Progress >= 100) {
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    })

    //#region Tabtrip
    $scope.TabIndex = 1;
    $scope.FLMAsset_EditTruck_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    }
    //#endregion

    //#region ThongTinChung
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_TruckEdit.URL.Get_Truck,
        data: { AssetID: _FLMAsset_TruckEdit.Param.AssetID, RegNo: _FLMAsset_TruckEdit.Param.RegNo },
        success: function (res) {
            $scope.TruckItem = res;
            var flag = false;
            $.each($scope.dataExprInputDay, function (i, v) {
                if (v.value == $scope.TruckItem.ExprInputDay) {
                    flag = true;
                }
            });
            
            if (!flag) {
                $scope.dataExprInputDay[0].value = $scope.TruckItem.ExprInputDay;
            }
            $timeout(function () {
                $scope.cboExprDay_options.dataSource.data($scope.dataExprDay);
                $scope.cboExprInputDay_options.dataSource.data($scope.dataExprInputDay);
            }, 10);
            $rootScope.Loading.Change("Thông tin chung ...", 40);

        }
    });

    $scope.FLMAsset_Truck_SaveClick = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_TruckEdit.URL.Save_Truck,
                data: { item: $scope.TruckItem },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã lưu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $state.go('main.FLMAsset.EditTruck', { AssetID: res, });
                },
                error: function (e) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.FLMAsset_Truck_DelClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu này',
            Ok: function (e) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_TruckEdit.URL.Delete_Truck,
                    data: { item: $scope.TruckItem },
                    success: function (res) {
                        $state.go('main.FLMAsset.Index')
                    }
                });
            }
        })
    }

    //tab 1
    //$scope.masked_RegNo_Options = {
    //    mask: "00A-00009",
    //    clearPromptChar: true,
    //    promptChar: " ",
    //}

    $scope.cboGov_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.GroupOfVehicle,
        data: {},
        success: function (res) {
            $scope.cboGov_options.dataSource.data(res.Data);
            $rootScope.IsLoading = false;
        }
    });
    $scope.numRegWeight_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numRemainValue_options = {
        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,
        change: function (e) {
            if ($scope.TruckItem.RemainValue > 0)
                $scope.TruckItem.CurrentValue = $scope.TruckItem.BaseValue - $scope.TruckItem.RemainValue;
            else
                $scope.TruckItem.CurrentValue = 0;
        }
    }
    $scope.numRegCapacity_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numMinWeight_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numMinCapacity_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numMaxWeight_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numMaxCapacity_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numYearOfProduction_options = { format: '0000', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numEmptyWeight_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numTempMax_options = { format: 'n2', spinners: false, culture: 'en-US', min: -100, step: 1, decimals: 2, }
    $scope.numTempMin_options = { format: 'n2', spinners: false, culture: 'en-US', min: -100, step: 1, decimals: 2, }
    $scope.numBaseValue_options = {
        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,
        change: function (e) {
            if ($scope.TruckItem.RemainValue > 0)
                $scope.TruckItem.CurrentValue = $scope.TruckItem.BaseValue - $scope.TruckItem.RemainValue;
            else
                $scope.TruckItem.CurrentValue = $scope.TruckItem.BaseValue;
        }
    }
    $scope.numCurrentValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numDepreciationPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numWarrantyPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.cboDriver_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'DriverName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                DriverName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    $scope.cboExprDay_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'name', dataValueField: 'value',
        dataSource: Common.DataSource.Local([], {
            id: 'value',
            fields: {
                name: { type: 'string' },
                value: { type: 'string' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    $scope.cboExprInputDay_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'name', dataValueField: 'value',
        dataSource: Common.DataSource.Local([], {
            id: 'value',
            fields: {
                name: { type: 'string' },
                value: { type: 'string' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    $scope.cboAssistant_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'DriverName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                DriverName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_TruckEdit.URL.Get_ALLDriverList,
        data: {},
        success: function (res) {
            $scope.cboDriver_options.dataSource.data(res)
            $scope.cboAssistant_options.dataSource.data(res)
        }
    });
    $scope.dtpDepreciationStart_options = {

    }
    $scope.dtpWarrantyEnd_options = {

    }
    $scope.cboRent_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'VendorName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    VendorName: { type: 'string' }
                }
            }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_TruckEdit.URL.Get_RentList,
        data: {},
        success: function (res) {
            $scope.cboRent_options.dataSource.data(res.Data)
        }
    });

    //#region LocationAddress
    $scope.LocationAddress_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.LocationAddress_List,
            pageSize: 50,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    Location: { type: 'string' },
                    Address: { type: 'string' },
                    Lat: { type: 'number' },
                    Lng: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        selectable: "multiple row",
        change: function (e) {
            var grid = e.sender;
            var selectedData = grid.dataItem(grid.select());
            $scope.LocationHasChoose = true;
            console.log(selectedData.id);
        },
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        //toolbar: kendo.template($('#CATLocation_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '135px',
                template: '<a href="/" ng-click="LocationEdit_Click($event,LocationEdit_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="LocationMap_Click($event,LocationMap_win,dataItem)" class="k-button"><i class="fa fa-map-marker"></i></a>' +
                    '<a href="/" ng-click="LocationDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATLocation.Code}}', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Location', title: '{{RS.CATLocation.Location}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '{{RS.CATLocation.Address}}', width: 250,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EconomicZone', title: '{{RS.CATLocation.EconomicZone}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: '{{RS.CATLocation.Note}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note1', title: '{{RS.CATLocation.Note1}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 150,
                template: "#=Lat==null?\"\":Lat#",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            {
                field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 150,
                template: "#=Lng==null?\"\":Lng#",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            {
                field: 'CreateBy', title: '{{RS.CATLocation.CreateBy}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: '{{RS.CATLocation.CreateDate}}', width: 150,
                template: '#=CreateDate==null?"":Common.Date.FromJsonDMYHM(CreateDate)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Loại địa điểm', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LoadTimeCO', title: 'LoadTimeCO', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'UnLoadTimeCO', title: 'UnLoadTimeCO', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'LoadTimeDI', title: 'LoadTimeDI', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'UnLoadTimeDI', title: 'UnLoadTimeDI', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
        ],
    };

    $scope.LocationEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    }

    $scope.LocationAddress_Win_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Location_Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.LoadRegionData($scope.LocationItem);
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.LocationAddress_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $timeout(function () {
            $scope.LocationAddress_grid.resize();
        }, 1);
    }

    $scope.LocationEdit_win_cboCountryOptions = {
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
            _FLMAsset_TruckEdit.Data.Country = data;
            $scope.LocationEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.LocationEdit_win_cboProvinceOptions = {
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
            _FLMAsset_TruckEdit.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_FLMAsset_TruckEdit.Data.Province[obj.CountryID]))
                    _FLMAsset_TruckEdit.Data.Province[obj.CountryID].push(obj);
                else _FLMAsset_TruckEdit.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.LocationEdit_win_cboDistrictOptions = {
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
            _FLMAsset_TruckEdit.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_FLMAsset_TruckEdit.Data.District[obj.ProvinceID]))
                    _FLMAsset_TruckEdit.Data.District[obj.ProvinceID].push(obj);
                else _FLMAsset_TruckEdit.Data.District[obj.ProvinceID] = [obj];
            })
        }
    });

    $scope.LocationEdit_win_cboWardOptions = {
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

    $scope.LocationEdit_win_cboGOLOptions = {
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
            $scope.LocationEdit_win_cboGOLOptions.dataSource.data(data)
        }
    });
    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _FLMAsset_TruckEdit.Data.Province[countryID];
            $scope.LocationEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _FLMAsset_TruckEdit.Data.District[provinceID];
            $scope.LocationEdit_win_cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)
        }
        catch (e) { }
    }

    $scope.LocationSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.LocationItem.CountryID) && $scope.LocationItem.CountryID > 0) {
                if (Common.HasValue($scope.LocationItem.ProvinceID) && $scope.LocationItem.ProvinceID > 0) {
                    if (Common.HasValue($scope.LocationItem.DistrictID) && $scope.LocationItem.DistrictID > 0) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset_TruckEdit.URL.Location_Save,
                            data: { item: $scope.LocationItem },
                            success: function (res) {
                                win.close();
                                $scope.LocationAddress_grid.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                            }
                        });
                    } else $rootScope.Message({ Msg: 'Quận huyện không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
                } else $rootScope.Message({ Msg: 'Tỉnh thành không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
            }
            else $rootScope.Message({ Msg: 'Quốc gia không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.LocationDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_TruckEdit.URL.Location_Delete,
                    data: { 'item': data },
                    success: function (res) {
                        $scope.LocationAddress_grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                    }
                });
            }
        })
    }
    $scope.LocationAddress_Win_SaveClick = function ($event, win, grid) {
        $event.preventDefault();


        var data = $scope.LocationAddress_grid.dataItem($scope.LocationAddress_grid.select());

        if (Common.HasValue(data)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_TruckEdit.URL.FLM_LocationNotIn_Save,
                data: { locationID: data.ID, assetID: $scope.TruckItem.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.LocationAddress_gridOptions.dataSource.read();
                    $scope.TruckItem.LocationAddress = data.Address;
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }

        else $rootScope.Message({ Msg: 'Chưa chọn điểm', NotifyType: Common.Message.NotifyType.ERROR });


    }
    $scope.LocationEdit_win_numLatOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.LocationEdit_win_numLngOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.LocationEdit_win_numLoadTimeCOOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.LocationEdit_win_numUnLoadTimeCOOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.LocationEdit_win_numLoadTimeDIOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.LocationEdit_win_numUnLoadTimeDIOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    openMap.Create({
        Element: 'map',
        Tooltip_Show: true,
        Tooltip_Element: 'map_tooltip',
        InfoWin_Show: false,
        ClickMarker: null,
        ClickMap: function (e) {

            openMap.SetVisible(null, false);
            openMap.Clear();
            $timeout(function () {
                var img = Common.String.Format(openMap.mapImage.Marker_Yellow);
                var icon = openMap.mapStyle.Icon(img, 1);
                var o = openMap._to4326(e);
                $scope.locationMap.Lat = o[1];
                $scope.locationMap.Lng = o[0];
                $scope.mapEdited = true;
                var marker = openMap.Marker(o[1], o[0], '', icon, '');
                openMap.FitBound([marker]);
            }, 100)
        }
    });

    $scope.LocationMap_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Location_Get,
            data: { 'ID': data.id },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.mapEdited = false;
                $rootScope.IsLoading = false;
                $scope.InitMap(win, $scope.LocationItem);
                win.center();
                win.open();
            }
        });
    }

    $scope.InitMap = function (win, data) {
        if (Common.HasValue(data)) {
            openMap.SetVisible(null, false);
            openMap.Clear();
            $timeout(function () {
                if (data.Lat > 0 && data.Lng > 0) {
                    var img = Common.String.Format(openMap.mapImage.Marker_Red);
                    var icon = openMap.mapStyle.Icon(img, 1);
                    var marker = openMap.Marker(data.Lat, data.Lng, data.Address, icon, data);
                    openMap.FitBound([marker]);
                }
            }, 100)
        }
        win.center();
        win.open();
    }

    $scope.LocationMapSave_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.mapEdited) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn lưu địa điểm đã chọn?',
                Close: function () {
                    if (Common.HasValue($scope.LocationItem)) {
                        openMap.SetVisible(null, false);
                        openMap.Clear();
                        $timeout(function () {
                            if ($scope.LocationItem.Lat > 0 && $scope.LocationItem.Lng > 0) {
                                var img = Common.String.Format(openMap.mapImage.Marker_Red);
                                var icon = openMap.mapStyle.Icon(img, 1);
                                var marker = openMap.Marker($scope.LocationItem.Lat, $scope.LocationItem.Lng, $scope.LocationItem.Address, icon, $scope.LocationItem);
                                openMap.FitBound([marker]);
                            }
                        }, 100)
                    }
                },
                Ok: function () {
                    $rootScope.IsLoading = true;
                    $scope.LocationItem.Lat = $scope.locationMap.Lat;
                    $scope.LocationItem.Lng = $scope.locationMap.Lng;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_TruckEdit.URL.Location_Save,
                        data: { item: $scope.LocationItem },
                        success: function (res) {
                            win.close();
                            $scope.LocationAddress_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    }
    //#endregion

    //#region depreciation
    $scope.numValue_Options = { format: '#,##0.000000000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 9, };

    $scope.depreciation_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Month: { type: 'number', editable: false },
                    Year: { type: 'number', editable: false },
                    Value: { type: 'number' },
                    IsClosed: { type: 'boolean', editable: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell',
        columns: [
            {
                field: 'Month', title: 'Tháng',
                filterable: { cell: { operator: 'equal', showOperators: false }, width: 100, editable: false, }
            },
            {
                field: 'Year', title: 'Năm',
                filterable: { cell: { operator: 'equal', showOperators: false }, width: 100, editable: false, }
            },
            {
                field: 'Value', title: 'Giá trị', width: 200,
                template: '#=Common.Number.ToNumber2(Value)#',
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box style="width:100%" k-options="numValue_Options"/>').appendTo(container)
                }
            },
            {
                field: 'IsClosed', title: 'Đã đóng', width: 100,
                template: '<input type="checkbox" ng-model="dataItem.IsClosed" disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Tất cả', Value: '' }, { Text: 'Đã đóng', Value: true }, { Text: 'Chưa đóng', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            {
                title: '', filterable: false, sortable: false
            },
        ],
    }

    $scope.hasData = true;
    $scope.FLMAsset_Depreciation_GenerateClick = function ($event, win, vform) {
        $event.preventDefault();
        if ($scope.IsEdited) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.WARNING,
                Title: 'Thông báo',
                Msg: 'Bạn có muốn lưu thay đổi',
                Close: function (e) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_TruckEdit.URL.Get_Truck,
                        data: { AssetID: _FLMAsset_TruckEdit.Param.AssetID, RegNo: _FLMAsset_TruckEdit.Param.RegNo },
                        success: function (res) {
                            $scope.IsEdited = false;
                            $scope.TruckItem = res;
                            $scope.LoadingFixedCost(win);
                        }
                    });
                },
                Ok: function (e) {
                    if (vform()) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset_TruckEdit.URL.Save_Truck,
                            data: { item: $scope.TruckItem },
                            success: function (res) {
                                Common.Services.Call($http, {
                                    url: Common.Services.url.FLM,
                                    method: _FLMAsset_TruckEdit.URL.Get_Truck,
                                    data: { AssetID: _FLMAsset_TruckEdit.Param.AssetID, RegNo: _FLMAsset_TruckEdit.Param.RegNo },
                                    success: function (res) {
                                        $scope.IsEdited = false;
                                        $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                                        $scope.TruckItem = res;
                                        $scope.LoadingFixedCost(win);
                                    }
                                });
                            }
                        });
                    }
                },
                pars: null
            })
        } else {
            $scope.LoadingFixedCost(win);
        }
        $scope.HasDelete();
    }

    $scope.LoadingFixedCost = function (win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.FixedCost_ByAssetList,
            data: { assetID: _FLMAsset_TruckEdit.Param.AssetID },
            success: function (res) {
                $scope.depreciation_GridOptions.dataSource.data(res);
                if (res.length == 0) {
                    $scope.hasData = false;
                }
                $rootScope.IsLoading = false;
            }
        });
        win.center();
        win.open();
    }

    $scope.FixedCost_Generate = function ($event, grid) {
        debugger
        $event.preventDefault();
        var error = false;
        if ($scope.TruckItem.CurrentValue < 0 || !Common.HasValue($scope.TruckItem.CurrentValue)) {
            $rootScope.Message({ Msg: 'Chưa có giá trị hiện tại', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.TruckItem.DepreciationPeriod <= 0 || !Common.HasValue($scope.TruckItem.DepreciationPeriod)) {
            $rootScope.Message({ Msg: 'T/g k.hao (tháng) phải lớn hơn 0', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if (!Common.HasValue($scope.TruckItem.DepreciationStart)) {
            $rootScope.Message({ Msg: 'Chưa chọn T/g bắt đầu tính KH', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }

        if (!error) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_TruckEdit.URL.FixedCost_Generate,
                data: { assetID: _FLMAsset_TruckEdit.Param.AssetID },
                success: function (res) {
                    grid.dataSource.data(res);
                    grid.refresh();
                    $scope.hasData = true;
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.FixedCost_Save = function ($event, grid) {
        $event.preventDefault();
        var lst = grid.dataSource.data();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.FixedCost_Save,
            data: { assetID: _FLMAsset_TruckEdit.Param.AssetID, lst: lst },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_TruckEdit.URL.FixedCost_ByAssetList,
                    data: { assetID: _FLMAsset_TruckEdit.Param.AssetID },
                    success: function (res) {
                        $scope.depreciation_GridOptions.dataSource.data(res);
                        $scope.TruckItem.HasDepreciation = true;
                        $rootScope.IsLoading = false;
                    },
                    error: function (e) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    };

    $scope.HasDelete = function () {
        $timeout(function () {
            var data = $scope.depreciation_GridOptions.dataSource.data();
            if (data.length > 0) {
                $scope.hasDelete = true;
            } else $scope.hasDelete = false;
        }, 1);
    }

    $scope.HasDelete();

    $scope.FixedCost_Delete = function ($event, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_TruckEdit.URL.AssetDelete,
                    data: { assetID: _FLMAsset_TruckEdit.Param.AssetID },
                    success: function (res) {
                        Common.Services.Error(res, function () {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            Common.Services.Call($http, {
                                url: Common.Services.url.FLM,
                                method: _FLMAsset_TruckEdit.URL.FixedCost_ByAssetList,
                                data: { assetID: _FLMAsset_TruckEdit.Param.AssetID },
                                success: function (res) {
                                    $scope.depreciation_GridOptions.dataSource.data(res);
                                    if (res.length == 0) {
                                        $scope.hasData = false;
                                    }
                                    $scope.HasDelete();
                                }
                            });

                            Common.Services.Call($http, {
                                url: Common.Services.url.FLM,
                                method: _FLMAsset_TruckEdit.URL.Get_Truck,
                                data: { AssetID: _FLMAsset_TruckEdit.Param.AssetID, RegNo: _FLMAsset_TruckEdit.Param.RegNo },
                                success: function (res) {
                                    $scope.TruckItem = res;
                                }
                            });
                            $rootScope.IsLoading = false;
                            grid.dataSource.read();
                        })
                    }
                })
            }
        })
    };

    $scope.ChooseExprDay_Click = function ($event, win, check) {
        $event.preventDefault();
        $scope.ExprDay.Exp = $scope.TruckItem.ExprInputDay;
        win.center().open();
    };

    $scope.ExprDay_SaveClick = function ($event, win) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: 'Đã cập nhật.',
            NotifyType: Common.Message.NotifyType.SUCCESS
        });
        var flag = false;
        $.each($scope.dataExprInputDay, function (i, v) {
            if (v.value == $scope.ExprDay.Exp) {
                $scope.TruckItem.ExprInputDay = $scope.ExprDay.Exp;
                flag = true;
            }
        });
        if (!flag) {
            $scope.TruckItem.ExprInputDay = $scope.ExprDay.Exp;
            $scope.dataExprInputDay[0].value = $scope.ExprDay.Exp;
        }
        $scope.cboExprInputDay_options.dataSource.data($scope.dataExprInputDay);
        win.close();
    };

    $scope.ChangeValue_Click = function ($event, check) {
        if (check == true) {
            $scope.TruckItem.ExprTOMaster = "";
            $scope.TruckItem.ExprInputTOMaster = "";
        } else {
            $scope.TruckItem.ExprDay = "";
            $scope.TruckItem.ExprInputDay = "";
        }
    };

    $scope.Value_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.TotalDay_options = { format: '#', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.TotalKM_options = { format: '#', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.KM_options = { format: '#', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.ExprDay_CheckClick = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Asset_CheckExpr,
            data: { sExpression: $scope.ExprDay.Exp, assetID: _FLMAsset_TruckEdit.Param.AssetID },
            success: function (res) {
                if (res == true) {

                } else {

                }
            }
        })
    };
    //#endregion
    //#endregion

    //#region ThietBiTheoXe
    $scope.currEQM_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Get_EQMList,
            readparam: function () { return { AssetID: _FLMAsset_TruckEdit.Param.AssetID, request: "" } },
            model: {
                id: 'ID',
                fields: {
                    Code:{type:'string'},
                    WarrantyEnd: { type: 'date' },
                    GroupOfEquipmentID: { type: 'int' },
                    BaseValue: { type: 'number' },
                    CurrentValue: { type: 'number' },
                    DepreciationPeriod: { type: 'number' },
                }   
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'Code', title: 'Số Part.', width: 100, template: '<a href="/" ng-click=FLMAsset_EQM_EditClick($event,dataItem) style=" text-decoration:underline; color:blue">#=Code#</a>' },
            { field: 'GroupOfEquipmentName', title: 'Loại thiết bị', width: 100, },
            { field: 'BaseValue', title: 'Giá trị ban đầu', width: 100, },
            { field: 'CurrentValue', title: 'Giá trị hiện tại', width: 100 },
            { field: 'DepreciationPeriod', title: 'T/g k.hao (tháng)', width: 100 },
            { field: 'WarrantyEnd', title: 'Ngày kết thúc BH', template: '#=WarrantyEnd==null?"":Common.Date.FromJsonDDMMYY(WarrantyEnd)#', width: 100 }
        ],
    }

    $scope.pastEQM_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Get_EQMPastList,
            readparam: function () { return { AssetID: _FLMAsset_TruckEdit.Param.AssetID, request: "" } },
            model: {
                id: 'ID',
                fields: {
                    Code: { type: 'string' },
                    WarrantyEnd: { type: 'date' },
                    GroupOfEquipmentName: { type: 'string' },
                    BaseValue: { type: 'number' },
                    GroupOfEquipmentID: { type: 'int' },
                    DepreciationPeriod: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'Code', title: 'Số Part.', width: 100, template: '<a href="/" ng-click=FLMAsset_EQM_EditClick($event,dataItem) style=" text-decoration:underline; color:blue">#=Code#</a>' },
            { field: 'GroupOfEquipmentName', title: 'Loại thiết bị', width: 100, },
            { field: 'BaseValue', title: 'Giá trị ban đầu', width: 100 },
            { field: 'CurrentValue', title: 'Giá trị hiện tại', width: 100 },
            { field: 'DepreciationPeriod', title: 'T/g k.hao (tháng)', width: 100 },
            { field: 'WarrantyEnd', title: 'Ngày kết thúc BH', template: '#=WarrantyEnd==null?"":Common.Date.FromJsonDDMMYY(WarrantyEnd)#', width: 100 }
        ],
    }

    $scope.SplitterEQM_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "50%" },
            { collapsible: false, resizable: false, size: "50%" },
        ],
    }

    $scope.FLMAsset_EQM_EditClick = function ($event, data) {
        $event.preventDefault();
        $state.go("main.FLMAsset.EditEQM", { AssetID: data.ID })
    }
    //#endregion

    //#region DinhMuc
    $scope.ConsumptionGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Read_Consumption,
            readparam: function () { return { AssetID: _FLMAsset_TruckEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    MaterialName: { type: 'string', editable: false },
                    QuantityPerKM: { type: 'number', editable: true },
                    KMStart: { type: 'number', editable: true },
                    KMCurrent: { type: 'number', editable: true },
                    KMWarning: { type: 'number', editable: true },
                    IsFuel: { type: 'boolean', editable: false },
                    GroupOfMaterialName: { type: 'string', editable: false }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: { mode: 'inline' },
        dataBound: function () {
            $scope.IsConsumptionEdit = false;
            $rootScope.Loading.Change("Định mức ...", $rootScope.Loading.Progress + LoadingStep);
        },
        columns: [
             {
                 title: ' ', width: '85px',
                 template: '<a ng-show="!IsConsumptionEdit" href="/" ng-click="FLMAsset_ConsumptionGrid_Edit($event,ConsumptionGrid)" class="k-button" ><i class="fa fa-pencil"></i></a>' +
                     '<a ng-show="!IsConsumptionEdit" href="/" ng-click="FLMAsset_ConsumptionGrid_Delete($event,ConsumptionGrid)" class="k-button" ><i class="fa fa-trash"></i></a>' +
                     '<a ng-show="IsConsumptionEdit && ConsumptionEdit.ID==#=ID#?true:false" href="/" ng-click="FLMAsset_ConsumptionGrid_Save($event,ConsumptionGrid)" class="k-button" ><i class="fa fa-check "></i></a>' +
                     '<a ng-show="IsConsumptionEdit && ConsumptionEdit.ID==#=ID#?true:false" href="/" ng-click="FLMAsset_ConsumptionGrid_Cancel($event,ConsumptionGrid)" class="k-button"><i class="fa fa-ban"></i></a>',
                 filterable: false, sortable: false
             },
            { field: 'MaterialName', title: 'Vật tư', width: '200px' },
            {
                field: 'QuantityPerKM', title: 'Định mức/KM', template: '#=Common.Number.ToNumber3(QuantityPerKM)#', width: '200px',
                //editor: function (container, options) {
                //    $('<input data-bind="value:' + options.field + '" />').appendTo(container).kendoNumericTextBox({
                //        format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3,
                //    })
                //}
                editor: '<input class="cus-combobox" kendo-numeric-text-box k-options="numQuantityPerKMOptions" data-bind="value:QuantityPerKM" ng-model="ConsumptionEdit.QuantityPerKM"/>'
            },
            {
                field: 'KMStart', title: 'KMStart', template: '#=Common.Number.ToNumber3(KMStart)#', width: '200px',
                editor: '<input class="cus-combobox" kendo-numeric-text-box k-options="numKMStartOptions" data-bind="value:KMStart" ng-model="ConsumptionEdit.KMStart"/>'
            },
            {
                field: 'KMCurrent', title: 'KMCurrent', template: '#=Common.Number.ToNumber3(KMCurrent)#', width: '200px',
                editor: '<input class="cus-combobox" kendo-numeric-text-box k-options="numKMCurrentOptions" data-bind="value:KMCurrent" ng-model="ConsumptionEdit.KMCurrent"/>'
            },
            {
                field: 'KMWarning', title: 'KMWarning', template: '#=Common.Number.ToNumber3(KMWarning)#', width: '200px',
                editor: '<input class="cus-combobox" kendo-numeric-text-box k-options="numKMWarningOptions" data-bind="value:KMWarning" ng-model="ConsumptionEdit.KMWarning"/>'
            },
            {
                field: 'IsFuel', title: 'Nhiên liệu?', width: '100px', hidden: true,
                template: '<input type="checkbox" #= IsFuel ? "checked=checked" : "" # disabled="disabled" />',
                groupHeaderTemplate: "#=(value == true)?'Là nhiên liệu':'Không phải nhiên liệu'#",
            },
            {
                field: 'GroupOfMaterialName', title: 'Nhóm vật tư', width: '100px', hidden: true,
                template: '#=GroupOfMaterialName==null?"":GroupOfMaterialName#',
            },
        ]
    }


    $scope.FLMAsset_Consumption_AddClick = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.ConsumptionNotChoose_GridOptions.dataSource.read();
    }

    $scope.numQuantityPerKMOptions = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numKMStartOptions = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numKMCurrentOptions = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numKMWarningOptions = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }

    $scope.ConsumptionNotChoose_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Read_ConsumptionNotIn,
            readparam: function () { return { AssetID: _FLMAsset_TruckEdit.Param.AssetID } },
            group: [{ field: "IsFuel" }, { field: "GroupOfMaterialName" }],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', defaultValue: false },
                    QuantityPerKM: { type: 'number' },
                    KMStart: { type: 'number' },
                    KMCurrent: { type: 'number' },
                    KMWarning: { type: 'number' },
                    MaterialID: { type: 'number', editable: false },
                    PackingID: { type: 'number' },
                    MaterialName: { type: 'string', editable: false },
                    PackingName: { type: 'string' },
                    GroupOfMaterialName: { type: 'string', editable: false },
                    IsFuel: { type: 'boolean', editable: false }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: 'incell', groupable: false,
        columns: [
            {
                title: ' ', width: '100px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ConsumptionNotChoose_Grid,ConsumptionNotChoose_ChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ConsumptionNotChoose_Grid,ConsumptionNotChoose_ChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'MaterialName', title: 'Vật tư', template: '#:MaterialName#', width: '200px' },
            {
                field: 'QuantityPerKM', title: 'Định mức/KM', template: '#=Common.Number.ToNumber3(QuantityPerKM)#', width: '200px',
                editor: function (container, options) {
                    $('<input data-bind="value:' + options.field + '" />').appendTo(container).kendoNumericTextBox({
                        format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3,
                    })
                }
            },
            {
                field: 'KMStart', title: 'KMStart', template: '#=Common.Number.ToNumber3(KMStart)#', width: '200px',
                editor: function (container, options) {
                    $('<input data-bind="value:' + options.field + '" />').appendTo(container).kendoNumericTextBox({
                        format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3,
                    })
                }
            },
            {
                field: 'KMCurrent', title: 'KMCurrent', template: '#=Common.Number.ToNumber3(KMCurrent)#', width: '200px',
                editor: function (container, options) {
                    $('<input data-bind="value:' + options.field + '" />').appendTo(container).kendoNumericTextBox({
                        format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3,
                    })
                }
            },
            {
                field: 'KMWarning', title: 'KMWarning', template: '#=Common.Number.ToNumber3(KMWarning)#', width: '200px',
                editor: function (container, options) {
                    $('<input data-bind="value:' + options.field + '" />').appendTo(container).kendoNumericTextBox({
                        format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3,
                    })
                }
            },
            {
                field: 'IsFuel', title: 'Nhiên liệu?', width: '100px', hidden: true,
                template: '<input type="checkbox" #= IsFuel ? "checked=checked" : "" # disabled="disabled" />',
                groupHeaderTemplate: "#=(value == true)?'Là nhiên liệu':'Không phải nhiên liệu'#",
            },
            {
                field: 'GroupOfMaterialName', title: 'Nhóm vật tư', width: '100px', hidden: true,
                template: '#=GroupOfMaterialName==null?"":GroupOfMaterialName#',
            },
        ]
    }
    $scope.ConsumptionNotChoose_ChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.FLMAsset_ConsumptionGrid_Edit = function ($event, grid) {
        $scope.IsConsumptionEdit = true;
        var tr = $event.target.closest('tr');
        $scope.ConsumptionEdit = grid.dataItem(tr);
        _FLMAsset_TruckEdit.Data.ItemBackup = $.extend(true, {}, $scope.ConsumptionEdit);
        grid.editRow(tr);
        var td = $(tr).find('input');
        td[0].focus();
    }

    $scope.FLMAsset_ConsumptionGrid_Save = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsConsumptionEdit = false;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Save_Consumption,
            data: { item: $scope.ConsumptionEdit, AssetID: _FLMAsset_TruckEdit.Param.AssetID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ConsumptionGrid_Options.dataSource.read()
            }
        });

    }

    $scope.FLMAsset_ConsumptionGrid_Delete = function ($event, grid) {
        $event.preventDefault();

        var obj = grid.dataItem($event.target.closest('tr'));
        if (Common.HasValue(obj)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có muốn xóa',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_TruckEdit.URL.Delete_Consumption,
                        data: { 'item': obj },
                        success: function (res) {
                            $scope.ConsumptionGrid_Options.dataSource.read()
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.FLMAsset_ConsumptionGrid_Cancel = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.IsConsumptionEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == _FLMAsset_TruckEdit.Data.ItemBackup.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _FLMAsset_TruckEdit.Data.ItemBackup))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    $scope.ConsumptionNotChoose_Win_SaveChooseClick = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var dataChoose = $.grep(data, function (o) { return o.IsChoose == true });
        var checkErrorQuantityPerKM = $.grep(dataChoose, function (o) { return o.QuantityPerKM == 0 });
        var checkErrorKMStart = $.grep(dataChoose, function (o) { return o.KMStart < 0 || !Common.HasValue(o.KMStart) });
        var checkErrorKMCurrent = $.grep(dataChoose, function (o) { return o.KMCurrent < 0 || !Common.HasValue(o.KMCurrent) });
        var checkErrorKMWarning = $.grep(dataChoose, function (o) { return o.KMWarning < 0 || !Common.HasValue(o.KMWarning) });
        debugger
        var error = false;
        if (checkErrorQuantityPerKM.length != 0) {
            error = true;
            $rootScope.Message({ Msg: 'Điền định mức/km trước khi lưu', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (checkErrorKMStart.length != 0) {
            error = true;
            $rootScope.Message({ Msg: 'Điền KMStart trước khi lưu', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (checkErrorKMCurrent.length != 0) {
            error = true;
            $rootScope.Message({ Msg: 'Điền KMCurrent trước khi lưu', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (checkErrorKMWarning.length != 0) {
            error = true;
            $rootScope.Message({ Msg: 'Điền KMWarning trước khi lưu', NotifyType: Common.Message.NotifyType.ERROR });
        }
        //debugger
        if (dataChoose.length > 0) {
            if (!error) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    NotifyType: Common.Message.NotifyType.SUCCESS,
                    Title: 'Thông báo',
                    Msg: 'Đồng ý thêm các định mức đã chọn',
                    Action: true,
                    Close: null,
                    Ok: function (e) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset_TruckEdit.URL.Save_ConsumptionNotIn,
                            data: { lst: dataChoose, AssetID: _FLMAsset_TruckEdit.Param.AssetID },
                            success: function (res) {
                                win.close();
                                $scope.ConsumptionGrid_Options.dataSource.read();
                            }
                        });
                    },
                    pars: null
                })
            }
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Chưa chọn vật tư',
                Action: true,
                Close: null,
                Ok: null,
                pars: null
            })
        }
    }

    $scope.ConsumptionNotChoose_Win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion
   
    //#region LichSuKhauHao
    $scope.DepreciationGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.History_Depreciation,
            readparam: function () { return { AssetID: _FLMAsset_TruckEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Value: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'Month', title: 'Tháng', width: 100, },
            { field: 'Year', title: 'Năm', width: 100 },
            { field: 'Value', title: 'Giá trị', template: '#=Common.Number.ToMoney(Value)#', width: 100 }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Lịch sử khấu hao ...", $rootScope.Loading.Progress + LoadingStep);
        }
    }
    //#endregion

    //#region LichSu chuyen
    $scope.OPSHistory_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.History_OPS,
            readparam: function () {
                return {
                    AssetID: _FLMAsset_TruckEdit.Param.AssetID,
                    dtFrom: $scope.OPSSearchItem.DateFrom,
                    dtTo: $scope.OPSSearchItem.DateTo,
                    isDI:true
                }
            },
            model: {
                id: 'MasterID',
                fields: {
                    MasterID: { type: 'number' },
                    TotalCostStation: { type: 'number' },
                    TotalCostTrouble: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: {mode:"row"}, resizable: true,
        columns: [
            {field: "MasterCode", title: 'Mã chuyến', width: 120, filterable: { cell: { showOperators: false, operator: "contains" } }},
            {
                field: 'ETD', title: 'ETD', width: 140,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(ETD)#',
            },
            {
                field: 'ETA', title: 'ETA', width: 140,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(ETA)#',
            },
            { field: 'TotalCostStation', title: 'Tổng chi phí trạm', template: '#=Common.Number.ToMoney(TotalCostStation)#', width: 150 },
            { field: 'TotalCostTrouble', title: 'Tổng vấn đề phát sinh', template: '#=Common.Number.ToMoney(TotalCostTrouble)#', width: 150 }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Lịch sử chuyến ...", $rootScope.Loading.Progress + LoadingStep);
        }
    }

    $scope.OPSSearch_Click = function ($event,grid) {
        $event.preventDefault();
        if ($scope.OPSSearchItem.DateTo >= $scope.OPSSearchItem.DateFrom) {
            grid.dataSource.read();
        }
        else $rootScope.Message({ Msg: 'Khoảng thời gian tìm kiếm không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
    }
    //#endregion

    //#region LichSu sua chua
    $scope.RepairHistory_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.History_Repair,
            readparam: function () {
                return {
                    AssetID: _FLMAsset_TruckEdit.Param.AssetID,
                    dtFrom: $scope.RepairSearchItem.DateFrom,
                    dtTo: $scope.RepairSearchItem.DateTo,
                }
            },
            model: {
                id: 'ReceiptID',
                fields: {
                    ReceiptID: { type: 'number' },
                    Amount: { type: 'number' },
                    DateReceipt: { type: 'date' },
                    InvoiceDate: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: "row" }, resizable: true,
        columns: [
            { field: "ReceiptNo", title: 'Mã phiếu', width: 120, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'DateReceipt', title: 'Ngày cấp phiếu', width: 140,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(DateReceipt)#',
            },
            { field: 'Amount', title: 'Tổng chi phí', template: '#=Common.Number.ToMoney(Amount)#', width: 150 },
            { field: "InvoiceNo", title: 'Số hóa đơn', width: 120, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'InvoiceDate', title: 'Ngày hóa đơn', width: 140,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(InvoiceDate)#',
            },
            { field: "Note", title: 'Ghi chú', width: 120, filterable: { cell: { showOperators: false, operator: "contains" } } },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Lịch sử sữa chữa ...", $rootScope.Loading.Progress + LoadingStep);
        }
    }

    $scope.RepairSearch_Click = function ($event, grid) {
        $event.preventDefault();
        if ($scope.RepairSearchItem.DateTo >= $scope.RepairSearchItem.DateFrom) {
            grid.dataSource.read();
        }
        else $rootScope.Message({ Msg: 'Khoảng thời gian tìm kiếm không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
    }
    //#endregion

    //#region LichSuCapPhatVatTu
    $scope.FuelHistoryGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.Get_FuelHistory,
            readparam: function () { return { AssetID: _FLMAsset_TruckEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    DateReceipt: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: "DateReceipt", title: 'Ngày phát sinh', width: '15%', template: "#=DateReceipt==null?\"\":Common.Date.FromJsonDDMMYY(DateReceipt)#" },
            { field: "MaterialName", title: 'Tên vật tư', width: '15%', },
            { field: "GroupOfMaterialName", title: 'Loại vật tư', width: '15%', },
            { field: "Quantity", title: 'Số lượng', width: '10%', attributes: { style: "text-align: left; " }, },
            { field: "Price", title: 'Đơn giá', width: '15%', attributes: { style: "text-align: left; " }, },
            { field: "Amount", title: 'Thành tiền', width: '15%', attributes: { style: "text-align: left; " }, },
            { field: "Code", title: 'Mã phiếu', width: '15%', attributes: { style: "text-align: left; " }, }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Lịch sử cấp phát vật tư ...", $rootScope.Loading.Progress + LoadingStep);
        }
    }
    //#endregion

    //#region PhuPhiThang
    $scope.FeeDefault_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.FeeDefault_List,
            readparam: function () { return { AssetID: _FLMAsset_TruckEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    WarrantyEnd: { type: 'date' },
                    ExprPrice:{type:'number'},
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="FeeDefault_Edit_Click($event,dataItem,assetFee_win)" class="k-button" ><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="FeeDefault_Delete_Click($event,dataItem)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'TypeOfScheduleFeeCode', title: 'Mã chi phí', width: 150, template: '' },
            { field: 'TypeOfScheduleFeeName', title: 'Tên chi phí', width: 150, },
            { field: 'ExprPrice', title: 'Giá', width: 150, },
            { title: '', filterable: false, sortable: false },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Phụ phí tháng ...", $rootScope.Loading.Progress + LoadingStep);
        }
    }

    $scope.FeeDefault_Add_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadData_FeeDefault(0, win);
    };

    $scope.FeeDefault_Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadData_FeeDefault(data.ID, win);
    };

    $scope.LoadData_FeeDefault = function (ID, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_TruckEdit.URL.FeeDefault_Get,
            data: { ID: ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.ItemAssetFee = res;
                    win.center().open();
                    $rootScope.IsLoading = false;

                }
            }
        });
    }

    $scope.FeeDefault_Delete_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa phụ phí?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_TruckEdit.URL.FeeDefault_Delete,
                    data: { ID: data.ID },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $scope.FeeDefault_Grid.dataSource.read();
                            $rootScope.IsLoading = false;
                        }
                    }
                });
            }
        })
    };

    $scope.assetFee_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_TruckEdit.URL.FeeDefault_Save,
                data: { AssetID: _FLMAsset_TruckEdit.Param.AssetID, item: $scope.ItemAssetFee },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $scope.FeeDefault_Grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                    }
                }
            });
        }
    }

    $scope.cboTypeOfScheduleFee_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_FLMTypeOfScheduleFee,
        success: function (data) {
            $scope.cboTypeOfScheduleFee_Options.dataSource.data(data);
        }
    })
    //#endregion
  
    $scope.FLMAsset_Truck_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMAsset.Index")
    }
    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}])