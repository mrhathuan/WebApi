
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIStatus_Order = {
    URL: {
        Read_Customer: 'REP_Customer_Read',
        Excel_Export: 'REPDIOrder_Export',
        Get_TransportMode: 'ALL_SYSVarTransportMode'
    },
}

angular.module('myapp').controller('REPDIStatusOrderData_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIStatusOrderData_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.REPDIOrderItem = {
        lstCustomerID: [],
        TransportModeID: 0,
        DateFrom: new Date(),
        DateTo: new Date(),
        IsRemainning: false
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                CustomerName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function () {
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
        },
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
        method: _REPDIStatus_Order.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

    $scope.cboTransportMode_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID', index: 0,
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (res) {

            $scope.cboTransportMode_Options.dataSource.data(res);
        }
    })

    $scope.ExportExcel_Click = function ($event) {
        $event.preventDefault();

        if (!Common.HasValue($scope.REPDIOrderItem.DateFrom) || !Common.HasValue($scope.REPDIOrderItem.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.REPDIOrderItem.DateFrom > $scope.REPDIOrderItem.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        } else if (!Common.HasValue($scope.REPDIOrderItem.TransportModeID) || $scope.REPDIOrderItem.TransportModeID == 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Hình thức v.chuyển chính xác',
                Close: null,
                Ok: null
            })
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIStatus_Order.URL.Excel_Export,
                data: { lstCustomer: $scope.REPDIOrderItem.lstCustomerID, dateFrom: $scope.REPDIOrderItem.DateFrom, dateTo: $scope.REPDIOrderItem.DateTo, IsRemaining: $scope.REPDIOrderItem.IsRemainning, TransportModeID: $scope.REPDIOrderItem.TransportModeID },
                success: function (res) {
                    $rootScope.DownloadFile(res);
                }
            });
        }
    }

}]);