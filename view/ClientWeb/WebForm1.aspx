<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ClientWeb.WebForm1" %>

<!DOCTYPE html>

<html ng-app="myapp" ng-controller="defaultController" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/fonts.4.6/font.css" type="text/css" />

    <link rel="stylesheet" href="Content/kendo/2016.1.112/kendo.common.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.1.112/kendo.dataviz.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.1.112/kendo.default.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.1.112/kendo.dataviz.default.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/perfect-scrollbar.css" type="text/css" />
    <link rel="stylesheet" href="Content/Style.css" type="text/css" />
    <link rel="stylesheet" href="Content/ol.css" type="text/css" />

    <script type="text/javascript" src="Scripts/jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.signalR-2.2.0.min.js"></script>
    <script type="text/javascript" src="Scripts/perfect-scrollbar.jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/angular.min.js"></script>
    <script type="text/javascript" src="Scripts/angular-ui-router.min.js"></script>    
    <script type="text/javascript" src="Scripts/jquery-ui-1.11.4.min.js"></script>    
    <script type="text/javascript" src="Scripts/kendo/2016.1.112/kendo.all.min.js"></script>
    <script type="text/javascript" src="Scripts/kendo/2016.1.112/kendo.aspnetmvc.min.js"></script>
    <script type="text/javascript" src="Scripts/kendo/2016.1.112/jszip.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cookie.js"></script>
    <script type="text/javascript" src="Scripts/ol3/ol.js"></script>
    <script type="text/javascript" src="Scripts/ol3/ol3gm.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>

    <script type="text/javascript">
        angular.module('myapp', ['ui.router', 'kendo.directives']).value('signalRServer', 'signalr/hubs');

        angular.module('myapp').controller('defaultController', ['$rootScope', '$scope', '$http', '$sce', '$location', '$state', '$timeout', '$window', '$compile', function ($rootScope, $scope, $http, $sce, $location, $state, $timeout, $window, $compile) {
            Common.Log('defaultController');

            
        }]);
    </script>
</head>
<body>
    <div ></div>
</body>
</html>
