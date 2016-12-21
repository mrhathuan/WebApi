
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIPivotOrder = {
    URL: {
        List: 'REPDIPivotOrder_List',
        Read: 'REPDIPivotOrder_Read',
        GetTemplate: 'REPDIPivotOrder_GetTemplate',
        SaveTemplate: 'REPDIPivotOrder_SaveTemplate',
        DeleteTemplate: 'REPDIPivotOrder_DeleteTemplate',
        Read_Customer: 'REP_Customer_Read',
    }
}

var collapsed = {
    columns: [],
    rows: []
};

myapp.controller('REPDIPivotOrder_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIPivotOrder_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.chart_theme = 'Bootstrap';
    $scope.chart_type = 'column';
    $scope.Template = {};
    $scope.Template.Name = "";
    $scope.HasChooseTemplate = false;

    $scope.InitDataChart = function () {
        _REPDIChart.Data._dataChart = [];
        _REPDIChart.Data._dataChart.push({ Code: 'document', Name: 'Chứng từ' });
        _REPDIChart.Data._dataChart.push({ Code: 'finance', Name: 'Tài chính' });
        _REPDIChart.Data._dataChart.push({ Code: 'analysis', Name: 'Phân tích' });
        $scope.cboChart_Options.dataSource.data(_REPDIChart.Data._dataChart);
    };

    $scope.pivot_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            if (e.sender.select().index() == 0) {
                $scope.REPDIPivot_ShowChart();
            }
        }
    }

    $scope.REPDIPivot_Splitter_Options = {
        panes: [
            { collapsible: true, size: '350px' },
            { collapsible: true }
        ]
    };

    $scope.ResetSettings = function () {
        $scope.chart_theme = 'Bootstrap';
        angular.extend($scope.Item, {
            chart_type: 'finance',
            chart_typeDisplay: 'Tài chính',
            fchart_type: "Cước thu",
            fchart_index: 0,
            Theme: 'Bootstrap'
        });
    }

    $scope.Item = {
        lstCustomerID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        chart_type:'finance',
        chart_typeDisplay: 'Tài chính',
        fchart_type: "Cước thu",
        fchart_index: 0,
        PieChart: { category: "VendorName", measure: "Tấn nhận" },
        Theme: 'Bootstrap'
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        itemTemplate: '<span>[#= Code #] #= CustomerName #</span>',
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
        change: function (e) {
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
        }
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

    $scope.cboChart_Options = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Name', dataValueField: 'Code',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'Code',
                fields: {
                    Code: { type: 'string' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            // change chart type here
            $scope.Item.chart_type = this.value();
            switch ($scope.Item.chart_type)
            {
                case 'document':
                    $scope.filters = $scope.dfilters;
                    _REPDIChart.showChartWithFilter($scope);
                    break;
                case 'finance':
                    $scope.filters = $scope.ffilters;
                    $scope.showFinanceChart();
                    break;
                case 'analysis':
                    $scope.showPieChart();
                    break;
            }
        }
    };

    $scope.cboDashBoard_ThemeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0,
        dataSource: Common.DataSource.Local({
            data: [
                {
                    ID: 'Default', Text: 'Default'
                },
                {
                    ID: 'Black', Text: 'Black'
                },
                {
                    ID: 'Blueopal', Text: 'Blueopal'
                },
                {
                    ID: 'Bootstrap', Text: 'Bootstrap'
                },
                {
                    ID: 'Highcontrast', Text: 'Highcontrast'
                },
                {
                    ID: 'Metro', Text: 'Metro'
                },
                {
                    ID: 'Metroblack', Text: 'Metroblack'
                },
                {
                    ID: 'Moonlight', Text: 'Moonlight'
                },
                {
                    ID: 'Silver', Text: 'Silver'
                },
                {
                    ID: 'Uniform', Text: 'Uniform'
                }
            ],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'string' },
                    Text: { type: 'string' },
                }
            }
        })
    }

    $scope.OnDashBoard_ThemeChange = function ($event) {
        var chart = $("#repdipivot_pivot_chart").data("kendoChart");
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            chart.setOptions({ theme: obj.ID });
            $scope.chart_theme = obj.ID;
            chart.refresh();
        }
    }

    $scope.cboFChart_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0,
        dataSource: Common.DataSource.Local({
            data: [
                {
                    ID: 'income', Text: 'Cước thu'
                },
                {
                    ID: 'cost', Text: 'Cước chi'
                }
            ],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'string' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            // change chart type here
            $scope.Item.fchart_type = this.value();
            $scope.Item.fchart_index = e.sender.select();
            $scope.showFinanceChart()
        }
    }

    $scope.cboPieChartCat_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'string' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            // change chart type here
            $scope.Item.PieChart.category = this.value();
            $scope.showPieChart();
        }
    }
    $scope.cboPieChartMeasure_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'string' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            // change chart type here
            $scope.Item.PieChart.measure = this.value();
            $scope.showPieChart();
        }
    }
    
    

    $scope.InitDataChart();

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var strCookie = Common.Cookie.Get(_REPDIChart.Data.CookieSearch);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Item.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Item.DateTo = Common.Date.FromJson(objCookie.DateTo);
            } catch (e) { }
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPDIPivotOrder.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
            $scope.Init_LoadCookie();
        }
    });

    $scope.REPDIPivot_PivotExcel_Click = function ($event, cbo) {
        $event.preventDefault();

        $timeout(function () {
            $('#repdipivot_pivot_grid').data('kendoPivotGrid').saveAsExcel();
        }, 1);
    };

    $scope.REPDIPivot_Search = function ($event, cbo, _columns, _rows, _measures) {
        $event.preventDefault();
        Common.Cookie.Set(_REPDIChart.Data.CookieSearch, JSON.stringify($scope.Item));
        //var lstid = $scope.Item.lstCustomerID;
        
        if (!Common.HasValue(_columns))
            _columns = [];

        if (!Common.HasValue(_rows))
            _rows = [{ name: "VendorShortName", expand: true }];

        if (!Common.HasValue(_measures))
            _measures = ["Tổng thu", "Tổng chi"];

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPivotOrder.URL.Read,
            data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, lstgroupid: [] },
            success: function (res) {
                debugger
                $rootScope.IsLoading = false;

                angular.forEach(res, function (v, i) {
                    v.RequestDate = Common.Date.FromJsonDDMMYY(v.RequestDate);
                    v.DateConfig = Common.Date.FromJsonDDMMYY(v.DateConfig);
                    v.OrderDateConfig = Common.Date.FromJsonDDMMYY(v.OrderDateConfig);
                    v.OPSGroupProductDateConfig = Common.Date.FromJsonDDMMYY(v.OPSGroupProductDateConfig);
                    v.OPSDateConfig = Common.Date.FromJsonDDMMYY(v.OPSDateConfig);
                    v.OrderCreatedDate = Common.Date.FromJsonDDMMYY(v.OrderCreatedDate);
                    v.ETD = Common.Date.FromJsonDDMMYY(v.ETD);
                });

                _REPDIChart.Data.DataSourceConfig = {
                    data: res,
                    schema: {
                        model: {
                            fields: {
                                OrderCode: { type: "string" },
                                DateConfig: { type: "string" },
                                RequestDate: { type: "string" },
                                SOCode: { type: "string" },
                                CustomerCode: { type: "string" },
                                CustomerShortName: { type: "string" },
                                OrderCreatedBy: { type: "string" },
                                OrderCreatedDate: { type: "string" },
                                ETD: { type: "string" },

                                StockCode: { type: "string" },
                                StockName: { type: "string" },
                                StockAddress: { type: "string" },

                                LocationToProvince: { type: "string" },
                                LocationToDistrict: { type: "string" },
                                AreaToCodeCredit: { type: "string" },
                                AreaToNameCredit: { type: "string" },

                                PartnerCode: { type: "string" },
                                PartnerName: { type: "string" },
                                PartnerCodeName: { type: "string" },
                                Address: { type: "string" },

                                OrderRouting: { type: "string" },
                                OrderGroupProductRouting: { type: "string" },
                                OPSRouting: { type: "string" },
                                OPSGroupProductRouting: { type: "string" },

                                GroupOfProductCode: { type: "string" },
                                GroupOfProductName: { type: "string" },
                                GroupOfProductVendorCode: { type: "string" },
                                GroupOfProductVendorName: { type: "string" },

                                VehicleCode: { type: "string" },
                                VendorCode: { type: "string" },
                                VendorName: { type: "string" },
                                VendorShortName: { type: "string" },

                                DNCode: { type: "string" },
                                TonTranfer: { type: "number" },
                                CBMTranfer: { type: "number" },
                                QuantityTranfer: { type: "number" },
                                TonBBGN: { type: "number" },
                                CBMBBGN: { type: "number" },
                                QuantityBBGN: { type: "number" },
                                TonReturn: { type: "number" },
                                CBMReturn: { type: "number" },
                                QuantityReturn: { type: "number" },

                                IncomeQuantity: { type: "number" },
                                IncomeUnitPrice: { type: "number" },
                                Income: { type: "number" },

                                IncomeUnLoadQuantity: { type: "number" },
                                IncomeUnLoadUnitPrice: { type: "number" },
                                IncomeUnLoad: { type: "number" },

                                IncomeLoadQuantity: { type: "number" },
                                IncomeLoadUnitPrice: { type: "number" },
                                IncomeLoad: { type: "number" },

                                IncomeReturnUnitPrice: { type: "number" },
                                IncomeReturnQuantity: { type: "number" },
                                IncomeReturn: { type: "number" },

                                IncomeReturnUnLoadUnitPrice: { type: "number" },
                                IncomeReturnUnLoadQuantity: { type: "number" },
                                IncomeReturnUnLoad: { type: "number" },

                                IncomeReturnLoadUnitPrice: { type: "number" },
                                IncomeReturnLoadQuantity: { type: "number" },
                                IncomeReturnLoad: { type: "number" },

                                IncomeExUnitPrice: { type: "number" },
                                IncomeExQuantity: { type: "number" },
                                IncomeEx: { type: "number" },
                                IncomeExNote: { type: "string" },
                                IncomeExCostName: { type: "string" },

                                CostUnitPrice: { type: "number" },
                                CostQuantity: { type: "number" },
                                Cost: { type: "number" },
                                CostExNote: { type: "string" },
                                CostExCostName: { type: "string" },

                                CostUnLoadUnitPrice: { type: "number" },
                                CostUnLoadQuantity: { type: "number" },
                                CostUnLoad: { type: "number" },

                                CostLoadUnitPrice: { type: "number" },
                                CostLoadQuantity: { type: "number" },
                                CostLoad: { type: "number" },

                                CostLoadUnitPrice: { type: "number" },
                                CostLoadQuantity: { type: "number" },
                                CostLoad: { type: "number" },

                                CostReturnUnitPrice: { type: "number" },
                                CostReturnQuantity: { type: "number" },
                                CostReturn: { type: "number" },

                                CostReturnUnLoadUnitPrice: { type: "number" },
                                CostReturnUnLoadQuantity: { type: "number" },
                                CostReturnUnLoad: { type: "number" },

                                CostReturnLoadUnitPrice: { type: "number" },
                                CostReturnLoadQuantity: { type: "number" },
                                CostReturnLoad: { type: "number" },

                                CostExUnitPrice: { type: "number" },
                                CostExQuantity: { type: "number" },
                                CostEx: { type: "number" },

                                TotalIncome: { type: "number" },
                                TotalCost: { type: "number" },
                                TotalPL: { type: "number" },
                                PercentPLIncome: { type: "number" },

                                LocationToNote: { type: "string" },
                                LocationToNote1: { type: "string" },

                                RegWeight: { type: "number" },
                                MinWeight: { type: "number" },
                                MaxWeightCal: { type: "number" },

                                RegCapacity: { type: "number" },
                                MinCapacity: { type: "number" },
                                MaxCapacity: { type: "number" },
                            }
                        },
                        cube: {
                            dimensions: {
                                OrderCode: { caption: "Đơn hàng" },
                                DNCode: { caption: "Số DN" },
                                RequestDate: { caption: "Ngày y/c v/c" },
                                ETD: { caption: "Ngày v/c" },
                                SOCode: { caption: "Số SO" },

                                StockCode: { caption: "Mã điểm lấy" },
                                StockName: { caption: "Tên điểm lấy" },
                                StockAddress: { caption: "Điểm lấy" },

                                PartnerCode: { caption: "Mã NPP" },
                                PartnerName: { caption: "Điểm giao" },
                                PartnerCodeName: { caption: "Mã + Tên phân phối" },
                                Address: { caption: "Điểm giao" },

                                RegWeight: { caption: "Trọng tải đăng kí của xe" },
                                MinWeight: { caption: "Trọng tải tối thiểu" },
                                MaxWeightCal: { caption: "Trọng tải tối đa" },

                                RegCapacity: { caption: "Số khối đăng kí của xe" },
                                MinCapacity: { caption: "Số khối tối thiểu" },
                                MaxCapacity: { caption: "Số khối tối đa" },

                                OrderGroupProductRouting: { caption: "Cung đường" },
                                GroupOfProductCode: { caption: "Mã nhóm hàng" },
                                GroupOfProductName: { caption: "Nhóm hàng" },
                                VehicleCode: { caption: "Số xe" },
                                VendorCode: { caption: "Mã nhà vận tải" },
                                VendorShortName: { caption: "Nhà vận tải" },
                                CustomerCode: { caption: "Mã khách hàng" },
                                CustomerShortName: { caption: "Khách hàng" },
                                OrderCreatedBy: { caption: "Người tạo đơn" },
                                OrderCreatedDate: { caption: "Ngày tạo đơn" },
                                LocationToNote: { caption: "Ghi chú điểm giao" },
                                LocationToNote1: { caption: "Ghi chú điểm giao 1" },
                                LocationToProvince: { caption: "Tỉnh thành điểm giao" },
                                LocationToDistrict: { caption: "Quận huyện điểm giao" },
                                AreaToCodeCredit: { caption: "Mã khu vực giao" },
                                AreaToNameCredit: { caption: "Tên khu vực giao" },
                            },
                            measures: {
                                "Tấn nhận": { field: "TonTranfer", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Khối nhận": { field: "CBMTranfer", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số lượng nhận": { field: "QuantityTranfer", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Tấn giao": { field: "TonBBGN", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Khối giao": { field: "CBMBBGN", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số lượng giao": { field: "QuantityBBGN", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá thu": { field: "IncomeUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số lượng thu": { field: "IncomeQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước thu": { field: "Income", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá b.x lên": { field: "IncomeLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L b.x lên": { field: "IncomeLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước b.x lên": { field: "IncomeLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá b.x xuống": { field: "IncomeUnLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L b.x xuống": { field: "IncomeUnLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước b.x xuống": { field: "IncomeUnLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá trả về": { field: "IncomeReturnUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L trả về": { field: "IncomeReturnQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước trả về": { field: "IncomeReturn", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá b.x lên trả về": { field: "IncomeReturnLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L b.x lên trả về": { field: "IncomeReturnLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước b.x lên trả về": { field: "IncomeReturnLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá b.x xuống trả về": { field: "IncomeReturnUnLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L b.x xuống trả về": { field: "IncomeReturnUnLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước b.x xuống trả về": { field: "IncomeReturnUnLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá phụ thu": { field: "IncomeExUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L phụ thu": { field: "IncomeExQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước phụ thu": { field: "IncomeEx", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá chi": { field: "CostUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số lượng chi": { field: "CostQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi": { field: "Cost", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá chi b.x lên": { field: "CostLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L chi b.x lên": { field: "CostLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi b.x lên": { field: "CostLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá chi b.x xuống": { field: "CostUnLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L chi b.x xuống": { field: "CostUnLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi b.x xuống": { field: "CostUnLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá chi trả về": { field: "CostReturnUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L chi trả về": { field: "CostReturnQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi trả về": { field: "CostReturn", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá chi b.x lên trả về": { field: "CostReturnLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L chi b.x lên trả về": { field: "CostReturnLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi b.x lên trả về": { field: "CostReturnLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá chi b.x xuống trả về": { field: "CostReturnUnLoadUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L chi b.x xuống trả về": { field: "CostReturnUnLoadQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi b.x xuống trả về": { field: "CostReturnUnLoad", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Giá chi phụ thu": { field: "CostExUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "S.L chi phụ thu": { field: "CostExQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi phụ thu": { field: "CostEx", format: "{0:#,##0.0}", aggregate: "sum" },

                                "Tổng thu": { field: "TotalIncome", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Tổng chi": { field: "TotalCost", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Lợi nhuận": { field: "TotalPL", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Lợi nhuận trên doanh thu": {
                                    field: "PercentPLIncome", format: "{0:#,##0.0} %",
                                    aggregate: function (value, state, context) {
                                        var dataItem = context.dataItem;
                                        state.TotalPL = (state.TotalPL || 0) + dataItem.TotalPL;
                                        state.TotalIncome = (state.TotalIncome || 0) + dataItem.TotalIncome;
                                    },
                                    result: function (state) {
                                        if (state.TotalIncome == 0) {
                                            if (state.TotalPL == 0)
                                                return 0;
                                            else
                                                return 100;
                                        }
                                        else
                                            return (state.TotalPL / state.TotalIncome) * 100;
                                    }
                                },
                            }
                        }
                    },
                    columns: _columns,
                    rows: _rows,
                    measures: _measures
                };

                //Options for pie chart
                $scope.cboPieChartCat_Options.dataSource.data(Object.keys(_REPDIChart.Data.DataSourceConfig.schema.cube.dimensions).map(function (k) { return { ID: k, Text: k } }));
                $scope.cboPieChartMeasure_Options.dataSource.data(Object.keys(_REPDIChart.Data.DataSourceConfig.schema.cube.measures).map(function (k) { return { ID: k, Text: k } }));

                $timeout(function () {
                    var pivotgrid = $('#repdipivot_pivot_grid').data('kendoPivotGrid');
                    if (pivotgrid != null) {
                        pivotgrid.destroy();
                        $('#repdipivot_pivot_grid').empty();
                    }

                    var pivotgrid = $('#repdipivot_pivot_grid').kendoPivotGrid({
                        //rowHeaderTemplate: '#: member.name # - #: member.caption #',
                        excel: { fileName: 'pivotpl.xlsx' },
                        filterable: true, sortable: false, columnWidth: '150',
                        //gather the collapsed members
                        collapseMember: function (e) {
                            var axis = _REPDIChart.Data.collapsed[e.axis];
                            var path = e.path[0];
                            if (axis.indexOf(path) === -1) axis.push(path);
                        },
                        //gather the expanded members
                        expandMember: function (e) {
                            var axis = _REPDIChart.Data.collapsed[e.axis];
                            var index = axis.indexOf(e.path[0]);
                            if (index !== -1) axis.splice(index, 1);
                        },
                        dataSource: angular.copy(_REPDIChart.Data.DataSourceConfig),
                    }).data("kendoPivotGrid");

                    var parentconfig = $("#repdipivot_pivot_config").parent();
                    var height = parentconfig.height() - 10;
                    if (height < 300) height = 300;

                    var config = $("#repdipivot_pivot_config").data('kendoPivotConfigurator');
                    if (config == null) {
                        $("#repdipivot_pivot_config").kendoPivotConfigurator({
                            dataSource: pivotgrid.dataSource,
                            filterable: true,
                            sortable: false,
                            height: height
                        });
                    }
                    else {
                        config.setDataSource(pivotgrid.dataSource);
                    }

                    // Show the chart
                    $scope.ResetSettings();
                    $scope.REPDIPivot_ShowChart();
                }, 1);

            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

        $timeout(function () {
            $scope.REPDIPivot_Splitter.resize();
        }, 1);
    };

    $scope.REPDIPivot_ShowChart = function () {
        // Prepare data for all chart
        _REPDIChart.initSettingsData($scope);
        $scope.initFinanceChartData();

        if ($scope.Item.chart_type == 'document') {
            $scope.filters = $scope.dfilters;
            _REPDIChart.showChartWithFilter($scope);
        }
        else {
            $scope.showFinanceChart();
        }
    };

    $scope.REPDIPivot_UpdateChart = function () {
        if ($scope.Item.chart_type == "document") {
            _REPDIChart.showChartWithFilter(this);
        }
        else {
            $scope.initFinanceChartData()
            $scope.showFinanceChart()
        }
    };

    $scope.showFinanceChart = function () {
        _REPDIChart.showWaterfallChart($scope.financeCategories[$scope.Item.fchart_index], $scope.financeData[$scope.Item.fchart_index], $scope.chart_theme);
    };

    $scope.initFinanceChartData = function () {
        $scope.filters = $scope.ffilters;
        // init filters for datasource
        var rowArr = [];
        var filterArr = [];
        $.each($scope.filters, function (i, f) {
            if (f.choice != null && f.choice > 0) {
                rowArr.push({ name: f.name, expand: true });
                filterArr.push({ field: f.name, operator: "eq", value: f.filterOptions[f.choice] })
            }
        })

        $scope.financeCategories = [["Cước thu", "Cước b.x lên", "Cước b.x xuống", "Cước trả về", "Cước b.x lên trả về", "Cước b.x xuống trả về", "Cước phụ thu"],
                                    ["Cước chi", "Cước chi b.x lên", "Cước chi b.x xuống", "Cước chi trả về", "Cước chi b.x lên trả về", "Cước chi b.x xuống trả về", "Cước chi phụ thu"]];
        $scope.financeData = [];
        $.each($scope.financeCategories, function (i, v) {
            var DSConfig = angular.copy(_REPDIChart.Data.DataSourceConfig);
            angular.extend(DSConfig, { columns: [], rows: rowArr, filter:filterArr, measures: $scope.financeCategories[i] });
            var tmpDS = new kendo.data.PivotDataSource(DSConfig);
            tmpDS.read();
            var dataArr = tmpDS.data().map(function (d) { return d.value });
            dataArr = dataArr.slice(0, v.length);

            $scope.financeData.push(dataArr);
        })
    };
    $scope.initPieChartData = function(){
        var DSConfig = angular.copy(_REPDIChart.Data.DataSourceConfig);
        angular.extend(DSConfig, { columns: [], rows: [{ name: $scope.Item.PieChart.category, expand: true }], filter: [], measures: [$scope.Item.PieChart.measure] });
        var tmpDS = new kendo.data.PivotDataSource(DSConfig);
        tmpDS.read();
        var categories = _REPDIChart.initFilterOptions(tmpDS);
        var data = tmpDS.data().map(function (o) { return o.value });
        var total = 0;
        var chartData = [];
        for (var i = 0; i < categories.length; i++) {
            total = total + data[i]*1;
        }
        for (var i = 1; i < categories.length; i++) {
            chartData.push({ category: categories[i], value: (data[i] * 100 / total).toFixed(2) });
        }
        return chartData;
    };
    $scope.showPieChart = function () {
        _REPDIChart.showPieChart($scope.initPieChartData(), $scope.chart_theme);
    };

    //#region Template
    $scope.LoadTemplate = function () {
        Common.Log('LoadTemplate');

        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPivotOrder.URL.GetTemplate,
            data: { functionID: $rootScope.FunctionItem.ID },
            success: function (res) {
                if (Common.HasValue(res) && Common.HasValue(res.lstTemplate)) {
                    $scope.REPDIPivot_Template_grid.dataSource.data(res.lstTemplate);
                }
            }
        });
    };

    $scope.REPDIPivot_Template_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                    }
            },
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,REPDIPivot_Template_grid,REPDIPivot_Template_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,REPDIPivot_Template_grid,REPDIPivot_Template_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Name', width: '500px', title: 'Template', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: '', width: '', title: '' },
        ],
        toolbar: kendo.template($('#REPDIPivot_Template_gridToolbar').html()),
    };

    $scope.LoadTemplate();

    $scope.REPDIPivot_Template_gridChoose_Change = function ($event, grid, haschoose) {
        $scope.HasChooseTemplate = haschoose;
    };

    $scope.Template_Click = function ($event, win) {
        Common.Log('Template_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        $timeout(function () {
            $scope.REPDIPivot_Template_grid.resize();
        }, 10);

        win.center().open();
    };

    $scope.Add_Click = function ($event, win) {
        Common.Log('Add_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        $scope.Template.Name = "";
        win.center().open();
    };

    $scope.Save_Click = function ($event, win) {
        Common.Log('Save_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        
        var config = $("#repdipivot_pivot_config").data('kendoPivotConfigurator');
        if (Common.HasValue(config)) {
            if ($scope.Template.Name.length > 0) {
                var item = {};
                item.Name = $scope.Template.Name;
                item.lstRow = [];
                item.lstColumn = [];
                item.lstMeasure = [];
                var lstRow = config.dataSource.rows();
                var lstColumn = config.dataSource.columns();
                var lstMeasure = config.dataSource.measures();
                $.each(lstRow, function (i, o) {
                    item.lstRow.push({ name: o.name[0], expand: o.expand });
                });
                $.each(lstColumn, function (i, o) {
                    item.lstColumn.push({ name: o.name[0], expand: o.expand });
                });
                $.each(lstMeasure, function (i, o) {
                    item.lstMeasure.push(o.name);
                });
                
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPDIPivotOrder.URL.SaveTemplate,
                    data: { functionID: $rootScope.FunctionItem.ID, item: item },
                    success: function (res) {
                        $scope.LoadTemplate();
                        $rootScope.Message({ Msg: 'Đã thêm', NotifyType: Common.Message.NotifyType.SUCCESS });
                        win.close();
                    }
                });
            }
        } else {
            $rootScope.Message({ Msg: 'Chưa có template', NotifyType: Common.Message.NotifyType.ERROR });
        }
    };

    $scope.Accept_Click = function ($event, win, grid) {
        Common.Log('Accept_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        var item = null;
        var count = 0;
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
            {
                item = v;
                count++;
            }
        });

        if (count == 1) {
            var pivotgrid = $('#repdipivot_pivot_grid').data('kendoPivotGrid');
            if (Common.HasValue(pivotgrid)) {
                var columns = [];
                var rows = [];
                var measures = [];
                $.each(item.lstColumn, function (i, o) {
                    columns.push({ name: o.name, expand: o.expand });
                });
                $.each(item.lstRow, function (i, o) {
                    rows.push({ name: o.name, expand: o.expand });
                });
                $.each(item.lstMeasure, function (i, o) {
                    measures.push(o);
                });
                
                $scope.REPDIPivot_Search($event, $scope.mulCustomer_Options, columns, rows, measures);
            }
        } else {
            if (count > 1)
                $rootScope.Message({ Msg: 'Chỉ được chọn 1 template', NotifyType: Common.Message.NotifyType.ERROR });
        }

        win.close();
    };

    $scope.Delete_Click = function ($event, win, grid) {
        Common.Log('Delete_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });

        if (lstid.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPivotOrder.URL.DeleteTemplate,
                data: { functionID: $rootScope.FunctionItem.ID, lstid: lstid },
                success: function (res) {
                    $scope.LoadTemplate();
                    $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.ERROR });
                }
            });
        }

    };

    $scope.Close_Click = function ($event, win) {
        Common.Log('Close_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        win.close();
    };

    $scope.REPDIPivot_Expand = function ($event) {
        var pivotDataSource = $("#repdipivot_pivot_grid").data("kendoPivotGrid").dataSource;

        pivotDataSource.expandColumn("[VendorShortName].[OrderCode]");
    }
    //#endregion
}]);