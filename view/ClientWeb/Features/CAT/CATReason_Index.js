/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATReason = {
    URL: {
        Read: 'CATReason_List',
        Delete: 'CATReason_Delete',
        Save: 'CATReason_Save',
        Get: 'CATReason_Get',

        ExcelInit: 'CATReason_ExcelInit',
        ExcelChange: 'CATReason_ExcelChange',
        ExcelImport: 'CATReason_ExcelImport',
        ExcelApprove: 'CATReason_ExcelApprove',
    }
}

//#endregion

angular.module('myapp').controller('CATReason_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATReason_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = null;

    $scope.reason_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATReason.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ReasonName: { type: 'string' },
                    TypeOfReasonName: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#reason_grid_toolbar').html()), editable: 'inline',
        columns: [
            {
                field: "Command", title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="Edit_Click($event,win,reason_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Destroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'ReasonName', title: '{{RS.CATReason.ReasonName}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfReasonName', title: '{{RS.CATReason.TypeOfReasonName}}', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
        ]
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.TypeOfReasonName,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
    $scope.Edit_Click = function ($event, win, grid) {
        $event.preventDefault();
        Common.Log("Edit_Click");

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item))
            $scope.LoadItem(win, item.ID);
    }

    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATReason.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.Item = res;
                win.center().open();
            }
        });
    }

    $scope.cboTypeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfReason,
        success: function (data) {            
            $scope.cboTypeOptions.dataSource.data(data);
        }
    })

    $scope.Destroy_Click = function ($event, item) {
        $event.preventDefault();
        Common.Log("Destroy_Click");

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
                        method: _CATReason.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.reason_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATReason.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    $scope.reason_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.AddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminOther.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $rootScope.CATReason_Excel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.excelShare.Init({
            functionkey: 'CATReason_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATReason.URL.ExcelInit,
            methodChange: _CATReason.URL.ExcelChange,
            methodImport: _CATReason.URL.ExcelImport,
            methodApprove: _CATReason.URL.ExcelApprove,

            Changed: function () {

            },
            Approved: function () {
                $scope.reason_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);