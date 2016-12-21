<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ClientWeb._Report" %>

<!DOCTYPE html> 

<html ng-app="myapp" ng-controller="defaultController" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HỆ THỐNG QUẢN LÝ VẬN CHUYỂN</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link rel="stylesheet" href="Content/fonts.4.6/font.css" type="text/css" />

    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.common.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.dataviz.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.default.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.dataviz.default.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/Report.css" type="text/css" />
    <link rel="stylesheet" href="Scripts/ReportViewer/styles/telerikReportViewer-9.0.15.225.css" type="text/css" />

    <script type="text/javascript" src="Scripts/jquery-1.12.4.min.js"></script>
    <script type="text/javascript" src="Scripts/angular.min.js"></script>
    <script type="text/javascript" src="Scripts/angular-ui-router.min.js"></script>    
    <script type="text/javascript" src="Scripts/jquery-ui-1.11.4.min.js"></script>    
    <script type="text/javascript" src="Scripts/kendo/2016.3.914/kendo.all.min.js"></script>
    <script type="text/javascript" src="Scripts/kendo/2016.3.914/kendo.aspnetmvc.min.js"></script>
    <script type="text/javascript" src="Scripts/kendo/2016.3.914/jszip.min.js"></script>
    <script type="text/javascript" src="Scripts/ReportViewer/js/telerikReportViewer-9.0.15.225.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>

    <script type="text/javascript">
        angular.module('myapp', ['ui.router', 'kendo.directives']);
    </script>    

    <script type="text/javascript" src="Scripts/Report.js"></script>
    <script type="text/javascript" src="Reports/ScheduleDriverFee.js"></script>
    <script type="text/javascript" src="Reports/WorkOrderFuel.js"></script>
    <script type="text/javascript" src="Reports/OPSSotrans.js"></script>
</head>
<body>
    <div class="content" ui-view="view"></div>     
</body>
</html>
