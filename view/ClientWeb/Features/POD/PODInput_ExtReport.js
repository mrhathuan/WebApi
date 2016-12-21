
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _ExtReport = {
    URL: {
        Search: 'PODOPSExtReturn_Data',
        Template: 'PODOPSExtReturnTemplate',
    },
}

angular.module('myapp').controller('PODInput_ExtReportCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('PODInput_ExtReportCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = {
        lstCustomerID: [],
        lstGroupID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.EXTReport_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    InvoiceDate: { type: 'date' },
                    Quantity: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: {
            mode: "multiple",
            allowUnsort: true
        }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                field: 'CustomerCode', title: '<b>Mã khách hàng</b><br>[CustomerCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: '<b>Khách hàng</b><br>[CustomerName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', title: '<b>Số xe</b><br>[VehicleNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceNo', title: '<b>Số hóa</b><br>[InvoiceNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', title: '<b>Ngày</b><br>[InvoiceDate]', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(InvoiceDate)#',
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
          
            {
                field: 'GroupProductCode', title: '<b>Nhóm sản phẩm</b><br>[GroupProductCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductCode', title: '<b>Mã hàng</b><br>[ProductCode]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Quantity', title: '<b>Số lượng</b><br>[Quantity]', width: '100px', template: '#=Quantity==null?" ":Common.Number.ToNumber3(Quantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExtReturnStatusName', title: '<b>Tình trạng</b><br>[ExtReturnStatusName]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: '<b>Ghi chú</b><br>[Note]', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _ExtReport.URL.Search,
            data: { dtFrom: $scope.Item.DateFrom, dtTo: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.EXTReport_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();

        var functionID = $rootScope.FunctionID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _ExtReport.URL.Template,
                    data: { itemfile: file, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    };

   
    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput_ExtReport,
            event: $event,
            current: $state.current
        });
    };
}]);