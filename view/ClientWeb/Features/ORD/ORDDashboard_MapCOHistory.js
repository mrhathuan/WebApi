/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _ORDDashboard_MapCOHistory = {
    URL: {
        Read: 'MAP_SummaryCO_Vehicle_Data',
        Read_Vehicle: 'Dashboard_Tractor_List',
        Read_Customer: 'REP_Customer_Read',
        Master_Data: 'MAP_SummaryCO_Master_Data',
        Master_DataList: 'MAP_SummaryCO_Master_DataList',
        VehiclePosition_Get: 'MAP_Summary_VehiclePosition_Get',
        VehiclePosition_GetList: 'MAP_Summary_VehiclePosition_GetList',
    },
    Data: {
        Vehicle: [],
        TypeOfVehicle: [
            { ID: 1, Name: 'Xe tải' },
            //{ ID: 2, Name: 'Xe container' },
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
        ListPoint: [],
        ListRoute: [],
        ListMaster: [],
        ListColor: ["#F08080", "#CD5C5C", "#66CDAA", "#008B8B", "#4682B4", "#DAA520", "#708090", "#BA55D3", "#008080", "#BDB76B"],
    },
}

angular.module('myapp').controller('ORDDashboard_MapCOHistoryCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile) {
    Common.Log('ORDDashboard_MapCOHistoryCtrl');
    $rootScope.IsLoading = false;

    $scope.IsExpand = true;
    $scope.IsExpandMaster = true;

    $scope.Item = {
        request: '',
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        TypeOfVehicleID: 1,
        VehicleCode: '',
        VehicleID: "",
        Theme: 'Bootstrap',
        Type: 'column',
        MasterID: "",
    }

    $scope.Total = {
        ListData: [],
        ListMarker: [],
        ListMarkerLegend: [],
        Vehicle: 0,
        Romooc: 0,
        c20DC: 0,
        c40DC: 0,
        c40HC: 0,
        Export: 0,
        Import: 0,
        Local: 0,
        Schedule: 0,
    }

    $scope.Master = {
        VehicleCode: "",
        RomoocCode: "",
        c20DC: 0,
        c40DC: 0,
        c40HC: 0,
        KM: 0,
        ATD: "",
        ATA: "",
    };

    $scope.Info = null;

    $scope.cboTypeOfVehicleOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'Name',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_MapCOHistory.Data.TypeOfVehicle,
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

    $scope.cboVehicleOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        placeholder: "Chọn xe",
        suggest: true,
        dataTextField: 'Code',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        }),
        change: function (e) {

        }
    }

    $scope.cboMasterOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        placeholder: "Chọn chuyến",
        suggest: true,
        dataTextField: 'Code',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var masterID = this.value();
            _ORDDashboard_MapCOHistory.Data.ListPoint = [];
            _ORDDashboard_MapCOHistory.Data.ListRoute = [];
            openMapV2.ClearVector("VectorPoint");
            openMapV2.ClearVector("VectorLineActual");
            openMapV2.ClearVector("VectorLinePlan");

            if (masterID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDDashboard_MapCOHistory.URL.Master_Data,
                    data: { masterID: masterID },
                    success: function (res) {
                        $timeout(function () {
                            res.ATA = Common.Date.FromJsonDDMMHM(res.ATA);
                            res.ATD = Common.Date.FromJsonDDMMHM(res.ATD);
                            $scope.Master = res;
                            if (res.ListMarker.length > 0)
                                $scope.DrawPlan(res.ListMarker);
                        }, 10);

                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_MapCOHistory.URL.VehiclePosition_Get,
                            data: { vehicleCode: res.VehicleCode, dtfrom: res.ATD, dtto: res.ATA },
                            success: function (ListPoint) {
                                var itemRoute = { ListPoint: ListPoint };
                                _ORDDashboard_MapCOHistory.Data.ListRoute.push(itemRoute);
                                _ORDDashboard_MapCOHistory.Data.ListPoint = ListPoint;
                                $scope.DrawActualLine();
                                $scope.VehicleMove();
                                $rootScope.IsLoading = false;
                            }
                        });
                    }
                });
            } else {
                $rootScope.IsLoading = true;
                var lstMasterID = [];
                angular.forEach(_ORDDashboard_MapCOHistory.Data.ListMaster, function (v, i) {
                    lstMasterID.push(v.ID);
                });
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDDashboard_MapCOHistory.URL.Master_DataList,
                    data: { lstMasterID: lstMasterID },
                    success: function (res) {
                        $timeout(function () {
                            res.ATA = Common.Date.FromJsonDDMMHM(res.ATA);
                            res.ATD = Common.Date.FromJsonDDMMHM(res.ATD);
                            $scope.Master = res;
                        }, 10);

                        var lstMaster = [];
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_MapCOHistory.URL.VehiclePosition_GetList,
                            data: { lstMaster: _ORDDashboard_MapCOHistory.Data.ListMaster },
                            success: function (ListRoute) {
                                _ORDDashboard_MapCOHistory.Data.ListRoute = ListRoute;
                                $scope.VehicleMove();
                                $rootScope.IsLoading = false;
                            }
                        });
                    }
                });
            }
        }
    }

    $scope.cboThemeOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0, placeholder: 'Chọn theme',
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_MapCOHistory.Data.Theme,
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
            data: _ORDDashboard_MapCOHistory.Data.ChartType,
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

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDDashboard_MapCOHistory.URL.Read_Vehicle,
        data: {},
        success: function (res) {
            _ORDDashboard_MapCOHistory.Data.Vehicle = res;
            $timeout(function () {
                $scope.cboVehicle.setDataSource(res);
            }, 10);
        }
    });


    $scope.Marker_Click = function (o, feature) {
        if (Common.HasValue(o)) {
            $timeout(function () {
                $scope.Info = o;
            }, 10);
        } else {
            openMapV2.Close();
            $scope.VehicleMove();
        }
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
            },
            {
                Name: 'VectorLinePlan',
                zIndex: 98
            },
            {
                Name: 'VectorLineActual',
                zIndex: 99
            },
            {
                Name: 'VectorVehicleMove',
                zIndex: 101
            },
            ]
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
    }

    $scope.DrawRoute = function (from, to) {
        openMapV2.ClearVector("VectorLinePlan");
        openMapV2.NewRoute(from, to, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorLinePlan", null, function () {
            openMapV2.FitBound("VectorLinePlan", 15);
        });
    }

    $scope.DrawPlan = function (ListMarker) {
        openMapV2.ClearVector("VectorPoint");
        Common.Data.Each(ListMarker, function (o) {
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewEmpty(openMapV2.NewImage.Color.Dark)), 1, '' + o.SortOrder, '#cecece', true);

            if (o.TypeOfLocation == 1)
                icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewEmpty(openMapV2.NewImage.Color.Orange)), 1, '' + o.SortOrder, '#cecece', true);
            if (o.TypeOfLocation == 2)
                icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewEmpty(openMapV2.NewImage.Color.Blue)), 1, '' + o.SortOrder, '#cecece', true);

            openMapV2.NewMarker(o.Lat, o.Lng, "", o.Name, icon, o, "VectorPoint");
        });

        for (var i = 0; i < ListMarker.length - 1; i++) {
            var l1 = ListMarker[i];
            var l2 = ListMarker[i + 1];
            $scope.DrawRoute([l1.Lng, l1.Lat], [l2.Lng, l2.Lat]);
        }
    };

    $scope.DrawActualLine = function () {
        openMapV2.ClearVector("VectorLineActual");

        for (var i = 0; i < _ORDDashboard_MapCOHistory.Data.ListPoint.length - 1; i++) {
            var l1 = _ORDDashboard_MapCOHistory.Data.ListPoint[i];
            var l2 = _ORDDashboard_MapCOHistory.Data.ListPoint[i + 1];

            var style = openMapV2.NewStyle.Line(5, 'rgba(150,50,50, 0.7)', [15, 10], "", '#fff');
            if (Common.HasValue(l1) && Common.HasValue(l2)) {
                var x = openMapV2.NewPolyLine([openMapV2.NewPoint(l1.Lat, l1.Lng), openMapV2.NewPoint(l2.Lat, l2.Lng)], 1, "", style, {}, "VectorLineActual")
            }
        }
    }

    $scope.VehicleMove = function ($event) {
        if (Common.HasValue($event))
            $event.preventDefault();

        openMapV2.ClearVector("VectorVehicleMove");

        angular.forEach(_ORDDashboard_MapCOHistory.Data.ListRoute, function (item, index) {
            if (item.ListPoint.length > 0) {
                var ListPoint = [];
                var iColor = index % 9;
                for (var i = 0; i < item.ListPoint.length - 1; i++) {
                    var l1 = item.ListPoint[i];
                    var l2 = item.ListPoint[i + 1];

                    var style = openMapV2.NewStyle.Line(3, _ORDDashboard_MapCOHistory.Data.ListColor[iColor], [15, 10], "", '#fff');
                    if (Common.HasValue(l1) && Common.HasValue(l2)) {
                        var x = openMapV2.NewPolyLine([openMapV2.NewPoint(l1.Lat, l1.Lng), openMapV2.NewPoint(l2.Lat, l2.Lng)], 1, "", style, {}, "VectorVehicleMove")
                        var y = openMapV2.NewRouteDirection("Code" + index, x, 100, 'VectorVehicleMove');
                        y.Init();
                        ListPoint.push(y);
                    }
                }

                if (ListPoint.length > 0) {
                    var fn = function (p) {
                        if (p < ListPoint.length) {
                            obj = ListPoint[p];
                            obj.Start(fn, p + 1, ListPoint);
                        }
                    }

                    var idx = 0;
                    var obj = ListPoint[idx];
                    obj.Start(fn, idx + 1, ListPoint);
                }
            }
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
            case "Quantity":
                $scope.ChartQuantity_Click(null, true);
                break;
            case "Schedule":
                $scope.ChartSchedule_Click(null, true);
                break;
            case "QuantityByDay":
                $scope.ChartQuantityDetail_Click(null, true);
                break;
            case "ScheduleByDay":
                $scope.ChartScheduleDetail_Click(null, true);
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

    $scope.PanelMaster_Click = function ($event) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $scope.IsExpandMaster = !$scope.IsExpandMaster;

        if ($scope.IsExpandMaster) {
            $($event.currentTarget.children).removeClass("fa-expand");
            $($event.currentTarget.children).addClass("fa-compress");
        }
        else {
            $($event.currentTarget.children).removeClass("fa-compress");
            $($event.currentTarget.children).addClass("fa-expand");
        }
    };
    
    $scope.ChartQuantity_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Sản lượng";
        $scope.ReportCode = "Quantity";

        var series = [];
        var valueAxis = [];
        var categoryAxis = [];
        series.push({ name: 'Số lượng container', data: [$scope.Total.c20DC, $scope.Total.c40DC, $scope.Total.c40HC] });
        categoryAxis.push("20DC");
        categoryAxis.push("40DC");
        categoryAxis.push("40HC");

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
        series.push({ name: 'Chuyến', data: [$scope.Total.Export, $scope.Total.Import, $scope.Total.Local] });
        categoryAxis.push("Xuất khẩu");
        categoryAxis.push("Nhập khẩu");
        categoryAxis.push("Nội địa");

        $scope.InitChart($scope.ReportName, series, valueAxis, categoryAxis, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };


    $scope.ChartQuantityDetail_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        // Init data
        $scope.ReportName = "Sản lượng theo ngày";
        $scope.ReportCode = "QuantityByDay";

        var data = [];
        var dtfrom = new Date($scope.Item.DateFrom.getFullYear(), $scope.Item.DateFrom.getMonth(), $scope.Item.DateFrom.getDate());
        var dtto = new Date($scope.Item.DateTo.getFullYear(), $scope.Item.DateTo.getMonth(), $scope.Item.DateTo.getDate());
        while (dtfrom <= dtto) {
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, c20DC: 0, c40DC: 0, c40HC: 0 };
            dtfrom = dtfrom.addDays(1);
        }

        var series = [];
        series.push({ name: '20DC', field: "c20DC" });
        series.push({ name: '40DC', field: "c40DC" });
        series.push({ name: '40HC', field: "c40HC" });

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            data[strDate].c20DC += v.c20DC;
            data[strDate].c40DC += v.c40DC;
            data[strDate].c40HC += v.c40HC;
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
            data[Common.Date.FromJsonDDMMYY(dtfrom)] = { Date: dtfrom, Export: 0, Import: 0, Local: 0 };
            dtfrom = dtfrom.addDays(1);
        }

        var series = [];
        series.push({ name: 'Xuất khẩu', field: "Export" });
        series.push({ name: 'Nhập khẩu', field: "Import" });
        series.push({ name: 'Nội địa', field: "Local" });

        angular.forEach($scope.Total.ListData, function (v, i) {
            var strDate = Common.Date.FromJsonDDMMYY(v.DateConfig);
            if (Common.HasValue(data[strDate])) {
                data[strDate].Export += v.Export;
                data[strDate].Import += v.Import;
                data[strDate].Local += v.Local;
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
            method: _ORDDashboard_MapCOHistory.URL.Read,
            data: { request: "", dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, typeofvehicleID: $scope.Item.TypeOfVehicleID, VehicleID: $scope.Item.VehicleID },
            success: function (res) {
                $timeout(function () {
                    $scope.Total = res;
                    $scope.ReloadMap();
                    $scope.Master = null;
                    _ORDDashboard_MapCOHistory.Data.ListMaster = res.ListMaster;
                    var lstMaster = [];
                    if (res.ListMaster.length > 0) {
                        lstMaster = $.extend(true, [], res.ListMaster);
                        lstMaster.splice(0, 0, { ID: -1, Code: 'Tất cả' });
                    }
                    $scope.cboMaster.setDataSource(lstMaster);
                    $rootScope.IsLoading = false;
                }, 10);
            }
        });
    }

    $scope.MapHistory_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.ORDDashboard.MapCO");
    }

    $scope.Dashboard_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.ORDDashboard.Index");
    }
}]);