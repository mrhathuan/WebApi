/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingOrderCode = {
    URL: {
        Read: 'CUSSettingOrderCode_List',
        Get: 'CUSSettingOrderCode_Get',
        Save: 'CUSSettingOrderCode_Save',
        Delete: 'CUSSettingOrderCode_Delete'
    }
}

angular.module('myapp').controller('CUSSettingOrder_CodeCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingOrderCode_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.ItemCUSSetting = {};

    $scope.setting_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingOrderCode.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    CreateDate: { type: 'date' },
                },
            }
        }),
        height: '100%', filterable: true, sortable: true, menu: false, pageable:true,
        filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CUSSetting_EditClick($event,dataItem,CusSetting_win)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CUSSetting_DeleteClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>'
            },
            { field: 'CustomerCode', width: "100px", title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: "150px", title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'CreateDate', width: '130px', title: 'Ngày tạo', template: "#=CreateDate==null?' ':Common.Date.FromJsonDMY(CreateDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { field: 'CreateBy', width: "150px", title: 'Người tạo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.AddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadData_CUSSetting(0, win);
    };

    $scope.CUSSetting_EditClick = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadData_CUSSetting(data.ID, win);
    };

    $scope.LoadData_CUSSetting = function (ID, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingOrderCode.URL.Get,
            data: { id: ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.ItemCUSSetting = res;
                    win.center().open();
                    $rootScope.IsLoading = false;

                }
            }
        });
    }

    $scope.CUSSetting_DeleteClick = function ($event, data) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSSettingOrderCode.URL.Delete,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.setting_gridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
    //#region Action

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();

        var flag = true;
        if (vform()) {
            if ($scope.ItemCUSSetting.CustomerID >= 0) {
                if ($scope.ItemCUSSetting.ActionType != 0) {
                    if (!Common.HasValue($scope.ItemCUSSetting.Expr) || $scope.ItemCUSSetting.Expr == "") {
                        $rootScope.Message({ Msg: 'Công thức không được trống', NotifyType: Common.Message.NotifyType.ERROR });
                        flag = false;
                    }
                    else {
                        var pattern = /(\[)(S)(o)(r)(t)(-)\d+(\])/ig;
                        var checkExpr = $scope.ItemCUSSetting.Expr.match(pattern);
                        if (Common.HasValue(checkExpr) && checkExpr.length > 0) {

                        }
                        else {
                            $rootScope.Message({ Msg: 'Công thức phải có ít nhất 1 loại sort', NotifyType: Common.Message.NotifyType.ERROR });
                            flag = false;
                        }
                    }
                }

                if (flag) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSSettingOrderCode.URL.Save,
                        data: { item: $scope.ItemCUSSetting, id: $scope.ItemCUSSetting.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.setting_gridOptions.dataSource.read();
                            win.close();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            }
            else $rootScope.Message({ Msg: 'Khách hàng không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CUSSettingOrder,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    };

    $scope.cboListCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                CustomerName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Customer,
        success: function (data) {
            var res = [];
            res.push({ ID: "0", CustomerName: "Tất cả" });
            $.each(data, function (i, v) {
                res.push(v);
            });
            $scope.cboListCustomer_Options.dataSource.data(res);
        }
    })

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

}]);