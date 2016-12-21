/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _PODTime = {
    URL: {
        Read: 'PODTime_PODTime_List',
        Save: 'PODTime_PODTime_SaveList'
    }
}

angular.module('myapp').controller('PODTime_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODTime_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.PODTime_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            sort: { field: "ETD", dir: "desc" },
            method: _PODTime.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsComment: { type: 'bool', editable: false },
                    ETD: { type: 'date', editable: false },
                    ETARequest: { type: 'date', editable: false },
                    CreatedDate: { type: 'date', editable: false },

                    DateToLoadEnd: { type: 'date' },
                    DateToLoadStart: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    DateToCome: { type: 'date' },

                    DateFromLoadEnd: { type: 'date' },
                    DateFromLoadStart: { type: 'date' },
                    DateFromLeave: { type: 'date' },
                    DateFromCome: { type: 'date' },

                    RequestDate: { type: 'date', editable: false },
                    IsComplete: { type: 'number', editable: false },
                    Code: { type: 'string', editable: false },
                    CustomerName: { editable: false },
                    SOCode: { editable: false },
                    DNCode: { editable: false },
                    OrderCode: { editable: false },
                    LocationToCode: { editable: false },
                    LocationToName: { editable: false },
                    LocationToAddress: { editable: false },
                    LocationToProvince: { editable: false },
                    LocationToDistrict: { editable: false },
                    EconomicZone: { editable: false },
                    LocationFromCode: { editable: false },
                    Quantity: { editable: false },
                    QuantityTransfer: { editable: false },
                    RegNo: { editable: false },
                    DriverName: { editable: false },
                    DriverTel: { editable: false },
                    MasterCode: { editable: false },
                    KPIOPSDate: { type: 'date', editable: false },
                    KPIPODDate: { type: 'date', editable: false },
                    IsKPIOPS: { type: 'boolean', editable: false },
                    IsKPIPOD: { type: 'boolean', editable: false },

                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell',
        columns: [
            { field: 'CustomerName', width: "110px", title: "{{RS.CUSCustomer.Code}}", filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: "110px", title:"{{ RS.ORDGroupProduct.SOCode}}", filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: "110px", title: "{{RS.OPSDITOGroupProduct.DNCode}}", filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: "120px", title:"{{ RS.ORDOrder.Code}}", filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: "120px", title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: "120px", title: 'Tên NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: "120px", title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToProvince', width: "110px", title: 'Tỉnh thành', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToDistrict', width: "110px", title: 'Quận huyện', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EconomicZone', width: "110px", title: 'RouteID', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: "120px", title: 'Mã kho', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Quantity', width: "86px", title: 'Số lượng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'QuantityTransfer', width: "110px", title: 'Số lượng thực',
                template: "<span class='lblTransfer'>#=IsComplete==1?QuantityTransfer:' '#</span>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'RegNo', width: "100px", title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', width: "120px", title: 'Tên tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverTel', width: "120px", title: 'SĐT Tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: "100px", title: 'Lệnh', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'RequestDate', width: "120px", title: 'Ngày gửi',
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }

            },
            {
                field: 'ETARequest', width: "150px", title: 'T/g y/c giao hàng',
                template: "#=ETARequest==null?' ':Common.Date.FromJsonDMYHMS(ETARequest)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DateFromCome', width: 150, title: 'Ngày đến kho',
                template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMYHMS(DateFromCome)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            {
                field: 'DateFromLoadStart', width: 150, title: 'T/g vào máng',
                template: "#=DateFromLoadStart==null?' ':Common.Date.FromJsonDMYHMS(DateFromLoadStart)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            {
                field: 'DateFromLoadEnd', width: 150, title: 'T/g rời máng',
                template: "#=DateFromLoadEnd==null?' ':Common.Date.FromJsonDMYHMS(DateFromLoadEnd)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            {
                field: 'DateFromLeave', width: 150, title: 'Ngày rời kho',
                template: "#=DateFromLeave==null?' ':Common.Date.FromJsonDMYHMS(DateFromLeave)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            {
                field: 'DateToCome', width: 150, title: 'Ngày đến NPP',
                template: "#=DateToCome==null?' ':Common.Date.FromJsonDMYHMS(DateToCome)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            {
                field: 'DateToLoadStart', width: 150, title: 'T/g bắt đầu dỡ hàng',
                template: "#=DateToLoadStart==null?' ':Common.Date.FromJsonDMYHMS(DateToLoadStart)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            {
                field: 'DateToLoadEnd', width: 150, title: 'T/g kết thúc dỡ hàng',
                template: "#=DateToLoadEnd==null?' ':Common.Date.FromJsonDMYHMS(DateToLoadEnd)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            {
                field: 'DateToLeave', width: 150, title: 'Ngày rời NPP',
                template: "#=DateToLeave==null?' ':Common.Date.FromJsonDMYHMS(DateToLeave)#",
                filterable: { cell: { operator: 'gte', showOperators: false } },
                editor: function (container, options) {
                    var input = $("<input focus-k-datetimepicker />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                    });
                }
            },
            { field: 'KPIOPSDate', width: "120px", title: 'Ngày v.c hợp đồng', template: "#=KPIOPSDate==null?' ':Common.Date.FromJsonDMY(KPIOPSDate)#", sortable: false, filterable: false, menu: false },
            {
                field: 'IsKPIOPS', width: "120px", title: 'Đạt KPI v.c', sortable: false, filterable: false, menu: false,
                template: '<input ng-show="dataItem.IsKPIOPS != null" disabled type="checkbox" #= IsKPIOPS ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            { field: 'KPIPODDate', width: "120px", title: 'Ngày c.t hợp đồng', template: "#=KPIPODDate==null?' ':Common.Date.FromJsonDMY(KPIPODDate)#", sortable: false, filterable: false, menu: false },
            {
                field: 'IsKPIPOD', width: "120px", title: 'Đạt KPI c.t', sortable: false, filterable: false, menu: false,
                template: '<input ng-show="dataItem.IsKPIPOD != null" disabled type="checkbox" #= IsKPIPOD ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    }

    $scope.PODTime_Save_Click = function ($event, grid) {
        $event.preventDefault();
        
        var lst = [];
        lst = $.grep($scope.PODTime_Grid_Options.dataSource.data(), function (item) { return item.dirty == true });
        if (lst.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODTime.URL.Save,
                data: { 'lst': lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.PODTime_Grid_Options.dataSource.read();
                }
            })
        }
    }

    $scope.PODTime_Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.PODInput.Index")
    }

    //Actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
}])