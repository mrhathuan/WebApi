/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('manage_chartsController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, charting, openMap) {
    console.log('manage_costController');

    //Chart.pluginService.register({
    //    afterDraw: function (chart, easing) {
    //        if (chart.config.options.showPercentage || chart.config.options.showLabel) {
    //            var self = chart.config;
    //            var ctx = chart.chart.ctx;

    //            ctx.font = '12px Arial';
    //            ctx.textAlign = "center";
    //            ctx.fillStyle = "#000";
			
    //            self.data.datasets.forEach(function (dataset, datasetIndex) {				
    //                var total = 0, 
    //                    labelxy = [],
    //                    offset = Math.PI / 2, 
    //                    radius,
    //                    centerx,
    //                    centery, 
    //                    lastend = 0; 

    //                for (var val of dataset.data) { total += val; } 
				
    //                var i = 0;
    //            var meta = dataset._meta[i];
    //            while(!meta) {
    //                i++;
    //                meta = dataset._meta[i];
    //            }
				
    //            var element;
    //            for(index = 0; index < meta.data.length; index++) {
					
    //                element = meta.data[index];
    //                radius = 0.9 * element._view.outerRadius - element._view.innerRadius;
    //                centerx = element._model.x;
    //                centery = element._model.y;
    //                var thispart = dataset.data[index],
	//					arcsector = Math.PI * (2 * thispart / total);
    //                if (element.hasValue() && dataset.data[index] > 0) {
    //                    labelxy.push(lastend + arcsector / 2 + Math.PI + offset);
    //                }
    //                else {
    //                    labelxy.push(-1);
    //                }
    //                lastend += arcsector;
    //            }


    //            var lradius = radius * 3 / 4;
    //            for (var idx in labelxy) {
    //                if (labelxy[idx] === -1) continue;
    //                var langle = labelxy[idx],
	//				dx = centerx + lradius * Math.cos(langle),
	//				dy = centery + lradius * Math.sin(langle),
	//				val = Math.round(dataset.data[idx] / total * 100);
    //                if (chart.config.options.showPercentage)
    //                    ctx.fillText(val + '%', dx, dy);
    //                else 
    //                    ctx.fillText(chart.config.data.labels[idx], dx, dy);
    //            }
    //            ctx.restore();
    //        });
    //    }
    //}
    //});
   

    $scope.BackTo = function () { 
       $state.go('manage.dashboard');
    }
    var dNow = new Date();
    $scope.Filter = {
        DateFrom: dNow.addDays(-7),
        DateTo: dNow,
    }
    
    //chart line
    $scope.options =  {
        responsive: true,
        scaleShowGridLines : false,
        scaleGridLineColor : "rgba(0,0,0,.05)",
        scaleGridLineWidth : 1,
        bezierCurve : true,
        bezierCurveTension : 0.4,
        pointDot : true,
        pointDotRadius : 4,
        pointDotStrokeWidth : 1,
        pointHitDetectionRadius : 20,   
        datasetStroke : true,   
        datasetStrokeWidth : 2,    
        datasetFill: true,
        legend: {
            display: false
        },
        onAnimationProgress: function(){},
        tooltipEvents: [],
        showTooltips: true,
        tooltipCaretSize: 0,
        onAnimationComplete: function () {
            this.showTooltip(this.segments, true);
        },
        animation: {
            animateScale: true
        }
       
    };

//tổng quan đơn hàng
    Common.Services.Call($http, {
        url: Common.Services.url.MOBI,
        method: "MobiManage_OrderList",
        data: {
            dtfrom: $scope.Filter.DateFrom,
            dtto: $scope.Filter.DateTo,
        },
        success: function (res) {
            
            $scope.data = {
                OrderList: {
                    labels: ['MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT', 'SUN'],
                    datasets: [
                      {
                          label: 'Số đơn hàng',
                          fill: true,
                          lineTension: 0.1,
                          backgroundColor: "rgba(84, 90, 203, 1)",
                          borderColor: "rgba(75,192,192,1)",
                          borderCapStyle: 'butt',
                          borderDash: [],
                          borderDashOffset: 7,
                          pointBorderColor: "rgba(59, 59, 62, 1)",
                          pointBackgroundColor: "rgba(111, 111, 120, 1)",
                          pointBorderWidth: 5,
                          pointHoverRadius: 5,
                          pointHoverBackgroundColor: "rgba(255, 255, 255, 1)",
                          pointHoverBorderColor: "rgba(115, 201, 95, 1)",
                          pointHoverBorderWidth: 7,
                          pointRadius: 2,
                          pointHitRadius: 10,
                          spanGaps: false,
                          data: res
                      }
                    ]
                }
            };
        }
    })
        


    //Tình trạng đơn hàng
    Common.Services.Call($http, {
        url: Common.Services.url.MOBI,
        method: "MobiManage_OrderSummary",
        data: {
            dtfrom: $scope.Filter.DateFrom,
            dtto: $scope.Filter.DateTo,
        },
        success: function (res) {           
            var labels = [];
            var data = [];
            angular.forEach(res, function (o, i) {
                labels.push(o.StatusOfOrderName);
                data.push(o.Sum);
            })          
            var ctx = document.getElementById("pieOrderSummary").getContext("2d");
            var myChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [

                        {
                            label: '%',
                            data: data,
                            backgroundColor: [
                                "#FF6384",
                                "#36A2EB",
                                "#FFCE56"
                            ],
                            hoverBackgroundColor: [
                                "#FF6384",
                                "#36A2EB",
                                "#FFCE56"
                            ]
                        }
                    ]
                },
                options: {
                    tooltips: {
                        callbacks: {
                            title: function (tooltipItem, data) { return data.labels[tooltipItem[0].index]; },
                            label: function (tooltipItem, data) {
                                var allData = data.datasets[tooltipItem.datasetIndex].data;
                                var tooltipLabel = data.labels[tooltipItem.index];
                                var tooltipData = allData[tooltipItem.index];
                                var total = 0;
                                for (var i in allData) {
                                    total += allData[i];
                                }
                                var tooltipPercentage = Math.round((tooltipData / total) * 100);
                                return tooltipLabel + ': ' + tooltipData + ' (' + tooltipPercentage + '%)';
                            }
                        },
                        enabled: true,
                    },
                    showPercentage: true
                }
            })
        }
    })


//tình trạng xe
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
            var ctx = document.getElementById("pieSumOfVehicle").getContext("2d");
            var myChart = new Chart(ctx, {

                type: 'pie',
                data: {
                    labels: ["Rảnh",
                        "Đang chạy"
                    ],
                    datasets: [

                        {
                            label: '%',
                            data: res,
                            backgroundColor: [
                                "#4caf50",
                                "#f5455a"
                            ],
                            hoverBackgroundColor: [
                                "#4caf50",
                                "#f5455a"
                            ]
                        }
                    ]
                },
                options: {
                    tooltips: {
                        callbacks: {
                            title: function (tooltipItem, data) { return data.labels[tooltipItem[0].index]; },
                            label: function (tooltipItem, data) {
                                var allData = data.datasets[tooltipItem.datasetIndex].data;
                                var tooltipLabel = data.labels[tooltipItem.index];
                                var tooltipData = allData[tooltipItem.index];
                                var total = 0;
                                for (var i in allData) {
                                    total += allData[i];
                                }
                                var tooltipPercentage = Math.round((tooltipData / total) * 100);
                                return tooltipLabel + ': ' + tooltipData + ' (' + tooltipPercentage + '%)';
                            }
                        },
                        enabled: true,
                    },
                    showPercentage: true
                }

            })
                            
        }
    })

//Số lượng chuyến
    Common.Services.Call($http, {
        url: Common.Services.url.MOBI,
        method: "MobiManage_NumOfTOMaster",
        data: {
            dtfrom: $scope.Filter.DateFrom,
            dtto: $scope.Filter.DateTo,
        },
        success: function (res) {
            $scope.data = {
                NumOfTOMaste: {
                    labels: ['MON', 'TUE', 'WEN', 'THU', 'FRI', 'SAT', 'SUN'],
                    datasets: [
                      {
                          label: 'Số chuyến',
                          fill: true,
                          lineTension: 0.1,
                          backgroundColor: "rgba(84, 90, 203, 1)",
                          borderColor: "rgba(75,192,192,1)",
                          borderCapStyle: 'butt',
                          borderDash: [],
                          borderDashOffset: 7,
                          pointBorderColor: "rgba(59, 59, 62, 1)",
                          pointBackgroundColor: "rgba(111, 111, 120, 1)",
                          pointBorderWidth: 5,
                          pointHoverRadius: 5,
                          pointHoverBackgroundColor: "rgba(255, 255, 255, 1)",
                          pointHoverBorderColor: "rgba(115, 201, 95, 1)",
                          pointHoverBorderWidth: 7,
                          pointRadius: 2,
                          pointHitRadius: 10,
                          spanGaps: false,
                          data: res
                      }
                    ]
                }
            }
        }
    })
});