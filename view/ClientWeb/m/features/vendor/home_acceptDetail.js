/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
angular.module('myapp').controller('vendor_homeAcceptDetailController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, openMap) {
    console.log('vendor_homeAcceptDetailController');

    $scope.masterID = $state.params.id;
    $scope.AcceptedItem = {};
    $scope.ShowMap = false;
    $scope.IsReceiveFull = false;

    $scope.LoadData = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileVendor_MasterGet",
            data: {id: $scope.masterID },
            success: function (res) {
                $scope.AcceptedItem = res;
                if (Common.HasValue(res)) {

                    $scope.IsReceiveFull = true;
                    angular.forEach($scope.AcceptedItem.lstLocationFrom, function (o, i) {
                        if (o.IsLeave == false)
                            $scope.IsReceiveFull = false;
                    })
                }
                $scope.LoadDataMap();
            }
        })
    }
    $scope.LoadData();

    $scope.BackToHome = function () {
        $state.go('vendor.home');
    }

    $scope.AddTrouble = function () {
        $state.go('vendor.home_trouble', { masterID: $scope.AcceptedItem.TOMasterID })
    }

    $scope.LocationComplete = function (masterID, locationID, statusID) {
        $rootScope.PopupConfirm({
            title: 'Xác nhận hoàn thành nhanh địa điểm này',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                Common.Services.Call($http, {
                    url: Common.Services.url.MOBI,
                    method: "FLMMobileVendor_LeaveLocation",
                    data: { masterID: masterID, locationID: locationID, statusID: statusID },
                    success: function (res) {
                        $scope.LoadData();
                        $ionicLoading.hide();
                    }
                })
            }
        });
    }
    
    //map
    $scope.ShowMapRoute = function () {
        $state.go('map', { p0: 1, p1: $scope.AcceptedItem.TOMasterID })
    }
    $scope.CreateMap = function () {
        Common.Log("CreateMap");

        Common.Log(openMap.hasMap);

        openMap.Create({
            Element: 'map',
            Tooltip_Show: false,
            InfoWin_Show: false,
            ClickMarker: null,
            ClickMap: null,
            mcallback: function (e) {
                $rootScope.LogWrite(e);
            }
        });

    }
    $scope.CreateMap();

    $scope.LoadDataMap = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobile_ListLocationOfMaster",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $ionicLoading.hide();
                $scope.ListLocationFrom = res.lstLocationFrom;
                $scope.ListLocationTo = res.lstLocationTo;
                $scope.$broadcast('scroll.refreshComplete');

                $scope.DrawMarker();
                $scope.ReloadMap();

            }
        });
    }

    $scope.ReloadMap = function () {
        $scope.DrawRoute();
    }

    $scope.DrawMarker = function () {
        $scope.Marker = [];
        angular.forEach($scope.ListLocationFrom, function (o) {
            var icon = openMap.mapStyle.Icon("img/marker_blue.png", 1);
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                $scope.Marker.push(openMap.Marker(o.Lat, o.Lng, o.LocationName, icon, o));
            }
        })
        angular.forEach($scope.ListLocationTo, function (o) {
            var icon = openMap.mapStyle.Icon("img/marker_pink.png", 1);
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                $scope.Marker.push(openMap.Marker(o.Lat, o.Lng, o.LocationName, icon, o));
            }
        })
        openMap.FitBound($scope.Marker);
    }

    $scope.DrawRoute = function () {
        openMap.Close(); //Hide info window
        openMap.ClearRoute(); //Clear route data, make them invisible from map
        openMap.SetVisible(null, false); //set all vectorlayers invisible from map
        for (var i = 0; i < $scope.Marker.length - 1; i++) {
            var pStart = $scope.Marker[i];
            var pEnd = $scope.Marker[i + 1];
            if (Common.HasValue(pStart) && Common.HasValue(pEnd)) {
                openMap.SetVisible([pStart, pEnd], true);
                var style = openMap.mapStyle.Route(6, 'rgba(3,169,244, 0.7)');
                openMap.Route("", pStart, pEnd, style);
            }
        }
    }
});