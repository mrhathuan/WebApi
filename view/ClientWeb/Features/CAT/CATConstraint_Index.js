/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATConstraint = {
    URL: {
        Read: 'CATConstraint_List',
        Get: 'CATConstraint_Get',
        Save: 'CATConstraint_Save',
        Delete: 'CATConstraint_Delete',

        Read_Grid_Route: 'CATConstraint_Route_List',
        Delete_Grid_Route: 'CATConstraint_Route_Delete',
        Read_Route_NotIn: 'CATConstraint_RouteNotIn_List',
        Save_Route_NotIn: 'CATConstraint_RouteNotIn_Save',

        Read_Grid_Location: 'CATConstraint_Location_List',
        Delete_Grid_Location: 'CATConstraint_Location_Delete',
        Read_Location_NotIn: 'CATConstraint_LocationNotIn_List',
        Save_Location_NotIn: 'CATConstraint_LocationNotIn_Save',

        Read_Grid_Truck: 'CATConstraint_Truck_List',
        Read_Grid_Tractor: 'CATConstraint_Tractor_List',
        Delete_Grid_Vehicle: 'CATConstraint_Vehicle_Delete',// xài chung truck va tractor
        Read_Truck_NotIn: 'CATConstraint_TruckNotIn_List',
        Read_Tractor_NotIn: 'CATConstraint_TractorNotIn_List',
        Save_Vehicle_NotIn: 'CATConstraint_VehicleNotIn_Save',// xài chung truck va tractor

        Read_Weight_Grid: 'CATConstraint_Weight_List',
        Save_Weight: 'CATConstraint_Weight_Save',
        Get_Weight: 'CATConstraint_Weight_Get',
        Delete_Weight: 'CATConstraint_Weight_Delete',

        Read_OpenHour_Grid: 'CATConstraint_OpenHour_List',
        Save_OpenHour: 'CATConstraint_OpenHour_Save',
        Get_OpenHour: 'CATConstraint_OpenHour_Get',
        Delete_OpenHour: 'CATConstraint_OpenHour_Delete',
        UpdateConstraint: 'CATConstraint_UpdateConstraint',

        Read_KM_Grid: 'CATConstraint_KM_List',
        Save_KM: 'CATConstraint_KM_Save',
        Get_KM: 'CATConstraint_KM_Get',
        Delete_KM: 'CATConstraint_KM_Delete',
    }
}

angular.module('myapp').controller('CATConstraint_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATConstraint_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = { ID: -1 };

    $scope.HasChooseRoute = false;
    $scope.HasChooseLocation = false;
    $scope.HasChooseTruck = false;
    $scope.HasChooseTractor = false;

    $scope.HasChoose = false;

    $scope.Constraint_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read,
            readparam: function () { return {} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsApproved: { type: 'boolean' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '84px', filterable: false, sortable: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Constraint_Grid,ConstraintGridChoose_Change)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Constraint_Grid,ConstraintGridChoose_Change)" />' + '<a href="/" ng-click="CATConstraint_EditClick($event,Constraint_win,Constraint_Grid)" class="k-button"><i class="fa fa-pencil"></i></a>',
            },
            { title: '{{RS.CATConstraint.Code}}', field: 'Code', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '{{RS.CATConstraint.ConstraintName}}', field: 'ConstraintName', width: 210, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                title: '{{RS.CATConstraint.IsApproved}}', field: 'IsApproved', width: 210, template: '<input type="checkbox" #=IsApproved?checked="checked":""# disabled></input>',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đúng', Value: true }, { Text: 'Sai', Value: false }, { Text: 'All', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    };

    $scope.ConstraintGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.Update_Constrain_Click = function ($event, grid) {
        $event.preventDefault();
        var lstID = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                lstID.push(v.ID);
        });
        if (lstID.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.UpdateConstraint,
                data: { lst:lstID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        grid.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.Constraint_AddRoute = function ($event, win) {
        $event.preventDefault();
        $scope.Constraint_win_Tab.select(0)
        $scope.LoadItem(win, 0);
    }

    $scope.CATConstraint_EditClick = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) { id = item.ID }
        $scope.LoadItem(win, id);
    }
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                $scope.Constraint_LocationGridOptions.dataSource.read();
                $scope.Constraint_OpenHourGridOptions.dataSource.read();
                $scope.Constraint_RouteGridOptions.dataSource.read();
                $scope.Constraint_WeightGridOptions.dataSource.read();
                $scope.Constraint_TractorGridOptions.dataSource.read();
                $scope.Constraint_TruckGridOptions.dataSource.read();
                $scope.Constraint_KMGridOptions.dataSource.read();
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    //#region Popup

    //#region tab1

    $scope.Constraint_win_Save = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {

            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Constraint_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }
    $scope.Constraint_win_Delete = function ($event, win) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATConstraint.URL.Delete,
                    data: { item: $scope.Item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.Constraint_GridOptions.dataSource.read();
                        win.close();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        })
    }
    $scope.Constraint_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }
    $scope.Constraint_win_TabOptions = { animation: { open: { effects: "fadeIn" } } }

    //#endregion

    //#region tab2
    $scope.Constraint_RouteGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Grid_Route,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="Constraint_RouteGrid_Delete($event,Constraint_RouteGrid)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { title: '{{RS.CATRouting.Code}}', field: 'RoutingCode', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '{{RS.CATRouting.RoutingName}}', field: 'RoutingName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Constraint_win_AddRoute = function ($event, win) {
        $event.preventDefault();
        $scope.Route_win_GridOptions.dataSource.read();
        win.center().open();
    }

    $scope.Constraint_RouteGrid_Delete = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.IsLoading = true;

            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Delete_Grid_Route,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Constraint_RouteGridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.Route_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Route_NotIn,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 20
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Route_win_Grid,Route_win_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Route_win_Grid,Route_win_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATRouting.Code}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: '{{RS.CATRouting.RoutingName}}', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Route_win_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChooseRoute = hasChoose;
    }

    $scope.Route_win_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        var lstid = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) lstid.push(o.ID);
        })
        if (lstid.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Save_Route_NotIn,
                data: { 'lst': lstid, id: $scope.Item.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Constraint_RouteGridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.Route_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region tab3
    $scope.Constraint_LocationGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Grid_Location,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="Constraint_LocationGrid_Delete($event,Constraint_LocationGrid)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { title: '{{RS.CATLocation.Code}}', field: 'LocationCode', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '{{RS.CATLocation.Location}}', field: 'LocationName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Quốc gia', field: 'CountryName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Tỉnh thành', field: 'ProvinceName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Quận huyện', field: 'DistrictName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Constraint_win_AddLocation = function ($event, win) {
        $event.preventDefault();
        $scope.Location_win_GridOptions.dataSource.read();
        win.center().open();
    }

    $scope.Constraint_LocationGrid_Delete = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.IsLoading = true;

            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Delete_Grid_Location,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Constraint_RouteGridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.Location_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Location_NotIn,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Location_win_Grid,Location_win_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Location_win_Grid,Location_win_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATLocation.Code}}', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: '{{RS.CATLocation.Location}}', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: '{{RS.CATLocation.Address}}', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Quốc gia', field: 'CountryName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Tỉnh thành', field: 'ProvinceName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Quận huyện', field: 'DistrictName', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.Location_win_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChooseLocation = hasChoose;
    }
    $scope.Location_win_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        var lstid = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) lstid.push(o.ID);
        })
        if (lstid.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Save_Location_NotIn,
                data: { 'lst': lstid, id: $scope.Item.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Constraint_LocationGridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    $scope.Location_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region tab4
    $scope.Constraint_TruckGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Grid_Truck,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="Constraint_TruckGrid_Delete($event,Constraint_TruckGrid,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { title: 'Số xe', field: 'VehicleNo', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Constraint_win_AddTruck = function ($event, win) {
        $event.preventDefault();
        $scope.Truck_win_GridOptions.dataSource.read();
        win.center().open();
    }

    $scope.Constraint_TruckGrid_Delete = function ($event, grid,data) {
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
                    url: Common.Services.url.CAT,
                    method: _CATConstraint.URL.Delete_Grid_Vehicle,
                    data: { 'item': data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.Constraint_TruckGridOptions.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
            
    }

    $scope.Truck_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Truck_NotIn,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Truck_win_Grid,Truck_win_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Truck_win_Grid,Truck_win_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RegNo', title: 'Số xe', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.Truck_win_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChooseTruck = hasChoose;
    }
    $scope.Truck_win_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        var lstid = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) lstid.push(o.ID);
        })
        if (lstid.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Save_Vehicle_NotIn,
                data: { 'lst': lstid, id: $scope.Item.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Constraint_TruckGridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    //#endregion

    //#region tab5
    $scope.Constraint_TractorGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Grid_Tractor,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="Constraint_TractorGrid_Delete($event,Constraint_TractorGrid,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { title: 'Số xe', field: 'VehicleNo', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Constraint_win_AddTractor = function ($event, win) {
        $event.preventDefault();
        $scope.Tractor_win_GridOptions.dataSource.read();
        win.center().open();
    }

    $scope.Constraint_TractorGrid_Delete = function ($event, grid, data) {
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
                    url: Common.Services.url.CAT,
                    method: _CATConstraint.URL.Delete_Grid_Vehicle,
                    data: { 'item': data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.Constraint_TractorGridOptions.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.Tractor_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Tractor_NotIn,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Tractor_win_Grid,Tractor_win_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Tractor_win_Grid,Tractor_win_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RegNo', title: 'Số xe', width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.Tractor_win_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChooseTractor = hasChoose;
    }
    $scope.Tractor_win_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        var lstid = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) lstid.push(o.ID);
        })
        if (lstid.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Save_Vehicle_NotIn,
                data: { 'lst': lstid, id: $scope.Item.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Constraint_TractorGridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    //#endregion

    //#region tab6
    $scope.Constraint_WeightGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_Weight_Grid,
            readparam: function () { return { id: $scope.Item.ID } },
            group: [{ field: "DateID" }],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    TimeFrom: { type: 'date' },
                    TimeTo: { type: 'date' },
                    Weight: { type: 'number' },
                    DateID: { type: 'number' },
                    NoOfDelivery: { type: 'number' },
                    IsOneContainer: { type: 'boolean' },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="Constraint_WeightGrid_Delete($event,Constraint_WeightGrid)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            {
                title: '{{RS.CATConstraintRequire.TimeFrom}}', field: 'TimeFrom', width: 200, template: '#=Common.Date.FromJsonHM(TimeFrom)#',
                filterable: { cell: { template: function (e) { e.element.kendoTimePicker({ format: Common.Date.Format.HM }); }, operator: 'equal', showOperators: false } },
            },
            {
                title: '{{RS.CATConstraintRequire.TimeTo}}', field: 'TimeTo', width: 200, template: '#=Common.Date.FromJsonHM(TimeTo)#',
                filterable: { cell: { template: function (e) { e.element.kendoTimePicker({ format: Common.Date.Format.HM }); }, operator: 'equal', showOperators: false } },
            },
            { title: '{{RS.CATConstraintRequire.Weight}}', field: 'Weight', width: 100, template: '#=Common.Number.ToNumber2(Weight)#', },
            { title: 'Chỉ nhận bốc hàng 1 cont', field: 'IsOneContainer', width: 100, template: '<input type="checkbox" ng-value="dataItem.IsOneContainer" disabled="disabled" />', },
            { title: 'Chiều dài mooc cho phép', field: 'NoOfDelivery', width: 100, template: '#=NoOfDelivery==null?"":Common.Number.ToNumber2(NoOfDelivery)#', },
            { title: '{{RS.CATConstraintRequire.DateID}}', field: 'DateID', width: 200, hidden: true, groupHeaderTemplate: '#=GetDateName(value)#' },
        ]
    }

    

    $scope.Constraint_WeightGrid_Delete = function ($event, grid) {
        $event.preventDefault();
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Delete_Weight,
                data: { item: item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Constraint_WeightGridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Constraint_win_AddWeight = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItemWeight(win, 0, vform)
    }


    $scope.Weight_win_cbDateNameOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local([], {
            id: 'Value',
            fields: {
                Text: { type: 'string' },
                Value: { type: 'number' },
            }
        })
    }

    $timeout(function () {
        $scope.Weight_win_cbDateNameOptions.dataSource.data([{ Text: 'Thứ 2', Value: 1 }, { Text: 'Thứ 3', Value: 2 }, { Text: 'Thứ 4', Value: 3 }, { Text: 'Thứ 5', Value: 4 },
        { Text: 'Thứ 6', Value: 5 }, { Text: 'Thứ 7', Value: 6 }, { Text: 'Chủ nhật', Value: 0 }])
    }, 200)

    $scope.Weight_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            if (!Common.HasValue($scope.ItemWeight.DateID)) {
                $rootScope.Message({ Msg: 'Chưa chọn thứ', NotifyType: Common.Message.NotifyType.ERROR });
                vform({ model: 'ItemWeight.DateID', error: 'Chưa chọn thứ' });
            }
            else if ($scope.ItemWeight.TimeTo <= $scope.ItemWeight.TimeFrom) {
                $rootScope.Message({ Msg: 'Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc', NotifyType: Common.Message.NotifyType.ERROR });
                vform({ model: 'ItemWeight.TimeFrom', error: 'Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc' });
            } else if ($scope.ItemWeight.Weight <= 0) {
                $rootScope.Message({ Msg: 'Trọng tải phải lớn hơn 0', NotifyType: Common.Message.NotifyType.ERROR });
                vform({ model: 'ItemWeight.Weight', error: 'Trọng tải phải lớn hơn 0' });
            }
            else {

                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATConstraint.URL.Save_Weight,
                    data: { item: $scope.ItemWeight, id: $scope.Item.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.Constraint_WeightGridOptions.dataSource.read();
                        win.close()
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.Weight_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }

    $scope.LoadItemWeight = function (win, id, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Get_Weight,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemWeight = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.numWeight_Option = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numNoOfDelivery_Option = { format: 'n0', spinners: false, culture: 'en-US', min: 1,max:2, step: 1, decimals: 0 }
    //#endregion

    //#region tab7
    $scope.Constraint_OpenHourGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_OpenHour_Grid,
            readparam: function () { return { id: $scope.Item.ID } },
            group: [{ field: "DateID" }],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    TimeFrom: { type: 'date' },
                    TimeTo: { type: 'date' },
                    Weight: { type: 'number' },
                    DateID: { type: 'number' },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="Constraint_OpenHourGrid_Delete($event,Constraint_OpenHourGrid)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            {
                title: '{{RS.CATConstraintRequire.TimeFrom}}', field: 'TimeFrom', width: 200, template: '#=Common.Date.FromJsonHM(TimeFrom)#',
                filterable: { cell: { template: function (e) { e.element.kendoTimePicker({ format: Common.Date.Format.HM }); }, operator: 'equal', showOperators: false } },
            },
            {
                title: '{{RS.CATConstraintRequire.TimeTo}}', field: 'TimeTo', width: 200, template: '#=Common.Date.FromJsonHM(TimeTo)#',
                filterable: { cell: { template: function (e) { e.element.kendoTimePicker({ format: Common.Date.Format.HM }); }, operator: 'equal', showOperators: false } },
            },
            { title: '{{RS.CATConstraintRequire.DateID}}', field: 'DateID', width: 200, hidden: true, groupHeaderTemplate: '#=GetDateName(value)#' },
        ]
    }
    $scope.Constraint_OpenHourGrid_Delete = function ($event, grid) {
        $event.preventDefault();
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Delete_OpenHour,
                data: { item: item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Constraint_OpenHourGridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Constraint_win_AddOpenHour = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItemOpenHour(win, 0, vform)
    }


    $scope.OpenHour_win_cbDateNameOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local([], {
            id: 'Value',
            fields: {
                Text: { type: 'string' },
                Value: { type: 'number' },
            }
        })
    }

    $timeout(function () {
        $scope.OpenHour_win_cbDateNameOptions.dataSource.data([{ Text: 'Thứ 2', Value: 1 }, { Text: 'Thứ 3', Value: 2 }, { Text: 'Thứ 4', Value: 3 }, { Text: 'Thứ 5', Value: 4 },
        { Text: 'Thứ 6', Value: 5 }, { Text: 'Thứ 7', Value: 6 }, { Text: 'Chủ nhật', Value: 0 }])
    }, 200)

    $scope.OpenHour_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            if (!Common.HasValue($scope.ItemOpenHour.DateID)) {
                $rootScope.Message({ Msg: 'Chưa chọn thứ', NotifyType: Common.Message.NotifyType.ERROR });
                vform({ model: 'ItemOpenHour.DateID', error: 'Chưa chọn thứ' });
            }
            else if ($scope.ItemOpenHour.TimeTo <= $scope.ItemOpenHour.TimeFrom) {
                $rootScope.Message({ Msg: 'Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc', NotifyType: Common.Message.NotifyType.ERROR });
                vform({ model: 'ItemOpenHour.TimeFrom', error: 'Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc' });
            }
            else {

                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATConstraint.URL.Save_OpenHour,
                    data: { item: $scope.ItemOpenHour, id: $scope.Item.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.Constraint_OpenHourGridOptions.dataSource.read();
                        win.close()
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.OpenHour_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }

    $scope.LoadItemOpenHour = function (win, id, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Get_OpenHour,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemOpenHour = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
    //#endregion

    //#region tab7
    $scope.Constraint_KMGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Read_KM_Grid,
            readparam: function () { return { id: $scope.Item.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    KMFrom: { type: 'number' },
                    KMScore: { type: 'number' },
                    KMTo: { type: 'number' },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="Constraint_KMGrid_Delete($event,Constraint_KMGrid)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            {
                title: 'KMFrom', field: 'KMFrom', width: 200, template: '#=Common.Number.ToNumber2(KMFrom)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                title: 'KMTo', field: 'KMTo', width: 200, template: '#=Common.Number.ToNumber2(KMTo)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                title: 'KMScore', field: 'KMScore', width: 200, template: '#=Common.Number.ToNumber2(KMScore)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
        ]
    }
    $scope.Constraint_KMGrid_Delete = function ($event, grid) {
        $event.preventDefault();
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATConstraint.URL.Delete_KM,
                data: { item: item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.Constraint_KMGridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Constraint_win_AddKM = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItemKM(win, 0, vform)
    }


    $scope.numKMFrom_Option = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numKMTo_Option = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numKMScore_Option = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.KM_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            if ($scope.ItemKM.KMFrom >= $scope.ItemKM.KMTo)
                $rootScope.Message({ Msg: 'KMTo phải lớn hơn KMFrom', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATConstraint.URL.Save_KM,
                    data: { item: $scope.ItemKM, id: $scope.Item.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.Constraint_KMGridOptions.dataSource.read();
                        win.close()
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.KM_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }

    $scope.LoadItemKM = function (win, id, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATConstraint.URL.Get_KM,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemKM = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
    //#endregion

    //#endregion
}])
var GetDateName = function (val) {
    var str = '';
    switch (val) {
        default:
            break;
        case 0: str = "Chủ nhật"; break;
        case 1: str = "Thứ Hai"; break;
        case 2: str = "Thứ Ba"; break;
        case 3: str = "Thứ Tư"; break;
        case 4: str = "Thứ Năm"; break;
        case 5: str = "Thứ Sáu"; break;
        case 6: str = "Thứ Bảy"; break;
    }
    return str;
}