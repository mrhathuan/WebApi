/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMFleet_Vehicle = {
    URL: {
        Read: 'FLMFLeetPlanning_List',
        Save: 'FLMFLeetPlanning_Save',
        Get: 'FLMFLeetPlanning_Get',
        Delete: 'FLMFLeetPlanning_Delete',

        DriverAll_List: 'FLMAsset_AllDriverRead',
        Vehicle_List: 'FLMVehicle_List',
        TypeOfDriver: 'ALL_SYSVarTypeOfDriver',
        ALL_Shift: 'ALL_CATShift',
    },
}

angular.module('myapp').controller('FLMFleetPlanning_VehicleCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMFleetPlanning_VehicleCtrl');

    $rootScope.IsLoading = false;
    $scope.Item = null;

    $scope.FLMFLeetPlanning_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleet_Vehicle.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="FLMFLeetPlanning_GridEdit_Click($event,dataItem, FLMFLeetPlanning_win,FLMFLeetPlanning_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="FLMFLeetPlanning_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'VehicleNo', title: 'Xe', width: 150, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'PlanningDate', title: 'Ngày lên kế hoạch', template: "#=PlanningDate==null?' ':Common.Date.FromJsonDMY(PlanningDate)#", width: 120,
                filterable: { cell: { showOperators: false, operator: "gte" } }
            },
            { field: 'ShiftName', title: 'Ca', width: 150, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'DriverName1', title: 'Tài xế 1', width: 150, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'DriverName2', title: 'Tài xế 2', width: 150, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'DriverName3', title: 'Tài xế 3', width: 150, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.FLMFLeetPlanning_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.FLMFLeetPlanningLoadItem(0, win, vform)
    }

    $scope.FLMFLeetPlanning_GridEdit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.FLMFLeetPlanningLoadItem(item.ID, win, vform)
    }

    $scope.FLMFLeetPlanningLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleet_Vehicle.URL.Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                vform({ clear: true })
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.FLMFLeetPlanning_GridDestroy_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa kế hoạch đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMFleet_Vehicle.URL.Delete,
                    data: { id: item.id },
                    success: function (res) {
                        $scope.FLMFLeetPlanning_Grid_Options.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.FLMFLeetPlanning_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        var error = false;
        if ($scope.Item.VehicleID <= 0) {
            $rootScope.Message({ Msg: 'Chưa chọn xe', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.DriverID1 > 0 && $scope.Item.TypeOfDriverID1 <= 0) {
            $rootScope.Message({ Msg: 'Chưa chọn loại tài xế 1', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.DriverID1 <= 0 && $scope.Item.TypeOfDriverID1 > 0) {
            $rootScope.Message({ Msg: 'Chưa chọn tài xế 1', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.DriverID2 > 0 && $scope.Item.TypeOfDriverID2 <= 0) {
            $rootScope.Message({ Msg: 'Chưa chọn loại tài xế 2', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.DriverID2 <= 0 && $scope.Item.TypeOfDriverID2 > 0) {
            $rootScope.Message({ Msg: 'Chưa chọn tài xế 2', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.DriverID3 > 0 && $scope.Item.TypeOfDriverID3 <= 0) {
            $rootScope.Message({ Msg: 'Chưa chọn loại tài xế 3', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.DriverID3 <= 0 && $scope.Item.TypeOfDriverID3 > 0) {
            $rootScope.Message({ Msg: 'Chưa chọn tài xế 3', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if (vform() && !error) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMFleet_Vehicle.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $scope.FLMFLeetPlanning_Grid_Options.dataSource.read();
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

    //#region cbx
    $scope.FLMDriverAll_List_CBbOoptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DriverName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DriverName: { type: 'string' },
                }
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMFleet_Vehicle.URL.DriverAll_List,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                var item = { DriverID: -1, DriverName: '' };
                res.unshift(item);
                $scope.FLMDriverAll_List_CBbOoptions.dataSource.data(res);
            }
        }
    });

    $scope.Vehicle_List_CBbOoptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'VehicleID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'VehicleID',
                fields: {
                    VehicleID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMFleet_Vehicle.URL.Vehicle_List,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.Vehicle_List_CBbOoptions.dataSource.data(res.Data);
            }
        }
    });

    $scope.TypeOfDriver_List_CBbOoptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
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
        method: _FLMFleet_Vehicle.URL.TypeOfDriver,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                var item = { ID: -1, ValueOfVar: '' };
                $scope.TypeOfDriver_List_CBbOoptions.dataSource.data(res.Data);
            }
        }
    });

    $scope.ALL_Shift_List_CBbOoptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ShiftName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ShiftName: { type: 'string' },
                }
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMFleet_Vehicle.URL.ALL_Shift,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.ALL_Shift_List_CBbOoptions.dataSource.data(res.Data);
            }
        }
    });
    //#endregion

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.FLMFleetPlanning,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }
    //#endregion
}])
