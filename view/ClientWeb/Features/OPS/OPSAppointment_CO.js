/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_CO = {
    URL: {
        Read: 'OPS_COAppointment_Read',
        Cancel: 'COAppointment_Cancel',
    },
};

angular.module('myapp').controller('OPSAppointment_COCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COCtrl');
    $scope.HasChoose = false;

    $scope.OPSAppointment_CO_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_CO.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    RequestDate: { type: 'date' },
                    CreatedDate: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#OPSAppointment_CO_gridToolbar').html()),
        columns: [
            {
                field: 'Command', title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSAppointment_CO_grid,OPSAppointment_CO_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSAppointment_CO_grid,OPSAppointment_CO_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'StatusOfOrderName', title: '{{RS.ORDOrder.StatusOfOrderName}}', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'StatusOfPlanName', title: '{{RS.ORDOrder.StatusOfPlanName}}', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'IsHot', title: '{{RS.ORDOrder.IsHot}}', width: 100,
                template: '<i class="fa fa-bolt" style="color: #= IsHot != true ? "rgba(160, 165, 189, 1)" : "rgba(249, 114, 117, 1)"#"></i>',
                attributes: { style: "text-align: center; " }, headerAttributes: { style: 'text-align: center;' },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                },
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Code', title: '{{RS.ORDOrder.Code}}', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ServiceOfOrderName', title: '{{RS.ORDOrder.ServiceOfOrderName}}', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerCode', title: '{{RS.CUSCustomer.Code}}', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDate', title: '{{RS.ORDOrder.RequestDate}}', width: 150,
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHMS(RequestDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfOrderName', title: '{{RS.ORDOrder.TypeOfOrderName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerName', title: '{{RS.CUSCustomer.CustomerName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CreatedDate', title: '{{RS.ORDOrder.CreatedDate}}', width: 150,
                template: "#=CreatedDate==null?' ':Common.Date.FromJsonDMYHMS(CreatedDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CreatedBy', title: '{{RS.ORDOrder.CreatedBy}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.OPSAppointment_CO_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn có chắc muốn trả về các đơn hàng được chọn?',
                pars: { lstid: lstid },
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_CO.URL.Cancel,
                        data: { lstid: lstid },
                        success: function (res) {
                            $scope.OPSAppointment_CO_gridOptions.dataSource.read();
                            $rootScope.Message({ Msg: 'Đã trả về', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.HasChoose = false;
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            });
        }
    };

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);