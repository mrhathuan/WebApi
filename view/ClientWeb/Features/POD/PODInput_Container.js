/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODInput_Container = {
    URL: {
        Read: 'PODCOInput_List',
        Update: 'PODCOInput_Save'
    },
    Data: {
        DIPODStatus: []
    }
}

//#endregion

angular.module('myapp').controller('PODInput_ContainerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODInput_ContainerCtrl');
    $rootScope.IsLoading = false;

    $scope.PODInputItem = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID:[]
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
        if (!Common.HasValue($scope.PODInputItem.DateFrom) || !Common.HasValue($scope.PODInputItem.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.PODInputItem.DateFrom > $scope.PODInputItem.DateTo) {
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
            $scope.PODInput_gridOptions.dataSource.read();
        }
    }


    var CS = $.extend({
        CustomerCode: 100, CustomerName: 150, MasterCode: 100, OrderCode: 100, ETD: 150, ETA: 150, DateFromCome: 150,
        DateFromLeave: 150, DateToCome: 150, DateToLeave: 150, IsInvoice: 80, InvoiceNo: 110, InvoiceBy: 150, InvoiceDate: 150,
    }, true, $rootScope.GetColumnSettings('PODInput_grid'));

    $scope.PODInput_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_Container.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.PODInputItem.DateFrom,
                    'dtTo': $scope.PODInputItem.DateTo,
                    'listCustomerID': $scope.PODInputItem.ListCustomerID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    DateFromCome: { type: 'date' },
                    DateFromLeave: { type: 'date' },
                    DateToCome: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    InvoiceDate: { type: 'date' },
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '45px', field: 'F_Command',
                template: '<a href="/" ng-click="PODInput_SaveClick($event,PODInput_grid)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false,
            },
            { field: 'CustomerCode', width: CS['CustomerCode'], title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: CS['CustomerName'], title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: CS['MasterCode'], title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: CS['OrderCode'], title: 'Mã đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: CS['ETD'], title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'ETA', width: CS['ETA'], title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'DateFromCome', width: CS['DateFromCome'], title: 'Ngày đến kho', template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMY(DateFromCome)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'DateFromLeave', width: CS['DateFromLeave'], title: 'Ngày rời kho', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                }
            },
             {
                 field: 'DateToCome', width: CS['DateToCome'], title: 'Ngày đến NPP', template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMY(DateFromCome)#",
                 filterable: {
                     cell: {
                         template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                     }
                 }
             },
            {
                field: 'DateToLeave', width: CS['DateToLeave'], title: 'Ngày rời NPP', template: "#=DateToLeave==null?' ':Common.Date.FromJsonDMY(DateToLeave)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                }
            },
    
            {
                field: 'IsInvoice', width: CS['IsInvoice'], title: 'Đã nhận', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsInvoice'></input>",
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
                }
            },
            {
                field: 'InvoiceNo', width: CS['InvoiceNo'], title: 'Số chứng từ',
                template: '<input  class="k-textbox " ng-model="dataItem.InvoiceNo" #= IsInvoice ? disabled="disabled": "" # style="width:100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceBy', width: CS['InvoiceBy'], title: 'Người nhận chứng từ',
                template: '<input  class="k-textbox " ng-model="dataItem.InvoiceBy" #= IsInvoice ? disabled="disabled": "" # style="width:100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', width: CS['InvoiceDate'],
                title: 'Ngày nhận chứng từ', template: '<input  class="cus-datetimepicker dtpInvoiceDate" #= IsInvoice ? disabled="disabled": "" #></input>',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: ' ', field: 'F_Empty', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
            var grid = this;
            if (Common.HasValue(grid) && Common.HasValue(grid.tbody)) {
                var lst = grid.tbody.find('tr');
                $.each(lst, function (i, tr) {
                    var item = grid.dataItem(tr);
                    var classInvoiceDate = $(tr).find('.dtpInvoiceDate');
                    var dtpInvoiceDate = classInvoiceDate.kendoDateTimePicker({
                        value: item.InvoiceDate,
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                        change: function (e) {
                            item.InvoiceDate = this.value();
                        }
                    }).data("kendoDateTimePicker");

                    dtpInvoiceDate.enable(!item.IsInvoice)
                })
            }
        }
    }

    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.PODInput_grid);
    }, 100);


    $scope.PODInput_SaveClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODInput_Container.URL.Update,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.PODInput_gridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    //actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
}]);