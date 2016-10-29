/// <reference path="../../angular.min.js" />

'use strict'

app.controller('DASH_IndexCtr', ['$scope', '$http', function ($scope, $http) {
  
    $http.post("/Pn/Pn/DASH_Read").then(function success(res) {
        $scope.totalOrder = res.data.totalOrder;
        $scope.totalProduct = res.data.totalProduct;
        $scope.totalProductTrue = res.data.totalProductTrue;
        $scope.totalProductFalse = res.data.totalProductFalse;
        $scope.totalProductNull = res.data.totalProductNull;
        $scope.totalPost = res.data.totalPost;
        $scope.newOrder = res.data.newOrder;
        $scope.viewOrder = res.data.viewOrder;
        $scope.okOrder = res.data.viewOrder;
    });

    var dNow = new Date();
    $scope.Filter = {
        DateFrom: dNow.addDays(-7).toISOString(),
        DateTo: dNow.toISOString()
    };
    //chart line
    $scope.options = {
        responsive: true,
        scaleShowGridLines: false,
        scaleGridLineColor: "rgba(0,0,0,.05)",
        scaleGridLineWidth: 1,
        bezierCurve: true,
        bezierCurveTension: 0.4,
        pointDot: true,
        pointDotRadius: 4,
        pointDotStrokeWidth: 1,
        pointHitDetectionRadius: 20,
        datasetStroke: true,
        datasetStrokeWidth: 2,
        datasetFill: true,
        legend: {
            display: false
        },
        onAnimationProgress: function () { },
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

    $http.post("/Pn/Pn/DASH_OrderCharts", { dFrom: $scope.Filter.DateFrom, dTo: $scope.Filter.DateTo }).then(function success(res) {
        var data = res.data.result;
        var ctx = document.getElementById("chartOrder");
        var lineChart = new Chart(ctx, {
            type: 'line',
            data: {
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
                         data: data
                     }
                ]
            }
        });
    });

}]);