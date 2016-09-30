var app = angular.module('MyApp', ['ngRoute']);
app.config([
    '$locationProvider', '$routeProvider',
    function ($locationProvider, $routeProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        }).hashPrefix('!');

        $routeProvider
        .when('/', { // For Home Page
            templateUrl: '/AngularTemplates/Home.html',
            controller: 'HomeController'
         })
        .when('/Home/Contact', { // For Contact page
            templateUrl: '/AngularTemplates/Contact.html',
            controller: 'ContactController'
        })
        .when('/Home/About', { // For About page
            templateUrl: '/AngularTemplates/About.html',
            controller: 'AboutController'
        })
        .when('/Home/User/:userid', { // For User page 
           templateUrl: '/AngularTemplates/User.html',
           controller: 'UserController'
        })
        .otherwise({   // This is when any route not matched => error
           controller: 'ErrorController'
        })
    }]);




