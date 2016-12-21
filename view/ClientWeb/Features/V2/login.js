/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _login = {
    URL: {
        Login: 'App_Login',
        ListResource: 'App_ListResource',
    }
};
//#endregion

angular.module('myapp').controller('loginController', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('loginController');
    $rootScope.IsLoading = false;

    $scope.login_Username = '';
    $scope.login_Password = '';

    if (Common.HasValue(Common.Auth.Item) && Common.Auth.Item.UserID > 0) {
        Common.Cookie.Set('main_Tabs', []);
        $state.go('main');
    }

    $scope.eventLogin = function ($event) {
        $event.preventDefault();
                
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _login.URL.Login,
            data: { username: this.login_Username, password: this.login_Password, devicetype: 1 },
            success: function (res) {
                if (Common.HasValue(res) && res.UserID > 0) {
                    Common.Auth.Item = res;
                    Common.Auth.SetHeaderKey(res.HeaderKey);

                    $rootScope.Default_IsLogin = true;
                    $rootScope.Default_UserName = Common.Auth.Item.UserName;
                    $rootScope.Default_DisplayName = Common.Auth.Item.LastName + ' ' + Common.Auth.Item.FirstName;
                    $rootScope.Default_Address = Common.Auth.Item.Address;
                    $rootScope.Default_Tel = Common.Auth.Item.Tel;
                    $rootScope.Default_Fax = Common.Auth.Item.Fax;

                    RS = {};
                    $rootScope.RS = {};
                    Common.Services.Call($http, {
                        url: Common.Services.url.SYS,
                        method: _login.URL.ListResource,
                        data: {},
                        success: function (res) {
                            $rootScope.IsLoading = false;

                            angular.forEach(res, function (v, i) {
                                var table = v.Key.substr(0, v.Key.indexOf('.'));
                                var col = v.Key.substr(v.Key.indexOf('.') + 1);
                                var current = RS[table];
                                if (!Common.HasValue(current))
                                    RS[table] = {};
                                RS[table][col] = v.Name;
                            });
                            $rootScope.RS = RS;

                            $state.go('main');
                        }
                    });
                }
                else
                    $rootScope.Message({ Msg: res.StringError });
            }
        });
    }
}]);