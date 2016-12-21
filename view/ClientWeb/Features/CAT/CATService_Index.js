/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATService = {
    URL: {
        Read: 'CATService_List',
        Delete: 'CATService_Destroy',
        Save: 'CATService_Save',
        Get: 'CATService_Get',

        ListService: 'CATService_ServiceType',

        ExcelInit: 'CATService_ExcelInit',
        ExcelChange: 'CATService_ExcelChange',
        ExcelImport: 'CATService_ExcelImport',
        ExcelApprove: 'CATService_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    },
    ExcelKey: {
        Resource: "CATService_Excel",
        CATService: "CATService"
    }
}

//#endregion

angular.module('myapp').controller('CATService_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATService_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATServiceItem = null

    $scope.CATService_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATService.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ServiceName: { type: 'string' },
                    Summary: { type: 'string' },
                    PackingName: { type: 'string' },
                    CostName: { type: 'string' },
                    RevenueName: { type: 'string' },
                    GroupName: { type: 'string' }
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                field: 'Command', title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATServiceEdit_Click($event,CATService_win,CATService_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATServiceDestroy_Click($event,CATService_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATService.Code}}', width: 150, sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'ServiceName', title: '{{RS.CATService.ServiceName}}', sortorder: 2, configurable: true, isfunctionalHidden: false, width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'Summary', title: '{{RS.CATService.Summary}}', width: 150, sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'ServiceTypeName', title: '{{RS.CATService.ServiceTypeName}}', sortorder: 4, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { title: '', filterable: false, sortable: false, sortorder: 5, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.CATServiceEdit_Click = function ($event, win, grid) {
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
            ListView: views.CATService,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion
    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATService.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATServiceItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATServiceDestroy_Click = function ($event, grid) {
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
                        method: _CATService.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.CATService_gridOptions.dataSource.read();
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.CATService_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATServiceItem.ServiceTypeID) && $scope.CATServiceItem.ServiceTypeID > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATService.URL.Save,
                    data: { item: $scope.CATServiceItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATService_gridOptions.dataSource.read();
                        })
                    }
                });
            }
            else $rootScope.Message({ Msg: 'Chưa chọn loại dịch vụ', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATService_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATService_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    //#region cbo
    $scope.cboServiceOptions = {
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
        method: _CATService.URL.ListService,

        success: function (res) {
            $scope.cboServiceOptions.dataSource.data(res.Data);
        }
    });
    //#endregion

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.CATService_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 5; i++) {
            var resource = $rootScope.RS[_CATService.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã dich vụ] không được trống và > 50 ký tự',
                '[Mã dịch vụ] đã bị trùng',
                '[Tên dịch vụ] không được trống và > 50 ký tự',
                'không có mã loại dịch vụ',
                'không tồn tại mã loại trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _CATService.ExcelKey.CATService,
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATService.URL.ExcelInit,
            methodChange: _CATService.URL.ExcelChange,
            methodImport: _CATService.URL.ExcelImport,
            methodApprove: _CATService.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATService_gridOptions.dataSource.read();
            }
        });
    };
}]);