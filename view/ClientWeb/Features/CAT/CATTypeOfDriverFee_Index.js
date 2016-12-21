/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATTypeOfDriverFee = {
    URL: {
        Read: 'CATTypeOfDriverFee_List',
        Delete: 'CATTypeOfDriverFee_Delete',
        Save: 'CATTypeOfDriverFee_Save',
        Get: 'CATTypeOfDriverFee_Get',

        ExcelInit: 'CATTypeOfDriverFee_ExcelInit',
        ExcelChange: 'CATTypeOfDriverFee_ExcelChange',
        ExcelImport: 'CATTypeOfDriverFee_ExcelImport',
        ExcelApprove: 'CATTypeOfDriverFee_ExcelApprove',
    }
}

//#endregion

angular.module('myapp').controller('CATTypeOfDriverFee_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATTypeOfDriverFee_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    
    $scope.CATTypeOfDriverFee_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfDriverFee.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#CATTypeOfDriverFee_grid_toolbar').html()),
        columns: [
            {
                field: "Command", title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="Edit_Click($event,CATTypeOfDriverFee_win,CATTypeOfDriverFee_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Destroy_Click($event,CATTypeOfDriverFee_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã loại', width: 150,sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeName', title: 'Tên loại', width: 150,sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.Edit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID);
    }

    //#endregion
    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfDriverFee.URL.Get,
            data: { 'id': id },
            success: function (res) {
                $scope.Item = res;
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        });
    }

    $scope.Destroy_Click = function ($event, grid) {
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
                        method: _CATTypeOfDriverFee.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATTypeOfDriverFee_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATTypeOfDriverFee.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {

                    $scope.CATTypeOfDriverFee_gridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.AddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    };


    $scope.CATTypeOfDriverFee_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                '[Tên] không được trống và > 50 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CATTypeOfDriverFee_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATTypeOfDriverFee.URL.ExcelInit,
            methodChange: _CATTypeOfDriverFee.URL.ExcelChange,
            methodImport: _CATTypeOfDriverFee.URL.ExcelImport,
            methodApprove: _CATTypeOfDriverFee.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATTypeOfDriverFee_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);