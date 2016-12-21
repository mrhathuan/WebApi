/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSetting_Detail = {
    URL: {
        Data: 'FLMContract_Data',
        Get: 'FLMContract_Get',
        Save: 'FLMContract_Save',
        Delete: 'FLMContract_Delete',

        Price_List: 'FLMContract_DriverFee_List',
        Price_Delete: 'FLMContract_DriverFee_Delete',
        Price_Get: 'FLMContract_DriverFee_Get',
        Price_Save: 'FLMContract_DriverFee_Save',

        TypeOfDriverFee_List: 'ALL_CATTypeOfDriverFee',
        DriverFeeSum_List: 'CATDriverFeeSum_List',
        TypeOfDriver: 'ALL_SYSVarTypeOfDriver',

        CODefault_List: 'FLMContract_CODefault_List',
        CODefault_Save: 'FLMContract_CODefault_Update',
        CODefault_NotIn_List: 'FLMContract_CODefault_NotInList',
        CODefault_NotIn_Save: 'FLMContract_CODefault_NotIn_SaveList',
        CODefault_Delete: 'FLMContract_CODefault_Delete',

        Price_GroupLocation_List: 'FLMContract_DriverFee_GroupLocation_List',
        Price_GroupLocation_Delete: 'FLMContract_DriverFee_GroupLocation_DeleteList',
        Price_GroupLocation_Save: 'FLMContract_DriverFee_GroupLocation_SaveList',
        Price_GroupLocation_NotInList: 'FLMContract_DriverFee_GroupLocation_GroupNotInList',

        Price_Location_List: 'FLMContract_DriverFee_Location_List',
        Price_Location_Delete: 'FLMContract_DriverFee_Location_DeleteList',
        Price_Location_Save: 'FLMContract_DriverFee_Location_Save',
        Price_Location_Get: 'FLMContract_DriverFee_Location_Get',
        Price_Location_NotInSave: 'FLMContract_DriverFee_Location_LocationNotInSaveList',
        Price_Location_NotInList: 'FLMContract_DriverFee_Location_LocationNotInList',

        Price_Route_List: 'FLMContract_DriverFee_Route_List',
        Price_Route_Delete: 'FLMContract_DriverFee_Route_DeleteList',
        Price_Route_Save: 'FLMContract_DriverFee_Route_SaveList',
        Price_Route_NotInList: 'FLMContract_DriverFee_Route_RouteNotInList',

        Price_ParentRoute_List: 'FLMContract_DriverFee_ParentRoute_List',
        Price_ParentRoute_Delete: 'FLMContract_DriverFee_ParentRoute_DeleteList',
        Price_ParentRoute_Save: 'FLMContract_DriverFee_ParentRoute_SaveList',
        Price_ParentRoute_NotInList: 'FLMContract_DriverFee_ParentRoute_RouteNotInList',

        Price_Customer_List: 'FLMContract_DriverFee_Customer_List',
        Price_Customer_Delete: 'FLMContract_DriverFee_Customer_DeleteList',
        Price_Customer_Save: 'FLMContract_DriverFee_Customer_SaveList',
        Price_Customer_NotInList: 'FLMContract_DriverFee_Customer_GroupNotInList',

        Price_GroupProduct_List: 'FLMContract_DriverFee_GroupProduct_List',
        Price_GroupProduct_Delete: 'FLMContract_DriverFee_GroupProduct_DeleteList',
        Price_GroupProduct_Save: 'FLMContract_DriverFee_GroupProduct_Save',
        Price_GroupProduct_Get: 'FLMContract_DriverFee_GroupProduct_Get',
        Price_GroupProduct_NotInSave: 'FLMContract_DriverFee_GroupProduct_NotInSaveList',
        Price_GroupProduct_NotInList: 'FLMContract_DriverFee_GroupProduct_NotInList',

        Excel_Check: 'FLMDriverFee_Excel_Check',
        Excel_Import: 'FLMDriverFee_Import',
        Excel_Export: 'FLMDriverFee_Export',

        ExcelTemp_Check: 'FLMDriverFeeTemp_Excel_Check',
        ExcelTemp_Import: 'FLMDriverFeeTemp_Import',
        ExcelTemp_Export: 'FLMDriverFeeTemp_Export',

        Read_LocationNotIn: 'FLMContract_NewRouting_LocationList',
        Routing_List: 'FLMContract_Routing_List',
        Get_Routing: 'FLMContract_NewRouting_Get',
        Routing_NotIn_List: 'FLMContract_Routing_NotIn_List',
        Routing_Insert: 'FLMContract_Routing_Insert',
        Read_AreaNotIn: 'FLMContract_NewRouting_AreaList',
        Refresh_RoutingArea: 'FLMContract_NewRouting_AreaRefresh',
        Delete_Area: 'FLMContract_NewRouting_AreaDelete',
        Get_Area: 'FLMContract_NewRouting_AreaGet',
        Save_Area: 'FLMContract_NewRouting_AreaSave',
        Read_AreaDetail: 'FLMContract_NewRouting_AreaDetailList',
        Delete_AreaDetail: 'FLMContract_NewRouting_AreaDetailDelete',
        Get_AreaDetail: 'FLMContract_NewRouting_AreaDetailGet',
        Save_AreaDetail: 'FLMContract_NewRouting_AreaDetailSave',
        Save_Routing: 'FLMContract_NewRouting_Save',
        Routing_ContractTerm: 'FLMContract_Routing_ContractTermList',
        Routing_Save: 'FLMContract_Routing_Save',
        KPI_Save: 'FLMContract_KPI_Save',
        KPI_Routing_List: 'FLMContract_KPI_Routing_List',
        KPI_Routing_Apply: 'FLMContract_KPI_Routing_Apply',
        KPI_Check_KPI: 'FLMContract_KPI_Check_Hit',
        KPI_Check_Expression: 'FLMContract_KPI_Check_Expression',
        Routing_Delete: 'FLMContract_Routing_Delete',
        ContractRouting_Delete: 'FLMContract_Routing_NotIn_Delete',
        Routing_search_List: 'FLMContract_Routing_CATNotIn_List',
        Routing_search_save: 'FLMContract_Routing_CATNotIn_Save',
        Routing_Excel_Check: 'FLMContract_Routing_Excel_Check',
        Routing_Excel_Export: 'FLMContract_Routing_Export',
        Routing_Excel_Import: 'FLMContract_Routing_Import',
        TypeOfRunLevelSave: "FLMContract_Setting_TypeOfRunLevelSave",
        LevelList: 'FLMContract_Setting_LevelList',
        LevelGet: 'FLMContract_Setting_LevelGet',
        LevelSave: 'FLMContract_Setting_LevelSave',
        LevelDeleteList: 'FLMContract_Setting_LevelDeleteList',
        GOVListSetting: 'FLMContract_Setting_Level_GOVList',

        GOVList: 'FLMContract_Setting_GOVList',
        GOVGet: 'FLMContract_Setting_GOVGet',
        GOVSave: 'FLMContract_Setting_GOVSave',
        GOVDeleteList: 'FLMContract_Setting_GOVDeleteList',
        GOVNotInList: 'FLMContract_Setting_GOVNotInList',
        GOVNotInSave: 'FLMContract_Setting_GOVNotInSave',

        Term_List: 'FLMContract_ContractTerm_List',
        Term_Get: 'FLMContract_ContractTerm_Get',
        Term_Save: 'FLMContract_ContractTerm_Save',
        Term_Delete: 'FLMContract_ContractTerm_Delete',
        Term_Price_List: 'FLMContract_ContractTerm_Price_List',
        Term_Lock: 'FLMContract_ContractTerm_Close',
        Term_UnLock: 'FLMContract_ContractTerm_Open',
        Term_RemoveWarning: 'FLMTerm_Change_RemoveWarning',
        Routing_ContractTerm: 'FLMContract_Routing_ContractTermList',
        contractSetting_save: "FLMContractSetting_Save",

        PriceCopyRead: 'FLMSetting_Price_Copy',
        PriceCopySave: 'FLMSetting_PriceCopy_Save',
        CompanyUrlRead: "FLMSetting_Vendor_List",

        AreaLocation_List: 'FLMContract_NewRouting_AreaLocation_List',
        AreaLocation_NotInList: 'FLMContract_NewRouting_AreaLocationNotIn_List',
        AreaLocation_NotInSave: 'FLMContract_NewRouting_AreaLocationNotIn_Save',
        AreaLocation_Delete: 'FLMContract_NewRouting_AreaLocation_Delete',
        AreaLocation_Copy: 'FLMContract_NewRouting_AreaLocation_Copy',

        GroupOfProduct_List: 'FLMContract_GroupOfProduct_List',
        GroupOfProduct_Get: 'FLMContract_GroupOfProduct_Get',
        GroupOfProduct_Save: 'FLMContract_GroupOfProduct_Save',
        GroupOfProduct_Delete: 'FLMContract_GroupOfProduct_Delete',
        GroupOfProduct_Check: 'FLMContract_GroupOfProduct_Check',

        TypeOfSGroupProductChangeSave: 'FLMContract_Setting_TypeOfSGroupProductChangeSave',

        Partner_ExcelInit: 'FLM_Partner_ExcelInit',
        Partner_ExcelChange: 'FLM_Partner_ExcelChange',
        Partner_ExcelImport: 'FLM_Partner_ExcelImport',
        Partner_ExcelApprove: 'FLM_Partner_ExcelApprove',

        Partner_List: 'FLM_Partner_List',
        Partner_Get: 'FLM_Partner_Get',
        Partner_CUSLocationSaveCode: 'FLM_Partner_CUSLocationSaveCode',

        FilterByPartner_List: 'FLM_Partner_FilterByPartner_List',
        FilterByLocation_List: 'FLM_Partner_FilterByLocation_List',
        FilterByPartner_GetNum: 'FLM_Partner_FilterByPartner_GetNumOfCusLocation',

        PartnerUrlRead: "Partner_List",
        PartnerUrlUpdate: "Partner_Save",
        PartnerUrlDestroy: "Partner_Delete",
        PartnerLocationRead: "PartnerLocation_List",
        PartnerlocationUrlSaveList: "PartnerLocation_SaveList",
        PartnerUrlGet: "Partner_Get",
        Partner_Export: "FLM_PartnerLocation_Export",
        Partner_Check: "FLM_PartnerLocation_Check",
        Partner_Import: "FLM_PartnerLocation_Import",

        PartnerUrlSaveList: "Partner_SaveList",
        PartnerNotInUrlRead: "PartnerNotIn_Read",
        PartnerlocationUrlGet: "PartnerLocation_Get",
        PartnerlocationUrlDestroy: "PartnerLocation_Destroy",
        PartnerlocationUrlUpdate: "PartnerLocation_Save",
        PartnerLocation_SaveList: "PartnerLocation_SaveList",
        Routing_Contract_List: 'FLM_Partner_RoutingContract_List',
        Routing_Contract_SaveList: 'FLM_Partner_RoutingContract_SaveList',
        Routing_Contract_NewRoutingSave: 'FLM_Partner_RoutingContract_NewRoutingSave',
        Routing_Contract_ContractData: 'FLM_Partner_RoutingContract_ContractData',
        Routing_Contract_NewRoutingGet: 'FLM_Partner_RoutingContract_NewRoutingGet',
        Routing_Contract_NewAreaSave: 'FLM_Partner_RoutingContract_NewAreaSave',
        Routing_Contract_AreaList: 'FLM_Partner_RoutingContract_AreaList',

        RoutingExcelOnline_Init: 'FLMContract_Routing_ExcelOnline_Init',
        RoutingExcelOnline_Change: 'FLMContract_Routing_ExcelOnline_Change',
        RoutingExcelOnline_Import: 'FLMContract_Routing_ExcelOnline_Import',
        RoutingExcelOnline_Approve: 'FLMContract_Routing_ExcelOnline_Approve',

        GroupOfProduct_ExcelInit: 'FLMContract_GroupOfProduct_ExcelInit',
        GroupOfProduct_ExcelChange: 'FLMContract_GroupOfProduct_ExcelChange',
        GroupOfProduct_ExcelImport: 'FLMContract_GroupOfProduct_ExcelImport',
        GroupOfProduct_ExcelApprove: 'FLMContract_GroupOfProduct_ExcelApprove',
    },
    Data: {
        Province: {},
        District: {},
        ListCompany: null,
        ListCustomer: null,
        ListMaterial: null,
        ListKPI: null,
        ListGOP: null,
        ListGOPHasEmpty: null,
        ListProduct: null,
        ListSetting: [],
    },
    Const: {
        CustomerID: -1,
        CompanyID: -1,
    },
    ExcelKey: {
        Partner_and_Stock: 'FLMSetting_Detail_PartnerStock',
        Resource: "FLMCustomer_Contract_Excel",
        ContractRouting: "FLMContractRouting",
        GroupOfProduct: "CUSContractGroupOfProduct",
        ResourceGroupOfProduct: "CUSContract_GroupOfProduct_Excel",
    },
}

angular.module('myapp').controller('FLMSetting_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_DetailCtrl');
    $rootScope.IsLoading = false;
    $rootScope.Loading.Show('Thông tin đội xe');
    var LoadingStep = 40;
    $scope.TabIndex = 0;
    $scope.ID = $state.params.ID;
    $scope.Item = null;
    $scope.TabIndexPrice = 0;
    $scope.ItemPrice = { ID: 0 };

    $scope.GLHasChoose = false;
    $scope.LocationHasChoose = false;
    $scope.RouteHasChoose = false;
    $scope.ParentRouteHasChoose = false;
    $scope.CustomerHasChoose = false;
    $scope.GroupProductHasChoose = false;
    $scope.CUSRoutingItem = {};
    $scope.CUSRoutingItem.IsArea = false;
    $scope.PointLocationItem = {};
    $scope.CurrentAreaID = 0;
    $scope.AreaLocationItem = {};
    $scope.ParamEdit = {};
    $scope.ParamService = { ServiceOfOrderID: -1, GetEmpty: 0, ReturnEmpty: 0 };
    $scope.ItemPartner = { ID: 0 };
    $scope.ListFilterPartnerIDChoose = [];
    $scope.ListFilterLocationIDChoose = [];
    $scope.ListFilterCusLocationIDChoose = [];
    $scope.IsFilterPartner = false;
    $scope.IsFilterLocation = false;
    $scope.IsFilterUseLocation = false;
    $scope.IsLocationFrom = false;
    $scope.StockTypeID = 4;
    $scope.typeOfPartnerNotin = 1;
    //#region kpi
    $scope.KPIItemCheck = {
        ID: -1,
        KPICode: '',
        DateRequest: new Date(),
        DateDN: new Date(),
        DateFromCome: new Date(),
        DateFromLeave: new Date(),
        DateFromLoadStart: new Date(),
        DateFromLoadEnd: new Date(),
        DateToCome: new Date(),
        DateToLeave: new Date(),
        DateToLoadStart: new Date(),
        DateToLoadEnd: new Date(),
        DateInvoice: new Date(),
        ETARequest: new Date(),
        Expression: '',
        Zone: 0,
        LeadTime: 0,
        CompareField: ''
    }

    $scope.contract_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        navigatable : false,
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
            }, 1);
        }
    }

    //#region cbx
    $scope.cboTransportOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }
    $scope.cboServiceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (res) {
            $timeout(function () {
                $scope.cboServiceOptions.dataSource.data(res);
            }, 1);
        }
    });

    $scope.cboTransportFTCFCLOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (res) {
            $timeout(function () {
                var data = [];
                $.each(res, function (i, v) {
                    if (v.TypeOfVar == 33 || v.TypeOfVar == 31) {
                        data.push(v)
                    }
                });
                $scope.cboTransportOptions.dataSource.data(res);
                //$scope.cboTransportFTCFCLOptions.dataSource.data(data);
            }, 1);
        }
    });

    $scope.cboContractDateOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfContractDate,
        success: function (res) {
            $timeout(function () {
                $scope.cboContractDateOptions.dataSource.data(res);
            }, 1);
        }
    });
    //#endregion

    //#region search_routing
    $scope.Routing_Search_Click = function ($event, win) {
        $event.preventDefault();
        $scope.routing_search_gridOptions.dataSource.read();
        $timeout(function () { $scope.routing_search_grid.resize(); }, 1);
        win.center().open();
    };

    $scope.routing_search_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_search_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: 'Chọn', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,routing_search_grid)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,routing_search_grid)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: "", filterable: false, sortable: false
            }
        ]
    };

    $scope.Routing_Search_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true) data.push(o);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Routing_search_save,
                data: { contractID: $scope.ID, data: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $scope.routing_gridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    //#endregion

    //#region routing
    $scope.routing_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_List,
            readparam: function () { return { contractID: $scope.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SortOrder: { type: 'number' },
                }
            },
            pageSize: 20,
            sort: [{ field: "SortOrder", dir: "asc" }],
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        //toolbar: kendo.template($('#routing_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '160px',
                template: '<a href="/" ng-click="Routing_Edit_Click($event,dataItem,routing_info_win)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Routing_KPI_Click($event,dataItem,kpi_info_win)" class="k-button"><i class="fa fa-info"></i></a>' +
                    '<a href="/" ng-click="Routing_CATAreaDetail_Click($event,dataItem,RoutingAreaDetail_win)" class="k-button" ng-show="dataItem.IsArea"><i class="fa fa-map-marker"></i></a>' +
                    '<a href="/" ng-click="Routing_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'SortOrder', title: 'STT', template: '#= SortOrder != 0 ? SortOrder : "" #', width: 60, filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CATCode', width: 150, title: 'Mã hệ thống', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'LeadTime', title: 'LeadTime', width: 120, template: '#=LeadTime != null? Common.Number.ToNumber2(LeadTime) : "" #', filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'Zone', title: 'Zone', template: '#=Zone != null? Zone : "" #', width: 120, filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'ContractTermCode', title: 'Phụ lục hợp đồng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractRoutingType', title: 'Loại', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Cung đường ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.term_gridOptions.dataSource.read();
        }
    };

    //#region Area Detail
    $scope.AreaLocation_TabIndex = 0;
    $scope.AreaLocation_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.AreaLocation_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.AreaLocation_TabIndex);
            }, 1)
        }
    }

    $scope.CurrentAreaFromLocationID = -1;
    $scope.CurrentAreaToLocationID = -1;
    $scope.Routing_CATAreaDetail_Click = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Get_Routing,
            data: { ID: data.RoutingID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.CUSRoutingItem = res;
                if (res.ID > 0) {
                    $scope.AreaLocationItem.LocationFrom = res.AreaFromName;
                    $scope.AreaLocationItem.LocationTo = res.AreaToName;

                    $scope.CurrentAreaFromLocationID = res.RoutingAreaFromID;
                    $scope.CurrentAreaToLocationID = res.RoutingAreaToID;
                }
                win.center().open();
                $scope.AreaLocationFrom_GridOptions.dataSource.read();
                //$scope.AreaLocationTo_GridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    //#region CODefault
    $scope.HasCODefault = false;

    $scope.codefault_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.CODefault_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string', editable: false },
                    Ton: { type: 'number' }
                }
            },
            pageSize: 0
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: false, editable: 'incell',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,codefault_grid,codefault_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,codefault_grid,codefault_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'PackingName', title: 'Loại container' },
            { field: 'Ton', title: 'Trọng tải (tấn)', editor: "<input type='number' class='k-textbox' step='0.001' min='0' ng-model='dataItem.Ton' />" }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thiết lập container ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.gop_gridOptions.dataSource.read();
        }
    };

    $scope.codefault_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasCODefault = hasChoose;
    };

    $scope.codefault_notin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.CODefault_NotIn_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string', editable: false },
                    TypeOfPackageName: { type: 'string' }
                }
            },
            pageSize: 0
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: false, editable: false,

        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,codefault_notin_grid,codefault_notin_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,codefault_notin_grid,codefault_notin_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã cont' },
            { field: 'PackingName', title: 'Tên cont' },
            { field: 'TypeOfPackageName', title: 'Loại cont' },
        ]
    };

    $scope.CODefault_Save_Click = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.CODefault_Save,
            data: { data: data, contractID: $scope.ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã cập nhật." });
                    $scope.codefault_gridOptions.dataSource.read();
                })
            }
        });
    };

    $scope.CODefault_NotIn_Click = function ($event, grid, win) {
        $event.preventDefault();

        grid.dataSource.read();
        win.center().open();
    };

    $scope.CODefault_NotIn_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true; });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.CODefault_NotIn_Save,
                data: { data: data, contractID: $scope.ID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật." });
                        $scope.codefault_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                    })
                }
            })
        } else {
            win.close();
        }
    };

    $scope.CODefault_Delete_Click = function ($event, grid) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true; });
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có chắc muốn xóa?',
                Close: null,
                Ok: function () {
                    var lst = [];
                    angular.forEach(data, function (v, i) {
                        lst.push(v.ID);
                    });
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.CODefault_Delete,
                        data: { data: lst },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã cập nhật." });
                                $scope.codefault_gridOptions.dataSource.read();
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            });
        }
    };
    //#endregion

    //#region location area from
    $scope.AreaLocationFromHasChoose = false;

    $scope.AreaLocationFrom_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.AreaLocation_List,
            readparam: function () { return { areaID: $scope.CurrentAreaFromLocationID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pagesize:20,
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,AreaLocationFrom_Grid,AreaLocationFrom_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,AreaLocationFrom_Grid,AreaLocationFrom_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'LocationName', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationCode', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Country', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Province', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'District', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPartnerName', title: 'Loại partner', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            $scope.AreaLocationTo_GridOptions.dataSource.read();
        }
    }

    $scope.AreaLocationFrom_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.AreaLocationFromHasChoose = hasChoose;
    }

    $scope.AreaLocationFromNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.AreaLocation_NotInSave,
                data: { areaID: $scope.CurrentAreaFromLocationID, lstID: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocationFrom_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    $scope.AreaLocationFrom_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.AreaLocation_Delete,
                data: { lstID: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocationFrom_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.AreaLocationFrom_AddNew = function ($event, win) {
        $event.preventDefault();
        $scope.AreaLocationNotInHasChoose = false;
        $scope.CurrentAreaLocationID = $scope.CurrentAreaFromLocationID;
        win.center().open();
        $scope.AreaLocationFromNotIn_GridOptions.dataSource.read();
        $scope.AreaLocationFromNotIn_Grid.refresh();
    }
    //#ednregion

    //#region location area to
    $scope.AreaLocationToHasChoose = false;

    $scope.AreaLocationTo_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.AreaLocation_List,
            readparam: function () { return { areaID: $scope.CurrentAreaToLocationID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pagesize:20,
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,AreaLocationTo_Grid,AreaLocationTo_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,AreaLocationTo_Grid,AreaLocationTo_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'LocationName', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationCode', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Country', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Province', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'District', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPartnerName', title: 'Loại partner', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]

    }

    $scope.AreaLocationTo_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.AreaLocationToHasChoose = hasChoose;
    }

    $scope.AreaLocationToNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.AreaLocation_NotInSave,
                data: { areaID: $scope.CurrentAreaToLocationID, lstID: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocationTo_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.AreaLocationTo_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.AreaLocation_Delete,
                data: { lstID: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocationTo_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.AreaLocationTo_AddNew = function ($event, win) {
        $event.preventDefault();
        $scope.AreaLocationNotInHasChoose = false;
        $scope.CurrentAreaLocationID = $scope.CurrentAreaToLocationID;
        win.center().open();
        $scope.AreaLocationToNotIn_GridOptions.dataSource.read();
        $scope.AreaLocationToNotIn_Grid.refresh();
    }
    //#ednregion
    //#endregion

    $scope.RoutingTypeBak = -1;
    $scope.ContractTermBak = -1;
    $scope.Routing_Edit_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.RoutingItem = null;
        $scope.RoutingItem = $.extend({}, true, item);
        $scope.RoutingTypeBak = $scope.RoutingItem.ContractRoutingTypeID;
        $scope.ContractTermBak = $scope.RoutingItem.ContractTermID;

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_ContractTerm,
            data: { contractID: $scope.ID },
            success: function (res) {
                $scope.cboContractTermOptions.dataSource.data(res);
                win.center().open();
                $rootScope.IsLoading = false;
            },
            error: function () {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.cboContractTermOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                Code: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    $scope.numSortOrder_options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0, }

    $scope.cboContractRoutingTypeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarContractRoutingType,
        success: function (data) {
            $scope.cboContractRoutingTypeOptions.dataSource.data(data);
        }
    })

    $scope.Routing_Info_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();
        if (!Common.HasValue($scope.RoutingItem.ContractRoutingTypeID) || $scope.RoutingItem.ContractRoutingTypeID < 0) {
            $rootScope.Message({ Msg: 'Chưa chọn loại cung đường.', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            if ($scope.RoutingTypeBak != $scope.RoutingItem.ContractRoutingTypeID && ($scope.ContractTermBak > 0 && $scope.ContractTermBak != $scope.RoutingItem.ContractTermID)) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn có muốn thay đổi loại cung đường và phụ lục? (Cung đường ở bảng giá nếu có sẽ bị xóa)',
                    Ok: function () {
                        $scope.Routing_Info_Save(grid, win);
                    },
                    Close: function () {
                    }
                })
            } else if ($scope.RoutingTypeBak != $scope.RoutingItem.ContractRoutingTypeID) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn có muốn thay đổi loại cung đường? (Cung đường ở bảng giá nếu có sẽ bị xóa)',
                    Ok: function () {
                        $scope.Routing_Info_Save(grid, win);
                    },
                    Close: function () {
                    }
                })
            } else if ($scope.ContractTermBak > 0 && $scope.ContractTermBak != $scope.RoutingItem.ContractTermID) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn có muốn thay đổi phụ lục? (Cung đường ở bảng giá nếu có sẽ bị xóa)',
                    Ok: function () {
                        $scope.Routing_Info_Save(grid, win);
                    },
                    Close: function () {
                    }
                })
            } else {
                $scope.Routing_Info_Save(grid, win);
            }
        }
    }
    $scope.Routing_Info_Save = function (grid, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_Save,
            data: { item: $scope.RoutingItem },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    $scope.routing_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.IsLoading = false;
                })
            }
        });
    }
    $scope.Routing_KPI_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.RoutingItem = $.extend({}, true, item);
        $scope.KPIItemCheck.Zone = item.Zone > 0 ? item.Zone : 0;
        $scope.KPIItemCheck.LeadTime = item.LeadTime > 0 ? item.LeadTime : 0;
        win.center().open();
        $scope.kpi_gridOptions.dataSource.data(item.ListKPI);
    }
    $scope.kpi_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    KPICode: { type: 'string', editable: false },
                    KPIName: { type: 'string', editable: false },
                }
            },
            pageSize: 0,
            sort: [{ field: "KPICode", dir: "asc" }]
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true, editable: 'incell',
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" title="Kiểm tra" ng-click="KPI_Check_Click($event,dataItem,kpi_check_win)" class="k-button"><i class="fa fa-bolt"></i></a>'
            },
            { field: 'KPICode', title: 'Mã', width: 100 },
            { field: 'KPIName', title: 'Tên KPI', width: 200 },
            { field: 'Expression', title: 'Biểu thức' },
            { field: 'CompareField', title: 'TG so sánh', width: 120 }
        ]
    }
    $scope.Routing_KPI_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.RoutingItem = $.extend({}, true, item);
        $scope.KPIItemCheck.Zone = item.Zone > 0 ? item.Zone : 0;
        $scope.KPIItemCheck.LeadTime = item.LeadTime > 0 ? item.LeadTime : 0;
        //debugger

        //var expTemp = {};
        //var comTemp = {};
        //var data = $scope.kpi_gridOptions.dataSource.data();
        //Common.Data.Each(item.ListKPI, function (o) {
        //    expTemp[o.KPIID] = o.Expression;
        //    comTemp[o.KPIID] = o.CompareField;
        //})
        //Common.Data.Each(data, function (o) {
        //    if (Common.HasValue(expTemp[o.KPIID])) {
        //        o.Expression = expTemp[o.KPIID];
        //    }
        //    else {
        //        o.Expression = "";
        //    }
        //    if (Common.HasValue(comTemp[o.KPIID])) {
        //        o.CompareField = comTemp[o.KPIID];
        //    }
        //    else {
        //        o.CompareField = "";
        //    }
        //})
        win.center().open();
        $scope.kpi_gridOptions.dataSource.data(item.ListKPI);
    }
    $scope.KPI_SaveChanges_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.KPI_Save,
            data: { data: data, routingID: $scope.RoutingItem.ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    Common.Data.Each(grid.dataSource.data(), function (o) { o.dirty = false });
                    grid.dataSource.sync();
                    $scope.routing_gridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                })
            }
        });
    }
    $scope.KPI_Apply_ToAll_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var flag = false;
        Common.Data.Each(data, function (o) {
            if (o.dirty)
                flag = true;
        })
        if (flag) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert, Msg: "Lưu thay đổi trước."
            })
        }
        else {
            win.center().open();
            $scope.kpi_routing_gridOptions.dataSource.read();
        }
    }
    $scope.kpi_routing_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.KPI_Routing_List,
            readparam: function () {
                return {
                    contractID: $scope.ID, routingID: Common.HasValue($scope.RoutingItem) ? $scope.RoutingItem.ID : -1
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: 'Chọn', width: '50px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,kpi_routing_grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,kpi_routing_grid)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Zone', title: 'Zone', filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'LeadTime', title: 'Thời gian', filterable: { cell: { operator: 'eq', showOperators: false } }
            }
        ]
    }
    $scope.KPI_Routing_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true; });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.KPI_Routing_Apply,
                data: { data: data, routingID: $scope.RoutingItem.ID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật." });
                        win.close();
                        $scope.routing_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                    })
                }
            })
        } else {
            $rootScope.Message({ Msg: "Không có dữ liệu được chọn." });
            win.close();
        }
    }
    $scope.KPI_Check_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.KPIItemCheck.ID = item.ID;
        $scope.KPIItemCheck.KPICode = item.KPICode;
        $scope.KPIItemCheck.Expression = item.Expression;
        $scope.KPIItemCheck.CompareField = item.CompareField;
        $scope.KPIItemCheck.KPIID = item.KPIID;
        win.center().open();
    }
    $scope.KPI_Save_Click = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = [];
        data.push($scope.KPIItemCheck);
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.KPI_Save,
            data: { data: data, routingID: $scope.RoutingItem.ID },
            success: function (res) {
                var data = grid.dataSource.data();
                Common.Data.Each(data, function (o) {
                    if (o.ID == $scope.KPIItemCheck.ID && o.KPIID == $scope.KPIItemCheck.KPIID) {
                        o.Expression = $scope.KPIItemCheck.Expression;
                        o.CompareField = $scope.KPIItemCheck.CompareField;
                    }
                })
                grid.dataSource.sync();
                win.close();
                $rootScope.IsLoading = false;
            }
        });
    }
    $scope.KPI_Check_KPI_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.KPI_Check_KPI,
            data: {
                Expression: $scope.KPIItemCheck.Expression,
                Field: $scope.KPIItemCheck.CompareField,
                zone: $scope.KPIItemCheck.Zone,
                leadTime: $scope.KPIItemCheck.LeadTime,
                item: $scope.KPIItemCheck,
                lst: $scope.kpi_gridOptions.dataSource.data()
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: res
                    });
                })
            },
            error: function () {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Lỗi!"
                });
            }
        });
    }
    $scope.Routing_Delete_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_Delete,
            data: { ID: item.ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã xóa!" });
                    $scope.routing_gridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                })
            }
        });
    };
    $scope.KPI_Check_Expression_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.KPI_Check_Expression,
            data: {
                Expression: $scope.KPIItemCheck.Expression,
                zone: $scope.KPIItemCheck.Zone,
                leadTime: $scope.KPIItemCheck.LeadTime,
                item: $scope.KPIItemCheck,
                lst: $scope.kpi_gridOptions.dataSource.data()
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: Common.Date.FromJsonDMYHMS(res)
                    });
                })
            },
            error: function () {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Sai công thức."
                });
            }
        });
    }
    //#region addRouting
    $scope.Routing_Add_Click = function ($event, grid, win) {
        $event.preventDefault();

        $scope.routing_notin_gridOptions.dataSource.read();
        win.center().open();
    };

    $scope.routing_notin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_NotIn_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: 'Chọn', width: '120px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,routing_notin_grid)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,routing_notin_grid)" />' +
                    '<a href="/" ng-click="RoutingCUSEdit_Click($event,dataItem,RoutingAdd_win,CUSRoutingEdit_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="RoutingCUSDelete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EDistance', title: 'Khoảng cách (km)', width: 120, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'EHours', title: 'Thời gian (giờ)', width: 120, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.Routing_NotIn_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true; });
        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Routing_Insert,
                data: { data: data, contractID: $scope.ID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật." });
                        $scope.routing_gridOptions.dataSource.read();
                        win.close();
                    })
                }
            })
        } else {
            win.close();
        }
    };

    $scope.KPI_Routing_Add_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadCUSRoutingItem(0, win, vform)
    };

    $scope.RoutingCUSEdit_Click = function ($event, data, win, vform) {
        $event.preventDefault();
        $scope.LoadCUSRoutingItem(data.RoutingID, win, vform)
    }

    $scope.RoutingCUSDelete_Click = function ($event, data) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa ?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.ContractRouting_Delete,
                    data: { ID: data.ID, contractID: $scope.ID },
                    success: function (res) {
                        Common.Services.Error(res, function () {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $rootScope.IsLoading = false;
                            $scope.routing_notin_grid.dataSource.read();
                        })
                    }
                })
            }
        })
    };

    $scope.LoadCUSRoutingItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Get_Routing,
            data: { ID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.CUSRoutingItem = res;
                if (res.ID > 0) {
                    $scope.AreaLocationItem.LocationFrom = res.AreaFromName;
                    $scope.AreaLocationItem.LocationTo = res.AreaToName;
                    $scope.PointLocationItem.LocationFrom = res.LocationFromName;
                    $scope.PointLocationItem.LocationTo = res.LocationToName;
                }
                else {
                    $scope.AreaLocationItem.LocationFrom = "";
                    $scope.AreaLocationItem.LocationTo = "";
                    $scope.PointLocationItem.LocationFrom = "";
                    $scope.PointLocationItem.LocationTo = "";
                    $scope.CUSRoutingItem.LocationFromID = -1;
                    $scope.CUSRoutingItem.LocationToID = -1;
                    $scope.CUSRoutingItem.RoutingAreaFromID = -1;
                    $scope.CUSRoutingItem.RoutingAreaToID = -1;
                }
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.CUSRoutingAdd_win_numEDistanceOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, };

    $scope.CUSRoutingAdd_win_numEHoursOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.ChooseLocation_Click = function ($event, win, choice) {
        $event.preventDefault();
        $scope.isLocationFrom = choice;
        win.center();
        win.open();
        $scope.LocationNotIn_win_grid.refresh()
    }

    $scope.ChooseArea_Click = function ($event, win, choice) {
        $event.preventDefault();
        $scope.isLocationFrom = choice;
        win.center();
        win.open();
        $scope.AreaNotIn_win_grid.refresh();
    }

    //#region popup choose location
    $scope.LocationNotIn_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Read_LocationNotIn,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        editable: false, selectable: true,
        columns: [
            { field: 'Code', title: '{{RS.CUSRouting.Code}}', width: '115px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: '{{RS.CUSRouting.Location}}', width: '235px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: '{{RS.CUSRouting.Address}}', width: '500px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.LocationNotIn_RefreshClick = function ($event, win, grid) {
        $event.preventDefault()
    }

    $scope.LocationNotIn_AddChooseClick = function ($event, win, grid) {
        $event.preventDefault();
        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            if ($scope.isLocationFrom) {
                $scope.CUSRoutingItem.LocationFromID = item.ID;
                $scope.PointLocationItem.LocationFrom = item.Code + " - " + item.Location;
            }
            else {
                $scope.CUSRoutingItem.LocationToID = item.ID;
                $scope.PointLocationItem.LocationTo = item.Code + " - " + item.Location;
            }
            win.close();
        }
    }
    //#endregion

    //#region popup choose area
    $scope.AreaNotIn_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Read_AreaNotIn,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        selectable: 'row',
        //toolbar: kendo.template($('#AreaNotIn_win_gridtoolbar').html()),
        columns: [
            {
                title: ' ', width: '150px',
                template: '<a href="/" ng-click="AreaNotInEdit_Click($event,AreaEdit_winPopup,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="AreaNotInDestroy_Click($event,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-times"></i></a>' +
                    '<a href="/" ng-click="AreaNotInRefresh_Click($event,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-refresh"></i></a>' +
                    '<a href="/" ng-click="AreaNotInShowDetail_Click($event,AreaNotInDetail_win,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-info-circle"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CUSRouting.Code}}', width: '160px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaName', title: '{{RS.CUSRouting.AreaName}}', width: '450px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.AreaNotInEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.AreaLoadItem(win, id);
    }

    $scope.AreaNotIn_AddNewClick = function ($event, win) {
        $event.preventDefault();

        $scope.AreaLoadItem(win, 0)
    }

    $scope.AreaNotIn_AddChooseClick = function ($event, win, grid) {
        $event.preventDefault();

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            if ($scope.isLocationFrom) {
                $scope.CUSRoutingItem.RoutingAreaFromID = item.ID;
                $scope.AreaLocationItem.LocationFrom = item.Code + " - " + item.AreaName;
            }
            else {
                $scope.CUSRoutingItem.RoutingAreaToID = item.ID;
                $scope.AreaLocationItem.LocationTo = item.Code + " - " + item.AreaName;
            }
            win.close();
        }
    }

    $scope.AreaNotIn_win_CloseClick = function ($event, win) {
        $event.preventDefault()
        win.close()
    }

    $scope.AreaNotInShowDetail_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $scope.CurrentAreaID = item.ID;
            win.center();
            win.open();
            $scope.AreaNotInDetail_gridOptions.dataSource.read();
            $scope.AreaNotInDetail_grid.refresh();
        }
    }

    $scope.AreaNotInRefresh_Click = function ($event, grid) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item)) {

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn làm mới vị trí',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Refresh_RoutingArea,
                        data: { 'item': item },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.AreaNotInDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Delete_Area,
                    data: { 'item': item },
                    success: function (res) {
                        $scope.AreaNotIn_win_gridOptions.dataSource.read();
                    }
                });
            }
        }
    }

    $scope.AreaLoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Get_Area,
            data: { 'ID': id },
            success: function (res) {
                $scope.AreaEditItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.AreaEdit_winPopupSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn lưu dữ liệu đã chọn ?',
            Close: null,
            Ok: function () {
                if (vform()) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Save_Area,
                        data: { item: $scope.AreaEditItem },
                        success: function (res) {
                            $scope.AreaNotIn_win_gridOptions.dataSource.read();
                            win.close();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        }
                    });
                }
            }
        })
    }
    //#endregion 
    //#region popup detail area
    $scope.AreaNotInDetail_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Read_AreaDetail,
            readparam: function () { return { areaID: $scope.CurrentAreaID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="AreaNotInDetail_EditClick($event,AreaDetailEdit_winPopup,AreaNotInDetail_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="AreaNotInDetail_DestroyClick($event,AreaNotInDetail_grid)" class="k-button"><i class="fa fa-times"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'CountryName', title: '{{RS.CUSRouting.CountryName}}', width: '160px' },
            { field: 'ProvinceName', title: '{{RS.CUSRouting.ProvinceName}}', width: '160px' },
            { field: 'DistrictName', title: '{{RS.CUSRouting.DistrictName}}', width: '150px' }
        ]

    }

    $scope.AreaNotInDetail_EditClick = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.AreaDetailLoadItem(win, id);
    }

    $scope.AreaNotInDetail_DestroyClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item)) {

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Delete_AreaDetail,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.AreaNotInDetail_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.AreaNotInDetail_AddNewClick = function ($event, win) {
        $event.preventDefault();
        $scope.AreaDetailLoadItem(win, 0)
    }

    $scope.AreaNotInDetail_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.AreaNotInDetail_AddNewClick = function ($event, win) {
        $event.preventDefault();
        $scope.AreaDetailLoadItem(win, 0)
    }

    $scope.AreaEdit_winPopupSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn lưu dữ liệu đã chọn ?',
            Close: null,
            Ok: function () {
                if (vform()) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Save_Area,
                        data: { item: $scope.AreaEditItem },
                        success: function (res) {
                            $scope.AreaNotIn_win_gridOptions.dataSource.read();
                            win.close()
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        }
                    });
                }
            }
        })
    }

    $scope.AreaEdit_winPopupClose_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.AreaDetailLoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Get_AreaDetail,
            data: { 'ID': id },
            success: function (res) {
                $scope.AreaDetailItem = res;
                $scope.LoadRegionData($scope.AreaDetailItem);
                win.center();
                win.open();
            }
        });
    }

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _FLMSetting_Detail.Data.Province[countryID];
            $scope.AreaDetailEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _FLMSetting_Detail.Data.District[provinceID];
            $scope.AreaDetailEdit_win_cboDistrictOptions.dataSource.data(data);
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

    $scope.AreaDetailEdit_win_cboCountryOptions = {
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
                $scope.AreaDetailItem.ProvinceID = -1;
                $scope.AreaDetailItem.DistrictID = -1;
                $scope.AreaDetailItem.WardID = -1;
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.CountryID = "";
                $scope.AreaDetailItem.ProvinceID = "";
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            $scope.AreaDetailEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.AreaDetailEdit_win_cboProvinceOptions = {
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
                $scope.AreaDetailItem.DistrictID = -1;
                $scope.AreaDetailItem.WardID = -1;
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.ProvinceID = "";
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _FLMSetting_Detail.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_FLMSetting_Detail.Data.Province[obj.CountryID]))
                    _FLMSetting_Detail.Data.Province[obj.CountryID].push(obj);
                else _FLMSetting_Detail.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.AreaDetailEdit_win_cboDistrictOptions = {
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
                $scope.AreaDetailItem.WardID = "";
                // $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _FLMSetting_Detail.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_FLMSetting_Detail.Data.District[obj.ProvinceID]))
                    _FLMSetting_Detail.Data.District[obj.ProvinceID].push(obj);
                else _FLMSetting_Detail.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })

    $scope.AreaDetailEdit_winPopupSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Save_AreaDetail,
            data: { item: $scope.AreaDetailItem, areaID: $scope.CurrentAreaID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $scope.AreaNotInDetail_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                })
            }
        });
    }

    $scope.CUSRoutingEditSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            if ($scope.CUSRoutingItem.IsArea == true) {
                $scope.CUSRoutingItem.LocationFromID = null;
                $scope.CUSRoutingItem.LocationToID = null;
            }
            else {
                $scope.CUSRoutingItem.RoutingAreaFromID = null;
                $scope.CUSRoutingItem.RoutingAreaToID = null;
            }
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Save_Routing,
                data: { item: $scope.CUSRoutingItem, contractID: $scope.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.routing_notin_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                }
            });
        }
        else {
            $rootScope.Message({
                Msg: 'Chưa nhập đủ thông tin.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    }

    //#region area location
    $scope.CurrentAreaLocationID = -1;
    $scope.AreaLocationHasChoose = false;
    $scope.AreaLocationNotInHasChoose = false;

    $scope.AreaLocation_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.AreaLocation_List,
            readparam: function () { return { areaID: $scope.CurrentAreaLocationID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pagesize:20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,AreaLocation_win_grid,AreaLocation_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,AreaLocation_win_grid,AreaLocation_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'LocationName', title: 'Tên', width: '300px' },
            { field: 'LocationCode', title: 'Mã', width: '300px' },
            { field: 'Address', title: 'Địa chỉ', width: '350px' },
            { field: 'Country', title: 'Quốc gia', width: '150px' },
            { field: 'Province', title: 'Tỉnh thành', width: '150px' },
            { field: 'District', title: 'Quận huyện', width: '150px' }
        ]

    }

    $scope.AreaLocation_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.AreaLocationHasChoose = hasChoose;
    }

    $scope.AreaLocation_Click = function ($event, win, grid) {
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.CurrentAreaLocationID = id;
        $scope.AreaLocationHasChoose = false;
        $event.preventDefault();
        win.center().open();
        $scope.AreaLocation_win_gridOptions.dataSource.read();
        $scope.AreaLocation_win_grid.refresh();
    }

    $scope.AreaLocation_AddNew = function ($event, win) {
        $event.preventDefault();
        $scope.AreaLocationNotInHasChoose = false;
        win.center().open();
        $scope.AreaLocationNotIn_GridOptions.dataSource.read();
        $scope.AreaLocationNotIn_Grid.refresh();
    }

    $scope.AreaLocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.AreaLocation_NotInList,
            readparam: function () {
                return {
                    areaID: $scope.CurrentAreaLocationID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pagesize: 20,
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,AreaLocationNotIn_Grid,AreaLocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,AreaLocationNotIn_Grid,AreaLocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Location', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Code', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CountryName', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPartnerName', title: 'Loại partner', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    };

    $scope.AreaLocationFromNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.AreaLocation_NotInList,
            readparam: function () {
                return {
                    areaID: $scope.CurrentAreaLocationID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pagesize: 20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,AreaLocationFromNotIn_Grid,AreaLocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,AreaLocationFromNotIn_Grid,AreaLocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Location', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Code', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CountryName', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPartnerName', title: 'Loại partner', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.AreaLocationToNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.AreaLocation_NotInList,
            readparam: function () {
                return {
                    areaID: $scope.CurrentAreaLocationID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pagesize: 20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,AreaLocationToNotIn_Grid,AreaLocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,AreaLocationToNotIn_Grid,AreaLocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Location', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Code', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CountryName', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPartnerName', title: 'Loại partner', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.AreaLocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.AreaLocationNotInHasChoose = hasChoose;
    }

    $scope.AreaLocationNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.AreaLocation_NotInSave,
                data: { areaID: $scope.CurrentAreaLocationID, lstID: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocation_win_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.AreaLocation_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.AreaLocation_Delete,
                data: { lstID: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocation_win_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.AreaLocationAreaNotIn_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Read_AreaNotIn,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        selectable: 'row',
        columns: [
            { field: 'Code', title: '{{RS.CUSRouting.Code}}', width: '160px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaName', title: '{{RS.CUSRouting.AreaName}}', width: '450px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.AreaLocation_Copy = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.AreaLocationAreaNotIn_win_gridOptions.dataSource.read();
        $scope.AreaLocationAreaNotIn_win_grid.refresh();
    }

    $scope.AreaLocationAreaNotIn_AddChooseClick = function ($event, win, grid) {
        $event.preventDefault();
        var item = grid.dataItem(grid.select());
        var datasend = [];
        if (Common.HasValue(item))
            datasend.push(item.ID);
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.AreaLocation_Copy,
                data: { areaID: $scope.CurrentAreaLocationID, lstID: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocation_win_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }
    //#endregion
    //#endregion 
    //#endregion 

    //#endregion

    //#region Excel_Routing
    $scope.Routing_Excel_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'CATRoutingCode', width: 150, title: 'Mã hệ thống', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'CATRoutingName', width: 150, title: 'Tên hệ thống', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'ContractRoutingCode', width: 150, title: 'Mã cung đường', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'ContractRoutingName', width: 250, title: 'Tên cung đường', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'SortOrder', title: 'Số thứ tự', width: 80, filterable: { cell: { operator: 'equal', showOperators: false } } },
                { field: 'Zone', title: 'Zone', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                { field: 'LeadTime', title: 'LeadTime', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Routing_Excel_Export,
                    data: { contractID: $scope.ID },
                    success: function (res) {

                        $rootScope.DownloadFile(res);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Routing_Excel_Check,
                    data: { file: e.FilePath, contractID: $scope.ID, customerID: $scope.Item.SYSCustomerID },
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
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Routing_Excel_Import,
                    data: { data: data, contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        $scope.routing_gridOptions.dataSource.read();
                    }
                })
            }
        })
    };
    //#endregion

    //#region infoSetting

    $scope.SaveTypeOfRun = true;
    $scope.cboTypeOfRun_Option = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.SaveTypeOfRun = false;
        }
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfRunLevel,
        success: function (res) {
            $timeout(function () {
                var item = { ID: -1, ValueOfVar: 'Giá thường' };
                var data = [];
                data.push(item);
                Common.Data.Each(res, function (value) {
                    data.push(value);
                });
                $scope.cboTypeOfRun_Option.dataSource.data(data);
            }, 1);
        }
    });

    $scope.TypeOfRunLevelSave_Click = function ($event, vform) {
        $event.preventDefault();

        if (vform()) {
            if ($scope.Item.TypeOfRunLevelID != $scope.TypeOfRunIDBackup) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn có muốn thay đổi cách tính giá? (Các bảng giá cũ sẽ bị xóa)',
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMSetting_Detail.URL.TypeOfRunLevelSave,
                            data: { contractID: $scope.ID, typeID: $scope.Item.TypeOfRunLevelID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.SaveTypeOfRun = true;
                                $scope.TypeOfRunIDBackup = $scope.Item.TypeOfRunLevelID;
                                $scope.term_gridOptions.dataSource.read();
                                $scope.routing_gridOptions.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        });
                    },
                    Close: function () {
                        $scope.Item.TypeOfRunLevelID = $scope.TypeOfRunIDBackup;
                    }
                })
            }
        }
    }
    //#endregion

    //#region LTL level
    $scope.LTLLevelHasChoose = false;
    $scope.LTLLevel_Click = function ($event, win) {
        $event.preventDefault();
        $scope.price_level_win_GridOptions.dataSource.read()
        win.center().open();
    }

    $scope.price_level_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.LevelList,
            readparam: function () { return { contractID: $scope.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            },
            pagesize:20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_level_win_Grid,ltl_level_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_level_win_Grid,ltl_level_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: 100,
                template: '<a href="/" ng-click="price_level_EditClick($event,dataItem,price_level_winDetail,price_level_detai_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã bậc giá', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LevelName', title: 'Tên bậc giá', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: " ", filterable: false, sortable: false }
        ]
    }

    $scope.ltl_level_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LTLLevelHasChoose = hasChoose;
    }

    $scope.price_level_EditClick = function ($event, data, win, vform) {
        $event.preventDefault();
        $scope.price_level_LoadItem(data.ID, win, vform)
    }
    $scope.price_level_win_AddNewClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.price_level_LoadItem(0, win, vform)
    }
    $scope.price_level_LoadItem = function (id, win, vform) {
        $rootScope.IsLoading = true;

        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.LevelGet,
            data: { id: id },
            success: function (res) {

                $rootScope.IsLoading = false;
                $scope.PriceLevelItem = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.price_level_winDetail_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.LevelSave,
                data: { item: $scope.PriceLevelItem, contractID: $scope.ID, typeMode: $scope.Item.TypeOfMode },
                success: function (res) {
                    $scope.price_level_win_GridOptions.dataSource.read();
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

    $scope.PriceLevel_numTon_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.PriceLevel_numCBM_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.PriceLevel_numQuantity_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0 }

    $scope.ltl_level_Choose_DeleteClick = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.LevelDeleteList,
                data: { lst: datasend, contractID: $scope.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_level_win_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    $scope.numMinWeight_options = { format: '#', spinners: false, culture: 'en-US', min: 0, max: 100, step: 1, decimals: 1, }

    $scope.TypeOf_ladenempty_Click = function ($event) {
        $event.preventDefault();

        $.each(_FLMSetting_Detail.Data.ListSetting, function (i, v) {
            if (v.ServiceOfOrderID == $scope.ParamService.ServiceOfOrderID) {
                v.Laden = $scope.ParamService.Laden;
                v.GetEmpty = $scope.ParamService.GetEmpty;
                v.ReturnEmpty = $scope.ParamService.ReturnEmpty;
            }
        });

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.contractSetting_save,
            data: { contractID: $scope.ID, setting: _FLMSetting_Detail.Data.ListSetting },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    //#region FTL gov level
    $scope.GOVLevelHasChoose = false;
    $scope.GOVLevelItem = null;
    $scope.FTLLevel_Click = function ($event, win) {
        $event.preventDefault();
        $scope.gov_level_win_GridOptions.dataSource.read()
        win.center().open();
    }

    $scope.gov_level_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.LevelList,
            readparam: function () { return { contractID: $scope.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            },
            pagesize:20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gov_level_win_Grid,gov_level_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gov_level_win_Grid,gov_level_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: 45,
                template: '<a href="/" ng-click="gov_level_EditClick($event,dataItem,gov_level_winDetail,gov_level_detai_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã bậc giá', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LevelName', title: 'Tên bậc giá', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: " ", filterable: false, sortable: false }
        ]
    }

    $scope.gov_level_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOVLevelHasChoose = hasChoose;
    }

    $scope.gov_level_EditClick = function ($event, data, win, vform) {
        $event.preventDefault();
        $scope.gov_level_LoadItem(data.ID, win, vform)
    }
    $scope.gov_level_win_AddNewClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.gov_level_LoadItem(0, win, vform)
    }
    $scope.gov_level_LoadItem = function (id, win, vform) {
        $rootScope.IsLoading = true;
        $scope.LoadCboGOV();
        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.LevelGet,
            data: { id: id },
            success: function (res) {

                $rootScope.IsLoading = false;
                $scope.GOVLevelItem = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
    $scope.gov_level_winDetail_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.LevelSave,
                data: { item: $scope.GOVLevelItem, contractID: $scope.ID, typeMode: $scope.Item.TypeOfMode },
                success: function (res) {
                    $scope.gov_level_win_GridOptions.dataSource.read();
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
    $scope.GOVLevel_cboGOV_Options = {
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

    //Common.ALL.Get($http, {
    //    url: Common.ALL.URL.GroupOfVehicle,
    //    success: function (data) {
    //        $scope.GOVLevel_cboGOV_Options.dataSource.data(data);
    //    }
    //})

    $scope.LoadCboGOV = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.GOVListSetting,
            data: { contractID: $scope.ID },
            success: function (res) {
                $scope.GOVLevel_cboGOV_Options.dataSource.data(res);
            }
        });
    }

    $scope.gov_level_Choose_DeleteClick = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.LevelDeleteList,
                data: { lst: datasend, contractID: $scope.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.gov_level_win_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region Group Of Vehicle
    $scope.GOVHasChoose = false;
    $scope.FTLGOVList_Click = function ($event, win) {
        $event.preventDefault();
        $scope.gov_GridOptions.dataSource.read();
        $scope.gov_Grid.refresh();
        win.center().open();
    }
    $scope.gov_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.GOVList,
            readparam: function () { return { contractID: $scope.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                }
            },
            pagesize:20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gov_Grid,groupOfVehicle_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gov_Grid,groupOfVehicle_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="govEdit_Click($event,gov_detail_win,dataItem,gov_form)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfVehicleCode', title: 'Mã loại xe', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfVehicleName', title: 'Loại xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SortOrder', title: 'Thứ tự',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
        ]
    };

    $scope.groupOfVehicle_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOVHasChoose = hasChoose;
    }

    $scope.ItemGOV = null;
    $scope.govEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.GOVGet,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemGOV = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.GOV_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.GOVSave,
                data: { item: $scope.ItemGOV },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.gov_GridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.GOV_Choose_DeleteClick = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.GOVDeleteList,
                data: { lst: datasend, contractID: $scope.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.gov_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.GOV_ChooseNotIn_Click = function ($event, win) {
        $event.preventDefault();

        $scope.gov_NotIn_GridOptions.dataSource.read();
        win.center().open();
        $scope.gov_NotIn_Grid.refresh();
    }
    $scope.gov_NotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.GOVNotInList,
            readparam: function () { return { contractID: $scope.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                }
            },
            pagesize:20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gov_NotIn_Grid,groupOfVehicle_GridChooseNotIn_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gov_NotIn_Grid,groupOfVehicle_GridChooseNotIn_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã loại xe', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Tên loại xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.groupOfVehicle_GridChooseNotIn_Change = function ($event, grid, hasChoose) {
    }

    $scope.gov_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.GOVNotInSave,
                data: { contractID: $scope.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.gov_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        } else {
            $rootScope.Message({ Msg: 'Chưa chọn loại xe', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    //#endregion

    //#region term
    $scope.term_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Term_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DateEffect: { type: 'date' },
                    IsWarning: { type: 'boolean' },
                    IsClosed: { type: 'boolean' },
                    PriceContract: { type: 'number' },
                    PriceCurrent: { type: 'number' },
                    RateMaterial: { type: 'number' },
                    RatePrice: { type: 'number' },
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,

        columns: [
            {
                title: ' ', width: '200px',
                template: '<a href="/" ng-click="Term_Edit_Click($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-show="!dataItem.IsClosed" ng-click="Term_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsWarning && !dataItem.IsClosed" ng-click="Term_Material_Click($event,dataItem)" class="k-button"><i class="fa fa-file"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsWarning && !dataItem.IsClosed" ng-click="Term_RemoveWarning_Click($event,dataItem)" class="k-button"><i class="fa fa-ban"></i></a>' +
                    '<a href="/" ng-show="!dataItem.IsClosed" ng-click="Term_Lock_Click($event,dataItem)" class="k-button"><i class="fa fa-unlock"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsClosed" ng-click="Term_Unlock_Click($event,dataItem)" class="k-button"><i class="fa fa-lock"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã phụ lục', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TermName', title: 'Phụ lục', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PriceContract', width: 120, title: 'Giá nhiên liệu phụ lục', template: '#=PriceContract==null?" ":Common.Number.ToMoney(PriceContract)#',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'PriceCurrent', width: 120, title: 'Giá nhiên liệu mới', template: '#=PriceCurrent==null?" ":Common.Number.ToMoney(PriceCurrent)#',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'RateMaterial', width: 120, title: 'Tỷ lệ thay đổi giá n/l', template: '#=RateMaterial==null?" ":Common.Number.ToNumber2(RateMaterial)# %',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'RatePrice', width: 120, title: 'Tỷ lệ thay đổi giá chính', template: '#=RatePrice==null?" ":Common.Number.ToNumber2(RatePrice)# %',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateEffect', width: 120, title: 'Ngày hiệu lực', template: '#=Common.Date.FromJsonDMY(DateEffect)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateExpire', width: 120, title: 'Ngày hết hạn', template: '#=Common.Date.FromJsonDMY(DateExpire)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Phụ lục hợp đồng ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Partner_GridOptions.dataSource.read();
        }
    };

    $scope.Term_RemoveWarning_Click = function myfunction($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn có muốn hủy cảnh báo?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Term_RemoveWarning,
                        data: { ID: data.ID },
                        success: function (res) {
                            $scope.term_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    }

    $scope.Term_Lock_Click = function myfunction($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn khóa phụ lục đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Term_Lock,
                        data: { ID: data.ID },
                        success: function (res) {
                            $scope.term_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    }

    $scope.Term_Unlock_Click = function myfunction($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn mở khóa phụ lục đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Term_UnLock,
                        data: { ID: data.ID },
                        success: function (res) {
                            $scope.term_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    }

    //$scope.Term_Delete_Click = function myfunction($event, data) {
    //    $event.preventDefault();
    //    if (Common.HasValue(data)) {
    //        $rootScope.IsLoading = true;
    //        Common.Services.Call($http, {
    //            url: Common.Services.url.FLM,
    //            method: _FLMSetting_Detail.URL.Term_Delete,
    //            data: { ID: data.ID },
    //            success: function (res) {
    //                $scope.term_gridOptions.dataSource.read();
    //                $rootScope.IsLoading = false;
    //                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
    //            },
    //            error: function (res) {
    //                $rootScope.IsLoading = false;
    //            }
    //        });

    //    }
    //}

    $scope.Term_Delete_Click = function myfunction($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Term_Delete,
                        data: { ID: data.ID },
                        success: function (res) {
                            $scope.term_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            if (Common.HasValue($scope.CustomerID) && $scope.CustomerID > 0) {
                                $state.go('main.FLMSetting.Detail', { ID: $scope.ID, CustomerID: $scope.CustomerID }, { reload: true });
                            }
                            else {
                                $state.go('main.FLMSetting.Detail', { ID: $scope.ID, CustomerID: 0 }, { reload: true });
                            }
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    }
    $scope.Term_Add_Click = function myfunction($event) {
        $event.preventDefault();
        $scope.ParamEdit.TermID = 0;
        $scope.ParamEdit.ContractID = $scope.ID;
        //$scope.ParamEdit.CustomerID = $scope.Item.CustomerID;
        $state.go('main.FLMSetting.Term', $scope.ParamEdit);
    }

    $scope.Term_Edit_Click = function myfunction($event, data) {
        $event.preventDefault();
        var newParam = {
            TermID: data.ID,
            ContractID: $scope.ID
        }
        $state.go('main.FLMSetting.Term', newParam);
    }

    $scope.Term_Material_Click = function ($event, data) {
        $event.preventDefault();
        $state.go('main.CUSContract.Material', { TermID: data.ID });
    }

    //#endregion

    //#region price
    $scope.HasPriceChoose = false;

    $scope.price_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                    EffectDate: { type: 'date' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,

        dataBound: function () {
            if (Common.HasValue($scope.Item)) {
                $scope.Item.IsDisabled = false;
                if (this.dataSource.data().length > 0) {
                    debugger
                    $scope.Item.IsDisabled = true;
                }
            }
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_grid,price_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_grid,price_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '100px',
                template: '<a href="/" ng-click="Price_Edit_Click($event,dataItem,price_win,price_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Price_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'FeeName', title: 'Tên bảng giá', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfDriverFeeName', width: 150, title: 'Loại phí tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverFeeSumName', width: 150, title: 'Loại tính tổng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfDriverName', width: 150, title: 'Loại tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ListCustomerCode', width: 200, title: 'Mã khách',
                // filterable: { cell: { operator: 'contains', showOperators: false } },
                filterable: false
            },
            {
                title: '', filterable: false, sortable: false,
            }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Bảng giá tài xế ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.routing_gridOptions.dataSource.read();
        }
    }

    $scope.price_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasPriceChoose = hasChoose;
    }

    $scope.Price_Add_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.PriceLoadItem(0, win, vform)
    }

    $scope.Price_Edit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.PriceLoadItem(item.ID, win, vform)
    }

    $scope.PriceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPrice = res;
                vform({ clear: true })
                $scope.price_tabstrip.select(0);
                win.center().open();
                $scope.price_GroupLocation_GridOptions.dataSource.read();
                $scope.price_Location_GridOptions.dataSource.read();
                $scope.price_Route_GridOptions.dataSource.read();
                $scope.price_ParentRoute_GridOptions.dataSource.read();
                $scope.price_Customer_GridOptions.dataSource.read();
                $scope.price_GroupProduct_GridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Price_Delete_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Price_Delete,
                    data: { id: item.ID },
                    success: function (res) {
                        $scope.price_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.Price_win_Submit_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            var error = false;
            if (!Common.HasValue($scope.ItemPrice.DriverFeeSumID) || !$scope.ItemPrice.DriverFeeSumID > 0) {
                $rootScope.Message({ Msg: 'Chưa chọn loại tính tổng', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                error = true;
            }

            if (!error) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Price_Save,
                    data: { contractID: $scope.ID, item: $scope.ItemPrice },
                    success: function (res) {
                        $scope.price_gridOptions.dataSource.read();
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMSetting_Detail.URL.Price_Get,
                            data: { id: res },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.ItemPrice = res;
                                $scope.price_tabstrip.select(0);
                                $scope.price_GroupLocation_GridOptions.dataSource.read();
                                $scope.price_Location_GridOptions.dataSource.read();
                                $scope.price_Route_GridOptions.dataSource.read();
                                $scope.price_ParentRoute_GridOptions.dataSource.read();
                                $scope.price_Customer_GridOptions.dataSource.read();
                                $scope.price_GroupProduct_GridOptions.dataSource.read();
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        });
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.cboOrderTypeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfOrder,
        success: function (res) {
            if (res.length > 0) {
                $scope.cboOrderTypeOptions.dataSource.data(res);
            }
        }
    })
    //#endregion
    $scope.Update_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        $rootScope.IsLoading = false;
                        $state.go('main.FLMSetting.Detail', { ID: res });
                    })
                }
            });
        }
    }

    $scope.Delete_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa hợp đồng?',
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Delete,
                    data: { id: $scope.Item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            $state.go('main.FLMSetting.Index');
                        })
                    }
                });
            }
        })
    }

    //#region price
    $scope.price_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndexPrice = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndexPrice);
            }, 1)
        }
    }
    $scope.cboTypeOfDriverFeeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'TypeName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMSetting_Detail.URL.TypeOfDriverFee_List,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.cboTypeOfDriverFeeOptions.dataSource.data(res.Data);
            }
        }
    });

    $scope.cboTypeOfDriver = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMSetting_Detail.URL.TypeOfDriver,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                var item = { ID: -1, ValueOfVar: '' };
                res.Data.unshift(item);
                $scope.cboTypeOfDriver.dataSource.data(res.Data);
            }
        }
    });

    $scope.cboDriverFeeSumOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMSetting_Detail.URL.DriverFeeSum_List,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.cboDriverFeeSumOptions.dataSource.data(res.Data);
            }
        }
    });

    //#endregion
    //#region group location
    $scope.price_GroupLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_GroupLocation_List,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_GroupLocation_Grid,price_GroupLocation_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_GroupLocation_Grid,price_GroupLocation_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'GroupOfLocationCode', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfLocationName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_GroupLocation_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GLHasChoose = hasChoose;
    }

    $scope.price_GroupLocation_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_GroupNotIn_GridOptions.dataSource.read();
        $scope.price_GroupNotIn_Grid.resize();
    }

    $scope.price_GroupLocation_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_GroupLocation_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_GroupLocation_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_GroupNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_GroupLocation_NotInList,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_GroupNotIn_Grid,price_GroupNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_GroupNotIn_Grid,price_GroupNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã loại địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên loại địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_GroupNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GLNotInHasChoose = hasChoose;
    }

    $scope.price_GroupNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_GroupLocation_Save,
                data: { driverFeeID: $scope.ItemPrice.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_GroupLocation_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region route
    $scope.price_Route_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Route_List,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_Route_Grid,price_Route_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_Route_Grid,price_Route_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', title: "Ghi chú", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_Route_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteHasChoose = hasChoose;
    }

    $scope.price_Route_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_RouteNotIn_GridOptions.dataSource.read();
        $scope.price_RouteNotIn_Grid.resize();
    }

    $scope.price_RouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Route_NotInList,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_RouteNotIn_Grid,price_RouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_RouteNotIn_Grid,price_RouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', title: "Ghi chú", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_RouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteNotInHasChoose = hasChoose;
    }

    $scope.price_RouteNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_Route_Save,
                data: { driverFeeID: $scope.ItemPrice.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_Route_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_Route_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_Route_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_Route_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region  location

    $scope.price_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Location_List,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_Location_Grid,price_Location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_Location_Grid,price_Location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
             {
                 title: ' ', width: '90px',
                 template: '<a href="/" ng-click="price_Location_GridEdit_Click($event,price_Location_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                     '<a href="/" ng-click="price_Location_GridDestroy_Click($event,dataItem,price_Location_win)" class="k-button"><i class="fa fa-trash"></i></a>',
                 filterable: false, sortable: false
             },
            { field: 'LocationCode', title: "Mã địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: "Tên địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfTOLocationName', title: "Loại", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_Location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.price_Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_LocationNotIn_GridOptions.dataSource.read();
        $scope.price_LocationNotIn_Grid.resize();
    }

    $scope.price_Location_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_Location_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_LocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Location_NotInList,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_LocationNotIn_Grid,price_LocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_LocationNotIn_Grid,price_LocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: "Tên địa điểm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_LocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationNotInHasChoose = hasChoose;
    }

    $scope.price_LocationNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_Location_NotInSave,
                data: { driverFeeID: $scope.ItemPrice.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_Location_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_Location_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_Location_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_Location_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.price_Location_GridEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Location_Get,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPriceLocation = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.price_Location_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Location_Save,
            data: { item: $scope.ItemPriceLocation },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.price_Location_GridOptions.dataSource.read();
                win.close();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }


    $scope.cboTypeOfTOLocation_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfTOLocation,
        success: function (data) {
            var newdata = [];
            newdata.push({ ValueOfVar: " ", ID: -1 })
            Common.Data.Each(data, function (o) {
                newdata.push(o);
            })
            $scope.cboTypeOfTOLocation_Options.dataSource.data(newdata)
        }
    })


    //#endregion

    //#region parent route
    $scope.price_ParentRoute_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_ParentRoute_List,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ParentRoute_Grid,price_ParentRoute_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ParentRoute_Grid,price_ParentRoute_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ParentRoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ParentRoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_ParentRoute_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteHasChoose = hasChoose;
    }

    $scope.price_ParentRoute_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_ParentRouteNotIn_GridOptions.dataSource.read();
        $scope.price_ParentRouteNotIn_Grid.resize();
    }

    $scope.price_ParentRouteNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_ParentRoute_NotInList,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_ParentRouteNotIn_Grid,price_ParentRouteNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_ParentRouteNotIn_Grid,price_ParentRouteNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.price_ParentRouteNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ParentRouteNotInHasChoose = hasChoose;
    }

    $scope.price_ParentRouteNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_ParentRoute_Save,
                data: { driverFeeID: $scope.ItemPrice.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_ParentRoute_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_ParentRoute_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_ParentRoute_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_ParentRoute_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region customer
    $scope.price_Customer_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Customer_List,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_Customer_Grid,price_Customer_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_Customer_Grid,price_Customer_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'CustomerCode', title: "Mã khách hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_Customer_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerHasChoose = hasChoose;
    }

    $scope.price_Customer_AddNew = function ($event, win) {
        $event.preventDefault();
        $scope.price_Customer_GridOptions.dataSource.read();
        win.center();
        win.open();
        $scope.price_CustomerNotIn_Grid.resize();
    }

    $scope.price_Customer_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_Customer_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_Customer_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_CustomerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_Customer_NotInList,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, pagesize: 20,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_CustomerNotIn_Grid,price_CustomerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_CustomerNotIn_Grid,price_CustomerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã khách hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_CustomerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerNotInHasChoose = hasChoose;
    }

    $scope.price_CustomerNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_Customer_Save,
                data: { driverFeeID: $scope.ItemPrice.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_Customer_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region  GroupProduct

    $scope.price_GroupProduct_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_GroupProduct_List,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_GroupProduct_Grid,price_GroupProduct_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_GroupProduct_Grid,price_GroupProduct_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
             {
                 title: ' ', width: '90px',
                 template: '<a href="/" ng-click="price_GroupProduct_GridEdit_Click($event,price_GroupProduct_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                     '<a href="/" ng-click="price_GroupProduct_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                 filterable: false, sortable: false
             },
            { field: 'Code', title: "Mã nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: "Mã khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_GroupProduct_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupProductHasChoose = hasChoose;
    }

    $scope.price_GroupProduct_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.price_GroupProductNotIn_GridOptions.dataSource.read();
        $scope.price_GroupProductNotIn_Grid.resize();
    }

    $scope.price_GroupProduct_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_GroupProduct_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_GroupProduct_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_GroupProductNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_GroupProduct_NotInList,
            readparam: function () { return { driverFeeID: $scope.ItemPrice.ID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_GroupProductNotIn_Grid,price_GroupProductNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_GroupProductNotIn_Grid,price_GroupProductNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: "Mã khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.price_GroupProductNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupProductNotInHasChoose = hasChoose;
    }

    $scope.price_GroupProductNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_GroupProduct_NotInSave,
                data: { driverFeeID: $scope.ItemPrice.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_GroupProduct_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.price_GroupProduct_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var lst = [];
        if (Common.HasValue(data)) lst.push(data.ID);
        if (lst.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Price_GroupProduct_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.price_GroupProduct_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.price_GroupProduct_GridEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_GroupProduct_Get,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPriceGroupProduct = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.price_GroupProduct_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Price_GroupProduct_Save,
            data: { item: $scope.ItemPriceGroupProduct },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.price_GroupProduct_GridOptions.dataSource.read();
                win.close();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }


    //#endregion

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go('main.FLMSetting.Index');
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    //#region Excel
    $scope.Excel_Click = function ($event, win) {
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'TypeOfDriverCode', title: 'Mã loại tài xế', width: 120 },
                { field: 'TypeOfDriverName', title: 'Loại tài xế', width: 120 },
                { field: 'CustomerCode', title: 'Mã k.hàng', width: 120 },
                { field: 'CustomerName', title: 'Khách hàng', width: 120 },
                { field: 'GroupOfLocationCode', title: 'Mã loại điểm', width: 120 },
                { field: 'GroupOfLocationName', title: 'Loại điểm', width: 120 },
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Excel_Export,
                    data: { contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.Excel_Check,
                    data: { file: e, contractID: $scope.ID },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                var check = true;
                angular.forEach(data, function (v, i) {
                    if (!v.ExcelSuccess)
                        return check = false;
                }, 10);
                if (check) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Excel_Import,
                        data: { lst: data, contractID: $scope.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.price_grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                } else
                    $rootScope.Message({ Msg: 'Dữ liệu lỗi, không thể lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        })
    }

    $scope.ExcelTemp_Click = function ($event, win) {
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'TypeOfDriverCode', title: 'Mã loại tài xế', width: 120 },
                { field: 'TypeOfDriverName', title: 'Loại tài xế', width: 120 },
                { field: 'CustomerCode', title: 'Mã k.hàng', width: 120 },
                { field: 'CustomerName', title: 'Khách hàng', width: 120 },
                { field: 'GroupOfLocationCode', title: 'Mã loại điểm', width: 120 },
                { field: 'GroupOfLocationName', title: 'Loại điểm', width: 120 },
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.ExcelTemp_Export,
                    data: { contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.ExcelTemp_Check,
                    data: { file: e, contractID: $scope.ID },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                var check = true;
                angular.forEach(data, function (v, i) {
                    if (!v.ExcelSuccess)
                        return check = false;
                }, 10);
                if (check) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.ExcelTemp_Import,
                        data: { lst: data, contractID: $scope.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.price_grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                } else
                    $rootScope.Message({ Msg: 'Dữ liệu lỗi, không thể lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        })
    }
    //#endregion

    //#region FLMSettingVendor
    $scope.PriceCopy_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.PriceCopyRead,
            readparam: function () { },
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
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PriceCopy_Grid,PriceCopy_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'ContractNo', title: 'Số hợp đồng', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', title: 'Loại vận chuyển', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', title: 'Loại dịch vụ', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', title: 'Loại vận chuyển', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'EffectDate', width: '130px', title: 'Ngày hiệu lực', template: "#=EffectDate==null?' ':Common.Date.FromJsonDMY(EffectDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ExpiredDate', width: '130px', title: 'Ngày hết hạn', template: "#=ExpiredDate==null?' ':Common.Date.FromJsonDMY(ExpiredDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            }
        ]
    }

    $scope.PriceCopy_SaveList = function ($event, grid, win) {
        var data = grid.dataSource.data();
        var containerID = 0;
        $.each(data, function (i, v) {
            if (v.IsChoose) {
                containerID = v.ID;
            }
        });
        if (containerID > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.PriceCopySave,
                data: { ContractID: containerID, ID: $scope.ID },
                success: function (res) {
                    $scope.price_gridOptions.dataSource.read();
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $rootScope.IsLoading = false;
                    win.close();
                }
            })
        }
    }

    $scope.Price_Copy_Click = function ($event, win) {
        $event.preventDefault();
        $scope.PriceCopy_Grid.dataSource.read();
        win.center().open();
        $timeout(function () {
            $scope.PriceCopy_Grid.dataSource.read();
            $scope.PriceCopy_Grid.resize();
        }, 10)
    }

    $scope.cboCompanyOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerRelateName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerRelateName: { type: 'string' },
                }
            }
        })
    }

    $scope.loadCboCompany = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.CompanyUrlRead,
            data: { request: "" },
            success: function (res) {
                var data = [];
                if (Common.HasValue(res.Data)) {
                    $.each(res.Data, function (i, v) {
                        data.push({ ID: v.ID, CustomerRelateName: v.CustomerRelateName });
                    });
                }
                $scope.cboCompanyOptions.dataSource.data(data);
            }
        })
    }
    $scope.loadCboCompany();


    $scope.IsShowService = 26;
    $scope.cboServiceOfOrder_Option = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var valSys = -1;
            Common.Log("TransportMode:" + val);
            var cbx = this;
            if (e.sender.selectedIndex >= 0) {
                var object = cbx.dataItem(cbx.select());
                if (object != null) {
                    valSys = object.TypeOfVar;
                }
            }

            if (val != null && val != "") {
                $.each(_FLMSetting_Detail.Data.ListSetting, function (i, v) {
                    if (v.ServiceOfOrderID == val) {
                        $scope.ParamService.ServiceOfOrderID = v.ServiceOfOrderID;
                        $scope.ParamService.Laden = v.Laden;
                        $scope.ParamService.GetEmpty = v.GetEmpty;
                        $scope.ParamService.ReturnEmpty = v.ReturnEmpty;
                    }
                });

                if (valSys == 26) {
                    $scope.IsShowService = 26;
                }
                else {
                    if (valSys == 27) {
                        $scope.IsShowService = 27;
                    }
                    else {
                        $scope.IsShowService = 28;
                    }
                }
            }
        }
    };


    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (data) {
            $scope.cboServiceOfOrder_Option.dataSource.data(data);
        }
    })

    //#region GOP
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMSetting_Detail.URL.Data,
        data: { ID: $scope.ID },
        success: function (res) {
            _FLMSetting_Detail.Data.ListCustomer = res.ListCustomer;
            _FLMSetting_Detail.Data.ListCompany = {};
            _FLMSetting_Detail.Data.ListKPI = res.ListKPI;
            _FLMSetting_Detail.Data.ListGOP = res.ListGroupOfProduct;

            _FLMSetting_Detail.Data.ListGOPHasEmpty = [];
            _FLMSetting_Detail.Data.ListGOPHasEmpty.push({ ID: -1, Code: ' ', GroupName: ' ' });
            _FLMSetting_Detail.Data.ListProduct = {};
            var itemProEmpty = { ID: -1, Code: ' ', ProductName: ' ' };

            Common.Data.Each(_FLMSetting_Detail.Data.ListGOP, function (o) {
                _FLMSetting_Detail.Data.ListGOPHasEmpty.push(o);
                if (!Common.HasValue(_FLMSetting_Detail.Data.ListProduct[o.ID])) {
                    _FLMSetting_Detail.Data.ListProduct[o.ID] = [];
                    _FLMSetting_Detail.Data.ListProduct[o.ID].push(itemProEmpty);
                }
            })
            _FLMSetting_Detail.Data.ListProduct['-1'] = [];
            _FLMSetting_Detail.Data.ListProduct['-1'].push(itemProEmpty);
            $scope.gop_cbo_GroupOfProductChangeOptions.dataSource.data(_FLMSetting_Detail.Data.ListGOPHasEmpty);

            Common.Data.Each(res.ListProduct, function (o) {
                _FLMSetting_Detail.Data.ListProduct[o.GroupOfProductID].push(o);
            })

       
            $scope.gop_cbo_GroupOfProductOptions.dataSource.data(res.ListGroupOfProduct);

            Common.Data.Each(res.ListCustomer, function (o) {
                _FLMSetting_Detail.Data.ListCompany[o.ID] = [];
                _FLMSetting_Detail.Data.ListCompany[o.ID].push({ CustomerRelateName: ' ', ID: -1 })
            })
            Common.Data.Each(res.ListCompany, function (o) {
                _FLMSetting_Detail.Data.ListCompany[o.CustomerOwnID].push(o);
            })
            _FLMSetting_Detail.Data.ListMaterial = res.ListMaterial;
            //$scope.cboMaterial_Options.dataSource.data(_FLMSetting_Detail.Data.ListMaterial);
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Get,
                data: { ID: $scope.ID },
                success: function (res) {
                    $scope.Item = res;
                    _FLMSetting_Detail.Data.ListSetting = res.ListSetting;
                    $scope.ParamService.ServiceOfOrderID = res.ListSetting[0].ServiceOfOrderID;
                    $scope.ParamService.Laden = res.ListSetting[0].Laden;
                    $scope.ParamService.GetEmpty = res.ListSetting[0].GetEmpty;
                    $scope.ParamService.ReturnEmpty = res.ListSetting[0].ReturnEmpty;
                    if ($scope.Item.ID < 1) {
                        //if ($scope.CustomerID < 1) {
                        //    $scope.Item.IsDisabled = false;
                        //} else {
                        //    //$scope.Item.IsDisabled = true;
                        //    $scope.Item.CustomerID = $scope.CustomerID;
                        //}
                    } else {
                        _FLMSetting_Detail.Const.CustomerID = $scope.Item.CustomerID;
                        _FLMSetting_Detail.Const.CompanyID = $scope.Item.CompanyID;
                        //$scope.material_gridOptions.dataSource.read();
                    }
                    //$scope.LoadDataCompany(false);
                    $rootScope.IsLoading = false;
                    $scope.routing_gridOptions.dataSource.read();
                    $rootScope.Loading.Change("Thông tin chung ...", $rootScope.Loading.Progress + LoadingStep);
                    $scope.codefault_gridOptions.dataSource.read();
                }
            })
        }
    })

    $scope.gop_cbo_GroupOfProductOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
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
            var val = this.value();
            if (e.sender.selectedIndex >= 0) {
                $timeout(function () {
                    $scope.gop_cbo_ProductOptions.dataSource.data(_FLMSetting_Detail.Data.ListProduct[val]);

                    $scope.GOPItem.ProductID = -1;
                }, 1)
            }
        }
    };

    $scope.HasGOPChoose = false;

    $scope.gop_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.GroupOfProduct_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        //toolbar: kendo.template($('#gop_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="GOP_Edit_Click($event,gop_win,gop_grid,gop_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="GOP_Delete_Click($event,dataItem,gop_grid)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfProductName', title: 'Nhóm hàng', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductName', title: 'Hàng', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductIDChangeName', title: 'Nhóm hàng qui đổi', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductIDChangeName', title: 'Hàng qui đổi', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Expression', title: 'Công thức',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Quy đổi tấn khối số lượng ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.price_gridOptions.dataSource.read();
        }
    }

    $scope.gop_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasGOPChoose = hasChoose;
    }

    $scope.GOP_Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.GOP_LoadItem(win, -1, vform);
    };

    $scope.GOP_Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.GOP_LoadItem(win, item.ID, vform);
    };

    $scope.GOP_Delete_Click = function ($event, dataItem, grid) {
        $event.preventDefault();
        if (dataItem.ID > 0) {
            var lstid = [];
            lstid.push(dataItem.ID);
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.GroupOfProduct_Delete,
                data: { lstid: lstid },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã xóa' });
                    })
                }
            });
        }
    };

    $scope.GOP_LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.GroupOfProduct_Get,
            data: { id: id, contractID: $scope.ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.GOPItem = res;
                        $scope.gop_cbo_ProductOptions.dataSource.data(_FLMSetting_Detail.Data.ListProduct[$scope.GOPItem.GroupOfProductID]);
                    }, 1);
                }
            }
        });

        win.center();
        win.open();
    };

    $scope.gop_cbo_ProductOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ProductName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProductName: { type: 'string' },
                }
            }
        })
    };

    $scope.gop_cbo_GroupOfProductChangeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
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
            var val = this.value();
            if (e.sender.selectedIndex >= 0) {
                $timeout(function () {
                    var datacbo = [];
                    datacbo.push({ ID: -1, ProductName: "" });
                    if (Common.HasValue(_FLMSetting_Detail.Data.ListProduct[val])) {
                        $.each(_FLMSetting_Detail.Data.ListProduct[val], function (i, v) {
                            datacbo.push(v);
                        });
                    }
                    $scope.gop_cbo_ProductChangeOptions.dataSource.data(datacbo);
                    $scope.GOPItem.ProductIDChange = -1;
                }, 1)
            }
        }
    };

    $scope.gop_cbo_ProductChangeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ProductName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProductName: { type: 'string' },
                }
            }
        })
    };

    $scope.GOP_Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            if ($scope.GOPItem.ExpressionInput != "" && $scope.GOPItem.ExpressionInput != null) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.GroupOfProduct_Save,
                    data: { item: $scope.GOPItem, contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        win.close();
                        grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã cập nhật' });
                    }
                });
            }
        }
        else {
            $rootScope.Message({
                Msg: 'Công thức xét quy đổi không được rỗng.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    };

    $scope.GOP_Check_Expression_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.GroupOfProduct_Check,
                data: { item: $scope.GOPItem },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Alert, Msg: res
                        });
                    })
                }
            });
        }

    };

    $scope.ContractGroupOfProduct_Excel_Online = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_FLMSetting_Detail.ExcelKey.ResourceGroupOfProduct + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã nhóm hàng] không được trống và > 50 ký tự',
                '[Mã nhóm hàng] không tồn tại trong hệ thống',
                '[Mã mặt hàng] không tồn tại trong hệ thống',
                '[Mã nhóm hàng quy đổi] không tồn tại trong hệ thống',
                '[Mã mặt hàng quy đổi] không tồn tại trong hệ thống',
                '[Mã nhóm hàng quy đổi] không được trống',
                '[Công thức xét quy đổi] không được trống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMSetting_Detail.ExcelKey.GroupOfProduct,
            params: { contractID: $scope.ID },
            rowStart: 2,
            colCheckChange: 15,
            url: Common.Services.url.FLM,
            methodInit: _FLMSetting_Detail.URL.GroupOfProduct_ExcelInit,
            methodChange: _FLMSetting_Detail.URL.GroupOfProduct_ExcelChange,
            methodImport: _FLMSetting_Detail.URL.GroupOfProduct_ExcelImport,
            methodApprove: _FLMSetting_Detail.URL.GroupOfProduct_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.gop_gridOptions.dataSource.read();
            }
        });
    };
    //#endregion
    //#endregion

    //#region TypeOfSGroupProduct
    $scope.cboTypeOfSGroupProduct_Option = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfSGroupProductChange,
        success: function (res) {
            $timeout(function () {
                $scope.cboTypeOfSGroupProduct_Option.dataSource.data(res);
            }, 1);
        }
    });

    $scope.TypeOfSGroupProduct_Click = function ($event, vform) {
        $event.preventDefault();

        if (vform()) {
            if ($scope.Item.TypeOfSGroupProductChangeID != $scope.TypeOfRunIDBackup) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.TypeOfSGroupProductChangeSave,
                    data: { contractID: $scope.ID, typeID: $scope.Item.TypeOfSGroupProductChangeID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.TypeOf_ladenempty_Click = function ($event) {
        $event.preventDefault();
        $.each(_FLMSetting_Detail.Data.ListSetting, function (i, v) {
            if (v.ServiceOfOrderID == $scope.ParamService.ServiceOfOrderID) {
                v.Laden = $scope.ParamService.Laden;
                v.GetEmpty = $scope.ParamService.GetEmpty;
                v.ReturnEmpty = $scope.ParamService.ReturnEmpty;
            }
        });

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.contractSetting_save,
            data: { contractID: $scope.ID, setting: _FLMSetting_Detail.Data.ListSetting },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
    //#endregion

    //#region tab danh muc địa chỉ
    $scope.Partner_CountryCbbOptions = {}; $scope.Partner_ProvinceCbbOptions = {}; $scope.Partner_DistrictCbbOptions = {};
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
    $scope.CreateCBB($scope.Partner_CountryCbbOptions, $scope.Partner_ProvinceCbbOptions, $scope.Partner_DistrictCbbOptions, 'Partner_CountryCbb', 'Partner_ProvinceCbb', 'Partner_DistrictCbb', _CUSDetail.Obj);

    $scope.TabIndexPartner = 1;
    $scope.Partner_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndexPartner = angular.element(e.item).data('tabindex'); //or
                Common.Log("TabIndexPartner:" + $scope.TabIndexPartner)
            }, 1);
        }                                                                      
    };

    $scope.Partner_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeID: { type: 'number' },
                    TypeName: { type: 'string', editable: false },
                    CATName: { type: 'string', editable: false },
                    CATAddress: { type: 'string', editable: false },
                    ProvinceName: { type: 'string', editable: false },
                    DistrictName: { type: 'string', editable: false },
                    CATCode: { type: 'string', editable: false },
                    CUSCode: { type: 'string', editable: true },
                    CUSLocationName: { type: 'string', editable: true },
                    CATPartnerID: { type: 'number' },
                    CUSPartnerID: { type: 'number' },
                    CATLocationID: { type: 'number' },
                    CUSLocationID: { type: 'number' },
                    CUSLocationID: { type: 'number' },
                    F_command: { type: 'string', editable: false },
                    F_empty: { type: 'string', editable: false },
                    IsPartner: { type: 'boolean' },
                }
            },
            pageSize: 100
        }),
        toolbar: kendo.template($('#partner-grid-toolbar').html()),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell',
        edit: function (e) {
            var grid = this;

            if (e.model.IsPartner) {
                grid.closeCell();
            }
        },
        save: function (e) {
            var grid = this, fieldEdit = '', valueEdit = '';
            var flagSave = -1;//1 change code, 2 change name

            for (f in e.values) { fieldEdit = f; } valueEdit = e.values[fieldEdit];

            if (fieldEdit == "CUSCode") {
                if (valueEdit != "") {
                    if (e.model.CUSLocationName == "" || !Common.HasValue(e.model.CUSLocationName)) {
                        e.model.CUSLocationName = valueEdit;
                    }
                }
                flagSave = 1;
            }

            if (fieldEdit == "CUSLocationName") {
                if (e.model.CUSCode == "" || !Common.HasValue(e.model.CUSCode)) {
                    flagSave = -1;
                }
                else
                    flagSave = 2;
            }

            $timeout(function () {

                if (flagSave > 0) {
                    var itemSend = e.model;

                    switch (flagSave) {
                        default:
                            break;
                        case -1: break;
                        case 1://luu khi change code
                            if (itemSend.CUSCode == "") {
                                $rootScope.Message({
                                    Type: Common.Message.Type.Confirm,
                                    NotifyType: Common.Message.NotifyType.SUCCESS,
                                    Title: 'Thông báo',
                                    Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                                    Close: function () {
                                        grid.cancelChanges();
                                    },
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.FLM,
                                            method: _FLMSetting_Detail.URL.Partner_CUSLocationSaveCode,
                                            data: { item: itemSend },
                                            success: function (res) {
                                                $scope.LoadDataPartner('Đã cập nhật');
                                            },
                                            error: function (res) {
                                                grid.cancelChanges();
                                                $rootScope.IsLoading = false;
                                            }
                                        });
                                    }
                                })
                            }
                            else {
                                if (!Common.HasValue(itemSend.CUSLocationName) || itemSend.CUSLocationName == "") {
                                    $rootScope.Message({ Msg: 'Thiếu tên sử dụng', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                                }
                                else {
                                    $rootScope.IsLoading = true;
                                    Common.Services.Call($http, {
                                        url: Common.Services.url.FLM,
                                        method: _FLMSetting_Detail.URL.Partner_CUSLocationSaveCode,
                                        data: { item: itemSend },
                                        success: function (res) {
                                            $scope.LoadDataPartner('Đã cập nhật');
                                        },
                                        error: function (res) {
                                            grid.cancelChanges();
                                            $rootScope.IsLoading = false;
                                        }
                                    });
                                }
                            }
                            break;
                        case 2:// luu khi change name
                            if (!Common.HasValue(itemSend.CUSLocationName) || itemSend.CUSLocationName == "") {
                                $rootScope.Message({ Msg: 'Tên sử dụng không trống', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                            }
                            else {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.FLM,
                                    method: _FLMSetting_Detail.URL.Partner_CUSLocationSaveCode,
                                    data: { item: itemSend },
                                    success: function (res) {
                                        $scope.LoadDataPartner('Đã cập nhật');
                                    },
                                    error: function (res) {
                                        grid.cancelChanges();
                                        $rootScope.IsLoading = false;
                                    }
                                });
                            }
                            break;
                    }
                }
            }, 1)

        },
        columns: [
            {
                title: ' ', width: '45px', field: 'F_command',
                template: '<a href="/" ng-click="Partner_EditClick($event,dataItem,Partner_win,Stock_win)" ng-show="Auth.ActEdit&&dataItem.IsPartner" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Partner_RoutingClick($event,dataItem,routing_contract_win,routing_contract_grid)" ng-show="Auth.ActEdit&&!dataItem.IsPartner" class="k-button"><i class="fa fa-random"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'TypeName', title: ' ', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATName', title: 'Tên hệ thống', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATAddress', title: 'Điạ chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh / thành phố', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận / huyện', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATCode', title: 'Mã hệ thống', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSCode', title: 'Mã sử dụng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSLocationName', title: 'Tên sử dụng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', field: 'F_empty', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Danh sach địa chỉ...", $rootScope.Loading.Progress + LoadingStep);
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    }

    $scope.ExcelPartnerMenuOptions = {
        openOnClick: true,
        direction: "left"
    }

    $scope.AddPartnerMenuOptions = {
        openOnClick: true,
        direction: "left"
    }

    $scope.SearchPartnerMenuOptions = {
        openOnClick: true,
        direction: "left"
    }

    $scope.Partner_RoutingClick = function ($event, data, win, grid) {
        $event.preventDefault();

        $scope.PartnerRouting_LocationID = data.CATLocationID;
        win.center();
        win.open();
        grid.dataSource.read();
    };

    //thông tin partner trên popup
    $scope.Partner_EditClick = function ($event, data, partnerWin, stockWin) {
        $event.preventDefault();

        if (data.TypeID == 1 || data.TypeID == 2 || data.TypeID == 3) {

            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Partner_Get,
                data: { id: data.CUSPartnerID, typepartnerid: -1 },
                success: function (res) {
                    $scope.ItemPartner = res;
                    $scope.TabIndexPartner = 1;
                    $scope.PartnerLocation_GridOptions.dataSource.read();
                    $scope.Partner_Tab.select(0);
                    $rootScope.IsLoading = false;
                    partnerWin.center();
                    partnerWin.open();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
        else if (data.TypeID == 4) {
            stockWin.center();
            stockWin.open();
            $scope.Stock_GridOptions.dataSource.read();
        }
    };

    $scope.Partner_Add = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Partner_Get,
            data: { id: 0, typepartnerid: -1 },
            success: function (res) {
                $scope.ItemPartner = res;
                $scope.TabIndexPartner = 1;
                $scope.Partner_Tab.select(0);
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.LoadDataPartner = function (mess, win) {

        $rootScope.IsLoading = true;
        var lstCUSPartnerID = [];
        var lstCUSLocationID = [];
        var isUseLocation = $scope.IsFilterUseLocation;
        if ($scope.IsFilterPartner == true)
            lstCUSPartnerID = $scope.ListFilterPartnerIDChoose;
        if ($scope.IsFilterLocation == true)
            lstCUSLocationID = $scope.ListFilterCusLocationIDChoose;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Partner_List,
            data: {
                lstPartner: lstCUSPartnerID,
                lstLocation: lstCUSLocationID,
                isUseLocation: isUseLocation
            },
            success: function (res) {
                $scope.Partner_GridOptions.dataSource.data(res)
                $rootScope.IsLoading = false;
                if (Common.HasValue(mess) && mess != '') {
                    $rootScope.Message({ Msg: mess, Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                }
                if (Common.HasValue(win) && win != '')
                    win.close();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.cboType_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data: [
                { Value: 1, Text: 'Cảng biển' },
                { Value: 2, Text: 'Hãng tàu' },
                { Value: 3, Text: 'Nhà phân phối' },
               { Value: 4, Text: 'Kho' },
            ],
            model: {
                id: 'Value',
                fields: {
                    Value: { type: 'number' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                if (cbo.value() < 4) {
                    var data = cbo.dataItem(cbo.select());
                    if (Common.HasValue(data)) {
                        $scope.ItemPartner.TypeName = data.Text;
                    }
                }
                else {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.StockGet,
                        data: { id: $scope.id, stockID: 0 },
                        success: function (res) {
                            $scope.Partner_win.close();
                            $scope.StockTypeID = 4;
                            $scope.StockData = res;
                            $scope.Stock_Tab.select(0);
                            $scope.TabIndexStock = 1;
                            $scope.Stock_ProductGrid.dataSource.read();

                            $scope.Stock_EditWin.center();
                            $scope.Stock_EditWin.open();
                            $rootScope.IsLoading = false;
                        },
                        error: function () {
                            $rootScope.IsLoading = false;
                        }
                    })

                }
            }
        }
    }


    $scope.PartnerType_CbbOptions = {
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
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfPartner,
        success: function (res) {
            res.splice(0, 0, { ID: -1, GroupName: " ", Code: " " });
            $scope.PartnerType_CbbOptions.dataSource.data(res);
        }
    });

    $scope.Partner_Save = function ($event, vform, win) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemPartner.TypeID != 1 && $scope.ItemPartner.TypeID != 2 && $scope.ItemPartner.TypeID != 3) {
                $rootScope.Message({ Msg: 'Loại đối tác không chính xác', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.PartnerUrlUpdate,
                    data: { item: $scope.ItemPartner, typePartner: $scope.ItemPartner.TypeID },
                    success: function (res) {
                        $scope.LoadDataPartner('');

                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMSetting_Detail.URL.Partner_Get,
                            data: { id: res, typepartnerid: -1 },
                            success: function (res) {

                                $scope.ItemPartner = res;
                                $scope.TabIndexPartner = 1;
                                $scope.Partner_Tab.select(0);
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        });
                    }
                })
            }
        }
    }

    $scope.Partner_Delete = function ($event, win) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: $scope.ItemPartner },
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.PartnerUrlDestroy,
                    data: { cuspartnerid: $scope.ItemPartner.ID },
                    success: function (res) {
                        $scope.LoadDataPartner('');
                        win.close();
                        $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        });


    }

    $scope.Partner_ExcelOnl = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_FLMSetting_Detail.ExcelKey.Partner_and_Stock + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Phân loại] không được trống và > 50 ký tự',
                '[Phân loại] không tồn tại',
                '[Mã hệ thống đối tác] không được trống và > 50 ký tự',
                '[Mã hệ thống đối tác] đã bị trùng',
                '[Mã đối tác] không được trống và > 50 ký tự',
                '[Mã đối tác] đã bị trùng',
                '[Tên đối tác] không được trống và > 1000 ký tự',
                '[Loại đối tác / địa chỉ] không tồn tại',
                '[Mã hệ thống địa chỉ] đã bị trùng',
                '[Mã địa chỉ] đã bị trùng',
                '[Tên địa chỉ] không được trống và > 500 ký tự',
                '[Địa chỉ] không được trống và > 500 ký tự',
                '[Tỉnh thành] không được trống',
                '[Tỉnh thành] không tồn tại',
                '[Quận huyện] không được trống',
                '[Quận huyện] không tồn tại',
                '[Điện thoại] không được > 50 ký tự',
                '[Fax] không được > 50 ký tự',
                '[Email] không được > 50 ký tự',
                '[Khu công nhiệp] không được > 500 ký tự',
                '[Mã khu vực] không được > 1000 ký tự',
                '[Kinh độ] nhập sai',
                '[Vĩ độ] nhập sai',
                '[Thời hạn lấy rỗng] nhập sai',
                '[Thời hạn trả rỗng] nhập sai',
                '[T/g b.hành (tháng)] nhập sai',
                '[Ngày kết thúc BH] nhập sai',
                '[Thông số kỹ thuật] nhập sai'
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMSetting_Detail.ExcelKey.Partner_and_Stock,
            params: { },
            rowStart: 1,
            colCheckChange: 30,
            url: Common.Services.url.FLM,
            methodInit: _FLMSetting_Detail.URL.Partner_ExcelInit,
            methodChange: _FLMSetting_Detail.URL.Partner_ExcelChange,
            methodImport: _FLMSetting_Detail.URL.Partner_ExcelImport,
            methodApprove: _FLMSetting_Detail.URL.Partner_ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.LoadDataPartner('');
            }
        });

    }

    $scope.Partner_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.PartnerNotInUrlRead,
            readparam: function () { return { typePartner: $scope.typeOfPartnerNotin } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', editable: false },
                    CATCode: { type: 'string', editable: false },
                    CATName: { type: 'string', editable: false }
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Partner_NotinGrid,Partner_NotinGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Partner_NotinGrid,Partner_NotinGridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'PartnerCode', title: 'Mã', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CATCode', title: 'Mã hệ thống', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CATName', title: 'Tên', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },

        ]
    };

    $scope.Partner_Search = function ($event, type, win, grid) {
        $event.preventDefault();
        $scope.typeOfPartnerNotin = type;
        win.center();
        win.open();
        grid.dataSource.read();

    };

    $scope.Partner_NotinGridChange = function ($event, grid, haschoose) {
        $scope.PartnerHasChoose = haschoose;
    };

    $scope.Partner_NotinSaveList = function ($event, grid, win) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.PartnerUrlSaveList,
                data: { lst: data, id: $scope.id },
                success: function (res) {

                    $scope.LoadDataPartner('');
                    $rootScope.IsLoading = false;
                    win.close();

                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.numRateGetEmpty_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }
    $scope.numRateReturnEmpty_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }

    //thông tin location tren popup

    $scope.PartnerLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.PartnerLocationRead,
            readparam: function () { return { partnerID: $scope.ItemPartner.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Code: { type: 'string' },
                    LocationName: { type: 'string' },
                    ProvinceName: { type: 'string', editable: false },
                    DistrictName: { type: 'string', editable: false },
                    WardName: { type: 'string', editable: false },
                    Address: { type: 'string', editable: false },
                    Lat: { type: 'number', editable: false },
                    Lng: { type: 'number', editable: false },
                    CATLocationCode: { type: 'string', editable: false },
                    CATLocationName: { type: 'string', editable: false },
                    GroupOfLocationName: { type: 'string', editable: false },
                }
            },
            pageSize: 100,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '120px', attributes: { style: "text-align: center" }, filterable: false, sortable: false,
                template: '<a href="/" ng-click="Location_Edit($event,dataItem,Location_EditWin)" ng-show="dataItem.IsEditable&&Auth.ActEdit" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Location_Delete($event,dataItem)" ng-show="dataItem.IsEditable&&Auth.ActDel" class="k-button"><i class="fa fa-trash"></i></a>' +
                    ' <a href="/" ng-click="Location_ChooseArea($event,dataItem,Area_win)" class="k-button"><i class="fa fa-info-circle"></i></a>'

            },
            {
                field: 'CATLocationCode', title: 'Mã hệ thống', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CATLocationName', title: 'Tên hệ thống', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Loại địa điểm', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Code', title: 'Mã', width: '100px', attributes: { style: "background-color: yellowgreen;" },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên', width: '125px', attributes: { style: "background-color: yellowgreen;" },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh thành', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Quận huyện', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lng', title: 'Kinh độ', width: '100px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'Lat', title: 'Vĩ độ', width: '100px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Location_Add = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.PartnerlocationUrlGet,
            data: { locationID: 0 },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.LocationData = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }
    $scope.Location_Edit = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.PartnerlocationUrlGet,
            data: { locationID: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.LocationData = res;
                win.center().open();
            }
        })
    }

    $scope.Location_Save = function ($event, vform, win, winroute, gridroute) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.PartnerlocationUrlUpdate,
                data: { item: $scope.LocationData, cuspartnerID: $scope.ItemPartner.ID },
                success: function (res) {
                    $scope.LoadDataPartner("");
                    $scope.PartnerLocation_GridOptions.dataSource.read();
                    debugger
                    if ($scope.LocationData.LocationID != res) {
                        $scope.PartnerRouting_LocationID = res;
                        winroute.center();
                        winroute.open();
                        gridroute.dataSource.read();
                    }
                    $rootScope.IsLoading = false;
                    win.close();
                },
                error: function () {
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.Location_Delete = function ($event, data) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.PartnerlocationUrlDestroy,
                    data: { item: data },
                    success: function (res) {
                        $scope.LoadDataPartner("");
                        $scope.PartnerLocation_GridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã xóa', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });


                    },
                    error: function () {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        });
    }

    $scope.Location_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: "PartnerLocation_NotInList",
            readparam: function () { return { partnerID: $scope.ItemPartner.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', editable: false },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Location_NotinGrid,Location_NotinGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Location_NotinGrid,Location_NotinGridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'CATLocationCode', title: 'Mã hệ thống', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CATLocationName', title: 'Tên hệ thống', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Loại địa điểm', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Code', title: 'Mã', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh thành', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Quận huyện', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.Location_NotinGridChange = function ($event, grid, haschoose) {
        $scope.LocationSearchHasChoose = haschoose;
    };


    $scope.Location_Search = function ($event, win, grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.Location_NotinSaveList = function ($event, grid, win) {
        $event.preventDefault();
        var data = [];
        angular.forEach(grid.dataSource.data(), function (o, i) {
            if (o.IsChoose) {
                data.push(o.LocationID);
            }
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: "PartnerLocation_SaveNotinList",
                data: { lst: data, partnerID: $scope.ItemPartner.ID },
                success: function (res) {

                    $scope.PartnerLocation_GridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.LocationSearchHasChoose = false;
                }
            })
        }
    }

    //#region kho
    $scope.Stock_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.StockRead,
            readparam: function () { return { id: $scope.id } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    CountryName: { type: 'string' },
                    ProvinceName: { type: 'string' },
                    DistrictName: { type: 'string' },
                    WardName: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: true, pageSize: 20, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-click="Stock_Edit($event,dataItem,Stock_EditWin)" ng-show="Auth.ActEdit" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    ' <a href="/" ng-click="Stock_Delete($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>' +
                    ' <a href="/" ng-click="Location_ChooseArea($event,dataItem,Area_win)" class="k-button"><i class="fa fa-info-circle"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'CATCode', title: 'Mã hệ thống', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CATLocationName', title: 'Tên hệ thống', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Code', title: 'Mã sử dụng', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên sử dụng', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: 'Quốc gia', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh thành', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.Stock_Edit = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.StockGet,
            data: { id: $scope.id, stockID: data.ID },
            success: function (res) {
                $scope.StockData = res;
                $scope.Stock_Tab.select(0);
                $scope.TabIndexStock = 1;
                $scope.Stock_ProductGrid.dataSource.read();
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            },
            error: function () {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.Stock_Save = function ($event, vform, winroute, gridroute) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.StockUpdate,
                data: { item: $scope.StockData, id: $scope.id },
                success: function (res) {
                    if (res.ID != $scope.StockData.ID) {
                        $scope.StockData = res;
                        $scope.PartnerRouting_LocationID = res.LocationID;
                        winroute.center();
                        winroute.open();
                        gridroute.dataSource.read();
                    }
                    $scope.Stock_Grid.dataSource.read();
                    $scope.LoadDataPartner('');
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function () {
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.Stock_Delete = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_Detail.URL.StockDestroy,
                    data: { item: data },
                    success: function (res) {
                        $scope.Stock_Grid.dataSource.read();
                        $scope.LoadDataPartner('');
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        });
    }

    $scope.Stock_Add = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.StockGet,
            data: { id: $scope.id, stockID: 0 },
            success: function (res) {
                $scope.StockData = res;
                $scope.Stock_Tab.select(0);
                $scope.TabIndexStock = 1;
                $scope.Stock_ProductGrid.dataSource.read();
                win.center().open();
                $rootScope.IsLoading = false;
            },
            error: function () {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.cboStockType_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data: [
                { Value: 1, Text: 'Cảng biển' },
                { Value: 2, Text: 'Hãng tàu' },
                { Value: 3, Text: 'Nhà phân phối' },
               { Value: 4, Text: 'Kho' },
            ],
            model: {
                id: 'Value',
                fields: {
                    Value: { type: 'number' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            var val = cbo.value();
            if (e.sender.selectedIndex >= 0) {
                if (val < 4) {
                    var data = cbo.dataItem(cbo.select());
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.Partner_Get,
                        data: { id: 0, typepartnerid: val },
                        success: function (res) {
                            $scope.Stock_EditWin.close();
                            $scope.ItemPartner = res;
                            $scope.TabIndexPartner = 1;
                            $scope.Partner_Tab.select(0);

                            $scope.Partner_win.center();
                            $scope.Partner_win.open();
                            $rootScope.IsLoading = false;
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            }
        }
    }

    $scope.Stock_Search = function ($event, win, grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    }

    $scope.StockNotin_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.StockNotInRead,
            readparam: function () { return { id: $scope.id } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', editable: false },
                    Address: { type: 'string', editable: false },
                    StockCode: { type: 'string', editable: false },
                    StockName: { type: 'string', editable: false }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,StockNotin_Grid,StockNotin_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,StockNotin_Grid,StockNotin_GridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockCode', title: 'Mã kho', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockName', title: 'Tên kho', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: 'Địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.StockNotin_GridChange = function ($event, grid, haschoose) {
        $scope.StockSearchHasChoose = haschoose;
    }

    $scope.StockNotIn_SaveList = function ($event, grid, win) {
        $event.preventDefault();
        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.StockSaveList,
                data: { lst: data, id: $scope.id },
                success: function (res) {
                    $scope.Stock_Grid.dataSource.read();
                    $scope.LoadDataPartner('');
                    win.close();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (es) {
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.StockDetailSearch_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.RoutingAreaNotInList,
            readparam: function () { return { locationID: $scope.locationID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
            pageSize: 100,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
             {
                 title: ' ', width: '30px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,StockDetailSearch_Grid,StockSearchChooseChange)" />',
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,StockDetailSearch_Grid,StockSearchChooseChange)" />',
                 filterable: false, sortable: false
             },
            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'AreaName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.StockSearchChooseChange = function ($event, grid, haschoose) {
        $scope.HasChooseSearch = haschoose;
    };

    $scope.Stock_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndexStock = angular.element(e.item).data('tabindex'); //or
                Common.Log("TabIndexStock:" + $scope.TabIndexStock)
            }, 1);
        }
    };

    $scope.Stock_ProductGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.StockProduct_Read,
            readparam: function () { return { stockID: $scope.StockData.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', editable: false }
                }
            }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: false,
        columns: [
            {
                title: ' ', width: '30px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Stock_ProductGrid,Stock_ProductGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Stock_ProductGrid,Stock_ProductGridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfProductCode', title: 'Mã nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductName', title: 'Nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };
    $scope.Stock_ProductGridChange = function ($event, grid, haschoose) {
        $scope.StockProductHasChoose = haschoose;
    };

    $scope.Stock_Product_Delete = function ($event, grid) {
        $event.preventDefault();
        var lstID = [];
        var dataSource = grid.dataSource.data();
        for (var i = 0; i < dataSource.length; i++) {
            if (dataSource[i].IsChoose) {
                lstID.push(dataSource[i].GroupOfProductID);
            }
        }
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.StockProduct_Delete,
                        data: { lstGroupID: lstID, stockID: $scope.StockData.ID },
                        success: function (res) {
                            grid.dataSource.read();
                            $rootScope.IsLoading = false;
                            $scope.StockProductHasChoose = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (e) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            })
        }
    };

    $scope.Stock_Product_Search = function ($event, win, grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.StockNotin_ProductGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.ProductNotin_Read,
            readparam: function () { return { stockID: $scope.StockData.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', editable: false },
                    Address: { type: 'string', editable: false },
                    StockCode: { type: 'string', editable: false },
                    StockName: { type: 'string', editable: false }
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,StockNotin_ProductGrid,StockNotin_ProductGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,StockNotin_ProductGrid,StockNotin_ProductGridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Mã KH', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.StockNotin_ProductGridChange = function ($event, grid, haschoose) {
        $scope.StockProductSearchHasChoose = haschoose;
    };

    $scope.Stock_ProductSaveList = function ($event, gridSource, gridTarget) {
        $event.preventDefault();

        var dataSource = gridSource.dataSource.data();
        var lstID = [];
        for (var i = 0; i < dataSource.length; i++) {
            if (dataSource[i].IsChoose) {
                lstID.push(dataSource[i].ID);
            }
        }
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn thêm các dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.ProductNotin_Save,
                        data: { lst: lstID, stockID: $scope.StockData.ID },
                        success: function (res) {
                            $scope.StockProductSearchHasChoose = false;
                            gridSource.dataSource.read();
                            gridTarget.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Đã thêm mới', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            })
        }
    }
    //#endregion

    //cap nhat khu vuc cho diem
    $scope.Location_ChooseArea = function ($event, data, win) {
        $event.preventDefault();

        $scope.Area_LocationID = data.LocationID;
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.RoutingAreaList,
            data: { locationID: $scope.Area_LocationID },
            success: function (res) {
                $rootScope.IsLoading = false;
                win.center();
                win.open();
                $.each(res, function (i, v) {
                    v.IsChoose = false;
                });
                $scope.Area_GridOptions.dataSource.data(res);
                $scope.AreaHasChoose = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    };


    $scope.Area_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: false, pageSize: 20, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
             {
                 title: ' ', width: '30px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Area_Grid,Area_GridChooseChange)" />',
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Area_Grid,Area_GridChooseChange)" />',
                 filterable: false, sortable: false
             },
            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'AreaName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Area_GridChooseChange = function ($event, grid, haschoose) {
        $scope.AreaHasChoose = haschoose;
    };

    $scope.Area_NotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.RoutingAreaNotInList,
            readparam: function () { return { locationID: $scope.Area_LocationID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
            pageSize: 100,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
             {
                 title: ' ', width: '30px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Area_NotIn_Grid,Area_NotIn_GridChooseChange)" />',
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Area_NotIn_Grid,Area_NotIn_GridChooseChange)" />',
                 filterable: false, sortable: false
             },
            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'AreaName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.Area_NotIn_GridChooseChange = function ($event, grid, haschoose) {
        $scope.AreaSearchHasChoose = haschoose;
    };

    $scope.Area_Search = function ($event, win, grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.Area_Delete = function ($event, grid) {
        $event.preventDefault();
        var lstAreaID = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstAreaID.push(v.ID);
        });

        if (lstAreaID.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.RoutingAreaNotInDeleteList,
                data: { lstAreaID: lstAreaID, locationID: $scope.Area_LocationID },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.RoutingAreaList,
                        data: { locationID: $scope.Area_LocationID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $.each(res, function (i, v) {
                                v.IsChoose = false;
                            });
                            $scope.Area_GridOptions.dataSource.data(res);
                            $scope.AreaHasChoose = false;
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                },
                error: function (ds) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.Area_Save = function ($event, grid, win) {
        $event.preventDefault();
        var lstAreaID = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstAreaID.push(v.ID);
        });

        if (lstAreaID.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.RoutingAreaNotInSave,
                data: { lstAreaID: lstAreaID, locationID: $scope.Area_LocationID },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_Detail.URL.RoutingAreaList,
                        data: { locationID: $scope.Area_LocationID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $.each(res, function (i, v) {
                                v.IsChoose = false;
                            });
                            $scope.Area_GridOptions.dataSource.data(res);
                            win.close();
                            $scope.AreaHasChoose = false;
                            $scope.Area_NotIn_GridChooseChange = false;
                            $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            });
        }
    };

    //#region filter by partner
    $scope.Partner_FilterByPartner = function ($event, win, grid) {
        $event.preventDefault();
        if ($scope.IsFilterPartner) {
            $scope.IsFilterPartner = false;
            $scope.LoadDataPartner("Đã cập nhật", win);
        }
        else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.FilterByPartner_GetNum,
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.center();
                    win.open();
                    if (res.length > 0)
                        $scope.ListFilterPartnerIDChoose = res;
                    grid.dataSource.read();

                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.filterByPartner_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.FilterByPartner_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="filterByPartnerChooseAll_Check($event,filterByPartner_grid)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="filterByPartnerChoose_Check($event,filterByPartner_grid)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'TypeOfPartnerName', title: 'Loại đối tác', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATName', title: 'Tên hệ thống', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATCode', title: 'Mã hệ thống', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Điạ chỉ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfPartnerName', title: 'Loại NPP', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh / thành phố', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận / huyện', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerCode', title: 'Mã sử dụng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            var grid = this;
            var data = grid.items();
            if (Common.HasValue(grid) && Common.HasValue(grid.tbody)) {
                Common.Data.Each(data, function (tr) {
                    var item = grid.dataItem(tr);
                    if ($scope.ListFilterPartnerIDChoose.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('td input.chkChoose').prop('checked', true);
                        if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
                    }
                })
            }
        }
    }

    $scope.filterByPartnerChooseAll_Check = function ($event, grid) {
        if ($event.target.checked == true) {
            grid.items().each(function () {
                var tr = this, item = grid.dataItem(tr);
                $scope.ListFilterPartnerIDChoose.push(item.ID);
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
                $scope.ListFilterPartnerIDChoose.splice($scope.ListFilterPartnerIDChoose.indexOf(item.ID), 1)
                if (item.IsChoose == true) {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                    if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
                }
            });
        }
    };

    $scope.filterByPartnerChoose_Check = function ($event, grid) {
        var tr = $($event.target).closest('tr'), item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');

            $scope.ListFilterPartnerIDChoose.push(item.ID);
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
            if ($scope.ListFilterPartnerIDChoose.indexOf(item.ID) > -1) {
                $scope.ListFilterPartnerIDChoose.splice($scope.ListFilterPartnerIDChoose.indexOf(item.ID), 1)
            }
        }
    }

    $scope.ApplyFilterByPartner_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.IsFilterPartner = true;
        $scope.LoadDataPartner("Đã cập nhật", win);
    };
    //#endregion

    //#region filter by location
    $scope.Partner_FilterByLocation = function ($event, win, grid) {
        $event.preventDefault();
        if ($scope.IsFilterLocation) {
            $scope.IsFilterLocation = false;
            $scope.LoadDataPartner("Đã cập nhật", win);
        }
        else {
            win.center();
            win.open();
            grid.dataSource.read();
        }
    };

    $scope.filterByLocation_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.FilterByLocation_List,
            model: {
                id: 'CusLocationID',
                fields: {
                    CusLocationID: { type: 'number' },
                    IsChoose: { type: 'boolean', editable: false },
                    CatLocationID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="filterByLocationChooseAll_Check($event,filterByLocation_grid)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="filterByLocationChoose_Check($event,filterByLocation_grid)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'TypeOfPartnerName', title: 'Loại đối tác', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerCode', title: 'Mã đối tác', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', title: 'Tên đối tác', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerAddress', title: 'Điạ chỉ đối tác', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATLocationCode', title: 'Mã hệ thống', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CATLocationName', title: 'Tên hệ thống', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSLocationCode', title: 'Mã sử dụng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSLocationName', title: 'Tên sử dụng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh / thành phố', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận / huyện', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            var grid = this;
            var data = grid.items();
            if (Common.HasValue(grid) && Common.HasValue(grid.tbody)) {
                Common.Data.Each(data, function (tr) {
                    var item = grid.dataItem(tr);
                    if ($scope.ListFilterCusLocationIDChoose.indexOf(item.CusLocationID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('td input.chkChoose').prop('checked', true);
                        if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');
                    }
                })
            }
        }
    }

    $scope.filterByLocationChooseAll_Check = function ($event, grid) {
        if ($event.target.checked == true) {
            grid.items().each(function () {
                var tr = this, item = grid.dataItem(tr);
                $scope.ListFilterLocationIDChoose.push(item.CatLocationID);
                $scope.ListFilterCusLocationIDChoose.push(item.CusLocationID);
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
                $scope.ListFilterLocationIDChoose.splice($scope.ListFilterLocationIDChoose.indexOf(item.CatLocationID), 1);
                $scope.ListFilterCusLocationIDChoose.splice($scope.ListFilterLocationIDChoose.indexOf(item.CusLocationID), 1)
                if (item.IsChoose == true) {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                    if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
                }
            });
        }
    };

    $scope.filterByLocationChoose_Check = function ($event, grid) {
        var tr = $($event.target).closest('tr'), item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose')) $(tr).addClass('IsChoose');

            $scope.ListFilterLocationIDChoose.push(item.CatLocationID);
            $scope.ListFilterCusLocationIDChoose.push(item.CusLocationID);
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose')) $(tr).removeClass('IsChoose');
            if ($scope.ListFilterLocationIDChoose.indexOf(item.CatLocationID) > -1) {
                $scope.ListFilterLocationIDChoose.splice($scope.ListFilterLocationIDChoose.indexOf(item.CatLocationID), 1)
            }
            if ($scope.ListFilterCusLocationIDChoose.indexOf(item.CusLocationID) > -1) {
                $scope.ListFilterCusLocationIDChoose.splice($scope.ListFilterCusLocationIDChoose.indexOf(item.CusLocationID), 1)
            }
        }
    }

    $scope.ApplyFilterByLocation_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.IsFilterLocation = true;
        $scope.LoadDataPartner("Đã cập nhật", win);

    };

    $scope.Partner_FilterByUseLocation = function ($event, grid) {
        $event.preventDefault();
        $scope.IsFilterUseLocation = !$scope.IsFilterUseLocation;
        $scope.LoadDataPartner("Đã cập nhật", "");
    };
    //#endregion

    //#region cung duong khi luu kho
    $scope.routing_contract_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_Contract_List,
            readparam: function () { return { locationid: $scope.PartnerRouting_LocationID } },
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
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_Contract_SaveList,
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
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_Contract_NewRoutingGet,
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
                url: Common.Services.url.FLM,
                method: _FLMSetting_Detail.URL.Routing_Contract_NewRoutingSave,
                data: { item: $scope.NewItemRouting},
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
        url: Common.Services.url.FLM,
        method: _FLMSetting_Detail.URL.Routing_Contract_ContractData,
        success: function (res) {
            $scope.cboRoutingContract_Options.dataSource.data(res);
        }
    });

    $scope.PartnerRoutingArea_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_Contract_AreaList,
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
            url: Common.Services.url.FLM,
            method: _FLMSetting_Detail.URL.Routing_Contract_NewAreaSave,
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

    $scope.autoUpdateCusPartnerCode = function () {
        if (!Common.HasValue($scope.ItemPartner.CUSCode) || $scope.ItemPartner.CUSCode == '') {
            $scope.ItemPartner.CUSCode = $scope.ItemPartner.CATCode
        }
    };

    $scope.autoUpdateCusStockCode = function () {
        if (!Common.HasValue(ItemPartner.CUSCode) || $scope.ItemPartner.CUSCode == '') {
            $scope.StockData.Code = $scope.StockData.CATCode
        }
    };

    //#endregion
    $scope.Routing_ExcelOnline_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 15; i++) {
            var resource = $rootScope.RS[_FLMSetting_Detail.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã hệ thống] không được trống', //0
                '[Mã hệ thống] đã sử dụng', //1
                '[Mã cung đường] không được trống', //2
                '[Mã cung đường] đã sử dụng', //3
                '[Mã cung đường] đã sử dụng trong hợp đồng', //4
                '[Zone] không chính xác', //5
                '[LeadTime] không chính xác', //6
                '[Thứ tự] không chính xác', //7
                '[Phụ lục] không tồn tại', //8
                '[Mã loại cung đường] không được trống', //9
                '[Mã loại cung đường] không tồn tại', //10
                '[Khu vực đi] không tại trong hệ thống', //11
                '[Khu vực đi] không được trống', //12
                '[Khu vực đến] không tại trong hệ thống', //13
                '[Khu vực đến] không được trống', //14
                '[Điểm đi] không tại trong hệ thống', //15
                '[Điểm đi] không được trống', //16
                '[Điểm đến] không tại trong hệ thống', //17
                '[Điểm đến] không được trống', //18
                "[Mã cung đường hệ thống] đã sử dụng trong hợp đồng", //19
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMSetting_Detail.ExcelKey.ContractRouting + $scope.ID,
            params: { contractID: $scope.ID, customerID: $scope.Item.CustomerID},
            rowStart: 1,
            colCheckChange: 15,
            url: Common.Services.url.FLM,
            methodInit: _FLMSetting_Detail.URL.RoutingExcelOnline_Init,
            methodChange: _FLMSetting_Detail.URL.RoutingExcelOnline_Change,
            methodImport: _FLMSetting_Detail.URL.RoutingExcelOnline_Import,
            methodApprove: _FLMSetting_Detail.URL.RoutingExcelOnline_Approve,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.routing_gridOptions.dataSource.read();
            }
        });
    };

    //#region ContractQuantity
    $scope.cboTypeOfContractQuantityOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfContractQuantity,
        success: function (res) {
            if (res.length > 0) {
                var data = [];
                data.push({ ID: -1, ValueOfVar: "" });
                $.each(res, function (i, v) {
                    data.push(v);
                });
                $scope.cboTypeOfContractQuantityOptions.dataSource.data(data);
            }
        }
    });
    //#endregion
}]);