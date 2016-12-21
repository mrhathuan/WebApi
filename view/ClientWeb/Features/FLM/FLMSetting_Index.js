/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSettingIndex = {
    URL: {
        Read: 'FLMContract_List'
    }
}

angular.module('myapp').controller('FLMSetting_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.contract_grid = null;
    $scope.contract_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingIndex.URL.Read,
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
                field: 'ContractNo', width: 200, title: 'Số hợp đồng', template: "<a class='text' ng-click='Detail_Click($event, dataItem)' href='#=URL#'>#=ContractNo==null?\"\":ContractNo#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DisplayName', width: 250, title: 'Tên hiển thị', template: "<a class='text' ng-click='Detail_Click($event, dataItem)' href='#=URL#'>#=DisplayName==null?\"\":DisplayName#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', width: 100, title: 'Dịch vụ',
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
             {
                 field: 'CustomerCode', width: 100, title: 'Mã khách hàng',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
              {
                  field: 'CustomerName', width: 150, title: 'Tên khách hàng',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
               {
                   field: 'ShortName', width: 150, title: 'Tên ngắn',
                   filterable: { cell: { operator: 'contains', showOperators: false } }
               },
               {
                   field: 'CompanyCode', width: 100, title: 'Mã đối tác',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
              {
                  field: 'CompanyName', width: 150, title: 'Tên đối tác',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
              {
                  field: 'CompanyShortName', width: 150, title: 'Tên ngắn đối tác',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
           
            { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.Detail_Click = function ($event, item) {
        $event.preventDefault();

        $state.go("main.FLMSetting.Detail", { ID: item.ID });
    }

    $scope.New_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.FLMSetting.Detail", { ID: -1 });
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FLMSetting,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
}]);