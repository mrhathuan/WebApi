/// <reference path="~/m/common.js" />
app.controller('mapviewCtrl', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, $ionicPopup, $ionicSideMenuDelegate, openMap, $interval) {
    $rootScope.LogWrite("mapviewCtrl")
    $ionicSideMenuDelegate.canDragContent(false)

    $scope.IsReceiveFull = false;
    $scope.StatusObj = {
        Close: 221,
        Plan: 222,
        Come: 223,
        LoadStart: 224,
        LoadEnd: 225,
        Leave: 226
    };
    $scope.stateBack = '';
    $scope.Marker = [];

    $rootScope.DriverID = Common.Auth.Item.DriverID;
    $rootScope.DirverItem = Common.Auth.Item;
    $rootScope.Host = Common.Services.url.Host;
    //get param
    $scope.viewtype = $state.params.p0;
    // event

    $scope.LoadDataMap = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobile_ListLocationOfMaster",
            data: { masterID: $scope.masterID },
            success: function (res) {
                $ionicLoading.hide();
                $scope.ListLocationFrom = res.lstLocationFrom;
                $scope.ListLocationTo = res.lstLocationTo;
                $scope.DrawMarker();
                $scope.ReloadMap();
            }
        });

        navigator.geolocation.getCurrentPosition(function (position) {
            var icon = openMap.mapStyle.Icon("img/marker.png", 1);
            $rootScope.LogWrite(position.coords.latitude + " - " + position.coords.longitude);
            $scope.Marker.push(openMap.Marker(position.coords.latitude, position.coords.longitude, "", icon, {}));
        });
    }

    $scope.Init = function () {

        switch ($scope.viewtype) {
            case "0": // cung duong cua chuyen
                $scope.masterID = parseInt($state.params.p1);
                if ($scope.masterID > 0) {
                    $scope.LoadDataMap();
                    Common.RootObj.selectedTab = 2;
                    $scope.stateBack = 'main.truck';
                }
                break;
            case "1":
                break;
        }
    }
    $scope.Init();

    $scope.CloseMap = function () {
        $state.go($scope.stateBack);
    }

    //map

    $scope.CreateMap = function () {
        Common.Log("CreateMap");

        Common.Log(openMap.hasMap);

        openMap.Create({
            Element: 'mapview',
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