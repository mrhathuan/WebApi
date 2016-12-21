/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _CUSContract_Index = {
    URL: {
        Read: 'CUSContract_List'
    }
}

angular.module('myapp').controller('CUSContract_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CUSContract_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.contract_grid = null;

    //#region Auth
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

    $scope.contract_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSContract_Index.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    EffectDate: { type: 'date' },
                    ExpiredDate: { type: 'date' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                field: 'CustomerCode', width: 200, title: 'Mã khách hàng', template: "<a class='text' ng-click='DetailV2_Click($event, dataItem)' href='#=URL#'>#=CustomerCode==null?\"\":CustomerCode#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: 250, title: 'Khách hàng', template: "<a class='text' ng-click='DetailV2_Click($event, dataItem)' href='#=URL#'>#=CustomerName==null?\"\":CustomerName#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractNo', width: 200, title: 'Số hợp đồng', template: "<a class='text' ng-click='DetailV2_Click($event, dataItem)' href='#=URL#'>#=ContractNo==null?\"\":ContractNo#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DisplayName', width: 250, title: 'Tên hiển thị', template: "<a class='text' ng-click='DetailV2_Click($event, dataItem)' href='#=URL#'>#=DisplayName==null?\"\":DisplayName#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeName', width: 100, title: 'Hình thức v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContractName', title: 'Loại hợp đồng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EffectDate', width: 120, title: 'Ngày hiệu lực', template: "#=EffectDate==null?' ':kendo.toString(EffectDate, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ExpiredDate', width: 120, title: 'Ngày hết hạn', template: "#=ExpiredDate==null?' ':kendo.toString(ExpiredDate, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
           
            { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.$watch('contract_grid', function (newValue, oldValue) {
        if (newValue != null)
            Common.Controls.Grid.ReorderColumns({ Grid: newValue, CookieName: 'CUSContract_Grid' });
    });

    $scope.DetailV2_Click = function ($event, item) {
        $event.preventDefault();
        $state.go("main.CUSCustomer.Contract", { ID: item.ID, CustomerID: 0 });
    }

    $scope.New_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.CUSCustomer.Contract", { ID: -1, CustomerID: 0 });
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