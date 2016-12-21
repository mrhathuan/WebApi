/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _CUSDetail = {
    URL: {
        CusGet: "Customer_Get",
        CusUpdate: "Customer_Update",
        CusDestroy: "Customer_Destroy",
        CusCreateSeaPort: "SeaportCarrier_SaveAll",
        CatFieldRead: "CATField_List",
        CatTypeBusinessRead: "CATTypeBusiness_List",
        CatScaleRead: "CATScale_List",
        ContactGet: "Contact_Get",
        ContactRead: "Contact_Read",
        ContactUpdate: "Contact_Update",
        ContactDestroy: "Contact_Destroy",
        CompanyRead: "Company_Read",
        CompanyDelete: "Company_Delete",
        CompanyNotInRead: "CompanyNotInList_Read",
        CompanyNotInSave: "CompanyNotInList_Save",
        GOPRead: "GroupOfProduct_Read",
        GOPUpdate: "GroupOfProduct_Update",
        GOPDestroy: "GroupOfProduct_Destroy",
        GOPAllRead: "GroupOfProductAll_Read",
        GOPReset: "GroupOfProduct_ResetPrice",
        GOPGet: "GroupOfProduct_Get",
        ProductGet: "Product_Get",
        ProductRead: "Product_Read",
        ProductUpdate: "Product_Update",
        ProductDestroy: "Product_Destroy",
        ProductExport: "CUS_Product_Export",
        ProductCheck: "CUS_Product_Check",
        ProductImport: "CUS_Product_Import",
        RoutingRead: "Routing_List",
        RoutingDelete: "Routing_Delete",
        RoutingNotinRead: "RoutingNotIn_List",
        RoutingNotinSave: "RoutingNotIn_SaveList",
        StockRead: "Stock_Read",
        StockNotInRead: "StockNotIn_Read",
        StockSaveList: "Stock_SaveList",
        StockDestroy: "Stock_Destroy",
        StockUpdate: "Stock_Update",
        StockProduct_Read: "GroupOfProductInStock_List",
        StockProduct_Delete: "GroupOfProductInStock_DeleteList",
        ProductNotin_Read: "GroupOfProductNotInStock_List",
        ProductNotin_Save: "GroupOfProductNotInStock_SaveList",
        StockGet: "Stock_Get",
        PartnerUrlRead: "Partner_List",
        PartnerUrlUpdate: "Partner_Save",
        PartnerUrlDestroy: "Partner_Delete",
        PartnerLocationRead: "PartnerLocation_List",
        PartnerlocationUrlSaveList: "PartnerLocation_SaveList",
        PartnerUrlGet: "Partner_Get",
        Partner_Export: "CUS_PartnerLocation_Export",
        Partner_Check: "CUS_PartnerLocation_Check",
        Partner_Import: "CUS_PartnerLocation_Import",

        PartnerUrlSaveList: "Partner_SaveList",
        PartnerNotInUrlRead: "PartnerNotIn_Read",
        PartnerlocationUrlGet: "PartnerLocation_Get",
        PartnerlocationUrlDestroy: "PartnerLocation_Destroy",
        PartnerlocationUrlUpdate: "PartnerLocation_Save",
        PartnerLocation_SaveList: "PartnerLocation_SaveList",
        Routing_Reset: "Routing_Reset",
        CATPackingGOP: "ALL_CATPackingGOPTU",
        SettingInfoGet: "CUSSettingInfo_Get",
        SettingInfoSave: "CUSSettingInfo_Save",
        LocationList: "CUSSettingInfo_LocationList",

        Location_Check: "Location_Check",
        CustomerList: 'CUSContract_ByCustomerList',
        CompanyList: 'CUSContract_ByCompanyList',

        RoutingAreaList: 'Customer_Location_RoutingAreaList',
        RoutingAreaNotInList: 'Customer_Location_RoutingAreaNotInList',
        RoutingAreaNotInSave: 'Customer_Location_RoutingAreaNotInSave',
        RoutingAreaNotInDeleteList: 'Customer_Location_RoutingAreaNotInDeleteList',

        Product_ExcelInit: 'CUS_Product_ExcelInit',
        Product_ExcelChange: 'CUS_Product_ExcelChange',
        Product_ExcelImport: 'CUS_Product_ExcelImport',
        Product_ExcelApprove: 'CUS_Product_ExcelApprove',

        Partner_ExcelInit: 'CUS_Partner_ExcelInit',
        Partner_ExcelChange: 'CUS_Partner_ExcelChange',
        Partner_ExcelImport: 'CUS_Partner_ExcelImport',
        Partner_ExcelApprove: 'CUS_Partner_ExcelApprove',

        Partner_List: 'CUS_Partner_List',
        Partner_Get: 'CUS_Partner_Get',
        Partner_CUSLocationSaveCode: 'CUS_Partner_CUSLocationSaveCode',

        FilterByPartner_List: 'CUS_Partner_FilterByPartner_List',
        FilterByLocation_List: 'CUS_Partner_FilterByLocation_List',
        FilterByPartner_GetNum: 'CUS_Partner_FilterByPartner_GetNumOfCusLocation',

        Routing_Contract_List: 'CUS_Partner_RoutingContract_List',
        Routing_Contract_SaveList: 'CUS_Partner_RoutingContract_SaveList',
        Routing_Contract_NewRoutingSave: 'CUS_Partner_RoutingContract_NewRoutingSave',
        Routing_Contract_ContractData: 'CUS_Partner_RoutingContract_ContractData',
        Routing_Contract_NewRoutingGet: 'CUS_Partner_RoutingContract_NewRoutingGet',
        Routing_Contract_NewAreaSave: 'CUS_Partner_RoutingContract_NewAreaSave',
        Routing_Contract_AreaList: 'CUS_Partner_RoutingContract_AreaList',

        SettingGet: 'Customer_Setting_Get',
        SettingSave: 'Customer_Setting_Save',
        SettingGenerate: 'Customer_Generate_LocationArea',

        RoutingLocation_Contract_List: 'CUS_Partner_RoutingLocationContract_List',
        RoutingLocation_Contract_NewRoutingGet: 'CUS_Partner_RoutingLocationContract_NewRoutingGet',
        Routing_Contract_NewRoutingLocationSave: 'CUS_Partner_RoutingLocationContract_NewRoutingSave',
        Routing_Contract_LocationList: 'CUS_Partner_RoutingContract_LocationList',

        CUS_Partner_StockDock_List: 'CUS_Partner_StockDock_List',
        CUS_Partner_StockDock_Save: 'CUS_Partner_StockDock_Save',
        CUS_Partner_StockDock_Delete: 'CUS_Partner_StockDock_Delete',
        CUS_Partner_StockDock_Get: 'CUS_Partner_StockDock_Get',

    },
    Obj: {
        Country: [],
        Province: [],
        District: []
    },
    Data: {
        Country: [],
        Province: {},
        District: {},
        ProductImport: [],
        PartnerImport: [],
    },
    CookieName: {
        Partner: "CUSCustomerPartner"
    },
    ExcelKey: {
        Product: 'CUSDetail_CUSProduct',
        ResourceProduct: 'CUSDetail_CUSProduct',
        Partner_and_Stock: 'CUSDetail_Partner_Stock1',
    },
    Param: {
        CustomerID: -1
    }
}

angular.module('myapp').controller('CUSCustomer_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CUSCustomer_DetailCtrl');
    $rootScope.Loading.Show('Thông tin khách hàng');
    var LoadingStep = 20;
    $scope.CookiePartner = { customerid: 0, dataChoose: [] };
    $scope.CatDockData = null;
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.HasChooseSearch = false;
    $scope.PartnerHasChoose = false;
    $scope.LocationSearchHasChoose = false;
    $scope.FilterByPartnerHasChoose = false;
    $scope.FilterByLocationHasChoose = false;

    $scope.StockSearchHasChoose = false;
    $scope.StockProductHasChoose = false;
    $scope.StockProductSearchHasChoose = false;

    $scope.Routing_NotinHasChoose = false;
    $scope.AreaHasChoose = false;
    $scope.AreaSearchHasChoose = false;
    $scope.Area_LocationID = 0;
    $scope.typeOfPartnerNotin = 1;
    $scope.TabIndex = 1;
    $scope.TabIndexPartner = 1;
    $scope.TabIndexStock = 1;
    $scope.cuspartnerid = 0;
    $scope.ItemPartner = { ID: 0 };
    $scope.ListFilterPartnerIDChoose = [];
    $scope.ListFilterLocationIDChoose = [];
    $scope.ListFilterCusLocationIDChoose = [];
    $scope.IsFilterPartner = false;
    $scope.IsFilterLocation = false;
    $scope.IsFilterUseLocation = false;
    $scope.IsLocationFrom = false;
    $scope.StockTypeID = 4;
    $scope.locationID = 0;
    $scope.IsCheckNum = false;
    $scope.TotalNumLocation = false;

    $scope.PartnerRouting_LocationID=0;

    _CUSDetail.Param = $.extend(true, _CUSDetail.Param, $state.params);

    //#region var

    $scope.CusData = null;
    $scope.id = $stateParams.id;
    $scope.ContactData = null;
    $scope.FunctionName = "";
    $scope.gopID = 0;
    $scope.partnerID = 0;
    $scope.GopData = null;
    $scope.ProductData = null;
    $scope.StockData = { ID: 0 };
    $scope.PartnerData = null;
    $scope.LocationData = null;
    $scope.isDistributor = false;
    $scope.isCarrier = false;
    $scope.isSeaport = false;
    $scope.SettingInfo = null;
    $scope.Setting = null;
    $scope.customerid = 0;
    //#endregion

    //#region Auth
    if ($scope.id <= 0) {
        if (!$rootScope.CheckView("ActAdd", "main.CUSCustomer.Index")) return;
    }
    $scope.Auth = $rootScope.GetAuth();
    //#endregion


    //#region tab thông tin chung

    $scope.Location_Check = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Location_Check,
            data: { customerid: $scope.id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'ĐÃ CẬP NHẬT', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Main_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        navigatable : false,
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
                Common.Log($scope.TabIndex)
            }, 1);
        }
    };

    $scope.CusCountry_CbbOptions = {
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
                $scope.CusData.ProvinceID = -1;
                $scope.CusData.DistrictID = -1;
                $scope.CusData.WardID = "";
                $scope.LoadRegionData($scope.CusData);
            }
            else {
                $scope.CusData.CountryID = "";
                $scope.CusData.ProvinceID = "";
                $scope.CusData.DistrictID = "";
                $scope.CusData.WardID = "";
                $scope.LoadRegionData($scope.CusData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CUSDetail.Data.Country = data;
            $scope.CusCountry_CbbOptions.dataSource.data(data);
        }
    })

    $scope.CusProvince_CbbOptions = {
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
                $scope.CusData.DistrictID = -1;
                $scope.CusData.WardID = "";
                $scope.LoadRegionData($scope.CusData);
            }
            else {
                $scope.CusData.ProvinceID = "";
                $scope.CusData.DistrictID = "";
                $scope.CusData.WardID = "";
                $scope.LoadRegionData($scope.CusData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CUSDetail.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CUSDetail.Data.Province[obj.CountryID]))
                    _CUSDetail.Data.Province[obj.CountryID].push(obj);
                else _CUSDetail.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.CusDistrict_CbbOptions = {
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
                $scope.CusData.WardID = "";
                $scope.LoadRegionData($scope.CusData);
            }
            else {
                $scope.CusData.DistrictID = "";
                $scope.CusData.WardID = "";
                $scope.LoadRegionData($scope.CusData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CUSDetail.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CUSDetail.Data.District[obj.ProvinceID]))
                    _CUSDetail.Data.District[obj.ProvinceID].push(obj);
                else _CUSDetail.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CUSDetail.Data.Province[countryID];
            $scope.CusProvince_CbbOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CUSDetail.Data.District[provinceID];
            $scope.CusDistrict_CbbOptions.dataSource.data(data);
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
        }
        catch (e) { }
    }

    $scope.Cus_SaveAndApproved = function ($event, vform) {
        $event.preventDefault();
        $scope.CusData.IsApproved = true;
        $scope.Cus_Save($event, vform);
    }
    $scope.Cus_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.CusUpdate,
                data: { item: $scope.CusData },
                success: function (res) {
                    if (res.ID != $scope.id) {
                        $state.go('main.CUSCustomer.Detail', { id: res.ID }, { reload: true });
                    }
                    $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                },
                error: function (rs) {
                    $rootScope.IsLoading = false;
                }
            })
        }
    };

    $scope.Cus_Delete = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa dữ liệu đã chọn ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.CusDestroy,
                    data: { item: $scope.CusData },
                    success: function (res) {
                        $state.go('main.CUSCustomer.Index');
                    },
                    error: function (rs) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        });
    };

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSDetail.URL.CusGet,
        data: { id: $scope.id },
        success: function (res) {
            $scope.CusData = res;
            //$scope.LoadDataPartner('');
            $rootScope.IsLoading = false;
            $rootScope.Loading.Change("Thông tin chung ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Contact_GridOptions.dataSource.read();
        },
        error: function (e) {
            $rootScope.IsLoading = false;
        }
    })



    //#endregion

    //#region tab thiết lập
    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSDetail.URL.SettingGet,
        data: { customerid: $scope.id },
        success: function (res) {
            $scope.Setting = res;
            //$scope.LoadDataPartner('');
            $rootScope.IsLoading = false;
        },
        error: function (e) {
            $rootScope.IsLoading = false;
        }
    })


    $scope.Setting_Save_Click = function ($event) {
        $event.preventDefault();
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.SettingSave,
                data: { customerid: $scope.id, item: $scope.CusData, setting: $scope.Setting },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
    };

    $scope.Setting_Generate_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.SettingGenerate,
            data: { customerid: $scope.id },
            success: function (res) {
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.SUCCESS });
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };
    //#endregion


    //#region tab thông tin liên hệ
    $scope.Contact_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.ContactRead,
            readparam: function () { return { id: $scope.id } },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true }
                }
            }
        }),
        toolbar: kendo.template($('#contact-grid-toolbar').html()),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        editable: 'inline',
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Contact_Edit($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Contact_Delete($event,dataItem)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'FirstName', title: '{{RS.CUSCustomer.FirstName}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LastName', title: '{{RS.CUSCustomer.LastName}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PositionName', title: '{{RS.CUSCustomer.PositionName}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DepartmentName', title: '{{RS.CUSCustomer.DepartmentName}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TelNo', title: '{{RS.CUSCustomer.TelNo}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Celphone', title: '{{RS.CUSCustomer.Celphone}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Email', title: '{{RS.CUSCustomer.Email}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: '{{RS.CUSCustomer.Note}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin hợp đồng ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.GOP_TreeOptions.dataSource.read();
        }
    };

    $scope.Contact_Add = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.ContactGet,
            data: { id: 0 },
            success: function (res) {
                $scope.ContactData = res;
                $scope.Contact_Win.center().open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Contact_Edit = function ($event, data) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.ContactGet,
            data: { id: data.ID },
            success: function (res) {
                $scope.ContactData = res;
                $scope.Contact_Win.center().open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Contact_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.ContactUpdate,
                data: { item: $scope.ContactData, id: $scope.id },
                success: function (res) {
                    $scope.Contact_Grid.dataSource.read();
                    $scope.Contact_Win.close();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.Contact_Delete = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.ContactDestroy,
                    data: { id: data.ID },
                    success: function (res) {
                        $scope.Contact_Grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã xóa', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        });


    }
    //#endregion

    //#region tab thông tin hàng hóa
    $scope.Product_SplitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: true, resizable: true, size: '50%' },
            { collapsible: true, resizable: true, size: '50%' }
        ]
    };

    $scope.GOP_TreeOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.GOPRead,
            readparam: function () { return { id: $scope.id } },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number', nullable: true },
                    PriceOfGOPName: { type: 'string' },
                    ParentName: { type: 'string' },
                    ParentID: { type: 'number' },
                    HasReturn: { type: 'bool' },
                    IsDefault: { type: 'bool' }
                },
                expanded: false
            }
        }),
        toolbar: kendo.template($('#goproduct-treelist-toolbar').html()),
        height: '99%', filterable: false, sortable: true, selectable: true, editable: false,
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
                title: ' ', width: '84px',
                template: '<a href="/" ng-click="GOP_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a> <a href="/" ng-click="GOP_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CUSGroupOfProduct.Code}}', width: '125px',
            },
            {
                field: 'GroupName', title: '{{RS.CUSGroupOfProduct.GroupName}}', width: '125px',
            },
            {
                field: 'PriceOfGOPName', title: '{{RS.CUSGroupOfProduct.PriceOfGOPName}}', width: '125px',
            },
            {
                field: 'HasReturn', title: '{{RS.CUSGroupOfProduct.HasReturn}}', width: '80px', template: "<input disabled type='checkbox' #= HasReturn==true ? 'checked=checked' : '' # ></input>",
            },
            {
                field: 'IsDefault', title: '{{RS.CUSGroupOfProduct.IsDefault}}', width: '80px', template: "<input disabled type='checkbox' #= IsDefault==true ? 'checked=checked' : '' # ></input>",
            },
            {
                field: 'ParentName', title: '{{RS.CUSGroupOfProduct.ParentName}}',
            },
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin hàng hóa ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Partner_GridOptions.dataSource.read();
        }
    };

    $scope.Product_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.ProductRead,
            readparam: function () { return { gopID: $scope.gopID } },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ProductName: { type: 'string' },
                    PackingName: { type: 'string' },
                    IsKg: { type: 'bool' },
                    IsDefault: { type: 'bool' }
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        toolbar: kendo.template($('#product-grid-toolbar').html()),
        columns: [
            {
                title: ' ', width: '84px',
                template: '<a href="/" ng-click="Product_Edit($event)" class="k-button"><i class="fa fa-pencil"></i></a> <a href="/" ng-click="Product_Delete($event)" class="k-button" ng-show="Auth.ActDel"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã hàng hóa/đơn vị', width: '150px', },
            { field: 'ProductName', title: 'Tên hàng hóa/đơn vị', width: '150px', },
            { field: 'PackingCode', title: 'UOM', width: '100px', },
            {
                field: 'IsDefault', title: 'Hàng mặc định', width: '80px',
                template: "<input disabled type='checkbox' #= IsDefault ? 'checked=checked' : '' # ></input>",
            },
            {
                field: 'IsKg', title: 'Kg?', width: '80px',
                template: "<input disabled type='checkbox' #= IsKg ? 'checked=checked' : '' # ></input>",
            },
            { field: 'Length', title: 'Dài (cm)', template: '#=Length==null?" ":Common.Number.ToNumber3(Length)#', width: '100px', },
            { field: 'Width', title: 'Rộng (cm)', template: '#=Width==null?" ":Common.Number.ToNumber3(Width)#', width: '100px', },
            { field: 'Height', title: 'Cao (cm)', template: '#=Height==null?" ":Common.Number.ToNumber3(Height)#', width: '100px' },
            { field: 'CBM', title: 'Thể tích (cm3)', template: '#=CBM==null?" ":Common.Number.ToNumber3(CBM)#', width: '100px', },
            { field: 'Description', title: 'Mô tả', width: '100px', },
            { title: '', filterable: false, sortable: false }
        ]
    };

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
    };

    $scope.number_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3 }

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
    };

    $scope.Packing_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'Code',
        dataValueField: 'ID',
        index: 0,
        change: function (e) {
            if (!Common.HasValue(e.sender.dataItem(e.item)) || this.value() == "") {
                $rootScope.Message({ Msg: 'Dữ liệu không thể thiếu', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.ProductData.PackingID = "";
                this.open();
                this.select(0);
            }

        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    $scope.GOP_Add = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        if ($scope.PriceOfGOP_CbbOptions.dataSource.data().length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.GOPGet,
                data: { id: 0 },
                success: function (res) {
                    $scope.GopData = res;
                    $timeout(function () {
                        $scope.PriceOfGOP_Cbb.select(0);
                        $scope.GopData.PriceOfGOPID = $scope.PriceOfGOP_Cbb.value();
                    }, 100)
                    $scope.GOP_Win.center().open();
                    $rootScope.IsLoading = false;
                },
                error: function (rs) {
                    $rootScope.IsLoading = false;
                }
            })
        }
        else {
            $rootScope.IsLoading = false;
        }
    }

    $scope.GOP_Edit = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Thông tin";
        var grid = $scope.GOP_Tree;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.GOPGet,
            data: { id: item.ID },
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.GOPAllRead,
                    data: { request: "", id: $scope.id, gopID: $scope.gopID },
                    success: function (res) {
                        $scope.Parent_CbbOptions.dataSource.data(res.Data);
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
                $scope.GopData = res;
                $scope.GOP_Win.center().open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })

    }

    $scope.GOP_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.GopData.PriceOfGOPID != null && $scope.GopData.PriceOfGOPID != "" && $scope.GopData.PriceOfGOPID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.GOPUpdate,
                    data: { item: $scope.GopData, id: $scope.id },
                    success: function (res) {
                        $scope.GOP_Tree.dataSource.read();
                        $rootScope.IsLoading = false;
                        $scope.GOP_Win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
            else {
                $rootScope.Message({ Msg: 'Sai trường tính theo!', NotifyType: Common.Message.NotifyType.ERROR });
            }
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
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.GOPDestroy,
                    data: { item: item },
                    success: function (res) {
                        $scope.GOP_Tree.dataSource.read();
                        $scope.Product_Grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã xóa', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        $rootScope.IsLoading = false;
                    },
                    error: function (rs) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        });
    }

    $scope.GOP_Reset = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn cập nhật bảng giá?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.GOPReset,
                    data: { id: $scope.id },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (rs) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })

    }

    $scope.Product_Add = function ($event) {
        $event.preventDefault();

        $scope.FunctionName = "Thông tin";
        if ($scope.Packing_CbbOptions.dataSource.data().length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.ProductGet,
                data: { id: 0 },
                success: function (res) {
                    $scope.ProductData = {};
                    if (res != null)
                        $scope.ProductData = res;
                    $timeout(function () {
                        $scope.Packing_Cbb.select(0);
                        $scope.ProductData.PackingID = $scope.Packing_Cbb.value();
                    }, 100)
                    $scope.Product_Win.center().open();
                    $rootScope.IsLoading = false;
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            })
        }
        else {
            $rootScope.IsLoading = false;
        }

    }

    $scope.Product_Edit = function ($event) {
        $event.preventDefault();

        $scope.FunctionName = "Thông tin";
        var grid = $scope.Product_Grid;
        var item = grid.dataItem($($event.currentTarget).closest("tr"));
        if ($scope.Packing_CbbOptions.dataSource.data().length > 0) {
            $scope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.ProductGet,
                data: { id: item.ID },
                success: function (res) {
                    $scope.ProductData = res;
                    $scope.Product_Win.center().open();
                    $scope.IsLoading = false;
                },
                error: function (res) {
                    $scope.IsLoading = false;
                }
            })
        }
        else {
            $scope.IsLoading = false;
        }


    }

    $scope.Product_Save = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.gopID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.ProductUpdate,
                    data: { item: $scope.ProductData, gopID: $scope.gopID },
                    success: function (res) {
                        $scope.Product_Grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        $scope.Product_Win.close();

                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $scope.IsLoading = false;
                    }
                })
            }
            else {
                $rootScope.Message({ Msg: 'Bạn chưa chọn nhóm hàng hóa!', NotifyType: Common.Message.NotifyType.ERROR });
            }
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
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.ProductDestroy,
                    data: { item: item },
                    success: function (res) {
                        $scope.Product_Grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã xóa', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $scope.IsLoading = false;
                    }
                })
            }
        });
    }

    $scope.Product_OnlExcel = function ($event) {
        $event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 18; i++) {
            var resource = $rootScope.RS[_CUSDetail.ExcelKey.ResourceProduct + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }

        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã nhóm hàng] không được trống và > 50 ký tự',
                '[Tên nhóm hàng] không được trống và > 500 ký tự',
                '[Tính giá theo] không được trống',
                '[Tính giá theo] không tồn tại',
                '[Mã hàng] không được > 50 ký tự',
                '[Mã hàng] bị trùng trong cùng nhóm hàng',
                '[Tên hàng] không được trống và > 500 ký tự',
                '[UOM] không được trống',
                '[UOM] không tồn tại',
                '[Chiều dài] nhập sai ({0}_{1}) hoặc <0',
                '[Chiều rộng] nhập sai ({0}_{1}) hoặc <0',
                '[Chiều cao] nhập sai ({0}_{1}) hoặc <0',
                '[Thể tích(cm3)] nhập sai ({0}_{1}) hoặc <0',
                '[Cân nặng] nhập sai ({0}_{1}) hoặc <0',
                '[Mô tả] không được > 3000 ký tự',
                '[Nhiệt độ tối thiểu] nhập sai ({0}_{1})',
                '[Nhiệt độ tối đa] nhập sai ({0}_{1})',
            ];
        }

        var Productkey=_CUSDetail.ExcelKey.Product+"_"+$scope.id;
        $rootScope.excelShare.Init({
            functionkey: Productkey,
            params: { customerid: $scope.id },
            rowStart: 1,
            colCheckChange: 24,
            url: Common.Services.url.CUS,
            methodInit: _CUSDetail.URL.Product_ExcelInit,
            methodChange: _CUSDetail.URL.Product_ExcelChange,
            methodImport: _CUSDetail.URL.Product_ExcelImport,
            methodApprove: _CUSDetail.URL.Product_ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.GOP_TreeOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    }

    $scope.Product_Excel = function ($event, grid1, grid2, win) {
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
                    method: _CUSDetail.URL.ProductExport,
                    data: { customerID: $scope.id },
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.ProductCheck,
                    data: { file: e.FilePath, customerID: $scope.id },
                    success: function (data) {

                        _CUSDetail.Data.ProductImport = data;
                        var dataGrid = [];
                        Common.Data.Each(data, function (group) {
                            Common.Data.Each(group.ListProduct, function (pro) {
                                dataGrid.push({
                                    ExcelRow: pro.ExcelRow,
                                    ExcelSuccess: pro.ExcelSuccess,
                                    ExcelError: pro.ExcelError,
                                    GroupOfProductCode: group.Code,
                                    GroupOfProductName: group.Name,
                                    Code: pro.Code,
                                    ProductName: pro.ProductName,
                                })
                            })
                        })

                        $rootScope.IsLoading = false;
                        callback(dataGrid);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.ProductImport,
                    data: { data: _CUSDetail.Data.ProductImport, customerID: $scope.id },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        grid1.dataSource.read();
                        grid2.dataSource.read();
                    }
                })
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SLI_SYSVarPriceOfGOP,
        success: function (res) {
            $scope.PriceOfGOP_CbbOptions.dataSource.data(res);
            $rootScope.IsLoading = false;
        }
    });

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_CATPackingGOP,
        success: function (res) {
            $scope.Packing_CbbOptions.dataSource.data(res);
            $scope.IsLoading = false;
        }
    });


    //#endregion

    //#region tab cung đường (dang ẩn)
    $scope.Routing_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.RoutingRead,
            readparam: function () { return { id: $scope.id } },
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
        height: '99%', pageable: true, sortable: true, filterable: { mode: 'row' }, columnMenu: false, resizable: true,
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
            $scope.Contract_GridOptions.dataSource.read();
        }
    };

    $scope.Routing_NotinGridChange = function ($event, grid, haschoose) {
        $scope.Routing_NotinHasChoose = haschoose;
    };

    $scope.Routing_NotinGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.RoutingNotinRead,
            readparam: function () { return { id: $scope.id } },
            model: {
                id: 'ID',
                fields: {
                    IsChoose: { type: 'bool' },
                    ID: { type: 'number', editable: false, nullable: true },
                    EDistance: { type: 'number', editable: false, nullable: true },
                    EHours: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', filterable: { mode: 'row' }, sortable: true, pageable: true, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Routing_NotinGrid,Routing_NotinGridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Routing_NotinGrid,Routing_NotinGridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EDistance', title: 'Khoảng cách', width: 100,
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'EHours', title: 'Thời gian', width: 100,
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             {
                 title: ' ', filterable: false, sortable: false
             },
        ]
    };

    $scope.Routing_SaveList = function ($event) {
        $event.preventDefault();

        var grid = $scope.Routing_NotinGrid;
        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.RoutingNotinSave,
                data: { lst: data, id: $scope.id },
                success: function (res) {
                    $scope.Routing_Grid.dataSource.read();
                    $scope.Routing_NotinGrid.dataSource.read();
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
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.RoutingDelete,
                    data: { item: item },
                    success: function (res) {
                        $scope.Routing_Grid.dataSource.read();
                    }
                })
            }
        });
    }

    $scope.Routing_Search = function ($event) {
        $event.preventDefault();
        $scope.FunctionName = "Tìm kiếm cung đường";
        $scope.Routing_NotinGrid.dataSource.read();
        $scope.Routing_Win.center().open();
    }

    $scope.Routing_Refresh = function ($event) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Routing_Reset,
            data: { id: $scope.id },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        })
    }
    //#endregion

    //#region tab thiết lập (đang ẩn)
    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSDetail.URL.SettingInfoGet,
        data: { id: $scope.id },
        success: function (data) {
            $scope.SettingInfo = data;
        }
    });
    $scope.SelectLocation = function ($event, win) {
        $event.preventDefault();
        $scope.LocationGridOptions.dataSource.read();
        win.center();
        win.open();
    }

    $scope.SaveLocation = function ($event, win, grid) {
        $event.preventDefault();
        var row = grid.select();
        var data = grid.dataItem(row);

        if (data == null) {
            $rootScope.Message({ Msg: 'Chưa chọn điểm trả về', NotifyType: Common.Message.NotifyType.ERROR });
        } else {
            $scope.SettingInfo.LocationReturnID = data.ID;
            $scope.SettingInfo.LocationReturnCode = data.Code;
            $scope.SettingInfo.LocationReturnName = data.Location;
            $scope.SettingInfo.LocationReturnAddress = data.Address;

            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.SettingInfoSave,
                data: { item: $scope.SettingInfo, id: $scope.id },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
            win.close();
        }

    }
    //#endregion

    //#region tab hợp đồng
    $scope.Contract_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.CustomerList,
            readparam: function () { return { customerID: $scope.id } },
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
                field: 'ContractNo', width: 200, title: 'Số hợp đồng', template: "<a class='text' ng-click='DetailContract_Click($event, dataItem)' href='#=URL#'>#=ContractNo==null?\"\":ContractNo#</a>",
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
            $rootScope.Loading.Change("Hợp đồng ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.ContractVendor_GridOptions.dataSource.read();
        }
    };

    $scope.DetailContract_Click = function ($event, data) {
        $event.preventDefault();
        $state.go('main.CUSCustomer.Contract', { ID: data.ID, CustomerID: $scope.id });
    };

    $scope.Contract_Add_Click = function ($event) {
        $event.preventDefault();
        $state.go('main.CUSCustomer.Contract', { ID: 0, CustomerID: $scope.id });
    };

    //#endregion

    //#region tab hợp đồng vendor
    $scope.ContractVendor_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.CompanyList,
            readparam: function () { return { customerID: $scope.id } },
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
                field: 'ContractNo', width: 200, title: 'Số hợp đồng', template: "<a class='text' ng-click='ContractVendor_Click($event, dataItem)' href='#=URL#'>#=ContractNo==null?\"\":ContractNo#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DisplayName', width: 250, title: 'Tên hiển thị', template: "",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', width: 100, title: 'Dịch vụ',
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
            $rootScope.Loading.Change("Hợp đồng vendor ...", $rootScope.Loading.Progress + LoadingStep);
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    };

    $scope.ContractVendor_Click = function ($event, data) {
        $event.preventDefault();
        $state.go('main.VENVendor.Contract', { ID: data.ID, CustomerID: data.CustomerID });
    };
    //#endregion

    //#region tab danh muc địa chỉ
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
                                            url: Common.Services.url.CUS,
                                            method: _CUSDetail.URL.Partner_CUSLocationSaveCode,
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
                                        url: Common.Services.url.CUS,
                                        method: _CUSDetail.URL.Partner_CUSLocationSaveCode,
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
                                    url: Common.Services.url.CUS,
                                    method: _CUSDetail.URL.Partner_CUSLocationSaveCode,
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
                title: ' ', width: '130px', field: 'F_command',
                template: '<a href="/" ng-click="Partner_EditClick($event,dataItem,Partner_win,Stock_win)" ng-show="Auth.ActEdit&&dataItem.IsPartner" class="k-button"><i class="fa fa-pencil"></i></a>'+
                    '<a href="/" ng-click="Partner_RoutingClick($event,dataItem,routing_contract_win,routing_contract_grid)" ng-show="Auth.ActEdit&&!dataItem.IsPartner" class="k-button"><i class="fa fa-random"></i></a>' +
                    '<a href="/" ng-click="Partner_RoutingLocationClick($event,dataItem,routingLocation_contract_win,routingLocation_contract_grid)" ng-show="Auth.ActEdit&&!dataItem.IsPartner" class="k-button"><i class="fa fa-location-arrow"></i></a>' +
                    '<a href="/" ng-click="Partner_RoutingDockClick($event,dataItem,PartnerCATDock_win,PartnerCATDock_Grid)" ng-show="Auth.ActEdit&&!dataItem.IsPartner&&dataItem.TypeID == 4" class="k-button" style="border: 1px solid !important;">Dock</a>',
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
            $rootScope.Loading.Change("Danh mục địa chỉ ...", $rootScope.Loading.Progress + LoadingStep);
            $scope.Routing_GridOptions.dataSource.read();
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

    $scope.Partner_RoutingClick = function ($event,data, win, grid) {
        $event.preventDefault();

        $scope.PartnerRouting_LocationID = data.CATLocationID;
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.Partner_RoutingLocationClick = function ($event, data, win, grid) {
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
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.Partner_Get,
                data: { id: data.CUSPartnerID,typepartnerid:-1 },
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Partner_Get,
            data: { id: 0,typepartnerid:-1 },
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
        {
            $scope.CookiePartner.customerid = $scope.id;
            $scope.CookiePartner.dataChoose = $scope.ListFilterPartnerIDChoose;
            Common.Cookie.Set(_CUSDetail.CookieName.Partner, JSON.stringify($scope.CookiePartner));
            lstCUSPartnerID = $scope.ListFilterPartnerIDChoose;
        }
        if ($scope.IsFilterLocation == true)
            lstCUSLocationID = $scope.ListFilterCusLocationIDChoose;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Partner_List,
            data: {
                customerid: $scope.id,
                lstPartner: lstCUSPartnerID,
                lstLocation: lstCUSLocationID,
                isUseLocation:isUseLocation
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
                        url: Common.Services.url.CUS,
                        method: _CUSDetail.URL.StockGet,
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
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.PartnerUrlUpdate,
                    data: { item: $scope.ItemPartner, customerid: $scope.id, typePartner: $scope.ItemPartner.TypeID },
                    success: function (res) {
                        $scope.LoadDataPartner('');
                        
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.CUS,
                            method: _CUSDetail.URL.Partner_Get,
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
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.PartnerUrlDestroy,
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
            var resource = $rootScope.RS[_CUSDetail.ExcelKey.Partner_and_Stock + '_' + i];
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
                '[XK hạn lưu container] nhập sai',
                '[XK hạn giữ rỗng] nhập sai',
                '[NK hạn lưu container] nhập sai',
                '[NK hạn giữ rỗng] nhập sai',
                '[TG đóng hàng container] nhập sai',
                '[TG dỡ hàng container] nhập sai',
                '[TG đóng hàng phân phối] nhập sai',
                '[TG dỡ hàng phân phối] nhập sai',
                '[T/g b.hành (tháng)] nhập sai',
                '[Ngày kết thúc BH] nhập sai',
                '[Thông số kỹ thuật] nhập sai'
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _CUSDetail.ExcelKey.Partner_and_Stock,
            params: { customerid: $scope.id },
            rowStart: 1,
            colCheckChange: 30,
            url: Common.Services.url.CUS,
            methodInit: _CUSDetail.URL.Partner_ExcelInit,
            methodChange: _CUSDetail.URL.Partner_ExcelChange,
            methodImport: _CUSDetail.URL.Partner_ExcelImport,
            methodApprove: _CUSDetail.URL.Partner_ExcelApprove,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.PartnerNotInUrlRead,
            readparam: function () { return { id: $scope.id, typePartner: $scope.typeOfPartnerNotin } },
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
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.PartnerUrlSaveList,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.PartnerLocationRead,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.PartnerlocationUrlGet,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.PartnerlocationUrlGet,
            data: { locationID: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.LocationData = res;
                win.center().open();
            }
        })
    }

    $scope.Location_Save = function ($event, vform, win,winroute,gridroute) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.PartnerlocationUrlUpdate,
                data: { item: $scope.LocationData, cuspartnerID: $scope.ItemPartner.ID },
                success: function (res) {
                    $scope.LoadDataPartner("");
                    $scope.PartnerLocation_GridOptions.dataSource.read();
                    debugger
                    if ($scope.LocationData.LocationID != res)
                    {
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
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.PartnerlocationUrlDestroy,
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
            url: Common.Services.url.CUS,
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
                url: Common.Services.url.CUS,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.StockRead,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.StockGet,
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
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.StockUpdate,
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
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.StockDestroy,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.StockGet,
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
            var val=cbo.value();
            if (e.sender.selectedIndex >= 0) {
                if (val < 4) {
                    var data = cbo.dataItem(cbo.select());
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSDetail.URL.Partner_Get,
                        data: { id: 0,typepartnerid:val },
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.StockNotInRead,
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
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.StockSaveList,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.RoutingAreaNotInList,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.StockProduct_Read,
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
                        url: Common.Services.url.CUS,
                        method: _CUSDetail.URL.StockProduct_Delete,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.ProductNotin_Read,
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
                        url: Common.Services.url.CUS,
                        method: _CUSDetail.URL.ProductNotin_Save,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.RoutingAreaList,
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.RoutingAreaNotInList,
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
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.RoutingAreaNotInDeleteList,
                data: { lstAreaID: lstAreaID, locationID: $scope.Area_LocationID },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSDetail.URL.RoutingAreaList,
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
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.RoutingAreaNotInSave,
                data: { lstAreaID: lstAreaID, locationID: $scope.Area_LocationID },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSDetail.URL.RoutingAreaList,
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
    $scope.Partner_FilterByPartner = function ($event, win,grid) {
        $event.preventDefault();
        if ($scope.IsFilterPartner) {
            $scope.IsFilterPartner = false;
            $scope.LoadDataPartner("Đã cập nhật", win);
        }
        else {
            $scope.ListFilterPartnerIDChoose = [];
            var strCookie = Common.Cookie.Get(_CUSDetail.CookieName.Partner);
            if (Common.HasValue(strCookie) && strCookie != '') {
                try {
                    var objCookie = eval('[' + strCookie + ']')[0];
                    if (objCookie.customerid == $scope.id) {
                        $scope.ListFilterPartnerIDChoose = objCookie.dataChoose;
                    }
                } catch (e) { }
            }
            win.center();
            win.open();
            grid.dataSource.read();

            //$rootScope.IsLoading = true;
            //Common.Services.Call($http, {
            //    url: Common.Services.url.CUS,
            //    method: _CUSDetail.URL.FilterByPartner_GetNum,
            //    data: { customerid: $scope.id },
            //    success: function (res) {
            //        $rootScope.IsLoading = false;
            //        win.center();
            //        win.open();
            //        if(res.length>0)
            //        $scope.ListFilterPartnerIDChoose = res;
            //        grid.dataSource.read();

            //    },
            //    error: function (res) {
            //        $rootScope.IsLoading = false;
            //    }
            //});
        }
    };

    $scope.filterByPartner_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.FilterByPartner_List,
            readparam: function () { return {customerid: $scope.id} },
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

    $scope.ApplyFilterByPartner_Click = function ($event, win,grid) {
        $event.preventDefault();
        $scope.IsFilterPartner = true;
        $scope.LoadDataPartner("Đã cập nhật",win);
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.FilterByLocation_List,
            readparam: function () { return { customerid: $scope.id } },
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Routing_Contract_List,
            readparam: function () { return { customerid: $scope.id, locationid: $scope.PartnerRouting_LocationID } },
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

    $scope.PartnerRouting_SaveClick = function ($event, win,grid) {
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Routing_Contract_SaveList,
            data: { lstClear: lstClear, lstAdd: lstAdd ,locationid:$scope.PartnerRouting_LocationID},
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
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Routing_Contract_NewRoutingGet,
            data: { customerid: $scope.id },
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
        if(vform())
        {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.Routing_Contract_NewRoutingSave,
                data: { item: $scope.NewItemRouting, customerid: $scope.id },
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
        url: Common.Services.url.CUS,
        method: _CUSDetail.URL.Routing_Contract_ContractData,
        data: { customerid: $scope.id },
        success: function (res) {
            $scope.cboRoutingContract_Options.dataSource.data(res);
        }
    });

    $scope.PartnerRoutingArea_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Routing_Contract_AreaList,
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
                template:'<a href="/" ng-click="ChooseArea_ChooseClick($event,dataItem,PartnerRoutingArea_win)"  class="k-button"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã khu vực', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaName', title: 'Tên khu vực', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.ChooseArea_Click = function ($event, win,grid,type) {
        $event.preventDefault();
        $scope.IsLocationFrom = type;
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.ChooseArea_ChooseClick = function ($event, data,win) {
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
            AreaName:""
        }

        win.center();
        win.open();
    };

    $scope.PartnerNewArea_SaveClick = function ($event, win,vform) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Routing_Contract_NewAreaSave,
            data: { locationid: $scope.PartnerRouting_LocationID,item:$scope.NewItemArea },
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

    //#region cung duong theo điểm
    $scope.routingLocation_contract_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.RoutingLocation_Contract_List,
            readparam: function () { return { customerid: $scope.id, locationid: $scope.PartnerRouting_LocationID } },
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
            //{
            //    field: "IsCheckFrom", title: ' ', width: 82, attributes: { style: "text-align: center;" },
            //    template: '<input type="checkbox" ng-model="dataItem.IsCheckFrom" />',
            //    filterable: false
            //},
            { field: 'LocationFromCode', title: 'Mã điểm đi', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', title: 'Tên điểm đi', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            //{
            //    field: "IsCheckTo", title: ' ', width: 82, attributes: { style: "text-align: center;" },
            //    template: '<input type="checkbox" ng-model="dataItem.IsCheckTo" />',
            //    filterable: false
            //},
            { field: 'LocationToCode', title: 'Mã điểm đến', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', title: 'Tên điểm đến', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.PartnerRoutingLocation_AddClick = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.RoutingLocation_Contract_NewRoutingGet,
            data: { customerid: $scope.id },
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

    $scope.PartnerNewRoutingLocation_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.Routing_Contract_NewRoutingLocationSave,
                data: { item: $scope.NewItemRouting, customerid: $scope.id },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã lưu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.routingLocation_contract_gridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.PartnerRoutingLocation_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.Routing_Contract_LocationList,
            readparam: function () { return { customerid: $scope.id } },
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
                template: '<a href="/" ng-click="ChooseLocation_ChooseClick($event,dataItem,PartnerRoutingLocation_win)"  class="k-button"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã điểm', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên điểm', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.ChooseLocation_Click = function ($event, win, grid, type) {
        $event.preventDefault();
        $scope.IsLocationFrom = type;
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.ChooseLocation_ChooseClick = function ($event, data, win) {
        $event.preventDefault();
        if ($scope.IsLocationFrom) {
            $scope.NewItemRouting.LocationFromID = data.ID;
            $scope.NewItemRouting.LocationFromCode_Name = data.Code + " - " + data.LocationName;
        }
        else {
            $scope.NewItemRouting.LocationToID = data.ID;
            $scope.NewItemRouting.LocationToCode_Name = data.Code + " - " + data.LocationName;
        }
        win.close();
    };

    //#endregion

    $scope.autoUpdateCusPartnerCode = function () {
        if (!Common.HasValue(ItemPartner.CUSCode) || $scope.ItemPartner.CUSCode == '') {
            $scope.ItemPartner.CUSCode = $scope.ItemPartner.CATCode
        }
    };

    $scope.autoUpdateCusStockCode = function () {
        if (!Common.HasValue(ItemPartner.CUSCode) || $scope.ItemPartner.CUSCode == '') {
            $scope.StockData.Code = $scope.StockData.CATCode
        }
    };
        
    //#endregion

    //#region common
    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };
    //#endregion

    //#region Options






    //#endregion

    //#region Event



    $scope.Partner_Excel = function ($event, grid, win, type) {
        $event.preventDefault();
        switch (type) {
            case 1:
                _CUSDetail.Data.isCarrier = true;
                _CUSDetail.Data.isSeaport = false;
                _CUSDetail.Data.isDistributor = false;
                break;
            case 2:
                _CUSDetail.Data.isCarrier = false;
                _CUSDetail.Data.isSeaport = true;
                _CUSDetail.Data.isDistributor = false;
                break;
            case 3:
                _CUSDetail.Data.isCarrier = false;
                _CUSDetail.Data.isSeaport = false;
                _CUSDetail.Data.isDistributor = true;
                break;
        }
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'PartnerCode', width: 150, title: 'Mã đối tác', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'PartnerName', width: 250, title: 'Tên đối tac', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'LocationCode', width: 250, title: 'Mã địa chỉ', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'LocationName', width: 250, title: 'Tên địa chỉ', filterable: { cell: { showOperators: false, operator: "contains" } } },
            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.Partner_Export,
                    data: { customerID: $scope.id, isCarrier: _CUSDetail.Data.isCarrier, isSeaport: _CUSDetail.Data.isSeaport, isDistributor: _CUSDetail.Data.isDistributor },
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.Partner_Check,
                    data: { file: e.FilePath, customerID: $scope.id, isCarrier: _CUSDetail.Data.isCarrier, isSeaport: _CUSDetail.Data.isSeaport, isDistributor: _CUSDetail.Data.isDistributor },
                    success: function (data) {

                        _CUSDetail.Data.PartnerImport = data;
                        var dataGrid = [];
                        Common.Data.Each(data, function (partner) {
                            Common.Data.Each(partner.ListLocation, function (location) {
                                dataGrid.push({
                                    ExcelRow: location.ExcelRow,
                                    ExcelSuccess: location.ExcelSuccess,
                                    ExcelError: location.ExcelError,
                                    PartnerCode: partner.CUSPartnerCode,
                                    PartnerName: partner.CUSPartnerName,
                                    LocationCode: location.CUSLocationCode,
                                    LocationName: location.CUSLocationName,

                                })
                            })
                        })

                        $rootScope.IsLoading = false;
                        callback(dataGrid);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSDetail.URL.Partner_Import,
                    data: { data: _CUSDetail.Data.PartnerImport, customerID: $scope.id, isCarrier: _CUSDetail.Data.isCarrier, isSeaport: _CUSDetail.Data.isSeaport, isDistributor: _CUSDetail.Data.isDistributor },
                    success: function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        _CUSDetail.Data.PartnerImport = [];
                        $rootScope.IsLoading = false;
                        grid.dataSource.read();
                    }
                })
            }
        })
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

    //#region Init



    $scope.Stock_CountryOptions = {}; $scope.Stock_ProvinceOptions = {}; $scope.Stock_DistrictOptions = {};
    $scope.CusCountry_CbbOptions = {}; $scope.CusProvince_CbbOptions = {}; $scope.CusDistrict_CbbOptions = {};
    $scope.Partner_CountryCbbOptions = {}; $scope.Partner_ProvinceCbbOptions = {}; $scope.Partner_DistrictCbbOptions = {};
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
    $scope.CreateCBB($scope.Stock_CountryOptions, $scope.Stock_ProvinceOptions, $scope.Stock_DistrictOptions, 'Stock_Country', 'Stock_Province', 'Stock_District', _CUSDetail.Obj);
    $scope.CreateCBB($scope.CusCountry_CbbOptions, $scope.CusProvince_CbbOptions, $scope.CusDistrict_CbbOptions, 'CusCountry_Cbb', 'CusProvince_Cbb', 'CusDistrict_Cbb', _CUSDetail.Obj);
    $scope.CreateCBB($scope.Partner_CountryCbbOptions, $scope.Partner_ProvinceCbbOptions, $scope.Partner_DistrictCbbOptions, 'Partner_CountryCbb', 'Partner_ProvinceCbb', 'Partner_DistrictCbb', _CUSDetail.Obj);
    $scope.CreateCBB($scope.Location_CountryCbbOptions, $scope.Location_ProvinceCbbOptions, $scope.Location_DistrictCbbOptions, 'Location_CountryCbb', 'Location_ProvinceCbb', 'Location_DistrictCbb', _CUSDetail.Obj);
    //#endregion

    //#region CATDock
    $scope.Partner_RoutingDockClick = function ($event,data, win, grid) {
        $event.preventDefault();
        $scope.locationID = data.CATLocationID;                     
        win.center().open();
        grid.dataSource.read();
    }

    $scope.PartnerCATDock_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.CUS_Partner_StockDock_List,
            readparam: function () { return { locationid: $scope.locationID } },
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
                title: ' ', width: '84px',
                template: '<a href="/" ng-click="CATDock_EditClick($event,dataItem,PartnerCATDockAdd_win)"  class="k-button"><i class="fa fa-pencil"></i></a>'+
                    '<a href="/" ng-click="CATDock_DeleteClick($event,dataItem)"  class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DockName', title: 'Tên', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }
    $scope.CATDock_EditClick = function ($event, dataItem, win) {
        $event.preventDefault();
        $scope.LoadCatDock(dataItem.ID, win);
    }

    $scope.PartnerCATDock_AddClick = function($event, win){
        $event.preventDefault();
        $scope.LoadCatDock(-1, win);
    }

    $scope.LoadCatDock = function (ID, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.CUS_Partner_StockDock_Get,
            data: { id : ID },
            success: function (res) {
                $scope.CatDockData = res;
                win.center().open();
                $rootScope.IsLoading = false;
            }
        })
    }
    $scope.PartnerCATDock_SaveClick = function($event, vform, win){
        $event.preventDefault();
        $rootScope.IsLoading = true;
        if (vform) {
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSDetail.URL.CUS_Partner_StockDock_Save,
                data: { item: $scope.CatDockData, locationid: $scope.locationID },
                success: function (res) {
                    win.close();
                    $scope.PartnerCATDock_Grid.dataSource.read();
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $rootScope.IsLoading = false;
                }
            })
        }
        else {
            $rootScope.Message({
                Msg: 'Dữ liệu không được trống.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    }

    $scope.CATDock_DeleteClick = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSDetail.URL.CUS_Partner_StockDock_Delete,
            data: { item: data},
            success: function (res) {
                $scope.PartnerCATDock_Grid.dataSource.read();
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
                $rootScope.IsLoading = false;
            }
        })
    }
    //#endregion
}]);