/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _SYSFunction_Index = {
    URL: {
        Read: 'SYSFunction_Read',
        Parent: 'SYSFunction_Parent',
        Item: 'SYSFunction_Item',
        Delete: 'SYSFunction_Delete',
        Move: 'SYSFunction_Move',
        Save: 'SYSFunction_Save',
        Refresh: 'SYSFunction_Refresh',

        Export: 'SYSFunction_Export',
        ImportCheck: 'SYSFunction_ImportCheck',
        Import: 'SYSFunction_ExcelImport',
    }
};
//#endregion

angular.module('myapp').controller('SYSFunction_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSFunction_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.HasChoose = false;
    $scope.Item = null;
    $scope.sysfunction_treeOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.SYS,
            method: _SYSFunction_Index.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsAdmin: { type: 'bool', defaultValue: false },
                    IsApproved: { type: 'bool', defaultValue: true }
                },
                expanded: true
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#sysfunction_treeToolbar').html()),
        columns: [
            {
                title: ' ', width: '120px',
                headerTemplate: '<input class="chkSelectAll" style="margin-left:19px;" type="checkbox" ng-click="gridChooseAll_Check($event,sysfunction_tree,sysfunction_treeChooseChange)" />',
                
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sysfunction_tree,sysfunction_treeChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Edit_Click($event,sysfunction_win,sysfunction_tree)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Up_Click($event,dataItem)" class="k-button"><i class="fa fa-angle-up"></i></a>',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Down_Click($event,dataItem)" class="k-button"><i class="fa fa-angle-down"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSFunction.Code}}', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FunctionName', title: '{{RS.SYSFunction.FunctionName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Icon', title: '{{RS.SYSFunction.Icon}}', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: '{{RS.SYSFunction.IsApproved}}', width: '50px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />'
            },
            {
                field: 'IsAdmin', title: '{{RS.SYSFunction.IsAdmin}}', width: '50px',
                template: '<input type="checkbox" #= IsAdmin ? "checked=checked" : "" # disabled="disabled" />'
            },
            {
                field: 'SortOrder', title: '{{RS.SYSFunction.SortOrder}}', width: '100px',
                sortable: true, filterable: false, menu: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.sysfunction_treeChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.SYSFunction,
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

    $scope.sysfunction_gridCheck = function ($event, grid) {
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose'))
                $(tr).addClass('IsChoose');
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose'))
                $(tr).removeClass('IsChoose');
        }
    };

    $scope.Refresh_Click = function ($event, tree) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSFunction_Index.URL.Refresh,
            data: {},
            success: function (res) {
                tree.dataSource.read();
            }
        });
    };

    $scope.Add_Click = function ($event, win) {
        $event.preventDefault();

        $scope.LoadItem(win, -1);
    };

    $scope.Edit_Click = function ($event, win, grid) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID);
    };

    $scope.Up_Click = function ($event, item) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSFunction_Index.URL.Move,
            data: { id: item.ID, typeid: -1 },
            success: function (res) {
                $rootScope.IsLoading = false;
                //debugger;
                $scope.sysfunction_tree.dataSource.read();
            }
        });
    };

    $scope.Down_Click = function ($event, item) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSFunction_Index.URL.Move,
            data: { id: item.ID, typeid: 1 },
            success: function (res) {
                $rootScope.IsLoading = false;
                //debugger;
                $scope.sysfunction_tree.dataSource.read();
            }
        });
    };

    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSFunction_Index.URL.Parent,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.sysfunction_win_cboOptions.dataSource.data(res);
                    }, 1);
                }

                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSFunction_Index.URL.Item,
                    data: { id: id },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            $scope.Item = res;

                            $timeout(function () {
                                $scope.sysfunction_win_gridOptions.dataSource.data(res.ListActions);
                            }, 1);
                        }
                    }
                });
            }
        });


        win.center();
        win.open();
    }

    $scope.Del_Click = function ($event, grid) {
        $event.preventDefault();

        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSFunction_Index.URL.Delete,
                    data: { lstid: lstid },
                    success: function (res) {
                        $scope.sysfunction_treeOptions.dataSource.read();
                    }
                });
            }
        }
    };

    $scope.Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        $scope.Item.ListActions = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                $scope.Item.ListActions.push({ ID: v.ID });
        });

        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _SYSFunction_Index.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $scope.sysfunction_treeOptions.dataSource.read();
                }
            });
        }
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.sysfunction_win_cboOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Text', dataValueField: 'ValueInt',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ValueInt',
                fields: {
                    ValueInt: { type: 'number' },
                    ValueString: { type: 'string' },
                    Text: { type: 'string' }
                }
            }
        })
    };

    $scope.sysfunction_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    $scope.sysfunction_win_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 10,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    ActionName: { type: 'string' },
                    IsView: { type: 'bool' },
                    IsApproved: { type: 'bool' },
                    IsChoose: { type: 'bool' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input type="checkbox" ng-click="sysfunction_win_gridChooseAll($event,sysfunction_win_grid)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="sysfunction_win_gridChoose($event,sysfunction_win_grid)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSFunction.Code}}', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ActionName', title: '{{RS.SYSFunction.ActionName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsView', title: '{{RS.SYSFunction.IsView}}', width: '80px',
                template: '<input disabled="disabled" type="checkbox" #= IsView ? "checked=checked" : "" #/>',
                templateAttributes: { style: 'text-align: center;' },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: '{{RS.SYSFunction.IsApproved}}', width: '80px',
                template: '<input disabled="disabled" type="checkbox" #= IsApproved ? "checked=checked" : "" #/>',
                templateAttributes: { style: 'text-align: center;' },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: ' ', filterable: false, sortable: false
            }
        ]
    };

    $scope.sysfunction_win_gridChooseAll = function ($event, grid) {
        if ($event.target.checked == true) {
            grid.tbody.find('tr[role="row"]').each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose != true) {
                    $(tr).find('td input.chkChoose').prop('checked', true);
                    item.IsChoose = true;
                }
            });
        }
        else {
            grid.tbody.find('tr[role="row"]').each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose == true) {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                }
            });
        }
    };

    $scope.sysfunction_win_gridChoose = function ($event, grid) {
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
        }
        else {
            item.IsChoose = false;
        }
    };

    $scope.Excel_Click = function ($event, grid) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            width: '900px',
            height: '500px',
            columns: [
                { field: 'Code', title: '{{RS.SYSFunction.Code}}', width: '80px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'FunctionName', title: '{{RS.SYSFunction.FunctionName}}', width: '80px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            ],
            Download: function ($event) {
                $rootScope.IsLoading = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSFunction_Index.URL.Export,
                    data: {},
                    success: function (res) {
                        $rootScope.IsLoading = false;

                        $rootScope.DownloadFile(res);
                    }
                });
            },
            Upload: function (file, success) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSFunction_Index.URL.ImportCheck,
                    data: { file: file },
                    success: function (res) {
                        success(res);
                        $rootScope.IsLoading = false;
                    }
                });
            },
            Complete: function (file, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSFunction_Index.URL.Import,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Đã cập nhật' });
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    };
}]);