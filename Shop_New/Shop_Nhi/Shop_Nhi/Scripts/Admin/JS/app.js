/// <reference path="../../angular.min.js" />
var app = angular.module('myapp', ['ngRoute', 'kendo.directives']);

app.config(function ($routeProvider) {

    $routeProvider.when("/DASH_Index", {
        templateUrl: '/Pn/Pn/DASH_Index',
        controller: 'DASH_IndexCtr'
    });
    $routeProvider.when("/PRO_Index", {
        templateUrl: '/Pn/Pn/PRO_Index',
        controller: 'PRO_IndexCtr'
    });    
    $routeProvider.otherwise({ redirectTo: "/DASH_Index" });
});

app.controller('indexController', ['$scope', function ($scope) {
   
}])
