/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATSYSCustomerTroubleDetail = {
    URL: {
        Read: 'CATSYSCustomerTrouble_Trouble_List',
        Save: 'CATSYSCustomerTrouble_Trouble_Save',
    },
    Param: {
        id: 0
    },
}

//#endregion

angular.module('myapp').controller('CATSYSCustomerTrouble_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATSYSCustomerTrouble_DetailCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.HasChoose = false
    _CATSYSCustomerTroubleDetail.Param = $.extend(true, _CATSYSCustomerTroubleDetail.Param, $state.params);
    
    $scope.CATSYSCustomerTroubleDetail_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATSYSCustomerTroubleDetail.URL.Read,
            readparam: function () { return { id: _CATSYSCustomerTroubleDetail.Param.id } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                    IsUse: { type: 'boolean' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CATSYSCustomerTroubleDetail_grid,CATSYSCustomerTroubleDetail_gridChooseChange)" />',
                
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CATSYSCustomerTroubleDetail_grid,CATSYSCustomerTroubleDetail_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                field: 'IsUse', title: 'Đã chọn', width: 120,
                template: '<input ng-disabled="true" type="checkbox" #= IsUse ? "checked=checked" : "" # ></input>',
                attributes: { style: "text-align: center;" }, filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATGroupOfTrouble.Code}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Name', title: '{{RS.CATGroupOfTrouble.Name}}', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfTroubleName', title: 'Loại vận chuyển', width: 250, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'CostValue', title: 'Chi phí', width: 200, filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input kendo-numeric-text-box k-options="CATSYSCustomerTroubleDetail_numOptions" ng-model="dataItem.CostValue" style="width:100%" ></input>'
            },
            { title: ' ', filterable: false, sortable: false },
        ]
    };
    $scope.CATSYSCustomerTroubleDetail_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
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
    $scope.CATSYSCustomerTroubleDetail_numOptions = { format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0 }

    $scope.CATSYSCustomerTroubleDetail_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.CATSYSCustomerTrouble.Index")
    }

    $scope.CATSYSCustomerTroubleDetail_Save_Click = function ($event, grid) {
        $event.preventDefault();
        $scope.IsLoading = true;
        var data = grid.dataSource.data();
        var dataSennd = $.grep(data, function (o) { return o.IsChoose == true })
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATSYSCustomerTroubleDetail.URL.Save,
            data: { lst: dataSennd, id: _CATSYSCustomerTroubleDetail.Param.id },
            success: function (res) {
                $scope.IsLoading = false;
                $rootScope.Message({ Msg: 'Đã lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.CATSYSCustomerTroubleDetail_gridOptions.dataSource.read()
            }
        });
    }

}]);