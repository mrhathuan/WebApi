/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfTrouble = {
    URL: {
        Read: 'CATGroupOfTrouble_Read',
        Delete: 'CATGroupOfTrouble_Destroy',
        Save: 'CATGroupOfTrouble_Update',
        Get: 'CATGroupOfTrouble_Get',

        ExcelInit: 'CATGroupOfTrouble_ExcelInit',
        ExcelChange: 'CATGroupOfTrouble_ExcelChange',
        ExcelImport: 'CATGroupOfTrouble_ExcelImport',
        ExcelApprove: 'CATGroupOfTrouble_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfTrouble_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfTrouble_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATGroupOfTroubleItem = null

    $scope.CATGroupOfTrouble_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfTrouble.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Hour: { type: 'number' },
                    TimeTo: { type: 'date' },
                    TimeFrom: { type: 'date' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode:'row'},
        columns: [
            {
                field: "Command", title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATGroupOfTroubleEdit_Click($event,CATGroupOfTrouble_win,CATGroupOfTrouble_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfTroubleDestroy_Click($event,CATGroupOfTrouble_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATGroupOfTrouble.Code}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false ,filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Name', title: '{{RS.CATGroupOfTrouble.Name}}', width: 200, sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfGroupTroubleName', title: '{{RS.CATGroupOfTrouble.TypeOfGroupTroubleName}}', sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
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
    $scope.CATGroupOfTroubleEdit_Click = function ($event, win, grid) {
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
            method: _CATGroupOfTrouble.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATGroupOfTroubleItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfTroubleDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn có muốn xóa không',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATGroupOfTrouble.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATGroupOfTrouble_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATGroupOfTrouble_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATGroupOfTroubleItem.TypeOfGroupTroubleID) && $scope.CATGroupOfTroubleItem.TypeOfGroupTroubleID>0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATGroupOfTrouble.URL.Save,
                    data: { item: $scope.CATGroupOfTroubleItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATGroupOfTrouble_gridOptions.dataSource.read();
                        })
                    }
                });
            }
            else $rootScope.Message({ Msg: 'Chọn phân loại vấn đề cho chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATGroupOfTrouble_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfTrouble_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATGroupOfTrouble_cboType_Options = {
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
            $scope.CATGroupOfTrouble_cboType_Options.dataSource.data(data)
        }
    })

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.CATGroupOfTrouble_Excel_Click = function ($event) {
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
            functionkey: 'CATGroupOfTrouble_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfTrouble.URL.ExcelInit,
            methodChange: _CATGroupOfTrouble.URL.ExcelChange,
            methodImport: _CATGroupOfTrouble.URL.ExcelImport,
            methodApprove: _CATGroupOfTrouble.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfTrouble_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);