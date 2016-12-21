/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIOptimizer = {
    URL: {
        List: 'Opt_Optimizer_List',
        Save: 'Opt_Optimizer_Save',
        Delete: 'Opt_Optimizer_Delete',
    }
}

angular.module('myapp').controller('OPSAppointment_DIOptimizerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_DIOptimizerCtrl');
    $rootScope.IsLoading = false;

    $scope.Optimizer = { ID: -1, OptimizerName: '', DateFrom: new Date().addDays(-3), DateTo: new Date() };

    $scope.optimizerGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer.URL.List,
            readparam: function () {
                return {
                    isCo: false
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CreatedDate: { type: 'date' },
                    DateFrom: { type: 'date' },
                    DateTo: { type: 'date' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, selectable: true, reorderable: false, resizable: true,
        columns: [
            {
                width: '95px', title: ' ', sortable: false,
                sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: "<a  href='/' class='k-button' ng-click='Optimizer_Edit($event,dataItem,optimizer_info_win)'><i class='fa fa-pencil'></i></a>" +
                    "<a  href='/' class='k-button' ng-click='Optimizer_Delete($event,dataItem,optimizerGrid)'><i class='fa fa-trash'></i></a>"
            },
            {
                field: 'OptimizerName', title: 'Tên optimizer',width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateFrom', width: '150px', title: 'Ngày BD',
                template: "#=DateFrom != null ? Common.Date.ToString(DateFrom) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateTo', width: '150px', title: 'Ngày KT',
                template: "#=DateTo != null ? Common.Date.ToString(DateTo) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'StatusOfOptimizerName', width: '150px', title: 'Trạng thái',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CreatedBy', width: '200px', title: 'Người tạo',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CreatedDate', width: '150px', title: 'Ngày tạo',
                template: "#=Common.Date.ToString(CreatedDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
                sortorder: 6, configurable: true, isfunctionalHidden: false
            }
        ],
        change: function () {
            $scope.HasSelect = true;
        },
        dataBound: function () {
            this.element.find('tr').dblclick(function (e) {
                e.preventDefault();
                var item = $scope.optimizerGrid.dataItem(this);
                if (Common.HasValue(item)) {
                    $scope.OptimizerID = item.ID;
                    Common.Cookie.Set("OPSDIOptimizer", JSON.stringify(item));
                    $state.go("main.OPSAppointment.DIOptimizer_GroupOfProduct", { OptimizerID: item.ID });
                }
            });
        }
    }

    $scope.Optimizer_New = function ($event, grid, win) {
        $event.preventDefault();

        $scope.Optimizer.ID = -1;
        $scope.Optimizer.OptimizerName = '';
        win.center().open();
    }

    $scope.Optimizer_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        if ($scope.Optimizer.OptimizerName == '' || $scope.Optimizer.OptimizerName == null) {
            $rootScope.Message({
                Msg: 'Nhập tên optimizer',
                NotifyType: Common.Message.NotifyType.WARNING
            })
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIOptimizer.URL.Save,
                data: { item: $scope.Optimizer },
                success: function (res) {
                    Common.Services.Error(res, function () {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });

                        win.close();
                        grid.dataSource.read();
                    })
                }
            })
        }
    }

    $scope.Optimizer_Edit = function ($event, item, win) {
        $event.preventDefault();

        $scope.Optimizer = $.extend({}, true, item);
        win.center().open();
    }

    $scope.Optimizer_Delete = function ($event, item, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa optimizer?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer.URL.Delete,
                    data: { optimizerID: item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function () {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            grid.dataSource.read();
                        })
                    }
                })
            }
        })
    }

    $scope.Optimizer_Select = function ($event) {
        $event.preventDefault();

        var item = $scope.optimizerGrid.dataItem($scope.optimizerGrid.select());
        if (Common.HasValue(item)) {
            $scope.OptimizerID = item.ID;
            Common.Cookie.Set("OPSDIOptimizer", JSON.stringify(item));
            $state.go("main.OPSAppointment.DIOptimizer_GroupOfProduct", { OptimizerID: item.ID });
        }
        else {
            $rootScope.Message({
                Msg: 'Chọn optimizer.',
                NotifyType: Common.Message.NotifyType.WARNING
            });
        }
    }

    $scope.Back_Click = function($event)
    {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DI");
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
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