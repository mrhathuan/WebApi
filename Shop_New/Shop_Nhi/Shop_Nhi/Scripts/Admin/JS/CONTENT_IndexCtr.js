/// <reference path="../../angular.min.js" />

'use strict'

app.controller('CONTENT_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị nội dung website';
    $scope.Themes = ["/Content/theme-color/orange-theme.css",
        "/Content/theme-color/bridge-theme.css",

    ];
   
    $scope.Modal_THEME = function ($event) {
        $event.preventDefault();
        $scope.showModal_Theme = true;
    }

}]);