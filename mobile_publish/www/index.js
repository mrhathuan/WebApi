angular.module('myapp').run(['$ionicPlatform', function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        if (window.cordova && window.cordova.plugins.Keyboard) {
            // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
            // for form inputs)
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

            // Don't remove this line unless you know what you are doing. It stops the viewport
            // from snapping when text inputs are focused. Ionic handles this internally for
            // a much nicer keyboard experience.
            cordova.plugins.Keyboard.disableScroll(true);
        }
        if (window.StatusBar) {
            StatusBar.styleDefault();
        }
    });
}]);

angular.module('myapp').config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    
}]);

//#region Data
var _index = {
    URL: {
        Login: 'App_Login',
        Connect: 'App_Connect',
    }
};
//#endregion

angular.module('myapp').controller('indexController', function ($rootScope, $scope, $http, $sce, $location, $ionicLoading, $state, $timeout, $interval, $window, $ionicPopup) {
	$scope.modelUsername = '';
    $scope.modelPassword = '';
    $rootScope.DriverItem = {};
    $rootScope.DriverID = 0;
    $rootScope.showMenuIcon = true;
    $scope.eventLogin = function ($event) {
        $event.preventDefault();
        
        var username = this.modelUsername.trim().toLowerCase();
        var password = this.modelPassword.trim().toLowerCase();
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
			
			$scope._tag = '';
			if (ix > 0) {
                var route = username.substr(0, ix);
				$scope._tag = route;
				$scope._username = username.substr(ix + 1);
                Common.Services.url.Host = 'http://' + route + '.' + Common.Services.url.domain;
                Common.Services.url.SYS = Common.Services.url.Host + Common.Services.url.SYS;
                Common.Services.url.MOBI = Common.Services.url.Host + Common.Services.url.MOBI;
            }
			else {
				Common.Services.url.Host = 'http://localhost:2743/';
                Common.Services.url.SYS = '/api/SYS/';
                Common.Services.url.MOBI = '/api/Mobile/';
			}
			
            $ionicLoading.show({ template: 'Loading...' });
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _index.URL.Connect,
                data: { dt: new Date() },
                success: function (res) {
										
                    Common.Services.Call($http, {
                        url: Common.Services.url.SYS,
                        method: _index.URL.Login,
                        data: { username: $scope._username, password: $scope._password, devicetype: 1 },
                        success: function (res) {
                            $ionicLoading.hide();
							
                            if (Common.HasValue(res) && res.UserID > 0) {
                                location.href = 'index_.html?d=' + $scope._tag + '?p=' + res.HeaderKey;
								//location.href = 'index_' + $scope._tag + '.html?p=' + res.HeaderKey;								
                            }
                            else {
                                $rootScope.PopupAlert({
                                    template: 'Sai tài khoản hoặc mật khẩu!',
                                    ok: function () { location.href = 'index.html' }
                                });
                            }
                        }
                    });

                },
                error: function (res) {					
                    $ionicLoading.hide();
                    $rootScope.PopupAlert({
                        template: 'Không kết nối được hệ thống',
                        ok: function () { location.href = 'index.html' }
                    });
                    
                }
            });
        }
        else {
            $scope.ErrorStr = "Thiếu tài khoản hoặc mật khẩu!";
        }
    }

    $rootScope.PopupConfirm = function (options) {
        var optOrg = {
            title: "",
            subTitle: "",
            okText: "OK",
            cancelText: "Cancel",
            template: '',
            ok: function () { },
            cancel: function () { },
        }
        var opt = {}; angular.extend(opt, optOrg, options);

        $ionicPopup.show({
            title: opt.title,
            subTitle: opt.subTitle,
            template: opt.template,
            scope: opt.scope,
            buttons: [
              {
                  text: opt.okText,
                  type: 'button-positive',
                  onTap: function (e) { return true; }
              },
              { text: opt.cancelText, onTap: function (e) { return false; } },
            ]
        }).then(function (res) {
            if (res) {
                opt.ok();
            }
            else {
                opt.cancel();
            }
        });
    };

    $rootScope.PopupConfirmInput = function (options) {
        var optOrg = {
            title: "",
            subTitle: "",
            okText: "OK",
            cancelText: "Cancel",
            template: '',
            scope: {},
            ok: function () { },
            cancel: function () { },
        }
        var opt = {}; angular.extend(opt, optOrg, options);

        $ionicPopup.show({
            title: opt.title,
            subTitle: opt.subTitle,
            template: opt.template,
            scope: opt.scope,
            buttons: [
              {
                  text: opt.okText,
                  type: 'button-positive',
                  onTap: function (e) { return { ok: true, scope: this }; }
              },
              { text: opt.cancelText, onTap: function (e) { return { ok: false, scope: this }; } },
            ]
        }).then(function (res) {
            if (res.ok) {
                opt.ok(res.scope.scope);
            }
            else {
                opt.cancel();
            }
        });
    };

    $rootScope.PopupAlert = function (options) {
        var optOrg = {
            title: "Thông báo",
            okText: "OK",
            template: '',
            ok: function () { },
        }
        var opt = {}; angular.extend(opt, optOrg, options);

        var alertPopup = $ionicPopup.alert({
            title: opt.title,
            template: opt.template,
        });

        alertPopup.then(function (res) {
            opt.ok();
        });
        return alertPopup
    };


});

