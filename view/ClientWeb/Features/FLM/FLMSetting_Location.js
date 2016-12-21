/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSettingLocation = {
    URL: {
        Location_List: 'FLMSetting_Location_List',
        Location_SaveList: 'FLMSetting_Location_SaveList',
        Location_Delete: 'FLMSetting_Location_Delete',
        Location_NotInList: 'FLMSetting_Location_NotInList',
        Location_HasRun: 'FLMSetting_Location_HasRun',

        Location_ExcelInit: 'FLMSetting_Location_ExcelInit',
        Location_ExcelChange: 'FLMSetting_Location_ExcelChange',
        Location_ExcelImport: 'FLMSetting_Location_ExcelImport',
        Location_ExcelApprove: 'FLMSetting_Location_ExcelApprove',

        Routing_Contract_List: 'FLMSetting_Location_RoutingContract_List',
        Routing_Contract_SaveList: 'FLMSetting_Location_RoutingContract_SaveList',
        Routing_Contract_NewRoutingSave: 'FLMSetting_Location_RoutingContract_NewRoutingSave',
        Routing_Contract_ContractData: 'FLMSetting_Location_RoutingContract_ContractData',
        Routing_Contract_NewRoutingGet: 'FLMSetting_Location_RoutingContract_NewRoutingGet',
        Routing_Contract_NewAreaSave: 'FLMSetting_Location_RoutingContract_NewAreaSave',
        Routing_Contract_AreaList: 'FLMSetting_Location_RoutingContract_AreaList',
    },
    ExcelKey: {
        ResourceLocation: "FLMSetting_Location",
        Location:"FLMSetting_Location"
    }
}

angular.module('myapp').controller('FLMSetting_LocationCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FLMSetting_LocationCtrl');
    $rootScope.IsLoading = false;

    
    $scope.Auth = $rootScope.GetAuth();
    $scope.PartnerRouting_LocationID = -1;
    $scope.LocationNotInChoose = false;

    $scope.location_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingLocation.URL.Location_List,
            readparam: function () { return {  } },
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
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.location_DeleteClick = function ($event, data, grid) {
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
                    url: Common.Services.url.FLM,
                    method: _FLMSettingLocation.URL.Location_Delete,
                    data: { id: data.ID },
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

    $scope.location_ExcelClick = function ($event) {
        $event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_FLMSettingLocation.ExcelKey.ResourceLocation + '_' + i];
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
            functionkey: _FLMSettingLocation.ExcelKey.Location,
            params: {},
            rowStart: 1,
            colCheckChange: 10,
            url: Common.Services.url.FLM,
            methodInit: _FLMSettingLocation.URL.Location_ExcelInit,
            methodChange: _FLMSettingLocation.URL.Location_ExcelChange,
            methodImport: _FLMSettingLocation.URL.Location_ExcelImport,
            methodApprove: _FLMSettingLocation.URL.Location_ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.location_grid.dataSource.read();
            }
        });
    };

    $scope.location_searchClick = function ($event, win, grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.location_HasRunClick = function ($event) {
        $event.preventDefault();
        //URL Location_HasRun
    };

    $scope.locationNotIn_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingLocation.URL.Location_NotInList,
            readparam: function () { return { } },
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
    $scope.location_ChoosseClick = function ($event, win, grid) {
        $event.preventDefault();
        var data = [];
        angular.forEach(grid.dataSource.data(), function (obj, index) {
            if (obj.IsChoose)
                data.push(obj);
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSettingLocation.URL.Location_SaveList,
                data: { lst: data },
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
            url: Common.Services.url.FLM,
            method: _FLMSettingLocation.URL.Routing_Contract_List,
            readparam: function () { return {  locationid: $scope.PartnerRouting_LocationID } },
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
            method: _FLMSettingLocation.URL.Routing_Contract_SaveList,
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
            method: _FLMSettingLocation.URL.Routing_Contract_NewRoutingGet,
            data: {},
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
                method: _FLMSettingLocation.URL.Routing_Contract_NewRoutingSave,
                data: { item: $scope.NewItemRouting },
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
        method: _FLMSettingLocation.URL.Routing_Contract_ContractData,
        data: {   },
        success: function (res) {
            $scope.cboRoutingContract_Options.dataSource.data(res);
        }
    });

    $scope.PartnerRoutingArea_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingLocation.URL.Routing_Contract_AreaList,
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
            method: _FLMSettingLocation.URL.Routing_Contract_NewAreaSave,
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

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FLMSetting,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
}]);