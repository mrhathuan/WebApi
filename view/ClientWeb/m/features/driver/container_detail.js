angular.module('myapp').controller('driver_containerDetailController', function ($rootScope, $scope, $state, $stateParams, $location, $http, $timeout, $ionicLoading, $ionicModal, dataService) {
    console.log('driver_containerDetailController');

    $scope.masterID = $stateParams.masterID;

    $scope.statusID = 1
    $scope.ShowReturnDetail = false;
    $scope.ShowBtnAdd = true;

    $scope.BtnAdd = function () {
        $scope.ShowBtnAdd = !$scope.ShowBtnAdd;
    }

    Common.RootObj.selectedTab = 2;
    $scope.SOList = [];
    $scope.FileList = [];
    $scope.Host = Common.Services.url.Host;


    $scope.LoadSO = function () {
        dataService.FLMMobile_COList($scope.masterID).then(function (res) {
            $ionicLoading.hide();
            $scope.SOList = res;
            angular.forEach($scope.SOList, function (o, i) {
                o.lstFile = [];
                var id = o.ID,
                    code = "copod";
                dataService.FLMMobileDriver_FileList(id,code).then(function (res) {
                    o.lstFile = res;
                })               
            })
        })     
    }
    $scope.LoadSO();

    // nav bar
    $scope.BackToTruck = function () {
        $state.go($rootScope.PreviousState, $rootScope.PreviousParam);
    }

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
                rs.TypeOfFileCode = 'COPOD';

                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: 'App_FileSave',
                    data: { item: rs },
                    success: function (res) {
                        $rootScope.PopupAlert({
                            title: 'Thông báo',
                            template: 'Lưu thành công',
                            ok: function () { $scope.LoadSO(); }
                        })
                    }
                });
            },
            function (e) {
                $rootScope.PopupAlert({ title: "up fail: " + JSON.stringify(e) });
            }, options);
    }

    // $scope.filesChanged = function (e) {
    //     $scope.fileUploads = e.files;
    //     var file = $scope.fileUploads;
    //     var fd = new FormData();
    //     fd.append('file', file[0]);
    //     $http.post(Common.Services.url.Host + "/api/Mobile/SaveImage", fd, {
    //         transformRequest: angular.identity,
    //         headers: {
    //             'Content-Type': undefined,
    //             'auth': Common.Auth.HeaderKey
    //         }
    //     })
    //     .success(function (e) {
    //         e.ReferID = $scope.uploadItem.ID;
    //         e.TypeOfFileCode = 'COPOD';

    //         Common.Services.Call($http, {
    //             url: Common.Services.url.SYS,
    //             method: 'App_FileSave',
    //             data: { item: e },
    //             success: function (res) {
    //                 $rootScope.PopupAlert({
    //                     title: 'Thông báo',
    //                     template: 'Lưu thành công',
    //                     ok: function () { $scope.LoadSO(); }
    //                 })
    //             }
    //         });
    //     })
    //     .error(function () {
    //     });
    // }

    // $scope.BtnUpload = function (item) {
    //     $scope.uploadItem = item;
    //     $('#file').click();
    // }

    $scope.takePicture = function () {       
        try {    
            $scope.modal.hide();
            $scope.modal.remove();        
            navigator.camera.getPicture($scope.uploadPhoto, function (err) {
                $rootScope.PopupAlert({ title: "camera fail: " + JSON.stringify(e) });
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
            $rootScope.PopupAlert({ title: "Plugin fail: " + JSON.stringify(e) });
        }
    }

     $scope.choosePicture = function () {       
        try {    
            $scope.modal.hide();
            $scope.modal.remove();        
            navigator.camera.getPicture($scope.uploadPhoto, function (err) {
                $rootScope.PopupAlert({ title: "camera fail: " + JSON.stringify(e) });
            }, {
                quality: 50,
                targetWidth: 1280,
                targetHeight: 1280,
                saveToPhotoAlbum: false,
                destinationType: Camera.DestinationType.FILE_URI,
                sourceType: navigator.camera.PictureSourceType.PHOTOLIBRARY
            });           
        }
        catch (e) {
            $rootScope.PopupAlert({ title: "Plugin fail: " + JSON.stringify(e) });
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

    $scope.TempClick = function () {
        $scope.temp = $scope.CurrentTemperature;
        $rootScope.PopupConfirmInput({
            title: 'Thay đổi nhiệt độ hiện tại của xe',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<div style="margin-top:5px"><input type="number" class="input-temp" placeholder="Nhập nhiệt độ..." type="text" ng-model="temp"> ',
            ok: function (scope) {
                $scope.CurrentTemperature = scope.temp;
            }
        });
    }


    $scope.imageModal_Click = function(item){
        $scope.showModal('selectImage.html');
        $scope.uploadItem = item;
    }
    // Close the modal
    $scope.closeModal = function () {
        $scope.modal.hide();
        $scope.modal.remove()
    };
});