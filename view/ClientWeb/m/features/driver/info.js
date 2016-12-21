angular.module('myapp').controller('driver_infoController', function ($rootScope, $scope, $window, $ionicLoading, $state, $location, $http, $timeout, $ionicPopup, dataService) {
    console.log('driver_infoController');

    $scope.show = {
        profile:true,
    }
    $scope.selindex = 0;
    $scope.CustomerID = Common.Auth.Item.CustomerID;

    $scope.Init = function () {
        dataService.GetDriverInfo($rootScope.DriverID).then(function (res) {
            $rootScope.DriverItem = res;
        })
      

    }
    //$scope.Init();

    $scope.LogOut = function () {
        $rootScope.PopupConfirm({
            title: 'Bạn có muốn quay trở lại giao diện đăng nhập?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                location.href = 'index.html';
            }
        });
    }

    $scope.ChangeView = function () {
        $rootScope.PopupConfirm({
            title: 'Bạn có muốn chuyển sang giao diện nhà xe?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $state.go('vendor.home');
            }
        });
    }
    //test
    $scope.scanBarcode = function() {
        $cordovaBarcodeScanner.scan().then(function(imageData) {
            $scope.barcode = JSON.stringify(imageData);
        }, function(error) {
            console.log("An error happened -> " + error);
        });
    };
 
    $scope.takePicture = function () {
        navigator.camera.getPicture(function (imageURI) {
            $timeout(function () { $scope.lastPhoto = imageURI; }, 100)
            
            // imageURI is the URL of the image that we can use for
            // an <img> element or backgroundImage.

        }, function (err) {

            // Ruh-roh, something bad happened

        }, {
            quality: 75,
            targetWidth: 320,
            targetHeight: 320,
            saveToPhotoAlbum: false
        });
    }

    $scope.centerOnMe = function () {

        $ionicLoading.show({
            template: 'Loading...'
        });


        navigator.geolocation.getCurrentPosition(function (position) {
            $scope.lat = position.coords.latitude;
            $scope.lng = position.coords.longitude;
            $ionicLoading.hide();
        });

    };

});