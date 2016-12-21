/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region
var _ORDDashboard_Index = {
    URL: {
        UserSetting_Get: 'Dashboard_UserSetting_Get',
        UserSetting_Save: 'Dashboard_UserSetting_Save',
        Summary_Data: 'Chart_Summary_Data',
        Customer_Order_Data: 'Chart_Customer_Order_Data',
        Customer_Order_Rate_Data: 'Chart_Customer_Order_Rate_Data',
        Customer_OPS_Data: 'Chart_Customer_OPS_Data',
        Owner_Capacity_Data: 'Chart_Owner_Capacity_Data',
        Owner_KM_Data: 'Chart_Owner_KM_Data',
        Owner_Operation_Data: 'Chart_Owner_Operation_Data',
        Owner_CostRate_Data: 'Chart_Owner_CostRate_Data',
        Owner_CostChange_Data: 'Chart_Owner_CostChange_Data',
        Owner_OnTime_PickUp_Data: 'Chart_Owner_OnTime_PickUp_Data',
        Owner_OnTime_Delivery_Data: 'Chart_Owner_OnTime_Delivery_Data',
        Owner_OnTime_POD_Data: 'Chart_Owner_OnTime_POD_Data',
        Owner_Return_Data: 'Chart_Owner_Return_Data',
        Owner_Profit_Data: 'Chart_Owner_Profit_Data',
        Owner_Profit_Customer_Data: 'Chart_Owner_Profit_Customer_Data',
        Owner_OnTime_PickUp_Radial_Data: 'Chart_Owner_OnTime_PickUp_Radial_Data',
        Owner_OnTime_Delivery_Radial_Data: 'Chart_Owner_OnTime_Delivery_Radial_Data',
        Owner_OnTime_POD_Radial_Data: 'Chart_Owner_OnTime_POD_Radial_Data',
        Owner_Profit_Vendor_Data: 'Chart_Owner_Profit_Vendor_Data',
        Read_Customer: 'REP_Customer_Read',
        Widget_Data: 'Widget_Data',

        Owner_Profit_Vendor_ExcelExport: 'Owner_Profit_Vendor_ExcelExport',
        Summary_ExcelExport: 'Summary_ExcelExport',
        Owner_Operation_ExcelExport: 'Owner_Operation_ExcelExport',
        Owner_Profit_Customer_ExcelExport: 'Owner_Profit_Customer_ExcelExport',
        Customer_Order_Rate_ExcelExport: 'Customer_Order_Rate_ExcelExport',
        Owner_Profit_Vendor_ExcelExport: 'Owner_Profit_Vendor_ExcelExport',
        Owner_Capacity_ExcelExport: 'Owner_Capacity_ExcelExport',
        Owner_CostRate_ExcelExport: 'Owner_CostRate_ExcelExport',
        Owner_OnTime_Delivery_Radial_ExcelExport: 'Owner_OnTime_Delivery_Radial_ExcelExport',
        Owner_OnTime_PickUp_ExcelExport: 'Owner_OnTime_PickUp_ExcelExport',
        Owner_Profit_ExcelExport: 'Owner_Profit_ExcelExport',
        Owner_CostChange_ExcelExport: 'Owner_CostChange_ExcelExport',
        Owner_OnTime_PickUp_Radial_ExcelExport: 'Owner_OnTime_PickUp_Radial_ExcelExport',
        Customer_OPS_ExcelExport: 'Customer_OPS_ExcelExport',
        Owner_KM_ExcelExport: 'Owner_KM_ExcelExport',
        Owner_OnTime_Delivery_ExcelExport: 'Owner_OnTime_Delivery_ExcelExport',
        Owner_OnTime_POD_Radial_ExcelExport: 'Owner_OnTime_POD_Radial_ExcelExport',
        Owner_OnTime_POD_ExcelExport: 'Owner_OnTime_POD_ExcelExport',
        Owner_Return_ExcelExport: 'Owner_Return_ExcelExport',
        Owner_Profit_Customer_ExcelExport: 'Owner_Profit_Customer_ExcelExport',
    },
    Data: {
        BackGroundColor: ["#ffffff", "#c3c3c3", "#b97a57", "#99d9ea", "#880015", "#ffc90e", "#22b14c", "#00a2e8", "#3f48cc", "#a349a4"],
        Color: ["#000000", "#000000", "#ffffff", "#000000", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff", "#ffffff"],
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
        TransportMode: [
               {
                   ID: 1, Name: 'LTL'
               },
               {
                   ID: 2, Name: 'FTL'
               },
               {
                   ID: 3, Name: 'FCL'
               },
               {
                   ID: 4, Name: 'LCL'
               },
        ],
        Status: [
           {
               ID: 1, Name: 'Yêu cầu'
           },
           {
               ID: 0, Name: 'Đang lập kế hoạch'
           },
           {
               ID: 2, Name: 'Đang vận chuyển'
           },
           {
               ID: 3, Name: 'Đã hoàn tất'
           },
           {
               ID: 4, Name: 'Đã nhận chứng từ'
           },
        ],
        TopCustomer: [
           {
               ID: 5, Name: 'Top 5'
           },
           {
               ID: 10, Name: 'Top 10'
           },
           {
               ID: 0, Name: 'Tất cả'
           },
        ],
        Finance: [
            { ID: 1, Code: "Doanh thu", Name: "Doanh thu" },
            { ID: 2, Code: "Chi phí", Name: "Chi phí" },
        ],
        TyepOfDate: [
            { ID: 1, Code: "days", Name: "Theo ngày" },
            { ID: 2, Code: "months", Name: "Theo tháng" },
        ],
        ListChart: [
            {
                ID: 1, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Tổng đơn hàng', Description: 'Tổng quan về số lượng đơn hàng được yêu cầu, phân loại theo loại hình vận chuyển',
                ListView: ["ViewCustomer", ],
            },
            //{ ID: 2, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Đơn hàng theo khách hàng', Description: 'Thống kê sản lượng yêu cầu vận chuyển của từng khách hàng cụ thể' },
            {
                ID: 3, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Sản lượng vận chuyển', Description: 'Tổng quan tình hình vận chuyển của đơn hàng theo từng trạng thái và loại hình vận chuyển',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 4, IsShow: true, IsPie: true, IsRadial: false, ChartName: 'Tỷ lệ đơn hàng yêu cầu', Description: 'Thống kê tỷ lệ đơn hàng được yêu cầu giữa các khách hàng trong khoảng thời gian',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 5, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Khả năng chuyên chở theo loại xe', Description: 'Thống kê khả năng chuyên chở của đội xe nhà theo từng loại xe',
                ListView: [],
            },
            {
                ID: 6, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'KM theo từng ngày', Description: 'Thống kê số KM chạy có tải, không tải theo ngày của đội xe nhà',
                ListView: [],
            },
            {
                ID: 7, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Thời gian vận hành', Description: 'Thống kê thời gian vận hành của xe nhà theo từng xe, phân bổ theo từng trạng thái',
                ListView: [],
            },
            {
                ID: 8, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Chi phí trên mỗi tấn/khối/km của xe', Description: 'Thống kê chi phí trên mỗi tấn/khối/KM của đội xe nhà theo từng xe',
                ListView: [],
            },
            {
                ID: 9, IsShow: true, IsPie: true, IsRadial: false, ChartName: 'Chi phí biến đổi', Description: 'Thống kê tỷ lệ giữa chi phí cố định và chi phí biến đổi của đội xe nhà',
                ListView: [],
            },
            {
                ID: 10, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Đơn hàng bốc đúng giờ theo ngày', Description: 'Thống kê tỷ lệ đơn hàng bốc đúng giờ theo từng ngày',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 11, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Đơn hàng giao đúng giờ theo ngày', Description: 'Thống kê tỷ lệ đơn hàng giao đúng giờ theo từng ngày',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 12, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Chứng từ giao đúng giờ theo ngày', Description: 'Thống kê tỷ lệ chứng từ giao đúng giờ theo từng ngày',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 13, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Thống kê hàng bị trả về', Description: 'Thống kê số lượng tấn, khối, đơn vị vận chuyển của hàng bị trả về do lỗi vận chuyển, hư hỏng',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 14, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Thống kê doanh thu, chi phí', Description: 'Thống kê doanh thu, chi phí theo từng khoản cụ thể',
                ListView: [],
            },
            {
                ID: 15, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Thống kê thu, chi theo khách hàng', Description: 'Thống kê doanh thu, chi phí theo từng khách hàng',
                ListView: [],
            },
            {
                ID: 16, IsShow: true, IsPie: false, IsRadial: true, ChartName: 'Tỷ lệ đơn hàng bốc đúng giờ', Description: 'Thống kê tỷ lệ đơn hàng bốc đúng',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 17, IsShow: true, IsPie: false, IsRadial: true, ChartName: 'Tỷ lệ đơn hàng giao đúng giờ', Description: 'Thống kê tỷ lệ đơn hàng giao đúng',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 18, IsShow: true, IsPie: false, IsRadial: true, ChartName: 'Tỷ lệ chứng từ giao đúng giờ', Description: 'Thống kê tỷ lệ chứng từ giao đúng',
                ListView: ["ViewCustomer", ],
            },
            {
                ID: 19, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Thống kê chi phí theo nhà thầu', Description: 'Thống kê chi phí theo từng nhà thầu',
                ListView: ["ViewVendor", ],
            },
            {
                ID: 20, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Tỷ lệ chi phí theo nhà thầu', Description: 'Tỷ lệ chi phí theo từng nhà thầu',
                ListView: ["ViewVendor", ],
            },
            {
                ID: 21, IsShow: true, IsPie: false, IsRadial: false, ChartName: 'Tỷ lệ doanh thu theo khách hàng', Description: 'Tỷ lệ doanh thu theo từng khách hàng',
                ListView: [],
            },
        ],
        ListWidget: [
            { ID: 1, IsShow: true, IsOps: false, Icon: "fa-wpforms", Color: "complete", ChartName: "Đơn hàng thành công", Total: 0, Unit: "Đơn hàng", Description: "Thống kê số lượng đơn hàng đã vận chuyển thành công" },
            { ID: 2, IsShow: true, IsOps: false, Icon: "fa-sitemap", Color: "plan", ChartName: "Đơn hàng lập kế hoạch", Total: 0, Unit: "Đơn hàng", Description: "Thống kế số lượng đơn hàng đang lập kế hoạch" },
            { ID: 3, IsShow: true, IsOps: true, Icon: "fa-truck", Color: "wait", ChartName: "Vận chuyển chờ duyệt", Total: 0, Unit: "Chuyến", Description: "Thống kế số chuyến đang chờ duyệt" },
            { ID: 4, IsShow: true, IsOps: true, Icon: "fa-truck", Color: "monitor", ChartName: "Vận chuyển đã duyệt", Total: 0, Unit: "Chuyến", Description: "Thống kế số chuyến đã duyệt" },
            { ID: 5, IsShow: true, IsOps: true, Icon: "fa-truck", Color: "cancel", ChartName: "Vận chuyển bị hủy", Total: 0, Unit: "Đơn hàng", Description: "Thống kế số đơn hàng bị hủy" },
            { ID: 6, IsShow: true, IsOps: true, Icon: "fa-truck", Color: "complete", ChartName: "Chuyến đã hoàn thành", Total: 0, Unit: "Chuyến", Description: "Thống kế số chuyến đã hoàn thành theo ngày thực tế giao hàng" },
        ],
        ListView: [
            { ID: 1, Code: "ViewAdmin" },
            { ID: 2, Code: "ViewCustomer" },
            { ID: 3, Code: "ViewVendor" },
            { ID: 4, Code: "ViewDriver" },
        ],
        DefColor: "#000000",
        DefBackGroundColor: "#ffffff",
        Interval: null,
        ListRadialSetting: [
            { Value: 40, Color: "#FF8382" },
            { Value: 80, Color: "#bbb138" },
            { Value: 100, Color: "#3ba752" }
        ]
    }
}
//#endregion

angular.module('myapp').controller('ORDDashboard_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', '$interval', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams, $interval) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDDashboard_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    var newDate = new Date();
    newDate.setHours(0, 0, 0, 0);

    $scope.UserSetting = { Layout: 1 };

    $scope.Chart = {
        DateFrom: newDate.addDays(-3),
        DateTo: newDate.addDays(3),
        CustomerID: null,
        TransportID: 1,
        StatusID: 1,
        TypeOfDateID: 1,

        IsShowCustomer: true,
        IsShowTransportMode: true,
        IsShowStatus: true,
        IsShowFinance: false,
        IsShowRadial: false,
        IsShowTypeOfDate: false,

        CurrentChart: "",
        Theme: 'Bootstrap',
        Type: 'column',
        BackGroundColor: "#ffffff",
        Range1: null,
        Range2: null,
        Range3: null,
        Color1: null,
        Color2: null,
        Color3: null,

        IsChartAdd: false,
        SearchText: "",
        ListChart: [],
    }

    $scope.Widget = {
        DateFrom: newDate.addDays(-3),
        DateTo: newDate.addDays(3),
        CustomerID: null,
        ListChart: [],
        Search: "",
    }

    $scope.cboCustomerOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "CodeName",
        dataValueField: "ID",
        placeholder: "Chọn khách hàng...",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        change: function () {

        }
    };

    $scope.cboTransportOptions = {
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Index.Data.TransportMode,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "Name",
        dataValueField: "ID",
        placeholder: "Chọn loại hình v.chuyển",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        change: function () {

        }
    };

    $scope.cboStatusOptions = {
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Index.Data.Status,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "Name",
        dataValueField: "ID",
        placeholder: "Chọn tình trạng",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        change: function () {

        }
    };

    $scope.cboCustomerTopOptions = {
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Index.Data.TopCustomer,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "Name",
        dataValueField: "ID",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        change: function () {

        }
    };

    $scope.cboFinanceOptions = {
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Index.Data.Finance,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "Name",
        dataValueField: "ID",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        change: function () {

        }
    };

    $scope.cboChartOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Name', dataValueField: 'Code', minLength: 3, index: 0, placeholder: 'Chọn chart',
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Index.Data.ChartType,
            model: {
                id: 'ID',
                fields: {
                    Code: { type: 'string' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function () {

        },
    };

    $scope.cboThemeOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Text', dataValueField: 'ID', minLength: 3, index: 0, placeholder: 'Chọn theme',
        dataSource: Common.DataSource.Local({
            data: _ORDDashboard_Index.Data.Theme,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'string' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function () {

        },
    };

    $scope.chartDefaultOptions =
        {
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
                    lock: "y",
                }
            },
            dataBound: function (e) {

            }
        };

    $scope.cpkColorOptions = {
        palette: _ORDDashboard_Index.Data.BackGroundColor,
        change: function (e) {

        }
    };


    $scope.widget_sortOptions = {
        change: function (e) {
            var fromID = e.oldIndex;
            var toID = e.newIndex;
            var temp = $scope.UserSetting.ListWidget[fromID];
            $scope.UserSetting.ListWidget[fromID] = $scope.UserSetting.ListWidget[toID];
            $scope.UserSetting.ListWidget[toID] = temp;
            angular.forEach($scope.UserSetting.ListWidget, function (v, i) {
                v.ChartID = i + 1;
                v.ChartKey = "Widget" + "_" + v.ChartID;
            });
            $scope.SaveUserSetting(false);
        },
        filter: ".repeat",
        container: $(".dashboard-widget-mid")
    };

    $scope.widget_placeholder = function (element) {
        return element.clone().addClass("widget-placeholder").text("drop here");
    };

    $scope.widget_hint = function (element) {
        return element.clone().addClass("widget-hint");
    };


    $scope.chart_sortOptions = {
        change: function (e) {
            var fromID = e.oldIndex;
            var toID = e.newIndex;
            var temp = $scope.UserSetting.ListChart[fromID];
            $scope.UserSetting.ListChart[fromID] = $scope.UserSetting.ListChart[toID];
            $scope.UserSetting.ListChart[toID] = temp;
            angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                v.ChartID = i + 1;
                v.ChartKey = $scope.UserSetting.Layout + "_" + v.ChartID;
            });
            $scope.SaveUserSetting(false);

            $(document).find('.cus-chart').each(function (i, o) {
                try {
                    $(o).data('kendoChart').resize();
                } catch (e) { }
            });
        },
        cursor: "move",
        filter: ".cus-form",
        handler: ".form-header",
        container: $(".dashboard-chart"),
        autoScroll: true
    };

    $scope.chart_placeholder = function (element) {
        return element.clone().addClass("chart-placeholder").text("drop here");
    };

    $scope.chart_hint = function (element) {
        return element.clone().addClass("chart-hint");
    };

    $scope.draggableHint = function (e) {
        var chartKey = this.currentTarget.attr("chart-key");
        if (Common.HasValue(chartKey)) {
            var item = null;
            angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                if (v.ChartKey == chartKey) {
                    item = v;
                    return true;
                }
            });
            if (Common.HasValue(item)) {
                var hintElement = $("<div id='hint'>" + item.ChartName + "</div>");
                hintElement.css({
                    "width": "auto",
                    "background-color": item.BackGroundColor,
                    "color": item.Color,
                    "text-align": "center",
                    "line-height": "30px",
                    "padding": "5px",
                    "font-weight": "bold"
                });
                return hintElement;
            }
        }
    }

    $scope.onDrop = function (e) {
        var targetChartKey = e.draggable.currentTarget.attr("chart-key");
        var dropChartKey = e.dropTarget.find(".cus-form").attr("id");
        if (Common.HasValue(targetChartKey) && Common.HasValue(dropChartKey) && targetChartKey != dropChartKey) {
            var fromID = targetChartKey.split("_")[1] - 1;
            var toID = dropChartKey.split("_")[1] - 1;
            var temp = $scope.UserSetting.ListChart[fromID];
            $scope.UserSetting.ListChart[fromID] = $scope.UserSetting.ListChart[toID];
            $scope.UserSetting.ListChart[toID] = temp;
            angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                v.ChartID = i + 1;
                v.ChartKey = $scope.UserSetting.Layout + "_" + v.ChartID;
            });
            $scope.SaveUserSetting(true);
        }
    }

    $scope.dragOptions = {
        container: $(".dashboard-chart"),
        ignore: ".form-body",
        autoScroll: true,
        distance: 0
    }

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _ORDDashboard_Index.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.cboCustomerOptions.dataSource.data(res.Data);
            $scope.LoadUserSetting();
        }
    });

    $scope.AutoSlider = function () {
        if (!Common.HasValue(_ORDDashboard_Index.Data.Interval) && $scope.UserSetting.ListWidget.length > 4) {
            _ORDDashboard_Index.Data.Interval = $interval(function () {
                var view = $("div.dashboard-widget-mid-scroll");
                var width = 265;
                var base = 1060;
                if ($scope.UserSetting.ListWidget.length > 4) {
                    var totalRight = ($scope.UserSetting.ListWidget.length - 4) * width;
                    var marginLeft = parseInt(view.css("margin-left"));
                    if (marginLeft == 0 || (marginLeft < 0 && -marginLeft < totalRight))
                        view.animate({ marginLeft: '-=' + width + "px" }, 400);
                    else
                        if (marginLeft != 0)
                            view.animate({ marginLeft: '+=' + width + "px" }, 400);
                } else {
                    if (marginLeft < 0)
                        view.animate({ marginLeft: '+=' + width + "px" }, 400);
                }
            }, 10000);
        }
    };

    // Load thông tin setting của dashboard theo user
    $scope.LoadUserSetting = function () {
        Common.Log("Load user setting");

        if ($rootScope.FunctionItem.ID > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDDashboard_Index.URL.UserSetting_Get,
                data: { functionID: $rootScope.FunctionItem.ID },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $timeout(function () {
                            $scope.UserSetting = res;
                            $rootScope.IsLoading = false;
                            $scope.LoadChart();
                        }, 10);
                    }
                }
            });
        }
    }

    // Lưu thông tin setting dashboard của user
    $scope.SaveUserSetting = function (IsReloadSetting) {
        Common.Log("Save user setting");
        if (IsReloadSetting)
            $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDDashboard_Index.URL.UserSetting_Save,
            data: { item: $scope.UserSetting },
            success: function (res) {
                $rootScope.IsLoading = false;
                if (IsReloadSetting)
                    $scope.LoadUserSetting();
            }
        });
    };

    // Vẽ Radial Gauge
    $scope.InitRadialGauge = function (v, isZoom) {
        var chartBox = null;
        if (!Common.HasValue(isZoom))
            chartBox = angular.element($("#" + v.ChartKey));
        else
            chartBox = angular.element($("#chartZoom"));

        if (Common.HasValue(chartBox) && chartBox.length > 0) {
            var chartName = angular.element(chartBox[0].querySelector('.chartName'));
            var chartDiv = angular.element(chartBox[0].querySelector('.cus-chart'));
            var gaugeValue = angular.element(chartBox[0].querySelector('.radial-gauge-value'));
            var formHeader = angular.element(chartBox[0].querySelector('.form-header'));
            var formBody = angular.element(chartBox[0].querySelector('.form-body'));
            var iConfig = angular.element(chartBox[0].querySelector('i.config'));
            var zConfig = angular.element(chartBox[0].querySelector('i.zoom-config'));
            if (Common.HasValue(chartName)) {
                $(chartName).text(v.ChartName);
                $(chartName).css("color", v.Color);
            }
            if (Common.HasValue(gaugeValue)) {
                $(gaugeValue).text(v.Value + "%");
            }
            if (Common.HasValue(formHeader)) {
                $(formHeader).css("background-color", v.BackGroundColor);
            }
            if (Common.HasValue(formBody)) {
                $(formBody).css("border-color", v.BorderColor);
            }
            if (Common.HasValue(iConfig)) {
                $(iConfig).css("color", v.Color);
            }
            if (Common.HasValue(zConfig)) {
                $(zConfig).css("color", v.Color);
            }
            if (Common.HasValue(chartDiv)) {
                if (v.IsShow) {
                    var height = $(chartDiv).css("height");
                    var gaugeBottom = parseFloat(height) * 0.4 + 17;
                    chartDiv.addClass("radial-gauge");
                    formBody.addClass("radial-gauge");
                    chartDiv.show();

                    //$(gaugeValue).css("bottom", gaugeBottom);
                    gaugeValue.show();

                    var chart = $(chartDiv).data("kendoRadialGauge");
                    // distroy existing chart
                    if (chart != null) {
                        chart.destroy();
                    }

                    $(chartDiv).kendoRadialGauge({
                        pointer: [],
                        scale: {
                            minorUnit: 5,
                            startAngle: 0,
                            endAngle: 180,
                            max: 100,
                            rangeSize: 30,
                            rangeDistance: -10,
                            labels: {
                                visible: false,
                                template: "#= value #%"
                            },
                            majorTicks: {
                                visible: false
                            },
                            minorTicks: {
                                visible: false
                            },
                            rangePlaceholderColor: "#b1b2b3",
                            ranges: v.Ranges
                        }
                    });

                    chart = $(chartDiv).data("kendoRadialGauge");
                    if (Common.HasValue(chart)) {
                        $timeout(function () {
                            chart.resize();
                        }, 10);
                    }
                }
            }
        }
    };

    // Vẽ chart
    $scope.InitChart = function (v, isZoom) {
        var chartBox = null;
        if (!Common.HasValue(isZoom))
            chartBox = angular.element($("#" + v.ChartKey));
        else
            chartBox = angular.element($("#chartZoom"));

        if (Common.HasValue(chartBox) && chartBox.length > 0) {
            var chartName = angular.element(chartBox[0].querySelector('.chartName'));
            var chartDiv = angular.element(chartBox[0].querySelector('.cus-chart'));
            var gaugeValue = angular.element(chartBox[0].querySelector('.radial-gauge-value'));
            var formHeader = angular.element(chartBox[0].querySelector('.form-header'));
            var formBody = angular.element(chartBox[0].querySelector('.form-body'));
            var iConfig = angular.element(chartBox[0].querySelector('i.config'));
            var zConfig = angular.element(chartBox[0].querySelector('i.zoom-config'));
            if (Common.HasValue(chartName)) {
                $(chartName).text(v.ChartName);
                $(chartName).css("color", v.Color);
            }
            if (Common.HasValue(formHeader)) {
                $(formHeader).css("background-color", v.BackGroundColor);
            }
            if (Common.HasValue(formBody)) {
                $(formBody).css("border-color", v.BorderColor);
            }
            if (Common.HasValue(iConfig)) {
                $(iConfig).css("color", v.Color);
            }
            if (Common.HasValue(zConfig)) {
                $(zConfig).css("color", v.Color);
            }
            if (Common.HasValue(chartDiv)) {
                if (v.IsShow) {
                    chartDiv.removeClass("radial-gauge");
                    formBody.removeClass("radial-gauge");
                    chartDiv.show();
                    if (Common.HasValue(gaugeValue))
                        gaugeValue.hide();
                    var chart = $(chartDiv).data("kendoChart");
                    // distroy existing chart
                    if (chart != null) {
                        chart.destroy();
                    }

                    var categoryAxis = {};
                    var tooltip = {};
                    var legend = { visible: true, position: "bottom" };
                    var valueAxis = { line: { visible: false }, minorGridLines: { visible: false }, majorGridLines: { visible: false }, labels: { rotation: "auto" } };
                    var seriesDefaults = { type: v.ChartType };
                    var axisDefaults = {
                        majorGridLines: {
                            visible: false
                        }
                    };

                    if (v.IsPie) {
                        tooltip = { visible: true, template: "${ category }: ${ dataItem.Total } " + v.Unit + ", ${ value }%" };
                        legend = { visible: false };
                    }
                    else {
                        tooltip = { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" };
                        categoryAxis = {
                            field: "Date", type: "category",
                            majorGridLines: { visible: false },
                            baseUnit: 'days',
                            labels: {
                                rotation: "auto",
                                format: "d/M"
                            },
                            min: 0
                        };
                    }

                    if (Common.HasValue(v.CategoryAxis))
                        categoryAxis = v.CategoryAxis;
                    if (Common.HasValue(v.ValueAxis))
                        valueAxis = v.ValueAxis;
                    if (Common.HasValue(v.Tooltip))
                        tooltip = v.Tooltip;
                    if (Common.HasValue(v.Legend))
                        legend = v.Legend;
                    if (Common.HasValue(v.SeriesDefaults))
                        seriesDefaults = v.SeriesDefaults;

                    $(chartDiv).kendoChart({
                        dataSource: {
                            data: v.Data,
                        },
                        series: v.Series,
                        legend: legend,
                        tooltip: tooltip,
                        categoryAxis: categoryAxis,
                        seriesDefaults: seriesDefaults,
                        valueAxis: valueAxis,
                        axisDefaults: axisDefaults,
                        pannable: {
                            lock: "y"
                        },
                        zoomable: {
                            mousewheel: {
                                lock: "y"
                            },
                            selection: {
                                lock: "y",
                            }
                        },
                        dataBound: function (e) {

                        }
                    });

                    chart = $(chartDiv).data("kendoChart");
                    if (Common.HasValue(chart)) {
                        chart.setOptions({ theme: v.ChartTheme });
                        chart.refresh();
                        $timeout(function () {
                            chart.resize();
                        }, 10);
                    }
                }
            }
        }
    };

    // Load thông tin các chart được thiết lập
    $scope.LoadChart = function () {
        Common.Log("Load chart");
        $rootScope.IsLoading = true;
        var filter = { CustomerID: null, TransportModeID: 1, StatusID: 1, DateFrom: $scope.Chart.DateFrom, DateTo: $scope.Chart.DateTo, CustomerTopID: 0, FinanceID: 1, TypeOfDateID: 1 };

        if (Common.HasValue($scope.UserSetting)) {
            $timeout(function () {
                var divider = $scope.UserSetting.Layout;
                if ($scope.UserSetting.Layout == 4)
                    divider = 2;
                if ($scope.UserSetting.Layout == 5)
                    divider = 3;
                angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                    // Ktra loại chart
                    angular.forEach(_ORDDashboard_Index.Data.ListChart, function (chart, idx) {
                        if (v.TypeOfChart == chart.ID) {
                            v.IsPie = chart.IsPie;
                            v.IsRadial = chart.IsRadial;
                            v.ChartName = chart.ChartName;
                            return true;
                        }
                    });

                    // Set các thông tin mặc định
                    v.IsShow = v.IsShowChartType = true;
                    v.IsShowCustomer = v.IsShowTransportMode = v.IsShowCustomer = v.IsShowStatus = v.IsShowFinance = v.IsShowRadial = false;
                    v.Series = [];
                    v.Layout = $scope.UserSetting.Layout;
                    v.Index = v.ChartID % divider;

                    if (!Common.HasValue(v.BackGroundColor) || v.BackGroundColor == "")
                        v.BackGroundColor = _ORDDashboard_Index.Data.DefBackGroundColor;
                    if (!Common.HasValue(v.Color) || v.Color == "")
                        v.Color = _ORDDashboard_Index.Data.DefColor;

                    if (v.BackGroundColor == _ORDDashboard_Index.Data.DefBackGroundColor)
                        v.BorderColor = "#dadada";
                    else
                        v.BorderColor = v.BackGroundColor;

                    if (!Common.HasValue(v.Filter))
                        v.Filter = filter;

                    if (v.IsRadial) {
                        v.IsShowRadial = true;
                        if (Common.HasValue(v.ListRadialSetting) && v.ListRadialSetting.length == 3) {

                        } else {
                            v.ListRadialSetting = _ORDDashboard_Index.Data.ListRadialSetting;
                        }
                    }

                    // Tùy theo chart sẽ vẽ khác nhau
                    switch (v.TypeOfChart) {
                        // Tổng đơn hàng
                        case 1:
                            v.IsShowCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Summary_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        var average = { Date: "Trung bình", TotalLTL: 0, TotalFTL: 0, TotalFCL: 0, TotalLCL: 0 };
                                        angular.forEach(res.ListData, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            average.TotalLTL += v.TotalLTL;
                                            average.TotalFTL += v.TotalFTL;
                                            average.TotalFCL += v.TotalFCL;
                                            average.TotalLCL += v.TotalLCL;
                                        });
                                        if (res.ListData.length > 0)
                                        {
                                            average.TotalLTL = (average.TotalLTL / res.ListData.length).toFixed(1);
                                            average.TotalFTL = (average.TotalFTL / res.ListData.length).toFixed(1);
                                            average.TotalFCL = (average.TotalFCL / res.ListData.length).toFixed(1);
                                            average.TotalLCL = (average.TotalLCL / res.ListData.length).toFixed(1);
                                        }
                                        res.ListData.push(average);
                                        v.Data = res.ListData;
                                        v.Series.push({ name: 'LTL', field: "TotalLTL" });
                                        v.Series.push({ name: 'FTL', field: "TotalFTL" });
                                        v.Series.push({ name: 'FCL', field: "TotalFCL" });
                                        v.Series.push({ name: 'LCL', field: "TotalLCL" });

                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Đơn hàng theo khách hàng
                        case 2:
                            v.IsShowCustomer = true;
                            v.IsShowTransportMode = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Customer_Order_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        // LTL
                                        if (v.Filter.TransportModeID == 1) {
                                            v.Series.push({ name: 'Tấn', field: "Ton" });
                                            v.Series.push({ name: 'Khối', field: "CBM" });
                                            angular.forEach(res.ListLTL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            });
                                            v.Data = res.ListLTL;
                                            $scope.InitChart(v);
                                        }
                                        // FTL
                                        if (v.Filter.TransportModeID == 2) {
                                            v.Series.push({ name: 'Số lượng đơn', field: "Total" });
                                            angular.forEach(res.ListFTL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            });
                                            v.Data = res.ListFTL;
                                            $scope.InitChart(v);
                                        }
                                        // FCL
                                        if (v.Filter.TransportModeID == 3) {
                                            v.Series.push({ name: '20DC', field: "Total20DC" });
                                            v.Series.push({ name: '40DC', field: "Total40DC" });
                                            v.Series.push({ name: '40HC', field: "Total40HC" });
                                            angular.forEach(res.ListFCL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            });
                                            v.Data = res.ListFCL;
                                            $scope.InitChart(v);
                                        }
                                        // LCL
                                        if (v.Filter.TransportModeID == 4) {
                                            v.Series.push({ name: '20DC', field: "Total20DC" });
                                            v.Series.push({ name: '40DC', field: "Total40DC" });
                                            v.Series.push({ name: '40HC', field: "Total40HC" });
                                            angular.forEach(res.ListLCL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            });
                                            v.Data = res.ListLCL;
                                            $scope.InitChart(v);
                                        }
                                    }
                                }
                            });
                            break;
                            // Tình hình vận chuyển
                        case 3:
                            v.IsShowCustomer = true;
                            v.IsShowTransportMode = true;
                            v.IsShowStatus = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Customer_OPS_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID, statusid: v.Filter.StatusID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        var average = { Date: "Trung bình", Ton: 0, CBM: 0 };
                                        // LTL
                                        if (v.Filter.TransportModeID == 1) {
                                            v.Series.push({ name: 'Tấn', field: "Ton" });
                                            v.Series.push({ name: 'Khối', field: "CBM" });
                                            angular.forEach(res.ListLTL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                                average.Ton += v.Ton;
                                                average.CBM += v.CBM;
                                            });
                                            if (res.ListLTL.length > 0) {
                                                average.Ton = (average.Ton / res.ListLTL.length).toFixed(1);
                                                average.CBM = (average.CBM / res.ListLTL.length).toFixed(1);
                                            }
                                            res.ListLTL.push(average);
                                            v.Data = res.ListLTL;
                                            $scope.InitChart(v);
                                        }
                                        // FTL
                                        if (v.Filter.TransportModeID == 2) {
                                            v.Series.push({ name: 'Số lượng đơn', field: "Total" });
                                            angular.forEach(res.ListFTL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            });
                                            v.Data = res.ListFTL;
                                            $scope.InitChart(v);
                                        }
                                        // FCL
                                        if (v.Filter.TransportModeID == 3) {
                                            v.Series.push({ name: '20DC', field: "Total20DC" });
                                            v.Series.push({ name: '40DC', field: "Total40DC" });
                                            v.Series.push({ name: '40HC', field: "Total40HC" });
                                            angular.forEach(res.ListFCL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            });
                                            v.Data = res.ListFCL;
                                            $scope.InitChart(v);
                                        }
                                        // LCL
                                        if (v.Filter.TransportModeID == 4) {
                                            v.Series.push({ name: '20DC', field: "Total20DC" });
                                            v.Series.push({ name: '40DC', field: "Total40DC" });
                                            v.Series.push({ name: '40HC', field: "Total40HC" });
                                            angular.forEach(res.ListLCL, function (v, i) {
                                                v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            });
                                            v.Data = res.ListLCL;
                                            $scope.InitChart(v);
                                        }
                                    }
                                }
                            });
                            break;
                            // Tỷ lệ đơn hàng
                        case 4:
                            v.IsShowChartType = false;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Customer_Order_Rate_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, quantity: v.Filter.CustomerTopID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        v.Data = res.ListData;
                                        v.Series.push({
                                            type: "pie",
                                            field: "Percent",
                                            categoryField: "CustomerName",
                                        });
                                        v.Unit = "đơn";
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Khả năng chuyên chở
                        case 5:
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Capacity_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        v.Series.push({ name: 'Tỉ lệ tấn', field: "ValueTon" });
                                        v.Series.push({ name: 'Tỉ lệ khối', field: "ValueCBM" });
                                        v.Data = res.ListData;
                                        v.CategoryAxis = {
                                            field: "GroupOfVehicleCode", type: "category",
                                            majorGridLines: { visible: false },
                                            min: 0
                                        };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // KM theo từng ngày
                        case 6:
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_KM_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        var average = { Date: "Trung bình", KMLaden: 0, KMEmpty: 0 };
                                        v.Series.push({ name: 'Có hàng', field: "KMLaden" });
                                        v.Series.push({ name: 'Không hàng', field: "KMEmpty" });
                                        angular.forEach(res.ListData, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            average.KMLaden += v.KMLaden;
                                            average.KMEmpty += v.KMEmpty;
                                        });
                                        if (res.ListData.length > 0)
                                        {
                                            average.KMLaden = (average.KMLaden / res.ListData.length).toFixed(1);
                                            average.KMEmpty = (average.KMEmpty / res.ListData.length).toFixed(1);
                                        }
                                        res.ListData.push(average);
                                        v.Data = res.ListData;
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Thời gian vận hành
                        case 7:
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Operation_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        v.Series.push({ name: 'Tổng giờ', field: "TotalTime" });
                                        v.Series.push({ name: 'T.g chạy', field: "RunningTime" });
                                        v.Series.push({ name: 'T.g bốc xếp', field: "LoadingTime" });
                                        v.Series.push({ name: 'T.g chờ', field: "WaittingTime" });
                                        v.Series.push({ name: 'T.g thừa', field: "OtherTime" });
                                        v.Data = res.ListData;
                                        v.CategoryAxis = {
                                            field: "VehicleCode", type: "category",
                                            majorGridLines: { visible: false },
                                            min: 0, max: 3,
                                            labels: {
                                                rotation: "-90"
                                            }
                                        };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 8: Chi phí trên mỗi tấn/khối/km của xe
                        case 8:
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_CostRate_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        var average = { VehicleCode: "Trung bình", KMIndex: 0, TonIndex: 0, CBMIndex: 0 };
                                        v.Series.push({ name: 'C.phí mỗi KM', field: "KMIndex" });
                                        v.Series.push({ name: 'C.phí mỗi Tấn', field: "TonIndex" });
                                        v.Series.push({ name: 'C.phí mỗi Khối', field: "CBMIndex" });
                                        angular.forEach(res.ListData, function (v, i) {
                                            average.KMIndex += v.KMIndex;
                                            average.TonIndex += v.TonIndex;
                                            average.CBMIndex += v.CBMIndex;
                                        });
                                        if (res.ListData.length > 0) {
                                            average.KMIndex = (average.KMIndex / res.ListData.length).toFixed(1);
                                            average.TonIndex = (average.TonIndex / res.ListData.length).toFixed(1);
                                            average.CBMIndex = (average.CBMIndex / res.ListData.length).toFixed(1);
                                        }
                                        res.ListData.push(average);
                                        v.Data = res.ListData;
                                        v.CategoryAxis = {
                                            field: "VehicleCode", type: "category",
                                            majorGridLines: { visible: false },
                                            min: 0, max: 3,
                                            labels: {
                                                rotation: "-90"
                                            }
                                        };
                                        v.Tooltip = { visible: true, template: "#= series.name #: #= Common.Number.ToMoney(value) # đ" };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 9: Chi phí biến đổi
                        case 9:
                            v.IsShowChartType = false;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_CostChange_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        v.Data = res.ListData;
                                        v.Series.push({
                                            type: "pie",
                                            field: "Percent",
                                            categoryField: "Category",
                                        });
                                        v.Legend = { visible: true, position: "bottom" };
                                        v.Tooltip = { visible: true, template: "${ category }: ${ Common.Number.ToMoney(dataItem.Total) } đ" + ", ${ value }%" };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 10: Tỉ lệ đơn hàng bốc đúng giờ
                        case 10:
                            v.IsShowCustomer = true;
                            v.IsShowChartType = false;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_OnTime_PickUp_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];

                                        angular.forEach(res.ListData, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Series.push({ name: 'Tổng đơn', field: "Total", type: "column", axis: "Total", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" } });
                                        v.Series.push({ name: 'Tỉ lệ đúng giờ', field: "Percent", type: "line", axis: "Percent", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #%" } });
                                        v.Data = res.ListData;
                                        v.ValueAxis = [
                                            {
                                                title: { text: "Tổng đơn", font: "12px sans-serif", },
                                                name: "Total",
                                            },
                                            {
                                                name: "Percent",
                                                title: { text: "Tỉ lệ đúng giờ", font: "12px sans-serif", },
                                                min: 0,
                                                max: 100,
                                            }];
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 11: Tỉ lệ đơn hàng giao đúng giờ
                        case 11:
                            v.IsShowCustomer = true;
                            v.IsShowChartType = false;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_OnTime_Delivery_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        angular.forEach(res.ListData, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Series.push({ name: 'Tổng đơn', field: "Total", type: "column", axis: "Total", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" } });
                                        v.Series.push({ name: 'Tỉ lệ đúng giờ', field: "Percent", type: "line", axis: "Percent", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #%" } });
                                        v.Data = res.ListData;
                                        v.ValueAxis = [
                                            {
                                                title: { text: "Tổng đơn", font: "12px sans-serif", },
                                                name: "Total",
                                            },
                                            {
                                                name: "Percent",
                                                title: { text: "Tỉ lệ đúng giờ", font: "12px sans-serif", },
                                                min: 0,
                                                max: 100,
                                            }];
                                        v.CategoryAxis = {
                                            field: "Date", type: "category",
                                            majorGridLines: { visible: false },
                                            baseUnit: 'days',
                                            labels: {
                                                rotation: "auto",
                                                format: "d/M"
                                            },
                                            min: 0,
                                            axisCrossingValue: [0, 1000]
                                        };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 12: Tỉ lệ chứng từ giao đúng giờ
                        case 12:
                            v.IsShowCustomer = true;
                            v.IsShowChartType = false;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_OnTime_POD_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        angular.forEach(res.ListData, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Series.push({ name: 'Tổng c.từ', field: "Total", type: "column", axis: "Total", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" } });
                                        v.Series.push({ name: 'Tỉ lệ đúng giờ', field: "Percent", type: "line", axis: "Percent", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #%" } });
                                        v.Data = res.ListData;
                                        v.ValueAxis = [
                                            {
                                                title: { text: "Tổng c.từ", font: "12px sans-serif", },
                                                name: "Total",
                                            },
                                            {
                                                name: "Percent",
                                                title: { text: "Tỉ lệ đúng giờ", font: "12px sans-serif", },
                                                min: 0,
                                                max: 100,
                                            }];
                                        v.CategoryAxis = {
                                            field: "Date", type: "category",
                                            majorGridLines: { visible: false },
                                            baseUnit: 'days',
                                            labels: {
                                                rotation: "auto",
                                                format: "d/M"
                                            },
                                            min: 0,
                                            axisCrossingValue: [0, 1000]
                                        };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 13: Hàng trả về
                        case 13:
                            v.IsShowCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Return_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        angular.forEach(res.ListData, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Series.push({ name: 'Tấn', field: "Ton" });
                                        v.Series.push({ name: 'Khối', field: "CBM" });
                                        v.Series.push({ name: 'Số lượng', field: "Quantity" });
                                        v.Data = res.ListData;
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 14: Doanh thu, chi phí
                        case 14:
                            v.IsShowCustomer = true;
                            v.IsShowChartType = false;
                            v.IsShowFinance = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Profit_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];

                                        v.Data = res.ListData;
                                        var max, min, majorUnit = 0;
                                        if (v.Filter.FinanceID == 1) {
                                            angular.forEach(res.ListData, function (o, i) {
                                                max = max + o.Credit;
                                            });
                                            max = max + 1000000;
                                            majorUnit = (max - min) / 5;
                                        } else {
                                            angular.forEach(res.ListData, function (o, i) {
                                                max = max + o.Debit;
                                            });
                                            max = max + 1000000;
                                            majorUnit = (max - min) / 5;
                                        }

                                        v.Data.push({ Category: "Tổng", Summary: "total" });

                                        if (v.Filter.FinanceID == 1) {
                                            v.Series.push({
                                                type: "horizontalWaterfall",
                                                field: "Credit",
                                                categoryField: "Category",
                                                summaryField: "Summary",
                                            });
                                        } else {
                                            v.Series.push({
                                                type: "horizontalWaterfall",
                                                field: "Debit",
                                                categoryField: "Category",
                                                summaryField: "Summary",
                                            });
                                        }

                                        v.Tooltip = { visible: true, template: "#= Common.Number.ToMoney(value) # đ" };
                                        v.ValueAxis = {
                                            labels: {
                                                template: "#: kendo.toString(value, 'n0') #",
                                                rotation: "90"
                                            },
                                            min: min,
                                            max: max,
                                            majorUnit: majorUnit,
                                        };
                                        v.Legend = { visible: false };
                                        v.SeriesDefaults = {};
                                        v.CategoryAxis = {};

                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 15: Doanh thu, chi phí theo từng khách hàng
                        case 15:
                            v.IsShowCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Profit_Customer_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];

                                        v.Series.push({ name: 'Doanh thu', field: "Credit" });
                                        v.Series.push({ name: 'Chi phí', field: "Debit" });
                                        v.Data = res.ListData;
                                        v.CategoryAxis = {
                                            field: "CustomerCode", type: "category",
                                            majorGridLines: { visible: false },
                                            min: 0,
                                            //labels: {
                                            //    rotation: "90"
                                            //}
                                        };
                                        v.Tooltip = { visible: true, template: "#= series.name #: #= Common.Number.ToMoney(value) # đ" };

                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 16: Tỉ lệ đơn hàng bốc đúng giờ
                        case 16:
                            v.IsShowCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_OnTime_PickUp_Radial_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Value = res.Percent;
                                        var color = v.ListRadialSetting[0].Color;
                                        angular.forEach(v.ListRadialSetting, function (o, i) {
                                            if (v.Value >= o.Value)
                                                color = o.Color;
                                        });
                                        v.Ranges = [{
                                            from: 0,
                                            to: v.Value,
                                            color: color
                                        }];
                                        $scope.InitRadialGauge(v);
                                    }
                                }
                            });
                            break;
                            // Chart 17: Tỉ lệ đơn hàng giao đúng giờ
                        case 17:
                            v.IsShowCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_OnTime_Delivery_Radial_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Value = res.Percent;
                                        var color = v.ListRadialSetting[0].Color;
                                        angular.forEach(v.ListRadialSetting, function (o, i) {
                                            if (v.Value >= o.Value)
                                                color = o.Color;
                                        });
                                        v.Ranges = [{
                                            from: 0,
                                            to: v.Value,
                                            color: color
                                        }];
                                        $scope.InitRadialGauge(v);
                                    }
                                }
                            });
                            break;
                            // Chart 18: Tỉ lệ chứng từ giao đúng giờ
                        case 18:
                            v.IsShowCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_OnTime_POD_Radial_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Value = res.Percent;
                                        var color = v.ListRadialSetting[0].Color;
                                        angular.forEach(v.ListRadialSetting, function (o, i) {
                                            if (v.Value >= o.Value)
                                                color = o.Color;
                                        });
                                        v.Ranges = [{
                                            from: 0,
                                            to: v.Value,
                                            color: color
                                        }];
                                        $scope.InitRadialGauge(v);
                                    }
                                }
                            });
                            break;
                            // Chart 19: Chi phí theo từng nhà thầu
                        case 19:
                            v.IsShowCustomer = false;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Profit_Vendor_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo},
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];

                                        v.Series.push({ name: 'Chi phí', field: "Debit" });
                                        v.Data = res.ListData;
                                        v.CategoryAxis = {
                                            field: "CustomerCode", type: "category",
                                            majorGridLines: { visible: false },
                                            min: 0,
                                        };
                                        v.Tooltip = { visible: true, template: "#= series.name #: #= Common.Number.ToMoney(value) # đ" };

                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 20: Tỉ lệ chi phí theo từng nhà thầu
                        case 20:
                            v.IsShowCustomer = false;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Profit_Vendor_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo},
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        v.Data = res.ListData;
                                        v.Series.push({
                                            type: "pie",
                                            field: "DebitPercent",
                                            categoryField: "CustomerCode",
                                        });
                                        v.Tooltip = { visible: true, template: "${ category }: ${ dataItem.Credit } đ" + ", ${ value }%" };
                                        v.Legend = { visible: false };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                            // Chart 21: Tỉ lệ doanh thu theo từng khách hàng
                        case 21:
                            v.IsShowCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.ORD,
                                method: _ORDDashboard_Index.URL.Owner_Profit_Customer_Data,
                                data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID},
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        v.Data = [];
                                        v.Series = [];
                                        angular.forEach(res.ListData, function (o, i) {
                                            if (o.CreditPercent > 0)
                                                v.Data.push(o);
                                        });
                                        v.Series.push({
                                            type: "pie",
                                            field: "CreditPercent",
                                            categoryField: "CustomerCode",
                                        });
                                        v.Tooltip = { visible: true, template: "${ category }: ${ dataItem.Credit } đ" + ", ${ value }%" };
                                        v.Legend = { visible: false };
                                        $scope.InitChart(v);
                                    }
                                }
                            });
                            break;
                    }
                });
                $rootScope.IsLoading = false;

                $scope.LoadWidget();
            }, 10);
        } else
            $rootScope.IsLoading = false;

        $rootScope.IsLoading = false;
    };

    //Excel
    $scope.ChartExcel_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var chartKey = $($event.currentTarget).data("key");
        var arrKey = chartKey.split("_");
        angular.forEach($scope.UserSetting.ListChart, function (v, i) {
            if (v.ChartKey == chartKey) {
                switch (v.TypeOfChart) {
                    case 1:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Summary_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    if (Common.HasValue(res)) {
                                        $rootScope.DownloadFile(res);
                                        $rootScope.IsLoading = false;
                                    }
                                }
                            }
                        });
                        break;

                    case 2:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Customer_Order_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                
                                }
                            }
                        });
                        break;

                    case 3:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Customer_OPS_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID, statusid: v.Filter.StatusID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;

                    case 4:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Customer_Order_Rate_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, quantity: v.Filter.CustomerTopID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                    case 5:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Capacity_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                    case 6:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_KM_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                    case 7:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Operation_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 8: Chi phí trên mỗi tấn/khối/km của xe
                    case 8:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_CostRate_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 9: Chi phí biến đổi
                    case 9:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_CostChange_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 10: Tỉ lệ đơn hàng bốc đúng giờ
                    case 10:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_PickUp_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 11: Tỉ lệ đơn hàng giao đúng giờ
                    case 11:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_Delivery_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 12: Tỉ lệ chứng từ giao đúng giờ
                    case 12:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_POD_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 13: Hàng trả về
                    case 13:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Return_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 14: Doanh thu, chi phí
                    case 14:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 15: Doanh thu, chi phí theo từng khách hàng
                    case 15:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Customer_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID,TypeOfChart: v.TypeOfChart  },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 16: Tỉ lệ đơn hàng bốc đúng giờ
                    case 16:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_PickUp_Radial_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 17: Tỉ lệ đơn hàng giao đúng giờ
                    case 17:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_Delivery_Radial_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 18: Tỉ lệ chứng từ giao đúng giờ
                    case 18:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_POD_Radial_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 19: Chi phí theo từng nhà thầu
                    case 19:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Vendor_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, TypeOfChart: v.TypeOfChart  },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 20: Tỉ lệ chi phí theo từng nhà thầu
                    case 20:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Vendor_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, TypeOfChart: v.TypeOfChart },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                        // Chart 21: Tỉ lệ doanh thu theo từng khách hàng
                    case 21:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Customer_ExcelExport,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID, TypeOfChart: v.TypeOfChart },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    $rootScope.DownloadFile(res);
                                    $rootScope.IsLoading = false;
                                }
                            }
                        });
                        break;
                }
            }
        });
    }

    // Reload lại chart được thay đổi setting
    $scope.ReloadChart = function (chartKey) {
        Common.Log("Reload chart");
        $rootScope.IsLoading = true;

        angular.forEach($scope.UserSetting.ListChart, function (v, i) {
            if (v.ChartKey == chartKey) {
                switch (v.TypeOfChart) {
                    case 1:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Summary_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    var average = { Date: "Trung bình", TotalLTL: 0, TotalFTL: 0, TotalFCL: 0, TotalLCL: 0 };
                                    angular.forEach(res.ListData, function (v, i) {
                                        v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        average.TotalLTL += v.TotalLTL;
                                        average.TotalFTL += v.TotalFTL;
                                        average.TotalFCL += v.TotalFCL;
                                        average.TotalLCL += v.TotalLCL;
                                    });
                                    if (res.ListData.length > 0) {
                                        average.TotalLTL = (average.TotalLTL / res.ListData.length).toFixed(1);
                                        average.TotalFTL = (average.TotalFTL / res.ListData.length).toFixed(1);
                                        average.TotalFCL = (average.TotalFCL / res.ListData.length).toFixed(1);
                                        average.TotalLCL = (average.TotalLCL / res.ListData.length).toFixed(1);
                                    }
                                    res.ListData.push(average);
                                    v.Data = res.ListData;
                                    v.Series.push({ name: 'LTL', field: "TotalLTL" });
                                    v.Series.push({ name: 'FTL', field: "TotalFTL" });
                                    v.Series.push({ name: 'FCL', field: "TotalFCL" });
                                    v.Series.push({ name: 'LCL', field: "TotalLCL" });

                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;

                    case 2:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Customer_Order_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    // LTL
                                    if (v.Filter.TransportModeID == 1) {
                                        v.Series.push({ name: 'Tấn', field: "Ton" });
                                        v.Series.push({ name: 'Khối', field: "CBM" });
                                        angular.forEach(res.ListLTL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Data = res.ListLTL;
                                        $scope.InitChart(v);
                                    }
                                    // FTL
                                    if (v.Filter.TransportModeID == 2) {
                                        v.Series.push({ name: 'Số lượng đơn', field: "Total" });
                                        angular.forEach(res.ListFTL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Data = res.ListFTL;
                                        $scope.InitChart(v);
                                    }
                                    // FCL
                                    if (v.Filter.TransportModeID == 3) {
                                        v.Series.push({ name: '20DC', field: "Total20DC" });
                                        v.Series.push({ name: '40DC', field: "Total40DC" });
                                        v.Series.push({ name: '40HC', field: "Total40HC" });
                                        angular.forEach(res.ListFCL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Data = res.ListFCL;
                                        $scope.InitChart(v);
                                    }
                                    // LCL
                                    if (v.Filter.TransportModeID == 4) {
                                        v.Series.push({ name: '20DC', field: "Total20DC" });
                                        v.Series.push({ name: '40DC', field: "Total40DC" });
                                        v.Series.push({ name: '40HC', field: "Total40HC" });
                                        angular.forEach(res.ListLCL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Data = res.ListLCL;
                                        $scope.InitChart(v);
                                    }
                                }
                            }
                        });
                        break;

                    case 3:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Customer_OPS_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID, statusid: v.Filter.StatusID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    // LTL
                                    var average = { Date: "Trung bình", Ton: 0, CBM: 0 };
                                    // LTL
                                    if (v.Filter.TransportModeID == 1) {
                                        v.Series.push({ name: 'Tấn', field: "Ton" });
                                        v.Series.push({ name: 'Khối', field: "CBM" });
                                        angular.forEach(res.ListLTL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                            average.Ton += v.Ton;
                                            average.CBM += v.CBM;
                                        });
                                        if (res.ListLTL.length > 0) {
                                            average.Ton = (average.Ton / res.ListLTL.length).toFixed(1);
                                            average.CBM = (average.CBM / res.ListLTL.length).toFixed(1);
                                        }
                                        res.ListLTL.push(average);
                                        v.Data = res.ListLTL;
                                        $scope.InitChart(v);
                                    }
                                    // FTL
                                    if (v.Filter.TransportModeID == 2) {
                                        v.Series.push({ name: 'Số lượng đơn', field: "Total" });
                                        angular.forEach(res.ListFTL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Data = res.ListFTL;
                                        $scope.InitChart(v);
                                    }
                                    // FCL
                                    if (v.Filter.TransportModeID == 3) {
                                        v.Series.push({ name: '20DC', field: "Total20DC" });
                                        v.Series.push({ name: '40DC', field: "Total40DC" });
                                        v.Series.push({ name: '40HC', field: "Total40HC" });
                                        angular.forEach(res.ListFCL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Data = res.ListFCL;
                                        $scope.InitChart(v);
                                    }
                                    // LCL
                                    if (v.Filter.TransportModeID == 4) {
                                        v.Series.push({ name: '20DC', field: "Total20DC" });
                                        v.Series.push({ name: '40DC', field: "Total40DC" });
                                        v.Series.push({ name: '40HC', field: "Total40HC" });
                                        angular.forEach(res.ListLCL, function (v, i) {
                                            v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        });
                                        v.Data = res.ListLCL;
                                        $scope.InitChart(v);
                                    }
                                }
                            }
                        });
                        break;

                    case 4:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Customer_Order_Rate_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, quantity: v.Filter.CustomerTopID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    v.Data = res.ListData;
                                    v.Series.push({
                                        type: "pie",
                                        field: "Percent",
                                        categoryField: "CustomerName",
                                    });
                                    v.Unit = "đơn";
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                    case 5:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Capacity_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    v.Series.push({ name: 'Tỉ lệ tấn', field: "ValueTon" });
                                    v.Series.push({ name: 'Tỉ lệ khối', field: "ValueCBM" });
                                    v.Data = res.ListData;
                                    v.CategoryAxis = {
                                        field: "GroupOfVehicleCode", type: "category",
                                        majorGridLines: { visible: false },
                                        min: 0
                                    };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                    case 6:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_KM_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    var average = { Date: "Trung bình", KMLaden: 0, KMEmpty: 0 };
                                    v.Series.push({ name: 'Có hàng', field: "KMLaden" });
                                    v.Series.push({ name: 'Không hàng', field: "KMEmpty" });
                                    angular.forEach(res.ListData, function (v, i) {
                                        v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        average.KMLaden += v.KMLaden;
                                        average.KMEmpty += v.KMEmpty;
                                    });
                                    if (res.ListData.length > 0) {
                                        average.KMLaden = (average.KMLaden / res.ListData.length).toFixed(1);
                                        average.KMEmpty = (average.KMEmpty / res.ListData.length).toFixed(1);
                                    }
                                    res.ListData.push(average);
                                    v.Data = res.ListData;
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                    case 7:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Operation_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    v.Series.push({ name: 'Tổng giờ', field: "TotalTime" });
                                    v.Series.push({ name: 'T.g chạy', field: "RunningTime" });
                                    v.Series.push({ name: 'T.g bốc xếp', field: "LoadingTime" });
                                    v.Series.push({ name: 'T.g chờ', field: "WaittingTime" });
                                    v.Series.push({ name: 'T.g thừa', field: "OtherTime" });
                                    v.Data = res.ListData;
                                    v.CategoryAxis = {
                                        field: "VehicleCode", type: "category",
                                        majorGridLines: { visible: false },
                                        min: 0, max: 3,
                                        labels: {
                                            rotation: "90"
                                        }
                                    };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 8: Chi phí trên mỗi tấn/khối/km của xe
                    case 8:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_CostRate_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    var average = { VehicleCode: "Trung bình", KMIndex: 0, TonIndex: 0, CBMIndex: 0 };
                                    v.Series.push({ name: 'C.phí mỗi KM', field: "KMIndex" });
                                    v.Series.push({ name: 'C.phí mỗi Tấn', field: "TonIndex" });
                                    v.Series.push({ name: 'C.phí mỗi Khối', field: "CBMIndex" });
                                    angular.forEach(res.ListData, function (v, i) {
                                        v.Date = Common.Date.FromJsonDDMM(v.Date);
                                        average.KMIndex += v.KMIndex;
                                        average.TonIndex += v.TonIndex;
                                        average.CBMIndex += v.CBMIndex;
                                    });
                                    if (res.ListData.length > 0) {
                                        average.KMIndex = (average.KMIndex / res.ListData.length).toFixed(1);
                                        average.TonIndex = (average.TonIndex / res.ListData.length).toFixed(1);
                                        average.CBMIndex = (average.CBMIndex / res.ListData.length).toFixed(1);
                                    }
                                    res.ListData.push(average);
                                    v.Data = res.ListData;
                                    v.CategoryAxis = {
                                        field: "VehicleCode", type: "category",
                                        majorGridLines: { visible: false },
                                        min: 0, max: 3,
                                        labels: {
                                            rotation: "90"
                                        }
                                    };
                                    v.Tooltip = { visible: true, template: "#= series.name #: #= Common.Number.ToMoney(value) # đ" };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 9: Chi phí biến đổi
                    case 9:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_CostChange_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    v.Data = res.ListData;
                                    v.Series.push({
                                        type: "pie",
                                        field: "Percent",
                                        categoryField: "Category",
                                    });
                                    v.Legend = { visible: true, position: "bottom" };
                                    v.Tooltip = { visible: true, template: "${ category }: ${ Common.Number.ToMoney(dataItem.Total) } đ" + ", ${ value }%" };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 10: Tỉ lệ đơn hàng bốc đúng giờ
                    case 10:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_PickUp_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    angular.forEach(res.ListData, function (v, i) {
                                        v.Date = Common.Date.FromJsonDDMM(v.Date);
                                    });
                                    v.Series.push({ name: 'Tổng đơn', field: "Total", type: "column", axis: "Total", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" } });
                                    v.Series.push({ name: 'Tỉ lệ đúng giờ', field: "Percent", type: "line", axis: "Percent", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #%" } });
                                    v.Data = res.ListData;
                                    v.ValueAxis = [
                                        {
                                            title: { text: "Tổng đơn", font: "12px sans-serif", },
                                            name: "Total",
                                        },
                                        {
                                            name: "Percent",
                                            title: { text: "Tỉ lệ đúng giờ", font: "12px sans-serif", },
                                            min: 0,
                                            max: 100,
                                        }];
                                    v.CategoryAxis = {
                                        field: "Date", type: "category",
                                        majorGridLines: { visible: false },
                                        baseUnit: 'days',
                                        labels: {
                                            rotation: "auto",
                                            format: "d/M"
                                        },
                                        min: 0,
                                        axisCrossingValue: [0, 1000]
                                    };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 11: Tỉ lệ đơn hàng giao đúng giờ
                    case 11:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_Delivery_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    angular.forEach(res.ListData, function (v, i) {
                                        v.Date = Common.Date.FromJsonDDMM(v.Date);
                                    });
                                    v.Series.push({ name: 'Tổng đơn', field: "Total", type: "column", axis: "Total", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" } });
                                    v.Series.push({ name: 'Tỉ lệ đúng giờ', field: "Percent", type: "line", axis: "Percent", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #%" } });
                                    v.Data = res.ListData;
                                    v.ValueAxis = [
                                        {
                                            title: { text: "Tổng đơn", font: "12px sans-serif", },
                                            name: "Total",
                                        },
                                        {
                                            name: "Percent",
                                            title: { text: "Tỉ lệ đúng giờ", font: "12px sans-serif", },
                                            min: 0,
                                            max: 100,
                                        }];
                                    v.CategoryAxis = {
                                        field: "Date", type: "category",
                                        majorGridLines: { visible: false },
                                        baseUnit: 'days',
                                        labels: {
                                            rotation: "auto",
                                            format: "d/M"
                                        },
                                        min: 0,
                                        axisCrossingValue: [0, 1000]
                                    };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 12: Tỉ lệ chứng từ giao đúng giờ
                    case 12:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_POD_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    angular.forEach(res.ListData, function (v, i) {
                                        v.Date = Common.Date.FromJsonDDMM(v.Date);
                                    });
                                    v.Series.push({ name: 'Tổng c.từ', field: "Total", type: "column", axis: "Total", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #" } });
                                    v.Series.push({ name: 'Tỉ lệ đúng giờ', field: "Percent", type: "line", axis: "Percent", tooltip: { visible: true, template: "#= series.name #: #= Common.Number.ToNumber1(value) #%" } });
                                    v.Data = res.ListData;
                                    v.ValueAxis = [
                                        {
                                            title: { text: "Tổng c.từ", font: "12px sans-serif", },
                                            name: "Total",
                                        },
                                        {
                                            name: "Percent",
                                            title: { text: "Tỉ lệ đúng giờ", font: "12px sans-serif", },
                                            min: 0,
                                            max: 100,
                                        }];
                                    v.CategoryAxis = {
                                        field: "Date", type: "category",
                                        majorGridLines: { visible: false },
                                        baseUnit: 'days',
                                        labels: {
                                            rotation: "auto",
                                            format: "d/M"
                                        },
                                        min: 0,
                                        axisCrossingValue: [0, 1000]
                                    };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 13: Hàng trả về
                    case 13:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Return_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    angular.forEach(res.ListData, function (v, i) {
                                        v.Date = Common.Date.FromJsonDDMM(v.Date);
                                    });
                                    v.Series.push({ name: 'Tấn', field: "Ton" });
                                    v.Series.push({ name: 'Khối', field: "CBM" });
                                    v.Series.push({ name: 'Số lượng', field: "Quantity" });
                                    v.Data = res.ListData;
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 14: Doanh thu, chi phí
                    case 14:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];

                                    v.Data = res.ListData;
                                    var max = 0;
                                    var min = 0;
                                    var majorUnit = 0;
                                    if (v.Filter.FinanceID == 1) {
                                        angular.forEach(res.ListData, function (o, i) {
                                            max = max + o.Credit;
                                        });
                                    } else {
                                        angular.forEach(res.ListData, function (o, i) {
                                            max = max + o.Debit;
                                        });
                                    }
                                    max = max + 1000000;
                                    majorUnit = max / 5;

                                    v.Data.push({ Category: "Tổng", Summary: "total" });

                                    if (v.Filter.FinanceID == 1) {
                                        v.Series.push({
                                            type: "horizontalWaterfall",
                                            field: "Credit",
                                            categoryField: "Category",
                                            summaryField: "Summary",
                                        });
                                    } else {
                                        v.Series.push({
                                            type: "horizontalWaterfall",
                                            field: "Debit",
                                            categoryField: "Category",
                                            summaryField: "Summary",
                                        });
                                    }

                                    v.Tooltip = { visible: true, template: "#= Common.Number.ToMoney(value) # đ" };
                                    v.ValueAxis = {
                                        labels: {
                                            template: "#: kendo.toString(value, 'n0') #",
                                            rotation: "90"
                                        },
                                        min: min,
                                        max: max,
                                        majorUnit: majorUnit,
                                    };
                                    v.Legend = { visible: false };
                                    v.SeriesDefaults = {};
                                    v.CategoryAxis = {};

                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 15: Doanh thu, chi phí theo từng khách hàng
                    case 15:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Customer_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];

                                    v.Series.push({ name: 'Doanh thu', field: "Credit" });
                                    v.Series.push({ name: 'Chi phí', field: "Debit" });
                                    v.Data = res.ListData;
                                    v.CategoryAxis = {
                                        field: "CustomerCode", type: "category",
                                        majorGridLines: { visible: false },
                                        min: 0,
                                        //labels: {
                                        //    rotation: "90"
                                        //}
                                    };
                                    v.Tooltip = { visible: true, template: "#= series.name #: #= Common.Number.ToMoney(value) # đ" };

                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 16: Tỉ lệ đơn hàng bốc đúng giờ
                    case 16:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_PickUp_Radial_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Value = res.Percent;
                                    if (v.IsRadial) {
                                        v.IsShowRadial = true;
                                        if (Common.HasValue(v.ListRadialSetting) && v.ListRadialSetting.length == 3) {

                                        } else {
                                            v.ListRadialSetting = _ORDDashboard_Index.Data.ListRadialSetting;
                                        }
                                        var color = v.ListRadialSetting[0].Color;
                                        angular.forEach(v.ListRadialSetting, function (o, i) {
                                            if (v.Value >= o.Value)
                                                color = o.Color;
                                        });
                                        v.Ranges = [{
                                            from: 0,
                                            to: v.Value,
                                            color: color
                                        }];
                                    }
                                    $scope.InitRadialGauge(v);
                                }
                            }
                        });
                        break;
                        // Chart 17: Tỉ lệ đơn hàng giao đúng giờ
                    case 17:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_Delivery_Radial_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Value = res.Percent;
                                    if (v.IsRadial) {
                                        v.IsShowRadial = true;
                                        if (Common.HasValue(v.ListRadialSetting) && v.ListRadialSetting.length == 3) {

                                        } else {
                                            v.ListRadialSetting = _ORDDashboard_Index.Data.ListRadialSetting;
                                        }
                                        var color = v.ListRadialSetting[0].Color;
                                        angular.forEach(v.ListRadialSetting, function (o, i) {
                                            if (v.Value >= o.Value)
                                                color = o.Color;
                                        });
                                        v.Ranges = [{
                                            from: 0,
                                            to: v.Value,
                                            color: color
                                        }];
                                    }
                                    $scope.InitRadialGauge(v);
                                }
                            }
                        });
                        break;
                        // Chart 18: Tỉ lệ chứng từ giao đúng giờ
                    case 18:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_OnTime_POD_Radial_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Value = res.Percent;
                                    if (v.IsRadial) {
                                        v.IsShowRadial = true;
                                        if (Common.HasValue(v.ListRadialSetting) && v.ListRadialSetting.length == 3) {

                                        } else {
                                            v.ListRadialSetting = _ORDDashboard_Index.Data.ListRadialSetting;
                                        }
                                        var color = v.ListRadialSetting[0].Color;
                                        angular.forEach(v.ListRadialSetting, function (o, i) {
                                            if (v.Value >= o.Value)
                                                color = o.Color;
                                        });
                                        v.Ranges = [{
                                            from: 0,
                                            to: v.Value,
                                            color: color
                                        }];
                                    }
                                    $scope.InitRadialGauge(v);
                                }
                            }
                        });
                        break;
                        // Chart 19: Chi phí theo từng nhà thầu
                    case 19:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Vendor_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];

                                    v.Series.push({ name: 'Chi phí', field: "Debit" });
                                    v.Data = res.ListData;
                                    v.CategoryAxis = {
                                        field: "CustomerCode", type: "category",
                                        majorGridLines: { visible: false },
                                        min: 0,
                                    };
                                    v.Tooltip = { visible: true, template: "#= series.name #: #= Common.Number.ToMoney(value) # đ" };

                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 20: Tỉ lệ chi phí theo từng nhà thầu
                    case 20:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Vendor_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    v.Data = res.ListData;
                                    v.Series.push({
                                        type: "pie",
                                        field: "DebitPercent",
                                        categoryField: "CustomerCode",
                                    });
                                    v.Tooltip = { visible: true, template: "${ category }: ${ Common.Number.ToMoney(dataItem.Debit) } đ" + ", ${ value }%" };
                                    v.Legend = { visible: false };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                        // Chart 21: Tỉ lệ doanh thu theo từng khách hàng
                    case 21:
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Owner_Profit_Customer_Data,
                            data: { dtfrom: v.Filter.DateFrom, dtto: v.Filter.DateTo, customerid: v.Filter.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Data = [];
                                    v.Series = [];
                                    angular.forEach(res.ListData, function (o, i) {
                                        if (o.CreditPercent > 0)
                                            v.Data.push(o);
                                    });
                                    v.Series.push({
                                        type: "pie",
                                        field: "CreditPercent",
                                        categoryField: "CustomerCode",
                                    });
                                    v.Tooltip = { visible: true, template: "${ category }: ${ Common.Number.ToMoney(dataItem.Credit) } đ" + ", ${ value }%" };
                                    v.Legend = { visible: false };
                                    $scope.InitChart(v);
                                }
                            }
                        });
                        break;
                }

                return true;
            }
        });
    };

    // Chỉnh sửa layout
    $scope.LayoutEdit_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
    }

    // Thực hiện thay đổi layout
    $scope.LayoutChange_Click = function ($event, win, layoutid) {
        $event.preventDefault();

        if ($scope.UserSetting.Layout != layoutid) {
            $timeout(function () {
                var oldLayout = $scope.UserSetting.Layout;
                $scope.UserSetting.Layout = layoutid;
                angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                    v.ChartID = i + 1;
                    v.ChartKey = $scope.UserSetting.Layout + "_" + v.ChartID;
                });
                win.close();

                $scope.SaveUserSetting(true);
            }, 10);
        } else
            win.close();
    }

    // Hiển thị popup thay đổi setting của chart
    $scope.ChartSetting_Click = function ($event, win) {
        $event.preventDefault();

        var chartKey = $($event.currentTarget).data("key");
        $scope.Chart.CurrentChart = chartKey;

        $timeout(function () {
            angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                if (v.ChartKey == chartKey) {
                    $scope.Chart.IsShowCustomer = v.IsShowCustomer;
                    $scope.Chart.IsShowTransportMode = v.IsShowTransportMode;
                    $scope.Chart.IsShowStatus = v.IsShowStatus;
                    $scope.Chart.IsShowChartType = v.IsShowChartType;
                    $scope.Chart.IsShowFinance = v.IsShowFinance;
                    $scope.Chart.IsShowRadial = v.IsShowRadial;
                    $scope.Chart.IsShowTypeOfDate = v.IsShowTypeOfDate;

                    $scope.Chart.ChartType = v.ChartType;
                    $scope.Chart.ChartTheme = v.ChartTheme;
                    $scope.Chart.BackGroundColor = v.BackGroundColor;

                    if (Common.HasValue(v.ListRadialSetting) && v.ListRadialSetting.length == 3) {
                        $scope.Chart.Range1 = v.ListRadialSetting[0].Value;
                        $scope.Chart.Range2 = v.ListRadialSetting[1].Value;
                        $scope.Chart.Range3 = v.ListRadialSetting[2].Value;
                        $scope.Chart.Color1 = v.ListRadialSetting[0].Color;
                        $scope.Chart.Color2 = v.ListRadialSetting[1].Color;
                        $scope.Chart.Color3 = v.ListRadialSetting[2].Color;
                    }

                    $scope.Chart.DateFrom = v.Filter.DateFrom;
                    $scope.Chart.DateTo = v.Filter.DateTo;
                    $scope.Chart.CustomerID = v.Filter.CustomerID;
                    $scope.Chart.TransportModeID = v.Filter.TransportModeID;
                    $scope.Chart.StatusID = v.Filter.StatusID;
                    $scope.Chart.CustomerTopID = v.Filter.CustomerTopID;
                    $scope.Chart.FinanceID = v.Filter.FinanceID;
                    win.center().open();
                }
            });
        }, 1);

        $scope.Chart.IsChartAdd = false;
    }

    // Apply setting cho chart hiện tại
    $scope.ChartApply_Click = function ($event, win) {
        $event.preventDefault();
        if (Common.HasValue($scope.Chart.CurrentChart)) {
            $timeout(function () {
                angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                    if (v.ChartKey == $scope.Chart.CurrentChart) {
                        if (v.IsRadial) {
                            if ($scope.Chart.Range1 < 0 || $scope.Chart.Range1 > 100) {
                                $rootScope.Message({ Msg: 'Mốc 1 phải từ 0-100', NotifyType: Common.Message.NotifyType.ERROR });
                                return false;
                            }
                            if ($scope.Chart.Range2 < 0 || $scope.Chart.Range2 > 100) {
                                $rootScope.Message({ Msg: 'Mốc 1 phải từ 0-100', NotifyType: Common.Message.NotifyType.ERROR });
                                return false;
                            }
                            if ($scope.Chart.Range3 < 0 || $scope.Chart.Range3 > 100) {
                                $rootScope.Message({ Msg: 'Mốc 1 phải từ 0-100', NotifyType: Common.Message.NotifyType.ERROR });
                                return false;
                            }

                            v.ListRadialSetting = [];
                            v.ListRadialSetting.push({ Value: $scope.Chart.Range1, Color: $scope.Chart.Color1 });
                            v.ListRadialSetting.push({ Value: $scope.Chart.Range2, Color: $scope.Chart.Color2 });
                            v.ListRadialSetting.push({ Value: $scope.Chart.Range3, Color: $scope.Chart.Color3 });
                        }

                        v.Filter = {};

                        if ($scope.Chart.CustomerID > 0)
                            v.Filter.CustomerID = $scope.Chart.CustomerID;
                        else
                            v.Filter.CustomerID = null;

                        if ($scope.Chart.TransportModeID > 0)
                            v.Filter.TransportModeID = $scope.Chart.TransportModeID;
                        else
                            v.Filter.TransportModeID = null;

                        if ($scope.Chart.StatusID > -1)
                            v.Filter.StatusID = $scope.Chart.StatusID;
                        else
                            v.Filter.StatusID = null;

                        v.Filter.DateFrom = $scope.Chart.DateFrom;
                        v.Filter.DateTo = $scope.Chart.DateTo;
                        v.Filter.CustomerTopID = $scope.Chart.CustomerTopID;
                        v.Filter.FinanceID = $scope.Chart.FinanceID;

                        if ($scope.Chart.ChartType != null) {
                            v.ChartType = $scope.Chart.ChartType;
                        }
                        else
                            v.ChartType = "column";

                        if ($scope.Chart.ChartTheme != null) {
                            v.ChartTheme = $scope.Chart.ChartTheme;
                        }
                        else
                            v.ChartTheme = "Bootstrap";

                        if ($scope.Chart.BackGroundColor != null) {
                            v.BackGroundColor = $scope.Chart.BackGroundColor;
                            angular.forEach(_ORDDashboard_Index.Data.BackGroundColor, function (color, i) {
                                if (color == $scope.Chart.BackGroundColor)
                                    v.Color = _ORDDashboard_Index.Data.Color[i];
                            });
                        }
                        else {
                            v.BackGroundColor = _ORDDashboard_Index.Data.DefBackGroundColor;
                            v.Color = _ORDDashboard_Index.Data.Color;
                        }

                        if (v.BackGroundColor == _ORDDashboard_Index.Data.DefBackGroundColor)
                            v.BorderColor = "#dadada";
                        else
                            v.BorderColor = v.BackGroundColor;

                        $scope.SaveUserSetting(false);

                        return true;
                    }
                });

                // Reaload Current Chart
                $scope.ReloadChart($scope.Chart.CurrentChart);
                win.close();
            }, 10);
        } else
            win.close();
    }

    // Show popup filter dữ liệu chart
    $scope.ChartFilter_Click = function ($event, win) {
        $event.preventDefault();

        var chartKey = $($event.currentTarget).data("key");
        $scope.Chart.CurrentChart = chartKey;

        $timeout(function () {
            angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                if (v.ChartKey == chartKey) {
                    $scope.Chart.IsShowCustomer = v.IsShowCustomer;
                    $scope.Chart.IsShowTransportMode = v.IsShowTransportMode;
                    $scope.Chart.IsShowStatus = v.IsShowStatus;
                    $scope.Chart.IsShowChartType = v.IsShowChartType;
                    $scope.Chart.IsShowFinance = v.IsShowFinance;
                    $scope.Chart.IsShowRadial = v.IsShowRadial;
                    $scope.Chart.IsShowTypeOfDate = v.IsShowTypeOfDate;

                    $scope.Chart.ChartType = v.ChartType;
                    $scope.Chart.ChartTheme = v.ChartTheme;
                    $scope.Chart.BackGroundColor = v.BackGroundColor;

                    if (Common.HasValue(v.ListRadialSetting) && v.ListRadialSetting.length == 3) {
                        $scope.Chart.Range1 = v.ListRadialSetting[0].Value;
                        $scope.Chart.Range2 = v.ListRadialSetting[1].Value;
                        $scope.Chart.Range3 = v.ListRadialSetting[2].Value;
                        $scope.Chart.Color1 = v.ListRadialSetting[0].Color;
                        $scope.Chart.Color2 = v.ListRadialSetting[1].Color;
                        $scope.Chart.Color3 = v.ListRadialSetting[2].Color;
                    }

                    $scope.Chart.DateFrom = v.Filter.DateFrom;
                    $scope.Chart.DateTo = v.Filter.DateTo;
                    $scope.Chart.CustomerID = v.Filter.CustomerID;
                    $scope.Chart.TransportModeID = v.Filter.TransportModeID;
                    $scope.Chart.StatusID = v.Filter.StatusID;
                    $scope.Chart.CustomerTopID = v.Filter.CustomerTopID;
                    $scope.Chart.FinanceID = v.Filter.FinanceID;
                    win.center().open();
                }
            });
        }, 1);

        $scope.Chart.IsChartAdd = false;
    }

    // Thêm chart
    $scope.ChartAdd_Click = function ($event, win) {
        $event.preventDefault();

        angular.forEach(_ORDDashboard_Index.Data.ListChart, function (chart, i) {
            chart.IsShow = true;
        });

        angular.forEach(_ORDDashboard_Index.Data.ListChart, function (chart, i) {
            angular.forEach($scope.UserSetting.ListChart, function (setting, i) {
                if (chart.ID == setting.TypeOfChart)
                    chart.IsShow = false;
            });
        });

        $scope.Chart.ListChart = [];
        $scope.Chart.ListChart = _ORDDashboard_Index.Data.ListChart;

        $scope.Chart.IsChartAdd = true;

        win.center().open();
    }

    // Đổi chart khác
    $scope.Change_Click = function ($event, win) {
        $event.preventDefault();

        if (Common.HasValue($scope.Chart.CurrentChart)) {
            angular.forEach(_ORDDashboard_Index.Data.ListChart, function (chart, i) {
                chart.IsShow = true;
            });

            angular.forEach(_ORDDashboard_Index.Data.ListChart, function (chart, i) {
                angular.forEach($scope.UserSetting.ListChart, function (setting, i) {
                    if (chart.ID == setting.TypeOfChart)
                        chart.IsShow = false;
                });
            });

            $scope.Chart.ListChart = [];
            $scope.Chart.ListChart = _ORDDashboard_Index.Data.ListChart;

            win.center().open();
        }
    }

    // Add chart mới
    $scope.ChartChange_Click = function ($event, win, win_setting) {
        $event.preventDefault();

        if (Common.HasValue($scope.Chart.CurrentChart) || $scope.Chart.IsChartAdd) {
            var ChartID = -1;
            if ($scope.Chart.CurrentChart != "") ChartID = $scope.Chart.CurrentChart.split("_");
            if ($scope.Chart.IsChartAdd) ChartID = -1;

            var typeofchartID = $event.currentTarget.getAttribute("typeofchart-id");
            angular.forEach(_ORDDashboard_Index.Data.ListChart, function (v, i) {
                if (v.ID == typeofchartID)
                {
                    chart = v;
                    return true;
                }
            });
            var flag = false;
            if (ChartID > -1) {
                angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                    if (v.ChartID == ChartID) {
                        flag = true;
                        v.ChartName = chart.ChartName;
                        v.TypeOfChart = typeofchartID;
                    }
                });
            } else
                ChartID = $scope.UserSetting.ListChart.length + 1;

            // Chart mới
            if (!flag) {
                var newChart = { ChartID: ChartID, ChartName: chart.ChartName, ChartType: 'column', ChartTheme: 'Bootstrap', TypeOfChart: typeofchartID, ChartKey: $scope.UserSetting.Layout + "_" + ChartID, BackGroundColor: _ORDDashboard_Index.Data.DefBackGroundColor, Color: _ORDDashboard_Index.Data.DefColor };
                $scope.UserSetting.ListChart.push(newChart);
            }

            angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                v.ChartID = i + 1;
                v.ChartKey = $scope.UserSetting.Layout + "_" + v.ChartID;
            });

            win.close();
            win_setting.close();
            $scope.SaveUserSetting(true);
        } else {
            win.close();
            win_setting.close();
        }

        $scope.Chart.IsChartAdd = false;
    }

    // Xóa chart hiện tại ra khỏi widget
    $scope.ChartRemove_Click = function ($event, win) {
        $event.preventDefault();
        var chartKey = $($event.currentTarget).data("key");

        if (Common.HasValue(chartKey)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn có chắc muốn xóa biểu đồ này?',
                Ok: function () {
                    var arrKey = chartKey.split("_");
                    var idx = -1;
                    if ($scope.UserSetting.Layout == arrKey[0]) {
                        angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                            if (v.ChartID == arrKey[1]) {
                                idx = i;
                                return true;
                            }
                        });

                        if (idx >= 0) {
                            $scope.UserSetting.ListChart.splice(idx, 1);
                        }

                        angular.forEach($scope.UserSetting.ListChart, function (v, i) {
                            v.ChartID = i + 1;
                            v.ChartKey = $scope.UserSetting.Layout + "_" + v.ChartID;
                        });

                        $scope.SaveUserSetting(true);
                    }
                }
            });
        }
    }

    // Đóng popup
    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    // Hiển thị popup thay đổi setting của chart
    $scope.MenuConfig_Click = function ($event) {
        $event.preventDefault();
    }

    // Zoom 1 chart
    $scope.ChartZoom_Click = function ($event) {
        $event.preventDefault();
        var chartKey = $($event.currentTarget).data("key");

        angular.forEach($scope.UserSetting.ListChart, function (v, i) {
            if (v.ChartKey == chartKey) {
                if (v.IsRadial)
                    $scope.InitRadialGauge(v, true);
                else
                    $scope.InitChart(v, true);

                return true;
            }
        });

        $scope.chartZoom_win.setOptions({ draggable: false });
        $scope.chartZoom_win.maximize();
        $scope.chartZoom_win.center().open();
    }

    // Minimize 1 chart
    $scope.ChartMinimize_Click = function ($event) {
        $event.preventDefault();
        var chartKey = $($event.currentTarget).data("key");

        $scope.chartZoom_win.close();
    }

    // Load widget
    $scope.LoadWidget = function () {
        Common.Log("Load widget");
        $rootScope.IsLoading = true;

        if (Common.HasValue($scope.UserSetting.ListWidget)) {
            angular.forEach($scope.UserSetting.ListWidget, function (v, i) {
                v.Total = v.Ton = v.CBM = v.Total20DC = v.Total40DC = v.Total40HC = 0;
                angular.forEach(_ORDDashboard_Index.Data.ListWidget, function (widget, i) {
                    if (v.TypeOfChart == widget.ID) {
                        widget.IsShow = false;
                        v.IsShow = true;
                        v.ChartName = widget.ChartName;
                        v.Unit = widget.Unit;
                        v.Color = widget.Color;
                        v.Icon = widget.Icon;
                        v.IsOps = widget.IsOps;
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDDashboard_Index.URL.Widget_Data,
                            data: { dtfrom: $scope.Widget.DateFrom, dtto: $scope.Widget.DateTo, typeofchart: v.TypeOfChart, customerid: $scope.Widget.CustomerID },
                            success: function (res) {
                                if (Common.HasValue(res)) {
                                    v.Total = res.Total;
                                    v.Ton = parseInt(res.Ton);
                                    v.CBM = parseInt(res.CBM);
                                    v.Total20DC = res.Total20DC;
                                    v.Total40DC = res.Total40DC;
                                    v.Total40HC = res.Total40HC;
                                }
                            }
                        });
                        return false;
                    }
                });
            });

            $(".dashboard-widget-mid-scroll").css("width", $scope.UserSetting.ListWidget.length * 265);
        }

        $rootScope.IsLoading = false;
        $scope.AutoSlider();
    };

    // Filter cho Tổng quan
    $scope.WidgetFilter_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
    }

    // Áp dụng filter Tổng quan
    $scope.WidgetFilterApply_Click = function ($event, win) {
        $event.preventDefault();

        $scope.LoadWidget();
        win.close();
    }

    // Áp dụng filter Tổng quan cho các chart bên dưới
    $scope.WidgetFilterApplyAll_Click = function ($event, win) {
        $event.preventDefault();

        $scope.LoadWidget();

        angular.forEach($scope.UserSetting.ListChart, function (v, i) {
            v.Filter.DateFrom = $scope.Widget.DateFrom;
            v.Filter.DateTo = $scope.Widget.DateTo;
            v.Filter.CustomerID = $scope.Widget.CustomerID;
            $scope.ReloadChart(v.ChartKey);
        });
        $rootScope.IsLoading = false;
        win.close();
    }

    // Thêm widget tổng quan
    $scope.WidgetAdd_Click = function ($event, win) {
        $event.preventDefault();

        angular.forEach(_ORDDashboard_Index.Data.ListWidget, function (chart, i) {
            chart.IsShow = true;
        });

        angular.forEach(_ORDDashboard_Index.Data.ListWidget, function (chart, i) {
            angular.forEach($scope.UserSetting.ListWidget, function (setting, i) {
                if (chart.ID == setting.TypeOfChart)
                    chart.IsShow = false;
            });
        });

        $scope.Widget.ListChart = [];
        $scope.Widget.ListChart = _ORDDashboard_Index.Data.ListWidget;

        win.center().open();
    }

    // Thêm widget mới
    $scope.WidgetChange_Click = function ($event, win) {
        $event.preventDefault();

        var typeofchartID = $event.currentTarget.getAttribute("typeofchart-id");
        var chart = _ORDDashboard_Index.Data.ListWidget[typeofchartID - 1];

        var newChart = { ChartID: $scope.UserSetting.ListWidget.length + 1, ChartName: chart.ChartName, TypeOfChart: typeofchartID };
        $scope.UserSetting.ListWidget.push(newChart);

        angular.forEach($scope.UserSetting.ListWidget, function (v, i) {
            v.ChartID = i + 1;
            v.ChartKey = "Widget" + "_" + v.ChartID;
        });

        win.close();
        $scope.SaveUserSetting(false);

        $scope.LoadWidget();
    }

    // Xóa widget tổng quan
    $scope.WidgetRemove_Click = function ($event) {
        $event.preventDefault();
        var chartKey = $($event.currentTarget).data("key");

        if (Common.HasValue(chartKey)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn có chắc muốn xóa widget này?',
                Ok: function () {
                    var arrKey = chartKey.split("_");
                    var idx = -1;
                    angular.forEach($scope.UserSetting.ListWidget, function (v, i) {
                        if (v.ChartID == arrKey[1]) {
                            idx = i;
                            return true;
                        }
                    });

                    if (idx >= 0) {
                        $scope.UserSetting.ListWidget.splice(idx, 1);
                    }

                    angular.forEach($scope.UserSetting.ListWidget, function (v, i) {
                        v.ChartID = i + 1;
                        v.ChartKey = "Widget" + "_" + v.ChartID;
                    });

                    $scope.SaveUserSetting(false);
                    $scope.LoadWidget();
                }
            });
        }
    }

    // Xử lý scroll widget
    $scope.WidgetScroll_Click = function ($event, isRight) {
        $event.preventDefault();
        var view = $("div.dashboard-widget-mid-scroll");
        var width = 265;
        var base = 1060;
        if ($scope.UserSetting.ListWidget.length > 4) {
            var totalRight = ($scope.UserSetting.ListWidget.length - 4) * width;
            var marginLeft = parseInt(view.css("margin-left"));
            if (isRight) {
                if (marginLeft == 0 || (marginLeft < 0 && -marginLeft < totalRight))
                    view.animate({ marginLeft: '-=' + width + "px" }, 400);
            } else {
                if (marginLeft != 0)
                    view.animate({ marginLeft: '+=' + width + "px" }, 400);
            }
        } else {
            if (marginLeft < 0)
                view.animate({ marginLeft: '+=' + width + "px" }, 400);
        }
    }

    $timeout(function () {
        $scope.OnView_Resize(function () {
            $(document).find('.cus-chart').each(function (i, o) {
                try {
                    $(o).data('kendoChart').resize();
                } catch (e) { }
            });
        });
    }, 100);

    $scope.DefaultDisplay_Click = function ($event) {
        $event.preventDefault();
        var current = $state.current;


    };

    $scope.Map_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.ORDDashboard.Map");
    };

    $scope.MapCO_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.ORDDashboard.MapCO");
    };
}]);