/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('vendor_homeController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading) {
    console.log('vendor_homeController');
    //$rootScope.CustomerID = Common.Auth.Item.CustomerID;
    $scope.selectedTab = 1;
    $scope.reasonID = 0;

    $scope.today = new Date();
    $scope.dateFrom = Common.Date.StartDay($scope.today.addDays(-20));
    $scope.dateTo = Common.Date.EndDay($scope.today.addDays(2));
    $scope._dataHasDNVendor = [];
    $scope._dataHasDNVehicle = [];
    $scope.CheckAllLoad = function (stt, callback) {
        $scope.lstCheck[stt] = true;
        var rs = true;
        for (var i = 0; i < $scope.lstCheck.length; i++) {
            if ($scope.lstCheck[i] == false) {
                rs = false;
                break;
            }
        }
        if (rs)
            callback();
    }
    $scope.LoadData = function () {
        $ionicLoading.show("Tải dữ liệu...");
        $scope.lstCheck = [false, false, false, false];

        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileVendor_TenderRequestList",
            data: {
                dtfrom: $scope.dateFrom,
                dtto: $scope.dateTo,
                vendorID:$rootScope.VendorID,
            },
            success: function (res) {
                $scope.CheckAllLoad(0, function () { $ionicLoading.hide(); $scope.$broadcast('scroll.refreshComplete'); });
                $scope.RequestList = res;
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileVendor_TenderAcceptList",
            data: {
                dtfrom: $scope.dateFrom,
                dtto: $scope.dateTo,
                vendorID: $rootScope.VendorID,
            },
            success: function (res) {
                $scope.CheckAllLoad(1, function () { $ionicLoading.hide(); $scope.$broadcast('scroll.refreshComplete'); });
                $scope.AcceptList = res;
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileVendor_TenderRejectList",
            data: {
                dtfrom: $scope.dateFrom,
                dtto: $scope.dateTo
            },
            success: function (res) {
                $scope.CheckAllLoad(2, function () { $ionicLoading.hide(); $scope.$broadcast('scroll.refreshComplete'); });
                $scope.RejectList = res;
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileVendor_ReasonList",
            data: {},
            success: function (res) {
                $scope.CheckAllLoad(3, function () { $ionicLoading.hide(); $scope.$broadcast('scroll.refreshComplete'); });
                $scope.ReasonData = res;
            }
        })

    }
    $scope.LoadData();

    $scope.TenderAccept = function (id) {
        $ionicLoading.show("Gửi duyệt...")
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileVendor_TenderApproved",
            data: {
                id: id,
            },
            success: function (res) {
                $ionicLoading.hide();
                $scope.LoadData();
            }
        })
    }

    $scope.TenderReject = function (id) {
        $rootScope.PopupConfirmInput({
            title: 'Chọn lý do từ chối',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<div style="margin-top:5px">Lý do từ chối</div><select style="width:100%;margin-bottom:8px;" ng-model="reasonID" ng-init="reasonID = ReasonData[0]==null? 0 :ReasonData[0]" ng-options="option.ReasonName for option in ReasonData"></select> <br/>Ghi chú<input type="text" ng-model="reasonNote"> ',
            ok: function (scope) {
                var rsid = scope.reasonID.ID;
                if (rsid > 0) {
                    $ionicLoading.show();
                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: "FLMMobileVendor_TenderReject",
                        data: {
                            id: id,
                            item: { ReasonID: rsid, Reason: scope.reasonNote }
                        },
                        success: function (res) {
                            $ionicLoading.hide();
                            $scope.LoadData();
                        }
                    })
                }
                else {
                    var confirmPopup = $ionicPopup.alert({
                        title: 'Lỗi',
                        template: "Chưa chọn lý do từ chối",
                    });
                }
            }
        });
    }

    $scope.LocationComplete = function (masterID, locationID, statusID) {
        $rootScope.PopupConfirm({
            title: 'Xác nhận rời khỏi điểm này?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                Common.Services.Call($http, {
                    url: Common.Services.url.MOBI,
                    method: "FLMMobileVendor_LeaveLocation",
                    data: {masterID: masterID, locationID: locationID, statusID: statusID },
                    success: function (res) {
                        $scope.LoadData();
                    }
                })
            }
        });
    }

});