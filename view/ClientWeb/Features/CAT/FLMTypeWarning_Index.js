/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _FLMTypeWarning = {
    URL: {
        Read: 'FLMTypeWarning_List',
        Delete: 'FLMTypeWarning_Delete',
        Save: 'FLMTypeWarning_Save',
        Get: 'FLMTypeWarning_Get',

        //ExcelInit: 'CATCountry_ExcelInit',
        //ExcelChange: 'CATCountry_ExcelChange',
        //ExcelImport: 'CATCountry_ExcelImport',
        //ExcelApprove: 'CATCountry_ExcelApprove',
    },
}


angular.module('myapp').controller('FLMTypeWarning_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FLMTypeWarning_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.FLMTypeItem = null

    $scope.FLMTypeWarning_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _FLMTypeWarning.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    WarningName: { type: 'string' },
                    FLMTypeWarningName:{type:'string'},
                    IsDate: { type: 'bool' },
                    CreatedDate: { type: 'date' },
                    ModifiedDate: { type: 'date' },
                    CreatedBy: { type: 'string' },
                    ModifiedBy: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#FLMTypeWarning_grid_toolbar').html()),
        columns: [
            {
                field: 'Command', title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="FLMTypeWarningEdit_Click($event,FLMTypeWarning_win,FLMTypeWarning_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="FLMTypeWarningDestroy_Click($event,FLMTypeWarning_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 200,sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'WarningName', title: 'Tên cảnh báo', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FLMTypeWarningName', title: 'Loại cảnh báo', width: 200, sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };


    $scope.FLMTypeWarningEdit_Click = function ($event, win, grid) {
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
            method: _FLMTypeWarning.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.FLMTypeItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.FLMTypeWarningDestroy_Click = function ($event, grid) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa các dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _FLMTypeWarning.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.FLMTypeWarning_gridOptions.dataSource.read();
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.FLMTypeWarningAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.FLMTypeWarningSave_Click = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _FLMTypeWarning.URL.Save,
                    data: { item: $scope.FLMTypeItem },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $rootScope.IsLoading = false;
                        win.close();
                        $scope.FLMTypeWarning_gridOptions.dataSource.read();
                    }
                });
        }
    }

    $scope.FLMTypeWarningClose_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.cboFLMTypeWarningOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {

        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarFLMTypeWarning,
        success: function (res) {
            debugger
            $scope.cboFLMTypeWarningOptions.dataSource.data(res);
        }
    })

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
       
        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: '',
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);