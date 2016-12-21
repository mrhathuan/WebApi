/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONMonitor_Approve_DICost = {
    URL: {
        Read: 'MONInput_DIFLMFee_List',
        Approved: 'MONInput_DIFLMFee_Approved',
        DriverList: 'MONInput_DIFLMFee_DriverList',
        StationCost: 'MONInput_DIFLMFee_StationCostList',
        TroubleCost: 'MONInput_DIFLMFee_TroubleCostList',
        TroubleCostNotIn_List: 'MONInput_DIFLMFee_TroubleCostNotIn_List',
        TroubleCostNotIn_Save: 'MONInput_DIFLMFee_TroubleCostNotIn_SaveList',
        TroubleCost_Delete: 'MONInput_DIFLMFee_TroubleCost_DeleteList',
        TroubleCostSave: 'MONInput_DIFLMFee_TroubleCostSave',
        StationCostSave: 'MONInput_DIFLMFee_StationCostSave',
        Update: 'MONInput_DIFLMFee_Save',
        GetDrivers: 'MONInput_DIFLMFee_GetDrivers',
        SaveDrivers: 'MONInput_DIFLMFee_SaveDrivers',

        ExcelExport: 'PODFLMInput_Export',
        ExcelCheck: 'PODFLMInput_Excel_Check',
        ExcelImport: 'PODFLMInput_Excel_Import',
    },
    Data: {
        DIPODStatus: []
    }
}

//#endregion

angular.module('myapp').controller('MONMonitor_Approve_DICost2Ctrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    if ($rootScope.IsPageComplete != true) return;
    Common.Log('MONMonitor_Approve_DICost2Ctrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.ItemSearch = {
        DateFrom: new Date().addDays(-10),
        DateTo: new Date().addDays(2)
    }

    //#region Common

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.MONCost,
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

    //#region Grid

    $scope.TroubleDriverCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'DriverName',
        dataValueField: 'ID',
        change: function (e) {
            var grid = $scope.Trouble_Grid;
            var item = grid.dataItem($(e.sender.wrapper).closest('tr'));
            if (Common.HasValue(item)) {
                item.DriverName = this.text();
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "Monitor_DriverList",
        data: {},
        success: function (res) {
            $scope.TroubleDriverCbbOptions.dataSource.data(res.Data);
        }
    })

    $scope.Trouble_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONDI_TroubleRead",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    COTOID: { type: 'number', editable: true },
                    TroubleCostStatusID: { type: 'number', editable: false },
                    RoutingName: { type: 'string' },
                    Cost: { type: 'number', defaultValue: 0 },
                    CostOfCustomer: { type: 'number', defaultValue: 0 },
                    CostOfVendor: { type: 'number', defaultValue: 0 },
                    TroubleCostStatusName: { type: 'string' },
                    IsChoose: {type:'boolean'}
                }
            },
            readparam: function () { return { DateFrom: $scope.ItemSearch.DateFrom, DateTo: $scope.ItemSearch.DateTo } }
        }),
        height: '99%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell',
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Trouble_Grid,Trouble_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Trouble_Grid,Trouble_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
           { field: 'TroubleCostStatusName', width: 130, title: '{{RS.SYSVar1.ValueOfVar}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
           { field: 'MasterCode', width: 130, title: '{{RS.OPSDITOMaster.Code}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
           { field: 'GroupOfTroubleName', width: 150, title: '{{RS.CATRouting.ParentName}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
           {
               field: 'Cost', width: 100, title: '{{RS.CATCost.CostValue}}', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-on-change='ChangeProblemCostGrid(dataItem)' k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }, filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'CostOfCustomer', width: 100, title: '{{RS.CATTrouble.CostOfCustomer}}', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }, filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'CostOfVendor', width: 100, title: '{{RS.CATTrouble.CostOfVendor}}', editor: function (container, options) {
                   var input = $("<input kendo-numerictextbox k-options='numPrice_Options'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }, filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DriverID', width: 100, title: 'Tài xế', template: '#=DriverName == null ? "" : DriverName #', editor: function (container, options) {
                   var input = $("<input kendo-combobox k-options='TroubleDriverCbbOptions'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           { field: 'Description', width: 150, title: '{{RS.SYSGroup.Description}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
        }
    };

    $scope.Trouble_AproveAll = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        for (var i = 0; i < grid.dataSource.data().length; i++) {
            var item = grid.dataSource.data()[i];
            if (item.IsChoose) {
                if (!Number.isFinite(item.CostOfCustomer))
                    item.CostOfCustomer = 0;
                if (!Number.isFinite(item.CostOfVendor))
                    item.CostOfVendor = 0;
                if (!Number.isFinite(item.Cost))
                    item.Cost = 0;
                data.push(item);
            }
        }
        if (data.length > 0)
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONDI_TroubleApproved",
                data: { lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.Trouble_RejectAll = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        for (var i = 0; i < grid.dataSource.data().length; i++) {
            var item = grid.dataSource.data()[i];
            if (item.IsChoose) {
                if (!Number.isFinite(item.CostOfCustomer))
                    item.CostOfCustomer = 0;
                if (!Number.isFinite(item.CostOfVendor))
                    item.CostOfVendor = 0;
                if (!Number.isFinite(item.Cost))
                    item.Cost = 0;
                data.push(item);
            }
        }
        if (data.length > 0)
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: "MONDI_TroubleReject",
                data: { lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
    }

    $scope.SearchClick = function (e) {
        e.preventDefault();
        $scope.Trouble_Grid.dataSource.read();
    }
    //#endregion

}]);