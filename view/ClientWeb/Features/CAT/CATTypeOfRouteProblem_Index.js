/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATTypeOfRouteProblem = {
    URL: {
        Read: 'CATTypeOfRouteProblem_Read',
        Delete: 'CATTypeOfRouteProblem_Destroy',
        Save: 'CATTypeOfRouteProblem_Save',
        Get: 'CATTypeOfRouteProblem_Get',

        ExcelInit: 'CATTypeOfRouteProblem_ExcelInit',
        ExcelChange: 'CATTypeOfRouteProblem_ExcelChange',
        ExcelImport: 'CATTypeOfRouteProblem_ExcelImport',
        ExcelApprove: 'CATTypeOfRouteProblem_ExcelApprove',
    },
    ExcelKey: {
        Resource: "CATTypeOfRouteProblem_Excel",
        TypeOfRouteProblem: "CATTypeOfRouteProblem"
    }
}

//#endregion

angular.module('myapp').controller('CATTypeOfRouteProblem_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATTypeOfRouteProblem_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.CATTypeOfRouteProblem_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfRouteProblem.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#CATTypeOfRouteProblem_grid_toolbar').html()),
        columns: [
            {
                field: 'Command', title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATTypeOfRouteProblemEdit_Click($event,CATTypeOfRouteProblem_win,CATTypeOfRouteProblem_grid,CATTypeOfRouteProblem_win_form)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATTypeOfRouteProblemDestroy_Click($event,CATTypeOfRouteProblem_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATTypeOfRouteProblem.Code}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeName', title: '{{RS.CATTypeOfRouteProblem.TypeName}}', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.CATTypeOfRouteProblemEdit_Click = function ($event, win, grid,vform) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID,vform);
    }
    
    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfRouteProblem.URL.Get,
            data: { 'id': id },
            success: function (res) {
                $scope.Item = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATTypeOfRouteProblemDestroy_Click = function ($event, grid) {
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
                        method: _CATTypeOfRouteProblem.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATTypeOfRouteProblem_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATTypeOfRouteProblem_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATTypeOfRouteProblem.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $scope.CATTypeOfRouteProblem_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.CATTypeOfRouteProblem_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATTypeOfRouteProblemAddNew_Click = function ($event, win,vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0,vform);
    }

    $scope.CATTypeOfRouteProblem_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_CATTypeOfRouteProblem.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã vấn đề] không được trống và > 50 ký tự',
                '[Tên vấn đề] không được trống và > 50 ký tự',
                '[Mã vấn đề] đã bị trùng',
            ];
        }
        $rootScope.excelShare.Init({
            functionkey: _CATTypeOfRouteProblem.ExcelKey.TypeOfRouteProblem,
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATTypeOfRouteProblem.URL.ExcelInit,
            methodChange: _CATTypeOfRouteProblem.URL.ExcelChange,
            methodImport: _CATTypeOfRouteProblem.URL.ExcelImport,
            methodApprove: _CATTypeOfRouteProblem.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATTypeOfRouteProblem_gridOptions.dataSource.read();
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);