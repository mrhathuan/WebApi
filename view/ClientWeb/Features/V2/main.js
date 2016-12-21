/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _main = {
    URL: {
        ListFunction: 'App_ListFunction',
        ChangeAction: 'App_ChangeAction',
    }
};
//#endregion

angular.module('myapp').controller('mainController', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'signalRHubProxy', function ($rootScope, $scope, $http, $location, $state, $timeout, signalRHubProxy) {
    Common.Log('mainController');
    $rootScope.IsLoading = false;

    $timeout(function () {
        $rootScope.ClientPushHubProxy.on('serverTime', function (data) {
            var dt = new Date(data);
            $rootScope.ServerTime.IsConnected = true;
            $rootScope.ServerTime.Month = (dt.getMonth() + 1) + '';
            $rootScope.ServerTime.Day = dt.getDate() + '';
            $rootScope.ServerTime.Hour = dt.getHours() + '';
            $rootScope.ServerTime.Minute = dt.getMinutes() + '';
        });
    }, 2);

    $scope.MenuClick = function ($event) {
        $event.preventDefault();

        $rootScope.IsShowMenu = !$rootScope.IsShowMenu;
        $rootScope.ResizeMain();
    };

    $scope.comment_text = '';
    $scope.comment_send = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.CommentSave,
            data: { item: { Comment: $scope.comment_text, ReferID: $rootScope.comment_item.ReferID, TypeOfCommentCode: $rootScope.comment_item.Type } },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.comment_text = '';
                    $rootScope.Comment_Show($rootScope.comment_item.ReferID);
                    $rootScope.Message({ Msg: 'Thành công' });
                }
            }
        });
    };

    $scope.comment_hide = function ($event) {
        $event.preventDefault();

        $rootScope.comment_item = null;
        $rootScope.hasComment = false;
        $rootScope.ResizeMain();
    };

    $rootScope.FunctionItem = { ID: -1, Code: '', CodeView: '' };
    $scope.ItemMenu_Click = function ($event, item) {
        Common.Cookie.Set('main_ItemMenu', JSON.stringify([{ ID: item.ID, Code: item.Code, CodeView: item.CodeView, ListActions: item.ListActions }]));

        if ($rootScope.FunctionItem.ID > 0)
            $rootScope.FunctionItem.IsActive = false;
        item.IsActive = true;
        $rootScope.FunctionItem = item;

        var acts = item.ListActions;
        if (!Common.HasValue(acts))
            acts = [];
        Common.Auth.Item.ListActionCode = acts;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _main.URL.ChangeAction,
            data: { ListActions: acts },
            success: function (res) { }
        });

        if (Common.HasValue($rootScope.CompleteLoading))
            $timeout(function () { $rootScope.CompleteLoading(); }, 1);
    };

    var str = Common.Cookie.Get('main_ItemMenu');
    if (Common.HasValue(str) && str != '')
        $rootScope.FunctionItem = eval(Common.Cookie.Get('main_ItemMenu'))[0];

    $scope.Functions = [];
    Common.Services.Call($http, {
        url: Common.Services.url.SYS,
        method: _main.URL.ListFunction,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                //var activeid = -1;

                result = [
                    {
                        ID: 1, Code: 'View', CodeView: '', FunctionName: 'Tổng quan', Icon: '',
                        Childs: [
                            { ID: 16, Code: 'main.PUBDashboard.Index', CodeView: '', FunctionName: 'Dashboard', Icon: 'fa-trello', IsActive: false, ListActions: [], ListSettings: [], Note: null }
                        ]
                    },
                    {
                        ID: 3, Code: 'Admin', CodeView: '', FunctionName: 'Quản lý', Icon: '',
                        Childs: [
                            { ID: 17, Code: '', CodeView: '', FunctionName: 'Đơn hàng', Icon: 'fa-clipboard', IsActive: true, ListActions: [], ListSettings: [], Note: '20' },
                            { ID: 124, Code: '', CodeView: '', FunctionName: 'Điều phối', Icon: 'fa-object-ungroup', IsActive: false, ListActions: [], ListSettings: [], Note: null },
                            { ID: 128, Code: '', CodeView: '', FunctionName: 'Giám sát', Icon: 'fa-mobile', IsActive: false, ListActions: [], ListSettings: [], Note: null },
                            { ID: 139, Code: '', CodeView: '', FunctionName: 'Chứng từ', Icon: 'fa-sitemap', IsActive: false, ListActions: [], ListSettings: [], Note: null },
                        ]
                    },
                ];

                //var lst = [];
                //var parentlvl1 = null;
                //var parentlvl2 = null;
                angular.forEach(res, function (v, i) {
                    v.IsActive = false;
                    v.Note = null;
                    v.Icon = '';

                    var settings = [];
                    v.CodeView = 'main.' + v.Code;
                    settings['[' + v.ID + ']'] = { ReferKey: 'main.' + v.Code + '.Index' };
                    if (v.ListSettings.length > 0) {
                        angular.forEach(v.ListSettings, function (vSet, j) {
                            if (vSet.ReferKey == '' || vSet.ReferKey == null)
                                settings['[' + v.ID + ']'] = vSet;
                            else
                                settings[vSet.ReferKey] = vSet;
                        });
                    }
                    v.ListSettings = settings;

                    if (v.ID == 17) {
                        v.Code = settings['[' + v.ID + ']'].ReferKey;

                        result[1].Childs[0] = v;
                        //result[1].Childs[0].CodeView = v.CodeView;
                        //result[1].Childs[0].ListActions = v.ListActions;
                        //result[1].Childs[0].ListSettings = v.ListSettings;
                    }

                    if (v.ID == 124) {
                        v.Code = settings['[' + v.ID + ']'].ReferKey;

                        result[1].Childs[1] = v;
                        //result[1].Childs[1].CodeView = v.CodeView;
                        //result[1].Childs[1].ListActions = v.ListActions;
                        //result[1].Childs[1].ListSettings = v.ListSettings;
                    }

                    if (v.ID == 128) {
                        v.Code = settings['[' + v.ID + ']'].ReferKey;

                        result[1].Childs[2] = v;
                        //result[1].Childs[2].CodeView = v.CodeView;
                        //result[1].Childs[2].ListActions = v.ListActions;
                        //result[1].Childs[2].ListSettings = v.ListSettings;
                    }

                    if (v.ID == 139) {
                        v.Code = settings['[' + v.ID + ']'].ReferKey;

                        result[1].Childs[3] = v;
                        //result[1].Childs[2].CodeView = v.CodeView;
                        //result[1].Childs[2].ListActions = v.ListActions;
                        //result[1].Childs[2].ListSettings = v.ListSettings;
                    }


                    if ($rootScope.FunctionItem.ID == v.ID)
                        $rootScope.FunctionItem = v;

                    //if (v.HasChild == true) {
                    //    if (parentlvl1 == null || parentlvl1.id != v.ParentID) {
                    //        parentlvl1 = { id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, expanded: false, items: [], lvl: 1 };
                    //        parentlvl2 = null;
                    //        lst.push(parentlvl1);
                    //    }
                    //    else {
                    //        parentlvl2 = { id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, expanded: false, items: [], lvl: 2 };
                    //        parentlvl1.items.push(parentlvl2);
                    //    }
                    //}
                    //else if (parentlvl2 != null) {
                    //    if (parentlvl2.id == v.ParentID)
                    //        parentlvl2.items.push({ id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, icon: v.Icon, lvl: 3 });
                    //    else if (parentlvl1 != null && parentlvl1.id == v.ParentID) {
                    //        parentlvl1.items.push({ id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, icon: v.Icon, lvl: 2 });
                    //        parentlvl2 = null;
                    //    }
                    //    else
                    //        lst.push({ id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, icon: v.Icon, lvl: 1 });
                    //}
                    //else if (parentlvl1 != null) {
                    //    if (parentlvl1.id == v.ParentID)
                    //        parentlvl1.items.push({ id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, icon: v.Icon, lvl: 2 });
                    //    else
                    //        lst.push({ id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, icon: v.Icon, lvl: 1 });
                    //}
                    //else
                    //    lst.push({ id: v.ID, code: v.Code, text: v.FunctionName, spriteCssClass: 'small ' + v.Icon, icon: v.Icon, lvl: 1 });

                    //if (v.ID == activeid) {
                    //    if (parentlvl2 != null) {
                    //        if (parentlvl2.id == v.ParentID)
                    //            $scope.main_MenuOptionBreakumbLoad(parentlvl1, parentlvl2, v);
                    //        else if (parentlvl1.id == v.ParentID)
                    //            $scope.main_MenuOptionBreakumbLoad(parentlvl1, null, v);
                    //        else
                    //            $scope.main_MenuOptionBreakumbLoad(null, null, v);
                    //    }
                    //    else if (parentlvl1 != null) {
                    //        if (parentlvl1.id == v.ParentID)
                    //            $scope.main_MenuOptionBreakumbLoad(parentlvl1, null, v);
                    //        else
                    //            $scope.main_MenuOptionBreakumbLoad(null, null, v);
                    //    }
                    //    else
                    //        $scope.main_MenuOptionBreakumbLoad(null, null, v);
                    //}
                });

                $scope.Functions = result;
                //$timeout(function () {
                //    $scope.main_MenuData = lst;

                //    var path = $location.path();
                //    if (path == '/main' || path == '') 
                //        $scope.main_HideMenu = false;
                //}, 1);
            }
        }
    });

    //$scope.main_MenuOptionBreakumbLoad = function (parentlvl1, parentlvl2, item) {
    //    if (parentlvl1 == null && parentlvl2 == null) {
    //        $rootScope.FunctionName = item.FunctionName;
    //        $rootScope.FunctionBreakumb = '<b>' + item.FunctionName + '</b>';
    //        $rootScope.FunctionBreakumbNoBold = item.FunctionName;
    //    }
    //    else if (parentlvl2 != null) {
    //        $rootScope.FunctionName = item.FunctionName;
    //        $rootScope.FunctionBreakumb = parentlvl1.text + ' &gt;&gt; ' + parentlvl2.text + ' &gt;&gt; <b>' + item.FunctionName + '</b>';
    //        $rootScope.FunctionBreakumbNoBold = parentlvl1.text + ' &gt;&gt; ' + parentlvl2.text + ' &gt;&gt; ' + item.FunctionName;
    //    }
    //    else if (parentlvl1 != null) {
    //        $rootScope.FunctionName = item.FunctionName;
    //        $rootScope.FunctionBreakumb = parentlvl1.text + ' &gt;&gt; <b>' + item.FunctionName + '</b>';
    //        $rootScope.FunctionBreakumbNoBold = parentlvl1.text + ' &gt;&gt; ' + item.FunctionName;
    //    }
    //};

    //$rootScope.ResizeMain();

    $scope.IsShowSetting = false;
    $scope.mainsetting = { IsDefault: false, Views: [] };
    $rootScope.FunctionSetting = function (options) {
        options = $.extend(true, {
            ListView: [],
            event: null,
            grid: null,
            current: null,
        }, options);

        //$scope.IsShowSetting = true;
        //var left = $(options.event.target).position().left;
        //var top = $(options.event.target).position().top + $(options.event.target).height();
        //$scope.mainsetting = { 'left': left, 'top': top };

        $scope.mainsetting.IsDefault = $state.current.name == $rootScope.FunctionItem.Code;
        $scope.winMainSetting.open().center();
        $scope.mainsetting.Views = [];
        var views = [];
        angular.forEach(options.ListView, function (v, i) {
            if (Common.HasValue(v.Code) && $rootScope.FunctionItem.CodeView + '.' + v.Code != options.current.name) {
                v.Code = $rootScope.FunctionItem.CodeView + '.' + v.Code;
                views.push(v);
            }
        });
        $scope.mainsetting.Views = views;
    };

    $scope.winMainSetting_Click = function ($event) {
        $event.preventDefault();

        $scope.winMainSetting.close();
    };

    $scope.MainSetting_Click = function ($event) {
        $event.preventDefault();

        $scope.mainsetting.IsDefault = !$scope.mainsetting.IsDefault;
    };
}]);

