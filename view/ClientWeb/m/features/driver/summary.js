angular.module('myapp').controller('driver_summaryController', function ($rootScope, $scope, $ionicLoading, $state, $location, $http, $timeout, $ionicSideMenuDelegate, charting, ionicDatePicker, $cordovaDatePicker, dataService) {
    console.log('driver_summaryController');

    $rootScope.DriverID = Common.Auth.Item.DriverID;
    $scope.selectedTab = 1;
    $scope.salaryChart = null;
    $scope.performChart = null;
    $scope.inputDate = null;

    var date = new Date();
  
    $scope.CRDate = function (fnCallback) {
        var options = {
            callback:function (val) {
                fnCallback(val);
            },
            from: new Date(2010, 8, 1),
            to: new Date(2025, 8, 1),
            inputDate: $scope.inputDate,
            mondayFirst: true,
            setLabel: 'Ok',
            todayLabel: 'Hôm nay',
            closeLabel: 'Đóng',
            weeksList: ["CN", "H", "B", "T", "N", "S", "B"],
            monthsList: ["Th 01", "Th 02", "Th 03", "Th 04", "Th 05", "Th 06", "Th 07", "Th 08", "Th 09", "Th 10", "Th 11", "Th 12"],
            dateFormat: 'dd/MM/yyyy',
            showTodayButton: true,
            closeOnSelect: false,
            templateType: 'popup'
        };
        return options;
    }


    $scope.H_dateFrom_callback = function () {
        $scope.inputDate = $scope.HSearch.dSFrom;
        ionicDatePicker.openDatePicker($scope.CRDate(function (val) {
            if (!new Date(val)) {

            } else {
                $scope.HSearch.dSFrom = new Date(val);
                $scope.inputDate = $scope.HSearch.dSFrom;
            }
        }));
    }
    

    $scope.H_dateTo_callback = function () {
        $scope.inputDate = $scope.HSearch.dSTo;
        ionicDatePicker.openDatePicker($scope.CRDate(function (val) {
            if (!new Date(val)) {

            } else {
                $scope.HSearch.dSTo = new Date(val);
                $scope.inputDate = $scope.HSearch.dSTo;
            }
        }));
    };

    $scope.SSearch = {
        dSFrom: new Date(date.getFullYear(), date.getMonth(), 1),
        dSTo: new Date(date.getFullYear(), date.getMonth() + 1, 0)
    }
    $scope.HSearch = {
        dSFrom: new Date(date.getFullYear(), date.getMonth(), 1),
        dSTo: new Date(date.getFullYear(), date.getMonth() + 1, 0)
    }

    //Performance

    $scope.myChartOpts = charting.pieChartOptions;

    

    $scope.S_dateFrom_callback = function (val) {
        $scope.inputDate = $scope.SSearch.dSFrom;
        ionicDatePicker.openDatePicker($scope.CRDate(function (val) {
            if (!new Date(val)) {
            } else {
                $scope.SSearch.dSFrom = new Date(val);
                $scope.inputDate = $scope.SSearch.dSFrom;
            }
        }));       
    };

    $scope.S_dateTo_callback = function (val) {
        $scope.inputDate = $scope.SSearch.dSTo;
        ionicDatePicker.openDatePicker($scope.CRDate(function (val) {
            if (!new Date(val)) {
            } else {
                $scope.SSearch.dSTo = new Date(val);
                $scope.inputDate = $scope.SSearch.dSTo;
            }
        }));    
    };

    $scope.loadHistory = function () {
        var driverID = $rootScope.DriverID,
            dtfrom = $scope.HSearch.dSFrom,
            dtto = $scope.HSearch.dSTo;
        dataService.FLMMobileDriverHistory_List(driverID,dtfrom, dtto).then(function (res) {
            $ionicLoading.hide(); $scope.$broadcast('scroll.refreshComplete');
            $scope.HistoryList = res;
            $ionicLoading.hide();
            var hdata = [];
            hdata[0] = ['Complete', 0];
            hdata[1] = ['Cancel', 1];
            angular.forEach(res, function (o, i) {
                if (o.IsReject) {
                    hdata[1][1]++;
                }
                else {
                    hdata[0][1]++;
                }
            })
            $scope.someData = [hdata];
        })      
    }
    $scope.loadSalary = function () {
        var dtfrom = $scope.SSearch.dSFrom,
            dtto = $scope.SSearch.dSTo;
       dataService.FLMMobileDriverSalary_List(dtfrom,dtto).then(function (res) {
           $ionicLoading.hide();
           $scope.SalaryList = res;
           var data = [[]];
           var hasData = false;
           data[0].push(['Lương chuyến', 0]);
           data[0].push(['Lương cơ bản', 0]);
           data[0].push(['Lương thưởng', 0]);
           data[0].push(['Lương doanh thu', 0]);
           angular.forEach(res, function (o, i) {
               switch (o.CostName) {
                   case 'Lương chuyến':
                       data[0][0][1] += o.Price;
                       hasData = true;
                       break;
                   case 'Lương thưởng':
                       data[0][1][1] += o.Price;
                       hasData = true;
                       break;
               }
           })
           if (hasData) {
               $scope.salaryData = data;

               if (Common.HasValue($scope.salaryChart))
                   $scope.salaryChart.destroy();
               $timeout(function () {
                   $scope.salaryChart = $.jqplot('salaryChart', $scope.salaryData, charting.donutOptions);
               }, 100)
           }
       })        
    }

    $scope.$watch("HSearch.dSFrom", function (newValue, oldValue) {
        if (newValue != oldValue) {
            $ionicLoading.show();
            $scope.loadHistory();
        }
    });
    $scope.$watch("HSearch.dSTo", function (newValue, oldValue) {
        if (newValue != oldValue) {
            $ionicLoading.show();
            $scope.loadHistory();
        }
    });

    $scope.$watch("SSearch.dSFrom", function (newValue, oldValue) {
        if (newValue != oldValue) {
            $ionicLoading.show();
            $scope.loadSalary();
        }
    });
    $scope.$watch("SSearch.dSTo", function (newValue, oldValue) {
        if (newValue != oldValue) {
            $ionicLoading.show();
            $scope.loadSalary();
        }
    });

    $scope.SelectTab = function (idx) {
        if (idx == 2) {
            $scope.performChart.destroy();
            $timeout(function () {
                $scope.salaryChart = $.jqplot('salaryChart', $scope.salaryData, charting.donutOptions);
            },100)
        }
        else if (idx == 3) {
            if (Common.HasValue($scope.salaryChart))
                $scope.salaryChart.destroy();
            $timeout(function () {
                $scope.performChart = $.jqplot('chart', $scope.someData, charting.pieChartOptions);
            }, 100)
        }
        else {
            if(Common.HasValue($scope.salaryChart))
                $scope.salaryChart.destroy();
            if (Common.HasValue($scope.performChart))
                $scope.performChart.destroy();
        }
        $scope.selectedTab = idx;
    }

    $scope.LoadData = function () {
        $scope.lstCheck = [false, false, false, false, false, false];
        var driverID = $rootScope.DriverID,
            dtfrom = $scope.HSearch.dSFrom,
            dtto = $scope.HSearch.dSTo;
        dataService.FLMMobileDriverHistory_List(driverID,dtfrom,dtto).then(function (res) {
            $ionicLoading.hide(); $scope.$broadcast('scroll.refreshComplete');
            $scope.HistoryList = res;
            $ionicLoading.hide();

            var hdata = [];
            hdata[0] = ['Complete', 0];
            hdata[1] = ['Cancel', 1];
            angular.forEach(res, function (o, i) {
                if (o.IsReject) {
                    hdata[1][1]++;
                }
                else {
                    hdata[0][1]++;
                }
            })
            $scope.someData = [hdata];
            $scope.performChart = $.jqplot('chart', $scope.someData, charting.pieChartOptions);
        })

        dataService.FLMMobileDriverSalary_List($scope.SSearch.dSFrom, $scope.SSearch.dSTo).then(function (res) {
            $scope.SalaryList = res;
            var data = [[]];
            var hasData = false;
            data[0].push(['Lương chuyến', 0]);
            data[0].push(['Lương cơ bản', 0]);
            data[0].push(['Lương thưởng', 0]);
            data[0].push(['Lương doanh thu', 0]);
            angular.forEach(res, function (o, i) {
                switch (o.CostName) {
                    case 'Lương chuyến':
                        data[0][0][1] += o.Price;
                        hasData = true;
                        break;
                    case 'Lương thưởng':
                        data[0][1][1] += o.Price;
                        hasData = true;
                        break;
                }
            })
            if (hasData) {
                $scope.salaryData = data;
                $scope.salaryChart = $.jqplot('salaryChart', $scope.salaryData, charting.donutOptions);
            }
        })       
    }
    $scope.LoadData();

    $scope.Master = function (item) {
        $state.go('driver.summary_master', { timeSheetDriverID: item.TimeSheetDriverID });
    }

    //Incomde
    $scope.salaryData = [[['s',10]]];
    $scope.SalaryChartOpts = charting.donutOptions;
});