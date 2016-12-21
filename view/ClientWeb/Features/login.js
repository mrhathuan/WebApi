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
    $scope.login_RememberMe = false;
    $scope.login_Message = "";

    if (Common.HasValue(Common.Auth.Item) && Common.Auth.Item.UserID > 0) {
        Common.Cookie.Set('main_Tabs', []);
        $state.go('main');
    }

    $rootScope.IsPageComplete = true;
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
                    
                    $rootScope.FunctionItem = { IDMain: -1, ID: -1 };
                    Common.Cookie.Set('main_ItemMenu', JSON.stringify([{ IDMain: -1, ID: -1, Code: '', CodeView: '', ListActions: [] }]));
                    $state.go('main');

                    //var RS = {};
                    //var RSS = {};
                    //$rootScope.RS = {};
                    //$rootScope.RSS = {};
                    //Common.Services.Call($http, {
                    //    url: Common.Services.url.SYS,
                    //    method: _login.URL.ListResource,
                    //    data: { dt: new Date() },
                    //    success: function (res) {
                    //        $rootScope.IsLoading = false;

                    //        angular.forEach(res, function (v, i) {
                    //            var table = v.Key.substr(0, v.Key.indexOf('.'));
                    //            var col = v.Key.substr(v.Key.indexOf('.') + 1);
                    //            var current = RS[table];
                    //            if (!Common.HasValue(current)) {
                    //                RS[table] = {};
                    //                RSS[table] = {};
                    //            }
                    //            RS[table][col] = v.Name;
                    //            RSS[table][col] = v.ShortName;
                    //        });
                    //        $rootScope.RS = RS;
                    //        $rootScope.RSS = RSS;

                            
                    //    }
                    //});

                    $rootScope.signalProxy.on('messagecall' + res.UserID, function (data) {
                        if (Common.HasValue($rootScope.UserMessage_Change))
                            $rootScope.UserMessage_Change(data.total);
                    });
                }
                else {
                    $scope.login_Message = res.StringError;
                    $rootScope.IsLoading = false;
                    //$rootScope.Message({ Msg: res.StringError });
                }
            }
        });
    }
}]);