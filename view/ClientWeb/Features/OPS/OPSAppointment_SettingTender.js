/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSSettingTender = {
    URL: {
        Setting_List: 'OPS_DI_Tendering_Setting_List',
        Setting_Save: 'OPS_DI_Tendering_Setting_Save',
        Setting_Get: 'OPS_DI_Tendering_Setting_Get',
        Setting_Delete: 'OPS_DI_Tendering_Setting_Delete',
        Location_List: 'OPS_DI_Tendering_Setting_Location_List',
        Routing_List: 'OPS_DI_Tendering_Setting_Routing_List',
        GroupLocation_List: 'OPS_DI_Tendering_Setting_GroupLocation_List',
        Customer_List: 'OPS_DI_Tendering_Setting_Customer_List',
        Vendor_List: 'OPS_DI_Tendering_Setting_Vendor_List',
        Packet_Save: 'OPS_DI_Tendering_Setting_Packet_Save',
    },
};

angular.module('myapp').controller('OPSAppointment_SettingTenderCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSSettingTenderCtrl');
    $rootScope.IsLoading = true;
    $scope.SettingTenderApplyChoose = false;
    $scope.item = {};
    $scope.DataTender = {};
    $scope.dataVendor = {};
    $scope.TabIndex = 1;
    $scope.name = "";
    $scope.dataExist = [];

    $scope.SettingTender_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Edit_Click($event,dataItem,SettingTenderEdit_win)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Name', title: 'Tên', width: '250px', template: '<a href="/" ng-click="Detail_Click($event,dataItem,SettingTender_win)">{{dataItem.Name}}</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: 'Ngày tạo', template: "#=CreateDate==null?' ':Common.Date.FromJsonDMYHM(CreateDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                }
            }
        ]
    };

    $scope.SettingTenderApply_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingTenderApply_grid,SettingTenderApplyChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '# if(IsUse == true){# <input class="chkChoose" type="checkbox" #= IsUse ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingTenderApply_grid,SettingTenderApplyChooseChange)" /> #}else{# <input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingTenderApply_grid,SettingTenderApplyChooseChange)" /> #}#',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Name', title: 'Tên', width: '250px', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: 'Ngày tạo', template: "#=CreateDate==null?' ':Common.Date.FromJsonDMYHM(CreateDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                }
            }
        ],
    };

    $scope.Apply_Save_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSSettingTender.URL.Packet_Save,
                data: { fID: $rootScope.FunctionItem.ID, data: lstid },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.SettingTenderApply_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.SettingTenderApplyChoose = false;
                    win.close();
                }
            });
        }

    }

    $scope.Location_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '55px',
                template: '<a href="/" ng-click="LocationDelete_Click($event)" class="k-button"><i class="fa fa-trash"></i></a>',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'LocationCode', title: 'Mã điểm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên điểm', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationAddress', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.Routing_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '55px',
                template: '<a href="/" ng-click="RoutingDelete_Click($event)" class="k-button"><i class="fa fa-trash"></i></a>',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.GroupLocation_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '55px',
                template: '<a href="/" ng-click="GroupLocationDelete_Click($event)" class="k-button"><i class="fa fa-trash"></i></a>',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'GroupLocationCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupLocationName', title: 'Tên',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ],
    };

    $scope.Customer_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '55px',
                template: '<a href="/" ng-click="CustomerDelete_Click($event)" class="k-button"><i class="fa fa-trash"></i></a>',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'CustomerCode', title: 'Mã KH', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Tên KH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.Vendor_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="VendorEdit_Click($event,dataItem,VenderEdit_win)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="VendorDelete_Click($event)" class="k-button"><i class="fa fa-trash"></i></a>',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'VendorCode', title: 'Mã đối tác', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: 'Tên đối tác', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RateTime', title: 'Tgian chấp nhận', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RateTimeConfirm', title: 'Tgian xác nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ],
    };

    $scope.LocationAdd_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.Location_List,
            readparam: function () {
                return {
                    dataExist: $scope.dataExist
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,LocationAdd_Grid,LocationChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,LocationAdd_Grid,LocationChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'LocationCode', title: 'Mã điểm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên điểm', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationAddress', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.RoutingAdd_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.Routing_List,
            readparam: function () {
                return {
                    dataExist: $scope.dataExist
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,RoutingAdd_Grid,RoutingChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,RoutingAdd_Grid,RoutingChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.GroupLocationAdd_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.GroupLocation_List,
            readparam: function () {
                return {
                    dataExist: $scope.dataExist
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,GroupLocationAdd_Grid,GroupLocationChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,GroupLocationAdd_Grid,GroupLocationChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'GroupLocationCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupLocationName', title: 'Tên',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ],
    };

    $scope.CustomerAdd_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.Customer_List,
            readparam: function () {
                return {
                    dataExist: $scope.dataExist
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CustomerAdd_Grid,CustomerChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CustomerAdd_Grid,CustomerChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'CustomerCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Tên', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ],
    };

    $scope.VendorAdd_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.Vendor_List,
            readparam: function () {
                return {
                    dataExist: $scope.dataExist
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,VendorAdd_Grid,VendorChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,VendorAdd_Grid,VendorChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'VendorCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: 'Tên', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RateTime', title: 'Thời gian', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RateTimeConfirm', title: 'Xác nhận Thời gian', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ],
    };

    $scope.SettingApply_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadTender();
        $timeout(function () {
            $scope.SettingTenderApply_grid.resize();
        });
        win.center().open();
    }

    $scope.LocationChooseChange = function ($event, grid, haschoose) {
        $scope.LocationChoose = haschoose;
    };

    $scope.SettingTenderApplyChooseChange = function ($event, grid, haschoose) {
        $scope.SettingTenderApplyChoose = haschoose;
    };

    $scope.RoutingChooseChange = function ($event, grid, haschoose) {
        $scope.RoutingChoose = haschoose;
    };

    $scope.GroupLocationChooseChange = function ($event, grid, haschoose) {
        $scope.GroupLocationChoose = haschoose;
    };

    $scope.CustomerChooseChange = function ($event, grid, haschoose) {
        $scope.CustomerChooseChoose = haschoose;
    };

    $scope.VendorChooseChange = function ($event, grid, haschoose) {
        $scope.VendorChooseChoose = haschoose;
    };

    $scope.resetform = function () {
        $scope.isLocation = false;
        $scope.isRouting = false;
        $scope.isListGroup = false;
        $scope.isCustomer = false;
        $scope.isVendor = false;
    };

    $scope.LoadTender = function () {
        $timeout(function () {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSSettingTender.URL.Setting_List,
                data: { fID: $rootScope.FunctionItem.ID },
                success: function (res) {
                    $scope.SettingTender_gridOptions.dataSource.data(res);
                    var data = [];
                    if (Common.HasValue(res)) {
                        $.each(res, function (i, v) {
                            v.IsChoose = false;
                            data.push(v);
                        });
                    }
                    $scope.SettingTenderApply_gridOptions.dataSource.data(data);
                    $rootScope.IsLoading = false;
                }
            });
        }, 1);
        $scope.resetform();
    };

    $scope.LoadTender();

    $scope.Add_Click = function ($event, win) {
        $event.preventDefault();

        $scope.item = {};
        $scope.item.ReferID = $rootScope.FunctionItem.ID;
        win.center().open();
    };

    $scope.Edit_Click = function ($event, data, win) {
        $event.preventDefault();

        $scope.item = data;
        win.center().open();
    };

    $scope.SaveEdit_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.Setting_Save,
            data: { item: $scope.item },
            success: function (res) {
                $scope.LoadTender();
                $rootScope.IsLoading = false;
                win.close();
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
            }
        });
    }

    $scope.Delete_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSSettingTender.URL.Setting_Delete,
                    data: { sID: data.ID },
                    success: function (res) {
                        $scope.LoadTender();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Msg: 'Đã xóa.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                    }
                });
            }
        });
    };

    $scope.Detail_Click = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.Setting_Get,
            data: { sID: data.ID },
            success: function (res) {
                $scope.DataTender = res;
                $scope.Location_Grid_Options.dataSource.data($scope.DataTender.ListLocation || []);
                $scope.Routing_Grid_Options.dataSource.data($scope.DataTender.ListRouting || []);
                $scope.GroupLocation_Grid_Options.dataSource.data($scope.DataTender.ListGroupLocation || []);
                $scope.Customer_Grid_Options.dataSource.data($scope.DataTender.ListCustomer || []);
                $scope.Vendor_Grid_Options.dataSource.data($scope.DataTender.ListVendor || []);

                win.center().open();

                $timeout(function () {
                    $scope.Routing_Grid.resize();
                    $scope.Location_Grid.resize();
                    $scope.GroupLocation_Grid.resize();
                    $scope.Customer_Grid.resize();
                    $scope.Vendor_Grid.resize();
                    $rootScope.IsLoading = false;
                }, 100);
            }
        })
    };

    $scope.win_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    };

    $scope.LocationAdd_Click = function ($event, win) {
        $event.preventDefault();
        $scope.resetform();
        $scope.isLocation = true;
        $scope.name = " địa chỉ";
        var data = $scope.Location_Grid_Options.dataSource.data();
        var result = [];
        if (Common.HasValue(data)) {
            $.each(data, function (i, v) {
                result.push(v.LocationID);
            });
        }
        $scope.dataExist = result;
        $scope.LocationAdd_Grid_Options.dataSource.read();
        $timeout(function () {
            $scope.LocationAdd_Grid.resize();
        });
        win.center().open();
    };

    $scope.RoutingAdd_Click = function ($event, win) {
        $event.preventDefault();
        $scope.resetform();
        $scope.isRouting = true;
        $scope.name = " cung đường";

        var data = $scope.Routing_Grid_Options.dataSource.data();
        var result = [];
        if (Common.HasValue(data)) {
            $.each(data, function (i, v) {
                result.push(v.RoutingID);
            });
        }
        $scope.dataExist = result;
        $scope.RoutingAdd_Grid_Options.dataSource.read();
        $timeout(function () {
            $scope.RoutingAdd_Grid.resize();
        });
        win.center().open();
    };

    $scope.GroupLocationAdd_Click = function ($event, win) {
        $event.preventDefault();
        $scope.resetform();
        $scope.isListGroup = true;
        $scope.name = " nhóm điểm";

        var data = $scope.GroupLocation_Grid_Options.dataSource.data();
        var result = [];
        if (Common.HasValue(data)) {
            $.each(data, function (i, v) {
                result.push(v.GroupLocationID);
            });
        }

        $scope.dataExist = result;
        $scope.GroupLocationAdd_Grid_Options.dataSource.read();
        $timeout(function () {
            $scope.GroupLocationAdd_Grid.resize();
        });
        win.center().open();
    };

    $scope.CustomerAdd_Click = function ($event, win) {
        $event.preventDefault();
        $scope.resetform();
        $scope.isCustomer = true;
        $scope.name = " khách hàng";
        var data = $scope.Customer_Grid_Options.dataSource.data();
        var result = [];
        if (Common.HasValue(data)) {
            $.each(data, function (i, v) {
                result.push(v.CustomerID);
            });
        }

        $scope.dataExist = result;
        $scope.CustomerAdd_Grid_Options.dataSource.read();
        $timeout(function () {
            $scope.CustomerAdd_Grid.resize();
        });
        win.center().open();
    };

    $scope.VendorAdd_Click = function ($event, win) {
        $event.preventDefault();
        $scope.resetform();
        $scope.isVendor = true;
        $scope.name = " đối tác";
        var data = $scope.Vendor_Grid_Options.dataSource.data();
        var result = [];
        if (Common.HasValue(data)) {
            $.each(data, function (i, v) {
                result.push(v.VendorID);
            });
        }
        $scope.dataExist = result;
        $scope.VendorAdd_Grid_Options.dataSource.read();
        $timeout(function () {
            $scope.VendorAdd_Grid.resize();
        });
        win.center().open();
    };

    $scope.FormSaveEdit_Click = function ($event, win) {
        $event.preventDefault();

        var data = [];
        switch ($scope.TabIndex) {
            case 1:
                var item = $.extend(true, [], $scope.Location_Grid_Options.dataSource.data());
                $.each($scope.LocationAdd_Grid_Options.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true)
                        item.push(v);
                });
                $scope.Location_Grid_Options.dataSource.data(item);
                break;
            case 2:
                var item = $.extend(true, [], $scope.Routing_Grid_Options.dataSource.data());
                $.each($scope.RoutingAdd_Grid_Options.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true)
                        item.push(v);
                });
                $scope.Routing_Grid_Options.dataSource.data(item);
                break;
            case 3:
                var item = $.extend(true, [], $scope.GroupLocation_Grid_Options.dataSource.data());
                $.each($scope.GroupLocationAdd_Grid_Options.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true)
                        item.push(v);
                });
                $scope.GroupLocation_Grid_Options.dataSource.data(item);
                break;
            case 4:
                var item = $.extend(true, [], $scope.Customer_Grid_Options.dataSource.data());
                $.each($scope.CustomerAdd_Grid_Options.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true)
                        item.push(v);
                });
                $scope.Customer_Grid_Options.dataSource.data(item);
                break;
            case 5:
                var item = $.extend(true, [], $scope.Vendor_Grid_Options.dataSource.data());
                $.each($scope.VendorAdd_Grid_Options.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true)
                        item.push(v);
                });
                $scope.Vendor_Grid_Options.dataSource.data(item);
                break;
        }
        win.close();
    };

    $scope.LocationDelete_Click = function ($event) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        $scope.Location_Grid.removeRow(tr);
    };

    $scope.RoutingDelete_Click = function ($event) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        $scope.Routing_Grid.removeRow(tr);
    };

    $scope.GroupLocationDelete_Click = function ($event) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        $scope.GroupLocation_Grid.removeRow(tr);
    };

    $scope.CustomerDelete_Click = function ($event) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        $scope.Customer_Grid.removeRow(tr);
    };

    $scope.VendorDelete_Click = function ($event) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        $scope.Vendor_Grid.removeRow(tr);
    };

    $scope.VendorEdit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.dataVendor = data;
        win.center().open();
    }

    $scope.VenderSaveEdit_Click = function ($event, win) {
        $event.preventDefault();
        $.each($scope.Vendor_Grid_Options.dataSource.data(), function (i, v) {
            if (v.VendorID == $scope.dataVendor.VendorID) {
                v.RateTime = $scope.dataVendor.RateTime;
                v.RateTimeConfirm = $scope.dataVendor.RateTimeConfirm;
            }
        });
        $scope.Vendor_Grid_Options.dataSource.data($scope.Vendor_Grid_Options.dataSource.data());
        win.close();
    }

    $scope.SaveGrid_Click = function ($event, win) {
        $event.preventDefault();

        var index = 1;
        $.each($scope.Vendor_Grid_Options.dataSource.data(), function (i, v) {
            v.SortOrder = index;
            index++;
        });
        $scope.DataTender.ListLocation = $scope.Location_Grid_Options.dataSource.data();
        $scope.DataTender.ListRouting = $scope.Routing_Grid_Options.dataSource.data();
        $scope.DataTender.ListGroupLocation = $scope.GroupLocation_Grid_Options.dataSource.data();
        $scope.DataTender.ListCustomer = $scope.Customer_Grid_Options.dataSource.data();
        $scope.DataTender.ListVendor = $scope.Vendor_Grid_Options.dataSource.data();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSSettingTender.URL.Setting_Save,
            data: { item: $scope.DataTender },
            success: function (res) {
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
                win.close();
            }
        });
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.Index");
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);
