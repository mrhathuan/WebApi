/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATMaterial = {
    URL: {
        Read: 'CATMaterial_Read',
        Delete: 'CATMaterial_Destroy',
        Save: 'CATMaterial_Update',
        Get: 'CATMaterial_Get',

        ExcelInit: 'CATMaterial_ExcelInit',
        ExcelChange: 'CATMaterial_ExcelChange',
        ExcelImport: 'CATMaterial_ExcelImport',
        ExcelApprove: 'CATMaterial_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATMaterial_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATMaterial_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATMaterialItem = null

    $scope.CATMaterial_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATMaterial.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                    IsFuel: { type: 'boolean' }
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                field: "Command", title: ' ', width: '84px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATMaterialEdit_Click($event,CATMaterial_win,CATMaterial_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATMaterialDestroy_Click($event,CATMaterial_grid)" class="k-button"><i class="fa fa-trash"></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATMaterial.Code}}', width: 150,sortorder: 1, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaterialName', title: '{{RS.CATMaterial.MaterialName}}', width: 150, sortorder: 2, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfMaterialName', title: '{{RS.CATMaterial.GroupOfMaterialName}}', sortorder: 3, configurable: true, isfunctionalHidden: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
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

    $scope.CATMaterialEdit_Click = function ($event, win, grid) {
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
            method: _CATMaterial.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CATMaterialItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATMaterialDestroy_Click = function ($event, grid) {
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
                    $scope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATMaterial.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATMaterial_gridOptions.dataSource.read();
                            $scope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATMaterial_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.CATMaterialItem.GroupOfMaterialID) && $scope.CATMaterialItem.GroupOfMaterialID > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATMaterial.URL.Save,
                    data: { item: $scope.CATMaterialItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATMaterial_gridOptions.dataSource.read();
                        })
                    }
                });
            }
            else
                $rootScope.Message({ Msg: 'Chưa chọn nhóm vật tư', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.CATMaterial_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATMaterial_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATMaterial_cboGroupOfMaterial_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
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

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfMaterial,
        success: function (data) {
            $scope.CATMaterial_cboGroupOfMaterial_Options.dataSource.data(data);
        }
    })

    $scope.CATMaterial_Excel_Click = function ($event) {
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
                '[Mã nhóm vật tư] không được trống',
                '[Mã nhóm vật tư] không tồn tại'
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: '',
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _CATMaterial.URL.ExcelInit,
            methodChange: _CATMaterial.URL.ExcelChange,
            methodImport: _CATMaterial.URL.ExcelImport,
            methodApprove: _CATMaterial.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATMaterial_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);