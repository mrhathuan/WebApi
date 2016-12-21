/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATCost = {
    URL: {
        Read: 'CATCost_Read',
        Delete: 'CATCost_Destroy',
        Save: 'CATCost_Update',
        Get: 'CATCost_Get',

        ExcelInit: 'CATCost_ExcelInit',
        ExcelChange: 'CATCost_ExcelChange',
        ExcelImport: 'CATCost_ExcelImport',
        ExcelApprove: 'CATCost_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    },
    ExcelKey: {
        Resource: "CATCost_Excel",
        CATDriver: "CATCost"
    }
}

//#endregion

angular.module('myapp').controller('CATCost_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATCost_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATCostItem = null
    $scope.IsEdit = false;

    $scope.CATCost_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATCost.URL.Read,
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
                field: 'Command', title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATCostEdit_Click($event,CATCost_win,CATCost_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATCostDestroy_Click($event,CATCost_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATCost.Code}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'CostName', title: '{{RS.CATCost.CostName}}', width: 250, sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'AccountNo', title: '{{RS.CATCost.AccountNo}}', width: 100, sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'GroupOfCostName', title: '{{CATGroupOfCost.GroupName}}', width: 150, sortorder: 4, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'TypeOfCostName', title: '{{RS.CATCost.TypeOfCostName}}', width: 150, sortorder: 5, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { title: '', filterable: false, sortable: false, sortorder: 6, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.CATCostEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.IsEdit = true;
        $scope.LoadItem(win, id);
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
    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATCost.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATCostItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATCostDestroy_Click = function ($event, grid) {
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
                        method: _CATCost.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATCost_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATCost_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATCostItem.GroupOfCostID) && $scope.CATCostItem.GroupOfCostID > 0) {
                if (Common.HasValue($scope.CATCostItem.TypeOfCostID) && $scope.CATCostItem.TypeOfCostID > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATCost.URL.Save,
                        data: { item: $scope.CATCostItem },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                win.close();
                                $scope.CATCost_gridOptions.dataSource.read();
                            })
                        }
                    });
                } else $rootScope.Message({ Msg: 'Chưa chọn loại chi phí', NotifyType: Common.Message.NotifyType.ERROR });
            } else $rootScope.Message({ Msg: 'Chưa chọn nhóm chi phí', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATCost_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATCost_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.IsEdit = false;
        $scope.LoadItem(win, 0);
    }

    $scope.CATCost_cboGroup_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }
    $scope.CATCost_cboType_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
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
    
    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfCost,
        success: function (data) {
            $scope.CATCost_cboGroup_Options.dataSource.data(data);
        }
    })
    Common.ALL.Get($http, {
        url: Common.ALL.URL.SLI_SYSVarTypeOfCost,
        success: function (data) {
            $scope.CATCost_cboType_Options.dataSource.data(data);
        }
    })

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.CATCost_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 5; i++) {
            var resource = $rootScope.RS[_CATCost.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                'Không tồn tại mã trong hệ thống',
                '[Tên] không được trống và > 50 ký tự',
                '[định mức] không được trống và > 50 ký tự',
                '[Mã nhóm chi] không được trống và > 50 ký tự',
                '[Mã nhóm chi phí] đã bị trùng',
                'Không tồn tại mã nhóm chi trong hệ thống',
                '[Mã loại chi phí] không được trống và > 50 ký tự',
                '[Mã loại chi phí] đã bị trùng',
                'Không tồn tại mã loại chi phí trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CATGroupOfTrouble_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _CATCost.URL.ExcelInit,
            methodChange: _CATCost.URL.ExcelChange,
            methodImport: _CATCost.URL.ExcelImport,
            methodApprove: _CATCost.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATCost_gridOptions.dataSource.read();
            }
        });
    };
}]);