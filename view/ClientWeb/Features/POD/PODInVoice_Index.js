/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _PODInVoice = {
    URL: {
        Read: 'PODInvoice_Data',
        Save_List: 'PODInvoice_SaveList',

        Read_Customer: 'CATDistributor_Customer_Read',
        VenList: 'REPTotalPriceVendor_ListVendor',
    },
}

angular.module('myapp').controller('PODInVoice_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('PODInVoice_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = {
        CustomerID:-1,
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        ListVendor: [],
    }

    $scope.PODInVoice_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    InvoiceDate: { type: 'date', editable: true, },
                    CustomerCode: { type: 'string', editable: false },
                    SOCode: { type: 'string', editable: false },
                    DNCode: { type: 'string', editable: false },
                    RegNo: { type: 'string', editable: false },
                    OrderCode: { type: 'string', editable: false },
                    RequestDate: { type: 'date', editable: false },
                    LocationFromCode: { type: 'string', editable: false },
                    LocationToAddress: { type: 'string', editable: false },
                    Ton: { type: 'number', editable: false },
                    CBM: { type: 'number', editable: false },
                    Quantity: { type: 'number', editable: false },
                    TonTranfer: { type: 'number', editable: false },
                    CBMTranfer: { type: 'number', editable: false },
                    QuantityTranfer: { type: 'number', editable: false },
                    TonBBGN: { type: 'number', editable: false },
                    CBMBBGN: { type: 'number', editable: false },
                    OPSGroupNote1: { type: 'string', editable: false },
                    OPSGroupNote2: { type: 'string', editable: false },
                    ORDGroupNote1: { type: 'string', editable: false },
                    ORDGroupNote2: { type: 'string', editable: false },
                    TOMasterNote1: { type: 'string', editable: false },
                    TOMasterNote2: { type: 'string', editable: false },
                    InvoiceNote: { type: 'string', editable: false },
                    QuantityBBGN: { type: 'number', editable: false },
                    ShipmentNo: { type: 'string', editable: true, },
                    InvoiceNo: { type: 'string', editable: true, },
                    BillingNo: { type: 'string', editable: true, },


                }
            }
        }),
        height: '100%', groupable: false, pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        selectable: false, reorderable: false, editable: true,
        columns: [
           
            {
                field: 'CustomerCode', width: '120px', title: 'Mã KH',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'SOCode', width: '100px', title: 'Số SO',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'RegNo', width: '100px', title: 'Xe',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã đơn hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            
            {
                field: 'RequestDate', width: '130px', title: 'Ngày gửi y/c',
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false,
                    }
                },
            },
            {
                field: 'LocationFromCode', width: '100px', title: 'Mã kho',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'LocationToAddress', width: '250px', title: 'Đ/c giao',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'Ton', width: '100px', title: 'Tấn',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBM', width: '100px', title: 'Khối',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'Quantity', width: '100px', title: 'Số lượng',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'TonTranfer', width: '100px', title: 'Tấn lấy',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBMTranfer', width: '100px', title: 'Khối lấy',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'QuantityTranfer', width: '100px', title: 'Số lượng lấy',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'TonBBGN', width: '100px', title: 'Tấn giao',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBMBBGN', width: '100px', title: 'Khối giao',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'QuantityBBGN', width: '100px', title: 'Số lượng giao',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'OPSGroupNote1', width: '100px', title: 'Ghi chú 1',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'OPSGroupNote2', width: '100px', title: 'Ghi chú 2',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'ORDGroupNote1', width: '100px', title: 'Ghi chú Đ/h 1',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'ORDGroupNote2', width: '100px', title: 'Ghi chú Đ/h 2',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'TOMasterNote1', width: '100px', title: 'Ghi chú chuyến 1',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'TOMasterNote2', width: '100px', title: 'Ghi chú chuyến 2',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'InvoiceNote', width: '100px', title: 'Ghi chú chứng từ',
                filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'ShipmentNo', width: '100px', title: 'Số Shipment',
                template: '<form class="cus-form-enter" ng-submit="Enter_Click($event, dataItem)"><input ng-model="dataItem.ShipmentNo" class="k-textbox" style="width:100%" /></form>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'InvoiceNo', width: '100px', title: 'Số hóa đơn',
                template: '<form class="cus-form-enter" ng-submit="Enter_Click($event, dataItem)"><input ng-model="dataItem.InvoiceNo" class="k-textbox" style="width:100%" /></form>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'BillingNo', width: '100px', title: 'Số Bill',
                template: '<form class="cus-form-enter" ng-submit="Enter_Click($event, dataItem)"><input ng-model="dataItem.BillingNo" class="k-textbox" style="width:100%" /></form>',
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'InvoiceDate', width: '130px', title: 'Ngày hóa đơn', 
                template: '<form class="cus-form-enter" ng-submit="Enter_Click($event, dataItem)"><input class="cus-datepicker" focus-k-datepicker kendo-date-picker k-options="DateDMY" ng-model="dataItem.InvoiceDate" style="width:100%" /></form>',
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false,
                    }
                },
                
            },
            { title: '', filterable: false, sortable: false }
        ],
    }

    $scope.Enter_Click = function ($event, item) {
        var lst = [];
        //lst = $.grep($scope.PODInVoice_gridOptions.dataSource.data(), function (item) { return item.dirty == true });
        if (Common.HasValue(item)) {
            lst.push(item);
            if (item.BillingNo == "" && item.InvoiceNo == "" && item.ShipmentNo == "") {
                $rootScope.Message({
                    Msg: 'Dữ liệu sai.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
            }
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODInVoice.URL.Save_List,
                    data: { 'lst': lst },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        //$scope.PODInVoice_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }
    $scope.cboCustomerOptions =
    {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Chọn khách hàng...",
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {

                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            if ($scope.Item.CustomerID == -1) {
                $scope.Item.ListVendor = [];
            } else if (Common.HasValue($scope.Item.CustomerID) && $scope.Item.CustomerID > 0) {
                $timeout(function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _PODInVoice.URL.VenList,
                        data: { cusId: $scope.Item.CustomerID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.Item.ListVendor = res;
                        }
                    });
                }, 1)


            }
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _PODInVoice.URL.Read_Customer,
        data: {},
        success: function (res) {
            var item = { ID: -1, CustomerName: '' };
            res.Data.unshift(item);
            $scope.cboCustomerOptions.dataSource.data(res.Data);
        }
    });

    $scope.POD_Search_Click = function ($event) {
        $event.preventDefault();

        if (!Common.HasValue($scope.Item.DateFrom) || !Common.HasValue($scope.Item.DateTo) || !Common.HasValue($scope.Item.CustomerID)) {

            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày, Nhập khách chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.Item.DateFrom > $scope.Item.DateTo || !Common.HasValue($scope.Item.CustomerID)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }

        else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODInVoice.URL.Read,
                data: {
                    cusId: $scope.Item.CustomerID,
                    dtFrom: $scope.Item.DateFrom, dtTo: $scope.Item.DateTo
                },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    Common.Services.Error(res, function (res) {
                        $scope.PODInVoice_gridOptions.dataSource.data(res);
                    })
                }
            });
        }
    }
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: '',
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);