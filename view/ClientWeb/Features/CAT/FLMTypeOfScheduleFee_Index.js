/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _FLMTypeOfScheduleFee = {
    URL: {
        Read: 'FLMTypeOfScheduleFee_List',
        Get: 'FLMTypeOfScheduleFee_Get',
        Save: 'FLMTypeOfScheduleFee_Save',
        Delete: 'FLMTypeOfScheduleFee_Delete',

        ExcelInit: 'FLMTypeOfScheduleFee_ExcelInit',
        ExcelChange: 'FLMTypeOfScheduleFee_ExcelChange',
        ExcelImport: 'FLMTypeOfScheduleFee_ExcelImport',
        ExcelApprove: 'FLMTypeOfScheduleFee_ExcelApprove',
    },
    Data: {
        _gridModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                TypeName: { type: 'string' },
                Code: { type: 'string' },
            }
        },
    }
};
//#endregion

angular.module('myapp').controller('FLMTypeOfScheduleFee_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FLMTypeOfScheduleFee_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.FLMTypeOfScheduleFeeItem = null

    $scope.FLMTypeOfScheduleFee_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _FLMTypeOfScheduleFee.URL.Read,
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
                field: "Command", title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="FLMTypeOfScheduleFeeEdit_Click($event,FLMTypeOfScheduleFee_win,FLMTypeOfScheduleFee_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="FLMTypeOfScheduleFeeDestroy_Click($event,FLMTypeOfScheduleFee_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.FLMTypeOfScheduleFee.Code}}', width: 150,sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeName', title: '{{RS.FLMTypeOfScheduleFee.TypeName}}',width:200,sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfScheduleFeeStatusName', title: '{{RS.FLMTypeOfScheduleFee.TypeOfScheduleFeeStatusName}}',sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.FLMTypeOfScheduleFee_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _FLMTypeOfScheduleFee.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.FLMTypeOfScheduleFeeItem = res;
                
                win.center();
                win.open();
            }
        });
    }

    $scope.FLMTypeOfScheduleFee_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.FLMTypeOfScheduleFeeItem.TypeOfScheduleFeeStatusID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _FLMTypeOfScheduleFee.URL.Save,
                    data: { item: $scope.FLMTypeOfScheduleFeeItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.FLMTypeOfScheduleFee_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                        })
                    }
                });
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn tình trạng', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.FLMTypeOfScheduleFee_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.FLMTypeOfScheduleFeeEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;

        $scope.LoadItem(win, id);
    }


    $scope.cboTypeOfScheduleFeeStatus_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarTypeOfScheduleFeeStatus,
        success: function (data) {
            $scope.cboTypeOfScheduleFeeStatus_Options.dataSource.data(data)
        }
    })

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

    $scope.FLMTypeOfScheduleFeeDestroy_Click = function ($event, grid) {
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
                        method: _FLMTypeOfScheduleFee.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.FLMTypeOfScheduleFee_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.FLMTypeOfScheduleFee_Excel_Click = function ($event) {
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
            functionkey: 'FLMTypeOfScheduleFee_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _FLMTypeOfScheduleFee.URL.ExcelInit,
            methodChange: _FLMTypeOfScheduleFee.URL.ExcelChange,
            methodImport: _FLMTypeOfScheduleFee.URL.ExcelImport,
            methodApprove: _FLMTypeOfScheduleFee.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.FLMTypeOfScheduleFee_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);