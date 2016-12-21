
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMFleetPlanning_Column = {
    URL: {
        Data: 'FLMFLeetPlanning_Data',
        DataDetail: 'FLMFLeetPlanning_DetailData',
        Save: 'FLMFLeetPlanning_Save',
        Delete: 'FLMFLeetPlanning_Delete',

        DriverAll_List: 'FLMAsset_AllDriverRead',
        Vehicle_List: 'FLMVehicle_List',
        TypeOfDriver: 'ALL_SYSVarTypeOfDriver',
        ALL_Shift: 'ALL_CATShift',
    },
    Data: {
        ListVehicle: null,
        ListShift: null,
        DataCheck: null,
        DataField: null,
    }
}

angular.module('myapp').controller('FLMFleetPlanning_ColumnCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMFleetPlanning_ColumnCtrl');

    $rootScope.IsLoading = false;
    $scope.Item = null;
    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-3),
        DateTo: (new Date()).addDays(3),
    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMFleetPlanning_Column.URL.Data,
        data: {},
        success: function (res) {
            _FLMFleetPlanning_Column.Data.ListVehicle = res.ListVehicle;
            _FLMFleetPlanning_Column.Data.ListShift = res.ListShift;

            $scope.CreateGrid();
        }
    });

    $scope.SearchData = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.ItemSearch.DateFrom) || !Common.HasValue($scope.ItemSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ItemSearch.DateFrom > $scope.ItemSearch.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $scope.CreateGrid();
        }
    }

    $scope.CreateGrid = function () {

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMFleetPlanning_Column.URL.DataDetail,
            data: { dtFrom: $scope.ItemSearch.DateFrom, dtTo: $scope.ItemSearch.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;

                var Model = {
                    field: {
                        VehicleID: { type: "number" },
                        VehicleNo: { type: "string" },
                        TypeOfVehicleName: { type: "string" }
                    }
                }

                var Columns = [
                    { field: 'VehicleNo', title: 'Xe', width: 150, filterable: { cell: { showOperators: false, operator: "contains" } } },
                    { field: 'TypeOfVehicleName', title: 'Loại xe', width: 150, filterable: { cell: { showOperators: false, operator: "contains" } } },
                ]

                _FLMFleetPlanning_Column.Data.DataField = {};
                for (var i = $scope.ItemSearch.DateFrom; i <= $scope.ItemSearch.DateTo; i = i.addDays(1)) {
                    var dt = Common.Date.ToString(i, "ddMMyyyy");
                    var listCol = [];
                    for (var j = 0; j < _FLMFleetPlanning_Column.Data.ListShift.length; j++) {
                        var shift = _FLMFleetPlanning_Column.Data.ListShift[j];
                        var field = "S" + shift.ID + "_D" + dt;
                        _FLMFleetPlanning_Column.Data.DataField[field] = {
                            Date: i,
                            Shift: shift
                        }
                        Model[field] = { type: "boolean" };
                        var col = {
                            field: field, title: shift.ShiftName, width: 50, filterable: false, sortable: false, headerAttributes: { style: "text-align: center;" },
                            template: '<input type="checkbox"  ng-model="dataItem.' + field + '" ng-click="Detail_Click($event,dataItem,\'' + field + '\',Detail_Win)" />'
                            //template: '<a href="/" ng-click="Detail_Click($event,dataItem)" class="k-button"><input type="checkbox" disabled="disabled" ng-model="dataItem.' + field + '" /></a> '
                        };

                        listCol.push(col)
                    }
                    Columns.push({
                        title: Common.Date.ToString(i, "dd/MM/yyyy"), headerAttributes: { style: "text-align: center;" },
                        columns: listCol
                    });
                }

                _FLMFleetPlanning_Column.Data.DataCheck = {};

                Common.Data.Each(res, function (o) {
                    var d = Common.Date.FromJson(o.PlanningDate);
                    d = Common.Date.ToString(d, "ddMMyyyy");
                    if (!Common.HasValue(_FLMFleetPlanning_Column.Data.DataCheck["V" + o.VehicleID + "_S" + o.ShiftID + "_D" + d]))
                        _FLMFleetPlanning_Column.Data.DataCheck["V" + o.VehicleID + "_S" + o.ShiftID + "_D" + d] = o;
                })

                $scope.FLMFLeetPlanning_Column_Grid.setOptions({
                    dataSource: Common.DataSource.Local({
                        model: Model,
                        pageSize: 20,
                    }),
                    height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: false,
                    columns: Columns
                })

                var dataGrid = [];

                Common.Data.Each(_FLMFleetPlanning_Column.Data.ListVehicle, function (vehicle) {
                    var item = {};
                    item["VehicleID"] = vehicle.VehicleID;
                    item["VehicleNo"] = vehicle.Code;
                    item["TypeOfVehicleName"] = vehicle.TypeOfVehicleName;
                    for (var prop in _FLMFleetPlanning_Column.Data.DataField) {
                        item[prop] = false;
                        if (Common.HasValue(_FLMFleetPlanning_Column.Data.DataCheck["V" + vehicle.VehicleID + "_" + prop])) {
                            item[prop] = true;
                        }
                    }
                    dataGrid.push(item)
                })

                $scope.FLMFLeetPlanning_Column_Grid.dataSource.data(dataGrid);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Detail_Click = function ($event, data, field, win) {

        if (Common.HasValue(_FLMFleetPlanning_Column.Data.DataCheck["V" + data.VehicleID + "_" + field])) {
            $scope.Item = _FLMFleetPlanning_Column.Data.DataCheck["V" + data.VehicleID + "_" + field];
        }
        else {
            $scope.Item = {
                ID: 0,
                VehicleID: data.VehicleID,
                VehicleNo: data.VehicleNo,
                PlanningDate: _FLMFleetPlanning_Column.Data.DataField[field].Date,
                ShiftID: _FLMFleetPlanning_Column.Data.DataField[field].Shift.ID,
                ShiftName: _FLMFleetPlanning_Column.Data.DataField[field].Shift.ShiftName,
                DriverID1: -1,
                DriverID2: -1,
                DriverID3: -1,
                TypeOfDriverID1: -1,
                TypeOfDriverID2: -1,
                TypeOfDriverID3: -1,
            }
        }

        win.center();
        win.open();

        $event.preventDefault();
    }

    $scope.FLMFLeetPlanning_Column_Save_Click = function ($event,win,vform) {
        $event.preventDefault();
        if (!$scope.Item.DriverID1 > 0 && !$scope.Item.DriverID2 > 0 && !$scope.Item.DriverID3 > 0) {
            $rootScope.Message({ Msg: 'Phải chọn ít nhất một tài xế', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $scope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMFleetPlanning_Column.URL.Save,
                data: {item:$scope.Item},
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.CreateGrid();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.FLMFLeetPlanning_Column_Delete_Click = function ($event, win, vform) {
        $event.preventDefault();
        
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $scope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMFleetPlanning_Column.URL.Delete,
                    data: { id: $scope.Item.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.CreateGrid();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    }

    //#region cbx
    $scope.FLMDriverAll_List_CBbOoptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DriverName', dataValueField: 'DriverID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    DriverID: { type: 'number' },
                    DriverName: { type: 'string' },
                }
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMFleetPlanning_Column.URL.DriverAll_List,
        data: {},
        success: function (res) {
            var item = { DriverID: -1, DriverName: ' ' };
            // res.unshift(item);
            var data = [];
            data.push(item);
            Common.Data.Each(res, function (o) {
                data.push(o)
            })
            $scope.FLMDriverAll_List_CBbOoptions.dataSource.data(data);
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
        method: _FLMFleetPlanning_Column.URL.TypeOfDriver,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                var item = { ID: -1, ValueOfVar: '' };
                var data = [];

                data.push(item);
                for (var i in res.Data) {
                    data.push(res.Data[i]);
                }
                $scope.TypeOfDriver_List_CBbOoptions.dataSource.data(data);
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
            ListView: views.FLMFleetPlanning_Column,
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
