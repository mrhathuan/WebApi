
angular.module('myapp').controller('driver_truckDetailController', function ($rootScope, $window, $scope, $state, $stateParams, $location, $http, $timeout, $ionicLoading, $ionicModal, $ionicPopup, dataService) {
    console.log('driver_truckDetailController');

    $scope.masterID = $stateParams.masterID;
    $scope.locationID = $stateParams.locationID;
    $scope.sheetDriverID = $stateParams.sheetDriverID;
    $scope.sheetID = $stateParams.sheetID;
    $scope.statusID = 1
    $scope.selectedTab = 1
    $scope.ShowReturnDetail = false;
    $scope.GOPItem = {};
    $scope.Lat = null;
    $scope.Lng = null;
    $scope.ShowBtnAdd = true;
    $scope.SOAddressCombobox = {};
    $scope.GOPCombobox = {};
    $scope.ProductCombobox = {};
    $scope.SOGOPCombobox = {};
    $scope.SOProductCombobox = {};
    $scope.item = null;
    $scope.showBntQtyTranfer = false;
    $scope.SOListProduct = [];
    $scope.SOListGOP = [];    
    $scope.GroupProductItem = {};

    $scope.BtnAdd = function () {
        $scope.ShowBtnAdd = !$scope.ShowBtnAdd;
    }

    $scope.StatusObj = {
        Close: 221,
        Plan: 222,
        Come: 223,
        LoadStart: 224,
        LoadEnd: 225,
        Leave:226
    }

     $rootScope.GetCurrentPosition(function (position) {
        $scope.Lat = position.coords.latitude;
        $scope.Lng = position.coords.longitude;
        
    });

    if ($stateParams.statusID == 0) {
        $scope.statusID = 1;
    }
    else if ($stateParams.statusID == 1) {
        $scope.statusID = 2;
    }
    else if ($stateParams.statusID == 2) {
        $scope.statusID = 3;
    }
    else if ($stateParams.statusID == 3) {
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
        dataService.FLMMobile_SOList($scope.masterID, $scope.locationID).then(function (res) {
            $ionicLoading.hide();
            $scope.SOList = res;
            angular.forEach($scope.SOList, function (o, i) {
                o.lstFile = [];
                var id = o.TOGroupProductID,
                    code = "dipod";
                dataService.FLMMobileDriver_FileList(id, code).then(function (res) {
                    o.lstFile = res;
                })
          
            })
        })     
    }
    $scope.LoadSO();


    $scope.changeQtyTranfer = function () {
        $scope.showBntQtyTranfer = true;
        
    }

    $scope.cancelQtyTranferClick = function (QuantityTranfer) {

    }

    $scope.SOQtyTranferEdit = function (item) {
        $scope.QuantityTranfer = item.QuantityTranfer;
        $rootScope.PopupConfirmInput({
            title: 'Điều chỉnh số lượng giao',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<input class="cus-textbox" type="number" ng-model="QuantityTranfer" />',
            ok: function (scope) {
                item.QuantityTranfer = scope.QuantityTranfer;
                if (scope.QuantityTranfer > 0) {
                    $ionicLoading.show();
                    dataService.Mobile_SL_Save(item).then(function (res) {
                        $ionicLoading.hide();
                        $scope.LoadSO();
                    })
                }
            }
        });
    }

    $scope.SONoteReturnEdit = function (item) {
        $scope.Note = item.Note;
        $rootScope.PopupConfirmInput({
            title: 'Thêm ghi chú',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<textarea style="border:1px solid rgba(0,0,0,.15)" ng-model="Note" rows="6" cols="10"></textarea>',
            ok: function (scope) {
                item.Note = scope.Note;
                $ionicLoading.show();
                dataService.Mobile_SL_Save(item).then(function (res) {
                    $ionicLoading.hide();
                    $scope.LoadSO();
                })
            }
        });
    }
    
    $scope.SOQtyReturnEdit = function (item) {
        $scope.QuantityReturn = item.QuantityReturn;
        $rootScope.PopupConfirmInput({
            title: 'Điều chỉnh số lượng trả về',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<input class="cus-textbox" type="number" ng-model="QuantityReturn" />',
            ok: function (scope) {
                item.QuantityReturn = scope.QuantityReturn;
                if (scope.QuantityReturn > 0) {
                    $ionicLoading.show();
                    dataService.Mobile_SL_Save(item).then(function (res) {
                        $ionicLoading.hide();
                        $scope.LoadSO();
                    })
                }
            }
        });
    }


    $scope.AddNewSO_Click = function (item) {      
        var TOGroupID = item.TOGroupProductID;
        var lst = [];
        var check=[];
        dataService.Mobile_GroupProductOfTOGroup(TOGroupID).then(function (res) {
            $scope.ListGOP = res;
            angular.forEach(res, function (o, i) {
                if (!Common.HasValue(check[o.GroupProductID])) {
                    lst.push(o);
                    check[o.GroupProductID] = true;                    
                }                
            })            
            $scope.SOListGOP = lst;
            $scope.SOListProduct = {};
            $rootScope.PopupConfirmInput({
                title: 'Thêm mới hàng hóa',
                okText: 'Đồng ý',
                cancelText: 'Quay lại',
                scope: $scope,
                templateUrl: 'addNewSO.html',
                ok: function (scope) {                    
                    if (Common.HasValue(scope.GroupProductItem) && scope.GroupProductItem.GroupProductID > 0 && scope.GroupProductItem.ID > 0 && scope.GroupProductItem.Quantity > 0) {                        
                        dataService.Mobile_AddGroupProductFromDN(TOGroupID, scope.GroupProductItem).then(function (res) {
                            $rootScope.PopupAlert({
                                title: 'Thông báo',
                                template: 'Lưu thành công',
                                ok: function () {
                                    $scope.LoadSO();                                   
                                }
                            })
                        })
                    } else {
                        $rootScope.PopupAlert({
                            title: "Chưa điền đủ thông tin",
                            okText: "OK",
                            template: '',
                            ok: function () {
                                $scope.AddNewSO_Click(item);
                                $scope.SOProductCombobox.Clear();
                            }
                        })
                    }
                }, cancel: function () {
                    $scope.SOProductCombobox.Clear();
                    $scope.GroupProductItem = {}
                }
            })
        });  
    }

    $scope.SOReloadProduct = function () {
        var lst = [];
        $scope.SOProductCombobox.Clear();
        angular.forEach($scope.ListGOP, function (o, i) {
            if (o.GroupProductID == $scope.GroupProductItem.GroupProductID) {
                lst.push(o);            
            }
        })
        $scope.SOListProduct = lst;
    }

    //$scope.cancelQtyTranferClick = function () {
    //    $scope.showBntQtyTranfer = !$scope.showBntQtyTranfer;        
    //}

    $scope.Reject = function () {
        var timeSheetDriverID = $scope.timeSheetDriverID;
        dataService.Reject(timeSheetDriverID).then(function (res) {
            $state.go('main.truck');
        })       
    }

    $scope.LocationComplete = function () {
        var str = $scope.GetTitleStatus();
        $scope.temp = $rootScope.CurrentTemperature;
        if ($scope.statusID == 1 && $scope.temp == null) {
            str = 'Yêu cầu nhập nhiệt độ hiện tại của xe.'
            $rootScope.PopupConfirmInput({
                title: str,
                okText: 'Đồng ý',
                cancelText: 'Quay lại',
                scope: $scope,
                template: '<div style="margin-top:5px"><input type="number" class="input-temp" placeholder="Nhập nhiệt độ..." type="text" ng-model="temp"> ',
                ok: function (scope) {
                    $rootScope.CurrentTemperature = scope.temp;
                    $ionicLoading.show();
                    dataService.FLMMobileStatus_Save($scope.sheetID, $scope.sheetDriverID, $scope.masterID, $scope.locationID, $scope.temp, $scope.Lat, $scope.Lng).then(function (res) {
                        $scope.statusID++;
                        if ($scope.statusID > 4) {
                            $state.go('driver.truck');
                        }
                        $ionicLoading.hide();
                    })
                }
            });
        } else {
            $rootScope.PopupConfirmInput({
                title: str,
                okText: 'Đồng ý',
                cancelText: 'Quay lại',
                scope: $scope,
                ok: function (scope) {
                    $ionicLoading.show();
                    dataService.FLMMobileStatus_Save($scope.sheetID, $scope.sheetDriverID, $scope.masterID, $scope.locationID, $scope.temp, $scope.Lat, $scope.Lng).then(function (res) {
                        $scope.statusID++;
                        if ($scope.statusID > 4) {
                            $state.go('driver.truck');
                        }
                        $ionicLoading.hide();
                    })
                }
            });
        }
    }   

    $scope.GetTitleStatus = function () {
        if ($scope.statusID == 1) {
            return 'Xác nhận đã đến điểm ';
        }
        else if ($scope.statusID == 2) {
            return 'Xác nhận bắt đầu bốc xếp hàng hóa';
        }
        else if ($scope.statusID == 3) {
            return 'Xác nhận hoàn tất bốc xếp';
        }
        else if ($scope.statusID == 4) {
            return 'Xác nhận rời khỏi điểm này';
        }
        else {
            return "";
        }
    }


    $scope.SwicthTab = function (i) {
        $scope.selectedTab = i;
    }

    // nav bar
    $scope.BackToTruck = function () {
        $state.go($rootScope.PreviousState, $rootScope.PreviousParam);
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
    $scope.LoadGOPReturn();

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
                rs.ReferID = $scope.uploadItem.TOGroupProductID;
                rs.TypeOfFileCode = 'DIPOD';

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

     $scope.imageModal_Click = function(item){
        $scope.showModal('selectImage.html');
        $scope.uploadItem = item;
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
        $scope.temp = $rootScope.CurrentTemperature;
        $rootScope.PopupConfirmInput({
            title: 'Thay đổi nhiệt độ hiện tại của xe',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<div style="margin-top:5px"><input type="number" class="input-temp" placeholder="Nhập nhiệt độ..." type="text" ng-model="temp"> ',
            ok: function (scope) {
                $rootScope.CurrentTemperature = scope.temp;
            }
        });
    }

    // Close the modal
    $scope.closeModal = function () {
        $scope.modal.hide();
        $scope.modal.remove();
    };

    $scope.openModal = function () {
        $scope.modal.show();
    };
    $scope.$on('$destroy', function () {
        $scope.modal.remove();
    });


    $scope.$on('modal.hidden', function () {

    });


    $scope.$on('modal.removed', function () {

    });

});