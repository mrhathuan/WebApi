/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATTypeBusiness = {
    URL: {
        Read: 'CATTypeBusiness_Read',
        Delete: 'CATTypeBusiness_Destroy',
        Save: 'CATTypeBusiness_Update',
        Get: 'CATTypeBusiness_Get',

        ExcelInit: 'CATTypeBusiness_ExcelInit',
        ExcelChange: 'CATTypeBusiness_ExcelChange',
        ExcelImport: 'CATTypeBusiness_ExcelImport',
        ExcelApprove: 'CATTypeBusiness_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    },
    ExcelKey: {
        Resource: "CATTypeBusiness_Excel",
        Business: "CATTypeBusiness"
    }

}

//#endregion

angular.module('myapp').controller('CATTypeBusiness_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATTypeBusiness_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATTypeBusinessItem = null

    $scope.CATTypeBusiness_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATTypeBusiness.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode:'row'},
        columns: [
            {
                field: 'Command', title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATTypeBusinessEdit_Click($event,CATTypeBusiness_win,CATTypeBusiness_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATTypeBusinessDestroy_Click($event,CATTypeBusiness_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATTypeBusiness.Code}}', width: 250, sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'TypeBusinessName', title: '{{RS.CATTypeBusiness.TypeBusinessName}}', sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
        ]
    };
    
    $scope.CATTypeBusinessEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;

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
            method: _CATTypeBusiness.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATTypeBusinessItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATTypeBusinessDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn có muốn xóa',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATTypeBusiness.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATTypeBusiness_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATTypeBusiness_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) { 
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATTypeBusiness.URL.Save,
                    data: { item: $scope.CATTypeBusinessItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATTypeBusiness_gridOptions.dataSource.read();
                        })
                    }
                }); 
        }
    }

    $scope.CATTypeBusiness_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATTypeBusiness_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATTypeBusiness_cboType_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SLI_SYSVarTypeOfGroupTrouble,
        success: function (data) {
            $scope.CATTypeBusiness_cboType_Options.dataSource.data(data)
        }
    })

    $scope.CATTypeBusiness_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_CATTypeBusiness.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã ] không được trống và > 50 ký tự',
                '[Tên ] không được trống và > 50 ký tự',
                '[Mã ] đã bị trùng',
            ];
        }
        $rootScope.excelShare.Init({
            functionkey: _CATTypeBusiness.ExcelKey.Business,
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATTypeBusiness.URL.ExcelInit,
            methodChange: _CATTypeBusiness.URL.ExcelChange,
            methodImport: _CATTypeBusiness.URL.ExcelImport,
            methodApprove: _CATTypeBusiness.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATTypeBusiness_gridOptions.dataSource.read();
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);