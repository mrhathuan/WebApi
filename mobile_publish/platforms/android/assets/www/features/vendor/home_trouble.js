angular.module('myapp').controller('vendor_homeTroubleController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, $ionicSideMenuDelegate, $ionicModal) {
    console.log('vendor_homeTroubleController');

    $ionicSideMenuDelegate.canDragContent(false)
    $scope.TroubleItem = {};
    $scope.GroupTroubleItem = 0;
    $scope.TroubleList = [];
    $scope.FileList = [];
    $scope.Host = Common.Services.url.Host;
    $scope.zoomphoto = false;

    $scope.LoadTroubleList = function (masterID) {
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobileDriver_TroubleList",
            data: {
                masterID: masterID,
            },
            success: function (res) {
                $ionicLoading.hide();
                $scope.TroubleList = res;
                angular.forEach($scope.TroubleList, function (o, i) {
                    o.lstFile = [];
                    Common.Services.Call($http, {
                        url: Common.Services.url.MOBI,
                        method: 'FLMMobileDriver_FileList',
                        data: { id: o.ID,code:"trouble" },
                        success: function (res) {
                            o.lstFile = res;
                        }
                    });
                })
            }
        })
    }

    $scope.LoadData = function () {
        $ionicLoading.show();
        Common.Services.Call($http, {
            url: Common.Services.url.MOBI,
            method: "FLMMobile_GroupTroubleList",
            data: {},
            success: function (res) {
                $scope.ListGroupTrouble = res;
                $scope.LoadTroubleList($state.params.masterID);
            }
        });
    }
    $scope.LoadData();

    $scope.SaveTrouble = function () {
        $scope.TroubleItem.DITOMasterID = $state.params.masterID;
        $rootScope.PopupConfirm({
            title: 'Gửi phát sinh ?',
            template: '',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                Common.Services.Call($http, {
                    url: Common.Services.url.MOBI,
                    method: "FLMMobile_TroubleSave",
                    data: { item: $scope.TroubleItem },
                    success: function (res) {
                        $ionicLoading.hide();
                        $rootScope.PopupAlert({
                            title: 'Thông báo',
                            template: 'Lưu thành công',
                            ok: function () { $scope.LoadTroubleList($state.params.masterID); }
                        })
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
        $http.post(Common.Services.url.Host + "/api/Mobile/SaveImage", fd, {
            transformRequest: angular.identity,
            headers: {
                'Content-Type': undefined,
                'auth': Common.Auth.HeaderKey
            }
        })
        .success(function (e) {
            e.ReferID = $scope.uploadItem.ID;
            e.TypeOfFileCode = 'Trouble';

            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: 'App_FileSave',
                data: { item: e },
                success: function (res) {
                    $rootScope.PopupAlert({
                        title: 'Thông báo',
                        template: 'Lưu thành công',
                        ok: function () { $scope.LoadTroubleList($state.params.masterID); }
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

    $scope.showImages = function (index, lst) {
        $scope.FileList=lst
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