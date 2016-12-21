/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _SYSGroup_Index = {
    URL: {
        Read: 'SYSGroup_Read',
        Parent: 'SYSGroup_Parent',
        Item: 'SYSGroup_Item',
        Save: 'SYSGroup_Save',
        Delete: 'SYSGroup_Delete',
    }
};
//#endregion

angular.module('myapp').controller('SYSGroup_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSGroup_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.TabIndex = 1;

    $scope.CustomerHasChoose = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.HasChoose = false;
    $scope.Item = null;

    $scope.Main_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
                Common.Log($scope.TabIndex)
            }, 1);
        }
    };

    $scope.sysgroup_treeOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _SYSGroup_Index.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsApproved: { type: 'bool', defaultValue: true }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#sysgroup_treeToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll"  type="checkbox" ng-click="gridChooseAll_Check($event,sysgroup_tree,sysgroup_treeChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sysgroup_tree,sysgroup_treeChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Edit_Click($event,dataItem,sysgroup_win)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSGroup.Code}}', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: '{{RS.SYSGroup.GroupName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: '{{RS.SYSGroup.IsApproved}}', width: '150px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />'
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.sysgroup_treeChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.SYSGroup,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

    $scope.sysgroup_gridCheck = function ($event, grid) {
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

    $scope.Add_Click = function ($event, win) {
        $event.preventDefault();

        $scope.LoadItem(win, -1);
    };

    $scope.Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    };

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSGroup_Index.URL.Item,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                var lstid = [];
                if (Common.HasValue(res.ListCustomerID))
                    lstid = res.ListCustomerID.split(',');
                angular.forEach(res.ListCustomer, function (v, i) {
                    if (lstid.indexOf(v.ID + '') >= 0)
                        v.IsChoose = true;
                    else
                        v.IsChoose = false;
                });
                $scope.Main_Tab.select(0);
                $timeout(function () {
                    $scope.customer_gridOptions.dataSource.data(res.ListCustomer);
                }, 1);
                win.center();
                win.open();
            }
        });
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
                    method: _SYSGroup_Index.URL.Delete,
                    data: { lstid: lstid },
                    success: function (res) {
                        $scope.sysgroup_treeOptions.dataSource.read();
                    }
                });
            }
        }
    };

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            var strCustomer = '';
            angular.forEach($scope.customer_gridOptions.dataSource.data(), function (v, i) {
                if (v.IsChoose == true)
                    strCustomer += ',' + v.ID;
            });
            if (strCustomer != '')
                strCustomer = strCustomer.substr(1);
            $scope.Item.ListCustomerID = strCustomer;

            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _SYSGroup_Index.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.sysgroup_treeOptions.dataSource.read();
                }
            });
        }
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };


    $scope.customer_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,customer_grid,customer_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,customer_grid,customer_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CUSCustomer.Code}}', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: '{{RS.CUSCustomer.CustomerName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfCustomerName', title: '{{RS.CUSCustomer.TypeOfCustomerName}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: ' ', filterable: false, sortable: false
            }
        ]
    };


    $scope.customer_gridChooseChange = function ($event, grid, haschoose) {
        $scope.CustomerHasChoose = haschoose;
    };
}]);