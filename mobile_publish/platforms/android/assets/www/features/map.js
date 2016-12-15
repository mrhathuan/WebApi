/// <reference path="~/m/js/common.js" />

angular.module('myapp').controller('mapController',function ($rootScope, $scope, $state,$ionicHistory, $location, $http, $timeout, $ionicLoading,$ionicSideMenuDelegate, openMap) {
	Common.Log('mapController');
	$ionicSideMenuDelegate.canDragContent(false)
	$scope.Marker = [];
	$scope.VehicleRouteMarker = [];
    $scope.viewtype = $state.params.p0;//get param
    $scope.ShowMap = true;
    $scope.ShowDetail = false;
    $rootScope.Host = Common.Services.url.Host;
    $scope.PartnerData = [];
    $scope.PartnerSearchKey = "";
    $scope.Lat = 0;
    $scope.Lng = 0;

    //#region Map
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
            }
        });

    }
    $timeout(function () { $scope.CreateMap(); }, 300)
    

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
        if ($scope.Marker.length > 0)
            openMap.FitBound($scope.Marker);
    }

    $scope.DrawRoute = function () {
        for (var i = 1; i < $scope.Marker.length; i++) {
            var pStart = $scope.Marker[i];
            var pEnd = $scope.Marker[i - 1];
            if (Common.HasValue(pStart) && Common.HasValue(pEnd)) {
                openMap.SetVisible([pStart, pEnd], true);
                var style = openMap.mapStyle.Route(6, 'rgba(3,169,244, 0.7)');
                openMap.Route("", pStart, pEnd, style);
            }
        }
    }

    $scope.CloseMap = function () {      
        $state.go($scope.stateBack);
    }

    //#endregion

    $scope.LoadDataMap = function (type) {
        $ionicLoading.show();
        var method='FLMMobile_ListLocationOfMaster';
        if (type == 'container')
            method = 'FLMMobile_ListCOLocationOfMaster';
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: method,
            data: { masterID: $scope.masterID },
            success: function (res) {
                $ionicLoading.hide();
                $scope.ListLocationFrom = res.lstLocationFrom;
                $scope.ListLocationTo = res.lstLocationTo;

                openMap.Close(); //Hide info window
                openMap.ClearRoute(); //Clear route data, make them invisible from map
                openMap.SetVisible(null, false); //set all vectorlayers invisible from map
                $scope.DrawMarker();
                $scope.ReloadMap();
            }
        });
    }

    $scope.Init = function () {

        switch ($scope.viewtype) {
            case "0": // cung duong cua chuyen
                $scope.masterID = parseInt($state.params.p1);
                if ($scope.masterID > 0) {
                    $scope.LoadDataMap();
                    Common.RootObj.selectedTab = 2;
                    $scope.stateBack = 'driver.truck';
                    $scope.statePar = {}
                }
                break;
            case "1":
                $scope.masterID = parseInt($state.params.p1);
                if ($scope.masterID > 0) {
                    $scope.LoadDataMap();
                    Common.RootObj.selectedTab = 2;
                    $scope.stateBack = 'vendor.home';
                    $scope.statePar = { id: $scope.masterID }
                }
                break;
            case "2":
                $scope.stateBack = 'manage';
                $scope.HideButton = [true, true, true, true];
                Common.Services.Call($http, {
                    url: Common.Services.url.MOBI,
                    method: "MobiManage_VehicleList",
                    data: {},
                    success: function (res) {
                        openMap.Close(); //Hide info window
                        openMap.ClearRoute(); //Clear route data, make them invisible from map
                        openMap.SetVisible(null, false); //set all vectorlayers invisible from map
                        $scope.VehicleMarker = [];
                        angular.forEach(res, function (o) {
                            var icon = openMap.mapStyle.Icon("img/ico_xe_going.png", 1);
                            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                                $scope.VehicleMarker.push(openMap.Marker(o.Lat, o.Lng, o.RegNo, icon, o));
                            }
                        })
                        openMap.FitBound($scope.VehicleMarker);
                    }
                })
                break;
            case "3": // cung duong cua container
                $scope.masterID = parseInt($state.params.p1);
                if ($scope.masterID > 0) {
                    $scope.LoadDataMap('container');
                    Common.RootObj.selectedTab = 2;
                    $scope.stateBack = 'driver.truck';
                    $scope.statePar = {}
                }
                break;
            case "4": // bao su co
                $scope.ProblemID = parseInt($state.params.p1);
                if ($scope.ProblemID > 0) {
                    $scope.stateBack = 'driver.problem';

                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: "ProblemGet",
                        data: { id: $scope.ProblemID },
                        success: function (res) {
                            openMap.Close(); //Hide info window
                            openMap.ClearRoute(); //Clear route data, make them invisible from map
                            openMap.SetVisible(null, false); //set all vectorlayers invisible from map
                            $scope.ProblemMarker = [];
                            var icon = openMap.mapStyle.Icon("img/rWarning.png", 1);
                            if (Common.HasValue(res.Lat) && Common.HasValue(res.Lng)) {
                                $scope.ProblemMarker.push(openMap.Marker(res.Lat, res.Lng, res.TypeOfRouteProblemName, icon, res));
                            }
                            if ($scope.ProblemMarker.length > 0)
                                openMap.FitBound($scope.ProblemMarker);
                        }
                    })
                }
                break;
        }
    }
   
    $scope.ShowActualRoute = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobile_MasterGetActualTime",
            data: {
                masterID: $scope.masterID
            },
            success: function (res) {
                if (res != null) {
                    var from = res.ATD;
                    var to = res.ATA;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: "Extend_VehiclePosition_Get",
                        data: {
                            vehicleCode: $rootScope.vehicleCode,
                            dtfrom: from,
                            dtto: to,
                        },
                        success: function (res) {
                            $ionicLoading.hide();
                            openMap.SetVisible($scope.VehicleRouteMarker, false);
                            $scope.VehicleRouteMarker = [];
                            //draw route
                            if (Common.HasValue($scope.RealRoute))
                                openMap.SetVisible($scope.RealRoute, false);
                            $scope.RealRoute = [];
                            var style = openMap.mapStyle.Line(4, 'rgba(150,50,50, 0.7)', 0.7, '', 'rgba(150,50,50, 0.7)', 0, [15, 10]);
                            for (var i = 1; i < res.length; i++) {
                                var pStart = res[i - 1];
                                var pEnd = res[i];
                                if (Common.HasValue(pStart) && Common.HasValue(pEnd)) {
                                    //openMap.SetVisible([pStart, pEnd], true);
                                    $scope.RealRoute.push(openMap.Polyline("", [openMap.Point(pStart.Lat, pStart.Lng), openMap.Point(pEnd.Lat, pEnd.Lng)], style))
                                }
                            }
                            if ($scope.RealRoute.length > 0)
                                openMap.FitBound($scope.RealRoute);
                        }
                    });
                }
                else
                    $ionicLoading.hide();
            }
        });

        
    }

    $scope.CurrentLocationClick = function () {
        var timeoutVal = 10 * 1000 * 1000;
        var opstions = { enableHighAccuracy: true, timeout: timeoutVal, maximumAge: 3000 };
        document.addEventListener("deviceready", function () {
            try {
                navigator.geolocation.getCurrentPosition(function (position) {
                    if (Common.HasValue($scope.MarkerCurrent))
                        openMap.SetVisible($scope.MarkerCurrent, false);
                    $scope.MarkerCurrent = [];
                    var icon = openMap.mapStyle.Icon("img/ico_xe_going.png", 1);
                    $scope.MarkerCurrent.push(openMap.Marker(position.coords.latitude, position.coords.longitude, "", icon, {}));
                    openMap.FitBound($scope.MarkerCurrent);
                }, function (e) {
                    $rootScope.PopupAlert({ title: JSON.stringify(e) });
                }, opstions);
            } catch (e) {
                $rootScope.PopupAlert({ title: 'catch: ' + JSON.stringify(e) });
            }
        }, false);
        
        
    }

    $scope.CheckInLocation = function (type) {
        $scope.ShowMap = false;
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileDriver_PartnerList",
            data: { type: type },
            success: function (res) {
                $ionicLoading.hide();
                $scope.PartnerData = res;
                $scope.PartnerDataBind = res;
            }
        });
    }

    $scope.BackToMap = function () {
        $scope.ShowMap = true;
    }

    $scope.PartnerSearch = function (e) {
        if (e.which === 13) {
            $scope.PartnerDataBind = [];
            angular.forEach($scope.PartnerData, function (o, i) {
                if (o.PartnerCode.toLowerCase().match(e.target.value.toLowerCase()) != null) {
                    $scope.PartnerDataBind.push(o);
                }
            })
        }
        
    }

    $scope.PartnerClick = function (item) {
        $scope.PartnerLocationItem = item;
        $scope.ShowDetail = true;
        $ionicLoading.show();
        navigator.geolocation.getCurrentPosition(function (position) {
            $scope.PartnerLocationItem.Lat = position.coords.latitude;
            $scope.PartnerLocationItem.Lng = position.coords.longitude;
            $ionicLoading.hide();
        });
    }
    $scope.BackToPartnerList = function () {
        $scope.ShowDetail = false;
    }

    $scope.LocationSave = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileDriver_CheckInLocation",
            data: { item: $scope.PartnerLocationItem },
            success: function (res) {
                $scope.ShowDetail = false;
                Common.Services.Call($http, {
                    url: Common.Services.url.MOBI,
                    method: "FLMMobileDriver_PartnerList",
                    data: { type: 1 },
                    success: function (res) {
                        $ionicLoading.hide();
                        $scope.PartnerData = res;
                        $scope.PartnerDataBind = res;
                    }
                });
            }
        });
    }

    if (Common.HasValue($state.params.p0) && Common.HasValue($state.params.p1) && $state.params.p0 != "" && $state.params.p1 != "") {
        $timeout(function () { $scope.Init(); }, 500)
        
    }
    else {
		$state.go('main');       
    }
});
