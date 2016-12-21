
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPCOPivotOrder = {
    URL: {
        Read: 'REPCOPivotOrder_Data',
        GetTemplate: 'REPCOPivotOrder_GetTemplate',
        SaveTemplate: 'REPCOPivotOrder_SaveTemplate',
        DeleteTemplate: 'REPCOPivotOrder_DeleteTemplate',
        Read_Customer: 'REP_Customer_Read',
    }
}

var collapsed = {
    columns: [],
    rows: []
};

var _REPCOChart = {
    Data: {
        CookieSearch: "REPCOPivotPOD_CookieSearch",
        DataSourceConfig: [],
        _dataChart: [],
        collapsed: {
            columns: [],
            rows: []
        }
    },

    //function flatten the tree of tuples that datasource returns
    flattenTree: function (tuples) {
        tuples = tuples.slice();
        var result = [];
        var tuple = tuples.shift();
        var idx, length, spliceIndex, children, member;
        while (tuple) {
            //required for multiple measures
            if (tuple.dataIndex !== undefined) {
                result.push(tuple);
            }
            spliceIndex = 0;
            for (idx = 0, length = tuple.members.length; idx < length; idx++) {
                member = tuple.members[idx];
                children = member.children;
                if (member.measure) {
                    [].splice.apply(tuples, [0, 0].concat(children));
                } else {
                    [].splice.apply(tuples, [spliceIndex, 0].concat(children));
                }
                spliceIndex += children.length;
            }
            tuple = tuples.shift();
        }
        return result;
    },

    //Check whether the tuple has been collapsed
    isCollapsed: function (tuple, collapsed) {
        if (tuple.members == undefined || tuple.members.length == 0) {// measures
            return false;
        }
        var name = tuple.members[0].parentName;
        for (var idx = 0, length = collapsed.length; idx < length; idx++) {
            if (collapsed[idx] === name) {
                console.log(name);
                return true;
            }
        }
        return false;
    },

    //the main function that convert PivotDataSource data into understandable for the Chart widget format
    convertData: function (dataSource, collapsed, filterCount) {
        var columnTuples = this.flattenTree(dataSource.axes().columns.tuples || [], collapsed.columns);
        var rowTuples = this.flattenTree(dataSource.axes().rows.tuples || [], collapsed.rows);
        var data = dataSource.data();
        var rowTuple, columnTuple;
        var result = [];
        var columnsLength = columnTuples.length;
        var idx = (filterCount + 1) * columnsLength
        for (var i = filterCount + 1; i < rowTuples.length; i++) {
            rowTuple = rowTuples[i];
            var cube = {
                row: rowTuple.members[rowTuple.members.length - 1].caption
            };
            for (var j = 0; j < columnsLength; j++) {
                columnTuple = columnTuples[j];
                var fieldName = "field" + j.toString();
                angular.extend(cube, { [fieldName]: Number(data[idx].value).toFixed(2) });
                idx += 1;
            }
            result.push(cube);
        }
        return result;
    },

    showWaterfallChart: function (categories, data, chart_theme) {
        var chart = $("#repdipivot_pivot_chart").data("kendoChart");
        // distroy existing chart
        if (chart != null) {
            chart.destroy();
            $('#repdipivot_pivot_chart').empty();
        }

        var chartData = [];
        $.each(categories, function(i, c){
            chartData.push({caption: c, value: data[i]});
        })
        chartData.push({caption: "Tổng", summary: "total"});
        var palette = [
            "#31B6FC",
        ];
        debugger
        var options = {
            dataSource:{
                data: chartData
            },
            series: [{
                type: "horizontalWaterfall",
                field: "value",
                categoryField: "caption",
                summaryField: "summary",
                color: function(point) {
                    return palette[point.index % palette.length];
                }                
            }],
            legend: {
                visible: false
            },
            tooltip: { visible: true, template: "#= Common.Number.ToMoney(value) # đ" },
            axisDefaults: {
                majorGridLines: {
                    visible: false
                }
            },
            valueAxis: {
                labels: {
                    template: "#: kendo.toString(value, 'n0') #"
                }
            }
        };

        $("#repdipivot_pivot_chart").kendoChart(
            angular.extend({}, options)
            );

        chart = $("#repdipivot_pivot_chart").data("kendoChart");
        chart.setOptions({ theme: chart_theme });
        chart.refresh();
    },

    showPieChart: function(data, chart_theme){
        var chart = $("#repdipivot_pivot_chart").data("kendoChart");
        // distroy existing chart
        if (chart != null) {
            chart.destroy();
            $('#repdipivot_pivot_chart').empty();
        }

        var options = {
            legend: {
                position: "bottom"
            },
            seriesDefaults: {
                labels: {
                    visible: true,
                    format: "{0}%"
                }
            },
            series: [{
                type: "pie",
                data: data
            }],
            tooltip: { visible: true, template: "#= category #" }
        };

        $("#repdipivot_pivot_chart").kendoChart(
            angular.extend({}, options)
            );
        chart = $("#repdipivot_pivot_chart").data("kendoChart");
        chart.setOptions({ theme: chart_theme });
        chart.refresh();
    },

    initChart: function (data, measures, scope) {
        var chart = $("#repdipivot_pivot_chart").data("kendoChart");
        // distroy existing chart
        if (chart != null) {
            chart.destroy();
            $('#repdipivot_pivot_chart').empty();
        }

        // create new chart here
        var seriesSettings = [];
        for (var i = 0; i < measures.length; i++) {
            var chartSettings = scope.barChartSettings[i];
            var chart_type = chartSettings.filterCodes[chartSettings.choice];
            seriesSettings.push(angular.extend({field: "field" + i.toString(), name: measures[i], axis: i.toString()}, this.initChartType(chart_type)));
        }

        var valueAxisSettings = [];
        for (var i = 0; i < measures.length; i++) {
            valueAxisSettings.push({ labels: { format: "{0}" }, name: i.toString(), title: { text: measures[i] } });
        }

        var axisCrossingValueSettings=[];
        for(var i = 0; i < measures.length; i++){
            axisCrossingValueSettings.push(i<measures.length/2? 0 : 1000);
        }

        $("#repdipivot_pivot_chart").kendoChart({
            dataSource: {
                data: data
            },
            series: seriesSettings,
            legend: {
                position: "bottom"
            },
            valueAxis: valueAxisSettings,
            categoryAxis: {
                field: "row",
                labels: {
                    rotation: -90,
                    template: '#= value #'
                },
                min:0,max:7,
                axisCrossingValue: axisCrossingValueSettings
            },
            tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToMoney(value) #" },
            pannable: {
                lock: "y"
            },
            zoomable: {
                mousewheel: {
                    lock: "y"
                },
                selection: {
                    lock: "y"
                }
            },
            dataBound: function(e) {
                var view = e.sender.dataSource.view();
                $(".overlay").toggle(view.length === 0);
            }
        });

        chart = $("#repdipivot_pivot_chart").data("kendoChart");
        chart.setOptions({ theme: scope.chart_theme });
        chart.refresh();
        chart.setDataSource(new kendo.data.DataSource({data: data}));
    },

    /* Summary: Init the setting object with chart type */
    initChartType: function (chart_type){
        var seriesSetting = {};
        switch (chart_type)
        {
            case "stackedline":
                seriesSetting = {type: 'line'};
                break;
            case "smoothline":
                seriesSetting = {type: 'line', style: 'smooth'};
                break;
            default:
                seriesSetting = {type: 'column'};
                break;
        }
        return seriesSetting;
    },

    showChartWithFilter: function(scope){
        // Only show chart when category exists
        if (!scope.categoryName){
            return true;
        }
        var DSConfig = angular.copy(_REPCOChart.Data.DataSourceConfig);
        var rowArr = [];
        var filterArr = [];
        if(Common.HasValue(scope.filters)){
            $.each(scope.filters, function(i,f){
                if (f.choice != null && f.choice > 0){
                    rowArr.push({name:f.name, expand: true });
                    filterArr.push({field: f.name, operator: "eq", value: f.filterOptions[f.choice] })
                }
            })
            rowArr.push({name:scope.categoryName, expand: true });

            angular.extend(DSConfig, {columns: [], rows: rowArr, filter:filterArr});
            var tmpDS = new kendo.data.PivotDataSource(DSConfig);
            tmpDS.read();
            this.initChart(_REPCOChart.convertData(tmpDS, _REPCOChart.Data.collapsed, filterArr.length), tmpDS.options.measures, scope);
        }
    },

    initFilterOptions: function(dataSource){
        var rowTuples = dataSource.axes().rows.tuples;
        if (rowTuples.length > 0 && rowTuples[0].members.length == 1){
            var item = rowTuples[0].members[0];
            var filterOptions = [];
            filterOptions.push(item.caption);
            $.each(item.children, function(i, c){
                var caption = c.members[0].caption;
                filterOptions.push(caption)
            })
            filterOptions[0] = "Tất cả";
            return filterOptions;
        }
        else {
            return [];
        }
    },

    initFilterData: function(scope, filterName){
        var DSConfig = angular.copy(_REPCOChart.Data.DataSourceConfig);
        angular.extend(DSConfig,{columns: [], rows: [{ name: filterName, expand: true }],measures: ["Sản lượng thu"]  });
        var tmpDS = new kendo.data.PivotDataSource(DSConfig);
        tmpDS.read();
        scope.filters.push({filterOptions: _REPCOChart.initFilterOptions(tmpDS), choice: "Tất cả", name:filterName});
    },

    initSettingsData: function (scope) {
        var pivotGridData = $('#repdipivot_pivot_grid').data('kendoPivotGrid');
        if (pivotGridData != null) {
            var pivotDataSource = pivotGridData.dataSource;
            scope.filters = [];
            $.each(pivotDataSource.rows(), function (i, v){
                _REPCOChart.initFilterData(scope, v.name[0]);
            })
            $.each(pivotDataSource.columns(), function (i, v){
                _REPCOChart.initFilterData(scope, v.name[0]);
            })

            scope.barChartSettings = [];
            $.each(pivotDataSource.measures(), function(i, v){
                scope.barChartSettings.push({
                    filterOptions: ["Bar chart", "Line chart(stacked)", "Line chart(smooth)"],
                    filterCodes: ["column", "stackedline", "smoothline"],
                    name: v.name,
                    choice: "Bar chart"
                });
            })

            // Update measures to stored datasource
            _REPCOChart.Data.DataSourceConfig.measures = pivotDataSource.measures().map(function(o) {return o.name;});
            if (scope.filters.length > 0){
                // the last column (or row) will be the category, cannot be filter
                scope.categoryName = scope.filters[scope.filters.length-1].name;
                // ffilter: filters for finance charts
                scope.ffilters = angular.copy(scope.filters);
                // dfilters: for document(chung tu) charts
                scope.filters.splice(-1,1);
                // dfilters: for document(chung tu) charts
                scope.dfilters = scope.filters;
            }
            else {
                scope.dfilters = [];
                scope.ffilters = [];
                //No rows no columns, do not show chart
            }
        }
    }
}

myapp.controller('REPCOPivotOrder_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPCOPivotOrder_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.chart_theme = 'Bootstrap';
    $scope.chart_type = 'column';
    $scope.Template = {};
    $scope.Template.Name = "";
    $scope.HasChooseTemplate = false;

    $scope.InitDataChart = function () {
        _REPCOChart.Data._dataChart = [];
        _REPCOChart.Data._dataChart.push({ Code: 'document', Name: 'Chứng từ' });
        _REPCOChart.Data._dataChart.push({ Code: 'finance', Name: 'Tài chính' });
        _REPCOChart.Data._dataChart.push({ Code: 'analysis', Name: 'Phân tích' });
        $scope.cboChart_Options.dataSource.data(_REPCOChart.Data._dataChart);
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
        PieChart: { category: "OrderCode", measure: "Đơn giá thu" },
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
                    _REPCOChart.showChartWithFilter($scope);
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
        var strCookie = Common.Cookie.Get(_REPCOChart.Data.CookieSearch);
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
        method: _REPCOPivotOrder.URL.Read_Customer,
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
        Common.Cookie.Set(_REPCOChart.Data.CookieSearch, JSON.stringify($scope.Item));
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
            method: _REPCOPivotOrder.URL.Read,
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

                _REPCOChart.Data.DataSourceConfig = {
                    data: res,
                    schema: {
                        model: {
                            fields: {
                                OrderCode: { type: "string" },
                                ContainerNo: { type: "string" },
                                SealNo1: { type: "string" },
                                SealNo2: { type: "string" },
                                PackingCode: { type: "string" },
                                LocationFromCode: { type: "string" },
                                LocationFromName: { type: "string" },
                                LocationToCode: { type: "string" },
                                LocationToName: { type: "string" },
                                CUSRoutingCode: { type: "string" },
                                CUSRoutingName: { type: "string" },
                                CustomerCode: { type: "string" },
                                CustomerName: { type: "string" },
                                OrderDateConfig: { type: "string" },
                                OrderCreatedBy: { type: "string" },
                                OrderCreatedDate: { type: "string" },
                                UserDefine1: { type: "string" },
                                UserDefine2: { type: "string" },
                                UserDefine3: { type: "string" },
                                TripNo: { type: "string" },
                                ServiceOfOrderIncome: { type: "string" },
                                ServiceOfOrderCost: { type: "string" },
                                IncomeUnitPrice: { type: "string" },
                                IncomeQuantity: { type: "string" },
                                Income: { type: "string" },
                                IncomeEx: { type: "string" },
                                CostUnitPrice: { type: "string" },
                                CostQuantity: { type: "string" },
                                Cost: { type: "string" },
                                IncomeTrouble: { type: "string" },
                                CostTrouble: { type: "string" },
                                CostDepreciation: { type: "string" },
                                CostSchedule: { type: "string" },
                                CostDriver: { type: "string" },
                                CostStation: { type: "string" },
                                TotalIncome: { type: "string" },
                                TotalCost: { type: "string" },
                                TotalPL: { type: "string" },
                            }
                        },
                        cube: {
                            dimensions: {
                                OrderCode: { caption: "Đơn hàng" },
                                ContainerNo: { caption: "Số cont" },
                                SealNo1: { caption: "Số seal1" },
                                SealNo2: { caption: "Số seal2" },
                                PackingCode: { caption: "Loại cont" },
                                LocationFromCode: { caption: "Mã điểm lấy" },
                                LocationFromName: { caption: "Tên điểm lấy" },
                                LocationToCode: { caption: "Mã điểm giao" },
                                LocationToName: { caption: "Tên điểm giao" },
                                CUSRoutingCode: { caption: "Mã cung đường" },
                                CUSRoutingName: { caption: "Tên cung đường" },
                                CustomerCode: { caption: "Mã khách hàng" },
                                CustomerName: { caption: "Tên khách hàng" },
                                OrderDateConfig: { caption: "Ngày tính giá" },
                                OrderCreatedBy: { caption: "Người tạo đơn hàng" },
                                OrderCreatedDate: { caption: "Ngày tạo đơn hàng" },
                                UserDefine1: { caption: "Ghi chú đơn hàng 1" },
                                UserDefine2: { caption: "Ghi chú đơn hàng 2" },
                                UserDefine3: { caption: "Ghi chú đơn hàng 3" },
                                TripNo: { caption: "Hãng tàu" },
                                ServiceOfOrderIncome: { caption: "Loại hình vận chuyển thu" },
                                ServiceOfOrderCost: { caption: "Loại hình vận chuyển chi" },
                            },
                            measures: {
                                "Đơn giá thu": { field: "IncomeUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Sản lượng thu": { field: "IncomeQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước thu": { field: "Income", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Phụ phí": { field: "IncomeEx", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Đơn giá chi": { field: "CostUnitPrice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Sản lượng chi": { field: "CostQuantity", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Cước chi": { field: "Cost", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Phụ thu phát sinh": { field: "IncomeTrouble", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Chi phí phát sinh": { field: "CostTrouble", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Chi phí khấu hao": { field: "CostDepreciation", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Chi phí tháng": { field: "CostSchedule", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Chi phí tài xế": { field: "CostDriver", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Chi phí trạm": { field: "CostStation", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Tổng thu": { field: "TotalIncome", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Tổng chi": { field: "TotalCost", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Lợi nhuận": { field: "TotalPL", format: "{0:#,##0.0}", aggregate: "sum" },
                            }
                        }
                    },
                    columns: _columns,
                    rows: _rows,
                    measures: _measures
                };

                //Options for pie chart
                $scope.cboPieChartCat_Options.dataSource.data(Object.keys(_REPCOChart.Data.DataSourceConfig.schema.cube.dimensions).map(function (k) { return { ID: k, Text: k } }));
                $scope.cboPieChartMeasure_Options.dataSource.data(Object.keys(_REPCOChart.Data.DataSourceConfig.schema.cube.measures).map(function (k) { return { ID: k, Text: k } }));

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
                            var axis = _REPCOChart.Data.collapsed[e.axis];
                            var path = e.path[0];
                            if (axis.indexOf(path) === -1) axis.push(path);
                        },
                        //gather the expanded members
                        expandMember: function (e) {
                            var axis = _REPCOChart.Data.collapsed[e.axis];
                            var index = axis.indexOf(e.path[0]);
                            if (index !== -1) axis.splice(index, 1);
                        },
                        dataSource: angular.copy(_REPCOChart.Data.DataSourceConfig),
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
        _REPCOChart.initSettingsData($scope);
        $scope.initFinanceChartData();

        if ($scope.Item.chart_type == 'document') {
            $scope.filters = $scope.dfilters;
            _REPCOChart.showChartWithFilter($scope);
        }
        else {
            $scope.showFinanceChart();
        }
    };

    $scope.REPDIPivot_UpdateChart = function () {
        if ($scope.Item.chart_type == "document") {
            _REPCOChart.showChartWithFilter(this);
        }
        else {
            $scope.initFinanceChartData()
            $scope.showFinanceChart()
        }
    };

    $scope.showFinanceChart = function () {
        _REPCOChart.showWaterfallChart($scope.financeCategories[$scope.Item.fchart_index], $scope.financeData[$scope.Item.fchart_index], $scope.chart_theme);
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

        $scope.financeCategories = [["Cước thu", "Phụ phí", "Phụ thu phát sinh"],
                                    ["Cước chi", "Chi phí phát sinh", "Chi phí khấu hao", "Chi phí tháng", "Chi phí tài xế", "Chi phí trạm"]];
        $scope.financeData = [];
        $.each($scope.financeCategories, function (i, v) {
            var DSConfig = angular.copy(_REPCOChart.Data.DataSourceConfig);
            angular.extend(DSConfig, { columns: [], rows: rowArr, filter:filterArr, measures: $scope.financeCategories[i] });
            var tmpDS = new kendo.data.PivotDataSource(DSConfig);
            tmpDS.read();
            var dataArr = tmpDS.data().map(function (d) { return d.value });
            dataArr = dataArr.slice(0, v.length);

            $scope.financeData.push(dataArr);
        })
    };
    $scope.initPieChartData = function(){
        var DSConfig = angular.copy(_REPCOChart.Data.DataSourceConfig);
        angular.extend(DSConfig, { columns: [], rows: [{ name: $scope.Item.PieChart.category, expand: true }], filter: [], measures: [$scope.Item.PieChart.measure] });
        var tmpDS = new kendo.data.PivotDataSource(DSConfig);
        tmpDS.read();
        var categories = _REPCOChart.initFilterOptions(tmpDS);
        var data = tmpDS.data().map(function (o) { return o.value });
        var total = 0;
        var chartData = [];
        for (var i = 0; i < categories.length; i++) {
            total = total + data[i]*1;
        }
        for (var i = 1; i < categories.length; i++) {
            chartData.push({ category: categories[i], value: (data[i]*100/total).toFixed(2) });
        }
        return chartData;
    };
    $scope.showPieChart = function () {
        _REPCOChart.showPieChart($scope.initPieChartData(), $scope.chart_theme);
    };

    //#region Template
    $scope.LoadTemplate = function () {
        Common.Log('LoadTemplate');

        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPCOPivotOrder.URL.GetTemplate,
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
                    method: _REPCOPivotOrder.URL.SaveTemplate,
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
                method: _REPCOPivotOrder.URL.DeleteTemplate,
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
    //#endregion
}]);