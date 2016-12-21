/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATPackingGOPTU = {
    URL: {
        Read: 'CATPackingGOPTU_Read',
        Delete: 'CATPackingGOPTU_Destroy',
        Save: 'CATPackingGOPTU_Update',
        Get: 'CATPackingGOPTU_Get',
        List_GOP: 'CATPackingGOP_List',

        ExcelInit: 'CATPackingGOPTU_ExcelInit',
        ExcelChange: 'CATPackingGOPTU_ExcelChange',
        ExcelImport: 'CATPackingGOPTU_ExcelImport',
        ExcelApprove: 'CATPackingGOPT_ExcelApprove',
    },
}

//#endregion

angular.module('myapp').controller('CATPackingGOPTU_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATPackingGOPTU_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATPackingGOPTUItem = null
    $scope.IsEdit = false;

    $scope.CATPackingGOPTU_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATPackingGOPTU.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                field: "Command", title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATPackingGOPTU_Click($event,CATPackingGOPTU_win,CATPackingGOPTU_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATPackingGOPTUDestroy_Click($event,CATPackingGOPTU_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATPackingGOPTU.Code}}', width: 200,sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PackingName', title: '{{RS.CATPackingGOPTU.PackingName}}', width: 250, sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPackageName', title: '{{RS.CATPackingGOPTU.TypeOfPackageName}}', width: 250, sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };
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
    $scope.CATPackingGOPTU_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.IsEdit = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATPackingGOPTU.URL.Get,
            data: { id: id },
            success: function (res) {
                $scope.CATPackingGOPTUItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATPackingGOPTUDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có muốn xóa',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATPackingGOPTU.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATPackingGOPTU_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATPackingGOPTU_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATPackingGOPTU.URL.Save,
                data: { item: $scope.CATPackingGOPTUItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATPackingGOPTU_grid.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATPackingGOPTU_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.CATPackingGOPTU_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.IsEdit = false;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATPackingGOPTU.URL.Get,
            data: { id: 0 },
            success: function (res) {
                $scope.CATPackingGOPTUItem = res;
                win.center();
                win.open();
            }
        });
    }
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminOther.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }


    $scope.CATPackingGOP_cboList_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ValueOfVar',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATPackingGOPTU.URL.List_GOP,

        success: function (data) {

            $scope.CATPackingGOP_cboList_Options.dataSource.data(data.Data);
        }
    });

    $scope.CATPackingGOPTU_Excel_Click = function ($event) {
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
            methodInit: _CATPackingGOPTU.URL.ExcelInit,
            methodChange: _CATPackingGOPTU.URL.ExcelChange,
            methodImport: _CATPackingGOPTU.URL.ExcelImport,
            methodApprove: _CATPackingGOPTU.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATPackingGOPTU_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    }
}]);