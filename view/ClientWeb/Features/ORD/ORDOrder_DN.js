/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _ORDOrder_DN = {
    URL: {
        Read: "ORDOrder_DN_List",
        SO_DownLoad: "ORDOrder_DN_Rest_SO_Download",
        DN_DownLoad: "ORDOrder_DN_Rest_DN_Download",
        All_DownLoad: "ORDOrder_DN_Rest_All_Download",
        Excel_DownLoad: "ORDOrder_DN_DownLoadExcel",
    }
}

//#endregion

angular.module('myapp').controller('ORDOrder_DNCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_DNCtrl');
    $rootScope.IsLoading = false;
    
    $scope.OrderID = -1;
    $scope.CustomerID = -1;
    $scope.RestOptions = {
        SO: {
            IsAll: true,
            DateFrom: null,
            DateTo: null
        },
        DN: {
            IsAll: true,
            DateFrom: null,
            DateTo: null
        },
        All: {
            IsAll: true,
            DateFrom: null,
            DateTo: null
        }
    }

    //#region View

    $scope.orderDN_grid = null;
    $scope.orderDN_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_DN.URL.Read,
            readparam: function () { return { CusID: $scope.CustomerID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsComment: { type: 'bool' },
                    IsComplete: { type: 'number' },
                    Code: { type: 'string' },
                    Note: { type: 'string' },
                    IsInvoice: { type: 'bool' }
                }
            }
        }),
        height: '100%', groupable: true, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        toolbar: kendo.template($('#orderDN_gridToolbar').html()),
        dataBound: function(){
            var grid = this;
            Common.Data.Each(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (item != null) {
                    switch (item.StatusID) {
                        case 3:
                            $(tr).find('span.lblStatus').addClass('txt-wait');
                            break;
                        case 2:
                            $(tr).find('span.lblStatus').addClass('txt-tran');
                            break;
                        case 1:
                            $(tr).find('span.lblStatus').addClass('txt-comp');
                            break;
                        default:
                            break;
                    }
                }
            })
        },
        columns: [
            {
                field: 'StatusID', width: 140, title: '{{RS.ORDOrder_DN.StatusID}}',
                template: "<span class='lblStatus'>#=StatusID==1?'Đã hoàn tất': StatusID == 2?'Đang vận chuyển':'Đang lập chuyến'#</span>",
                filterable: false, groupable: false, sortorder: 0, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'IsComplete', width: 100, title: '{{RS.ORDOrder_DN.IsComplete}}',
                template: "<div class='lblDFromLeave'>#=DateToLeave==null?'&nbsp':Common.Date.FromJsonDDMMHM(DateToLeave)# #=Note==null?' ':Note#</div>",
                filterable: false, groupable: false, sortable: false, sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'IsInvoice', width: 70, title: '{{RS.ORDOrder_DN.IsInvoice}}', template: "<input type='checkbox' #= IsInvoice ? 'checked=checked' : '' # disabled ></input>",
                filterable: false, attributes: { style: 'text-align: center;' }, groupHeaderTemplate: 'Chứng từ: #=value ? "Đã nhận" : "Chưa nhận"#',
                sortable: false, sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SOCode', width: 100, title: '{{RS.ORDGroupProduct.SOCode}}', template: "<a class='action-text' ng-click='ORDDN_ViewORD_Detail_Click($event, dataItem)' href='#=URL#'>#=SOCode==null?\"\":SOCode#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DNCode', width: 100, title: '{{RS.OPSDITOGroupProduct.DNCode}}', template: "<a class='action-text' ng-click='ORDDN_ViewORD_Detail_Click($event, dataItem)' href='#=URL#'>#=DNCode==null?\"\":DNCode#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: 100, title: '{{RS.ORDOrder.Code}}', template: "<a class='action-text' ng-click='ORDDN_ViewORD_Detail_Click($event, dataItem)' href='#=URL#'>#=OrderCode==null?\"\":OrderCode#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToCode', width: 100, title: '{{RS.ORDOrder_DN.LocationToCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToName', width: 100, title: '{{RS.ORDOrder_DN.LocationToName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToAddress', width: 200, title: '{{RS.ORDOrder_DN.LocationToAddress}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToProvince', width: 120, title: '{{RS.ORDOrder_DN.LocationToProvince}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToDistrict', width: 120, title: '{{RS.ORDOrder_DN.LocationToDistrict}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'EconomicZone', width: 100, title: '{{RS.CATLocation.EconomicZone}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromCode', width: 100, title: '{{RS.ORDOrder_DN.LocationFromCode}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: 60, title: '{{RS.OPSDITOGroupProduct.Quantity}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'QuantityTransfer', width: 80, title: '{{RS.OPSDITOGroupProduct.QuantityTransfer}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RegNo', width: 100, title: '{{RS.CATVehicle.RegNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DriverName', width: 100, title: '{{RS.OPSDITOMaster.DriverName1}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DriverTel', width: 100, title: '{{RS.OPSDITOMaster.DriverTel1}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'MasterCode', width: 100, title: '{{RS.OPSDITOMaster.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDate', width: 150, title: '{{RS.ORDOrder.RequestDate}}', template: "#=RequestDate==null?' ':Common.Date.ParseFromString(RequestDate)#",
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
                sortable: false, sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETARequest', width: 150, title: '{{RS.ORDGroupProduct.ETARequest}}', template: "#=ETARequest==null?' ':Common.Date.ParseFromString(ETARequest)#",
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
                sortable: false, sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateFromCome', width: 150, title: '{{RS.OPSDITOGroupProduct.DateFromCome}}', template: "#=DateFromCome==null?' ':Common.Date.ParseFromString(DateFromCome)#",
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
                sortable: false, sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateFromLoadStart', width: 150, title: '{{RS.OPSDITOGroupProduct.DateFromLoadStart}}', template: "#=DateFromLoadStart==null?' ':Common.Date.ParseFromString(DateFromLoadStart)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFromLoadStart' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFromLoadStart' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
                sortable: false, sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateFromLoadEnd', width: 150, title: '{{RS.OPSDITOGroupProduct.DateFromLoadEnd}}', template: "#=DateFromLoadEnd==null?' ':Common.Date.ParseFromString(DateFromLoadEnd)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFromLoadEnd' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFromLoadEnd' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
                sortable: false, sortorder: 23, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateToCome', width: 150, title: '{{RS.OPSDITOGroupProduct.DateToCome}}', template: "#=DateToCome==null?' ':Common.Date.ParseFromString(DateToCome)#",
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
                sortable: false, sortorder: 24, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateFromLeave', width: 150, title: '{{RS.OPSDITOGroupProduct.DateFromLeave}}', template: "#=DateFromLeave==null?' ':Common.Date.ParseFromString(DateFromLeave)#",
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
                sortable: false, sortorder: 25, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateToLoadStart', width: 150, title: '{{RS.OPSDITOGroupProduct.DateToLoadStart}}', template: "#=DateToLoadStart==null?' ':Common.Date.ParseFromString(DateToLoadStart)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateToLoadStart' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToLoadStart' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
                sortable: false, sortorder: 26, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DateToLoadEnd', width: 150, title: '{{RS.OPSDITOGroupProduct.DateToLoadEnd}}', template: "#=DateToLoadEnd==null?' ':Common.Date.ParseFromString(DateToLoadEnd)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' field='RequestDate' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' field='RequestDate' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
                sortable: false, sortorder: 27, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'KPIOPS', width: 150, title: '{{RS.ORDOrder_DN.KPIOPS}}', template: "#=KPIOPS==null?' ':Common.Date.ParseFromString(KPIOPS)#",
                filterable: false, sortable: false, sortorder: 28, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'KPIPOD', width: 150, title: '{{RS.ORDOrder_DN.KPIPOD}}', template: "#=KPIPOD==null?' ':Common.Date.ParseFromString(KPIPOD)#",
                filterable: false, sortable: false, sortorder: 29, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note2', width: 100, title: '{{RS.OPSDITOGroupProduct.Note2}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 30, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note1', width: 100, title: '{{RS.OPSDITOGroupProduct.Note1}}',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortable: false, sortorder: 31, configurable: true, isfunctionalHidden: false
            },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ]
    };

    $timeout(function () {
        $('.dtp-filter-from').kendoDatePicker({
            format: Common.Date.Format.DDMM,
            change: function (e) {
                try {
                    var element = this.wrapper.parent();
                    var field = e.sender.element.data('field');
                    var dtp_To = $(element).find('.dtp-filter-to[data-field=' + field + ']').data('kendoDatePicker');
                    var f = this.value();
                    var t = dtp_To.value();
                    if (Common.HasValue(t)) {
                        try {
                            t = t.addDays(1);
                        }
                        catch (e) {

                        }
                    }

                    var f_filter = { field: field, operator: "gte", value: f };
                    var t_filter = { field: field, operator: "lte", value: t };

                    if (Common.HasValue($scope.orderDN_grid.dataSource.filter())) {
                        var filters = $scope.orderDN_grid.dataSource.filter().filters;
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
                        var filters = [];
                        if (Common.HasValue(f))
                            filters.push(f_filter);
                        if (Common.HasValue(t))
                            filters.push(t_filter);

                        $scope.orderDN_grid.dataSource.filter(filters);
                    }
                    $scope.orderDN_grid.dataSource.read();
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })

        $('.dtp-filter-to').kendoDatePicker({
            format: Common.Date.Format.DDMM,
            change: function (e) {
                try {
                    var element = this.wrapper.parent();
                    var field = e.sender.element.data('field');
                    var dtp_From = $(element).find('.dtp-filter-from[data-field=' + field + ']').data('kendoDatePicker');
                    var f = dtp_From.value();
                    var t = this.value()
                    if (Common.HasValue(t)) {
                        try {
                            t = t.addDays(1);
                        }
                        catch (e) {

                        }
                    }

                    var f_filter = { field: field, operator: "gte", value: f };
                    var t_filter = { field: field, operator: "lte", value: t };

                    if (Common.HasValue($scope.orderDN_grid.dataSource.filter())) {
                        var filters = $scope.orderDN_grid.dataSource.filter().filters;
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
                        var filters = [];
                        if (Common.HasValue(f))
                            filters.push(f_filter);
                        if (Common.HasValue(t))
                            filters.push(t_filter);

                        $scope.orderDN_grid.dataSource.filter(filters);
                    }
                    $scope.orderDN_grid.dataSource.read();
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })
    }, 500)

    $scope.$watch('orderDN_grid', function (newValue, oldValue) {
        if (newValue != null)
            Common.Controls.Grid.ReorderColumns({ Grid: newValue, CookieName: 'OrderORD_DN_Grid' });
    });

    $scope.orderDN_cus_cboOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID', minLength: 3,
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
            var cbo = this;
            var obj = cbo.dataItem(cbo.select());
            if (Common.HasValue(obj)) {
                $scope.CustomerID = obj.ID;
            } else {
                $scope.CustomerID = -1;
            }
            $scope.orderDN_gridOptions.dataSource.read();
            setTimeout(function () {
                
            }, 10)
        }
    }
    $timeout(function () {
        Common.ALL.Get($http, {
            url: Common.ALL.URL.Customer,
            success: function (res) {
                $scope.orderDN_cus_cboOptions.dataSource.data(res);
                var cbb = $scope.orderDN_cus_cbo;
                cbb.select(0);
                var obj = cbb.dataItem(cbb.select());
                if (Common.HasValue(obj)) {
                    $scope.CustomerID = obj.ID;
                } else {
                    $scope.CustomerID = -1;
                }
                $scope.orderDN_gridOptions.dataSource.read();
            }
        });
    }, 100)

    $scope.TabIndex = 1;
    $scope.rest_win_tabstripOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        },
        select: function (e) {
            var index = angular.element(e.item).data('tabindex');
            $scope.TabIndex = index;
        }
    }

    //#endregion
    
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    
    $scope.ORDDN_Comment_Click = function ($event, grid) {
        $event.preventDefault();
        
        var sel = grid.select();
        var referid = -1;
        var title = null;
        if (Common.HasValue(sel) && sel.length > 0) {
            var item = grid.dataItem(sel[0]);
            title = '<b>ĐN: ' + item.DNCode + '</b>: ĐH ' + item.OrderCode + ' - ' + item.CustomerName;
            referid = item.id;
        }
        $rootScope.Comment({ Type: Common.Comment.ORDDN, ReferID: referid, Title: title });
    };

    $scope.ORDDN_ViewORD_Detail_Click = function ($event, item) {
        $event.preventDefault();
        
        var view = '';
        var flag = false;
        switch (item.TypeOfView) {
            case 0:
                break;
            case 1:
                flag = true;
                view = "main.ORDOrder.FCLIMEX"
                break;
            case 2:
                flag = true;
                view = "main.ORDOrder.FCLLO"
                break;
            case 3:
                flag = true;
                view = "main.ORDOrder.FTLLO"
                break;
            case 4:
                flag = true;
                view = "main.ORDOrder.LTLLO"
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            default:
                break;
        }
        if (flag) {
            $state.go(view, {
                OrderID: item.OrderID,
                CustomerID: item.CustomerID,
                ServiceID: item.ServiceOfOrderID,
                TransportID: item.TransportModeID,
                ContractID: item.ContractID > 0 ? item.ContractID : -1
            });
        }
    }

    $scope.ORDDN_Add_Click = function ($event) {
        $event.preventDefault();
        
        $state.go('main.ORDOrder.New');
    }

    $scope.ORDDN_ViewORD_Click = function ($event) {
        $event.preventDefault();
        $state.go('main.ORDOrder.Index');
    }

    $scope.ORDDN_Excel_Click = function ($event, grid) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.orderDN_grid.dataSource);
        request.pageSize = 0;

        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_DN.URL.Excel_DownLoad,
            data: {
                CusID: $scope.CustomerID,
                request: request,
            },
            success: function (res) {
                $rootScope.DownloadFile(res);
            }
        });
    }

    $scope.ORDDN_Rest_Click = function ($event, grid, win) {
        $event.preventDefault();
        
        win.center().open();
    }

    $scope.Rest_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.ORDDN_Rest_SO_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_DN.URL.SO_DownLoad,
            data: {
                CusID: $scope.CustomerID, IsAll: $scope.RestOptions.SO.IsAll,
                DateFrom: $scope.RestOptions.SO.DateFrom, DateTo: $scope.RestOptions.SO.DateTo
            },
            success: function (res) {
                $rootScope.DownloadFile(res);
                win.close();
            }
        });
    }

    $scope.ORDDN_Rest_DN_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_DN.URL.DN_DownLoad,
            data: {
                CusID: $scope.CustomerID, IsAll: $scope.RestOptions.DN.IsAll,
                DateFrom: $scope.RestOptions.DN.DateFrom, DateTo: $scope.RestOptions.DN.DateTo
            },
            success: function (res) {
                $rootScope.DownloadFile(res);
                win.close();
            }
        });
    }

    $scope.ORDDN_Rest_All_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_DN.URL.All_DownLoad,
            data: {
                CusID: $scope.CustomerID, IsAll: $scope.RestOptions.All.IsAll,
                DateFrom: $scope.RestOptions.All.DateFrom, DateTo: $scope.RestOptions.All.DateTo
            },
            success: function (res) {
                $rootScope.DownloadFile(res);
                win.close();
            }
        });
    }
    //#endregion
}]);