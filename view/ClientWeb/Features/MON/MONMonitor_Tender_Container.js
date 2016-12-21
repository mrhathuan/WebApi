/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONMonitor_Input_Container = {
    URL: {
        Read: 'MONCOInput_List',
        Update: 'MONCOInput_Save',
        Customer_List: "Customer_List",
        Vendor_List: "Vendor_List",
        Reset: 'PODDIInput_Check_Reset',
    },
    Data: {
        DIPODStatus: [],
        ListProduct: [],
    }
}

//#endregion

angular.module('myapp').controller('MONMonitor_Tender_ContainerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('MONMonitor_Tender_ContainerCtrl');
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
            method: _MONMonitor_Input_Container.URL.Customer_List,
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
        method: _MONMonitor_Input_Container.URL.Customer_List,
        data: {},
        success: function (res) {
            $scope.mts_CustomerOptions.dataSource.data(res.Data)
        }
    });

    // Phần vendor_List
    $scope.mts_VendorOptions = {
        dataSource: Common.DataSource.Local({
            url: Common.Services.url.POD,
            method: _MONMonitor_Input_Container.URL.Vendor_List,
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
        method: _MONMonitor_Input_Container.URL.Vendor_List,
        data: {},
        success: function (res) {
            $scope.mts_VendorOptions.dataSource.data(res.Data)
        }
    });

    $scope.SearchClick = function ($event) {
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
            $scope.MONInput_Container_grid.dataSource.read();
        }
    }

    $scope.MONInput_Container_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONCO_TenderList",
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
                    IsChoose: { type: 'boolean', },
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

                    OrderCode: { type: 'string', editable: false },
                    MasterCode: { type: 'string', editable: false },
                    VehicleNo: { type: 'string', editable: false },
                    RomoocNo: { type: 'string', editable: false },
                    TypeOfContainer: { type: 'string', editable: false },
                    CustomerCode: { type: 'string', editable: false },
                    CustomerName: { type: 'string', editable: false },
                    F_Command1: { type: 'string', editable: false },
                }
            },
            sort: [{ field: 'MasterID', dir: "asc" }]
        }),
        selectable: false, reorderable: false, editable: true,
        height: '100%', pageable: Common.PageSize, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,MONInput_Container_grid,MONInput_Container_gridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,MONInput_Container_grid,MONInput_Container_gridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: 80, field: 'F_Command1', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="LineSave($event,dataItem)" ng-show="!dataItem.IsInvoice" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false,
            },
            {
                field: 'IsInvoice', width: 50, title: 'Đã nhận', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsInvoice' ng-disabled='true'></input>",
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
                field: 'InvoiceNo', width: 100, sortorder: 2, configurable: true, isfunctionalHidden: false,
                title: '{{RS.OPSDITOGroupProduct.InvoiceNote}}',
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.InvoiceNo"  ng-keydown="QuickSave($event,dataItem)" ng-change="InvoiceNo_Change($event,dataItem)" style="width: 100%" ng-disabled="dataItem.IsInvoice"  />',

                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'OrderCode', width: 100, sortorder: 6, configurable: true, isfunctionalHidden: false, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Note1', width: 100, sortorder: 10, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Note1"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ng-disabled="dataItem.IsInvoice"  />'
            },
            {
                field: 'Note2', width: 100, sortorder: 11, configurable: true, isfunctionalHidden: false, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.Note2"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ng-disabled="dataItem.IsInvoice"  />'
            },

            {
                field: 'ContainerNo', width: 100, sortorder: 12, configurable: true, isfunctionalHidden: false, title: 'Số cont', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.ContainerNo"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ng-disabled="dataItem.IsInvoice"  />'
            },
            {
                field: 'SealNo1', width: 100, sortorder: 13, configurable: true, isfunctionalHidden: false, title: 'Seal 1', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.SealNo1"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ng-disabled="dataItem.IsInvoice"  />'
            },
            {
                field: 'SealNo2', width: 100, sortorder: 14, configurable: true, isfunctionalHidden: false, title: 'Seal 2', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input type="text"  class="k-textbox "  ng-model="dataItem.SealNo2"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ng-disabled="dataItem.IsInvoice"  />'
            },
            { field: 'MasterCode', width: 100, sortorder: 15, configurable: true, isfunctionalHidden: false, title: 'Lệnh v.c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleNo', width: 100, sortorder: 16, configurable: true, isfunctionalHidden: false, title: 'Số đầu kéo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RomoocNo', width: 100, sortorder: 17, configurable: true, isfunctionalHidden: false, title: 'Số romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainer', width: 100, sortorder: 18, configurable: true, isfunctionalHidden: false, title: 'Loại Cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrder', width: 100, sortorder: 19, configurable: true, isfunctionalHidden: false, title: 'Dịch vụ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfStatusContainerName', width: 100, sortorder: 19, configurable: true, isfunctionalHidden: false, title: 'Chặng cont', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDate', width: 100, sortorder: 20, configurable: true, isfunctionalHidden: false, title: 'Ngày gửi', template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHMS(RequestDate)#", filterable: false },
            { field: 'CustomerCode', width: 100, sortorder: 21, configurable: true, isfunctionalHidden: false, title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: 100, sortorder: 22, configurable: true, isfunctionalHidden: false, title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'DateFromCome', width: 150, sortorder: 26, configurable: true, isfunctionalHidden: false, title: 'Ngày nhận', template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMYHM(DateFromCome)#", filterable: false,
                template: '<input ng-focus="OpenEditCell($event)" style="width:100%" class="k-textbox" value="#=DateFromCome==null?\' \':Common.Date.FromJsonDMYHM(DateFromCome)# " />',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM"  />');
                    var data = $scope.MONInput_Container_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
            },
            {
                field: 'DateToCome', width: 150, sortorder: 28, configurable: true, isfunctionalHidden: false, title: 'Ngày giao', filterable: false,
                template: '<input ng-focus="OpenEditCell($event)" style="width:100%" class="k-textbox" value="#=DateToCome==null?\' \':Common.Date.FromJsonDMYHM(DateToCome)# " />',
                editor: function (container, options) {
                    var input = $('<input class="cus-datepicker" kendo-date-time-picker focus-k-datepicker k-options="DateDMYHM"  />');
                    var data = $scope.MONInput_Container_grid.dataItem(container.closest('tr'));
                    if (data.IsInvoice)
                        input = $("<span></span>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                },
            },
            { field: 'LocationFromCode', width: 100, sortorder: 30, configurable: true, isfunctionalHidden: false, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 100, sortorder: 31, configurable: true, isfunctionalHidden: false, title: 'Tên điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 100, sortorder: 32, configurable: true, isfunctionalHidden: false, title: 'Địa chỉ đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 100, sortorder: 33, configurable: true, isfunctionalHidden: false, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 100, sortorder: 34, configurable: true, isfunctionalHidden: false, title: 'Tên điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 100, sortorder: 35, configurable: true, isfunctionalHidden: false, title: 'Địa chỉ đến', filterable: { cell: { operator: 'contains', showOperators: false } } },

            { title: ' ', sortable: false, filterable: false, menu: false, sortorder: 999, configurable: false, isfunctionalHidden: false, }
        ],
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var item = grid.dataItem($(tr));
                if (item.IsComplete) {

                    $(tr).css("background-color", "#73C95F");
                    angular.forEach($(tr).find('td'), function (td, i) {
                        $(td).css("color", "#000");
                    })
                }
            });
        }
    }

    $scope.OpenEditCell = function (e) {
        var td = e.target.closest('td');
        $(e.target).hide();
        $timeout(function () {
            $(td).trigger("click");
        }, 100)

    }

    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.MONInput_Container_grid);
    }, 100);

    $scope.InvoiceNo_Change = function (e, data) {
        if (data.InvoiceNo != "") {
            if (data.InvoiceBy == "" || !Common.HasValue(data.InvoiceBy)) {
                data.InvoiceBy = $rootScope.Default_UserName;
            }
            if (data.InvoiceDateString == "") {
                data.InvoiceDateString = Common.Date.FromJsonDDMM(new Date());
            }
        }
    }

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

    $scope.QuickSave = function ($event, data) {
        var grid = $scope.MONInput_Container_grid;
        if ($event.which === 13) {
            var error = false;
            var ispod = false;
            if (Common.HasValue(data.InvoiceNo) && data.InvoiceNo != '') {
                var str = data.InvoiceDateString.split("/");
                if (str.length == 2) {
                    var day = parseInt(str[0]);
                    var month = parseInt(str[1]);
                    ispod = true;
                    if (day > 0 && day <= 31 && month > 0 && month < 13) {
                        data.InvoiceDate = new Date(new Date().getFullYear(), month - 1, day);
                    }
                    else {
                        $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                        error = true;
                    }
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                    error = true;
                }
            }

            if (!error) {
                if (!data.IsInvoice) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONCO_TenderSave",
                        data: { item: data},
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.MONInput_Container_grid.dataSource.read();
                        }
                    })
                }
            }
        }
    }

    $scope.LineSave = function ($event, data) {
        $event.preventDefault();
        var grid = $scope.MONInput_Container_grid;
        var error = false;
        var ispod = false;
        if (Common.HasValue(data.InvoiceNo) && data.InvoiceNo != '') {
            var str = data.InvoiceDateString.split("/");
            if (str.length == 2) {
                var day = parseInt(str[0]);
                var month = parseInt(str[1]);
                ispod = true;
                if (day > 0 && day <= 31 && month > 0 && month < 13) {
                    data.InvoiceDate = new Date(new Date().getFullYear(), month - 1, day);
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                    error = true;
                }
            }
            else {
                $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                error = true;
            }
        }

        if (!error) {
            if (!data.IsInvoice) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONCO_TenderSave",
                    data: { item: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.MONInput_Container_grid.dataSource.read();
                    }
                })
            }
        }
    }

    //tool bar
    $scope.MasterActualTime_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    MasterCode: { editable: false },
                    ID: { type: 'number' },
                    ATA: { type: 'date' },
                    ATD: { type: 'date' },
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: true,
        columns: [
            { field: 'MasterCode', title: '{{RS.OPSDITOMaster.Code}}', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'ATD', title: '{{RS.OPSDITOMaster.ATD}}', width: 150,
                template: '<input style="width:99%;" class="cus-datepicker" focus-k-datetimepicker kendo-date-time-picker k-ng-model="dataItem.ATD" name="ATD" k-options="DateDMYHM" />',
            },
            {
                field: 'ATA', title: '{{RS.OPSDITOMaster.ATA}}', width: 150,
                template: '<input style="width:99%;" class="cus-datepicker" focus-k-datetimepicker kendo-date-time-picker k-ng-model="dataItem.ATA" name="ATA" k-options="DateDMYHM" />',
            },
        ]
    };

    $scope.RevertMaster = function ($event) {
        console.log("RevertMaster")
        $event.preventDefault();

        var lst = [];
        angular.forEach($scope.MONInput_Container_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose)
                lst.push(o.ID);
        })

        if (lst.length > 0) {
            $rootScope.IsLoading = true;
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hủy hoàn thành nhanh chuyến?',
                pars: {},
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONCO_TenderUpdateStatus",
                        data: { lst: lst, type: 2 },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.MONInput_Container_grid.dataSource.read();
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Không có lựa chọn!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Complete = function ($event) {
        console.log("Complete")
        $event.preventDefault();

        var lst = [];
        angular.forEach($scope.MONInput_Container_grid.dataSource.data(), function (o, i) {
            if (o.IsChoose )
                lst.push(o.ID);
        })

        if (lst.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hoàn thành?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONCO_TenderUpdateStatus",
                        data: { lst: lst, type: 1 },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công!', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.MONInput_Container_grid.dataSource.read();
                        }
                    })
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Không có lựa chọn!', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    //#region Confirm window

    $scope.WinConfirmObj = {
        Lable: "Đang tải thông báo...",
        OK: function () { },
        Deny: function () { },
    }
    $scope.CTConfirm = function (opstions) {
        $scope.WinConfirmObj = {
            Lable: "",
            OK: function () {
            },
            Deny: function () {
            },
            TextOK: "Đồng ý",
            TextDeny: "Không đồng ý",
        }
        angular.extend($scope.WinConfirmObj, opstions);

        $scope.WinConfirmObj.Action_OK = function () {
            $scope.WinConfirmObj.OK();
            $scope.Confirm_Win.close();
        }
        $scope.WinConfirmObj.Action_Deny = function () {
            $scope.WinConfirmObj.Deny();
            $scope.Confirm_Win.close();
        }
        $scope.Confirm_Win.center().open();
    }

    //#endregion

    //actions
    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.PODInput.Index")
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.MONMonitor,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }

}]);