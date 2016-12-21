/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _SYSGroupChild_Index = {
    URL: {
        Read: 'SYSGroupChild_Read',
        Parent: 'SYSGroup_Parent',
        Item: 'SYSGroupChild_Item',
        Save: 'SYSGroupChild_Save',
        Delete: 'SYSGroupChild_Delete',
    }
};
//#endregion

angular.module('myapp').controller('SYSGroupChild_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSGroupChild_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.HasChoose = false;
    $scope.Item = null;
    $scope.SYSGroupChild_treeOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _SYSGroupChild_Index.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsApproved: { type: 'bool', defaultValue: true }
                },
                expanded: false
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#SYSGroupChild_treeToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll"  type="checkbox" ng-click="gridChooseAll_Check($event,SYSGroupChild_tree,SYSGroupChild_treeChooseChange)" />',
                headerAttributes: { style: 'text-align: left;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SYSGroupChild_tree,SYSGroupChild_treeChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Edit_Click($event,SYSGroupChild_win,SYSGroupChild_tree,SYSGroupChild_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSGroupChild.Code}}', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: '{{RS.SYSGroupChild.GroupName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: '{{RS.SYSGroupChild.IsApproved}}', width: '150px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />'
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.SYSGroupChild_treeChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: '',
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    $scope.SYSGroupChild_gridCheck = function ($event, grid) {
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
            method: _SYSGroupChild_Index.URL.Parent,
            data: { id: id },
            
            success: function (res) {
                debugger
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.SYSGroupChild_cboOptions.dataSource.data(res);
                    }, 1);
                }

                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSGroupChild_Index.URL.Item,
                    data: { id: id },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            $scope.Item = res;
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
                    method: _SYSGroupChild_Index.URL.Delete,
                    data: { lstid: lstid },
                    success: function (res) {
                        $scope.SYSGroupChild_treeOptions.dataSource.read();
                    }
                });
            }
        }
    };

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _SYSGroupChild_Index.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.SYSGroupChild_treeOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.SYSGroupChild_cboOptions = {
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
}]);