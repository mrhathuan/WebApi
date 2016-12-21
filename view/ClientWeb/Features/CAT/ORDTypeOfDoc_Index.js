/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _ORDTypeOfDoc = {
    URL: {
        Read: 'ORDTypeOfDoc_List',
        Get: 'ORDTypeOfDoc_Get',
        Save: 'ORDTypeOfDoc_Save',
        Delete: 'ORDTypeOfDoc_Delete',
        
        GetCBO: 'ALL_TypeOfWAInspectionStatus',
    },
    Data: {
        _gridModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                SortOrder: { type: 'number' },
                TypeName: { type: 'string' },
                Code: { type: 'string' },
                TypeOfWAInspectionStatusName: { type: 'string' },
            }
        },
    },
    ExcelKey: {
        Resource: "ORDTypeOfDoc_Excel",
        ORDTypeOfDoc: "ORDTypeOfDoc"
    }
};
//#endregion

angular.module('myapp').controller('ORDTypeOfDoc_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDTypeOfDoc_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.ORDTypeOfDocItem = null

    $scope.ORDTypeOfDoc_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _ORDTypeOfDoc.URL.Read,
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
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="ORDTypeOfDocEdit_Click($event,ORDTypeOfDoc_win,ORDTypeOfDoc_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="ORDTypeOfDocDestroy_Click($event,ORDTypeOfDoc_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.ORDTypeOfDoc.Code}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeName', title: '{{RS.ORDTypeOfDoc.TypeName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfWAInspectionStatusName', title: '{{RS.ORDTypeOfDoc.TypeOfWAInspectionStatusName}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };

    $scope.ORDTypeOfDoc_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _ORDTypeOfDoc.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.ORDTypeOfDocItem = res;
                
                win.center();
                win.open();
            }
        });

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _ORDTypeOfDoc.URL.GetCBO,
            data: {},
            success: function (res) {
                $scope.cboTypeOfDoc_Options.dataSource.data(res.Data);
            }
        });
    }

    $scope.cboTypeOfDoc_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
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
    };

    $scope.ORDTypeOfDoc_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _ORDTypeOfDoc.URL.Save,
                data: { item: $scope.ORDTypeOfDocItem },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                        $scope.ORDTypeOfDoc_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.ORDTypeOfDoc_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.ORDTypeOfDocEdit_Click = function ($event, win, grid) {
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

    $scope.ORDTypeOfDocDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _ORDTypeOfDoc.URL.Delete,
                    data: { 'item': item },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.ORDTypeOfDoc_gridOptions.dataSource.read();
                        })
                    }
                });
            }
        }
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);