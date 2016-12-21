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

    $rootScope.Loading.Show("Khởi tạo...");
    $rootScope.Loading.Change("Khởi tạo...", 20);

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
                    $rootScope.Comment_Show($rootScope.comment_item.ReferID, $rootScope.comment_title);
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

    $rootScope.FunctionItem = { IDMain: -1, ID: -1, Code: '', CodeView: '' };
    $rootScope.Breakumb = [];
    $rootScope.BreakumbDetail = [];
    $scope.ItemMenu_Click = function ($event, item) {
        $event.preventDefault();
        angular.forEach($rootScope.Functions, function (parent, i) {
            angular.forEach(parent.Childs, function (child, j) {
                if (child.IsActive == true)
                    child.IsActive = false;
            });
        });
        item.IsActive = true;
        $rootScope.FunctionItem = item;
        $rootScope.Breakumb = $.extend(true, [], item.Breakumb);
        $rootScope.BreakumbDetail = $.extend(true, [], item.BreakumbDetail);

        var acts = item.ListActionCode;
        if (!Common.HasValue(acts))
            acts = [];
        Common.Auth.Item.ListActionCode = acts;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _main.URL.ChangeAction,
            data: { ListActionCode: acts },
            success: function (res) { }
        });
        $state.go($rootScope.FunctionItem.Code);
    };

    $rootScope.ChildMenu_Click = function ($event, item) {
        $event.preventDefault();

        $rootScope.FunctionItem = item;
        $rootScope.Breakumb = $.extend(true, [], item.Breakumb);
        $rootScope.BreakumbDetail = $.extend(true, [], item.BreakumbDetail);
        var acts = item.ListActionCode;
        if (!Common.HasValue(acts))
            acts = [];
        Common.Auth.Item.ListActionCode = acts;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _main.URL.ChangeAction,
            data: { ListActionCode: acts },
            success: function (res) { }
        });
        $state.go($rootScope.FunctionItem.Code);
    };
    $rootScope.Breakumb_Find = function (node, id) {
        if (node.ID == id)
            return node;
        else {
            for (var i = 0; i < node.Childs.length; i++) {
                var rt = $rootScope.Breakumb_Find(node.Childs[i], id);
                if (rt != null)
                    return rt;
            }
            return null;
        }
    };
    $rootScope.Breakumb_Click = function ($event, item) {
        var current = null;
        for (var i = 0; i < $rootScope.Functions.length; i++) {
            current = $rootScope.Breakumb_Find($rootScope.Functions[i], item.ID);
            if (current != null)
                break;
        }
        item = current;

        $rootScope.FunctionItem = item;
        $rootScope.Breakumb = $.extend(true, [], item.Breakumb);
        $rootScope.BreakumbDetail = $.extend(true, [], item.BreakumbDetail);
        var acts = item.ListActionCode;
        if (!Common.HasValue(acts))
            acts = [];
        Common.Auth.Item.ListActionCode = acts;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _main.URL.ChangeAction,
            data: { ListActionCode: acts },
            success: function (res) { }
        });
    };

    //$timeout(function () {
    //    if ($state.current.name.split('.').length > 1) {
    //        $rootScope.FunctionItem.CodeView = $state.current.name.split('.')[0] + '.' + $state.current.name.split('.')[1];
    //        $rootScope.FunctionItem.Code = $state.current.name;
    //        $rootScope.FunctionItem.Params = $state.params;
    //    }
    //    //$state.go('main');
    //}, 1);

    $scope.menutooltip = {};
    $rootScope.ItemMenu_ShowToolTip = function ($event, item) {
        $event.stopPropagation();

        var top = $($event.currentTarget).offset().top - 59; //59px page top
        $scope.menutooltip.FunctionName = item.FunctionName;
        $scope.menutooltip.Style = {
            'visibility': 'visible', 'top': top, 'left': $event.target.offsetLeft - 99
        }
    }
    $rootScope.ItemMenu_HideToolTip = function () {
        $scope.menutooltip.Style = {
            'visibility': 'hidden'
        }
    }

    $scope.Merge = function (arrayA, arrayB) {
        var result = $.extend(true, [], arrayA);
        angular.forEach(arrayB, function (v, i) {
            result.push(v);
        });
        return result;
    };

    $rootScope.Functions = [];
    $rootScope.ListState = null;
    $timeout(function () {
        if ($state.current.name.split('.').length > 1) {
            $rootScope.FunctionItem.CodeView = $state.current.name.split('.')[0] + '.' + $state.current.name.split('.')[1];
            $rootScope.FunctionItem.Code = $state.current.name;
            $rootScope.FunctionItem.Params = $state.params;
        }
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _main.URL.ListFunction,
            data: {},
            success: function (res) {
                if (Common.HasValue(res)) {
                    var firstItem = null;
                    $rootScope.ListState = {};

                    var lst = [];
                    var lstLevel = [];
                    angular.forEach(res, function (v, i) {
                        var code = v.Code;
                        v.IsActive = false;

                        v.Note = null;
                        v.Childs = [];
                        v.Breakumb = [];
                        v.BreakumbDetail = [];
                        
                        var settings = [];
                        v.CodeView = 'main.' + v.Code;
                        settings['[' + v.ID + ']'] = { ReferKey: 'main.' + v.Code + '.Index' };
                        //
                        v.Code = settings['[' + v.ID + ']'].ReferKey;
                        
                        if (v.ListSettings.length > 0) {                            
                            angular.forEach(v.ListSettings, function (vSet, j) {
                                if (vSet.ReferKey == '' || vSet.ReferKey == null)
                                    settings['[' + v.ID + ']'] = vSet;
                                else
                                    settings[vSet.ReferKey] = vSet;
                            });
                        }
                        v.ListSettings = settings;
                        //v.Code = settings['[' + v.ID + ']'].ReferKey;
                        v.Breakumb.push({ ID: v.ID, IsLink: false, Code: v.Code, FunctionName: v.FunctionName });
                        v.BreakumbDetail.push({ ID: v.ID, IsLink: true, Code: v.Code, FunctionName: v.FunctionName });

                        var lvl = v.Level - 2;
                        if (lvl >= 0) {
                            if (lvl == 0) {
                                v.IDMain = v.ID;
                                lstLevel[lvl] = v;
                                lstLevel[1] = null, lstLevel[2] = null, lstLevel[3] = null, lstLevel[4] = null;
                                lst.push(v);
                            }
                            else {
                                var parent = lstLevel[lvl - 1];
                                if (lvl == 1)
                                    v.IDMain = v.ID;
                                else
                                    v.IDMain = parent.IDMain;
                                v.Breakumb = $scope.Merge(parent.BreakumbDetail, v.Breakumb);
                                v.BreakumbDetail = $scope.Merge(parent.BreakumbDetail, v.BreakumbDetail);
                                lstLevel[lvl - 1].Childs.push(v);
                                lstLevel[lvl] = v;
                            }
                        }

                        if ($rootScope.FunctionItem.CodeView == v.CodeView) {
                            var item = $.extend(true, { IDMain: -1, ID: -1, Code: '', CodeView: '' }, v);
                            item.Code = $rootScope.FunctionItem.Code;
                            item.Params = $rootScope.FunctionItem.Params;
                            $rootScope.FunctionItem = item;
                            $rootScope.Breakumb = $.extend(true, [], v.Breakumb);
                            $rootScope.BreakumbDetail = $.extend(true, [], v.BreakumbDetail);
                        }

                        $rootScope.ListState[code] = v;
                        if (firstItem == null && lvl == 1 && v.Childs.length == 0)
                            firstItem = v;
                    });

                    angular.forEach(lst, function (vP, i) {
                        angular.forEach(vP.Childs, function (vC, i) {
                            if (vC.ID == $rootScope.FunctionItem.IDMain) {
                                vC.IsActive = true;
                            }
                        });
                    });

                    $rootScope.Functions = lst;
                    if ($rootScope.FunctionItem.ID < 1 && firstItem != null) {
                        if (firstItem.ID == 16) {
                            firstItem.IsActive = true;
                            $rootScope.FunctionItem = firstItem;
                        }
                        else
                            $rootScope.FunctionItem = null;
                        //$state.go($rootScope.FunctionItem.Code);
                    }

                    if (Common.HasValue($rootScope._completefunction)) {
                        $timeout(function () {
                            $rootScope._completefunction();
                        }, 5);
                    }
                    else {
                        if (Common.HasValue($rootScope.ListState) && Common.HasValue($rootScope.FunctionItem) && $rootScope.FunctionItem.ID > 0) {
                            if ($rootScope.FunctionItem.Code != '') {
                                if (Common.HasValue($rootScope.FunctionItem.Params))
                                    $state.go($rootScope.FunctionItem.Code, $rootScope.FunctionItem.Params);
                                else
                                    $state.go($rootScope.FunctionItem.Code);
                            }
                        }
                    }

                    $rootScope.Loading.Change("Khởi tạo...", 80);
                    $rootScope.Loading.Hide();
                }
            }
        });
    }, 2);

    $rootScope.Loading.Change("Khởi tạo...", 50);

    $scope.VisibleSetting = function ($event) {
        $event.preventDefault();
        $rootScope.isPopupVisible = true;
        $scope.mainsetting.Style.visibility = 'visible';
    }
    $scope.HiddenSetting = function ($event) {
        $event.preventDefault();
        $rootScope.isPopupVisible = false;
        $scope.mainsetting.Style.visibility = 'hidden';
    }

    $scope.mainsetting = {
        IsDefault: false, Views: [],
        Style: {
            visibility: 'hidden',
            position: 'absolute',
            top: '0', left: '0'
        }, CallBack: null
    };
    $scope.MainSetting_Click = function ($event) {
        $event.preventDefault();

        $scope.mainsetting.IsDefault = !$scope.mainsetting.IsDefault;
        if ($scope.mainsetting.CallBack != null)
            $scope.mainsetting.CallBack($scope.mainsetting.IsDefault);
    };
    $scope.MainSetting_Change = function ($event, item) {
        var fun = $rootScope.FunctionItem;
        fun.Code = item.Code;
        Common.Cookie.Set('main_ItemMenu', JSON.stringify([{ ID: fun.ID, Code: fun.Code, CodeView: fun.CodeView, ListActions: fun.ListActions }]));
    };

    $rootScope.ShowSetting = function (options) {
        options = $.extend(true, {
            ListView: [], event: null, callback: null,
            grid: null, current: null, customview: false, customcache: ''
        }, options);
        $scope.mainsetting.CallBack = options.callback;
        var ele = $(options.event.target).closest('a');
        var off = $(options.event.target).closest('a').offset();
        var r = $(document).width() - off.left - 40;
        var t = off.top - ele.height();
        $scope.mainsetting.Style = {
            'top': t - 2, 'right': r, 'position': 'absolute', 'visibility': 'visible', 'z-index': '999999'
        };
        if (options.customview && options.customcache != '') {
            $scope.mainsetting.IsDefault = $state.current.name == Common.Cookie.Get(options.customcache);
        } else {
            $scope.mainsetting.IsDefault = $state.current.name == $rootScope.FunctionItem.Code;
        }
        $scope.mainsetting.Views = [];
        var views = [];
        angular.forEach(options.ListView, function (v, i) {
            if (Common.HasValue(v.Code) && $rootScope.FunctionItem.CodeView + '.' + v.Code != options.current.name) {
                v.Code = $rootScope.FunctionItem.CodeView + '.' + v.Code;
                views.push(v);
            }
        });
        $scope.mainsetting.Views = views;
        $rootScope.SettingGrid.grid = options.grid;

        $rootScope.PopupVisible(options.event.target);
    };
    $rootScope.HideSetting = function () {
        $scope.mainsetting.Style.visibility = 'hidden';
        $rootScope.isPopupVisible = false;
    };

    $rootScope.GridColumns = {};

    $rootScope.Close_Click = function ($event, win) {
        try {
            if (Common.HasValue($event))
                $event.preventDefault();

            win.close();
        } catch (e) {
            Common.Log("Window is not found!");
        }
    }

    //$rootScope.PerfectScrollbar = function (grid) {
    //    if (Common.HasValue(grid)) {
    //        var gridElement = grid.element;
    //        $(gridElement.find('.k-grid-content')).perfectScrollbar('destroy');
    //        $(gridElement.find('.k-grid-content')).perfectScrollbar({
    //            minScrollbarLength: 30
    //        });
    //        $(gridElement).find('.k-grid-header').css('padding-right', '0');
    //        $(gridElement).find('.k-grid-content-locked').css('height', $(gridElement).find('.k-grid-content-locked').height() + 17);
    //    }
    //}
    
}]);