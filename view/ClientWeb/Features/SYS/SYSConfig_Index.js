/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _urlSYSConfigGroup_Read = 'SYSConfigGroup_Read';
//#endregion

angular.module('myapp').controller('SYSConfig_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSConfig_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.IsDetail = false;
    $scope.sysconfig_groupOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.SYS,
            method: _urlSYSConfigGroup_Read,
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' }
                },
                expanded: false
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#sysgroup_treeToolbar').html()),
        columns: [
            {
                field: 'Code', title: '{{RS.SYSGroup.Code}}', width: '200px',
                template: '<a href="/" ng-click="sysgroup_Item_Click($event,sysconfig_group)">#=Code#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: '{{RS.SYSGroup.GroupName}}', width: '300px',
                template: '<a href="/" ng-click="sysgroup_Item_Click($event,sysconfig_group)">#=GroupName#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.sysgroup_Item_Click = function ($event, tree) {
        $event.preventDefault();

        var item = tree.dataItem($($event.target).closest('tr'));

        $state.go('main.SYSConfig.Detail', { id: item.ID });
    };
}]);