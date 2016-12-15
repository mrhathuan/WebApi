
angular.module('myapp').controller('driver_truckController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, $interval, $cordovaLocalNotification,dataService) {
    console.log('driver_truckController');
    $scope.$root.showMenuIcon = true;
    $scope.MainData = [];
    $scope.TimeSheet = [];
    $scope.lstCheck = [];
    $scope.reasonID = 0;
    $scope.ShowMap = 1;
    $scope.IsReceiveFull = false;
    $scope.selectedTab = 1;
    $scope.Lat = null;
    $scope.Lng = null;
    $scope.StatusObj = {
        Close: 221,
        Plan: 222,
        Come: 223,
        LoadStart: 224,
        LoadEnd: 225,
        Leave: 226
    };
     
    $rootScope.IsCheckRouteProblem = true;

    $rootScope.GetCurrentPosition(function (position) {
        $scope.Lat = position.coords.latitude;
        $scope.Lng = position.coords.longitude;
        
    });
    $scope.tabRunTitle = "Đã nhận";

    $scope.interval = null;
    $scope.BackGroundInterval = null;
    if (Common.RootObj.selectedTab == 2) {
        $scope.selectedTab = 2;
    }

    $rootScope.DriverID = 0;
    $rootScope.DirverItem = Common.Auth.Item;
    $rootScope.Host = Common.Services.url.Host;

    $scope.CheckAllLoad = function (stt, callback) {
        try {
            $scope.lstCheck[stt] = true;
            var rs = true;
            for (var i = 0; i < $scope.lstCheck.length; i++) {
                if ($scope.lstCheck[i] == false) {
                    rs = false;
                    break;
                }
            }
            if (rs) {
                callback();
            }
        }
        catch (e) {
            $rootScope.PopupAlert({
                title: 'ERROR!',
                template: e
            })
        }
    }

    Date.prototype.addDays = function (days) {
        this.setDate(this.getDate() + parseInt(days));
        return this;
    };

    Date.prototype.addHours = function (h) {
        this.setTime(this.getTime() + (h * 60 * 60 * 1000));
        return this;
    }

    $scope.LoadData = function () {
        $ionicLoading.show({ template: '...' });
        $scope.lstCheck = [false, false, false, false, false, false];

        dataService.FLMMobileScheduleOpen_List().then(function (openTimeSheet) {           
			$scope.OpenTimeSheet = openTimeSheet;
			$scope.CheckAllLoad(0, function () { $scope.LoadEnd() });
		})
				
		dataService.FLMMobileScheduleAcceptList().then(function(acceptTimeSheet){
			 $scope.AcceptTimeSheet =acceptTimeSheet;
			 $scope.CheckAllLoad(1, function () { $scope.LoadEnd() });
		})
		
		dataService.FLMMobileRejectList().then(function(rejectTimeSheet){
			  $scope.RejectTimeSheet = rejectTimeSheet;
              $scope.CheckAllLoad(2, function () { $scope.LoadEnd() });
		})
	
        dataService.FLMMobileScheduleGetList().then(function(getTimeSheet){
			  $scope.GetTimeSheet = getTimeSheet;  
			  $scope.CheckAllLoad(3, function () { $scope.LoadEnd() });
		})
		
        dataService.FLMMobileReasonList().then(function(reasonData){
			  $scope.ReasonData = reasonData;
              $scope.CheckAllLoad(4, function () { $scope.LoadEnd() });
		})
		
        dataService.FLMMobileRomoocList().then(function(romoocList){
			   $scope.RomoocList = romoocList;
                //$scope.CheckAllLoad(4, function () { $scope.LoadEnd() });
		})
		
		dataService.FLMMobileScheduleRunningList().then(function(res){
			 $scope.RunningData = res;            
                if (res.length > 0) {                    
                    $scope.IsReceiveFull = true;
                    angular.forEach(res[0].lstLocationFrom, function (o, i) {
                        if (o.IsLeave == false)
                            $scope.IsReceiveFull = false;
                    })
                }
                $scope.CheckAllLoad(5, function () { $scope.LoadEnd() });
		})
		
    }
    $scope.LoadData();    
    $scope.LoadEnd = function () {
        $ionicLoading.hide();
        $scope.$broadcast('scroll.refreshComplete');

        if ($scope.RunningData.length == 0) {
            $scope.IsRunning = false;
            $scope.tabRunTitle = "Đã nhận";
            if ($scope.AcceptTimeSheet.length == 0) {
                $scope.ViewAccepted = 1;// empty
            }
            else if ($scope.AcceptTimeSheet.length == 1) {
                $scope.ViewAccepted = 2;// master detail
                $scope.AcceptedItem = $scope.AcceptTimeSheet[0];
        
            }
            else {
                $scope.ViewAccepted = 3; // list accepted
            }
            $rootScope.ListGeofence = [];
            $rootScope.IsRecordVehicle = false;
            $rootScope.VehicleID = 1;
        }
        else {
            $scope.ViewAccepted = 2;
            $scope.AcceptedItem = $scope.RunningData[0];                                    
            $scope.IsRunning = true;
            $scope.tabRunTitle = "Đang chạy";
            $rootScope.VehicleID = $scope.AcceptedItem.VehicleID;
            $rootScope.vehicleCode = $scope.AcceptedItem.GPSCode;            
            $rootScope.IsRecordVehicle = true;            
            console.log($scope.AcceptedItem);            
            if ($scope.AcceptedItem.IsContainer) {
                $scope.LoadListNextStation = function () {
                    var masterID = $scope.AcceptedItem.TOMasterID;
                    dataService.FLMMobileDriver_COStationlist(masterID).then(function (res) {
                        var lst = []
                        angular.forEach(res, function (o, i) {
                            lst.push({
                                Lat: o.Lat,
                                Lng: o.Lng,
                                Radius: 0.05,
                                IsEnter: false,
                                Item: o,
                                EnterParams: { masterID: $scope.AcceptedItem.TOMasterID, o: o },
                                Enter: $scope.COStationPass,
                            })
                        })
                        $rootScope.IndexOfList.COStation = $rootScope.AddGeofence(lst, $rootScope.IndexOfList.COStation);
                    })                  
                }
                $scope.LoadListNextStation();

                $scope.ListLocationFrom = $scope.AcceptedItem.lstLocationFrom;                
                $scope.ListLocationTo = $scope.AcceptedItem.lstLocationTo;
                $scope.ListLocationGetRomooc = $scope.AcceptedItem.lstLocationGetRomooc;
                $scope.ListLocationReturnRomooc = $scope.AcceptedItem.lstLocationReturnRomooc;

                //geofence diem lay hang
                if ($scope.IsReceiveFull == false) {
                    var lst = []
                    angular.forEach($scope.AcceptedItem.lstLocationFrom, function (o, i) {
                        if (o.Status == 0)
                            lst.push({
                                Lat: o.Lat,
                                Lng: o.Lng,
                                Radius: 0.1,
                                IsEnter: false,
                                Item: o,
                                EnterParams: { timesheetID: $scope.AcceptedItem.TimeSheetID, romoocID: null, masterID: $scope.AcceptedItem.TOMasterID, o: o },
                                Enter: $scope.COLocationAutoComplete,
                            })
                    })
                    $rootScope.IndexOfList.LocationFrom = $rootScope.AddGeofence(lst, $rootScope.IndexOfList.LocationFrom);
                }
                //geofence diem giao hang
                else {
                    var lst = []
                    angular.forEach($scope.AcceptedItem.lstLocationTo, function (o, i) {
                        if (o.Status == 0)
                            lst.push({
                                Lat: o.Lat,
                                Lng: o.Lng,
                                Radius: 0.1,
                                IsEnter: false,
                                Item: o,
                                EnterParams: { timesheetID: $scope.AcceptedItem.TimeSheetID, romoocID: null, masterID: $scope.AcceptedItem.TOMasterID, o: o },
                                Enter: $scope.COLocationAutoComplete,
                            })
                    })
                    $rootScope.IndexOfList.LocationTo = $rootScope.AddGeofence(lst, $rootScope.IndexOfList.LocationTo);
                }
                //geofence lay romooc
                var lst1 = []
                angular.forEach($scope.AcceptedItem.lstLocationGetRomooc, function (o, i) {
                    if (o.Status == 0)
                        lst1.push({
                            Lat: o.Lat,
                            Lng: o.Lng,
                            Radius: 0.1,
                            IsEnter: false,
                            Item: o,
                            EnterParams: { timesheetID: $scope.AcceptedItem.TimeSheetID, romoocID: $scope.AcceptedItem.RomoocID, masterID: $scope.AcceptedItem.TOMasterID, o: o },
                            Enter: $scope.COLocationAutoComplete,
                        })
                })
                $rootScope.IndexOfList.LocationGetRomooc = $rootScope.AddGeofence(lst1, $rootScope.IndexOfList.LocationGetRomooc);
                //geofence tra romooc
                var lst2 = []
                angular.forEach($scope.AcceptedItem.lstLocationReturnRomooc, function (o, i) {
                    if (o.Status == 0)
                        lst2.push({
                            Lat: o.Lat,
                            Lng: o.Lng,
                            Radius: 0.1,
                            IsEnter: false,
                            Item: o,
                            EnterParams: { timesheetID: $scope.AcceptedItem.TimeSheetID, romoocID: 0, masterID: $scope.AcceptedItem.TOMasterID, o: o },
                            Enter: $scope.COLocationAutoComplete,
                        })
                })
                $rootScope.IndexOfList.LocationReturnRomooc = $rootScope.AddGeofence(lst2, $rootScope.IndexOfList.LocationReturnRomooc);

            }
            else {

                $scope.LoadListNextStation = function () {
                    var masterID = $scope.AcceptedItem.TOMasterID;
                    dataService.FLMMobileDriver_Stationlist(masterID).then(function (res) {
                        var lst = []
                        angular.forEach(res, function (o, i) {
                            lst.push({
                                Lat: o.Lat,
                                Lng: o.Lng,
                                Radius: 0.05,
                                IsEnter: false,
                                Item: o,
                                EnterParams: { masterID: $scope.AcceptedItem.TOMasterID, o: o },
                                Enter: $scope.DIStationPass,
                            })
                        })
                        $rootScope.IndexOfList.Station = $rootScope.AddGeofence(lst, $rootScope.IndexOfList.Station);
                    })                  
                }
                $scope.LoadListNextStation();

                //geofence diem lay hang
                if ($scope.IsReceiveFull == false) {
                    var lst = []
                    angular.forEach($scope.AcceptedItem.lstLocationFrom, function (o, i) {
                        if (o.Status == 0)
                            lst.push({
                                Lat: o.Lat,
                                Lng: o.Lng,
                                Radius: 0.1,
                                IsEnter: false,
                                Item: o,
                                EnterParams: { timesheetID: $scope.AcceptedItem.TimeSheetID, timedriverID: $scope.AcceptedItem.TimeSheetDriverID, masterID: $scope.AcceptedItem.TOMasterID, o: o },
                                Enter: $scope.DILocationAutoComplete,
                            })
                    })
                    $rootScope.IndexOfList.LocationFrom = $rootScope.AddGeofence(lst, $rootScope.IndexOfList.LocationFrom);
                }
                //geofence diem giao hang
                else {
                    var lst = []
                    angular.forEach($scope.AcceptedItem.lstLocationTo, function (o, i) {
                        if (o.Status == 0)
                            lst.push({
                                Lat: o.Lat,
                                Lng: o.Lng,
                                Radius: 0.1,
                                IsEnter: false,
                                Item: o,
                                EnterParams: { timesheetID: $scope.AcceptedItem.TimeSheetID, timedriverID: $scope.AcceptedItem.TimeSheetDriverID, masterID: $scope.AcceptedItem.TOMasterID, o: o },
                                Enter: $scope.DILocationAutoComplete,
                            })
                    })
                    $rootScope.IndexOfList.LocationTo = $rootScope.AddGeofence(lst, $rootScope.IndexOfList.LocationTo);
                }
            }
        }
    }

    $scope.DIStationPass = function (param) {
        var masterID = param.masterID,
            stationID = param.o.LocationID;
        if ($rootScope.IsCallServer == false) {
            $rootScope.IsCallServer = true;
            dataService.FLMMobileDriver_StationPass(masterID, stationID).then(function (res) {
                $rootScope.IsCallServer = false;
                if (res < 0) {
                    $rootScope.RemoveGeofence($rootScope.IndexOfList.Station);
                    $scope.LoadListNextStation();
                    var isBackgroundMode = false;
                    try {
                        isBackgroundMode = cordova.plugins.backgroundMode.isActive();
                    } catch (e) {
                        isBackgroundMode = false;
                    }
                    if (isBackgroundMode) {
                        cordova.plugins.notification.local.schedule({
                            id: 10,
                            title: "STM GPS",
                            text: 'Vừa qua trạm ' + param.o.LocationName,
                        });
                    }
                    else {
                        $rootScope.PopupAlert({
                            title: 'GPS',
                            template: 'Vừa qua trạm ' + param.o.LocationName,
                        });
                    }
                }
            })           
        }
    };

    $scope.DILocationAutoComplete = function (param) {
        var timesheetID = param.timesheetID,
            timedriverID = param.timedriverID,
            masterID = param.masterID,
            locationID = param.o.LocationID;
        if ($rootScope.IsCallServer == false) {
            $rootScope.IsCallServer = true;
            dataService.FLMMobileStatus_Save(timesheetID, timedriverID, masterID, locationID,$rootScope.CurrentTemperature, $scope.Lat, $scope.Lng).then(function (res) {
                $rootScope.IsCallServer = false;
                var isBackgroundMode = false;
                try {
                    isBackgroundMode = cordova.plugins.backgroundMode.isActive();
                } catch (e) {
                    isBackgroundMode = false;
                }
                if (isBackgroundMode) {
                    cordova.plugins.notification.local.schedule({
                        id: 10,
                        title: "STM GPS",
                        text: 'Đã đến ' + param.o.LocationName + '(' + param.o.LocationAddress + ')',
                    });
                }
                else {
                    $rootScope.PopupAlert({
                        title: 'GPS',
                        template: 'Đã đến ' + param.o.LocationName + '(' + param.o.LocationAddress + ')',
                    });
                }
                $rootScope.RemoveGeofence($rootScope.IndexOfList.Station);
                $rootScope.RemoveGeofence($rootScope.IndexOfList.LocationFrom);
                $rootScope.RemoveGeofence($rootScope.IndexOfList.LocationTo);
                $scope.LoadData();                
            })
           
        }
    };

    $scope.COStationPass = function (param) {
        var masterID = param.masterID,
            stationID = param.o.LocationID;
        if ($rootScope.IsCallServer == false) {
            $rootScope.IsCallServer = true;
            dataService.FLMMobileDriver_COStationPass(masterID, stationID).then(function (res) {
                $rootScope.IsCallServer = false;
                if (res < 0) {
                    $rootScope.RemoveGeofence($rootScope.IndexOfList.COStation);
                    $scope.LoadListNextStation();
                    var isBackgroundMode = false;
                    try {
                        isBackgroundMode = cordova.plugins.backgroundMode.isActive();
                    } catch (e) {
                        isBackgroundMode = false;
                    }
                    if (isBackgroundMode) {
                        cordova.plugins.notification.local.schedule({
                            id: 10,
                            title: "STM GPS",
                            text: 'Vừa qua trạm ' + param.o.LocationName,
                        });
                    }
                    else {
                        $rootScope.PopupAlert({
                            title: 'GPS',
                            template: 'Vừa qua trạm ' + param.o.LocationName,
                        });
                    }

                }
            })
         
        }
    };

    $scope.COLocationAutoComplete = function (param) {
        var timesheetID = param.timesheetID,
            masterID = param.masterID,
            locationID = param.o.LocationID,
            romoocID = param.romoocID;
        if ($rootScope.IsCallServer == false) {
            $rootScope.IsCallServer = true;
            dataService.FLMMobileStatus_COSave(timesheetID,masterID,locationID,romoocID,$scope.Lat, $scope.Lng).then(function (res) {
                $rootScope.IsCallServer = false;
                var isBackgroundMode = false;
                try {
                    isBackgroundMode = cordova.plugins.backgroundMode.isActive();
                } catch (e) {
                    isBackgroundMode = false;
                }
                if (isBackgroundMode) {
                    cordova.plugins.notification.local.schedule({
                        id: 10,
                        title: "STM GPS",
                        text: 'Đã đến ' + param.o.LocationName + '(' + param.o.LocationAddress + ')',
                    });
                }
                else {
                    $rootScope.PopupAlert({
                        title: 'GPS',
                        template: 'Đã đến ' + param.o.LocationName + '(' + param.o.LocationAddress + ')',
                    });
                }

                $rootScope.RemoveGeofence($rootScope.IndexOfList.COStation);
                $rootScope.RemoveGeofence($rootScope.IndexOfList.LocationFrom);
                $rootScope.RemoveGeofence($rootScope.IndexOfList.LocationTo);
                $scope.LoadData();
            })
          
        }
    };
    
    $scope.RunMaster = function (id, item) {
        $scope.temp = item.TempMin;
        var str = "Xác nhận bắt đầu chạy chuyến này";
        if ($scope.temp !=null ) {
            str = 'Nhập nhiệt độ xe';
        }
        if (!$scope.IsRunning)
            $rootScope.PopupConfirmInput({
                title: str,
                okText: 'Đồng ý',
                cancelText: 'Quay lại',
                scope: $scope,
                template: '<div style="margin-top:5px" ng-show="temp!=null"><input type="number" class="input-temp" placeholder="Nhập nhiệt độ..." type="text" ng-model="temp"> ',
                ok: function (scope) {
                    $rootScope.CurrentTemperature = scope.temp;

                    $ionicLoading.show();
                    dataService.FLMMobileMaster_Run(id).then(function (res) {
                        if (res == "") {
                            $scope.LoadData();
                        }
                        else {
                            $ionicLoading.hide();
                            $rootScope.PopupAlert({
                                title: 'Lỗi',
                                template: res,
                            });
                        }
                    }).catch(function (error) {
                        $scope.LoadData();
                    });
                    
                }
            });
            //$rootScope.PopupConfirm({
            //    title: 'Bạn chắc chắn muốn chạy chuyến này?',
            //    okText: 'Chấp nhận',
            //    cancelText: 'Từ chối',
            //    ok: function () {
            //        $ionicLoading.show();
            //        Common.Services.Call($http, {
            //            url: Common.Services.url.MOBI,
            //            method: "FLMMobileMaster_Run",
            //            data: { timeID: id },
            //            success: function (res) {
            //                if (res == "") {
            //                    $scope.LoadData();
            //                }
            //                else {
            //                    $ionicLoading.hide();
            //                    $rootScope.PopupAlert({
            //                        title: 'Lỗi',
            //                        template: res,
            //                    });
            //                }
            //            },
            //            error: function (e) {
            //                $scope.LoadData();
            //            }
            //        })
            //    }
            //});
    }

    $scope.RejectMaster = function (timedriverID, timesheetID) {
        $rootScope.PopupConfirmInput({
            title: 'Chọn lý do từ chối',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            scope: $scope,
            template: '<div style="margin-top:5px">Lý do từ chối</div><select style="width:100%;margin-bottom:8px;" ng-model="reasonID" ng-init="reasonID = ReasonData[0]==null? 0 :ReasonData[0]" ng-options="option.ReasonName for option in ReasonData"></select> <br/>Ghi chú<input type="text" ng-model="reasonNote"> ',
            ok: function (scope) {
                var reasonID = scope.reasonID.ID,
                    reasonNote = scope.reasonNote;
                var rsid = scope.reasonID.ID;
                if (rsid > 0) {
                    $ionicLoading.show();
                    dataService.FLMMobileMaster_Reject(timesheetID, timedriverID, reasonID, reasonNote).then(function (res) {
                        $scope.LoadData();
                    })                   
                }
                else {
                    $rootScope.PopupAlert({
                        title: 'Lỗi',
                        template: "Chưa chọn lý do từ chối",
                    });
                }
            }
        });

    }

    $scope.AcceptMaster = function (timedriverID, timesheetID) {
        $rootScope.PopupConfirm({
            title: 'Bạn chắc chắn muốn chạy chuyến này?',
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                dataService.FLMMobileMaster_Accept(timesheetID,timedriverID).then(function (res) {
                    $scope.LoadData();
                }).catch(function (error) {
                    $scope.LoadData();
                })
            }
        });

    }

    $scope.ReAcceptMaster = function (timesheetID) {
        dataService.FLMMobileMaster_ReAccept(timesheetID).then(function (res) {
            $scope.LoadData();
        })      
    }

$scope.RunMaster_End = function(){
     $rootScope.PopupConfirm({
            title: 'Bạn chắc chắn muốn kết thúc chuyến này?',
            okText: 'Đồng ý',
            cancelText: 'Quay lại',
            ok: function () {
                $ionicLoading.show();
                dataService.FLMMobileStatus_Complete($scope.AcceptedItem.TOMasterID).then(function (res) {
                    $ionicLoading.hide();                    
                    $scope.LoadData();
                })               
            }
        });
    }

    $scope.LocationComplete = function (timedriverID, timesheetID, masterID, locationID, status, locatioName) {
        //var statusType = $scope.TranslateStatus(statusID);       
        if (locatioName == null)
            locatioName = '';
        var strTitle = $scope.GetTitleStatus(status) +" "+ locatioName;                      
        if (status == 0) {
            $scope.temp = $rootScope.CurrentTemperature;
            $rootScope.PopupConfirmInput({
                title: 'Thay đổi nhiệt độ hiện tại của xe',
                okText: 'Đồng ý',
                cancelText: 'Quay lại',
                scope: $scope,
                template: '<div style="margin-top:5px"><input type="number" class="input-temp" placeholder="Nhập nhiệt độ..." type="text" ng-model="temp"> ',
                ok: function (scope) {
                    $rootScope.CurrentTemperature = scope.temp;
                    var temp = $rootScope.CurrentTemperature;
                    $ionicLoading.show();
                    dataService.FLMMobileStatus_Save(timesheetID, timedriverID, masterID, locationID,temp, $scope.Lat, $scope.Lng).then(function (res) {
                        $scope.LoadData();
                    })                 
                }
            });
        }
        else
            $rootScope.PopupConfirm({
            title: strTitle,
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                var temp = $rootScope.CurrentTemperature;
                dataService.FLMMobileStatus_Save(timesheetID, timedriverID, masterID, locationID, temp, $scope.Lat, $scope.Lng).then(function (res) {
                    $scope.LoadData();
                })              
            },
            cancel: function () {
            }
        });
    }

    $scope.COLocationComplete = function (timesheetID, masterID, locationID, statusID, FromOrTo,romoocID) {
        var str = "";
        switch (FromOrTo) {
            case 'from':
                if (statusID < 1) {
                    str = "Xác nhận đến nơi nhận hàng"
                }
                else {
                    str = "Xác nhận rời nơi nhận hàng";
                }
                break;
            case 'to':
                if (statusID < 1) {
                    str = "Xác nhận đến nơi giao hàng";
                }
                else {
                    str = "Xác nhận rời nơi giao hàng"
                }
                break;
            case 'getRomooc':
                if (statusID < 1) {
                    str = "Xác nhận đến lấy mooc"
                }
                else {
                    str = "Xác nhận rời nơi lấy mooc";
                }
                break;
            case 'returnRomooc':
                if (statusID < 1) {
                    str = "Xác nhận đến trả mooc"
                }
                else {
                    str = "Xác nhận rời nơi trả mooc";
                }
                break;
        }
        $rootScope.PopupConfirm({
            title: str,
            okText: 'Chấp nhận',
            cancelText: 'Từ chối',
            ok: function () {
                $ionicLoading.show();
                dataService.FLMMobileStatus_COSave(timesheetID, masterID, locationID, romoocID,$scope.Lat, $scope.Lng).then(function (res) {
                    $scope.LoadData();
                })               
            },
            cancel: function () {
                $interval.cancel($scope.interval);
            }
        });
    }

    $scope.ShowSOList = function (masterID, locationID, statusID, timedriverID, timesheetID) {
        if ($scope.IsRunning == true)
            $state.go('driver.truck_detail', { masterID: masterID, locationID: locationID, statusID: statusID, sheetDriverID: timedriverID, sheetID: timesheetID })
    }

    $scope.ShowCOList = function (masterID) {
        if ($scope.IsRunning == true)
            $state.go('driver.container_detail', { masterID: masterID})
    }

    $scope.AddTrouble = function (type) {
        $state.go('driver.truck_trouble', { masterID: $scope.AcceptedItem.TOMasterID, type: type })
    }

    $scope.ViewStation = function (type) {
        $state.go('driver.truck_station', { masterID: $scope.AcceptedItem.TOMasterID, type: type });
    }

    $scope.CloseMap = function () {
        $scope.ShowMap = 1;
    }

    $scope.ShowMapRoute = function (viewtype) {
        $state.go('map', { p0: viewtype, p1: $scope.AcceptedItem.TOMasterID })

    }

    // $scope.TranslateStatus = function (DITOStatusID) {
    //     if (DITOStatusID < $scope.StatusObj.Come) {
    //         return 1;
    //     }
    //     else if (DITOStatusID == $scope.StatusObj.Come) {
    //         return 2;
    //     }
    //     else if (DITOStatusID == $scope.StatusObj.LoadStart) {
    //         return 3;
    //     }
    //     else if (DITOStatusID == $scope.StatusObj.LoadEnd) {
    //         return 4;
    //     }
    //     else {
    //         return 5;
    //     }
    // }

    $scope.GetTitleStatus = function (status) {
        if (status == 0) {
            return 'Xác nhận đã đến điểm ';
        }
        else if (status == 1) {
            return 'Xác nhận bắt đầu bốc xếp hàng hóa';
        }
        else if (status == 2) {
            return 'Xác nhận hoàn tất bốc xếp';
        }
        else if (status == 3) {
            return 'Xác nhận rời khỏi điểm này';
        }
        else {
            return "";
        }
    }

    $scope.TempClick = function () {
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

});