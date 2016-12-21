/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _COOptimizer_Romooc = {
    URL: {
        Romooc_List: 'Opt_Romooc_List',
        Romooc_NotIn_List: 'Opt_Romooc_NotIn_List',
        Romooc_Delete: 'Opt_Romooc_Remove',
        Romooc_SaveList: 'Opt_Romooc_SaveList'
    }
}

angular.module('myapp').controller('OPSAppointment_COOptimizer_RomoocCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COOptimizer_RomoocCtrl');
    $rootScope.IsLoading = false;

    $scope.HasChoose = false;
    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.ButtonRunTooltip = "Chạy tối ưu";
    $scope.OptimizerName = "";
    $scope.StatusOfOptimizer = 0;

    try {
        var objCookie = JSON.parse(Common.Cookie.Get("OPSCOOptimizer"));
        if (Common.HasValue(objCookie)) {
            if (objCookie.ID != $scope.OptimizerID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _COOptimizer_Vehicle.URL.Optimizer_Get,
                    data: {
                        optimizerID: $scope.OptimizerID
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            if (res.ID > 0) {
                                Common.Cookie.Set("OPSCOOptimizer", JSON.stringify(res));
                                $scope.OptimizerName = res.OptimizerName;
                                $scope.StatusOfOptimizer = res.StatusOfOptimizer;
                                if ($scope.StatusOfOptimizer > 0)
                                    $scope.ButtonRunTooltip = "Xem kết quả";
                                $scope.ResetView();
                            } else {
                                $rootScope.Message({ Msg: "Không tìm thấy optimizer." });
                                $state.go("main.OPSAppointment.COOptimizer");
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
        orientation: "horizontal",
        panes: [
          { collapsible: true, resizable: true, size: '40%' },
          { collapsible: true, resizable: true, size: '60%', collapsed: false }
        ],
        resize: function () {
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

    $scope.romoocGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _COOptimizer_Romooc.URL.Romooc_List,
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
                    IsChoose: { type: 'bool', defaultValue: false }
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false,
        toolbar: $scope.StatusOfOptimizer == 2 ? false : kendo.template($("#romooc_Grid_toolbar").html()),
        columns: [
            {
                field: 'Choose', title: ' ', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,romoocGrid,romoocGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,romoocGrid,romoocGridChoose_Change)" />',
                filterable: false, sortable: false, hidden: $scope.StatusOfOptimizer == 2 ? true : false
            },
            {
                field: 'RomoocNo', width: '120px', title: 'Số Romooc',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeightCal', width: '80px', title: 'Trọng tải',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: 'Vị trí', width: '150px', template: '<a href="/" title="#=Common.HasValue(Lat)&&Common.HasValue(Lng)?"Xem vị trí":"Không có dữ liệu"#" ng-click="Romooc_Location_Click($event, dataItem)"><i style="cursor:pointer;color:#=Common.HasValue(Lat)&&Common.HasValue(Lng)?"rgb(49, 182, 252)":"rgb(90, 104, 119)"#" class="fa fa-map-marker"></i> #=Common.HasValue(Lat)&&Common.HasValue(Lng)?ConvertLatLng(Lat)+"B "+ConvertLatLng(Lng)+"Đ":""#</a>',
                filterable: false
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.romoocGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.Romooc_Location_Click = function ($event, item) {
        $event.preventDefault();
        
        openMapV2.ClearVector("VectorMarker");
        if (Common.HasValue(item)) {
            $timeout(function () {
                if (item.Lat > 0 && item.Lng > 0) {
                    var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Romooc_40), 1);
                    if (item.Is20DC)
                        icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Romooc_20), 1);
                    openMapV2.NewMarker(item.Lat, item.Lng, item.Regno, item.Regno, icon, {
                        Item: item, Type: 'Vehicle'
                    }, "VectorMarker");
                    openMapV2.Center(item.Lat, item.Lng);
                }
            }, 100)
        }
    }

    $scope.romoocNotInGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _COOptimizer_Romooc.URL.Romooc_NotIn_List,
            readparam: function () {
                return {
                    optimizerID: $scope.OptimizerID
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, 
        columns: [
            {
                title: ' ', width: '35px', filterable: false, sortable: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,romoocNotInGrid,romoocNotInGridChoose_Change)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,romoocNotInGrid,romoocNotInGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }
            },
            {
                field: 'RomoocNo', title: 'Số romooc',  filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaxWeightCal', width: '80px', title: 'Trọng tải', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lat', width: '150px', title: 'Vĩ độ', filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Lng', width: '150px', title: 'Kinh độ', filterable: { cell: { operator: 'gte', showOperators: false } }
            }
        ]
    }

    $scope.New_Click = function ($event, grid, win) {
        $event.preventDefault();
        grid.dataSource.read();
        win.center().open();
        $timeout(function () {
            $scope.romooc_Splitter.resize();
        }, 100);
    }

    $scope.Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstID = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                lstID.push(v.ID);
        });
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn xóa các romooc đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _COOptimizer_Romooc.URL.Romooc_Delete,
                        data: {OptimizerID:$scope.OptimizerID, lstID: lstID },
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

    $scope.Accept_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = []
        angular.forEach(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true)
                data.push(o.ID)
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _COOptimizer_Romooc.URL.Romooc_SaveList,
                data: { optimizerID: $scope.OptimizerID, data: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        win.close();
                        $scope.romoocGrid_Options.dataSource.read();
                    })
                }
            })
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.COOptimizer");
    };

    $timeout(function () {
        $scope.splitter.resize();
    }, 1000)

    $scope.ResetView = function () {
        if ($scope.StatusOfOptimizer == 2) {
            $scope.romoocGrid.setOptions({ toolbar: false });
            $timeout(function () {
                $scope.romoocGrid.hideColumn("Choose");
            }, 300)
        }
        $scope.romoocGrid.refresh();
    }
}]);

function ConvertLatLng(value) {
    var str = parseInt(value, 10);
    var deg = value - str;
    deg = Math.round(deg * 60);

    return str + "*" + deg;
}