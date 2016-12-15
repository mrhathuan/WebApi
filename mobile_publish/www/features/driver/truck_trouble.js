angular.module('myapp').controller('driver_trucktroubleController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, $ionicSideMenuDelegate, $ionicModal, dataService) {
    console.log('driver_trucktroubleController');

    $ionicSideMenuDelegate.canDragContent(false);
    Common.RootObj.selectedTab = 2;
    $scope.TroubleItem = {};
    $rootScope.DriverID = Common.Auth.Item.DriverID;
    $scope.GroupTroubleItem = 0;
    $scope.TroubleList = [];
    $scope.FileList = [];
    $scope.Host = Common.Services.url.Host;
    $scope.zoomphoto = false;
    var isCO = false;
    var typeOfFileCode = 'trouble';
    if ($state.params.type == "container") {
        typeOfFileCode = 'TroubleCO';
        isCO = true;
    }

    $scope.LoadTroubleList = function (masterID) {
        dataService.FLMMobileDriver_TroubleList(masterID, isCO).then(function (res) {
            $ionicLoading.hide();
            $scope.TroubleList = res;
            angular.forEach($scope.TroubleList, function (o, i) {
                o.lstFile = [];
                var id = o.ID,
                    code = typeOfFileCode;
                dataService.FLMMobileDriver_FileList(id,code).then(function (res) {
                    o.lstFile = res;
                })             
            })
        })
       
    }

    $scope.LoadData = function () {
        $ionicLoading.show();
        dataService.FLMMobile_GroupTroubleList(isCO).then(function (res) {
            $scope.ListGroupTrouble = res;
            $scope.LoadTroubleList($state.params.masterID);
        })       
    }
    $scope.LoadData();

    $scope.SaveTrouble = function () {
        if (!isCO)
            $scope.TroubleItem.DITOMasterID = $state.params.masterID;
        else
            $scope.TroubleItem.COTOMasterID = $state.params.masterID;

        $rootScope.PopupConfirm({
            title: 'Gửi phát sinh ?',
            template: '',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                navigator.geolocation.getCurrentPosition(function (position) {
                    $scope.TroubleItem.Lat = position.coords.latitude;
                    $scope.TroubleItem.Lng = position.coords.longitude;
                    var item = $scope.TroubleItem;
                    dataService.FLMMobile_TroubleSave($scope.TroubleItem).then(function (res) {
                        $ionicLoading.hide();
                        $rootScope.PopupAlert({
                            title: 'Thông báo',
                            template: 'Lưu thành công',
                            ok: function () { $scope.LoadTroubleList($state.params.masterID); }
                        })
                    })                   
                });

            }
        });

    }

    //$scope.filesChanged = function (e) {
    //    $scope.fileUploads = e.files;
    //    var file = $scope.fileUploads;
    //    var fd = new FormData();
    //    fd.append('file', file[0]);
    //    $http.post(Common.Services.url.Host + "/api/Mobile/SaveImage", fd, {
    //        transformRequest: angular.identity,
    //        headers: {
    //            'Content-Type': undefined,
    //            'auth': Common.Auth.HeaderKey
    //        }
    //    })
    //    .success(function (e) {
    //        e.ReferID = $scope.uploadItem.ID;
    //        e.TypeOfFileCode = typeOfFileCode;

    //        Common.Services.Call($http, {
    //            url: Common.Services.url.SYS,
    //            method: 'App_FileSave',
    //            data: { item: e },
    //            success: function (res) {
    //                $rootScope.PopupAlert({
    //                    title: 'Thông báo',
    //                    template: 'Lưu thành công',
    //                    ok: function () { $scope.LoadTroubleList($state.params.masterID); }
    //                })
    //            }
    //        });
    //    })
    //    .error(function () {
    //    });
    //}


    //$scope.BtnUpload = function (item) {
    //    $scope.uploadItem = item;
    //    $('#file').click();
    //}

    $scope.uploadPhoto = function (imageURI) {
        var options = new FileUploadOptions();
        options.fileKey = "file";
        options.fileName = "img.jpg";
        options.mimeType = "image/jpeg";
        options.headers = {
            'Content-Type': undefined,
            'auth': Common.Auth.HeaderKey
        };
        options.chunkedMode = false;
        var params = {};
        options.params = params;
        var ft = new FileTransfer();
        ft.upload(imageURI, encodeURI(Common.Services.url.Host + "/api/Mobile/SaveImage"),
            function (e) {
                var rs = JSON.parse(e.response);
                rs.ReferID = $scope.uploadItem.ID;
                rs.TypeOfFileCode = typeOfFileCode;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: 'App_FileSave',
                    data: { item: rs },
                    success: function (res) {
                        $rootScope.PopupAlert({
                            title: 'Thông báo',
                            template: 'Lưu thành công',
                            ok: function () { $scope.LoadTroubleList($state.params.masterID); }
                        })
                    }
                });
            },
            function (e) {
                $rootScope.PopupAlert({ title: "up fail: " + JSON.stringify(e) });
            }, options);
    }


    $scope.takePicture = function (item) {
        $scope.uploadItem = item;
        try {
            navigator.camera.getPicture($scope.uploadPhoto, function (err) {
            }, {
                quality: 50,
                targetWidth: 1280,
                targetHeight: 1280,
                saveToPhotoAlbum: false,
                destinationType: Camera.DestinationType.FILE_URI,
                sourceType: navigator.camera.PictureSourceType.CAMERA
            });
        }
        catch (e) {
        }
    }

    $scope.showImages = function (index, lst) {
        $scope.FileList = lst
        $scope.activeSlide = index;
        $scope.showModal('features/image-popover.html');
    }

    $scope.showModal = function (templateUrl) {
        $ionicModal.fromTemplateUrl(templateUrl, {
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function (modal) {
            $scope.modal = modal;
            $scope.modal.show();
        });
    }
    // nav bar
    $scope.BackToTruck = function () {
        $state.go('driver.truck');
    }
    // Close the modal
    $scope.closeModal = function () {
        $scope.modal.hide();
        $scope.modal.remove()
    };
});