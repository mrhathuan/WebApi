/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _VENDetail = {
    URL: {
        VenGet: "Vendor_Get",
        VenUpdate: "Vendor_Update",
        VenDestroy: "Vendor_Destroy",
        TractorListUrlGet: 'CATTractor_Read',
        RomoocListUrlGet: 'CATRomooc_Read',
        TruckListUrlGet: 'CATTruck_Read',
        GroupOfVehicleList: 'VENGroupOfVehicleList',
        TruckUrlRead: 'Truck_Read',
        TruckUrlUpdate: 'Truck_Update',
        TruckUrlDestroy: 'Truck_Destroy',
        TruckNotInList: 'Truck_NotInList',
        TruckNotInSave: 'Truck_NotInSave',
        TractorNotInList: 'Tractor_NotInList',
        TractorNotInSave: 'Tractor_NotInSave',
        TractorUrlRead: 'Tractor_Read',
        TractorUrlUpdate: 'Tractor_Update',
        TractorUrlDestroy: 'Tractor_Destroy',
        RomoocUrlRead: 'Romooc_Read',
        RomoocUrlUpdate: 'Romooc_Update',
        RomoocUrlDestroy: 'Romooc_Destroy',
        RomoocNotInList: 'Romooc_NotInList',
        RomoocNotInSave: 'Romooc_NotInSave',
        RomoocGet: 'Romooc_Get',
        TractorGet: 'Tractor_Get',
        TruckGet: 'Truck_Get',
        CompanyUrlRead: 'Company_Read',
        CompanyUrlDelete: 'Company_Delete',
        CompanyNotInUrlRead: 'CompanyNotInList_Read',
        CompanyNotInUrlSave: 'CompanyNotInList_Save',
        RoutingRead: 'Routing_List',
        RoutingDelete: 'Routing_Delete',
        RoutingNotinRead: 'RoutingNotIn_List',
        RoutingCusNotinRead: 'RoutingCusNotIn_List',
        RoutingNotinSave: 'RoutingNotIn_SaveList',
        Routing_Reset: 'Routing_Update',
        GOPRead: 'GroupOfProduct_Read',
        GOPUpdate: 'GroupOfProduct_Update',
        GOPDestroy: 'GroupOfProduct_Destroy',
        GOPAllRead: 'GroupOfProductAll_Read',
        GOPReset: 'GroupOfProduct_ResetPrice',
        GOPMappingList: 'GroupOfProductMapping_List',
        GOPMappingNotinList: 'GroupOfProductMappingNotIn_List',
        GOPMappingSave: 'GroupOfProductMapping_SaveList',
        GOPMappingDelete: 'GroupOfProductMapping_Delete',
        GOPGetByCode: 'GroupOfProduct_GetByCode',
        TruckExport: 'VEN_Truck_Export',
        TruckCheck: 'VEN_Truck_Check',
        TruckImport: 'VEN_Truck_Import',
        Driver_List: 'VENDriver_List',
        Driver_Get: 'VENDriver_Get',
        Driver_Save: 'VENDriver_Save',
        Driver_Delete: 'VENDriver_Delete',
        CATDrivingLicence_List: 'ALL_CATDrivingLicence',
        DriverNotIn_List: 'VENDriver_NotInList',
        DriverNotIn_SaveList: 'VENDriver_NotInSave',
        DriverExcel_Export: 'VENDriver_Excel_Export',
        DriverExcel_Save: 'VENDriver_Excel_Save',
        DriverExcel_Check: 'VENDriver_Excel_Check',
        DrivingLicence_List: 'VENDriver_DrivingLicence_List',
        DrivingLicence_Get: 'VENDriver_DrivingLicence_Get',
        DrivingLicence_Save: 'VENDriver_DrivingLicence_Save',
        DrivingLicence_Delete: 'VENDriver_DrivingLicence_Delete',

        CustomerList: 'VENContract_ByCustomerList',

        VenLocation_List: 'VENLocation_List',
        VenLocation_SaveList: 'VENLocation_SaveList',
        VenLocation_Delete: 'VENLocation_Delete',
        VenLocation_NotInList: 'VENLocation_NotInList',
        VenLocation_HasRun: 'VENLocation_HasRun',

        VenLocation_ExcelInit: 'VENLocation_ExcelInit',
        VenLocation_ExcelChange: 'VENLocation_ExcelChange',
        VenLocation_ExcelImport: 'VENLocation_ExcelImport',
        VenLocation_ExcelApprove: 'VENLocation_ExcelApprove',

        Routing_Contract_List: 'VENLocation_RoutingContract_List',
        Routing_Contract_SaveList: 'VENLocation_RoutingContract_SaveList',
        Routing_Contract_NewRoutingSave: 'VENLocation_RoutingContract_NewRoutingSave',
        Routing_Contract_ContractData: 'VENLocation_RoutingContract_ContractData',
        Routing_Contract_NewRoutingGet: 'VENLocation_RoutingContract_NewRoutingGet',
        Routing_Contract_NewAreaSave: 'VENLocation_RoutingContract_NewAreaSave',
        Routing_Contract_AreaList: 'VENLocation_RoutingContract_AreaList',


        Truck_ExcelInit: 'VEN_Truck_ExcelInit',
        Truck_ExcelChange: 'VEN_Truck_ExcelChange',
        Truck_ExcelImport: 'VEN_Truck_ExcelImport',
        Truck_ExcelApprove: 'VEN_Truck_ExcelApprove',

        Tractor_ExcelInit: 'VEN_Tractor_ExcelInit',
        Tractor_ExcelChange: 'VEN_Tractor_ExcelChange',
        Tractor_ExcelImport: 'VEN_Tractor_ExcelImport',
        Tractor_ExcelApprove: 'VEN_Tractor_ExcelApprove',

        Romooc_ExcelInit: 'Romooc_ExcelInit',
        Romooc_ExcelChange: 'Romooc_ExcelChange',
        Romooc_ExcelImport: 'Romooc_ExcelImport',
        Romooc_ExcelApprove: 'Romooc_ExcelApprove',

        Driver_ExcelInit: 'VEN_Driver_ExcelInit',
        Driver_ExcelChange: 'VEN_Driver_ExcelChange',
        Driver_ExcelImport: 'VEN_Driver_ExcelImport',
        Driver_ExcelApprove: 'VEN_Driver_ExcelApprove',

        VENLocationSaveLoad_List: 'VENLocationSaveLoad_List',
    },
    Obj: {
        Country: [],
        Province: [],
        District: []
    },
    Param: {
        ID: -1
    },
    Data: {
        Country: {},
        Province: {},
        District: {},
    },
    ExcelKey: {
        Location: "VENDetail_Location",
        ResourceLocation: "VENDetail_Location",

        ResourceTruck: "VENDetail_Truck",
        ResourceTractor: "VENDetail_Tractor",
        ResourceRomooc: "VENDetail_Romooc",

        Truck: "VENDetail_Truck",
        Tractor: "VENDetail_Tractor",
        Romooc: "VENDetail_Romooc",
    }
}

angular.module('myapp').controller('VENVendor_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('VENVendor_DetailCtrl');
    $rootScope.IsLoading = false;
    $rootScope.Loading.Show('Thông tin khách hàng');
    var LoadingStep = 20;

    $scope.VenData = null;
    _VENDetail.Param.ID = $stateParams.id;
    $scope.DetailItem = {};
    $scope.gopID = 0;
    $scope.ItemDriver = { DriverID: 0 };
    $scope.ItemDrivingLicence = null;
    $scope.PartnerRouting_LocationID = -1;

    $scope.TruckNotInChoose = false;
    $scope.TractorNotInChoose = false;
    $scope.LocationNotInChoose = false;
    $scope.TabIndex = 0;

    //#region Auth
    if (_VENDetail.Param.ID <= 0) {
        if (!$rootScope.CheckView("ActAdd", "main.VENVendor.Index")) return;
    }
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

    $scope.VenCountry_CbbOptions = {
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
                $scope.VenData.ProvinceID = -1;
                $scope.VenData.DistrictID = -1;
                $scope.VenData.WardID = -1;
                $scope.LoadRegionData($scope.VenData);
            }
            else {
                $scope.VenData.CountryID = "";
                $scope.VenData.ProvinceID = "";
                $scope.VenData.DistrictID = "";
                $scope.VenData.WardID = "";
                $scope.LoadRegionData($scope.VenData);
            }
        }
    }
    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _VENDetail.Data.Country = data;
            $scope.VenCountry_CbbOptions.dataSource.data(data);
        }
    })

    $scope.VenProvince_CbbOptions = {
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
                $scope.VenData.DistrictID = -1;
                $scope.VenData.WardID = "";
                $scope.LoadRegionData($scope.VenData);
            }
            else {
                $scope.VenData.ProvinceID = "";
                $scope.VenData.DistrictID = "";
                $scope.VenData.WardID = "";
                $scope.LoadRegionData($scope.VenData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _VENDetail.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_VENDetail.Data.Province[obj.CountryID]))
                    _VENDetail.Data.Province[obj.CountryID].push(obj);
                else _VENDetail.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.VenDistrict_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DistrictName', dataValueField: 'ID',
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
                $scope.VenData.WardID = "";
                // $scope.LoadRegionData($scope.VenData);
            }
            else {
                $scope.VenData.DistrictID = "";
                $scope.VenData.WardID = "";
                $scope.LoadRegionData($scope.VenData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _VENDetail.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_VENDetail.Data.District[obj.ProvinceID]))
                    _VENDetail.Data.District[obj.ProvinceID].push(obj);
                else _VENDetail.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })

    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENDetail.URL.VenGet,
        data: { id: _VENDetail.Param.ID },
        success: function (res) {
            $scope.VenData = res;
            $rootScope.Loading.Change("Thông tin chung ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Truck_GridOptions.dataSource.read();
            $scope.LoadRegionData($scope.VenData);
        }
    })

    //#region Options
    $scope.Main_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        navigatable : false,
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Main_Tab select:" + $scope.TabIndex);
            }, 1)
        }
    }

    $scope.Truck_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TruckUrlRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    RegWeight:{type: 'number'},
                    MaxWeight: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    RegCapacity: { type: 'number' },
                }
            }
        }),
        toolbar: kendo.template($('#Truck-grid-toolbar').html()),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Truck_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                        ' <a href="/" ng-click="Truck_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false, locked: true
            },
            { field: 'RegNo', title: 'Số xe', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfVehicleName', title: 'Loại xe', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                title: 'Trọng tải', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegWeight', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxWeight', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            },
            {
                title: 'Số khối', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegCapacity', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxCapacity', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            }

        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin xe tải ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Tractor_GridOptions.dataSource.read();
        }
    }
    $scope.Truck_NotIn_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TruckNotInList,
            readparam: function () { return { vendorID: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    IsChoose: { type: 'boolean' },
                    RegWeight: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    RegCapacity: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Truck_NotIn_win_Grid,Truck_NotIn_win_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Truck_NotIn_win_Grid,Truck_NotIn_win_GridChoose_Change)" />',
                filterable: false, sortable: false, locked: false,
            },
            { field: 'RegNo', title: 'Số xe', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                title: 'Trọng tải', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegWeight', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxWeight', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            },
            {
                title: 'Số khối', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegCapacity', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxCapacity', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            }

        ]
    }

    $scope.Truck_NotIn_win_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TruckNotInChoose = hasChoose;
    }

    $scope.Tractor_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TractorUrlRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    RegWeight: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    MinWeight: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    MinCapacity: { type: 'number' },
                     RegCapacity: { type: 'number' },
                }
            }
        }),
        toolbar: kendo.template($('#Tractor-grid-toolbar').html()),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Tractor_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a> <a href="/" ng-click="Tractor_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'RegNo', title: 'Số xe', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfVehicleName', title: 'Loại xe', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                title: 'Trọng tải', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegWeight', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MinWeight', title: 'Tối thiểu', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxWeight', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            },
            {
                title: 'Số khối', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegCapacity', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MinCapacity', title: 'Tối thiểu', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxCapacity', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            }

        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin đầu kéo ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Romooc_GridOptions.dataSource.read();
        }
    }
    $scope.Tractor_NotIn_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TractorNotInList,
            readparam: function () { return { vendorID: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    IsChoose: { type: 'boolean' },
                    RegWeight: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    MinWeight: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    MinCapacity: { type: 'number' },
                    RegCapacity: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Tractor_NotIn_win_Grid,Tractor_NotIn_win_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Tractor_NotIn_win_Grid,Tractor_NotIn_win_GridChoose_Change)" />',
                filterable: false, sortable: false, locked: false,
            },
            { field: 'RegNo', title: 'Số xe', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                title: 'Trọng tải', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegWeight', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MinWeight', title: 'Tối thiểu', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxWeight', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            },
            {
                title: 'Số khối', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                columns: [
                    {
                        field: 'RegCapacity', title: 'Đăng ký', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MinCapacity', title: 'Tối thiểu', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                    {
                        field: 'MaxCapacity', title: 'Tối đa', width: '135px',
                        filterable: { cell: { operator: 'eq', showOperators: false } }
                    },
                ]
            }

        ]
    }
    $scope.Tractor_NotIn_win_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TractorNotInChoose = hasChoose;
    }

    $scope.masked_RegNo_Options = {
        mask: "00A-00009",
        clearPromptChar: true,
        promptChar: " ",
    }

    $scope.Romooc_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.RomoocUrlRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true }
                }
            }
        }),
        toolbar: kendo.template($('#Romooc-grid-toolbar').html()),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Romooc_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a>'+ 
                    '<a href="/" ng-click="Romooc_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'RegNo', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin romooc ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Company_GridOptions.dataSource.read();
        }
    }
    $scope.Romooc_NotIn_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.RomoocNotInList,
            readparam: function () { return { vendorID: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    IsChoose: { type: 'boolean' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Romooc_NotIn_win_Grid,Romooc_NotIn_win_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Romooc_NotIn_win_Grid,Romooc_NotIn_win_GridChoose_Change)" />',
                filterable: false, sortable: false, locked: false,
            },
            { field: 'RegNo', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.RomoocNotInChoose = false;

    $scope.Romooc_NotIn_win_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RomoocNotInChoose = hasChoose;
    }

    $scope.Company_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.CompanyUrlRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true }
                }
            }
        }),
        toolbar: kendo.template($('#Company-grid-toolbar').html()),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Company_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'CustomerRelateCode', title: 'Mã đối tác',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '300px' }
            },
            {
                field: 'CustomerRelateName', title: 'Tên đối tác', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Công ty ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Routing_GridOptions.dataSource.read();
        }
    }

    $scope.CompanyNotin_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.CompanyNotInUrlRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '99%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CompanyNotin_Grid,CompanyNotin_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CompanyNotin_Grid,CompanyNotin_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'CustomerRelateCode', title: 'Mã đối tác', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerRelateName', title: 'Tên đối tác', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.Routing_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.RoutingRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: "RoutingID",
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    RoutingID: { type: 'number', editable: false, nullable: true },
                    RoutingParentID: { type: 'number', nullable: true },
                    LeadTime: { type: 'number' }
                },
            }
        }),
        height: '99%', pageable: true, sortable: false, filterable: { mode: 'row' }, columnMenu: false, resizable: true,
        selectable: true,
        toolbar: kendo.template($('#routing-toolbar').html()),
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Routing_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Cung đường ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.GOP_TreeOptions.dataSource.read();
        }
    }

    $scope.Routing_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.RoutingNotinRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    IsChoose: { type: 'boolean' },
                    ID: { type: 'number', editable: false, nullable: true },
                    EDistance: { type: 'number', editable: false, nullable: true },
                    EHours: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', filterable: { mode: 'row' }, sortable: false, resizable: true, pageable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Routing_NotinGrid)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Routing_NotinGrid)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EDistance', title: 'Khoảng cách', width: 120,
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'EHours', title: 'Thời gian', width: 120,
                filterable: { cell: { operator: 'gte', showOperators: false } }
            }
        ]
    };

    $scope.RoutingCus_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.RoutingCusNotinRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    IsChoose: { type: 'boolean' },
                    ID: { type: 'number', editable: false, nullable: true },
                    EDistance: { type: 'number', editable: false, nullable: true },
                    EHours: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', filterable: { mode: 'row' }, sortable: false, resizable: true, pageable: true,

        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,RoutingCus_NotinGrid)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,RoutingCus_NotinGrid)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EDistance', title: 'Khoảng cách', width: 120,
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'EHours', title: 'Thời gian', width: 120,
                filterable: { cell: { operator: 'gte', showOperators: false } }
            }
        ]
    };

    $scope.Product_SplitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: true, resizable: true, size: '50%' },
            { collapsible: true, resizable: true, size: '50%' }
        ]
    }

    $scope.GOP_TreeOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.GOPRead,
            readparam: function () { return { id: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number', nullable: true },
                    PriceOfGOPName: { type: 'string' },
                    ParentName: { type: 'string' },
                    ParentID: { type: 'number' }
                },
                expanded: false
            }
        }),
        toolbar: kendo.template($('#goproduct-treelist-toolbar').html()),
        height: '99%', filterable: false, columnMenu: false, sortable: false, selectable: true, editable: false,
        change: function (e) {
            var tree = this;
            var dataItem = tree.dataItem(this.select());
            if (dataItem != null) {
                $scope.gopID = dataItem.ID;
            }
            $scope.Product_Grid.dataSource.read();
        },
        columns: [
            { title: '', width: 20, filterable: false },
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="GOP_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    ' <a href="/" ng-click="GOP_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã nhóm SP', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Tên nhóm SP', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PriceOfGOPName', title: 'Tính theo', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Hàng hóa ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Driver_GridOptions.dataSource.read();
        }
    }

    $scope.Product_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.GOPMappingList,
            readparam: function () { return { groupOfProductID: $scope.gopID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ProductName: { type: 'string' },
                    PackingName: { type: 'string' },
                    IsKg: { type: 'bool' }
                }
            },
        }),
        height: '99%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        toolbar: kendo.template($('#product-grid-toolbar').html()),
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Product_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false, locked: true
            },
            { field: 'GroupOfProductName', title: 'Hàng hóa', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Khách hàng', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.PriceOfGOP_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'ValueOfVar',
        dataValueField: 'ID',
        index: 0,
        dataSource: Common.DataSource.Local({
            data: []
        })
    }

    $scope.Parent_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: false,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        index: 0,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' }
                }
            }
        }),
        dataBound: function (e) {
            //if (me._gopItemBind != null) {
            //    this.select(function (dataItem) {
            //        return dataItem.ID == me._gopItemBind.ParentID;
            //    })
            //}
        }
    }

    $scope.Product_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.GOPMappingNotinList,
            readparam: function () { return { groupOfProductID: $scope.gopID, venID: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    IsChoose: { type: 'boolean' },
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', filterable: { mode: 'row' }, sortable: false,

        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Product_NotinGrid,Product_NotinGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Product_NotinGrid,Product_NotinGridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfProductName', title: 'Hàng hóa', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Khách hàng', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }
    //#endregion

    //#region Event
    $scope.Ven_SaveAndApproved = function ($event, vform) {
        $event.preventDefault();
        $scope.VenData.IsApproved = true;
        $scope.Ven_Save($event, vform);
    }
    $scope.Ven_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.VenUpdate,
                data: { item: $scope.VenData },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $state.go("main.VENVendor.Detail", { id: res }, { reload: true })
                },
                error: function (e) {
                    $rootScope.IsLoading = false;
                }
            });
        }

    }

    $scope.Ven_Delete = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa dữ liệu đã chọn ?',
            pars: {},
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.VenDestroy,
                    data: { item: $scope.VenData },
                    success: function (res) {
                        $state.go('main.VENVendor.Index');
                    }
                })
            }
        });

    }

    $scope.Truck_Add = function ($event) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TruckGet,
            data: { id: 0 },
            success: function (res) {
                $scope.DetailItem = res;
                $scope.Truck_Win.center().open();
            }
        })

    }

    $scope.cboGroupOfVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'GroupName', dataValueField: 'ID',
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
    };

    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENDetail.URL.GroupOfVehicleList,
        data: {  },
        success: function (res) {
            $scope.cboGroupOfVehicle_Options.dataSource.data(res);
        }
    })

    $scope.Truck_Edit = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        var grid = $scope.Truck_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TruckGet,
            data: { id: item.ID },
            success: function (res) {

                $scope.DetailItem = res;
                $scope.Truck_Win.center().open();
            }
        })

    }

    $scope.Truck_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.TruckUrlUpdate,
                data: { item: $scope.DetailItem, id: _VENDetail.Param.ID },
                success: function (res) {
                    $scope.Truck_Grid.dataSource.read();
                    $scope.Truck_Win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Truck_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Truck_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.TruckUrlDestroy,
                    data: { item: item },
                    success: function (res) {
                        $scope.Truck_Grid.dataSource.read();
                    }
                })
            }
        });


    }

    $scope.Truck_Excel = function ($event, grid, win, type) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'RegNo', title: 'Số xe', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                {
                    title: 'Trọng tải', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                    columns: [
                        {
                            field: 'RegWeight', title: 'Đăng ký', width: '135px',
                            filterable: { cell: { operator: 'contains', showOperators: false } }
                        },
                        {
                            field: 'MaxWeight', title: 'Tối đa', width: '135px',
                            filterable: { cell: { operator: 'contains', showOperators: false } }
                        },
                    ]
                },
                {
                    title: 'Số khối', headerAttributes: { style: "text-align: center; vertical-align: middle;" },
                    columns: [
                        {
                            field: 'RegCapacity', title: 'Đăng ký', width: '135px',
                            filterable: { cell: { operator: 'contains', showOperators: false } }
                        },
                        {
                            field: 'MaxCapacity', title: 'Tối đa', width: '135px',
                            filterable: { cell: { operator: 'contains', showOperators: false } }
                        },
                    ]
                }
            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.TruckExport,
                    data: { customerID: _VENDetail.Param.ID, type: type },
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.TruckCheck,
                    data: { file: e.FilePath, customerID: _VENDetail.Param.ID, type: type },
                    success: function (data) {
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                var check = true;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].ExcelSuccess == false) {
                        check = false;
                        break;
                    }
                }
                if (check)
                    Common.Services.Call($http, {
                        url: Common.Services.url.VEN,
                        method: _VENDetail.URL.TruckImport,
                        data: { data: data, customerID: _VENDetail.Param.ID, type: type },
                        success: function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            grid.dataSource.read();
                        }
                    })
                else {
                    $rootScope.Message({ Msg: 'Có dữ liệu lỗi, không thể lưu', NotifyType: Common.Message.NotifyType.ERROR });
                }
            }
        })
    }


    $scope.Truck_Excel_Online = function ($event, grid,type) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_VENDetail.ExcelKey.ResourceTruck + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số xe tải] không được trống và > 50 ký tự',
                '[Số xe tải] nhập sai. VD: 20C-1234, 52EC-11223',
                '[Số xe tải] đã bị trùng',
                '[Số xe tải] đã tồn tại trong hệ thống',
                '[Số xe tải] đã tồn tại trong hệ thống',
                '[Mã loại xe] không tồn tại trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _VENDetail.ExcelKey.Truck,
            params: { customerID: _VENDetail.Param.ID, type: type },
            rowStart: 2,
            colCheckChange: 15,
            url: Common.Services.url.VEN,
            methodInit: _VENDetail.URL.Truck_ExcelInit,
            methodChange: _VENDetail.URL.Truck_ExcelChange,
            methodImport: _VENDetail.URL.Truck_ExcelImport,
            methodApprove: _VENDetail.URL.Truck_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.Truck_GridOptions.dataSource.read();
                $scope.Tractor_GridOptions.dataSource.read();
            }
        });
    };

    $scope.Tractor_Add = function ($event) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TractorGet,
            data: { id: 0 },
            success: function (res) {
                $scope.DetailItem = res;
                $scope.Tractor_Win.center().open();
                //$scope.masked_RegNo.setOptions({
                //    mask: "/\d{2}[a-zA-Z0-9]{2}-(\d{5}|\d{4})/"
                //})
            }
        })

    }

    $scope.Tractor_Edit = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        var grid = $scope.Tractor_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.TractorGet,
            data: { id: item.ID },
            success: function (res) {
                $scope.DetailItem = res;
                $scope.Tractor_Win.center().open();
            }
        })

    }

    $scope.Tractor_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.TractorUrlUpdate,
                data: { item: $scope.DetailItem, id: _VENDetail.Param.ID },
                success: function (res) {
                    $scope.Tractor_Grid.dataSource.read();
                    $scope.Tractor_Win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Tractor_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Tractor_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.TractorUrlDestroy,
                    data: { item: item },
                    success: function (res) {
                        $scope.Tractor_Grid.dataSource.read();
                    }
                })
            }
        });


    }


    $scope.Tractor_Excel_Online = function ($event, grid, type) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_VENDetail.ExcelKey.ResourceTractor + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số xe đầu kéo] không được trống và > 50 ký tự',
                '[Số xe đầu kéo] nhập sai. VD: 20C-1234, 52EC-11223',
                '[Số xe đầu kéo] đã bị trùng',
                '[Số xe đầu kéo] đã tồn tại trong hệ thống',
                '[Số xe đầu kéo] đã tồn tại trong hệ thống',
                '[Mã loại xe] không tồn tại trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _VENDetail.ExcelKey.Tractor,
            params: { customerID: _VENDetail.Param.ID, type: type },
            rowStart: 2,
            colCheckChange: 15,
            url: Common.Services.url.VEN,
            methodInit: _VENDetail.URL.Tractor_ExcelInit,
            methodChange: _VENDetail.URL.Tractor_ExcelChange,
            methodImport: _VENDetail.URL.Tractor_ExcelImport,
            methodApprove: _VENDetail.URL.Tractor_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.Truck_GridOptions.dataSource.read();
                $scope.Tractor_GridOptions.dataSource.read();
            }
        });
    };

    $scope.Truck_Search = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.Truck_NotIn_win_GridOptions.dataSource.read();
    }

    $scope.Truck_NotIn_win_SaveList = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose == true) dataSend.push(o.ID);
        })
        if (dataSend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.TruckNotInSave,
                data: { vendorID: _VENDetail.Param.ID, lst: dataSend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.Truck_GridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Tractor_Search = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.Tractor_NotIn_win_GridOptions.dataSource.read();
    }

    $scope.Tractor_NotIn_win_SaveList = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose == true) dataSend.push(o.ID);
        })
        if (dataSend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.TractorNotInSave,
                data: { vendorID: _VENDetail.Param.ID, lst: dataSend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.Tractor_GridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Romooc_Add = function ($event) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.RomoocGet,
            data: { id: 0 },
            success: function (res) {
                $scope.DetailItem = res;
                $scope.Romooc_Win.center().open();
            }
        })

    }

    $scope.Romooc_Edit = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        var grid = $scope.Romooc_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.RomoocGet,
            data: { id: item.ID },
            success: function (res) {
                $scope.DetailItem = res;
                $scope.Romooc_Win.center().open();
            }
        })

    }

    $scope.Romooc_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.RomoocUrlUpdate,
                data: { item: $scope.DetailItem, id: _VENDetail.Param.ID },
                success: function (res) {
                    $scope.Romooc_Grid.dataSource.read();
                    $scope.Romooc_Win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Romooc_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Romooc_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.RomoocUrlDestroy,
                    data: { item: item },
                    success: function (res) {
                        $scope.Romooc_Grid.dataSource.read();
                    }
                })
            }
        });


    }

    $scope.Romooc_Search = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.Romooc_NotIn_win_GridOptions.dataSource.read();
    }

    $scope.Romooc_NotIn_win_SaveList = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose == true) dataSend.push(o.ID);
        })
        if (dataSend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.RomoocNotInSave,
                data: { vendorID: _VENDetail.Param.ID, lst: dataSend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.Truck_GridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.Romooc_Excel_Online = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_VENDetail.ExcelKey.ResourceRomooc + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số xe] không được trống và > 50 ký tự',
                '[Số xe] nhập sai. VD: 20C-1234, 52EC-11223',
                '[Số xe] đã có trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: "VENDetail_Romooc_" + _VENDetail.Param.ID,
            params: { RommocID: _VENDetail.Param.ID },
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.VEN,
            methodInit: _VENDetail.URL.Romooc_ExcelInit,
            methodChange: _VENDetail.URL.Romooc_ExcelChange,
            methodImport: _VENDetail.URL.Romooc_ExcelImport,
            methodApprove: _VENDetail.URL.Romooc_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.Romooc_GridOptions.dataSource.read();
            }
        });
    };

    $scope.Driver_Excel_Online = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 6; i++) {
            var resource = $rootScope.RS[_VENDetail.ExcelKey.ResourceRomooc + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số CMND] Không được trống',
                '[Số CMND] đã sử dụng',
                '[Họ] không được trống',
                '[Tên] không được trống',
                '[Ngày sinh] không đúng định dạng (ngày/tháng/năm)',
                '[Loại bằng lái] không được trống',
                '[Số bằng lái] không được trống',
                '[Loại bằng lái] không tồn tại',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: "VENDetail_Driver_" + _VENDetail.Param.ID,
            params: { VendorID: _VENDetail.Param.ID },
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.VEN,
            methodInit: _VENDetail.URL.Driver_ExcelInit,
            methodChange: _VENDetail.URL.Driver_ExcelChange,
            methodImport: _VENDetail.URL.Driver_ExcelImport,
            methodApprove: _VENDetail.URL.Driver_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.Driver_GridOptions.dataSource.read()
            }
        });
    };

    $scope.Company_Add = function ($event) {
        $event.preventDefault();
        $scope.Company_Win.center().open();
        $timeout(function () {
            $scope.CompanyNotin_Grid.resize();
        }, 10)
    }

    $scope.Company_SaveList = function ($event) {
        $event.preventDefault();

        var grid = $scope.CompanyNotin_Grid;
        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.CompanyNotInUrlSave,
                data: { lst: data, id: _VENDetail.Param.ID },
                success: function (res) {
                    $scope.Company_Grid.dataSource.read();
                    $scope.CompanyNotin_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Company_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Company_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.CompanyUrlDelete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Company_Grid.dataSource.read();
                    }
                })
            }
        });


    }

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.GOP_Add = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.GOPGetByCode,
            data: { code: "", id: _VENDetail.Param.ID },
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.GOPAllRead,
                    data: { request: "", id: _VENDetail.Param.ID, gopID: $scope.gopID },
                    success: function (res) {
                        $scope.Parent_CbbOptions.dataSource.data(res.Data);
                    }
                });
                $scope.DetailItem = res;
                $scope.GOP_Win.center().open();
            }
        })
    }

    $scope.GOP_Edit = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        var grid = $scope.GOP_Tree;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.GOPGetByCode,
            data: { code: item.Code, id: _VENDetail.Param.ID },
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.GOPAllRead,
                    data: { request: "", id: _VENDetail.Param.ID, gopID: $scope.gopID },
                    success: function (res) {
                        $scope.Parent_CbbOptions.dataSource.data(res.Data);
                    }
                });
                $scope.DetailItem = res;
                $scope.GOP_Win.center().open();
            }
        })

    }

    $scope.GOP_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.GOPUpdate,
                data: { item: $scope.DetailItem, id: _VENDetail.Param.ID },
                success: function (res) {
                    $scope.GOP_Tree.dataSource.read();
                    $scope.GOP_Win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.GOP_Delete = function ($event) {
        $event.preventDefault();

        var grid = $scope.GOP_Tree;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.GOPDestroy,
                    data: { item: item },
                    success: function (res) {
                        $scope.GOP_Tree.dataSource.read();
                    }
                })
            }
        });
    }

    $scope.GOP_Reset = function ($event) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.GOPReset,
            data: { id: _VENDetail.Param.ID },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
    }

    $scope.Product_Add = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.Product_NotinGrid.dataSource.read();
    }

    $scope.Product_SaveList = function ($event) {
        $event.preventDefault();

        var grid = $scope.Product_NotinGrid;
        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.GOPMappingSave,
                data: { lst: data, groupOfProductID: $scope.gopID },
                success: function (res) {
                    $scope.Product_Grid.dataSource.read();
                    $scope.Product_NotinGrid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Product_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Product_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.GOPMappingDelete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Product_Grid.dataSource.read();
                    }
                })
            }
        });
    }

    $scope.Routing_SaveList = function ($event, grid) {
        $event.preventDefault();
        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.RoutingNotinSave,
                data: { lst: data, id: _VENDetail.Param.ID },
                success: function (res) {
                    $scope.Routing_Grid.dataSource.read();
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Routing_Delete = function ($event) {
        $event.preventDefault();
        var grid = $scope.Routing_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: item },
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.RoutingDelete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Routing_Grid.dataSource.read();
                    }
                })
            }
        });
    }

    $scope.Routing_Search = function ($event, win) {
        $event.preventDefault();
        $scope.FunctionName = "Tìm kiếm cung đường";
        $scope.Routing_NotinGrid.dataSource.read();
        win.center().open();
    }

    $scope.RoutingCus_Search = function ($event, win) {
        $event.preventDefault();
        $scope.FunctionName = "Tìm kiếm cung đường";
        $scope.RoutingCus_NotinGrid.dataSource.read();
        win.center().open();
    }

    $scope.Routing_Refresh = function ($event) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Routing_Reset,
            date: { id: _VENDetail.Param.ID },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
    }

    $scope.Product_Excel = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'GroupOfProductCode', width: 150, title: 'Mã nhóm hàng', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'GroupOfProductName', width: 250, title: 'Tên nhóm hàng', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'Code', width: 250, title: 'Mã hàng hóa', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'ProductName', width: 250, title: 'Tên hàng hóa', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _VENDetail.URL.TruckExport,
                    data: { customerID: _VENDetail.Param.ID },
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _VENDetail.URL.TruckCheck,
                    data: { file: e.FilePath, customerID: _VENDetail.Param.ID },
                    success: function (data) {
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                var check = true;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].ExcelSuccess == false) {
                        check = false;
                        break;
                    }
                }
                if (check)
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _VENDetail.URL.TruckImport,
                        data: { data: data, customerID: _VENDetail.Param.ID },
                        success: function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            grid.dataSource.read();
                        }
                    })
                else {
                    $rootScope.Message({ Msg: 'Có dữ liệu lỗi, không thể lưu', NotifyType: Common.Message.NotifyType.ERROR });
                }
            }
        })
    }
    //#endregion


    //#region Init


    Common.ALL.Get($http, {
        url: Common.ALL.URL.SLI_SYSVarPriceOfGOP,
        success: function (res) {
            $scope.PriceOfGOP_CbbOptions.dataSource.data(res)
        }
    });



    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _VENDetail.Data.Province[countryID];
            $scope.VenProvince_CbbOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _VENDetail.Data.District[provinceID];
            $scope.VenDistrict_CbbOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            //data = _SYSCustomer_Index.Data.Ward[districtID];
            //$scope.cboWardOptions.dataSource.data(data);
            //if (wardID < 1 && data.length > 0)
            //    wardID = data[0].ID;
            //$timeout(function () {
            //    item.WardID = wardID;
            //}, 1)
            $rootScope.IsLoading = false;
        }
        catch (e) { }
    }
    //#endregion

    //#region driver
    $scope.TabIndexDriver = 0;
    $scope.driver_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndexDriver = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndexDriver);
            }, 1)
        }
    }
    $scope.Driver_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Driver_List,
            readparam: function () { return { vendorId: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true }
                }
            }
        }),
        toolbar: kendo.template($('#Driver-grid-toolbar').html()),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '100px',
                template: '<a href="/" ng-click="Driver_Edit_Click($event,dataItem,Driver_Win,Driver_Form)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Driver_Delete_Click($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'CardNumber', title: 'Số CMND',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'LastName', title: 'Họ',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'FirstName', title: 'Tên',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'Cellphone', title: 'Số điện thoại',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'ListDrivingLicence', title: 'DS Bằng lái',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'Birthday', title: 'Ngày sinh', template: "#= Common.Date.FromJsonDMY(Birthday)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } }, width: '150px'
            },
            {
                field: 'Note', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                title: '', filterable: false, sortable: false
            },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin tài xế ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Contract_GridOptions.dataSource.read();
        }
    }

    $scope.Driver_Add_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.DriverLoadItem(0, win, vform)
    }

    $scope.Driver_Edit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.DriverLoadItem(item.ID, win, vform)
    }

    $scope.DriverLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Driver_Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemDriver = res;
                $scope.drivingLicence_GridOptions.dataSource.read();
                vform({ clear: true })
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Driver_Delete_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa tài xế đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.Driver_Delete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Driver_GridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.Driver_win_Submit_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.Driver_Save,
                data: { vendorId: _VENDetail.Param.ID, item: $scope.ItemDriver },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.VEN,
                        method: _VENDetail.URL.Driver_Get,
                        data: { id: res },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.ItemDriver = res;
                            $scope.drivingLicence_GridOptions.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                    $scope.Driver_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.DrivingLicence_CBbOoptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DrivingLicenceName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DrivingLicenceName: { type: 'string' },
                }
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _VENDetail.URL.CATDrivingLicence_List,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.DrivingLicence_CBbOoptions.dataSource.data(res.Data);
            }
        }
    });

    $scope.Driver_Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'CardNumber', title: 'Số CMND', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'LastName', title: 'Họ', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'FirstName', title: 'Tên', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.DriverExcel_Export,
                    data: { vendorId: _VENDetail.Param.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.DriverExcel_Check,
                    data: { file: e, vendorId: _VENDetail.Param.ID },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.DriverExcel_Save,
                    data: { lst: data, vendorId: _VENDetail.Param.ID },
                    success: function (res) {
                        $scope.Driver_GridOptions.dataSource.read()
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.driver_Search = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.driverNotIn_GridOptions.dataSource.read();
    }

    $scope.driverNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.DriverNotIn_List,
            readparam: function () { return { vendorId: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,driverNotIn_Grid,driverNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,driverNotIn_Grid,driverNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'CardNumber', title: 'Số CMND',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'LastName', title: 'Họ',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'FirstName', title: 'Tên',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'Cellphone', title: 'Số điện thoại',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'ListDrivingLicence', title: 'DS Bằng lái',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                field: 'Birthday', title: 'Ngày sinh', template: '#=Common.Date.FromJsonDDMMYY(Birthday)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } }, width: '150px'
            },
            {
                field: 'Note', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false }, width: '150px' }
            },
            {
                title: '', filterable: false, sortable: false
            },
        ]
    }
    $scope.driverNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.DriverNotInHasChoose = hasChoose;
    }

    $scope.driverNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.DriverNotIn_SaveList,
                data: { vendorId: _VENDetail.Param.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Driver_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.drivingLicence_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.DrivingLicence_List,
            readparam: function () { return { driverId: $scope.ItemDriver.DriverID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    ExpiredDate: { type: 'date' },
                    IsUse: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="drivingLicence_GridEdit_Click($event,dataItem, drivingLicence_win,drivingLicence_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="drivingLicence_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'DrivingLicenceName', title: "Loại bằng lái", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DrivingLicenceNumber', title: "Số bằng lái", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', title: "Ghi chú", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ExpiredDate', title: 'Ngày hết hạn', template: '#=Common.Date.FromJsonDDMMYY(ExpiredDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } }, width: '150px'
            },
            {
                field: 'IsUse', title: 'Đang sử dụng', width: 120,
                template: '<input type="checkbox" #= IsUse=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đang sử dụng', Value: true }, { Text: 'Không sử dụng', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.drivingLicence_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.DrivingLicenceLoadItem(0, win, vform)
    }

    $scope.drivingLicence_GridEdit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.DrivingLicenceLoadItem(item.ID, win, vform)
    }

    $scope.DrivingLicenceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.DrivingLicence_Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemDrivingLicence = res;
                vform({ clear: true })
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.drivingLicence_GridDestroy_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bằng lái đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.DrivingLicence_Delete,
                    data: { item: item },
                    success: function (res) {
                        $scope.drivingLicence_GridOptions.dataSource.read();
                        $scope.Driver_GridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.drivingLicence_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.DrivingLicence_Save,
                data: { driverId: $scope.ItemDriver.DriverID, item: $scope.ItemDrivingLicence },
                success: function (res) {
                    $scope.drivingLicence_GridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }
    //#endregion
    //#region Contract
    $scope.Contract_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.CustomerList,
            readparam: function () { return { vendorID: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    EffectDate: { type: 'date' },
                    ExpiredDate: { type: 'date' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                field: 'CustomerName', width: 250, title: 'Khách hàng', template: "",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractNo', width: 200, title: 'Số hợp đồng', template: "<a class='text' ng-click='DetailV2_Click($event, dataItem)' href='#=URL#'>#=ContractNo==null?\"\":ContractNo#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DisplayName', width: 250, title: 'Tên hiển thị', template: "",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeName', width: 100, title: 'Hình thức v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContractName', title: 'Loại hợp đồng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EffectDate', width: 120, title: 'Ngày hiệu lực', template: "#=EffectDate==null?' ':kendo.toString(EffectDate, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ExpiredDate', width: 120, title: 'Ngày hết hạn', template: "#=ExpiredDate==null?' ':kendo.toString(ExpiredDate, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin hợp đồng ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.location_gridOptions.dataSource.read();
        }
    };
    //#endregion 

    $scope.DetailV2_Click = function ($event, data) {
        $event.preventDefault();
        $state.go('main.VENVendor.Contract', { ID: data.ID, CustomerID: _VENDetail.Param.ID });
    };

    $scope.VenContract_Add = function ($event) {
        $event.preventDefault();
        $state.go('main.VENVendor.Contract', { ID: 0, CustomerID: _VENDetail.Param.ID });
    };

    $scope.numDrivingLicenceID_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    //#region danh sách địa chỉ
    $scope.location_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.VenLocation_List,
            readparam: function () { return { vendorId: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsVendorLoad: { type: 'boolane' },
                    IsVendorUnLoad: { type: 'boolane' },
                }
            }
        }),
        toolbar: kendo.template($('#location-grid-toolbar').html()),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
             {
                 title: ' ', width: 90,
                 template: '<a href="/" ng-click="location_RoutingClick($event,dataItem,routing_contract_win,routing_contract_grid)" ng-show="Auth.ActEdit" class="k-button"><i class="fa fa-random"></i></a>' +
                     '<a href="/" ng-click="location_DeleteClick($event,dataItem,location_grid)" ng-show="Auth.ActDel" class="k-button"><i class="fa fa-trash"></i></a>',
                 filterable: false, sortable: false
             },
            {
                field: 'CATLocationCode', title: 'Mã hệ thống', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CATLocationName', title: 'Tên hệ thống', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CUSLocationCode', title: 'Mã sử dụng', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CUSLocationName', title: 'Tên sử dụng', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ', width: 250,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Quận huyện', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingAreaCode', title: 'Mã Khu vực', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingAreaCodeNoUnicode', title: 'Mã Khu vực Không Unicode', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsVendorLoad', title: 'Bốc xếp lên', width: 150,
                template: '<input class="chkChoose" type="checkbox" ng-model="dataItem.IsVendorLoad"  />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không bốc xếp lên', Value: false }, { Text: 'Bốc xếp lên', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'IsVendorUnLoad', title: 'Bốc xếp xuống', width: 150,
                template: '<input class="chkChoose" type="checkbox" ng-model="dataItem.IsVendorUnLoad"  />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không bốc xếp xuống', Value: false }, { Text: 'Bốc xếp xuống', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Danh sách địa chỉ ...", $rootScope.Loading.Progress + LoadingStep);
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    }

    $scope.location_SaveLoadClick = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.VENLocationSaveLoad_List,
            data: { lst:data },
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

    $scope.location_DeleteClick = function ($event, data,grid) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENDetail.URL.VenLocation_Delete,
                    data: { id:data.ID },
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

    $scope.location_ExcelClick = function ($event ) {
        $event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_VENDetail.ExcelKey.ResourceLocation + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã địa điểm] không được trống ',
                '[Mã địa điểm] không có trong vendor',
                '[Mã địa điểm] đã bị trùng trên file',
                '[Mã khu vực] không được > 1000 ký tự',
                '[Mã khu vực không Unicode] không được > 1000 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _VENDetail.ExcelKey.Location,
            params: { vendorId: _VENDetail.Param.ID },
            rowStart: 1,
            colCheckChange: 12,
            url: Common.Services.url.VEN,
            methodInit: _VENDetail.URL.VenLocation_ExcelInit,
            methodChange: _VENDetail.URL.VenLocation_ExcelChange,
            methodImport: _VENDetail.URL.VenLocation_ExcelImport,
            methodApprove: _VENDetail.URL.VenLocation_ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.location_grid.dataSource.read();
            }
        });
    };

    $scope.location_searchClick = function ($event, win,grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.location_HasRunClick = function ($event) {
        $event.preventDefault();
        //url VenLocation_HasRun
    };

    $scope.locationNotIn_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.VenLocation_NotInList,
            readparam: function () { return { vendorId: _VENDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,locationNotIn_grid,locationNotIn_gridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,locationNotIn_grid,locationNotIn_gridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Location', title: 'Tên', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ', width: 250,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Quận huyện', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.location_RoutingClick = function ($event, data, win, grid) {
        $event.preventDefault();

        $scope.PartnerRouting_LocationID = data.LocationID;
        win.center();
        win.open();
        grid.dataSource.read();
    };
    
    $scope.locationNotIn_gridChange = function ($event, grid, hasChoose) {
        $scope.LocationNotInChoose = hasChoose;
    }
    $scope.location_ChoosseClick = function ($event, win,grid) {
        $event.preventDefault();
        var data = [];
        angular.forEach(grid.dataSource.data(), function (obj,index) {
            if (obj.IsChoose)
                data.push(obj);
        })

        if (data.length>0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.VenLocation_SaveList,
                data: { vendorId: _VENDetail.Param.ID, lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.location_grid.dataSource.read();
                    win.close();
                    $scope.LocationNotInChoose = false;
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    //#region cung duong theo location
    $scope.routing_contract_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Routing_Contract_List,
            readparam: function () { return { vendorId: _VENDetail.Param.ID, locationid: $scope.PartnerRouting_LocationID } },
            model: {
                id: 'CUSRoutingID',
                fields: {
                    CUSRoutingID: { type: 'number' },
                    IsCheckFrom: { type: 'boolean' },
                    IsCheckTo: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            { field: 'CATRoutingCode', title: 'Mã hệ thống', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATRoutingName', title: 'Tên hệ thống', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSRoutingCode', title: 'Mã sử dụng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSRoutingName', title: 'Tên sử dụng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContractCode', title: 'Mã hợp đồng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContractName', title: 'Tên hợp đồng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TermCode', title: 'Mã phụ lục', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TermName', title: 'Tên phụ lục', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: "IsCheckFrom", title: ' ', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" ng-model="dataItem.IsCheckFrom" />',
                filterable: false
            },
            { field: 'AreaFromCode', title: 'Mã kv đi', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaFromName', title: 'Tên kv đi', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: "IsCheckTo", title: ' ', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" ng-model="dataItem.IsCheckTo" />',
                filterable: false
            },
            { field: 'AreaToCode', title: 'Mã kv đến', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaToName', title: 'Tên kv đến', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.PartnerRouting_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.view();
        var lstClear = [];
        var lstAdd = [];
        Common.Data.Each(data, function (o) {
            lstClear.push(o.AreaFromID);
            lstClear.push(o.AreaToID);
            if (o.IsCheckFrom) {
                lstAdd.push(o.AreaFromID)
            }
            if (o.IsCheckTo) {
                lstAdd.push(o.AreaToID)
            }
        })

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Routing_Contract_SaveList,
            data: { lstClear: lstClear, lstAdd: lstAdd, locationid: $scope.PartnerRouting_LocationID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                win.close();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.PartnerRouting_AddClick = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Routing_Contract_NewRoutingGet,
            data: { vendorId: _VENDetail.Param.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.NewItemRouting = res;
                win.center();
                win.open();

            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.PartnerNewRouting_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENDetail.URL.Routing_Contract_NewRoutingSave,
                data: { item: $scope.NewItemRouting, vendorId: _VENDetail.Param.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã lưu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.routing_contract_gridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.cboRoutingContract_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DisplayName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENDetail.URL.Routing_Contract_ContractData,
        data: { vendorId: _VENDetail.Param.ID },
        success: function (res) {
            $scope.cboRoutingContract_Options.dataSource.data(res);
        }
    });

    $scope.PartnerRoutingArea_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Routing_Contract_AreaList,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-click="ChooseArea_ChooseClick($event,dataItem,PartnerRoutingArea_win)"  class="k-button"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã khu vực', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaName', title: 'Tên khu vực', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.ChooseArea_Click = function ($event, win, grid, type) {
        $event.preventDefault();
        $scope.IsLocationFrom = type;
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.ChooseArea_ChooseClick = function ($event, data, win) {
        $event.preventDefault();
        if ($scope.IsLocationFrom) {
            $scope.NewItemRouting.AreaFromID = data.ID;
            $scope.NewItemRouting.AreaFromCode_Name = data.Code + " - " + data.AreaName;
        }
        else {
            $scope.NewItemRouting.AreaToID = data.ID;
            $scope.NewItemRouting.AreaToCode_Name = data.Code + " - " + data.AreaName;
        }
        win.close();
    };

    $scope.AddArea_Click = function ($event, win) {
        $event.preventDefault();
        $scope.NewItemArea = {
            Code: "",
            AreaName: ""
        }

        win.center();
        win.open();
    };

    $scope.PartnerNewArea_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENDetail.URL.Routing_Contract_NewAreaSave,
            data: { locationid: $scope.PartnerRouting_LocationID, item: $scope.NewItemArea },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.PartnerRoutingArea_GridOptions.dataSource.read();
                win.close();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };
    //#endregion
    //#endregion

}]);