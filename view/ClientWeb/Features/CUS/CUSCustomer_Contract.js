/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSCustomer_Contract = {
    URL: {
        Get: 'CUSContract_Get',
        Save: 'CUSContract_Save',
        Delete: 'CUSContract_Delete',
        Data: 'CUSContract_Data',

        CODefault_List: 'CUSContract_CODefault_List',
        CODefault_Save: 'CUSContract_CODefault_Update',
        CODefault_NotIn_List: 'CUSContract_CODefault_NotInList',
        CODefault_NotIn_Save: 'CUSContract_CODefault_NotIn_SaveList',
        CODefault_Delete: 'CUSContract_CODefault_Delete',

        Material_List: 'CUSContract_Material_List',
        Material_Get: 'CUSContract_Material_Get',
        Material_Save: 'CUSContract_Material_Save',
        Material_Delete: 'CUSContract_Material_Delete',
        Material_RoutingList: 'CUSContract_Material_RoutingList',
        Material_RoutingNotList: 'CUSContract_Material_RoutingNotInList',
        Material_RoutingNotInSave: 'CUSContract_Material_RoutingNotInSaveList',
        Material_RoutingDelete: 'CUSContract_Material_RoutingDeleteList',

        Routing_List: 'CUSContract_Routing_List',
        Routing_Save: 'CUSContract_Routing_Save',
        Routing_NotIn_List: 'CUSContract_Routing_NotIn_List',
        Routing_Insert: 'CUSContract_Routing_Insert',
        Routing_Delete: 'CUSContract_Routing_Delete',
        Routing_LeadTime_Check: 'CUSContract_Routing_LeadTime_Check',
        Routing_ContractTerm: 'CUSContract_Routing_ContractTermList',

        Routing_Excel_Check: 'CUSContract_Routing_Excel_Check',
        Routing_Excel_Export: 'CUSContract_Routing_Export',
        Routing_Excel_Import: 'CUSContract_Routing_Import',

        RoutingExcelOnline_Init: 'CUSContract_Routing_ExcelOnline_Init',
        RoutingExcelOnline_Change: 'CUSContract_Routing_ExcelOnline_Change',
        RoutingExcelOnline_Import: 'CUSContract_Routing_ExcelOnline_Import',
        RoutingExcelOnline_Approve: 'CUSContract_Routing_ExcelOnline_Approve',

        KPI_Save: 'CUSContract_KPI_Save',
        KPI_Check_Expression: 'CUSContract_KPI_Check_Expression',
        KPI_Check_KPI: 'CUSContract_KPI_Check_Hit',
        KPI_Routing_List: 'CUSContract_KPI_Routing_List',
        KPI_Routing_Apply: 'CUSContract_KPI_Routing_Apply',

        Price_List: 'CUSContract_Price_List',
        Price_Copy: 'CUSContract_Price_Copy',
        Price_Delete: 'CUSContract_Price_Delete',
        Price_Get: 'CUSContract_Price_Get',
        Price_Save: 'CUSContract_Price_Save',

        GroupOfProduct_List: 'CUSContract_GroupOfProduct_List',
        GroupOfProduct_Get: 'CUSContract_GroupOfProduct_Get',
        GroupOfProduct_Save: 'CUSContract_GroupOfProduct_Save',
        GroupOfProduct_Delete: 'CUSContract_GroupOfProduct_Delete',
        GroupOfProduct_Check: 'CUSContract_GroupOfProduct_Check',

        Read_LocationNotIn: 'CUSContract_NewRouting_LocationList',
        Read_AreaNotIn: 'CUSContract_NewRouting_AreaList',
        Save_Area: 'CUSContract_NewRouting_AreaSave',
        Delete_Area: 'CUSContract_NewRouting_AreaDelete',
        Refresh_RoutingArea: 'CUSContract_NewRouting_AreaRefresh',
        Get_Area: 'CUSContract_NewRouting_AreaGet',
        Read_AreaDetail: 'CUSContract_NewRouting_AreaDetailList',
        Get_AreaDetail: 'CUSContract_NewRouting_AreaDetailGet',
        Delete_AreaDetail: 'CUSContract_NewRouting_AreaDetailDelete',
        Save_AreaDetail: 'CUSContract_NewRouting_AreaDetailSave',
        Save_Routing: 'CUSContract_NewRouting_Save',
        Get_Routing: 'CUSContract_NewRouting_Get',

        AreaLocation_List: 'CUSContract_NewRouting_AreaLocation_List',
        AreaLocation_NotInList: 'CUSContract_NewRouting_AreaLocationNotIn_List',
        AreaLocation_NotInSave: 'CUSContract_NewRouting_AreaLocationNotIn_Save',
        AreaLocation_Delete: 'CUSContract_NewRouting_AreaLocation_Delete',
        AreaLocation_Copy: 'CUSContract_NewRouting_AreaLocation_Copy',

        ContractRouting_Delete: 'CUSContract_Routing_NotIn_Delete',
        Routing_search_List: 'CUSContract_Routing_CATNotIn_List',
        Routing_search_save: 'CUSContract_Routing_CATNotIn_Save',

        TypeOfRunLevelSave: 'CUSContract_Setting_TypeOfRunLevelSave',
        TypeOfSGroupProductChangeSave: 'CUSContract_Setting_TypeOfSGroupProductChangeSave',
        contractSetting_save: "CATContractSetting_Save",

        GOVListSetting: 'CUSContract_Setting_Level_GOVList',

        GOVList: 'CUSContract_Setting_GOVList',
        GOVGet: 'CUSContract_Setting_GOVGet',
        GOVSave: 'CUSContract_Setting_GOVSave',
        GOVDeleteList: 'CUSContract_Setting_GOVDeleteList',
        GOVNotInList: 'CUSContract_Setting_GOVNotInList',
        GOVNotInSave: 'CUSContract_Setting_GOVNotInSave',

        LevelList: 'CUSContract_Setting_LevelList',
        LevelGet: 'CUSContract_Setting_LevelGet',
        LevelSave: 'CUSContract_Setting_LevelSave',
        LevelDeleteList: 'CUSContract_Setting_LevelDeleteList',

        Term_List: 'CUSContract_ContractTerm_List',
        Term_Get: 'CUSContract_ContractTerm_Get',
        Term_Save: 'CUSContract_ContractTerm_Save',
        Term_Delete: 'CUSContract_ContractTerm_Delete',
        Term_Price_List: 'CUSContract_ContractTerm_Price_List',
        Term_Lock: 'CUSContract_ContractTerm_Close',
        Term_UnLock: 'CUSContract_ContractTerm_Open',
        Term_RemoveWarning: 'CUSTerm_Change_RemoveWarning',

        PriceHistory: 'PriceHistory_ExportOneUser',

        GroupOfProduct_ExcelInit: 'CUSContract_GroupOfProduct_ExcelInit',
        GroupOfProduct_ExcelChange: 'CUSContract_GroupOfProduct_ExcelChange',
        GroupOfProduct_ExcelImport: 'CUSContract_GroupOfProduct_ExcelImport',
        GroupOfProduct_ExcelApprove: 'CUSContract_GroupOfProduct_ExcelApprove',
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
    Item: {
        MaterialNew: {
            ID: 0,
            MaterialID: 0,
            MaterialName: '',
            PriceCurrent: 0,
            PriceContract: 0,
            RateCurrent: 0,
            RateContract: 0
        }
    },
    Const: {
        CustomerID: -1
    },
    ExcelKey: {
        Resource: "CUSCustomer_Contract_Excel",
        ContractRouting: "CUSContractRouting",
        GroupOfProduct: "CUSContractGroupOfProduct",
        ResourceGroupOfProduct: "CUSContract_GroupOfProduct_Excel",
    }
}

angular.module('myapp').controller('CUSCustomer_ContractCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CUSContract_DetailCtrl');
    $rootScope.IsLoading = false;

    $rootScope.Loading.Show('Hợp đồng khách hàng');
    var LoadingStep = 35;

    $scope.TabIndex = 0;
    $scope.TabIndexM = 0;
    $scope.ID = $state.params.ID;
    $scope.customerID = $state.params.CustomerID;
    $scope.Item = null;
    $scope.GOPItem = null;
    $scope.ItemMaterial = { ID: 0 }
    $scope.CUSRoutingItem = {};
    $scope.PointLocationItem = {};
    $scope.AreaEditItem = {};
    $scope.AreaDetailItem = {};
    $scope.AreaLocationItem = {};
    $scope.CUSRoutingItem.IsArea = false;
    $scope.CurrentAreaID = 0;
    $scope.RouteHasChoose = false;
    $scope.RouteNotInHasChoose = false;
    $scope.ParamEdit = { ID: -1, ContractID: -1, CustomerID: -1 },
    $scope.ParamService = { ServiceOfOrderID: -1, GetEmpty: 0, ReturnEmpty: 0 };

    $scope.numMinWeight_options = { format: '#', spinners: false, culture: 'en-US', min: 0, max: 100, step: 1, decimals: 1, }

    //#region Auth
    if ($scope.ID <= 0) {
        if (Common.HasValue($scope.customerID) && $scope.customerID > 0) {
            if (!$rootScope.CheckView("ActAdd", "main.CUSCustomer.Detail", { id: $scope.customerID })) return;
        }
        else {
            if (!$rootScope.CheckView("ActAdd", "main.CUSContract.Index")) return;
        }
    }
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

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
                    $scope.gop_cbo_ProductOptions.dataSource.data(_CUSCustomer_Contract.Data.ListProduct[val]);

                    $scope.GOPItem.ProductID = -1;
                }, 1)
            }
        }
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
                    if (Common.HasValue(_CUSCustomer_Contract.Data.ListProduct[val])) {
                        $.each(_CUSCustomer_Contract.Data.ListProduct[val], function (i, v) {
                            datacbo.push(v);
                        });
                    }
                    $scope.gop_cbo_ProductChangeOptions.dataSource.data(datacbo);
                    $scope.GOPItem.ProductIDChange = -1;
                }, 1)
            }
        }
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

    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSCustomer_Contract.URL.Data,
        data: { ID: $scope.ID },
        success: function (res) {
            _CUSCustomer_Contract.Data.ListCustomer = res.ListCustomer;
            _CUSCustomer_Contract.Data.ListCompany = {};
            _CUSCustomer_Contract.Data.ListKPI = res.ListKPI;
            _CUSCustomer_Contract.Data.ListGOP = res.ListGroupOfProduct;
            $scope.gop_cbo_GroupOfProductOptions.dataSource.data(res.ListGroupOfProduct);

            _CUSCustomer_Contract.Data.ListGOPHasEmpty = [];
            _CUSCustomer_Contract.Data.ListGOPHasEmpty.push({ ID: -1, Code: ' ', GroupName: ' ' });
            _CUSCustomer_Contract.Data.ListProduct = {};

            var itemProEmpty = { ID: -1, Code: ' ', ProductName: ' ' };

            Common.Data.Each(_CUSCustomer_Contract.Data.ListGOP, function (o) {
                _CUSCustomer_Contract.Data.ListGOPHasEmpty.push(o);
                if (!Common.HasValue(_CUSCustomer_Contract.Data.ListProduct[o.ID])) {
                    _CUSCustomer_Contract.Data.ListProduct[o.ID] = [];
                    _CUSCustomer_Contract.Data.ListProduct[o.ID].push(itemProEmpty);
                }
            })
            _CUSCustomer_Contract.Data.ListProduct['-1'] = [];
            _CUSCustomer_Contract.Data.ListProduct['-1'].push(itemProEmpty);
            $scope.gop_cbo_GroupOfProductChangeOptions.dataSource.data(_CUSCustomer_Contract.Data.ListGOPHasEmpty);

            Common.Data.Each(res.ListProduct, function (o) {
                _CUSCustomer_Contract.Data.ListProduct[o.GroupOfProductID].push(o);
            })


            //Customer & Company Data
            Common.Data.Each(res.ListCustomer, function (o) {
                _CUSCustomer_Contract.Data.ListCompany[o.ID] = [];
            })
            Common.Data.Each(res.ListCompany, function (o) {
                _CUSCustomer_Contract.Data.ListCompany[o.CustomerOwnID].push(o);
            })
            $timeout(function () {
                $scope.cboCustomerOptions.dataSource.data(res.ListCustomer);
            }, 1)

            _CUSCustomer_Contract.Data.ListMaterial = res.ListMaterial;
            //$scope.cboMaterial_Options.dataSource.data(_CUSCustomer_Contract.Data.ListMaterial);
            var obj = _CUSCustomer_Contract.Data.ListMaterial[0];
            if (Common.HasValue(obj)) {
                _CUSCustomer_Contract.Item.MaterialNew.MaterialID = obj.ID;
                _CUSCustomer_Contract.Item.MaterialNew.MaterialName = obj.MaterialName;
            }

            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.Get,
                data: { ID: $scope.ID },
                success: function (res) {
                    $scope.Item = res;
                    _CUSCustomer_Contract.Data.ListSetting = res.ListSetting;
                    $scope.ParamService.ServiceOfOrderID = res.ListSetting[0].ServiceOfOrderID;
                    $scope.ParamService.Laden = res.ListSetting[0].Laden;
                    $scope.ParamService.GetEmpty = res.ListSetting[0].GetEmpty;
                    $scope.ParamService.ReturnEmpty = res.ListSetting[0].ReturnEmpty;
                    if ($scope.Item.ID < 1) {
                        $scope.TypeOfRunIDBackup = -1;

                        if ($scope.customerID < 1) {
                            $scope.Item.IsDisabled = false;
                        } else {
                            $scope.Item.IsDisabled = true;
                            $scope.Item.CustomerID = $scope.customerID;
                        }
                    } else {
                        _CUSCustomer_Contract.Const.CustomerID = $scope.Item.CustomerID;
                        _CUSCustomer_Contract.Const.CompanyID = $scope.Item.CompanyID;
                        $scope.TypeOfRunIDBackup = $scope.Item.TypeOfRunLevelID;
                        //$scope.material_gridOptions.dataSource.read();
                    }

                    $scope.LoadDataCompany(false);
                    $scope.routing_gridOptions.dataSource.read();
                    $rootScope.Loading.Change("Thông tin chung ...", $rootScope.Loading.Progress + LoadingStep);
                    $scope.codefault_gridOptions.dataSource.read();
                }
            })

            Common.ALL.Get($http, {
                url: Common.ALL.URL.SYSVarPriceOfGOP,
                success: function (res) {
                    _CUSCustomer_Contract.Data.ListPriceOfGOP = res;
                }
            })
        }
    })

    //#region Info
    $scope.cboCustomerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function () {
            $scope.LoadDataCompany(true);
        }
    };

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
    };

    $scope.LoadDataCompany = function (isChange) {
        Common.Log("LoadDataCompany");
        var cusID = $scope.Item.CustomerID;
        var data = _CUSCustomer_Contract.Data.ListCompany[cusID]
        $scope.cboCompanyOptions.dataSource.data(data);
        $scope.Item.CustomerID = cusID;
        $timeout(function () {
            if (Common.HasValue(data) && data.length > 0 && isChange) {
                $scope.Item.CompanyID = data[0].ID;
            }
        }, 1)
    };

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
    };

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
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (res) {
            $timeout(function () {
                $scope.cboTransportOptions.dataSource.data(res);
            }, 1);
        }
    });

    $scope.cboContractTypeOptions = {
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
        url: Common.ALL.URL.SYSVarTypeOfContract,
        success: function (res) {
            $timeout(function () {
                var data = [];
                $.each(res, function (i, v) {
                    if (v.ID != 84)
                        data.push(v);
                })
                $scope.cboContractTypeOptions.dataSource.data(data);
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

    //#region CODefault
    $scope.HasCODefault = false;

    $scope.codefault_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.CODefault_List,
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
            $scope.routing_gridOptions.dataSource.read();
        }
    };

    $scope.codefault_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasCODefault = hasChoose;
    };

    $scope.codefault_notin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.CODefault_NotIn_List,
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
            { field: 'PackingName', title: 'Tên cont' },
            { field: 'TypeOfPackageName', title: 'Loại cont' },
        ]
    };

    $scope.CODefault_Save_Click = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.CODefault_Save,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.CODefault_NotIn_Save,
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
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.CODefault_Delete,
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

    ////#region Material
    //$scope.material_tabstripOptions = {
    //    animation: {
    //        open: { effects: "fadeIn" }
    //    },
    //    select: function (e) {
    //        $timeout(function () {
    //            $scope.TabIndexM = angular.element(e.item).data('tabindex');
    //            Common.Log("Select_TabM_" + $scope.TabIndexM);
    //        }, 1)
    //    }
    //}
    //$scope.material_gridOptions = {
    //    dataSource: Common.DataSource.Grid($http, {
    //        url: Common.Services.url.CUS,
    //        method: _CUSCustomer_Contract.URL.Material_List,
    //        readparam: function () {
    //            return {
    //                contractID: $scope.ID
    //            }
    //        },
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number' },
    //                PriceWarning: { type: 'number' },
    //                IsWarning: { type: 'boolean' }
    //            }
    //        },
    //    }),
    //    height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
    //    selectable: false, filterable: { mode: 'row' }, reorderable: false, editable: 'false',
    //    columns: [
    //        {
    //            title: ' ', width: '120px',
    //            template: '<a href="/" ng-show="!dataItem.IsClosed" ng-click="Material_Edit_Click($event,dataItem,material_win,material_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
    //                '<a href="/" ng-show="!dataItem.IsClosed" ng-click="Material_Delete_Click($event,dataItem,material_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
    //                '<a href="/" ng-show="dataItem.IsWarning" ng-click="Material_Detail_Click($event,dataItem)" class="k-button"><i class="fa fa-file-text"></i></a>',
    //            filterable: false, sortable: false
    //        },
    //        {
    //            field: 'MaterialCode', width: 300, title: 'Mã Vật tư/nhiên liệu', filterable: { cell: { operator: 'contains', showOperators: false } }
    //        },
    //        {
    //            field: 'PriceContract', title: 'Giá hợp đồng', width: 150, filterable: { cell: { operator: 'gte', showOperators: false } }
    //        },
    //        {
    //            field: 'PriceCurrent', title: 'Giá hiện tại', width: 150, filterable: { cell: { operator: 'gte', showOperators: false } }
    //        },
    //        {
    //            field: 'IsWarning', title: 'Cảnh báo', width: 150,
    //            template: '<input type="checkbox" #= IsWarning=="true" ? "checked=checked" : "" # disabled="disabled" />',
    //            attributes: { style: "text-align: center; " },
    //            filterable: {
    //                cell: {
    //                    template: function (container) {
    //                        container.element.kendoComboBox({
    //                            dataSource: [{ Text: 'Có thay đổi', Value: true }, { Text: 'Không thay đổi', Value: false }, { Text: 'Tất cả', Value: '' }],
    //                            dataTextField: "Text", dataValueField: "Value",
    //                        });
    //                    }, showOperators: false
    //                }
    //            }
    //        },
    //        {
    //            field: 'PriceWarning', width: 120, title: 'Cảnh báo',
    //            filterable: { cell: { operator: 'lte', showOperators: false } }
    //        },
    //        { title: '', filterable: false, sortable: false }
    //    ]
    //}

    //$scope.Material_Detail_Click = function ($event, data) {
    //    $event.preventDefault();
    //    $state.go("main.CUSContract.Material", { ContractMaterialID: data.ID, StatusID: 2 });
    //};

    //$scope.PriceContract_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1000, decimals: 0, }
    //$scope.PriceCurrent_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1000, decimals: 0, }

    //$scope.cboMaterial_Options = {
    //    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
    //    dataTextField: 'MaterialName', dataValueField: 'ID', minLength: 3,
    //    dataSource: Common.DataSource.Local({
    //        data: [],
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number' },
    //                MaterialName: { type: 'string' },
    //            }
    //        }
    //    }),
    //};

    //$scope.Material_Add_Click = function ($event, win, vform) {
    //    $event.preventDefault();
    //    $scope.MaterialLoadItem(0, win, vform)
    //}

    //$scope.Material_Edit_Click = function ($event, data, win, vform) {
    //    $event.preventDefault();
    //    $scope.MaterialLoadItem(data.ID, win, vform)
    //}

    //$scope.Material_Delete_Click = function ($event, item, grid) {
    //    $event.preventDefault();

    //    $rootScope.Message({
    //        Msg: "Xác nhận xóa vật tư/nhiên liệu?",
    //        Type: Common.Message.Type.Confirm,
    //        Ok: function () {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.CUS,
    //                method: _CUSCustomer_Contract.URL.Material_Delete,
    //                data: { id: item.ID },
    //                success: function (res) {
    //                    $rootScope.IsLoading = false;
    //                    $scope.material_gridOptions.dataSource.read();
    //                    $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
    //                }
    //            });
    //        }
    //    });
    //};

    //$scope.MaterialLoadItem = function (id, win, vform) {
    //    $rootScope.IsLoading = true;
    //    Common.Services.Call($http, {
    //        url: Common.Services.url.CUS,
    //        method: _CUSCustomer_Contract.URL.Material_Get,
    //        data: { id: id },
    //        success: function (res) {
    //            vform({ clear: true });
    //            $rootScope.IsLoading = false;
    //            $scope.ItemMaterial = res;
    //            $scope.material_tabstrip.select(0);
    //            if (res.ID > 0) {
    //                $scope.material_routing_GridOptions.dataSource.read();
    //            }
    //            win.center();
    //            win.open();
    //        },
    //        error: function (res) {
    //            $rootScope.IsLoading = false;
    //        }
    //    });
    //}

    //$scope.material_routing_GridOptions = {
    //    dataSource: Common.DataSource.Grid($http, {
    //        url: Common.Services.url.CUS,
    //        method: _CUSCustomer_Contract.URL.Material_RoutingList,
    //        readparam: function () { return { contractMaterialID: $scope.ItemMaterial.ID } },
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number', editable: false, nullable: true },
    //                IsChoose: { type: 'boolean', },
    //            }
    //        }
    //    }),
    //    height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
    //    columns: [
    //        {
    //            title: ' ', width: '35px',
    //            headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,material_routing_Grid,material_routing_GridChoose_Change)" />',
    //            headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
    //            template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,material_routing_Grid,material_routing_GridChoose_Change)" />',
    //            filterable: false, sortable: false
    //        },
    //        { field: 'RoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
    //        { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
    //        { title: ' ', filterable: false, sortable: false }
    //    ]
    //}

    //$scope.material_routing_GridChoose_Change = function ($event, grid, hasChoose) {
    //    $scope.RouteHasChoose = hasChoose;
    //}

    //$scope.material_routing_notin_GridOptions = {
    //    dataSource: Common.DataSource.Grid($http, {
    //        url: Common.Services.url.CUS,
    //        method: _CUSCustomer_Contract.URL.Material_RoutingNotList,
    //        readparam: function () { return { contractMaterialID: $scope.ItemMaterial.ID } },
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number', editable: false, nullable: true },
    //                IsChoose: { type: 'boolean' },
    //            }
    //        }
    //    }),
    //    height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
    //    columns: [
    //        {
    //            title: ' ', width: '35px',
    //            headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,material_routing_notin_Grid,material_routing_notin_GridChoose_Change)" />',
    //            headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
    //            template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,material_routing_notin_Grid,material_routing_notin_GridChoose_Change)" />',
    //            filterable: false, sortable: false
    //        },
    //        { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
    //        { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
    //        { title: ' ', filterable: false, sortable: false }
    //    ]
    //}

    //$scope.material_routing_notin_GridChoose_Change = function ($event, grid, hasChoose) {
    //    $scope.RouteNotInHasChoose = hasChoose;
    //}

    //$scope.material_SaveInfo_Click = function ($event, vform, win) {
    //    $event.preventDefault();
    //    if (vform()) {
    //        $rootScope.IsLoading = true;
    //        Common.Services.Call($http, {
    //            url: Common.Services.url.CUS,
    //            method: _CUSCustomer_Contract.URL.Material_Save,
    //            data: { item: $scope.ItemMaterial, contractID: $scope.ID },
    //            success: function (res) {
    //                $scope.material_gridOptions.dataSource.read();
    //                $rootScope.Message({
    //                    Msg: 'Đã cập nhật.',
    //                    NotifyType: Common.Message.NotifyType.SUCCESS
    //                });
    //                win.close();
    //                $rootScope.IsLoading = false;
    //            },
    //            error: function (res) {
    //                $rootScope.IsLoading = false;
    //            }
    //        });

    //    }
    //};

    //$scope.material_routing_AddNew = function ($event, win) {
    //    $event.preventDefault();
    //    $scope.material_routing_notin_GridOptions.dataSource.read();
    //    win.center();
    //    win.open();
    //    $scope.material_routing_notin_Grid.resize();
    //}

    //$scope.material_routing_notin_Submit_Click = function ($event, grid, win) {
    //    $event.preventDefault();
    //    var data = [];
    //    Common.Data.Each(grid.dataSource.data(), function (o) {
    //        if (o.IsChoose == true) data.push(o.ID);
    //    })
    //    if (data.length > 0) {
    //        $rootScope.IsLoading = true;
    //        Common.Services.Call($http, {
    //            url: Common.Services.url.CUS,
    //            method: _CUSCustomer_Contract.URL.Material_RoutingNotInSave,
    //            data: { contractMaterialID: $scope.ItemMaterial.ID, lst: data },
    //            success: function (res) {
    //                $rootScope.IsLoading = false;
    //                $scope.material_routing_GridOptions.dataSource.read();
    //                win.close();
    //            },
    //            error: function (res) {
    //                $rootScope.IsLoading = false;
    //            }
    //        });

    //    }
    //}

    //$scope.material_routing_Delete = function ($event, grid) {
    //    $event.preventDefault();

    //    var data = [];
    //    Common.Data.Each(grid.dataSource.data(), function (o) {
    //        if (o.IsChoose == true) data.push(o.ID);
    //    })
    //    if (data.length > 0) {
    //        $rootScope.Message({
    //            Msg: "Xác nhận xóa cung đường?",
    //            Type: Common.Message.Type.Confirm,
    //            Ok: function () {
    //                $rootScope.IsLoading = true;
    //                Common.Services.Call($http, {
    //                    url: Common.Services.url.CUS,
    //                    method: _CUSCustomer_Contract.URL.Material_RoutingDelete,
    //                    data: { lst: data },
    //                    success: function (res) {
    //                        $rootScope.IsLoading = false;
    //                        $scope.material_routing_GridOptions.dataSource.read();
    //                        $rootScope.Message({
    //                            Msg: 'Đã xóa.',
    //                            NotifyType: Common.Message.NotifyType.SUCCESS
    //                        });
    //                        $scope.RouteHasChoose = false;
    //                    },
    //                    error: function (res) {
    //                        $rootScope.IsLoading = false;
    //                    }
    //                });
    //            }
    //        });
    //    }
    //};
    ////#endregion

    //#region Routing

    $scope.RoutingItem = null;

    $scope.routing_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Routing_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PriceWarning: { type: 'number' },
                    IsWarning: { type: 'boolean' },
                    LeadTime: { type: 'number' },
                    Zone: { type: 'number' },
                }
            },
            pageSize: 20,
            sort: [{ field: "SortOrder", dir: "asc" }],
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '200px',
                template: '<a href="/" ng-click="Routing_Edit_Click($event,dataItem,routing_info_win)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Routing_KPI_Click($event,dataItem,kpi_info_win)" class="k-button"><i class="fa fa-info"></i></a>' +
                    '<a href="/" ng-click="Routing_CATDetail_Click($event,dataItem,RoutingAdd_win,CUSRoutingEdit_win_vform)" class="k-button"><i class="fa fa-file"></i></a>' +
                    '<a href="/" ng-click="Routing_CATAreaDetail_Click($event,dataItem,RoutingAreaDetail_win)" class="k-button" ng-show="dataItem.IsArea"><i class="fa fa-map-marker"></i></a>' +
                    '<a href="/" ng-click="Routing_Delete_Click($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
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
            $rootScope.Loading.Change("Thông tin cung đường ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.gop_gridOptions.dataSource.read();
        }
    }

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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Get_Routing,
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
    //#region location area from
    $scope.AreaLocationFromHasChoose = false;

    $scope.AreaLocationFrom_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.AreaLocation_List,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.AreaLocation_NotInSave,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.AreaLocation_Delete,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.AreaLocation_List,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.AreaLocation_NotInSave,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.AreaLocation_Delete,
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

    $scope.Routing_CATDetail_Click = function ($event, data, win, vform) {
        $event.preventDefault();
        $scope.LoadCUSRoutingItem(data.RoutingID, win, vform)
    }

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
        DateTOMasterETD: new Date(),
        DateTOMasterETA: new Date(),
        DateTOMasterATD: new Date(),
        DateTOMasterATA: new Date(),
        DateOrderETD: new Date(),
        DateOrderETA: new Date(),
        DateOrderETDRequest: new Date(),
        DateOrderETARequest: new Date(),
        DateOrderCutOfTime: new Date(),
        Expression: '',
        ExpressionInput: '',
        Zone: 0,
        LeadTime: 0,
        CompareField: ''
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
    $scope.RoutingTypeBak = -1;
    $scope.ContractTermBak = -1;

    $scope.Routing_Edit_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.RoutingItem = $.extend({}, true, item);
        $scope.RoutingTypeBak = $scope.RoutingItem.ContractRoutingTypeID;
        $scope.ContractTermBak = $scope.RoutingItem.ContractTermID;

        $scope.KPIItemCheck.Zone = item.Zone > 0 ? item.Zone : 0;
        $scope.KPIItemCheck.LeadTime = item.LeadTime > 0 ? item.LeadTime : 0;
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Routing_ContractTerm,
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
    }

    $scope.Routing_KPI_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.RoutingItem = $.extend({}, true, item);
        $scope.KPIItemCheck.Zone = item.Zone > 0 ? item.Zone : 0;
        $scope.KPIItemCheck.LeadTime = item.LeadTime > 0 ? item.LeadTime : 0;

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

    $scope.Routing_Delete_Click = function ($event, item) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa cung đường?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Routing_Delete,
                    data: { ID: item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            $scope.routing_gridOptions.dataSource.read();
                        })
                    }
                });
            }
        });
    }

    $scope.Routing_Add_Click = function ($event, grid, win) {
        $event.preventDefault();

        $scope.routing_notin_gridOptions.dataSource.read();
        win.center().open();
    }

    $scope.routing_notin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Routing_NotIn_List,
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
            }
        ]
    }

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

    $scope.RoutingCUSDelete_Click = function ($event, data) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa ?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.ContractRouting_Delete,
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

    $scope.RoutingCUSEdit_Click = function ($event, data, win, vform) {
        $event.preventDefault();
        $scope.LoadCUSRoutingItem(data.RoutingID, win, vform);
    }

    $scope.Routing_NotIn_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();
        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true; });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.Routing_Insert,
                data: { data: data, contractID: $scope.ID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật." });
                        $scope.routing_gridOptions.dataSource.read();
                        win.close();
                        $rootScope.IsLoading = false;
                    })
                }
            })
        } else {
            win.close();
        }
    }

    $scope.numSortOrder_options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0, }

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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Routing_Save,
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
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Routing_Excel_Export,
                    data: { contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Routing_Excel_Check,
                    data: { file: e.FilePath, contractID: $scope.ID, customerID: $scope.Item.CustomerID },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Routing_Excel_Import,
                    data: { data: data, contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        $scope.routing_gridOptions.dataSource.read();
                    }
                })
            }
        })
    }


    $scope.KPI_SaveChanges_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.KPI_Save,
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

    $scope.KPI_Save_Click = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = [];
        data.push($scope.KPIItemCheck);
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.KPI_Save,
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

    $scope.KPI_Check_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.KPIItemCheck.ID = item.ID;
        $scope.KPIItemCheck.KPICode = item.KPICode;
        $scope.KPIItemCheck.Expression = item.Expression;
        $scope.KPIItemCheck.CompareField = item.CompareField;
        $scope.KPIItemCheck.KPIID = item.KPIID;
        win.center().open();
    }

    $scope.KPI_Check_Expression_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.KPI_Check_Expression,
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

    $scope.KPI_Check_KPI_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.KPI_Check_KPI,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.KPI_Routing_List,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.KPI_Routing_Apply,
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

    $scope.Routing_ExcelOnline_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 15; i++) {
            var resource = $rootScope.RS[_CUSCustomer_Contract.ExcelKey.Resource + '_' + i];
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
            functionkey: _CUSCustomer_Contract.ExcelKey.ContractRouting + $scope.ID,
            params: { contractID: $scope.ID, customerID: $scope.Item.CustomerID },
            rowStart: 1,
            colCheckChange: 15,
            url: Common.Services.url.CUS,
            methodInit: _CUSCustomer_Contract.URL.RoutingExcelOnline_Init,
            methodChange: _CUSCustomer_Contract.URL.RoutingExcelOnline_Change,
            methodImport: _CUSCustomer_Contract.URL.RoutingExcelOnline_Import,
            methodApprove: _CUSCustomer_Contract.URL.RoutingExcelOnline_Approve,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.routing_gridOptions.dataSource.read();
            }
        });
    };
    //#endregion

    //#region Price

    $scope.HasPriceChoose = false;

    $scope.price_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Price_List,
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
                title: ' ', width: '110px',
                template: '<a href="/" ng-click="Price_Edit_Click($event,dataItem,price_win,price_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Price_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="Price_Detail_Click($event,dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Tên bảng giá', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EffectDate', width: 120, title: 'Ngày hiệu lực', template: "#=EffectDate==null?' ':kendo.toString(EffectDate, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Name', title: 'Mô tả',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }

    $scope.price_copy_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: false,
        columns: [
            { field: 'Code', title: 'Mã BG', },
            { field: 'NewCode', title: 'Mã BG mới', template: '<input type="text" class="k-textbox" ng-model="dataItem.NewCode" style="width:100%"/>' },
            { field: 'NewName', title: 'Tên BG mới', template: '<input type="text" class="k-textbox" ng-model="dataItem.NewName" style="width:100%" />' }
        ]
    }

    $scope.price_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasPriceChoose = hasChoose;
    }

    $scope.Price_Add_Click = function ($event, win, vform) {
        $event.preventDefault()
        $scope.PriceLoadItem(0, win, vform)
    }

    $scope.Price_Detail_Click = function ($event, item) {
        $event.preventDefault();
        if ($scope.Item.TypeOfMode == 1) {
            $state.go('main.CUSContract.PriceCO', {
                ID: item.ID, ContractID: $scope.ID, CustomerID: _CUSCustomer_Contract.Const.CustomerID
            });
        } else {
            $state.go('main.CUSContract.PriceDI', {
                ID: item.ID, ContractID: $scope.ID, CustomerID: _CUSCustomer_Contract.Const.CustomerID
            });
        }
    }

    $scope.Price_Copy_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];;
        Common.Data.Each($.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true }), function (item) {
            data.push({
                ID: item.ID, Code: item.Code, NewCode: item.Code + "_Copy", NewName: item.Name + "_Copy"
            })
        })
        if (data.length > 0) {
            $scope.price_copy_gridOptions.dataSource.data(data);
            $timeout(function () {
                win.center().open();
            }, 1)
        }
    }

    $scope.Price_Edit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.PriceLoadItem(item.ID, win, vform)
    }

    $scope.PriceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Price_Get,
            data: { priceID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPrice = res;
                win.center().open();
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
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Price_Delete,
                    data: { ID: item.ID },
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

    $scope.Price_Copy_Submit_Click = function ($event, grid, win) {
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
                Msg: "Nhập mã mới cho các BD " + lstCode.join(", ")
            });
        } else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn sao chép các BG đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Price_Copy,
                        data: { data: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.price_gridOptions.dataSource.read();
                                win.close();
                            })
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            });
        }
    }

    $scope.Price_win_Submit_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Price_Save,
            data: { contractID: $scope.ID, item: $scope.ItemPrice },
            success: function (res) {
                $scope.price_gridOptions.dataSource.read();
                $rootScope.IsLoading = false;
                win.close();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

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

    $scope.cboTypeRunOptions = {
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
    });

    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfRunLevel,
        success: function (res) {
            if (res.length > 0) {
                $scope.cboTypeRunOptions.dataSource.data(res);
            }
        }
    });

    //#endregion

    //#region Action

    $scope.Update_Click = function ($event, vform) {
        $event.preventDefault();

        if (Common.HasValue($scope.Item.CustomerID) && $scope.Item.CustomerID > 0) {
            if (vform()) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $rootScope.IsLoading = false;
                            if (Common.HasValue($scope.CustomerID) && $scope.CustomerID > 0) {
                                $state.go('main.CUSCustomer.Contract', { ID: res, customerID: $scope.customerID }, { reload: true });
                            }
                            else {
                                $state.go('main.CUSCustomer.Contract', { ID: res, customerID: 0 }, { reload: true });
                            }
                        })
                    }
                });
            }
        }
        else
            $rootScope.Message({ Msg: "Không tìm thấy khách hàng" });
    }

    $scope.Delete_Click = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa hợp đồng?',
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Delete,
                    data: { ID: $scope.Item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            if (Common.HasValue($scope.customerID) && $scope.customerID > 0) {
                                $state.go('main.CUSCustomer.Detail', { id: $scope.customerID });
                            }
                            else {
                                $state.go('main.CUSContract.Index');
                            }
                        })
                    }
                });
            }
        })
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        if (Common.HasValue($scope.customerID) && $scope.customerID > 0) {
            $state.go('main.CUSCustomer.Detail', { id: $scope.customerID });
        }
        else {
            $state.go('main.CUSContract.Index');
        }
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
    //#endregion

    //#region GOP
    $scope.HasGOPChoose = false;

    $scope.gop_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.GroupOfProduct_List,
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
            $scope.term_gridOptions.dataSource.read();
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

            $rootScope.Message({
                Msg: "Xác nhận xóa?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.GroupOfProduct_Delete,
                        data: { lstid: lstid },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                grid.dataSource.read();
                                $rootScope.Message({ Msg: 'Đã xóa' });
                            });
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            });
        }
    };

    $scope.GOP_LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.GroupOfProduct_Get,
            data: { id: id, contractID: $scope.ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.GOPItem = res;
                        $scope.gop_cbo_ProductOptions.dataSource.data(_CUSCustomer_Contract.Data.ListProduct[res.GroupOfProductID]);
                        var datacbo = [];
                        datacbo.push({ ID: -1, ProductName: "" });
                        if (Common.HasValue(_CUSCustomer_Contract.Data.ListProduct[res.GroupOfProductIDChange])) {
                            $.each(_CUSCustomer_Contract.Data.ListProduct[res.GroupOfProductIDChange], function (i, v) {
                                datacbo.push(v);
                            });
                        }
                        $scope.gop_cbo_ProductChangeOptions.dataSource.data(datacbo);
                    }, 1);
                }
            }
        });

        win.center();
        win.open();
    };

    $scope.GOP_Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            if ($scope.GOPItem.ExpressionInput != "" && $scope.GOPItem.ExpressionInput != null) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.GroupOfProduct_Save,
                    data: { item: $scope.GOPItem, contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        win.close();
                        grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã cập nhật' });
                    }
                });
            }
            else {
                $rootScope.Message({
                    Msg: 'Công thức xét quy đổi không được rỗng.',
                    NotifyType: Common.Message.NotifyType.ERROR
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.GroupOfProduct_Check,
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

    $scope.CUSContractGroupOfProduct_Excel_Online = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_CUSCustomer_Contract.ExcelKey.ResourceGroupOfProduct + '_' + i];
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
            functionkey: _CUSCustomer_Contract.ExcelKey.GroupOfProduct,
            params: { contractID: $scope.ID },
            rowStart: 2,
            colCheckChange: 15,
            url: Common.Services.url.CUS,
            methodInit: _CUSCustomer_Contract.URL.GroupOfProduct_ExcelInit,
            methodChange: _CUSCustomer_Contract.URL.GroupOfProduct_ExcelChange,
            methodImport: _CUSCustomer_Contract.URL.GroupOfProduct_ExcelImport,
            methodApprove: _CUSCustomer_Contract.URL.GroupOfProduct_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.gop_gridOptions.dataSource.read();
            }
        });
    };
    //#endregion

    //#region AdddRouting
    $scope.KPI_Routing_Add_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadCUSRoutingItem(0, win, vform)
    };

    $scope.LoadCUSRoutingItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Get_Routing,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Read_LocationNotIn,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Read_AreaNotIn,
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
                    '<a href="/" ng-click="AreaNotInShowDetail_Click($event,AreaNotInDetail_win,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-info-circle"></i></a>' +
                    '<a href="/" ng-click="AreaLocation_Click($event,AreaLocation_win,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-map-marker"></i></a>',
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
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Refresh_RoutingArea,
                        data: { 'item': item },
                        success: function (res) {
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
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.Delete_Area,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Get_Area,
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
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Save_Area,
                        data: { item: $scope.AreaEditItem },
                        success: function (res) {
                            $scope.AreaNotIn_win_gridOptions.dataSource.read();
                            win.close()
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Read_AreaDetail,
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
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Delete_AreaDetail,
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
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Save_Area,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Get_AreaDetail,
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

            var data = _CUSCustomer_Contract.Data.Province[countryID];
            $scope.AreaDetailEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CUSCustomer_Contract.Data.District[provinceID];
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
            _CUSCustomer_Contract.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CUSCustomer_Contract.Data.Province[obj.CountryID]))
                    _CUSCustomer_Contract.Data.Province[obj.CountryID].push(obj);
                else _CUSCustomer_Contract.Data.Province[obj.CountryID] = [obj];
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
            _CUSCustomer_Contract.Data.District = {};
            Common.Data.Each(data, function (obj) {
                if (Common.HasValue(_CUSCustomer_Contract.Data.District[obj.ProvinceID])) {
                    _CUSCustomer_Contract.Data.District[obj.ProvinceID].push(obj);
                }
                else {
                    _CUSCustomer_Contract.Data.District[obj.ProvinceID] = [{ 'ID': -1, 'DistrictName': ' ' }, obj]

                }
            })
        }
    })

    $scope.AreaDetailEdit_winPopupSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Save_AreaDetail,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.Save_Routing,
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
    //#endregion 

    //#region area location
    $scope.CurrentAreaLocationID = -1;
    $scope.AreaLocationHasChoose = false;
    $scope.AreaLocationNotInHasChoose = false;

    $scope.AreaLocation_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.AreaLocation_List,
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
            { field: 'District', title: 'Quận huyện', width: '150px' },
            { field: 'TypeOfPartnerName', title: 'Loại partner', width: '150px' },
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
        $timeout(function () {
            $scope.AreaLocationNotIn_GridOptions.dataSource.read();
            $scope.AreaLocationNotIn_Grid.refresh();
        }, 300);
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
            pagesize:20,
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
            pagesize:20,
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
            pagesize:20,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.AreaLocation_NotInSave,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.AreaLocation_Delete,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Read_AreaNotIn,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.AreaLocation_Copy,
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

    $scope.Routing_Search_Click = function ($event, win) {
        $event.preventDefault();
        $scope.routing_search_gridOptions.dataSource.read();
        $timeout(function () { $scope.routing_search_grid.resize(); }, 1);
        win.center().open();
    };

    $scope.routing_search_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Routing_search_List,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.Routing_search_save,
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

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };
    //#endregion

    //#region Setting
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
                            url: Common.Services.url.CUS,
                            method: _CUSCustomer_Contract.URL.TypeOfRunLevelSave,
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
                    url: Common.Services.url.CUS,
                    method: _CUSCustomer_Contract.URL.TypeOfSGroupProductChangeSave,
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
        $.each(_CUSCustomer_Contract.Data.ListSetting, function (i, v) {
            if (v.ServiceOfOrderID == $scope.ParamService.ServiceOfOrderID) {
                v.Laden = $scope.ParamService.Laden;
                v.GetEmpty = $scope.ParamService.GetEmpty;
                v.ReturnEmpty = $scope.ParamService.ReturnEmpty;
            }
        });

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.contractSetting_save,
            data: { contractID: $scope.ID, lstsetting: _CUSCustomer_Contract.Data.ListSetting },
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.GOVList,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.GOVGet,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.GOVSave,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.GOVDeleteList,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.GOVNotInList,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.GOVNotInSave,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.LevelList,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.LevelGet,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.LevelSave,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.GOVListSetting,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.LevelDeleteList,
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

    //#region LTL level
    $scope.LTLLevelHasChoose = false;
    $scope.LTLLevel_Click = function ($event, win) {
        $event.preventDefault();
        $scope.price_level_win_GridOptions.dataSource.read()
        win.center().open();
    }

    $scope.price_level_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.LevelList,
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
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.LevelGet,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.LevelSave,
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
                url: Common.Services.url.CUS,
                method: _CUSCustomer_Contract.URL.LevelDeleteList,
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

    //#region Contract term
    $scope.term_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.Term_List,
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
                    PriceWarning: { type: 'number' },
                    RateMaterial: { type: 'number' },
                    RatePrice: { type: 'number' },
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        dataBound: function (e) {
            $rootScope.Loading.Change("Phụ lục hợp đồng ...", $rootScope.Loading.Progress + LoadingStep);
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        },
        columns: [
            {
                title: ' ', width: '200px',
                template: '<a href="/" ng-click="Term_Edit_Click($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-show="!dataItem.IsClosed && Auth.ActDel" ng-click="Term_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsWarning && !dataItem.IsClosed" ng-click="Term_Material_Click($event,dataItem)" class="k-button"><i class="fa fa-file"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsWarning && !dataItem.IsClosed && Auth.ActEdit" ng-click="Term_RemoveWarning_Click($event,dataItem)" class="k-button"><i class="fa fa-ban"></i></a>' +
                    '<a href="/" ng-show="!dataItem.IsClosed && Auth.ActEdit" ng-click="Term_Lock_Click($event,dataItem)" class="k-button"><i class="fa fa-unlock"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsClosed && Auth.ActEdit" ng-click="Term_Unlock_Click($event,dataItem)" class="k-button"><i class="fa fa-lock"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'ServiceOfOrderName', width: 100, title: 'Dịch vụ', filterable: { cell: { operator: 'contains', showOperators: false } } },
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
        ]
    };

    $scope.Term_RemoveWarning_Click = function myfunction($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn có muốn xóa cảnh báo?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Term_RemoveWarning,
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
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Term_Lock,
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
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Term_UnLock,
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

    $scope.Term_Delete_Click = function myfunction($event, data) {
        $event.preventDefault();
        debugger
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
                        url: Common.Services.url.CUS,
                        method: _CUSCustomer_Contract.URL.Term_Delete,
                        data: { ID: data.ID },
                        success: function (res) {
                            $scope.term_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            if (Common.HasValue($scope.CustomerID) && $scope.CustomerID > 0) {
                                $state.go('main.CUSCustomer.Contract', { ID: $scope.ID, customerID: $scope.customerID }, { reload: true });
                            }
                            else {
                                $state.go('main.CUSCustomer.Contract', { ID: $scope.ID, customerID: 0 }, { reload: true });
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
        $state.go('main.CUSContract.Term', $scope.ParamEdit);
    }

    $scope.Term_Edit_Click = function myfunction($event, data) {
        $event.preventDefault();
        var newParam = {
            TermID: data.ID,
            ContractID: $scope.ID
        }
        $state.go('main.CUSContract.Term', newParam);
    }

    $scope.Term_Material_Click = function ($event, data) {
        $event.preventDefault();
        $state.go('main.CUSContract.Material', { TermID: data.ID });
    }

    $scope.Price_History_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSCustomer_Contract.URL.PriceHistory,
            data: { cusId: $scope.Item.CustomerID, contractID: $scope.Item.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

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
                $.each(_CUSCustomer_Contract.Data.ListSetting, function (i, v) {
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
    //#endregion

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