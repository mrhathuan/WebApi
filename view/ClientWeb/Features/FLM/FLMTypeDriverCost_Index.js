var _FLMTypeDriverCost = {
    URL: {
        DriverCost_List: 'FLMTypeDriverCost_List',
        DriverCost_Get: 'FLMTypeDriverCost_Get',
        DriverCost_Delete: 'FLMTypeDriverCost_Delete',
        DriverCost_Save: 'FLMTypeDriverCost_Save'
    }
}

angular.module('myapp').controller('FLMTypeDriverCost_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMTypeDriverCost_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.ItemAssetFee = {};

    $scope.DriverCost_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMTypeDriverCost.URL.DriverCost_List,
            readparam: function () { return { } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    WarrantyEnd: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="DriverCost_Edit_Click($event,dataItem,DriverCost_win)" class="k-button" ><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="DriverCost_Delete_Click($event,dataItem)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'TypeOfScheduleFeeCode', title: 'Mã chi phí', width: 150, template: '' },
            { field: 'TypeOfScheduleFeeName', title: 'Tên chi phí', width: 150, },
            { field: 'IsAssistant', title: 'Tài xế', templateAttributes: { style: 'text-align: center;' }, template: '<input class="chkChoose" type="checkbox" #= IsAssistant ? "checked=checked" : "" # disabled/>', width: 100, },
            { field: 'ExprPrice ', title: 'Công thức giá', width: 150, template: '' },
            { field: 'ExprInputDay ', title: 'Điều kiện ngày', width: 150, },
            { field: 'ExprPriceDay ', title: 'Công thức theo ngày', width: 150, },
            { field: 'ExprInputTOMaster ', title: 'Điều kiện chuyến', width: 150, },
            { field: 'ExprPriceTOMaster ', title: 'Công thức theo chuyến', width: 150, },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.DriverCost_Add_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadData_DriverCost(0, win);
    };

    $scope.DriverCost_Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadData_DriverCost(data.ID, win);
    };

    $scope.LoadData_DriverCost = function (ID, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMTypeDriverCost.URL.DriverCost_Get,
            data: { id: ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.ItemAssetFee = res;
                    win.center().open();
                    $rootScope.IsLoading = false;
                }
            }
        });
    }

    $scope.DriverCost_Delete_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMTypeDriverCost.URL.DriverCost_Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $scope.DriverCost_Grid.dataSource.read();
                            $rootScope.IsLoading = false;
                        }
                    }
                });
            }
        })
    };

    $scope.DriverCost_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMTypeDriverCost.URL.DriverCost_Save,
                data: { item: $scope.ItemAssetFee },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $scope.DriverCost_Grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                    }
                }
            });
        }
    }

    $scope.cboTypeOfScheduleFee_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_FLMTypeOfScheduleFee,
        success: function (data) {
            $scope.cboTypeOfScheduleFee_Options.dataSource.data(data);
        }
    })

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

}]);