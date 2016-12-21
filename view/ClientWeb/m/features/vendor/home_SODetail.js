angular.module('myapp').controller('vendor_homeSODetailController', function ($rootScope, $scope, $state, $stateParams, $location, $http, $timeout, $ionicLoading, $ionicModal) {
    console.log('vendor_homeSODetailController');

    $scope.masterID = $stateParams.masterID;
    $scope.locationID = $stateParams.locationID;
    $scope.statusID = 1;

    $scope.StatusObj = {
        Close: 221,
        Plan: 222,
        Come: 223,
        LoadStart: 224,
        LoadEnd: 225,
        Leave:226
    }

    if ($stateParams.statusID < $scope.StatusObj.Come) {
        $scope.statusID = 1;
    }
    else if ($stateParams.statusID == $scope.StatusObj.Come) {
        $scope.statusID = 2;
    }
    else if ($stateParams.statusID < $scope.StatusObj.LoadStart) {
        $scope.statusID = 3;
    }
    else if ($stateParams.statusID < $scope.StatusObj.LoadEnd) {
        $scope.statusID = 4;
    }
    else {
        $scope.statusID = 5;
    }

    Common.RootObj.selectedTab = 2;
    $scope.SOList = [];
    $scope.FileList = [];
    $scope.Host = Common.Services.url.Host;

    $scope.LoadSO = function () {

        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobile_SOList",
            data: {
                masterID: $scope.masterID,
                locationID: $scope.locationID
            },
            success: function (res) {
                $ionicLoading.hide();
                $scope.SOList = res;
                angular.forEach($scope.SOList, function (o, i) {
                    o.lstFile = [];
                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: 'FLMMobileDriver_FileList',
                        data: { id: o.ID, code: "pod" },
                        success: function (res) {
                            o.lstFile = res;
                        }
                    });
                })
            }
        })
    }
    $scope.LoadSO();

    $scope.Reject = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "Reject",
            data: {
                timeSheetDriverID: $scope.timeSheetDriverID,
            },
            success: function (res) {
                $state.go('main.truck');
            }
        })
    }

    $scope.LocationComplete = function () {
        $rootScope.PopupConfirm({
            title: 'Xác trạng thái?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                Common.Services.Call($http, {
                    url: Common.Services.url.MOBI,
                    method: "FLMMobileVendor_LeaveLocation",
                    data: {masterID: $scope.masterID, locationID: $scope.locationID, statusID: $scope.statusID },
                    success: function (res) {
                        $scope.statusID++;
                        $ionicLoading.hide();
                    }
                })
            }
        });
    }

    $scope.filesChanged = function (e) {
        $scope.fileUploads = e.files;
        var file = $scope.fileUploads;
        var fd = new FormData();
        fd.append('file', file[0]);
        $http.post(Common.Services.url.Host + "api/Mobile/SaveImage", fd, {
            transformRequest: angular.identity,
            headers: {
                'Content-Type': undefined,
                'auth': Common.Auth.HeaderKey
            }
        })
        .success(function (e) {
            e.ReferID = $scope.uploadItem.ID;
            e.TypeOfFileCode = 'DIPOD';

            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: 'App_FileSave',
                data: { item: e },
                success: function (res) {
                    $rootScope.PopupAlert({
                        title: 'Thông báo',
                        template: 'Lưu thành công',
                        ok: function () { $scope.LoadSO(); }
                    })
                }
            });
        })
        .error(function () {
        });
    }
    $scope.BtnUpload = function (item) {
        $scope.uploadItem = item;
        $('#file').click();
    }

    $scope.takePicture = function () {
        navigator.camera.getPicture(function (imageURI) {
            $timeout(function () {

                var server = Common.Services.url.Host + "api/Mobile/SaveImage",
                    filePath = imageURI;

                var date = new Date();

                var options = {
                    fileKey: "file",
                    fileName: imageURI.substr(imageURI.lastIndexOf('/') + 1),
                    chunkedMode: false,
                    mimeType: "image/jpg"
                };
            }, 100)

            
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

    // Close the modal
    $scope.closeModal = function () {
        $scope.modal.hide();
        $scope.modal.remove()
    };
});