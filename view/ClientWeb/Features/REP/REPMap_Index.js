/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPMap = {
    URL: {
        Read: 'REP_CartoDB_List',
        Read_Customer: 'REP_Customer_Read',
        Read_Vehicle: 'REP_CartoDB_Vehicle_List',
        Read_VehicleRoute: 'REP_CartoDB_VehicleRoute_List',
    },
    Data: {
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
        ProvinceCode: {
            '01': 793923, '02': 794014, '03': 793918, '04': 793912, '05': 687794, '06': 508457, '07': 687792, '08': 793922, '09': 15143, '10': 795293,
            '11': 15144, '12': 508527, '13': 15138, '14': 15139, '15': 15132, '16': 508524, '17': 793921, '18': 793920, '19': 793919, '20': 0,
            '21': 508523, '22': 508521, '23': 687791, '24': 793917, '25': 793916, '26': 687787, '27': 687785, '28': 687780, '29': 687779, '30': 687778,
            '31': 687776, '32': 687775, '33': 687774, '34': 793911, '35': 793909, '36': 793904, '37': 793908, '38': 793905, '39': 687768, '40': 508498,
            '41': 793907, '42': 508500, '43': 793913, '44': 793925, '45': 793906, '46': 793914, '47': 687796, '48': 687798, '49': 793902, '50': 793899,
            '51': 793898, '52': 793924, '53': 793901, '54': 793897, '55': 793895, '56': 687762, '57': 687761, '58': 687758, '59': 687757, '60': 687756,
            '61': 793894, '62': 364265, '63': 508499, '64': 508488
        },
        VehicleCSS: [
          'Map {',
          '-torque-time-attribute: "createddate";',
          '-torque-aggregation-function: "count(cartodb_id)";',
          '-torque-frame-count: 760;',
          '-torque-animation-duration: 10;',
          '-torque-resolution: 2',
          '}',
          '#layer {',
          '  marker-width: 4;',
          '  marker-fill-opacity: 0.8;',
          '  marker-fill: red; ',
          '  comp-op: "lighten";',
          '}'
        ].join('\n'),
    },
    SQLMain_Query: "Select * from tms",
    SQLMain_Delete: "Delete from tms",
    SQLMain_Insert: "Insert into tms (id,ditomasterid,ditomastercode,km,customerid,customercode,orderid,ordercode,ordergroupproductid,socode,dncode,requestdate,etd,eta,dateconfig,tontranfer,cbmtranfer,quantitytranfer,tonreturn,cbmreturn,quantityreturn,ditogroupproductstatuspodid,stockcode,stockname,stockaddress,stockprovinceid,stockdistrictid,distributorcode,distributorname,distributorprovinceid,distributordistrictid,distributoraddress,distributorlat,distributorlng,vehicleid,vehicleton,vehiclecbm,domain,tonloading,cbmloading,tonunloading,cbmunloading,timeloading,timeunloading,isreturn) values (",
    SQLMain_InsertGeo: "Insert into tms (the_geom,id,ditomasterid,ditomastercode,km,customerid,customercode,orderid,ordercode,ordergroupproductid,socode,dncode,requestdate,etd,eta,dateconfig,tontranfer,cbmtranfer,quantitytranfer,tonreturn,cbmreturn,quantityreturn,ditogroupproductstatuspodid,stockcode,stockname,stockaddress,stockprovinceid,stockdistrictid,distributorcode,distributorname,distributorprovinceid,distributordistrictid,distributoraddress,distributorlat,distributorlng,vehicleid,vehicleton,vehiclecbm,domain,tonloading,cbmloading,tonunloading,cbmunloading,timeloading,timeunloading,isreturn) values (",

    SQLProvince_Query: "Select * from tms_province",
    SQLProvince_Delete: "Delete from tms_province",
    SQLProvince_Insert: "Insert into tms_province (the_geom,provinceid,provincecode,provincename,provincelat,provincelng) values (",

    SQLVehicle_Query: "Select * from tms_vehicle",
    SQLVehicle_Delete: "Delete from tms_vehicle",
    SQLVehicle_Insert: "Insert into tms_vehicle (the_geom,vehiclecode,lat,lng,domain,dateconfig,createddate) values (",
    API: {
        viaroute: '//router.project-osrm.org/viaroute?loc=',
        geojson: 'http://global.mapit.mysociety.org/area/',
        suffix: '.geojson?simplify_tolerance=0.0001',
        instructions: '&instructions=true',
        Layer: 'https://binhkurt.cartodb.com/api/v2/viz/c0e036c8-2d68-11e6-b7d0-0e674067d321/viz.json',
        URL: 'https://binhkurt.cartodb.com/api/v2/sql?q=',
        APIKey: '&api_key=a40fe191f16d5e17f1686dbefe1f2b38b8e346ff',
        APIKeyOnly: 'a40fe191f16d5e17f1686dbefe1f2b38b8e346ff',
        URLOnly: 'https://binhkurt.cartodb.com/api/v2/sql',
        UserName: 'binhkurt',
    },
    HostName: "",
}

angular.module('myapp').controller('REPMap_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap, $compile) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPMap_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    if (Common.Services.host == "localhost")
        Common.Services.host = "tmsdemo";

    _REPMap.HostName = Common.Services.host + "_" + Common.Auth.Item.SYSCustomerID;
    //_REPMap.HostName = "vinafco" + "_" + Common.Auth.Item.SYSCustomerID;

    $scope.IsExpand = true;
    $scope.Map = {};
    $scope.Sublayers = [];
    $scope.TorqueLayer = {};

    $scope.LayerActions = {
        refresh: function () {
            var qWhere = $scope.GenSQLWhere();
            if ($scope.Item.ProvinceID > 0)
                $scope.Sublayers[0].setSQL(_REPMap.SQLMain_Query + qWhere + " and distributorprovinceid = " + $scope.Item.ProvinceID);
            else {
                $scope.Sublayers[0].setSQL(_REPMap.SQLMain_Query + qWhere);
            }

            $scope.Sublayers[1].setSQL(_REPMap.SQLProvince_Query  + " where provinceid = " + $scope.Item.ProvinceID);
            return true;
        },
    };

    $scope.Item = {
        lstCustomerID: [],
        request: '',
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        ProvinceID: -1,
        DistrictID: -1,
        VehicleCode: '',
        VehicleID: -1,
        Theme: 'Bootstrap',
        Type: 'column',
    }

    $scope.Total = {
        TonMax: 0,
        CBMMax: 0,
        TonTranfer: 0,
        CBMTranfer: 0,
        TonReturn: 0,
        CBMReturn: 0,
        Schedule: 0,
        ScheduleEmpty: 0,
        KM: 0,
        KMAverage: 0,
        Vehicle: 0,
        TonLoading: 0,
        CBMLoading: 0,
        TimeLoading: 0,
        TonUnLoading: 0,
        CBMUnLoading: 0,
        TimeUnLoading: 0,
        Data: [],
        DataTonCBM: [],
        DataSchedule: [],
    };

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
            data: _REPMap.Data.Theme,
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
            data: _REPMap.Data.ChartType,
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
            _REPMap.Data.Province = [];
            
            if (res[0].ID != -1)
                res.splice(0, 0, { ID: -1, ProvinceName: 'Tất cả tỉnh' });

            _REPMap.Data.Province = res;
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

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPMAP,
            event: $event,
            current: $state.current
        });
    };

    $scope.ReloadCombobox = function () {
        Common.Log("ReloadCombobox");
        try {
            var provinceID = $scope.Item.ProvinceID;
            var districtID = $scope.Item.DistrictID;

            var data = [];
            if (provinceID > 0)
                data = _REPMap.Data.District[provinceID];
            $scope.cboDistrictOptions.dataSource.data(data);
        }
        catch (e) { }
    }

    $scope.GenSQLWhere = function () {
        var qWhere = " where domain = '" + _REPMap.HostName + "' and (dateconfig between '" + Common.Date.FromJsonYYMMDD($scope.Item.DateFrom) + "' and '" + Common.Date.FromJsonYYMMDD($scope.Item.DateTo) + "')";
        if ($scope.Item.lstCustomerID.length > 0)
            qWhere += " and customerid in (" + $scope.Item.lstCustomerID.toString() + ")";

        return qWhere;
    }

    $scope.InitMap = function () {
        cartodb.createVis('cartoDB_map', _REPMap.API.Layer, {
            shareable: false,
            title: false,
            description: true,
            search: false,
            legends: true,
            tiles_loader: true,
            center_lat: 15.5384,
            center_lon: 107.9517,
            zoom: 6
        })
       .done(function (vis, layers) {
           // layer 0 is the base layer, layer 1 is cartodb layer
           // setInteraction is disabled by default
           // you can get the native map to work with it
           $scope.Map = vis.getNativeMap();

           var subLayerOptions1 = {
               sql: "SELECT * FROM tms" + $scope.GenSQLWhere(),
           };

           var subLayerOptions2 = {
               sql: "SELECT * FROM tms_province where provinceid = " + $scope.Item.ProvinceID,
           };

           var sublayer1 = layers[1].getSubLayer(0);
           var sublayer2 = layers[1].getSubLayer(1);
           
           sublayer1.set(subLayerOptions1);
           sublayer2.set(subLayerOptions2);
           
           $scope.Sublayers.push(sublayer1);
           $scope.Sublayers.push(sublayer2);

           $scope.RefreshMap_Click();
       })
       .error(function (err) {
           console.log(err);
       });

        //$scope.Map = new L.Map('cartoDB_map', {
        //    center: [15.5384, 107.9517],
        //    zoom: 6
        //});

        //L.tileLayer('http://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}.png', {
        //    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, &copy; <a href="http://cartodb.com/attributions">CartoDB</a>'
        //}).addTo($scope.Map);

        //cartodb.createLayer($scope.Map, {
        //    type: "torque",
        //    options: {
        //        query: "select * from tms_vehicle as torque_category",
        //        user_name: _REPMap.API.UserName,
        //        table_name: "tms_vehicle",
        //        cartocss: _REPMap.Data.VehicleCSS,
        //        time_slider: false
        //    }
        //}).addTo($scope.Map)
        //    .done(function (layer) {
        //        layer.play();
        //    });
    };

    $scope.InitMap();

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
                    format: "d/M"
                },
                min: 0, max: 7
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

        angular.forEach($scope.Total.Data, function (v, i) {
            var strDate = v.dateconfig.substr(8, 2) + "/" + v.dateconfig.substr(5, 2) + "/" + v.dateconfig.substr(0, 4);
            data[strDate].Ton += v.tontranfer;
            data[strDate].CBM += v.cbmtranfer;
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

        angular.forEach($scope.Total.Data, function (v, i) {
            var strDate = v.dateconfig.substr(8, 2) + "/" + v.dateconfig.substr(5, 2) + "/" + v.dateconfig.substr(0, 4);
            if (Common.HasValue(data[strDate]))
                if (data[strDate].Data.indexOf(v.ditomasterid) < 0) {
                    data[strDate].Data.push(v.ditomasterid);
                    data[strDate].Schedule += 1;
                }

            if (v.isreturn) {
                if (data[strDate].DataEmpty.indexOf(v.ditomasterid) < 0) {
                    data[strDate].DataEmpty.push(v.ditomasterid);
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

        angular.forEach($scope.Total.Data, function (v, i) {
            var strDate = v.dateconfig.substr(8, 2) + "/" + v.dateconfig.substr(5, 2) + "/" + v.dateconfig.substr(0, 4);
            data[strDate].Ton += v.tonloading;
            data[strDate].CBM += v.cbmloading;
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

        angular.forEach($scope.Total.Data, function (v, i) {
            var strDate = v.dateconfig.substr(8, 2) + "/" + v.dateconfig.substr(5, 2) + "/" + v.dateconfig.substr(0, 4);
            data[strDate].Hour += v.timeloading;
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

        angular.forEach($scope.Total.Data, function (v, i) {
            var strDate = v.dateconfig.substr(8, 2) + "/" + v.dateconfig.substr(5, 2) + "/" + v.dateconfig.substr(0, 4);
            data[strDate].Ton += v.tonunloading;
            data[strDate].CBM += v.cbmunloading;
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

        angular.forEach($scope.Total.Data, function (v, i) {
            var strDate = v.dateconfig.substr(8, 2) + "/" + v.dateconfig.substr(5, 2) + "/" + v.dateconfig.substr(0, 4);
            data[strDate].Hour += v.timeunloading;
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

    $scope.InitData_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var isComplete = false;
        var isCompleteVehicle = true;

        // Delete rows tms
        $.ajax({
            type: 'POST',
            async: true,
            url: _REPMap.API.URL + _REPMap.SQLMain_Delete + $scope.GenSQLWhere() + _REPMap.API.APIKey,
            success: function (respond) {
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPMap.URL.Read,
                    data: { request: '', lstCustomerID: [], DateFrom: $scope.Item.DateFrom, DateTo: $scope.Item.DateTo, provinceID: $scope.Item.ProvinceID },
                    success: function (res) {
                        var count = res.lstDetail.length;
                        var countRun = 0;
                        Common.Log("Total: " + count);
                        angular.forEach(res.lstDetail, function (v, i) {
                            $.ajax({
                                type: 'POST',
                                async: true,
                                url: _REPMap.API.URL + $scope.GenSQLInsert(v) + _REPMap.API.APIKey,
                                success: function (responseData, textStatus, jqXHR) {
                                    countRun++;
                                    Common.Log("Insert data " + countRun);
                                    
                                },
                                error: function (responseData, textStatus, errorThrown) {
                                    countRun++;
                                    Common.Log("Error data " + countRun);
                                },
                                complete: function () {
                                    Common.Log(count + "==" + countRun);
                                    if (count == countRun) {
                                        isComplete = true;
                                        $scope.InitData_Complete(isComplete, isCompleteVehicle);
                                    }
                                },
                            });
                        });
                    }
                });
            },
            error: function (responseData, textStatus, errorThrown) {
                isComplete = true;
                $scope.InitData_Complete(isComplete, isCompleteVehicle);
            }
        });

        //// Delete rows tms_vehicle
        //$.ajax({
        //    type: 'POST',
        //    url: _REPMap.API.URL + _REPMap.SQLVehicle_Delete + $scope.GenSQLWhere() + _REPMap.API.APIKey,
        //    success: function (respond) {
        //        Common.Services.Call($http, {
        //            url: Common.Services.url.REP,
        //            method: _REPMap.URL.Read_VehicleRoute,
        //            data: { DateFrom: $scope.Item.DateFrom, DateTo: $scope.Item.DateTo },
        //            success: function (res) {
        //                var count = res.length;
        //                var countRun = 0;
        //                Common.Log("Total vehicle: " + count);
        //                angular.forEach(res, function (v, i) {
        //                    $.ajax({
        //                        type: 'POST',
        //                        async: true,
        //                        url: _REPMap.API.URL + $scope.GenSQLVehicle_Insert(v) + _REPMap.API.APIKey,
        //                        success: function (responseData, textStatus, jqXHR) {
        //                            countRun++;
        //                            Common.Log("Insert data vehicle" + countRun);

        //                        },
        //                        error: function (responseData, textStatus, errorThrown) {
        //                            countRun++;
        //                            Common.Log("Error data vehicle" + countRun);
        //                        },
        //                        complete: function () {
        //                            Common.Log("vehicle " + count + "==" + countRun);
        //                            if (count == countRun) {
        //                                isCompleteVehicle = true;
        //                                $scope.InitData_Complete(isComplete, isCompleteVehicle);
        //                            }
        //                        },
        //                    });
        //                });
        //            }
        //        });
        //    },
        //    error: function (responseData, textStatus, errorThrown) {
        //        isCompleteVehicle = true;
        //        $scope.InitData_Complete(isComplete, isCompleteVehicle);
        //    }
        //});
    };

    $scope.InitData_Complete = function (isComplete, isCompleteVehicle) {
        if (isComplete && isCompleteVehicle)
            $rootScope.IsLoading = false;
    };

    $scope.GenSQLInsert = function (v) {
        var sqlInsert = "";
        if (v.TheGeo == "")
            sqlInsert = _REPMap.SQLMain_Insert;
        else {
            sqlInsert = _REPMap.SQLMain_InsertGeo;
            sqlInsert += v.TheGeo + ",";
        }

        sqlInsert += v.ID + ",";
        sqlInsert += v.DITOMasterID + ",";
        sqlInsert += "'" + v.DITOMasterCode + "',";
        sqlInsert += v.KM + ",";
        sqlInsert += v.CustomerID + ",";
        sqlInsert += "'" + v.CustomerCode + "',";
        sqlInsert += v.OrderID + ",";
        sqlInsert += "'" + v.OrderCode + "',";
        sqlInsert += v.OrderGroupProductID + ",";
        sqlInsert += "'" + v.SOCode + "',";
        sqlInsert += "'" + v.DNCode + "',";
        sqlInsert += "'" + v.RequestDateString + "',";
        sqlInsert += "'" + v.ETDString + "',";
        sqlInsert += "'" + v.ETAString + "',";
        sqlInsert += "'" + v.DateConfigString + "',";
        sqlInsert += v.TonTranfer + ",";
        sqlInsert += v.CBMTranfer + ",";
        sqlInsert += v.QuantityTranfer + ",";
        sqlInsert += v.TonReturn + ",";
        sqlInsert += v.CBMReturn + ",";
        sqlInsert += v.QuantityReturn + ",";
        sqlInsert += v.DITOGroupProductStatusPODID + ",";
        sqlInsert += "'" + v.StockCode + "',";
        sqlInsert += "'" + v.StockName + "',";
        sqlInsert += "'" + v.StockAddress + "',";
        sqlInsert += v.StockProvinceID + ",";
        sqlInsert += v.StockDistrictID + ",";
        sqlInsert += "'" + v.DistributorCode + "',";
        sqlInsert += "'" + v.DistributorName + "',";
        sqlInsert += v.DistributorProvinceID + ",";
        sqlInsert += v.DistributorDistrictID + ",";
        sqlInsert += "'" + v.DistributorAddress + "',";
        sqlInsert += v.DistributorLat + ",";
        sqlInsert += v.DistributorLng + ",";
        sqlInsert += v.VehicleID + ",";
        sqlInsert += v.VehicleTon + ",";
        sqlInsert += v.VehicleCBM + ",";
        sqlInsert += "'" + _REPMap.HostName + "',";
        sqlInsert += v.TonLoading + ",";
        sqlInsert += v.CBMLoading + ",";
        sqlInsert += v.TonUnLoading + ",";
        sqlInsert += v.CBMUnLoading + ",";
        sqlInsert += v.TimeLoading + ",";
        sqlInsert += v.TimeUnLoading + ",";
        sqlInsert += v.IsReturn + ")";
        return sqlInsert;
    };

    $scope.GenSQLVehicle_Insert = function (v) {
        var sqlInsert = _REPMap.SQLVehicle_Insert;

        sqlInsert += "CDB_LatLng(" + v.Lat + "," + v.Lng + "),";
        sqlInsert += "'" + v.VehicleCode + "',";
        sqlInsert += v.Lat + ",";
        sqlInsert += v.Lng + ",";
        sqlInsert += "'" + _REPMap.HostName + "',";
        sqlInsert += "'" + Common.Date.FromJsonYYMMDD(v.CreatedDate) + "',";
        sqlInsert += "'" + Common.Date.FromJsonYYMMDDHM(v.CreatedDate) + "')";
        return sqlInsert;
    };

    $scope.GenSQLProvince_Insert = function (v) {
        var sqlInsert = _REPMap.SQLProvince_Insert;

        sqlInsert += v.TheGeo + ",";
        sqlInsert += v.ProvinceID + ",";
        sqlInsert += "'" + v.ProvinceCode + "',";
        sqlInsert += "'" + v.ProvinceName + "',";
        sqlInsert += v.ProvinceLat + ",";
        sqlInsert += v.ProvinceLng + ")";
        return sqlInsert;
    };

    $scope.RefreshMap_Click = function ($event) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.LayerActions['refresh']();
        $rootScope.IsLoading = false;
        
        var qWhere = $scope.GenSQLWhere();
        if ($scope.Item.ProvinceID > 0)
            qWhere += " and distributorprovinceid = " + $scope.Item.ProvinceID;

        var qAll = "Select * from tms" + qWhere;
        var qSum = "Select Sum(tontranfer) as TonTranfer,Sum(cbmtranfer) as CBMTranfer,Sum(tonreturn) as TonReturn,Sum(cbmreturn) as CBMReturn,Sum(tonloading) as TonLoading,Sum(cbmloading) as CBMLoading,Sum(tonunloading) as TonUnLoading,Sum(cbmunloading) as CBMUnLoading,Sum(timeloading) as TimeLoading,Sum(timeunloading) as TimeUnLoading from tms" + qWhere;
        
        var qSchedule = "Select Count(Distinct(ditomasterid)) from tms" + qWhere;
        var qScheduleEmpty = "Select Count(Distinct(ditomasterid)) from tms" + qWhere + " and (tonreturn > 0 or cbmreturn > 0 or quantityreturn > 0 or isreturn = true)";
        
        var qKM = "Select Distinct ditomasterid, km, vehicleid, vehicleton, vehiclecbm from tms" + qWhere;
        //Prepare SQL for insights
        var sql_boxplot = new cartodb.SQL({ user: _REPMap.API.UserName, api_key: _REPMap.API.APIKeyOnly });
        
        sql_boxplot.execute(qSum)
            .done(function (data) {
                $timeout(function () {
                    $scope.Total.TonTranfer = Math.round(data.rows[0].tontranfer * 10) / 10;
                    $scope.Total.CBMTranfer = Math.round(data.rows[0].cbmtranfer * 10) / 10;
                    $scope.Total.TonReturn = Math.round(data.rows[0].tonreturn * 10) / 10;
                    $scope.Total.CBMReturn = Math.round(data.rows[0].cbmreturn * 10) / 10;
                    $scope.Total.TonLoading = Math.round(data.rows[0].tonloading * 10) / 10;
                    $scope.Total.CBMLoading = Math.round(data.rows[0].cbmloading * 10) / 10;
                    $scope.Total.TonUnLoading = Math.round(data.rows[0].tonunloading * 10) / 10;
                    $scope.Total.CBMUnLoading = Math.round(data.rows[0].cbmunloading * 10) / 10;
                    $scope.Total.TimeLoading = Math.round(data.rows[0].timeloading * 10) / 10;
                    $scope.Total.TimeUnLoading = Math.round(data.rows[0].timeunloading * 10) / 10;
                }, 10);
            });

        sql_boxplot.execute(qSchedule)
           .done(function (data) {
               $timeout(function () {
                   $scope.Total.Schedule = data.rows[0].count;
               }, 10);
           });

        sql_boxplot.execute(qScheduleEmpty)
           .done(function (data) {
               $timeout(function () {
                   $scope.Total.ScheduleEmpty = data.rows[0].count
               }, 10);
           });

        sql_boxplot.execute(qKM)
           .done(function (data) {
               var totalKM = 0;
               var arrVehicle = [];
               var totalTon = 0;
               var totalCBM = 0;
               angular.forEach(data.rows, function (v, i) {
                   totalKM += v.km;
                   totalTon += v.vehicleton;
                   totalCBM += v.vehiclecbm;
                   if (arrVehicle.indexOf(v.vehicleid) < 0)
                       arrVehicle.push(v.vehicleid);
               });
               var totalVehicle = arrVehicle.length + 1;
               $timeout(function () {
                   $scope.Total.KM = totalKM;
                   $scope.Total.KMAverage = Math.round(totalKM / (totalVehicle > 1 ? totalVehicle - 1 : totalVehicle) * 10) / 10;
                   $scope.Total.TonMax = Math.round(totalTon * 10) / 10;
                   $scope.Total.CBMMax = Math.round(totalCBM * 10) / 10;
                   $scope.Total.Vehicle = totalVehicle - 1;
               }, 10);
           });

        sql_boxplot.execute(qAll)
           .done(function (data) {
               $scope.Total.Data = data.rows;
           });

        // Set zoom to province
        if ($scope.Item.ProvinceID > 0) {
            var query = "Select provincelat, provincelng from tms_province where provinceid = " + $scope.Item.ProvinceID;
            sql_boxplot.execute(query)
           .done(function (data) {
               if (data.rows.length > 0) {
                   $scope.Map.setZoom(9);
                   $timeout(function () {
                       $scope.Map.panTo([data.rows[0].provincelat, data.rows[0].provincelng]);
                   }, 100);
               }
           });
        }
    }

    $scope.GenProvincePolygon_Click = function ($event) {
        $event.preventDefault();

        $.ajax({
            type: 'POST',
            url: _REPMap.API.URL + _REPMap.SQLProvince_Delete + _REPMap.API.APIKey,
            success: function (respond) {
                angular.forEach(_REPMap.Data.Province, function (v, i) {
                    var provinceID = v.ID;
                    var provinceCode = v.Code;
                    var provinceName = v.ProvinceName;
                    var provinceLat = v.Lat;
                    var provinceLng = v.Lng;

                    var id = "";
                    if (v.ID <= 9)
                        id = _REPMap.Data.ProvinceCode["0" + v.ID];
                    else
                        id = _REPMap.Data.ProvinceCode[v.ID];

                    if (!Common.HasValue(id))
                        Common.Log(provinceID + "-" + provinceName);

                    var url = _REPMap.API.geojson + id + _REPMap.API.suffix;
                    
                    $.ajax({
                        type: 'POST',
                        url: url,
                        success: function (responseData, textStatus, jqXHR) {
                            var polygon = "ST_SetSRID(st_geomfromtext('POLYGON((";
                            angular.forEach(responseData.coordinates[0], function (v, i) {
                                polygon += v[0] + " " + v[1] + ",";
                            });
                            polygon += responseData.coordinates[0][0][0] + " " + responseData.coordinates[0][0][1] + "))'), 4326)";
                            var itemPolygon = { TheGeo: polygon, ProvinceID: provinceID, ProvinceCode: provinceCode, ProvinceName: provinceName, ProvinceLat: provinceLat, ProvinceLng: provinceLng };

                            var query = $scope.GenSQLProvince_Insert(itemPolygon);
                            var api_key = _REPMap.API.APIKeyOnly;

                            $.ajax({
                                type: 'POST',
                                url: _REPMap.API.URLOnly,

                                data: { q: query, api_key: api_key },
                                success: function (responseData, textStatus, jqXHR) {
                                },
                                error: function (responseData, textStatus, errorThrown) {
                                },
                                complete: function () {
                                },
                            });
                        },
                        error: function (error) {
                        },
                    });
                });
            },
        });
    };

    $scope.CopyTable_Click = function ($event) {
        $event.preventDefault();
        var strHostName = "tmsdemo";
        //Prepare SQL for insights
        $.ajax({
            type: 'POST',
            async: true,
            url: _REPMap.API.URL + "DROP TABLE " + strHostName + _REPMap.API.APIKey,
            success: function (responseData, textStatus, jqXHR) {
                $.ajax({
                    type: 'POST',
                    async: true,
                    url: _REPMap.API.URL + "CREATE TABLE " + strHostName + " AS SELECT * FROM vinafco" + _REPMap.API.APIKey,
                    success: function (responseData, textStatus, jqXHR) {
                        

                    },
                    error: function (responseData, textStatus, errorThrown) {

                    },
                    complete: function () {

                    },
                });
            },
            error: function (responseData, textStatus, errorThrown) {

            },
            complete: function () {

            },
        });


        
    };

}]);