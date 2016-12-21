/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfMaterial = {
    URL: {
        Read: 'CATGroupOfMaterial_Read',
        Delete: 'CATGroupOfMaterial_Destroy',
        Save: 'CATGroupOfMaterial_Update',
        Get: 'CATGroupOfMaterial_Get',
        Get_DataParent: 'CATGroupOfMaterial_DropdownList_Read',

        ExcelInit: 'CATGroupOfMaterial_ExcelInit',
        ExcelChange: 'CATGroupOfMaterial_ExcelChange',
        ExcelImport: 'CATGroupOfMaterial_ExcelImport',
        ExcelApprove: 'CATGroupOfMaterial_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfMaterial_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfMaterial_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATGroupOfMaterialItem = null

    $scope.CATGroupOfMaterial_gridOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfMaterial.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                    IsFuel:{type:'boolean'}
                },
                expanded: false
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            { title: ' ', width: 20, filterable: false, sortable: false },
            {
                field: "Command", title: ' ', width: '100px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATGroupOfMaterialEdit_Click($event,CATGroupOfMaterial_win,CATGroupOfMaterial_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfMaterialDestroy_Click($event,CATGroupOfMaterial_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', sortorder: 1, configurable: true, isfunctionalHidden: false, title: '{{RS.CATGroupOfMaterial.Code}}', width: 150 },
            { field: 'GroupName', sortorder: 2, configurable: true, isfunctionalHidden: false, title: '{{RS.CATGroupOfMaterial.GroupName}}', width: 150 },
            { field: 'IsFuel', sortorder: 3, configurable: true, isfunctionalHidden: false, title: '{{RS.CATGroupOfMaterial.IsFuel}}', template: '<input type="checkbox" #=IsFuel?"":checked="checked"# disabled="disabled">' },
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
    $scope.CATGroupOfMaterialEdit_Click = function ($event, win, grid) {
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
            method: _CATGroupOfMaterial.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.GetDataCboParent(res.ID);
                $scope.CATGroupOfMaterialItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfMaterialDestroy_Click = function ($event, grid) {
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
                    $scope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATGroupOfMaterial.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATGroupOfMaterial_gridOptions.dataSource.read();
                            $scope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATGroupOfMaterial_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATGroupOfMaterial.URL.Save,
                data: { item: $scope.CATGroupOfMaterialItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.CATGroupOfMaterial_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.CATGroupOfMaterial_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfMaterial_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATGroupOfMaterial_cboParent_Options = {
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

    $scope.GetDataCboParent = function (id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfMaterial.URL.Get_DataParent,
            data: { 'id': id },
            success: function (data) {
                $scope.CATGroupOfMaterial_cboParent_Options.dataSource.data(data);
            }
        })
    }

    $scope.CATGroupOfMaterial_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                '[Tên] không được trống và > 50 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CATGroupOfMaterial_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfMaterial.URL.ExcelInit,
            methodChange: _CATGroupOfMaterial.URL.ExcelChange,
            methodImport: _CATGroupOfMaterial.URL.ExcelImport,
            methodApprove: _CATGroupOfMaterial.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfMaterial_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);