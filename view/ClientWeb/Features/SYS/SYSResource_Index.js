/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _SYSResource = {
    URL: {
        Read: 'SYSResource_List',
        Get: 'SYSResource_Get',
        Save: 'SYSResource_Save',
        Delete: 'SYSResource_Delete',
        ExcelExport: 'SYSResource_ExcelExport',
        ExcelCheck: 'SYSResource_ExcelCheck',
        ExcelImport: 'SYSResource_ExcelImport',
        ChangeDefault: 'SYSResource_ChangeDefault'
    },
    Data: {
        Language: [
            { Value: 0, Text: 'Không chọn' },
            { Value: 1, Text: 'Tiếng Việt' },
            { Value: 2, Text: 'Tiếng Anh' },
        ]
    }
};
//#endregion

angular.module('myapp').controller('SYSResource_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSResource_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    
    $scope.Item = null;
    $scope.LanguageName = 0;

    $scope.ParamSearch = 0;

    $scope.cboLang_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'Value',
                fields: {
                    Value: { type: 'number' },
                    Text: { type: 'string' },
                }
            }
        })
    }

    $scope.SYSResource_SearchClick = function ($event, grid) {
        $event.preventDefault();
        var val = $scope.cboLang.value();
        if (!Common.HasValue(val) || val < 0) {
            $rootScope.Message({ Msg: 'Ngôn ngữ không chính xác', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $scope.ParamSearch = val;
            grid.dataSource.read();
        }
    };

    $scope.cboLang_Options.dataSource.data(_SYSResource.Data.Language)

    $scope.SYSResource_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _SYSResource.URL.Read,
            readparam: function () { return { langID: $scope.ParamSearch } },
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
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="SYSResource_Edit_Click($event,SYSResource_win,SYSResource_Grid,SYSResource_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="SYSResource_Delete_Click($event,SYSResource_Grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Key', title: 'Mã hệ thống', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: 'Tên', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ShortName', title: 'Tên ngắn', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        change: function (e) {
            //debugger;
        }
    };

    $scope.SYSResource_AddNewClick = function ($event, win, vform) {
        Common.Log('SYSResource_AddNewClick')
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.SYSResource_Edit_Click = function ($event, win, grid, vform) {
        Common.Log('SYSResource_Edit_Click')
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };
    

    $scope.SYSResource_Delete_Click = function ($event, grid) {
        Common.Log('SYSResource_Delete_Click')
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
                        url: Common.Services.url.SYS,
                        method: _SYSResource.URL.Delete,
                        data: { item: item, langID: $scope.ParamSearch },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });

                }
            })
        }
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSResource.URL.Get,
            data: { id: id, langID: $scope.ParamSearch },
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

    $scope.SYSResource_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _SYSResource.URL.Save,
                data: { item: $scope.Item, langID: $scope.ParamSearch },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.SYSResource_GridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.SYSResource,
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
    $scope.SYSResource_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.SYSResource_ExcelClick = function ($event, win) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            width: '900px',
            height: '500px',
            columns: [
                { field: 'Key', title: 'Mã hệ thống', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'Name', title: 'Tên', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'ShortName', title: 'Tên ngắn', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            ],
            Download: function ($event) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSResource.URL.ExcelExport,
                    data: { langID: $scope.ParamSearch },
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
                    method: _SYSResource.URL.ExcelCheck,
                    data: { item: file, langID: $scope.ParamSearch },
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
                    method: _SYSResource.URL.ExcelImport,
                    data: { lst: data, langID: $scope.ParamSearch },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.SYSResource_GridOptions.dataSource.read();
                        win.close();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        });
    };

    $scope.SYSResource_SetDefaultClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn đặt ngôn ngữ hiện tại là ngôn ngữ hệ thống?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSResource.URL.ChangeDefault,
                    data: { langID:$scope.ParamSearch },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật!', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    };
}]);