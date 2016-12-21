/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONInput_ExtReturnQuick = {
    URL: {
        Read: 'MONOPSExtReturn_List',
        Get: 'MONOPSExtReturn_Get',
        Save: 'MONOPSExtReturn_Save',
        Delete: 'MONOPSExtReturn_Delete',
        Approved: 'MONOPSExtReturn_Approved',

        CustomerList: 'MONOPSExtReturn_CustomerList',
        VehicleList: 'MONOPSExtReturn_VehicleList',
        DriverList: 'MONOPSExtReturn_DriverList',
        VendorList: 'MONOPSExtReturn_VendorList',
        GOPList: 'MONOPSExtReturn_GOPByCus',
        ProductList: 'MONOPSExtReturn_ProductByGOP',
        DITOMasterList: 'MONOPSExtReturn_DITOMasterList',

        DetailList: 'MONOPSExtReturn_DetailList',
        DetailNotIn: 'MONOPSExtReturn_DetailNotIn',
        SaveDetail: 'MONOPSExtReturn_DetailSave',

        FindList: 'MONOPSExtReturn_FindList',
        ExtReturnStatus: 'ALL_ExtReturnStatus',

        QuickList: 'MONOPSExtReturn_QuickList',
        QuickSave: 'MONOPSExtReturn_QuickSave',
        QuickData: 'MONOPSExtReturn_QuickData'
    },
    Data: {
        ExtReturnDetailList: [],
        ListExtReturnStatus: null,
        LastInvoiceNo: { DITOProductID: -1, InvoiceNo: ' ' },
        ListVendor: null,
        ListVehicle: null,
    }
}

//#endregion

angular.module('myapp').controller('MONMonitor_Input_ExtReturnQuickCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('MONMonitor_Input_ExtReturnQuickCtrl');
    $rootScope.IsLoading = false;

    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID: [],
    }

    var cookie = Common.Cookie.Get("ExtReturn_Search");
    if (!Common.HasValue(cookie) || cookie == '') {
        var val = JSON.stringify($scope.ItemSearch)
        Common.Cookie.Set("ExtReturn_Search", val)
    }
    else {
        var val = JSON.parse(cookie);
        $scope.ItemSearch = val;
    }

    $scope.mts_CustomerOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã khách hàng </span><span style="float:right;"> Tên khách hàng </span></strong>',
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Customer,
        success: function (data) {
            $scope.mts_CustomerOptions.dataSource.data(data)
        }
    })

    $scope.PODInput_SearchClick = function ($event) {
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
            var val = JSON.stringify($scope.ItemSearch)
            Common.Cookie.Set("ExtReturn_Search", val)
            $scope.extReturn_Quick_GridOptions.dataSource.read();
        }
    }

    
    //#region quick
    $scope.extReturn_Quick_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturnQuick.URL.QuickList,
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
                    ID: { type: 'number', editable: false, },
                    Quantity: { type: 'number' },
                    KMax: { type: 'number' },
                    InvoiceDate: { type: 'date' },
                    InvoiceNo: { type: 'string' },
                    RequestDate: { type: 'date' },
                }
            }
        }),
        reorderable: false, editable: 'inline',
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, pagesize: 20,
        columns: [
            {
                title: ' ', width: 110, field: "F_command1",
                template: '<a href="/" ng-click="Quick_SaveClick($event,dataItem)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false, locked: false, sortorder: 0, configurable: true, isfunctionalHidden: true,
            },
             {
                 field: 'InvoiceNo', title: 'Số chứng từ', width: 150,
                 template: '<input type="text" class="k-textbox"  ng-model="dataItem.InvoiceNo" ng-keydown="QuickSaveByEnter($event,dataItem,extReturn_Quick_Grid)" />',
                 filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 1, configurable: true, isfunctionalHidden: true,
             },
            {
                field: 'InvoiceDateString', title: 'Ngày chứng từ', width: 120,
                template: '<input type="text" class="k-textbox txtInvoiceReturnDate" ng-model="dataItem.InvoiceDateString" ng-keydown="ChangeInvoiceDate($event,dataItem)"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 2, configurable: true, isfunctionalHidden: true,
            },
            { field: 'DITOMasterCode', title: 'Mã chuyến', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: true, isfunctionalHidden: true, },
            { field: 'CustomerCode', title: 'Mã khách hàng', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 4, configurable: true, isfunctionalHidden: true, },
            { field: 'VendorCode', title: 'Mã nhà xe', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: true, },
            { field: 'VehicleNo', title: 'Xe', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: true, },
            { field: 'LocationTo', title: 'Nơi đến', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 7, configurable: true, isfunctionalHidden: true, },
            { field: 'GroupProductCode', title: 'Mã nhóm hàng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 8, configurable: true, isfunctionalHidden: true, },
            { field: 'ProductCode', title: 'Mã hàng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 9, configurable: true, isfunctionalHidden: true, },
            { field: 'OrderCode', title: 'Mã ĐH', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 10, configurable: true, isfunctionalHidden: true, },
            {
                field: 'RequestDate', title: 'Mã sản phẩm', width: 120, sortorder: 11, configurable: true, isfunctionalHidden: true,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(RequestDate)#'
            },
            {
                field: 'Quantity', title: 'Số lượng', width: 100, sortorder: 12, configurable: true, isfunctionalHidden: true,
                template: '<input kendo-numeric-text-box k-options="numQuantyOptions" k-min="1" k-max="dataItem.KMax" k-ng-model="dataItem.Quantity" style="width: 100%;" ng-keydown="QuickSaveByEnter($event,dataItem,extReturn_Quick_Grid)" />',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'ExtReturnStatusID', title: 'Loại trả về', width: 100, sortorder: 13, configurable: true, isfunctionalHidden: true,
                template: '<input kendo-combo-box focus-k-combobox class="cus-combobox" k-options="cboExtReturnStatusOptions"  ng-model="dataItem.ExtReturnStatusID" value="dataItem.ExtReturnStatusID" />',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
           
            {
                field: 'ETD', title: 'ETD', width: 120, template: '#=Common.Date.FromJsonDDMMYY(ETD)#', sortorder: 14, configurable: true, isfunctionalHidden: true,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', title: 'ETA', width: 120, template: '#=Common.Date.FromJsonDDMMYY(ETA)#', sortorder: 16, configurable: true, isfunctionalHidden: true,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
        ],
        dataBound: function (e) {
            var grid = this;
            var data = grid.dataSource.view();
            Common.Data.Each(data, function (o) {
                o.InvoiceDateString = Common.Date.ToString(o.InvoiceDate, Common.Date.Format.DDMM)
                if (o.ID == _MONInput_ExtReturnQuick.Data.LastInvoiceNo.DITOProductID)
                    o.InvoiceNo = _MONInput_ExtReturnQuick.Data.LastInvoiceNo.InvoiceNo;
            })
        }
    }


    $scope.numQuantyOptions = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.cboExtReturnStatusOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ValueOfVar: { type: 'string' },
                    ID: { type: 'number' },
                }
            }),

    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _MONInput_ExtReturn.URL.ExtReturnStatus,
        data: {},
        success: function (res) {
            $scope.cboExtReturnStatusOptions.dataSource.data(res.Data)
        }
    });

    $scope.Quick_SaveClick = function ($event, data) {
        $event.preventDefault();
        
        var str = data.InvoiceDateString.split("/");
        debugger
        if (str.length == 2) {
            if (Common.HasValue(data.InvoiceNo) && data.InvoiceNo != '') {
                var day = parseInt(str[0]);
                var month = parseInt(str[1]);
                if (day > 0 && day <= 31 && month > 0 && month < 13) {
                    var today = new Date();
                    if (month > today.getMonth() + 1 && month == 1)
                        data.InvoiceDate = new Date(today.getFullYear() - 1, month - 1, day);
                    else data.InvoiceDate = new Date(today.getFullYear(), month - 1, day);
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: _MONInput_ExtReturnQuick.URL.QuickSave,
                        data: { 'item': data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.extReturn_Quick_Grid.dataSource.read();
                        }
                    })
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                }
            } else $rootScope.Message({ Msg: 'Số chứng từ không được trống', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }

    }

    $scope.QuickSaveByEnter = function ($event, data, grid) {
        if ($event.which === 13) {
            Common.Log("QuickSaveByEnter");
            var error = true;
            var str = data.InvoiceDateString.split("/");

            if (str.length == 2) {
                if (Common.HasValue(data.InvoiceNo) && data.InvoiceNo != '') {
                    var day = parseInt(str[0]);
                    var month = parseInt(str[1]);
                    if (day > 0 && day <= 31 && month > 0 && month < 13) {
                        var today = new Date();
                        if (month > today.getMonth() + 1 && month == 1)
                            data.InvoiceDate = new Date(today.getFullYear() - 1, month - 1, day);
                        else data.InvoiceDate = new Date(today.getFullYear(), month - 1, day);
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.MON,
                            method: _MONInput_ExtReturnQuick.URL.QuickSave,
                            data: { 'item': data },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.extReturn_Quick_Grid.dataSource.read();
                            }
                        })
                    }
                    else {
                        $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                    }
                } else $rootScope.Message({ Msg: 'Số chứng từ không được trống', NotifyType: Common.Message.NotifyType.ERROR });
            }
            else {
                $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    };

    $scope.ChangeInvoiceDate = function ($event, data) {
        if ($event.which === 38) {
            //up arrow
            data.InvoiceDate = Common.Date.AddDay(data.InvoiceDate, 1);
            data.InvoiceDateString = Common.Date.ToString(data.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 40) {
            //down arrow
            data.InvoiceDate = Common.Date.AddDay(data.InvoiceDate, -1);
            data.InvoiceDateString = Common.Date.ToString(data.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 13) {
            $scope.QuickSaveByEnter($event, data);
        }
    }
    //#endregion

    //actions

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Input_ExtReturn");
    };
}]);