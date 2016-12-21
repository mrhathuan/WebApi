
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerTotalSchedule_Index = {
    URL: {
        Search: 'REPOwner_TotalSchedule',
        Template: 'REPOwner_TotalSchedule_Template',

        Read_Vendor: 'REP_Vendor_List',
    },
    Data: {
        _lstVehicle: [],
        _lstVehicleDetail: [],
        _lstDate: [],
        _lstDataChart: [],
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
    }
}

angular.module('myapp').controller('REPOwnerTotalSchedule_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerTotalSchedule_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    //#region search
    $scope.Item = {
        lstVendorID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        Theme: 'Bootstrap',
        Type: 'column',
    }

    $scope.mulVendor_Options = {
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
            
        },
    };

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPOwnerTotalSchedule_Index.URL.Read_Vendor,
        data: {},
        success: function (res) {
            $scope.mulVendor_Options.dataSource.data(res.Data);
            $timeout(function () {
                $scope.Item.lstVendorID.push(res.Data[0].ID);
                $scope.SearchData();
            }, 10);
        }
    });

    $scope.SearchData = function () {
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerTotalSchedule_Index.URL.Search,
            data: { lstVendorid: $scope.Item.lstVendorID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                _REPOwnerTotalSchedule_Index.Data._lstVehicle = res.lstVehicle;
                _REPOwnerTotalSchedule_Index.Data._lstVehicleDetail = res.lstVehicleDetail;
                _REPOwnerTotalSchedule_Index.Data._lstDate = res.lstDate;
                $scope.InitGrid();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.SearchData();
    };
    //#endregion

    //#region grid
    $scope.InitGrid = function () {
        Common.Log("InitGrid");
        var Model = {
            fields: {
                VehicleID: { type: 'number', editable: false },
                VehicleCode: { type: 'string' },
                VendorID: { type: 'string' },
                VendorCode: { type: 'string' },
                VendorName: { type: 'string' },
                TotalSchedule: { type: 'number' },
            },
            pageSize: 20
        }
        var GridColumn = [
            {
                field: 'VehicleCode', title: 'Số xe', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorCode', title: 'Mã đối tác', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: 'Đối tác', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]

        var Data = [];
        _REPOwnerTotalSchedule_Index.Data._lstChart = [];

        Common.Data.Each(_REPOwnerTotalSchedule_Index.Data._lstDate, function (itemDate) {
            Model.fields[itemDate.DateString] = {
                type: "number", editable: false,
            };

            GridColumn.push({
                field: itemDate.DateString, title: itemDate.DateName, width: 120,
            });
        });

        Common.Data.Each(_REPOwnerTotalSchedule_Index.Data._lstVehicleDetail, function (itemDetail) {
            var itemChart = { VehicleID: itemDetail.VehicleID, VehicleCode: itemDetail.VehicleCode, VendorID: itemDetail.VendorID, VendorCode: itemDetail.VendorCode, VendorName: itemDetail.VendorName, TotalSchedule: itemDetail.TotalSchedule };
            if (itemChart.TotalSchedule > 0)
                _REPOwnerTotalSchedule_Index.Data._lstChart.push(itemChart);
            var item = { VehicleID: itemDetail.VehicleID, VehicleCode: itemDetail.VehicleCode, VendorID: itemDetail.VendorID, VendorCode: itemDetail.VendorCode, VendorName: itemDetail.VendorName, TotalSchedule: itemDetail.TotalSchedule };
            Common.Data.Each(itemDetail.lstDate, function (itemDate) {
                item[itemDate.DateString] = itemDate.TotalSchedule;
            });
            Data.push(item);
        });

        GridColumn.push({ field: "TotalSchedule", title: 'Tổng chuyến', width: '150px', filterable: false, sortable: true })
        GridColumn.push({ title: ' ', filterable: false, sortable: false, width: '20px' })
        $scope.rep_grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: Data,
                model: Model,
                pageSize: 20,
            }),
            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false, pageSize: 20,
            columns: GridColumn
        });
        $rootScope.IsLoading = false;
    }

    $scope.cboThemeOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0, placeholder: 'Chọn theme',
        dataSource: Common.DataSource.Local({
            data: _REPOwnerTotalSchedule_Index.Data.Theme,
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
            data: _REPOwnerTotalSchedule_Index.Data.ChartType,
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

    $scope.ReDrawChart = function () {
        $scope.Chart_Click(null, true);
    };

    $scope.InitChartDetail = function (title, data, series, chart_theme, chart_type) {
        var chart = $("#REPOwnerTotalSchedule_Chart").data("kendoChart");
        // distroy existing chart
        if (chart != null) {
            chart.destroy();
            $('#REPOwnerTotalSchedule_Chart').empty();
        }

        $("#REPOwnerTotalSchedule_Chart").kendoChart({
            title: {
                text: title,
            },
            dataSource: {
                data: data,
                sort: { field: "VehicleCode", dir: "asc" },
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
                field: "VehicleCode", type: "category",
                majorGridLines: { visible: false },
            },
            tooltip: { visible: true, template: "#= series.name #: #= value #" },
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
        });

        chart = $("#REPOwnerTotalSchedule_Chart").data("kendoChart");
        chart.setOptions({ theme: chart_theme });
        chart.refresh();
        $timeout(function () {
            chart.resize();
        }, 10);
    };

    $scope.Chart_Click = function ($event, IsOpen) {
        if (Common.HasValue($event))
            $event.preventDefault();

        var series = [];
        series.push({ name: 'Số chuyến', field: "TotalSchedule" });

        // Draw chart
        $scope.InitChartDetail("Hệ số quay đầu xe", _REPOwnerTotalSchedule_Index.Data._lstChart, series, $scope.Item.Theme, $scope.Item.Type);

        if (!Common.HasValue(IsOpen)) {
            $scope.chart_win.center().open();
        }
    };

    $scope.Close_Click = function ($event) {
        $event.preventDefault();

        $scope.chart_win.close();
    }

    //#region excel
    $scope.Excel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerTotalSchedule_Index.URL.Template,
            data: { lstVendorid: $scope.Item.lstVendorID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }

    //#endregion
}]);