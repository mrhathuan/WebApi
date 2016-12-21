/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _KPITime_Vendor = {
    URL: {
        Time_List: 'KPIVENTime_List',
        Time_Save: 'KPIVENTime_Save',
        Time_Excel: 'KPIVENTime_Excel',
        Time_Generate: 'KPIVENTime_Generate',
        Vendor_List:'Vendor_List',

        KPI_List: 'KPIKPI_List',

        DashBoard_DN: 'Dashboard_DN_Data',
        DashBoard_Reason: 'Dashboard_Reason_Data'
    },
    Data: {
        KPI: [],
        Reason: [],
        DashBoard_DN: null,
        DashBoard_Reason_1: null,
        DashBoard_Reason_100: null
    },
    Item: {
        Reason: {
            ID: -1,
            Code: '',
            ReasonName: '',
            KPIID: -1
        }
    }
}

//#endregion

angular.module('myapp').controller('KPITime_VendorCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('KPITime_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.TabIndex = 0;
    $scope.HasTimeSearch = false;
    $scope.HasDataReason = false;

    $scope.IsDashBoardDNSettings = false;
    $scope.IsDashBoardReasonSettings = false;

    $scope.TimeItem = null;
    $scope.ViewItem = {
        VenID: -1, KPIID: -1, DateFrom: new Date().addDays(-3), DateTo: new Date().addDays(3)
    };

    $scope.DashBoardDN_Item = {
        DateFrom: new Date().addDays(-3), DateTo: new Date().addDays(3),
        StrFrom: Common.Date.ToString(new Date().addDays(-3), Common.Date.Format.DMY),
        StrTo: Common.Date.ToString(new Date().addDays(3), Common.Date.Format.DMY),
        CustomerID: -1, KPIID: -1, CustomerName: 'Tất cả ĐT', KPIName: '', IsRadialGauge: false
    };

    $scope.DashBoardReason_Item = {
        KPIID: -1, CustomerID: -1, CustomerName: 'Tất cả ĐT', KPIName: '',
        StrFrom: Common.Date.ToString(new Date().addDays(-3), Common.Date.Format.DMY),
        StrTo: Common.Date.ToString(new Date().addDays(3), Common.Date.Format.DMY),
        DateFrom: new Date().addDays(-3), DateTo: new Date().addDays(3), ChartID: 1,
        SortClass: 'k-icon k-i-arrowhead-n'
    };

    $scope.KPIDashboard_DN_Scale_KPI = 0;

    $scope.kpi_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
            }, 1);
        }
    }

    //#region DashBoard

    $scope.cboDashBoard_KPIOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'KPIName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    KPIName: { type: 'string' },
                }
            }
        })
    }

    $scope.OnDashBoardDN_KPIChange = function ($event) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            $scope.DashBoardDN_Item.KPIName = obj.KPIName;
        }
    }

    $scope.OnDashBoardDN_CusChange = function ($event) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            $scope.DashBoardDN_Item.CustomerName = obj.CustomerName;
        }
    }

    $scope.OnDashBoardDN_DateFromChange = function ($event) {
        $scope.DashBoardDN_Item.StrFrom = Common.Date.ToString($scope.DashBoardDN_Item.DateFrom, Common.Date.Format.DMY);
    }

    $scope.OnDashBoardDN_DateToChange = function ($event) {
        $scope.DashBoardDN_Item.StrTo = Common.Date.ToString($scope.DashBoardDN_Item.DateTo, Common.Date.Format.DMY);
    }

    $scope.OnDashBoardReason_KPIChange = function ($event) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            $scope.DashBoardReason_Item.KPIName = obj.KPIName;
        }
    }

    $scope.OnDashBoardReason_CusChange = function ($event) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            $scope.DashBoardReason_Item.CustomerName = obj.CustomerName;
        }
    }

    $scope.OnDashBoardReason_DateFromChange = function ($event) {
        $scope.DashBoardReason_Item.StrFrom = Common.Date.ToString($scope.DashBoardReason_Item.DateFrom, Common.Date.Format.DMY);
    }

    $scope.OnDashBoardReason_DateToChange = function ($event) {
        $scope.DashBoardReason_Item.StrTo = Common.Date.ToString($scope.DashBoardReason_Item.DateTo, Common.Date.Format.DMY);
    }

    $scope.cboDashBoard_CusOptions = {
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
        })
    }

    $scope.hor_splitterOptions = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: false, size: "50%" },
            { collapsible: true, resizable: false }
        ]
    };

    $scope.KPIDashboard_DNOptions = {
        legend: { position: "top" },
        dataSource: {
            data: [], sort: { field: "Date", dir: "asc" }
        },
        seriesDefaults: {
            type: "column", stack: false
        },
        series: [
            {
                type: "column", field: "TotalSuccess", name: 'Đạt', color: "blue",
                tooltip: {
                    visible: true,
                    template: 'Số lượng: #= value #'
                }
            },
            {
                type: "column", field: "TotalFail", name: 'Không đạt', color: "orange",
                tooltip: {
                    visible: true,
                    template: 'Số lượng: #= value #'
                }
            },
            //{
            //    type: "column", field: "TotalNull", name: 'Chưa tính', color: "green",
            //    tooltip: {
            //        visible: true,
            //        template: 'Số lượng: #= value #'
            //    }
            //},
            {
                type: "line", field: "Goal", name: 'Tỉ lệ',
                color: "#007eff", axis: "goal",
                tooltip: {
                    visible: true,
                    template: 'Tỉ lệ: #= value.toFixed(2) #%'
                }
            }
        ],
        valueAxis: [{
            name: "total", color: "#9B1A2E", title: { text: "Số lượng DN" }, min: 0, majorUnit: 1
        }, {
            name: "goal", color: "#007eff", title: { text: "Tỉ lệ đạt KPI" }, min: 0, max: 100
        }],
        categoryAxis: {
            field: "Date", majorGridLines: { visible: false }, labels: { format: "d/M", rotation: '300' },
            axisCrossingValues: [0, 10000]
        },
        zoomable: {
            mousewheel: {
                lock: "y"
            },
            selection: {
                lock: "y"
            }
        }
    }

    $scope.KPIDashboard_DN_ScaleOptions = {
        rangeSize: 40, rangeDistance: -10, majorUnit: 20, minorUnit: 5, startAngle: 0, endAngle: 180, max: 100,
        labels: { position: "outside", template: "#= value #%" },
        ranges: [
            { from: 0, to: 50, color: 'red' },
            { from: 50, to: 98, color: 'yellow' },
            { from: 98, to: 100, color: 'blue' }
        ]
    }

    $scope.KPIDashboard_ReasonOptions = {
        legend: { visible: false },
        dataSource: {
            data: [],
            sort: {
                field: "category",
                dir: "asc"
            }
        },
        seriesDefaults: {
            labels: {
                visible: true,
                font: "bold 13px Arial",
                background: "transparent",
                template: "#= category #: \n #= value <= 1 ? Math.round(value * 100) : Math.round(value) #%"
            }
        },
        series: [{
            type: "pie", field: "value", categoryField: "category"
        }],
        valueAxis: [{
            name: "total", color: "#FF9B57", title: { text: "Tỉ lệ" }, min: 0, max: 100
        }],
        tooltip: {
            visible: true,
            template: function (e) {
                var val = 0;
                if (e.dataItem.value <= 1)
                    val = e.dataItem.value * 100;
                else
                    val = e.dataItem.value;
                return "<div>" + e.dataItem.category + ": " + Math.round(val) + "%</div>";
            }
        },
        zoomable: {
            mousewheel: {
                lock: "y"
            },
            selection: {
                lock: "y"
            }
        }
    }

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

    $scope.OnDashBoard_ThemeChange = function ($event, chart) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            chart.setOptions({ theme: obj.ID });
        }
    }

    $scope.OnDashBoardDN_ChartChange = function ($event, chart) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            $timeout(function () {
                var data = [];
                var options = {};
                switch (obj.ID) {
                    default:
                    case 1:
                        $scope.DashBoardDN_Item.IsRadialGauge = false;
                        chart.setOptions({
                            seriesDefaults: {
                                type: "column", stack: false
                            }
                        });
                        $timeout(function () {
                            chart.redraw();
                        }, 1)
                        break;
                    case 2:
                        $scope.DashBoardDN_Item.IsRadialGauge = false;
                        chart.setOptions({
                            seriesDefaults: {
                                type: "column", stack: true
                            }
                        });
                        $timeout(function () {
                            chart.redraw();
                        }, 1)
                        break;
                    case 3:
                        $scope.DashBoardDN_Item.IsRadialGauge = true;
                        $timeout(function () {
                            $scope.KPIDashboard_DN_Scale.redraw();
                        }, 1)
                        break;
                }
            }, 1)
        }
    }

    $scope.OnDashBoardReason_ChartChange = function ($event, chart) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            $timeout(function () {
                var data = [];
                var options = {};
                switch (obj.ID) {
                    default:
                    case 1:
                        options = {
                            series: [{
                                type: "pie", field: "value", categoryField: "category"
                            }]
                        };
                        data = _KPITime_Vendor.Data.DashBoard_Reason_1;
                        break;
                    case 2:
                        options = {
                            series: [{
                                type: "line", field: "value", categoryField: "category"
                            }]
                        };
                        data = _KPITime_Vendor.Data.DashBoard_Reason_100;
                        break;
                    case 3:
                        options = {
                            series: [{
                                type: "column", field: "value", categoryField: "category"
                            }]
                        };
                        data = _KPITime_Vendor.Data.DashBoard_Reason_100;
                        break;
                }
                chart.setOptions(options);
                chart.setDataSource(data);
            }, 1)
        }
    }

    $scope.cboDashBoardDN_ChartOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0,
        dataSource: Common.DataSource.Local({
            data: [
                {
                    ID: '1', Text: 'BĐ cột'
                },
                {
                    ID: '2', Text: 'BĐ cột chồng'
                },
                {
                    ID: '3', Text: 'Radial Gauge'
                }
            ],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Text: { type: 'string' },
                }
            }
        })
    }

    $scope.cboDashBoardReason_ChartOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0,
        dataSource: Common.DataSource.Local({
            data: [
                {
                    ID: '1', Text: 'Pie'
                },
                {
                    ID: '2', Text: 'Line'
                },
                {
                    ID: '3', Text: 'Column'
                }
            ],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Text: { type: 'string' },
                }
            }
        })
    }

    $scope.cboDashBoardReason_SortOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0,
        template: '<span class="k-icon #: data.Icon #"></span>#: data.Text #',
        dataSource: Common.DataSource.Local({
            data: [
                {
                    ID: '1', Text: 'Tên lý do', Icon: 'k-i-arrowhead-n', Value: 'category', Dir: 'asc'
                },
                {
                    ID: '2', Text: 'Tên lý do', Icon: 'k-i-arrowhead-s', Value: 'category', Dir: 'desc'
                },
                {
                    ID: '3', Text: 'Giá trị', Icon: 'k-i-arrowhead-n', Value: 'value', Dir: 'asc'
                },
                {
                    ID: '3', Text: 'Giá trị', Icon: 'k-i-arrowhead-s', Value: 'value', Dir: 'desc'
                }
            ],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Text: { type: 'string' },
                }
            }
        })
    }

    $scope.OnDashBoardReason_SortChange = function ($event, chart) {
        var cbo = $event.sender;
        var obj = cbo.dataItem(cbo.select());
        if (Common.HasValue(obj)) {
            $timeout(function () {
                $scope.DashBoardReason_Item.SortClass = "k-icon " + obj.Icon;
                var sort = {
                    field: obj.Value,
                    dir: obj.Dir
                }

                var data = chart.dataSource.data();
                var data = new kendo.data.DataSource({
                    data: chart.dataSource.data(),
                    sort: sort
                });
                data.fetch();
                chart.setDataSource(data);
            }, 1)
        }
    }

    $scope.DashBoard_DN_Setting = function ($event) {
        $event.preventDefault();
        $scope.IsDashBoardDNSettings = true;
    }

    $scope.DashBoard_DN_Setting_OK = function ($event) {
        if ($event)
            $event.preventDefault();

        $scope.IsDashBoardDNSettings = false;
        $rootScope.IsLoading = true;
        Common.Log("DashBoard_DN_Setting_OK");
        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPITime_Vendor.URL.DashBoard_DN,
            data: {
                cusID: $scope.DashBoardDN_Item.CustomerID, kpiID: $scope.DashBoardDN_Item.KPIID,
                from: $scope.DashBoardDN_Item.DateFrom, to: $scope.DashBoardDN_Item.DateTo
            },
            success: function (res) {
                var total = 0;
                var kpi = 0;
                Common.Data.Each(res, function (o) {
                    total = total + o.TotalSuccess + o.TotalFail;
                    kpi += o.TotalSuccess;
                    o.Date = Common.Date.FromJsonDDMM(o.Date);
                })
                $scope.KPIDashboard_DN_Scale_KPI = 0;
                if (total > 0)
                    $scope.KPIDashboard_DN_Scale_KPI = Math.round(kpi * 10000 / total) / 100;
                $timeout(function () {
                    $scope.KPIDashboard_DN.setDataSource(res);
                    $rootScope.IsLoading = false;
                }, 1)
            }
        });
    }

    $scope.DashBoard_Reason_Setting = function ($event) {
        $event.preventDefault();
        $scope.IsDashBoardReasonSettings = true;
    }

    $scope.DashBoard_Reason_Setting_OK = function ($event) {
        if ($event)
            $event.preventDefault();

        $scope.IsDashBoardReasonSettings = false;
        $rootScope.IsLoading = true;
        Common.Log("DashBoard_Reason_Setting_OK");
        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPITime_Vendor.URL.DashBoard_Reason,
            data: {
                cusID: $scope.DashBoardReason_Item.CustomerID, kpiID: $scope.DashBoardReason_Item.KPIID,
                from: $scope.DashBoardReason_Item.DateFrom, to: $scope.DashBoardReason_Item.DateTo
            },
            success: function (res) {
                $scope.HasDataReason = res.length > 0;
                var data_1 = [];
                var data_100 = [];
                Common.Data.Each(res, function (o) {
                    var obj_1 = { 'value': o.Percent, 'category': o.ReasonName };
                    var obj_100 = { 'value': o.Percent * 100, 'category': o.ReasonName };
                    data_1.push(obj_1);
                    data_100.push(obj_100);
                })
                _KPITime_Vendor.Data.DashBoard_Reason_1 = data_1;
                _KPITime_Vendor.Data.DashBoard_Reason_100 = data_100;
                $timeout(function () {
                    if ($scope.DashBoardReason_Item.ChartID == 1)
                        $scope.KPIDashboard_Reason.setDataSource(data_1);
                    else
                        $scope.KPIDashboard_Reason.setDataSource(data_100);
                    $rootScope.IsLoading = false;
                }, 1)
            }
        });
    }

    $timeout(function () {
        $scope.hor_splitter.resize();
    }, 100)

    //#endregion

    //#region KPITime
    Common.Services.Call($http, {
        url: Common.Services.url.KPI,
        method: _KPITime_Index.URL.KPI_List,
        success: function (res) {
            _KPITime_Index.Data.KPI = res;
            Common.Data.Each(res, function (o) {
                if (_KPITime_Index.Item.Reason.KPIID < 1)
                    _KPITime_Index.Item.Reason.KPIID = o.ID;
                if ($scope.ViewItem.KPIID < 1)
                    $scope.ViewItem.KPIID = o.ID;
                if ($scope.DashBoardReason_Item.KPIID < 1) {
                    $scope.DashBoardReason_Item.KPIID = o.ID;
                    $scope.DashBoardReason_Item.KPIName = o.KPIName;
                }
                if ($scope.DashBoardDN_Item.KPIID < 1) {
                    $scope.DashBoardDN_Item.KPIID = o.ID;
                    $scope.DashBoardDN_Item.KPIName = o.KPIName;
                }
            })

            $timeout(function () {
                $scope.cboTime_KPIOptions.dataSource.data(_KPITime_Index.Data.KPI);
                $scope.cboDashBoard_KPIOptions.dataSource.data(_KPITime_Index.Data.KPI);
                $scope.DashBoard_Reason_Setting_OK();
                $scope.DashBoard_DN_Setting_OK();
            }, 1)
        }
    });
    $scope.cboTime_KPIOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'KPIName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    KPIName: { type: 'string' },
                }
            }
        })
    }

    $scope.cboTime_CusOptions = {
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
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.KPI,
        method: _KPITime_Vendor.URL.Vendor_List,
        success: function (res) {
            var data = [{ ID: -1, CustomerName: "Tất cả ĐT" }];
            Common.Data.Each(res.Data, function (o) {
                data.push(o);
                if ($scope.ViewItem.VenID < 1)
                    $scope.ViewItem.VenID = o.ID;
            })
            $timeout(function () {
                $scope.cboTime_CusOptions.dataSource.data(res.Data);
                $scope.cboDashBoard_CusOptions.dataSource.data(data);
            }, 1);
        }
    })

    var kpi_grid = null;

    $scope.kpi_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.KPI,
            method: _KPITime_Vendor.URL.Time_List,
            readparam: function () {
                return {
                    venID: $scope.ViewItem.VenID, kpiID: $scope.ViewItem.KPIID,
                    from: $scope.ViewItem.DateFrom, to: $scope.ViewItem.DateTo
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    OrderCode: { type: 'string', editable: false },
                    SOCode: { type: 'string', editable: false },
                    DNCode: { type: 'string', editable: false },
                    CustomerCode: { type: 'string', editable: false },
                    CustomerName: { type: 'string', editable: false },
                    RoutingName: { type: 'string', editable: false },
                    COTOMasterCode: { type: 'string', editable: false },
                    DITOMasterCode: { type: 'string', editable: false },
                    KPICode: { type: 'string', editable: false },
                    KPIName: { type: 'string', editable: false },

                    IsKPI: { type: 'boolean', editable: false },
                    DateData: { type: 'date', editable: false },
                    DateFromCome: { type: 'date', editable: false },
                    DateFromLeave: { type: 'date', editable: false },
                    DateFromLoadStart: { type: 'date', editable: false },
                    DateFromLoadEnd: { type: 'date', editable: false },
                    DateToCome: { type: 'date', editable: false },
                    DateToLeave: { type: 'date', editable: false },
                    DateToLoadStart: { type: 'date', editable: false },
                    DateToLoadEnd: { type: 'date', editable: false },
                    DateDN: { type: 'date', editable: false },
                    DateInvoice: { type: 'date', editable: false },
                    KPIDate: { type: 'date', editable: false },
                    ReasonCode: { type: 'string', editable: false },
                    ReasonName: { type: 'string', editable: false },
                    Note: { type: 'string', editable: false }
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#kpi_gridToolbar').html()),
        columns: [
            {
                field: 'OrderCode', title: 'Đơn hàng', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', title: 'Số SO', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', title: 'Số DN', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: 'Mã khách hàng', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Tên khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateData', title: 'Ngày y/c ĐH', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateData)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateDN', title: 'Ngày DN', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateDN)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromCome', title: 'Đến kho', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateFromCome)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLeave', title: 'Rời kho', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateFromLeave)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLoadStart', title: 'TG vào máng', width: '130px', template: '#=Common.Date.FromJsonDMYHM(DateFromLoadStart)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLoadEnd', title: 'TG ra máng', width: '130px', template: '#=Common.Date.FromJsonDMYHM(DateFromLoadEnd)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToCome', title: 'TG đến NPP', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateToCome)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLeave', title: 'TG rời NPP', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateToLeave)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLoadStart', title: 'TG BĐ dỡ hàng', width: '130px', template: '#=Common.Date.FromJsonDMYHM(DateToLoadStart)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLoadEnd', title: 'TG KT dỡ hàng', width: '130px', template: '#=Common.Date.FromJsonDMYHM(DateToLoadEnd)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMYHM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateInvoice', title: 'Ngày hóa đơn', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateInvoice)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'KPIDate', title: 'Ngày KPI', width: '120px', template: '#=Common.Date.FromJsonDMYHM(KPIDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateTOMasterETD', title: 'Ngày ETD chuyến', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateTOMasterETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateTOMasterETA', title: 'Ngày ETA chuyến', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateTOMasterETA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateTOMasterATD', title: 'Ngày ATD chuyến', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateTOMasterATD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateTOMasterATA', title: 'Ngày ATA chuyến', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateTOMasterATA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateOrderETD', title: 'Ngày đơn hàng ETD', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateOrderETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateOrderETA', title: 'Ngày đơn hàng ETA', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateOrderETA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateOrderETDRequest', title: ' Ngày đơn hàng ETD yêu cầu', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateOrderETDRequest)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateOrderETARequest', title: 'Ngày đơn hàng ETA yêu cầu', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateOrderETARequest)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateOrderCutOfTime', title: 'CutOfTime', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateOrderCutOfTime)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'IsKPI', title: 'Đạt KPI', width: '60px',
                template: '<input ng-show="dataItem.IsKPI != null" disabled type="checkbox" #= IsKPI ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
                sortable: true, filterable: false, menu: false
            },
            {
                field: 'ReasonName', title: 'Lý do', width: '150px',
                template: '<div style="height:100%;width:100%;"><a style="width: 100%;text-align:left;" href="/" ng-show="dataItem.IsKPI == false" ng-click="TimeReason_Click($event,dataItem,time_reason_win)" class="k-button">#=ReasonName!=""?ReasonName:"Chọn"#</a></div>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DITOGroupProductStatusPODName', title: 'Tình trạng c.từ', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractDisplayName', title: 'Hợp đồng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.$watch('kpi_grid', function (newValue, oldValue) {
        if (newValue != null)
            Common.Controls.Grid.ReorderColumns({ Grid: newValue, CookieName: 'KPITime_Grid' });
    });

    $scope.View_Click = function ($event) {
        $event.preventDefault();

        $scope.kpi_gridOptions.dataSource.read();
        $scope.HasTimeSearch = true;
    };

    $scope.Generate_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Title: 'Thông báo', Msg: 'Bạn muốn tính lại KPI?',
            Action: true, Close: null,
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.KPI,
                    method: _KPITime_Vendor.URL.Time_Generate,
                    data: pars,
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        if (Common.HasValue(res)) {
                            $scope.kpi_gridOptions.dataSource.read();
                        }
                    }
                });
            },
            pars: { dtfrom: $scope.ViewItem.DateFrom, dtto: $scope.ViewItem.DateTo, venID: $scope.ViewItem.VenID }
        });
    };

    $scope.TimeReason_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.LoadData_Reason();
        $scope.TimeItem = $.extend({}, true, item);
        win.center().open();
    }

    $scope.cboKPIReason_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ReasonName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ReasonName: { type: 'string' },
                }
            }
        })
    }

    $scope.LoadData_Reason = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPITime_Vendor.URL.Time_Reason_List,
            data: { kpiID: $scope.ViewItem.KPIID },
            success: function (res) {
                $timeout(function () {
                    $scope.cboKPIReason_Options.dataSource.data(res);
                }, 1)
            }
        });
    }

    $scope.Time_Reason_Submit_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPITime_Vendor.URL.Time_Save,
            data: { item: $scope.TimeItem },
            success: function (res) {
                Common.Services.Error(res, function () {
                    win.close();
                    $scope.kpi_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                })
            }
        });
    }

    $scope.KPI_Excel_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPITime_Vendor.URL.Time_Excel,
            data: {
                venID: $scope.ViewItem.VenID, kpiID: $scope.ViewItem.KPIID,
                from: $scope.ViewItem.DateFrom, to: $scope.ViewItem.DateTo
            },
            success: function (res) {
                $rootScope.DownloadFile(res);
            }
        })
    }
    //#endregion
    //#region Action
    $scope.ShowSetting = function ($event) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.KPITime,
            event: $event,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);