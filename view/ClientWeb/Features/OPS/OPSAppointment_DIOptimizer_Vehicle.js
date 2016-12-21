/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIOptimizer_Vehicle = {
    URL: {
        Vehicle_List: 'Opt_Vehicle_List',
        Vehicle_NotIn_List: 'Opt_Vehicle_NotIn_List',
        Vehicle_SaveList: 'Opt_Vehicle_SaveList',
        Vehicle_Update: 'Opt_Vehicle_Update',
        Vehicle_UpdateWeight: 'Opt_Vehicle_UpdateWeight',
        Vehicle_Remove: 'Opt_Vehicle_Remove',
        Romooc_List: 'Opt_Romooc_List',
        Optimizer_Get: 'Opt_Optimizer_Get'
    }
}

angular.module('myapp').controller('OPSAppointment_DIOptimizer_VehicleCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIOptimizer_VehicleCtrl');
    $rootScope.IsLoading = false;

    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.HasChooseVehicle = false;
    $scope.ButtonRunTooltip = "Chạy tối ưu";
    $scope.OptimizerName = "";
    $scope.StatusOfOptimizer = 0;
    $scope.VehicleEdit = {};

    try {
        var objCookie = JSON.parse(Common.Cookie.Get("OPSDIOptimizer"));
        if (Common.HasValue(objCookie)) {
            if (objCookie.ID != $scope.OptimizerID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_Vehicle.URL.Optimizer_Get,
                    data: {
                        optimizerID: $scope.OptimizerID
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            if (res.ID > 0) {
                                Common.Cookie.Set("OPSDIOptimizer", JSON.stringify(res));
                                $scope.OptimizerName = res.OptimizerName;
                                $scope.StatusOfOptimizer = res.StatusOfOptimizer;
                                if ($scope.StatusOfOptimizer > 0)
                                    $scope.ButtonRunTooltip = "Xem kết quả";
                                $scope.ResetView();
                            } else {
                                $rootScope.Message({ Msg: "Không tìm thấy optimizer." });
                                $state.go("main.OPSAppointment.DIOptimizer");
                            }
                        })
                    }
                })
            } else {
                $scope.OptimizerName = objCookie.OptimizerName;
                $scope.StatusOfOptimizer = objCookie.StatusOfOptimizer;
                if ($scope.StatusOfOptimizer > 0)
                    $scope.ButtonRunTooltip = "Xem kết quả";
            }
        }
    }
    catch (e) { }

    $scope.splitterOptions = {
        panes: [
                { collapsible: true, resizable: true, size: '50%' },
                { collapsible: true, resizable: true, size: '50%', collapsed: false }
        ],
        resize: function (e) {
            try {
                openMapV2.Resize();
            } catch (e) { }
        }
    };
    
    openMapV2.Init({
        Element: 'map',
        Tooltip_Show: true,
        Tooltip_Element: 'map_tooltip',
        InfoWin_Show: false,
        ClickMarker: null,
        ClickMap: null,
        DefinedLayer: [{
            Name: 'VectorMarker',
            zIndex: 100
        }]
    });

    $scope.vehicleGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Vehicle.URL.Vehicle_List,
            readparam: function () {
                return {
                    optimizerID: $scope.OptimizerID
                }
            },
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, selectable: true,
        toolbar: $scope.StatusOfOptimizer == 2 ? false : kendo.template($("#vehicle_Grid_toolbar").html()),
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,vehicleGrid,vehicle_gridChoose_Change)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,vehicleGrid,vehicle_gridChoose_Change)" />',
                filterable: false, sortable: false, hidden: $scope.StatusOfOptimizer == 2 ? true : false
            },
            {
                field: 'Command', title: ' ', width: '45px', attributes: { style: 'text-align: center;' },
                template: "<a  href='/' class='k-button' ng-click='Vehicle_Edit_Click($event,dataItem,vehicleEdit_win)'><i class='fa fa-pencil'></i></a>",
                filterable: false, sortable: false, hidden: $scope.StatusOfOptimizer == 2 ? true : false
            },
            {
                field: 'VehicleNo', width: '120px', title: 'Số xe', template: "#=IsOverLoad?\"<span style='color: red;'>\"+VehicleNo+\"</span>\":VehicleNo#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeightCal', title: 'Trọng tải', width: '100px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                title: 'Vị trí', width: '150px', template: '<a href="/" title="#=Common.HasValue(Lat)&&Common.HasValue(Lng)?"Xem vị trí":"Không có dữ liệu"#" ng-click="Vehicle_Location_Click($event, dataItem)"><i style="cursor:pointer;color:#=Common.HasValue(Lat)&&Common.HasValue(Lng)?"rgb(49, 182, 252)":"rgb(90, 104, 119)"#" class="fa fa-map-marker"></i> #=Common.HasValue(Lat)&&Common.HasValue(Lng)?ConvertLatLng(Lat)+"B "+ConvertLatLng(Lng)+"Đ":""#</a>',
                filterable: false
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.vehicleNotInGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Vehicle.URL.Vehicle_NotIn_List,
            readparam: function () {
                return {
                    optimizerID: $scope.OptimizerID
                }
            },
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px', filterable: false, sortable: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,vehicleNotInGrid,vehicleNotInGrid_ChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,vehicleNotInGrid,vehicleNotInGrid_ChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }
            },
            {
                field: 'VehicleNo', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeightCal', title: 'Trọng tải', width: '100px', filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Lat', width: '150px', title: 'Vĩ độ', filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Lng', width: '150px', title: 'Kinh độ', filterable: { cell: { operator: 'gte', showOperators: false } }
            }
        ]
    }

    $scope.vehicle_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChooseVehicle = hasChoose;
    }

    $scope.Vehicle_Location_Click = function ($event, item) {
        $event.preventDefault();

        openMapV2.ClearVector("VectorMarker");
        if (Common.HasValue(item)) {
            $timeout(function () {
                if (item.Lat > 0 && item.Lng > 0) {
                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Tractor), 1);
                    openMapV2.NewMarker(item.Lat, item.Lng, item.Regno, item.Regno, icon, {
                        Item: item, Type: 'Vehicle'
                    }, "VectorMarker");
                    openMapV2.Center(item.Lat, item.Lng);
                }
            }, 100)
        }
    }

    $scope.Vehicle_New_Click = function ($event, grid, win) {
        $event.preventDefault();

        win.center().open();
        grid.dataSource.read();
    }

    $scope.Vehicle_Delete_Click = function ($event, grid) {
        $event.preventDefault();

        var lstID = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                lstID.push(v.ID);
        });
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn xóa xe đầu kéo đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIOptimizer_Vehicle.URL.Vehicle_Remove,
                        data: { optimizerID: $scope.OptimizerID, data: lstID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã xóa!" });
                                grid.dataSource.read();
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.Vehicle_NotIn_Accept_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true)
                data.push(o.VehicleID);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIOptimizer_Vehicle.URL.Vehicle_SaveList,
                data: { optimizerID: $scope.OptimizerID, data: data },
                success: function (res) {
                    Common.Services.Error(res, function () {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        win.close();
                        $scope.vehicleGrid_Options.dataSource.read();
                    })
                }
            })
        } else {
            win.close();
        }
    }

    $scope.Vehicle_Edit_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.VehicleEdit.ID = item.ID;
        $scope.VehicleEdit.VehicleNo = item.VehicleNo;
        $scope.VehicleEdit.MaxWeightCal = item.MaxWeightCal;
        win.center().open();
    }

    $scope.Vehicle_Save_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Vehicle.URL.Vehicle_UpdateWeight,
            data: { vehicleID: $scope.VehicleEdit.ID, maxWeight: $scope.VehicleEdit.MaxWeightCal },
            success: function (res) {
                Common.Services.Error(res, function () {
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.vehicleGrid_Options.dataSource.read();
                })
            }
        })
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DIOptimizer");
    }

    $timeout(function () {
        $scope.splitter.resize();
    }, 1000)

    $scope.ResetView = function () {
        if ($scope.StatusOfOptimizer == 2) {
            $scope.vehicleGrid.setOptions({ toolbar: false });
            $timeout(function () {
                $scope.vehicleGrid.hideColumn("Choose");
                $scope.vehicleGrid.hideColumn("Command");
            }, 300)
        }            
        $scope.vehicleGrid.refresh();
    }
}]);

function ConvertLatLng(value) {
    var str = parseInt(value, 10);
    var deg = value - str;
    deg = Math.round(deg * 60);

    return str + "*" + deg;
}