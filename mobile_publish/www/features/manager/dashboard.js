/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('manage_dashboardController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, charting, openMap) {
    console.log('manage_dashboardController');
    
    $scope.selectedTab = 1;
    $scope.Filte=
    {
        DateFrom : new Date(),
        DateTo : new Date().addDays(1),
    };
    $scope.CbbModel=1;
    var options =function(tooltip){ return {
        grid: {
                drawBorder: false,
                shadow: false,
                background: 'rgba(59, 59, 62,1)'
        },
        highlighter: {
            show: true,
            tooltipLocation :'n',
            tooltipAxes: 'y',
            formatString: '<div style="border-radius:10px;padding:5px;background-color:#fff;color:black">%d ' + tooltip + '</div>'
        },
        seriesDefaults: {
            shadowAlpha: 0.1,
            shadowDepth: 2,
            fillToZero: true
        },
        series: [
            {
                color: 'rgba(84, 90, 203,1)',
                negativeColor: 'rgba(84, 90, 203,1)',
                showMarker: true,
                showLine: true,
                fill: true,
                fillAndStroke: true,
                markerOptions: {
                    style: 'filledCircle',
                    color: '#6F6F78',
                    size: 10
                },
                rendererOptions: {
                    smooth: true
                }
            },
        ],
        axes: {
            show: false,
            xaxis: {
                showLabel: false,
                show: false,
                pad: 1.0,
                tickOptions: {
                    tick: [[0, 'MON'], [1, 'TUE'], [2, 'WED'], [3, 'THU'], [4, 'FRI'], [5, 'SAT'], [6, 'SUN']],
                    show: false,
                    showGridline: false
                },
                rendererOptions: {
                    drawBaseline: false
                }

            },
            yaxis: {
                showLabel: false,
                pad: 1.0,
                show: false,
                tickOptions: {
                    show: false,
                    showGridline: false
                },
                rendererOptions: {
                    drawBaseline: false
                }
            }
        }
    };}
    var dNow=new Date();
    $scope.Filter = {
        DateFrom: dNow.addDays(-7),
        DateTo: dNow,
    }
    Common.Services.Call($http, {
        url: Common.Services.url.MOBI,
        method: "MobiManage_OrderList",
        data: {
            dtfrom: $scope.Filter.DateFrom,
            dtto: $scope.Filter.DateTo,
        },
        success: function (res) {
            $scope.OrderData = [[0, 0], [1, 0], [2, 0], [3, 0], [4, 0], [5, 0], [6, 0]];
            angular.forEach($scope.OrderData, function (o, i) {
                $scope.OrderData[i][1] = res[i];
            })
            $.jqplot('orderChart', [$scope.OrderData], options('đơn hàng'));
        }
    })
    $scope.ChartView = 1;
    //comobobox
    $scope.PickDate = function () {
        var data = [
            { text: '1 ngày', value: 1 },
            { text: '2 ngày', value: 2 },
            {text:'1 tuần',value:3}
        ]
        $scope.CreateDataCBB(data, 'text', $scope.CbbModel)
        $scope.ComboboxName = 'date';
    }

    $scope.PickChart = function () {
        var data = [
            { text: '1.Tổng quan đơn hàng', value: 1 },
            { text: '2.Tình trạng đơn hàng', value: 2 },
            { text: '3.Đồ thị tấn khối', value: 3 },
            { text: '4.Tình trạng xe', value: 4 },
            { text: '5.Số lượng chuyến', value: 5 },
        ]
        $scope.CreateDataCBB(data, 'text', $scope.CbbModel)
        $scope.ComboboxName = 'chart';
    }

    $scope.CreateDataCBB = function (data, textField, model) {
        $scope.ShowSelectPane = true;
        $scope.DataCombobox = [];
        $scope.ComoboboxModel = model;

        angular.forEach(data, function (o, i) {
            $scope.DataCombobox.push({ text: o[textField] ,value:o.value});
        })
    }

    $scope.ComboboxSelect = function (item) {
        if ($scope.ComboboxName == 'date') {
            switch (item.value) {
                case 1:
                    $scope.Filter.DateFrom = new Date();
                    $scope.Filter.DateTo = new Date().addDays(1);
                    break;
                case 2:
                    $scope.Filter.DateFrom = new Date().addDays(-1);
                    $scope.Filter.DateTo = new Date().addDays(1);
                    break;
                case 3:
                    $scope.Filter.DateFrom = new Date().addDays(-6);
                    $scope.Filter.DateTo = new Date().addDays(1);
                    break;
            }
        }
        if ($scope.ComboboxName == 'chart') {
            $scope.ChartView = item.value;
            switch (item.value) {
                case 1:

                    break;
                case 2:
                    if (Common.HasValue($scope.orderOverviewChart))
                        $scope.orderOverviewChart.destroy();
                    $ionicLoading.show();
                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: "MobiManage_OrderSummary",
                        data: {
                            dtfrom: $scope.Filter.DateFrom,
                            dtto: $scope.Filter.DateTo,
                        },
                        success: function (res) {
                            $ionicLoading.hide();
                            var data = [];
                            angular.forEach(res, function (o, i) {
                                data.push([o.StatusOfOrderName,o.Sum])
                            })
                            $scope.orderOverviewChart = $.jqplot('orderOverviewChart', [data], {
                                seriesDefaults: {
                                    // Make this a pie chart.
                                    renderer: jQuery.jqplot.PieRenderer,
                                    rendererOptions: {
                                        // Put data labels on the pie slices.
                                        // By default, labels show the percentage of the slice.
                                        sliceMargin: 1,
                                        shadowAlpha: 0,
                                        showDataLabels: true
                                    },
                                    color: ['#515974']
                                },
                                animate: true,
                                animateReplot: true,
                                grid: { shadow: false, drawBorder: false, shadow: false },
                                seriesColors: ['#1E40B6', '#481EB6', '#941EB6', '#B61E8C', '#B61E40', '#B6481E'],
                                legend: { show: true, location: 'e', border: '1px' }
                            });
                        }
                    })


                    break;
                case 3:

                    break;
                case 4:
                    if (Common.HasValue($scope.XeChart))
                        $scope.XeChart.destroy();
                    $ionicLoading.show();
                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: "MobiManage_SumOfVehicle",
                        data: {
                            dtfrom: $scope.Filter.DateFrom,
                            dtto: $scope.Filter.DateTo,
                        },
                        success: function (res) {
                            $ionicLoading.hide();
                            var data = [['Rảnh',res[0]],['Đang chạy',res[1]]];
                            
                            $scope.XeChart = $.jqplot('XeChart', [data], {
                                seriesDefaults: {
                                    // Make this a pie chart.
                                    renderer: jQuery.jqplot.PieRenderer,
                                    rendererOptions: {
                                        // Put data labels on the pie slices.
                                        // By default, labels show the percentage of the slice.
                                        sliceMargin: 1,
                                        shadowAlpha: 0,
                                        showDataLabels: true
                                    },
                                    color: ['#515974']
                                },
                                animate: true,
                                animateReplot: true,
                                grid: { shadow: false, drawBorder: false, shadow: false },
                                seriesColors: ['#4caf50', '#f5455a'],
                                legend: { show: true, location: 'e', border: '1px' }
                            });
                        }
                    })
                    break;
                case 5:
                    $ionicLoading.show();
                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: "MobiManage_NumOfTOMaster",
                        data: {
                            dtfrom: $scope.Filter.DateFrom,
                            dtto: $scope.Filter.DateTo,
                        },
                        success: function (res) {
                            $ionicLoading.hide();
                            $scope.MasterData = [[0, 0], [1, 0], [2, 0], [3, 0], [4, 0], [5, 0], [6, 0]];
                            angular.forEach($scope.MasterData, function (o, i) {
                                $scope.MasterData[i][1] = res[i];
                            })
                            $.jqplot('MasterChart', [$scope.MasterData], options('chuyến'));
                        }
                    })
                    break;
            }
        }
        $scope.ShowSelectPane = false;
    }

    $scope.CloseSelectPane = function () {
        $scope.ShowSelectPane = false;
    }

    //

    $scope.TabClick = function (idx) {
        $scope.selectedTab = 2;
        $state.go('map', { p0: 2, p1: 0 })
    }

    //kpi
    $scope.salaryData = [[['s', 10]]];
    $scope.salaryChart = $.jqplot('salaryChart', $scope.salaryData, charting.donutOptions);
    //noti
    Common.Services.Call($http, {
        url: Common.Services.url.MOBI,
        method: "MobiManage_GetAllNotification",
        data: {},
        success: function (res) {
            $scope.NotifyData = [];
            angular.forEach(res, function (o, i) {
                o.Img = '';
                switch (o.ActionCode) {
                    case 'OPSDI_Tendered':
                        o.Img = "img/ico_xe_going.png";
                        break;
                    case 'OPSDI_Complete':
                        o.Img = "img/ico_xe_done.png";
                        break;
                    case 'OPSDI_SendTender':
                        o.Img = "img/pcon.png";
                        break;
                    default:
                        o.Img = "img/isupport.png";
                        break;
                };
                $scope.NotifyData.push(o);
            })

        }
    })


    $scope.LogOut = function () {
        $rootScope.PopupConfirm({
            title: 'Bạn có muốn quay trở lại giao diện đăng nhập?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                location.href = 'index.html';
            }
        });
    }

});