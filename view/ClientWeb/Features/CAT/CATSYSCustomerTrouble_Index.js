/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATSYSCustomerTrouble = {
    URL: {
        Read: 'CATSYSCustomerTrouble_SysCustomer_List',
    },
    
}

//#endregion

angular.module('myapp').controller('CATSYSCustomerTrouble_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATSYSCustomerTrouble_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.Param={id:0}

    $scope.CATSYSCustomerTrouble_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATSYSCustomerTrouble.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode:'row'},
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="CATSYSCustomerTroubleEdit_Click($event,CATSYSCustomerTrouble_grid)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'CustomerName', title: '{{RS.CUSCustomer.CustomerName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TelNo', title: '{{RS.CUSCustomer.TelNo}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Fax', title: '{{RS.CUSCustomer.Fax}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Email', title: '{{RS.CUSCustomer.Email}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    };
    
    $scope.CATSYSCustomerTroubleEdit_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        $scope.Param.id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) $scope.Param.id = item.ID;
        $state.go("main.CATSYSCustomerTrouble.Detail", $scope.Param)
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
    //#endregion
}]);