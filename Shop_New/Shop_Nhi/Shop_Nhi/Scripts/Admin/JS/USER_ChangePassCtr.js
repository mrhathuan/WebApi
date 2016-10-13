/// <reference path="../../angular.min.js" />

'use strict'

app.controller('USER_ChangePassCtr', ['$http', '$scope', '$rootScope', '$timeout', function ($http, $scope, $rootScope, $timeout) {
    $rootScope.title = 'Đổi mật khẩu';

    $scope.Return_Data = function ($event) {
        $event.preventDefault();
        $scope.Password = "";
        $scope.NewPassword = "";
        $scope.ReNewPassword = "";
    }

    $scope.Save_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.Password == '') {
                vform({ model: 'Password', error: 'Chưa nhập mật khẩu' });
            } else if ($scope.Password != '' && $scope.NewPassword != $scope.renewPassword) {
                vform({ model: 'NewPassword', error: 'Mật khẩu mới khác mật khẩu nhập lại' });
            }else{
                $rootScope.IsLoading = true;
                $http.post("/Pn/Users/ChangePassword", { password: $scope.Password, renewPassword: $scope.renewPassword}).then(function success(res) {
                    $rootScope.IsLoading = false;
                    if (res.data.status == true) {
                        $scope.Password = "";
                        $scope.NewPassword = "";
                        $scope.ReNewPassword = "";
                        toastr.success(res.data.msg, '');
                        $timeout(function () {
                            location.href = '/Pn/#/DASH_Index';
                        },1000)
                    } else {
                        toastr.error(res.data.msg, '');
                    }
                })
            }
        }
    }

}]);