angular.module('myapp').controller('driver_summaryDetailController', function ($rootScope, $scope, $state, $stateParams, $location, $http, $timeout, $ionicLoading, $ionicModal, dataService) {
    console.log('driver_summaryDetailController');

    $scope.timesheetID = $stateParams.timesheetID;
    $scope.statusID = 1
    $scope.selectedTab = 1
    $scope.ShowReturnDetail = false;
    $scope.GOPItem = {};
    $scope.ShowBtnAdd = true;
    $scope.SOAddressCombobox = {};
    $scope.GOPCombobox = {};
    $scope.ProductCombobox = {};

    $scope.BtnAdd = function () {
        $scope.ShowBtnAdd = !$scope.ShowBtnAdd;
    }


    Common.RootObj.selectedTab = 2;
    $scope.SOList = [];
    $scope.FileList = [];
    $scope.Host = Common.Services.url.Host;


    $scope.LoadSO = function () {
        dataService.FLMMobile_SummarySOList($scope.timesheetID).then(function (res) {
            $ionicLoading.hide();
            $scope.SOList = res;
            angular.forEach($scope.SOList, function (o, i) {
                o.lstFile = [];
                var id = o.ID,
                    code = "dipod";
                dataService.FLMMobileDriver_FileList(id,code).then(function (res) {
                    o.lstFile = res;
                })               
            })
        })       
    }
    $scope.LoadSO();

    $scope.SwicthTab = function (i) {
        $scope.selectedTab = i;
    }

    // nav bar
    $scope.BackToTruck = function () {
        $state.go($rootScope.PreviousState.name);
    }

    //#region GOPreturn

    $scope.ReloadProduct = function () {
        $scope.ProductCombobox.Clear();
        $scope.GOPItem.ProductID = 0;
        $scope.ProductReturnList = [];
        angular.forEach($scope.ProductReturnData, function (o, i) {
            if (o.GroupOfProductID == $scope.GOPItem.GroupProductID)
                $scope.ProductReturnList.push(o);
        })
    }

    $scope.LoadGOPReturn = function () {

        $ionicLoading.show();
        dataService.Mobile_GOPReturnList($scope.masterID, $scope.locationID).then(function (res) {
            $scope.ReturnList = res;
        })
       
        dataService.Mobile_DITOGroupProductList($scope.masterID, $scope.locationID).then(function (res) {
            if (res.length > 0) {
                $scope.SOAddressList = res;
            }
            else {
                $scope.NoReturn = true;
            }
        })

        dataService.Mobile_CUSGOPList($scope.masterID).then(function (res) {
            if (res.length > 0) {
                $scope.GOPReturnList = res;
            }
            else {
                $scope.NoReturn = true;
            }
        })

        dataService.Mobile_CUSProductList($scope.masterID).then(function (res) {
            $ionicLoading.hide();
            if (res.length > 0) {
                $scope.ProductReturnData = res;
                $scope.ReloadProduct();
            }
            else {
                $scope.NoReturn = true;
            }
        })
    }
    //$scope.LoadGOPReturn();

    $scope.GOPReturn_Save = function () {
        if ($scope.GOPItem.OrderGroupID > 0 && $scope.GOPItem.GroupProductID > 0 && $scope.GOPItem.ProductID > 0 && $scope.GOPItem.Quantity > 0) {
            $scope.GOPItem.MasterID = $scope.masterID;
            $rootScope.PopupConfirm({
                title: 'Xác nhận lưu ?',
                okText: 'Chấp nhận',
                cancelText: 'Từ chối',
                ok: function () {
                    $ionicLoading.show();
                    dataService.Mobile_GOPReturnSave($scope.GOPItem).then(function (res) {
                        $ionicLoading.hide();
                        $scope.ShowReturnDetail = false;
                        $scope.LoadGOPReturn();
                        $scope.SOAddressCombobox.Clear();
                        $scope.GOPCombobox.Clear();
                        $scope.ProductCombobox.Clear();
                    })              
                }
            });
        }
        else {
            $rootScope.PopupAlert({
                title: "Chưa điền đủ thông tin",
                okText: "OK",
                template: '',
                ok: function () {
                },
            })
        }
        
    };

    $scope.ReturnAdd = function () {
        if ($scope.NoReturn) {
            $rootScope.PopupAlert({
                title: "Không có hàng trả về",
                okText: "OK",
                template: '',
                ok: function () {
                },
            })
        }
        else {
            $scope.GOPItem.OrderGroupID = 0;
            $scope.GOPItem.GroupProductID = 0;
            $scope.GOPItem.ProductID = 0;
            $scope.GOPItem.Quantity = '';
            $scope.ShowReturnDetail = true;
        }
    };

    $scope.GOPReturnCancel = function () {
        $scope.ShowReturnDetail = false;
        $scope.SOAddressCombobox.Clear();
        $scope.GOPCombobox.Clear();
        $scope.ProductCombobox.Clear();
    }

    $scope.GOPReturnEdit = function (item) {
        $scope.ReturnQuantity = item.Quantity;
        $rootScope.PopupConfirmInput({
            title: 'Điều chỉnh số lượng',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<input class="cus-textbox" type="number" ng-model="ReturnQuantity" />',
            ok: function (scope) {
                var num = scope.ReturnQuantity,
                    id = item.ID;
                if (num > 0) {
                    $ionicLoading.show();
                    dataService.Mobile_GOPReturnEdit(id,num).then(function (res) {
                        $ionicLoading.hide();
                        $scope.LoadGOPReturn();
                    })                 
                }
            }
        });
    }

    //#endregion

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