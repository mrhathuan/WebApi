<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClientWeb.Default" %>

<!DOCTYPE html>

<html ng-app="myapp" ng-controller="defaultController" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HỆ THỐNG QUẢN LÝ VẬN CHUYỂN</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link rel="stylesheet" href="Content/fonts.4.6/font.css" type="text/css" />

    <script src="http://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyDhl3J58jgXqJClW9MaYcS5KUF7LJRf9ns&sensor=true&language=vi&signed_in=false&libraries=places,geometry" type="text/javascript"></script>

    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.common.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.dataviz.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.default.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/kendo/2016.3.914/kendo.dataviz.default.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/perfect-scrollbar.css" type="text/css" />
    <link rel="stylesheet" href="Content/Style.css" type="text/css" />
    <link rel="stylesheet" href="Content/ol.css" type="text/css" />

    <script type="text/javascript" src="Scripts/jquery-1.12.4.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.connectingLine.js"></script>
    <script type="text/javascript" src="Scripts/jquery.signalR-2.2.0.min.js"></script>
    <script type="text/javascript" src="Scripts/perfect-scrollbar.jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/angular.min.js"></script>
    <script type="text/javascript" src="Scripts/angular-ui-router.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.11.4.min.js"></script>
    <script type="text/javascript" src="Scripts/kendo/2016.3.914/kendo.all.min.js"></script>
    <script type="text/javascript" src="Scripts/kendo/2016.3.914/kendo.aspnetmvc.min.js"></script>
    <script type="text/javascript" src="Scripts/kendo/2016.3.914/jszip.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cookie.js"></script>
    <script type="text/javascript" src="Scripts/ol3/ol.js"></script>
    <script type="text/javascript" src="Scripts/ol3/ol3gm.js"></script>
    <%=System.Web.Optimization.Scripts.Render("~/Scripts/common") %>
    <script type="text/javascript">
        Common.Services.host = 'localhost';
        angular.module('myapp', ['ui.router', 'kendo.directives']).value('signalRServer', 'signalr/hubs');
    </script>
    <%=System.Web.Optimization.Scripts.Render("~/Scripts/main") %>
    <%=System.Web.Optimization.Scripts.Render("~/Scripts/dev") %>
</head>
<body>
    <div class="page-top" ng-click="HideProfileClick()" ng-show="Default_IsLogin">
        <div class="header left">
            <div class="logo"><a ui-sref="main"><img alt="" src="Images/Icon_LogoV2.png" /></a></div>
            <div ng-show="Default_IsLogin" class="buttonmenu"><a ng-click="MenuClick($event)" href="/"><i class="fa fa-bars"></i></a></div>
            <a href="/" ng-show="QuickAddItem.Show" ng-click="QuickAddItem.Call($event)" class="common-action">
                <i class="fa fa-plus"></i>
            </a>
            <div class="time" ng-show="ServerTime.IsConnected && false"><span class="large">{{ServerTime.Hour}}<i>:</i>{{ServerTime.Minute}}</span> <span class="medium">[<i>{{ServerTime.Day}}</i>.<i>{{ServerTime.Month}}</i>]</span></div>
            <div class="clear"></div>
        </div>
        <div class="header right">
            <div class="info" ng-show="Default_IsLogin">
                <ul>
                    <li class="li-search">
                        <div class="search-all">
                            <div class="cbo-search">
                                <div class="btn-group">
                                    <button ng-click="Search_BTN_Click($event)" type="button" class="btn btn-default dropdown-toggle">
                                        {{Search.Text}} <i class="fa fa-caret-down" aria-hidden="true"></i>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li ng-click="Search_Item_Click($event, 1)">Đơn hàng</li>
                                        <li ng-click="Search_Item_Click($event, 2)">Mã chuyến</li>
                                        <li ng-click="Search_Item_Click($event, 3)">Số xe</li>
                                        <li ng-click="Search_Item_Click($event, 4)">Tài xế</li>
                                        <li ng-click="Search_Item_Click($event, 5)">Địa chỉ giao</li>
                                        <li ng-click="Search_Item_Click($event, 6)">Chứng từ</li>
                                    </ul>
                                </div>
                            </div>
                            <div class="form-search">
                                <form ng-submit="Search_Click($event,ResultSearch_view)">
                                    <input type="text" ng-model="Search.Content" class="k-textbox" placeholder="Tìm kiếm" />
                                </form>
                                <div ng-click="Search_Click($event,ResultSearch_view)" class="icon-search"><i class="fa fa-search"></i></div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <a href="/" ng-click="UserMessage_Click($event)">
                            <i class="fa fa-bell-o"></i>
                            <span class="number bg-red" ng-show="UserMessageTotal > 0">{{UserMessageTotal > 99 ? "99+" : UserMessageTotal}}</span>
                        </a>
                        <ul ng-show="Default_ShowUserMessage" style="width: 370px; text-align: left;" click-anywhere-but-message="UserMessage_CheckClose($event)">
                            <li>
                                <div class="msg-container no-border-bottom">
                                     <div ng-show="UserMessage_IsLoadMore" class="loading-msg">
                                          <i class="fa fa-spinner fa-spin"></i>
                                     </div>
                                    <div expand-k-tabstrip kendo-tabstrip="userMessage_Tabstrip" k-options="userMessage_Tabstrip_Options" class="cus-tabstrip">
                                        <ul>
                                            <li data-tabindex="0" class="k-state-active">Tất cả</li>
                                            <li data-tabindex="1">Đơn hàng</li>
                                            <li data-tabindex="2">Kế hoạch</li>
                                            <li data-tabindex="3">Vận chuyển</li>
                                            <li data-tabindex="4">Chứng từ</li>
                                        </ul>
                                        <div class="max-height-400 lazyload">
                                            <div ng-repeat="msg in UserMessage.All.Data" class="msg-item">
                                                <div class="msg-icon"></div>
                                                <div class="msg-html" ng-bind-html="HTML(msg.Message)"></div>
                                                <div class="msg-createdate" ng-bind-html="HTML(msg.CreatedDate)"></div>
                                                <div class="clear"></div>
                                            </div>
                                        </div>
                                        <div class="max-height-400 lazyload">
                                            <div ng-repeat="msg in UserMessage.ORD.Data" class="msg-item">
                                                <div class="msg-icon ord-icon"></div>
                                                <div class="msg-html" ng-bind-html="HTML(msg.Message)"></div>
                                                 <div class="msg-createdate" ng-bind-html="HTML(msg.CreatedDate)"></div>
                                                <div class="clear"></div>
                                            </div>
                                        </div>
                                        <div class="max-height-400 lazyload">
                                            <div ng-repeat="msg in UserMessage.OPS.Data" class="msg-item">
                                                <div class="msg-icon pod-icon"></div>
                                                <div class="msg-html" ng-bind-html="HTML(msg.Message)"></div>
                                                 <div class="msg-createdate" ng-bind-html="HTML(msg.CreatedDate)"></div>
                                                <div class="clear"></div>
                                            </div>
                                        </div>
                                        <div class="max-height-400 lazyload">
                                            <div ng-repeat="msg in UserMessage.MON.Data" class="msg-item">
                                                <div class="msg-icon"></div>
                                                <div class="msg-html" ng-bind-html="HTML(msg.Message)"></div>
                                                 <div class="msg-createdate" ng-bind-html="HTML(msg.CreatedDate)"></div>
                                                <div class="clear"></div>
                                            </div>
                                        </div>
                                        <div class="max-height-400 lazyload">
                                            <div ng-repeat="msg in UserMessage.POD.Data" class="msg-item">
                                                <div class="msg-icon"></div>
                                                <div class="msg-html" ng-bind-html="HTML(msg.Message)"></div>
                                                 <div class="msg-createdate" ng-bind-html="HTML(msg.CreatedDate)"></div>
                                                <div class="clear"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="msg-container no-border-top">
                                    <a href="/" ng-click="UserMessage_ViewAll_Click($event, UserMessageViewAll_win)" class="text-center">Xem tất cả</a>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="/" ng-click="ShowProfileClick($event)">
                            <span>{{Default_UserName}}</span>
                            <img src="Images/ico_avatar.png" alt="{{Default_DisplayName}}" />
                        </a>
                        <ul ng-show="Default_ShowProfile">
                            <li>
                                <a ui-sref="profile">
                                    <span>Thông tin tài khoản</span>
                                    <i class="fa fa-user"></i>
                                </a>
                            </li>
                            <li>
                                <a href="/" ng-click="Logout($event)">
                                    <span>Đăng xuất</span>
                                    <i class="fa fa-sign-out"></i>
                                </a>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="clear"></div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div class="page-body" ng-click="HideProfileClick()" ng-class="Default_IsLogin ? '' : 'page-login'">
        <div class="content" ui-view="view"></div>
    </div>
    <div ng-show="IsLoading" class="loading">
        <i class="fa fa-spinner fa-spin"></i>
    </div>
    <div class="cus-window" kendo-window="winloading" id="winloading" k-title="false" k-width="300" k-height="40" k-visible="false" k-resizable="false" k-modal="true">
        <div class="winloading">
            <div class="status">{{Loading.Status}}</div>
            <div kendo-progress-bar="progressBar" k-show-status="false" k-min="0" k-max="100" ng-model="Loading.Progress" style="width: 100%;"></div>
        </div>
    </div>

    <!--Message-->
    <span id="page-notification" kendo-notification="msg" k-options="msgOptions"></span>

    <!--Window Upload-->
    <div class="cus-window" draggable-k-window kendo-window="winfile" id="winfile" k-title="false" k-width="600" k-height="400" k-visible="false" k-resizable="false" k-modal="true">
        <div class="cus-form">
            <div class="form-header">
                <div class="left title">
                    File
                </div>
                <div class="clear"></div>
            </div>
            <div class="form-body with-footer" ng-show="!winfile_IsImage">
                <div class="cus-grid" kendo-grid="winfile_grid" k-options="winfile_gridOptions" expand-k-grid></div>
            </div>
            <div class="form-body with-footer" ng-show="winfile_IsImage">
                <div class="cus-listview winfile_view" kendo-list-view="winfile_view" k-options="winfile_viewOptions" k-data-source="winfile_viewDataSource" scrolldiv></div>
            </div>
            <div class="form-footer">
                <div>
                    <div style="display: none;">
                        <input name="files" type="file" kendo-upload="winfile_file" k-options="winfile_fileOptions" /></div>
                    <a href="/" ng-show="winfile_AllowChange" ng-click="winfile_Upload_Click($event,winfile_file)" class="k-button">Chọn file</a>
                    <a href="/" ng-show="winfile_ShowChoose" ng-click="winfile_Save_Click($event,winfile_grid,winfile_view)" class="k-button accept">Đồng ý</a>
                    <a href="/" ng-show="winfile_AllowChange && winfile_gridHasChoose" ng-click="winfile_Del_Click($event,winfile_grid,winfile_view)" class="k-button">Xóa</a>
                    <a href="/" ng-click="winfile_Close_Click($event,winfile)" class="k-button close">Đóng</a>
                </div>
            </div>
        </div>
    </div>

    <!--Window Excel-->
    <div class="cus-window" draggable-k-window kendo-window="winexcel" k-title="false" k-width="600" k-height="400" k-visible="false" k-resizable="false" k-modal="true">
        <div class="cus-form">
            <div class="form-header">
                <div class="left title">
                    Excel
                </div>
                <div class="clear"></div>
            </div>
            <div class="form-body with-footer">
                <div class="cus-grid" kendo-grid="winexcel_grid" k-options="winexcel_gridOptions" k-rebind="winexcel_gridOptions" expand-k-grid></div>
            </div>
            <div class="form-footer">
                <div>
                    <div style="display: none;">
                        <input name="files" type="file" kendo-upload="winexcel_file" accept=".xls, .xlsx" k-options="winexcel_fileOptions" /></div>
                    <a href="/" ng-click="winexcel_Download_Click($event)" class="k-button">Tải tệp</a>
                    <a href="/" ng-click="winexcel_Upload_Click($event,winexcel_file)" class="k-button">Chọn tệp</a>
                    <a href="/" ng-click="winexcel_Save_Click($event,winexcel)" class="k-button accept">Đồng ý</a>
                    <a href="/" ng-click="winexcel_Close_Click($event,winexcel)" class="k-button close">Đóng</a>
                </div>
            </div>
        </div>
    </div>

    <!--Window Excel Share-->
    <div class="cus-window" draggable-k-window kendo-window="winexcelshare" k-title="false" k-width="900" k-height="500" k-visible="false" k-resizable="false" k-modal="true">
        <div class="cus-form">
            <div class="form-header">
                <div class="left title">
                    {{excelShare.data.captionWin}}
                </div>
                <div class="clear"></div>
            </div>
            <div class="form-body with-footer">
                <div id="spreadsheet" style="width: 99%;height:99%" ></div>
            </div>
            <div class="form-footer">
                <div>
                    <a href="/" ng-click="winexcelshare_Reload_Click($event,winexcelshare)" class="k-button accept">Làm mới</a>
                    <a href="/" ng-click="winexcelshare_Approve_Click($event,winexcelshare)" ng-class="excelShare.data.rowRunning > 0 ? 'k-button accept excelshare-running' : 'k-button accept excelshare'"><i class="fa fa-spinner fa-spin"></i> {{excelShare.data.captionAccept}}</a>
                    <a href="/" ng-click="winexcelshare_Close_Click($event,winexcelshare)" class="k-button close">Đóng</a>
                </div>
            </div>
        </div>
    </div>

    <!--Window Message-->
    <div class="cus-window" draggable-k-window kendo-window="winmessage" k-title="false" k-width="500" k-height="150" k-visible="false" k-resizable="false" k-modal="true" k-on-close="winmessage_Win_Close()">
        <div class="cus-form">
            <div class="form-header">
                <div class="left title">
                    {{winmessage_title}}
                </div>
                <div class="clear"></div>
            </div>
            <div class="form-body with-footer">
                <div class="winmessage_msg"><span>{{winmessage_msg}}</span></div>
            </div>
            <div class="form-footer">
                <div class="winmessage_button">
                    <a href="/" ng-keydown="winmessage_Keydown($event)" ng-show="!winmessage_IsAlert" ng-click="winmessage_Save_Click($event,winmessage)" class="accept k-button">Đồng ý</a>
                    <a href="/" ng-keydown="winmessage_Keydown($event)" ng-click="winmessage_Close_Click($event,winmessage)" class="close k-button k-button-icontext">Đóng</a>
                </div>
            </div>
        </div>
    </div>

    <div class="cus-window" draggable-k-window kendo-window="wincustom" id="wincustom" allowexit="true" k-title="false" k-width="400" k-height="120" k-visible="false" k-resizable="false" k-modal="true">
        <div class="view-custom">
            <div class="item" ng-show="SettingGrid.hasFilterRow">
                <a href="/">
                    <div class="icon">
                        <div title="Ẩn/Hiện filter row" class="icon-icon" ng-class="SettingGrid.hasFilterRowEnable?'isEnable':''" style="background-image: url(/Images/function/ico_setting_active.png)" ng-click="SettingGrid_FilterRow_Click($event)"></div>
                    </div>
                </a>
            </div>
            <div class="item" ng-show="SettingGrid.hasSettingView">
                <a href="/">
                    <div class="icon">
                        <div title="Thiết lập hiển thị cột" class="icon-icon" style="background-image: url(/Images/function/ico_lich_active.png)" ng-click="SettingGrid_View_Click($event,wincustom_view,wincustom_view_grid)"></div>
                    </div>
                </a>
            </div>
            <div class="item" ng-show="SettingGrid.hasFilterDefault">
                <a href="/">
                    <div class="icon">
                        <div title="Thiết lập bộ lọc mặc định" class="icon-icon" style="background-image: url(/Images/function/ico_setting2_active.png)" ng-click="SettingGrid_FilterDefault_Click($event)"></div>
                    </div>
                </a>
            </div>
        </div>
    </div>

    <!--Window CustomViewGrid-->
    <div class="cus-window" draggable-k-window kendo-window="wincustom_view" id="wincustom_view" k-title="false" k-width="800" k-height="400" k-visible="false" k-resizable="true" k-modal="true">
        <div class="cus-form">
            <div class="form-header">
                <div class="left title">
                    THIẾT LẬP HIỂN THỊ CỘT
                </div>
                <div class="clear"></div>
            </div>
            <div class="form-body with-footer">
                <div class="cus-grid" kendo-grid="wincustom_view_grid" k-options="wincustom_view_gridOptions" expand-k-grid></div>
            </div>
            <div class="form-footer">
                <div>
                    <a href="/" ng-click="wincustom_view_Default_Click($event,wincustom_view)" class="k-button">Xóa</a>
                    <a href="/" ng-click="wincustom_view_Update_Click($event,wincustom_view,wincustom_view_grid)" class="k-button accept">Cập nhật</a>
                    <a href="/" ng-click="wincustom_view_Cancel_Click($event,wincustom_view)" class="k-button close">Đóng</a>
                </div>
            </div>
        </div>
    </div>

    <!--Window Result Search-->
    <div class="cus-window" draggable-k-window kendo-window="ResultSearch_view" k-title="false" k-width="900" k-height="500" k-visible="false" k-resizable="true" k-modal="true">
        <div class="cus-form">
            <div class="form-header">
                <div class="left title">
                    Kết quả tìm kiếm
                </div>
                <div class="right">
                    <a href="/" ng-show="!IsFullScreen" ng-click="Search_Zoom_Click($event)" class="k-button zoom-config"><i class="fa fa-television zoom-config"></i><span class="tooltip is-right">Phóng to</span></a>
                    <a href="/" class="k-button" ng-show="IsFullScreen" ng-click="Search_Minimize_Click($event)"><i class="fa fa-minus config"></i><span class="tooltip is-right">Thu nhỏ</span></a>
                </div>
                <div class="clear"></div>
            </div>
            <div class="form-body with-footer">
                <div class="cus-tabstrip" expand-k-tabstrip kendo-tab-strip k-content-urls="[null, null]" k-options="Search_WinTabOptions">
                    <ul>
                        <li data-tabindex="1" class="k-state-active">Vận chuyển phân phối
                        </li>
                        <li data-tabindex="1">Vận chuyển container
                        </li>
                    </ul>
                    <div>
                        <div class="cus-grid" expand-k-grid kendo-grid="Search_TruckGrid" k-options="Search_TruckGrid_Options"></div>
                    </div>
                    <div>
                        <div class="cus-grid" expand-k-grid kendo-grid="Search_ContainerGrid" k-options="Search_ContainerGrid_Options"></div>
                    </div>
                </div>
            </div>
            <div class="form-footer">
                <div>
                    <a href="/" ng-click="wincustom_view_Cancel_Click($event,ResultSearch_view)" class="k-button close">Đóng</a>
                </div>
            </div>
        </div>
    </div>
    <!--Window UserMessage ViewAll-->
    <div class="cus-window" draggable-k-window kendo-window="UserMessageViewAll_win" k-title="false" k-width="1000" k-height="500" k-visible="false" k-resizable="false" k-modal="true">
        <div class="cus-form">
            <div class="form-header">
                <div class="left title">Danh sách thông báo</div>
                 <div class="right">
                    <a href="/" ng-show="!IsFullScreen" ng-click="UserMessageViewAll_Zoom_Click($event)" class="k-button zoom-config"><i class="fa fa-television zoom-config" ></i><span class="tooltip is-right">Phóng to</span></a>
                    <a href="/" class="k-button" ng-show="IsFullScreen" ng-click="UserMessageViewAll_Minimize_Click($event)"><i class="fa fa-minus config"></i><span class="tooltip is-right">Thu nhỏ</span></a>
                 </div>
                <div class="clear"></div>
            </div>
            <div class="form-body with-footer">
                <div class="cus-grid" expand-k-grid kendo-grid="userMessage_ViewAll_grid" expand-k-grid k-options="userMessage_ViewAll_gridOptions"></div>
            </div>
            <div class="form-footer">
                <div>
                    <a href="/" ng-click="UserMessageViewAllClose_Click($event,UserMessageViewAll_win)" class="k-button close" data-title="Đóng">Đóng</a>
                </div>
            </div>
        </div>
    </div>

    <!--Download-->
    <iframe ng-src="{{fileDownload}}" style='position: fixed; display: none; top: -1px; left: -1px;' />
</body>

</html>
