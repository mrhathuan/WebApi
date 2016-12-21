/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _SYSUserResource = {
    URL: {
        Read: 'SYSUserResource_List',
        Get: 'SYSUserResource_Get',
        Save: 'SYSUserResource_Save',
        Delete: 'SYSUserResource_Delete',
        ResetToDefault: 'SYSUserResource_SetDefault',

        ExcelExport: 'SYSUserResource_ExcelExport',
        ExcelCheck: 'SYSUserResource_ExcelCheck',
        ExcelImport: 'SYSUserResource_ExcelImport',
    }
};
//#endregion

angular.module('myapp').controller('SYSUserResource_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSUserResource_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = null;
    $scope.SYSUserResource_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _SYSUserResource.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
            pageSize:20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="SYSUserResource_Edit_Click($event,SYSUserResource_win,SYSUserResource_Grid,SYSUserResource_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'SYSKey', title: '{{RS.SYSResource.Key}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SYSName', title: '{{RS.SYSResource.Name}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SYSShortName', title: '{{RS.SYSResource.ShortName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: '{{RS.SYSUserResource.Name}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ShortName', title: '{{RS.SYSUserResource.ShortName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        change: function (e) {
            //debugger;
        }
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.SYSUserResource,
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

    $scope.SYSUserResource_Edit_Click = function ($event, win, grid, vform) {
        Common.Log('SYSUserResource_Edit_Click')
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSUserResource.URL.Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.SYSUserResource_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _SYSUserResource.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.SYSUserResource_GridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    };

    $scope.SYSUserResource_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.SYSUserResource_ExcelClick = function ($event, win) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            width: '900px',
            height: '500px',
            columns: [
                { field: 'SYSKey', title: '{{RS.SYSUserResource.SYSKey}}', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'SYSName', title: '{{RS.SYSUserResource.SYSName}}', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'Name', title: '{{RS.SYSUserResource.Name}}', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            ],
            Download: function ($event) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserResource.URL.ExcelExport,
                    data: {},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            },
            Upload: function (file, success) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserResource.URL.ExcelCheck,
                    data: {item:file},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        success(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            },
            Complete: function (file, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserResource.URL.ExcelImport,
                    data: {lst:data},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.SYSUserResource_GridOptions.dataSource.read();
                        win.close();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        });
    };

    $scope.SYSUserResource_SetDefault = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSUserResource.URL.ResetToDefault,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.SYSUserResource_GridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
}]);