/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _COOptimizer_COTOContainer = {
    URL: {
        List: "Opt_COTOContainer_List"
    }
}

angular.module('myapp').controller('OPSAppointment_COOptimizer_COTOContainerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COOptimizer_COTOContainerCtrl');
    $rootScope.IsLoading = false;
    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.OptimizerName = "";

    try {
        var objCookie = JSON.parse(Common.Cookie.Get("OPSCOOptimizer"));
        if (Common.HasValue(objCookie)) {
            if (objCookie.ID != $scope.OptimizerID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _COOptimizer_Container.URL.Optimizer_Get,
                    data: {
                        optimizerID: $scope.OptimizerID
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            if (res.ID > 0) {
                                Common.Cookie.Set("OPSCOOptimizer", JSON.stringify(res));
                                $scope.OptimizerName = res.OptimizerName;
                                $scope.StatusOfOptimizer = res.StatusOfOptimizer;
                                $scope.OptimizerClosed = $scope.StatusOfOptimizer == 2;
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
                $scope.OptimizerClosed = $scope.StatusOfOptimizer == 2;
            }
        }
    }
    catch (e) { }
    
    $scope.cotocontainer_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _COOptimizer_COTOContainer.URL.List,
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
                    ETA: { type: 'date' },
                    ETD: { type: 'date' },
                    ETAStart: { type: 'date' },
                    ETDStart: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true,
        reorderable: false, filterable: { mode: 'row' }, selectable: false, allowCopy: true,
        columns: [
            {
                field: 'OPTCOTOMasterCode', width: '100px', title: 'Số chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', width: '80px', title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', width: '80px', title: 'Số Con.',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', width: '110px', title: 'Loại v.chuyển',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingName', width: '90px', title: 'Loại Con',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfCOContainerName', width: '90px', title: 'Trạng thái',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', width: '80px', title: 'Trọng tải', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'SealNo1', width: '90px', title: 'Số Seal 1',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SealNo2', width: '90px', title: 'Số Seal 2',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETDStart', width: '130px', title: 'BĐ ETD',
                template: "#=ETDStart==null?' ':kendo.toString(ETDStart, '" + Common.Date.Format.DMYHM + "')#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETD', width: '130px', title: 'ETD',
                template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: {
                    cell: {
                        template: function (e) {
                            e.element.kendoDatePicker({ format: Common.Date.Format.DMY });
                        },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETAStart', width: '130px', title: 'BĐ ETA',
                template: "#=ETAStart==null?' ':kendo.toString(ETAStart, '" + Common.Date.Format.DMYHM + "')#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA',
                template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'CustomerCode', width: '100px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PlanNote', width: '500px', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.Back_click = function($event)
    {
        $event.preventDefault();
        $state.go("main.OPSAppointment.COOptimizer_Master", { OptimizerID: $scope.OptimizerID });
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);