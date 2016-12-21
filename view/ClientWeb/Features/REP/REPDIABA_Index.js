
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIABA = {
    URL: {
        Read_Customer: 'REP_Customer_Read',
        Excel_Export_Cost: 'REPDIABA_Cost_Export',
        Excel_Export_Transport: 'REPDIABA_Transport_Export',
    },
}

angular.module('myapp').controller('REPDIABA_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIABA_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.REPDIABAItem = {
        lstCustomerID: [],
        TransportModeID: 0,
        DateFrom: new Date(),
        DateTo: new Date(),
        IsRemainning: false
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
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
        change: function (e) {
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
        }
    }
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
    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPDIABA.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });


    $scope.REPDIABA_ExportExcel_Cost_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        if (!Common.HasValue($scope.REPDIABAItem.DateFrom) || !Common.HasValue($scope.REPDIABAItem.DateTo)) {
            $rootScope.IsLoading = false;
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.REPDIABAItem.DateFrom > $scope.REPDIABAItem.DateTo) {
            $rootScope.IsLoading = false;
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIABA.URL.Excel_Export_Cost,
                data: { lstid: $scope.REPDIABAItem.lstCustomerID, dateFrom: $scope.REPDIABAItem.DateFrom, dateTo: $scope.REPDIABAItem.DateTo },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                }
            });
        }
        //$rootScope.IsLoading = false;
    }

    $scope.REPDIABA_ExportExcel_Transport_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        if (!Common.HasValue($scope.REPDIABAItem.DateFrom) || !Common.HasValue($scope.REPDIABAItem.DateTo)) {
            $rootScope.IsLoading = false;
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.REPDIABAItem.DateFrom > $scope.REPDIABAItem.DateTo) {
            $rootScope.IsLoading = false;
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            if ($scope.REPDIABAItem.lstCustomerID.length == 0)
            {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Vui lòng chọn ít nhất 1 khách hàng',
                    Close: null,
                    Ok: null
                })
            } else {
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPDIABA.URL.Excel_Export_Transport,
                    data: { lstid: $scope.REPDIABAItem.lstCustomerID, dateFrom: $scope.REPDIABAItem.DateFrom, dateTo: $scope.REPDIABAItem.DateTo },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                });
            }
        }
    }
}])