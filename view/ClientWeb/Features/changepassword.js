/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _UserProfile_Pass = {
    URL: {
        Get: 'UserProfile_GetUser',
        Save: 'UserProfile_SavePass',
    }
}

myapp.controller('changepasswordController', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('changepasswordController');
    $rootScope.IsLoading = false;

    $scope.Save_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.Item.Password == '') {
                vform({ model: 'Item.Password', error: 'Chưa nhập mật khẩu' });
            } else if ($scope.Item.Password != '' && $scope.Item.NewPassword != $scope.Item.ReNewPassword) {
                vform({ model: 'Item.NewPassword', error: 'Mật khẩu mới khác mật khẩu nhập lại' });
            }
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _UserProfile_Pass.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.Item.Password = '';
                        $scope.Item.NewPassword = '';
                        $scope.Item.ReNewPassword = '';
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }
    $scope.Return_Data = function ($event) {
        $event.preventDefault();
        $scope.Item.Password = "";
        $scope.Item.NewPassword = "";
        $scope.Item.ReNewPassword = "";
    }
    $scope.Close_Click = function ($event) {
        $event.preventDefault();
        $state.go("main");
    }
    $scope.Return_View = function($event)
    {
        $event.preventDefault();
        $state.go("profile");
    }
    Common.Services.Call($http, {
        url: Common.Services.url.SYS,
        method: _UserProfile_Pass.URL.Get,
        data: {},
        success: function (res) {
            $scope.Item = res;
        }
    });

});