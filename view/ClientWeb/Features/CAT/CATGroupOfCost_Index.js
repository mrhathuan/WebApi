/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfCost = {
    URL: {
        Read: 'CATGroupOfCost_Read',
        Delete: 'CATGroupOfCost_Destroy',
        Save: 'CATGroupOfCost_Update',
        Get: 'CATGroupOfCost_Get',
        Get_DataParent: 'CATGroupOfCost_GroupList',

        ExcelInit: 'CATGroupOfCost_ExcelInit',
        ExcelChange: 'CATGroupOfCost_ExcelChange',
        ExcelImport: 'CATGroupOfCost_ExcelImport',
        ExcelApprove: 'CATGroupOfCost_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    },
    ExcelKey: {
        Resource: "CATGroupOfCost_Excel",
        GroupOfCost: "CATGroupOfCost"
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfCost_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfCost_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATGroupOfCostItem = null

    $scope.CATGroupOfCost_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfCost.URL.Read,
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
                template: '<a href="/" ng-click="CATGroupOfCostEdit_Click($event,CATGroupOfCost_win,CATGroupOfCost_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfCostDestroy_Click($event,CATGroupOfCost_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATGroupOfCost.Code}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'GroupName', title: '{{RS.CATGroupOfCost.GroupName}}', width: 200, sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'ParentName', title: '{{RS.CATGroupOfCost.ParentName}}', sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false }, } },
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
    $scope.CATGroupOfCostEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;

        $scope.LoadItem(win, id);
    }

    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfCost.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.GetDataCboParent(id);
                $scope.CATGroupOfCostItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfCostDestroy_Click = function ($event, grid) {
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
                        method: _CATGroupOfCost.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATGroupOfCost_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATGroupOfCost_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATGroupOfCost.URL.Save,
                data: { item: $scope.CATGroupOfCostItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATGroupOfCost_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATGroupOfCost_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfCost_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATGroupOfCost_cboParent_Options = {
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

    $scope.GetDataCboParent = function (id) {
        
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfCost.URL.Get_DataParent,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATGroupOfCost_cboParent_Options.dataSource.data(res.Data);
            }
        })
    }

    $scope.CATGroupOfCost_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 4; i++) {
            var resource = $rootScope.RS[_CATGroupOfCost.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã nhóm chi phi] không được trống và > 50 ký tự',
                '[Mã nhóm chi phi] đã bị trùng',
                '[Tên nhóm chi phi] không được trống và > 50 ký tự',
                'Không có mã nhóm',
                'Không tồn tại mã nhóm  trong hệ thống',
            ];
        }
        $rootScope.excelShare.Init({
            functionkey: _CATGroupOfCost.ExcelKey.GroupOfCost,
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfCost.URL.ExcelInit,
            methodChange: _CATGroupOfCost.URL.ExcelChange,
            methodImport: _CATGroupOfCost.URL.ExcelImport,
            methodApprove: _CATGroupOfCost.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfCost_gridOptions.dataSource.read();
            }
        });
    };


    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);