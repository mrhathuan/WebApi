/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfLocation = {
    URL: {
        Read: 'CATGroupOfLocation_Read',
        Delete: 'CATGroupOfLocation_Destroy',
        Save: 'CATGroupOfLocation_Save',
        Get: 'CATGroupOfLocation_Get',

        ExcelInit: 'CATGroupOfLocation_ExcelInit',
        ExcelChange: 'CATGroupOfLocation_ExcelChange',
        ExcelImport: 'CATGroupOfLocation_ExcelImport',
        ExcelApprove: 'CATGroupOfLocation_ExcelApprove',
    },
    ExcelKey: {
        Resource: "CATGroupOfLocation",
        GroupOfLocation: "CATGroupOfLocation"
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfLocation_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfLocation_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.CATGroupOfLocation_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfLocation.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CATGroupOfLocationName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#CATGroupOfLocation_grid_toolbar').html()),
        columns: [
            {
                field: 'Command', title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATGroupOfLocationEdit_Click($event,CATGroupOfLocation_winPopup,CATGroupOfLocation_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATGroupOfLocationDestroy_Click($event,CATGroupOfLocation_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATGroupOfLocation.Code}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: '{{RS.CATGroupOfLocation.GroupName}}', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'GroupOfPartnerName', title: '{{RS.CATGroupOfPartner.GroupOfPartnerName}}', sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.CATGroupOfLocationEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID);
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
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfLocation.URL.Get,
            data: { 'id': id },
            success: function (res) {
                $scope.Item = res;
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfLocationDestroy_Click = function ($event, grid) {
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
                        method: _CATGroupOfLocation.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATGroupOfLocation_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATGroupOfLocationSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.Item.GroupOfPartnerID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATGroupOfLocation.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        
                        $scope.CATGroupOfLocation_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn loại NPP', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.CATGroupOfLocationClose_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfLocationAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    };

    $scope.cboGOP_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfPartner,
        success: function (data) {
            $scope.cboGOP_Options.dataSource.data(data);
        }
    })

    $scope.CATGroupOfLocation_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 5; i++) {
            var resource = $rootScope.RS[_CATGroupOfLocation.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã loại địa chỉ] không được trống và > 50 ký tự',
                '[Mã địa chỉ bị trùng] đã bị trùng',
                '[Tên địa chỉ] không được trống và > 50 ký tự',
                'Không có mã loại',
                'Không tồn tại mã loại  trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _CATGroupOfLocation.ExcelKey.GroupOfLocation,
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfLocation.URL.ExcelInit,
            methodChange: _CATGroupOfLocation.URL.ExcelChange,
            methodImport: _CATGroupOfLocation.URL.ExcelImport,
            methodApprove: _CATGroupOfLocation.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfLocation_gridOptions.dataSource.read();
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);