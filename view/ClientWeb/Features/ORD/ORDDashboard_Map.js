/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _ORDDashboard_Map = {
    URL: {
        Read: 'MAP_Summary_Data',
        Read_Customer: 'REP_Customer_Read',
    },
    Data: {
        TypeOfLocation:[
            { ID: 1, Name: "Điểm giao" },
            { ID: 2, Name: "Điểm nhận" },
        ],
        Province: [],
        District: [],
        Theme: [
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
        ChartType: [
               {
                   Code: 'column', Name: 'Column'
               },
               {
                   Code: 'line', Name: 'Line'
               },
        ],
    },
}

angular.module('myapp').controller('ORDDashboard_MapCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile) {
    Common.Log('ORDDashboard_MapCtrl');
    $rootScope.IsLoading = false;

    $scope.IsExpand = true;

    $scope.Item = {
        lstCustomerID: [],
        request: '',
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        TypeOfLocationID: 1,
        ProvinceID: -1,
        DistrictID: -1,
        VehicleCode: '',
        VehicleID: -1,
        Theme: 'Bootstrap',
        Type: 'column',
    }

    $scope.Total = {
        ListData: [],
        ListMarker: [],
        ListMarkerLegend: [],
        TonMax: 0,
        CBMMax: 0,
        TonTranfer: 0,
        CBMTranfer: 0,
        TonReturn: 0,
        CBMReturn: 0,
        TonLoading: 0,
        CBMLoading: 0,
        TonUnLoading: 0,
        CBMUnLoading: 0,
        TimeLoading: 0,
        TimeUnLoading: 0,
        Schedule: 0,
        ScheduleEmpty: 0,
        KM: 0,
        KMAverage: 0,
        Vehicle: 0,
    }

    $scope.Info = null;

    $scope.cboTypeOfLocationOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'Name',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Map.Data.TypeOfLocation,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            
        }
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains',
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID', placeholder: 'Chọn khách hàng',
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

        },
    };

    $scope.cboProvinceOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ProvinceName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.DistrictID = -1;
            }
            else {
                $scope.Item.ProvinceID = -1;
                $scope.Item.DistrictID = -1;
            }
            $scope.ReloadCombobox();
        }
    }

    $scope.cboDistrictOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'DistrictName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        change: function (e) {

        }
    }

    $scope.cboVehicleOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        placeholder: "Chọn xe",
        suggest: true,
        dataTextField: 'VehicleCode',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    VehicleCode: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            
        }
    }

    $scope.cboThemeOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0, placeholder: 'Chọn theme',
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Map.Data.Theme,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'string' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function () {
            $scope.Item.Theme = this.value();
            $scope.ReDrawChart();
        },
    }

    $scope.cboTypeOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Name', dataValueField: 'Code', minLength: 3, index: 0, placeholder: 'Chọn chart',
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Map.Data.ChartType,
            model: {
                id: 'ID',
                fields: {
                    Code: { type: 'string' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function () {
            $scope.Item.Type = this.value();
            $scope.ReDrawChart();
        },
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (res) {
            _ORDDashboard_Map.Data.Province = [];
            
            if (res[0].ID != -1)
                res.splice(0, 0, { ID: -1, ProvinceName: 'Tất cả tỉnh' });

            _ORDDashboard_Map.Data.Province = res;
            $scope.cboProvinceOptions.dataSource.data(res);
        }
    });

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _ORDDashboard_Map.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

    $scope.ReloadCombobox = function () {
        Common.Log("ReloadCombobox");
        try {
            var provinceID = $scope.Item.ProvinceID;
            var districtID = $scope.Item.DistrictID;

            var data = [];
            if (provinceID > 0)
                data = _ORDDashboard_Map.Data.District[provinceID];
            $scope.cboDistrictOptions.dataSource.data(data);
        }
        catch (e) { }
    }

    

    $scope.Marker_Click = function (o, feature) {
        $timeout(function () {
            $scope.Info = o;
        }, 10);
    };

    $scope.InfoWindowClose_Click = function ($event) {
        $event.preventDefault();

        openMapV2.Close();
    };

    $scope.CreateMap = function () {
        Common.Log("CreateMap");

        Common.Log(openMapV2.hasMap);
        openMapV2.hasMap = true;
        openMapV2.Init({
            Element: 'dashboardMap_map',
            Tooltip_Show: true,
            Tooltip_Element: 'dashboardMap_tooltip',
            InfoWin_Show: true,
            InfoWin_Width: 200,
            InfoWin_Height: 190,
            InfoWin_Element: 'dashboardMap_infoWindow',
            MapBoxLightDefault: false,
            ClickMarker: function (o, i) {
                $scope.Marker_Click(o, i);
            },
            ClickMap: function (o) {
                openMapV2.Close();
            },
            DefinedLayer: [{
                Name: 'VectorPoint',
                zIndex: 100
            }, {
                Name: 'VectorPolygon',
                zIndex: 99
            }]
        });

        $scope.ReloadMap();
    }



    $timeout(function () {
        $scope.CreateMap();
        $scope.RefreshMap_Click();
    }, 500);

    $scope.ReloadMap = function () {
        Common.Log("ReloadMap");

        openMapV2.Close(); //Hide info window
        openMapV2.ClearVector("VectorPoint");
        openMapV2.ClearVector("VectorPolygon");

        if ($scope.Item.ProvinceID > 0) {
            var str = '';
            var style = openMapV2.NewStyle.Polygon(1);
            if ($scope.Item.ProvinceID < 10)
                str = '0';
            var polygon = openMapV2.NewProvincePolygon(str + $scope.Item.ProvinceID, "", "VectorPolygon");
            polygon.Init(style, $scope.DrawMarker);

            var item = $scope.cboProvince.dataItem();
            if (item.Lat > 0 && item.Lng > 0)
                openMapV2.Center(item.Lat, item.Lng, 9);
        } else {
            $scope.DrawMarker();
        }
    }

    $scope.DrawMarker = function () {
        Common.Data.Each($scope.Total.ListMarker, function (o) {
            var icon = openMapV2.NewStyle.Circle(o.Radius, o.Color);
            openMapV2.NewMarker(o.Lat, o.Lng, "", o.Name, icon, o, "VectorPoint");
        });
    }

    $scope.InitChart = function (title, series, valueAxis, categoryAxis, chart_theme, chart_type) {
        var chart = $("#REPMap_Chart").data("kendoChart");
        // distroy existing chart
        if (chart != null) {
            chart.destroy();
            $('#REPMap_Chart').empty();
        }

        $("#REPMap_Chart").kendoChart({
            title: {
                text: title,
            },
            series: series,
            seriesDefaults: {
                type: chart_type
            },
            legend: {
                position: "bottom"
            },
            valueAxis: {
                line: {
                    visible: false
                },
                minorGridLines: {
                    visible: false
                },
                labels: {
                    rotation: "auto"
                }
            },
            categoryAxis: {
                categories: categoryAxis,
                majorGridLines: {
                    visible: false
                }
            },
            tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" },
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
            dataBound: function (e) {
                var view = e.sender.dataSource.view();
                //$(".overlay").toggle(view.length === 0);
            }
        });

        chart = $("#REPMap_Chart").data("kendoChart");
        chart.setOptions({ theme: chart_theme });
        chart.refresh();
        $timeout(function () {
            chart.resize();
        }, 10);
    };

    $scope.InitChartDetail = function (title, data, series, chart_theme, chart_type) {
        var chart = $("#REPMap_Chart").data("kendoChart");
        // distroy existing chart
        if (chart != null) {
            chart.destroy();
            $('#REPMap_Chart').empty();
        }
        
        $("#REPMap_Chart").kendoChart({
            title: {
                text: title,
            },
            dataSource: {
                data: data,
                sort: { field: "Date", dir: "asc" },
            },
            series: series,
            seriesDefaults: {
                type: chart_type
            },
            legend: {
                position: "bottom"
            },
            valueAxis: {
                line: {
                    visible: false
                },
                minorGridLines: {
                    visible: false
                },
                labels: {
                    rotation: "auto"
                }
            },
            categoryAxis: {
                field: "Date", type: "category",
                majorGridLines: { visible: false },
                baseUnit: 'days',
                labels: {
                    rotation: "auto",
                    format: "dd/MM"
                },
                min: 0
            },
            tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" },
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
            dataBound: function (e) {
                
            }
        });

        chart = $("#REPMap_Chart").data("kendoChart");
        chart.setOptions({ theme: chart_theme });
        chart.refresh();
        $timeout(function () {
            chart.resize();
        }, 10);
    };

    $scope.ReDrawChart = function () {
        switch ($scope.ReportCode) {
            case "TonCBM":
                $scope.ChartTonCBM_Click(null, true);
                break;
            case "Schedule":
                $scope.ChartSchedule_Click(null, true);
                break;
            case "Loading":
                $scope.ChartLoading_Click(null, true);
                break;
            case "UnLoading":
                $scope.ChartUnLoading_Click(null, true);
                break;
            case "TonCBMByDay":
                $scope.ChartTonCBMDetail_Click(null, true);
                break;
            case "ScheduleByDay":
                $scope.ChartScheduleDetail_Click(null, true);
                break;
            case "LoadingByDay":
                $scope.ChartLoadingDetail_Click(null, true);
                break;
            case "UnLoadingByDay":
                $scope.ChartUnLoadingDetail_Click(null, true);
                break;
            case "LoadingTimeByDay":
                $scope.ChartLoadingTimeDetail_Click(null, true);
                break;
            case "UnLoadingTimeByDay":
                $scope.ChartUnLoadingTimeDetail_Click(null, true);
                break;
        }
    }

    $scope.Panel_Click = function ($event) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $scope.IsExpand = !$scope.IsExpand;

        if ($scope.IsExpand) {
            $($event.currentTarget.children).removeClass("fa-expand");
            $($event.currentTarget.children).addClass("fa-compress");
        }
        else {
            $($event.currentTarget.children).removeClass("fa-compress");
            $($event.currentTarget.children).addClass("fa-expand");
        }
    };


    $scope.ChartTonCBM_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Sản lượng";
        $scope.ReportCode = "TonCBM";

        var series = [];
        var valueAxis = [];
        var categoryAxis = [];
        series.push({ name: 'Tấn', data: [$scope.Total.TonTranfer, $scope.Total.TonReturn] });
        series.push({ name: 'Khối', data: [$scope.Total.CBMTranfer, $scope.Total.CBMReturn] });
        categoryAxis.push("Đi");
        categoryAxis.push("Về");

        $scope.InitChart($scope.ReportName, series, valueAxis, categoryAxis, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartSchedule_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Tổng chuyến";
        $scope.ReportCode = "Schedule";

        var series = [];
        var valueAxis = [];
        var categoryAxis = [];
        series.push({ name: 'Chuyến', data: [$scope.Total.Schedule, $scope.Total.ScheduleEmpty] });
        categoryAxis.push("Đi có hàng");
        categoryAxis.push("Về có hàng");

        $scope.InitChart($scope.ReportName, series, valueAxis, categoryAxis, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartLoading_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Bốc xếp lên";
        $scope.ReportCode = "Loading";

        var series = [];
        var valueAxis = [];
        var categoryAxis = [];
        series.push({ name: 'Tấn', data: [$scope.Total.TonLoading] });
        series.push({ name: 'Khối', data: [$scope.Total.CBMLoading] });

        $scope.InitChart($scope.ReportName, series, valueAxis, categoryAxis, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartUnLoading_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Bốc xếp xuống";
        $scope.ReportCode = "UnLoading";

        var series = [];
        var valueAxis = [];
        var categoryAxis = [];
        series.push({ name: 'Tấn', data: [$scope.Total.TonUnLoading] });
        series.push({ name: 'Khối', data: [$scope.Total.CBMUnLoading] });

        $scope.InitChart($scope.ReportName, series, valueAxis, categoryAxis, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    
    $scope.ChartTonCBMDetail_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Sản lượng theo ngày";
        $scope.ReportCode = "TonCBMByDay";

        var data = [];
        var dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        var dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());
        while (dtfrom <= dtto) {
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, Ton: 0, CBM: 0 };
            dtfrom = dtfrom.addDays(1);
        }

        var series = [];
        series.push({ name: 'Tấn', field: "Ton" });
        series.push({ name: 'Khối', field: "CBM" });

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            data[strDate].Ton += v.TonTranfer;
            data[strDate].CBM += v.CBMTranfer;
        });

        dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());

        var dataChart = [];
        while (dtfrom <= dtto) {
            dataChart.push(data[Common.Date.FromJsonDDMMYY(dtfrom)]);
            dtfrom = dtfrom.addDays(1);
        }
        
        // Draw chart
        $scope.InitChartDetail($scope.ReportName, dataChart, series, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartScheduleDetail_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Chuyến theo ngày";
        $scope.ReportCode = "ScheduleByDay";

        var data = [];
        var dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        var dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());
        while (dtfrom <= dtto) {
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, Schedule: 0, ScheduleEmpty: 0, Data: [], DataEmpty: [] };
            dtfrom = dtfrom.addDays(1);
        }

        var series = [];
        series.push({ name: 'Đi có hàng', field: "Schedule" });
        series.push({ name: 'Về có hàng', field: "ScheduleEmpty" });

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            if (Common.HasValue(data[strDate]))
                if (data[strDate].Data.indexOf(v.DITOMasterID) < 0) {
                    data[strDate].Data.push(v.DITOMasterID);
                    data[strDate].Schedule += 1;
                }

            if (v.IsReturn) {
                if (data[strDate].DataEmpty.indexOf(v.DITOMasterID) < 0) {
                    data[strDate].DataEmpty.push(v.DITOMasterID);
                    data[strDate].ScheduleEmpty += 1;
                }
            }
        });

        dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());

        var dataChart = [];
        while (dtfrom <= dtto) {
            dataChart.push(data[Common.Date.FromJsonDDMMYY(dtfrom)]);
            dtfrom = dtfrom.addDays(1);
        }

        // Draw chart
        $scope.InitChartDetail($scope.ReportName, dataChart, series, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartLoadingDetail_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Bốc xếp lên theo ngày";
        $scope.ReportCode = "LoadingByDay";

        var data = [];
        var dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        var dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());
        while (dtfrom <= dtto) {
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, Ton: 0, CBM: 0 };
            dtfrom = dtfrom.addDays(1);
        }

        var series = [];
        series.push({ name: 'Tấn', field: "Ton" });
        series.push({ name: 'Khối', field: "CBM" });

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            data[strDate].Ton += v.TonLoading;
            data[strDate].CBM += v.CBMLoading;
        });

        dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());

        var dataChart = [];
        while (dtfrom <= dtto) {
            dataChart.push(data[Common.Date.FromJsonDDMMYY(dtfrom)]);
            dtfrom = dtfrom.addDays(1);
        }

        // Draw chart
        $scope.InitChartDetail($scope.ReportName, dataChart, series, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartLoadingTimeDetail_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Thời gian bốc xếp lên theo ngày";
        $scope.ReportCode = "LoadingTimeByDay";

        var data = [];
        var dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        var dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());
        while (dtfrom <= dtto) {
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, Hour: 0 };
            dtfrom = dtfrom.addDays(1);
        }

        var series = [];
        series.push({ name: 'Giờ', field: "Hour" });

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            data[strDate].Hour += v.TimeLoading;
        });

        dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());

        var dataChart = [];
        while (dtfrom <= dtto) {
            dataChart.push(data[Common.Date.FromJsonDDMMYY(dtfrom)]);
            dtfrom = dtfrom.addDays(1);
        }

        // Draw chart
        $scope.InitChartDetail($scope.ReportName, dataChart, series, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartUnLoadingDetail_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Bốc xếp xuống theo ngày";
        $scope.ReportCode = "UnLoadingByDay";

        var data = [];
        var dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        var dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());
        while (dtfrom <= dtto) {
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, Ton: 0, CBM: 0 };
            dtfrom = dtfrom.addDays(1);
        }

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            data[strDate].Ton += v.TonUnLoading;
            data[strDate].CBM += v.CBMUnLoading;
        });

        var series = [];
        series.push({ name: 'Tấn', field: "Ton" });
        series.push({ name: 'Khối', field: "CBM" });

        dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());

        var dataChart = [];
        while (dtfrom <= dtto) {
            dataChart.push(data[Common.Date.FromJsonDDMMYY(dtfrom)]);
            dtfrom = dtfrom.addDays(1);
        }
        
        // Draw chart
        $scope.InitChartDetail($scope.ReportName, dataChart, series, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.ChartUnLoadingTimeDetail_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Thời gian bốc xếp xuống theo ngày";
        $scope.ReportCode = "UnLoadingTimeByDay";

        var data = [];
        var dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        var dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());
        while (dtfrom <= dtto) {
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, Hour: 0 };
            dtfrom = dtfrom.addDays(1);
        }

        var series = [];
        series.push({ name: 'Giờ', field: "Hour" });

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            data[strDate].Hour += v.TimeUnLoading;
        });

        dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());

        var dataChart = [];
        while (dtfrom <= dtto) {
            dataChart.push(data[Common.Date.FromJsonDDMMYY(dtfrom)]);
            dtfrom = dtfrom.addDays(1);
        }

        // Draw chart
        $scope.InitChartDetail($scope.ReportName, dataChart, series, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };


    $scope.PDF_Click = function ($event) {
        $event.preventDefault();

        var chart = $("#REPMap_Chart").getKendoChart();
        chart.exportPDF({ paperSize: "auto", margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" } }).done(function (data) {
            kendo.saveAs({
                dataURI: data,
                fileName: "chart.pdf",
            });
        });
    };

    $scope.IMG_Click = function ($event) {
        $event.preventDefault();

        var chart = $("#REPMap_Chart").getKendoChart();
        chart.exportImage().done(function (data) {
            kendo.saveAs({
                dataURI: data,
                fileName: "chart.png",
            });
        });
    };

    $scope.SVG_Click = function ($event) {
        $event.preventDefault();

        var chart = $("#REPMap_Chart").getKendoChart();
        chart.exportSVG().done(function (data) {
            kendo.saveAs({
                dataURI: data,
                fileName: "chart.svg",
            });
        });
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.RefreshMap_Click = function ($event) {
        if (Common.HasValue($event))
            $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDDashboard_Map.URL.Read,
            data: { request: "", lstCustomerID: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, provinceID: $scope.Item.ProvinceID, typeoflocationID: $scope.Item.TypeOfLocationID },
            success: function (res) {
                $timeout(function () {
                    $scope.Total = res;
                    $scope.ReloadMap();
                    $rootScope.IsLoading = false;
                }, 10);
            }
        });
    }

    $scope.MapHistory_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.ORDDashboard.MapHistory");
    }

    $scope.Dashboard_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.ORDDashboard.Index");
    }
}]);