/// <reference path="../../angular.min.js" />

'use strict'

app.controller('CONTENT_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị nội dung website';
   
    $scope.Modal_THEME = function ($event) {
        $event.preventDefault();
        $scope.showModal_Theme = true;
    }

    $scope.Change_THEME = function ($event) {
        $event.preventDefault();       
        var link = $event.currentTarget.getAttribute("data-link");
        var cf = confirm('Bạn muốn chọn màu này cho trang chủ?');
        if (cf) {
            $rootScope.IsLoading = true;
            $http.post("/Pn/CONTENT/THEME_Change", { link: link }).then(function success(res) {
                if (res.data.status == true) {
                    $rootScope.IsLoading = false;
                    $scope.showModal_Theme = false;
                    toastr.success('Thành công', '');
                }
            })
        }        
    }

}]);