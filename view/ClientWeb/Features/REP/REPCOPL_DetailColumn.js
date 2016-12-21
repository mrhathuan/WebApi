
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPCOPL_DetailColumn = {
    URL: {
        Search: 'REPCOPL_ColumnDetailData',

        Read_Customer: 'REP_Customer_Read',
        Read_GroupOfProduct: 'REP_GroupOfProduct_Read',

        Excel_Export: 'REPCOPL_DetailColumn_Export',

        SettingList: 'CUSSettingsReport_List',
        SettingSave: 'CUSSettingsReport_Save',
        SettingDelete: 'CUSSettingsReport_Delete',
        SettingTemplate: 'CUSSettingsReport_Template',

        SettingCusDeleteList: 'CUSSettingReport_CustomerDeleteList',
        SettingCusNotInList: 'CUSSettingReport_CustomerNotInList',
        SettingCusNotInSave: 'CUSSettingReport_CustomerNotInSave',


        SettingProvinceInList: 'CUSSettingReport_ProvinceNotInList',
        SettingProvinceDeleteList: 'CUSSettingReport_ProvinceDeleteList',
        SettingProvinceNotInSave: 'CUSSettingReport_ProvinceNotInSave',

        SettingServiceOfOrderInList: 'CUSSettingReport_ServiceOfOrderNotInList',
        SettingServiceOfOrderNotInSave: 'CUSSettingReport_ServiceOfOrderNotInSave',
        SettingServiceOfOrderDeleteList: 'CUSSettingReport_ServiceOfOrderDeleteList',


        SettingAction: 'REPCOPL_SettingDownload',
    },
    Data: {
        _listColumn: [],
        _listColumnKey: [],
        _listData: [],
    }
}

angular.module('myapp').controller('REPCOPL_DetailColumnCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPCOPL_DetailColumnCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };
    $scope.arrayColumn = ['Cost', 'CostEx', 'CostQuantity', 'CostTrouble', 'CostUnitPrice', 'Income', 'IncomeEx', 'IncomeQuantity', 'IncomeTrouble', 'IncomeUnitPrice', 'PackingCode', 'GroupOfProductName', 'Description', 'CostDepreciation', 'CostSchedule', 'CostDriver', 'CostStation',
                           'IncomeServiceQuantity', 'IncomeServiceUnitPrice', 'IncomeService', 'CostServiceQuantity', 'CostServiceUnitPrice', 'CostService'];
    $scope.arrayColumnRS = ['Chi phí v.chuyển', 'Chi phí phụ thu', 'Chi số lượng', 'Chi phí phát sinh', 'Chi đơn giá', 'Doanh thu', 'Thu phụ thu', 'Thu số lượng', 'Thu phát sinh', 'Thu đơn giá', 'Loại cont', 'Nhóm hàng', 'Mô tả nhóm hàng', 'Chi phí khấu hao', 'Chi phí hàng tháng', 'Chi phí tài xế', 'Chi phí trạm',
                             'Số lượng thu của dịch vụ', 'Giá thu của dịch vụ', 'Dịch vụ thu', 'Số lượng chi của dịch vụ', 'Giá chi của dịch vụ', 'Dịch vụ chi' ];

    $scope.fieldGrid = [];

    //#region search
    $scope.Item = {
        lstCustomerID: [],
        lstGroupID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains',
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerName: { type: 'string' },
                    ID: { type: 'number' },
                }
            }
        }),
        change: function () {
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
        },
    };
    $scope.isWidth = false;
    $scope.SetWidth_Select = function (lst) {
        var list = lst;
        var lst1 = lst.wrapper.find('.k-floatwrap:first ul li');
        var widthDiv = lst.wrapper.find('.k-floatwrap:first').width();
        var w = 0;
        var obj = null;
        var lst2 = [];
        if (lst1.length > 1) {
            $.each(lst1, function (i, v) {
                if ($(v).attr('data-show') != 'unshow') {
                    lst2.push(v);
                }
            });
        }
        else {
            lst2 = lst1;
        }

        $.each(lst2, function (i, v) {
            w += $(v).outerWidth(true);
            $(v).attr('data-show', 'show')
            if (w >= widthDiv) {
                $(v).hide();
                $(v).attr('data-show', 'unshow');
            }
            obj = v;
        });
        if (obj == null) {
            $scope.isWidth = false;
        }
        if (w >= widthDiv && !$scope.isWidth) {
            $scope.isWidth = true;
            $(obj).show();
            $(obj).html('...');
        }
        if (w > widthDiv) {
            $scope.SetWidth_Select(list);
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPCOPL_DetailColumn.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

    $scope.dtpfilter = function () {
        $timeout(function () {
            $('.dtp-filter-from').kendoDatePicker({
                format: Common.Date.Format.DDMMYY,
                change: function (e) {
                    try {
                        var element = this.wrapper.parent();
                        var field = e.sender.element.data('field');
                        var dtp_To = $(element).find('.dtp-filter-to[data-field=' + field + ']').data('kendoDatePicker');
                        var f = this.value();
                        var t = dtp_To.value();

                        var f_filter = { field: field, operator: "gte", value: f };
                        var t_filter = { field: field, operator: "lte", value: t };
                        var filters = [];
                        if (Common.HasValue($scope.REPCOPL_gridOptions.dataSource.filter())) {
                            filters = $scope.REPCOPL_gridOptions.dataSource.filter().filters;
                            if (Common.HasValue(f)) {
                                var index = 0;
                                var isNew = true;
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(f_filter);
                                }
                                else {
                                    filters[index] = f_filter;
                                }
                            }
                            else {
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                            if (Common.HasValue(t)) {
                                var isNew = true;
                                var index = 0;
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(t_filter);
                                }
                                else {
                                    filters[index] = t_filter;
                                }
                            }
                            else {
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                        }
                        else {
                            if (Common.HasValue(f))
                                filters.push(f_filter);
                            if (Common.HasValue(t))
                                filters.push(t_filter);

                        }
                        $scope.REPCOPL_gridOptions.dataSource.filter(filters);
                    }
                    catch (e) {
                        $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                    }
                }
            })

            $('.dtp-filter-to').kendoDatePicker({
                format: Common.Date.Format.DDMMYY,
                change: function (e) {
                    try {
                        var element = this.wrapper.parent();
                        var field = e.sender.element.data('field');
                        var dtp_From = $(element).find('.dtp-filter-from[data-field=' + field + ']').data('kendoDatePicker');
                        var f = dtp_From.value();
                        var t = this.value();

                        var f_filter = { field: field, operator: "gte", value: f };
                        var t_filter = { field: field, operator: "lte", value: t };

                        var filters = [];
                        if (Common.HasValue($scope.REPCOPL_gridOptions.dataSource.filter())) {
                            filters = $scope.REPCOPL_gridOptions.dataSource.filter().filters;
                            if (Common.HasValue(f)) {
                                var index = 0;
                                var isNew = true;
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(f_filter);
                                }
                                else {
                                    filters[index] = f_filter;
                                }
                            }
                            else {
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                            if (Common.HasValue(t)) {
                                var isNew = true;
                                var index = 0;
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(t_filter);
                                }
                                else {
                                    filters[index] = t_filter;
                                }
                            }
                            else {
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                        }
                        else {
                            if (Common.HasValue(f))
                                filters.push(f_filter);
                            if (Common.HasValue(t))
                                filters.push(t_filter);
                        }
                        $scope.REPCOPL_gridOptions.dataSource.filter(filters);
                    }
                    catch (e) {
                        $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                    }
                }
            })
        }, 500);
    }
    $scope.SearchData = function () {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPCOPL_DetailColumn.URL.Search,
            data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                angular.forEach(res, function (item, dix) {
                    if (Common.HasValue(item)) {
                        if (Common.HasValue(item.RequestDate))
                            item.RequestDate = kendo.parseDate(item.RequestDate);
                        if (Common.HasValue(item.DateConfig))
                            item.DateConfig = kendo.parseDate(item.DateConfig);
                        if (Common.HasValue(item.OrderDateConfig))
                            item.OrderDateConfig = kendo.parseDate(item.OrderDateConfig);
                        if (Common.HasValue(item.OrderGroupProductDateConfig))
                            item.OrderGroupProductDateConfig = kendo.parseDate(item.OrderGroupProductDateConfig);
                        if (Common.HasValue(item.OPSDateConfig))
                            item.OPSDateConfig = kendo.parseDate(item.OPSDateConfig);
                        if (Common.HasValue(item.OPSGroupProductDateConfig))
                            item.OPSGroupProductDateConfig = kendo.parseDate(item.OPSGroupProductDateConfig);
                        if (Common.HasValue(item.ETD))
                            item.ETD = kendo.parseDate(item.ETD);
                        if (Common.HasValue(item.OrderCreatedDate))
                            item.OrderCreatedDate = kendo.parseDate(item.OrderCreatedDate);
                    }
                });
                _REPCOPL_DetailColumn.Data._listData = res.ListData;
                _REPCOPL_DetailColumn.Data._listColumn = res.ListColumn;
                _REPCOPL_DetailColumn.Data._listColumnKey = res.ListColumnKey;
                //$scope.REPCOPL_gridOptions.dataSource.data(res);
                $scope.InitGrid();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.SearchData();

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.SearchData();
    };
    //#endregion
    //#region grid
    $scope.InitGrid = function () {
        Common.Log("InitGrid");
        var Model = {
            id: 'ID',
            fields: {
                ID: { type: 'number', editable: false },
            },
        }
        var GridColumn = [
            {
                field: 'OrderCode', title: '<b>Đơn hàng</b><br>[OrderCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: '<b>Mã khách hàng</b><br>[CustomerCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', title: '<b>Số Container</b><br>[ContainerNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'COMasterCode', title: '<b>Số chuyến</b><br>[COMasterCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorCode', title: '<b>Mã nhà vận tải</b><br>[VendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateConfig', title: '<b>Ngày tính giá</b><br>[DateConfig]', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(DateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'RequestDate', title: '<b>Ngày y/c v/c</b><br>[RequestDate]', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(RequestDate)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='RequestDate' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='RequestDate' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'CUSRoutingCode', title: '<b>Mã cung đường</b><br>[CUSRoutingCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CUSRoutingName', title: '<b>Cung đường</b><br> [CUSRoutingName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: '<b>Nhà vận tải</b><br>[VendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: '<b>Khách hàng</b><br>[CustomerName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfCOContainerName', title: '<b>Tình trạng chặng</b><br>[StatusOfCOContainerName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeTrouble', title: '<b>>Thu phát sinh</b><br>[IncomeTrouble]', width: '120px', template: '#=IncomeTrouble==null?" ":Common.Number.ToMoney(IncomeTrouble)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeUnitPrice', title: '<b>Giá thu</b><br>[IncomeUnitPrice]', width: '120px', template: '#=IncomeUnitPrice==null?" ":Common.Number.ToMoney(IncomeUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeQuantity', title: '<b>Số lượng thu</b><br>[IncomeQuantity]', width: '120px', template: '#=IncomeQuantity==null?" ":Common.Number.ToNumber3(IncomeQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Income', title: '<b>Cước thu</b><br>[Income]', width: '120px', template: '#=Income==null?" ":Common.Number.ToMoney(Income)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeExUnitPrice', title: '<b>Giá phụ thu</b><br>[IncomeExUnitPrice]', width: '120px', template: '#=IncomeExUnitPrice==null?" ":Common.Number.ToMoney(IncomeExUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeExQuantity', title: '<b>Số lượng phụ thu</b><br>[IncomeExQuantity]', width: '120px', template: '#=IncomeExQuantity==null?" ":Common.Number.ToNumber3(IncomeExQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeEx', title: '<b>Cước phụ thu</b><br>[IncomeEx]', width: '120px', template: '#=IncomeEx==null?" ":Common.Number.ToMoney(IncomeEx)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeExCostName', title: '<b>Loại phụ thu</b><br>[IncomeExCostName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeExNote', title: '<b>Ghi chú phụ thu</b><br>[IncomeExNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostTrouble', title: '<b>Chi phát sinh</b><br>[CostTrouble]', width: '120px', template: '#=CostTrouble==null?" ":Common.Number.ToMoney(CostTrouble)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostUnitPrice', title: '<b>Giá chi</b><br>[CostUnitPrice]', width: '120px', template: '#=CostUnitPrice==null?" ":Common.Number.ToMoney(CostUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostQuantity', title: '<b>Số lượng chi</b><br>[CostQuantity]', width: '120px', template: '#=CostQuantity==null?" ":Common.Number.ToNumber3(CostQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Cost', title: '<b>Cước chi</b><br>[Cost]', width: '120px', template: '#=Cost==null?" ":Common.Number.ToMoney(Cost)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostExUnitPrice', title: '<b>Giá phụ chi</b><br>[CostExUnitPrice]', width: '120px', template: '#=CostExUnitPrice==null?" ":Common.Number.ToMoney(CostExUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostExQuantity', title: '<b>Số lượng phụ chi</b><br>[CostExQuantity]', width: '120px', template: '#=CostExQuantity==null?" ":Common.Number.ToNumber3(CostExQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostEx', title: '<b>Cước phụ chi</b><br>[CostEx]', width: '120px', template: '#=CostEx==null?" ":Common.Number.ToMoney(CostEx)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostExCostName', title: '<b>Loại phụ thu chi</b><br>[CostExCostName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostExNote', title: '<b>Ghi chú phụ thu chi</b><br>[CostExNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostDepreciation', title: '<b>Cước khấu hao</b><br>[CostDepreciation]', width: '120px', template: '#=CostDepreciation==null?" ":Common.Number.ToMoney(CostDepreciation)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostSchedule', title: '<b>Cước cố định</b><br>[CostSchedule]', width: '120px', template: '#=CostSchedule==null?" ":Common.Number.ToMoney(CostSchedule)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostDriver', title: '<b>Cước tài xế</b><br>[CostDriver]', width: '120px', template: '#=CostDriver==null?" ":Common.Number.ToMoney(CostDriver)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostStation', title: '<b>Cước trạm</b><br>[CostStation]', width: '120px', template: '#=CostStation==null?" ":Common.Number.ToMoney(CostStation)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalIncome', title: '<b>Tổng thu</b><br>[TotalIncome]', width: '120px', template: '#=TotalIncome==null?" ":Common.Number.ToMoney(TotalIncome)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalCost', title: '<b>Tổng chi</b><br>[TotalCost]', width: '120px', template: '#=TotalCost==null?" ":Common.Number.ToMoney(TotalCost)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalPL', title: '<b>PL</b><br>[TotalPL]', width: '120px', template: '#=TotalPL==null?" ":Common.Number.ToMoney(TotalPL)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'OrderCreatedBy', title: '<b>Người tạo đơn</b><br>[OrderCreatedBy]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCreatedDate', title: '<b>Ngày tạo đơn</b><br>[OrderCreatedDate]', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(OrderCreatedDate)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OrderCreatedDate' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OrderCreatedDate' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OrderDateConfig', title: '<b>Ngày tính giá đơn hàng</b><br>[OrderDateConfig]', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(OrderDateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OrderDateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OrderDateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OrderContainerDateConfig', title: '<b>Ngày tính giá cont</b><br>[OrderContainerDateConfig]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(OrderContainerDateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OrderContainerDateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OrderContainerDateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OrderContract', title: '<b>Hợp đồng ĐH</b><br>[OrderContract]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderRouting', title: '<b>Cung đường đơn hàng</b><br>[OrderRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSDateConfig', title: '<b>Ngày tính giá chuyến</b><br>[OPSDateConfig]', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(OPSDateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OPSDateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OPSDateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OPSContract', title: '<b>Hợp đồng chuyến</b><br>[OPSContract]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSRouting', title: '<b>Cung đường chuyến</b><br>[OPSRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', title: '<b>KM</b><br>[KM]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'LocationFromCode', title: '<b>Mã điểm đi</b><br>[LocationFromCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', title: '<b>Địa chỉ điểm đi</b><br>[LocationFromAddress]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', title: '<b>Mã điểm đến</b><br>[LocationToCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', title: '<b>Địa chỉ điểm đến</b><br>[LocationToAddress]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToProvince', title: '<b>Tỉnh giao</b><br>[LocationToProvince]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToDistrict', title: '<b>Quận huyện giao</b><br>[LocationToDistrict]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSContainerRouting', title: '<b>Cung đường chuyến</b><br>[OPSContainerRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderContainerNote', title: '<b>Ghi chú cont</b><br>[OrderContainerNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderContainerRouting', title: '<b>Cung đường khách hàng</b><br>[OrderContainerRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingCode', title: '<b>Loại cont</b><br>[PackingCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PercentPLIncome', title: '<b>Tỉ lệ doanh thu</b><br>[PercentPLIncome]', width: '120px', template: '#=PercentPLIncome==null?" ":Common.Number.ToNumber3(PercentPLIncome)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'SealNo1', title: '<b>Số seal 1</b><br>[SealNo1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SealNo2', title: '<b>Số seal 2</b><br>[SealNo2]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', title: '<b>Ngày vận chuyển</b><br>[ETD]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ETD' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ETD' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ETA', title: '<b>ETA</b><br>[ETA]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateFromCome', title: '<b>Thời gian đến điểm đi</b><br>[DateFromCome]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateFromCome)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFromCome' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFromCome' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateFromLeave', title: '<b>Thời gian rời điểm đi</b><br>[DateFromLeave]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateFromLeave)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFromLeave' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFromLeave' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateToCome', title: '<b>Thời gian đến điểm giao</b><br>[DateToCome]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateToCome)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateToCome' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToCome' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateToLeave', title: '<b>Thời gian rời điểm giao</b><br>[DateToLeave]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateToLeave)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateToLeave' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToLeave' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ExternalCode', title: '<b>Mã giao dịch</b><br>[ExternalCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExternalDate', title: '<b>Ngày giao dịch</b><br>[ExternalDate]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine1', title: '<b>Định nghĩa 1</b><br>[UserDefine1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine2', title: '<b>Định nghĩa 2</b><br>[UserDefine2]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine3', title: '<b>Định nghĩa 3</b><br>[UserDefine3]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderIncome ', title: '<b>Loại dịch vụ thu</b><br>[ServiceOfOrderIncome]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderCost ', title: '<b>Loại dịch vụ chi</b><br>[ServiceOfOrderCost ]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractTermIncome ', title: '<b>Phụ lục thu</b><br>[ContractTermIncome ]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractTermCost ', title: '<b>Phụ lục chi</b><br>[ContractTermCost ]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CutOffTime', title: '<b>CutOffTime</b><br>[CutOffTime]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(CutOffTime)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='CutOffTime' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='CutOffTime' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'Date_TimeGetEmpty', title: '<b>Ngày giờ lấy rỗng</b><br>[Date_TimeGetEmpty]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(Date_TimeGetEmpty)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='Date_TimeGetEmpty' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='Date_TimeGetEmpty' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'Date_TimeReturnEmpty', title: '<b>Ngày giờ trả rỗng</b><br>[Date_TimeReturnEmpty]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(Date_TimeReturnEmpty)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='Date_TimeReturnEmpty' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='Date_TimeReturnEmpty' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ETARequest', title: '<b>Ngày y/c giao hàng</b><br>[ETARequest]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(ETARequest)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ETARequest' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ETARequest' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'TypeOfContainerName', title: '<b>Loại container</b><br>[TypeOfContainerName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CarrierCode', title: '<b>Mã hãng tàu</b><br>[CarrierCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CarrierName', title: '<b>Tên hãng tàu</b><br>[CarrierName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationDepotCode', title: '<b>Mã điểm lấy rỗng</b><br>[LocationDepotCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationDepotName', title: '<b>Tên điểm lấy rỗng</b><br>[LocationDepotName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationReturnCode', title: '<b>Mã điểm trả rỗng</b><br>[LocationReturnCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationReturnName', title: '<b>Tên điểm trả rỗng</b><br>[LocationReturnName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TripNo', title: '<b>Số chuyến</b><br>[TripNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VesselNo', title: '<b>Số hiệu tàu</b><br>[VesselNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VesselName', title: '<b>Tên tàu</b><br>[VesselName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RegWeight', title: '<b>Trọng tải đăng ký xe</b><br>[RegWeight]', width: '120px', template: '#=RegWeight==null?" ":Common.Number.ToNumber3(RegWeight)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'RegCapacity', title: '<b>Số khối đăng ký xe</b><br>[RegCapacity]', width: '120px', template: '#=RegCapacity==null?" ":Common.Number.ToNumber3(RegCapacity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeServiceQuantity', title: '<b>Số lượng thu của dịch vụ</b><br>[IncomeServiceQuantity]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeServiceUnitPrice', title: '<b>Giá thu của dịch vụ</b><br>[IncomeServiceUnitPrice]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeService', title: '<b>Dịch vụ thu</b><br>[IncomeService]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostServiceQuantity', title: '<b>Số lượng chi của dịch vụ</b><br>[CostServiceQuantity]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostServiceUnitPrice', title: '<b>Giá chi của dịch vụ</b><br>[CostServiceUnitPrice]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostService', title: '<b>Dịch vụ chi</b><br>[CostService]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]

        $scope.fieldGrid = [];
        Common.Data.Each(_REPCOPL_DetailColumn.Data._listColumnKey, function (group) {
            var array = []; 
            for (var i = 0 ; i < $scope.arrayColumn.length ; i++) {
                var field = $scope.arrayColumn[i] + group.KeyCode;
                field = field.replace(/([ -.,*+?^$|(){}\[\]])/g, "_");
                Model.fields[field] = {
                    type: "number", editable: true,
                    filterable: { cell: { operator: 'equal', showOperators: false } }
                };
                array.push(field);
                GridColumn.push({
                    field: field, title: '<b>' + group.KeyCode + "-" + $scope.arrayColumnRS[i] + '</b><br>' + '[' + group.KeyCode + '-' + $scope.arrayColumn[i] + ']', width: 120, locked: false,
                });
            }
            $scope.fieldGrid.push(array);
        })

        GridColumn.push({ title: ' ', filterable: false, sortable: false })
        $scope.rep_grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                pageSize: 20,
            }),
            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: false, pazeSize: 20,
            columns: GridColumn
        })

        var dataGrid = [];
        Common.Data.Each(_REPCOPL_DetailColumn.Data._listData, function (data) {
            for (var i = 0 ; i < _REPCOPL_DetailColumn.Data._listColumn.length ; i++) {
                var group = _REPCOPL_DetailColumn.Data._listColumn[i];
                if (group.COTOMasterID == data.COTOMasterID && group.OrderID == data.OrderID && group.LocationFromID == data.LocationFromID && group.LocationToID == data.LocationToID) {
                    var count = 0;
                    Common.Data.Each(_REPCOPL_DetailColumn.Data._listColumnKey, function (key) {
                        var dataGroup = null;
                        if (group.KeyCode == key.KeyCode && group.PackingID == key.PackingID) {
                            dataGroup = group;
                            _REPCOPL_DetailColumn.Data._listColumn.splice(i, 1);
                        }

                        if (dataGroup != null) {
                            data[$scope.fieldGrid[count][0]] = dataGroup.Cost;
                            data[$scope.fieldGrid[count][1]] = dataGroup.CostEx;
                            data[$scope.fieldGrid[count][2]] = dataGroup.CostQuantity;
                            data[$scope.fieldGrid[count][3]] = dataGroup.CostTrouble;
                            data[$scope.fieldGrid[count][4]] = dataGroup.CostUnitPrice;
                            data[$scope.fieldGrid[count][5]] = dataGroup.Income;
                            data[$scope.fieldGrid[count][6]] = dataGroup.IncomeEx;
                            data[$scope.fieldGrid[count][7]] = dataGroup.IncomeQuantity;
                            data[$scope.fieldGrid[count][8]] = dataGroup.IncomeTrouble;
                            data[$scope.fieldGrid[count][9]] = dataGroup.IncomeUnitPrice;
                            data[$scope.fieldGrid[count][10]] = dataGroup.PackingCode;
                            data[$scope.fieldGrid[count][11]] = dataGroup.GroupOfProductName;
                            data[$scope.fieldGrid[count][12]] = dataGroup.Description;
                            data[$scope.fieldGrid[count][13]] = dataGroup.CostDepreciation;
                            data[$scope.fieldGrid[count][14]] = dataGroup.CostSchedule;
                            data[$scope.fieldGrid[count][15]] = dataGroup.CostDriver;
                            data[$scope.fieldGrid[count][16]] = dataGroup.CostStation;
                            data[$scope.fieldGrid[count][17]] = dataGroup.IncomeServiceQuantity;
                            data[$scope.fieldGrid[count][18]] = dataGroup.IncomeServiceUnitPrice;
                            data[$scope.fieldGrid[count][19]] = dataGroup.IncomeService;
                            data[$scope.fieldGrid[count][20]] = dataGroup.CostServiceQuantity;
                            data[$scope.fieldGrid[count][21]] = dataGroup.CostServiceUnitPrice;
                            data[$scope.fieldGrid[count][22]] = dataGroup.CostService;
                        } else {
                            for (var i = 0 ; i < $scope.fieldGrid[count].length ; i++) {
                                data[$scope.fieldGrid[count][i]] = '';
                            }
                        }
                        count++;

                    });
                }
            }
            dataGrid.push(data);
        })
        $scope.rep_grid.dataSource.data(dataGrid);
        $scope.dtpfilter();
        $rootScope.IsLoading = false;
    }

    //#region excel
    $scope.Excel_Click = function ($event) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.REPCOPL_gridOptions.dataSource);

        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPCOPL_DetailColumn.URL.Template,
                    data: { itemfile: file, lstid: $scope.Item.lstCustomerID, lstgroupid: $scope.Item.lstGroupID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
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
            ListView: views.REPCOPL,
            event: $event,
            current: $state.current
        });
    };
    //#endregion

    //#region Setting Report
    $scope.SettingItem = { ID: 0 };
    $scope.SettingHasChoose = false;
    $scope.settingReport_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: "row",
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,settingReport_Grid,settingReport_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,settingReport_Grid,settingReport_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="settingReport_GridEdit_Click($event,SettingReport_win,dataItem,Setting_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="SettingReport_ActionClick($event,dataItem)" class="k-button"><i class="fa fa-download"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Name', title: 'Tên thiết lập', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: 'Ngày tạo', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(CreateDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } },
            },
            {
                field: 'FileName', title: 'Tên File', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }


    $scope.settingReport_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.SettingHasChoose = hasChoose;
    }
    $scope.SettingReport = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPCOPL_DetailColumn.URL.SettingList,
            data: { functionID: $rootScope.FunctionItem.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                if (Common.HasValue(res)) {
                    angular.forEach(res, function (value, key) {
                        value.IsChoose = false;
                    });
                }
                $scope.settingReport_GridOptions.dataSource.data(res);
                win.center().open();
                $scope.settingReport_Grid.resize();
            }
        });
    }


    $scope.SettingReport_TabIndex = 0;
    $scope.SettingReport_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.SettingReport_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }
    $scope.SettingReport_AddClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadSettingItem(win, null, vform);
    }

    $scope.settingReport_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadSettingItem(win, data, vform);
    }

    $scope.LoadSettingItem = function (win, data, vform) {
        if (data != null) {
            $scope.SettingItem = data;
            if (Common.HasValue(data.ListCustomer)) {
                angular.forEach(data.ListCustomer, function (value, key) {
                    value.IsChoose = false;
                });
                $scope.SettingReport_Customer_Grid.dataSource.data(data.ListCustomer);
            } else {
                $scope.SettingReport_Customer_Grid.dataSource.data([]);
            }
            if (Common.HasValue(data.ListProvince)) {
                angular.forEach(data.ListProvince, function (value, key) {
                    value.IsChoose = false;
                });
                $scope.SettingReport_Province_Grid.dataSource.data(data.ListProvince);
            } else {
                $scope.SettingReport_Province_Grid.dataSource.data([]);
            }
            if (Common.HasValue(data.ListServiceOfOrder)) {
                angular.forEach(data.ListServiceOfOrder, function (value, key) {
                    value.IsChoose = false;
                });
                $scope.SettingReport_ServiceOfOrder_Grid.dataSource.data(data.ListServiceOfOrder);
            } else {
                $scope.SettingReport_ServiceOfOrder_Grid.dataSource.data([]);
            }
        } else {
            $scope.SettingItem = { ID: 0 };
            $scope.SettingItem.TypeExport = 1;
            $scope.SettingItem.TypeDateRange = 1;
            $scope.SettingReport_Tab.select(0);
        }
        
        vform({ clear: true });
        win.center().open();
    }

    $scope.SettingReport_AddFileClick = function ($event) {
        $event.preventDefault();
        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                if (file != null) {
                    $scope.SettingItem.FileID = file.ID;
                    $scope.SettingItem.FileName = file.FileName;
                    $scope.SettingItem.FilePath = file.FilePath;
                }
            }
        });
    };

    $scope.SettingReport_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            var error = false;
            if (!Common.HasValue($scope.SettingItem.FileID)) {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Chưa chọn file template',
                    Close: null,
                    Ok: null
                })
                error = true;
            }
            if (!error) {
                $rootScope.IsLoading = true;
                $scope.SettingItem.ReferID = $rootScope.FunctionItem.ID;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPCOPL_DetailColumn.URL.SettingSave,
                    data: { item: $scope.SettingItem },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })

                        Common.Services.Call($http, {
                            url: Common.Services.url.REP,
                            method: _REPCOPL_DetailColumn.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.IsLoading = false;
                                if (Common.HasValue(res)) {
                                    angular.forEach(res, function (value, key) {
                                        value.IsChoose = false;
                                    });
                                }
                                $scope.settingReport_GridOptions.dataSource.data(res);
                            }
                        });
                        win.close();
                    }
                });
            }
        }
    }

    $scope.SettingReport_Destroy_Click = function ($event, win) {
        $event.preventDefault();
        if (Common.HasValue($scope.SettingItem)) {
            var datasend = [];
            datasend.push($scope.SettingItem.ID);
            $scope.SettingDelete(win, datasend);
        }
    }
    $scope.settingReport_GridDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $scope.SettingDelete(null, datasend);
        }
    }

    $scope.SettingDelete = function (win, datasend) {
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPCOPL_DetailColumn.URL.SettingDelete,
                    data: { lst: datasend },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })

                        Common.Services.Call($http, {
                            url: Common.Services.url.REP,
                            method: _REPCOPL_DetailColumn.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.IsLoading = false;
                                if (Common.HasValue(res)) {
                                    angular.forEach(res, function (value, key) {
                                        value.IsChoose = false;
                                    });
                                }
                                $scope.settingReport_GridOptions.dataSource.data(res);
                            }
                        });
                        if (Common.HasValue(win))
                            win.close();
                    }
                });
            }
        })
    }
    $scope.cboTypeExport_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Lợi nhuận chuyến chi tiết' },
            { ID: 2, ValueName: 'Lợi nhuận chuyến theo cột' },
            { ID: 3, ValueName: 'Lợi nhuận đơn hàng chi tiết' },
            { ID: 4, ValueName: 'Lợi nhuận đơn hàng theo cột' },
        ],
        change: function (e) { }
    }

    $scope.cboTypeDateRange_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Theo tuần' },
            { ID: 2, ValueName: 'Theo tháng' },
            { ID: 3, ValueName: 'Theo ngày đã chọn' },
        ],
        change: function (e) {
        }
    }

    $scope.SettingReport_ActionClick = function ($event, data) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.rep_grid.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPCOPL_DetailColumn.URL.SettingAction,
            data: { item: data, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        });
    }
    //#region Customer
    $scope.CustomerHasChoose = false;
    $scope.SettingReport_Customer_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Customer_Grid,customer_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Customer_Grid,customer_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'CustomerCode', title: 'Mã khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Tên khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.customer_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerHasChoose = hasChoose;
    }

    $scope.customer_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.customerNotIn_GridOptions.dataSource.read();
    }

    $scope.customerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPCOPL_DetailColumn.URL.SettingCusNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,customerNotIn_Grid,customerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,customerNotIn_Grid,customerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.customerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerNotInHasChoose = hasChoose;
    }

    $scope.customerNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPCOPL_DetailColumn.URL.SettingCusNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    } else {
                        $scope.SettingReport_Customer_Grid.dataSource.data([]);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPCOPL_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.customer_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.CustomerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPCOPL_DetailColumn.URL.SettingCusDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    } else {
                        $scope.SettingReport_Customer_Grid.dataSource.data([]);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPCOPL_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    //#endregion


    //#region Province
    $scope.ProvinceHasChoose = false;
    $scope.SettingReport_Province_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ProvinceCode: { type: 'string' },
                    ProvinceName: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Province_Grid,Province_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Province_Grid,Province_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'ProvinceCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Province_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceHasChoose = hasChoose;
    }

    $scope.Province_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.ProvinceNotIn_GridOptions.dataSource.read();
    }

    $scope.ProvinceNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPCOPL_DetailColumn.URL.SettingProvinceInList,
            readparam: function () { return { lstProvince: $scope.SettingItem.ListProvince } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ProvinceNotIn_Grid,ProvinceNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ProvinceNotIn_Grid,ProvinceNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.ProvinceNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceNotInHasChoose = hasChoose;
    }

    $scope.ProvinNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPCOPL_DetailColumn.URL.SettingProvinceNotInSave,
                data: { item: $scope.SettingItem, lstProvinceID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListProvince)) {
                        angular.forEach(res.ListProvince, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Province_GridOptions.dataSource.data(res.ListProvince);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPCOPL_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.Province_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ProvinceID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPCOPL_DetailColumn.URL.SettingProvinceDeleteList,
                data: { item: $scope.SettingItem, lstProvinceID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListProvince)) {
                        angular.forEach(res.ListProvince, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Province_GridOptions.dataSource.data(res.ListProvince);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPCOPL_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion


    //#region ServiceOfOrder
    $scope.ServiceOfOrderHasChoose = false;
    $scope.SettingReport_ServiceOfOrder_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_ServiceOfOrder_Grid,ServiceOfOrder_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_ServiceOfOrder_Grid,ServiceOfOrder_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'ServiceOfOrderCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', title: 'Tên dịch vụ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.ServiceOfOrder_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ServiceOfOrderHasChoose = hasChoose;
    }

    $scope.ServiceOfOrder_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.ServiceOfOrderNotIn_GridOptions.dataSource.read();
    }

    $scope.ServiceOfOrderNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPCOPL_DetailColumn.URL.SettingServiceOfOrderInList,
            readparam: function () { return { lstServiceOfOrder: $scope.SettingItem.ListServiceOfOrder } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ServiceOfOrderNotIn_Grid,ServiceOfOrderNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ServiceOfOrderNotIn_Grid,ServiceOfOrderNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: 'Tên dịch vụ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.ServiceOfOrderNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ServiceOfOrderNotInHasChoose = hasChoose;
    }

    $scope.ServiceOfOrderNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPCOPL_DetailColumn.URL.SettingServiceOfOrderNotInSave,
                data: { item: $scope.SettingItem, lstServiceOfOrderID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListServiceOfOrder)) {
                        angular.forEach(res.ListServiceOfOrder, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_ServiceOfOrder_GridOptions.dataSource.data(res.ListServiceOfOrder);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPCOPL_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.ServiceOfOrder_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ServiceOfOrderID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPCOPL_DetailColumn.URL.SettingServiceOfOrderDeleteList,
                data: { item: $scope.SettingItem, lstServiceOfOrderID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListServiceOfOrder)) {
                        angular.forEach(res.ListServiceOfOrder, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_ServiceOfOrder_GridOptions.dataSource.data(res.ListServiceOfOrder);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPCOPL_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion
    $scope.Excel_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPCOPL_DetailColumn.URL.Excel_Export,
            data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }
}]);