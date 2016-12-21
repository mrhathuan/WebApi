angular.module('myapp').service('locationService', function ($http, $rootScope, $ionicPopup) {
    var callbackFn = function (location) {
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "Extend_VehiclePosition_Add",
            data: {
                vehicleCode: '61c-08983',
                lat: location.latitude,
                lng: location.longitude,
                deviceDate: new Date(),
            },
            success: function (res) {
                
            }, error: function (err) {
                $ionicPopup.alert({ title:JSON.stringify(err)});
            }
        })
        backgroundGeoLocation.finish();
    };

 var failureFn = function (error) {
     console.log('BackgroundGeoLocation error ' + JSON.stringify(error));
 }

 var start = function () {
     window.localStorage.setItem('bgGPS', 1);
     backgroundGeoLocation.configure(callbackFn, failureFn, {
         desiredAccuracy: 10,
         stationaryRadius: 20,         
         locationService: 'ANDROID_DISTANCE_FILTER',
         debug: true,
         stopOnTerminate: false
     });
     backgroundGeoLocation.start();
 };
 this.init = function () {
     var bgGPS = window.localStorage.getItem('bgGPS');
     if (bgGPS == 1 || bgGPS == null) {
         start();
     }
 }
 this.stop = function () {
     window.localStorage.setItem('bgGPS', 0);
     backgroundGeoLocation.stop();
 }
});