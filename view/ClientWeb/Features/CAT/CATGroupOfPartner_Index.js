/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfPartner = {
    URL: {
        Read: 'CATGroupOfPartner_Read',
        Delete: 'CATGroupOfPartner_Destroy',
        Save: 'CATGroupOfPartner_Update',
        Get: 'CATGroupOfPartner_Get',

        ExcelInit: 'CATGroupOfPartner_ExcelInit',
        ExcelChange: 'CATGroupOfPartner_ExcelChange',
        ExcelImport: 'CATGroupOfPartner_ExcelImport',
        ExcelApprove: 'CATGroupOfPartner_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    },
    ExcelKey: {
        Resource: "CATGroupOfPartner_Excel",
        GroupOfPartner: "CATGroupOfPartner"
    }

}

//#endregion

angular.module('myapp').controller('CATGroupOfPartner_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfPartner_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATGroupOfPartnerItem = null

    $scope.CATGroupOfPartner_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfPartner.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                field: 'Command', title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATGroupOfPartnerEdit_Click($event,CATGroupOfPartner_win,CATGroupOfPartner_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfPartnerDestroy_Click($event,CATGroupOfPartner_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATGroupOfPartner.Code}}', width: 250, sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { field: 'GroupName', title: '{{RS.CATGroupOfPartner.GroupName}}', sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } }, },
            { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
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

    $scope.CATGroupOfPartnerEdit_Click = function ($event, win, grid) {
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
            method: _CATGroupOfPartner.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATGroupOfPartnerItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfPartnerDestroy_Click = function ($event, grid) {
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
                        method: _CATGroupOfPartner.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATGroupOfPartner_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATGroupOfPartner_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATGroupOfPartner.URL.Save,
                data: { item: $scope.CATGroupOfPartnerItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATGroupOfPartner_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATGroupOfPartner_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfPartner_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.CATGroupOfPartner_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_CATGroupOfPartner.ExcelKey.Resource + '_' + i];
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
            functionkey: _CATGroupOfPartner.ExcelKey.GroupOfPartner,
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfPartner.URL.ExcelInit,
            methodChange: _CATGroupOfPartner.URL.ExcelChange,
            methodImport: _CATGroupOfPartner.URL.ExcelImport,
            methodApprove: _CATGroupOfPartner.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfPartner_gridOptions.dataSource.read();
            }
        });
    };
}]);