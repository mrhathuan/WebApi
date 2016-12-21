/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATKPITime_Index = {
    URL: {
        List: 'KPITime_List',
        Generate: 'KPITime_Generate'
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion
angular.module('myapp').controller('CATKPITime_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATKPITime_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = {
        DateFrom: new Date(),
        DateTo: new Date()
    };
    $scope.HasSearch = false;

    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $scope.catkpi_gridOptions.dataSource.read();
        $scope.HasSearch = true;
    };

    $scope.Refresh_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Title: 'Thông báo',
            Msg: 'Bạn muốn tính lại KPI ?',
            Action: true,
            Close: null,
            Ok: function (pars) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATKPITime_Index.URL.Generate,
                    data: pars,
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            $scope.catkpi_gridOptions.dataSource.read();
                        }
                    }
                });
            },
            pars: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo }
        });        
    };
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
    $scope.catkpi_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATKPITime_Index.URL.List,
            readparam: function () { return { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    OrderCode: { type: 'string' },
                    CustomerCode: { type: 'string' },
                    CustomerName: { type: 'string' },
                    IsKPIOPS: { type: 'boolean' },
                    IsKPIPOD: { type: 'boolean' },
                    KPIOPS: { type: 'date' },
                    KPIPOD: { type: 'date' },
                    DateDN: { type: 'date' },
                    DateFromLoadStart: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    DateData: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#catkpi_gridToolbar').html()),
        columns: [
            { field: 'OrderCode', title: '{{RS.ORDOrder.Code}}', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: '{{RS.CUSCustomer.Code}}', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: '{{RS.CUSCustomer.CustomerName}}', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'DateData', title: '{{RS.KPIKPITime.DateData}}', width: '120px', template: '#=Common.Date.FromJsonDMY(DateData)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateDN', title: '{{RS.KPIKPITime.DateDN}}', width: '120px', template: '#=Common.Date.FromJsonDMY(DateDN)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLoadStart', title: '{{RS.KPIKPITime.DateFromLoadStart}}', width: '120px', template: '#=Common.Date.FromJsonDMY(DateFromLoadStart)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLeave', title: '{{RS.KPIKPITime.DateToLeave}}', width: '120px', template: '#=Common.Date.FromJsonDMY(DateToLeave)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'KPIOPS', title: '{{RS.KPIKPITime.KPIOPS}}', width: '120px', template: '#=Common.Date.FromJsonDMY(KPIOPS)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'KPIPOD', title: '{{RS.KPIKPITime.KPIPOD}}', width: '120px', template: '#=Common.Date.FromJsonDMY(KPIPOD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'IsKPIOPS', title: '{{RS.KPIKPITime.IsKPIOPS}}', width: '100px', template: '<input type="checkbox" #= IsKPIOPS ? "checked=checked" : ""# />', templateAttributes: { style: 'text-align: center;' }, sortable: false, filterable: false, menu: false },
            { field: 'IsKPIPOD', title: '{{RS.KPIKPITime.IsKPIPOD}}', width: '100px', template: '<input type="checkbox" #= IsKPIPOD ? "checked=checked" : ""# />', templateAttributes: { style: 'text-align: center;' }, sortable: false, filterable: false, menu: false },
            { field: 'Note', title: '{{RS.KPIKPITime.Note}}', width: '200px', sortable: false, filterable: false, menu: false },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };
}]);