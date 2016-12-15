/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('manage_opsController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, charting, openMap) {
    console.log('manage_opsController');
    
    var skip = 0;
    var pageSize = 5;
    $scope.View = 1;
    $scope.Filter =
    {
        DateFrom: new Date(),
        DateTo: new Date().addDays(1),
    };
    $scope.StatusID = 0;

    $scope.TOMasterSummary = [];
    $scope.LoadDataSummary = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "MobiManage_TOMasterSummary",
            data: {
                dtfrom: $scope.Filter.DateFrom,
                dtto: $scope.Filter.DateTo,
            },
            success: function (res) {
                $ionicLoading.hide();
                $scope.TOMasterSummary = res;
            }
        })
    }
    $scope.LoadDataSummary();
    $scope.MasterSummaryClick = function (masterStatus) {
        $scope.View = 2;
        $scope.StatusID = masterStatus;
        $scope.LoadData();
    };

    $scope.TOMasterData = [];
    $scope.LoadData = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "MobiManage_TOMasterList",
            data: {
                skip: skip,
                pageSize: pageSize,
                dtfrom: $scope.Filter.DateFrom,
                dtto: $scope.Filter.DateTo,
                masterStatus: $scope.StatusID
            },
            success: function (res) {
                $ionicLoading.hide();
                $scope.TOMasterData =res;
            }
        })
    }

    $scope.BackTo = function () {
        if($scope.View==1)
            $state.go('manage.dashboard');
        else {
            $scope.View--;
        }
    }

    $scope.ColorClass = function (code) {
        var str = '';
        switch (code) {
            case 'plan':
                str = 'color-grey';
                break;
            case 'delivery':
                str = 'color-orange';
                break;
            case 'received':
                str = 'color-green';
                break;
            case 'invoice':
                str = 'color-orchid';
                break;
            default:
                str = '';
                break;
        }
        return str;
    }

    $scope.BackgroundClass = function (code) {
        var str = '';
        switch (code) {
            case 'plan':
                str = 'background-grey';
                break;
            case 'delivery':
                str = 'background-orange';
                break;
            case 'received':
                str = 'background-green';
                break;
            case 'invoice':
                str = 'background-orchid';
                break;
            default:
                str = '';
                break;
        }
        return str;
    }
    
    //comobobox
    $scope.PickDate = function () {
        var data = [
            { text: '1 ngày', value: 1 },
            { text: '2 ngày', value: 2 },
            { text: '1 tuần', value: 3 }
        ]
        $scope.CreateDataCBB(data, 'text', $scope.CbbModel)
    }

    $scope.CreateDataCBB = function (data, textField, model) {
        $scope.ShowSelectPane = true;
        $scope.DataCombobox = [];
        $scope.ComoboboxModel = model;

        angular.forEach(data, function (o, i) {
            $scope.DataCombobox.push({ text: o[textField], obj: o });
        })
    }

    $scope.ComboboxSelect = function (item) {
        switch (item.value) {
            case 1:
                $scope.Filter.DateFrom = new Date();
                $scope.Filter.DateTo = new Date().addDays(1);
                break;
            case 2:
                $scope.Filter.DateFrom = new Date().addDays(-1);;
                $scope.Filter.DateTo = new Date().addDays(1);
                break;
            case 3:
                $scope.Filter.DateFrom = new Date().addDays(-16);;
                $scope.Filter.DateTo = new Date().addDays(1);
                break;
        }
        $scope.ShowSelectPane = false;
        $scope.LoadDataSummary();
    }

    $scope.CloseSelectPane = function () {
        $scope.ShowSelectPane = false;
    }

    //
});