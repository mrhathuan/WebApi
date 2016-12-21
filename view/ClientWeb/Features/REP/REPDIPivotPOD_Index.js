
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIPivotPOD_Index = {
    URL: {
        List: 'REPDIPivotPOD_List',
        Read_Customer: 'REP_Customer_Read',
    }
};
var _REPDIChart = {
    Data: {
        CookieSearch: "REPDIPivotPOD_CookieSearch",
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
        var DSConfig = angular.copy(_REPDIChart.Data.DataSourceConfig);
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
            this.initChart(_REPDIChart.convertData(tmpDS, _REPDIChart.Data.collapsed, filterArr.length), tmpDS.options.measures, scope);
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
        var DSConfig = angular.copy(_REPDIChart.Data.DataSourceConfig);
        angular.extend(DSConfig,
                                        {
                                            columns: [],
                                            rows: [{ name: filterName, expand: true }],
                                            measures: ["Tấn nhận"]
                                        });
        var tmpDS = new kendo.data.PivotDataSource(DSConfig);
        tmpDS.read();
        scope.filters.push({filterOptions: _REPDIChart.initFilterOptions(tmpDS), choice: "Tất cả", name:filterName});
    },

    initSettingsData: function (scope) {
        var pivotGridData = $('#repdipivot_pivot_grid').data('kendoPivotGrid');
        if (pivotGridData != null) {
            var pivotDataSource = pivotGridData.dataSource;
            scope.filters = [];
            $.each(pivotDataSource.rows(), function (i, v){
                _REPDIChart.initFilterData(scope, v.name[0]);
            })
            $.each(pivotDataSource.columns(), function (i, v){
                _REPDIChart.initFilterData(scope, v.name[0]);
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
            _REPDIChart.Data.DataSourceConfig.measures = pivotDataSource.measures().map(function(o) {return o.name;});
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

var myapp = angular.module('myapp');
myapp.directive('filterMenu', function(){
    return {
        scope: {
            filterMenu: "=",
            update: '&'
        },
        restrict: 'A',
        templateUrl: 'Features/REP/REPDIPivotPOD_Filter.html',
        link: function(scope, el, attrs){
        },
        controller: function($scope){
            $scope.updateChart = function(){
                $scope.update();
            }
        }
    }
});

myapp.controller('REPDIPivotPOD_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIPivotPOD_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.chart_theme = 'Default';
    $scope.chart_type = 'column';

    $scope.InitDataChart = function () {
        _REPDIChart.Data._dataChart = [];
        _REPDIChart.Data._dataChart.push({ Code: 'column', Name: 'Bar chart' });
        _REPDIChart.Data._dataChart.push({ Code: 'stackedline', Name: 'Line chart(stacked)' });
        _REPDIChart.Data._dataChart.push({ Code: 'smoothline', Name: 'Line chart(smooth)' });
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

    $scope.Item = {
        lstCustomerID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        ChartType: 'column',
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
            $scope.chart_type = this.value();
            _REPDIChart.showChartWithFilter($scope);
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
        method: _REPDIPivotPOD_Index.URL.Read_Customer,
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

    $scope.REPDIPivot_Search = function ($event, cbo) {
        $event.preventDefault();
        var ItemFilter
        Common.Cookie.Set(_REPDIChart.Data.CookieSearch, JSON.stringify($scope.Item));

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPivotPOD_Index.URL.List,
            data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;

                angular.forEach(res, function (v, i) {
                    v.RequestDate = Common.Date.FromJsonDDMMYY(v.RequestDate);
                    v.InvoiceDate = Common.Date.FromJsonDDMMYY(v.InvoiceDate);
                    v.KPIOPSDate = Common.Date.FromJsonDDMMYY(v.KPIOPSDate);
                    v.KPIPODDate = Common.Date.FromJsonDDMMYY(v.KPIPODDate);
                    v.DateFromCome = Common.Date.FromJsonDDMMYY(v.DateFromCome);
                    v.DateFromLeave = Common.Date.FromJsonDDMMYY(v.DateFromLeave);
                    v.DateFromLoadStart = Common.Date.FromJsonDDMMYY(v.DateFromLoadStart);
                    v.DateFromLoadEnd = Common.Date.FromJsonDDMMYY(v.DateFromLoadEnd);
                    v.DateToCome = Common.Date.FromJsonDDMMYY(v.DateToCome);
                    v.DateToLeave = Common.Date.FromJsonDDMMYY(v.DateToLeave);
                    v.DateToLoadStart = Common.Date.FromJsonDDMMYY(v.DateToLoadStart);
                    v.DateToLoadEnd = Common.Date.FromJsonDDMMYY(v.DateToLoadEnd);
                });

                _REPDIChart.Data.DataSourceConfig = {
                    data: res,
                    schema: {
                        model: {
                            fields: {
                                OrderCode: { type: "string" },
                                DNCode: { type: "string" },
                                RequestDate: { type: "string" },
                                SOCode: { type: "string" },

                                StockCode: { type: "string" },
                                StockName: { type: "string" },
                                StockAddress: { type: "string" },

                                PartnerCode: { type: "string" },
                                PartnerName: { type: "string" },
                                PartnerCodeName: { type: "string" },
                                Address: { type: "string" },

                                CUSRoutingCode: { type: "string" },
                                CUSRoutingName: { type: "string" },
                                GroupOfProductCode: { type: "string" },
                                GroupOfProductName: { type: "string" },
                                VehicleCode: { type: "string" },
                                VendorCode: { type: "string" },
                                VendorName: { type: "string" },
                                CustomerCode: { type: "string" },
                                CustomerName: { type: "string" },

                                IsOrigin: { type: "bool" },
                                IntWaitInvoice: { type: "number" },
                                IntCompleteInvoice: { type: "number" },
                                IntRemainInvoice: { type: "number" },
                                InvoiceBy: { type: "string" },
                                InvoiceDate: { type: "string" },
                                InvoiceNote: { type: "string" },
                                Note: { type: "string" },
                                Note2: { type: "string" },
                                Note1: { type: "string" },

                                KPIOPSDate: { type: "string" },
                                KPIPODDate: { type: "string" },
                                IntKPIOPS: { type: "number" },
                                IntKPIPOD: { type: "number" },
                                WaitKPIPOD: { type: "number" },
                                FailKPIPOD: { type: "number" },
                                WaitFailKPIPOD: { type: "number" },

                                DateFromCome: { type: 'string' },
                                DateFromLeave: { type: 'string' },
                                DateFromLoadStart: { type: 'string' },
                                DateFromLoadEnd: { type: 'string' },
                                DateToCome: { type: 'string' },
                                DateToLeave: { type: 'string' },
                                DateToLoadStart: { type: 'string' },
                                DateToLoadEnd: { type: 'string' },

                                Ton: { type: "number" },
                                CBM: { type: "number" },
                                TonTranfer: { type: "number" },
                                CBMTranfer: { type: "number" },
                                QuantityTranfer: { type: "number" },
                                TonBBGN: { type: "number" },
                                CBMBBGN: { type: "number" },
                                QuantityBBGN: { type: "number" },

                            }
                        },
                        cube: {
                            dimensions: {
                                OrderCode: { caption: "Đơn hàng" },
                                DNCode: { caption: "Số DN" },
                                RequestDate: { caption: "Ngày y/c v/c" },
                                SOCode: { caption: "Số SO" },

                                StockCode: { caption: "Mã điểm lấy" },
                                StockName: { caption: "Tên điểm lấy" },
                                StockAddress: { caption: "Điểm lấy" },

                                PartnerCode: { caption: "Mã NPP" },
                                PartnerName: { caption: "Điểm giao" },
                                PartnerCodeName: { caption: "Mã + Tên phân phối" },
                                Address: { caption: "Điểm giao" },

                                CUSRoutingCode: { caption: "Mã cung đường" },
                                CUSRoutingName: { caption: "Cung đường" },
                                GroupOfProductCode: { caption: "Mã nhóm hàng" },
                                GroupOfProductName: { caption: "Nhóm hàng" },
                                VehicleCode: { caption: "Số xe" },
                                VendorCode: { caption: "Mã nhà vận tải" },
                                VendorName: { caption: "Nhà vận tải" },
                                CustomerCode: { caption: "Mã khách hàng" },
                                CustomerName: { caption: "Khách hàng" },

                                IsOrigin: { caption: "Chứng từ gốc" },
                                InvoiceBy: { caption: "Người nhận" },
                                InvoiceDate: { caption: "Ngày nhận" },
                                InvoiceNote: { caption: "Số chứng từ" },
                                Note: { caption: "Ghi chú" },
                                Note2: { caption: "Ghi chú 2" },
                                Note1: { caption: "Ghi chú 1" },

                                KPIOPSDate: { caption: "Ngày hợp đồng v/c" },
                                KPIPODDate: { caption: "Ngày hợp đồng c.từ" },

                                DateFromCome: { caption: 'Ngày đến kho' },
                                DateFromLeave: { caption: 'Ngày rời kho' },
                                DateFromLoadStart: { caption: 'T.gian vào máng' },
                                DateFromLoadEnd: { caption: 'T.gian ra máng' },
                                DateToCome: { caption: 'Ngày đến NPP' },
                                DateToLeave: { caption: 'Ngày rời NPP' },
                                DateToLoadStart: { caption: 'T.gian BĐ dỡ hàng' },
                                DateToLoadEnd: { caption: 'T.gian KT dỡ hàng' },
                            },
                            measures: {
                                "Tấn nhận": { field: "TonTranfer", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Khối nhận": { field: "CBMTranfer", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số lượng nhận": { field: "TonTranfer", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Tấn giao": { field: "TonBBGN", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Khối giao": { field: "CBMBBGN", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số lượng giao": { field: "QuantityBBGN", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Đạt KPI v/c": { field: "IntKPIOPS", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Đạt KPI c.từ": { field: "IntKPIPOD", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Chưa đến hạn c.từ": { field: "WaitKPIPOD", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Quá hạn c.từ": { field: "FailKPIPOD", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Quá hạn c.từ và chưa nhận": { field: "WaitFailKPIPOD", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số c.từ phải nhận": { field: "IntWaitInvoice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số c.từ thực nhận": { field: "IntCompleteInvoice", format: "{0:#,##0.0}", aggregate: "sum" },
                                "Số c.từ chưa nhận": { field: "IntRemainInvoice", format: "{0:#,##0.0}", aggregate: "sum" },
                            }
                        }
                    },
                    columns: [],
                    rows: [{ name: "OrderCode", title: "Don hang", expand: true }, {name: "ParnerCode"}, {name: "VendorName"}],
                    measures: ["Tấn nhận", "Khối nhận"]
                };

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
                    
                    var parentconfig=$("#repdipivot_pivot_config").parent();
                    var height=parentconfig.height()-10;
                    if(height<300)height=300;

                    var config = $("#repdipivot_pivot_config").data('kendoPivotConfigurator');
                    if (config == null) {
                        $("#repdipivot_pivot_config").kendoPivotConfigurator({
                            dataSource: pivotgrid.dataSource,
                            filterable: true,
                            sortable: false,
                            height:height
                        });
                    }
                    else {
                        config.setDataSource(pivotgrid.dataSource);
                    }
                    
                    // Show the chart
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
        Common.Log("Show chart");
        _REPDIChart.initSettingsData($scope);
        _REPDIChart.showChartWithFilter($scope);
    };

    $scope.REPDIPivot_UpdateChart = function(){
        _REPDIChart.showChartWithFilter(this);
    };

}]);