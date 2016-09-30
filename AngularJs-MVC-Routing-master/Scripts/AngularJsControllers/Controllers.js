var app = angular.module('MyApp');

app.controller('UserController', ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.Message = "This is User  Page with query string id value = " + $routeParams.userid;
}]);

app.controller('HomeController', function ($scope) {
    $scope.Message = "This is HOME page";
})
app.controller('AboutController', function ($scope) {
    $scope.Message = "This is ABOUT page";
})

app.controller('UserController1', ['$scope', '$location', function ($scope, $location) {
    var paras = $location.search().userid;
    $scope.Message = "This is User information Page of user has id" + $location.path();
}])

app.controller('UserController2', ['$scope', '$routeParams', function ($scope, $routeParams) {
    // $routeParams used for get query string value
    var loc = window.location.href;
    var id = loc.slice(loc.lastIndexOf('/'), loc.length).split('/')[1];
    $scope.Message = "This is ORDER Page with query string id value =" + id + $routeParams.userid;;
}])


app.controller('ContactController', function ($scope) {
    // $routeParams used for get query string value
    $scope.Message = "This is Contact Page  ";
})
app.controller('ErrorController', function ($scope) {
    $scope.Message = "404 Not Found!";
});