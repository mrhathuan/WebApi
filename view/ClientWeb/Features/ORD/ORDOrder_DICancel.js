/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_DICancel = {
    URL: {
        Read: 'ORD_DI_Cancel_List',
        Change: 'ORD_DI_Cancel_Change',
        Customer_List: 'ORDOrder_CustomerList',
        Reason_List: 'ORD_DI_Cancel_Reason_List'
    }   
};

angular.module('myapp').controller('ORDOrder_DICancelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DICancelCtrl');
    $rootScope.IsLoading = false;
        
    $scope.ID = -1;
    $scope.Reason = -1;
    $scope.ReasonNote = '';
    
    $scope.ListCustomer = [];
    $scope.DateTo = new Date().addDays(3);
    $scope.DateFrom = new Date().addDays(-3);

    $scope.gop_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _OPSAppointment_DICancel.URL.Read,
            readparam: function(){
                return {
                    fDate: $scope.DateFrom,
                    tDate: $scope.DateTo,
                    dataCus: $scope.ListCustomer
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, groupable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                field: 'Command', title: ' ', width: 45,sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a class="k-button" href"/" ng-click="Reason_Change($event,dataItem,reason_win)" titlt="Đổi lý do"><i class="fa fa-pencil"></i></a>',
                sortable: false, groupable: false, filterable: false
            },
            {
                field: 'OrderCode', title: '{{RS.ORDOrder.Code}}', width: 150,sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderGroupProductCode', title: '{{RS.CUSGroupOfProduct.GroupName}}', width: 150, sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DITOMasterCode', title: '{{RS.OPSDITOMaster.Code}}', width: 120, sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: '{{RS.CUSCustomer.ShortName}}', width: 120, sortorder: 4, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', title: '{{RS.OPSDITOGroupProduct.Ton}}', width: 100, format: "{0:n5}", sortorder: 5, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'CBM', title: '{{RS.OPSDITOGroupProduct.CBM}}', width: 100, format: "{0:n5}", sortorder: 6, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'Quantity', title: '{{RS.OPSDITOGroupProduct.Quantity}}', width: 100, sortorder: 7, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', title: '{{RS.OPSDITOGroupProduct.DNCode}}', width: 150, sortorder: 8, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', title: '{{RS.ORDGroupProduct.SOCode}}', width: 150, sortorder: 9, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 150, title: '{{RS.ORDGroupProduct.ETD}}', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 10, configurable: true, isfunctionalHidden: false,
            },
            {
                field: 'ETA', width: 150, title: '{{RS.ORDGroupProduct.ETA}}', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromName', title: '{{RS.ORDOrder_DICancel.LocationFromName}}', width: 150, sortorder: 12, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', title: '{{RS.ORDOrder_DICancel.LocationToName}}', width: 150, sortorder: 13, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CancelReasonName', title: '{{RS.ORDOrder_DICancel.CancelReasonName}}', width: 150, sortorder: 14, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CancelReasonNote', title: '{{RS.ORDOrder_DICancel.CancelReasonNote}}', width: 250, sortorder: 15, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.Reason_Change = function ($event, item, win) {
        $event.preventDefault();
        
        $scope.ID = item.ID;
        $scope.Reason = item.CancelReasonID;
        $scope.ReasonNote = item.CancelReasonNote;

        win.center().open();
    }

    $scope.cboReasonOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ReasonName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ReasonName: { type: 'string' }
                }
            }
        })
    };

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _OPSAppointment_DICancel.URL.Reason_List,
        success: function (res) {
            $timeout(function () {
                $scope.cboReasonOptions.dataSource.data(res);
            }, 1)
        }
    })

    $scope.mltCustomerOptions = {
        autoBind: false,
        valuePrimitive: true, placeholder: 'Chọn khách hàng',
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
        filter: 'contains',
        suggest: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            $scope.ListCustomer = this.value();
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _OPSAppointment_DICancel.URL.Customer_List,
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.mltCustomerOptions.dataSource.data(res.Data);
                }, 1);
            }
        }
    })

    $scope.$watch('DateTo', function () {
        $scope.gop_Grid.dataSource.read();
    })
    $scope.$watch('DateFrom', function () {
        $scope.gop_Grid.dataSource.read();
    })
    $scope.$watch('ListCustomer', function () {
        $scope.gop_Grid.dataSource.read();
    })

    $scope.Update_Click = function ($event, grid, win) {
        $event.preventDefault();

        if ($scope.Reason == null || $scope.Reason == undefined || !($scope.Reason > 0)) {
            $rootScope.Message({
                Msg: "Chọn lý do",
                Type: Common.Message.Type.Alert
            });
            return;
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _OPSAppointment_DICancel.URL.Change,
                data: {
                    gopID: $scope.ID,
                    reasonID: $scope.Reason,
                    reasonNote: $scope.ReasonNote
                },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({
                            Msg: "Đã cập nhật!"
                        });

                        $scope.ID = -1;
                        $scope.Reason = -1;
                        $scope.ReasonNote = '';

                        win.close();
                        grid.dataSource.read();
                    })
                }
            })
        }        
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        try {
            win.close();
        } catch (e) {
        }
    }
    
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
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
