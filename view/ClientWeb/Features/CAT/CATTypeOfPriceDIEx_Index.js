/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATTypeOfPriceDIEx = {
    URL: {
        Read: 'CATTypeOfPriceDIEx_List',
        Delete: 'CATTypeOfPriceDIEx_Delete',
        Save: 'CATTypeOfPriceDIEx_Save',
        Get: 'CATTypeOfPriceDIEx_Get',

        ExcelInit: 'CATTypeOfPriceDIEx_ExcelInit',
        ExcelChange: 'CATTypeOfPriceDIEx_ExcelChange',
        ExcelImport: 'CATTypeOfPriceDIEx_ExcelImport',
        ExcelApprove: 'CATTypeOfPriceDIEx_ExcelApprove',
    },
    ExcelKey: {
        Resource: "CATTypeOfPriceDIEx_Excel",
        TypeOfPriceDIEx: "CATTypeOfPriceDIEx"
    }
}

//#endregion

angular.module('myapp').controller('CATTypeOfPriceDIEx_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATTypeOfPriceDIEx_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.CATTypeOfPriceDIEx_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeOfPriceDIEx.URL.Read,
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
        toolbar: kendo.template($('#CATTypeOfPriceDIEx_grid_toolbar').html()),
        columns: [
            {
                field: 'Command', title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATTypeOfPriceDIExEdit_Click($event,CATTypeOfPriceDIEx_win,CATTypeOfPriceDIEx_grid,CATTypeOfPriceDIEx_win_form)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATTypeOfPriceDIExDestroy_Click($event,CATTypeOfPriceDIEx_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATTypeOfPriceDIEx.Code}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'TypeName', title: '{{RS.CATTypeOfPriceDIEx.TypeName}}', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfPriceExName', title: '{{RS.CATTypeOfPriceDIEx.TypeOfPriceExName}}', sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };

   

    $scope.CATTypeOfPriceDIExEdit_Click = function ($event, win, grid,vform) {
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
            method: _CATTypeOfPriceDIEx.URL.Get,
            data: { 'id': id },
            success: function (res) {
                $scope.Item = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATTypeOfPriceDIExDestroy_Click = function ($event, grid) {
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
                        method: _CATTypeOfPriceDIEx.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATTypeOfPriceDIEx_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATTypeOfPriceDIEx_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATTypeOfPriceDIEx.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $scope.CATTypeOfPriceDIEx_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.CATTypeOfPriceDIEx_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    


    $scope.CATTypeOfPriceDIExAddNew_Click = function ($event, win,vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0,vform);
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.CATTypeOfPriceDIEx_Excel_Click = function ($event) {
        $event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã phụ] không được trống và > 50 ký tự',
                '[Tên phụ] không được trống và > 50 ký tự',
                '[Mã phụ] đã bị trùng',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _CATTypeOfPriceDIEx.ExcelKey.TypeOfPriceDIEx,
            params: {},
            rowStart: 2,
            colCheckChange: 15,
            url: Common.Services.url.CAT,
            methodInit: _CATTypeOfPriceDIEx.URL.ExcelInit,
            methodChange: _CATTypeOfPriceDIEx.URL.ExcelChange,
            methodImport: _CATTypeOfPriceDIEx.URL.ExcelImport,
            methodApprove: _CATTypeOfPriceDIEx.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {},
            Approved: function () {
                $scope.CATTypeOfPriceDIEx_gridOptions.dataSource.read();
            }
        });
    };
}]);