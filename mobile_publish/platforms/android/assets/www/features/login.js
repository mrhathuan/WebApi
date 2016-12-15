/// <reference path="~/m/js/common.js" />

//#region Data
var _login = {
    URL: {
        Login: 'App_Login',
        Connect: 'App_Connect',
    }
};
//#endregion

angular.module('myapp').controller('loginController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading, $ionicNavBarDelegate) {
    console.log('loginController');

	$timeout(function(){
		//$ionicNavBarDelegate.showBar(false);
		//$rootScope.ShowMenu = false;
		$ionicNavBarDelegate.showBar(false);
		//$('#nav-bar').hide();
	},1);
	
    $scope.modelUsername = 'taixe1';
    $scope.modelPassword = '123456789';
    $rootScope.DriverItem = {};
    $rootScope.DriverID = 0;
    $state.go('manage');
    //$state.go('vendor.home');
    $scope.eventLogin = function ($event) {
        $event.preventDefault();
        
        //$scope.login_Username = $scope.modelUsername.trim();
        //$scope.login_Password = $scope.modelPassword.trim();
		var username = this.modelUsername.trim();
		var password = this.modelPassword.trim();		
		$scope._username = username;
		$scope._password = password;
		
        if (username.length > 0 || password.length > 0) {
            var len = username.length;
            var ix = username.indexOf(' ');
            if (ix <= 0 || ix >= len - 1) {
                ix = username.indexOf('-');
                if (ix <= 0 || ix >= len - 1) {
                    ix = username.indexOf('_');
                    if (ix <= 0 || ix >= len - 1){
						ix = username.indexOf('.');
						if (ix <= 0 || ix >= len - 1)
							ix = -1;
					}
                }
            }
			
            if (ix > 0) {
                var route = username.substr(0, ix);
				$scope._username = username.substr(ix + 1);
                Common.Services.url.Host = 'http://' + route + '.' + Common.Services.url.domain;
                Common.Services.url.SYS = Common.Services.url.Host + Common.Services.url.SYS;
                Common.Services.url.MOBI = Common.Services.url.Host + Common.Services.url.MOBI;
            }
			
            $ionicLoading.show({ template: 'Loading...' });

            
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _login.URL.Connect,
                data: { dt: new Date() },
                success: function (res) {
										
                    Common.Services.Call($http, {
                        url: Common.Services.url.SYS,
                        method: _login.URL.Login,
                        data: { username: $scope._username, password: $scope._password, devicetype: 1 },
                        success: function (res) {
                            $ionicLoading.hide();
							
                            if (Common.HasValue(res) && res.UserID > 0) {
								$ionicNavBarDelegate.showBar(true);
								
                                Common.Auth.Item = res;
                                Common.Auth.HeaderKey = res.HeaderKey;
                                if (res.StringError == "") {
                                    $rootScope.DriverID = res.DriverID;
                                    $rootScope.DriverName = res.DriverName;
                                    $rootScope.CustomerID = res.CustomerID;
                                    $state.go('main');
                                } else {
                                    $scope.ErrorStr = res.StringError;
                                }

                            }
                            else {
                                $scope.ErrorStr = "Sai tài khoản hoặc mật khẩu!"
                            }
                        }
                    });

                },
                error: function (res) {					
                    $ionicLoading.hide();
					$rootScope.PopupAlert({template:'Không kết nối được hệ thống'});
                }
            });

            

        }
        else {
            $scope.ErrorStr = "Thiếu tài khoản hoặc mật khẩu!"
        }
    }
});