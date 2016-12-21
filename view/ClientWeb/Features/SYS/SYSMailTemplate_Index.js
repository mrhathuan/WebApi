/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _SYSMailTemplate_Index = {
    URL: {
        Read: 'SYSMailTemplate_Read',
        Get: 'SYSMailTemplate_Get',
        Save: 'SYSMailTemplate_Save',

    }
};
//#endregion

angular.module('myapp').controller('SYSMailTemplate_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
 if ($rootScope.IsPageComplete != true) return;
 Common.Log('SYSMailTemplate_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.HasChoose = false;
    $scope.Item = null;

    $scope.SYSMailTemplate_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _SYSMailTemplate_Index.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    Code: { type: 'string' },
                    TemplateName: { type: 'string' },
                    CC: { type: 'string' },
                    Subject: { type: 'string' },
                    Content: { type: 'string' },
                    Details: { type: 'string' },
                    IsUse: { type: 'bool', defaultValue: true },
                    Note: { type: 'string', defaultValue: '' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Edit_Click($event,SYSMailTemplate_win,SYSMailTemplate_grid,SYSMailTemplate_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATMailTemplate.Code}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TemplateName', title: '{{RS.CATMailTemplate.TemplateName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsUse', title: '{{RS.CATMailTemplate.IsUse}}', width: '100px',
                template: '<input type="checkbox" #= IsUse ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.SYSMailTemplate_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.SYSMailTemplate_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    $scope.Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSMailTemplate_Index.URL.Get,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.Item = res;
                    }, 10);
                }
            }
        });

        win.center();
        win.open();
    }

    $scope.Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _SYSMailTemplate_Index.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.SYSMailTemplate_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật' });
                }
            });
        }
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.HTMLContent_View_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
    };

    $scope.HTMLContent_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.HTMLDetail_View_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
    };

    $scope.HTMLDetail_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };
}]);