/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIOptimizer_LocationMatrix = {
    URL: {
        Matrix_List: 'Opt_LocationMatrix_List',
        Matrix_Update: 'Opt_LocationMatrix_Update',
        Matrix_Refresh: 'Opt_LocationMatrix_Refresh',
        Optimizer_Get: 'Opt_Optimizer_Get'
    }
}

angular.module('myapp').controller('OPSAppointment_DIOptimizer_LocationMatrixCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_DIOptimizer_LocationMatrixCtrl');
    $rootScope.IsLoading = false;

    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.ButtonRunTooltip = "Chạy tối ưu";
    $scope.OptimizerName = "";
    $scope.StatusOfOptimizer = 0;
    $scope.IsActiveKey = false;
    
    try {
        var objCookie = JSON.parse(Common.Cookie.Get("OPSDIOptimizer"));
        if (Common.HasValue(objCookie)) {
            if (objCookie.ID != $scope.OptimizerID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_LocationMatrix.URL.Optimizer_Get,
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

    $scope.matrixGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_LocationMatrix.URL.Matrix_List,
            readparam: function () {
                return {
                    optimizerID: $scope.OptimizerID
                }
            },
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    EHours: { type: 'number' },
                    EDistance: { type: 'number' },
                    LocationFromName: { type: 'string', editable: false },
                    LocationToName: { type: 'string', editable: false },
                    LocationFromAddress: { type: 'string', editable: false },
                    LocationToAddress: { type: 'string', editable: false }
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: { mode: "multiple" }, columnMenu: false, resizable: true,
        reorderable: false, filterable: { mode: 'row' }, editable: 'incell',
        save: function (e) {
            var grid = this, myfield = '', myValue = '';
            for (f in e.values) { myfield = f; } myValue = e.values[myfield];
            if (myValue <= 0) {
                $rootScope.Message({ Msg: "Nhập số sai!" });
                e.preventDefault(); return;
            }
            $timeout(function () {
                var item = e.model;
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_LocationMatrix.URL.Matrix_Update,
                    data: { item: item },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            grid.dataSource.read();
                        })
                    }
                })
            }, 10)
        },
        columns: [
            { field: 'LocationFromName', width: '150px', title: 'Điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: '150px', title: 'Điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EDistance', width: '130px', title: 'Khoảng cách (km)', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'EHours', width: '100px', title: 'Thời gian (h)', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', title: 'Địa chỉ đến', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    };

    $scope.Refresh_Click = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_LocationMatrix.URL.Matrix_Refresh,
            data: { optimizerID: $scope.OptimizerID },
            success: function (res) {
                Common.Services.Error(res, function () {
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    $rootScope.IsLoading = false;
                    grid.dataSource.read();
                })
            }
        })
    }
    
    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DIOptimizer");
    }
}]);