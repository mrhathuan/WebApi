/// <reference path="../angular.min.js" />
var app = angular.module('myapp', ['ngRoute', 'kendo.directives']);

var URL = {
    MAIN: {
        DASH_Index: '/Tk/Main/DASH_Index'
    }
};

app.config(function ($routeProvider) {
   
    $routeProvider.when("/DASH_Index", {
        templateUrl: URL.MAIN.DASH_Index,
        controller: 'DASH_IndexCtr'
    });
    
    $routeProvider.otherwise({ redirectTo: "/DASH_Index" });
});

app.directive('scrolldiv', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            $timeout(function () {
                //$element.perfectScrollbar({
                //    minScrollbarLength: 50
                //});
            }, 1)
        },
        restrict: 'A'
    };
    return directive;
}]);

app.controller('indexController',
[
    '$scope', '$rootScope', function ($scope, $rootScope) {
        $rootScope.title = 'Quản trị';
        $scope.Default_ShowProfile = false;
        $scope.MenuItems = [
            {
                Name: 'Tổng quan', Childs: [{
                    NameChild: 'Dashboard',
                    IsActive: true,
                    Link: '#',
                    Icon: 'fa fa-tachometer'
                }]
            },
            {
                Name: 'Vận hành', Childs: [{
                    NameChild: 'Sản phẩm',
                    IsActive: false,
                    Link: 'zuryshop.net',
                    Icon: 'fa fa-tachometer'
                },
                {
                    NameChild: 'Danh mục',
                    IsActive: false,
                    Link: '#',
                    Icon: 'fa fa-tachometer'
                },
                {
                    NameChild: 'Đơn hàng',
                    IsActive: false,
                    Link: '#',
                    Icon: 'fa fa-tachometer'
                }
                ]
            }
        ];

        $scope.ShowProfileClick = function ($event) {
            $event.preventDefault();
            $scope.Default_ShowProfile = !$scope.Default_ShowProfile;
        }

        $scope.ItemMenu_Click = function ($event, item) {
            $event.preventDefault();
            angular.forEach($scope.MenuItems, function (parent, i) {
                angular.forEach(parent.Childs, function (child, j) {
                    if (child.IsActive == true)
                        child.IsActive = false;
                });
            });
            item.IsActive = true;
            location.href = item.Link;
        }
    }
]);