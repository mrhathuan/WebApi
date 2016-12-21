/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _SYSConfig_Detail = {
    URL: {
        GroupItem: 'SYSConfigGroup_GroupItem',
        InRead: 'SYSConfigFunction_InRead',
        NotInRead: 'SYSConfigFunction_NotInRead',
        AddFunction: 'SYSConfigFunction_AddFunction',
        DelFunction: 'SYSConfigFunction_DelFunction',
        GetItem: 'SYSConfigFunction_GetItem',
        SaveItem: 'SYSConfigFunction_SaveItem',
    }
};
//#endregion

angular.module('myapp').controller('SYSConfig_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSConfig_DetailCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    var id = $stateParams.id
    $scope.Close_Click = function ($event) {
        $event.preventDefault();

        $scope.$parent.IsDetail = false;
        $state.go('main.SYSConfig.Index');
    };

    $scope.Add_Click = function ($event, tree) {
        $event.preventDefault();

        $scope.sysconfig_functionnotinOptions.dataSource.read();
        var lst = [];
        angular.forEach(tree.dataItems(), function (v, i) {
            if (v.IsChoose)
                lst.push(v);
        });
        if (lst.length > 0) {
            if (confirm('Bạn muốn thêm các chức năng đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSConfig_Detail.URL.AddFunction,
                    data: { lst: lst, groupid: $scope.GroupItem.ID },
                    success: function (res) {

                        $scope.sysconfig_functioninOptions.dataSource.read();
                        $scope.sysconfig_functionnotinOptions.dataSource.read();
                    }
                });
            }
        }
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.SYSConfigDetail,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //end #region
    $scope.Del_Click = function ($event, tree) {
        $event.preventDefault();

        $scope.sysconfig_functioninOptions.dataSource.read();
        var lst = [];
        angular.forEach(tree.dataItems(), function (v, i) {
            if (v.IsChoose)
                lst.push(v);
        });
        if (lst.length > 0) {
            if (confirm('Bạn muốn xóa các chức năng đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSConfig_Detail.URL.DelFunction,
                    data: { lst: lst, groupid: $scope.GroupItem.ID },
                    success: function (res) {

                        $scope.sysconfig_functioninOptions.dataSource.read();
                        $scope.sysconfig_functionnotinOptions.dataSource.read();
                    }
                });
            }
        }
    };

    $scope.GroupItem = { ID: id, FunctionID: -1, FunctionName: '', GroupName: '', Code: '' };
    Common.Services.Call($http, {
        url: Common.Services.url.SYS,
        method: _SYSConfig_Detail.URL.GroupItem,
        data: { groupid: id },
        success: function (res) {
            $scope.GroupItem.GroupName = res.GroupName;
            $scope.GroupItem.Code = res.Code;
            //$timeout(function () {
            //    //$scope.sysconfig_functioninOptions.dataSource.read();
            //    //$scope.sysconfig_functionnotinOptions.dataSource.read();
            //}, 1);   
            //debugger;
        }
    });

    $scope.sysconfig_functioninOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.SYS,
            method: _SYSConfig_Detail.URL.InRead,
            readparam: function () {
                if (Common.HasValue($scope.GroupItem))
                    return { groupid: $scope.GroupItem.ID };
                else
                    return { groupid: -1 };
            },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false }
                },
                expanded: false
            }
        }),
        pageable: true, sortable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,sysconfig_functionin,sysconfig_functioninChooseChange)" />',
                headerAttributes: { style: 'padding-left:21px;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sysconfig_functionin,sysconfig_functioninChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSFunction.Code}}', width: '150px',
                template: '<a href="/" ng-click="Edit_Click($event,dataItem,sysconfig_win)">#=Code#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FunctionName', title: '{{RS.SYSFunction.FunctionName}}', width: '300px',
                template: '<a href="/" ng-click="Edit_Click($event,dataItem,sysconfig_win)">#=FunctionName#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
            debugger
        }
    };

    $scope.IsDel = false;
    $scope.sysconfig_functioninChooseChange = function ($event, tree, ischoose) {
        $scope.IsDel = ischoose;

        var chk = $event.target;
        var tr = $(chk).closest('tr');
        var item = tree.dataItem(tr);

        var lstID = [];
        var choose = $(chk).prop('checked');
        lstID.push(item.ID);
        angular.forEach(tree.items(), function (v, i) {
            chk = $(v).find('.chkChoose');
            tr = $(chk).closest('tr');
            item = tree.dataItem(tr);

            if (item.parentId > 0 && lstID.indexOf(item.parentId) >= 0) {
                if (choose == true) {
                    $(chk).prop('checked', true);
                    item.IsChoose = true;
                    lstID.push(item.ID);

                    if (!tr.hasClass('IsChoose'))
                        tr.addClass('IsChoose');
                }
                else {
                    $(chk).prop('checked', false);
                    item.IsChoose = false;
                    lstID.push(item.ID);

                    if (tr.hasClass('IsChoose'))
                        tr.removeClass('IsChoose');
                }
            }
        });
    };

    $scope.sysconfig_functionnotinOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.SYS,
            method: _SYSConfig_Detail.URL.NotInRead,
            readparam: function () {
                if (Common.HasValue($scope.GroupItem))
                    return { groupid: $scope.GroupItem.ID };
                else
                    return { groupid: -1 };
            },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false }
                },
                expanded: false
            }
        }),
        pageable: true, sortable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,sysconfig_functionnotin,sysconfig_functionnotinChooseChange)" />',
                headerAttributes: { style: 'padding-left:21px;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sysconfig_functionnotin,sysconfig_functionnotinChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSFunction.Code}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FunctionName', title: '{{RS.SYSFunction.FunctionName}}', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.IsAdd = false;
    $scope.sysconfig_functionnotinChooseChange = function ($event, tree, ischoose) {
        $scope.IsAdd = ischoose;

        var chk = $event.target;
        var tr = $(chk).closest('tr');
        var item = tree.dataItem(tr);

        var lstID = [];
        var choose = $(chk).prop('checked');
        lstID.push(item.ID);
        //check parent
        var parentId = item.parentId;
        if (ischoose) {
            while (Common.HasValue(parentId) && parentId > 0) {
                var parent;
                var keepGoing = true;
                angular.forEach(tree.items(), function (v, i) {
                    if (keepGoing) {
                        chk = $(v).find('.chkChoose');
                        tr = $(chk).closest('tr');
                        parent = tree.dataItem(tr);

                        if (parent.ID == parentId) {
                            $(chk).prop('checked', true);
                            parent.IsChoose = true;
                            //lstID.push(parent.ID);

                            if (!tr.hasClass('IsChoose'))
                                tr.addClass('IsChoose');
                            keepGoing = false;
                        }
                    }
                });
                parentId = parent.parentId;
            }
        }
        //check child
        angular.forEach(tree.items(), function (v, i) {
            chk = $(v).find('.chkChoose');
            tr = $(chk).closest('tr');
            item = tree.dataItem(tr);

            if (item.parentId > 0 && lstID.indexOf(item.parentId) >= 0) {
                if (choose == true) {
                    $(chk).prop('checked', true);
                    item.IsChoose = true;
                    lstID.push(item.ID);

                    if (!tr.hasClass('IsChoose'))
                        tr.addClass('IsChoose');
                }
                else {
                    $(chk).prop('checked', false);
                    item.IsChoose = false;
                    lstID.push(item.ID);

                    if (tr.hasClass('IsChoose'))
                        tr.removeClass('IsChoose');
                }
            }
        });
    };


    $scope.sysconfig_paneOptions = {
        orientation: 'horizontal',
        panes: [
            { collapsible: true, resizable: true, size: '50%' },
            { collapsible: true, resizable: true, size: '50%' }
        ]
    };

    $scope.sysconfig_win_gridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsApproved: { type: 'bool' },
                    IsView: { type: 'bool' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,sysconfig_win_grid)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sysconfig_win_grid)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSAction.Code}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ActionName', title: '{{RS.SYSAction.ActionName}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsView', title: '{{RS.SYSAction.IsView}}', width: '100px',
                template: '<input type="checkbox" #= IsView==true ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.Item = { IsApproved: false, ListActions: [] };

    $scope.Edit_Click = function ($event, dataItem, sysconfig_win) {
        $event.preventDefault();

        $scope.GroupItem.FunctionID = dataItem.ID;
        $scope.GroupItem.FunctionName = dataItem.FunctionName;

        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSConfig_Detail.URL.GetItem,
            data: { groupid: $scope.GroupItem.ID, functionid: $scope.GroupItem.FunctionID },
            success: function (res) {
                $scope.Item = res;
                $timeout(function () {
                    angular.forEach(res.ListActions, function (v, i) {
                        v.IsChoose = v.IsApproved;
                    });

                    $scope.sysconfig_win_gridOptions.dataSource.data(res.ListActions);
                    $timeout(function () {
                        $scope.sysconfig_win_grid.resize();
                    },10);
                    sysconfig_win.open();
                    sysconfig_win.center();
                }, 1);
            }
        });
    };

    $scope.Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        $scope.Item.ListActions = $scope.sysconfig_win_gridOptions.dataSource.data();
        angular.forEach($scope.Item.ListActions, function (v, i) {
            v.IsApproved = v.IsChoose;
        });

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn lưu dữ liệu ?',
            pars: {},
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSConfig_Detail.URL.SaveItem,
                    data: { groupid: $scope.GroupItem.ID, functionid: $scope.GroupItem.FunctionID, item: $scope.Item },
                    success: function (res) {
                        win.close();
                        $rootScope.Message({ Msg: 'Đã cập nhật', NotifyType: Common.Message.NotifyType.ERROR });
                    }
                });
            }
        });
    };

    $scope.WinClose_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };
}]);