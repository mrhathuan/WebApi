/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATScale = {
    URL: {
        Read: 'CATScale_Read',
        Delete: 'CATScale_Destroy',
        Save: 'CATScale_Update',
        Get: 'CATScale_Get',

        ExcelInit: 'CATScale_ExcelInit',
        ExcelChange: 'CATScale_ExcelChange',
        ExcelImport: 'CATScale_ExcelImport',
        ExcelApprove: 'CATScale_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    },
    ExcelKey: {
        Resource: "CATScale_Excel",
        CATScale: "CATScale"
    }
}

//#endregion

angular.module('myapp').controller('CATScale_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATScale_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATScaleItem = null

    $scope.CATScale_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATScale.URL.Read,
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
                template: '<a href="/" ng-click="CATScaleEdit_Click($event,CATScale_win,CATScale_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATScaleDestroy_Click($event,CATScale_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATScale.Code}}', width: 250, sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'ScaleName', title: '{{RS.CATScale.ScaleName}}', sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
        ]
    };
    
    $scope.CATScaleEdit_Click = function ($event, win, grid) {
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
            method: _CATScale.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATScaleItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATScaleDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có muốn xóa không',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATScale.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATScale_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATScale_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) { 
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATScale.URL.Save,
                    data: { item: $scope.CATScaleItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATScale_gridOptions.dataSource.read();
                        })
                    }
                }); 
        }
    }

    $scope.CATScale_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATScale_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATScale_cboType_Options = {
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
            $scope.CATScale_cboType_Options.dataSource.data(data)
        }
    })

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.CATScale_Excel_Click = function ($event) {
        $event.preventDefault();
        
        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_CATScale.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã qui mô] không được trống và > 50 ký tự',
                '[Tên  qui mô] không được trống và > 50 ký tự',
                '[Mã qui mô] đã bị trùng',
            ];
        }
        $rootScope.excelShare.Init({
            functionkey: _CATScale.ExcelKey.CATScale,
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATScale.URL.ExcelInit,
            methodChange: _CATScale.URL.ExcelChange,
            methodImport: _CATScale.URL.ExcelImport,
            methodApprove: _CATScale.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATScale_gridOptions.dataSource.read();
            }
        });
    };
}]);