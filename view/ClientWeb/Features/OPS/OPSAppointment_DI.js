/// <reference path="OPSAppointment_COImportPacket_Detail_Input.html" />
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_DI = {
    URL: {
        Read: 'OPS_DIAppointment_Read',
        Cancel: 'OPS_DIAppointment_Cancel',
    },
    Data: {
        Combobox: [{ ValueString: 'OPSAppointment.DI', Text: 'Xe tải', ValueInt: 0 }],
    },    
};

angular.module('myapp').controller('OPSAppointment_DICtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DICtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;



    $scope.Auth = $rootScope.GetAuth();

    if ($scope.Auth.ViewVendor && !$scope.Auth.ViewAdmin) {
        $state.go("main.OPSAppointment.DIRouteMasterVEN");
    }

    $scope.gop_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DI.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    RequestDate: { type: 'date' },
                    CreatedDate: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#gop_GridToolbar').html()),
        columns: [
            {
                field: "Command",title: ' ', width: '40px', headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gop_Grid,gop_GridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }, filterable: false, sortable: false,
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gop_Grid,gop_GridChooseChange)" />',
                sortorder: 0, configurable: false, isfunctionalHidden: false
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
                field: 'IsHot', title: '{{RS.ORDOrder.IsHot}}', width: 100, sortorder: 3, configurable: false, isfunctionalHidden: false,
                template: '<i class="fa fa-bolt" style="color: #= IsHot != true ? "rgba(160, 165, 189, 1)" : "rgba(249, 114, 117, 1)"#"></i>',
                attributes: { style: "text-align: center; " }, headerAttributes: { style: 'text-align: center;' },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }], dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            {
                field: 'Code', title: '{{RS.ORDOrder.Code}}', width: 160, sortorder: 4, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: '{{RS.CUSCustomer.Code}}', width: 160, sortorder: 5, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', title: '{{RS.ORDOrder.RequestDate}}', width: 150, sortorder: 6, configurable: true, isfunctionalHidden: false,
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHM(RequestDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Ton', title: '{{RS.OPSDITOGroupProduct.Ton}}', width: 150, format: "{0:n5}", sortorder: 7, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'CBM', title: '{{RS.OPSDITOGroupProduct.CBM}}', width: 150, format: "{0:n5}", sortorder: 8, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'TypeOfOrderName', title: '{{RS.ORDOrder.TypeOfOrderName}}', width: 150, sortorder: 9, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: '{{RS.CUSCustomer.CustomerName}}', width: 150, sortorder: 10, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreatedDate', title: '{{RS.ORDOrder.CreatedDate}}', width: 150, sortorder: 11, configurable: true, isfunctionalHidden: false,
                template: "#=CreatedDate==null?' ':Common.Date.FromJsonDMYHM(CreatedDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'CreatedBy', title: '{{RS.ORDOrder.CreatedBy}}', width: 150, sortorder: 12, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.gop_GridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                data.push(v.ID);
        });
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Trả về các đơn hàng được chọn?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DI.URL.Cancel,
                        data: { data: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.gop_GridOptions.dataSource.read();
                            $rootScope.Message({ Msg: 'Đã trả về', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.HasChoose = false;
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
            event: $event, grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }
    //#endregion
}]);
