/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />                      

//#region URL
var _CATServiceOfOrder = {
    URL: {
        Read: 'ServiceOfOrder_List',
        Delete: 'ServiceOfOrder_Delete',
        Save: 'ServiceOfOrder_Save',
        Get: 'ServiceOfOrder_Get',
        SYSVarServiceOfOrder: 'ServiceOfOrder_SYSVarServiceOfOrder',

        ExcelInit: 'ServiceOfOrder_ExcelInit',
        ExcelChange: 'ServiceOfOrder_ExcelChange',
        ExcelImport: 'ServiceOfOrder_ExcelImport',
        ExcelApprove: 'ServiceOfOrder_ExcelApprove',
    }
}

//#endregion

angular.module('myapp').controller('CATServiceOfOrder_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATServiceOfOrder_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.Item = null;

    $scope.Auth = $rootScope.GetAuth();
    $scope.CATServiceOfOrder_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATServiceOfOrder.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CATGroupOfLocationName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#CATServiceOfOrder_grid_toolbar').html()),
        columns: [
            {
                field: "Command", title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="ServiceOfOrderEdit_Click($event,CATServiceOfOrder_winPopup,CATServiceOfOrder_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="ServiceOfOrderDestroy_Click($event,CATServiceOfOrder_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 200,sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: 'Tên', width: 200, sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', title: ' Loại dịch vụ', sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.ServiceOfOrderEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID);
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

    $scope.CATServiceOfOrderAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    };

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATServiceOfOrder.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.Item = res;
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        });
    }

    $scope.ServiceOfOrderDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
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
                        method: _CATServiceOfOrder.URL.Delete,
                        data: { 'ID': item.ID },
                        success: function (res) {
                            $scope.CATServiceOfOrder_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATServiceOfOrderSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.Item.ServiceOfOrderID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATServiceOfOrder.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        $scope.CATServiceOfOrder_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn loại dịch vụ', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.cboService_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATServiceOfOrder.URL.SYSVarServiceOfOrder,
        data: {},
        success: function (res) {
            $scope.cboService_Options.dataSource.data(res.Data);
        }
    });

    $scope.CATServiceOfOrder_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 5; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                '[Tên] không được trống và > 50 ký tự',
                'Không có mã loại',
                'Không tồn tại mã loại trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CATPackingGOPTU_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATServiceOfOrder.URL.ExcelInit,
            methodChange: _CATServiceOfOrder.URL.ExcelChange,
            methodImport: _CATServiceOfOrder.URL.ExcelImport,
            methodApprove: _CATServiceOfOrder.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATPackingGOPTU_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);