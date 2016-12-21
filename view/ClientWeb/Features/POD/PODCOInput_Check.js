/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODCOInput_Check = {
    URL: {
        Read: 'PODCOInput_List',
        Update: 'PODCOInput_Save',
        Customer_List: "Customer_List",
        Vendor_List: "Vendor_List",
        Reset: 'PODDIInput_Check_Reset',
        UpdateHasUpload: 'PODDIInput_UpdateHasUpload',
    },
    Data: {
        DIPODStatus: [],
        ListProduct: [],
    }
}

//#endregion

angular.module('myapp').controller('PODCOInput_CheckCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODCOInput_CheckCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.FullName = Common.Auth.Item.LastName + " " + Common.Auth.Item.FirstName;

    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID: [],
        IsReturn: true
    }

    // Phần Customer_List
    $scope.mts_CustomerOptions = {
        dataSource: Common.DataSource.Local({
            url: Common.Services.url.POD,
            method: _PODCOInput_Check.URL.Customer_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        valuePrimitive: true, dataTextField: "CustomerName", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.POD,
        method: _PODCOInput_Check.URL.Customer_List,
        data: {},
        success: function (res) {
            $scope.mts_CustomerOptions.dataSource.data(res.Data)
        }
    });

    // Phần vendor_List
    $scope.mts_VendorOptions = {
        dataSource: Common.DataSource.Local({
            url: Common.Services.url.POD,
            method: _PODCOInput_Check.URL.Vendor_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "CustomerName", dataValueField: "ID", placeholder: "Chọn vendor...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.POD,
        method: _PODCOInput_Check.URL.Vendor_List,
        data: {},
        success: function (res) {
            $scope.mts_VendorOptions.dataSource.data(res.Data)
        }
    });

    $scope.PODCheck_SearchClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.ItemSearch.DateFrom) || !Common.HasValue($scope.ItemSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ItemSearch.DateFrom > $scope.ItemSearch.DateTo) {
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
            $scope.PODCOCheck_gridOptions.dataSource.read();
        }
    }

    $scope.PODCOCheck_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODCOInput_Check.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.ItemSearch.DateFrom,
                    'dtTo': $scope.ItemSearch.DateTo,
                    'listCustomerID': $scope.ItemSearch.ListCustomerID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsOrigin: { type: 'boolean', },
                    IsInvoice: { type: 'boolean', },
                    IsComplete: { type: 'boolean', },
                    IsClosed: { type: 'boolean', },
                    DITOGroupProductStatusPODID: { type: 'number', },
                    DateFromCome: { type: 'date' },
                    DateFromLeave: { type: 'date' },
                    ReceivedDate: { type: 'date' },
                    DateFromLoadEnd: { type: 'date' },
                    DateFromLoadStart: { type: 'date' },
                    DateToCome: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    RequestDate: { type: 'date' },
                }
            },
            sort: [{ field: 'MasterID', dir: "asc" }]
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: Common.PageSize, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: 80, field: 'F_Command1', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="PODInput_SaveClick($event,PODCOCheck_grid,dataItem)" ng-show="!dataItem.IsClosed" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>' +
                    '<a href="/" ng-click="PODInput_UpLoadClick($event,winfile,dataItem)" ng-show="!dataItem.IsClosed"  class="k-button" data-title="Chứng từ"><i ng-class="{hasupload:dataItem.HasUpload}"  class="fa fa-file"></i></a>'+
                    '<a href="/" ng-click="PODInput_ResetClick($event,PODCOCheck_grid,dataItem)" ng-show="dataItem.IsClosed&&Auth.ActApproved" class="k-button" data-title="Lưu"><i class="fa fa-backward"></i></a>',
                filterable: false, sortable: false,
            },
            {
                field: 'IsInvoice', width: 50, title: 'Đã nhận', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsInvoice' ng-disabled='dataItem.IsClosed' ng-click='ChangeStatusInvoice($event,dataItem)' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đã nhận', Value: true }, { Text: 'Chưa nhận', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }, sortorder: 1, configurable: true, isfunctionalHidden: false,
            },
            {
                field: 'InvoiceBy', width: 100, sortorder: 2, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceBy}}', template: '<input type="text" class="k-textbox "  ng-model="dataItem.InvoiceBy"  ng-keydown="QuickSave($event,PODCOCheck_grid,dataItem)" style="width: 100%" ng-disabled="dataItem.IsClosed"  ></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', width: 100, sortorder: 3, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceDate}}',
                template: '<input class="cus-datepicker" kendo-date-picker focus-k-datepicker k-options="DateDMY" k-ng-model="dataItem.InvoiceDate" ng-disabled="dataItem.IsClosed" />',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'InvoiceNo', width: 100, sortorder: 4, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceNote}}', template: '<input type="text"  class="k-textbox "  ng-model="dataItem.InvoiceNo"  ng-keydown="QuickSave($event,PODCOCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  ></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'OrderCode', width: 100, sortorder: 6, configurable: true, isfunctionalHidden: false, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Note1', width: 100, sortorder: 10, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Note1"  ng-keydown="QuickSave($event,PODCOCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  />'
            },
            {
                field: 'Note2', width: 100, sortorder: 11, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Note2"  ng-keydown="QuickSave($event,PODCOCheck_grid,dataItem)" style="width: 100%"ng-disabled="dataItem.IsClosed"  />'
            },
            { field: 'MasterCode', width: 100, sortorder: 15, configurable: true, isfunctionalHidden: false, title: 'Lệnh v.c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleNo', width: 100, sortorder: 16, configurable: true, isfunctionalHidden: false, title: 'Số đầu kéo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RomoocNo', width: 100, sortorder: 17, configurable: true, isfunctionalHidden: false, title: 'Số romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainer', width: 100, sortorder: 18, configurable: true, isfunctionalHidden: false, title: 'Loại Cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrder', width: 100, sortorder: 19, configurable: true, isfunctionalHidden: false, title: 'Dịch vụ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDate', width: 100, sortorder: 20, configurable: true, isfunctionalHidden: false, title: 'Ngày gửi', template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHMS(RequestDate)#", filterable: false },
            { field: 'CustomerCode', width: 100, sortorder: 21, configurable: true, isfunctionalHidden: false, title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: 100, sortorder: 22, configurable: true, isfunctionalHidden: false, title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DateFromCome', width: 100, sortorder: 26, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateFromCome}}', template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMY(DateFromCome)#", filterable: false },
            { field: 'DateFromLeave', width: 100, sortorder: 27, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateFromLeave}}', template: "#=DateFromLeave==null?' ':Common.Date.FromJsonDMY(DateFromLeave)#", filterable: false },
            { field: 'DateToCome', width: 100, sortorder: 28, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateToCome}}', template: "#=DateToCome==null?' ':Common.Date.FromJsonDMY(DateToCome)#", filterable: false },
            { field: 'DateToLeave', width: 100, sortorder: 29, configurable: true, isfunctionalHidden: false, title: '{{RS.OPSDITOGroupProduct.DateToLeave}}', template: "#=DateToLeave==null?' ':Common.Date.FromJsonDMY(DateToLeave)#", filterable: false },
            { field: 'KM', width: 100, sortorder: 40, configurable: true, isfunctionalHidden: false, title: 'KM', filterable: false },
            { field: 'ContainerNo', width: 100, sortorder: 41, configurable: true, isfunctionalHidden: false, title: 'Số cont', filterable: false },
            { field: 'SealNo1', width: 100, sortorder: 42, configurable: true, isfunctionalHidden: false, title: 'Số seal 1', filterable: false },
            { field: 'SealNo2', width: 100, sortorder: 43, configurable: true, isfunctionalHidden: false, title: 'Số seal 2', filterable: false },
            { title: ' ', sortable: false, filterable: false, menu: false, sortorder: 999, configurable: false, isfunctionalHidden: false, }
        ],
        dataBound: function (e) {
            
        }
    }

    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.PODCOCheck_grid);
    }, 100);

    $scope.ChangeStatusInvoice = function ($event, data) {
        var tr = $($event.target).closest('tr');
        var lsttd = tr.find('input[type=text]');
        if (data.IsInvoice) {
            if (!Common.HasValue(data.InvoiceBy) || data.InvoiceBy == '')
                data.InvoiceBy = $scope.FullName;
            if (!Common.HasValue(data.InvoiceDate) || data.InvoiceDate == '')
                data.InvoiceDate = new Date();
        }
        else {
            data.InvoiceBy = null;
            data.InvoiceDate = null;
        }
        if (lsttd.length > 0) {
            lsttd[0].focus();
        }
    };

    $scope.PODInput_SaveClick = function ($event, grid,data) {
        $event.preventDefault();
        if (data.IsInvoice) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Xác nhận chứng từ này đã nhận',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.POD,
                        method: _PODCOInput_Check.URL.Update,
                        data: { 'item': data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            })

        }
        else {
            $rootScope.Message({ Msg: 'Chưa tick nhận chứng từ', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.QuickSave = function ($event,grid,data) {
        if ($event.which === 13) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Xác nhận lưu thông tin chứng từ ?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.POD,
                        method: _PODCOInput_Check.URL.Update,
                        data: { 'item': data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            grid.dataSource.read();
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            })
        }
    }

    $scope.PODInput_ResetClick = function ($event, grid,data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn mở dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODCOInput_Check.URL.Reset,
                    data: { DITOGroupID: data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.PODCOCheck_grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã mở dữ liệu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.PODInput_UpLoadClick = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.UploadFile({
            IsImage: false,
            ID: data.ID,
            AllowChange: true,
            ShowChoose: false,
            Type: Common.CATTypeOfFileCode.COPOD,
            Window: win,
            UploadComplete: function (res) {
            },
            DeleteComplete: function (lst) {
            }
        });
    }

    //actions
    $scope.Back_Click = function ($event ) {
        $event.preventDefault();
        $state.go("main.PODInput.Index")
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

}]);